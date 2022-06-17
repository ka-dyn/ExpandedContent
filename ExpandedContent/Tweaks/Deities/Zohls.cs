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
    internal class Zohls {
        private static readonly BlueprintFeature GoodDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("882521af8012fc749930b03dc18a69de");
        private static readonly BlueprintFeature KnowledgeDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("443d44b3e0ea84046a9bf304c82a0425");
        private static readonly BlueprintFeature LawDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("092714336606cfc45a37d2ab39fabfa8");
        private static readonly BlueprintFeature TravelDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("c008853fe044bd442ae8bd22260592b7");
        private static readonly BlueprintFeature ArchonDomainGoodAllowed = Resources.GetModBlueprint<BlueprintFeature>("ArchonDomainGoodAllowed");
        private static readonly BlueprintFeature ArchonDomainLawAllowed = Resources.GetModBlueprint<BlueprintFeature>("ArchonDomainLawAllowed");
        private static readonly BlueprintFeature StormDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("StormDomainAllowed");
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelPositiveAllowed = Resources.GetBlueprint<BlueprintFeature>("8c769102f3996684fb6e09a2c4e7e5b9");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
        private static readonly BlueprintCharacterClass PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");


        public static void AddZohlsFeature() {

            BlueprintItem MasterworkHeavyCrossbow = Resources.GetBlueprint<BlueprintItem>("adc1a081ba26d2a439a62d702437a40d");

            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintArchetype SilverChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SilverChampionArchetype");

            BlueprintFeature WeaponSpecializationHeavyCrossbow = Resources.GetBlueprint<BlueprintFeature>("f9129b3edcbe39948a27d4f7c125defa");
            var ZohlsIcon = AssetLoader.LoadInternal("Deities", "Icon_Zohls.jpg");
            var ZohlsFeature = Helpers.CreateBlueprint<BlueprintFeature>("ZohlsFeature", (bp => {

                bp.SetName("Zohls");
                bp.SetDescription("\nTitles: Verity   " +
                    "\nAlignment: Lawful Good   " +
                    "\nAreas of Concern: Determination, Investigation, Truth   " +
                    "\nDomains: Good, Knowledge, Law, Travel   " +
                    "\nSubdomains: Archon, Exploration, Judgment, Thought   " +
                    "\nFavoured Weapon: Heavy crossbow   " +
                    "\nHoly Symbol: Book with chequered page   " +
                    "\nSacred Animal: Wren   " +
                    "\nTo Zohls, the act of discovery is almost as important as the truth uncovered by it. The empyreal lord of determination and investigation, she " +
                    "revels in unwinding mysteries-the more complicated the better-as well as shedding light upon previously obscured events. Zohls sees existence as " +
                    "a series of interlocking puzzles and delights in discovering patterns. She is guided by logic more than instinct, and has trouble interacting with " +
                    "less perceptive beings, even fellow archons. She is an impatient empyreal lord who genuinely forgets that not everyone shares her talent for " +
                    "brilliant leaps oflogic. Zohls often appears practically dressed in a tunic and breeches and ties back her shining chestnut hair into braids. She " +
                    "favors clothing of black and white, representing the light of truth and the darkness of the unknown, and she reveals her celestial nature to the " +
                    "deserving by unfurling magnificent wings of parchment that never wrinkles. Investigators and researchers-who both seek the truth in their own ways-pay " +
                    "homage to the empyreal lord sometimes known simply as \"Verity.\" Her agents often appear incognito to give a crucial hint or provide a key piece of " +
                    "evidence to the faithful. Those who hide criminal deeds capture Zohls's ire most strongly, but anyone who seeks to keep acts, lore, or objects hidden " +
                    "may encounter interference from Zohls's agents. Zohls uses her abilities and her agents to uncover fiendish plots and unravel the machinations of evil, " +
                    "particularly devils. Zohls's palace on Heaven is filled with endless halls full of labeled evidence, research notes, maps covered in check marks , sketches " +
                    "of interesting individuals, and laboratory equipment. She rarely visits her fellow empyreal lords, finding them largely uninteresting and difficult to " +
                    "talk to.");
                bp.m_Icon = ZohlsIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = PriestOfBalance.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = FeralChampionArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.HideInUI = true;
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SilverChampionArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.LawfulGood | AlignmentMaskType.NeutralGood | AlignmentMaskType.LawfulNeutral;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChannelPositiveAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { GoodDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { KnowledgeDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { LawDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { TravelDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ArchonDomainGoodAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ArchonDomainLawAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { StormDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { CrusaderSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { ClericSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { InquisitorSpellbook.ToReference<BlueprintSpellbookReference>() };
                });

                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();


                    c.m_Feature = WeaponSpecializationHeavyCrossbow.ToReference<BlueprintFeatureReference>();

                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                               InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                               WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[1] { MasterworkHeavyCrossbow.ToReference<BlueprintItemReference>() };
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
