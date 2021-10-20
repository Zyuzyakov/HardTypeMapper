using Interfaces.MapMethods;
using System;

namespace Interfaces.CollectionRules
{
    public interface IGetAnyRule
    {
        Action<IMapMethods, TFrom, TTo> GetAnyRule<TFrom, TTo>();
    }
}
