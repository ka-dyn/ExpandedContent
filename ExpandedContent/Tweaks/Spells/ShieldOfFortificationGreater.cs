using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Spells {
    internal class ShieldOfFortificationGreater {
        public static void AddShieldOfFortificationGreater() {
            var ShieldOfFortificationGreaterIcon = AssetLoader.LoadInternal("Skills", "Icon_ShieldOfFortificationGreater.jpg");
            var Icon_ScrollOfShieldOfFortificationGreater = AssetLoader.LoadInternal("Items", "Icon_ScrollOfShieldOfFortificationGreater.png");


            var ShieldOfFortificationGreaterBuff = Helpers.CreateBuff("ShieldOfFortificationGreaterBuff", bp => {
                bp.SetName("Greater Shield of Fortification");
                bp.SetDescription("This spell functions as shield of fortification, except there is a 50% chance that the critical hit or sneak attack is negated and damage is instead be rolled normally, rather than 25%. " +
                    "\nShield of Fortification: You create a magical barrier that protects a target’s vital areas. When the target is struck by a critical hit or a sneak attack, there is a 25% chance that the critical " +
                    "hit or sneak attack is negated and damage is instead rolled normally. This benefit does not stack with other effects that can turn critical hits or sneak attacks into normal attacks, such " +
                    "as the fortification armor special ability.");
                bp.m_Icon = ShieldOfFortificationGreaterIcon;
                bp.AddComponent<AddFortification>(c => {
                    c.UseContextValue = false;
                    c.Bonus = 50;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });

            var ShieldOfFortificationGreaterAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShieldOfFortificationGreaterAbility", bp => {
                bp.SetName("Greater Shield of Fortification");
                bp.SetDescription("This spell functions as shield of fortification, except there is a 50% chance that the critical hit or sneak attack is negated and damage is instead be rolled normally, rather than 25%. " +
                    "\nShield of Fortification: You create a magical barrier that protects a target’s vital areas. When the target is struck by a critical hit or a sneak attack, there is a 25% chance that the critical " +
                    "hit or sneak attack is negated and damage is instead rolled normally. This benefit does not stack with other effects that can turn critical hits or sneak attacks into normal attacks, such " +
                    "as the fortification armor special ability.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ShieldOfFortificationGreaterBuff.ToReference<BlueprintBuffReference>(),
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
                    c.School = SpellSchool.Abjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "f447e243ab2c1da4c851c019c3196526" }; //Mage Armor FX
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.SelectedTarget;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.m_Icon = ShieldOfFortificationGreaterIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("ShieldOfFortificationGreaterAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShieldOfFortificationGreaterScroll = ItemTools.CreateScroll("ScrollOfShieldOfFortificationGreater", Icon_ScrollOfShieldOfFortificationGreater, ShieldOfFortificationGreaterAbility, 4, 7);
            VenderTools.AddScrollToLeveledVenders(ShieldOfFortificationGreaterScroll);
            ShieldOfFortificationGreaterAbility.AddToSpellList(SpellTools.SpellList.ClericSpellList, 4);
            ShieldOfFortificationGreaterAbility.AddToSpellList(SpellTools.SpellList.InquisitorSpellList, 3);
            ShieldOfFortificationGreaterAbility.AddToSpellList(SpellTools.SpellList.PaladinSpellList, 3);
        }
    }
}
