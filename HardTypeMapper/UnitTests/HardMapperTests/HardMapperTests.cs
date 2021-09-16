using HardTypeMapper;
using HardTypeMapper.CollectionRules;
using Interfaces.CollectionRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.TestModels;
using Xunit;

namespace UnitTests.HardMapperTests
{
    public class HardMapperTests
    {
        private ICollectionRules collectionRules;
        private HardMapper hardMapper;

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
        }

        [Fact]
        public void Map_FromFlat_Correct()
        {
            Init();

            var street = new Street()
            {
                Name = "street",
            };

            var house = new House()
            {
                Name = "house",
                Street = street,
            };

            var flat = new Flat()
            {
                Name = "flat",
                House = house,
            };

            street.Houses = new List<House>() { house };

            house.Flats = new List<Flat>() { flat };


            var flatDto = hardMapper.Map<Flat, FlatDto>(flat);

            Assert.NotNull(flatDto);
            Assert.NotNull(flatDto.HouseDto);
            Assert.NotNull(flatDto.HouseDto.StreetDto);

            Assert.Empty(flatDto.HouseDto.FlatsDto);
            Assert.Empty(flatDto.HouseDto.StreetDto.HousesDto);
        }
    }
}
