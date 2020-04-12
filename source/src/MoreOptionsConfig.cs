using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using EnhancedMission;

namespace EnhancedMissionMoreOptionsPlugin
{
    public class MoreOptionsConfig : EnhancedMissionConfigBase<MoreOptionsConfig>
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
        private static MoreOptionsConfig _instance;
        public string ConfigVersion { get; set; } = BinaryVersion.ToString();

        public bool UseRealisticBlocking = false;

        public bool ChangeMeleeAI = false;

        public bool ChangeRangedAI = false;

        public int MeleeAI = 100;
        public int RangedAI = 100;
        private static MoreOptionsConfig CreateDefault()
        {
            return new MoreOptionsConfig();
        }


        public static MoreOptionsConfig Get()
        {
            if (_instance == null)
            {
                _instance = CreateDefault();
                _instance.SyncWithSave();
            }

            return _instance;
        }

        protected override XmlSerializer serializer => new XmlSerializer(typeof(MoreOptionsConfig));


        protected override void CopyFrom(MoreOptionsConfig other)
        {
            ConfigVersion = other.ConfigVersion;
            UseRealisticBlocking = other.UseRealisticBlocking;
            ChangeMeleeAI = other.ChangeMeleeAI;
            ChangeRangedAI = other.ChangeRangedAI;
            MeleeAI = other.MeleeAI;
            RangedAI = other.RangedAI;
        }

        public override void ResetToDefault()
        {
            CopyFrom(CreateDefault());
        }

        protected override string SaveName => SavePath + nameof(MoreOptionsConfig) + ".xml";
        protected override string[] OldNames { get; } = { };

    }
}
