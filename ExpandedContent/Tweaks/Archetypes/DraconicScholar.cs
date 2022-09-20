using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Facts;
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
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
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

namespace ExpandedContent.Tweaks.Archetypes {
    internal class DraconicScholar {
        public static void AddDraconicScholar() {
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var ArcanistExploitSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("b8bf3d5023f2d8c428fdf6438cecaea7");
            var ArcanistGreaterExploits = Resources.GetBlueprint<BlueprintFeature>("c7536b93f17c70d4fa3a8cf9aa76bfb7");
            var DrakeCompanionSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("DrakeCompanionSelection");
            var DrakeCompanionFeatureGreen = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureGreen");
            var DrakeCompanionFeatureSilver = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureSilver");
            var DrakeCompanionFeatureBlack = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureBlack");
            var DrakeCompanionFeatureBlue = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureBlue");
            var DrakeCompanionFeatureBrass = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureBrass");
            var DrakeCompanionFeatureBronze = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureBronze");
            var DrakeCompanionFeatureGold = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureGold");
            var DrakeCompanionFeatureRed = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureRed");
            var DrakeCompanionFeatureWhite = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureWhite");
            var DrakeCompanionFeatureCopper = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionFeatureCopper");
            var DrakeBreathCooldown = Resources.GetModBlueprint<BlueprintBuff>("DrakeBreathCooldown");
            var DrakeCompanionSlotFeature = Resources.GetModBlueprint<BlueprintFeature>("DrakeCompanionSlotFeature");
            var ArcanistArcaneReservoirResource = Resources.GetBlueprint<BlueprintAbilityResource>("cac948cbbe79b55459459dd6a8fe44ce");
            var DrakeSubtypeFire = Resources.GetModBlueprint<BlueprintFeature>("DrakeSubtypeFire");
            var DrakeSubtypeCold = Resources.GetModBlueprint<BlueprintFeature>("DrakeSubtypeCold");
            var DrakeSubtypeAir = Resources.GetModBlueprint<BlueprintFeature>("DrakeSubtypeAir");
            var DrakeSubtypeEarth = Resources.GetModBlueprint<BlueprintFeature>("DrakeSubtypeEarth");
            var DraconicExploitIcon = AssetLoader.LoadInternal("Skills", "Icon_DraconicExploit.jpg");
            var DraconicExploitTypeIcon = AssetLoader.LoadInternal("Skills", "Icon_DraconicExploitType.jpg");
            var DraconicExploitQuickenIcon = AssetLoader.LoadInternal("Skills", "Icon_DraconicExploitQuicken.jpg");


            // Archetype
            var DraconicScholarArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("DraconicScholarArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"DraconicScholarArchetype.Name", "Draconic Scholar");
                bp.LocalizedDescription = Helpers.CreateString($"DraconicScholarArchetype.Description", "The unique spellcasting style of dragons is the subject of many scholars. Some arcanists " + 
                    "see the combination of natural and learned spellcasting as similar enough to their own, and decide to pair with a draconic charge to further their knowledge of the arcane.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"DraconicScholarArchetype.Description", "The unique spellcasting style of dragons is the subject of many scholars. Some arcanists " + 
                    "see the combination of natural and learned spellcasting as similar enough to their own, and decide to pair with a draconic charge to further their knowledge of the arcane.");
            });
            // Companion
            var ArcanistDrakeCompanionFeature = Helpers.CreateBlueprint<BlueprintFeatureSelection>("ArcanistDrakeCompanionFeature", bp => {
                bp.SetName("Drake Companion");
                bp.SetDescription("A draconic scholar gains a drake companion, the drake commpanion levels along with the draconic scholar as if they where a draconic druid of the same level." +
                    "\nThis ability replaces spirit, spirit animal, and the hexes gained at 4th and 10th levels and alters spirit magic.");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.Group = FeatureGroup.None;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    DrakeCompanionFeatureBlack.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureBlue.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureBrass.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureBronze.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureCopper.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureGold.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureGreen.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureRed.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureSilver.ToReference<BlueprintFeatureReference>(),
                    DrakeCompanionFeatureWhite.ToReference<BlueprintFeatureReference>()
                };
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DrakeCompanionSelection.ToReference<BlueprintUnitFactReference>() };
                });
            });
            // Exploit Cooldown
            var DraconicExploitCooldown = Helpers.CreateBuff("DraconicExploitCooldown", bp => {
                bp.m_AllowNonContextActions = false;
                bp.SetName("Draconic Exploit Cooldown");
                bp.SetDescription("Targeted by draconic exploits used within 1 minute.");
                bp.m_Icon = DraconicExploitIcon;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });
            // Breath Exploit
            var BloodlineDraconicRedBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("3f31704e595e78942b3640cdc9b95d8b");
            var DraconicExploitBreathAbility = Helpers.CreateBlueprint<BlueprintAbility>("DraconicExploitBreathAbility", bp => {
                bp.SetName("Draconic Exploit - Breath");
                bp.SetDescription("The arcanist can expend 3 points from their arcane reservoir to remove the cooldown on a drakes breath attack weapon. All " +
                    "draconic exploits can only target ally drakes and once a drake is targeted by a exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = BloodlineDraconicRedBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { DrakeCompanionSlotFeature.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = false;
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { DraconicExploitCooldown.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ArcanistArcaneReservoirResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.CostIsCustom = false;
                    c.Amount = 3;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = DrakeBreathCooldown.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = DraconicExploitCooldown.ToReference<BlueprintBuffReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = false,
                            }
                        });
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DraconicExploitBreathFeature = Helpers.CreateBlueprint<BlueprintFeature>("DraconicExploitBreathFeature", bp => {
                bp.SetName("Draconic Exploit - Breath");
                bp.SetDescription("The arcanist can expend 3 points from their arcane reservoir to remove the cooldown on a drakes breath attack weapon. All " +
                    "draconic exploits can only target ally drakes and once a drake is targeted by a exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = DraconicExploitIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DraconicExploitBreathAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            // Elemental Melee Exploit
            var BloodlineDraconicBlueClawsAbility = Resources.GetBlueprint<BlueprintActivatableAbility>("a9afa9dbbf5c8f341bdb1ac801bf2a37");
            var SummonElementalSmallAir = Resources.GetBlueprint<BlueprintAbility>("9cc6b61eba880b944a8f489c44640b5c");
            var DraconicExploitEleMeleeAir = Helpers.CreateBuff("DraconicExploitEleMeleeAir", bp => {
                bp.SetName("Draconic Exploit - Electric Melee");
                bp.SetDescription("The arcanist can expend 3 points from their arcane reservoir to grant a drake 2d6 electric damage on all melee attacks, " +
                    "this effect lasts for rounds equal to half your arcanist level. All draconic exploits can only target ally drakes and once a drake is " +
                    "targeted by a exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = SummonElementalSmallAir.m_Icon;
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.WaitForAttackResolve = true;
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
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = true;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = PhysicalDamageForm.Slashing,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Electricity
                            },
                            Drain = false,
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
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
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 2,
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
                            },
                            IsAoE = false,
                            HalfIfSaved = false,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Stacking = StackingType.Replace;
            });
            var SummonElementalSmallWater = Resources.GetBlueprint<BlueprintAbility>("107788f47c4481f4db6da06498b28270");
            var DraconicExploitEleMeleeCold = Helpers.CreateBuff("DraconicExploitEleMeleeCold", bp => {
                bp.SetName("Draconic Exploit - Cold Melee");
                bp.SetDescription("The arcanist can expend 3 points from their arcane reservoir to grant a drake 2d6 cold damage on all melee attacks, " +
                    "this effect lasts for rounds equal to half your arcanist level. All draconic exploits can only target ally drakes and once a drake is " +
                    "targeted by a exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = SummonElementalSmallWater.m_Icon;
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.WaitForAttackResolve = true;
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
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = true;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = PhysicalDamageForm.Slashing,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Cold
                            },
                            Drain = false,
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
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
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 2,
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
                            },
                            IsAoE = false,
                            HalfIfSaved = false,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Stacking = StackingType.Replace;
            });
            var SummonElementalSmallEarth = Resources.GetBlueprint<BlueprintAbility>("69b36426bb910e341a943f101daed594");
            var DraconicExploitEleMeleeEarth = Helpers.CreateBuff("DraconicExploitEleMeleeEarth", bp => {
                bp.SetName("Draconic Exploit - Acid Melee");
                bp.SetDescription("The arcanist can expend 3 points from their arcane reservoir to grant a drake 2d6 acid damage on all melee attacks, " +
                    "this effect lasts for rounds equal to half your arcanist level. All draconic exploits can only target ally drakes and once a drake is " +
                    "targeted by a exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = SummonElementalSmallEarth.m_Icon;
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.WaitForAttackResolve = true;
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
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = true;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = PhysicalDamageForm.Slashing,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Acid
                            },
                            Drain = false,
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
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
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 2,
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
                            },
                            IsAoE = false,
                            HalfIfSaved = false,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Stacking = StackingType.Replace;
            });
            var SummonElementalSmallFire = Resources.GetBlueprint<BlueprintAbility>("d8f88028204bc2041be9d9d51f58e6a5");
            var DraconicExploitEleMeleeFire = Helpers.CreateBuff("DraconicExploitEleMeleeFire", bp => {
                bp.SetName("Draconic Exploit - Fire Melee");
                bp.SetDescription("The arcanist can expend 3 points from their arcane reservoir to grant a drake 2d6 fire damage on all melee attacks, " +
                    "this effect lasts for rounds equal to half your arcanist level. All draconic exploits can only target ally drakes and once a drake is " +
                    "targeted by a exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = SummonElementalSmallFire.m_Icon;
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.WaitForAttackResolve = true;
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
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = true;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = PhysicalDamageForm.Slashing,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Fire
                            },
                            Drain = false,
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
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
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 2,
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
                            },
                            IsAoE = false,
                            HalfIfSaved = false,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        }
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.Stacking = StackingType.Replace;
            });
            var DraconicExploitEleMeleeAbility = Helpers.CreateBlueprint<BlueprintAbility>("DraconicExploitEleMeleeAbility", bp => {
                bp.SetName("Draconic Exploit - Elemental Melee");
                bp.SetDescription("The arcanist can expend 3 points from their arcane reservoir to grant a drake 2d6 energy damage on all melee attacks, the " +
                    "energy type matches the subtype of the drake affected, this effect lasts for rounds equal to half your arcanist level. All " +
                    "draconic exploits can only target ally drakes and once a drake is targeted by a exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = BloodlineDraconicBlueClawsAbility.m_Icon;
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { DrakeCompanionSlotFeature.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = false;
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { DraconicExploitCooldown.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ArcanistArcaneReservoirResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.CostIsCustom = false;
                    c.Amount = 3;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DraconicExploitCooldown.ToReference<BlueprintBuffReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = false,
                            }
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        m_Fact = DrakeSubtypeAir.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DraconicExploitEleMeleeAir.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        m_IsExtendable = false,
                                    }
                                }),
                            IfFalse = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionHasFact() {
                                                m_Fact = DrakeSubtypeCold.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = DraconicExploitEleMeleeCold.ToReference<BlueprintBuffReference>(),
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                m_IsExtendable = false,
                                            }
                                        }),
                                    IfFalse = Helpers.CreateActionList(
                                        new Conditional() {
                                            ConditionsChecker = new ConditionsChecker() {
                                                Operation = Operation.And,
                                                Conditions = new Condition[] {
                                                    new ContextConditionHasFact() {
                                                        m_Fact = DrakeSubtypeEarth.ToReference<BlueprintUnitFactReference>(),
                                                        Not = false
                                                    }
                                                }
                                            },
                                            IfTrue = Helpers.CreateActionList(
                                                new ContextActionApplyBuff() {
                                                    m_Buff = DraconicExploitEleMeleeEarth.ToReference<BlueprintBuffReference>(),
                                                    DurationValue = new ContextDurationValue() {
                                                        Rate = DurationRate.Rounds,
                                                        DiceType = DiceType.Zero,
                                                        DiceCountValue = 0,
                                                        BonusValue = new ContextValue() {
                                                            ValueType = ContextValueType.Rank,
                                                            Value = 0,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Damage,
                                                            Property = UnitProperty.None
                                                        },
                                                        m_IsExtendable = false,
                                                    }
                                                }),
                                            IfFalse = Helpers.CreateActionList(
                                                new Conditional() {
                                                    ConditionsChecker = new ConditionsChecker() {
                                                        Operation = Operation.And,
                                                        Conditions = new Condition[] {
                                                            new ContextConditionHasFact() {
                                                                m_Fact = DrakeSubtypeFire.ToReference<BlueprintUnitFactReference>(),
                                                                Not = false
                                                            }
                                                        }
                                                    },
                                                    IfTrue = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = DraconicExploitEleMeleeFire.ToReference<BlueprintBuffReference>(),
                                                            DurationValue = new ContextDurationValue() {
                                                                Rate = DurationRate.Rounds,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = 0,
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None
                                                                },
                                                                m_IsExtendable = false,
                                                            }
                                                        }),
                                                    IfFalse = Helpers.CreateActionList()
                                                })
                                        })
                                })
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.Archetype = DraconicScholarArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString("DraconicExploitEleMeleeAbility.Duration", "1 round/2 levels");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DraconicExploitEleMeleeFeature = Helpers.CreateBlueprint<BlueprintFeature>("DraconicExploitEleMeleeFeature", bp => {
                bp.SetName("Draconic Exploit - Elemental Melee");
                bp.SetDescription("The arcanist can expend 3 points from their arcane reservoir to grant a drake 2d6 energy damage on all melee attacks, the " +
                    "energy type matches the subtype of the drake affected, this effect lasts for rounds equal to half your arcanist level. All " +
                    "draconic exploits can only target ally drakes and once a drake is targeted by a exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = DraconicExploitIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DraconicExploitEleMeleeAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            // Heal Exploit
            var Heal = Resources.GetBlueprint<BlueprintAbility>("ff8f1534f66559c478448723e16b6624");
            var HealFX = Resources.GetBlueprint<BlueprintAbility>("ff8f1534f66559c478448723e16b6624").GetComponent<AbilitySpawnFx>();
            var DazeBuff = Resources.GetBlueprint<BlueprintBuff>("9934fedff1b14994ea90205d189c8759");
            var Stunned = Resources.GetBlueprint<BlueprintBuff>("09d39b38bb7c6014394b6daced9bacd3");
            var Blindness = Resources.GetBlueprint<BlueprintBuff>("187f88d96a0ef464280706b63635f2af");
            var GlitterdustBlindness = Resources.GetBlueprint<BlueprintBuff>("52e4be2ba79c8c94d907bdbaf23ec15f");
            var DazzledBuff = Resources.GetBlueprint<BlueprintBuff>("df6d1025da07524429afbae248845ecc");
            var Insanity = Resources.GetBlueprint<BlueprintBuff>("53808be3c2becd24dbe572f77a7f44f8");
            var Feeblemind = Resources.GetBlueprint<BlueprintBuff>("8b3b4c225fe0fb046bfa8881c3ddad0d");
            var EcorcheBuff = Resources.GetBlueprint<BlueprintBuff>("88f1ab751a9555a40abe9d7743e865fb");
            var DraconicExploitHealAbility = Helpers.CreateBlueprint<BlueprintAbility>("DraconicExploitHealAbility", bp => {
                bp.SetName("Draconic Exploit - Heal");
                bp.SetDescription("The arcanist can expend 3 points from their arcane reservoir to cure 10 hit points of damage per level of draconic scholar " +
                    "It immediately ends any and all of the following adverse conditions affecting the target: blinded, confused, dazed, dazzled, diseased, exhausted, " +
                    "fatigued, nauseated, poisoned, sickened, and stunned. All draconic exploits can only target ally drakes and once a drake is targeted by a " +
                    "exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = Heal.m_Icon;
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { DrakeCompanionSlotFeature.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = false;
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { DraconicExploitCooldown.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ArcanistArcaneReservoirResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.CostIsCustom = false;
                    c.Amount = 3;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DraconicExploitCooldown.ToReference<BlueprintBuffReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = false,
                            }
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = DazeBuff.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = Stunned.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = Blindness.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = GlitterdustBlindness.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = DazzledBuff.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = Insanity.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = Feeblemind.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = EcorcheBuff.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Poison | SpellDescriptor.Disease | SpellDescriptor.Daze | SpellDescriptor.Sickened | SpellDescriptor.Fatigue | SpellDescriptor.Nauseated | SpellDescriptor.Exhausted | SpellDescriptor.Stun | SpellDescriptor.Confusion | SpellDescriptor.Blindness
                        },
                        new ContextActionHealTarget() {
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
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                }
                            }
                        });
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = HealFX.PrefabLink;
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.SelectedTarget;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.MultiplyByModifier;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 10;
                    c.Archetype = DraconicScholarArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DraconicExploitHealFeature = Helpers.CreateBlueprint<BlueprintFeature>("DraconicExploitHealFeature", bp => {
                bp.SetName("Draconic Exploit - Heal");
                bp.SetDescription("The arcanist can expend 3 points from their arcane reservoir to cure 10 hit points of damage per level of draconic scholar " +
                    "It immediately ends any and all of the following adverse conditions affecting the target: blinded, confused, dazed, dazzled, diseased, exhausted, " +
                    "fatigued, nauseated, poisoned, sickened, and stunned. All draconic exploits can only target ally drakes and once a drake is targeted by a " +
                    "exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = DraconicExploitIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DraconicExploitHealAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            // Quicken Exploit
            var DraconicExploitQuickenBuff = Helpers.CreateBuff("DraconicExploitQuickenBuff", bp => {
                bp.SetName("Draconic Exploit - Quicken");
                bp.SetDescription("As a swift actions an arcanist can expend 3 points from their arcane reservoir to grant a drake the ability to cast all spells as if they " +
                    "where affected by metamagic quicken for one round, at level 15 this increases to two rounds. All draconic exploits can only target ally drakes and once " +
                    "a drake is targeted by a exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = DraconicExploitQuickenIcon;
                bp.AddComponent<AutoMetamagic>(c => {
                    c.m_AllowedAbilities = AutoMetamagic.AllowedType.SpellOnly;
                    c.Metamagic = Metamagic.Quicken;
                    c.Abilities = new List<BlueprintAbilityReference> { }; //?
                    c.Descriptor = SpellDescriptor.None;
                    c.Once = false;
                    c.MaxSpellLevel = 10;
                    c.School = SpellSchool.None;
                    c.CheckSpellbook = false;
                    c.m_Spellbook = new BlueprintSpellbookReference();
                });
                bp.m_AllowNonContextActions = false;
                bp.Stacking = StackingType.Replace;
            });
            var DraconicExploitQuickenAbility = Helpers.CreateBlueprint<BlueprintAbility>("DraconicExploitQuickenAbility", bp => {
                bp.SetName("Draconic Exploit - Quicken");
                bp.SetDescription("As a swift actions an arcanist can expend 3 points from their arcane reservoir to grant a drake the ability to cast all spells as if they " +
                    "where affected by metamagic quicken for one round, at level 15 this increases to two rounds. All draconic exploits can only target ally drakes and once " +
                    "a drake is targeted by a exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = DraconicExploitQuickenIcon;
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { DrakeCompanionSlotFeature.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = false;
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { DraconicExploitCooldown.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ArcanistArcaneReservoirResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.CostIsCustom = false;
                    c.Amount = 3;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DraconicExploitCooldown.ToReference<BlueprintBuffReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = false,
                            }
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = DraconicExploitQuickenBuff.ToReference<BlueprintBuffReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = false,
                            }
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 14, ProgressionValue = 1 },                  
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 100, ProgressionValue = 2 }
                    };
                    c.Archetype = DraconicScholarArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DraconicExploitQuickenFeature = Helpers.CreateBlueprint<BlueprintFeature>("DraconicExploitQuickenFeature", bp => {
                bp.SetName("Draconic Exploit - Quicken");
                bp.SetDescription("As a swift actions an arcanist can expend 3 points from their arcane reservoir to grant a drake the ability to cast all spells as if they " +
                    "where affected by metamagic quicken for one round, at level 15 this increases to two rounds. All draconic exploits can only target ally drakes and once " +
                    "a drake is targeted by a exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = DraconicExploitIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DraconicExploitQuickenAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            // Physical Exploit
            var BloodragerBloodlineSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("62b33ac8ceb18dd47ad4c8f06849bc01");
            var DraconicExploitPhysicalBuff = Helpers.CreateBuff("DraconicExploitPhysicalBuff", bp => {
                bp.SetName("Draconic Exploit - Physical");
                bp.SetDescription("As a swift actions an arcanist can expend 3 points from their arcane reservoir to grant a drake a +6 bonus in all physical stats and a -2 " +
                    "penalty to all mental stats, this effect lasts for rounds equal to half your arcanist level. All draconic exploits can only target ally drakes and once a " +
                    "drake is targeted by a exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = BloodragerBloodlineSelection.m_Icon;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Strength;
                    c.Value = 6;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Dexterity;
                    c.Value = 6;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Constitution;
                    c.Value = 6;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Wisdom;
                    c.Value = -2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Intelligence;
                    c.Value = -2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Charisma;
                    c.Value = -2;
                });
                bp.m_AllowNonContextActions = false;
                bp.Stacking = StackingType.Replace;
            });
            var DraconicExploitPhysicalAbility = Helpers.CreateBlueprint<BlueprintAbility>("DraconicExploitPhysicalAbility", bp => {
                bp.SetName("Draconic Exploit - Physical");
                bp.SetDescription("As a swift actions an arcanist can expend 3 points from their arcane reservoir to grant a drake a +6 bonus in all physical stats and a -2 " +
                    "penalty to all mental stats, this effect lasts for rounds equal to half your arcanist level. All draconic exploits can only target ally drakes and once a " +
                    "drake is targeted by a exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = BloodragerBloodlineSelection.m_Icon;
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { DrakeCompanionSlotFeature.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = false;
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { DraconicExploitCooldown.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ArcanistArcaneReservoirResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.CostIsCustom = false;
                    c.Amount = 3;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DraconicExploitPhysicalBuff.ToReference<BlueprintBuffReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = false,
                            }
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = DraconicExploitCooldown.ToReference<BlueprintBuffReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = false,
                            }
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString("DraconicExploitPhysicalAbility.Duration", "1 round/2 levels");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DraconicExploitPhysicalFeature = Helpers.CreateBlueprint<BlueprintFeature>("DraconicExploitPhysicalFeature", bp => {
                bp.SetName("Draconic Exploit - Physical");
                bp.SetDescription("As a swift actions an arcanist can expend 3 points from their arcane reservoir to grant a drake a +6 bonus in all physical stats and a -2 " +
                    "penalty to all mental stats, this effect lasts for rounds equal to half your arcanist level. All draconic exploits can only target ally drakes and once a " +
                    "drake is targeted by a exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = DraconicExploitIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DraconicExploitPhysicalAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            // Mental Exploit
            var GrandCognatogenFeature = Resources.GetBlueprint<BlueprintFeature>("af4a320648eb5724889d6ff6255090b2");
            var DraconicExploitMentalBuff = Helpers.CreateBuff("DraconicExploitMentalBuff", bp => {
                bp.SetName("Draconic Exploit - Mental");
                bp.SetDescription("As a swift actions an arcanist can expend 3 points from their arcane reservoir to grant a drake a +6 bonus in all mental stats and a -2 " +
                    "penalty to all physical stats, this effect lasts for rounds equal to half your arcanist level. All draconic exploits can only target ally drakes and once a " +
                    "drake is targeted by a exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = GrandCognatogenFeature.m_Icon;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Strength;
                    c.Value = -2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Dexterity;
                    c.Value = -2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Constitution;
                    c.Value = -2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Wisdom;
                    c.Value = 6;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Intelligence;
                    c.Value = 6;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Charisma;
                    c.Value = 6;
                });
                bp.m_AllowNonContextActions = false;
                bp.Stacking = StackingType.Replace;
            });
            var DraconicExploitMentalAbility = Helpers.CreateBlueprint<BlueprintAbility>("DraconicExploitMentalAbility", bp => {
                bp.SetName("Draconic Exploit - Mental");
                bp.SetDescription("As a swift actions an arcanist can expend 3 points from their arcane reservoir to grant a drake a +6 bonus in all mental stats and a -2 " +
                    "penalty to all physical stats, this effect lasts for rounds equal to half your arcanist level. All draconic exploits can only target ally drakes and once a " +
                    "drake is targeted by a exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = GrandCognatogenFeature.m_Icon;
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { DrakeCompanionSlotFeature.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = false;
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { DraconicExploitCooldown.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ArcanistArcaneReservoirResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.CostIsCustom = false;
                    c.Amount = 3;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DraconicExploitMentalBuff.ToReference<BlueprintBuffReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = false,
                            }
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = DraconicExploitCooldown.ToReference<BlueprintBuffReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = false,
                            }
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString("DraconicExploitMentalAbility.Duration", "1 round/2 levels");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DraconicExploitMentalFeature = Helpers.CreateBlueprint<BlueprintFeature>("DraconicExploitMentalFeature", bp => {
                bp.SetName("Draconic Exploit - Mental");
                bp.SetDescription("As a swift actions an arcanist can expend 3 points from their arcane reservoir to grant a drake a +6 bonus in all mental stats and a -2 " +
                    "penalty to all physical stats, this effect lasts for rounds equal to half your arcanist level. All draconic exploits can only target ally drakes and once a " +
                    "drake is targeted by a exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = DraconicExploitIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DraconicExploitMentalAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            // Exploit Selections
            var ArcanistDraconicExploitSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("ArcanistDraconicExploitSelection", bp => {
                bp.SetName("Draconic Exploits");
                bp.SetDescription("The arcanists studies into the abilities of drakes have given them knowledge on how to exploit the rules of magic and nature regarding them. All " +
                    "draconic exploits can only target ally drakes and once a drake is targeted by a exploit it cannot benefit from another for 1 minute.");
                bp.m_Icon = DraconicExploitIcon;
                bp.IsClassFeature = true;
                bp.Group = FeatureGroup.None;
                bp.m_AllowNonContextActions = false;
                bp.Mode = SelectionMode.OnlyNew;
                bp.m_Features = new BlueprintFeatureReference[] {
                    DraconicExploitBreathFeature.ToReference<BlueprintFeatureReference>(),
                    DraconicExploitEleMeleeFeature.ToReference<BlueprintFeatureReference>(),
                    DraconicExploitHealFeature.ToReference<BlueprintFeatureReference>(),
                    DraconicExploitQuickenFeature.ToReference<BlueprintFeatureReference>(),
                    DraconicExploitPhysicalFeature.ToReference<BlueprintFeatureReference>(),
                    DraconicExploitMentalFeature.ToReference<BlueprintFeatureReference>(),
                };
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    DraconicExploitBreathFeature.ToReference<BlueprintFeatureReference>(),
                    DraconicExploitEleMeleeFeature.ToReference<BlueprintFeatureReference>(),
                    DraconicExploitHealFeature.ToReference<BlueprintFeatureReference>(),
                    DraconicExploitQuickenFeature.ToReference<BlueprintFeatureReference>(),
                    DraconicExploitPhysicalFeature.ToReference<BlueprintFeatureReference>(),
                    DraconicExploitMentalFeature.ToReference<BlueprintFeatureReference>(),
                };
            });
            var ArcanistExploitTypeSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("ArcanistExploitTypeSelection", bp => {
                bp.SetName("Exploit Type Selection");
                bp.SetDescription("The arcanist may select between a arcanist or draconic exploit.");
                bp.m_Icon = DraconicExploitTypeIcon;
                bp.IsClassFeature = true;
                bp.Group = FeatureGroup.None;
                bp.m_AllowNonContextActions = false;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    ArcanistDraconicExploitSelection.ToReference<BlueprintFeatureReference>(),
                    ArcanistExploitSelection.ToReference<BlueprintFeatureReference>()
                };
            });
            DraconicScholarArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, ArcanistExploitSelection),
                    Helpers.LevelEntry(5, ArcanistExploitSelection),
                    Helpers.LevelEntry(7, ArcanistExploitSelection),
                    Helpers.LevelEntry(9, ArcanistExploitSelection),
                    Helpers.LevelEntry(11, ArcanistExploitSelection, ArcanistGreaterExploits),
                    Helpers.LevelEntry(13, ArcanistExploitSelection),
                    Helpers.LevelEntry(15, ArcanistExploitSelection),
                    Helpers.LevelEntry(17, ArcanistExploitSelection),
                    Helpers.LevelEntry(19, ArcanistExploitSelection)
            };
            DraconicScholarArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, ArcanistDrakeCompanionFeature),
                    Helpers.LevelEntry(11, ArcanistDraconicExploitSelection),
                    Helpers.LevelEntry(15, ArcanistExploitTypeSelection),
                    Helpers.LevelEntry(19, ArcanistExploitTypeSelection),
            };
            var DrakeCompanionProgression = Resources.GetModBlueprint<BlueprintProgression>("DrakeCompanionProgression");
            DrakeCompanionProgression.m_Archetypes = DrakeCompanionProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = DraconicScholarArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Draconic Scholar")) { return; }
            ArcanistClass.m_Archetypes = ArcanistClass.m_Archetypes.AppendToArray(DraconicScholarArchetype.ToReference<BlueprintArchetypeReference>());
        }
    }
}
