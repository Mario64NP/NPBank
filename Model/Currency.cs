namespace Model
{
    /// <summary>
    /// Represents a currency.
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// Gets or sets the ID of the currency.
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the full name of the currency.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the 3-character ISO 4217 code of the currency.
        /// </summary>
        public string Code { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj is not Currency) 
                return false;

            return ((Currency)obj).ID == ID;
        }
    }
}