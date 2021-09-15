using HardTypeMapper;
using HardTypeMapper.CollectionRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.TestModels;

namespace UnitTests.HardMapperTests
{
    public class HardMapperTests
    {
        public void StackTrace_test()
        {
            var collectionRules = new CollectionRules();

            collectionRules.AddRule<Flat, FlatDto>((mm, flat) => new FlatDto()
            {
                Name = flat.Name,
                HouseDto = mm.Map<House, HouseDto>(flat.House, null)
            });

            collectionRules.AddRule<House, HouseDto>((mm, house) => new HouseDto()
            {
                Name = house.Name,
                StreetDto = mm.Map<Street, StreetDto>(house.Street, null)
            });

            var hardMapper = new HardMapper(collectionRules);

            var flat = new Flat()
            {
                Name = "flat",
                House = new House()
                {
                    Name = "house",
                    Street = new Street()
                    {
                        Name = "street",
                    }
                }
            };

            FlatDto fdto = hardMapper.Map<Flat, FlatDto>(flat);
        }
    }
}
