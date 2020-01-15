using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Exceptions
{
    class NeedLoginException : FtpException
    {
        public NeedLoginException() : base(Models.ResultCode.NeedAccount)
        {

        }
    }
}
