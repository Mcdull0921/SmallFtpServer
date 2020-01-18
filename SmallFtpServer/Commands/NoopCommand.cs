using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    class NoopCommand : Command
    {
        public NoopCommand(Client client) : base(client)
        {

        }
        public override CommandType CommandType => CommandType.NOOP;

        public override void Process(params string[] args)
        {
            client.Send(ResultCode.Success.ConvertString("NOOP"));
        }
    }
}
