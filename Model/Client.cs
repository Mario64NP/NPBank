namespace Model
{
    /// <summary>
    /// Represents a bank client.
    /// </summary>
    public abstract class Client
    {
        /// <summary>
        /// Gets or sets the ID of the client.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the full name of the client.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the phone number of the client
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Gets or sets the email of the client.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the bank account of the client.
        /// </summary>
        public BankAccount BankAccount { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Client) 
                return false;

            return ((Client)obj).ID == ID;
        }
    }
}