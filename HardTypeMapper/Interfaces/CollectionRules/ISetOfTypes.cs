using System;
using System.Collections.Generic;

namespace Interfaces.CollectionRules
{
    public interface ISetOfTypes
    {
        Type GetOutTypeParam();

        Type[] GetInTypeParams();

        string SetName { get; init; }

        bool Equals(object obj, bool withName);

        List<IParentRule> ParentRules { get; set; }
    }
}
