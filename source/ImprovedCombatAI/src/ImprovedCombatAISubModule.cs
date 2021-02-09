using System.Linq;
using ImprovedCombatAI.Config;
using MissionLibrary;
using MissionLibrary.Controller;
using MissionLibrary.View;
using MissionSharedLibrary;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ImprovedCombatAI
{
    public class ImprovedCombatAISubModule : MBSubModuleBase
    {
        public static readonly string ModuleId = "ImprovedCombatAI";

        protected override void OnSubModuleLoad()
        {
            base.OnSubModuleLoad();


            Initialize();
            Module.CurrentModule.GlobalTextManager.LoadGameTexts(
                BasePath.Name + $"Modules/{ModuleId}/ModuleData/module_strings.xml");
            Module.CurrentModule.GlobalTextManager.LoadGameTexts(
                BasePath.Name + $"Modules/{ModuleId}/ModuleData/MissionLibrary.xml");
        }

        private void Initialize()
        {
            if (!Initializer.Initialize(ModuleId))
                return;
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();

            if (!SecondInitialize())
                return;
        }

        private bool SecondInitialize()
        {
            if (!Initializer.SecondInitialize())
                return false;
            
            var menuClassCollection = AMenuManager.Get().MenuClassCollection;
            AMenuManager.Get().OnMenuClosedEvent += ImprovedCombatAIConfig.OnMenuClosed;
            menuClassCollection.AddOptionClass(
                ImprovedCombatAIOptionClassFactory.CreateOptionClassProvider(menuClassCollection));
            return true;
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            game.GameTextManager.LoadGameTexts(BasePath.Name + $"Modules/{ModuleId}/ModuleData/module_strings.xml");
            game.GameTextManager.LoadGameTexts(BasePath.Name + $"Modules/{ModuleId}/ModuleData/MissionLibrary.xml");
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
