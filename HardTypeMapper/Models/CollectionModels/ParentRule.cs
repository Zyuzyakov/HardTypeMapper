using Interfaces.CollectionRules;
using System;

namespace Models.CollectionModels
{
    public struct ParentRule : IParentRule
    {
        public ParentRule(Type parentType, Type childType, string ruleName)
        {
            ChildType = childType;
            ParentType = parentType;
            RuleName = ruleName;
        }

        public Type ParentType { get; set; }

        public Type ChildType { get; set; }

        public string RuleName { get; set; }
    }
}
