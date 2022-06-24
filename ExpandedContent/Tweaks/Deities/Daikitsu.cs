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

namespace ExpandedContent.Tweaks.Deities {
    internal class Daikitsu {
        private static readonly BlueprintFeature AnimalDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("9f05f9da2ea5ae44eac47d407a0000e5");
        private static readonly BlueprintFeature ArtificeDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("9656b1c7214180f4b9a6ab56f83b92fb");
        private static readonly BlueprintFeature CommunityDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("c87004460f3328c408d22c5ead05291f");
        private static readonly BlueprintFeature PlantDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("0e03c2a03222b0b42acf96096b286327");
        private static readonly BlueprintFeature WeatherDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("9dfdfd4904e98fa48b80c8f63ec2cf11");
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelPositiveAllowed = Resources.GetBlueprint<BlueprintFeature>("8c769102f3996684fb6e09a2c4e7e5b9");
        private static readonly BlueprintFeature ChannelNegativeAllowed = Resources.GetBlueprint<BlueprintFeature>("dab5255d809f77c4395afc2b713e9cd6");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
        private static readonly BlueprintCharacterClass DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");



        public static void AddDaikitsuFeature() {


            BlueprintItem MasterworkFlail = Resources.GetBlueprint<BlueprintItem>("433ab4d3f4c7393488981a4a8b48bec5");

            BlueprintArchetype ClawOfTheFalseWyrmArchetype = Resources.GetModBlueprint<BlueprintArchetype>("ClawOfTheFalseWyrmArchetype");

            BlueprintFeature FlailProficiency = Resources.GetBlueprint<BlueprintFeature>("6d273f46bce2e0f47a0958810dc4c7d9");
            var DaikitsuIcon = AssetLoader.LoadInternal("Deities", "Icon_Daikitsu.jpg");
            var DaikitsuFeature = Helpers.CreateBlueprint<BlueprintFeature>("DaikitsuFeature", (bp => {

                bp.SetName("Daikitsu");
                bp.SetDescription("\nTitles: Lady of Foxes   " +
                    "\nAlignment: Neutral   " +
                    "\nAreas of Concern: Agriculture, Craftsmanship, Kitsune, Rice" +
                    "\nEdict: Ensure the health of crops and vegetation, perfect a craft or trade, leave offerings for foxes, celebrate the turning of the seasons   " +
                    "\nDomains: Animal, Artifice, Community, Plant, Weather   " +
                    "\nSubdomains: Construct, Family, Fur, Growth, Home, Industry, Seasons   " +
                    "\nFavoured Weapon: Flail   " +
                    "\nHoly Symbol: Nine-tailed fox   " +
                    "\nSacred Animal: Fox   " +
                    "Daikitsu is widely worshiped in Tian Xia, as she is the goddess of rice, a staple food in those lands, as well as of " +
                    "agriculture and craftsmanship. Farmers pray to Daikitsu for good harvests, smiths and craftsmen seek her blessing for their " +
                    "creations, and families ask for her protection for their homes and families. Known as the Lady of Foxes, Daikitsu usually " +
                    "appears as beautiful kitsune woman with snow-white fur and nine tails. She is also the patron of kitsune, who venerate her " +
                    "as the mother of their race.");
                bp.m_Icon = DaikitsuIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.HideInUI = true;
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ClawOfTheFalseWyrmArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.NeutralGood | AlignmentMaskType.LawfulNeutral | AlignmentMaskType.TrueNeutral | AlignmentMaskType.ChaoticNeutral | AlignmentMaskType.NeutralEvil;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChannelPositiveAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChannelNegativeAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { AnimalDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ArtificeDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { CommunityDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { PlantDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { WeatherDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { CrusaderSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { ClericSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { InquisitorSpellbook.ToReference<BlueprintSpellbookReference>() };
                });

                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();


                    c.m_Feature = FlailProficiency.ToReference<BlueprintFeatureReference>();

                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                               InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                               WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[1] { MasterworkFlail.ToReference<BlueprintItemReference>() };
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
