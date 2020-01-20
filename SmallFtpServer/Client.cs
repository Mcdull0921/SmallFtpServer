using SmallFtpServer.Commands;
using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmallFtpServer
{
    class Client
    {
        public LoginInfo LoginInfo { get; private set; }
        public IEnumerable<UserInfo> Users { get; private set; }
        public string Id { get; private set; }
        public event Action<Client> OnClose;

        TcpListener pasvListener;
        IPEndPoint pasvEndPoint;
        Dictionary<string, Tuple<Command, FtpCommandAttribute>> commands;
        Socket currentSocket;
        Socket transSocket;
        CancellationTokenSource cts;
        CancellationToken token;
        Encoding defaultEncoding = Encoding.UTF8;

        public Client(Socket socket, IEnumerable<UserInfo> users, TcpListener pasvListener, IPEndPoint pasvEndPoint = null)
        {
            Id = Guid.NewGuid().ToString();
            commands = new Dictionary<string, Tuple<Command, FtpCommandAttribute>>();
            foreach (var t in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (t.IsClass && !t.IsAbstract && typeof(Command).IsAssignableFrom(t))
                {
                    var command = (Command)Activator.CreateInstance(t, this);
                    foreach (var attr in command.GetType().GetCustomAttributes<FtpCommandAttribute>())
                    {
                        commands.Add(attr.Name, new Tuple<Command, FtpCommandAttribute>(command, attr));
                    }
                }
            }
            LoginInfo = new LoginInfo();
            Users = users;
            currentSocket = socket;
            cts = new CancellationTokenSource();
            token = cts.Token;
            this.pasvListener = pasvListener;
            this.pasvEndPoint = pasvEndPoint;
        }

        private void Handle(CommandMsg commandMsg)
        {
            try
            {
                if (!commands.ContainsKey(commandMsg.cmd))
                    throw new UnKownCommandException();
                var command = commands[commandMsg.cmd];
                if (commandMsg.args.Length < command.Item2.ArgsNumber)
                    throw new ArgumentErrorException();
                if (command.Item2.NeedLogin && !LoginInfo.islogin)
                    throw new NeedLoginException();
                command.Item1.Process(commandMsg.args);
            }
            catch (FtpException ex)
            {
                Send(ex.Message);
                if (transSocket != null)
                    transSocket.Close();
                if (ex is CloseConnectException)
                    Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("全局异常：" + ex.Message);
                Send(ResultCode.CloseConnect.ConvertString());
                Close();
            }
        }

        public void Start()
        {
            Task.Run(() =>
            {
                Send(ResultCode.Ready.ConvertString());
                while (true)
                {
                    token.ThrowIfCancellationRequested();
                    var msgs = Receive();
                    foreach (var c in msgs)
                        Handle(c);
                }
            });
        }

        public void Dispose()
        {
            if (transSocket != null)
                transSocket.Close();
            currentSocket.Close();
            cts.Cancel();
        }

        private void Close()
        {
            Dispose();
            OnClose?.Invoke(this);
        }

        private List<CommandMsg> Receive()
        {
            var res = new List<CommandMsg>();
            byte[] buff = new byte[1024];
            try
            {
                currentSocket.Receive(buff);
                string clientCommand = defaultEncoding.GetString(buff);
                clientCommand = clientCommand.Trim("\0".ToCharArray());
                if (clientCommand.Length == 0)
                    throw new CloseConnectException();
                clientCommand = clientCommand.Trim("\r\n".ToCharArray());
                var msgs = clientCommand.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (var msg in msgs)
                {
                    if (msg.Length > 0)
                    {
                        Console.WriteLine(msg);
                        var index = msg.IndexOf(" ");
                        if (index > -1)
                        {
                            res.Add(new CommandMsg(msg.Substring(0, index), msg.Substring(index + 1, msg.Length - index - 1)));
                        }
                        else
                            res.Add(new CommandMsg(msg));
                    }
                }
            }
            catch
            {
                Close();
            }
            return res;
        }

        public void Send(string message)
        {
            message += "\r\n";
            Send(currentSocket, defaultEncoding.GetBytes(message.ToCharArray()));
        }

        public void SendDatas(string datas)
        {
            SendDatas(defaultEncoding.GetBytes(datas.ToCharArray()));
        }

        public void SendDatas(byte[] datas)
        {
            if (datas.Length > 0)
            {
                if (transSocket != null && transSocket.Connected)
                {
                    Send(transSocket, datas);
                    transSocket.Close();
                }
                else
                {
                    throw new CannotTransmitException();
                }
            }
        }

        public void ReceiveFile(string path)
        {
            if (transSocket != null && transSocket.Connected)
            {
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                byte[] buffer = new byte[1024];
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
                {
                    fs.Seek(0, SeekOrigin.Begin);
                    int length = 0;
                    do
                    {
                        length = transSocket.Receive(buffer);
                        fs.Write(buffer, 0, length);
                    }
                    while (length > 0);
                    fs.Close();
                }
            }
            else
                throw new CannotTransmitException();
        }

        public IPEndPoint PasvEndPoint
        {
            get
            {
                if (pasvEndPoint == null)
                    return (IPEndPoint)pasvListener.LocalEndpoint;
                return pasvEndPoint;
            }
        }

        public void SetPasvSocket()
        {
            int timeout = 5000;
            while (timeout-- > 0)
            {
                if (pasvListener.Pending())
                {
                    transSocket = pasvListener.AcceptSocket();
                    return;
                }
                Thread.Sleep(500);
            }
            throw new CannotTransmitException();
        }

        public void SetPortSocket(IPAddress ip, int port)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 5000);
            IPEndPoint hostEndPoint = new IPEndPoint(ip, port);
            try
            {
                socket.Connect(hostEndPoint);
                transSocket = socket;
            }
            catch (SocketException ex)
            {
                Console.WriteLine("PORT连接失败：{0}", ex.Message);
                throw new CannotTransmitException();
            }
        }

        private void Send(Socket socket, byte[] bytes)
        {
            try
            {
                socket.Send(bytes, bytes.Length, 0);
            }
            catch
            {

            }
        }
    }
}
