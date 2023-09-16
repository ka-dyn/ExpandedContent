using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Miscellaneous.AlchemistDiscoveries {
    internal class MindchemistSkillDiscovery {
        public static void AddMindchemistSkillDiscovery() {

            var DicoverySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("cd86c437488386f438dcc9ae727ea2a6");
            var AlchemistClass = Resources.GetBlueprint<BlueprintCharacterClass>("0937bec61c0dabc468428f496580c721");
            var MindchemistArchetype = Resources.GetModBlueprint<BlueprintArchetype>("MindchemistArchetype");
            var SkillFocusSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("c9629ef9eebb88b479b2fbc5e836656a");

            var SkillFocusDiplomacy = Resources.GetBlueprint<BlueprintFeature>("1621be43793c5bb43be55493e9c45924");
            var SkillFocusKnowledgeArcana = Resources.GetBlueprint<BlueprintFeature>("cad1b9175e8c0e64583432a22134d33c");
            var SkillFocusKnowledgeWorld = Resources.GetBlueprint<BlueprintFeature>("611e863120c0f9a4cab2d099f1eb20b4");
            var SkillFocusLoreReligion = Resources.GetBlueprint<BlueprintFeature>("c541f80af8d0af4498e1abb6025780c7");
            var SkillFocusTrickery = Resources.GetBlueprint<BlueprintFeature>("7feda1b98f0c169418aa9af78a85953b");
            var SkillFocusStealth = Resources.GetBlueprint<BlueprintFeature>("3a8d34905eae4a74892aae37df3352b9");
            var SkillFocusUseMagicDevice = Resources.GetBlueprint<BlueprintFeature>("f43ffc8e3f8ad8a43be2d44ad6e27914");


            var MindchemistSkillDiscoverySelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("MindchemistSkillDiscoverySelection", bp => {
                bp.SetName("Mindchemist Skill Focus");
                bp.SetDescription("A mindchemist may select Skill Focus (Any Knowledge skill, Lore Religion, Persuasion, Trickery, Stealth, or Use Magic Device) in place of a discovery.");
                bp.m_Icon = SkillFocusSelection.m_Icon;
                bp.AddFeatures(SkillFocusDiplomacy, SkillFocusKnowledgeArcana, SkillFocusKnowledgeWorld, SkillFocusLoreReligion, SkillFocusStealth, SkillFocusTrickery, SkillFocusUseMagicDevice);
                bp.Groups = new FeatureGroup[] {
                    FeatureGroup.Discovery
                };
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = AlchemistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = MindchemistArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                });
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideNotAvailibleInUI = true;
            });

            DicoverySelection.m_AllFeatures = DicoverySelection.m_AllFeatures.AppendToArray(MindchemistSkillDiscoverySelection.ToReference<BlueprintFeatureReference>());
        }
    }
}
