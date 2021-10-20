using Interfaces.CollectionRules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HardTypeMapper.Models.CollectionModels
{
    public struct SetOfTypes<TOutType> : ISetOfTypes
    {
        public SetOfTypes(Type inType, string nameRule = null)
        {
            ParentRule = null;
            SetName = nameRule ?? string.Empty;
            inTypes = new HashSet<Type>
            {
                inType
            };
        }

        public SetOfTypes(string nameRule, params Type[] inTypes)
        {
            ParentRule = null;
            SetName = nameRule ?? string.Empty;
            this.inTypes = new HashSet<Type>();
            foreach (var param in inTypes)
                this.inTypes.Add(param);
        }

        public readonly string SetName { get; init; }

        private HashSet<Type> inTypes { get; set; }

        public IEnumerable<Type> InTypes
        {
            get
            {
                var arrayTypes = new Type[inTypes?.Count ?? 0];

                inTypes?.CopyTo(arrayTypes);

                return arrayTypes;
            }
        }

        public IParentRule ParentRule { get; set; }

        public Type GetOutTypeParam()
        {
            return typeof(TOutType);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
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

        public bool Equals(object obj, bool withName)
        {
            if (withName)
                return Equals(obj);
            else return EqualsWithOutName(obj);
        }

        private bool EqualsWithOutName(object obj)
        {
            if (obj is not SetOfTypes<TOutType>)
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

        public  Type[] GetInTypeParams()
        {
            return InTypes.ToArray();
        }
    }
}
