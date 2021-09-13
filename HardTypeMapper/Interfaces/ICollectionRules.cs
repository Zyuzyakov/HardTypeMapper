using System;
using System.Linq.Expressions;

namespace Interfaces
{
    public interface ICollectionRules
    {
        ICollectionRules AddRule<TFrom, TTo>(Expression<Func<TFrom, TTo>> expressionMaping);

        Expression<Func<TFrom, TTo>> GetRule<TFrom, TTo>();

        bool TryDeleteRule<TFrom, TTo>();

        bool RuleExist<TFrom, TTo>();

    }
}
