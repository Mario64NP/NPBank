using Model;

namespace NPBank.UnitTests
{
    public class FiscalAccountTests
    {
        private FiscalAccount f;

        [SetUp]
        public void Setup()
        {
            f = new FiscalAccount();
        }

        [TearDown] public void Teardown()
        {
            f = null;
        }

        [TestCase("840-159-40", 5)]
        [TestCase("840-789-50", 50)]
        [TestCase("840-120-22", 100)]
        public void IsValidFiscalAccount_IsValid_ReturnsTrue(string number, int balance)
        {
            f.Number = number;
            f.Balance = balance;

            bool IsValid = FiscalAccount.IsValidFiscalAccount(f);

            Assert.IsTrue(IsValid);
        }

        [TestCase("840-159-40", -5)]
        [TestCase("84078950", 50)]
        [TestCase("84012022", -100)]
        public void IsValidFiscalAccount_NotValid_ReturnsFalse(string number, int balance)
        {
            f.Number = number;
            f.Balance = balance;

            bool IsValid = FiscalAccount.IsValidFiscalAccount(f);

            Assert.IsFalse(IsValid);
        }

        [TestCase("840-159-40")]
        [TestCase("840-789-50")]
        [TestCase("840-120-22")]
        public void ToString_ReturnsTrue(string number)
        {
            f.Number = number;

            Assert.AreEqual(f.ToString(), number);
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(155)]
        public void Equals_IsEqual_ReturnsTrue(int id)
        {
            f.ID = id;
            FiscalAccount c = new();
            c.ID = id;

            Assert.IsTrue(f.Equals(c));
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(155)]
        public void Equals_NotEqual_ReturnsFalse(int id)
        {
            f.ID = id;
            FiscalAccount c = new();
            c.ID = id + 5;

            Assert.IsFalse(f.Equals(c));
        }
    }
}