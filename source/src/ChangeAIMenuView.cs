using RTSCamera.View.Basic;

namespace ImprovedCombatAI
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
