namespace Model
{
    public class FiscalAccount
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public Currency Currency { get; set; }
        public double Balance { get; set; }
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