using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
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
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;

namespace ExpandedContent.Tweaks.Spells {
    internal class ShadowClaws {
        public static void AddShadowClaws() {
            var ShadowClawsIcon = AssetLoader.LoadInternal("Skills", "Icon_ShadowClaws.jpg");
            var Icon_ScrollOfShadowClaws = AssetLoader.LoadInternal("Items", "Icon_ScrollOfShadowClaws.png");


            var BloodlineClaws1d4 = Resources.GetBlueprintReference<BlueprintItemWeaponReference>("fe712a5237d918342936c0761cdc2d3e");

            var ShadowClawsBuff = Helpers.CreateBuff("ShadowClawsBuff", bp => {
                bp.SetName("Shadow Claws");
                bp.SetDescription("You summon a pair of claws over your hands formed from semireal material. This grants you two primary claw attacks dealing 1d4 points of damage if you are " +
                    "Medium (1d3 if Small) plus 1 point of Strength damage. A successful Fortitude saving throw negates the Strength damage (DC = this spell’s DC). \nWhile under the effect of this " +
                    "spell, all claw attacks gain this spells Strength damage component.");
                bp.m_Icon = ShadowClawsIcon;
                bp.AddComponent<EmptyHandWeaponOverride>(c => {
                    c.m_Weapon = BloodlineClaws1d4;
                    c.IsPermanent = false;
                    c.IsMonkUnarmedStrike = false;
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
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
                    c.CheckWeaponCategory = true;
                    c.Category = WeaponCategory.Claw;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.Bows;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
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
                        new ContextActionSavingThrow() {
                            Type = SavingThrowType.Fortitude,
                            FromBuff = true, //This might transfer the DC, needs testing
                            HasCustomDC = false,
                            CustomDC = new ContextValue(),
                            Actions = Helpers.CreateActionList(
                                new ContextActionConditionalSaved() {
                                    Succeed = Helpers.CreateActionList(),
                                    Failed = Helpers.CreateActionList(
                                        new ContextActionDealDamage() {
                                            m_Type = ContextActionDealDamage.Type.AbilityDamage,
                                            DamageType = new DamageTypeDescription() {
                                                Type = DamageType.Untyped,
                                                Common = new DamageTypeDescription.CommomData() {
                                                    Reality = 0,
                                                    Alignment = 0,
                                                    Precision = false
                                                },
                                                Physical = new DamageTypeDescription.PhysicalData() {
                                                    Material = 0,
                                                    Form = PhysicalDamageForm.Bludgeoning,
                                                    Enhancement = 0,
                                                    EnhancementTotal = 0
                                                },
                                                Energy = DamageEnergyType.NegativeEnergy
                                            },
                                            Drain = false,
                                            AbilityType = StatType.Strength,
                                            EnergyDrainType = EnergyDrainType.Permanent,
                                            Duration = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = new ContextValue() {
                                                    ValueType = ContextValueType.Simple,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Simple,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                m_IsExtendable = true,
                                            },
                                            PreRolledSharedValue = AbilitySharedValue.Damage,
                                            Value = new ContextDiceValue() {
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = new ContextValue() {
                                                    ValueType = ContextValueType.Simple,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Simple,
                                                    Value = 1,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                            },
                                            IsAoE = false,
                                            HalfIfSaved = false,
                                            UseMinHPAfterDamage = false,
                                            MinHPAfterDamage = 0,
                                            ResultSharedValue = AbilitySharedValue.Damage,
                                            CriticalSharedValue = AbilitySharedValue.Damage
                                        }
                                        )
                                }
                                )
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });

            var ShadowClawsAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShadowClawsAbility", bp => {
                bp.SetName("Shadow Claws");
                bp.SetDescription("You summon a pair of claws over your hands formed from semireal material. This grants you two primary claw attacks dealing 1d4 points of damage if you are " +
                    "Medium (1d3 if Small) plus 1 point of Strength damage. A successful Fortitude saving throw negates the Strength damage (DC = this spell’s DC). \nWhile under the effect of this " +
                    "spell, all claw attacks gain this spells Strength damage component.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ShadowClawsBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,                                
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Illusion;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = ShadowClawsIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("ShadowClawsAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShadowClawsScroll = ItemTools.CreateScroll("ScrollOfShadowClaws", Icon_ScrollOfShadowClaws, ShadowClawsAbility, 2, 3);
            VenderTools.AddScrollToLeveledVenders(ShadowClawsScroll);
            ShadowClawsAbility.AddToSpellList(SpellTools.SpellList.BardSpellList, 2);
            ShadowClawsAbility.AddToSpellList(SpellTools.SpellList.BloodragerSpellList, 2);
            ShadowClawsAbility.AddToSpellList(SpellTools.SpellList.MagusSpellList, 2);
            ShadowClawsAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 2);
        }
    }
}
