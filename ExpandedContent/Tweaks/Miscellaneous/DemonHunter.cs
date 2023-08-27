using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Designers.Mechanics.Facts;

namespace ExpandedContent.Tweaks.Miscellaneous {
    internal class DemonHunter {
        public static void AddDemonHunter() {



            var SubtypeDemon = Resources.GetBlueprint<BlueprintFeature>("dc960a234d365cb4f905bdc5937e623a");
            var DemonHunterFeature = Helpers.CreateBlueprint<BlueprintFeature>("DemonHunterFeature", bp => {
                bp.SetName("Demon Hunter");
                bp.SetDescription("You are well-versed in demonic lore.\nYou gain a +2 morale bonus on all attack rolls and a +2 morale bonus on caster level checks to penetrate spell resistance made against " +
                    "creatures with the demon subtype you recognize as demons.");                
                bp.AddComponent<PrerequisiteStatValue>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.Stat = StatType.SkillKnowledgeWorld;
                    c.Value = 6;
                });
                bp.AddComponent<SpellPenetrationBonus>(c => {
                    c.Value = 2;
                    c.Descriptor = ModifierDescriptor.Morale;
                    c.CheckFact = true;
                    c.m_RequiredFact = SubtypeDemon.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AttackBonusAgainstFactOwner>(c => {
                    c.AttackBonus = 2;
                    c.Descriptor = ModifierDescriptor.Morale;
                    c.Bonus = 0;
                    c.m_CheckedFact = SubtypeDemon.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.Groups = new FeatureGroup[] {
                    FeatureGroup.Feat,
                    FeatureGroup.CombatFeat
                };
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            if (ModSettings.AddedContent.Feats.IsDisabled("Demon Hunter")) { return; }
            FeatTools.AddAsFeat(DemonHunterFeature);
        }
    }
}
