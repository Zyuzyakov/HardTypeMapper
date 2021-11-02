using System;

namespace Exceptions.ForCollectionRules
{
    public class InParamsNotMatchException : Exception
    {
        public InParamsNotMatchException(Type parentTypeOut, Type childTypeOut)
            : base($"Входные параметры родительского правила для типа {parentTypeOut.FullName} "
                  + $"не соответствуют входным параметрам правила дочернего типа {childTypeOut.FullName}.") { }
    }
}
