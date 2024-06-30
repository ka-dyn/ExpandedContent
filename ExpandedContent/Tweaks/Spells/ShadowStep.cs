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
using Kingmaker.ResourceLinks;

namespace ExpandedContent.Tweaks.Spells {
    internal class ShadowStep {
        public static void AddShadowStep() {//Untested

            var ShadowStepIcon = AssetLoader.LoadInternal("Skills", "Icon_ShadowStep.jpg");
            var Icon_ScrollOfShadowStep = AssetLoader.LoadInternal("Items", "Icon_ScrollOfShadowStep.png");

            var MountedBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("b2d13e8f3bb0f1d4c891d71b4d983cf7");
            var ShadowProjectile = Resources.GetBlueprintReference<BlueprintProjectileReference>("f8daba62ae5f454aae7bcd280d924e74");


            var ShadowStepAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShadowStepAbility", bp => {
                bp.SetName("Shadow Step");
                bp.SetDescription("You enter a shadow or area of darkness, which transports you along a coiling path of shadowstuff to a units shadow within range.");
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            ToCaster = true,
                            m_Buff = MountedBuff
                        }
                        );
                });
                bp.AddComponent<AbilityCustomTeleportation>(c => {
                    c.m_Projectile = ShadowProjectile;
                    c.DisappearFx = new PrefabLink() { AssetId = "f1f41fef03cb5734e95db1342f0c605e" };
                    c.DisappearDuration = 0.25f;
                    c.AppearFx = new PrefabLink();
                    c.AppearDuration = 0;
                    c.AlongPath = false;
                    c.AlongPathDistanceMuliplier = 1;

                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Illusion;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = ShadowStepIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.CompletelyNormal | Metamagic.Quicken| Metamagic.Reach | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShadowStepScroll = ItemTools.CreateScroll("ScrollOfShadowStep", Icon_ScrollOfShadowStep, ShadowStepAbility, 3, 5);
            VenderTools.AddScrollToLeveledVenders(ShadowStepScroll);
            ShadowStepAbility.AddToSpellList(SpellTools.SpellList.BardSpellList, 3);
            ShadowStepAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 3);
            ShadowStepAbility.AddToSpellList(SpellTools.SpellList.WitchSpellList, 3);

        }
    }
}
