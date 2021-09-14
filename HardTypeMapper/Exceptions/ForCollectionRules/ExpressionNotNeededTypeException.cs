using System;

namespace Exceptions.ForCollectionRules
{
    public class ExpressionNotNeededTypeException : Exception
    {
        public ExpressionNotNeededTypeException(string neededType)
            : base($"Дерево выражений не является типом <{neededType}>.") { }
    }
}
