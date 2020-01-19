using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SmallFtpServer.Commands
{
    [Authentication]
    class PasvCommand : Command
    {
        public PasvCommand(Client client) : base(client)
        {

        }

        public override CommandType CommandType => CommandType.PASV;

        public override void Process(params string[] args)
        {
            var endPoint = client.PasvEndPoint;
            string ip = string.Format("{0},{1},{2}", endPoint.Address.ToString().Replace('.', ','), endPoint.Port >> 8, endPoint.Port & 0xff);
            client.Send(ResultCode.PasvMode.ConvertString(ip));
            client.SetPasvSocket();
        }
    }
}
