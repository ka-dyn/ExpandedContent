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
    internal class EntropicShield {
        public static void AddEntropicShield() {
            var EntropicShieldIcon = AssetLoader.LoadInternal("Skills", "Icon_EntropicShield.jpg");
            var Icon_ScrollOfEntropicShield = AssetLoader.LoadInternal("Items", "Icon_ScrollOfEntropicShield.png");


            var EntropicShieldBuff = Helpers.CreateBuff("EntropicShieldBuff", bp => {
                bp.SetName("Entropic Shield");
                bp.SetDescription("A magical field appears around you. This field deflects incoming arrows, rays, and other ranged attacks. Each ranged attack directed at you for which the " +
                    "attacker must make an attack roll has a 20% miss chance (similar to the effects of concealment). Other attacks that simply work at a distance are not affected.");
                bp.m_Icon = EntropicShieldIcon;
                bp.AddComponent<AddConcealment>(c => {
                    c.Descriptor = ConcealmentDescriptor.Blur;
                    c.Concealment = Concealment.Partial;
                    c.CheckWeaponRangeType = true;
                    c.RangeType = WeaponRangeType.Ranged;
                    c.CheckDistance = false;
                    c.DistanceGreater = 0.Feet();
                    c.OnlyForAttacks = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });

            var EntropicShieldAbility = Helpers.CreateBlueprint<BlueprintAbility>("EntropicShieldAbility", bp => {
                bp.SetName("Entropic Shield");
                bp.SetDescription("A magical field appears around you. This field deflects incoming arrows, rays, and other ranged attacks. Each ranged attack directed at you for which the " +
                    "attacker must make an attack roll has a 20% miss chance (similar to the effects of concealment). Other attacks that simply work at a distance are not affected.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = EntropicShieldBuff.ToReference<BlueprintBuffReference>(),
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
                bp.m_Icon = EntropicShieldIcon;
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
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = Helpers.CreateString("EntropicShieldAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var EntropicShieldScroll = ItemTools.CreateScroll("ScrollOfEntropicShield", Icon_ScrollOfEntropicShield, EntropicShieldAbility, 1, 1);
            VenderTools.AddScrollToLeveledVenders(EntropicShieldScroll);
            EntropicShieldAbility.AddToSpellList(SpellTools.SpellList.ClericSpellList, 1);
            EntropicShieldAbility.AddToSpellList(SpellTools.SpellList.WarpriestSpelllist, 1);
        }
    }
}
