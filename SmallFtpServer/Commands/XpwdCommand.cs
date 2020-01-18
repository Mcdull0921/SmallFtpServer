using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    [Authentication]
    class XpwdCommand : Command
    {
        public XpwdCommand(Client client) : base(client)
        {

        }

        public override CommandType CommandType => CommandType.XPWD;

        public override void Process(params string[] args)
        {
            client.Send(ResultCode.PrintWorkDirectory.ConvertString(client.LoginInfo.CurrentVirtualPath));
        }


    }
}
