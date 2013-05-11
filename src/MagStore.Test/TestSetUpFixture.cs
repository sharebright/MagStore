using NUnit.Framework;

namespace MagStore.Test
{
    public class TestSetUpFixture
    {
        [SetUp]
        protected void TestSetUp()
        {
            Arrange();
            Act();
        }

        protected virtual void Arrange()
        {
            
        }

        protected virtual void Act()
        {
            
        }
    }
}