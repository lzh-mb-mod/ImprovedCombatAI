using ImprovedCombatAI.Config;
using MissionLibrary.View;
using MissionSharedLibrary;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.ModuleManager;
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
                ModuleHelper.GetXmlPath(ModuleId, "module_strings"));
            Module.CurrentModule.GlobalTextManager.LoadGameTexts(
                ModuleHelper.GetXmlPath(ModuleId, "MissionLibrary"));
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

            game.GameTextManager.LoadGameTexts(ModuleHelper.GetXmlPath(ModuleId, "module_strings"));
            game.GameTextManager.LoadGameTexts(ModuleHelper.GetXmlPath(ModuleId, "MissionLibrary"));
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
