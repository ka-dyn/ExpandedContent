using kadynsWOTRMods.Config;
using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Items;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kadynsWOTRMods.Tweaks.Archdevils {
    internal class Mephistopheles {

        private static readonly BlueprintFeature RuneDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("77637f81d6aa33b4f82873d7934e8c4b");
        private static readonly BlueprintFeature EvilDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("351235ac5fc2b7e47801f63d117b656c");
        private static readonly BlueprintFeature KnowledgeDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("443d44b3e0ea84046a9bf304c82a0425");
        private static readonly BlueprintFeature LawDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("092714336606cfc45a37d2ab39fabfa8");
        private static readonly BlueprintFeature CrusaderSpellbook = Resources.GetBlueprint<BlueprintFeature>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintFeature ClericSpellbook = Resources.GetBlueprint<BlueprintFeature>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintFeature InquisitorSpellbook = Resources.GetBlueprint<BlueprintFeature>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelNegativeAllowed = Resources.GetBlueprint<BlueprintFeature>("dab5255d809f77c4395afc2b713e9cd6");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");


        public static void AddMephistopheles() {

            if (ModSettings.AddedContent.Archdevils.IsDisabled("Mephistopheles")) { return; }

            BlueprintFeature TridentProficiency = Resources.GetBlueprint<BlueprintFeature>("f9565a97342ac594e9b6a495368c1a57");
            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintItem ColdIronTrident = Resources.GetBlueprint<BlueprintItem>("fed59d344fe866d42a51c128dc1cc86b");

            var MephistophelesIcon = AssetLoader.LoadInternal("Deities", "Icon_Mephistopheles.jpg");
            var MephistophelesFeature = Helpers.CreateBlueprint<BlueprintFeature>("MephistophelesFeature", (bp => {

                bp.SetName("Mephistopheles");
                bp.SetDescription("\nTitles: Crimson Son, Devil King, Lord of the Eighth, Merchant of Souls, Seneschal of Hell   " +
                    "\nRealm: Caina, Hell   " +
                    "\nAlignment: Lawful Evil   " +
                    "\nAreas of Concern: Contracts, Devils, Secrets   " +
                    "\nDomains: Evil, Knowledge, Law, Rune  " +
                    "\nSubdomains: Devil, Language, Memory, Thought  " +
                    "\nFavoured Weapon: Trident  " +
                    "\nProfane Symbol: Trident and Ring  " +
                    "\nProfane Animal: Mockingbird  " +
                    "\nProfane Colours: Red and Yellow " +
                    "\nThe archdevil Mephistopheles is the ruler of Caina, the Eighth Layer of Hell, where he keeps many of Hell's greatest secrets and contracts. " +
                    "Mephistopheles' official and true unholy symbol is, in essence, a crimson trident piercing a golden ring, with the three prongs of the trident alluding " +
                    "to the spires of Caina. His symbols are rarely seen, however, as owing one's allegiance to Mephistopheles is most often kept secret. " +
                    "Simpler, alternative versions of his unholy symbol exist, including a red sun eclipsed by three mountains; a tongue pierced with three studs; and a " +
                    "scale surmounted with a feather, topped by a bone. The latter evokes Mephistopheles' three sets of wings. Mephistopheles originated as the consciousness of " +
                    "Hell itself, predating Asmodeus' discovery of the plane. When Asmodeus and his followers entered Hell following their Exodus from Heaven, Asmodeus traveled " +
                    "into Caina and tore Hell's very flesh from its bones, reshaping it into a being he named Mephistopheles. The newly formed entity, the first true devil, pledged " +
                    "to oppose Asmodeus' enemies as if they were its own, and became one of Asmodeus' closest lieutenants. This story is recorded in the " +
                    "Book of the Damned.");
                bp.m_Icon = MephistophelesIcon;
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

                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.LawfulEvil | AlignmentMaskType.NeutralEvil | AlignmentMaskType.LawfulNeutral | AlignmentMaskType.TrueNeutral;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChannelNegativeAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { EvilDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { LawDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { RuneDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { KnowledgeDomainAllowed.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_BasicItems = new BlueprintItemReference[1] { ColdIronTrident.ToReference<BlueprintItemReference>() };
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

   

