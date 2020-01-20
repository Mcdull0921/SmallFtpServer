using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    /// <summary>
    /// 存储文件
    /// </summary>
    [FtpCommand("STOR", 1, true)]
    class StorCommand : Command
    {
        public StorCommand(Client client) : base(client)
        {

        }

        public override void Process(params string[] args)
        {
            string path = client.LoginInfo.GetAbsolutePath(args[0]);
            client.Send(ResultCode.BeginTransmit.ConvertString());
            client.ReceiveFile(path);
            client.Send(ResultCode.EndTransmit.ConvertString());
        }
    }
}
