using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    /// <summary>
    /// 重命名文件-原始文件名
    /// </summary>
    [FtpCommand("RNFR", 1, true)]
    class RnfrCommand : Command
    {
        public RnfrCommand(Client client) : base(client)
        {

        }

        public override void Process(params string[] args)
        {
            client.LoginInfo.rename_filename = args[0];
            client.Send(ResultCode.MoreFileInfo.ConvertString());
        }
    }
}
