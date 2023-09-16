using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Items;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Blueprints.Classes.Spells;

namespace ExpandedContent.Tweaks.Deities {
    internal class Apsu {

        private static readonly BlueprintFeature GoodDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("882521af8012fc749930b03dc18a69de");
        private static readonly BlueprintFeature TravelDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("c008853fe044bd442ae8bd22260592b7");
        private static readonly BlueprintFeature ArtificeDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("9656b1c7214180f4b9a6ab56f83b92fb");
        private static readonly BlueprintFeature LawDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("092714336606cfc45a37d2ab39fabfa8");
        private static readonly BlueprintFeature ScalykindDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("ScalykindDomainAllowed");
        private static readonly BlueprintFeature ArchonDomainGoodAllowed = Resources.GetModBlueprint<BlueprintFeature>("ArchonDomainGoodAllowed");
        private static readonly BlueprintFeature ArchonDomainLawAllowed = Resources.GetModBlueprint<BlueprintFeature>("ArchonDomainLawAllowed");
        private static readonly BlueprintFeature DragonDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("DragonDomainAllowed");
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelPositiveAllowed = Resources.GetBlueprint<BlueprintFeature>("8c769102f3996684fb6e09a2c4e7e5b9");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
        private static readonly BlueprintFeature MythicIgnoreAlignmentRestrictions = Resources.GetBlueprintReference<BlueprintFeatureReference>("24e78475f0a243e1a810452d14d0a1bd");




        public static void AddApsu() {





            BlueprintFeature ImprovedUnarmedStrike = Resources.GetBlueprint<BlueprintFeature>("7812ad3672a4b9a4fb894ea402095167");
            BlueprintFeature QuarterstaffProficiency = Resources.GetBlueprint<BlueprintFeature>("aed4f88b52ae0fb468895f90da854ad4");
            BlueprintFeature BloodlineDraconicSilverArcana = Resources.GetBlueprint<BlueprintFeature>("1af96d3ab792e3048b5e0ca47f3a524b");
            BlueprintItem MasterworkQuarterstaff = Resources.GetBlueprint<BlueprintItem>("ad1a532601f8b644991d5012adccee6c");
            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintArchetype SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");

            var ApsuIcon = AssetLoader.LoadInternal("Deities", "Icon_Apsu.jpg");
            var ApsuFeature = Helpers.CreateBlueprint<BlueprintFeature>("ApsuFeature", (bp => {

                bp.SetName("Apsu");
                bp.SetDescription("\nTitles: Waybringer, The Exiled Wyrm, Maker of All   " +
                "\nRealm: Immortal Ambulatory, a traveling demi-plane   " +
                "\n{g|Encyclopedia:Alignment}Alignment{/g}: Lawful Good   " +
                "\nAreas of Concern: Good Dragons, Glory, Leadership, Peace   " +
                "\nDomains: Artifice, Good, Law, Travel, Scalykind   " +
                "\nSubdomains: Archon, Construct, Dragon, Exploration, Judgment, Toll, Trade   " +
                "\nFavoured Weapon: Bite, Quarterstaff (Arcana)  " +
                "\nHoly Symbol: Silver Dragon above Pool   " +
                "\nSacred Colours: Metallic Colors  " +
                "\nApsu is the patron deity of all good and metallic dragons, and one of the oldest gods of the Great Beyond. " +
                "Along with Tiamat, he is believed to be one of the two original creator " +
                "beings of the multiverse. According to draconic lore, in the dawn of time there flowed two waters, fresh and salt, " +
                "which became Apsu and Tiamat, parents of the first gods. However, their eldest son Dahak came to Hell to rampage, " +
                "then killed his siblings, whose shattered remains fell into the Material Plane and became the first metallic dragons. " +
                "Enraged, the fresh water descended upon the Material Plane to confront his son, saying the eternal words: " +
                "\"I shall then be Apsu, for I am the first\". " +
                "\nAppearance: Apsu appears as a regal silver dragon dwarfing the largest great wyrms. His scales sparkle with a pearlescent glow. ");
                bp.m_Icon = ApsuIcon;
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
                    c.Alignment = AlignmentMaskType.LawfulGood | AlignmentMaskType.LawfulNeutral | AlignmentMaskType.NeutralGood | AlignmentMaskType.TrueNeutral;
                });                
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ChannelPositiveAllowed.ToReference<BlueprintUnitFactReference>(),
                        GoodDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        LawDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        TravelDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        ArtificeDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        ScalykindDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        ArchonDomainGoodAllowed.ToReference<BlueprintUnitFactReference>(),
                        ArchonDomainLawAllowed.ToReference<BlueprintUnitFactReference>(),
                        DragonDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[] {
                        CrusaderSpellbook.ToReference<BlueprintSpellbookReference>(),
                        ClericSpellbook.ToReference<BlueprintSpellbookReference>(),
                        InquisitorSpellbook.ToReference<BlueprintSpellbookReference>()
                    };
                    c.m_IgnoreFact = MythicIgnoreAlignmentRestrictions.ToReference<BlueprintUnitFactReference>();
                    c.Alignment = AlignmentMaskType.LawfulGood | AlignmentMaskType.LawfulNeutral | AlignmentMaskType.NeutralGood | AlignmentMaskType.TrueNeutral;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();

                    c.m_Feature = BloodlineDraconicSilverArcana.ToReference<BlueprintFeatureReference>();

                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                        InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                        WarpriestClass.ToReference<BlueprintCharacterClassReference>()
                    };
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
                    c.m_BasicItems = new BlueprintItemReference[1] { MasterworkQuarterstaff.ToReference<BlueprintItemReference>() };
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

           
       

 



















    

