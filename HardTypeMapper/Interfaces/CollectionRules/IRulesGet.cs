using Interfaces.MapMethods;
using System;

namespace Interfaces.CollectionRules
{
    public interface IRulesGet
    {
        Action<IMapMethods, TFrom, TTo> GetRule<TFrom, TTo>(string nameRule = null);
    }
}
