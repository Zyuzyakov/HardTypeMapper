using Interfaces.CollectionRules;
using Interfaces.MapMethods;
using System.Collections.Generic;
using System.Linq;

namespace HardTypeMapper
{
    public class HardMapper : IMapMethods
    {
        private readonly ICollectionRules _collectionRules; 

        public HardMapper(ICollectionRules collectionRules)
        {
            _collectionRules = collectionRules ?? throw new System.ArgumentNullException(nameof(collectionRules));
        }

        #region MapObjects methods
        public TTo Map<TFrom, TTo>(TFrom from, string nameRule = null)
        {
            throw new System.NotImplementedException();
        }

        public TTo Map<TFrom1, TFrom2, TTo>(TFrom1 from1, TFrom2 from2, string nameRule = null)
        {
            throw new System.NotImplementedException();
        }
        #endregion

        public IEnumerable<TTo> Map<TFrom, TTo>(IEnumerable<TFrom> from, string nameRule = null)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<TTo> Map<TFrom, TTo>(IQueryable<TFrom> from, string nameRule = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
