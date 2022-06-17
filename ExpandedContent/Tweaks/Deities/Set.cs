using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Deities {
    internal class Set {
        private static readonly BlueprintFeature DarknessDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("6d8e7accdd882e949a63021af5cde4b8");
        private static readonly BlueprintFeature DeathDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("a099afe1b0b32554199b230699a69525");
        private static readonly BlueprintFeature EvilDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("351235ac5fc2b7e47801f63d117b656c");
        private static readonly BlueprintFeature MadnessDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("c346bcc77a6613040b3aa915b1ceddec");
        private static readonly BlueprintFeature WeatherDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("9dfdfd4904e98fa48b80c8f63ec2cf11");
        private static readonly BlueprintFeature StormDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("StormDomainAllowed");
        private static readonly BlueprintFeature UndeadDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("UndeadDomainAllowed");
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelNegativeAllowed = Resources.GetBlueprint<BlueprintFeature>("dab5255d809f77c4395afc2b713e9cd6");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");



        public static void AddSetFeature() {

            BlueprintItem MasterworkLongspear = Resources.GetBlueprint<BlueprintItem>("9f1545b033149e6429cc9c29354fd9f1");

            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintArchetype MantisZealotArchetype = Resources.GetModBlueprint<BlueprintArchetype>("MantisZealotArchetype");

            BlueprintFeature LongSpearProficiency = Resources.GetBlueprint<BlueprintFeature>("9c7b33404dc0ac1449ee2732657b722a");
            var SetIcon = AssetLoader.LoadInternal("Deities", "Icon_Set.jpg");
            var SetFeature = Helpers.CreateBlueprint<BlueprintFeature>("SetFeature", (bp => {

                bp.SetName("Set");
                bp.SetDescription("\nTitles: Lord of the Dark Desert   " +
                    "\nAlignment: Neutral Evil   " +
                    "\nAreas of Concern: Darkness, Deserts, Murder, Storms   " +
                    "\nDomains: Darkness, Death, Evil, Madness, Weather   " +
                    "\nSubdomains: Daemon, Loss, Murder, Nightmare, Storms, Undead   " +
                    "\nFavoured Weapon: Spear (Longspear)   " +
                    "\nHoly Symbol: Sha head   " +
                    "\nSacred Animal: Sha   " +
                    "\nOf all the deities of the Ancient Osirian pantheon, none is as hated and reviled as Set. He represents the foreign invader, the " +
                    "desert that encroaches upon the verdant banks of the River Sphinx, the storms that destroy crops and sink ships, and the dead that " +
                    "rise from their graves. He is evil personified, the enemy of all that is good, a god of sickness and disease, confusion and madness, " +
                    "rebellion and strife. He is a usurper, a murderer, and a stealer of souls. Set is the brother of Isis, Osiris, and Nephthys, who is " +
                    "also his consort. With Neith, Set is the father of Sobek. Set murdered Osiris, mutilated the body and scattered the pieces, and tried " +
                    "to steal his brother's throne, ushering in the Age of Darkness. Set is the enemy of both his brother Osiris and his nephew Horus. Set " +
                    "is opposed to Anubis as well, and the two war over the souls of the Osirian dead, with Anubis seeking to guide them safely to the Boneyard, " +
                    "and Set vying tosteal their souls or turn them into undead abominations. Set appears as a lean Osirian man with the head of a sha, also " +
                    "known as a Set-beast. Kinslayers, murderers, and usurpers all pay homage to Set, but he is also venerated by kings and pharaohs who value " +
                    "the god's cunning, strength, and power. Set is a patron of assassins, rogues, and necromancers, and he is worshiped by barbarians, " +
                    "warriors, and evil druids as well.");
                bp.m_Icon = SetIcon;
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
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = MantisZealotArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.TrueNeutral | AlignmentMaskType.LawfulEvil | AlignmentMaskType.NeutralEvil | AlignmentMaskType.ChaoticEvil;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChannelNegativeAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { DarknessDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { DeathDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { EvilDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { MadnessDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { WeatherDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { StormDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { UndeadDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { CrusaderSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { ClericSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { InquisitorSpellbook.ToReference<BlueprintSpellbookReference>() };
                });

                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();


                    c.m_Feature = LongSpearProficiency.ToReference<BlueprintFeatureReference>();

                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                               InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                               WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[1] { MasterworkLongspear.ToReference<BlueprintItemReference>() };
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
