using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.FactLogic;

namespace ExpandedContent.Tweaks.DemonLords {
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
            var InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");

            AreshkegalFeature.m_Icon = AreshkagalIcon;
            AreshkegalFeature.RemoveComponents<PrerequisiteNoFeature>();
            AreshkegalFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = ClawOfTheFalseWyrmArchetype.ToReference<BlueprintArchetypeReference>();
            });
            AreshkegalFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = InquistorClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
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

