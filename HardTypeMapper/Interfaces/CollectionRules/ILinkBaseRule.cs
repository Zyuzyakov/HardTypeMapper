using System;

namespace Interfaces.CollectionRules
{
    public interface ILinkBaseRule
    {
        ILinkBaseRule AddParentMap<TChildType, TParentType>(string nameRule = null) 
            where TParentType : new() 
            where TChildType : new();
    }
}
