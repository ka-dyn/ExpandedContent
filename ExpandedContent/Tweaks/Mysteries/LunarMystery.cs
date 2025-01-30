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
using Kingmaker.UI.UnitSettings.Blueprints;
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
using static ExpandedContent.Tweaks.Miscellaneous.NewActivatableAbilityGroups.NewActivatableAbilityGroupAdder;

namespace ExpandedContent.Tweaks.Mysteries {
    internal class LunarMystery {

        public static void AddLunarMystery() {

            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var OracleRevelationSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("60008a10ad7ad6543b1f63016741a5d2");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var RavenerHunterArchetype = Resources.GetModBlueprint<BlueprintArchetype>("RavenerHunterArchetype");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");
            var PlantType = Resources.GetBlueprint<BlueprintFeature>("706e61781d692a042b35941f14bc41c5");
            var LunarMysteryIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleLunarMystery.png");

            var ImmunityToMindAffecting = Resources.GetBlueprint<BlueprintFeature>("3eb606c0564d0814ea01a824dbe42fb0");
            var MasterShapeshifter = Resources.GetBlueprint<BlueprintFeature>("934670ef88b281b4da5596db8b00df2f");
            var BestialHowlFeature = Resources.GetBlueprint<BlueprintFeature>("87ab3793abc347d5a47fc6b7fec8dfcb");
            var WerewolfBleedFeature = Resources.GetBlueprint<BlueprintFeature>("502d36f8b703420397ca35d5f523b8f0");
            var ShiftersRush = Resources.GetBlueprintReference<BlueprintBuffReference>("c3365d5a75294b9b879c587668620bd4");
            var ShifterWildShapeWereRatBuff = Resources.GetBlueprint<BlueprintBuff>("9261713e33624de599d6183d6b7cf2e4");
            var ShifterWildShapeWereTigerBuff = Resources.GetBlueprint<BlueprintBuff>("1bc5f96600c74a079c8a0c6dafeb3320");
            var ShifterWildShapeWereWolfBuff = Resources.GetBlueprint<BlueprintBuff>("34273feba56448bc968dd5482cdfffc7");
            var AnimalShapesSpell = Resources.GetBlueprint<BlueprintAbility>("cf689244b2c7e904eb85f26fd6e81552");

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
            OracleLunarFinalRevelationWolfBuff.GetComponent<Polymorph>().m_Facts = OracleLunarFinalRevelationWolfBuff.GetComponent<Polymorph>().m_Facts
                .RemoveFromArray(WerewolfBleedFeature.ToReference<BlueprintUnitFactReference>());
            var PolymorphCleanup = new Polymorph[] {
                OracleLunarFinalRevelationRatBuff.GetComponent<Polymorph>(),
                OracleLunarFinalRevelationTigerBuff.GetComponent<Polymorph>(),
                OracleLunarFinalRevelationWolfBuff.GetComponent<Polymorph>()
            };
            foreach (var polymorph in PolymorphCleanup) {
                polymorph.m_Facts = polymorph.m_Facts.RemoveFromArray(BestialHowlFeature.ToReference<BlueprintUnitFactReference>());
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
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
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
            #region FormOfTheBeast
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
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] {MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>() };
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
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>() };
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
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>() };
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
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>() };
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
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>() };
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
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(7, OracleRevelationBeastFormFeature1),
                    Helpers.LevelEntry(9, OracleRevelationBeastFormFeature2),
                    Helpers.LevelEntry(11, OracleRevelationBeastFormFeature3),
                    Helpers.LevelEntry(13, OracleRevelationBeastFormFeature4)
                };
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
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 7;
                    c.Group = Prerequisite.GroupType.Any;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationBeastFormResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.GiveFeaturesForPreviousLevels = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationBeastFormProgression.ToReference<BlueprintFeatureReference>());
            #endregion

            #region GiftOfClawAndHorn
            var BearShamanTotemTransformationIcon = AssetLoader.LoadInternal("Skills", "Icon_BearShamanTotemTransformation.jpg");
            var OracleRevelationGiftOfClawAndHornResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationGiftOfClawAndHornResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Charisma,
                };
            });
            var OracleRevelationGiftOfClawAndHornToggleIncreaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationGiftOfClawAndHornToggleIncreaseFeature", bp => {
                bp.SetName("Double Natural Weapon");
                bp.SetDescription("At 11th level, you may select two natural weapons at a time.");
                bp.AddComponent<IncreaseActivatableAbilityGroupSize>(c => {
                    c.Group = (ActivatableAbilityGroup)ECActivatableAbilityGroup.GiftOfClawAndHorn;
                });
                bp.HideInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationGiftOfClawAndHornToggleBiteBuff = Helpers.CreateBuff("OracleRevelationGiftOfClawAndHornToggleBiteBuff", bp => {
                bp.SetName("Giftof CandH - Bite buff");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath | BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
            });
            var OracleRevelationGiftOfClawAndHornToggleClawBuff = Helpers.CreateBuff("OracleRevelationGiftOfClawAndHornToggleClawBuff", bp => {
                bp.SetName("Giftof CandH - Claws buff");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath | BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
            });
            var OracleRevelationGiftOfClawAndHornToggleGoreBuff = Helpers.CreateBuff("OracleRevelationGiftOfClawAndHornToggleGoreBuff", bp => {
                bp.SetName("Giftof CandH - Gore buff");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath | BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
            });
            var OracleRevelationGiftOfClawAndHornToggleBite = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationGiftOfClawAndHornToggleBite", bp => {
                bp.SetName("Gift of Claw and Horn - Bite");
                bp.SetDescription("With this activated, the gift of claw and horn ability will grant a bite attack.");
                bp.m_Icon = AnimalShapesSpell.Icon;
                bp.m_Buff = OracleRevelationGiftOfClawAndHornToggleBiteBuff.ToReference<BlueprintBuffReference>();
                bp.m_AllowNonContextActions = false;
                bp.Group = (ActivatableAbilityGroup)ECActivatableAbilityGroup.GiftOfClawAndHorn;
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>(),
                                MinLevel = 0,
                            },
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                                MinLevel = 0,
                            },
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                                MinLevel = 0
                            }
                        }
                    };
                    c.AutoCastConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] { }
                    };
                });
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var OracleRevelationGiftOfClawAndHornToggleClaw = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationGiftOfClawAndHornToggleClaw", bp => {
                bp.SetName("Gift of Claw and Horn - Claws");
                bp.SetDescription("With this activated, the gift of claw and horn ability will grant claw attacks while unarmed.");
                bp.m_Icon = AnimalShapesSpell.Icon;
                bp.m_Buff = OracleRevelationGiftOfClawAndHornToggleClawBuff.ToReference<BlueprintBuffReference>();
                bp.m_AllowNonContextActions = false;
                bp.Group = (ActivatableAbilityGroup)ECActivatableAbilityGroup.GiftOfClawAndHorn;
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>(),
                                MinLevel = 0,
                            },
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                                MinLevel = 0,
                            },
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                                MinLevel = 0
                            }
                        }
                    };
                    c.AutoCastConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] { }
                    };
                });
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var OracleRevelationGiftOfClawAndHornToggleGore = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationGiftOfClawAndHornToggleGore", bp => {
                bp.SetName("Gift of Claw and Horn - Gore");
                bp.SetDescription("With this activated, the gift of claw and horn ability will grant a gore attack.");
                bp.m_Icon = AnimalShapesSpell.Icon;
                bp.m_Buff = OracleRevelationGiftOfClawAndHornToggleGoreBuff.ToReference<BlueprintBuffReference>();
                bp.m_AllowNonContextActions = false;
                bp.Group = (ActivatableAbilityGroup)ECActivatableAbilityGroup.GiftOfClawAndHorn;
                bp.AddComponent<ActionPanelLogic>(c => {
                    c.Priority = 0;
                    c.AutoFillConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>(),
                                MinLevel = 0,
                            },
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                                MinLevel = 0,
                            },
                            new ContextConditionCharacterClass() {
                                Not = true,
                                m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                                MinLevel = 0
                            }
                        }
                    };
                    c.AutoCastConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] { }
                    };
                });
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.DeactivateImmediately = true;
                bp.HiddenInUI = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var OracleRevelationGiftOfClawAndHornToggleBase = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationGiftOfClawAndHornToggleBase", bp => {
                bp.SetName("Gift of Claw and Horn - Weapon Selection");
                bp.SetDescription("Choose which weapon to receive when using the gift of claw and horn ability. This cannot be changed while gift of claw and horn is active. \nAt 11th level the number of weapons " +
                    "able to be selected increases by 1.");
                bp.m_Icon = AnimalShapesSpell.Icon;
                bp.m_Buff = new BlueprintBuffReference();
                bp.m_AllowNonContextActions = false;
                bp.AddComponent<ActivatableAbilityVariants>(c => {
                    c.m_Variants = new BlueprintActivatableAbilityReference[] {
                        OracleRevelationGiftOfClawAndHornToggleBite.ToReference<BlueprintActivatableAbilityReference>(),
                        OracleRevelationGiftOfClawAndHornToggleClaw.ToReference<BlueprintActivatableAbilityReference>(),
                        OracleRevelationGiftOfClawAndHornToggleGore.ToReference<BlueprintActivatableAbilityReference>()
                    };
                });
                bp.AddComponent<ActivationDisable>();
                bp.WeightInGroup = 1;
                bp.IsOnByDefault = false;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var OracleRevelationGiftOfClawAndHornTogglesFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationGiftOfClawAndHornTogglesFeature", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationGiftOfClawAndHornToggleBite.ToReference<BlueprintUnitFactReference>(),
                        OracleRevelationGiftOfClawAndHornToggleClaw.ToReference<BlueprintUnitFactReference>(),
                        OracleRevelationGiftOfClawAndHornToggleGore.ToReference<BlueprintUnitFactReference>(),
                        OracleRevelationGiftOfClawAndHornToggleBase.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
            });
            var GiftOfClawAndHornFeatureLvl1 = Helpers.CreateBlueprint<BlueprintFeature>("GiftOfClawAndHornFeatureLvl1", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
            });
            var GiftOfClawAndHornFeatureLvl2 = Helpers.CreateBlueprint<BlueprintFeature>("GiftOfClawAndHornFeatureLvl2", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
            });
            var GiftOfClawAndHornFeatureLvl3 = Helpers.CreateBlueprint<BlueprintFeature>("GiftOfClawAndHornFeatureLvl3", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
            });
            var GiftOfClawAndHornFeatureLvl4 = Helpers.CreateBlueprint<BlueprintFeature>("GiftOfClawAndHornFeatureLvl4", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
            });
            var GiftOfClawAndHornFeatureLvl5 = Helpers.CreateBlueprint<BlueprintFeature>("GiftOfClawAndHornFeatureLvl5", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
            });
            var Bite1d6 = Resources.GetBlueprint<BlueprintItemWeapon>("a000716f88c969c499a535dadcf09286");
            var Claw1d4 = Resources.GetBlueprint<BlueprintItemWeapon>("118fdd03e569a66459ab01a20af6811a");
            var Gore1d6 = Resources.GetBlueprint<BlueprintItemWeapon>("daf4ab765feba8548b244e174e7af5be");
            var Enhancement1 = Resources.GetBlueprintReference<BlueprintWeaponEnchantmentReference>("d42fc23b92c640846ac137dc26e000d4");
            var Enhancement2 = Resources.GetBlueprintReference<BlueprintWeaponEnchantmentReference>("eb2faccc4c9487d43b3575d7e77ff3f5");
            var Enhancement3 = Resources.GetBlueprintReference<BlueprintWeaponEnchantmentReference>("80bb8a737579e35498177e1e3c75899b");
            var Enhancement4 = Resources.GetBlueprintReference<BlueprintWeaponEnchantmentReference>("783d7d496da6ac44f9511011fc5f1979");
            var ClawAndHornWeaponBite0 = Helpers.CreateBlueprint<BlueprintItemWeapon>("ClawAndHornWeaponBite0", bp => {
                bp.m_DisplayNameText = Bite1d6.m_DisplayNameText;
                bp.m_DescriptionText = Bite1d6.m_DescriptionText;
                bp.m_FlavorText = Bite1d6.m_FlavorText;
                bp.m_NonIdentifiedNameText = Bite1d6.m_NonIdentifiedNameText;
                bp.m_NonIdentifiedDescriptionText = Bite1d6.m_NonIdentifiedDescriptionText;
                bp.m_Icon = Bite1d6.m_Icon;
                bp.m_Cost = 0;
                bp.m_Weight = 0;
                bp.m_IsNotable = false;
                bp.m_IsJunk = false;
                bp.m_ForceStackable = false;
                bp.m_Destructible = false;
                bp.m_ShardItem = null;
                bp.m_MiscellaneousType = Kingmaker.Blueprints.Items.BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = Bite1d6.m_InventoryPutSound;
                bp.m_InventoryTakeSound = Bite1d6.m_InventoryTakeSound;
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = Bite1d6.TrashLootTypes;
                bp.CR = 0;
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = false;
                bp.CasterLevel = 1;
                bp.SpellLevel = 1;
                bp.DC = 11;
                bp.HideAbilityInfo = false;
                bp.IsNonRemovable = false;
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.m_VisualParameters = Bite1d6.m_VisualParameters;
                bp.m_Type = Bite1d6.m_Type;
                bp.m_Size = Size.Medium;
                bp.m_Enchantments = new BlueprintWeaponEnchantmentReference[] { };
                bp.m_OverrideDamageDice = false;
                bp.m_DamageDice = new DiceFormula() {
                    m_Rolls = 0,
                    m_Dice = DiceType.Zero
                };
                bp.m_OverrideDamageType = false;
                bp.m_DamageType = Bite1d6.m_DamageType;
                bp.CountAsDouble = false;
                bp.Double = false;
                bp.m_SecondWeapon = null;
                bp.KeepInPolymorph = false;
                bp.m_OverrideShardItem = false;
                bp.m_OverrideDestructible = false;
                bp.m_AlwaysPrimary = false;
                bp.m_IsCanChangeVisualOverriden = false;
                bp.m_CanChangeVisual = false;
            });
            var ClawAndHornWeaponBite1 = Helpers.CreateBlueprint<BlueprintItemWeapon>("ClawAndHornWeaponBite1", bp => {
                bp.m_DisplayNameText = Bite1d6.m_DisplayNameText;
                bp.m_DescriptionText = Bite1d6.m_DescriptionText;
                bp.m_FlavorText = Bite1d6.m_FlavorText;
                bp.m_NonIdentifiedNameText = Bite1d6.m_NonIdentifiedNameText;
                bp.m_NonIdentifiedDescriptionText = Bite1d6.m_NonIdentifiedDescriptionText;
                bp.m_Icon = Bite1d6.m_Icon;
                bp.m_Cost = 0;
                bp.m_Weight = 0;
                bp.m_IsNotable = false;
                bp.m_IsJunk = false;
                bp.m_ForceStackable = false;
                bp.m_Destructible = false;
                bp.m_ShardItem = null;
                bp.m_MiscellaneousType = Kingmaker.Blueprints.Items.BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = Bite1d6.m_InventoryPutSound;
                bp.m_InventoryTakeSound = Bite1d6.m_InventoryTakeSound;
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = Bite1d6.TrashLootTypes;
                bp.CR = 0;
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = false;
                bp.CasterLevel = 1;
                bp.SpellLevel = 1;
                bp.DC = 11;
                bp.HideAbilityInfo = false;
                bp.IsNonRemovable = false;
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.m_VisualParameters = Bite1d6.m_VisualParameters;
                bp.m_Type = Bite1d6.m_Type;
                bp.m_Size = Size.Medium;
                bp.m_Enchantments = new BlueprintWeaponEnchantmentReference[] { Enhancement1 };
                bp.m_OverrideDamageDice = false;
                bp.m_DamageDice = new DiceFormula() {
                    m_Rolls = 0,
                    m_Dice = DiceType.Zero
                };
                bp.m_OverrideDamageType = false;
                bp.m_DamageType = Bite1d6.m_DamageType;
                bp.CountAsDouble = false;
                bp.Double = false;
                bp.m_SecondWeapon = null;
                bp.KeepInPolymorph = false;
                bp.m_OverrideShardItem = false;
                bp.m_OverrideDestructible = false;
                bp.m_AlwaysPrimary = false;
                bp.m_IsCanChangeVisualOverriden = false;
                bp.m_CanChangeVisual = false;
            });
            var ClawAndHornWeaponBite2 = Helpers.CreateBlueprint<BlueprintItemWeapon>("ClawAndHornWeaponBite2", bp => {
                bp.m_DisplayNameText = Bite1d6.m_DisplayNameText;
                bp.m_DescriptionText = Bite1d6.m_DescriptionText;
                bp.m_FlavorText = Bite1d6.m_FlavorText;
                bp.m_NonIdentifiedNameText = Bite1d6.m_NonIdentifiedNameText;
                bp.m_NonIdentifiedDescriptionText = Bite1d6.m_NonIdentifiedDescriptionText;
                bp.m_Icon = Bite1d6.m_Icon;
                bp.m_Cost = 0;
                bp.m_Weight = 0;
                bp.m_IsNotable = false;
                bp.m_IsJunk = false;
                bp.m_ForceStackable = false;
                bp.m_Destructible = false;
                bp.m_ShardItem = null;
                bp.m_MiscellaneousType = Kingmaker.Blueprints.Items.BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = Bite1d6.m_InventoryPutSound;
                bp.m_InventoryTakeSound = Bite1d6.m_InventoryTakeSound;
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = Bite1d6.TrashLootTypes;
                bp.CR = 0;
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = false;
                bp.CasterLevel = 1;
                bp.SpellLevel = 1;
                bp.DC = 11;
                bp.HideAbilityInfo = false;
                bp.IsNonRemovable = false;
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.m_VisualParameters = Bite1d6.m_VisualParameters;
                bp.m_Type = Bite1d6.m_Type;
                bp.m_Size = Size.Medium;
                bp.m_Enchantments = new BlueprintWeaponEnchantmentReference[] { Enhancement2 };
                bp.m_OverrideDamageDice = false;
                bp.m_DamageDice = new DiceFormula() {
                    m_Rolls = 0,
                    m_Dice = DiceType.Zero
                };
                bp.m_OverrideDamageType = false;
                bp.m_DamageType = Bite1d6.m_DamageType;
                bp.CountAsDouble = false;
                bp.Double = false;
                bp.m_SecondWeapon = null;
                bp.KeepInPolymorph = false;
                bp.m_OverrideShardItem = false;
                bp.m_OverrideDestructible = false;
                bp.m_AlwaysPrimary = false;
                bp.m_IsCanChangeVisualOverriden = false;
                bp.m_CanChangeVisual = false;
            });
            var ClawAndHornWeaponBite3 = Helpers.CreateBlueprint<BlueprintItemWeapon>("ClawAndHornWeaponBite3", bp => {
                bp.m_DisplayNameText = Bite1d6.m_DisplayNameText;
                bp.m_DescriptionText = Bite1d6.m_DescriptionText;
                bp.m_FlavorText = Bite1d6.m_FlavorText;
                bp.m_NonIdentifiedNameText = Bite1d6.m_NonIdentifiedNameText;
                bp.m_NonIdentifiedDescriptionText = Bite1d6.m_NonIdentifiedDescriptionText;
                bp.m_Icon = Bite1d6.m_Icon;
                bp.m_Cost = 0;
                bp.m_Weight = 0;
                bp.m_IsNotable = false;
                bp.m_IsJunk = false;
                bp.m_ForceStackable = false;
                bp.m_Destructible = false;
                bp.m_ShardItem = null;
                bp.m_MiscellaneousType = Kingmaker.Blueprints.Items.BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = Bite1d6.m_InventoryPutSound;
                bp.m_InventoryTakeSound = Bite1d6.m_InventoryTakeSound;
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = Bite1d6.TrashLootTypes;
                bp.CR = 0;
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = false;
                bp.CasterLevel = 1;
                bp.SpellLevel = 1;
                bp.DC = 11;
                bp.HideAbilityInfo = false;
                bp.IsNonRemovable = false;
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.m_VisualParameters = Bite1d6.m_VisualParameters;
                bp.m_Type = Bite1d6.m_Type;
                bp.m_Size = Size.Medium;
                bp.m_Enchantments = new BlueprintWeaponEnchantmentReference[] { Enhancement3 };
                bp.m_OverrideDamageDice = false;
                bp.m_DamageDice = new DiceFormula() {
                    m_Rolls = 0,
                    m_Dice = DiceType.Zero
                };
                bp.m_OverrideDamageType = false;
                bp.m_DamageType = Bite1d6.m_DamageType;
                bp.CountAsDouble = false;
                bp.Double = false;
                bp.m_SecondWeapon = null;
                bp.KeepInPolymorph = false;
                bp.m_OverrideShardItem = false;
                bp.m_OverrideDestructible = false;
                bp.m_AlwaysPrimary = false;
                bp.m_IsCanChangeVisualOverriden = false;
                bp.m_CanChangeVisual = false;
            });
            var ClawAndHornWeaponBite4 = Helpers.CreateBlueprint<BlueprintItemWeapon>("ClawAndHornWeaponBite4", bp => {
                bp.m_DisplayNameText = Bite1d6.m_DisplayNameText;
                bp.m_DescriptionText = Bite1d6.m_DescriptionText;
                bp.m_FlavorText = Bite1d6.m_FlavorText;
                bp.m_NonIdentifiedNameText = Bite1d6.m_NonIdentifiedNameText;
                bp.m_NonIdentifiedDescriptionText = Bite1d6.m_NonIdentifiedDescriptionText;
                bp.m_Icon = Bite1d6.m_Icon;
                bp.m_Cost = 0;
                bp.m_Weight = 0;
                bp.m_IsNotable = false;
                bp.m_IsJunk = false;
                bp.m_ForceStackable = false;
                bp.m_Destructible = false;
                bp.m_ShardItem = null;
                bp.m_MiscellaneousType = Kingmaker.Blueprints.Items.BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = Bite1d6.m_InventoryPutSound;
                bp.m_InventoryTakeSound = Bite1d6.m_InventoryTakeSound;
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = Bite1d6.TrashLootTypes;
                bp.CR = 0;
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = false;
                bp.CasterLevel = 1;
                bp.SpellLevel = 1;
                bp.DC = 11;
                bp.HideAbilityInfo = false;
                bp.IsNonRemovable = false;
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.m_VisualParameters = Bite1d6.m_VisualParameters;
                bp.m_Type = Bite1d6.m_Type;
                bp.m_Size = Size.Medium;
                bp.m_Enchantments = new BlueprintWeaponEnchantmentReference[] { Enhancement4 };
                bp.m_OverrideDamageDice = false;
                bp.m_DamageDice = new DiceFormula() {
                    m_Rolls = 0,
                    m_Dice = DiceType.Zero
                };
                bp.m_OverrideDamageType = false;
                bp.m_DamageType = Bite1d6.m_DamageType;
                bp.CountAsDouble = false;
                bp.Double = false;
                bp.m_SecondWeapon = null;
                bp.KeepInPolymorph = false;
                bp.m_OverrideShardItem = false;
                bp.m_OverrideDestructible = false;
                bp.m_AlwaysPrimary = false;
                bp.m_IsCanChangeVisualOverriden = false;
                bp.m_CanChangeVisual = false;
            });
            var ClawAndHornWeaponClaw0 = Helpers.CreateBlueprint<BlueprintItemWeapon>("ClawAndHornWeaponClaw0", bp => {
                bp.m_DisplayNameText = Claw1d4.m_DisplayNameText;
                bp.m_DescriptionText = Claw1d4.m_DescriptionText;
                bp.m_FlavorText = Claw1d4.m_FlavorText;
                bp.m_NonIdentifiedNameText = Claw1d4.m_NonIdentifiedNameText;
                bp.m_NonIdentifiedDescriptionText = Claw1d4.m_NonIdentifiedDescriptionText;
                bp.m_Icon = Claw1d4.m_Icon;
                bp.m_Cost = 0;
                bp.m_Weight = 0;
                bp.m_IsNotable = false;
                bp.m_IsJunk = false;
                bp.m_ForceStackable = false;
                bp.m_Destructible = false;
                bp.m_ShardItem = null;
                bp.m_MiscellaneousType = Kingmaker.Blueprints.Items.BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = Claw1d4.m_InventoryPutSound;
                bp.m_InventoryTakeSound = Claw1d4.m_InventoryTakeSound;
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = Claw1d4.TrashLootTypes;
                bp.CR = 0;
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = false;
                bp.CasterLevel = 1;
                bp.SpellLevel = 1;
                bp.DC = 11;
                bp.HideAbilityInfo = false;
                bp.IsNonRemovable = false;
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.m_VisualParameters = Claw1d4.m_VisualParameters;
                bp.m_Type = Claw1d4.m_Type;
                bp.m_Size = Size.Medium;
                bp.m_Enchantments = new BlueprintWeaponEnchantmentReference[] { };
                bp.m_OverrideDamageDice = false;
                bp.m_DamageDice = new DiceFormula() {
                    m_Rolls = 0,
                    m_Dice = DiceType.Zero
                };
                bp.m_OverrideDamageType = false;
                bp.m_DamageType = Claw1d4.m_DamageType;
                bp.CountAsDouble = true;
                bp.Double = false;
                bp.m_SecondWeapon = null;
                bp.KeepInPolymorph = false;
                bp.m_OverrideShardItem = false;
                bp.m_OverrideDestructible = false;
                bp.m_AlwaysPrimary = false;
                bp.m_IsCanChangeVisualOverriden = false;
                bp.m_CanChangeVisual = false;
            });
            var ClawAndHornWeaponClaw1 = Helpers.CreateBlueprint<BlueprintItemWeapon>("ClawAndHornWeaponClaw1", bp => {
                bp.m_DisplayNameText = Claw1d4.m_DisplayNameText;
                bp.m_DescriptionText = Claw1d4.m_DescriptionText;
                bp.m_FlavorText = Claw1d4.m_FlavorText;
                bp.m_NonIdentifiedNameText = Claw1d4.m_NonIdentifiedNameText;
                bp.m_NonIdentifiedDescriptionText = Claw1d4.m_NonIdentifiedDescriptionText;
                bp.m_Icon = Claw1d4.m_Icon;
                bp.m_Cost = 0;
                bp.m_Weight = 0;
                bp.m_IsNotable = false;
                bp.m_IsJunk = false;
                bp.m_ForceStackable = false;
                bp.m_Destructible = false;
                bp.m_ShardItem = null;
                bp.m_MiscellaneousType = Kingmaker.Blueprints.Items.BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = Claw1d4.m_InventoryPutSound;
                bp.m_InventoryTakeSound = Claw1d4.m_InventoryTakeSound;
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = Claw1d4.TrashLootTypes;
                bp.CR = 0;
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = false;
                bp.CasterLevel = 1;
                bp.SpellLevel = 1;
                bp.DC = 11;
                bp.HideAbilityInfo = false;
                bp.IsNonRemovable = false;
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.m_VisualParameters = Claw1d4.m_VisualParameters;
                bp.m_Type = Claw1d4.m_Type;
                bp.m_Size = Size.Medium;
                bp.m_Enchantments = new BlueprintWeaponEnchantmentReference[] { Enhancement1 };
                bp.m_OverrideDamageDice = false;
                bp.m_DamageDice = new DiceFormula() {
                    m_Rolls = 0,
                    m_Dice = DiceType.Zero
                };
                bp.m_OverrideDamageType = false;
                bp.m_DamageType = Claw1d4.m_DamageType;
                bp.CountAsDouble = true;
                bp.Double = false;
                bp.m_SecondWeapon = null;
                bp.KeepInPolymorph = false;
                bp.m_OverrideShardItem = false;
                bp.m_OverrideDestructible = false;
                bp.m_AlwaysPrimary = false;
                bp.m_IsCanChangeVisualOverriden = false;
                bp.m_CanChangeVisual = false;
            });
            var ClawAndHornWeaponClaw2 = Helpers.CreateBlueprint<BlueprintItemWeapon>("ClawAndHornWeaponClaw2", bp => {
                bp.m_DisplayNameText = Claw1d4.m_DisplayNameText;
                bp.m_DescriptionText = Claw1d4.m_DescriptionText;
                bp.m_FlavorText = Claw1d4.m_FlavorText;
                bp.m_NonIdentifiedNameText = Claw1d4.m_NonIdentifiedNameText;
                bp.m_NonIdentifiedDescriptionText = Claw1d4.m_NonIdentifiedDescriptionText;
                bp.m_Icon = Claw1d4.m_Icon;
                bp.m_Cost = 0;
                bp.m_Weight = 0;
                bp.m_IsNotable = false;
                bp.m_IsJunk = false;
                bp.m_ForceStackable = false;
                bp.m_Destructible = false;
                bp.m_ShardItem = null;
                bp.m_MiscellaneousType = Kingmaker.Blueprints.Items.BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = Claw1d4.m_InventoryPutSound;
                bp.m_InventoryTakeSound = Claw1d4.m_InventoryTakeSound;
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = Claw1d4.TrashLootTypes;
                bp.CR = 0;
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = false;
                bp.CasterLevel = 1;
                bp.SpellLevel = 1;
                bp.DC = 11;
                bp.HideAbilityInfo = false;
                bp.IsNonRemovable = false;
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.m_VisualParameters = Claw1d4.m_VisualParameters;
                bp.m_Type = Claw1d4.m_Type;
                bp.m_Size = Size.Medium;
                bp.m_Enchantments = new BlueprintWeaponEnchantmentReference[] { Enhancement2 };
                bp.m_OverrideDamageDice = false;
                bp.m_DamageDice = new DiceFormula() {
                    m_Rolls = 0,
                    m_Dice = DiceType.Zero
                };
                bp.m_OverrideDamageType = false;
                bp.m_DamageType = Claw1d4.m_DamageType;
                bp.CountAsDouble = true;
                bp.Double = false;
                bp.m_SecondWeapon = null;
                bp.KeepInPolymorph = false;
                bp.m_OverrideShardItem = false;
                bp.m_OverrideDestructible = false;
                bp.m_AlwaysPrimary = false;
                bp.m_IsCanChangeVisualOverriden = false;
                bp.m_CanChangeVisual = false;
            });
            var ClawAndHornWeaponClaw3 = Helpers.CreateBlueprint<BlueprintItemWeapon>("ClawAndHornWeaponClaw3", bp => {
                bp.m_DisplayNameText = Claw1d4.m_DisplayNameText;
                bp.m_DescriptionText = Claw1d4.m_DescriptionText;
                bp.m_FlavorText = Claw1d4.m_FlavorText;
                bp.m_NonIdentifiedNameText = Claw1d4.m_NonIdentifiedNameText;
                bp.m_NonIdentifiedDescriptionText = Claw1d4.m_NonIdentifiedDescriptionText;
                bp.m_Icon = Claw1d4.m_Icon;
                bp.m_Cost = 0;
                bp.m_Weight = 0;
                bp.m_IsNotable = false;
                bp.m_IsJunk = false;
                bp.m_ForceStackable = false;
                bp.m_Destructible = false;
                bp.m_ShardItem = null;
                bp.m_MiscellaneousType = Kingmaker.Blueprints.Items.BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = Claw1d4.m_InventoryPutSound;
                bp.m_InventoryTakeSound = Claw1d4.m_InventoryTakeSound;
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = Claw1d4.TrashLootTypes;
                bp.CR = 0;
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = false;
                bp.CasterLevel = 1;
                bp.SpellLevel = 1;
                bp.DC = 11;
                bp.HideAbilityInfo = false;
                bp.IsNonRemovable = false;
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.m_VisualParameters = Claw1d4.m_VisualParameters;
                bp.m_Type = Claw1d4.m_Type;
                bp.m_Size = Size.Medium;
                bp.m_Enchantments = new BlueprintWeaponEnchantmentReference[] { Enhancement3 };
                bp.m_OverrideDamageDice = false;
                bp.m_DamageDice = new DiceFormula() {
                    m_Rolls = 0,
                    m_Dice = DiceType.Zero
                };
                bp.m_OverrideDamageType = false;
                bp.m_DamageType = Claw1d4.m_DamageType;
                bp.CountAsDouble = true;
                bp.Double = false;
                bp.m_SecondWeapon = null;
                bp.KeepInPolymorph = false;
                bp.m_OverrideShardItem = false;
                bp.m_OverrideDestructible = false;
                bp.m_AlwaysPrimary = false;
                bp.m_IsCanChangeVisualOverriden = false;
                bp.m_CanChangeVisual = false;
            });
            var ClawAndHornWeaponClaw4 = Helpers.CreateBlueprint<BlueprintItemWeapon>("ClawAndHornWeaponClaw4", bp => {
                bp.m_DisplayNameText = Claw1d4.m_DisplayNameText;
                bp.m_DescriptionText = Claw1d4.m_DescriptionText;
                bp.m_FlavorText = Claw1d4.m_FlavorText;
                bp.m_NonIdentifiedNameText = Claw1d4.m_NonIdentifiedNameText;
                bp.m_NonIdentifiedDescriptionText = Claw1d4.m_NonIdentifiedDescriptionText;
                bp.m_Icon = Claw1d4.m_Icon;
                bp.m_Cost = 0;
                bp.m_Weight = 0;
                bp.m_IsNotable = false;
                bp.m_IsJunk = false;
                bp.m_ForceStackable = false;
                bp.m_Destructible = false;
                bp.m_ShardItem = null;
                bp.m_MiscellaneousType = Kingmaker.Blueprints.Items.BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = Claw1d4.m_InventoryPutSound;
                bp.m_InventoryTakeSound = Claw1d4.m_InventoryTakeSound;
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = Claw1d4.TrashLootTypes;
                bp.CR = 0;
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = false;
                bp.CasterLevel = 1;
                bp.SpellLevel = 1;
                bp.DC = 11;
                bp.HideAbilityInfo = false;
                bp.IsNonRemovable = false;
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.m_VisualParameters = Claw1d4.m_VisualParameters;
                bp.m_Type = Claw1d4.m_Type;
                bp.m_Size = Size.Medium;
                bp.m_Enchantments = new BlueprintWeaponEnchantmentReference[] { Enhancement4 };
                bp.m_OverrideDamageDice = false;
                bp.m_DamageDice = new DiceFormula() {
                    m_Rolls = 0,
                    m_Dice = DiceType.Zero
                };
                bp.m_OverrideDamageType = false;
                bp.m_DamageType = Claw1d4.m_DamageType;
                bp.CountAsDouble = true;
                bp.Double = false;
                bp.m_SecondWeapon = null;
                bp.KeepInPolymorph = false;
                bp.m_OverrideShardItem = false;
                bp.m_OverrideDestructible = false;
                bp.m_AlwaysPrimary = false;
                bp.m_IsCanChangeVisualOverriden = false;
                bp.m_CanChangeVisual = false;
            });
            var ClawAndHornWeaponGore0 = Helpers.CreateBlueprint<BlueprintItemWeapon>("ClawAndHornWeaponGore0", bp => {
                bp.m_DisplayNameText = Gore1d6.m_DisplayNameText;
                bp.m_DescriptionText = Gore1d6.m_DescriptionText;
                bp.m_FlavorText = Gore1d6.m_FlavorText;
                bp.m_NonIdentifiedNameText = Gore1d6.m_NonIdentifiedNameText;
                bp.m_NonIdentifiedDescriptionText = Gore1d6.m_NonIdentifiedDescriptionText;
                bp.m_Icon = Gore1d6.m_Icon;
                bp.m_Cost = 0;
                bp.m_Weight = 0;
                bp.m_IsNotable = false;
                bp.m_IsJunk = false;
                bp.m_ForceStackable = false;
                bp.m_Destructible = false;
                bp.m_ShardItem = null;
                bp.m_MiscellaneousType = Kingmaker.Blueprints.Items.BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = Gore1d6.m_InventoryPutSound;
                bp.m_InventoryTakeSound = Gore1d6.m_InventoryTakeSound;
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = Gore1d6.TrashLootTypes;
                bp.CR = 0;
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = false;
                bp.CasterLevel = 1;
                bp.SpellLevel = 1;
                bp.DC = 11;
                bp.HideAbilityInfo = false;
                bp.IsNonRemovable = false;
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.m_VisualParameters = Gore1d6.m_VisualParameters;
                bp.m_Type = Gore1d6.m_Type;
                bp.m_Size = Size.Medium;
                bp.m_Enchantments = new BlueprintWeaponEnchantmentReference[] { };
                bp.m_OverrideDamageDice = false;
                bp.m_DamageDice = new DiceFormula() {
                    m_Rolls = 0,
                    m_Dice = DiceType.Zero
                };
                bp.m_OverrideDamageType = false;
                bp.m_DamageType = Gore1d6.m_DamageType;
                bp.CountAsDouble = false;
                bp.Double = false;
                bp.m_SecondWeapon = null;
                bp.KeepInPolymorph = false;
                bp.m_OverrideShardItem = false;
                bp.m_OverrideDestructible = false;
                bp.m_AlwaysPrimary = false;
                bp.m_IsCanChangeVisualOverriden = false;
                bp.m_CanChangeVisual = false;
            });
            var ClawAndHornWeaponGore1 = Helpers.CreateBlueprint<BlueprintItemWeapon>("ClawAndHornWeaponGore1", bp => {
                bp.m_DisplayNameText = Gore1d6.m_DisplayNameText;
                bp.m_DescriptionText = Gore1d6.m_DescriptionText;
                bp.m_FlavorText = Gore1d6.m_FlavorText;
                bp.m_NonIdentifiedNameText = Gore1d6.m_NonIdentifiedNameText;
                bp.m_NonIdentifiedDescriptionText = Gore1d6.m_NonIdentifiedDescriptionText;
                bp.m_Icon = Gore1d6.m_Icon;
                bp.m_Cost = 0;
                bp.m_Weight = 0;
                bp.m_IsNotable = false;
                bp.m_IsJunk = false;
                bp.m_ForceStackable = false;
                bp.m_Destructible = false;
                bp.m_ShardItem = null;
                bp.m_MiscellaneousType = Kingmaker.Blueprints.Items.BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = Gore1d6.m_InventoryPutSound;
                bp.m_InventoryTakeSound = Gore1d6.m_InventoryTakeSound;
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = Gore1d6.TrashLootTypes;
                bp.CR = 0;
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = false;
                bp.CasterLevel = 1;
                bp.SpellLevel = 1;
                bp.DC = 11;
                bp.HideAbilityInfo = false;
                bp.IsNonRemovable = false;
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.m_VisualParameters = Gore1d6.m_VisualParameters;
                bp.m_Type = Gore1d6.m_Type;
                bp.m_Size = Size.Medium;
                bp.m_Enchantments = new BlueprintWeaponEnchantmentReference[] { Enhancement1 };
                bp.m_OverrideDamageDice = false;
                bp.m_DamageDice = new DiceFormula() {
                    m_Rolls = 0,
                    m_Dice = DiceType.Zero
                };
                bp.m_OverrideDamageType = false;
                bp.m_DamageType = Gore1d6.m_DamageType;
                bp.CountAsDouble = false;
                bp.Double = false;
                bp.m_SecondWeapon = null;
                bp.KeepInPolymorph = false;
                bp.m_OverrideShardItem = false;
                bp.m_OverrideDestructible = false;
                bp.m_AlwaysPrimary = false;
                bp.m_IsCanChangeVisualOverriden = false;
                bp.m_CanChangeVisual = false;
            });
            var ClawAndHornWeaponGore2 = Helpers.CreateBlueprint<BlueprintItemWeapon>("ClawAndHornWeaponGore2", bp => {
                bp.m_DisplayNameText = Gore1d6.m_DisplayNameText;
                bp.m_DescriptionText = Gore1d6.m_DescriptionText;
                bp.m_FlavorText = Gore1d6.m_FlavorText;
                bp.m_NonIdentifiedNameText = Gore1d6.m_NonIdentifiedNameText;
                bp.m_NonIdentifiedDescriptionText = Gore1d6.m_NonIdentifiedDescriptionText;
                bp.m_Icon = Gore1d6.m_Icon;
                bp.m_Cost = 0;
                bp.m_Weight = 0;
                bp.m_IsNotable = false;
                bp.m_IsJunk = false;
                bp.m_ForceStackable = false;
                bp.m_Destructible = false;
                bp.m_ShardItem = null;
                bp.m_MiscellaneousType = Kingmaker.Blueprints.Items.BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = Gore1d6.m_InventoryPutSound;
                bp.m_InventoryTakeSound = Gore1d6.m_InventoryTakeSound;
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = Gore1d6.TrashLootTypes;
                bp.CR = 0;
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = false;
                bp.CasterLevel = 1;
                bp.SpellLevel = 1;
                bp.DC = 11;
                bp.HideAbilityInfo = false;
                bp.IsNonRemovable = false;
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.m_VisualParameters = Gore1d6.m_VisualParameters;
                bp.m_Type = Gore1d6.m_Type;
                bp.m_Size = Size.Medium;
                bp.m_Enchantments = new BlueprintWeaponEnchantmentReference[] { Enhancement2 };
                bp.m_OverrideDamageDice = false;
                bp.m_DamageDice = new DiceFormula() {
                    m_Rolls = 0,
                    m_Dice = DiceType.Zero
                };
                bp.m_OverrideDamageType = false;
                bp.m_DamageType = Gore1d6.m_DamageType;
                bp.CountAsDouble = false;
                bp.Double = false;
                bp.m_SecondWeapon = null;
                bp.KeepInPolymorph = false;
                bp.m_OverrideShardItem = false;
                bp.m_OverrideDestructible = false;
                bp.m_AlwaysPrimary = false;
                bp.m_IsCanChangeVisualOverriden = false;
                bp.m_CanChangeVisual = false;
            });
            var ClawAndHornWeaponGore3 = Helpers.CreateBlueprint<BlueprintItemWeapon>("ClawAndHornWeaponGore3", bp => {
                bp.m_DisplayNameText = Gore1d6.m_DisplayNameText;
                bp.m_DescriptionText = Gore1d6.m_DescriptionText;
                bp.m_FlavorText = Gore1d6.m_FlavorText;
                bp.m_NonIdentifiedNameText = Gore1d6.m_NonIdentifiedNameText;
                bp.m_NonIdentifiedDescriptionText = Gore1d6.m_NonIdentifiedDescriptionText;
                bp.m_Icon = Gore1d6.m_Icon;
                bp.m_Cost = 0;
                bp.m_Weight = 0;
                bp.m_IsNotable = false;
                bp.m_IsJunk = false;
                bp.m_ForceStackable = false;
                bp.m_Destructible = false;
                bp.m_ShardItem = null;
                bp.m_MiscellaneousType = Kingmaker.Blueprints.Items.BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = Gore1d6.m_InventoryPutSound;
                bp.m_InventoryTakeSound = Gore1d6.m_InventoryTakeSound;
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = Gore1d6.TrashLootTypes;
                bp.CR = 0;
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = false;
                bp.CasterLevel = 1;
                bp.SpellLevel = 1;
                bp.DC = 11;
                bp.HideAbilityInfo = false;
                bp.IsNonRemovable = false;
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.m_VisualParameters = Gore1d6.m_VisualParameters;
                bp.m_Type = Gore1d6.m_Type;
                bp.m_Size = Size.Medium;
                bp.m_Enchantments = new BlueprintWeaponEnchantmentReference[] { Enhancement3 };
                bp.m_OverrideDamageDice = false;
                bp.m_DamageDice = new DiceFormula() {
                    m_Rolls = 0,
                    m_Dice = DiceType.Zero
                };
                bp.m_OverrideDamageType = false;
                bp.m_DamageType = Gore1d6.m_DamageType;
                bp.CountAsDouble = false;
                bp.Double = false;
                bp.m_SecondWeapon = null;
                bp.KeepInPolymorph = false;
                bp.m_OverrideShardItem = false;
                bp.m_OverrideDestructible = false;
                bp.m_AlwaysPrimary = false;
                bp.m_IsCanChangeVisualOverriden = false;
                bp.m_CanChangeVisual = false;
            });
            var ClawAndHornWeaponGore4 = Helpers.CreateBlueprint<BlueprintItemWeapon>("ClawAndHornWeaponGore4", bp => {
                bp.m_DisplayNameText = Gore1d6.m_DisplayNameText;
                bp.m_DescriptionText = Gore1d6.m_DescriptionText;
                bp.m_FlavorText = Gore1d6.m_FlavorText;
                bp.m_NonIdentifiedNameText = Gore1d6.m_NonIdentifiedNameText;
                bp.m_NonIdentifiedDescriptionText = Gore1d6.m_NonIdentifiedDescriptionText;
                bp.m_Icon = Gore1d6.m_Icon;
                bp.m_Cost = 0;
                bp.m_Weight = 0;
                bp.m_IsNotable = false;
                bp.m_IsJunk = false;
                bp.m_ForceStackable = false;
                bp.m_Destructible = false;
                bp.m_ShardItem = null;
                bp.m_MiscellaneousType = Kingmaker.Blueprints.Items.BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = Gore1d6.m_InventoryPutSound;
                bp.m_InventoryTakeSound = Gore1d6.m_InventoryTakeSound;
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = Gore1d6.TrashLootTypes;
                bp.CR = 0;
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = false;
                bp.CasterLevel = 1;
                bp.SpellLevel = 1;
                bp.DC = 11;
                bp.HideAbilityInfo = false;
                bp.IsNonRemovable = false;
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.m_VisualParameters = Gore1d6.m_VisualParameters;
                bp.m_Type = Gore1d6.m_Type;
                bp.m_Size = Size.Medium;
                bp.m_Enchantments = new BlueprintWeaponEnchantmentReference[] { Enhancement4 };
                bp.m_OverrideDamageDice = false;
                bp.m_DamageDice = new DiceFormula() {
                    m_Rolls = 0,
                    m_Dice = DiceType.Zero
                };
                bp.m_OverrideDamageType = false;
                bp.m_DamageType = Gore1d6.m_DamageType;
                bp.CountAsDouble = false;
                bp.Double = false;
                bp.m_SecondWeapon = null;
                bp.KeepInPolymorph = false;
                bp.m_OverrideShardItem = false;
                bp.m_OverrideDestructible = false;
                bp.m_AlwaysPrimary = false;
                bp.m_IsCanChangeVisualOverriden = false;
                bp.m_CanChangeVisual = false;
            });
            var GiftOfClawAndHornBitePlus0 = Helpers.CreateBuff("GiftOfClawAndHornBitePlus0", bp => {
                bp.SetName("Gift of Claw and Horn - Bite");
                bp.SetDescription("As a swift action, you gain a natural weapon. The natural weapon lasts for a number of rounds equal to half your oracle level (minimum 1). You must choose a bite, claws, " +
                    "or gore attack. These attacks deal the normal damage for a creature of your size. At 5th level, your natural weapon gains a +1 enhancement bonus. This bonus increases by +1 at 10th, " +
                    "15th, and 20th level. At 11th level, you gain two natural weapons at a time. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = AnimalShapesSpell.Icon;
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = ClawAndHornWeaponBite0.ToReference<BlueprintItemWeaponReference>();
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var GiftOfClawAndHornBitePlus1 = Helpers.CreateBuff("GiftOfClawAndHornBitePlus1", bp => {
                bp.SetName("Gift of Claw and Horn - Bite");
                bp.SetDescription("As a swift action, you gain a natural weapon. The natural weapon lasts for a number of rounds equal to half your oracle level (minimum 1). You must choose a bite, claws, " +
                    "or gore attack. These attacks deal the normal damage for a creature of your size. At 5th level, your natural weapon gains a +1 enhancement bonus. This bonus increases by +1 at 10th, " +
                    "15th, and 20th level. At 11th level, you gain two natural weapons at a time. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = AnimalShapesSpell.Icon;
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = ClawAndHornWeaponBite1.ToReference<BlueprintItemWeaponReference>();
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var GiftOfClawAndHornBitePlus2 = Helpers.CreateBuff("GiftOfClawAndHornBitePlus2", bp => {
                bp.SetName("Gift of Claw and Horn - Bite");
                bp.SetDescription("As a swift action, you gain a natural weapon. The natural weapon lasts for a number of rounds equal to half your oracle level (minimum 1). You must choose a bite, claws, " +
                    "or gore attack. These attacks deal the normal damage for a creature of your size. At 5th level, your natural weapon gains a +1 enhancement bonus. This bonus increases by +1 at 10th, " +
                    "15th, and 20th level. At 11th level, you gain two natural weapons at a time. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = AnimalShapesSpell.Icon;
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = ClawAndHornWeaponBite2.ToReference<BlueprintItemWeaponReference>();
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var GiftOfClawAndHornBitePlus3 = Helpers.CreateBuff("GiftOfClawAndHornBitePlus3", bp => {
                bp.SetName("Gift of Claw and Horn - Bite");
                bp.SetDescription("As a swift action, you gain a natural weapon. The natural weapon lasts for a number of rounds equal to half your oracle level (minimum 1). You must choose a bite, claws, " +
                    "or gore attack. These attacks deal the normal damage for a creature of your size. At 5th level, your natural weapon gains a +1 enhancement bonus. This bonus increases by +1 at 10th, " +
                    "15th, and 20th level. At 11th level, you gain two natural weapons at a time. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = AnimalShapesSpell.Icon;
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = ClawAndHornWeaponBite3.ToReference<BlueprintItemWeaponReference>();
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var GiftOfClawAndHornBitePlus4 = Helpers.CreateBuff("GiftOfClawAndHornBitePlus4", bp => {
                bp.SetName("Gift of Claw and Horn - Bite");
                bp.SetDescription("As a swift action, you gain a natural weapon. The natural weapon lasts for a number of rounds equal to half your oracle level (minimum 1). You must choose a bite, claws, " +
                    "or gore attack. These attacks deal the normal damage for a creature of your size. At 5th level, your natural weapon gains a +1 enhancement bonus. This bonus increases by +1 at 10th, " +
                    "15th, and 20th level. At 11th level, you gain two natural weapons at a time. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = AnimalShapesSpell.Icon;
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = ClawAndHornWeaponBite4.ToReference<BlueprintItemWeaponReference>();
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var GiftOfClawAndHornClawPlus0 = Helpers.CreateBuff("GiftOfClawAndHornClawPlus0", bp => {
                bp.SetName("Gift of Claw and Horn - Claws");
                bp.SetDescription("As a swift action, you gain a natural weapon. The natural weapon lasts for a number of rounds equal to half your oracle level (minimum 1). You must choose a bite, claws, " +
                    "or gore attack. These attacks deal the normal damage for a creature of your size. At 5th level, your natural weapon gains a +1 enhancement bonus. This bonus increases by +1 at 10th, " +
                    "15th, and 20th level. At 11th level, you gain two natural weapons at a time. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = AnimalShapesSpell.Icon;
                bp.AddComponent<EmptyHandWeaponOverride>(c => {
                    c.m_Weapon = ClawAndHornWeaponClaw0.ToReference<BlueprintItemWeaponReference>();
                    c.IsPermanent = false;
                    c.IsMonkUnarmedStrike = false;
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var GiftOfClawAndHornClawPlus1 = Helpers.CreateBuff("GiftOfClawAndHornClawPlus1", bp => {
                bp.SetName("Gift of Claw and Horn - Claws");
                bp.SetDescription("As a swift action, you gain a natural weapon. The natural weapon lasts for a number of rounds equal to half your oracle level (minimum 1). You must choose a bite, claws, " +
                    "or gore attack. These attacks deal the normal damage for a creature of your size. At 5th level, your natural weapon gains a +1 enhancement bonus. This bonus increases by +1 at 10th, " +
                    "15th, and 20th level. At 11th level, you gain two natural weapons at a time. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = AnimalShapesSpell.Icon;
                bp.AddComponent<EmptyHandWeaponOverride>(c => {
                    c.m_Weapon = ClawAndHornWeaponClaw1.ToReference<BlueprintItemWeaponReference>();
                    c.IsPermanent = false;
                    c.IsMonkUnarmedStrike = false;
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var GiftOfClawAndHornClawPlus2 = Helpers.CreateBuff("GiftOfClawAndHornClawPlus2", bp => {
                bp.SetName("Gift of Claw and Horn - Claws");
                bp.SetDescription("As a swift action, you gain a natural weapon. The natural weapon lasts for a number of rounds equal to half your oracle level (minimum 1). You must choose a bite, claws, " +
                    "or gore attack. These attacks deal the normal damage for a creature of your size. At 5th level, your natural weapon gains a +1 enhancement bonus. This bonus increases by +1 at 10th, " +
                    "15th, and 20th level. At 11th level, you gain two natural weapons at a time. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = AnimalShapesSpell.Icon;
                bp.AddComponent<EmptyHandWeaponOverride>(c => {
                    c.m_Weapon = ClawAndHornWeaponClaw2.ToReference<BlueprintItemWeaponReference>();
                    c.IsPermanent = false;
                    c.IsMonkUnarmedStrike = false;
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var GiftOfClawAndHornClawPlus3 = Helpers.CreateBuff("GiftOfClawAndHornClawPlus3", bp => {
                bp.SetName("Gift of Claw and Horn - Claws");
                bp.SetDescription("As a swift action, you gain a natural weapon. The natural weapon lasts for a number of rounds equal to half your oracle level (minimum 1). You must choose a bite, claws, " +
                    "or gore attack. These attacks deal the normal damage for a creature of your size. At 5th level, your natural weapon gains a +1 enhancement bonus. This bonus increases by +1 at 10th, " +
                    "15th, and 20th level. At 11th level, you gain two natural weapons at a time. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = AnimalShapesSpell.Icon;
                bp.AddComponent<EmptyHandWeaponOverride>(c => {
                    c.m_Weapon = ClawAndHornWeaponClaw3.ToReference<BlueprintItemWeaponReference>();
                    c.IsPermanent = false;
                    c.IsMonkUnarmedStrike = false;
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var GiftOfClawAndHornClawPlus4 = Helpers.CreateBuff("GiftOfClawAndHornClawPlus4", bp => {
                bp.SetName("Gift of Claw and Horn - Claws");
                bp.SetDescription("As a swift action, you gain a natural weapon. The natural weapon lasts for a number of rounds equal to half your oracle level (minimum 1). You must choose a bite, claws, " +
                    "or gore attack. These attacks deal the normal damage for a creature of your size. At 5th level, your natural weapon gains a +1 enhancement bonus. This bonus increases by +1 at 10th, " +
                    "15th, and 20th level. At 11th level, you gain two natural weapons at a time. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = AnimalShapesSpell.Icon;
                bp.AddComponent<EmptyHandWeaponOverride>(c => {
                    c.m_Weapon = ClawAndHornWeaponClaw4.ToReference<BlueprintItemWeaponReference>();
                    c.IsPermanent = false;
                    c.IsMonkUnarmedStrike = false;
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var GiftOfClawAndHornGorePlus0 = Helpers.CreateBuff("GiftOfClawAndHornGorePlus0", bp => {
                bp.SetName("Gift of Claw and Horn - Gore");
                bp.SetDescription("As a swift action, you gain a natural weapon. The natural weapon lasts for a number of rounds equal to half your oracle level (minimum 1). You must choose a bite, claws, " +
                    "or gore attack. These attacks deal the normal damage for a creature of your size. At 5th level, your natural weapon gains a +1 enhancement bonus. This bonus increases by +1 at 10th, " +
                    "15th, and 20th level. At 11th level, you gain two natural weapons at a time. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = AnimalShapesSpell.Icon;
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = ClawAndHornWeaponGore0.ToReference<BlueprintItemWeaponReference>();
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var GiftOfClawAndHornGorePlus1 = Helpers.CreateBuff("GiftOfClawAndHornGorePlus1", bp => {
                bp.SetName("Gift of Claw and Horn - Gore");
                bp.SetDescription("As a swift action, you gain a natural weapon. The natural weapon lasts for a number of rounds equal to half your oracle level (minimum 1). You must choose a bite, claws, " +
                    "or gore attack. These attacks deal the normal damage for a creature of your size. At 5th level, your natural weapon gains a +1 enhancement bonus. This bonus increases by +1 at 10th, " +
                    "15th, and 20th level. At 11th level, you gain two natural weapons at a time. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = AnimalShapesSpell.Icon;
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = ClawAndHornWeaponGore1.ToReference<BlueprintItemWeaponReference>();
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var GiftOfClawAndHornGorePlus2 = Helpers.CreateBuff("GiftOfClawAndHornGorePlus2", bp => {
                bp.SetName("Gift of Claw and Horn - Gore");
                bp.SetDescription("As a swift action, you gain a natural weapon. The natural weapon lasts for a number of rounds equal to half your oracle level (minimum 1). You must choose a bite, claws, " +
                    "or gore attack. These attacks deal the normal damage for a creature of your size. At 5th level, your natural weapon gains a +1 enhancement bonus. This bonus increases by +1 at 10th, " +
                    "15th, and 20th level. At 11th level, you gain two natural weapons at a time. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = AnimalShapesSpell.Icon;
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = ClawAndHornWeaponGore2.ToReference<BlueprintItemWeaponReference>();
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var GiftOfClawAndHornGorePlus3 = Helpers.CreateBuff("GiftOfClawAndHornGorePlus3", bp => {
                bp.SetName("Gift of Claw and Horn - Gore");
                bp.SetDescription("As a swift action, you gain a natural weapon. The natural weapon lasts for a number of rounds equal to half your oracle level (minimum 1). You must choose a bite, claws, " +
                    "or gore attack. These attacks deal the normal damage for a creature of your size. At 5th level, your natural weapon gains a +1 enhancement bonus. This bonus increases by +1 at 10th, " +
                    "15th, and 20th level. At 11th level, you gain two natural weapons at a time. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = AnimalShapesSpell.Icon;
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = ClawAndHornWeaponGore3.ToReference<BlueprintItemWeaponReference>();
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var GiftOfClawAndHornGorePlus4 = Helpers.CreateBuff("GiftOfClawAndHornGorePlus4", bp => {
                bp.SetName("Gift of Claw and Horn - Gore");
                bp.SetDescription("As a swift action, you gain a natural weapon. The natural weapon lasts for a number of rounds equal to half your oracle level (minimum 1). You must choose a bite, claws, " +
                    "or gore attack. These attacks deal the normal damage for a creature of your size. At 5th level, your natural weapon gains a +1 enhancement bonus. This bonus increases by +1 at 10th, " +
                    "15th, and 20th level. At 11th level, you gain two natural weapons at a time. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = AnimalShapesSpell.Icon;
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = ClawAndHornWeaponGore4.ToReference<BlueprintItemWeaponReference>();
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var OracleRevelationGiftOfClawAndHornAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationGiftOfClawAndHornAbility", bp => {
                bp.SetName("Gift of Claw and Horn");
                bp.SetDescription("As a swift action, you gain a natural weapon. The natural weapon lasts for a number of rounds equal to half your oracle level (minimum 1). You must choose a bite, claws, " +
                    "or gore attack. These attacks deal the normal damage for a creature of your size. At 5th level, your natural weapon gains a +1 enhancement bonus. This bonus increases by +1 at 10th, " +
                    "15th, and 20th level. At 11th level, you gain two natural weapons at a time. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = BearShamanTotemTransformationIcon;
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationGiftOfClawAndHornResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = GiftOfClawAndHornBitePlus0.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = GiftOfClawAndHornBitePlus1.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = GiftOfClawAndHornBitePlus2.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = GiftOfClawAndHornBitePlus3.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = GiftOfClawAndHornBitePlus4.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = GiftOfClawAndHornClawPlus0.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = GiftOfClawAndHornClawPlus1.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = GiftOfClawAndHornClawPlus2.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = GiftOfClawAndHornClawPlus3.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = GiftOfClawAndHornClawPlus4.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = GiftOfClawAndHornGorePlus0.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = GiftOfClawAndHornGorePlus1.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = GiftOfClawAndHornGorePlus2.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = GiftOfClawAndHornGorePlus3.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = GiftOfClawAndHornGorePlus4.ToReference<BlueprintBuffReference>()
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionCasterHasFact() {
                                        m_Fact = OracleRevelationGiftOfClawAndHornToggleBiteBuff.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = GiftOfClawAndHornFeatureLvl1.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = GiftOfClawAndHornBitePlus0.ToReference<BlueprintBuffReference>(),
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                m_IsExtendable = false,
                                            }
                                        }
                                        ),
                                    IfFalse = Helpers.CreateActionList()
                                },
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = GiftOfClawAndHornFeatureLvl2.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = GiftOfClawAndHornBitePlus1.ToReference<BlueprintBuffReference>(),
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                m_IsExtendable = false,
                                            }
                                        }
                                        ),
                                    IfFalse = Helpers.CreateActionList()
                                },
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = GiftOfClawAndHornFeatureLvl3.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = GiftOfClawAndHornBitePlus2.ToReference<BlueprintBuffReference>(),
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                m_IsExtendable = false,
                                            }
                                        }
                                        ),
                                    IfFalse = Helpers.CreateActionList()
                                },
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = GiftOfClawAndHornFeatureLvl4.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = GiftOfClawAndHornBitePlus3.ToReference<BlueprintBuffReference>(),
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                m_IsExtendable = false,
                                            }
                                        }
                                        ),
                                    IfFalse = Helpers.CreateActionList()
                                },
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = GiftOfClawAndHornFeatureLvl5.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = GiftOfClawAndHornBitePlus4.ToReference<BlueprintBuffReference>(),
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                m_IsExtendable = false,
                                            }
                                        }
                                        ),
                                    IfFalse = Helpers.CreateActionList()
                                }
                                ),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionCasterHasFact() {
                                        m_Fact = OracleRevelationGiftOfClawAndHornToggleClawBuff.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = GiftOfClawAndHornFeatureLvl1.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = GiftOfClawAndHornClawPlus0.ToReference<BlueprintBuffReference>(),
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                m_IsExtendable = false,
                                            }
                                        }
                                        ),
                                    IfFalse = Helpers.CreateActionList()
                                },
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = GiftOfClawAndHornFeatureLvl2.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = GiftOfClawAndHornClawPlus1.ToReference<BlueprintBuffReference>(),
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                m_IsExtendable = false,
                                            }
                                        }
                                        ),
                                    IfFalse = Helpers.CreateActionList()
                                },
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = GiftOfClawAndHornFeatureLvl3.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = GiftOfClawAndHornClawPlus2.ToReference<BlueprintBuffReference>(),
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                m_IsExtendable = false,
                                            }
                                        }
                                        ),
                                    IfFalse = Helpers.CreateActionList()
                                },
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = GiftOfClawAndHornFeatureLvl4.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = GiftOfClawAndHornClawPlus3.ToReference<BlueprintBuffReference>(),
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                m_IsExtendable = false,
                                            }
                                        }
                                        ),
                                    IfFalse = Helpers.CreateActionList()
                                },
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = GiftOfClawAndHornFeatureLvl5.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = GiftOfClawAndHornClawPlus4.ToReference<BlueprintBuffReference>(),
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                m_IsExtendable = false,
                                            }
                                        }
                                        ),
                                    IfFalse = Helpers.CreateActionList()
                                }
                                ),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionCasterHasFact() {
                                        m_Fact = OracleRevelationGiftOfClawAndHornToggleGoreBuff.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = GiftOfClawAndHornFeatureLvl1.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = GiftOfClawAndHornGorePlus0.ToReference<BlueprintBuffReference>(),
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                m_IsExtendable = false,
                                            }
                                        }
                                        ),
                                    IfFalse = Helpers.CreateActionList()
                                },
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = GiftOfClawAndHornFeatureLvl2.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = GiftOfClawAndHornGorePlus1.ToReference<BlueprintBuffReference>(),
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                m_IsExtendable = false,
                                            }
                                        }
                                        ),
                                    IfFalse = Helpers.CreateActionList()
                                },
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = GiftOfClawAndHornFeatureLvl3.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = GiftOfClawAndHornGorePlus2.ToReference<BlueprintBuffReference>(),
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                m_IsExtendable = false,
                                            }
                                        }
                                        ),
                                    IfFalse = Helpers.CreateActionList()
                                },
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = GiftOfClawAndHornFeatureLvl4.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = GiftOfClawAndHornGorePlus3.ToReference<BlueprintBuffReference>(),
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                m_IsExtendable = false,
                                            }
                                        }
                                        ),
                                    IfFalse = Helpers.CreateActionList()
                                },
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = GiftOfClawAndHornFeatureLvl5.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = GiftOfClawAndHornGorePlus4.ToReference<BlueprintBuffReference>(),
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    Value = 0,
                                                    ValueRank = AbilityRankType.Default,
                                                    ValueShared = AbilitySharedValue.Damage,
                                                    Property = UnitProperty.None
                                                },
                                                m_IsExtendable = false,
                                            }
                                        }
                                        ),
                                    IfFalse = Helpers.CreateActionList()
                                }
                                ),
                            IfFalse = Helpers.CreateActionList()
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] {MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_UseMin = true;
                    c.m_Min = 1;
                });
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
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();            
            });
            var OracleRevelationGiftOfClawAndHornFeature1 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationGiftOfClawAndHornFeature1", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationGiftOfClawAndHornAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.Level = 5;
                    c.BeforeThisLevel = true;
                    c.m_Feature = GiftOfClawAndHornFeatureLvl1.ToReference<BlueprintFeatureReference>();
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
            });
            var OracleRevelationGiftOfClawAndHornFeature2 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationGiftOfClawAndHornFeature2", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.Level = 10;
                    c.BeforeThisLevel = true;
                    c.m_Feature = GiftOfClawAndHornFeatureLvl2.ToReference<BlueprintFeatureReference>();
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
            });
            var OracleRevelationGiftOfClawAndHornFeature3 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationGiftOfClawAndHornFeature3", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.Level = 15;
                    c.BeforeThisLevel = true;
                    c.m_Feature = GiftOfClawAndHornFeatureLvl3.ToReference<BlueprintFeatureReference>();
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
            });
            var OracleRevelationGiftOfClawAndHornFeature4 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationGiftOfClawAndHornFeature4", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.Level = 20;
                    c.BeforeThisLevel = true;
                    c.m_Feature = GiftOfClawAndHornFeatureLvl4.ToReference<BlueprintFeatureReference>();
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
            });
            var OracleRevelationGiftOfClawAndHornFeature5 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationGiftOfClawAndHornFeature5", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        GiftOfClawAndHornFeatureLvl5.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
            });
            var OracleRevelationGiftOfClawAndHornProgression = Helpers.CreateBlueprint<BlueprintProgression>("OracleRevelationGiftOfClawAndHornProgression", bp => {
                bp.SetName("Gift of Claw and Horn");
                bp.SetDescription("As a swift action, you gain a natural weapon. The natural weapon lasts for a number of rounds equal to half your oracle level (minimum 1). You must choose a bite, claws, " +
                    "or gore attack. These attacks deal the normal damage for a creature of your size. At 5th level, your natural weapon gains a +1 enhancement bonus. This bonus increases by +1 at 10th, " +
                    "15th, and 20th level. At 11th level, you gain two natural weapons at a time. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, OracleRevelationGiftOfClawAndHornFeature1, OracleRevelationGiftOfClawAndHornTogglesFeature),
                    Helpers.LevelEntry(5, OracleRevelationGiftOfClawAndHornFeature2),
                    Helpers.LevelEntry(10, OracleRevelationGiftOfClawAndHornFeature3),
                    Helpers.LevelEntry(11, OracleRevelationGiftOfClawAndHornToggleIncreaseFeature),
                    Helpers.LevelEntry(15, OracleRevelationGiftOfClawAndHornFeature4),
                    Helpers.LevelEntry(20, OracleRevelationGiftOfClawAndHornFeature5)
                };
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
                    c.m_Resource = OracleRevelationGiftOfClawAndHornResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.GiveFeaturesForPreviousLevels = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationGiftOfClawAndHornProgression.ToReference<BlueprintFeatureReference>());
            #endregion

            #region Moonbeam
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
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] {MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>() };
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
            #endregion
            #region PrimalCompanion
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
            #endregion
            //PropheticArmor
            var OracleRevelationNatureWhispers = Resources.GetBlueprint<BlueprintFeature>("3d2cd23869f0d98458169b88738f3c32");
            var OracleRevelationPropheticArmorBuff = Helpers.CreateBuff("OracleRevelationPropheticArmorBuff", bp => {
                bp.SetName("Prophetic Armor");
                bp.AddComponent<ReplaceStatBaseAttribute>(c => {
                    c.TargetStat = StatType.AC;
                    c.BaseAttributeReplacement = StatType.Charisma;
                    c.ReplaceIfHigher = true;
                });
                //Turns out ReplaceStatBaseAttribute does not work on saves
                //bp.AddComponent<ReplaceStatBaseAttribute>(c => {
                //    c.TargetStat = StatType.SaveReflex;
                //    c.BaseAttributeReplacement = StatType.Charisma;
                //    c.ReplaceIfHigher = true;
                //});
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Dexterity;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMin = false;
                    c.m_Min = 0;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SaveReflex;
                    c.Multiplier = -1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.DamageDice,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDiceAlternative;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Charisma;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMin = true;
                    c.m_Min = 0;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SaveReflex;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        Value = 0,
                        ValueRank = AbilityRankType.DamageDiceAlternative,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.AddComponent<RecalculateOnStatChange>(c => {
                    c.UseKineticistMainStat = false;
                    c.Stat = StatType.Dexterity;
                });
                bp.AddComponent<RecalculateOnStatChange>(c => {
                    c.UseKineticistMainStat = false;
                    c.Stat = StatType.Charisma;
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
            var ScaledFistACBonusBuff = Resources.GetBlueprint<BlueprintBuff>("64acb179cc6a4f19bb3513d094b28d02").GetComponent<SuppressBuffs>();
            ScaledFistACBonusBuff.m_Buffs = ScaledFistACBonusBuff.m_Buffs.AppendToArray(OracleRevelationPropheticArmorBuff.ToReference<BlueprintBuffReference>());
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationPropheticArmorFeature.ToReference<BlueprintFeatureReference>());
            //TouchOfTheMoon
            var OracleCureSpells = Resources.GetBlueprintReference<BlueprintFeatureReference>("0f7fb23d8f97b024388a433c5a8d493f");
            var OracleInflictSpells = Resources.GetBlueprintReference<BlueprintFeatureReference>("60b6566ca96b11549bd86a90d79b92f3");
            var TouchOfTheMoonCureIcon = AssetLoader.LoadInternal("Skills", "Icon_TouchOfTheMoonCure.jpg");
            var TouchoftheMoonLightWoundsBuff = Helpers.CreateBuff("TouchoftheMoonLightWoundsBuff", bp => {
                bp.SetName("Touch of the Moon - Temporary Health");
                bp.SetDescription("You have been granted temporary health from a allied touch of the moon cure spell. \nThis temporary health will be replaced if affected by another " +
                    "instance of touch of the moon.");
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.AddComponent<TemporaryHitPointsFromAbilityValue>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.Heal
                    };
                    c.RemoveWhenHitPointsEnd = true;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Heal;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.D4,
                        DiceCountValue = 1,
                        BonusValue = 1,
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => { c.Descriptor = SpellDescriptor.TemporaryHP; });
                bp.Stacking = StackingType.Replace;
            });
            var TouchoftheMoonModerateWoundsBuff = Helpers.CreateBuff("TouchoftheMoonModerateWoundsBuff", bp => {
                bp.SetName("Touch of the Moon - Temporary Health");
                bp.SetDescription("You have been granted temporary health from a allied touch of the moon cure spell. \nThis temporary health will be replaced if affected by another " +
                    "instance of touch of the moon.");
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.AddComponent<TemporaryHitPointsFromAbilityValue>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.Heal
                    };
                    c.RemoveWhenHitPointsEnd = true;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Heal;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.D4,
                        DiceCountValue = 2,
                        BonusValue = 2,
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => { c.Descriptor = SpellDescriptor.TemporaryHP; });
                bp.Stacking = StackingType.Replace;
            });
            var TouchoftheMoonSeriousWoundsBuff = Helpers.CreateBuff("TouchoftheMoonSeriousWoundsBuff", bp => {
                bp.SetName("Touch of the Moon - Temporary Health");
                bp.SetDescription("You have been granted temporary health from a allied touch of the moon cure spell. \nThis temporary health will be replaced if affected by another " +
                    "instance of touch of the moon.");
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.AddComponent<TemporaryHitPointsFromAbilityValue>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.Heal
                    };
                    c.RemoveWhenHitPointsEnd = true;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Heal;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.D4,
                        DiceCountValue = 3,
                        BonusValue = 3,
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => { c.Descriptor = SpellDescriptor.TemporaryHP; });
                bp.Stacking = StackingType.Replace;
            });
            var TouchoftheMoonCriticalWoundsBuff = Helpers.CreateBuff("TouchoftheMoonCriticalWoundsBuff", bp => {
                bp.SetName("Touch of the Moon - Temporary Health");
                bp.SetDescription("You have been granted temporary health from a allied touch of the moon cure spell. \nThis temporary health will be replaced if affected by another " +
                    "instance of touch of the moon.");
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.AddComponent<TemporaryHitPointsFromAbilityValue>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.Heal
                    };
                    c.RemoveWhenHitPointsEnd = true;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Heal;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.D4,
                        DiceCountValue = 4,
                        BonusValue = 4,
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => { c.Descriptor = SpellDescriptor.TemporaryHP; });
                bp.Stacking = StackingType.Replace;
            });
            var TouchOfTheMoonCureBuffCure1 = Helpers.CreateBuff("TouchOfTheMoonCureBuffCure1", bp => {
                bp.SetName("Touch of the Moon - Cure Level 1");
                bp.SetDescription("The next level 1 cure spell you cast this round will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell to all targets. " +
                    "Hit points healed this way expire after a number of minutes equal to half your oracle level.");
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.Stacking = StackingType.Replace;
            });
            var TouchOfTheMoonCureBuffCure2 = Helpers.CreateBuff("TouchOfTheMoonCureBuffCure2", bp => {
                bp.SetName("Touch of the Moon - Cure Level 2");
                bp.SetDescription("The next level 2 cure spell you cast this round will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell to all targets. " +
                    "Hit points healed this way expire after a number of minutes equal to half your oracle level.");
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.Stacking = StackingType.Replace;
            });
            var TouchOfTheMoonCureBuffCure3 = Helpers.CreateBuff("TouchOfTheMoonCureBuffCure3", bp => {
                bp.SetName("Touch of the Moon - Cure Level 3");
                bp.SetDescription("The next level 3 cure spell you cast this round will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell to all targets. " +
                    "Hit points healed this way expire after a number of minutes equal to half your oracle level.");
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.Stacking = StackingType.Replace;
            });
            var TouchOfTheMoonCureBuffCure4 = Helpers.CreateBuff("TouchOfTheMoonCureBuffCure4", bp => {
                bp.SetName("Touch of the Moon - Cure Level 4");
                bp.SetDescription("The next level 4 cure spell you cast this round will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell to all targets. " +
                    "Hit points healed this way expire after a number of minutes equal to half your oracle level.");
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.Stacking = StackingType.Replace;
            });
            var TouchOfTheMoonCureBuffCure5 = Helpers.CreateBuff("TouchOfTheMoonCureBuffCure5", bp => {
                bp.SetName("Touch of the Moon - Cure Level 5");
                bp.SetDescription("The next level 5 cure spell you cast this round will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell to all targets. " +
                    "Hit points healed this way expire after a number of minutes equal to half your oracle level.");
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.Stacking = StackingType.Replace;
            });
            var TouchOfTheMoonCureBuffCure6 = Helpers.CreateBuff("TouchOfTheMoonCureBuffCure6", bp => {
                bp.SetName("Touch of the Moon - Cure Level 6");
                bp.SetDescription("The next level 6 cure spell you cast this round will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell to all targets. " +
                    "Hit points healed this way expire after a number of minutes equal to half your oracle level.");
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.Stacking = StackingType.Replace;
            });
            var TouchOfTheMoonCureBuffCure7 = Helpers.CreateBuff("TouchOfTheMoonCureBuffCure7", bp => {
                bp.SetName("Touch of the Moon - Cure Level 7");
                bp.SetDescription("The next level 7 cure spell you cast this round will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell to all targets. " +
                    "Hit points healed this way expire after a number of minutes equal to half your oracle level.");
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.Stacking = StackingType.Replace;
            });
            var TouchOfTheMoonCureBuffCure8 = Helpers.CreateBuff("TouchOfTheMoonCureBuffCure8", bp => {
                bp.SetName("Touch of the Moon - Cure Level 8");
                bp.SetDescription("The next level 8 cure spell you cast this round will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell to all targets. " +
                    "Hit points healed this way expire after a number of minutes equal to half your oracle level.");
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.Stacking = StackingType.Replace;
            });
            var TouchOfTheMoonCureBuffCure9 = Helpers.CreateBuff("TouchOfTheMoonCureBuffCure9", bp => {
                bp.SetName("Touch of the Moon - Cure Level 9");
                bp.SetDescription("The next level 9 cure spell you cast this round will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell to all targets. " +
                    "Hit points healed this way expire after a number of minutes equal to half your oracle level.");
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.Stacking = StackingType.Replace;
            });
            CreateTouchoftheMoonCureActions(TouchOfTheMoonCureBuffCure1, 1);
            CreateTouchoftheMoonCureActions(TouchOfTheMoonCureBuffCure2, 2);
            CreateTouchoftheMoonCureActions(TouchOfTheMoonCureBuffCure3, 3);
            CreateTouchoftheMoonCureActions(TouchOfTheMoonCureBuffCure4, 4);
            CreateTouchoftheMoonCureActions(TouchOfTheMoonCureBuffCure5, 5);
            CreateTouchoftheMoonCureActions(TouchOfTheMoonCureBuffCure6, 6);
            CreateTouchoftheMoonCureActions(TouchOfTheMoonCureBuffCure7, 7);
            CreateTouchoftheMoonCureActions(TouchOfTheMoonCureBuffCure8, 8);
            CreateTouchoftheMoonCureActions(TouchOfTheMoonCureBuffCure9, 9);
            var TouchOfTheMoonCureAbilityCure1 = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfTheMoonCureAbilityCure1", bp => {
                bp.SetName("Touch of the Moon - Cure Level 1");
                bp.SetDescription("Cure spells are potentially more effective but entirely in the target’s mind. As a free action you may spend a spell slot, the next cure spell " +
                    "you cast this round of the same level will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell. Hit points " +
                    "healed this way expire after a number of minutes equal to half your oracle level.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TouchOfTheMoonCureBuffCure1.ToReference<BlueprintBuffReference>(),
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
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var TouchOfTheMoonCureAbilityCure2 = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfTheMoonCureAbilityCure2", bp => {
                bp.SetName("Touch of the Moon - Cure Level 2");
                bp.SetDescription("Cure spells are potentially more effective but entirely in the target’s mind. As a free action you may spend a spell slot, the next cure spell " +
                    "you cast this round of the same level will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell. Hit points " +
                    "healed this way expire after a number of minutes equal to half your oracle level.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TouchOfTheMoonCureBuffCure2.ToReference<BlueprintBuffReference>(),
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
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var TouchOfTheMoonCureAbilityCure3 = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfTheMoonCureAbilityCure3", bp => {
                bp.SetName("Touch of the Moon - Cure Level 3");
                bp.SetDescription("Cure spells are potentially more effective but entirely in the target’s mind. As a free action you may spend a spell slot, the next cure spell " +
                    "you cast this round of the same level will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell. Hit points " +
                    "healed this way expire after a number of minutes equal to half your oracle level.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TouchOfTheMoonCureBuffCure3.ToReference<BlueprintBuffReference>(),
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
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var TouchOfTheMoonCureAbilityCure4 = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfTheMoonCureAbilityCure4", bp => {
                bp.SetName("Touch of the Moon - Cure Level 4");
                bp.SetDescription("Cure spells are potentially more effective but entirely in the target’s mind. As a free action you may spend a spell slot, the next cure spell " +
                    "you cast this round of the same level will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell. Hit points " +
                    "healed this way expire after a number of minutes equal to half your oracle level.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TouchOfTheMoonCureBuffCure4.ToReference<BlueprintBuffReference>(),
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
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var TouchOfTheMoonCureAbilityCure5 = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfTheMoonCureAbilityCure5", bp => {
                bp.SetName("Touch of the Moon - Cure Level 5");
                bp.SetDescription("Cure spells are potentially more effective but entirely in the target’s mind. As a free action you may spend a spell slot, the next cure spell " +
                    "you cast this round of the same level will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell. Hit points " +
                    "healed this way expire after a number of minutes equal to half your oracle level.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TouchOfTheMoonCureBuffCure5.ToReference<BlueprintBuffReference>(),
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
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var TouchOfTheMoonCureAbilityCure6 = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfTheMoonCureAbilityCure6", bp => {
                bp.SetName("Touch of the Moon - Cure Level 6");
                bp.SetDescription("Cure spells are potentially more effective but entirely in the target’s mind. As a free action you may spend a spell slot, the next cure spell " +
                    "you cast this round of the same level will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell. Hit points " +
                    "healed this way expire after a number of minutes equal to half your oracle level.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TouchOfTheMoonCureBuffCure6.ToReference<BlueprintBuffReference>(),
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
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var TouchOfTheMoonCureAbilityCure7 = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfTheMoonCureAbilityCure7", bp => {
                bp.SetName("Touch of the Moon - Cure Level 7");
                bp.SetDescription("Cure spells are potentially more effective but entirely in the target’s mind. As a free action you may spend a spell slot, the next cure spell " +
                    "you cast this round of the same level will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell. Hit points " +
                    "healed this way expire after a number of minutes equal to half your oracle level.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TouchOfTheMoonCureBuffCure7.ToReference<BlueprintBuffReference>(),
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
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var TouchOfTheMoonCureAbilityCure8 = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfTheMoonCureAbilityCure8", bp => {
                bp.SetName("Touch of the Moon - Cure Level 8");
                bp.SetDescription("Cure spells are potentially more effective but entirely in the target’s mind. As a free action you may spend a spell slot, the next cure spell " +
                    "you cast this round of the same level will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell. Hit points " +
                    "healed this way expire after a number of minutes equal to half your oracle level.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TouchOfTheMoonCureBuffCure8.ToReference<BlueprintBuffReference>(),
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
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var TouchOfTheMoonCureAbilityCure9 = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfTheMoonCureAbilityCure9", bp => {
                bp.SetName("Touch of the Moon - Cure Level 9");
                bp.SetDescription("Cure spells are potentially more effective but entirely in the target’s mind. As a free action you may spend a spell slot, the next cure spell " +
                    "you cast this round of the same level will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell. Hit points " +
                    "healed this way expire after a number of minutes equal to half your oracle level.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TouchOfTheMoonCureBuffCure9.ToReference<BlueprintBuffReference>(),
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
                bp.m_Icon = TouchOfTheMoonCureIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationTouchOfTheMoonCureFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationTouchOfTheMoonCureFeature", bp => {
                bp.SetName("Touch of the Moon - Cure");
                bp.SetDescription("Cure spells are potentially more effective but entirely in the target’s mind. As a free action you may spend a spell slot, the next cure spell " +
                    "you cast this round of the same level will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell. Hit points " +
                    "healed this way expire after a number of minutes equal to half your oracle level.");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        TouchOfTheMoonCureAbilityCure1.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure2.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure3.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure4.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure5.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure6.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure7.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure8.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure9.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        TouchOfTheMoonCureAbilityCure1.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure2.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure3.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure4.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure5.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure6.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure7.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure8.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure9.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        TouchOfTheMoonCureAbilityCure1.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure2.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure3.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure4.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure5.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure6.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure7.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure8.ToReference<BlueprintAbilityReference>(),
                        TouchOfTheMoonCureAbilityCure9.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = true;
                    c.m_Feature = OracleCureSpells;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = true;
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 7;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = true;
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 7;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationTouchOfTheMoonInflictFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationTouchOfTheMoonInflictFeature", bp => {
                bp.SetName("Touch of the Moon - Inflict");
                bp.SetDescription("When you cast inflict spells, these spells carry with them the taint of madness. Subjects who take damage from your inflict spells are also subject to confusion, as the spell, " +
                    "except the duration of this effect is a number of rounds equal to the level of the inflict spell. The save DC against this effect is 10 + 1/2 your oracle level + your Charisma modifier.");
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = true;
                    c.m_Feature = OracleInflictSpells;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = true;
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 7;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            CreateTouchoftheMoonInflictActions(OracleRevelationTouchOfTheMoonInflictFeature, 1);
            CreateTouchoftheMoonInflictActions(OracleRevelationTouchOfTheMoonInflictFeature, 2);
            CreateTouchoftheMoonInflictActions(OracleRevelationTouchOfTheMoonInflictFeature, 3);
            CreateTouchoftheMoonInflictActions(OracleRevelationTouchOfTheMoonInflictFeature, 4);
            CreateTouchoftheMoonInflictActions(OracleRevelationTouchOfTheMoonInflictFeature, 5);
            CreateTouchoftheMoonInflictActions(OracleRevelationTouchOfTheMoonInflictFeature, 6);
            CreateTouchoftheMoonInflictActions(OracleRevelationTouchOfTheMoonInflictFeature, 7);
            CreateTouchoftheMoonInflictActions(OracleRevelationTouchOfTheMoonInflictFeature, 8);
            CreateTouchoftheMoonInflictActions(OracleRevelationTouchOfTheMoonInflictFeature, 9);
            var OracleRevelationTouchOfTheMoonSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("OracleRevelationTouchOfTheMoonSelection", bp => {
                bp.SetName("Touch of the Moon");
                bp.SetDescription("The exact effects of this revelation depend on whether you cast inflict or cure spells. " +
                    "\nIf you cast inflict spells, these spells carry with them the taint of madness. Subjects who take damage from your inflict spells are also subject to confusion, as the spell, " +
                    "except the duration of this effect is a number of rounds equal to the level of the inflict spell. The save DC against this effect is 10 + 1/2 your oracle level + your Charisma modifier. " +
                    "\nAlternatively, if you cast cure spells, these spells are potentially more effective but entirely in the target’s mind. As a free action you may spend a spell slot, the next cure spell " +
                    "you cast this round of the same level will also grant temporary hit points equal to 1d4 + 1 for each d8 that would normally be healed by the spell. Hit points " +
                    "healed this way expire after a number of minutes equal to half your oracle level. \nYou must be at least 7th level to select this revelation.");
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
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 7;
                    c.Group = Prerequisite.GroupType.Any;
                });
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.OracleRevelation;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddFeatures(OracleRevelationTouchOfTheMoonCureFeature, OracleRevelationTouchOfTheMoonInflictFeature);
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationTouchOfTheMoonSelection.ToReference<BlueprintFeatureReference>());
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
                bp.AddFeatures(OracleRevelationBeastFormProgression, OracleRevelationGiftOfClawAndHornProgression, OracleRevelationMoonbeamFeature, 
                    OracleRevelationPrimalCompanion, OracleRevelationPropheticArmorFeature, OracleRevelationTouchOfTheMoonSelection);
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
            MysteryTools.RegisterHermitMystery(OracleLunarMysteryFeature);
            MysteryTools.RegisterSecondHermitMystery(OracleLunarMysteryFeature);
            MysteryTools.RegisterMysteryGiftSelection(OracleLunarMysteryFeature);
        }

        private static void CreateTouchoftheMoonCureActions(BlueprintBuff buff, int spelllevel) {

            var TouchoftheMoonLightWoundsBuff = Resources.GetModBlueprint<BlueprintBuff>("TouchoftheMoonLightWoundsBuff");
            var TouchoftheMoonModerateWoundsBuff = Resources.GetModBlueprint<BlueprintBuff>("TouchoftheMoonModerateWoundsBuff");
            var TouchoftheMoonSeriousWoundsBuff = Resources.GetModBlueprint<BlueprintBuff>("TouchoftheMoonSeriousWoundsBuff");
            var TouchoftheMoonCriticalWoundsBuff = Resources.GetModBlueprint<BlueprintBuff>("TouchoftheMoonCriticalWoundsBuff");

            var curelight = Resources.GetBlueprintReference<BlueprintAbilityReference>("5590652e1c2225c4ca30c4a699ab3649");
            var curemoderate = Resources.GetBlueprintReference<BlueprintAbilityReference>("6b90c773a6543dc49b2505858ce33db5");
            var cureserious = Resources.GetBlueprintReference<BlueprintAbilityReference>("3361c5df793b4c8448756146a88026ad");
            var curecritical = Resources.GetBlueprintReference<BlueprintAbilityReference>("41c9016596fe1de4faf67425ed691203");
            var curelightmass = Resources.GetBlueprintReference<BlueprintAbilityReference>("5d3d689392e4ff740a761ef346815074");
            var curemoderatemass = Resources.GetBlueprintReference<BlueprintAbilityReference>("571221cc141bc21449ae96b3944652aa");
            var cureseriousmass = Resources.GetBlueprintReference<BlueprintAbilityReference>("0cea35de4d553cc439ae80b3a8724397");
            var curecriticalmass = Resources.GetBlueprintReference<BlueprintAbilityReference>("1f173a16120359e41a20fc75bb53d449");

            var OracleClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("20ce9bf8af32bee4c8557a045ab499b1");
            var InquisitorClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var RavenerHunterArchetype = Resources.GetModBlueprint<BlueprintArchetype>("RavenerHunterArchetype");
            var ArcanistClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");


            buff.AddComponent<AddAbilityUseTrigger>(c => {
                c.ActionsOnAllTargets = true;
                c.AfterCast = true;
                c.ForMultipleSpells = true;
                c.Abilities = new List<BlueprintAbilityReference>() {
                    curelight, curelightmass
                };
                c.ExactSpellLevel = true;
                c.ExactSpellLevelLimit = spelllevel;
                c.Action = Helpers.CreateActionList(
                    new Conditional() {
                        ConditionsChecker = new ConditionsChecker() {
                            Operation = Operation.And,
                            Conditions = new Condition[] {
                                new ContextConditionIsAlly() { Not = false }
                            }
                        },
                        IfTrue = Helpers.CreateActionList (
                            new ContextActionApplyBuff() {
                                m_Buff = TouchoftheMoonLightWoundsBuff.ToReference<BlueprintBuffReference>(),
                                DurationValue = new ContextDurationValue() {
                                    m_IsExtendable = true,
                                    Rate = DurationRate.Minutes,
                                    DiceCountValue = new ContextValue(),
                                    BonusValue = new ContextValue() {
                                        ValueType = ContextValueType.Rank,
                                        ValueRank = AbilityRankType.Default
                                    }
                                },
                                AsChild = false
                            }
                            ),
                        IfFalse = Helpers.CreateActionList()
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = TouchoftheMoonModerateWoundsBuff.ToReference<BlueprintBuffReference>()
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = TouchoftheMoonSeriousWoundsBuff.ToReference<BlueprintBuffReference>()
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = TouchoftheMoonCriticalWoundsBuff.ToReference<BlueprintBuffReference>()
                    },
                    new ContextActionRemoveSelf()
                    );
            });
            buff.AddComponent<AddAbilityUseTrigger>(c => {
                c.ActionsOnAllTargets = true;
                c.AfterCast = true;
                c.ForMultipleSpells = true;
                c.Abilities = new List<BlueprintAbilityReference>() {
                    curemoderate, curemoderatemass,
                };
                c.ExactSpellLevel = true;
                c.ExactSpellLevelLimit = spelllevel;
                c.Action = Helpers.CreateActionList(
                    new Conditional() {
                        ConditionsChecker = new ConditionsChecker() {
                            Operation = Operation.And,
                            Conditions = new Condition[] {
                                new ContextConditionIsAlly() { Not = false }
                            }
                        },
                        IfTrue = Helpers.CreateActionList(
                            new ContextActionApplyBuff() {
                                m_Buff = TouchoftheMoonModerateWoundsBuff.ToReference<BlueprintBuffReference>(),
                                DurationValue = new ContextDurationValue() {
                                    m_IsExtendable = true,
                                    Rate = DurationRate.Minutes,
                                    DiceCountValue = new ContextValue(),
                                    BonusValue = new ContextValue() {
                                        ValueType = ContextValueType.Rank,
                                        ValueRank = AbilityRankType.Default
                                    }
                                },
                                AsChild = false
                            }
                            ),
                        IfFalse = Helpers.CreateActionList()
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = TouchoftheMoonLightWoundsBuff.ToReference<BlueprintBuffReference>()
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = TouchoftheMoonSeriousWoundsBuff.ToReference<BlueprintBuffReference>()
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = TouchoftheMoonCriticalWoundsBuff.ToReference<BlueprintBuffReference>()
                    },
                    new ContextActionRemoveSelf()
                    );
            });
            buff.AddComponent<AddAbilityUseTrigger>(c => {
                c.ActionsOnAllTargets = true;
                c.AfterCast = true;
                c.ForMultipleSpells = true;
                c.Abilities = new List<BlueprintAbilityReference>() {
                    cureserious, cureseriousmass,
                };
                c.ExactSpellLevel = true;
                c.ExactSpellLevelLimit = spelllevel;
                c.Action = Helpers.CreateActionList(
                    new Conditional() {
                        ConditionsChecker = new ConditionsChecker() {
                            Operation = Operation.And,
                            Conditions = new Condition[] {
                                new ContextConditionIsAlly() { Not = false }
                            }
                        },
                        IfTrue = Helpers.CreateActionList(
                            new ContextActionApplyBuff() {
                                m_Buff = TouchoftheMoonSeriousWoundsBuff.ToReference<BlueprintBuffReference>(),
                                DurationValue = new ContextDurationValue() {
                                    m_IsExtendable = true,
                                    Rate = DurationRate.Minutes,
                                    DiceCountValue = new ContextValue(),
                                    BonusValue = new ContextValue() {
                                        ValueType = ContextValueType.Rank,
                                        ValueRank = AbilityRankType.Default
                                    }
                                },
                                AsChild = false
                            }
                            ),
                        IfFalse = Helpers.CreateActionList()
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = TouchoftheMoonLightWoundsBuff.ToReference<BlueprintBuffReference>()
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = TouchoftheMoonModerateWoundsBuff.ToReference<BlueprintBuffReference>()
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = TouchoftheMoonCriticalWoundsBuff.ToReference<BlueprintBuffReference>()
                    },
                    new ContextActionRemoveSelf()
                    );
            });
            buff.AddComponent<AddAbilityUseTrigger>(c => {
                c.ActionsOnAllTargets = true;
                c.AfterCast = true;
                c.ForMultipleSpells = true;
                c.Abilities = new List<BlueprintAbilityReference>() {
                    curecritical, curecriticalmass,
                };
                c.ExactSpellLevel = true;
                c.ExactSpellLevelLimit = spelllevel;
                c.Action = Helpers.CreateActionList(
                    new Conditional() {
                        ConditionsChecker = new ConditionsChecker() {
                            Operation = Operation.And,
                            Conditions = new Condition[] {
                                new ContextConditionIsAlly() { Not = false }
                            }
                        },
                        IfTrue = Helpers.CreateActionList(
                            new ContextActionApplyBuff() {
                                m_Buff = TouchoftheMoonCriticalWoundsBuff.ToReference<BlueprintBuffReference>(),
                                DurationValue = new ContextDurationValue() {
                                    m_IsExtendable = true,
                                    Rate = DurationRate.Minutes,
                                    DiceCountValue = new ContextValue(),
                                    BonusValue = new ContextValue() {
                                        ValueType = ContextValueType.Rank,
                                        ValueRank = AbilityRankType.Default
                                    }
                                },
                                AsChild = false
                            }
                            ),
                        IfFalse = Helpers.CreateActionList()
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = TouchoftheMoonLightWoundsBuff.ToReference<BlueprintBuffReference>()
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = TouchoftheMoonModerateWoundsBuff.ToReference<BlueprintBuffReference>()
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = TouchoftheMoonSeriousWoundsBuff.ToReference<BlueprintBuffReference>()
                    },
                    new ContextActionRemoveSelf()
                    );
            });
            buff.AddComponent<ContextRankConfig>(c => {
                c.m_Type = AbilityRankType.Default;
                c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                c.m_Stat = StatType.Unknown;
                c.m_SpecificModifier = ModifierDescriptor.None;
                c.m_Progression = ContextRankProgression.Div2;
                c.m_UseMin = true;
                c.m_Min = 1;
                c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                c.m_Class = new BlueprintCharacterClassReference[] {
                    OracleClass,
                    InquisitorClass,
                    ArcanistClass
                };
            });
        }

        private static void CreateTouchoftheMoonInflictActions(BlueprintFeature feature, int spelllevel) {
            var inflictlight = Resources.GetBlueprintReference<BlueprintAbilityReference>("e5af3674bb241f14b9a9f6b0c7dc3d27");
            var inflictmoderate = Resources.GetBlueprintReference<BlueprintAbilityReference>("65f0b63c45ea82a4f8b8325768a3832d");
            var inflictserious = Resources.GetBlueprintReference<BlueprintAbilityReference>("bd5da98859cf2b3418f6d68ea66cabbe");
            var inflictcritical = Resources.GetBlueprintReference<BlueprintAbilityReference>("651110ed4f117a948b41c05c5c7624c0");
            var inflictlightmass = Resources.GetBlueprintReference<BlueprintAbilityReference>("9da37873d79ef0a468f969e4e5116ad2");
            var inflictmoderatemass = Resources.GetBlueprintReference<BlueprintAbilityReference>("03944622fbe04824684ec29ff2cec6a7");
            var inflictseriousmass = Resources.GetBlueprintReference<BlueprintAbilityReference>("820170444d4d2a14abc480fcbdb49535");
            var inflictcriticalmass = Resources.GetBlueprintReference<BlueprintAbilityReference>("5ee395a2423808c4baf342a4f8395b19");
            var confusionbuff = Resources.GetBlueprintReference<BlueprintBuffReference>("886c7407dc629dc499b9f1465ff382df");
            var OracleRevelationProperty = Resources.GetModBlueprint<BlueprintUnitProperty>("OracleRevelationProperty");
            feature.AddComponent<AddAbilityUseTrigger>(c => {
                c.ActionsOnAllTargets = true;
                c.AfterCast = true;
                c.ForMultipleSpells = true;
                c.Abilities = new List<BlueprintAbilityReference>() {
                    inflictlight, inflictmoderate, inflictserious, inflictcritical, inflictlightmass, inflictmoderatemass, inflictseriousmass, inflictcriticalmass
                };
                c.ExactSpellLevel = true;
                c.ExactSpellLevelLimit = spelllevel;
                c.Action = Helpers.CreateActionList(
                    new Conditional {
                        ConditionsChecker = new ConditionsChecker {
                            Conditions = new Condition[] {
                                    new ContextConditionIsAlly() { Not = true }
                                }
                        },
                        IfTrue = Helpers.CreateActionList(
                            new ContextActionSavingThrow() {
                                m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                Type = SavingThrowType.Fortitude,
                                HasCustomDC = true,
                                CustomDC = new ContextValue() { ValueType = ContextValueType.CasterCustomProperty, m_CustomProperty = OracleRevelationProperty.ToReference<BlueprintUnitPropertyReference>() },
                                Actions = Helpers.CreateActionList(
                                    new ContextActionConditionalSaved() {
                                        Succeed = new ActionList(),
                                        Failed = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = confusionbuff,
                                                Permanent = false,                                                
                                                DurationValue = new ContextDurationValue() {
                                                    Rate = DurationRate.Rounds,
                                                    m_IsExtendable = true,
                                                    DiceCountValue = new ContextValue(),
                                                    BonusValue = spelllevel
                                                },
                                                IsFromSpell = true,
                                            }),
                                    }),
                            }),
                        IfFalse = Helpers.CreateActionList(),
                    }
                    );
            });



        }
    }
}
