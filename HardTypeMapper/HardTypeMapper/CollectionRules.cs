using Exceptions.ForCollectionRules;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HardTypeMapper
{
    public class CollectionRules : ICollectionRules
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
        public ICollectionRules AddRule<TFrom, TTo>(Expression<Func<ICollectionRules, TFrom, TTo>> expressionMaping, string nameRule = null)
        {
            var key = GetSetOfTypes<TTo>(nameRule, typeof(TFrom));

            AddToCollection(expressionMaping, key);

            return this;
        }

        public ICollectionRules AddRule<TFrom1, TFrom2, TTo>(Expression<Func<ICollectionRules, TFrom1, TFrom2, TTo>> expressionMaping, string nameRule = null)
        {
            var key = GetSetOfTypes<TTo>(nameRule, typeof(TFrom1), typeof(TFrom2));

            AddToCollection(expressionMaping, key);

            return this;
        }

        public ICollectionRules AddRule<TFrom1, TFrom2, TFrom3, TTo>(Expression<Func<ICollectionRules, TFrom1, TFrom2, TFrom3, TTo>> expressionMaping, string nameRule = null)
        {
            var key = GetSetOfTypes<TTo>(nameRule, typeof(TFrom1), typeof(TFrom2), typeof(TFrom3));

            AddToCollection(expressionMaping, key);

            return this;
        }
        #endregion

        #region Get methods
        public Expression<Func<ICollectionRules, TFrom, TTo>> GetAnyRule<TFrom, TTo>()
        {
            throw new NotImplementedException();
        }

        public Expression<Func<ICollectionRules, TFrom, TTo>> GetRule<TFrom, TTo>(string nameRule = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Expression<Func<ICollectionRules, TFrom, TTo>>> GetRules<TFrom, TTo>()
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
        #endregion
    }
}
