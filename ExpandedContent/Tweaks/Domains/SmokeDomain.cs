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
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Utility;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using ExpandedContent.Config;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Enums.Damage;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using UnityEngine;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.Craft;

namespace ExpandedContent.Tweaks.Domains {
    internal class SmokeDomain {

        public static void AddSmokeDomain() {

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
            var FireDomainGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("ef5aedb6a97071b46969b61a86a967db");
            var FireDomainCapstone = Resources.GetBlueprint<BlueprintFeature>("6c46620d4cab41b42be8dd8cfb1aa9d2");

            var CloudOfSmokeIcon = AssetLoader.LoadInternal("Skills", "Icon_CloudOfSmoke.jpg");
            var BlurBuff = Resources.GetBlueprint<BlueprintBuff>("dd3ad347240624d46a11a092b4dd4674");

            var SmokeDomainBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("SmokeDomainBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });



            var SmokeDomainCloudBuff = Helpers.CreateBuff("SmokeDomainCloudBuff", bp => {
                bp.SetName("Cloud of Smoke");
                bp.SetDescription("Creatures inside the cloud take a –2 penalty on attack rolls and Perception skill checks for as long as they remain inside " +
                    "and for 1 round after exiting the cloud. Creatures inside the cloud gain concealment from attacks made by opponents that are not adjacent to them.");
                bp.m_Icon = BlurBuff.Icon;
                bp.AddComponent<AddConcealment>(c => {
                    c.Descriptor = ConcealmentDescriptor.Fog;
                    c.Concealment = Concealment.Partial;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.CheckDistance = true;
                    c.DistanceGreater = new Feet() { m_Value = 5 };
                    c.OnlyForAttacks = true;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillPerception;
                    c.Descriptor = ModifierDescriptor.Penalty;
                    c.Value = -2;
                });
                bp.AddComponent<AddAttackBonus>(c => {
                    c.Bonus = -2;
                });
                bp.FxOnStart = BlurBuff.FxOnStart; //blur
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var SmokeDomainCloudExitBuff = Helpers.CreateBuff("SmokeDomainCloudExitBuff", bp => {
                bp.SetName("Cloud of Smoke - Lingering effect");
                bp.SetDescription("Creatures escaping from the cloud of smoke take a –2 penalty on attack rolls and Perception skill checks for 1 round after exiting the cloud.");
                bp.m_Icon = CloudOfSmokeIcon;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillPerception;
                    c.Descriptor = ModifierDescriptor.Penalty;
                    c.Value = -2;
                });
                bp.AddComponent<AddAttackBonus>(c => {
                    c.Bonus = -2;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });

            var SmokeDomainBaseAbilityArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("SmokeDomainBaseAbilityArea", bp => {
                bp.AddComponent<AbilityAreaEffectRunAction>(c => {
                    c.UnitEnter = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = SmokeDomainCloudBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue(),
                                BonusValue = new ContextValue(),
                                m_IsExtendable = true
                            },
                            IsFromSpell = false,
                        }
                        );
                    c.UnitExit = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = SmokeDomainCloudBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = SmokeDomainCloudExitBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue(),
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            IsFromSpell = false,
                        }
                        );
                    c.UnitMove = Helpers.CreateActionList();
                    c.Round = Helpers.CreateActionList(

                        );
                });
                bp.m_AllowNonContextActions = false;
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Any;
                bp.m_Tags = AreaEffectTags.DestroyableInCutscene;
                bp.SpellResistance = false;
                bp.AffectEnemies = true;
                bp.AggroEnemies = true;
                bp.AffectDead = false;
                bp.IgnoreSleepingUnits = false;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = new Feet() { m_Value = 5 };
                bp.Fx = new PrefabLink() { AssetId = "ee38b41b2b360b2458ec48f1868ca51b" }; //Cloudkill cloud
                bp.CanBeUsedInTacticalCombat = false;
                bp.m_TickRoundAfterSpawn = false;
            });
            SmokeDomainBaseAbilityArea.Fx = SmokeDomainBaseAbilityArea.Fx.CreateDynamicProxy(pfl => {
                Main.Log($"Editing: {pfl}");
                pfl.name = "SmokeDomainBaseAbility_5feetAoE";
                //Main.Log($"{FxDebug.DumpGameObject(pfl.gameObject)}");
                pfl.transform.localScale = new(0.25f, 1.0f, 0.25f);
                var Fog_Loop = pfl.transform.Find("Root/Fog_Loop (1)/Fog_Loop").GetComponent<ParticleSystem>();
                Fog_Loop.startColor = new Color(0.0549f, 0.0313f, 0.0471f, 1f);
                Fog_Loop.scalingMode = ParticleSystemScalingMode.Hierarchy;
                var Fog_Loop_1 = pfl.transform.Find("Root/Fog_Loop (1)").GetComponent<ParticleSystem>();
                Fog_Loop_1.startColor = new Color(0.0549f, 0.0313f, 0.0471f, 1f);
                Fog_Loop_1.scalingMode = ParticleSystemScalingMode.Hierarchy;
                var Smoke_Particles_Loop = pfl.transform.Find("Root/Smoke_Particles_Loop").GetComponent<ParticleSystem>();
                Smoke_Particles_Loop.startColor = new Color(0.06f, 0.0313f, 0.0471f, 1f);
                var StartSmoke_Fill = pfl.transform.Find("Root/StartSmoke_Fill").GetComponent<ParticleSystem>();
                StartSmoke_Fill.startColor = new Color(0.0549f, 0.0313f, 0.0471f, 1f);
                StartSmoke_Fill.scalingMode = ParticleSystemScalingMode.Hierarchy;
                var StartSmoke_Fill_1 = pfl.transform.Find("Root/StartSmoke_Fill (1)").GetComponent<ParticleSystem>();
                StartSmoke_Fill_1.startColor = new Color(0.0549f, 0.0313f, 0.0471f, 1f);
                StartSmoke_Fill_1.scalingMode = ParticleSystemScalingMode.Hierarchy;
                var StartSmoke_Particles = pfl.transform.Find("Root/StartSmoke_Particles").GetComponent<ParticleSystem>();
                StartSmoke_Particles.startColor = new Color(0.06f, 0.0313f, 0.0471f, 1f);
                StartSmoke_Particles.scalingMode = ParticleSystemScalingMode.Hierarchy;
                Object.DestroyImmediate(pfl.transform.Find("Root/Sparks").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("Root/Sparks_Loop").gameObject);
            });

            var SmokeDomainBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("SmokeDomainBaseAbility", bp => {
                bp.SetName("Cloud of Smoke");
                bp.SetDescription("As a standard action, you can create a 5-foot-radius cloud of smoke lasting 1 minute. Creatures inside the cloud take a –2 penalty on attack rolls " +
                    "and Perception skill checks for as long as they remain inside and for 1 round after exiting the cloud. Creatures inside the cloud gain concealment " +
                    "from attacks made by opponents that are not adjacent to them. You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = SmokeDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnAreaEffect() {
                            m_AreaEffect = SmokeDomainBaseAbilityArea.ToReference<BlueprintAbilityAreaEffectReference>(),
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
                bp.AddComponent<AbilityAoERadius>(c => {
                    c.m_Radius = 5.Feet();
                    c.m_TargetType = TargetType.Any;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Other;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.AOE;
                });
                bp.m_Icon = CloudOfSmokeIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.AvailableMetamagic = Metamagic.Quicken;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = Helpers.CreateString("SmokeDomainBaseAbility.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            //Spelllist
            var BurningHandsSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4783c3709a74a794dbe7c8e7e0b1b038");
            var GlitterdustSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("ce7dad2b25acf85429b6c9550787b2d9");
            var StinkingCloudSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("68a9e6d7256f1354289a39003a46d826");
            var ControlledFireballSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f72f8f03bf0136c4180cd1d70eb773a5");
            var FlameStrikeSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f9910c76efc34af41b6e43d5d8752f0f");
            var SummonElementalHugeFireSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4814f8645d1d77447a70479c0be51c72");
            var ElementalBodyIVFireSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c281eeecc554b72449fef43924e522ce");
            var SummonElementalElderFireSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e4926aa766a1cc048835237b3a97597d");
            var FieryBodySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("08ccad78cac525040919d51963f9ac39");
            var SmokeDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("SmokeDomainSpellList", bp => {
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
                            GlitterdustSpell
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            StinkingCloudSpell
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
                            ElementalBodyIVFireSpell
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
            var SmokeDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("SmokeDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = SmokeDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var SmokeDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("SmokeDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { SmokeDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = SmokeDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { SmokeDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = SmokeDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Smoke Subdomain");
                bp.SetDescription("\nYou can blind and choke with smoke, those who dare not to burn. \nCloud of Smoke: " +
                    "As a standard action, you can create a 5-foot-radius cloud of smoke lasting 1 minute. Creatures inside the cloud take a –2 penalty on attack rolls " +
                    "and Perception skill checks for as long as they remain inside and for 1 round after exiting the cloud. Creatures inside the cloud gain concealment " +
                    "from attacks made by opponents that are not adjacent to them. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nFire Resistance: At 6th level, you gain resist fire 10. This resistance increases to 20 at 12th level. At 20th level, you gain immunity to fire.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var SmokeDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("SmokeDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = SmokeDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            // Main Blueprint
            var SmokeDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("SmokeDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = SmokeDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = SmokeDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = SmokeDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Smoke Subdomain");
                bp.SetDescription("\nYou can blind and choke with smoke, those who dare not to burn. \nCloud of Smoke: " +
                    "As a standard action, you can create a 5-foot-radius cloud of smoke lasting 1 minute. Creatures inside the cloud take a –2 penalty on attack rolls " +
                    "and Perception skill checks for as long as they remain inside and for 1 round after exiting the cloud. Creatures inside the cloud gain concealment " +
                    "from attacks made by opponents that are not adjacent to them. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nFire Resistance: At 6th level, you gain resist fire 10. This resistance increases to 20 at 12th level. At 20th level, you gain immunity to fire. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsBurningHands}burning hands{/g}, {g|SpellsGlitterdust}glitterdust{/g}, {g|SpellsStinkingCloud}stinking cloud{/g}, " +
                    "{g|SpellsControlledFireball}controlled fireball{/g}, {g|SpellsFlameStrike}flame strike{/g}, {g|SpellsSummonHugeFireElemental}summon huge fire elemental{/g}, " +
                    "{g|SpellsElementalBodyIVFire}elemental body IV (fire){/g}, {g|SpellsSummonElderFireElemental}summon elder fire elemental{/g}, {g|SpellsFieryBody}fiery body{/g}.");
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
                    Helpers.LevelEntry(1, SmokeDomainBaseFeature),
                    Helpers.LevelEntry(6, FireDomainGreaterFeature),
                    Helpers.LevelEntry(12, FireDomainGreaterFeature),
                    Helpers.LevelEntry(20, FireDomainCapstone)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(SmokeDomainBaseFeature, FireDomainGreaterFeature, FireDomainGreaterFeature, FireDomainCapstone)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var SmokeDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("SmokeDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = SmokeDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = SmokeDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Smoke Subdomain");
                bp.SetDescription("\nYou can blind and choke with smoke, those who dare not to burn. \nCloud of Smoke: " +
                    "As a standard action, you can create a 5-foot-radius cloud of smoke lasting 1 minute. Creatures inside the cloud take a –2 penalty on attack rolls " +
                    "and Perception skill checks for as long as they remain inside and for 1 round after exiting the cloud. Creatures inside the cloud gain concealment " +
                    "from attacks made by opponents that are not adjacent to them. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nFire Resistance: At 6th level, you gain resist fire 10. This resistance increases to 20 at 12th level. At 20th level, you gain immunity to fire. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsBurningHands}burning hands{/g}, {g|SpellsGlitterdust}glitterdust{/g}, {g|SpellsStinkingCloud}stinking cloud{/g}, " +
                    "{g|SpellsControlledFireball}controlled fireball{/g}, {g|SpellsFlameStrike}flame strike{/g}, {g|SpellsSummonHugeFireElemental}summon huge fire elemental{/g}, " +
                    "{g|SpellsElementalBodyIVFire}elemental body IV (fire){/g}, {g|SpellsSummonElderFireElemental}summon elder fire elemental{/g}, {g|SpellsFieryBody}fiery body{/g}.");
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
                    Helpers.LevelEntry(1, SmokeDomainBaseFeature),
                    Helpers.LevelEntry(6, FireDomainGreaterFeature),
                    Helpers.LevelEntry(12, FireDomainGreaterFeature),
                    Helpers.LevelEntry(20, FireDomainCapstone)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(SmokeDomainBaseFeature, FireDomainGreaterFeature, FireDomainGreaterFeature, FireDomainCapstone)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // SmokeDomainSpellListFeatureDruid
            var SmokeDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("SmokeDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = SmokeDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            // SmokeDomainProgressionDruid
            var SmokeDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("SmokeDomainProgressionDruid", bp => {
                bp.SetName("Smoke Subdomain");
                bp.SetDescription("\nYou can blind and choke with smoke, those who dare not to burn. \nCloud of Smoke: " +
                    "As a standard action, you can create a 5-foot-radius cloud of smoke lasting 1 minute. Creatures inside the cloud take a –2 penalty on attack rolls " +
                    "and Perception skill checks for as long as they remain inside and for 1 round after exiting the cloud. Creatures inside the cloud gain concealment " +
                    "from attacks made by opponents that are not adjacent to them. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nFire Resistance: At 6th level, you gain resist fire 10. This resistance increases to 20 at 12th level. At 20th level, you gain immunity to fire. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsBurningHands}burning hands{/g}, {g|SpellsGlitterdust}glitterdust{/g}, {g|SpellsStinkingCloud}stinking cloud{/g}, " +
                    "{g|SpellsControlledFireball}controlled fireball{/g}, {g|SpellsFlameStrike}flame strike{/g}, {g|SpellsSummonHugeFireElemental}summon huge fire elemental{/g}, " +
                    "{g|SpellsElementalBodyIVFire}elemental body IV (fire){/g}, {g|SpellsSummonElderFireElemental}summon elder fire elemental{/g}, {g|SpellsFieryBody}fiery body{/g}.");
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
                    Helpers.LevelEntry(1, SmokeDomainBaseFeature,SmokeDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(6, FireDomainGreaterFeature),
                    Helpers.LevelEntry(12, FireDomainGreaterFeature),
                    Helpers.LevelEntry(20, FireDomainCapstone)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(SmokeDomainBaseFeature, FireDomainGreaterFeature, FireDomainGreaterFeature, FireDomainCapstone)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions
            var ProtectionDomainGreaterFeatureSeparatist = Resources.GetBlueprint<BlueprintFeature>("7eb39ba8115a422bb69c702cc20ca58a");
            var SeparatistAsIsProperty = Resources.GetModBlueprint<BlueprintUnitProperty>("SeparatistAsIsProperty");


            var SmokeDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("SmokeDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });



            var SmokeDomainBaseResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("SmokeDomainBaseResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 2,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });
            var SmokeDomainBaseAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("SmokeDomainBaseAbilitySeparatist", bp => {
                bp.SetName("Cloud of Smoke");
                bp.SetDescription("As a standard action, you can create a 5-foot-radius cloud of smoke lasting 1 minute. Creatures inside the cloud take a –2 penalty on attack rolls " +
                    "and Perception skill checks for as long as they remain inside and for 1 round after exiting the cloud. Creatures inside the cloud gain concealment " +
                    "from attacks made by opponents that are not adjacent to them. You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = SmokeDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSpawnAreaEffect() {
                            m_AreaEffect = SmokeDomainBaseAbilityArea.ToReference<BlueprintAbilityAreaEffectReference>(),
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
                bp.AddComponent<AbilityAoERadius>(c => {
                    c.m_Radius = 5.Feet();
                    c.m_TargetType = TargetType.Any;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Other;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.AOE;
                });
                bp.m_Icon = CloudOfSmokeIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.AvailableMetamagic = Metamagic.Quicken;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = Helpers.CreateString("SmokeDomainBaseAbilitySeparatist.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var SmokeDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("SmokeDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { SmokeDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = SmokeDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { SmokeDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = SmokeDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Smoke Subdomain");
                bp.SetDescription("\nYou can blind and choke with smoke, those who dare not to burn. \nCloud of Smoke: " +
                    "As a standard action, you can create a 5-foot-radius cloud of smoke lasting 1 minute. Creatures inside the cloud take a –2 penalty on attack rolls " +
                    "and Perception skill checks for as long as they remain inside and for 1 round after exiting the cloud. Creatures inside the cloud gain concealment " +
                    "from attacks made by opponents that are not adjacent to them. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nFire Resistance: At 6th level, you gain resist fire 10. This resistance increases to 20 at 12th level. At 20th level, you gain immunity to fire.");
                bp.IsClassFeature = true;
            });

            var SmokeDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("SmokeDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = SmokeDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = SmokeDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = SmokeDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = SmokeDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = SmokeDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Smoke Subdomain");
                bp.SetDescription("\nYou can blind and choke with smoke, those who dare not to burn. \nCloud of Smoke: " +
                    "As a standard action, you can create a 5-foot-radius cloud of smoke lasting 1 minute. Creatures inside the cloud take a –2 penalty on attack rolls " +
                    "and Perception skill checks for as long as they remain inside and for 1 round after exiting the cloud. Creatures inside the cloud gain concealment " +
                    "from attacks made by opponents that are not adjacent to them. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nFire Resistance: At 6th level, you gain resist fire 10. This resistance increases to 20 at 12th level. At 20th level, you gain immunity to fire. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsBurningHands}burning hands{/g}, {g|SpellsGlitterdust}glitterdust{/g}, {g|SpellsStinkingCloud}stinking cloud{/g}, " +
                    "{g|SpellsControlledFireball}controlled fireball{/g}, {g|SpellsFlameStrike}flame strike{/g}, {g|SpellsSummonHugeFireElemental}summon huge fire elemental{/g}, " +
                    "{g|SpellsElementalBodyIVFire}elemental body IV (fire){/g}, {g|SpellsSummonElderFireElemental}summon elder fire elemental{/g}, {g|SpellsFieryBody}fiery body{/g}.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, SmokeDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(8, FireDomainGreaterFeature),
                    Helpers.LevelEntry(14, FireDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(SmokeDomainBaseFeatureSeparatist, FireDomainGreaterFeature, FireDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            SmokeDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                SmokeDomainProgression.ToReference<BlueprintFeatureReference>(),
                SmokeDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            SmokeDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = SmokeDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            SmokeDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = SmokeDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            SmokeDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = SmokeDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            SmokeDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = SmokeDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            SmokeDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = SmokeDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(SmokeDomainBaseAbility.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(SmokeDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Smoke Subdomain")) { return; }
            DomainTools.RegisterDomain(SmokeDomainProgression);
            DomainTools.RegisterSecondaryDomain(SmokeDomainProgressionSecondary);
            DomainTools.RegisterDruidDomain(SmokeDomainProgressionDruid);
            DomainTools.RegisterBlightDruidDomain(SmokeDomainProgressionDruid);
            DomainTools.RegisterDivineHunterDomain(SmokeDomainProgression);
            DomainTools.RegisterTempleDomain(SmokeDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(SmokeDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(SmokeDomainProgression, SmokeDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(SmokeDomainProgressionSeparatist);

        }
    }
}
