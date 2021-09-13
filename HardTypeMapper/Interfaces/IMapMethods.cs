using System.Collections.Generic;
using System.Linq;

namespace Interfaces
{
    public interface IMapMethods
    {
        TTo Map<TFrom, TTo>(TFrom from);

        TTo Map<TFrom, TTo>(TFrom from, string nameRule);

        IEnumerable<TTo> Map<TFrom, TTo>(IEnumerable<TFrom> from);

        IEnumerable<TTo> Map<TFrom, TTo>(IEnumerable<TFrom> from, string nameRule);

        IQueryable<TTo> Map<TFrom, TTo>(IQueryable<TFrom> from);

        IQueryable<TTo> Map<TFrom, TTo>(IQueryable<TFrom> from, string nameRule);
    }
}
