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

namespace ExpandedContent.Tweaks.Spells {
    internal class Shillelagh {
        public static void AddShillelagh() {

            var ShillelaghIcon = AssetLoader.LoadInternal("Skills", "Icon_Shillelagh.jpg");
            var Icon_ScrollOfShillelagh = AssetLoader.LoadInternal("Items", "Icon_ScrollOfShillelagh.png");
            var Enhancement1 = Resources.GetBlueprint<BlueprintWeaponEnchantment>("d42fc23b92c640846ac137dc26e000d4");
            var LeadBladesBuff = Resources.GetBlueprint<BlueprintBuff>("91f43163db96f8941a41e2b584a97514");

            var ShillelaghBuff = Helpers.CreateBuff("ShillelaghBuff", bp => {
                bp.SetName("Shillelagh");
                bp.SetDescription("Your own non-magical club or quarterstaff becomes a weapon with a +1 enhancement bonus on attack and damage rolls. A quarterstaff gains this enhancement for both " +
                    "ends of the weapon. It deals damage as if it were two size categories larger (a Small club or quarterstaff so transmuted deals 1d8 points of damage, a Medium 2d6, and a Large 3d6), " +
                    "+1 for its enhancement bonus. These effects only occur when the weapon is wielded by you. If you do not wield it, the weapon behaves as if unaffected by this spell. A magical weapon " +
                    "still gains the damage via size increase, but not the additional +1 bonuses.");
                bp.m_Icon = ShillelaghIcon;
                bp.AddComponent<IncreaseDiceSizeOnAttack>(c => {
                    c.CheckWeaponCategories = true;
                    c.Categories = new WeaponCategory[] { WeaponCategory.Quarterstaff, WeaponCategory.Club, WeaponCategory.Greatclub };
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
            var ShillelaghAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShillelaghAbility", bp => {
                bp.SetName("Shillelagh");
                bp.SetDescription("Your own non-magical club or quarterstaff becomes a weapon with a +1 enhancement bonus on attack and damage rolls. A quarterstaff gains this enhancement for both " +
                    "ends of the weapon. It deals damage as if it were two size categories larger (a Small club or quarterstaff so transmuted deals 1d8 points of damage, a Medium 2d6, and a Large 3d6), " +
                    "+1 for its enhancement bonus. These effects only occur when the weapon is wielded by you. If you do not wield it, the weapon behaves as if unaffected by this spell. A magical weapon " +
                    "still gains the damage via size increase, but not the additional +1 bonuses.");
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
                            m_Buff = ShillelaghBuff.ToReference<BlueprintBuffReference>(),
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
                bp.m_Icon = ShillelaghIcon;
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
                bp.LocalizedDuration = Helpers.CreateString("ShillelaghAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShillelaghScroll = ItemTools.CreateScroll("ScrollOfShillelagh", Icon_ScrollOfShillelagh, ShillelaghAbility, 1, 1);
            VenderTools.AddScrollToLeveledVenders(ShillelaghScroll);
            ShillelaghAbility.AddToSpellList(SpellTools.SpellList.DruidSpellList, 1);
            ShillelaghAbility.AddToSpellList(SpellTools.SpellList.HunterSpelllist, 1);
        }
    }
}
