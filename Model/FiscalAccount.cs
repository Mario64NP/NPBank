namespace Model
{
    /// <summary>
    /// Represents a fiscal account.
    /// </summary>
    public class FiscalAccount
    {
        /// <summary>
        /// Gets or sets the ID of the fiscal account.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the account number of the fiscal account.
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// Gets or sets the currency of the fiscal account.
        /// </summary>
        public Currency Currency { get; set; }
        /// <summary>
        /// Gets or sets the balance of the fiscal account.
        /// </summary>
        public double Balance { get; set; }
        /// <summary>
        /// Gets or sets the bank account to which the fiscal account belongs.
        /// </summary>
        public BankAccount BankAccount { get; set; }

        public override string ToString()
        {
            return Number;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not FiscalAccount) 
                return false;

            return ((FiscalAccount)obj).ID == ID;
        }
    }
}