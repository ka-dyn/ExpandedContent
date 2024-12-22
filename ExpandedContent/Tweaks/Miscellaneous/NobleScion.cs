using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Designers.Mechanics.Facts;
using ExpandedContent.Tweaks.Components;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.FactLogic;

namespace ExpandedContent.Tweaks.Miscellaneous {
    internal class NobleScion {
        public static void AddNobleScion() {


            var BardicPerformanceResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("e190ba276831b5c4fa28737e5e49e6a6");
            var RagingSongResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("4a2302c4ec2cfb042bba67d825babfec");
            var FakeCelebrityResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("63c6df02067d42dd94fe65a0fc4ec696");
            var ScribingScrollsFeature = Resources.GetBlueprint<BlueprintFeature>("a8a385bf53ee3454593ce9054375a2ec");


            var NobleScionOfTheArtsFeature = Helpers.CreateBlueprint<BlueprintFeature>("NobleScionOfTheArtsFeature", bp => {
                bp.SetName("Noble Scion of the Arts");
                bp.SetDescription("You are a member of a proud noble family, whether or not you remain in good standing with your family. \nYou gain a +2 bonus on all " +
                    "{g|Encyclopedia:Knowledge_World}Knowledge (World){/g} checks. \r\nIf you have the bardic performance ability, you can use that ability for an additional 3 rounds per day.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Value = 2;
                    c.Stat = StatType.SkillKnowledgeWorld;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = BardicPerformanceResource;
                    c.Value = 3;
                });
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = RagingSongResource;
                    c.Value = 3;
                });
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = FakeCelebrityResource;
                    c.Value = 3;
                });
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = false;
            });

            var NobleScionOfLoreFeature = Helpers.CreateBlueprint<BlueprintFeature>("NobleScionOfLoreFeature", bp => {
                bp.SetName("Noble Scion of Lore");
                bp.SetDescription("You are a member of a proud noble family, whether or not you remain in good standing with your family. \nYou gain a +2 bonus on all " +
                    "{g|Encyclopedia:Knowledge_World}Knowledge (World){/g} checks. \r\nYou gain a +1 bonus on all Knowledge and Lore skills.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Value = 3;
                    c.Stat = StatType.SkillKnowledgeWorld;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Value = 1;
                    c.Stat = StatType.SkillKnowledgeArcana;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Value = 1;
                    c.Stat = StatType.SkillLoreNature;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Value = 1;
                    c.Stat = StatType.SkillLoreReligion;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = false;
            });

            var NobleScionOfMagicFeature = Helpers.CreateBlueprint<BlueprintFeature>("NobleScionOfMagicFeature", bp => {
                bp.SetName("Noble Scion of Magic");
                bp.SetDescription("You are a member of a proud noble family, whether or not you remain in good standing with your family. \nYou gain a +2 bonus on all " +
                    "{g|Encyclopedia:Knowledge_World}Knowledge (World){/g} checks. \r\nYou gain a +2 bonus on {g|Encyclopedia:Use_Magic_Device}Use Magic Device{/g} checks, " +
                    "along with the ability to craft scrolls during rest.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Value = 2;
                    c.Stat = StatType.SkillKnowledgeWorld;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Value = 2;
                    c.Stat = StatType.SkillUseMagicDevice;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ScribingScrollsFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = false;
            });

            var NobleScionOfWarFeature = Helpers.CreateBlueprint<BlueprintFeature>("NobleScionOfWarFeature", bp => {
                bp.SetName("Noble Scion of War");
                bp.SetDescription("You are a member of a proud noble family, whether or not you remain in good standing with your family. \nYou gain a +2 bonus on all " +
                    "{g|Encyclopedia:Knowledge_World}Knowledge (World){/g} checks. \r\nYou use your Charisma modifier to adjust Initiative checks instead of your Dexterity modifier.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Value = 2;
                    c.Stat = StatType.SkillKnowledgeWorld;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<ReplaceStatBaseAttribute>(c => {
                    c.TargetStat = StatType.Initiative;
                    c.BaseAttributeReplacement = StatType.Charisma;
                    c.ReplaceIfHigher = true;
                });
                bp.AddComponent<RecalculateOnStatChange>(c => {
                    c.UseKineticistMainStat = false;
                    c.Stat = StatType.Charisma;
                });
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = false;
            });




            var NobleScionFeatureSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("NobleScionFeatureSelection", bp => {
                bp.SetName("Noble Scion");
                bp.SetDescription("You are a member of a proud noble family, whether or not you remain in good standing with your family. \nYou gain a +2 bonus on all " +
                    "{g|Encyclopedia:Knowledge_World}Knowledge (World){/g} checks. \nWhen you select this feat, choose one of the benefits that matches the flavor of your noble family.");
                bp.AddComponent<PrerequisiteCharacterIsFirstLevel>(c => { c.HideInUI = true; });
                bp.AddComponent<PrerequisiteStatValue>(c => {
                    c.Stat = StatType.Charisma;
                    c.Value = 13;                    
                });                
                bp.m_Features = new BlueprintFeatureReference[] {
                    NobleScionOfTheArtsFeature.ToReference<BlueprintFeatureReference>(),
                    NobleScionOfLoreFeature.ToReference<BlueprintFeatureReference>(),
                    NobleScionOfMagicFeature.ToReference<BlueprintFeatureReference>(),
                    NobleScionOfWarFeature.ToReference<BlueprintFeatureReference>(),
                };
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    NobleScionOfTheArtsFeature.ToReference<BlueprintFeatureReference>(),
                    NobleScionOfLoreFeature.ToReference<BlueprintFeatureReference>(),
                    NobleScionOfMagicFeature.ToReference<BlueprintFeatureReference>(),
                    NobleScionOfWarFeature.ToReference<BlueprintFeatureReference>(),
                };
                bp.Groups = new FeatureGroup[] {
                    FeatureGroup.Feat,
                };
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });

            if (ModSettings.AddedContent.Feats.IsDisabled("Noble Scion")) { return; }
            FeatTools.AddAsFeat(NobleScionFeatureSelection);
        }
    }
}
