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
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.RuleSystem;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Properties;
using ExpandedContent.Config;
using Kingmaker.Blueprints.Classes.Selection;

namespace ExpandedContent.Tweaks.Domains {
    internal class StarsDomain {

        public static void AddStarsDomain() {

            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");

            

            //Spelllist
            var EntropicShieldSpell = Resources.GetModBlueprint<BlueprintAbility>("EntropicShieldAbility");
            var HypnoticPatternSpell = Resources.GetModBlueprint<BlueprintAbility>("HypnoticPatternAbility");
            var BlinkSpell = Resources.GetBlueprint<BlueprintAbility>("045351f1421ee3f449a9143db701d192");
            var DimensionDoorSpell = Resources.GetBlueprint<BlueprintAbility>("4a648b57935a59547b7a2ee86fb4f26a");
            var DimensionDoorSingleSpell = Resources.GetBlueprint<BlueprintAbility>("a9b8be9b87865744382f7c64e599aeb2");
            var DimensionDoorMassSpell = Resources.GetBlueprint<BlueprintAbility>("5bdc37e4acfa209408334326076a43bc");
            var SummonMonsterVBaseSpell = Resources.GetBlueprint<BlueprintAbility>("630c8b85d9f07a64f917d79cb5905741");
            var SummonMonsterVSingleSpell = Resources.GetBlueprint<BlueprintAbility>("0964bf88b582bed41b74e79596c4f6d9");
            var SummonMonsterVd3Spell = Resources.GetBlueprint<BlueprintAbility>("715f208d545be2f4aa2d3693e6347a5a");
            var SummonMonsterVd4plus1Spell = Resources.GetBlueprint<BlueprintAbility>("715f208d545be2f4aa2d3693e6347a5a");
            var OverwhelmingPresenceSpell = Resources.GetBlueprint<BlueprintAbility>("41cf93453b027b94886901dbfc680cb9");
            var SunbeamSpell = Resources.GetBlueprint<BlueprintAbility>("1fca0ba2fdfe2994a8c8bc1f0f2fc5b1");
            var SunburstSpell = Resources.GetBlueprint<BlueprintAbility>("e96424f70ff884947b06f41a765b7658");
            var PolarMidnightSpell = Resources.GetBlueprint<BlueprintAbility>("ba48abb52b142164eba309fd09898856");
            var MeteorSwarmSpell = Resources.GetBlueprint<BlueprintAbility>("5e36df08c71748f7936bce310181fb71");
            var StarsDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("StarsDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            EntropicShieldSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HypnoticPatternSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BlinkSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            DimensionDoorSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SummonMonsterVBaseSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SunbeamSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SunburstSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var StarsDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = StarsDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });            
            var StarsDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainBaseFeature", bp => {
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.Bonus = 2;
                    c.ModifierDescriptor = ModifierDescriptor.Insight;
                    c.SpellDescriptor = SpellDescriptor.MindAffecting;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Stars Subdomain");
                bp.SetDescription("\nThe firmament provides you inspiration, and you draw power from the stars’ distant light.\nGuarded Mind: You gain a +2 insight bonus on saving throws " +
                    "against all mind-affecting effects.\nThe Stars Are Right: At 8th level, you may spontaneously cast any of your Stars subdomain spells by swapping out a spell of an equal " +
                    "spell level. Any Stars subdomain spell that you cast heals you an amount of hit point damage equal to the spell’s level; this effect happens as you cast the spell.");
                bp.IsClassFeature = true;
            });

            var StarsDomainGreaterFeature1Cleric = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1Cleric", bp => {
                bp.SetName("The Stars Are Right - Cleric");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EntropicShieldSpell.ToReference<BlueprintAbilityReference>(),
                        HypnoticPatternSpell.ToReference<BlueprintAbilityReference>(),
                        BlinkSpell.ToReference<BlueprintAbilityReference>(),
                        DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>(),
                        OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>(),
                        SunbeamSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        MeteorSwarmSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature1Inquisitor = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1Inquisitor", bp => {
                bp.SetName("The Stars Are Right - Inquisitor");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EntropicShieldSpell.ToReference<BlueprintAbilityReference>(),
                        HypnoticPatternSpell.ToReference<BlueprintAbilityReference>(),
                        BlinkSpell.ToReference<BlueprintAbilityReference>(),
                        DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>(),
                        OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>(),
                        SunbeamSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        MeteorSwarmSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature1Hunter = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1Hunter", bp => {
                bp.SetName("The Stars Are Right - Hunter");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EntropicShieldSpell.ToReference<BlueprintAbilityReference>(),
                        HypnoticPatternSpell.ToReference<BlueprintAbilityReference>(),
                        BlinkSpell.ToReference<BlueprintAbilityReference>(),
                        DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>(),
                        OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>(),
                        SunbeamSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        MeteorSwarmSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature1Paladin = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1Paladin", bp => {
                bp.SetName("The Stars Are Right - Paladin");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EntropicShieldSpell.ToReference<BlueprintAbilityReference>(),
                        HypnoticPatternSpell.ToReference<BlueprintAbilityReference>(),
                        BlinkSpell.ToReference<BlueprintAbilityReference>(),
                        DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>(),
                        OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>(),
                        SunbeamSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        MeteorSwarmSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature1 = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1", bp => {
                bp.SetName("The Stars Are Right");
                bp.SetDescription("At 8th level, you may spontaneously cast any of your Stars subdomain spells by swapping out a spell of an equal " +
                    "spell level. Any Stars subdomain spell that you cast heals you an amount of hit point damage equal to the spell’s level; this effect happens as you cast the spell.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature1Cleric.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature1Inquisitor.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature1Hunter.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature1Paladin.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = false;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = true;
                    c.m_Ability = EntropicShieldSpell.ToReference<BlueprintAbilityReference>();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionHealTarget() {
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                })
                        });
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = false;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = true;
                    c.m_Ability = HypnoticPatternSpell.ToReference<BlueprintAbilityReference>();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionHealTarget() {
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 2,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                })
                        });
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = false;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = true;
                    c.m_Ability = BlinkSpell.ToReference<BlueprintAbilityReference>();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionHealTarget() {
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 3,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                })
                        });
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = false;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = true;
                    c.m_Ability = DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionHealTarget() {
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 4,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                })
                        });
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = false;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = true;
                    c.m_Ability = DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionHealTarget() {
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 4,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                })
                        });
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = false;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = true;
                    c.m_Ability = SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionHealTarget() {
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 5,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                })
                        });
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = false;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = true;
                    c.m_Ability = SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionHealTarget() {
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 5,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                })
                        });
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = false;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = true;
                    c.m_Ability = SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionHealTarget() {
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 5,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                })
                        });
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = false;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = true;
                    c.m_Ability = OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionHealTarget() {
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 6,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                })
                        });
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = false;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = true;
                    c.m_Ability = SunbeamSpell.ToReference<BlueprintAbilityReference>();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionHealTarget() {
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 7,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                })
                        });
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = false;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = true;
                    c.m_Ability = SunburstSpell.ToReference<BlueprintAbilityReference>();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionHealTarget() {
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 8,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                })
                        });
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = false;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = true;
                    c.m_Ability = MeteorSwarmSpell.ToReference<BlueprintAbilityReference>();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionHealTarget() {
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 9,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                })
                        });
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });

            var StarsDomainGreaterFeature2Cleric = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Cleric", bp => {
                bp.SetName("The Stars Are Right - Cleric 2");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature2Inquisitor = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Inquisitor", bp => {
                bp.SetName("The Stars Are Right - Inquisitor 2");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature2Hunter = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Hunter", bp => {
                bp.SetName("The Stars Are Right - Hunter 2");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature2Paladin = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Paladin", bp => {
                bp.SetName("The Stars Are Right - Paladin 2");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature2 = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2", bp => {
                bp.SetName("The Stars Are Right 2");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature2Cleric.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature2Inquisitor.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature2Hunter.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature2Paladin.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = false;
                    c.ActionsOnTarget = false;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = true;
                    c.m_Ability = PolarMidnightSpell.ToReference<BlueprintAbilityReference>();
                    c.ForMultipleSpells = false;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.MinSpellLevel = false;
                    c.MinSpellLevelLimit = 0;
                    c.ExactSpellLevel = false;
                    c.ExactSpellLevelLimit = 0;
                    c.CheckAbilityType = false;
                    c.Type = AbilityType.Spell;
                    c.CheckDescriptor = false;
                    c.SpellDescriptor = new SpellDescriptor();
                    c.CheckRange = false;
                    c.Range = AbilityRange.Touch;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionHealTarget() {
                                    Value = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 9,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                })
                        });
                });                
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            #region Stargazer stuff
            //Extra stuff for stargazer
            var AlchemistClass = Resources.GetBlueprint<BlueprintCharacterClass>("0937bec61c0dabc468428f496580c721");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var BardClass = Resources.GetBlueprint<BlueprintCharacterClass>("772c83a25e2268e448e841dcd548235f");
            var BloodragerClass = Resources.GetBlueprint<BlueprintCharacterClass>("d77e67a814d686842802c9cfd8ef8499");
            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var MagusClass = Resources.GetBlueprint<BlueprintCharacterClass>("45a4607686d96a1498891b3286121780");
            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var RangerClass = Resources.GetBlueprint<BlueprintCharacterClass>("cda0615668a6df14eb36ba19ee881af6");
            var RogueClass = Resources.GetBlueprint<BlueprintCharacterClass>("299aa766dee3cbf4790da4efb8c72484");
            var ShamanClass = Resources.GetBlueprint<BlueprintCharacterClass>("145f1d3d360a7ad48bd95d392c81b38e");
            var SkaldClass = Resources.GetBlueprint<BlueprintCharacterClass>("6afa347d804838b48bda16acb0573dc0");
            var SorcererClass = Resources.GetBlueprint<BlueprintCharacterClass>("b3a505fb61437dc4097f43c3f8f9a4cf");
            var WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
            var WitchClass = Resources.GetBlueprint<BlueprintCharacterClass>("1b9873f1e7bfe5449bc84d03e9c8e3cc");
            var WizardClass = Resources.GetBlueprint<BlueprintCharacterClass>("ba34257984f4c41408ce1dc2004e342e");

            var StarsDomainGreaterFeature1Alchemist = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1Alchemist", bp => {
                bp.SetName("The Stars Are Right - Alchemist");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = AlchemistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EntropicShieldSpell.ToReference<BlueprintAbilityReference>(),
                        HypnoticPatternSpell.ToReference<BlueprintAbilityReference>(),
                        BlinkSpell.ToReference<BlueprintAbilityReference>(),
                        DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>(),
                        OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>(),
                        SunbeamSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        MeteorSwarmSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = AlchemistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = AlchemistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature1Arcanist = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1Arcanist", bp => {
                bp.SetName("The Stars Are Right - Arcanist");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EntropicShieldSpell.ToReference<BlueprintAbilityReference>(),
                        HypnoticPatternSpell.ToReference<BlueprintAbilityReference>(),
                        BlinkSpell.ToReference<BlueprintAbilityReference>(),
                        DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>(),
                        OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>(),
                        SunbeamSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        MeteorSwarmSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature1Bard = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1Bard", bp => {
                bp.SetName("The Stars Are Right - Bard");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = BardClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EntropicShieldSpell.ToReference<BlueprintAbilityReference>(),
                        HypnoticPatternSpell.ToReference<BlueprintAbilityReference>(),
                        BlinkSpell.ToReference<BlueprintAbilityReference>(),
                        DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>(),
                        OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>(),
                        SunbeamSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        MeteorSwarmSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = BardClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = BardClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature1Bloodrager = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1Bloodrager", bp => {
                bp.SetName("The Stars Are Right - Bloodrager");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = BloodragerClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EntropicShieldSpell.ToReference<BlueprintAbilityReference>(),
                        HypnoticPatternSpell.ToReference<BlueprintAbilityReference>(),
                        BlinkSpell.ToReference<BlueprintAbilityReference>(),
                        DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>(),
                        OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>(),
                        SunbeamSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        MeteorSwarmSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = BloodragerClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = BloodragerClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature1Druid = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1Druid", bp => {
                bp.SetName("The Stars Are Right - Druid");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EntropicShieldSpell.ToReference<BlueprintAbilityReference>(),
                        HypnoticPatternSpell.ToReference<BlueprintAbilityReference>(),
                        BlinkSpell.ToReference<BlueprintAbilityReference>(),
                        DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>(),
                        OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>(),
                        SunbeamSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        MeteorSwarmSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature1Magus = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1Magus", bp => {
                bp.SetName("The Stars Are Right - Magus");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = MagusClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EntropicShieldSpell.ToReference<BlueprintAbilityReference>(),
                        HypnoticPatternSpell.ToReference<BlueprintAbilityReference>(),
                        BlinkSpell.ToReference<BlueprintAbilityReference>(),
                        DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>(),
                        OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>(),
                        SunbeamSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        MeteorSwarmSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = MagusClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = MagusClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature1Oracle = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1Oracle", bp => {
                bp.SetName("The Stars Are Right - Oracle");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EntropicShieldSpell.ToReference<BlueprintAbilityReference>(),
                        HypnoticPatternSpell.ToReference<BlueprintAbilityReference>(),
                        BlinkSpell.ToReference<BlueprintAbilityReference>(),
                        DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>(),
                        OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>(),
                        SunbeamSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        MeteorSwarmSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature1Ranger = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1Ranger", bp => {
                bp.SetName("The Stars Are Right - Ranger");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = RangerClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EntropicShieldSpell.ToReference<BlueprintAbilityReference>(),
                        HypnoticPatternSpell.ToReference<BlueprintAbilityReference>(),
                        BlinkSpell.ToReference<BlueprintAbilityReference>(),
                        DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>(),
                        OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>(),
                        SunbeamSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        MeteorSwarmSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = RangerClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = RangerClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature1Rogue = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1Rogue", bp => {
                bp.SetName("The Stars Are Right - Rogue");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = RogueClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EntropicShieldSpell.ToReference<BlueprintAbilityReference>(),
                        HypnoticPatternSpell.ToReference<BlueprintAbilityReference>(),
                        BlinkSpell.ToReference<BlueprintAbilityReference>(),
                        DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>(),
                        OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>(),
                        SunbeamSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        MeteorSwarmSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = RogueClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = RogueClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature1Shaman = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1Shaman", bp => {
                bp.SetName("The Stars Are Right - Shaman");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EntropicShieldSpell.ToReference<BlueprintAbilityReference>(),
                        HypnoticPatternSpell.ToReference<BlueprintAbilityReference>(),
                        BlinkSpell.ToReference<BlueprintAbilityReference>(),
                        DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>(),
                        OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>(),
                        SunbeamSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        MeteorSwarmSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature1Skald = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1Skald", bp => {
                bp.SetName("The Stars Are Right - Skald");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = SkaldClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EntropicShieldSpell.ToReference<BlueprintAbilityReference>(),
                        HypnoticPatternSpell.ToReference<BlueprintAbilityReference>(),
                        BlinkSpell.ToReference<BlueprintAbilityReference>(),
                        DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>(),
                        OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>(),
                        SunbeamSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        MeteorSwarmSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = SkaldClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = SkaldClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature1Sorcerer = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1Sorcerer", bp => {
                bp.SetName("The Stars Are Right - Sorcerer");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = SorcererClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EntropicShieldSpell.ToReference<BlueprintAbilityReference>(),
                        HypnoticPatternSpell.ToReference<BlueprintAbilityReference>(),
                        BlinkSpell.ToReference<BlueprintAbilityReference>(),
                        DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>(),
                        OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>(),
                        SunbeamSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        MeteorSwarmSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = SorcererClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = SorcererClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature1Warpriest = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1Warpriest", bp => {
                bp.SetName("The Stars Are Right - Warpriest");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EntropicShieldSpell.ToReference<BlueprintAbilityReference>(),
                        HypnoticPatternSpell.ToReference<BlueprintAbilityReference>(),
                        BlinkSpell.ToReference<BlueprintAbilityReference>(),
                        DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>(),
                        OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>(),
                        SunbeamSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        MeteorSwarmSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature1Witch = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1Witch", bp => {
                bp.SetName("The Stars Are Right - Witch");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EntropicShieldSpell.ToReference<BlueprintAbilityReference>(),
                        HypnoticPatternSpell.ToReference<BlueprintAbilityReference>(),
                        BlinkSpell.ToReference<BlueprintAbilityReference>(),
                        DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>(),
                        OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>(),
                        SunbeamSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        MeteorSwarmSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature1Wizard = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature1Wizard", bp => {
                bp.SetName("The Stars Are Right - Wizard");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = WizardClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EntropicShieldSpell.ToReference<BlueprintAbilityReference>(),
                        HypnoticPatternSpell.ToReference<BlueprintAbilityReference>(),
                        BlinkSpell.ToReference<BlueprintAbilityReference>(),
                        DimensionDoorSingleSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVSingleSpell.ToReference<BlueprintAbilityReference>(),
                        OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>(),
                        SunbeamSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        MeteorSwarmSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = WizardClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        DimensionDoorMassSpell.ToReference<BlueprintAbilityReference>(),
                        SummonMonsterVd3Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = WizardClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonMonsterVd4plus1Spell.ToReference<BlueprintAbilityReference>(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StargazerStarsDomainGreaterFeature1 = Helpers.CreateBlueprint<BlueprintFeature>("StargazerStarsDomainGreaterFeature1", bp => {
                bp.SetName("The Stars Are Right - Stargazer");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AlchemistClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature1Alchemist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature1Arcanist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = BardClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature1Bard.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = BloodragerClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature1Bloodrager.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature1Druid.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = MagusClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature1Magus.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature1Oracle.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = RangerClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature1Ranger.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = RogueClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature1Rogue.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature1Shaman.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = SkaldClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature1Skald.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = SorcererClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature1Sorcerer.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature1Warpriest.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature1Witch.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = WizardClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature1Wizard.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });

            var StarsDomainGreaterFeature2Alchemist = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Alchemist", bp => {
                bp.SetName("The Stars Are Right - Alchemist 2");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = AlchemistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature2Arcanist = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Arcanist", bp => {
                bp.SetName("The Stars Are Right - Arcanist 2");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature2Bard = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Bard", bp => {
                bp.SetName("The Stars Are Right - Bard 2");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = BardClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature2Bloodrager = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Bloodrager", bp => {
                bp.SetName("The Stars Are Right - Bloodrager 2");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = BloodragerClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature2Druid = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Druid", bp => {
                bp.SetName("The Stars Are Right - Druid 2");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature2Magus = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Magus", bp => {
                bp.SetName("The Stars Are Right - Magus 2");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = MagusClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature2Oracle = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Oracle", bp => {
                bp.SetName("The Stars Are Right - Oracle 2");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature2Ranger = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Ranger", bp => {
                bp.SetName("The Stars Are Right - Ranger 2");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = RangerClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature2Rogue = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Rogue", bp => {
                bp.SetName("The Stars Are Right - Rogue 2");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = RogueClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature2Shaman = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Shaman", bp => {
                bp.SetName("The Stars Are Right - Shaman 2");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature2Skald = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Skald", bp => {
                bp.SetName("The Stars Are Right - Skald 2");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = SkaldClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature2Sorcerer = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Sorcerer", bp => {
                bp.SetName("The Stars Are Right - Sorcerer 2");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = SorcererClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature2Warpriest = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Warpriest", bp => {
                bp.SetName("The Stars Are Right - Warpriest 2");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature2Witch = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Witch", bp => {
                bp.SetName("The Stars Are Right - Witch 2");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StarsDomainGreaterFeature2Wizard = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Wizard", bp => {
                bp.SetName("The Stars Are Right - Wizard 2");
                bp.SetDescription("");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = WizardClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        PolarMidnightSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StargazerStarsDomainGreaterFeature2 = Helpers.CreateBlueprint<BlueprintFeature>("StargazerStarsDomainGreaterFeature2", bp => {
                bp.SetName("The Stars Are Right - Stargazer");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AlchemistClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature2Alchemist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature2Arcanist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = BardClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature2Bard.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = BloodragerClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature2Bloodrager.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature2Druid.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = MagusClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature2Magus.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature2Oracle.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = RangerClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature2Ranger.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = RogueClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature2Rogue.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ShamanClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature2Shaman.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = SkaldClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature2Skald.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = SorcererClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature2Sorcerer.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature2Warpriest.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature2Witch.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = WizardClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StarsDomainGreaterFeature2Wizard.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StargazerMysteryMagicStarsDomainFeatureSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("StargazerMysteryMagicStarsDomainFeatureSelection");
            var StargazerStarsDomainGreaterFeatureCheck = Helpers.CreateBlueprint<BlueprintFeature>("StargazerStarsDomainGreaterFeatureCheck", bp => {
                bp.SetName("The Stars Are Right - Stargazer Check");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = StargazerMysteryMagicStarsDomainFeatureSelection.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = StargazerStarsDomainGreaterFeature1.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = StargazerMysteryMagicStarsDomainFeatureSelection.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = StargazerStarsDomainGreaterFeature2.ToReference<BlueprintUnitFactReference>();
                });
                bp.ReapplyOnLevelUp = true;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            #endregion

            //Deity plug
            var StarsDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = StarsDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var StarsDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("StarsDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = StarsDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = StarsDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = StarsDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Stars Subdomain");
                bp.SetDescription("\nThe firmament provides you inspiration, and you draw power from the stars’ distant light.\nGuarded Mind: You gain a +2 insight bonus on saving throws " +
                    "against all mind-affecting effects.\nThe Stars Are Right: At 8th level, you may spontaneously cast any of your Stars subdomain spells by swapping out a spell of an equal " +
                    "spell level. Any Stars subdomain spell that you cast heals you an amount of hit point damage equal to the spell’s level; this effect happens as you cast the spell.\nDomain " +
                    "{g|Encyclopedia:Spell}Spells{/g}: entropic shield, hypnotic pattern, blink, dimension door, summon monster V, overwhelming presence, sunbeam, sunburst, polar midnight/meteor swarm.");
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
                    Helpers.LevelEntry(1, StarsDomainBaseFeature),
                    Helpers.LevelEntry(8, StarsDomainGreaterFeature1, StarsDomainGreaterFeature2, StargazerStarsDomainGreaterFeatureCheck)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(StarsDomainBaseFeature, StarsDomainGreaterFeature1)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var StarsDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("StarsDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = StarsDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = StarsDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Stars Subdomain");
                bp.SetDescription("\nThe firmament provides you inspiration, and you draw power from the stars’ distant light.\nGuarded Mind: You gain a +2 insight bonus on saving throws " +
                    "against all mind-affecting effects.\nThe Stars Are Right: At 8th level, you may spontaneously cast any of your Stars subdomain spells by swapping out a spell of an equal " +
                    "spell level. Any Stars subdomain spell that you cast heals you an amount of hit point damage equal to the spell’s level; this effect happens as you cast the spell.\nDomain " +
                    "{g|Encyclopedia:Spell}Spells{/g}: entropic shield, hypnotic pattern, blink, dimension door, summon monster V, overwhelming presence, sunbeam, sunburst, polar midnight/meteor swarm.");
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
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, StarsDomainBaseFeature),
                    Helpers.LevelEntry(8, StarsDomainGreaterFeature1, StarsDomainGreaterFeature2, StargazerStarsDomainGreaterFeatureCheck)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(StarsDomainBaseFeature, StarsDomainGreaterFeature1)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });            
            StarsDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() { 
                StarsDomainProgression.ToReference<BlueprintFeatureReference>(),
                StarsDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            StarsDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = StarsDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            }); 
            StarsDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = StarsDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });


            //Separatist versions
            var StarsDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("StarsDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });




            var StarsDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("StarsDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = StarsDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = StarsDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = StarsDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = StarsDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = StarsDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Stars Subdomain");
                bp.SetDescription("\nThe firmament provides you inspiration, and you draw power from the stars’ distant light.\nGuarded Mind: You gain a +2 insight bonus on saving throws " +
                    "against all mind-affecting effects.\nThe Stars Are Right: At 8th level, you may spontaneously cast any of your Stars subdomain spells by swapping out a spell of an equal " +
                    "spell level. Any Stars subdomain spell that you cast heals you an amount of hit point damage equal to the spell’s level; this effect happens as you cast the spell.\nDomain " +
                    "{g|Encyclopedia:Spell}Spells{/g}: entropic shield, hypnotic pattern, blink, dimension door, summon monster V, overwhelming presence, sunbeam, sunburst, polar midnight/meteor swarm.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, StarsDomainBaseFeature),
                    Helpers.LevelEntry(10, StarsDomainGreaterFeature1, StarsDomainGreaterFeature2, StargazerStarsDomainGreaterFeatureCheck)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(StarsDomainBaseFeature, StarsDomainGreaterFeature1)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            StarsDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                StarsDomainProgression.ToReference<BlueprintFeatureReference>(),
                StarsDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            StarsDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = StarsDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            StarsDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = StarsDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            StarsDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = StarsDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            StarsDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = StarsDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            StarsDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = StarsDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });

            //Adding to stargazer
            StargazerMysteryMagicStarsDomainFeatureSelection.m_AllFeatures = StargazerMysteryMagicStarsDomainFeatureSelection.m_AllFeatures.AppendToArray(
                StarsDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
                );
            var StargazerMysteryMagicStarsDomainBackupDomainSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("StargazerMysteryMagicStarsDomainBackupDomainSelection");
            StargazerMysteryMagicStarsDomainBackupDomainSelection.AddComponent<PrerequisiteFeaturesFromList>(c => {
                c.Amount = 1;
                c.m_Features = new BlueprintFeatureReference[] {
                    StarsDomainProgression.ToReference<BlueprintFeatureReference>(),
                    StarsDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
                };
            });

            if (ModSettings.AddedContent.Domains.IsDisabled("Stars Subdomain")) { return; }
            DomainTools.RegisterDomain(StarsDomainProgression);
            DomainTools.RegisterSecondaryDomain(StarsDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(StarsDomainProgression);
            DomainTools.RegisterTempleDomain(StarsDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(StarsDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(StarsDomainProgression, StarsDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(StarsDomainProgressionSeparatist);

        }
    }
}
