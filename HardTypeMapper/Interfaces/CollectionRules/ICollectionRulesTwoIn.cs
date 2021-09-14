using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Interfaces.CollectionRules
{
    public interface ICollectionRulesTwoIn
    {
        ICollectionRulesOneIn AddRule<TFrom1, TFrom2, TTo>(Expression<Func<ICollectionRulesOneIn, TFrom1, TFrom2, TTo>> expressionMaping, string nameRule = null);

        Expression<Func<ICollectionRulesOneIn, TFrom1, TFrom2, TTo>> GetAnyRule<TFrom1, TFrom2, TTo>();

        Expression<Func<ICollectionRulesOneIn, TFrom1, TFrom2, TTo>> GetRule<TFrom1, TFrom2, TTo>(string nameRule);

        IEnumerable<Expression<Func<ICollectionRulesOneIn, TFrom1, TFrom2, TTo>>> GetRules<TFrom1, TFrom2, TTo>();

        void DeleteRule<TFrom1, TFrom2, TTo>(string nameRule = null);

        bool RuleExist<TFrom1, TFrom2, TTo>(string nameRule = null);
    }
}
