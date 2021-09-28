using System;

namespace Interfaces.Includes
{
    // Информация о заинклуженном свойстве
    public class IncludeProps
    {
        public Type ClassInclude { get; set; } // Тип класса в который заинклужено
        public string PropertyInclude { get; set; } // Свойство в которое заинклужено
        public Type TypeInclude { get; set; } // Тип инклюда (тип свойства в которое заинклюжено)

        public IncludeProps() { }

        public IncludeProps(Type classInclude)
        {
            ClassInclude = classInclude;
        }

        public IncludeProps(Type classInclude, string propertyInclude, Type typeInclude) : this(classInclude)
        {
            TypeInclude = typeInclude;
            PropertyInclude = propertyInclude;
        }

        public IncludeProps SetPropertyInclude(string propertyInclude)
        {
            var newIncludeInfo = Clone();

            newIncludeInfo.PropertyInclude = propertyInclude;

            return newIncludeInfo;
        }

        public IncludeProps Clone()
        {
            return new IncludeProps(ClassInclude, PropertyInclude, TypeInclude);
        }
    }
}
