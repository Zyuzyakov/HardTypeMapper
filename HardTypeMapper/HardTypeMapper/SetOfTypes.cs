using System;
using System.Collections.Generic;
using System.Linq;

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

        public SetOfTypes(string nameRule, params Type[] inTypes)
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

        public override bool Equals(object obj)
        {          
            if (!(obj is SetOfTypes<TOutType>))
                return false;

            var equalsObj = (SetOfTypes<TOutType>)obj;

            if (InTypes.ToList().Count != equalsObj.InTypes.ToList().Count)
                return false;

            if (SetName != equalsObj.SetName)
                return false;

            if (GetOutTypeParam() != equalsObj.GetOutTypeParam())
                return false;

            var equalsObjInTypes = equalsObj.InTypes.ToList();

            foreach (var itemIn in InTypes)
                if (!equalsObjInTypes.Contains(itemIn))
                    return false;

            return true;
        }
    }
}
