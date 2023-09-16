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
    internal class BlackButterfly {
        private static readonly BlueprintFeature ChaosDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("8c7d778bc39fec642befc1435b00f613");
        private static readonly BlueprintFeature GoodDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("882521af8012fc749930b03dc18a69de");
        private static readonly BlueprintFeature LiberationDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("801ca88338451a546bca2ee59da87c53");
        private static readonly BlueprintFeature TravelDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("c008853fe044bd442ae8bd22260592b7");
        private static readonly BlueprintFeature RevolutionDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("RevolutionDomainAllowed");
        private static readonly BlueprintFeature StarsDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("StarsDomainAllowed");
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelPositiveAllowed = Resources.GetBlueprint<BlueprintFeature>("8c769102f3996684fb6e09a2c4e7e5b9");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
        private static readonly BlueprintFeature MythicIgnoreAlignmentRestrictions = Resources.GetBlueprint<BlueprintFeature>("24e78475f0a243e1a810452d14d0a1bd");



        public static void AddBlackButterflyFeature() {

            BlueprintItem MasterworkStarknife = Resources.GetBlueprint<BlueprintItem>("657eca867b9324c4ca46cbf9ca01b940");

            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintArchetype SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");

            BlueprintFeature StarknifeProficiency = Resources.GetBlueprint<BlueprintFeature>("7818ba3db79ac064e88fa14a2478b24b");
            var BlackButterflyIcon = AssetLoader.LoadInternal("Deities", "Icon_BlackButterfly.jpg");
            var BlackButterflyFeature = Helpers.CreateBlueprint<BlueprintFeature>("BlackButterflyFeature", (bp => {

                bp.SetName("Black Butterfly");
                bp.SetDescription("\nTitles: The Silence Between, Desna's Shadow   " +
                    "\nAlignment: Chaotic Good   " +
                    "\nAreas of Concern: Distance, Silence, Space   " +
                    "\nDomains: Chaos, Good, Liberation, Void   " +
                    "\nSubdomains: Azata, Freedom, Revolution, Stars   " +
                    "\nFavoured Weapon: Starknife   " +
                    "\nHoly Symbol: Black butterfly with star   " +
                    "\nSacred Animal: Butterfly   " +
                    "\nLegend tells that at the dawn of creation, Desna placed the stars in the sky. As the goddess worked, she realized " +
                    "she had created a pattern of spaces between the stars. When Desna placed the final star, a shadowy butterfly formed in " +
                    "the spaces between and sprang into existence. This is the Black Butterfly, Desna's Shadow. The Black Butterfly appears as " +
                    "the living silhouette of a graceful woman with white hair and eyes. Her expansive black butterfly wings hold reflections of " +
                    "all the stars, planets, and galaxies in creation. One of the stars on the Black Butterfly's wings is actually her magical " +
                    "starknife, Voidsedge, which can disappear from her wing and appear in her hand with a thought. The Silence Between knows the " +
                    "value of stillness. She has seen the great expanse of space, and its vast and awful silence is reflected in her ancient eyes. " +
                    "Those who live lives of silence, happily or unhappily, turn to the Black Butterfly as one who understands their solemn existence. " +
                    "Explorers venturing into strange territories offer prayers to the Black Butterfly as well. Desna's Shadow teaches that in the " +
                    "silences between breaths and thoughts, one can see one's true nature. While she despises all the forces of evil, she holds particular " +
                    "enmity for the entities of the Dark Tapestry, and it is said she possesses great knowledge about the Dark Tapestry to which she refuses " +
                    "to give voice.");
                bp.m_Icon = BlackButterflyIcon;
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
                    c.m_CharacterClass = InquistorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.NeutralGood | AlignmentMaskType.ChaoticGood | AlignmentMaskType.ChaoticNeutral;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ChannelPositiveAllowed.ToReference<BlueprintUnitFactReference>(),
                        ChaosDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        GoodDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        LiberationDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        TravelDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        RevolutionDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        StarsDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[] {
                        CrusaderSpellbook.ToReference<BlueprintSpellbookReference>(),
                        ClericSpellbook.ToReference<BlueprintSpellbookReference>(),
                        InquisitorSpellbook.ToReference<BlueprintSpellbookReference>()
                    };
                    c.m_IgnoreFact = MythicIgnoreAlignmentRestrictions.ToReference<BlueprintUnitFactReference>();
                    c.Alignment = AlignmentMaskType.NeutralGood | AlignmentMaskType.ChaoticGood | AlignmentMaskType.ChaoticNeutral;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();


                    c.m_Feature = StarknifeProficiency.ToReference<BlueprintFeatureReference>();

                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                               InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                               WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[1] { MasterworkStarknife.ToReference<BlueprintItemReference>() };
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
