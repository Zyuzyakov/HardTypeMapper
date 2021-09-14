using HardTypeMapper;
using Xunit;

namespace UnitTests.CollectionRulesMethodTests
{
    public class SetOfTypesTests
    {
        [Fact]
        public void SetOfTypes_Init_Correct()
        {
            var setOfTypes1 = new SetOfTypes<object>();

            Assert.Empty(setOfTypes1.InTypes);
            Assert.True(string.IsNullOrEmpty(setOfTypes1.SetName));

            var setOfTypes2 = new SetOfTypes<object>();

            Assert.Empty(setOfTypes2.InTypes);
            Assert.True(string.IsNullOrEmpty(setOfTypes2.SetName));


            Assert.True(setOfTypes1.Equals(setOfTypes2));
        }
    }
}
