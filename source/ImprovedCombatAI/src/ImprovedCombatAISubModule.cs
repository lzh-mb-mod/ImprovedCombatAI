using ImprovedCombatAI.Config;
using ImprovedCombatAI.Usage;
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
            Module.CurrentModule.GlobalTextManager.LoadGameTexts();
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

        protected override void OnApplicationTick(float dt)
        {
            base.OnApplicationTick(dt);

            Initializer.OnApplicationTick(dt);
        }


        private bool SecondInitialize()
        {
            if (!Initializer.SecondInitialize())
                return false;

            ImprovedCombatAIUsageCategory.RegisterUsageCategory();
            var menuClassCollection = AMenuManager.Get().MenuClassCollection;
            AMenuManager.Get().OnMenuClosedEvent += ImprovedCombatAIConfig.OnMenuClosed;
            menuClassCollection.RegisterItem(
                ImprovedCombatAIOptionClassFactory.CreateOptionClassProvider(menuClassCollection));
            return true;
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);

            game.GameTextManager.LoadGameTexts();
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
