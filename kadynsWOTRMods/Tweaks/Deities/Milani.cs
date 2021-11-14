using HarmonyLib;
using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Deities {
    static class Milani {
        private static readonly BlueprintFeature GoodDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("882521af8012fc749930b03dc18a69de");
        private static readonly BlueprintFeature ChaosDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("8c7d778bc39fec642befc1435b00f613");
        private static readonly BlueprintFeature HealingDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("73ae164c388990c43ade94cfe8ed5755");
        private static readonly BlueprintFeature LiberationDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("801ca88338451a546bca2ee59da87c53");
        private static readonly BlueprintFeature ProtectionDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("d4ce7592bd12d63439907ad64e986e59");
        private static readonly BlueprintFeature CrusaderSpellbook = Resources.GetBlueprint<BlueprintFeature>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintFeature ClericSpellbook = Resources.GetBlueprint<BlueprintFeature>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintFeature InquisitorSpellbook = Resources.GetBlueprint<BlueprintFeature>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelPositiveAllowed = Resources.GetBlueprint<BlueprintFeature>("8c769102f3996684fb6e09a2c4e7e5b9");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");




        public static void AddMilaniFeature() {





            if (ModSettings.AddedContent.Deities.IsDisabled("Milani")) { return; }
            var MilaniSacredWeaponFeature = Resources.GetModBlueprint<BlueprintFeature>("MilaniSacredWeaponFeature");
            var DeitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("59e7a76987fe3b547b9cce045f4db3e4");
            BlueprintFeature LightMaceProficiency = Resources.GetBlueprint<BlueprintFeature>("d0a788c77b0eae948944fa424125c120");
            BlueprintFeature HeavyMaceProficiency = Resources.GetBlueprint<BlueprintFeature>("3f18330d717ea0148b496ee8cc291a60");
            BlueprintItem ColdIronHeavyMace = Resources.GetBlueprint<BlueprintItem>("4499f1b1dfc7a3d4ab1b95a13580e2e4");
            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            var MilaniIcon = AssetLoader.LoadInternal("Deities", "Icon_Milani.jpg");
            var MilaniFeature = Helpers.CreateBlueprint<BlueprintFeature>("MilaniFeature", (bp => {
                
                bp.SetName("Milani");
                bp.SetDescription("\n<b>Titles</b>: The Everbloom   " +
                "\nRealm(s): Refuge of the Red Rose, Axis/ Milani's Garden, Elysium   " +
                "\nAlignment: Chaotic Good   " +
                "\nAreas of Concern: Hope, Devotion, Uprisings   " +
                "\nDomains: Chaos, Good, Healing, Liberation, Protection   " +
                "\nSubdomains: Azata, Defense, Freedom, Purity, Restoration, Revolution   " +
                "\nFavoured Weapon: Morningstar (Mace)   " +
                "\nHoly Symbol: Rose on Bloody Street   " +
                "\nSacred Animal: Mouse   " +
                "\nSacred Colours: Red, White   " +
                "\nThe minor goddess Milani, also known as the Everbloom, is the patron of all those who fight " +
                 "against oppression and unjust rule. Her symbol is a rose growing out of a blood-soaked street. Until the death of Aroden in " +
                 "4606 AR, the goddess Milani was simply one of dozens of saints within the Last Azlanti's faith. " +
                "She was the beacon of hope to all those who fought against repressive regimes, giving courage to those who had little but their desire to " +
                "live a free life. The death of her patron, combined with the tremendous upheaval and suffering that followed his death, gave her a focus and " +
                "attracted many new followers. Those devoted to her found the courage to organize the rebellions against the infernal takeover of the " +
                "Chelish Empire, helping many of her outlying territories break free of its control. They fought against the slow slide into barbarism, " +
                "restoring people's hope that a just and good society could be restored. Milani has never been as popular as Aroden's other followers," +
                "such as Iomedae, perhaps because the Inheritor's worship had already been firmly established before their patron's " +
                "passing. Milani has two realms: Milani's Garden in Elysium, where she spends most of her time, and the Refuge of the Red Rose in Axis, " +
                "a small corner located within the collapsing divine realm of Aroden. Only a few square miles in size, the Refuge of the Red Rose " +
                "sits near the center of her former patron's realm. From here she gives aid to all those fighting oppression and evil, strives to mitigate " +
                "the institutionalised inequality in Axis, and seeks to relocate the downtrodden to " +
                "friendlier places.");
                
                bp.m_Icon = MilaniIcon;
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
                    c.Alignment = AlignmentMaskType.Good | AlignmentMaskType.ChaoticNeutral;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChannelPositiveAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { GoodDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ChaosDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { HealingDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { LiberationDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { ProtectionDomainAllowed.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { CrusaderSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { ClericSpellbook.ToReference<BlueprintSpellbookReference>() };
                    c.m_Spellbooks = new BlueprintSpellbookReference[1] { InquisitorSpellbook.ToReference<BlueprintSpellbookReference>() };
                });

                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();


                    c.m_Feature = LightMaceProficiency.ToReference<BlueprintFeatureReference>();
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
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { MilaniSacredWeaponFeature.ToReference<BlueprintUnitFactReference>() };
                });
            }));
            
            













        }

        }

    }

    

    
