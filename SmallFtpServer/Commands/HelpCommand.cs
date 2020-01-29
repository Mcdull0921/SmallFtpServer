using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    [FtpCommand("HELP")]
    class HelpCommand : Command
    {
        public HelpCommand(Client client) : base(client)
        {

        }

        public override void Process(params string[] args)
        {
            client.Send(ResultCode.HelpInfo.ConvertString(string.Join(' ', client.Commands)));
        }
    }
}
