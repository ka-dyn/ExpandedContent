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
    internal class Shizuru {
        private static readonly BlueprintFeature GloryDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("2418251fa9c8ada4bbfbaaf5c90ac200");
        private static readonly BlueprintFeature GoodDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("882521af8012fc749930b03dc18a69de");
        private static readonly BlueprintFeature LawDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("092714336606cfc45a37d2ab39fabfa8");
        private static readonly BlueprintFeature ReposeDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("076ba1e3a05fac146acfc956a9f41e95");
        private static readonly BlueprintFeature SunDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("e28412c548ff21a49ac5b8b792b0aa9b");
        private static readonly BlueprintFeature ArchonDomainGoodAllowed = Resources.GetModBlueprint<BlueprintFeature>("ArchonDomainGoodAllowed");
        private static readonly BlueprintFeature ArchonDomainLawAllowed = Resources.GetModBlueprint<BlueprintFeature>("ArchonDomainLawAllowed");
        private static readonly BlueprintFeature HeroismDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("HeroismDomainAllowed");
        private static readonly BlueprintFeature RevelationDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("RevelationDomainAllowed");
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



        public static void AddShizuruFeature() {

            BlueprintItem MasterworkLongsword = Resources.GetBlueprint<BlueprintItem>("571c56d11dafbb04094cbaae659974b5");
            BlueprintItem MasterworkDuelingsword = Resources.GetBlueprint<BlueprintItem>("354f5109f1da7d24c830f37ef3b56cb8");

            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintArchetype SilverChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SilverChampionArchetype");
            BlueprintArchetype SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");
            BlueprintArchetype DivineTrackerArchetype = Resources.GetModBlueprint<BlueprintArchetype>("DivineTrackerArchetype");

            BlueprintFeature LongswordProficiency = Resources.GetBlueprint<BlueprintFeature>("62e27ffd9d53e14479f73da29760f64e");
            BlueprintFeature DuelingswordProficiency = Resources.GetBlueprint<BlueprintFeature>("9c37279588fd9e34e9c4cb234857492c");
            var ShizuruIcon = AssetLoader.LoadInternal("Deities", "Icon_Shizuru.jpg");
            var ShizuruFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShizuruFeature", (bp => {

                bp.SetName("Shizuru");
                bp.SetDescription("\nTitles: Empress of Heaven   " +
                    "\nAlignment: Lawful Good   " +
                    "\nAreas of Concern: Ancestors, Honor, The Sun, Swordplay   " +
                    "\nDomains: Glory, Good, Law, Repose, Sun   " +
                    "\nSubdomains: Ancestors, Archon, Day, Heroism, Honor, Light, Revelation  " +
                    "\nFavoured Weapon: Katana (Longsword & Dueling sword)   " +
                    "\nHoly Symbol: Katana in front of the sun   " +
                    "\nSacred Animal: Carp   " +
                    "\nShizuru is the Empress of Heaven, goddess of ancestors and the sun. She is also the goddess of honor and swordplay, " +
                    "and is the patron of samurai and other honorable swordfighters. Shizuru loves the moon god Tsukiyo, so much so that she " +
                    "returned him to life when he was slain by his brother Fumeiyoshi. The two lovers are rarely together, however, just as night " +
                    "and day are forever divided, but the two deities are able to rejoin, if only briefly, during rare solar eclipses—a sacred time " +
                    "for both faiths. Shizuru’s worship is popular throughout the Dragon Empires, but her faith is strongest in Minkai, for the " +
                    "emperors of that land revere Shizuru as their divine ancestor. Shizuru is often depicted in art as a beautiful samurai, as a " +
                    "magnificent dragon with golden scales, or as both simultaneously.");
                bp.m_Icon = ShizuruIcon;
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
                    c.Alignment = AlignmentMaskType.LawfulGood | AlignmentMaskType.NeutralGood | AlignmentMaskType.LawfulNeutral;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ChannelPositiveAllowed.ToReference<BlueprintUnitFactReference>(),
                        GloryDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        GoodDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        LawDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        ReposeDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        SunDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        ArchonDomainGoodAllowed.ToReference<BlueprintUnitFactReference>(),
                        ArchonDomainLawAllowed.ToReference<BlueprintUnitFactReference>(),
                        HeroismDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        RevelationDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[] {
                        CrusaderSpellbook.ToReference<BlueprintSpellbookReference>(),
                        ClericSpellbook.ToReference<BlueprintSpellbookReference>(),
                        InquisitorSpellbook.ToReference<BlueprintSpellbookReference>()
                    };
                    c.m_IgnoreFact = MythicIgnoreAlignmentRestrictions.ToReference<BlueprintUnitFactReference>();
                    c.Alignment = AlignmentMaskType.LawfulGood | AlignmentMaskType.NeutralGood | AlignmentMaskType.LawfulNeutral;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = LongswordProficiency.ToReference<BlueprintFeatureReference>();
                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                               InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                               WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = DuelingswordProficiency.ToReference<BlueprintFeatureReference>();
                    c.Level = 1;
                    c.m_Archetypes = null;
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
                    c.m_BasicItems = new BlueprintItemReference[2] { 
                        MasterworkLongsword.ToReference<BlueprintItemReference>(),
                        MasterworkDuelingsword.ToReference<BlueprintItemReference>() 
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
