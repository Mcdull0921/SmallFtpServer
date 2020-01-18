using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SmallFtpServer.Commands
{
    [Authentication, Argument(1)]
    class PortCommand : Command
    {
        public PortCommand(Client client) : base(client)
        {

        }

        public override CommandType CommandType => CommandType.PORT;

        public override void Process(params string[] args)
        {
            string[] data = args[0].Split(new char[] { ',' });
            if (data.Length != 6)
                throw new ArgumentErrorException();
            var port = (int.Parse(data[4]) << 8) + int.Parse(data[5]);
            var ip = IPAddress.Parse(data[0] + "." + data[1] + "." + data[2] + "." + data[3]);
            var socket = GetSocket(ip, port);
            if (socket == null)
                throw new CannotTransmitException();
            client.Send(ResultCode.Success.ConvertString(args[0]));
            client.SetTransSockect(socket);
        }

        private Socket GetSocket(IPAddress ip, int port)
        {
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 5000);
            IPEndPoint hostEndPoint = new IPEndPoint(ip, port);
            try
            {
                socket.Connect(hostEndPoint);
                return socket;
            }
            catch (SocketException ex)
            {
                Console.WriteLine("PORT连接失败：{0}", ex.Message);
                return null;
            }
        }
    }
}
