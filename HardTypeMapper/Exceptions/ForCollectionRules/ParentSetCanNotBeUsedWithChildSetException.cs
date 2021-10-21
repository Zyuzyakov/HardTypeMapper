using System;

namespace Exceptions.ForCollectionRules
{
    public class ParentSetCanNotBeUsedWithChildSetException : Exception
    {
        public ParentSetCanNotBeUsedWithChildSetException(Type childType, Type parentType)
            : base($"Правило мапинга для дочернего типа <{childType.FullName}> имеет входящие параметры, которые не подойдут для правила мапинга родительского типа <{parentType.FullName}>.") { }
    }
}
