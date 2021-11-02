using Exceptions.ForCollectionRules;
using HardTypeMapper.CollectionRules;
using Interfaces.CollectionRules;
using Interfaces.MapMethods;
using System;
using UnitTests.TestModels;
using Xunit;

namespace UnitTests.CollectionRulesMethodTests
{
    public class CollectionRules_Get_Tests
    {
        #region Class constructors
        [Fact]
        public void VoidConstructor_Correct()
        {
            ICollectionRules collectionRules = new CollectionRules();

            Assert.NotNull(collectionRules);
        }
        #endregion

        #region Get methods
        [Fact]
        public void GetAnyRule_WhenNotExist_ThrowRuleNotExistException()
        {
            var collectionRules = new CollectionRules();

            Assert.Throws<RuleNotExistException>(() => collectionRules.GetAnyRule<Street, StreetDto>());
        }

        [Fact]
        public void GetAnyRule_Correct()
        {
            var collectionRules = new CollectionRules();

            collectionRules.AddRule<Street, StreetDto>((y, x, z) => new StreetDto());

            var rule = collectionRules.GetAnyRule<Street, StreetDto>();

            Assert.NotNull(rule);
        }

        [Fact]
        public void GetRule_WhenEmptyParam_ThrowArgumentNullException()
        {
            var collectionRules = new CollectionRules();

            Assert.Throws<ArgumentNullException>(() => collectionRules.GetRule<Street, StreetDto>(string.Empty));
        }

        [Fact]
        public void GetRule_WhenRuleNotExist_ThrowRuleNotExistException()
        {
            var collectionRules = new CollectionRules();

            Assert.Throws<RuleNotExistException>(() => collectionRules.GetRule<Street, StreetDto>("test"));
        }

        [Fact]
        public void GetRule_WithName_Correct()
        {
            var collectionRules = new CollectionRules();

            Action<IMapMethods, Street, StreetDto> exprRule = (colRules, street, dto) => { };

            collectionRules.AddRule(exprRule, "test");

            var rule = collectionRules.GetRule<Street, StreetDto>("test");

            Assert.NotNull(rule);
        }

        [Fact]
        public void GetRule_WithOutName_Correct()
        {
            var collectionRules = new CollectionRules();

            Action<IMapMethods, Street, StreetDto> exprRule = (mm, street, dto) => { };

            collectionRules.AddRule(exprRule);

            var rule = collectionRules.GetRule<Street, StreetDto>();

            Assert.NotNull(rule);
        }

        [Fact]
        public void GetRule_NameAddGetWithOutName_ThrowRuleNotExistException()
        {
            var collectionRules = new CollectionRules();

            Action<IMapMethods, Street, StreetDto> exprRule = (mm, street, dto) => { };

            collectionRules.AddRule(exprRule, "test");

            Assert.Throws<RuleNotExistException>(() => collectionRules.GetRule<Street, StreetDto>());
        }

        [Fact]
        public void GetRule_NameNullGetWithName_ThrowRuleNotExistException()
        {
            var collectionRules = new CollectionRules();

            Action<IMapMethods, Street, StreetDto> exprRule = (mm, street, dto) => { };

            collectionRules.AddRule(exprRule);

            Assert.Throws<RuleNotExistException>(() => collectionRules.GetRule<Street, StreetDto>("test"));
        }
        #endregion
    }
}
