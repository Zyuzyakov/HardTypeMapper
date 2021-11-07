using HardTypeMapper.Models.CollectionModels;
using Interfaces.CollectionRules;
using Interfaces.MapMethods;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace HardTypeMapper.CollectionRules
{
    public class CollectionRules : AbstractCollectionRules, ICollectionRules
    {     
        #region Add methods
        public ILinkBaseRule AddRule<TFrom, TTo>(Action<IMapMethods, TFrom, TTo> actionMaping, string nameRule = null)
        {
            var key = SetOfTypesHelper.Create<TTo>(nameRule, typeof(TFrom));

            AddRule(key, actionMaping);

            return this;
        }
        #endregion

        #region Get methods
        public Action<IMapMethods, TFrom, TTo> GetRuleWithParent<TFrom, TTo>(string nameRule = null)
        {
            if (string.Empty == nameRule)
                throw new ArgumentNullException(nameof(nameRule));

            var setKey = SetOfTypesHelper.Create<TTo>(nameRule, typeof(TFrom));

            var setRule = dictRuleAction.FirstOrDefault(x => x.Key.Equals(setKey, true));

            var actionWithParent = ConvertAction<Action<IMapMethods, TFrom, TTo>>(setRule.Value);

            Func<ISetOfRule, Action<IMapMethods, TFrom, TTo>> getAction = s 
                => ConvertAction<Action<IMapMethods, TFrom, TTo>>(dictRuleAction[s]);

            var set = setRule.Key.ParentRule;
            while (set != null)
            {
                actionWithParent = (mm, from, to) => { actionWithParent(mm, from, to); getAction(set)(mm, from, to); };
                set = set.ParentRule; 
            }

            return actionWithParent;
        }

        public Action<IMapMethods, TFrom, TTo> GetAnyRule<TFrom, TTo>()
        {
            var key = SetOfTypesHelper.Create<TTo>(null, typeof(TFrom));

            return ConvertAction<Action<IMapMethods, TFrom, TTo>>(GetAnyRule(key));
        }

        public Action<IMapMethods, TFrom, TTo> GetRule<TFrom, TTo>(string nameRule = null)
        {
            if (string.Empty == nameRule)
                throw new ArgumentNullException(nameof(nameRule));

            var key = SetOfTypesHelper.Create<TTo>(nameRule, typeof(TFrom));

            return ConvertAction<Action<IMapMethods, TFrom, TTo>>(GetRule(key));
        }
        #endregion

        #region Exist methods
        public bool ExistRule<TFrom, TTo>(string nameRule = null)
        {
            if (string.Empty == nameRule)
                throw new ArgumentException($"Параметр <{nameof(nameRule)}> может быть равным null, но не может быть равным string.Empty.");

            var key = SetOfTypesHelper.Create<TTo>(nameRule, typeof(TFrom));

            return GetRules(key, true).Any();
        }
        #endregion

        #region Delete methods
        public void DeleteRule<TFrom, TTo>(string nameRule = null)
        {
            if (string.Empty == nameRule)
                throw new ArgumentException($"Параметр <{nameof(nameRule)}> может быть равным null, но не может быть равным string.Empty.");

            var key = SetOfTypesHelper.Create<TTo>(nameRule, typeof(TFrom));

            DeleteRule(key);
        }
        #endregion
    }
}
