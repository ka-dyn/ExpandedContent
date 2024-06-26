﻿using ExpandedContent.Utilities;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using ExpandedContent.Extensions;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Enums;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.Utility;
using Kingmaker.ResourceLinks;
using UnityEngine;

namespace ExpandedContent.Tweaks.Spells {
    internal class DustOfTwilight {
        public static void AddDustOfTwilight() {

            var DustOfTwilightIcon = AssetLoader.LoadInternal("Skills", "Icon_DustOfTwilight.jpg");
            var Icon_ScrollOfDustOfTwilight = AssetLoader.LoadInternal("Items", "Icon_ScrollOfDustOfTwilight.png");
            var FatiguedBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("e6f2fc5d73d88064583cb828801212f4");
            var GlitterdustBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("03457e519288aad4085eae91918a76bf");

            var DustOfTwilightAbility = Helpers.CreateBlueprint<BlueprintAbility>("DustOfTwilightAbility", bp => {
                bp.SetName("Dust of Twilight");
                bp.SetDescription("A shower of iridescent black particles clings in the air, removing all forms of glitterdust. \nCreatures in the area must make a Fortitude save or become fatigued.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSavingThrow() {
                            Type = SavingThrowType.Fortitude,
                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                            HasCustomDC = false,
                            CustomDC = new ContextValue(),
                            Actions = Helpers.CreateActionList(
                                new ContextActionConditionalSaved() {
                                    Succeed = new ActionList(),
                                    Failed = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = FatiguedBuff,
                                            Permanent = true,
                                            DurationValue = new ContextDurationValue() {
                                                m_IsExtendable = true,
                                                DiceCountValue = new ContextValue(),
                                                BonusValue = new ContextValue()
                                            },
                                            IsFromSpell = true,
                                        }
                                    ),
                                }
                            ),
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = GlitterdustBuff                            
                        });
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 10.Feet();
                    c.m_TargetType = TargetType.Any;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker();
                    c.m_SpreadSpeed = 0.Feet();
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "a765d9070704e4c4591eba41211d0987" };
                    c.Time = AbilitySpawnFxTime.OnPrecastFinished;
                    c.Anchor = AbilitySpawnFxAnchor.ClickedTarget;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.AOE;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = DustOfTwilightIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = Helpers.CreateString("DustOfTwilightAbility.SavingThrow", "Fortitude negates (fatigue only)"); 
            });
            var DustOfTwilightAbilitySpawnFx = DustOfTwilightAbility.GetComponent<AbilitySpawnFx>();
            DustOfTwilightAbilitySpawnFx.PrefabLink = DustOfTwilightAbilitySpawnFx.PrefabLink.CreateDynamicProxy(pfl => {
                Main.Log($"Editing: {pfl}");
                pfl.name = "DustOfTwilight_10feetAoE";
                //Main.Log($"{FxDebug.DumpGameObject(pfl.gameObject)}");
                var pointlightnormal = pfl.transform.Find("Root/Point Light").GetComponent<Light>();
                pointlightnormal.color = new Color(0.1448f, 0f, 0.1132f, 1f);
                var pointlightAnimated = pfl.transform.Find("Root/Point Light").GetComponent<AnimatedLight>();
                pointlightAnimated.m_Color = new Color(0.1448f, 0f, 0.1132f, 1f);
                var Sparks_Start = pfl.transform.Find("Root/Sparks_Start").GetComponent<ParticleSystem>();
                Sparks_Start.startColor = new Color(0.0566f, 0f, 0.2144f, 1f);
                var StartSmoke_Particles = pfl.transform.Find("Root/StartSmoke_Particles").GetComponent<ParticleSystem>();
                StartSmoke_Particles.startColor = new Color(0.0566f, 0f, 0.2144f, 1f);
                var Smoke_Particles_Loop = pfl.transform.Find("Root/Smoke_Particles_Loop").GetComponent<ParticleSystem>();
                Smoke_Particles_Loop.startColor = new Color(0.0566f, 0f, 0.2144f, 1f);
                var Flash_Sparks = pfl.transform.Find("Root/Flash_Sparks").GetComponent<ParticleSystem>();
                Flash_Sparks.startColor = new Color(0.0566f, 0f, 0.2144f, 1f);
                var Sparks_Loop = pfl.transform.Find("Root/Sparks_Loop").GetComponent<ParticleSystem>();
                Sparks_Loop.startColor = new Color(0.0566f, 0f, 0.2144f, 1f);
                var Golden_Dust = pfl.transform.Find("Root/Golden_Dust").GetComponent<ParticleSystem>();
                Golden_Dust.startColor = new Color(0.0566f, 0f, 0.2144f, 1f);
                var Sparks_Geom_Loop = pfl.transform.Find("Root/Sparks_Geom_Loop").GetComponent<ParticleSystem>();
                Sparks_Geom_Loop.startColor = new Color(0.0566f, 0f, 0.2144f, 1f);
                var Sparks_Geom_Loop2 = pfl.transform.Find("Root/Sparks_Geom_Loop (2)").GetComponent<ParticleSystem>();
                Sparks_Geom_Loop2.startColor = new Color(0.0566f, 0f, 0.2144f, 1f);

            });

            var DustOfTwilightScroll = ItemTools.CreateScroll("ScrollOfDustOfTwilight", Icon_ScrollOfDustOfTwilight, DustOfTwilightAbility, 2, 3);
            VenderTools.AddScrollToLeveledVenders(DustOfTwilightScroll);
            DustOfTwilightAbility.AddToSpellList(SpellTools.SpellList.BardSpellList, 2);
            DustOfTwilightAbility.AddToSpellList(SpellTools.SpellList.BloodragerSpellList, 2);
            DustOfTwilightAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 2);
            DustOfTwilightAbility.AddToSpellList(SpellTools.SpellList.LichWizardSpelllist, 2);
            DustOfTwilightAbility.AddToSpellList(SpellTools.SpellList.AeonSpellList, 2);
        }
    }
}
