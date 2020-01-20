using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    /// <summary>
    /// 当前目录
    /// </summary>
    [FtpCommand("PWD", needLogin: true)]
    [FtpCommand("XPWD", needLogin: true)]
    class PwdCommand : Command
    {
        public PwdCommand(Client client) : base(client)
        {

        }

        public override void Process(params string[] args)
        {
            client.Send(ResultCode.PrintWorkDirectory.ConvertString(client.LoginInfo.CurrentVirtualPath));
        }


    }
}
