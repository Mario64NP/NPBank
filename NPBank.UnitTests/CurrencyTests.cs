using Model;

namespace NPBank.UnitTests
{
    public class CurrencyTests
    {
        private Currency c;

        [SetUp]
        public void Setup()
        {
            c = new();
        }

        [TearDown]
        public void Teardown()
        {
            c = null;
        }

        [TestCase("RSD")]
        [TestCase("EUR")]
        [TestCase("USD")]
        public void ToString_ReturnsTrue(string name)
        {
            c.Name = name;

            Assert.AreEqual(c.ToString(), name);
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(155)]
        public void Equals_IsEqual_ReturnsTrue(int id)
        {
            c.ID = id;
            Currency d = new();
            d.ID = id;

            Assert.IsTrue(c.Equals(d));
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(155)]
        public void Equals_NotEqual_ReturnsFalse(int id)
        {
            c.ID = id;
            Currency d = new();
            d.ID = id + 5;

            Assert.IsFalse(c.Equals(d));
        }
    }
}