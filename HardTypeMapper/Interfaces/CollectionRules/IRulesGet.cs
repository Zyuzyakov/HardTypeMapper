using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Interfaces.CollectionRules
{
    public interface IRulesGet
    {
        Expression<Func<IRulesGet, TFrom, TTo>> GetAnyRule<TFrom, TTo>();
        Expression<Func<IRulesGet, TFrom, TTo>> GetRule<TFrom, TTo>(string nameRule = null);
        Dictionary<string, Expression<Func<IRulesGet, TFrom, TTo>>> GetRules<TFrom, TTo>();

        Expression<Func<IRulesGet, TFrom1, TFrom2, TTo>> GetAnyRule<TFrom1, TFrom2, TTo>();
        Expression<Func<IRulesGet, TFrom1, TFrom2, TTo>> GetRule<TFrom1, TFrom2, TTo>(string nameRule = null);
        Dictionary<string, Expression<Func<IRulesGet, TFrom1, TFrom2, TTo>>> GetRules<TFrom1, TFrom2, TTo>();
    }
}
