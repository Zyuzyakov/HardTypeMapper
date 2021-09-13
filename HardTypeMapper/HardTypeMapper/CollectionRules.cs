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
        protected Dictionary<Type, Expression> dictRuleExpressionWithOutName;

        protected Dictionary<string, Expression> dictRuleExpressionWithName;
        #endregion

        #region Class constructors
        public CollectionRules() 
        {
            dictRuleExpressionWithOutName = new Dictionary<Type, Expression>();
            dictRuleExpressionWithName = new Dictionary<string, Expression>();
        }
        #endregion

        #region Add methods
        public ICollectionRules AddRule<TFrom, TTo>(Expression<Func<TFrom, TTo>> expressionMaping)
        {
            if (expressionMaping is null)
                throw new ArgumentNullException(nameof(expressionMaping));

            if (dictRuleExpressionWithOutName.TryAdd(expressionMaping.GetType(), expressionMaping))
                return this;
            else throw new RuleExistException(expressionMaping.GetType());
        }

        public ICollectionRules AddRule<TFrom, TTo>(Expression<Func<TFrom, TTo>> expressionMaping, string nameRule)
        {
            if (expressionMaping is null)
                throw new ArgumentNullException(nameof(expressionMaping));

            if (string.IsNullOrEmpty(nameRule))
                throw new ArgumentNullException(nameof(nameRule));

            if (dictRuleExpressionWithName.TryAdd(nameRule, expressionMaping))
                return this;
            else throw new RuleExistException(nameRule);
        }
        #endregion

        #region Get methods
        public Expression<Func<TFrom, TTo>> GetFirstRule<TFrom, TTo>()
        {
            throw new NotImplementedException();
        }

        public Expression<Func<TFrom, TTo>> GetRule<TFrom, TTo>(string nameRule)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Expression<Func<TFrom, TTo>>> GetRules<TFrom, TTo>()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Exist and Delete methods
        public bool RuleExist<TFrom, TTo>()
        {
            throw new NotImplementedException();
        }

        public bool TryDeleteRule<TFrom, TTo>()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private methods

        #endregion
    }
}
