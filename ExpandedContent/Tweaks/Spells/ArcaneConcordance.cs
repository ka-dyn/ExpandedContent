using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Components;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.Settings;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;

namespace ExpandedContent.Tweaks.Spells {
    internal class ArcaneConcordance {
        public static void AddArcaneConcordance() {
            var ArcaneConcordanceIcon = AssetLoader.LoadInternal("Skills", "Icon_ArcaneConcordance.jpg");

            //Main ability
            var ArcaneConcordanceAbility = Helpers.CreateBlueprint<BlueprintAbility>("ArcaneConcordanceAbility", bp => {
                bp.SetName("Arcane Concordance");
                bp.SetDescription("A shimmering, blue and gold radiance surrounds you, enhancing arcane spells cast within its area. Any arcane spell cast by " +
                    "a creature within the area gains a +1 enhancement bonus to the DC of any saving throws against the spell, and is cast as if one of either " +
                    "the Extend Spell or Reach spell metamagic feats have been applied to it (without increasing the spell level or Casting Time; you choose the " +
                    "metamagic feat when you cast arcane concordance).");
                bp.AddComponent<AbilityVariants>(c => {
                    c.m_Variants = new BlueprintAbilityReference[] {

                    };
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Evocation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.AOE;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = ArcaneConcordanceIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("ArcaneConcordanceAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            #region Extend

            var ExtendArcaneConcordanceEffectBuff = Helpers.CreateBuff("ExtendArcaneConcordanceEffectBuff", bp => {
                bp.SetName("Arcane Concordance - Extend");
                bp.SetDescription("While inside the arcane concordance aura, any arcane spell cast my you gains a +1 enhancement bonus to the DC of any saving throws against the " +
                    "spell, and is cast as if the Extend Spell metamagic feat has been applied to it (without increasing the spell level or Casting Time).");
                bp.AddComponent<AutoMetamagicMagicSourceOnly>(c => {
                    c.m_AffectedSpellSource = AutoMetamagicMagicSourceOnly.AffectedSpellSource.Arcane;
                    c.Metamagic = Metamagic.Extend;
                    c.Once = false;
                });
                bp.m_Icon = ArcaneConcordanceIcon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var ExtendArcaneConcordanceArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("ExtendArcaneConcordanceArea", bp => {
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Any;
                bp.SpellResistance = false;
                bp.AggroEnemies = false;
                bp.AffectEnemies = true;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = 13.Feet();
                bp.Fx = new PrefabLink();
                bp.AddComponent(AuraUtils.CreateUnconditionalAuraEffect(ExtendArcaneConcordanceEffectBuff.ToReference<BlueprintBuffReference>()));
            });
            var ExtendArcaneConcordanceSelfBuff = Helpers.CreateBuff("ExtendArcaneConcordanceSelfBuff", bp => {
                bp.SetName("Arcane Concordance - Extend Self Buff");
                bp.SetDescription("");
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = ExtendArcaneConcordanceArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell| BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
            });
            var ExtendArcaneConcordanceAbility = Helpers.CreateBlueprint<BlueprintAbility>("ExtendArcaneConcordanceAbility", bp => {
                bp.SetName("Arcane Concordance - Extend");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nExtend: You gain cold resistance 30, and a +6 " +
                    "enhancement to your CMD.");
                bp.m_Parent = ArcaneConcordanceAbility.ToReference<BlueprintAbilityReference>();
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ExtendArcaneConcordanceSelfBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Evocation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.AOE;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = ArcaneConcordanceIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("ArcaneConcordanceAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            ArcaneConcordanceAbility.GetComponent<AbilityVariants>().m_Variants = ArcaneConcordanceAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(ExtendArcaneConcordanceAbility.ToReference<BlueprintAbilityReference>());
            #endregion
            #region Reach
            var RainModerate = Resources.GetBlueprint<BlueprintBuff>("f37b708de9eeb2c4ab248d79bb5b5aa7");
            var SnowModerateBuff = Resources.GetBlueprint<BlueprintBuff>("845332298344c6447972dc9b131add08");
            var RainStormBuff = Resources.GetBlueprint<BlueprintBuff>("7c260a8970e273d439f2a2e19b7196af");
            var RainHeavyBuff = Resources.GetBlueprint<BlueprintBuff>("5c315bec0240479d9fafcc65b9efb574");
            var RainLightBuff = Resources.GetBlueprint<BlueprintBuff>("b13768381de549e2a78f502fa65dd613");
            var SnowHeavyBuff = Resources.GetBlueprint<BlueprintBuff>("4a15ab872f11463da1c1265d5b4324ad");
            var SnowLightBuff = Resources.GetBlueprint<BlueprintBuff>("26d8835510914ca2a8fe74b1519c09ac");
            var InsideTheStormBuff = Resources.GetBlueprint<BlueprintBuff>("32e90ae6f8c7656448d9e80173222012");
            var ReachArcaneConcordanceBuff = Helpers.CreateBuff("ReachArcaneConcordanceBuff", bp => {
                bp.SetName("Arcane Concordance - Reach");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nReach: You do not take penalties or damage or " +
                    "suffer from reduced visibility or other effects due to natural Reach. You gain electricity resistance 30, and your call lightning and call lightning storm spells and effects " +
                    "always function as though called outdoors in stormy Reach.");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Electricity;
                    c.Value = 30;
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = RainModerate.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = SnowModerateBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = RainStormBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = RainHeavyBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = RainLightBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = SnowHeavyBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = SnowLightBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = InsideTheStormBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true,
                            },
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                });
                bp.m_Icon = CallLightningStorm.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var ReachArcaneConcordanceAbility = Helpers.CreateBlueprint<BlueprintAbility>("ReachArcaneConcordanceAbility", bp => {
                bp.SetName("Arcane Concordance - Reach");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nReach: You do not take penalties or damage or " +
                    "suffer from reduced visibility or other effects due to natural Reach. You gain electricity resistance 30, and your call lightning and call lightning storm spells and effects " +
                    "always function as though called outdoors in stormy Reach.");
                bp.m_Parent = ArcaneConcordanceAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ReachDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ReachArcaneConcordanceBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = ReachDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = CallLightningStorm.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("ArcaneConcordanceAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            ArcaneConcordanceAbility.GetComponent<AbilityVariants>().m_Variants = ArcaneConcordanceAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(ReachArcaneConcordanceAbility.ToReference<BlueprintAbilityReference>());
            #endregion
            ArcaneConcordanceAbility.AddToSpellList(SpellTools.SpellList.BardSpellList, 3);
            ArcaneConcordanceAbility.AddToSpellList(SpellTools.SpellList.AzataMythicSpelllist, 3);
            ArcaneConcordanceAbility.AddToSpellList(SpellTools.SpellList.AeonSpellList, 3);
        }
    }
}
