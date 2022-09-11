using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ImprovedCombatAI.Utilities
{
    public class Utility
    {
        public static float GetAppropriateDistance(Agent agent, Agent targetAgent)
        {
            var weaponLength = agent.GetCurWeaponOffset().z;
            var currentWeapon = agent.WieldedWeapon;
            if (!currentWeapon.IsEmpty)
            {
                weaponLength += agent.WieldedWeapon.CurrentUsageItem.GetRealWeaponLength();
            }

            return weaponLength + agent.Monster.ArmLength * agent.AgentScale;
        }

        public static void GetTargetFrameOfInfantry(
            ref WorldPosition formationPosition,
            ref Vec2 formationDirection,
            Agent agent, Agent targetAgent)
        {
            
            var selfPosition = agent.GetWorldPosition();
            var selfPositionVec3 = agent.Position;
            if (!selfPositionVec3.IsValid)
            {
                formationPosition = WorldPosition.Invalid;
                return;
            }

            var targetAgentPosition = targetAgent.GetWorldPosition();
            var targetAgentPositionVec3 = targetAgent.Position;
            var middlePosition = Mission.Current.GetStraightPathToTarget(selfPosition.AsVec2,
                targetAgentPosition, 2);

            var middlePosVec3 = middlePosition.GetGroundVec3();
            if (!middlePosVec3.IsValid)
            {
                formationPosition = WorldPosition.Invalid;
                return;
            }

            formationDirection = (targetAgentPositionVec3 - middlePosVec3).AsVec2.Normalized();
            middlePosVec3 = targetAgentPositionVec3 + (middlePosVec3 - targetAgentPositionVec3).NormalizedCopy() *
                GetAppropriateDistance(agent, targetAgent);
            formationPosition = middlePosVec3.ToWorldPosition();
        }
    }
}
