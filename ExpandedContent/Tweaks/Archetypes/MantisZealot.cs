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

namespace ExpandedContent.Tweaks.Archetypes {
    internal class MantisZealot {
        public static void AddMantisZealot() {

            var WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
            var WarpriestProficiencies = Resources.GetBlueprint<BlueprintFeature>("ad29d445f1534474db8295a61e42d08b");
            var WarpriestSacredWeaponBaseDamageFeature = Resources.GetBlueprint<BlueprintFeature>("8eb5505ae69cc174fb1781134f949e5f");
            var WarpriestWeaponFocusSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("ac384183dbfbbd7499410a21d749bef1");
            var SacredWeaponEnchantFeature = Resources.GetBlueprint<BlueprintFeature>("6db4ccce643b7b2468717effb303afcf");
            var SacredArmorFeature = Resources.GetBlueprint<BlueprintFeature>("35e2d9525c240ce4c8ae47dd387b6e53");
            var SacredWeaponEnchantPlus2 = Resources.GetBlueprint<BlueprintFeature>("4ee3e74f79d5e4f47b7a0091984b2518");
            var SacredArmorEnchantPlus2 = Resources.GetBlueprint<BlueprintFeature>("ec327c67f6a6b2f49a8ca218466a8818");
            var SacredWeaponEnchantPlus3 = Resources.GetBlueprint<BlueprintFeature>("bc1a85932b7d82f49852d6ded3049837");
            var SacredArmorEnchantPlus3 = Resources.GetBlueprint<BlueprintFeature>("bd292463fa74d664086f0a3e4e425c47");
            var SacredWeaponEnchantPlus4 = Resources.GetBlueprint<BlueprintFeature>("b7295813f02c8be45a6e1fce541584df");
            var SacredArmorEnchantPlus4 = Resources.GetBlueprint<BlueprintFeature>("ee65ff63443ce42488515db6a43fee5e");
            var SacredWeaponEnchantPlus5 = Resources.GetBlueprint<BlueprintFeature>("5fffe6a74adb1b742a8ebf64b8bb90fb");
            var SacredArmorEnchantPlus5 = Resources.GetBlueprint<BlueprintFeature>("1e560784dfcb00f4da1415bbad3226c3");
            var WarpriestAspectOfWar = Resources.GetBlueprint<BlueprintFeature>("65cc7abc21826a344aa156e2a40dcecc");
            var SneakAttack = Resources.GetBlueprint<BlueprintFeature>("9b9eac6709e1c084cb18c3a366e0ec87");

            var LightArmorProficiency = Resources.GetBlueprint<BlueprintFeature>("6d3728d4e9c9898458fe5e9532951132");
            var SimpleWeaponProficiency = Resources.GetBlueprint<BlueprintFeature>("e70ecf1ed95ca2f40b754f1adb22bbdd");
            var MartialWeaponProficiency = Resources.GetBlueprint<BlueprintFeature>("203992ef5b35c864390b4e4a1e200629");
            var FalcataProficiency = Resources.GetBlueprint<BlueprintFeature>("91fe4440ac82dbf4383c872c065c6661");

            var EffortlessDualWielding = Resources.GetBlueprint<BlueprintFeature>("0ad7c2825c8504642b571265757d7037");
            var WeaponTrainingHeavyBlades = Resources.GetBlueprint<BlueprintFeature>("2a0ce0186af38ed419f47fce16f93c2a");
            //Icons
            var AchaekekIcon = AssetLoader.LoadInternal("Deities", "Icon_Achaekek.jpg");
            var MantisAspectIcon = AssetLoader.LoadInternal("Skills", "Icon_MantisAspect.jpg");

            var MantisZealotArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("MantisZealotArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"MantisZealotArchetype.Name", "Mantis Zealot");
                bp.LocalizedDescription = Helpers.CreateString($"MantisZealotArchetype.Description", "Among the Red Mantis worshipers of Achaekek, some hold such strong faith in their " +
                    "assassin god that they gain divine power.They forge themselves into perfect killers in honor of He Who Walks in Blood.These warpriests are a relatively recent addition " +
                    "to the Red Mantis arsenal, but in the few short decades they have served the assassins, they have quickly built a reputation for themselves as particularly fanatical " +
                    "devotees of the Mantis God.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"MantisZealotArchetype.Description", "Among the Red Mantis worshipers of Achaekek, some hold such strong faith in their " +
                    "assassin god that they gain divine power.They forge themselves into perfect killers in honor of He Who Walks in Blood.These warpriests are a relatively recent addition " +
                    "to the Red Mantis arsenal, but in the few short decades they have served the assassins, they have quickly built a reputation for themselves as particularly fanatical " +
                    "devotees of the Mantis God.");
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.LawfulEvil;
                });
            });
            var MantisZealotProficiencies = Helpers.CreateBlueprint<BlueprintFeature>("MantisZealotProficiencies", bp => {
                bp.SetName("Mantis Zealot Proficiencies");
                bp.SetDescription("Mantis zealots are proficient with simple and martial weapons, as well as with the sawtooth sabre. They are proficient with light armor but not with shields.");
                bp.Ranks = 1;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        LightArmorProficiency.ToReference<BlueprintUnitFactReference>(),
                        SimpleWeaponProficiency.ToReference<BlueprintUnitFactReference>(),
                        MartialWeaponProficiency.ToReference<BlueprintUnitFactReference>(),
                        FalcataProficiency.ToReference<BlueprintUnitFactReference>(),
                    };
                });
            });
            var SawtoothSabreStyle = Helpers.CreateBlueprint<BlueprintFeature>("SawtoothSabreStyle", bp => {
                bp.SetName("Sawtooth Sabre Style");
                bp.SetDescription("A Mantis zealot emulates He Who Walks in Blood in their fighting style, with their twin blades acting as the mantises claws. This allows the zealot to use one-handed " +
                    "heavy blades as light weapons for the purpose of two-weapon fighting");
                bp.Ranks = 1;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { EffortlessDualWielding.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WeaponTrainingHeavyBlades.ToReference<BlueprintUnitFactReference>() };
                });
            });

            //Sacred Reflexes
            ///Resource
            var SacredReflexesAbilityResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("SacredReflexesAbilityResource", bp => {
                bp.m_Min = 7;
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByStat = false,
                    m_Class = new BlueprintCharacterClassReference[] {
                        WarpriestClass.ToReference<BlueprintCharacterClassReference>() },
                    m_Archetypes = new BlueprintArchetypeReference[] {
                        MantisZealotArchetype.ToReference<BlueprintArchetypeReference>() },
                    
                    IncreasedByLevel = true,
                    StartingLevel = 7,
                    LevelIncrease = 1,
                    StartingIncrease = 1,
                    LevelStep = 1,
                    PerStepIncrease = 1,
                };

            });
            ///Buffs
            var SacredReflexesAbility1Buff = Helpers.CreateBuff("SacredReflexesAbility1Buff", bp => {
                bp.SetName("Sacred Reflexes");
                bp.SetDescription("Emulate the Mantis God’s grace and move with a supernatural fluidity. As a swift action gain uncanny dodge as per the rogue " +
                    "class feature. You can use this ability for a number of minutes per day equal to his warpriest level, but it must be spent in 1-minute increments.");
                bp.AddComponent<FlatFootedIgnore>(c => {
                    c.Type = FlatFootedIgnoreType.UncannyDodge;
                });
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.AttackOfOpportunityBeforeInitiative;
                });
                bp.m_Icon = AchaekekIcon;
            });
            var SacredReflexesAbility2Buff = Helpers.CreateBuff("SacredReflexesAbility2Buff", bp => {
                bp.SetName("Sacred Reflexes Improved");
                bp.SetDescription("Emulate the Mantis God’s grace and move with a supernatural fluidity. As a swift action gain uncanny dodge as per the rogue " +
                    "class feature and evasion as per the Monk feature. You can use this ability for a number of minutes per day equal to his warpriest level, but it must be spent in 1-minute " +
                    "increments.");
                bp.AddComponent<FlatFootedIgnore>(c => {
                    c.Type = FlatFootedIgnoreType.UncannyDodge;
                });
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.AttackOfOpportunityBeforeInitiative;
                });
                bp.AddComponent<Evasion>(c => {
                    c.SavingThrow = SavingThrowType.Reflex;
                });
                bp.m_Icon = AchaekekIcon;
            });
            var SacredReflexesAbility3Buff = Helpers.CreateBuff("SacredReflexesAbility3Buff", bp => {
                bp.SetName("Sacred Reflexes Mastered");
                bp.SetDescription("Emulate the Mantis God’s grace and move with a supernatural fluidity. As a swift action gain improved uncanny dodge as per the rogue " +
                    "class feature and evasion as per the Monk feature. You can use this ability for a number of minutes per day equal to his warpriest level, but it must be spent in 1-minute " +
                    "increments.");
                bp.AddComponent<FlatFootedIgnore>(c => {
                    c.Type = FlatFootedIgnoreType.UncannyDodge;
                });
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.AttackOfOpportunityBeforeInitiative;
                });
                bp.AddComponent<Evasion>(c => {
                    c.SavingThrow = SavingThrowType.Reflex;
                });
                bp.AddComponent<AddMechanicsFeature>(c => {
                    c.m_Feature = AddMechanicsFeature.MechanicsFeatureType.CannotBeFlanked;
                });
                bp.m_Icon = AchaekekIcon;
            });
            var SacredReflexesAbility4Buff = Helpers.CreateBuff("SacredReflexesAbility4Buff", bp => {
                bp.SetName("Sacred Reflexes Perfect");
                bp.SetDescription("Emulate the Mantis God’s grace and move with a supernatural fluidity. As a swift action gain improved uncanny dodge as per the rogue " +
                    "class feature and improved evasion as per the Monk feature. You can use this ability for a number of minutes per day equal to his warpriest level, but it must be spent in 1-minute " +
                    "increments.");
                bp.AddComponent<FlatFootedIgnore>(c => {
                    c.Type = FlatFootedIgnoreType.UncannyDodge;
                });
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.AttackOfOpportunityBeforeInitiative;
                });
                bp.AddComponent<ImprovedEvasion>(c => {
                    c.SavingThrow = SavingThrowType.Reflex;
                });
                bp.AddComponent<AddMechanicsFeature>(c => {
                    c.m_Feature = AddMechanicsFeature.MechanicsFeatureType.CannotBeFlanked;
                });
                bp.m_Icon = AchaekekIcon;
            });
            ///Abilities
            var SacredReflexesAbility1 = Helpers.CreateBlueprint<BlueprintActivatableAbility>("SacredReflexesAbility", bp => {
                bp.SetName("Sacred Reflexes");
                bp.SetDescription("Emulate the Mantis God’s grace and move with a supernatural fluidity. As a swift action gain uncanny dodge as per the rogue " +
                    "class feature. You can use this ability for a number of minutes per day equal to his warpriest level, but it must be spent in 1-minute increments."); ;
                bp.m_Icon = AchaekekIcon;
                bp.AddComponent<ActivatableAbilityResourceLogic> (c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = SacredReflexesAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = SacredReflexesAbility1Buff.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Swift;
                
            });
            var SacredReflexesAbility2 = Helpers.CreateBlueprint<BlueprintActivatableAbility>("SacredReflexesAbility2", bp => {
                bp.SetName("Sacred Reflexes Improved");
                bp.SetDescription("Emulate the Mantis God’s grace and move with a supernatural fluidity. As a swift action gain uncanny dodge as per the rogue " +
                    "class feature and evasion as per the Monk feature. You can use this ability for a number of minutes per day equal to his warpriest level, but it must be spent in 1-minute " +
                    "increments.");
                bp.m_Icon = AchaekekIcon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = SacredReflexesAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = SacredReflexesAbility2Buff.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Swift;

            }); 
            var SacredReflexesAbility3 = Helpers.CreateBlueprint<BlueprintActivatableAbility>("SacredReflexesAbility3", bp => {
                bp.SetName("Sacred Reflexes Mastered");
                bp.SetDescription("Emulate the Mantis God’s grace and move with a supernatural fluidity. As a swift action gain improved uncanny dodge as per the rogue " +
                    "class feature and evasion as per the Monk feature. You can use this ability for a number of minutes per day equal to his warpriest level, but it must be spent in 1-minute " +
                    "increments.");
                bp.m_Icon = AchaekekIcon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = SacredReflexesAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = SacredReflexesAbility3Buff.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Swift;

            }); 
            var SacredReflexesAbility4 = Helpers.CreateBlueprint<BlueprintActivatableAbility>("SacredReflexesAbility4", bp => {
                bp.SetName("Sacred Reflexes Perfect");
                bp.SetDescription("Emulate the Mantis God’s grace and move with a supernatural fluidity. As a swift action gain improved uncanny dodge as per the rogue " +
                    "class feature and improved evasion as per the Monk feature. You can use this ability for a number of minutes per day equal to his warpriest level, but it must be spent in 1-minute " +
                    "increments.");
                bp.m_Icon = AchaekekIcon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerMinute;
                    c.m_RequiredResource = SacredReflexesAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = SacredReflexesAbility4Buff.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = true;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Swift;

            });
            ///Features
            var SacredReflexesFeature1 = Helpers.CreateBlueprint<BlueprintFeature>("SacredReflexesFeature1", bp => {
                bp.SetName("Sacred Reflexes");
                bp.SetDescription("At 7th level, a mantis zealot can emulate the Mantis God’s grace and move with a supernatural fluidity. As a swift action he can gain uncanny dodge as per the rogue " +
                    "class feature. He can use this ability for a number of minutes per day equal to his warpriest level, but it must be spent in 1-minute increments. At 10th level, when he uses this " +
                    "ability, he also gains evasion as per the monk class feature. At 13th level, he gains improved uncanny dodge instead of uncanny dodge. At 19th level, he gains improved evasion " +
                    "instead of evasion.");
                bp.m_Icon = AchaekekIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { SacredReflexesAbility1.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = SacredReflexesAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                });
            });
            var SacredReflexesFeature2 = Helpers.CreateBlueprint<BlueprintFeature>("SacredReflexesFeature2", bp => {
                bp.SetName("Sacred Reflexes Improved");
                bp.SetDescription("At 7th level, a mantis zealot can emulate the Mantis God’s grace and move with a supernatural fluidity. As a swift action he can gain uncanny dodge as per the rogue " +
                    "class feature. He can use this ability for a number of minutes per day equal to his warpriest level, but it must be spent in 1-minute increments. At 10th level, when he uses this " +
                    "ability, he also gains evasion as per the monk class feature. At 13th level, he gains improved uncanny dodge instead of uncanny dodge. At 19th level, he gains improved evasion " +
                    "instead of evasion.");
                bp.m_Icon = AchaekekIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { SacredReflexesAbility2.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = SacredReflexesAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                });
            });
            var SacredReflexesFeature3 = Helpers.CreateBlueprint<BlueprintFeature>("SacredReflexesFeature3", bp => {
                bp.SetName("Sacred Reflexes Mastered");
                bp.SetDescription("At 7th level, a mantis zealot can emulate the Mantis God’s grace and move with a supernatural fluidity. As a swift action he can gain uncanny dodge as per the rogue " +
                    "class feature. He can use this ability for a number of minutes per day equal to his warpriest level, but it must be spent in 1-minute increments. At 10th level, when he uses this " +
                    "ability, he also gains evasion as per the monk class feature. At 13th level, he gains improved uncanny dodge instead of uncanny dodge. At 19th level, he gains improved evasion " +
                    "instead of evasion.");
                bp.m_Icon = AchaekekIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { SacredReflexesAbility3.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = SacredReflexesAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                });
            });
            var SacredReflexesFeature4 = Helpers.CreateBlueprint<BlueprintFeature>("SacredReflexesFeature4", bp => {
                bp.SetName("Sacred Reflexes Perfect");
                bp.SetDescription("At 7th level, a mantis zealot can emulate the Mantis God’s grace and move with a supernatural fluidity. As a swift action he can gain uncanny dodge as per the rogue " +
                    "class feature. He can use this ability for a number of minutes per day equal to his warpriest level, but it must be spent in 1-minute increments. At 10th level, when he uses this " +
                    "ability, he also gains evasion as per the monk class feature. At 13th level, he gains improved uncanny dodge instead of uncanny dodge. At 19th level, he gains improved evasion " +
                    "instead of evasion.");
                bp.m_Icon = AchaekekIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { SacredReflexesAbility4.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = SacredReflexesAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                });
            });
            //Sneak Attack
            var MantisZealotSneakAttackFeature = Helpers.CreateBlueprint<BlueprintFeature>("MantisZealotSneakAttackFeature", bp => {
                bp.SetName("Zealots Sneak Attack");
                bp.SetDescription("At 4th level, a mantis zealot can make a sneak attack. This ability functions as the rogue class feature of the same name. At 4th level, his sneak attack damage is " +
                    "+1d6. This damage increases by 1d6 at 8th level and every 4 warpriest levels thereafter. If the zealot gets a sneak attack bonus from another source, the bonuses stack.");
                bp.m_DescriptionShort = Helpers.CreateString($"CastigatorOpportunist.Description", "At 4th level, a mantis zealot can make a sneak attack. This ability functions as the rogue class " +
                    "feature of the same name. At 4th level, his sneak attack damage is +1d6. This damage increases by 1d6 at 8th level and every 4 warpriest levels thereafter. If the zealot gets a " +
                    "sneak attack bonus from another source, the bonuses stack.");
                bp.m_Icon = SneakAttack.Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { SneakAttack.ToReference<BlueprintUnitFactReference>() };
                });
            });
            //Aspect of the Mantis Capstone
            var AspectOfTheMantisResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("AspectOfTheMantisResource", bp => {
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
            var MantisBleedBuff = Helpers.CreateBlueprint<BlueprintBuff>("MantisBleedBuff", bp => {
                bp.SetName("Mantis Bleed");
                bp.SetDescription("This creature takes 25 hit point {g|Encyclopedia:Damage}damage{/g} each turn. Bleeding can be stopped through the application " +
                    "of any {g|Encyclopedia:Spell}spell{/g} that cures hit point damage.");
                bp.m_Icon = MantisAspectIcon;
                bp.AddComponent<AddFactContextActions>(c => {
                    //Dex bleed damage or just 25 depending on time
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
                                    Form = Kingmaker.Enums.Damage.PhysicalDamageForm.Slashing,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = Kingmaker.Enums.Damage.DamageEnergyType.Fire,
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
                                    Value = 25,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                            },
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        });
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
                                    IfFalse= Helpers.CreateActionList(
                                        new ContextActionRemoveSelf()
                                        )
                                    }
                                )
                        };
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
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });
            var MantisBleedEnchantment = Helpers.CreateBlueprint<BlueprintWeaponEnchantment>("MantisBleedEnchantment", bp => {
                bp.SetName("Mantis");
                bp.SetDescription("All attacks deal 25 bleed damage that triggers each round but does not stack with itself.");
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.OnlyHit = true;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = MantisBleedBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                m_IsExtendable = true,
                            }
                        });
                });
                bp.SetPrefix("Mantis");
                bp.SetSuffix("");
            });
            var AspectOfTheMantisBuff = Helpers.CreateBuff("AspectOfTheMantisBuff", bp => {
                bp.SetName("Aspect of the Mantis");
                bp.SetDescription("At 20th level, the Mantis Zealot can become an aspect of Achaekek. Once per day as a swift action, they can add 5 " +
                    "to their base attack bonus, gain DR 10/—, and can cause all attacks to deal 25 bleed damage that triggers each round but does not stack. This " +
                    "ability lasts for 1 minute.");
                bp.m_Icon = MantisAspectIcon;
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Stat = StatType.BaseAttackBonus;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 5,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                });
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 10,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                });
                //Bleed Stuff
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = MantisBleedEnchantment.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.PrimaryHand;
                });
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = MantisBleedEnchantment.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.SecondaryHand;
                });
            });
            var AspectOfTheMantisAbility = Helpers.CreateBlueprint<BlueprintAbility>("AspectOfTheMantisAbility", bp => {
                bp.SetName("Aspect Of The Mantis");
                bp.SetDescription("At 20th level, the Mantis Zealot can become an aspect of Achaekek. Once per day as a swift action, they can add 5 " +
                    "to their base attack bonus, gain DR 10/—, and can cause all attacks to deal 25 bleed damage that triggers each round but does not stack. This " +
                    "ability lasts for 1 minute.");
                bp.m_Icon = MantisAspectIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = AspectOfTheMantisBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = AspectOfTheMantisResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var AspectOfTheMantisFeature = Helpers.CreateBlueprint<BlueprintFeature>("AspectOfTheMantisFeature", bp => {
                bp.SetName("Aspect Of The Mantis");
                bp.SetDescription("At 20th level, the Mantis Zealot can become an aspect of Achaekek. Once per day as a swift action, they can add 5 " +
                    "to their base attack bonus, gain DR 10/—, and can cause all attacks to deal 25 bleed damage that triggers each round but does not stack. This " +
                    "ability lasts for 1 minute.");
                bp.m_Icon = MantisAspectIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {AspectOfTheMantisAbility.ToReference<BlueprintUnitFactReference>()};
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = AspectOfTheMantisResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            MantisZealotArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, WarpriestProficiencies, WarpriestSacredWeaponBaseDamageFeature, WarpriestWeaponFocusSelection),
                    Helpers.LevelEntry(4, SacredWeaponEnchantFeature),
                    Helpers.LevelEntry(7, SacredArmorFeature),
                    Helpers.LevelEntry(8, SacredWeaponEnchantPlus2),
                    Helpers.LevelEntry(10, SacredArmorEnchantPlus2),
                    Helpers.LevelEntry(12, SacredWeaponEnchantPlus3),
                    Helpers.LevelEntry(13, SacredArmorEnchantPlus3),
                    Helpers.LevelEntry(16, SacredWeaponEnchantPlus4, SacredArmorEnchantPlus4),
                    Helpers.LevelEntry(19, SacredArmorEnchantPlus5),
                    Helpers.LevelEntry(20, SacredWeaponEnchantPlus5, WarpriestAspectOfWar)
            };
            MantisZealotArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, MantisZealotProficiencies, SawtoothSabreStyle),
                    Helpers.LevelEntry(4, MantisZealotSneakAttackFeature),
                    Helpers.LevelEntry(7, SacredReflexesFeature1),
                    Helpers.LevelEntry(8, MantisZealotSneakAttackFeature),
                    Helpers.LevelEntry(10, SacredReflexesFeature2),
                    Helpers.LevelEntry(12, MantisZealotSneakAttackFeature),
                    Helpers.LevelEntry(13, SacredReflexesFeature3),
                    Helpers.LevelEntry(16, MantisZealotSneakAttackFeature),
                    Helpers.LevelEntry(19, SacredReflexesFeature4),
                    Helpers.LevelEntry(20, MantisZealotSneakAttackFeature, AspectOfTheMantisFeature),
            };
            
            WarpriestClass.m_Archetypes = WarpriestClass.m_Archetypes.AppendToArray(MantisZealotArchetype.ToReference<BlueprintArchetypeReference>());

        }
    }
}
