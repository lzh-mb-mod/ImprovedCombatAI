using MissionSharedLibrary.Config;
using MissionSharedLibrary.Utilities;
using System;
using System.IO;
using TaleWorlds.MountAndBlade;

namespace ImprovedCombatAI.Config
{
    public class ImprovedCombatAIConfig : MissionConfigBase<ImprovedCombatAIConfig>
    {
        protected static Version BinaryVersion => new Version(1, 0);

        protected override void UpgradeToCurrentVersion()
        {
            switch (ConfigVersion?.ToString())
            {
                default:
                    Utility.DisplayLocalizedText("str_config_incompatible");
                    ResetToDefault();
                    Serialize();
                    break;
                case "1.0":
                    break;
            }
        }
        public string ConfigVersion { get; set; } = BinaryVersion.ToString();

        public bool UseRealisticBlocking = false;

        public int MeleeAIDifficulty = 50;

        public int RangedAIDifficulty = 50;

        public bool DirectlySetMeleeAI = false;

        public bool DirectlySetRangedAI = false;

        public float RangedError = 0;

        public bool EnableTacticalAI = false;

        protected override void CopyFrom(ImprovedCombatAIConfig other)
        {
            ConfigVersion = other.ConfigVersion;
            UseRealisticBlocking = other.UseRealisticBlocking;
            MeleeAIDifficulty = other.MeleeAIDifficulty;
            RangedAIDifficulty = other.RangedAIDifficulty;
            DirectlySetMeleeAI = other.DirectlySetMeleeAI;
            DirectlySetRangedAI = other.DirectlySetRangedAI;
            RangedError = other.RangedError;
            EnableTacticalAI = other.EnableTacticalAI;
        }

        private static readonly string SavePathStatic =
            Path.Combine(ConfigPath.ConfigDir, ImprovedCombatAISubModule.ModuleId);
        private static readonly string OldSavePathStatic = Path.Combine(ConfigPath.ConfigDir, "RTSCamera");

        protected override string SaveName => Path.Combine(SavePathStatic, nameof(ImprovedCombatAIConfig) + ".xml");

        protected override string OldSavePath => OldSavePathStatic;

        protected override string[] OldNames { get; } =
        {
            Path.Combine(OldSavePathStatic, "MoreOptionsConfig.xml"),
            Path.Combine(OldSavePathStatic, "ChangeAIConfig.xml"),
            Path.Combine(SavePathStatic + "ChangeAIConfig.xml")
        };

        public static void OnMenuClosed()
        {
            Get().Serialize();
            UpdateAgentProperties();
        }

        private static void UpdateAgentProperties()
        {
            if (Mission.Current == null)
                return;
            foreach (var agent in Mission.Current.Agents)
            {
                agent.UpdateAgentProperties();
            }
        }
    }
}
