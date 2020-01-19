using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace SmallFtpServer.Commands
{
    [Authentication]
    class ListCommand : Command
    {
        public ListCommand(Client client) : base(client)
        {

        }

        public override CommandType CommandType => CommandType.LIST;

        public override void Process(params string[] args)
        {
            client.Send(ResultCode.BeginTransmit.ConvertString());
            client.SendDatas(GetList(client.LoginInfo.CurrentFullPath, true));
            client.Send(ResultCode.EndTransmit.ConvertString());
        }
    }
}
