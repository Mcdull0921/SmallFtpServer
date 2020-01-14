using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer
{
    abstract class Command
    {
        protected Client client;
        public Command(Client client)
        {
            this.client = client;
        }
        public abstract void Process(params string[] objs);
        public abstract CommandType CommandType { get; }

        public static string FormatMsg(ResultCode code)
        {
            switch (code)
            {
                default:
                    return string.Format("{0} {1}", (int)code, code.GetDescription());
            }
        }
    }
}
