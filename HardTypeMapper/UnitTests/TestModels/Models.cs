using System.Collections.Generic;

namespace UnitTests.TestModels
{
    public class Street
    {
        public string Name { get; set; }
        public ICollection<House> Houses { get; set; }
    }
    public class House
    {
        public string Name { get; set; }
        public Street Street { get; set; }
        public ICollection<Flat> Flats { get; set; }
    }
    public class Flat
    {
        public string Name { get; set; }
        public House House { get; set; }
    }
}
