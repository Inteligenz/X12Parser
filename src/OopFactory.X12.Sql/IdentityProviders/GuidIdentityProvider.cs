namespace OopFactory.X12.Sql.IdentityProviders
{
    using System;

    using OopFactory.X12.Sql.Interfaces;

    /// <summary>
    /// Represents an Identity provider for guid-based identifiers
    /// </summary>
    public class GuidIdentityProvider : IIdentityProvider
    {
        /// <summary>
        /// Validates the provider's schema and ensures a table exists
        /// </summary>
        /// <exception cref="NotImplementedException">Thrown on call</exception>
        public void EnsureSchema()
        {
            throw new NotImplementedException("'EnsureSchema' in not needed with GuidIdentityProvider");
        }

        /// <summary>
        /// Obtains the next identifier and returns it
        /// </summary>
        /// <param name="schema">Schema of database to retrieve next id from</param>
        /// <param name="table">Table to get next id from</param>
        /// <returns>Next id obtained from database</returns>
        public object NextId(string schema, string table)
        {
            /*
			   * Could also use the built in Win32 function, but this will work equally as well and doesn't do any locking
			   * Sequential guids are more performant while reading than non sequential guids
			   */

            var guidArray = Guid.NewGuid().ToByteArray();

            var baseDate = new DateTime(1900, 1, 1);
            var now = DateTime.Now;

            // Get the days and milliseconds which will be used to build the byte string 
            var days = new TimeSpan(now.Ticks - baseDate.Ticks);
            var msecs = now.TimeOfDay;

            // Convert to a byte array 
            // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
            var daysArray = BitConverter.GetBytes(days.Days);
            var msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            // Reverse the bytes to match SQL Servers ordering 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }
    }
}
