using BlueprintCore.Utils.Assets;
using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Classes;
using ExpandedContent.Tweaks.Components;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UI.GenericSlot;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System.Collections.Generic;
using UnityEngine;

namespace ExpandedContent.Tweaks.Mysteries {
    internal class LunarMystery {




        public static void AddLunarMystery() {

            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var OracleRevelationSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("60008a10ad7ad6543b1f63016741a5d2");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var RavenerHunterArchetype = Resources.GetModBlueprint<BlueprintArchetype>("RavenerHunterArchetype");
            var PlantType = Resources.GetBlueprint<BlueprintFeature>("706e61781d692a042b35941f14bc41c5");
            var LunarMysteryIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleLunarMystery.png");

            var ImmunityToMindAffecting = Resources.GetBlueprint<BlueprintFeature>("3eb606c0564d0814ea01a824dbe42fb0");
            var MasterShapeshifter = Resources.GetBlueprint<BlueprintFeature>("934670ef88b281b4da5596db8b00df2f");
            var BestialHowlFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("87ab3793abc347d5a47fc6b7fec8dfcb");
            var WerewolfBleedFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("502d36f8b703420397ca35d5f523b8f0");
            var ShiftersRush = Resources.GetBlueprintReference<BlueprintBuffReference>("c3365d5a75294b9b879c587668620bd4");
            var ShifterWildShapeWereRatBuff = Resources.GetBlueprint<BlueprintBuff>("9261713e33624de599d6183d6b7cf2e4");
            var ShifterWildShapeWereTigerBuff = Resources.GetBlueprint<BlueprintBuff>("1bc5f96600c74a079c8a0c6dafeb3320");
            var ShifterWildShapeWereWolfBuff = Resources.GetBlueprint<BlueprintBuff>("34273feba56448bc968dd5482cdfffc7");

            //Spelllist
            var HypnotismSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("88367310478c10b47903463c5d0152b0");
            var DustOfTwilightAbility = Resources.GetModBlueprint<BlueprintAbility>("DustOfTwilightAbility");
            var RageSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("97b991256e43bb140b263c326f690ce2");
            var MoonstruckAbility = Resources.GetModBlueprint<BlueprintAbility>("MoonstruckAbility");
            var AspectOfTheWolfSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("6126b36fe22291543ad72f8b9f0d53a7");
            var LitanyOfMadnessSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("435e73bcff18f304293484f9511b4672");
            var PrimalRegressionSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("07d577a74441a3a44890e3006efcf604");
            var BloodMistAbility = Resources.GetModBlueprint<BlueprintAbility>("BloodMistAbility");
            var PolarMidnightSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("ba48abb52b142164eba309fd09898856");
            var BreakEnchantmentSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("7792da00c85b9e042a0fdfc2b66ec9a8");
            var OwlsWisdomMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("9f5ada581af3db4419b54db77f44e430");
            var OwlsWisdomSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f0455c9295b53904f9e02fc571dd2ce1");
            var RemoveBlindnessSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c927a8b0cd3f5174f8c0b67cdbfde539");
            var ConfusionSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("cf6c901fb7acc904e85c63b342e9c949");
            var MindBlankSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("df2a0ba6b6dcecf429cbb80a56fee5cf");
            var MindBlankMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("87a29febd010993419f2a4a9bee11cfc");
            var TrueSeeingSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4cf3d0fae3239ec478f51e86f49161cb");
            var TrueSeeingMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fa08cb49ade3eee42b5fd42bd33cb407");
            var SoundBurstSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c3893092a333b93499fd0a21845aa265");
            var ShoutSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f09453607e683784c8fca646eec49162");
            var SongOfDiscordSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d38aaf487e29c3d43a3bffa4a4a55f8f");
            var ShoutGreaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fd0d3840c48cafb44bb29e8eb74df204");
            var BrilliantInspirationSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("a5c56f0f699daec44b7aedd8b273b08a");
            var OracleLunarSpells = Helpers.CreateBlueprint<BlueprintFeature>("OracleLunarSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HypnotismSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DustOfTwilightAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = RageSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MoonstruckAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = AspectOfTheWolfSpell;
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = LitanyOfMadnessSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PrimalRegressionSpell;
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BloodMistAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PolarMidnightSpell;
                    c.SpellLevel = 9;
                });
            });
            var EnlightnedPhilosopherLunarSpells = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherLunarSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HypnotismSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = OwlsWisdomSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = RemoveBlindnessSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ConfusionSpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueSeeingSpell;
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = OwlsWisdomMassSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueSeeingMassSpell;
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MindBlankSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MindBlankMassSpell;
                    c.SpellLevel = 9;
                });
            });
            var OceansEchoLunarSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoLunarSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HypnotismSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = RageSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutSpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SongOfDiscordSpell;
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutGreaterSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BrilliantInspirationSpell;
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BloodMistAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PolarMidnightSpell;
                    c.SpellLevel = 9;
                });
            });
            //Final Revelation
            var EnlightenedPhilosopherFinalRevelationBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("9f1ee3c61ef993d448b0b866ee198ea8");
            var EnlightenedPhilosopherFinalRevelationResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("d19c2e7ec505b734a973ce8d0986f4d6");
            var OracleLunarFinalRevelationResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleLunarFinalRevelationResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    LevelIncrease = 0,
                    IncreasedByLevelStartPlusDivStep = false,
                    StartingLevel = 0,
                    StartingIncrease = 0,
                    LevelStep = 0,
                    PerStepIncrease = 0,
                    MinClassLevelIncrease = 0,
                    OtherClassesModifier = 0,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Unknown
                };
            });
            var OracleLunarFinalRevelationWolfBleed = Helpers.CreateBlueprint<BlueprintBuff>("OracleLunarFinalRevelationWolfBleed", bp => {
                bp.SetName("Wolves Wounds");
                bp.SetDescription("Claw wounds cause the target to bleed for 5 damage each round, stacking up to 5 times. Bleeding can be stopped through the application " +
                    "of any {g|Encyclopedia:Spell}spell{/g} that cures hit point damage.");
                bp.m_Icon = LunarMysteryIcon;
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = PhysicalDamageForm.Slashing,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Fire,
                                Type = DamageType.Direct
                            },
                            Drain = false,
                            AbilityType = StatType.Unknown,
                            Duration = new ContextDurationValue() {
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
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true,
                            },
                            PreRolledSharedValue = AbilitySharedValue.Damage,
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.One,
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
                            },
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionIsInCombat() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
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
                        }
                        );
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
                bp.Ranks = 5;
                bp.Frequency = DurationRate.Rounds;
            });
            var OracleLunarFinalRevelationRatBuff = Helpers.CreateBuff("OracleLunarFinalRevelationRatBuff", bp => {
                bp.SetName("Lycan Shape - Wererat");
                bp.SetDescription("Once per day, you can transform into a wererat for a number of hours equal to your Charisma modifier. " +
                    "\nWererats gain: Two claw attacks (1d4) a bite attack (1d6) and free disarm attempts on bite attacks. " +
                    "\nJump up, weakening wound, crippling strike and opportunist rogue talents." +
                    "\n3d6 sneak attack dice, stacking with other sneak attack dice. " +
                    "\n{g|Encyclopedia:Damage_Reduction}DR{/g} 5/silver.");
                bp.m_Icon = LunarMysteryIcon;
                bp.AddComponent(ShifterWildShapeWereRatBuff.GetComponent<Polymorph>());
                bp.AddComponent<SpellDescriptorComponent>(c => { c.Descriptor = SpellDescriptor.Polymorph; });
                bp.AddComponent(ShifterWildShapeWereRatBuff.GetComponent<ReplaceAsksList>());
                bp.AddComponent(ShifterWildShapeWereRatBuff.GetComponent<ReplaceSourceBone>());
                bp.AddComponent(ShifterWildShapeWereRatBuff.GetComponent<ReplaceCastSource>());
                bp.AddComponent<ManeuverOnAttack>(c => { c.Category = WeaponCategory.Bite; c.Maneuver = CombatManeuver.Disarm; });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Feat;
                    c.Stat = StatType.SneakAttack;
                    c.Value = 3;
                });
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.Value = 5;
                    c.BypassedByMaterial = true;
                    c.Material = PhysicalDamageMaterial.Silver;
                });
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Replace;
            });
            var OracleLunarFinalRevelationTigerBuff = Helpers.CreateBuff("OracleLunarFinalRevelationTigerBuff", bp => {
                bp.SetName("Lycan Shape - Weretiger");
                bp.SetDescription("Once per day, you can transform into a weretiger for a number of hours equal to your Charisma modifier. " +
                    "\nWeretigers gain: Two claw attacks (2d8) a bite attack (1d10). " +
                    "\nCombat expertise, pounce and lunge as bonus feats." +
                    "\nA +5 racial bonus to AC and {g|Encyclopedia:Fast_Healing}fast healing{/g} 5." +
                    "\n{g|Encyclopedia:Damage_Reduction}DR{/g} 15/silver.");
                bp.m_Icon = LunarMysteryIcon;
                bp.AddComponent(ShifterWildShapeWereTigerBuff.GetComponent<Polymorph>());
                bp.AddComponent<SpellDescriptorComponent>(c => { c.Descriptor = SpellDescriptor.Polymorph; });
                bp.AddComponent(ShifterWildShapeWereTigerBuff.GetComponent<ReplaceAsksList>());
                bp.AddComponent(ShifterWildShapeWereTigerBuff.GetComponent<ReplaceSourceBone>());
                bp.AddComponent(ShifterWildShapeWereTigerBuff.GetComponent<ReplaceCastSource>());
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Racial;
                    c.Stat = StatType.AC;
                    c.Value = 5;
                });
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.Value = 15;
                    c.BypassedByMaterial = true;
                    c.Material = PhysicalDamageMaterial.Silver;
                });
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Replace;
            });
            var OracleLunarFinalRevelationWolfBuff = Helpers.CreateBuff("OracleLunarFinalRevelationWolfBuff", bp => {
                bp.SetName("Lycan Shape - Werewolf");
                bp.SetDescription("Once per day, you can transform into a werewolf for a number of hours equal to your Charisma modifier. " +
                    "\nWerewolves gain: Two claw attacks (1d4) a bite attack (1d6) and free trip attempts on bite attacks. " +
                    "\nTrip and greater trip as bonus feats." +
                    "\nClaw attacks cause the target to bleed for 5 damage each round, stacking up to 5 times." +
                    "\n{g|Encyclopedia:Damage_Reduction}DR{/g} 10/silver.");
                bp.m_Icon = LunarMysteryIcon;
                bp.AddComponent(ShifterWildShapeWereWolfBuff.GetComponent<Polymorph>());
                bp.AddComponent<SpellDescriptorComponent>(c => { c.Descriptor = SpellDescriptor.Polymorph; });
                bp.AddComponent(ShifterWildShapeWereWolfBuff.GetComponent<ReplaceAsksList>());
                bp.AddComponent(ShifterWildShapeWereWolfBuff.GetComponent<ReplaceSourceBone>());
                bp.AddComponent(ShifterWildShapeWereWolfBuff.GetComponent<ReplaceCastSource>());
                bp.AddComponent<ManeuverOnAttack>(c => { c.Category = WeaponCategory.Bite; c.Maneuver = CombatManeuver.Trip; });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = true;
                    c.Category = WeaponCategory.Claw;
                    c.CheckWeaponGroup = true;
                    c.Group = WeaponFighterGroup.Natural;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.CheckPhysicalDamageForm = true;
                    c.DamageForm = PhysicalDamageForm.Piercing | PhysicalDamageForm.Slashing;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = false;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleLunarFinalRevelationWolfBleed.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                m_IsExtendable = true,
                            }
                        }
                        );
                });
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.Value = 10;
                    c.BypassedByMaterial = true;
                    c.Material = PhysicalDamageMaterial.Silver;
                });
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Replace;
            });
            OracleLunarFinalRevelationWolfBuff.GetComponent<Polymorph>().m_Facts.RemoveAll(f => f.deserializedGuid == WerewolfBleedFeature.deserializedGuid);
            var PolymorphCleanup = new Polymorph[] {
                OracleLunarFinalRevelationRatBuff.GetComponent<Polymorph>(),
                OracleLunarFinalRevelationTigerBuff.GetComponent<Polymorph>(),
                OracleLunarFinalRevelationWolfBuff.GetComponent<Polymorph>()
            };
            foreach (var polymorph in PolymorphCleanup) {
                polymorph.m_Facts.RemoveAll(f => f.deserializedGuid == BestialHowlFeature.deserializedGuid);
            }
            var OracleLunarFinalRevelationAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleLunarFinalRevelationAbility", bp => {
                bp.SetName("Lycan Shape");
                bp.SetDescription("Once per day, you can transform into a lycanthrope of your choice for a number of hours equal to your Charisma modifier, " +
                    "gaining all the powers of a natural lycanthrope of that type. (The Master Shapeshifter mythic ability allows this to be cast unlimited times per day, " +
                    "without time limit.)");
                bp.m_Icon = LunarMysteryIcon;
                //Variants added after
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleLunarFinalRevelationResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleLunarFinalRevelationRatAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleLunarFinalRevelationRatAbility", bp => {
                bp.SetName("Lycan Shape - Wererat");
                bp.SetDescription("Once per day, you can transform into a wererat for a number of hours equal to your Charisma modifier. " +
                    "\nWererats gain: Two claw attacks (1d4) a bite attack (1d6) and free disarm attempts on bite attacks. " +
                    "\nJump up, weakening wound, crippling strike and opportunist rogue talents." +
                    "\n3d6 sneak attack dice, stacking with other sneak attack dice. " +
                    "\n{g|Encyclopedia:Damage_Reduction}DR{/g} 5/silver.");
                bp.m_Icon = LunarMysteryIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = ShiftersRush,
                            ToCaster = true
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        Not = false,
                                        m_Fact = MasterShapeshifter.ToReference<BlueprintUnitFactReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleLunarFinalRevelationRatBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,//!
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Hours,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    },
                                    ToCaster = true
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleLunarFinalRevelationRatBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,//!
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Hours,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    },
                                    ToCaster = true
                                })
                        }
                        );
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleLunarFinalRevelationResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = OracleLunarFinalRevelationAbility.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleLunarFinalRevelationTigerAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleLunarFinalRevelationTigerAbility", bp => {
                bp.SetName("Lycan Shape - Weretiger");
                bp.SetDescription("Once per day, you can transform into a weretiger for a number of hours equal to your Charisma modifier. " +
                    "\nWeretigers gain: Two claw attacks (2d8) a bite attack (1d10). " +
                    "\nCombat expertise, pounce and lunge as bonus feats." +
                    "\nA +5 racial bonus to AC and {g|Encyclopedia:Fast_Healing}fast healing{/g} 5." +
                    "\n{g|Encyclopedia:Damage_Reduction}DR{/g} 15/silver.");
                bp.m_Icon = LunarMysteryIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = ShiftersRush,
                            ToCaster = true
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        Not = false,
                                        m_Fact = MasterShapeshifter.ToReference<BlueprintUnitFactReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleLunarFinalRevelationTigerBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,//!
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Hours,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    },
                                    ToCaster = true
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleLunarFinalRevelationTigerBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,//!
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Hours,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    },
                                    ToCaster = true
                                })
                        }
                        );
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleLunarFinalRevelationResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = OracleLunarFinalRevelationAbility.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleLunarFinalRevelationWolfAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleLunarFinalRevelationWolfAbility", bp => {
                bp.SetName("Lycan Shape - Werewolf");
                bp.SetDescription("Once per day, you can transform into a werewolf for a number of hours equal to your Charisma modifier. " +
                    "\nWerewolves gain: Two claw attacks (1d4) a bite attack (1d6) and free trip attempts on bite attacks. " +
                    "\nTrip and greater trip as bonus feats." +
                    "\nClaw attacks cause the target to bleed for 5 damage each round, stacking up to 5 times." +
                    "\n{g|Encyclopedia:Damage_Reduction}DR{/g} 10/silver.");
                bp.m_Icon = LunarMysteryIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = ShiftersRush,
                            ToCaster = true
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        Not = false,
                                        m_Fact = MasterShapeshifter.ToReference<BlueprintUnitFactReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleLunarFinalRevelationWolfBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,//!
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Hours,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    },
                                    ToCaster = true
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleLunarFinalRevelationWolfBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,//!
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Hours,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    },
                                    ToCaster = true
                                })
                        }
                        );
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleLunarFinalRevelationResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { MasterShapeshifter.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_Parent = OracleLunarFinalRevelationAbility.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            OracleLunarFinalRevelationAbility.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    OracleLunarFinalRevelationRatAbility.ToReference<BlueprintAbilityReference>(),
                    OracleLunarFinalRevelationTigerAbility.ToReference<BlueprintAbilityReference>(),
                    OracleLunarFinalRevelationWolfAbility.ToReference<BlueprintAbilityReference>()
                };
            });
            var OracleLunarFinalRevelation = Helpers.CreateBlueprint<BlueprintFeature>("OracleLunarFinalRevelation", bp => {
                bp.SetName("Final Revelation");
                bp.SetDescription("Upon reaching 20th level, you become an avatar of the primal world. Once per day, you can transform into a lycanthrope of your choice for a number " +
                    "of hours equal to your Charisma modifier, gaining all the powers of a natural lycanthrope of that type. In addition, you become immune to mind-affecting effects.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ImmunityToMindAffecting.ToReference<BlueprintUnitFactReference>(),
                        OracleLunarFinalRevelationAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleLunarFinalRevelationResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.IsClassFeature = true;
            });
            //Main Mystery Feature
            var OracleLunarMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleLunarMysteryFeature", bp => {
                bp.m_Icon = LunarMysteryIcon;
                bp.SetName("Lunar");
                bp.SetDescription("An oracle with the lunar mystery adds {g|Encyclopedia:Mobility}Mobility{/g}, {g|Encyclopedia:Lore_Nature}Lore (nature){/g}, " +
                    "{g|Encyclopedia:Perception}Perception{/g}  and {g|Encyclopedia:Knowledge_World}Knowledge (World) {/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleLunarFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleLunarSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillMobility;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPerception;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //EnlightnedPhilosopherMystery
            var EnlightnedPhilosopherLunarMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherLunarMysteryFeature", bp => {
                bp.m_Icon = LunarMysteryIcon;
                bp.SetName("Lunar");
                bp.SetDescription("An oracle with the lunar mystery adds {g|Encyclopedia:Mobility}Mobility{/g}, {g|Encyclopedia:Lore_Nature}Lore (nature){/g}, " +
                    "{g|Encyclopedia:Perception}Perception{/g}  and {g|Encyclopedia:Knowledge_World}Knowledge (World) {/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = EnlightnedPhilosopherLunarSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillMobility;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPerception;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.EnlightenedPhilosopherMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //DivineHerbalistMystery
            var DivineHerbalistLunarMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("DivineHerbalistLunarMysteryFeature", bp => {
                bp.m_Icon = LunarMysteryIcon;
                bp.SetName("Lunar");
                bp.SetDescription("Gain access to the spells and revelations of the lunar mystery. \nDue to the divine herbalist archetype the class skills gained from this archetype" +
                    "are replaced by the master herbalist feature and the brew potions feat.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleLunarFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleLunarSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.DivineHerbalistMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //Ocean's Echo
            var OceansEchoLunarMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoLunarMysteryFeature", bp => {
                bp.m_Icon = LunarMysteryIcon;
                bp.SetName("Lunar");
                bp.SetDescription("Gain access to the spells and revelations of the lunar mystery. \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleLunarFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoLunarSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.None };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //RavenerHunter
            var RavenerHunterLunarMysteryProgression = Helpers.CreateBlueprint<BlueprintProgression>("RavenerHunterLunarMysteryProgression", bp => {
                bp.SetName("Lunar");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.m_Icon = LunarMysteryIcon;
                bp.m_AllowNonContextActions = false;
                bp.Groups = new FeatureGroup[] { FeatureGroup.None };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                //LevelEntry added later                
                bp.GiveFeaturesForPreviousLevels = true;
            });
            //FormOfTheBeast
            var OracleRevelationBeastFormResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationBeastFormResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    LevelIncrease = 0,
                    IncreasedByLevelStartPlusDivStep = false,
                    StartingLevel = 0,
                    StartingIncrease = 0,
                    LevelStep = 0,
                    PerStepIncrease = 0,
                    MinClassLevelIncrease = 0,
                    OtherClassesModifier = 0,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Unknown,
                };
            });
            var BeastShapeAbilityI = Resources.GetBlueprint<BlueprintAbility>("61a7ed778dd93f344a5dacdbad324cc9");
            var BeastShapeAbilityII = Resources.GetBlueprint<BlueprintAbility>("5d4028eb28a106d4691ed1b92bbb1915");
            var BeastShapeAbilityIII = Resources.GetBlueprint<BlueprintAbility>("9b93040dad242eb43ac7de6bb6547030");
            var BeastShapeAbilityIVS = Resources.GetBlueprint<BlueprintAbility>("502cd7fd8953ac74bb3a3df7e84818ae");
            var BeastShapeAbilityIVW = Resources.GetBlueprint<BlueprintAbility>("3fa892e5e3efa364fb3d2692738a7c15");
            var BeastShapeIBuff = Resources.GetBlueprint<BlueprintBuff>("00d8fbe9cf61dc24298be8d95500c84b");
            var BeastShapeIIBuff = Resources.GetBlueprint<BlueprintBuff>("200bd9b179ee660489fe88663115bcbc");
            var BeastShapeIIIBuff = Resources.GetBlueprint<BlueprintBuff>("0c0afabcfddeecc40a1545a282f2bec8");
            var BeastShapeIVSmilodonBuff = Resources.GetBlueprint<BlueprintBuff>("c38def68f6ce13b4b8f5e5e0c6e68d08");
            var BeastShapeIVWyvernBuff = Resources.GetBlueprint<BlueprintBuff>("dae2d173d9bd5b14dbeb4a1d9d9b0edc");
            var OracleRevelationBeastFormIAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationBeastFormIAbility", bp => {
                bp.SetName("Beast Form (Wolf)");
                bp.m_Description = BeastShapeAbilityI.m_Description;
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationBeastFormResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BeastShapeIBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { BeastShapeIBuff.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 0;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
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
                bp.m_Icon = BeastShapeIBuff.Icon;
                bp.Type = AbilityType.Supernatural;
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
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationBeastFormIAbility.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationBeastFormIIAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationBeastFormIIAbility", bp => {
                bp.SetName("Beast Form (Leopard)");
                bp.m_Description = BeastShapeAbilityII.m_Description;
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationBeastFormResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BeastShapeIIBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { BeastShapeIIBuff.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 0;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
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
                bp.m_Icon = BeastShapeIIBuff.Icon;
                bp.Type = AbilityType.Supernatural;
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
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationBeastFormIIAbility.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationBeastFormIIIAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationBeastFormIIIAbility", bp => {
                bp.SetName("Beast Form (Bear)");
                bp.m_Description = BeastShapeAbilityIII.m_Description;
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationBeastFormResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BeastShapeIIIBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { BeastShapeIIIBuff.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 0;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
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
                bp.m_Icon = BeastShapeIIIBuff.Icon;
                bp.Type = AbilityType.Supernatural;
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
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationBeastFormIIIAbility.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationBeastFormIVBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationBeastFormIVBaseAbility", bp => {
                bp.SetName("Beast Form (Final)");
                bp.SetDescription("You become a large smilodon or a large wyvern.");                
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationBeastFormResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
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
                bp.m_Icon = BeastShapeIVWyvernBuff.Icon;
                bp.Type = AbilityType.Supernatural;
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
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationBeastFormIVBaseAbility.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationBeastFormIVSAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationBeastFormIVSAbility", bp => {
                bp.SetName("Beast Form (Smilodon)");
                bp.m_Description = BeastShapeAbilityIVS.m_Description;
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationBeastFormResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BeastShapeIVSmilodonBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { BeastShapeIVSmilodonBuff.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 0;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
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
                bp.m_Icon = BeastShapeIVSmilodonBuff.Icon;
                bp.m_Parent = OracleRevelationBeastFormIVBaseAbility.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
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
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationBeastFormIVSAbility.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationBeastFormIVWAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationBeastFormIVWAbility", bp => {
                bp.SetName("Beast Form (Wyvern)");
                bp.m_Description = BeastShapeAbilityIVW.m_Description;
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationBeastFormResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BeastShapeIVWyvernBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
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
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { BeastShapeIVWyvernBuff.ToReference<BlueprintUnitFactReference>() };
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
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 0;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
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
                bp.m_Icon = BeastShapeIVWyvernBuff.Icon;
                bp.m_Parent = OracleRevelationBeastFormIVBaseAbility.ToReference<BlueprintAbilityReference>();
                bp.Type = AbilityType.Supernatural;
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
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationBeastFormIVWAbility.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            OracleRevelationBeastFormIVBaseAbility.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    OracleRevelationBeastFormIVSAbility.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationBeastFormIVWAbility.ToReference<BlueprintAbilityReference>()
                };
            });
            var OracleRevelationBeastFormFeature1 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationBeastFormFeature1", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationBeastFormIAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationBeastFormFeature2 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationBeastFormFeature2", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationBeastFormIIAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationBeastFormFeature3 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationBeastFormFeature3", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationBeastFormIIIAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationBeastFormFeature4 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationBeastFormFeature4", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationBeastFormIVBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationBeastFormProgression = Helpers.CreateBlueprint<BlueprintProgression>("OracleRevelationBeastFormProgression", bp => {
                bp.SetName("Form of the Beast");
                bp.SetDescription("You can assume the form of a medium wolf, as beast shape I. " +
                    "At 9th level, you can assume the form of a medium leopard, as beast shape II. " +
                    "At 11th level, you can assume the form of a large bear, as beast shape II." +
                    "At 13th level, you can assume the form of a large smilodon or a large wyvern, as beast shape IV. You can use this ability once per day, but the duration is 1 " +
                    "hour/level. You must be at least 7th level to select this revelation.");
                bp.m_Icon = BeastShapeIVWyvernBuff.Icon;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(7, OracleRevelationBeastFormFeature1),
                    Helpers.LevelEntry(9, OracleRevelationBeastFormFeature2),
                    Helpers.LevelEntry(11, OracleRevelationBeastFormFeature3),
                    Helpers.LevelEntry(13, OracleRevelationBeastFormFeature4)
                };
                bp.GiveFeaturesForPreviousLevels = true;
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleLunarMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherLunarMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistLunarMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoLunarMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        RavenerHunterLunarMysteryProgression.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Group = Prerequisite.GroupType.Any;
                    c.Level = 7;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 7;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationBeastFormResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationBeastFormProgression.ToReference<BlueprintFeatureReference>());
            //EyeOfTheMoon

            //GiftOfClawAndHorn

            //MantleOfMoonlight ????

            //Moonbeam
            var OracleRevelationMoonbeamResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationMoonbeamResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Charisma,
                };
            });
            var RayOfExhaustion00 = Resources.GetBlueprint<BlueprintProjectile>("8e38d2cfc358e124e93c792dea56ff9a");
            var RayWeapon = Resources.GetBlueprint<BlueprintItemWeapon>("f6ef95b1f7bb52b408a5b345a330ffe8");
            var Blind = Resources.GetBlueprint<BlueprintBuff>("0ec36e7596a4928489d2049e1e1c76a7");
            var GloomblindBoltsIcon = AssetLoader.LoadInternal("Skills", "Icon_GloomblindBolts.jpg");
            var OracleRevelationMoonbeamAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationMoonbeamAbility", bp => {
                bp.SetName("Moonbeam");
                bp.SetDescription("You can fire a ray of moonlight as a ranged touch attack at any creature within 30 feet. This ray deals 1d6 points of damage + 1 for every 2 oracle levels you possess. " +
                    "In addition, the target must succeed at a Fortitude save or become blinded for 1 round. You can use this ability a number of times per day equal to your Charisma modifier (minimum 1).");
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        RayOfExhaustion00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Simple;
                    c.IsHandOfTheApprentice = false;
                    c.m_Length = 0.Feet();
                    c.m_LineWidth = 5.Feet();
                    c.NeedAttackRoll = true;
                    c.m_Weapon = RayWeapon.ToReference<BlueprintItemWeaponReference>();
                    c.ReplaceAttackRollBonusStat = false;
                    c.AttackRollBonusStat = StatType.Unknown;
                    c.UseMaxProjectilesCount = true;
                    c.MaxProjectilesCountRank = AbilityRankType.ProjectilesCount;
                    c.DelayBetweenProjectiles = 0.0f;
                    c.m_ControlledProjectileHolderBuff = null; //?
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = 0,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Divine
                            },
                            Drain = false,
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
                            Duration = new ContextDurationValue() {
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
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true,
                            },
                            PreRolledSharedValue = AbilitySharedValue.Damage,
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
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
                            },
                            IsAoE = false,
                            HalfIfSaved = false,
                            UseMinHPAfterDamage = false,
                            MinHPAfterDamage = 0,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionSavingThrow() {
                            Type = SavingThrowType.Fortitude,
                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                            UseDCFromContextSavingThrow = false,
                            HasCustomDC = false,
                            CustomDC = new ContextValue(),
                            Actions = Helpers.CreateActionList(
                                new ContextActionConditionalSaved() {
                                    Succeed = Helpers.CreateActionList(),
                                    Failed = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = Blind.ToReference<BlueprintBuffReference>(),
                                            Permanent = false,
                                            UseDurationSeconds = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = new ContextValue() {
                                                    ValueType = ContextValueType.Simple,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage
                                                },
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Simple,
                                                    Value = 1,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage
                                                }
                                            },
                                            DurationSeconds = 0
                                        }
                                        ),
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Damage;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationMoonbeamResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Icon = GloomblindBoltsIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationMoonbeamAbility.Duration", "1 round");
                bp.LocalizedSavingThrow = Helpers.CreateString("OracleRevelationMoonbeamAbility.SavingThrow", "Fortitude negates (blindness)");
            });
            var OracleRevelationMoonbeamFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationMoonbeamFeature", bp => {
                bp.SetName("Moonbeam");
                bp.SetDescription("You can fire a ray of moonlight as a ranged touch attack at any creature within 30 feet. This ray deals 1d6 points of damage + 1 for every 2 oracle levels you possess. " +
                    "In addition, the target must succeed at a Fortitude save or become blinded for 1 round. You can use this ability a number of times per day equal to your Charisma modifier (minimum 1).");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationMoonbeamAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleLunarMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherLunarMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistLunarMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoLunarMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        RavenerHunterLunarMysteryProgression.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationMoonbeamResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationMoonbeamFeature.ToReference<BlueprintFeatureReference>());
            //PrimalCompanion
            var OracleBondedMountProgression = Resources.GetBlueprint<BlueprintProgression>("7d1c29c3101dd7643a625448fbbaa919");
            var AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");
            var AnimalCompanionArchetypeSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("65af7290b4efd5f418132141aaa36c1b");
            var MountTargetFeature = Resources.GetBlueprint<BlueprintFeature>("cb06f0e72ffb5c640a156bd9f8000c1d");
            var OracleRevalationBondedMountSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("0234d0dd1cead22428e71a2500afa2e1");
            var AnimalCompanionEmptyCompanion = Resources.GetBlueprintReference<BlueprintFeatureReference>("472091361cf118049a2b4339c4ea836a");
            var AnimalCompanionFeatureBear = Resources.GetBlueprintReference<BlueprintFeatureReference>("f6f1cdcc404f10c4493dc1e51208fd6f");
            var AnimalCompanionFeatureBoar = Resources.GetBlueprintReference<BlueprintFeatureReference>("afb817d80b843cc4fa7b12289e6ebe3d");
            var AnimalCompanionFeatureSmilodon = Resources.GetBlueprintReference<BlueprintFeatureReference>("126712ef923ab204983d6f107629c895");
            var AnimalCompanionFeatureVelociraptor = Resources.GetBlueprintReference<BlueprintFeatureReference>("89420de28b6bb9443b62ce489ae5423b");
            var AnimalCompanionFeatureWolf = Resources.GetBlueprintReference<BlueprintFeatureReference>("67a9dc42b15d0954ca4689b13e8dedea");
            var AnimalCompanionFeatureSmilodonPreorder = Resources.GetBlueprintReference<BlueprintFeatureReference>("44f4d77689434e07a5a44dcb65b25f71");
            var CompanionWebSpiderFeature = Resources.GetModBlueprint<BlueprintFeature>("CompanionWebSpiderFeature");
            var OracleRevelationPrimalCompanion = Helpers.CreateBlueprint<BlueprintFeatureSelection>("OracleRevelationPrimalCompanion", bp => {
                bp.SetName("Primal Companion");
                bp.SetDescription("You gain the service of a faithful animal of the night. You can select from a bear, boar, smilodon, spider, velociraptor, or wolf. This animal functions as a " +
                    "druid’s animal companion, using your oracle level as your effective druid level.");
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = OracleBondedMountProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = AnimalCompanionArchetypeSelection.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = MountTargetFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleLunarMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherLunarMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistLunarMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoLunarMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        RavenerHunterLunarMysteryProgression.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.CheckInProgression = false;
                    c.HideInUI = true;
                    c.m_Feature = OracleRevalationBondedMountSelection.ToReference<BlueprintFeatureReference>();
                });
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.OracleRevelation;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddFeatures(AnimalCompanionFeatureBear, AnimalCompanionFeatureBoar, AnimalCompanionFeatureSmilodon, AnimalCompanionFeatureVelociraptor, AnimalCompanionFeatureWolf,
                    AnimalCompanionFeatureSmilodonPreorder, CompanionWebSpiderFeature, AnimalCompanionEmptyCompanion);
            });
            OracleRevelationPrimalCompanion.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.CheckInProgression = false;
                c.HideInUI = true;
                c.m_Feature = OracleRevelationPrimalCompanion.ToReference<BlueprintFeatureReference>();
            });
            OracleRevalationBondedMountSelection.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.CheckInProgression = false;
                c.HideInUI = true;
                c.m_Feature = OracleRevelationPrimalCompanion.ToReference<BlueprintFeatureReference>();
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationPrimalCompanion.ToReference<BlueprintFeatureReference>());
            //PropheticArmor
            var OracleRevelationNatureWhispers = Resources.GetBlueprint<BlueprintFeature>("3d2cd23869f0d98458169b88738f3c32");
            var OracleRevelationPropheticArmorBuff = Helpers.CreateBuff("OracleRevelationPropheticArmorBuff", bp => {
                bp.AddComponent<ReplaceStatBaseAttribute>(c => {
                    c.TargetStat = StatType.AC;
                    c.BaseAttributeReplacement = StatType.Charisma;
                    c.ReplaceIfHigher = true;
                });
                bp.AddComponent<ReplaceStatBaseAttribute>(c => {
                    c.TargetStat = StatType.SaveReflex;
                    c.BaseAttributeReplacement = StatType.Charisma;
                    c.ReplaceIfHigher = true;
                });
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
            });
            var OracleRevelationPropheticArmorFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationPropheticArmorFeature", bp => {
                bp.SetName("Prophetic Armor");
                bp.SetDescription("You are so in tune with your primal nature that your instincts often act to save you from danger that your civilized mind isn’t even aware of. " +
                    "You may use your Charisma modifier (instead of your Dexterity modifier) as part of your Armor Class and all Reflex saving throws. Your armor’s maximum " +
                    "Dexterity bonus applies to your Charisma, instead.");
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleRevelationPropheticArmorBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                m_IsExtendable = true,
                            },
                            AsChild = true,
                            SameDuration = true
                        }
                        );
                    c.NewRound = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList();
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleLunarMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherLunarMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistLunarMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoLunarMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        RavenerHunterLunarMysteryProgression.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.CheckInProgression = false;
                    c.HideInUI = true;
                    c.m_Feature = OracleRevelationNatureWhispers.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationNatureWhispers.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.CheckInProgression = false;
                c.HideInUI = true;
                c.m_Feature = OracleRevelationPropheticArmorFeature.ToReference<BlueprintFeatureReference>();
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationPropheticArmorFeature.ToReference<BlueprintFeatureReference>());
            //TouchOfTheMoon





            //Ravener Hunter Cont.
            var RavenerHunterLunarRevelationSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("RavenerHunterLunarRevelationSelection", bp => {
                bp.SetName("Lunar Revelation");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.None;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
                bp.AddFeatures();
            });
            RavenerHunterLunarMysteryProgression.LevelEntries = new LevelEntry[] {
                 Helpers.LevelEntry(1, RavenerHunterLunarRevelationSelection),
                 Helpers.LevelEntry(8, RavenerHunterLunarRevelationSelection)
            };
            RavenerHunterLunarMysteryProgression.UIGroups = new UIGroup[] {
                Helpers.CreateUIGroup(RavenerHunterLunarRevelationSelection, RavenerHunterLunarRevelationSelection)
            };
            var RavenerHunterChargedByNatureSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("RavenerHunterChargedByNatureSelection");
            var SecondChargedByNatureSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("SecondChargedByNatureSelection");
            RavenerHunterChargedByNatureSelection.m_AllFeatures = RavenerHunterChargedByNatureSelection.m_AllFeatures.AppendToArray(RavenerHunterLunarMysteryProgression.ToReference<BlueprintFeatureReference>());
            SecondChargedByNatureSelection.m_AllFeatures = SecondChargedByNatureSelection.m_AllFeatures.AppendToArray(RavenerHunterLunarMysteryProgression.ToReference<BlueprintFeatureReference>());

            MysteryTools.RegisterMystery(OracleLunarMysteryFeature);
            MysteryTools.RegisterSecondMystery(OracleLunarMysteryFeature);
            MysteryTools.RegisterEnlightendPhilosopherMystery(EnlightnedPhilosopherLunarMysteryFeature);
            MysteryTools.RegisterSecondEnlightendPhilosopherMystery(EnlightnedPhilosopherLunarMysteryFeature);
            MysteryTools.RegisterHerbalistMystery(DivineHerbalistLunarMysteryFeature);
            MysteryTools.RegisterSecondHerbalistMystery(DivineHerbalistLunarMysteryFeature);
            MysteryTools.RegisterOceansEchoMystery(OceansEchoLunarMysteryFeature);
            MysteryTools.RegisterSecondOceansEchoMystery(OceansEchoLunarMysteryFeature);
        }
    }
}
