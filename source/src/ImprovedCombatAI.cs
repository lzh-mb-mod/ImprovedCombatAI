using RTSCamera;
using RTSCamera.Config.Basic;
using System;
using System.IO;
using System.Xml.Serialization;

namespace ImprovedCombatAI
{
    public class ImprovedCombatAI : RTSCameraConfigBase<ImprovedCombatAI>
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
        private static ImprovedCombatAI _instance;
        public string ConfigVersion { get; set; } = BinaryVersion.ToString();

        public bool UseRealisticBlocking = false;

        public int MeleeAIDifficulty = 50;

        public int RangedAIDifficulty = 50;

        public bool ChangeMeleeAI = false;

        public bool ChangeRangedAI = false;

        public int MeleeAI = 100;
        public int RangedAI = 100;

        public float RangedError = 0;
        private static ImprovedCombatAI CreateDefault()
        {
            return new ImprovedCombatAI();
        }


        public static ImprovedCombatAI Get()
        {
            if (_instance == null)
            {
                _instance = CreateDefault();
                _instance.SyncWithSave();
            }

            return _instance;
        }

        protected override XmlSerializer serializer => new XmlSerializer(typeof(ImprovedCombatAI));


        protected override void CopyFrom(ImprovedCombatAI other)
        {
            ConfigVersion = other.ConfigVersion;
            UseRealisticBlocking = other.UseRealisticBlocking;
            MeleeAIDifficulty = other.MeleeAIDifficulty;
            RangedAIDifficulty = other.RangedAIDifficulty;
            ChangeMeleeAI = other.ChangeMeleeAI;
            ChangeRangedAI = other.ChangeRangedAI;
            MeleeAI = other.MeleeAI;
            RangedAI = other.RangedAI;
            RangedError = other.RangedError;
        }

        public override void ResetToDefault()
        {
            CopyFrom(CreateDefault());
        }

        protected override string SaveName => Path.Combine(SavePath, nameof(ImprovedCombatAI) + ".xml");
        protected override string[] OldNames { get; } =
        {
            Path.Combine(OldSavePath, "MoreOptionsConfig.xml"),
            Path.Combine(OldSavePath, "ChangeAIConfig.xml"),
            SavePath + "ChangeAIConfig.xml",
        };

    }
}
