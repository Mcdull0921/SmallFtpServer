using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    [Authentication, Argument(1)]
    class StorCommand : Command
    {
        public StorCommand(Client client) : base(client)
        {

        }
        public override CommandType CommandType => CommandType.STOR;

        public override void Process(params string[] args)
        {
            string path = client.LoginInfo.GetAbsolutePath(args[0]);
            client.Send(ResultCode.BeginTransmit.ConvertString());
            if (client.ReceiveFile(path))
                client.Send(ResultCode.EndTransmit.ConvertString());
            else
                throw new InvalidFileException("不能上传文件(文件可能已存在)");
        }      
    }
}
