using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.ElementsSystem;
using Kingmaker.Enums;
using Kingmaker.Localization;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValueType = Kingmaker.UnitLogic.Mechanics.ValueType;

namespace ExpandedContent.Tweaks.Classes.ClassFeaturesOathbreaker {
    internal class OathbreakersDirection {

        private static readonly BlueprintCharacterClass OathbreakerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("OathbreakerClass");
        public static void AddOathbreakersDirection() {
            var ODIcon = AssetLoader.LoadInternal("Skills", "Icon_OD.png");
            var OathbreakersDirectionBuffAllies = Helpers.CreateBlueprint<BlueprintBuff>("OathbreakersDirectionBuffAllies", bp => {

                bp.SetName("Oathbreaker's Direction");
                bp.SetDescription("At 1st level, the Oathbreaker can, as a move action, indicate an enemy in combat and rally her allies to " +
                    "focus on that target. The Oathbreaker and her allies gain a +1 bonus on weapon attack and damage rolls against the target. " +
                    "This ability applies only to allies who can see or hear the Oathbreaker and who are within 30 feet of the Oathbreaker at the time she " +
                    "activates this ability. At 5th level and every 5 levels thereafter (10th, 15th, and 20th level), the bonus increases by 1. The Oathbreaker's Direction " +
                    "lasts until the target dies or the Oathbreaker selects a new target.");
                bp.m_Icon = ODIcon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AttackBonusAgainstTarget>(c => {
                    c.CheckCasterFriend = true;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = Kingmaker.UnitLogic.Abilities.AbilitySharedValue.DamageBonus
                    };
                    
                });
                bp.AddComponent<DamageBonusAgainstTarget>(c => {
                    c.CheckCasterFriend = true;
                    c.ApplyToSpellDamage = true;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = Kingmaker.UnitLogic.Abilities.AbilitySharedValue.DamageBonus
                    };

                });
                bp.AddComponent<RemoveBuffIfCasterIsMissing>(c => {
                    c.RemoveOnCasterDeath = true;
                });
            });
            var HellSealVariantDevouringFlamesBuff = Resources.GetBlueprint<BlueprintBuff>("5617dbbb3890e2f4b96b47318c5c438b");
            var OathbreakersDirectionBuff = Helpers.CreateBlueprint<BlueprintBuff>("OathbreakersDirectionBuff", bp => {

                bp.SetName("Oathbreaker's Direction");
                bp.SetDescription("At 1st level, the Oathbreaker can, as a move action, indicate an enemy in combat and rally her allies to " +
                    "focus on that target. The Oathbreaker and her allies gain a +1 bonus on weapon attack and damage rolls against the target. " +
                    "This ability applies only to allies who can see or hear the Oathbreaker and who are within 30 feet of the Oathbreaker at the time she " +
                    "activates this ability. At 5th level and every 5 levels thereafter (10th, 15th, and 20th level), the bonus increases by 1. The Oathbreaker's Direction " +
                    "lasts until the target dies or the Oathbreaker selects a new target.");
                bp.m_Icon = ODIcon;               
                bp.FxOnStart = HellSealVariantDevouringFlamesBuff.FxOnStart;
                bp.IsClassFeature = true;
                bp.AddComponent<AttackBonusAgainstTarget>(c => {
                    c.CheckCaster = true;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = Kingmaker.UnitLogic.Abilities.AbilitySharedValue.DamageBonus
                    };
                    
                });
                bp.AddComponent<DamageBonusAgainstTarget>(c => {
                    c.CheckCaster = true;
                    c.ApplyToSpellDamage = true;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = Kingmaker.UnitLogic.Abilities.AbilitySharedValue.DamageBonus
                    };
                    

                });
                
                bp.AddComponent<RemoveBuffIfCasterIsMissing>(c => {
                    c.RemoveOnCasterDeath = true;
                });
                bp.AddComponent<UniqueBuff>();
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = new ActionList() { };
                    c.Deactivated = new ActionList() {
                        Actions = new ContextAction[1] {
                            new ContextActionRemoveBuff() {
                                m_Buff = OathbreakersDirectionBuffAllies.ToReference<BlueprintBuffReference>()
                            }
                        }
                    };
                });
            });




            var OathbreakerClass = Resources.GetModBlueprint<BlueprintFeature>("OathbreakerClass");
            var OathbreakersDirectionAbility = Helpers.CreateBlueprint<BlueprintAbility>("OathbreakersDirectionAbility", bp => {

                bp.SetName("Oathbreaker's Direction");
                bp.SetDescription("At 1st level, the Oathbreaker can, as a move action, indicate an enemy in combat and rally her allies to " +
                    "focus on that target. The Oathbreaker and her allies gain a +1 bonus on weapon attack and damage rolls against the target. " +
                    "This ability applies only to allies who can see or hear the Oathbreaker and who are within 30 feet of the Oathbreaker at the time she " +
                    "activates this ability. At 5th level and every 5 levels thereafter (10th, 15th, and 20th level), the bonus increases by 1. The Oathbreaker's Direction " +
                    "lasts until the target dies or the Oathbreaker selects a new target.");
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Until the directed target is dead");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "None");
                bp.m_Icon = ODIcon;
                bp.Type = AbilityType.Extraordinary;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetEnemies = true;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Kineticist;
                bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Move;

                bp.AvailableMetamagic = Kingmaker.UnitLogic.Abilities.Metamagic.Heighten | Kingmaker.UnitLogic.Abilities.Metamagic.Reach;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OathbreakersDirectionBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() { m_IsExtendable = true },
                            AsChild = true,
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = OathbreakersDirectionBuffAllies.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() { m_IsExtendable = true },
                            AsChild = true,
                        });
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.DamageBonus;
                    c.Value = new ContextDiceValue() {
                        DiceCountValue = 0,
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageBonus,
                            ValueShared = AbilitySharedValue.DamageBonus,
                        }
                    };

                    c.Modifier = 1;
                });
                bp.AddContextRankConfig(c => {
                    c.m_Type = Kingmaker.Enums.AbilityRankType.DamageBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Progression = ContextRankProgression.StartPlusDivStep;
                    c.m_StepLevel = 5;
                    c.m_Max = 20;
                    c.m_Class = new BlueprintCharacterClassReference[1] { OathbreakerClass.ToReference<BlueprintCharacterClassReference>() };
                });
            });



                var OathbreakersDirectionAbilitySwift = Helpers.CreateBlueprint<BlueprintAbility>("OathbreakersDirectionAbilitySwift", bp => {
                    bp.SetName("Oathbreaker's Direction (Swift)");
                    bp.SetDescription("At 11th level, an Oathbreaker can active her Oathbreaker's Direction ability " +
                        "as a swift action.");
                    bp.m_Icon = ODIcon;
                    
                    bp.Type = AbilityType.Extraordinary;
                    bp.Range = AbilityRange.Medium;
                    bp.ActionType = Kingmaker.UnitLogic.Commands.Base.UnitCommand.CommandType.Swift;
                    bp.CanTargetEnemies = true;
                    bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                    bp.Animation = Kingmaker.Visual.Animation.Kingmaker.Actions.UnitAnimationActionCastSpell.CastAnimationStyle.Kineticist;
                    bp.AvailableMetamagic = Kingmaker.UnitLogic.Abilities.Metamagic.Heighten | Kingmaker.UnitLogic.Abilities.Metamagic.Reach;
                    bp.ComponentsArray = OathbreakersDirectionAbility.ComponentsArray;

                });
                var OathbreakersDirectionSwiftFeature = Helpers.CreateBlueprint<BlueprintFeature>("OathbreakersDirectionSwiftFeature", bp => {

                    bp.SetName("Oathbreaker's Direction (Swift)");
                    bp.SetDescription("At 11th level, the Oathbreaker can, as a swift action, indicate an enemy in combat and rally her allies to " +
                        "focus on that target. The Oathbreaker and her allies gain a +1 bonus on weapon attack and damage rolls against the target. " +
                        "This ability applies only to allies who can see or hear the Oathbreaker and who are within 30 feet of the Oathbreaker at the time she " +
                        "activates this ability. At 5th level and every 5 levels thereafter (10th, 15th, and 20th level), the bonus increases by 1. The Oathbreaker's Direction " +
                        "lasts until the target dies or the Oathbreaker selects a new target.");
                    bp.m_Icon = ODIcon;
                    bp.AddComponent<AddFacts>(c => {
                        c.m_Facts = new BlueprintUnitFactReference[1] { OathbreakersDirectionAbilitySwift.ToReference<BlueprintUnitFactReference>() };
                    });
                });









                var OathbreakersDirectionFeature = Helpers.CreateBlueprint<BlueprintFeature>("OathbreakersDirectionFeature", bp => {

                    bp.SetName("Oathbreaker's Direction");
                    bp.SetDescription("At 1st level, the Oathbreaker can, as a move action, indicate an enemy in combat and rally her allies to " +
                        "focus on that target. The Oathbreaker and her allies gain a +1 bonus on weapon attack and damage rolls against the target. " +
                        "This ability applies only to allies who can see or hear the Oathbreaker and who are within 30 feet of the Oathbreaker at the time she " +
                        "activates this ability. At 5th level and every 5 levels thereafter (10th, 15th, and 20th level), the bonus increases by 1. The Oathbreaker's Direction " +
                        "lasts until the target dies or the Oathbreaker selects a new target.");
                    bp.m_DescriptionShort = Helpers.CreateString("$OathbreakersDirection.DescriptionShort", "At 1st level, the Oathbreaker can, as a move action, indicate an enemy in combat and rally her allies to " +
                        "focus on that target. The Oathbreaker and her allies gain a +1 bonus on weapon attack and damage rolls against the target.");

                    bp.Ranks = 1;
                    bp.IsClassFeature = true;
                    bp.m_Icon = ODIcon;

                    bp.AddComponent<AddFacts>(c => {
                        c.m_Facts = new BlueprintUnitFactReference[1] { OathbreakersDirectionAbility.ToReference<BlueprintUnitFactReference>() };
                    });
                });
                var OathbreakersDirectionIncrease = Helpers.CreateBlueprint<BlueprintFeature>("OathbreakersDirectionIncrease", bp => {

                    bp.SetName("Oathbreaker's Direction - Bonus Increase");
                    bp.SetDescription("At 5th level and every 5 levels thereafter (10th, 15th, and 20th level), the bonus increases by 1. The Oathbreaker's Direction " +
                        "lasts until the target dies or the Oathbreaker selects a new target.");
                    bp.m_DescriptionShort = Helpers.CreateString("$OathbreakersDirection.DescriptionShort", "At 1st level, the Oathbreaker can, as a move action, indicate an enemy in combat and rally her allies to " +
                        "focus on that target. The Oathbreaker and her allies gain a +1 bonus on weapon attack and damage rolls against the target.");
                    bp.Ranks = 1;
                    bp.IsClassFeature = true;
                    bp.m_Icon = ODIcon;


                });
            
         }

    }
}
    


        