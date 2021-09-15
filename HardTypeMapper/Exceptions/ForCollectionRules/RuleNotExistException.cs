using System;
using System.Linq;

namespace Exceptions.ForCollectionRules
{
    public class RuleNotExistException : Exception
    {
        public RuleNotExistException(Type typeRule)
           : base($"Правило <{typeRule.FullName}> не существует в коллекции правил ICollectionRules.") { }

        public RuleNotExistException(string nameRule)
           : base($"Имя <{nameRule}> правила не существует в коллекции правил ICollectionRules.") { }

        public RuleNotExistException(string nameRule, Type outType, Type[] inTyper)
           : base($"Имя <{nameRule}> правила не существует в коллекции правил ICollectionRules."
                 + $"Выходной параметр типа <{outType.FullName}>," 
                 + $"входные параметры типов: <{String.Join(',', inTyper.Select(x => x.FullName))}>.") { }
    }
}
