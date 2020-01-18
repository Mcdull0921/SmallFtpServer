using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Exceptions
{
    class CannotTransmitException : FtpException
    {
        public CannotTransmitException() : base(ResultCode.CantTransmit)
        {

        }
    }
}
