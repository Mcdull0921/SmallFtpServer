using SmallFtpServer.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    class RntoCommand : Command
    {
        public RntoCommand(Client client) : base(client)
        {

        }
        public override CommandType CommandType => CommandType.RNTO;

        public override void Process(params string[] args)
        {
            throw new UnKownCommandException();
        }
    }
}
