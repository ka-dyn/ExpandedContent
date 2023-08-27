using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.AI.Blueprints;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Utility;
using Kingmaker.Visual.HitSystem;
using Kingmaker.Visual.Sound;
using UnityEngine;
using static ExpandedContent.Tweaks.Miscellaneous.DrakePetTypeAdder;

namespace ExpandedContent.Tweaks.Classes.DrakeClass {
    internal class DrakeCompanionGold {

        public static void AddDrakeCompanionGold() {

            var AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");
            var DrakeCompanionClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DrakeCompanionClass");
            var DrakeSubtypeFire = Resources.GetModBlueprint<BlueprintFeature>("DrakeSubtypeFire");
            var FormOfTheDragonGoldBuff = Resources.GetBlueprint<BlueprintBuff>("89669cfba3d9c15448c23b79dd604c41").GetComponent<Polymorph>();
            var BloodlineDraconicGoldProgression = Resources.GetBlueprint<BlueprintProgression>("6c67ef823db8d7d45bb0ef82f959743d");
            var BlackDragonBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("3c0924a80e504f04c94de6ec2a28f9aa");
            var CharacterBrain = Resources.GetBlueprint<BlueprintBrain>("cf986dd7ba9d4ec46ad8a3a0406d02ae");
            var Neutrals = Resources.GetBlueprint<BlueprintFaction>("d8de50cc80eb4dc409a983991e0b77ad");
            var WeaponEmptyHand = Resources.GetBlueprint<BlueprintItemWeapon>("20375b5a0c9243d45966bd72c690ab74");
            var BiteHuge2d6 = Resources.GetBlueprint<BlueprintItemWeapon>("d2f99947db522e24293a7ec4eded453f");
            var TailHuge2d6 = Resources.GetBlueprint<BlueprintItemWeapon>("c36359e00abf82b40b5df9e5394207dd");
            var DrakeCompanionSlotFeature = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionSlotFeature");
            var DragonType = Resources.GetBlueprint<BlueprintFeature>("455ac88e22f55804ab87c2467deff1d6");
            var AzataDragonUnit = Resources.GetBlueprint<BlueprintUnit>("32a037e97c3d5c54b85da8f639616c57");
            var AzataDragonUnitNPC_medium = Resources.GetBlueprint<BlueprintUnit>("3e56db348cc24838bc78b55a114e552a");
            var RedDragon = Resources.GetBlueprint<BlueprintUnit>("9e8727d008bec6e47842ba13df87d939");
            var UnitDog = Resources.GetBlueprint<BlueprintUnit>("918939943bf32ba4a95470ea696c2ba5");
            var HeadLocatorFeature = Resources.GetBlueprint<BlueprintFeature>("9c57e9674b4a4a2b9920f9fec47f7e6a");
            var DrakeSizeTiny = Resources.GetModBlueprint<BlueprintFeature>("DrakeSizeTiny");
            var HavocLargeDragonBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("478370a2af1b5cb4abe9c2fe80ef0cb5");


            var DrakeGoldPortrait = Helpers.CreateBlueprint<BlueprintPortrait>("DrakeGoldPortrait", bp => {
                bp.Data = PortraitLoader.LoadPortraitData("Gold");
            });


            var DrakeBloodGold = Helpers.CreateBlueprint<BlueprintFeature>("DrakeBloodGold", bp => {
                bp.SetName("DrakeBloodGold");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });


            var DrakeCompanionUnitGold = Helpers.CreateBlueprint<BlueprintUnit>("DrakeCompanionUnitGold", bp => {
                bp.SetLocalisedName("Gold Drake");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = DrakeCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.RaceStat = StatType.Unknown;
                    c.LevelsStat = StatType.Charisma;
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
                bp.Color = AzataDragonUnit.Color;
                bp.Alignment = Alignment.LawfulGood;
                bp.m_Portrait = DrakeGoldPortrait.ToReference<BlueprintPortraitReference>();
                bp.Prefab = FormOfTheDragonGoldBuff.m_Prefab;
                //bp.Visual = RedDragon.Visual;
                bp.Visual = new UnitVisualParams() {
                    BloodType = BloodType.Common,
                    FootprintType = FootprintType.Humanoid,
                    FootprintScale = 1,
                    ArmorFx = new PrefabLink(),
                    BloodPuddleFx = new PrefabLink(),
                    DismemberFx = new PrefabLink(),
                    RipLimbsApartFx = new PrefabLink(),
                    IsNotUseDismember = false,
                    m_Barks = HavocLargeDragonBarks.ToReference<BlueprintUnitAsksListReference>(),
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
                bp.m_Faction = Neutrals.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = AzataDragonUnit.FactionOverrides;
                bp.m_Brain = CharacterBrain.ToReference<BlueprintBrainReference>();
                bp.Body = new BlueprintUnit.UnitBody() {
                    DisableHands = false,
                    m_EmptyHandWeapon = WeaponEmptyHand.ToReference<BlueprintItemWeaponReference>(),
                    m_PrimaryHand = BiteHuge2d6.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHand = TailHuge2d6.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_PrimaryHandAlternative1 = BiteHuge2d6.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHandAlternative1 = TailHuge2d6.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_PrimaryHandAlternative2 = BiteHuge2d6.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHandAlternative2 = TailHuge2d6.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_PrimaryHandAlternative3 = BiteHuge2d6.ToReference<BlueprintItemEquipmentHandReference>(),
                    m_SecondaryHandAlternative3 = TailHuge2d6.ToReference<BlueprintItemEquipmentHandReference>()
                };
                //Stats set for Tiny size
                bp.Strength = 8;
                bp.Dexterity = 17;
                bp.Constitution = 11;
                bp.Wisdom = 10;
                bp.Intelligence = 4;
                bp.Charisma = 7;
                bp.Speed = new Feet(20);
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
                    DrakeCompanionSlotFeature.ToReference<BlueprintUnitFactReference>(),
                    DrakeSubtypeFire.ToReference<BlueprintUnitFactReference>(),
                    DragonType.ToReference<BlueprintUnitFactReference>(),
                    DrakeBloodGold.ToReference<BlueprintUnitFactReference>(),
                    DrakeSizeTiny.ToReference<BlueprintUnitFactReference>()
                };
            });

            EyePortraitInjecotr.Replacements[DrakeCompanionUnitGold.PortraitSafe.Data] = PortraitLoader.LoadInternal("Portraits", "GoldPetEye.png", new Vector2Int(176, 24), TextureFormat.RGBA32);

            var DrakeCompanionFeatureGold = Helpers.CreateBlueprint<BlueprintFeature>("DrakeCompanionFeatureGold", bp => {
                bp.SetName("Drake Companion - Gold");
                bp.SetDescription("Drakes are brutish lesser kindred of true dragons. Though they aren’t particularly intelligent, drakes’ significantly faster breeding allows their kind to survive in harsh environments. While a " +
                    "young drake is weaker than a standard animal companion, as they grow they will start to resemble their draconic cousins more and more until they rival them in power. " +
                    "\n This drake is descended from metallic gold dragons, giving it the fire subtype and granting it potential to wield fire breath attacks");
                bp.m_Icon = BloodlineDraconicGoldProgression.m_Icon;
                bp.AddComponent<AddPet>(c => {
                    c.Type = PetType.AnimalCompanion;
                    c.ProgressionType = (PetProgressionType)CustomPetProgressionType.DrakeCompanion;
                    c.m_Pet = DrakeCompanionUnitGold.ToReference<BlueprintUnitReference>();
                    c.m_LevelRank = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisitePet>(c => {
                    c.NoCompanion = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 1;
                bp.Groups = new FeatureGroup[] { FeatureGroup.AnimalCompanion };
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
            });
        }
    }
}
