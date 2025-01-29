using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.Settings;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System.Linq;

namespace ExpandedContent.Tweaks.Spells {
    internal class MomentOfPrescience {
        public static void AddMomentOfPrescience() {
            var MomentOfPrescienceIcon = AssetLoader.LoadInternal("Skills", "Icon_MomentOfPrescience.jpg");
            //var Icon_ScrollOfMomentOfPrescience = AssetLoader.LoadInternal("Items", "Icon_ScrollOfMomentOfPrescience.png");
            var DivinationSchool = Resources.GetBlueprint<BlueprintProgression>("d7d18ce5c24bd324d96173fdc3309646");
            var MomentOfPrescienceBuffIcon = DivinationSchool.Icon;

            var MomentOfPrescienceAttackRollBuff = Helpers.CreateBuff("MomentOfPrescienceAttackRollBuff", bp => {
                bp.SetName("Moment of Prescience - Attack Roll");
                bp.SetDescription("Your next attack roll gains an insight bonus equal to your caster level (maximum +25). if you apply another moment of prescience, this will be removed without effect.");
                bp.m_Icon = MomentOfPrescienceBuffIcon;
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.AdditionalAttackBonus;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                    c.Descriptor = ModifierDescriptor.Insight;
                });
                bp.AddComponent<AddInitiatorAttackRollTrigger>(c => {
                    c.OnlyHit = false;
                    c.CriticalHit = false;
                    c.SneakAttack = false;
                    c.OnOwner = false;
                    c.CheckWeapon = false;
                    c.WeaponCategory = WeaponCategory.UnarmedStrike;
                    c.AffectFriendlyTouchSpells = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = true;
                    c.m_Max = 25;
                });
                bp.m_AllowNonContextActions = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell | BlueprintBuff.Flags.RemoveOnRest;
            });
            var MomentOfPrescienceSkillCheckBuff = Helpers.CreateBuff("MomentOfPrescienceSkillCheckBuff", bp => {
                bp.SetName("Moment of Prescience - Skill Check");
                bp.SetDescription("Your next skill check roll gains an insight bonus equal to your caster level (maximum +25). if you apply another moment of prescience, this will be removed without effect.");
                bp.m_Icon = MomentOfPrescienceBuffIcon;
                bp.AddComponent<BuffAllSkillsBonus>(c => {
                    c.Value = 1;
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Multiplier = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 1,
                        ValueRank = AbilityRankType.StatBonus
                    };
                });
                bp.AddComponent<AddInitiatorSkillRollTrigger>(c => {
                    c.OnlySuccess = false;
                    c.Skill = StatType.Unknown;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.AddComponent<AddInitiatorPartySkillRollTrigger>(c => {
                    c.OnlySuccess = false;
                    c.Skill = StatType.Unknown;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = true;
                    c.m_Max = 25;
                });
                bp.m_AllowNonContextActions = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell | BlueprintBuff.Flags.RemoveOnRest;
            });
            var MomentOfPrescienceSavingThrowBuff = Helpers.CreateBuff("MomentOfPrescienceSavingThrowBuff", bp => {
                bp.SetName("Moment of Prescience - Saving Throw");
                bp.SetDescription("Your next saving throw gains an insight bonus equal to your caster level (maximum +25). if you apply another moment of prescience, this will be removed without effect.");
                bp.m_Icon = MomentOfPrescienceBuffIcon;
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.SaveFortitude;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                    c.Descriptor = ModifierDescriptor.Insight;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.SaveFortitude;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                    c.Descriptor = ModifierDescriptor.Insight;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.SaveWill;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                    c.Descriptor = ModifierDescriptor.Insight;
                });
                bp.AddComponent<AddInitiatorSavingThrowTrigger>(c => {
                    c.OnlyPass = false;
                    c.OnlyFail = false;
                    c.SpecificSave = false;
                    c.ChooseSave = SavingThrowType.Fortitude;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = true;
                    c.m_Max = 25;
                });
                bp.m_AllowNonContextActions = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell | BlueprintBuff.Flags.RemoveOnRest;
            });
            var MomentOfPrescienceACBuff = Helpers.CreateBuff("MomentOfPrescienceACBuff", bp => {
                bp.SetName("Moment of Prescience - AC");
                bp.SetDescription("You gain a insight bonus to your AC equal to your caster level (maximum +25) against the next incoming attack roll. if you apply another moment of prescience, this will be removed without effect.");
                bp.m_Icon = MomentOfPrescienceBuffIcon;
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.AC;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                    c.Descriptor = ModifierDescriptor.Insight;
                });
                bp.AddComponent<AddTargetAttackRollTrigger>(c => {
                    c.OnlyHit = false;
                    c.CriticalHit = false;
                    c.OnlyMelee = false;
                    c.NotReach = false;
                    c.CheckCategory = false;
                    c.Not = false;
                    c.Categories = new WeaponCategory[0];
                    c.AffectFriendlyTouchSpells = false;
                    c.ActionsOnAttacker = Helpers.CreateActionList();
                    c.ActionOnSelf = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = true;
                    c.m_Max = 25;
                });
                bp.m_AllowNonContextActions = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell | BlueprintBuff.Flags.RemoveOnRest;
            });


            var MomentOfPrescienceBuffAbility = Helpers.CreateBlueprint<BlueprintAbility>("MomentOfPrescienceBuffAbility", bp => {
                bp.SetName("Moment of Prescience");
                bp.SetDescription("As a free action you may activate your moment of prescience, gaining an insight bonus equal to your caster level (maximum +25) " +
                    "on your next attack roll, skill check, or saving throw. Alternatively, you can apply the insight " +
                    "bonus to your AC against a single attack (even if flat-footed). You can’t have more than one moment of prescience active on you at the same time, " +
                    "however this effect will last until discharged or you rest.");
                bp.AddComponent<AbilityVariants>(c => {
                    c.m_Variants = new BlueprintAbilityReference[] {

                    };
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Divination;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = MomentOfPrescienceBuffIcon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("MomentOfPrescienceBuffAbility.Duration", "Until discharged");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            #region Ability Variants
            var MomentOfPrescienceAttackRollAbility = Helpers.CreateBlueprint<BlueprintAbility>("MomentOfPrescienceAttackRollAbility", bp => {
                bp.SetName("Moment of Prescience - Attack Roll");
                bp.SetDescription("As a free action you may activate your moment of prescience, gaining an insight bonus equal to your caster level (maximum +25) " +
                    "on your next attack roll, skill check, or saving throw. Alternatively, you can apply the insight " +
                    "bonus to your AC against a single attack (even if flat-footed). You can’t have more than one moment of prescience active on you at the same time, " +
                    "however this effect will last until discharged or you rest.");
                bp.m_Parent = MomentOfPrescienceBuffAbility.ToReference<BlueprintAbilityReference>();
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = MomentOfPrescienceAttackRollBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Divination;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = MomentOfPrescienceBuffIcon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            MomentOfPrescienceBuffAbility.GetComponent<AbilityVariants>().m_Variants = MomentOfPrescienceBuffAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(MomentOfPrescienceAttackRollAbility.ToReference<BlueprintAbilityReference>());
            var MomentOfPrescienceSkillCheckAbility = Helpers.CreateBlueprint<BlueprintAbility>("MomentOfPrescienceSkillCheckAbility", bp => {
                bp.SetName("Moment of Prescience - Skill Check");
                bp.SetDescription("As a free action you may activate your moment of prescience, gaining an insight bonus equal to your caster level (maximum +25) " +
                    "on your next attack roll, skill check, or saving throw. Alternatively, you can apply the insight " +
                    "bonus to your AC against a single attack (even if flat-footed). You can’t have more than one moment of prescience active on you at the same time, " +
                    "however this effect will last until discharged or you rest.");
                bp.m_Parent = MomentOfPrescienceBuffAbility.ToReference<BlueprintAbilityReference>();
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = MomentOfPrescienceSkillCheckBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Divination;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = MomentOfPrescienceBuffIcon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            MomentOfPrescienceBuffAbility.GetComponent<AbilityVariants>().m_Variants = MomentOfPrescienceBuffAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(MomentOfPrescienceSkillCheckAbility.ToReference<BlueprintAbilityReference>());
            var MomentOfPrescienceSavingThrowAbility = Helpers.CreateBlueprint<BlueprintAbility>("MomentOfPrescienceSavingThrowAbility", bp => {
                bp.SetName("Moment of Prescience - Saving Throw");
                bp.SetDescription("As a free action you may activate your moment of prescience, gaining an insight bonus equal to your caster level (maximum +25) " +
                    "on your next attack roll, skill check, or saving throw. Alternatively, you can apply the insight " +
                    "bonus to your AC against a single attack (even if flat-footed). You can’t have more than one moment of prescience active on you at the same time, " +
                    "however this effect will last until discharged or you rest.");
                bp.m_Parent = MomentOfPrescienceBuffAbility.ToReference<BlueprintAbilityReference>();
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = MomentOfPrescienceSavingThrowBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Divination;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = MomentOfPrescienceBuffIcon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            MomentOfPrescienceBuffAbility.GetComponent<AbilityVariants>().m_Variants = MomentOfPrescienceBuffAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(MomentOfPrescienceSavingThrowAbility.ToReference<BlueprintAbilityReference>());
            var MomentOfPrescienceACAbility = Helpers.CreateBlueprint<BlueprintAbility>("MomentOfPrescienceACAbility", bp => {
                bp.SetName("Moment of Prescience - AC");
                bp.SetDescription("As a free action you may activate your moment of prescience, gaining an insight bonus equal to your caster level (maximum +25) " +
                    "on your next attack roll, skill check, or saving throw. Alternatively, you can apply the insight " +
                    "bonus to your AC against a single attack (even if flat-footed). You can’t have more than one moment of prescience active on you at the same time, " +
                    "however this effect will last until discharged or you rest.");
                bp.m_Parent = MomentOfPrescienceBuffAbility.ToReference<BlueprintAbilityReference>();
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = MomentOfPrescienceACBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Divination;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = MomentOfPrescienceBuffIcon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            MomentOfPrescienceBuffAbility.GetComponent<AbilityVariants>().m_Variants = MomentOfPrescienceBuffAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(MomentOfPrescienceACAbility.ToReference<BlueprintAbilityReference>());
            #endregion

            var MomentOfPrescienceBaseBuff = Helpers.CreateBuff("MomentOfPrescienceBaseBuff", bp => {
                bp.SetName("Moment of Prescience");
                bp.SetDescription("This spell grants you a sixth sense. Once during the spell’s duration, you may choose to use its effect. " +
                    "This spell grants you an insight bonus equal to your caster level (maximum +25) on any single attack roll, " +
                    "skill check, or saving throw. Alternatively, you can apply the insight bonus to your AC against a single attack " +
                    "(even if flat-footed). Activating the effect is a free action. Once used, the spell ends. You can’t have more than one moment of " +
                    "prescience active on you at the same time.");
                bp.m_Icon = MomentOfPrescienceIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        MomentOfPrescienceBuffAbility.ToReference<BlueprintUnitFactReference>()
                    };
                    c.CasterLevel = 0;
                    c.DoNotRestoreMissingFacts = false;
                    c.HasDifficultyRequirements = false;
                    c.InvertDifficultyRequirements = false;
                    c.MinDifficulty = GameDifficultyOption.Story;
                });
                bp.AddComponent<ReplaceAbilityParamsWithContext>(c => {
                    c.m_Ability = MomentOfPrescienceBuffAbility.ToReference<BlueprintAbilityReference>();
                });
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });

            var variants = new BlueprintAbility[] {
                MomentOfPrescienceAttackRollAbility, MomentOfPrescienceSkillCheckAbility, MomentOfPrescienceSavingThrowAbility, MomentOfPrescienceACAbility
            };
            foreach (var variant in variants) {
                variant.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Conditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] { }
                    };
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = MomentOfPrescienceAttackRollBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = MomentOfPrescienceSkillCheckBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = MomentOfPrescienceSavingThrowBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = MomentOfPrescienceACBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = MomentOfPrescienceBaseBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });

            }


            var MomentOfPrescienceAbility = Helpers.CreateBlueprint<BlueprintAbility>("MomentOfPrescienceAbility", bp => {
                bp.SetName("Moment of Prescience");
                bp.SetDescription("This spell grants you a sixth sense. Once during the spell’s duration, you may choose to use its effect. " +
                    "This spell grants you an insight bonus equal to your caster level (maximum +25) on any single attack roll, " +
                    "skill check, or saving throw. Alternatively, you can apply the insight bonus to your AC against a single attack " +
                    "(even if flat-footed). Activating the effect is a free action. Once used, the spell ends. You can’t have more than one moment of " +
                    "prescience active on you at the same time.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = MomentOfPrescienceBaseBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                }); bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Divination;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = MomentOfPrescienceIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.CompletelyNormal | Metamagic.Quicken;
                bp.LocalizedDuration = Helpers.CreateString("MomentOfPrescienceAbility.Duration", "1 hour/level or until discharged");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });



            //var MomentOfPrescienceScroll = ItemTools.CreateScroll("ScrollOfMomentOfPrescience", Icon_ScrollOfMomentOfPrescience, MomentOfPrescienceAbility, 8, 15);
            //VenderTools.AddScrollToLeveledVenders(MomentOfPrescienceScroll);
            MomentOfPrescienceAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 8);
            MomentOfPrescienceAbility.AddToSpellList(SpellTools.SpellList.WitchSpellList, 8);
            var LuckDomainSpellList = Resources.GetBlueprint<BlueprintSpellList>("9e756552e9b05ce459feac658dd2d8fb");
            LuckDomainSpellList.SpellsByLevel
                .Where(level => level.SpellLevel == 8)
                .ForEach(level => level.Spells.Clear());
            LuckDomainSpellList.SpellsByLevel[8].m_Spells.Add(MomentOfPrescienceAbility.ToReference<BlueprintAbilityReference>());
            var luckdomainstochangetext = new BlueprintProgression[] {
                Resources.GetBlueprint<BlueprintProgression>("8bd8cfad69085654b9118534e4aa215e"),
                Resources.GetBlueprint<BlueprintProgression>("1ba7fc652568a524db218ccff2f9ed90"),
                Resources.GetBlueprint<BlueprintProgression>("1f26551e7e61436c8ee188064038a896"),
            };
            foreach(var domain in luckdomainstochangetext) {
                domain.SetDescription("You are infused with luck, and your mere presence can spread good fortune.\r\nBit of Luck: You can touch a willing creature " +
                    "as a {g|Encyclopedia:Standard_Actions}standard action{/g}, giving it a bit of luck. For the next {g|Encyclopedia:Combat_Round}round{/g}, any time " +
                    "the target {g|Encyclopedia:Dice}rolls{/g} a d20, she may roll twice and take the more favorable result. You can use this ability a number of times " +
                    "per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\r\nDivine Fortune: At 6th level, as a standard {g|Encyclopedia:CA_Types}action{/g}, " +
                    "you can bless yourself with divine luck. For the next half your level in the class that gave you access to this domain rounds you roll two times on " +
                    "every d20 roll and take the best result. You can use this ability once per day at 6th level, and one additional time per day for every 6 levels in " +
                    "the class that gave you access to this domain beyond 6th.\r\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsTrueStrike}true strike{/g}, " +
                    "{g|SpellsAid}aid{/g}, {g|SpellsProtectionFromEnergy}protection from energy{/g}, {g|SpellsCommunalProtectionFromEnergy}protection from energy, communal{/g}, " +
                    "{g|SpellsBreakEnchantment}break enchantment{/g}, {g|SpellsMassCatsGrace}cat's grace, mass{/g}, {g|SpellsRestorationGreater}restoration, greater{/g}, " +
                    "moment of prescience, {g|SpellsMassHeal}heal, mass{/g}.");
            }


        }
    }
}
