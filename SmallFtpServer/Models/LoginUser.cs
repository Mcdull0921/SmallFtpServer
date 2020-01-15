using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Models
{
    class LoginUser
    {
        public string username { get; set; }
        public string rootdir { get; set; }
        public bool islogin { get; set; }
    }
}
