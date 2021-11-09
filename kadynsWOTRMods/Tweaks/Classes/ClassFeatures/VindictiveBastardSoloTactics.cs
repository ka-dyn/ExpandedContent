using kadynsWOTRMods;
using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kadynsTweaks.NewWIP.VindictiveBastard
{
    internal class VindictiveBastardSoloTactics
    {

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
        private static readonly BlueprintFeatureSelection TeamworkFeat = Resources.GetBlueprint<BlueprintFeatureSelection>("d87e2f6a9278ac04caeb0f93eff95fcb");

        private static readonly BlueprintFeature AlliedSpellcaster = Resources.GetBlueprint<BlueprintFeature>("9093ceeefe9b84746a5993d619d7c86f");
        private static readonly BlueprintFeature BackToBack = Resources.GetBlueprint<BlueprintFeature>("c920f2cd2244d284aa69a146aeefcb2c");
        private static readonly BlueprintFeature CoordinatedDefense = Resources.GetBlueprint<BlueprintFeature>("992fd59da1783de49b135ad89142c6d7");
        private static readonly BlueprintFeature CoordinatedManuevers = Resources.GetBlueprint<BlueprintFeature>("b186cea78dce3a04aacff0a81786008c");
        private static readonly BlueprintFeature Outflank = Resources.GetBlueprint<BlueprintFeature>("422dab7309e1ad343935f33a4d6e9f11");
        private static readonly BlueprintFeature PreciseStrike = Resources.GetBlueprint<BlueprintFeature>("5662d1b793db90c4b9ba68037fd2a768");
        private static readonly BlueprintFeature ShakeItOff = Resources.GetBlueprint<BlueprintFeature>("6337b37f2a7c11b4ab0831d6780bce2a");
        private static readonly BlueprintFeature ShieldedCaster = Resources.GetBlueprint<BlueprintFeature>("0b707584fc2ea724aa72c396c2230dc7");
        private static readonly BlueprintFeature ShieldWall = Resources.GetBlueprint<BlueprintFeature>("8976de442862f82488a4b138a0a89907");
        private static readonly BlueprintFeature SiezeTheMoment = Resources.GetBlueprint<BlueprintFeature>("1191ef3065e6f8e4f9fbe1b7e3c0f760");
        private static readonly BlueprintFeature TandemTrip = Resources.GetBlueprint<BlueprintFeature>("d26eb8ab2aabd0e45a4d7eec0340bbce");

        public static void AddVindictiveSoloTactics()
        {

            var VindictiveBastardSoloTacticsResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("VindictiveBastardSoloTacticsResource", bp =>
            {
                bp.m_Min = 1;
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount
                {
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
            var VindictiveBastardSoloTacticsAbility = Helpers.CreateBlueprint<BlueprintAbility>("VindictiveBastardSoloTacticsAbility", bp =>
            {
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
                bp.AddComponent<AddFacts>(c =>
                {
                    c.m_Facts = new BlueprintUnitFactReference[] { InquisitorSoloTacticsFeature.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c =>
                {
                    c.m_RequiredResource = VindictiveBastardSoloTacticsResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                    c.ResourceCostIncreasingFacts = new List<BlueprintUnitFactReference>();
                    c.ResourceCostDecreasingFacts = new List<BlueprintUnitFactReference>();
                });
            });
            var VindictiveBastardTeamworkFeat = Helpers.CreateBlueprint<BlueprintFeatureSelection>("VindictiveBastardTeamworkFeat", bp =>
            {
                bp.SetName("Teamwork Feat");
                bp.SetDescription("At 3rd level and every 6 levels thereafter, the vindictive bastard gains a bonus feat in addition to those gained from normal advancement. " +
                    "These bonus feats must be selected from those listed as teamwork feats. " +
                    "The vindictive bastard must meet the prerequisites of the selected bonus feat.");
                bp.m_Icon = TeamworkFeat.Icon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    AlliedSpellcaster.ToReference<BlueprintFeatureReference>(),
                    BackToBack.ToReference<BlueprintFeatureReference>(),
                    CoordinatedDefense.ToReference<BlueprintFeatureReference>(),
                    CoordinatedManuevers.ToReference<BlueprintFeatureReference>(),
                    Outflank.ToReference<BlueprintFeatureReference>(),
                    PreciseStrike.ToReference<BlueprintFeatureReference>(),
                    ShakeItOff.ToReference<BlueprintFeatureReference>(),
                    ShieldedCaster.ToReference<BlueprintFeatureReference>(),
                    ShieldWall.ToReference<BlueprintFeatureReference>(),
                    SiezeTheMoment.ToReference<BlueprintFeatureReference>(),
                    TandemTrip.ToReference<BlueprintFeatureReference>()
                };
                bp.IsClassFeature = true;
            });
        }
    }
}
    

