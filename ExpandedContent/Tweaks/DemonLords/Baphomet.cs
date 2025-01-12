using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Blueprints;

namespace ExpandedContent.Tweaks.DemonLords {
    internal class Baphomet
    {




        public static void AddBaphomet()
        {

            var BaphometIcon = AssetLoader.LoadInternal("Deities", "Icon_Baphomet.jpg");
            var BaphometFeature = Resources.GetBlueprint<BlueprintFeature>("bd72ca8ffcfec5745899ac56c93f12c5");
            var DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
            var ClawOfTheFalseWyrmArchetype = Resources.GetModBlueprint<BlueprintArchetype>("ClawOfTheFalseWyrmArchetype");
            var InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");
            var MythicIgnoreAlignmentRestrictions = Resources.GetBlueprint<BlueprintFeature>("24e78475f0a243e1a810452d14d0a1bd");

            BaphometFeature.m_Icon = BaphometIcon;
            BaphometFeature.RemoveComponents<PrerequisiteNoFeature>();
            BaphometFeature.SetDisallowedArchetype(DreadKnightClass, ClawOfTheFalseWyrmArchetype);
            BaphometFeature.SetDisallowedArchetype(InquistorClass, SwornOfTheEldestArchetype);
            BaphometFeature.DisallowAngelfireApostle();
            BaphometFeature.DisallowDarkSister();
            BaphometFeature.DisallowNewMantisZealot();
            BaphometFeature.MagicDeceiverLock();

            BaphometFeature.SetAllowedDomains(
                    DeityTools.DomainAllowed.DemonDomainEvilAllowed,
                    DeityTools.DomainAllowed.DemonDomainChaosAllowed,
                    DeityTools.DomainAllowed.FerocityDomainAllowed,
                    DeityTools.DomainAllowed.ResolveDomainAllowed,
                    DeityTools.DomainAllowed.FurDomainAllowed,
                    DeityTools.SeparatistDomainAllowed.BloodDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.CavesDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.ArcaneDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.LightningDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.CurseDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.DefenseDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.DragonDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.FerocityDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.FistDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.FurDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.GrowthDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.HeroismDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.LustDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.PsychopompDomainDeathAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.PsychopompDomainReposeAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.RageDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.ResolveDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.RestorationDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.RevelationDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.RevolutionDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.InsectDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.RiversDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.ScalykindDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.StarsDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.StormDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.ThieveryDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.WhimsyDomainAllowedSeparatist,//Chaos
                    DeityTools.SeparatistDomainAllowed.WindDomainAllowedSeparatist
                );

        }
    }

}


