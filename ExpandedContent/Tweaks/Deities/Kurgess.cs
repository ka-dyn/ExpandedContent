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
    internal class Kurgess {
        private static readonly BlueprintFeature CommunityDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("c87004460f3328c408d22c5ead05291f");
        private static readonly BlueprintFeature GoodDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("882521af8012fc749930b03dc18a69de");
        private static readonly BlueprintFeature LuckDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("d4e192475bb1a1045859c7664addd461");
        private static readonly BlueprintFeature StrengthDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("58d2867520de17247ac6988a31f9e397");
        private static readonly BlueprintFeature TravelDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("c008853fe044bd442ae8bd22260592b7");
        private static readonly BlueprintFeature ResolveDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("ResolveDomainAllowed");
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



        public static void AddKurgessFeature() {

            BlueprintItem MasterworkJavelin = Resources.GetBlueprint<BlueprintItem>("2c7b186e938933247a00e019732786c9");

            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintArchetype SilverChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SilverChampionArchetype");
            BlueprintArchetype SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");

            BlueprintFeature JavelinProficiency = Resources.GetBlueprint<BlueprintFeature>("8e85b6347caa4e84fa67d71f65a6a990");
            var KurgessIcon = AssetLoader.LoadInternal("Deities", "Icon_Kurgess.jpg");
            var KurgessFeature = Helpers.CreateBlueprint<BlueprintFeature>("KurgessFeature", (bp => {

                bp.SetName("Kurgess");
                bp.SetDescription("\nTitles: The Strong Man   " +
                    "\nAlignment: Neutral Good   " +
                    "\nAreas of Concern: Bravery, Competition, Sport   " +
                    "\nDomains: Community, Good, Luck, Strength, Travel   " +
                    "\nSubdomains: Agathion, Competition, Family, Fate, Home, Resolve, Trade   " +
                    "\nFavoured Weapon: Javelin   " +
                    "\nHoly Symbol: Flexing arm with chain   " +
                    "\nSacred Animal: Horse   " +
                    "\nKurgess, the Strong Man, was once a mortal of peerless physical prowess. As legends of his sporting victories " +
                    "and his noble death spread across the Inner Sea region—and even other planes—athletes began looking to Kurgess " +
                    "as a role model, and competitions were often dedicated to him as the embodiment of honor and athleticism. Shortly " +
                    "thereafter, Cayden Cailean and Desna raised Kurgess to godhood, crowning him with deific powers. Why they did this " +
                    "remains up for debate, but more than a few scholars whisper that Kurgess might actually be the two deities’ " +
                    "half-mortal son. He stands as patron of and inspiration for all who engage in physical challenges.");
                bp.m_Icon = KurgessIcon;
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


                    c.m_Feature = JavelinProficiency.ToReference<BlueprintFeatureReference>();

                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                               InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                               WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[1] { MasterworkJavelin.ToReference<BlueprintItemReference>() };
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
