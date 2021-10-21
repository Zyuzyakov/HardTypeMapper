using System;

namespace Exceptions.ForCollectionRules
{
    public class NotHaveParentClassException : Exception
    {
        public NotHaveParentClassException(Type type) 
            : base($"Тип {type.FullName} не имеет базового класса, за исключением Object.") { }
    }
}
