﻿using System.Collections.Generic;
using System.Linq;

namespace Interfaces.Includes
{
    public interface IIncludeInfo
    {
        IList<IncludeProps> GetIncludes(IQueryable query);
    }
}