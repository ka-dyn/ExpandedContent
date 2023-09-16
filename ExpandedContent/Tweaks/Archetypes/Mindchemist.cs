using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.Blueprints.Classes.Selection;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class Mindchemist {
        public static void AddMindchemist() {

            var AlchemistClass = Resources.GetBlueprint<BlueprintCharacterClass>("0937bec61c0dabc468428f496580c721");
            var CognatogenFeature = Resources.GetBlueprint<BlueprintFeature>("e3f460ea61fcc504183c7d6818bbbf7a");
            var MutagenFeature = Resources.GetBlueprint<BlueprintFeature>("cee8f65448ce71c4b8b8ca13751dd8ea");
            var PoisonResistanceFeature = Resources.GetBlueprint<BlueprintFeature>("c9022272c87bd66429176ce5c597989c");
            var PoisonImmunityFeature = Resources.GetBlueprint<BlueprintFeature>("202af59b918143a4ab7c33d72c8eb6d5");
            var SenseiWisdom = Resources.GetBlueprint<BlueprintFeature>("4356b5d6d34489747bba68d43924a857");
            var SkillFocusSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("c9629ef9eebb88b479b2fbc5e836656a");

            var MindchemistArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("MindchemistArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"MindchemistArchetype.Name", "Mindchemist");
                bp.LocalizedDescription = Helpers.CreateString($"MindchemistArchetype.Description", "While most alchemists use mutagens to boost their physical ability at the cost of mental " +
                    "ability, some use alchemy for the opposite purpose—to boost the power of the mind and memory. A mindchemist can reach incredible levels of mental acuity, but suffers " +
                    "lingering debilitating effects to his physique.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"MindchemistArchetype.Description", "While most alchemists use mutagens to boost their physical ability at the cost of " +
                    "mental ability, some use alchemy for the opposite purpose—to boost the power of the mind and memory. A mindchemist can reach incredible levels of mental acuity, but " +
                    "suffers lingering debilitating effects to his physique.");                
            });
            var MindchemistBonusFeats = Helpers.CreateBlueprint<BlueprintFeature>("MindchemistBonusFeats", bp => {
                bp.SetName("Skilled Discovery");
                bp.SetDescription("A mindchemist may select Skill Focus (Any Knowledge skill, Lore Religion, Persuasion, Trickery, Stealth, or Use Magic Device) in place of a discovery.");
                bp.m_Icon = SkillFocusSelection.m_Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                //The selection for these skills is in Miscellaneous/AlchemistDiscoveries
            });
            var PerfectRecallFeature = Helpers.CreateBlueprint<BlueprintFeature>("PerfectRecallFeature", bp => {
                bp.SetName("Perfect Recall");
                bp.SetDescription("At 2nd level, a mindchemist has honed his memory. When making a Knowledge check, he may add his Intelligence bonus on the check a second time. Thus, a " +
                    "mindchemist with 5 ranks in {g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and a +2 Intelligence bonus has a total skill bonus of +9 (5 + 2 + 2) using this ability.");
                bp.AddComponent<DerivativeStatBonus>(c => {
                    c.BaseStat = StatType.Intelligence;
                    c.DerivativeStat = StatType.SkillKnowledgeArcana;
                });
                bp.AddComponent<DerivativeStatBonus>(c => {
                    c.BaseStat = StatType.Intelligence;
                    c.DerivativeStat = StatType.SkillKnowledgeWorld;
                });
                bp.AddComponent<RecalculateOnStatChange>(c => {
                    c.UseKineticistMainStat = false;
                    c.Stat = StatType.Intelligence;
                });
                bp.m_Icon = SenseiWisdom.m_Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            MindchemistArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, MutagenFeature),
                    Helpers.LevelEntry(2, PoisonResistanceFeature),
                    Helpers.LevelEntry(5, PoisonResistanceFeature),
                    Helpers.LevelEntry(8, PoisonResistanceFeature),
                    Helpers.LevelEntry(10, PoisonImmunityFeature)
            };
            MindchemistArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, CognatogenFeature, MindchemistBonusFeats),
                    Helpers.LevelEntry(2, PerfectRecallFeature)

            };
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Mindchemist")) { return; }
            AlchemistClass.m_Archetypes = AlchemistClass.m_Archetypes.AppendToArray(MindchemistArchetype.ToReference<BlueprintArchetypeReference>());

        }
    }
}
