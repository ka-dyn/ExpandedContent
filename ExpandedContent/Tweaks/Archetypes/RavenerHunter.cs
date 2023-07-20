using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.Utility;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UI.GenericSlot;
using Kingmaker.Blueprints.Items.Ecnchantments;
using static ExpandedContent.Utilities.SpellTools;
using ExpandedContent.Tweaks.Classes;
using Kingmaker.Designers.Mechanics.Buffs;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class RavenerHunter {
        public static void AddRavenerHunter() {

            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var InquisitorSpellSlotsTable = Resources.GetBlueprint<BlueprintSpellsTable>("83d3e15962e5d6949b90b5c226a2b487");
            var InquisitorSpellsKnownTable = Resources.GetBlueprint<BlueprintSpellsTable>("c133d22305bab964c88a767cc69b1f9b");
            var InquisitorSpelllist = Resources.GetBlueprint<BlueprintSpellList>("57c894665b7895c499b3dce058c284b3");
            var ClericSpelllist = Resources.GetBlueprint<BlueprintSpellList>("8443ce803d2d31347897a3d85cc32f53");
            var DomainSelectionFeature = Resources.GetBlueprint<BlueprintFeatureSelection>("48525e5da45c9c243a343fc6545dbdb9");
            var OracleRevelationSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("60008a10ad7ad6543b1f63016741a5d2");
            var SoloTacticsFeature = Resources.GetBlueprint<BlueprintFeature>("5602845cd22683840a6f28ec46331051");
            var TeamWorkFeat = Resources.GetBlueprint<BlueprintFeatureSelection>("d87e2f6a9278ac04caeb0f93eff95fcb");
            var OracleMysterySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("5531b975dcdf0e24c98f1ff7e017e741");
            var AngelicAspect = Resources.GetBlueprint<BlueprintAbility>("75a10d5a635986641bfbcceceec87217");
            var OracleAncestorsMysteryFeature = Resources.GetBlueprint<BlueprintFeature>("9936554e9372dab4a874c1dd165fb6f8");
            var OracleBattleMysteryFeature = Resources.GetBlueprint<BlueprintFeature>("6b8e676943cb91648b21b7ca72fbb833");
            var OracleFlameMysteryFeature = Resources.GetBlueprint<BlueprintFeature>("3b68909df737cd4458509d7f3a9c3706");
            var OracleLifeMysteryFeature = Resources.GetBlueprint<BlueprintFeature>("a5458a1c316d1f24e8d9770f4fc179fa");
            var OracleNatureMysteryFeature = Resources.GetBlueprint<BlueprintFeature>("69579bfe28e15b942af0722021d8725c");
            var OracleStoneMysteryFeature = Resources.GetBlueprint<BlueprintFeature>("7bb4bb3e7fd26f34e8ca035a27e03e85");
            var OracleWavesMysteryFeature = Resources.GetBlueprint<BlueprintFeature>("e2f8bd7c45dfb954c8c42b168505c783");
            var OracleWindMysteryFeature = Resources.GetBlueprint<BlueprintFeature>("7c1fdd831af747b47bb2cce0051f309b");

            var AcidSkin = Resources.GetBlueprint<BlueprintFeature>("bc1cbe328e1cd214698e6da7fbd9400f");
            var AirBarrier = Resources.GetBlueprint<BlueprintFeature>("7e9743e4be4aad9438a58988cfe521bb");
            var Battlecry = Resources.GetBlueprint<BlueprintFeature>("9a67248ce68bd5f47a36d240ff8196e4");
            var BattlefieldClarity = Resources.GetBlueprint<BlueprintFeature>("c0c2b21d83dd2514c98ae8d3684ad981");
            var BloodOfHeros = Resources.GetBlueprint<BlueprintFeature>("1b9eebdfa2ad9d44eb60446d465b0cfd");
            var BondedMount = Resources.GetBlueprint<BlueprintFeatureSelection>("0234d0dd1cead22428e71a2500afa2e1");
            var BurningMagic = Resources.GetBlueprint<BlueprintFeature>("125294de0a922c34db4cd58ca7a200ac");
            var Channel = Resources.GetBlueprint<BlueprintFeature>("ade57ae9bbe55c142a012c2453b3088c");
            var CinderDance = Resources.GetBlueprint<BlueprintFeature>("6e67eae3081853544b191943f5ed4534");
            var ClobberingStrike = Resources.GetBlueprint<BlueprintFeature>("b830f7ed7a68fa349a08cccc35975bdd");
            var CombatHealer = Resources.GetBlueprint<BlueprintFeature>("db1d9829383e78841a6f1145411a54b4");
            var EnhancedCures = Resources.GetBlueprint<BlueprintFeature>("973a22b02c793ca49b48652e3d70ae80");
            var ErosionTouch = Resources.GetBlueprint<BlueprintFeature>("b459fee5bc4b33449bb883b0ac5a01d8");
            var FireBreath = Resources.GetBlueprint<BlueprintFeature>("5b664b58c911bc944b1572fccf0e7f5f");
            var FluidNature = Resources.GetBlueprint<BlueprintFeature>("e248a57686c53be4cb49b082057850f3");
            var FormOfFlame = Resources.GetBlueprint<BlueprintProgression>("9c8997f740d5a634fb19ac32e0517180");
            var FreezingSpells = Resources.GetBlueprint<BlueprintFeature>("bc2f6769fe042834db7120e3c8a50b47");
            var FriendToAnimals = Resources.GetBlueprint<BlueprintFeature>("9a56368c28795544fbeb43fe70e1a40d");
            var HeatAura = Resources.GetBlueprint<BlueprintFeature>("44dbe8e30c013544d85976671009d79d");
            var IceArmor = Resources.GetBlueprint<BlueprintFeature>("a1cd9835c6699534ca124fab239fdf1c");
            var IceSkin = Resources.GetBlueprint<BlueprintFeature>("cdeba08f8137cb141a9aa2f6fe55f99c");
            var Invisibility = Resources.GetBlueprint<BlueprintFeature>("1f72e7cdcc07d994bac4fb65fc341971");
            var LifeLeach = Resources.GetBlueprint<BlueprintFeature>("5efbf89a8bf2ab34a883810fd0fcc216");
            var LifeLink = Resources.GetBlueprint<BlueprintFeature>("ef97c9bcc1c54ea7993ef8b2489c908a");
            var LightningBreath = Resources.GetBlueprint<BlueprintFeature>("b7b19ce8776f81547ad3bfb6ac859045");
            var ManeuverMastery = Resources.GetBlueprint<BlueprintFeatureSelection>("89629bb513a70cb4596d1d780b95ea72");
            var MightyPebble = Resources.GetBlueprint<BlueprintFeature>("b29d13f63f371664e9ae25bd65e5c463");
            var MoltenSkin = Resources.GetBlueprint<BlueprintFeature>("5fd4d9ba38a9f8745b39fb8baee58337");
            var NatureWhispers = Resources.GetBlueprint<BlueprintFeature>("3d2cd23869f0d98458169b88738f3c32");
            var PhantomTouch = Resources.GetBlueprint<BlueprintFeature>("60a1be62a0edf274580a1cdc5250293e");
            var PunitiveTransformation = Resources.GetBlueprint<BlueprintFeature>("504a1b83335abcb4eb1ba1227a1b9e06");
            var SacredCouncil = Resources.GetBlueprint<BlueprintFeature>("a8ae26103124a1c409279392b8f56238");
            var SafeCuring = Resources.GetBlueprint<BlueprintFeature>("3fa75c1a809882a4697db75daf8803e3");
            var ShardExplosion = Resources.GetBlueprint<BlueprintFeature>("650b9b5efa60c5d4ea25faad3d346fcf");
            var SkillAtArms = Resources.GetBlueprint<BlueprintFeature>("ba63899cf42ba8a459742078a526e7ec");
            var SparkSkin = Resources.GetBlueprint<BlueprintFeature>("279637adc9719db4c93352f71366eba8");
            var SpiritBoost = Resources.GetBlueprint<BlueprintFeature>("8cf1bc6fe4d14304392496ff66023271");
            var SpiritOfNature = Resources.GetBlueprint<BlueprintFeature>("a5ab3559cb7921a4a8c288110be248ec");
            var SpiritShield = Resources.GetBlueprint<BlueprintFeature>("899d6aa944ea9654fa829edcaedcf073");
            var StoneStability = Resources.GetBlueprint<BlueprintFeature>("4244aa481ad9a604db8442c02712c1e0");
            var StormOfSouls = Resources.GetBlueprint<BlueprintFeature>("0edd5395810cf3441a093ca49efb858f");
            var Thunderburst = Resources.GetBlueprint<BlueprintFeature>("55e42ed24a0806140bccdcea79459f6a");
            var TouchOfAcid = Resources.GetBlueprint<BlueprintFeature>("9333c3106d4a6c24d8f22279194aeeff");
            var TouchOfElectricity = Resources.GetBlueprint<BlueprintFeature>("44df0246a8d254e4fb41305135ae2692");
            var TouchOfFlame = Resources.GetBlueprint<BlueprintFeature>("7551135fc3779744396b229b38dca152");
            var VortexSpells = Resources.GetBlueprint<BlueprintFeature>("cd9791a958181544a8b71346ec0722db");
            var WarSight = Resources.GetBlueprint<BlueprintFeature>("84f5169d964185741b97e95a1f1f2a79");
            var WeaponMastery = Resources.GetBlueprint<BlueprintProgression>("0a4c3556355747241b2d3dcc0a88ec10");
            var WintryTouch = Resources.GetBlueprint<BlueprintFeature>("a63e315828427a54492724e06c0bd969");



            var RavenerHunterArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("RavenerHunterArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"RavenerHunterArchetype.Name", "Ravener Hunter");
                bp.LocalizedDescription = Helpers.CreateString($"RavenerHunterArchetype.Description", "Throughout the Mwangi Expanse, cults of Angazhan pollute the pristine jungle with demonic " +
                    "influence and wanton bloodshed. For generations, the catfolk of Murraseth have viewed such faiths with loathing and hatred, and they believe it is their sacred duty to " +
                    "hunt down the followers of the Ravener King and expel them from the Material Plane.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"RavenerHunterArchetype.Description", "Throughout the Mwangi Expanse, cults of Angazhan pollute the pristine jungle with demonic " +
                    "influence and wanton bloodshed. For generations, the catfolk of Murraseth have viewed such faiths with loathing and hatred, and they believe it is their sacred duty to " +
                    "hunt down the followers of the Ravener King and expel them from the Material Plane.");
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.Good | AlignmentMaskType.LawfulNeutral | AlignmentMaskType.TrueNeutral | AlignmentMaskType.ChaoticNeutral;
                });
            });
            var RavenerHunterSpelllist = Helpers.CreateBlueprint<BlueprintSpellList>("RavenerHunterSpelllist", bp => {
                bp.IsMythic = false;
                bp.SpellsByLevel = InquisitorSpelllist.SpellsByLevel;
            });
            var RavenerHunterSpellbook = Helpers.CreateBlueprint<BlueprintSpellbook>("RavenerHunterSpellbook", bp => {
                bp.Name = Helpers.CreateString($"RavenerHunterSpellbook.Name", "Ravener Hunter");
                bp.m_SpellsPerDay = InquisitorSpellSlotsTable.ToReference<BlueprintSpellsTableReference>();
                bp.m_SpellsKnown = InquisitorSpellsKnownTable.ToReference<BlueprintSpellsTableReference>();
                bp.m_SpellList = RavenerHunterSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                bp.CastingAttribute = StatType.Wisdom;
                bp.Spontaneous = true;
                bp.SpellsPerLevel = 0;
                bp.AllSpellsKnown = false;
                bp.CantripsType = CantripsType.Orisions;
                bp.CasterLevelModifier = 0;
                bp.IsArcane = false;
            });
            RavenerHunterArchetype.m_ReplaceSpellbook = RavenerHunterSpellbook.ToReference<BlueprintSpellbookReference>();
            var RavenerHunterHolyMagicFeature = Helpers.CreateBlueprint<BlueprintFeature>("RavenerHunterHolyMagicFeature", bp => {
                bp.SetName("Holy Magic");
                bp.SetDescription("A ravener hunter adds all spells of 6th-level and lower on the cleric spell list with the good descriptor to her inquisitor spell list as inquisitor " +
                    "spells of the same level. If a spell appears on both the cleric and inquisitor spell lists, the ravener hunter uses the lower of the two spell levels listed for the " +
                    "spell. She cannot cast spells with the chaotic, evil, or lawful descriptors, even from spell trigger or spell completion items.");
                bp.m_Icon = AngelicAspect.m_Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<ForbidSpecificSpellsCast>(c => {
                    c.m_Spells = new BlueprintAbilityReference[] { };
                    c.UseSpellDescriptor = true;
                    c.SpellDescriptor = SpellDescriptor.Chaos | SpellDescriptor.Evil | SpellDescriptor.Law;
                });
            });
            var RavenerHunterAncestorsRevelationSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("RavenerHunterAncestorsRevelationSelection", bp => {
                bp.SetName("Ancestors Revelation");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.None;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
                bp.AddFeatures(BloodOfHeros, PhantomTouch, SacredCouncil, SpiritShield, StormOfSouls);
            });
            var RavenerHunterBattleRevelationSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("RavenerHunterBattleRevelationSelection", bp => {
                bp.SetName("Battle Revelation");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.None;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
                bp.AddFeatures(Battlecry, BattlefieldClarity, CombatHealer, ManeuverMastery, SkillAtArms, WarSight, WeaponMastery);
            });
            var RavenerHunterFlameRevelationSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("RavenerHunterFlameRevelationSelection", bp => {
                bp.SetName("Flame Revelation");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.None;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
                bp.AddFeatures(BurningMagic, CinderDance, FireBreath, FormOfFlame, HeatAura, MoltenSkin, TouchOfFlame);
            });
            var RavenerHunterLifeRevelationSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("RavenerHunterLifeRevelationSelection", bp => {
                bp.SetName("Life Revelation");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.None;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
                bp.AddFeatures(Channel, CombatHealer, EnhancedCures, LifeLink, SafeCuring, SpiritBoost);
            });
            var RavenerHunterNatureRevelationSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("RavenerHunterNatureRevelationSelection", bp => {
                bp.SetName("Nature Revelation");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.None;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
                bp.AddFeatures(BondedMount, ErosionTouch, FriendToAnimals, LifeLeach, NatureWhispers, SpiritOfNature);
            });
            var RavenerHunterStoneRevelationSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("RavenerHunterStoneRevelationSelection", bp => {
                bp.SetName("Stone Revelation");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.None;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
                bp.AddFeatures(AcidSkin, ClobberingStrike, MightyPebble, ShardExplosion, StoneStability, TouchOfAcid);
            });
            var RavenerHunterWavesRevelationSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("RavenerHunterWavesRevelationSelection", bp => {
                bp.SetName("Waves Revelation");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.None;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
                bp.AddFeatures(FluidNature, FreezingSpells, IceArmor, IceSkin, PunitiveTransformation, WintryTouch);
            });
            var RavenerHunterWindRevelationSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("RavenerHunterWindRevelationSelection", bp => {
                bp.SetName("Wind Revelation");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.None;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
                bp.AddFeatures(AirBarrier, Invisibility, LightningBreath, SparkSkin, Thunderburst, TouchOfElectricity, VortexSpells);
            });

            var RavenerHunterAncestorsMysteryProgression = Helpers.CreateBlueprint<BlueprintProgression>("RavenerHunterAncestorsMysteryProgression", bp => {
                bp.SetName("Ancestors");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.m_Icon = OracleAncestorsMysteryFeature.m_Icon;
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
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, RavenerHunterAncestorsRevelationSelection),
                    Helpers.LevelEntry(8, RavenerHunterAncestorsRevelationSelection)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RavenerHunterAncestorsRevelationSelection, RavenerHunterAncestorsRevelationSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var RavenerHunterBattleMysteryProgression = Helpers.CreateBlueprint<BlueprintProgression>("RavenerHunterBattleMysteryProgression", bp => {
                bp.SetName("Battle");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.m_Icon = OracleBattleMysteryFeature.m_Icon;
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
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, RavenerHunterBattleRevelationSelection),
                    Helpers.LevelEntry(8, RavenerHunterBattleRevelationSelection)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RavenerHunterBattleRevelationSelection, RavenerHunterBattleRevelationSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var RavenerHunterFlameMysteryProgression = Helpers.CreateBlueprint<BlueprintProgression>("RavenerHunterFlameMysteryProgression", bp => {
                bp.SetName("Flame");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.m_Icon = OracleFlameMysteryFeature.m_Icon;
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
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, RavenerHunterFlameRevelationSelection),
                    Helpers.LevelEntry(8, RavenerHunterFlameRevelationSelection)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RavenerHunterFlameRevelationSelection, RavenerHunterFlameRevelationSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var RavenerHunterLifeMysteryProgression = Helpers.CreateBlueprint<BlueprintProgression>("RavenerHunterLifeMysteryProgression", bp => {
                bp.SetName("Life");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.m_Icon = OracleLifeMysteryFeature.m_Icon;
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
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, RavenerHunterLifeRevelationSelection),
                    Helpers.LevelEntry(8, RavenerHunterLifeRevelationSelection)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RavenerHunterLifeRevelationSelection, RavenerHunterLifeRevelationSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var RavenerHunterNatureMysteryProgression = Helpers.CreateBlueprint<BlueprintProgression>("RavenerHunterNatureMysteryProgression", bp => {
                bp.SetName("Nature");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.m_Icon = OracleNatureMysteryFeature.m_Icon;
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
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, RavenerHunterNatureRevelationSelection),
                    Helpers.LevelEntry(8, RavenerHunterNatureRevelationSelection)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RavenerHunterNatureRevelationSelection, RavenerHunterNatureRevelationSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var RavenerHunterStoneMysteryProgression = Helpers.CreateBlueprint<BlueprintProgression>("RavenerHunterStoneMysteryProgression", bp => {
                bp.SetName("Stone");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.m_Icon = OracleStoneMysteryFeature.m_Icon;
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
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, RavenerHunterStoneRevelationSelection),
                    Helpers.LevelEntry(8, RavenerHunterStoneRevelationSelection)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RavenerHunterStoneRevelationSelection, RavenerHunterStoneRevelationSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var RavenerHunterWavesMysteryProgression = Helpers.CreateBlueprint<BlueprintProgression>("RavenerHunterWavesMysteryProgression", bp => {
                bp.SetName("Waves");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.m_Icon = OracleWavesMysteryFeature.m_Icon;
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
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, RavenerHunterWavesRevelationSelection),
                    Helpers.LevelEntry(8, RavenerHunterWavesRevelationSelection)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RavenerHunterWavesRevelationSelection, RavenerHunterWavesRevelationSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var RavenerHunterWindMysteryProgression = Helpers.CreateBlueprint<BlueprintProgression>("RavenerHunterWindMysteryProgression", bp => {
                bp.SetName("Wind");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.m_Icon = OracleWindMysteryFeature.m_Icon;
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
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, RavenerHunterWindRevelationSelection),
                    Helpers.LevelEntry(8, RavenerHunterWindRevelationSelection)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RavenerHunterWindRevelationSelection, RavenerHunterWindRevelationSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            var RavenerHunterChargedByNatureSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("RavenerHunterChargedByNatureSelection", bp => {
                bp.SetName("Charged by Nature");
                bp.SetDescription("Rather than having a deity patron, a ravener hunter is charged by the spirits of the Mwangi to eradicate evil wherever it appears. At 1st level, a " +
                    "ravener hunter chooses an oracle mystery from the following list: ancestor, battle, flame, heavens, life, nature, stone, waves, wind, or wood. She gains one revelation " +
                    "from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her effective oracle level to determine the revelation’s effects, " +
                    "and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her chosen mystery at 8th level.");
                bp.m_Icon = OracleMysterySelection.m_Icon;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Group = FeatureGroup.None;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    RavenerHunterAncestorsMysteryProgression.ToReference<BlueprintFeatureReference>(),
                    RavenerHunterBattleMysteryProgression.ToReference<BlueprintFeatureReference>(),
                    RavenerHunterFlameMysteryProgression.ToReference<BlueprintFeatureReference>(),
                    RavenerHunterLifeMysteryProgression.ToReference<BlueprintFeatureReference>(),
                    RavenerHunterNatureMysteryProgression.ToReference<BlueprintFeatureReference>(),
                    RavenerHunterStoneMysteryProgression.ToReference<BlueprintFeatureReference>(),
                    RavenerHunterWavesMysteryProgression.ToReference<BlueprintFeatureReference>(),
                    RavenerHunterWindMysteryProgression.ToReference<BlueprintFeatureReference>()
                };
            });

            var AreshkagalFeature = Resources.GetBlueprint<BlueprintFeature>("d714ecb5d5bb89a42957de0304e459c9");
            var BaphometFeature = Resources.GetBlueprint<BlueprintFeature>("bd72ca8ffcfec5745899ac56c93f12c5");
            var DeskariFeature = Resources.GetBlueprint<BlueprintFeature>("ddf913858bdf43b4da3b731e082fbcc0");
            var KabririFeature = Resources.GetBlueprint<BlueprintFeature>("f12c1ccc9d600c04f8887cd28a8f45a5");
            var LamashtuFeature = Resources.GetBlueprint<BlueprintFeature>("f86bc8fbf13221f4f9041608a1fb8585");
            var DemonHunterFeature = Resources.GetModBlueprint<BlueprintFeature>("DemonHunterFeature");
            var RavenerDemonHunterFeature = Helpers.CreateBlueprint<BlueprintFeature>("RavenerDemonHunterFeature", bp => {
                bp.SetName("Demon Hunter");
                bp.SetDescription("At 3rd level, a ravener hunter gains Demon Hunter as a bonus feat, ignoring its prerequisites. In addition she gains a +2 morale " +
                    "bonus on attack rolls and caster level checks to overcome spell resistance of creatures that she recognizes as followers of demonic deities." +
                    "\nDemon Hunter: You gain a +2 morale bonus on all attack rolls and a +2 morale bonus on caster level checks to penetrate spell resistance made against " +
                    "creatures with the demon subtype you recognize as demons.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DemonHunterFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<SpellPenetrationBonus>(c => {
                    c.Value = 2;
                    c.Descriptor = ModifierDescriptor.Morale;
                    c.CheckFact = true;
                    c.m_RequiredFact = AreshkagalFeature.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<SpellPenetrationBonus>(c => {
                    c.Value = 2;
                    c.Descriptor = ModifierDescriptor.Morale;
                    c.CheckFact = true;
                    c.m_RequiredFact = BaphometFeature.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<SpellPenetrationBonus>(c => {
                    c.Value = 2;
                    c.Descriptor = ModifierDescriptor.Morale;
                    c.CheckFact = true;
                    c.m_RequiredFact = DeskariFeature.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<SpellPenetrationBonus>(c => {
                    c.Value = 2;
                    c.Descriptor = ModifierDescriptor.Morale;
                    c.CheckFact = true;
                    c.m_RequiredFact = KabririFeature.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<SpellPenetrationBonus>(c => {
                    c.Value = 2;
                    c.Descriptor = ModifierDescriptor.Morale;
                    c.CheckFact = true;
                    c.m_RequiredFact = LamashtuFeature.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AttackBonusAgainstFactOwner>(c => {
                    c.AttackBonus = 2;
                    c.Descriptor = ModifierDescriptor.Morale;
                    c.Bonus = 0;
                    c.m_CheckedFact = AreshkagalFeature.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<AttackBonusAgainstFactOwner>(c => {
                    c.AttackBonus = 2;
                    c.Descriptor = ModifierDescriptor.Morale;
                    c.Bonus = 0;
                    c.m_CheckedFact = BaphometFeature.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<AttackBonusAgainstFactOwner>(c => {
                    c.AttackBonus = 2;
                    c.Descriptor = ModifierDescriptor.Morale;
                    c.Bonus = 0;
                    c.m_CheckedFact = DeskariFeature.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<AttackBonusAgainstFactOwner>(c => {
                    c.AttackBonus = 2;
                    c.Descriptor = ModifierDescriptor.Morale;
                    c.Bonus = 0;
                    c.m_CheckedFact = KabririFeature.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<AttackBonusAgainstFactOwner>(c => {
                    c.AttackBonus = 2;
                    c.Descriptor = ModifierDescriptor.Morale;
                    c.Bonus = 0;
                    c.m_CheckedFact = LamashtuFeature.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var SecondChargedByNatureSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("SecondChargedByNatureSelection", bp => {
                bp.SetName("Mythic Charged by Nature");
                bp.SetDescription("Mythic power allows you further access to the power of the spirits.\nYou select a second mystery from charged by nature, getting all its benefits.");
                bp.m_Icon = OracleMysterySelection.m_Icon;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Group = FeatureGroup.None;
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = RavenerHunterChargedByNatureSelection.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    RavenerHunterAncestorsMysteryProgression.ToReference<BlueprintFeatureReference>(),
                    RavenerHunterBattleMysteryProgression.ToReference<BlueprintFeatureReference>(),
                    RavenerHunterFlameMysteryProgression.ToReference<BlueprintFeatureReference>(),
                    RavenerHunterLifeMysteryProgression.ToReference<BlueprintFeatureReference>(),
                    RavenerHunterNatureMysteryProgression.ToReference<BlueprintFeatureReference>(),
                    RavenerHunterStoneMysteryProgression.ToReference<BlueprintFeatureReference>(),
                    RavenerHunterWavesMysteryProgression.ToReference<BlueprintFeatureReference>(),
                    RavenerHunterWindMysteryProgression.ToReference<BlueprintFeatureReference>()
                };
                bp.Mode = SelectionMode.OnlyNew;
            });
            SecondChargedByNatureSelection.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.CheckInProgression = false;
                c.HideInUI = false;
                c.m_Feature = SecondChargedByNatureSelection.ToReference<BlueprintFeatureReference>();
            });
            FeatTools.AddAsMythicAbility(SecondChargedByNatureSelection);



            #region Revelation scaling for Ravener Hunter
            ///AddFeatureOnClassLevel
            var RavenerHunterAddFeatureOnClassLevel = new BlueprintFeature[] { 
                AcidSkin,
                CinderDance,
                FluidNature,
                Resources.GetBlueprint<BlueprintFeature>("774a58477c052ec48896cd4cebd7437c"), //FormOfFlame1 Feature
                Resources.GetBlueprint<BlueprintFeature>("1d84f34bcb2e94840bab48256834aacc"), //FormOfFlame2 Feature
                Resources.GetBlueprint<BlueprintFeature>("71dc20d132bb06d41ab4d0f1e94fef46"), //FormOfFlame3 Feature
                FreezingSpells,
                IceSkin,
                Invisibility,
                MoltenSkin,
                SparkSkin,
                StoneStability,
                TouchOfAcid,
                TouchOfElectricity,
                TouchOfFlame,
                WarSight,
                WintryTouch,
                Resources.GetBlueprint<BlueprintFeature>("485bfaffc5d906241ac2fb4529175279"), //MM BullRush Feature
                Resources.GetBlueprint<BlueprintFeature>("42f46ce3ecf00e549ad8a0bd0ef84919"), //MM DirtyTrick Feature
                Resources.GetBlueprint<BlueprintFeature>("8142f89d36cc52243ad1cfd48ce8f655"), //MM Disarm Feature
                Resources.GetBlueprint<BlueprintFeature>("f6331dc792c40eb4ab99a4ad36eb7b41"), //MM Sunder Feature
                Resources.GetBlueprint<BlueprintFeature>("86f1c6ea242b6c74e81e370e4ffad4d1"), //MM Trip Feature
            };
            foreach (var feature in RavenerHunterAddFeatureOnClassLevel) {
                feature.GetComponents<AddFeatureOnClassLevel>().ForEach(c => {
                    c.m_AdditionalClasses = c.m_AdditionalClasses.AppendToArray(InquisitorClass.ToReference<BlueprintCharacterClassReference>());
                    c.m_Archetypes = c.m_Archetypes.AppendToArray(RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>());

                }) ;
            }
            ///BlueprintAbilityResource.MaxAmount
            var RavenerHunterAbilityResource = new BlueprintAbilityResource[] {
                Resources.GetBlueprint<BlueprintAbilityResource>("7f80bf1b62a94234493b4e6c3ee2e96d"), //AirBarrier Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("1d599c9e8701c8847b244564b97b036e"), //Battlecry Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("0e5f40b0cd3465243afc81fb86caff11"), //BattlefieldClarity Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("8eb43ae66258f5a408b1e780e0fbd1f1"), //BloodOfHeros Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("6467269767a2685419641102925b1849"), //CombatHealer Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("f4738aded0f73f940a0721bda26fc96b"), //ErosionTouch Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("77ca57a5c7405d541b8d609b6d1f0420"), //FireBreath Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("a4a7540ff16e06f4390eda7aba50216d"), //FormOfFlame Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("6651f65627aead44e9fd1503b678f78f"), //HeatAura Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("f8797170f46024f4791f1cf2b62f96d5"), //IceArmor Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("1d03dd8d343205c49ba6512ed5ef6666"), //Invisibility Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("783589ea314a1344b87c7d97685d1459"), //LifeLeech Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("5f624fa5d4cd4882b9368e4d123306bd"), //LifeLink Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("b02c0073333a42140a75e29bf811be17"), //LightningBreath Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("9f7633112027a0f4f8d282914f4900e0"), //MightyPebble Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("e579ad81e91912641a6693d1fd250910"), //PhantomTouch Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("26195c218cc16be499e75d82588394df"), //ShardExplosion Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("4e2643951e18e1d43868ba7451690657"), //SpiritShield Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("4e5515062208cf148b4abe799ff1042d"), //StormOfSouls Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("706dedfe783128a409dd13969d046142"), //ThunderBurst Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("92902d7d14cbc034ea323fd1937984b0"), //TouchOfAcid Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("0b2affefb91af2d4c904b30df8c6c5d0"), //TouchOfElectricity Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("07d5652a55d47d74d81d0ca6f3c72f21"), //TouchOfFire Resource
                Resources.GetBlueprint<BlueprintAbilityResource>("3dd307755f73abc418152f42c40edf6b") //WintryTouch Resource

            };
            foreach (var abilityresource in RavenerHunterAbilityResource) {
                abilityresource.m_MaxAmount.m_Class = abilityresource.m_MaxAmount.m_Class.AppendToArray(InquisitorClass.ToReference<BlueprintCharacterClassReference>() );
                abilityresource.m_MaxAmount.m_ClassDiv = abilityresource.m_MaxAmount.m_ClassDiv.AppendToArray(InquisitorClass.ToReference<BlueprintCharacterClassReference>() );
                abilityresource.m_MaxAmount.m_Archetypes = abilityresource.m_MaxAmount.m_Archetypes.AppendToArray(RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>() );
                abilityresource.m_MaxAmount.m_ArchetypesDiv = abilityresource.m_MaxAmount.m_ArchetypesDiv.AppendToArray(RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>() );
            }
            ///ContextRankConfig on ability
            var RavenerHunterContextRankConfigOnAbility = new BlueprintAbility[] {
                Resources.GetBlueprint<BlueprintAbility>("ed03a5fe17751964b99ad4295b7f8c7a"), //AirBarrier Ability
                Resources.GetBlueprint<BlueprintAbility>("b9eca127dd82f554fb2ccd804de86cf6"), //Channel Ability
                Resources.GetBlueprint<BlueprintAbility>("ab0635df6b4674e4e96809bd718cab89"), //ChannelHarm Ability
                Resources.GetBlueprint<BlueprintAbility>("f9fa310c8c0f8784e94b6ae265f7b921"), //ErosionTouch Ability
                Resources.GetBlueprint<BlueprintAbility>("1eea9d93d63897541a60cda0066601b8"), //FireBreath Ability
                Resources.GetBlueprint<BlueprintAbility>("abcd8a7c09a9f3d4ea9b8bdfe5c15c43"), //HeatAura Ability
                Resources.GetBlueprint<BlueprintAbility>("eef719a4d5af5d74193284e09b6bb596"), //IceArmor Ability
                Resources.GetBlueprint<BlueprintAbility>("4f22925d0df782c49b13d484b864250f"), //LifeLeech Ability
                Resources.GetBlueprint<BlueprintAbility>("291364b4b58e6044ea9bbe2d2b917052"), //LightningBreath Ability
                Resources.GetBlueprint<BlueprintAbility>("2ae123c190625644e889517e15e8f640"), //MightyPebble Ability
                Resources.GetBlueprint<BlueprintAbility>("7d72d34578a369a4f8d2515d04a96b71"), //PhantomTouch Ability
                Resources.GetBlueprint<BlueprintAbility>("378d4f5fe60457c49b5e110ff0f8e2cb"), //ShardExplosion Ability
                Resources.GetBlueprint<BlueprintAbility>("b363ff079a69b844e957b8295cc73eb0"), //SpiritShield Ability
                Resources.GetBlueprint<BlueprintAbility>("5195439b7006c004e865ff741a57406c"), //StormOfSouls Ability
                Resources.GetBlueprint<BlueprintAbility>("b8f49a69ecd09ae489411e4394d93e74"), //ThunderBurst Ability
                Resources.GetBlueprint<BlueprintAbility>("49ccae55165ead643ba583f9a6a23736"), //TouchOfAcid Ability
                Resources.GetBlueprint<BlueprintAbility>("c00330e4f80c9ad42b089cb6c612158f"), //TouchOfElectricity Ability
                Resources.GetBlueprint<BlueprintAbility>("b53791f139cd1d445bccf6677996c2c4"), //TouchOfFire Ability
                Resources.GetBlueprint<BlueprintAbility>("8f5b0cc7d3c20ce4fbfe796b56f56389") //WintryTouch Ability

            };
            foreach (var contextrankconfig in RavenerHunterContextRankConfigOnAbility) {
                contextrankconfig.GetComponents<ContextRankConfig>().ForEach(c => { c.m_Class = c.m_Class.AppendToArray(InquisitorClass.ToReference<BlueprintCharacterClassReference>()); });
                contextrankconfig.GetComponents<ContextRankConfig>().ForEach(c => { c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(); });
            }
            ///ContextRankConfig on buff
            var RavenerHunterContextRankConfigOnBuff = new BlueprintBuff[] {
                Resources.GetBlueprint<BlueprintBuff>("55705db41bf5c1f438486258efa636bd"), //Battlecry Buff
                Resources.GetBlueprint<BlueprintBuff>("ab88d55181c294a40aea8745f1fd6419"), //BloodOfHeros Buff
                Resources.GetBlueprint<BlueprintBuff>("4ae27ae7c3d758041b25e9a3aff73592"), //BurningMagic Buff
                Resources.GetBlueprint<BlueprintBuff>("6213362c096173d45b3d7521a220d8a0"), //IceArmor Buff
                Resources.GetBlueprint<BlueprintBuff>("d2f288936790a8249aaade7184984144"), //SpiritOfNature Buff
                Resources.GetBlueprint<BlueprintBuff>("60376ff2340b73346a555979d7763088"), //SpiritShield Buff
                Resources.GetBlueprint<BlueprintBuff>("c1228cf39a1dc1648bdbf345189efdee"), //MM BullRush Feature
                Resources.GetBlueprint<BlueprintBuff>("7b1acc94ff9b2564f9d6436b37424dea"), //MM DirtyTrick Feature
                Resources.GetBlueprint<BlueprintBuff>("2d8cb604b8945c746978e172bdb4afbf"), //MM Disarm Feature
                Resources.GetBlueprint<BlueprintBuff>("ec92ffb75498fd845abda587838365c0"), //MM Sunder Feature
                Resources.GetBlueprint<BlueprintBuff>("100139e153a394c4a8e30bbe99fb5794") //MM Trip Feature

            };
            foreach (var contextrankconfig in RavenerHunterContextRankConfigOnBuff) {
                contextrankconfig.GetComponents<ContextRankConfig>().ForEach(c => { c.m_Class = c.m_Class.AppendToArray(InquisitorClass.ToReference<BlueprintCharacterClassReference>()); });
                contextrankconfig.GetComponents<ContextRankConfig>().ForEach(c => { c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(); });
            }
            ///ContextRankConfig on feature
            var RavenerHunterContextRankConfigOnFeature = new BlueprintFeature[] {
                Resources.GetBlueprint<BlueprintFeature>("125294de0a922c34db4cd58ca7a200ac"), //BurningMagic Feature
                Resources.GetBlueprint<BlueprintFeature>("4fe07207483321e4cb7b81e2eaeb9cec"), //FreezingSpells1 Feature
                Resources.GetBlueprint<BlueprintFeature>("75dc65756f4c51c40932ac2ffdf66b94"), //FreezingSpells11 Feature
                Resources.GetBlueprint<BlueprintFeature>("8cf1bc6fe4d14304392496ff66023271"), //SpiritBoost Feature
                Resources.GetBlueprint<BlueprintFeature>("cd9791a958181544a8b71346ec0722db") //VortexSpells Feature

            };
            foreach (var contextrankconfig in RavenerHunterContextRankConfigOnFeature) {
                contextrankconfig.GetComponents<ContextRankConfig>().ForEach(c => { c.m_Class = c.m_Class.AppendToArray(InquisitorClass.ToReference<BlueprintCharacterClassReference>()); });
                contextrankconfig.GetComponents<ContextRankConfig>().ForEach(c => { c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(); });
            }            
            ///classlevels on progression
            var RavenerHunterRevelationProgressions = new BlueprintProgression[] {
                Resources.GetBlueprint<BlueprintProgression>("7d1c29c3101dd7643a625448fbbaa919"), //BoundedMount Progression
                Resources.GetBlueprint<BlueprintProgression>("9c8997f740d5a634fb19ac32e0517180"), //FormOfFlame Progression
                Resources.GetBlueprint<BlueprintProgression>("0a4c3556355747241b2d3dcc0a88ec10") //WeaponMastery Progression

            };
            foreach (var classlevels in RavenerHunterRevelationProgressions) {
                classlevels.m_Classes = classlevels.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
                classlevels.m_Archetypes = classlevels.m_Archetypes.AppendToArray(
                    new BlueprintProgression.ArchetypeWithLevel() {
                        m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    });
            }
            ///Friend to Animals
            var SummonNaturesAlly1 = Resources.GetBlueprintReference<BlueprintAbilityReference>("c6147854641924442a3bb736080cfeb6");
            var SummonNaturesAlly2 = Resources.GetBlueprintReference<BlueprintAbilityReference>("298148133cdc3fd42889b99c82711986");
            var SummonNaturesAlly3 = Resources.GetBlueprintReference<BlueprintAbilityReference>("fdcf7e57ec44f704591f11b45f4acf61");
            var SummonNaturesAlly4 = Resources.GetBlueprintReference<BlueprintAbilityReference>("c83db50513abdf74ca103651931fac4b");
            var SummonNaturesAlly5 = Resources.GetBlueprintReference<BlueprintAbilityReference>("8f98a22f35ca6684a983363d32e51bfe");
            var SummonNaturesAlly6 = Resources.GetBlueprintReference<BlueprintAbilityReference>("55bbce9b3e76d4a4a8c8e0698d29002c");
            var SummonNaturesAlly7 = Resources.GetBlueprintReference<BlueprintAbilityReference>("051b979e7d7f8ec41b9fa35d04746b33");
            var SummonNaturesAlly8 = Resources.GetBlueprintReference<BlueprintAbilityReference>("ea78c04f0bd13d049a1cce5daf8d83e0");
            var SummonNaturesAlly9 = Resources.GetBlueprintReference<BlueprintAbilityReference>("a7469ef84ba50ac4cbf3d145e3173f8e");
            FriendToAnimals.TemporaryContext(bp => {
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.SpellLevel = 1;
                    c.m_Spell = SummonNaturesAlly1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.SpellLevel = 2;
                    c.m_Spell = SummonNaturesAlly2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.SpellLevel = 3;
                    c.m_Spell = SummonNaturesAlly3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.SpellLevel = 4;
                    c.m_Spell = SummonNaturesAlly4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.SpellLevel = 5;
                    c.m_Spell = SummonNaturesAlly5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.SpellLevel = 6;
                    c.m_Spell = SummonNaturesAlly6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.SpellLevel = 7;
                    c.m_Spell = SummonNaturesAlly7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.SpellLevel = 8;
                    c.m_Spell = SummonNaturesAlly8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.SpellLevel = 9;
                    c.m_Spell = SummonNaturesAlly9;
                });

            });

            #endregion

            #region Revelation Mystery Prereqisites
            MysteryTools.ConfigureRavenerHunterRevelation(AcidSkin, RavenerHunterStoneMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(AirBarrier, RavenerHunterWindMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(Battlecry, RavenerHunterBattleMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(BattlefieldClarity, RavenerHunterBattleMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(BloodOfHeros, RavenerHunterAncestorsMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(BondedMount, RavenerHunterNatureMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(BurningMagic, RavenerHunterFlameMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(Channel, RavenerHunterLifeMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(CinderDance, RavenerHunterFlameMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(ClobberingStrike, RavenerHunterStoneMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(CombatHealer, RavenerHunterBattleMysteryProgression, 7);
            MysteryTools.ConfigureRavenerHunterRevelation(CombatHealer, RavenerHunterLifeMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(EnhancedCures, RavenerHunterLifeMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(ErosionTouch, RavenerHunterNatureMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(FireBreath, RavenerHunterFlameMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(FluidNature, RavenerHunterWavesMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(FormOfFlame, RavenerHunterFlameMysteryProgression, 7);
            MysteryTools.ConfigureRavenerHunterRevelation(FreezingSpells, RavenerHunterWavesMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(FriendToAnimals, RavenerHunterNatureMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(HeatAura, RavenerHunterFlameMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(IceArmor, RavenerHunterWavesMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(IceSkin, RavenerHunterWavesMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(Invisibility, RavenerHunterWindMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(LifeLeach, RavenerHunterNatureMysteryProgression, 7);
            MysteryTools.ConfigureRavenerHunterRevelation(LifeLink, RavenerHunterLifeMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(LightningBreath, RavenerHunterWindMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(ManeuverMastery, RavenerHunterBattleMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(MightyPebble, RavenerHunterStoneMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(MoltenSkin, RavenerHunterFlameMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(NatureWhispers, RavenerHunterNatureMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(PhantomTouch, RavenerHunterAncestorsMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(PunitiveTransformation, RavenerHunterWavesMysteryProgression, 7);
            MysteryTools.ConfigureRavenerHunterRevelation(SacredCouncil, RavenerHunterAncestorsMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(SafeCuring, RavenerHunterLifeMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(ShardExplosion, RavenerHunterStoneMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(SkillAtArms, RavenerHunterBattleMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(SparkSkin, RavenerHunterWindMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(SpiritBoost, RavenerHunterLifeMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(SpiritOfNature, RavenerHunterNatureMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(SpiritShield, RavenerHunterAncestorsMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(StoneStability, RavenerHunterStoneMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(StormOfSouls, RavenerHunterAncestorsMysteryProgression, 7);
            MysteryTools.ConfigureRavenerHunterRevelation(Thunderburst, RavenerHunterWindMysteryProgression, 7);
            MysteryTools.ConfigureRavenerHunterRevelation(TouchOfAcid, RavenerHunterStoneMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(TouchOfElectricity, RavenerHunterWindMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(TouchOfFlame, RavenerHunterFlameMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(VortexSpells, RavenerHunterWindMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(WarSight, RavenerHunterBattleMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(WeaponMastery, RavenerHunterBattleMysteryProgression);
            MysteryTools.ConfigureRavenerHunterRevelation(WintryTouch, RavenerHunterWavesMysteryProgression);
            #endregion



            RavenerHunterArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, DomainSelectionFeature),
                    Helpers.LevelEntry(3, SoloTacticsFeature,TeamWorkFeat)
            };
            RavenerHunterArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, RavenerHunterHolyMagicFeature, RavenerHunterChargedByNatureSelection),
                    Helpers.LevelEntry(3, RavenerDemonHunterFeature),
                    Helpers.LevelEntry(6, SoloTacticsFeature)

            };
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Ravener Hunter")) { return; }
            InquisitorClass.m_Archetypes = InquisitorClass.m_Archetypes.AppendToArray(RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>());

        }
    }
}
