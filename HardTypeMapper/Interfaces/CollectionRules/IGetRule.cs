using Interfaces.MapMethods;
using System;

namespace Interfaces.CollectionRules
{
    public interface IGetRule
    {
        Action<IMapMethods, TFrom, TTo> GetRule<TFrom, TTo>(string nameRule = null);

        Action<IMapMethods, TFrom, TTo> GetAnyRule<TFrom, TTo>();

        Action<IMapMethods, TFrom, TTo> GetRuleWithParent<TFrom, TTo>(string nameRule = null);
    }
}
