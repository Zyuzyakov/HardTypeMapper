namespace Interfaces.CollectionRules
{
    public interface ILinkBaseRule
    {
        void AddParentMapIfExistRule();
        void AddParentMap(string nameRule = null);
    }
}
