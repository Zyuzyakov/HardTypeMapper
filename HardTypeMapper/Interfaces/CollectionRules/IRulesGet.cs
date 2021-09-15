using Interfaces.MapMethods;
using System;
using System.Linq.Expressions;

namespace Interfaces.CollectionRules
{
    public interface IRulesGet
    {
        Expression<Func<IMapMethods, TFrom, TTo>> GetRule<TFrom, TTo>(string nameRule = null);
        Expression<Func<IMapMethods, TFrom1, TFrom2, TTo>> GetRule<TFrom1, TFrom2, TTo>(string nameRule = null);
    }
}
