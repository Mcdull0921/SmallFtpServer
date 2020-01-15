using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Exceptions
{
    class CloseConnectException : FtpException
    {
        public CloseConnectException() : base(ResultCode.CloseConnect)
        {

        }
    }
}
