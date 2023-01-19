using Model;

namespace NPBank.UnitTests
{
    public class ExchangeRateTests
    {
        private ExchangeRate ex;

        [SetUp]
        public void Setup()
        {
            ex = new();
        }

        [TearDown]
        public void TearDown()
        {
            ex = null;
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(0.5)]
        [TestCase(150.7)]
        public void IsValidExchangeRate_IsValid_ReturnsTrue(double rate)
        {
            ex.Rate = rate;

            Assert.IsTrue(ExchangeRate.IsValidExchangeRate(ex));
        }

        [TestCase(0)]
        [TestCase(-5)]
        [TestCase(-0.5)]
        [TestCase(-150.7)]
        public void IsValidExchangeRate_NotValid_ReturnsFalse(double rate)
        {
            ex.Rate = rate;

            Assert.IsFalse(ExchangeRate.IsValidExchangeRate(ex));
        }
    }
}