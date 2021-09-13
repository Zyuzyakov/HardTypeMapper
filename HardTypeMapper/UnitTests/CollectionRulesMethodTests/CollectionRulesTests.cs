using Interfaces;
using Xunit;
using HardTypeMapper;
using System;
using System.Linq.Expressions;
using Exceptions.ForCollectionRules;

namespace UnitTests.CollectionRulesMethodTests
{
    public class CollectionRulesTests
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

            Expression<Func<Street, StreetDto>> expr = null;

            Assert.Throws<ArgumentNullException>(() => collectionRules.AddRule(expr));
        }

        [Fact]
        public void AddRule_WithGoodParam_Correct()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<Street, StreetDto>> expr = x => new StreetDto();

            collectionRules.AddRule(expr);
        }

        [Fact]
        public void AddRule_DoubleAdd_ThrowRuleExistException()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<Street, StreetDto>> expr = x => new StreetDto();

            collectionRules.AddRule(expr);

            Assert.Throws<RuleExistException>(() => collectionRules.AddRule(expr));
        }

        [Fact]
        public void AddRule_WithNullEmptyParams_ThrowArgumentNullException()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<Street, StreetDto>> expr = null;

            Assert.Throws<ArgumentNullException>(() => collectionRules.AddRule(expr, null));
            Assert.Throws<ArgumentNullException>(() => collectionRules.AddRule(expr, string.Empty));
            Assert.Throws<ArgumentNullException>(() => collectionRules.AddRule(expr, "test"));
        }

        [Fact]
        public void AddRule_WithName_GoodParam_Correct()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<Street, StreetDto>> expr = x => new StreetDto();

            collectionRules.AddRule(expr, "test");
        }

        [Fact]
        public void AddRule_WithName_DoubleAdd_ThrowRuleExistException()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<Street, StreetDto>> expr = x => new StreetDto();

            collectionRules.AddRule(expr, "test");

            Assert.Throws<RuleExistException>(() => collectionRules.AddRule(expr, "test"));
        }
        #endregion

        #region Get methods
        [Fact]
        public void GetFirstRule_WhenNotExist_ThrowRuleNotExistException()
        {
            var collectionRules = new CollectionRules();

            Assert.Throws<RuleNotExistException>(() => collectionRules.GetFirstRule<Street, StreetDto>());
        }

        [Fact]
        public void GetFirstRule_Correct()
        {
            var collectionRules = new CollectionRules();

            collectionRules.AddRule<Street, StreetDto>(x => new StreetDto());

            var rule = collectionRules.GetFirstRule<Street, StreetDto>();

            Assert.NotNull(rule);
        }

        #endregion
    }
}
