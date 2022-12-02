namespace Model
{
    public class ExchangeRate
    {
        public int FromCurrencyID { get; set; }
        public Currency FromCurrency { get; set; }
        public int ToCurrencyID { get; set; }
        public Currency ToCurrency { get; set; }
        public double Rate { get; set; }
    }
}