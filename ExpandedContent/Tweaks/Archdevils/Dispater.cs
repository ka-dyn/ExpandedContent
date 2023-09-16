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
    internal class Dispater {

        private static readonly BlueprintFeature TrickeryDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("eaa368e08628a8641b16cd41cbd2cb33");
        private static readonly BlueprintFeature EvilDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("351235ac5fc2b7e47801f63d117b656c");
        private static readonly BlueprintFeature NobilityDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("e0471d01e73254a4ca23278705b75e57");
        private static readonly BlueprintFeature LawDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("092714336606cfc45a37d2ab39fabfa8");
        private static readonly BlueprintFeature ThieveryDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("ThieveryDomainAllowed");
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelNegativeAllowed = Resources.GetBlueprint<BlueprintFeature>("dab5255d809f77c4395afc2b713e9cd6");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
        private static readonly BlueprintCharacterClass DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
        private static readonly BlueprintFeature MythicIgnoreAlignmentRestrictions = Resources.GetBlueprint<BlueprintFeature>("24e78475f0a243e1a810452d14d0a1bd");



        public static void AddDispater() {

            
            BlueprintFeature HeavyMaceProficiency = Resources.GetBlueprint<BlueprintFeature>("3f18330d717ea0148b496ee8cc291a60");
            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintArchetype MantisZealotArchetype = Resources.GetModBlueprint<BlueprintArchetype>("MantisZealotArchetype");
            BlueprintArchetype ClawOfTheFalseWyrmArchetype = Resources.GetModBlueprint<BlueprintArchetype>("ClawOfTheFalseWyrmArchetype");
            BlueprintArchetype SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");

            BlueprintItem ColdIronHeavyMace = Resources.GetBlueprint<BlueprintItem>("4499f1b1dfc7a3d4ab1b95a13580e2e4");

            var DispaterIcon = AssetLoader.LoadInternal("Deities", "Icon_Dispater.jpg");
            var DispaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("DispaterFeature", (bp => {

                bp.SetName("Dispater");
                bp.SetDescription("\nTitles: Iron Lord, Father of Dis, First King, King of Iron, Asmodeus' Eye, Master of the Tower, Lord of the Second   " +
                    "\nRealm: Dis " +
                    "\nAlignment: Lawful Evil   " +
                    "\nAreas of Concern: Cities, Prisons, Rulership   " +
                    "\nDomains: Evil, Law, Nobility, Trickery   " +
                    "\nSubdomains: Deception, Devil, Leadership, Legislation, Thievery   " +
                    "\nFavoured Weapon: Heavy mace   " +
                    "\nProfane Symbol: Iron Nail, Crown and Ring   " +                    
                    "\nProfane Animal: Hound   " +
                    "\nThe outcast, the forlorn, the forsaken—all need but to call upon Dispater to have the path home revealed. Known as the Iron Lord, the Father of Dis, " +
                    "and the First King, Dispater welcomes all to his perfect city. Among the most active and cunning of Hell’s rulers, he directly oversees the administration " +
                    "and constant expansion of his vast metropolis, entertains dispossessed souls and the envoys of powerful lords from throughout the planes, and judges those " +
                    "who would f lout infernal law. One of Asmodeus’s oldest and most loyal allies, Dispater holds a favored place in the hierarchy of Hell and serves his lord " +
                    "by creating a city of such dark perfection as to surpass all godly imagining. Calm, creative, and wise, Dispater is also unforgiving, manipulative, and " +
                    "arrogant, blending the traits of the perfect ruler with those of the archfiend he is. He places great value on the concepts of rank and station, as well " +
                    "as the intricacies of courtly manners and lordly right. Despite his interest in conduct and enjoyment of deriding the uncouth, the Lord of the Second " +
                    "quickly dismisses such decorum to further his schemes, often confessing his boredom with Hell’s formalities to those he seeks to influence. Dispater " +
                    "stands out among the archfiends by holding respect for the concept of courtly love. Thrice during his rule has the Iron Lord taken a queen. His first " +
                    "bride was a fallen angel, though none, not even Dispater, can recall her face, name, or fate—a condition that greatly vexes the archfiend. His second, " +
                    "Feronia, was a demigoddess from the Plane of Fire, and after a tryst lasting but a few centuries, she left the First King—somewhat congenially—taking " +
                    "with her the babe who would become the empyreal lord Ragathiel. His current wife, the beauteous Erecura, was once mortal, but her gift of fateful " +
                    "visions allowed her to slip the bonds of death. The pair share a cordial romance, and she is one of Dispater’s closest advisors, revealing her visions " +
                    "to him in exchange for mysterious gifts.");
                bp.m_Icon = DispaterIcon;
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
                        NobilityDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        TrickeryDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        ThieveryDomainAllowed.ToReference<BlueprintUnitFactReference>()
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


                    c.m_Feature = HeavyMaceProficiency.ToReference<BlueprintFeatureReference>();

                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                               InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                               WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[1] { ColdIronHeavyMace.ToReference<BlueprintItemReference>() };
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
























































        

