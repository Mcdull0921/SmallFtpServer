﻿using SmallFtpServer.Exceptions;
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
        public LoginInfo LoginInfo { get; private set; }
        public IEnumerable<UserInfo> Users { get; private set; }
        public string Id { get; private set; }
        public event Action<Client> OnClose;


        Dictionary<CommandType, Command> commands;
        Socket currentSocket;
        CancellationTokenSource cts;
        CancellationToken token;
        Encoding defaultEncoding = Encoding.UTF8;

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
            LoginInfo = new LoginInfo();
            this.Users = users;
            this.currentSocket = socket;
            cts = new CancellationTokenSource();
            token = cts.Token;
        }

        private void Handle(CommandMsg commandMsg)
        {
            try
            {
                Console.WriteLine(commandMsg);
                object obj;
                if (!Enum.TryParse(typeof(CommandType), commandMsg.cmd, true, out obj))
                    throw new UnKownCommandException();
                CommandType commandType = (CommandType)obj;
                if (!commands.ContainsKey(commandType))
                    throw new UnKownCommandException();
                var command = commands[commandType];
                AuthenticationAttribute authentication = command.GetType().GetCustomAttribute<AuthenticationAttribute>();
                if (authentication != null && LoginInfo.user == null)
                    throw new NeedLoginException();
                command.Process(commandMsg.args);
            }
            catch (FtpException ex)
            {
                Send(ex.Message);
                if (ex is CloseConnectException)
                    Close();
            }
            catch (Exception ex)
            {
                Send(ResultCode.CloseConnect.ConvertString());
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
                clientCommand = clientCommand.Trim("\r\n".ToCharArray());//"PORT 192,168,0,105,51,49\r\nLIST\r\n"这种情况怎么处理，2条命令同时发来
                var msgs = clientCommand.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (var msg in msgs)
                {
                    if (msg.Length > 0)
                    {
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
            Send(defaultEncoding.GetBytes(message.ToCharArray()));
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
