using Exceptions.ForCollectionRules;
using HardTypeMapper.Models.CollectionModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HardTypeMapper.CollectionRules
{
    public abstract class AbstractCollectionRules
    {
        #region Сlass variables
        protected Dictionary<ISetOfTypes, Delegate> dictRuleAction;
        #endregion

        #region Class constructors
        protected AbstractCollectionRules()
        {
            dictRuleAction = new Dictionary<ISetOfTypes, Delegate>();
        }
        #endregion

        #region Add methods
        public void AddRule(ISetOfTypes setOfTypes, Delegate actionMaping)
        {
            if (setOfTypes is null)
                throw new ArgumentNullException(nameof(setOfTypes));

            if (actionMaping is null)
                throw new ArgumentNullException(nameof(actionMaping));

            if (!dictRuleAction.TryAdd(setOfTypes, actionMaping))
                 throw new RuleNotAddException(setOfTypes.SetName);
        }
        #endregion

        #region Get methods
        public Delegate GetAnyRule(ISetOfTypes setOfTypes)
        {
            if (setOfTypes is null)
                throw new ArgumentNullException(nameof(setOfTypes));

            var rules = GetRules<Delegate>(setOfTypes, false);

            if (!rules.Any())
                throw new RuleNotExistException(setOfTypes.SetName, setOfTypes.GetOutTypeParam(), setOfTypes.GetInTypeParams());

            return rules.First();
        }

        public Delegate GetRule(ISetOfTypes setOfTypes)
        {
            if (setOfTypes is null)
                throw new ArgumentNullException(nameof(setOfTypes));

            var rules = GetRules<Delegate>(setOfTypes, true);

            if (!rules.Any())
                throw new RuleNotExistException(setOfTypes.SetName, setOfTypes.GetOutTypeParam(), setOfTypes.GetInTypeParams());

            return rules.First();
        }

        protected Dictionary<string, Delegate> GetRules(ISetOfTypes setOfTypes, bool withName)
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
        public bool ExistRule(ISetOfTypes setOfTypes)
        {
            if (setOfTypes is null)
                throw new ArgumentNullException(nameof(setOfTypes));

            return GetRules<Delegate>(setOfTypes, true).Any();
        }

        public void DeleteRule(ISetOfTypes setOfTypes)
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
        private readonly Func<KeyValuePair<ISetOfTypes, Delegate>, ISetOfTypes, bool, bool> equals =
               (pair, existKey, withName) => pair.Key.Equals(existKey, withName);
           
        private protected TRule ConvertAction<TRule>(Delegate action) where TRule : Delegate
        {
            if (action is TRule actionConverted)
                return actionConverted;

            throw new DelegateNotNeededTypeException(typeof(TRule).FullName);
        }

        protected IEnumerable<TRule> GetRules<TRule>(ISetOfTypes key, bool checkName) where TRule : Delegate
        {
            foreach (var item in dictRuleAction)
                if (equals(item, key, checkName))
                    yield return ConvertAction<TRule>(item.Value);
        }
        #endregion
    }
}
