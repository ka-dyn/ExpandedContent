using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Items;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.DemonLords {
    internal class Treerazer {
        private static readonly BlueprintFeature ChaosDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("8c7d778bc39fec642befc1435b00f613");
        private static readonly BlueprintFeature DestructionDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("6832681c9a91bf946a1d9da28c5be4b4");
        private static readonly BlueprintFeature EvilDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("351235ac5fc2b7e47801f63d117b656c");
        private static readonly BlueprintFeature PlantDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("0e03c2a03222b0b42acf96096b286327");
        private static readonly BlueprintFeature CrusaderSpellbook = Resources.GetBlueprint<BlueprintFeature>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintFeature ClericSpellbook = Resources.GetBlueprint<BlueprintFeature>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintFeature InquisitorSpellbook = Resources.GetBlueprint<BlueprintFeature>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelNegativeAllowed = Resources.GetBlueprint<BlueprintFeature>("dab5255d809f77c4395afc2b713e9cd6");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");



        public static void AddTreerazerFeature() {

            BlueprintItem MasterworkGreataxe = Resources.GetBlueprint<BlueprintItem>("38934e7d48e501644b2bbd43a417e737");

            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");

            BlueprintFeature GreataxeProficiency = Resources.GetBlueprint<BlueprintFeature>("70ab8880eaf6c0640887ae586556a652");
            var TreerazerIcon = AssetLoader.LoadInternal("Deities", "Icon_Treerazer.jpg");
            var TreerazerFeature = Helpers.CreateBlueprint<BlueprintFeature>("TreerazerFeature", (bp => {

                bp.SetName("Treerazer");
                bp.SetDescription("\nTitles: Lord of the Blasted Tarn   " +
                    "\nAlignment: Chaotic Evil   " +
                    "\nAreas of Corruption of nature, Pollution   " +
                    "\nDomains: Chaos, Destruction, Evil, Plant   " +
                    "\nSubdomains: Catastrophe, Decay, Demon, Rage   " +
                    "\nFavoured Weapon: Greataxe   " +
                    "\nHoly Symbol: Axe in bleeding tree stump   " +
                    "\nSacred Animal: Deinonychus   " +
                    "\nTreerazer, the self-styled Lord of the Blasted Tarn, was once the favored minion and lieutenant of Cyth-V’sug, " +
                    "Demon Lord of Fungus and Parasites. After a failed attempt to wrest that crown away from Cyth-V’sug, Treerazer fled " +
                    "to the Material Plane. Cyth-V’sug was unable to pursue, but took steps to ensure that Treerazer would remain there by " +
                    "exiling him, transforming Treerazer into a native outsider and severing his bond to the Abyss. He spent many centuries " +
                    "wandering the remote corners of Golarion before finally coming upon the abandoned elven nation of Kyonin in 2497 ar. In " +
                    "the Sovyrian Stone, he found an artifact that he believed he could use to reinstate his Abyssal link and, perhaps, even " +
                    "uproot the entire nation and refocus the portal from Sovyrian to the Abyss, thereby reclaiming his position there and " +
                    "taking one more step toward revenge against Cyth-V’sug. Yet the elves sensed his tamperings and returned to confront the " +
                    "demon. A terrific battle resulted, and while the elves were able to drive Treerazer out of Iadara and into southern Kyonin, " +
                    "they were unable to slay him or force him out completely—they merely concentrated his power in a smaller region. Instead, " +
                    "the elves walled off this region, a perverted realm known today as the Tanglebriar. Treerazer lurks at the Tanglebriar’s " +
                    "heart to this day, the greatest bogeyman in elven mythology and a very real and constant threat to the nation’s security. " +
                    "Cults of Treerazer are quite rare beyond Kyonin, where secret cabals of cultists venerate him. When they do appear beyond " +
                    "these borders, they are secretive but sadistic groups, eager to sacrifice nonbelievers yet cunning in remaining undetected " +
                    "by the law of the land.");
                bp.m_Icon = TreerazerIcon;
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

                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.ChaoticNeutral | AlignmentMaskType.NeutralEvil | AlignmentMaskType.ChaoticEvil;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChannelNegativeAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChaosDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { DestructionDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { EvilDomainAllowed.ToReference<BlueprintUnitFactReference>() };
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


                    c.m_Feature = GreataxeProficiency.ToReference<BlueprintFeatureReference>();

                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                               InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                               WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[1] { MasterworkGreataxe.ToReference<BlueprintItemReference>() };
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
