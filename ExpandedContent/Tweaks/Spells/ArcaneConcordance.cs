using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Components;
using ExpandedContent.Tweaks.DemonLords;
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
using UnityEngine;

namespace ExpandedContent.Tweaks.Spells {
    internal class ArcaneConcordance {
        public static void AddArcaneConcordance() {
            var ArcaneConcordanceIcon = AssetLoader.LoadInternal("Skills", "Icon_ArcaneConcordance.jpg");
            var Icon_ScrollOfArcaneConcordance = AssetLoader.LoadInternal("Items", "Icon_ScrollOfArcaneConcordance.png");

            //Main ability
            var ArcaneConcordanceAbility = Helpers.CreateBlueprint<BlueprintAbility>("ArcaneConcordanceAbility", bp => {
                bp.SetName("Arcane Concordance");
                bp.SetDescription("A shimmering, blue and gold radiance surrounds you, enhancing arcane spells cast within its area. Any arcane spell cast by " +
                    "a creature within 10 feet of the caster gains a +1 enhancement bonus to the DC of any saving throws against the spell, and is cast as if one of either " +
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
                bp.SetDescription("While inside the arcane concordance aura, any arcane spell cast gains a +1 enhancement bonus to the DC of any saving throws against the " +
                    "spell, and is cast as if the Extend Spell metamagic feat has been applied to it (without increasing the spell level or Casting Time).");
                bp.AddComponent<AutoMetamagicMagicSourceOnly>(c => {
                    c.m_AffectedSpellSource = AutoMetamagicMagicSourceOnly.AffectedSpellSource.Arcane;
                    c.Metamagic = Metamagic.Extend;
                    c.Once = false;
                });
                bp.AddComponent<IncreaseSpellDCMagicSourceOnly>(c => {
                    c.BonusDC = 1;
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.m_AffectedSpellSource = IncreaseSpellDCMagicSourceOnly.AffectedSpellSource.Arcane;
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
                bp.Size = 10.Feet();
                bp.Fx = new PrefabLink() { AssetId = "6b75812d8c3b0d34f9bc204d6babc2a1" }; //Enchantment00_Alignment_Aoe_30Feet
                bp.AddComponent(AuraUtils.CreateUnconditionalAuraEffect(ExtendArcaneConcordanceEffectBuff.ToReference<BlueprintBuffReference>()));
            });
            ExtendArcaneConcordanceArea.Fx = ExtendArcaneConcordanceArea.Fx.CreateDynamicProxy(pfl => {
                Main.Log($"Editing: {pfl}");
                pfl.name = "ArcaneConcordanceExtend_10feetAoE";
                Main.Log($"{FxDebug.DumpGameObject(pfl.gameObject)}");
                pfl.transform.localScale = new(0.37f, 1.0f, 0.37f);
                Object.DestroyImmediate(pfl.transform.Find("Ground (1)/StartWave/Waves").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("Ground (1)/StartWave/BigWave_mid").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("Ground (1)/StartWave/Bottom_wave").gameObject);
                var sparks = pfl.transform.Find("Ground (1)/sparks (2)").GetComponent<ParticleSystem>();
                sparks.startColor = new Color(0.14f, 0.25f, 0.78f, 0.67f);
                var Border_particleEnergy = pfl.transform.Find("Ground (1)/Border_particleEnergy").GetComponent<ParticleSystem>();
                Border_particleEnergy.startColor = new Color(0f, 0.3578f, 1f, 1f);
                var Bottom_dark = pfl.transform.Find("Ground (1)/StartWave/Bottom_dark").GetComponent<ParticleSystem>();
                Bottom_dark.startColor = new Color(0.14f, 0.25f, 0.78f, 0.67f);
            });
            var ExtendArcaneConcordanceSelfBuff = Helpers.CreateBuff("ExtendArcaneConcordanceSelfBuff", bp => {
                bp.SetName("Arcane Concordance - Extend Aura");
                bp.SetDescription("A shimmering, blue and gold radiance surrounds you, enhancing arcane spells cast within its area. Any arcane spell cast by " +
                    "a creature within 10 feet of the caster gains a +1 enhancement bonus to the DC of any saving throws against the spell, and is cast as if the Extend Spell " +
                    "metamagic feats has been applied to it (without increasing the spell level).");
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = ExtendArcaneConcordanceArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
                bp.m_Icon = ArcaneConcordanceIcon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var ExtendArcaneConcordanceAbility = Helpers.CreateBlueprint<BlueprintAbility>("ExtendArcaneConcordanceAbility", bp => {
                bp.SetName("Arcane Concordance - Extend");
                bp.SetDescription("A shimmering, blue and gold radiance surrounds you, enhancing arcane spells cast within its area. Any arcane spell cast by " +
                    "a creature within 10 feet of the caster gains a +1 enhancement bonus to the DC of any saving throws against the spell, and is cast as if the Extend Spell " +
                    "metamagic feat has been applied to it (without increasing the spell level).");
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 20;
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
            var ReachArcaneConcordanceEffectBuff = Helpers.CreateBuff("ReachArcaneConcordanceEffectBuff", bp => {
                bp.SetName("Arcane Concordance - Reach");
                bp.SetDescription("While inside the arcane concordance aura, any arcane spell cast my you gains a +1 enhancement bonus to the DC of any saving throws against the " +
                    "spell, and is cast as if the Reach Spell metamagic feat has been applied to it (without increasing the spell level or Casting Time).");
                bp.AddComponent<AutoMetamagicMagicSourceOnly>(c => {
                    c.m_AffectedSpellSource = AutoMetamagicMagicSourceOnly.AffectedSpellSource.Arcane;
                    c.Metamagic = Metamagic.Reach;
                    c.Once = false;
                });
                bp.AddComponent<IncreaseSpellDCMagicSourceOnly>(c => {
                    c.BonusDC = 1;
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.m_AffectedSpellSource = IncreaseSpellDCMagicSourceOnly.AffectedSpellSource.Arcane;
                });
                bp.m_Icon = ArcaneConcordanceIcon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var ReachArcaneConcordanceArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("ReachArcaneConcordanceArea", bp => {
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Any;
                bp.SpellResistance = false;
                bp.AggroEnemies = false;
                bp.AffectEnemies = true;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = 10.Feet();
                bp.Fx = new PrefabLink() { AssetId = "6b75812d8c3b0d34f9bc204d6babc2a1" }; //Enchantment00_Alignment_Aoe_30Feet
                bp.AddComponent(AuraUtils.CreateUnconditionalAuraEffect(ReachArcaneConcordanceEffectBuff.ToReference<BlueprintBuffReference>()));
            });
            ReachArcaneConcordanceArea.Fx = ReachArcaneConcordanceArea.Fx.CreateDynamicProxy(pfl => {
                Main.Log($"Editing: {pfl}");
                pfl.name = "ArcaneConcordanceReach_10feetAoE";
                //Main.Log($"{FxDebug.DumpGameObject(pfl.gameObject)}");
                pfl.transform.localScale = new(0.37f, 1.0f, 0.37f);
                Object.DestroyImmediate(pfl.transform.Find("Ground (1)/StartWave/Waves").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("Ground (1)/StartWave/BigWave_mid").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("Ground (1)/StartWave/Bottom_wave").gameObject);
                var sparks = pfl.transform.Find("Ground (1)/sparks (2)").GetComponent<ParticleSystem>();
                sparks.startColor = new Color(0.992f, 0.884f, 0.066f, 1f);
                var Border = pfl.transform.Find("Ground (1)/Border").GetComponent<ParticleSystem>();
                Border.startColor = new Color(0f, 0.25f, 1f, 1f);
                var BorderWaves = pfl.transform.Find("Ground (1)/BorderWaves").GetComponent<ParticleSystem>();
                BorderWaves.startColor = new Color(0f, 0.25f, 1f, 1f);
                var EnergyWave_Start = pfl.transform.Find("Ground (1)/StartWave/EnergyWave_Start").GetComponent<ParticleSystem>();
                EnergyWave_Start.startColor = new Color(0f, 0.16f, 1f, 1f);
                var BigWave_trail = pfl.transform.Find("Ground (1)/StartWave/BigWave_trail").GetComponent<ParticleSystem>();
                BigWave_trail.startColor = new Color(0f, 0.253f, 1f, 1f);
            });
            var ReachArcaneConcordanceSelfBuff = Helpers.CreateBuff("ReachArcaneConcordanceSelfBuff", bp => {
                bp.SetName("Arcane Concordance - Reach Aura");
                bp.SetDescription("A shimmering, blue and gold radiance surrounds you, enhancing arcane spells cast within its area. Any arcane spell cast by " +
                    "a creature within 10 feet of the caster gains a +1 enhancement bonus to the DC of any saving throws against the spell, and is cast as if the Reach Spell " +
                    "metamagic feat has been applied to it (without increasing the spell level).");
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = ReachArcaneConcordanceArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
                bp.m_Icon = ArcaneConcordanceIcon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var ReachArcaneConcordanceAbility = Helpers.CreateBlueprint<BlueprintAbility>("ReachArcaneConcordanceAbility", bp => {
                bp.SetName("Arcane Concordance - Reach");
                bp.SetDescription("A shimmering, blue and gold radiance surrounds you, enhancing arcane spells cast within its area. Any arcane spell cast by " +
                    "a creature within 10 feet of the caster gains a +1 enhancement bonus to the DC of any saving throws against the spell, and is cast as if the Reach Spell " +
                    "metamagic feat has been applied to it (without increasing the spell level).");
                bp.m_Parent = ArcaneConcordanceAbility.ToReference<BlueprintAbilityReference>();
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ReachArcaneConcordanceSelfBuff.ToReference<BlueprintBuffReference>(),
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 20;
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
            ArcaneConcordanceAbility.GetComponent<AbilityVariants>().m_Variants = ArcaneConcordanceAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(ReachArcaneConcordanceAbility.ToReference<BlueprintAbilityReference>());
            #endregion

            var ArcaneConcordanceScroll = ItemTools.CreateScroll("ScrollOfArcaneConcordance", Icon_ScrollOfArcaneConcordance, ArcaneConcordanceAbility, 3, 7);
            VenderTools.AddScrollToLeveledVenders(ArcaneConcordanceScroll);

            ArcaneConcordanceAbility.AddToSpellList(SpellTools.SpellList.BardSpellList, 3);
            ArcaneConcordanceAbility.AddToSpellList(SpellTools.SpellList.AzataMythicSpelllist, 3);
            ArcaneConcordanceAbility.AddToSpellList(SpellTools.SpellList.AeonSpellList, 3);
        }
    }
}
