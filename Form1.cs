using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;

namespace FtpServer
{
    public partial class Form1 : Form
    {
        FtpServer server;
        public Form1()
        {
            InitializeComponent();
            CustomSettings settings = ConfigurationManager.GetSection("customSettings") as CustomSettings;
            server = new FtpServer(Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["Port"]), 2, settings.userCollection.values);
            server.clientChange += server_clientChange;
            GetLocalIP();
        }

        private void GetLocalIP()
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

        void server_clientChange(FtpClient client)
        {
            this.Invoke(new Action(() =>
            {
                listView1.Items.Clear();
                foreach (var c in server.clients)
                {
                    ListViewItem item = new ListViewItem(c.IP.ToString());
                    item.SubItems.Add(c.user.username);
                    item.SubItems.Add(c.user.isLogin ? "是" : "否");
                    listView1.Items.Add(item);
                }
            }));
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (server.Start())
            {
                this.lbStatus.Text = string.Format("服务已启动：{0}:{1}", FtpServer.LocalIP, FtpServer.Port);
                this.btnStop.Enabled = true;
                this.btnStart.Enabled = false;
            }
            else
            {
                MessageBox.Show("服务启动失败！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (server.Stop())
            {
                this.btnStop.Enabled = false;
                this.btnStart.Enabled = true;
                this.lbStatus.Text = string.Format("服务已关闭");
                listView1.Items.Clear();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.Stop();
        }



    }
}
