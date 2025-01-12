using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.FactLogic;

namespace ExpandedContent.Tweaks.Deities {
    internal class PatchLichDeity {


        public static void AddLichDeity() {

            var WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
            var InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var MantisZealotArchetype = Resources.GetModBlueprint<BlueprintArchetype>("MantisZealotArchetype");
            var DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
            var ClawOfTheFalseWyrmArchetype = Resources.GetModBlueprint<BlueprintArchetype>("ClawOfTheFalseWyrmArchetype");
            var SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");

            var LichDeityMythicFeature = Resources.GetBlueprint<BlueprintFeature>("d633cf9ebcdc8ed4e8f2546c3e08742e");
            var LichDeityFeature = Resources.GetBlueprint<BlueprintFeature>("b4153b422d02d4f48b3f8f0ceb6a10eb");
            LichDeityFeature.SetDisallowedArchetype(WarpriestClass, MantisZealotArchetype);
            LichDeityFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            LichDeityFeature.SetDisallowedArchetype(InquistorClass, SwornOfTheEldestArchetype);
            LichDeityFeature.DisallowNewMantisZealot();
            LichDeityFeature.AddComponent<AddFacts>(c => {
                        c.m_Facts = new BlueprintUnitFactReference[] { LichDeityMythicFeature.ToReference<BlueprintUnitFactReference>() };
                    });
            LichDeityFeature.SetDescription("Leader of a small cult of undead, created by their own hands, becoming famous as the " +
                "indomitable commander of the Fifth Mendevian Crusade against the Worldwound. Determined to destroy the forces of the Abyss at any cost, " +
                "they ventured on the path of the Lord of Death. For millennia, powerful necromancers have dreamed of a power that could rival the divine. " +
                "They have created cults in their own names, and attracted many followers. Thanks to their legendary powers, they has managed to achieve much greater " +
                "success than the others: acquireing a talent to bestow divine spells upon their followers. From that moment, the undead created by them " +
                "needed no other deities to cast spells.\nDomains: Death, Evil, Strength, War. \nFavoured Weapons: Scythe, Sickle.");
            LichDeityFeature.SetAllowedDomains(
                    DeityTools.DomainAllowed.OldUndeadDomainAllowed,
                    DeityTools.DomainAllowed.ArcaneDomainAllowed,
                    DeityTools.SeparatistDomainAllowed.ArchonDomainLawAllowedSeparatist,//Lawful
                    DeityTools.SeparatistDomainAllowed.BloodDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.CavesDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.CurseDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.LightningDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.DefenseDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.DragonDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.FerocityDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.FistDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.FurDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.GrowthDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.HeroismDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.InsectDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.LoyaltyDomainAllowedSeparatist,//Lawful
                    DeityTools.SeparatistDomainAllowed.LustDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.PsychopompDomainDeathAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.PsychopompDomainReposeAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.RageDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.ResolveDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.RestorationDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.RevelationDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.RevolutionDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.RiversDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.ScalykindDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.StarsDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.StormDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.ThieveryDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.WhimsyDomainAllowedSeparatist,//Chaos
                    DeityTools.SeparatistDomainAllowed.WindDomainAllowedSeparatist
                );

        }

        public static void InnerSeaDeitiesregionToggle() {
            if (ModSettings.AddedContent.Deities.IsDisabled("Lich Deity")) { return; }
            var LichDeityFeature = Resources.GetBlueprint<BlueprintFeature>("b4153b422d02d4f48b3f8f0ceb6a10eb");
            LichDeityFeature.RemoveComponents<PrerequisiteNoFeature>();

        }
    }
        
}
    
