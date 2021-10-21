using System;

namespace Exceptions.ForCollectionRules
{
    public class NotHaveParentSetOfRuleException : Exception
    {
        public NotHaveParentSetOfRuleException(Type parentType, string ruleName)
        : base($"Не найдено правило <{ruleName}> для мапинга для родительского типа {parentType.FullName}.") { }
    }
}
