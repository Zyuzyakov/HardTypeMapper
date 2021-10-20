using Interfaces.CollectionRules;
using System;

namespace Models.CollectionModels
{
    public struct ParentRule : IParentRule
    {
        public ParentRule(Type parentType, string ruleName)
        {
            ParentType = parentType;
            RuleName = ruleName;
        }

        public Type ParentType { get; set; }
        public string RuleName { get; set; }

        public bool ExistParentRule()
        {
            return !(ParentType is null);
        }
    }
}
