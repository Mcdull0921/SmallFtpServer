using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SmallFtpServer
{
    public class FtpServer
    {
        List<Client> clients;
        IEnumerable<UserInfo> users;
        TcpListener listener;
        TcpListener pasvListener;
        Thread mainThread;
        int maxConnect;

        public FtpServer(int port = 21, int max_connect = 100, string pasv_IP = "", int pasv_port = 5379, params UserInfo[] users)
        {
            this.users = users;
            maxConnect = max_connect;
            clients = new List<Client>();
            listener = new TcpListener(IPAddress.Any, port);
            pasvListener = new TcpListener(string.IsNullOrEmpty(pasv_IP) ? IPAddress.Any : IPAddress.Parse(pasv_IP), pasv_port);
        }

        public bool IsListening
        {
            get; private set;
        }

        public bool Listen()
        {
            if (!IsListening)
            {
                pasvListener.Start();
                mainThread = new Thread(() =>
                {
                    while (IsListening)
                    {
                        if (clients.Count < maxConnect)
                        {
                            try
                            {
                                listener.Start();
                                Socket socket = listener.AcceptSocket();
                                Client client = new Client(socket, users, pasvListener);
                                client.OnClose += Client_OnClose;
                                clients.Add(client);
                                client.Start();
                            }
                            catch (Exception ex)
                            {
                                IsListening = false;
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
                IsListening = true;
                return true;
            }
            return false;
        }

        private void Client_OnClose(Client obj)
        {
            clients.Remove(obj);
        }

        public void Close()
        {
            if (IsListening)
            {
                IsListening = false;
                listener.Stop();
                pasvListener.Stop();
                foreach (var c in clients)
                    c.Dispose();
                clients.Clear();
            }
        }
    }
}
