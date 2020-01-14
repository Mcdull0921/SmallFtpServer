using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer
{
    enum RequestType
    {
        /// <summary>
        /// 发送登录名
        /// </summary>
        LOGIN_USER,
        /// <summary>
        /// 发送登录密码
        /// </summary>
        LOGIN_PASS,
        /// <summary>
        /// 请求系统类型
        /// </summary>
        SYSTEM,
        RESTART,
        /// <summary>
        /// 请求获得文件
        /// </summary>
        RETRIEVE,
        /// <summary>
        /// 存储文件
        /// </summary>
        STORE,
        /// <summary>
        /// 要重命名的文件
        /// </summary>
        RENAME_FROM,
        /// <summary>
        /// 重命名为新文件
        /// </summary>
        RENAME_TO,
        ABORT,
        /// <summary>
        /// 删除文件
        /// </summary>
        DELETE,
        /// <summary>
        /// 创建目录
        /// </summary>
        XMKD,
        /// <summary>
        /// 显示当前工作目录
        /// </summary>
        PWD,
        /// <summary>
        /// 请求获得目录信息
        /// </summary>
        LIST,
        /// <summary>
        /// 等待(NOOP)  此命令不产生什么实际动作，它仅使服务器返回OK。
        /// </summary>
        NOOP,
        /// <summary>
        /// 表示类型
        /// </summary>
        REPRESENTATION_TYPE,
        /// <summary>
        /// 退出登录
        /// </summary>
        LOGOUT,
        /// <summary>
        /// 客户端告知服务器端口
        /// </summary>
        DATA_PORT,
        /// <summary>
        /// 采用PASV传输方法（理解为客户端不发送PORT命令，即服务器不能确定客户端口）
        /// </summary>
        PASSIVE,
        /// <summary>
        /// 改变工作目录
        /// </summary>
        CWD,
        /// <summary>
        /// 返回上级目录
        /// </summary>
        CDUP,
        /// <summary>
        /// 设置传输编码
        /// </summary>
        OPTS,
        CHANGE_DIR_UP,
        UNKNOWN_CMD,
        PORT,
        PASV,
        ERROR
    }
}
