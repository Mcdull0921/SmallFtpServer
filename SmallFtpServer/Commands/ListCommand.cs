using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace SmallFtpServer.Commands
{
    class ListCommand : Command
    {
        public ListCommand(Client client) : base(client)
        {

        }

        public override CommandType CommandType => CommandType.LIST;

        public override void Process(params string[] args)
        {
            client.Send(ResultCode.BeginTransmit.ConvertString());
            client.SendDatas(GetList(client.LoginInfo.CurrentFullPath));
            client.Send(ResultCode.EndTransmit.ConvertString());
        }


        private string GetList(string currentPath)
        {
            string[] dirs = Directory.GetDirectories(currentPath);
            string[] files = Directory.GetFiles(currentPath);
            string msg = "";
            DateTimeFormatInfo dateTimeFormat = new CultureInfo("en-US", true).DateTimeFormat;
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo fi = new FileInfo(files[i]);
                string name = fi.Name;
                //msg += string.Format("{0:yyyy-MM-dd HH:mm:ss}    {1}    {2}\r\n", fi.LastWriteTimeUtc, fi.Length, name);
                msg += string.Format("-rw-r--r-- 1 root root     {0} {1}  {2} {3:HH:mm} {4}\r\n", fi.Length, dateTimeFormat.GetMonthName(fi.LastWriteTime.Month).Substring(0, 3),
                    fi.LastWriteTime.Day, fi.LastWriteTime, name);
            }

            for (int i = 0; i < dirs.Length; i++)
            {
                DirectoryInfo di = new DirectoryInfo(dirs[i]);
                string name = di.Name;
                //msg += string.Format("{0:yyyy-MM-dd HH:mm:ss}     {1}    {2}\r\n", di.LastWriteTimeUtc, "<DIR>", name);
                msg += string.Format("drw-r--r--    2 0        0            0 {0} {1} {2:HH:mm} {3}\r\n", dateTimeFormat.GetMonthName(di.LastWriteTime.Month).Substring(0, 3),
                  di.LastWriteTime.Day, di.LastWriteTime, name);
            }
            return msg;
        }
    }
}
