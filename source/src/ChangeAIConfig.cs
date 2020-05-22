using RTSCamera;
using System;
using System.IO;
using System.Xml.Serialization;

namespace ImprovedCombatAI
{
    public class ChangeAIConfig : RTSCameraConfigBase<ChangeAIConfig>
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
        private static ChangeAIConfig _instance;
        public string ConfigVersion { get; set; } = BinaryVersion.ToString();

        public bool UseRealisticBlocking = false;

        public int MeleeAIDifficulty = 50;

        public int RangedAIDifficulty = 50;

        public bool ChangeMeleeAI = false;

        public bool ChangeRangedAI = false;

        public int MeleeAI = 100;
        public int RangedAI = 100;
        private static ChangeAIConfig CreateDefault()
        {
            return new ChangeAIConfig();
        }


        public static ChangeAIConfig Get()
        {
            if (_instance == null)
            {
                _instance = CreateDefault();
                _instance.SyncWithSave();
            }

            return _instance;
        }

        protected override XmlSerializer serializer => new XmlSerializer(typeof(ChangeAIConfig));


        protected override void CopyFrom(ChangeAIConfig other)
        {
            ConfigVersion = other.ConfigVersion;
            UseRealisticBlocking = other.UseRealisticBlocking;
            MeleeAIDifficulty = other.MeleeAIDifficulty;
            RangedAIDifficulty = other.RangedAIDifficulty;
            ChangeMeleeAI = other.ChangeMeleeAI;
            ChangeRangedAI = other.ChangeRangedAI;
            MeleeAI = other.MeleeAI;
            RangedAI = other.RangedAI;
        }

        public override void ResetToDefault()
        {
            CopyFrom(CreateDefault());
        }

        protected override string SaveName => Path.Combine(SavePath, nameof(ChangeAIConfig) + ".xml");
        protected override string[] OldNames { get; } =
        {
            Path.Combine(OldSavePath, "MoreOptionsConfig.xml"),
            Path.Combine(OldSavePath, "ChangeAIConfig.xml"),
        };

    }
}
