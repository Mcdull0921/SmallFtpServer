using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    /// <summary>
    /// 发送密码
    /// </summary>
    [FtpCommand("PASS", 1)]
    class PassCommand : Command
    {
        public PassCommand(Client client) : base(client)
        {

        }

        public override void Process(params string[] args)
        {
            if (string.IsNullOrEmpty(client.LoginInfo.username))
                throw new NeedLoginException();
            if (Login(client.LoginInfo.username, args[0]))
                client.Send(ResultCode.Login.ConvertString());
            else
            {
                client.LoginInfo.LoginOut();
                client.Send(ResultCode.UnLogin.ConvertString());
            }
        }

        private bool Login(string username, string password)
        {
            foreach (var u in client.Users)
            {
                if (u.username.Equals(username) && u.password.Equals(password))
                {
                    client.LoginInfo.Login(u);
                    return true;
                }
            }
            return false;
        }
    }
}
