using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    class PassCommand : Command
    {
        public PassCommand(Client client) : base(client)
        {

        }

        public override CommandType CommandType => CommandType.PASS;

        public override void Process(params string[] args)
        {
            if (args.Length < 1)
                throw new ArgumentErrorException();
            if (string.IsNullOrEmpty(client.LoginInfo.username))
                throw new NeedLoginException();
            if (Login(client.LoginInfo.username, args[0]))
                client.Send(ResultCode.Login.ConvertString());
            else
            {
                client.LoginInfo.user = null;
                client.LoginInfo.username = null;
                client.Send(ResultCode.UnLogin.ConvertString());
            }
        }

        private bool Login(string username, string password)
        {
            foreach (var u in client.Users)
            {
                if (u.username.Equals(username) && u.password.Equals(password))
                {
                    client.LoginInfo.currentDir = u.rootdirectory;
                    client.LoginInfo.user = u;
                    return true;
                }
            }
            return false;
        }
    }
}
