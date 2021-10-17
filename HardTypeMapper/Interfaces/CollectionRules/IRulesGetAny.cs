using Interfaces.MapMethods;
using System;

namespace Interfaces.CollectionRules
{
    public interface IRulesGetAny
    {
        Action<IMapMethods, TFrom, TTo> GetAnyRule<TFrom, TTo>();
    }
}
