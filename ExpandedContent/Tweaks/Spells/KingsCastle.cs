using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Visual.Animation.Kingmaker.Actions;

namespace ExpandedContent.Tweaks.Spells {
    internal class KingsCastle {
        public static void AddKingsCastle() {

            var EmergencySwapAbility = Resources.GetBlueprint<BlueprintAbility>("b50ca9b5d6292fb42b8eab8e5d64842d");
            var DimensionDoorAbility = Resources.GetBlueprint<BlueprintAbility>("5bdc37e4acfa209408334326076a43bc");
            var DimensionDoorScroll = Resources.GetBlueprint<BlueprintItemEquipmentUsable>("a7f61c7d07a8d9945a891c9a8c75d0cb");

           

            var KingsCastleAbility = Helpers.CreateBlueprint<BlueprintAbility>("KingsCastleAbility", bp => {
                bp.SetName("King's Castle");
                bp.SetDescription("When you cast this spell, choose a single ally. You teleport to your ally’s space while your ally teleports to your former space.");
                bp.AddComponent(EmergencySwapAbility.GetComponent<AbilityCustomDimensionDoorSwap>());
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = DimensionDoorAbility.Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.CompletelyNormal | Metamagic.Quicken;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var KingsCastleScroll = ItemTools.CreateScroll("ScrollOfKingsCastle", DimensionDoorScroll.Icon, KingsCastleAbility, 4, 7);
            VenderTools.AddScrollToLeveledVenders(KingsCastleScroll);
            KingsCastleAbility.AddToSpellList(SpellTools.SpellList.PaladinSpellList, 4);

        }
    }
}
