using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Spells {
    internal class PlantShape {
        public static void AddPlantShape() {

            var PlantShapeIcon = AssetLoader.LoadInternal("Skills", "Icon_PlantShape.jpg");
            var Icon_ScrollOfPlantShape = AssetLoader.LoadInternal("Items", "Icon_ScrollOfPlantShape.png");
            var Enhancement1 = Resources.GetBlueprint<BlueprintWeaponEnchantment>("d42fc23b92c640846ac137dc26e000d4");
            var LeadBladesBuff = Resources.GetBlueprint<BlueprintBuff>("91f43163db96f8941a41e2b584a97514");

            var PlantShapeBuff = Helpers.CreateBuff("PlantShapeBuff", bp => {
                bp.SetName("Plant Shape");
                bp.SetDescription("");
                bp.m_Icon = PlantShapeIcon;
                bp.AddComponent<IncreaseDiceSizeOnAttack>(c => {
                    c.CheckWeaponCategories = true;
                    c.Categories = new WeaponCategory[] { WeaponCategory.Quarterstaff | WeaponCategory.Club };
                    c.CheckWeaponSubCategories = false;
                    c.SubCategories = new WeaponSubCategory[0];
                    c.UseContextBonus = true;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.AdditionalSize = 0;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterBuffRank;
                    c.m_Buff = LeadBladesBuff.ToReference<BlueprintBuffReference>();
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 0, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 1, ProgressionValue = 1 }
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var PlantShapeAbility = Helpers.CreateBlueprint<BlueprintAbility>("PlantShapeAbility", bp => {
                bp.SetName("Plant Shape");
                bp.SetDescription("");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new EnhanceWeapon() {
                            m_Enchantment = new BlueprintItemEnchantmentReference[] { Enhancement1.ToReference<BlueprintItemEnchantmentReference>() },
                            EnchantmentType = EnhanceWeapon.EnchantmentApplyType.MagicWeapon,
                            Greater = false,
                            UseSecondaryHand = false,
                            EnchantLevel = 1,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = PlantShapeBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.EnchantWeapon;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic =  Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("PlantShapeAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var PlantShapeScroll = ItemTools.CreateScroll("ScrollOfPlantShape", Icon_ScrollOfPlantShape, PlantShapeAbility, 1, 1);
            VenderTools.AddScrollToLeveledVenders(PlantShapeScroll);
            PlantShapeAbility.AddToSpellList(SpellTools.SpellList.DruidSpellList, 1);
        }
    }
}
