using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer
{
    class UserInfo
    {
        public bool isLogin { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string rootDir { get; set; }
    }
}
