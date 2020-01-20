using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    /// <summary>
    /// 跳转到上一级目录
    /// </summary>
    [FtpCommand("CDUP", needLogin: true)]
    class CdupCommand : Command
    {
        public CdupCommand(Client client) : base(client)
        {

        }

        public override void Process(params string[] args)
        {
            if (client.LoginInfo.ChangeWorkDirectory(".."))
            {
                client.Send(ResultCode.PrintWorkDirectory.ConvertString(client.LoginInfo.CurrentVirtualPath));
            }
            else
            {
                client.Send(ResultCode.InvalidFilePath.ConvertString("已经是根目录"));
            }
        }
    }
}
