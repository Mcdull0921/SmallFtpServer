using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SmallFtpServer.Commands
{
    [Authentication, Argument(1)]
    class DeleCommand : Command
    {
        public DeleCommand(Client client) : base(client)
        {

        }
        public override CommandType CommandType => CommandType.DELE;

        public override void Process(params string[] args)
        {
            try
            {
                switch (Delete(args[0]))
                {
                    case 1:
                        client.Send(ResultCode.FileComplete.ConvertString("目录删除成功"));
                        return;
                    case 2:
                        client.Send(ResultCode.FileComplete.ConvertString("文件删除成功"));
                        return;
                }
                throw new InvalidFileException("目录或者文件不存在");
            }
            catch (Exception ex)
            {
                throw new InvalidFileException(ex.Message);
            }
        }

        private int Delete(string path)
        {
            string filename = client.LoginInfo.GetAbsolutePath(path);
            Console.WriteLine("删除目录/文件" + filename);
            FileInfo fi = new FileInfo(filename);
            if (Directory.Exists(filename))
            {
                Directory.Delete(filename, true);
                return 1;
            }
            else if (File.Exists(filename))
            {
                File.Delete(filename);
                fi.Delete();
                return 2;
            }
            return 0;
        }
    }
}
