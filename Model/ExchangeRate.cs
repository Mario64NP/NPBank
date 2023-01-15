namespace Model
{
    /// <summary>
    /// Represents the exchange rate from one currency to another.
    /// </summary>
    public class ExchangeRate
    {
        /// <summary>
        /// Gets or sets the ID of the source currency.
        /// </summary>
        public int FromCurrencyID { get; set; }
        /// <summary>
        /// Gets or sets the source currency.
        /// </summary>
        public Currency FromCurrency { get; set; }
        /// <summary>
        /// Gets or sets the ID of the destination currency.
        /// </summary>
        public int ToCurrencyID { get; set; }
        /// <summary>
        /// Gets or sets the destination currency.
        /// </summary>
        public Currency ToCurrency { get; set; }
        /// <summary>
        /// Gets or sets the rate at which the source currency is converted into the destination currency.
        /// </summary>
        public double Rate { get; set; }
    }
}