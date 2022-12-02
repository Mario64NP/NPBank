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
    }
}