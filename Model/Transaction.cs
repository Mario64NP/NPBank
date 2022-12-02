namespace Model
{
    public class Transaction
    {
        public int Id { get; set; }
        public FiscalAccount FromAccount { get; set; }
        public FiscalAccount ToAccount { get; set; }
        public double Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}