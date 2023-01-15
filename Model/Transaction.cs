namespace Model
{
    /// <summary>
    /// Represents a financial transaction between two fiscal accounts.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Gets or sets the ID of the transaction.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the source fiscal account.
        /// </summary>
        public FiscalAccount FromAccount { get; set; }
        /// <summary>
        /// Gets or sets the destination fiscal account.
        /// </summary>
        public FiscalAccount ToAccount { get; set; }
        /// <summary>
        /// Gets or sets the amount of the transaction.
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// Gets or sets the time and date when the transaction occured.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}