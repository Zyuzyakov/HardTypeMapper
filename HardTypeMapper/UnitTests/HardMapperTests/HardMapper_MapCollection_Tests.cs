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

            collectionRules.AddRule<Flat, FlatDto>((mm, flat, dto) =>
            {
                dto.Name = flat.Name;
                dto.HouseDto = mm.Map<House, HouseDto>(flat.House);
            });

            collectionRules.AddRule<House, HouseDto>((mm, house, dto) =>
            {
                dto.Name = house.Name;
                dto.StreetDto = mm.Map<Street, StreetDto>(house.Street);
                dto.FlatsDto = mm.Map<Flat, FlatDto>(house.Flats).ToList();
            });

            collectionRules.AddRule<Street, StreetDto>((mm, street, dto) => 
            {
                dto.Name = street.Name;
                dto.HousesDto = mm.Map<House, HouseDto>(street.Houses).ToList();
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

            var flatsDtos = hardMapper.Map<Flat, FlatDto>(listFlats).ToList();

            Assert.Equal(3, flatsDtos.Count);

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

        [Fact]
        public void Map_FromHouseCollection_Correct()
        {
            Init();

            var listHouses = new List<House>() { house, house2 };

            var housesDtos = hardMapper.Map<House, HouseDto>(listHouses).ToList();

            Assert.Equal(2, housesDtos.Count);

            var house1Dto = housesDtos.First(x => x.Name == "house");

            Assert.Single(house1Dto.FlatsDto);
            var house1Flat = house1Dto.FlatsDto.First();
            Assert.Null(house1Flat.HouseDto);
            Assert.NotNull(house1Dto.StreetDto);
            Assert.Empty(house1Dto.StreetDto.HousesDto);

            var house2Dto = housesDtos.First(x => x.Name == "house2");

            Assert.Equal(2, house2Dto.FlatsDto.Count());
            var house2Flat2 = house2Dto.FlatsDto.First(x => x.Name == "flat2");
            var house2Flat3 = house2Dto.FlatsDto.First(x => x.Name == "flat3");
            Assert.Null(house2Flat2.HouseDto);
            Assert.Null(house2Flat3.HouseDto);
            Assert.NotNull(house2Dto.StreetDto);
            Assert.Empty(house2Dto.StreetDto.HousesDto);
        }

        [Fact]
        public void Map_FromStreetCollection_Correct()
        {
            Init();

            var listStreets = new List<Street>() { street };

            var streetsDtos = hardMapper.Map<Street, StreetDto>(listStreets).ToList();

            Assert.Single(streetsDtos);
            var streetDto = streetsDtos.First(x => x.Name == "street");
            Assert.Equal(2, streetDto.HousesDto.Count);

            var house1Dto = streetDto.HousesDto.First(x => x.Name == "house");
            Assert.Null(house1Dto.StreetDto);
            Assert.Single(house1Dto.FlatsDto);
            var house1DtoFlat = house1Dto.FlatsDto.First();
            Assert.NotNull(house1DtoFlat);
            Assert.Null(house1DtoFlat.HouseDto);

            var house2Dto = streetDto.HousesDto.First(x => x.Name == "house2");
            Assert.Null(house2Dto.StreetDto);
            Assert.Equal(2, house2Dto.FlatsDto.Count);
            var house2DtoFlat2 = house2Dto.FlatsDto.First(x => x.Name == "flat2");
            Assert.NotNull(house2DtoFlat2);
            Assert.Null(house2DtoFlat2.HouseDto);
            var house2DtoFlat3 = house2Dto.FlatsDto.First(x => x.Name == "flat3");
            Assert.NotNull(house2DtoFlat3);
            Assert.Null(house2DtoFlat3.HouseDto);
        }
    }
}
