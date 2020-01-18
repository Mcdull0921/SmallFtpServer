using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
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
        /// 新建文件夹
        /// </summary>
        XMKD,
        /// <summary>
        /// 存储文件
        /// </summary>
        STOR,
        /// <summary>
        /// 获取文件
        /// </summary>
        RETR,
        /// <summary>
        /// 删除文件
        /// </summary>
        DELE,
        /// <summary>
        /// 重命名文件-原始文件名
        /// </summary>
        RNFR,
        /// <summary>
        /// 重命名文件-新的文件名
        /// </summary>
        RNTO,
        /// <summary>
        /// 被动模式，向客户端传输服务端数据传输监听端口
        /// </summary>
        PASV,
        /// <summary>
        /// 主动模式，从客户端接受客户端监听端口，主动和客户端建立连接
        /// </summary>
        PORT,
        /// <summary>
        /// 显示列表
        /// </summary>
        LIST,
        /// <summary>
        /// 跳转目录
        /// </summary>
        CWD,
        /// <summary>
        /// 跳转到上一级目录
        /// </summary>
        CDUP,
        /// <summary>
        /// 退出登录
        /// </summary>
        QUIT,
        /// <summary>
        /// 空指令
        /// </summary>
        NOOP

    }
}
