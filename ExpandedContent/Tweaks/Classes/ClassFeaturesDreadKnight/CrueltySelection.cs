using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem.Rules.Damage;
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
    internal class CrueltySelection {



        public static void CreateCrueltySelection() {

            var CrueltySelectIcon = AssetLoader.LoadInternal("Skills", "Icon_CrueltySelect.png");
            var DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadknightClass");
            var TouchOfProfaneCorruptionAbilityFatigued = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityFatigued");
            var TouchOfProfaneCorruptionAbilityShaken = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityShaken");
            var TouchOfProfaneCorruptionAbilitySickened = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilitySickened");
            var TouchOfProfaneCorruptionAbilityDazed = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityDazed");
            var TouchOfProfaneCorruptionAbilityDiseased = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityDiseased");
            var TouchOfProfaneCorruptionAbilityStaggered = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityStaggered");
            var TouchOfProfaneCorruptionAbilityCursed = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityCursed");
            var TouchOfProfaneCorruptionAbilityExhausted = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityExhausted");
            var TouchOfProfaneCorruptionAbilityFrightened = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityFrightened");
            var TouchOfProfaneCorruptionAbilityNauseated = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityNauseated");
            var TouchOfProfaneCorruptionAbilityPoisoned = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityPoisoned");
            var TouchOfProfaneCorruptionAbilityBlinded = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityBlinded");           
            var TouchOfProfaneCorruptionAbilityParalyzed = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityParalyzed");
            var TouchOfProfaneCorruptionAbilityStunned = Resources.GetModBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionAbilityStunned");




            var CrueltyFatigued = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyFatigued", bp => {
                bp.SetName("Cruelty - Fatigued");
                bp.SetDescription("The target is fatigued.");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityFatigued.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var CrueltyShakened = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyShaken", bp => {
                bp.SetName("Cruelty - Shaken");
                bp.SetDescription("The target is shaken for 1 round per level of the Dread Knight.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityShaken.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var CrueltySickened = Helpers.CreateBlueprint<BlueprintFeature>("CrueltySickened", bp => {
                bp.SetName("Cruelty - Sickened");
                bp.SetDescription("The target is sickened for 1 round per level of the Dread Knight.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilitySickened.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var CrueltyDazed = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyDazed", bp => {
                bp.SetName("Cruelty - Dazed");
                bp.SetDescription("The target is dazed for 1 round.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityDazed.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var CrueltyDiseased = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyDiseased", bp => {
                bp.SetName("Cruelty - Diseased");
                bp.SetDescription("The target contracts a disease, as if the Dread Knight had cast contagion, using his Dread Knight level as his caster level.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityDiseased.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var CrueltyStaggered = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyStaggered", bp => {
                bp.SetName("Cruelty - Staggered");
                bp.SetDescription("The target is staggered for 1 round per two levels of the Dread Knight.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityStaggered.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var CrueltyCursed = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyCursed", bp => {
                bp.SetName("Cruelty - Cursed");
                bp.SetDescription("The target is cursed, as if the Dread Knight had cast bestow curse, using his Dread Knight level as his caster level.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityCursed.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var CrueltyExhausted = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyExhausted", bp => {
                bp.SetName("Cruelty - Exhausted");
                bp.SetDescription("The target is exhausted. The Dread Knight must have the fatigue cruelty before selecting this cruelty.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityExhausted.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddPrerequisite(Helpers.Create<PrerequisiteFeature>(c => {
                    c.m_Feature = CrueltyFatigued.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                }));
            });
            var CrueltyFrightened = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyFrightened", bp => {
                bp.SetName("Cruelty - Frightened");
                bp.SetDescription("The target is frightened for 1 round per two levels of the Dread Knight. The Dread Knight must have the shaken cruelty before selecting this cruelty.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityFrightened.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddPrerequisite(Helpers.Create<PrerequisiteFeature>(c => {
                    c.m_Feature = CrueltyShakened.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                }));
            });
            var CrueltyNauseated = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyNauseated", bp => {
                bp.SetName("Cruelty - Nauseated");
                bp.SetDescription("The target is nauseated for 1 round per three levels of the Dread Knight. The Dread Knight must have the sickened cruelty before selecting this cruelty.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityNauseated.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddPrerequisite(Helpers.Create<PrerequisiteFeature>(c => {
                    c.m_Feature = CrueltySickened.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                }));
            });
            var CrueltyPoisoned = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyPoisoned", bp => {
                bp.SetName("Cruelty - Poisoned");
                bp.SetDescription("The target is poisoned, as if the Dread Knight had cast poison, using the Dread Knight’s level as the caster level.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityPoisoned.ToReference<BlueprintUnitFactReference>() };
                });
            });
            
            var CrueltyBlinded = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyBlinded", bp => {
                bp.SetName("Cruelty - Blinded");
                bp.SetDescription("The target is blinded for 1 round per level of the Dread Knight.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityBlinded.ToReference<BlueprintUnitFactReference>() };
                });
            });          
            var CrueltyParalyzed = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyParalyzed", bp => {
                bp.SetName("Cruelty - Paralyzed");
                bp.SetDescription("The target is paralyzed for 1 round.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityParalyzed.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var CrueltyStunned = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyStunned", bp => {
                bp.SetName("Cruelty - Stunned");
                bp.SetDescription("The target is stunned for 1 round per four levels of the Dread Knight.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityStunned.ToReference<BlueprintUnitFactReference>() };
                });
            });


            var CrueltySelection1 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("CrueltySelection1", bp => {
                bp.SetName("Cruelty Selection");
                bp.SetDescription("At 3rd level, and every three levels thereafter, a Dread Knight can select one cruelty. " +
                    "Each cruelty adds an effect to the Dread Knight's touch of profane corruption ability. Whenever the Dread Knight uses " +
                    "touch of profane corruption to deal damage to one target, the target also receives the additional effect from one of the " +
                    "cruelties possessed by the Dread Knight. This choice is made when the touch is used. The target receives a Fortitude " +
                    "save to avoid this cruelty. If the save is successful, the target takes the damage as normal, but not the effects of the " +
                    "cruelty. The DC of this save is equal to 10 + 1/2 the antipaladin’s level + the antipaladin’s Charisma modifier.");
                bp.m_Icon = CrueltySelectIcon;
                bp.Ranks = 1;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                CrueltyFatigued.ToReference<BlueprintFeatureReference>(),
                CrueltySickened.ToReference<BlueprintFeatureReference>(),
                CrueltyShakened.ToReference<BlueprintFeatureReference>() };
                bp.m_Features = new BlueprintFeatureReference[] {
                CrueltyFatigued.ToReference<BlueprintFeatureReference>(),
                CrueltySickened.ToReference<BlueprintFeatureReference>(),
                CrueltyShakened.ToReference<BlueprintFeatureReference>() };
                bp.IsClassFeature = true;
                


            });
            var CrueltySelection2 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("CrueltySelection2", bp => {
                bp.SetName("Cruelty Selection");
                bp.SetDescription("At 3rd level, and every three levels thereafter, a Dread Knight can select one cruelty. " +
                    "Each cruelty adds an effect to the Dread Knight's touch of profane corruption ability. Whenever the Dread Knight uses " +
                    "touch of profane corruption to deal damage to one target, the target also receives the additional effect from one of the " +
                    "cruelties possessed by the Dread Knight. This choice is made when the touch is used. The target receives a Fortitude " +
                    "save to avoid this cruelty. If the save is successful, the target takes the damage as normal, but not the effects of the " +
                    "cruelty. The DC of this save is equal to 10 + 1/2 the antipaladin’s level + the antipaladin’s Charisma modifier.");
                bp.m_Icon = CrueltySelectIcon;
                bp.Ranks = 1;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                CrueltyDazed.ToReference<BlueprintFeatureReference>(),
                CrueltyDiseased.ToReference<BlueprintFeatureReference>(),
                CrueltyStaggered.ToReference<BlueprintFeatureReference>() };
                bp.m_Features = new BlueprintFeatureReference[]  {
                CrueltyDazed.ToReference<BlueprintFeatureReference>(),
                CrueltyDiseased.ToReference<BlueprintFeatureReference>(),
                CrueltyStaggered.ToReference<BlueprintFeatureReference>() };
                bp.IsClassFeature = true;
                


            });
            var CrueltySelection3 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("CrueltySelection3", bp => {
                bp.SetName("Cruelty Selection");
                bp.SetDescription("At 3rd level, and every three levels thereafter, a Dread Knight can select one cruelty. " +
                    "Each cruelty adds an effect to the Dread Knight's touch of profane corruption ability. Whenever the Dread Knight uses " +
                    "touch of profane corruption to deal damage to one target, the target also receives the additional effect from one of the " +
                    "cruelties possessed by the Dread Knight. This choice is made when the touch is used. The target receives a Fortitude " +
                    "save to avoid this cruelty. If the save is successful, the target takes the damage as normal, but not the effects of the " +
                    "cruelty. The DC of this save is equal to 10 + 1/2 the antipaladin’s level + the antipaladin’s Charisma modifier.");
                bp.m_Icon = CrueltySelectIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                CrueltyCursed.ToReference<BlueprintFeatureReference>(),
                CrueltyExhausted.ToReference<BlueprintFeatureReference>(),
                CrueltyFrightened.ToReference<BlueprintFeatureReference>(),
                CrueltyNauseated.ToReference<BlueprintFeatureReference>(),
                CrueltyPoisoned.ToReference<BlueprintFeatureReference>() };
                bp.m_Features = new BlueprintFeatureReference[] {
                CrueltyCursed.ToReference<BlueprintFeatureReference>(),
                CrueltyExhausted.ToReference<BlueprintFeatureReference>(),
                CrueltyFrightened.ToReference<BlueprintFeatureReference>(),
                CrueltyNauseated.ToReference<BlueprintFeatureReference>(),
                CrueltyPoisoned.ToReference<BlueprintFeatureReference>() };
                bp.IsClassFeature = true;
                bp.Ranks = 1;

            });
            var CrueltySelection4 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("CrueltySelection4", bp => {
                bp.SetName("Cruelty Selection");
                bp.SetDescription("At 3rd level, and every three levels thereafter, a Dread Knight can select one cruelty. " +
                    "Each cruelty adds an effect to the Dread Knight's touch of profane corruption ability. Whenever the Dread Knight uses " +
                    "touch of profane corruption to deal damage to one target, the target also receives the additional effect from one of the " +
                    "cruelties possessed by the Dread Knight. This choice is made when the touch is used. The target receives a Fortitude " +
                    "save to avoid this cruelty. If the save is successful, the target takes the damage as normal, but not the effects of the " +
                    "cruelty. The DC of this save is equal to 10 + 1/2 the antipaladin’s level + the antipaladin’s Charisma modifier.");
                bp.m_Icon = CrueltySelectIcon;
                bp.Ranks = 1;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                CrueltyBlinded.ToReference<BlueprintFeatureReference>(),                
                CrueltyParalyzed.ToReference<BlueprintFeatureReference>(),
                CrueltyStunned.ToReference<BlueprintFeatureReference>() };
                bp.m_Features = new BlueprintFeatureReference[] {
                CrueltyBlinded.ToReference<BlueprintFeatureReference>(),
                CrueltyParalyzed.ToReference<BlueprintFeatureReference>(),
                CrueltyStunned.ToReference<BlueprintFeatureReference>() };
                bp.IsClassFeature = true;


            });

        }
    }
}





