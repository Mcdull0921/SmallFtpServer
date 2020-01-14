using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SmallFtpServer
{
    class Client
    {
        public Dictionary<CommandType, Command> commands;
        public UserInfo currentUser;

        RequestHandle requestHandle;
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
            currentUser = new UserInfo();
        }

        public void Handle(string cmd)
        {
            var commandType = (CommandType)Enum.Parse(typeof(CommandType), cmd);
            if (!commands.ContainsKey(commandType))
                throw new Exception(FormatMsg(ResultCode.UnKownCommand));
            var command = commands[commandType];
            command.Process();
        }

        public string FormatMsg(ResultCode code)
        {
            switch (code)
            {
                default:
                    return string.Format("{0} {1}", (int)code, code.GetDescription());
            }
        }
    }
}
