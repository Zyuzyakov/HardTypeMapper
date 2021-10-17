using System;

namespace Exceptions.ForCollectionRules
{
    public class DelegateNotNeededTypeException : Exception
    {
        public DelegateNotNeededTypeException(string neededType)
            : base($"ДЕлегат не является типом <{neededType}>.") { }
    }
}
