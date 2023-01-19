using Controller;
using Model;

namespace NPBank.UnitTests
{
    public class CoordinatorTests
    {
        private Coordinator _coordinator;

        [SetUp]
        public void Setup()
        {
            _coordinator = new();
        }

        [TearDown]
        public void TearDown()
        {
            _coordinator = null;
        }

        [TestCase(50, 2, 3)]
        [TestCase(100, 2, 2)]
        public void ExecuteTransaction_ReturnsTrue(int amount, int currency1, int currency2)
        {
            FiscalAccount f1 = new()
            {
                Balance = 100,
                Currency = new Currency() { ID = currency1 }
            };
            FiscalAccount f2 = new()
            {
                Balance = 100,
                Currency = new Currency() { ID = currency2 }
            };
            Transaction t = new()
            {
                FromAccount = f1,
                ToAccount = f2,
                Amount = amount
            };

            _coordinator.ExecuteTransaction(t);

            double f2Balance = 100;
            if (f1.Currency.Equals(f2.Currency))
                f2Balance += t.Amount;
            else
                f2Balance += amount * _coordinator.BankContext.ExchangeRates.Single(ex => ex.FromCurrency.Equals(f1.Currency) && ex.ToCurrency.Equals(f2.Currency)).Rate;

            Assert.That(f1.Balance, Is.EqualTo(100 - amount));
            Assert.That(f2.Balance, Is.EqualTo(f2Balance));
        }

        [TestCase("Insert API key here", "RSD", "EUR")]
        [TestCase("Insert API key here", "EUR", "USD")]
        public void GetRateOnline_ReturnsTrue(string key, string ccFrom, string ccTo)
        {
            double rate = Coordinator.GetRateOnline(key, ccFrom, ccTo);

            Assert.IsTrue(rate > 0);
        }

        [TestCase("wrong api key", "RSD", "EUR")]
        [TestCase("Inser API key here", "EUR", "LLL")]
        public void GetRateOnline_ReturnsFalse(string key, string ccFrom, string ccTo)
        {
            double rate = -1;
            try
            {
                rate = Coordinator.GetRateOnline(key, ccFrom, ccTo);
            }
            catch (Exception)
            {}

            Assert.IsFalse(rate > 0);
        }
    }
}