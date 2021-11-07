using HardTypeMapper.Models.CollectionModels;
using Interfaces.CollectionRules;
using Interfaces.MapMethods;
using System;
using System.Collections.Generic;
using System.Linq;

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

            var actionsWithParent = new List<Action<IMapMethods, TFrom, TTo>>();
            actionsWithParent.Add(ConvertAction<Action<IMapMethods, TFrom, TTo>>(setRule.Value));

            Func<ISetOfRule, Action<IMapMethods, TFrom, TTo>> getAction = s 
                => ConvertAction<Action<IMapMethods, TFrom, TTo>>(dictRuleAction[s]);

            var set = setRule.Key.ParentRule;
            while (set != null)
            {
                actionsWithParent.Add(getAction(set));
                set = set.ParentRule; 
            }

            Action<IMapMethods, TFrom, TTo> finalyRule = (mm, from, to) => {
                foreach (var a in actionsWithParent)
                    a(mm, from, to);
            };

            return finalyRule;
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
