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

namespace ExpandedContent.Tweaks.Monitors {
    internal class Ilsurrish {
        private static readonly BlueprintFeature AnimalDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("9f05f9da2ea5ae44eac47d407a0000e5");
        private static readonly BlueprintFeature ArtificeDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("9656b1c7214180f4b9a6ab56f83b92fb");
        private static readonly BlueprintFeature ChaosDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("8c7d778bc39fec642befc1435b00f613");
        private static readonly BlueprintFeature PlantDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("0e03c2a03222b0b42acf96096b286327");
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelPositiveAllowed = Resources.GetBlueprint<BlueprintFeature>("8c769102f3996684fb6e09a2c4e7e5b9");
        private static readonly BlueprintFeature ChannelNegativeAllowed = Resources.GetBlueprint<BlueprintFeature>("dab5255d809f77c4395afc2b713e9cd6");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
        private static readonly BlueprintCharacterClass RangerClass = Resources.GetBlueprint<BlueprintCharacterClass>("cda0615668a6df14eb36ba19ee881af6");
        private static readonly BlueprintCharacterClass DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
        private static readonly BlueprintFeature MythicIgnoreAlignmentRestrictions = Resources.GetBlueprint<BlueprintFeature>("24e78475f0a243e1a810452d14d0a1bd");

        public static void AddIlsurrishFeature() {

            BlueprintItem StandardQuarterstaff = Resources.GetBlueprint<BlueprintItem>("ada85dae8d12eda4bbe6747bb8b5883c");

            BlueprintArchetype ClawOfTheFalseWyrmArchetype = Resources.GetModBlueprint<BlueprintArchetype>("ClawOfTheFalseWyrmArchetype");
            BlueprintArchetype SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");
            BlueprintArchetype DivineTrackerArchetype = Resources.GetModBlueprint<BlueprintArchetype>("DivineTrackerArchetype");

            BlueprintFeature ImprovedUnarmedStrike = Resources.GetBlueprint<BlueprintFeature>("7812ad3672a4b9a4fb894ea402095167");
            var IlsurrishIcon = AssetLoader.LoadInternal("Deities", "Icon_Ilsurrish.jpg");
            var IlsurrishFeature = Helpers.CreateBlueprint<BlueprintFeature>("IlsurrishFeature", (bp => {

                bp.SetName("Il'surrish");
                bp.SetDescription("\nTitles: The Wanderer   " +
                    "\nAlignment: Chaotic Neutral   " +
                    "\nForm: Protean Lord  " +
                    "\nAreas of Concern: Formlessness, Loss of control, Potential " +
                    "\nDomains: Animal, Artifice, Chaos, Plant  " +
                    "\nFavoured Weapon: Unarmed strike   " +
                    "\nHoly Symbol: Cerulean sphere dotted with stars   " +
                    "\nIl’surrish the Wanderer seldom manifests physically, but when it does, it usually appears as a lesser protean wrought of furious cerulean light. More commonly " +
                    "the Wanderer possesses a demon, devil, or archon who, overwhelmed, burns like a metaphysical candle from the inside out, consumed by the experience. Those few " +
                    "who survive recall only a beautiful and horrific sensory avalanche—an experience on par with a keketar’s description of its first shallow communion with the Speakers " +
                    "of the Depths. Il’surrish’s largely protean clergy are collectively known as the Asomatous Clutch, and many of them are gifted with insubstantiality as well. " +
                    "Il’surrish’s mobile realm, the Labyrinth of Light and Loss, is an ever-shifting amalgamation of hundreds of castles, a serpentine mating ball of architecture " +
                    "orbited by shattered, petrified qlippoth and the broken remnants of the primal inevitable Argreth the Burning Monolith. The realm only rarely ventures beyond the " +
                    "deep Maelstrom, usually during rare conclaves of gods and protean lords.");
                bp.m_Icon = IlsurrishIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = false;
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
                    c.Alignment = AlignmentMaskType.ChaoticGood | AlignmentMaskType.TrueNeutral | AlignmentMaskType.ChaoticNeutral | AlignmentMaskType.ChaoticEvil;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChannelPositiveAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChannelNegativeAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChaosDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { AnimalDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ArtificeDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { PlantDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { CrusaderSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { ClericSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { InquisitorSpellbook.ToReference<BlueprintSpellbookReference>() };
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
