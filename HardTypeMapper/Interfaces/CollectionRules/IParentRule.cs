using System;

namespace Interfaces.CollectionRules
{
    public interface IParentRule
    {
        Type ParentType { get; set; }

        string RuleName{ get; set; }

        bool ExistParentRule();
    }
}
