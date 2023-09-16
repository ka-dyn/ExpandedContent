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
    internal class Barbatos {

        private static readonly BlueprintFeature MagicDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("08a5686378a87b64399d329ba4ef71b8");
        private static readonly BlueprintFeature EvilDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("351235ac5fc2b7e47801f63d117b656c");
        private static readonly BlueprintFeature TravelDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("c008853fe044bd442ae8bd22260592b7");
        private static readonly BlueprintFeature LawDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("092714336606cfc45a37d2ab39fabfa8");
        private static readonly BlueprintFeature FerocityDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("FerocityDomainAllowed");
        private static readonly BlueprintFeature ResolveDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("ResolveDomainAllowed");
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelNegativeAllowed = Resources.GetBlueprint<BlueprintFeature>("dab5255d809f77c4395afc2b713e9cd6");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
        private static readonly BlueprintCharacterClass DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
        private static readonly BlueprintFeature MythicIgnoreAlignmentRestrictions = Resources.GetBlueprint<BlueprintFeature>("24e78475f0a243e1a810452d14d0a1bd");



        public static void AddBarbatos() {


            BlueprintFeature QuarterstaffProficiency = Resources.GetBlueprint<BlueprintFeature>("aed4f88b52ae0fb468895f90da854ad4");
            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintArchetype MantisZealotArchetype = Resources.GetModBlueprint<BlueprintArchetype>("MantisZealotArchetype");
            BlueprintArchetype ClawOfTheFalseWyrmArchetype = Resources.GetModBlueprint<BlueprintArchetype>("ClawOfTheFalseWyrmArchetype");
            BlueprintArchetype SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");

            BlueprintItem MasterworkQuarterstaff = Resources.GetBlueprint<BlueprintItem>("ad1a532601f8b644991d5012adccee6c");

            var BarbatosIcon = AssetLoader.LoadInternal("Deities", "Icon_Barbatos.jpg");
            var BarbatosFeature = Helpers.CreateBlueprint<BlueprintFeature>("BarbatosFeature", (bp => {

                bp.SetName("Barbatos");
                bp.SetDescription("\nTitles: The Bearded Lord, The Iron Hierophant, The Keeper of Lost Children, The Lord of the First   " +
                    "\nRealm: Avernus " +
                    "\nAlignment: Lawful Evil   " +
                    "\nAreas of Concern: Animals, Corruption, Gateways   " +
                    "\nDomains: Evil, Law, Magic, Travel  " +
                    "\nSubdomains: Arcane, Devil, Divine, Trade " +
                    "\nFavoured Weapon: Quarterstaff  " +
                    "\nProfane Symbol: Three-eyed beard  " +
                    "\nProfane Animal: Raven  " +
                    "\nFew dare speak Barbatos’s name, as legend says he is the most likely of Hell’s archfiends to hear. Of Asmodeus’s lieutenants, the mysterious lord of Avernus has held " +
                    "his station for the shortest time, a term measured in millennia, but still a tenure too short for Hell’s elite to embrace the newcomer. As the Lord of the First, Barbatos " +
                    "holds the least enviable duties of any of the archfiends, charged with the dual Sisyphean tasks of disseminating the endless tides of accursed souls to their appropriate " +
                    "damnations and guarding Hell’s shores against incursion. He rules with a light hand, but brooks no disobedience, swiftly revealing any plot to oppose him. Those who repeatedly " +
                    "prove themselves untrustworthy are sapped of their vitality and hung upon the Penitent Cross, a massive, bleeding willow tree at the heart of his floating paradise of worms, " +
                    "the Promised Land. As Barbatos possesses uncanny knowledge of seemingly all that happens within his realm, legends tell of mortals who venture to Avernus to treat with him " +
                    "for the release of loved ones’ souls. Often the Lord of the First consents, but never without seeking a damning price, and rarely is what the archfiend releases not " +
                    "returned to him multiple fold. It is said that Barbatos can see into the Material Plane through any representation of his sigil, can hear 21 words spoken after his name, " +
                    "and knows the languages of all beasts. When called upon by desperate souls, he delights in sending servants to offer just enough lore or out-ofcontext information for a " +
                    "mortal to make her situation all the more hopeless. When he appears on the mortal realm he typically takes the shape of a heavily cloaked hermit or a woodland creature " +
                    "with a long, strange beard. In any guise he always retains some vestige of his natural form, by which the wary might recognize him.");
                bp.m_Icon = BarbatosIcon;
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
                        MagicDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        TravelDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        FerocityDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        ResolveDomainAllowed.ToReference<BlueprintUnitFactReference>()
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
                    c.m_Feature = QuarterstaffProficiency.ToReference<BlueprintFeatureReference>();
                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                        InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                        WarpriestClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[] {
                        MasterworkQuarterstaff.ToReference<BlueprintItemReference>()
                    };
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



