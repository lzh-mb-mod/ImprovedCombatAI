using System;
using System.Collections.Generic;
using System.Text;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace EnhancedMissionMoreOptionsPlugin
{
    public class EnhancedMissionGameModels
    {
        public static MissionGameModels missionGameModels;

        public static void ReplaceModel()
        {
            var current = MissionGameModels.Current;
            missionGameModels = new MissionGameModels(new GameModel[]
            {
                current.AgentApplyDamageModel, current.AgentDecideKilledOrUnconsciousModel,
                current.ApplyWeatherEffectsModel, current.BattleMoraleModel,
                new EnhancedAgentStatCalculateModel(current.AgentStatCalculateModel)
            });
        }
        public static void Clear()
        {
            missionGameModels = null;
        }
    }
}
