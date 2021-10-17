using Interfaces.Includes;
using System.Linq;

namespace Interfaces.MapMethods
{
    public interface IMapQueryble
    {
        IQueryable<TTo> Map<TFrom, TTo>(IQueryable<TFrom> from, IIncludeInfo inculdeInfo = null, string nameRule = null) where TTo : new();
    }
}
