using System;

namespace Interfaces.CollectionRules
{
    public interface ISetOfRule
    {
        Type GetOutTypeParam();

        Type[] GetInTypeParams();

        string SetName { get; init; }

        bool Equals(object obj, bool withName);

        IParentRule ParentRule { get; set; }
    }
}
