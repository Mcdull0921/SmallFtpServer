using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SmallFtpServer.Commands
{
    [Authentication, Argument(1)]
    class RetrCommand : Command
    {
        public RetrCommand(Client client) : base(client)
        {

        }

        public override CommandType CommandType => CommandType.RETR;

        public override void Process(params string[] args)
        {
            var file = GetFile(args[0]);
            if (file == null)
                throw new InvalidFileException("文件不存在");
            if (file.Length == 0)
                throw new InvalidFileException("文件大小为空");
            client.Send(ResultCode.BeginTransmit.ConvertString());
            client.SendDatas(file);
            client.Send(ResultCode.EndTransmit.ConvertString());
        }

        private byte[] GetFile(string filename)
        {
            string name = client.LoginInfo.GetAbsolutePath(filename);
            FileInfo fi = new FileInfo(name);
            if (fi.Exists)
            {
                FileStream fs = fi.OpenRead();
                byte[] b = new byte[fs.Length];
                fs.Read(b, 0, b.Length);
                fs.Close();
                return b;
            }
            else
            {
                return null;
            }
        }

    }
}
