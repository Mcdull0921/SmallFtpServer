using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmallFtpServer
{
    class Client
    {
        public LoginUser CurrentUser { get; private set; }
        public string Id { get; private set; }

        IEnumerable<UserInfo> users;
        Dictionary<CommandType, Command> commands;
        Socket currentSocket;
        CancellationTokenSource cts;
        CancellationToken token;
        public event Action<Client> OnClose;
        public Client(Socket socket, IEnumerable<UserInfo> users)
        {
            Id = Guid.NewGuid().ToString();
            commands = new Dictionary<CommandType, Command>();
            foreach (var t in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (t.IsClass && !t.IsAbstract && typeof(Command).IsAssignableFrom(t))
                {
                    var command = (Command)Activator.CreateInstance(t, this);
                    commands.Add(command.CommandType, command);
                }
            }
            CurrentUser = new LoginUser();
            this.users = users;
            this.currentSocket = socket;
            cts = new CancellationTokenSource();
            token = cts.Token;
        }

        private void Handle(string[] cmd)
        {
            //object obj;
            //if (!Enum.TryParse(typeof(CommandType), cmd, true, out obj))
            //    throw new Exception(Command.FormatMsg(ResultCode.UnKownCommand));
            //CommandType commandType = (CommandType)obj;
            //if (!commands.ContainsKey(commandType))
            //    throw new Exception(Command.FormatMsg(ResultCode.UnKownCommand));
            //var command = commands[commandType];
            //command.Process();
        }

        public void Start()
        {
            Task.Run(() =>
            {
                Send(ResultCode.Ready.ConvertString());
                while (true)
                {
                    token.ThrowIfCancellationRequested();
                    var commands = Receive();
                    foreach (var c in commands)
                        Handle(c);
                }
            });
        }

        public void Dispose()
        {
            currentSocket.Close();
            cts.Cancel();
        }

        private void Close()
        {
            Dispose();
            OnClose?.Invoke(this);
        }

        private List<string[]> Receive()
        {
            List<string[]> res = new List<string[]>();
            byte[] buff = new byte[1024];
            try
            {
                currentSocket.Receive(buff);
                string clientCommand = Encoding.ASCII.GetString(buff);
                clientCommand = clientCommand.Trim("\0".ToCharArray());
                clientCommand = clientCommand.Trim("\r\n".ToCharArray());//"PORT 192,168,0,105,51,49\r\nLIST\r\n"这种情况怎么处理，2条命令同时发来
                var msgs = clientCommand.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (var msg in msgs)
                {
                    var command = new string[2];
                    if (msg.Length > 0)
                    {
                        var index = msg.IndexOf(" ");
                        if (index > -1)
                        {
                            res.Add(new string[] { msg.Substring(0, index), msg.Substring(index + 1, msg.Length - index - 1) });
                        }
                        else
                            res.Add(new string[] { msg });
                    }
                }
            }
            catch
            {
                Close();
            }
            return res;
        }

        private void Send(string message)
        {
            message += "\r\n";
            Send(Encoding.ASCII.GetBytes(message.ToCharArray()));
        }

        private void Send(byte[] bytes)
        {
            try
            {
                currentSocket.Send(bytes, bytes.Length, 0);
            }
            catch
            {

            }
        }
    }
}
