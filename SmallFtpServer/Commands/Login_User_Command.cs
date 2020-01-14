using System;
using System.Collections.Generic;
using System.Text;

namespace SmallFtpServer.Commands
{
    class Login_User_Command : Command
    {
        public Login_User_Command(Client client) : base(client)
        {

        }

        public override CommandType CommandType => CommandType.LOGIN_USER;

        public override void Process(params string[] objs)
        {
            throw new NotImplementedException();
        }
    }
}
