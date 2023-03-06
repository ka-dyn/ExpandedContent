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
    internal class BearShaman {
        public static void AddBearShaman() {

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
            var EarthDomainProgressionDruid = Resources.GetBlueprint<BlueprintProgression>("a3217dc55003b914aa296da7ada029bc");
            var ProtectionDomainProgressionDruid = Resources.GetModBlueprint<BlueprintProgression>("ProtectionDomainProgressionDruid");
            var StrengthDomainProgressionDruid = Resources.GetModBlueprint<BlueprintProgression>("StrengthDomainProgressionDruid");

            var BearShamanArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("BearShamanArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"BearShamanArchetype.Name", "Bear Totem Druid");
                bp.LocalizedDescription = Helpers.CreateString($"BearShamanArchetype.Description", "A druid whose focus calls upon the mighty bear, titan of the woodlands and mountains, a paragon of " +
                    "strength and ferocity, and yet also a quiet protector rich in wisdom.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"BearShamanArchetype.Description", "A druid whose focus calls upon the mighty bear, titan of the woodlands and mountains, a paragon " +
                    "of strength and ferocity, and yet also a quiet protector rich in wisdom.");
            });
            var BearShamanWildShape = Helpers.CreateBlueprint<BlueprintFeature>("BearShamanWildShape", bp => {
                bp.SetName("Wild Shape - Bear Totem Druid");
                bp.SetDescription("A bear totem druid wild shape is effected by their dedication to the bear sprits. All wild shape forms gained from level 6 onwards are gained 2 levels later than a standard druid, however " +
                    "bear form is gained 2 levels earlier.");
                bp.IsClassFeature = true;
            });

            var AnimalCompanionFeatureBear = Resources.GetBlueprintReference<BlueprintFeatureReference>("f6f1cdcc404f10c4493dc1e51208fd6f");
            var BearShamanDomainSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("BearShamanDomainSelection", bp => {
                bp.SetName("Bear Totem Druids Bond");
                bp.SetDescription("A bear totem druid who chooses an animal companion must select a bear. If choosing a domain, the bear totem druid must choose from the Animal, Earth, Protection, and Strength domains.");
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.OnlyNew;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    AnimalDomainProgressionDruid.ToReference<BlueprintFeatureReference>(),
                    EarthDomainProgressionDruid.ToReference<BlueprintFeatureReference>(),
                    ProtectionDomainProgressionDruid.ToReference<BlueprintFeatureReference>(),
                    StrengthDomainProgressionDruid.ToReference<BlueprintFeatureReference>()
                };
            });
            var BearShamanBondSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("BearShamanBondSelection", bp => {
                bp.SetName("Bear Totem Druids Bond");
                bp.SetDescription("A bear totem druid who chooses an animal companion must select a bear. If choosing a domain, the bear totem druid must choose from the Animal, Earth, Protection, and Strength domains.");
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.OnlyNew;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddFeatures(AnimalCompanionFeatureBear, BearShamanDomainSelection);
            });

            var DiehardFeat = Resources.GetBlueprintReference<BlueprintFeatureReference>("86669ce8759f9d7478565db69b8c19ad");
            var EnduranceFeat = Resources.GetBlueprintReference<BlueprintFeatureReference>("54ee847996c25cd4ba8773d7b8555174");
            var GreatFortitudeFeat = Resources.GetBlueprintReference<BlueprintFeatureReference>("79042cb55f030614ea29956177977c52");
            var ImprovedGreatFortitudeFeat = Resources.GetBlueprintReference<BlueprintFeatureReference>("f5db1cc7ad48d794f85252fa4a64157b");
            var ToughnessFeat = Resources.GetBlueprintReference<BlueprintFeatureReference>("d09b20029e9abfe4480b356c92095623");
            var BearShamanBonusFeatSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("BearShamanBonusFeatSelection", bp => {
                bp.SetName("Bear Totem Druid Bonus Feat");
                bp.SetDescription("At 9th level and every 4 levels thereafter, a bear totem druid gains one of the following bonus feats: Diehard, Endurance, Great Fortitude, Improved Great " +
                    "Fortitude, Toughness. She must meet the prerequisites for these bonus feats.");
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.OnlyNew;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddFeatures(DiehardFeat, EnduranceFeat, GreatFortitudeFeat, ImprovedGreatFortitudeFeat,ToughnessFeat);
            });

            var BearShamanTotemTransformationResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("BearShamanTotemTransformationResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = true,
                    m_Class = new BlueprintCharacterClassReference[] {                        
                        DruidClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[] {
                        BearShamanArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    LevelIncrease = 1,
                    StartingLevel = 2,
                    StartingIncrease = 1,
                };
            });

            var Bite1d6 = Resources.GetBlueprint<BlueprintItemWeapon>("a000716f88c969c499a535dadcf09286");
            var BloodlineAbyssalClaw1d4 = Resources.GetBlueprint<BlueprintItemWeapon>("289c13ba102d0df43862a488dad8a5d5");
            var CheetahSprintIcon = AssetLoader.LoadInternal("Skills", "Icon_CheetahSprint.jpg");
            var SenseVitals = Resources.GetBlueprint<BlueprintAbility>("82962a820ebc0e7408b8582fdc3f4c0c");
            var BearsEndurance = Resources.GetBlueprint<BlueprintAbility>("a900628aea19aa74aad0ece0e65d091a");
            var DruidClawsIcon = AssetLoader.LoadInternal("Skills", "Icon_DruidClaws.jpg");

            var BearShamanTotemTransformationMovementBuff = Helpers.CreateBuff("BearShamanTotemTransformationMovementBuff", bp => {
                bp.SetName("Bear Totem Transformation - Movement");
                bp.SetDescription("A bear totem druid may select which aspect of the bear to embue themselves with.\nMovement (+10 enhancement bonus to speed, +4 racial bonus on Mobility checks)");
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
            var BearShamanTotemTransformationSensesBuff = Helpers.CreateBuff("BearShamanTotemTransformationSensesBuff", bp => {
                bp.SetName("Bear Totem Transformation - Senses");
                bp.SetDescription("A bear totem druid may select which aspect of the bear to embue themselves with.\nSenses (Blind Fight feat, +4 on racial bonus on perception checks)");
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
            var BearShamanTotemTransformationToughnessBuff = Helpers.CreateBuff("BearShamanTotemTransformationToughnessBuff", bp => {
                bp.SetName("Bear Totem Transformation - Toughness");
                bp.SetDescription("A bear totem druid may select which aspect of the bear to embue themselves with.\nToughness (+2 natural armor bonus to AC, Endurance feat)");
                bp.m_Icon = BearsEndurance.Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.Fatigue | SpellDescriptor.Exhausted;
                    c.ModifierDescriptor = ModifierDescriptor.None;
                    c.Value = 4;
                    c.Bonus = new ContextValue();

                });
                bp.AddComponent<AddMechanicsFeature>(c => {
                    c.m_Feature = AddMechanicsFeature.MechanicsFeatureType.Endurance;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Stat = StatType.SkillAthletics;
                    c.Value = 2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Stat = StatType.AC;
                    c.Value = 2;
                });
            });
            var BearShamanTotemTransformationNaturalWeaponsBuff = Helpers.CreateBuff("BearShamanTotemTransformationNaturalWeaponsBuff", bp => {
                bp.SetName("Bear Totem Transformation - Natural Weapons");
                bp.SetDescription("A bear totem druid may select which aspect of the bear to embue themselves with.\nNatural Weapons (bite {g|Encyclopedia:Attack}attack{/g} dealing " +
                    "{g|Encyclopedia:Dice}1d6{/g} {g|Encyclopedia:Damage}damage{/g} and two claws dealing {g|Encyclopedia:Dice}1d4{/g} {g|Encyclopedia:Damage}damage{/g} " +
                    "for a Medium druid, +2 to CMB)");
                bp.m_Icon = DruidClawsIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;                
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = Bite1d6.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddComponent<EmptyHandWeaponOverride>(c => {
                    c.m_Weapon = BloodlineAbyssalClaw1d4.ToReference<BlueprintItemWeaponReference>();
                    c.IsPermanent = false;
                    c.IsMonkUnarmedStrike = false;
                });
            });

            var BearShamanTotemTransformationPolymorphHook = Helpers.CreateBuff("BearShamanTotemTransformationPolymorphHook", bp => {
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
                        new ContextActionRemoveBuff() { m_Buff = BearShamanTotemTransformationMovementBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = BearShamanTotemTransformationSensesBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = BearShamanTotemTransformationToughnessBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = BearShamanTotemTransformationNaturalWeaponsBuff.ToReference<BlueprintBuffReference>() }
                        );
                    c.NewRound = Helpers.CreateActionList();
                });
            });
            var BearShamanTotemTransformationIcon = AssetLoader.LoadInternal("Skills", "Icon_BearShamanTotemTransformation.jpg");
            var BearShamanTotemTransformationAbilityStandard = Helpers.CreateBlueprint<BlueprintAbility>("BearShamanTotemTransformationAbilityStandard", bp => {
                bp.SetName("Bear Totem Transformation");
                bp.SetDescription("As a standard action a bear totem druid may adopt an aspect of the bear while retaining her normal form. They gain one of the following bonuses: \nMovement (+10 enhancement " +
                    "bonus to speed, +4 racial bonus on Mobility checks)\nSenses (Blind Fight feat, +4 on perception checks)\nToughness (+2 natural armor bonus to AC, Endurance feat)\nNatural Weapons (bite " +
                    "{g|Encyclopedia:Attack}attack{/g} dealing {g|Encyclopedia:Dice}1d6{/g} {g|Encyclopedia:Damage}damage{/g} and two claws dealing {g|Encyclopedia:Dice}1d4{/g} {g|Encyclopedia:Damage}damage{/g} " +
                    "for a Medium druid, +2 to CMB)");
                bp.m_Icon = BearShamanTotemTransformationIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BearShamanTotemTransformationPolymorphHook.ToReference<BlueprintBuffReference>(),
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
                    c.m_RequiredResource = BearShamanTotemTransformationResource.ToReference<BlueprintAbilityResourceReference>();
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
                bp.LocalizedDuration = Helpers.CreateString("BearShamanTotemTransformationAbilityStandard.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var BearShamanTotemTransformationAbilityMove = Helpers.CreateBlueprint<BlueprintAbility>("BearShamanTotemTransformationAbilityMove", bp => {
                bp.SetName("Bear Totem Transformation - Move Action");
                bp.SetDescription("As a move action a bear totem druid may adopt an aspect of the bear while retaining her normal form. They gain one of the following bonuses: \nMovement (+10 enhancement " +
                    "bonus to speed, +4 racial bonus on Mobility checks)\nSenses (Blind Fight feat, +4 on perception checks)\nToughness (+2 natural armor bonus to AC, Endurance feat)\nNatural Weapons (bite " +
                    "{g|Encyclopedia:Attack}attack{/g} dealing {g|Encyclopedia:Dice}1d6{/g} {g|Encyclopedia:Damage}damage{/g} and two claws dealing {g|Encyclopedia:Dice}1d4{/g} {g|Encyclopedia:Damage}damage{/g} " +
                    "for a Medium druid, +2 to CMB)");
                bp.m_Icon = BearShamanTotemTransformationIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BearShamanTotemTransformationPolymorphHook.ToReference<BlueprintBuffReference>(),
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
                    c.m_RequiredResource = BearShamanTotemTransformationResource.ToReference<BlueprintAbilityResourceReference>();
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
                bp.LocalizedDuration = Helpers.CreateString("BearShamanTotemTransformationAbilityMove.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var BearShamanTotemTransformationAbilitySwift = Helpers.CreateBlueprint<BlueprintAbility>("BearShamanTotemTransformationAbilitySwift", bp => {
                bp.SetName("Bear Totem Transformation - Swift Action");
                bp.SetDescription("As a swift action a bear totem druid may adopt an aspect of the bear while retaining her normal form. They gain one of the following bonuses: \nMovement (+10 enhancement " +
                    "bonus to speed, +4 racial bonus on Mobility checks)\nSenses (Blind Fight feat, +4 on perception checks)\nToughness (+2 natural armor bonus to AC, Endurance feat)\nNatural Weapons (bite " +
                    "{g|Encyclopedia:Attack}attack{/g} dealing {g|Encyclopedia:Dice}1d6{/g} {g|Encyclopedia:Damage}damage{/g} and two claws dealing {g|Encyclopedia:Dice}1d4{/g} {g|Encyclopedia:Damage}damage{/g} " +
                    "for a Medium druid, +2 to CMB)");
                bp.m_Icon = BearShamanTotemTransformationIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BearShamanTotemTransformationPolymorphHook.ToReference<BlueprintBuffReference>(),
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
                    c.m_RequiredResource = BearShamanTotemTransformationResource.ToReference<BlueprintAbilityResourceReference>();
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
                bp.LocalizedDuration = Helpers.CreateString("BearShamanTotemTransformationAbilitySwift.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var BearShamanTotemTransformationMovementToggleBuff = Helpers.CreateBuff("BearShamanTotemTransformationMovementToggleBuff", bp => {
                bp.SetName("Bear Totem Transformation - Movement");
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
                        BearShamanTotemTransformationAbilityStandard.ToReference<BlueprintAbilityReference>(),
                        BearShamanTotemTransformationAbilityMove.ToReference<BlueprintAbilityReference>(),
                        BearShamanTotemTransformationAbilitySwift.ToReference<BlueprintAbilityReference>()
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
                            m_Buff = BearShamanTotemTransformationMovementBuff.ToReference<BlueprintBuffReference>(),
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
            var BearShamanTotemTransformationSensesToggleBuff = Helpers.CreateBuff("BearShamanTotemTransformationSensesToggleBuff", bp => {
                bp.SetName("Bear Totem Transformation - Senses");
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
                        BearShamanTotemTransformationAbilityStandard.ToReference<BlueprintAbilityReference>(),
                        BearShamanTotemTransformationAbilityMove.ToReference<BlueprintAbilityReference>(),
                        BearShamanTotemTransformationAbilitySwift.ToReference<BlueprintAbilityReference>()
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
                            m_Buff = BearShamanTotemTransformationSensesBuff.ToReference<BlueprintBuffReference>(),
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
            var BearShamanTotemTransformationToughnessToggleBuff = Helpers.CreateBuff("BearShamanTotemTransformationToughnessToggleBuff", bp => {
                bp.SetName("Bear Totem Transformation - Toughness");
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
                        BearShamanTotemTransformationAbilityStandard.ToReference<BlueprintAbilityReference>(),
                        BearShamanTotemTransformationAbilityMove.ToReference<BlueprintAbilityReference>(),
                        BearShamanTotemTransformationAbilitySwift.ToReference<BlueprintAbilityReference>()
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
                            m_Buff = BearShamanTotemTransformationToughnessBuff.ToReference<BlueprintBuffReference>(),
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
            var BearShamanTotemTransformationNaturalWeaponsToggleBuff = Helpers.CreateBuff("BearShamanTotemTransformationNaturalWeaponsToggleBuff", bp => {
                bp.SetName("Bear Totem Transformation - Natural Weapons");
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
                        BearShamanTotemTransformationAbilityStandard.ToReference<BlueprintAbilityReference>(),
                        BearShamanTotemTransformationAbilityMove.ToReference<BlueprintAbilityReference>(),
                        BearShamanTotemTransformationAbilitySwift.ToReference<BlueprintAbilityReference>()
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
                            m_Buff = BearShamanTotemTransformationNaturalWeaponsBuff.ToReference<BlueprintBuffReference>(),
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

            var BearShamanTotemTransformationMovementToggleAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("BearShamanTotemTransformationMovementToggleAbility", bp => {
                bp.SetName("Bear Totem Transformation - Movement");
                bp.SetDescription("A bear totem druid may select which aspect of the bear to embue themselves with.\nMovement (+10 enhancement bonus to speed, +4 racial bonus on Mobility checks)");
                bp.m_Icon = CheetahSprintIcon;
                bp.m_Buff = BearShamanTotemTransformationMovementToggleBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.Group = ActivatableAbilityGroup.DivineWeaponProperty;
                bp.WeightInGroup = 1;
            });
            var BearShamanTotemTransformationSensesToggleAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("BearShamanTotemTransformationSensesToggleAbility", bp => {
                bp.SetName("Bear Totem Transformation - Senses");
                bp.SetDescription("A bear totem druid may select which aspect of the bear to embue themselves with.\nSenses (Blind Fight feat, +4 on perception checks)");
                bp.m_Icon = SenseVitals.Icon;
                bp.m_Buff = BearShamanTotemTransformationSensesToggleBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.Group = ActivatableAbilityGroup.DivineWeaponProperty;
                bp.WeightInGroup = 1;
            });
            var BearShamanTotemTransformationToughnessToggleAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("BearShamanTotemTransformationToughnessToggleAbility", bp => {
                bp.SetName("Bear Totem Transformation - Toughness");
                bp.SetDescription("A bear totem druid may select which aspect of the bear to embue themselves with.\nToughness (+2 natural armor bonus to AC, Endurance feat)");
                bp.m_Icon = BearsEndurance.Icon;
                bp.m_Buff = BearShamanTotemTransformationToughnessToggleBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.Group = ActivatableAbilityGroup.DivineWeaponProperty;
                bp.WeightInGroup = 1;
            });
            var BearShamanTotemTransformationNaturalWeaponsToggleAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("BearShamanTotemTransformationNaturalWeaponsToggleAbility", bp => {
                bp.SetName("Bear Totem Transformation - Natural Weapons");
                bp.SetDescription("A bear totem druid may select which aspect of the bear to embue themselves with.\nNatural Weapons (bite {g|Encyclopedia:Attack}attack{/g} dealing " +
                    "{g|Encyclopedia:Dice}1d6{/g} {g|Encyclopedia:Damage}damage{/g} and two claws dealing {g|Encyclopedia:Dice}1d4{/g} {g|Encyclopedia:Damage}damage{/g} " +
                    "for a Medium druid, +2 to CMB)");
                bp.m_Icon = DruidClawsIcon;
                bp.m_Buff = BearShamanTotemTransformationNaturalWeaponsToggleBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.Group = ActivatableAbilityGroup.DivineWeaponProperty;
                bp.WeightInGroup = 1;
            });

            var BearShamanTotemTransformationFeature = Helpers.CreateBlueprint<BlueprintFeature>("BearShamanTotemTransformationFeature", bp => {
                bp.SetName("Bear Totem Transformation");
                bp.SetDescription("At 2nd level, a bear totem druid may adopt an aspect of the bear while retaining their normal form. They gain one of the following bonuses: \nMovement (+10 enhancement " +
                    "bonus to speed, +4 racial bonus on Mobility checks)\nSenses (Blind Fight feat, +4 on perception checks)\nToughness (+2 natural armor bonus to AC, Endurance feat)\nNatural Weapons (bite " +
                    "{g|Encyclopedia:Attack}attack{/g} dealing {g|Encyclopedia:Dice}1d6{/g} {g|Encyclopedia:Damage}damage{/g} and two claws dealing {g|Encyclopedia:Dice}1d4{/g} {g|Encyclopedia:Damage}damage{/g} " +
                    "for a Medium druid, +2 to CMB)" +
                    "\nUsing this ability is a standard action at 2nd level, a move action at 7th level, and a swift action at 12th level. This ability lasts one minute and can be used a number of times equal " +
                    "to their druid level. This is a polymorph effect and cannot be used while the druid is using another polymorph effect, such as wild shape.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        BearShamanTotemTransformationAbilityStandard.ToReference<BlueprintUnitFactReference>(),
                        BearShamanTotemTransformationMovementToggleAbility.ToReference<BlueprintUnitFactReference>(),
                        BearShamanTotemTransformationSensesToggleAbility.ToReference<BlueprintUnitFactReference>(),
                        BearShamanTotemTransformationToughnessToggleAbility.ToReference<BlueprintUnitFactReference>(),
                        BearShamanTotemTransformationNaturalWeaponsToggleAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = BearShamanTotemTransformationResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
            });
            var BearShamanTotemTransformationMoveFeature = Helpers.CreateBlueprint<BlueprintFeature>("BearShamanTotemTransformationMoveFeature", bp => {
                bp.SetName("Bear Totem Transformation (Move Action)");
                bp.SetDescription("At 2nd level, a bear totem druid may adopt an aspect of the bear while retaining their normal form. They gain one of the following bonuses: \nMovement (+10 enhancement " +
                    "bonus to speed, +4 racial bonus on Mobility checks)\nSenses (Blind Fight feat, +4 on perception checks)\nToughness (+2 natural armor bonus to AC, Endurance feat)\nNatural Weapons (bite " +
                    "{g|Encyclopedia:Attack}attack{/g} dealing {g|Encyclopedia:Dice}1d6{/g} {g|Encyclopedia:Damage}damage{/g} and two claws dealing {g|Encyclopedia:Dice}1d4{/g} {g|Encyclopedia:Damage}damage{/g} " +
                    "for a Medium druid, +2 to CMB)" +
                    "\nUsing this ability is a standard action at 2nd level, a move action at 7th level, and a swift action at 12th level. This ability lasts one minute and can be used a number of times equal " +
                    "to their druid level. This is a polymorph effect and cannot be used while the druid is using another polymorph effect, such as wild shape.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        BearShamanTotemTransformationAbilityMove.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
            });
            var BearShamanTotemTransformationSwiftFeature = Helpers.CreateBlueprint<BlueprintFeature>("BearShamanTotemTransformationSwiftFeature", bp => {
                bp.SetName("Bear Totem Transformation (Swift Action)");
                bp.SetDescription("At 2nd level, a bear totem druid may adopt an aspect of the bear while retaining their normal form. They gain one of the following bonuses: \nMovement (+10 enhancement " +
                    "bonus to speed, +4 racial bonus on Mobility checks)\nSenses (Blind Fight feat, +4 on perception checks)\nToughness (+2 natural armor bonus to AC, Endurance feat)\nNatural Weapons (bite " +
                    "{g|Encyclopedia:Attack}attack{/g} dealing {g|Encyclopedia:Dice}1d6{/g} {g|Encyclopedia:Damage}damage{/g} and two claws dealing {g|Encyclopedia:Dice}1d4{/g} {g|Encyclopedia:Damage}damage{/g} " +
                    "for a Medium druid, +2 to CMB)" +
                    "\nUsing this ability is a standard action at 2nd level, a move action at 7th level, and a swift action at 12th level. This ability lasts one minute and can be used a number of times equal " +
                    "to their druid level. This is a polymorph effect and cannot be used while the druid is using another polymorph effect, such as wild shape.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        BearShamanTotemTransformationAbilitySwift.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
            });

            var GreaslyBearSummon = Resources.GetBlueprint<BlueprintUnit>("e8684351ff2e92c44a7fafd45d4fba73");
            var SummonMonsterPool = Resources.GetBlueprint<BlueprintSummonPool>("d94c93e7240f10e41ae41db4c83d1cbe");
            var SummonedCreatureSpawnAllyI_III = Resources.GetBlueprint<BlueprintBuff>("2c3b98e56c1b4e9479f692d7ef95b2d2");
            var SummonedCreatureSpawnAllyIV_VI = Resources.GetBlueprint<BlueprintBuff>("25629d7e78016a340b0e50818b6d8bb5");
            var SummonedCreatureSpawnAllyVII_IX = Resources.GetBlueprint<BlueprintBuff>("932d27490e1701548a48b4cbc2f2caac");
            var AnimalClass = Resources.GetBlueprint<BlueprintCharacterClass>("4cd1757a0eea7694ba5c933729a53920");
            var HeadLocatorFeature = Resources.GetBlueprint<BlueprintFeature>("9c57e9674b4a4a2b9920f9fec47f7e6a");
            var DumbMosterBrain = Resources.GetBlueprint<BlueprintBrain>("5abc8884c6f15204c8604cb01a2efbab");
            var Summoned = Resources.GetBlueprint<BlueprintFaction>("1b08d9ed04518ec46a9b3e4e23cb5105");
            var BearBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("90ac36b1eee5d7c429e4aea89dd3f1dc");
            var BearPortait = Resources.GetBlueprint<BlueprintPortrait>("5e5cae0071684bc7910bfd1f5e759661"); //BearDireElite portrait
            var Bite1d4 = Resources.GetBlueprint<BlueprintItemWeapon>("35dfad6517f401145af54111be04d6cf");
            var Claw1d4 = Resources.GetBlueprint<BlueprintItemWeapon>("118fdd03e569a66459ab01a20af6811a");
            var ClawLarge1d6 = Resources.GetBlueprint<BlueprintItemWeapon>("c76f72a862d168d44838206524366e1c");
            var Unlootable = Resources.GetBlueprintReference<BlueprintBuffReference>("0f775c7d5d8b6494197e1ce937754482");
            var NatualAllyCreatureVisual = Resources.GetBlueprintReference<BlueprintBuffReference>("e4b996b5168fe284ab3141a91895d7ea");
            var SubtypeExtraplanar = Resources.GetBlueprint<BlueprintFeature>("136fa0343d5b4b348bdaa05d83408db3");
            var SuperiorSummoning = Resources.GetBlueprint<BlueprintFeature>("0477936c0f74841498b5c8753a8062a3");
            var BloodlineAbyssalSummoning = Resources.GetBlueprint<BlueprintFeature>("de24d9e57d7bad24dbada7389eebcd65");

            var BasicFeatSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("247a4068296e8be42890143f451b4b45");
            var IronWillFeat = Resources.GetBlueprintReference<BlueprintFeatureReference>("175d1577bb6c9a04baf88eec99c66334");
            var ImprovedInitiativeFeat = Resources.GetBlueprintReference<BlueprintFeatureReference>("797f25d709f559546b29e7bcb181cc74");
            var SkillFocusPerseptionFeat = Resources.GetBlueprintReference<BlueprintFeatureReference>("f74c6bdf5c5f5374fb9302ecdc1f7d64");
            var SkillFocusSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("c9629ef9eebb88b479b2fbc5e836656a");

            var ReducedReach = Resources.GetBlueprint<BlueprintUnitFact>("c33f2d68d93ceee488aa4004347dffca");
            var NaturalArmor6 = Resources.GetBlueprint<BlueprintUnitFact>("987ba44303e88054c9504cb3083ba0c9");
            var NaturalArmor4 = Resources.GetBlueprint<BlueprintUnitFact>("16fc201a83edcde4cbd64c291ebe0d07");
            var NaturalArmor9 = Resources.GetBlueprint<BlueprintUnitFact>("da6417809bdedfa468dd2fd0cc74be92");
            var NaturalArmor8 = Resources.GetBlueprint<BlueprintUnitFact>("b9342e2a6dc5165489ba3412c50ca3d1");
            var NaturalArmor11 = Resources.GetBlueprint<BlueprintUnitFact>("fe38367139432294e8c229edc066e4ac");

            var SizeIncreasePlus1 = Helpers.CreateBlueprint<BlueprintFeature>("SizeIncreasePlus1", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.HideInUI = true;
                bp.AddComponent<ChangeUnitSize>(c => {
                    c.m_Type = ChangeUnitSize.ChangeType.Delta;
                    c.SizeDelta = 1;
                    c.Size = Size.Fine;
                });
            });
            var TotemCreature = Helpers.CreateBlueprint<BlueprintFeature>("TotemCreature", bp => {
                bp.SetName("TotemCreature");
                bp.SetDescription("");
                bp.HideInUI = true;                
            });

            // Summons
            var SummonBear3Icon = AssetLoader.LoadInternal("Skills", "Icon_SummonBear3.jpg");
            var SummonBear4Icon = AssetLoader.LoadInternal("Skills", "Icon_SummonBear4.jpg");
            var SummonBear5Icon = AssetLoader.LoadInternal("Skills", "Icon_SummonBear5.jpg");
            var SummonBear6Icon = AssetLoader.LoadInternal("Skills", "Icon_SummonBear6.jpg");
            var SummonBear7Icon = AssetLoader.LoadInternal("Skills", "Icon_SummonBear7.jpg");
            var SummonBear8Icon = AssetLoader.LoadInternal("Skills", "Icon_SummonBear8.jpg");

            var GrizzlyBearSummoned = Helpers.CreateBlueprint<BlueprintUnit>("GrizzlyBearSummoned", bp => {
                bp.SetLocalisedName("Summoned Grizzly");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                    c.Levels = 5;
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Unknown;
                    c.Skills = new StatType[] { StatType.SkillPerception | StatType.SkillAthletics };
                    c.Selections = new SelectionEntry[] {
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                GreatFortitudeFeat,
                                ToughnessFeat,
                                IronWillFeat,
                                ImprovedInitiativeFeat,
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
                                SkillFocusPerseptionFeat
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

                bp.Gender = Gender.Male;
                bp.Size = Size.Large;
                bp.Color = GreaslyBearSummon.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = BearPortait.ToReference<BlueprintPortraitReference>();
                bp.Prefab = new UnitViewLink { AssetId = "5c04c52b936e15d4baf3688f0d1b99d3" }; //BearDireElite prefab
                bp.Visual = new UnitVisualParams() {
                    BloodType = BloodType.Common,
                    FootprintType = FootprintType.AnimalPaw,
                    FootprintScale = 1,
                    ArmorFx = new PrefabLink(),
                    BloodPuddleFx = new PrefabLink(),
                    DismemberFx = new PrefabLink(),
                    RipLimbsApartFx = new PrefabLink(),
                    IsNotUseDismember = false,
                    m_Barks = BearBarks.ToReference<BlueprintUnitAsksListReference>(),
                    ReachFXThresholdBonus = 0,
                    DefaultArmorSoundType = ArmorSoundType.Flesh,
                    FootstepSoundSizeType = FootstepSoundSizeType.BootMedium,
                    FootSoundType = FootSoundType.HardPaw,
                    FootSoundSize = Size.Medium,
                    BodySoundType = BodySoundType.Flesh,
                    BodySoundSize = Size.Medium,
                    FoleySoundPrefix = null, //?
                    NoFinishingBlow = false,
                    ImportanceOverride = 0,
                    SilentCaster = true
                };
                bp.m_Faction = Summoned.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = GreaslyBearSummon.FactionOverrides;
                bp.m_Brain = DumbMosterBrain.ToReference<BlueprintBrainReference>();
                bp.Body = new BlueprintUnit.UnitBody() {
                    DisableHands = true,
                    m_EmptyHandWeapon = new BlueprintItemWeaponReference(),
                    m_PrimaryHand = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHand = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative3 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative3 = new BlueprintItemEquipmentHandReference(),
                    ActiveHandSet = 0,
                    m_AdditionalLimbs = new BlueprintItemWeaponReference[] {
                        Bite1d6.ToReference<BlueprintItemWeaponReference>(),
                        ClawLarge1d6.ToReference<BlueprintItemWeaponReference>(),
                        ClawLarge1d6.ToReference<BlueprintItemWeaponReference>()
                    }
                };
                bp.Strength = 21;
                bp.Dexterity = 13;
                bp.Constitution = 19;
                bp.Wisdom = 12;
                bp.Intelligence = 2;
                bp.Charisma = 6;
                bp.Speed = new Feet(40);
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
                    NaturalArmor6.ToReference<BlueprintUnitFactReference>(),
                    ReducedReach.ToReference<BlueprintUnitFactReference>(),
                    SubtypeExtraplanar.ToReference<BlueprintUnitFactReference>(),
                    TotemCreature.ToReference<BlueprintUnitFactReference>()
                };
            });
            var GrizzlyBearYoungSummoned = Helpers.CreateBlueprint<BlueprintUnit>("GrizzlyBearYoungSummoned", bp => {
                bp.SetLocalisedName("Summoned Young Grizzly");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                    c.Levels = 5;
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Unknown;
                    c.Skills = new StatType[] { StatType.SkillPerception | StatType.SkillAthletics };
                    c.Selections = new SelectionEntry[] {
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                GreatFortitudeFeat,
                                ToughnessFeat,
                                IronWillFeat,
                                ImprovedInitiativeFeat,
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
                                SkillFocusPerseptionFeat
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

                bp.Gender = Gender.Male;
                bp.Size = Size.Medium;
                bp.Color = GreaslyBearSummon.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = BearPortait.ToReference<BlueprintPortraitReference>();
                bp.Prefab = new UnitViewLink { AssetId = "c21428f352f90c44288f0a758a64d8cb" }; //Petbear prefab
                bp.Visual = new UnitVisualParams() {
                    BloodType = BloodType.Common,
                    FootprintType = FootprintType.AnimalPaw,
                    FootprintScale = 1,
                    ArmorFx = new PrefabLink(),
                    BloodPuddleFx = new PrefabLink(),
                    DismemberFx = new PrefabLink(),
                    RipLimbsApartFx = new PrefabLink(),
                    IsNotUseDismember = false,
                    m_Barks = BearBarks.ToReference<BlueprintUnitAsksListReference>(),
                    ReachFXThresholdBonus = 0,
                    DefaultArmorSoundType = ArmorSoundType.Flesh,
                    FootstepSoundSizeType = FootstepSoundSizeType.BootMedium,
                    FootSoundType = FootSoundType.HardPaw,
                    FootSoundSize = Size.Medium,
                    BodySoundType = BodySoundType.Flesh,
                    BodySoundSize = Size.Medium,
                    FoleySoundPrefix = null, //?
                    NoFinishingBlow = false,
                    ImportanceOverride = 0,
                    SilentCaster = true
                };
                bp.m_Faction = Summoned.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = GreaslyBearSummon.FactionOverrides;
                bp.m_Brain = DumbMosterBrain.ToReference<BlueprintBrainReference>();
                bp.Body = new BlueprintUnit.UnitBody() {
                    DisableHands = true,
                    m_EmptyHandWeapon = new BlueprintItemWeaponReference(),
                    m_PrimaryHand = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHand = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative3 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative3 = new BlueprintItemEquipmentHandReference(),
                    ActiveHandSet = 0,
                    m_AdditionalLimbs = new BlueprintItemWeaponReference[] {
                        Bite1d4.ToReference<BlueprintItemWeaponReference>(),
                        Claw1d4.ToReference<BlueprintItemWeaponReference>(),
                        Claw1d4.ToReference<BlueprintItemWeaponReference>()
                    }
                };
                bp.Strength = 17;
                bp.Dexterity = 17;
                bp.Constitution = 15;
                bp.Wisdom = 12;
                bp.Intelligence = 2;
                bp.Charisma = 6;
                bp.Speed = new Feet(40);
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
                    NaturalArmor4.ToReference<BlueprintUnitFactReference>(),
                    SubtypeExtraplanar.ToReference<BlueprintUnitFactReference>(),
                    TotemCreature.ToReference<BlueprintUnitFactReference>()
                };
            });
            var GrizzlyBearAdvancedSummoned = Helpers.CreateBlueprint<BlueprintUnit>("GrizzlyBearAdvancedSummoned", bp => {
                bp.SetLocalisedName("Summoned Advanced Grizzly");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                    c.Levels = 5;
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Unknown;
                    c.Skills = new StatType[] { StatType.SkillPerception | StatType.SkillAthletics };
                    c.Selections = new SelectionEntry[] {
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                GreatFortitudeFeat,
                                ToughnessFeat,
                                IronWillFeat,
                                ImprovedInitiativeFeat,
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
                                SkillFocusPerseptionFeat
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

                bp.Gender = Gender.Male;
                bp.Size = Size.Large;
                bp.Color = GreaslyBearSummon.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = BearPortait.ToReference<BlueprintPortraitReference>();
                bp.Prefab = new UnitViewLink { AssetId = "5c04c52b936e15d4baf3688f0d1b99d3" }; //BearDireElite prefab
                bp.Visual = new UnitVisualParams() {
                    BloodType = BloodType.Common,
                    FootprintType = FootprintType.AnimalPaw,
                    FootprintScale = 1,
                    ArmorFx = new PrefabLink(),
                    BloodPuddleFx = new PrefabLink(),
                    DismemberFx = new PrefabLink(),
                    RipLimbsApartFx = new PrefabLink(),
                    IsNotUseDismember = false,
                    m_Barks = BearBarks.ToReference<BlueprintUnitAsksListReference>(),
                    ReachFXThresholdBonus = 0,
                    DefaultArmorSoundType = ArmorSoundType.Flesh,
                    FootstepSoundSizeType = FootstepSoundSizeType.BootMedium,
                    FootSoundType = FootSoundType.HardPaw,
                    FootSoundSize = Size.Medium,
                    BodySoundType = BodySoundType.Flesh,
                    BodySoundSize = Size.Medium,
                    FoleySoundPrefix = null, //?
                    NoFinishingBlow = false,
                    ImportanceOverride = 0,
                    SilentCaster = true
                };
                bp.m_Faction = Summoned.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = GreaslyBearSummon.FactionOverrides;
                bp.m_Brain = DumbMosterBrain.ToReference<BlueprintBrainReference>();
                bp.Body = new BlueprintUnit.UnitBody() {
                    DisableHands = true,
                    m_EmptyHandWeapon = new BlueprintItemWeaponReference(),
                    m_PrimaryHand = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHand = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative3 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative3 = new BlueprintItemEquipmentHandReference(),
                    ActiveHandSet = 0,
                    m_AdditionalLimbs = new BlueprintItemWeaponReference[] {
                        Bite1d6.ToReference<BlueprintItemWeaponReference>(),
                        ClawLarge1d6.ToReference<BlueprintItemWeaponReference>(),
                        ClawLarge1d6.ToReference<BlueprintItemWeaponReference>()
                    }
                };
                bp.Strength = 25;
                bp.Dexterity = 17;
                bp.Constitution = 23;
                bp.Wisdom = 16;
                bp.Intelligence = 2;
                bp.Charisma = 10;
                bp.Speed = new Feet(40);
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
                    NaturalArmor8.ToReference<BlueprintUnitFactReference>(),
                    ReducedReach.ToReference<BlueprintUnitFactReference>(),
                    SubtypeExtraplanar.ToReference<BlueprintUnitFactReference>(),
                    TotemCreature.ToReference<BlueprintUnitFactReference>()
                };
            });
            var GrizzlyBearGiantSummoned = Helpers.CreateBlueprint<BlueprintUnit>("GrizzlyBearGiantSummoned", bp => {
                bp.SetLocalisedName("Summoned Giant Grizzly");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                    c.Levels = 5;
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Unknown;
                    c.Skills = new StatType[] { StatType.SkillPerception | StatType.SkillAthletics };
                    c.Selections = new SelectionEntry[] {
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                GreatFortitudeFeat,
                                ToughnessFeat,
                                IronWillFeat,
                                ImprovedInitiativeFeat,
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
                                SkillFocusPerseptionFeat
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

                bp.Gender = Gender.Male;
                bp.Size = Size.Large;
                bp.Color = GreaslyBearSummon.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = BearPortait.ToReference<BlueprintPortraitReference>();
                bp.Prefab = new UnitViewLink { AssetId = "5c04c52b936e15d4baf3688f0d1b99d3" }; //BearDireElite prefab
                bp.Visual = new UnitVisualParams() {
                    BloodType = BloodType.Common,
                    FootprintType = FootprintType.AnimalPaw,
                    FootprintScale = 1,
                    ArmorFx = new PrefabLink(),
                    BloodPuddleFx = new PrefabLink(),
                    DismemberFx = new PrefabLink(),
                    RipLimbsApartFx = new PrefabLink(),
                    IsNotUseDismember = false,
                    m_Barks = BearBarks.ToReference<BlueprintUnitAsksListReference>(),
                    ReachFXThresholdBonus = 0,
                    DefaultArmorSoundType = ArmorSoundType.Flesh,
                    FootstepSoundSizeType = FootstepSoundSizeType.BootMedium,
                    FootSoundType = FootSoundType.HardPaw,
                    FootSoundSize = Size.Medium,
                    BodySoundType = BodySoundType.Flesh,
                    BodySoundSize = Size.Medium,
                    FoleySoundPrefix = null, //?
                    NoFinishingBlow = false,
                    ImportanceOverride = 0,
                    SilentCaster = true
                };
                bp.m_Faction = Summoned.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = GreaslyBearSummon.FactionOverrides;
                bp.m_Brain = DumbMosterBrain.ToReference<BlueprintBrainReference>();
                bp.Body = new BlueprintUnit.UnitBody() {
                    DisableHands = true,
                    m_EmptyHandWeapon = new BlueprintItemWeaponReference(),
                    m_PrimaryHand = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHand = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative3 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative3 = new BlueprintItemEquipmentHandReference(),
                    ActiveHandSet = 0,
                    m_AdditionalLimbs = new BlueprintItemWeaponReference[] {
                        Bite1d6.ToReference<BlueprintItemWeaponReference>(),
                        ClawLarge1d6.ToReference<BlueprintItemWeaponReference>(),
                        ClawLarge1d6.ToReference<BlueprintItemWeaponReference>()
                    }
                };
                bp.Strength = 25;
                bp.Dexterity = 11;
                bp.Constitution = 23;
                bp.Wisdom = 12;
                bp.Intelligence = 2;
                bp.Charisma = 6;
                bp.Speed = new Feet(40);
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
                    NaturalArmor9.ToReference<BlueprintUnitFactReference>(),
                    ReducedReach.ToReference<BlueprintUnitFactReference>(),
                    SubtypeExtraplanar.ToReference<BlueprintUnitFactReference>(),
                    SizeIncreasePlus1.ToReference<BlueprintUnitFactReference>(),
                    TotemCreature.ToReference<BlueprintUnitFactReference>()
                };
            });
            var GrizzlyBearGiantAdvancedSummoned = Helpers.CreateBlueprint<BlueprintUnit>("GrizzlyBearGiantAdvancedSummoned", bp => {
                bp.SetLocalisedName("Summoned Giant Advanced Grizzly");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                    c.Levels = 5;
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Unknown;
                    c.Skills = new StatType[] { StatType.SkillPerception | StatType.SkillAthletics };
                    c.Selections = new SelectionEntry[] {
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                GreatFortitudeFeat,
                                ToughnessFeat,
                                IronWillFeat,
                                ImprovedInitiativeFeat,
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
                                SkillFocusPerseptionFeat
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

                bp.Gender = Gender.Male;
                bp.Size = Size.Large;
                bp.Color = GreaslyBearSummon.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = BearPortait.ToReference<BlueprintPortraitReference>();
                bp.Prefab = new UnitViewLink { AssetId = "5c04c52b936e15d4baf3688f0d1b99d3" }; //BearDireElite prefab
                bp.Visual = new UnitVisualParams() {
                    BloodType = BloodType.Common,
                    FootprintType = FootprintType.AnimalPaw,
                    FootprintScale = 1,
                    ArmorFx = new PrefabLink(),
                    BloodPuddleFx = new PrefabLink(),
                    DismemberFx = new PrefabLink(),
                    RipLimbsApartFx = new PrefabLink(),
                    IsNotUseDismember = false,
                    m_Barks = BearBarks.ToReference<BlueprintUnitAsksListReference>(),
                    ReachFXThresholdBonus = 0,
                    DefaultArmorSoundType = ArmorSoundType.Flesh,
                    FootstepSoundSizeType = FootstepSoundSizeType.BootMedium,
                    FootSoundType = FootSoundType.HardPaw,
                    FootSoundSize = Size.Medium,
                    BodySoundType = BodySoundType.Flesh,
                    BodySoundSize = Size.Medium,
                    FoleySoundPrefix = null, //?
                    NoFinishingBlow = false,
                    ImportanceOverride = 0,
                    SilentCaster = true
                };
                bp.m_Faction = Summoned.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = GreaslyBearSummon.FactionOverrides;
                bp.m_Brain = DumbMosterBrain.ToReference<BlueprintBrainReference>();
                bp.Body = new BlueprintUnit.UnitBody() {
                    DisableHands = true,
                    m_EmptyHandWeapon = new BlueprintItemWeaponReference(),
                    m_PrimaryHand = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHand = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative3 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative3 = new BlueprintItemEquipmentHandReference(),
                    ActiveHandSet = 0,
                    m_AdditionalLimbs = new BlueprintItemWeaponReference[] {
                        Bite1d6.ToReference<BlueprintItemWeaponReference>(),
                        ClawLarge1d6.ToReference<BlueprintItemWeaponReference>(),
                        ClawLarge1d6.ToReference<BlueprintItemWeaponReference>()
                    }
                };
                bp.Strength = 29;
                bp.Dexterity = 15;
                bp.Constitution = 27;
                bp.Wisdom = 16;
                bp.Intelligence = 2;
                bp.Charisma = 10;
                bp.Speed = new Feet(40);
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
                    NaturalArmor11.ToReference<BlueprintUnitFactReference>(),
                    ReducedReach.ToReference<BlueprintUnitFactReference>(),
                    SubtypeExtraplanar.ToReference<BlueprintUnitFactReference>(),
                    SizeIncreasePlus1.ToReference<BlueprintUnitFactReference>(),
                    TotemCreature.ToReference<BlueprintUnitFactReference>()
                };
            });

            var SummonBearStandardSingle = Helpers.CreateBlueprint<BlueprintAbility>("SummonBearStandardSingle", bp => {
                bp.SetName("Summon Grizzly Bear");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side a natural grizzly bear. The summoned ally appears where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = GrizzlyBearSummoned.ToReference<BlueprintUnitReference>(),
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
                bp.m_Icon = SummonBear4Icon;
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
                bp.LocalizedDuration = Helpers.CreateString("SummonBearStandardSingle.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonBearYoungSingle = Helpers.CreateBlueprint<BlueprintAbility>("SummonBearYoungSingle", bp => {
                bp.SetName("Summon Young Grizzly Bear");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side a natural young grizzly bear. The summoned ally appears where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nYoung: Creature " +
                    "is one size category smaller, has natural armor decreased by 2, gains a -4 penalty to strength and constitution, and a +4 bonus to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = GrizzlyBearYoungSummoned.ToReference<BlueprintUnitReference>(),
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
                bp.m_Icon = SummonBear3Icon;
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
                bp.LocalizedDuration = Helpers.CreateString("SummonBearYoungSingle.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonBearGiantSingle = Helpers.CreateBlueprint<BlueprintAbility>("SummonBearGiantSingle", bp => {
                bp.SetName("Summon Giant Grizzly Bear");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side a natural giant grizzly bear. The summoned ally appears where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nGiant: Creature " +
                    "is one size category larger, has natural armor increased by 3, gains a +4 bonus to strength and constitution, and a -2 penalty to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = GrizzlyBearGiantSummoned.ToReference<BlueprintUnitReference>(),
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
                bp.m_Icon = SummonBear5Icon;
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
                bp.LocalizedDuration = Helpers.CreateString("SummonBearGiantSingle.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonBearAdvancedSingle = Helpers.CreateBlueprint<BlueprintAbility>("SummonBearAdvancedSingle", bp => {
                bp.SetName("Summon Advanced Grizzly Bear");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side a natural advanced grizzly bear. The summoned ally appears where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nAdvanced: Creature " +
                    "has natural armor increased by 2, gains a +4 bonus all ability scores (except Int scores of 2 or less).");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = GrizzlyBearAdvancedSummoned.ToReference<BlueprintUnitReference>(),
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
                bp.m_Icon = SummonBear5Icon;
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
                bp.LocalizedDuration = Helpers.CreateString("SummonBearAdvancedSingle.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonBearFinalSingle = Helpers.CreateBlueprint<BlueprintAbility>("SummonBearFinalSingle", bp => {
                bp.SetName("Summon Giant Advanced Grizzly Bear");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side a natural giant advanced grizzly bear. The summoned ally appears where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nGiant + Advanced: Creature " +
                    "is one size category larger, has natural armor increased by 5, gains a +8 bonus to strength and constitution, a +4 bonus to wisdom and charisma, and a +2 bonus to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = GrizzlyBearGiantAdvancedSummoned.ToReference<BlueprintUnitReference>(),
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
                bp.m_Icon = SummonBear6Icon;
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
                bp.LocalizedDuration = Helpers.CreateString("SummonBearFinalSingle.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var SummonBearStandardD3 = Helpers.CreateBlueprint<BlueprintAbility>("SummonBearStandardD3", bp => {
                bp.SetName("Summon Grizzly Bear (1d3)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d3{/g} natural young grizzly bears. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = GrizzlyBearSummoned.ToReference<BlueprintUnitReference>(),
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
                bp.m_Icon = SummonBear5Icon;
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
                bp.LocalizedDuration = Helpers.CreateString("SummonBearStandardD3.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonBearYoungD3 = Helpers.CreateBlueprint<BlueprintAbility>("SummonBearYoungD3", bp => {
                bp.SetName("Summon Young Grizzly Bear (1d3)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d3{/g} natural young grizzly bears. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nYoung: Creature " +
                    "is one size category smaller, has natural armor decreased by 2, gains a -4 penalty to strength and constitution, and a +4 bonus to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = GrizzlyBearYoungSummoned.ToReference<BlueprintUnitReference>(),
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
                bp.m_Icon = SummonBear4Icon;
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
                bp.LocalizedDuration = Helpers.CreateString("SummonBearYoungD3.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonBearGiantD3 = Helpers.CreateBlueprint<BlueprintAbility>("SummonBearGiantD3", bp => {
                bp.SetName("Summon Giant Grizzly Bear (1d3)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d3{/g} natural giant grizzly bears. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nGiant: Creature " +
                    "is one size category larger, has natural armor increased by 3, gains a +4 bonus to strength and constitution, and a -2 penalty to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = GrizzlyBearGiantSummoned.ToReference<BlueprintUnitReference>(),
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
                bp.m_Icon = SummonBear6Icon;
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
                bp.LocalizedDuration = Helpers.CreateString("SummonBearGiantD3.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonBearAdvancedD3 = Helpers.CreateBlueprint<BlueprintAbility>("SummonBearAdvancedD3", bp => {
                bp.SetName("Summon Advanced Grizzly Bear (1d3)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d3{/g} natural advanced grizzly bears. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nAdvanced: Creature " +
                    "has natural armor increased by 2, gains a +4 bonus all ability scores (except Int scores of 2 or less).");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = GrizzlyBearAdvancedSummoned.ToReference<BlueprintUnitReference>(),
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
                bp.m_Icon = SummonBear6Icon;
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
                bp.LocalizedDuration = Helpers.CreateString("SummonBearAdvancedD3.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonBearFinalD3 = Helpers.CreateBlueprint<BlueprintAbility>("SummonBearFinalD3", bp => {
                bp.SetName("Summon Giant Advanced Grizzly Bear (1d3)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d3{/g} natural giant advanced grizzly bears. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nGiant + Advanced: Creature " +
                    "is one size category larger, has natural armor increased by 5, gains a +8 bonus to strength and constitution, a +4 bonus to wisdom and charisma, and a +2 bonus to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = GrizzlyBearAdvancedSummoned.ToReference<BlueprintUnitReference>(),
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
                bp.m_Icon = SummonBear7Icon;
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
                bp.LocalizedDuration = Helpers.CreateString("SummonBearFinalD3.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var SummonBearStandardD4plus1 = Helpers.CreateBlueprint<BlueprintAbility>("SummonBearStandardD4plus1", bp => {
                bp.SetName("Summon Grizzly Bear (1d4 + 1)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d4{/g} + 1 natural young grizzly bears. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = GrizzlyBearSummoned.ToReference<BlueprintUnitReference>(),
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
                bp.m_Icon = SummonBear6Icon;
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
                bp.LocalizedDuration = Helpers.CreateString("SummonBearStandardD4plus1.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonBearYoungD4plus1 = Helpers.CreateBlueprint<BlueprintAbility>("SummonBearYoungD4plus1", bp => {
                bp.SetName("Summon Young Grizzly Bear (1d4 + 1)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d4{/g} + 1 natural young grizzly bears. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nYoung: Creature " +
                    "is one size category smaller, has natural armor decreased by 2, gains a -4 penalty to strength and constitution, and a +4 bonus to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = GrizzlyBearYoungSummoned.ToReference<BlueprintUnitReference>(),
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
                bp.m_Icon = SummonBear5Icon;
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
                bp.LocalizedDuration = Helpers.CreateString("SummonBearYoungD4plus1.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonBearGiantD4plus1 = Helpers.CreateBlueprint<BlueprintAbility>("SummonBearGiantD4plus1", bp => {
                bp.SetName("Summon Giant Grizzly Bear (1d4 + 1)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d4{/g} + 1 natural giant grizzly bears. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nGiant: Creature " +
                    "is one size category larger, has natural armor increased by 3, gains a +4 bonus to strength and constitution, and a -2 penalty to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = GrizzlyBearGiantSummoned.ToReference<BlueprintUnitReference>(),
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
                bp.m_Icon = SummonBear7Icon;
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
                bp.LocalizedDuration = Helpers.CreateString("SummonBearGiantD4plus1.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonBearAdvancedD4plus1 = Helpers.CreateBlueprint<BlueprintAbility>("SummonBearAdvancedD4plus1", bp => {
                bp.SetName("Summon Advanced Grizzly Bear (1d4 + 1)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d4{/g} + 1 natural advanced grizzly bears. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nAdvanced: Creature " +
                    "has natural armor increased by 2, gains a +4 bonus all ability scores (except Int scores of 2 or less).");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = GrizzlyBearAdvancedSummoned.ToReference<BlueprintUnitReference>(),
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
                bp.m_Icon = SummonBear7Icon;
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
                bp.LocalizedDuration = Helpers.CreateString("SummonBearAdvancedD4plus1.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonBearFinalD4plus1 = Helpers.CreateBlueprint<BlueprintAbility>("SummonBearFinalD4plus1", bp => {
                bp.SetName("Summon Giant Advanced Grizzly Bear (1d4 + 1)");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} summons to your side {g|Encyclopedia:Dice}1d4{/g} + 1 natural giant advanced grizzly bears. The summoned allies appear where you designate and acts according to its " +
                    "{g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}check{/g} results. It {g|Encyclopedia:Attack}attacks{/g} your opponents to the best of its ability.\nGiant + Advanced: Creature " +
                    "is one size category larger, has natural armor increased by 5, gains a +8 bonus to strength and constitution, a +4 bonus to wisdom and charisma, and a +2 bonus to dexterity.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnMonster() {
                            m_Blueprint = GrizzlyBearAdvancedSummoned.ToReference<BlueprintUnitReference>(),
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
                bp.m_Icon = SummonBear8Icon;
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
                bp.LocalizedDuration = Helpers.CreateString("SummonBearFinalD4plus1.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });


            var BearShamanTotemicSummonsTempHitPoints = Helpers.CreateBuff("BearShamanTotemicSummonsTempHitPoints", bp => {
                bp.SetName("Totemic Ally Temporary Hit Points");
                bp.SetDescription("A bear totem druid may cast summon nature’s ally as a standard action when summoning bears, and summoned bears gain temporary hit points equal to their druid level.");
                bp.m_Icon = BearShamanTotemTransformationIcon;
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

            var BearShamanTotemicSummonsFeature = Helpers.CreateBlueprint<BlueprintFeature>("BearShamanTotemicSummonsFeature", bp => {
                bp.SetName("Totemic Summons");
                bp.SetDescription("At 5th level, a bear totem druid may cast summon nature’s ally as a standard action when summoning bears, and summoned bears gain temporary hit points equal to their druid level. " +
                    "They can apply the young template to any bear to reduce the level of the summoning spell required by one. They can also increase the level of summoning required by one in order to apply either " +
                    "the advanced or the giant template, or increase it by two to apply both the advanced and giant templates.");
                bp.AddComponent<OnSpawnBuff>(c => {
                    c.m_IfHaveFact = BearShamanTotemTransformationFeature.ToReference<BlueprintFeatureReference>();
                    c.CheckSummonedUnitFact = true;
                    c.m_IfSummonHaveFact = TotemCreature.ToReference<BlueprintFeatureReference>();
                    c.m_buff = BearShamanTotemicSummonsTempHitPoints.ToReference<BlueprintBuffReference>();
                    c.IsInfinity = true;
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonBearYoungSingle.ToReference<BlueprintAbilityReference>(),
                        SummonBearStandardSingle.ToReference<BlueprintAbilityReference>(),
                        SummonBearGiantSingle.ToReference<BlueprintAbilityReference>(),
                        SummonBearFinalSingle.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
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
                        new BlueprintAbilityReference(),
                        SummonBearYoungD3.ToReference<BlueprintAbilityReference>(),
                        SummonBearStandardD3.ToReference<BlueprintAbilityReference>(),
                        SummonBearGiantD3.ToReference<BlueprintAbilityReference>(),
                        SummonBearFinalD3.ToReference<BlueprintAbilityReference>(),
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
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonBearYoungD4plus1.ToReference<BlueprintAbilityReference>(),
                        SummonBearStandardD4plus1.ToReference<BlueprintAbilityReference>(),
                        SummonBearGiantD4plus1.ToReference<BlueprintAbilityReference>(),
                        SummonBearFinalD4plus1.ToReference<BlueprintAbilityReference>(),
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
                        new BlueprintAbilityReference(),
                        SummonBearAdvancedSingle.ToReference<BlueprintAbilityReference>(),
                        SummonBearAdvancedD3.ToReference<BlueprintAbilityReference>(),
                        SummonBearAdvancedD4plus1.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
            });

            //Adding Bear Shaman to Domain Configs            
            //Protection Domain Hooks - Class added in Urban druid, this is only the archetype
            var ProtectionDomainBaseBuffConfig = Resources.GetBlueprint<BlueprintBuff>("2ddb4cfc3cfd04c46a66c6cd26df1c06").GetComponent<ContextRankConfig>();
            ProtectionDomainBaseBuffConfig.m_AdditionalArchetypes = ProtectionDomainBaseBuffConfig.m_AdditionalArchetypes.AppendToArray(BearShamanArchetype.ToReference<BlueprintArchetypeReference>());
            var ProtectionDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("c5815bd0bf87bdb4fa9c440c8088149b").GetComponent<ContextRankConfig>();
            ProtectionDomainBaseAbilityConfig.m_AdditionalArchetypes = ProtectionDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(BearShamanArchetype.ToReference<BlueprintArchetypeReference>());            
            var ProtectionDomainBaseSelfBuffConfig = Resources.GetBlueprint<BlueprintBuff>("74a4fb45f23705d4db2784d16eb93138").GetComponent<ContextRankConfig>();
            ProtectionDomainBaseSelfBuffConfig.m_AdditionalArchetypes = ProtectionDomainBaseSelfBuffConfig.m_AdditionalArchetypes.AppendToArray(BearShamanArchetype.ToReference<BlueprintArchetypeReference>());
            var ProtectionDomainGreaterEffect = Resources.GetBlueprint<BlueprintBuff>("fea7c44605c90f14fa40b2f2f5ae6339");
            ProtectionDomainGreaterEffect.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_AdditionalArchetypes = c.m_AdditionalArchetypes.AppendToArray(BearShamanArchetype.ToReference<BlueprintArchetypeReference>());
            });
            var ProtectionDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("f3d878f77d0ee854b864f5ea1c80e752");
            ProtectionDomainGreaterResource.m_MaxAmount.m_Archetypes = ProtectionDomainGreaterResource.m_MaxAmount.m_Archetypes.AppendToArray(BearShamanArchetype.ToReference<BlueprintArchetypeReference>());
            //Strength Domain Hooks
            var StrengthDomainBaseBuffConfig = Resources.GetBlueprint<BlueprintBuff>("94dfcf5f3a72ce8478c8de5db69e752b").GetComponent<ContextRankConfig>();
            StrengthDomainBaseBuffConfig.m_AdditionalArchetypes = StrengthDomainBaseBuffConfig.m_AdditionalArchetypes.AppendToArray(BearShamanArchetype.ToReference<BlueprintArchetypeReference>());
            StrengthDomainBaseBuffConfig.m_Class = StrengthDomainBaseBuffConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            var StrengthDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("1d6364123e1f6a04c88313d83d3b70ee").GetComponent<ContextRankConfig>();
            StrengthDomainBaseAbilityConfig.m_AdditionalArchetypes = StrengthDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(BearShamanArchetype.ToReference<BlueprintArchetypeReference>());
            StrengthDomainBaseAbilityConfig.m_Class = StrengthDomainBaseAbilityConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());            
            var StrengthDomainGreaterFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("3298fd30e221ef74189a06acbf376d29").GetComponent<ContextRankConfig>();
            StrengthDomainGreaterFeatureConfig.m_AdditionalArchetypes = StrengthDomainGreaterFeatureConfig.m_AdditionalArchetypes.AppendToArray(BearShamanArchetype.ToReference<BlueprintArchetypeReference>());
            StrengthDomainGreaterFeatureConfig.m_Class = StrengthDomainGreaterFeatureConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());


            BearShamanArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, DruidBondSelection),
                    Helpers.LevelEntry(4, ResistNaturesLureFeature),
                    Helpers.LevelEntry(6, WildShapeIILeopardFeature, WildShapeElementalSmallFeature),
                    Helpers.LevelEntry(8, WildShapeIVBearFeature, WildShapeElementalFeatureAddMediumFeature),
                    Helpers.LevelEntry(9, VenomImmunityFeature),
                    Helpers.LevelEntry(10, WildShapeIIISmilodonFeature, WildShapeElementalFeatureAddLargeFeature, WildShapeIVShamblingMoundFeature),
                    Helpers.LevelEntry(12, WildShapeElementalHugeFeatureFeature)
            };
            BearShamanArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, BearShamanBondSelection),
                    Helpers.LevelEntry(2, BearShamanTotemTransformationFeature),
                    Helpers.LevelEntry(5, BearShamanTotemicSummonsFeature),
                    Helpers.LevelEntry(6, BearShamanWildShape, WildShapeIVBearFeature),
                    Helpers.LevelEntry(7, BearShamanTotemTransformationMoveFeature),
                    Helpers.LevelEntry(8, WildShapeIILeopardFeature, WildShapeElementalSmallFeature),
                    Helpers.LevelEntry(9, BearShamanBonusFeatSelection),
                    Helpers.LevelEntry(10, WildShapeElementalFeatureAddMediumFeature),
                    Helpers.LevelEntry(12, WildShapeIIISmilodonFeature, WildShapeElementalFeatureAddLargeFeature, WildShapeIVShamblingMoundFeature, BearShamanTotemTransformationSwiftFeature),
                    Helpers.LevelEntry(13, BearShamanBonusFeatSelection),
                    Helpers.LevelEntry(14, WildShapeElementalHugeFeatureFeature),
                    Helpers.LevelEntry(17, BearShamanBonusFeatSelection)
            };
            DruidClass.Progression.UIGroups = DruidClass.Progression.UIGroups.AppendToArray(
                Helpers.CreateUIGroup(BearShamanTotemTransformationFeature, BearShamanTotemicSummonsFeature, BearShamanTotemTransformationMoveFeature, BearShamanTotemTransformationSwiftFeature)
            );
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Bear Shaman")) { return; }
            DruidClass.m_Archetypes = DruidClass.m_Archetypes.AppendToArray(BearShamanArchetype.ToReference<BlueprintArchetypeReference>());

        }
    }
}
