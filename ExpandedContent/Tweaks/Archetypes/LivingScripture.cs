using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class LivingScripture {






        public static void AddLivingScripture() {

            if (ModSettings.AddedContent.Archetypes.IsDisabled("Living Scripture")) { return; }
            var OverwhelmingSpellAbility9 = Resources.GetBlueprint<BlueprintAbility>("b4e57533bea14f0b9d7b0e7d767ad8ea");
            var OverwhelmingSpellAbility6 = Resources.GetBlueprint<BlueprintAbility>("827ef56e1a4d4a9888d83ed0edc873e2");
            var OverwhelmingSpellAbility3 = Resources.GetBlueprint<BlueprintAbility>("d79544d7b9da4445ad427b00d929c398");
            var WizardFeatSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("8c3102c2ff3b69444b139a98521a4899"); 
            var SpellFocusGreater = Resources.GetBlueprint<BlueprintParametrizedFeature>("5b04b45b228461c43bad768eb0f7c7bf");
            var OverwhelmingSpellFeature = Resources.GetBlueprint<BlueprintFeature>("90bc32b5e7a141c1b20e5596772115e7");
            var ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
            var SpellFocus = Resources.GetBlueprint<BlueprintParametrizedFeature>("16fa59cc9a72a6043b566b49184f53fe");
            var TrueJudgmentFeature = Resources.GetBlueprint<BlueprintFeature>("f069b6557a2013544ac3636219186632");
            var ThirdJudgment = Resources.GetBlueprint<BlueprintFeature>("490c7e92b22cc8a4bb4885a027b355db");
            var SecondJudgment = Resources.GetBlueprint<BlueprintFeature>("33bf0404b70d65f42acac989ec5295b2");
            var JudgmentAdditionalUse = Resources.GetBlueprint<BlueprintFeature>("ee50875819478774b8968701893b52f5");
            var JudgmentFeature = Resources.GetBlueprint<BlueprintFeature>("981def910b98200499c0c8f85a78bde8");
            var SternGaze = Resources.GetBlueprint<BlueprintFeature>("a6d917fd5c9bee0449bd01c92e3b0308");
            var SpellSpecializationSelection = Resources.GetBlueprint<BlueprintParametrizedFeature>("f327a765a4353d04f872482ef3e48c35");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var ScriptureIcon = AssetLoader.LoadInternal("Skills", "Icon_Scripture.png");
            var DivineScriptureFeature = Helpers.CreateBlueprint<BlueprintFeature>("DivineScriptureFeature", bp => {
                bp.SetName("Living Scripture");
                bp.SetDescription("Starting at level 1, a living scripture gains spell focus as a bonus feat and at level 2 he can select one spell that he knows to inscribe upon his body, " +
                    "gaining benefits as per the spell specialization feature. The living scripture may replace this spell with any spell known upon " +
                    "gaining the next level. At level 4, he gains spell focus greater as a bonus feat. At 6th level, the living scripture is a magical bastion of his faith, gaining an innate, stackable spell resistance " +
                    "of 13. At levels 10, 15 and 20, the living scripture " +
                    "discovers new ways to increase the power of his spellcasting, choosing a bonus feat from the Wizard class bonus feat selection.");
                bp.m_Icon = ScriptureIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;                
            });
            var IconSR = AssetLoader.LoadInternal("Skills", "Icon_SR.png");
            var ImpenetrableScriptureFeature = Helpers.CreateBlueprint<BlueprintFeature>("ImpenetrableScriptureFeature", bp => {
                bp.SetName("Impenetrable Scripture");
                bp.SetDescription("At 6th level, the living scripture is a magical bastion of his faith, gaining an innate, stackable spell resistance " +
                    "of 13.");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_Icon = IconSR;
                bp.AddComponent<AddSpellResistance>(c => {
                    c.Value = new Kingmaker.UnitLogic.Mechanics.ContextValue { Value = 13 };
                });
                
             
            });

              
          



            var LivingScriptureArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("LivingScriptureArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"LivingScriptureArchetype.Name", "Living Scripture");
                bp.LocalizedDescription = Helpers.CreateString($"LivingScriptureArchetype.Description", "The living scripture literally wields the sacred " +
                    "word of his deity, memorizing scripture to smite the foes of his god with divine might. Unlike most inquisitors, a living scripture " +
                    "focuses on careful study of divine scripture, valuing knowledge over intuition.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"LivingScriptureArchetype.Description", "The living scripture literally wields the sacred " +
                    "word of his deity, memorizing scripture to smite the foes of his god with divine might. Unlike most inquisitors, a living scripture " +
                    "focuses on careful study of divine scripture, valuing knowledge over intuition.");
                bp.m_ReplaceSpellbook = ClericSpellbook.ToReference<BlueprintSpellbookReference>();               
                bp.RemoveFeatures = new LevelEntry[] {


                    Helpers.LevelEntry(1, JudgmentFeature, SternGaze),
                    Helpers.LevelEntry(4, JudgmentAdditionalUse),
                    Helpers.LevelEntry(7, JudgmentAdditionalUse),
                    Helpers.LevelEntry(8, SecondJudgment),
                    Helpers.LevelEntry(10, JudgmentAdditionalUse),
                    Helpers.LevelEntry(13, JudgmentAdditionalUse),
                    Helpers.LevelEntry(16, JudgmentAdditionalUse, ThirdJudgment),
                    Helpers.LevelEntry(19, JudgmentAdditionalUse),
                    Helpers.LevelEntry(20, TrueJudgmentFeature)

                };
                bp.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, DivineScriptureFeature, SpellFocus),
                    Helpers.LevelEntry(2, SpellSpecializationSelection),
                    Helpers.LevelEntry(4, SpellFocusGreater),
                    Helpers.LevelEntry(6, ImpenetrableScriptureFeature),
                    Helpers.LevelEntry(10, WizardFeatSelection),
                    Helpers.LevelEntry(15, WizardFeatSelection),
                    Helpers.LevelEntry(20, WizardFeatSelection),



                };
            });
            InquisitorClass.m_Archetypes = InquisitorClass.m_Archetypes.AppendToArray(LivingScriptureArchetype.ToReference<BlueprintArchetypeReference>());
            InquisitorClass.Progression.UIGroups = InquisitorClass.Progression.UIGroups.AppendToArray(
                Helpers.CreateUIGroup(SpellFocus, SpellSpecializationSelection, SpellFocusGreater, ImpenetrableScriptureFeature, WizardFeatSelection, WizardFeatSelection, WizardFeatSelection));



        }
    }
}
