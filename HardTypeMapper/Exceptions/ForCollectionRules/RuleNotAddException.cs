using System;

namespace Exceptions.ForCollectionRules
{
    public class RuleNotAddException : Exception
    {
        public RuleNotAddException(string nameRule = null)
            : base($"Правило <{nameRule}> не было добавлено в коллекцию.") { }
    }
}
