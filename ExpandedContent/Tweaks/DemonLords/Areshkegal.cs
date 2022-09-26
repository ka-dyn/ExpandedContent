using HarmonyLib;
using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.FactLogic;

namespace ExpandedContent.Tweaks.DemonLords
{
    internal class Areshkegal
    {



        public static void AddAreshkegal()
        {

            var AreshkagalIcon = AssetLoader.LoadInternal("Deities", "Icon_Areshkagal.jpg");
            var AreshkegalFeature = Resources.GetBlueprint<BlueprintFeature>("d714ecb5d5bb89a42957de0304e459c9");
            var DemonDomainChaosAllowed = Resources.GetModBlueprint<BlueprintFeature>("DemonDomainChaosAllowed");
            var DemonDomainEvilAllowed = Resources.GetModBlueprint<BlueprintFeature>("DemonDomainEvilAllowed");
            var WindDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("WindDomainAllowed");
            var ThieveryDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("ThieveryDomainAllowed");
            var DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
            var ClawOfTheFalseWyrmArchetype = Resources.GetModBlueprint<BlueprintArchetype>("ClawOfTheFalseWyrmArchetype");

            AreshkegalFeature.m_Icon = AreshkagalIcon;
            AreshkegalFeature.RemoveComponents<PrerequisiteNoFeature>();
            AreshkegalFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = ClawOfTheFalseWyrmArchetype.ToReference<BlueprintArchetypeReference>();
            });
            AreshkegalFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { DemonDomainChaosAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            AreshkegalFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { DemonDomainEvilAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            AreshkegalFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { WindDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            AreshkegalFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { ThieveryDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
        }


    }

}

