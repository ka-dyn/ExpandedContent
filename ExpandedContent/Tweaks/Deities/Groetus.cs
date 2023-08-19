using ExpandedContent.Extensions;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.UnitLogic.FactLogic;

namespace ExpandedContent.Tweaks.Deities {
    internal class Groetus {

        private static readonly BlueprintFeature StarsDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("StarsDomainAllowed");

        public static void AddGroetus() {
            var GroetusFeature = Resources.GetBlueprint<BlueprintFeature>("c3e4d5681906d5246ab8b0637b98cbfe");
            GroetusFeature.RemoveComponents<PrerequisiteFeature>();
            var DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
            var ClawOfTheFalseWyrmArchetype = Resources.GetModBlueprint<BlueprintArchetype>("ClawOfTheFalseWyrmArchetype");
            GroetusFeature.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = ClawOfTheFalseWyrmArchetype.ToReference<BlueprintArchetypeReference>();
            });
            GroetusFeature.AddComponent<AddFacts>(bp => {
                bp.m_Facts = new BlueprintUnitFactReference[1] { StarsDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
        }
    }
}
