using HardTypeMapper.CollectionRules;
using Interfaces.CollectionRules;
using Interfaces.MapMethods;
using System;
using Xunit;

namespace UnitTests.CollectionRulesMethodTests
{
    public class CollectionRules_AddParent_GetRuleWithParent_Tests
    {
        #region Class constructors
        [Fact]
        public void VoidConstructor_Correct()
        {
            ICollectionRules collectionRules = new CollectionRules();

            Assert.NotNull(collectionRules);
        }
        #endregion

        #region AddParent method
        [Fact]
        public void AddParentRule_Correct()
        {
            var collectionRules = new CollectionRules();

            Action<IMapMethods, Child, ChildDto> actionChild = (mm, c, cdto)
                => { cdto.ChildField = c.ChildField; };
            Action<IMapMethods, Parent, ParentDto> actionParent = (mm, p, pdto)
                => { pdto.ParentField = p.ParentField; };
            
            collectionRules.AddRule(actionParent);
            collectionRules.AddRule(actionChild).AddParentMap();

            var ruleChild = collectionRules.GetRule<Child, ChildDto>();
            var ruleParent = collectionRules.GetRule<Parent, ParentDto>();

            Assert.NotNull(ruleChild);
            Assert.NotNull(ruleParent);

            var child = new Child() { ChildField = "child", ParentField = "parent" };
            var dto = new ChildDto();

            ruleChild.Invoke(null, child, dto);
            ruleParent.Invoke(null, child, dto);

            Assert.Equal("child", dto.ChildField);
            Assert.Equal("parent", dto.ParentField);
        }
        #endregion

        #region GetRuleWithParent method
        [Fact]
        public void GetRuleWithParent_Correct()
        {
            var collectionRules = new CollectionRules();

            Action<IMapMethods, Child, ChildDto> actionChild = (mm, c, cdto)
                => { cdto.ChildField = c.ChildField; };
            Action<IMapMethods, Parent, ParentDto> actionParent = (mm, p, pdto)
                => { pdto.ParentField = p.ParentField; };

            collectionRules.AddRule(actionParent);
            collectionRules.AddRule(actionChild).AddParentMap();

            var ruleChild = collectionRules.GetRuleWithParent<Child, ChildDto>();

            Assert.NotNull(ruleChild);

            var child = new Child() { ChildField = "child", ParentField = "parent" };
            var dto = new ChildDto();

            ruleChild.Invoke(null, child, dto);

            Assert.Equal("child", dto.ChildField);
            Assert.Equal("parent", dto.ParentField);
        }
        #endregion

        class Parent { public string ParentField; }
        class Child : Parent { public string ChildField; }

        class ParentDto { public string ParentField; }
        class ChildDto : ParentDto { public string ChildField; }
    }
}
