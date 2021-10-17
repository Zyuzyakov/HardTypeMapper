namespace Interfaces.CollectionRules
{
    public interface IRulesDelete
    {
        void DeleteRule<TFrom, TTo>(string nameRule = null);
    }
}
