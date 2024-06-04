using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.UI.ServiceWindow;
using Kingmaker.UI.GenericSlot;
using ExpandedContent.Tweaks.Components;
using Kingmaker.UnitLogic.ActivatableAbilities;
using TabletopTweaks.Core.NewComponents;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Blueprints.Classes.Selection;
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.Blessings {
    internal class ArtificeBlessing {
        public static void AddArtificeBlessing() {

            var BlessingSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("6d9dcc2a59210a14891aeedb09d406aa");
            var SecondBlessingSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("b7ce4a67287cda746a59b31c042305cf");
            var OwlcatArtificeBlessing = Resources.GetBlueprintReference<BlueprintFeatureReference>("04e5d412a7b94f809a28607618b71619");

            var ArtificeDomainAllowed = Resources.GetBlueprintReference<BlueprintFeatureReference>("9656b1c7214180f4b9a6ab56f83b92fb");
            var WarpriestClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("30b5e47d47a0e37438cc5a80c96cfb99");
            var WarpriestAspectOfWarBuff = Resources.GetBlueprint<BlueprintBuff>("27d14b07b52c2df42a4dcd6bfb840425");
            var BlessingResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("d128a6332e4ea7c4a9862b9fdb358cca");
            var MasterStrikeToggleAbility = Resources.GetBlueprint<BlueprintActivatableAbility>("926bff1386d58824688363a3eeb98260");
            var ExploitWeakness = Resources.GetBlueprint<BlueprintFeature>("374a73288a36e2d4f9e54c75d2e6e573");


            //DC Property, putting this here as it is the first Blessing, it is unlikely I will need to use it much
            var PaladinClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var RangerClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("cda0615668a6df14eb36ba19ee881af6");
            var DivineTrackerArchetype = Resources.GetModBlueprint<BlueprintArchetype>("DivineTrackerArchetype");
            var BlessingDCProperty = Helpers.CreateBlueprint<BlueprintUnitProperty>("BlessingDCProperty", bp => {
                bp.AddComponent<SummClassLevelGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.Div2,
                        m_Negate = false
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        WarpriestClass,
                        RangerClass,
                        PaladinClass
                    };
                    c.Archetype = DivineTrackerArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_Archetypes = new BlueprintArchetypeReference[] { TempleChampionArchetype.ToReference<BlueprintArchetypeReference>() };
                });
                bp.AddComponent<StatValueGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs,
                        m_Negate = false,
                        m_LimitType = PropertySettings.LimitType.None
                    };
                    c.Stat = StatType.Wisdom;
                    c.ValueType = StatValueGetter.ReturnType.Bonus;
                });
                bp.BaseValue = 10;
                bp.OperationOnComponents = BlueprintUnitProperty.MathOperation.Sum;
            });


            var ArtificeBlessingMajorMHBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("ArtificeBlessingMajorMHBaseAbility", bp => {
                bp.SetName("Transfer Magic - Main Hand");
                bp.SetDescription("You can temporarily copy a weapon enchantment from one weapon to another. You may copy any simple permanent " +
                    "enchant from a weapon you have currently equipped. If you are using this ability on a double weapon, only one end of the double weapon is " +
                    "affected. The copy lasts for 1 minute. You can use this ability multiple times on the same weapon.");
                bp.m_Icon = MasterStrikeToggleAbility.Icon;
                bp.AddComponent<AbilityVariants>(c => {
                    c.m_Variants = new BlueprintAbilityReference[] {

                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString("ArtificeBlessingMajorMHBaseAbility.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var ArtificeBlessingMajorOHBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("ArtificeBlessingMajorOHBaseAbility", bp => {
                bp.SetName("Transfer Magic - Off Hand");
                bp.SetDescription("You can temporarily copy a weapon enchantment from one weapon to another. You may copy any simple permanent " +
                    "enchant from a weapon you have currently equipped. If you are using this ability on a double weapon, only one end of the double weapon is " +
                    "affected. The copy lasts for 1 minute. You can use this ability multiple times on the same weapon.");
                bp.m_Icon = MasterStrikeToggleAbility.Icon;
                bp.AddComponent<AbilityVariants>(c => {
                    c.m_Variants = new BlueprintAbilityReference[] {

                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString("ArtificeBlessingMajorOHBaseAbility.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            


            var ArtificeBlessingMajorFeature = Helpers.CreateBlueprint<BlueprintFeature>("ArtificeBlessingMajorFeature", bp => {
                bp.SetName("Transfer Magic");
                bp.SetDescription("At 10th level, you can temporarily copy a weapon enchantment from one weapon to another. You may copy any simple permanent " +
                    "enchant from a weapon you have currently equipped. If you are using this ability on a double weapon, only one end of the double weapon is " +
                    "affected. The copy lasts for 1 minute. You can use this ability multiple times on the same weapon. \nSome examples of simple enchants are, " +
                    "Icy Burst, Holy, Keen, Speed.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ArtificeBlessingMajorMHBaseAbility.ToReference<BlueprintUnitFactReference>(),
                        ArtificeBlessingMajorOHBaseAbility.ToReference<BlueprintUnitFactReference>()
                        
                    };
                });
                bp.HideInCharacterSheetAndLevelUp = true;
            });
                        
            var ConstructType = Resources.GetBlueprint<BlueprintFeature>("fd389783027d63343b4a5634bd81645f");
            var ArtificeBlessingMinorBuff = Helpers.CreateBuff("ArtificeBlessingMinorBuff", bp => {
                bp.SetName("Crafter’s Wrath");
                bp.SetDescription("For 1 minute, whenever you deal damage to constructs or objects with melee weapons, you bypasses hardness and damage reduction.");
                bp.m_Icon = ExploitWeakness.Icon;
                bp.AddComponent<IgnoreDamageReductionOnAttackRangeType>(c => {
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyNaturalAttacks = false;
                    c.CriticalHit = false;
                    c.m_WeaponType = null;
                    c.CheckEnemyFact = true;
                    c.m_CheckedFact = ConstructType.ToReference<BlueprintUnitFactReference>();
                    c.CheckWeaponRangeType = true;
                    c.RangeType = WeaponRangeType.Melee;
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });            
            var ArtificeBlessingMinorAbility = Helpers.CreateBlueprint<BlueprintAbility>("ArtificeBlessingMinorAbility", bp => {
                bp.SetName("Crafter’s Wrath");
                bp.SetDescription("You may touch an ally and grant them greater power to harm and destroy crafted objects. For 1 minute, whenever " +
                    "this ally deals damage to constructs or objects with melee weapons, they bypasses hardness and damage reduction.");
                bp.m_Icon = ExploitWeakness.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ArtificeBlessingMinorBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.One,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString("ArtificeBlessingMinorAbility.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            
            var ArtificeBlessingFeature = Helpers.CreateBlueprint<BlueprintFeature>("ArtificeBlessingFeature", bp => {
                bp.SetName("Artifice");
                bp.SetDescription("At 1st level, you can touch an ally and grant them greater power to harm and destroy crafted objects. For 1 minute, whenever this " +
                    "ally deals damage to constructs or objects with melee weapons, they bypasses hardness and damage reduction. \nAt 10th level, you can temporarily " +
                    "copy a weapon enchantment from one weapon to another. You may copy any simple permanent enchant from a weapon you have currently equipped. If you " +
                    "are using this ability on a double weapon, only one end of the double weapon is affected. The copy lasts for 1 minute. You can use this ability " +
                    "multiple times on the same weapon. \nSome examples of simple enchants are, Icy Burst, Holy, Keen, Speed.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ArtificeBlessingMinorAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = WarpriestClass;
                    c.Level = 10;
                    c.m_Feature = ArtificeBlessingMajorFeature.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.CheckInProgression = true;
                    c.HideInUI = true;
                    c.m_Feature = ArtificeDomainAllowed;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.WarpriestBlessing };
            });
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerArtificeBlessingFeature", ArtificeBlessingFeature, "At 1st level, you can touch an ally and grant them greater power to harm and destroy crafted objects. For 1 minute, whenever this ally deals damage to constructs or objects with melee weapons, they bypasses hardness and damage reduction. \nAt 13th level, you can temporarily copy a weapon enchantment from one weapon to another. You may copy any simple permanent enchant from a weapon you have currently equipped. If you are using this ability on a double weapon, only one end of the double weapon is affected. The copy lasts for 1 minute. You can use this ability multiple times on the same weapon. \nSome examples of simple enchants are, Icy Burst, Holy, Keen, Speed.");

            //Added in ModSupport
            var DivineTrackerArtificeBlessingFeature = Resources.GetModBlueprint<BlueprintFeature>("DivineTrackerArtificeBlessingFeature");
            var QuickenBlessingArtificeFeature = Helpers.CreateBlueprint<BlueprintFeature>("QuickenBlessingArtificeFeature", bp => {
                bp.SetName("Quicken Blessing — Artifice");
                bp.SetDescription("Choose one of your blessings that normally requires a standard action to use. You can expend two of your daily uses of blessings " +
                    "to deliver that blessing (regardless of whether it’s a minor or major effect) as a swift action instead.");
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Feat };
                bp.AddComponent<AbilityActionTypeConversion>(c => {
                    c.m_Abilities = new BlueprintAbilityReference[] {                        
                        Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMinorAbility").ToReference<BlueprintAbilityReference>()
                    };
                    c.ResourceMultiplier = 2;
                    c.ActionType = UnitCommand.CommandType.Swift;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.Amount = 1;
                    c.m_Features = new BlueprintFeatureReference[] {
                        ArtificeBlessingFeature.ToReference<BlueprintFeatureReference>(),
                        DivineTrackerArtificeBlessingFeature.ToReference<BlueprintFeatureReference>()
                    };
                });
            });
            ArtificeBlessingFeature.IsPrerequisiteFor = new List<BlueprintFeatureReference>() { QuickenBlessingArtificeFeature.ToReference<BlueprintFeatureReference>() };



            #region Major Varients
            //var BaneMonstriusHumanoid = Resources.GetBlueprint<BlueprintWeaponEnchantment>("c5f84a79ad154c84e8d2e9fe0dd49350"); Not used
            //var BaneNonHuman = Resources.GetBlueprint<BlueprintWeaponEnchantment>("eb2b2e9f741e4cc18edc47cbb1387e02"); Not used
            //var BaneOrcGoblin = Resources.GetBlueprint<BlueprintWeaponEnchantment>("0391d8eae25f39a48bcc6c2fc8bf4e12"); Not used
            //var BaneReptilian= Resources.GetBlueprint<BlueprintWeaponEnchantment>("c4b9cce255d1d6641a6105a255934e2e"); Not used
            //var Deteriorative = Resources.GetBlueprint<BlueprintWeaponEnchantment>("bbe55d6e76b973d41bf5abeed643861d"); Might be unique
            var Agile = Resources.GetBlueprint<BlueprintWeaponEnchantment>("a36ad92c51789b44fa8a1c5c116a1328");
            CreateTrasferMagicWeaponEnchant("Agile", Agile);
            var Anarchic = Resources.GetBlueprint<BlueprintWeaponEnchantment>("57315bc1e1f62a741be0efde688087e9");
            CreateTrasferMagicWeaponEnchant("Anarchic", Anarchic);
            var Axiomatic = Resources.GetBlueprint<BlueprintWeaponEnchantment>("0ca43051edefcad4b9b2240aa36dc8d4");
            CreateTrasferMagicWeaponEnchant("Axiomatic", Axiomatic);
            var BaneAberration = Resources.GetBlueprint<BlueprintWeaponEnchantment>("ee71cc8848219c24b8418a628cc3e2fa");
            CreateTrasferMagicWeaponEnchant("Aberration Bane", BaneAberration);
            var BaneAnimal = Resources.GetBlueprint<BlueprintWeaponEnchantment>("78cf9fabe95d3934688ea898c154d904");
            CreateTrasferMagicWeaponEnchant("Animal Bane", BaneAnimal);
            var BaneConstruct = Resources.GetBlueprint<BlueprintWeaponEnchantment>("73d30862f33cc754bb5a5f3240162ae6");
            CreateTrasferMagicWeaponEnchant("Construct Bane", BaneConstruct);
            var BaneDragon = Resources.GetBlueprint<BlueprintWeaponEnchantment>("e5cb46a0a658b0a41854447bea32d2ee");
            CreateTrasferMagicWeaponEnchant("Dragon Bane", BaneDragon);
            var BaneEverything = Resources.GetBlueprint<BlueprintWeaponEnchantment>("1a93ab9c46e48f3488178733be29342a");
            CreateTrasferMagicWeaponEnchant("Bane", BaneEverything);
            var BaneFey = Resources.GetBlueprint<BlueprintWeaponEnchantment>("b6948040cdb601242884744a543050d4");
            CreateTrasferMagicWeaponEnchant("Fey Bane", BaneFey);
            var BaneGiant = Resources.GetBlueprint<BlueprintWeaponEnchantment>("dcecb5f2ffacfd44ead0ed4f8846445d");
            CreateTrasferMagicWeaponEnchant("Giant Bane", BaneGiant);
            var BaneLiving = Resources.GetBlueprint<BlueprintWeaponEnchantment>("e1d6f5e3cd3855b43a0cb42f6c747e1c");
            CreateTrasferMagicWeaponEnchant("Living Bane", BaneLiving);
            var BaneLycanthrope = Resources.GetBlueprint<BlueprintWeaponEnchantment>("188efcfcd9938d44e9561c87794d17a8");
            CreateTrasferMagicWeaponEnchant("Lycanthrope Bane", BaneLycanthrope);
            var BaneMagicalBeast = Resources.GetBlueprint<BlueprintWeaponEnchantment>("97d477424832c5144a9413c64d818659");
            CreateTrasferMagicWeaponEnchant("Magical Beast Bane", BaneMagicalBeast);
            var BaneOutsiderChaotic = Resources.GetBlueprint<BlueprintWeaponEnchantment>("234177d5807909f44b8c91ed3c9bf7ac");
            CreateTrasferMagicWeaponEnchant("Chaotic Outsider Bane", BaneOutsiderChaotic);
            var BaneOutsiderEvil = Resources.GetBlueprint<BlueprintWeaponEnchantment>("20ba9055c6ae1e44ca270c03feacc53b");
            CreateTrasferMagicWeaponEnchant("Evil Outsider Bane", BaneOutsiderEvil);
            var BaneOutsiderGood = Resources.GetBlueprint<BlueprintWeaponEnchantment>("a876de94b916b7249a77d090cb9be4f3");
            CreateTrasferMagicWeaponEnchant("Good Outsider Bane", BaneOutsiderGood);
            var BaneOutsiderLawful = Resources.GetBlueprint<BlueprintWeaponEnchantment>("3a6f564c8ea2d1941a45b19fa16e59f5");
            CreateTrasferMagicWeaponEnchant("Lawful Outsider Bane", BaneOutsiderLawful);
            var BaneOutsiderNeutral = Resources.GetBlueprint<BlueprintWeaponEnchantment>("4e30e79c500e5af4b86a205cc20436f2");
            CreateTrasferMagicWeaponEnchant("Neutral Outsider Bane", BaneOutsiderNeutral);
            var BanePlant = Resources.GetBlueprint<BlueprintWeaponEnchantment>("0b761b6ed6375114d8d01525d44be5a9");
            CreateTrasferMagicWeaponEnchant("Plant Bane", BanePlant);
            var BaneUndead = Resources.GetBlueprint<BlueprintWeaponEnchantment>("eebb4d3f20b8caa43af1fed8f2773328");
            CreateTrasferMagicWeaponEnchant("Undead Bane", BaneUndead);
            var BaneVermin = Resources.GetBlueprint<BlueprintWeaponEnchantment>("c3428441c00354c4fabe27629c6c64dd");
            CreateTrasferMagicWeaponEnchant("Vermin Bane", BaneVermin);
            var Bleed = Resources.GetBlueprint<BlueprintWeaponEnchantment>("ac0108944bfaa7e48aa74f407e3944e3");
            CreateTrasferMagicWeaponEnchant("Bleed", Bleed);
            var BrilliantEnergy = Resources.GetBlueprint<BlueprintWeaponEnchantment>("66e9e299c9002ea4bb65b6f300e43770");
            CreateTrasferMagicWeaponEnchant("Brilliant Energy", BrilliantEnergy);
            var Corrosive = Resources.GetBlueprint<BlueprintWeaponEnchantment>("633b38ff1d11de64a91d490c683ab1c8");
            CreateTrasferMagicWeaponEnchant("Corrosive", Corrosive);
            var Disruption = Resources.GetBlueprint<BlueprintWeaponEnchantment>("0f20d79b7049c0f4ca54ca3d1ea44baa");
            CreateTrasferMagicWeaponEnchant("Disruption", Disruption);
            var Flaming = Resources.GetBlueprint<BlueprintWeaponEnchantment>("30f90becaaac51f41bf56641966c4121");
            CreateTrasferMagicWeaponEnchant("Flaming", Flaming);
            var FlamingBurst = Resources.GetBlueprint<BlueprintWeaponEnchantment>("3f032a3cd54e57649a0cdad0434bf221");
            CreateTrasferMagicWeaponEnchant("Flaming Burst", FlamingBurst);
            var Frost = Resources.GetBlueprint<BlueprintWeaponEnchantment>("421e54078b7719d40915ce0672511d0b");
            CreateTrasferMagicWeaponEnchant("Frost", Frost);
            var Furious = Resources.GetBlueprint<BlueprintWeaponEnchantment>("b606a3f5daa76cc40add055613970d2a");
            CreateTrasferMagicWeaponEnchant("Furious", Furious);
            var GhostTouch = Resources.GetBlueprint<BlueprintWeaponEnchantment>("47857e1a5a3ec1a46adf6491b1423b4f");
            CreateTrasferMagicWeaponEnchant("Ghost Touch", GhostTouch);
            var HeartSeeker = Resources.GetBlueprint<BlueprintWeaponEnchantment>("e252b26686ab66241afdf33f2adaead6");
            CreateTrasferMagicWeaponEnchant("Heartseeker", HeartSeeker);
            var Holy = Resources.GetBlueprint<BlueprintWeaponEnchantment>("28a9964d81fedae44bae3ca45710c140");
            CreateTrasferMagicWeaponEnchant("Holy", Holy);
            var IcyBurst = Resources.GetBlueprint<BlueprintWeaponEnchantment>("564a6924b246d254c920a7c44bf2a58b");
            CreateTrasferMagicWeaponEnchant("Icy Burst", IcyBurst);
            var Keen = Resources.GetBlueprint<BlueprintWeaponEnchantment>("102a9c8c9b7a75e4fb5844e79deaf4c0");
            CreateTrasferMagicWeaponEnchant("Keen", Keen);
            var Necrotic = Resources.GetBlueprint<BlueprintWeaponEnchantment>("bad4134798e182c4487819dce9b43003");
            CreateTrasferMagicWeaponEnchant("Necrotic", Necrotic);
            var Radiant = Resources.GetBlueprint<BlueprintWeaponEnchantment>("5ac5c88157f7dde48a2a5b24caf40131");
            CreateTrasferMagicWeaponEnchant("Radiant", Radiant);
            var Shock = Resources.GetBlueprint<BlueprintWeaponEnchantment>("7bda5277d36ad114f9f9fd21d0dab658");
            CreateTrasferMagicWeaponEnchant("Shock", Shock);
            var ShockingBurst = Resources.GetBlueprint<BlueprintWeaponEnchantment>("914d7ee77fb09d846924ca08bccee0ff");
            CreateTrasferMagicWeaponEnchant("Shocking Burst", ShockingBurst);
            var Speed = Resources.GetBlueprint<BlueprintWeaponEnchantment>("f1c0c50108025d546b2554674ea1c006");
            CreateTrasferMagicWeaponEnchant("Speed", Speed);
            var Thundering = Resources.GetBlueprint<BlueprintWeaponEnchantment>("690e762f7704e1f4aa1ac69ef0ce6a96");
            CreateTrasferMagicWeaponEnchant("Thundering", Thundering);
            var Unholy = Resources.GetBlueprint<BlueprintWeaponEnchantment>("d05753b8df780fc4bb55b318f06af453");
            CreateTrasferMagicWeaponEnchant("Unholy", Unholy);


            #endregion

            if (ModSettings.AddedContent.Miscellaneous.IsDisabled("Atifice Blessing Rework")) { return; }

            BlessingSelection.m_AllFeatures = BlessingSelection.m_AllFeatures.RemoveFromArray(OwlcatArtificeBlessing);
            SecondBlessingSelection.m_AllFeatures = SecondBlessingSelection.m_AllFeatures.RemoveFromArray(OwlcatArtificeBlessing);
            BlessingTools.RegisterBlessing(ArtificeBlessingFeature);

        }

        private static void CreateTrasferMagicWeaponEnchant(string enchantname, BlueprintWeaponEnchantment weaponEnchant) {

            var MasterStrikeToggleAbility = Resources.GetBlueprint<BlueprintActivatableAbility>("926bff1386d58824688363a3eeb98260");
            var WarpriestAspectOfWarBuff = Resources.GetBlueprint<BlueprintBuff>("27d14b07b52c2df42a4dcd6bfb840425");
            var BlessingResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("d128a6332e4ea7c4a9862b9fdb358cca");
            var ArtificeBlessingMajorMHBaseAbility = Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMajorMHBaseAbility").GetComponent<AbilityVariants>();
            var ArtificeBlessingMajorOHBaseAbility = Resources.GetModBlueprint<BlueprintAbility>("ArtificeBlessingMajorOHBaseAbility").GetComponent<AbilityVariants>();
            var QuickenBlessingArtificeFeature = Resources.GetModBlueprint<BlueprintFeature>("QuickenBlessingArtificeFeature");

            string enchantnameWithNoSpaces = enchantname.Replace(" ","");

            var mainhandbuff = Helpers.CreateBuff($"ArtificeBlessingMH{enchantnameWithNoSpaces}Buff", bp => {
                bp.SetName($"Transfer Magic - Main Hand - {enchantname}");
                bp.SetDescription($"Your main hand weapon has a temporarily copy of the {enchantname} weapon enchantment. If you are using this ability on a " +
                    "double weapon, only one end of the double weapon is affected. The copy lasts for 1 minute. You can stack multiple versions of this " +
                    "ability on the same weapon.");
                bp.m_Icon = MasterStrikeToggleAbility.Icon;
                bp.AddComponent<BuffEnchantAnyWeapon>(c => {
                    c.m_EnchantmentBlueprint = weaponEnchant.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.PrimaryHand;
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var offhandbuff = Helpers.CreateBuff($"ArtificeBlessingOH{enchantnameWithNoSpaces}Buff", bp => {
                bp.SetName($"Transfer Magic - Off Hand - {enchantname}");
                bp.SetDescription($"Your off hand weapon has a temporarily copy of the {enchantname} weapon enchantment. If you are using this ability on a " +
                    "double weapon, only one end of the double weapon is affected. The copy lasts for 1 minute. You can stack multiple versions of this " +
                    "ability on the same weapon.");
                bp.m_Icon = MasterStrikeToggleAbility.Icon;
                bp.AddComponent<BuffEnchantAnyWeapon>(c => {
                    c.m_EnchantmentBlueprint = weaponEnchant.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.SecondaryHand;
                });
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
            var mainhandability = Helpers.CreateBlueprint<BlueprintAbility>($"ArtificeBlessingMH{enchantnameWithNoSpaces}Ability", bp => {
                bp.SetName($"Transfer Magic - Main Hand - {enchantname}");
                bp.SetDescription($"You can temporarily copy the {enchantname} weapon enchantment to another weapon. If you are using this ability on " +
                    $"a double weapon, only one end of the double weapon is affected. The copy lasts for 1 minute. You can use this ability multiple times " +
                    $"on the same weapon.");
                bp.m_Icon = MasterStrikeToggleAbility.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = mainhandbuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.One,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<CheckWeaponEnchant>(c => {
                    c.m_Enchantment = weaponEnchant.ToReference<BlueprintItemEnchantmentReference>();
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"ArtificeBlessingMH{enchantnameWithNoSpaces}Ability.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var offhandability = Helpers.CreateBlueprint<BlueprintAbility>($"ArtificeBlessingOH{enchantnameWithNoSpaces}Ability", bp => {
                bp.SetName($"Transfer Magic - Off Hand - {enchantname}");
                bp.SetDescription($"You can temporarily copy the {enchantname} weapon enchantment to another weapon. If you are using this ability on " +
                    $"a double weapon, only one end of the double weapon is affected. The copy lasts for 1 minute. You can use this ability multiple times " +
                    $"on the same weapon.");
                bp.m_Icon = MasterStrikeToggleAbility.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = offhandbuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.One,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<CheckWeaponEnchant>(c => {
                    c.m_Enchantment = weaponEnchant.ToReference<BlueprintItemEnchantmentReference>();
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = Helpers.CreateString($"ArtificeBlessingOH{enchantnameWithNoSpaces}Ability.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var mainhandabilityswift = Helpers.CreateBlueprint<BlueprintAbility>($"ArtificeBlessingMH{enchantnameWithNoSpaces}AbilitySwift", bp => {
                bp.SetName($"Transfer Magic - Main Hand - {enchantname}");
                bp.SetDescription($"You can temporarily copy the {enchantname} weapon enchantment to another weapon. If you are using this ability on " +
                    $"a double weapon, only one end of the double weapon is affected. The copy lasts for 1 minute. You can use this ability multiple times " +
                    $"on the same weapon.");
                bp.m_Icon = MasterStrikeToggleAbility.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = mainhandbuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.One,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<CheckWeaponEnchant>(c => {
                    c.m_Enchantment = weaponEnchant.ToReference<BlueprintItemEnchantmentReference>();
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() { 
                        WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>(),
                        WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = QuickenBlessingArtificeFeature.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.LocalizedDuration = Helpers.CreateString($"ArtificeBlessingMH{enchantnameWithNoSpaces}AbilitySwift.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var offhandabilityswift = Helpers.CreateBlueprint<BlueprintAbility>($"ArtificeBlessingOH{enchantnameWithNoSpaces}AbilitySwift", bp => {
                bp.SetName($"Transfer Magic - Off Hand - {enchantname}");
                bp.SetDescription($"You can temporarily copy the {enchantname} weapon enchantment to another weapon. If you are using this ability on " +
                    $"a double weapon, only one end of the double weapon is affected. The copy lasts for 1 minute. You can use this ability multiple times " +
                    $"on the same weapon.");
                bp.m_Icon = MasterStrikeToggleAbility.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = offhandbuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.One,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<CheckWeaponEnchant>(c => {
                    c.m_Enchantment = weaponEnchant.ToReference<BlueprintItemEnchantmentReference>();
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = BlessingResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 2;
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>() {
                        WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>(),
                        WarpriestAspectOfWarBuff.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = QuickenBlessingArtificeFeature.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.LocalizedDuration = Helpers.CreateString($"ArtificeBlessingOH{enchantnameWithNoSpaces}AbilitySwift.Duration", "1 minute");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            ArtificeBlessingMajorMHBaseAbility.m_Variants = ArtificeBlessingMajorMHBaseAbility.m_Variants.AppendToArray(mainhandability.ToReference<BlueprintAbilityReference>(), mainhandabilityswift.ToReference<BlueprintAbilityReference>());
            ArtificeBlessingMajorOHBaseAbility.m_Variants = ArtificeBlessingMajorOHBaseAbility.m_Variants.AppendToArray(offhandability.ToReference<BlueprintAbilityReference>(), offhandabilityswift.ToReference<BlueprintAbilityReference>());

        }
    }
}
