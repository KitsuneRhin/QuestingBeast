using BepInEx.Configuration;

namespace QuestingBeast
{
    internal class PluginConfig
    {
        public ConfigEntry<bool> MountFormPersists { get; private set; }
        public ConfigEntry<bool> CombatFormsPersists { get; private set; }
        public ConfigEntry<float> MountFormDuration { get; private set; }
        public ConfigEntry<float> CombatFormDuration { get; private set; }

        public PluginConfig(ConfigFile config)
        {
            config.SaveOnConfigSet = false;
            try
            {
                MountFormPersists = config.Bind("Mount", "MountFormPersists", true,
                    new ConfigDescription("If mount form stays until changed.\nRecommend: 'On'",
                    new AcceptableValueRange<bool>(false, true)));

                CombatFormsPersists = config.Bind("Combat", "CombatFormsPersist", false,
                    new ConfigDescription("If combat forms stay until changed.\nRecommend: 'Off'",
                    new AcceptableValueRange<bool>(false, true)));

                MountFormDuration = config.Bind("Mount", "MountFormDuration", 300f,
                    new ConfigDescription("Duration of the mount form in seconds.\nGame Default:120\nMod Default:300",
                    new AcceptableValueRange<float>(5f, 1200f)));

                CombatFormDuration = config.Bind("Combat", "CombatFormDuration", 300f,
                    new ConfigDescription("Duration of the combat forms in seconds.\nGame Default:120\nMod Default:300",
                    new AcceptableValueRange<float>(5f, 1200)));
            }
            finally
            {
                config.Save();
                config.SaveOnConfigSet = true;
            }
        }
    }
}
