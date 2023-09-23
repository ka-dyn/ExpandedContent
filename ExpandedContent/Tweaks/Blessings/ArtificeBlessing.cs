using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.UI.ServiceWindow;
using Kingmaker.UI.GenericSlot;
using ExpandedContent.Tweaks.Components;

namespace ExpandedContent.Tweaks.Blessings {
    internal class ArtificeBlessing {
        public static void AddArtificeBlessing() {

            var ArtificeDomainAllowed = Resources.GetBlueprintReference<BlueprintFeatureReference>("3795653d6d3b291418164b27be88cb43");
            var ArtificepriestClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("30b5e47d47a0e37438cc5a80c96cfb99");
            var BlessingResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("d128a6332e4ea7c4a9862b9fdb358cca");
            var ViciousEnchantment = Resources.GetBlueprintReference<BlueprintItemEnchantmentReference>("a1455a289da208144981e4b1ef92cc56");


            var ArtificeBlessingMajorBuff = Helpers.CreateBuff("ArtificeBlessingMajorBuff", bp => {
                bp.SetName("Battle Lust");
                bp.SetDescription("All attacks are treated as if you had the vicious weapon special ability. In addition, you receive a +4 insight bonus on attack rolls made to " +
                    "confirm critical hits. These benefits last for 1 minute.");
                bp.m_Icon = sdasd;
                bp.AddComponent<CriticalConfirmationBonus>(c => {
                    c.Value = 4;
                    c.CheckWeaponRangeType = false;
                    c.Type = WeaponRangeType.Melee;
                });
                bp.AddComponent<BuffEnchantAnyWeapon>(c => {
                    c.m_EnchantmentBlueprint = ViciousEnchantment;
                    c.Slot = EquipSlotBase.SlotType.PrimaryHand;
                });
                bp.AddComponent<BuffEnchantAnyWeapon>(c => {
                    c.m_EnchantmentBlueprint = ViciousEnchantment;
                    c.Slot = EquipSlotBase.SlotType.SecondaryHand;
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var ArtificeBlessingMajorAbility = Helpers.CreateBlueprint<BlueprintAbility>("ArtificeBlessingMajorAbility", bp => {
                bp.SetName("Crafter’s Wrath");
                bp.SetDescription("At 1st level, you can touch one melee weapon and grant it greater power to harm and destroy crafted objects. For 1 minute, whenever this " +
                    "weapon deals damage to constructs or objects, it bypasses hardness and damage reduction.");
                bp.m_Icon = sdasd;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ArtificeBlessingMajorBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.One,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0
                        });
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString("ArtificeBlessingMajorAbility.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ArtificeBlessingMajorFeature = Helpers.CreateBlueprint<BlueprintFeature>("ArtificeBlessingMajorFeature", bp => {
                bp.SetName("Crafter’s Wrath");
                bp.SetDescription("At 1st level, you can touch one melee weapon and grant it greater power to harm and destroy crafted objects. For 1 minute, whenever this " +
                    "weapon deals damage to constructs or objects, it bypasses hardness and damage reduction.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ArtificeBlessingMajorAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInCharacterSheetAndLevelUp = true;
            });


























            var ConstructType = Resources.GetBlueprint<BlueprintFeature>("fd389783027d63343b4a5634bd81645f");
            var ArtificeBlessingMinorBuff = Helpers.CreateBuff("ArtificeBlessingMinorBuff", bp => {
                bp.SetName("Crafter’s Wrath");
                bp.SetDescription("At 1st level, you can touch an ally and grant them greater power to harm and destroy crafted objects. For 1 minute, whenever " +
                    "this ally deals damage to constructs or objects with melee weapons, they bypasses hardness and damage reduction.");
                bp.m_Icon = edrghbe;
                bp.AddComponent<IgnoreDamageReductionOnAttackRangeType>(c => {
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyNaturalAttacks = false;
                    c.CriticalHit = false;
                    c.m_WeaponType = null;
                    c.CheckEnemyFact = true;
                    c.m_CheckedFact = ConstructType.ToReference<BlueprintUnitFactReference>();
                    c.CheckWeaponRangeType = true;
                    c.RangeType = WeaponRangeType.Melee;
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });            
            var ArtificeBlessingMinorAbility = Helpers.CreateBlueprint<BlueprintAbility>("ArtificeBlessingMinorAbility", bp => {
                bp.SetName("Crafter’s Wrath");
                bp.SetDescription("At 1st level, you can touch an ally and grant them greater power to harm and destroy crafted objects. For 1 minute, whenever " +
                    "this ally deals damage to constructs or objects with melee weapons, they bypasses hardness and damage reduction.");
                bp.m_Icon = edrghbe;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ArtificeBlessingMinorBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.One,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0
                        });
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString("ArtificeBlessingMinorAbility.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            
            var ArtificeBlessingFeature = Helpers.CreateBlueprint<BlueprintFeature>("ArtificeBlessingFeature", bp => {
                bp.SetName("Artifice");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ArtificeBlessingMinorAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ArtificepriestClass;
                    c.Level = 10;
                    c.m_Feature = ArtificeBlessingMajorFeature.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.CheckInProgression = true;
                    c.HideInUI = true;
                    c.m_Feature = ArtificeDomainAllowed;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.WarpriestBlessing };
            });

            //BlessingTools.RegisterBlessing(ArtificeBlessingFeature);
            //BlessingTools.CreateDivineTrackerBlessing("DivineTrackerArtificeBlessingFeature", ArtificeBlessingFeature, "");

        }
    }
}
