using Interfaces.MapMethods;
using System;
using System.Linq.Expressions;

namespace Interfaces.CollectionRules
{
    public interface IRulesGetAny
    {
        Expression<Func<IMapMethods, TFrom, TTo>> GetAnyRule<TFrom, TTo>();
        Expression<Func<IMapMethods, TFrom1, TFrom2, TTo>> GetAnyRule<TFrom1, TFrom2, TTo>();
    }
}
