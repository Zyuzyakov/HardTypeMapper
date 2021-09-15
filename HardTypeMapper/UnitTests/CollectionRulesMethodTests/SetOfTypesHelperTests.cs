using HardTypeMapper.Models.CollectionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.TestModels;
using Xunit;

namespace UnitTests.CollectionRulesMethodTests
{
    public class SetOfTypesHelperTests
    {
        [Fact]
        public void Create_WhenInParamsNullOrZero_ThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => SetOfTypesHelper.Create<StreetDto>(string.Empty, null));
        }

        [Fact]
        public void Create_Correct()
        {
            var setOfTypes = SetOfTypesHelper.Create<StreetDto>("test", typeof(Street), typeof(House));

            Assert.NotNull(setOfTypes);

            Assert.Equal("test", setOfTypes.SetName);
            Assert.Equal(typeof(StreetDto), setOfTypes.GetOutTypeParam());
          
            var inParams = setOfTypes.GetInTypeParams();

            Assert.Equal(2, inParams.Length);
            Assert.Contains(typeof(Street), inParams);
            Assert.Contains(typeof(House), inParams);
        }
    }
}
