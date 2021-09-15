namespace Interfaces.MapMethods
{
    public interface IMapObject
    {
        TTo Map<TFrom, TTo>(TFrom from, string nameRule = null);

        TTo Map<TFrom1, TFrom2, TTo>(TFrom1 from1, TFrom2 from2, string nameRule = null);
    }
}
