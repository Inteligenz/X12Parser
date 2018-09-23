namespace OopFactory.X12.Shared.Models
{
    using System;

    /// <summary>
    /// Move this class in seperate file if being used by other classes.
    /// </summary>
    public class DateTimePeriod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimePeriod"/> class with the specified <see cref="DateTime"/>
        /// </summary>
        /// <param name="date">DateTime to initialize object with</param>
        public DateTimePeriod(DateTime date)
        {
            this.StartDate = date;
            this.IsDateRange = false;
        }

        public bool IsDateRange { get; }

        public DateTime StartDate { get; }

        public DateTime EndDate { get; }

        public DateTimePeriod(DateTime startDate, DateTime endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.IsDateRange = true;
        }
    }
}
