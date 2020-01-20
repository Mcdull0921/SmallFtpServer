using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    /// <summary>
    /// 服务器系统信息
    /// </summary>
    [FtpCommand("SYST")]
    class SystCommand : Command
    {
        public SystCommand(Client client) : base(client)
        {

        }

        public override void Process(params string[] objs)
        {
            client.Send(ResultCode.SystemInfo.ConvertString(Environment.OSVersion.ToString()));
        }

    }
}
