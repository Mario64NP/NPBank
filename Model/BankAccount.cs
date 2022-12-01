using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class BankAccount
    {
        public int ID { get; set; }
        public Client Owner { get; set; }
        public DateTime DateCreated { get; set; }
        public List<FiscalAccount> FiscalAccounts { get; set; }
    }
}