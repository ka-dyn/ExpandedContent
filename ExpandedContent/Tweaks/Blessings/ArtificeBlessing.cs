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
using Kingmaker.UnitLogic.ActivatableAbilities;

namespace ExpandedContent.Tweaks.Blessings {
    internal class ArtificeBlessing {
        public static void AddArtificeBlessing() {

            var ArtificeDomainAllowed = Resources.GetBlueprintReference<BlueprintFeatureReference>("3795653d6d3b291418164b27be88cb43");
            var WarpriestClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("30b5e47d47a0e37438cc5a80c96cfb99");
            var BlessingResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("d128a6332e4ea7c4a9862b9fdb358cca");
            var MasterStrikeToggleAbility = Resources.GetBlueprint<BlueprintActivatableAbility>("926bff1386d58824688363a3eeb98260");

            var Frost = Resources.GetBlueprint<BlueprintWeaponEnchantment>("421e54078b7719d40915ce0672511d0b");

            var DestructionJudgementBuff = Resources.GetBlueprint<BlueprintBuff>("a8e7e315b5a241b47ad526771eee19b7");


            var ArtificeBlessingMajorBuff = Helpers.CreateBuff("ArtificeBlessingMajorBuff", bp => {
                bp.SetName("Transfer Magic");
                bp.SetDescription("At 10th level, you can temporarily copy a weapon enchantment from one weapon to another. You may copy any permanent enchant with " +
                    "a base price modifier of +1 or +2. If you are using this ability on a double weapon, only one end of the double weapon is affected. The copy " +
                    "lasts for 1 minute. You can use this ability multiple times on the same weapon or weapons. Alternatively, you can use transfer magic to move " +
                    "a +1 or +2 armor special ability from armor you are wearing to another. \nYou may copy enchantments from weapons you are carrying in unused " +
                    "equiptment slots.");
                bp.m_Icon = MasterStrikeToggleAbility.Icon;
                bp.AddComponent<CriticalConfirmationBonus>(c => {
                    c.Value = 4;
                    c.CheckWeaponRangeType = false;
                    c.Type = WeaponRangeType.Melee;
                });
                bp.AddComponent<BuffEnchantAnyWeapon>(c => {
                    c.m_EnchantmentBlueprint = Frost.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.PrimaryHand;
                });
                bp.AddComponent<BuffEnchantAnyWeapon>(c => {
                    c.m_EnchantmentBlueprint = Frost.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.SecondaryHand;
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var ArtificeBlessingMajorAbility = Helpers.CreateBlueprint<BlueprintAbility>("ArtificeBlessingMajorAbility", bp => {
                bp.SetName("Transfer Magic");
                bp.SetDescription("At 10th level, you can temporarily copy a weapon enchantment from one weapon to another. You may copy any permanent enchant with " +
                    "a base price modifier of +1 or +2. If you are using this ability on a double weapon, only one end of the double weapon is affected. The copy " +
                    "lasts for 1 minute. You can use this ability multiple times on the same weapon or weapons. Alternatively, you can use transfer magic to move " +
                    "a +1 or +2 armor special ability from armor you are wearing to another. \nYou may copy enchantments from weapons you are carrying in unused " +
                    "equiptment slots.");
                bp.m_Icon = MasterStrikeToggleAbility.Icon;
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
                bp.AddComponent<CheckWeaponEnchant>(c => {
                    c.IncludeStowedWeapons = true;
                    c.m_Enchantment = Frost.ToReference<BlueprintItemEnchantmentReference>();
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
                bp.SetName("Transfer Magic");
                bp.SetDescription("At 10th level, you can temporarily copy a weapon enchantment from one weapon to another. You may copy any permanent enchant with " +
                    "a base price modifier of +1 or +2. If you are using this ability on a double weapon, only one end of the double weapon is affected. The copy " +
                    "lasts for 1 minute. You can use this ability multiple times on the same weapon or weapons. Alternatively, you can use transfer magic to move " +
                    "a +1 or +2 armor special ability from armor you are wearing to another. \nYou may copy enchantments from weapons you are carrying in unused " +
                    "equiptment slots.");
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
                bp.m_Icon = DestructionJudgementBuff.Icon;
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
                    "this ally deals damage to constructs or objects with melee weapons, they bypasses hardness and damage reduction. \nAt 10th level, you can " +
                    "temporarily copy a weapon enchantment from one weapon to another. You may copy any permanent enchant with a base price modifier of +1 or +2. " +
                    "If you are using this ability on a double weapon, only one end of the double weapon is affected. The copy lasts for 1 minute. You can use " +
                    "this ability multiple times on the same weapon or weapons. Alternatively, you can use transfer magic to move a +1 or +2 armor special ability " +
                    "from armor you are wearing to another. \\nYou may copy enchantments from weapons you are carrying in unused equiptment slots.");
                bp.m_Icon = DestructionJudgementBuff.Icon;
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
                bp.SetDescription("At 1st level, you can touch an ally and grant them greater power to harm and destroy crafted objects. For 1 minute, whenever this " +
                    "ally deals damage to constructs or objects with melee weapons, they bypasses hardness and damage reduction. \nAt 10th level, you can temporarily " +
                    "copy a weapon enchantment from one weapon to another. You may copy any permanent enchant with a base price modifier of +1 or +2. If you are using " +
                    "this ability on a double weapon, only one end of the double weapon is affected. The copy lasts for 1 minute. You can use this ability multiple " +
                    "times on the same weapon or weapons. Alternatively, you can use transfer magic to move a +1 or +2 armor special ability from armor you are " +
                    "wearing to another. \nYou may copy enchantments from weapons you are carrying in unused equiptment slots.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ArtificeBlessingMinorAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = WarpriestClass;
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

            BlessingTools.RegisterBlessing(ArtificeBlessingFeature);
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerArtificeBlessingFeature", ArtificeBlessingFeature, "At 1st level, you can touch an ally and grant them greater power to harm and destroy crafted objects. For 1 minute, whenever this ally deals damage to constructs or objects with melee weapons, they bypasses hardness and damage reduction. \nAt 13th level, you can temporarily copy a weapon enchantment from one weapon to another. You may copy any permanent enchant with a base price modifier of +1 or +2. If you are using this ability on a double weapon, only one end of the double weapon is affected. The copy lasts for 1 minute. You can use this ability multiple times on the same weapon or weapons. Alternatively, you can use transfer magic to move a +1 or +2 armor special ability from armor you are wearing to another. \nYou may copy enchantments from weapons you are carrying in unused equiptment slots.");

        }
    }
}
