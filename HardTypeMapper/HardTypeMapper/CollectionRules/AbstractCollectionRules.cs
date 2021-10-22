using Exceptions.ForCollectionRules;
using Interfaces.CollectionRules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HardTypeMapper.CollectionRules
{
    public abstract class AbstractCollectionRules : ILinkBaseRule
    {
        #region Сlass variables
        protected Dictionary<ISetOfRule, Delegate> dictRuleAction;

        protected ISetOfRule lastAddSetOfRule;
        #endregion

        #region Class constructors
        protected AbstractCollectionRules()
        {
            dictRuleAction = new Dictionary<ISetOfRule, Delegate>();
        }
        #endregion

        #region Add methods
        public void AddRule(ISetOfRule setOfTypes, Delegate actionMaping)
        {
            if (setOfTypes is null)
                throw new ArgumentNullException(nameof(setOfTypes));

            if (actionMaping is null)
                throw new ArgumentNullException(nameof(actionMaping));            

            if (!dictRuleAction.TryAdd(setOfTypes, actionMaping))
                 throw new RuleNotAddException(setOfTypes.SetName);

            lastAddSetOfRule = setOfTypes;
        }

        public void AddParentMap(string nameRule = null) 
        {

            if (!TryGetParentType(lastAddSetOfRule.GetOutTypeParam(), out Type parentType))
                throw new NotHaveParentClassException(lastAddSetOfRule.GetOutTypeParam());

            if (!TryGetParentSetOfRule(parentType, nameRule, out ISetOfRule parentSetOfRule))
                throw new NotHaveParentSetOfRuleException(parentType, nameRule);

            if (!ChildSetMatchParentSet(lastAddSetOfRule, parentSetOfRule))
                throw new ParentSetCanNotBeUsedWithChildSetException(lastAddSetOfRule.GetOutTypeParam(), parentType);

            throw new NotImplementedException();
        }
        #endregion

        #region Get methods
        public Delegate GetAnyRule(ISetOfRule setOfTypes)
        {
            if (setOfTypes is null)
                throw new ArgumentNullException(nameof(setOfTypes));

            var rules = GetRules<Delegate>(setOfTypes, false);

            if (!rules.Any())
                throw new RuleNotExistException(setOfTypes.SetName, setOfTypes.GetOutTypeParam(), setOfTypes.GetInTypeParams());

            return rules.First();
        }

        public Delegate GetRule(ISetOfRule setOfTypes)
        {
            if (setOfTypes is null)
                throw new ArgumentNullException(nameof(setOfTypes));

            var rules = GetRules<Delegate>(setOfTypes, true);

            if (!rules.Any())
                throw new RuleNotExistException(setOfTypes.SetName, setOfTypes.GetOutTypeParam(), setOfTypes.GetInTypeParams());

            return rules.First();
        }

        protected Dictionary<string, Delegate> GetRules(ISetOfRule setOfTypes, bool withName)
        {
            if (setOfTypes is null)
                throw new ArgumentNullException(nameof(setOfTypes));

            var dictReturn = new Dictionary<string, Delegate>();

            foreach (var pair in dictRuleAction)
                if (pair.Key.Equals(setOfTypes, withName))
                    dictReturn.Add(pair.Key.SetName, pair.Value);

            return dictReturn;
        }
        #endregion

        #region Exist and Delete methods
        public bool ExistRule(ISetOfRule setOfTypes)
        {
            if (setOfTypes is null)
                throw new ArgumentNullException(nameof(setOfTypes));

            return GetRules<Delegate>(setOfTypes, true).Any();
        }

        public void DeleteRule(ISetOfRule setOfTypes)
        {
            if (setOfTypes is null)
                throw new ArgumentNullException(nameof(setOfTypes));

            foreach (var item in dictRuleAction)
                if (equals(item, setOfTypes, true))
                {
                    dictRuleAction.Remove(item.Key);

                    return;
                }

            throw new RuleNotExistException(setOfTypes.SetName);
        }
        #endregion

        #region Class private methods
        private readonly Func<KeyValuePair<ISetOfRule, Delegate>, ISetOfRule, bool, bool> equals =
               (pair, existKey, withName) => pair.Key.Equals(existKey, withName);
           
        private protected TRule ConvertAction<TRule>(Delegate action) where TRule : Delegate
        {
            if (action is TRule actionConverted)
                return actionConverted;

            throw new DelegateNotNeededTypeException(typeof(TRule).FullName);
        }

        protected IEnumerable<TRule> GetRules<TRule>(ISetOfRule key, bool checkName) where TRule : Delegate
        {
            foreach (var item in dictRuleAction)
                if (equals(item, key, checkName))
                    yield return ConvertAction<TRule>(item.Value);
        }

        private bool TryGetParentType(Type target, out Type parentType)
        {
            parentType = target.BaseType;

            if (parentType == typeof(object))
                parentType = null;

            return parentType != null;
        }

        private bool TryGetParentSetOfRule(Type parentType, string nameRule, out ISetOfRule parentSetOfRule)
        {
            parentSetOfRule = dictRuleAction.FirstOrDefault(x => x.Key.GetOutTypeParam() == parentType && x.Key.SetName == nameRule).Key;

            return parentSetOfRule != null;
        }

        private bool ChildSetMatchParentSet(ISetOfRule chieldSet, ISetOfRule parentSet)
        {
            //to do нужно проверить входящие типы parentSet с chieldSet и их иерархию наследования (чтобы каждый параметр соответствовал)


        }

        private IEnumerable<Type> GetHierarchyTypes(Type type)
        {
            yield return type;

            var objectType = typeof(object);

            while (type != null)
            {
                type = type.BaseType;

                if (type == objectType)
                    type = null;
                else if (type != null)
                    yield return type;
            }
        }
        #endregion
    }
}
