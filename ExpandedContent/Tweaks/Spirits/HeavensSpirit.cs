using ExpandedContent.Extensions;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.ElementsSystem;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.RuleSystem;
using Kingmaker.Utility;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.RuleSystem.Rules.Damage;
using ExpandedContent.Config;
using Kingmaker.Blueprints.Classes.Selection;
using ExpandedContent.Utilities;
using Kingmaker.Designers.EventConditionActionSystem.Evaluators;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Enums.Damage;

namespace ExpandedContent.Tweaks.Spirits {
    internal class HeavensSpirit {
        public static void AddHeavensSprit() {

            var ShamanClass = Resources.GetBlueprint<BlueprintCharacterClass>("145f1d3d360a7ad48bd95d392c81b38e");
            var UnswornShamanArchetype = Resources.GetBlueprint<BlueprintArchetype>("556590a43467a27459ac1a80324c9f9f");
            var ShamanHexSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("4223fe18c75d4d14787af196a04e14e7");

            var GlitterdustBuff = Resources.GetBlueprint<BlueprintBuff>("03457e519288aad4085eae91918a76bf");



            //Spelllist
            var ColorSpraySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("91da41b9793a4624797921f221db653c");
            var RainbowPatternSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4b8265132f9c8174f87ce7fa6d0fe47b");
            var PrismaticSpraySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("b22fd434bdb60fb4ba1068206402c4cf");
            var ChainLightningSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("645558d63604747428d55f0dd3a4cb58");
            var SearingLightSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("bf0accce250381a44b857d4af6c8e10d");
            var HypnoticPatternAbility = Resources.GetModBlueprint<BlueprintAbility>("HypnoticPatternAbility");
            var SunburstSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e96424f70ff884947b06f41a765b7658");
            var PolarMidnightSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("ba48abb52b142164eba309fd09898856");
            var BreakEnchantmentSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("7792da00c85b9e042a0fdfc2b66ec9a8");
            var HeavensSpritSpells = Helpers.CreateBlueprint<BlueprintFeature>("HeavensSpritSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ColorSpraySpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HypnoticPatternAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SearingLightSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = RainbowPatternSpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BreakEnchantmentSpell;
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ChainLightningSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PrismaticSpraySpell;
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SunburstSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PolarMidnightSpell;
                    c.SpellLevel = 9;
                });
            });






            var ShamanHeavensSpiritBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ShamanHeavensSpiritBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Charisma,
                };
            });
            var ShamanHeavensSpiritBaseAbilityBuff = Helpers.CreateBuff("ShamanHeavensSpiritBaseAbilityBuff", bp => {
                bp.SetName("Stardust");
                bp.SetDescription("As a standard action, the shaman causes stardust to materialize around one creature within 30 feet. This stardust causes the target to shed light " +
                    "as a candle, and it cannot benefit from concealment or any invisibility effects. The creature takes a –1 penalty on attack rolls and sight-based Perception " +
                    "checks. This penalty to attack rolls and Perception checks increases by 1 at 4th level and every 4 levels thereafter, to a maximum of –6 at 20th level. This " +
                    "effect lasts for a number of rounds equal to half the shaman’s level (minimum 1). Sightless creatures cannot be affected by this ability.");
                bp.m_Icon = GlitterdustBuff.Icon;
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.AdditionalAttackBonus;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Multiplier = -1;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.SkillPerception;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Multiplier = -1;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Class = new BlueprintCharacterClassReference[] { ShamanClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Progression = ContextRankProgression.OnePlusDivStep;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 4;
                    c.m_UseMax = true;
                    c.m_Max = 5;
                });
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.GreaterInvisibility;
                });
                bp.AddComponent<DoNotBenefitFromConcealment>();
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var ShamanHeavensSpiritBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHeavensSpiritBaseAbility", bp => {
                bp.SetName("Stardust");
                bp.SetDescription("As a standard action, the shaman causes stardust to materialize around one creature within 30 feet. This stardust causes the target to shed light " +
                    "as a candle, and it cannot benefit from concealment or any invisibility effects. The creature takes a –1 penalty on attack rolls and sight-based Perception " +
                    "checks. This penalty to attack rolls and Perception checks increases by 1 at 4th level and every 4 levels thereafter, to a maximum of –6 at 20th level. This " +
                    "effect lasts for a number of rounds equal to half the shaman’s level (minimum 1). Sightless creatures cannot be affected by this ability. The shaman can use " +
                    "this ability a number of times per day equal to 3 + her Charisma modifier.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ShamanHeavensSpiritBaseAbilityBuff.ToReference<BlueprintBuffReference>(),
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
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Class = new BlueprintCharacterClassReference[] { ShamanClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Progression = ContextRankProgression.Div2;
                    
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ShamanHeavensSpiritBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Debuff;
                });
                bp.m_Icon = GlitterdustBuff.Icon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("ShamanHeavensSpiritBaseAbility.Duration", "1 round/2 levels");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShamanHeavensSpiritBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHeavensSpiritBaseFeature", bp => {
                bp.SetName("Stardust");
                bp.SetDescription("As a standard action, the shaman causes stardust to materialize around one creature within 30 feet. This stardust causes the target to shed light " +
                    "as a candle, and it cannot benefit from concealment or any invisibility effects. The creature takes a –1 penalty on attack rolls and sight-based Perception " +
                    "checks. This penalty to attack rolls and Perception checks increases by 1 at 4th level and every 4 levels thereafter, to a maximum of –6 at 20th level. This " +
                    "effect lasts for a number of rounds equal to half the shaman’s level (minimum 1). Sightless creatures cannot be affected by this ability. The shaman can use " +
                    "this ability a number of times per day equal to 3 + her Charisma modifier.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ShamanHeavensSpiritBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ShamanHeavensSpiritBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });                
                bp.m_AllowNonContextActions = false;                
                bp.IsClassFeature = true;
            });
            var ShamanHeavensSpiritGreaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHeavensSpiritGreaterFeature", bp => {
                bp.SetName("Void Adaptation");
                bp.SetDescription("The shaman gains cold resistance 15. In addition, every time you miss an attack roll because of {g|Encyclopedia:Concealment}concealment{/g}, " +
                    "you can {g|Encyclopedia:Dice}reroll{/g} your miss chance percentile roll one time to see if you actually hit.");
                bp.AddComponent<RerollConcealment>(c => {
                    c.m_WeaponCoverage = RerollConcealment.WeaponCoverage.All;
                });
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Value = 15;
                    c.Type = DamageEnergyType.Cold;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });




            var ShamanHeavensSpiritProgression = Helpers.CreateBlueprint<BlueprintProgression>("ShamanHeavensSpiritProgression", bp => {
                bp.SetName("Heavens");
                bp.SetDescription("A shaman who selects the heavens spirit has eyes that sparkle like starlight, exuding an aura of otherworldliness to those she is around. " +
                    "When she calls upon one of this spirit’s abilities, her eyes turn pitch black and the colors around her drain for a brief moment.");
                bp.AddComponent<AddFeaturesFromSelectionToDescription>(c => {
                    c.Introduction = Helpers.CreateString("ShamanHeavensSpritProgression.AddFeaturesFromSelectionToDescription.Introduction", "Additional Hexes:");
                    c.m_FeatureSelection = ShamanHexSelection.ToReference<BlueprintFeatureSelectionReference>();
                    c.OnlyIfRequiresThisFeature = true;
                });
                bp.AddComponent<AddSpellsToDescription>(c => {
                    c.Introduction = Helpers.CreateString("ShamanHeavensSpritProgression.AddSpellsToDescription.Introduction", "Bonus Spells:");
                    c.m_SpellLists = new BlueprintSpellListReference[] { HeavensSpritSpells.ToReference<BlueprintSpellListReference>() };
                });
                bp.m_AllowNonContextActions = false;                
                bp.Groups = new FeatureGroup[] { FeatureGroup.ShamanSpirit };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ShamanClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {};
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, ShamanHeavensSpiritBaseFeature, HeavensSpritSpells),
                    Helpers.LevelEntry(8, ShamanHeavensSpiritGreaterFeature),
                    Helpers.LevelEntry(16, ShamanHeavensSpiritTrueFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });



        }
    }
}
