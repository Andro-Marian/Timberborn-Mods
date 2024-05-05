﻿using System.Collections.Generic;
using Newtonsoft.Json;
using TimberApi.SpecificationSystem;
using TimberApi.SpecificationSystem.SpecificationTypes;

namespace HideRangePath.Specifications.KeyBindingGroup
{
    public class KeyBindingGroupGenerator : ISpecificationGenerator
    {
        public IEnumerable<ISpecification> Generate()
        {
            yield return new GeneratedSpecification(JsonConvert.SerializeObject(new
            {
                Id = "HideRangePath",
                DisplayName = "Hide Range Path",
                LocKey = "HideRangePath",
                Order = 0,
                IsHiddenGroup = false,
            }), "HideRangePath", "KeyBindingGroupSpecification");
        }
    }
}