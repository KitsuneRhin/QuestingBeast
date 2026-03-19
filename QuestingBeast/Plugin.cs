using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace QuestingBeast
{
    [BepInPlugin(
        "KitsuneRhin.QuestingBeast",
        "QuestingBeast",
        "1.0.0")
     ]

    public class Plugin : BaseUnityPlugin
    {
        private Harmony _harmony;
        internal static ManualLogSource Log;
        internal static PluginConfig _config;
        public static Plugin PluginInstance { get; private set; }


        private void Awake()
        {
            PluginInstance = this;
            Log = Logger;
            _config = new PluginConfig(Config);
            Log.LogInfo("A new QuestingBeast is hatching!");
            _harmony = new Harmony("KitsuneRhin.QuestingBeast");

            try
            {
                _harmony.PatchAll();
            }
            catch (System.Exception ex)
            {
                Log.LogError($"Error patching: {ex}");
            }
        }

        private void OnDestroy()
        {
            try
            {
                _harmony?.UnpatchSelf();
                Log.LogInfo("QuestingBeast unpatched successfully");
            }
            catch (System.Exception ex)
            {
                Log.LogWarning($"Error unpatching: {ex}");
            }
        }
    }
}
