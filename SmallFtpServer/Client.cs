using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SmallFtpServer
{
    class Client
    {
        Dictionary<CommandType, Command> commands;
        RequestHandle requestHandle;
        UserInfo currentUser;
        public Client()
        {
            commands = new Dictionary<CommandType, Command>();
            foreach (var t in Assembly.GetExecutingAssembly().GetTypes())
            {
                if (t.IsClass && !t.IsAbstract && typeof(Command).IsAssignableFrom(t))
                {
                    var command = (Command)Activator.CreateInstance(t, this);
                    commands.Add(command.CommandType, command);
                }
            }
            requestHandle = new RequestHandle(this);
        }
    }
}
