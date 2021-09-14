using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HardTypeMapper
{
    public class CollectionRules : ICollectionRules
    {
        #region Сlass variables
        protected Dictionary<SetOfTypes<object>, Expression> dictRuleExpression;
        #endregion

        #region Class constructors
        public CollectionRules() 
        {
            dictRuleExpression = new Dictionary<SetOfTypes<object>, Expression>();
        }
        #endregion

        #region Add methods
        public ICollectionRules AddRule<TFrom, TTo>(Expression<Func<ICollectionRules, TFrom, TTo>> expressionMaping, string nameRule = null)
        {
            throw new NotImplementedException();
        }

        public ICollectionRules AddRule<TFrom1, TFrom2, TTo>(Expression<Func<ICollectionRules, TFrom1, TFrom2, TTo>> expressionMaping, string nameRule = null)
        {
            throw new NotImplementedException();
        }

        public ICollectionRules AddRule<TFrom1, TFrom2, TFrom3, TTo>(Expression<Func<ICollectionRules, TFrom1, TFrom2, TFrom3, TTo>> expressionMaping, string nameRule = null)
        {
            throw new NotImplementedException();
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
        
        #endregion
    }
}
