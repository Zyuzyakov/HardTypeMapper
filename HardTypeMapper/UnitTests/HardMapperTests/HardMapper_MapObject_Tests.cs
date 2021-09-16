using HardTypeMapper;
using HardTypeMapper.CollectionRules;
using Interfaces.CollectionRules;
using System.Collections.Generic;
using System.Linq;
using UnitTests.TestModels;
using Xunit;

namespace UnitTests.HardMapperTests
{
    public class HardMapper_MapObject_Tests
    {
        private ICollectionRules collectionRules;
        private HardMapper hardMapper;

        Street street;
        House house;
        Flat flat;

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

            flat = new Flat()
            {
                Name = "flat",
                House = house,
            };

            street.Houses = new List<House>() { house };
            house.Flats = new List<Flat>() { flat };
        }

        [Fact]
        public void Map_FromFlat_Correct()
        {
            Init();

            var flatDto = hardMapper.Map<Flat, FlatDto>(flat);

            Assert.NotNull(flatDto);
            Assert.NotNull(flatDto.HouseDto);
            Assert.NotNull(flatDto.HouseDto.StreetDto);

            Assert.Empty(flatDto.HouseDto.FlatsDto);
            Assert.Empty(flatDto.HouseDto.StreetDto.HousesDto);
        }

        [Fact]
        public void Map_FromHouse_Correct()
        {
            Init();

            var houseDto = hardMapper.Map<House, HouseDto>(house);

            Assert.NotNull(houseDto);
            Assert.NotNull(houseDto.StreetDto);
            Assert.Empty(houseDto.StreetDto.HousesDto);
            Assert.Single(houseDto.FlatsDto);

            var flatDto = houseDto.FlatsDto.First();

            Assert.Null(flatDto.HouseDto);
        }

        [Fact]
        public void Map_FromStreet_Correct()
        {
            Init();

            var streetDto = hardMapper.Map<Street, StreetDto>(street);

            Assert.NotNull(streetDto);
            Assert.Single(streetDto.HousesDto);

            var houseDto = streetDto.HousesDto.First();

            Assert.Null(houseDto.StreetDto);
            Assert.Single(houseDto.FlatsDto);

            var flatDto = houseDto.FlatsDto.First();

            Assert.Null(flatDto.HouseDto);
        }
    }
}
