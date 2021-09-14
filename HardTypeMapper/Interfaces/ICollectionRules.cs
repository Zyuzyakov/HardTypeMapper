using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Interfaces
{
    public interface ICollectionRules
    {
        ICollectionRules AddRule<TFrom, TTo>(Expression<Func<ICollectionRules, TFrom, TTo>> expressionMaping, string nameRule = null);

        ICollectionRules AddRule<TFrom1, TFrom2, TTo>(Expression<Func<ICollectionRules, TFrom1, TFrom2, TTo>> expressionMaping, string nameRule = null);

        ICollectionRules AddRule<TFrom1, TFrom2, TFrom3, TTo>(Expression<Func<ICollectionRules, TFrom1, TFrom2, TFrom3, TTo>> expressionMaping, string nameRule = null);



        Expression<Func<ICollectionRules, TFrom, TTo>> GetAnyRule<TFrom, TTo>();

        Expression<Func<ICollectionRules, TFrom, TTo>> GetRule<TFrom, TTo>(string nameRule);

        IEnumerable<Expression<Func<ICollectionRules, TFrom, TTo>>> GetRules<TFrom, TTo>();



        void DeleteRule<TFrom, TTo>(string nameRule = null);

        bool RuleExist<TFrom, TTo>(string nameRule = null);
    }
}
