using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Interfaces.CollectionRules
{
    public interface ICollectionRulesTreeIn
    {
        ICollectionRulesOneIn AddRule<TFrom1, TFrom2, TFrom3, TTo>(Expression<Func<ICollectionRulesOneIn, TFrom1, TFrom2, TFrom3, TTo>> expressionMaping, string nameRule = null);

        Expression<Func<ICollectionRulesOneIn, TFrom1, TFrom2, TFrom3, TTo>> GetAnyRule<TFrom1, TFrom2, TFrom3, TTo>();

        Expression<Func<ICollectionRulesOneIn, TFrom1, TFrom2, TFrom3, TTo>> GetRule<TFrom1, TFrom2, TFrom3, TTo>(string nameRule);

        IEnumerable<Expression<Func<ICollectionRulesOneIn, TFrom1, TFrom2, TFrom3, TTo>>> GetRules<TFrom1, TFrom2, TFrom3, TTo>();

        void DeleteRule<TFrom1, TFrom2, TFrom3, TTo>(string nameRule = null);

        bool RuleExist<TFrom1, TFrom2, TFrom3, TTo>(string nameRule = null);
    }
}
