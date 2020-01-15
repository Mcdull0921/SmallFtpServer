using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Models
{
    class LoginInfo
    {
        public string username { get; set; }
        public string currentDir { get; set; }
        public UserInfo user { get; set; }
        public string rootDir
        {
            get
            {
                return user?.rootdirectory;
            }
        }
    }
}
