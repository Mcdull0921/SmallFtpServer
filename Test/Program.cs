using SmallFtpServer;
using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            FtpServer client = new FtpServer();

            Console.ReadKey();
        }
    }
}
