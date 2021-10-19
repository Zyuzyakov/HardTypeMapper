using System.Collections.Generic;

namespace HardTypeMapper.QuerybleMapping
{
    internal class QueryMappingInfo
    {
        public List<IncludeProp> IncludeProps { get; set; } // что было заинклюжено настоящими инклудами
        public IncludeProp CurrentInfoInclude { get; set; } // в текущем мапере что инклудится и куда
        public bool MapQuery { get; set; } = false; // изначально мапится квери
        public bool? OnlyNotDeleted { get; set; } // параметр deleted расспространяется на все заинклуженные сущности

        public QueryMappingInfo(List<IncludeProp> infoIncludes, IncludeProp currentInfoInclude, bool mapNotQuery, bool? onlyNotDeleted)
            : this(currentInfoInclude, mapNotQuery, onlyNotDeleted)
        {
            IncludeProps = infoIncludes;
        }

        public QueryMappingInfo(IncludeProp currentInfoInclude, bool mapNotQuery, bool? onlyNotDeleted)
        {
            IncludeProps = new List<IncludeProp>();

            MapQuery = mapNotQuery;
            CurrentInfoInclude = currentInfoInclude;
            OnlyNotDeleted = onlyNotDeleted;
        }

        public QueryMappingInfo SetPropertyInclude(string propertyInclude)
        {
            var retObj = Clone();
            retObj.CurrentInfoInclude.PropertyInclude = propertyInclude;

            return retObj;
        }

        public QueryMappingInfo Clone()
        {
            var cloneThis = new QueryMappingInfo(IncludeProps, CurrentInfoInclude.Clone(), MapQuery, OnlyNotDeleted);

            return cloneThis;
        }
    }
}
