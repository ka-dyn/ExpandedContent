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
    internal class CompanionGiantFly {
        public static void AddCompanionGiantFly() {

            var AnimalCompanionClass = Resources.GetBlueprint<BlueprintCharacterClass>("26b10d4340839004f960f9816f6109fe");
            var HeadLocatorFeature = Resources.GetBlueprint<BlueprintFeature>("9c57e9674b4a4a2b9920f9fec47f7e6a");
            var GiantFlyBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("5f2435439043da240ad7ba96179523cf");
            var MediumGiantFly = Resources.GetBlueprint<BlueprintUnit>("9b43934530f94d03a445055a36b081db");
            var AzataDragonUnit = Resources.GetBlueprint<BlueprintUnit>("32a037e97c3d5c54b85da8f639616c57");
            var CharacterBrain = Resources.GetBlueprint<BlueprintBrain>("cf986dd7ba9d4ec46ad8a3a0406d02ae");
            var Neutrals = Resources.GetBlueprint<BlueprintFaction>("d8de50cc80eb4dc409a983991e0b77ad");
            var WeaponEmptyHand = Resources.GetBlueprint<BlueprintItemWeapon>("20375b5a0c9243d45966bd72c690ab74");
            var Bite1d8 = Resources.GetBlueprint<BlueprintItemWeapon>("61bc14eca5f8c1040900215000cfc218");
            var NaturalArmor6 = Resources.GetBlueprint<BlueprintUnitFact>("987ba44303e88054c9504cb3083ba0c9");
            var NaturalArmor5 = Resources.GetBlueprint<BlueprintUnitFact>("7661741dbb9604842a642457456fd0e4");
            var AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");
            var TripImmune = Resources.GetBlueprint<BlueprintFeature>("c1b26f97b974aec469613f968439e7bb");
            var VerminType = Resources.GetBlueprint<BlueprintFeature>("09478937695300944a179530664e42ec");
            var AnimalCompanionSlotFeature = Resources.GetBlueprint<BlueprintFeature>("75bb2b3c41c99e041b4743fdb16a4289");
            var InflictLightWoundsMass = Resources.GetBlueprint<BlueprintAbility>("e5af3674bb241f14b9a9f6b0c7dc3d27");




            var AnimalCompanionFeatureCentipede = Resources.GetBlueprint<BlueprintFeature>("f9ef7717531f5914a9b6ecacfad63f46");

            var UnmountableFeature = Resources.GetModBlueprint<BlueprintFeature>("UnmountableFeature");


            
            var CompanionGiantFlyBloodDrainAbility = Helpers.CreateBlueprint<BlueprintAbility>("CompanionGiantFlyBloodDrainAbility", bp => {
                bp.SetName("Blood Drain");
                bp.SetDescription("Make one attack at highest base attack bonus, if the attack hits the target they suffer 1d2 constitution damage.");
                bp.m_Icon = InflictLightWoundsMass.Icon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Weapon;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "None");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.AbilityDamage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Physical,
                                Common = new DamageTypeDescription.CommomData(),
                                Physical = new DamageTypeDescription.PhysicalData(),
                                Energy = DamageEnergyType.Fire,
                            },
                            AbilityType = StatType.Constitution,
                            Duration = new ContextDurationValue() {
                                m_IsExtendable = true,
                                DiceCountValue = new ContextValue(),
                                BonusValue = new ContextValue(),
                            },
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.D2,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue(),
                            }
                        });
                });
                bp.AddComponent<AbilityDeliverAttackWithWeapon>();
            });
            var CompanionGiantFlyBloodDrainFeature = Helpers.CreateBlueprint<BlueprintFeature>("CompanionGiantFlyBloodDrainFeature", bp => {
                bp.SetName("Blood Drain");
                bp.SetDescription("Make one attack at highest base attack bonus, if the attack hits the target they suffer 1d2 constitution damage.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        CompanionGiantFlyBloodDrainAbility.ToReference<BlueprintUnitFactReference>(),
                    };
                });
                bp.IsClassFeature = true;
            });

            var CompanionNotUpgradedGiantFly = Helpers.CreateBlueprint<BlueprintFeature>("CompanionNotUpgradedGiantFly", bp => {
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
            var CompanionUpgradeGiantFly = Helpers.CreateBlueprint<BlueprintFeature>("CompanionUpgradeGiantFly", bp => {
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
                        NaturalArmor6.ToReference<BlueprintUnitFactReference>(),
                        CompanionGiantFlyBloodDrainFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddMechanicsFeature>(c => {
                    c.m_Feature = AddMechanicsFeature.MechanicsFeatureType.IterativeNaturalAttacks;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });
            var CompanionUpdateGiantFlyFeature = Helpers.CreateBlueprint<BlueprintFeature>("CompanionUpdateGiantFlyFeature", bp => {
                bp.SetName("");
                bp.SetDescription("");                
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 7;
                    c.m_Feature = CompanionNotUpgradedGiantFly.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 7;
                    c.m_Feature = CompanionUpgradeGiantFly.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });


            
            

            
            

            var CompanionGiantFlyPortrait = Helpers.CreateBlueprint<BlueprintPortrait>("CompanionGiantFlyPortrait", bp => {
                bp.Data = new PortraitData() {
                    PortraitCategory = PortraitCategory.None,
                    IsDefault = false,
                    InitiativePortrait = false
                };
            });
            var CompanionGiantFlyUnit = Helpers.CreateBlueprint<BlueprintUnit>("CompanionGiantFlyUnit", bp => {
                bp.SetLocalisedName("Giant Fly");
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
                bp.Color = MediumGiantFly.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = CompanionGiantFlyPortrait.ToReference<BlueprintPortraitReference>();
                bp.Prefab = new UnitViewLink { AssetId = "fd4b4672f8c1168468071445db6710dc" };
                bp.Visual = MediumGiantFly.Visual;
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
                bp.Strength = 14;
                bp.Dexterity = 21;
                bp.Constitution = 15;
                bp.Wisdom = 13;
                bp.Intelligence = 0;
                bp.Charisma = 6;
                bp.Speed = new Feet(60);
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
                    CompanionUpdateGiantFlyFeature.ToReference<BlueprintUnitFactReference>(),
                    TripImmune.ToReference<BlueprintUnitFactReference>(),
                    UnmountableFeature.ToReference<BlueprintUnitFactReference>(),
                    AnimalCompanionSlotFeature.ToReference<BlueprintUnitFactReference>()
                };
            });

            FullPortraitInjecotr.Replacements[CompanionGiantFlyUnit.PortraitSafe.Data] = PortraitLoader.LoadInternal("Portraits", "GiantFlyFulllength.png", new Vector2Int(692, 1024), TextureFormat.RGBA32);
            HalfPortraitInjecotr.Replacements[CompanionGiantFlyUnit.PortraitSafe.Data] = PortraitLoader.LoadInternal("Portraits", "GiantFlyMedium.png", new Vector2Int(330, 432), TextureFormat.RGBA32);
            SmallPortraitInjecotr.Replacements[CompanionGiantFlyUnit.PortraitSafe.Data] = PortraitLoader.LoadInternal("Portraits", "GiantFlySmall.png", new Vector2Int(185, 242), TextureFormat.RGBA32);
            EyePortraitInjecotr.Replacements[CompanionGiantFlyUnit.PortraitSafe.Data] = PortraitLoader.LoadInternal("Portraits", "GiantFlyPetEye.png", new Vector2Int(176, 24), TextureFormat.RGBA32);

            var CompanionGiantFlyFeature = Helpers.CreateBlueprint<BlueprintFeature>("CompanionGiantFlyFeature", bp => {
                bp.SetName("Animal Companion — Giant Fly");
                bp.SetDescription("{g|Encyclopedia:Size}Size{/g}: Small" +
                    "\n{g|Encyclopedia:Speed}Speed{/g}: 60 ft." +
                    "\n{g|Encyclopedia:Armor_Class}AC{/g}: +5 natural armor" +
                    "\n{g|Encyclopedia:Attack}Attack{/g}: bite ({g|Encyclopedia:Dice}1d6{/g})" +
                    "\n{g|Encyclopedia:Ability_Scores}Ability scores{/g}: {g|Encyclopedia:Strength}Str{/g} 14, {g|Encyclopedia:Dexterity}Dex{/g} 21, {g|Encyclopedia:Constitution}Con{/g} 15, {g|Encyclopedia:Intelligence}Int{/g} -, {g|Encyclopedia:Wisdom}Wis{/g} 13, {g|Encyclopedia:Charisma}Cha{/g} 6" +
                    "\nSpecial Attacks: bleed (1d6)" +
                    "\nAt 7th level size becomes Medium, Str +4, Dex -2, Con +2, +1 natural armor, bleed increases to 2d6, gains iterative attacks and the Blood Drain ability." +
                    "\nBlood Drain: Make one attack at highest base attack bonus, if the attack hits the target they suffer 1d2 constitution damage." +
                    "\nThis animal companion can't be used as a mount.");
                bp.m_Icon = AnimalCompanionFeatureCentipede.m_Icon;
                bp.AddComponent<AddPet>(c => {
                    c.Type = PetType.AnimalCompanion;
                    c.ProgressionType = PetProgressionType.AnimalCompanion;
                    c.m_Pet = CompanionGiantFlyUnit.ToReference<BlueprintUnitReference>();
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

            AnimalCompanionSelectionBase.m_AllFeatures = AnimalCompanionSelectionBase.m_AllFeatures.AppendToArray(CompanionGiantFlyFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionDomain.m_AllFeatures = AnimalCompanionSelectionDomain.m_AllFeatures.AppendToArray(CompanionGiantFlyFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionDruid.m_AllFeatures = AnimalCompanionSelectionDruid.m_AllFeatures.AppendToArray(CompanionGiantFlyFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionHunter.m_AllFeatures = AnimalCompanionSelectionHunter.m_AllFeatures.AppendToArray(CompanionGiantFlyFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionMadDog.m_AllFeatures = AnimalCompanionSelectionMadDog.m_AllFeatures.AppendToArray(CompanionGiantFlyFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionRanger.m_AllFeatures = AnimalCompanionSelectionRanger.m_AllFeatures.AppendToArray(CompanionGiantFlyFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionSacredHuntsmaster.m_AllFeatures = AnimalCompanionSelectionSacredHuntsmaster.m_AllFeatures.AppendToArray(CompanionGiantFlyFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionSylvanSorcerer.m_AllFeatures = AnimalCompanionSelectionSylvanSorcerer.m_AllFeatures.AppendToArray(CompanionGiantFlyFeature.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionWildlandShaman.m_AllFeatures = AnimalCompanionSelectionWildlandShaman.m_AllFeatures.AppendToArray(CompanionGiantFlyFeature.ToReference<BlueprintFeatureReference>());

        }
    }
}
