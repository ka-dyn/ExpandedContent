using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class Conqueror {
        public static void AddConqueror() {
            var DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
            var InspiredRage = Resources.GetBlueprint<BlueprintFeature>("1a639eadc2c3ed546bc4bb236864cd0c");
            var RagingSong = Resources.GetBlueprint<BlueprintFeature>("334cb75aa673d1d4bb279761c2ef5cf1");
            var FiendishMaw1d6Feature = Resources.GetBlueprint<BlueprintFeature>("a5f55fc653c389e4ea1c032342a507bd");
            var FiendTotemFeature = Resources.GetBlueprint<BlueprintFeature>("ce449404eeb4a7c499fbe0248056174f");
            var FiendishMaw1d8Feature = Resources.GetBlueprint<BlueprintFeature>("b78ddb104afa442408c520f84b0c0281");
            var DemonicConquestFeature = Resources.GetBlueprint<BlueprintFeature>("2bba973bcb1abdf40a0192c769b1cbc2");
            var FiendTotemGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("1105632657d94d940a43707a3a57b006");
            var FiendishMaw2d6Feature = Resources.GetBlueprint<BlueprintFeature>("594ac4d1c4067d74a881a1a4a7cfcb31");
            var FiendishMaw3d6Feature = Resources.GetBlueprint<BlueprintFeature>("486c52f5ef06ec54f82683ac221b2a6a");
            var SongOfStrength = Resources.GetBlueprint<BlueprintFeature>("45cfdf2474fe89f49892a72540614ce0");
            var SkaldMovePerformance = Resources.GetBlueprint<BlueprintFeature>("f24dc98de96671d4288cba09584e249b");
            var DirgeOfDoom = Resources.GetBlueprint<BlueprintFeature>("12a03dce517a43e45b7203c8f8d4e110");
            var SkaldSwiftPerformance = Resources.GetBlueprint<BlueprintFeature>("0c12540bf7e45f04faa84730136cb726");
            var SongOfTheFallen = Resources.GetBlueprint<BlueprintFeature>("9fc5d126524dbc84a90b1856707e2d87");
            var MasterSkald = Resources.GetBlueprint<BlueprintFeature>("ae4d45a39a91dee4fb4200d7a677d9a7");
            var SkaldRagePowerSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("2476514e31791394fa140f1a07941c96");
            var IconCommand = AssetLoader.LoadInternal("Skills", "Icon_Command.png");


            var ConquerorInspiredRageFeature = Helpers.CreateBlueprint<BlueprintFeature>("ConquerorInspiredRageFeature", bp => {
                bp.SetName("Profane Commandment");
                bp.SetDescription("At 1st level, a conqueror receives the ability to incite rage in those he rules over, as per the skald inspired rage class feature. " +
                    "Affected allies gain a +1 {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:MeleeAttack}melee attack{/g} and {g|Encyclopedia:Damage}damage rolls{/g} and a +1 " +
                    "bonus on Will {g|Encyclopedia:Saving_Throw}saving throws{/g}, but also take a -1 {g|Encyclopedia:Penalty}penalty{/g} to {g|Encyclopedia:Armor_Class}AC{/g}. While under the " +
                    "effects of inspired rage, allies other than the conqueror cannot use any ability that requires patience or {g|Encyclopedia:Concentration}concentration{/g}. At 4th level and " +
                    "every 4 levels thereafter, the song's bonuses on Will saves increase by 1; the penalty to AC doesn't change. At 8th and 16th levels, the song's bonuses to " +
                    "{g|Encyclopedia:Attack}attack{/g} and damage increase by 1. (Unlike the barbarian's rage ability, those affected are not fatigued after the song ends.)");
                bp.m_DescriptionShort = Helpers.CreateString($"ConquerorInspiredageFeature.Description", "At 1st level, a conqueror receives the ability to incite rage in those he rules over, as the skald inspired rage class feature.");
                bp.m_Icon = IconCommand;

                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { InspiredRage.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var ConquerorProfaneConquestFeature = Helpers.CreateBlueprint<BlueprintFeature>("ConquerorProfaneConquestFeature", bp => {
                bp.SetName("Profane Conquest");
                bp.SetDescription("At 7th level, the conqueror's commandments warp the minds of those who accept them even further, the {g|Encyclopedia:Strength}Strength{/g} {g|Encyclopedia:Bonus}bonuses{/g} of the " +
                    "conquerors incite rage increase by 2, however if affected creature stops {g|Encyclopedia:Attack}attacking{/g} or changes the target she suffers ({g|Encyclopedia:Dice}1d6{/g} + his level) " +
                    "points of {g|Encyclopedia:Damage}damage{/g}.");
                bp.m_DescriptionShort = Helpers.CreateString($"ConquerorInspiredageFeature.Description", "At 7th level, the conqueror's commandments warp the minds of those who accept them even further.");
                bp.m_Icon = DemonicConquestFeature.Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DemonicConquestFeature.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var ProfaneLordIcon = AssetLoader.LoadInternal("Skills", "Icon_ProfaneLord.png");
            var ProfaneLordFeature = Helpers.CreateBlueprint<BlueprintFeature>("ProfaneLordFeature", bp => {
                bp.SetName("Profane Lord");
                bp.SetDescription("At 20th level, a conqueror becomes a beacon of unyielding force. A conquerors inspired rage no longer gives allies a {g|Encyclopedia:Penalty}penalty{/g} to {g|Encyclopedia:Armor_Class}AC{/g}, " +
                    "nor limits what {g|Encyclopedia:Skills}skills{/g} or abilities they can use. Additionally, they act as if under a haste effect.");
                bp.m_DescriptionShort = Helpers.CreateString($"ConquerorInspiredageFeature.Description", "At 7th level, the conqueror's commandments warp the minds of those who accept them even further.");
                bp.m_Icon = ProfaneLordIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { MasterSkald.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var DreadKnightChannelNegativeEnergyFeature = Resources.GetModBlueprint<BlueprintFeature>("DreadKnightChannelNegativeEnergyFeature");
            var SinfulAbsolutionUse = Resources.GetModBlueprint<BlueprintFeature>("SinfulAbsolutionUse");
            var TouchOfProfaneCorruptionUse = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionUse");
            var AuraOfSinFeature = Resources.GetModBlueprint<BlueprintFeature>("AuraOfSinFeature");
            var ProfaneChampion = Resources.GetModBlueprint<BlueprintFeature>("ProfaneChampion");
            
            var ConquerorArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("ConquerorArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"ConquerorArchetype.Name", "Conqueror");
                bp.LocalizedDescription = Helpers.CreateString($"ConquerorArchetype.Description", "The Conqueror is bent on earthly conquest. They care nothing for the intricacies of divine spellcasting, " +
                    "but malevolent energy still surrounds them. Whether alone or at the head of a marauding host, these cruel warriors bring suffering and death.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"ConquerorArchetype.Description", "The Conqueror is bent on earthly conquest. They care nothing for the intricacies of divine spellcasting, " +
                    "but malevolent energy still surrounds them. Whether alone or at the head of a marauding host, these cruel warriors bring suffering and death.");
                bp.RemoveSpellbook = true;
                bp.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(4, DreadKnightChannelNegativeEnergyFeature),
                    Helpers.LevelEntry(7, SinfulAbsolutionUse),
                    Helpers.LevelEntry(10, TouchOfProfaneCorruptionUse),
                    Helpers.LevelEntry(13, SinfulAbsolutionUse),
                    Helpers.LevelEntry(16, TouchOfProfaneCorruptionUse),
                    Helpers.LevelEntry(14, AuraOfSinFeature),
                    Helpers.LevelEntry(20, ProfaneChampion)
                };
                bp.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, ConquerorInspiredRageFeature, RagingSong),
                    Helpers.LevelEntry(3, FiendishMaw1d6Feature),
                    Helpers.LevelEntry(6, FiendTotemFeature, SongOfStrength),
                    Helpers.LevelEntry(7, FiendishMaw1d8Feature, ConquerorProfaneConquestFeature, SkaldMovePerformance),
                    Helpers.LevelEntry(9, FiendTotemGreaterFeature),
                    Helpers.LevelEntry(10, DirgeOfDoom),
                    Helpers.LevelEntry(12, FiendishMaw2d6Feature, SkaldRagePowerSelection),
                    Helpers.LevelEntry(13, SkaldSwiftPerformance),
                    Helpers.LevelEntry(13, SongOfTheFallen),
                    Helpers.LevelEntry(15, SkaldRagePowerSelection),
                    Helpers.LevelEntry(17, FiendishMaw3d6Feature),
                    Helpers.LevelEntry(18, SkaldRagePowerSelection),
                    Helpers.LevelEntry(20, ProfaneLordFeature)
                };
            });
            DreadKnightClass.m_Archetypes = DreadKnightClass.m_Archetypes.AppendToArray(ConquerorArchetype.ToReference<BlueprintArchetypeReference>());
        }
    }
}
