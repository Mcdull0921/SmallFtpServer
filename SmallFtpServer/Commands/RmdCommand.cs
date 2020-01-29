using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SmallFtpServer.Commands
{
    /// <summary>
    /// 删除文件夹
    /// </summary>
    [FtpCommand("RMD", 1, true)]
    class RmdCommand : Command
    {
        public RmdCommand(Client client) : base(client)
        {

        }

        public override void Process(params string[] args)
        {
            try
            {
                string path = client.LoginInfo.GetAbsolutePath(args[0]);
                FtpServer.Logger.Info("删除目录" + path);
                if (!Directory.Exists(path))
                    throw new InvalidFileException("目录不存在");
                Directory.Delete(path, true);
                client.Send(ResultCode.FileComplete.ConvertString("目录删除成功"));
            }
            catch (Exception ex)
            {
                throw new InvalidFileException(ex.Message);
            }
        }
    }
}
