using System.Collections.Generic;
using System.Linq;

namespace Interfaces
{
    public interface IMapMethods
    {
        TTo Map<TFrom, TTo>(TFrom from);

        IEnumerable<TTo> Map<TFrom, TTo>(IEnumerable<TFrom> from);

        IQueryable<TTo> Map<TFrom, TTo>(IQueryable<TFrom> from);
    }
}
