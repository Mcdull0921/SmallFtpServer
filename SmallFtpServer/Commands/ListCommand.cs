using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace SmallFtpServer.Commands
{
    /// <summary>
    /// 显示列表
    /// </summary>
    [FtpCommand("LIST", needLogin: true)]
    class ListCommand : Command
    {
        public ListCommand(Client client) : base(client)
        {

        }

        public override void Process(params string[] args)
        {
            try
            {
                client.Send(ResultCode.BeginTransmit.ConvertString());
                var datas = GetList(client.LoginInfo.CurrentFullPath, true);
                client.SendDatas(datas.Length == 0 ? " \0" : datas);
                client.Send(ResultCode.EndTransmit.ConvertString());
            }
            catch (InvalidFileException ex)
            {
                throw new InvalidFileException(client.LoginInfo.CurrentVirtualPath);
            }
        }

        protected string GetList(string path, bool containFiles)
        {
            try
            {
                DateTimeFormatInfo dateTimeFormat = new CultureInfo("en-US", true).DateTimeFormat;
                if (Directory.Exists(path))
                {
                    return GetDirList(path, containFiles, dateTimeFormat);
                }
                if (File.Exists(path))
                {
                    return GetFileInfo(path, dateTimeFormat);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new InvalidFileException(ex.Message);
            }
            throw new InvalidFileException(path);

        }

        private string GetDirList(string dirPath, bool containFiles, DateTimeFormatInfo dateTimeFormat)
        {
            string res = "";
            if (containFiles)
            {
                string[] files = Directory.GetFiles(dirPath);
                for (int i = 0; i < files.Length; i++)
                {
                    res += GetFileInfo(files[i], dateTimeFormat);
                }
            }
            string[] dirs = Directory.GetDirectories(dirPath);
            for (int i = 0; i < dirs.Length; i++)
            {
                DirectoryInfo di = new DirectoryInfo(dirs[i]);
                string name = di.Name;
                //res += string.Format("{0:yyyy-MM-dd HH:mm:ss}     {1}    {2}\r\n", di.LastWriteTimeUtc, "<DIR>", name);
                res += string.Format("drw-r--r--    2 0        0            0 {0} {1} {2:HH:mm} {3}\r\n", dateTimeFormat.GetMonthName(di.LastWriteTime.Month).Substring(0, 3),
                  di.LastWriteTime.Day, di.LastWriteTime, name);
            }
            return res;
        }

        private string GetFileInfo(string filePath, DateTimeFormatInfo dateTimeFormat)
        {
            FileInfo fi = new FileInfo(filePath);
            string name = fi.Name;
            //return string.Format("{0:yyyy-MM-dd HH:mm:ss}    {1}    {2}\r\n", fi.LastWriteTimeUtc, fi.Length, name);
            return string.Format("-rw-r--r-- 1 root root     {0} {1}  {2} {3:HH:mm} {4}\r\n", fi.Length, dateTimeFormat.GetMonthName(fi.LastWriteTime.Month).Substring(0, 3), fi.LastWriteTime.Day, fi.LastWriteTime, name);
        }
    }
}
