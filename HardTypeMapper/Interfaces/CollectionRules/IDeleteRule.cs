namespace Interfaces.CollectionRules
{
    public interface IDeleteRule
    {
        void DeleteRule<TFrom, TTo>(string nameRule = null);
    }
}
