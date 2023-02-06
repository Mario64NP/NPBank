using Controller;
using Microsoft.EntityFrameworkCore;
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

        [TestCase(50)]
        [TestCase(100.15)]
        public void ExecuteTransaction_ReturnsTrue(double amount)
        {
            Currency c1 = new() { Name = "Ctest1", Code = "TEST1" };
            Currency c2 = new() { Name = "Ctest2", Code = "TEST2" };
            ExchangeRate e = new() { FromCurrency = c1, ToCurrency = c2, Rate = 9.14 };
            _coordinator.BankContext.Add(e);
            _coordinator.BankContext.SaveChanges();

            FiscalAccount f1 = new()
            {
                Balance = 100,
                Currency = c1
            };
            FiscalAccount f2 = new()
            {
                Balance = 100,
                Currency = c2
            };
            FiscalAccount f3 = new()
            {
                Balance = 100,
                Currency = c2
            };
            Transaction t1 = new()
            {
                FromAccount = f1,
                ToAccount = f2,
                Amount = amount
            };
            Transaction t2 = new()
            {
                FromAccount = f2,
                ToAccount = f3,
                Amount = amount
            };

            _coordinator.ExecuteTransaction(t1);

            double f2Balance = 100 + amount * _coordinator.BankContext.ExchangeRates.Single(ex => ex.FromCurrency.Equals(f1.Currency) && ex.ToCurrency.Equals(f2.Currency)).Rate;

            Assert.That(f1.Balance, Is.EqualTo(100 - amount));
            Assert.That(f2.Balance, Is.EqualTo(f2Balance));

            _coordinator.ExecuteTransaction(t2);

            Assert.That(f2.Balance, Is.EqualTo(f2Balance - amount));
            Assert.That(f3.Balance, Is.EqualTo(100 + amount));

            _coordinator.BankContext.Remove(e);
            _coordinator.BankContext.Remove(c1);
            _coordinator.BankContext.Remove(c2);
            _coordinator.BankContext.SaveChanges();
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

        [TestCase("Pera", "pera@email.com", "066 200")]
        [TestCase("Laza", "laza@email.com", "066 248")]
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

        [TestCase("Pera", "pera@email.com", "066 359")]
        [TestCase("Laza", "laza@email.com", "066 340")]
        public void AddClient_AlreadyExists_ThrowsException(string name, string email, string number)
        {
            Client c1 = new NaturalEntity() { Name = name, Email = email, PhoneNumber = number };
            _coordinator.AddClient(c1);

            Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateException>(() => _coordinator.AddClient(c1));

            _coordinator.BankContext.Database.ExecuteSqlRaw($"DELETE FROM NaturalEntity WHERE ID = {c1.ID}");
        }

        [TestCase("Pera", "pera@email.com", "066 759")]
        [TestCase("Laza", "laza@email.com", "066 740")]
        public void RemoveClient_ReturnsTrue(string name, string email, string number)
        {
            Client c1 = new NaturalEntity() { Name = name, Email = email, PhoneNumber = number };
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

        [TestCase("Pera", "pera@email.com", "066 859")]
        [TestCase("Laza", "laza@email.com", "066 840")]
        public void RemoveClient_DoesntExist_ThrowsException(string name, string email, string number)
        {
            Client c1 = new NaturalEntity() {ID = 250, Name = name, Email = email, PhoneNumber = number };

            Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException>(() => _coordinator.RemoveClient(c1));
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
            NaturalEntity n = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b = new() { DateCreated = DateTime.MaxValue, Owner = n };

            int numberOfBAccountsBefore = _coordinator.BankContext.BankAccounts.Count();

            _coordinator.AddBAccount(b);

            Assert.That(_coordinator.BankContext.BankAccounts.Count(), Is.EqualTo(numberOfBAccountsBefore + 1));
            Assert.That(_coordinator.BankContext.BankAccounts.Where(b => b.DateCreated == DateTime.MaxValue && b.Owner.Equals(n)).Count(), Is.EqualTo(1));

            _coordinator.BankContext.Remove(b);
            _coordinator.BankContext.Remove(n);
            _coordinator.BankContext.SaveChanges();
        }

        [Test]
        public void AddBAccount_AlreadyExists_ThrowsException()
        {
            NaturalEntity n = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b = new() { DateCreated = DateTime.MaxValue, Owner = n };
            _coordinator.AddBAccount(b);

            Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateException>(() => _coordinator.AddBAccount(b));

            _coordinator.BankContext.Database.ExecuteSqlRaw($"DELETE FROM BankAccounts WHERE ID = {b.ID}");
            _coordinator.BankContext.Remove(n);
            _coordinator.BankContext.SaveChanges();
        }

        [Test]
        public void RemoveBAccount_ReturnsTrue()
        {
            NaturalEntity n = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b = new() { DateCreated = DateTime.MaxValue, Owner = n };

            _coordinator.BankContext.Add(b);
            _coordinator.BankContext.SaveChanges();

            int numberOfBAccountsBefore = _coordinator.BankContext.BankAccounts.Count();

            _coordinator.RemoveBAccount(b);

            Assert.That(_coordinator.BankContext.BankAccounts.Count(), Is.EqualTo(numberOfBAccountsBefore - 1));
            Assert.That(_coordinator.BankContext.BankAccounts.Where(b => b.DateCreated == DateTime.MaxValue && b.Owner.Equals(n)).Count(), Is.EqualTo(0));

            _coordinator.BankContext.Remove(n);
            _coordinator.BankContext.SaveChanges();
        }

        [Test]
        public void RemoveBAccount_DoesntExist_ThrowsException()
        {
            NaturalEntity n = new() { ID = 150, Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b = new() { ID = 160, DateCreated = DateTime.MaxValue, Owner = n };

            Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException>(() => _coordinator.RemoveBAccount(b));
        }

        [Test]
        public void GetAllBAccounts_ReturnsTrue()
        {
            NaturalEntity n1 = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b1 = new() { DateCreated = DateTime.MaxValue, Owner = n1 };
            NaturalEntity n2 = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b2 = new() { DateCreated = DateTime.MaxValue, Owner = n2 };

            int numberOfBAccountsBefore = _coordinator.BankContext.BankAccounts.Count();

            _coordinator.BankContext.Add(b1);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllBAccounts(), Has.Count.EqualTo(numberOfBAccountsBefore + 1));

            _coordinator.BankContext.Add(b2);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllBAccounts(), Has.Count.EqualTo(numberOfBAccountsBefore + 2));

            _coordinator.BankContext.Remove(b2);
            _coordinator.BankContext.Remove(n2);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllBAccounts(), Has.Count.EqualTo(numberOfBAccountsBefore + 1));

            _coordinator.BankContext.Remove(b1);
            _coordinator.BankContext.Remove(n1);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllBAccounts(), Has.Count.EqualTo(numberOfBAccountsBefore));
        }

        [TestCase("123-654", 5.06)]
        [TestCase("456-987", 2.00)]
        public void AddFAccount_ReturnsTrue(string number, double balance)
        {
            NaturalEntity n = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b = new() { DateCreated = DateTime.MaxValue, Owner = n };
            Currency c = new() { Name = "TestCurrency1", Code = "TEST1" };
            FiscalAccount f = new() { Number = number, Balance = balance, Currency = c, BankAccount = b };

            int numberOfFAccountsBefore = _coordinator.BankContext.FiscalAccounts.Count();

            _coordinator.AddFAccount(f);

            Assert.That(_coordinator.BankContext.FiscalAccounts.Count(), Is.EqualTo(numberOfFAccountsBefore + 1));
            Assert.That(_coordinator.BankContext.FiscalAccounts.Where(f => f.Number == number && f.Balance == balance && f.Currency.Equals(c) && f.BankAccount.Equals(b)).Count(), Is.EqualTo(1));

            _coordinator.BankContext.Remove(f);
            _coordinator.BankContext.Remove(c);
            _coordinator.BankContext.Remove(b);
            _coordinator.BankContext.Remove(n);
            _coordinator.BankContext.SaveChanges();
        }

        [Test]
        public void AddFAccount_AlreadyExists_ThrowsException()
        {
            NaturalEntity n = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b = new() { DateCreated = DateTime.MaxValue, Owner = n };
            Currency c = new() { Name = "TestCurrency1", Code = "TEST1" };
            FiscalAccount f = new() { Number = "Atest", Balance = 3, Currency = c, BankAccount = b };
            _coordinator.AddFAccount(f);

            Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateException>(() => _coordinator.AddFAccount(f));

            _coordinator.BankContext.Remove(f);
            _coordinator.BankContext.Remove(c);
            _coordinator.BankContext.SaveChanges();
            _coordinator.BankContext.Remove(b);
            _coordinator.BankContext.Remove(n);
            _coordinator.BankContext.SaveChanges();
        }

        [TestCase("123-654", 5.06)]
        [TestCase("456-987", 2.00)]
        public void RemoveFAccount_ReturnsTrue(string number, double balance)
        {
            NaturalEntity n = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b = new() { DateCreated = DateTime.MaxValue, Owner = n };
            Currency c = new() { Name = "TestCurrency1", Code = "TEST1" };
            FiscalAccount f = new() { Number = number, Balance = balance, Currency = c, BankAccount = b };

            _coordinator.BankContext.Add(f);
            _coordinator.BankContext.SaveChanges();

            int numberOfFAccountsBefore = _coordinator.BankContext.FiscalAccounts.Count();

            _coordinator.RemoveFAccount(f);

            Assert.That(_coordinator.BankContext.FiscalAccounts.Count(), Is.EqualTo(numberOfFAccountsBefore - 1));
            Assert.That(_coordinator.BankContext.FiscalAccounts.Where(f => f.Number == number && f.Balance == balance && f.Currency.Equals(c) && f.BankAccount.Equals(b)).Count(), Is.EqualTo(0));

            _coordinator.BankContext.Remove(c);
            _coordinator.BankContext.Remove(b);
            _coordinator.BankContext.Remove(n);
            _coordinator.BankContext.SaveChanges();
        }

        [Test]
        public void RemoveFAccount_DoesntExist_ThrowsException()
        {
            NaturalEntity n = new() { ID = 5, Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b = new() { ID = 681, DateCreated = DateTime.MaxValue, Owner = n };
            Currency c = new() { ID = 7, Name = "TestCurrency1", Code = "TEST1" };
            FiscalAccount f = new() { ID = -891, Number = "Atest", Balance = 6, Currency = c, BankAccount = b };

            Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException>(() => _coordinator.RemoveFAccount(f));
        }

        [TestCase("123-654", 5.06)]
        [TestCase("456-987", 2.00)]
        public void GetAllFAccounts_ReturnsTrue(string number, double balance)
        {
            NaturalEntity n1 = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b1 = new() { DateCreated = DateTime.MaxValue, Owner = n1 };
            Currency c1 = new() { Name = "TestCurrency1", Code = "TEST1" };
            FiscalAccount f1 = new() { Number = number, Balance = balance, Currency = c1, BankAccount = b1 };
            NaturalEntity n2 = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b2 = new() { DateCreated = DateTime.MaxValue, Owner = n2 };
            Currency c2 = new() { Name = "TestCurrency1", Code = "TEST1" };
            FiscalAccount f2 = new() { Number = number, Balance = balance, Currency = c2, BankAccount = b2 };

            int numberOfFAccountsBefore = _coordinator.BankContext.FiscalAccounts.Count();

            _coordinator.BankContext.Add(f1);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllFAccounts(), Has.Count.EqualTo(numberOfFAccountsBefore + 1));

            _coordinator.BankContext.Add(f2);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllFAccounts(), Has.Count.EqualTo(numberOfFAccountsBefore + 2));
            
            _coordinator.BankContext.Remove(f2);
            _coordinator.BankContext.Remove(c2);
            _coordinator.BankContext.Remove(b2);
            _coordinator.BankContext.Remove(n2);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllFAccounts(), Has.Count.EqualTo(numberOfFAccountsBefore + 1));
            
            _coordinator.BankContext.Remove(f1);
            _coordinator.BankContext.Remove(c1);
            _coordinator.BankContext.Remove(b1);
            _coordinator.BankContext.Remove(n1);
            _coordinator.BankContext.SaveChanges();
            Assert.That(_coordinator.GetAllFAccounts(), Has.Count.EqualTo(numberOfFAccountsBefore));
        }

        [TestCase(5.06)]
        [TestCase(2)]
        public void AddExchangeRate_ReturnsTrue(double rate)
        {
            Currency c1 = new() { Name = "Ctest1", Code = "TEST1"};
            Currency c2 = new() { Name = "Ctest2", Code = "TEST2"};
            ExchangeRate e = new() { FromCurrency = c1, ToCurrency = c2, Rate = rate };

            int numberOfExchangeRatesBefore = _coordinator.BankContext.ExchangeRates.Count();

            _coordinator.AddExchangeRate(e);

            Assert.That(_coordinator.BankContext.ExchangeRates.Count(), Is.EqualTo(numberOfExchangeRatesBefore + 1));
            Assert.That(_coordinator.BankContext.ExchangeRates.Where(e => e.FromCurrency.Equals(c1) && e.ToCurrency.Equals(c2) && e.Rate == rate).Count(), Is.EqualTo(1));

            _coordinator.BankContext.Remove(e);
            _coordinator.BankContext.Remove(c1);
            _coordinator.BankContext.Remove(c2);
            _coordinator.BankContext.SaveChanges();
        }

        [Test]
        public void AddExchangeRate_AlreadyExists_ThrowsException()
        {
            Currency c1 = new() { Name = "Ctest1", Code = "TEST1" };
            Currency c2 = new() { Name = "Ctest2", Code = "TEST2" };
            ExchangeRate e = new() { FromCurrency = c1, ToCurrency = c2, Rate = 7 };
            _coordinator.AddExchangeRate(e);

            Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateException>(() => _coordinator.AddExchangeRate(e));

            _coordinator.BankContext.Database.ExecuteSqlRaw($"DELETE FROM ExchangeRates WHERE FromCurrencyID = {e.FromCurrencyID} AND ToCurrencyID = {e.ToCurrencyID}");
            _coordinator.BankContext.Remove(c1);
            _coordinator.BankContext.Remove(c2);
            _coordinator.BankContext.SaveChanges();
        }

        [TestCase(5.06)]
        [TestCase(2)]
        public void RemoveExchangeRate_ReturnsTrue(double rate)
        {
            Currency c1 = new() { Name = "Ctest1", Code = "TEST1" };
            Currency c2 = new() { Name = "Ctest2", Code = "TEST2" };
            ExchangeRate e = new() { FromCurrency = c1, ToCurrency = c2, Rate = rate };

            _coordinator.BankContext.Add(e);
            _coordinator.BankContext.SaveChanges();

            int numberOfExchangeRatesBefore = _coordinator.BankContext.ExchangeRates.Count();

            _coordinator.RemoveExchangeRate(e);

            Assert.That(_coordinator.BankContext.ExchangeRates.Count(), Is.EqualTo(numberOfExchangeRatesBefore - 1));
            Assert.That(_coordinator.BankContext.ExchangeRates.Where(e => e.FromCurrency.Equals(c1) && e.ToCurrency.Equals(c2) && e.Rate == rate).Count(), Is.EqualTo(0));

            _coordinator.BankContext.Remove(c1);
            _coordinator.BankContext.Remove(c2);
            _coordinator.BankContext.SaveChanges();
        }

        [Test]
        public void RemoveExchangeRate_DoesntExist_ThrowsException()
        {
            Currency c1 = new() { Name = "Ctest1", Code = "TEST1" };
            Currency c2 = new() { Name = "Ctest2", Code = "TEST2" };
            ExchangeRate e = new() { FromCurrencyID = -5, ToCurrencyID = -10, Rate = 4 };

            Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException>(() => _coordinator.RemoveExchangeRate(e));
        }

        [TestCase(5.06)]
        [TestCase(7)]
        public void GetAllExchangeRates_ReturnsTrue(double rate)
        {
            Currency c1 = new() { Name = "Ctest1", Code = "TEST1" };
            Currency c2 = new() { Name = "Ctest2", Code = "TEST2" };
            ExchangeRate e1 = new() { FromCurrency = c1, ToCurrency = c2, Rate = rate };
            Currency c3 = new() { Name = "Ctest1", Code = "TEST1" };
            Currency c4 = new() { Name = "Ctest2", Code = "TEST2" };
            ExchangeRate e2 = new() { FromCurrency = c3, ToCurrency = c4, Rate = rate };

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

            _coordinator.BankContext.Remove(c1);
            _coordinator.BankContext.Remove(c2);
            _coordinator.BankContext.Remove(c3);
            _coordinator.BankContext.Remove(c4);
            _coordinator.BankContext.SaveChanges();
        }

        [TestCase(50)]
        [TestCase(42)]
        public void AddTransaction_ReturnsTrue(double amount)
        {
            NaturalEntity n1 = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b1 = new() { DateCreated = DateTime.MaxValue, Owner = n1 };
            Currency c1 = new() { Name = "TestCurrency1", Code = "TEST1" };
            FiscalAccount f1 = new() { Number = "Atest", Balance = 3, Currency = c1, BankAccount = b1 };
            NaturalEntity n2 = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b2 = new() { DateCreated = DateTime.MaxValue, Owner = n2 };
            Currency c2 = new() { Name = "TestCurrency2", Code = "TEST2" };
            FiscalAccount f2 = new() { Number = "Atest", Balance = 4, Currency = c2, BankAccount = b2 };
            Transaction t = new() { FromAccount = f1, ToAccount = f2, Amount = amount, Timestamp =  DateTime.MaxValue};

            int numberOfTransactionsBefore = _coordinator.BankContext.Transactions.Count();

            _coordinator.AddTransaction(t);

            Assert.That(_coordinator.BankContext.Transactions.Count(), Is.EqualTo(numberOfTransactionsBefore + 1));
            Assert.That(_coordinator.BankContext.Transactions.Where(t => t.FromAccount.Equals(f1) && t.ToAccount.Equals(f2) && t.Amount == amount && t.Timestamp == DateTime.MaxValue).Count(), Is.EqualTo(1));

            _coordinator.BankContext.Remove(f1);
            _coordinator.BankContext.Remove(f2);
            _coordinator.BankContext.Remove(c1);
            _coordinator.BankContext.Remove(c2);
            _coordinator.BankContext.Remove(b1);
            _coordinator.BankContext.Remove(b2);
            _coordinator.BankContext.Remove(n1);
            _coordinator.BankContext.Remove(n2);
            _coordinator.BankContext.SaveChanges();
        }

        [Test]
        public void AddTransaction_AlreadyExists_ThrowsException()
        {
            NaturalEntity n1 = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b1 = new() { DateCreated = DateTime.MaxValue, Owner = n1 };
            Currency c1 = new() { Name = "TestCurrency1", Code = "TEST1" };
            FiscalAccount f1 = new() { Number = "Atest", Balance = 3, Currency = c1, BankAccount = b1 };
            NaturalEntity n2 = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b2 = new() { DateCreated = DateTime.MaxValue, Owner = n2 };
            Currency c2 = new() { Name = "TestCurrency2", Code = "TEST2" };
            FiscalAccount f2 = new() { Number = "Atest", Balance = 4, Currency = c2, BankAccount = b2 };
            Transaction t = new() { FromAccount = f1, ToAccount = f2, Amount = 55, Timestamp = DateTime.MaxValue };
            _coordinator.BankContext.Add(t);
            _coordinator.BankContext.SaveChanges();

            Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateException>(() => _coordinator.AddTransaction(t));

            _coordinator.BankContext.Database.ExecuteSqlRaw($"DELETE FROM Transactions WHERE ID = {t.Id}");
            _coordinator.BankContext.Remove(f1);
            _coordinator.BankContext.Remove(f2);
            _coordinator.BankContext.Remove(c1);
            _coordinator.BankContext.Remove(c2);
            _coordinator.BankContext.Remove(b1);
            _coordinator.BankContext.Remove(b2);
            _coordinator.BankContext.Remove(n1);
            _coordinator.BankContext.Remove(n2);
            _coordinator.BankContext.SaveChanges();
        }

        [TestCase(50)]
        [TestCase(42)]
        public void RemoveTransaction_ReturnsTrue(double amount) 
        {
            NaturalEntity n1 = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b1 = new() { DateCreated = DateTime.MaxValue, Owner = n1 };
            Currency c1 = new() { Name = "TestCurrency1", Code = "TEST1" };
            FiscalAccount f1 = new() { Number = "Atest", Balance = 3, Currency = c1, BankAccount = b1 };
            NaturalEntity n2 = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b2 = new() { DateCreated = DateTime.MaxValue, Owner = n2 };
            Currency c2 = new() { Name = "TestCurrency2", Code = "TEST2" };
            FiscalAccount f2 = new() { Number = "Atest", Balance = 4, Currency = c2, BankAccount = b2 };
            Transaction t = new() { FromAccount = f1, ToAccount = f2, Amount = 55, Timestamp = DateTime.MaxValue };
            _coordinator.BankContext.Add(t);
            _coordinator.BankContext.SaveChanges();

            int numberOfTransactionsBefore = _coordinator.BankContext.Transactions.Count();

            _coordinator.RemoveTransaction(t);
            Assert.That(_coordinator.BankContext.Transactions.Count(), Is.EqualTo(numberOfTransactionsBefore - 1));
            Assert.That(_coordinator.BankContext.Transactions.Where(t => t.FromAccount.Equals(f1) && t.ToAccount.Equals(f2) && t.Amount == amount && t.Timestamp == DateTime.MaxValue).Count(), Is.EqualTo(0));

            _coordinator.BankContext.Remove(f1);
            _coordinator.BankContext.Remove(f2);
            _coordinator.BankContext.Remove(c1);
            _coordinator.BankContext.Remove(c2);
            _coordinator.BankContext.Remove(b1);
            _coordinator.BankContext.Remove(b2);
            _coordinator.BankContext.Remove(n1);
            _coordinator.BankContext.Remove(n2);
            _coordinator.BankContext.SaveChanges();
        }

        [Test]
        public void RemoveTransaction_DoesntExist_ThrowsException()
        {
            NaturalEntity n1 = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b1 = new() { DateCreated = DateTime.MaxValue, Owner = n1 };
            Currency c1 = new() { Name = "TestCurrency1", Code = "TEST1" };
            FiscalAccount f1 = new() { Number = "Atest", Balance = 3, Currency = c1, BankAccount = b1 };
            NaturalEntity n2 = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b2 = new() { DateCreated = DateTime.MaxValue, Owner = n2 };
            Currency c2 = new() { Name = "TestCurrency1", Code = "TEST1" };
            FiscalAccount f2 = new() { Number = "Atest", Balance = 4, Currency = c2, BankAccount = b2 };
            Transaction t = new() { Id = -5, FromAccount = f1, ToAccount = f2, Amount = 9, Timestamp = DateTime.MaxValue };

            Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException>(() => _coordinator.RemoveTransaction(t));
        }

        [TestCase(50, 62.3)]
        [TestCase(42, 14)]
        public void GetAllTransactions_ReturnsTrue(double amount1, double amount2)
        {
            NaturalEntity n1 = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b1 = new() { DateCreated = DateTime.MaxValue, Owner = n1 };
            Currency c1 = new() { Name = "TestCurrency1", Code = "TEST1" };
            FiscalAccount f1 = new() { Number = "Atest", Balance = 3, Currency = c1, BankAccount = b1 };
            NaturalEntity n2 = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b2 = new() { DateCreated = DateTime.MaxValue, Owner = n2 };
            Currency c2 = new() { Name = "TestCurrency2", Code = "TEST2" };
            FiscalAccount f2 = new() { Number = "Atest", Balance = 4, Currency = c2, BankAccount = b2 };
            Transaction t1 = new() { FromAccount = f1, ToAccount = f2, Amount = amount1, Timestamp = DateTime.MaxValue };
            NaturalEntity n3 = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b3 = new() { DateCreated = DateTime.MaxValue, Owner = n3 };
            Currency c3 = new() { Name = "TestCurrency1", Code = "TEST1" };
            FiscalAccount f3 = new() { Number = "Atest", Balance = 3, Currency = c3, BankAccount = b3 };
            NaturalEntity n4 = new() { Name = "Atest", Email = "Btest", PhoneNumber = "Ctest" };
            BankAccount b4 = new() { DateCreated = DateTime.MaxValue, Owner = n4 };
            Currency c4 = new() { Name = "TestCurrency2", Code = "TEST2" };
            FiscalAccount f4 = new() { Number = "Atest", Balance = 4, Currency = c4, BankAccount = b4 };
            Transaction t2 = new() { FromAccount = f3, ToAccount = f4, Amount = amount2, Timestamp = DateTime.MaxValue };

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

            _coordinator.BankContext.Remove(f1);
            _coordinator.BankContext.Remove(f2);
            _coordinator.BankContext.Remove(f3);
            _coordinator.BankContext.Remove(f4);
            _coordinator.BankContext.Remove(c1);
            _coordinator.BankContext.Remove(c2);
            _coordinator.BankContext.Remove(c3);
            _coordinator.BankContext.Remove(c4);
            _coordinator.BankContext.Remove(b1);
            _coordinator.BankContext.Remove(b2);
            _coordinator.BankContext.Remove(b3);
            _coordinator.BankContext.Remove(b4);
            _coordinator.BankContext.Remove(n1);
            _coordinator.BankContext.Remove(n2);
            _coordinator.BankContext.Remove(n3);
            _coordinator.BankContext.Remove(n4);
            _coordinator.BankContext.SaveChanges();
        }

        [Test]
        public void GetAllCurrencies_ReturnsTrue()
        {
            Currency c1 = new() { Name = "Ctest1", Code = "TEST1" };
            Currency c2 = new() { Name = "Ctest2", Code = "TEST2" };

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