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

namespace ExpandedContent.Tweaks.Domains {
    internal class CavesDomain {

        public static void AddCavesDomain() {

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
            var EarthDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("3ff40918d33219942929f0dbfe5d1dee");
            var EarthDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("40e2c67afaecf4d47a60c619e2228d5a");
            var Tremorsence = Resources.GetBlueprint<BlueprintFeature>("6e668702fdc53c343a0363813683346e");
            var EarthBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("4172d92c598de1d47aa2c0dd51c05e24");
            //CavesDomainGreaterBuff
            var CavesDomainGreaterBuff = Helpers.CreateBuff("CavesDomainGreaterBuff", bp => {
                bp.SetName("Tunnel Runner");
                bp.SetDescription("While underground you gain an insight bonus equal to your cleric level on Stealth and Perception skill checks and an " +
                    "insight bonus equal to your Wisdom modifier on initiative checks. You can use this ability for 1 minute per day per cleric level you " +
                    "possess. These minutes do not need to be consecutive, but they must be spent in " +
                    "1-minute increments.");
                bp.m_Icon = Tremorsence.Icon;
                bp.AddComponent<AddContextStatBonusInTerrain>(c => {
                    c.Terrain = AreaSetting.Underground;
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SkillStealth;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.StatBonus,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None,
                    };
                });
                bp.AddComponent<AddContextStatBonusInTerrain>(c => {
                    c.Terrain = AreaSetting.Underground;
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SkillPerception;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.StatBonus,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None,
                    };
                });
                // Config for Stealth and Perception
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { 
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        DruidClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                });
                bp.AddComponent<AddContextStatBonusInTerrain>(c => {
                    c.Terrain = AreaSetting.Underground;
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.Initiative;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None,
                    };
                    c.HasMinimal = true;
                    c.Minimal = 0;
                });
                // Config for Initiative
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Wisdom;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                });
                bp.m_AllowNonContextActions = false;                
            });
            //CavesDomainGreaterResource
            var CavesDomainGreaterResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("CavesDomainGreaterResource", bp => {
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
            //CavesDomainGreaterAbility
            var CavesDomainGreaterAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("CavesDomainGreaterAbility", bp => {
                bp.SetName("Tunnel Runner");
                bp.SetDescription("While underground you gain an insight bonus equal to your cleric level on Stealth and Perception skill checks and an " +
                    "insight bonus equal to your Wisdom modifier on initiative checks. You can use this ability for 1 minute per day per cleric level you " +
                    "possess. These minutes do not need to be consecutive, but they must be spent in " +
                    "1-minute increments.");
                bp.m_Icon = Tremorsence.Icon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = CavesDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = CavesDomainGreaterBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
                bp.DeactivateIfCombatEnded = false;                
            });
            //CavesDomainGreaterFeature
            var CavesDomainGreaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("CavesDomainGreaterFeature", bp => {
                bp.SetName("Tunnel Runner");
                bp.SetDescription("At 8th level, while underground you may gain an insight bonus equal to your cleric level on Stealth and Perception skill checks and an " +
                    "insight bonus equal to your Wisdom modifier on initiative checks. You can use this ability for 1 minute per day per cleric level you " +
                    "possess. These minutes do not need to be consecutive, but they must be spent in " +
                    "1-minute increments.");
                bp.m_Icon = Tremorsence.Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = CavesDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { CavesDomainGreaterAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;

                
            });

            //Spelllist
            var StoneFistSpell = Resources.GetBlueprint<BlueprintAbility>("85067a04a97416949b5d1dbf986d93f3");
            var CreatePitSpell = Resources.GetBlueprint<BlueprintAbility>("29ccc62632178d344ad0be0865fd3113");
            var SpikedPitSpell = Resources.GetBlueprint<BlueprintAbility>("46097f610219ac445b4d6403fc596b9f");
            var SpikeStonesSpell = Resources.GetBlueprint<BlueprintAbility>("d1afa8bc28c99104da7d784115552de5");
            var AcidicSpraySpell = Resources.GetBlueprint<BlueprintAbility>("c543eef6d725b184ea8669dd09b3894c");
            var HungryPitSpell = Resources.GetBlueprint<BlueprintAbility>("f63f4d1806b78604a952b3958892ce1c");
            var ElementalBodyIVEarthSpell = Resources.GetBlueprint<BlueprintAbility>("facdc8851a0b3f44a8bed50f0199b83c");
            var IronBodySpell = Resources.GetBlueprint<BlueprintAbility>("198fcc43490993f49899ed086fe723c1");
            var ElementalSwarmEarthSpell = Resources.GetBlueprint<BlueprintAbility>("a6c41f10be92dec488276ab079a296c8");
            var CavesDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("CavesDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            StoneFistSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            CreatePitSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SpikedPitSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SpikeStonesSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            AcidicSpraySpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HungryPitSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ElementalBodyIVEarthSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            IronBodySpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ElementalSwarmEarthSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var CavesDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("CavesDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = CavesDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var CavesDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("CavesDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { EarthDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = EarthDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { EarthDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = CavesDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Caves Subdomain");
                bp.SetDescription("\nYou have mastery over rock and stone, can manifest vast pits, and command earth creatures.\nAcid Dart: As a " +
                    "{g|Encyclopedia:Standard_Actions}standard action{/g}, you can unleash an acid dart targeting any foe within 30 feet as a ranged " +
                    "{g|Encyclopedia:TouchAttack}touch attack{/g}. This acid dart deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}acid damage{/g}" +
                    "+ 1 point for every two levels you possess in the class that gave you access to this domain. You can use this ability a number of " +
                    "times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nTunnel Runner: At 8th level, while underground you may gain an insight " +
                    "bonus equal to your cleric level on Stealth and Perception skill checks and an insight bonus equal to your Wisdom modifier on initiative checks. " +
                    "You can use this ability for 1 minute per day per cleric level you possess. These minutes do not need to be consecutive, but they must be spent in " +
                    "1-minute increments.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var CavesDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("CavesDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = CavesDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var CavesDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("CavesDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = CavesDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = CavesDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = CavesDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Caves Subdomain");
                bp.SetDescription("\nYou have mastery over rock and stone, can manifest vast pits, and command earth creatures.\nAcid Dart: As a " +
                    "{g|Encyclopedia:Standard_Actions}standard action{/g}, you can unleash an acid dart targeting any foe within 30 feet as a ranged " +
                    "{g|Encyclopedia:TouchAttack}touch attack{/g}. This acid dart deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}acid damage{/g}" +
                    "+ 1 point for every two levels you possess in the class that gave you access to this domain. You can use this ability a number of " +
                    "times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nTunnel Runner: At 8th level, while underground you may gain an insight " +
                    "bonus equal to your cleric level on Stealth and Perception skill checks and an insight bonus equal to your Wisdom modifier on initiative checks. " +
                    "You can use this ability for 1 minute per day per cleric level you possess. These minutes do not need to be consecutive, but they must be spent in " +
                    "1-minute increments.\nDomain Spells: stone fist, createpit, spiked pit, spike stones, acidic spray, hungry pit, elemental body IV (earth), " +
                    "iron body, elemental swarm (earth).");
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
                    Helpers.LevelEntry(1, CavesDomainBaseFeature),
                    Helpers.LevelEntry(8, CavesDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(CavesDomainBaseFeature, CavesDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var CavesDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("CavesDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = CavesDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = CavesDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Caves Subdomain");
                bp.SetDescription("\nYou have mastery over rock and stone, can manifest vast pits, and command earth creatures.\nAcid Dart: As a " +
                    "{g|Encyclopedia:Standard_Actions}standard action{/g}, you can unleash an acid dart targeting any foe within 30 feet as a ranged " +
                    "{g|Encyclopedia:TouchAttack}touch attack{/g}. This acid dart deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}acid damage{/g}" +
                    "+ 1 point for every two levels you possess in the class that gave you access to this domain. You can use this ability a number of " +
                    "times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nTunnel Runner: At 8th level, while underground you may gain an insight " +
                    "bonus equal to your cleric level on Stealth and Perception skill checks and an insight bonus equal to your Wisdom modifier on initiative checks. " +
                    "You can use this ability for 1 minute per day per cleric level you possess. These minutes do not need to be consecutive, but they must be spent in " +
                    "1-minute increments.\nDomain Spells: stone fist, createpit, spiked pit, spike stones, acidic spray, hungry pit, elemental body IV (earth), " +
                    "iron body, elemental swarm (earth).");
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
                    Helpers.LevelEntry(1, CavesDomainBaseFeature),
                    Helpers.LevelEntry(8, CavesDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(CavesDomainBaseFeature, CavesDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // CavesDomainSpellListFeatureDruid
            var CavesDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("CavesDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = CavesDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            // CavesDomainProgressionDruid
            var CavesDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("CavesDomainProgressionDruid", bp => {
                bp.SetName("Caves Subdomain");
                bp.SetDescription("\nYou have mastery over rock and stone, can manifest vast pits, and command earth creatures.\nAcid Dart: As a " +
                    "{g|Encyclopedia:Standard_Actions}standard action{/g}, you can unleash an acid dart targeting any foe within 30 feet as a ranged " +
                    "{g|Encyclopedia:TouchAttack}touch attack{/g}. This acid dart deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}acid damage{/g}" +
                    "+ 1 point for every two levels you possess in the class that gave you access to this domain. You can use this ability a number of " +
                    "times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nTunnel Runner (Su): At 6th level, as a standard action, " +
                    "you can create a 30-foot aura of gale-like winds that slows the progress of enemies. Creatures in the aura cannot take a 5-foot step " +
                    "and treat it as as difficult terrain. You can use this ability for a number of rounds per day equal to your cleric level. The rounds " +
                    "do not need to be consecutive.\nDomain Spells: stone fist, createpit, spiked pit, spike stones, acidic spray, hungry pit, elemental body IV (earth), " +
                    "iron body, elemental swarm (earth).");
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
                    Helpers.LevelEntry(1, CavesDomainBaseFeature, CavesDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(8, CavesDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions
            var EarthDomainBaseAbilitySeparatist = Resources.GetBlueprint<BlueprintAbility>("80b9520ca51a45dabda33063b4536533");
            var EarthDomainBaseResourceSeparatist = Resources.GetBlueprint<BlueprintAbilityResource>("a039cb04bce34432826ad1c70eaf38ed");

            var CavesDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("CavesDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var CavesDomainGreaterResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("CavesDomainGreaterResourceSeparatist", bp => {
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

            var CavesDomainGreaterBuffSeparatist = Helpers.CreateBuff("CavesDomainGreaterBuffSeparatist", bp => {//needs redoing
                bp.SetName("Tunnel Runner");
                bp.SetDescription("While underground you gain an insight bonus equal to your cleric level on Stealth and Perception skill checks and an " +
                    "insight bonus equal to your Wisdom modifier on initiative checks. You can use this ability for 1 minute per day per cleric level you " +
                    "possess. These minutes do not need to be consecutive, but they must be spent in " +
                    "1-minute increments.");
                bp.m_Icon = Tremorsence.Icon;
                bp.AddComponent<AddContextStatBonusInTerrain>(c => {
                    c.Terrain = AreaSetting.Underground;
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SkillStealth;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.StatBonus,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None,
                    };
                });
                bp.AddComponent<AddContextStatBonusInTerrain>(c => {
                    c.Terrain = AreaSetting.Underground;
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.SkillPerception;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.StatBonus,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None,
                    };
                });
                // Config for Stealth and Perception
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.DamageDice
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { 
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        DruidClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_StartLevel = 3;
                    c.m_StepLevel = 1;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                });
                bp.AddComponent<AddContextStatBonusInTerrain>(c => {
                    c.Terrain = AreaSetting.Underground;
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.Initiative;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None,
                    };
                    c.HasMinimal = true;
                    c.Minimal = 0;
                });
                // Config for Initiative
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.StatBonus;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.DamageBonus
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = -1
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Wisdom;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                });
                bp.m_AllowNonContextActions = false;
            });

            var CavesDomainGreaterAbilitySeparatist = Helpers.CreateBlueprint<BlueprintActivatableAbility>("CavesDomainGreaterAbilitySeparatist", bp => {
                bp.SetName("Tunnel Runner");
                bp.SetDescription("While underground you gain an insight bonus equal to your cleric level on Stealth and Perception skill checks and an " +
                    "insight bonus equal to your Wisdom modifier on initiative checks. You can use this ability for 1 minute per day per cleric level you " +
                    "possess. These minutes do not need to be consecutive, but they must be spent in " +
                    "1-minute increments.");
                bp.m_Icon = Tremorsence.Icon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = CavesDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = CavesDomainGreaterBuffSeparatist.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
                bp.DeactivateIfCombatEnded = false;

            });

            var CavesDomainGreaterFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("CavesDomainGreaterFeatureSeparatist", bp => {
                bp.SetName("Tunnel Runner");
                bp.SetDescription("At 8th level, while underground you may gain an insight bonus equal to your cleric level on Stealth and Perception skill checks and an " +
                    "insight bonus equal to your Wisdom modifier on initiative checks. You can use this ability for 1 minute per day per cleric level you " +
                    "possess. These minutes do not need to be consecutive, but they must be spent in " +
                    "1-minute increments.");
                bp.m_Icon = Tremorsence.Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = CavesDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { CavesDomainGreaterAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var CavesDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("CavesDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { EarthDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = EarthDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { EarthDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = CavesDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Caves Subdomain");
                bp.SetDescription("\nYou have mastery over rock and stone, can manifest vast pits, and command earth creatures.\nAcid Dart: As a " +
                    "{g|Encyclopedia:Standard_Actions}standard action{/g}, you can unleash an acid dart targeting any foe within 30 feet as a ranged " +
                    "{g|Encyclopedia:TouchAttack}touch attack{/g}. This acid dart deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}acid damage{/g}" +
                    "+ 1 point for every two levels you possess in the class that gave you access to this domain. You can use this ability a number of " +
                    "times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nTunnel Runner: At 8th level, while underground you may gain an insight " +
                    "bonus equal to your cleric level on Stealth and Perception skill checks and an insight bonus equal to your Wisdom modifier on initiative checks. " +
                    "You can use this ability for 1 minute per day per cleric level you possess. These minutes do not need to be consecutive, but they must be spent in " +
                    "1-minute increments.");
                bp.IsClassFeature = true;
            });

            var CavesDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("CavesDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = CavesDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = CavesDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = CavesDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = CavesDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = CavesDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Caves Subdomain");
                bp.SetDescription("\nYou have mastery over rock and stone, can manifest vast pits, and command earth creatures.\nAcid Dart: As a " +
                    "{g|Encyclopedia:Standard_Actions}standard action{/g}, you can unleash an acid dart targeting any foe within 30 feet as a ranged " +
                    "{g|Encyclopedia:TouchAttack}touch attack{/g}. This acid dart deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}acid damage{/g}" +
                    "+ 1 point for every two levels you possess in the class that gave you access to this domain. You can use this ability a number of " +
                    "times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nTunnel Runner: At 8th level, while underground you may gain an insight " +
                    "bonus equal to your cleric level on Stealth and Perception skill checks and an insight bonus equal to your Wisdom modifier on initiative checks. " +
                    "You can use this ability for 1 minute per day per cleric level you possess. These minutes do not need to be consecutive, but they must be spent in " +
                    "1-minute increments.\nDomain Spells: stone fist, createpit, spiked pit, spike stones, acidic spray, hungry pit, elemental body IV (earth), " +
                    "iron body, elemental swarm (earth).");
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
                    Helpers.LevelEntry(1, CavesDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(10, CavesDomainGreaterFeatureSeparatist)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(CavesDomainBaseFeatureSeparatist, CavesDomainGreaterFeatureSeparatist)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            CavesDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                CavesDomainProgression.ToReference<BlueprintFeatureReference>(),
                CavesDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            CavesDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = CavesDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            CavesDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = CavesDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            CavesDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = CavesDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            CavesDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = CavesDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            CavesDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = CavesDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(CavesDomainGreaterAbility.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(CavesDomainGreaterAbilitySeparatist.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Caves Subdomain")) { return; }
            DomainTools.RegisterDomain(CavesDomainProgression);
            DomainTools.RegisterSecondaryDomain(CavesDomainProgressionSecondary);
            DomainTools.RegisterDruidDomain(CavesDomainProgressionDruid);
            DomainTools.RegisterBlightDruidDomain(CavesDomainProgressionDruid);
            DomainTools.RegisterDivineHunterDomain(CavesDomainProgression);
            DomainTools.RegisterTempleDomain(CavesDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(CavesDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(CavesDomainProgression, CavesDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(CavesDomainProgressionSeparatist);

        }
    }
}
