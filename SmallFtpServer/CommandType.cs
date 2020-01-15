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
        SYST,
        /// <summary>
        /// 传输编码
        /// </summary>
        OPTS,
        /// <summary>
        /// 当前目录
        /// </summary>
        XPWD,
        /// <summary>
        /// 退出登录
        /// </summary>
        QUIT

    }
}
