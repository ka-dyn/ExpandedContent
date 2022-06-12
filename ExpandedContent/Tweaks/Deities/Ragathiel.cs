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

namespace ExpandedContent.Tweaks.Deities {
    internal class Ragathiel {
        private static readonly BlueprintFeature GoodDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("882521af8012fc749930b03dc18a69de");
        private static readonly BlueprintFeature LawDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("092714336606cfc45a37d2ab39fabfa8");
        private static readonly BlueprintFeature DestructionDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("6832681c9a91bf946a1d9da28c5be4b4");
        private static readonly BlueprintFeature NobilityDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("e0471d01e73254a4ca23278705b75e57");
        private static readonly BlueprintFeature ArchonDomainGoodAllowed = Resources.GetModBlueprint<BlueprintFeature>("ArchonDomainGoodAllowed");
        private static readonly BlueprintFeature ArchonDomainLawAllowed = Resources.GetModBlueprint<BlueprintFeature>("ArchonDomainLawAllowed");
        private static readonly BlueprintFeature CrusaderSpellbook = Resources.GetBlueprint<BlueprintFeature>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintFeature ClericSpellbook = Resources.GetBlueprint<BlueprintFeature>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintFeature InquisitorSpellbook = Resources.GetBlueprint<BlueprintFeature>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelPositiveAllowed = Resources.GetBlueprint<BlueprintFeature>("8c769102f3996684fb6e09a2c4e7e5b9");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
        private static readonly BlueprintCharacterClass PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");


        public static void AddRagathielFeature() {

            BlueprintItem ColdIronBastardSword = Resources.GetBlueprint<BlueprintItem>("200baf16628d3ab4b993094b51df5891");

            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintArchetype SilverChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SilverChampionArchetype");

            BlueprintFeature BastardSwordProficiency = Resources.GetBlueprint<BlueprintFeature>("57299a78b2256604dadf1ab9a42e2873");
            var RagathielIcon = AssetLoader.LoadInternal("Deities", "Icon_Ragathiel.jpg");
            var RagathielFeature = Helpers.CreateBlueprint<BlueprintFeature>("RagathielFeature", (bp => {

                bp.SetName("Ragathiel");
                bp.SetDescription("\nTitles: General of Vengeance   " +
                    "\nAlignment: Lawful Good   " +
                    "\nDomains: Destruction, Good, Law, Nobility   " +
                    "\nSubdomains: Archon, Leadership, Martyr, Rage   " +
                    "\nFavoured Weapon: Bastard Sword   " +
                    "\nHoly Symbol: Sword, crossed with Wing   " +
                    "\nSacred Animal: Mastiff   " +
                    "\nSacred Colours: Crimson, Gold   " +
                    "\nRagathiel is an empyreal lord—a good servant of the gods who through transcendence has achieved " +
                    "some small measure of divine power—known as the General of Vengeance. His portfolio includes chivalry, duty, " +
                    "and vengeance, and his holy symbol is a bastard sword crossed with a crimson wing. He makes his home at the base " +
                    "of the mountain of Heaven, in his eponymous fortress, a magnificent steel structure designed to withstand a thousand-year siege " +
                    "if need be. Ragathiel takes an active role in the battle against Hell's fiendish legions. He shines at the head of his army, " +
                    "a figure of golden light cleaving through the ranks of devils that face him. Soldiers of all kinds, but particularly knights, " +
                    "pay homage to Ragathiel and pray for his virtue and wrathful strength in battle. Those who have taken vows, especially those " +
                    "of duty or vengeance, hold Ragathiel as their ideal, and his agents sometimes assist those who have been grievously wronged " +
                    "and now seek righteous vengeance. As strange as it may seem, Ragathiel is the son of Feronia, a demigoddess of the Plane of Fire, " +
                    "and Dispater, the Father of Dis. He was conceived in a brief tryst (lasting only a few centuries), after which his mother took " +
                    "him out of Hell. Ragathiel's tainted heritage has left him with a wrathful heart, and the angel struggles constantly to master " +
                    "his baser impulses in service to the light. For thousands of years he strove to prove himself to the angelic choirs of Heaven. In the " +
                    "Maelstrom he wrestled for 16 years with a monstrous evil serpent whose scales wept acidic blood until he was able to choke the " +
                    "life out of it. He led an entire army against one of the iron fortresses of Avernus and burned the castle to the ground with holy fire, " +
                    "single-handedly maiming Infernal Duke Deumus in the process. Finally the other empyreal lords agreed to admit Ragathiel into their ranks, and now " +
                    "they appear to trust the angel completely—though that trust took centuries to develop. At some point in history, Ragathiel destroyed " +
                    "Typhon, the original ruler of the first layer of Hell.He may have been manipulated into doing so by the schemes of Asmodeus to punish " +
                    "Typhon for his indiscretions.[9] Ragathiel was once served by the Hand of the Inheritor, an angel who now acts as the goddess Iomedae's herald. " +
                    "Then known as the Hand of Vengeance, this angel asked to be reassigned to Iomedae after the death of Aroden. " +
                    "\nAppearance: Ragathiel is a tower of glorious might. He stands almost 20 feet tall, and five lofty burning wings stretch from his back. " +
                    "The General of Vengeance once had six wings, but one was severed, torn from his body by the archdevil Dispater—Ragathiel's father.   " +
                    "\nWorship: Ragathiel is commonly worshiped by the avowed, knights, soldiers, the wronged, and the empyreal lord employs aasimar, paladins, " +
                    "blink dogs, and kirin as his most common servants.[4] The holiest time for Ragathiel's followers is the brief moments before and after a battle. " +
                    "The faithful ask Ragathiel's eyes to fall upon them and for his wings to shield them.This is also a standard greeting and farewell among worshipers. " +
                    "Followers of Ragathiel commonly wear red and silver clothing.Many also don an ornament or sigil the color of flames, typically on the helmet. " +
                    "Ragathiel has a small but old cult in Magnimar, a city known for its many mystery cults revering various empyreal lords.   " +
                    "\nPaladin Code:   " +
                    "\n*I will avenge evil wrought upon the innocent.   " +
                    "\n*I will not give my word lightly, but once it is given, I will uphold a promise until my last breath.   " +
                    "\n*Those proven guilty must be punished for their crimes. I will not turn a blind eye to wrongdoing.   " +
                    "\n*Rage is a virtue and a strength only when focused against the deserving. I will never seek disproportionate retribution.   " +
                    "\n*Redemption finds hearts from even the cruelest origins. I will strive not to act upon prejudice against fellow mortals based on race or origin.");
                bp.m_Icon = RagathielIcon;
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
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SilverChampionArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.LawfulGood | AlignmentMaskType.NeutralGood | AlignmentMaskType.LawfulNeutral | AlignmentMaskType.TrueNeutral;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChannelPositiveAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { GoodDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { LawDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { DestructionDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { NobilityDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ArchonDomainGoodAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ArchonDomainLawAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { CrusaderSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { ClericSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { InquisitorSpellbook.ToReference<BlueprintSpellbookReference>() };
                });

                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();


                    c.m_Feature = BastardSwordProficiency.ToReference<BlueprintFeatureReference>();
                    
                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                               InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                               WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[1] { ColdIronBastardSword.ToReference<BlueprintItemReference>() };
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
