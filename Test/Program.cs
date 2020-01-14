using SmallFtpServer;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            FtpServer ftpServer = new FtpServer();
            ftpServer.Test();

            Console.ReadKey();
        }
    }
}
