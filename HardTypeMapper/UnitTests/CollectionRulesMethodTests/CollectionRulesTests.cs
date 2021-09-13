using Interfaces;
using Xunit;
using HardTypeMapper;

namespace UnitTests.CollectionRulesMethodTests
{
    public class CollectionRulesTests
    {
        [Fact]
        public void VoidConstructor_Correct()
        {
            ICollectionRules collectionRules = new CollectionRules();

            Assert.NotNull(collectionRules);
        }
    }
}
