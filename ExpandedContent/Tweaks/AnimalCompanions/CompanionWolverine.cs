using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.AI.Blueprints;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Utility;
using Kingmaker.Visual.HitSystem;
using Kingmaker.Visual.Sound;
using UnityEngine;

namespace ExpandedContent.Tweaks.AnimalCompanions {
    internal class CompanionWolverine {
        public static void AddCompanionWolverine() {

            var AnimalCompanionClass = Resources.GetBlueprint<BlueprintCharacterClass>("26b10d4340839004f960f9816f6109fe");
            var HeadLocatorFeature = Resources.GetBlueprint<BlueprintFeature>("9c57e9674b4a4a2b9920f9fec47f7e6a");
            var WolverineBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("540e70df273f47feba72aabac0da466f");
            var DireWolverine = Resources.GetBlueprint<BlueprintUnit>("2eafd55b239849a1b7b1f8169b2944ae");
            var AzataDragonUnit = Resources.GetBlueprint<BlueprintUnit>("32a037e97c3d5c54b85da8f639616c57");
            var CharacterBrain = Resources.GetBlueprint<BlueprintBrain>("cf986dd7ba9d4ec46ad8a3a0406d02ae");
            var Neutrals = Resources.GetBlueprint<BlueprintFaction>("d8de50cc80eb4dc409a983991e0b77ad");
            var WeaponEmptyHand = Resources.GetBlueprint<BlueprintItemWeapon>("20375b5a0c9243d45966bd72c690ab74");
            var Bite1d8 = Resources.GetBlueprint<BlueprintItemWeapon>("61bc14eca5f8c1040900215000cfc218");
            var Claw1d6 = Resources.GetBlueprint<BlueprintItemWeapon>("65eb73689b94d894080d33a768cdf645");
            var NaturalArmor6 = Resources.GetBlueprint<BlueprintUnitFact>("987ba44303e88054c9504cb3083ba0c9");
            var NaturalArmor8 = Resources.GetBlueprint<BlueprintUnitFact>("b9342e2a6dc5165489ba3412c50ca3d1");
            var AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");
            var TripDefenceFourLegs = Resources.GetBlueprint<BlueprintFeature>("136fa0343d5b4b348bdaa05d83408db3");
            var RageFeature = Resources.GetBlueprint<BlueprintFeature>("2479395977cfeeb46b482bc3385f4647");
            var RageBuff = Resources.GetBlueprint<BlueprintBuff>("da8ce41ac3cd74742b80984ccc3c9613");
            var AnimalCompanionSlotFeature = Resources.GetBlueprint<BlueprintFeature>("75bb2b3c41c99e041b4743fdb16a4289");





            var AnimalCompanionFeatureBear = Resources.GetBlueprint<BlueprintFeature>("f6f1cdcc404f10c4493dc1e51208fd6f");

            var UnmountableFeature = Helpers.CreateBlueprint<BlueprintFeature>("UnmountableFeature", bp => {
                bp.SetName("Unmountable");
                bp.SetDescription("");                
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
            });
            var MountTargetAbility = Resources.GetBlueprint<BlueprintAbility>("9f8c0f4fcabdb3145b449826d17da18d");
            MountTargetAbility.AddComponent<AbilityTargetHasFact>(c => {
                c.m_CheckedFacts = new BlueprintUnitFactReference[] {
                    UnmountableFeature.ToReference<BlueprintUnitFactReference>()
                };
                c.Inverted = true;
            });


            var CompanionNotUpgradedWolverine = Helpers.CreateBlueprint<BlueprintFeature>("CompanionNotUpgradedWolverine", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<ChangeUnitSize>(c => {
                    c.m_Type = ChangeUnitSize.ChangeType.Delta;
                    c.SizeDelta = -2;
                    c.Size = Size.Fine;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        NaturalArmor6.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });
            var CompanionUpgradeWolverine = Helpers.CreateBlueprint<BlueprintFeature>("CompanionUpgradeWolverine", bp => {
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
                    c.Value = 4;
                    c.Stat = StatType.Constitution;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        NaturalArmor8.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });
            var CompanionUpdateWolverineFeature = Helpers.CreateBlueprint<BlueprintFeature>("CompanionUpdateWolverineFeature", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<ChangeUnitSize>(c => {
                    c.m_Type = ChangeUnitSize.ChangeType.Delta;
                    c.SizeDelta = -1;
                    c.Size = Size.Fine;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 4;
                    c.m_Feature = CompanionNotUpgradedWolverine.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 4;
                    c.m_Feature = CompanionUpgradeWolverine.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });


            var CompanionWolverineRagResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("CompanionWolverineRagResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 6,
                    IncreasedByLevel = false,
                    m_Class = new BlueprintCharacterClassReference[] { },
                    m_Archetypes = new BlueprintArchetypeReference[] { },
                    LevelIncrease = 1,
                    StartingLevel = 6,
                    StartingIncrease = 1,
                };
            });
            var CompanionWolverineRageAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("CompanionWolverineRageAbility", bp => {
                bp.SetName("Rage");
                bp.SetDescription("While in rage, a barbarian gains a +2 {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:MeleeAttack}melee attack{/g} {g|Encyclopedia:Dice}rolls{/g}, melee " +
                    "{g|Encyclopedia:Damage}damage rolls{/g}, thrown weapon damage rolls, and Will {g|Encyclopedia:Saving_Throw}saving throws{/g}. In addition, she takes a –2 {g|Encyclopedia:Penalty}penalty{/g} " +
                    "to {g|Encyclopedia:Armor_Class}Armor Class{/g}. She also gains 2 temporary {g|Encyclopedia:HP}hit points{/g} per {g|Encyclopedia:Hit_Dice}Hit Die{/g}.");
                bp.m_Icon = RageFeature.Icon;
                bp.m_Buff = RageBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.NewRound;
                    c.m_RequiredResource = CompanionWolverineRagResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.Group = ActivatableAbilityGroup.Rage;
                bp.DeactivateIfCombatEnded = true;
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
            });
            

            var CompanionWolverineRageFeature = Helpers.CreateBlueprint<BlueprintFeature>("CompanionWolverineRageFeature", bp => {
                bp.SetName("Rage");
                bp.SetDescription("While in rage, a barbarian gains a +2 {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:MeleeAttack}melee attack{/g} {g|Encyclopedia:Dice}rolls{/g}, melee " +
                    "{g|Encyclopedia:Damage}damage rolls{/g}, thrown weapon damage rolls, and Will {g|Encyclopedia:Saving_Throw}saving throws{/g}. In addition, she takes a –2 {g|Encyclopedia:Penalty}penalty{/g} " +
                    "to {g|Encyclopedia:Armor_Class}Armor Class{/g}. She also gains 2 temporary {g|Encyclopedia:HP}hit points{/g} per {g|Encyclopedia:Hit_Dice}Hit Die{/g}.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        CompanionWolverineRageAbility.ToReference<BlueprintUnitFactReference>(),
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = CompanionWolverineRagResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
            });
            var ExtraRageWolverineFeat = Helpers.CreateBlueprint<BlueprintFeature>("ExtraRageWolverineFeat", bp => {
                bp.SetName("Extra Rage (Wolverine)");
                bp.SetDescription("Benefit: You can rage for 6 additional {g|Encyclopedia:Combat_Round}rounds{/g} per day.\\nSpecial: You can gain Extra Rage multiple times. Its effects stack.");
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = CompanionWolverineRageFeature.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                });
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = CompanionWolverineRagResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 6;
                });
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Feat };
                bp.IsClassFeature = true;
            });
            FeatTools.AddAsFeat(ExtraRageWolverineFeat);
            var CompanionWolverinePortrait = Helpers.CreateBlueprint<BlueprintPortrait>("CompanionWolverinePortrait", bp => {
                bp.Data = PortraitLoader.LoadPortraitData("Wolverine");
            });
            var CompanionWolverineUnit = Helpers.CreateBlueprint<BlueprintUnit>("CompanionWolverineUnit", bp => {
                bp.SetLocalisedName("Wolverine");
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
                bp.Size = Size.Large;
                bp.Color = DireWolverine.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = CompanionWolverinePortrait.ToReference<BlueprintPortraitReference>();
                bp.Prefab = new UnitViewLink { AssetId = "dfd41d2cada9b2b4e80a9e6977b69c71" };
                bp.Visual = new UnitVisualParams() {
                    BloodType = BloodType.Common,
                    FootprintType = FootprintType.Humanoid,
                    FootprintScale = 1,
                    ArmorFx = new PrefabLink(),
                    BloodPuddleFx = new PrefabLink(),
                    DismemberFx = new PrefabLink(),
                    RipLimbsApartFx = new PrefabLink(),
                    IsNotUseDismember = false,
                    m_Barks = WolverineBarks.ToReference<BlueprintUnitAsksListReference>(),
                    ReachFXThresholdBonus = 0,
                    DefaultArmorSoundType = ArmorSoundType.Flesh,
                    FootstepSoundSizeType = FootstepSoundSizeType.BootMedium,
                    FootSoundType = FootSoundType.SoftPaw,
                    FootSoundSize = Size.Medium,
                    BodySoundType = BodySoundType.Flesh,
                    BodySoundSize = Size.Medium,
                    FoleySoundPrefix = null, //?
                    NoFinishingBlow = false,
                    ImportanceOverride = 0,
                    SilentCaster = true
                };
                bp.m_Faction = Neutrals.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = AzataDragonUnit.FactionOverrides;
                bp.m_Brain = CharacterBrain.ToReference<BlueprintBrainReference>();
                bp.Body = new BlueprintUnit.UnitBody() {
                    DisableHands = false,
                    m_EmptyHandWeapon = WeaponEmptyHand.ToReference<BlueprintItemWeaponReference>(),
                    m_PrimaryHand = Bite1d8.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHand = Claw1d6.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_PrimaryHandAlternative1 = Bite1d8.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHandAlternative1 = Claw1d6.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_PrimaryHandAlternative2 = Bite1d8.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHandAlternative2 = Claw1d6.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_PrimaryHandAlternative3 = Bite1d8.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHandAlternative3 = Claw1d6.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_AdditionalLimbs = new BlueprintItemWeaponReference[] {
                        Claw1d6.ToReference<BlueprintItemWeaponReference>()
                    }
                };
                bp.Strength = 10;
                bp.Dexterity = 17;
                bp.Constitution = 15;
                bp.Wisdom = 12;
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
                    CompanionUpdateWolverineFeature.ToReference<BlueprintUnitFactReference>(),
                    TripDefenceFourLegs.ToReference<BlueprintUnitFactReference>(),
                    UnmountableFeature.ToReference<BlueprintUnitFactReference>(),
                    CompanionWolverineRageFeature.ToReference<BlueprintUnitFactReference>(),
                    AnimalCompanionSlotFeature.ToReference<BlueprintUnitFactReference>()
                };
            });

            EyePortraitInjecotr.Replacements[CompanionWolverineUnit.PortraitSafe.Data] = PortraitLoader.LoadInternal("Portraits", "WolverinePetEye.png", new Vector2Int(176, 24), TextureFormat.RGBA32);

            var CompanionWolverineFeature = Helpers.CreateBlueprint<BlueprintFeature>("CompanionWolverineFeature", bp => {
                bp.SetName("Animal Companion — Wolverine");
                bp.SetDescription("{g|Encyclopedia:Size}Size{/g}: Small" +
                    "\n{g|Encyclopedia:Speed}Speed{/g}: 30 ft." +
                    "\n{g|Encyclopedia:Armor_Class}AC{/g}: +6 natural armor" +
                    "\n{g|Encyclopedia:Attack}Attack{/g}: bite ({g|Encyclopedia:Dice}1d4{/g}), 2 claws ({g|Encyclopedia:Dice}1d3{/g})" +
                    "\n{g|Encyclopedia:Ability_Scores}Ability scores{/g}: {g|Encyclopedia:Strength}Str{/g} 10, {g|Encyclopedia:Dexterity}Dex{/g} 17, {g|Encyclopedia:Constitution}Con{/g} 15, {g|Encyclopedia:Intelligence}Int{/g} 2, {g|Encyclopedia:Wisdom}Wis{/g} 12, {g|Encyclopedia:Charisma}Cha{/g} 10" +
                    "\nSpecial Attacks: rage (as a barbarian for 6 rounds per day)" +
                    "\nAt 4th level size becomes Medium, Str +4, Dex -2, Con +4, +2 natural armor." +
                    "\nThis animal companion can't be used as a mount.");
                bp.m_Icon = AnimalCompanionFeatureBear.m_Icon;
                bp.AddComponent<AddPet>(c => {
                    c.Type = PetType.AnimalCompanion;
                    c.ProgressionType = PetProgressionType.AnimalCompanion;
                    c.m_Pet = CompanionWolverineUnit.ToReference<BlueprintUnitReference>();
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

            AnimalCompanionSelectionBase.m_AllFeatures = AnimalCompanionSelectionBase.m_AllFeatures.AppendToArray(CompanionWolverineFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionDomain.m_AllFeatures = AnimalCompanionSelectionDomain.m_AllFeatures.AppendToArray(CompanionWolverineFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionDruid.m_AllFeatures = AnimalCompanionSelectionDruid.m_AllFeatures.AppendToArray(CompanionWolverineFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionHunter.m_AllFeatures = AnimalCompanionSelectionHunter.m_AllFeatures.AppendToArray(CompanionWolverineFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionMadDog.m_AllFeatures = AnimalCompanionSelectionMadDog.m_AllFeatures.AppendToArray(CompanionWolverineFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionRanger.m_AllFeatures = AnimalCompanionSelectionRanger.m_AllFeatures.AppendToArray(CompanionWolverineFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionSacredHuntsmaster.m_AllFeatures = AnimalCompanionSelectionSacredHuntsmaster.m_AllFeatures.AppendToArray(CompanionWolverineFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionSylvanSorcerer.m_AllFeatures = AnimalCompanionSelectionSylvanSorcerer.m_AllFeatures.AppendToArray(CompanionWolverineFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionWildlandShaman.m_AllFeatures = AnimalCompanionSelectionWildlandShaman.m_AllFeatures.AppendToArray(CompanionWolverineFeature.ToReference<BlueprintFeatureReference>());

        }
    }
}
