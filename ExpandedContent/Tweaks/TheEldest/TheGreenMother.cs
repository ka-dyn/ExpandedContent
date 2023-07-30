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

namespace ExpandedContent.Tweaks.TheEldest {
    internal class TheGreenMother {
        private static readonly BlueprintFeature CharmDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("f1ceba79ee123cc479cece27bc994ff2");
        private static readonly BlueprintFeature EarthDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("5ca99a6ae118feb449dbbd165a8fe7c4");
        private static readonly BlueprintFeature EvilDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("351235ac5fc2b7e47801f63d117b656c");
        private static readonly BlueprintFeature PlantDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("0e03c2a03222b0b42acf96096b286327");
        private static readonly BlueprintFeature CavesDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("CavesDomainAllowed");
        private static readonly BlueprintFeature LustDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("LustDomainAllowed");
        private static readonly BlueprintFeature GrowthDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("GrowthDomainAllowed");
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelNegativeAllowed = Resources.GetBlueprint<BlueprintFeature>("dab5255d809f77c4395afc2b713e9cd6");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
        private static readonly BlueprintCharacterClass DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");



        public static void AddTheGreenMotherFeature() {

            BlueprintItem MasterworkSickle = Resources.GetBlueprint<BlueprintItem>("eae563aae0febbc4e91a1c5d0a6fd82a");

            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintArchetype MantisZealotArchetype = Resources.GetModBlueprint<BlueprintArchetype>("MantisZealotArchetype");
            BlueprintArchetype ClawOfTheFalseWyrmArchetype = Resources.GetModBlueprint<BlueprintArchetype>("ClawOfTheFalseWyrmArchetype");

            BlueprintFeature SickleProficiency = Resources.GetBlueprint<BlueprintFeature>("7b4c0488057fa2e42b8a92bac3304eb9");
            var TheGreenMotherIcon = AssetLoader.LoadInternal("Deities", "Icon_TheGreenMother.jpg");
            var TheGreenMotherFeature = Helpers.CreateBlueprint<BlueprintFeature>("TheGreenMotherFeature", (bp => {

                bp.SetName("The Green Mother");
                bp.SetDescription("\nTitles: The Feasting Flower, The Hungry Blossom   " +
                    "\nAlignment: Neutral Evil   " +
                    "\nAreas of Concern: Carnivorous plants, Intrigue, Seduction   " +
                    "\nDomains: Charm, Earth, Evil, Plant   " +
                    "\nSubdomains: Caves, Decay, Growth, Lust   " +
                    "\nFavoured Weapon: Sickle   " +
                    "\nHoly Symbol: Sensual, female, briar-wrapped lips   " +
                    "\nSacred Animal: None (carnivorous plants)   " +
                    "\nFrom the elegant flower that draws in the bee to the bioluminescent lures that call deep-sea fish to the slaughter, seduction is an integral " +
                    "part of the natural world, bringing both pleasure and pain. No creature embodies this idea better than the Green Mother. The Green Mother resembles " +
                    "a beautiful humanoid woman with bark-like skin and green hair, her features a constantly shifting mix of elf, dryad, and nymph. As befits the " +
                    "Eldest most associated with simplistic human ideas of nature, her body regularly changes to incorporate natural features: hands becoming gnarled " +
                    "roots, hair becoming a literal waterfall, or skin suddenly becoming a mirror-smooth pond capable of sucking in and drowning an unfortunate lover. " +
                    "Travelers from beyond the First World are often quick to cast the Green Mother in one of two lights: either as the villainous temptress of morality " +
                    "plays or as the equally deadly seductress of bawdy tales (contributing to such thinly veiled sobriquets as \"The Feasting Flower\" and \"The Hungry Blossom\"). " +
                    "Yet the Green Mother is not merely a creature of lust. Rather, she is the seduction of nature incarnate, the caution to the unwary. She is the " +
                    "rolling landscape that calls to the wanderer, the primal need that leads salmon to spawn and die, the mantis gnawing on the corpse of her mate. She " +
                    "is both the flower’s bloom and the inevitable death that follows. To attempt to define her via mortal concepts of morality is to miss the point entirely. " +
                    "She is a master puppeteer, pulling strings that affect events even far beyond the First World’s borders. Though her sense of humor is dark, and many see " +
                    "evil in her tricks and traps, the Green Mother sees herself as merely impartial, helping to guide natural selection by uncovering and exploiting weaknesses. " +
                    "If the practice amuses her as well, surely she deserves such a small reward.");
                bp.m_Icon = TheGreenMotherIcon;
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
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.TrueNeutral | AlignmentMaskType.LawfulEvil | AlignmentMaskType.NeutralEvil | AlignmentMaskType.ChaoticEvil;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChannelNegativeAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { CharmDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { EarthDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { EvilDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { PlantDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { CavesDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { LustDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { GrowthDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { CrusaderSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { ClericSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { InquisitorSpellbook.ToReference<BlueprintSpellbookReference>() };
                });

                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();


                    c.m_Feature = SickleProficiency.ToReference<BlueprintFeatureReference>();

                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                               InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                               WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[1] { MasterworkSickle.ToReference<BlueprintItemReference>() };
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
