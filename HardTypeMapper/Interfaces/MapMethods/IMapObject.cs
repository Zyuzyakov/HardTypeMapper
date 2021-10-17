namespace Interfaces.MapMethods
{
    public interface IMapObject
    {        
        TTo Map<TFrom, TTo>(TFrom from, string nameRule = null) where TTo : new();
    }
}
