using System;
using EnhancedMission;
using TaleWorlds.MountAndBlade;

namespace EnhancedMissionMoreOptionsPlugin
{
    public class ChangeBodyProperties : ChangeBodyPropertiesBase
    {
        private MoreOptionsConfig _config;

        public ChangeBodyProperties()
        {
            _config = MoreOptionsConfig.Get();
        }

        public override bool UseRealisticBlocking
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

        public override bool ChangeMeleeAI
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
        public override int MeleeAI
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
        public override bool ChangeRangedAI
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
        public override int RangedAI
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
