using System.Linq.Expressions;

namespace Models.HardMapperModels
{
    public class MapInfo
    {
        public MapInfo(
            bool itRootMapper,
            bool firstCallFromCollection,
            Expression expr
            )
        {
            ItRootMapper = itRootMapper;
            FirstCallFromCollection = firstCallFromCollection;
            Expr = expr;
        }

        public bool FirstCallFromCollection { get; set; }
        public bool ItRootMapper { get; set; } = false;
        public Expression Expr { get; set; }

      /*  public override bool Equals(object obj)
        {
            if (obj is not MapInfo)
                return false;

            var mapInfoObj = obj as MapInfo;

            return Expr == mapInfoObj?.Expr;
        }
        public override int GetHashCode() => base.GetHashCode();*/
    }
}
