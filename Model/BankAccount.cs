using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class BankAccount
    {
        public int ID { get; set; }
        [ForeignKey("Client")]
        public Client Owner { get; set; }
        public DateTime DateCreated { get; set; }
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