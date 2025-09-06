using MissionLibrary.Provider;
using MissionLibrary.View;
using MissionSharedLibrary.Provider;
using MissionSharedLibrary.View.ViewModelCollection;
using MissionSharedLibrary.View.ViewModelCollection.Options;
using MissionSharedLibrary.View.ViewModelCollection.Options.Selection;
using TaleWorlds.Core;

namespace ImprovedCombatAI.Config
{
    public class ImprovedCombatAIOptionClassFactory
    {
        public static IProvider<AOptionClass> CreateOptionClassProvider(AMenuClassCollection menuClassCollection)
        {
            return ProviderCreator.Create(() =>
            {
                var optionClass = new OptionClass(ImprovedCombatAISubModule.ModuleId,
                    GameTexts.FindText("str_improved_combat_ai_option_class"), menuClassCollection);

                var optionCategory = new OptionCategory("CombatAI", GameTexts.FindText("str_improved_combat_ai_ai_options"));
                optionCategory.AddOption(new SelectionOptionViewModel(
                    GameTexts.FindText("str_improved_combat_ai_apply_to"), null,
                    new SelectionOptionData(
                        i => ImprovedCombatAIConfig.Get().ApplyTo = (ApplyTo)i,
                        () => (int)ImprovedCombatAIConfig.Get().ApplyTo,
                        (int)ApplyTo.Count,
                        new[]
                        {
                            new SelectionItem(true, "str_improved_combat_ai_apply_to_option", "None"),
                            new SelectionItem(true, "str_improved_combat_ai_apply_to_option", "HeroOnly"),
                            new SelectionItem(true, "str_improved_combat_ai_apply_to_option", "All")
                        }
                        ), true));
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
                optionCategory.AddOption(new NumericOptionViewModel(
                    GameTexts.FindText("str_improved_combat_ai_lead_error"), null,
                    () => ImprovedCombatAIConfig.Get().LeadingError,
                    b => ImprovedCombatAIConfig.Get().LeadingError = b, 0, 1, false, true));
                optionCategory.AddOption(new BoolOptionViewModel(
                    GameTexts.FindText("str_improved_combat_ai_override_desire_to_attack"), null,
                    () => ImprovedCombatAIConfig.Get().OverrideDesireToAttack,
                    b => ImprovedCombatAIConfig.Get().OverrideDesireToAttack = b));
                optionClass.AddOptionCategory(0, optionCategory);
                return optionClass;
            }, ImprovedCombatAISubModule.ModuleId, new System.Version(1, 0, 0));
        }
    }
}