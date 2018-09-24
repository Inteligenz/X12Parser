namespace X12.Shared.Extensions
{
    using System;
    using System.Linq;

    using X12.Shared.Attributes;
    using X12.Shared.Properties;

    /// <summary>
    /// Represents a collection of extensions for Enumerations
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the <see cref="EdiFieldValueAttribute"/> from the referenced Enum value
        /// </summary>
        /// <param name="enumValue">Value to get EdiFieldValue from</param>
        /// <returns>EdiFieldValueAttribute parsed from Enum</returns>
        /// <exception cref="InvalidOperationException">Thrown if Enum cannot be parsed</exception>
        public static string EdiFieldValue(this Enum enumValue)
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

        /// <summary>
        /// Parses the EdiFieldValue to its equivalent Enum
        /// </summary>
        /// <typeparam name="T">Enum value to parse to</typeparam>
        /// <param name="itemValue">EdiFieldValue to be parsed</param>
        /// <returns>Enum representation of value</returns>
        /// <exception cref="InvalidOperationException">Thrown if the value is not valid</exception>
        public static T ToEnumFromEdiFieldValue<T>(this string itemValue)
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
