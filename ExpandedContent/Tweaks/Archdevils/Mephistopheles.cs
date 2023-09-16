using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.FactLogic;

namespace ExpandedContent.Tweaks.Archdevils {
    internal class Mephistopheles {

        private static readonly BlueprintFeature RuneDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("77637f81d6aa33b4f82873d7934e8c4b");
        private static readonly BlueprintFeature EvilDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("351235ac5fc2b7e47801f63d117b656c");
        private static readonly BlueprintFeature KnowledgeDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("443d44b3e0ea84046a9bf304c82a0425");
        private static readonly BlueprintFeature LawDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("092714336606cfc45a37d2ab39fabfa8");
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelNegativeAllowed = Resources.GetBlueprint<BlueprintFeature>("dab5255d809f77c4395afc2b713e9cd6");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
        private static readonly BlueprintCharacterClass DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
        private static readonly BlueprintFeature MythicIgnoreAlignmentRestrictions = Resources.GetBlueprintReference<BlueprintFeatureReference>("24e78475f0a243e1a810452d14d0a1bd");



        public static void AddMephistopheles() {


            BlueprintFeature TridentProficiency = Resources.GetBlueprint<BlueprintFeature>("f9565a97342ac594e9b6a495368c1a57");
            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintArchetype MantisZealotArchetype = Resources.GetModBlueprint<BlueprintArchetype>("MantisZealotArchetype");
            BlueprintArchetype ClawOfTheFalseWyrmArchetype = Resources.GetModBlueprint<BlueprintArchetype>("ClawOfTheFalseWyrmArchetype");
            BlueprintArchetype SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");

            BlueprintItem ColdIronTrident = Resources.GetBlueprint<BlueprintItem>("fed59d344fe866d42a51c128dc1cc86b");

            var MephistophelesIcon = AssetLoader.LoadInternal("Deities", "Icon_Mephistopheles.jpg");
            var MephistophelesFeature = Helpers.CreateBlueprint<BlueprintFeature>("MephistophelesFeature", (bp => {

                bp.SetName("Mephistopheles");
                bp.SetDescription("\nTitles: Crimson Son, Devil King, Lord of the Eighth, Merchant of Souls, Seneschal of Hell   " +
                    "\nRealm: Caina " +
                    "\nAlignment: Lawful Evil   " +
                    "\nAreas of Concern: Contracts, Devils, Secrets   " +
                    "\nDomains: Evil, Knowledge, Law, Rune  " +
                    "\nSubdomains: Devil, Language, Memory, Thought  " +
                    "\nFavoured Weapon: Trident  " +
                    "\nProfane Symbol: Trident and Ring  " +
                    "\nProfane Animal: Mockingbird  " +
                    "\nMephistopheles offers the world to those willing to pay his price. Seneschal of Hell and ally of man, the Lord of the Eighth promises anything, possessing the " +
                    "magic and resources to grant nearly any boon a mortal might imagine. Amid one’s greatest desires Mephistopheles plants the seeds of damnation, sealing each of his " +
                    "contracts in mortal blood and ending every deal richer in souls. He is called the Devil King, the Crimson Son, and the Merchant of Souls, and few can deny the deadly " +
                    "temptations and fiery rhetoric of Hell’s most fervent apostle. Upon Asmodeus’s arrival in Hell, he sought to learn what manner of land he had come to claim. As he " +
                    "explored the domains that would become Hell’s layers, he stripped the scorched skin of the land, molding it into a being of ashes and hellfire, and called it " +
                    "Mephistopheles. The primogenial devil, Mephistopheles knew the will of the Pit and that it welcomed Asmodeus and his hordes, revealing to him the secrets that would " +
                    "make his empire the ordered and unconquerable dominion he envisioned. Countless ages later, the Crimson Son continues to serve his creator obediently, his insight " +
                    "into all of Hell’s domains allowing him to presage changes in the planar terrain, the genesis of new diabolical breeds, and even more cultic whims of the infernal " +
                    "realm. Mephistopheles loathes the inhabitants of the Material Plane. Seeing mortals only as fuel for the infernal realm, he endlessly seeks souls to satisfy both " +
                    "his master Asmodeus and his mother, Hell. Taking pride in his sly and divisive words, Mephistopheles is the creator of the infernal contract. While the living " +
                    "inevitably believe they can elude such damning agreements, few souls have ever won their freedom. In truth, not only do mortals rarely have any hope of escape, " +
                    "but the boons of a contract often damn the bargainer’s soul long before Mephistopheles or his brethren must collect. In any case, even an entire lifetime of service " +
                    "means little to a patient, immortal being like Mephistopheles, and in the end stands as but a fraction of the time a damned soul will languish in the pits of Hell.");
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
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.HideInUI = true;
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = MantisZealotArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.HideInUI = true;
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ClawOfTheFalseWyrmArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.HideInUI = true;
                    c.m_CharacterClass = InquistorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.LawfulEvil | AlignmentMaskType.NeutralEvil | AlignmentMaskType.LawfulNeutral;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ChannelNegativeAllowed.ToReference<BlueprintUnitFactReference>(),
                        EvilDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        LawDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        KnowledgeDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        RuneDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[] {
                        CrusaderSpellbook.ToReference<BlueprintSpellbookReference>(),
                        ClericSpellbook.ToReference<BlueprintSpellbookReference>(),
                        InquisitorSpellbook.ToReference<BlueprintSpellbookReference>()
                    };
                    c.m_IgnoreFact = MythicIgnoreAlignmentRestrictions.ToReference<BlueprintUnitFactReference>();
                    c.Alignment = AlignmentMaskType.LawfulEvil | AlignmentMaskType.NeutralEvil | AlignmentMaskType.LawfulNeutral;
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

   

