using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Classes.DrakeClass;
using ExpandedContent.Tweaks.Components;
using ExpandedContent.Tweaks.DemonLords;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.Root;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
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
using Kingmaker.Settings;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Visual.Sound;
using System.Linq;
using UnityEngine;

namespace ExpandedContent.Tweaks.Spells {
    internal class FormOfTheExoticDragon {
        public static void AddFormOfTheExoticDragon() {

            var FormOfTheExoticDragonIcon = AssetLoader.LoadInternal("Skills", "Icon_FormOfTheExoticDragon.jpg");

            //Asks
            var HavocDragonLargeBarks = Resources.GetBlueprintReference<BlueprintUnitAsksListReference>("478370a2af1b5cb4abe9c2fe80ef0cb5");
            var UmbralDragonBarks = Resources.GetBlueprintReference<BlueprintUnitAsksListReference>("a526fcf667234d4e8bb2ba5376a0f91a");
            var CragLinnormBarks = Resources.GetBlueprintReference<BlueprintUnitAsksListReference>("d48621d24d06bf84ead88849feb09cb8");


            //Smol melee
            var Bite1d8 = Resources.GetBlueprint<BlueprintItemWeapon>("61bc14eca5f8c1040900215000cfc218");
            var Claw1d6 = Resources.GetBlueprint<BlueprintItemWeapon>("65eb73689b94d894080d33a768cdf645");
            var Tail1d8 = Resources.GetBlueprint<BlueprintItemWeapon>("29e50b018da8468c8dcb411148ba6413");
            //normal melee
            var BiteHuge2d8 = Resources.GetBlueprint<BlueprintItemWeapon>("54af541fa85b3634cb6b801d96c3f2c9");
            var ClawHuge2d6 = Resources.GetBlueprint<BlueprintItemWeapon>("75254f19ca6e1d048a88b7545bb65221");
            var WingHuge1d8 = Resources.GetBlueprint<BlueprintItemWeapon>("78984ad70667084469ca7587465d4609");
            var TailHuge2d6 = Resources.GetBlueprint<BlueprintItemWeapon>("c36359e00abf82b40b5df9e5394207dd");
            //Linnorm
            var Bite4d10 = Resources.GetBlueprint<BlueprintItemWeapon>("06e1cd5de4274553ae2cbcddc8b21a36");
            var Tail4d6 = Resources.GetBlueprint<BlueprintItemWeapon>("efe700e7e536e7942bccd585b49e8861");

            var NegativeEnergyAffinity = Resources.GetBlueprint<BlueprintFeature>("d5ee498e19722854198439629c1841a5");

            var FireBreath = Resources.GetBlueprint<BlueprintAbility>("6722219d5c8205c4d8ee4c2f41c31e4e");
            var BreathWeaponCooldownBuff = Resources.GetBlueprint<BlueprintBuff>("b78d21189e7f6e943920236f009d30e3");

            var SonicCone30 = Resources.GetBlueprintReference<BlueprintProjectileReference>("c7fd792125b79904881530dbc2ff83de");
            var FormOfTheExoticDragonBreathHavoc = Helpers.CreateBlueprint<BlueprintAbility>("FormOfTheExoticDragonBreathHavoc", bp => {
                bp.SetName("Havoc Breath Weapon");
                bp.SetDescription("Your breath weapon deals {g|Encyclopedia:Dice}12d8{/g} points of {g|Encyclopedia:Energy_Damage}sonic damage{/g} in a 30-foot cone " +
                    "and allows a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. You can use it as often as you like, but " +
                    "you must wait 1d4 {g|Encyclopedia:Combat_Round}rounds{/g} between uses. \nOnly enemies are affected by this attack. ");
                bp.m_Icon = FireBreath.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] { SonicCone30 };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 30 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionIsEnemy() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionDealDamage() {
                                    m_Type = ContextActionDealDamage.Type.Damage,
                                    DamageType = new DamageTypeDescription() {
                                        Type = DamageType.Energy,
                                        Common = new DamageTypeDescription.CommomData() {
                                            Reality = 0,
                                            Alignment = 0,
                                            Precision = false
                                        },
                                        Physical = new DamageTypeDescription.PhysicalData() {
                                            Material = 0,
                                            Form = 0,
                                            Enhancement = 0,
                                            EnhancementTotal = 0
                                        },
                                        Energy = DamageEnergyType.Sonic
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
                                        DiceType = DiceType.D8,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 12,
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
                                    },
                                    IsAoE = true,
                                    HalfIfSaved = true,
                                    ResultSharedValue = AbilitySharedValue.Damage,
                                    CriticalSharedValue = AbilitySharedValue.Damage
                                }),
                            IfFalse = Helpers.CreateActionList()
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = BreathWeaponCooldownBuff.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
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
                                    },
                                    IsNotDispelable = true
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { BreathWeaponCooldownBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var NecromancyCone50 = Resources.GetBlueprintReference<BlueprintProjectileReference>("d07c1c64e5f087e4a919688db7059f0e");
            var BlindBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("0ec36e7596a4928489d2049e1e1c76a7");
            var FormOfTheExoticDragonBreathUmbral = Helpers.CreateBlueprint<BlueprintAbility>("FormOfTheExoticDragonBreathUmbral", bp => {
                bp.SetName("Umbral Breath Weapon");
                bp.SetDescription("Your breath weapon deals {g|Encyclopedia:Dice}12d8{/g} points of {g|Encyclopedia:Energy_Damage}negative energy damage{/g} in a 50-foot cone " +
                    "and allows a {g|Encyclopedia:Saving_Throw}Reflex save{/g} for half {g|Encyclopedia:Damage}damage{/g}. If the saving throw is failed, the target is blinded for 1d4 rounds. " +
                    "You can use it as often as you like, but you must wait 1d4 {g|Encyclopedia:Combat_Round}rounds{/g} between uses.");
                bp.m_Icon = FireBreath.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] { NecromancyCone50 };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 50 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = 0,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.NegativeEnergy
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
                                DiceType = DiceType.D8,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 12,
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
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = BlindBuff,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
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
                                    },
                                }
                                )
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = BreathWeaponCooldownBuff.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
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
                                    },
                                    IsNotDispelable = true
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { BreathWeaponCooldownBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });





            var FormOfTheExoticDragonAbility = Helpers.CreateBlueprint<BlueprintAbility>("FormOfTheExoticDragonAbility", bp => {
                bp.SetName("Form of the Exotic Dragon");
                bp.SetDescription("Take the form of a rarely seen dragon. You gain a +10 polymorph {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Strength}Strength{/g}, " +
                    "a +8 polymorph bonus to {g|Encyclopedia:Constitution}Constitution{/g}, a +8 natural armor bonus to {g|Encyclopedia:Armor_Class}AC{/g}, immunity to " +
                    "difficult terrain, {g|Encyclopedia:Blindsense}blindsense{/g} with a range of 60 feet, a breath weapon, {g|Encyclopedia:Damage_Reduction}damage reduction{/g} 10/magic, " +
                    "frightful presence ({g|Encyclopedia:DC}DC{/g} equal to the DC for this spell), and elemental resistances. Your movement {g|Encyclopedia:Speed}speed{/g} is increased " +
                    "by 10 feet. \nThe weapons and abilities granted by this spell vary depending on form taken.");
                bp.AddComponent<AbilityVariants>(c => {
                    c.m_Variants = new BlueprintAbilityReference[] {

                    };
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = FormOfTheExoticDragonIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("FormOfTheExoticDragonAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });


            //4967d58863a545b45b0894a50ea984eb is small
            //50a55eca7295aa7428bb1e068bd9ef11 is ?
            //81ee7affc01ade84e9646a8f0862d708


            CreateFormAbilityVariant("Havoc",
                "This form also grants sonic damage immunity, a sonic breath attack that only damages enemies, and the following natural attacks, one bite ({g|Encyclopedia:Dice}1d8{/g}), two claws (1d6), and one tail slap attack (1d8). \nUnlike other forms of this spell, size is set to medium.", 
                HavocDragonLargeBarks, FormOfTheExoticDragonBreathHavoc, Size.Medium, DamageEnergyType.Sonic, "50a55eca7295aa7428bb1e068bd9ef11", Bite1d8, Claw1d6, Tail1d8);



            CreateFormAbilityVariant("Umbral",
                "This form also grants negative energy damage immunity, a negative energy breath attack that also blinds for 1d4 rounds on a failed save, negaive energy affinity, and the following natural attacks, one bite ({g|Encyclopedia:Dice}2d8{/g}), two claws (2d6), two wing {g|Encyclopedia:Attack}attacks{/g} (1d8), and one tail slap attack (2d6).",
                UmbralDragonBarks, FormOfTheExoticDragonBreathUmbral, Size.Huge, DamageEnergyType.NegativeEnergy, "19f52bbfe67ce304ebfb5ff6c7f98af7", BiteHuge2d8, ClawHuge2d6, WingHuge1d8, WingHuge1d8, TailHuge2d6);
            var FormOfTheExoticDragonBuffUmbral = Resources.GetModBlueprint<BlueprintBuff>("FormOfTheExoticDragonBuffUmbral");
            FormOfTheExoticDragonBuffUmbral.TemporaryContext(bp => {
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = NegativeEnergyAffinity.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = NegativeEnergyAffinity.ToReference<BlueprintUnitFactReference>();
                    c.Not = true;
                });
            });

            //fde8f314998429842ad66fa78433d5c0 to big
            CreateFormAbilityVariant("Crag Linnorm",
                "This form also grants fire damage immunity, a fire breath attack, fast healing 5, and the following natural attacks, one large bite ({g|Encyclopedia:Dice}4d6{/g}), two claw attacks (2d6), and one tail slap attack (4d6).",
                CragLinnormBarks, FireBreath, Size.Huge, DamageEnergyType.Fire,"fde8f314998429842ad66fa78433d5c0", Bite4d10, ClawHuge2d6, Tail4d6);
            var FormOfTheExoticDragonBuffCragLinnorm = Resources.GetModBlueprint<BlueprintBuff>("FormOfTheExoticDragonBuffCragLinnorm");
            FormOfTheExoticDragonBuffCragLinnorm.TemporaryContext(bp => {
                bp.AddComponent<AddEffectFastHealing>(c => {
                    c.Heal = 5;
                    c.Bonus = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                });
                //This did not work :(
                //bp.AddComponent<ChangeUnitSize>(c => {
                //    c.Size = Size.Fine;
                //    c.m_Type = ChangeUnitSize.ChangeType.Delta;
                //    c.SizeDelta = -1;
                //});
            });




            //No scroll
            FormOfTheExoticDragonAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 9);
            FormOfTheExoticDragonAbility.AddToSpellList(SpellTools.SpellList.DruidSpellList, 9);




        }
        private static void CreateFormAbilityVariant(string variantName, string descriptionSecondHalf, BlueprintUnitAsksListReference unitAsksList, BlueprintAbility breathWeapon, Size size , DamageEnergyType energyimmunity, string prefab, BlueprintItemWeapon headAttack, BlueprintItemWeapon clawAttack, params BlueprintItemWeapon[] otherWeapons) {
            var FormOfTheExoticDragonAbility = Resources.GetModBlueprint<BlueprintAbility>("FormOfTheExoticDragonAbility");
            var FormOfTheDragonIIIBlackBuff = Resources.GetBlueprint<BlueprintBuff>("c231e0cf7c203644d81e665d6115ae69");
            var FormOfTheDragonIIIBlackPolymorph = Resources.GetBlueprint<BlueprintBuff>("c231e0cf7c203644d81e665d6115ae69").GetComponent<Polymorph>();
            var TurnBarkStandart = Resources.GetBlueprint<BlueprintAbility>("bd09b025ee2a82f46afab922c4decca9");
            var ShifterClawVisualBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("b09147e9b63b49b89c90361fbad90a68");
            var GrowingSpikesVisualBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("1ee79c0656c2488180db35be9b24b5bc");

            string variantNameWithNoSpaces = variantName.Replace(" ", "");

            var variantBuff = Helpers.CreateBuff($"FormOfTheExoticDragonBuff{variantNameWithNoSpaces}", bp => {
                bp.SetName($"Form of the Exotic Dragon - {variantName}");
                bp.SetDescription($"Take the form of a rarely seen dragon. You gain a +10 polymorph bonus to Strength, a +8 polymorph bonus to Constitution, " +
                    $"a +8 natural armor bonus to AC, immunity to difficult terrain, blindsense with a range of 60 feet, and damage reduction 10/magic, frightful " +
                    $"presence (DC equal to the DC for this spell). Your movement speed is increased by 10 feet. \n{descriptionSecondHalf}");
                bp.AddComponent<AddConditionImmunity>(c => { c.Condition = Kingmaker.UnitLogic.UnitCondition.DifficultTerrain; });
                bp.AddComponent<SpellDescriptorComponent>(c => { c.Descriptor = SpellDescriptor.Polymorph; });
                bp.AddComponent(FormOfTheDragonIIIBlackBuff.GetComponent<AddDamageResistancePhysical>());
                bp.AddComponent<Blindsense>(c => { c.Range = new Feet(60); c.Blindsight = false; });
                bp.AddComponent<AddEnergyImmunity>(c => {
                    c.Type = energyimmunity;
                });
                bp.AddComponent(FormOfTheDragonIIIBlackBuff.GetComponent<BuffMovementSpeed>());
                bp.AddComponent<ReplaceAsksList>(c => { c.m_Asks = unitAsksList; });
                bp.AddComponent(FormOfTheDragonIIIBlackBuff.GetComponent<ReplaceSourceBone>());
                bp.AddComponent<AddMechanicsFeature>(c => { c.m_Feature = AddMechanicsFeature.MechanicsFeatureType.NaturalSpell; });
                bp.AddComponent<ReplaceAbilityParamsWithContext>(c => { c.m_Ability = breathWeapon.ToReference<BlueprintAbilityReference>(); });
                bp.AddComponent<Polymorph>(c => {
                    c.m_Race = FormOfTheDragonIIIBlackPolymorph.m_Race;
                    c.m_Prefab = new UnitViewLink() { AssetId = prefab };
                    c.m_PrefabFemale = FormOfTheDragonIIIBlackPolymorph.m_PrefabFemale;
                    c.m_SpecialDollType = SpecialDollType.None;
                    c.m_ReplaceUnitForInspection = FormOfTheDragonIIIBlackPolymorph.m_ReplaceUnitForInspection;
                    c.m_Portrait = FormOfTheDragonIIIBlackPolymorph.m_Portrait;
                    c.m_KeepSlots = false;
                    c.Size = size;
                    c.UseSizeAsBaseForDamage = false;
                    c.StrengthBonus = 10;
                    c.DexterityBonus = 0;
                    c.ConstitutionBonus = 8;
                    c.NaturalArmor = 8;
                    c.m_MainHand = headAttack.ToReference<BlueprintItemWeaponReference>();
                    c.m_OffHand = clawAttack.ToReference<BlueprintItemWeaponReference>();
                    c.AllowDamageTransfer = false;
                    c.m_AdditionalLimbs = new BlueprintItemWeaponReference[] {
                        clawAttack.ToReference<BlueprintItemWeaponReference>()
                    };
                    c.m_SecondaryAdditionalLimbs = otherWeapons.Select(f => f.ToReference<BlueprintItemWeaponReference>()).ToArray();
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        TurnBarkStandart.ToReference<BlueprintUnitFactReference>(),
                        breathWeapon.ToReference<BlueprintUnitFactReference>()
                    };
                    c.m_EnterTransition = FormOfTheDragonIIIBlackPolymorph.m_EnterTransition;
                    c.m_ExitTransition = FormOfTheDragonIIIBlackPolymorph.m_ExitTransition;
                    c.m_TransitionExternal = FormOfTheDragonIIIBlackPolymorph.m_TransitionExternal;
                    c.m_SilentCaster = true;
                });
                bp.AddComponent(FormOfTheDragonIIIBlackBuff.GetComponent<AddFactContextActions>());
                bp.AddComponent<ReplaceCastSource>(c => { c.CastSource = Kingmaker.Blueprints.Root.Fx.CastSource.Head; });
                bp.AddComponent<SuppressBuffs>(c => {
                    c.m_Buffs = new BlueprintBuffReference[] { ShifterClawVisualBuff, GrowingSpikesVisualBuff };
                    c.Descriptor = new SpellDescriptor();
                });
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
            });

            var variantAbility = Helpers.CreateBlueprint<BlueprintAbility>($"FormOfTheExoticDragonAbility{variantNameWithNoSpaces}", bp => {
                bp.SetName($"Form of the Exotic Dragon - {variantName}");
                bp.SetDescription($"Take the form of a rarely seen dragon. You gain a +10 polymorph bonus to Strength, a +8 polymorph bonus to Constitution, " +
                    $"a +8 natural armor bonus to AC, immunity to difficult terrain, blindsense with a range of 60 feet, and damage reduction 10/magic, frightful " +
                    $"presence (DC equal to the DC for this spell). Your movement speed is increased by 10 feet. \n{descriptionSecondHalf}");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = variantBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0,
                            IsFromSpell = true,
                            ToCaster = false,
                            AsChild = false,
                        });
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_Progression = ContextRankProgression.AsIs;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
                });
                bp.m_Icon = FormOfTheExoticDragonAbility.Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.m_Parent = FormOfTheExoticDragonAbility.ToReference<BlueprintAbilityReference>();
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Extend | Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = FormOfTheExoticDragonAbility.LocalizedDuration;
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var AbilityVariantsComponent = Resources.GetModBlueprint<BlueprintAbility>("FormOfTheExoticDragonAbility").GetComponent<AbilityVariants>();
            AbilityVariantsComponent.m_Variants = AbilityVariantsComponent.m_Variants.AppendToArray(variantAbility.ToReference<BlueprintAbilityReference>());
        }
    }
}
