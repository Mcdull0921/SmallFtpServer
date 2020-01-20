using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Models
{
    class CommandMsg
    {
        public string cmd { get; private set; }
        public string[] args { get; private set; }
        public CommandMsg(string cmd, string args = null)
        {
            this.cmd = cmd.ToUpper();
            //this.args = args.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            this.args = string.IsNullOrEmpty(args) ? new string[0] : new string[] { args };
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", cmd, string.Join(" ", args));
        }
    }
}
