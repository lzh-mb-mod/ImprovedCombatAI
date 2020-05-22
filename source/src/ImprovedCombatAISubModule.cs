using RTSCamera;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ImprovedCombatAI
{
    public class ImprovedCombatAISubModule : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();

            Module.CurrentModule.GlobalTextManager.LoadGameTexts(BasePath.Name + "Modules/ImprovedCombatAI/ModuleData/module_strings.xml");
            RTSCameraExtension.AddExtension(new ChangeAIExtension());
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            game.GameTextManager.LoadGameTexts(BasePath.Name + "Modules/ImprovedCombatAI/ModuleData/module_strings.xml");
            gameStarterObject.AddModel(new ImprovedCombatAIAgentStatCalculateModel(GetGameModel<AgentStatCalculateModel>(gameStarterObject)));
        }

        private T GetGameModel<T>(IGameStarter gameStarter) where T : GameModel
        {
            GameModel[] gameModels = gameStarter.Models.ToArray();
            for (int index = gameModels.Length - 1; index >= 0; --index)
            {
                if (gameModels[index] is T gameModel)
                    return gameModel;
            }
            return default(T);
        }
    }
}
