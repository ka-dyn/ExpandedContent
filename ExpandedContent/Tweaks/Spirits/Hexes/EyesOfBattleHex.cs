using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Utility;

namespace ExpandedContent.Tweaks.Spirits.Hexes
{
    internal class EyesOfBattleHex {
        public static void AddEyesOfBattleHex() {

            var ShamanClass = Resources.GetBlueprint<BlueprintCharacterClass>("145f1d3d360a7ad48bd95d392c81b38e");

            var ShamanBattleSpiritProgression = Resources.GetBlueprint<BlueprintProgression>("26e81d83d13dbf6448b10019c802612d");
            var ShamanBattleSpiritWanderingFeature = Resources.GetBlueprint<BlueprintFeature>("82ec7a49f3ab65a44bbf4177dfdfe469");
            var ShamanBattleSpiritBaseFeature = Resources.GetBlueprint<BlueprintProgression>("5e1161a3a7e5a83458de8a5f412c8d0c");

            var TrueSeeingIcon = Resources.GetBlueprint<BlueprintBuff>("09b4b69169304474296484c74aa12027").Icon;

            var ShamanHexEyesOfBattleResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ShamanHexEyesOfBattleResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = true,
                    LevelIncrease = 1,
                    IncreasedByLevelStartPlusDivStep = false,
                    StartingLevel = 1,
                    StartingIncrease = 1,
                    LevelStep = 0,
                    PerStepIncrease = 1,
                    MinClassLevelIncrease = 0,
                    OtherClassesModifier = 0,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Unknown,
                    m_Class = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    }
                };
                bp.m_UseMax = true;
                bp.m_Max = 20;
            });


            var ShamanHexEyesOfBattlePerceptionBonusBuff = Helpers.CreateBuff("ShamanHexEyesOfBattlePerceptionBonusBuff", bp => {
                bp.SetName("Eyes of Battle - Perception");
                bp.SetDescription("The shaman’s senses become magically heightened in the heat of battle. As a swift action, she can grant herself " +
                    "a +10 insight bonus for 1 round on Perception checks made to notice and pinpoint invisible creatures within 30 feet.");
                bp.m_Icon = TrueSeeingIcon;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillPerception;
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Value = 10;                    
                });
                bp.IsClassFeature = false;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var ShamanHexEyesOfBattleIgnoreConcealmentBuff = Helpers.CreateBuff("ShamanHexEyesOfBattleIgnoreConcealmentBuff", bp => {
                bp.SetName("Eyes of Battle - Ignore Concealment");
                bp.SetDescription("The shaman’s senses become magically heightened in the heat of battle. As a swift action, she can ignore the affects of concealment on " +
                    "her next attack, as long as that attack is made before the end of her next turn.");
                bp.m_Icon = TrueSeeingIcon;
                bp.AddComponent<IgnoreConcealment>();
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = false;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.Bows;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.ActionsOnInitiator = true;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = false;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.IsClassFeature = false;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });



            var ShamanHexEyesOfBattleAbilityBase = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHexEyesOfBattleAbilityBase", bp => {
                bp.SetName("Eyes of Battle");
                bp.SetDescription("The shaman’s senses become magically heightened in the heat of battle. As a swift action, she can grant herself " +
                    "a +10 insight bonus for 1 round on Perception checks made to notice and pinpoint invisible creatures within 30 feet. She can " +
                    "instead use this ability as a swift action to ignore the affects of concealment on her next " +
                    "attack, as long as that attack is made before the end of her next turn. The shaman can use this ability a number of times " +
                    "per day equal to her shaman level.");
                bp.m_Icon = TrueSeeingIcon;
                
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Hex;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var ShamanHexEyesOfBattlePerceptionBonusAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHexEyesOfBattlePerceptionBonusAbility", bp => {
                bp.SetName("Eyes of Battle - Perception");
                bp.SetDescription("The shaman’s senses become magically heightened in the heat of battle. As a swift action, she can grant herself " +
                    "a +10 insight bonus for 1 round on Perception checks made to notice and pinpoint invisible creatures within 30 feet. " +
                    "The shaman can use this ability a number of times per day equal to her shaman level.");
                bp.m_Icon = TrueSeeingIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(                        
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanHexEyesOfBattlePerceptionBonusBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = false,
                            ToCaster = false,
                            AsChild = true,
                        }
                        );
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Hex;
                });
                bp.m_Parent = ShamanHexEyesOfBattleAbilityBase.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShamanHexEyesOfBattleIgnoreConcealmentAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHexEyesOfBattleIgnoreConcealmentAbility", bp => {
                bp.SetName("Eyes of Battle - Ignore Concealment");
                bp.SetDescription("The shaman’s senses become magically heightened in the heat of battle. As a swift action, she can ignore the affects of concealment on " +
                    "her next attack, as long as that attack is made before the end of her next turn. The shaman can use this ability a number of times per day equal " +
                    "to her shaman level.");
                bp.m_Icon = TrueSeeingIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanHexEyesOfBattleIgnoreConcealmentBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 2,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = false,
                            ToCaster = false,
                            AsChild = true,
                        }
                        );
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Hex;
                });
                bp.m_Parent = ShamanHexEyesOfBattleAbilityBase.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            ShamanHexEyesOfBattleAbilityBase.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    ShamanHexEyesOfBattlePerceptionBonusAbility.ToReference<BlueprintAbilityReference>(),
                    ShamanHexEyesOfBattleIgnoreConcealmentAbility.ToReference<BlueprintAbilityReference>()
                };
            });
            var ShamanHexEyesOfBattleFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexEyesOfBattleFeature", bp => {
                bp.SetName("Eyes of Battle");
                bp.SetDescription("The shaman’s senses become magically heightened in the heat of battle. As a swift action, she can grant herself " +
                    "a +10 insight bonus for 1 round on Perception checks made to notice and pinpoint invisible creatures within 30 feet. She can " +
                    "instead use this ability as a swift action to ignore the affects of concealment on her next " +
                    "attack, as long as that attack is made before the end of her next turn. The shaman can use this ability a number of times " +
                    "per day equal to her shaman level.");
                bp.m_Icon = TrueSeeingIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanHexEyesOfBattleAbilityBase.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ShamanHexEyesOfBattleResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanBattleSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanBattleSpiritWanderingFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanBattleSpiritBaseFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ShamanHex };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            SpiritTools.RegisterShamanHex(ShamanHexEyesOfBattleFeature);

            ShamanBattleSpiritProgression.IsPrerequisiteFor.Add(ShamanHexEyesOfBattleFeature.ToReference<BlueprintFeatureReference>());

        }
    }
}
