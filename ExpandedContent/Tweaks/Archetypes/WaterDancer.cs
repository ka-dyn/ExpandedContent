using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Classes;
using ExpandedContent.Tweaks.Classes.DrakeClass;
using ExpandedContent.Tweaks.Components;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.Items.Weapons;
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
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Class.Kineticist;
using Kingmaker.UnitLogic.Class.Kineticist.Actions;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System.Collections.Generic;
using TabletopTweaks.Core.NewComponents.Properties;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class WaterDancer {
        public static void AddWaterDancer() {

            var MonkClass = Resources.GetBlueprint<BlueprintCharacterClass>("e8f21e5b58e0569468e420ebea456124");
            var KineticistClass = Resources.GetBlueprint<BlueprintCharacterClass>("42a455d9ec1ad924d889272429eb8391");
            var BurnResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("066ac4b762e32be4b953703174ed925c");
            var BurnPerRoundResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("90eec36e1bca15a4dafc7b17916debf4");
            var BurnEffectBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("95b1c0d55f30996429a3a4eba4d2b4a6");
            var GatherPowerAbility = Resources.GetBlueprintReference<BlueprintAbilityReference>("6dcbffb8012ba2a4cb4ac374a33e2d9a");
            var GatherPowerFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("0601925a028b788469365d5f8f39e14a");
            var GatherPowerAbilitiesFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("71f526b1d4b50b94582b0b9cbe12b0e0");
            var GatherPowerBuffI = Resources.GetBlueprintReference<BlueprintBuffReference>("e6b8b31e1f8c524458dc62e8a763cfb1");
            var GatherPowerBuffII = Resources.GetBlueprintReference<BlueprintBuffReference>("3a2bfdc8bf74c5c4aafb97591f6e4282");
            var GatherPowerBuffIII = Resources.GetBlueprintReference<BlueprintBuffReference>("82eb0c274eddd8849bb89a8e6dbc65f8");
            var KineticBladeEnableBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("426a9c079ee7ac34aa8e0054f2218074");
            var KineticSharpshooterQuiverBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("dcc5e9da41444def92d90cfcc5f8a133");
            var ElementalBastionBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("99953956704788444964899b5b8e96ab");
            var ElementalFocusWater = Resources.GetBlueprint<BlueprintProgression>("7ab8947ce2e19c44a9edcf5fd1466686");
            var ElementalFocusSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("1f3a15a3ae8a5524ab8b97f469bf4e3d");
            var Kinetic_WaterBlast00_Projectile = Resources.GetBlueprintReference<BlueprintProjectileReference>("06e268d6a2b5a3a438c2dd52d68bfef6");
            var ColdCone15Feet00 = Resources.GetBlueprint<BlueprintProjectile>("5af8b717a209fd444a1e4d077ed776f0");
            var KineticBlastProgression = Resources.GetBlueprint<BlueprintProgression>("30a5b8cf728bd4a4d8d90fc4953e322e");
            var KineticBlastFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("93efbde2764b5504e98e6824cab3d27c");
            var KineticistMainStatProperty = Resources.GetBlueprintReference<BlueprintUnitPropertyReference>("f897845bbbc008d4f9c1c4a03e22357a");
            var ColdBlastProgression = Resources.GetBlueprint<BlueprintProgression>("dbb1159b0e8137c4ea20434a854ae6a8");
            var MetakinsisEmpowerBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("f5f3aa17dd579ff49879923fb7bc2adb");
            var MonkWeaponProficiency = Resources.GetBlueprintReference<BlueprintFeatureReference>("c7d6f5244c617734a8a76b6785a752b4");

            var MonkFlurryOfBlowsFeatureUnlock = Resources.GetBlueprintReference<BlueprintFeatureReference>("fd99770e6bd240a4aab70f7af103e56a");
            var MonkFlurryOfBlowsFeatureLevel11Unlock = Resources.GetBlueprintReference<BlueprintFeatureReference>("a34b8a9fcc9024b42bacfd5e6b614bfa");
            var MonkBonusFeatSelectionLevel1 = Resources.GetBlueprint<BlueprintFeatureSelection>("ac3b7f5c11bce4e44aeb332f66b75bab");
            var MonkBonusFeatSelectionLevel6 = Resources.GetBlueprint<BlueprintFeatureSelection>("b993f42cb119b4f40ac423ae76394374");
            var MonkBonusFeatSelectionLevel10 = Resources.GetBlueprint<BlueprintFeatureSelection>("1051170c612d5b844bfaa817d6f4cfff");

            var MonkACBonusUnlock = Resources.GetBlueprintReference<BlueprintFeatureReference>("2615c5f87b3d72b42ac0e73b56d895e0");
            var ScaledFistACBonusUnlock = Resources.GetBlueprintReference<BlueprintFeatureReference>("2a8922e28b3eba54fa7a244f7b05bd9e");

            var StunningFist = Resources.GetBlueprintReference<BlueprintFeatureReference>("a29a582c3daa4c24bb0e991c596ccb28");
            var StunningFistFatigueFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("819645da2e446f84d9b168ed1676ec29");
            var StunningFistSickenedFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("d256ab3837538cc489d4b571e3a813eb");

            var MonkUnarmedSrike = Resources.GetBlueprintReference<BlueprintFeatureReference>("c3fbeb2ffebaaa64aa38ce7a0bb18fb0");
            var MonkUnarmedSrikeLevel4 = Resources.GetBlueprintReference<BlueprintFeatureReference>("8267a0695a4df3f4ca508499e6164b98");
            var MonkUnarmedSrikeLevel8 = Resources.GetBlueprintReference<BlueprintFeatureReference>("f790a36b5d6f85a45a41244f50b947ca");
            var MonkUnarmedSrikeLevel12 = Resources.GetBlueprintReference<BlueprintFeatureReference>("b3889f445dbe42948b8bb1ba02e6d949");
            var MonkUnarmedSrikeLevel16 = Resources.GetBlueprintReference<BlueprintFeatureReference>("078636a2ce835e44394bb49a930da230");
            var MonkUnarmedSrikeLevel20 = Resources.GetBlueprintReference<BlueprintFeatureReference>("df38e56fa8b3f0f469d55f9aa26b3f5c");
            var Unarmed1d6 = Resources.GetBlueprintReference<BlueprintItemWeaponReference>("f60c5a820b69fb243a4cce5d1d07d06e");
            var UnarmedWeaponType = Resources.GetBlueprintReference<BlueprintWeaponTypeReference>("fcca8e6b85d19b14786ba1ab553e23ad");

            var KiPowerFeature = Resources.GetBlueprint<BlueprintFeature>("e9590244effb4be4f830b1e3fffced13");
            var ScaledFistPowerFeature = Resources.GetBlueprint<BlueprintFeature>("ae98ab7bda409ef4bb39149a212d6732");
            var ExtraKi = Resources.GetBlueprint<BlueprintFeature>("231a2a603d0b437e939553e6da3e7247");
            var AbundantKiPool = Resources.GetBlueprint<BlueprintFeature>("e8752f9126d986748b10d0bdac693264");
            var ScaledFistPowerResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("7d002c1025fbfe2458f1509bf7a89ce1");
            var KiAbundantStep = Resources.GetBlueprint<BlueprintAbility>("336a841704b7e2341b51f89fc9491f54");
            var DarkLurkerDimensionDoor = Resources.GetBlueprintReference<BlueprintAbilityReference>("e439fcfa702d4cd294e9d26c337ab77b");

            var KiPowerFeatureSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("3049386713ff04245a38b32483362551");
            var WildTalentSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("5c883ae0cd6d7d5448b7a420f51f8459");
            var ExtraWildTalentSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("0f73730cf1b44ee882671e55d5f6e471");

            var MonkStyleStrike = Resources.GetBlueprintReference<BlueprintFeatureSelectionReference>("7bc6a93f6e48eff49be5b0cde83c9450");
            var GeyserStyleIcon = AssetLoader.LoadInternal("Skills", "Icon_GeyserStyle.jpg");
            var RainStyleIcon = AssetLoader.LoadInternal("Skills", "Icon_RainStyle.jpg");
            var RiverStyleIcon = AssetLoader.LoadInternal("Skills", "Icon_RiverStyle.jpg");
            var WaterfallStyleIcon = AssetLoader.LoadInternal("Skills", "Icon_WaterfallStyle.jpg");
            var WaveStyleIcon = AssetLoader.LoadInternal("Skills", "Icon_WaveStyle.jpg");


            var WaterDancerArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("WaterDancerArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"WaterDancerArchetype.Name", "Water Dancer");
                bp.LocalizedDescription = Helpers.CreateString($"WaterDancerArchetype.Description", "Water dancers derive their martial training from ancient nereid traditions " +
                    "jealously guarded by these enigmatic fey.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"WaterDancerArchetype.Description", "Water dancers derive their martial training from ancient nereid traditions " +
                    "jealously guarded by these enigmatic fey.");
                bp.OverrideAttributeRecommendations = true;
                bp.RecommendedAttributes = new StatType[] { StatType.Strength, StatType.Constitution, StatType.Charisma };
                bp.NotRecommendedAttributes = new StatType[] { };
                bp.AddComponent<PrerequisiteNoClassLevel>(c => {
                    c.m_CharacterClass = KineticistClass.ToReference<BlueprintCharacterClassReference>();
                    c.HideInUI = true;
                });
            });
            KineticistClass.AddComponent<PrerequisiteNoArchetype>(c => {
                c.m_CharacterClass = MonkClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = WaterDancerArchetype.ToReference<BlueprintArchetypeReference>();
                c.HideInUI = true;
            });



            var WaterDancerProficiency = Helpers.CreateBlueprint<BlueprintFeature>("WaterDancerProficiency", bp => {
                bp.SetName("Water Dancer Proficiencies");
                bp.SetDescription("Water dancers are proficient with kinetic blasts and unarmed strikes. " +
                    "Water dancers are not proficient with any armor or shields. " +
                    "When wearing armor, using a shield, or carrying a medium or heavy load, a water dancer loses their {g|Encyclopedia:Armor_Class}AC{/g} {g|Encyclopedia:Bonus}bonus{/g}, " +
                    "as well as his fast movement ability.");
                bp.AddComponent<AddProficiencies>(c => {
                    c.m_RaceRestriction = new BlueprintRaceReference();
                    c.ArmorProficiencies = new ArmorProficiencyGroup[0];
                    c.WeaponProficiencies = new WeaponCategory[] {                        
                        WeaponCategory.KineticBlast
                    };
                });
                bp.IsClassFeature = true;
            });

            var WaterDancerUnarmedStrike = Helpers.CreateBlueprint<BlueprintFeature>("WaterDancerUnarmedStrike", bp => {
                bp.SetName("Unarmed Strike");
                bp.SetDescription("A water dancer gains the unarmed strike monk class feature but treats his unarmed strike damage as that of a monk 4 levels lower (minimum 1st level)." +
                    "\r\nAt 1st level, a monk gains Improved Unarmed Strike as a bonus {g|Encyclopedia:Feat}feat{/g}. The {g|Encyclopedia:Damage}damage{/g} dealt by a Medium monk's " +
                    "{g|Encyclopedia:UnarmedAttack}unarmed strike{/g} increases with level: {g|Encyclopedia:Dice}1d6{/g} at levels 1–3, 1d8 at levels 4–7, 1d10 at levels 8–11, 2d6 " +
                    "at levels 12–15, 2d8 at levels 16–19, and 2d10 at level 20.\nIf the monk is Small, his unarmed strike damage increases as follows: 1d4 at levels 1–3, 1d6 at " +
                    "levels 4–7, 1d8 at levels 8–11, 1d10 at levels 12–15, 2d6 at levels 16–19, and 2d8 at level 20.\nIf the monk is Large, his unarmed strike damage increases as " +
                    "follows: 1d8 at levels 1–3, 2d6 at levels 4–7, 2d8 at levels 8–11, 3d6 at levels 12–15, 3d8 at levels 16–19, and 4d8 at level 20.");
                bp.AddComponent<EmptyHandWeaponOverride>(c => {
                    c.m_Weapon = Unarmed1d6;
                    c.IsPermanent = true;
                    c.IsMonkUnarmedStrike = true;
                });
                bp.IsClassFeature = true;
            });

            var WaterDancerBurnFeature = Helpers.CreateBlueprint<BlueprintFeature>("WaterDancerBurnFeature", bp => {
                bp.SetName("Burn");
                bp.SetDescription("At 1st level, a water dancer can overexert themselves to channel more power than normal, pushing past the limit of what is safe for their body by accepting burn. " +
                    "For each point of burn they accept, a water dancer takes (1 per {g|Encyclopedia:Character_Level}character level{/g}) points of nonlethal {g|Encyclopedia:Damage}damage{/g}. " +
                    "This damage can't be {g|Encyclopedia:Healing}healed{/g} by any means other than getting a full night's {g|Encyclopedia:Rest}rest{/g}, which removes all burn, or by using the ki cooling ability. " +
                    "\nA water dancer can accept only 1 point of burn per {g|Encyclopedia:Combat_Round}round{/g}. This limit rises to 2 points of burn at 6th level, and rises by 1 additional point every 3 levels thereafter. " +
                    "A water dancer can't choose to accept burn if it would put their total number of points of burn higher than (3 + their {g|Encyclopedia:Constitution}Constitution{/g} modifier), though they can be forced " +
                    "to accept more burn from a source outside their control.");
                bp.AddComponent<AddAbilityResources>(c => {
                    c.UseThisAsResource = false;
                    c.m_Resource = BurnResource;
                    c.Amount = 0;
                    c.RestoreAmount = false;
                    c.RestoreOnLevelUp = false;
                });
                bp.AddComponent<AddKineticistPart>(c => {
                    c.m_Class = MonkClass.ToReference<BlueprintCharacterClassReference>();
                    c.MainStat = StatType.Charisma;
                    c.m_MaxBurn = BurnResource;
                    c.m_MaxBurnPerRound = BurnPerRoundResource;
                    c.m_GatherPowerAbility = GatherPowerAbility;
                    c.m_GatherPowerIncreaseFeature = GatherPowerFeature;
                    c.m_GatherPowerBuff1 = GatherPowerBuffI;
                    c.m_GatherPowerBuff2 = GatherPowerBuffII;
                    c.m_GatherPowerBuff3 = GatherPowerBuffIII;
                    c.m_Blasts = new BlueprintAbilityReference[] { 
                    
                    };
                    c.m_BladeActivatedBuff = KineticBladeEnableBuff;
                    c.m_QuiverBuff = KineticSharpshooterQuiverBuff;
                    c.m_CanGatherPowerWithShieldBuff = ElementalBastionBuff;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.UseThisAsResource = false;
                    c.m_Resource = BurnPerRoundResource;
                    c.Amount = 0;
                    c.RestoreAmount = false;
                    c.RestoreOnLevelUp = true;
                });
                bp.AddComponent<AddKineticistBurnValueChangedTrigger>(c => {
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BurnEffectBuff,
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0,
                                m_IsExtendable = true
                            },
                            IsFromSpell = false,
                            IsNotDispelable = false,
                            ToCaster = false,
                            AsChild = true,
                            SameDuration = false,
                            NotLinkToAreaEffect = false,
                            IgnoreParentContext = false
                        }
                        );
                });
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });

            #region Normal Blasts
            var WaterDancerElementalFocusFeatureSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("WaterDancerElementalFocusFeatureSelection", bp => {
                bp.SetName("Elemental Focus");
                bp.SetDescription("A water dancer gains the elemental focus ability of the kineticist class, but must choose water as his element. " +
                    "He gains the basic hydrokinesis wild talent as normal, and gains the kinetic blast feature of the kineticist class for the water element, using his monk level as his effective kineticist level. " +
                    "He can’t use his kinetic blast when armored or encumbered.");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.Group = FeatureGroup.None;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    ElementalFocusWater.ToReference<BlueprintFeatureReference>()
                };
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ElementalFocusSelection.ToReference<BlueprintUnitFactReference>() };
                });
            });
            ElementalFocusWater.m_Classes = ElementalFocusWater.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = MonkClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0,
                });
            ElementalFocusWater.m_Archetypes = ElementalFocusWater.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = WaterDancerArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0,
                }
                );
            KineticBlastProgression.m_Classes = KineticBlastProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = MonkClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0,
                });
            KineticBlastProgression.m_Archetypes = KineticBlastProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = WaterDancerArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0,
                }
                );
            ColdBlastProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.CheckInProgression = false;
                c.HideInUI = false;
                c.m_CharacterClass = MonkClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = WaterDancerArchetype.ToReference<BlueprintArchetypeReference>();
            });
            #endregion
            #region Ki
            var WaterDancerKiPowerFeature = Helpers.CreateBlueprint<BlueprintFeature>("WaterDancerKiPowerFeature", bp => {
                bp.SetName("Ki Pool");
                bp.SetDescription("At 3rd level, a monk gains a pool of ki points, supernatural energy he can use to accomplish amazing feats. " +
                    "The number of points in a monk's ki pool is equal to 1/2 his monk level + his {g|Encyclopedia:Charisma}Charisma{/g} modifier. " +
                    "As long as he has at least 1 point in his ki pool, he can make a ki strike.\r\nAt 3rd level, ki strike allows his " +
                    "{g|Encyclopedia:UnarmedAttack}unarmed attacks{/g} to be treated as magic weapons for the purpose of overcoming " +
                    "{g|Encyclopedia:Damage_Reduction}damage reduction{/g}.\r\nAt 7th level, his unarmed {g|Encyclopedia:Attack}attacks{/g} are " +
                    "also treated as cold iron and silver for the purpose of overcoming damage reduction.\r\nAt 10th level, his unarmed attacks are " +
                    "also treated as {g|Encyclopedia:Alignment}lawful{/g} weapons for the purpose of overcoming damage reduction.\r\nAt 16th level, " +
                    "his unarmed attacks are treated as adamantine weapons for the purpose of overcoming damage reduction and bypassing hardness.\r\nBy " +
                    "spending 1 point from his ki pool as a {g|Encyclopedia:Swift_Action}swift action{/g}, a monk can make one additional unarmed " +
                    "strike at his highest {g|Encyclopedia:BAB}attack bonus{/g} when making a flurry of blows attack. This bonus attack stacks " +
                    "with all bonus attacks gained from flurry of blows, as well as those from {g|SpellsHaste}haste{/g} and similar effects. " +
                    "A monk gains additional powers that consume points from his ki pool as he gains levels.\r\nThe ki pool is replenished each " +
                    "morning after 8 hours of {g|Encyclopedia:Rest}rest{/g} or meditation; these hours do not need to be consecutive.");
                bp.CopyComponentArray(ScaledFistPowerFeature);
                bp.IsClassFeature = true;
                bp.IsPrerequisiteFor = new System.Collections.Generic.List<BlueprintFeatureReference>() {
                    ExtraKi.ToReference<BlueprintFeatureReference>(),
                    AbundantKiPool.ToReference<BlueprintFeatureReference>()
                };
            });
            ExtraKi.AddComponent<PrerequisiteFeature>(c => {
                c.Group = Prerequisite.GroupType.Any;
                c.CheckInProgression = false;
                c.HideInUI = false;
                c.IsFeatureSelectionWhiteList = false;
                c.m_Feature = WaterDancerKiPowerFeature.ToReference<BlueprintFeatureReference>();
            });
            AbundantKiPool.AddComponent<PrerequisiteFeature>(c => {
                c.Group = Prerequisite.GroupType.Any;
                c.CheckInProgression = false;
                c.HideInUI = false;
                c.IsFeatureSelectionWhiteList = false;
                c.m_Feature = WaterDancerKiPowerFeature.ToReference<BlueprintFeatureReference>();
            });
            #endregion
            #region Nereid's Grace
            var NereidsGraceProperty = Helpers.CreateBlueprint<BlueprintUnitProperty>("NereidsGraceProperty", bp => {
                bp.AddComponent<CompositeCustomPropertyGetter>(c => {
                    c.CalculationMode = CompositeCustomPropertyGetter.Mode.Lowest;
                    c.Properties = new CompositeCustomPropertyGetter.ComplexCustomProperty[] {
                        new CompositeCustomPropertyGetter.ComplexCustomProperty() {
                            Property = new ClassLevelGetter() {
                                m_Class = MonkClass.ToReference<BlueprintCharacterClassReference>()
                            }
                        },
                        new CompositeCustomPropertyGetter.ComplexCustomProperty() {
                            Property = new SimplePropertyGetter() {
                                Property = UnitProperty.StatBonusCharisma
                            }
                        }
                    };
                });
            });
            var NereidsGraceBuff = Helpers.CreateBuff("NereidsGraceBuff", bp => {
                bp.SetName("Nereid’s Grace Dodge Bonus Buff");
                bp.SetDescription("");
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_UseMin = true;
                    c.m_Min = 0;
                    c.m_CustomProperty = NereidsGraceProperty.ToReference<BlueprintUnitPropertyReference>();
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Dodge;
                    c.Stat = StatType.AC;
                    c.Multiplier = 1;
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
                bp.AddComponent<RecalculateOnStatChange>(c => {
                    c.UseKineticistMainStat = false;
                    c.Stat = StatType.Charisma;
                });
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi| BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
            });
            var NereidsGraceDodgeBonus = Helpers.CreateBlueprint<BlueprintFeature>("NereidsGraceDodgeBonus", bp => {
                bp.SetName("Nereid’s Grace Dodge Bonus Feature");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { NereidsGraceBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.HideNotAvailibleInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });
            var NereidsGraceFeature = Helpers.CreateBlueprint<BlueprintFeature>("NereidsGraceFeature", bp => {
                bp.SetName("Nereid’s Grace");
                bp.SetDescription("When unarmored, a water dancer adds 1 point of Charisma bonus per monk level to his Armor Class as a dodge bonus. " +
                    "If he is caught flat-footed or otherwise denied his Dexterity bonus, he also loses this bonus. " +
                    "He uses his Charisma score instead of his Wisdom score to determine the size of his ki pool and the DC and effects of monk class features.");
                bp.AddComponent<MonkNoArmorFeatureUnlock>(c => {
                    c.m_NewFact = NereidsGraceDodgeBonus.ToReference<BlueprintUnitFactReference>();
                });
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            #endregion
            #region Wild Talents
            var WaterDancerWildTalentSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("WaterDancerWildTalentSelection", bp => {
                bp.SetName("Wild Talents");
                bp.SetDescription("At 2nd level and every 4 levels thereafter, a water dancer selects a new utility wild talent from the list of options available to the kineticist class, " +
                    "treating his monk level – 2 (minimum 1) as his kineticist level for the purpose of fulfilling prerequisites. " +
                    "He can select only universal wild talents or those that match his element. ");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.Group = FeatureGroup.KineticWildTalent;
                bp.Mode = SelectionMode.Default;
                bp.m_AllFeatures = WildTalentSelection.m_AllFeatures;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WildTalentSelection.ToReference<BlueprintUnitFactReference>() };
                });
            });

            #region Prerequisite Updates
            var WaterDancerWildTalentPrereqs = new BlueprintFeature[] {
                Resources.GetBlueprint<BlueprintFeature>("c73b37aaa2b82b44686c56db8ce14e7f"), //HealingBurst Feature
                Resources.GetBlueprint<BlueprintFeature>("ed01d50910ae67b4dadc050f16d93bdf"), //KineticRestoration Feature
                Resources.GetBlueprint<BlueprintFeature>("0377fcf4c10871f4187809d273af7f5d"), //KineticRevifcation Feature
                Resources.GetBlueprint<BlueprintFeature>("7c4bbfe3b089a8a4ebcd2401995230a4") //TidalWave Feature
            };
            foreach (var wildtalentfeature in WaterDancerWildTalentPrereqs) {
                wildtalentfeature.GetComponent<PrerequisiteClassLevel>().TemporaryContext(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    
                });
                wildtalentfeature.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.m_CharacterClass = MonkClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = WaterDancerArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = wildtalentfeature.GetComponent<PrerequisiteClassLevel>().Level+2;
                });
            }
            #endregion            
            #region ContextCalculateAbilityParamsBasedOnClass Replacer
            var WaterDancerWildTalentAbilities = new BlueprintAbility[] {
                Resources.GetBlueprint<BlueprintAbility>("3c030a62a0efa1c419ecf315a9d694ef"), //Slick Feature
                Resources.GetBlueprint<BlueprintAbility>("566e989d7c1d1d14f8371e35f7c5d9b8"), //SlickShort Feature
                Resources.GetBlueprint<BlueprintAbility>("d8d451ed3c919a4438cde74cd145b981"), //TidalWave Feature
            };
            foreach (var wildtalentAbility in WaterDancerWildTalentAbilities) {
                wildtalentAbility.RemoveComponents<ContextCalculateAbilityParamsBasedOnClass>();
                wildtalentAbility.AddComponent<ContextCalculateAbilityParamsBasedOnHigherClass>(c => {
                    c.UseKineticistMainStat = true;
                    c.StatType = StatType.Charisma;
                    c.m_Classes = new BlueprintCharacterClassReference[] { 
                        KineticistClass.ToReference<BlueprintCharacterClassReference>(),
                        MonkClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] { 
                        WaterDancerArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                });
            }
            #endregion
            #endregion
            #region Ki Cooling
            var WaterDancerKiCoolingAbility = Helpers.CreateBlueprint<BlueprintAbility>("WaterDancerKiCoolingAbility", bp => {
                bp.SetName("Ki Cooling");
                bp.SetDescription("Spend 2 points from your ki pool to lower your kinetic burn by 1.");
                bp.m_Icon = KiPowerFeatureSelection.Icon;
                bp.AddComponent<AbilityResourceLogic>(c => {                    
                    c.m_RequiredResource = ScaledFistPowerResource;
                    c.m_IsSpendResource = true;
                    c.CostIsCustom = false;
                    c.Amount = 2;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionHealBurn() {
                            Value = 1
                        }                        
                        );
                });
                bp.Type = AbilityType.Extraordinary;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var WaterDancerKiCoolingFeature = Helpers.CreateBlueprint<BlueprintFeature>("WaterDancerKiCoolingFeature", bp => {
                bp.SetName("Ki Cooling");
                bp.SetDescription("At 4th level, a water dancer can spend 2 points from their ki pool to lower their burn by 1.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WaterDancerKiCoolingAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.IsClassFeature = true;
            });


            #endregion

            #region Water Style Strikes
            #region Style Buffs
            var WaterStyleGeyserCalmBuff = Helpers.CreateBuff("WaterStyleGeyserCalmBuff", bp => {
                bp.SetName("Geyser Style - Calm");
                bp.SetDescription("Spend 1 point from your ki pool as a swift action, your first unarmed strike each round ignores damage reduction for one minute.");
                bp.m_Icon = GeyserStyleIcon;
                bp.AddComponent<IgnoreDamageReductionOnAttack>(c => {
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = true;
                    c.CriticalHit = false;
                    c.m_WeaponType = UnarmedWeaponType;
                    c.CheckEnemyFact = false;
                    c.m_CheckedFact = null;
                    c.OnlyNaturalAttacks = false;
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var WaterStyleGeyserRapidBuff = Helpers.CreateBuff("WaterStyleGeyserRapidBuff", bp => {
                bp.SetName("Geyser Style - Rapid");
                bp.SetDescription("Accept 1 point of burn as a swift action, the next unarmed strike that hits deals extra damage equal to your blast damage, " +
                    "unarmed strikes this round ignore damage reduction.");
                bp.m_Icon = GeyserStyleIcon;
                bp.AddComponent<IgnoreDamageReductionOnAttack>(c => {
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.CriticalHit = false;
                    c.m_WeaponType = UnarmedWeaponType;
                    c.CheckEnemyFact = false;
                    c.m_CheckedFact = null;
                    c.OnlyNaturalAttacks = false;
                });

                bp.AddComponent<AdditionalDiceOnAttack>(c => {
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnHit = false;
                    c.OnlyOnFirstHit = true;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = null;
                    c.CheckWeaponCategory = true;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.ReduceHPToZero = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet();
                    c.AllNaturalAndUnarmed = false;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.InitiatorConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[0] { }
                    };
                    c.TargetConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[0] { }
                    };
                    c.m_RandomizeDamage = false;//Maybe needs this
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.D6,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageDice
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Shared,
                            ValueShared = AbilitySharedValue.Damage
                        }
                    };
                    c.DamageType = new DamageTypeDescription() {
                        Type = DamageType.Physical,
                        Common = new DamageTypeDescription.CommomData() {
                            Reality = 0,
                            Alignment = 0,
                            Precision = false
                        },
                        Physical = new DamageTypeDescription.PhysicalData() {
                            Material = 0,
                            Form = PhysicalDamageForm.Bludgeoning,
                            Enhancement = 0,
                            EnhancementTotal = 0
                        },
                        Energy = DamageEnergyType.Fire
                    };
                    c.m_DamageEntries = null;
                });
                
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureRank;
                    c.m_Feature = KineticBlastFeature;
                    c.m_Progression = ContextRankProgression.AsIs;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_CustomProperty = KineticistMainStatProperty;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageDice
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageBonus
                        }
                    };
                    c.Modifier = 1;
                });
                //bp.AddComponent<ContextCalculateSharedValue>(c => {//Might not need this one, if dice are rolled in the AdditionalDiceOnAttack component, needs testing | ElementalFistAcidBuff |
                //    c.ValueType = AbilitySharedValue.Duration;//Final damage
                //    c.Value = new ContextDiceValue() {
                //        DiceType = DiceType.D6,
                //        DiceCountValue = new ContextValue() {
                //            ValueType = ContextValueType.Rank,
                //            ValueRank = AbilityRankType.DamageDice
                //        },
                //        BonusValue = new ContextValue() {
                //            ValueType = ContextValueType.Shared,
                //            ValueShared = AbilitySharedValue.Damage
                //        }
                //    };
                //    c.Modifier = 1;
                //});
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var WaterStyleRiverCalmBuff = Helpers.CreateBuff("WaterStyleRiverCalmBuff", bp => {
                bp.SetName("River Style - Calm");
                bp.SetDescription("Spend 1 point from your ki pool as a swift action, for one minute your first unarmed strike each round deals an extra 2d6 bludgeoning damage, increasing by 1d6 at levels 11 and 17.");
                bp.m_Icon = RiverStyleIcon;
                bp.AddComponent<AdditionalDiceOnAttack>(c => {
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnHit = false;
                    c.OnlyOnFirstHit = true;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = null;
                    c.CheckWeaponCategory = true;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.ReduceHPToZero = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet();
                    c.AllNaturalAndUnarmed = false;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.InitiatorConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[0] { }
                    };
                    c.TargetConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[0] { }
                    };
                    c.m_RandomizeDamage = false;//Maybe needs this
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.D6,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageDice
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 0
                        }
                    };
                    c.DamageType = new DamageTypeDescription() {
                        Type = DamageType.Physical,
                        Common = new DamageTypeDescription.CommomData() {
                            Reality = 0,
                            Alignment = 0,
                            Precision = false
                        },
                        Physical = new DamageTypeDescription.PhysicalData() {
                            Material = 0,
                            Form = PhysicalDamageForm.Bludgeoning,
                            Enhancement = 0,
                            EnhancementTotal = 0
                        },
                        Energy = DamageEnergyType.Fire
                    };
                    c.m_DamageEntries = null;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Class = new BlueprintCharacterClassReference[] { MonkClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Progression = ContextRankProgression.StartPlusDivStep;
                    c.m_StartLevel = -1;
                    c.m_StepLevel = 6;
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var WaterStyleRiverRapidBuff = Helpers.CreateBuff("WaterStyleRiverRapidBuff", bp => {
                bp.SetName("River Style - Rapid");
                bp.SetDescription("Accept 1 point of burn as a swift action, for one round your unarmed strikes deal an extra 2d8 bludgeoning damage, increasing by 1d8 at levels 11 and 17.");
                bp.m_Icon = RiverStyleIcon;
                bp.AddComponent<AdditionalDiceOnAttack>(c => {
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnHit = true;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = null;
                    c.CheckWeaponCategory = true;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.ReduceHPToZero = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet();
                    c.AllNaturalAndUnarmed = false;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.InitiatorConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[0] { }
                    };
                    c.TargetConditions = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[0] { }
                    };
                    c.m_RandomizeDamage = false;//Maybe needs this
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.D8,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageDice
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 0
                        }
                    };
                    c.DamageType = new DamageTypeDescription() {
                        Type = DamageType.Physical,
                        Common = new DamageTypeDescription.CommomData() {
                            Reality = 0,
                            Alignment = 0,
                            Precision = false
                        },
                        Physical = new DamageTypeDescription.PhysicalData() {
                            Material = 0,
                            Form = PhysicalDamageForm.Bludgeoning,
                            Enhancement = 0,
                            EnhancementTotal = 0
                        },
                        Energy = DamageEnergyType.Fire
                    };
                    c.m_DamageEntries = null;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Class = new BlueprintCharacterClassReference[] { MonkClass.ToReference<BlueprintCharacterClassReference>() };
                    c.m_Progression = ContextRankProgression.StartPlusDivStep;
                    c.m_StartLevel = -1;
                    c.m_StepLevel = 6;
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var WaterStyleWaterfallCalmBuff = Helpers.CreateBuff("WaterStyleWaterfallCalmBuff", bp => {
                bp.SetName("Waterfall Style - Calm");
                bp.SetDescription("Spend 1 point from your ki pool as a swift action, make a free trip attempt on critical unarmed hits for one minute. " +
                    "\nThese trip attempts use the {g|Encyclopedia:BAB}base attack bonus{/g} of the attack used to hit the foe and do not provoke an " +
                    "{g|Encyclopedia:Attack_Of_Opportunity}attack of opportunity{/g}.");
                bp.m_Icon = WaterfallStyleIcon;
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = true;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = true;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
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
                        new ContextActionCombatManeuver() {
                            Type = CombatManeuver.Trip,
                            OnSuccess = Helpers.CreateActionList(),
                            ReplaceStat = false,
                            NewStat = StatType.Unknown,
                            UseKineticistMainStat = false,
                            UseCastingStat = false,
                            UseCasterLevelAsBaseAttack = false,
                            UseBestMentalStat = false,
                            BatteringBlast = false
                        }
                        );
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var WaterStyleWaterfallRapidBuff = Helpers.CreateBuff("WaterStyleWaterfallRapidBuff", bp => {
                bp.SetName("Waterfall Style - Rapid");
                bp.SetDescription("Accept 1 point of burn as a swift action, the next unarmed strike that hits deals extra damage equal to half your blast damage, in addition to making a free trip attempt. " +
                    "\nThis trip attempt uses the {g|Encyclopedia:BAB}base attack bonus{/g} of the attack used to hit the foe and does not provoke an " +
                    "{g|Encyclopedia:Attack_Of_Opportunity}attack of opportunity{/g}.");
                bp.m_Icon = WaterfallStyleIcon;

                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureRank;
                    c.m_Feature = KineticBlastFeature;
                    c.m_Progression = ContextRankProgression.AsIs;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_CustomProperty = KineticistMainStatProperty;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageDice
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageBonus
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Duration;//Final damage
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.D6,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageDice
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Shared,
                            ValueShared = AbilitySharedValue.Damage
                        }
                    };
                    c.Modifier = 0.5f;
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = false;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = true;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = true;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
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
                        new ContextActionCombatManeuver() {
                            Type = CombatManeuver.Trip,
                            OnSuccess = Helpers.CreateActionList(),
                            ReplaceStat = false,
                            NewStat = StatType.Unknown,
                            UseKineticistMainStat = false,
                            UseCastingStat = false,
                            UseCasterLevelAsBaseAttack = false,
                            UseBestMentalStat = false,
                            BatteringBlast = false
                        },
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Physical,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = PhysicalDamageForm.Bludgeoning,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Fire
                            },
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
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.DamageDice,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Shared,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Duration,
                                    Property = UnitProperty.None
                                },
                            },
                            IsAoE = false,
                            HalfIfSaved = false,
                        }
                        );
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var WaterStyleWaveCalmBuff = Helpers.CreateBuff("WaterStyleWaveCalmBuff", bp => {
                bp.SetName("Wave Style - Calm");
                bp.SetDescription("Spend 1 point from your ki pool to enter a braced stance for 1 minute. This stance grants a +2 bonus to {g|Encyclopedia:CMD}CMD{/g} " +
                    "against all {g|Encyclopedia:Combat_Maneuvers}combat maneuvers{/g}.");
                bp.m_Icon = WaveStyleIcon;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.AdditionalCMD;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 2;
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var WaterStyleSuppressCalmBuff = Helpers.CreateBuff("WaterStyleSuppressCalmBuff", bp => {
                bp.SetName("Calm Style suppress");
                bp.SetDescription("");
                bp.AddComponent<SuppressBuffs>(c => {
                    c.m_Buffs = new BlueprintBuffReference[] { 
                        WaterStyleGeyserCalmBuff.ToReference<BlueprintBuffReference>(),
                        WaterStyleRiverCalmBuff.ToReference<BlueprintBuffReference>(),
                        WaterStyleWaterfallCalmBuff.ToReference<BlueprintBuffReference>(),
                        WaterStyleWaveCalmBuff.ToReference<BlueprintBuffReference>()
                    };
                });
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
            });
            #endregion
            #region Geyser
            var WaterStyleGeyserBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("WaterStyleGeyserBaseAbility", bp => {
                bp.SetName("Geyser Style");
                bp.SetDescription("Calm: Spend 1 point from your ki pool as a swift action, your first unarmed strike each round ignores damage reduction for one minute." +
                    "\nRapid: Accept 1 point of burn as a swift action, the next unarmed strike that hits deals extra damage equal to your blast damage, unarmed strikes this round ignore damage reduction.");
                bp.m_Icon = GeyserStyleIcon;
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var WaterStyleGeyserCalmAbility = Helpers.CreateBlueprint<BlueprintAbility>("WaterStyleGeyserCalmAbility", bp => {
                bp.SetName("Geyser Style - Calm");
                bp.SetDescription("Spend 1 point from your ki pool as a swift action, your first unarmed strike each round ignores damage reduction for one minute.");
                bp.m_Icon = GeyserStyleIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WaterStyleGeyserCalmBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleRiverCalmBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleWaterfallCalmBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleWaveCalmBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ScaledFistPowerResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.m_Parent = WaterStyleGeyserBaseAbility.ToReference<BlueprintAbilityReference>();
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var WaterStyleGeyserRapidAbility = Helpers.CreateBlueprint<BlueprintAbility>("WaterStyleGeyserRapidAbility", bp => {
                bp.SetName("Geyser Style - Rapid");
                bp.SetDescription("Accept 1 point of burn as a swift action, the next unarmed strike that hits deals extra damage equal to your blast damage, " +
                    "unarmed strikes this round ignore damage reduction.");
                bp.m_Icon = GeyserStyleIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WaterStyleGeyserRapidBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = WaterStyleSuppressCalmBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleRiverRapidBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleWaterfallRapidBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityKineticist>(c => {
                    c.m_RequiredResource = null;
                    c.m_IsSpendResource = false;
                    c.CostIsCustom = false;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                    c.BlastBurnCost = 0;
                    c.InfusionBurnCost = 0;
                    c.WildTalentBurnCost = 1;
                    c.CachedDamageInfo = new List<AbilityKineticist.DamageInfo>();
                    c.CachedDamageSource = null;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.m_Parent = WaterStyleGeyserBaseAbility.ToReference<BlueprintAbilityReference>();
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            WaterStyleGeyserBaseAbility.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    WaterStyleGeyserCalmAbility.ToReference<BlueprintAbilityReference>(),
                    WaterStyleGeyserRapidAbility.ToReference<BlueprintAbilityReference>()
                };
            });
            #endregion
            #region Rain
            var WaterStyleRainBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("WaterStyleRainBaseAbility", bp => {
                bp.SetName("Rain Style");
                bp.SetDescription("Calm: Leap to an ally as per dimension door as a move action by expending 1 point from your ki pool." +
                    "\nRapid: Accept 1 point of burn, leap to an enemy as per dimension door as a move action, then make a single unarmed attack against that enemy. " +
                    "Unlike other rapid styles, this does not suppress calm water styles.");
                bp.m_Icon = RainStyleIcon;
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Kineticist;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Move;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });


            var WaterStyleRainCalmAbility = Helpers.CreateBlueprint<BlueprintAbility>("WaterStyleRainCalmAbility", bp => {
                bp.SetName("Rain Style - Calm");
                bp.SetDescription("Leap to an ally as per dimension door as a move action by expending 1 point from your ki pool. \nThis style does not remove or suppress other water styles.");
                bp.m_Icon = RainStyleIcon;
                bp.AddComponent(KiAbundantStep.GetComponent<AbilityCustomDimensionDoor>());
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ScaledFistPowerResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Kineticist;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Move;
                bp.AvailableMetamagic = 0;
                bp.m_Parent = WaterStyleRainBaseAbility.ToReference<BlueprintAbilityReference>();
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var WaterStyleRainRapidAbility = Helpers.CreateBlueprint<BlueprintAbility>("WaterStyleRainRapidAbility", bp => {
                bp.SetName("Rain Style - Rapid");
                bp.SetDescription("Accept 1 point of burn, leap to an enemy as per dimension door as a move action, then make a single unarmed attack against that enemy. " +
                    "Unlike other rapid styles, this does not suppress calm water styles.");
                bp.m_Icon = RainStyleIcon;
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(                        
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleGeyserRapidBuff.ToReference<BlueprintBuffReference>(), ToCaster = true },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleRiverRapidBuff.ToReference<BlueprintBuffReference>(), ToCaster = true },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleWaterfallRapidBuff.ToReference<BlueprintBuffReference>(), ToCaster = true }
                        );
                });
                bp.AddComponent<AdditionalAbilityEffectRunActionOnClickedTarget>(c => {
                    c.Action = Helpers.CreateActionList(
                        new ContextActionCastSpell() {
                            m_Spell = DarkLurkerDimensionDoor,
                            OverrideDC = false,
                            DC = 0,
                            OverrideSpellLevel = false,
                            SpellLevel = 0,
                            CastByTarget = false,
                            LogIfCanNotTarget = false,
                            MarkAsChild = false
                        },
                        new ContextActionMeleeAttack() {
                            SelectNewTarget = false,
                            AutoHit = false,
                            IgnoreStatBonus = false,
                            AutoCritThreat = false,
                            AutoCritConfirmation = false,
                            ExtraAttack = false,
                            FullAttack = false,
                            ForceStartAnimation = false,
                            AnimationType = Kingmaker.Visual.Animation.Kingmaker.UnitAnimationType.SpecialAttack
                        }
                        );
                });
                bp.AddComponent<AbilityCustomTeleportation>(c => {
                    c.m_Projectile = Kinetic_WaterBlast00_Projectile;
                    c.DisappearFx = new PrefabLink() { AssetId = "f1f41fef03cb5734e95db1342f0c605e" };
                    c.DisappearDuration = 0.5f;
                    c.AppearFx = new PrefabLink();
                    c.AppearDuration = 0;
                    c.AlongPath = false;
                    c.AlongPathDistanceMuliplier = 1;
                });
                bp.AddComponent<AbilityKineticist>(c => {
                    c.m_RequiredResource = null;
                    c.m_IsSpendResource = false;
                    c.CostIsCustom = false;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                    c.BlastBurnCost = 0;
                    c.InfusionBurnCost = 0;
                    c.WildTalentBurnCost = 1;
                    c.CachedDamageInfo = new List<AbilityKineticist.DamageInfo>();
                    c.CachedDamageSource = null;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Kineticist;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Move;
                bp.AvailableMetamagic = 0;
                bp.m_Parent = WaterStyleRainBaseAbility.ToReference<BlueprintAbilityReference>();
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            WaterStyleRainBaseAbility.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    WaterStyleRainCalmAbility.ToReference<BlueprintAbilityReference>(),
                    WaterStyleRainRapidAbility.ToReference<BlueprintAbilityReference>()
                };
            });
            #endregion
            #region River
            var WaterStyleRiverBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("WaterStyleRiverBaseAbility", bp => {
                bp.SetName("River Style");
                bp.SetDescription("Calm:  Spend 1 point from your ki pool as a swift action, for one minute your first unarmed strike each round deals an extra 2d6 bludgeoning damage, " +
                    "increasing by 1d6 at levels 11 and 17." +
                    "\nRapid: Accept 1 point of burn as a swift action, for one round your unarmed strikes deal an extra 2d8 bludgeoning damage, increasing by 1d8 at levels 11 and 17.");
                bp.m_Icon = RiverStyleIcon;
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var WaterStyleRiverCalmAbility = Helpers.CreateBlueprint<BlueprintAbility>("WaterStyleRiverCalmAbility", bp => {
                bp.SetName("River Style - Calm");
                bp.SetDescription("Spend 1 point from your ki pool as a swift action, for one minute your first unarmed strike each round deals an extra 2d6 bludgeoning damage, increasing by 1d6 at levels 11 and 17.");
                bp.m_Icon = RiverStyleIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WaterStyleRiverCalmBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleGeyserCalmBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleWaterfallCalmBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleWaveCalmBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ScaledFistPowerResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.m_Parent = WaterStyleRiverBaseAbility.ToReference<BlueprintAbilityReference>();
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var WaterStyleRiverRapidAbility = Helpers.CreateBlueprint<BlueprintAbility>("WaterStyleRiverRapidAbility", bp => {
                bp.SetName("River Style - Rapid");
                bp.SetDescription("Accept 1 point of burn as a swift action, for one round your unarmed strikes deal an extra 2d8 bludgeoning damage, increasing by 1d8 at levels 11 and 17.");
                bp.m_Icon = RiverStyleIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WaterStyleRiverRapidBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = WaterStyleSuppressCalmBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleGeyserRapidBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleWaterfallRapidBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityKineticist>(c => {
                    c.m_RequiredResource = null;
                    c.m_IsSpendResource = false;
                    c.CostIsCustom = false;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                    c.BlastBurnCost = 0;
                    c.InfusionBurnCost = 0;
                    c.WildTalentBurnCost = 1;
                    c.CachedDamageInfo = new List<AbilityKineticist.DamageInfo>();
                    c.CachedDamageSource = null;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.m_Parent = WaterStyleRiverBaseAbility.ToReference<BlueprintAbilityReference>();
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            WaterStyleRiverBaseAbility.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    WaterStyleRiverCalmAbility.ToReference<BlueprintAbilityReference>(),
                    WaterStyleRiverRapidAbility.ToReference<BlueprintAbilityReference>()
                };
            });
            #endregion
            #region Waterfall
            var WaterStyleWaterfallBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("WaterStyleWaterfallBaseAbility", bp => {
                bp.SetName("Waterfall Style");
                bp.SetDescription("Calm: Spend 1 point from your ki pool as a swift action, make a free trip attempt on critical unarmed hits for one minute. " +
                    "\nRapid: Accept 1 point of burn as a swift action, the next unarmed strike that hits deals extra damage equal to half your blast damage, in addition to making a free trip attempt. " +
                    "\nThese trip attempts use the {g|Encyclopedia:BAB}base attack bonus{/g} of the attack used to hit the foe and do not provoke an " +
                    "{g|Encyclopedia:Attack_Of_Opportunity}attack of opportunity{/g}.");
                bp.m_Icon = WaterfallStyleIcon;
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var WaterStyleWaterfallCalmAbility = Helpers.CreateBlueprint<BlueprintAbility>("WaterStyleWaterfallCalmAbility", bp => {
                bp.SetName("Waterfall Style - Calm");
                bp.SetDescription("Spend 1 point from your ki pool as a swift action, make a free trip attempt on critical unarmed hits for one minute. " +
                    "\nThese trip attempts use the {g|Encyclopedia:BAB}base attack bonus{/g} of the attack used to hit the foe and do not provoke an " +
                    "{g|Encyclopedia:Attack_Of_Opportunity}attack of opportunity{/g}.");
                bp.m_Icon = WaterfallStyleIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WaterStyleWaterfallCalmBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleGeyserCalmBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleRiverCalmBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleWaveCalmBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ScaledFistPowerResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.m_Parent = WaterStyleWaterfallBaseAbility.ToReference<BlueprintAbilityReference>();
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var WaterStyleWaterfallRapidAbility = Helpers.CreateBlueprint<BlueprintAbility>("WaterStyleWaterfallRapidAbility", bp => {
                bp.SetName("Waterfall Style - Rapid");
                bp.SetDescription("Accept 1 point of burn as a swift action, the next unarmed strike that hits deals extra damage equal to half your blast damage, in addition to making a free trip attempt. " +
                    "\nThis trip attempt uses the {g|Encyclopedia:BAB}base attack bonus{/g} of the attack used to hit the foe and does not provoke an " +
                    "{g|Encyclopedia:Attack_Of_Opportunity}attack of opportunity{/g}.");
                bp.m_Icon = WaterfallStyleIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WaterStyleWaterfallRapidBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = WaterStyleSuppressCalmBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleGeyserRapidBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleRiverRapidBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityKineticist>(c => {
                    c.m_RequiredResource = null;
                    c.m_IsSpendResource = false;
                    c.CostIsCustom = false;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                    c.BlastBurnCost = 0;
                    c.InfusionBurnCost = 0;
                    c.WildTalentBurnCost = 1;
                    c.CachedDamageInfo = new List<AbilityKineticist.DamageInfo>();
                    c.CachedDamageSource = null;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.m_Parent = WaterStyleWaterfallBaseAbility.ToReference<BlueprintAbilityReference>();
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            WaterStyleWaterfallBaseAbility.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    WaterStyleWaterfallCalmAbility.ToReference<BlueprintAbilityReference>(),
                    WaterStyleWaterfallRapidAbility.ToReference<BlueprintAbilityReference>()
                };
            });
            #endregion
            #region Wave
            var WaterStyleWaveBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("WaterStyleWaveBaseAbility", bp => {
                bp.SetName("Wave Style");
                bp.SetDescription("Calm: Spend 1 point from your ki pool to enter a braced stance for 1 minute. This stance grants a +2 bonus to {g|Encyclopedia:CMD}CMD{/g} against all " +
                    "{g|Encyclopedia:Combat_Maneuvers}combat maneuvers{/g}." +
                    "\nRapid: Accept 1 point of burn, strike out with a kinetic blast in a 15-foot cone, targets take damage equal to your blast damage. " +
                    "Attempt a bull rush {g|Encyclopedia:Combat_Maneuvers}combat maneuver{/g} {g|Encyclopedia:Check}check{/g} against each target, " +
                    "using your {g|Encyclopedia:Charisma}Charisma{/g} modifier instead of your {g|Encyclopedia:Strength}Strength{/g} modifier to " +
                    "determine your {g|Encyclopedia:CMB}Combat Maneuver Bonus{/g}. Targets may attempt a Reflex save to reduce the damage by half and " +
                    "be unaffected by the bull rush combat maneuver.");
                bp.m_Icon = WaveStyleIcon;
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var WaterStyleWaveCalmAbility = Helpers.CreateBlueprint<BlueprintAbility>("WaterStyleWaveCalmAbility", bp => {
                bp.SetName("Wave Style - Calm");
                bp.SetDescription("Spend 1 point from your ki pool to enter a braced stance for 1 minute. This stance grants a +2 bonus to {g|Encyclopedia:CMD}CMD{/g} " +
                    "against all {g|Encyclopedia:Combat_Maneuvers}combat maneuvers{/g}.");
                bp.m_Icon = WaveStyleIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WaterStyleWaveCalmBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleGeyserCalmBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleRiverCalmBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleWaterfallCalmBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = ScaledFistPowerResource;
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.m_Parent = WaterStyleWaveBaseAbility.ToReference<BlueprintAbilityReference>();
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var WaterStyleWaveRapidAbility = Helpers.CreateBlueprint<BlueprintAbility>("WaterStyleWaveRapidAbility", bp => {
                bp.SetName("Wave Style - Rapid");
                bp.SetDescription("Accept 1 point of burn, strike out with a kinetic blast in a 15-foot cone, targets take damage equal to your blast damage. " +
                    "Attempt a bull rush {g|Encyclopedia:Combat_Maneuvers}combat maneuver{/g} {g|Encyclopedia:Check}check{/g} against each target, " +
                    "using your {g|Encyclopedia:Charisma}Charisma{/g} modifier instead of your {g|Encyclopedia:Strength}Strength{/g} modifier to " +
                    "determine your {g|Encyclopedia:CMB}Combat Maneuver Bonus{/g}. Targets may attempt a Reflex save to reduce the damage by half and " +
                    "be unaffected by the bull rush combat maneuver.");
                bp.m_Icon = WaveStyleIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            ToCaster = true,
                            m_Buff = WaterStyleSuppressCalmBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleGeyserRapidBuff.ToReference<BlueprintBuffReference>(), ToCaster = true },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleRiverRapidBuff.ToReference<BlueprintBuffReference>(), ToCaster = true },
                        new ContextActionRemoveBuff() { m_Buff = WaterStyleWaterfallRapidBuff.ToReference<BlueprintBuffReference>(), ToCaster = true },
                        new ContextActionSavingThrow() {
                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                            Type = SavingThrowType.Reflex,
                            HasCustomDC = false,
                            CustomDC = new ContextValue(),
                            Actions = Helpers.CreateActionList(
                                new ContextActionDealDamage() {
                                    m_Type = ContextActionDealDamage.Type.Damage,
                                    DamageType = new DamageTypeDescription() {
                                        Type = DamageType.Physical,
                                        Common = new DamageTypeDescription.CommomData() {
                                            Reality = 0,
                                            Alignment = 0,
                                            Precision = false
                                        },
                                        Physical = new DamageTypeDescription.PhysicalData() {
                                            Material = 0,
                                            Form = PhysicalDamageForm.Bludgeoning,
                                            Enhancement = 0,
                                            EnhancementTotal = 0
                                        },
                                        Energy = DamageEnergyType.Fire
                                    },
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
                                            ValueType = ContextValueType.Rank,
                                            Value = 0,
                                            ValueRank = AbilityRankType.DamageDice,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Shared,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    },
                                    IsAoE = true,
                                    HalfIfSaved = true,
                                    ResultSharedValue = AbilitySharedValue.Damage,
                                    CriticalSharedValue = AbilitySharedValue.Damage
                                },
                                new ContextActionConditionalSaved() {
                                    Succeed = new ActionList(),
                                    Failed = Helpers.CreateActionList(
                                        new ContextActionCombatManeuver() {
                                            Type = CombatManeuver.BullRush,
                                            IgnoreConcealment = true,
                                            OnSuccess = Helpers.CreateActionList(),
                                            ReplaceStat = false,
                                            NewStat = StatType.Unknown,
                                            UseKineticistMainStat = true,
                                            UseCastingStat = false,
                                            UseCasterLevelAsBaseAttack = false,
                                            UseBestMentalStat = false,
                                            BatteringBlast = false
                                        }
                                    ),
                                }
                            ),
                        }
                        );
                });
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        ColdCone15Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 15 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureRank;
                    c.m_Feature = KineticBlastFeature;
                    c.m_Progression = ContextRankProgression.AsIs;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.CustomProperty;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_CustomProperty = KineticistMainStatProperty;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageDice
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageBonus
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextCalculateAbilityParamsBasedOnClass>(c => {
                    c.StatType = StatType.Charisma;
                    c.m_CharacterClass = MonkClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<AbilityKineticist>(c => {
                    c.m_RequiredResource = null;
                    c.m_IsSpendResource = false;
                    c.CostIsCustom = false;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                    c.BlastBurnCost = 0;
                    c.InfusionBurnCost = 0;
                    c.WildTalentBurnCost = 1;
                    c.CachedDamageInfo = new List<AbilityKineticist.DamageInfo>();
                    c.CachedDamageSource = null;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Kineticist;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.m_Parent = WaterStyleWaveBaseAbility.ToReference<BlueprintAbilityReference>();
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            WaterStyleWaveBaseAbility.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    WaterStyleWaveCalmAbility.ToReference<BlueprintAbilityReference>(),
                    WaterStyleWaveRapidAbility.ToReference<BlueprintAbilityReference>()
                };
            });
            #endregion
            #region Style Features
            var WaterStyleGeyserFeature = Helpers.CreateBlueprint<BlueprintFeature>("WaterStyleGeyserFeature", bp => {
                bp.SetName("Geyser Style");
                bp.SetDescription("Calm: Spend 1 point from your ki pool as a swift action, your first unarmed strike each round ignores damage reduction for one minute." +
                    "\nRapid: Accept 1 point of burn as a swift action, the next unarmed strike that hits deals extra damage equal to your blast damage, unarmed strikes this round ignore damage reduction.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WaterStyleGeyserBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.IsClassFeature = true;
            });
            var WaterStyleRainFeature = Helpers.CreateBlueprint<BlueprintFeature>("WaterStyleRainFeature", bp => {
                bp.SetName("Rain Style");
                bp.SetDescription("Calm: Leap to an ally as per dimension door as a move action by expending 1 point from your ki pool." +
                    "\nRapid: Accept 1 point of burn, leap to an enemy as per dimension door as a move action, then make a single unarmed attack against that enemy. " +
                    "Unlike other rapid styles, this does not suppress calm water styles.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WaterStyleRainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.IsClassFeature = true;
            });
            var WaterStyleRiverFeature = Helpers.CreateBlueprint<BlueprintFeature>("WaterStyleRiverFeature", bp => {
                bp.SetName("River Style");
                bp.SetDescription("Calm:  Spend 1 point from your ki pool as a swift action, for one minute your first unarmed strike each round deals an extra 2d6 bludgeoning damage, " +
                    "increasing by 1d6 at levels 11 and 17." +
                    "\nRapid: Accept 1 point of burn as a swift action, for one round your unarmed strikes deal an extra 2d8 bludgeoning damage, increasing by 1d8 at levels 11 and 17.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WaterStyleRiverBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.IsClassFeature = true;
            });
            var WaterStyleWaterfallFeature = Helpers.CreateBlueprint<BlueprintFeature>("WaterStyleWaterfallFeature", bp => {
                bp.SetName("Waterfall Style");
                bp.SetDescription("Calm: Spend 1 point from your ki pool as a swift action, make a free trip attempt on critical unarmed hits for one minute. " +
                    "\nRapid: Accept 1 point of burn as a swift action, the next unarmed strike that hits deals extra damage equal to half your blast damage, in addition to making a free trip attempt. " +
                    "\nThese trip attempts use the {g|Encyclopedia:BAB}base attack bonus{/g} of the attack used to hit the foe and do not provoke an " +
                    "{g|Encyclopedia:Attack_Of_Opportunity}attack of opportunity{/g}.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WaterStyleWaterfallBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.IsClassFeature = true;
            });
            var WaterStyleWaveFeature = Helpers.CreateBlueprint<BlueprintFeature>("WaterStyleWaveFeature", bp => {
                bp.SetName("Wave Style");
                bp.SetDescription("Calm: Spend 1 point from your ki pool to enter a braced stance for 1 minute. This stance grants a +2 bonus to {g|Encyclopedia:CMD}CMD{/g} against all " +
                    "{g|Encyclopedia:Combat_Maneuvers}combat maneuvers{/g}." +
                    "\nRapid: Accept 1 point of burn, strike out with a kinetic blast in a 15-foot cone, targets take damage equal to your blast damage. " +
                    "Attempt a bull rush {g|Encyclopedia:Combat_Maneuvers}combat maneuver{/g} {g|Encyclopedia:Check}check{/g} against each target, " +
                    "using your {g|Encyclopedia:Charisma}Charisma{/g} modifier instead of your {g|Encyclopedia:Strength}Strength{/g} modifier to " +
                    "determine your {g|Encyclopedia:CMB}Combat Maneuver Bonus{/g}. Targets may attempt a Reflex save to reduce the damage by half and " +
                    "be unaffected by the bull rush combat maneuver.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WaterStyleWaveBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.IsClassFeature = true;
            });
            #endregion
            var WaterStyleStrikesFeatureSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("WaterStyleStrikesFeatureSelection", bp => {
                bp.SetName("Water Style Strke");
                bp.SetDescription("At 5th level, and every 4 levels thereafter, a water dancer learns one type of water style strike. \r\nEach style has two forms: \nCalm water " +
                    "forms use ki as a resource and provide long lasting, minor effects. \nRapid water forms force the water dancer to accept a point of burn, in exchange for short, but major effects. " +
                    "\r\nA water dancer can only be effected by one style at a time, using a calm style removes other calm styles, rapid styles do not remove calm styles but suppress them for one " +
                    "round.");
                bp.Groups = new FeatureGroup[0];
                bp.Mode = SelectionMode.OnlyNew;
                bp.IsClassFeature = true;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    WaterStyleGeyserFeature.ToReference<BlueprintFeatureReference>(),
                    WaterStyleRainFeature.ToReference<BlueprintFeatureReference>(),
                    WaterStyleRiverFeature.ToReference<BlueprintFeatureReference>(),
                    WaterStyleWaterfallFeature.ToReference<BlueprintFeatureReference>(),
                    WaterStyleWaveFeature.ToReference<BlueprintFeatureReference>()
                }; 
                bp.m_Features = new BlueprintFeatureReference[] { 
                
                };
            });
            #endregion

            WaterDancerArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, MonkWeaponProficiency, MonkFlurryOfBlowsFeatureUnlock, MonkACBonusUnlock, StunningFist, MonkBonusFeatSelectionLevel1, MonkUnarmedSrike),
                    Helpers.LevelEntry(2, MonkBonusFeatSelectionLevel1),
                    Helpers.LevelEntry(3, KiPowerFeature),
                    Helpers.LevelEntry(4, StunningFistFatigueFeature, MonkUnarmedSrikeLevel4, KiPowerFeatureSelection),
                    Helpers.LevelEntry(5, MonkStyleStrike),
                    Helpers.LevelEntry(6, MonkBonusFeatSelectionLevel6, KiPowerFeatureSelection),
                    Helpers.LevelEntry(8, StunningFistSickenedFeature, MonkUnarmedSrikeLevel8, KiPowerFeatureSelection),
                    Helpers.LevelEntry(9, MonkStyleStrike),
                    Helpers.LevelEntry(10, MonkBonusFeatSelectionLevel10, KiPowerFeatureSelection),
                    Helpers.LevelEntry(11, MonkFlurryOfBlowsFeatureLevel11Unlock),
                    Helpers.LevelEntry(12, MonkUnarmedSrikeLevel12, KiPowerFeatureSelection),
                    Helpers.LevelEntry(13, MonkStyleStrike),
                    Helpers.LevelEntry(14, MonkBonusFeatSelectionLevel10, KiPowerFeatureSelection),
                    Helpers.LevelEntry(16, MonkUnarmedSrikeLevel16, KiPowerFeatureSelection),
                    Helpers.LevelEntry(17, MonkStyleStrike),
                    Helpers.LevelEntry(18, MonkBonusFeatSelectionLevel10, KiPowerFeatureSelection),
                    Helpers.LevelEntry(20, MonkUnarmedSrikeLevel20, KiPowerFeatureSelection)
            };
            WaterDancerArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, WaterDancerProficiency, WaterDancerUnarmedStrike, WaterDancerBurnFeature, WaterDancerElementalFocusFeatureSelection, KineticBlastProgression, NereidsGraceFeature, ScaledFistACBonusUnlock),
                    Helpers.LevelEntry(2, WildTalentSelection),
                    Helpers.LevelEntry(3, WaterDancerKiPowerFeature),
                    Helpers.LevelEntry(4, WaterDancerKiCoolingFeature),
                    Helpers.LevelEntry(5, WaterStyleStrikesFeatureSelection),
                    Helpers.LevelEntry(6, WildTalentSelection),
                    Helpers.LevelEntry(8, MonkUnarmedSrikeLevel4),
                    Helpers.LevelEntry(9, WaterStyleStrikesFeatureSelection),
                    Helpers.LevelEntry(10, WildTalentSelection),
                    Helpers.LevelEntry(12, MonkUnarmedSrikeLevel8),
                    Helpers.LevelEntry(13, WaterStyleStrikesFeatureSelection),
                    Helpers.LevelEntry(14, WildTalentSelection),
                    Helpers.LevelEntry(16, MonkUnarmedSrikeLevel12),
                    Helpers.LevelEntry(17, WaterStyleStrikesFeatureSelection),
                    Helpers.LevelEntry(18, WildTalentSelection),
                    Helpers.LevelEntry(20, MonkUnarmedSrikeLevel16)
            };


            //if (ModSettings.AddedContent.Archetypes.IsDisabled("Water Dancer")) { return; }
            MonkClass.m_Archetypes = MonkClass.m_Archetypes.AppendToArray(WaterDancerArchetype.ToReference<BlueprintArchetypeReference>());




            //Hook into Wild Talents from CO+
        }
    }
}
