namespace Interfaces.CollectionRules
{
    public interface IRulesExist
    {
        bool ExistRule<TFrom, TTo>(string nameRule = null);
    }
}
