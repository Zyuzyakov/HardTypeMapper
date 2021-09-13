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

        protected Dictionary<Type, Expression> dictRuleExpressionWithName;
        #endregion

        #region Class constructors
        public CollectionRules() { }
        #endregion

        #region Add methods
        public ICollectionRules AddRule<TFrom, TTo>(Expression<Func<TFrom, TTo>> expressionMaping)
        {
            throw new NotImplementedException();
        }

        public ICollectionRules AddRule<TFrom, TTo>(Expression<Func<TFrom, TTo>> expressionMaping, string nameRule)
        {
            throw new NotImplementedException();
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
