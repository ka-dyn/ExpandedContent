using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Visual.Animation.Kingmaker.Actions;

namespace ExpandedContent.Tweaks.Spells {
    internal class Moonstruck {
        public static void AddMoonstruck() {

            var MoonstruckIcon = AssetLoader.LoadInternal("Skills", "Icon_Moonstruck.jpg");
            var Icon_ScrollOfMoonstruck = AssetLoader.LoadInternal("Items", "Icon_ScrollOfMoonstruck.png");
            var Daze = Resources.GetBlueprint<BlueprintBuff>("d2e35b870e4ac574d9873b36402487e5");
            var Bite1d6 = Resources.GetBlueprint<BlueprintItemWeapon>("a000716f88c969c499a535dadcf09286");
            var BloodlineAbyssalClaw1d4 = Resources.GetBlueprint<BlueprintItemWeapon>("289c13ba102d0df43862a488dad8a5d5");

            var MoonstruckDazeBuff = Helpers.CreateBuff("MoonstruckDazeBuff", bp => {
                bp.SetName("Moonstruck Daze");
                bp.SetDescription("During the first round of the moonstruck tranformation, and on the round it ends, the target is dazed by the transformative effects of the spell.");
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.Dazed;
                });
                bp.m_Icon = Daze.Icon;
                bp.FxOnStart = new PrefabLink() { AssetId = "396af91a93f6e2b468f5fa1a944fae8a" };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var MoonstruckBuff = Helpers.CreateBuff("MoonstruckBuff", bp => {
                bp.SetName("Moonstruck");
                bp.SetDescription("You invoke the mystical power of the moon to drive the target into a mad, bestial frenzy. If the target fails its save, it is dazed for 1 round, its " +
                    "nails and teeth elongate and sharpen. The target gains a bite attack and two claw attacks that deal damage appropriate for the creature’s size, and for the " +
                    "remainder of the spell’s duration the target behaves as if under simultaneous rage and confusion spells. During the final round of the spell’s duration, the target " +
                    "is again dazed as it returns to its normal state." +
                    "\nRage: Each affected creature gains a +2 morale {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g} and {g|Encyclopedia:Constitution}Constitution{/g}, " +
                    "a +1 morale bonus on {g|Encyclopedia:Saving_Throw}Will saves{/g}, and a –2 {g|Encyclopedia:Penalty}penalty{/g} to {g|Encyclopedia:Armor_Class}AC{/g} and cannot " +
                    "cast {g|Encyclopedia:Spell}spells{/g}." +
                    "\nConfusion: This {g|Encyclopedia:Spell}spell{/g} causes {g|ConditionConfusion}confusion{/g} in the targets, making them unable to determine their actions. " +
                    "{g|Encyclopedia:Dice}Roll{/g} d100 and consult the following table at the start of each subject's turn each {g|Encyclopedia:Combat_Round}round{/g} to see what " +
                    "it does in that round.\n01–25: Acts normally\n26–50: Does nothing but babble incoherently\n51–75: Deals (1d8 points of {g|Encyclopedia:Damage}damage{/g} + " +
                    "{g|Encyclopedia:Strength}Strength{/g} modifier) to self with item in hand\n76–100: {g|Encyclopedia:Attack}Attacks{/g} nearest creature (for this purpose, a " +
                    "familiar counts as part of the subject's self)");
                //Dazes and start fx
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionSpawnFx() {
                            PrefabLink = new PrefabLink() { AssetId = "a536a9fb639035140b8771b2e3a21ffd" }
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = MoonstruckDazeBuff.ToReference<BlueprintBuffReference>(),
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
                    c.NewRound = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = MoonstruckDazeBuff.ToReference<BlueprintBuffReference>(),
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
                //New weapons
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = Bite1d6.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddComponent<EmptyHandWeaponOverride>(c => {
                    c.m_Weapon = BloodlineAbyssalClaw1d4.ToReference<BlueprintItemWeaponReference>();
                    c.IsPermanent = false;
                    c.IsMonkUnarmedStrike = false;
                });
                //Rage
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Morale;
                    c.Stat = StatType.Strength;
                    c.Value = 2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Morale;
                    c.Stat = StatType.Constitution;
                    c.Value = 2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Stat = StatType.AC;
                    c.Value = -2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Morale;
                    c.Stat = StatType.SaveWill;
                    c.Value = 1;
                });
                bp.AddComponent<ForbidSpellCasting>(c => {
                    c.ForbidMagicItems = false;
                    c.m_IgnoreFeature = null;
                });
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.StealthForbidden;
                });
                //Confusion
                bp.AddComponent<BuffStatusCondition>(c => {
                    c.SaveEachRound = false;
                    c.Condition = UnitCondition.Confusion;
                    c.SaveType = SavingThrowType.Unknown;
                }); bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.MindAffecting;
                });
                bp.m_Icon = MoonstruckIcon;
                //bp.FxOnStart = new PrefabLink() { AssetId = "602fa850c4a94d84eb8aa1bcc0d008c7" }; //Might change this
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });


            var MoonstruckAbility = Helpers.CreateBlueprint<BlueprintAbility>("MoonstruckAbility", bp => {
                bp.SetName("Moonstruck");
                bp.SetDescription("You invoke the mystical power of the moon to drive the target into a mad, bestial frenzy. If the target fails its save, it is dazed for 1 round, its " +
                    "nails and teeth elongate and sharpen. The target gains a bite attack and two claw attacks that deal damage appropriate for the creature’s size, and for the " +
                    "remainder of the spell’s duration the target behaves as if under simultaneous rage and confusion spells. During the final round of the spell’s duration, the target " +
                    "is again dazed as it returns to its normal state." +
                    "\nRage: Each affected creature gains a +2 morale {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g} and {g|Encyclopedia:Constitution}Constitution{/g}, " +
                    "a +1 morale bonus on {g|Encyclopedia:Saving_Throw}Will saves{/g}, and a –2 {g|Encyclopedia:Penalty}penalty{/g} to {g|Encyclopedia:Armor_Class}AC{/g} and cannot " +
                    "cast {g|Encyclopedia:Spell}spells{/g}." +
                    "\nConfusion: This {g|Encyclopedia:Spell}spell{/g} causes {g|ConditionConfusion}confusion{/g} in the targets, making them unable to determine their actions. " +
                    "{g|Encyclopedia:Dice}Roll{/g} d100 and consult the following table at the start of each subject's turn each {g|Encyclopedia:Combat_Round}round{/g} to see what " +
                    "it does in that round.\n01–25: Acts normally\n26–50: Does nothing but babble incoherently\n51–75: Deals (1d8 points of {g|Encyclopedia:Damage}damage{/g} + " +
                    "{g|Encyclopedia:Strength}Strength{/g} modifier) to self with item in hand\n76–100: {g|Encyclopedia:Attack}Attacks{/g} nearest creature (for this purpose, a " +
                    "familiar counts as part of the subject's self)");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Will;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = MoonstruckBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.DamageBonus
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.StartPlusDivStep;
                    c.m_StartLevel = 2;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Enchantment;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.MindAffecting;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = MoonstruckIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower |  Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Bolstered | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("MoonstruckAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = Helpers.CreateString("MoonstruckAbility.SavingThrow", "Will negates");
            });
            var MoonstruckScroll = ItemTools.CreateScroll("ScrollOfMoonstruck", Icon_ScrollOfMoonstruck, MoonstruckAbility, 4, 7);
            VenderTools.AddScrollToLeveledVenders(MoonstruckScroll);
            MoonstruckAbility.AddToSpellList(SpellTools.SpellList.BloodragerSpellList, 4);
            MoonstruckAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 4);
            MoonstruckAbility.AddToSpellList(SpellTools.SpellList.DruidSpellList, 4);
            MoonstruckAbility.AddToSpellList(SpellTools.SpellList.HunterSpelllist, 4);
            MoonstruckAbility.AddToSpellList(SpellTools.SpellList.WitchSpellList, 4);

        }
    }
}
