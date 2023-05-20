using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Spells {
    internal class FinaleSetup {
        public static void AddFinale() {

            //Performance Cooldown
            var PerformanceCooldown = Helpers.CreateBuff("PerformanceCooldown", bp => {
                bp.m_AllowNonContextActions = false;
                bp.SetName("Used a Finale this round");
                bp.SetDescription("");
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });
            //Bardic Performance Patch
            var BardicPerformances = new BlueprintActivatableAbility[] {
                Resources.GetBlueprint<BlueprintActivatableAbility>("12dc796147c42e04487fcad3aaa40cea"), //ArchaeologistLuck
                Resources.GetBlueprint<BlueprintActivatableAbility>("08cc564a5e2c49e4b8eefef393d8041c"), //BeastTamerInspireFerocity
                Resources.GetBlueprint<BlueprintActivatableAbility>("406e8baa9e223d14ca981ee3e80426d1"), //DirgeBardDanceOfTheDead
                Resources.GetBlueprint<BlueprintActivatableAbility>("1b28d456a5b1b4744a1d87cf24309ad1"), //FlameDancerPerformance
                Resources.GetBlueprint<BlueprintActivatableAbility>("32d247b6e6b65794ab47fc372c444a96"), //InciteRageAll
                Resources.GetBlueprint<BlueprintActivatableAbility>("b1d8fdffd132bfd428a8045b7b8b363c"), //InciteRageAllies
                Resources.GetBlueprint<BlueprintActivatableAbility>("dbd7c54ba43e1d54592e037d63117f7b"), //InciteRageEnemies
                Resources.GetBlueprint<BlueprintActivatableAbility>("d5ee8a2e5bf46c549988e9b09a59acd4"), //StormCall
                Resources.GetBlueprint<BlueprintActivatableAbility>("1bf09d2956d0f7a4eb4f6a2bcfb8970f"), //InspireTranquiility
                Resources.GetBlueprint<BlueprintActivatableAbility>("d99d63f84e180d44e8f92b9a832c609d"), //DirgeOfDoomBard
                Resources.GetBlueprint<BlueprintActivatableAbility>("993908ad3fb81f34ba0ed168b7c61f58"), //Fascinate
                Resources.GetBlueprint<BlueprintActivatableAbility>("ad8a93dfa2db7ac4e85133b5e4f14a5f"), //FrighteningTune
                Resources.GetBlueprint<BlueprintActivatableAbility>("430ab3bb57f2cfc46b7b3a68afd4f74e"), //InspireCompetence
                Resources.GetBlueprint<BlueprintActivatableAbility>("5250fe10c377fdb49be449dfe050ba70"), //InspireCourage
                Resources.GetBlueprint<BlueprintActivatableAbility>("be36959e44ac33641ba9e0204f3d227b"), //InspireGreatness
                Resources.GetBlueprint<BlueprintActivatableAbility>("a4ce06371f09f504fa86fcf6d0e021e4"), //InspireHeroics
                Resources.GetBlueprint<BlueprintActivatableAbility>("72e4699d1f86461429bf5c22866b5c4a"), //SenseiInspireCompetence
                Resources.GetBlueprint<BlueprintActivatableAbility>("70274c5aa9124424c984217b62dabee8"), //SenseiInspireCourage
                Resources.GetBlueprint<BlueprintActivatableAbility>("29f4e6db62a482741b30fb548ce55598"), //SenseiInspireGreatness
                Resources.GetBlueprint<BlueprintActivatableAbility>("da129e268aaf38643a4fa2d9e39adc7f"), //MartyrInspireCourage
                Resources.GetBlueprint<BlueprintActivatableAbility>("1188f92d6c06cd1409bb6189039ccb08"), //MartyrInspireGreatness
                Resources.GetBlueprint<BlueprintActivatableAbility>("14f08110ef716574e89bceef1d20da15"), //MartyrInspireHeroics
                Resources.GetBlueprint<BlueprintActivatableAbility>("82f83a21ecbf9344d939c757152f5621"), //BattleProwess
                Resources.GetBlueprint<BlueprintActivatableAbility>("2aa2ee37286e2f94083f6a36cf40c17f"), //InsightfullContemplation
                Resources.GetBlueprint<BlueprintActivatableAbility>("e7fef5dae0700cd428e92a9d049c6cf3"), //SongOfInspiration
                Resources.GetBlueprint<BlueprintActivatableAbility>("80e0c9d9cbcdc2c49a8b59560e049d47"), //CallOfTheWildBeastShapeI
                Resources.GetBlueprint<BlueprintActivatableAbility>("d928fdebd9958a746a15d01334305d19"), //CallOfTheWildBeastShapeII
                Resources.GetBlueprint<BlueprintActivatableAbility>("33f957216dfccc8458f8bb048ae74c71"), //CallOfTheWildBeastShapeIII
                Resources.GetBlueprint<BlueprintActivatableAbility>("5860ff292cd73f7438049b175bbefdd9"), //SongOfTheSenses
                Resources.GetBlueprint<BlueprintActivatableAbility>("44bfb3d43e68de84e9b5cef8defd2bfb"), //DirgeOfDoomSkald
                Resources.GetBlueprint<BlueprintActivatableAbility>("264e93ac44ace16488226b8f7756bf26"), //InspiredRage
                Resources.GetBlueprint<BlueprintActivatableAbility>("e8e5a5cc8b603d5448d9098cc20065f2"), //SongOfStrength
                Resources.GetBlueprint<BlueprintActivatableAbility>("c62bb8123ebc5874d9601ba02907afca"), //SongOfTheFallen
                Resources.GetModBlueprint<BlueprintActivatableAbility>("OceansEchoInspireHeroicsToggleAbility"), //OceansEchoInspireHeroics
                Resources.GetModBlueprint<BlueprintActivatableAbility>("OceansEchoInspireCompetenceToggleAbility"), //OceansEchoInspireCompetence
                Resources.GetModBlueprint<BlueprintActivatableAbility>("OceansEchoInspireCourageToggleAbility"), //OceansEchoInspireCourage
                Resources.GetModBlueprint<BlueprintActivatableAbility>("WyrmSingerDraconicRageAbility"), //WyrmSingerDraconicRageAbility
                Resources.GetModBlueprint<BlueprintActivatableAbility>("WyrmSingerWyrmSagaAbility"), //WyrmSingerWyrmSagaAbility
            };
            foreach (var BardicPerformance in BardicPerformances) {
                BardicPerformance.AddComponent<RestrictionHasFact>(c => {
                    c.m_Feature = PerformanceCooldown.ToReference<BlueprintUnitFactReference>();
                    c.Not = true;
                });
            }


            
        }
    }
}
