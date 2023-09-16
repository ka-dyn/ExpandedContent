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
    internal class Irez {
        private static readonly BlueprintFeature GoodDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("882521af8012fc749930b03dc18a69de");
        private static readonly BlueprintFeature LuckDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("d4e192475bb1a1045859c7664addd461");
        private static readonly BlueprintFeature MagicDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("08a5686378a87b64399d329ba4ef71b8");
        private static readonly BlueprintFeature RuneDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("77637f81d6aa33b4f82873d7934e8c4b");
        private static readonly BlueprintFeature AgathionDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("AgathionDomainAllowed");
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelPositiveAllowed = Resources.GetBlueprint<BlueprintFeature>("8c769102f3996684fb6e09a2c4e7e5b9");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
        private static readonly BlueprintCharacterClass PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
        private static readonly BlueprintFeature MythicIgnoreAlignmentRestrictions = Resources.GetBlueprint<BlueprintFeature>("24e78475f0a243e1a810452d14d0a1bd");



        public static void AddIrezFeature() {

            BlueprintItem MasterworkDart = Resources.GetBlueprint<BlueprintItem>("8a51768893cc2fb4cab0589da671c073");

            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintArchetype SilverChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SilverChampionArchetype");
            BlueprintArchetype SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");

            BlueprintFeature DartProficiency = Resources.GetBlueprint<BlueprintFeature>("f029268a6639a0745a849e5147734088");
            var IrezIcon = AssetLoader.LoadInternal("Deities", "Icon_Irez.jpg");
            var IrezFeature = Helpers.CreateBlueprint<BlueprintFeature>("IrezFeature", (bp => {

                bp.SetName("Irez");
                bp.SetDescription("\nTitles: Lady of Inscribed Wonder   " +
                    "\nAlignment: Neutral Good   " +
                    "\nAreas of Concern: Cards, Scribes, Spells   " +
                    "\nEdict: Read fortunes, practice calligraphy, devise and study runes.   " +
                    "\nDomains: Good, Luck, Magic, Rune   " +
                    "\nSubdomains: Agathion, Fate, Language, Wards   " +
                    "\nFavoured Weapon: Dart   " +
                    "\nHoly Symbol: Fan of parchment strips   " +
                    "\nSacred Animal: Roc   " +
                    "\nIrez emerged from the reeds along the coast of the Sea of No Shadows in Nirvana. She understands the mystical symbolism behind runes, " +
                    "particularly how runes can empower arcane magic. Harrow readers and gamblers pay homage to the Lady of lnscribed Wonder, and her agents " +
                    "teach the exacting art of calligraphy to favored mortals. Irez still makes her home among the reeds where she first appeared. Her palace is a " +
                    "fantastical amalgamation of woven reed floors, paper walls, and bamboo roofs, and while it looks fragile enough to blow away in a stiffbreeze, " +
                    "it has stood undamaged for thousands of years.");
                bp.m_Icon = IrezIcon;
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
                        ChfannelNegativeAllowed.ToReference<BlueprintUnitFactReference>(),
                        EviflDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        LawDfomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        AirDofmainAllowed.ToReference<BlueprintUnitFactReference>(),
                        DeathDfomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        DeathDfomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        DeathDfomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        DeathDfomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        WindDomfainAllowed.ToReference<BlueprintUnitFactReference>(),
                        UndeadDofmainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[] {
                        CrusaderSpellbook.ToReference<BlueprintSpellbookReference>(),
                        ClericSpellbook.ToReference<BlueprintSpellbookReference>(),
                        InquisitorSpellbook.ToReference<BlueprintSpellbookReference>()
                    };
                    c.m_IgnoreFact = MythicIgnoreAlignmentRestrictions.ToReference<BlueprintUnitFactReference>();
                    c.Alignment = gggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();


                    c.m_Feature = DartProficiency.ToReference<BlueprintFeatureReference>();

                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                               InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                               WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[1] { MasterworkDart.ToReference<BlueprintItemReference>() };
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
