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
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Buffs.Blueprints;

namespace ExpandedContent.Tweaks.Spirits.Hexes {
    internal class SluggishHex {
        public static void AddSluggishHex() {

            var ShamanClass = Resources.GetBlueprint<BlueprintCharacterClass>("145f1d3d360a7ad48bd95d392c81b38e");

            var ShamanFrostSpiritProgression = Resources.GetBlueprint<BlueprintProgression>("44c130a12d49ebf4a95945feffe5aae7");
            var ShamanFrostSpiritWanderingFeature = Resources.GetBlueprint<BlueprintFeature>("6aeab860502ca2643affcbf1a83187e0");
            var ShamanFrostSpiritBaseFeature = Resources.GetBlueprint<BlueprintProgression>("30dc785861c12374c910e5ede5b87ef4");

            var ShamanHexSluggishCooldown = Helpers.CreateBuff("ShamanHexSluggishCooldown", bp => {
                bp.SetName("Already targeted by this hex today");
                bp.SetDescription("");
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });

            var SlowIcon = Resources.GetBlueprint<BlueprintAbility>("f492622e473d34747806bdb39356eb89").Icon;

            var ShamanHexSluggishBuff = Helpers.CreateBuff("ShamanHexSluggishBuff", bp => {
                bp.SetName("Sluggish");
                bp.SetDescription("The shaman causes the speed of a creature within 30 feet to be halved. The target can attempt a Fortitude saving throw to negate this effect. " +
                    "The penalty lasts for a number of rounds equal to the shaman’s character level and does not stack with other effects that reduce speed. Whether or not the " +
                    "save is successful, the creature can’t be the target of this hex again for 24 hours.");
                bp.m_Icon = SlowIcon;
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = Kingmaker.UnitLogic.UnitCondition.Slowed;
                });
                bp.AddComponent<ChangeImpatience>(c => {
                    c.Delta = -1;
                });
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = new Kingmaker.ResourceLinks.PrefabLink() { AssetId = "d6a8c6aead8ec1e4b9362cff1c2373a2" };
            });


            var ShamanHexSluggishAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHexSluggishAbility", bp => {
                bp.SetName("Sluggish");
                bp.SetDescription("The shaman causes the speed of a creature within 30 feet to be halved. The target can attempt a Fortitude saving throw to negate this effect. " +
                    "The penalty lasts for a number of rounds equal to the shaman’s character level and does not stack with other effects that reduce speed. Whether or not the " +
                    "save is successful, the creature can’t be the target of this hex again for 24 hours.");
                bp.m_Icon = SlowIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSavingThrow() {
                            Type = SavingThrowType.Fortitude,
                            FromBuff = false,
                            HasCustomDC = false,
                            CustomDC = new ContextValue(),
                            Actions = Helpers.CreateActionList(
                                new ContextActionConditionalSaved() {
                                    Succeed = Helpers.CreateActionList(),
                                    Failed = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = ShamanHexSluggishBuff.ToReference<BlueprintBuffReference>(),
                                            UseDurationSeconds = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    ValueRank = AbilityRankType.Default
                                                },
                                                m_IsExtendable = true
                                            },
                                            DurationSeconds = 0,
                                            IsFromSpell = false,
                                            ToCaster = false,
                                            AsChild = true,
                                        }
                                    )
                                }
                                )
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanHexSluggishCooldown.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Days,
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Charisma;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ShamanClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] {
                        ShamanHexSluggishCooldown.ToReference<BlueprintUnitFactReference>()
                    };
                    c.Inverted = true;
                    c.FromCaster = true;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Fortitude;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Debuff;
                });
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.StatType = StatType.Wisdom;
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Hex;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("ShamanHexSluggishAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });



            var ShamanHexSluggishFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexSluggishFeature", bp => {
                bp.SetName("Sluggish");
                bp.SetDescription("The shaman causes the speed of a creature within 30 feet to be halved. The target can attempt a Fortitude saving throw to negate this effect. " +
                    "The penalty lasts for a number of rounds equal to the shaman’s character level and does not stack with other effects that reduce speed. Whether or not the " +
                    "save is successful, the creature can’t be the target of this hex again for 24 hours.");
                bp.m_Icon = SlowIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanHexSluggishAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanFrostSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanFrostSpiritWanderingFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanFrostSpiritBaseFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ShamanHex };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            SpiritTools.RegisterShamanHex(ShamanHexSluggishFeature);

            ShamanFrostSpiritProgression.IsPrerequisiteFor.Add(ShamanHexSluggishFeature.ToReference<BlueprintFeatureReference>());

        }
    }
}
