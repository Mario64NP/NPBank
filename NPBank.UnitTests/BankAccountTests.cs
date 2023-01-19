using Model;

namespace NPBank.UnitTests
{
    public class BankAccountTests
    {
        private BankAccount b;

        [SetUp]
        public void Setup()
        {
            b = new BankAccount();
        }

        [TearDown]
        public void TearDown()
        {
            b = null;
        }

        [TestCase("1.1.2012.")]
        [TestCase("10.22.2022.")]
        public void IsValidBankAccount_IsValid_ReturnsTrue(DateTime date)
        {
            b.DateCreated = date;
            
            bool IsValid = BankAccount.IsValidBankAccount(b);

            Assert.IsTrue(IsValid);
        }

        [TestCase("2.4.2035.")]
        [TestCase("12.12.2023.")]
        public void IsValidBankAccount_NotValid_ReturnsFalse(DateTime date)
        {
            b.DateCreated = date;
            
            bool IsValid = BankAccount.IsValidBankAccount(b);
            
            Assert.IsFalse(IsValid);
        }

        [TestCase("Pera", 1)]
        [TestCase("Mika", 2)]
        [TestCase("Laza", 15)]
        public void ToString_ReturnsTrue(string name, int id)
        {
            b.Owner = new NaturalEntity() { Name = name};
            b.ID = id;

            Assert.AreEqual(b.ToString(), $"{name}'s account (ID: {id})");
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(155)]
        public void Equals_IsEqual_ReturnsTrue(int id) 
        {
            b.ID = id;
            BankAccount c = new BankAccount();
            c.ID = id;

            Assert.IsTrue(b.Equals(c));
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(155)]
        public void Equals_NotEqual_ReturnsFalse(int id) 
        {
            b.ID = id;
            BankAccount c = new BankAccount();
            c.ID = id+5;

            Assert.IsFalse(b.Equals(c));
        }
    }
}