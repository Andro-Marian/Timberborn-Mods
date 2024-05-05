using HarmonyLib;
using TimberApi.ConsoleSystem;
using TimberApi.ModSystem;
using Timberborn.MainMenuScene;
using Timberborn.WaterBuildingsUI;
using UnityEngine;

namespace HideWelcomeWindow
{
    [HarmonyPatch]
    public class HideWelcomeWindowPlugin : IModEntrypoint
    {
        public void Entry(IMod mod, IConsoleWriter consoleWriter)
        {
            var harmony = new Harmony("com.github.andro-marian.hidewelcomewindow");
            
            harmony.PatchAll();
        }
        
        [HarmonyPrefix]
        [HarmonyPatch(typeof(WelcomeScreenBox), "Show")]
        public static bool OnWelcomeScreenShow(WelcomeScreenBox __instance)
        {
            __instance.ShowMainMenu();
            return false;
        }
        
        [HarmonyPrefix]
        [HarmonyPatch(typeof(FloodgateFragment), "UpdateSliderValue")]
        public static bool OnFloodgateFragment(FloodgateFragment __instance, ref float __result, float value)
        {
            float num = Mathf.Round(value * 4f) / 4f;
            __instance._slider.SetValueWithoutNotify(num);
            Debug.Log("Value: " + value);
            __result = num;
            
            return false;
        }
    }
}
