using System;

namespace SmallFtpServer
{
    public class FtpServer
    {
        Client client = new Client();
        public void Test()
        {
            client.Handle("LOGIN_USER");
        }
    }
}
