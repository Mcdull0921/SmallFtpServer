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
            string info = string.Format("{0} Type: {1}", Environment.OSVersion, client.LoginInfo.transferMode);
            client.Send(ResultCode.SystemInfo.ConvertString(info));
        }

    }
}
