using System;

namespace Models.IncludeModels
{
    // Информация о заинклуженном свойстве
    internal class IncludeProp : IEquatable<IncludeProp>
    {
        /// <summary>
        /// Тип класса в который заинклужено
        /// </summary>
        public Type ClassInclude { get; set; }
        /// <summary>
        /// Свойство в которое заинклужено
        /// </summary>
        public string PropertyInclude { get; set; }
        /// <summary>
        /// Тип инклюда (тип свойства в которое заинклюжено) todo от этого свойства возможно можно избавиться
        /// </summary>
        public Type TypeInclude { get; set; } 

        public IncludeProp() { }

        public IncludeProp(Type classInclude)
        {
            ClassInclude = classInclude;
        }

        public IncludeProp(Type classInclude, string propertyInclude, Type typeInclude) : this(classInclude)
        {
            TypeInclude = typeInclude;
            PropertyInclude = propertyInclude;
        }

        public IncludeProp SetPropertyInclude(string propertyInclude)
        {
            var newIncludeInfo = Clone();

            newIncludeInfo.PropertyInclude = propertyInclude;

            return newIncludeInfo;
        }

        public IncludeProp Clone()
        {
            return new IncludeProp(ClassInclude, PropertyInclude, TypeInclude);
        }

        public bool Equals(IncludeProp other)
        {
            bool classEqual = ClassInclude.FullName == other.ClassInclude.FullName;

            bool propertyuEqual = PropertyInclude == other.PropertyInclude;

            bool typeEqual = TypeInclude == other.TypeInclude;

            if (classEqual && propertyuEqual && typeEqual)
                return true;
            else return false;
        }
    }
}
