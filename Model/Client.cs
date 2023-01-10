namespace Model
{
    public abstract class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public BankAccount BankAccount { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}