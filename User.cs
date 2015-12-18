//作者：Mcdull
//说明：FTP账号类
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace FtpServer
{
    sealed class User
    {
        public bool isLogin { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string workingDir { get; set; }
    }

    sealed class CustomSettings : ConfigurationSection
    {
        [ConfigurationProperty("users", IsRequired = true)]
        public UserCollection userCollection
        {
            get { return (UserCollection)base["users"]; }
        }
    }

    sealed class UserCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new UserElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            UserElement u = (UserElement)element;
            return u.username;
        }
        private List<UserElement> list;
        public IEnumerable<UserElement> values
        {
            get
            {
                if (list == null)
                {
                    list = new List<UserElement>();
                    foreach (UserElement e in this)
                    {
                        list.Add(e);
                    }
                }
                return list;
            }
        }
    }

    sealed class UserElement : ConfigurationElement
    {
        [ConfigurationProperty("username", IsRequired = true)]
        public string username
        {
            get { return (string)base["username"]; }
        }
        [ConfigurationProperty("password", IsRequired = true)]
        public string password
        {
            get { return (string)base["password"]; }
        }
        [ConfigurationProperty("rootDir", IsRequired = true)]
        public string rootDir
        {
            get { return (string)base["rootDir"]; }
        }
    }
}
