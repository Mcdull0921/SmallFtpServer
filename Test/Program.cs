using SmallFtpServer;
using SmallFtpServer.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {

        static void Main(string[] args)
        {
            FtpServer ftpServer = new FtpServer(pasv_port: 5379, users: new UserInfo { username = "test", password = "123", rootdirectory = @"D:\taiji" });
            ftpServer.Listen();


            Console.ReadKey();
        }

    }
}
