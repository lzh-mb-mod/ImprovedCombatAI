using RTSCamera;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace ImprovedCombatAI
{
    class ChangeAIExtension : RTSCameraExtension
    {
        public override void OpenModMenu(Mission mission)
        {
        }

        public override void CloseModMenu(Mission mission)
        {
        }

        public override void OpenExtensionMenu(Mission mission)
        {
            mission.GetMissionBehaviour<ChangeAIMenuView>()?.ActivateMenu();
        }

        public override List<MissionBehaviour> CreateMissionBehaviours(Mission mission)
        {
            return new List<MissionBehaviour>
            {
                new ChangeAIMenuView(),
            };
        }

        public override string ExtensionName => "Change AI";

        public override string ButtonName => GameTexts.FindText("str_extension_change_ai").ToString();
    }
}
