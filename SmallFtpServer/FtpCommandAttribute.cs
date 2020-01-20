using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    class FtpCommandAttribute : Attribute
    {
        /// <summary>
        /// ftp命令名
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 参数个数
        /// </summary>
        public int ArgsNumber { get; private set; }
        /// <summary>
        /// 是否需要登录
        /// </summary>
        public bool NeedLogin { get; private set; }

        public FtpCommandAttribute(string name, int argsNumber = 0, bool needLogin = false)
        {
            Name = name;
            ArgsNumber = argsNumber;
            NeedLogin = needLogin;
        }
    }
}
