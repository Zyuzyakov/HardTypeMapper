using System;
using System.Linq.Expressions;

namespace Interfaces.CollectionRules
{
    public interface IRulesAdd
    {
        IRulesAdd AddRule<TFrom, TTo>(Expression<Func<IRulesGet, TFrom, TTo>> expressionMaping, string nameRule = null);

        IRulesAdd AddRule<TFrom1, TFrom2, TTo>(Expression<Func<IRulesGet, TFrom1, TFrom2, TTo>> expressionMaping, string nameRule = null);
    }
}
