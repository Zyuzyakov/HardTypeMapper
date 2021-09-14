using System.Collections.Generic;

namespace UnitTests.TestModels
{
    public class StreetDto
    {
        public string Name { get; set; }
        public ICollection<HouseDto> HousesDto { get; set; }
    }
    public class HouseDto
    {
        public string Name { get; set; }
        public StreetDto StreetDto { get; set; }
        public ICollection<FlatDto> FlatsDto { get; set; }
    }
    public class FlatDto
    {
        public string Name { get; set; }
        public HouseDto HouseDto { get; set; }
    }
}
