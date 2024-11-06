using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.AI.Blueprints;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Utility;
using Kingmaker.Visual.HitSystem;
using Kingmaker.Visual.Sound;
using UnityEngine;

namespace ExpandedContent.Tweaks.AnimalCompanions {
    internal class CompanionSaplingTreant {
        public static void AddCompanionSaplingTreant() {

            var AnimalCompanionClass = Resources.GetBlueprint<BlueprintCharacterClass>("26b10d4340839004f960f9816f6109fe");
            var HeadLocatorFeature = Resources.GetBlueprint<BlueprintFeature>("9c57e9674b4a4a2b9920f9fec47f7e6a");
            var TreantBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("bb9ffa4bd65336f4f99ebd3a234f90cf");
            var StandartTreant = Resources.GetBlueprint<BlueprintUnit>("9c2c79e4484e468d8ea06c18de5725c8");
            var AzataDragonUnit = Resources.GetBlueprint<BlueprintUnit>("32a037e97c3d5c54b85da8f639616c57");
            var CharacterBrain = Resources.GetBlueprint<BlueprintBrain>("cf986dd7ba9d4ec46ad8a3a0406d02ae");
            var Neutrals = Resources.GetBlueprint<BlueprintFaction>("d8de50cc80eb4dc409a983991e0b77ad");
            var WeaponEmptyHand = Resources.GetBlueprint<BlueprintItemWeapon>("20375b5a0c9243d45966bd72c690ab74");
            var Slam1d6 = Resources.GetBlueprint<BlueprintItemWeapon>("767e6932882a99c4b8ca95c88d823137");
            var NaturalArmor5 = Resources.GetBlueprint<BlueprintUnitFact>("7661741dbb9604842a642457456fd0e4");
            var NaturalArmor7 = Resources.GetBlueprint<BlueprintUnitFact>("e73864391ccf0894997928443a29d755");
            var AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");
            var TripDefenceFourLegs = Resources.GetBlueprint<BlueprintFeature>("136fa0343d5b4b348bdaa05d83408db3");
            var AnimalCompanionSlotFeature = Resources.GetBlueprint<BlueprintFeature>("75bb2b3c41c99e041b4743fdb16a4289");
            var ConstructType = Resources.GetBlueprint<BlueprintFeature>("fd389783027d63343b4a5634bd81645f");
            var PlantType = Resources.GetBlueprint<BlueprintFeature>("706e61781d692a042b35941f14bc41c5");
            var EntangleSpell = Resources.GetBlueprint<BlueprintAbility>("0fd00984a2c0e0a429cf1a911b4ec5ca");


            var CompanionSaplingTreantConstructFeature = Helpers.CreateBlueprint<BlueprintFeature>("CompanionSaplingTreantConstructFeature", bp => {
                bp.SetName("Construct Smasher");
                bp.SetDescription("The sapling treant deals double damage to constructs and objects.");
                bp.AddComponent<AddOutgoingDamageBonus>(c => {
                    c.DamageType = new DamageTypeDescription() {
                        Type = DamageType.Physical,
                        Common = new DamageTypeDescription.CommomData() {
                            Reality = 0,
                            Alignment = 0,
                            Precision = false
                        },
                        Physical = new DamageTypeDescription.PhysicalData() {
                            Material = 0,
                            Form = Kingmaker.Enums.Damage.PhysicalDamageForm.Bludgeoning,
                            Enhancement = 0,
                            EnhancementTotal = 0
                        },
                        Energy = Kingmaker.Enums.Damage.DamageEnergyType.Fire
                    };
                    c.Condition = DamageIncreaseCondition.Fact;
                    c.Reason = DamageIncreaseReason.None;
                    c.OriginalDamageFactor = 1;
                    c.CheckedDescriptor = SpellDescriptor.None;
                    c.m_CheckedFact = ConstructType.ToReference<BlueprintUnitFactReference>();
                });                
            });

            var CompanionNotUpgradedSaplingTreant = Helpers.CreateBlueprint<BlueprintFeature>("CompanionNotUpgradedSaplingTreant", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<ChangeUnitSize>(c => {
                    c.m_Type = ChangeUnitSize.ChangeType.Delta;
                    c.SizeDelta = -2;
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
            var CompanionUpgradeSaplingTreant = Helpers.CreateBlueprint<BlueprintFeature>("CompanionUpgradeSaplingTreant", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Value = 8;
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
                        NaturalArmor7.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = Slam1d6.ToReference<BlueprintItemWeaponReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });
            var CompanionUpdateSaplingTreantFeature = Helpers.CreateBlueprint<BlueprintFeature>("CompanionUpdateSaplingTreantFeature", bp => {
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
                    c.m_Feature = CompanionNotUpgradedSaplingTreant.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 4;
                    c.m_Feature = CompanionUpgradeSaplingTreant.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });

            var CompanionSaplingTreantPortrait = Helpers.CreateBlueprint<BlueprintPortrait>("CompanionSaplingTreantPortrait", bp => {
                bp.Data = PortraitLoader.LoadPortraitData("SaplingTreant");
            });
            var CompanionSaplingTreantUnit = Helpers.CreateBlueprint<BlueprintUnit>("CompanionSaplingTreantUnit", bp => {
                bp.SetLocalisedName("Sapling Treant");
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
                bp.Size = Size.Huge;
                bp.Color = StandartTreant.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = CompanionSaplingTreantPortrait.ToReference<BlueprintPortraitReference>();
                bp.Prefab = new UnitViewLink { AssetId = "16f5bf5f4dc3c9e4dab4165b360a5e3d" };
                bp.Visual = new UnitVisualParams() {
                    BloodType = BloodType.Vegetable,
                    FootprintType = FootprintType.Humanoid,
                    FootprintScale = 1,
                    ArmorFx = new PrefabLink(),
                    BloodPuddleFx = new PrefabLink(),
                    DismemberFx = new PrefabLink(),
                    RipLimbsApartFx = new PrefabLink(),
                    IsNotUseDismember = false,
                    m_Barks = TreantBarks.ToReference<BlueprintUnitAsksListReference>(),
                    ReachFXThresholdBonus = 0,
                    DefaultArmorSoundType = ArmorSoundType.Wood,
                    FootstepSoundSizeType = FootstepSoundSizeType.BootMedium,
                    FootSoundType = FootSoundType.HardPaw,
                    FootSoundSize = Size.Medium,
                    BodySoundType = BodySoundType.Wood,
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
                    m_PrimaryHand = Slam1d6.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHand = Slam1d6.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_PrimaryHandAlternative1 = Slam1d6.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHandAlternative1 = Slam1d6.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_PrimaryHandAlternative2 = Slam1d6.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHandAlternative2 = Slam1d6.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_PrimaryHandAlternative3 = Slam1d6.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHandAlternative3 = Slam1d6.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_AdditionalLimbs = new BlueprintItemWeaponReference[0] { }
                };
                bp.Strength = 15;
                bp.Dexterity = 10;
                bp.Constitution = 12;
                bp.Wisdom = 12;
                bp.Intelligence = 2;
                bp.Charisma = 7;
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
                    CompanionUpdateSaplingTreantFeature.ToReference<BlueprintUnitFactReference>(),
                    PlantType.ToReference<BlueprintUnitFactReference>(),
                    CompanionSaplingTreantConstructFeature.ToReference<BlueprintUnitFactReference>(),
                    AnimalCompanionSlotFeature.ToReference<BlueprintUnitFactReference>()
                };
            });

            EyePortraitInjecotr.Replacements[CompanionSaplingTreantUnit.PortraitSafe.Data] = PortraitLoader.LoadInternal("Portraits", "SaplingTreantPetEye.png", new Vector2Int(176, 24), TextureFormat.RGBA32);

            var CompanionSaplingTreantFeature = Helpers.CreateBlueprint<BlueprintFeature>("CompanionSaplingTreantFeature", bp => {
                bp.SetName("Animal Companion — Sapling Treant");
                bp.SetDescription("{g|Encyclopedia:Size}Size{/g}: Medium" +
                    "\n{g|Encyclopedia:Speed}Speed{/g}: 30 ft." +
                    "\n{g|Encyclopedia:Armor_Class}AC{/g}: +5 natural armor" +
                    "\n{g|Encyclopedia:Attack}Attack{/g}: 2 slams ({g|Encyclopedia:Dice}1d6{/g}) " +
                    "\n{g|Encyclopedia:Ability_Scores}Ability scores{/g}: {g|Encyclopedia:Strength}Str{/g} 15, {g|Encyclopedia:Dexterity}Dex{/g} 10, {g|Encyclopedia:Constitution}Con{/g} 12, {g|Encyclopedia:Intelligence}Int{/g} 2, {g|Encyclopedia:Wisdom}Wis{/g} 12, {g|Encyclopedia:Charisma}Cha{/g} 7" +
                    "\nSpecial Attacks: The sapling treant deals double damage to constructs and objects." +
                    "\nAt 4th level size becomes Large, Str +8, Dex -2, Con +4, +2 natural armor, and also gains an additional slam attack.");
                bp.m_Icon = EntangleSpell.m_Icon;
                bp.AddComponent<AddPet>(c => {
                    c.Type = PetType.AnimalCompanion;
                    c.ProgressionType = PetProgressionType.AnimalCompanion;
                    c.m_Pet = CompanionSaplingTreantUnit.ToReference<BlueprintUnitReference>();
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

            //Does not need to be added to pet selections

        }
    }
}
