using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Designers.TempMapCode.Ambush;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Visual.HitSystem;
using Kingmaker.Visual.Sound;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Spells {
    internal class WoodenPhalanx {
        public static void AddWoodenPhalanx() {

            var WoodenPhalanxIcon = AssetLoader.LoadInternal("Skills", "Icon_WoodenPhalanx.jpg");
            var Icon_ScrollOfWoodenPhalanx = AssetLoader.LoadInternal("Items", "Icon_ScrollOfWoodenPhalanx.png");
            var SuperiorSummoning = Resources.GetBlueprint<BlueprintFeature>("0477936c0f74841498b5c8753a8062a3");
            var BloodlineAbyssalSummoning = Resources.GetBlueprint<BlueprintFeature>("de24d9e57d7bad24dbada7389eebcd65");
            var DRAdamantine5 = Resources.GetBlueprint<BlueprintFeature>("fb88b018013dc8e419150f86540c07f2");
            var GolemWoodImmunity = Resources.GetBlueprint<BlueprintFeature>("be8dcb83f4bd3e24185ceb0cea084a06");
            var WoodGolemSplintering = Resources.GetBlueprint<BlueprintAbility>("eba7737aef48d304fb6788d748a2df69");
            var GolumAutumnVisual = Resources.GetBlueprint<BlueprintBuff>("40aaecaebce743e48a1d35957583fcc6");
            var SummonedCreatureSpawnAllyVII_IX = Resources.GetBlueprint<BlueprintBuff>("932d27490e1701548a48b4cbc2f2caac");
            var NaturalArmor8 = Resources.GetBlueprint<BlueprintUnitFact>("b9342e2a6dc5165489ba3412c50ca3d1");
            var ConstructClass = Resources.GetBlueprint<BlueprintCharacterClass>("fd66bdea5c33e5f458e929022322e6bf");
            var Slam2d6 = Resources.GetBlueprint<BlueprintItemWeapon>("c6d3cd958772be148952c011b3a15452");
            var GolumWoodSummon = Resources.GetBlueprint<BlueprintUnit>("38db08516a706cf448327c5b81c22c79");
            var Unlootable = Resources.GetBlueprintReference<BlueprintBuffReference>("0f775c7d5d8b6494197e1ce937754482");
            var Summoned = Resources.GetBlueprint<BlueprintFaction>("1b08d9ed04518ec46a9b3e4e23cb5105");
            var StoneGolumBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("34ca66289f4899347a7ab9bef34a13f0");
            var StoneGolemPortait = Resources.GetBlueprint<BlueprintPortrait>("2a1e6b8c31e245f7899825149ce4d113");

            var WoodenPhalanxSummonPool = Helpers.CreateBlueprint<BlueprintSummonPool>("WoodenPhalanxSummonPool", bp => {
                bp.Limit = 0;
                bp.DoNotRemoveDeadUnits = false;
            });

            var AdvancedGolumWoodSummon = Helpers.CreateBlueprint<BlueprintUnit>("AdvancedGolumWoodSummon", bp => {
                bp.SetLocalisedName("Advanced Wood Golum");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = ConstructClass.ToReference<BlueprintCharacterClassReference>();
                    c.Levels = 8;
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Unknown;
                    c.Skills = new StatType[0];
                    c.Selections = new SelectionEntry[0];
                    c.DoNotApplyAutomatically = false;
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = Unlootable;
                });
                
                bp.Gender = Gender.Male;
                bp.Size = Size.Medium;
                bp.Color = GolumWoodSummon.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = StoneGolemPortait.ToReference<BlueprintPortraitReference>();
                bp.Prefab = GolumWoodSummon.Prefab;
                bp.Visual = new UnitVisualParams() {
                    BloodType = BloodType.Common,
                    FootprintType = FootprintType.AnimalPaw,
                    FootprintScale = 1,
                    ArmorFx = new PrefabLink(),
                    BloodPuddleFx = new PrefabLink(),
                    DismemberFx = new PrefabLink(),
                    RipLimbsApartFx = new PrefabLink(),
                    IsNotUseDismember = false,
                    m_Barks = StoneGolumBarks.ToReference<BlueprintUnitAsksListReference>(),
                    ReachFXThresholdBonus = 0,
                    DefaultArmorSoundType = ArmorSoundType.Flesh,
                    FootstepSoundSizeType = FootstepSoundSizeType.BootMedium,
                    FootSoundType = FootSoundType.HardPaw,
                    FootSoundSize = Size.Medium,
                    BodySoundType = BodySoundType.Flesh,
                    BodySoundSize = Size.Medium,
                    FoleySoundPrefix = null, //?
                    NoFinishingBlow = false,
                    ImportanceOverride = 0,
                    SilentCaster = true
                };
                bp.m_Faction = Summoned.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = GolumWoodSummon.FactionOverrides;
                bp.m_Brain = GolumWoodSummon.m_Brain;
                bp.Body = new BlueprintUnit.UnitBody() {
                    DisableHands = true,
                    m_EmptyHandWeapon = new BlueprintItemWeaponReference(),
                    m_PrimaryHand = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHand = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative3 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative3 = new BlueprintItemEquipmentHandReference(),
                    ActiveHandSet = 0,
                    m_AdditionalLimbs = new BlueprintItemWeaponReference[] {
                        Slam2d6.ToReference<BlueprintItemWeaponReference>(),
                        Slam2d6.ToReference<BlueprintItemWeaponReference>()
                    }
                };
                bp.Strength = 22;
                bp.Dexterity = 21;
                bp.Constitution = 14;
                bp.Wisdom = 21;
                bp.Intelligence = 0;
                bp.Charisma = 5;
                bp.Speed = new Feet(30);
                bp.Skills = new BlueprintUnit.UnitSkills() {
                    Acrobatics = 0,
                    Physique = 0,
                    Diplomacy = 0,
                    Thievery = 0,
                    LoreNature = 0,
                    Perception = 0,
                    Stealth = 0,
                    UseMagicDevice = 0,
                    LoreReligion = 0,
                    KnowledgeWorld = 0,
                    KnowledgeArcana = 0,
                };
                bp.MaxHP = 0;
                bp.m_AddFacts = new BlueprintUnitFactReference[] {
                    NaturalArmor8.ToReference<BlueprintUnitFactReference>(),
                    DRAdamantine5.ToReference<BlueprintUnitFactReference>(),
                    GolemWoodImmunity.ToReference<BlueprintUnitFactReference>(),
                    WoodGolemSplintering.ToReference<BlueprintUnitFactReference>(),
                    GolumAutumnVisual.ToReference<BlueprintUnitFactReference>()
                };
            });

            var WoodenPhalanxAbility = Helpers.CreateBlueprint<BlueprintAbility>("WoodenPhalanxAbility", bp => {
                bp.SetName("Wooden Phalanx");
                bp.SetDescription("You create 1d4+2 wood golems with the advanced template. The golems willingly aid you in combat or battle, perform a specific mission, " +
                    "or serve as bodyguards. You can only have one wooden phalanx spell in effect at one time. If you cast this spell while another casting is still in " +
                    "effect, the previous casting is dispelled.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionClearSummonPool() {
                            m_SummonPool = WoodenPhalanxSummonPool.ToReference<BlueprintSummonPoolReference>()
                        },
                        new ContextActionSpawnMonster() {
                            m_Blueprint = AdvancedGolumWoodSummon.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyVII_IX.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    IsNotDispelable = true
                                }
                                ),
                            m_SummonPool = WoodenPhalanxSummonPool.ToReference<BlueprintSummonPoolReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default
                                },
                                m_IsExtendable = true
                            },
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.D4,
                                DiceCountValue = 1,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.ProjectilesCount
                                }
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = true
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = SuperiorSummoning.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        BloodlineAbyssalSummoning.ToReference<BlueprintFeatureReference>(),
                        SuperiorSummoning.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 0, ProgressionValue = 2 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 1, ProgressionValue = 3 },
                        new ContextRankConfig.CustomProgressionItem(){ BaseValue = 2, ProgressionValue = 4 }
                    };
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Summoning;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Conjuration;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Summon_Polymorph;
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                });
                bp.m_Icon = WoodenPhalanxIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Reach | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("WoodenPhalanxAbility.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WoodenPhalanxScroll = ItemTools.CreateScroll("ScrollOfWoodenPhalanx", Icon_ScrollOfWoodenPhalanx, WoodenPhalanxAbility, 9, 17);
            VenderTools.AddScrollToLeveledVenders(WoodenPhalanxScroll);
            WoodenPhalanxAbility.AddToSpellList(SpellTools.SpellList.ClericSpellList, 9);
            WoodenPhalanxAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 9);
        }
    }
}
