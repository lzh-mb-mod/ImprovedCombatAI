using HarmonyLib;
using ImprovedCombatAI.Config;
using MissionSharedLibrary.Utilities;
using System;
using System.Reflection;
using TaleWorlds.MountAndBlade;

namespace ImprovedCombatAI.Patch
{
    public class Patch_MovementOrder
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
                    typeof(MovementOrder).GetMethod("SetChargeBehaviorValues",
                        BindingFlags.Static | BindingFlags.NonPublic),
                    prefix: new HarmonyMethod(typeof(Patch_MovementOrder).GetMethod(nameof(Prefix_SetChargeBehaviorValues),
                        BindingFlags.Static | BindingFlags.Public), Priority.First));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Utility.DisplayMessage(e.ToString());
                return false;
            }

            return true;
        }

        public static bool Prefix_SetChargeBehaviorValues(Agent unit)
        {
            if (!ImprovedCombatAIConfig.Get().EnableTacticalAI)
                return true;
            if (!unit.IsMount &&
                !unit.IsDetachedFromFormation && unit.Formation != null &&
                unit.Formation.GetReadonlyMovementOrderReference().OrderEnum == MovementOrder.MovementOrderEnum.Charge)
            {
                if (QueryLibrary.IsInfantry(unit))
                {
                    unit.SetAIBehaviorValues(HumanAIComponent.AISimpleBehaviorKind.GoToPos, 3, 10, 5, 50, 12);
                    unit.SetAIBehaviorValues(HumanAIComponent.AISimpleBehaviorKind.Melee, 7, 2, 3.75f, 5, 2.4f);
                    unit.SetAIBehaviorValues(HumanAIComponent.AISimpleBehaviorKind.Ranged, 0.01f, 10, 5, 20, 15f);
                    unit.SetAIBehaviorValues(HumanAIComponent.AISimpleBehaviorKind.ChargeHorseback, 11, 10, 10.7f, 60, 9);
                    unit.SetAIBehaviorValues(HumanAIComponent.AISimpleBehaviorKind.RangedHorseback, 0.01f, 7, 5, 8, 15);
                    unit.SetAIBehaviorValues(HumanAIComponent.AISimpleBehaviorKind.AttackEntityMelee, 0.5f, 12f, 0.6f, 30f, 0.4f);
                    unit.SetAIBehaviorValues(HumanAIComponent.AISimpleBehaviorKind.AttackEntityRanged, 0.55f, 12f, 0.8f, 30f, 0.45f);
                    return false;
                }
            }

            return true;
        }
    }
}
