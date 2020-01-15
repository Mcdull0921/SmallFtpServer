using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Models
{
    public class UserInfo
    {
        public string username { get; set; }
        public string password { get; set; }
        public string dir { get; set; }
    }
}
