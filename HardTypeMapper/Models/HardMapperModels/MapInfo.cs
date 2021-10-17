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
    }
}
