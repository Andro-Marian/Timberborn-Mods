﻿using Bindito.Core;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;
using TimberApi.SpecificationSystem;

namespace HideRangePath.Specifications.KeyBinding
{
    [Configurator(SceneEntrypoint.All)]
    public class KeyBindingConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.MultiBind<ISpecificationGenerator>().To<KeyBindingGenerator>().AsSingleton();
        }
    }
}