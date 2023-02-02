using Controller;
using Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        [TestCase(50, 3, 1)]
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
            { }

            Assert.IsFalse(rate > 0);
        }

        [TestCase("Pera", "pera@email.com", "066 159")]
        [TestCase("Laza", "laza@email.com", "066 240")]
        public void AddClient_ReturnsTrue(string name, string email, string number)
        {
            Client c1 = new NaturalEntity() { Name = name, Email = email, PhoneNumber = number, };
            Client c2 = new LegalEntity() { Name = name, Email = email, PhoneNumber = number, Owner = "test" };

            int numberOfClientsBefore = _coordinator.BankContext.Clients.Count();

            _coordinator.AddClient(c1);
            _coordinator.AddClient(c2);

            Assert.That(_coordinator.BankContext.Clients.Count(), Is.EqualTo(numberOfClientsBefore + 2));
            Assert.That(_coordinator.BankContext.Clients.Where(c => c.Name == name && c.Email == email && c.PhoneNumber == number).Count(), Is.EqualTo(2));

            _coordinator.BankContext.Remove(c1);
            _coordinator.BankContext.Remove(c2);
            _coordinator.BankContext.SaveChanges();
        }

        [TestCase("Pera", "pera@email.com", "066 159")]
        [TestCase("Laza", "laza@email.com", "066 240")]
        public void RemoveClient_ReturnsTrue(string name, string email, string number)
        {
            Client c1 = new NaturalEntity() { Name = name, Email = email, PhoneNumber = number, };
            Client c2 = new LegalEntity() { Name = name, Email = email, PhoneNumber = number, Owner = "test" };

            _coordinator.BankContext.Add(c1);
            _coordinator.BankContext.Add(c2);
            _coordinator.BankContext.SaveChanges();

            int numberOfClientsBefore = _coordinator.BankContext.Clients.Count();

            _coordinator.RemoveClient(c1);
            _coordinator.RemoveClient(c2);

            Assert.That(_coordinator.BankContext.Clients.Count(), Is.EqualTo(numberOfClientsBefore - 2));
            Assert.That(_coordinator.BankContext.Clients.Where(c => c.Name == name && c.Email == email && c.PhoneNumber == number).Count(), Is.EqualTo(0));
        }

        [TestCase("Pera", "pera@email.com", "066 159")]
        [TestCase("Laza", "laza@email.com", "066 240")]
        public void GetAllClients_ReturnsTrue(string name, string email, string number)
        {
            Client c1 = new NaturalEntity() { Name = name, Email = email, PhoneNumber = number, };
            Client c2 = new LegalEntity() { Name = name, Email = email, PhoneNumber = number, Owner = "test" };

            int numberOfClientsBefore = _coordinator.BankContext.Clients.Count();

            _coordinator.BankContext.Add(c1);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllClients(), Has.Count.EqualTo(numberOfClientsBefore + 1));

            _coordinator.BankContext.Add(c2);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllClients(), Has.Count.EqualTo(numberOfClientsBefore + 2));

            _coordinator.BankContext.Remove(c2);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllClients(), Has.Count.EqualTo(numberOfClientsBefore + 1));

            _coordinator.BankContext.Remove(c1);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllClients(), Has.Count.EqualTo(numberOfClientsBefore));
        }

        [Test]
        public void AddBAccount_ReturnsTrue()
        {
            BankAccount b = new() { DateCreated = DateTime.MaxValue, Owner = _coordinator.GetAllClients()[0] };

            int numberOfBAccountsBefore = _coordinator.BankContext.BankAccounts.Count();

            _coordinator.AddBAccount(b);

            Assert.That(_coordinator.BankContext.BankAccounts.Count(), Is.EqualTo(numberOfBAccountsBefore + 1));
            Assert.That(_coordinator.BankContext.BankAccounts.Where(b => b.DateCreated == DateTime.MaxValue && b.Owner == _coordinator.GetAllClients()[0]).Count(), Is.EqualTo(1));

            _coordinator.BankContext.Remove(b);
            _coordinator.BankContext.SaveChanges();
        }

        [Test]
        public void RemoveBAccount_ReturnsTrue()
        {
            BankAccount b = new() { DateCreated = DateTime.MaxValue, Owner = _coordinator.GetAllClients()[0] };

            _coordinator.BankContext.Add(b);
            _coordinator.BankContext.SaveChanges();

            int numberOfBAccountsBefore = _coordinator.BankContext.BankAccounts.Count();

            _coordinator.RemoveBAccount(b);

            Assert.That(_coordinator.BankContext.BankAccounts.Count(), Is.EqualTo(numberOfBAccountsBefore - 1));
            Assert.That(_coordinator.BankContext.BankAccounts.Where(b => b.DateCreated == DateTime.MaxValue && b.Owner == _coordinator.GetAllClients()[0]).Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetAllBAccounts_ReturnsTrue()
        {
            BankAccount b1 = new() { DateCreated = DateTime.MaxValue, Owner = _coordinator.GetAllClients()[0] };
            BankAccount b2 = new() { DateCreated = DateTime.MaxValue, Owner = _coordinator.GetAllClients()[1] };

            int numberOfBAccountsBefore = _coordinator.BankContext.BankAccounts.Count();

            _coordinator.BankContext.Add(b1);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllBAccounts(), Has.Count.EqualTo(numberOfBAccountsBefore + 1));

            _coordinator.BankContext.Add(b2);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllBAccounts(), Has.Count.EqualTo(numberOfBAccountsBefore + 2));

            _coordinator.BankContext.Remove(b2);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllBAccounts(), Has.Count.EqualTo(numberOfBAccountsBefore + 1));

            _coordinator.BankContext.Remove(b1);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllBAccounts(), Has.Count.EqualTo(numberOfBAccountsBefore));
        }

        [TestCase("123-654", 5.06)]
        [TestCase("456-987", 2.00)]
        public void AddFAccount_ReturnsTrue(string number, double balance)
        {
            FiscalAccount f = new() { Number = number, Balance = balance, Currency = _coordinator.GetAllCurrencies()[0], BankAccount = _coordinator.GetAllBAccounts()[0] };

            int numberOfFAccountsBefore = _coordinator.BankContext.FiscalAccounts.Count();

            _coordinator.AddFAccount(f);

            Assert.That(_coordinator.BankContext.FiscalAccounts.Count(), Is.EqualTo(numberOfFAccountsBefore + 1));
            Assert.That(_coordinator.BankContext.FiscalAccounts.Where(f => f.Number == number && f.Balance == balance && f.Currency.Equals(_coordinator.GetAllCurrencies()[0]) && f.BankAccount.Equals(_coordinator.GetAllBAccounts()[0])).Count(), Is.EqualTo(1));

            _coordinator.BankContext.Remove(f);
            _coordinator.BankContext.SaveChanges();
        }

        [TestCase("123-654", 5.06)]
        [TestCase("456-987", 2.00)]
        public void RemoveFAccount_ReturnsTrue(string number, double balance)
        {
            FiscalAccount f = new() { Number = number, Balance = balance, Currency = _coordinator.GetAllCurrencies()[0], BankAccount = _coordinator.GetAllBAccounts()[0] };

            _coordinator.BankContext.Add(f);
            _coordinator.BankContext.SaveChanges();

            int numberOfFAccountsBefore = _coordinator.BankContext.FiscalAccounts.Count();

            _coordinator.RemoveFAccount(f);

            Assert.That(_coordinator.BankContext.FiscalAccounts.Count(), Is.EqualTo(numberOfFAccountsBefore - 1));
            Assert.That(_coordinator.BankContext.FiscalAccounts.Where(f => f.Number == number && f.Balance == balance && f.Currency.Equals(_coordinator.GetAllCurrencies()[0]) && f.BankAccount.Equals(_coordinator.GetAllBAccounts()[0])).Count(), Is.EqualTo(0));
        }

        [TestCase("123-654", 5.06)]
        [TestCase("456-987", 2.00)]
        public void GetAllFAccounts_ReturnsTrue(string number, double balance)
        {
            FiscalAccount f1 = new() { Number = number, Balance = balance, Currency = _coordinator.GetAllCurrencies()[0], BankAccount = _coordinator.GetAllBAccounts()[0] };
            FiscalAccount f2 = new() { Number = number, Balance = balance, Currency = _coordinator.GetAllCurrencies()[1], BankAccount = _coordinator.GetAllBAccounts()[1] };

            int numberOfFAccountsBefore = _coordinator.BankContext.FiscalAccounts.Count();

            _coordinator.BankContext.Add(f1);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllFAccounts(), Has.Count.EqualTo(numberOfFAccountsBefore + 1));

            _coordinator.BankContext.Add(f2);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllFAccounts(), Has.Count.EqualTo(numberOfFAccountsBefore + 2));
            
            _coordinator.BankContext.Remove(f2);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllFAccounts(), Has.Count.EqualTo(numberOfFAccountsBefore + 1));
            
            _coordinator.BankContext.Remove(f1);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllFAccounts(), Has.Count.EqualTo(numberOfFAccountsBefore));
        }

        [TestCase(1, 2, 5.06)]
        [TestCase(3, 2, 2.00)]
        public void AddExchangeRate_ReturnsTrue(int from, int to, double rate)
        {
            ExchangeRate e = new() { FromCurrency = _coordinator.GetAllCurrencies().Single(c => c.ID == from), ToCurrency = _coordinator.GetAllCurrencies().Single(c => c.ID == to), Rate = rate };

            int numberOfExchangeRatesBefore = _coordinator.BankContext.ExchangeRates.Count();

            _coordinator.AddExchangeRate(e);

            Assert.That(_coordinator.BankContext.ExchangeRates.Count(), Is.EqualTo(numberOfExchangeRatesBefore + 1));
            Assert.That(_coordinator.BankContext.ExchangeRates.Where(e => e.FromCurrencyID == from && e.ToCurrencyID == to && e.Rate == rate).Count(), Is.EqualTo(1));

            _coordinator.BankContext.Remove(e);
            _coordinator.BankContext.SaveChanges();
        }

        [TestCase(1, 2, 5.06)]
        [TestCase(3, 2, 2.00)]
        public void RemoveExchangeRate_ReturnsTrue(int from, int to, double rate)
        {
            ExchangeRate e = new() { FromCurrency = _coordinator.BankContext.Currencies.Single(c => c.ID == from), ToCurrency = _coordinator.BankContext.Currencies.Single(c => c.ID == to), Rate = rate };

            _coordinator.BankContext.Add(e);
            _coordinator.BankContext.SaveChanges();

            int numberOfExchangeRatesBefore = _coordinator.BankContext.ExchangeRates.Count();

            _coordinator.RemoveExchangeRate(e);

            Assert.That(_coordinator.BankContext.ExchangeRates.Count(), Is.EqualTo(numberOfExchangeRatesBefore - 1));
            Assert.That(_coordinator.BankContext.ExchangeRates.Where(e => e.FromCurrencyID == from && e.ToCurrencyID == to && e.Rate == rate).Count(), Is.EqualTo(0));
        }

        [TestCase(2, 1, 5.06)]
        [TestCase(1, 2, 2.00)]
        public void GetAllExchangeRates_ReturnsTrue(int from, int to, double rate)
        {
            ExchangeRate e1 = new() { FromCurrency = _coordinator.BankContext.Currencies.Single(c => c.ID == from), ToCurrency = _coordinator.BankContext.Currencies.Single(c => c.ID == to), Rate = rate };
            ExchangeRate e2 = new() { FromCurrency = _coordinator.BankContext.Currencies.Single(c => c.ID == from + 1), ToCurrency = _coordinator.BankContext.Currencies.Single(c => c.ID == to + 1), Rate = rate };

            int numberOfExchangeRatesBefore = _coordinator.BankContext.ExchangeRates.Count();

            _coordinator.BankContext.Add(e1);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllExchangeRates(), Has.Count.EqualTo(numberOfExchangeRatesBefore + 1));

            _coordinator.BankContext.Add(e2);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllExchangeRates(), Has.Count.EqualTo(numberOfExchangeRatesBefore + 2));

            _coordinator.BankContext.Remove(e2);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllExchangeRates(), Has.Count.EqualTo(numberOfExchangeRatesBefore + 1));

            _coordinator.BankContext.Remove(e1);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllExchangeRates(), Has.Count.EqualTo(numberOfExchangeRatesBefore));
        }

        [TestCase(50)]
        [TestCase(42)]
        public void AddTransaction_ReturnsTrue(double amount)
        {
            Transaction t = new() { FromAccount = _coordinator.GetAllFAccounts()[0], ToAccount = _coordinator.GetAllFAccounts()[1], Amount = amount, Timestamp =  DateTime.MaxValue};

            int numberOfTransactionsBefore = _coordinator.BankContext.Transactions.Count();

            _coordinator.AddTransaction(t);

            Assert.That(_coordinator.BankContext.Transactions.Count(), Is.EqualTo(numberOfTransactionsBefore + 1));
            Assert.That(_coordinator.BankContext.Transactions.Where(t => t.FromAccount.Equals(_coordinator.GetAllFAccounts()[0]) && t.ToAccount.Equals(_coordinator.GetAllFAccounts()[1]) && t.Amount == amount && t.Timestamp == DateTime.MaxValue).Count(), Is.EqualTo(1));

            _coordinator.BankContext.Remove(t);
            _coordinator.BankContext.SaveChanges();
        }

        [TestCase(50)]
        [TestCase(42)]
        public void RemoveTransaction_ReturnsTrue(double amount) 
        {
            Transaction t = new() { FromAccount = _coordinator.GetAllFAccounts()[0], ToAccount = _coordinator.GetAllFAccounts()[1], Amount = amount, Timestamp = DateTime.MaxValue };

            _coordinator.BankContext.Add(t);
            _coordinator.BankContext.SaveChanges();

            int numberOfTransactionsBefore = _coordinator.BankContext.Transactions.Count();

            _coordinator.RemoveTransaction(t);
            Assert.That(_coordinator.BankContext.Transactions.Count(), Is.EqualTo(numberOfTransactionsBefore - 1));
            Assert.That(_coordinator.BankContext.Transactions.Where(t => t.FromAccount.Equals(_coordinator.GetAllFAccounts()[0]) && t.ToAccount.Equals(_coordinator.GetAllFAccounts()[1]) && t.Amount == amount && t.Timestamp == DateTime.MaxValue).Count(), Is.EqualTo(0));
        }

        [TestCase(50)]
        [TestCase(42)]
        public void GetAllTransactions_ReturnsTrue(double amount)
        {
            Transaction t1 = new() { FromAccount = _coordinator.GetAllFAccounts()[0], ToAccount = _coordinator.GetAllFAccounts()[1], Amount = amount, Timestamp = DateTime.MaxValue };
            Transaction t2 = new() { FromAccount = _coordinator.GetAllFAccounts()[0], ToAccount = _coordinator.GetAllFAccounts()[1], Amount = amount+50, Timestamp = DateTime.MaxValue };

            int numberOfTransactionsBefore = _coordinator.BankContext.Transactions.Count();

            _coordinator.BankContext.Add(t1);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllTransactions(), Has.Count.EqualTo(numberOfTransactionsBefore + 1));
            
            _coordinator.BankContext.Add(t2);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllTransactions(), Has.Count.EqualTo(numberOfTransactionsBefore + 2));

            _coordinator.BankContext.Remove(t2);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllTransactions(), Has.Count.EqualTo(numberOfTransactionsBefore + 1));

            _coordinator.BankContext.Remove(t1);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllTransactions(), Has.Count.EqualTo(numberOfTransactionsBefore));
        }

        [Test]
        public void GetAllCurrencies_ReturnsTrue()
        {
            Currency c1 = new() { Name = "asdf", Code = "ABC" };
            Currency c2 = new() { Name = "fdsa", Code = "XYZ" };

            int numberOfCurrenciesBefore = _coordinator.BankContext.Currencies.Count();

            _coordinator.BankContext.Add(c1);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllCurrencies(), Has.Count.EqualTo(numberOfCurrenciesBefore + 1));

            _coordinator.BankContext.Add(c2);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllCurrencies(), Has.Count.EqualTo(numberOfCurrenciesBefore + 2));

            _coordinator.BankContext.Remove(c2);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllCurrencies(), Has.Count.EqualTo(numberOfCurrenciesBefore + 1));

            _coordinator.BankContext.Remove(c1);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllCurrencies(), Has.Count.EqualTo(numberOfCurrenciesBefore));
        }
    }
}