using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Exceptions
{
    class InvalidFileException : FtpException
    {
        public InvalidFileException(string message) : base(ResultCode.InvalidFile, message)
        {

        }
    }
}
