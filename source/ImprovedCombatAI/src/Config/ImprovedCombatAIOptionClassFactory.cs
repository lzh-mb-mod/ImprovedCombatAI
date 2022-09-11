using MissionLibrary.Provider;
using MissionLibrary.View;
using MissionSharedLibrary.Provider;
using MissionSharedLibrary.View.ViewModelCollection;
using MissionSharedLibrary.View.ViewModelCollection.Options;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace ImprovedCombatAI.Config
{
    public class ImprovedCombatAIOptionClassFactory
    {
        public static IIdProvider<AOptionClass> CreateOptionClassProvider(IMenuClassCollection menuClassCollection)
        {
            return IdProviderCreator.Create(() =>
            {
                var optionClass = new OptionClass(ImprovedCombatAISubModule.ModuleId,
                    GameTexts.FindText("str_improved_combat_ai_option_class"), menuClassCollection);

                var optionCategory = new OptionCategory("CombatAI", GameTexts.FindText("str_improved_combat_ai_ai_options"));
                optionCategory.AddOption(new BoolOptionViewModel(
                    GameTexts.FindText("str_improved_combat_ai_use_realistic_blocking"), null,
                    () => ImprovedCombatAIConfig.Get().UseRealisticBlocking,
                    b => ImprovedCombatAIConfig.Get().UseRealisticBlocking = b));
                optionCategory.AddOption(new NumericOptionViewModel(
                    GameTexts.FindText("str_improved_combat_ai_melee_ai_difficulty"), null,
                    () => ImprovedCombatAIConfig.Get().MeleeAIDifficulty,
                    f => ImprovedCombatAIConfig.Get().MeleeAIDifficulty = (int) f, 0, 100, true, true));
                optionCategory.AddOption(new BoolOptionViewModel(
                    GameTexts.FindText("str_improved_combat_ai_directly_set_melee_ai"), GameTexts.FindText("str_improved_combat_ai_directly_set_melee_ai_description"),
                    () => ImprovedCombatAIConfig.Get().DirectlySetMeleeAI,
                    b => ImprovedCombatAIConfig.Get().DirectlySetMeleeAI = b));
                optionCategory.AddOption(new NumericOptionViewModel(
                    GameTexts.FindText("str_improved_combat_ai_ranged_ai_difficulty"), null,
                    () => ImprovedCombatAIConfig.Get().RangedAIDifficulty,
                    f => ImprovedCombatAIConfig.Get().RangedAIDifficulty = (int)f, 0, 100, true, true));
                optionCategory.AddOption(new BoolOptionViewModel(
                    GameTexts.FindText("str_improved_combat_ai_directly_set_ranged_ai"), GameTexts.FindText("str_improved_combat_ai_directly_set_ranged_ai_description"),
                    () => ImprovedCombatAIConfig.Get().DirectlySetRangedAI,
                    b => ImprovedCombatAIConfig.Get().DirectlySetRangedAI = b));
                optionCategory.AddOption(new BoolOptionViewModel(
                    new TextObject("Enable Tactical AI"), null, () => ImprovedCombatAIConfig.Get().EnableTacticalAI,
                    b => ImprovedCombatAIConfig.Get().EnableTacticalAI = b));
                optionCategory.AddOption(new BoolOptionViewModel(new TextObject("EnableAiMoveEnemySideTimeValue"), null,
                    () => ImprovedCombatAIConfig.Get().EnableAiMoveEnemySideTimeValue,
                    b => ImprovedCombatAIConfig.Get().EnableAiMoveEnemySideTimeValue = b));
                optionCategory.AddOption(new NumericOptionViewModel(new TextObject("AiMoveEnemySideTimeValue"), null,
                    () => ImprovedCombatAIConfig.Get().AiMoveEnemySideTimeValue,
                    f => ImprovedCombatAIConfig.Get().AiMoveEnemySideTimeValue = f, -5, 5, false, true));
                optionCategory.AddOption(new BoolOptionViewModel(new TextObject("EnableAiMinimumDistanceToContinueFactor"), null,
                    () => ImprovedCombatAIConfig.Get().EnableAiMinimumDistanceToContinueFactor,
                    b => ImprovedCombatAIConfig.Get().EnableAiMinimumDistanceToContinueFactor = b));
                optionCategory.AddOption(new NumericOptionViewModel(new TextObject("AiMinimumDistanceToContinueFactor"), null,
                    () => ImprovedCombatAIConfig.Get().AiMinimumDistanceToContinueFactor,
                    f => ImprovedCombatAIConfig.Get().AiMinimumDistanceToContinueFactor = f, -5, 5, false, true));
                optionCategory.AddOption(new BoolOptionViewModel(new TextObject("EnableAiStandGroundTimerValue"), null,
                    () => ImprovedCombatAIConfig.Get().EnableAiStandGroundTimerValue,
                    b => ImprovedCombatAIConfig.Get().EnableAiStandGroundTimerValue = b));
                optionCategory.AddOption(new NumericOptionViewModel(new TextObject("AiStandGroundTimerValue"), null,
                    () => ImprovedCombatAIConfig.Get().AiStandGroundTimerValue,
                    f => ImprovedCombatAIConfig.Get().AiStandGroundTimerValue = f, -5, 5, false, true));
                optionCategory.AddOption(new BoolOptionViewModel(new TextObject("EnableAiStandGroundTimerMoveAlongValue"), null,
                    () => ImprovedCombatAIConfig.Get().EnableAiStandGroundTimerMoveAlongValue,
                    b => ImprovedCombatAIConfig.Get().EnableAiStandGroundTimerMoveAlongValue = b));
                optionCategory.AddOption(new NumericOptionViewModel(new TextObject("AiStandGroundTimerMoveAlongValue"), null,
                    () => ImprovedCombatAIConfig.Get().AiStandGroundTimerMoveAlongValue,
                    f => ImprovedCombatAIConfig.Get().AiStandGroundTimerMoveAlongValue = f, -5, 5, false, true));
                optionCategory.AddOption(new BoolOptionViewModel(new TextObject("EnableCheckMovementIntervalFactor"), null,
                    () => ImprovedCombatAIConfig.Get().EnableCheckMovementIntervalFactor,
                    b => ImprovedCombatAIConfig.Get().EnableCheckMovementIntervalFactor = b));
                optionCategory.AddOption(new NumericOptionViewModel(new TextObject("CheckMovementIntervalFactor"), null,
                    () => ImprovedCombatAIConfig.Get().CheckMovementIntervalFactor,
                    f => ImprovedCombatAIConfig.Get().CheckMovementIntervalFactor = f, 0, 1f, false, true));

                optionClass.AddOptionCategory(0, optionCategory);
                return optionClass;
            }, ImprovedCombatAISubModule.ModuleId);
        }
    }
}