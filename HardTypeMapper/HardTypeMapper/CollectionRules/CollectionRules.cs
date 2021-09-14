﻿using Exceptions.ForCollectionRules;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HardTypeMapper.CollectionRules
{
    public class CollectionRules : ICollectionRulesOneIn
    {
        #region Сlass variables
        protected Dictionary<ISetOfTypes, Expression> dictRuleExpression;
        #endregion

        #region Class constructors
        public CollectionRules() 
        {
            dictRuleExpression = new Dictionary<ISetOfTypes, Expression>();
        }
        #endregion

        #region Add methods
        public ICollectionRulesOneIn AddRule<TFrom, TTo>(Expression<Func<ICollectionRulesOneIn, TFrom, TTo>> expressionMaping, string nameRule = null)
        {
            var key = GetSetOfTypes<TTo>(nameRule, typeof(TFrom));

            AddToCollection(expressionMaping, key);

            return this;
        }
        #endregion

        #region Get methods
        public Expression<Func<ICollectionRulesOneIn, TFrom, TTo>> GetAnyRule<TFrom, TTo>()
        {
           var key = GetSetOfTypes<TTo>(string.Empty, typeof(TFrom));

            foreach (var item in dictRuleExpression)
                if (item.Key.Equals(key, true))
                {
                    var exprReturn = item.Value as Expression<Func<ICollectionRulesOneIn, TFrom, TTo>>;

                    if (exprReturn is not null)
                        return exprReturn;
                    else throw new ExpressionNotNeededTypeException(nameof(Expression<Func<ICollectionRulesOneIn, TFrom, TTo>>));
                }

            throw new RuleNotExistException(nameof(Expression<Func<ICollectionRulesOneIn, TFrom, TTo>>));
        }

        public Expression<Func<ICollectionRulesOneIn, TFrom, TTo>> GetRule<TFrom, TTo>(string nameRule)
        {
            if (string.IsNullOrEmpty(nameRule))
                throw new ArgumentNullException(nameof(nameRule));

            if (!RuleExist<TFrom, TTo>(nameRule))
                throw new RuleNotExistException(nameRule);

            var ruleExpr = dictRuleExpression.First(x => x.Key.SetName == nameRule).Value;

            return ConvertExpression<Expression<Func<ICollectionRulesOneIn, TFrom, TTo>>>(ruleExpr);
        }

        public IEnumerable<Expression<Func<ICollectionRulesOneIn, TFrom, TTo>>> GetRules<TFrom, TTo>()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Exist and Delete methods
        public bool RuleExist<TFrom, TTo>(string nameRule = null)
        {
            throw new NotImplementedException();
        }

        public void DeleteRule<TFrom, TTo>(string nameRule = null)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Class private methods
        SetOfTypes<TOutType> GetSetOfTypes<TOutType>(string nameRule, params Type[] inTypes)
        {
            if (inTypes is null || inTypes.Length < 1)
                throw new ArgumentException($"Количество входных типов должно быть >= 1.");

            return new SetOfTypes<TOutType>(nameRule, inTypes);
        }

        private void AddToCollection(Expression expr, ISetOfTypes key)
        {
            if (expr is null)
                throw new ArgumentNullException(nameof(expr));

            if (key is null)
                throw new ArgumentNullException(nameof(key));

            if (!dictRuleExpression.TryAdd(key, expr))
                throw new RuleNotAddException(key.SetName);
        }

        private TAsType ConvertExpression<TAsType>(Expression expr) where TAsType : Expression
        {
            if (expr is TAsType)
                return (TAsType)expr;

            throw new ExpressionNotNeededTypeException(typeof(TAsType).FullName);
        }
        #endregion
    }
}
