using SmallFtpServer.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    class QuitCommand : Command
    {
        public QuitCommand(Client client) : base(client)
        {

        }

        public override CommandType CommandType => CommandType.QUIT;

        public override void Process(params string[] args)
        {
            throw new CloseConnectException();
        }
    }
}
