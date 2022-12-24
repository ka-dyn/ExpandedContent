using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
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
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Spells {
    internal class InvokeDeity {
        public static void AddInvokeDeity() {
            var InvokeDeityIcon = AssetLoader.LoadInternal("Skills", "Icon_InvokeDeity.jpg");
            var GoldCoins = Resources.GetBlueprint<BlueprintItem>("f2bc0997c24e573448c6c91d2be88afa");
            //Domain bank
            var AirDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("6e5f4ff5a7010754ca78708ce1a9b233"); //Done
            var AnimalDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("9f05f9da2ea5ae44eac47d407a0000e5"); //Done
            var ArtificeDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("9656b1c7214180f4b9a6ab56f83b92fb"); //Done
            var ChaosDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("8c7d778bc39fec642befc1435b00f613"); //Done
            var CharmDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("f1ceba79ee123cc479cece27bc994ff2"); //Done
            var CommunityDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("c87004460f3328c408d22c5ead05291f"); //Done
            var DarknessDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("6d8e7accdd882e949a63021af5cde4b8"); //Done
            var DeathDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("a099afe1b0b32554199b230699a69525"); //Done
            var DestructionDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("6832681c9a91bf946a1d9da28c5be4b4"); //Done
            var EarthDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("5ca99a6ae118feb449dbbd165a8fe7c4"); //Done
            var EvilDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("351235ac5fc2b7e47801f63d117b656c"); //Done
            var FireDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("8d4e9731082008640b28417f577f5f31"); //Done
            var GloryDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("2418251fa9c8ada4bbfbaaf5c90ac200"); //Done
            var GoodDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("882521af8012fc749930b03dc18a69de"); //Done
            var HealingDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("73ae164c388990c43ade94cfe8ed5755"); //Done
            var KnowledgeDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("443d44b3e0ea84046a9bf304c82a0425"); //Done
            var LawDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("092714336606cfc45a37d2ab39fabfa8"); //Done
            var LiberationDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("801ca88338451a546bca2ee59da87c53"); //Done
            var LuckDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("d4e192475bb1a1045859c7664addd461"); //Done
            var MadnessDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("c346bcc77a6613040b3aa915b1ceddec"); //Done
            var MagicDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("08a5686378a87b64399d329ba4ef71b8"); //Done
            var NobilityDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("e0471d01e73254a4ca23278705b75e57"); //Done
            var PlantDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("0e03c2a03222b0b42acf96096b286327"); //Done
            var ProtectionDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("d4ce7592bd12d63439907ad64e986e59"); //Done
            var ReposeDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("076ba1e3a05fac146acfc956a9f41e95"); //Done
            var RuneDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("77637f81d6aa33b4f82873d7934e8c4b");
            //var ScalykindDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("ScalykindDomainAllowed");
            var StrengthDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("58d2867520de17247ac6988a31f9e397"); //Done
            var SunDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("e28412c548ff21a49ac5b8b792b0aa9b"); //Done
            var TravelDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("c008853fe044bd442ae8bd22260592b7"); //Done
            var TrickeryDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("eaa368e08628a8641b16cd41cbd2cb33"); //Done
            var WarDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("3795653d6d3b291418164b27be88cb43"); //Done
            var WaterDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("8f49469c40e2c6e4db61296558e08966"); //Done
            var WeatherDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("9dfdfd4904e98fa48b80c8f63ec2cf11"); //Done
            //Icon bank
            var Haste = Resources.GetBlueprint<BlueprintBuff>("8d20b0a6129bd814eb0146041879f38a"); //Air
            var WildShapeWolfBuff = Resources.GetBlueprint<BlueprintBuff>("470fb1a22e7eb5849999f1101eacc5dc"); //Animal
            var IronBodyBuff = Resources.GetBlueprint<BlueprintBuff>("2eabea6a1f9a58246a822f207e8ca79e"); //Artifice
            var AnarchicBuff = Resources.GetBlueprint<BlueprintBuff>("4f1cad5d64816514e8f0f22b3d4a8a61"); //Chaos
            var EaglesSplendorBuff = Resources.GetBlueprint<BlueprintBuff>("7ed853ffcfd29914cb098cd7b1c46cc4"); //Charm
            var MindFogBuff = Resources.GetBlueprint<BlueprintBuff>("59fa875508d497d43823bf5253299070"); //Community
            var TrueSeeingBuff = Resources.GetBlueprint<BlueprintBuff>("09b4b69169304474296484c74aa12027"); //Darkness
            var SoulReaver = Resources.GetBlueprint<BlueprintAbility>("b4afacd337dac4a40a769a567c038ab7"); //Death
            var Destruction = Resources.GetBlueprint<BlueprintAbility>("3b646e1db3403b940bf620e01d2ce0c7"); //Destruction
            var StoneSkinBuff = Resources.GetBlueprint<BlueprintBuff>("37a956d0e7a84ab0bb66baf784767047"); //Earth
            var UnholyWeaponBuff = Resources.GetBlueprint<BlueprintBuff>("fc5f634a8edb9cd408c57b0b13fdd6df"); //Evil
            var ScorchingRay = Resources.GetBlueprint<BlueprintAbility>("cdb106d53c65bbc4086183d54c3b97c7"); //Fire
            var HeroismBuff = Resources.GetBlueprint<BlueprintBuff>("87ab2fed7feaaff47b62a3320a57ad8d"); //Glory
            var HolyWeaponBuff = Resources.GetBlueprint<BlueprintBuff>("f39603c07fad372459c768c6ec16a429"); //Good
            var PillarOfLife = Resources.GetBlueprint<BlueprintAbility>("aca83c764d751594287f18b817814bce"); //Healing
            var FoxesCunningBuff = Resources.GetBlueprint<BlueprintBuff>("c8c9872e9e02026479d82b9264b9cc6b"); //Knowledge
            var AxiomaticWeaponBuff = Resources.GetBlueprint<BlueprintBuff>("79e3ad6d6f26253439e2760a8c91d5d9"); //Law
            var CatsGraceBuff = Resources.GetBlueprint<BlueprintBuff>("f011d0ab4a405e54aa0e83cd10e54430"); //Liberation
            var Prayer = Resources.GetBlueprint<BlueprintBuff>("789bae3802e7b6b4c8097aaf566a1cf5"); //Luck
            var ConfusionBuff = Resources.GetBlueprint<BlueprintBuff>("886c7407dc629dc499b9f1465ff382df"); //Madness
            var SpellResistanceBuff = Resources.GetBlueprint<BlueprintBuff>("50a77710a7c4914499d0254e76a808e5"); //Magic
            var CommandHalt = Resources.GetBlueprint<BlueprintAbility>("a43abe1819699894c94a7cec3ccd3765"); //Nobility
            var BarkskinBuff = Resources.GetBlueprint<BlueprintBuff>("533592a86adecda4e9fd5ed37a028432"); //Plant
            var MageShieldBuff = Resources.GetBlueprint<BlueprintBuff>("9c0fa9b438ada3f43864be8dd8b3e741"); //Protection
            var OwlsWisdomBuff = Resources.GetBlueprint<BlueprintBuff>("73fc1d19f14339042ba5af34872c1745"); //Repose
            //var WildShapeWolfBuff = Resources.GetBlueprint<BlueprintBuff>(""); //Rune
            //var WildShapeWolfBuff = Resources.GetBlueprint<BlueprintBuff>(""); //Scalykind
            var BullsStrengthBuff = Resources.GetBlueprint<BlueprintBuff>("b175001b42b1a02479881b72fe132116"); //Strength
            var SacredNimbus = Resources.GetBlueprint<BlueprintAbility>("bf74b3b54c21a9344afe9947546e036f"); //Sun
            var BearsEnduranceBuff = Resources.GetBlueprint<BlueprintBuff>("c3de8cc9a0f50e2418dde526d8855faa"); //Travel
            var RangerdLegerdemain = Resources.GetBlueprint<BlueprintActivatableAbility>("994ea025e59f00c40aef981b0d4f32f2"); //Trickery
            var MagicWeaponGreater = Resources.GetBlueprint<BlueprintAbility>("0f92caa35619f234298d95a4b6dda90d"); //War
            var Tsunami = Resources.GetBlueprint<BlueprintAbility>("d8144161e352ca846a73cf90e85bf9ac"); //Water
            var CallLightningStorm = Resources.GetBlueprint<BlueprintAbility>("d5a36a7ee8177be4f848b953d1c53c84"); //Weather

            //Main ability
            var InvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("InvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC.\nYou must worship a being that grants access to domains " +
                    "in order to cast this spell.");
                bp.AddComponent<AbilityVariants>(c => {
                    c.m_Variants = new BlueprintAbilityReference[] {

                    };
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = InvokeDeityIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            //Air
            var AirInvokeDeityBuff = Helpers.CreateBuff("AirInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Air Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nAir: Increase your current speed by 30 feet and " +
                    "gain a +1 dodge bonus to your AC.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Speed;
                    c.Value = 30;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Dodge;
                    c.Stat = StatType.AC;
                    c.Value = 1;
                });
                bp.m_Icon = Haste.m_Icon;                
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var AirInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("AirInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Air Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nAir: Increase your current speed by 30 feet and " +
                    "gain a +1 dodge bonus to your AC.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        AirDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = AirInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = AirDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = Haste.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(AirInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Animal
            var Bite1d4 = Resources.GetBlueprint<BlueprintItemWeapon>("35dfad6517f401145af54111be04d6cf");
            var AnimalInvokeDeityBuff = Helpers.CreateBuff("AnimalInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Animal Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nAnimal: You gain low-light vision, scent, a +6 " +
                    "natural armor bonus, and a bite attack that deals damage as normal for a creature of your size.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Stat = StatType.AC;
                    c.Value = 6;
                });
                bp.AddComponent<AddAdditionalLimb>(c => {
                    c.m_Weapon = Bite1d4.ToReference<BlueprintItemWeaponReference>();
                });
                bp.AddComponent<Blindsense>(c => {
                    c.Range = new Feet() { m_Value = 60 };
                    c.Blindsight = true;
                });
                bp.m_Icon = WildShapeWolfBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var AnimalInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("AnimalInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Animal Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nAnimal: You gain low-light vision, scent, a +6 " +
                    "natural armor bonus, and a bite attack that deals damage as normal for a creature of your size.");
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        AnimalDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = AnimalInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = AnimalDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = WildShapeWolfBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(AnimalInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Artifice
            var ArtificeInvokeDeityBuff = Helpers.CreateBuff("ArtificeInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Artifice Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nArtifice: You are immune to bleed, disease, and " +
                    "poison effects, and you gain a +4 bonus on saving throws against death effects, mind-affecting effects, and necromancy effects.");
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Bleed | SpellDescriptor.Disease | SpellDescriptor.Poison;
                    c.m_IgnoreFeature = null;
                    c.CheckFact = false;
                    c.m_FactToCheck = null;
                });
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.Death | SpellDescriptor.MindAffecting;
                    c.ModifierDescriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 4;
                    c.Bonus = new ContextValue();
                });
                bp.AddComponent<SavingThrowBonusAgainstSchool>(c => {
                    c.School = SpellSchool.Necromancy;
                    c.ModifierDescriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 4;
                });
                bp.m_Icon = IronBodyBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var ArtificeInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("ArtificeInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Artifice Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nArtifice: You are immune to bleed, disease, and " +
                    "poison effects, and you gain a +4 bonus on saving throws against death effects, mind-affecting effects, and necromancy effects.");
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ArtificeDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ArtificeInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = ArtificeDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = IronBodyBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(ArtificeInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Chaos
            var ChaoticAligned = Resources.GetBlueprint<BlueprintWeaponEnchantment>("5781c3a3255f5be4a9f94c6faf0ac0c3");
            var ChaosInvokeDeityBuff = Helpers.CreateBuff("ChaosInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Chaos Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nChaos: You are immune to additional damage from " +
                    "critical hits and precision damage (such as sneak attack damage), and weapons you wield count as chaotic for the purpose of overcoming damage reduction.");
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = ChaoticAligned.ToReference<BlueprintItemEnchantmentReference>();
                    c.AllWeapons = true;
                    c.Slot = Kingmaker.UI.GenericSlot.EquipSlotBase.SlotType.PrimaryHand;
                });
                bp.AddComponent<AddImmunityToCriticalHits>();
                bp.AddComponent<AddImmunityToPrecisionDamage>();
                bp.m_Icon = AnarchicBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var ChaosInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("ChaosInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Chaos Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nChaos: You are immune to additional damage from " +
                    "critical hits and precision damage (such as sneak attack damage), and weapons you wield count as chaotic for the purpose of overcoming damage reduction.");
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ChaosDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ChaosInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = ChaosDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = AnarchicBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(ChaosInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Charm
            var CharmInvokeDeityBuff = Helpers.CreateBuff("CharmInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Charm Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nCharm: You gain a +6 enhancement bonus to Charisma.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.Stat = StatType.Charisma;
                    c.Value = 6;
                }); 
                bp.m_Icon = EaglesSplendorBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var CharmInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("CharmInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Charm Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nCharm: You gain a +6 enhancement bonus to Charisma.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        CharmDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = CharmInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = CharmDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = EaglesSplendorBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(CharmInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Community
            var CommunityInvokeDeityAreaBuff = Helpers.CreateBuff("CommunityInvokeDeityAreaBuff", bp => {
                bp.SetName("Invoke Deity - Community Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nCommunity: You give a sacred +2 will save bonus to all " +
                    "allies within 30 feet.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Sacred;
                    c.Stat = StatType.SaveWill;
                    c.Value = 2;
                });
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
            });
            var CommunityInvokeDeityArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("CommunityInvokeDeityArea", bp => {
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Enemy;
                bp.SpellResistance = false;
                bp.AggroEnemies = false;
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Ally;
                bp.AffectEnemies = false;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = 30.Feet();
                bp.Fx = new PrefabLink();
                bp.AddComponent(AuraUtils.CreateUnconditionalAuraEffect(CommunityInvokeDeityAreaBuff.ToReference<BlueprintBuffReference>()));
            });
            var CommunityInvokeDeityBuff = Helpers.CreateBuff("CommunityInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Community Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nCommunity: You give a sacred +2 will save bonus to all " +
                    "allies within 30 feet.");
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = CommunityInvokeDeityArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
                bp.m_Icon = MindFogBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var CommunityInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("CommunityInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Community Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nCommunity: You give a sacred +2 will save bonus to all " +
                    "allies within 30 feet.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        CommunityDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = CommunityInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = CommunityDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = MindFogBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(CommunityInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Darkness
            var BlindFight = Resources.GetBlueprint<BlueprintFeature>("4e219f5894ad0ea4daa0699e28c37b1d");
            var BlindFightImproved = Resources.GetBlueprint<BlueprintFeature>("4f1a78b02ac71bd4fa7d6e011d6f8ce0");
            var DarknessInvokeDeityBuff = Helpers.CreateBuff("DarknessInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Darkness Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nDarkness: You gain the effects of the improved " +
                    "blind fight feat and are immune to blindness effects.");
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = BlindFight.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = BlindFight.ToReference<BlueprintUnitFactReference>();
                    c.Not = true;
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = BlindFightImproved.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = BlindFightImproved.ToReference<BlueprintUnitFactReference>();
                    c.Not = true;
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Blindness;
                    c.m_IgnoreFeature = null;
                    c.CheckFact = false;
                    c.m_FactToCheck = null;
                });
                bp.m_Icon = TrueSeeingBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var DarknessInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("DarknessInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Darkness Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nDarkness: You gain the effects of the improved " +
                    "blind fight feat and are immune to blindness effects.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        DarknessDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DarknessInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = DarknessDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = TrueSeeingBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(DarknessInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Death
            var DeathInvokeDeityBuff = Helpers.CreateBuff("DeathInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Death Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nDeath: You gain resistance 30 to positive and negative " +
                    "energy that would harm you, you are also immune to energy drain and death effects.");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.PositiveEnergy;
                    c.Value = 30;
                });
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.NegativeEnergy;
                    c.Value = 30;
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Death;
                    c.m_IgnoreFeature = null;
                    c.CheckFact = false;
                    c.m_FactToCheck = null;
                });
                bp.AddComponent<AddImmunityToEnergyDrain>();
                bp.m_Icon = SoulReaver.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var DeathInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("DeathInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Death Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nDeath: You gain resistance 30 to positive and negative " +
                    "energy that would harm you, you are also immune to energy drain and death effects.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        DeathDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DeathInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = DeathDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = SoulReaver.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(DeathInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Destruction
            var ConstructType = Resources.GetBlueprint<BlueprintFeature>("fd389783027d63343b4a5634bd81645f");
            var DestructionInvokeDeityBuff = Helpers.CreateBuff("DestructionInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Destruction Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nDestruction: You gain a +2 bonus on damage rolls " +
                    "with melee weapons, spells you cast from the evocation spell school have their save DCs (if any) increased by 1, and your attacks against enemies with the construct type deal an " +
                    "additional 1d6 points of force damage.");
                bp.AddComponent<WeaponAttackTypeDamageBonus>(c => {
                    c.Type = WeaponRangeType.Melee;
                    c.AttackBonus = 2;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue();
                });
                bp.AddComponent<IncreaseSpellSchoolDC>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.School = SpellSchool.Evocation;
                    c.BonusDC = 1;
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.WaitForAttackResolve = true;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
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
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        Not = false,
                                        m_Fact = ConstructType.ToReference<BlueprintUnitFactReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionDealDamage() {
                                    m_Type = ContextActionDealDamage.Type.Damage,
                                    DamageType = new DamageTypeDescription() {
                                        Type = DamageType.Force,
                                        Common = new DamageTypeDescription.CommomData() {
                                            Reality = 0,
                                            Alignment = 0,
                                            Precision = false
                                        },
                                        Physical = new DamageTypeDescription.PhysicalData() {
                                            Material = 0,
                                            Form = PhysicalDamageForm.Slashing,
                                            Enhancement = 0,
                                            EnhancementTotal = 0
                                        },
                                        Energy = DamageEnergyType.PositiveEnergy
                                    },
                                    Drain = false,
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
                                    IsAoE = false,
                                    HalfIfSaved = false,
                                    ResultSharedValue = AbilitySharedValue.Damage,
                                    CriticalSharedValue = AbilitySharedValue.Damage
                                }),
                            IfFalse = Helpers.CreateActionList()
                        });
                });
                bp.m_Icon = Destruction.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var DestructionInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("DestructionInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Destruction Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nDestruction: You gain a +2 bonus on damage rolls " +
                    "with melee weapons, spells you cast from the evocation spell school have their save DCs (if any) increased by 1, and your attacks against enemies with the construct type deal an " +
                    "additional 1d6 points of force damage.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        DestructionDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = DestructionInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = DestructionDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = Destruction.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(DestructionInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Earth
            var EarthInvokeDeityBuff = Helpers.CreateBuff("EarthInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Earth Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nEarth: You gain DR 10/bludgeoning and are immune to " +
                    "petrification effects.");
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.Value = 10;
                    c.BypassedByForm = true;
                    c.Form = PhysicalDamageForm.Bludgeoning;
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Petrified;
                    c.m_IgnoreFeature = null;
                    c.CheckFact = false;
                    c.m_FactToCheck = null;
                });
                bp.m_Icon = StoneSkinBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var EarthInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("EarthInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Earth Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nEarth: You gain DR 10/bludgeoning and are immune to " +
                    "petrification effects.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        EarthDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = EarthInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = EarthDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = StoneSkinBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(EarthInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Evil
            var EvilAligned = Resources.GetBlueprint<BlueprintWeaponEnchantment>("785f58ae4af37d041a6924634b0f238f");
            var EvilInvokeDeityBuff = Helpers.CreateBuff("EvilInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Evil Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nEvil: You automatically confirm critical hits, and " +
                    "weapons you wield count as evil for the purpose of overcoming damage reduction.");
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = EvilAligned.ToReference<BlueprintItemEnchantmentReference>();
                    c.AllWeapons = true;
                    c.Slot = Kingmaker.UI.GenericSlot.EquipSlotBase.SlotType.PrimaryHand;
                });
                bp.AddComponent<InitiatorCritAutoconfirm>();
                bp.m_Icon = UnholyWeaponBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var EvilInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("EvilInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Evil Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nEvil: You automatically confirm critical hits, and " +
                    "weapons you wield count as evil for the purpose of overcoming damage reduction.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        EvilDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = EvilInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = EvilDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = UnholyWeaponBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(EvilInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Fire
            var ScorchingRay00 = Resources.GetBlueprint<BlueprintProjectile>("8cc159ce94d29fe46a94b80ce549161f");
            var RayWeapon = Resources.GetBlueprint<BlueprintItemWeapon>("f6ef95b1f7bb52b408a5b345a330ffe8");
            var FireInvokeDeityBuffAbility = Helpers.CreateBlueprint<BlueprintAbility>("FireInvokeDeityBuffAbility", bp => {
                bp.SetName("Fire Domain - Scorching Ray");
                bp.SetDescription("As a standard action, you can fire a ray of flame, as per scorching ray (limit one ray). This ray deals 4d6 of fire damage.");
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[1] {
                        ScorchingRay00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Simple;
                    c.IsHandOfTheApprentice = false;
                    c.m_Length = 0.Feet();
                    c.m_LineWidth = 5.Feet();
                    c.NeedAttackRoll = true;
                    c.m_Weapon = RayWeapon.ToReference<BlueprintItemWeaponReference>();
                    c.ReplaceAttackRollBonusStat = false;
                    c.AttackRollBonusStat = StatType.Unknown;
                    c.DelayBetweenProjectiles = 0.3f;
                    c.m_ControlledProjectileHolderBuff = null; //?
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
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
                                Energy = DamageEnergyType.Fire
                            },
                            Drain = false,
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
                                    ValueType = ContextValueType.Simple,
                                    Value = 4,
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
                            IsAoE = false,
                            HalfIfSaved = false,
                            UseMinHPAfterDamage = false,
                            MinHPAfterDamage = 0,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        });
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Fire;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Evocation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Damage;
                });
                bp.m_Icon = ScorchingRay.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var FireInvokeDeityBuff = Helpers.CreateBuff("FireInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Fire Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nFire: You gain fire resistance 30, and as a standard action, " +
                    "you can fire a ray of flame, as per scorching ray (limit one ray) once per round.");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Fire;
                    c.Value = 30;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        FireInvokeDeityBuffAbility.ToReference<BlueprintUnitFactReference>()
                    };
                    c.CasterLevel = 0;
                    c.DoNotRestoreMissingFacts = false;
                    c.HasDifficultyRequirements = false;
                    c.InvertDifficultyRequirements = false;
                    c.MinDifficulty = GameDifficultyOption.Story;
                });
                bp.AddComponent<ReplaceAbilityParamsWithContext>(c => {
                    c.m_Ability = FireInvokeDeityBuffAbility.ToReference<BlueprintAbilityReference>();
                });
                bp.m_Icon = ScorchingRay.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var FireInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("FireInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Fire Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nFire: You gain fire resistance 30, and as a standard action, " +
                    "you can fire a ray of flame, as per scorching ray (limit one ray) once per round.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        FireDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FireInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = FireDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = ScorchingRay.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(FireInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Glory
            var ProtectionFromEvilBuff = Resources.GetBlueprint<BlueprintBuff>("4a6911969911ce9499bf27dde9bfcedc");
            var UndeadType = Resources.GetBlueprint<BlueprintFeature>("734a29b693e9ec346ba2951b27987e33");
            var GloryInvokeDeityBuff = Helpers.CreateBuff("GloryInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Glory Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nGlory: You gain the benefits of heroism and " +
                    "protection from evil, and your first successful attack each round against an undead target deals an additional 1d6 points of positive energy damage.");
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.WaitForAttackResolve = true;
                    c.OnlyHit = true;
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
                    c.CheckWeaponCategory = false;
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
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionHasFact() {
                                        Not = false,
                                        m_Fact = UndeadType.ToReference<BlueprintUnitFactReference>()
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
                                            Form = PhysicalDamageForm.Slashing,
                                            Enhancement = 0,
                                            EnhancementTotal = 0
                                        },
                                        Energy = DamageEnergyType.PositiveEnergy
                                    },
                                    Drain = false,
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
                                    IsAoE = false,
                                    HalfIfSaved = false,
                                    ResultSharedValue = AbilitySharedValue.Damage,
                                    CriticalSharedValue = AbilitySharedValue.Damage
                                }),
                            IfFalse = Helpers.CreateActionList()
                        });
                });
                bp.m_Icon = HeroismBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var GloryInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("GloryInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Glory Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nGlory: You gain the benefits of heroism and " +
                    "protection from evil, and your first successful attack each round against an undead target deals an additional 1d6 points of positive energy damage.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        GloryDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = GloryInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = ProtectionFromEvilBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = HeroismBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        }
                        );
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = GloryDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = HeroismBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(GloryInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Good
            var GoodAligned = Resources.GetBlueprint<BlueprintWeaponEnchantment>("326da486cd9077242a0e25df7eb7cd78");
            var GoodInvokeDeityBuff = Helpers.CreateBuff("GoodInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Good Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nGood: You are immune to fear and compulsion effects, " +
                    "and weapons you wield count as good for the purpose of overcoming damage reduction.");
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = GoodAligned.ToReference<BlueprintItemEnchantmentReference>();
                    c.AllWeapons = true;
                    c.Slot = Kingmaker.UI.GenericSlot.EquipSlotBase.SlotType.PrimaryHand;
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Compulsion | SpellDescriptor.Fear;
                    c.m_IgnoreFeature = null;
                    c.CheckFact = false;
                    c.m_FactToCheck = null;
                });
                bp.m_Icon = HolyWeaponBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var GoodInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("GoodInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Good Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nGood: You are immune to fear and compulsion effects, " +
                    "and weapons you wield count as good for the purpose of overcoming damage reduction.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        GoodDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = GoodInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = GoodDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = HolyWeaponBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(GoodInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Healing
            var HealingInvokeDeityBuff = Helpers.CreateBuff("HealingInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Healing Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nHealing: You gain fast healing 3. Your spells and class " +
                    "features that have the Cure spell descriptor, along with channeling energy to heal, have their caster level raised by 2.");
                bp.AddComponent<IncreaseSpellDescriptorCasterLevel>(c => {
                    c.Descriptor = SpellDescriptor.Cure | SpellDescriptor.ChannelPositiveHeal | SpellDescriptor.ChannelNegativeHeal | SpellDescriptor.RestoreHP;
                    c.BonusCasterLevel = 2;
                    c.ModifierDescriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.AddComponent<AddEffectFastHealing>(c => {
                    c.Heal = 3;
                    c.Bonus = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                });
                bp.m_Icon = PillarOfLife.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var HealingInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("HealingInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Healing Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nHealing: You gain fast healing 3. Your spells and class " +
                    "features that have the Cure spell descriptor, along with channeling energy to heal, have their caster level raised by 2.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        HealingDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = HealingInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = HealingDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PillarOfLife.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(HealingInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Knowledge
            var KnowledgeInvokeDeityBuff = Helpers.CreateBuff("KnowledgeInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Knowledge Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nKnowledge: You gain a +6 enhancement bonus to Intelligence.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.Stat = StatType.Intelligence;
                    c.Value = 6;
                });
                bp.m_Icon = FoxesCunningBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var KnowledgeInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("KnowledgeInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Knowledge Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nKnowledge: You gain a +6 enhancement bonus to Intelligence.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        KnowledgeDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = KnowledgeInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = KnowledgeDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = FoxesCunningBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(KnowledgeInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Law
            var LawAligned = Resources.GetBlueprint<BlueprintWeaponEnchantment>("76c7f6e9f0618a64fa21905687e36133");
            var LawInvokeDeityBuff = Helpers.CreateBuff("LawInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Law Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nLaw: You are immune to confusion effects, gain a +4 " +
                    "bonus on saving throws against transmutation spells, and weapons you wield count as lawful for the purpose of overcoming damage reduction.");
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = LawAligned.ToReference<BlueprintItemEnchantmentReference>();
                    c.AllWeapons = true;
                    c.Slot = Kingmaker.UI.GenericSlot.EquipSlotBase.SlotType.PrimaryHand;
                });
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Confusion;
                    c.m_IgnoreFeature = null;
                    c.CheckFact = false;
                    c.m_FactToCheck = null;
                });
                bp.AddComponent<SavingThrowBonusAgainstSchool>(c => {
                    c.School = SpellSchool.Transmutation;
                    c.ModifierDescriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 4;
                });
                bp.m_Icon = AxiomaticWeaponBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var LawInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("LawInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Law Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nLaw: You are immune to confusion effects, gain a +4 " +
                    "bonus on saving throws against transmutation spells, and weapons you wield count as lawful for the purpose of overcoming damage reduction.");
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        LawDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = LawInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = LawDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = AxiomaticWeaponBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(LawInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Liberation
            var LiberationInvokeDeityBuff = Helpers.CreateBuff("LiberationInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Liberation Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nLiberation: You gain a +6 enhancement bonus to Dexterity.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.Stat = StatType.Dexterity;
                    c.Value = 6;
                });
                bp.m_Icon = CatsGraceBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var LiberationInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("LiberationInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Liberation Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nLiberation: You gain a +6 enhancement bonus to Dexterity.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        LiberationDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = LiberationInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = LiberationDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = CatsGraceBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(LiberationInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Luck
            var LuckInvokeDeityBuff = Helpers.CreateBuff("LuckInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Luck Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nLuck: You gain a +1 luck bonus to your AC and " +
                    "on attack rolls, ability checks, saving throws, and skill checks.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.AC;
                    c.Value = 1;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Stat = StatType.AdditionalAttackBonus;
                    c.Value = 1;
                });
                bp.AddComponent<BuffAllSavesBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Value = 1;
                });
                bp.AddComponent<BuffAllSkillsBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Luck;
                    c.Value = 1;
                });
                bp.m_Icon = Prayer.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var LuckInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("LuckInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Luck Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nLuck: You gain a +1 luck bonus to your AC and " +
                    "on attack rolls, ability checks, saving throws, and skill checks.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        LuckDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = LuckInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = LuckDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = Prayer.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(LuckInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Madness
            var CommandFX = new PrefabLink() { AssetId = "8de64fbe047abc243a9b4715f643739f" }; //For Nobility but madness need it too.
            var MadnessInvokeDeityBuffAbilityCooldown = Helpers.CreateBuff("MadnessInvokeDeityBuffAbilityCooldown", bp => {
                bp.m_AllowNonContextActions = false;
                bp.SetName("Invoke Madness Cooldown");
                bp.SetDescription("Targeted by Madness Domain - Invoke Madness within 1 minute.");
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });
            var MadnessInvokeDeityBuffAbility = Helpers.CreateBlueprint<BlueprintAbility>("MadnessInvokeDeityBuffAbility", bp => {
                bp.SetName("Madness Domain - Invoke Madness");
                bp.SetDescription("The subject stands in place for 1 {g|Encyclopedia:Combat_Round}round{/g}. It may not take any {g|Encyclopedia:CA_Types}actions{/g} " +
                    "but is not considered {g|Encyclopedia:Helpless}helpless{/g}. If the subject can't carry out your command on its next turn, the {g|Encyclopedia:Spell}spell{/g} " +
                    "automatically fails.\nThis ability takes a Move action.");
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = CommandFX;
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.ClickedTarget;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                });                
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.MindAffecting | SpellDescriptor.Compulsion | SpellDescriptor.Confusion;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Enchantment;
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { MadnessInvokeDeityBuffAbilityCooldown.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Will;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = MadnessInvokeDeityBuffAbilityCooldown.ToReference<BlueprintBuffReference>(),
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1,
                                m_IsExtendable = false,
                            }
                        },
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = ConfusionBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            m_AbilityParameter = AbilityParameterType.Level
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            m_AbilityParameter = AbilityParameterType.Level
                                        },
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0
                                }
                            )
                        });
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Will;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = ConfusionBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = Helpers.CreateString("MadnessInvokeDeityBuffAbility.Duration", "1d4 rounds");
                bp.LocalizedSavingThrow = Helpers.CreateString("MadnessInvokeDeityBuffAbility.SavingThrow", "Will negates");
            });
            var MadnessInvokeDeityBuff = Helpers.CreateBuff("MadnessInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Madness Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nMadness: As a move action, you can issue one " +
                    "order to a single creature to halt. This effect functions as if you had cast the spell command.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        MadnessInvokeDeityBuffAbility.ToReference<BlueprintUnitFactReference>()
                    };
                    c.CasterLevel = 0;
                    c.DoNotRestoreMissingFacts = false;
                    c.HasDifficultyRequirements = false;
                    c.InvertDifficultyRequirements = false;
                    c.MinDifficulty = GameDifficultyOption.Story;
                });
                bp.AddComponent<ReplaceAbilityParamsWithContext>(c => {
                    c.m_Ability = MadnessInvokeDeityBuffAbility.ToReference<BlueprintAbilityReference>();
                });
                bp.m_Icon = ConfusionBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var MadnessInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("MadnessInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Madness Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nMadness: As a move action, you can issue one " +
                    "order to a single creature to halt. This effect functions as if you had cast the spell command.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        MadnessDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = MadnessInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = MadnessDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = ConfusionBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(MadnessInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Magic
            var MagicInvokeDeityBuff = Helpers.CreateBuff("MagicInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Magic Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nMagic: You gain spell resistance equal to 11 + " +
                    "your caster level.");
                bp.AddComponent<AddSpellResistance>(c => {
                    c.AddCR = false;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.StatBonus
                    };
                    c.AllSpellResistancePenaltyDoNotUse = false;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;                    
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.StatBonus;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 11,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        },
                    };
                    c.Modifier = 1;
                });
                bp.m_Icon = SpellResistanceBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var MagicInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("MagicInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Magic Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nMagic: You gain spell resistance equal to 11 + " +
                    "your caster level.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        MagicDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = MagicInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = MagicDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = SpellResistanceBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(MagicInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Nobility
            var UndeadMindAffection = Resources.GetBlueprint<BlueprintFeature>("7853143d87baea1429bb409b023edb6b");
            var AnimalType = Resources.GetBlueprint<BlueprintFeature>("a95311b3dc996964cbaa30ff9965aaf6");
            var MonstrousHumanoidType = Resources.GetBlueprint<BlueprintFeature>("57614b50e8d86b24395931fffc5e409b");
            var MagicalBeastType = Resources.GetBlueprint<BlueprintFeature>("625827490ea69d84d8e599a33929fdc6");
            var VerminType = Resources.GetBlueprint<BlueprintFeature>("09478937695300944a179530664e42ec");
            var BloodlineSerpentineArcana = Resources.GetBlueprint<BlueprintFeature>("02707231be1d3a74ba7e38a426c8df37");
            var CommandHaltBuff = Resources.GetBlueprint<BlueprintBuff>("1baa8a3dffc1f104f9942abe852f631a");
            var NobilityInvokeDeityBuffAbility = Helpers.CreateBlueprint<BlueprintAbility>("NobilityInvokeDeityBuffAbility", bp => {
                bp.SetName("Nobility Domain - Command to Halt");
                bp.SetDescription("The subject stands in place for 1 {g|Encyclopedia:Combat_Round}round{/g}. It may not take any {g|Encyclopedia:CA_Types}actions{/g} " +
                    "but is not considered {g|Encyclopedia:Helpless}helpless{/g}. If the subject can't carry out your command on its next turn, the {g|Encyclopedia:Spell}spell{/g} " +
                    "automatically fails.\nThis ability takes a Move action.");
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = CommandFX;
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.ClickedTarget;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                });
                bp.AddComponent<AbilityTargetHasNoFactUnless>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { UndeadType.ToReference<BlueprintUnitFactReference>() };
                    c.m_UnlessFact = UndeadMindAffection.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AbilityTargetHasNoFactUnless>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { 
                        AnimalType.ToReference<BlueprintUnitFactReference>(),
                        MonstrousHumanoidType.ToReference<BlueprintUnitFactReference>(),
                        MagicalBeastType.ToReference<BlueprintUnitFactReference>(),
                        VerminType.ToReference<BlueprintUnitFactReference>()
                    };
                    c.m_UnlessFact = BloodlineSerpentineArcana.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { 
                        UndeadType.ToReference<BlueprintUnitFactReference>(),
                        ConstructType.ToReference<BlueprintUnitFactReference>()
                    };
                    c.Inverted = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.MindAffecting;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Enchantment;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Will;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = CommandHaltBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            m_AbilityParameter = AbilityParameterType.Level
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            m_AbilityParameter = AbilityParameterType.Level
                                        },
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0
                                }
                            )
                        });
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Will;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = CommandHalt.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.ActionType = UnitCommand.CommandType.Move;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = Helpers.CreateString("NobilityInvokeDeityBuffAbility.SavingThrow", "Will negates");
            });
            var NobilityInvokeDeityBuff = Helpers.CreateBuff("NobilityInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Nobility Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nNobility: As a move action, you can issue one " +
                    "order to a single creature to halt. This effect functions as if you had cast the spell command.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        NobilityInvokeDeityBuffAbility.ToReference<BlueprintUnitFactReference>()
                    };
                    c.CasterLevel = 0;
                    c.DoNotRestoreMissingFacts = false;
                    c.HasDifficultyRequirements = false;
                    c.InvertDifficultyRequirements = false;
                    c.MinDifficulty = GameDifficultyOption.Story;
                });
                bp.AddComponent<ReplaceAbilityParamsWithContext>(c => {
                    c.m_Ability = NobilityInvokeDeityBuffAbility.ToReference<BlueprintAbilityReference>();
                });
                bp.m_Icon = CommandHalt.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var NobilityInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("NobilityInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Nobility Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nNobility: As a move action, you can issue one " +
                    "order to a single creature to halt. This effect functions as if you had cast the spell command.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        NobilityDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = NobilityInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = NobilityDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = CommandHalt.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(NobilityInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Plant
            var PlantInvokeDeityBuff = Helpers.CreateBuff("PlantInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Plant Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nPlant: You become immune to paralysis, poison, " +
                    "polymorph effects, sleep effects, and stunning.");
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Paralysis | SpellDescriptor.Poison | SpellDescriptor.Polymorph | SpellDescriptor.Sleep | SpellDescriptor.Stun;
                    c.m_IgnoreFeature = null;
                    c.CheckFact = false;
                    c.m_FactToCheck = null;
                });
                bp.m_Icon = BarkskinBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var PlantInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("PlantInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Plant Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nPlant: You become immune to paralysis, poison, " +
                    "polymorph effects, sleep effects, and stunning.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        PlantDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = PlantInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = PlantDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = BarkskinBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(PlantInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Protection
            var FalseLifeBuff = Resources.GetBlueprint<BlueprintBuff>("0fdb3cca6744fd94b9436459e6d9b947");
            var DelayPoisonBuff = Resources.GetBlueprint<BlueprintBuff>("51ebd62ee464b1446bb01fa1e214942f");
            var ProtectionFromChaosBuff = Resources.GetBlueprint<BlueprintBuff>("a4742d7afde0f4f47b380abed025b219");
            var ProtectionFromGoodBuff = Resources.GetBlueprint<BlueprintBuff>("b19e788487556aa4397080ef3dbb3619");
            var ProtectionFromLawBuff = Resources.GetBlueprint<BlueprintBuff>("744bec63273df53438c6b76aaaa78382");
            var MageArmorBuff = Resources.GetBlueprint<BlueprintBuff>("a92acdf18049d784eaa8f2004f5d2304");
            var ProtectionInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("ProtectionInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Protection Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nProtection: You gain the protection of the following " +
                    "spells: false life, delay poison, protection from alignment, mage armor, and shield.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ProtectionDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = FalseLifeBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = DelayPoisonBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = ProtectionFromChaosBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = ProtectionFromEvilBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = ProtectionFromGoodBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = ProtectionFromLawBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = MageArmorBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = MageShieldBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = ProtectionDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = MageShieldBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(ProtectionInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Repose
            var ReposeInvokeDeityBuff = Helpers.CreateBuff("ReposeInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Repose Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nRepose: You gain a +6 enhancement bonus to Wisdom.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.Stat = StatType.Wisdom;
                    c.Value = 6;
                });
                bp.m_Icon = OwlsWisdomBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var ReposeInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("ReposeInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Repose Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nRepose: You gain a +6 enhancement bonus to Wisdom.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ReposeDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = ReposeInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = ReposeDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = OwlsWisdomBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(ReposeInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Rune

            //Scalykind??

            //Strength
            var StrengthInvokeDeityBuff = Helpers.CreateBuff("StrengthInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Strength Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nStrength: You gain a +6 enhancement bonus to Strength.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.Stat = StatType.Strength;
                    c.Value = 6;
                });
                bp.m_Icon = BullsStrengthBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var StrengthInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("StrengthInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Strength Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nStrength: You gain a +6 enhancement bonus to Strength.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        StrengthDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = StrengthInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = StrengthDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = BullsStrengthBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(StrengthInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Sun
            var DazzledBuff = Resources.GetBlueprint<BlueprintBuff>("df6d1025da07524429afbae248845ecc");
            var SunInvokeDeityBuff = Helpers.CreateBuff("SunInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Sun Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nSun: You are immune to the blindness, dazed, and dazzled conditions." +
                    "Any enemies that attack you with melee weapons find their vision obscured by divine sun-rays (50% miss chance).");
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Blindness | SpellDescriptor.Daze;
                    c.m_IgnoreFeature = null;
                    c.CheckFact = false;
                    c.m_FactToCheck = null;
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = DazzledBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<AddConcealment>(c => {
                    c.Descriptor = ConcealmentDescriptor.Displacement;
                    c.Concealment = Concealment.Total;
                    c.CheckWeaponRangeType = true;
                    c.RangeType = WeaponRangeType.Melee;
                    c.CheckDistance = false;
                    c.DistanceGreater = new Feet() { m_Value = 0 };
                    c.OnlyForAttacks = true;
                });
                bp.m_Icon = SacredNimbus.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var SunInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("SunInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Sun Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nSun: You are immune to the blindness, dazed, and dazzled conditions." +
                    "Any enemies that attack you with melee weapons find their vision obscured by divine sun-rays (50% miss chance).");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SunDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = DazzledBuff.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = SunInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = SunDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = SacredNimbus.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(SunInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Travel
            var TravelInvokeDeityBuff = Helpers.CreateBuff("TravelInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Travel Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nTravel: You gain a +6 enhancement bonus to Constitution.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.Stat = StatType.Constitution;
                    c.Value = 6;
                });
                bp.m_Icon = BearsEnduranceBuff.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var TravelInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("TravelInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Travel Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nTravel: You gain a +6 enhancement bonus to Constitution.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        TravelDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TravelInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = TravelDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = BearsEnduranceBuff.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(TravelInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Trickery
            var TrickeryInvokeDeityBuff = Helpers.CreateBuff("TrickeryInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Trickery Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nTrickery: You gain a +4 enhancement bonus to Persuasion, " +
                    "Thievery and Stealth checks.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillPersuasion;
                    c.Value = 4;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillThievery;
                    c.Value = 4;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillStealth;
                    c.Value = 4;
                });
                bp.m_Icon = RangerdLegerdemain.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var TrickeryInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("TrickeryInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Trickery Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nTrickery: You gain a +4 enhancement bonus to Persuasion, " +
                    "Thievery and Stealth checks.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        TrickeryDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = TrickeryInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = TrickeryDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = RangerdLegerdemain.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(TrickeryInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //War
            var WarInvokeDeityBuff = Helpers.CreateBuff("WarInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - War Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nWar: You gain a +2 insight bonus on attack rolls " +
                    "and deal an additional 1d6 points of damage each time you hit a foe.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Insight;
                    c.Stat = StatType.AdditionalAttackBonus;
                    c.Value = 2;
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.WaitForAttackResolve = true;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = false;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
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
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Direct,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = PhysicalDamageForm.Slashing,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Electricity
                            },
                            Drain = false,
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
                            IsAoE = false,
                            HalfIfSaved = false,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        }
                        );
                });
                bp.m_Icon = MagicWeaponGreater.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var WarInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("WarInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - War Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nWar: You gain a +2 insight bonus on attack rolls " +
                    "and deal an additional 1d6 points of damage each time you hit a foe.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        WarDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WarInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = WarDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = MagicWeaponGreater.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(WarInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Water
            var WaterInvokeDeityBuff = Helpers.CreateBuff("WaterInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Water Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nWater: You gain cold resistance 30, and a +6 " +
                    "enhancement to your CMD.");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Cold;
                    c.Value = 30;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Enhancement;
                    c.Stat = StatType.AdditionalCMD;
                    c.Value = 6;
                });
                bp.m_Icon = Tsunami.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var WaterInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("WaterInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Water Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nWater: You gain cold resistance 30, and a +6 " +
                    "enhancement to your CMD.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        WaterDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WaterInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = WaterDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = Tsunami.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(WaterInvokeDeityAbility.ToReference<BlueprintAbilityReference>());
            //Weather
            var RainModerate = Resources.GetBlueprint<BlueprintBuff>("f37b708de9eeb2c4ab248d79bb5b5aa7");
            var SnowModerateBuff = Resources.GetBlueprint<BlueprintBuff>("845332298344c6447972dc9b131add08");
            var RainStormBuff = Resources.GetBlueprint<BlueprintBuff>("7c260a8970e273d439f2a2e19b7196af");
            var RainHeavyBuff = Resources.GetBlueprint<BlueprintBuff>("5c315bec0240479d9fafcc65b9efb574");
            var RainLightBuff = Resources.GetBlueprint<BlueprintBuff>("b13768381de549e2a78f502fa65dd613");
            var SnowHeavyBuff = Resources.GetBlueprint<BlueprintBuff>("4a15ab872f11463da1c1265d5b4324ad");
            var SnowLightBuff = Resources.GetBlueprint<BlueprintBuff>("26d8835510914ca2a8fe74b1519c09ac");
            var InsideTheStormBuff = Resources.GetBlueprint<BlueprintBuff>("32e90ae6f8c7656448d9e80173222012");
            var WeatherInvokeDeityBuff = Helpers.CreateBuff("WeatherInvokeDeityBuff", bp => {
                bp.SetName("Invoke Deity - Weather Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nWeather: You do not take penalties or damage or " +
                    "suffer from reduced visibility or other effects due to natural weather. You gain electricity resistance 30, and your call lightning and call lightning storm spells and effects " +
                    "always function as though called outdoors in stormy weather.");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Electricity;
                    c.Value = 30;
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = RainModerate.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = SnowModerateBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = RainStormBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = RainHeavyBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = RainLightBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = SnowHeavyBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = SnowLightBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = InsideTheStormBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            DurationValue = new ContextDurationValue() {
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
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true,
                            },
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                });
                bp.m_Icon = CallLightningStorm.m_Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var WeatherInvokeDeityAbility = Helpers.CreateBlueprint<BlueprintAbility>("WeatherInvokeDeityAbility", bp => {
                bp.SetName("Invoke Deity - Weather Domain");
                bp.SetDescription("By holding aloft a holy symbol and calling your deity’s name, you take on an aspect of that divinity. When you cast this spell, choose a domain offered by your deity. " +
                    "You gain that domain’s benefits, along with the listed physical changes; abilities that allow a saving throw use this spell’s DC. \nWeather: You do not take penalties or damage or " +
                    "suffer from reduced visibility or other effects due to natural weather. You gain electricity resistance 30, and your call lightning and call lightning storm spells and effects " +
                    "always function as though called outdoors in stormy weather.");
                bp.m_Parent = InvokeDeityAbility.ToReference<BlueprintAbilityReference>();
                bp.MaterialComponent = new BlueprintAbility.MaterialComponentData() {
                    Count = 500,
                    m_Item = GoldCoins.ToReference<BlueprintItemReference>()
                };
                bp.AddComponent<AbilityCasterHasFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        WeatherDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = WeatherInvokeDeityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityShowIfCasterHasFact>(c => {
                    c.m_UnitFact = WeatherDomainAllowed.ToReference<BlueprintUnitFactReference>();
                    c.Not = false;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = CallLightningStorm.m_Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Heighten;
                bp.LocalizedDuration = Helpers.CreateString("InvokeDeityAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants = InvokeDeityAbility.GetComponent<AbilityVariants>().m_Variants.AppendToArray(WeatherInvokeDeityAbility.ToReference<BlueprintAbilityReference>());

            InvokeDeityAbility.AddToSpellList(SpellTools.SpellList.ClericSpellList, 6);
            InvokeDeityAbility.AddToSpellList(SpellTools.SpellList.HunterSpelllist, 4);
            InvokeDeityAbility.AddToSpellList(SpellTools.SpellList.InquisitorSpellList, 6);
            InvokeDeityAbility.AddToSpellList(SpellTools.SpellList.PaladinSpellList, 4);
            InvokeDeityAbility.AddToSpellList(SpellTools.SpellList.WarpriestSpelllist, 6);
            InvokeDeityAbility.AddToSpellList(SpellTools.SpellList.WitchSpellList, 6);
        }
    }
}
