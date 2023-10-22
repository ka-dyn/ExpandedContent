using ExpandedContent.Utilities;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpandedContent.Extensions;
using TabletopTweaks.Core.NewComponents;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Designers.Mechanics.Facts;
using ExpandedContent.Tweaks.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.Buffs.Components;

namespace ExpandedContent.Tweaks.Blessings {
    internal class PlantBlessing {

        public static void AddPlantBlessing() {

            var PlantDomainAllowed = Resources.GetBlueprintReference<BlueprintFeatureReference>("0e03c2a03222b0b42acf96096b286327");
            var WarpriestClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("30b5e47d47a0e37438cc5a80c96cfb99");
            var WarpriestAspectOfWarBuff = Resources.GetBlueprint<BlueprintBuff>("27d14b07b52c2df42a4dcd6bfb840425");
            var BlessingResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("d128a6332e4ea7c4a9862b9fdb358cca");
            var EntangledBuff = Resources.GetBlueprint<BlueprintBuff>("d1aea643c260c5e4ea66012876f2b7f5");
            var PaladinClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var RangerClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("cda0615668a6df14eb36ba19ee881af6");
            var DivineTrackerArchetype = Resources.GetModBlueprint<BlueprintArchetype>("DivineTrackerArchetype");


            var PlantBlessingMinorBuff = Helpers.CreateBuff("PlantBlessingMinorBuff", bp => {
                bp.SetName("Creeping Vines");
                bp.SetDescription("At 1st level, as a swift action you can cause any creature you hit this round with a melee attackto sprout entangling vines that " +
                    "attempt to hold it in place, entangling it for 1 round (Reflex negates).");
                bp.m_Icon = EntangledBuff.Icon;
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.OnlyHit = true;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionSavingThrow() {
                            Type = SavingThrowType.Reflex,
                            FromBuff = false,
                            HasCustomDC = true,
                            CustomDC = new ContextValue() { 
                                ValueType = ContextValueType.Shared,
                                Value = 0,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                m_AbilityParameter = AbilityParameterType.Level
                            },
                            Actions = Helpers.CreateActionList(
                                new ContextActionConditionalSaved() {
                                    Succeed = Helpers.CreateActionList(),
                                    Failed = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = EntangledBuff.ToReference<BlueprintBuffReference>(),
                                            Permanent = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = new ContextValue(),
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Simple,
                                                    Value = 1,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                m_IsExtendable = true
                                            },
                                            IsFromSpell = false,
                                        })
                                }
                                )
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.Archetype = DivineTrackerArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { TempleChampionArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        WarpriestClass,
                        RangerClass,
                        PaladinClass
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Wisdom;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.BonusValue;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.DamageDice,
                            ValueShared = AbilitySharedValue.Damage
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.StatBonus,
                            ValueShared = AbilitySharedValue.StatBonus
                        }
                    };
                    c.Modifier = 1;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var PlantBlessingMinorAbility = Helpers.CreateBlueprint<BlueprintAbility>("PlantBlessingMinorAbility", bp => {
                bp.SetName("Creeping Vines");
                bp.SetDescription("At 1st level, as a swift action you can cause any creature you hit this round with a melee attackto sprout entangling vines that " +
                    "attempt to hold it in place, entangling it for 1 round (Reflex negates).");
                bp.m_Icon = EntangledBuff.Icon;
                bp.AddComponent<SpellComponent>(c => {
                    c.m_Flags = 0;
                    c.School = SpellSchool.Enchantment;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;

                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = PlantBlessingMinorBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue(),
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 3,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = true,
                        });
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("PlantBlessingMinorAbility.Duration", "1 round");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();

            });



            var PlantBlessingFeature = Helpers.CreateBlueprint<BlueprintFeature>("PlantBlessingFeature", bp => {
                bp.SetName("Plant");
                bp.SetDescription("At 1st level, as a swift action you can cause any creature you hit this round with a melee attackto sprout entangling vines that " +
                    "attempt to hold it in place, entangling it for 1 round (Reflex negates). \nAt 10th level, you can summon a battle companion. This ability functions " +
                    "as summon nature's ally IV with a duration of 1 minute. This ability can summon only one animal, regardless of the list used, and the creature’s " +
                    "type changes to plant instead of animal. For every 2 levels beyond 10th, the level of the summon nature's ally {g|Encyclopedia:Spell}spell{/g} " +
                    "increases by 1 (to a maximum of summon nature's ally IX at 20th level).");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { 
                        PlantBlessingMinorAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = WarpriestClass;
                    c.Level = 10;
                    c.m_Feature = PlantBlessingMajorFeature.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.CheckInProgression = true;
                    c.HideInUI = true;
                    c.m_Feature = PlantDomainAllowed;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.WarpriestBlessing };
            });
            BlessingTools.RegisterBlessing(PlantBlessingFeature);
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerPlantBlessingFeature", PlantBlessingFeature, "At 1st level, as a swift action you can cause any creature you hit this round with a melee attackto sprout entangling vines that attempt to hold it in place, entangling it for 1 round (Reflex negates). \nAt 13th level, you can summon a battle companion. This ability functions as summon nature's ally V with a duration of 1 minute. This ability can summon only one animal, regardless of the list used, and the creature’s type changes to plant instead of animal. For every 2 levels beyond 12th, the level of the summon nature's ally {g|Encyclopedia:Spell}spell{/g} increases by 1 (to a maximum of summon nature's ally IX at 20th level).");

            //Added in ModSupport
            var DivineTrackerPlantBlessingFeature = Resources.GetModBlueprint<BlueprintFeature>("DivineTrackerPlantBlessingFeature");
            var QuickenBlessingPlantFeature = Helpers.CreateBlueprint<BlueprintFeature>("QuickenBlessingPlantFeature", bp => {
                bp.SetName("Quicken Blessing — Plant");
                bp.SetDescription("Choose one of your blessings that normally requires a standard action to use. You can expend two of your daily uses of blessings " +
                    "to deliver that blessing (regardless of whether it’s a minor or major effect) as a swift action instead.");
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Feat };
                bp.AddComponent<AbilityActionTypeConversion>(c => {
                    c.m_Abilities = new BlueprintAbilityReference[] {
                        Resources.GetModBlueprint<BlueprintAbility>("PlantBlessingMajorAbility").ToReference<BlueprintAbilityReference>()
                    };
                    c.ResourceMultiplier = 2;
                    c.ActionType = UnitCommand.CommandType.Swift;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.Amount = 1;
                    c.m_Features = new BlueprintFeatureReference[] {
                        PlantBlessingFeature.ToReference<BlueprintFeatureReference>(),
                        DivineTrackerPlantBlessingFeature.ToReference<BlueprintFeatureReference>()
                    };
                });
            });


        }

    }
}
