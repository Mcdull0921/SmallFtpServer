using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SmallFtpServer.Commands
{
    [Authentication, Argument(1)]
    class CwdCommand : Command
    {
        public CwdCommand(Client client) : base(client)
        {

        }

        public override CommandType CommandType => CommandType.CWD;

        public override void Process(params string[] args)
        {
            var dirPath = args[0].TrimEnd("\r\n".ToCharArray());
            if (client.LoginInfo.ChangeWorkDirectory(dirPath))
            {
                client.Send(ResultCode.PrintWorkDirectory.ConvertString(client.LoginInfo.CurrentVirtualPath));
            }
            else
            {
                client.Send(ResultCode.InvalidFilePath.ConvertString(dirPath));
            }
        }
    }
}
