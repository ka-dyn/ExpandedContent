using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Blueprints;

namespace ExpandedContent.Tweaks.DemonLords {
    internal class Deskari {


        
                
        public static void AddDeskari() {



            var DeskariIcon = AssetLoader.LoadInternal("Deities", "Icon_Deskari.jpg");
            var DeskariFeature = Resources.GetBlueprint<BlueprintFeature>("ddf913858bdf43b4da3b731e082fbcc0");
            var BloodDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("BloodDomainAllowed");
            var DemonDomainChaosAllowed = Resources.GetModBlueprint<BlueprintFeature>("DemonDomainChaosAllowed");
            var DemonDomainEvilAllowed = Resources.GetModBlueprint<BlueprintFeature>("DemonDomainEvilAllowed");
            var DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var ClawOfTheFalseWyrmArchetype = Resources.GetModBlueprint<BlueprintArchetype>("ClawOfTheFalseWyrmArchetype");
            var SilverChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SilverChampionArchetype");
            var InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");

            DeskariFeature.m_Icon = DeskariIcon;
                    DeskariFeature.RemoveComponents<PrerequisiteNoFeature>();
            DeskariFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = ClawOfTheFalseWyrmArchetype.ToReference<BlueprintArchetypeReference>();
            });
            DeskariFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SilverChampionArchetype.ToReference<BlueprintArchetypeReference>();
            });
            DeskariFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = InquistorClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
            });
            DeskariFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { BloodDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            DeskariFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { DemonDomainChaosAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            DeskariFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { DemonDomainEvilAllowed.ToReference<BlueprintUnitFactReference>() };
            });

        }
               
            
    }
       
}
    


