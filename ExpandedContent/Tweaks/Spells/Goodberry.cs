using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Visual.Animation.Kingmaker.Actions;

namespace ExpandedContent.Tweaks.Spells {
    internal class Goodberry {
        public static void AddGoodberry() {
            var Kameberry = Resources.GetBlueprint<BlueprintItem>("9673fb63c820b5345ad07c32219edfde");
            var DustShardItem = Resources.GetBlueprint<BlueprintItem>("b352a456952e4d50b1b2069d7b8debd0");
            var PotionOfCureLightWounds = Resources.GetBlueprint<BlueprintItemEquipmentUsable>("d52566ae8cbe8dc4dae977ef51c27d91");
            var Sickened = Resources.GetBlueprint<BlueprintBuff>("4e42460798665fd4cb9173ffa7ada323");
            var GooberryIcon = AssetLoader.LoadInternal("Skills", "Icon_Goodberry.jpg");
            var Icon_ScrollOfGoodberry = AssetLoader.LoadInternal("Items", "Icon_ScrollOfGoodberry.png");


            var GoodberryCooldown = Helpers.CreateBuff("GoodberryCooldown", bp => {
                bp.m_AllowNonContextActions = false;
                bp.SetName("");
                bp.SetDescription("");
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });

            var GoodberryItemAbility = Helpers.CreateBlueprint<BlueprintAbility>("GoodberryItemAbility", bp => {
                bp.SetName("Goodberry");
                bp.SetDescription("When eaten goodberries heal 2d4 point of damage, however if they are eaten more than once every 24 hours they can make the creature that ate them sick for a short time.");
                bp.m_Icon = Kameberry.m_Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionCasterHasFact() {
                                        Not = false,
                                        m_Fact = GoodberryCooldown.ToReference<BlueprintUnitFactReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = Sickened.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = 0,
                                        DiceCountValue = 0,
                                        BonusValue = 1
                                    }
                                }),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionHealTarget() {
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.D4,
                                        DiceCountValue = 2,
                                        BonusValue = 0
                                    }
                                },
                                new ContextActionApplyBuff() {
                                    m_Buff = GoodberryCooldown.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Days,
                                        DiceType = 0,
                                        DiceCountValue = 0,
                                        BonusValue = 1
                                    },
                                    IsNotDispelable = true
                                }
                                )
                        });
                });
                bp.Type = AbilityType.Extraordinary;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var GoodberryItem = Helpers.CreateBlueprint<BlueprintItemEquipmentUsable>("GoodberryItem", bp => {
                bp.SetName("Bunch of Goodberries");
                bp.SetDescription("When eaten goodberries heal 2d4 point of damage, however if they are eaten more than once every 24 hours they can make the creature that ate them sick for a short time.");
                bp.SetFlavorText("A small bunch of magic berries made from plant matter with druidic magic.");
                bp.m_Icon = Kameberry.m_Icon;
                bp.m_ForceStackable = true; //?
                bp.m_Cost = 1;
                bp.m_Weight = 1;
                bp.m_IsNotable = false;
                bp.m_Destructible = true;
                bp.m_ShardItem = DustShardItem.ToReference<BlueprintItemReference>();
                bp.m_MiscellaneousType = BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = PotionOfCureLightWounds.m_InventoryPutSound;
                bp.m_InventoryTakeSound = PotionOfCureLightWounds.m_InventoryTakeSound;
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = new TrashLootType[] { TrashLootType.Scrolls_RE };
                bp.CR = 0;
                bp.m_Ability = GoodberryItemAbility.ToReference<BlueprintAbilityReference>();
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = false;
                bp.CasterLevel = 1;
                bp.SpellLevel = 1;
                bp.DC = 0;
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.IsNonRemovable = false;
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.Type = UsableItemType.Other;
                bp.m_InventoryEquipSound = PotionOfCureLightWounds.m_InventoryEquipSound;
                bp.m_BeltItemPrefab = new PrefabLink();
                bp.m_Enchantments = new BlueprintEquipmentEnchantmentReference[0];
            });

            var GoodberryAbility = Helpers.CreateBlueprint<BlueprintAbility>("GoodberryAbility", bp => {
                bp.SetName("Goodberry");
                bp.SetDescription("Casting Goodberry creates a Bunch of Goodberries and stores them in the party inventeory. When eaten the goodberries heal " +
                    "2d4 point of damage, however if they are eaten more than once a day they can make the creature that ate them sick for a short time.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new AddItemToPlayer() {
                            m_ItemToGive = GoodberryItem.ToReference<BlueprintItemReference>(),
                            Silent = false,
                            Quantity = 1,
                            Identify = true,
                            Equip = false 
                        });
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = GooberryIcon;
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
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var GoodberryScroll = ItemTools.CreateScroll("ScrollOfGoodberry", Icon_ScrollOfGoodberry, GoodberryAbility, 1, 1);
            VenderTools.AddScrollToLeveledVenders(GoodberryScroll);
            GoodberryAbility.AddToSpellList(SpellTools.SpellList.DruidSpellList, 1);
            GoodberryAbility.AddToSpellList(SpellTools.SpellList.HunterSpelllist, 1);
            GoodberryAbility.AddToSpellList(SpellTools.SpellList.ShamanSpelllist, 1);
            GoodberryAbility.AddToSpellList(SpellTools.SpellList.AzataMythicSpelllist, 1);
        }
    }
}
