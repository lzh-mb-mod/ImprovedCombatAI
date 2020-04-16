using System;
using System.Collections.Generic;
using System.Text;
using EnhancedMission;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.MountAndBlade;

namespace EnhancedMissionChangeAI
{
    public class ChangeAIMenuView : MissionMenuViewBase
    {

        public ChangeAIMenuView()
            : base(25, nameof(ChangeAIMenuView))
        {
            this.GetDataSource = () => new ChangeAIMenuVM(this.OnCloseMenu);
        }
    }
}
