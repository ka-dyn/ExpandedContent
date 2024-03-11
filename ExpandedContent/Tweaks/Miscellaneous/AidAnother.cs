using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Components;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UI.ServiceWindow.CharacterScreen;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Visual.Animation.Kingmaker.Actions;

namespace ExpandedContent.Tweaks.Miscellaneous {
    internal class AidAnother {
        public static void AddAidAnother() {
            var AidAnotherIcon = AssetLoader.LoadInternal("Skills", "Icon_AidAnother.jpg");
            var AidAnotherDefenceIcon = AssetLoader.LoadInternal("Skills", "Icon_AidAnotherDefence.jpg");
            var AidAnotherOffenceIcon = AssetLoader.LoadInternal("Skills", "Icon_AidAnotherOffence.jpg");
            var PerfectAidAnotherDefenceIcon = AssetLoader.LoadInternal("Skills", "Icon_PerfectAidAnotherDefence.jpg");
            var PerfectAidAnotherOffenceIcon = AssetLoader.LoadInternal("Skills", "Icon_PerfectAidAnotherOffence.jpg");
            var SwiftAidAnotherIcon = AssetLoader.LoadInternal("Skills", "Icon_SwiftAidAnother.jpg");
            var SwiftAidAnotherDefenceIcon = AssetLoader.LoadInternal("Skills", "Icon_SwiftAidAnotherDefence.jpg");
            var SwiftAidAnotherOffenceIcon = AssetLoader.LoadInternal("Skills", "Icon_SwiftAidAnotherOffence.jpg");
            var OracleRevelationSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("60008a10ad7ad6543b1f63016741a5d2");
            var CombatExpertise = Resources.GetBlueprint<BlueprintFeature>("4c44724ffa8844f4d9bedb5bb27d144a");
            var OracleSuccorMysteryFeature = Resources.GetModBlueprint<BlueprintFeature>("OracleSuccorMysteryFeature");
            var EnlightnedPhilosopherSuccorMysteryFeature = Resources.GetModBlueprint<BlueprintFeature>("EnlightnedPhilosopherSuccorMysteryFeature");
            var DivineHerbalistSuccorMysteryFeature = Resources.GetModBlueprint<BlueprintFeature>("DivineHerbalistSuccorMysteryFeature");
            var OceansEchoSuccorMysteryFeature = Resources.GetModBlueprint<BlueprintFeature>("OceansEchoSuccorMysteryFeature");
            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");


            //Stackable bonuses
                //Community Blessing
            var CommunityBlessingMinorBuff = Resources.GetModBlueprint<BlueprintBuff>("CommunityBlessingMinorBuff");
            var CommunityBlessingMinorProperty = Helpers.CreateBlueprint<BlueprintUnitProperty>("CommunityBlessingMinorProperty", bp => {
                bp.AddComponent<FactRankGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs
                    };
                    c.m_Fact = CommunityBlessingMinorBuff.ToReference<BlueprintUnitFactReference>();
                });
                bp.BaseValue = 2;
                bp.OperationOnComponents = BlueprintUnitProperty.MathOperation.Multiply;
            });
            //Golden Legionnarire
            var GoldenLegionnarireImprovedAidFeature = Helpers.CreateBlueprint<BlueprintFeature>("GoldenLegionnarireImprovedAidFeature", bp => {
                bp.SetName("Improved Aid");
                bp.SetDescription("At 4th level, when a Golden Legionnaire successfully uses the aid another action in Combat, the bonus on the ally’s attack roll or to its " +
                    "Armor Class increases by 1. At 9th level, the bonus increases by an additional 1.");
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
            });
            var GoldenLegionnarireImprovedAidProperty = Helpers.CreateBlueprint<BlueprintUnitProperty>("GoldenLegionnarireImprovedAidProperty", bp => {
                //ClassLevelGetter added in mod support
                bp.AddComponent<FactRankGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs
                    };
                    c.m_Fact = GoldenLegionnarireImprovedAidFeature.ToReference<BlueprintUnitFactReference>();
                });
                bp.BaseValue = 1;
                bp.OperationOnComponents = BlueprintUnitProperty.MathOperation.Multiply;
            });

            var StackableBonusesProperty = Helpers.CreateBlueprint<BlueprintUnitProperty>("StackableBonusesProperty", bp => {
                bp.AddComponent<CustomPropertyGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_StartLevel = 0,
                        m_StepLevel = 0,
                        m_Negate = false,
                        m_LimitType = PropertySettings.LimitType.None,
                        m_Min = 0,
                        m_Max = 5,
                    };
                    c.m_Property = CommunityBlessingMinorProperty.ToReference<BlueprintUnitPropertyReference>();
                });
                bp.AddComponent<CustomPropertyGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_StartLevel = 0,
                        m_StepLevel = 0,
                        m_Negate = false,
                        m_LimitType = PropertySettings.LimitType.None,
                        m_Min = 0,
                        m_Max = 5,
                    };
                    c.m_Property = GoldenLegionnarireImprovedAidProperty.ToReference<BlueprintUnitPropertyReference>();
                });
                bp.BaseValue = 0;
                bp.OperationOnComponents = BlueprintUnitProperty.MathOperation.Sum;
            });

            //Oracle Perfect Aid
            var OracleRevelationPerfectAid = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationPerfectAid", bp => {
                bp.SetName("Perfect Aid");
                bp.SetDescription("You can effortlessly give aid to your allies, whether that means providing them with help attacking or defending them in the heat of combat. Whenever you use the aid another action " +
                    "to inflict a penalty on attack rolls or to AC against one of your allies, the penalty you inflict increases by 1. This bonus increases by 1 at 4th level and every 5 oracle levels thereafter (to a " +
                    "maximum of -5 at 19th level). It doesn’t stack with other feats or class features that improve the bonus you provide when using the aid another action. This revelation also counts as the Combat " +
                    "Expertise feat, but only for the purpose of meeting the prerequisites of the Swift Aid feat.");
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleSuccorMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherSuccorMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistSuccorMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoSuccorMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            var OracleRevelationPerfectAidProperty = Helpers.CreateBlueprint<BlueprintUnitProperty>("OracleRevelationPerfectAidProperty", bp => {
                bp.AddComponent<ClassLevelGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.StartPlusDivStep,
                        m_StartLevel = -1,
                        m_StepLevel = 5,
                        m_Negate = false,
                        m_LimitType = PropertySettings.LimitType.Max,
                        m_Min = 0,
                        m_Max = 5,
                    };
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<FactRankGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs
                    };
                    c.m_Fact = OracleRevelationPerfectAid.ToReference<BlueprintUnitFactReference>();
                });
                bp.BaseValue = 1;
                bp.OperationOnComponents = BlueprintUnitProperty.MathOperation.Multiply;
            });
            //Asavir Camaraderie Added to Asavir in ModSupport
            var AsavirCamaraderieFeature = Helpers.CreateBlueprint<BlueprintFeature>("AsavirCamaraderieFeature", bp => {
                bp.SetName("Camaraderie");
                bp.SetDescription("An asavir is skilled at forging those who battle by her side into a loyal and well-coordinated team and exhorting them to fight bravely. " +
                    "The bonus she grants to any allies when she takes the aid another action in combat increases by 1. At 5th level, the bonus instead increases " +
                    "by 2, and at 9th level, it instead increases by 3. These bonuses do not stack with other abilities that enhance the aid another action.");                
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
            });
            var AsavirCamaraderieProperty = Helpers.CreateBlueprint<BlueprintUnitProperty>("AsavirCamaraderieProperty", bp => {
                //ClassLevelGetter added in mod support
                bp.AddComponent<FactRankGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs
                    };
                    c.m_Fact = AsavirCamaraderieFeature.ToReference<BlueprintUnitFactReference>();
                });
                bp.BaseValue = 1;
                bp.OperationOnComponents = BlueprintUnitProperty.MathOperation.Multiply;
            });
            




            var AidAnotherDefenceBuff = Helpers.CreateBuff("AidAnotherDefenceBuff", bp => {
                bp.SetName("Aid Another - Distracted Attacks");
                bp.SetDescription("In melee combat, you can help your allies attack or defend by distracting or interfering with an opponent. If you’re in position to make a melee attack on an opponent you can attempt to " +
                    "aid your allies against that enemy as a standard action. The distracted enemy is inflicted with a -2 penalty on their next attack roll against your allies. This penalty only applies to the first attack " +
                    "roll and is removed after 1 round.");
                bp.m_Icon = AidAnotherDefenceIcon;
                bp.AddComponent<AttackBonusAgainstNotCaster>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = -2,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                });
                bp.AddComponent<AddInitiatorAttackRollTrigger>(c => {
                    c.OnlyHit = false;
                    c.CriticalHit = false;
                    c.SneakAttack = false;
                    c.OnOwner = false;
                    c.CheckWeapon = false;
                    c.WeaponCategory = WeaponCategory.UnarmedStrike;
                    c.AffectFriendlyTouchSpells = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
            });
            var AidAnotherOffenceBuff = Helpers.CreateBuff("AidAnotherOffenceBuff", bp => {
                bp.SetName("Aid Another - Distracted Defending");
                bp.SetDescription("In melee combat, you can help your allies attack or defend by distracting or interfering with an opponent. If you’re in position to make a melee attack on an opponent you can attempt to " +
                    "aid your allies against that enemy as a standard action. The distracted enemy is inflicted with a -2 penalty to their AC against the next attack by your allies. This penalty only applies to the " +
                    "first attack roll and is removed after 1 round.");
                bp.m_Icon = AidAnotherOffenceIcon;
                bp.AddComponent<ACBonusAgainstNotCaster>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = -2,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                });
                bp.AddComponent<AddTargetAttackRollTrigger>(c => {
                    c.OnlyHit = false;
                    c.CriticalHit = false;
                    c.OnlyMelee = false;
                    c.NotReach = false;
                    c.CheckCategory = false;
                    c.Not = false;
                    c.Categories = new WeaponCategory[0];
                    c.AffectFriendlyTouchSpells = false;
                    c.ActionsOnAttacker = Helpers.CreateActionList();
                    c.ActionOnSelf = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
            });
            //The perfect aid another buffs are now set to invisible, and will be used for every feature that increases aid another.
            var PerfectAidAnotherDefenceBuff = Helpers.CreateBuff("PerfectAidAnotherDefenceBuff", bp => {
                bp.SetName("Aid Another Bonus - Distracted Attacks");
                bp.SetDescription("");
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.m_Icon = PerfectAidAnotherDefenceIcon;
                bp.AddComponent<AttackBonusAgainstNotCaster>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.StatBonus
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.MaxCustomProperty;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_CustomProperty = OracleRevelationPerfectAidProperty.ToReference<BlueprintUnitPropertyReference>();
                    c.m_CustomPropertyList = new BlueprintUnitPropertyReference[] {
                        OracleRevelationPerfectAidProperty.ToReference<BlueprintUnitPropertyReference>(),
                        AsavirCamaraderieProperty.ToReference<BlueprintUnitPropertyReference>(),
                        StackableBonusesProperty.ToReference<BlueprintUnitPropertyReference>()
                    };
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.StatBonus;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.StatBonus
                        }
                    };
                    c.Modifier = -1;
                });
                bp.AddComponent<AddInitiatorAttackRollTrigger>(c => {
                    c.OnlyHit = false;
                    c.CriticalHit = false;
                    c.SneakAttack = false;
                    c.OnOwner = false;
                    c.CheckWeapon = false;
                    c.WeaponCategory = WeaponCategory.UnarmedStrike;
                    c.AffectFriendlyTouchSpells = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
            });
            var PerfectAidAnotherOffenceBuff = Helpers.CreateBuff("PerfectAidAnotherOffenceBuff", bp => {
                bp.SetName("Aid Another Bonus - Distracted Defending");
                bp.SetDescription("");
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.m_Icon = PerfectAidAnotherOffenceIcon;
                bp.AddComponent<ACBonusAgainstNotCaster>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.StatBonus
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.MaxCustomProperty;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_CustomProperty = OracleRevelationPerfectAidProperty.ToReference<BlueprintUnitPropertyReference>();
                    c.m_CustomPropertyList = new BlueprintUnitPropertyReference[] {
                        OracleRevelationPerfectAidProperty.ToReference<BlueprintUnitPropertyReference>(),
                        AsavirCamaraderieProperty.ToReference<BlueprintUnitPropertyReference>(),
                        StackableBonusesProperty.ToReference<BlueprintUnitPropertyReference>()
                    };
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.StatBonus;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.StatBonus
                        }
                    };
                    c.Modifier = -1;
                });
                bp.AddComponent<AddTargetAttackRollTrigger>(c => {
                    c.OnlyHit = false;
                    c.CriticalHit = false;
                    c.OnlyMelee = false;
                    c.NotReach = false;
                    c.CheckCategory = false;
                    c.Not = false;
                    c.Categories = new WeaponCategory[0];
                    c.AffectFriendlyTouchSpells = false;
                    c.ActionsOnAttacker = Helpers.CreateActionList();
                    c.ActionOnSelf = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
            });
            var SwiftAidAnotherDefenceBuff = Helpers.CreateBuff("SwiftAidAnotherDefenceBuff", bp => {
                bp.SetName("Swift Aid Another - Distracted Attacks");
                bp.SetDescription("In melee combat, you can help your allies attack or defend by distracting or interfering with an opponent. If you’re in position to make a melee attack on an opponent you can attempt to " +
                    "aid your allies against that enemy as a swift action. The distracted enemy is inflicted with a -1 penalty on their next attack roll against your allies. This penalty only applies to the first attack " +
                    "roll and is removed after 1 round.");
                bp.m_Icon = SwiftAidAnotherDefenceIcon;
                bp.AddComponent<AttackBonusAgainstNotCaster>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = -1,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage
                    };
                });
                bp.AddComponent<AddInitiatorAttackRollTrigger>(c => {
                    c.OnlyHit = false;
                    c.CriticalHit = false;
                    c.SneakAttack = false;
                    c.OnOwner = false;
                    c.CheckWeapon = false;
                    c.WeaponCategory = WeaponCategory.UnarmedStrike;
                    c.AffectFriendlyTouchSpells = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
            });
            var SwiftAidAnotherOffenceBuff = Helpers.CreateBuff("SwiftAidAnotherOffenceBuff", bp => {
                bp.SetName("Swift Aid Another - Distracted Defending");
                bp.SetDescription("In melee combat, you can help your allies attack or defend by distracting or interfering with an opponent. If you’re in position to make a melee attack on an opponent you can attempt to " +
                    "aid your allies against that enemy as a swift action. The distracted enemy is inflicted with a -1 penalty to either their AC against the next attack by your allies. This penalty only applies to the " +
                    "first attack roll and is removed after 1 round.");
                bp.m_Icon = SwiftAidAnotherOffenceIcon;
                bp.AddComponent<ACBonusAgainstNotCaster>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = -1,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage
                    };
                });
                bp.AddComponent<AddTargetAttackRollTrigger>(c => {
                    c.OnlyHit = false;
                    c.CriticalHit = false;
                    c.OnlyMelee = false;
                    c.NotReach = false;
                    c.CheckCategory = false;
                    c.Not = false;
                    c.Categories = new WeaponCategory[0];
                    c.AffectFriendlyTouchSpells = false;
                    c.ActionsOnAttacker = Helpers.CreateActionList();
                    c.ActionOnSelf = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
            });

            var AidAnotherAbility = Helpers.CreateBlueprint<BlueprintAbility>("AidAnotherAbility", bp => {
                bp.SetName("Aid Another");
                bp.SetDescription("In melee combat, you can help your allies attack or defend by distracting or interfering with an opponent. If you’re in position to make a melee attack on an opponent you can attempt to " +
                    "aid your allies against that enemy as a standard action. The distracted enemy is inflicted with a -2 penalty to either their AC against the next attack by your allies, or a -2 penalty on their next " +
                    "attack roll against your allies. This penalty only applies to the first attack roll and is removed after 1 round.");
                bp.m_Icon = AidAnotherIcon;
                //Variants added later
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var AidAnotherDefenceAbility = Helpers.CreateBlueprint<BlueprintAbility>("AidAnotherDefenceAbility", bp => {
                bp.SetName("Aid Another - Distracted Attacks");
                bp.SetDescription("In melee combat, you can help your allies attack or defend by distracting or interfering with an opponent. If you’re in position to make a melee attack on an opponent you can attempt to " +
                    "aid your allies against that enemy as a standard action. The distracted enemy is inflicted with a -2 penalty on their next attack roll against your allies. This penalty only applies to the first attack " +
                    "roll and is removed after 1 round.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = AidAnotherDefenceBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = 0,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            IsNotDispelable = true
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionCasterHasFact() {
                                        Not = false,
                                        m_Fact = OracleRevelationPerfectAid.ToReference<BlueprintUnitFactReference>()
                                    },
                                    new ContextConditionCasterHasFact() {
                                        Not = false,
                                        m_Fact = AsavirCamaraderieFeature.ToReference<BlueprintUnitFactReference>()
                                    },
                                    new ContextConditionCasterHasFact() {
                                        Not = false,
                                        m_Fact = CommunityBlessingMinorBuff.ToReference<BlueprintUnitFactReference>()
                                    },
                                    new ContextConditionCasterHasFact() {
                                        Not = false,
                                        m_Fact = GoldenLegionnarireImprovedAidFeature.ToReference<BlueprintUnitFactReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = PerfectAidAnotherDefenceBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = 0,
                                        DiceCountValue = 0,
                                        BonusValue = 1
                                    },
                                    IsNotDispelable = true
                                }),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = SwiftAidAnotherDefenceBuff.ToReference<BlueprintBuffReference>()
                        });
                });
                bp.AddComponent<AbilityTargetIsAlly>(c => {
                    c.Not = true;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = AidAnotherDefenceIcon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.m_Parent = AidAnotherAbility.ToReference<BlueprintAbilityReference>();
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = Helpers.CreateString("AidAnotherDefenceAbility.Duration", "1 round or until used");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var AidAnotherOffenceAbility = Helpers.CreateBlueprint<BlueprintAbility>("AidAnotherOffenceAbility", bp => {
                bp.SetName("Aid Another - Distracted Defending");
                bp.SetDescription("In melee combat, you can help your allies attack or defend by distracting or interfering with an opponent. If you’re in position to make a melee attack on an opponent you can attempt to " +
                    "aid your allies against that enemy as a standard action. The distracted enemy is inflicted with a -2 penalty to their AC against the next attack by your allies. This penalty only applies to the " +
                    "first attack roll and is removed after 1 round.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = AidAnotherOffenceBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = 0,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            IsNotDispelable = true
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionCasterHasFact() {
                                        Not = false,
                                        m_Fact = OracleRevelationPerfectAid.ToReference<BlueprintUnitFactReference>()
                                    },
                                    new ContextConditionCasterHasFact() {
                                        Not = false,
                                        m_Fact = AsavirCamaraderieFeature.ToReference<BlueprintUnitFactReference>()
                                    },
                                    new ContextConditionCasterHasFact() {
                                        Not = false,
                                        m_Fact = CommunityBlessingMinorBuff.ToReference<BlueprintUnitFactReference>()
                                    },
                                    new ContextConditionCasterHasFact() {
                                        Not = false,
                                        m_Fact = GoldenLegionnarireImprovedAidFeature.ToReference<BlueprintUnitFactReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = PerfectAidAnotherOffenceBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = 0,
                                        DiceCountValue = 0,
                                        BonusValue = 1
                                    },
                                    IsNotDispelable = true
                                }),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = SwiftAidAnotherOffenceBuff.ToReference<BlueprintBuffReference>()
                        });
                });
                bp.AddComponent<AbilityTargetIsAlly>(c => {
                    c.Not = true;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = AidAnotherOffenceIcon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.m_Parent = AidAnotherAbility.ToReference<BlueprintAbilityReference>();
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = Helpers.CreateString("AidAnotherOffenceAbility.Duration", "1 round or until used");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            AidAnotherAbility.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    AidAnotherDefenceAbility.ToReference<BlueprintAbilityReference>(),
                    AidAnotherOffenceAbility.ToReference<BlueprintAbilityReference>()
                };
            });
            var SkillAbilities = Resources.GetBlueprint<BlueprintFeature>("e4c33ff99d638744686112e2a5f49856").GetComponent<AddFacts>();
             
            
            var SwiftAidAnotherAbility = Helpers.CreateBlueprint<BlueprintAbility>("SwiftAidAnotherAbility", bp => {
                bp.SetName("Swift Aid Another");
                bp.SetDescription("In melee combat, you can help your allies attack or defend by distracting or interfering with an opponent. If you’re in position to make a melee attack on an opponent you can attempt to " +
                    "aid your allies against that enemy as a swift action. The distracted enemy is inflicted with a -1 penalty to either their AC against the next attack by your allies, or a -1 penalty on their next " +
                    "attack roll against your allies. This penalty only applies to the first attack roll and is removed after 1 round. \nThis penalty does not stack with the standard aid another penalty.");
                bp.m_Icon = SwiftAidAnotherIcon;
                //Variants added later
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SwiftAidAnotherDefenceAbility = Helpers.CreateBlueprint<BlueprintAbility>("SwiftAidAnotherDefenceAbility", bp => {
                bp.SetName("Swift Aid Another - Distracted Attacks");
                bp.SetDescription("In melee combat, you can help your allies attack or defend by distracting or interfering with an opponent. If you’re in position to make a melee attack on an opponent you can attempt to " +
                    "aid your allies against that enemy as a swift action. The distracted enemy is inflicted with a -1 penalty on their next attack roll against your allies. This penalty only applies to the first attack " +
                    "roll and is removed after 1 round. \nThis penalty does not stack with the standard aid another penalty.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        Not = true,
                                        m_Fact = AidAnotherDefenceBuff.ToReference<BlueprintUnitFactReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SwiftAidAnotherDefenceBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = 0,
                                        DiceCountValue = 0,
                                        BonusValue = 1
                                    },
                                    IsNotDispelable = true
                                }),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionCasterHasFact() {
                                        Not = false,
                                        m_Fact = OracleRevelationPerfectAid.ToReference<BlueprintUnitFactReference>()
                                    },
                                    new ContextConditionCasterHasFact() {
                                        Not = false,
                                        m_Fact = AsavirCamaraderieFeature.ToReference<BlueprintUnitFactReference>()
                                    },
                                    new ContextConditionCasterHasFact() {
                                        Not = false,
                                        m_Fact = CommunityBlessingMinorBuff.ToReference<BlueprintUnitFactReference>()
                                    },
                                    new ContextConditionCasterHasFact() {
                                        Not = false,
                                        m_Fact = GoldenLegionnarireImprovedAidFeature.ToReference<BlueprintUnitFactReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = PerfectAidAnotherDefenceBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = 0,
                                        DiceCountValue = 0,
                                        BonusValue = 1
                                    },
                                    IsNotDispelable = true
                                }),
                            IfFalse = Helpers.CreateActionList()
                        });
                });
                bp.AddComponent<AbilityTargetIsAlly>(c => {
                    c.Not = true;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = SwiftAidAnotherDefenceIcon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.m_Parent = SwiftAidAnotherAbility.ToReference<BlueprintAbilityReference>();
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = Helpers.CreateString("SwiftAidAnotherDefenceAbility.Duration", "1 round or until used");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SwiftAidAnotherOffenceAbility = Helpers.CreateBlueprint<BlueprintAbility>("SwiftAidAnotherOffenceAbility", bp => {
                bp.SetName("Swift Aid Another - Distracted Defending");
                bp.SetDescription("In melee combat, you can help your allies attack or defend by distracting or interfering with an opponent. If you’re in position to make a melee attack on an opponent you can attempt to " +
                    "aid your allies against that enemy as a swift action. The distracted enemy is inflicted with a -1 penalty to either their AC against the next attack by your allies. This penalty only applies to the " +
                    "first attack roll and is removed after 1 round. \nThis penalty does not stack with the standard aid another penalty.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        Not = true,
                                        m_Fact = AidAnotherOffenceBuff.ToReference<BlueprintUnitFactReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SwiftAidAnotherOffenceBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = 0,
                                        DiceCountValue = 0,
                                        BonusValue = 1
                                    },
                                    IsNotDispelable = true
                                }),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionCasterHasFact() {
                                        Not = false,
                                        m_Fact = OracleRevelationPerfectAid.ToReference<BlueprintUnitFactReference>()
                                    },
                                    new ContextConditionCasterHasFact() {
                                        Not = false,
                                        m_Fact = AsavirCamaraderieFeature.ToReference<BlueprintUnitFactReference>()
                                    },
                                    new ContextConditionCasterHasFact() {
                                        Not = false,
                                        m_Fact = CommunityBlessingMinorBuff.ToReference<BlueprintUnitFactReference>()
                                    },
                                    new ContextConditionCasterHasFact() {
                                        Not = false,
                                        m_Fact = GoldenLegionnarireImprovedAidFeature.ToReference<BlueprintUnitFactReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = PerfectAidAnotherOffenceBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = 0,
                                        DiceCountValue = 0,
                                        BonusValue = 1
                                    },
                                    IsNotDispelable = true
                                }),
                            IfFalse = Helpers.CreateActionList()
                        });
                });
                bp.AddComponent<AbilityTargetIsAlly>(c => {
                    c.Not = true;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = SwiftAidAnotherOffenceIcon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.m_Parent = SwiftAidAnotherAbility.ToReference<BlueprintAbilityReference>();
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = Helpers.CreateString("SwiftAidAnotherOffenceAbility.Duration", "1 round or until used");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            SwiftAidAnotherAbility.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    SwiftAidAnotherDefenceAbility.ToReference<BlueprintAbilityReference>(),
                    SwiftAidAnotherOffenceAbility.ToReference<BlueprintAbilityReference>()
                };
            });
            var SwiftAidAnotherFeature = Helpers.CreateBlueprint<BlueprintFeature>("SwiftAidAnotherFeature", bp => {
                bp.SetName("Swift Aid");
                bp.SetDescription("With a quick but harmless swipe, you can aid an ally’s assault. \nAs a swift action, you can attempt the aid another action, granting your enemy either a -1 penalty on their next attack roll or a -1 bonus to their AC. This ability still confers any " +
                    "extra effects your standard aid another ability would.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { SwiftAidAnotherAbility.ToReference<BlueprintUnitFactReference>()};
                });
                bp.AddComponent<PrerequisiteStatValue>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.Stat = StatType.Intelligence;
                    c.Value = 13;
                });
                bp.AddComponent<PrerequisiteStatValue>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.Stat = StatType.BaseAttackBonus;
                    c.Value = 6;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        CombatExpertise.ToReference<BlueprintFeatureReference>(),
                        OracleRevelationPerfectAid.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { 
                    FeatureGroup.Feat,
                    FeatureGroup.CombatFeat
                };
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            //Excellent Aid Added to Halfling Opportunist in ModSupport
            var HalflingOpportunistExcellentAidFeature = Helpers.CreateBlueprint<BlueprintFeature>("HalflingOpportunistExcellentAidFeature", bp => {
                bp.SetName("Excellent Aid");
                bp.SetDescription("A halfling opportunist has an amazing talent for getting the most out of those who assist her. Increase the bonus she gets from " +
                    "aid another by +1. This increases by another +1 at 3rd level and again at 5th level. This increase does not apply to when she uses aid another " +
                    "to help others, only when others aid her.");
                //ContextRankConfig added in ModSupport
                bp.AddComponent<AttackBonusAgainstFactOwner>(c => {
                    c.m_CheckedFact = AidAnotherOffenceBuff.ToReference<BlueprintUnitFactReference>();
                    c.AttackBonus = 0;
                    c.Bonus = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Descriptor = ModifierDescriptor.None;
                    c.Not = false;
                });
                bp.AddComponent<AttackBonusAgainstFactOwner>(c => {
                    c.m_CheckedFact = SwiftAidAnotherOffenceBuff.ToReference<BlueprintUnitFactReference>();
                    c.AttackBonus = 0;
                    c.Bonus = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.Descriptor = ModifierDescriptor.None;
                    c.Not = false;
                });
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
            });


            if (ModSettings.AddedContent.Miscellaneous.IsDisabled("Aid Another")) { return; }
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationPerfectAid.ToReference<BlueprintFeatureReference>());
            SkillAbilities.m_Facts = SkillAbilities.m_Facts.AppendToArray(AidAnotherAbility.ToReference<BlueprintUnitFactReference>());
            FeatTools.AddAsFeat(SwiftAidAnotherFeature);











        }
    }
}
