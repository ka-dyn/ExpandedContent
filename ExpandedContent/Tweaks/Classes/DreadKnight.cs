using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.Enums;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System.Collections.Generic;
using System.Linq;
using TabletopTweaks.Core.NewComponents;

namespace ExpandedContent.Tweaks.Classes {

    internal class DreadKnightClass {
        public static void AddDreadKnightClass() {

            var WarpriestSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("7d7d51be2948d2544b3c2e1596fd7603");
            var AnimalClass = Resources.GetBlueprint<BlueprintCharacterClass>("4cd1757a0eea7694ba5c933729a53920");           
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            // Spellbook
            var BloodragerSpellLevels = Resources.GetBlueprint<BlueprintSpellsTable>("d9e9437865e83344b864ef49ffa53013");
            var BloodragerClass = Resources.GetBlueprint<BlueprintCharacterClass>("d77e67a814d686842802c9cfd8ef8499");
            var PaladinSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("bce4989b070ce924b986bf346f59e885");

            var WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
            var DreadKnightSpellLevels = Helpers.CreateBlueprint<BlueprintSpellsTable>("DreadKnightSpellLevels", bp => {
                bp.Levels = BloodragerSpellLevels.Levels.Select(level => SpellTools.CreateSpellLevelEntry(level.Count)).ToArray();
            });
            var DreadKnightSpellbook = Helpers.CreateBlueprint<BlueprintSpellbook>("DreadKnightSpellbook", bp => {
                bp.Name = Helpers.CreateString("$DreadKnightSpellbook.Name", "Dread Knight");
                bp.CastingAttribute = BloodragerClass.Spellbook.CastingAttribute;
                bp.AllSpellsKnown = BloodragerClass.Spellbook.AllSpellsKnown;
                bp.CantripsType = BloodragerClass.Spellbook.CantripsType;
                bp.HasSpecialSpellList = BloodragerClass.Spellbook.HasSpecialSpellList;
                bp.SpecialSpellListName = BloodragerClass.Spellbook.SpecialSpellListName;
                bp.m_SpellsPerDay = DreadKnightSpellLevels.ToReference<BlueprintSpellsTableReference>();
                bp.m_SpellsKnown = BloodragerClass.Spellbook.m_SpellsKnown;
                bp.m_SpellSlots = BloodragerClass.Spellbook.m_SpellSlots;
                bp.m_SpellList = WarpriestClass.Spellbook.m_SpellList;
                bp.m_MythicSpellList = BloodragerClass.Spellbook.m_MythicSpellList;
                bp.m_Overrides = BloodragerClass.Spellbook.m_Overrides;
                bp.IsArcane = false;
                bp.Spontaneous = true;
                SpellTools.Spellbook.AllSpellbooks.Add(bp);
            });
            // Progression
            var DreadKnightProgression = Helpers.CreateBlueprint<BlueprintProgression>("DreadKnightProgression", bp => {
                bp.SetName("Dread Knight");
                bp.SetDescription("Dread Knights are villains at their most dangerous. " +
                        "They care nothing for the lives of others and actively seek to bring death and destruction to ordered society. They rarely travel with " +
                        "those that they do not subjugate, unless as part of a ruse to bring ruin from within.");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            // Main Class
            var DreadKnightClass = Helpers.CreateBlueprint<BlueprintCharacterClass>("DreadKnightClass", bp => {
                bp.LocalizedName = Helpers.CreateString($"DreadKnightClass.Name", "Dread Knight");
                bp.LocalizedDescription = Helpers.CreateString($"DreadKnightClass.Description", "Although it is a rare occurrence, paladins do " +
                    "sometimes stray from the path of righteousness. Most of these wayward holy warriors seek out redemption and forgiveness for their " +
                    "misdeeds, regaining their powers through piety, charity, and powerful magic. Yet there are others, the dark and disturbed few, " +
                    "who turn actively to evil, courting the dark powers they once railed against in order to take vengeance on their former brothers. " +
                    "It’s said that those who climb the farthest have the farthest to fall, and tyrants are living proof of this fact, their pride and " +
                    "hatred blinding them to the glory of their forsaken patrons. " +
                    "\nDread Knights become the antithesis of their former selves. They make pacts with fiends, take the lives of the innocent, " +
                    "and put nothing ahead of their personal power and wealth. Champions of evil, they often lead armies of evil creatures and " +
                    "work with other villains to bring ruin to the holy and tyranny to the weak. Not surprisingly, paladins stop at nothing to " +
                    "put an end to such nefarious antiheroes.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"DreadKnightClass.Description", "Dread Knights are villains at their most dangerous. " +
                    "They care nothing for the lives of others and actively seek to bring death and destruction to ordered society. They rarely travel with " +
                    "those that they do not subjugate, unless as part of a ruse to bring ruin from within.");
                bp.HitDie = Kingmaker.RuleSystem.DiceType.D10;
                bp.m_BaseAttackBonus = PaladinClass.m_BaseAttackBonus;
                bp.m_FortitudeSave = PaladinClass.m_FortitudeSave;
                bp.m_PrototypeId = PaladinClass.m_PrototypeId;
                bp.m_ReflexSave = PaladinClass.m_ReflexSave;
                bp.m_WillSave = PaladinClass.m_WillSave;
                bp.m_Progression = DreadKnightProgression.ToReference<BlueprintProgressionReference>();
                bp.m_Difficulty = PaladinClass.Difficulty;
                bp.RecommendedAttributes = PaladinClass.RecommendedAttributes;
                bp.NotRecommendedAttributes = PaladinClass.NotRecommendedAttributes;
                bp.m_Spellbook = DreadKnightSpellbook.ToReference<BlueprintSpellbookReference>();
                bp.m_EquipmentEntities = PaladinClass.m_EquipmentEntities;
                bp.m_StartingItems = PaladinClass.StartingItems;
                bp.m_Difficulty = PaladinClass.Difficulty;
                bp.m_DefaultBuild = null;
                bp.m_Archetypes = new BlueprintArchetypeReference[] {};
                bp.SkillPoints = 3;
                bp.ClassSkills = new StatType[4] {
                    StatType.SkillKnowledgeWorld,
                    StatType.SkillMobility,
                    StatType.SkillAthletics,
                    StatType.SkillLoreReligion
                };
                bp.IsDivineCaster = true;
                bp.IsArcaneCaster = false;
                bp.StartingGold = 411;
                bp.PrimaryColor = 6;
                bp.SecondaryColor = 11;
                bp.MaleEquipmentEntities = PaladinClass.MaleEquipmentEntities;
                bp.FemaleEquipmentEntities = PaladinClass.FemaleEquipmentEntities;
                bp.RecommendedAttributes = PaladinClass.RecommendedAttributes;
                bp.AddComponent<PrerequisiteNoClassLevel>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<PrerequisiteIsPet>(c => {
                    c.Not = true;
                    c.HideInUI = true;
                });
                bp.AddComponent<PrerequisiteAlignment>(c => { c.Alignment = Kingmaker.UnitLogic.Alignments.AlignmentMaskType.Evil; });
            });            
            DreadKnightProgression.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                new BlueprintProgression.ClassWithLevel {
                    m_Class = DreadKnightClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                }
            };
            DreadKnightSpellbook.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
            // Proficiencies
            var ProfIcon = AssetLoader.LoadInternal("Skills", "Icon_DKProf.png");
            var PaladinClassProficiencies = Resources.GetBlueprint<BlueprintFeature>("b10ff88c03308b649b50c31611c2fefb");
            var DreadKnightProficiencies = Helpers.CreateBlueprint<BlueprintFeature>("DreadKnightProficiencies", bp => {
                bp.SetName("Dread Knight Proficiences");
                bp.SetDescription("Dread Knights are proficient with all simple and {g|Encyclopedia:Weapon_Proficiency}martial weapons{/g}, with all types of armor " +
                    "(heavy, medium, and light), and with shields (except tower shields).");
                bp.m_Icon = ProfIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PaladinClassProficiencies.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            #region Sinful Absolution
            var SinfulAbsIcon = AssetLoader.LoadInternal("Skills", "Icon_SinfulAbsolution.png");
            var FiendishSmiteGoodBuff = Resources.GetBlueprint<BlueprintBuff>("a9035e49d6d79a64eaec321f2cb629a8");
            var SinfulAbsolutionBuff = Helpers.CreateBlueprint<BlueprintBuff>("SinfulAbsolutionBuff", bp => {
                bp.SetName("Sinful Absolution");
                bp.SetDescription("Once per day, a dreadknight can reach out to the profane lords to grant sinful absolution to the forces arrayed against them. As a swift action, " +
                    "the dreadknight chooses one target within sight to crush. The Dread Knight adds their Charisma bonus (if any) on " +
                    "their attack rolls and adds their dreadknight level on all damage rolls made against the target of their unholy smite. Regardless of the target, sinful absolution " +
                    "attacks automatically bypass any DR the creature might possess. In addition, while Sinful Absolution is in effect, the dreadknight gains a deflection " +
                    "bonus equal to their Charisma modifier(if any) to their AC against attacks made by the target of sinful absolution. The sinful absolution effect remains until the " +
                    "target of sinful absolution is dead or the next time the Dread Knight rests and regains their uses of this ability. At 4th level, " +
                    "and at every three levels thereafter, the dreadknight may grant sinful absoltion one additional time per day.");
                bp.m_Icon = SinfulAbsIcon;
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Stack;
                bp.FxOnStart = FiendishSmiteGoodBuff.FxOnStart;
                bp.FxOnRemove = FiendishSmiteGoodBuff.FxOnRemove;
                bp.AddComponent<AttackBonusAgainstTarget>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.StatBonus
                    };
                    c.CheckCaster = true;
                });
                bp.AddComponent<DamageBonusAgainstTarget>(c => {
                    c.CheckCaster = true;
                    c.ApplyToSpellDamage = true;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.DamageBonus
                    };
                });
                bp.AddComponent<ACBonusAgainstTarget>(c => {
                    c.CheckCaster = true;
                    c.Descriptor = ModifierDescriptor.Deflection;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.StatBonus
                    };
                });
                bp.AddComponent<RemoveBuffIfCasterIsMissing>(c => {
                    c.RemoveOnCasterDeath = true;
                });
                bp.AddComponent<IgnoreTargetDR>(c => {
                    c.CheckCaster = true;
                });
                bp.AddComponent<UniqueBuff>();
            });
            var SinfulAbsolutionResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("SinfulAbsolutionResource", bp => {
                bp.m_Min = 1;
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByStat = false,
                    IncreasedByLevel = false,
                };
                bp.m_Max = 10;
            });
            var FiendishSmiteGoodAbility = Resources.GetBlueprint<BlueprintAbility>("478cf0e6c5f3a4142835faeae3bd3e04");
            var SinfulAbsolutionAbility = Helpers.CreateBlueprint<BlueprintAbility>("SinfulAbsolutionAbility", bp => {
                bp.SetName("Sinful Absolution");
                bp.SetDescription("Once per day, a dreadknight can reach out to the profane lords to grant sinful absolution to the forces arrayed against them. As a swift action, " +
                    "the dreadknight chooses one enemy within sight to crush. The dreadknight adds their Charisma bonus (if any) on " +
                    "their attack rolls and adds their dreadknight level on all damage rolls made against the target of their unholy smite. Regardless of the target, sinful absolution " +
                    "attacks automatically bypass any DR the creature might possess. In addition, while Sinful Absolution is in effect, the dreadknight gains a deflection " +
                    "bonus equal to their Charisma modifier(if any) to their AC against attacks made by the target of sinful absolution. The sinful absolution effect remains until the " +
                    "target of sinful absolution is dead or the next time the dreadknight rests and regains their uses of this ability. At 4th level, " +
                    "and at every three levels thereafter, the dreadknight may grant sinful absoltion one additional time per day.");
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Until the target of Sinful Absolution is dead");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "None");
                bp.m_Icon = SinfulAbsIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = Metamagic.Heighten | Metamagic.Reach;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {
                                    new ContextConditionIsEnemy(),
                                    new ContextConditionHasBuffFromCaster() {
                                        m_Buff = SinfulAbsolutionBuff.ToReference<BlueprintBuffReference>(),
                                        Not = true
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SinfulAbsolutionBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    DurationValue = new ContextDurationValue() {
                                        m_IsExtendable = true,
                                        DiceCountValue = new ContextValue(),
                                        BonusValue = new ContextValue()
                                    }
                                }
                            ),
                            IfFalse = Helpers.CreateActionList(),
                        });
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.StatBonus;
                    c.Value = new ContextDiceValue() {
                        DiceCountValue = new ContextValue(),
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueShared = AbilitySharedValue.StatBonus
                        },
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.DamageBonus;
                    c.Value = new ContextDiceValue() {
                        DiceCountValue = new ContextValue(),
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageBonus,
                            ValueShared = AbilitySharedValue.DamageBonus,
                        },
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent(Helpers.CreateContextRankConfig(c => {
                    c.m_Type = AbilityRankType.DamageBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Class = new BlueprintCharacterClassReference[] { DreadKnightClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 20;
                }));
                bp.AddComponent(Helpers.CreateContextRankConfig(c => {
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Charisma;
                    c.m_Max = 20;
                }));
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().PrefabLink;
                    c.Anchor = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().Anchor;
                    c.PositionAnchor = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().PositionAnchor;
                    c.OrientationAnchor = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().OrientationAnchor;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = SinfulAbsolutionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
            });
            var SinfulAbsolutionFeature = Helpers.CreateBlueprint<BlueprintFeature>("SinfulAbsolutionFeature", bp => {
                bp.SetName("Sinful Absolution");
                bp.SetDescription("Once per day, a dreadknight can reach out to the profane lords to grant sinful absolution to the forces arrayed against them. As a swift action, " +
                    "the dreadknight chooses one target within sight to crush. The dreadknight adds their Charisma bonus (if any) on " +
                    "their attack rolls and adds their dreadknight level on all damage rolls made against the target of their unholy smite. Regardless of the target, sinful absolution " +
                    "attacks automatically bypass any DR the creature might possess. In addition, while Sinful Absolution is in effect, the dreadknight gains a deflection " +
                    "bonus equal to their Charisma modifier(if any) to their AC against attacks made by the target of sinful absolution. The sinful absolution effect remains until the " +
                    "target of sinful absolution is dead or the next time the Dread Knight rests and regains their uses of this ability. At 4th level, " +
                    "and at every three levels thereafter, the dreadknight may grant sinful absoltion one additional time per day.");
                bp.m_DescriptionShort = Helpers.CreateString("$SinfulAbsolutionFeature.DescriptionShort", "Once per day, a dreadknight can reach out to the profane lords to grant sinful absolution to the forces arrayed against them. As a swift action, " +
                    "the dreadknight chooses one target within sight to crush. The dreadknight adds their Charisma bonus (if any) on " +
                    "their attack rolls and adds their dreadknight level on all damage rolls made against the target of their unholy smite. Regardless of the target, sinful absolution " +
                    "attacks automatically bypass any DR the creature might possess. In addition, while Sinful Absolution is in effect, the dreadknight gains a deflection " +
                    "bonus equal to their Charisma modifier(if any) to their AC against attacks made by the target of sinful absolution.");
                bp.m_Icon = SinfulAbsIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = SinfulAbsolutionResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SinfulAbsolutionAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
            });
            var SinfulAbsolutionUse = Helpers.CreateBlueprint<BlueprintFeature>("SinfulAbsolutionUse", bp => {
                bp.SetName("Sinful Absolution - Additional Use");
                bp.SetDescription("Once per day, a dreadknight can reach out to the profane lords to grant sinful absolution to the forces arrayed against them. As a swift action, " +
                    "the dreadknight chooses one target within sight to crush. The Dread Knight adds their Charisma bonus (if any) on " +
                    "their attack rolls and adds their dreadknight level on all damage rolls made against the target of their unholy smite. Regardless of the target, sinful absolution " +
                    "attacks automatically bypass any DR the creature might possess. In addition, while Sinful Absolution is in effect, the dreadknight gains a deflection " +
                    "bonus equal to their Charisma modifier(if any) to their AC against attacks made by the target of sinful absolution. The sinful absolution effect remains until the " +
                    "target of sinful absolution is dead or the next time the dreadknight rests and regains their uses of this ability. At 4th level, " +
                    "and at every three levels thereafter, the Dread Knight may grant sinful absoltion one additional time per day.");
                bp.m_Icon = SinfulAbsIcon;
                bp.Ranks = 10;
                bp.IsClassFeature = true;
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = SinfulAbsolutionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 1;
                });
            });
            var MythicCompanionClass = Resources.GetBlueprint<BlueprintCharacterClass>("530b6a79cb691c24ba99e1577b4beb6d");
            var MythicStartingClass = Resources.GetBlueprint<BlueprintCharacterClass>("247aa787806d5da4f89cfc3dff0b217f");
            var AeonMythicClass = Resources.GetBlueprint<BlueprintCharacterClass>("15a85e67b7d69554cab9ed5830d0268e");
            var AngelMythicClass = Resources.GetBlueprint<BlueprintCharacterClass>("a5a9fe8f663d701488bd1db8ea40484e");
            var AzataMythicClass = Resources.GetBlueprint<BlueprintCharacterClass>("9a3b2c63afa79744cbca46bea0da9a16");
            var DemonMythicClass = Resources.GetBlueprint<BlueprintCharacterClass>("8e19495ea576a8641964102d177e34b7");
            var LichMythicClass = Resources.GetBlueprint<BlueprintCharacterClass>("5d501618a28bdc24c80007a5c937dcb7");
            var DevilMythicClass = Resources.GetBlueprint<BlueprintCharacterClass>("211f49705f478b3468db6daa802452a2");
            var SwarmThatWalksClass = Resources.GetBlueprint<BlueprintCharacterClass>("5295b8e13c2303f4c88bdb3d7760a757");
            var MythicAbilitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("ba0e5a900b775be4a99702f1ed08914d");
            var AbundantSin = Helpers.CreateBlueprint<BlueprintFeature>("AbundantSin", bp => {
                bp.SetName("Abundant Sin");
                bp.SetDescription("You have found a way to force dark powers to grant you additional uses of the Sinful Absolution ability. " +
                    "\nBenefit: You can use Sinful Absolution a number of additional times per day equal to half your mythic rank.");
                bp.m_Icon = SinfulAbsIcon;
                bp.Groups = new FeatureGroup[] { FeatureGroup.MythicAbility };
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<IncreaseResourceAmountBySharedValue>(c => {
                    c.m_Resource = SinfulAbsolutionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = new ContextValue() { ValueType = ContextValueType.Rank };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_BaseValueType = ContextRankBaseValueType.MythicLevel;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_Max = 20;
                    c.m_Class = new BlueprintCharacterClassReference[9] {
                        MythicCompanionClass.ToReference<BlueprintCharacterClassReference>(),
                        MythicStartingClass.ToReference<BlueprintCharacterClassReference>(),
                        AeonMythicClass.ToReference<BlueprintCharacterClassReference>(),
                        AngelMythicClass.ToReference<BlueprintCharacterClassReference>(),
                        AzataMythicClass.ToReference<BlueprintCharacterClassReference>(),
                        DemonMythicClass.ToReference<BlueprintCharacterClassReference>(),
                        LichMythicClass.ToReference<BlueprintCharacterClassReference>(),
                        DevilMythicClass.ToReference<BlueprintCharacterClassReference>(),
                        SwarmThatWalksClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<PrerequisiteFeature>(c => { c.m_Feature = SinfulAbsolutionFeature.ToReference<BlueprintFeatureReference>(); });
            });
            MythicAbilitySelection.m_AllFeatures = MythicAbilitySelection.m_AllFeatures.AppendToArray(AbundantSin.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Aura of Evil
            var EvilIcon = AssetLoader.LoadInternal("Skills", "Icon_AuraEvil.png");
            var UnholyNimbus = Resources.GetBlueprint<BlueprintBuff>("ec14c8e821c460b42bb925a2320ddf0c");
            var AuraOfEvilBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfEvilBuff", bp => {
                bp.SetName("Aura of Evil");
                bp.SetDescription("At 1st level, a dreadknight radiates a profane aura that is easily detectable by those strongly " +
                    "attuned to a good alignment. This aura has no negative or positive effect.");
                bp.m_Icon = EvilIcon;
                bp.FxOnStart = UnholyNimbus.FxOnStart;
                bp.FxOnRemove = UnholyNimbus.FxOnRemove;
                bp.IsClassFeature = true;
            });
            var AuraOfEvilFeature = Helpers.CreateBlueprint<BlueprintFeature>("AuraOfEvilFeature", bp => {
                bp.SetName("Aura of Evil");
                bp.SetDescription("At 1st level, a dreadknight radiates a profane aura that is easily detectable by those strongly " +
                    "attuned to a good alignment. This aura has no negative or positive effect.");
                bp.m_Icon = EvilIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                //Remove fx for now
                //bp.AddComponent<AuraFeatureComponent>(c => {
                //    c.m_Buff = AuraOfEvilBuff.ToReference<BlueprintBuffReference>();
                //});
            });
            #endregion
            #region Profane Resilience
            var ProfaneResIcon = AssetLoader.LoadInternal("Skills", "Icon_UnholyRes.png");
            var ProfaneResilience = Helpers.CreateBlueprint<BlueprintFeature>("ProfaneResilience", bp => {
                bp.SetName("Profane Resilience");
                bp.SetDescription("At 2nd level, a dreadknight gains a bonus equal to his Charisma bonus (if any) on all saving throws.");
                bp.m_Icon = ProfaneResIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<DerivativeStatBonus>(c => {
                    c.BaseStat = StatType.Charisma;
                    c.DerivativeStat = StatType.SaveFortitude;
                });
                bp.AddComponent<DerivativeStatBonus>(c => {
                    c.BaseStat = StatType.Charisma;
                    c.DerivativeStat = StatType.SaveWill;
                });
                bp.AddComponent<DerivativeStatBonus>(c => {
                    c.BaseStat = StatType.Charisma;
                    c.DerivativeStat = StatType.SaveReflex;
                });
                bp.AddComponent<RecalculateOnStatChange>(c => {
                    c.UseKineticistMainStat = false;
                    c.Stat = StatType.Charisma;
                });
            });
            #endregion
            #region Touch of Profane Corruption
            var TOCIcon = AssetLoader.LoadInternal("Skills", "Icon_TouchCorrupt.png");
            var ChannelTOCIcon = AssetLoader.LoadInternal("Skills", "Icon_ChannelTOC.png");
            var AbsoluteDeathAbility = Resources.GetBlueprint<BlueprintAbility>("7d721be6d74f07f4d952ee8d6f8f44a0");            
            var TouchOfProfaneCorruptionResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("TouchOfProfaneCorruptionResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[0],
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[0],
                    IncreasedByLevelStartPlusDivStep = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Charisma
                };
                bp.m_Max = 20;
            });
            var FatiguedBuff = Resources.GetBlueprint<BlueprintBuff>("e6f2fc5d73d88064583cb828801212f4");
            var TouchItem = Resources.GetBlueprint<BlueprintItemWeapon>("bb337517547de1a4189518d404ec49d4");
            var NegativeEnergyAffinity = Resources.GetBlueprint<BlueprintFeature>("d5ee498e19722854198439629c1841a5");
            var TouchOfProfaneCorruptionAbility = Helpers.CreateBlueprint<BlueprintAbility>("TouchOfProfaneCorruptionAbility", bp => {
                bp.SetName("Touch of Profane Corruption");
                bp.SetDescription("Beginning at 2nd level, a dreadknight surrounds his hand with a profane flame, causing terrible wounds to open on those he touches. " +
               "Each day he can use this ability a number of times equal to 1/2 his Dread Knight level + his Charisma modifier. As a touch attack, a dreadknight can cause " +
               "1d6 points of damage plus an additional 1d6 points of damage for every two dreadknight levels he possesses. Using this ability is a standard action that does " +
               "not provoke attacks of opportunity. " +
               "\nAlternatively, a dreadknight can use this power to heal undead creatures, restoring 1d6 hit points plus an additional 1d6 hit points for every two levels " +
               "the dreadknight possesses.This ability is modified by any feat, spell, or effect that specifically works with the lay on hands paladin class feature. " +
               "For example, the Extra Lay On Hands feat grants a dreadknight 2 additional uses of the touch of corruption class feature.");
                bp.m_Icon = TOCIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "None");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
                bp.AddContextRankConfig(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Class = new BlueprintCharacterClassReference[] { DreadKnightClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Progression = ContextRankProgression.Div2;                    
                    c.m_Min = 1;
                    c.m_UseMin = true;
                });
                bp.AddComponent<AbilityDeliverTouch>(c => {
                    c.m_TouchWeapon = TouchItem.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                      new Conditional() {
                          ConditionsChecker = new ConditionsChecker() {
                              Conditions = new Condition[] {
                              new ContextConditionHasFact() {
                                  m_Fact = NegativeEnergyAffinity.ToReference<BlueprintUnitFactReference>()
                              }
                              }
                          },
                          IfTrue = Helpers.CreateActionList(
                              new ContextActionHealTarget() {
                                  Value = new ContextDiceValue() {
                                      DiceType = DiceType.D6,
                                      DiceCountValue = new ContextValue() {
                                          ValueType = ContextValueType.Rank,
                                          ValueRank = AbilityRankType.DamageDice
                                      },
                                      BonusValue = new ContextValue()
                                  }
                              }),
                          IfFalse = Helpers.CreateActionList(
                                                new ContextActionDealDamage() {
                                                    DamageType = new DamageTypeDescription() {
                                                        Type = DamageType.Energy,
                                                        Common = new DamageTypeDescription.CommomData(),
                                                        Physical = new DamageTypeDescription.PhysicalData(),
                                                        Energy = DamageEnergyType.NegativeEnergy,
                                                    },
                                                    Duration = new ContextDurationValue() {
                                                        m_IsExtendable = true,
                                                        DiceCountValue = new ContextValue(),
                                                        BonusValue = new ContextValue(),
                                                    },
                                                    Value = new ContextDiceValue() {
                                                        DiceType = DiceType.D6,
                                                        DiceCountValue = new ContextValue() {
                                                            ValueType = ContextValueType.Rank,
                                                            ValueRank = AbilityRankType.DamageDice
                                                        },
                                                        BonusValue = new ContextValue(),
                                                    }
                                                })
                      });
                });
                bp.AddComponent<AbilityEffectMiss>();
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = AbsoluteDeathAbility.GetComponent<AbilitySpawnFx>().PrefabLink;
                    c.Anchor = AbsoluteDeathAbility.GetComponent<AbilitySpawnFx>().Anchor;
                    c.PositionAnchor = AbsoluteDeathAbility.GetComponent<AbilitySpawnFx>().PositionAnchor;
                    c.OrientationAnchor = AbsoluteDeathAbility.GetComponent<AbilitySpawnFx>().OrientationAnchor;
                });
            });
            var FiendishQuarryAbility = Resources.GetBlueprint<BlueprintAbility>("d9660af97d116f94ab98dbec15dbc704");
            var ChannelTouchOfProfaneCorruptionAbility = Helpers.CreateBlueprint<BlueprintAbility>("ChannelTouchOfProfaneCorruptionAbility", bp => {
                bp.SetName("Channel Profane Corruption");
                bp.SetDescription("Beginning at 7th level as a swift action, a dreadknight can spend three uses of their profane corruption to " +
                    "surround their weapon with a profane flame and deliver an attack with their weapon, causing 1d6 points of damage plus an additional " +
                    "1d6 points of damage for every two dreadknight levels they possesses, as well as applying the cruelty selected. ");
                bp.m_Icon = ChannelTOCIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Weapon;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "Instant");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "None");
                bp.AddContextRankConfig(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Class = new BlueprintCharacterClassReference[] { DreadKnightClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 1;
                    c.m_StepLevel = 2;
                    c.m_Min = 1;
                    c.m_UseMin = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                      new Conditional() {
                          ConditionsChecker = new ConditionsChecker() {
                              Conditions = new Condition[] {
                                  new ContextConditionHasFact() {
                                      m_Fact = NegativeEnergyAffinity.ToReference<BlueprintUnitFactReference>()
                                  }
                              }
                          },
                          IfTrue = Helpers.CreateActionList(
                              new ContextActionHealTarget() {
                                  Value = new ContextDiceValue() {
                                      DiceType = DiceType.D6,
                                      DiceCountValue = new ContextValue() {
                                          ValueType = ContextValueType.Rank,
                                          ValueRank = AbilityRankType.DamageDice
                                      },
                                      BonusValue = new ContextValue()
                                  }
                              }),
                          IfFalse = Helpers.CreateActionList(
                                                new ContextActionDealDamage() {
                                                    DamageType = new DamageTypeDescription() {
                                                        Type = DamageType.Energy,
                                                        Common = new DamageTypeDescription.CommomData(),
                                                        Physical = new DamageTypeDescription.PhysicalData(),
                                                        Energy = DamageEnergyType.NegativeEnergy,
                                                    },
                                                    Duration = new ContextDurationValue() {
                                                        m_IsExtendable = true,
                                                        DiceCountValue = new ContextValue(),
                                                        BonusValue = new ContextValue(),
                                                    },
                                                    Value = new ContextDiceValue() {
                                                        DiceType = DiceType.D6,
                                                        DiceCountValue = new ContextValue() {
                                                            ValueType = ContextValueType.Rank,
                                                            ValueRank = AbilityRankType.DamageDice
                                                        },
                                                        BonusValue = new ContextValue(),
                                                    }
                                                })
                      });
                });
                bp.AddComponent<AbilityDeliverAttackWithWeapon>();
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().PrefabLink;
                    c.Anchor = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().Anchor;
                    c.PositionAnchor = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().PositionAnchor;
                    c.OrientationAnchor = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().OrientationAnchor;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 3;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
            });
            var ChannelTouchOfProfaneCorruptionFeature = Helpers.CreateBlueprint<BlueprintFeature>("ChannelTouchOfProfaneCorruptionFeature", bp => {
                bp.SetName("Channel Profane Corruption");
                bp.SetDescription("Beginning at 7th level as a swift action, a dreadknight can spend three uses of their touch of profane corruption to " +
                    "surround their weapon with a profane flame, causing terrible wounds to open on those they attacks. This ability works with any weapon.");
                bp.m_DescriptionShort = Helpers.CreateString("$TouchOfProfaneCorruptionFeature.DescriptionShort", "Beginning at 7th level as a swift action, a dreadknight can spend two uses of their touch of profane corruption to " +
                    "surround their weapon with a profane flame, causing terrible wounds to open on those they attacks. This ability works with any weapon.");
                bp.m_Icon = ChannelTOCIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var TouchOfProfaneCorruptionFact = Helpers.CreateBlueprint<BlueprintUnitFact>("TouchOfProfaneCorruptionFact", bp => {
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
            });
            var CrueltyResource = Resources.GetModBlueprint<BlueprintUnitFact>("CrueltyResource");
            var CrueltyFact = Resources.GetModBlueprint<BlueprintUnitFact>("CrueltyFact");
            var TouchOfProfaneCorruptionFeature = Helpers.CreateBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionFeature", bp => {
                bp.SetName("Profane Corruption");
                bp.SetDescription("Beginning at 2nd level, a dreadknight surrounds her hand with a profane flame, causing terrible wounds to open on those she touches. " +
               "Each day she can use this ability a number of times equal to 1/2 his Dread Knight level + her Charisma modifier. As a touch attack, a dreadknight can cause " +
               "1d6 points of damage plus an additional 1d6 points of damage for every two Dread Knight levels she possesses. Using this ability is a standard action that does " +
               "not provoke attacks of opportunity. " +
               "\nAlternatively, a dreadknight can use this power to heal undead creatures, restoring 1d6 hit points plus an additional 1d6 hit points for every two levels " +
               "the dreadknight possesses.This ability is modified by any feat, spell, or effect that specifically works with the lay on hands paladin class feature. " +
               "For example, the Extra Lay On Hands feat grants a dreadknight 2 additional uses of the touch of corruption class feature.");
                bp.m_DescriptionShort = Helpers.CreateString("$TouchOfProfaneCorruptionFeature.DescriptionShort", "Beginning at 2nd level, a dreadknight can surround her hand " +
                    "with a profane flame, causing terrible wounds to open on those she touches. Alternatively, a dreadknight can use this power to heal undead creatures.");
                bp.m_Icon = TOCIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbility.ToReference<BlueprintUnitFactReference>(), TouchOfProfaneCorruptionFact.ToReference<BlueprintUnitFactReference>(), CrueltyFact.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 1;
                });
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = CrueltyResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 1;
                });
            });
            var TouchOfProfaneCorruptionUse = Helpers.CreateBlueprint<BlueprintFeature>("TouchOfProfaneCorruptionUse", bp => {
                bp.SetName("Profane Corruption - Additional Use");
                bp.SetDescription("Beginning at 2nd level, a dreadknight surrounds his hand with a profane flame, causing terrible wounds to open on those he touches. " +
               "Each day he can use this ability a number of times equal to 1/2 his dreadknight level + his Charisma modifier. As a touch attack, a dreadknight can cause " +
               "1d6 points of damage plus an additional 1d6 points of damage for every two Dread Knight levels he possesses. Using this ability is a standard action that does " +
               "not provoke attacks of opportunity. " +
               "\nAlternatively, a dreadknight can use this power to heal undead creatures, restoring 1d6 hit points plus an additional 1d6 hit points for every two levels " +
               "the Dread Knight possesses.This ability is modified by any feat, spell, or effect that specifically works with the lay on hands paladin class feature. " +
               "For example, the Extra Lay On Hands feat grants a dreadknight 2 additional uses of the touch of corruption class feature.");
                bp.m_Icon = TOCIcon;
                bp.Ranks = 10;
                bp.IsClassFeature = true;
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 1;
                });
            });
            var ExtraLayOnHands = Resources.GetBlueprint<BlueprintFeature>("a2b2f20dfb4d3ed40b9198e22be82030");
            ExtraLayOnHands.AddComponent<IncreaseResourceAmount>(c => { c.m_Resource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>(); c.Value = 2; });            
            // Cruelties
            var StunnedBuff = Resources.GetBlueprint<BlueprintBuff>("09d39b38bb7c6014394b6daced9bacd3");
            var ParalyzedBuff = Resources.GetBlueprint<BlueprintBuff>("af1e2d232ebbb334aaf25e2a46a92591");
            var BlindedBuff = Resources.GetBlueprint<BlueprintBuff>("187f88d96a0ef464280706b63635f2af");
            var PoisonedBuff = Resources.GetBlueprint<BlueprintBuff>("ba1ae42c58e228c4da28328ea6b4ae34");
            var NauseatedBuff = Resources.GetBlueprint<BlueprintBuff>("956331dba5125ef48afe41875a00ca0e");
            var FrightenedBuff = Resources.GetBlueprint<BlueprintBuff>("f08a7239aa961f34c8301518e71d4cdf");
            var ExhaustedBuff = Resources.GetBlueprint<BlueprintBuff>("46d1b9cc3d0fd36469a471b047d773a2");
            var CursedBuff = Resources.GetBlueprint<BlueprintBuff>("caae9592917719a41b601b678a8e6ddf");
            var StaggeredBuff = Resources.GetBlueprint<BlueprintBuff>("df3950af5a783bd4d91ab73eb8fa0fd3");
            var DiseasedBuff = Resources.GetBlueprint<BlueprintBuff>("b523ff6c5db9a9c489daff7aae41afb9");
            var DazedBuff = Resources.GetBlueprint<BlueprintBuff>("d2e35b870e4ac574d9873b36402487e5");
            var SickenedBuff = Resources.GetBlueprint<BlueprintBuff>("4e42460798665fd4cb9173ffa7ada323");
            var ShakenBuff = Resources.GetBlueprint<BlueprintBuff>("25ec6cb6ab1845c48a95f9c20b034220");
            var CrueltySelectIcon = AssetLoader.LoadInternal("Skills", "Icon_CrueltySelect.png");
            var FatigueIcon = Resources.GetBlueprint<BlueprintBuff>("e6f2fc5d73d88064583cb828801212f4");
            var BestowCurseFeeblyBody = Resources.GetBlueprint<BlueprintAbility>("0c853a9f35a7bf749821ebe5d06fade7");
            var CrueltyFatiguedBuff = Helpers.CreateBuff("CrueltyFatiguedBuff", bp => {
                bp.SetName("Cruelty - Fatigued");
                bp.SetDescription("The next use of profane corruption will be enhanced with the fatigued cruelty.");
                bp.m_Icon = FatigueIcon.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>(),
                        ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>()
                    };
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
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {
                                    new ContextConditionIsAlly() { Not = true }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                            new ContextActionSavingThrow() {
                                Type = SavingThrowType.Fortitude,
                                m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                HasCustomDC = false,
                                CustomDC = new ContextValue(),
                                Actions = Helpers.CreateActionList(
                                    new ContextActionConditionalSaved() {
                                        Succeed = new ActionList(),
                                        Failed = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = FatiguedBuff.ToReference<BlueprintBuffReference>(),
                                                Permanent = true,
                                                DurationValue = new ContextDurationValue() {
                                                    m_IsExtendable = true,
                                                    DiceCountValue = new ContextValue(),
                                                    BonusValue = new ContextValue()
                                                },
                                                IsFromSpell = true,
                                            }),
                                    }),
                            }),
                            IfFalse = Helpers.CreateActionList(),
                        });
                });
                bp.AddComponent<UniqueBuff>();
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.StatType = StatType.Charisma;
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                });
            });
            var CrueltyShakenBuff = Helpers.CreateBuff("CrueltyShakenBuff", bp => {
                bp.SetName("Cruelty - Shaken");
                bp.SetDescription("The next use of profane corruption will be enhanced with the shaken cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = FatigueIcon.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>(),
                        ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>()
                    };
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
                                HasCustomDC = false,
                                CustomDC = new ContextValue(),
                                Actions = Helpers.CreateActionList(
                                    new ContextActionConditionalSaved() {
                                        Succeed = new ActionList(),
                                        Failed = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = ShakenBuff.ToReference<BlueprintBuffReference>(),
                                                DurationValue = new ContextDurationValue() {
                                                    m_IsExtendable = true,
                                                    DiceCountValue = 0,
                                                    BonusValue = new ContextValue() {
                                                        Value = 1,
                                                        ValueType = ContextValueType.Rank,
                                                        ValueRank = AbilityRankType.Default
                                                    }
                                                },
                                                IsFromSpell = true,
                                            }),
                                    }),
                            }),
                            IfFalse = Helpers.CreateActionList(),
                        });
                });
                bp.AddComponent<UniqueBuff>();
                bp.AddContextRankConfig(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Class = new BlueprintCharacterClassReference[] { DreadKnightClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 20;
                });
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.StatType = StatType.Charisma;
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                });
            });
            var CrueltySickenedBuff = Helpers.CreateBuff("CrueltySickenedBuff", bp => {
                bp.SetName("Cruelty - Sickened");
                bp.SetDescription("The next use of profane corruption will be enhanced with the sickened cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = SickenedBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>(),
                        ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>()
                    };
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
                                HasCustomDC = false,
                                CustomDC = new ContextValue(),
                                Actions = Helpers.CreateActionList(
                                    new ContextActionConditionalSaved() {
                                        Succeed = new ActionList(),
                                        Failed = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = SickenedBuff.ToReference<BlueprintBuffReference>(),
                                                DurationValue = new ContextDurationValue() {
                                                    m_IsExtendable = true,
                                                    DiceCountValue = 0,
                                                    BonusValue = new ContextValue() {
                                                        Value = 1,
                                                        ValueType = ContextValueType.Rank,
                                                        ValueRank = AbilityRankType.Default
                                                    }
                                                },
                                                IsFromSpell = true,
                                            }),
                                    }),
                            }),
                            IfFalse = Helpers.CreateActionList(),
                        });
                });
                bp.AddComponent<UniqueBuff>();
                bp.AddContextRankConfig(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Class = new BlueprintCharacterClassReference[] { DreadKnightClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 20;
                });
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.StatType = StatType.Charisma;
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                });
            });
            var CrueltyDazedBuff = Helpers.CreateBuff("CrueltyDazedBuff", bp => {
                bp.SetName("Cruelty - Dazed");
                bp.SetDescription("The next use of profane corruption will be enhanced with the dazed cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = DazedBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>(),
                        ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>()
                    };
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
                                HasCustomDC = false,
                                CustomDC = new ContextValue(),
                                Actions = Helpers.CreateActionList(
                                    new ContextActionConditionalSaved() {
                                        Succeed = new ActionList(),
                                        Failed = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = DazedBuff.ToReference<BlueprintBuffReference>(),
                                                DurationValue = new ContextDurationValue() {
                                                    m_IsExtendable = true,
                                                    DiceCountValue = 0,
                                                    BonusValue = new ContextValue() {
                                                        Value = 1
                                                    }
                                                },
                                                IsFromSpell = true,
                                            }),
                                    }),
                            }),
                            IfFalse = Helpers.CreateActionList(),
                        });
                });
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.StatType = StatType.Charisma;
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                });
            });
            var CrueltyDiseasedBuff = Helpers.CreateBuff("CrueltyDiseasedBuff", bp => {
                bp.SetName("Cruelty - Diseased");
                bp.SetDescription("The next use of profane corruption will be enhanced with the diseased cruelty.");
                bp.m_Icon = DiseasedBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>(),
                        ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>()
                    };
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
                                HasCustomDC = false,
                                CustomDC = new ContextValue(),
                                Actions = Helpers.CreateActionList(
                                    new ContextActionConditionalSaved() {
                                        Succeed = new ActionList(),
                                        Failed = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = DiseasedBuff.ToReference<BlueprintBuffReference>(),
                                                Permanent = true,
                                                DurationValue = new ContextDurationValue() {
                                                    m_IsExtendable = true,
                                                    DiceCountValue = new ContextValue(),
                                                    BonusValue = new ContextValue()
                                                },
                                                IsFromSpell = true,
                                            }),
                                    }),
                            }),
                            IfFalse = Helpers.CreateActionList(),
                        });
                });
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.StatType = StatType.Charisma;
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                });
            });
            var CrueltyStaggeredBuff = Helpers.CreateBuff("CrueltyStaggeredBuff", bp => {
                bp.SetName("Cruelty - Staggered");
                bp.SetDescription("The next use of profane corruption will be enhanced with the staggered cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = StaggeredBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>(),
                        ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>()
                    };
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
                                HasCustomDC = false,
                                CustomDC = new ContextValue(),
                                Actions = Helpers.CreateActionList(
                                    new ContextActionConditionalSaved() {
                                        Succeed = new ActionList(),
                                        Failed = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = StaggeredBuff.ToReference<BlueprintBuffReference>(),
                                                DurationValue = new ContextDurationValue() {
                                                    m_IsExtendable = true,
                                                    DiceCountValue = 0,
                                                    BonusValue = new ContextValue() {
                                                        Value = 1,
                                                        ValueType = ContextValueType.Rank,
                                                        ValueRank = AbilityRankType.Default
                                                    }
                                                },
                                                IsFromSpell = true,
                                            }),
                                    }),
                            }),
                            IfFalse = Helpers.CreateActionList(),
                        });
                });;
                bp.AddContextRankConfig(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Class = new BlueprintCharacterClassReference[] { DreadKnightClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_Max = 20;
                });
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.StatType = StatType.Charisma;
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                });
            });
            var CrueltyCursedBuff = Helpers.CreateBuff("CrueltyCursedBuff", bp => {
                bp.SetName("Select Cruelty - Cursed");
                bp.SetDescription("The next use of profane corruption will be enhanced with the cursed cruelty.");
                bp.m_Icon = CursedBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>(),
                        ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>()
                    };
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
                                HasCustomDC = false,
                                CustomDC = new ContextValue(),
                                Actions = Helpers.CreateActionList(
                                    new ContextActionConditionalSaved() {
                                        Succeed = new ActionList(),
                                        Failed = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = CursedBuff.ToReference<BlueprintBuffReference>(),
                                                Permanent = true,
                                                DurationValue = new ContextDurationValue() {
                                                    m_IsExtendable = true,
                                                    DiceCountValue = new ContextValue(),
                                                    BonusValue = new ContextValue()
                                                },
                                                IsFromSpell = true,
                                            }),
                                    }),
                            }),
                            IfFalse = Helpers.CreateActionList(),
                        });
                });
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.StatType = StatType.Charisma;
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                });
            });
            var CrueltyExhaustedBuff = Helpers.CreateBuff("CrueltyExhaustedBuff", bp => {
                bp.SetName("Cruelty - Exhausted");
                bp.SetDescription("The next use of profane corruption will be enhanced with the exhausted cruelty.");
                bp.m_Icon = ExhaustedBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>(),
                        ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>()
                    };
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
                                HasCustomDC = false,
                                CustomDC = new ContextValue(),
                                Actions = Helpers.CreateActionList(
                                    new ContextActionConditionalSaved() {
                                        Succeed = new ActionList(),
                                        Failed = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = ExhaustedBuff.ToReference<BlueprintBuffReference>(),
                                                Permanent = true,
                                                DurationValue = new ContextDurationValue() {
                                                    m_IsExtendable = true,
                                                    DiceCountValue = new ContextValue(),
                                                    BonusValue = new ContextValue()
                                                },
                                                IsFromSpell = true,
                                            }),
                                    }),
                            }),
                            IfFalse = Helpers.CreateActionList(),
                        });
                });
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.StatType = StatType.Charisma;
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                });
            });
            var CrueltyFrightenedBuff = Helpers.CreateBuff("CrueltyFrightenedBuff", bp => {
                bp.SetName("Cruelty - Frightened");
                bp.SetDescription("The next use of profane corruption will be enhanced with the frightened cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = FrightenedBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>(),
                        ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>()
                    };
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
                                HasCustomDC = false,
                                CustomDC = new ContextValue(),
                                Actions = Helpers.CreateActionList(
                                    new ContextActionConditionalSaved() {
                                        Succeed = new ActionList(),
                                        Failed = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = FrightenedBuff.ToReference<BlueprintBuffReference>(),
                                                DurationValue = new ContextDurationValue() {
                                                    m_IsExtendable = true,
                                                    DiceCountValue = 0,
                                                    BonusValue = new ContextValue() {
                                                        Value = 1,
                                                        ValueType = ContextValueType.Rank,
                                                        ValueRank = AbilityRankType.Default
                                                    }
                                                },
                                                IsFromSpell = true,
                                            }),
                                    }),
                            }),
                            IfFalse = Helpers.CreateActionList(),
                        });
                });
                bp.AddContextRankConfig(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Class = new BlueprintCharacterClassReference[] { DreadKnightClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_Max = 20;
                });
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.StatType = StatType.Charisma;
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                });
            });
            var CrueltyNauseatedBuff = Helpers.CreateBuff("CrueltyNauseatedBuff", bp => {
                bp.SetName("Cruelty - Nauseated");
                bp.SetDescription("The next use of profane corruption will be enhanced with the nauseated cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = NauseatedBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>(),
                        ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>()
                    };
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
                                HasCustomDC = false,
                                CustomDC = new ContextValue(),
                                Actions = Helpers.CreateActionList(
                                    new ContextActionConditionalSaved() {
                                        Succeed = new ActionList(),
                                        Failed = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = NauseatedBuff.ToReference<BlueprintBuffReference>(),
                                                DurationValue = new ContextDurationValue() {
                                                    m_IsExtendable = true,
                                                    DiceCountValue = 0,
                                                    BonusValue = new ContextValue() {
                                                        Value = 1,
                                                        ValueType = ContextValueType.Rank,
                                                        ValueRank = AbilityRankType.Default
                                                    }
                                                },
                                                IsFromSpell = true,
                                            }),
                                    }),
                            }),
                            IfFalse = Helpers.CreateActionList(),
                        });
                });
                bp.AddContextRankConfig(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Class = new BlueprintCharacterClassReference[] { DreadKnightClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Progression = ContextRankProgression.DivStep;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 3;
                    c.m_UseMin = true;
                    c.m_Min = 1;                    
                });
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.StatType = StatType.Charisma;
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                });
            });
            var CrueltyPoisonedBuff = Helpers.CreateBuff("CrueltyPoisonedBuff", bp => {
                bp.SetName("Cruelty - Poisoned");
                bp.SetDescription("The next use of profane corruption will be enhanced with the poisoned cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = PoisonedBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>(),
                        ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>()
                    };
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
                                HasCustomDC = false,
                                CustomDC = new ContextValue(),
                                Actions = Helpers.CreateActionList(
                                    new ContextActionConditionalSaved() {
                                        Succeed = new ActionList(),
                                        Failed = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = PoisonedBuff.ToReference<BlueprintBuffReference>(),
                                                Permanent = true,
                                                DurationValue = new ContextDurationValue() {
                                                    m_IsExtendable = true,
                                                    DiceCountValue = new ContextValue(),
                                                    BonusValue = new ContextValue()
                                                },
                                                IsFromSpell = true,
                                            }),
                                    }),
                            }),
                            IfFalse = Helpers.CreateActionList(),
                        });
                });
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.StatType = StatType.Charisma;
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                });
            });
            var CrueltyBlindedBuff = Helpers.CreateBuff("CrueltyBlindedBuff", bp => {
                bp.SetName("Cruelty - Blinded");
                bp.SetDescription("The next use of profane corruption will be enhanced with the blinded cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = BlindedBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>(),
                        ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>()
                    };
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
                                HasCustomDC = false,
                                CustomDC = new ContextValue(),
                                Actions = Helpers.CreateActionList(
                                    new ContextActionConditionalSaved() {
                                        Succeed = new ActionList(),
                                        Failed = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = BlindedBuff.ToReference<BlueprintBuffReference>(),
                                                DurationValue = new ContextDurationValue() {
                                                    m_IsExtendable = true,
                                                    DiceCountValue = 0,
                                                    BonusValue = new ContextValue() {
                                                        Value = 1,
                                                        ValueType = ContextValueType.Rank,
                                                        ValueRank = AbilityRankType.Default
                                                    }
                                                },
                                                IsFromSpell = true,
                                            }),
                                    }),
                            }),
                            IfFalse = Helpers.CreateActionList(),
                        });
                });
                bp.AddContextRankConfig(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Class = new BlueprintCharacterClassReference[] { DreadKnightClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 20;
                });
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.StatType = StatType.Charisma;
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                });
            });
            var CrueltyParalyzedBuff = Helpers.CreateBuff("CrueltyParalyzedBuff", bp => {
                bp.SetName("Cruelty - Paralyzed");
                bp.SetDescription("The next use of profane corruption will be enhanced with the paralyzed cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = ParalyzedBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>(),
                        ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>()
                    };
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
                                HasCustomDC = false,
                                CustomDC = new ContextValue(),
                                Actions = Helpers.CreateActionList(
                                    new ContextActionConditionalSaved() {
                                        Succeed = new ActionList(),
                                        Failed = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = ParalyzedBuff.ToReference<BlueprintBuffReference>(),
                                                DurationValue = new ContextDurationValue() {
                                                    m_IsExtendable = true,
                                                    DiceCountValue = 0,
                                                    BonusValue = new ContextValue() {
                                                        Value = 1                                                        
                                                    }
                                                },
                                                IsFromSpell = true,
                                            }),
                                    }),
                            }),
                            IfFalse = Helpers.CreateActionList(),
                        });
                });
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.StatType = StatType.Charisma;
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                });
            });
            var CrueltyStunnedBuff = Helpers.CreateBuff("CrueltyStunnedBuff", bp => {
                bp.SetName("Cruelty - Stunned");
                bp.SetDescription("The next use of profane corruption will be enhanced with the stunned cruelty.");
                bp.Ranks = 1;
                bp.m_Icon = StunnedBuff.Icon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAbilityUseTrigger>(c => {
                    c.ActionsOnAllTargets = false;
                    c.AfterCast = true;
                    c.ActionsOnTarget = true;
                    c.FromSpellbook = false;
                    c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                    c.ForOneSpell = false;
                    c.m_Ability = new BlueprintAbilityReference();
                    c.ForMultipleSpells = true;
                    c.Abilities = new List<BlueprintAbilityReference>() {
                        TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>(),
                        ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>()
                    };
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
                        new Conditional {
                            ConditionsChecker = new ConditionsChecker {
                                Conditions = new Condition[] {
                                    new ContextConditionIsCaster() { Not = true }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                            new ContextActionSavingThrow() {
                                m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                Type = SavingThrowType.Fortitude,
                                CustomDC = new ContextValue(),
                                Actions = Helpers.CreateActionList(
                                    new ContextActionConditionalSaved() {
                                        Succeed = new ActionList(),
                                        Failed = Helpers.CreateActionList(
                                            new ContextActionApplyBuff() {
                                                m_Buff = StunnedBuff.ToReference<BlueprintBuffReference>(),
                                                DurationValue = new ContextDurationValue() {
                                                    m_IsExtendable = true,
                                                    DiceCountValue = 0,
                                                    BonusValue = new ContextValue() {
                                                        Value = 1,
                                                        ValueType = ContextValueType.Rank
                                                    }
                                                },
                                                IsFromSpell = true,
                                            }),
                                    }),
                            }),
                            IfFalse = Helpers.CreateActionList(),
                        });
                });
                bp.AddContextRankConfig(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Class = new BlueprintCharacterClassReference[] { DreadKnightClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Progression = ContextRankProgression.DivStep;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 4;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                });
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.StatType = StatType.Charisma;
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                });
            });
            var TouchOfProfaneCorruptionAbilityFatigued = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityFatigued", bp => {
                bp.SetName("Select Cruelty - Fatigued");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to become fatigued.");
                bp.m_Icon = FatigueIcon.Icon;
                bp.m_Buff = CrueltyFatiguedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyFatigued = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyFatigued", bp => {
                bp.SetName("Cruelty - Fatigued");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to become fatigued upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[1] { TouchOfProfaneCorruptionAbilityFatigued.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var TouchOfProfaneCorruptionAbilityShaken = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityShaken", bp => {
                bp.SetName("Select Cruelty - Shaken");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to become shaken for 1 round/dread knight level upon a failed fortitude save.");
                bp.m_Buff = CrueltyShakenBuff.ToReference<BlueprintBuffReference>();
                bp.m_Icon = ShakenBuff.Icon;
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyShaken = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyShaken", bp => {
                bp.SetName("Cruelty - Shaken");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time, and must be used once before selecting another cruelty. " +
                    "This cruelty causes the target to become shaken upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityShaken.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var TouchOfProfaneCorruptionAbilitySickened = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilitySickened", bp => {
                bp.SetName("Select Cruelty - Sickened");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to become sickened for 1 round/dread knight level upon a failed fortitude save.");
                bp.m_Icon = SickenedBuff.Icon;
                bp.m_Buff = CrueltySickenedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltySickened = Helpers.CreateBlueprint<BlueprintFeature>("CrueltySickened", bp => {
                bp.SetName("Cruelty - Sickened");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time, and must be used once before selecting another cruelty. " +
                    "This cruelty causes the target to become sickened upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilitySickened.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var TouchOfProfaneCorruptionAbilityDazed = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityDazed", bp => {
                bp.SetName("Select Cruelty - Dazed");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to become dazed for 1 round upon a failed fortitude save.");
                bp.m_Icon = DazedBuff.Icon;
                bp.m_Buff = CrueltyDazedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyDazed = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyDazed", bp => {
                bp.SetName("Cruelty - Dazed");
                bp.SetDescription("A Dread Knight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to become dazed for 1 round upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityDazed.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var TouchOfProfaneCorruptionAbilityDiseased = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityDiseased", bp => {
                bp.SetName("Select Cruelty - Diseased");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to contract the bubonic plague upon a failed fortitude save.");
                bp.m_Icon = DiseasedBuff.Icon;
                bp.m_Buff = CrueltyDiseasedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyDiseased = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyDiseased", bp => {
                bp.SetName("Cruelty - Diseased");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to contract the bubonic plague upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityDiseased.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var TouchOfProfaneCorruptionAbilityStaggered = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityStaggered", bp => {
                bp.SetName("Select Cruelty - Staggered");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to become staggered for 1 round/two levels of dreadknight upon a failed fortitude save.");
                bp.m_Icon = StaggeredBuff.Icon;
                bp.m_Buff = CrueltyStaggeredBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyStaggered = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyStaggered", bp => {
                bp.SetName("Cruelty - Staggered");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                     "This cruelty causes the target to become staggered for 1 round/two levels of dreadknight upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityStaggered.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var TouchOfProfaneCorruptionAbilityCursed = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityCursed", bp => {
                bp.SetName("Select Cruelty - Cursed");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty bestows a curse of deteroriation upon a failed fortitude save.");
                bp.m_Icon = CursedBuff.Icon;
                bp.m_Buff = CrueltyCursedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyCursed = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyCursed", bp => {
                bp.SetName("Cruelty - Cursed");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty bestows a curse of deteroriation upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityCursed.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var TouchOfProfaneCorruptionAbilityExhausted = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityExhausted", bp => {
                bp.SetName("Select Cruelty - Exhausted");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty forces exhaustion on the target upon a failed fortitude save.");
                bp.m_Icon = ExhaustedBuff.Icon;
                bp.m_Buff = CrueltyExhaustedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyExhausted = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyExhausted", bp => {
                bp.SetName("Cruelty - Exhausted");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty forces exhaustion on the target upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityExhausted.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddPrerequisite(Helpers.Create<PrerequisiteFeature>(c => {
                    c.m_Feature = CrueltyFatigued.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                }));
            });
            var TouchOfProfaneCorruptionAbilityFrightened = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityFrightened", bp => {
                bp.SetName("Select Cruelty - Frightened");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be frightened for 1 round/2 levels of dreadknight upon a failed fortitude save.");
                bp.m_Icon = FrightenedBuff.Icon;
                bp.m_Buff = CrueltyFrightenedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyFrightened = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyFrightened", bp => {
                bp.SetName("Cruelty - Frightened");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be frightened for 1 round/2 levels of dreadknight upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityFrightened.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddPrerequisite(Helpers.Create<PrerequisiteFeature>(c => {
                    c.m_Feature = CrueltyShaken.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                }));
            });
            var TouchOfProfaneCorruptionAbilityNauseated = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityNauseated", bp => {
                bp.SetName("Select Cruelty - Nauseated");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be nauseated for 1 round/3 levels of dreadknight upon a failed fortitude save.");
                bp.m_Icon = NauseatedBuff.Icon;
                bp.m_Buff = CrueltyNauseatedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyNauseated = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyNauseated", bp => {
                bp.SetName("Cruelty - Nauseated");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be nauseated for 1 round/3 levels of dreadknight upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityNauseated.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddPrerequisite(Helpers.Create<PrerequisiteFeature>(c => {
                    c.m_Feature = CrueltySickened.ToReference<BlueprintFeatureReference>();
                    c.HideInUI = true;
                }));
            });
            var TouchOfProfaneCorruptionAbilityPoisoned = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityPoisoned", bp => {
                bp.SetName("Select Cruelty - Poisoned");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be poisoned for 1 round/level of dreadknight upon a failed fortitude save.");
                bp.m_Icon = PoisonedBuff.Icon;
                bp.m_Buff = CrueltyPoisonedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyPoisoned = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyPoisoned", bp => {
                bp.SetName("Cruelty - Poisoned");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be poisoned for 1 round/level of dreadknight upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityPoisoned.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var TouchOfProfaneCorruptionAbilityBlinded = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityBlinded", bp => {
                bp.SetName("Select Cruelty - Blinded");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be become blinded for 1 round/level of dreadknight upon a failed fortitude save.");
                bp.m_Icon = BlindedBuff.Icon;
                bp.m_Buff = CrueltyBlindedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyBlinded = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyBlinded", bp => {
                bp.SetName("Cruelty - Blinded");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "\nThis cruelty causes the target to be become blinded for 1 round/level of dreadknight upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityBlinded.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var TouchOfProfaneCorruptionAbilityParalyzed = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityParalyzed", bp => {
                bp.SetName("Select Cruelty - Paralyzed");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be become paralyzed for 1 round upon a failed fortitude save.");
                bp.m_Icon = ParalyzedBuff.Icon;
                bp.m_Buff = CrueltyParalyzedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyParalyzed = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyParalyzed", bp => {
                bp.SetName("Cruelty - Paralyzed");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be become paralyzed for 1 round upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityParalyzed.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var TouchOfProfaneCorruptionAbilityStunned = Helpers.CreateBlueprint<BlueprintActivatableAbility>("TouchOfProfaneCorruptionAbilityStunned", bp => {
                bp.SetName("Select Cruelty - Stunned");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "This cruelty causes the target to be become stunned for 1 round/4 levels of dreadknight upon a failed fortitude save.");
                bp.m_Icon = StunnedBuff.Icon;
                bp.m_Buff = CrueltyStunnedBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateImmediately = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                bp.WeightInGroup = 1;
            });
            var CrueltyStunned = Helpers.CreateBlueprint<BlueprintFeature>("CrueltyStunned", bp => {
                bp.SetName("Cruelty - Stunned");
                bp.SetDescription("A dreadknight may enhance their Profane Corruption with learned cruelties. Only one cruelty may be selected at a time. " +
                    "\nThis cruelty causes the target to be become stunned for 1 round/4 levels of dreadknight upon a failed fortitude save.");
                bp.m_Icon = CrueltySelectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TouchOfProfaneCorruptionAbilityStunned.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var CrueltySelection1 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("CrueltySelection1", bp => {
                bp.SetName("Cruelty Selection");
                bp.SetDescription("At 3rd level, and every three levels thereafter, a dreadknight can select one cruelty. " +
                    "Each cruelty adds an effect to the dreadknight's profane corruption ability. Whenever the dreadknight uses " +
                    "any from of profane corruption to deal damage to one target, the target also receives the additional effect from one of the " +
                    "cruelties possessed by the dreadknight. The Dread Knight selects this cruelty before the attack. The target receives a Fortitude " +
                    "save to avoid this cruelty. If the save is successful, the target takes the damage as normal, but not the effects of the " +
                    "cruelty. The DC of this save is equal to 10 + 1/2 the dreadknight’s level + the dreadknight’s Charisma modifier.");
                bp.m_Icon = CrueltySelectIcon;
                bp.Ranks = 1;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                CrueltyFatigued.ToReference<BlueprintFeatureReference>(),
                CrueltySickened.ToReference<BlueprintFeatureReference>(),
                CrueltyShaken.ToReference<BlueprintFeatureReference>() };
                bp.m_Features = new BlueprintFeatureReference[] {
                CrueltyFatigued.ToReference<BlueprintFeatureReference>(),
                CrueltySickened.ToReference<BlueprintFeatureReference>(),
                CrueltyShaken.ToReference<BlueprintFeatureReference>() };
                bp.IsClassFeature = true;
            });
            var CrueltySelection2 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("CrueltySelection2", bp => {
                bp.SetName("Cruelty Selection");
                bp.SetDescription("At 3rd level, and every three levels thereafter, a dreadknight can select one cruelty. " +
                    "Each cruelty adds an effect to the dreadknight's touch of profane corruption ability. Whenever the dreadknight uses " +
                    "touch of profane corruption to deal damage to one target, the target also receives the additional effect from one of the " +
                    "cruelties possessed by the dreadknight. This choice is made when the touch is used. The target receives a Fortitude " +
                    "save to avoid this cruelty. If the save is successful, the target takes the damage as normal, but not the effects of the " +
                    "cruelty. The DC of this save is equal to 10 + 1/2 the dreadknight’s level + the dreadknight’s Charisma modifier.");
                bp.m_Icon = CrueltySelectIcon;
                bp.Ranks = 1;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                CrueltyDazed.ToReference<BlueprintFeatureReference>(),
                CrueltyDiseased.ToReference<BlueprintFeatureReference>(),
                CrueltyStaggered.ToReference<BlueprintFeatureReference>(),
                CrueltyFatigued.ToReference<BlueprintFeatureReference>(),
                CrueltySickened.ToReference<BlueprintFeatureReference>(),
                CrueltyShaken.ToReference<BlueprintFeatureReference>() };
                bp.m_Features = new BlueprintFeatureReference[]  {
                CrueltyDazed.ToReference<BlueprintFeatureReference>(),
                CrueltyDiseased.ToReference<BlueprintFeatureReference>(),
                CrueltyStaggered.ToReference<BlueprintFeatureReference>(),
                CrueltyFatigued.ToReference<BlueprintFeatureReference>(),
                CrueltySickened.ToReference<BlueprintFeatureReference>(),
                CrueltyShaken.ToReference<BlueprintFeatureReference>()};
                bp.IsClassFeature = true;
            });
            var CrueltySelection3 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("CrueltySelection3", bp => {
                bp.SetName("Cruelty Selection");
                bp.SetDescription("At 3rd level, and every three levels thereafter, a Dread Knight can select one cruelty. " +
                    "Each cruelty adds an effect to the dreadknight's touch of profane corruption ability. Whenever the dreadknight uses " +
                    "touch of profane corruption to deal damage to one target, the target also receives the additional effect from one of the " +
                    "cruelties possessed by the Dread Knight. This choice is made when the touch is used. The target receives a Fortitude " +
                    "save to avoid this cruelty. If the save is successful, the target takes the damage as normal, but not the effects of the " +
                    "cruelty. The DC of this save is equal to 10 + 1/2 the dreadknight’s level + the dreadknight’s Charisma modifier.");
                bp.m_Icon = CrueltySelectIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                CrueltyCursed.ToReference<BlueprintFeatureReference>(),
                CrueltyExhausted.ToReference<BlueprintFeatureReference>(),
                CrueltyFrightened.ToReference<BlueprintFeatureReference>(),
                CrueltyNauseated.ToReference<BlueprintFeatureReference>(),
                CrueltyPoisoned.ToReference<BlueprintFeatureReference>(),
                CrueltyDazed.ToReference<BlueprintFeatureReference>(),
                CrueltyDiseased.ToReference<BlueprintFeatureReference>(),
                CrueltyStaggered.ToReference<BlueprintFeatureReference>(),
                CrueltyFatigued.ToReference<BlueprintFeatureReference>(),
                CrueltySickened.ToReference<BlueprintFeatureReference>(),
                CrueltyShaken.ToReference<BlueprintFeatureReference>()};
                bp.m_Features = new BlueprintFeatureReference[] {
                CrueltyCursed.ToReference<BlueprintFeatureReference>(),
                CrueltyExhausted.ToReference<BlueprintFeatureReference>(),
                CrueltyFrightened.ToReference<BlueprintFeatureReference>(),
                CrueltyNauseated.ToReference<BlueprintFeatureReference>(),
                CrueltyPoisoned.ToReference<BlueprintFeatureReference>(),
                CrueltyDazed.ToReference<BlueprintFeatureReference>(),
                CrueltyDiseased.ToReference<BlueprintFeatureReference>(),
                CrueltyStaggered.ToReference<BlueprintFeatureReference>(),
                CrueltyFatigued.ToReference<BlueprintFeatureReference>(),
                CrueltySickened.ToReference<BlueprintFeatureReference>(),
                CrueltyShaken.ToReference<BlueprintFeatureReference>()};
                bp.IsClassFeature = true;
                bp.Ranks = 1;
            });
            var CrueltySelection4 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("CrueltySelection4", bp => {
                bp.SetName("Cruelty Selection");
                bp.SetDescription("At 3rd level, and every three levels thereafter, a dreadknight can select one cruelty. " +
                    "Each cruelty adds an effect to the dreadknight's touch of profane corruption ability. Whenever the Dread Knight uses " +
                    "profane corruption to deal damage to one target, the target also receives the additional effect from one of the " +
                    "cruelties possessed by the dreadknight. This choice is made when the touch is used. The target receives a Fortitude " +
                    "save to avoid this cruelty. If the save is successful, the target takes the damage as normal, but not the effects of the " +
                    "cruelty. The DC of this save is equal to 10 + 1/2 the dreadknight’s level + the dreadknight’s Charisma modifier.");
                bp.m_DescriptionShort = Helpers.CreateString("$CrueltySelection4.DescriptionShort", "At 3rd level, and every three levels thereafter, a dreadknight can select one cruelty. " +
                    "Each cruelty adds an effect to the dreadknight's touch of profane corruption ability. Whenever the dreadknight uses " +
                    "profane corruption to deal damage to one target, the target also receives the additional effect from one of the " +
                    "cruelties possessed by the dreadknight.");
                bp.m_Icon = CrueltySelectIcon;
                bp.Ranks = 1;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                CrueltyBlinded.ToReference<BlueprintFeatureReference>(),
                CrueltyParalyzed.ToReference<BlueprintFeatureReference>(),
                CrueltyStunned.ToReference<BlueprintFeatureReference>(),
                CrueltyCursed.ToReference<BlueprintFeatureReference>(),
                CrueltyExhausted.ToReference<BlueprintFeatureReference>(),
                CrueltyFrightened.ToReference<BlueprintFeatureReference>(),
                CrueltyNauseated.ToReference<BlueprintFeatureReference>(),
                CrueltyPoisoned.ToReference<BlueprintFeatureReference>(),
                CrueltyDazed.ToReference<BlueprintFeatureReference>(),
                CrueltyDiseased.ToReference<BlueprintFeatureReference>(),
                CrueltyStaggered.ToReference<BlueprintFeatureReference>(),
                CrueltyFatigued.ToReference<BlueprintFeatureReference>(),
                CrueltySickened.ToReference<BlueprintFeatureReference>(),
                CrueltyShaken.ToReference<BlueprintFeatureReference>()};
                bp.m_Features = new BlueprintFeatureReference[] {
                CrueltyBlinded.ToReference<BlueprintFeatureReference>(),
                CrueltyParalyzed.ToReference<BlueprintFeatureReference>(),
                CrueltyStunned.ToReference<BlueprintFeatureReference>(),
                CrueltyCursed.ToReference<BlueprintFeatureReference>(),
                CrueltyExhausted.ToReference<BlueprintFeatureReference>(),
                CrueltyFrightened.ToReference<BlueprintFeatureReference>(),
                CrueltyNauseated.ToReference<BlueprintFeatureReference>(),
                CrueltyPoisoned.ToReference<BlueprintFeatureReference>(),
                CrueltyDazed.ToReference<BlueprintFeatureReference>(),
                CrueltyDiseased.ToReference<BlueprintFeatureReference>(),
                CrueltyStaggered.ToReference<BlueprintFeatureReference>(),
                CrueltyFatigued.ToReference<BlueprintFeatureReference>(),
                CrueltySickened.ToReference<BlueprintFeatureReference>(),
                CrueltyShaken.ToReference<BlueprintFeatureReference>()};
                bp.IsClassFeature = true;
            });
            var MasteredCruelty = Helpers.CreateBlueprint<BlueprintFeature>("MasteredCruelty", bp => {
                bp.SetName("Cruel Master");
                bp.SetDescription("You have found a way to force dark powers to grant you the ability to apply one additional cruelty when using profane corruption. " +
                    "\nBenefit: You can select one additional cruelty to use with profane corruption.");
                bp.m_Icon = CrueltySelectIcon;
                bp.Groups = new FeatureGroup[] { FeatureGroup.MythicAbility };
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<IncreaseActivatableAbilityGroupSize>(c => {
                    c.Group = ActivatableAbilityGroup.MasterHealingTechnique;
                });
                bp.AddComponent<PrerequisiteFeature>(c => { c.m_Feature = TouchOfProfaneCorruptionFeature.ToReference<BlueprintFeatureReference>(); });
            });
            MythicAbilitySelection.m_AllFeatures = MythicAbilitySelection.m_AllFeatures.AppendToArray(MasteredCruelty.ToReference<BlueprintFeatureReference>());
            // Plague Bringer
            var PlagueIcon = AssetLoader.LoadInternal("Skills", "Icon_Plague.png");
            var PlagueBringer = Helpers.CreateBlueprint<BlueprintFeature>("PlagueBringer", bp => {
                bp.SetName("Plague Bringer");
                bp.SetDescription("At 3rd level, the powers of darkness make the dreadknight a beacon of corruption and disease. " +
                    "A dreadknight does not take any damage or take any penalty from diseases.");
                bp.m_Icon = PlagueIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<BuffDescriptorImmunity>(c => { c.Descriptor = SpellDescriptor.Disease; });
            });
            // Aura of Cowardice - NEEDS TESTING
            var AOCIcon = AssetLoader.LoadInternal("Skills", "Icon_AOC.png");
            var AuraOfCowardiceEffectBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfCowardiceEffectBuff", bp => {
                bp.SetName("Aura of Cowardice");
                bp.SetDescription("At 3rd level, a dreadknight radiates a palpably daunting aura that causes all enemies within 10 " +
                    "feet to take a –4 penalty on saving throws against fear effects. Creatures that are normally immune to fear lose that " +
                    "immunity while within 10 feet of a dreadknight with this ability. This ability functions only while the dreadknight remains " +
                    "conscious, not if he is unconscious or dead.");
                bp.IsClassFeature = true;
                bp.m_Icon = AOCIcon;
                bp.AddComponent<SavingThrowContextBonusAgainstDescriptor>(c => {
                    c.ModifierDescriptor = ModifierDescriptor.Penalty;
                    c.SpellDescriptor = SpellDescriptor.Fear;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Progression = ContextRankProgression.Custom;
                    c.m_Class = new BlueprintCharacterClassReference[] { DreadKnightClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_CustomProgression = new ContextRankConfig.CustomProgressionItem[] {
                        new ContextRankConfig.CustomProgressionItem() {
                            BaseValue = 7,
                            ProgressionValue = -4
                        },
                        new ContextRankConfig.CustomProgressionItem() {
                            BaseValue = 100,
                            ProgressionValue = -2
                        }
                    };
                });
                bp.AddComponent<SpellDescriptorImmunityIgnore>(c => {
                    c.Descriptor = SpellDescriptor.Fear | SpellDescriptor.Shaken | SpellDescriptor.Frightened;
                });

                bp.AddComponent<BuffDescriptorImmunityIgnore>(c => {
                    c.Descriptor = SpellDescriptor.Fear | SpellDescriptor.Shaken | SpellDescriptor.Frightened;
                });
            });
            var AuraOfCowardiceArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("AuraOfCowardiceArea", bp => {
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Enemy;
                bp.SpellResistance = false;
                bp.AggroEnemies = true;                
                bp.AffectEnemies = true;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = 13.Feet();
                bp.Fx = new PrefabLink();
                bp.AddComponent(AuraUtils.CreateUnconditionalAuraEffect(AuraOfCowardiceEffectBuff.ToReference<BlueprintBuffReference>()));
            });
            var AuraOfCowardiceBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfCowardiceBuff", bp => {
                bp.SetName("Aura of Cowardice");
                bp.SetDescription("At 3rd level, a dreadknight radiates a palpably daunting aura that causes all enemies within 10 " +
                    "feet to take a –4 penalty on saving throws against fear effects. Creatures that are normally immune to fear lose that " +
                    "immunity while within 10 feet of a dreadknight with this ability. This ability functions only while the Dread Knight remains " +
                    "conscious, not if he is unconscious or dead.");
                bp.m_Icon = AOCIcon;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = AuraOfCowardiceArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
            });
            var AuraOfCowardiceFeature = Helpers.CreateBlueprint<BlueprintFeature>("AuraOfCowardiceFeature", bp => {
                bp.SetName("Aura of Cowardice");
                bp.SetDescription("At 3rd level, a dreadknight radiates a palpably daunting aura that causes all enemies within 10 " +
                    "feet to take a –4 penalty on saving throws against fear effects. Creatures that are normally immune to fear lose that " +
                    "immunity while within 10 feet of a dreadknight with this ability. This ability functions only while the Dread Knight remains " +
                    "conscious, not if he is unconscious or dead.");
                bp.m_Icon = AOCIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AuraFeatureComponent>(c => {
                    c.m_Buff = AuraOfCowardiceBuff.ToReference<BlueprintBuffReference>();
                });
            });
            // Dreadknight Channel Negitive Energy
            var ChannelNegativeEnergy = Resources.GetBlueprint<BlueprintAbility>("89df18039ef22174b81052e2e419c728");
            var ChannelNegativeHeal = Resources.GetBlueprint<BlueprintAbility>("9be3aa47a13d5654cbcb8dbd40c325f2");
            var MythicChannelProperty = Resources.GetBlueprint<BlueprintUnitProperty>("152e61de154108d489ff34b98066c25c");
            var SelectiveChannel = Resources.GetBlueprint<BlueprintFeature>("fd30c69417b434d47b6b03b9c1f568ff");
            var DeathDomainGreaterLiving = Resources.GetBlueprint<BlueprintFeature>("fd7c08ccd3c7773458eb9613db3e93ad");
            var LifeDominantSoul = Resources.GetBlueprint<BlueprintFeature>("8f58b4029511b5345981ffaf1da5ea2e");
            var ExtraChannel = Resources.GetBlueprint<BlueprintFeature>("cd9f19775bd9d3343a31a065e93f0c47");
            var ChannelEnergyFact = Resources.GetBlueprint<BlueprintUnitFact>("93f062bc0bf70e84ebae436e325e30e8");
            var HealTargetFX = new PrefabLink() { AssetId = "9a38d742801be084d89bd34318c600e8" };
            var DreadKnightChannelNegativeEnergyAbility = Helpers.CreateBlueprint<BlueprintAbility>("DreadKnightChannelNegativeEnergyAbility", bp => {
                bp.SetName("Channel Negative Energy - Damage Living");
                bp.SetDescription("Channeling negative energy causes a burst that damages every living creature in a 30-foot radius centered on the Dread Knight. The amount of damage " +
                    "inflicted is equal to 1d6 points of damage plus 1d6 points of damage for every two Dread Knight levels beyond 1st (2d6 at 3rd, 3d6 at 5th, and so on). " +
                    "Creatures that take damage from channeled energy receive a Will save to halve the damage. " +
                    "The DC of this save is equal to 10 + 1/2 the Dread Knight's level + the Dread Knight's Charisma modifier.");
                bp.m_DescriptionShort = Helpers.CreateString("$DreadKnightChannelNegativeEnergyAbility.DescriptionShort", "Channeling negative energy causes a burst that damages " +
                    "every creature in a 30-foot radius centered on the Dread Knight. The amount of damage " +
                    "inflicted is equal to 1d6 points of damage plus 1d6 points of damage for every two Dread Knight levels beyond 1st (2d6 at 3rd, 3d6 at 5th, and so on). " +
                    "Creatures that take damage from channeled energy receive a Will save to halve the damage.");
                bp.m_Icon = ChannelNegativeEnergy.Icon;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Heighten | Metamagic.Quicken;
                bp.Range = AbilityRange.Personal;
                bp.Type = AbilityType.Special;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.Harmful;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.ResourceAssetIds = ChannelNegativeEnergy.ResourceAssetIds;
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 2;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 30.Feet();
                    c.m_TargetType = TargetType.Any;
                    c.m_Condition = new ConditionsChecker() {
                        Conditions = new Condition[0]
                    };
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = ChannelNegativeEnergy.GetComponent<AbilitySpawnFx>().PrefabLink;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = new SpellDescriptorWrapper(SpellDescriptor.ChannelNegativeHarm);
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Progression = ContextRankProgression.OnePlusDiv2;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_Min = 0;
                    c.m_UseMin = false;
                    c.m_Class = new BlueprintCharacterClassReference[] { DreadKnightClass.ToReference<BlueprintCharacterClassReference>()};
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_CustomProperty = MythicChannelProperty.ToReference<BlueprintUnitPropertyReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Min = 0;
                    c.m_UseMin = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        m_Fact = NegativeEnergyAffinity.ToReference<BlueprintUnitFactReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = SelectiveChannel.ToReference<BlueprintUnitFactReference>()
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new Conditional() {
                                            ConditionsChecker = new ConditionsChecker() {
                                                Conditions = new Condition[] {
                                                    new ContextConditionIsEnemy() {
                                                        Not = false
                                                    },
                                                }
                                            },
                                            IfTrue = Helpers.CreateActionList(
                                                new ContextActionSavingThrow() {
                                                    m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                                    Type = SavingThrowType.Will,
                                                    CustomDC = new ContextValue(),
                                                    Actions = Helpers.CreateActionList(
                                                        new ContextActionDealDamage() {
                                                            DamageType = new DamageTypeDescription() {
                                                                Type = DamageType.Energy,
                                                                Common = new DamageTypeDescription.CommomData(),
                                                                Physical = new DamageTypeDescription.PhysicalData(),
                                                                Energy = DamageEnergyType.NegativeEnergy
                                                            },
                                                            Duration = new ContextDurationValue() {
                                                                m_IsExtendable = true,
                                                                DiceCountValue = new ContextValue(),
                                                                BonusValue = new ContextValue()
                                                            },
                                                            Value = new ContextDiceValue() {
                                                                DiceType = Kingmaker.RuleSystem.DiceType.D6,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    ValueRank = AbilityRankType.DamageDice
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    ValueRank = AbilityRankType.DamageBonus
                                                                }
                                                            },
                                                            IsAoE = true,
                                                            HalfIfSaved = true
                                                        }
                                                    )
                                                }
                                            ),
                                            IfFalse = Helpers.CreateActionList()
                                        }
                                    ),
                                    IfFalse = Helpers.CreateActionList(
                                        new Conditional() {
                                            ConditionsChecker = new ConditionsChecker() {
                                                Conditions = new Condition[] {
                                                    new ContextConditionIsCaster() {
                                                        Not = true
                                                    }
                                                }
                                            },
                                            IfTrue = Helpers.CreateActionList(
                                                new ContextActionSavingThrow() {
                                                    m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                                                    Type = SavingThrowType.Will,
                                                    CustomDC = new ContextValue(),
                                                    Actions = Helpers.CreateActionList(
                                                        new ContextActionDealDamage() {
                                                            DamageType = new DamageTypeDescription() {
                                                                Type = DamageType.Energy,
                                                                Common = new DamageTypeDescription.CommomData(),
                                                                Physical = new DamageTypeDescription.PhysicalData(),
                                                                Energy = DamageEnergyType.NegativeEnergy
                                                            },
                                                            Duration = new ContextDurationValue() {
                                                                m_IsExtendable = true,
                                                                DiceCountValue = new ContextValue(),
                                                                BonusValue = new ContextValue()
                                                            },
                                                            Value = new ContextDiceValue() {
                                                                DiceType = Kingmaker.RuleSystem.DiceType.D6,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    ValueRank = AbilityRankType.DamageDice
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    ValueRank = AbilityRankType.DamageBonus
                                                                }
                                                            },
                                                            IsAoE = true,
                                                            HalfIfSaved = true
                                                        }
                                                    )
                                                }
                                            ),
                                            IfFalse = Helpers.CreateActionList()
                                        }
                                    )
                                }
                            ),
                        }                        
                    );
                });
            });
            var DreadKnightChannelNegativeHealAbility = Helpers.CreateBlueprint<BlueprintAbility>("DreadKnightChannelNegativeHealAbility", bp => {
                bp.SetName("Channel Negative Energy - Heal Undead");
                bp.SetDescription("Channeling negative energy causes a burst that heals every undead creature in a 30-foot radius centered on the Dread Knight. The amount of damage " +
                    "healed is equal to 1d6 points of damage plus 1d6 points of damage for every two Dread Knight levels beyond 1st (2d6 at 3rd, 3d6 at 5th, and so on).");
                bp.m_DescriptionShort = Helpers.CreateString("$DreadKnightChannelNegativeEnergyAbility.DescriptionShort", "Channeling negative energy causes a burst that heals every undead " +
                    "creature in a 30-foot radius centered on the Dread Knight. The amount of damage " +
                    "healed is equal to 1d6 points of damage plus 1d6 points of damage for every two Dread Knight levels beyond 1st (2d6 at 3rd, 3d6 at 5th, and so on).");
                bp.m_Icon = ChannelNegativeHeal.Icon;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Heighten | Metamagic.Quicken;
                bp.Range = AbilityRange.Personal;
                bp.Type = AbilityType.Special;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.Harmful;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.ResourceAssetIds = ChannelNegativeHeal.ResourceAssetIds;
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 2;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 30.Feet();
                    c.m_TargetType = TargetType.Any;
                    c.m_Condition = new ConditionsChecker() {
                        Conditions = new Condition[0]
                    };
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = ChannelNegativeHeal.GetComponent<AbilitySpawnFx>().PrefabLink;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Progression = ContextRankProgression.OnePlusDiv2;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_Min = 0;
                    c.m_UseMin = false;
                    c.m_Class = new BlueprintCharacterClassReference[] { DreadKnightClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_CustomProperty = MythicChannelProperty.ToReference<BlueprintUnitPropertyReference>();
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Min = 0;
                    c.m_UseMin = true;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Heal;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.D6,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None,
                            m_AbilityParameter = AbilityParameterType.Level
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.DamageBonus,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None,
                            m_AbilityParameter = AbilityParameterType.Level
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.StatBonus;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.D6,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None,
                            m_AbilityParameter = AbilityParameterType.Level
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Shared,
                            Value = 0,
                            ValueRank = AbilityRankType.StatBonus,
                            ValueShared = AbilitySharedValue.Heal,
                            Property = UnitProperty.None,
                            m_AbilityParameter = AbilityParameterType.Level
                        }
                    };
                    c.Modifier = 0.5;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        m_Fact = NegativeEnergyAffinity.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    },
                                    new ContextConditionHasFact() {
                                        m_Fact = DeathDomainGreaterLiving.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = SelectiveChannel.ToReference<BlueprintUnitFactReference>()
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new Conditional() {
                                            ConditionsChecker = new ConditionsChecker() {
                                                Operation = Operation.And,
                                                Conditions = new Condition[] {
                                                    new ContextConditionIsAlly() {
                                                        Not = false
                                                    },
                                                    new ContextConditionHasFact() {
                                                        m_Fact = LifeDominantSoul.ToReference<BlueprintUnitFactReference>(),
                                                        Not = true
                                                    }
                                                }
                                            },
                                            IfTrue = Helpers.CreateActionList(
                                                new ContextActionHealTarget() {
                                                    Value = new ContextDiceValue() {
                                                        DiceType = DiceType.Zero,
                                                        DiceCountValue = new ContextValue() {
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 0,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Damage,
                                                            Property = UnitProperty.None,
                                                            m_AbilityParameter = AbilityParameterType.Level
                                                        },
                                                        BonusValue = new ContextValue() {
                                                            ValueType = ContextValueType.Shared,
                                                            Value = 0,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Heal,
                                                            Property = UnitProperty.None,
                                                            m_AbilityParameter = AbilityParameterType.Level
                                                        }
                                                    }
                                                },
                                                new ContextActionSpawnFx() {
                                                    PrefabLink = HealTargetFX
                                                }
                                                ),
                                            IfFalse = Helpers.CreateActionList(
                                                new Conditional() {
                                                    ConditionsChecker = new ConditionsChecker() {
                                                        Operation = Operation.And,
                                                        Conditions = new Condition[] {
                                                            new ContextConditionIsAlly() {
                                                                Not = false
                                                            },
                                                            new ContextConditionHasFact() {
                                                                m_Fact = LifeDominantSoul.ToReference<BlueprintUnitFactReference>(),
                                                                Not = false
                                                            }
                                                        }
                                                    },
                                                    IfTrue = Helpers.CreateActionList(
                                                        new ContextActionHealTarget() {
                                                            Value = new ContextDiceValue() {
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Simple,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.Damage,
                                                                    Property = UnitProperty.None,
                                                                    m_AbilityParameter = AbilityParameterType.Level
                                                                },
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Shared,
                                                                    Value = 0,
                                                                    ValueRank = AbilityRankType.Default,
                                                                    ValueShared = AbilitySharedValue.StatBonus,
                                                                    Property = UnitProperty.None,
                                                                    m_AbilityParameter = AbilityParameterType.Level
                                                                }
                                                            }
                                                        },
                                                        new ContextActionSpawnFx() {
                                                            PrefabLink = HealTargetFX
                                                        }
                                                        ),
                                                    IfFalse = Helpers.CreateActionList()
                                                }
                                                )
                                        }
                                    ),
                                    IfFalse = Helpers.CreateActionList(
                                        new Conditional() {
                                            ConditionsChecker = new ConditionsChecker() {
                                                Conditions = new Condition[] {
                                                    new ContextConditionHasFact() {
                                                        m_Fact = LifeDominantSoul.ToReference<BlueprintUnitFactReference>(),
                                                        Not = false
                                                    }
                                                }
                                            },
                                            IfTrue = Helpers.CreateActionList(
                                                new ContextActionHealTarget() {
                                                    Value = new ContextDiceValue() {
                                                        DiceType = DiceType.Zero,
                                                        DiceCountValue = new ContextValue() {
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 0,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Damage,
                                                            Property = UnitProperty.None,
                                                            m_AbilityParameter = AbilityParameterType.Level
                                                        },
                                                        BonusValue = new ContextValue() {
                                                            ValueType = ContextValueType.Shared,
                                                            Value = 0,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.StatBonus,
                                                            Property = UnitProperty.None,
                                                            m_AbilityParameter = AbilityParameterType.Level
                                                        }
                                                    }
                                                },
                                                new ContextActionSpawnFx() {
                                                    PrefabLink = HealTargetFX
                                                }
                                                ),
                                            IfFalse = Helpers.CreateActionList(
                                                new ContextActionHealTarget() {
                                                    Value = new ContextDiceValue() {
                                                        DiceType = DiceType.Zero,
                                                        DiceCountValue = new ContextValue() {
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 0,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Damage,
                                                            Property = UnitProperty.None,
                                                            m_AbilityParameter = AbilityParameterType.Level
                                                        },
                                                        BonusValue = new ContextValue() {
                                                            ValueType = ContextValueType.Shared,
                                                            Value = 0,
                                                            ValueRank = AbilityRankType.Default,
                                                            ValueShared = AbilitySharedValue.Heal,
                                                            Property = UnitProperty.None,
                                                            m_AbilityParameter = AbilityParameterType.Level
                                                        }
                                                    }
                                                },
                                                new ContextActionSpawnFx() {
                                                    PrefabLink = HealTargetFX
                                                }
                                                )
                                        }
                                    )
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(),
                        }
                    );
                });
                bp.AddComponent<AbilityUseOnRest>(c => {
                    c.Type = AbilityUseOnRestType.HealMassUndead;
                    c.BaseValue = 10;
                    c.AddCasterLevel = true;
                    c.MultiplyByCasterLevel = false;
                    c.MaxCasterLevel = 0;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.RestoreHP | SpellDescriptor.ChannelNegativeHeal;
                });
            });
            var DreadKnightChannelNegativeEnergyFeature = Helpers.CreateBlueprint<BlueprintFeature>("DreadKnightChannelNegativeEnergyFeature", bp => {
                bp.SetName("Channel Negative Energy");
                bp.SetDescription("When a Dread Knight reaches 4th level, he gains the supernatural ability to channel negative " +
                    "energy like a cleric. Using this ability consumes two uses of his touch of corruption ability. A Dread Knight " +
                    "uses his level as his effective cleric level when channeling negative energy. This is a Charisma-based ability..");
                bp.m_DescriptionShort = Helpers.CreateString("$DreadKnightChannelNegativeEnergyFeature.DescriptionShort", "Channeling negative energy causes a burst that damages every creature in a 30-foot radius centered on the Dread Knight. The amount of damage " +
                    "inflicted is equal to 1d6 points of damage plus 1d6 points of damage for every two Dread Knight levels beyond 1st (2d6 at 3rd, 3d6 at 5th, and so on). " +
                    "Creatures that take damage from channeled energy receive a Will save to halve the damage.");
                bp.m_Icon = ChannelNegativeEnergy.Icon;
                bp.Ranks = 1;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ChannelEnergyFact.ToReference<BlueprintUnitFactReference>(),
                        DreadKnightChannelNegativeEnergyAbility.ToReference<BlueprintUnitFactReference>(),
                        DreadKnightChannelNegativeHealAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
            });
            SelectiveChannel.AddPrerequisiteFeature(DreadKnightChannelNegativeEnergyFeature, Prerequisite.GroupType.Any);
            ExtraChannel.AddPrerequisiteFeature(DreadKnightChannelNegativeEnergyFeature, Prerequisite.GroupType.Any);
            #endregion
            #region Profane Bond
            var WeaponBondProgression = Resources.GetBlueprint<BlueprintProgression>("e08a817f475c8794aa56fdd904f43a57");
            WeaponBondProgression.m_DisplayName = Helpers.CreateString("$WeaponBondSwitchAbility.DisplayName", "Divine/Profane Weapon Bond");
            WeaponBondProgression.m_Description = Helpers.CreateString("$WeaponBondSwitchAbility.Description", "Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
                "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
                "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
                "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
                "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
                "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
                "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
                "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
                "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
                "levels beyond 5th, to a total of four times per day at 17th level.");
            var AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");
            var MountTargetFeature = Resources.GetBlueprint<BlueprintFeature>("cb06f0e72ffb5c640a156bd9f8000c1d");
            var Vicious = Resources.GetBlueprint<BlueprintItemEnchantment>("a1455a289da208144981e4b1ef92cc56");
            var Vorpal = Resources.GetBlueprint<BlueprintItemEnchantment>("2f60bfcba52e48a479e4a69868e24ebc");
            var Unholy = Resources.GetBlueprint<BlueprintItemEnchantment>("d05753b8df780fc4bb55b318f06af453");
            var Anarchic = Resources.GetBlueprint<BlueprintItemEnchantment>("57315bc1e1f62a741be0efde688087e9");
            var TemplateFiendish = Resources.GetModBlueprint<BlueprintFeature>("TemplateFiendish");
            var WeaponBondFeature = Resources.GetBlueprint<BlueprintFeature>("1c7cdc1605554954f838d85bbdd22d90");
            var WeaponBondPlus3 = Resources.GetBlueprint<BlueprintFeature>("d2f45a2034d4f7643ba1a450bc5c4c06");
            var WeaponBondPlus4 = Resources.GetBlueprint<BlueprintFeature>("6d73f49b602e29a43a6faa2ea1e4a425");
            var WeaponBondPlus5 = Resources.GetBlueprint<BlueprintFeature>("f17c3ba33bb44d44782cb3851d823011");
            var WeaponBondPlus6 = Resources.GetBlueprint<BlueprintFeature>("b936ee90c070edb46bd76025dc1c5936");
            var AnimalCompanionArchetypeSelection = Resources.GetBlueprint<BlueprintFeature>("65af7290b4efd5f418132141aaa36c1b");
            var ProfaneWeaponIcon = AssetLoader.LoadInternal("Skills", "Icon_ProfaneWeapon.png");
            var WeaponBondAdditionalUse = Resources.GetBlueprint<BlueprintFeature>("5a64de5435667da4eae2e4c95ec87917");
            var WeaponBondAxiomaticChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("d76e8a80ab14ac942b6a9b8aaa5860b1");
            var WeaponBondFlamingBurstChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("3af19bdbd6215434f8421a860cc98363");
            var WeaponBondSwitchAbility = Resources.GetBlueprint<BlueprintAbility>("7ff088ab58c69854b82ea95c2b0e35b4");
            //WeaponBondSwitchAbility.m_DisplayName = Helpers.CreateString("$WeaponBondSwitchAbility.DisplayName", "Divine/Profane Weapon Bond");
            //WeaponBondSwitchAbility.m_Description = Helpers.CreateString("$WeaponBondSwitchAbility.Description", "Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
            //    "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
            //    "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
            //    "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
            //    "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
            //    "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
            //    "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
            //    "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
            //    "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
            //    "levels beyond 5th, to a total of four times per day at 17th level.");
            //WeaponBondSwitchAbility.RemoveComponents<ContextRankConfig>();
            //WeaponBondSwitchAbility.AddComponent<ContextRankConfig>(c => {
            //    c.m_BaseValueType = ContextRankBaseValueType.CharacterLevel;
            //    c.m_Max = 20;
            //});
            WeaponBondSwitchAbility.GetComponent<ContextRankConfig>().TemporaryContext(c => {
                c.m_Class = c.m_Class.AppendToArray(DreadKnightClass.ToReference<BlueprintCharacterClassReference>());
                c.m_UseMax = true;
                c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
            });
            WeaponBondSwitchAbility.RemoveComponents<AbilityCasterAlignment>();
            WeaponBondSwitchAbility.AddComponent<AbilityCasterAlignment>(c => {
                c.Alignment = AlignmentMaskType.Any;
            });
            //var WeaponBondDurationBuff = Resources.GetBlueprint<BlueprintBuff>("bf570774501886f47b395a4bfe75eeb2");
            //WeaponBondDurationBuff.m_DisplayName = Helpers.CreateString("$WeaponBondSwitchAbility.DisplayName", "Divine/Profane Weapon Bond");
            //WeaponBondDurationBuff.m_Description = Helpers.CreateString("$WeaponBondSwitchAbility.Description", "Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
            //    "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
            //    "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
            //    "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
            //    "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
            //    "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
            //    "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
            //    "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
            //    "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
            //    "levels beyond 5th, to a total of four times per day at 17th level.");
            var WeaponBondResourse = Resources.GetBlueprint<BlueprintAbilityResource>("3683d1af071c1744185ff93cba9db10b");
            //WeaponBondFlamingBurstChoice.SetName("Divine/Profane Weapon Bond - Flaming Burst");
            //WeaponBondFlamingBurstChoice.SetDescription("A paladin/dread knight can add the flaming burst property to a weapon enhanced with her divine/profane weapon bond, " +
            //    "but this consumes 2 points of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon. A flaming burst weapon functions as a " +
            //    "flaming weapon that also explodes with flame upon striking a successful {g|Encyclopedia:Critical}critical hit{/g}. The fire does not harm " +
            //    "the wielder. In addition to the extra {g|Encyclopedia:Dice}1d6{/g} {g|Encyclopedia:Energy_Damage}fire damage{/g} from the flaming ability, a " +
            //    "flaming burst weapon deals an extra 1d10 points of fire {g|Encyclopedia:Damage}damage{/g} on a successful critical hit. If the weapon's " +
            //    "critical multiplier is ?3, add an extra 2d10 points of fire damage instead, and if the multiplier is ?4, add an extra 3d10 points of fire damage.");
            var WeaponBondBrilliantEnergyChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("f1eec5cc68099384cbfc6964049b24fa");
            //WeaponBondBrilliantEnergyChoice.SetName("Divine/Profane Weapon Bond - Brilliant Energy");
            //WeaponBondBrilliantEnergyChoice.SetDescription("A paladin/dread knight can add the brilliant energy property to a weapon enhanced with her divine/profane weapon bond, " +
            //    "but this consumes 4 points of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon.\nA brilliant energy weapon ignores nonliving matter. " +
            //    "Armor and shield bonuses to {g|Encyclopedia:Armor_Class}AC{/g} (including any enhancement bonuses to that armor) do not count against it because the weapon " +
            //    "passes through armor. ({g|Encyclopedia:Dexterity}Dexterity{/g}, deflection, dodge, natural armor, and other such bonuses still apply.) A brilliant energy " +
            //    "weapon cannot harm undead, constructs, or objects.");
            //WeaponBondAdditionalUse.SetName("Divine/Profane Weapon Bond - Additional Use");
            //WeaponBondAdditionalUse.SetDescription("Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
            //    "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
            //    "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
            //    "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
            //    "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
            //    "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
            //    "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
            //    "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
            //    "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
            //    "levels beyond 5th, to a total of four times per day at 17th level.");
            //var WeaponBondKeenChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("27d76f1afda08a64d897cc81201b5218");
            //WeaponBondKeenChoice.SetName("Divine/Profane Weapon Bond - Keen");
            //WeaponBondKeenChoice.SetDescription("A paladin/dread knight can add the keen property to a weapon enhanced with her divine/profane weapon bond, " +
            //    "but this consumes 1 point of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon.\nThe keen property doubles the " +
            //    "threat range of a weapon. This benefit doesn't stack with any other effects that expand the threat range of a weapon " +
            //    "(such as the keen edge {g|Encyclopedia:Spell}spell{/g} or the Improved {g|Encyclopedia:Critical}Critical{/g} {g|Encyclopedia:Feat}feat{/g}).");
            //var WeaponBondSpeedChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("ed1ef581af9d9014fa1386216b31cdae");
            //WeaponBondSpeedChoice.SetName("Divine/Profane Weapon Bond - Speed");
            //WeaponBondSpeedChoice.SetDescription("A paladin/dread knight can add the {g|Encyclopedia:Speed}speed{/g} property to a weapon " +
            //    "enhanced with her divine/profane weapon bond, but this consumes 3 points of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to " +
            //    "this weapon.\nWhen making a full {g|Encyclopedia:Attack}attack{/g}, the wielder of a speed weapon may make one extra attack with it. " +
            //    "The attack uses the wielder's full {g|Encyclopedia:BAB}base attack bonus{/g}, plus any modifiers appropriate to the situation. " +
            //    "(This benefit is not cumulative with similar effects, such as a haste {g|Encyclopedia:Spell}spell{/g}.)");
            //var WeaponBondFlamingChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("7902941ef70a0dc44bcfc174d6193386");
            //WeaponBondFlamingChoice.SetName("Divine/Profane Weapon Bond - Flaming");
            //WeaponBondFlamingChoice.SetDescription("A paladin/dread knight can add the flaming property to a weapon enhanced with her divine/profane weapon bond, " +
            //    "but this consumes 1 point of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon.\nA flaming weapon is sheathed in " +
            //    "fire that deals an extra {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} on a successful hit. " +
            //    "The fire does not harm the wielder.");
            //WeaponBondAxiomaticChoice.SetName("Divine/Profane Weapon Bond - Axiomatic");
            //WeaponBondAxiomaticChoice.SetDescription("A paladin/dread knight can add the axiomatic property to a weapon enhanced with her divine/profane weapon bond, " +
            //    "but this consumes 2 points of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon.\nAn axiomatic weapon is infused with " +
            //    "lawful power. It makes the weapon {g|Encyclopedia:Alignment}lawful-aligned{/g} and thus overcomes the corresponding {g|Encyclopedia:Damage_Reduction}damage " +
            //    "reduction{/g}. It deals an extra {g|Encyclopedia:Dice}2d6{/g} points of {g|Encyclopedia:Damage}damage{/g} against chaotic creatures.");
            var VorpalIcon = AssetLoader.LoadInternal("Skills", "Icon_Vorpal.png");
            var ViciousIcon = AssetLoader.LoadInternal("Skills", "Icon_Vicious.png");
            var FiendishWeaponViciousBuff = Helpers.CreateBlueprint<BlueprintBuff>("FiendishWeaponViciousBuff", bp => {
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Stack;
                bp.AddComponent<AddBondProperty>(c => {
                    c.m_Enchant = Vicious.ToReference<BlueprintItemEnchantmentReference>();
                    c.EnchantPool = EnchantPoolType.DivineWeaponBond;
                });
            });
            var FiendishWeaponViciousChoice = Helpers.CreateBlueprint<BlueprintActivatableAbility>("FiendishWeaponViciousChoice", bp => {
                bp.SetName("Profane Weapon Bond - Vicious");
                bp.SetDescription("A dreadknight can add the vicious property to a weapon enhanced with her profane weapon bond, but this " +
                    "consumes 1 points of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon.\nWhen a vicious weapon strikes an " +
                    "opponent, it creates a flash of disruptive energy that resonates between the opponent and the wielder. This energy deals an " +
                    "extra {g|Encyclopedia:Dice}2d6{/g} points of {g|Encyclopedia:Damage}damage{/g} to the opponent and 1d6 points of damage to the wielder.");
                bp.m_Icon = ViciousIcon;
                bp.Group = ActivatableAbilityGroup.DivineWeaponProperty;
                bp.WeightInGroup = 1;
                bp.DeactivateImmediately = true;
                bp.m_Buff = FiendishWeaponViciousBuff.ToReference<BlueprintBuffReference>();
            });
            var FiendishWeaponVorpalBuff = Helpers.CreateBlueprint<BlueprintBuff>("FiendishWeaponVorpalBuff", bp => {
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Stack;
                bp.AddComponent<AddBondProperty>(c => {
                    c.m_Enchant = Vorpal.ToReference<BlueprintItemEnchantmentReference>();
                    c.EnchantPool = EnchantPoolType.DivineWeaponBond;
                });
            });
            var FiendishWeaponVorpalChoice = Helpers.CreateBlueprint<BlueprintActivatableAbility>("FiendishWeaponVorpalChoice", bp => {
                bp.SetName("Profane Weapon Bond - Vorpal");
                bp.SetDescription("A dreadknight can add the vorpal property to a weapon enhanced with her profane weapon bond, but this " +
                    "consumes 5 points of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon.\nA vorpal weapon is infused with " +
                    "terrifying power. It allows the weapon to sever the heads of those it strikes.\nUpon a {g|Encyclopedia:Dice}roll{/g} of natural " +
                    "20 (followed by a successful roll to confirm the {g|Encyclopedia:Critical}critical hit{/g}), the weapon severs the opponent's " +
                    "head (if it has one) from its body. Some creatures, such as many aberrations and all oozes, have no heads. Others, " +
                    "such as golems and undead creatures other than vampires, are not affected by the loss of their heads. Most other creatures, " +
                    "however, die when their heads are cut off.");
                bp.m_Icon = VorpalIcon;
                bp.Group = ActivatableAbilityGroup.DivineWeaponProperty;
                bp.WeightInGroup = 4;
                bp.DeactivateImmediately = true;
                bp.m_Buff = FiendishWeaponVorpalBuff.ToReference<BlueprintBuffReference>();
            });
            var FiendishWeaponAnarchicBuff = Helpers.CreateBlueprint<BlueprintBuff>("FiendishWeaponAnarchicBuff", bp => {
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Stack;
                bp.AddComponent<AddBondProperty>(c => {
                    c.m_Enchant = Anarchic.ToReference<BlueprintItemEnchantmentReference>();
                    c.EnchantPool = EnchantPoolType.DivineWeaponBond;
                });
            });
            var SacredWeaponAnarchicChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("6fdc32d0af41ffb42b8285dbac9a050a");
            var FiendishWeaponAnarchicChoice = Helpers.CreateBlueprint<BlueprintActivatableAbility>("FiendishWeaponAnarchicChoice", bp => {
                bp.SetName("Profane Weapon Bond - Anarchic");
                bp.SetDescription("A dreadknight can add the anarchic property to a weapon enhanced with her profane weapon bond, but this " +
                    "consumes 2 points of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon.\nAn anarchic weapon is infused with " +
                    "chaotic power. It makes the weapon {g|Encyclopedia:Alignment}chaotic-aligned{/g} and thus overcomes the corresponding " +
                    "{g|Encyclopedia:Damage_Reduction}damage reduction{/g}. It deals an extra {g|Encyclopedia:Dice}2d6{/g} points of " +
                    "{g|Encyclopedia:Damage}damage{/g} against lawful creatures.");
                bp.m_Icon = SacredWeaponAnarchicChoice.Icon;
                bp.Group = ActivatableAbilityGroup.DivineWeaponProperty;
                bp.WeightInGroup = 2;
                bp.DeactivateImmediately = true;
                bp.m_Buff = FiendishWeaponAnarchicBuff.ToReference<BlueprintBuffReference>();
            });
            var HellknightUnholyAbility = Resources.GetBlueprint<BlueprintActivatableAbility>("a68cd0fbf5d21ef4f8b9375ec0ac53b9");
            var FiendishWeaponUnholyBuff = Helpers.CreateBlueprint<BlueprintBuff>("FiendishWeaponUnholyBuff", bp => {
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Stack;
                bp.AddComponent<AddBondProperty>(c => {
                    c.m_Enchant = Unholy.ToReference<BlueprintItemEnchantmentReference>();
                    c.EnchantPool = EnchantPoolType.DivineWeaponBond;
                });
            });
            var FiendishWeaponUnholyChoice = Helpers.CreateBlueprint<BlueprintActivatableAbility>("FiendishWeaponUnholyChoice", bp => {
                bp.SetName("Profane Weapon Bond - Unholy");
                bp.SetDescription("A dreadknight can add the unholy property to a weapon enhanced with her profane weapon bond, but this consumes " +
                    "2 points of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon.\nAn unholy weapon is imbued with unholy power. " +
                    "This power makes the weapon evil-aligned and thus overcomes the corresponding {g|Encyclopedia:Damage_Reduction}damage reduction{/g}. " +
                    "It deals an extra {g|Encyclopedia:Dice}2d6{/g} points of {g|Encyclopedia:Damage}damage{/g} against all creatures of " +
                    "good {g|Encyclopedia:Alignment}alignment{/g}.");
                bp.m_Icon = HellknightUnholyAbility.Icon;
                bp.Group = ActivatableAbilityGroup.DivineWeaponProperty;
                bp.WeightInGroup = 2;
                bp.DeactivateImmediately = true;
                bp.m_Buff = FiendishWeaponUnholyBuff.ToReference<BlueprintBuffReference>();
            });
            var FiendishWeaponBondFeature = Helpers.CreateBlueprint<BlueprintFeature>("FiendishWeaponBondFeature", bp => {
                bp.SetName("Profane Weapon Bond (+1)");
                bp.SetDescription("Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
                "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
                "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
                "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
                "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
                "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
                "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
                "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
                "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
                "levels beyond 5th, to a total of four times per day at 17th level.");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.ComponentsArray = WeaponBondFeature.ComponentsArray;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { FiendishWeaponViciousChoice.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var FiendishWeaponBondPlus2 = Helpers.CreateBlueprint<BlueprintFeature>("FiendishWeaponBondPlus2", bp => {
                bp.SetName("Profane Weapon Bond (+2)");
                bp.SetDescription("Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
                "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
                "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
                "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
                "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
                "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
                "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
                "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
                "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
                "levels beyond 5th, to a total of four times per day at 17th level.");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<IncreaseActivatableAbilityGroupSize>(c => { c.Group = ActivatableAbilityGroup.DivineWeaponProperty; });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[4] { FiendishWeaponUnholyChoice.ToReference<BlueprintUnitFactReference>(),
                        WeaponBondAxiomaticChoice.ToReference<BlueprintUnitFactReference>(), FiendishWeaponAnarchicChoice.ToReference<BlueprintUnitFactReference>(),
                        WeaponBondFlamingBurstChoice.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var FiendishWeaponBondPlus3 = Helpers.CreateBlueprint<BlueprintFeature>("FiendishWeaponBondPlus3", bp => {
                bp.SetName("Profane Weapon Bond (+3)");
                bp.SetDescription("Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
                "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
                "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
                "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
                "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
                "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
                "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
                "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
                "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
                "levels beyond 5th, to a total of four times per day at 17th level.");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.ComponentsArray = WeaponBondPlus3.ComponentsArray;
            });
            var FiendishWeaponBondPlus4 = Helpers.CreateBlueprint<BlueprintFeature>("FiendishWeaponBondPlus4", bp => {
                bp.SetName("Profane Weapon Bond (+4)");
                bp.SetDescription("Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
                "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
                "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
                "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
                "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
                "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
                "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
                "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
                "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
                "levels beyond 5th, to a total of four times per day at 17th level.");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.ComponentsArray = WeaponBondPlus4.ComponentsArray;
            });
            var FiendishWeaponBondPlus5 = Helpers.CreateBlueprint<BlueprintFeature>("FiendishWeaponBondPlus5", bp => {
                bp.SetName("Profane Weapon Bond (+5)");
                bp.SetDescription("Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
                "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
                "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
                "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
                "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
                "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
                "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
                "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
                "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
                "levels beyond 5th, to a total of four times per day at 17th level.");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.ComponentsArray = WeaponBondPlus5.ComponentsArray;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { FiendishWeaponVorpalChoice.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var FiendishWeaponBondPlus6 = Helpers.CreateBlueprint<BlueprintFeature>("FiendishWeaponBondPlus6", bp => {
                bp.SetName("Profane Weapon Bond (+6)");
                bp.SetDescription("Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
                "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
                "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
                "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
                "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
                "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
                "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
                "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
                "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
                "levels beyond 5th, to a total of four times per day at 17th level.");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.ComponentsArray = WeaponBondPlus6.ComponentsArray;
            });
            var FiendishWeaponBondProgression = Helpers.CreateBlueprint<BlueprintProgression>("FiendishWeaponBondProgression", bp => {
                bp.SetName("Profane Weapon Bond");
                bp.SetDescription("Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
                "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
                "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
                "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
                "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
                "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
                "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
                "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
                "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
                "levels beyond 5th, to a total of four times per day at 17th level.");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = DreadKnightClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
            });
            var AnimatedLongsword = Resources.GetBlueprint<BlueprintUnit>("34df84245e875364b9a8832256bc349f");
            var WyvernCharoite = Resources.GetBlueprint<BlueprintUnit>("fb3cf6666c50638439cdecfa45ae80ac");
            var UnitDog = Resources.GetBlueprint<BlueprintUnit>("918939943bf32ba4a95470ea696c2ba5");
            var LongswordCompanionUnit = Helpers.CreateBlueprint<BlueprintUnit>("LongswordCompanionUnit", bp => {
                bp.m_Portrait = AnimatedLongsword.m_Portrait;
                bp.m_Faction = UnitDog.m_Faction;
                bp.m_Brain = UnitDog.m_Brain;
                bp.m_AddFacts = AnimatedLongsword.m_AddFacts;
                bp.Gender = AnimatedLongsword.Gender;
                bp.LocalizedName = AnimatedLongsword.LocalizedName;
                bp.Size = AnimatedLongsword.Size;
                bp.Color = AnimatedLongsword.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.Prefab = AnimatedLongsword.Prefab;
                bp.Visual = AnimatedLongsword.Visual;
                bp.FactionOverrides = UnitDog.FactionOverrides;
                bp.Body = AnimatedLongsword.Body;
                bp.Strength = UnitDog.Strength;
                bp.Dexterity = UnitDog.Dexterity;
                bp.Constitution = UnitDog.Constitution;
                bp.Intelligence = UnitDog.Intelligence;
                bp.Wisdom = UnitDog.Wisdom;
                bp.Charisma = UnitDog.Charisma;
                bp.Speed = AnimatedLongsword.Speed;
                bp.Skills = UnitDog.Skills;
                bp.m_DisplayName = AnimatedLongsword.m_DisplayName;
                bp.m_Description = AnimatedLongsword.m_Description;
                bp.m_DescriptionShort = AnimatedLongsword.m_DescriptionShort;
                bp.ComponentsArray = UnitDog.ComponentsArray;
            });
            var LongswordCompanion = Helpers.CreateBlueprint<BlueprintFeature>("LongswordCompanion", bp => {
                bp.SetName("Intelligent Longsword");
                bp.SetDescription("You gain an intelligent longsword companion.");
                bp.m_DescriptionShort = Helpers.CreateString("LongswordCompanion.DescriptionShort", "You gain an intelligent longsword companion.");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.Groups = new FeatureGroup[] { FeatureGroup.AnimalCompanion };
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddPet>(c => {
                    c.m_Pet = LongswordCompanionUnit.ToReference<BlueprintUnitReference>();
                    c.m_LevelRank = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisitePet>(c => { c.NoCompanion = true; });
            });
            //removing longsword pet for now, may add back in a archetype later
            FiendishWeaponBondProgression.LevelEntries = new LevelEntry[8] {
                Helpers.LevelEntry(5, FiendishWeaponBondFeature),
                Helpers.LevelEntry(8, FiendishWeaponBondPlus2),
                Helpers.LevelEntry(9, WeaponBondAdditionalUse),
                Helpers.LevelEntry(11, FiendishWeaponBondPlus3),
                Helpers.LevelEntry(13, WeaponBondAdditionalUse),
                Helpers.LevelEntry(14, FiendishWeaponBondPlus4),
                Helpers.LevelEntry(17, FiendishWeaponBondPlus5, WeaponBondAdditionalUse),
                Helpers.LevelEntry(20, FiendishWeaponBondPlus6)
            };
            var DreadKnightAnimalCompanionProgression = Helpers.CreateBlueprint<BlueprintProgression>("DreadKnightAnimalCompanionProgression", bp => {
                bp.SetName("Dread Knight Animal Companion Progression");
                bp.SetName("");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.GiveFeaturesForPreviousLevels = true;
                bp.ReapplyOnLevelUp = true;
                bp.m_ExclusiveProgression = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                bp.m_FeaturesRankIncrease = new List<BlueprintFeatureReference>();
                bp.LevelEntries = Enumerable.Range(2, 20)
                    .Select(i => new LevelEntry {
                        Level = i,
                        m_Features = new List<BlueprintFeatureBaseReference> {
                            AnimalCompanionRank.ToReference<BlueprintFeatureBaseReference>()
                        },
                    })
                    .ToArray();
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                new BlueprintProgression.ClassWithLevel {
                    m_Class = DreadKnightClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                }
            };
                bp.UIGroups = new UIGroup[0];
            });
            var ServantIcon = AssetLoader.LoadInternal("Skills", "Icon_Servant.png");
            var DreadKnightProfaneMountFiendish = Helpers.CreateBlueprint<BlueprintFeature>("DreadKnightProfaneMountFiendish", bp => {
                bp.SetName("Profane Mount — Fiendish");
                bp.SetDescription("A dreadknight's animal companion gains spell resistance equal to its level +5. It also gains:\n" +
                    "1 — 4 HD: resistance 5 to cold and fire.\n" +
                    "5 — 10 HD: resistance 10 to cold and fire, DR 5/good\n" +
                    "11+ HD: resistance 15 to cold and fire, DR 10/good\n" +
                    "Smite Good (Su): Once per day, the fiendish creature may smite a good-aligned creature. As a swift action, " +
                    "the creature chooses one target within sight to smite. If this target is good, the creature adds its Charisma bonus (if any) to " +
                    "attack rolls and gains a damage bonus equal to its HD against that foe. This effect persists until the target is dead or the creature rests.");
                bp.m_Icon = ServantIcon;
                bp.IsClassFeature = true;
                bp.Ranks = 1;
                bp.AddComponent<AddFeatureToPet>(c => {
                    c.m_Feature = TemplateFiendish.ToReference<BlueprintFeatureReference>();
                });
                bp.AddPrerequisite<PrerequisiteAlignment>(p => {
                    p.Alignment = AlignmentMaskType.Evil | AlignmentMaskType.TrueNeutral;
                    p.HideInUI = true;
                });
            });
            var AnimalCompanionEmpty = Resources.GetBlueprintReference<BlueprintFeatureReference>("472091361cf118049a2b4339c4ea836a");
            var CavalierMountFeatureWolf = Resources.GetModBlueprint<BlueprintFeature>("CavalierMountFeatureWolf");
            var AnimalCompanionFeatureHorse_PreorderBonus = Resources.GetBlueprint<BlueprintFeature>("bfeb9be0a3c9420b8b2beecc8171029c");
            var AnimalCompanionFeatureHorse = Resources.GetBlueprint<BlueprintFeature>("9dc58b5901677c942854019d1dd98374");
            var DreadKnightCompanionSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DreadKnightCompanionSelection", bp => {
                bp.SetName("Fiendish Servant");
                bp.SetDescription("The dreadknight is granted a fiendish steed to carry her into battle. This mount functions " +
                    "as a druid’s animal companion, using the dreadknight’s level as her effective druid level. The creature must be one that " +
                    "she is capable of riding and must be suitable as a mount. A Medium dreadknight can select a horse. A Small dreadknight can select a wolf.");
                bp.IsClassFeature = true;
                bp.ReapplyOnLevelUp = false;
                bp.Groups = new FeatureGroup[0];
                bp.Mode = SelectionMode.OnlyNew;
                bp.Group = FeatureGroup.None;
                bp.Ranks = 1;
                bp.m_Icon = ServantIcon;
                bp.IsPrerequisiteFor = new List<BlueprintFeatureReference>();
                bp.AddFeatures(
                    AnimalCompanionEmpty,
                    AnimalCompanionFeatureHorse.ToReference<BlueprintFeatureReference>(),
                    AnimalCompanionFeatureHorse_PreorderBonus.ToReference<BlueprintFeatureReference>(),
                    CavalierMountFeatureWolf.ToReference<BlueprintFeatureReference>()
                );
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = DreadKnightAnimalCompanionProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = MountTargetFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = AnimalCompanionArchetypeSelection.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = DreadKnightProfaneMountFiendish.ToReference<BlueprintFeatureReference>();
                });
            });
            #endregion
            #region Aura of Despair
            var AODespIcon = AssetLoader.LoadInternal("Skills", "Icon_AODespair.png");
            var AuraOfDespairEffectBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfDespairEffectBuff", bp => {
                bp.SetName("Aura of Despair");
                bp.SetDescription("At 8th level, enemies within 10 feet of an antipaladin take a –2 penalty on all saving throws. This penalty does not stack with the penalty from aura of cowardice. " +
                    "This ability functions only while the dreadknight is conscious, not if he is unconscious or dead.");
                bp.IsClassFeature = true;
                bp.m_Icon = AODespIcon;
                bp.AddComponent<BuffAllSavesBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Penalty;
                    c.Value = -2;
                });
            });
            var AuraOfDespairArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("AuraOfDespairArea", bp => {
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Enemy;
                bp.SpellResistance = false;
                bp.AggroEnemies = true;
                bp.AffectEnemies = true;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = 13.Feet();
                bp.Fx = new PrefabLink();
                bp.AddComponent(AuraUtils.CreateUnconditionalAuraEffect(AuraOfDespairEffectBuff.ToReference<BlueprintBuffReference>()));
            });
            var AuraOfDespairBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfDespairBuff", bp => {
                bp.SetName("Aura of Despair");
                bp.SetDescription("At 8th level, enemies within 10 feet of an dreadknight take a –2 penalty on all saving throws. This penalty does not stack with the penalty from aura of cowardice. " +
                    "This ability functions only while the dreadknight is conscious, not if he is unconscious or dead.");
                bp.m_Icon = AODespIcon;
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = AuraOfDespairArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
            });
            var AuraOfDespairFeature = Helpers.CreateBlueprint<BlueprintFeature>("AuraOfDespairFeature", bp => {
                bp.SetName("Aura of Despair");
                bp.SetDescription("At 8th level, enemies within 10 feet of an dreadknight take a –2 penalty on all saving throws. This penalty does not stack with the penalty from aura of cowardice. " +
                    "This ability functions only while the dreadknight is conscious, not if he is unconscious or dead.");
                bp.m_Icon = AODespIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AuraFeatureComponent>(c => {
                    c.m_Buff = AuraOfDespairBuff.ToReference<BlueprintBuffReference>();
                });
            });
            #endregion
            #region Aura of Absolution
            var AOAIcon = AssetLoader.LoadInternal("Skills", "Icon_AOAbsolution.png");
            var OBBaneIcon = AssetLoader.LoadInternal("Skills", "Icon_OBBane.png");
            var AuraOfAbsolutionBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfAbsolutionBuff", bp => {
                bp.SetName("Aura of Absolution");
                bp.SetDescription("At 11th level, a dreadknight can expend two uses of his sinful absolution ability to grant the " +
                    "effect to all allies for 1 minute, using his bonuses. As a {g|Encyclopedia:Swift_Action}swift action{/g}, the dreadknight chooses one target within sight to smite. " +
                    "The dreadknight's allies add his {g|Encyclopedia:Charisma}Charisma{/g} bonus (if any) to their {g|Encyclopedia:Attack}attack rolls{/g} and add his dreadknight level to " +
                    "all {g|Encyclopedia:Damage}damage rolls{/g} made against the target of his smite. Attacks automatically bypass any {g|Encyclopedia:Damage_Reduction}DR{/g} the creature " +
                    "might possess. In addition, while aura of absolution is in effect, the dreadknight's allies gain a deflection {g|Encyclopedia:Bonus}bonus{/g} equal to his Charisma bonus " +
                    "(if any) to their {g|Encyclopedia:Armor_Class}AC{/g} against attacks made by the target of this ability.");
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Stack;
                bp.m_Icon = AOAIcon;
                bp.FxOnStart = FiendishSmiteGoodBuff.FxOnStart;
                bp.FxOnRemove = FiendishSmiteGoodBuff.FxOnRemove;
                bp.AddComponent<AttackBonusAgainstTarget>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.StatBonus
                    };
                    c.CheckCasterFriend = true;
                });
                bp.AddComponent<DamageBonusAgainstTarget>(c => {
                    c.CheckCaster = false;
                    c.CheckCasterFriend = true;
                    c.ApplyToSpellDamage = true;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.DamageBonus
                    };
                });
                bp.AddComponent<ACBonusAgainstTarget>(c => {
                    c.Descriptor = ModifierDescriptor.Deflection;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.StatBonus
                    };
                    c.CheckCasterFriend = true;
                });
                bp.AddComponent<IgnoreTargetDR>(c => {
                    c.CheckCaster = false;
                    c.CheckCasterFriend = true;
                });
                bp.AddComponent<UniqueBuff>();
            });
            var AuraOfAbsolutionAbility = Helpers.CreateBlueprint<BlueprintAbility>("AuraOfAbsolutionAbility", bp => {
                bp.SetName("Aura of Absolution");
                bp.SetDescription("At 11th level, a dreadknight can expend two uses of his sinful absolution ability to grant the " +
                    "effect to all allies for 1 minute, using his bonuses. As a {g|Encyclopedia:Swift_Action}swift action{/g}, the dreadknight chooses one target within sight to smite. " +
                    "The dreadknight's allies add his {g|Encyclopedia:Charisma}Charisma{/g} bonus (if any) to their {g|Encyclopedia:Attack}attack rolls{/g} and add his dreadknight level to " +
                    "all {g|Encyclopedia:Damage}damage rolls{/g} made against the target of his smite. Attacks automatically bypass any {g|Encyclopedia:Damage_Reduction}DR{/g} the creature " +
                    "might possess. In addition, while aura of absolution is in effect, the dreadknight's allies gain a deflection {g|Encyclopedia:Bonus}bonus{/g} equal to his Charisma bonus " +
                    "(if any) to their {g|Encyclopedia:Armor_Class}AC{/g} against attacks made by the target of this ability.");
                bp.LocalizedDuration = Helpers.CreateString($"{bp.name}.Duration", "1 minute");
                bp.LocalizedSavingThrow = Helpers.CreateString($"{bp.name}.SavingThrow", "None");
                bp.m_Icon = AOAIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = Metamagic.Heighten | Metamagic.Reach;
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = SinfulAbsolutionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 2;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = AuraOfAbsolutionBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            DurationValue = new ContextDurationValue() {
                                DiceType = DiceType.One,
                                m_IsExtendable = true,
                                DiceCountValue = new ContextValue(),
                                BonusValue = 1
                            }
                        }
                    );
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.StatBonus;
                    c.Value = new ContextDiceValue() {
                        DiceCountValue = new ContextValue(),
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueShared = AbilitySharedValue.StatBonus
                        },
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.DamageBonus;
                    c.Value = new ContextDiceValue() {
                        DiceCountValue = new ContextValue(),
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageBonus,
                            ValueShared = AbilitySharedValue.DamageBonus,
                        },
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent(Helpers.CreateContextRankConfig(c => {
                    c.m_Type = AbilityRankType.DamageBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.CharacterLevel;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Max = 20;
                }));
                bp.AddComponent(Helpers.CreateContextRankConfig(c => {
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Charisma;
                    c.m_Max = 20;
                }));
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().PrefabLink;
                    c.Anchor = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().Anchor;
                    c.PositionAnchor = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().PositionAnchor;
                    c.OrientationAnchor = FiendishSmiteGoodAbility.GetComponent<AbilitySpawnFx>().OrientationAnchor;
                });
            });
            var AuraOfAbsolutionFeature = Helpers.CreateBlueprint<BlueprintFeature>("AuraOfAbsolutionFeature", bp => {
                bp.SetName("Aura of Absolution");
                bp.SetDescription("At 11th level, a dreadknight can expend two uses of his sinful absolution ability to grant the " +
                    "effect to all allies for 1 minute, using his bonuses. As a {g|Encyclopedia:Swift_Action}swift action{/g}, the dreadknight chooses one target within sight to smite. " +
                    "The dreadknight's allies add his {g|Encyclopedia:Charisma}Charisma{/g} bonus (if any) to their {g|Encyclopedia:Attack}attack rolls{/g} and add his dreadknight level to " +
                    "all {g|Encyclopedia:Damage}damage rolls{/g} made against the target of his smite. Attacks automatically bypass any {g|Encyclopedia:Damage_Reduction}DR{/g} the creature " +
                    "might possess. In addition, while aura of absolution is in effect, the dreadknight's allies gain a deflection {g|Encyclopedia:Bonus}bonus{/g} equal to his Charisma bonus " +
                    "(if any) to their {g|Encyclopedia:Armor_Class}AC{/g} against attacks made by the target of this ability.");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_Icon = AOAIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                            AuraOfAbsolutionAbility.ToReference<BlueprintUnitFactReference>(),
                        };
                });
            });
            #endregion
            #region Aura of Sin
            var AOSIcon = AssetLoader.LoadInternal("Skills", "Icon_AOS.png");
            var AuraOfSinEffectBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfSinEffectBuff", bp => {
                bp.SetName("Aura of Sin");
                bp.SetDescription("At 14th level, an dreadknight’s weapons are treated as evil-aligned for the purposes of overcoming damage " +
                    "reduction. Any attack made against an enemy within 10 feet of him is treated as evil-aligned for the purposes of overcoming damage " +
                    "reduction. This ability functions only while the dreadknight is conscious, not if he is unconscious or dead.");
                bp.m_Icon = AOSIcon;
                bp.IsClassFeature = true;
                bp.AddComponent<AddIncomingDamageWeaponProperty>(c => {
                    c.Material = PhysicalDamageMaterial.Adamantite;
                    c.AddAlignment = true;
                    c.Alignment = DamageAlignment.Evil;
                    c.Reality = DamageRealityType.Ghost;
                });
            });
            var AuraOfSinArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("AuraOfSinArea", bp => {
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Enemy;
                bp.SpellResistance = false;
                bp.AggroEnemies = true;
                bp.AffectEnemies = true;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = 13.Feet();
                bp.Fx = new PrefabLink();
                bp.AddComponent(AuraUtils.CreateUnconditionalAuraEffect(AuraOfSinEffectBuff.ToReference<BlueprintBuffReference>()));
            });
            var AuraOfSinBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfSinBuff", bp => {
                bp.SetName("Aura of Sin");
                bp.SetDescription("At 14th level, an dreadknight’s weapons are treated as evil-aligned for the purposes of overcoming damage " +
                    "reduction. Any attack made against an enemy within 10 feet of him is treated as evil-aligned for the purposes of overcoming damage " +
                    "reduction. This ability functions only while the dreadknight is conscious, not if he is unconscious or dead.");
                bp.m_Icon = AOSIcon;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = AuraOfSinArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
            });
            var AuraOfSinFeature = Helpers.CreateBlueprint<BlueprintFeature>("AuraOfSinFeature", bp => {
                bp.SetName("Aura of Sin");
                bp.SetDescription("At 14th level, an dreadknight’s weapons are treated as evil-aligned for the purposes of overcoming damage " +
                    "reduction. Any attack made against an enemy within 10 feet of him is treated as evil-aligned for the purposes of overcoming damage " +
                    "reduction. This ability functions only while the dreadknight is conscious, not if he is unconscious or dead.");
                bp.m_Icon = AOSIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AuraFeatureComponent>(c => {
                    c.m_Buff = AuraOfSinBuff.ToReference<BlueprintBuffReference>();
                });
            });
            #endregion
            #region Aura of Depravity
            var AODepIcon = AssetLoader.LoadInternal("Skills", "Icon_AODepravity.png");
            var AuraOfDepravityEffectBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfDepravityEffectBuff", bp => {
                bp.SetName("Aura of Depravity");
                bp.SetDescription("At 17th level, a dreadknight gains DR 5/good and immunity to compulsion " +
                    "spells and spell-like abilities.Each enemy within 10 feet takes a –4 penalty on saving throws against compulsion effects. " +
                    "This ability functions only while the dreadknight is conscious, not if he is unconscious or dead.");
                bp.IsClassFeature = true;
                bp.m_Icon = AODepIcon;
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.Compulsion;
                    c.ModifierDescriptor = ModifierDescriptor.Penalty;
                    c.Value = -4;
                });
            });
            var AuraOfDepravityArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("AuraOfDepravityArea", bp => {
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Enemy;
                bp.SpellResistance = false;
                bp.AggroEnemies = true;
                bp.AffectEnemies = true;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = 13.Feet();
                bp.Fx = new PrefabLink();
                bp.AddComponent(AuraUtils.CreateUnconditionalAuraEffect(AuraOfDepravityEffectBuff.ToReference<BlueprintBuffReference>()));
            });
            var AuraOfDepravityBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfDepravityBuff", bp => {
                bp.SetName("Aura of Depravity");
                bp.SetDescription("At 17th level, a dreadknight gains DR 5/good and immunity to compulsion " +
                    "spells and spell-like abilities. Each enemy within 10 feet takes a –4 penalty on saving throws against compulsion effects. " +
                    "This ability functions only while the dreadknight is conscious, not if he is unconscious or dead.");
                bp.m_Icon = AODepIcon;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = AuraOfDepravityArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
            });
            var AuraOfDepravityFeature = Helpers.CreateBlueprint<BlueprintFeature>("AuraOfDepravityFeature", bp => {
                bp.SetName("Aura of Depravity");
                bp.SetDescription("At 17th level, a dreadknight gains DR 5/good and immunity to compulsion " +
                    "spells and spell-like abilities. Each enemy within 10 feet takes a –4 penalty on saving throws against compulsion effects. " +
                    "This ability functions only while the dreadknight is conscious, not if he is unconscious or dead.");
                bp.m_Icon = AODepIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = SinfulAbsolutionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 1;
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.m_IgnoreFeature = null;
                    c.m_FactToCheck = null;
                    c.Descriptor = SpellDescriptor.Compulsion;
                });
                bp.AddComponent<AuraFeatureComponent>(c => {
                    c.m_Buff = AuraOfDepravityBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.m_WeaponType = null;
                    c.Material = PhysicalDamageMaterial.Adamantite;
                    c.MinEnhancementBonus = 1;
                    c.BypassedByAlignment = true;
                    c.Alignment = DamageAlignment.Good;
                    c.Reality = DamageRealityType.Ghost;
                    c.m_CheckedFactMythic = null;
                    c.Value = 10;
                    c.Pool = 12;
                });
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => {
                    c.m_CasterIgnoreImmunityFact = null;
                    c.Descriptor = SpellDescriptor.Compulsion;
                });
            });
            #endregion
            #region Profane Champion
            var ProfaneChampIcon = AssetLoader.LoadInternal("Skills", "Icon_ProfaneChamp.png");            
            var ProfaneChampion = Helpers.CreateBlueprint<BlueprintFeature>("ProfaneChampion", bp => {
                bp.SetName("Profane Champion");
                bp.SetDescription("At 20th level, a dreadknight becomes a conduit for the might of dark powers. Her DR increases to 10/good. " +
                    "\nThe dreadknight gains an additional 3 uses of Profane Corruption. In addition, whenever she channels negative energy or uses " +
                    "touch of corruption to damage a creature, she deals the maximum possible amount. ");
                bp.m_Icon = ProfaneChampIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.m_WeaponType = null;
                    c.Material = PhysicalDamageMaterial.Adamantite;
                    c.MinEnhancementBonus = 1;
                    c.BypassedByAlignment = true;
                    c.Alignment = DamageAlignment.Good;
                    c.Reality = DamageRealityType.Ghost;
                    c.m_CheckedFactMythic = null;
                    c.Value = 5;
                    c.Pool = 12;
                });
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 3;
                });
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = SinfulAbsolutionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 3;
                });
                bp.AddComponent<AutoMetamagic>(c => {
                    c.m_AllowedAbilities = AutoMetamagic.AllowedType.Any;
                    c.Metamagic = Kingmaker.UnitLogic.Abilities.Metamagic.Maximize;
                    c.Abilities = new List<BlueprintAbilityReference> {
                        DreadKnightChannelNegativeEnergyAbility.ToReference<BlueprintAbilityReference>(),
                        ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>(),
                        TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>()
                    };
                });
            });
            var ProfaneBoonSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("ProfaneBoonSelection", bp => {
                bp.SetName("Profane Boon");
                bp.SetDescription("Upon reaching 5th level, a dreadknight receives a boon from their dark patrons. This boon can take one of two forms. Once the form is chosen, it cannot be changed. " +
                    "The first type of bond allows the dread knight to enhance their weapon as a standard action by calling upon the aid of a fiendish spirit for 1 " +
                    "minute per dreadknight level. When called, the spirit causes the weapon to shed unholy light as a torch. At 5th level, this spirit grants the " +
                    "weapon a + 1 enhancement bonus.For every three levels beyond 5th, the weapon gains another + 1 enhancement bonus, to a maximum of + 6 at 20th level. ");
                bp.m_DescriptionShort = Helpers.CreateString("$ProfaneBoonSelection.DescriptionShort", "Upon reaching 5th level, a dreadknight receives a boon from their dark patrons. This boon can take one of two forms. Once the form is chosen, it cannot be changed. " +
                    "The first type of bond allows the dread knight to enhance their weapon as a standard action by calling upon the aid of a fiendish spirit for 1 " +
                    "minute per dreadknight level. When called, the spirit causes the weapon to shed unholy light as a torch. At 5th level, this spirit grants the " +
                    "weapon a + 1 enhancement bonus.For every three levels beyond 5th, the weapon gains another + 1 enhancement bonus, to a maximum of + 6 at 20th level.");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_Features = new BlueprintFeatureReference[2] {
                DreadKnightCompanionSelection.ToReference<BlueprintFeatureReference>(),
                FiendishWeaponBondProgression.ToReference<BlueprintFeatureReference>() };
                bp.m_AllFeatures = new BlueprintFeatureReference[2] {
                DreadKnightCompanionSelection.ToReference<BlueprintFeatureReference>(),
                FiendishWeaponBondProgression.ToReference<BlueprintFeatureReference>() };
            });
            #endregion
            //Hellknight Singnifer Spellbook Stuffs
            var HellknightSigniferClass = Resources.GetBlueprint<BlueprintCharacterClass>("ee6425d6392101843af35f756ce7fefd");
            var HellknightSigniferDreadknightLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("HellknightSigniferDreadknightLevelUp", bp => {
                bp.SetName("Dreadknight");
                bp.SetDescription("At 1st level, and at every level thereafter, a Hellknight signifer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a " +
                    "spellcasting class he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for " +
                    "additional spells per day, spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Hellknight " +
                    "signifer, he must decide to which class he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = DreadKnightSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 10;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var HellknightSigniferDreadknightProgression = Helpers.CreateBlueprint<BlueprintProgression>("HellknightSigniferDreadknightProgression", bp => {
                bp.SetName("Dreadknight");
                bp.SetDescription("At 1st level, and at every level thereafter, a Hellknight signifer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a " +
                    "spellcasting class he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, except for " +
                    "additional spells per day, spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before becoming a Hellknight " +
                    "signifer, he must decide to which class he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.HellknightSigniferSpellbook };
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] { 
                    new BlueprintProgression.ClassWithLevel {
                        m_Class =  HellknightSigniferClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }                      
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] { };
                bp.ForAllOtherClasses = false;
                bp.LevelEntries = new LevelEntry[10] {
                    Helpers.LevelEntry(1, HellknightSigniferDreadknightLevelUp),
                    Helpers.LevelEntry(2, HellknightSigniferDreadknightLevelUp),
                    Helpers.LevelEntry(3, HellknightSigniferDreadknightLevelUp),
                    Helpers.LevelEntry(4, HellknightSigniferDreadknightLevelUp),
                    Helpers.LevelEntry(5, HellknightSigniferDreadknightLevelUp),
                    Helpers.LevelEntry(6, HellknightSigniferDreadknightLevelUp),
                    Helpers.LevelEntry(7, HellknightSigniferDreadknightLevelUp),
                    Helpers.LevelEntry(8, HellknightSigniferDreadknightLevelUp),
                    Helpers.LevelEntry(9, HellknightSigniferDreadknightLevelUp),
                    Helpers.LevelEntry(10, HellknightSigniferDreadknightLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            // Signature Abilities
            DreadKnightClass.m_SignatureAbilities = new BlueprintFeatureReference[5] {
                    SinfulAbsolutionFeature.ToReference<BlueprintFeatureReference>(),
                    CrueltySelection4.ToReference<BlueprintFeatureReference>(),
                    TouchOfProfaneCorruptionFeature.ToReference<BlueprintFeatureReference>(),
                    DreadKnightChannelNegativeEnergyFeature.ToReference<BlueprintFeatureReference>(),
                    ProfaneBoonSelection.ToReference<BlueprintFeatureReference>()
            };
            DreadKnightProgression.LevelEntries = new LevelEntry[20] {
                Helpers.LevelEntry(1, SinfulAbsolutionFeature, DreadKnightProficiencies, AuraOfEvilFeature),
                Helpers.LevelEntry(2, ProfaneResilience, TouchOfProfaneCorruptionFeature),
                Helpers.LevelEntry(3, AuraOfCowardiceFeature, PlagueBringer, CrueltySelection1),
                Helpers.LevelEntry(4, SinfulAbsolutionUse, TouchOfProfaneCorruptionUse, DreadKnightChannelNegativeEnergyFeature),
                Helpers.LevelEntry(5, ProfaneBoonSelection),
                Helpers.LevelEntry(6, TouchOfProfaneCorruptionUse, CrueltySelection2),
                Helpers.LevelEntry(7, SinfulAbsolutionUse, ChannelTouchOfProfaneCorruptionFeature),
                Helpers.LevelEntry(8, TouchOfProfaneCorruptionUse, AuraOfDespairFeature),
                Helpers.LevelEntry(9, CrueltySelection3),
                Helpers.LevelEntry(10, SinfulAbsolutionUse, TouchOfProfaneCorruptionUse),
                Helpers.LevelEntry(11, AuraOfAbsolutionFeature),
                Helpers.LevelEntry(12, TouchOfProfaneCorruptionUse, CrueltySelection4),
                Helpers.LevelEntry(13, SinfulAbsolutionUse),
                Helpers.LevelEntry(14, TouchOfProfaneCorruptionUse, AuraOfSinFeature),
                Helpers.LevelEntry(15, CrueltySelection4),
                Helpers.LevelEntry(16, SinfulAbsolutionUse, TouchOfProfaneCorruptionUse),
                Helpers.LevelEntry(17, AuraOfDepravityFeature),
                Helpers.LevelEntry(18, TouchOfProfaneCorruptionUse, CrueltySelection4),
                Helpers.LevelEntry(19, SinfulAbsolutionUse),
                Helpers.LevelEntry(20, TouchOfProfaneCorruptionUse, ProfaneChampion)
            };            
            DreadKnightProgression.UIGroups = new UIGroup[] {
                 Helpers.CreateUIGroup(SinfulAbsolutionFeature, SinfulAbsolutionUse),
                 Helpers.CreateUIGroup(DreadKnightProficiencies, TouchOfProfaneCorruptionFeature, TouchOfProfaneCorruptionUse),
                 Helpers.CreateUIGroup(AuraOfEvilFeature, ProfaneResilience, AuraOfCowardiceFeature, ProfaneBoonSelection, AuraOfDespairFeature, AuraOfSinFeature, AuraOfDepravityFeature, ProfaneChampion),
                 Helpers.CreateUIGroup(CrueltySelection1, CrueltySelection2, CrueltySelection3, CrueltySelection4),
                 Helpers.CreateUIGroup(PlagueBringer, DreadKnightChannelNegativeEnergyFeature, ChannelTouchOfProfaneCorruptionFeature, AuraOfSinFeature, AuraOfAbsolutionFeature),
            };
            if (ModSettings.AddedContent.Classes.IsDisabled("Dread Knight")) { return; }
            Helpers.RegisterClass(DreadKnightClass);
        }
    }
}