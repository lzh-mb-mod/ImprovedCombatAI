using ImprovedCombatAI.Config;
using ImprovedCombatAI.Usage;
using MissionLibrary.View;
using MissionSharedLibrary;
using System;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.Engine;
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
        }

        private void Initialize()
        {
            if (!Initializer.Initialize(ModuleId))
                return;
        }

        protected override void OnApplicationTick(float dt)
        {
            base.OnApplicationTick(dt);

            Initializer.OnApplicationTick(dt);
        }

        protected override void OnBeforeInitialModuleScreenSetAsRoot()
        {
            base.OnBeforeInitialModuleScreenSetAsRoot();

            if (!ThirdInitialize())
                return;
            try
            {
                Module.CurrentModule.GlobalTextManager.LoadGameTexts();
            }
            catch (Exception e)
            {
                MBDebug.Print(e.ToString());
                InformationManager.DisplayMessage(new InformationMessage($"RTS Camera: failed to load game texts: {e}"));
            }
        }


        private bool ThirdInitialize()
        {
            if (!Initializer.ThirdInitialize())
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
            gameStarterObject.AddModel(new ImprovedCombatAIAgentStatCalculateModel());
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
