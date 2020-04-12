using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Helpers;
using SandBox;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace EnhancedMissionMoreOptionsPlugin
{
    public class EnhancedAgentStatCalculateModel : AgentStatCalculateModel
    {
        private AgentStatCalculateModel _previousModel;

        public EnhancedAgentStatCalculateModel(AgentStatCalculateModel previousModel)
        {
            _previousModel = previousModel;
        }

        public override void InitializeAgentStats(Agent agent, Equipment spawnEquipment, AgentDrivenProperties agentDrivenProperties,
            AgentBuildData agentBuildData)
        {
            _previousModel.InitializeAgentStats(agent, spawnEquipment, agentDrivenProperties, agentBuildData);
        }

        public override void UpdateAgentStats(Agent agent, AgentDrivenProperties agentDrivenProperties)
        {
            _previousModel.UpdateAgentStats(agent, agentDrivenProperties);
            if (agent.IsHuman)
            {
                EnhancedSetAiRelatedProperties(agent, agentDrivenProperties);
            }
        }

        public override short CalculateConsumableMaxAmountAdder()
        {
            return _previousModel.CalculateConsumableMaxAmountAdder();
        }

        public override float GetDifficultyModifier()
        {
            return _previousModel.GetDifficultyModifier();
        }
        private int GetWeaponSkill(BasicCharacterObject character, WeaponComponentData equippedItem)
        {
            SkillObject skill = DefaultSkills.Athletics;
            if (equippedItem != null)
                skill = equippedItem.RelevantSkill;
            return character.GetSkillValue(skill);
        }

        private int GetMeleeSkill(
            BasicCharacterObject character,
            WeaponComponentData equippedItem,
            WeaponComponentData secondaryItem)
        {
            SkillObject skill = DefaultSkills.Athletics;
            if (equippedItem != null)
            {
                SkillObject relevantSkill = equippedItem.RelevantSkill;
                skill = relevantSkill == DefaultSkills.OneHanded || relevantSkill == DefaultSkills.Polearm ? relevantSkill : (relevantSkill != DefaultSkills.TwoHanded ? DefaultSkills.OneHanded : (secondaryItem == null ? DefaultSkills.TwoHanded : DefaultSkills.OneHanded));
            }
            return character.GetSkillValue(skill);
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
                mainHandWeapon = (WeaponComponentData)null;
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
                offHandWeapon = (WeaponComponentData)null;
            }
            else
            {
                missionWeapon = equipment[offHandItemIndex];
                offHandWeapon = missionWeapon.CurrentUsageItem;
            }
            var config = MoreOptionsConfig.Get();
            float meleeAILevel;
            if (config.ChangeMeleeAI)
            {
                meleeAILevel = MathF.Clamp(config.MeleeAI / 100.0f, 0, 1);
            }
            else
            {
                int meleeSkill = this.GetMeleeSkill(agent.Character, mainHandWeapon, offHandWeapon);
                meleeAILevel = MathF.Sqrt(MBMath.ClampFloat((float)meleeSkill / 200f * GetDifficultyModifier(), 0.0f, 1f));
            }

            float rangedAILevel;
            if (config.ChangeRangedAI)
            {
                rangedAILevel = MathF.Clamp(config.RangedAI / 100.0f, 0, 1);
            }
            else
            {
                int weaponSkill = this.GetWeaponSkill(agent.Character, mainHandWeapon);
                rangedAILevel = this.CalculateAILevel(agent, weaponSkill);
            }

            float num1 = meleeAILevel + agent.Defensiveness;
            agentDrivenProperties.AiFacingMissileWatch = (float)((double)meleeAILevel * 0.0599999986588955 - 0.959999978542328);
            agentDrivenProperties.AiFlyingMissileCheckRadius = (float)(8.0 - 6.0 * (double)meleeAILevel);
            float num2 = offHandWeapon != null ? 0.0f : -0.1f;
            agentDrivenProperties.AIBlockOnDecideAbility = MBMath.Lerp(0.1f, 0.95f,  meleeAILevel, 1E-05f);
            agentDrivenProperties.AIParryOnDecideAbility = MBMath.Lerp(0.1f, 0.95f,  meleeAILevel + num2, 1E-05f);
            agentDrivenProperties.AiTryChamberAttackOnDecide = (float)((meleeAILevel - 0.150000005960464) * 0.100000001490116);
            agentDrivenProperties.AIAttackOnParryChance = (float)(0.300000011920929 - 0.100000001490116 * (double)agent.Defensiveness);
            agentDrivenProperties.AiAttackOnParryTiming = (float)(0.300000011920929 * (double)meleeAILevel - 0.200000002980232);
            agentDrivenProperties.AIParryOnAttackAbility = (float)MBMath.Lerp(0.2f, 0.98f, meleeAILevel);
            agentDrivenProperties.AiKick = (float)(((double)meleeAILevel > 0.400000005960464 ? 0.400000005960464 : (double)meleeAILevel) - 0.100000001490116);
            agentDrivenProperties.AiAttackCalculationMaxTimeFactor = meleeAILevel;
            agentDrivenProperties.AiDecideOnAttackWhenReceiveHitTiming = (float)(-0.25 * (1.0 - (double)meleeAILevel));
            agentDrivenProperties.AiDecideOnAttackContinueAction = (float)(-0.5 * (1.0 - (double)meleeAILevel));
            agentDrivenProperties.AiDecideOnAttackingContinue = 0.1f * meleeAILevel;
            agentDrivenProperties.AIParryOnAttackingContinueAbility = MBMath.Lerp(0.2f, 0.98f, meleeAILevel, 1E-05f);
            agentDrivenProperties.AIDecideOnRealizeEnemyBlockingAttackAbility = 0.5f * MBMath.ClampFloat(meleeAILevel - 0.1f, 0.0f, 1f);
            agentDrivenProperties.AIRealizeBlockingFromIncorrectSideAbility = 0.5f * MBMath.ClampFloat(meleeAILevel - 0.1f, 0.0f, 1f);
            agentDrivenProperties.AiAttackingShieldDefenseChance = (float)(0.200000002980232 + 0.300000011920929 * (double)meleeAILevel);
            agentDrivenProperties.AiAttackingShieldDefenseTimer = (float)(0.300000011920929 * (double)meleeAILevel - 0.300000011920929);
            agentDrivenProperties.AiRandomizedDefendDirectionChance = (float)(1.0 - Math.Log((double)meleeAILevel * 7.0 + 1.0, 2.0) * 0.333330005407333);
            agentDrivenProperties.AISetNoAttackTimerAfterBeingHitAbility = MBMath.ClampFloat(meleeAILevel, 0.05f, 0.95f);
            agentDrivenProperties.AISetNoAttackTimerAfterBeingParriedAbility = MBMath.ClampFloat(meleeAILevel, 0.05f, 0.95f);
            agentDrivenProperties.AISetNoDefendTimerAfterHittingAbility = MBMath.ClampFloat(meleeAILevel, 0.5f, 0.95f);
            agentDrivenProperties.AISetNoDefendTimerAfterParryingAbility = MBMath.ClampFloat(meleeAILevel, 0.5f, 0.95f);
            agentDrivenProperties.AIEstimateStunDurationPrecision = 1f - MBMath.ClampFloat(meleeAILevel, 0.05f, 0.95f);
            agentDrivenProperties.AIHoldingReadyMaxDuration = MBMath.Lerp(0.2f, 0.0f, Math.Min(1f, meleeAILevel * 1.5f), 1E-05f);
            agentDrivenProperties.AIHoldingReadyVariationPercentage = meleeAILevel;
            agentDrivenProperties.AiRaiseShieldDelayTimeBase = (float)(0.5 * (double)meleeAILevel - 0.75);
            agentDrivenProperties.AiUseShieldAgainstEnemyMissileProbability = (float)(0.100000001490116 + (double)meleeAILevel * 0.200000002980232 + (double)num1 * 0.200000002980232);
            agentDrivenProperties.AiCheckMovementIntervalFactor = (float)(0.00499999988824129 * (1.0 - (double)meleeAILevel));
            agentDrivenProperties.AiParryDecisionChangeValue = (float)(0.0500000007450581 + 0.699999988079071 * (double)meleeAILevel);
            agentDrivenProperties.AiDefendWithShieldDecisionChanceValue = Math.Min(1f, (float)(0.400000005960464 + 0.800000011920929 * (double)meleeAILevel + 0.200000002980232 * (double)num1));
            agentDrivenProperties.AiMoveEnemySideTimeValue = (float)(0.5 * (double)meleeAILevel - 2.5);
            agentDrivenProperties.AiMinimumDistanceToContinueFactor = (float)(2.0 + 0.300000011920929 * (3.0 - (double)meleeAILevel));
            agentDrivenProperties.AiStandGroundTimerValue = (float)(0.5 * ((double)meleeAILevel - 1.0));
            agentDrivenProperties.AiStandGroundTimerMoveAlongValue = (float)(0.5 * (double)meleeAILevel - 1.0);
            agentDrivenProperties.AiHearingDistanceFactor = 1f + meleeAILevel;
            agentDrivenProperties.AiChargeHorsebackTargetDistFactor = (float)(1.5 * (3.0 - (double)meleeAILevel));
            agentDrivenProperties.AIAttackOnDecideChance = MathF.Clamp((float)(0.330000013113022 * (double)this.CalculateAIAttackOnDecideMaxValue() * (3.0 - (double)agent.Defensiveness)), 0.05f, 1f);
            agentDrivenProperties.AiRangedHorsebackMissileRange =
                (float)(0.300000011920929 + 0.400000005960464 * (double)rangedAILevel);
            agentDrivenProperties.AiShootFreq =
                (float)(0.300000011920929 + 0.699999988079071 * (double)rangedAILevel);
            agentDrivenProperties.AiWaitBeforeShootFactor = agent._propertyModifiers.resetAiWaitBeforeShootFactor
                ? 0.0f
                : (float)(1.0 - 0.5 * (double)rangedAILevel);
            agentDrivenProperties.AiMovemetDelayFactor = (float)(4.0 / (3.0 + (double)rangedAILevel));
            agentDrivenProperties.AiWaitBeforeShootFactor = agent._propertyModifiers.resetAiWaitBeforeShootFactor
                ? 0.0f
                : (float)(1.0 - 0.5 * (double)rangedAILevel);
            float num3 = 1f - rangedAILevel;
            agentDrivenProperties.AiRangerLeadErrorMin = (float)(-(double)num3 * 0.300000011920929);
            agentDrivenProperties.AiRangerLeadErrorMax = num3 * 0.2f;
            agentDrivenProperties.AiRangerVerticalErrorMultiplier = num3 * 0.1f;
            agentDrivenProperties.AiRangerHorizontalErrorMultiplier = num3 * ((float)Math.PI / 90f);
            if (config.UseRealisticBlocking)
                agentDrivenProperties.SetStat(DrivenProperty.UseRealisticBlocking, config.UseRealisticBlocking ? 1f : 0.0f);
        }
    }
}
