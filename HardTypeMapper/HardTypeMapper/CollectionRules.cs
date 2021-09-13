using Exceptions.ForCollectionRules;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HardTypeMapper
{
    public class CollectionRules : ICollectionRules
    {
        #region Сlass variables
        protected Dictionary<Type, Expression> dictRuleExpressionWithOutName;

        protected Dictionary<string, Expression> dictRuleExpressionWithName;
        #endregion

        #region Class constructors
        public CollectionRules() 
        {
            dictRuleExpressionWithOutName = new Dictionary<Type, Expression>();
            dictRuleExpressionWithName = new Dictionary<string, Expression>();
        }
        #endregion

        #region Add methods
        public ICollectionRules AddRule<TFrom, TTo>(Expression<Func<TFrom, TTo>> expressionMaping)
        {
            if (expressionMaping is null)
                throw new ArgumentNullException(nameof(expressionMaping));

            if (dictRuleExpressionWithOutName.TryAdd(expressionMaping.GetType(), expressionMaping))
                return this;
            else throw new RuleExistException(expressionMaping.GetType());
        }

        public ICollectionRules AddRule<TFrom, TTo>(Expression<Func<TFrom, TTo>> expressionMaping, string nameRule)
        {
            if (expressionMaping is null)
                throw new ArgumentNullException(nameof(expressionMaping));

            if (string.IsNullOrEmpty(nameRule))
                throw new ArgumentNullException(nameof(nameRule));

            if (dictRuleExpressionWithName.TryAdd(nameRule, expressionMaping))
                return this;
            else throw new RuleExistException(nameRule);
        }
        #endregion

        #region Get methods
        public Expression<Func<TFrom, TTo>> GetFirstRule<TFrom, TTo>()
        {
            var rules = GetRulesCommonCollection<TFrom, TTo>();

            if (rules.Count() < 1)
                throw new RuleNotExistException(typeof(Expression<Func<TFrom, TTo>>));

            return rules.First();
        }

        public Expression<Func<TFrom, TTo>> GetRule<TFrom, TTo>(string nameRule)
        {
            if (dictRuleExpressionWithName.TryGetValue(nameRule, out Expression expr))
                return AsTypeExprOrThrow<TFrom, TTo>(expr);
            else throw new RuleNotExistException(nameRule);
        }

        public IEnumerable<Expression<Func<TFrom, TTo>>> GetRules<TFrom, TTo>()
        {
            return GetRulesCommonCollection<TFrom, TTo>();
        }
        #endregion

        #region Exist and Delete methods
        public bool RuleExist<TFrom, TTo>()
        {
            throw new NotImplementedException();
        }

        public void DeleteRule<TFrom, TTo>()
        {
            throw new NotImplementedException();
        }

        public void DeleteRule<TFrom, TTo>(string nameRule)
        {
            throw new NotImplementedException();
        }

        public bool RuleExist<TFrom, TTo>(string nameRule)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private/protected methods
        protected IEnumerable<Expression<Func<TFrom, TTo>>> GetRulesCommonCollection<TFrom, TTo>()
        {
            var typeExpr = typeof(Expression<Func<TFrom, TTo>>);

            if (dictRuleExpressionWithOutName.TryGetValue(typeExpr, out Expression expr))
                yield return AsTypeExprOrThrow<TFrom, TTo>(expr);


            foreach (var keyValue in dictRuleExpressionWithName)
                if (keyValue.Value is Expression<Func<TFrom, TTo>>)
                    yield return AsTypeExprOrThrow<TFrom, TTo>(keyValue.Value);
        }

        private Expression<Func<TFrom, TTo>> AsTypeExprOrThrow<TFrom, TTo>(Expression expr)
        {
            var exprConvert = expr as Expression<Func<TFrom, TTo>>;

            if (exprConvert is not null)
                return exprConvert;
            else throw new ExpressionNotNeededType(expr);
        }
        #endregion
    }
}
