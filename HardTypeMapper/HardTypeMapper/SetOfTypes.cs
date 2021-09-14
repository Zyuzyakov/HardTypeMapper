using System;
using System.Collections.Generic;

namespace HardTypeMapper
{
    public struct SetOfTypes<TOutType>
    {
        public SetOfTypes(Type inType, string nameRule = null)
        {
            SetName = nameRule;
            _inTypes = new HashSet<Type>();
            _inTypes.Add(inType);
        }

        public SetOfTypes(string nameRule = null, params Type[] inTypes)
        {
            SetName = nameRule;
            _inTypes = new HashSet<Type>();
            foreach (var param in inTypes)
                _inTypes.Add(param);
        }

        public readonly string SetName { get; }

        private HashSet<Type> _inTypes { get; set; }

        public IEnumerable<Type> InTypes
        {
            get
            {
                var arrayTypes = new Type[_inTypes?.Count ?? 0];

                _inTypes?.CopyTo(arrayTypes);

                return arrayTypes;
            }
        }

        public Type GetOutTypeParam()
        {
            return typeof(TOutType);
        }
    }
}
