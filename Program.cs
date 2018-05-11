using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Configuration;
using System.Net;
using System.Net.Sockets;

namespace FtpServer
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            CustomSettings settings = ConfigurationManager.GetSection("customSettings") as CustomSettings;
            var server = new FtpServer(Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Port"]), 2, settings.userCollection.values);
            server.clientChange += server_clientChange;
            GetLocalIP();
            server.Start();
            Console.WriteLine("服务已启动!");
            while (true) ;
        }

        static void GetLocalIP()
        {
            Console.WriteLine("本地ip4:");
            IPHostEntry iphe = Dns.GetHostEntry(Dns.GetHostName());
            for (int i = 0; i < iphe.AddressList.Length; i++)
            {
                if (iphe.AddressList[i].AddressFamily.ToString() != ProtocolFamily.InterNetworkV6.ToString())
                {
                    Console.WriteLine(iphe.AddressList[i].ToString());
                }
            }
        }

        static void server_clientChange(FtpServer.ClientArgs c)
        {
            if (c.status == 1)
                Console.WriteLine("【{0}】用户{1}登录成功", c.ip, c.username);
            else if (c.status == 2)
                Console.WriteLine("【{0}】用户{1}退出登录", c.ip, c.username);
        }
    }
}
