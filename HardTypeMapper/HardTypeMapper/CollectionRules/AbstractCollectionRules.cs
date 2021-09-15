using Exceptions.ForCollectionRules;
using HardTypeMapper.Models.CollectionModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace HardTypeMapper.CollectionRules
{
    public abstract class AbstractCollectionRules
    {
        #region Сlass variables
        protected Dictionary<ISetOfTypes, Expression> dictRuleExpression;
        #endregion

        #region Class constructors
        protected AbstractCollectionRules()
        {
            dictRuleExpression = new Dictionary<ISetOfTypes, Expression>();
        }
        #endregion

        #region Add methods
        public void AddRule(ISetOfTypes setOfTypes, Expression expressionMaping)
        {
            if (setOfTypes is null)
                throw new ArgumentNullException(nameof(setOfTypes));

            if (expressionMaping is null)
                throw new ArgumentNullException(nameof(expressionMaping));

            if (!dictRuleExpression.TryAdd(setOfTypes, expressionMaping))
                 throw new RuleNotAddException(setOfTypes.SetName);
        }
        #endregion

        #region Get methods
        public Expression GetAnyRule(ISetOfTypes setOfTypes)
        {
            if (setOfTypes is null)
                throw new ArgumentNullException(nameof(setOfTypes));

            var rules = GetRules<Expression>(setOfTypes, false);

            if (!rules.Any())
                throw new RuleNotExistException(setOfTypes.SetName, setOfTypes.GetOutTypeParam(), setOfTypes.GetInTypeParams());

            return rules.First();
        }

        public Expression GetRule(ISetOfTypes setOfTypes)
        {
            if (setOfTypes is null)
                throw new ArgumentNullException(nameof(setOfTypes));

            var rules = GetRules<Expression>(setOfTypes, true);

            if (!rules.Any())
                throw new RuleNotExistException(setOfTypes.SetName, setOfTypes.GetOutTypeParam(), setOfTypes.GetInTypeParams());

            return rules.First();
        }

        public Dictionary<string, Expression> GetRules(ISetOfTypes setOfTypes, bool withName)
        {
            if (setOfTypes is null)
                throw new ArgumentNullException(nameof(setOfTypes));

            var dictReturn = new Dictionary<string, Expression>();

            foreach (var pair in dictRuleExpression)
                if (pair.Key.Equals(setOfTypes, withName))
                    dictReturn.Add(pair.Key.SetName, pair.Value);

            return dictReturn;
        }
        #endregion

        #region Exist and Delete methods
        public bool ExistRule(ISetOfTypes setOfTypes)
        {
            if (setOfTypes is null)
                throw new ArgumentNullException(nameof(setOfTypes));

            return GetRules<Expression>(setOfTypes, true).Any();
        }

        public void DeleteRule(ISetOfTypes setOfTypes)
        {
            if (setOfTypes is null)
                throw new ArgumentNullException(nameof(setOfTypes));

            foreach (var item in dictRuleExpression)
                if (equals(item, setOfTypes, true))
                {
                    dictRuleExpression.Remove(item.Key);

                    return;
                }

            throw new RuleNotExistException(setOfTypes.SetName);
        }
        #endregion

        #region Class private methods
        Func<KeyValuePair<ISetOfTypes, Expression>, ISetOfTypes, bool, bool> equals =
               (pair, existKey, withName) => pair.Key.Equals(existKey, withName);
           
        private protected TRuleExpr ConvertExpression<TRuleExpr>(Expression expr) where TRuleExpr : Expression
        {
            if (expr is TRuleExpr)
                return (TRuleExpr)expr;

            throw new ExpressionNotNeededTypeException(typeof(TRuleExpr).FullName);
        }

        private protected IEnumerable<TRuleExpr> GetRules<TRuleExpr>(ISetOfTypes key, bool checkName) where TRuleExpr : Expression
        {
            foreach (var item in dictRuleExpression)
                if (equals(item, key, checkName))
                    yield return ConvertExpression<TRuleExpr>(item.Value);
        }
        #endregion
    }
}
