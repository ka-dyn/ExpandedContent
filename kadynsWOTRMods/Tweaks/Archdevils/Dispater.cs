using kadynsWOTRMods.Config;
using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Items;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kadynsWOTRMods.Tweaks.Archdevils {
    internal class Dispater {

        private static readonly BlueprintFeature TrickeryDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("eaa368e08628a8641b16cd41cbd2cb33");
        private static readonly BlueprintFeature EvilDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("351235ac5fc2b7e47801f63d117b656c");
        private static readonly BlueprintFeature NobilityDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("e0471d01e73254a4ca23278705b75e57");
        private static readonly BlueprintFeature LawDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("092714336606cfc45a37d2ab39fabfa8");
        private static readonly BlueprintFeature CrusaderSpellbook = Resources.GetBlueprint<BlueprintFeature>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintFeature ClericSpellbook = Resources.GetBlueprint<BlueprintFeature>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintFeature InquisitorSpellbook = Resources.GetBlueprint<BlueprintFeature>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelNegativeAllowed = Resources.GetBlueprint<BlueprintFeature>("dab5255d809f77c4395afc2b713e9cd6");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");


        public static void AddDispater() {

            if (ModSettings.AddedContent.Archdevils.IsDisabled("Dispater")) { return; }
            
            BlueprintFeature HeavyMaceProficiency = Resources.GetBlueprint<BlueprintFeature>("3f18330d717ea0148b496ee8cc291a60");
            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintItem ColdIronHeavyMace = Resources.GetBlueprint<BlueprintItem>("4499f1b1dfc7a3d4ab1b95a13580e2e4");

            var DispaterIcon = AssetLoader.LoadInternal("Deities", "Icon_Dispater.jpg");
            var DispaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("DispaterFeature", (bp => {

                bp.SetName("Dispater");
                bp.SetDescription("\nTitles: Iron Lord, Father of Dis, First King, King of Iron, Asmodeus' Eye, Master of the Tower, Lord of the Second   " +
                    "\nRealm: Dis, Hell   " +
                    "\nAlignment: Lawful Evil   " +
                    "\nAreas of Concern: Cities, Prisons, Rulership   " +
                    "\nDomains: Evil, Law, Nobility, Trickery   " +
                    "\nSubdomains: Deception, Devil, Leadership, Legislation, Thievery   " +
                    "\nProfane Symbol: Iron Nail, Crown and Ring   " +
                    "\nFavoured Weapon: Heavy Mace   " +
                    "\nProfane Animal: Hound   " +
                    "\nProfane Colours: Iron gray, Red" +
                    "\nThe archdevil Dispater is Hell's greatest jailer and politician and " +
                    "rules its second layer, Dis. Also known as the Iron Lord, Father of Dis and First King, Dispater is one " +
                    "of the more active rulers of Hell and one of Asmodeus's closest and longest-standing allies. His unholy symbol " +
                    "is an iron spike driven into a golden ring with a red and purple crown. His symbol of office is an artefact " +
                    "known as The Eclipsing Eye. Alone among the lords of Hell, Dispater believes in the virtues of courtly love, " +
                    "and has taken three wives over the course of his eons-long existence. The first is forgotten, even by the Iron Lord " +
                    "himself, the only token of her existence a shattered statue in Dispater's throne room. The second was Feronia, " +
                    "an elemental demigoddess with whom Dispater shared a brief, tempestuous relationship that led to the birth of his son, " +
                    "the empyreal lord Ragathiel. Dispater's third and current wife is Erecura, a once-mortal demigoddess who was condemned " +
                    "to Hell for stealing the secret of eternal life from Pharasma. Dispater has allied with the archdevil Mephistopheles as " +
                    "they both enjoy the art of ruling, and is married to Erecura.   " +
                    "\nAppearance: Dispater appears as a majestic, seven-foot-tall figure with rust-red skin, four horns, and a burning crown " +
                    "that hovers above his head. His skin bears dozens of scars and piercings, each of them a past wound turned into a mark of " +
                    "pride. He retains his angelic wings, though they are now black, and he keeps them retracted within his body at almost all times. " +
                    "The First King's staff of office is The Eclipsing Eye, a bladed staff with a great ruby through which Asmodeus constantly " +
                    "watches him.");
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

                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.LawfulEvil | AlignmentMaskType.NeutralEvil | AlignmentMaskType.LawfulNeutral | AlignmentMaskType.TrueNeutral;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChannelNegativeAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { EvilDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { LawDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { TrickeryDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { NobilityDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { CrusaderSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { ClericSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { InquisitorSpellbook.ToReference<BlueprintSpellbookReference>() };
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
























































        

