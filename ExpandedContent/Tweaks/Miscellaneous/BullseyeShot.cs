using BlueprintCore.Abilities.Restrictions.New;
using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Deities;
using ExpandedContent.Tweaks.DemonLords;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Recommendations;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Visual.Decals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;

namespace ExpandedContent.Tweaks.Miscellaneous {
    internal class BullseyeShot {
        public static void AddBullseyeShot() {

            var PreciseShotFeature = Resources.GetBlueprint<BlueprintFeature>("8f3d1e6b4be006f4d896081f2f889665");
            var PointBlankShotFeature = Resources.GetBlueprint<BlueprintFeature>("0da0c194d6e1d43419eb8d990b28e0ab");
            var BullseyeShotIcon = AssetLoader.LoadInternal("Skills", "Icon_BullseyeShot.png");

            var BullseyeShotBuff = Helpers.CreateBuff("BullseyeShotBuff", bp => {
                bp.SetName("Bullseye Shot");
                bp.SetDescription("You slow your breath, calm yourself, and hit the bullseye, just as you were trained to do. \nYou can spend a move action to steady your shot. " +
                    "When you do, you gain a +4 bonus on your next ranged attack roll before the start of your next turn.");
                bp.m_Icon = BullseyeShotIcon;
                bp.AddComponent<WeaponAttackTypeDamageBonus>(c => {
                    c.Type = WeaponRangeType.Ranged;
                    c.AttackBonus = 4;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 1;
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = false;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.Bows;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Ranged;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = false;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var BullseyeShotAbility = Helpers.CreateBlueprint<BlueprintAbility>("BullseyeShotAbility", bp => {
                bp.SetName("Bullseye Shot");
                bp.SetDescription("You slow your breath, calm yourself, and hit the bullseye, just as you were trained to do. \nYou can spend a move action to steady your shot. " +
                    "When you do, you gain a +4 bonus on your next ranged attack roll before the start of your next turn.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BullseyeShotBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.m_Icon = BullseyeShotIcon;
                bp.Type = AbilityType.Physical;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Move;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var BullseyeShotFeature = Helpers.CreateBlueprint<BlueprintFeature>("BullseyeShotFeature", bp => {
                bp.SetName("Bullseye Shot");
                bp.SetDescription("You slow your breath, calm yourself, and hit the bullseye, just as you were trained to do. \nYou can spend a move action to steady your shot. " +
                    "When you do, you gain a +4 bonus on your next ranged attack roll before the start of your next turn.");
                bp.m_Icon = BullseyeShotIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { BullseyeShotAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<PrerequisiteStatValue>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.Stat = StatType.BaseAttackBonus;
                    c.Value = 5;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = PointBlankShotFeature.ToReference<BlueprintFeatureReference>();
                    c.Group = Prerequisite.GroupType.All;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.m_Feature = PreciseShotFeature.ToReference<BlueprintFeatureReference>();
                    c.Group = Prerequisite.GroupType.All;
                });
                bp.Groups = new FeatureGroup[] {
                    FeatureGroup.Feat,
                    FeatureGroup.CombatFeat
                };
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            if (ModSettings.AddedContent.Feats.IsDisabled("Bullseye Shot")) { return; }
            FeatTools.AddAsFeat(BullseyeShotFeature);
        }
    }
}
