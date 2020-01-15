using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Exceptions
{
    abstract class FtpException : Exception
    {
        public ResultCode code { get; private set; }
        private string[] @params;
        public FtpException(ResultCode code, params string[] @params)
        {
            this.code = code;
            this.@params = @params;
        }

        public override string Message
        {
            get
            {
                return code.ConvertString(@params);
            }
        }
    }
}
