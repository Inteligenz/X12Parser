namespace OopFactory.X12.Hipaa.Claims.Services
{
    using System.Text;

    /// <summary>
    /// Locally used extension methods
    /// </summary>
    public static class LocalExtensions
    {
        /// <summary>
        /// Repeats the provided value as many times indicated by count
        /// </summary>
        /// <param name="value">Character to be repeated</param>
        /// <param name="count">Number of times to repeat the value</param>
        /// <returns>String of repeated value</returns>
        public static string Repeat(this char value, int count)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                builder.Append(value);
            }

            return builder.ToString();
        }
    }
}
