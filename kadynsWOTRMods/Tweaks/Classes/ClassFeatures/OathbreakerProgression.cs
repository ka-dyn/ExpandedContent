using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kadynsWOTRMods.Tweaks.Classes.ClassFeatures
{
    internal class OathbreakerProgression
    {
        private static readonly BlueprintAbility OathbreakersBaneAbility = Resources.GetModBlueprint<BlueprintAbility>("OathbreakersBaneAbility");
        private static readonly BlueprintFeature BreakerOfOaths = Resources.GetModBlueprint<BlueprintFeature>("BreakerOfOaths");
        private static readonly BlueprintFeature OathbreakersBaneFeature = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersBaneFeature");
        private static readonly BlueprintFeature OathbreakerStalwart = Resources.GetModBlueprint<BlueprintFeature>("OathbreakerStalwart");
        private static readonly BlueprintFeature AuraOfSelfRighteousness = Resources.GetModBlueprint<BlueprintFeature>("AuraOfSelfRighteousness");
        private static readonly BlueprintCharacterClass PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
        private static readonly BlueprintFeature SmiteEvilFeature = Resources.GetBlueprint<BlueprintFeature>("3a6db57fce75b0244a6a5819528ddf26");
        private static readonly BlueprintAbility SmiteEvilAbility = Resources.GetBlueprint<BlueprintAbility>("7bb9eb2042e67bf489ccd1374423cdec");
        private static readonly BlueprintBuff SmiteEvilBuff = Resources.GetBlueprint<BlueprintBuff>("b6570b8cbb32eaf4ca8255d0ec3310b0");
        private static readonly BlueprintFeature OathbreakerSoloTacticsFeature = Resources.GetModBlueprint<BlueprintFeature>("OathbreakerSoloTactics");

        private static readonly BlueprintActivatableAbility BattleMeditationAbility = Resources.GetBlueprint<BlueprintActivatableAbility>("a7a3303c8ab81914e8ecca76aedc70ec");
        private static readonly BlueprintFeature InquisitorSoloTacticsFeature = Resources.GetBlueprint<BlueprintFeature>("5602845cd22683840a6f28ec46331051");
        private static readonly BlueprintAbilityResource OathbreakersBaneResource = Resources.GetModBlueprint<BlueprintAbilityResource>("OathbreakersBaneResource");
        private static readonly BlueprintBuff OathbreakersBaneBuff = Resources.GetModBlueprint<BlueprintBuff>("OathbreakersBaneBuff");

        private static readonly BlueprintFeature PaladinClassProficiencies = Resources.GetBlueprint<BlueprintFeature>("b10ff88c03308b649b50c31611c2fefb");
        private static readonly BlueprintFeature PaladinLayOnHands = Resources.GetBlueprint<BlueprintFeature>("a1adf65aad7a4f3ba9a7a18e6075a2ec");
        private static readonly BlueprintFeature PaladinDivineBond = Resources.GetBlueprint<BlueprintFeature>("bf8a4b51ff7b41c3b5aa139e0fe16b34");

        private static readonly BlueprintFeature OathbreakerProficiencies = Resources.GetModBlueprint<BlueprintFeature>("OathbreakerProficiencies");
        private static readonly BlueprintFeature OathbreakersDirectionFeature = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersDirectionFeature");
        private static readonly BlueprintFeature FadedGrace = Resources.GetModBlueprint<BlueprintFeature>("FadedGrace");
        private static readonly BlueprintFeature SpitefulTenacity = Resources.GetModBlueprint<BlueprintFeature>("SpitefulTenacity");
        private static readonly BlueprintFeature OathbreakersBaneUse = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersBaneUse");
        private static readonly BlueprintCharacterClass OathbreakerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("OathbreakerClass");
        private static readonly BlueprintFeature OathbreakerSoloTactics = Resources.GetModBlueprint<BlueprintFeature>("OathbreakerSoloTactics");
        private static readonly BlueprintFeature OathbreakerTeamworkFeat = Resources.GetModBlueprint<BlueprintFeatureSelection>("OathbreakerTeamworkFeat");
        private static readonly BlueprintFeature OathbreakersDirectionSwiftFeature = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersDirectionSwiftFeature");
        public static void AddOathbreakerProgression()
        {



            var OathbreakerProficiencies = Helpers.CreateBlueprint<BlueprintFeature>("OathbreakerProficiencies", bp =>
            {
                bp.SetName("Oathbreaker Proficiences");
                bp.SetDescription("Oathbreakers are proficient with all simple and {g|Encyclopedia:Weapon_Proficiency}martial weapons{/g}, with all types of armor " +
                    "(heavy, medium, and light), and with shields (except tower shields).");
                bp.AddComponent<AddFacts>(c =>
                {
                    c.m_Facts = new BlueprintUnitFactReference[] { PaladinClassProficiencies.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });


            var BreakerOfOaths = Resources.GetModBlueprint<BlueprintFeature>("BreakerOfOaths");
            var AuraOfSelfRighteousness = Resources.GetModBlueprint<BlueprintFeature>("AuraOfSelfRighteousness");
            var OathbreakerStalwart = Resources.GetModBlueprint<BlueprintFeature>("OathbreakerStalwart");
            var OathbreakersDirectionSwiftFeature = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersDirectionSwiftFeature");
            var OathbreakersBaneUse = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersBaneUse");
            var OathbreakerTeamworkFeat = Resources.GetModBlueprint<BlueprintFeature>("OathbreakerTeamworkFeat");
            var SpitefulTenacity = Resources.GetModBlueprint<BlueprintFeature>("SpitefulTenacity");
            var FadedGrace = Resources.GetModBlueprint<BlueprintFeature>("FadedGrace");
            var OathbreakerSoloTactics = Resources.GetModBlueprint<BlueprintFeature>("OathbreakerSoloTactics");
            var OathbreakersDirectionFeature = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersDirectionFeature");
            var OathbreakersBaneFeature = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersBaneFeature");
            var OathbreakerProgression = Helpers.CreateBlueprint<BlueprintProgression>("OathbreakerProgression", bp =>
            {
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[1];
                { OathbreakerClass.ToReference<BlueprintCharacterClassReference>(); };
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_UIDeterminatorsGroup = new BlueprintFeatureBaseReference[4]
                {
                    OathbreakerProficiencies.ToReference<BlueprintFeatureBaseReference>(),
                    OathbreakersBaneFeature.ToReference<BlueprintFeatureBaseReference>(),
                    OathbreakersDirectionFeature.ToReference<BlueprintFeatureBaseReference>(),
                    OathbreakerSoloTactics.ToReference<BlueprintFeatureBaseReference>() };
                bp.LevelEntries = new LevelEntry[]
                {
                    Helpers.CreateLevelEntry(1,OathbreakersBaneFeature, OathbreakersDirectionFeature),
                    Helpers.CreateLevelEntry(2, FadedGrace, OathbreakerSoloTactics),
                    Helpers.CreateLevelEntry(3, SpitefulTenacity, OathbreakerTeamworkFeat),
                    Helpers.CreateLevelEntry(4, OathbreakersBaneUse),
                    Helpers.CreateLevelEntry(5),
                    Helpers.CreateLevelEntry(6, OathbreakerTeamworkFeat),
                    Helpers.CreateLevelEntry(7, OathbreakersBaneUse),
                    Helpers.CreateLevelEntry(8),
                    Helpers.CreateLevelEntry(9, OathbreakerTeamworkFeat),
                    Helpers.CreateLevelEntry(10, OathbreakersBaneUse),
                    Helpers.CreateLevelEntry(11, OathbreakersDirectionSwiftFeature),
                    Helpers.CreateLevelEntry(12, OathbreakerTeamworkFeat),
                    Helpers.CreateLevelEntry(13, OathbreakersBaneUse),
                    Helpers.CreateLevelEntry(14, OathbreakerStalwart),
                    Helpers.CreateLevelEntry(15, OathbreakerTeamworkFeat),
                    Helpers.CreateLevelEntry(16, OathbreakersBaneUse),
                    Helpers.CreateLevelEntry(17, AuraOfSelfRighteousness),
                    Helpers.CreateLevelEntry(18, OathbreakerTeamworkFeat),
                    Helpers.CreateLevelEntry(19, OathbreakersBaneUse),
                    Helpers.CreateLevelEntry(20, BreakerOfOaths),

                };
                bp.UIGroups = new UIGroup[]
                {
                    Helpers.CreateUIGroup(
                        OathbreakersBaneFeature,
                        OathbreakersBaneUse,
                        OathbreakersBaneUse,
                        OathbreakersBaneUse,
                        OathbreakersBaneUse,
                        OathbreakersBaneUse,
                        OathbreakersBaneUse),
                    Helpers.CreateUIGroup(
                        OathbreakersDirectionFeature,
                        OathbreakersDirectionSwiftFeature),
                    Helpers.CreateUIGroup(
                        FadedGrace,
                        SpitefulTenacity,
                        OathbreakerStalwart,
                        AuraOfSelfRighteousness,
                        BreakerOfOaths),
                    Helpers.CreateUIGroup(
                        OathbreakerSoloTactics,
                        OathbreakerTeamworkFeat,
                        OathbreakerTeamworkFeat,
                        OathbreakerTeamworkFeat,
                        OathbreakerTeamworkFeat,
                        OathbreakerTeamworkFeat,
                        OathbreakerTeamworkFeat)
                };
            });
        }
    }
}
