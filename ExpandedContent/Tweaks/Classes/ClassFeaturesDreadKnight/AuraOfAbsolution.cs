using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpandedContent;
using ExpandedContent.Utilities;
using ExpandedContent.Extensions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Blueprints.Classes.Prerequisites;

namespace ExpandedContent.Tweaks.Classes.ClassFeaturesDreadKnight {
    internal class AuraOfAbsolution {


        public static void AddAuraOfAbsolution() {

            var AOAIcon = AssetLoader.LoadInternal("Skills", "Icon_AOAbsolution.png");
            var SinfulAbsolutionResource = Resources.GetModBlueprint<BlueprintAbilityResource>("SinfulAbsolutionResource");
            var OBBaneIcon = AssetLoader.LoadInternal("Skills", "Icon_OBBane.png");
            var FiendishSmiteGoodBuff = Resources.GetBlueprint<BlueprintBuff>("a9035e49d6d79a64eaec321f2cb629a8");
            var AuraOfAbsolutionBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfAbsolutionBuff", bp => {
                bp.SetName("Aura of Absolution");
                bp.SetDescription("Once per day, a Dread Knight can reach out to the profane lords to grant sinful absolution to the forces arrayed against them. As a swift action, " +
                    "the Dread Knight chooses one target within sight to crush. The Dread Knight adds their Charisma bonus (if any) on " +
                    "their attack rolls and adds their Dread Knight level on all damage rolls made against the target of their unholy smite. Regardless of the target, sinful absolution " +
                    "attacks automatically bypass any DR the creature might possess. In addition, while Sinful Absolution is in effect, the Dread Knight gains a deflection " +
                    "bonus equal to their Charisma modifier(if any) to their AC against attacks made by the target of sinful absolution. The sinful absolution effect remains until the " +
                    "target of sinful absolution is dead or the next time the Dread Knight rests and regains their uses of this ability. At 4th level, " +
                    "and at every three levels thereafter, the Dread Knight may grant sinful absoltion one additional time per day.");
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Stack;
                bp.m_Icon = AOAIcon;
                bp.FxOnStart = FiendishSmiteGoodBuff.FxOnStart;
                bp.FxOnRemove = FiendishSmiteGoodBuff.FxOnRemove;
                bp.AddComponent<AttackBonusAgainstTarget>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.StatBonus
                    };
                    c.CheckCasterFriend = true;
                });
                bp.AddComponent<DamageBonusAgainstTarget>(c => {
                    c.CheckCasterFriend = true;
                    c.ApplyToSpellDamage = true;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.DamageBonus
                    };
                });
                bp.AddComponent<ACBonusAgainstTarget>(c => {
                    c.Descriptor = ModifierDescriptor.Deflection;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.StatBonus
                    };
                    c.CheckCasterFriend = true;

                });
                bp.AddComponent<IgnoreTargetDR>(c => {
                    c.CheckCasterFriend = true;
                   
                });
                bp.AddComponent<UniqueBuff>();
            });
            var FiendishSmiteGoodAbility = Resources.GetBlueprint<BlueprintAbility>("478cf0e6c5f3a4142835faeae3bd3e04");
            var AuraOfAbsolutionAbility = Helpers.CreateBlueprint<BlueprintAbility>("AuraOfAbsolutionAbility", bp => {
                bp.SetName("Aura of Absolution");
                bp.SetDescription("At 11th level, a Dread Knight can expend two uses of his sinful absolution ability to grant the " +
                    "ability to all allies within 10 feet, using his bonuses. Allies must use this ability " +
                    "by the start of the antipaladin’s next turn and the bonuses last for 1 minute. Using this ability is a free action.");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "None");
                bp.m_Icon = AOAIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = Metamagic.Heighten | Metamagic.Reach;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = AuraOfAbsolutionBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        m_IsExtendable = true,
                                        DiceCountValue = new ContextValue(),
                                        BonusValue = new ContextValue()
                                    }
                                });


                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.StatBonus;
                    c.Value = new ContextDiceValue() {
                        DiceCountValue = new ContextValue(),
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueShared = AbilitySharedValue.StatBonus
                        },

                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.DamageBonus;
                    c.Value = new ContextDiceValue() {
                        DiceCountValue = new ContextValue(),
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageBonus,
                            ValueShared = AbilitySharedValue.DamageBonus,

                        },

                    };
                    c.Modifier = 1;
                });
                bp.AddComponent(Helpers.CreateContextRankConfig(c => {
                    c.m_Type = AbilityRankType.DamageBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.CharacterLevel;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 20;
                }));
                bp.AddComponent(Helpers.CreateContextRankConfig(c => {

                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Charisma;
                    c.m_Max = 20;
                }));

                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().PrefabLink;
                    c.Anchor = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().Anchor;
                    c.PositionAnchor = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().PositionAnchor;
                    c.OrientationAnchor = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().OrientationAnchor;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = SinfulAbsolutionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 2;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
            });
            var AuraOfAbsolutionFeature = Helpers.CreateBlueprint<BlueprintFeature>("AuraOfAbsolutionFeature", bp => {
                bp.SetName("Aura of Absolution");
                bp.SetDescription("At 11th level, a Dread Knight can expend two uses of his sinful absolution ability to grant the " +
                    "ability to all allies within 10 feet, using his bonuses. Allies must use this ability " +
                    "by the start of the antipaladin’s next turn and the bonuses last for 1 minute. Using this ability is a free action.");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_Icon = AOAIcon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = SinfulAbsolutionResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        AuraOfAbsolutionAbility.ToReference<BlueprintUnitFactReference>(),

                    };
                });
            });
        }
    }
}
