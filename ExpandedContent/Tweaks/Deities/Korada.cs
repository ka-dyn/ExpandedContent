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

namespace ExpandedContent.Tweaks.Deities {
    internal class Korada {
        private static readonly BlueprintFeature GoodDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("882521af8012fc749930b03dc18a69de");
        private static readonly BlueprintFeature HealingDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("73ae164c388990c43ade94cfe8ed5755");
        private static readonly BlueprintFeature MagicDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("08a5686378a87b64399d329ba4ef71b8");
        private static readonly BlueprintFeature ProtectionDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("d4ce7592bd12d63439907ad64e986e59");
        private static readonly BlueprintFeature RestorationDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("RestorationDomainAllowed");
        private static readonly BlueprintFeature AgathionDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("AgathionDomainAllowed");
        private static readonly BlueprintFeature DefenseDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("DefenseDomainAllowed");
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelPositiveAllowed = Resources.GetBlueprint<BlueprintFeature>("8c769102f3996684fb6e09a2c4e7e5b9");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
        private static readonly BlueprintCharacterClass PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
        private static readonly BlueprintCharacterClass RangerClass = Resources.GetBlueprint<BlueprintCharacterClass>("cda0615668a6df14eb36ba19ee881af6");
        private static readonly BlueprintFeature MythicIgnoreAlignmentRestrictions = Resources.GetBlueprint<BlueprintFeature>("24e78475f0a243e1a810452d14d0a1bd");



        public static void AddKoradaFeature() {

            BlueprintItem StandardQuarterstaff = Resources.GetBlueprint<BlueprintItem>("ada85dae8d12eda4bbe6747bb8b5883c");

            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintArchetype SilverChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SilverChampionArchetype");
            BlueprintArchetype SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");
            BlueprintArchetype DivineTrackerArchetype = Resources.GetModBlueprint<BlueprintArchetype>("DivineTrackerArchetype");

            BlueprintFeature ImprovedUnarmedStrike = Resources.GetBlueprint<BlueprintFeature>("7812ad3672a4b9a4fb894ea402095167");
            var KoradaIcon = AssetLoader.LoadInternal("Deities", "Icon_Korada.jpg");
            var KoradaFeature = Helpers.CreateBlueprint<BlueprintFeature>("KoradaFeature", (bp => {

                bp.SetName("Korada");
                bp.SetDescription("\nTitles: The Open Hand of Harmony   " +
                    "\nAlignment: Neutral Good   " +
                    "\nAreas of Concern: Foresight, Forgiveness, Peace   " +
                    "\nDomains: Good, Healing, Magic, Protection   " +
                    "\nSubdomains: Agathion, Defense, Divine, Restoration   " +
                    "\nFavoured Weapon: Unarmed strikes   " +
                    "\nHoly Symbol: Lotus above two figures    " +
                    "\nSacred Animal: Monkey   " +
                    "\nKorada is neither young nor old, only present and wise. Sparse light brown fur covers his body, and his three monkey tails constantly writhe " +
                    "behind him like the flickering flames of a fire. He carries no weapons; when spurred to anger, the Open Hand of Harmony uses only his hands and " +
                    "millennia of martial training to defend himself, refusing to cause lethal harm to his attacker. Korada's holy symbol has several incarnations based on " +
                    "the viewer's region and the local ideals of enlightenment and peace, though the most common representation is that of two kneeling figures before a lotus. " +
                    "Many who are weary from a lifetime of imbalance or violence turn to Korada in search of peace. Retired soldiers, their hands stained with blood, lay down " +
                    "their weapons forever in the name of this empyreal lord. Likewise, diplomats in strange lands, individuals facing difficult moral choices, and champions of " +
                    "evil who long for absolution all find comfort in the teachings of the Open Hand of Harmony. While true repentance is necessary for evil to turn to good, " +
                    "Korada believes that no mortals are beyond redemption. Korada is said to possess the gift of foresight, but he rarely acts on these strange visions. He " +
                    "believes the struggle to change is always worthwhile, even if such a journey does not always end in success. It is the process, not the result, that matters " +
                    "most to Korada and his followers. When he is not teaching lessons of harmony to his devotees, Korada resides in Nirvana, in the heart of the Dream Lotus-an " +
                    "efflorescent palace whose blossoming violet walls always drip with cool, golden nectar that is said to calm even the wildest of spirits.");
                bp.m_Icon = KoradaIcon;
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
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.HideInUI = true;
                    c.m_CharacterClass = InquistorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.LawfulGood | AlignmentMaskType.NeutralGood | AlignmentMaskType.ChaoticGood | AlignmentMaskType.TrueNeutral;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ChannelPositiveAllowed.ToReference<BlueprintUnitFactReference>(),
                        GoodDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        HealingDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        MagicDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        ProtectionDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        RestorationDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        AgathionDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        DefenseDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[] {
                        CrusaderSpellbook.ToReference<BlueprintSpellbookReference>(),
                        ClericSpellbook.ToReference<BlueprintSpellbookReference>(),
                        InquisitorSpellbook.ToReference<BlueprintSpellbookReference>()
                    };
                    c.m_IgnoreFact = MythicIgnoreAlignmentRestrictions.ToReference<BlueprintUnitFactReference>();
                    c.Alignment = AlignmentMaskType.LawfulGood | AlignmentMaskType.NeutralGood | AlignmentMaskType.ChaoticGood | AlignmentMaskType.TrueNeutral;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = ImprovedUnarmedStrike.ToReference<BlueprintFeatureReference>();
                    c.Level = 1;
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        DivineTrackerArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                        WarpriestClass.ToReference<BlueprintCharacterClassReference>(),
                        RangerClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[1] { StandardQuarterstaff.ToReference<BlueprintItemReference>() };
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
