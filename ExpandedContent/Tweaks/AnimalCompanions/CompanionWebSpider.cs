using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.AI.Blueprints;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Utility;
using Kingmaker.Visual.HitSystem;
using Kingmaker.Visual.Sound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExpandedContent.Tweaks.AnimalCompanions {
    internal class CompanionWebSpider {
        public static void AddCompanionWebSpider() {

            var AnimalCompanionClass = Resources.GetBlueprint<BlueprintCharacterClass>("26b10d4340839004f960f9816f6109fe");
            var HeadLocatorFeature = Resources.GetBlueprint<BlueprintFeature>("9c57e9674b4a4a2b9920f9fec47f7e6a");
            var SpiderBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("7d340f75a57c47d45b0e79200a6b5eac");
            var MediumWebSpider = Resources.GetBlueprint<BlueprintUnit>("9b43934530f94d03a445055a36b081db");
            var AzataDragonUnit = Resources.GetBlueprint<BlueprintUnit>("32a037e97c3d5c54b85da8f639616c57");
            var CharacterBrain = Resources.GetBlueprint<BlueprintBrain>("cf986dd7ba9d4ec46ad8a3a0406d02ae");
            var Neutrals = Resources.GetBlueprint<BlueprintFaction>("d8de50cc80eb4dc409a983991e0b77ad");
            var WeaponEmptyHand = Resources.GetBlueprint<BlueprintItemWeapon>("20375b5a0c9243d45966bd72c690ab74");
            var Bite1d8 = Resources.GetBlueprint<BlueprintItemWeapon>("61bc14eca5f8c1040900215000cfc218");
            var NaturalArmor6 = Resources.GetBlueprint<BlueprintUnitFact>("987ba44303e88054c9504cb3083ba0c9");
            var NaturalArmor5 = Resources.GetBlueprint<BlueprintUnitFact>("7661741dbb9604842a642457456fd0e4");
            var AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");
            var TripDefenceEightLegs = Resources.GetBlueprint<BlueprintFeature>("a60900c666b2b37478a2bf4bb005973d");
            var VerminType = Resources.GetBlueprint<BlueprintFeature>("09478937695300944a179530664e42ec");
            var AnimalCompanionSlotFeature = Resources.GetBlueprint<BlueprintFeature>("75bb2b3c41c99e041b4743fdb16a4289");
            var SpiderWebImmunity = Resources.GetBlueprint<BlueprintFeature>("3051e7002c803fc47a11bcfa381b9fbd");




            var AnimalCompanionFeatureCentipede = Resources.GetBlueprint<BlueprintFeature>("f9ef7717531f5914a9b6ecacfad63f46");

            var UnmountableFeature = Resources.GetModBlueprint<BlueprintFeature>("UnmountableFeature");

            var WebArea = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("fd323c05f76390749a8555b13156813d");

            var CompanionWebSpiderWebResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("CompanionWebSpiderWebResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Constitution,
                };
            });
            var CompanionWebSpiderWebAbility = Helpers.CreateBlueprint<BlueprintAbility>("CompanionWebSpiderWebAbility", bp => {
                bp.SetName("Spiders Web");
                bp.SetDescription("Web creates a many-layered mass of {g|Encyclopedia:Strength}strong{/g}, sticky strands. These strands trap those caught in them. Creatures caught within a web become grappled by " +
                    "the sticky fibers.\nAnyone in the effect's area when spider's web is cast must make a {g|Encyclopedia:Saving_Throw}Reflex save{/g}. If this save succeeds, the creature is inside the web but is " +
                    "otherwise unaffected. If the save fails, the creature gains the grappled condition, but can break free by making a {g|Encyclopedia:Combat_Maneuvers}combat maneuver{/g} {g|Encyclopedia:Check}check{/g}" +
                    ", {g|Encyclopedia:Athletics}Athletics check{/g}, or {g|Encyclopedia:Mobility}Mobility check{/g} as a {g|Encyclopedia:Standard_Actions}standard action{/g} against the {g|Encyclopedia:DC}DC{/g} of this " +
                    "ability. The entire area of the web is considered difficult terrain. Anyone moving through the webs must make a Reflex save each {g|Encyclopedia:Combat_Round}round{/g}. Creatures that fail lose their " +
                    "movement and become grappled in the first square of webbing that they enter. Spiders are immune to this ability. \nYou can use this ability a number of times per day equal to 3 + your Constitution modifier.");
                bp.m_Icon = AnimalCompanionFeatureCentipede.Icon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Close;
                bp.CanTargetEnemies = false;
                bp.CanTargetPoint = true;
                bp.CanTargetFriends = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "1 minute");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "Reflex");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = CompanionWebSpiderWebResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnAreaEffect() {
                            m_AreaEffect = WebArea.ToReference<BlueprintAbilityAreaEffectReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            }
                        }
                        );
                });
                bp.AddComponent<AbilityAoERadius>(c => {
                    c.m_Radius = new Feet() { m_Value = 20 };
                    c.m_TargetType = Kingmaker.UnitLogic.Abilities.Components.TargetType.Any;
                    c.m_CanBeUsedInTacticalCombat = false;
                    c.m_DiameterInCells = 0;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Ground | SpellDescriptor.MovementImpairing;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Reflex;
                    c.AOEType = CraftAOE.AOE;
                    c.SpellType = CraftSpellType.Debuff;
                });
            });
            var CompanionWebSpiderWebFeature = Helpers.CreateBlueprint<BlueprintFeature>("CompanionWebSpiderWebFeature", bp => {
                bp.SetName("Spiders Web");
                bp.SetDescription("Web creates a many-layered mass of {g|Encyclopedia:Strength}strong{/g}, sticky strands. These strands trap those caught in them. Creatures caught within a web become grappled by " +
                    "the sticky fibers.\nAnyone in the effect's area when spider's web is cast must make a {g|Encyclopedia:Saving_Throw}Reflex save{/g}. If this save succeeds, the creature is inside the web but is " +
                    "otherwise unaffected. If the save fails, the creature gains the grappled condition, but can break free by making a {g|Encyclopedia:Combat_Maneuvers}combat maneuver{/g} {g|Encyclopedia:Check}check{/g}" +
                    ", {g|Encyclopedia:Athletics}Athletics check{/g}, or {g|Encyclopedia:Mobility}Mobility check{/g} as a {g|Encyclopedia:Standard_Actions}standard action{/g} against the {g|Encyclopedia:DC}DC{/g} of this " +
                    "ability. The entire area of the web is considered difficult terrain. Anyone moving through the webs must make a Reflex save each {g|Encyclopedia:Combat_Round}round{/g}. Creatures that fail lose their " +
                    "movement and become grappled in the first square of webbing that they enter. Spiders are immune to this ability. \nYou can use this ability a number of times per day equal to 3 + your Constitution modifier.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        CompanionWebSpiderWebAbility.ToReference<BlueprintUnitFactReference>(),
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = CompanionWebSpiderWebResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<BindAbilitiesToClass>(c => {
                    c.m_Abilites = new BlueprintAbilityReference[] { CompanionWebSpiderWebAbility.ToReference<BlueprintAbilityReference>() };
                    c.m_CharacterClass = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Stat = StatType.Constitution;
                    c.LevelStep = 2;
                    c.Odd = false;
                    c.FullCasterChecks = false;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { CompanionWebSpiderWebAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Constitution;
                });
                bp.IsClassFeature = true;
            });

            var CompanionNotUpgradedWebSpider = Helpers.CreateBlueprint<BlueprintFeature>("CompanionNotUpgradedWebSpider", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<ChangeUnitSize>(c => {
                    c.m_Type = ChangeUnitSize.ChangeType.Delta;
                    c.SizeDelta = -1;
                    c.Size = Size.Fine;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        NaturalArmor5.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });
            var CompanionUpgradeWebSpider = Helpers.CreateBlueprint<BlueprintFeature>("CompanionUpgradeWebSpider", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Value = 4;
                    c.Stat = StatType.Strength;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Value = -2;
                    c.Stat = StatType.Dexterity;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Value = 2;
                    c.Stat = StatType.Constitution;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        NaturalArmor6.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddMechanicsFeature>(c => {
                    c.m_Feature = AddMechanicsFeature.MechanicsFeatureType.IterativeNaturalAttacks;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });
            var CompanionUpdateWebSpiderFeature = Helpers.CreateBlueprint<BlueprintFeature>("CompanionUpdateWebSpiderFeature", bp => {
                bp.SetName("");
                bp.SetDescription("");                
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 4;
                    c.m_Feature = CompanionNotUpgradedWebSpider.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 4;
                    c.m_Feature = CompanionUpgradeWebSpider.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });


            
            

            
            

            var CompanionWebSpiderPortrait = Helpers.CreateBlueprint<BlueprintPortrait>("CompanionWebSpiderPortrait", bp => {
                bp.Data = new PortraitData() {
                    PortraitCategory = PortraitCategory.None,
                    IsDefault = false,
                    InitiativePortrait = false
                };
            });
            var CompanionWebSpiderUnit = Helpers.CreateBlueprint<BlueprintUnit>("CompanionWebSpiderUnit", bp => {
                bp.SetLocalisedName("Web Tyrant Spider");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Strength;
                    c.Skills = new StatType[] { };
                    // Do I need Selections???
                    c.DoNotApplyAutomatically = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        HeadLocatorFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AllowDyingCondition>();
                bp.AddComponent<AddResurrectOnRest>();
                bp.Gender = Gender.Male;
                bp.Size = Size.Medium;
                bp.Color = MediumWebSpider.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = CompanionWebSpiderPortrait.ToReference<BlueprintPortraitReference>();
                bp.Prefab = new UnitViewLink { AssetId = "54e0335882f2dea4188214ea3c8c93de" };
                bp.Visual = MediumWebSpider.Visual;
                bp.m_Faction = Neutrals.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = AzataDragonUnit.FactionOverrides;
                bp.m_Brain = CharacterBrain.ToReference<BlueprintBrainReference>();
                bp.Body = new BlueprintUnit.UnitBody() {
                    DisableHands = false,
                    m_EmptyHandWeapon = WeaponEmptyHand.ToReference<BlueprintItemWeaponReference>(),
                    m_PrimaryHand = Bite1d8.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHand = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative1 = Bite1d8.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative2 = Bite1d8.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative3 = Bite1d8.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHandAlternative3 = new BlueprintItemEquipmentHandReference()
                };
                bp.Strength = 10;
                bp.Dexterity = 17;
                bp.Constitution = 10;
                bp.Wisdom = 10;
                bp.Intelligence = 0;
                bp.Charisma = 2;
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
                    CompanionUpdateWebSpiderFeature.ToReference<BlueprintUnitFactReference>(),
                    TripDefenceEightLegs.ToReference<BlueprintUnitFactReference>(),
                    UnmountableFeature.ToReference<BlueprintUnitFactReference>(),
                    CompanionWebSpiderWebFeature.ToReference<BlueprintUnitFactReference>(),
                    SpiderWebImmunity.ToReference<BlueprintUnitFactReference>(),
                    AnimalCompanionSlotFeature.ToReference<BlueprintUnitFactReference>()
                };
            });

            FullPortraitInjecotr.Replacements[CompanionWebSpiderUnit.PortraitSafe.Data] = PortraitLoader.LoadInternal("Portraits", "WebSpiderFulllength.png", new Vector2Int(692, 1024), TextureFormat.RGBA32);
            HalfPortraitInjecotr.Replacements[CompanionWebSpiderUnit.PortraitSafe.Data] = PortraitLoader.LoadInternal("Portraits", "WebSpiderMedium.png", new Vector2Int(330, 432), TextureFormat.RGBA32);
            SmallPortraitInjecotr.Replacements[CompanionWebSpiderUnit.PortraitSafe.Data] = PortraitLoader.LoadInternal("Portraits", "WebSpiderSmall.png", new Vector2Int(185, 242), TextureFormat.RGBA32);
            EyePortraitInjecotr.Replacements[CompanionWebSpiderUnit.PortraitSafe.Data] = PortraitLoader.LoadInternal("Portraits", "WebSpiderPetEye.png", new Vector2Int(176, 24), TextureFormat.RGBA32);

            var CompanionWebSpiderFeature = Helpers.CreateBlueprint<BlueprintFeature>("CompanionWebSpiderFeature", bp => {
                bp.SetName("Animal Companion — Web Tyrant Spider");
                bp.SetDescription("{g|Encyclopedia:Size}Size{/g}: Small" +
                    "\n{g|Encyclopedia:Speed}Speed{/g}: 30 ft." +
                    "\n{g|Encyclopedia:Armor_Class}AC{/g}: +5 natural armor" +
                    "\n{g|Encyclopedia:Attack}Attack{/g}: bite ({g|Encyclopedia:Dice}1d4{/g})" +
                    "\n{g|Encyclopedia:Ability_Scores}Ability scores{/g}: {g|Encyclopedia:Strength}Str{/g} 10, {g|Encyclopedia:Dexterity}Dex{/g} 17, {g|Encyclopedia:Constitution}Con{/g} 10, {g|Encyclopedia:Intelligence}Int{/g} -, {g|Encyclopedia:Wisdom}Wis{/g} 10, {g|Encyclopedia:Charisma}Cha{/g} 2" +
                    "\nSpecial Attacks: Web" +
                    "\nAt 4th level size becomes Medium, Str +4, Dex -2, Con +2, +1 natural armor and gains iterative attacks." +
                    "\nSpiders Web: Web creates a many-layered mass of {g|Encyclopedia:Strength}strong{/g}, sticky strands. These strands trap those caught in them. Creatures caught within a web become grappled by " +
                    "the sticky fibers.\nAnyone in the effect's area when spider's web is cast must make a {g|Encyclopedia:Saving_Throw}Reflex save{/g}. If this save succeeds, the creature is inside the web but is " +
                    "otherwise unaffected. If the save fails, the creature gains the grappled condition, but can break free by making a {g|Encyclopedia:Combat_Maneuvers}combat maneuver{/g} {g|Encyclopedia:Check}check{/g}" +
                    ", {g|Encyclopedia:Athletics}Athletics check{/g}, or {g|Encyclopedia:Mobility}Mobility check{/g} as a {g|Encyclopedia:Standard_Actions}standard action{/g} against the {g|Encyclopedia:DC}DC{/g} of this " +
                    "ability. The entire area of the web is considered difficult terrain. Anyone moving through the webs must make a Reflex save each {g|Encyclopedia:Combat_Round}round{/g}. Creatures that fail lose their " +
                    "movement and become grappled in the first square of webbing that they enter. Spiders are immune to this ability." +
                    "\nThis animal companion can't be used as a mount.");
                bp.m_Icon = AnimalCompanionFeatureCentipede.m_Icon;
                bp.AddComponent<AddPet>(c => {
                    c.Type = PetType.AnimalCompanion;
                    c.ProgressionType = PetProgressionType.AnimalCompanion;
                    c.m_Pet = CompanionWebSpiderUnit.ToReference<BlueprintUnitReference>();
                    c.m_LevelRank = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisitePet>(c => {
                    c.NoCompanion = true;
                    c.Type = PetType.AnimalCompanion;
                    c.HideInUI = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 1;
                bp.Groups = new FeatureGroup[] { FeatureGroup.AnimalCompanion };
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
            });

            var AnimalCompanionSelectionBase = Resources.GetBlueprint<BlueprintFeatureSelection>("90406c575576aee40a34917a1b429254");
            var AnimalCompanionSelectionDruid = Resources.GetBlueprint<BlueprintFeatureSelection>("571f8434d98560c43935e132df65fe76");
            var AnimalCompanionSelectionDomain = Resources.GetBlueprint<BlueprintFeatureSelection>("2ecd6c64683b59944a7fe544033bb533");
            var AnimalCompanionSelectionHunter = Resources.GetBlueprint<BlueprintFeatureSelection>("715ac15eb8bd5e342bc8a0a3c9e3e38f");
            var AnimalCompanionSelectionMadDog = Resources.GetBlueprint<BlueprintFeatureSelection>("738b59d0b58187f4d846b0caaf0f80d7");
            var AnimalCompanionSelectionRanger = Resources.GetBlueprint<BlueprintFeatureSelection>("ee63330662126374e8785cc901941ac7");
            var AnimalCompanionSelectionSacredHuntsmaster = Resources.GetBlueprint<BlueprintFeatureSelection>("2995b36659b9ad3408fd26f137ee2c67");
            var AnimalCompanionSelectionSylvanSorcerer = Resources.GetBlueprint<BlueprintFeatureSelection>("a540d7dfe1e2a174a94198aba037274c");
            var AnimalCompanionSelectionWildlandShaman = Resources.GetBlueprint<BlueprintFeatureSelection>("164c875d6b27483faba479c7f5261915");

            AnimalCompanionSelectionBase.m_AllFeatures = AnimalCompanionSelectionBase.m_AllFeatures.AppendToArray(CompanionWebSpiderFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionDomain.m_AllFeatures = AnimalCompanionSelectionDomain.m_AllFeatures.AppendToArray(CompanionWebSpiderFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionDruid.m_AllFeatures = AnimalCompanionSelectionDruid.m_AllFeatures.AppendToArray(CompanionWebSpiderFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionHunter.m_AllFeatures = AnimalCompanionSelectionHunter.m_AllFeatures.AppendToArray(CompanionWebSpiderFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionMadDog.m_AllFeatures = AnimalCompanionSelectionMadDog.m_AllFeatures.AppendToArray(CompanionWebSpiderFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionRanger.m_AllFeatures = AnimalCompanionSelectionRanger.m_AllFeatures.AppendToArray(CompanionWebSpiderFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionSacredHuntsmaster.m_AllFeatures = AnimalCompanionSelectionSacredHuntsmaster.m_AllFeatures.AppendToArray(CompanionWebSpiderFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionSylvanSorcerer.m_AllFeatures = AnimalCompanionSelectionSylvanSorcerer.m_AllFeatures.AppendToArray(CompanionWebSpiderFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionWildlandShaman.m_AllFeatures = AnimalCompanionSelectionWildlandShaman.m_AllFeatures.AppendToArray(CompanionWebSpiderFeature.ToReference<BlueprintFeatureReference>());

        }
    }
}
