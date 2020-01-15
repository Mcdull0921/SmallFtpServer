using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    class SystCommand : Command
    {
        public SystCommand(Client client) : base(client)
        {

        }
        public override CommandType CommandType => CommandType.SYST;

        public override void Process(params string[] objs)
        {
            client.Send(ResultCode.SystemInfo.ConvertString(Environment.OSVersion.ToString()));
        }

    }
}
