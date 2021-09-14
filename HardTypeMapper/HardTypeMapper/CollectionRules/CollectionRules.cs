﻿using Exceptions.ForCollectionRules;
using Interfaces.CollectionRules;
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
            var key = GetSetOfTypes<TTo>(null, typeof(TFrom));

            var rules = GetRule<Expression<Func<ICollectionRulesOneIn, TFrom, TTo>>>(key);

            if (!rules.Any())
                throw new RuleNotExistException(typeof(Expression<Func<ICollectionRulesOneIn, TFrom, TTo>>));

            return rules.First();
        }

        public Expression<Func<ICollectionRulesOneIn, TFrom, TTo>> GetRule<TFrom, TTo>(string nameRule)
        {
            if (string.IsNullOrEmpty(nameRule))
                throw new ArgumentNullException(nameof(nameRule));

            if (!RuleExist<TFrom, TTo>(nameRule))
                throw new RuleNotExistException(nameRule);

            var key = GetSetOfTypes<TTo>(nameRule, typeof(TFrom));

            var rules = GetRule<Expression<Func<ICollectionRulesOneIn, TFrom, TTo>>>(key);

            if (!rules.Any())
                throw new RuleNotExistException(nameRule);

            return rules.First();
        }

        public IEnumerable<Expression<Func<ICollectionRulesOneIn, TFrom, TTo>>> GetRules<TFrom, TTo>()
        {
            var key = GetSetOfTypes<TTo>(null, typeof(TFrom));

            return GetRule<Expression<Func<ICollectionRulesOneIn, TFrom, TTo>>>(key);
        }
        #endregion

        #region Exist and Delete methods
        public bool RuleExist<TFrom, TTo>(string nameRule = null)
        {
            if (string.Empty == nameRule)
                throw new ArgumentException($"Параметр <{nameof(nameRule)}> может быть равным null, но не может быть равным string.Empty.");

            var key = GetSetOfTypes<TTo>(nameRule, typeof(TFrom));

            return GetRule<Expression>(key).Any();
        }

        public void DeleteRule<TFrom, TTo>(string nameRule = null)
        {
            if (string.Empty == nameRule)
                throw new ArgumentException($"Параметр <{nameof(nameRule)}> может быть равным null, но не может быть равным string.Empty.");

            var key = GetSetOfTypes<TTo>(nameRule, typeof(TFrom));

            Func<KeyValuePair<ISetOfTypes, Expression>, bool> equals =
               pair => string.IsNullOrEmpty(pair.Key.SetName)
               ?
               pair.Key.Equals(key, false)
               :
               pair.Key.Equals(key, true);

            foreach (var item in dictRuleExpression)
                if (equals(item))
                {
                    dictRuleExpression.Remove(item.Key);

                    return;
                }

            throw new RuleNotExistException(nameRule);
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

        private TRuleExpr ConvertExpression<TRuleExpr>(Expression expr) where TRuleExpr : Expression
        {
            if (expr is TRuleExpr)
                return (TRuleExpr)expr;

            throw new ExpressionNotNeededTypeException(typeof(TRuleExpr).FullName);
        }

        private IEnumerable<TRuleExpr> GetRule<TRuleExpr>(ISetOfTypes key) where TRuleExpr : Expression
        {
            Func<KeyValuePair<ISetOfTypes, Expression>, bool> equals =
                pair => string.IsNullOrEmpty(pair.Key.SetName)
                ?
                pair.Key.Equals(key, false)
                :
                pair.Key.Equals(key, true);

            foreach (var item in dictRuleExpression)
                if (equals(item))
                    yield return ConvertExpression<TRuleExpr>(item.Value);
        }
        #endregion
    }
}
