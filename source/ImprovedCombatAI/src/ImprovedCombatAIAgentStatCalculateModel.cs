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
            if (agent.IsHuman)
            {
                EnhancedSetAiRelatedProperties(agent, agentDrivenProperties);
            }
        }

        public override float GetDifficultyModifier()
        {
            return _previousModel?.GetDifficultyModifier() ?? 0;
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

        public override int GetEffectiveSkill(BasicCharacterObject agentCharacter, IAgentOriginBase agentOrigin, Formation agentFormation,
            SkillObject skill)
        {
            return _previousModel?.GetEffectiveSkill(agentCharacter, agentOrigin, agentFormation, skill) ??
                   base.GetEffectiveSkill(agentCharacter, agentOrigin, agentFormation, skill);
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

        //private int GetWeaponSkill(BasicCharacterObject character, WeaponComponentData equippedItem)
        //{
        //    SkillObject skill = DefaultSkills.Athletics;
        //    if (equippedItem != null)
        //        skill = equippedItem.RelevantSkill;
        //    return character.GetSkillValue(skill);
        //}

        //private int GetMeleeSkill(
        //    BasicCharacterObject character,
        //    WeaponComponentData equippedItem,
        //    WeaponComponentData secondaryItem)
        //{
        //    SkillObject skill = DefaultSkills.Athletics;
        //    if (equippedItem != null)
        //    {
        //        SkillObject relevantSkill = equippedItem.RelevantSkill;
        //        skill = relevantSkill == DefaultSkills.OneHanded || relevantSkill == DefaultSkills.Polearm ? relevantSkill : (relevantSkill != DefaultSkills.TwoHanded ? DefaultSkills.OneHanded : (secondaryItem == null ? DefaultSkills.TwoHanded : DefaultSkills.OneHanded));
        //    }
        //    return character.GetSkillValue(skill);
        //}

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
                int weaponSkill = GetEffectiveSkill(agent.Character, agent.Origin, agent.Formation, skill);
                rangedAILevel = CalculateAILevel(agent, weaponSkill);
                rangedAILevel = MathF.Clamp(rangedAILevel / Math.Max(1 - config.RangedAIDifficulty / 100f, 0.001f), 0, 1);
            }

            float num1 = meleeAILevel + agent.Defensiveness;
            agentDrivenProperties.AiRangedHorsebackMissileRange = (float)(0.300000011920929 + 0.400000005960464 * rangedAILevel);
            agentDrivenProperties.AiFacingMissileWatch = (float)(meleeAILevel * 0.0599999986588955 - 0.959999978542328);
            agentDrivenProperties.AiFlyingMissileCheckRadius = (float)(8.0 - 6.0 * meleeAILevel);
            agentDrivenProperties.AiShootFreq = (float)(0.300000011920929 + 0.699999988079071 * rangedAILevel);
            agentDrivenProperties.AiWaitBeforeShootFactor = agent.PropertyModifiers.resetAiWaitBeforeShootFactor ? 0.0f : (float)(1.0 - 0.5 * rangedAILevel);
            agentDrivenProperties.AIBlockOnDecideAbility = MBMath.Lerp(0.25f, 0.99f, MBMath.ClampFloat((float)Math.Pow(meleeAILevel, 1.0), 0.0f, 1f));
            agentDrivenProperties.AIParryOnDecideAbility = MBMath.Lerp(0.01f, 0.95f, MBMath.ClampFloat((float)Math.Pow(meleeAILevel, 1.5), 0.0f, 1f));
            agentDrivenProperties.AiTryChamberAttackOnDecide = (float)((meleeAILevel - 0.150000005960464) * 0.100000001490116);
            agentDrivenProperties.AIAttackOnParryChance = (float)(0.300000011920929 - 0.100000001490116 * agent.Defensiveness);
            agentDrivenProperties.AiAttackOnParryTiming = (float)(0.300000011920929 * meleeAILevel - 0.200000002980232);
            agentDrivenProperties.AIDecideOnAttackChance = 0.15f * agent.Defensiveness;
            agentDrivenProperties.AIParryOnAttackAbility = MBMath.ClampFloat((float)Math.Pow(meleeAILevel, 3.0), 0.0f, 1f);
            agentDrivenProperties.AiKick = (float)(meleeAILevel - 0.100000001490116);
            agentDrivenProperties.AiAttackCalculationMaxTimeFactor = meleeAILevel;
            agentDrivenProperties.AiDecideOnAttackWhenReceiveHitTiming = (float)(-0.25 * (1.0 - meleeAILevel));
            agentDrivenProperties.AiDecideOnAttackContinueAction = (float)(-0.5 * (1.0 - meleeAILevel));
            agentDrivenProperties.AiDecideOnAttackingContinue = 0.1f * meleeAILevel;
            agentDrivenProperties.AIParryOnAttackingContinueAbility = MBMath.Lerp(0.05f, 0.95f, MBMath.ClampFloat((float)Math.Pow(meleeAILevel, 3.0), 0.0f, 1f));
            agentDrivenProperties.AIDecideOnRealizeEnemyBlockingAttackAbility = 0.5f * MBMath.ClampFloat((float)Math.Pow(meleeAILevel, 2.5) - 0.1f, 0.0f, 1f);
            agentDrivenProperties.AIRealizeBlockingFromIncorrectSideAbility = 0.5f * MBMath.ClampFloat((float)Math.Pow(meleeAILevel, 2.5) - 0.1f, 0.0f, 1f);
            agentDrivenProperties.AiAttackingShieldDefenseChance = (float)(0.200000002980232 + 0.300000011920929 * meleeAILevel);
            agentDrivenProperties.AiAttackingShieldDefenseTimer = (float)(0.300000011920929 * meleeAILevel - 0.300000011920929);
            agentDrivenProperties.AiRandomizedDefendDirectionChance = (float)(1.0 - Math.Log(meleeAILevel * 7.0 + 1.0, 2.0) * 0.333330005407333);
            agentDrivenProperties.AiShooterError = 0.008f;
            agentDrivenProperties.AISetNoAttackTimerAfterBeingHitAbility = MBMath.ClampFloat((float)Math.Pow(meleeAILevel, 2.0), 0.05f, 0.95f);
            agentDrivenProperties.AISetNoAttackTimerAfterBeingParriedAbility = MBMath.ClampFloat((float)Math.Pow(meleeAILevel, 2.0), 0.05f, 0.95f);
            agentDrivenProperties.AISetNoDefendTimerAfterHittingAbility = MBMath.ClampFloat((float)Math.Pow(meleeAILevel, 2.0), 0.05f, 0.95f);
            agentDrivenProperties.AISetNoDefendTimerAfterParryingAbility = MBMath.ClampFloat((float)Math.Pow(meleeAILevel, 2.0), 0.05f, 0.95f);
            agentDrivenProperties.AIEstimateStunDurationPrecision = 1f - MBMath.ClampFloat((float)Math.Pow(meleeAILevel, 2.0), 0.05f, 0.95f);
            agentDrivenProperties.AIHoldingReadyMaxDuration = MBMath.Lerp(0.25f, 0.0f, Math.Min(1f, meleeAILevel * 1.2f));
            agentDrivenProperties.AIHoldingReadyVariationPercentage = meleeAILevel;
            agentDrivenProperties.AiRaiseShieldDelayTimeBase = (float)(0.5 * meleeAILevel - 0.75);
            agentDrivenProperties.AiUseShieldAgainstEnemyMissileProbability = (float)(0.100000001490116 + meleeAILevel * 0.600000023841858 + num1 * 0.200000002980232);
            agentDrivenProperties.AiCheckMovementIntervalFactor = (float)(0.00499999988824129 * (1.10000002384186 - meleeAILevel));
            agentDrivenProperties.AiMovementDelayFactor = (float)(4.0 / (3.0 + rangedAILevel));
            agentDrivenProperties.AiParryDecisionChangeValue = (float)(0.0500000007450581 + 0.699999988079071 * meleeAILevel);
            agentDrivenProperties.AiDefendWithShieldDecisionChanceValue = Math.Min(1f, (float)(0.200000002980232 + 0.5 * meleeAILevel + 0.200000002980232 * num1));
            agentDrivenProperties.AiMoveEnemySideTimeValue = (float)(0.5 * meleeAILevel - 2.5);
            agentDrivenProperties.AiMinimumDistanceToContinueFactor = (float)(2.0 + 0.300000011920929 * (3.0 - meleeAILevel));
            agentDrivenProperties.AiStandGroundTimerValue = (float)(0.5 * (meleeAILevel - 1.0));
            agentDrivenProperties.AiStandGroundTimerMoveAlongValue = (float)(0.5 * meleeAILevel - 1.0);
            agentDrivenProperties.AiHearingDistanceFactor = 1f + meleeAILevel;
            agentDrivenProperties.AiChargeHorsebackTargetDistFactor = (float)(1.5 * (3.0 - meleeAILevel));
            agentDrivenProperties.AiWaitBeforeShootFactor = agent.PropertyModifiers.resetAiWaitBeforeShootFactor ? 0.0f : (float)(1.0 - 0.5 * rangedAILevel);
            float num2 = 1f - rangedAILevel;
            agentDrivenProperties.AiRangerLeadErrorMin = (float)(-(double)num2 * 0.349999994039536) + config.RangedError;
            agentDrivenProperties.AiRangerLeadErrorMax = num2 * 0.2f + config.RangedError;
            agentDrivenProperties.AiRangerVerticalErrorMultiplier = num2 * 0.1f;
            agentDrivenProperties.AiRangerHorizontalErrorMultiplier = num2 * ((float)Math.PI / 90f);
            agentDrivenProperties.AIAttackOnDecideChance = MathF.Clamp((float)(0.230000004172325 * CalculateAIAttackOnDecideMaxValue() * (3.0 - agent.Defensiveness)), 0.05f, 1f);
            //agentDrivenProperties.SetStat(DrivenProperty.UseRealisticBlocking, agent.Controller != Agent.ControllerType.Player ? 1f : 0.0f);
            if (config.UseRealisticBlocking)
                agentDrivenProperties.SetStat(DrivenProperty.UseRealisticBlocking, 1f);
        }

        public override bool CanAgentRideMount(Agent agent, Agent targetMount)
        {
            return _previousModel.CanAgentRideMount(agent, targetMount);
        }
    }
}
