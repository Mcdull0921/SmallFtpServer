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
            Uri rootpath = new Uri(client.LoginInfo.rootDir.FullName, UriKind.Absolute);
            Uri currentpath = new Uri(client.LoginInfo.currentDir.FullName, UriKind.Absolute);
            Uri path = rootpath.MakeRelativeUri(currentpath);
            client.Send(ResultCode.PrintWorkDirectory.ConvertString("/" + path.OriginalString));
        }


    }
}
