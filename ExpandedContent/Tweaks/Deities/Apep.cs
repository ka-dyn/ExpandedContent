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
    internal class Apep {
        private static readonly BlueprintFeature ChaosDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("8c7d778bc39fec642befc1435b00f613");
        private static readonly BlueprintFeature DarknessDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("6d8e7accdd882e949a63021af5cde4b8");
        private static readonly BlueprintFeature DestructionDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("6832681c9a91bf946a1d9da28c5be4b4");
        private static readonly BlueprintFeature EvilDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("351235ac5fc2b7e47801f63d117b656c");
        private static readonly BlueprintFeature ScalykindDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("ScalykindDomainAllowed");
        private static readonly BlueprintFeature DragonDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("DragonDomainAllowed");
        private static readonly BlueprintFeature DemonDomainChaosAllowed = Resources.GetModBlueprint<BlueprintFeature>("DemonDomainChaosAllowed");
        private static readonly BlueprintFeature DemonDomainEvilAllowed = Resources.GetModBlueprint<BlueprintFeature>("DemonDomainEvilAllowed");
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelNegativeAllowed = Resources.GetBlueprint<BlueprintFeature>("dab5255d809f77c4395afc2b713e9cd6");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");



        public static void AddApepFeature() {

            BlueprintItem MasterworkDagger = Resources.GetBlueprint<BlueprintItem>("dfc92affae244554e8745a9ee9b7c520");

            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");

            BlueprintFeature DaggerProficiency = Resources.GetBlueprint<BlueprintFeature>("b776c19291928cf4184d4dc65f09f3a6");
            var ApepIcon = AssetLoader.LoadInternal("Deities", "Icon_Apep.jpg");
            var ApepFeature = Helpers.CreateBlueprint<BlueprintFeature>("ApepFeature", (bp => {

                bp.SetName("Apep");
                bp.SetDescription("\nTitles: Devourer of the Dawn   " +
                    "\nAlignment: Chaotic Evil   " +
                    "\nAreas of Concern: Chaos, Darkness, Destruction, Snakes   " +
                    "\nDomains: Chaos, Darkness, Destruction, Evil, Scalykind   " +
                    "\nSubdomains: Catastrophe, Demon, Dragon, Entropy, Fear, Night   " +
                    "\nFavoured Weapon: Dagger   " +
                    "\nHoly Symbol: Coiled Serpent   " +
                    "\nSacred Animal: Snake   " +
                    "\nBorn in the waters of primeval chaos before creation, the malevolent serpent Apep stands in sharp contrast to the " +
                    "more benevolent serpent goddess Wadjet. Apep is the personification of chaos and darkness , the hidden dangers that lurk " +
                    "just beneath the surface, the deadly whorls and eddies that drown and swallow, and the hungry river snakes that prey on those " +
                    "who venture too near the water's edge. Apep is the raging rapids, the boundless flood that inundates and sweeps away all that " +
                    "mortals seek to build. He is the darkness in the water's depths that swallows light and life, devouring the sun and plunging " +
                    "the world into eternal night. Said to dwell in the Tenth Region of Night, Apep is the great enemy of the sun god Ra, and ancient " +
                    "Osirians believed each sunrise was but a temporary victory, for the Devourer of the Dawn always waits to seize and consume the " +
                    "sun again and again, until one day the sun shall rise no more and all shall be ended. Apep is a great golden serpent miles long, " +
                    "and his crushing coils encircle the world. He also appears as a fearsome dragon with night-black scales, and evil dragons of " +
                    "Osirion venerate him in this form. Apep's human cults are secretive , usually meeting at night or under the cloak of darkness. " +
                    "These evil sects keep themselves hidden from the public eye lest they attract the wrath of those who follow the more benevolent " +
                    "and helpful gods.");
                bp.m_Icon = ApepIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = PriestOfBalance.ToReference<BlueprintArchetypeReference>();
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
                    c.m_Facts = new BlueprintUnitFactReference[1] { DarknessDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { DestructionDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { EvilDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ScalykindDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { DragonDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { DemonDomainChaosAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { DemonDomainEvilAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { CrusaderSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { ClericSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { InquisitorSpellbook.ToReference<BlueprintSpellbookReference>() };
                });

                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();


                    c.m_Feature = DaggerProficiency.ToReference<BlueprintFeatureReference>();

                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                               InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                               WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[1] { MasterworkDagger.ToReference<BlueprintItemReference>() };
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
