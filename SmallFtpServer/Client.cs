using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SmallFtpServer
{
    class Client
    {
        public UserInfo currentUser;

        Dictionary<CommandType, Command> commands;
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
            currentUser = new UserInfo();
        }

        public void Handle(string cmd)
        {
            object obj;
            if (!Enum.TryParse(typeof(CommandType), cmd, true, out obj))
                throw new Exception(Command.FormatMsg(ResultCode.UnKownCommand));
            CommandType commandType = (CommandType)obj;
            if (!commands.ContainsKey(commandType))
                throw new Exception(Command.FormatMsg(ResultCode.UnKownCommand));
            var command = commands[commandType];
            command.Process();
        }
    }
}
