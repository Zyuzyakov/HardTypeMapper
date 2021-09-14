using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Interfaces.CollectionRules
{
    public interface ICollectionRulesOneIn
    {
        ICollectionRulesOneIn AddRule<TFrom, TTo>(Expression<Func<ICollectionRulesOneIn, TFrom, TTo>> expressionMaping, string nameRule = null);

        Expression<Func<ICollectionRulesOneIn, TFrom, TTo>> GetAnyRule<TFrom, TTo>();

        Expression<Func<ICollectionRulesOneIn, TFrom, TTo>> GetRule<TFrom, TTo>(string nameRule);

        IEnumerable<Expression<Func<ICollectionRulesOneIn, TFrom, TTo>>> GetRules<TFrom, TTo>();

        void DeleteRule<TFrom, TTo>(string nameRule = null);

        bool RuleExist<TFrom, TTo>(string nameRule = null);
    }
}
