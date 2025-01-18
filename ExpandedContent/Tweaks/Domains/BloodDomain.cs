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
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.ElementsSystem;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.RuleSystem.Rules.Damage;
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.Domains {
    internal class BloodDomain {

        public static void AddBloodDomain() {

            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");
            var WarDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("fbef6b2053ab6634a82df06f76c260e3");
            var WarDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("27f08408c3e237f43a6ca56d4236b8fe");
            var SacredWeaponEnchantAnarchicChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("6fdc32d0af41ffb42b8285dbac9a050a");

            var WoundingBuff = Helpers.CreateBuff("WoundingBuff", bp => {
                bp.SetName("Wounded");
                bp.SetDescription("This creature takes 1 hit point {g|Encyclopedia:Damage}damage{/g} each turn for each stack of this status. This can be stopped through the application " +
                    "of any {g|Encyclopedia:Spell}spell{/g} that cures hit point damage.");
                bp.m_Icon = SacredWeaponEnchantAnarchicChoice.Icon;
                bp.AddComponent<AddFactContextActions>(c => {
                    c.NewRound = Helpers.CreateActionList(
                        new ContextActionDealDamage()
                        {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription()
                            {
                                Common = new DamageTypeDescription.CommomData()
                                {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData()
                                {
                                    Material = 0,
                                    Form = Kingmaker.Enums.Damage.PhysicalDamageForm.Slashing,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = Kingmaker.Enums.Damage.DamageEnergyType.Fire,
                                Type = DamageType.Direct
                            },
                            Drain = false,
                            AbilityType = StatType.Unknown,
                            Duration = new ContextDurationValue()
                            {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue()
                                {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue()
                                {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true,
                            },
                            PreRolledSharedValue = AbilitySharedValue.Damage,
                            Value = new ContextDiceValue()
                            {
                                DiceType = DiceType.One,
                                DiceCountValue = new ContextValue()
                                {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue()
                                {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                            },
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        });
                    new Conditional()
                    {
                        ConditionsChecker = new ConditionsChecker()
                        {
                            Operation = Operation.And,
                            Conditions = new Condition[] {
                                    new ContextConditionIsInCombat() {
                                        Not = false
                                    }
                                }
                        },
                        IfTrue = Helpers.CreateActionList(),
                        IfFalse = Helpers.CreateActionList(
                            new Conditional()
                            {
                                ConditionsChecker = new ConditionsChecker()
                                {
                                    Operation = Operation.And,
                                    Conditions = new Condition[] {
                                            new ContextConditionIsPartyMember() {
                                                Not = false
                                            }
                                    }
                                },
                                IfTrue = Helpers.CreateActionList(),
                                IfFalse = Helpers.CreateActionList(
                                    new ContextActionRemoveSelf()
                                    )
                            }
                            )
                    };
                });
                bp.AddComponent<AddHealTrigger>(c => {
                    c.Action = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                    c.OnHealDamage = true;
                });
                bp.AddComponent<CombatStateTrigger>(c => {
                    c.CombatEndActions = Helpers.CreateActionList(
                        new ContextActionRemoveSelf()
                        );
                });
                bp.Stacking = StackingType.Stack;
                bp.Frequency = DurationRate.Rounds;
            });
            //Wounding Weapon Enchant
            var WoundingEnchantment = Helpers.CreateBlueprint<BlueprintWeaponEnchantment>("WoundingEnchantment", bp => {
                bp.SetName("Wounding");
                bp.SetDescription("All attacks deal 1 bleed damage that triggers each round and stacks with itself.");
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.OnlyHit = true;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff()
                        {
                            m_Buff = WoundingBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue()
                            {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                m_IsExtendable = true,
                            }
                        });
                });
                bp.SetPrefix("Wounding");
                bp.SetSuffix("");
            });
            //BloodDomainGreaterResource
            var BloodDomainGreaterResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("BloodDomainGreaterResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
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
            //BloodDomainGreaterAbility
            var BloodDomainGreaterAbility = Helpers.CreateBlueprint<BlueprintAbility>("BloodDomainGreaterAbility", bp => {
                bp.SetName("Wounding Blade");
                bp.SetDescription("You may give a weapon that you touch the wounding special weapon quality for a number of rounds equal " +
                    "to 1/2 your cleric level. You can use this ability once per day at 8th level, and an additional time per day for every four levels " +
                    "beyond 8th. \nWounded: Creatures hit by this weapon take 1 hit point {g|Encyclopedia:Damage}damage{/g} each turn for each stack of this status. " +
                    "This can be stopped through the application of any {g|Encyclopedia:Spell}spell{/g} that cures hit point damage.");
                bp.m_Icon = SacredWeaponEnchantAnarchicChoice.Icon;
                bp.AddComponent<SpellComponent>(c => {
                    c.m_Flags = 0;
                    c.School = SpellSchool.Enchantment;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BloodDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionEnchantWornItem() {
                            m_Enchantment = WoundingEnchantment.ToReference<BlueprintItemEnchantmentReference>(),
                            Slot = Kingmaker.UI.GenericSlot.EquipSlotBase.SlotType.PrimaryHand,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            }
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { 
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] { 
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic |= Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Reach;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = Helpers.CreateString("BloodDomainGreaterAbility.Duration", "1 round/2 levels");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
                
            });
            //BloodDomainGreaterFeature
            var BloodDomainGreaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("BloodDomainGreaterFeature", bp => {
                bp.SetName("Wounding Blade");
                bp.SetDescription("At 8th level, you can give a weapon that you touch the wounding special weapon quality for a number of rounds equal " +
                    "to 1/2 your cleric level. You can use this ability once per day at 8th level, and an additional time per day for every four levels " +
                    "beyond 8th. \nWounded: Creatures hit by this weapon take 1 hit point {g|Encyclopedia:Damage}damage{/g} each turn for each stack of this status. " +
                    "This can be stopped through the application of any {g|Encyclopedia:Spell}spell{/g} that cures hit point damage.");
                bp.m_Icon = SacredWeaponEnchantAnarchicChoice.Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = BloodDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { BloodDomainGreaterAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            //Spelllist
            var MagicWeaponSpell = Resources.GetBlueprint<BlueprintAbility>("d7fdd79f0f6b6a2418298e936bb68e40");
            var AidSpell = Resources.GetBlueprint<BlueprintAbility>("03a9630394d10164a9410882d31572f0");
            var VampiricTouchSpell = Resources.GetBlueprint<BlueprintAbility>("6cbb040023868574b992677885390f92");
            var DivingPowerSpell = Resources.GetBlueprint<BlueprintAbility>("ef16771cb05d1344989519e87f25b3c5");
            var VinetrapSpell = Resources.GetBlueprint<BlueprintAbility>("6d1d48a939ce475409f06e1b376bc386");
            var BladeBarrierSpell = Resources.GetBlueprint<BlueprintAbility>("36c8971e91f1745418cc3ffdfac17b74");
            var InflictSeriousWoundsSpell = Resources.GetBlueprint<BlueprintAbility>("820170444d4d2a14abc480fcbdb49535");
            var PowerWordStunSpell = Resources.GetBlueprint<BlueprintAbility>("f958ef62eea5050418fb92dfa944c631");
            var PowerWordKillSpell = Resources.GetBlueprint<BlueprintAbility>("2f8a67c483dfa0f439b293e094ca9e3c");
            var BloodDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("BloodDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            MagicWeaponSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            AidSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            VampiricTouchSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            DivingPowerSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            VinetrapSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BladeBarrierSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            InflictSeriousWoundsSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            PowerWordStunSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            PowerWordKillSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var BloodDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("BloodDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = BloodDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var BloodDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("BloodDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WarDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = WarDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { WarDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = BloodDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Blood Subdomain");
                bp.SetDescription("\nYou are a crusader for your god, always ready and willing to fight to defend your faith.\nBattle Rage: You can {g|Encyclopedia:TouchAttack}touch{/g} " +
                    "a creature as a {g|Encyclopedia:Standard_Actions}standard action{/g} to give it a {g|Encyclopedia:Bonus}bonus{/g} on melee {g|Encyclopedia:Damage}damage rolls{/g} " +
                    "equal to 1/2 your level in the class that gave you access to this domain for 1 {g|Encyclopedia:Combat_Round}round{/g} (minimum +1). You can do so a number of times " +
                    "per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nWounding Blade (Su): At 8th level, you can give a weapon that you touch the wounding special " +
                    "weapon quality for a number of rounds equal to 1/2 your cleric level. You can use this ability once per day at 8th level, and an additional time per day for every " +
                    "four levels beyond 8th.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var BloodDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("BloodDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = BloodDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var BloodDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("BloodDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = BloodDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = BloodDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = BloodDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Blood Subdomain");
                bp.SetDescription("\nYou are a crusader for your god, always ready and willing to fight to defend your faith.\nBattle Rage: You can {g|Encyclopedia:TouchAttack}touch{/g} " +
                    "a creature as a {g|Encyclopedia:Standard_Actions}standard action{/g} to give it a {g|Encyclopedia:Bonus}bonus{/g} on melee {g|Encyclopedia:Damage}damage rolls{/g} " +
                    "equal to 1/2 your level in the class that gave you access to this domain for 1 {g|Encyclopedia:Combat_Round}round{/g} (minimum +1). You can do so a number of times " +
                    "per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nWounding Blade (Su): At 8th level, you can give a weapon that you touch the wounding special " +
                    "weapon quality for a number of rounds equal to 1/2 your cleric level. You can use this ability once per day at 8th level, and an additional time per day for every " +
                    "four levels beyond 8th.\nDomain Spells: magic weapon, aid, vampiric touch, diving power, vinetrap, blade barrier, inflict serious wounds, power word stun, power word kill.");
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
                    Helpers.LevelEntry(1, BloodDomainBaseFeature),
                    Helpers.LevelEntry(8, BloodDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(BloodDomainBaseFeature, BloodDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var BloodDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("BloodDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = BloodDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = BloodDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Blood Subdomain");
                bp.SetDescription("\nYou are a crusader for your god, always ready and willing to fight to defend your faith.\nBattle Rage: You can {g|Encyclopedia:TouchAttack}touch{/g} " +
                    "a creature as a {g|Encyclopedia:Standard_Actions}standard action{/g} to give it a {g|Encyclopedia:Bonus}bonus{/g} on melee {g|Encyclopedia:Damage}damage rolls{/g} " +
                    "equal to 1/2 your level in the class that gave you access to this domain for 1 {g|Encyclopedia:Combat_Round}round{/g} (minimum +1). You can do so a number of times " +
                    "per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nWounding Blade (Su): At 8th level, you can give a weapon that you touch the wounding special " +
                    "weapon quality for a number of rounds equal to 1/2 your cleric level. You can use this ability once per day at 8th level, and an additional time per day for every " +
                    "four levels beyond 8th.\nDomain Spells: magic weapon, aid, vampiric touch, diving power, vinetrap, blade barrier, inflict serious wounds, power word stun, power word kill.");
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
                    Helpers.LevelEntry(1, BloodDomainBaseFeature),
                    Helpers.LevelEntry(8, BloodDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(BloodDomainBaseFeature, BloodDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions
            var WarDomainBaseAbilitySeparatist = Resources.GetBlueprint<BlueprintAbility>("294cef74b96446d69b008dba8ebcfb11");
            var WarDomainBaseResourceSeparatist = Resources.GetBlueprint<BlueprintAbilityResource>("77e5cc411c984ab1962cb5033ee8a23c");

            var BloodDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("BloodDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var BloodDomainGreaterResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("BloodDomainGreaterResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
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
                    StartingLevel = 10,
                    LevelStep = 4,
                    StartingIncrease = 1,
                    PerStepIncrease = 1,
                };
            });
            var BloodDomainGreaterAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("BloodDomainGreaterAbilitySeparatist", bp => {
                bp.SetName("Wounding Blade");
                bp.SetDescription("You may give a weapon that you touch the wounding special weapon quality for a number of rounds equal " +
                    "to 1/2 your cleric level. You can use this ability once per day at 8th level, and an additional time per day for every four levels " +
                    "beyond 8th. \nWounded: Creatures hit by this weapon take 1 hit point {g|Encyclopedia:Damage}damage{/g} each turn for each stack of this status. " +
                    "This can be stopped through the application of any {g|Encyclopedia:Spell}spell{/g} that cures hit point damage.");
                bp.m_Icon = SacredWeaponEnchantAnarchicChoice.Icon;
                bp.AddComponent<SpellComponent>(c => {
                    c.m_Flags = 0;
                    c.School = SpellSchool.Enchantment;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BloodDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;

                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionEnchantWornItem() {
                            m_Enchantment = WoundingEnchantment.ToReference<BlueprintItemEnchantmentReference>(),
                            Slot = Kingmaker.UI.GenericSlot.EquipSlotBase.SlotType.PrimaryHand,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            }
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 3;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { 
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic |= Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Reach;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = Helpers.CreateString("BloodDomainGreaterAbilitySeparatist.Duration", "1 round/2 levels");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();

            });

            var BloodDomainGreaterFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("BloodDomainGreaterFeatureSeparatist", bp => {
                bp.SetName("Wounding Blade");
                bp.SetDescription("At 8th level, you can give a weapon that you touch the wounding special weapon quality for a number of rounds equal " +
                    "to 1/2 your cleric level. You can use this ability once per day at 8th level, and an additional time per day for every four levels " +
                    "beyond 8th. \nWounded: Creatures hit by this weapon take 1 hit point {g|Encyclopedia:Damage}damage{/g} each turn for each stack of this status. " +
                    "This can be stopped through the application of any {g|Encyclopedia:Spell}spell{/g} that cures hit point damage.");
                bp.m_Icon = SacredWeaponEnchantAnarchicChoice.Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = BloodDomainGreaterResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { BloodDomainGreaterAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var BloodDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("BloodDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WarDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = WarDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { WarDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = BloodDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Blood Subdomain");
                bp.SetDescription("\nYou are a crusader for your god, always ready and willing to fight to defend your faith.\nBattle Rage: You can {g|Encyclopedia:TouchAttack}touch{/g} " +
                    "a creature as a {g|Encyclopedia:Standard_Actions}standard action{/g} to give it a {g|Encyclopedia:Bonus}bonus{/g} on melee {g|Encyclopedia:Damage}damage rolls{/g} " +
                    "equal to 1/2 your level in the class that gave you access to this domain for 1 {g|Encyclopedia:Combat_Round}round{/g} (minimum +1). You can do so a number of times " +
                    "per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nWounding Blade (Su): At 8th level, you can give a weapon that you touch the wounding special " +
                    "weapon quality for a number of rounds equal to 1/2 your cleric level. You can use this ability once per day at 8th level, and an additional time per day for every " +
                    "four levels beyond 8th.");
                bp.IsClassFeature = true;
            });

            var BloodDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("BloodDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = BloodDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = BloodDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = BloodDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = BloodDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = BloodDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Blood Subdomain");
                bp.SetDescription("\nYou are a crusader for your god, always ready and willing to fight to defend your faith.\nBattle Rage: You can {g|Encyclopedia:TouchAttack}touch{/g} " +
                    "a creature as a {g|Encyclopedia:Standard_Actions}standard action{/g} to give it a {g|Encyclopedia:Bonus}bonus{/g} on melee {g|Encyclopedia:Damage}damage rolls{/g} " +
                    "equal to 1/2 your level in the class that gave you access to this domain for 1 {g|Encyclopedia:Combat_Round}round{/g} (minimum +1). You can do so a number of times " +
                    "per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nWounding Blade (Su): At 8th level, you can give a weapon that you touch the wounding special " +
                    "weapon quality for a number of rounds equal to 1/2 your cleric level. You can use this ability once per day at 8th level, and an additional time per day for every " +
                    "four levels beyond 8th.\nDomain Spells: magic weapon, aid, vampiric touch, diving power, vinetrap, blade barrier, inflict serious wounds, power word stun, power word kill.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {  };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, BloodDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(10, BloodDomainGreaterFeatureSeparatist)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(BloodDomainBaseFeatureSeparatist, BloodDomainGreaterFeatureSeparatist)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            BloodDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                BloodDomainProgression.ToReference<BlueprintFeatureReference>(),
                BloodDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            BloodDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = BloodDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            BloodDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = BloodDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            BloodDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = BloodDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            BloodDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = BloodDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            BloodDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = BloodDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            }); 
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(BloodDomainGreaterAbility.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(BloodDomainGreaterAbilitySeparatist.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Blood Subdomain")) { return; }
            DomainTools.RegisterDomain(BloodDomainProgression);
            DomainTools.RegisterSecondaryDomain(BloodDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(BloodDomainProgression);
            DomainTools.RegisterTempleDomain(BloodDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(BloodDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(BloodDomainProgression, BloodDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(BloodDomainProgressionSeparatist);

        }

    }
}
