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

        /// <summary>
        /// Checks if all the properties of the transaction have valid values.
        /// </summary>
        /// <param name="t">The transaction</param>
        /// <returns><c>true</c> if all the properties are valid; otherwise, <c>false</c></returns>
        public static bool IsValidTransaction(Transaction t)
        {
            return t is not null && !t.FromAccount.Equals(t.ToAccount) && t.Amount > 0 && t.Timestamp < DateTime.Now;
        }
    }
}