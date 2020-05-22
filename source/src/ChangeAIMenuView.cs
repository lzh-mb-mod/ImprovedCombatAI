using RTSCamera;

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
