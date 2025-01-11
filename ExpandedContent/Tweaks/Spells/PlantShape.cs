using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.Root.Fx;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Visual.Sound;

namespace ExpandedContent.Tweaks.Spells {
    internal class PlantShape {
        public static void AddPlantShape() {

            var PlantShapeIIcon = AssetLoader.LoadInternal("Skills", "Icon_PlantShapeI.jpg");
            var PlantShapeIIIcon = AssetLoader.LoadInternal("Skills", "Icon_PlantShapeII.jpg");
            var PlantShapeIIIIcon = AssetLoader.LoadInternal("Skills", "Icon_PlantShapeIII.jpg");
            //var Icon_ScrollOfPlantShapeI = AssetLoader.LoadInternal("Items", "Icon_ScrollOfPlantShapeI.png");
            //var Icon_ScrollOfPlantShapeII = AssetLoader.LoadInternal("Items", "Icon_ScrollOfPlantShapeII.png");
            //var Icon_ScrollOfPlantShapeIII = AssetLoader.LoadInternal("Items", "Icon_ScrollOfPlantShapeIII.png");
            var Enhancement1 = Resources.GetBlueprint<BlueprintWeaponEnchantment>("d42fc23b92c640846ac137dc26e000d4");
            var Bite1d6 = Resources.GetBlueprint<BlueprintItemWeapon>("a000716f88c969c499a535dadcf09286");
            var Bite2d6 = Resources.GetBlueprint<BlueprintItemWeapon>("2abc1dc6172759c42971bd04b8c115cb");
            var BiteLarge1d8 = Resources.GetBlueprint<BlueprintItemWeapon>("ec35ef997ed5a984280e1a6d87ae80a8");
            var SlamLarge1d6 = Resources.GetBlueprint<BlueprintItemWeapon>("7fe0fa95a5c21ee439e6849b7e018a82");
            var SlamGargantuan2n6 = Resources.GetBlueprint<BlueprintItemWeapon>("27eee74857c42db499b3a6b20cfa6211");
            var Slam1d4 = Resources.GetBlueprint<BlueprintItemWeapon>("7445b0b255796d34495a8bca81b2e2d4");
            var QuickwoodRoots = Resources.GetBlueprint<BlueprintItemWeapon>("f76b563b69718a943a9a1c89bf81215f");
            var TurnBarkStandart = Resources.GetBlueprint<BlueprintAbility>("bd09b025ee2a82f46afab922c4decca9");
            var MandragoraPoisonFeature = Resources.GetBlueprint<BlueprintFeature>("ec44af8b3449c5b4889145dbfc246a00");
            var DRSlashing10 = Resources.GetBlueprint<BlueprintFeature>("0df8cdae87d2a3047ad2b1c0568407e9");
            var FireVulnerability = Resources.GetBlueprint<BlueprintFeature>("8e934134fec60ab4c8972c85a7b62f89");
            var AcidResistance20 = Resources.GetBlueprint<BlueprintFeature>("416386972c8de2e42953533c4946599a");
            var TripImmunity = Resources.GetBlueprint<BlueprintFeature>("c1b26f97b974aec469613f968439e7bb");
            var BlindSight = Resources.GetBlueprint<BlueprintFeature>("236ec7f226d3d784884f066aa4be1570");
            var OverrunAbility = Resources.GetBlueprint<BlueprintAbility>("1a3b471ecea51f7439a946b23577fd70");
            var BeastShapeIBuffPolymorph = Resources.GetBlueprint<BlueprintBuff>("00d8fbe9cf61dc24298be8d95500c84b").GetComponent<Polymorph>();
            var BeastShapeIBuffSuppressBuffs = Resources.GetBlueprint<BlueprintBuff>("00d8fbe9cf61dc24298be8d95500c84b").GetComponents<SuppressBuffs>();
            var BeastShapeShamblingMoundBuff = Resources.GetBlueprint<BlueprintBuff>("50ab9c820eb9cf94d8efba3632ad5ce2");
            var BeastShapeShamblingMoundBuffPolymorph = Resources.GetBlueprint<BlueprintBuff>("50ab9c820eb9cf94d8efba3632ad5ce2").GetComponent<Polymorph>();
            var MandragoraBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("d11108d2f1662a842929f53e16bd6742");
            var TreantBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("bb9ffa4bd65336f4f99ebd3a234f90cf");
            var FlytrapBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("6400a869e4026f242af0c3da506ecdd6");
            var QuickwoodBarks = Resources.GetBlueprint<BlueprintUnitAsksList>("a84220a063d385f4d87dd844d397c8ed");

            var Mandragora = Resources.GetBlueprint<BlueprintUnit>("5186793686fa66240ac47624c6461865");
            //Name of PlantShapeII blueprints are odd as the 2nd version was added in a later patch

            var PlantShapeIBuff = Helpers.CreateBuff("PlantShapeIBuff", bp => {
                bp.SetName("Plant Shape (Mandragora)");
                bp.SetDescription("You are in mandragora form now. You have a +2 size bonus to Dexterity and Constitution and a +2 natural armor bonus. " +
                    "Your movement speed is increased by 10 feet. You also have one 1d6 bite attack, two 1d4 slams and poison ability.");
                bp.m_Icon = PlantShapeIIcon;
                bp.AddComponent<Polymorph>(c => {
                    c.m_Race = BeastShapeIBuffPolymorph.m_Race;
                    c.m_Prefab = Mandragora.Prefab;
                    c.m_PrefabFemale = BeastShapeIBuffPolymorph.m_PrefabFemale;
                    c.m_SpecialDollType = SpecialDollType.None;
                    c.m_ReplaceUnitForInspection = BeastShapeIBuffPolymorph.m_ReplaceUnitForInspection;
                    c.m_Portrait = BeastShapeIBuffPolymorph.m_Portrait;                    
                    c.m_KeepSlots = false;
                    c.Size = Size.Small;
                    c.UseSizeAsBaseForDamage = false;
                    c.StrengthBonus = 0;
                    c.DexterityBonus = 2;
                    c.ConstitutionBonus = 2;
                    c.NaturalArmor = 2;
                    c.AllowDamageTransfer = false;
                    c.m_MainHand = Bite1d6.ToReference<BlueprintItemWeaponReference>();
                    c.m_OffHand = Slam1d4.ToReference<BlueprintItemWeaponReference>();
                    c.AllowDamageTransfer = false;
                    c.m_AdditionalLimbs = new BlueprintItemWeaponReference[] {                        
                        Slam1d4.ToReference<BlueprintItemWeaponReference>()
                    };
                    c.m_SecondaryAdditionalLimbs = new BlueprintItemWeaponReference[0];
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        TurnBarkStandart.ToReference<BlueprintUnitFactReference>(),
                        MandragoraPoisonFeature.ToReference<BlueprintUnitFactReference>()
                    };
                    c.m_EnterTransition = BeastShapeIBuffPolymorph.m_EnterTransition;
                    c.m_ExitTransition = BeastShapeIBuffPolymorph.m_ExitTransition;
                    c.m_TransitionExternal = BeastShapeIBuffPolymorph.m_TransitionExternal;
                    c.m_SilentCaster = true;
                });
                bp.AddComponent<BuffMovementSpeed>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Value = 10;
                    c.ContextBonus = new ContextValue();
                    c.CappedOnMultiplier = false;
                    c.CappedMinimum = false;
                });
                bp.AddComponent<ReplaceAsksList>(c => {
                    c.m_Asks = MandragoraBarks.ToReference<BlueprintUnitAsksListReference>();
                });
                bp.AddComponent<ReplaceCastSource>(c => {
                    c.CastSource = CastSource.Head;
                });
                //bp.AddComponent<ReplaceSourceBone>(c => {
                //   c.SourceBone = ;
                //});
                bp.AddComponents(BeastShapeIBuffSuppressBuffs);
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var PlantShapeIIBuff = Helpers.CreateBuff("PlantShapeIIBuff", bp => {
                bp.SetName("Plant Shape (Shambling Mound)");
                bp.m_Description = BeastShapeShamblingMoundBuff.m_Description;
                bp.m_Icon = PlantShapeIIIcon;
                bp.Components = BeastShapeShamblingMoundBuff.Components;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var PlantShapeIIQuickwoodBuff = Helpers.CreateBuff("PlantShapeIIQuickwoodBuff", bp => {
                bp.SetName("Plant Shape (Quickwood)");
                bp.SetDescription("You become a huge quickwood. You gain a +4 size bonus to your Strength, a +2 size bonus to your Constitution, +4 natural " +
                    "armor bonus, resist fire 20, resist electricity 20, and spell resistance equal to 10 + half your {g|Encyclopedia:Caster_Level}caster level{/g}. " +
                    "Your movement speed is reduced by 10 feet. You also have one 2d6 bite attack and two 1d4 root (tentacle) attacks.");
                bp.m_Icon = PlantShapeIIcon;
                bp.AddComponent<Polymorph>(c => {
                    c.m_Race = BeastShapeIBuffPolymorph.m_Race;
                    c.m_Prefab = new UnitViewLink() { AssetId = "20ab823ad1fa2344696f4467b831aed5" }; 
                    c.m_PrefabFemale = BeastShapeIBuffPolymorph.m_PrefabFemale;
                    c.m_SpecialDollType = SpecialDollType.None;
                    c.m_ReplaceUnitForInspection = BeastShapeIBuffPolymorph.m_ReplaceUnitForInspection;
                    c.m_Portrait = BeastShapeIBuffPolymorph.m_Portrait;
                    c.m_KeepSlots = false;
                    c.Size = Size.Huge;
                    c.UseSizeAsBaseForDamage = false;
                    c.StrengthBonus = 4;
                    c.DexterityBonus = 0;
                    c.ConstitutionBonus = 2;
                    c.NaturalArmor = 4;
                    c.AllowDamageTransfer = false;
                    c.m_MainHand = Bite2d6.ToReference<BlueprintItemWeaponReference>();
                    c.m_OffHand = QuickwoodRoots.ToReference<BlueprintItemWeaponReference>();
                    c.AllowDamageTransfer = false;
                    c.m_AdditionalLimbs = new BlueprintItemWeaponReference[] {
                        QuickwoodRoots.ToReference<BlueprintItemWeaponReference>()
                    };
                    c.m_SecondaryAdditionalLimbs = new BlueprintItemWeaponReference[0];
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        TurnBarkStandart.ToReference<BlueprintUnitFactReference>()
                    };
                    c.m_EnterTransition = BeastShapeIBuffPolymorph.m_EnterTransition;
                    c.m_ExitTransition = BeastShapeIBuffPolymorph.m_ExitTransition;
                    c.m_TransitionExternal = BeastShapeIBuffPolymorph.m_TransitionExternal;
                    c.m_SilentCaster = true;
                });
                bp.AddComponent<ResistEnergy>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 20
                    };
                    c.Type = DamageEnergyType.Electricity;
                    c.UsePool = false;
                    c.UseValueMultiplier = false;
                    c.HealOnDamage = false;
                });
                bp.AddComponent<ResistEnergy>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 20
                    };
                    c.Type = DamageEnergyType.Fire;
                    c.UsePool = false;
                    c.UseValueMultiplier = false;
                    c.HealOnDamage = false;
                });
                bp.AddComponent<BuffMovementSpeed>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Value = -10;
                    c.ContextBonus = new ContextValue();
                    c.CappedOnMultiplier = false;
                    c.CappedMinimum = false;
                });
                bp.AddComponent<AddSpellResistance>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.StatBonus
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterLevel;
                    c.m_Progression = ContextRankProgression.Div2;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.StatBonus;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.Default
                        },
                        BonusValue = 10
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ReplaceAsksList>(c => {
                    c.m_Asks = QuickwoodBarks.ToReference<BlueprintUnitAsksListReference>();
                });
                bp.AddComponent<ReplaceCastSource>(c => {
                    c.CastSource = CastSource.Head;
                });
                //bp.AddComponent<ReplaceSourceBone>(c => {
                //   c.SourceBone = ;
                //});
                bp.AddComponents(BeastShapeIBuffSuppressBuffs);
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var PlantShapeIIITreantBuff = Helpers.CreateBuff("PlantShapeIIITreantBuff", bp => {
                bp.SetName("Plant Shape (Treant)");
                bp.SetDescription("You are in treant form now. You have a +8 size bonus to your Strength, +4 to Constitution, -2 penalty to Dexterity and a +6 natural " +
                    "armor bonus. You also have two 2d6 slam attacks, damage reduction 10/slashing, vulnerability to fire and overrun ability.");
                bp.m_Icon = PlantShapeIIIIcon;
                bp.AddComponent<Polymorph>(c => {
                    c.m_Race = BeastShapeIBuffPolymorph.m_Race;
                    c.m_Prefab = new UnitViewLink() { AssetId = "16f5bf5f4dc3c9e4dab4165b360a5e3d" };
                    c.m_PrefabFemale = BeastShapeIBuffPolymorph.m_PrefabFemale;
                    c.m_SpecialDollType = SpecialDollType.None;
                    c.m_ReplaceUnitForInspection = BeastShapeIBuffPolymorph.m_ReplaceUnitForInspection;
                    c.m_Portrait = BeastShapeIBuffPolymorph.m_Portrait;
                    c.m_KeepSlots = false;
                    c.Size = Size.Huge;
                    c.UseSizeAsBaseForDamage = false;
                    c.StrengthBonus = 8;
                    c.DexterityBonus = -2;
                    c.ConstitutionBonus = 4;
                    c.NaturalArmor = 6;
                    c.AllowDamageTransfer = false;
                    c.m_MainHand = SlamGargantuan2n6.ToReference<BlueprintItemWeaponReference>();
                    c.m_OffHand = SlamGargantuan2n6.ToReference<BlueprintItemWeaponReference>();
                    c.m_AdditionalLimbs = new BlueprintItemWeaponReference[0];
                    c.m_SecondaryAdditionalLimbs = new BlueprintItemWeaponReference[0];
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        TurnBarkStandart.ToReference<BlueprintUnitFactReference>(),
                        DRSlashing10.ToReference<BlueprintUnitFactReference>(),
                        FireVulnerability.ToReference<BlueprintUnitFactReference>(),
                        OverrunAbility.ToReference<BlueprintUnitFactReference>()
                    };
                    c.m_EnterTransition = BeastShapeShamblingMoundBuffPolymorph.m_EnterTransition;
                    c.m_ExitTransition = BeastShapeShamblingMoundBuffPolymorph.m_ExitTransition;
                    c.m_TransitionExternal = BeastShapeShamblingMoundBuffPolymorph.m_TransitionExternal;
                    c.m_SilentCaster = true;
                });
                bp.AddComponent<ReplaceAsksList>(c => {
                    c.m_Asks = TreantBarks.ToReference<BlueprintUnitAsksListReference>();
                });
                bp.AddComponent<ReplaceCastSource>(c => {
                    c.CastSource = CastSource.Head;
                });
                //bp.AddComponent<ReplaceSourceBone>(c => {
                //   c.SourceBone = ;
                //});
                bp.AddComponents(BeastShapeIBuffSuppressBuffs);
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });
            var PlantShapeIIIGiantFlytrapBuff = Helpers.CreateBuff("PlantShapeIIIGiantFlytrapBuff", bp => {
                bp.SetName("Plant Shape (Giant Flytrap)");
                bp.SetDescription("You are in giant flytrap form now. You have a +8 size bonus to your Strength, +4 to Constitution, -2 penalty to Dexterity and a +6 natural " +
                    "armor bonus. You also have four 1d8 bite attacks, acid Resistance 20, immunity to trip and blindsight.");
                bp.m_Icon = PlantShapeIIIIcon;
                bp.AddComponent<Polymorph>(c => {
                    c.m_Race = BeastShapeIBuffPolymorph.m_Race;
                    c.m_Prefab = new UnitViewLink() { AssetId = "c091d2aca0b6c3c45bbcc3d9a5394c7a" };
                    c.m_PrefabFemale = BeastShapeIBuffPolymorph.m_PrefabFemale;
                    c.m_SpecialDollType = SpecialDollType.None;
                    c.m_ReplaceUnitForInspection = BeastShapeIBuffPolymorph.m_ReplaceUnitForInspection;
                    c.m_Portrait = BeastShapeIBuffPolymorph.m_Portrait;
                    c.m_KeepSlots = false;
                    c.Size = Size.Huge;
                    c.UseSizeAsBaseForDamage = false;
                    c.StrengthBonus = 8;
                    c.DexterityBonus = -2;
                    c.ConstitutionBonus = 4;
                    c.NaturalArmor = 6;
                    c.AllowDamageTransfer = false;
                    c.m_MainHand = BiteLarge1d8.ToReference<BlueprintItemWeaponReference>();
                    c.m_OffHand = BiteLarge1d8.ToReference<BlueprintItemWeaponReference>();
                    c.m_AdditionalLimbs = new BlueprintItemWeaponReference[] {
                        BiteLarge1d8.ToReference<BlueprintItemWeaponReference>(),
                        BiteLarge1d8.ToReference<BlueprintItemWeaponReference>()
                    };
                    c.m_SecondaryAdditionalLimbs = new BlueprintItemWeaponReference[0];
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        TurnBarkStandart.ToReference<BlueprintUnitFactReference>(),
                        BlindSight.ToReference<BlueprintUnitFactReference>(),
                        TripImmunity.ToReference<BlueprintUnitFactReference>()
                    };
                    c.m_EnterTransition = BeastShapeShamblingMoundBuffPolymorph.m_EnterTransition;
                    c.m_ExitTransition = BeastShapeShamblingMoundBuffPolymorph.m_ExitTransition;
                    c.m_TransitionExternal = BeastShapeShamblingMoundBuffPolymorph.m_TransitionExternal;
                    c.m_SilentCaster = true;
                });
                bp.AddComponent<ResistEnergy>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 20
                    };
                    c.Type = DamageEnergyType.Acid;
                    c.UsePool = false;
                    c.UseValueMultiplier = false;
                    c.HealOnDamage = false;
                });
                bp.AddComponent<ReplaceAsksList>(c => {
                    c.m_Asks = FlytrapBarks.ToReference<BlueprintUnitAsksListReference>();
                });
                bp.AddComponent<ReplaceCastSource>(c => {
                    c.CastSource = CastSource.Head;
                });
                //bp.AddComponent<ReplaceSourceBone>(c => {
                //   c.SourceBone = ;
                //});
                bp.AddComponents(BeastShapeIBuffSuppressBuffs);
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.Stacking = StackingType.Replace;
            });

            var PlantShapeIAbility = Helpers.CreateBlueprint<BlueprintAbility>("PlantShapeIAbility", bp => {
                bp.SetName("Plant Shape I");
                bp.SetDescription("You become a small mandragora. You gain a +2 size bonus to your Dexterity and Constitution and a +2 natural armor bonus. " +
                    "Your movement speed is increased by 10 feet. You also gain one 1d6 bite attack, two 1d4 slams and poison ability.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(                                                
                        new ContextActionApplyBuff() {
                            m_Buff = PlantShapeIBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] {PlantShapeIBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
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
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic =  Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("PlantShapeIAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var PlantShapeIIAbility = Helpers.CreateBlueprint<BlueprintAbility>("PlantShapeIIAbility", bp => {
                bp.SetName("Plant Shape II (Shambling Mound)");
                bp.SetDescription("You become a large shambling mound. You gain a +4 size bonus to your Strength, a +2 size bonus to your Constitution, +4 natural " +
                    "armor bonus, resist fire 20, and resist electricity 20. Your movement speed is reduced by 10 feet. You also have two 2d6 slam attacks, the constricting " +
                    "vines ability, and the poison ability.\nConstricting Vines: A shambling mound's vines coil around any creature it hits with a slam attack. The shambling " +
                    "mound attempts a grapple maneuver check against its target, and on a successful check its vines deal 2d6+5 damage and the foe is grappled.\nGrappled " +
                    "characters cannot move, and take a -2 penalty on all attack rolls and a -4 penalty to Dexterity. Grappled characters attempt to escape every round by " +
                    "making a successful combat maneuver, Strength, Athletics, or Mobility check. The DC of this check is the shambling mound's CMD.\nEach round, creatures " +
                    "grappled by a shambling mound suffer 4d6+Strength modifier × 2 damage.\nA shambling mound receives a +4 bonus on grapple maneuver checks.\nPoison:\nSlam; " +
                    "Save: Fortitude\nFrequency: 1/round for 2 rounds\nEffect: 1d2 Strength and 1d2 Dexterity damage\nCure: 1 save\nThe save DC is Constitution-based.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = PlantShapeIIBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { PlantShapeIIBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
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
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("PlantShapeIIAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var PlantShapeIIQuickwoodAbility = Helpers.CreateBlueprint<BlueprintAbility>("PlantShapeIIQuickwoodAbility", bp => {
                bp.SetName("Plant Shape II (Quickwood)");
                bp.SetDescription("You become a huge quickwood. You gain a +4 size bonus to your Strength, a +2 size bonus to your Constitution, +4 natural " +
                    "armor bonus, resist fire 20, resist electricity 20, and spell resistance equal to 10 + half your {g|Encyclopedia:Caster_Level}caster level{/g}. " +
                    "Your movement speed is reduced by 10 feet. You also have one 2d6 bite attack and two 1d4 root (tentacle) attacks.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = PlantShapeIIQuickwoodBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { PlantShapeIIQuickwoodBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
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
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("PlantShapeIIQuickwoodAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var PlantShapeIIParentAbility = Helpers.CreateBlueprint<BlueprintAbility>("PlantShapeIIParentAbility", bp => {
                bp.SetName("Plant Shape II");
                bp.SetDescription("You become a large shambling mound or a huge quickwood.");
                bp.AddComponent<AbilityVariants>(c => {
                    c.m_Variants = new BlueprintAbilityReference[] {
                        PlantShapeIIAbility.ToReference<BlueprintAbilityReference>(),
                        PlantShapeIIQuickwoodAbility.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("PlantShapeIIIAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            PlantShapeIIAbility.m_Parent = PlantShapeIIParentAbility.ToReference<BlueprintAbilityReference>();
            PlantShapeIIQuickwoodAbility.m_Parent = PlantShapeIIParentAbility.ToReference<BlueprintAbilityReference>();


            var PlantShapeIIITreantAbility = Helpers.CreateBlueprint<BlueprintAbility>("PlantShapeIIITreantAbility", bp => {
                bp.SetName("Plant Shape III (Treant)");
                bp.SetDescription("You become a huge treant. You gain a +8 size bonus to your Strength, +4 to Constitution, -2 penalty to Dexterity and a +6 " +
                    "natural armor bonus. You also gain two 2d6 slam attacks, damage reduction 10/slashing, vulnerability to fire and overrun ability.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = PlantShapeIIITreantBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { PlantShapeIIITreantBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
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
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("PlantShapeIIITreantAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var PlantShapeIIIGiantFlytrapAbility = Helpers.CreateBlueprint<BlueprintAbility>("PlantShapeIIIGiantFlytrapAbility", bp => {
                bp.SetName("Plant Shape III (Giant Flytrap)");
                bp.SetDescription("You become a huge giant flytrap. You gain a +8 size bonus to your Strength, +4 to Constitution, -2 penalty to Dexterity and a +6 " +
                    "natural armor bonus. You also gain four 1d8 bite attacks, acid Resistance 20 and blindsight and poison ability.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = PlantShapeIIIGiantFlytrapBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { PlantShapeIIIGiantFlytrapBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
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
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("PlantShapeIIIGiantFlytrapAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var PlantShapeIIIAbility = Helpers.CreateBlueprint<BlueprintAbility>("PlantShapeIIIAbility", bp => {
                bp.SetName("Plant Shape III");
                bp.SetDescription("You become a huge treant or a huge giant flytrap.");
                bp.AddComponent<AbilityVariants>(c => {
                    c.m_Variants = new BlueprintAbilityReference[] {
                        PlantShapeIIITreantAbility.ToReference<BlueprintAbilityReference>(),
                        PlantShapeIIIGiantFlytrapAbility.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("PlantShapeIIIAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            PlantShapeIIITreantAbility.m_Parent = PlantShapeIIIAbility.ToReference<BlueprintAbilityReference>();
            PlantShapeIIIGiantFlytrapAbility.m_Parent = PlantShapeIIIAbility.ToReference<BlueprintAbilityReference>();



            //I'll add the scrolls when I have time for more art
            //var PlantShapeIScroll = ItemTools.CreateScroll("ScrollOfPlantShapeI", Icon_ScrollOfPlantShapeI, PlantShapeIAbility, 5, 9);
            //VenderTools.AddScrollToLeveledVenders(PlantShapeIScroll);
            PlantShapeIAbility.AddToSpellList(SpellTools.SpellList.AlchemistSpellList, 5);
            PlantShapeIAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 5);

            //var PlantShapeIIScroll = ItemTools.CreateScroll("ScrollOfPlantShapeII", Icon_ScrollOfPlantShapeII, PlantShapeIIAbility, 6, 11);
            //VenderTools.AddScrollToLeveledVenders(PlantShapeIIScroll);
            PlantShapeIIParentAbility.AddToSpellList(SpellTools.SpellList.AlchemistSpellList, 6);
            PlantShapeIIParentAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 6);

            //var PlantShapeIIIScroll = ItemTools.CreateScroll("ScrollOfPlantShapeIII", Icon_ScrollOfPlantShapeIII, PlantShapeIIIAbility, 7, 13);
            //VenderTools.AddScrollToLeveledVenders(PlantShapeIIIScroll);
            PlantShapeIIIAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 7);
        }
    }
}
