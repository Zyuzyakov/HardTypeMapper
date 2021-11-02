using Exceptions.ForCollectionRules;
using HardTypeMapper.CollectionRules;
using Interfaces.CollectionRules;
using Interfaces.MapMethods;
using System;
using UnitTests.TestModels;
using Xunit;

namespace UnitTests.CollectionRulesMethodTests
{
    public class CollectionRules_Add_Tests
    {
        #region Class constructors
        [Fact]
        public void VoidConstructor_Correct()
        {
            ICollectionRules collectionRules = new CollectionRules();

            Assert.NotNull(collectionRules);
        }
        #endregion

        #region Add methods
        [Fact]
        public void AddRule_WithNullParam_ThrowArgumentNullException()
        {
            var collectionRules = new CollectionRules();

            Action<IMapMethods, Street, StreetDto> expr = null;

            Assert.Throws<ArgumentNullException>(() => collectionRules.AddRule(expr));
        }

        [Fact]
        public void AddRule_WithGoodParam_Correct()
        {
            var collectionRules = new CollectionRules();

            Action<IMapMethods, Street, StreetDto> expr = (x, y, z) => { };

            var iRulesAdd = collectionRules.AddRule(expr);

            Assert.NotNull(iRulesAdd);
        }

        [Fact]
        public void AddRule_DoubleAdd_ThrowRuleExistException()
        {
            var collectionRules = new CollectionRules();

            Action<IMapMethods, Street, StreetDto> expr = (x, y, z) => { };

            collectionRules.AddRule(expr);

            Assert.Throws<RuleNotAddException>(() => collectionRules.AddRule(expr));
        }

        [Fact]
        public void AddRule_WithNullEmptyParams_ThrowArgumentNullException()
        {
            var collectionRules = new CollectionRules();

            Action<IMapMethods, Street, StreetDto> expr = null;

            Assert.Throws<ArgumentNullException>(() => collectionRules.AddRule(expr, null));
            Assert.Throws<ArgumentNullException>(() => collectionRules.AddRule(expr, string.Empty));
            Assert.Throws<ArgumentNullException>(() => collectionRules.AddRule(expr, "test"));
        }

        [Fact]
        public void AddRule_WithName_GoodParam_Correct()
        {
            var collectionRules = new CollectionRules();

            Action<IMapMethods, Street, StreetDto> expr = (x, y, z) => { };

            var iRulesAdd = collectionRules.AddRule(expr, "test");

            Assert.NotNull(iRulesAdd);
        }

        [Fact]
        public void AddRule_WithName_DoubleAdd_ThrowRuleExistException()
        {
            var collectionRules = new CollectionRules();

            Action<IMapMethods, Street, StreetDto> expr = (x, y, z) => { };

            collectionRules.AddRule(expr, "test");

            Assert.Throws<RuleNotAddException>(() => collectionRules.AddRule(expr, "test"));
        }
        #endregion
    }
}
