using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace HardTypeMapper.IQuerybleMapping
{
    // Обходит дерево выражений и находит все Include/ThenInclude
    public class ExpressionIncludeVisitor : ExpressionVisitor
    {
        private List<IncludeProps> includeTypes = new List<IncludeProps>();
        private IncludeProps includeInfo = new IncludeProps();
        private bool nowInIncludeCall = false;

        private const string INCLUDE = "Include";
        private const string THENINCLUDE = "ThenInclude";

        public List<IncludeProps> GetIncludeTypesAndClear()
        {
            nowInIncludeCall = false;

            includeInfo = new IncludeProps();

            var retutnList = includeTypes;

            includeTypes = new List<IncludeProps>();

            return retutnList;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Object != null)
                Visit(node.Object);

            if (nowInIncludeCall)
                Visit(node.Arguments[0]);
            else if (node.Method.Name == INCLUDE || node.Method.Name == THENINCLUDE)
            {
                nowInIncludeCall = true;

                var first = true;

                foreach (var arg in node.Arguments)
                    if (first)
                        first = false;
                    else
                        Visit(arg);
            }

            return base.VisitMethodCall(node);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.Operand != null && nowInIncludeCall)
                Visit(node.Operand);

            return node;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            if (node.Body != null && nowInIncludeCall)
            {
                includeInfo.TypeInclude = node.ReturnType; //3

                Visit(node.Body);
            }

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (nowInIncludeCall)
            {
                includeInfo.PropertyInclude = node.Member.Name; //2

                includeInfo.ClassInclude = GetClassInclude(node.Member); //1

                AddIncludeInfo();
            }

            return node;
        }

        protected Type GetClassInclude(MemberInfo memberInfo)
        {
            return memberInfo.ReflectedType;
        }

        private void AddIncludeInfo()
        {
            includeTypes.Add(includeInfo);
            nowInIncludeCall = false;
            includeInfo = new IncludeProps();
        }
    }
}
