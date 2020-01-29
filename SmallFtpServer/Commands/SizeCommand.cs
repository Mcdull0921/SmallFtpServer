using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Globalization;
using System.IO;

namespace SmallFtpServer.Commands
{
    [FtpCommand("SIZE", 1, true)]
    class SizeCommand : Command
    {
        public SizeCommand(Client client) : base(client)
        {

        }

        public override void Process(params string[] args)
        {
            try
            {
                string path = client.LoginInfo.GetAbsolutePath(args[0]);
                FileInfo file = new FileInfo(path);
                if (!file.Exists)
                    throw new InvalidFileException("文件不存在");
                client.Send(ResultCode.FileInfo.ConvertString(string.Format(CultureInfo.InvariantCulture, "{0}", file.Length)));
            }
            catch (Exception ex)
            {
                throw new InvalidFileException(ex.Message);
            }
        }
    }
}
