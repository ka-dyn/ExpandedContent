using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
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
    internal class Ragadahn {
        private static readonly BlueprintFeature ScalykindDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("ScalykindDomainAllowed");
        private static readonly BlueprintFeature ChaosDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("8c7d778bc39fec642befc1435b00f613");
        private static readonly BlueprintFeature EvilDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("351235ac5fc2b7e47801f63d117b656c");
        private static readonly BlueprintFeature WaterDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("8f49469c40e2c6e4db61296558e08966");
        private static readonly BlueprintFeature DragonDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("DragonDomainAllowed");
        private static readonly BlueprintFeature CrusaderSpellbook = Resources.GetBlueprint<BlueprintFeature>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintFeature ClericSpellbook = Resources.GetBlueprint<BlueprintFeature>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintFeature InquisitorSpellbook = Resources.GetBlueprint<BlueprintFeature>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelNegativeAllowed = Resources.GetBlueprint<BlueprintFeature>("dab5255d809f77c4395afc2b713e9cd6");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");



        public static void AddRagadahnFeature() {

            BlueprintItem MasterworkTrident = Resources.GetBlueprint<BlueprintItem>("cd1b1365e098ca44aab3f8974c7d5a39");

            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");

            BlueprintFeature TridentProficiency = Resources.GetBlueprint<BlueprintFeature>("f9565a97342ac594e9b6a495368c1a57");
            var RagadahnIcon = AssetLoader.LoadInternal("Deities", "Icon_Ragadahn.jpg");
            var RagadahnFeature = Helpers.CreateBlueprint<BlueprintFeature>("RagadahnFeature", (bp => {

                bp.SetName("Ragadahn");
                bp.SetDescription("\nTitles: The Water Lord, Serpent King, The World Serpent, The Father of Dragons   " +
                    "\nAlignment: Chaotic Evil   " +
                    "\nAreas of Concern: Linnorms, Oceans, Spirals   " +
                    "\nDomains: Chaos, Evil, Scalykind , Water   " +
                    "\nSubdomains: Ancestors, Dragon, Oceans, Venom   " +
                    "\nFavoured Weapon: Whip (Trident)   " +
                    "\nHoly Symbol: Blue ouroboros   " +
                    "\nSacred Animal: Sea snake   " +
                    "\nMany adjectives can describe Ragadahn, but “humble” is not one of them. Fond of titles, he is also called the Serpent King, the Water Lord, " +
                    "and even the Father of Dragons. This last is considered heretical by most metallic and chromatic dragons, yet many linnorms pay homage to him " +
                    "as their progenitor, and few of the great dragons—or anyone else—would dare enter the First World’s deep seas and oceans without his permission. " +
                    "Ragadahn himself resembles an enormous, streamlined linnorm with flippers instead of legs, his scales a deep, shimmering blue-green. While his " +
                    "home is the deep trench-city of Karaphas, he claims all of the seas and lakes of the First World—and, to some extent, of the Material Plane—as " +
                    "his personal realm. All creatures who live in or enter deep waters are thus subject to his authority, though in truth he cares little for the " +
                    "dealings of ordinary fey or mortals, and few ever glimpse his majesty. That said, any land-dwellers wishing to speak with him are best advised to " +
                    "call his name from a sea cliff or peninsula rather than approach his home directly, lest they be accused of trespassing. Most stories of the Water " +
                    "Lord tend to caricature him as the epitome of draconic arrogance, yet these are quick to note his wisdom as well—for despite his tempestuous nature, " +
                    "Ragadahn is a consummate scholar of the unknown and unknowable. He believes that life first arose in the oceans, and that the secrets to eternity " +
                    "and existence remain in their depths. He’s fond of symbolism, particularly the unending ouroboros and the ever-descending spiral, and behind his " +
                    "fearsome facade lies a keen mind and a wealth of knowledge regarding extinct aquatic cultures from across the planes.");
                bp.m_Icon = RagadahnIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = PriestOfBalance.ToReference<BlueprintArchetypeReference>();
                });
                
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.ChaoticNeutral | AlignmentMaskType.NeutralEvil | AlignmentMaskType.ChaoticEvil;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChannelNegativeAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ScalykindDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChaosDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { EvilDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { WaterDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { DragonDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { CrusaderSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { ClericSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { InquisitorSpellbook.ToReference<BlueprintSpellbookReference>() };
                });

                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();


                    c.m_Feature = TridentProficiency.ToReference<BlueprintFeatureReference>();

                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                               InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                               WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[1] { MasterworkTrident.ToReference<BlueprintItemReference>() };
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
