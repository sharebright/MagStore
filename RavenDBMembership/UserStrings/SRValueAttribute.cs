using System;

namespace MagStore.UserStrings
{
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Enum)]
    public class SrValueAttribute : Attribute
    {
        public string Value;

        public SrValueAttribute(string Value)
        {
            this.Value = Value;
        }
    }
}
