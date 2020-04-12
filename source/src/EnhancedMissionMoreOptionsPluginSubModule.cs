using System;
using System.Collections.Generic;
using System.Text;
using EnhancedMission;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace EnhancedMissionMoreOptionsPlugin
{
    public class EnhancedMissionMoreOptionsPluginSubModule : MBSubModuleBase
    {

        public override void BeginGameStart(Game game)
        {
            base.BeginGameStart(game);

            EnhancedMissionGameModels.ReplaceModel();
            ChangeBodyPropertiesBase.SetInstance(new ChangeBodyProperties());
        }

        public override void OnGameEnd(Game game)
        {
            base.OnGameEnd(game);

            ChangeBodyPropertiesBase.SetInstance(null);
            EnhancedMissionGameModels.Clear();
        }
    }
}
