using Interfaces;
using Xunit;
using System;
using System.Linq.Expressions;
using Exceptions.ForCollectionRules;
using HardTypeMapper.CollectionRules;

namespace UnitTests.CollectionRulesMethodTests
{
    public class CollectionRulesTests
    {
        #region Class constructors
        [Fact]
        public void VoidConstructor_Correct()
        {
            ICollectionRulesOneIn collectionRules = new CollectionRules();

            Assert.NotNull(collectionRules);
        }
        #endregion

        #region Add methods
        [Fact]
        public void AddRule_WithNullParam_ThrowArgumentNullException()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<ICollectionRulesOneIn, Street, StreetDto>> expr = null;

            Assert.Throws<ArgumentNullException>(() => collectionRules.AddRule(expr));
        }

        [Fact]
        public void AddRule_WithGoodParam_Correct()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<ICollectionRulesOneIn, Street, StreetDto>> expr = (x, y) => new StreetDto();

            collectionRules.AddRule(expr);
        }

        [Fact]
        public void AddRule_DoubleAdd_ThrowRuleExistException()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<ICollectionRulesOneIn, Street, StreetDto>> expr = (x, y) => new StreetDto();

            collectionRules.AddRule(expr);

            Assert.Throws<RuleNotAddException>(() => collectionRules.AddRule(expr));
        }

        [Fact]
        public void AddRule_WithNullEmptyParams_ThrowArgumentNullException()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<ICollectionRulesOneIn, Street, StreetDto>> expr = null;

            Assert.Throws<ArgumentNullException>(() => collectionRules.AddRule(expr, null));
            Assert.Throws<ArgumentNullException>(() => collectionRules.AddRule(expr, string.Empty));
            Assert.Throws<ArgumentNullException>(() => collectionRules.AddRule(expr, "test"));
        }

        [Fact]
        public void AddRule_WithName_GoodParam_Correct()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<ICollectionRulesOneIn, Street, StreetDto>> expr = (x, y) => new StreetDto();

            collectionRules.AddRule(expr, "test");
        }

        [Fact]
        public void AddRule_WithName_DoubleAdd_ThrowRuleExistException()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<ICollectionRulesOneIn, Street, StreetDto>> expr = (x, y) => new StreetDto();

            collectionRules.AddRule(expr, "test");

            Assert.Throws<RuleNotAddException>(() => collectionRules.AddRule(expr, "test"));
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

            collectionRules.AddRule<Street, StreetDto>((y,x) => new StreetDto());

            var rule = collectionRules.GetAnyRule<Street, StreetDto>();

            Assert.NotNull(rule);
        }

        #endregion
    }
}
