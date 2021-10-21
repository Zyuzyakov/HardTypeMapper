using System;

namespace Interfaces.CollectionRules
{
    public interface IParentRule
    {
        Type ChildType { get; set; }

        Type ParentType { get; set; }

        string RuleName{ get; set; }
    }
}
