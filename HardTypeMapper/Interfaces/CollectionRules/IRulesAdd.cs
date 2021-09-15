using Interfaces.MapMethods;
using System;
using System.Linq.Expressions;

namespace Interfaces.CollectionRules
{
    public interface IRulesAdd
    {
        IRulesAdd AddRule<TFrom, TTo>(Expression<Func<IMapMethods, TFrom, TTo>> expressionMaping, string nameRule = null);

        IRulesAdd AddRule<TFrom1, TFrom2, TTo>(Expression<Func<IMapMethods, TFrom1, TFrom2, TTo>> expressionMaping, string nameRule = null);
    }
}
