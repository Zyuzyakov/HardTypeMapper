using HardTypeMapper.CollectionRules;
using HardTypeMapper.Models.CollectionModels;
using System.Linq;
using UnitTests.TestModels;
using Xunit;

namespace UnitTests.ModelsTests
{
    public class SetOfTypesTests
    {
        [Fact]
        public void SetOfTypes_VoidConstructor_Correct()
        {
            var setOfTypes1 = new SetOfRule<object>();

            Assert.Empty(setOfTypes1.InTypes);
            Assert.True(string.IsNullOrEmpty(setOfTypes1.SetName));
            Assert.Equal(typeof(object), setOfTypes1.GetOutTypeParam());

            var setOfTypes2 = new SetOfRule<object>();

            Assert.Empty(setOfTypes2.InTypes);
            Assert.True(string.IsNullOrEmpty(setOfTypes2.SetName));
            Assert.Equal(typeof(object), setOfTypes1.GetOutTypeParam());

            Assert.True(setOfTypes1.Equals(setOfTypes2));
        }

        [Fact]
        public void SetOfTypes_ParamConstructor_Correct()
        {
            var setOfTypes1 = new SetOfRule<Street>(typeof(StreetDto));

            Assert.Single(setOfTypes1.InTypes);
            Assert.True(string.IsNullOrEmpty(setOfTypes1.SetName));
            Assert.Equal(typeof(Street), setOfTypes1.GetOutTypeParam());

            var setOfTypes2 = new SetOfRule<Street>(typeof(StreetDto), "test");

            Assert.Single(setOfTypes2.InTypes);
            Assert.Equal("test", setOfTypes2.SetName);
            Assert.Equal(typeof(Street), setOfTypes1.GetOutTypeParam());

            Assert.False(setOfTypes1.Equals(setOfTypes2));
        }

        [Fact]
        public void SetOfTypes_MultiInParams_Correct()
        {
            var setOfTypes1 = new SetOfRule<Street>("", typeof(StreetDto), typeof(HouseDto));

            Assert.Equal(2, setOfTypes1.InTypes.ToList().Count);
            Assert.True(string.IsNullOrEmpty(setOfTypes1.SetName));
            Assert.Equal(typeof(Street), setOfTypes1.GetOutTypeParam());
        }

        [Fact]
        public void Equals_MultiInParams_Correct()
        {
            var setOfTypes1 = new SetOfRule<Street>("", typeof(StreetDto), typeof(HouseDto));

            Assert.Equal(2, setOfTypes1.InTypes.ToList().Count);
            Assert.True(string.IsNullOrEmpty(setOfTypes1.SetName));
            Assert.Equal(typeof(Street), setOfTypes1.GetOutTypeParam());

            var setOfTypes2 = new SetOfRule<Street>("", typeof(HouseDto), typeof(StreetDto));

            Assert.Equal(2, setOfTypes2.InTypes.ToList().Count);
            Assert.True(string.IsNullOrEmpty(setOfTypes2.SetName));
            Assert.Equal(typeof(Street), setOfTypes2.GetOutTypeParam());

            Assert.True(setOfTypes1.Equals(setOfTypes2));
            Assert.True(setOfTypes2.Equals(setOfTypes1));
        }

        [Fact]
        public void EqualsWithOutName_MultiInParams_Correct()
        {
            var setOfTypes1 = new SetOfRule<Street>("", typeof(StreetDto), typeof(HouseDto));

            Assert.Equal(2, setOfTypes1.InTypes.ToList().Count);
            Assert.True(string.IsNullOrEmpty(setOfTypes1.SetName));
            Assert.Equal(typeof(Street), setOfTypes1.GetOutTypeParam());

            var setOfTypes2 = new SetOfRule<Street>("test", typeof(HouseDto), typeof(StreetDto));

            Assert.Equal(2, setOfTypes2.InTypes.ToList().Count);
            Assert.Equal("test", setOfTypes2.SetName);
            Assert.Equal(typeof(Street), setOfTypes2.GetOutTypeParam());

            Assert.False(setOfTypes1.Equals(setOfTypes2));
            Assert.True(setOfTypes1.Equals(setOfTypes2, false));
            Assert.False(setOfTypes1.Equals(setOfTypes2, true));
        }
    }
}
