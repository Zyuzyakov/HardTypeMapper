namespace Interfaces.CollectionRules
{
    public interface IRulesDelete
    {
        void DeleteRule<TFrom, TTo>(string nameRule = null);

        void DeleteRule<TFrom1, TFrom2, TTo>(string nameRule = null);
    }
}
