using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.Settings;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;

namespace ExpandedContent.Tweaks.Spells {
    internal class ParticulateForm {
        public static void AddParticulateForm() {
            var ParticulateFormIcon = AssetLoader.LoadInternal("Skills", "Icon_ParticulateForm.jpg");
            var Icon_ScrollOfParticulateForm = AssetLoader.LoadInternal("Items", "Icon_ScrollOfParticulateForm.png");
            var RemoveDisease = Resources.GetBlueprint<BlueprintAbility>("4093d5a0eb5cae94e909eb1e0e1a6b36");

            var ParticulateFormBuffAbility = Helpers.CreateBlueprint<BlueprintAbility>("ParticulateFormBuffAbility", bp => {
                bp.SetName("Particulate Form");
                bp.SetDescription("You may end the effect of particulate form on yourself early as a swift action; regaining 5d6 hit points and attempting a " +
                    "additional saving throw against one disease or poison affecting you (at the original save DC), ending that disease or poison with a successful saving throw.");                
                //Adding the effect after the buff as it's easier and I want a nap, will tidy up later or forget.               
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = RemoveDisease.Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = new Metamagic();
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var ParticulateFormBuff = Helpers.CreateBuff("ParticulateFormBuff", bp => {
                bp.SetName("Particulate Form");
                bp.SetDescription("The targets’ physical forms undergo a bizarre transformation. They look and function normally, but are composed of countless particles that separate and reconnect " +
                    "to remain whole. Each target gains fast healing 1 and is immune to bleed damage, critical hits, sneak attacks, and other forms of precision damage. The value of this fast healing " +
                    "increases by 1 at caster levels 10th, 15th, and 20th. Any target can end the spell effect on itself as a swift action; the target then regains 5d6 hit points and can attempt an " +
                    "additional saving throw against one disease or poison affecting it (at the original save DC), ending that disease or poison with a successful saving throw.");
                bp.m_Icon = ParticulateFormIcon;
                bp.AddComponent<AddEffectContextFastHealing>(c => {
                    c.Bonus = new ContextValue() {
                        ValueType = ContextValueType.Rank
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.StartPlusDivStep;
                    c.m_StartLevel = 5;
                    c.m_StepLevel = 5;
                    c.m_UseMax = true;
                    c.m_Max = 20;
                });
                bp.AddComponent<AddImmunityToCriticalHits>();
                bp.AddComponent<AddImmunityToPrecisionDamage>();
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => {
                    c.Descriptor = SpellDescriptor.Bleed;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ParticulateFormBuffAbility.ToReference<BlueprintUnitFactReference>()
                    };
                    c.CasterLevel = 0;
                    c.DoNotRestoreMissingFacts = false;
                    c.HasDifficultyRequirements = false;
                    c.InvertDifficultyRequirements = false;
                    c.MinDifficulty = GameDifficultyOption.Story;
                });
                bp.AddComponent<ReplaceAbilityParamsWithContext>(c => {
                    c.m_Ability = ParticulateFormBuffAbility.ToReference<BlueprintAbilityReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
            });

            ParticulateFormBuffAbility.AddComponent<AbilityEffectRunAction>(c => {
                c.SavingThrowType = SavingThrowType.Unknown;
                c.Actions = Helpers.CreateActionList(
                    new ContextActionHealTarget() {
                        Value = new ContextDiceValue() {
                            DiceType = DiceType.D6,
                            DiceCountValue = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 5,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            BonusValue = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 0,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                        },
                    },
                    new ContextActionDispelMagic() {
                        m_StopAfterCountRemoved = true,
                        m_CountToRemove = 1,
                        m_BuffType = ContextActionDispelMagic.BuffType.All,
                        m_MaxCasterLevel = 0,
                        m_UseMaxCasterLevel = false,
                        m_MaxSpellLevel = 0,
                        m_CheckType = RuleDispelMagic.CheckType.DC,
                        Descriptor = SpellDescriptor.Disease | SpellDescriptor.Poison
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = ParticulateFormBuff.ToReference<BlueprintBuffReference>()
                    });
            });




            var ParticulateFormAbility = Helpers.CreateBlueprint<BlueprintAbility>("ParticulateFormAbility", bp => {
                bp.SetName("Particulate Form");
                bp.SetDescription("The targets’ physical forms undergo a bizarre transformation. They look and function normally, but are composed of countless particles that separate and reconnect " +
                    "to remain whole. Each target gains fast healing 1 and is immune to bleed damage, critical hits, sneak attacks, and other forms of precision damage. The value of this fast healing " +
                    "increases by 1 at caster levels 10th, 15th, and 20th. Any target can end the spell effect on itself as a swift action; the target then regains 5d6 hit points and can attempt an " +
                    "additional saving throw against one disease or poison affecting it (at the original save DC), ending that disease or poison with a successful saving throw.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ParticulateFormBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
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
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 30.Feet();
                    c.m_TargetType = TargetType.Ally;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker();
                    c.m_SpreadSpeed = 0.Feet();
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = ParticulateFormIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic =  Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.Extend| Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("ParticulateFormAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ParticulateFormScroll = ItemTools.CreateScroll("ScrollOfParticulateForm", Icon_ScrollOfParticulateForm, ParticulateFormAbility, 7, 13);
            VenderTools.AddScrollToLeveledVenders(ParticulateFormScroll);
            ParticulateFormAbility.AddToSpellList(SpellTools.SpellList.ClericSpellList, 7);
            ParticulateFormAbility.AddToSpellList(SpellTools.SpellList.WitchSpellList, 7);
            ParticulateFormAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 7);
            ParticulateFormAbility.AddToSpellList(SpellTools.SpellList.AeonSpellList, 7);
        }
    }
}
