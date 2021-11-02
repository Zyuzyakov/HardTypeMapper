using Xunit;
using System;
using HardTypeMapper.CollectionRules;
using Interfaces.CollectionRules;
using UnitTests.TestModels;
using Interfaces.MapMethods;

namespace UnitTests.CollectionRulesMethodTests
{
    public class CollectionRules_Exist_Tests
    {
        #region Class constructors
        [Fact]
        public void VoidConstructor_Correct()
        {
            ICollectionRules collectionRules = new CollectionRules();

            Assert.NotNull(collectionRules);
        }
        #endregion

        #region Exist methods
        [Fact]
        public void ExistRule_WhenRuleNameEmpty_ThrowArgumentException()
        {
            var collectionRules = new CollectionRules();

            Assert.Throws<ArgumentException>(() => collectionRules.ExistRule<Street, StreetDto>(""));
        }

        [Fact]
        public void ExistRule_Correct()
        {
            var collectionRules = new CollectionRules();

            Action<IMapMethods, Street, StreetDto> exprRule = (colRules, street, dto) => { };

            Assert.False(collectionRules.ExistRule<Street, StreetDto>());

            collectionRules.AddRule(exprRule, "test");

            Assert.True(collectionRules.ExistRule<Street, StreetDto>("test"));
            Assert.False(collectionRules.ExistRule<Street, StreetDto>());
            Assert.False(collectionRules.ExistRule<Street, StreetDto>("notExist"));
        }
        #endregion
    }
}
