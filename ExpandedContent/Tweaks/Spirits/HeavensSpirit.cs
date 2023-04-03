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
using BlueprintCore.Utils;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.ResourceLinks;

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
            var PrismaticSpraySpell = Resources.GetBlueprint<BlueprintAbility>("b22fd434bdb60fb4ba1068206402c4cf");
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
                    c.m_Spell = PrismaticSpraySpell.ToReference<BlueprintAbilityReference>();
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

            var ScintillatingPatternSpell = Resources.GetBlueprint<BlueprintAbility>("4dc60d08c6c4d3c47b413904e4de5ff0");











            var ShamanHeavensPrismaticSprayResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ShamanHeavensPrismaticSprayResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByStat = false,
                };
            });
            var ShamanHeavensScintillatingPatternResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("ShamanHeavensScintillatingPatternResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByStat = false,
                };
            });












            var ShamanHeavensPrismaticSprayAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHeavensPrismaticSprayAbility", bp => {
                bp.SetName("Phantasmagoric Display - Prismatic Spray");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} causes seven shimmering, multicolored beams of light to spray from your hand. Each beam has a different power. Creatures in " +
                    "the area of the spell with 8 HD or less are automatically blinded for {g|Encyclopedia:Dice}2d4{/g} {g|Encyclopedia:Combat_Round}rounds{/g}. Every creature in the area is randomly " +
                    "struck by one or more beams, which have additional effects:\n20 points {g|Encyclopedia:Energy_Damage}fire damage{/g} (Reflex half)\n40 points acid " +
                    "{g|Encyclopedia:Damage}damage{/g} (Reflex half)\n80 points electricity damage (Reflex half)\nPoison (Frequency 1/rd. for 6 rds.; Init. effect {g|Encyclopedia:Injury_Death}death{/g}; " +
                    "Sec. effect 1 {g|Encyclopedia:Constitution}Con{/g}/rd.; {g|Encyclopedia:Healing}Cure{/g} 2 consecutive Fort {g|Encyclopedia:Saving_Throw}saves{/g})\nBaleful " +
                    "Polymorph (Fortitude negates)\nInsane, as insanity spell (Will negates)\nSent to another plane forever (Will negates)\nCreature struck by two rays receives both rays' effects at once.");
                bp.m_Icon = PrismaticSpraySpell.Icon;
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Evocation;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = PrismaticSpraySpell.GetComponent<AbilityEffectRunAction>().Actions;
                });
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = PrismaticSpraySpell.GetComponent<AbilityDeliverProjectile>().m_Projectiles;
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 60 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Other;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.AOE;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Bolstered | Metamagic.Selective;
                bp.LocalizedDuration = PrismaticSpraySpell.LocalizedDuration;
                bp.LocalizedSavingThrow = PrismaticSpraySpell.LocalizedSavingThrow;
            });
            var ShamanHeavensScintillatingPatternAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamanHeavensScintillatingPatternAbility", bp => {
                bp.SetName("Phantasmagoric Display - Scintillating Pattern");
                bp.SetDescription("A twisting pattern of coruscating colors weaves through the air, affecting creatures within. The {g|Encyclopedia:Spell}spell{/g} affects a total number of HD of creatures " +
                    "equal to your {g|Encyclopedia:Caster_Level}caster level{/g} (maximum 20). Creatures with the fewest HD are affected first, and among creatures with equal HD, those who are closest to the " +
                    "spell's point of origin are affected first. HD that are not sufficient to affect a creature are wasted. The spell affects each subject according to its HD.\n6 or less: " +
                    "{g|Encyclopedia:Injury_Death}Unconscious{/g} for {g|Encyclopedia:Dice}1d4{/g} {g|Encyclopedia:Combat_Round}rounds{/g}, then stunned for the same amount of rounds, and then confused for the " +
                    "same amount of rounds. (Treat an unconscious result as stunned for nonliving creatures.)\n7 to 12: Stunned for 1d4 rounds, then confused for the same amount of rounds.\n13 or more: Confused " +
                    "for 1d4 rounds.\nSightless creatures are not affected by scintillating pattern.");        
                bp.m_Icon = ScintillatingPatternSpell.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = ScintillatingPatternSpell.GetComponent<AbilityEffectRunAction>().Actions;
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "63f322580ec0e7c4c96fc62ecabad40f" };
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 20.Feet();
                    c.m_TargetType = TargetType.Any;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker() {
                        Conditions = new Condition[0]
                    };
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Other;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.AOE;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.Harmful;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Reach | Metamagic.Selective;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });



            var ShamanHeavensSpiritTrueFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHeavensSpiritTrueFeature", bp => {
                bp.SetName("Phantasmagoric Display");
                bp.SetDescription("The shaman can cast prismatic spray and scintillating pattern, each once per day with a caster level equal to her shaman level.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ShamanHeavensPrismaticSprayAbility.ToReference<BlueprintUnitFactReference>(),
                        ShamanHeavensScintillatingPatternAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ShamanHeavensPrismaticSprayResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ShamanHeavensScintillatingPatternResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] {
                        ShamanHeavensPrismaticSprayAbility.ToReference<BlueprintAbilityReference>(),
                        ShamanHeavensScintillatingPatternAbility.ToReference<BlueprintAbilityReference>() 
                    };
                    c.Stat = StatType.Wisdom;
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
