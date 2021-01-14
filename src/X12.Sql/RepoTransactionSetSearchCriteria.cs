namespace X12.Sql
{
    using System;

    /// <summary>
    /// Represents search criteria for a transaction set
    /// </summary>
    public class RepoTransactionSetSearchCriteria
    {
        /// <summary>
        /// Gets or sets the unique identifier for Interchange
        /// </summary>
        public object InterchangeId { get; set; }

        /// <summary>
        /// Gets or sets the sender's identifier
        /// </summary>
        public string SenderId { get; set; }

        /// <summary>
        /// Gets or sets the receiver's identifier
        /// </summary>
        public string ReceiverId { get; set; }

        /// <summary>
        /// Gets or sets the Interchange Control Number
        /// </summary>
        public string InterchangeControlNumber { get; set; }

        /// <summary>
        /// Gets or sets the Interchange minimum date
        /// </summary>
        public DateTime? InterchangeMinDate { get; set; }

        /// <summary>
        /// Gets or sets the Interchange maximum date
        /// </summary>
        public DateTime? InterchangeMaxDate { get; set; }

        /// <summary>
        /// Gets or sets the group's identifier
        /// </summary>
        public object FunctionalGroupId { get; set; }

        /// <summary>
        /// Gets or sets the functional identifier
        /// </summary>
        public string FunctionalIdCode { get; set; }

        /// <summary>
        /// Gets or sets the function group's control number
        /// </summary>
        public string FunctionalGroupControlNumber { get; set; }

        /// <summary>
        /// Gets or sets the pattern for the version
        /// </summary>
        public string VersionPattern { get; set; }

        /// <summary>
        /// Gets or sets the unique Transaction Set identifier
        /// </summary>
        public object TransactionSetId { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Set code
        /// </summary>
        public string TransactionSetCode { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Set control number
        /// </summary>
        public string TransactionSetControlNumber { get; set; }
    }
}