using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ExchangeRate
    {
        public Currency FromCurrency { get; set; }
        public Currency ToCurrency { get; set; }
        public double Rate { get; set; }
    }
}