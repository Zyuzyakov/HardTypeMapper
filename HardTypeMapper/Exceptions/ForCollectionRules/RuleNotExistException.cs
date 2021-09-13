using System;

namespace Exceptions.ForCollectionRules
{
    public class RuleNotExistException : Exception
    {
        public RuleNotExistException(Type typeRule)
           : base($"Правило <{typeRule.FullName}> не существует в коллекции правил ICollectionRules.") { }

        public RuleNotExistException(string nameRule)
           : base($"Имя <{nameRule}> правила не существует в коллекции правил ICollectionRules.") { }
    }
}
