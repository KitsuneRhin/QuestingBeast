using HarmonyLib;
using System;
using Awaken.TG.Main.Locations.Pets.Variants;

namespace QuestingBeast
{
    [HarmonyPatch(typeof(PetVariantBase))]
    [HarmonyPatch("InitFromAttachment")]
    [HarmonyPatch(new Type[] { typeof(PetVariantAttachment), typeof(bool) })]

    internal class PatchDuration
    {
        static void Prefix(PetVariantBase __instance, PetVariantAttachment spec, bool isRestored)
        {
            Plugin.Log.LogInfo("Prefix triggered... Intercepting pet variant attachment");
            try
            {
                if (__instance is MountPetVariant)
                {
                    if (Plugin._config.MountFormPersists.Value)
                    {
                        spec.hasDuration = false;
                    }
                    else
                    {
                        spec.hasDuration = true;
                        spec.duration = Plugin._config.MountFormDuration.Value;
                    }
                    Plugin.Log.LogInfo($"Mount form detected. Duration: {(spec.hasDuration ? spec.duration.ToString() : "Infinite")}");
                }
                else if (__instance is AoEPetVariant || __instance is NpcAllyPetVariant)
                {
                    if (Plugin._config.CombatFormsPersists.Value)
                    {
                        spec.hasDuration = false;
                    }
                    else
                    {
                        spec.hasDuration = true;
                        spec.duration = Plugin._config.CombatFormDuration.Value;
                    }
                    Plugin.Log.LogInfo($"Combat form detected. Duration: {(spec.hasDuration ? spec.duration.ToString() : "Infinite")}");
                }
            }
            catch (System.Exception ex)
            {
                Plugin.Log.LogError($"Error in InitFromAttachment Prefix: {ex}");
            }
        }
    }
}
