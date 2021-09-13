﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Interfaces
{
    public interface ICollectionRules
    {
        ICollectionRules AddRule<TFrom, TTo>(Expression<Func<TFrom, TTo>> expressionMaping);

        ICollectionRules AddRule<TFrom, TTo>(Expression<Func<TFrom, TTo>> expressionMaping, string nameRule);

        Expression<Func<TFrom, TTo>> GetFirstRule<TFrom, TTo>();

        Expression<Func<TFrom, TTo>> GetRule<TFrom, TTo>(string nameRule);

        IEnumerable<Expression<Func<TFrom, TTo>>> GetRules<TFrom, TTo>();

        bool TryDeleteRule<TFrom, TTo>();

        bool RuleExist<TFrom, TTo>();
    }
}