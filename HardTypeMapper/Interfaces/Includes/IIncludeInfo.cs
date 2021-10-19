using System.Collections.Generic;
using System.Linq;

namespace Interfaces.Includes
{
    public interface IIncludeInfo
    {
        IList<IncludeProps> GetIncludes(IQueryable query);

        bool IsInclude(IncludeProps propSearch);

        void AddInclude(IncludeProps prop);
    }
}
