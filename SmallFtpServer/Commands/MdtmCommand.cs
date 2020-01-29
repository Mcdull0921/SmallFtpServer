using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SmallFtpServer.Commands
{
    [FtpCommand("MDTM", 1, true)]
    class MdtmCommand : Command
    {
        public MdtmCommand(Client client) : base(client)
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
                client.Send(ResultCode.FileInfo.ConvertString(file.LastWriteTimeUtc.ToString("yyyyMMddHHmmss.fff")));
            }
            catch (Exception ex)
            {
                throw new InvalidFileException(ex.Message);
            }
        }
    }
}
