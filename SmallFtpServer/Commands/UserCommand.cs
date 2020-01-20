using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    /// <summary>
    /// 发送用户名
    /// </summary>
    [FtpCommand("USER", 1)]
    class UserCommand : Command
    {
        public UserCommand(Client client) : base(client)
        {

        }

        public override void Process(params string[] args)
        {
            client.LoginInfo.username = args[0];
            client.Send(ResultCode.NeedPassword.ConvertString());
        }
    }
}
