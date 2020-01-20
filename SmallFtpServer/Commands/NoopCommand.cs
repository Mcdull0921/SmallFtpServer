using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    /// <summary>
    /// 空指令
    /// </summary>
    [FtpCommand("NOOP")]
    class NoopCommand : Command
    {
        public NoopCommand(Client client) : base(client)
        {

        }

        public override void Process(params string[] args)
        {
            client.Send(ResultCode.Success.ConvertString("NOOP"));
        }
    }
}
