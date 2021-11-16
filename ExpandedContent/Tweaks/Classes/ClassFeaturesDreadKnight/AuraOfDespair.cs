using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Classes.ClassFeaturesDreadKnight {
    internal class AuraOfDespair {

        public static void AddAuraOfDespairFeature() {
            
            var AODespIcon = AssetLoader.LoadInternal("Skills", "Icon_AODespair.png");
            var AuraOfDespairEffectBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfDespairEffectBuff", bp => {

                bp.SetName("Aura of Despair");
                bp.SetDescription("At 8th level, enemies within 10 feet of an antipaladin take a –2 penalty on all saving throws. This penalty does not stack with the penalty from aura of cowardice. " +
                    "This ability functions only while the antipaladin is conscious, not if he is unconscious or dead.");

                bp.IsClassFeature = true;
                bp.m_Icon = AODespIcon;
                bp.AddComponent<BuffAllSavesBonus>(c => {
                    c.Descriptor = Kingmaker.Enums.ModifierDescriptor.Penalty;
                    c.Value = -2;
                });
            });


            var AuraOfDespairArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("AuraOfDespairArea", bp => {

                bp.AggroEnemies = true;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = new Feet() { m_Value = 13 };
                bp.AddComponent<AbilityAreaEffectBuff>(c => {
                    c.m_Buff = AuraOfDespairEffectBuff.ToReference<BlueprintBuffReference>();
                    c.Condition = new ConditionsChecker() {

                        Conditions = new Condition[] {
                            new ContextConditionIsEnemy()
                        }

                    };

                });
            });

            var AuraOfDespairBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfDespairBuff", bp => {
                bp.SetName("Aura of Despair");
                bp.SetDescription("At 8th level, enemies within 10 feet of an antipaladin take a –2 penalty on all saving throws. This penalty does not stack with the penalty from aura of cowardice. " +
                    "This ability functions only while the antipaladin is conscious, not if he is unconscious or dead.");
                bp.m_Icon = AODespIcon;

                bp.IsClassFeature = true;
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = AuraOfDespairArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
            });

            var AuraOfDespairFeature = Helpers.CreateBlueprint<BlueprintFeature>("AuraOfDespairFeature", bp => {

                bp.SetName("Aura of Despair");
                bp.SetDescription("At 8th level, enemies within 10 feet of an antipaladin take a –2 penalty on all saving throws. This penalty does not stack with the penalty from aura of cowardice. " +
                    "This ability functions only while the antipaladin is conscious, not if he is unconscious or dead.");
                bp.m_Icon = AODespIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AuraFeatureComponent>(c => {
                    c.m_Buff = AuraOfDespairBuff.ToReference<BlueprintBuffReference>();
                });

            });
        }
    }
}
    

