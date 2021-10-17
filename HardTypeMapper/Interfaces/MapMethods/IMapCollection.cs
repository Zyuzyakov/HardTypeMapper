using System.Collections.Generic;

namespace Interfaces.MapMethods
{
    public interface IMapCollection
    {
        IEnumerable<TTo> Map<TFrom, TTo>(IEnumerable<TFrom> from, string nameRule = null) where TTo : new(); 
    }
}
