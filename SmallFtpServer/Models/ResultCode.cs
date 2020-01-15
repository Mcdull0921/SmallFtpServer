using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SmallFtpServer.Models
{
    enum ResultCode
    {
        [Description("Ready")]
        Ready = 220,
        [Description("语法错误，未被认可的指令")]
        UnKownCommand = 500
    }
}
