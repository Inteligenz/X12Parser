namespace OopFactory.X12.Shared.Extensions
{
    using System;
    using System.Linq;

    using OopFactory.Shared.Properties;
    using OopFactory.X12.Shared.Attributes;

    /// <summary>
    /// Represents a collection of extensions for Enumerations
    /// </summary>
    public static class EnumExtensions
    {
        public static string EDIFieldValue(this Enum enumValue)
        {
            var attributes = (EdiFieldValueAttribute[])enumValue
                .GetType()
                .GetField(enumValue.ToString())
                .GetCustomAttributes(typeof(EdiFieldValueAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Value;
            }

            throw new InvalidOperationException(string.Format(Resources.EDIValueNotFound, enumValue));
        }

        public static T ToEnumFromEDIFieldValue<T>(this string itemValue)
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }

            foreach (var field in from field in type.GetFields()
                                  let attributes = (EdiFieldValueAttribute[])field.GetCustomAttributes(typeof(EdiFieldValueAttribute), false)
                                  where attributes.Length > 0 && attributes[0].Value == itemValue
                                  select field)
            {
                return (T)field.GetValue(null);
            }

            throw new InvalidOperationException(string.Format(Resources.EDIFieldNotFound, itemValue));
        }
    }
}
