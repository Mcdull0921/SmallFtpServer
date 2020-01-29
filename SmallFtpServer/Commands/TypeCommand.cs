using SmallFtpServer.Exceptions;
using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    [FtpCommand("TYPE", 1)]
    class TypeCommand : Command
    {
        public TypeCommand(Client client) : base(client)
        {

        }
        public override void Process(params string[] args)
        {
            client.LoginInfo.transferMode = FtpTransferMode.Parse(args[0]);
            if (client.LoginInfo.transferMode.FileType == FtpFileType.Ascii)
            {
                client.Send(ResultCode.Success.ConvertString("ASCII transfer mode active."));
            }
            else if (client.LoginInfo.transferMode.IsBinary)
            {
                client.Send(ResultCode.Success.ConvertString("Binary transfer mode active."));
            }
            else
            {
                throw new ArgumentErrorException();
            }
        }
    }

}
