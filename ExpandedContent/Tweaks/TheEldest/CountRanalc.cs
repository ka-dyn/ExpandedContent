using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.TheEldest {
    internal class CountRanalc {
        private static readonly BlueprintFeature ChaosDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("8c7d778bc39fec642befc1435b00f613");
        private static readonly BlueprintFeature DarknessDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("6d8e7accdd882e949a63021af5cde4b8");
        private static readonly BlueprintFeature NobilityDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("e0471d01e73254a4ca23278705b75e57");
        private static readonly BlueprintFeature TravelDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("c008853fe044bd442ae8bd22260592b7");
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelPositiveAllowed = Resources.GetBlueprint<BlueprintFeature>("8c769102f3996684fb6e09a2c4e7e5b9");
        private static readonly BlueprintFeature ChannelNegativeAllowed = Resources.GetBlueprint<BlueprintFeature>("dab5255d809f77c4395afc2b713e9cd6");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
        private static readonly BlueprintCharacterClass DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");



        public static void AddCountRanalcFeature() {

            BlueprintItem MasterworkRapier = Resources.GetBlueprint<BlueprintItem>("d8ecfcf0db3de304681f8632da4b6b8c");

            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype ClawOfTheFalseWyrmArchetype = Resources.GetModBlueprint<BlueprintArchetype>("ClawOfTheFalseWyrmArchetype");

            BlueprintFeature RapierProficiency = Resources.GetBlueprint<BlueprintFeature>("aed4f88b52ae0fb468895f90da854ad4");
            var CountRanalcIcon = AssetLoader.LoadInternal("Deities", "Icon_CountRanalc.jpg");
            var CountRanalcFeature = Helpers.CreateBlueprint<BlueprintFeature>("CountRanalcFeature", (bp => {

                bp.SetName("Count Ranalc");
                bp.SetDescription("\nTitles: The Traitor   " +
                    "\nAlignment: Chaotic Neutral   " +
                    "\nAreas of Concern: Betrayal, Exiles, Shadows   " +
                    "\nDomains: Chaos, Darkness, Nobility, Travel   " +
                    "\nSubdomains: Exploration, Loss, Martyr, Night   " +
                    "\nFavoured Weapon: Rapier   " +
                    "\nHoly Symbol: Eye with crescent moon pupil   " +
                    "\nSacred Animal: Bat   " +
                    "\nOver eons beyond mortal ken, many Eldest have been forced out of power or vanished into obscurity. Yet only one has managed to do both these " +
                    "things and still exert influence over both the First World and the planes beyond. The creature known as Count Ranalc, for reasons long lost to " +
                    "history—first rose to power in the early days of the multiverse as a physical embodiment of primal darkness and the chaos of creation. Tall and gaunt, " +
                    "constantly wrapped in inky shadow, he was always difficult for the other fey lords to work with, his arrogance born from both power and the knowledge " +
                    "that, in the end, his domain of darkness would consume all things. Still, the other Eldest respected his strength and the necessity of chaos in fueling " +
                    "the fires of existence, which makes it all the more surprising that they eventually banded together to exile him from the plane entirely. What " +
                    "Ranalc did to rouse the ire—or self-preservation—of the other Eldest remains one of the First World’s greatest mysteries, but it was at that point, " +
                    "millennia ago, that he was first given the appellation “the Traitor.” Cast out onto the Shadow Plane, Ranalc shocked the others by refusing to be " +
                    "beaten, instead creating a stronghold within the darkness and embracing his new role as lord of exiles and betrayal. From his darkened realm, he " +
                    "continued to grant power to his followers and influence both the First World and Material Plane. Yet even then the mystery of the dark lord was not " +
                    "yet complete. Though historical records dating all the way back to the Age of Anguish show evidence of Ranalc’s machinations on Golarion, he abruptly " +
                    "disappeared from both his shadowed court and the pages of history in the third century of the Age of Anguish. Whatever the truth, the fact that he " +
                    "continues to grant magic to his followers indicates that, wherever the missing demigod has gone, he has not yet met the same fate as Aroden.");
                bp.m_Icon = CountRanalcIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = FeralChampionArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.HideInUI = true;
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ClawOfTheFalseWyrmArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.ChaoticGood | AlignmentMaskType.TrueNeutral | AlignmentMaskType.ChaoticNeutral | AlignmentMaskType.ChaoticEvil;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChannelPositiveAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChannelNegativeAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChaosDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { DarknessDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { NobilityDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { TravelDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { CrusaderSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { ClericSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { InquisitorSpellbook.ToReference<BlueprintSpellbookReference>() };
                });

                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();


                    c.m_Feature = RapierProficiency.ToReference<BlueprintFeatureReference>();

                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                               InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                               WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[1] { MasterworkRapier.ToReference<BlueprintItemReference>() };
                    c.m_RestrictedByClass = new BlueprintCharacterClassReference[3] {
                                ClericClass.ToReference<BlueprintCharacterClassReference>(),
                                InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                                WarpriestClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
            }));

        }
    }

}
