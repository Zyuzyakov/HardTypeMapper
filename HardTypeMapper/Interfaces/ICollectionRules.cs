using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Interfaces
{
    public interface ICollectionRules
    {
        ICollectionRules AddRule<TFrom, TTo>(Expression<Func<TFrom, TTo>> expressionMaping);

        ICollectionRules AddRule<TFrom, TTo>(Expression<Func<TFrom, TTo>> expressionMaping, string nameRule);

        Expression<Func<TFrom, TTo>> GetFirstRule<TFrom, TTo>();

        Expression<Func<TFrom, TTo>> GetRule<TFrom, TTo>(string nameRule);

        IEnumerable<Expression<Func<TFrom, TTo>>> GetRules<TFrom, TTo>();

        void DeleteRule<TFrom, TTo>();

        void DeleteRule<TFrom, TTo>(string nameRule);

        bool RuleExist<TFrom, TTo>();

        bool RuleExist<TFrom, TTo>(string nameRule);
    }
}
