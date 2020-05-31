using TaleWorlds.MountAndBlade;

namespace ImprovedCombatAI
{
    public class ChangeBodyProperties
    {
        private static ChangeBodyProperties _instance;
        private ImprovedCombatAI _config;

        public static ChangeBodyProperties Get()
        {
            if (_instance == null)
                _instance = new ChangeBodyProperties();
            return _instance;
        }
        private ChangeBodyProperties()
        {
            _config = ImprovedCombatAI.Get();
        }

        public void SaveConfig()
        {
            _config.Serialize();
        }

        public bool UseRealisticBlocking
        {
            get => _config.UseRealisticBlocking;
            set
            {
                if (_config.UseRealisticBlocking == value)
                    return;
                _config.UseRealisticBlocking = value;
                UpdateAgentProperties();
            }
        }

        public int MeleeAIDifficulty
        {
            get => _config.MeleeAIDifficulty;
            set
            {
                if (_config.MeleeAIDifficulty == value)
                    return;
                _config.MeleeAIDifficulty = value;
                UpdateAgentProperties();
            }
        }

        public bool ChangeMeleeAI
        {
            get => _config.ChangeMeleeAI;
            set
            {
                if (_config.ChangeMeleeAI == value)
                    return;
                _config.ChangeMeleeAI = value;
                UpdateAgentProperties();
            }
        }
        public int MeleeAI
        {
            get => _config.MeleeAI;
            set
            {
                if (_config.MeleeAI == value)
                    return;
                _config.MeleeAI = value;
                UpdateAgentProperties();
            }
        }

        public int RangedAIDifficulty
        {
            get => _config.RangedAIDifficulty;
            set
            {
                if (_config.RangedAIDifficulty == value)
                    return;
                _config.RangedAIDifficulty = value;
                UpdateAgentProperties();
            }
        }
        public bool ChangeRangedAI
        {
            get => _config.ChangeRangedAI;
            set
            {
                if (_config.ChangeRangedAI == value)
                    return;
                _config.ChangeRangedAI = value;
                UpdateAgentProperties();
            }
        }
        public int RangedAI
        {
            get => _config.RangedAI;
            set
            {
                if (_config.RangedAI == value)
                    return;
                _config.RangedAI = value;
                UpdateAgentProperties();
            }
        }
        public float RangedError
        {
            get => _config.RangedError;
            set
            {
                _config.RangedError = value;
                UpdateAgentProperties();
            }
        }

        private void UpdateAgentProperties()
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
