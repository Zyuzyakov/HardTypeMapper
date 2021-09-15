using Xunit;
using System;
using System.Linq.Expressions;
using Exceptions.ForCollectionRules;
using HardTypeMapper.CollectionRules;
using Interfaces.CollectionRules;
using UnitTests.TestModels;
using System.Linq;

namespace UnitTests.CollectionRulesMethodTests
{
    public class CollectionRules_ICollectionRulesOneIn_Tests
    {
        #region Class constructors
        [Fact]
        public void VoidConstructor_Correct()
        {
            ISummaryCollectionRules collectionRules = new CollectionRules();

            Assert.NotNull(collectionRules);
        }
        #endregion

        #region Add methods
        [Fact]
        public void AddRule_WithNullParam_ThrowArgumentNullException()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<IRulesGet, Street, StreetDto>> expr = null;

            Assert.Throws<ArgumentNullException>(() => collectionRules.AddRule(expr));
        }

        [Fact]
        public void AddRule_WithGoodParam_Correct()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<IRulesGet, Street, StreetDto>> expr = (x, y) => new StreetDto();

            collectionRules.AddRule(expr);
        }

        [Fact]
        public void AddRule_DoubleAdd_ThrowRuleExistException()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<IRulesGet, Street, StreetDto>> expr = (x, y) => new StreetDto();

            collectionRules.AddRule(expr);

            Assert.Throws<RuleNotAddException>(() => collectionRules.AddRule(expr));
        }

        [Fact]
        public void AddRule_WithNullEmptyParams_ThrowArgumentNullException()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<IRulesGet, Street, StreetDto>> expr = null;

            Assert.Throws<ArgumentNullException>(() => collectionRules.AddRule(expr, null));
            Assert.Throws<ArgumentNullException>(() => collectionRules.AddRule(expr, string.Empty));
            Assert.Throws<ArgumentNullException>(() => collectionRules.AddRule(expr, "test"));
        }

        [Fact]
        public void AddRule_WithName_GoodParam_Correct()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<IRulesGet, Street, StreetDto>> expr = (x, y) => new StreetDto();

            collectionRules.AddRule(expr, "test");
        }

        [Fact]
        public void AddRule_WithName_DoubleAdd_ThrowRuleExistException()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<IRulesGet, Street, StreetDto>> expr = (x, y) => new StreetDto();

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

            Expression<Func<IRulesGet, Street, StreetDto>> exprRule = (colRules, street) => new StreetDto();

            collectionRules.AddRule(exprRule, "test");

            var rule = collectionRules.GetRule<Street, StreetDto>("test");

            Assert.NotNull(rule);
        }

        [Fact]
        public void GetRule_WithOutName_Correct()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<IRulesGet, Street, StreetDto>> exprRule = (colRules, street) => new StreetDto();

            collectionRules.AddRule(exprRule);

            var rule = collectionRules.GetRule<Street, StreetDto>();

            Assert.NotNull(rule);
        }

        [Fact]
        public void GetRule_NameAddGetWithOutName_ThrowRuleNotExistException()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<IRulesGet, Street, StreetDto>> exprRule = (colRules, street) => new StreetDto();

            collectionRules.AddRule(exprRule, "test");

            Assert.Throws<RuleNotExistException>(() => collectionRules.GetRule<Street, StreetDto>());
        }

        [Fact]
        public void GetRule_NameNullGetWithName_ThrowRuleNotExistException()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<IRulesGet, Street, StreetDto>> exprRule = (colRules, street) => new StreetDto();

            collectionRules.AddRule(exprRule);

            Assert.Throws<RuleNotExistException>(() => collectionRules.GetRule<Street, StreetDto>("test"));
        }

        [Fact]
        public void GetRules_Correct_ReturnEmpty()
        {
            var collectionRules = new CollectionRules();

            var rules = collectionRules.GetRules<Street, StreetDto>();

            Assert.Empty(rules);
        }

        [Fact]
        public void GetRules_Correct_ReturnRules()
        {
            var collectionRules = new CollectionRules();

            Expression<Func<IRulesGet, Street, StreetDto>> expr = (x, y) => new StreetDto();

            collectionRules.AddRule(expr);

            var rules = collectionRules.GetRules<Street, StreetDto>();

            Assert.Single(rules);

            collectionRules.AddRule(expr, "test");

            rules = collectionRules.GetRules<Street, StreetDto>();

            Assert.Equal(2, rules.ToList().Count());
        }
        #endregion

        #region Exist and Delete methods
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

            Expression<Func<IRulesGet, Street, StreetDto>> exprRule = (colRules, street) => new StreetDto();

            Assert.False(collectionRules.ExistRule<Street, StreetDto>());

            collectionRules.AddRule(exprRule, "test");

            Assert.True(collectionRules.ExistRule<Street, StreetDto>("test"));

            Assert.False(collectionRules.ExistRule<Street, StreetDto>());

            Assert.False(collectionRules.ExistRule<Street, StreetDto>("notExist"));
        }

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
         
            Expression<Func<IRulesGet, Street, StreetDto>> exprRule = (colRules, street) => new StreetDto();

            collectionRules.AddRule(exprRule, "test");

            collectionRules.DeleteRule<Street, StreetDto>("test");

            collectionRules.AddRule(exprRule);

            collectionRules.DeleteRule<Street, StreetDto>();
        }
        #endregion
    }
}
