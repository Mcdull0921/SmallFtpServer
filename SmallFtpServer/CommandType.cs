using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer
{
    enum CommandType
    {
        /// <summary>
        /// 发送用户名
        /// </summary>
        USER,
        /// <summary>
        /// 发送密码
        /// </summary>
        PASS,
        /// <summary>
        /// 服务器系统信息
        /// </summary>
        SYST
       
    }
}
