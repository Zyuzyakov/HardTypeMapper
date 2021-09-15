namespace Interfaces.CollectionRules
{
    public interface IRulesExist
    {
        bool ExistRule<TFrom, TTo>(string nameRule = null);

        bool ExistRule<TFrom1, TFrom2, TTo>(string nameRule = null);
    }
}
