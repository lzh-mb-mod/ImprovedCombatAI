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
                optionClass.AddOptionCategory(0, optionCategory);
                return optionClass;
            }, ImprovedCombatAISubModule.ModuleId);
        }
    }
}