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

        /// <summary>
        /// Checks if all the properties of the client have valid values.
        /// </summary>
        /// <param name="c">The client</param>
        /// <returns><c>true</c> if all the properties are valid; otherwise, <c>false</c></returns>
        public static bool IsValidClient(Client c)
        {
            return c is not null && !string.IsNullOrEmpty(c.Name) && !string.IsNullOrEmpty(c.Email) && !string.IsNullOrEmpty(c.PhoneNumber);
        }

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