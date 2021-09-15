﻿using System;

namespace HardTypeMapper.Models.CollectionModels
{
    public static class SetOfTypesHelper
    {
        public static SetOfTypes<TOutType> Create<TOutType>(string nameRule, params Type[] inTypes)
        {
            if (inTypes is null || inTypes.Length < 1)
                throw new ArgumentException($"Количество входных типов должно быть > 0.");

            return new SetOfTypes<TOutType>(nameRule, inTypes);
        }
    }
}
