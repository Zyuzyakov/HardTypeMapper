using System;
using System.Linq.Expressions;

namespace Exceptions.ForCollectionRules
{
    public class ExpressionNotNeededType : Exception
    {
        public ExpressionNotNeededType(Expression expr) 
            : base($"Дерево выражений <{expr.GetType().FullName}> не имеет требуемого формата Expression<Func<TFrom, TTo>>.") { }
    }
}
