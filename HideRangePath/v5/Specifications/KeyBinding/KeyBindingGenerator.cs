﻿using System.Collections.Generic;
using Newtonsoft.Json;
using TimberApi.SpecificationSystem;
using TimberApi.SpecificationSystem.SpecificationTypes;

namespace HideRangePath.Specifications.KeyBinding
{
    public class KeyBindingGenerator : ISpecificationGenerator
    {
        public IEnumerable<ISpecification> Generate()
        {
            yield return new GeneratedSpecification(JsonConvert.SerializeObject(new
            {
                Id = "HideRangePathToggleKey",
                GroupId = "HideRangePath",
                DisplayName = "Toggle",
                LocKey = "HideRangePath.ToggleKey",
                Order = 0,
                AllowOtherModifiers = true,
                DevModeOnly = false,
                Hidden = false
            }), "HideRangePathToggleKey", "KeyBindingSpecification");
            
            yield return new GeneratedSpecification(JsonConvert.SerializeObject(new
            {
                Id = "HideRangePathHoldKey",
                GroupId = "HideRangePath",
                DisplayName = "Holding",
                LocKey = "HideRangePath.HoldKey",
                Order = 1,
                AllowOtherModifiers = true,
                DevModeOnly = false,
                Hidden = false
            }), "HideRangePathHoldKey", "KeyBindingSpecification");
        }
    }
}