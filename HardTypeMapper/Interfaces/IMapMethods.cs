using System.Collections.Generic;
using System.Linq;

namespace Interfaces
{
    public interface IMapMethods
    {
        TTo Map<TFrom, TTo>(TFrom from, string nameRule = null);

        IEnumerable<TTo> Map<TFrom, TTo>(IEnumerable<TFrom> from, string nameRule = null);

        IQueryable<TTo> Map<TFrom, TTo>(IQueryable<TFrom> from, string nameRule = null);
    }
}
