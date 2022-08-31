using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Spells {
    internal class HydraulicPush {
        public static void AddHydraulicPush() {

            var Kinetic_WaterBlast00_Projectile = Resources.GetBlueprint<BlueprintProjectile>("06e268d6a2b5a3a438c2dd52d68bfef6");
            var WaterBlastAbility = Resources.GetBlueprint<BlueprintAbility>("e3f41966c2d662a4e9582a0497621c46");

            var HydraulicPushAbility = Helpers.CreateBlueprint<BlueprintAbility>("HydraulicPushAbility", bp => {
                bp.SetName("Hydraulic Push");
                bp.SetDescription("You call forth a quick blast of water that knocks over and soaks one creature or square. You can use this blast of water to make a bull rush " +
                    "against any one creature or object. Your CMB for this bull rush is equal to your caster level plus your Intelligence, Wisdom, or Charisma modifier, whichever " +
                    "is highest. This bull rush does not provoke an attack of opportunity.");
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        Kinetic_WaterBlast00_Projectile.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Simple;
                    c.m_Length = new Feet() { m_Value = 0 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionCombatManeuver() {
                            Type = CombatManeuver.BullRush,
                            ReplaceStat = false,
                            NewStat = StatType.Wisdom,
                            UseCasterLevelAsBaseAttack = true,
                            UseBestMentalStat =true,
                            OnSuccess = Helpers.CreateActionList()
                        }
                        );
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Evocation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Reflex;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = WaterBlastAbility.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            HydraulicPushAbility.AddToSpellList(SpellTools.SpellList.BloodragerSpellList, 1);
            HydraulicPushAbility.AddToSpellList(SpellTools.SpellList.DruidSpellList, 1);
            HydraulicPushAbility.AddToSpellList(SpellTools.SpellList.HunterSpelllist, 1);
            HydraulicPushAbility.AddToSpellList(SpellTools.SpellList.MagusSpellList, 1);
            HydraulicPushAbility.AddToSpellList(SpellTools.SpellList.ShamanSpelllist, 1);
            HydraulicPushAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 1);
        }
    }
}
