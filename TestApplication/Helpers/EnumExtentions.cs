using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace TestApplication.Helpers
{
    public static class EnumExtentions
    {
        public static string GetDescription<T>(this T value) where T : struct
        {
            Type type = value.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("value must be of Enum type", "value");
            }

            MemberInfo[] memberInfo = type.GetMember(value.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            return value.ToString();
        }
    }
}