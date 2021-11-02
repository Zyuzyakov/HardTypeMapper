using Xunit;
using System;
using Exceptions.ForCollectionRules;
using HardTypeMapper.CollectionRules;
using Interfaces.CollectionRules;
using UnitTests.TestModels;
using Interfaces.MapMethods;

namespace UnitTests.CollectionRulesMethodTests
{
    public class CollectionRules_Delete_Tests
    {
        #region Class constructors
        [Fact]
        public void VoidConstructor_Correct()
        {
            ICollectionRules collectionRules = new CollectionRules();

            Assert.NotNull(collectionRules);
        }
        #endregion

        #region Delete methods
        [Fact]
        public void DeleteRule_WhenParamEmpty_ThrowArgumentException()
        {
            var collectionRules = new CollectionRules();

            Assert.Throws<ArgumentException>(() => collectionRules.DeleteRule<Street, StreetDto>(string.Empty));
        }

        [Fact]
        public void DeleteRule_WhenParamGood_ThrowRuleNotExistException()
        {
            var collectionRules = new CollectionRules();

            Assert.Throws<RuleNotExistException>(() => collectionRules.DeleteRule<Street, StreetDto>());
            Assert.Throws<RuleNotExistException>(() => collectionRules.DeleteRule<Street, StreetDto>(null));
            Assert.Throws<RuleNotExistException>(() => collectionRules.DeleteRule<Street, StreetDto>("test"));
        }

        [Fact]
        public void DeleteRule_WhenParamGood_Correct()
        {
            var collectionRules = new CollectionRules();
         
            Action<IMapMethods, Street, StreetDto> exprRule = (colRules, street, dto) => { };

            collectionRules.AddRule(exprRule, "test");
            Assert.True(collectionRules.ExistRule<Street, StreetDto>("test"));
            collectionRules.DeleteRule<Street, StreetDto>("test");
            Assert.False(collectionRules.ExistRule<Street, StreetDto>("test"));

            collectionRules.AddRule(exprRule);
            Assert.True(collectionRules.ExistRule<Street, StreetDto>());
            collectionRules.DeleteRule<Street, StreetDto>();
            Assert.False(collectionRules.ExistRule<Street, StreetDto>());
        }
        #endregion
    }
}
