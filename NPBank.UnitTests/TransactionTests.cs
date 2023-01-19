using Model;

namespace NPBank.UnitTests
{
    public class TransactionTests
    {
        private Transaction t;

        [SetUp]
        public void Setup()
        {
            t = new();
        }

        [TearDown]
        public void Teardown()
        {
            t = null;
        }

        [TestCase(1, 2, 5, "1.1.2020.")]
        [TestCase(3, 5, 7.25, "11.12.2022.")]
        [TestCase(1, 2, 5, "1.6.1990.")]
        public void IsValidTransaction_IsValid_ReturnsTrue(int id1, int id2, double amount, DateTime date)
        {
            t.FromAccount = new();
            t.ToAccount = new();
            t.FromAccount.ID = id1;
            t.ToAccount.ID = id2;
            t.Amount = amount;
            t.Timestamp = date;

            Assert.IsTrue(Transaction.IsValidTransaction(t));
        }

        [TestCase(1, 1, 5, "1.1.2020.")]
        [TestCase(1, 2, 0, "1.1.2020.")]
        [TestCase(3, 5, -5.7, "1.1.2020.")]
        [TestCase(7, 8, 5.9, "1.1.2029.")]
        public void IsValidTransaction_NotValid_ReturnsFalse(int id1, int id2, double amount, DateTime date)
        {
            t.FromAccount = new();
            t.ToAccount = new();
            t.FromAccount.ID = id1;
            t.ToAccount.ID = id2;
            t.Amount = amount;
            t.Timestamp = date;

            Assert.IsFalse(Transaction.IsValidTransaction(t));
        }
    }
}