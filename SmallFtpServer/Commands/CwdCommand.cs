using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SmallFtpServer.Commands
{
    /// <summary>
    /// 跳转目录
    /// </summary>
    [FtpCommand("CWD", 1, true)]
    class CwdCommand : Command
    {
        public CwdCommand(Client client) : base(client)
        {

        }

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
