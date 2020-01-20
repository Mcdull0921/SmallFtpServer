using SmallFtpServer.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    /// <summary>
    /// 退出登录
    /// </summary>
    [FtpCommand("QUIT")]
    class QuitCommand : Command
    {
        public QuitCommand(Client client) : base(client)
        {

        }

        public override void Process(params string[] args)
        {
            throw new CloseConnectException();
        }
    }
}
