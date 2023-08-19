using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Craft;
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

namespace ExpandedContent.Tweaks.Spells {
    internal class Shambler {
        public static void AddShambler() {

            var ShamblerIcon = AssetLoader.LoadInternal("Skills", "Icon_Shambler.jpg");
            var Icon_ScrollOfShambler = AssetLoader.LoadInternal("Items", "Icon_ScrollOfShambler.png");
            var SuperiorSummoning = Resources.GetBlueprint<BlueprintFeature>("0477936c0f74841498b5c8753a8062a3");
            var BloodlineAbyssalSummoning = Resources.GetBlueprint<BlueprintFeature>("de24d9e57d7bad24dbada7389eebcd65");
            var FireResistance10 = Resources.GetBlueprint<BlueprintFeature>("24700a71dd3dc844ea585345f6dd18f6");
            var ElectricityImmunity = Resources.GetBlueprint<BlueprintFeature>("cd1e5ab641a833c49994aff99db98952");
            var ShamblingMoundGrabFeature = Resources.GetBlueprint<BlueprintFeature>("efc1e80fb41e06544be46604983806d6");
            var SummonedCreatureSpawnAllyVII_IX = Resources.GetBlueprint<BlueprintBuff>("932d27490e1701548a48b4cbc2f2caac");
            var NaturalArmor12 = Resources.GetBlueprint<BlueprintUnitFact>("0b2d92c6aac8093489dfdadf1e448280");
            var PlantClass = Resources.GetBlueprint<BlueprintCharacterClass>("9393cc36ea29d084bab7433e3a28d40b");
            var SlamGargantuan2d6 = Resources.GetBlueprint<BlueprintItemWeapon>("27eee74857c42db499b3a6b20cfa6211");
            var GolumSummoned = Resources.GetBlueprint<BlueprintUnit>("38db08516a706cf448327c5b81c22c79");
            var DLC3_CR6_ShamblingMound = Resources.GetBlueprint<BlueprintUnit>("3ba7ed5832a44bae8a549c00455d8bde");
            var ShamblingMoundType = Resources.GetBlueprintReference<BlueprintUnitTypeReference>("e6d913acd6a4efd49a35c2381e9ed2df");
            var Unlootable = Resources.GetBlueprintReference<BlueprintBuffReference>("0f775c7d5d8b6494197e1ce937754482");
            var Summoned = Resources.GetBlueprint<BlueprintFaction>("1b08d9ed04518ec46a9b3e4e23cb5105");
            var ShamblingMoundBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("2b16730449d17104fa90b38ac310a547");
            var ShamblingMoundPortait = Resources.GetBlueprint<BlueprintPortrait>("7d5acff44886482eb193df0505b42de2");
            var DumbMonsterBrain = Resources.GetBlueprintReference<BlueprintBrainReference>("5abc8884c6f15204c8604cb01a2efbab");

            var BasicFeatSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("247a4068296e8be42890143f451b4b45");
            var PowerAttackFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("9972f33f977fc724c838e59641b2fca5");
            var CleaveFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("d809b6c4ff2aaff4fa70d712a70f7d7b");
            var CleavingFinishFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("59bd93899149fa44687ff4121389b3a9");
            var LightningReflexes = Resources.GetBlueprintReference<BlueprintFeatureReference>("15e7da6645a7f3d41bdad7c8c4b9de1e");
            var ToughnessFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("d09b20029e9abfe4480b356c92095623");
            var WeaponFocusParaFeat = Resources.GetBlueprint<BlueprintParametrizedFeature>("1e1f627d26ad36f43bbd26cc2bf8ac7e");




            var ShamblerSummonPool = Helpers.CreateBlueprint<BlueprintSummonPool>("ShamblerSummonPool", bp => {
                bp.Limit = 0;
                bp.DoNotRemoveDeadUnits = false;
            });

            var AdvancedShamblingMoundSummon = Helpers.CreateBlueprint<BlueprintUnit>("AdvancedShamblingMoundSummon", bp => {
                bp.SetLocalisedName("Advanced Shambling Mound");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = PlantClass.ToReference<BlueprintCharacterClassReference>();
                    c.Levels = 9;
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Unknown;
                    c.Skills = new StatType[] { StatType.SkillPerception | StatType.SkillStealth };
                    c.Selections = new SelectionEntry[] {
                        new SelectionEntry() {
                            IsParametrizedFeature = false,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                PowerAttackFeature,
                                CleaveFeature,
                                CleavingFinishFeature,
                                LightningReflexes,
                                ToughnessFeature,
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = null,
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.UnarmedStrike,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        },
                        new SelectionEntry() {
                            IsParametrizedFeature = true,
                            IsFeatureSelectMythicSpellbook = false,
                            m_Selection = BasicFeatSelection.ToReference<BlueprintFeatureSelectionReference>(),
                            m_Features = new BlueprintFeatureReference[] {
                                WeaponFocusParaFeat.ToReference<BlueprintFeatureReference>()
                            },
                            m_ParametrizedFeature = WeaponFocusParaFeat.ToReference<BlueprintParametrizedFeatureReference>(),
                            m_ParamObject = null,
                            ParamSpellSchool = SpellSchool.None,
                            ParamWeaponCategory = WeaponCategory.OtherNaturalWeapons,
                            Stat = StatType.Unknown,
                            m_FeatureSelectMythicSpellbook = null,
                            m_Spellbook = null
                        }
                    }; 
                    c.DoNotApplyAutomatically = false;
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = Unlootable;
                });
                bp.AddComponent<ChangeImpatience>(c => {
                    c.Delta = -1;
                });
                bp.m_Type = ShamblingMoundType;
                bp.Gender = Gender.Male;
                bp.Size = Size.Large;
                bp.Color = DLC3_CR6_ShamblingMound.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = ShamblingMoundPortait.ToReference<BlueprintPortraitReference>();
                bp.Prefab = DLC3_CR6_ShamblingMound.Prefab;
                bp.Visual = new UnitVisualParams() {
                    BloodType = BloodType.Common,
                    FootprintType = FootprintType.AnimalPaw,
                    FootprintScale = 1,
                    ArmorFx = new PrefabLink(),
                    BloodPuddleFx = new PrefabLink(),
                    DismemberFx = new PrefabLink(),
                    RipLimbsApartFx = new PrefabLink(),
                    IsNotUseDismember = false,
                    m_Barks = ShamblingMoundBarks.ToReference<BlueprintUnitAsksListReference>(),
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
                bp.FactionOverrides = GolumSummoned.FactionOverrides;
                bp.m_Brain = DumbMonsterBrain;
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
                        SlamGargantuan2d6.ToReference<BlueprintItemWeaponReference>(),
                        SlamGargantuan2d6.ToReference<BlueprintItemWeaponReference>()
                    }
                };
                bp.Strength = 25;
                bp.Dexterity = 14;
                bp.Constitution = 21;
                bp.Wisdom = 14;
                bp.Intelligence = 11;
                bp.Charisma = 13;
                bp.Speed = new Feet(20);
                bp.Skills = new BlueprintUnit.UnitSkills() {
                    Acrobatics = 0,
                    Physique = 0,
                    Diplomacy = 0,
                    Thievery = 0,
                    LoreNature = 0,
                    Perception = 15,
                    Stealth = 12,
                    UseMagicDevice = 0,
                    LoreReligion = 0,
                    KnowledgeWorld = 0,
                    KnowledgeArcana = 0,
                };
                bp.MaxHP = 0;
                bp.m_AddFacts = new BlueprintUnitFactReference[] {
                    NaturalArmor12.ToReference<BlueprintUnitFactReference>(),
                    FireResistance10.ToReference<BlueprintUnitFactReference>(),
                    ElectricityImmunity.ToReference<BlueprintUnitFactReference>(),
                    ShamblingMoundGrabFeature.ToReference<BlueprintUnitFactReference>()
                };
            });

            var ShamblerAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShamblerAbility", bp => {
                bp.SetName("Shambler");
                bp.SetDescription("The shambler spell creates 1d4+2 shambling mounds with the advanced template. The creatures willingly aid you in combat or battle, " +
                    "perform a specific mission, or serve as bodyguards. The creatures remain with you for 7 days. You can only have one shambler spell in effect " +
                    "at one time. If you cast this spell while another casting is still in effect, the previous casting is dispelled.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionClearSummonPool() {
                            m_SummonPool = ShamblerSummonPool.ToReference<BlueprintSummonPoolReference>()
                        },
                        new ContextActionSpawnMonster() {
                            m_Blueprint = AdvancedShamblingMoundSummon.ToReference<BlueprintUnitReference>(),
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
                            m_SummonPool = ShamblerSummonPool.ToReference<BlueprintSummonPoolReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Days,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 7,
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
                bp.m_Icon = ShamblerIcon;
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
                bp.LocalizedDuration = Helpers.CreateString("ShamblerAbility.Duration", "7 days");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShamblerScroll = ItemTools.CreateScroll("ScrollOfShambler", Icon_ScrollOfShambler, ShamblerAbility, 9, 17);
            VenderTools.AddScrollToLeveledVenders(ShamblerScroll);
            ShamblerAbility.AddToSpellList(SpellTools.SpellList.DruidSpellList, 9);
            ShamblerAbility.AddToSpellList(SpellTools.SpellList.ShamanSpelllist, 9);
        }
    }
}
