using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SmallFtpServer.Models
{
    class LoginInfo
    {
        public string username { get; set; }
        public DirectoryInfo currentDir { get; private set; }
        public DirectoryInfo rootDir { get; private set; }
        public bool islogin { get; private set; }

        public void Login(UserInfo user)
        {
            rootDir = new DirectoryInfo(user.rootdirectory);
            currentDir = new DirectoryInfo(user.rootdirectory);
            islogin = true;
        }

        public void LoginOut()
        {
            username = null;
            islogin = false;
        }
    }
}
