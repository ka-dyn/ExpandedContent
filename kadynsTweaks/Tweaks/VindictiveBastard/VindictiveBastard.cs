using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System.Collections.Generic;
using kadynsTweaks.Extensions;
using kadynsTweaks.Utilities;
using System;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Blueprints.Classes.Selection;
using kadynsTweaks.Config;
using Kingmaker.UnitLogic.ActivatableAbilities;
using HarmonyLib;
using System.Linq;

namespace kadynsTweaks.NewWIP.VindictiveBastard {
    internal class VindictiveBastard {
        private static readonly BlueprintAbility VindictiveBastardVindictiveSmiteAbility = Resources.GetModBlueprint<BlueprintAbility>("VindictiveBastardVindictiveSmiteAbility");
        private static readonly BlueprintArchetype VindictiveBastardArchetype = Resources.GetModBlueprint<BlueprintArchetype>("VindictiveBastardArchetype");

        private static readonly BlueprintCharacterClass PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
        private static readonly BlueprintFeature SmiteEvilFeature = Resources.GetBlueprint<BlueprintFeature>("3a6db57fce75b0244a6a5819528ddf26");
        private static readonly BlueprintAbility SmiteEvilAbility = Resources.GetBlueprint<BlueprintAbility>("7bb9eb2042e67bf489ccd1374423cdec");
        private static readonly BlueprintBuff SmiteEvilBuff = Resources.GetBlueprint<BlueprintBuff>("b6570b8cbb32eaf4ca8255d0ec3310b0");
        
        private static readonly BlueprintActivatableAbility BattleMeditationAbility = Resources.GetBlueprint<BlueprintActivatableAbility>("a7a3303c8ab81914e8ecca76aedc70ec");
        private static readonly BlueprintFeature InquisitorSoloTacticsFeature = Resources.GetBlueprint<BlueprintFeature>("5602845cd22683840a6f28ec46331051");
        private static readonly BlueprintAbilityResource VindictiveBastardVindictiveSmiteResource = Resources.GetModBlueprint<BlueprintAbilityResource>("VindictiveBastardVindictiveSmiteResource");
        private static readonly BlueprintBuff VindictiveBastardVindictiveSmiteBuff = Resources.GetModBlueprint<BlueprintBuff>("VindictiveBastardVindictiveSmiteBuff");

        private static readonly BlueprintFeature PaladinLayOnHands = Resources.GetBlueprint<BlueprintFeature>("a1adf65aad7a4f3ba9a7a18e6075a2ec");
        private static readonly BlueprintFeature PaladinDivineBond = Resources.GetBlueprint<BlueprintFeature>("bf8a4b51ff7b41c3b5aa139e0fe16b34");
        public static void AddVindictiveBastard() {

            var VindictiveBastardVindictiveSmiteResource = Resources.GetModBlueprint<BlueprintAbilityResource>("VindictiveBastardVindictiveSmiteResource");
            var VindictiveBastardVindictiveSmiteFeature = Helpers.CreateBlueprint<BlueprintFeature>("VindictiveBastardVindictiveSmiteFeature", bp => {
                bp.SetName("Vindictive Smite");
                bp.SetDescription("A vindictive bastard is particularly ruthless against those who have harmed her or her allies. " +
              "Once per day as a swift action, she can smite one target within sight who is engaged in melee combat with her or an ally. " +
              "She adds her Charisma modifier to her attack rolls and adds her paladin level to damage rolls against the target of her smite. " +
              "In addition, while vindictive smite is in effect, the vindictive bastard gains a deflection bonus equal to her Charisma bonus (if any) " +
              "to her AC against attacks by the target of the smite. If the target of vindictive smite has rendered an ally of the vindictive bastard " +
              "unconscious or dead within the last 24 hours, the bonus on damage rolls on the first attack that hits increases by 2 for every paladin " +
              "level she has. The vindictive smite effect remains until the target of the smite is dead or the next time the vindictive bastard rests " +
              "and regains her uses of this ability.At 4th level and every 3 levels thereafter, the vindictive bastard can invoke her vindictive smite " +
              "one additional time per day, to a maximum of seven times per day at 19th level." +
              "This replaces smite evil.");
                bp.IsClassFeature = true;
                bp.m_Icon = SmiteEvilAbility.Icon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = VindictiveBastardVindictiveSmiteResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        VindictiveBastardVindictiveSmiteAbility.ToReference<BlueprintUnitFactReference>(),
                        SmiteEvilFeature.ToReference<BlueprintUnitFactReference>()

                    };
                });
            });
            var IronWill = Resources.GetBlueprint<BlueprintFeature>("175d1577bb6c9a04baf88eec99c66334").ToReference<BlueprintFeatureReference>();
            var GreatFortitude = Resources.GetBlueprint<BlueprintFeature>("79042cb55f030614ea29956177977c52").ToReference<BlueprintFeatureReference>();
            var LightningReflexes = Resources.GetBlueprint<BlueprintFeature>("15e7da6645a7f3d41bdad7c8c4b9de1e").ToReference<BlueprintFeatureReference>();
            var VindictiveBastardFadedGrace = Helpers.CreateBlueprint<BlueprintFeatureSelection>("VindictiveBastardFadedGrace", bp => {
                bp.SetName("Faded Grace");
                bp.SetDescription("At 2nd level, a vindictive bastard gains one of the following as a bonus feat: Great Fortitude, " +
                    "Iron Will, or Lightning Reflexes. This replaces divine grace.");
                bp.IsClassFeature = true;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.HideNotAvailibleInUI = true;
                bp.m_Features = new BlueprintFeatureReference[] {
                    GreatFortitude,
                    IronWill,
                    LightningReflexes
                    };
                bp.m_AllFeatures = bp.m_Features;
            });
            var VindictiveBastardSoloTacticsResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("VindictiveBastardSoloTacticsResource", bp => {
                bp.m_Min = 1;
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Charisma,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        PaladinClass.ToReference<BlueprintCharacterClassReference>()
                        },
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        VindictiveBastardArchetype.ToReference<BlueprintArchetypeReference>()
                        },
                    IncreasedByLevelStartPlusDivStep = true,
                    StartingLevel = 2,
                    LevelStep = 2,
                    PerStepIncrease = 1
                };
            });
            var VindictiveBastardSoloTacticsAbility = Helpers.CreateBlueprint<BlueprintAbility>("VindictiveBastardSoloTacticsAbility", bp => {
                bp.SetName("Solo Tactics");
                bp.SetDescription("At 2nd level, a vindictive bastard gains solo tactics, as per the inquisitor class feature. She can " +
                    "activate this ability as a swift action and gains the benefits of it for 1 round. She can use this ability a " +
                    "number of rounds per day equal to half her paladin level + her Charisma modifier.This replaces lay on hands.");
                bp.m_Icon = BattleMeditationAbility.Icon;
                bp.Type = AbilityType.Extraordinary;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { InquisitorSoloTacticsFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = VindictiveBastardSoloTacticsResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });





            });
            var VindictiveBastardArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("VindictiveBastardArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString("VindictiveBastardArchetype.Name", "Vindictive Bastard");
                bp.LocalizedDescription = Helpers.CreateString("VindictiveBastardArchetype.Description", "While paladins often collaborate " +
                    "with less righteous adventurers in order to further their causes, those who spend too much time around companions with particularly loose morals" +
                    " run the risk of adopting those same unscrupulous ideologies and methods. Such a vindictive bastard, as these fallen paladins are known," +
                    " strikes out for retribution and revenge, far more interested in tearing down those who have harmed her or her companions" +
                    " than furthering a distant deity’s cause.");
            });

                PaladinClass.m_Archetypes = PaladinClass.m_Archetypes.AppendToArray(VindictiveBastardArchetype.ToReference<BlueprintArchetypeReference>());





                Main.LogPatch("Added", VindictiveBastardArchetype);
            }
    
        }



    }



   
       

