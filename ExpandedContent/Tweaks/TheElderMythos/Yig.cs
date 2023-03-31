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

namespace ExpandedContent.Tweaks.TheElderMythos {
    internal class Yig {
        private static readonly BlueprintFeature ChaosDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("8c7d778bc39fec642befc1435b00f613");
        private static readonly BlueprintFeature CommunityDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("c87004460f3328c408d22c5ead05291f");
        private static readonly BlueprintFeature ProtectionDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("d4ce7592bd12d63439907ad64e986e59");
        private static readonly BlueprintFeature ScalykindDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("ScalykindDomainFeature");
        private static readonly BlueprintFeature DefenseDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("DefenseDomainAllowed");
        private static readonly BlueprintFeature DragonDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("DragonDomainAllowed");
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelPositiveAllowed = Resources.GetBlueprint<BlueprintFeature>("8c769102f3996684fb6e09a2c4e7e5b9");
        private static readonly BlueprintFeature ChannelNegativeAllowed = Resources.GetBlueprint<BlueprintFeature>("dab5255d809f77c4395afc2b713e9cd6");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
        private static readonly BlueprintCharacterClass DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");



        public static void AddYigFeature() {

            BlueprintItem MasterworkPunchingDagger = Resources.GetBlueprint<BlueprintItem>("4eadc3edae4500a4c972b61cad66add8");

            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype MantisZealotArchetype = Resources.GetModBlueprint<BlueprintArchetype>("MantisZealotArchetype");
            BlueprintArchetype ClawOfTheFalseWyrmArchetype = Resources.GetModBlueprint<BlueprintArchetype>("ClawOfTheFalseWyrmArchetype");


            BlueprintFeature PunchingDaggerProficiency = Resources.GetBlueprint<BlueprintFeature>("a13839f75ac30cc48945c96ef1c98e1c");
            var YigIcon = AssetLoader.LoadInternal("Deities", "Icon_Yig.jpg");
            var YigFeature = Helpers.CreateBlueprint<BlueprintFeature>("YigFeature", (bp => {

                bp.SetName("Yig");
                bp.SetDescription("\nTitles: Father of Serpents   " +
                    "\nAlignment: Chaotic Neutral   " +
                    "\nAreas of Concern: Cycles, Procreation, Serpents   " +
                    "\nRank: Great Old One   " +
                    "\nDomains: Chaos, Community, Protection, Scalykind   " +
                    "\nSubdomains: Defense, Dragon, Revelry, Venom   " +
                    "\nFavoured Weapon: Punching dagger   " +
                    "\nHoly Symbol: Coiled serpent with a crescent mark on its head   " +
                    "\nOf all the Great Old Ones—indeed, of all the gods of the Elder Mythos—the Father of Serpents is without a doubt the most benign. Yet those who would be so foolish as " +
                    "to expect kindness from Yig would do well to think again, for Yig does not suffer fools and is as likely to devour those who beseech him for aid as he is to provide aid. " +
                    "Even Yig’s most devout worshipers realize the god may simply wish to feed at times, and on these occasions, no amount of devotion can protect the supplicant from death. " +
                    "Yig is worshiped primarily by nomadic societies and those who live with a close connection to the land; veneration of him is all but unknown in urban regions. He protects " +
                    "communities that honor him in the right ways, particularly if they follow the complex rituals required of his worship through the seasons. His favor can result in bountiful " +
                    "harvests, good hunts, and strong and healthy babies—at times immaculate conceptions are attributed to Yig, particularly if the resulting children bear crescent-shaped " +
                    "birthmarks upon their brows. Conversely, when a society has displeased or failed Yig, its children are born with crippling or monstrous deformities that leave mothers " +
                    "dead and fathers insane. Yig speaks through the actions of serpents, yet he is rarely worshiped by serpentfolk. Indeed, the Father of Serpents has long been at war with " +
                    "the god Ydersius, although since the decapitation of Ydersius, Yig has turned his attentions elsewhere. Yig appears as a scaled, humanoid creature with a serpent’s head " +
                    "and lashing tail, or as an immense rattlesnake with a crescent-shaped mark upon the brow.");
                bp.m_Icon = YigIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.HideInUI = true;
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
                    c.m_Facts = new BlueprintUnitFactReference[1] { CommunityDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ProtectionDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ScalykindDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { DragonDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { DefenseDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { CrusaderSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { ClericSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { InquisitorSpellbook.ToReference<BlueprintSpellbookReference>() };
                });

                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();


                    c.m_Feature = PunchingDaggerProficiency.ToReference<BlueprintFeatureReference>();

                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                               InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                               WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[1] { MasterworkPunchingDagger.ToReference<BlueprintItemReference>() };
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
