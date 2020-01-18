using SmallFtpServer.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    class XmkdCommand : Command
    {
        public XmkdCommand(Client client) : base(client)
        {

        }
        public override CommandType CommandType => CommandType.XMKD;

        public override void Process(params string[] args)
        {
            throw new UnKownCommandException();
        }
    }
}
