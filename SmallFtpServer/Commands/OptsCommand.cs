using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    class OptsCommand : Command
    {
        public OptsCommand(Client client) : base(client)
        {

        }

        public override CommandType CommandType => CommandType.OPTS;

        public override void Process(params string[] args)
        {
            client.Send(ResultCode.Success.ConvertString(string.Join(" ", args)));
            //UpdateEncode(args[0], args[1]);
        }

        //private void UpdateEncode(string name, string mode)
        //{
        //    if (mode.ToUpper() == "ON")
        //    {
        //        switch (name.ToUpper())
        //        {
        //            case "UTF8":
        //                client.defaultEncoding = Encoding.UTF8;
        //                break;
        //        }
        //    }
        //    else
        //        client.defaultEncoding = Encoding.Unicode;
        //}
    }
}
