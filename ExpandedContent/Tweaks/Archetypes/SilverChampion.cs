using ExpandedContent.Config;
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
    internal class SilverChampion {
        public static void AddSilverChampion() {

            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var PaladinDivineBondSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("ad7dc4eba7bf92f4aba23f716d7a9ba6");
            var PaladinSpelllist = Resources.GetBlueprint<BlueprintSpellList>("9f5be2f7ea64fe04eb40878347b147bc");
            var PaladinSpellLevels = Resources.GetBlueprint<BlueprintSpellsTable>("9aed4803e424ae8429c392d8fbfb88ff");
            var SmiteEvilAdditionalUse = Resources.GetBlueprint<BlueprintFeature>("0f5c99ffb9c084545bbbe960b825d137");
            var SelectionMercy = Resources.GetBlueprint<BlueprintFeatureSelection>("02b187038a8dce545bb34bbfb346428d");
            var ChannelEnergyPaladinFeature = Resources.GetBlueprint<BlueprintFeature>("cb6d55dda5ab906459d18a435994a760");
            var AuraOfJusticeFeature = Resources.GetBlueprint<BlueprintFeature>("9f13fdd044ccb8a439f27417481cb00e");
            var AuraOfRighteousnessFeature = Resources.GetBlueprint<BlueprintFeature>("6bd4a71232014254e80726f3a3756962");
            var DrakeCompanionFeatureSilver = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureSilver");
            var DrakeCompanionSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("DrakeCompanionSelection");



            var SilverChampionArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("SilverChampionArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"SilverChampionArchetype.Name", "Silver Champion");
                bp.LocalizedDescription = Helpers.CreateString($"SilverChampionArchetype.Description", "Paladins who serve as priests of Apsu are almost always on the move, wandering from " +
                    "place to place and trying to show their dedication to the Waybringer in their deeds rather than depending on mere words. More rarely a drake is so inspired by a paladin " +
                    "of Apsu that it chooses to assist the paladin in all her actions. These drake allies see the benefit of a silver champion remaining mobile and understand the powerful " +
                    "threats the champion must face, and in time even consent to serve their chosen paladins as a companion.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"SilverChampionArchetype.Description", "Paladins who serve as priests of Apsu are almost always on the move, wandering from " +
                    "place to place and trying to show their dedication to the Waybringer in their deeds rather than depending on mere words. More rarely a drake is so inspired by a paladin " +
                    "of Apsu that it chooses to assist the paladin in all her actions. These drake allies see the benefit of a silver champion remaining mobile and understand the powerful " +
                    "threats the champion must face, and in time even consent to serve their chosen paladins as a companion.");                
            });
            
            var SilverChampionSpellbook = Helpers.CreateBlueprint<BlueprintSpellbook>("SilverChampionSpellbook", bp => {
                bp.Name = Helpers.CreateString($"SilverChampionSpellbook.Name", "Silver Champion");
                bp.m_SpellsPerDay = PaladinSpellLevels.ToReference<BlueprintSpellsTableReference>();
                bp.m_SpellList = PaladinSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                bp.CastingAttribute = StatType.Charisma;
                bp.AllSpellsKnown = true;
                bp.CantripsType = CantripsType.Cantrips;
                bp.CasterLevelModifier = -3;
                bp.IsArcane = false;
            });
            SilverChampionArchetype.m_ReplaceSpellbook = SilverChampionSpellbook.ToReference<BlueprintSpellbookReference>();
            var SilverDrakeCompanion = Helpers.CreateBlueprint<BlueprintFeature>("SilverDrakeCompanion", bp => {
                bp.SetName("Silver Drake Companion");
                bp.SetDescription("At 5th Level Apsu grants their champion with a silver drake companion. The Silver Champion treats their paladin level as their effective " +
                    "druid level for this companion.");
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        DrakeCompanionFeatureSilver.ToReference<BlueprintUnitFactReference>(),
                        DrakeCompanionSelection.ToReference<BlueprintUnitFactReference>()
                    };
                });
            });
            var Command = Resources.GetBlueprint<BlueprintAbility>("feb70aab86cc17f4bb64432c83737ac2");
            var ExpeditiousRetreat = Resources.GetBlueprint<BlueprintAbility>("4f8181e7a7f1d904fbaea64220e83379");
            var Longstrider = Resources.GetBlueprint<BlueprintAbility>("14c90900b690cac429b229efdf416127");
            var MagicFang = Resources.GetBlueprint<BlueprintAbility>("403cf599412299a4f9d5d925c7b9fb33");
            var AlignWeaponLawful = Resources.GetBlueprint<BlueprintAbility>("859b9e68620893545a235eceb439b7be");
            var AlignWeaponGood = Resources.GetBlueprint<BlueprintAbility>("d89d049fc8fec1a48a41aa0aa4905340");
            var Castigate = Resources.GetBlueprint<BlueprintAbility>("ce4c4e52c53473549ae033e2bb44b51a");
            var MagicFangGreater = Resources.GetBlueprint<BlueprintAbility>("f1100650705a69c4384d3edd88ba0f52");
            var DimensionDoor = Resources.GetBlueprint<BlueprintAbility>("5bdc37e4acfa209408334326076a43bc");
            var DragonsBreath = Resources.GetBlueprint<BlueprintAbility>("5e826bcdfde7f82468776b55315b2403");
            var HolySmite = Resources.GetBlueprint<BlueprintAbility>("ad5ed5ea4ec52334a94e975a64dad336");
            var Poison = Resources.GetBlueprint<BlueprintAbility>("d797007a142a6c0409a74b064065a15e");
            var ApsusGift = Helpers.CreateBlueprint<BlueprintFeature>("ApsusGift", bp => {
                bp.SetName("Apsu's Gift");
                bp.SetDescription("All spells provided by Apsu's domains and subdomains are added to the Silver Champions spellbook.");
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Command.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ExpeditiousRetreat.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Longstrider.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MagicFang.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = AlignWeaponLawful.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = AlignWeaponGood.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Castigate.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MagicFangGreater.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DimensionDoor.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DragonsBreath.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HolySmite.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = Poison.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
            });
            SilverChampionArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(4, SmiteEvilAdditionalUse, ChannelEnergyPaladinFeature),
                    Helpers.LevelEntry(5, PaladinDivineBondSelection),
                    Helpers.LevelEntry(6, SelectionMercy),
                    Helpers.LevelEntry(10, SmiteEvilAdditionalUse),
                    Helpers.LevelEntry(11, AuraOfJusticeFeature),
                    Helpers.LevelEntry(12, SelectionMercy),
                    Helpers.LevelEntry(16, SmiteEvilAdditionalUse),
                    Helpers.LevelEntry(17, AuraOfRighteousnessFeature),
                    Helpers.LevelEntry(18, SelectionMercy)
            };
            SilverChampionArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, ApsusGift),
                    Helpers.LevelEntry(5, SilverDrakeCompanion)                    
            };
            var DrakeCompanionProgression = Resources.GetModBlueprint<BlueprintProgression>("DrakeCompanionProgression");
            DrakeCompanionProgression.m_Archetypes = DrakeCompanionProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = SilverChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });

            if (ModSettings.AddedContent.Archetypes.IsDisabled("Silver Champion")) { return; }
            PaladinClass.m_Archetypes = PaladinClass.m_Archetypes.AppendToArray(SilverChampionArchetype.ToReference<BlueprintArchetypeReference>());
        }
    }
}
