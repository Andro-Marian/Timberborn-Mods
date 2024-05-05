﻿using Bindito.Core;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;
using TimberApi.SpecificationSystem;

namespace HideRangePath.Specifications.KeyBindingGroup
{
    [Configurator(SceneEntrypoint.All)]
    public class KeyBindingGroupConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.MultiBind<ISpecificationGenerator>().To<KeyBindingGroupGenerator>().AsSingleton();
        }
    }
}