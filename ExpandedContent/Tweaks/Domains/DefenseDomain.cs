using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.ElementsSystem;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.RuleSystem;
using Kingmaker.Utility;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.RuleSystem.Rules.Damage;
using ExpandedContent.Config;
using ExpandedContent.Tweaks.Spells;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.UnitLogic.Parts;

namespace ExpandedContent.Tweaks.Domains {
    internal class DefenseDomain {

        public static void AddDefenseDomain() {

            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var ProtectionDomainGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("e2e9d41bfa7aa364592b9d57dd74c9db");
            var ShieldOfFortificationIcon = AssetLoader.LoadInternal("Skills", "Icon_ShieldOfFortification.jpg");
            var MadnessDomainGreaterArea = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("19ee79b1da25ea049ba4fea92c2a4025");
            var ProtectionDomainBaseSelfBuff = Resources.GetBlueprint<BlueprintBuff>("74a4fb45f23705d4db2784d16eb93138");











            var DefenseDomainBaseClassFeature = Helpers.CreateBlueprint<BlueprintFeature>("DefenseDomainBaseClassFeature", bp => {
                bp.SetName("Defense Domain Resistance Bonus");
                bp.SetDescription("Your faith is your greatest source of protection, and you can use that faith to defend others. In addition, you receive a +1 resistance bonus on saving throws. " +
                    "This bonus increases by 1 for every 5 levels you possess.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ProtectionDomainBaseSelfBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = false;
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
            });



            var DefenseDomainBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("DefenseDomainBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByStat = false,
                };
            });

            var DefenseDomainBaseAreaBuff = Helpers.CreateBuff("DefenseDomainBaseAreaBuff", bp => {
                bp.SetName("Deflection Aura");
                bp.SetDescription("Once each day, you can emit a 30-foot aura for a number of rounds equal to your cleric level. Allies within the aura gain a +2 deflection bonus to AC and combat maneuver defense.");
                bp.m_Icon = ShieldOfFortificationIcon;
                bp.AddComponent<AddStatBonusAbilityValue>(c => {
                    c.Descriptor = ModifierDescriptor.Deflection;
                    c.Stat = StatType.AC;
                    c.Value = 2;
                });
                bp.AddComponent<AddStatBonusAbilityValue>(c => {
                    c.Descriptor = ModifierDescriptor.Deflection;
                    c.Stat = StatType.AdditionalCMD;
                    c.Value = 2;
                });                
            });

            var DefenseDomainBaseArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("DefenseDomainBaseArea", bp => {
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Ally;
                bp.SpellResistance = false;
                bp.AggroEnemies = false;
                bp.AffectEnemies = false;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = 30.Feet();
                bp.Fx = MadnessDomainGreaterArea.Fx;
                bp.AddComponent<AbilityAreaEffectBuff>(c => {
                    c.Condition = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionIsAlly()
                        }
                    };
                    c.m_Buff = DefenseDomainBaseAreaBuff.ToReference<BlueprintBuffReference>();
                });

            });

            var DefenseDomainBaseBuff = Helpers.CreateBuff("DefenseDomainBaseBuff", bp => {
                bp.SetName("Deflection Aura");
                bp.SetDescription("Once each day, you can emit a 30-foot aura for a number of rounds equal to your cleric level. Allies within the aura gain a +2 deflection bonus to AC and combat maneuver defense.");
                bp.m_Icon = ShieldOfFortificationIcon;
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = DefenseDomainBaseArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.m_AllowNonContextActions = false;
            });

            var DefenseDomainBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("DefenseDomainBaseAbility", bp => {
                bp.SetName("Deflection Aura");
                bp.SetDescription("Once each day, you can emit a 30-foot aura for a number of rounds equal to your cleric level. Allies within the aura gain a +2 deflection bonus to AC and combat maneuver defense.");
                bp.m_Icon = ShieldOfFortificationIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DefenseDomainBaseBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    Value = 0,
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DefenseDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { TempleChampionArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("DefenseDomainBaseAbility.Duration", "1 round/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            //Spelllist
            var ShieldSpell = Resources.GetBlueprint<BlueprintAbility>("ef768022b0785eb43a18969903c537c4");
            var BarkskinSpell = Resources.GetBlueprint<BlueprintAbility>("5b77d7cc65b8ab74688e74a37fc2f553");
            var ProtectionFromEnergySpell = Resources.GetBlueprint<BlueprintAbility>("d2f116cfe05fcdd4a94e80143b67046f");
            var ProtectionFromEnergyCommunalSpell = Resources.GetBlueprint<BlueprintAbility>("76a629d019275b94184a1a8733cac45e");
            var SpellResistanceSpell = Resources.GetBlueprint<BlueprintAbility>("0a5ddfbcfb3989543ac7c936fc256889");
            var StoneskinCommunalSpell = Resources.GetBlueprint<BlueprintAbility>("7c5d556b9a5883048bf030e20daebe31");
            var ParticulateFormSpell = Resources.GetModBlueprint<BlueprintAbility>("ParticulateFormAbility");
            var ProtectionFromSpellsSpell = Resources.GetBlueprint<BlueprintAbility>("42aa71adc7343714fa92e471baa98d42");
            var SeamantleSpell = Resources.GetBlueprint<BlueprintAbility>("7ef49f184922063499b8f1346fb7f521");
            var DefenseDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("DefenseDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ShieldSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BarkskinSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ProtectionFromEnergySpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ProtectionFromEnergyCommunalSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SpellResistanceSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            StoneskinCommunalSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ParticulateFormSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ProtectionFromSpellsSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SeamantleSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });
            var DefenseDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("DefenseDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = DefenseDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var DefenseDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("DefenseDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DefenseDomainBaseAbility.ToReference<BlueprintUnitFactReference>(), DefenseDomainBaseClassFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DefenseDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { DefenseDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = DefenseDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Defense Subdomain");
                bp.SetDescription("\nYour faith is your fortitude, inspiring all with the strength to defend aginst any blow. In addition, you receive a +1 resistance {g|Encyclopedia:Bonus}bonus{/g} " +
                    "on {g|Encyclopedia:Saving_Throw}saving throws{/g}. This bonus increases by 1 for every 5 levels you possess in the class that gave you access to this domain.\nDeflection Aura: " +
                    "Once each day, you can emit a 30-foot aura for a number of rounds equal to your cleric level. Allies within the aura gain a +2 deflection bonus to AC and combat maneuver defense." +
                    "\nAura of Protection: At 8th level, you can emit a 30-foot aura of protection for a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class " +
                    "that gave you access to this domain. You and your allies within this aura gain a +1 deflection bonus to {g|Encyclopedia:Armor_Class}AC{/g} and resistance 5 against all elements " +
                    "(acid, cold, electricity, fire, and sonic). The deflection bonus increases by +1 for every four levels you possess in the class that gave you access to this domain beyond 8th. " +
                    "At 14th level, the resistance against all elements increases to 10. These rounds do not need to be consecutive.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var DefenseDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("DefenseDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = DefenseDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            // Main Blueprint
            var DefenseDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("DefenseDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DefenseDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = DefenseDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = DefenseDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Defense Subdomain");
                bp.SetName("Defense Subdomain");
                bp.SetDescription("\nYour faith is your fortitude, inspiring all with the strength to defend aginst any blow. In addition, you receive a +1 resistance {g|Encyclopedia:Bonus}bonus{/g} " +
                    "on {g|Encyclopedia:Saving_Throw}saving throws{/g}. This bonus increases by 1 for every 5 levels you possess in the class that gave you access to this domain.\nDeflection Aura: " +
                    "Once each day, you can emit a 30-foot aura for a number of rounds equal to your cleric level. Allies within the aura gain a +2 deflection bonus to AC and combat maneuver defense." +
                    "\nAura of Protection: At 8th level, you can emit a 30-foot aura of protection for a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class " +
                    "that gave you access to this domain. You and your allies within this aura gain a +1 deflection bonus to {g|Encyclopedia:Armor_Class}AC{/g} and resistance 5 against all elements " +
                    "(acid, cold, electricity, fire, and sonic). The deflection bonus increases by +1 for every four levels you possess in the class that gave you access to this domain beyond 8th. " +
                    "At 14th level, the resistance against all elements increases to 10. These rounds do not need to be consecutive.\nDomain {g|Encyclopedia:Spell}Spells{/g}: shield, " +
                    "barkskin, protection from energy, communal protection from energy, {g|Encyclopedia:Spell_Resistance}spell resistance{/g}, communal stoneskin, " +
                    "particulate form, protection from spells, seamantle.");
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
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = -2
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, DefenseDomainBaseFeature),
                    Helpers.LevelEntry(8, ProtectionDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(DefenseDomainBaseFeature, ProtectionDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var DefenseDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("DefenseDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DefenseDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = DefenseDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Defense Subdomain");
                bp.SetDescription("\nYour faith is your fortitude, inspiring all with the strength to defend aginst any blow. In addition, you receive a +1 resistance {g|Encyclopedia:Bonus}bonus{/g} " +
                    "on {g|Encyclopedia:Saving_Throw}saving throws{/g}. This bonus increases by 1 for every 5 levels you possess in the class that gave you access to this domain.\nDeflection Aura: " +
                    "Once each day, you can emit a 30-foot aura for a number of rounds equal to your cleric level. Allies within the aura gain a +2 deflection bonus to AC and combat maneuver defense." +
                    "\nAura of Protection: At 8th level, you can emit a 30-foot aura of protection for a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class " +
                    "that gave you access to this domain. You and your allies within this aura gain a +1 deflection bonus to {g|Encyclopedia:Armor_Class}AC{/g} and resistance 5 against all elements " +
                    "(acid, cold, electricity, fire, and sonic). The deflection bonus increases by +1 for every four levels you possess in the class that gave you access to this domain beyond 8th. " +
                    "At 14th level, the resistance against all elements increases to 10. These rounds do not need to be consecutive.\nDomain {g|Encyclopedia:Spell}Spells{/g}: shield, " +
                    "barkskin, protection from energy, communal protection from energy, {g|Encyclopedia:Spell_Resistance}spell resistance{/g}, communal stoneskin, " +
                    "particulate form, protection from spells, seamantle.");
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
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, DefenseDomainBaseFeature),
                    Helpers.LevelEntry(8, ProtectionDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(DefenseDomainBaseFeature, ProtectionDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            DefenseDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                DefenseDomainProgression.ToReference<BlueprintFeatureReference>(),
                DefenseDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            DefenseDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = DefenseDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            DefenseDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = DefenseDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(DefenseDomainBaseAbility.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Defense Subdomain")) { return; }
            DomainTools.RegisterDomain(DefenseDomainProgression);
            DomainTools.RegisterSecondaryDomain(DefenseDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(DefenseDomainProgression);
            DomainTools.RegisterTempleDomain(DefenseDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(DefenseDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(DefenseDomainProgression, DefenseDomainProgressionSecondary);
        }
    }
}
