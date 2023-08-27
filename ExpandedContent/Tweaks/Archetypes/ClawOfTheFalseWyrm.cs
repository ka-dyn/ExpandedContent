using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class ClawOfTheFalseWyrm {
        public static void AddClawOfTheFalseWyrm() {

            var DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
            var ProfaneBoonSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("ProfaneBoonSelection");
            var PaladinSpellLevels = Resources.GetBlueprint<BlueprintSpellsTable>("9aed4803e424ae8429c392d8fbfb88ff");
            var BloodragerSpellList = Resources.GetBlueprint<BlueprintSpellList>("98c05aeff6e3d384f8aec6d584973642");
            var SinfulAbsolutionUse = Resources.GetModBlueprint<BlueprintFeature>("SinfulAbsolutionUse");
            var CrueltySelection2 = Resources.GetModBlueprint<BlueprintFeatureSelection>("CrueltySelection2");
            var CrueltySelection4 = Resources.GetModBlueprint<BlueprintFeatureSelection>("CrueltySelection4");
            var DreadKnightChannelNegativeEnergyFeature = Resources.GetModBlueprint<BlueprintFeature>("DreadKnightChannelNegativeEnergyFeature");
            var AuraOfAbsolutionFeature = Resources.GetModBlueprint<BlueprintFeature>("AuraOfAbsolutionFeature");
            var AuraOfDepravityFeature = Resources.GetModBlueprint<BlueprintFeature>("AuraOfDepravityFeature");
            var DrakeCompanionFeatureRed = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureRed");
            var DrakeCompanionSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("DrakeCompanionSelection");
            var ClawOfTheFalseWyrmSpellbook = Helpers.CreateBlueprint<BlueprintSpellbook>("ClawOfTheFalseWyrmSpellbook", bp => {
                bp.Name = Helpers.CreateString($"ClawOfTheFalseWyrmSpellbook.Name", "Claw of the False Wyrm");
                bp.m_SpellsPerDay = PaladinSpellLevels.ToReference<BlueprintSpellsTableReference>();
                bp.m_SpellList = BloodragerSpellList.ToReference<BlueprintSpellListReference>();
                bp.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                bp.CastingAttribute = StatType.Charisma;
                bp.AllSpellsKnown = true;
                bp.CantripsType = CantripsType.Cantrips;
                bp.CasterLevelModifier = -3;
                bp.IsArcane = false;
            });
            var ClawOfTheFalseWyrmArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("ClawOfTheFalseWyrmArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"ClawOfTheFalseWyrmArchetype.Name", "Claw of the False Wyrm");
                bp.LocalizedDescription = Helpers.CreateString($"ClawOfTheFalseWyrmArchetype.Description", "The Claws of the False Wyrm are a cult made up of warpriests of Dahak who have been " +
                    "chosen to act as a destuctive foil to Apsu's Silver Champions. Tasked with hunting down the followers of Apsu the most powerful and destuctive of the Claws are granted young " +
                    "drakes to join them on their hunt. These Claws must constantly display their command over their charges as any signs of weakness could be seen as the chance for a drake to " +
                    "succeed its master.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"ClawOfTheFalseWyrmArchetype.Description", "The Claws of the False Wyrm are a cult made up of warpriests of Dahak who have been " +
                    "chosen to act as a destuctive foil to Apsu's Silver Champions. Tasked with hunting down the followers of Apsu the most powerful and destuctive of the Claws are granted young " +
                    "drakes to join them on their hunt. These Claws must constantly display their command over their charges as any signs of weakness could be seen as the chance for a drake to " +
                    "succeed its master.");
                bp.m_ReplaceSpellbook = ClawOfTheFalseWyrmSpellbook.ToReference<BlueprintSpellbookReference>();
            });            
            var RedDrakeCompanion = Helpers.CreateBlueprint<BlueprintFeature>("RedDrakeCompanion", bp => {
                bp.SetName("Red Drake Companion");
                bp.SetDescription("At 5th Level Dahak grants their claw a red drake companion. The Claw treats their dreadknight level as their effective " +
                    "druid level for this companion.");
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        DrakeCompanionFeatureRed.ToReference<BlueprintUnitFactReference>(),
                        DrakeCompanionSelection.ToReference<BlueprintUnitFactReference>()
                    };
                });
            });
            var Sleep = Resources.GetBlueprint<BlueprintAbility>("bb7ecad2d3d2c8247a38f44855c99061");
            var Invisibility = Resources.GetBlueprint<BlueprintAbility>("89940cde01689fb46946b2f8cd7b66b7");
            var ProtectionFromLawCommunal = Resources.GetBlueprint<BlueprintAbility>("8b8ccc9763e3cc74bbf5acc9c98557b9");
            var PerniciousPoisonSpell = Resources.GetBlueprint<BlueprintAbility>("dee3074b2fbfb064b80b973f9b56319e");
            var MagicFang = Resources.GetBlueprint<BlueprintAbility>("403cf599412299a4f9d5d925c7b9fb33");
            var Boneshaker = Resources.GetBlueprint<BlueprintAbility>("b7731c2b4fa1c9844a092329177be4c3");
            var DispelMagic = Resources.GetBlueprint<BlueprintAbility>("92681f181b507b34ea87018e8f7a528a");
            var Contagion = Resources.GetBlueprint<BlueprintAbility>("48e2744846ed04b4580be1a3343a5d3d");
            var MagicFangGreater = Resources.GetBlueprint<BlueprintAbility>("f1100650705a69c4384d3edd88ba0f52");
            var CallLightning = Resources.GetBlueprint<BlueprintAbility>("2a9ef0e0b5822a24d88b16673a267456");
            var DragonsBreath = Resources.GetBlueprint<BlueprintAbility>("5e826bcdfde7f82468776b55315b2403");
            var Confusion = Resources.GetBlueprint<BlueprintAbility>("cf6c901fb7acc904e85c63b342e9c949");
            var Poison = Resources.GetBlueprint<BlueprintAbility>("d797007a142a6c0409a74b064065a15e");
            var FreedomOfMovement = Resources.GetBlueprint<BlueprintAbility>("0087fc2d64b6095478bc7b8d7d512caf");
            var UnholyBlight = Resources.GetBlueprint<BlueprintAbility>("a02cf51787df937489ef5d4cf5970335");
            var DahaksGift = Helpers.CreateBlueprint<BlueprintFeature>("DahaksGift", bp => {
                bp.SetName("Dahaks's Gift");
                bp.SetDescription("The Claw gains a spellbook with the bloodrager spelllist and gains spellslots at the same rate as a paladin. All spells provided by Dahak's domains " +
                    "and subdomains are added to the Claw of the False Wyrm spellbook.");
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Sleep.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MagicFang.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PerniciousPoisonSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Boneshaker.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Invisibility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ProtectionFromLawCommunal.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MagicFangGreater.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DispelMagic.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Contagion.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = CallLightning.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = FreedomOfMovement.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DragonsBreath.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = UnholyBlight.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Poison.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Confusion.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.ReapplyOnLevelUp = true;
            });
            ClawOfTheFalseWyrmArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(4, SinfulAbsolutionUse, DreadKnightChannelNegativeEnergyFeature),
                    Helpers.LevelEntry(5, ProfaneBoonSelection),
                    Helpers.LevelEntry(6, CrueltySelection2),
                    Helpers.LevelEntry(10, SinfulAbsolutionUse),
                    Helpers.LevelEntry(11, AuraOfAbsolutionFeature),
                    Helpers.LevelEntry(12, CrueltySelection4),
                    Helpers.LevelEntry(16, SinfulAbsolutionUse),
                    Helpers.LevelEntry(17, AuraOfDepravityFeature),
                    Helpers.LevelEntry(18, CrueltySelection4)
            };
            ClawOfTheFalseWyrmArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, DahaksGift),
                    Helpers.LevelEntry(5, RedDrakeCompanion)
            };
            var DrakeCompanionProgression = Resources.GetModBlueprint<BlueprintProgression>("DrakeCompanionProgression");
            DrakeCompanionProgression.m_Archetypes = DrakeCompanionProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = ClawOfTheFalseWyrmArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            DrakeCompanionProgression.m_Classes = DrakeCompanionProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = DreadKnightClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            DreadKnightClass.m_Archetypes = DreadKnightClass.m_Archetypes.AppendToArray(ClawOfTheFalseWyrmArchetype.ToReference<BlueprintArchetypeReference>());
        }
    }
}
