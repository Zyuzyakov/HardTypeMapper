using Interfaces.CollectionRules;
using Interfaces.MapMethods;
using Models.HardMapperModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace HardTypeMapper
{
    public class HardMapper : IMapMethods
    {
        private readonly List<MapInfo> _mapInfos;
        private readonly ICollectionRules _collectionRules;

        public HardMapper(ICollectionRules collectionRules)
        {
            _mapInfos = new List<MapInfo>();
            _collectionRules = collectionRules ?? throw new System.ArgumentNullException(nameof(collectionRules));
        }

        #region MapObjects methods
        public TTo Map<TFrom, TTo>(TFrom from)
        {
            if (from is null)
                return default;

            var fromThisClass = CallFromThisClass();

            var rule = _collectionRules.GetRule<TFrom, TTo>();

            if (ContinueProcessMap(rule, fromThisClass, false))
                return rule.Compile()(this, from);

            return default;
        }
        public TTo Map<TFrom, TTo>(TFrom from, string nameRule)
        {
            throw new System.NotImplementedException();
        }

        public TTo Map<TFrom1, TFrom2, TTo>(TFrom1 from1, TFrom2 from2)
        {
            throw new System.NotImplementedException();
        }
        public TTo Map<TFrom1, TFrom2, TTo>(TFrom1 from1, TFrom2 from2, string nameRule)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region MapCollection
        public IEnumerable<TTo> Map<TFrom, TTo>(IEnumerable<TFrom> from)
        {
            if (from is null)
                yield break;

            var fromThisClass = CallFromThisClass();

            var rule = _collectionRules.GetRule<TFrom, TTo>();

            if (ContinueProcessMap(rule, fromThisClass, true))
            {
                var ruleFunc = rule.Compile();

                foreach (var item in from)
                    yield return ruleFunc(this, item);
            }
        }
        public IEnumerable<TTo> Map<TFrom, TTo>(IEnumerable<TFrom> from, string nameRule)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region MapQuery
        public IQueryable<TTo> Map<TFrom, TTo>(IQueryable<TFrom> from, string nameRule = null)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        #region Class private methods
        private bool CallFromThisClass()
        {
            var st = new StackTrace();
            var frames = st.GetFrames();

            for (int i = 2; i < frames.Length; i++)
            {           
                var method = frames[i].GetMethod();

                var classType = method.DeclaringType;

                if (classType == this.GetType())
                    return true;
            }

            return false;
        }

        private bool ContinueProcessMap(Expression rule, bool fromThisClass, bool callFromCollection)
        {
            var mapInfo = GetMapInfo(rule);

            if (mapInfo is null)
                _mapInfos.Add(new MapInfo(fromThisClass, callFromCollection, rule));
            else if (callFromCollection && fromThisClass)
                return false;

            return true;
        }
        private MapInfo GetMapInfo(Expression expr) => _mapInfos.FirstOrDefault(x => x.Expr == expr);
        #endregion
    }
}
