using SmallFtpServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace SmallFtpServer
{
    static class Extention
    {
        public static string GetDescription(this Enum value)
        {
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            if (name == null)
                return null;
            FieldInfo field = type.GetField(name);
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? name : attribute.Description;
        }

        public static string ConvertString(this ResultCode code, params string[] @params)
        {
            string description = string.Format(code.GetDescription(), @params);
            return string.Format("{0} {1}", (int)code, description);
        }
    }
}
