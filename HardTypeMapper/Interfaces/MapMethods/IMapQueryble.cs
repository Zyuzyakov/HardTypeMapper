using System.Linq;

namespace Interfaces.MapMethods
{
    public interface IMapQueryble
    {
        IQueryable<TTo> Map<TFrom, TTo>(IQueryable<TFrom> from, string nameRule = null) where TTo : new();
    }
}
