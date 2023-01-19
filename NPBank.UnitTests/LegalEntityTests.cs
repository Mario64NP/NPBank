using Model;

namespace NPBank.UnitTests
{
    public class LegalEntityTests : ClientTests
    {
        public override void Setup()
        {
            c = new LegalEntity();
            d = new LegalEntity();
        }
    }
}