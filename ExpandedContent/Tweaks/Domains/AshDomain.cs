using System.Collections.Generic;
using ExpandedContent.Utilities;
using ExpandedContent.Extensions;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics.Components;
using ExpandedContent.Config;
using Kingmaker.RuleSystem;
using ExpandedContent.Tweaks.Components;
using Kingmaker.Craft;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Utility;
using ExpandedContent.Tweaks.Spells;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic;
using Kingmaker.ResourceLinks;
using UnityEngine;

namespace ExpandedContent.Tweaks.Domains {
    internal class AshDomain {

        public static void AddAshDomain() {

            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");
            var FireDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("4ecdf240d81533f47a5279f5075296b9");
            var FireDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("c65febce6d0dbde408de41d663a3bcb8");


            var Tremorgsence = Resources.GetBlueprint<BlueprintFeature>("6e668702fdc53c343a0363813683346e");
            var GaleAura = AssetLoader.LoadInternal("Skills", "Icon_GaleAura.jpg");
            var BlindBuff = Resources.GetBlueprint<BlueprintBuff>("0ec36e7596a4928489d2049e1e1c76a7");
            //AshDomainGreaterBuffs
            var AshDomainGreaterBuffRemoveInvisible = Helpers.CreateBuff("AshDomainGreaterBuffRemoveInvisible", bp => {
                bp.SetName("Wall of Ashes - Visible");
                bp.SetDescription("All creatures in the wall of ashes are made visible by the gaps in the smoke.");
                bp.m_Icon = GaleAura;
                bp.AddComponent<AddConditionImmunity>(c => {
                    c.Condition = UnitCondition.Invisible;
                });
                bp.m_AllowNonContextActions = false;
            });
            var AshDomainGreaterBuffBlind = Helpers.CreateBuff("AshDomainGreaterBuffBlind", bp => {
                bp.SetName("Wall of Ashes - Blind");
                bp.SetDescription("Any creature that fails the Fortitude save from wall of ashes is blinded for 1d4 rounds.");
                bp.m_Icon = BlindBuff.Icon;
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.Blindness;
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.SightBased;
                });
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => {
                    c.Descriptor = SpellDescriptor.SightBased;
                });
                bp.m_AllowNonContextActions = false;
            });
            //AshDomainGreaterResource
            var AshDomainGreaterResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("AshDomainGreaterResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = true,
                    m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        DruidClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[] {
                        DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    LevelIncrease = 1,
                    StartingLevel = 8,
                    StartingIncrease = 1,
                };
            });

            var AshDomainGreaterArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("AshDomainGreaterArea", bp => {
                bp.AddComponent<AbilityAreaEffectRunAction>(c => {
                    c.UnitEnter = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = AshDomainGreaterBuffRemoveInvisible.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0,
                                m_IsExtendable = true
                            },
                            IsFromSpell = false,
                        },
                        new ContextActionSavingThrow() {
                            Type = SavingThrowType.Fortitude,
                            FromBuff = false,
                            HasCustomDC = false,
                            CustomDC = new ContextValue(),
                            Actions = Helpers.CreateActionList(
                                new ContextActionConditionalSaved() {
                                    Succeed = Helpers.CreateActionList(),
                                    Failed = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = AshDomainGreaterBuffBlind.ToReference<BlueprintBuffReference>(),
                                            Permanent = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.D4,
                                                DiceCountValue = 1,
                                                BonusValue = 0,
                                                m_IsExtendable = true
                                            },
                                            IsFromSpell = false,
                                        })
                                }
                                )
                        }
                        );
                    c.UnitExit = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = AshDomainGreaterBuffRemoveInvisible.ToReference<BlueprintBuffReference>()
                        });
                    c.UnitMove = Helpers.CreateActionList();
                    c.Round = Helpers.CreateActionList();
                });
                bp.m_AllowNonContextActions = false;
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Any;
                bp.m_Tags = AreaEffectTags.DestroyableInCutscene;
                bp.SpellResistance = false;
                bp.AffectEnemies = true;
                bp.AggroEnemies = true;
                bp.AffectDead = false;
                bp.IgnoreSleepingUnits = false;
                bp.Shape = AreaEffectShape.Wall;
                bp.Size = new Feet() { m_Value = 60 };
                bp.Fx = new PrefabLink() { AssetId = "4ffc8d2162a215e44a1a728752b762eb" };
                bp.CanBeUsedInTacticalCombat = false;
                bp.m_TickRoundAfterSpawn = false;                
            });
            AshDomainGreaterArea.Fx = AshDomainGreaterArea.Fx.CreateDynamicProxy(pfl => {
                Main.Log($"Editing: {pfl}");
                pfl.name = "AshDomainGreater_WallAoE";
                Main.Log($"{FxDebug.DumpGameObject(pfl.gameObject)}");
                //pfl.transform.localScale = new(1.75f, 1.0f, 1.75f);
                //var Fog_Loop = pfl.transform.Find("Root/Fog_Loop (1)/Fog_Loop").GetComponent<ParticleSystem>();
                //Fog_Loop.startColor = new Color(0.0549f, 0.0313f, 0.0471f, 1f);
                ////Fog_Loop.scalingMode = ParticleSystemScalingMode.Hierarchy;
                //var Fog_Loop_1 = pfl.transform.Find("Root/Fog_Loop (1)").GetComponent<ParticleSystem>();
                //Fog_Loop_1.startColor = new Color(0.0549f, 0.0313f, 0.0471f, 1f);
                ////Fog_Loop_1.scalingMode = ParticleSystemScalingMode.Hierarchy;
                //var Smoke_Particles_Loop = pfl.transform.Find("Root/Smoke_Particles_Loop").GetComponent<ParticleSystem>();
                //Smoke_Particles_Loop.startColor = new Color(0.06f, 0.0313f, 0.0471f, 1f);
                //var StartSmoke_Fill = pfl.transform.Find("Root/StartSmoke_Fill").GetComponent<ParticleSystem>();
                //StartSmoke_Fill.startColor = new Color(0.0549f, 0.0313f, 0.0471f, 1f);
                ////StartSmoke_Fill.scalingMode = ParticleSystemScalingMode.Hierarchy;
                //var StartSmoke_Fill_1 = pfl.transform.Find("Root/StartSmoke_Fill (1)").GetComponent<ParticleSystem>();
                //StartSmoke_Fill_1.startColor = new Color(0.0549f, 0.0313f, 0.0471f, 1f);
                ////StartSmoke_Fill_1.scalingMode = ParticleSystemScalingMode.Hierarchy;
                //var StartSmoke_Particles = pfl.transform.Find("Root/StartSmoke_Particles").GetComponent<ParticleSystem>();
                //StartSmoke_Particles.startColor = new Color(0.06f, 0.0313f, 0.0471f, 1f);
                ////StartSmoke_Particles.scalingMode = ParticleSystemScalingMode.Hierarchy;
                //var Sparks = pfl.transform.Find("Root/Sparks").GetComponent<ParticleSystem>();
                //Sparks.startColor = new Color(0.9f, 0.25f, 0f, 1f);
                //Sparks.transform.localScale = new(1.75f, 1.0f, 1.75f);
                //var Sparks_Loop = pfl.transform.Find("Root/Sparks_Loop").GetComponent<ParticleSystem>();
                //Sparks_Loop.startColor = new Color(0.8f, 0.5f, 0f, 0.9f);
                //Sparks_Loop.transform.localScale = new(1.75f, 1.0f, 1.75f);
            });



            //AshDomainGreaterAbility            
            var AshDomainGreaterAbility = Helpers.CreateBlueprint<BlueprintAbility>("AshDomainGreaterAbility", bp => {
                bp.SetName("Wall of Ashes");
                bp.SetDescription("At 8th level, you can create a wall of swirling ashes lasting for one minute. Any creature passing through it must make a Fortitude save or " +
                    "be blinded for 1d4 rounds. The wall of ash reveals invisible creatures that are inside it, although they become invisible again if they move out of the wall. " +
                    "You can use this ability a number of times per day equal to your cleric level.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnAreaEffect() {
                            m_AreaEffect = AshDomainGreaterArea.ToReference<BlueprintAbilityAreaEffectReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true,
                            },
                            OnUnit = false
                        });
                });
                //bp.AddComponent<ContextCalculateSharedValue>(c => {
                //    c.ValueType = AbilitySharedValue.Damage;
                //    c.Value = new ContextDiceValue() {
                //        DiceType = DiceType.One,
                //        DiceCountValue = new ContextValue() {
                //            ValueType = ContextValueType.Rank,
                //            Value = 0,
                //            ValueRank = AbilityRankType.DamageDice
                //        },
                //        BonusValue = new ContextValue() {
                //            ValueType = ContextValueType.Rank,
                //            Value = 0,
                //            ValueRank = AbilityRankType.DamageDiceAlternative
                //        }
                //    };
                //    c.Modifier = 1;
                //});
                //bp.AddComponent<ContextRankConfig>(c => {
                //    c.m_Type = AbilityRankType.DamageDice;
                //    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                //    c.m_Stat = StatType.Unknown;
                //    c.m_Progression = ContextRankProgression.Div2;
                //    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                //    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] {
                //        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                //        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                //    };
                //    c.m_Class = new BlueprintCharacterClassReference[] {
                //        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                //        DruidClass.ToReference<BlueprintCharacterClassReference>(),
                //        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                //        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                //        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                //        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                //    };
                //});
                //bp.AddComponent<ContextRankConfig>(c => {
                //    c.m_Type = AbilityRankType.DamageDiceAlternative;
                //    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                //    c.m_Stat = StatType.Wisdom;
                //    c.m_SpecificModifier = ModifierDescriptor.None;
                //    c.m_Progression = ContextRankProgression.BonusValue;
                //});
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = AshDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Debuff;
                    c.SavingThrow = CraftSavingThrow.Fortitude;
                    c.AOEType = CraftAOE.AOE;
                });
                bp.m_Icon = GaleAura;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken;
                bp.LocalizedDuration = Helpers.CreateString("AshDomainGreaterAbility.Duration", "1 minute");
                bp.LocalizedSavingThrow = Helpers.CreateString("AshDomainGreaterAbility.SavingThrow", "Fortitude partial");
                bp.IsDomainAbility = true;
            });
            //AshDomainGreaterFeature
            var AshDomainGreaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("AshDomainGreaterFeature", bp => {
                bp.SetName("Wall of Ashes");
                bp.SetDescription("At 8th level, you can create a wall of swirling ashes lasting for one minute. Any creature passing through it must make a Fortitude save or " +
                    "be blinded for 1d4 rounds. The wall of ash reveals invisible creatures that are inside it, although they become invisible again if they move out of the wall. " +
                    "You can use this ability a number of times per day equal to your cleric level.");
                bp.m_Icon = GaleAura;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = AshDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { AshDomainGreaterAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { AshDomainGreaterAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            //Spelllist
            var BurningHandsSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4783c3709a74a794dbe7c8e7e0b1b038");
            var ScorchingRaySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("cdb106d53c65bbc4086183d54c3b97c7");
            var FireballSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2d81362af43aeac4387a3d4fced489c3");
            var ControlledFireballSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f72f8f03bf0136c4180cd1d70eb773a5");
            var FlameStrikeSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f9910c76efc34af41b6e43d5d8752f0f");
            var SummonElementalHugeFireSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4814f8645d1d77447a70479c0be51c72");
            var DisintegrateSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4aa7942c3e62a164387a73184bca3fc1");
            var SummonElementalElderFireSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e4926aa766a1cc048835237b3a97597d");
            var FieryBodySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("08ccad78cac525040919d51963f9ac39");
            var AshDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("AshDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BurningHandsSpell
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ScorchingRaySpell
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            FireballSpell
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ControlledFireballSpell
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            FlameStrikeSpell
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SummonElementalHugeFireSpell
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            DisintegrateSpell
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SummonElementalElderFireSpell
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            FieryBodySpell
                        }
                    },
                };
            });
            var AshDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("AshDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = AshDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var AshDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("AshDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { FireDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = FireDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { FireDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = AshDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Ash Subdomain");
                bp.SetDescription("\nYou can focus flame to a fine point, and use the embers left to blind your foes. " +
                    "\nFire Bolt: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can unleash a scorching bolt of divine fire from your outstretched hand. " +
                    "You can target any single foe within 30 feet as a ranged {g|Encyclopedia:TouchAttack}touch attack{/g} with this bolt of fire. If you hit the foe, the " +
                    "fire bolt deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} + 1 point for every two levels you possess in " +
                    "the class that gave you access to this domain. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. " +
                    "\nWall of Ashes: At 8th level, you can create a wall of swirling ashes lasting for one minute. Any creature passing through it must make a Fortitude save or " +
                    "be blinded for 1d4 rounds. The wall of ash reveals invisible creatures that are inside it, although they become invisible again if they move out of the wall. " +
                    "You can use this ability a number of times per day equal to your cleric level.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var AshDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("AshDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = AshDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            // Main Blueprint
            var AshDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("AshDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = AshDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = AshDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = AshDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Ash Subdomain");
                bp.SetDescription("\nYou can focus flame to a fine point, and use the embers left to blind your foes. " +
                    "\nFire Bolt: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can unleash a scorching bolt of divine fire from your outstretched hand. " +
                    "You can target any single foe within 30 feet as a ranged {g|Encyclopedia:TouchAttack}touch attack{/g} with this bolt of fire. If you hit the foe, the " +
                    "fire bolt deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} + 1 point for every two levels you possess in " +
                    "the class that gave you access to this domain. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. " +
                    "\nWall of Ashes: At 8th level, you can create a wall of swirling ashes lasting for one minute. Any creature passing through it must make a Fortitude save or " +
                    "be blinded for 1d4 rounds. The wall of ash reveals invisible creatures that are inside it, although they become invisible again if they move out of the wall. " +
                    "You can use this ability a number of times per day equal to your cleric level. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsBurningHands}burning hands{/g}, {g|SpellsScorchingRay}scorching ray{/g}, {g|SpellsFireball}fireball{/g}, " +
                    "{g|SpellsControlledFireball}controlled fireball{/g}, {g|SpellsFlameStrike}flame strike{/g}, {g|SpellsSummonHugeFireElemental}summon huge fire elemental{/g}, " +
                    "{g|SpellsDisintegrate}disintegrate{/g}, {g|SpellsSummonElderFireElemental}summon elder fire elemental{/g}, {g|SpellsFieryBody}fiery body{/g}.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.Domain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = -2
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, AshDomainBaseFeature),
                    Helpers.LevelEntry(8, AshDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(AshDomainBaseFeature, AshDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var AshDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("AshDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = AshDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = AshDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Ash Subdomain");
                bp.SetDescription("\nYou can focus flame to a fine point, and use the embers left to blind your foes. " +
                    "\nFire Bolt: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can unleash a scorching bolt of divine fire from your outstretched hand. " +
                    "You can target any single foe within 30 feet as a ranged {g|Encyclopedia:TouchAttack}touch attack{/g} with this bolt of fire. If you hit the foe, the " +
                    "fire bolt deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} + 1 point for every two levels you possess in " +
                    "the class that gave you access to this domain. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. " +
                    "\nWall of Ashes: At 8th level, you can create a wall of swirling ashes lasting for one minute. Any creature passing through it must make a Fortitude save or " +
                    "be blinded for 1d4 rounds. The wall of ash reveals invisible creatures that are inside it, although they become invisible again if they move out of the wall. " +
                    "You can use this ability a number of times per day equal to your cleric level. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsBurningHands}burning hands{/g}, {g|SpellsScorchingRay}scorching ray{/g}, {g|SpellsFireball}fireball{/g}, " +
                    "{g|SpellsControlledFireball}controlled fireball{/g}, {g|SpellsFlameStrike}flame strike{/g}, {g|SpellsSummonHugeFireElemental}summon huge fire elemental{/g}, " +
                    "{g|SpellsDisintegrate}disintegrate{/g}, {g|SpellsSummonElderFireElemental}summon elder fire elemental{/g}, {g|SpellsFieryBody}fiery body{/g}.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.ClericSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, AshDomainBaseFeature),
                    Helpers.LevelEntry(8, AshDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(AshDomainBaseFeature, AshDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // AshDomainSpellListFeatureDruid
            var AshDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("AshDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = AshDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            // AshDomainProgressionDruid
            var AshDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("AshDomainProgressionDruid", bp => {
                bp.SetName("Ash Subdomain");
                bp.SetDescription("\nYou can focus flame to a fine point, and use the embers left to blind your foes. " +
                    "\nFire Bolt: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can unleash a scorching bolt of divine fire from your outstretched hand. " +
                    "You can target any single foe within 30 feet as a ranged {g|Encyclopedia:TouchAttack}touch attack{/g} with this bolt of fire. If you hit the foe, the " +
                    "fire bolt deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} + 1 point for every two levels you possess in " +
                    "the class that gave you access to this domain. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. " +
                    "\nWall of Ashes: At 8th level, you can create a wall of swirling ashes lasting for one minute. Any creature passing through it must make a Fortitude save or " +
                    "be blinded for 1d4 rounds. The wall of ash reveals invisible creatures that are inside it, although they become invisible again if they move out of the wall. " +
                    "You can use this ability a number of times per day equal to your cleric level. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsBurningHands}burning hands{/g}, {g|SpellsScorchingRay}scorching ray{/g}, {g|SpellsFireball}fireball{/g}, " +
                    "{g|SpellsControlledFireball}controlled fireball{/g}, {g|SpellsFlameStrike}flame strike{/g}, {g|SpellsSummonHugeFireElemental}summon huge fire elemental{/g}, " +
                    "{g|SpellsDisintegrate}disintegrate{/g}, {g|SpellsSummonElderFireElemental}summon elder fire elemental{/g}, {g|SpellsFieryBody}fiery body{/g}.");
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.DruidDomain, FeatureGroup.BlightDruidDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, AshDomainBaseFeature, AshDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(8, AshDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions
            var FireDomainBaseAbilitySeparatist = Resources.GetBlueprint<BlueprintAbility>("411e5a7eda124e289f369fd206aec513");
            var FireDomainBaseResourceSeparatist = Resources.GetBlueprint<BlueprintAbilityResource>("1d0d026ce5f64f9c94e79d3e6e1795ac");

            var AshDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("AshDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var AshDomainGreaterResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("AshDomainGreaterResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = false,
                    m_Class = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        DruidClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[] {
                        DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    IncreasedByLevelStartPlusDivStep = true,
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    LevelIncrease = 1,
                    StartingLevel = 3,
                    StartingIncrease = 1,
                    LevelStep = 1,
                    PerStepIncrease = 1,
                };
                bp.m_Min = 1;
            });

            

            var AshDomainGreaterAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("AshDomainGreaterAbilitySeparatist", bp => {
                bp.SetName("Wall of Ashes");
                bp.SetDescription("At 8th level, you can create a wall of swirling ashes lasting for one minute. Any creature passing through it must make a Fortitude save or " +
                    "be blinded for 1d4 rounds. The wall of ash reveals invisible creatures that are inside it, although they become invisible again if they move out of the wall. " +
                    "You can use this ability a number of times per day equal to your cleric level.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnAreaEffect() {
                            m_AreaEffect = AshDomainGreaterArea.ToReference<BlueprintAbilityAreaEffectReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true,
                            },
                            OnUnit = false
                        });
                });
                //bp.AddComponent<ContextCalculateSharedValue>(c => {
                //    c.ValueType = AbilitySharedValue.Damage;
                //    c.Value = new ContextDiceValue() {
                //        DiceType = DiceType.One,
                //        DiceCountValue = new ContextValue() {
                //            ValueType = ContextValueType.Rank,
                //            Value = 0,
                //            ValueRank = AbilityRankType.DamageDice
                //        },
                //        BonusValue = new ContextValue() {
                //            ValueType = ContextValueType.Rank,
                //            Value = 0,
                //            ValueRank = AbilityRankType.Default
                //        }
                //    };
                //    c.Modifier = 1;
                //});
                //bp.AddComponent<ContextRankConfig>(c => {
                //    c.m_Type = AbilityRankType.DamageDice;
                //    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                //    c.m_Stat = StatType.Unknown;
                //    c.m_Progression = ContextRankProgression.StartPlusDivStep;
                //    c.m_StartLevel = 2;
                //    c.m_StepLevel = 2;
                //    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                //    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] {
                //        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                //        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                //    };
                //    c.m_Class = new BlueprintCharacterClassReference[] {
                //        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                //        DruidClass.ToReference<BlueprintCharacterClassReference>(),
                //        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                //        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                //        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                //        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                //    };
                //});
                //bp.AddComponent<ContextRankConfig>(c => {
                //    c.m_Type = AbilityRankType.DamageDiceAlternative;
                //    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                //    c.m_Stat = StatType.Wisdom;
                //    c.m_SpecificModifier = ModifierDescriptor.None;
                //    c.m_Progression = ContextRankProgression.BonusValue;
                //});
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = AshDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Debuff;
                    c.SavingThrow = CraftSavingThrow.Fortitude;
                    c.AOEType = CraftAOE.AOE;
                });
                bp.m_Icon = GaleAura;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Long;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken;
                bp.LocalizedDuration = Helpers.CreateString("AshDomainGreaterAbility.Duration", "1 minute");
                bp.LocalizedSavingThrow = Helpers.CreateString("AshDomainGreaterAbility.SavingThrow", "Fortitude partial");
                bp.IsDomainAbility = true;
            });

            var AshDomainGreaterFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("AshDomainGreaterFeatureSeparatist", bp => {
                bp.SetName("Tunnel Runner");
                bp.SetDescription("At 8th level, while underground you may gain an insight bonus equal to your cleric level on Stealth and Perception skill checks and an " +
                    "insight bonus equal to your Wisdom modifier on initiative checks. You can use this ability for 1 minute per day per cleric level you " +
                    "possess. These minutes do not need to be consecutive, but they must be spent in " +
                    "1-minute increments.");
                bp.m_Icon = GaleAura;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = AshDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { AshDomainGreaterAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { AshDomainGreaterAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var AshDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("AshDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { FireDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = FireDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { FireDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = AshDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Ash Subdomain");
                bp.SetDescription("\nYou can focus flame to a fine point, and use the embers left to blind your foes. " +
                    "\nFire Bolt: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can unleash a scorching bolt of divine fire from your outstretched hand. " +
                    "You can target any single foe within 30 feet as a ranged {g|Encyclopedia:TouchAttack}touch attack{/g} with this bolt of fire. If you hit the foe, the " +
                    "fire bolt deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} + 1 point for every two levels you possess in " +
                    "the class that gave you access to this domain. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. " +
                    "\nWall of Ashes: At 8th level, you can create a wall of swirling ashes lasting for one minute. Any creature passing through it must make a Fortitude save or " +
                    "be blinded for 1d4 rounds. The wall of ash reveals invisible creatures that are inside it, although they become invisible again if they move out of the wall. " +
                    "You can use this ability a number of times per day equal to your cleric level.");
                bp.IsClassFeature = true;
            });

            var AshDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("AshDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = AshDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = AshDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = AshDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = AshDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = AshDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Ash Subdomain");
                bp.SetDescription("\nYou can focus flame to a fine point, and use the embers left to blind your foes. " +
                    "\nFire Bolt: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can unleash a scorching bolt of divine fire from your outstretched hand. " +
                    "You can target any single foe within 30 feet as a ranged {g|Encyclopedia:TouchAttack}touch attack{/g} with this bolt of fire. If you hit the foe, the " +
                    "fire bolt deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} + 1 point for every two levels you possess in " +
                    "the class that gave you access to this domain. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. " +
                    "\nWall of Ashes: At 8th level, you can create a wall of swirling ashes lasting for one minute. Any creature passing through it must make a Fortitude save or " +
                    "be blinded for 1d4 rounds. The wall of ash reveals invisible creatures that are inside it, although they become invisible again if they move out of the wall. " +
                    "You can use this ability a number of times per day equal to your cleric level. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsBurningHands}burning hands{/g}, {g|SpellsScorchingRay}scorching ray{/g}, {g|SpellsFireball}fireball{/g}, " +
                    "{g|SpellsControlledFireball}controlled fireball{/g}, {g|SpellsFlameStrike}flame strike{/g}, {g|SpellsSummonHugeFireElemental}summon huge fire elemental{/g}, " +
                    "{g|SpellsDisintegrate}disintegrate{/g}, {g|SpellsSummonElderFireElemental}summon elder fire elemental{/g}, {g|SpellsFieryBody}fiery body{/g}.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] { };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, AshDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(10, AshDomainGreaterFeatureSeparatist)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(AshDomainBaseFeatureSeparatist, AshDomainGreaterFeatureSeparatist)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            AshDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                AshDomainProgression.ToReference<BlueprintFeatureReference>(),
                AshDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            AshDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = AshDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            AshDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = AshDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            AshDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = AshDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            AshDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = AshDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            AshDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = AshDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(AshDomainGreaterAbility.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(AshDomainGreaterAbilitySeparatist.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Ash Subdomain")) { return; }
            DomainTools.RegisterDomain(AshDomainProgression);
            DomainTools.RegisterSecondaryDomain(AshDomainProgressionSecondary);
            DomainTools.RegisterDruidDomain(AshDomainProgressionDruid);
            DomainTools.RegisterBlightDruidDomain(AshDomainProgressionDruid);
            DomainTools.RegisterDivineHunterDomain(AshDomainProgression);
            DomainTools.RegisterTempleDomain(AshDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(AshDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(AshDomainProgression, AshDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(AshDomainProgressionSeparatist);

        }
    }
}
