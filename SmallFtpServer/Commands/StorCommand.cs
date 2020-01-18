using SmallFtpServer.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    class StorCommand : Command
    {
        public StorCommand(Client client) : base(client)
        {

        }
        public override CommandType CommandType => CommandType.STOR;

        public override void Process(params string[] args)
        {
            throw new UnKownCommandException();
        }
    }
}
