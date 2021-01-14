namespace X12.Sql
{
    using System;

    /// <summary>
    /// Collection of common <see cref="Type"/> extensions
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Creates a default instance if value type
        /// </summary>
        /// <param name="t">Object to create instance of</param>
        /// <returns>New instance if value type; otherwise, null</returns>
        public static object GetDefaultValue(this Type t)
        {
            return t.IsValueType ? Activator.CreateInstance(t) : null;
        }
    }
}