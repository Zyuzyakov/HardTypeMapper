using Interfaces.MapMethods;
using System;

namespace Interfaces.CollectionRules
{
    public interface IAddRule
    {
        ILinkBaseRule AddRule<TFrom, TTo>(Action<IMapMethods, TFrom, TTo> actionMaping, string nameRule = null);
    }
}
