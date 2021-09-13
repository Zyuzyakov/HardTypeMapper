using System;

namespace Exceptions.ForCollectionRules
{
    public class RuleExistException : Exception
    {
        public RuleExistException(Type typeRule) 
            : base($"Правило <{typeRule.FullName}> уже существует в коллекции правил без имени.") { }

        public RuleExistException(string nameRule)
           : base($"Имя <{nameRule}> правила уже существует в коллекции правил с именами.") { }
    }
}
