using SmallFtpServer.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    class RnfrCommand : Command
    {
        public RnfrCommand(Client client) : base(client)
        {

        }
        public override CommandType CommandType => CommandType.RNFR;

        public override void Process(params string[] args)
        {
            throw new UnKownCommandException();
        }
    }
}
