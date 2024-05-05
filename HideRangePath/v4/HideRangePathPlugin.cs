using HarmonyLib;
using HideRangePath.ToggleButton;
using TimberApi.ConsoleSystem;
using TimberApi.DependencyContainerSystem;
using TimberApi.ModSystem;
using Timberborn.BuildingsNavigation;
using Timberborn.GameScene;
using Timberborn.InputSystem;

namespace HideRangePath
{
    [HarmonyPatch]
    public class HideRangePathPlugin : IModEntrypoint
    {
        private static InputService _key;
        private static HideRangePathTogglePanel _button;
        
        public void Entry(IMod mod, IConsoleWriter consoleWriter)
        {
            var harmony = new Harmony("com.github.andro-marian.hiderangepath");

            harmony.PatchAll();
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(NewGameInitializer), "Start")]
        private static void OnGameStarted(NewGameInitializer __instance)
        {
            _key = DependencyContainer.GetInstance<InputService>();
            _button = DependencyContainer.GetInstance<HideRangePathTogglePanel>();
        }

        #region Buildings:
        
        /*[HarmonyPrefix]
        [HarmonyPatch(typeof(BuildingRangeDrawer), "OnSelect")]
        public static void OnBuildingSelect(BuildingRangeDrawer __instance)
        {
            _button.Status = false;
        }*/
        
        [HarmonyPrefix]
        [HarmonyPatch(typeof(BuildingRangeDrawer), "DrawRange", typeof(bool))]
        public static bool OnBuildingDrawRange(BuildingRangeDrawer __instance, bool isPreview)
        {
            return _button.Status || _key.IsShiftHeld;
            //return _button.Status || _key.IsKeyHeld("HideRangePathHoldKey");
        }

        #endregion
        #region Paths:
                
        /*[HarmonyPrefix]
        [HarmonyPatch(typeof(PathRangeDrawer), "OnSelect")]
        public static void OnPathSelect(PathRangeDrawer __instance)
        {
            _button.Status = false;
        }*/

        [HarmonyPrefix]
        [HarmonyPatch(typeof(PathRangeDrawer), "DrawRange", typeof(bool))]
        public static bool OnPathDrawRange(PathRangeDrawer __instance, bool isSingle)
        {
            return _button.Status || _key.IsShiftHeld;
        }

        #endregion
    }
}
