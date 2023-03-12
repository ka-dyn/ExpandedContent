using HarmonyLib;
using BlueprintCore.Blueprints;
using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints.Root;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.Utility;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Designers.EventConditionActionSystem.Conditions;
using static UnityModManagerNet.UnityModManager;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ExpandedContent.Tweaks.Miscellaneous {
    internal class ShapechangeFeatsPatch {


        protected static bool IsTabletopTweaksBaseEnabled() { return IsModEnabled("TabletopTweaks-Base"); }

        public static void AddShapechangeFeatsPatch() {

            var WildShapeDragonShapeGreen = Resources.GetModBlueprint<BlueprintAbility>("WildShapeDragonShapeGreen");
            var WildShapeDragonShapeSilver = Resources.GetModBlueprint<BlueprintAbility>("WildShapeDragonShapeSilver");
            var WildShapeDragonShapeBlack = Resources.GetModBlueprint<BlueprintAbility>("WildShapeDragonShapeBlack");
            var WildShapeDragonShapeBlue = Resources.GetModBlueprint<BlueprintAbility>("WildShapeDragonShapeBlue");
            var WildShapeDragonShapeBrass = Resources.GetModBlueprint<BlueprintAbility>("WildShapeDragonShapeBrass");
            var WildShapeDragonShapeRed = Resources.GetModBlueprint<BlueprintAbility>("WildShapeDragonShapeRed");
            var WildShapeDragonShapeWhite = Resources.GetModBlueprint<BlueprintAbility>("WildShapeDragonShapeWhite");
            var WildShapeDragonShapeGreen2 = Resources.GetModBlueprint<BlueprintAbility>("WildShapeDragonShapeGreen2");
            var WildShapeDragonShapeSilver2 = Resources.GetModBlueprint<BlueprintAbility>("WildShapeDragonShapeSilver2");
            var WildShapeDragonShapeBlack2 = Resources.GetModBlueprint<BlueprintAbility>("WildShapeDragonShapeBlack2");
            var WildShapeDragonShapeBlue2 = Resources.GetModBlueprint<BlueprintAbility>("WildShapeDragonShapeBlue2");
            var WildShapeDragonShapeBrass2 = Resources.GetModBlueprint<BlueprintAbility>("WildShapeDragonShapeBrass2");
            var WildShapeDragonShapeRed2 = Resources.GetModBlueprint<BlueprintAbility>("WildShapeDragonShapeRed2");
            var WildShapeDragonShapeWhite2 = Resources.GetModBlueprint<BlueprintAbility>("WildShapeDragonShapeWhite2");
            var ShiftersRushBuff = Resources.GetBlueprint<BlueprintBuff>("c3365d5a75294b9b879c587668620bd4");
            ShiftersRushBuff.AddComponent<FreeActionSpell>(c => { c.m_Ability = WildShapeDragonShapeGreen.ToReference<BlueprintAbilityReference>(); });
            ShiftersRushBuff.AddComponent<FreeActionSpell>(c => { c.m_Ability = WildShapeDragonShapeSilver.ToReference<BlueprintAbilityReference>(); });
            ShiftersRushBuff.AddComponent<FreeActionSpell>(c => { c.m_Ability = WildShapeDragonShapeBlack.ToReference<BlueprintAbilityReference>(); });
            ShiftersRushBuff.AddComponent<FreeActionSpell>(c => { c.m_Ability = WildShapeDragonShapeBlue.ToReference<BlueprintAbilityReference>(); });
            ShiftersRushBuff.AddComponent<FreeActionSpell>(c => { c.m_Ability = WildShapeDragonShapeBrass.ToReference<BlueprintAbilityReference>(); });
            ShiftersRushBuff.AddComponent<FreeActionSpell>(c => { c.m_Ability = WildShapeDragonShapeRed.ToReference<BlueprintAbilityReference>(); });
            ShiftersRushBuff.AddComponent<FreeActionSpell>(c => { c.m_Ability = WildShapeDragonShapeWhite.ToReference<BlueprintAbilityReference>(); });
            ShiftersRushBuff.AddComponent<FreeActionSpell>(c => { c.m_Ability = WildShapeDragonShapeGreen2.ToReference<BlueprintAbilityReference>(); });
            ShiftersRushBuff.AddComponent<FreeActionSpell>(c => { c.m_Ability = WildShapeDragonShapeSilver2.ToReference<BlueprintAbilityReference>(); });
            ShiftersRushBuff.AddComponent<FreeActionSpell>(c => { c.m_Ability = WildShapeDragonShapeBlack2.ToReference<BlueprintAbilityReference>(); });
            ShiftersRushBuff.AddComponent<FreeActionSpell>(c => { c.m_Ability = WildShapeDragonShapeBlue2.ToReference<BlueprintAbilityReference>(); });
            ShiftersRushBuff.AddComponent<FreeActionSpell>(c => { c.m_Ability = WildShapeDragonShapeBrass2.ToReference<BlueprintAbilityReference>(); });
            ShiftersRushBuff.AddComponent<FreeActionSpell>(c => { c.m_Ability = WildShapeDragonShapeRed2.ToReference<BlueprintAbilityReference>(); });
            ShiftersRushBuff.AddComponent<FreeActionSpell>(c => { c.m_Ability = WildShapeDragonShapeWhite2.ToReference<BlueprintAbilityReference>(); });












            var WildShapeDragonShapeFeature = Resources.GetModBlueprint<BlueprintFeature>("WildShapeDragonShapeFeature");

            
            if (!IsTabletopTweaksBaseEnabled()) {  //TTT disabled
                var ShiftersRushFeatureCondition = Resources.GetBlueprint<BlueprintFeature>("4ddc88f422a84f76a952e24bec7b53e1").GetComponent<PrerequisiteCondition>().Condition;
                var ShiftersRushFeatureConditionsChecker = ShiftersRushFeatureCondition as OrAndLogic;
                ShiftersRushFeatureConditionsChecker.ConditionsChecker.Conditions = ShiftersRushFeatureConditionsChecker.ConditionsChecker.Conditions.AppendToArray(
                    new ContextConditionHasFact() { Not = false, m_Fact = WildShapeDragonShapeFeature.ToReference<BlueprintUnitFactReference>() }
                    );
            }

            if (IsTabletopTweaksBaseEnabled()) { //TTT enabled

                if (ModSupportUtilities.GetShiftersRushTTTBaseSetting()) { //TTT Option ON
                    var ShiftersRushFeaturePrerequisite = Resources.GetBlueprint<BlueprintFeature>("4ddc88f422a84f76a952e24bec7b53e1").GetComponent<PrerequisiteFeaturesFromList>();
                    ShiftersRushFeaturePrerequisite.m_Features = ShiftersRushFeaturePrerequisite.m_Features.AppendToArray(WildShapeDragonShapeFeature.ToReference<BlueprintFeatureReference>());
                }
                else { //TTT Option OFF
                    var ShiftersRushFeatureCondition = Resources.GetBlueprint<BlueprintFeature>("4ddc88f422a84f76a952e24bec7b53e1").GetComponent<PrerequisiteCondition>().Condition;
                    var ShiftersRushFeatureConditionsChecker = ShiftersRushFeatureCondition as OrAndLogic;
                    ShiftersRushFeatureConditionsChecker.ConditionsChecker.Conditions = ShiftersRushFeatureConditionsChecker.ConditionsChecker.Conditions.AppendToArray(
                        new ContextConditionHasFact() { Not = false, m_Fact = WildShapeDragonShapeFeature.ToReference<BlueprintUnitFactReference>() }
                        );
                }
            }
        }
        protected static bool IsModEnabled(string modName) {
            return modEntries.Where(mod => mod.Info.Id.Equals(modName) && mod.Enabled && !mod.ErrorOnLoading).Any();
        }
    }
}
