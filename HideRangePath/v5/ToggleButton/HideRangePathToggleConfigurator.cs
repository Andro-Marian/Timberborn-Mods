using Bindito.Core;
using TimberApi.ConfiguratorSystem;
using TimberApi.SceneSystem;

namespace HideRangePath.ToggleButton
{
    [Configurator(SceneEntrypoint.InGame | SceneEntrypoint.MapEditor)]
    public class HideRangePathToggleConfigurator : IConfigurator
    {
        public void Configure(IContainerDefinition containerDefinition)
        {
            containerDefinition.Bind<HideRangePathTogglePanel>().AsSingleton();
        }
    }
}