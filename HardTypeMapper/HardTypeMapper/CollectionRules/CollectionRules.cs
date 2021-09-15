using HardTypeMapper.Models.CollectionModels;
using Interfaces.CollectionRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HardTypeMapper.CollectionRules
{
    public class CollectionRules : AbstractCollectionRules, ISummaryCollectionRules
    {     
        #region Add methods
        public IRulesAdd AddRule<TFrom, TTo>(Expression<Func<IRulesGet, TFrom, TTo>> expressionMaping, string nameRule = null)
        {
            var key = SetOfTypesHelper.Create<TTo>(nameRule, typeof(TFrom));

            AddRule(key, expressionMaping);

            return this;
        }
        #endregion

        #region Get methods
        public Expression<Func<IRulesGet, TFrom, TTo>> GetAnyRule<TFrom, TTo>()
        {
            var key = SetOfTypesHelper.Create<TTo>(null, typeof(TFrom));

            return ConvertExpression<Expression<Func<IRulesGet, TFrom, TTo>>>(GetAnyRule(key));
        }

        public Expression<Func<IRulesGet, TFrom, TTo>> GetRule<TFrom, TTo>(string nameRule = null)
        {
            if (string.Empty == nameRule)
                throw new ArgumentNullException(nameof(nameRule));

            var key = SetOfTypesHelper.Create<TTo>(nameRule, typeof(TFrom));

            return ConvertExpression<Expression<Func<IRulesGet, TFrom, TTo>>>(GetRule(key));
        }

        public Dictionary<string, Expression<Func<IRulesGet, TFrom, TTo>>> GetRules<TFrom, TTo>()
        {
            var key = SetOfTypesHelper.Create<TTo>(null, typeof(TFrom));

            var dictReturn = new Dictionary<string, Expression<Func<IRulesGet, TFrom, TTo>>>();

            var dict = GetRules(key, false);

            foreach (var v in dict)
                dictReturn.Add(v.Key, ConvertExpression<Expression<Func<IRulesGet, TFrom, TTo>>>(v.Value));

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

        public IRulesAdd AddRule<TFrom1, TFrom2, TTo>(Expression<Func<IRulesGet, TFrom1, TFrom2, TTo>> expressionMaping, string nameRule = null)
        {
            throw new NotImplementedException();
        }

        public Expression<Func<IRulesGet, TFrom1, TFrom2, TTo>> GetAnyRule<TFrom1, TFrom2, TTo>()
        {
            throw new NotImplementedException();
        }

        public Expression<Func<IRulesGet, TFrom1, TFrom2, TTo>> GetRule<TFrom1, TFrom2, TTo>(string nameRule = null)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, Expression<Func<IRulesGet, TFrom1, TFrom2, TTo>>> GetRules<TFrom1, TFrom2, TTo>()
        {
            throw new NotImplementedException();
        }

        public bool ExistRule<TFrom1, TFrom2, TTo>(string nameRule = null)
        {
            throw new NotImplementedException();
        }

        public void DeleteRule<TFrom1, TFrom2, TTo>(string nameRule = null)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
