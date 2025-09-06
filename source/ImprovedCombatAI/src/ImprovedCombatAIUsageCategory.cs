using MissionLibrary.Usage;
using MissionSharedLibrary.HotKey;
using MissionSharedLibrary.Usage;
using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace ImprovedCombatAI.Usage
{
    public class ImprovedCombatAIUsageCategory
    {
        public const string CategoryId = "ImprovedCombatAIUsage";

        public static AUsageCategory Category => AUsageCategoryManager.Get().GetItem(CategoryId);

        public static void RegisterUsageCategory()
        {
            AUsageCategoryManager.Get()?.RegisterItem(CreateCategory, CategoryId, new Version(1, 0));
        }

         public static UsageCategory CreateCategory()
        {
            var usageCategoryData = new UsageCategoryData(
                GameTexts.FindText("str_improved_combat_ai_option_class"),
                new List<TaleWorlds.Localization.TextObject>
                {
                    GameTexts.FindText("str_mission_library_open_menu_hint").SetTextVariable("KeyName",
                        GeneralGameKeyCategory.GetKey(GeneralGameKey.OpenMenu).ToSequenceString()),
                });

            return new UsageCategory(CategoryId, usageCategoryData);
        }
    }
}
