using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityModManagerNet.UnityModManager;


namespace ExpandedContent.Config {
    internal class ModSupport {

        protected static bool IsMysticalMayhemEnabled() { return IsModEnabled("MysticalMayhem"); }
        protected static bool IsTabletopTweaksBaseEnabled() { return IsModEnabled("TabletopTweaks-Base"); }






        [HarmonyLib.HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch {
            static bool Initialized;

            [HarmonyAfter()]
            public static void Postfix() {
                if (Initialized) return;
                Initialized = true;

                //Ravener Hunter spelllist patch
                //This is done after other mods have loaded to also grab any spells they may add
                var ClericSpelllist = Resources.GetBlueprint<BlueprintSpellList>("8443ce803d2d31347897a3d85cc32f53");
                var RavenerHunterSpelllist = Resources.GetModBlueprint<BlueprintSpellList>("RavenerHunterSpelllist");
                SpellWithDesriptorAdders.RavenerHunterSpellAdder(ClericSpelllist, RavenerHunterSpelllist);

                //Skulking Hunter spelllist patch
                //This is done after other mods have loaded to also grab any spells they may add
                var RangerSpelllist = Resources.GetBlueprint<BlueprintSpellList>("29f3c338532390546bc5347826a655c4");
                var DruidSpelllist = Resources.GetBlueprint<BlueprintSpellList>("bad8638d40639d04fa2f80a1cac67d6b");
                var WizardSpelllist = Resources.GetBlueprint<BlueprintSpellList>("ba0401fdeb4062f40a7aa95b6f07fe89");
                var SkulkingHunterSpelllist = Resources.GetModBlueprint<BlueprintSpellList>("SkulkingHunterSpelllist");
                SpellWithDesriptorAdders.SkulkingHunterSpellAdder(RangerSpelllist, DruidSpelllist, WizardSpelllist, SkulkingHunterSpelllist);


                if (IsMysticalMayhemEnabled()) {

                    var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
                    var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
                    var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
                    var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
                    var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
                    var ColorSpraySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("91da41b9793a4624797921f221db653c");
                    var RainbowPatternSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4b8265132f9c8174f87ce7fa6d0fe47b");
                    var PrismaticSpraySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("b22fd434bdb60fb4ba1068206402c4cf");
                    var ChainLightningSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("645558d63604747428d55f0dd3a4cb58");
                    var SearingLightSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("bf0accce250381a44b857d4af6c8e10d");
                    var HypnoticPatternAbility = Resources.GetModBlueprint<BlueprintAbility>("HypnoticPatternAbility");
                    var SunburstSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e96424f70ff884947b06f41a765b7658");
                    var BreakEnchantmentSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("7792da00c85b9e042a0fdfc2b66ec9a8");
                    var OwlsWisdomMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("9f5ada581af3db4419b54db77f44e430");
                    var OwlsWisdomSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f0455c9295b53904f9e02fc571dd2ce1");
                    var RemoveBlindnessSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c927a8b0cd3f5174f8c0b67cdbfde539");
                    var ConfusionSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("cf6c901fb7acc904e85c63b342e9c949");
                    var SoundBurstSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c3893092a333b93499fd0a21845aa265");
                    var ShoutSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f09453607e683784c8fca646eec49162");
                    var SongOfDiscordSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d38aaf487e29c3d43a3bffa4a4a55f8f");
                    var ShoutGreaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fd0d3840c48cafb44bb29e8eb74df204");
                    var BrilliantInspirationSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("a5c56f0f699daec44b7aedd8b273b08a");
                    var MeteorSwarmAbility = Resources.GetBlueprintReference<BlueprintAbilityReference>("d0cd103b15494866b0444c1a961bc40f");
                    var OracleHeavensSpells = Resources.GetBlueprint<BlueprintFeature>("53faeeb7938b43eb9d8355a1f696b870");
                    var OceansEchoHeavensSpells = Resources.GetBlueprint<BlueprintFeature>("1cde726627a245aea3b75362f5b7f245");
                    OracleHeavensSpells.RemoveComponents<AddKnownSpell>();
                    OceansEchoHeavensSpells.RemoveComponents<AddKnownSpell>();
                    OracleHeavensSpells.TemporaryContext(bp => {                        
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = ColorSpraySpell;
                            c.SpellLevel = 1;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = HypnoticPatternAbility.ToReference<BlueprintAbilityReference>();
                            c.SpellLevel = 2;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = SearingLightSpell;
                            c.SpellLevel = 3;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = RainbowPatternSpell;
                            c.SpellLevel = 4;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = BreakEnchantmentSpell;
                            c.SpellLevel = 5;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = ChainLightningSpell;
                            c.SpellLevel = 6;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = PrismaticSpraySpell;
                            c.SpellLevel = 7;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = SunburstSpell;
                            c.SpellLevel = 8;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = MeteorSwarmAbility;
                            c.SpellLevel = 9;
                        });
                    });                    
                    OceansEchoHeavensSpells.TemporaryContext(bp => {
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = ColorSpraySpell;
                            c.SpellLevel = 1;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = SoundBurstSpell;
                            c.SpellLevel = 2;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = SearingLightSpell;
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
                            c.m_Spell = SunburstSpell;
                            c.SpellLevel = 8;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = MeteorSwarmAbility;
                            c.SpellLevel = 9;
                        });
                    });
                    var StarsDomainGreaterFeature2Classes = new BlueprintFeature[] {
                        Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Cleric"),
                        Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Inquisitor"),
                        Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Hunter"),
                        Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Paladin"),
                        Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Alchemist"),
                        Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Arcanist"),
                        Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Bard"),
                        Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Bloodrager"),
                        Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Druid"),
                        Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Magus"),
                        Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Oracle"),
                        Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Ranger"),
                        Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Rogue"),
                        Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Shaman"),
                        Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Skald"),
                        Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Sorcerer"),
                        Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Warpriest"),
                        Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Witch"),
                        Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Wizard")
                    };
                    foreach (var StarsDomainGreaterFeature2Class in StarsDomainGreaterFeature2Classes) {
                        StarsDomainGreaterFeature2Class.GetComponents<SpontaneousSpellConversion>().ForEach(c => {
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
                                MeteorSwarmAbility
                            };
                        });
                    }
                    var StarsDomainGreaterFeature2 = Resources.GetBlueprint<BlueprintFeature>("962a7e6f19604aaeac784faa9df3b4af");
                    StarsDomainGreaterFeature2.RemoveComponents<AddAbilityUseTrigger>();
                    StarsDomainGreaterFeature2.AddComponent<AddAbilityUseTrigger>(c => {
                            c.ActionsOnAllTargets = false;
                            c.AfterCast = false;
                            c.ActionsOnTarget = false;
                            c.FromSpellbook = false;
                            c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                            c.ForOneSpell = true;
                            c.m_Ability = MeteorSwarmAbility;
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
                    var StarsDomainProgression = Resources.GetBlueprint<BlueprintProgression>("74f44da8e5ed4f2da62f04ea0b82abe8");
                    var StarsDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("46f8d2ba6d3344fc95d8eb93938e78a1");
                    StarsDomainProgression.SetDescription("\nThe firmament provides you inspiration, and you draw power from the stars’ distant light.\nGuarded Mind: You gain a +2 insight bonus on saving throws " +
                    "against all mind-affecting effects.\nThe Stars Are Right: At 8th level, you may spontaneously cast any of your Stars subdomain spells by swapping out a spell of an equal " +
                    "spell level. Any Stars subdomain spell that you cast heals you an amount of hit point damage equal to the spell’s level; this effect happens as you cast the spell.\nDomain " +
                    "{g|Encyclopedia:Spell}Spells{/g}: entropic shield, hypnotic pattern, blink, dimension door, summon monster V, overwhelming presence, sunbeam, sunburst, meteor swarm.");
                    StarsDomainProgressionSecondary.SetDescription("\nThe firmament provides you inspiration, and you draw power from the stars’ distant light.\nGuarded Mind: You gain a +2 insight bonus on saving throws " +
                    "against all mind-affecting effects.\nThe Stars Are Right: At 8th level, you may spontaneously cast any of your Stars subdomain spells by swapping out a spell of an equal " +
                    "spell level. Any Stars subdomain spell that you cast heals you an amount of hit point damage equal to the spell’s level; this effect happens as you cast the spell.\nDomain " +
                    "{g|Encyclopedia:Spell}Spells{/g}: entropic shield, hypnotic pattern, blink, dimension door, summon monster V, overwhelming presence, sunbeam, sunburst, meteor swarm.");                    
                    var StarsDomainSpellList = Resources.GetModBlueprint<BlueprintSpellList>("StarsDomainSpellList");
                    StarsDomainSpellList.SpellsByLevel
                        .Where(level => level.SpellLevel == 9)
                        .ForEach(level => level.Spells.Clear());
                    StarsDomainSpellList.SpellsByLevel[9].m_Spells.Add(MeteorSwarmAbility);
                    var HeavensSpiritSpellList = Resources.GetModBlueprint<BlueprintSpellList>("HeavensSpiritSpellList");
                    HeavensSpiritSpellList.SpellsByLevel
                        .Where(level => level.SpellLevel == 9)
                        .ForEach(level => level.Spells.Clear());
                    HeavensSpiritSpellList.SpellsByLevel[9].m_Spells.Add(MeteorSwarmAbility);
                    var ShamanHeavensSpiritManifestationFeature = Resources.GetModBlueprint<BlueprintFeature>("ShamanHeavensSpiritManifestationFeature");
                    var ShamanHeavensSpiritProgression = Resources.GetModBlueprint<BlueprintProgression>("ShamanHeavensSpiritProgression");
                    ShamanHeavensSpiritProgression.LevelEntries = ShamanHeavensSpiritProgression.LevelEntries.AppendToArray(Helpers.LevelEntry(20, ShamanHeavensSpiritManifestationFeature));
                }

                if (IsTabletopTweaksBaseEnabled()) {

                    var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
                    var RayOfEnfeeblementSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("450af0402422b0b4980d9c2175869612");
                    var ShieldOfFortificationAbility = Resources.GetModBlueprint<BlueprintAbility>("ShieldOfFortificationAbility");
                    var ShieldOfFortificationGreaterAbility = Resources.GetModBlueprint<BlueprintAbility>("ShieldOfFortificationGreaterAbility");
                    var ClaySkinAbility = Resources.GetModBlueprint<BlueprintAbility>("ClaySkinAbility");
                    var StoneSkinSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c66e86905f7606c4eaa5c774f0357b2b");
                    var HeroismGreaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e15e5e7045fda2244b98c8f010adfe31");
                    var BanishmentSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d361391f645db984bbf58907711a146a");
                    var ProjectionFromSpellsSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("42aa71adc7343714fa92e471baa98d42");
                    var SoundBurstSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c3893092a333b93499fd0a21845aa265");
                    var ShoutSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f09453607e683784c8fca646eec49162");
                    var SongOfDiscordSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d38aaf487e29c3d43a3bffa4a4a55f8f");
                    var ShoutGreaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fd0d3840c48cafb44bb29e8eb74df204");
                    var BrilliantInspirationSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("a5c56f0f699daec44b7aedd8b273b08a");
                    var MagesDisjunctionAbility = Resources.GetBlueprintReference<BlueprintAbilityReference>("85f616bb9d054f669f357636c6c06338 ");
                    var OracleSuccorSpells = Resources.GetBlueprint<BlueprintFeature>("5f3dfabf6cc040f3a12e2803f2c13b49");
                    var OceansEchoSuccorSpells = Resources.GetBlueprint<BlueprintFeature>("cd8640ade16943a187057c1022b79da6");
                    OracleSuccorSpells.RemoveComponents<AddKnownSpell>();
                    OceansEchoSuccorSpells.RemoveComponents<AddKnownSpell>();
                    OracleSuccorSpells.TemporaryContext(bp => {
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = RayOfEnfeeblementSpell;
                            c.SpellLevel = 1;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = ShieldOfFortificationAbility.ToReference<BlueprintAbilityReference>();
                            c.SpellLevel = 2;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = ClaySkinAbility.ToReference<BlueprintAbilityReference>();
                            c.SpellLevel = 3;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = ShieldOfFortificationGreaterAbility.ToReference<BlueprintAbilityReference>();
                            c.SpellLevel = 4;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = StoneSkinSpell;
                            c.SpellLevel = 5;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = HeroismGreaterSpell;
                            c.SpellLevel = 6;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = BanishmentSpell;
                            c.SpellLevel = 7;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = ProjectionFromSpellsSpell;
                            c.SpellLevel = 8;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = MagesDisjunctionAbility;
                            c.SpellLevel = 9;
                        });
                    });
                    OceansEchoSuccorSpells.TemporaryContext(bp => {
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = RayOfEnfeeblementSpell;
                            c.SpellLevel = 1;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = SoundBurstSpell;
                            c.SpellLevel = 2;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = ClaySkinAbility.ToReference<BlueprintAbilityReference>();
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
                            c.m_Spell = ProjectionFromSpellsSpell;
                            c.SpellLevel = 8;
                        });
                        bp.AddComponent<AddKnownSpell>(c => {
                            c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                            c.m_Spell = MagesDisjunctionAbility;
                            c.SpellLevel = 9;
                        });
                    });


                    var WildShapeDragonShapeBiteFeature = Resources.GetModBlueprint<BlueprintFeature>("WildShapeDragonShapeBiteFeature");
                    var MutatedShapeFeaturePrerequisite = Resources.GetBlueprint<BlueprintFeature>("82cb6efb4e3f48cbaf2ea59a3dd1a5cc").GetComponent<PrerequisiteFeaturesFromList>();
                    MutatedShapeFeaturePrerequisite.m_Features = MutatedShapeFeaturePrerequisite.m_Features.AppendToArray(WildShapeDragonShapeBiteFeature.ToReference<BlueprintFeatureReference>());
                    BlueprintFeature MutatedShapeEffect = Resources.GetBlueprint<BlueprintFeature>("84e5049cdf7443f7a4ac6b4ac5c80c0c");
                    var DragonShapeForms = new BlueprintBuff[] {
                        Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeBiteBuff"),
                        Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeBlackBuff"),
                        Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeBlackBuff2"),
                        Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeBlueBuff"),
                        Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeBlueBuff2"),
                        Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeBrassBuff"),
                        Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeBrassBuff2"),
                        Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeGreenBuff"),
                        Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeGreenBuff2"),
                        Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeRedBuff"),
                        Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeRedBuff2"),
                        Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeSilverBuff"),
                        Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeSilverBuff2"),
                        Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeWhiteBuff"),
                        Resources.GetModBlueprint<BlueprintBuff>("WildShapeDragonShapeWhiteBuff2"),
                    };
                    foreach (var DragonShapeForm in DragonShapeForms) {
                        DragonShapeForm.GetComponents<Polymorph>().ForEach(c => {
                            c.m_Facts = c.m_Facts.AppendToArray(MutatedShapeEffect.ToReference<BlueprintUnitFactReference>());
                        });
                    }

                }
            }
        }

        protected static bool IsModEnabled(string modName) {
            return modEntries.Where(mod => mod.Info.Id.Equals(modName) && mod.Enabled && !mod.ErrorOnLoading).Any();
        }
    }
}
