using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Models
{
    class CommandMsg
    {
        public string cmd { get; private set; }
        public string[] args { get; private set; }
        public CommandMsg(string cmd, string args)
        {
            this.cmd = cmd;
            this.args = args.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }

        public CommandMsg(string cmd)
        {
            this.cmd = cmd;
            this.args = new string[0];
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", cmd, string.Join(" ", args));
        }
    }
}
