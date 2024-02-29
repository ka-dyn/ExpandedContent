using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Archetypes;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueprintCore.Utils;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;

namespace ExpandedContent.Tweaks.Miscellaneous.AlchemistDiscoveries {
    internal class HealingTouchDiscovery {
        public static void AddHealingTouchDiscovery() {

            var DicoverySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("cd86c437488386f438dcc9ae727ea2a6");
            var ExtraDicoverySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("537965879fc24ad3948aaffa7a1a3a66");

            var AlchemistClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("0937bec61c0dabc468428f496580c721");
            var RogueClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("299aa766dee3cbf4790da4efb8c72484");
            var FighterClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("48ac8db94d5de7645906c7d0ad3bcfbd");
            var UndergroundChemistArchetype = Resources.GetBlueprintReference<BlueprintArchetypeReference>("b4f5741d42c4cb04abf5ab3fe33f6fc5");
            var MutationWarriorArchetype = Resources.GetBlueprintReference<BlueprintArchetypeReference>("758e0061a077e54409a3bf0eb51511e5");
            var SpontaneousHealingFeature = Resources.GetBlueprint<BlueprintFeature>("2bc1ee626a69667469ab5c1698b99956");
            var SpontaneousHealingAbility = Resources.GetBlueprint<BlueprintAbility>("f5f45344aa0202c4bbda8287dc63f850");
            var SpontaneousHealingResource = Resources.GetBlueprint<BlueprintAbilityResource>("0b417a7292b2e924782ef2aab9451816");
            var SpontaneousHealingCooldown = Resources.GetBlueprint<BlueprintBuff>("89b501348d054d6408a930efd6105200");
            var LayOnHandsOthers = Resources.GetBlueprint<BlueprintAbility>("caae1dc6fcf7b37408686971ee27db13");




            var HealingTouchDiscoveryAbility = Helpers.CreateBlueprint<BlueprintAbility>("HealingTouchDiscoveryAbility", bp => {
                bp.SetName("Healing Touch");
                bp.SetDescription("The alchemist gains the ability to heal other creatures. As a standard action, he may touch a creature and apply 1 round’s effect " +
                    "of his spontaneous healing discovery to that creature; this counts toward his spontaneous healing limit for the day. The alchemist’s daily limit " +
                    "for hit points healed by spontaneous healing increases to 5 × his alchemist level.");
                bp.m_Icon = LayOnHandsOthers.m_Icon;
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = SpontaneousHealingResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent(SpontaneousHealingAbility.GetComponent<AbilityEffectRunAction>());
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.Inverted = true;
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { SpontaneousHealingCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.RestoreHP;
                });
                bp.AddComponent(SpontaneousHealingAbility.GetComponent<AbilitySpawnFx>());
                bp.Type = AbilityType.Extraordinary;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });





            var HealingTouchDiscoveryFeature = Helpers.CreateBlueprint<BlueprintFeature>("HealingTouchDiscoveryFeature", bp => {
                bp.SetName("Healing Touch");
                bp.SetDescription("The alchemist gains the ability to heal other creatures. As a standard action, he may touch a creature and apply 1 round’s effect " +
                    "of his spontaneous healing discovery to that creature; this counts toward his spontaneous healing limit for the day. The alchemist’s daily limit " +
                    "for hit points healed by spontaneous healing increases to 5 × his alchemist level.");
                bp.m_Icon = LayOnHandsOthers.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { HealingTouchDiscoveryAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<IncreaseResourceAmountBySharedValue>(c => {
                    c.m_Resource = SpontaneousHealingResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Decrease = false;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Progression = ContextRankProgression.StartPlusDivStep;
                    c.m_StartLevel = 1;
                    c.m_StepLevel = 2;                    
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        AlchemistClass,
                        RogueClass,
                        FighterClass
                    };
                    c.Archetype = UndergroundChemistArchetype;
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { MutationWarriorArchetype };
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = SpontaneousHealingFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.m_CharacterClass = AlchemistClass;
                    c.Level = 6;
                });
                bp.Groups = new FeatureGroup[] {
                    FeatureGroup.Discovery
                };
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });



            DicoverySelection.m_AllFeatures = DicoverySelection.m_AllFeatures.AppendToArray(HealingTouchDiscoveryFeature.ToReference<BlueprintFeatureReference>());
            ExtraDicoverySelection.m_AllFeatures = ExtraDicoverySelection.m_AllFeatures.AppendToArray(HealingTouchDiscoveryFeature.ToReference<BlueprintFeatureReference>());

        }
    }
}
