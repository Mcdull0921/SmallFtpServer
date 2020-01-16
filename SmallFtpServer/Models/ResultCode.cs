using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SmallFtpServer.Models
{
    enum ResultCode
    {
        [Description("开始数据传输连接")]
        BeginTransmit = 150,
        [Description("关闭数据连接，文件传输终止")]
        EndTransmit = 226,
        [Description("Entering Passive Mode ({0})")]
        PasvMode = 227,
        [Description("Success:{0}")]
        Success = 200,
        [Description("Welcome")]
        Ready = 220,
        [Description("语法错误，未被认可的指令")]
        UnKownCommand = 500,
        [Description("服务关闭控制连接")]
        CloseConnect = 221,
        [Description("系统信息:{0}")]
        SystemInfo = 215,
        [Description("当前目录:{0}")]
        PrintWorkDirectory = 257,
        [Description("参数错误")]
        ArgumentError = 504,
        [Description("已输入用户名，需要口令")]
        NeedPassword = 331,
        [Description("需要登录账号")]
        NeedAccount = 332,
        [Description("登录成功")]
        Login = 230,
        [Description("账号或密码错误，无法登录")]
        UnLogin = 530,
        [Description("文件无效:{0}")]
        InvalidFile = 550
    }
}
