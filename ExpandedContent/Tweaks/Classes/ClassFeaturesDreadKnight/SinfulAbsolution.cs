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
using Kingmaker.Blueprints.Classes.Selection;

namespace ExpandedContent.Tweaks.Classes.ClassFeaturesDreadKnight {
    internal class SinfulAbsolution {

        private static readonly BlueprintCharacterClass MythicCompanionClass = Resources.GetBlueprint<BlueprintCharacterClass>("530b6a79cb691c24ba99e1577b4beb6d");
        private static readonly BlueprintCharacterClass MythicStartingClass = Resources.GetBlueprint<BlueprintCharacterClass>("247aa787806d5da4f89cfc3dff0b217f");
        private static readonly BlueprintCharacterClass AeonMythicClass = Resources.GetBlueprint<BlueprintCharacterClass>("15a85e67b7d69554cab9ed5830d0268e");
        private static readonly BlueprintCharacterClass AngelMythicClass = Resources.GetBlueprint<BlueprintCharacterClass>("a5a9fe8f663d701488bd1db8ea40484e");
        private static readonly BlueprintCharacterClass AzataMythicClass = Resources.GetBlueprint<BlueprintCharacterClass>("9a3b2c63afa79744cbca46bea0da9a16");
        private static readonly BlueprintCharacterClass DemonMythicClass = Resources.GetBlueprint<BlueprintCharacterClass>("8e19495ea576a8641964102d177e34b7");
        private static readonly BlueprintCharacterClass LichMythicClass = Resources.GetBlueprint<BlueprintCharacterClass>("5d501618a28bdc24c80007a5c937dcb7");
        private static readonly BlueprintCharacterClass DevilMythicClass = Resources.GetBlueprint<BlueprintCharacterClass>("211f49705f478b3468db6daa802452a2");
        private static readonly BlueprintCharacterClass SwarmThatWalksClass = Resources.GetBlueprint<BlueprintCharacterClass>("5295b8e13c2303f4c88bdb3d7760a757");





        public static void AddSinfulAbsolution() {

            var SinfulAbsIcon = AssetLoader.LoadInternal("Skills", "Icon_SinfulAbsolution.png");
            var FiendishSmiteGoodBuff = Resources.GetBlueprint<BlueprintBuff>("a9035e49d6d79a64eaec321f2cb629a8");
            var SinfulAbsolutionBuff = Helpers.CreateBlueprint<BlueprintBuff>("SinfulAbsolutionBuff", bp => {
                bp.SetName("Sinful Absolution");
                bp.SetDescription("Once per day, a Dread Knight can reach out to the profane lords to grant sinful absolution to the forces arrayed against them. As a swift action, " +
                    "the Dread Knight chooses one target within sight to crush. The Dread Knight adds their Charisma bonus (if any) on " +
                    "their attack rolls and adds their Dread Knight level on all damage rolls made against the target of their unholy smite. Regardless of the target, sinful absolution " +
                    "attacks automatically bypass any DR the creature might possess. In addition, while Sinful Absolution is in effect, the Dread Knight gains a deflection " +
                    "bonus equal to their Charisma modifier(if any) to their AC against attacks made by the target of sinful absolution. The sinful absolution effect remains until the " +
                    "target of sinful absolution is dead or the next time the Dread Knight rests and regains their uses of this ability. At 4th level, " +
                    "and at every three levels thereafter, the Dread Knight may grant sinful absoltion one additional time per day.");
                bp.m_Icon = SinfulAbsIcon;
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Stack;
                bp.FxOnStart = FiendishSmiteGoodBuff.FxOnStart;
                bp.FxOnRemove = FiendishSmiteGoodBuff.FxOnRemove;
                bp.AddComponent<AttackBonusAgainstTarget>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.StatBonus
                    };
                    c.CheckCaster = true;
                });
                bp.AddComponent<DamageBonusAgainstTarget>(c => {
                    c.CheckCaster = true;
                    c.ApplyToSpellDamage = true;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.DamageBonus
                    };
                });
                bp.AddComponent<ACBonusAgainstTarget>(c => {
                    c.CheckCaster = true;
                    c.Descriptor = ModifierDescriptor.Deflection;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.StatBonus
                    };
                    

                });
                bp.AddComponent<RemoveBuffIfCasterIsMissing>(c => {
                    c.RemoveOnCasterDeath = true;
                });
                bp.AddComponent<IgnoreTargetDR>(c => {
                    c.CheckCaster = true;
                });
                bp.AddComponent<UniqueBuff>();
                

            });
            var DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
            var SinfulAbsolutionResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("SinfulAbsolutionResource", bp => {
                bp.m_Min = 1;
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByStat = false,
                    IncreasedByLevel = false,
                    

                };
                bp.m_Max = 10;                

            });
            
            var FiendishSmiteGoodAbility = Resources.GetBlueprint<BlueprintAbility>("478cf0e6c5f3a4142835faeae3bd3e04");
            var SinfulAbsolutionAbility = Helpers.CreateBlueprint<BlueprintAbility>("SinfulAbsolutionAbility", bp => {
                bp.SetName("Sinful Absolution");
                bp.SetDescription("Once per day, a Dread Knight can reach out to the profane lords to grant sinful absolution to the forces arrayed against them. As a swift action, " +
                    "the Dread Knight chooses one enemy within sight to crush. The Dread Knight adds their Charisma bonus (if any) on " +
                    "their attack rolls and adds their Dread Knight level on all damage rolls made against the target of their unholy smite. Regardless of the target, sinful absolution " +
                    "attacks automatically bypass any DR the creature might possess. In addition, while Sinful Absolution is in effect, the Dread Knight gains a deflection " +
                    "bonus equal to their Charisma modifier(if any) to their AC against attacks made by the target of sinful absolution. The sinful absolution effect remains until the " +
                    "target of sinful absolution is dead or the next time the Dread Knight rests and regains their uses of this ability. At 4th level, " +
                    "and at every three levels thereafter, the Dread Knight may grant sinful absoltion one additional time per day.");
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Until the target of Sinful Absolution is dead");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "None");
                bp.m_Icon = SinfulAbsIcon;
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
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {
                                    new ContextConditionIsEnemy(),
                                    new ContextConditionHasBuffFromCaster() {
                                        m_Buff = SinfulAbsolutionBuff.ToReference<BlueprintBuffReference>(),
                                        Not = true
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SinfulAbsolutionBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        m_IsExtendable = true,
                                        DiceCountValue = new ContextValue(),
                                        BonusValue = new ContextValue()
                                    }
                                }
                            ),
                            IfFalse = Helpers.CreateActionList(),
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
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Class = new BlueprintCharacterClassReference[] { DreadKnightClass.ToReference<BlueprintCharacterClassReference>() };
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
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
            });


            var SinfulAbsolutionFeature = Helpers.CreateBlueprint<BlueprintFeature>("SinfulAbsolutionFeature", bp => {
                bp.SetName("Sinful Absolution");
                bp.SetDescription("Once per day, a Dread Knight can reach out to the profane lords to grant sinful absolution to the forces arrayed against them. As a swift action, " +
                    "the Dread Knight chooses one target within sight to crush. The Dread Knight adds their Charisma bonus (if any) on " +
                    "their attack rolls and adds their Dread Knight level on all damage rolls made against the target of their unholy smite. Regardless of the target, sinful absolution " +
                    "attacks automatically bypass any DR the creature might possess. In addition, while Sinful Absolution is in effect, the Dread Knight gains a deflection " +
                    "bonus equal to their Charisma modifier(if any) to their AC against attacks made by the target of sinful absolution. The sinful absolution effect remains until the " +
                    "target of sinful absolution is dead or the next time the Dread Knight rests and regains their uses of this ability. At 4th level, " +
                    "and at every three levels thereafter, the Dread Knight may grant sinful absoltion one additional time per day.");
                bp.m_DescriptionShort = Helpers.CreateString("$SinfulAbsolutionFeature.DescriptionShort", "Once per day, a Dread Knight can reach out to the profane lords to grant sinful absolution to the forces arrayed against them. As a swift action, " +
                    "the Dread Knight chooses one target within sight to crush. The Dread Knight adds their Charisma bonus (if any) on " +
                    "their attack rolls and adds their Dread Knight level on all damage rolls made against the target of their unholy smite. Regardless of the target, sinful absolution " +
                    "attacks automatically bypass any DR the creature might possess. In addition, while Sinful Absolution is in effect, the Dread Knight gains a deflection " +
                    "bonus equal to their Charisma modifier(if any) to their AC against attacks made by the target of sinful absolution.");
                bp.m_Icon = SinfulAbsIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;

                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = SinfulAbsolutionResource.ToReference<BlueprintAbilityResourceReference>();
                });                
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SinfulAbsolutionAbility.ToReference<BlueprintUnitFactReference>(),

                    };
                });
            });
            var SinfulAbsolutionUse = Helpers.CreateBlueprint<BlueprintFeature>("SinfulAbsolutionUse", bp => {
                bp.SetName("Sinful Absolution - Additional Use");
                bp.SetDescription("Once per day, a Dread Knight can reach out to the profane lords to grant sinful absolution to the forces arrayed against them. As a swift action, " +
                    "the Dread Knight chooses one target within sight to crush. The Dread Knight adds their Charisma bonus (if any) on " +
                    "their attack rolls and adds their Dread Knight level on all damage rolls made against the target of their unholy smite. Regardless of the target, sinful absolution " +
                    "attacks automatically bypass any DR the creature might possess. In addition, while Sinful Absolution is in effect, the Dread Knight gains a deflection " +
                    "bonus equal to their Charisma modifier(if any) to their AC against attacks made by the target of sinful absolution. The sinful absolution effect remains until the " +
                    "target of sinful absolution is dead or the next time the Dread Knight rests and regains their uses of this ability. At 4th level, " +
                    "and at every three levels thereafter, the Dread Knight may grant sinful absoltion one additional time per day.");
                bp.m_Icon = SinfulAbsIcon;
                bp.Ranks = 10;
                bp.IsClassFeature = true;
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = SinfulAbsolutionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 1;
                });
            });
            var MythicAbilitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("ba0e5a900b775be4a99702f1ed08914d");
            var AbundantSin = Helpers.CreateBlueprint<BlueprintFeature>("AbundantSin", bp => {
                bp.SetName("Abundant Sin");
                bp.SetDescription("You have found a way to force dark powers to grant you additional uses of the Sinful Absolution ability. " +
                    "\nBenefit: You can use Sinful Absolution a number of additional times per day equal to half your mythic rank.");
                bp.m_Icon = SinfulAbsIcon;
                bp.Groups = new FeatureGroup[] { FeatureGroup.MythicAbility };
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<IncreaseResourceAmountBySharedValue>(c => {
                    c.m_Resource = SinfulAbsolutionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = new ContextValue() { ValueType = ContextValueType.Rank };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_BaseValueType = ContextRankBaseValueType.MythicLevel;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_Max = 20;
                    c.m_Class = new BlueprintCharacterClassReference[9] {
                        MythicCompanionClass.ToReference<BlueprintCharacterClassReference>(),
                        MythicStartingClass.ToReference<BlueprintCharacterClassReference>(),
                        AeonMythicClass.ToReference<BlueprintCharacterClassReference>(),
                        AngelMythicClass.ToReference<BlueprintCharacterClassReference>(),
                        AzataMythicClass.ToReference<BlueprintCharacterClassReference>(),
                        DemonMythicClass.ToReference<BlueprintCharacterClassReference>(),
                        LichMythicClass.ToReference<BlueprintCharacterClassReference>(),
                        DevilMythicClass.ToReference<BlueprintCharacterClassReference>(),
                        SwarmThatWalksClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<PrerequisiteFeature>(c => { c.m_Feature = SinfulAbsolutionFeature.ToReference<BlueprintFeatureReference>(); });

            });
            MythicAbilitySelection.m_AllFeatures = MythicAbilitySelection.m_AllFeatures.AppendToArray(AbundantSin.ToReference<BlueprintFeatureReference>());
        }
    }
}




   