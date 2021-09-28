using HardTypeMapper;
using HardTypeMapper.CollectionRules;
using Interfaces.CollectionRules;
using Interfaces.MapMethods;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitTests.TestModels;

namespace UnitTests.HardMapperTests
{
    public class HardMapper_MapQuery_Tests
    {
        private DbContextOptions<TestContext> _options;

        private ICollectionRules collectionRules;
        private IMapMethods hardMapper;

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

        private void SetupDb(string dbName)
        {
            Init();

            _options = new DbContextOptionsBuilder<TestContext>()
                   .UseInMemoryDatabase(databaseName: $"{nameof(HardMapper_MapQuery_Tests)} {Guid.NewGuid()} {dbName}")
                   .Options;         

            using (var context = new TestContext(_options))
            {
                // adds

                context.SaveChanges();
            }
        }

    }
}
