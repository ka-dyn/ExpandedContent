using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.ResourceLinks;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.TempMapCode.Ambush;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Utility;
using Kingmaker.Visual.HitSystem;
using Kingmaker.Visual.Sound;
using static TabletopTweaks.Core.MechanicsChanges.AdditionalModifierDescriptors;
using Kingmaker.AI.Blueprints;

namespace ExpandedContent.Tweaks.Spells {
    internal class ShadowJaunt {
        public static void AddShadowJaunt() {//Untested

            var ShadowJauntIcon = AssetLoader.LoadInternal("Skills", "Icon_ShadowJaunt.jpg");
            var Icon_ScrollOfShadowJaunt = AssetLoader.LoadInternal("Items", "Icon_ScrollOfShadowJaunt.png");

            var MountedBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("b2d13e8f3bb0f1d4c891d71b4d983cf7");
            var ShadowProjectile = Resources.GetBlueprintReference<BlueprintProjectileReference>("f8daba62ae5f454aae7bcd280d924e74");
            var NormalGameShadow = Resources.GetBlueprint<BlueprintUnit>("13e32ff7f7a54b6b95cb36366bf5dc00");


            var Summoned = Resources.GetBlueprint<BlueprintFaction>("1b08d9ed04518ec46a9b3e4e23cb5105");
            var Unlootable = Resources.GetBlueprintReference<BlueprintBuffReference>("0f775c7d5d8b6494197e1ce937754482");
            var ShadowFX = Resources.GetBlueprintReference<BlueprintBuffReference>("8caafe9dc0ff21041b36ad225569d164");
            var SubtypeExtraplanar = Resources.GetBlueprint<BlueprintFeature>("136fa0343d5b4b348bdaa05d83408db3");
            var TripImmune = Resources.GetBlueprint<BlueprintFeature>("c1b26f97b974aec469613f968439e7bb");
            var CantMove = Resources.GetBlueprint<BlueprintBuff>("fcdb9249b94bd924aae64d2de58e21e3");
            var CantAct = Resources.GetBlueprint<BlueprintBuff>("178ca49f3e694f399d267d44a3500577");
            var DumbMosterBrain = Resources.GetBlueprintReference<BlueprintBrainReference>("5abc8884c6f15204c8604cb01a2efbab");
            var SummonedCreatureSpawnAllyIV_VI = Resources.GetBlueprint<BlueprintBuff>("25629d7e78016a340b0e50818b6d8bb5");
            var SummonMonsterPool = Resources.GetBlueprint<BlueprintSummonPool>("d94c93e7240f10e41ae41db4c83d1cbe");

            var HeadLocatorFeature = Resources.GetBlueprint<BlueprintFeature>("9c57e9674b4a4a2b9920f9fec47f7e6a");
            var UndeadClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("19a2d9e58d916d04db4cd7ad2c7a3ee2");
            var ShadowJauntDecoyUnit = Helpers.CreateBlueprint<BlueprintUnit>("ShadowJauntDecoyUnit", bp => {
                bp.SetLocalisedName("Shadow Decoy");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = UndeadClass;
                    c.Levels = 1;
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Unknown;
                    c.Skills = new StatType[] { StatType.SkillStealth };
                    // Do I need Selections???
                    c.DoNotApplyAutomatically = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        HeadLocatorFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = Unlootable;
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = ShadowFX;
                });

                bp.Gender = Gender.Male;
                bp.Size = Size.Medium;
                bp.Color = NormalGameShadow.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = NormalGameShadow.m_Portrait;
                bp.Prefab = NormalGameShadow.Prefab;
                bp.Visual = NormalGameShadow.Visual;
                bp.m_Faction = Summoned.ToReference<BlueprintFactionReference>();
                bp.FactionOverrides = NormalGameShadow.FactionOverrides;
                bp.m_Brain = DumbMosterBrain;
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
                    m_AdditionalLimbs = new BlueprintItemWeaponReference[] { }
                };
                bp.Strength = 1;
                bp.Dexterity = 1;
                bp.Constitution = 1;
                bp.Wisdom = 1;
                bp.Intelligence = 1;
                bp.Charisma = 1;
                bp.Speed = new Feet(0);
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
                    CantAct.ToReference<BlueprintUnitFactReference>(),
                    CantMove.ToReference<BlueprintUnitFactReference>(),
                    SubtypeExtraplanar.ToReference<BlueprintUnitFactReference>(),
                    TripImmune.ToReference<BlueprintUnitFactReference>()
                };
            });

            var ShadowJauntBuff = Helpers.CreateBuff("ShadowJauntBuff", bp => {
                bp.SetName("Shadow Jaunt");
                bp.SetDescription("You instantly travel between shadows to a point within range. You leave a shadowy decoy in your former location and are wrapped in shadow at your destination; " +
                    "you attempt a Stealth check as a free action to hide in your new location. In addition, for 1 round, the envelope of shadow around you grants you concealment (20% miss chance). " +
                    "\nThe shadow decoy is fragile and will not survive more than a single attack.");
                bp.m_Icon = ShadowJauntIcon;
                bp.AddComponent<AddConcealment>(c => {
                    c.Descriptor = ConcealmentDescriptor.Blur;
                    c.Concealment = Concealment.Partial;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.CheckDistance = false;
                    c.DistanceGreater = 0.Feet();
                    c.OnlyForAttacks = false;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList( new ContextActionHideInPlainSight() );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
            });

            var ShadowJauntAbility = Helpers.CreateBlueprint<BlueprintAbility>("ShadowJauntAbility", bp => {
                bp.SetName("Shadow Jaunt");
                bp.SetDescription("You instantly travel between shadows to a point within range. You leave a shadowy decoy in your former location and are wrapped in shadow at your destination; " +
                    "you attempt a Stealth check as a free action to hide in your new location. In addition, for 1 round, the envelope of shadow around you grants you concealment (20% miss chance). " +
                    "\nThe shadow decoy is fragile and will not survive more than a single attack.");
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            ToCaster = true,
                            m_Buff = MountedBuff
                        },
                        new ContextActionSpawnMonster() {
                            m_Blueprint = ShadowJauntDecoyUnit.ToReference<BlueprintUnitReference>(),
                            AfterSpawn = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedCreatureSpawnAllyIV_VI.ToReference<BlueprintBuffReference>(),
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
                            m_SummonPool = SummonMonsterPool.ToReference<BlueprintSummonPoolReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default
                                },
                                m_IsExtendable = true
                            },
                            CountValue = new ContextDiceValue() {
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            LevelValue = 0,
                            DoNotLinkToCaster = false,
                            IsDirectlyControllable = false
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = ShadowJauntBuff.ToReference<BlueprintBuffReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            ToCaster = true
                        }
                        );
                });
                bp.AddComponent<AbilityCustomTeleportation>(c => {
                    c.m_Projectile = ShadowProjectile;
                    c.DisappearFx = new PrefabLink() { AssetId = "f1f41fef03cb5734e95db1342f0c605e" };
                    c.DisappearDuration = 0.25f;
                    c.AppearFx = new PrefabLink();
                    c.AppearDuration = 0;
                    c.AlongPath = false;
                    c.AlongPathDistanceMuliplier = 1;

                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Illusion;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = ShadowJauntIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.CompletelyNormal | Metamagic.Quicken| Metamagic.Reach | Metamagic.Heighten;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ShadowJauntScroll = ItemTools.CreateScroll("ScrollOfShadowJaunt", Icon_ScrollOfShadowJaunt, ShadowJauntAbility, 4, 7);
            VenderTools.AddScrollToLeveledVenders(ShadowJauntScroll);
            ShadowJauntAbility.AddToSpellList(SpellTools.SpellList.BardSpellList, 4);
            ShadowJauntAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 4);

        }
    }
}
