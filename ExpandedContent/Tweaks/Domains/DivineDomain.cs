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
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.ResourceLinks;
using UnityEngine;
using ExpandedContent.Tweaks.Components;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Designers.Mechanics.Buffs;

namespace ExpandedContent.Tweaks.Domains {
    internal class DivineDomain {

        public static void AddDivineDomain() {

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
            var MagicDomainGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("cf47e96abd88c9f418f8e67f5a14381f");
            var MagicDomainGreaterFeatureSeparatist = Resources.GetBlueprint<BlueprintFeature>("724216a6124d486fa55d7476db26bf1a");
            var AirBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("e1ff99dc3aeaa064e8eecde51c1c4773");
            var Kinetic_AirBlastLine00 = Resources.GetBlueprint<BlueprintProjectile>("03689858955c6bf409be06f35f09946a");
            var DivineVesselIcon = AssetLoader.LoadInternal("Skills", "Icon_DivineVessel.png");


            var DivineDomainBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("DivineDomainBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });


            var DivineDomainBaseBoonBuff = Helpers.CreateBuff("DivineDomainBaseBoonBuff", bp => {
                bp.SetName("Divine Vessel - Boon");
                bp.SetDescription("This boon grants a +2 bonus on the next attack roll, skill check, or ability check made before the end of their next turn.");
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.AdditionalAttackBonus;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 2
                    };
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddInitiatorAttackRollTrigger>(c => {
                    c.OnlyHit = false;
                    c.CriticalHit = false;
                    c.SneakAttack = false;
                    c.OnOwner = false;
                    c.CheckWeapon = false;
                    c.WeaponCategory = WeaponCategory.UnarmedStrike;
                    c.AffectFriendlyTouchSpells = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.AddComponent<BuffAllSkillsBonus>(c => {
                    c.Value = 2;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Multiplier = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 1
                    };
                });
                bp.AddComponent<AddInitiatorSkillRollTrigger>(c => {
                    c.OnlySuccess = false;
                    c.Skill = StatType.Unknown;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.AddComponent<AddInitiatorPartySkillRollTrigger>(c => {
                    c.OnlySuccess = false;
                    c.Skill = StatType.Unknown;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.SaveFortitude;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value= 2
                    };
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.SaveFortitude;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 2
                    };
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Stat = StatType.SaveWill;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 2
                    };
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddInitiatorSavingThrowTrigger>(c => {
                    c.OnlyPass = false;
                    c.OnlyFail = false;
                    c.SpecificSave = false;
                    c.ChooseSave = SavingThrowType.Fortitude;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.m_Icon = DivineVesselIcon;
                bp.Stacking = StackingType.Replace;
            });

            var DivineDomainBaseBoonAbility = Helpers.CreateBlueprint<BlueprintAbility>("DivineDomainBaseBoonAbility", bp => {
                bp.SetName("Divine Vessel - Boon");
                bp.SetDescription("");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DivineDomainBaseBoonBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 2,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = new Feet() { m_Value = 15 };
                    c.m_TargetType = TargetType.Ally;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker() { };
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "d119d19888a8f964b8acc5dfce6ea9e9" };
                    c.Time = AbilitySpawnFxTime.OnStart;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetFriends = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Immediate;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.DisableLog = true;
            });
            var DivineDomainBaseBoonAbilitySpawnFx = DivineDomainBaseBoonAbility.GetComponent<AbilitySpawnFx>();
            DivineDomainBaseBoonAbilitySpawnFx.PrefabLink = DivineDomainBaseBoonAbilitySpawnFx.PrefabLink.CreateDynamicProxy(pfl => {
                Main.Log($"Editing: {pfl}");
                pfl.name = "DivineDomainBase_15feetAoE";
                Main.Log($"{FxDebug.DumpGameObject(pfl.gameObject)}");
                var Shockwave_Rays = pfl.transform.Find("Ground/Shockwave_Rays").GetComponent<ParticleSystem>();
                Shockwave_Rays.transform.localScale = new(0.66f, 0.66f, 0.66f);
                var Shockwave = pfl.transform.Find("Ground/Shockwave").GetComponent<ParticleSystem>();
                Shockwave.transform.localScale = new(0.66f, 0.66f, 0.66f);
                var Sparks = pfl.transform.Find("Ground/Sparks").GetComponent<ParticleSystem>();
                Sparks.transform.localScale = new(0.66f, 1.0f, 0.66f);
                var Sparks_Small = pfl.transform.Find("Ground/Sparks_Small").GetComponent<ParticleSystem>();
                Sparks_Small.transform.localScale = new(0.66f, 1.0f, 0.66f);
                var Spiral00 = pfl.transform.Find("Ground/Spiral00").GetComponent<ParticleSystem>();
                Spiral00.transform.localScale = new(0.66f, 1.0f, 0.66f);
                var Spiral01 = pfl.transform.Find("Ground/Spiral01").GetComponent<ParticleSystem>();
                Spiral01.transform.localScale = new(0.66f, 1.0f, 0.66f);
            });


            var DivineDomainBaseSelfBuff = Helpers.CreateBuff("DivineDomainBaseSelfBuff", bp => {
                bp.SetName("Divine Vessel");
                bp.SetDescription("If you are targeted by a divine spell during this round, grant each ally within 15 feet of you a divine boon. " +
                    "This boon grants a +2 bonus on the next attack roll, skill check, or ability check made before the end of their next turn.");
                bp.AddComponent<ContextActionOnApplyingSpell>(c => {
                    c.m_AffectedSpellSource = ContextActionOnApplyingSpell.AffectedSpellSource.Divine;
                    c.ActionOnSelf = Helpers.CreateActionList(
                        new ContextActionCastSpell() {
                            m_Spell = DivineDomainBaseBoonAbility.ToReference<BlueprintAbilityReference>(),
                            OverrideDC = false,
                            DC = 0,
                            OverrideSpellLevel = false,
                            SpellLevel = 0,
                            CastByTarget = false,
                            LogIfCanNotTarget = false,
                            MarkAsChild = false
                        }
                        );
                });
                bp.m_Icon = DivineVesselIcon;
                bp.Stacking = StackingType.Replace;
            });

            var DivineDomainBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("DivineDomainBaseAbility", bp => {
                bp.SetName("Divine Vessel");
                bp.SetDescription("As a swift action you become a divine vessel for one round. If you are targeted by a divine spell during this round, " +
                    "grant each ally within 15 feet of you a divine boon. This boon grants a +2 bonus on the next attack roll, skill check, or ability check " +
                    "made before the end of their next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DivineDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DivineDomainBaseSelfBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.m_Icon = DivineVesselIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = Helpers.CreateString("DivineDomainBaseAbility.Duration", "1 round");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            //Spelllist
            var ColourSpraySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("91da41b9793a4624797921f221db653c");
            var BlessSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("90e59f4a4ada87243b7b3535a06d0638");
            var DispelMagicSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("92681f181b507b34ea87018e8f7a528a");
            var ProtectionFromEnergyCommunalSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("76a629d019275b94184a1a8733cac45e");
            var CleanseSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("be2062d6d85f4634ea4f26e9e858c3b8");
            var GreaterDispelMagicSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f0f761b808dc4b149b08eaf44b99f633");
            var ResurrectionSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("80a1a388ee938aa4e90d427ce9a7a3e9");
            var ProtectionFromSpellsSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("42aa71adc7343714fa92e471baa98d42");
            var HeroicInvocationSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("43740dab07286fe4aa00a6ee104ce7c1");
            var DivineDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("DivineDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ColourSpraySpell
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BlessSpell
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            DispelMagicSpell
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ProtectionFromEnergyCommunalSpell
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            CleanseSpell
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            GreaterDispelMagicSpell
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ResurrectionSpell
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ProtectionFromSpellsSpell
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HeroicInvocationSpell
                        }
                    },
                };
            });
            var DivineDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("DivineDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = DivineDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var DivineDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("DivineDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        DivineDomainBaseAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DivineDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = DivineDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Divine Subdomain");
                bp.SetDescription("\nMany worshippers harness the magic of the divine, you wield the magic of divinity itself. " +
                    "\nDivine Vessel: As a swift action you become a divine vessel for one round. If you are targeted by a divine spell during this round, " +
                    "grant each ally within 15 feet of you a divine boon. This boon grants a +2 bonus on the next attack roll, skill check, or ability check " +
                    "made before the end of their next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nDispelling Touch: At 8th level, you can use a {g|SpellsDispelMagicTarget}targeted dispel magic{/g} effect as a melee {g|Encyclopedia:TouchAttack}touch attack{/g}. " +
                    "You can use this ability once per day at 8th level and one additional time per day for every four levels in the class that gave you access to this domain beyond 8th.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var DivineDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("DivineDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = DivineDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            // Main Blueprint
            var DivineDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("DivineDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DivineDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = DivineDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = DivineDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Divine Subdomain");
                bp.SetDescription("\nMany worshippers harness the magic of the divine, you wield the magic of divinity itself. " +
                    "\nDivine Vessel: As a swift action you become a divine vessel for one round. If you are targeted by a divine spell during this round, " +
                    "grant each ally within 15 feet of you a divine boon. This boon grants a +2 bonus on the next attack roll, skill check, or ability check " +
                    "made before the end of their next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nDispelling Touch: At 8th level, you can use a {g|SpellsDispelMagicTarget}targeted dispel magic{/g} effect as a melee {g|Encyclopedia:TouchAttack}touch attack{/g}. " +
                    "You can use this ability once per day at 8th level and one additional time per day for every four levels in the class that gave you access to this domain beyond 8th. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsColorSpray}color spray{/g}, {g|SpellsBless}bless{/g}, {g|SpellsDispelMagic}dispel magic{/g}, " +
                    "{g|SpellsCommunalProtectionFromEnergy}protection from energy, communal{/g}, {g|SpellsCleanse}cleanse{/g}, {g|SpellsGreaterDispelMagic}dispel magic, greater{/g}, " +
                    "{g|SpellsResurrection}resurrection{/g}, {g|SpellsProtectionFromSpells}protection from spells{/g}, {g|SpellsHeroicInvocatiom}heroic invocation{/g}.");
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
                    Helpers.LevelEntry(1, DivineDomainBaseFeature),
                    Helpers.LevelEntry(8, MagicDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(DivineDomainBaseFeature, MagicDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var DivineDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("DivineDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DivineDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DivineDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Divine Subdomain");
                bp.SetDescription("\nMany worshippers harness the magic of the divine, you wield the magic of divinity itself. " +
                    "\nDivine Vessel: As a swift action you become a divine vessel for one round. If you are targeted by a divine spell during this round, " +
                    "grant each ally within 15 feet of you a divine boon. This boon grants a +2 bonus on the next attack roll, skill check, or ability check " +
                    "made before the end of their next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nDispelling Touch: At 8th level, you can use a {g|SpellsDispelMagicTarget}targeted dispel magic{/g} effect as a melee {g|Encyclopedia:TouchAttack}touch attack{/g}. " +
                    "You can use this ability once per day at 8th level and one additional time per day for every four levels in the class that gave you access to this domain beyond 8th. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsColorSpray}color spray{/g}, {g|SpellsBless}bless{/g}, {g|SpellsDispelMagic}dispel magic{/g}, " +
                    "{g|SpellsCommunalProtectionFromEnergy}protection from energy, communal{/g}, {g|SpellsCleanse}cleanse{/g}, {g|SpellsGreaterDispelMagic}dispel magic, greater{/g}, " +
                    "{g|SpellsResurrection}resurrection{/g}, {g|SpellsProtectionFromSpells}protection from spells{/g}, {g|SpellsHeroicInvocatiom}heroic invocation{/g}.");
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
                    Helpers.LevelEntry(1, DivineDomainBaseFeature),
                    Helpers.LevelEntry(8, MagicDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(DivineDomainBaseFeature, MagicDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });


            //Separatist versions
            var DivineDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("DivineDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var DivineDomainBaseResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("DivineDomainBaseResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 2,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });

            var DivineDomainBaseAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("DivineDomainBaseAbilitySeparatist", bp => {
                bp.SetName("Divine Vessel");
                bp.SetDescription("As a swift action you become a divine vessel for one round. If you are targeted by a divine spell during this round, " +
                    "grant each ally within 15 feet of you a divine boon. This boon grants a +2 bonus on the next attack roll, skill check, or ability check " +
                    "made before the end of their next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DivineDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DivineDomainBaseSelfBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.m_Icon = DivineVesselIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = Helpers.CreateString("DivineDomainBaseAbilitySeparatist.Duration", "1 round");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var DivineDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("DivineDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        DivineDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DivineDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = DivineDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Divine Subdomain");
                bp.SetDescription("\nMany worshippers harness the magic of the divine, you wield the magic of divinity itself. " +
                    "\nDivine Vessel: As a swift action you become a divine vessel for one round. If you are targeted by a divine spell during this round, " +
                    "grant each ally within 15 feet of you a divine boon. This boon grants a +2 bonus on the next attack roll, skill check, or ability check " +
                    "made before the end of their next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nDispelling Touch: At 8th level, you can use a {g|SpellsDispelMagicTarget}targeted dispel magic{/g} effect as a melee {g|Encyclopedia:TouchAttack}touch attack{/g}. " +
                    "You can use this ability once per day at 8th level and one additional time per day for every four levels in the class that gave you access to this domain beyond 8th.");
                bp.IsClassFeature = true;
            });

            var DivineDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("DivineDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DivineDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DivineDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DivineDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DivineDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DivineDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Divine Subdomain");
                bp.SetDescription("\nMany worshippers harness the magic of the divine, you wield the magic of divinity itself. " +
                    "\nDivine Vessel: As a swift action you become a divine vessel for one round. If you are targeted by a divine spell during this round, " +
                    "grant each ally within 15 feet of you a divine boon. This boon grants a +2 bonus on the next attack roll, skill check, or ability check " +
                    "made before the end of their next turn. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                    "\nDispelling Touch: At 8th level, you can use a {g|SpellsDispelMagicTarget}targeted dispel magic{/g} effect as a melee {g|Encyclopedia:TouchAttack}touch attack{/g}. " +
                    "You can use this ability once per day at 8th level and one additional time per day for every four levels in the class that gave you access to this domain beyond 8th. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsColorSpray}color spray{/g}, {g|SpellsBless}bless{/g}, {g|SpellsDispelMagic}dispel magic{/g}, " +
                    "{g|SpellsCommunalProtectionFromEnergy}protection from energy, communal{/g}, {g|SpellsCleanse}cleanse{/g}, {g|SpellsGreaterDispelMagic}dispel magic, greater{/g}, " +
                    "{g|SpellsResurrection}resurrection{/g}, {g|SpellsProtectionFromSpells}protection from spells{/g}, {g|SpellsHeroicInvocatiom}heroic invocation{/g}.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, DivineDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(8, MagicDomainGreaterFeatureSeparatist)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(DivineDomainBaseFeatureSeparatist, MagicDomainGreaterFeatureSeparatist)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            DivineDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                DivineDomainProgression.ToReference<BlueprintFeatureReference>(),
                DivineDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            DivineDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = DivineDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            DivineDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = DivineDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            DivineDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = DivineDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            DivineDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = DivineDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            DivineDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = DivineDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            if (ModSettings.AddedContent.Domains.IsDisabled("Divine Subdomain")) { return; }
            DomainTools.RegisterDomain(DivineDomainProgression);
            DomainTools.RegisterSecondaryDomain(DivineDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(DivineDomainProgression);
            DomainTools.RegisterTempleDomain(DivineDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(DivineDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(DivineDomainProgression, DivineDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(DivineDomainProgressionSeparatist);

        }
    }
}
