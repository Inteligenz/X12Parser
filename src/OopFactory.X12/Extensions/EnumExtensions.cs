using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OopFactory.X12.Attributes;
using OopFactory.X12.Parsing.Model.Typed;

namespace OopFactory.X12.Extensions
{
    public static class EnumExtensions
    {
        public static string EDIFieldValue(this Enum enumValue)
        {
            var attributes = (EDIFieldValueAttribute[])enumValue.GetType().GetField(enumValue.ToString()).GetCustomAttributes(typeof(EDIFieldValueAttribute), false);
            if (attributes.Length > 0)
                return attributes[0].Value;

            throw new InvalidOperationException("No EDIValue Attribute defined for " + enumValue);
        }

        public static string EDIFieldValue<T>(this IEnumerable<T> enumValue, char repetitionSeparator)
        {
            var tt = typeof(T);
            var at = typeof(EDIFieldValueAttribute);
            var sb = new StringBuilder();
            enumValue.ToList().ForEach(t =>
                {
                    var val = tt.GetField(t.ToString()).GetCustomAttributes(at, false).FirstOrDefault();
                    if (null != val)
                    {
                        sb.AppendFormat("{0}{1}", ((EDIFieldValueAttribute)val).Value, repetitionSeparator);
                    }
                });
            if (0 < sb.Length)
                sb = sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static string EDIFieldValueSafe(this Enum enumValue)
        {
            if (null == enumValue)
                return null;
            var attributes = (EDIFieldValueAttribute[])enumValue.GetType().GetField(enumValue.ToString()).GetCustomAttributes(typeof(EDIFieldValueAttribute), false);
            if (attributes.Length > 0)
                return attributes[0].Value;

            return null;
        }

        public static T ToEnumFromEDIFieldValue<T>(this string itemValue) where T : struct, IConvertible
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new InvalidOperationException();

            foreach (var field in from field in type.GetFields()
                                  let attributes = (EDIFieldValueAttribute[])field.GetCustomAttributes(typeof(EDIFieldValueAttribute), false)
                                  where attributes.Length > 0 && attributes[0].Value == itemValue
                                  select field)
            {
                return (T)field.GetValue(null);
            }

            throw new InvalidOperationException("No EDI Field Value found for " + itemValue);
        }

        public static T? ToEnumFromEDIFieldValueSafe<T>(this string itemValue) where T : struct
        {
            if (string.IsNullOrWhiteSpace(itemValue))
                return new T?();
            var type = typeof(T);
            if (!type.IsEnum)
                throw new InvalidOperationException();

            foreach (var field in from field in type.GetFields()
                                  let attributes = (EDIFieldValueAttribute[])field.GetCustomAttributes(typeof(EDIFieldValueAttribute), false)
                                  where attributes.Length > 0 && attributes[0].Value == itemValue
                                  select field)
            {
                return (T)field.GetValue(null);
            }
            return new T?();
        }

        public static IEnumerable<T> ToMultiEnumFromEDIFieldValueSafe<T>(this string itemValue, char SubElementSeparator) where T : struct
        {
            if (string.IsNullOrWhiteSpace(itemValue))
                return new T[] { };
            var type = typeof(T);
            if (!type.IsEnum)
                throw new InvalidOperationException();
            var items = itemValue.Split(SubElementSeparator).ToList();
            return type.GetFields().Where(t =>
            {
                var attrib = (EDIFieldValueAttribute[])t.GetCustomAttributes(typeof(EDIFieldValueAttribute), false);
                return attrib.Length > 0 && items.Contains(attrib.First().Value);
            }).Select(t => (T)t.GetValue(null));
        }
    }
}
