using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    [Authentication]
    class PasvCommand : Command
    {
        public PasvCommand(Client client) : base(client)
        {

        }

        public override CommandType CommandType => CommandType.PASV;

        public override void Process(params string[] args)
        {

        }
    }
}
