using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    class UserCommand : Command
    {
        public UserCommand(Client client) : base(client)
        {

        }

        public override CommandType CommandType => CommandType.USER;

        public override void Process(params string[] objs)
        {
            if (objs.Length < 1)
                throw new ArgumentException("参数错误！");
            //client.currentUser.username = objs[0];
        }
    }
}
