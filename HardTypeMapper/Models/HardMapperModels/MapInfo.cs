using System;

namespace Models.HardMapperModels
{
    public class MapInfo
    {
        public MapInfo(
            bool itRootMapper,
            bool firstCallFromCollection,
            Delegate action
            )
        {
            ItRootMapper = itRootMapper;
            FirstCallFromCollection = firstCallFromCollection;
            Action = action;
        }

        public bool FirstCallFromCollection { get; set; }
        public bool ItRootMapper { get; set; } = false;
        public Delegate Action { get; set; }


        /* 
          сравнение по Expr
          public override bool Equals(object obj)
          {
              if (obj is not MapInfo)
                  return false;

              var mapInfoObj = obj as MapInfo;

              return Expr == mapInfoObj?.Expr;
          }
          public override int GetHashCode() => base.GetHashCode();
        */
    }
}
