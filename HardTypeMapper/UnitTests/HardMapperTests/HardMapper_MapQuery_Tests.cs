using HardTypeMapper;
using HardTypeMapper.CollectionRules;
using HardTypeMapper.IQuerybleMapping;
using Interfaces.CollectionRules;
using Interfaces.Includes;
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

        #region FromStreet
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

                Assert.Empty(streetDto.HousesDto);
            }
        }

        [Fact]
        public void Map_FromStreetQuery_WithIIncludeInfo_WithOutInclude_Correct()
        {
            SetupDb(nameof(Map_FromStreetQuery_WithIIncludeInfo_WithOutInclude_Correct));

            using (var context = new TestContext(_options))
            {
                var streets = context.Streets;

                var includeInfo = new ExpressionIncludeEfCoreVisitor();

                includeInfo.AddInclude(new IncludeProps());

                var listStreets = hardMapper.Map<Street, StreetDto>(streets, includeInfo).ToList();

                Assert.Single(listStreets);

                var streetDto = listStreets.First(x => x.Name == "street");

                Assert.Equal(2, streetDto.HousesDto.Count());

                var house1 = streetDto.HousesDto.First(x => x.Name == "house");
                var house2 = streetDto.HousesDto.First(x => x.Name == "house2");

                Assert.Empty(house1.FlatsDto);
                Assert.Empty(house2.FlatsDto);
            }
        }
        #endregion

        #region FromHouse
        [Fact]
        public void Map_FromHouseQuery_WithOutIIncludeInfo_WithOutInclude_Correct()
        {
            SetupDb(nameof(Map_FromHouseQuery_WithOutIIncludeInfo_WithOutInclude_Correct));

            using (var context = new TestContext(_options))
            {
                var houses = context.Houses;

                var listHouses = hardMapper.Map<House, HouseDto>(houses).ToList();

                Assert.Equal(2, listHouses.Count());

                var houseDto1 = listHouses.First(x => x.Name == "house");
                var houseDto2 = listHouses.First(x => x.Name == "house2");

                Assert.Null(houseDto1.StreetDto);
                Assert.Empty(houseDto1.FlatsDto);
                Assert.Null(houseDto2.StreetDto);
                Assert.Empty(houseDto2.FlatsDto);
            }
        }

        [Fact]
        public void Map_FromHouseQuery_WithOutIIncludeInfo_WithInclude_Correct()
        {
            SetupDb(nameof(Map_FromHouseQuery_WithOutIIncludeInfo_WithInclude_Correct));

            using (var context = new TestContext(_options))
            {
                var houses = context.Houses.Include(x => x.Flats).Include(x => x.Street);

                var listHouses = hardMapper.Map<House, HouseDto>(houses).ToList();

                Assert.Equal(2, listHouses.Count());

                var houseDto1 = listHouses.First(x => x.Name == "house");
                var houseDto2 = listHouses.First(x => x.Name == "house2");

                Assert.NotNull(houseDto1.StreetDto);
                Assert.Empty(houseDto1.StreetDto.HousesDto);
                Assert.NotNull(houseDto2.StreetDto);
                Assert.Empty(houseDto2.StreetDto.HousesDto);

                Assert.Single(houseDto1.FlatsDto);
                Assert.Equal(2, houseDto2.FlatsDto.Count());

                var flatDto1 = houseDto1.FlatsDto.First();
                var flatDto2 = houseDto2.FlatsDto.First(x => x.Name == "flat2");
                var flatDto3 = houseDto2.FlatsDto.First(x => x.Name == "flat3");

                Assert.Null(flatDto1.HouseDto);
                Assert.Null(flatDto2.HouseDto);
                Assert.Null(flatDto3.HouseDto);
            }
        }

        [Fact]
        public void Map_FromHouseQuery_WithIIncludeInfo_WithInclude_Correct()
        {
            SetupDb(nameof(Map_FromHouseQuery_WithIIncludeInfo_WithInclude_Correct));

            using (var context = new TestContext(_options))
            {
                var houses = context.Houses.Include(x => x.Street).Include(x => x.Flats);

                var listHouses = hardMapper.Map<House, HouseDto>(houses, new ExpressionIncludeEfCoreVisitor()).ToList();

                Assert.Equal(2, listHouses.Count());

                var houseDto1 = listHouses.First(x => x.Name == "house");
                var houseDto2 = listHouses.First(x => x.Name == "house2");

                Assert.Null(houseDto1.StreetDto);
                Assert.Null(houseDto2.StreetDto);

                Assert.Empty(houseDto1.FlatsDto);
                Assert.Empty(houseDto2.FlatsDto);
            }
        }
        #endregion

        #region FromFlats
        [Fact]
        public void Map_FromFlatQuery_WithOutIIncludeInfo_WithOutInclude_Correct()
        {
            SetupDb(nameof(Map_FromFlatQuery_WithOutIIncludeInfo_WithOutInclude_Correct));

            using (var context = new TestContext(_options))
            {
                var flats = context.Flats;

                var listFlats = hardMapper.Map<Flat, FlatDto>(flats).ToList();

                Assert.Equal(3, listFlats.Count());

                var flatDto1 = listFlats.First(x => x.Name == "flat");
                var flatDto2 = listFlats.First(x => x.Name == "flat2");
                var flatDto3 = listFlats.First(x => x.Name == "flat3");

                Assert.Null(flatDto1.HouseDto);
                Assert.Null(flatDto2.HouseDto);
                Assert.Null(flatDto3.HouseDto);
            }
        }

        [Fact]
        public void Map_FromFlatQuery_WithOutIIncludeInfo_WithInclude_Correct()
        {
            SetupDb(nameof(Map_FromFlatQuery_WithOutIIncludeInfo_WithInclude_Correct));

            using (var context = new TestContext(_options))
            {
                var flats = context.Flats.Include(x => x.House).ThenInclude(x => x.Street);

                var listFlats = hardMapper.Map<Flat, FlatDto>(flats).ToList();

                Assert.Equal(3, listFlats.Count());

                var flatDto1 = listFlats.First(x => x.Name == "flat");
                var flatDto2 = listFlats.First(x => x.Name == "flat2");
                var flatDto3 = listFlats.First(x => x.Name == "flat3");

                Assert.NotNull(flatDto1.HouseDto);
                Assert.NotNull(flatDto2.HouseDto);
                Assert.NotNull(flatDto3.HouseDto);
               
                Assert.Empty(flatDto1.HouseDto.FlatsDto);
                Assert.Empty(flatDto2.HouseDto.FlatsDto);
                Assert.Empty(flatDto3.HouseDto.FlatsDto);

                Assert.NotNull(flatDto1.HouseDto.StreetDto);
                Assert.NotNull(flatDto2.HouseDto.StreetDto);
                Assert.NotNull(flatDto3.HouseDto.StreetDto);

                Assert.Empty(flatDto1.HouseDto.StreetDto.HousesDto);
                Assert.Empty(flatDto2.HouseDto.StreetDto.HousesDto);
                Assert.Empty(flatDto3.HouseDto.StreetDto.HousesDto);
            }
        }

        [Fact]
        public void Map_FromFlatQuery_WithIIncludeInfo_WithInclude_Correct()
        {
            SetupDb(nameof(Map_FromFlatQuery_WithIIncludeInfo_WithInclude_Correct));

            using (var context = new TestContext(_options))
            {
                var flats = context.Flats.Include(x => x.House).ThenInclude(x => x.Street);

                var listFlats = hardMapper.Map<Flat, FlatDto>(flats, new ExpressionIncludeEfCoreVisitor()).ToList();

                Assert.Equal(3, listFlats.Count());

                var flatDto1 = listFlats.First(x => x.Name == "flat");
                var flatDto2 = listFlats.First(x => x.Name == "flat2");
                var flatDto3 = listFlats.First(x => x.Name == "flat3");

                Assert.Null(flatDto1.HouseDto);
                Assert.Null(flatDto2.HouseDto);
                Assert.Null(flatDto3.HouseDto);
            }
        }
        #endregion
    }
}
