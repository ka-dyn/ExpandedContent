using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.FactLogic;

namespace ExpandedContent.Tweaks.Backgrounds {
    internal class ArchdukeOfCheliax {



        public static void AddBackgroundArchdukeOfCheliax() {

            

            var AOCIcon = AssetLoader.LoadInternal("Backgrounds", "Icon_AOC.png");
            var BackgroundNobleSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("7b11f589e81617a46b3e5eda3632508d");
            var BackgroundArchdukeOfCheliax = Helpers.CreateBlueprint<BlueprintFeature>("BackgroundArchdukeOfCheliax", bp => {
                bp.SetName("Archduke of Cheliax");
                bp.SetDescription("An Archduke of Cheliax adds {g|Encyclopedia:Lore_Nature}Lore (Nature){/g}, {g|Encyclopedia:Lore_Religion}Lore (Religion){/g}, " +
                    "{g|Encyclopedia:Knowledge_World}Knowledge (World){/g} and {g|Encyclopedia:Knowledge_Arcana}Knowledge (Arcana){/g} " +
                    "to the list of her class {g|Encyclopedia:Skills}skills{/g}. She can also use her {g|Encyclopedia:Intelligence}Intelligence{/g} instead " +
                    "of {g|Encyclopedia:Wisdom}Wisdom{/g} while attempting Lore (Nature) or Lore (Religion) " +
                    "{g|Encyclopedia:Check}checks{/g}. " +
                    "\nShe also gains a +2 bonus to initiative and can use her Intelligence modifier to determine initiative. " +
                    "\nThe highest rank of the standard nobility are the Archdukes, which are fixed at six members, each of whom rule one of the " +
                    "Archduchies of Cheliax. Archduke is a hereditary title that is held by a single family that has not changed much since the turmoil of the civil war.");
                bp.m_Icon = AOCIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_DescriptionShort = Helpers.CreateString("$BackgroundArchdukeOfCheliax.DescriptionShort", "The highest rank of the standard nobility are the Archdukes, which are fixed at six members, " +
                    "each of whom rule one of the " +
                    "Archduchies of Cheliax. Archduke is a hereditary title that is held by a single family that has not changed much since the turmoil of the civil war.");
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.AddComponent<AddBackgroundClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillKnowledgeWorld;
                    c.Value = 1;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeArcana;
                });
                bp.AddComponent<AddBackgroundClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeArcana;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillKnowledgeArcana;
                    c.Value = 1;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreReligion;
                });
                bp.AddComponent<AddBackgroundClassSkill>(c => {
                    c.Skill = StatType.SkillLoreReligion;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillLoreReligion;
                    c.Value = 1;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.AddComponent<AddBackgroundClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillLoreNature;
                    c.Value = 1;
                });
                bp.AddComponent<ReplaceStatBaseAttribute>(c => {
                    c.TargetStat = StatType.SkillLoreNature;
                    c.BaseAttributeReplacement = StatType.Intelligence;
                    c.ReplaceIfHigher = true;
                });
                bp.AddComponent<ReplaceStatBaseAttribute>(c => {
                    c.TargetStat = StatType.SkillLoreReligion;
                    c.BaseAttributeReplacement = StatType.Intelligence;
                    c.ReplaceIfHigher = true;
                });
                bp.AddComponent<ReplaceStatBaseAttribute>(c => {
                    c.TargetStat = StatType.Initiative;
                    c.BaseAttributeReplacement = StatType.Intelligence;
                    c.ReplaceIfHigher = true;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.Initiative;
                    c.Value = 2;
                });
                bp.AddComponent<RecalculateOnStatChange>(c => {
                    c.UseKineticistMainStat = false;
                    c.Stat = StatType.Intelligence;
                });

            });


            if (ModSettings.AddedContent.Backgrounds.IsDisabled("Archduke of Cheliax")) { return; }
            BackgroundNobleSelection.m_AllFeatures = BackgroundNobleSelection.m_AllFeatures.AppendToArray(BackgroundArchdukeOfCheliax.ToReference<BlueprintFeatureReference>());





        }
    }
}
