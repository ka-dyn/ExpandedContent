using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.FactLogic;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class SoldierOfGaia {
        public static void AddSoldierOfGaia() {

            var WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
            var WarpriestSpelllist = Resources.GetBlueprint<BlueprintSpellList>("c5a1b8df32914d74c9b44052ba3e686a");
            var WarpriestSpellLevels = Resources.GetBlueprint<BlueprintSpellsTable>("c73a394ec54adc243aef8ac967e39324");
            var WarpriestSpontaneousSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("2d26a3364d65c4e4e9fb470172f638a9");
            var DruidSpontaneousSummonIcon = Resources.GetBlueprint<BlueprintFeature>("b296531ffe013c8499ad712f8ae97f6b").Icon;
            var SecondBlessingSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("b7ce4a67287cda746a59b31c042305cf");
            var SacredArmorFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("35e2d9525c240ce4c8ae47dd387b6e53");
            var SacredArmorEnchantPlus2 = Resources.GetBlueprintReference<BlueprintFeatureReference>("ec327c67f6a6b2f49a8ca218466a8818");
            var SacredArmorEnchantPlus3 = Resources.GetBlueprintReference<BlueprintFeatureReference>("bd292463fa74d664086f0a3e4e425c47");
            var SacredArmorEnchantPlus4 = Resources.GetBlueprintReference<BlueprintFeatureReference>("ee65ff63443ce42488515db6a43fee5e");
            var SacredArmorEnchantPlus5 = Resources.GetBlueprintReference<BlueprintFeatureReference>("1e560784dfcb00f4da1415bbad3226c3");

            var SoldierOfGaiaArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("SoldierOfGaiaArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"SoldierOfGaiaArchetype.Name", "Soldier of Golarion");
                bp.LocalizedDescription = Helpers.CreateString($"SoldierOfGaiaArchetype.Description", "Every war needs soldiers. Most people don’t know it, but a war rages " +
                    "every day between the forces of the natural world and those that would defile and destroy it. Whether these enemies be the obscene monstrosities of other " +
                    "planes or the foulness known as “civilization”, Golarion calls upon a few mortal emissaries to serve as her warriors in this fight.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"SoldierOfGaiaArchetype.Description", "Every war needs soldiers. Most people don’t know it, but a war " +
                    "rages every day between the forces of the natural world and those that would defile and destroy it. Whether these enemies be the obscene monstrosities " +
                    "of other planes or the foulness known as “civilization”, Golarion calls upon a few mortal emissaries to serve as her warriors in this fight.");
            });


            var SoldierOfGaiaSpelllist = Helpers.CreateBlueprint<BlueprintSpellList>("SoldierOfGaiaSpelllist", bp => {//Fill in mod support to get all spells
                bp.IsMythic = false;
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                };
            });
            var SoldierOfGaiaSpellbook = Helpers.CreateBlueprint<BlueprintSpellbook>("SoldierOfGaiaSpellbook", bp => {
                bp.Name = Helpers.CreateString($"SoldierOfGaiaSpellbook.Name", "Soldier of Golarion");
                bp.m_SpellsPerDay = WarpriestSpellLevels.ToReference<BlueprintSpellsTableReference>();
                bp.m_SpellList = SoldierOfGaiaSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                bp.CastingAttribute = StatType.Wisdom;
                bp.AllSpellsKnown = true;
                bp.CantripsType = CantripsType.Orisions;
                bp.CasterLevelModifier = 0;
                bp.IsArcane = false;
            });
            SoldierOfGaiaArchetype.m_ReplaceSpellbook = SoldierOfGaiaSpellbook.ToReference<BlueprintSpellbookReference>();

            var SummonNatureISingle = Resources.GetBlueprintReference<BlueprintAbilityReference>("c6147854641924442a3bb736080cfeb6");
            var SummonNatureIISingle = Resources.GetBlueprintReference<BlueprintAbilityReference>("848bd9df8b2643143a7020be7cde8800");
            var SummonNatureIIISingle = Resources.GetBlueprintReference<BlueprintAbilityReference>("6db23a29c0c55c546a0feaef0c8d33d6");
            var SummonNatureIVSingle = Resources.GetBlueprintReference<BlueprintAbilityReference>("71dfb899a04db614e9db458ed4e43f56");
            var SummonNatureVSingle = Resources.GetBlueprintReference<BlueprintAbilityReference>("28ea1b2e0c4a9094da208b4c186f5e4f");
            var SummonNatureVISingle = Resources.GetBlueprintReference<BlueprintAbilityReference>("060afb9e13d8a3547ad0dd20c407c0a5");
            var SummonNatureIId3 = Resources.GetBlueprintReference<BlueprintAbilityReference>("b8ac9c653789b2a46ad85a075734c0e2");
            var SummonNatureIIId3 = Resources.GetBlueprintReference<BlueprintAbilityReference>("06d11dfa15e63bd41b01e09d5464ee8f");
            var SummonNatureIVd3 = Resources.GetBlueprintReference<BlueprintAbilityReference>("eb259941d7c2c5144844a31e72810642");
            var SummonNatureVd3 = Resources.GetBlueprintReference<BlueprintAbilityReference>("03e8e9605925b7140bdd331232b78d25");
            var SummonNatureVId3 = Resources.GetBlueprintReference<BlueprintAbilityReference>("2aab2a0c280ed3e408a09967ec6bb281");
            var SummonNatureIIId4Plus1 = Resources.GetBlueprintReference<BlueprintAbilityReference>("bb1bac85be6b1e44eafdc54a3b757c3e");
            var SummonNatureIVd4Plus1 = Resources.GetBlueprintReference<BlueprintAbilityReference>("3050599c1ca9a9b40940a9426d4f861b");
            var SummonNatureVd4Plus1 = Resources.GetBlueprintReference<BlueprintAbilityReference>("87c64591b0e6f7542807429d14bb1723");
            var SummonNatureVId4Plus1 = Resources.GetBlueprintReference<BlueprintAbilityReference>("7aefdbd7e0933b744b9c85566d16e504");
            var SoldierOfGaiaSpontaneousCasting = Helpers.CreateBlueprint<BlueprintFeature>("SoldierOfGaiaSpontaneousCasting", bp => {
                bp.SetName("Spontaneous Casting");
                bp.SetDescription("A soldier of Gaia can expend any prepared spell of 1st level or higher to cast a summon nature’s ally spell of that level.");
                bp.m_Icon = DruidSpontaneousSummonIcon;
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        SummonNatureISingle,
                        SummonNatureIISingle,
                        SummonNatureIIISingle,
                        SummonNatureIVSingle,
                        SummonNatureVSingle,
                        SummonNatureVISingle,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonNatureIId3,
                        SummonNatureIIId3,
                        SummonNatureIVd3,
                        SummonNatureVd3,
                        SummonNatureVId3,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonNatureIIId4Plus1,
                        SummonNatureIVd4Plus1,
                        SummonNatureVd4Plus1,
                        SummonNatureVId4Plus1,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
            });

            var AirBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("e1ff99dc3aeaa064e8eecde51c1c4773");
            var AnimalBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("9d991f8374c3def4cb4a6287f370814d");
            var EarthBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("73c37a22bc9a523409a47218d507acf6");
            var FireBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("2368212fa3856d74589e924d3e2074d8");
            var PlantBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("4cd28bbb761f490fa418d471383e38c7");
            var WaterBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("0f457943bb99f9b48b709c90bfc0467e");
            var WeatherBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("4172d92c598de1d47aa2c0dd51c05e24");
            var SoldierOfGaiaBlessingSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("", bp => {
                bp.SetName("Soldier's Blessing");
                bp.SetDescription("At least one of a soldier’s blessings must be drawn from the following list: Air, Animal, Earth, Fire, Plant, Water, or Weather. " +
                    "\nThe soldier cannot worship a deity that does not offer at least one of those blessings. ");
                bp.AddFeatures(AirBlessingFeature, AnimalBlessingFeature, EarthBlessingFeature, FireBlessingFeature, PlantBlessingFeature, WaterBlessingFeature, WeatherBlessingFeature);
                bp.m_Icon = SecondBlessingSelection.Icon;
                bp.Mode = SelectionMode.OnlyNew;
                bp.Group = FeatureGroup.WarpriestBlessing;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
            });









            SoldierOfGaiaArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, WarpriestSpontaneousSelection, SecondBlessingSelection),
                    Helpers.LevelEntry(7, SacredArmorFeature),
                    Helpers.LevelEntry(10, SacredArmorEnchantPlus2),
                    Helpers.LevelEntry(13, SacredArmorEnchantPlus3),
                    Helpers.LevelEntry(16, SacredArmorEnchantPlus4),
                    Helpers.LevelEntry(19, SacredArmorEnchantPlus5)
            };
            SoldierOfGaiaArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, SoldierOfGaiaSpontaneousCasting, SoldierOfGaiaBlessingSelection),
                    Helpers.LevelEntry(7, SoldierOfGaiaFriendOfTheForest )
            };

            if (ModSettings.AddedContent.Archetypes.IsDisabled("Soldier of Gaia")) { return; }
            WarpriestClass.m_Archetypes = WarpriestClass.m_Archetypes.AppendToArray(SoldierOfGaiaArchetype.ToReference<BlueprintArchetypeReference>());
        }

    }
}
