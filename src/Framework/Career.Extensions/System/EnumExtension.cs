using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Career.Extensions.System
{
    public static class EnumExtension
    {
        public static string GetDescription<T>(this T e) where T : Enum, IConvertible
        {
            string description = null;

            Type type = e.GetType();
            Array values = Enum.GetValues(type);

            foreach (int val in values)
            {
                if (val == e.ToInt32(CultureInfo.InvariantCulture))
                {
                    MemberInfo[] memInfo = type.GetMember(type.GetEnumName(val) ?? throw new InvalidOperationException());
                    object[] descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if (descriptionAttributes.Length > 0)
                    {
                        description = ((DescriptionAttribute)descriptionAttributes[0]).Description;
                    }

                    break;
                }
            }

            return description;
        }
    }
}
