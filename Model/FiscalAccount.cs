namespace Model
{
    public class FiscalAccount
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public Currency Currency { get; set; }
        public decimal Balance { get; set; }
    }
}