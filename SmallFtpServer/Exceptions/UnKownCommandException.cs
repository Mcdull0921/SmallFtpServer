using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Exceptions
{
    class UnKownCommandException : FtpException
    {
        public UnKownCommandException() : base(Models.ResultCode.UnKownCommand)
        {

        }
    }
}
