using HardTypeMapper;
using HardTypeMapper.CollectionRules;
using HardTypeMapper.IQuerybleMapping;
using Interfaces.CollectionRules;
using Interfaces.MapMethods;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitTests.TestModels;
using Xunit;

namespace UnitTests.HardMapperTests
{
    public class HardMapper_MapQuery_Tests
    {
        private DbContextOptions<TestContext> _options;

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

        private void SetupDb(string dbName)
        {
            Init();

            _options = new DbContextOptionsBuilder<TestContext>()
                   .UseInMemoryDatabase(databaseName: $"{nameof(HardMapper_MapQuery_Tests)} {Guid.NewGuid()} {dbName}")
                   .Options;         

            using (var context = new TestContext(_options))
            {
                context.Streets.Add(street);

                context.SaveChanges();

                Assert.Single(context.Streets);
                Assert.Equal(2, context.Houses.Count());
                Assert.Equal(3, context.Flats.Count());
            }            
        }

        [Fact]
        public void Map_FromStreetQuery_WithOutIIncludeInfo_WithOutInclude_Correct()
        {
            SetupDb(nameof(Map_FromStreetQuery_WithOutIIncludeInfo_WithOutInclude_Correct));

            using (var context = new TestContext(_options))
            {
                var streets = context.Streets;

               var listStreets = hardMapper.Map<Street, StreetDto>(streets).ToList();

                Assert.Single(listStreets);
                var streetDto = listStreets.First(x => x.Name == "street");
                Assert.Empty(streetDto.HousesDto);
            }            
        }

        [Fact]
        public void Map_FromStreetQuery_WithOutIIncludeInfo_WithInclude_Correct()
        {
            SetupDb(nameof(Map_FromStreetQuery_WithOutIIncludeInfo_WithInclude_Correct));

            using (var context = new TestContext(_options))
            {
                var streets = context.Streets.Include(x => x.Houses).ThenInclude(x => x.Flats);

                var listStreets = hardMapper.Map<Street, StreetDto>(streets).ToList();

                Assert.Single(listStreets);
                var streetDto = listStreets.First(x => x.Name == "street");
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

        [Fact]
        public void Map_FromStreetQuery_WithIIncludeInfo_WithInclude_Correct()
        {
            SetupDb(nameof(Map_FromStreetQuery_WithIIncludeInfo_WithInclude_Correct));

            using (var context = new TestContext(_options))
            {
                var streets = context.Streets.Include(x => x.Houses).ThenInclude(x => x.Flats);

                var listStreets = hardMapper.Map<Street, StreetDto>(streets, new ExpressionIncludeEfCoreVisitor()).ToList();

                Assert.Single(listStreets);
                var streetDto = listStreets.First(x => x.Name == "street");
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
}
