using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Exceptions
{
    class ArgumentErrorException : FtpException
    {
        public ArgumentErrorException() : base(Models.ResultCode.ArgumentError)
        {

        }
    }
}
