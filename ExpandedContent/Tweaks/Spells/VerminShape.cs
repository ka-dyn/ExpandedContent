using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.Root.Fx;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Visual.Sound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Spells {
    internal class VerminShape {
        public static void AddVerminShape() {

            var VerminShapeIIcon = AssetLoader.LoadInternal("Skills", "Icon_VerminShapeI.jpg");
            //var VerminShapeIIIcon = AssetLoader.LoadInternal("Skills", "Icon_VerminShapeII.jpg");
            var BeastShapeIBuffPolymorph = Resources.GetBlueprint<BlueprintBuff>("00d8fbe9cf61dc24298be8d95500c84b").GetComponent<Polymorph>();
            var BeastShapeIBuffSuppressBuffs = Resources.GetBlueprint<BlueprintBuff>("00d8fbe9cf61dc24298be8d95500c84b").GetComponents<SuppressBuffs>();
            var TurnBarkStandart = Resources.GetBlueprint<BlueprintAbility>("bd09b025ee2a82f46afab922c4decca9");
            var CentipedeBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("0efbb783c068b9d4aaf5dc98515ffdee");
            var FlyBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("5f2435439043da240ad7ba96179523cf");
            var SpiderBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("7d340f75a57c47d45b0e79200a6b5eac");
            var CentipedePoisonFeature = Resources.GetBlueprint<BlueprintFeature>("7edf8a604926e364e8c07fafa8c54001");
            var TripImmunity = Resources.GetBlueprint<BlueprintFeature>("c1b26f97b974aec469613f968439e7bb");
            var AirborneFeature = Resources.GetBlueprint<BlueprintFeature>("70cffb448c132fa409e49156d013b175");
            var SpiderWebImmunity = Resources.GetBlueprint<BlueprintFeature>("3051e7002c803fc47a11bcfa381b9fbd");
            var WebArea = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("fd323c05f76390749a8555b13156813d");
            var WebSpell = Resources.GetBlueprint<BlueprintAbility>("134cb6d492269aa4f8662700ef57449f");
            var InfestAbilityIcon = Resources.GetBlueprint<BlueprintAbility>("ddeb440cc43526241b09b3e1fe81da44").Icon;


            var Bite1d6 = Resources.GetBlueprint<BlueprintItemWeapon>("a000716f88c969c499a535dadcf09286");
            var Bite2d6 = Resources.GetBlueprint<BlueprintItemWeapon>("2abc1dc6172759c42971bd04b8c115cb");


            var VerminShapeWebAbilityCooldown = Helpers.CreateBuff("VerminShapeWebAbilityCooldown", bp => {
                bp.m_AllowNonContextActions = false;
                bp.SetName("Web Ability Cooldown");
                bp.SetDescription("After use you cannot use the spiders web ability for 1 minute.");
                bp.m_Icon = WebSpell.Icon;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });

            var VerminShapeWebAbility = Helpers.CreateBlueprint<BlueprintAbility>("VerminShapeWebAbility", bp => {
                bp.SetName("Spiders Web");
                bp.SetDescription("Web creates a many-layered mass of {g|Encyclopedia:Strength}strong{/g}, sticky strands. These strands trap those caught in them. Creatures caught within a web become grappled by " +
                    "the sticky fibers.\nAnyone in the effect's area when spider's web is cast must make a {g|Encyclopedia:Saving_Throw}Reflex save{/g}. If this save succeeds, the creature is inside the web but is " +
                    "otherwise unaffected. If the save fails, the creature gains the grappled condition, but can break free by making a {g|Encyclopedia:Combat_Maneuvers}combat maneuver{/g} {g|Encyclopedia:Check}check{/g}" +
                    ", {g|Encyclopedia:Athletics}Athletics check{/g}, or {g|Encyclopedia:Mobility}Mobility check{/g} as a {g|Encyclopedia:Standard_Actions}standard action{/g} against the {g|Encyclopedia:DC}DC{/g} of this " +
                    "ability. The entire area of the web is considered difficult terrain. Anyone moving through the webs must make a Reflex save each {g|Encyclopedia:Combat_Round}round{/g}. Creatures that fail lose their " +
                    "movement and become grappled in the first square of webbing that they enter. Spiders are immune to this ability. \nAfter use you cannot use this ability for 1 minute.");
                bp.m_Icon = WebSpell.Icon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Close;
                bp.CanTargetEnemies = false;
                bp.CanTargetPoint = true;
                bp.CanTargetFriends = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "1 minute");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "Reflex");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnAreaEffect() {
                            m_AreaEffect = WebArea.ToReference<BlueprintAbilityAreaEffectReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            }
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = VerminShapeWebAbilityCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Minutes,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 1,
                                    },
                                    IsNotDispelable = true
                                }
                                )
                            }
                        );
                });
                bp.AddComponent<ContextCalculateAbilityParams>(c => {
                    c.UseKineticistMainStat = false;
                    c.StatType = StatType.Constitution;
                    c.StatTypeFromCustomProperty = false;
                    c.ReplaceCasterLevel = false;
                    c.CasterLevel = 0;
                    c.ReplaceSpellLevel = false;
                    c.SpellLevel = 0;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { VerminShapeWebAbilityCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityAoERadius>(c => {
                    c.m_Radius = new Feet() { m_Value = 20 };
                    c.m_TargetType = TargetType.Any;
                    c.m_CanBeUsedInTacticalCombat = false;
                    c.m_DiameterInCells = 0;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Ground | SpellDescriptor.MovementImpairing;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Reflex;
                    c.AOEType = CraftAOE.AOE;
                    c.SpellType = CraftSpellType.Debuff;
                });
            });


            var VerminShapeICentipedeBuff = Helpers.CreateBuff("VerminShapeICentipedeBuff", bp => {
                bp.SetName("Vermin Shape (Centipede)");
                bp.SetDescription("You are in centipede form now. You have a +2 size bonus to Strength and Constitution and a +3 natural armor bonus. " +
                    "You have two 1d6 bite attacks plus poison, cannot be be tripped, and have a +2 resistance bonus on all saving throws against " +
                    "mind-affecting effects.");
                bp.m_Icon = VerminShapeIIcon;
                bp.AddComponent<Polymorph>(c => {
                    c.m_Race = BeastShapeIBuffPolymorph.m_Race;
                    c.m_Prefab = new UnitViewLink { AssetId = "bf09aef8864bed844a2353f76ffc1864" }; ;
                    c.m_PrefabFemale = BeastShapeIBuffPolymorph.m_PrefabFemale;
                    c.m_SpecialDollType = SpecialDollType.None;
                    c.m_ReplaceUnitForInspection = BeastShapeIBuffPolymorph.m_ReplaceUnitForInspection;
                    c.m_Portrait = BeastShapeIBuffPolymorph.m_Portrait;
                    c.m_KeepSlots = false;
                    c.Size = Size.Medium;
                    c.UseSizeAsBaseForDamage = false;
                    c.StrengthBonus = 2;
                    c.DexterityBonus = 0;
                    c.ConstitutionBonus = 2;
                    c.NaturalArmor = 3;
                    c.AllowDamageTransfer = false;
                    c.m_MainHand = Bite1d6.ToReference<BlueprintItemWeaponReference>();
                    c.m_OffHand = Bite1d6.ToReference<BlueprintItemWeaponReference>();
                    c.AllowDamageTransfer = false;
                    c.m_AdditionalLimbs = new BlueprintItemWeaponReference[0];
                    c.m_SecondaryAdditionalLimbs = new BlueprintItemWeaponReference[0];
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        TurnBarkStandart.ToReference<BlueprintUnitFactReference>(),
                        CentipedePoisonFeature.ToReference<BlueprintUnitFactReference>(),
                        TripImmunity.ToReference<BlueprintUnitFactReference>()
                    };
                    c.m_EnterTransition = BeastShapeIBuffPolymorph.m_EnterTransition;
                    c.m_ExitTransition = BeastShapeIBuffPolymorph.m_ExitTransition;
                    c.m_TransitionExternal = BeastShapeIBuffPolymorph.m_TransitionExternal;
                    c.m_SilentCaster = true;
                });
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.MindAffecting;
                    c.ModifierDescriptor = ModifierDescriptor.Resistance;
                    c.Value = 2;
                });
                bp.AddComponent<ReplaceAsksList>(c => {
                    c.m_Asks = CentipedeBarks.ToReference<BlueprintUnitAsksListReference>();
                });
                bp.AddComponent<ReplaceCastSource>(c => {
                    c.CastSource = CastSource.Head;
                });
                //bp.AddComponent<ReplaceSourceBone>(c => {
                //   c.SourceBone = ;
                //});
                bp.AddComponents(BeastShapeIBuffSuppressBuffs);
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });

            var VerminShapeIFlyBuff = Helpers.CreateBuff("VerminShapeIFlyBuff", bp => {
                bp.SetName("Vermin Shape (Giant Fly)");
                bp.SetDescription("You are in giant fly form now. You have a +2 size bonus to Strength and Constitution and a +3 natural armor bonus. " +
                    "Your movement speed is increased by 10 feet. You have two 1d6 bite attacks, trip and ground effect immunity, and have a +2 " +
                    "resistance bonus on all saving throws against mind-affecting effects.");
                bp.m_Icon = VerminShapeIIcon;
                bp.AddComponent<Polymorph>(c => {
                    c.m_Race = BeastShapeIBuffPolymorph.m_Race;
                    c.m_Prefab = new UnitViewLink { AssetId = "fd4b4672f8c1168468071445db6710dc" }; ;
                    c.m_PrefabFemale = BeastShapeIBuffPolymorph.m_PrefabFemale;
                    c.m_SpecialDollType = SpecialDollType.None;
                    c.m_ReplaceUnitForInspection = BeastShapeIBuffPolymorph.m_ReplaceUnitForInspection;
                    c.m_Portrait = BeastShapeIBuffPolymorph.m_Portrait;
                    c.m_KeepSlots = false;
                    c.Size = Size.Medium;
                    c.UseSizeAsBaseForDamage = false;
                    c.StrengthBonus = 2;
                    c.DexterityBonus = 0;
                    c.ConstitutionBonus = 2;
                    c.NaturalArmor = 3;
                    c.AllowDamageTransfer = false;
                    c.m_MainHand = Bite1d6.ToReference<BlueprintItemWeaponReference>();
                    c.m_OffHand = Bite1d6.ToReference<BlueprintItemWeaponReference>();
                    c.AllowDamageTransfer = false;
                    c.m_AdditionalLimbs = new BlueprintItemWeaponReference[0];
                    c.m_SecondaryAdditionalLimbs = new BlueprintItemWeaponReference[0];
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        TurnBarkStandart.ToReference<BlueprintUnitFactReference>(),
                        AirborneFeature.ToReference<BlueprintUnitFactReference>()
                    };
                    c.m_EnterTransition = BeastShapeIBuffPolymorph.m_EnterTransition;
                    c.m_ExitTransition = BeastShapeIBuffPolymorph.m_ExitTransition;
                    c.m_TransitionExternal = BeastShapeIBuffPolymorph.m_TransitionExternal;
                    c.m_SilentCaster = true;
                });
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.MindAffecting;
                    c.ModifierDescriptor = ModifierDescriptor.Resistance;
                    c.Value = 2;
                });
                bp.AddComponent<BuffMovementSpeed>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Value = 10;
                    c.ContextBonus = new ContextValue();
                    c.CappedOnMultiplier = false;
                    c.CappedMinimum = false;
                });
                bp.AddComponent<ReplaceAsksList>(c => {
                    c.m_Asks = CentipedeBarks.ToReference<BlueprintUnitAsksListReference>();
                });
                bp.AddComponent<ReplaceCastSource>(c => {
                    c.CastSource = CastSource.Head;
                });
                //bp.AddComponent<ReplaceSourceBone>(c => {
                //   c.SourceBone = ;
                //});
                bp.AddComponents(BeastShapeIBuffSuppressBuffs);
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });

            var VerminShapeIISpiderBuff = Helpers.CreateBuff("VerminShapeIISpiderBuff", bp => {
                bp.SetName("Vermin Shape (Giant Spider)");
                bp.SetDescription("You become a giant spider. You gain a +4 size bonus to your Strength, a -2 penalty to your Dexterity, a +2 size bonus to your Constitution, +5 natural " +
                    "armor bonus, trip immunity, web immunity, and a web ability with a ({g|Encyclopedia:DC}DC{/g} of 10 + half the casters level + their " +
                    "{g|Encyclopedia:Constitution}Constitution{/g} modifier); after use, the web ability cannot be used for 1 minute. " +
                    "Your movement speed is increased by 10 feet. You have have two 2d6 bite attacks plus poison");
                bp.m_Icon = InfestAbilityIcon;
                bp.AddComponent<Polymorph>(c => {
                    c.m_Race = BeastShapeIBuffPolymorph.m_Race;
                    c.m_Prefab = new UnitViewLink() { AssetId = "54e0335882f2dea4188214ea3c8c93de" };
                    c.m_PrefabFemale = BeastShapeIBuffPolymorph.m_PrefabFemale;
                    c.m_SpecialDollType = SpecialDollType.None;
                    c.m_ReplaceUnitForInspection = BeastShapeIBuffPolymorph.m_ReplaceUnitForInspection;
                    c.m_Portrait = BeastShapeIBuffPolymorph.m_Portrait;
                    c.m_KeepSlots = false;
                    c.Size = Size.Large;
                    c.UseSizeAsBaseForDamage = false;
                    c.StrengthBonus = 4;
                    c.DexterityBonus = -2;
                    c.ConstitutionBonus = 2;
                    c.NaturalArmor = 5;
                    c.AllowDamageTransfer = false;
                    c.m_MainHand = Bite2d6.ToReference<BlueprintItemWeaponReference>();
                    c.m_OffHand = Bite2d6.ToReference<BlueprintItemWeaponReference>();
                    c.AllowDamageTransfer = false;
                    c.m_AdditionalLimbs = new BlueprintItemWeaponReference[0];
                    c.m_SecondaryAdditionalLimbs = new BlueprintItemWeaponReference[0];
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        TurnBarkStandart.ToReference<BlueprintUnitFactReference>(),
                        TripImmunity.ToReference<BlueprintUnitFactReference>(),
                        SpiderWebImmunity.ToReference<BlueprintUnitFactReference>(),
                        VerminShapeWebAbility.ToReference<BlueprintUnitFactReference>()
                    };
                    c.m_EnterTransition = BeastShapeIBuffPolymorph.m_EnterTransition;
                    c.m_ExitTransition = BeastShapeIBuffPolymorph.m_ExitTransition;
                    c.m_TransitionExternal = BeastShapeIBuffPolymorph.m_TransitionExternal;
                    c.m_SilentCaster = true;
                });                
                bp.AddComponent<BuffMovementSpeed>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Value = 10;
                    c.ContextBonus = new ContextValue();
                    c.CappedOnMultiplier = false;
                    c.CappedMinimum = false;
                });
                
                bp.AddComponent<ReplaceAsksList>(c => {
                    c.m_Asks = SpiderBarks.ToReference<BlueprintUnitAsksListReference>();
                });
                bp.AddComponent<ReplaceCastSource>(c => {
                    c.CastSource = CastSource.Head;
                });
                //bp.AddComponent<ReplaceSourceBone>(c => {
                //   c.SourceBone = ;
                //});
                bp.AddComponents(BeastShapeIBuffSuppressBuffs);
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });






            var VerminShapeICentipedeAbility = Helpers.CreateBlueprint<BlueprintAbility>("VerminShapeICentipedeAbility", bp => {
                bp.SetName("Vermin Shape (Centipede)");
                bp.SetDescription("You become a giant-centipede. You gain a +2 size bonus to Strength and Constitution and a +3 natural armor bonus. " +
                    "You gain two 1d6 bite attacks plus poison, cannot be be tripped, and have a +2 resistance bonus on all saving throws against " +
                    "mind-affecting effects.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = VerminShapeICentipedeBuff.ToReference<BlueprintBuffReference>(),
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { VerminShapeICentipedeBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
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
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = VerminShapeIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("VerminShapeICentipedeAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var VerminShapeIFlyAbility = Helpers.CreateBlueprint<BlueprintAbility>("VerminShapeIFlyAbility", bp => {
                bp.SetName("Vermin Shape (Giant Fly)");
                bp.SetDescription("You become a giant-fly. You gain a +2 size bonus to Strength and Constitution and a +3 natural armor bonus. " +
                    "Your movement speed is increased by 10 feet. You have two 1d6 bite attacks, trip and ground effect immunity, and have a +2 " +
                    "resistance bonus on all saving throws against mind-affecting effects.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = VerminShapeIFlyBuff.ToReference<BlueprintBuffReference>(),
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { VerminShapeIFlyBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
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
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = VerminShapeIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("VerminShapeIFlyAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var VerminShapeIParentAbility = Helpers.CreateBlueprint<BlueprintAbility>("VerminShapeIParentAbility", bp => {
                bp.SetName("Plant Shape II");
                bp.SetDescription("You become a giant-centipede or a giant-fly.");
                bp.AddComponent<AbilityVariants>(c => {
                    c.m_Variants = new BlueprintAbilityReference[] {
                        VerminShapeICentipedeAbility.ToReference<BlueprintAbilityReference>(),
                        VerminShapeIFlyAbility.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = VerminShapeIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("VerminShapeIParentAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            VerminShapeICentipedeAbility.m_Parent = VerminShapeIParentAbility.ToReference<BlueprintAbilityReference>();
            VerminShapeIFlyAbility.m_Parent = VerminShapeIParentAbility.ToReference<BlueprintAbilityReference>();

            var VerminShapeIIAbility = Helpers.CreateBlueprint<BlueprintAbility>("VerminShapeIIAbility", bp => {
                bp.SetName("Vermin Shape II");
                bp.SetDescription("You become a giant spider. You gain a +4 size bonus to your Strength, a -2 penalty to your Dexterity, a +2 size bonus to your Constitution, +5 natural " +
                    "armor bonus, trip immunity, web immunity, and a web ability with a ({g|Encyclopedia:DC}DC{/g} of 10 + half the casters level + their " +
                    "{g|Encyclopedia:Constitution}Constitution{/g} modifier); after use, the web ability cannot be used for 1 minute. " +
                    "Your movement speed is increased by 10 feet. You have have two 2d6 bite attacks plus poison");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = VerminShapeIISpiderBuff.ToReference<BlueprintBuffReference>(),
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
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { VerminShapeIISpiderBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
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
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = InfestAbilityIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("VerminShapeIIAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });


            //I'll add the scrolls when I have time for more art
            //var VerminShapeIScroll = ItemTools.CreateScroll("ScrollOfVerminShapeI", Icon_ScrollOfVerminShapeI, VerminShapeIAbility, 3, 5);
            //VenderTools.AddScrollToLeveledVenders(VerminShapeIScroll);
            VerminShapeIParentAbility.AddToSpellList(SpellTools.SpellList.AlchemistSpellList, 4);
            VerminShapeIParentAbility.AddToSpellList(SpellTools.SpellList.BloodragerSpellList, 4);
            VerminShapeIParentAbility.AddToSpellList(SpellTools.SpellList.DruidSpellList, 3);
            VerminShapeIParentAbility.AddToSpellList(SpellTools.SpellList.MagusSpellList, 4);
            VerminShapeIParentAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 4);
            VerminShapeIParentAbility.AddToSpellList(SpellTools.SpellList.WitchSpellList, 3);

            //var VerminShapeIIScroll = ItemTools.CreateScroll("ScrollOfVerminShapeII", Icon_ScrollOfVerminShapeII, VerminShapeIIAbility, 4, 7);
            //VenderTools.AddScrollToLeveledVenders(VerminShapeIIScroll);
            VerminShapeIIAbility.AddToSpellList(SpellTools.SpellList.AlchemistSpellList, 5);
            VerminShapeIIAbility.AddToSpellList(SpellTools.SpellList.DruidSpellList, 4);
            VerminShapeIIAbility.AddToSpellList(SpellTools.SpellList.MagusSpellList, 5);
            VerminShapeIIAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 5);
            VerminShapeIIAbility.AddToSpellList(SpellTools.SpellList.WitchSpellList, 4);
        }
    }
}
