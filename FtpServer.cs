//作者：Mcdull
//说明：FTP服务端类，负责监听端口并建立连接。
//此处单独封装一个类是为了和UI层分离，使得UI仅需通过捕获事件获取通知而无须关心逻辑
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace FtpServer
{
    class FtpServer
    {
        private TcpListener listener;
        private bool isStart;
        private Thread mainThread;
        private int maxConnect;

        public static IPAddress LocalIP { get; private set; }
        public static int Port { get; private set; }
        public static IEnumerable<UserElement> Users { get; private set; }

        public FtpServer(int port, int max_connect, IEnumerable<UserElement> users)
        {
            clients = new List<FtpClient>();
            // listener = new TcpListener(ip, port);
            listener = new TcpListener(IPAddress.Any, port);
            maxConnect = max_connect;
            // LocalIP = ip;
            LocalIP = IPAddress.Any;
            Port = port;
            Users = users;
        }

        public List<FtpClient> clients;
        public event Action<ClientArgs> clientChange;
        public class ClientArgs
        {
            public string ip { get; set; }
            public string username { get; set; }
            /// <summary>
            /// 访问状态：0访问 1登录成功 2退出登录
            /// </summary>
            public int status { get; set; }
        }

        public bool Start()
        {
            if (!isStart)
            {
                mainThread = new Thread(p =>
                {
                    while (isStart)
                    {
                        if (clients.Count < maxConnect)
                        {
                            try
                            {
                                listener.Start();
                                Socket socket = listener.AcceptSocket();
                                FtpClient client = new FtpClient(socket);
                                client.Quit += client_Quit;
                                client.Login += client_Login;
                                clients.Add(client);
                                onClientChange(new ClientArgs() { ip = client.IP.ToString(), username = client.user.username, status = 0 });
                                client.Start();
                            }
                            catch
                            {
                                isStart = false;
                                return;
                            }
                        }
                        else
                        {
                            listener.Stop();
                        }
                        Thread.Sleep(2000);
                    }
                });
                mainThread.IsBackground = true;   //设置为后台线程，进程关闭后线程强制关闭（前台线程则会导致线程结束后进程才能结束）
                mainThread.Start();
                isStart = true;
                return true;
            }
            return false;
        }

        public bool Stop()
        {
            if (isStart)
            {
                isStart = false;
                listener.Stop();
                mainThread.Abort();
                foreach (var c in clients)
                    c.Stop();
                clients.Clear();
                return true;
            }
            return false;
        }

        void client_Login(FtpClient client)
        {
            onClientChange(new ClientArgs() { ip = client.IP.ToString(), username = client.user.username, status = 1 });
        }

        void client_Quit(FtpClient client)
        {
            var a = new ClientArgs() { ip = client.IP.ToString(), username = client.user.username, status = 2 };
            clients.Remove(client);
            onClientChange(a);
        }

        private void onClientChange(ClientArgs args)
        {
            var temp = clientChange;
            if (temp != null)
                temp(args);
        }
    }
}
