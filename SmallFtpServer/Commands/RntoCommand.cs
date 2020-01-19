using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SmallFtpServer.Commands
{
    [Authentication, Argument(1)]
    class RntoCommand : Command
    {
        public RntoCommand(Client client) : base(client)
        {

        }
        public override CommandType CommandType => CommandType.RNTO;

        public override void Process(params string[] args)
        {
            if (string.IsNullOrEmpty(client.LoginInfo.rename_filename))
                throw new InvalidFileException("目录或者文件不存在");
            try
            {
                switch (Rename(client.LoginInfo.rename_filename, args[0]))
                {
                    case 1:
                        client.Send(ResultCode.FileComplete.ConvertString("目录改名成功"));
                        return;
                    case 2:
                        client.Send(ResultCode.FileComplete.ConvertString("文件改名成功"));
                        return;
                }
                throw new InvalidFileException("目录或者文件不存在");
            }
            catch (Exception ex)
            {
                throw new InvalidFileException(ex.Message);
            }
        }

        private int Rename(string from, string to)
        {
            string name = client.LoginInfo.GetAbsolutePath(from);
            string new_name = client.LoginInfo.GetAbsolutePath(to);
            Console.WriteLine("重命名" + name + "到" + new_name);
            FileInfo fi = new FileInfo(name);
            if (Directory.Exists(name))
            {
                Directory.Move(name, new_name);
                return 1;
            }
            else if (fi.Exists)
            {
                fi.MoveTo(new_name);
                return 2;
            }
            return 0;
        }
    }
}
