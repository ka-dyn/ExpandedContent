using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.Utility;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UI.GenericSlot;
using Kingmaker.Blueprints.Items.Ecnchantments;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class Archer {
        public static void AddArcher() {

            var FighterClass = Resources.GetBlueprint<BlueprintCharacterClass>("48ac8db94d5de7645906c7d0ad3bcfbd");
            var BraveryFeature = Resources.GetBlueprint<BlueprintFeature>("f6388946f9f472f4585591b80e9f2452");
            var ArmorTrainingFeature = Resources.GetBlueprint<BlueprintFeature>("3c380607706f209499d951b29d3c44f3");
            var WeaponTrainingSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("b8cecf4e5e464ad41b79d5b42b76b399");
            var WeaponTrainingRankUpSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("5f3cc7b9a46b880448275763fe70c0b0");
            var ArmorMasteryFeature = Resources.GetBlueprint<BlueprintFeature>("ae177f17cfb45264291d4d7c2cb64671");
            var SenseVitals = Resources.GetBlueprint<BlueprintAbility>("82962a820ebc0e7408b8582fdc3f4c0c");

            var ArcherArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("ArcherArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"ArcherArchetype.Name", "Archer");
                bp.LocalizedDescription = Helpers.CreateString($"ArcherArchetype.Description", "The archer is dedicated to the careful mastery of the bow, perfecting his skills with years of practice " +
                    "honed day after day on ranges and hunting for game, or else on the battlefield, raining destruction down on the enemy lines.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"ArcherArchetype.Description", "The archer is dedicated to the careful mastery of the bow, perfecting his skills with years of " +
                    "practice honed day after day on ranges and hunting for game, or else on the battlefield, raining destruction down on the enemy lines.");
                
            });
            var HawkeyeFeature = Helpers.CreateBlueprint<BlueprintFeature>("HawkeyeFeature", bp => {
                bp.SetName("Hawkeye");
                bp.SetDescription("At 2nd level, an archer gains a +1 bonus on Perception checks. This bonus increases by +1 for every 4 levels beyond 2nd.");
                bp.m_Icon = SenseVitals.m_Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.SkillPerception;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 2;
                    c.m_StepLevel = 4;
                    c.m_Class = new BlueprintCharacterClassReference[] { FighterClass.ToReference<BlueprintCharacterClassReference>() };
                });
            });

            var TrickShotSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("TrickShotSelection", bp => {
                bp.SetName("Trick Shot Selection");
                bp.SetDescription("At 3rd level, an archer can choose one of the following combat maneuvers or actions: disarm or sunder. He can perform this " +
                    "action with a bow against any target within 30 feet, with a –4 penalty to his CMB. Every four levels beyond 3rd, he may choose an additional trick shot to learn. " +
                    "\nAt 11th level, he may also choose from the following combat maneuvers: bull rush, grapple, trip.");
                bp.AddFeatures(TrickShotDisarmFeature, TrickShotSunderFeature, TrickShotBullRushFeature, TrickShotGrappleFeature, TrickShotTripFeature);
                bp.Mode = SelectionMode.OnlyNew;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
            });

            ArcherArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(2, BraveryFeature),
                    Helpers.LevelEntry(3, ArmorTrainingFeature),
                    Helpers.LevelEntry(5, WeaponTrainingSelection, WeaponTrainingRankUpSelection),
                    Helpers.LevelEntry(6, BraveryFeature),
                    Helpers.LevelEntry(7, ArmorTrainingFeature),
                    Helpers.LevelEntry(9, WeaponTrainingSelection, WeaponTrainingRankUpSelection),
                    Helpers.LevelEntry(10, BraveryFeature),
                    Helpers.LevelEntry(11, ArmorTrainingFeature),
                    Helpers.LevelEntry(13, WeaponTrainingSelection, WeaponTrainingRankUpSelection),
                    Helpers.LevelEntry(14, BraveryFeature),
                    Helpers.LevelEntry(15, ArmorTrainingFeature),
                    Helpers.LevelEntry(17, WeaponTrainingSelection, WeaponTrainingRankUpSelection),
                    Helpers.LevelEntry(18, BraveryFeature),
                    Helpers.LevelEntry(19, ArmorMasteryFeature)
            };
            ArcherArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(2, HawkeyeFeature),
                    Helpers.LevelEntry(3, TrickShotSelection),
                    Helpers.LevelEntry(5, ExpertArcherFeature),
                    Helpers.LevelEntry(7, TrickShotSelection),
                    Helpers.LevelEntry(9, SafeShotFeature),
                    Helpers.LevelEntry(11, TrickShotSelection),
                    Helpers.LevelEntry(13, EvasiceArcherFeature),
                    Helpers.LevelEntry(15, TrickShotSelection),
                    Helpers.LevelEntry(17, VolleyFeature),
                    Helpers.LevelEntry(19, TrickShotSelection)

            };
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Archer")) { return; }
            FighterClass.m_Archetypes = FighterClass.m_Archetypes.AppendToArray(ArcherArchetype.ToReference<BlueprintArchetypeReference>());

        }
    }
}
