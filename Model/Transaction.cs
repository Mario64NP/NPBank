using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Transaction
    {
        public FiscalAccount FromAccount { get; set; }
        public FiscalAccount ToAccount { get; set; }
        public double Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}