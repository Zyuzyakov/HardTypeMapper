using System;
using System.Collections.Generic;
using System.Linq;

namespace HardTypeMapper.CollectionRules
{
    public interface ISetOfTypes
    {
        Type GetOutTypeParam();

        string SetName { get; }

        bool Equals(object obj, bool withOutName);
    }

    public struct SetOfTypes<TOutType> : ISetOfTypes
    {
        public SetOfTypes(Type inType, string nameRule = null)
        {
            SetName = nameRule ?? string.Empty;
            _inTypes = new HashSet<Type>();
            _inTypes.Add(inType);
        }

        public SetOfTypes(string nameRule, params Type[] inTypes)
        {
            SetName = nameRule ?? string.Empty;
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
            bool equals = EqualsWithOutName(obj);

            if (equals)
            {
                var equalsObj = (SetOfTypes<TOutType>)obj;

                if (SetName == equalsObj.SetName)
                    return true;
            }

            return false;
        }

        public bool Equals(object obj, bool withOutName)
        {
            if (withOutName)
                return EqualsWithOutName(obj);
            else return Equals(obj);
        }

        private bool EqualsWithOutName(object obj)
        {
            if (!(obj is SetOfTypes<TOutType>))
                return false;

            var equalsObj = (SetOfTypes<TOutType>)obj;

            if (InTypes.ToList().Count != equalsObj.InTypes.ToList().Count)
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
