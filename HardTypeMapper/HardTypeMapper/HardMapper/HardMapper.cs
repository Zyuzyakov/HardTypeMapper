using Interfaces.CollectionRules;
using Interfaces.Includes;
using Interfaces.MapMethods;
using Models.HardMapperModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HardTypeMapper
{
    public class HardMapper : IMapMethods
    {
        private readonly List<MapInfo> _mapInfos;
        private readonly ICollectionRules _collectionRules;
        protected IIncludeInfo _includeInfo;

        public HardMapper(ICollectionRules collectionRules)
        {
            _mapInfos = new List<MapInfo>();
            _collectionRules = collectionRules ?? throw new ArgumentNullException(nameof(collectionRules));
        }

        #region MapObjects methods
        public TTo Map<TFrom, TTo>(TFrom from, string nameRule = null) where TTo : new()
        {
            if (from is null)
                return default;

            var fromThisClass = CallFromThisClass(false);

            var rule = _collectionRules.GetRule<TFrom, TTo>(nameRule);           

            if (ContinueProcessMap(rule, fromThisClass, false))
            {
                var retObj = new TTo();

                rule(this, from, retObj);

                return retObj;            
            }

            return default;
        }
        #endregion

        #region MapCollection
        public IEnumerable<TTo> Map<TFrom, TTo>(IEnumerable<TFrom> from, string nameRule = null) where TTo : new()
        {
            if (from is null)
                yield break;

            var fromThisClass = CallFromThisClass(true);

            var rule = _collectionRules.GetRule<TFrom, TTo>(nameRule);

            if (ContinueProcessMap(rule, fromThisClass, true))
            {
                var ruleFunc = rule;

                foreach (var item in from)
                    if (item is not null)
                    {
                        var retObj = new TTo();

                        ruleFunc(this, item, retObj);

                        yield return retObj;
                    }
            }
        }
        #endregion

        #region MapQuery
        public IQueryable<TTo> Map<TFrom, TTo>(IQueryable<TFrom> from, IIncludeInfo inculdeInfo = null, string nameRule = null) where TTo : new()
        {
            if (from is null)
                throw new ArgumentNullException(nameof(from));

            if (inculdeInfo != null)
                _includeInfo = inculdeInfo;

            var rule = _collectionRules.GetRule<TFrom, TTo>(nameRule);

            _mapInfos.Add(new MapInfo(true, true, rule));

            var mapMethods = this;

            Func<IMapMethods, TFrom, TTo> funcSelect = (mm, from) => { var to = new TTo(); rule(mapMethods, from, to); return to; };

            return from.Select(x => funcSelect(mapMethods, x));
        }
        #endregion

        #region Class private methods
        private bool CallFromThisClass(bool fromCollection)
        {
            var st = new StackTrace();
            var frames = st.GetFrames();

            int startFrom = fromCollection ? 1 : 2;

            for (int i = startFrom; i < frames.Length; i++)
            {           
                var method = frames[i].GetMethod();

                var classType = method.DeclaringType;

                if (classType == this.GetType())
                    return true;
            }

            return false;
        }

        private bool ContinueProcessMap(Delegate rule, bool fromThisClass, bool callFromCollection)
        {
            var mapInfo = GetMapInfo(rule);

            if (mapInfo is null)
                _mapInfos.Add(new MapInfo(!fromThisClass, callFromCollection, rule));
            else if (fromThisClass && mapInfo.ItRootMapper)
                return false;
            else if (fromThisClass && mapInfo.FirstCallFromCollection != callFromCollection)
                return false;
            else if (!callFromCollection && mapInfo.FirstCallFromCollection)
                return false;

            return true;
        }

        private MapInfo GetMapInfo(Delegate expr) => _mapInfos.FirstOrDefault(x => x.Action == expr);
        #endregion
    }
}
