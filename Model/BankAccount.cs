using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    /// <summary>
    /// Represents a user account in the bank's system. 
    /// One bank account can have multiple fiscal accounts associated with it.
    /// </summary>
    public class BankAccount
    {
        /// <summary>
        /// Gets or sets the ID of the bank account.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the client who is the owner of the bank account.
        /// </summary>
        [ForeignKey("Client")]
        public Client Owner { get; set; }
        /// <summary>
        /// Gets or sets the date and time when the bank account was created.
        /// </summary>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// Gets or sets the list of fiscal accounts associated with the bank account.
        /// </summary>
        public List<FiscalAccount> FiscalAccounts { get; set; }

        public override string ToString()
        {
            return Owner.Name + $"'s account (ID: {ID})";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not BankAccount) 
                return false;

            return ((BankAccount)obj).ID == ID;
        }
    }
}