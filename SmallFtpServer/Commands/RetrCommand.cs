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
            var file = GetFile(client.LoginInfo, args[0]);
            if (file == null)
            {
                client.Send(ResultCode.InvalidRequest.ConvertString("文件不存在"));
            }
            else if (file.Length == 0)
            {
                client.Send(ResultCode.InvalidRequest.ConvertString("文件大小为空"));
            }
            else
            {
                client.Send("150 开始传输数据");
                //client.SendMessageByTempSocket(getTempSocket(), file);
                //client.SendMessage("226 文件发送完毕");
            }
        }

        private byte[] GetFile(LoginInfo loginInfo, string filename)
        {
            string name = GetFileName(loginInfo, filename);
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
        private string GetFileName(LoginInfo loginInfo, string name)
        {
            if (name[0] == '/')
            {
                name = name.TrimStart("/".ToCharArray());
                return loginInfo.rootDir.FullName + name;
            }
            else
                return loginInfo.currentDir.FullName + name.TrimEnd("\r\n".ToCharArray());
        }
    }
}
