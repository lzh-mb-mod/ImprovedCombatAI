using HarmonyLib;
using ImprovedCombatAI.Config;
using MissionSharedLibrary.Utilities;
using System;
using System.Reflection;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ImprovedCombatAI.Patch
{
    public class Patch_HumanAIComponent
    {
        private static readonly Harmony Harmony = new Harmony(ImprovedCombatAISubModule.ModuleId + "_" + nameof(Patch_HumanAIComponent));

        private static bool _patched;
        public static bool Patch()
        {
            try
            {
                if (_patched)
                    return false;
                _patched = true;
                Harmony.Patch(
                    typeof(HumanAIComponent).GetMethod("GetFormationFrame",
                        BindingFlags.Instance | BindingFlags.NonPublic),
                    prefix: new HarmonyMethod(typeof(Patch_HumanAIComponent).GetMethod(nameof(Prefix_GetFormationFrame),
                        BindingFlags.Static | BindingFlags.Public)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Utility.DisplayMessage(e.ToString());
                return false;
            }

            return true;
        }

        public static bool Prefix_GetFormationFrame(
            ref HumanAIComponent __instance,
            ref bool __result,
            ref WorldPosition formationPosition,
            ref Vec2 formationDirection,
            ref float speedLimit,
            ref bool isSettingDestinationSpeed,
            ref bool limitIsMultiplier,
            bool finalDestination,
            Agent ___Agent)
        {
            if (!ImprovedCombatAIConfig.Get().EnableTacticalAI)
                return true;
            var formation = ___Agent.Formation;
            if (!___Agent.IsMount && formation != null &&
                !___Agent.IsDetachedFromFormation)
            {
                if (formation.GetReadonlyMovementOrderReference().OrderEnum == MovementOrder.MovementOrderEnum.Charge)
                {
                    if (QueryLibrary.IsInfantry(___Agent))
                    {
                        var targetAgent = ___Agent.GetTargetAgent();
                        if (targetAgent == null)
                            return true;
                        Utilities.Utility.GetTargetFrameOfInfantry(ref formationPosition, ref formationDirection, ___Agent, targetAgent);
                        limitIsMultiplier = true;
                        speedLimit = HumanAIComponent.FormationSpeedAdjustmentEnabled ? __instance.GetDesiredSpeedInFormation(true) : -1f;
                        __result = formationPosition.IsValid;
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
