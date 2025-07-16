using System;
using ImprovedCombatAI.Config;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ImprovedCombatAI
{
    public class ImprovedCombatAIAgentStatCalculateModel : AgentStatCalculateModel
    {
        private readonly AgentStatCalculateModel _previousModel;
        public ImprovedCombatAIAgentStatCalculateModel(AgentStatCalculateModel previousModel)
        {
            _previousModel = previousModel;
        }

        public override void InitializeAgentStats(Agent agent, Equipment spawnEquipment, AgentDrivenProperties agentDrivenProperties,
            AgentBuildData agentBuildData)
        {
            _previousModel?.InitializeAgentStats(agent, spawnEquipment, agentDrivenProperties, agentBuildData);
        }

        public override void UpdateAgentStats(Agent agent, AgentDrivenProperties agentDrivenProperties)
        {
            _previousModel?.UpdateAgentStats(agent, agentDrivenProperties);
            var config = ImprovedCombatAIConfig.Get();
            if (agent.IsHuman && (config.ApplyTo == ApplyTo.All || config.ApplyTo == ApplyTo.HeroOnly && agent.IsHero))
            {
                EnhancedSetAiRelatedProperties(agent, agentDrivenProperties);
            }
        }

        public override float GetDifficultyModifier()
        {
            return _previousModel?.GetDifficultyModifier() ?? 0.5f;
        }

        public override void InitializeMissionEquipment(Agent agent)
        {
            base.InitializeMissionEquipment(agent);

            _previousModel?.InitializeMissionEquipment(agent);
        }

        public override float GetEffectiveMaxHealth(Agent agent)
        {
            return _previousModel?.GetEffectiveMaxHealth(agent) ?? base.GetEffectiveMaxHealth(agent);
        }

        public override int GetEffectiveSkill(Agent agent, SkillObject skill)
        {
            return base.GetEffectiveSkill(agent, skill);
        }

        public override int GetEffectiveSkillForWeapon(Agent agent, WeaponComponentData weapon)
        {
            return base.GetEffectiveSkillForWeapon(agent, weapon);
        }

        public override float GetInteractionDistance(Agent agent)
        {
            return _previousModel?.GetInteractionDistance(agent) ?? base.GetInteractionDistance(agent);
        }

        public override float GetMaxCameraZoom(Agent agent)
        {
            return _previousModel?.GetMaxCameraZoom(agent) ?? base.GetMaxCameraZoom(agent);
        }

        public override string GetMissionDebugInfoForAgent(Agent agent)
        {
            return _previousModel?.GetMissionDebugInfoForAgent(agent) ?? base.GetMissionDebugInfoForAgent(agent);
        }

        public override float GetWeaponInaccuracy(Agent agent, WeaponComponentData weapon, int weaponSkill)
        {
            return _previousModel?.GetWeaponInaccuracy(agent, weapon, weaponSkill) ?? base.GetWeaponInaccuracy(agent, weapon, weaponSkill);
        }

        private void EnhancedSetAiRelatedProperties(
            Agent agent,
            AgentDrivenProperties agentDrivenProperties)
        {
            MissionEquipment equipment = agent.Equipment;
            EquipmentIndex mainHandItemIndex = agent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
            MissionWeapon missionWeapon;
            WeaponComponentData mainHandWeapon;
            if (mainHandItemIndex == EquipmentIndex.None)
            {
                mainHandWeapon = null;
            }
            else
            {
                missionWeapon = equipment[mainHandItemIndex];
                mainHandWeapon = missionWeapon.CurrentUsageItem;
            }

            EquipmentIndex offHandItemIndex = agent.GetWieldedItemIndex(Agent.HandIndex.OffHand);
            WeaponComponentData offHandWeapon;
            if (offHandItemIndex == EquipmentIndex.None)
            {
                offHandWeapon = null;
            }
            else
            {
                missionWeapon = equipment[offHandItemIndex];
                offHandWeapon = missionWeapon.CurrentUsageItem;
            }
            var config = ImprovedCombatAIConfig.Get();
            float meleeAILevel;
            if (config.DirectlySetMeleeAI)
            {
                meleeAILevel = MathF.Clamp(config.MeleeAIDifficulty / 100.0f, 0, 1);
            }
            else
            {
                int meleeSkill = GetMeleeSkill(agent, mainHandWeapon, offHandWeapon);
                meleeAILevel = CalculateAILevel(agent, meleeSkill);
                meleeAILevel = MathF.Clamp(meleeAILevel / Math.Max(1 - config.MeleeAIDifficulty / 100f, 0.001f), 0, 1);
            }

            float rangedAILevel;
            if (config.DirectlySetRangedAI)
            {
                rangedAILevel = MathF.Clamp(config.RangedAIDifficulty / 100.0f, 0, 1);
            }
            else
            {
                SkillObject skill = mainHandWeapon == null ? DefaultSkills.Athletics : mainHandWeapon.RelevantSkill;
                int weaponSkill = GetEffectiveSkill(agent, skill);
                rangedAILevel = CalculateAILevel(agent, weaponSkill);
                rangedAILevel = MathF.Clamp(rangedAILevel / Math.Max(1 - config.RangedAIDifficulty / 100f, 0.001f), 0, 1);
            }

            float num1 = meleeAILevel + agent.Defensiveness;
            agentDrivenProperties.AiRangedHorsebackMissileRange = (float)(0.3f + 0.4f * rangedAILevel);
            agentDrivenProperties.AiFacingMissileWatch = (float)(meleeAILevel * 0.06f - 0.96f);
            agentDrivenProperties.AiFlyingMissileCheckRadius = (float)(8.0 - 6.0 * meleeAILevel);
            agentDrivenProperties.AiShootFreq = (float)(0.3f + 0.7f * rangedAILevel);
            agentDrivenProperties.AiWaitBeforeShootFactor = agent.PropertyModifiers.resetAiWaitBeforeShootFactor ? 0.0f : (float)(1.0 - 0.5 * rangedAILevel);
            agentDrivenProperties.AIBlockOnDecideAbility = MBMath.Lerp(0.5f, 0.99f, MBMath.ClampFloat((float)Math.Pow(meleeAILevel, 0.5f), 0.0f, 1f));
            agentDrivenProperties.AIParryOnDecideAbility = MBMath.Lerp(0.5f, 0.95f, MBMath.ClampFloat((float)meleeAILevel, 0.0f, 1f));
            agentDrivenProperties.AiTryChamberAttackOnDecide = (float)((meleeAILevel - 0.15f) * 0.1f);
            agentDrivenProperties.AIAttackOnParryChance = (float)(0.3f - 0.1f * agent.Defensiveness);
            agentDrivenProperties.AiAttackOnParryTiming = (float)(0.3f * meleeAILevel - 0.2f);
            agentDrivenProperties.AIDecideOnAttackChance = 0.5f * agent.Defensiveness;
            agentDrivenProperties.AIParryOnAttackAbility = MBMath.ClampFloat((float)meleeAILevel, 0.0f, 1f);
            agentDrivenProperties.AiKick = (float)(meleeAILevel - 0.1f);
            agentDrivenProperties.AiAttackCalculationMaxTimeFactor = meleeAILevel;
            agentDrivenProperties.AiDecideOnAttackWhenReceiveHitTiming = (float)(-0.25 * (1.0 - meleeAILevel));
            agentDrivenProperties.AiDecideOnAttackContinueAction = (float)(-0.5 * (1.0 - meleeAILevel));
            agentDrivenProperties.AiDecideOnAttackingContinue = 0.1f * meleeAILevel;
            agentDrivenProperties.AIParryOnAttackingContinueAbility = MBMath.Lerp(0.05f, 0.95f, MBMath.ClampFloat((float)meleeAILevel, 0.0f, 1f));
            agentDrivenProperties.AIDecideOnRealizeEnemyBlockingAttackAbility = MBMath.ClampFloat((float)Math.Pow(meleeAILevel, 2.5) - 0.1f, 0.0f, 1f);
            agentDrivenProperties.AIRealizeBlockingFromIncorrectSideAbility = MBMath.ClampFloat((float)Math.Pow(meleeAILevel, 2.5) - 0.1f, 0.0f, 1f);
            agentDrivenProperties.AiAttackingShieldDefenseChance = (float)(0.2f + 0.3f * meleeAILevel);
            agentDrivenProperties.AiAttackingShieldDefenseTimer = (float)(0.3f * meleeAILevel - 0.3f);
            agentDrivenProperties.AiRandomizedDefendDirectionChance = (float)(1.0 - MathF.Pow(meleeAILevel, 3f));
            agentDrivenProperties.AiShooterError = 0.008f;
            agentDrivenProperties.AISetNoAttackTimerAfterBeingHitAbility = MBMath.Lerp(0.33f, 1f, meleeAILevel);
            agentDrivenProperties.AISetNoAttackTimerAfterBeingParriedAbility = MBMath.Lerp(0.2f, 1f, meleeAILevel * meleeAILevel);
            agentDrivenProperties.AISetNoDefendTimerAfterHittingAbility = MBMath.Lerp(0.1f, 0.99f, meleeAILevel * meleeAILevel);
            agentDrivenProperties.AISetNoDefendTimerAfterParryingAbility = MBMath.Lerp(0.15f, 1f, meleeAILevel * meleeAILevel);
            agentDrivenProperties.AIEstimateStunDurationPrecision = 1f - MBMath.Lerp(0.2f, 1f, meleeAILevel);
            agentDrivenProperties.AIHoldingReadyMaxDuration = MBMath.Lerp(0.25f, 0.0f, Math.Min(1f, meleeAILevel * 2f));
            agentDrivenProperties.AIHoldingReadyVariationPercentage = meleeAILevel;
            agentDrivenProperties.AiRaiseShieldDelayTimeBase = (float)(0.5 * meleeAILevel - 0.75);
            agentDrivenProperties.AiUseShieldAgainstEnemyMissileProbability = (float)(0.100000001490116 + meleeAILevel * 0.6f + num1 * 0.2f);
            agentDrivenProperties.AiCheckMovementIntervalFactor = (float)(0.005f * (1.1f - meleeAILevel));
            agentDrivenProperties.AiMovementDelayFactor = (float)(4.0 / (3.0 + rangedAILevel));
            agentDrivenProperties.AiParryDecisionChangeValue = (float)(0.05f + 0.7f * meleeAILevel);
            agentDrivenProperties.AiDefendWithShieldDecisionChanceValue = Math.Min(2f, (float)(0.2f + 0.5 * meleeAILevel + 0.6f * num1));
            agentDrivenProperties.AiMoveEnemySideTimeValue = (float)(0.5 * meleeAILevel - 2.5);
            agentDrivenProperties.AiMinimumDistanceToContinueFactor = (float)(2.0 + 0.3f * (3.0 - meleeAILevel));
            agentDrivenProperties.AiHearingDistanceFactor = 1f + meleeAILevel;
            agentDrivenProperties.AiChargeHorsebackTargetDistFactor = (float)(1.5 * (3.0 - meleeAILevel));
            agentDrivenProperties.AiWaitBeforeShootFactor = agent.PropertyModifiers.resetAiWaitBeforeShootFactor ? 0.0f : (float)(1.0 - 0.5 * rangedAILevel);
            float num2 = 1f - rangedAILevel;
            agentDrivenProperties.AiRangerLeadErrorMin = (float)(-(double)num2 * 0.35f) + config.RangedError;
            agentDrivenProperties.AiRangerLeadErrorMax = num2 * 0.2f + config.RangedError;
            agentDrivenProperties.AiRangerVerticalErrorMultiplier = num2 * 0.1f;
            agentDrivenProperties.AiRangerHorizontalErrorMultiplier = num2 * ((float)Math.PI / 90f);
            if (config.OverrideDesireToAttack)
                agentDrivenProperties.AIAttackOnDecideChance = MathF.Clamp((float)(0.3f * meleeAILevel * (3.0 - agent.Defensiveness)), 0.05f, 1f);
            if (config.UseRealisticBlocking)
                agentDrivenProperties.SetStat(DrivenProperty.UseRealisticBlocking, 1f);
        }

        public override bool CanAgentRideMount(Agent agent, Agent targetMount)
        {
            return _previousModel.CanAgentRideMount(agent, targetMount);
        }

        public override float GetWeaponDamageMultiplier(Agent agent, WeaponComponentData weapon)
        {
            return _previousModel.GetWeaponDamageMultiplier(agent, weapon);
        }

        public override float GetKnockBackResistance(Agent agent)
        {
            return _previousModel.GetKnockBackResistance(agent);
        }

        public override float GetKnockDownResistance(Agent agent, StrikeType strikeType = StrikeType.Invalid)
        {
            return _previousModel.GetKnockDownResistance(agent, strikeType);
        }

        public override float GetDismountResistance(Agent agent)
        {
            return _previousModel.GetDismountResistance(agent);
        }
    }
}
