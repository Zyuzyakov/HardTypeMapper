using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UnitTests.TestModels
{
    public class Street
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<House> Houses { get; set; }
    }
    public class House
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Street Street { get; set; }
        public ICollection<Flat> Flats { get; set; }
    }
    public class Flat
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public House House { get; set; }
    }
}
