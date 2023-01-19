using Model;

namespace NPBank.UnitTests
{
    public abstract class ClientTests
    {
        protected Client c, d;

        [SetUp]
        public abstract void Setup();

        [TearDown]
        public void TearDown()
        {
            c = null;
        }

        [TestCase("Pera","pera@mail.com","069 123")]
        [TestCase("Mika","mika@mail.com","069 135")]
        [TestCase("Laza","laza@mail.com","069 159")]
        public void IsValidClient_IsValid_ReturnsTrue(string name, string email, string phonenumber)
        {
            c.Name = name;
            c.Email = email;
            c.PhoneNumber = phonenumber;

            bool IsValid = Client.IsValidClient(c);

            Assert.That(IsValid, Is.True);
        }

        [TestCase(null, "mika@mail.com", "069 135")]
        [TestCase("Laza", null, "069 159")]
        [TestCase("Pera", "pera@mail.com", null)]
        [TestCase(null, null, "069 135")]
        [TestCase(null, "laza@mail.com", null)]
        [TestCase("Pera", null, null)]
        [TestCase(null, null, null)]

        public void IsValidClient_NotValid_ReturnsFalse(string name, string email, string phonenumber)
        {
            c.Name = name;
            c.Email = email;
            c.PhoneNumber = phonenumber;

            bool IsValid = Client.IsValidClient(c);

            Assert.That(IsValid, Is.False);
        }

        [TestCase("Pera", "pera@mail.com", "069 123")]
        [TestCase("Mika", "mika@mail.com", "069 135")]
        [TestCase("Laza", "laza@mail.com", "069 159")]
        public void ToString_ReturnsTrue(string name, string email, string phonenumber)
        {
            c.Name = name;
            c.Email = email;
            c.PhoneNumber = phonenumber;

            Assert.That(name, Is.EqualTo(c.ToString()));
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(155)]
        public void Equals_IsEqual_ReturnsTrue(int id)
        {
            c.ID = id;
            d.ID = id;

            Assert.That(c, Is.EqualTo(d));
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(155)]
        public void Equals_NotEqual_ReturnsFalse(int id)
        {
            c.ID = id;
            d.ID = id + 5;

            Assert.That(c, Is.Not.EqualTo(d));
        }
    }
}