using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer
{
    class RequestHandle
    {
        Client client;
        public RequestHandle(Client client)
        {
            this.client = client;
        }

        public void Handle(string cmd)
        {
            var aa = (CommandType)Enum.Parse(typeof(CommandType), cmd);
            Console.WriteLine(aa);
        }
    }
}
