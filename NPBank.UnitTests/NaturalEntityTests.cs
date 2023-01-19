using Model;

namespace NPBank.UnitTests
{
    public class NaturalEntityTests : ClientTests
    {
        public override void Setup()
        {
            c = new NaturalEntity();
            d = new NaturalEntity();
        }
    }
}