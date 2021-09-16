using HardTypeMapper;
using HardTypeMapper.CollectionRules;
using Interfaces.CollectionRules;
using Interfaces.MapMethods;
using System.Collections.Generic;
using System.Linq;
using UnitTests.TestModels;
using Xunit;

namespace UnitTests.HardMapperTests
{
    public class HardMapper_MapCollection_Tests
    {
        private ICollectionRules collectionRules;
        private IMapMethods hardMapper;

        Street street;

        House house;
        House house2;

        Flat flat;
        Flat flat2;
        Flat flat3;

        private void Init()
        {
            collectionRules = new CollectionRules();

            collectionRules.AddRule<Flat, FlatDto>((mm, flat) => new FlatDto()
            {
                Name = flat.Name,
                HouseDto = mm.Map<House, HouseDto>(flat.House)
            });

            collectionRules.AddRule<House, HouseDto>((mm, house) => new HouseDto()
            {
                Name = house.Name,
                StreetDto = mm.Map<Street, StreetDto>(house.Street),
                FlatsDto = mm.Map<Flat, FlatDto>(house.Flats).ToList()
            });

            collectionRules.AddRule<Street, StreetDto>((mm, street) => new StreetDto()
            {
                Name = street.Name,
                HousesDto = mm.Map<House, HouseDto>(street.Houses).ToList()
            });

            hardMapper = new HardMapper(collectionRules);


            street = new Street()
            {
                Name = "street",
            };

            house = new House()
            {
                Name = "house",
                Street = street,
            };
            house2 = new House()
            {
                Name = "house2",
                Street = street
            };

            flat = new Flat()
            {
                Name = "flat",
                House = house,
            };
            flat2 = new Flat()
            {
                Name = "flat2",
                House = house2,
            };
            flat3 = new Flat()
            {
                Name = "flat3",
                House = house2,
            };

            street.Houses = new List<House>() { house, house2 };
            house.Flats = new List<Flat>() { flat };
            house2.Flats = new List<Flat>() { flat2, flat3 };
        }

        [Fact]
        public void Map_FromFlatCollection_Correct()
        {
            Init();

            var listFlats = new List<Flat>() { flat, flat2, flat3 };

            var flatsDtos = hardMapper.Map<Flat, FlatDto>(listFlats);

            Assert.Equal(3, flatsDtos.Count());

            var flat1Dto = flatsDtos.First(x => x.Name == "flat");
            var flat2Dto = flatsDtos.First(x => x.Name == "flat2");
            var flat3Dto = flatsDtos.First(x => x.Name == "flat3");

            Assert.NotNull(flat1Dto);
            Assert.NotNull(flat1Dto.HouseDto);
            Assert.NotNull(flat1Dto.HouseDto.StreetDto);
            Assert.Empty(flat1Dto.HouseDto.FlatsDto);
            Assert.Empty(flat1Dto.HouseDto.StreetDto.HousesDto);

            Assert.NotNull(flat2Dto);
            Assert.NotNull(flat2Dto.HouseDto);
            Assert.NotNull(flat2Dto.HouseDto.StreetDto);
            Assert.Empty(flat2Dto.HouseDto.FlatsDto);
            Assert.Empty(flat2Dto.HouseDto.StreetDto.HousesDto);

            Assert.NotNull(flat3Dto);
            Assert.NotNull(flat3Dto.HouseDto);
            Assert.NotNull(flat3Dto.HouseDto.StreetDto);
            Assert.Empty(flat3Dto.HouseDto.FlatsDto);
            Assert.Empty(flat3Dto.HouseDto.StreetDto.HousesDto);
        }
    }
}
