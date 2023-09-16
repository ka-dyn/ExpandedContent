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
    internal class Zyphus {
        private static readonly BlueprintFeature DeathDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("a099afe1b0b32554199b230699a69525");
        private static readonly BlueprintFeature DestructionDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("6832681c9a91bf946a1d9da28c5be4b4");
        private static readonly BlueprintFeature EvilDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("351235ac5fc2b7e47801f63d117b656c");
        private static readonly BlueprintFeature PlantDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("0e03c2a03222b0b42acf96096b286327");
        private static readonly BlueprintFeature WarDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("3795653d6d3b291418164b27be88cb43");
        private static readonly BlueprintFeature BloodDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("BloodDomainAllowed");
        private static readonly BlueprintFeature UndeadDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("UndeadDomainAllowed");
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelNegativeAllowed = Resources.GetBlueprint<BlueprintFeature>("dab5255d809f77c4395afc2b713e9cd6");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
        private static readonly BlueprintCharacterClass DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
        private static readonly BlueprintFeature MythicIgnoreAlignmentRestrictions = Resources.GetBlueprintReference<BlueprintFeatureReference>("24e78475f0a243e1a810452d14d0a1bd");



        public static void AddZyphusFeature() {

            BlueprintItem MasterworkHeavyPick = Resources.GetBlueprint<BlueprintItem>("febe2a102d3b7be4fa8d6490b7fa8018");

            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintArchetype MantisZealotArchetype = Resources.GetModBlueprint<BlueprintArchetype>("MantisZealotArchetype");
            BlueprintArchetype ClawOfTheFalseWyrmArchetype = Resources.GetModBlueprint<BlueprintArchetype>("ClawOfTheFalseWyrmArchetype");
            BlueprintArchetype SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");

            BlueprintFeature HeavyPickProficiency = Resources.GetBlueprint<BlueprintFeature>("aeac272bf357c1247a51e9c56af7193b");
            var ZyphusIcon = AssetLoader.LoadInternal("Deities", "Icon_Zyphus.jpg");
            var ZyphusFeature = Helpers.CreateBlueprint<BlueprintFeature>("ZyphusFeature", (bp => {

                bp.SetName("Zyphus");
                bp.SetDescription("\nTitles: Grim Harvestman   " +
                    "\nAlignment: Neutral Evil   " +
                    "\nAreas of Concern: Accidental death, Graveyards, Tragedy   " +
                    "\nDomains: Death, Destruction, Evil, Plant, War   " +
                    "\nSubdomains: Blood, Catastrophe, Daemon, Decay, Murder, Undead   " +
                    "\nFavoured Weapon: Heavy pick   " +
                    "\nHoly Symbol: Pick axe made of a femur, a human skull & a large rib   " +
                    "\nSacred Animal: Vulture   " +
                    "\nZyphus is the malignant god of accidental death. A bitter enemy of the goddess Pharasma, he is worshiped by small cults throughout Golarion. " +
                    "These groups foment tragedy and bring about accidents in the hopes of strengthening the power of their god.Followers of the Grim Harvestman " +
                    "believe that the god arose from the first maddened soul to die a pointless death. It is written that since that time, every mortal that dies in " +
                    "the same meaningless way is destined to be absorbed by Zyphus after death. This puts him in direct competition with the goddess Pharasma, and his " +
                    "followers believe that once he has acquired enough souls he will be able to engineer the death of Pharasma herself. Due to shared interests and " +
                    "hatred of Pharasma, Zyphus is known to consort with Urgathoa, even seeing her as a mentor.");
                bp.m_Icon = ZyphusIcon;
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
                    c.Alignment = AlignmentMaskType.TrueNeutral | AlignmentMaskType.LawfulEvil | AlignmentMaskType.NeutralEvil | AlignmentMaskType.ChaoticEvil;
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
                    c.m_Feature = HeavyPickProficiency.ToReference<BlueprintFeatureReference>();
                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                               InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                               WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[1] { MasterworkHeavyPick.ToReference<BlueprintItemReference>() };
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
