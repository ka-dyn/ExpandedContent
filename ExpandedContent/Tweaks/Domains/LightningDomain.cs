using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Components;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System.Collections.Generic;
using static ExpandedContent.Utilities.DeityTools;

namespace ExpandedContent.Tweaks.Domains {
    internal class LightningDomain {

        public static void AddLightningDomain() {

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
            var AirDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("b3494639791901e4db3eda6117ad878f");
            var AirDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("8a924658f99bf724082cfe3c0f7b28f1");
            var LightningRodIcon = AssetLoader.LoadInternal("Skills", "Icon_LightningRod.jpg");



            
            //LightningDomainGreaterBuff
            var LightningDomainGreaterBuff = Helpers.CreateBuff("LightningDomainGreaterBuff", bp => {
                bp.SetName("Lightning Rod");
                bp.SetDescription("The next spell you cast with the electricity descriptor that hits this creature " +
                    "this round has it’s damage against that creature increased by 50%. This additional damage results from divine power that is not subject to being reduced by " +
                    "electricity resistance, and you take an equal amount of electricity damage immediately after you cast the spell. The spell can deal this additional damage only " +
                    "once, even if it could affect the target multiple times.");
                bp.m_Icon = LightningRodIcon;
                bp.AddComponent<SpellDamageBonusWithRelect>(c => {
                    c.descriptor = SpellDescriptor.Electricity;
                    c.change_damage_type = true;
                    c.damage_type = Kingmaker.Enums.Damage.DamageEnergyType.Divine;
                    c.reflect_damage = true;
                    c.change_reflect_damage_type = true;
                    c.reflect_damage_type = Kingmaker.Enums.Damage.DamageEnergyType.Electricity;
                    c.only_spells = true;
                    c.remove_self_on_apply = true;
                    c.only_from_caster = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            //LightningDomainGreaterResource
            var LightningDomainGreaterResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("LightningDomainGreaterResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        DruidClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    StartingLevel = 8,
                    LevelStep = 4,
                    StartingIncrease = 1,
                    PerStepIncrease = 1,
                };
            });
            //LightningDomainGreaterAbility
            var LightningDomainGreaterAbility = Helpers.CreateBlueprint<BlueprintAbility>("LightningDomainGreaterAbility", bp => {
                bp.SetName("Lightning Rod");
                bp.SetDescription("As a swift action designate one creature within line of sight, the next spell you cast with the electricity descriptor that hits this creature " +
                    "this round has it’s damage against that creature increased by 50%. This additional damage results from divine power that is not subject to being reduced by " +
                    "electricity resistance, and you take an equal amount of electricity damage immediately after you cast the spell. The spell can deal this additional damage only " +
                    "once, even if it could affect the target multiple times. You can use this ability once per day at 8th level and one additional time per day for every 4 cleric " +
                    "levels you have beyond 8th.");
                bp.m_Icon = LightningRodIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = LightningDomainGreaterBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = LightningDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            //LightningDomainGreaterFeature
            var LightningDomainGreaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("LightningDomainGreaterFeature", bp => {
                bp.SetName("Lightning Rod");
                bp.SetDescription("As a swift action designate one creature within line of sight, the next spell you cast with the electricity descriptor that hits this creature " +
                    "this round has it’s damage against that creature increased by 50%. This additional damage results from divine power that is not subject to being reduced by " +
                    "electricity resistance, and you take an equal amount of electricity damage immediately after you cast the spell. The spell can deal this additional damage only " +
                    "once, even if it could affect the target multiple times. You can use this ability once per day at 8th level and one additional time per day for every 4 cleric " +
                    "levels you have beyond 8th.");
                bp.m_Icon = LightningRodIcon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = LightningDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { LightningDomainGreaterAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            //Spelllist
            var ShockingGraspSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("ab395d2335d3f384e99dddee8562978f");
            var ProtectionFromArrowsSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c28de1f98a3f432448e52e5d47c73208");
            var LightningBoltSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d2cff9243a7ee804cb6d5be47af30c73");
            var ShoutSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f09453607e683784c8fca646eec49162");
            var CloudKillSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("548d339ba87ee56459c98e80167bdf10");
            var ChainLightningSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("645558d63604747428d55f0dd3a4cb58");
            var ElementalBodyIVAirSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("ee63301f83c76694692d4704d8a05bdc");
            var ShoutGreaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fd0d3840c48cafb44bb29e8eb74df204");
            var ElementalSwarmAirSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("07e8f6479cbcc3f46a12696784805305");
            var LightningDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("LightningDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ShockingGraspSpell
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ProtectionFromArrowsSpell
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            LightningBoltSpell
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ShoutSpell
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            CloudKillSpell
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ChainLightningSpell
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ElementalBodyIVAirSpell
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ShoutGreaterSpell
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ElementalSwarmAirSpell
                        }
                    },
                };
            });     
            var LightningDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("LightningDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = LightningDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var LightningDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("LightningDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { AirDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = AirDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { AirDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = LightningDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Lightning Subdomain");
                bp.SetDescription("\nYou herald the roar of thunder and crash of lightning. " +
                    "\nLightning Arc: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can unleash an arc of electricity targeting any foe within 30 feet as a ranged " +
                    "{g|Encyclopedia:TouchAttack}touch attack{/g}. This arc of electricity deals {g|Encyclopedia:Dice}1d6{/g} points of electricity damage + 1 point for every two " +
                    "levels you possess in the class that gave you access to this domain. You can use this ability a number of times per day equal to 3 + your " +
                    "{g|Encyclopedia:Wisdom}Wisdom{/g} modifier. " +
                    "\nLightning Rod: As a swift action designate one creature within line of sight, the next spell you cast with the electricity descriptor that hits this creature " +
                    "this round has it’s damage against that creature increased by 50%. This additional damage results from divine power that is not subject to being reduced by " +
                    "electricity resistance, and you take an equal amount of electricity damage immediately after you cast the spell. The spell can deal this additional damage only " +
                    "once, even if it could affect the target multiple times. You can use this ability once per day at 8th level and one additional time per day for every 4 cleric " +
                    "levels you have beyond 8th.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var LightningDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("LightningDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = LightningDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var LightningDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("LightningDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LightningDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = LightningDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = LightningDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Lightning Subdomain");
                bp.SetDescription("\nYou herald the roar of thunder and crash of lightning. " +
                    "\nLightning Arc: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can unleash an arc of electricity targeting any foe within 30 feet as a ranged " +
                    "{g|Encyclopedia:TouchAttack}touch attack{/g}. This arc of electricity deals {g|Encyclopedia:Dice}1d6{/g} points of electricity damage + 1 point for every two " +
                    "levels you possess in the class that gave you access to this domain. You can use this ability a number of times per day equal to 3 + your " +
                    "{g|Encyclopedia:Wisdom}Wisdom{/g} modifier. " +
                    "\nLightning Rod: As a swift action designate one creature within line of sight, the next spell you cast with the electricity descriptor that hits this creature " +
                    "this round has it’s damage against that creature increased by 50%. This additional damage results from divine power that is not subject to being reduced by " +
                    "electricity resistance, and you take an equal amount of electricity damage immediately after you cast the spell. The spell can deal this additional damage only " +
                    "once, even if it could affect the target multiple times. You can use this ability once per day at 8th level and one additional time per day for every 4 cleric " +
                    "levels you have beyond 8th. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsShockingGrasp}shocking grasp{/g}, {g|SpellsProtectionFromArrows}protection from arrows{/g}, " +
                    "{g|SpellsLightningBolt}lightning bolt{/g}, {g|SpellsShout}shout{/g}, {g|SpellsCloudkill}cloudkill{/g}, {g|SpellsChainLightning}chain lightning{/g}, " +
                    "{g|SpellsElementalBodyIVAir}elemental body IV (air){/g}, {g|SpellsGreaterShout}shout, greater{/g}, {g|SpellsElementalSwarmAir}elemental swarm (air){/g}.");
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
                    Helpers.LevelEntry(1, LightningDomainBaseFeature),
                    Helpers.LevelEntry(6, LightningDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(LightningDomainBaseFeature, LightningDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var LightningDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("LightningDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LightningDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LightningDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Lightning Subdomain");
                bp.SetDescription("\nYou herald the roar of thunder and crash of lightning. " +
                    "\nLightning Arc: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can unleash an arc of electricity targeting any foe within 30 feet as a ranged " +
                    "{g|Encyclopedia:TouchAttack}touch attack{/g}. This arc of electricity deals {g|Encyclopedia:Dice}1d6{/g} points of electricity damage + 1 point for every two " +
                    "levels you possess in the class that gave you access to this domain. You can use this ability a number of times per day equal to 3 + your " +
                    "{g|Encyclopedia:Wisdom}Wisdom{/g} modifier. " +
                    "\nLightning Rod: As a swift action designate one creature within line of sight, the next spell you cast with the electricity descriptor that hits this creature " +
                    "this round has it’s damage against that creature increased by 50%. This additional damage results from divine power that is not subject to being reduced by " +
                    "electricity resistance, and you take an equal amount of electricity damage immediately after you cast the spell. The spell can deal this additional damage only " +
                    "once, even if it could affect the target multiple times. You can use this ability once per day at 8th level and one additional time per day for every 4 cleric " +
                    "levels you have beyond 8th. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsShockingGrasp}shocking grasp{/g}, {g|SpellsProtectionFromArrows}protection from arrows{/g}, " +
                    "{g|SpellsLightningBolt}lightning bolt{/g}, {g|SpellsShout}shout{/g}, {g|SpellsCloudkill}cloudkill{/g}, {g|SpellsChainLightning}chain lightning{/g}, " +
                    "{g|SpellsElementalBodyIVAir}elemental body IV (air){/g}, {g|SpellsGreaterShout}shout, greater{/g}, {g|SpellsElementalSwarmAir}elemental swarm (air){/g}.");
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
                    Helpers.LevelEntry(1, LightningDomainBaseFeature),
                    Helpers.LevelEntry(8, LightningDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(LightningDomainBaseFeature, LightningDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // LightningDomainSpellListFeatureDruid
            var LightningDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("LightningDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = LightningDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            // LightningDomainProgressionDruid
            var LightningDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("LightningDomainProgressionDruid", bp => {
                bp.SetName("Lightning Subdomain");
                bp.SetDescription("\nYou herald the roar of thunder and crash of lightning. " +
                    "\nLightning Arc: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can unleash an arc of electricity targeting any foe within 30 feet as a ranged " +
                    "{g|Encyclopedia:TouchAttack}touch attack{/g}. This arc of electricity deals {g|Encyclopedia:Dice}1d6{/g} points of electricity damage + 1 point for every two " +
                    "levels you possess in the class that gave you access to this domain. You can use this ability a number of times per day equal to 3 + your " +
                    "{g|Encyclopedia:Wisdom}Wisdom{/g} modifier. " +
                    "\nLightning Rod: As a swift action designate one creature within line of sight, the next spell you cast with the electricity descriptor that hits this creature " +
                    "this round has it’s damage against that creature increased by 50%. This additional damage results from divine power that is not subject to being reduced by " +
                    "electricity resistance, and you take an equal amount of electricity damage immediately after you cast the spell. The spell can deal this additional damage only " +
                    "once, even if it could affect the target multiple times. You can use this ability once per day at 8th level and one additional time per day for every 4 cleric " +
                    "levels you have beyond 8th. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsShockingGrasp}shocking grasp{/g}, {g|SpellsProtectionFromArrows}protection from arrows{/g}, " +
                    "{g|SpellsLightningBolt}lightning bolt{/g}, {g|SpellsShout}shout{/g}, {g|SpellsCloudkill}cloudkill{/g}, {g|SpellsChainLightning}chain lightning{/g}, " +
                    "{g|SpellsElementalBodyIVAir}elemental body IV (air){/g}, {g|SpellsGreaterShout}shout, greater{/g}, {g|SpellsElementalSwarmAir}elemental swarm (air){/g}.");
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
                    Helpers.LevelEntry(1, LightningDomainBaseFeature, LightningDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(8, LightningDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions
            var WeatherDomainBaseAbilitySeparatist = Resources.GetBlueprint<BlueprintAbility>("deea159d0ab8445fa8f9098f73ef348c");
            var WeatherDomainBaseResourceSeparatist = Resources.GetBlueprint<BlueprintAbilityResource>("4df120899535443e977d50f35717be09");
            var SeparatistAsIsProperty = Resources.GetModBlueprint<BlueprintUnitProperty>("SeparatistAsIsProperty");


            var LightningDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("LightningDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var LightningDomainGreaterResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("LightningDomainGreaterResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevelStartPlusDivStep = true,
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        DruidClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    LevelIncrease = 1,
                    StartingLevel = 3,
                    StartingIncrease = 1,
                    LevelStep = 1,
                    PerStepIncrease = 1,
                };
                bp.m_Min = 1;
            });


            var LightningDomainGreaterAbilitySeparatist = Helpers.CreateBlueprint<BlueprintActivatableAbility>("LightningDomainGreaterAbilitySeparatist", bp => {
                bp.SetName("Lightning Rod");
                bp.SetDescription("As a swift action designate one creature within line of sight, the next spell you cast with the electricity descriptor that hits this creature " +
                    "this round has it’s damage against that creature increased by 50%. This additional damage results from divine power that is not subject to being reduced by " +
                    "electricity resistance, and you take an equal amount of electricity damage immediately after you cast the spell. The spell can deal this additional damage only " +
                    "once, even if it could affect the target multiple times. You can use this ability once per day at 8th level and one additional time per day for every 4 cleric " +
                    "levels you have beyond 8th.");
                bp.m_Icon = LightningRodIcon;
                bp.m_Buff = LightningDomainGreaterBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.NewRound;
                    c.m_RequiredResource = LightningDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
            });

            var LightningDomainGreaterFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("LightningDomainGreaterFeatureSeparatist", bp => {
                bp.SetName("Lightning Rod");
                bp.SetDescription("As a swift action designate one creature within line of sight, the next spell you cast with the electricity descriptor that hits this creature " +
                    "this round has it’s damage against that creature increased by 50%. This additional damage results from divine power that is not subject to being reduced by " +
                    "electricity resistance, and you take an equal amount of electricity damage immediately after you cast the spell. The spell can deal this additional damage only " +
                    "once, even if it could affect the target multiple times. You can use this ability once per day at 8th level and one additional time per day for every 4 cleric " +
                    "levels you have beyond 8th.");
                bp.m_Icon = LightningRodIcon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = LightningDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { LightningDomainGreaterAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var LightningDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("LightningDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WeatherDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = WeatherDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { WeatherDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = LightningDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Lightning Subdomain");
                bp.SetDescription("\nYou herald the roar of thunder and crash of lightning. " +
                    "\nLightning Arc: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can unleash an arc of electricity targeting any foe within 30 feet as a ranged " +
                    "{g|Encyclopedia:TouchAttack}touch attack{/g}. This arc of electricity deals {g|Encyclopedia:Dice}1d6{/g} points of electricity damage + 1 point for every two " +
                    "levels you possess in the class that gave you access to this domain. You can use this ability a number of times per day equal to 3 + your " +
                    "{g|Encyclopedia:Wisdom}Wisdom{/g} modifier. " +
                    "\nLightning Rod: As a swift action designate one creature within line of sight, the next spell you cast with the electricity descriptor that hits this creature " +
                    "this round has it’s damage against that creature increased by 50%. This additional damage results from divine power that is not subject to being reduced by " +
                    "electricity resistance, and you take an equal amount of electricity damage immediately after you cast the spell. The spell can deal this additional damage only " +
                    "once, even if it could affect the target multiple times. You can use this ability once per day at 8th level and one additional time per day for every 4 cleric " +
                    "levels you have beyond 8th.");
                bp.IsClassFeature = true;
            });

            var LightningDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("LightningDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LightningDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LightningDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LightningDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LightningDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = LightningDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Lightning Subdomain");
                bp.SetDescription("\nYou herald the roar of thunder and crash of lightning. " +
                    "\nLightning Arc: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can unleash an arc of electricity targeting any foe within 30 feet as a ranged " +
                    "{g|Encyclopedia:TouchAttack}touch attack{/g}. This arc of electricity deals {g|Encyclopedia:Dice}1d6{/g} points of electricity damage + 1 point for every two " +
                    "levels you possess in the class that gave you access to this domain. You can use this ability a number of times per day equal to 3 + your " +
                    "{g|Encyclopedia:Wisdom}Wisdom{/g} modifier. " +
                    "\nLightning Rod: As a swift action designate one creature within line of sight, the next spell you cast with the electricity descriptor that hits this creature " +
                    "this round has it’s damage against that creature increased by 50%. This additional damage results from divine power that is not subject to being reduced by " +
                    "electricity resistance, and you take an equal amount of electricity damage immediately after you cast the spell. The spell can deal this additional damage only " +
                    "once, even if it could affect the target multiple times. You can use this ability once per day at 8th level and one additional time per day for every 4 cleric " +
                    "levels you have beyond 8th. " +
                    "\nDomain {g|Encyclopedia:Spell}Spells{/g}: {g|SpellsShockingGrasp}shocking grasp{/g}, {g|SpellsProtectionFromArrows}protection from arrows{/g}, " +
                    "{g|SpellsLightningBolt}lightning bolt{/g}, {g|SpellsShout}shout{/g}, {g|SpellsCloudkill}cloudkill{/g}, {g|SpellsChainLightning}chain lightning{/g}, " +
                    "{g|SpellsElementalBodyIVAir}elemental body IV (air){/g}, {g|SpellsGreaterShout}shout, greater{/g}, {g|SpellsElementalSwarmAir}elemental swarm (air){/g}.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, LightningDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(10, LightningDomainGreaterFeatureSeparatist)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(LightningDomainBaseFeatureSeparatist, LightningDomainGreaterFeatureSeparatist)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            LightningDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                LightningDomainProgression.ToReference<BlueprintFeatureReference>(),
                LightningDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            LightningDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = LightningDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            LightningDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = LightningDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            LightningDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = LightningDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            LightningDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = LightningDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            LightningDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = LightningDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            //DomainMastery.Abilities.Add(LightningDomainGreaterAbility.ToReference<BlueprintAbilityReference>());
            //DomainMastery.Abilities.Add(LightningDomainGreaterAbilitySeparatist.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Lightning Subdomain")) { return; }
            DomainTools.RegisterDomain(LightningDomainProgression);
            DomainTools.RegisterSecondaryDomain(LightningDomainProgressionSecondary);
            DomainTools.RegisterDruidDomain(LightningDomainProgressionDruid);
            DomainTools.RegisterBlightDruidDomain(LightningDomainProgressionDruid);
            DomainTools.RegisterDivineHunterDomain(LightningDomainProgression);
            DomainTools.RegisterTempleDomain(LightningDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(LightningDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(LightningDomainProgression, LightningDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(LightningDomainProgressionSeparatist);
            DomainTools.AllowFakeDivineSpark(LightningDomainAllowed);

        }
    }
}
