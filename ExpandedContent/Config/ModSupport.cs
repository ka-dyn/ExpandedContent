using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Components;
using ExpandedContent.Utilities;
using HarmonyLib;
using Kingmaker;
using Kingmaker.Assets.UnitLogic.Mechanics.Properties;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints.Root;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.DLC;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.Stores;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TabletopTweaks.Core.NewComponents;
using static Kingmaker.Kingdom.Buffs.KingdomTacticalArmyFeature;
using static UnityModManagerNet.UnityModManager;


namespace ExpandedContent.Config {
    internal class ModSupport {

        protected static bool IsMysticalMayhemEnabled() { return IsModEnabled("MysticalMayhem"); }
        protected static bool IsTabletopTweaksBaseEnabled() { return IsModEnabled("TabletopTweaks-Base"); }
        protected static bool IsPrestigePlusEnabled() { return IsModEnabled("PrestigePlus"); }
        protected static bool IsCharacterOptionsPlusEnabled() { return IsModEnabled("CharacterOptionsPlus"); }
        protected static bool IsMakingFriendsEnabled() { return IsModEnabled("WOTR_MAKING_FRIENDS"); }
        protected static bool IsHomebrewArchetypesEnabled() { if (Resources.GetBlueprint<BlueprintProgression>("0e9edc96f2724444e8aae89d6e8bc225") != null) { return true; } else return false; }

        private static readonly BlueprintDlc Dlc5 = Resources.GetBlueprint<BlueprintDlc>("95a25ca16bd54ce3b3ea56f83538fa0d");




        protected static bool IsDLCEnabled(BlueprintDlc dlcname) {
            IEnumerable<BlueprintDlc> dlcs = BlueprintRoot.Instance.DlcSettings.Dlcs;
            return dlcs.Contains(dlcname);
            //return dlcname.IsAvailable.Equals(true);
        }


        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch {
            static bool Initialized;

            [HarmonyAfter()]
            [HarmonyPostfix]
            [HarmonyPriority(Priority.Last)]

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

                //Soldier of Golarion spelllist patch
                //This is done after other mods have loaded to also grab any spells they may add
                var SoldierOfGaiaSpelllist = Resources.GetModBlueprint<BlueprintSpellList>("SoldierOfGaiaSpelllist");
                SoldierOfGaiaSpelllist.SpellsByLevel[0].m_Spells = ClericSpelllist.SpellsByLevel[0].m_Spells;
                SoldierOfGaiaSpelllist.SpellsByLevel[1].m_Spells = ClericSpelllist.SpellsByLevel[1].m_Spells;
                SoldierOfGaiaSpelllist.SpellsByLevel[2].m_Spells = ClericSpelllist.SpellsByLevel[2].m_Spells;
                SoldierOfGaiaSpelllist.SpellsByLevel[3].m_Spells = ClericSpelllist.SpellsByLevel[3].m_Spells;
                SoldierOfGaiaSpelllist.SpellsByLevel[4].m_Spells = ClericSpelllist.SpellsByLevel[4].m_Spells;
                SoldierOfGaiaSpelllist.SpellsByLevel[5].m_Spells = ClericSpelllist.SpellsByLevel[5].m_Spells;
                SoldierOfGaiaSpelllist.SpellsByLevel[6].m_Spells = ClericSpelllist.SpellsByLevel[6].m_Spells;




                if (IsDLCEnabled(Dlc5)) {
                    Main.Log("You should only be able to see this with DLC5 installed, but this is broke so you can see it reguardless.");
                }

                if (IsMysticalMayhemEnabled()) {
                    Main.Log("Starting Mystical Mayhem Compat Patch.");
                    //var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
                    //var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
                    //var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
                    //var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
                    //var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
                    //var ColorSpraySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("91da41b9793a4624797921f221db653c");
                    //var RainbowPatternSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4b8265132f9c8174f87ce7fa6d0fe47b");
                    //var PrismaticSpraySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("b22fd434bdb60fb4ba1068206402c4cf");
                    //var ChainLightningSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("645558d63604747428d55f0dd3a4cb58");
                    //var SearingLightSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("bf0accce250381a44b857d4af6c8e10d");
                    //var HypnoticPatternAbility = Resources.GetModBlueprint<BlueprintAbility>("HypnoticPatternAbility");
                    //var SunburstSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e96424f70ff884947b06f41a765b7658");
                    //var BreakEnchantmentSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("7792da00c85b9e042a0fdfc2b66ec9a8");
                    //var OwlsWisdomMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("9f5ada581af3db4419b54db77f44e430");
                    //var OwlsWisdomSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f0455c9295b53904f9e02fc571dd2ce1");
                    //var RemoveBlindnessSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c927a8b0cd3f5174f8c0b67cdbfde539");
                    //var ConfusionSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("cf6c901fb7acc904e85c63b342e9c949");
                    //var SoundBurstSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c3893092a333b93499fd0a21845aa265");
                    //var ShoutSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f09453607e683784c8fca646eec49162");
                    //var SongOfDiscordSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d38aaf487e29c3d43a3bffa4a4a55f8f");
                    //var ShoutGreaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fd0d3840c48cafb44bb29e8eb74df204");
                    //var BrilliantInspirationSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("a5c56f0f699daec44b7aedd8b273b08a");
                    //var MeteorSwarmAbility = Resources.GetBlueprintReference<BlueprintAbilityReference>("d0cd103b15494866b0444c1a961bc40f");
                    //var OracleHeavensSpells = Resources.GetBlueprint<BlueprintFeature>("53faeeb7938b43eb9d8355a1f696b870");
                    //var OceansEchoHeavensSpells = Resources.GetBlueprint<BlueprintFeature>("1cde726627a245aea3b75362f5b7f245");
                    //OracleHeavensSpells.RemoveComponents<AddKnownSpell>();
                    //OceansEchoHeavensSpells.RemoveComponents<AddKnownSpell>();
                    //OracleHeavensSpells.TemporaryContext(bp => {                        
                    //    bp.AddComponent<AddKnownSpell>(c => {
                    //        c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    //        c.m_Spell = ColorSpraySpell;
                    //        c.SpellLevel = 1;
                    //    });
                    //    bp.AddComponent<AddKnownSpell>(c => {
                    //        c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    //        c.m_Spell = HypnoticPatternAbility.ToReference<BlueprintAbilityReference>();
                    //        c.SpellLevel = 2;
                    //    });
                    //    bp.AddComponent<AddKnownSpell>(c => {
                    //        c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    //        c.m_Spell = SearingLightSpell;
                    //        c.SpellLevel = 3;
                    //    });
                    //    bp.AddComponent<AddKnownSpell>(c => {
                    //        c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    //        c.m_Spell = RainbowPatternSpell;
                    //        c.SpellLevel = 4;
                    //    });
                    //    bp.AddComponent<AddKnownSpell>(c => {
                    //        c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    //        c.m_Spell = BreakEnchantmentSpell;
                    //        c.SpellLevel = 5;
                    //    });
                    //    bp.AddComponent<AddKnownSpell>(c => {
                    //        c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    //        c.m_Spell = ChainLightningSpell;
                    //        c.SpellLevel = 6;
                    //    });
                    //    bp.AddComponent<AddKnownSpell>(c => {
                    //        c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    //        c.m_Spell = PrismaticSpraySpell;
                    //        c.SpellLevel = 7;
                    //    });
                    //    bp.AddComponent<AddKnownSpell>(c => {
                    //        c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    //        c.m_Spell = SunburstSpell;
                    //        c.SpellLevel = 8;
                    //    });
                    //    bp.AddComponent<AddKnownSpell>(c => {
                    //        c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    //        c.m_Spell = MeteorSwarmAbility;
                    //        c.SpellLevel = 9;
                    //    });
                    //});                    
                    //OceansEchoHeavensSpells.TemporaryContext(bp => {
                    //    bp.AddComponent<AddKnownSpell>(c => {
                    //        c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    //        c.m_Spell = ColorSpraySpell;
                    //        c.SpellLevel = 1;
                    //    });
                    //    bp.AddComponent<AddKnownSpell>(c => {
                    //        c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    //        c.m_Spell = SoundBurstSpell;
                    //        c.SpellLevel = 2;
                    //    });
                    //    bp.AddComponent<AddKnownSpell>(c => {
                    //        c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    //        c.m_Spell = SearingLightSpell;
                    //        c.SpellLevel = 3;
                    //    });
                    //    bp.AddComponent<AddKnownSpell>(c => {
                    //        c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    //        c.m_Spell = ShoutSpell;
                    //        c.SpellLevel = 4;
                    //    });
                    //    bp.AddComponent<AddKnownSpell>(c => {
                    //        c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    //        c.m_Spell = SongOfDiscordSpell;
                    //        c.SpellLevel = 5;
                    //    });
                    //    bp.AddComponent<AddKnownSpell>(c => {
                    //        c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    //        c.m_Spell = ShoutGreaterSpell;
                    //        c.SpellLevel = 6;
                    //    });
                    //    bp.AddComponent<AddKnownSpell>(c => {
                    //        c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    //        c.m_Spell = BrilliantInspirationSpell;
                    //        c.SpellLevel = 7;
                    //    });
                    //    bp.AddComponent<AddKnownSpell>(c => {
                    //        c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    //        c.m_Spell = SunburstSpell;
                    //        c.SpellLevel = 8;
                    //    });
                    //    bp.AddComponent<AddKnownSpell>(c => {
                    //        c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    //        c.m_Spell = MeteorSwarmAbility;
                    //        c.SpellLevel = 9;
                    //    });
                    //});
                    //var StarsDomainGreaterFeature2Classes = new BlueprintFeature[] {
                    //    Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Cleric"),
                    //    Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Inquisitor"),
                    //    Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Hunter"),
                    //    Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Paladin"),
                    //    Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Alchemist"),
                    //    Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Arcanist"),
                    //    Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Bard"),
                    //    Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Bloodrager"),
                    //    Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Druid"),
                    //    Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Magus"),
                    //    Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Oracle"),
                    //    Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Ranger"),
                    //    Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Rogue"),
                    //    Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Shaman"),
                    //    Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Skald"),
                    //    Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Sorcerer"),
                    //    Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Warpriest"),
                    //    Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Witch"),
                    //    Resources.GetModBlueprint<BlueprintFeature>("StarsDomainGreaterFeature2Wizard")
                    //};
                    //foreach (var StarsDomainGreaterFeature2Class in StarsDomainGreaterFeature2Classes) {
                    //    StarsDomainGreaterFeature2Class.GetComponents<SpontaneousSpellConversion>().ForEach(c => {
                    //        c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                    //            new BlueprintAbilityReference(),
                    //            new BlueprintAbilityReference(),
                    //            new BlueprintAbilityReference(),
                    //            new BlueprintAbilityReference(),
                    //            new BlueprintAbilityReference(),
                    //            new BlueprintAbilityReference(),
                    //            new BlueprintAbilityReference(),
                    //            new BlueprintAbilityReference(),
                    //            new BlueprintAbilityReference(),
                    //            MeteorSwarmAbility
                    //        };
                    //    });
                    //}
                    //var StarsDomainGreaterFeature2 = Resources.GetBlueprint<BlueprintFeature>("962a7e6f19604aaeac784faa9df3b4af");
                    //StarsDomainGreaterFeature2.RemoveComponents<AddAbilityUseTrigger>();
                    //StarsDomainGreaterFeature2.AddComponent<AddAbilityUseTrigger>(c => {
                    //        c.ActionsOnAllTargets = false;
                    //        c.AfterCast = false;
                    //        c.ActionsOnTarget = false;
                    //        c.FromSpellbook = false;
                    //        c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    //        c.ForOneSpell = true;
                    //        c.m_Ability = MeteorSwarmAbility;
                    //        c.ForMultipleSpells = false;
                    //        c.Abilities = new List<BlueprintAbilityReference>();
                    //        c.MinSpellLevel = false;
                    //        c.MinSpellLevelLimit = 0;
                    //        c.ExactSpellLevel = false;
                    //        c.ExactSpellLevelLimit = 0;
                    //        c.CheckAbilityType = false;
                    //        c.Type = AbilityType.Spell;
                    //        c.CheckDescriptor = false;
                    //        c.SpellDescriptor = new SpellDescriptor();
                    //        c.CheckRange = false;
                    //        c.Range = AbilityRange.Touch;
                    //        c.Action = Helpers.CreateActionList(
                    //            new ContextActionOnContextCaster() {
                    //                Actions = Helpers.CreateActionList(
                    //                    new ContextActionHealTarget() {
                    //                        Value = new ContextDiceValue() {
                    //                            DiceType = DiceType.Zero,
                    //                            DiceCountValue = new ContextValue() {
                    //                                ValueType = ContextValueType.Simple,
                    //                                Value = 0,
                    //                                ValueRank = AbilityRankType.Default,
                    //                                ValueShared = AbilitySharedValue.Damage,
                    //                                Property = UnitProperty.None
                    //                            },
                    //                            BonusValue = new ContextValue() {
                    //                                ValueType = ContextValueType.Simple,
                    //                                Value = 9,
                    //                                ValueRank = AbilityRankType.Default,
                    //                                ValueShared = AbilitySharedValue.Damage,
                    //                                Property = UnitProperty.None
                    //                            },
                    //                        }
                    //                    })
                    //            });
                    //    });                    
                    //var StarsDomainProgression = Resources.GetBlueprint<BlueprintProgression>("74f44da8e5ed4f2da62f04ea0b82abe8");
                    //var StarsDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("46f8d2ba6d3344fc95d8eb93938e78a1");
                    //StarsDomainProgression.SetDescription("\nThe firmament provides you inspiration, and you draw power from the stars’ distant light.\nGuarded Mind: You gain a +2 insight bonus on saving throws " +
                    //"against all mind-affecting effects.\nThe Stars Are Right: At 8th level, you may spontaneously cast any of your Stars subdomain spells by swapping out a spell of an equal " +
                    //"spell level. Any Stars subdomain spell that you cast heals you an amount of hit point damage equal to the spell’s level; this effect happens as you cast the spell.\nDomain " +
                    //"{g|Encyclopedia:Spell}Spells{/g}: entropic shield, hypnotic pattern, blink, dimension door, summon monster V, overwhelming presence, sunbeam, sunburst, meteor swarm.");
                    //StarsDomainProgressionSecondary.SetDescription("\nThe firmament provides you inspiration, and you draw power from the stars’ distant light.\nGuarded Mind: You gain a +2 insight bonus on saving throws " +
                    //"against all mind-affecting effects.\nThe Stars Are Right: At 8th level, you may spontaneously cast any of your Stars subdomain spells by swapping out a spell of an equal " +
                    //"spell level. Any Stars subdomain spell that you cast heals you an amount of hit point damage equal to the spell’s level; this effect happens as you cast the spell.\nDomain " +
                    //"{g|Encyclopedia:Spell}Spells{/g}: entropic shield, hypnotic pattern, blink, dimension door, summon monster V, overwhelming presence, sunbeam, sunburst, meteor swarm.");                    
                    //var StarsDomainSpellList = Resources.GetModBlueprint<BlueprintSpellList>("StarsDomainSpellList");
                    //StarsDomainSpellList.SpellsByLevel
                    //    .Where(level => level.SpellLevel == 9)
                    //    .ForEach(level => level.Spells.Clear());
                    //StarsDomainSpellList.SpellsByLevel[9].m_Spells.Add(MeteorSwarmAbility);
                    //var HeavensSpiritSpellList = Resources.GetModBlueprint<BlueprintSpellList>("HeavensSpiritSpellList");
                    //HeavensSpiritSpellList.SpellsByLevel
                    //    .Where(level => level.SpellLevel == 9)
                    //    .ForEach(level => level.Spells.Clear());
                    //HeavensSpiritSpellList.SpellsByLevel[9].m_Spells.Add(MeteorSwarmAbility);
                    //var ShamanHeavensSpiritManifestationFeature = Resources.GetModBlueprint<BlueprintFeature>("ShamanHeavensSpiritManifestationFeature");
                    //var ShamanHeavensSpiritProgression = Resources.GetModBlueprint<BlueprintProgression>("ShamanHeavensSpiritProgression");
                    //ShamanHeavensSpiritProgression.LevelEntries = ShamanHeavensSpiritProgression.LevelEntries.AppendToArray(Helpers.LevelEntry(20, ShamanHeavensSpiritManifestationFeature));
                    Main.Log("Finished Mystical Mayhem Compat Patch.");
                }

                if (!IsTabletopTweaksBaseEnabled()) {
                    var RangerClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("cda0615668a6df14eb36ba19ee881af6");
                    var DivineTrackerArchetype = Resources.GetModBlueprint<BlueprintArchetype>("DivineTrackerArchetype");

                    var IroriFeatureAddFeatureOnClassLevel = Resources.GetBlueprint<BlueprintFeature>("23a77a5985de08349820429ce1b5a234").GetComponent<AddFeatureOnClassLevel>();
                    IroriFeatureAddFeatureOnClassLevel.m_AdditionalClasses = IroriFeatureAddFeatureOnClassLevel.m_AdditionalClasses.AppendToArray(RangerClass);
                    IroriFeatureAddFeatureOnClassLevel.m_Archetypes = IroriFeatureAddFeatureOnClassLevel.m_Archetypes.AppendToArray(DivineTrackerArchetype.ToReference<BlueprintArchetypeReference>());
                    var PuluraFeatureAddFeatureOnClassLevel = Resources.GetBlueprint<BlueprintFeature>("ebb0b46f95dbac74681c78aae895dbd0").GetComponent<AddFeatureOnClassLevel>();
                    PuluraFeatureAddFeatureOnClassLevel.m_AdditionalClasses = PuluraFeatureAddFeatureOnClassLevel.m_AdditionalClasses.AppendToArray(RangerClass);
                    PuluraFeatureAddFeatureOnClassLevel.m_Archetypes = PuluraFeatureAddFeatureOnClassLevel.m_Archetypes.AppendToArray(DivineTrackerArchetype.ToReference<BlueprintArchetypeReference>());
                                        
                }

                if (IsTabletopTweaksBaseEnabled()) {
                    Main.Log("Starting TTT-Base Compat Patch.");
                    #region Oracle Stuff
                    Main.Log("Patching Oracle Stuff");
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
                    Main.Log("Done");

                    #endregion
                    #region Wild Shape Stuff
                    Main.Log("Patching Wild Shape Stuff");
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
                    Main.Log("Done");
                    #endregion
                    #region Natural Spell
                    Main.Log("Patching Natural Spell Stuff");
                    var NaturalSpellPrerequisiteFeaturesFromList = Resources.GetBlueprint<BlueprintFeature>("c806103e27cce6f429e5bf47067966cf").GetComponent<PrerequisiteFeaturesFromList>();
                    var TreesingerWildShapeMandragoraFeature = Resources.GetModBlueprint<BlueprintFeature>("TreesingerWildShapeMandragoraFeature");
                    NaturalSpellPrerequisiteFeaturesFromList.m_Features = NaturalSpellPrerequisiteFeaturesFromList.m_Features.AppendToArray(
                        WildShapeDragonShapeBiteFeature.ToReference<BlueprintFeatureReference>(), 
                        TreesingerWildShapeMandragoraFeature.ToReference<BlueprintFeatureReference>()
                        );
                    Main.Log("Done");
                    #endregion
                    #region Blessing Stuff
                    Main.Log("Patching Blessing Stuff");
                    var newblessingabilities = new BlueprintAbilityReference[] {
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMajorMHBaseAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMajorOHBaseAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHAberrationBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHAgileAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHAnarchicAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHAnimalBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHAxiomaticAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHBleedAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHBrilliantEnergyAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHChaoticOutsiderBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHConstructBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHCorrosiveAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHDisruptionAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHDragonBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHEvilOutsiderBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHFeyBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHFlamingAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHFlamingBurstAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHFrostAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHFuriousAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHGhostTouchAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHGiantBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHGoodOutsiderBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHHeartseekerAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHHolyAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHIcyBurstAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHKeenAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHLawfulOutsiderBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHLivingBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHLycanthropeBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHMagicalBeastBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHNecroticAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHNeutralOutsiderBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHPlantBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHRadiantAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHShockAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHShockingBurstAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHSpeedAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHThunderingAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHUndeadBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHUnholyAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHVerminBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMinorAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHAberrationBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHAgileAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHAnarchicAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHAnimalBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHAxiomaticAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHBleedAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHBrilliantEnergyAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHChaoticOutsiderBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHConstructBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHCorrosiveAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHDisruptionAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHDragonBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHEvilOutsiderBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHFeyBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHFlamingAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHFlamingBurstAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHFrostAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHFuriousAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHGhostTouchAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHGiantBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHGoodOutsiderBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHHeartseekerAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHHolyAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHIcyBurstAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHKeenAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHLawfulOutsiderBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHLivingBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHLycanthropeBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHMagicalBeastBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHNecroticAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHNeutralOutsiderBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHPlantBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHRadiantAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHShockAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHShockingBurstAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHSpeedAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHThunderingAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHUndeadBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHUnholyAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHVerminBaneAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("WarBlessingMajorAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("WarBlessingMinorAbilityAC").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("WarBlessingMinorAbilityAttack").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("WarBlessingMinorAbilityBase").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("WarBlessingMinorAbilitySaves").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("WarBlessingMinorAbilitySpeed").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHAberrationBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHAgileAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHAnarchicAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHAnimalBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHAxiomaticAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHBleedAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHChaoticOutsiderBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHConstructBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHCorrosiveAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHDisruptionAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHDragonBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHEvilOutsiderBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHFeyBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHFlamingAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHFlamingBurstAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHFrostAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHFuriousAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHGhostTouchAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHGiantBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHGoodOutsiderBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHHeartseekerAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHHolyAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHIcyBurstAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHKeenAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHLawfulOutsiderBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHLivingBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHLycanthropeBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHMagicalBeastBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHNecroticAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHNeutralOutsiderBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHPlantBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHRadiantAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHShockAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHShockingBurstAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHSpeedAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHThunderingAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHUndeadBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHUnholyAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMHVerminBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHAberrationBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHAgileAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHAnarchicAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHAnimalBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHAxiomaticAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHBleedAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHChaoticOutsiderBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHConstructBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHCorrosiveAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHDisruptionAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHDragonBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHEvilOutsiderBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHFeyBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHFlamingAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHFlamingBurstAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHFrostAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHFuriousAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHGhostTouchAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHGiantBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHGoodOutsiderBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHHeartseekerAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHHolyAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHIcyBurstAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHKeenAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHLawfulOutsiderBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHLivingBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHLycanthropeBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHMagicalBeastBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHNecroticAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHNeutralOutsiderBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHPlantBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHRadiantAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHShockAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHShockingBurstAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHSpeedAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHThunderingAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHUndeadBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHUnholyAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingOHVerminBaneAbilitySwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("WarBlessingMinorAbilityACSwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("WarBlessingMinorAbilityAttackSwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("WarBlessingMinorAbilitySavesSwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("WarBlessingMinorAbilitySpeedSwift").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("PlantBlessingMinorAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("PlantBlessingMajorAbility").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("CommunityBlessingMinorAbilitySelf").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("CommunityBlessingMinorAbilityOthers").ToReference<BlueprintAbilityReference>(),
                        Resources.GetModBlueprint<BlueprintAbility>("CommunityBlessingMajorAbility").ToReference<BlueprintAbilityReference>(),
                    };
                    var DivineTrackerBlessingSelectionFirst = Resources.GetModBlueprint<BlueprintFeatureSelection>("DivineTrackerBlessingSelectionFirst");
                    var BlessingSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("6d9dcc2a59210a14891aeedb09d406aa");
                    var EnhancedBlessingsFeature = Resources.GetBlueprint<BlueprintFeature>("99700c820ee64463806a41d3015d3a38");
                    EnhancedBlessingsFeature.RemoveComponents<PrerequisiteFeature>();
                    EnhancedBlessingsFeature.AddComponent<PrerequisiteFeaturesFromList>(c => {
                        c.Amount = 1;
                        c.m_Features = new BlueprintFeatureReference[] {
                            DivineTrackerBlessingSelectionFirst.ToReference<BlueprintFeatureReference>(),
                            BlessingSelection.ToReference<BlueprintFeatureReference>()
                        };
                    });
                    EnhancedBlessingsFeature.GetComponent<AutoMetamagic>().Abilities.AddRange(newblessingabilities.ToList());
                    var AbundantBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("c6b0c4208ec246b3a7cce66e75a0c3fb");
                    AbundantBlessingFeature.RemoveComponents<PrerequisiteFeature>();
                    AbundantBlessingFeature.AddComponent<PrerequisiteFeaturesFromList>(c => {
                        c.Amount = 1;
                        c.m_Features = new BlueprintFeatureReference[] {
                            DivineTrackerBlessingSelectionFirst.ToReference<BlueprintFeatureReference>(),
                            BlessingSelection.ToReference<BlueprintFeatureReference>()
                        };
                    });
                    var DTImpossibleBlessingSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("DTImpossibleBlessingSelection");
                    DTImpossibleBlessingSelection.AddFeatures(DivineTrackerBlessingSelectionFirst.m_AllFeatures);
                    FeatTools.AddAsMythicAbility(DTImpossibleBlessingSelection);


                    var QuickenBlessing = Resources.GetBlueprint<BlueprintFeatureSelection>("094d657008ac413f8198a351b573791a");
                    var PaladinClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("bfa11238e7ae3544bbeb4d0b92e897ec");
                    var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
                    var RangerClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("cda0615668a6df14eb36ba19ee881af6");
                    var DivineTrackerArchetype = Resources.GetModBlueprint<BlueprintArchetype>("DivineTrackerArchetype");
                    QuickenBlessing.GetComponent<PrerequisiteClassLevel>().TemporaryContext(c => {
                        c.Group = Prerequisite.GroupType.Any;
                    });
                    QuickenBlessing.AddComponent<PrerequisiteArchetypeLevel>(c => {
                        c.Group = Prerequisite.GroupType.Any;
                        c.Level = 13;
                        c.m_CharacterClass = RangerClass;
                        c.m_Archetype = DivineTrackerArchetype.ToReference<BlueprintArchetypeReference>();
                    });
                    QuickenBlessing.AddComponent<PrerequisiteArchetypeLevel>(c => {
                        c.Group = Prerequisite.GroupType.Any;
                        c.Level = 10;
                        c.m_CharacterClass = PaladinClass;
                        c.m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>();
                    });


                    PatchTTTQuickenBlessingPrerequisites("73084f37bd1244098851720dabfdd7b0", "e1ff99dc3aeaa064e8eecde51c1c4773", "DivineTrackerAirBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("094c4c32d8ed4c849fb23f2069455cc0", "9d991f8374c3def4cb4a6287f370814d", "DivineTrackerAnimalBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("73fecdcb8db94ed79404672b8bccdcb2", "528e316f9f092954b9e38d3a82b1634a", "DivineTrackerChaosBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("f570b791de834d41977940f761261ed1", "3ed6cd88caecec944b837f57b9be176f", "DivineTrackerDarknessBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("bc7687b1d9d94008a2c5fce3af0469e3", "6d11e8b00add90c4f93c2ad6d12885f7", "DivineTrackerDeathBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("6897283b0b9641c9b92dbad935aa1b90", "dd5e75a02e4563e44a0931c6f46fb0a7", "DivineTrackerDestructionBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("e59e8b336fc644f4aeaaf2730116fcf3", "73c37a22bc9a523409a47218d507acf6", "DivineTrackerEarthBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("3766b9b32e0e470daac9f0f4bd606de7", "f38f3abf6ca3a07499a61f96e342bb16", "DivineTrackerEvilBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("3beac8ab95da46e28b2f388070e3e630", "2368212fa3856d74589e924d3e2074d8", "DivineTrackerFireBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("af1f351820df43ca86f1351ab2947838", "60a85144ed37e3a45b343d291dc48079", "DivineTrackerGoodBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("4a0340ec743f4a589e6c23f397372bfc", "f3881a1a7b44dc74c9d76907c94e49f2", "DivineTrackerHealingBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("42a5ad56fe3b48219732541fe375b67f", "9c49504e2e4c66d4aa341348356b47a8", "DivineTrackerLawBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("f1683878346449a3a52709c3605e4c62", "70654ee784fffa74489933a0d2047bbd", "DivineTrackerLuckBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("2f73ce5c332045bdacfebefcc43c9f85", "1754ff61a0805714fa2b89c8c1bb87ad", "DivineTrackerMagicBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("d9ed761ef2484d3d89761a9e1da72e11", "f52af97d05e5de34ea6e0d1b0af740ea", "DivineTrackerNobilityBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("b1c084e9f5574c10a8883dbf04d12691", "c6a3fa9d8d7f942499e4909cd01ca22d", "DivineTrackerProtectionBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("b244a14194154c8cab3bf44b4b0ff9cc", "64a416082927673409deb330af04d6d2", "DivineTrackerReposeBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("260f1c977e4144f0b5a29b0973990af7", "ba825e3c77acaec4386e00f691f8f3be", "DivineTrackerSunBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("4d93dc80eb32405a86ee2abb3cf94f8f", "87641a8efec53d64d853ecc436234dce", "DivineTrackerTravelBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("f5ffcbdc36384c44b6e795fc77b63639", "a8e7abcad0cf8384b9f12c3b075b5cae", "DivineTrackerTrickeryBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("3a68affde38845c08b255a3c571f3777", "0f457943bb99f9b48b709c90bfc0467e", "DivineTrackerWaterBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("2dc4120954184ba49d142e2ed29c9b55", "4172d92c598de1d47aa2c0dd51c05e24", "DivineTrackerWeatherBlessingFeature");
                    //new ones
                    PatchTTTQuickenBlessingPrerequisites("7edeb0ae9dee4320b5d6bfdca526cf55", "4cd28bbb761f490fa418d471383e38c7", "DivineTrackerPlantBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("874c472db4684f3bab936aac86a30643", "2097edd687ff4cdeb33872c048599fc1", "DivineTrackerWarBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("cfce7f2de08a47b99bd1daff0364d408", "516bc13e0e76a834bb3a4c3e3d01c0cf", "DivineTrackerCharmBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("82917a02493c4ca5aef86eb0750782bc", "d082055b649b44c1880442a98f384556", "DivineTrackerGloryBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("fd11c9155cbc489691c96e07b8f845ae", "1585a2eec1984d1496ce03021cfa9738", "DivineTrackerKnowledgeBlessingFeature");
                    PatchTTTQuickenBlessingPrerequisites("40b743a82ccc47a189eea8e0638e236b", "f76713cc1768432cab0d6352f030519d", "DivineTrackerRuneBlessingFeature");


                    var newquickenblessings = new BlueprintFeature[] {//*These were added by Owlcat, keeping Artifice and Community though
                        Resources.GetModBlueprint<BlueprintFeature>("QuickenBlessingArtificeFeature"),
                        //*Resources.GetModBlueprint<BlueprintFeature>("QuickenBlessingWarFeature"),
                        //*Resources.GetModBlueprint<BlueprintFeature>("QuickenBlessingPlantFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("QuickenBlessingCommunityFeature")
                    };
                    foreach (var newquickenblessing in newquickenblessings) {
                        QuickenBlessing.m_AllFeatures = QuickenBlessing.m_AllFeatures.AppendToArray(newquickenblessing.ToReference<BlueprintFeatureReference>());
                        QuickenBlessing.m_Features = QuickenBlessing.m_Features.AppendToArray(newquickenblessing.ToReference<BlueprintFeatureReference>());
                    }
                    //Remove the TTT Artifice and Community options from quicken
                    var TTTQuickenBlessingArtificeFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("");
                    var TTTQuickenBlessingCommunityFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("");

                    QuickenBlessing.m_AllFeatures = QuickenBlessing.m_AllFeatures.RemoveFromArray(TTTQuickenBlessingArtificeFeature);
                    QuickenBlessing.m_Features = QuickenBlessing.m_Features.RemoveFromArray(TTTQuickenBlessingArtificeFeature);
                    QuickenBlessing.m_AllFeatures = QuickenBlessing.m_AllFeatures.RemoveFromArray(TTTQuickenBlessingCommunityFeature);
                    QuickenBlessing.m_Features = QuickenBlessing.m_Features.RemoveFromArray(TTTQuickenBlessingCommunityFeature);
                    Main.Log("Done");

                    #endregion
                    #region God load order stuff
                    Main.Log("Patching God load order Stuff");
                    //I sohuld not need to do this but for some users TTT always loads first
                    var IroriFeatureAddFeatureOnClassLevel = Resources.GetBlueprint<BlueprintFeature>("23a77a5985de08349820429ce1b5a234").GetComponent<AddFeatureOnClassLevelExclude>();
                    IroriFeatureAddFeatureOnClassLevel.m_AdditionalClasses = IroriFeatureAddFeatureOnClassLevel.m_AdditionalClasses.AppendToArray(RangerClass);
                    IroriFeatureAddFeatureOnClassLevel.m_Archetypes = IroriFeatureAddFeatureOnClassLevel.m_Archetypes.AppendToArray(DivineTrackerArchetype.ToReference<BlueprintArchetypeReference>());
                    var PuluraFeature = Resources.GetBlueprint<BlueprintFeature>("ebb0b46f95dbac74681c78aae895dbd0");
                    var SlingStaffProficiency = Resources.GetBlueprint<BlueprintFeature>("a0be067e11f4d8345a8b57a92e52a301");
                    PuluraFeature.AddComponent<AddFeatureOnClassLevelExclude>(c => {
                        c.m_Class = IroriFeatureAddFeatureOnClassLevel.m_Class;
                        c.m_AdditionalClasses = IroriFeatureAddFeatureOnClassLevel.m_AdditionalClasses;
                        c.m_Archetypes = IroriFeatureAddFeatureOnClassLevel.m_Archetypes;
                        c.m_ExcludeClass = IroriFeatureAddFeatureOnClassLevel.m_Class;
                        c.m_AdditionalExcludeClasses = new BlueprintCharacterClassReference[0];
                        c.m_ExcludeArchetypes = IroriFeatureAddFeatureOnClassLevel.m_ExcludeArchetypes;
                        c.m_Feature = SlingStaffProficiency.ToReference<BlueprintFeatureReference>();
                        c.Level = IroriFeatureAddFeatureOnClassLevel.Level;
                        c.BeforeThisLevel = IroriFeatureAddFeatureOnClassLevel.BeforeThisLevel;
                    });
                    Main.Log("Done");
                    #endregion
                    #region Drake Rider stuff
                    Main.Log("Patching Drake Rider Stuff");
                    var ExpertTrainer = Resources.GetBlueprint<BlueprintFeature>("ae97a4eb750d499c837988f62a24e0de");
                    var DrakeRiderArchetype = Resources.GetModBlueprint<BlueprintArchetype>("DrakeRiderArchetype");

                    DrakeRiderArchetype.RemoveFeatures = DrakeRiderArchetype.RemoveFeatures.AppendToArray(new LevelEntry() { 
                        Level = 4,
                        m_Features =  new List<BlueprintFeatureBaseReference>() { ExpertTrainer.ToReference<BlueprintFeatureBaseReference>() }
                    });
                    Main.Log("Done");
                    #endregion
                    #region Shadow Mystery stuff
                    Main.Log("Patching Shadow Mystery Stuff");
                    var ShadowEnchantmentSpell = Resources.GetBlueprint<BlueprintAbility>("d934f706a12b40ec87a9c8baf221b8a9");
                    var ShadowEnchantmentGreaterSpell = Resources.GetBlueprint<BlueprintAbility>("ba07962827484eb38bf0b6aadd9f5f22");
                    var OracleShadowFinalRevelationMetamagic = Resources.GetModBlueprint<BlueprintFeature>("OracleShadowFinalRevelation").GetComponent<AutoMetamagic>();
                    OracleShadowFinalRevelationMetamagic.Abilities.Add(ShadowEnchantmentSpell.ToReference<BlueprintAbilityReference>());
                    OracleShadowFinalRevelationMetamagic.Abilities.Add(ShadowEnchantmentGreaterSpell.ToReference<BlueprintAbilityReference>());

                    var OracleRevelationDarkSecretsSpellList3 = Resources.GetModBlueprint<BlueprintSpellList>("OracleRevelationDarkSecretsSpellList3");
                    var OracleRevelationDarkSecretsSpellList6 = Resources.GetModBlueprint<BlueprintSpellList>("OracleRevelationDarkSecretsSpellList6");
                    OracleRevelationDarkSecretsSpellList3.SpellsByLevel
                        .Where(level => level.SpellLevel == 3)
                        .ForEach(level => level.m_Spells.Add(ShadowEnchantmentSpell.ToReference<BlueprintAbilityReference>()
                        ));
                    OracleRevelationDarkSecretsSpellList6.SpellsByLevel
                        .Where(level => level.SpellLevel == 6)
                        .ForEach(level => level.m_Spells.Add(ShadowEnchantmentGreaterSpell.ToReference<BlueprintAbilityReference>()
                        ));

                    Main.Log("Done");
                    #endregion
                    #region Stargazer Spellbooks
                    Main.Log("Patching Stargazer Spellbook Stuff");
                    var StargazerChannelerOfTheUnknownLevelUp = Resources.GetModBlueprint<BlueprintFeature>("StargazerChannelerOfTheUnknownLevelUp");
                    var StargazerChannelerOfTheUnknownProgression = Resources.GetModBlueprint<BlueprintProgression>("StargazerChannelerOfTheUnknownProgression");
                    var StargazerClericProgression = Resources.GetModBlueprint<BlueprintProgression>("StargazerClericProgression");
                    var ClericClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("67819271767a9dd4fbfd4ae700befea0");
                    var ChannelerOfTheUnknownArchetype = Resources.GetBlueprintReference<BlueprintArchetypeReference>("5cf66f5c604d4c8087fa0c86c712b8d3");
                    var ChannelerOfTheUnknownSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("00aef73f0e574d5ba30ec30e69165fee");

                    StargazerChannelerOfTheUnknownLevelUp.AddComponent<AddSpellbookLevel>(c => {
                        c.m_Spellbook = ChannelerOfTheUnknownSpellbook.ToReference<BlueprintSpellbookReference>();
                    });
                    StargazerChannelerOfTheUnknownProgression.AddComponent<PrerequisiteArchetypeLevel>(c => {
                        c.m_CharacterClass = ClericClass;
                        c.m_Archetype = ChannelerOfTheUnknownArchetype;
                        c.Level = 1;
                        c.HideInUI = false;
                    });
                    StargazerClericProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                        c.m_CharacterClass = ClericClass;
                        c.m_Archetype = ChannelerOfTheUnknownArchetype;
                        c.HideInUI = false;
                    });
                    var StargazerSpellbook = Resources.GetModBlueprint<BlueprintFeatureSelection>("StargazerSpellbook");
                    StargazerSpellbook.m_AllFeatures = StargazerSpellbook.m_AllFeatures.AppendToArray(StargazerChannelerOfTheUnknownProgression.ToReference<BlueprintFeatureReference>());
                    Main.Log("Done");
                    #endregion
                    #region Subdomain stuff
                    Main.Log("Patching Subdomain Stuff");
                    var ArcaneDomainSpellList = Resources.GetModBlueprint<BlueprintSpellList>("ArcaneDomainSpellList");
                    ArcaneDomainSpellList.SpellsByLevel
                        .Where(level => level.SpellLevel == 9)
                        .ForEach(level => level.Spells.Clear());
                    ArcaneDomainSpellList.SpellsByLevel[9].m_Spells.Add(MagesDisjunctionAbility);
                    var arcaneDomainProgressions = new BlueprintProgression[] {
                        Resources.GetModBlueprint<BlueprintProgression>("ArcaneDomainProgression"),
                        Resources.GetModBlueprint<BlueprintProgression>("ArcaneDomainProgressionSecondary"),
                        Resources.GetModBlueprint<BlueprintProgression>("ArcaneDomainProgressionSeparatist")
                    };
                    foreach(var progression in arcaneDomainProgressions) {
                        progression.SetDescription("\nYou are a true scholar of the mystic arts, harnessing the fusion of arcane and divine. " +
                            "\nArcane Beacon: As a standard action you can become a beacon of arcane energy for 2 {g|Encyclopedia:Combat_Round}rounds{/g}. The aura emanates 15 feet from you. " +
                            "All arcane spells cast within the aura either gain a +1 bonus to their caster level or increase their saving throw DC by +1. " +
                            "The caster chooses the benefit when she casts the spell. You can use this ability a number of times per day equal to 3 + your Wisdom modifier. " +
                            "\nDispelling Touch: At 8th level, you can use a {g|SpellsDispelMagicTarget}targeted dispel magic{/g} effect as a melee {g|Encyclopedia:TouchAttack}touch attack{/g}. " +
                            "You can use this ability once per day at 8th level and one additional time per day for every four levels in the class that gave you access to this domain beyond 8th. " +
                            "\nDomain {g|Encyclopedia:Spell}Spells{/g}: magic missile, {g|SpellsResistEnergy}resist energy{/g}, {g|SpellsDispelMagic}dispel magic{/g}, arcane concordance, " +
                            "{g|SpellsSpellResistance}spell resistance{/g}, {g|SpellsGreaterDispelMagic}dispel magic, greater{/g}, {g|SpellsPowerWordBlind}power word blind{/g}, " +
                            "{g|SpellsProtectionFromSpells}protection from spells{/g}, mages disjunction.");
                    }
                    Main.Log("Done");
                    #endregion
                    Main.Log("Finishing TTT-Base Compat Patch.");
                }

                if (IsPrestigePlusEnabled()) {
                    Main.Log("Starting Prestige Plus Compat Patch.");
                    #region Asavir
                    var AsavirCamaraderieProperty = Resources.GetModBlueprint<BlueprintUnitProperty>("AsavirCamaraderieProperty");
                    var AsavirCamaraderieFeature = Resources.GetModBlueprint<BlueprintFeature>("AsavirCamaraderieFeature");
                    var AsavirClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("A29AE95EADB4469DA996FA9B913165CC");
                    var AsavirProgression = Resources.GetBlueprint<BlueprintProgression>("C45A4085D218439A8695BCB2D8CBCB14");
                    AsavirProgression.LevelEntries.Where(entry => entry.Level == 1).FirstOrDefault()?.m_Features.Add(AsavirCamaraderieFeature.ToReference<BlueprintFeatureBaseReference>());
                    AsavirCamaraderieProperty.AddComponent<ClassLevelGetter>(c => {
                        c.Settings = new PropertySettings() {
                            m_Progression = PropertySettings.Progression.StartPlusDivStep,
                            m_StartLevel = 1,
                            m_StepLevel = 4,
                            m_Negate = false,
                            m_LimitType = PropertySettings.LimitType.Max,
                            m_Min = 0,
                            m_Max = 3,
                        };
                        c.m_Class = AsavirClass;
                    });
                    #endregion
                    #region Golden Legionnaire
                    var GoldenLegionnarireImprovedAidProperty = Resources.GetModBlueprint<BlueprintUnitProperty>("GoldenLegionnarireImprovedAidProperty");
                    var GoldenLegionnarireImprovedAidFeature = Resources.GetModBlueprint<BlueprintFeature>("GoldenLegionnarireImprovedAidFeature");
                    var GoldenLegionnaireClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("8DFFB0E03A0E4FC7B1187CC1DC6B832F");
                    var GoldenLegionnaireProgression = Resources.GetBlueprint<BlueprintProgression>("FC85D290971B41488F6FF747E9C70399");
                    GoldenLegionnaireProgression.LevelEntries.Where(entry => entry.Level == 4).FirstOrDefault()?.m_Features.Add(GoldenLegionnarireImprovedAidFeature.ToReference<BlueprintFeatureBaseReference>());
                    GoldenLegionnarireImprovedAidProperty.AddComponent<ClassLevelGetter>(c => {
                        c.Settings = new PropertySettings() {
                            m_Progression = PropertySettings.Progression.StartPlusDivStep,
                            m_StartLevel = 4,
                            m_StepLevel = 5,
                            m_Negate = false,
                            m_LimitType = PropertySettings.LimitType.Max,
                            m_Min = 0,
                            m_Max = 2,
                        };
                        c.m_Class = GoldenLegionnaireClass;
                    });
                    #endregion
                    #region Halfling Opportunist
                    var HalflingOpportunistExcellentAidFeature = Resources.GetModBlueprint<BlueprintFeature>("HalflingOpportunistExcellentAidFeature");
                    var HalflingOpportunistClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("AEA57FFB36F043AB9BA6BFB3B0D9AFF9");
                    var HalflingOpportunistProgression = Resources.GetBlueprint<BlueprintProgression>("BB330A5A194946D3B34D662876F43011");
                    HalflingOpportunistProgression.LevelEntries.Where(entry => entry.Level == 1).FirstOrDefault()?.m_Features.Add(HalflingOpportunistExcellentAidFeature.ToReference<BlueprintFeatureBaseReference>());
                    HalflingOpportunistExcellentAidFeature.AddComponent<ContextRankConfig>(c => {
                        c.m_Type = AbilityRankType.Default;
                        c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                        c.m_Stat = StatType.Unknown;
                        c.m_SpecificModifier = ModifierDescriptor.None;
                        c.m_Progression = ContextRankProgression.OnePlusDiv2;
                        c.m_Class = new BlueprintCharacterClassReference[] { HalflingOpportunistClass };
                    });
                    #endregion
                    Main.Log("Finished Prestige Plus Compat Patch.");
                }

                if (IsCharacterOptionsPlusEnabled()) {
                    Main.Log("Starting Character Options Plus Compat Patch.");
                    var ShadowTrapSpell = Resources.GetBlueprint<BlueprintAbility>("a3a7e60e786646c499b8ad685e625c06");
                    var TouchofBlindnessSpell = Resources.GetBlueprint<BlueprintAbility>("6177af1ba0964f58a0a0c02778e95483");
                    var OracleShadowFinalRevelationMetamagic = Resources.GetModBlueprint<BlueprintFeature>("OracleShadowFinalRevelation").GetComponent<AutoMetamagic>();
                    OracleShadowFinalRevelationMetamagic.Abilities.Add(ShadowTrapSpell.ToReference<BlueprintAbilityReference>());
                    OracleShadowFinalRevelationMetamagic.Abilities.Add(TouchofBlindnessSpell.ToReference<BlueprintAbilityReference>());

                    var OracleRevelationDarkSecretsSpellList1 = Resources.GetModBlueprint<BlueprintSpellList>("OracleRevelationDarkSecretsSpellList1");
                    OracleRevelationDarkSecretsSpellList1.SpellsByLevel
                        .Where(level =>  level.SpellLevel == 1)
                        .ForEach(level => level.m_Spells.Add(ShadowTrapSpell.ToReference<BlueprintAbilityReference>() 
                        ));
                    OracleRevelationDarkSecretsSpellList1.SpellsByLevel
                        .Where(level => level.SpellLevel == 1)
                        .ForEach(level => level.m_Spells.Add(TouchofBlindnessSpell.ToReference<BlueprintAbilityReference>()
                        ));

                    
                    Main.Log("Finished Character Options Plus Compat Patch.");
                }

                if (IsMakingFriendsEnabled()) {
                    Main.Log("Starting Making Friends Compat Patch.");
                    var BlackTentaclesSpell = Resources.GetBlueprint<BlueprintAbility>("b3c05fb405964aba827dbb7eed6b5f0a");
                    var OracleDarkTapestrySpellsConfig = Resources.GetModBlueprint<BlueprintFeature>("OracleDarkTapestrySpells")
                        .GetComponents<AddKnownSpell>()
                        .Where(c => c.SpellLevel == 4);
                    OracleDarkTapestrySpellsConfig.ForEach(c => {
                        c.m_Spell = BlackTentaclesSpell.ToReference<BlueprintAbilityReference>();
                    });
                    Main.Log("Finished Making Friends Plus Compat Patch.");
                }
            }
        }

        [HarmonyPatch(typeof(GameStarter), "FixTMPAssets")]//This patches very late, after localization, so do not use it for anything involving text
        static class BlueprintsCache_Init_Patc {
            static bool Initialized;

            [HarmonyAfter()]
            [HarmonyPostfix]
            [HarmonyPriority(Priority.Last)]

            public static void Postfix() {
                if (Initialized) return;
                Initialized = true;

                if (IsCharacterOptionsPlusEnabled()) {
                    Main.Log("Starting Late Character Options Plus Compat Patch.");
                    Main.Log("Patching Suffocate Wild Talent.");
                    var MonkClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("e8f21e5b58e0569468e420ebea456124");
                    var KineticistClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("42a455d9ec1ad924d889272429eb8391");
                    var WaterDancerArchetype = Resources.GetBlueprintReference<BlueprintArchetypeReference>("748a85a9c88649a6b92cf948fce8e070");
                    var SuffocateWildTalentAbility = Resources.GetBlueprint<BlueprintAbility>("938c3f619f9b4318b90554210e044752");
                    if (SuffocateWildTalentAbility != null) {
                        SuffocateWildTalentAbility.RemoveComponents<ContextCalculateAbilityParamsBasedOnClass>();
                        SuffocateWildTalentAbility.AddComponent<ContextCalculateAbilityParamsBasedOnHigherClass>(c => {
                            c.UseKineticistMainStat = true;
                            c.StatType = StatType.Charisma;
                            c.m_Classes = new BlueprintCharacterClassReference[] {
                                KineticistClass,
                                MonkClass
                            };
                            c.m_Archetypes = new BlueprintArchetypeReference[] {
                                WaterDancerArchetype
                            };
                        });
                    }
                    var SuffocateWildTalentFeature = Resources.GetBlueprint<BlueprintFeature>("2306f7e0445c48069789ff9ddcd6ec11");
                    if (SuffocateWildTalentFeature != null) {
                        SuffocateWildTalentFeature.GetComponent<PrerequisiteClassLevel>().TemporaryContext(c => {
                            c.Group = Prerequisite.GroupType.Any;
                        });
                        SuffocateWildTalentFeature.AddComponent<PrerequisiteArchetypeLevel>(c => {
                            c.Group = Prerequisite.GroupType.Any;
                            c.m_CharacterClass = MonkClass;
                            c.m_Archetype = WaterDancerArchetype;
                            c.Level = 14;
                        });
                    }
                    Main.Log("Suffocate Wild Talent Patched.");
                    Main.Log("Patching Shimmering Mirage Wild Talent.");
                    var ShimmeringMirageFeature = Resources.GetBlueprint<BlueprintFeature>("2438b572-d1dd-4bab-b484-7b8fe4dab6ed");
                    if (ShimmeringMirageFeature != null) {
                        ShimmeringMirageFeature.GetComponent<PrerequisiteClassLevel>().TemporaryContext(c => {
                            c.Group = Prerequisite.GroupType.Any;
                        });
                        ShimmeringMirageFeature.AddComponent<PrerequisiteArchetypeLevel>(c => {
                            c.Group = Prerequisite.GroupType.Any;
                            c.m_CharacterClass = MonkClass;
                            c.m_Archetype = WaterDancerArchetype;
                            c.Level = 12;
                        });
                    }
                    Main.Log("Shimmering Mirage Wild Talent Patched.");

                    Main.Log("Finished Late Character Options Plus Compat Patch.");
                }

                if (IsHomebrewArchetypesEnabled()) {
                    Main.Log("Starting Late Homebrew Archetypes Compat Patch.");
                    #region Void Domain
                    Main.Log("Patching Void Domain.");
                    var VoidDomainAllowedFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("fadd0fd2c0e5a6b4eb59d974a778fb2a");
                    var VoidDeities = new BlueprintFeature[] {  
                        Resources.GetModBlueprint<BlueprintFeature>("AtlachNachaFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("AzathothFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("BlackButterflyFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("CthulhuFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("HasturFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("KerkamothFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("MaatFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("MonadFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("NhimbalothFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("RhanTegothFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("ShubNiggurathFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("YogSothothFeature")
                    };
                    foreach (var deity in VoidDeities) {
                        deity.SetAllowedDomains(VoidDomainAllowedFeature);
                    }

                    var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
                    var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
                    var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
                    var VoidDomainProgressions = new BlueprintProgression[] {
                        Resources.GetBlueprint<BlueprintProgression>("0e9edc96f2724444e8aae89d6e8bc225"),//Normal
                        Resources.GetBlueprint<BlueprintProgression>("d4b6d394869684149a7f2d0b1a8caca0"),//Secondary
                    };
                    foreach (var progression in VoidDomainProgressions) {
                        progression.m_Classes = progression.m_Classes.AppendToArray(
                            new BlueprintProgression.ClassWithLevel {
                                m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                                AdditionalLevel = 0
                            },
                            new BlueprintProgression.ClassWithLevel {
                                m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                                AdditionalLevel = 0
                            }
                        );
                        progression.m_Archetypes = progression.m_Archetypes.AppendToArray(
                            new BlueprintProgression.ArchetypeWithLevel {
                                m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                                AdditionalLevel = 0
                            }
                        );
                    }

                    var GreatTapestrySummonBuffConfig = Resources.GetBlueprint<BlueprintBuff>("a38d60935197cab4198322da3c85785a").GetComponent<ContextRankConfig>();
                    GreatTapestrySummonBuffConfig.m_Class = GreatTapestrySummonBuffConfig.m_Class.AppendToArray(
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>()
                        );
                    GreatTapestrySummonBuffConfig.m_AdditionalArchetypes = GreatTapestrySummonBuffConfig.m_AdditionalArchetypes.AppendToArray(
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>()
                        );
                    Main.Log("Void Domain Patched.");

                    #endregion
                    #region Elder Cultist Lock
                    Main.Log("Patching Elder Cultist Lock.");
                    var NonElderMythosDeities = new BlueprintFeature[] {
                        Resources.GetModBlueprint<BlueprintFeature>("ApepFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("BastetFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("HathorFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("NeithFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("NephthysFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("PtahFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("SekhmetFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("SelketFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("SetFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("SobekFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("BesmaraFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("MilaniFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("NaderiFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("SivanahFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("YdersiusFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("ZyphusFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("DaikitsuFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("FumeiyoshiFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("HeiFengFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("LadyNanbyoFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("LaoShuPoFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("NalinivatiFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("WukongFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("YamatsumiFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("AbraxasFeature"),
                        Resources.GetBlueprint<BlueprintFeature>("d714ecb5d5bb89a42957de0304e459c9"),//AreshkegalFeature
                        Resources.GetBlueprint<BlueprintFeature>("bd72ca8ffcfec5745899ac56c93f12c5"),//BaphometFeature
                        Resources.GetModBlueprint<BlueprintFeature>("CythVsugFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("DagonFeature"),
                        Resources.GetBlueprint<BlueprintFeature>("ddf913858bdf43b4da3b731e082fbcc0"),//DeskariFeature
                        Resources.GetModBlueprint<BlueprintFeature>("GoguntaFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("JezeldaFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("JubilexFeature"),
                        Resources.GetBlueprint<BlueprintFeature>("f12c1ccc9d600c04f8887cd28a8f45a5"),//KabririFeature
                        Resources.GetModBlueprint<BlueprintFeature>("MazmezzFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("MestamaFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("NocticulaFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("NurgalFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("OrcusFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("PazuzuFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("ShaxFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("ShivaskaFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("TreerazerFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("ZuraFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("FindeladlaraFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("KetephysFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("YuelralFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("AshavaFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("BlackButterflyFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("ChadaliFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("ChucaroFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("ImmonhielFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("JalaijataliFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("LalaciFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("PicoperiFeature"),
                        Resources.GetBlueprint<BlueprintFeature>("ebb0b46f95dbac74681c78aae895dbd0"),//PuluraFeature
                        Resources.GetModBlueprint<BlueprintFeature>("SinashaktiFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("TolcFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("ValaniFeature"),
                        Resources.GetBlueprint<BlueprintFeature>("99a7a8f13c1300c42878558fa9471e2f"),//GreenFaithFeature
                        Resources.GetBlueprint<BlueprintFeature>("c3e4d5681906d5246ab8b0637b98cbfe"),//GroetusFeature
                        Resources.GetModBlueprint<BlueprintFeature>("AtroposFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("BarzahkFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("CeyannanFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("IlsurrishFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("MonadFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("NarriseminekFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("SsilameshnikFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("YdajiskFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("DammarFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("ImotFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("MotherVultureFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("MrtyuFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("NarakaasFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("PhlegyasFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("SalocFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("TeshallasFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("ThePaleHorseFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("ValeFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("VavaalravFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("VonymosFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("DrethaFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("LanishraFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("NulgrethFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("RullFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("SezelrianFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("VargFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("VerexFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("ZagreshFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("CountRanalcFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("TheGreenMotherFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("TheLanternKingFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("TheLostPrinceFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("NgFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("RagadahnFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("ShykaFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("ApollyonFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("CharonFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("SzurielFeature"),
                        Resources.GetModBlueprint<BlueprintFeature>("TrelmarixianFeature")
                    };
                    foreach ( var deity in NonElderMythosDeities ) {
                        deity.DisallowElderMythosCultist();
                    }
                    Main.Log("Elder Cultist Lock Patched.");
                    #endregion
                    #region Aid Another
                    Main.Log("Patching Aid Another.");
                    var HAAidAnotherAbility = Resources.GetBlueprint<BlueprintAbility>("96541931e626be84788bb554f90563d1");
                    var FightDefensivelyFeature = Resources.GetBlueprint<BlueprintFeature>("ca22afeb94442b64fb8536e7a9f7dc11")
                        .GetComponents<AddFacts>()
                        .Where(c => c.m_Facts.Any(f => f.deserializedGuid == HAAidAnotherAbility.AssetGuid));
                    FightDefensivelyFeature.ForEach(c => {
                        c.m_Facts = c.m_Facts.RemoveFromArray(HAAidAnotherAbility.ToReference<BlueprintUnitFactReference>());
                    });
                    Main.Log("Aid Another Patched.");
                    #endregion
                    Main.Log("Finished Late Homebrew Archetypes Compat Patch.");

                }
                if (!IsHomebrewArchetypesEnabled()) {
                    Main.Log("Don't worry about 0e9edc96f2724444e8aae89d6e8bc225 not loading, it's all according to plan yesyes.");
                }
            }
        }

        protected static bool IsModEnabled(string modName) {
            return modEntries.Where(mod => mod.Info.Id.Equals(modName) && mod.Enabled && !mod.ErrorOnLoading).Any();
        }

        
        private static void PatchTTTQuickenBlessingPrerequisites(string tttquickenfeatureguid, string warpriestblessingguid, string rangerblessingguid) {

            var quickenBlessingFeature = Resources.GetBlueprint<BlueprintFeature>(tttquickenfeatureguid);
            var warpriestBlessingFeature = Resources.GetBlueprint<BlueprintFeature>(warpriestblessingguid);
            var rangerBlessingFeature = Resources.GetModBlueprint<BlueprintFeature>(rangerblessingguid);

            quickenBlessingFeature.RemoveComponents<PrerequisiteFeature>();
            quickenBlessingFeature.AddComponent<PrerequisiteFeaturesFromList>(c => {
                c.Amount = 1;
                c.m_Features = new BlueprintFeatureReference[] {
                    warpriestBlessingFeature.ToReference<BlueprintFeatureReference>(),
                    rangerBlessingFeature.ToReference<BlueprintFeatureReference>()
                };
            });
        }
    }
}
