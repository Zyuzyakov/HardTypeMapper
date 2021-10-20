namespace Interfaces.CollectionRules
{
    public interface IExistRule
    {
        bool ExistRule<TFrom, TTo>(string nameRule = null);
    }
}
