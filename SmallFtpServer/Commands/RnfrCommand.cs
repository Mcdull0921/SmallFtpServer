using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    [Authentication, Argument(1)]
    class RnfrCommand : Command
    {
        public RnfrCommand(Client client) : base(client)
        {

        }
        public override CommandType CommandType => CommandType.RNFR;

        public override void Process(params string[] args)
        {
            client.LoginInfo.rename_filename = args[0];
            client.Send(ResultCode.MoreFileInfo.ConvertString());
        }
    }
}
