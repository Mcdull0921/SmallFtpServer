using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer
{
    [AttributeUsage(AttributeTargets.Class)]
    class ArgumentAttribute : Attribute
    {
        public int ArgsNumber { get; private set; }
        public ArgumentAttribute(int args_num)
        {
            ArgsNumber = args_num;
        }
    }
}
