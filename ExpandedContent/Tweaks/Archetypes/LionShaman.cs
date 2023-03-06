using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.Utility;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.ResourceLinks;
using Kingmaker.Visual.Sound;
using Kingmaker.AI.Blueprints;
using Kingmaker.Visual.HitSystem;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Designers.TempMapCode.Ambush;
using Kingmaker.Craft;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class LionShaman {
        public static void AddLionShaman() {

            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var DruidBondSelection = Resources.GetBlueprintReference<BlueprintFeatureReference>("3830f3630a33eba49b60f511b4c8f2a8");
            var DruidSpontaneousSummonFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("b296531ffe013c8499ad712f8ae97f6b");
            var VenomImmunityFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("5078622eb5cecaf4683fa16a9b948c2c");
            var ResistNaturesLureFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("ad6a5b0e1a65c3540986cf9a7b006388");

            var WildShapeIWolfFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("19bb148cb92db224abb431642d10efeb");
            var WildShapeIILeopardFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("c4d651bc0d4eabd41b08ee81bfe701d8");
            var WildShapeElementalSmallFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("bddd46a6f6a3e6e4b99008dcf5271c3b");
            var WildShapeIVBearFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("1368c7ce69702444893af5ffd3226e19");
            var WildShapeElementalFeatureAddMediumFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("6e4b88e2a044c67469c038ac2f09d061");
            var WildShapeIIISmilodonFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("253c0c0d00e50a24797445f20af52dc8");
            var WildShapeElementalFeatureAddLargeFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("e66154511a6f9fc49a9de644bd8922db");
            var WildShapeIVShamblingMoundFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("0f31b23c2ab39354bbde4e33e8151495");
            var WildShapeElementalHugeFeatureFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("fe58dd496a36e274b86958f4677071b2");

            var AnimalDomainProgressionDruid = Resources.GetBlueprint<BlueprintProgression>("a75ad4936e099c54881cf553e2110703");
            var GloryDomainProgressionDruid = Resources.GetModBlueprint<BlueprintProgression>("GloryDomainProgressionDruid");
            var NobilityDomainProgressionDruid = Resources.GetModBlueprint<BlueprintProgression>("NobilityDomainProgressionDruid");
            var SunDomainProgressionDruid = Resources.GetModBlueprint<BlueprintProgression>("SunDomainProgressionDruid");

            var LionShamanArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("LionShamanArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"LionShamanArchetype.Name", "Lion Totem Druid");
                bp.LocalizedDescription = Helpers.CreateString($"LionShamanArchetype.Description", "A druid with this totem calls upon the proud lion, imposing and majestic, the mighty leader of deadly hunters.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"LionShamanArchetype.Description", "A druid with this totem calls upon the proud lion, imposing and majestic, the mighty leader of deadly hunters.");
            });
            var LionShamanWildShape = Helpers.CreateBlueprint<BlueprintFeature>("LionShamanWildShape", bp => {
                bp.SetName("Wild Shape - Lion Totem Druid");
                bp.SetDescription("A lion totem druid wild shape is effected by their dedication to feline sprits. All wild shape forms gained from level 6 onwards are gained 2 levels later than a standard druid, however " +
                    "feline forms are gained 2 levels earlier.");
                bp.IsClassFeature = true;
            });

            var AnimalCompanionFeatureLeopard = Resources.GetBlueprintReference<BlueprintFeatureReference>("2ee2ba60850dd064e8b98bf5c2c946ba");
            var AnimalCompanionFeatureSmilodon = Resources.GetBlueprintReference<BlueprintFeatureReference>("126712ef923ab204983d6f107629c895");
            var LionShamanDomainSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("LionShamanDomainSelection", bp => {
                bp.SetName("Lion Totem Druids Bond");
                bp.SetDescription("A lion totem druid who chooses an animal companion must select a feline. If choosing a domain, the lion totem druid must choose from the Animal, Glory, Nobility, and Sun domains.");
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.OnlyNew;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    AnimalDomainProgressionDruid.ToReference<BlueprintFeatureReference>(),
                    GloryDomainProgressionDruid.ToReference<BlueprintFeatureReference>(),
                    NobilityDomainProgressionDruid.ToReference<BlueprintFeatureReference>(),
                    SunDomainProgressionDruid.ToReference<BlueprintFeatureReference>()
                };
            });
            var LionShamanBondSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("LionShamanBondSelection", bp => {
                bp.SetName("Lion Totem Druids Bond");
                bp.SetDescription("A lion totem druid who chooses an animal companion must select a feline. If choosing a domain, the lion totem druid must choose from the Animal, Glory, Nobility, and Sun domains.");
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.OnlyNew;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddFeatures(AnimalCompanionFeatureLeopard, AnimalCompanionFeatureSmilodon, LionShamanDomainSelection);
            });

            var DodgeFeat = Resources.GetBlueprintReference<BlueprintFeatureReference>("97e216dbb46ae3c4faef90cf6bbe6fd5");
            var LungeFeat = Resources.GetBlueprintReference<BlueprintFeatureReference>("d41d5bd9a775d7245929256d58a3e03e");
            var IronWillFeat = Resources.GetBlueprintReference<BlueprintFeatureReference>("175d1577bb6c9a04baf88eec99c66334");
            var ImprovedIronWillFeat = Resources.GetBlueprintReference<BlueprintFeatureReference>("3ea2215150a1c8a4a9bfed9d9023903e");
            var SkillFocusAcrobaticsFeat = Resources.GetBlueprintReference<BlueprintFeatureReference>("52dd89af385466c499338b7297896ded");
            var LionShamanBonusFeatSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("LionShamanBonusFeatSelection", bp => {
                bp.SetName("Lion Totem Druid Bonus Feat");
                bp.SetDescription("At 9th level and every 4 levels thereafter, a lion totem druid gains one of the following bonus feats: Dodge, Lunge, Improved Iron Will, Iron Will, or Skill Focus (Acrobatics). " +
                    "She must meet the prerequisites for these bonus feats.");
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.OnlyNew;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddFeatures(DodgeFeat, LungeFeat, IronWillFeat, ImprovedIronWillFeat, SkillFocusAcrobaticsFeat);
            });

            var LionShamanTotemTransformationResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("LionShamanTotemTransformationResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = true,
                    m_Class = new BlueprintCharacterClassReference[] {                        
                        DruidClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[] {
                        LionShamanArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    LevelIncrease = 1,
                    StartingLevel = 2,
                    StartingIncrease = 1,
                };
            });

            var Bite1d4 = Resources.GetBlueprint<BlueprintItemWeapon>("35dfad6517f401145af54111be04d6cf");
            var BloodlineAbyssalClaw1d4 = Resources.GetBlueprint<BlueprintItemWeapon>("289c13ba102d0df43862a488dad8a5d5");
            var CheetahSprintIcon = AssetLoader.LoadInternal("Skills", "Icon_CheetahSprint.jpg");
            var SenseVitals = Resources.GetBlueprint<BlueprintAbility>("82962a820ebc0e7408b8582fdc3f4c0c");
            var BearsEndurance = Resources.GetBlueprint<BlueprintAbility>("a900628aea19aa74aad0ece0e65d091a");
            var DruidClawsIcon = AssetLoader.LoadInternal("Skills", "Icon_DruidClaws.jpg");

            var LionShamanTotemTransformationMovementBuff = Helpers.CreateBuff("LionShamanTotemTransformationMovementBuff", bp => {
                bp.SetName("Lion Totem Transformation - Movement");
                bp.SetDescription("A lion totem druid may select which aspect of the lion to embue themselves with.\nMovement (+20 enhancement bonus to speed, +4 racial bonus on Mobility checks)");
                bp.m_Icon = CheetahSprintIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.Stat = StatType.Speed;
                    c.Value = 10;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.SkillMobility;
                    c.Value = 4;
                });
            });
            var LionShamanTotemTransformationSensesBuff = Helpers.CreateBuff("LionShamanTotemTransformationSensesBuff", bp => {
                bp.SetName("Lion Totem Transformation - Senses");
                bp.SetDescription("A lion totem druid may select which aspect of the lion to embue themselves with.\nSenses (Blind Fight feat, +4 on racial bonus on perception checks)");
                bp.m_Icon = SenseVitals.Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<FlatFootedIgnore>(c => {
                    c.Type = FlatFootedIgnoreType.BlindFight;
                });
                bp.AddComponent<BlindnessACCompensation>();
                bp.AddComponent<RerollConcealment>();
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => { c.Descriptor = SpellDescriptor.GazeAttack; });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.SkillPerception;
                    c.Value = 4;
                });
            });            
            var LionShamanTotemTransformationNaturalWeaponsBuff = Helpers.CreateBuff("LionShamanTotemTransformationNaturalWeaponsBuff", bp => {
                bp.SetName("Lion Totem Transformation - Natural Weapons");
                bp.SetDescription("A lion totem druid may select which aspect of the lion to embue themselves with.\nNatural Weapons (bite {g|Encyclopedia:Attack}attack{/g} dealing " +
                    "{g|Encyclopedia:Dice}1d4{/g} {g|Encyclopedia:Damage}damage{/g} and two claws dealing {g|Encyclopedia:Dice}1d4{/g} {g|Encyclopedia:Damage}damage{/g} " +
                    "for a Medium druid, +2 to CMB)");
                bp.m_Icon = DruidClawsIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;                
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = Bite1d4.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddComponent<EmptyHandWeaponOverride>(c => {
                    c.m_Weapon = BloodlineAbyssalClaw1d4.ToReference<BlueprintItemWeaponReference>();
                    c.IsPermanent = false;
                    c.IsMonkUnarmedStrike = false;
                });
            });

            var LionShamanTotemTransformationPolymorphHook = Helpers.CreateBuff("LionShamanTotemTransformationPolymorphHook", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.Ranks = 1;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() { m_Buff = LionShamanTotemTransformationMovementBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = LionShamanTotemTransformationSensesBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = LionShamanTotemTransformationNaturalWeaponsBuff.ToReference<BlueprintBuffReference>() }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
            });
            var LionShamanTotemTransformationIcon = AssetLoader.LoadInternal("Skills", "Icon_LionShamanTotemTransformation.jpg");
            var LionShamanTotemTransformationAbilityStandard = Helpers.CreateBlueprint<BlueprintAbility>("LionShamanTotemTransformationAbilityStandard", bp => {
                bp.SetName("Lion Totem Transformation");
                bp.SetDescription("As a standard action a lion totem druid may adopt an aspect of the lion while retaining her normal form. They gain one of the following bonuses: \nMovement (+20 enhancement " +
                    "bonus to speed, +4 racial bonus on Mobility checks)\nSenses (Blind Fight feat, +4 on perception checks)\nNatural Weapons (bite {g|Encyclopedia:Attack}attack{/g} dealing {g|Encyclopedia:Dice}1d4{/g} " +
                    "{g|Encyclopedia:Damage}damage{/g} and two claws dealing {g|Encyclopedia:Dice}1d4{/g} {g|Encyclopedia:Damage}damage{/g} for a Medium druid, +2 to CMB)");
                bp.m_Icon = LionShamanTotemTransformationIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = LionShamanTotemTransformationPolymorphHook.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = LionShamanTotemTransformationResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("LionShamanTotemTransformationAbilityStandard.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var LionShamanTotemTransformationAbilityMove = Helpers.CreateBlueprint<BlueprintAbility>("LionShamanTotemTransformationAbilityMove", bp => {
                bp.SetName("Lion Totem Transformation - Move Action");
                bp.SetDescription("As a move action a lion totem druid may adopt an aspect of the lion while retaining her normal form. They gain one of the following bonuses: \nMovement (+20 enhancement " +
                    "bonus to speed, +4 racial bonus on Mobility checks)\nSenses (Blind Fight feat, +4 on perception checks)\nNatural Weapons (bite {g|Encyclopedia:Attack}attack{/g} dealing {g|Encyclopedia:Dice}1d4{/g} " +
                    "{g|Encyclopedia:Damage}damage{/g} and two claws dealing {g|Encyclopedia:Dice}1d4{/g} {g|Encyclopedia:Damage}damage{/g} for a Medium druid, +2 to CMB)");
                bp.m_Icon = LionShamanTotemTransformationIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = LionShamanTotemTransformationPolymorphHook.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = LionShamanTotemTransformationResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Move;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("LionShamanTotemTransformationAbilityMove.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var LionShamanTotemTransformationAbilitySwift = Helpers.CreateBlueprint<BlueprintAbility>("LionShamanTotemTransformationAbilitySwift", bp => {
                bp.SetName("Lion Totem Transformation - Swift Action");
                bp.SetDescription("As a swift action a lion totem druid may adopt an aspect of the lion while retaining her normal form. They gain one of the following bonuses: \nMovement (+20 enhancement " +
                    "bonus to speed, +4 racial bonus on Mobility checks)\nSenses (Blind Fight feat, +4 on perception checks)\nNatural Weapons (bite {g|Encyclopedia:Attack}attack{/g} dealing {g|Encyclopedia:Dice}1d4{/g} " +
                    "{g|Encyclopedia:Damage}damage{/g} and two claws dealing {g|Encyclopedia:Dice}1d4{/g} {g|Encyclopedia:Damage}damage{/g} for a Medium druid, +2 to CMB)");
                bp.m_Icon = LionShamanTotemTransformationIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = LionShamanTotemTransformationPolymorphHook.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = LionShamanTotemTransformationResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("LionShamanTotemTransformationAbilitySwift.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var LionShamanTotemTransformationMovementToggleBuff = Helpers.CreateBuff("LionShamanTotemTransformationMovementToggleBuff", bp => {
                bp.SetName("Lion Totem Transformation - Movement");
                bp.SetDescription("");
                bp.Ranks = 1;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        LionShamanTotemTransformationAbilityStandard.ToReference<BlueprintAbilityReference>(),
                        LionShamanTotemTransformationAbilityMove.ToReference<BlueprintAbilityReference>(),
                        LionShamanTotemTransformationAbilitySwift.ToReference<BlueprintAbilityReference>()
                    };
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = LionShamanTotemTransformationMovementBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        }
                        );
                });
            });
            var LionShamanTotemTransformationSensesToggleBuff = Helpers.CreateBuff("LionShamanTotemTransformationSensesToggleBuff", bp => {
                bp.SetName("Lion Totem Transformation - Senses");
                bp.SetDescription("");
                bp.Ranks = 1;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        LionShamanTotemTransformationAbilityStandard.ToReference<BlueprintAbilityReference>(),
                        LionShamanTotemTransformationAbilityMove.ToReference<BlueprintAbilityReference>(),
                        LionShamanTotemTransformationAbilitySwift.ToReference<BlueprintAbilityReference>()
                    };
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = LionShamanTotemTransformationSensesBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        }
                        );
                });
            });            
            var LionShamanTotemTransformationNaturalWeaponsToggleBuff = Helpers.CreateBuff("LionShamanTotemTransformationNaturalWeaponsToggleBuff", bp => {
                bp.SetName("Lion Totem Transformation - Natural Weapons");
                bp.SetDescription("");
                bp.Ranks = 1;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        LionShamanTotemTransformationAbilityStandard.ToReference<BlueprintAbilityReference>(),
                        LionShamanTotemTransformationAbilityMove.ToReference<BlueprintAbilityReference>(),
                        LionShamanTotemTransformationAbilitySwift.ToReference<BlueprintAbilityReference>()
                    };
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = LionShamanTotemTransformationNaturalWeaponsBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        }
                        );
                });
            });

            var LionShamanTotemTransformationMovementToggleAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("LionShamanTotemTransformationMovementToggleAbility", bp => {
                bp.SetName("Lion Totem Transformation - Movement");
                bp.SetDescription("A lion totem druid may select which aspect of the lion to embue themselves with.\nMovement (+20 enhancement bonus to speed, +4 racial bonus on Mobility checks)");
                bp.m_Icon = CheetahSprintIcon;
                bp.m_Buff = LionShamanTotemTransformationMovementToggleBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.Group = ActivatableAbilityGroup.DivineWeaponProperty;
                bp.WeightInGroup = 1;
            });
            var LionShamanTotemTransformationSensesToggleAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("LionShamanTotemTransformationSensesToggleAbility", bp => {
                bp.SetName("Lion Totem Transformation - Senses");
                bp.SetDescription("A lion totem druid may select which aspect of the lion to embue themselves with.\nSenses (Blind Fight feat, +4 on perception checks)");
                bp.m_Icon = SenseVitals.Icon;
                bp.m_Buff = LionShamanTotemTransformationSensesToggleBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.Group = ActivatableAbilityGroup.DivineWeaponProperty;
                bp.WeightInGroup = 1;
            });
            var LionShamanTotemTransformationNaturalWeaponsToggleAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("LionShamanTotemTransformationNaturalWeaponsToggleAbility", bp => {
                bp.SetName("Lion Totem Transformation - Natural Weapons");
                bp.SetDescription("A lion totem druid may select which aspect of the lion to embue themselves with.\nNatural Weapons (bite {g|Encyclopedia:Attack}attack{/g} dealing " +
                    "{g|Encyclopedia:Dice}1d4{/g} {g|Encyclopedia:Damage}damage{/g} and two claws dealing {g|Encyclopedia:Dice}1d4{/g} {g|Encyclopedia:Damage}damage{/g} " +
                    "for a Medium druid, +2 to CMB)");
                bp.m_Icon = DruidClawsIcon;
                bp.m_Buff = LionShamanTotemTransformationNaturalWeaponsToggleBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.Group = ActivatableAbilityGroup.DivineWeaponProperty;
                bp.WeightInGroup = 1;
            });

            var LionShamanTotemTransformationFeature = Helpers.CreateBlueprint<BlueprintFeature>("LionShamanTotemTransformationFeature", bp => {
                bp.SetName("Lion Totem Transformation");
                bp.SetDescription("At 2nd level, a lion totem druid may adopt an aspect of the lion while retaining their normal form. They gain one of the following bonuses: \nMovement (+20 enhancement " +
                    "bonus to speed, +4 racial bonus on Mobility checks)\nSenses (Blind Fight feat, +4 on perception checks)\nNatural Weapons (bite {g|Encyclopedia:Attack}attack{/g} dealing {g|Encyclopedia:Dice}1d4{/g} " +
                    "{g|Encyclopedia:Damage}damage{/g} and two claws dealing {g|Encyclopedia:Dice}1d4{/g} {g|Encyclopedia:Damage}damage{/g} for a Medium druid, +2 to CMB)" +
                    "\nUsing this ability is a standard action at 2nd level, " +
                    "a move action at 7th level, and a swift action at 12th level. This ability lasts one minute and can be used a number of times equal to their druid level. This is a polymorph effect and cannot be used while " +
                    "the druid is using another polymorph effect, such as wild shape.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        LionShamanTotemTransformationAbilityStandard.ToReference<BlueprintUnitFactReference>(),
                        LionShamanTotemTransformationMovementToggleAbility.ToReference<BlueprintUnitFactReference>(),
                        LionShamanTotemTransformationSensesToggleAbility.ToReference<BlueprintUnitFactReference>(),
                        LionShamanTotemTransformationNaturalWeaponsToggleAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = LionShamanTotemTransformationResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
            });
            var LionShamanTotemTransformationMoveFeature = Helpers.CreateBlueprint<BlueprintFeature>("LionShamanTotemTransformationMoveFeature", bp => {
                bp.SetName("Lion Totem Transformation (Move Action)");
                bp.SetDescription("At 2nd level, a lion totem druid may adopt an aspect of the lion while retaining their normal form. They gain one of the following bonuses: \nMovement (+20 enhancement " +
                    "bonus to speed, +4 racial bonus on Mobility checks)\nSenses (Blind Fight feat, +4 on perception checks)\nNatural Weapons (bite {g|Encyclopedia:Attack}attack{/g} dealing {g|Encyclopedia:Dice}1d4{/g} " +
                    "{g|Encyclopedia:Damage}damage{/g} and two claws dealing {g|Encyclopedia:Dice}1d4{/g} {g|Encyclopedia:Damage}damage{/g} for a Medium druid, +2 to CMB)" +
                    "\nUsing this ability is a standard action at 2nd level, " +
                    "a move action at 7th level, and a swift action at 12th level. This ability lasts one minute and can be used a number of times equal to their druid level. This is a polymorph effect and cannot be used while " +
                    "the druid is using another polymorph effect, such as wild shape.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        LionShamanTotemTransformationAbilityMove.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
            });
            var LionShamanTotemTransformationSwiftFeature = Helpers.CreateBlueprint<BlueprintFeature>("LionShamanTotemTransformationSwiftFeature", bp => {
                bp.SetName("Lion Totem Transformation (Swift Action)");
                bp.SetDescription("At 2nd level, a lion totem druid may adopt an aspect of the lion while retaining their normal form. They gain one of the following bonuses: \nMovement (+20 enhancement " +
                    "bonus to speed, +4 racial bonus on Mobility checks)\nSenses (Blind Fight feat, +4 on perception checks)\nNatural Weapons (bite {g|Encyclopedia:Attack}attack{/g} dealing {g|Encyclopedia:Dice}1d4{/g} " +
                    "{g|Encyclopedia:Damage}damage{/g} and two claws dealing {g|Encyclopedia:Dice}1d4{/g} {g|Encyclopedia:Damage}damage{/g} for a Medium druid, +2 to CMB)" +
                    "\nUsing this ability is a standard action at 2nd level, " +
                    "a move action at 7th level, and a swift action at 12th level. This ability lasts one minute and can be used a number of times equal to their druid level. This is a polymorph effect and cannot be used while " +
                    "the druid is using another polymorph effect, such as wild shape.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        LionShamanTotemTransformationAbilitySwift.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
            });

            var LeopardSummon = Resources.GetBlueprint<BlueprintUnit>("768275c9885dd954fb3c84ba69ac4281");
            var SmilodonSummon = Resources.GetBlueprint<BlueprintUnit>("beae4985629a6f64eb98081e3171e4c1");
            var SummonMonsterPool = Resources.GetBlueprint<BlueprintSummonPool>("d94c93e7240f10e41ae41db4c83d1cbe");
            var SummonedCreatureSpawnAllyI_III = Resources.GetBlueprint<BlueprintBuff>("2c3b98e56c1b4e9479f692d7ef95b2d2");
            var SummonedCreatureSpawnAllyIV_VI = Resources.GetBlueprint<BlueprintBuff>("25629d7e78016a340b0e50818b6d8bb5");
            var SummonedCreatureSpawnAllyVII_IX = Resources.GetBlueprint<BlueprintBuff>("932d27490e1701548a48b4cbc2f2caac");
            var AnimalClass = Resources.GetBlueprint<BlueprintCharacterClass>("4cd1757a0eea7694ba5c933729a53920");
            var HeadLocatorFeature = Resources.GetBlueprint<BlueprintFeature>("9c57e9674b4a4a2b9920f9fec47f7e6a");
            var DumbMosterBrain = Resources.GetBlueprint<BlueprintBrain>("5abc8884c6f15204c8604cb01a2efbab");
            var ChargeMosterBrain = Resources.GetBlueprint<BlueprintBrain>("d9a2e59e78e28c643b2d3a1cddf212a7");
            var Summoned = Resources.GetBlueprint<BlueprintFaction>("1b08d9ed04518ec46a9b3e4e23cb5105");
            var BearBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("90ac36b1eee5d7c429e4aea89dd3f1dc");
            var LeopardPortait = Resources.GetBlueprint<BlueprintPortrait>("70c86316f0cd4ea4f98987079a4a5e60"); //Leopard portrait
            var Unlootable = Resources.GetBlueprintReference<BlueprintBuffReference>("0f775c7d5d8b6494197e1ce937754482");
            var NatualAllyCreatureVisual = Resources.GetBlueprintReference<BlueprintBuffReference>("e4b996b5168fe284ab3141a91895d7ea");
            var Pounce = Resources.GetBlueprint<BlueprintFeature>("1a8149c09e0bdfc48a305ee6ac3729a8");
            var SubtypeExtraplanar = Resources.GetBlueprint<BlueprintFeature>("13c87ac5985cc85498ef9d1ac8b78923");
            var TripDefenceFourLegs = Resources.GetBlueprint<BlueprintFeature>("136fa0343d5b4b348bdaa05d83408db3");
            var SuperiorSummoning = Resources.GetBlueprint<BlueprintFeature>("0477936c0f74841498b5c8753a8062a3");
            var BloodlineAbyssalSummoning = Resources.GetBlueprint<BlueprintFeature>("de24d9e57d7bad24dbada7389eebcd65");

            var BasicFeatSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("247a4068296e8be42890143f451b4b45");
            var WeaponFinesseFeat = Resources.GetBlueprintReference<BlueprintFeatureReference>("90e54424d682d104ab36436bd527af09");
            var ImprovedInitiativeFeat = Resources.GetBlueprintReference<BlueprintFeatureReference>("797f25d709f559546b29e7bcb181cc74");
            var SkillFocusStealthFeat = Resources.GetBlueprintReference<BlueprintFeatureReference>("3a8d34905eae4a74892aae37df3352b9");
            var SkillFocusPerceptionFeat = Resources.GetBlueprintReference<BlueprintFeatureReference>("f74c6bdf5c5f5374fb9302ecdc1f7d64");
            var SkillFocusSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("c9629ef9eebb88b479b2fbc5e836656a");
            var WeaponFocusParaFeat = Resources.GetBlueprint<BlueprintParametrizedFeature>("1e1f627d26ad36f43bbd26cc2bf8ac7e");
            var ImprovedCritParaFeat = Resources.GetBlueprint<BlueprintParametrizedFeature>("f4201c85a991369408740c6888362e20");
            var LeopardType = Resources.GetBlueprint<BlueprintUnitType>("fd68af84ca0e7b240ad55175e8d4b58c");
            var SmilodonType = Resources.GetBlueprint<BlueprintUnitType>("9b09a3b97fa0ee941954732b829d610c");

            var ReducedReach = Resources.GetBlueprint<BlueprintUnitFact>("c33f2d68d93ceee488aa4004347dffca");
            var NaturalArmor3 = Resources.GetBlueprint<BlueprintUnitFact>("f6e106931f95fec4eb995f0d0629fb84");
            var NaturalArmor6 = Resources.GetBlueprint<BlueprintUnitFact>("987ba44303e88054c9504cb3083ba0c9");
            var NaturalArmor4 = Resources.GetBlueprint<BlueprintUnitFact>("16fc201a83edcde4cbd64c291ebe0d07");
            var NaturalArmor9 = Resources.GetBlueprint<BlueprintUnitFact>("da6417809bdedfa468dd2fd0cc74be92");
            var NaturalArmor8 = Resources.GetBlueprint<BlueprintUnitFact>("b9342e2a6dc5165489ba3412c50ca3d1");
            var NaturalArmor11 = Resources.GetBlueprint<BlueprintUnitFact>("fe38367139432294e8c229edc066e4ac");

            var SizeIncreasePlus1 = Resources.GetModBlueprint<BlueprintFeature>("SizeIncreasePlus1");

            var TotemCreature = Resources.GetModBlueprint<BlueprintFeature>("TotemCreature");

            // Summons
            var SummonLeopard2Icon = AssetLoader.LoadInternal("Skills", "Icon_SummonLeopard2.jpg");
            var SummonLeopard3Icon = AssetLoader.LoadInternal("Skills", "Icon_SummonLeopard3.jpg"); //???
            var SummonLeopard4Icon = AssetLoader.LoadInternal("Skills", "Icon_SummonLeopard4.jpg");
            var SummonLeopard5Icon = AssetLoader.LoadInternal("Skills", "Icon_SummonLeopard5.jpg");
            var SummonLeopard6Icon = AssetLoader.LoadInternal("Skills", "Icon_SummonLeopard6.jpg");
            var SummonLeopard7Icon = AssetLoader.LoadInternal("Skills", "Icon_SummonLeopard7.jpg");

            var SummonSmilodon5Icon = AssetLoader.LoadInternal("Skills", "Icon_SummonSmilodon5.jpg");
            var SummonSmilodon6Icon = AssetLoader.LoadInternal("Skills", "Icon_SummonSmilodon6.jpg"); //???
            var SummonSmilodon7Icon = AssetLoader.LoadInternal("Skills", "Icon_SummonSmilodon7.jpg");
            var SummonSmilodon8Icon = AssetLoader.LoadInternal("Skills", "Icon_SummonSmilodon8.jpg");
            var SummonSmilodon9Icon = AssetLoader.LoadInternal("Skills", "Icon_SummonSmilodon9.jpg");

            var LeopardYoungSummoned = Helpers.CreateBlueprint<BlueprintUnit>("LeopardYoungSummoned", bp => {
                bp.SetLocalisedName("Summoned Young Leopard");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                    c.Levels = 3;
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Unknown;
                    c.Skills = new StatType[] { StatType.SkillPerception | StatType.SkillStealth | StatType.SkillAthletics | StatType.SkillMobility };
                    c.Selections = new SelectionEntry[] {
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                WeaponFinesseFeat,
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = null,
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.UnarmedStrike,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = SkillFocusSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusStealthFeat
                            },
                            m_ParametrizedFeature = null,
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.UnarmedStrike,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        }
                    };
                    c.DoNotApplyAutomatically = false;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        HeadLocatorFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = Unlootable;
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = NatualAllyCreatureVisual;
                });
                bp.m_Type = LeopardType.ToReference<BlueprintUnitTypeReference>();
                bp.Gender = Gender.Male;
                bp.Size = Size.Small;
                bp.Color = LeopardSummon.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = LeopardPortait.ToReference<BlueprintPortraitReference>();
                bp.Prefab = new UnitViewLink { AssetId = "14b0730dd2c0a684f89f2982bf1035cd" }; //Petleopard prefab
                bp.Visual = LeopardSummon.Visual;
                bp.m_Faction = Summoned.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = LeopardSummon.FactionOverrides;
                bp.m_Brain = ChargeMosterBrain.ToReference<BlueprintBrainReference>();
                bp.Body = LeopardSummon.Body;
                bp.Strength = 12;
                bp.Dexterity = 23;
                bp.Constitution = 11;
                bp.Wisdom = 13;
                bp.Intelligence = 2;
                bp.Charisma = 6;
                bp.Speed = new Feet(30);
                bp.Skills = new BlueprintUnit.UnitSkills() {
                    Acrobatics = 0,
                    Physique = 0,
                    Diplomacy = 0,
                    Thievery = 0,
                    LoreNature = 0,
                    Perception = 0,
                    Stealth = 0,
                    UseMagicDevice = 0,
                    LoreReligion = 0,
                    KnowledgeWorld = 0,
                    KnowledgeArcana = 0,
                };
                bp.MaxHP = 0;
                bp.m_AddFacts = new BlueprintUnitFactReference[] {
                    Pounce.ToReference<BlueprintUnitFactReference>(),
                    TripDefenceFourLegs.ToReference<BlueprintUnitFactReference>(),
                    SubtypeExtraplanar.ToReference<BlueprintUnitFactReference>(),
                    TotemCreature.ToReference<BlueprintUnitFactReference>()
                };
            });
            var LeopardAdvancedSummoned = Helpers.CreateBlueprint<BlueprintUnit>("LeopardAdvancedSummoned", bp => {
                bp.SetLocalisedName("Summoned Advanced Leopard");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                    c.Levels = 3;
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Unknown;
                    c.Skills = new StatType[] { StatType.SkillPerception | StatType.SkillStealth | StatType.SkillAthletics | StatType.SkillMobility };
                    c.Selections = new SelectionEntry[] {
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                WeaponFinesseFeat,
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = null,
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.UnarmedStrike,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = SkillFocusSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusStealthFeat
                            },
                            m_ParametrizedFeature = null,
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.UnarmedStrike,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        }
                    };
                    c.DoNotApplyAutomatically = false;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        HeadLocatorFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = Unlootable;
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = NatualAllyCreatureVisual;
                });
                bp.m_Type = LeopardType.ToReference<BlueprintUnitTypeReference>();
                bp.Gender = Gender.Male;
                bp.Size = Size.Medium;
                bp.Color = LeopardSummon.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = LeopardPortait.ToReference<BlueprintPortraitReference>();
                bp.Prefab = new UnitViewLink { AssetId = "a9334574d1c444f45a14c51fbc74c4f8" }; //Summonedleopard prefab
                bp.Visual = LeopardSummon.Visual;
                bp.m_Faction = Summoned.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = LeopardSummon.FactionOverrides;
                bp.m_Brain = ChargeMosterBrain.ToReference<BlueprintBrainReference>();
                bp.Body = LeopardSummon.Body;
                bp.Strength = 20;
                bp.Dexterity = 23;
                bp.Constitution = 19;
                bp.Wisdom = 17;
                bp.Intelligence = 2;
                bp.Charisma = 10;
                bp.Speed = new Feet(30);
                bp.Skills = new BlueprintUnit.UnitSkills() {
                    Acrobatics = 0,
                    Physique = 0,
                    Diplomacy = 0,
                    Thievery = 0,
                    LoreNature = 0,
                    Perception = 0,
                    Stealth = 0,
                    UseMagicDevice = 0,
                    LoreReligion = 0,
                    KnowledgeWorld = 0,
                    KnowledgeArcana = 0,
                };
                bp.MaxHP = 0;
                bp.m_AddFacts = new BlueprintUnitFactReference[] {
                    Pounce.ToReference<BlueprintUnitFactReference>(),
                    NaturalArmor3.ToReference<BlueprintUnitFactReference>(),
                    TripDefenceFourLegs.ToReference<BlueprintUnitFactReference>(),
                    SubtypeExtraplanar.ToReference<BlueprintUnitFactReference>(),
                    TotemCreature.ToReference<BlueprintUnitFactReference>()
                };
            });
            var LeopardGiantSummoned = Helpers.CreateBlueprint<BlueprintUnit>("LeopardGiantSummoned", bp => {
                bp.SetLocalisedName("Summoned Giant Leopard");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                    c.Levels = 3;
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Unknown;
                    c.Skills = new StatType[] { StatType.SkillPerception | StatType.SkillStealth | StatType.SkillAthletics | StatType.SkillMobility };
                    c.Selections = new SelectionEntry[] {
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                WeaponFinesseFeat,
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = null,
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.UnarmedStrike,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = SkillFocusSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusStealthFeat
                            },
                            m_ParametrizedFeature = null,
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.UnarmedStrike,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        }
                    };
                    c.DoNotApplyAutomatically = false;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        HeadLocatorFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = Unlootable;
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = NatualAllyCreatureVisual;
                });
                bp.m_Type = LeopardType.ToReference<BlueprintUnitTypeReference>();
                bp.Gender = Gender.Male;
                bp.Size = Size.Medium;
                bp.Color = LeopardSummon.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = LeopardPortait.ToReference<BlueprintPortraitReference>();
                bp.Prefab = new UnitViewLink { AssetId = "a9334574d1c444f45a14c51fbc74c4f8" }; //Summonedleopard prefab
                bp.Visual = LeopardSummon.Visual;
                bp.m_Faction = Summoned.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = LeopardSummon.FactionOverrides;
                bp.m_Brain = ChargeMosterBrain.ToReference<BlueprintBrainReference>();
                bp.Body = LeopardSummon.Body;
                bp.Strength = 20;
                bp.Dexterity = 17;
                bp.Constitution = 19;
                bp.Wisdom = 13;
                bp.Intelligence = 2;
                bp.Charisma = 6;
                bp.Speed = new Feet(30);
                bp.Skills = new BlueprintUnit.UnitSkills() {
                    Acrobatics = 0,
                    Physique = 0,
                    Diplomacy = 0,
                    Thievery = 0,
                    LoreNature = 0,
                    Perception = 0,
                    Stealth = 0,
                    UseMagicDevice = 0,
                    LoreReligion = 0,
                    KnowledgeWorld = 0,
                    KnowledgeArcana = 0,
                };
                bp.MaxHP = 0;
                bp.m_AddFacts = new BlueprintUnitFactReference[] {
                    Pounce.ToReference<BlueprintUnitFactReference>(),
                    NaturalArmor4.ToReference<BlueprintUnitFactReference>(),
                    TripDefenceFourLegs.ToReference<BlueprintUnitFactReference>(),
                    ReducedReach.ToReference<BlueprintUnitFactReference>(),
                    SubtypeExtraplanar.ToReference<BlueprintUnitFactReference>(),
                    SizeIncreasePlus1.ToReference<BlueprintUnitFactReference>(),
                    TotemCreature.ToReference<BlueprintUnitFactReference>()
                };
            });
            var LeopardGiantAdvancedSummoned = Helpers.CreateBlueprint<BlueprintUnit>("LeopardGiantAdvancedSummoned", bp => {
                bp.SetLocalisedName("Summoned Giant Advanced Leopard");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                    c.Levels = 3;
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Unknown;
                    c.Skills = new StatType[] { StatType.SkillPerception | StatType.SkillStealth | StatType.SkillAthletics | StatType.SkillMobility };
                    c.Selections = new SelectionEntry[] {
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                WeaponFinesseFeat,
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = null,
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.UnarmedStrike,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = SkillFocusSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusStealthFeat
                            },
                            m_ParametrizedFeature = null,
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.UnarmedStrike,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        }
                    };
                    c.DoNotApplyAutomatically = false;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        HeadLocatorFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = Unlootable;
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = NatualAllyCreatureVisual;
                });
                bp.m_Type = LeopardType.ToReference<BlueprintUnitTypeReference>();
                bp.Gender = Gender.Male;
                bp.Size = Size.Medium;
                bp.Color = LeopardSummon.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = LeopardPortait.ToReference<BlueprintPortraitReference>();
                bp.Prefab = new UnitViewLink { AssetId = "a9334574d1c444f45a14c51fbc74c4f8" }; //Summonedleopard prefab
                bp.Visual = LeopardSummon.Visual;
                bp.m_Faction = Summoned.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = LeopardSummon.FactionOverrides;
                bp.m_Brain = ChargeMosterBrain.ToReference<BlueprintBrainReference>();
                bp.Body = LeopardSummon.Body;
                bp.Strength = 24;
                bp.Dexterity = 21;
                bp.Constitution = 23;
                bp.Wisdom = 17;
                bp.Intelligence = 2;
                bp.Charisma = 10;
                bp.Speed = new Feet(30);
                bp.Skills = new BlueprintUnit.UnitSkills() {
                    Acrobatics = 0,
                    Physique = 0,
                    Diplomacy = 0,
                    Thievery = 0,
                    LoreNature = 0,
                    Perception = 0,
                    Stealth = 0,
                    UseMagicDevice = 0,
                    LoreReligion = 0,
                    KnowledgeWorld = 0,
                    KnowledgeArcana = 0,
                };
                bp.MaxHP = 0;
                bp.m_AddFacts = new BlueprintUnitFactReference[] {
                    Pounce.ToReference<BlueprintUnitFactReference>(),
                    NaturalArmor4.ToReference<BlueprintUnitFactReference>(),
                    TripDefenceFourLegs.ToReference<BlueprintUnitFactReference>(),
                    ReducedReach.ToReference<BlueprintUnitFactReference>(),
                    SubtypeExtraplanar.ToReference<BlueprintUnitFactReference>(),
                    SizeIncreasePlus1.ToReference<BlueprintUnitFactReference>(),
                    TotemCreature.ToReference<BlueprintUnitFactReference>()
                };
            });

            var SmilodonYoungSummoned = Helpers.CreateBlueprint<BlueprintUnit>("SmilodonYoungSummoned", bp => {
                bp.SetLocalisedName("Summoned Young Smilodon");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                    c.Levels = 14;
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Unknown;
                    c.Skills = new StatType[] { StatType.SkillPerception | StatType.SkillStealth | StatType.SkillAthletics };
                    c.Selections = new SelectionEntry[] {
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = null,
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.UnarmedStrike,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = true,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = WeaponFocusParaFeat.ToReference<BlueprintParametrizedFeatureReference>(),
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.Claw,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = true,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = WeaponFocusParaFeat.ToReference<BlueprintParametrizedFeatureReference>(),
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.Bite,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = true,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = ImprovedCritParaFeat.ToReference<BlueprintParametrizedFeatureReference>(),
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.Claw,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = true,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = ImprovedCritParaFeat.ToReference<BlueprintParametrizedFeatureReference>(),
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.Bite,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = SkillFocusSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusStealthFeat,
                                SkillFocusPerceptionFeat
                            },
                            m_ParametrizedFeature = null,
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.UnarmedStrike,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        }
                    };
                    c.DoNotApplyAutomatically = false;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        HeadLocatorFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = Unlootable;
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = NatualAllyCreatureVisual;
                });
                bp.m_Type = SmilodonType.ToReference<BlueprintUnitTypeReference>();
                bp.Gender = Gender.Male;
                bp.Size = Size.Medium;
                bp.Color = SmilodonSummon.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = SmilodonSummon.ToReference<BlueprintPortraitReference>();
                bp.Prefab = new UnitViewLink { AssetId = "fab2f65ceb662cd4c972b76ec52fefde" }; //Petsmilodon prefab
                bp.Visual = SmilodonSummon.Visual;
                bp.m_Faction = Summoned.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = SmilodonSummon.FactionOverrides;
                bp.m_Brain = ChargeMosterBrain.ToReference<BlueprintBrainReference>();
                bp.Body = SmilodonSummon.Body;
                bp.Strength = 23;
                bp.Dexterity = 19;
                bp.Constitution = 13;
                bp.Wisdom = 12;
                bp.Intelligence = 2;
                bp.Charisma = 10;
                bp.Speed = new Feet(40);
                bp.Skills = new BlueprintUnit.UnitSkills() {
                    Acrobatics = 6,
                    Physique = 0,
                    Diplomacy = 0,
                    Thievery = 0,
                    LoreNature = 0,
                    Perception = 12,
                    Stealth = 15,
                    UseMagicDevice = 0,
                    LoreReligion = 0,
                    KnowledgeWorld = 0,
                    KnowledgeArcana = 0,
                };
                bp.MaxHP = 0;
                bp.m_AddFacts = new BlueprintUnitFactReference[] {
                    NaturalArmor4.ToReference<BlueprintUnitFactReference>(),
                    SubtypeExtraplanar.ToReference<BlueprintUnitFactReference>(),
                    TotemCreature.ToReference<BlueprintUnitFactReference>()
                };
            });
            var SmilodonAdvancedSummoned = Helpers.CreateBlueprint<BlueprintUnit>("SmilodonAdvancedSummoned", bp => {
                bp.SetLocalisedName("Summoned Advanced Smilodon");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                    c.Levels = 14;
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Unknown;
                    c.Skills = new StatType[] { StatType.SkillPerception | StatType.SkillStealth | StatType.SkillAthletics };
                    c.Selections = new SelectionEntry[] {
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = null,
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.UnarmedStrike,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = true,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = WeaponFocusParaFeat.ToReference<BlueprintParametrizedFeatureReference>(),
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.Claw,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = true,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = WeaponFocusParaFeat.ToReference<BlueprintParametrizedFeatureReference>(),
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.Bite,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = true,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = ImprovedCritParaFeat.ToReference<BlueprintParametrizedFeatureReference>(),
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.Claw,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = true,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = ImprovedCritParaFeat.ToReference<BlueprintParametrizedFeatureReference>(),
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.Bite,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = SkillFocusSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusStealthFeat,
                                SkillFocusPerceptionFeat
                            },
                            m_ParametrizedFeature = null,
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.UnarmedStrike,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        }
                    };
                    c.DoNotApplyAutomatically = false;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        HeadLocatorFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = Unlootable;
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = NatualAllyCreatureVisual;
                });
                bp.m_Type = SmilodonType.ToReference<BlueprintUnitTypeReference>();
                bp.Gender = Gender.Male;
                bp.Size = Size.Large;
                bp.Color = SmilodonSummon.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = SmilodonSummon.ToReference<BlueprintPortraitReference>();
                bp.Prefab = new UnitViewLink { AssetId = "1c760926d1ce40c41a52114ab7dd4c44" }; //summoned smilodon prefab
                bp.Visual = SmilodonSummon.Visual;
                bp.m_Faction = Summoned.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = SmilodonSummon.FactionOverrides;
                bp.m_Brain = ChargeMosterBrain.ToReference<BlueprintBrainReference>();
                bp.Body = SmilodonSummon.Body;
                bp.Strength = 31;
                bp.Dexterity = 19;
                bp.Constitution = 21;
                bp.Wisdom = 16;
                bp.Intelligence = 2;
                bp.Charisma = 14;
                bp.Speed = new Feet(40);
                bp.Skills = new BlueprintUnit.UnitSkills() {
                    Acrobatics = 6,
                    Physique = 0,
                    Diplomacy = 0,
                    Thievery = 0,
                    LoreNature = 0,
                    Perception = 12,
                    Stealth = 15,
                    UseMagicDevice = 0,
                    LoreReligion = 0,
                    KnowledgeWorld = 0,
                    KnowledgeArcana = 0,
                };
                bp.MaxHP = 0;
                bp.m_AddFacts = new BlueprintUnitFactReference[] {
                    NaturalArmor8.ToReference<BlueprintUnitFactReference>(),
                    ReducedReach.ToReference<BlueprintUnitFactReference>(),
                    SubtypeExtraplanar.ToReference<BlueprintUnitFactReference>(),
                    TotemCreature.ToReference<BlueprintUnitFactReference>()
                };
            });
            var SmilodonGiantSummoned = Helpers.CreateBlueprint<BlueprintUnit>("SmilodonGiantSummoned", bp => {
                bp.SetLocalisedName("Summoned Giant Smilodon");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                    c.Levels = 14;
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Unknown;
                    c.Skills = new StatType[] { StatType.SkillPerception | StatType.SkillStealth | StatType.SkillAthletics };
                    c.Selections = new SelectionEntry[] {
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = null,
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.UnarmedStrike,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = true,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = WeaponFocusParaFeat.ToReference<BlueprintParametrizedFeatureReference>(),
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.Claw,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = true,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = WeaponFocusParaFeat.ToReference<BlueprintParametrizedFeatureReference>(),
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.Bite,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = true,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = ImprovedCritParaFeat.ToReference<BlueprintParametrizedFeatureReference>(),
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.Claw,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = true,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = ImprovedCritParaFeat.ToReference<BlueprintParametrizedFeatureReference>(),
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.Bite,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = SkillFocusSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusStealthFeat,
                                SkillFocusPerceptionFeat
                            },
                            m_ParametrizedFeature = null,
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.UnarmedStrike,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        }
                    };
                    c.DoNotApplyAutomatically = false;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        HeadLocatorFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = Unlootable;
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = NatualAllyCreatureVisual;
                });
                bp.m_Type = SmilodonType.ToReference<BlueprintUnitTypeReference>();
                bp.Gender = Gender.Male;
                bp.Size = Size.Large;
                bp.Color = SmilodonSummon.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = SmilodonSummon.ToReference<BlueprintPortraitReference>();
                bp.Prefab = new UnitViewLink { AssetId = "1c760926d1ce40c41a52114ab7dd4c44" }; //summoned smilodon prefab
                bp.Visual = SmilodonSummon.Visual;
                bp.m_Faction = Summoned.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = SmilodonSummon.FactionOverrides;
                bp.m_Brain = ChargeMosterBrain.ToReference<BlueprintBrainReference>();
                bp.Body = SmilodonSummon.Body;
                bp.Strength = 31;
                bp.Dexterity = 13;
                bp.Constitution = 21;
                bp.Wisdom = 12;
                bp.Intelligence = 2;
                bp.Charisma = 10;
                bp.Speed = new Feet(40);
                bp.Skills = new BlueprintUnit.UnitSkills() {
                    Acrobatics = 6,
                    Physique = 0,
                    Diplomacy = 0,
                    Thievery = 0,
                    LoreNature = 0,
                    Perception = 12,
                    Stealth = 15,
                    UseMagicDevice = 0,
                    LoreReligion = 0,
                    KnowledgeWorld = 0,
                    KnowledgeArcana = 0,
                };
                bp.MaxHP = 0;
                bp.m_AddFacts = new BlueprintUnitFactReference[] {
                    NaturalArmor9.ToReference<BlueprintUnitFactReference>(),
                    ReducedReach.ToReference<BlueprintUnitFactReference>(),
                    SubtypeExtraplanar.ToReference<BlueprintUnitFactReference>(),
                    SizeIncreasePlus1.ToReference<BlueprintUnitFactReference>(),
                    TotemCreature.ToReference<BlueprintUnitFactReference>()
                };
            });
            var SmilodonGiantAdvancedSummoned = Helpers.CreateBlueprint<BlueprintUnit>("SmilodonGiantAdvancedSummoned", bp => {
                bp.SetLocalisedName("Summoned Giant Advanced Smilodon");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                    c.Levels = 14;
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Unknown;
                    c.Skills = new StatType[] { StatType.SkillPerception | StatType.SkillStealth | StatType.SkillAthletics };
                    c.Selections = new SelectionEntry[] {
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = null,
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.UnarmedStrike,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = true,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = WeaponFocusParaFeat.ToReference<BlueprintParametrizedFeatureReference>(),
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.Claw,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = true,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = WeaponFocusParaFeat.ToReference<BlueprintParametrizedFeatureReference>(),
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.Bite,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = true,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = ImprovedCritParaFeat.ToReference<BlueprintParametrizedFeatureReference>(),
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.Claw,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = true,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                SkillFocusSelection.ToReference<BlueprintFeatureReference>(),
                                ImprovedInitiativeFeat,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>(),
                                ImprovedCritParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = ImprovedCritParaFeat.ToReference<BlueprintParametrizedFeatureReference>(),
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.Bite,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = SkillFocusSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                SkillFocusStealthFeat,
                                SkillFocusPerceptionFeat
                            },
                            m_ParametrizedFeature = null,
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.UnarmedStrike,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        }
                    };
                    c.DoNotApplyAutomatically = false;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        HeadLocatorFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = Unlootable;
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = NatualAllyCreatureVisual;
                });
                bp.m_Type = SmilodonType.ToReference<BlueprintUnitTypeReference>();
                bp.Gender = Gender.Male;
                bp.Size = Size.Large;
                bp.Color = SmilodonSummon.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = SmilodonSummon.ToReference<BlueprintPortraitReference>();
                bp.Prefab = new UnitViewLink { AssetId = "1c760926d1ce40c41a52114ab7dd4c44" }; //summoned smilodon prefab
                bp.Visual = SmilodonSummon.Visual;
                bp.m_Faction = Summoned.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = SmilodonSummon.FactionOverrides;
                bp.m_Brain = ChargeMosterBrain.ToReference<BlueprintBrainReference>();
                bp.Body = SmilodonSummon.Body;
                bp.Strength = 35;
                bp.Dexterity = 17;
                bp.Constitution = 25;
                bp.Wisdom = 12;
                bp.Intelligence = 2;
                bp.Charisma = 10;
                bp.Speed = new Feet(40);
                bp.Skills = new BlueprintUnit.UnitSkills() {
                    Acrobatics = 6,
                    Physique = 0,
                    Diplomacy = 0,
                    Thievery = 0,
                    LoreNature = 0,
                    Perception = 12,
                    Stealth = 15,
                    UseMagicDevice = 0,
                    LoreReligion = 0,
                    KnowledgeWorld = 0,
                    KnowledgeArcana = 0,
                };
                bp.MaxHP = 0;
                bp.m_AddFacts = new BlueprintUnitFactReference[] {
                    NaturalArmor11.ToReference<BlueprintUnitFactReference>(),
                    ReducedReach.ToReference<BlueprintUnitFactReference>(),
                    SubtypeExtraplanar.ToReference<BlueprintUnitFactReference>(),
                    SizeIncreasePlus1.ToReference<BlueprintUnitFactReference>(),
                    TotemCreature.ToReference<BlueprintUnitFactReference>()
                };
            });

            var SummonLeopardStandardSingle = Helpers.CreateBlueprint<BlueprintAbility>("SummonLeopardStandardSingle", bp => {
                bp.SetName("Summon Leopard");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side a natural leopard. The summoned ally appears where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = LeopardSummon.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyI_III.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonLeopard3Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonLeopardStandardSingle.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonLeopardYoungSingle = Helpers.CreateBlueprint<BlueprintAbility>("SummonLeopardYoungSingle", bp => {
                bp.SetName("Summon Young Leopard");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side a natural young leopard. The summoned ally appears where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nYoung: Creature " +
                    "is one size category smaller, has natural armor decreased by 2, gains a -4 penalty to strength and constitution, and a +4 bonus to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = LeopardYoungSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyI_III.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonLeopard2Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonLeopardYoungSingle.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonLeopardGiantSingle = Helpers.CreateBlueprint<BlueprintAbility>("SummonLeopardGiantSingle", bp => {
                bp.SetName("Summon Giant Leopard");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side a natural giant leopard. The summoned ally appears where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nGiant: Creature " +
                    "is one size category larger, has natural armor increased by 3, gains a +4 bonus to strength and constitution, and a -2 penalty to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = LeopardGiantSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonLeopard4Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonLeopardGiantSingle.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonLeopardAdvancedSingle = Helpers.CreateBlueprint<BlueprintAbility>("SummonLeopardAdvancedSingle", bp => {
                bp.SetName("Summon Advanced Leopard");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side a natural advanced leopard. The summoned ally appears where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nAdvanced: Creature " +
                    "has natural armor increased by 2, gains a +4 bonus all ability scores (except Int scores of 2 or less).");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = LeopardAdvancedSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonLeopard4Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonLeopardAdvancedSingle.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonLeopardFinalSingle = Helpers.CreateBlueprint<BlueprintAbility>("SummonLeopardFinalSingle", bp => {
                bp.SetName("Summon Giant Advanced Leopard");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side a natural giant advanced leopard. The summoned ally appears where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nGiant + Advanced: Creature " +
                    "is one size category larger, has natural armor increased by 5, gains a +8 bonus to strength and constitution, a +4 bonus to wisdom and charisma, and a +2 bonus to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = LeopardGiantAdvancedSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonLeopard5Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonLeopardFinalSingle.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var SummonLeopardStandardD3 = Helpers.CreateBlueprint<BlueprintAbility>("SummonLeopardStandardD3", bp => {
                bp.SetName("Summon Leopard (1d3)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d3{/g} natural young leopards. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = LeopardSummon.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D3,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonLeopard4Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonLeopardStandardD3.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonLeopardYoungD3 = Helpers.CreateBlueprint<BlueprintAbility>("SummonLeopardYoungD3", bp => {
                bp.SetName("Summon Young Leopard (1d3)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d3{/g} natural young leopards. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nYoung: Creature " +
                    "is one size category smaller, has natural armor decreased by 2, gains a -4 penalty to strength and constitution, and a +4 bonus to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = LeopardYoungSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyI_III.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D3,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonLeopard3Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonLeopardYoungD3.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonLeopardGiantD3 = Helpers.CreateBlueprint<BlueprintAbility>("SummonLeopardGiantD3", bp => {
                bp.SetName("Summon Giant Leopard (1d3)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d3{/g} natural giant leopards. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nGiant: Creature " +
                    "is one size category larger, has natural armor increased by 3, gains a +4 bonus to strength and constitution, and a -2 penalty to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = LeopardGiantSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D3,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonLeopard5Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonLeopardGiantD3.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonLeopardAdvancedD3 = Helpers.CreateBlueprint<BlueprintAbility>("SummonLeopardAdvancedD3", bp => {
                bp.SetName("Summon Advanced Leopard (1d3)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d3{/g} natural advanced leopards. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nAdvanced: Creature " +
                    "has natural armor increased by 2, gains a +4 bonus all ability scores (except Int scores of 2 or less).");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = LeopardAdvancedSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D3,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonLeopard5Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonLeopardAdvancedD3.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonLeopardFinalD3 = Helpers.CreateBlueprint<BlueprintAbility>("SummonLeopardFinalD3", bp => {
                bp.SetName("Summon Giant Advanced Leopard (1d3)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d3{/g} natural giant advanced leopards. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nGiant + Advanced: Creature " +
                    "is one size category larger, has natural armor increased by 5, gains a +8 bonus to strength and constitution, a +4 bonus to wisdom and charisma, and a +2 bonus to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = LeopardAdvancedSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D3,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonLeopard6Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonLeopardFinalD3.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var SummonLeopardStandardD4plus1 = Helpers.CreateBlueprint<BlueprintAbility>("SummonLeopardStandardD4plus1", bp => {
                bp.SetName("Summon Leopard (1d4 + 1)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d4{/g} + 1 natural young leopards. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = LeopardSummon.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D4,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.OnePlusDivStep;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonLeopard5Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonLeopardStandardD4plus1.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonLeopardYoungD4plus1 = Helpers.CreateBlueprint<BlueprintAbility>("SummonLeopardYoungD4plus1", bp => {
                bp.SetName("Summon Young Leopard (1d4 + 1)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d4{/g} + 1 natural young leopards. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nYoung: Creature " +
                    "is one size category smaller, has natural armor decreased by 2, gains a -4 penalty to strength and constitution, and a +4 bonus to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = LeopardYoungSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D4,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.OnePlusDivStep;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonLeopard4Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonLeopardYoungD4plus1.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonLeopardGiantD4plus1 = Helpers.CreateBlueprint<BlueprintAbility>("SummonLeopardGiantD4plus1", bp => {
                bp.SetName("Summon Giant Leopard (1d4 + 1)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d4{/g} + 1 natural giant leopards. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nGiant: Creature " +
                    "is one size category larger, has natural armor increased by 3, gains a +4 bonus to strength and constitution, and a -2 penalty to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = LeopardGiantSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D4,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.OnePlusDivStep;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonLeopard6Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonLeopardGiantD4plus1.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonLeopardAdvancedD4plus1 = Helpers.CreateBlueprint<BlueprintAbility>("SummonLeopardAdvancedD4plus1", bp => {
                bp.SetName("Summon Advanced Leopard (1d4 + 1)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d4{/g} + 1 natural advanced leopards. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nAdvanced: Creature " +
                    "has natural armor increased by 2, gains a +4 bonus all ability scores (except Int scores of 2 or less).");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = LeopardAdvancedSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D4,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.OnePlusDivStep;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonLeopard6Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonLeopardAdvancedD4plus1.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonLeopardFinalD4plus1 = Helpers.CreateBlueprint<BlueprintAbility>("SummonLeopardFinalD4plus1", bp => {
                bp.SetName("Summon Giant Advanced Leopard (1d4 + 1)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d4{/g} + 1 natural giant advanced leopards. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nGiant + Advanced: Creature " +
                    "is one size category larger, has natural armor increased by 5, gains a +8 bonus to strength and constitution, a +4 bonus to wisdom and charisma, and a +2 bonus to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = LeopardAdvancedSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyVII_IX.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D4,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.OnePlusDivStep;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonLeopard7Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonLeopardFinalD4plus1.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var SummonSmilodonStandardSingle = Helpers.CreateBlueprint<BlueprintAbility>("SummonSmilodonStandardSingle", bp => {
                bp.SetName("Summon Smilodon");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side a natural smilodon. The summoned ally appears where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = SmilodonSummon.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyI_III.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonSmilodon6Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonSmilodonStandardSingle.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonSmilodonYoungSingle = Helpers.CreateBlueprint<BlueprintAbility>("SummonSmilodonYoungSingle", bp => {
                bp.SetName("Summon Young Smilodon");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side a natural young smilodon. The summoned ally appears where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nYoung: Creature " +
                    "is one size category smaller, has natural armor decreased by 2, gains a -4 penalty to strength and constitution, and a +4 bonus to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = SmilodonYoungSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyI_III.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonSmilodon5Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonSmilodonYoungSingle.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonSmilodonGiantSingle = Helpers.CreateBlueprint<BlueprintAbility>("SummonSmilodonGiantSingle", bp => {
                bp.SetName("Summon Giant Smilodon");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side a natural giant smilodon. The summoned ally appears where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nGiant: Creature " +
                    "is one size category larger, has natural armor increased by 3, gains a +4 bonus to strength and constitution, and a -2 penalty to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = SmilodonGiantSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonSmilodon7Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonSmilodonGiantSingle.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonSmilodonAdvancedSingle = Helpers.CreateBlueprint<BlueprintAbility>("SummonSmilodonAdvancedSingle", bp => {
                bp.SetName("Summon Advanced Smilodon");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side a natural advanced smilodon. The summoned ally appears where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nAdvanced: Creature " +
                    "has natural armor increased by 2, gains a +4 bonus all ability scores (except Int scores of 2 or less).");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = SmilodonAdvancedSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonSmilodon7Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonSmilodonAdvancedSingle.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonSmilodonFinalSingle = Helpers.CreateBlueprint<BlueprintAbility>("SummonSmilodonFinalSingle", bp => {
                bp.SetName("Summon Giant Advanced Smilodon");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side a natural giant advanced smilodon. The summoned ally appears where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nGiant + Advanced: Creature " +
                    "is one size category larger, has natural armor increased by 5, gains a +8 bonus to strength and constitution, a +4 bonus to wisdom and charisma, and a +2 bonus to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = SmilodonGiantAdvancedSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonSmilodon8Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonSmilodonFinalSingle.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var SummonSmilodonStandardD3 = Helpers.CreateBlueprint<BlueprintAbility>("SummonSmilodonStandardD3", bp => {
                bp.SetName("Summon Smilodon (1d3)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d3{/g} natural young smilodons. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = SmilodonSummon.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D3,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonSmilodon7Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonSmilodonStandardD3.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonSmilodonYoungD3 = Helpers.CreateBlueprint<BlueprintAbility>("SummonSmilodonYoungD3", bp => {
                bp.SetName("Summon Young Smilodon (1d3)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d3{/g} natural young smilodons. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nYoung: Creature " +
                    "is one size category smaller, has natural armor decreased by 2, gains a -4 penalty to strength and constitution, and a +4 bonus to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = SmilodonYoungSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyI_III.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D3,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonSmilodon6Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonSmilodonYoungD3.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonSmilodonGiantD3 = Helpers.CreateBlueprint<BlueprintAbility>("SummonSmilodonGiantD3", bp => {
                bp.SetName("Summon Giant Smilodon (1d3)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d3{/g} natural giant smilodons. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nGiant: Creature " +
                    "is one size category larger, has natural armor increased by 3, gains a +4 bonus to strength and constitution, and a -2 penalty to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = SmilodonGiantSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D3,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonSmilodon8Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonSmilodonGiantD3.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonSmilodonAdvancedD3 = Helpers.CreateBlueprint<BlueprintAbility>("SummonSmilodonAdvancedD3", bp => {
                bp.SetName("Summon Advanced Smilodon (1d3)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d3{/g} natural advanced smilodons. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nAdvanced: Creature " +
                    "has natural armor increased by 2, gains a +4 bonus all ability scores (except Int scores of 2 or less).");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = SmilodonAdvancedSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D3,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonSmilodon8Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonSmilodonAdvancedD3.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonSmilodonFinalD3 = Helpers.CreateBlueprint<BlueprintAbility>("SummonSmilodonFinalD3", bp => {
                bp.SetName("Summon Giant Advanced Smilodon (1d3)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d3{/g} natural giant advanced smilodons. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nGiant + Advanced: Creature " +
                    "is one size category larger, has natural armor increased by 5, gains a +8 bonus to strength and constitution, a +4 bonus to wisdom and charisma, and a +2 bonus to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = SmilodonAdvancedSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D3,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonSmilodon9Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonSmilodonFinalD3.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var SummonSmilodonStandardD4plus1 = Helpers.CreateBlueprint<BlueprintAbility>("SummonSmilodonStandardD4plus1", bp => {
                bp.SetName("Summon Smilodon (1d4 + 1)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d4{/g} + 1 natural young smilodons. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = SmilodonSummon.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D4,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.OnePlusDivStep;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonSmilodon8Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonSmilodonStandardD4plus1.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonSmilodonYoungD4plus1 = Helpers.CreateBlueprint<BlueprintAbility>("SummonSmilodonYoungD4plus1", bp => {
                bp.SetName("Summon Young Smilodon (1d4 + 1)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d4{/g} + 1 natural young smilodons. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nYoung: Creature " +
                    "is one size category smaller, has natural armor decreased by 2, gains a -4 penalty to strength and constitution, and a +4 bonus to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = SmilodonYoungSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D4,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.OnePlusDivStep;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonSmilodon7Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonSmilodonYoungD4plus1.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonSmilodonGiantD4plus1 = Helpers.CreateBlueprint<BlueprintAbility>("SummonSmilodonGiantD4plus1", bp => {
                bp.SetName("Summon Giant Smilodon (1d4 + 1)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d4{/g} + 1 natural giant smilodons. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nGiant: Creature " +
                    "is one size category larger, has natural armor increased by 3, gains a +4 bonus to strength and constitution, and a -2 penalty to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = SmilodonGiantSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D4,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.OnePlusDivStep;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonSmilodon9Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonSmilodonGiantD4plus1.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonSmilodonAdvancedD4plus1 = Helpers.CreateBlueprint<BlueprintAbility>("SummonSmilodonAdvancedD4plus1", bp => {
                bp.SetName("Summon Advanced Smilodon (1d4 + 1)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d4{/g} + 1 natural advanced smilodons. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nAdvanced: Creature " +
                    "has natural armor increased by 2, gains a +4 bonus all ability scores (except Int scores of 2 or less).");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = SmilodonAdvancedSummoned.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
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
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D4,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
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
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.OnePlusDivStep;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = SummonSmilodon9Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("SummonSmilodonAdvancedD4plus1.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });


            var LionShamanTotemicSummonsTempHitPoints = Helpers.CreateBuff("LionShamanTotemicSummonsTempHitPoints", bp => {
                bp.SetName("Totemic Ally Temporary Hit Points");
                bp.SetDescription("A lion totem druid may cast summon nature’s ally as a standard action when summoning felines, and summoned felines gain temporary hit points equal to their druid level.");
                bp.m_Icon = LionShamanTotemTransformationIcon;
                bp.AddComponent<TemporaryHitPointsFromAbilityValue>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.Default
                    };
                    c.RemoveWhenHitPointsEnd = true;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] { DruidClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Replace;
            });

            var LionShamanTotemicSummonsFeature = Helpers.CreateBlueprint<BlueprintFeature>("LionShamanTotemicSummonsFeature", bp => {
                bp.SetName("Totemic Summons");
                bp.SetDescription("At 5th level, a lion totem druid may cast summon nature’s ally as a standard action when summoning felines, and summoned felines gain temporary hit points equal to their druid level. " +
                    "They can apply the young template to any feline to reduce the level of the summoning spell required by one. They can also increase the level of summoning required by one in order to apply either " +
                    "the advanced or the giant template, or increase it by two to apply both the advanced and giant templates.");
                bp.AddComponent<OnSpawnBuff>(c => {
                    c.m_IfHaveFact = LionShamanTotemTransformationFeature.ToReference<BlueprintFeatureReference>();
                    c.CheckSummonedUnitFact = true;
                    c.m_IfSummonHaveFact = TotemCreature.ToReference<BlueprintFeatureReference>();
                    c.m_buff = LionShamanTotemicSummonsTempHitPoints.ToReference<BlueprintBuffReference>();
                    c.IsInfinity = true;
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonLeopardYoungSingle.ToReference<BlueprintAbilityReference>(),
                        SummonLeopardYoungD3.ToReference<BlueprintAbilityReference>(),
                        SummonLeopardYoungD4plus1.ToReference<BlueprintAbilityReference>(),
                        SummonSmilodonYoungSingle.ToReference<BlueprintAbilityReference>(),
                        SummonSmilodonYoungD3.ToReference<BlueprintAbilityReference>(),
                        SummonSmilodonYoungD4plus1.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()                        
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonLeopardStandardSingle.ToReference<BlueprintAbilityReference>(),
                        SummonLeopardStandardD3.ToReference<BlueprintAbilityReference>(),
                        SummonLeopardStandardD4plus1.ToReference<BlueprintAbilityReference>(),
                        SummonSmilodonStandardSingle.ToReference<BlueprintAbilityReference>(),
                        SummonSmilodonStandardD3.ToReference<BlueprintAbilityReference>(),
                        SummonSmilodonStandardD4plus1.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonLeopardGiantSingle.ToReference<BlueprintAbilityReference>(),
                        SummonLeopardGiantD3.ToReference<BlueprintAbilityReference>(),
                        SummonLeopardGiantD4plus1.ToReference<BlueprintAbilityReference>(),
                        SummonSmilodonGiantSingle.ToReference<BlueprintAbilityReference>(),
                        SummonSmilodonGiantD3.ToReference<BlueprintAbilityReference>(),
                        SummonSmilodonGiantD4plus1.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonLeopardAdvancedSingle.ToReference<BlueprintAbilityReference>(),
                        SummonLeopardAdvancedD3.ToReference<BlueprintAbilityReference>(),
                        SummonLeopardAdvancedD4plus1.ToReference<BlueprintAbilityReference>(),
                        SummonSmilodonAdvancedSingle.ToReference<BlueprintAbilityReference>(),
                        SummonSmilodonAdvancedD3.ToReference<BlueprintAbilityReference>(),
                        SummonSmilodonAdvancedD4plus1.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonLeopardFinalSingle.ToReference<BlueprintAbilityReference>(),
                        SummonLeopardFinalD3.ToReference<BlueprintAbilityReference>(),
                        SummonLeopardFinalD4plus1.ToReference<BlueprintAbilityReference>(),
                        SummonSmilodonFinalSingle.ToReference<BlueprintAbilityReference>(),
                        SummonSmilodonFinalD3.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
            });

            //Adding Bear Shaman to Domain Configs
            //Glory Domain Hooks
            var GloryDomainBaseBuffConfig = Resources.GetBlueprint<BlueprintBuff>("55edcfff497a1e04a963f72c485da5cb").GetComponent<ContextRankConfig>();
            GloryDomainBaseBuffConfig.m_AdditionalArchetypes = GloryDomainBaseBuffConfig.m_AdditionalArchetypes.AppendToArray(LionShamanArchetype.ToReference<BlueprintArchetypeReference>());
            GloryDomainBaseBuffConfig.m_Class = GloryDomainBaseBuffConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            var GloryDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("d018241b5a761414897ad6dc4df2db9f").GetComponent<ContextRankConfig>();
            GloryDomainBaseAbilityConfig.m_AdditionalArchetypes = GloryDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(LionShamanArchetype.ToReference<BlueprintArchetypeReference>());
            GloryDomainBaseAbilityConfig.m_Class = GloryDomainBaseAbilityConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            var GloryDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("17e891b3964492f43aae44f994b5d454").GetComponent<AddFeatureOnClassLevel>();
            GloryDomainBaseFeatureConfig.m_AdditionalClasses = GloryDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            GloryDomainBaseFeatureConfig.m_Archetypes = GloryDomainBaseFeatureConfig.m_Archetypes.AppendToArray(LionShamanArchetype.ToReference<BlueprintArchetypeReference>());
            var GloryDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("c89e92387e940e541b02c1969cd1fe2a").GetComponent<ContextRankConfig>();
            GloryDomainGreaterAbilityConfig.m_AdditionalArchetypes = GloryDomainGreaterAbilityConfig.m_AdditionalArchetypes.AppendToArray(LionShamanArchetype.ToReference<BlueprintArchetypeReference>());
            GloryDomainGreaterAbilityConfig.m_Class = GloryDomainGreaterAbilityConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            //Nobility Domain Hooks - Class added in Urban druid, this is only the archetype
            var NobilityDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("7a305ef528cb7884385867a2db410102").GetComponent<ContextRankConfig>();
            NobilityDomainBaseAbilityConfig.m_AdditionalArchetypes = NobilityDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(LionShamanArchetype.ToReference<BlueprintArchetypeReference>());
            var NobilityDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("a1a7f3dd904ed8e45b074232f48190d1").GetComponent<AddFeatureOnClassLevel>();
            NobilityDomainBaseFeatureConfig.m_Archetypes = NobilityDomainBaseFeatureConfig.m_Archetypes.AppendToArray(LionShamanArchetype.ToReference<BlueprintArchetypeReference>());
            var NobilityDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("3fc6e1f3acbcb0e4c83badf7709ce53d");
            NobilityDomainBaseResource.m_MaxAmount.m_Archetypes = NobilityDomainBaseResource.m_MaxAmount.m_Archetypes.AppendToArray(LionShamanArchetype.ToReference<BlueprintArchetypeReference>());
            var NobilityDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("2972215a5367ae44b8ddfe435a127a6e").GetComponent<ContextRankConfig>();
            NobilityDomainGreaterAbilityConfig.m_AdditionalArchetypes = NobilityDomainGreaterAbilityConfig.m_AdditionalArchetypes.AppendToArray(LionShamanArchetype.ToReference<BlueprintArchetypeReference>());
            //Sun Domain Hooks            
            var SunDomainBaseFeatureDruid = Resources.GetModBlueprint<BlueprintFeature>("SunDomainBaseFeatureDruid").GetComponent<IncreaseSpellDamageByClassLevel>();
            SunDomainBaseFeatureDruid.m_Archetypes = SunDomainBaseFeatureDruid.m_Archetypes.AppendToArray(LionShamanArchetype.ToReference<BlueprintArchetypeReference>());
            var SunDomainGreaterAuraConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("cfe8c5683c759f047a56a4b5e77ac93f").GetComponent<ContextRankConfig>();
            SunDomainGreaterAuraConfig.m_AdditionalArchetypes = SunDomainGreaterAuraConfig.m_AdditionalArchetypes.AppendToArray(LionShamanArchetype.ToReference<BlueprintArchetypeReference>());
            SunDomainGreaterAuraConfig.m_Class = SunDomainGreaterAuraConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            var SunDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("6bea29e2257fa6742923ba757435aba8");
            SunDomainGreaterResource.m_MaxAmount.m_Class = SunDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            SunDomainGreaterResource.m_MaxAmount.m_Archetypes = SunDomainGreaterResource.m_MaxAmount.m_Archetypes.AppendToArray(LionShamanArchetype.ToReference<BlueprintArchetypeReference>());
            SunDomainGreaterResource.m_MaxAmount.m_ClassDiv = SunDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());



            LionShamanArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, DruidBondSelection),
                    Helpers.LevelEntry(4, ResistNaturesLureFeature),
                    Helpers.LevelEntry(6, WildShapeIILeopardFeature, WildShapeElementalSmallFeature),
                    Helpers.LevelEntry(8, WildShapeIVBearFeature, WildShapeElementalFeatureAddMediumFeature),
                    Helpers.LevelEntry(9, VenomImmunityFeature),
                    Helpers.LevelEntry(10, WildShapeIIISmilodonFeature, WildShapeElementalFeatureAddLargeFeature, WildShapeIVShamblingMoundFeature),
                    Helpers.LevelEntry(12, WildShapeElementalHugeFeatureFeature)
            };
            LionShamanArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, LionShamanBondSelection),
                    Helpers.LevelEntry(2, LionShamanTotemTransformationFeature),
                    Helpers.LevelEntry(4, LionShamanWildShape, WildShapeIILeopardFeature),
                    Helpers.LevelEntry(5, LionShamanTotemicSummonsFeature),
                    Helpers.LevelEntry(7, LionShamanTotemTransformationMoveFeature),
                    Helpers.LevelEntry(8, WildShapeIIISmilodonFeature, WildShapeElementalSmallFeature),
                    Helpers.LevelEntry(9, LionShamanBonusFeatSelection),
                    Helpers.LevelEntry(10, WildShapeIVBearFeature, WildShapeElementalFeatureAddMediumFeature),
                    Helpers.LevelEntry(12, WildShapeElementalFeatureAddLargeFeature, WildShapeIVShamblingMoundFeature, LionShamanTotemTransformationSwiftFeature),
                    Helpers.LevelEntry(13, LionShamanBonusFeatSelection),
                    Helpers.LevelEntry(14, WildShapeElementalHugeFeatureFeature),
                    Helpers.LevelEntry(17, LionShamanBonusFeatSelection)
            };
            DruidClass.Progression.UIGroups = DruidClass.Progression.UIGroups.AppendToArray(
                Helpers.CreateUIGroup(LionShamanTotemTransformationFeature, LionShamanTotemicSummonsFeature, LionShamanTotemTransformationMoveFeature, LionShamanTotemTransformationSwiftFeature)
            );
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Lion Shaman")) { return; }
            DruidClass.m_Archetypes = DruidClass.m_Archetypes.AppendToArray(LionShamanArchetype.ToReference<BlueprintArchetypeReference>());

        }
    }
}
