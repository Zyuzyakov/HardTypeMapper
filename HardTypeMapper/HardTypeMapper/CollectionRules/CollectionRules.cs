using Exceptions.ForCollectionRules;
using HardTypeMapper.Models.CollectionModels;
using Interfaces.CollectionRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HardTypeMapper.CollectionRules
{
    public class CollectionRules : AbstractCollectionRules, ICollectionRulesOneIn
    {     
        #region Add methods
        public ICollectionRulesOneIn AddRule<TFrom, TTo>(Expression<Func<ICollectionRulesOneIn, TFrom, TTo>> expressionMaping, string nameRule = null)
        {
            var key = SetOfTypesHelper.Create<TTo>(nameRule, typeof(TFrom));

            AddRule(key, expressionMaping);

            return this;
        }
        #endregion

        #region Get methods
        public Expression<Func<ICollectionRulesOneIn, TFrom, TTo>> GetAnyRule<TFrom, TTo>()
        {
            var key = SetOfTypesHelper.Create<TTo>(null, typeof(TFrom));

            return ConvertExpression<Expression<Func<ICollectionRulesOneIn, TFrom, TTo>>>(GetAnyRule(key));
        }

        public Expression<Func<ICollectionRulesOneIn, TFrom, TTo>> GetRule<TFrom, TTo>(string nameRule = null)
        {
            if (string.Empty == nameRule)
                throw new ArgumentNullException(nameof(nameRule));

            var key = SetOfTypesHelper.Create<TTo>(nameRule, typeof(TFrom));

            return ConvertExpression<Expression<Func<ICollectionRulesOneIn, TFrom, TTo>>>(GetRule(key));
        }

        public Dictionary<string, Expression<Func<ICollectionRulesOneIn, TFrom, TTo>>> GetRules<TFrom, TTo>()
        {
            var key = SetOfTypesHelper.Create<TTo>(null, typeof(TFrom));

            var dictReturn = new Dictionary<string, Expression<Func<ICollectionRulesOneIn, TFrom, TTo>>>();

            var dict = GetRules(key, false);

            foreach (var v in dict)
                dictReturn.Add(v.Key, ConvertExpression<Expression<Func<ICollectionRulesOneIn, TFrom, TTo>>>(v.Value));

            return dictReturn;
        }
        #endregion

        #region Exist and Delete methods
        public bool ExistRule<TFrom, TTo>(string nameRule = null)
        {
            if (string.Empty == nameRule)
                throw new ArgumentException($"Параметр <{nameof(nameRule)}> может быть равным null, но не может быть равным string.Empty.");

            var key = SetOfTypesHelper.Create<TTo>(nameRule, typeof(TFrom));

            return GetRules(key, true).Any();
        }

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
