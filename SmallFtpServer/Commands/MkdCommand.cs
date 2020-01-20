using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SmallFtpServer.Commands
{
    /// <summary>
    /// 新建文件夹
    /// </summary>
    [FtpCommand("MKD", 1, true)]
    [FtpCommand("XMKD", 1, true)]
    class MkdCommand : Command
    {
        public MkdCommand(Client client) : base(client)
        {

        }

        public override void Process(params string[] args)
        {
            if (!CreateDir(args[0]))
                client.Send(ResultCode.InvalidFile.ConvertString("目录已经存在"));
            else
                client.Send(ResultCode.FileComplete.ConvertString("目录创建成功"));
        }

        private bool CreateDir(string dirName)
        {
            var dir = client.LoginInfo.GetAbsolutePath(dirName);
            Console.WriteLine("创建目录" + dir);
            if (Directory.Exists(dir))
            {
                return false;
            }
            else
            {
                Directory.CreateDirectory(dir);
                return true;
            }
        }
    }
}
