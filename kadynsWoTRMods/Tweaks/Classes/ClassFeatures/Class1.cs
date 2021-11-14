using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kadynsWOTRMods.Tweaks.Classes.ClassFeatures {
    internal class Class1 {



        public static void Progression2() {


            var OathbreakerClass = Resources.GetModBlueprint<BlueprintFeature>("OathbreakerClass");




            var PaladinClassProficiences = Resources.GetBlueprint<BlueprintFeature>("b10ff88c03308b649b50c31611c2fefb");
            var OathbreakerProficiencies = Helpers.CreateBlueprint<BlueprintFeature>("OathbreakerProficiencies", bp => {
                bp.SetName("Oathbreaker Proficiences");
                bp.SetDescription("Oathbreakers are proficient with all simple and {g|Encyclopedia:Weapon_Proficiency}martial weapons{/g}, with all types of armor " +
                    "(heavy, medium, and light), and with shields (except tower shields).");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PaladinClassProficiences.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });

            var OathbreakerSoloTactics = Resources.GetModBlueprint<BlueprintFeature>("OathbreakerSoloTactics");
            var OathbreakersDirectionFeature = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersDirectionFeature");
            var BreakerOfOaths = Resources.GetModBlueprint<BlueprintFeature>("BreakerOfOaths");
            var AuraOfSelfRighteousness = Resources.GetModBlueprint<BlueprintFeature>("AuraOfSelfRighteousness");
            var OathbreakerStalwart = Resources.GetModBlueprint<BlueprintFeature>("OathbreakerStalwart");
            var OathbreakersDirectionSwiftFeature = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersDirectionSwiftFeature");
            var OathbreakersBaneUse = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersBaneUse");
            var OathbreakerTeamworkFeat = Resources.GetModBlueprint<BlueprintFeature>("OathbreakerTeamworkFeat");
            var SpitefulTenacity = Resources.GetModBlueprint<BlueprintFeature>("SpitefulTenacity");
            var FadedGrace = Resources.GetModBlueprint<BlueprintFeature>("FadedGrace");
            var OathbreakersBaneFeature = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersBaneFeature");
            var OathbreakerProgression = Helpers.CreateBlueprint<BlueprintProgression>("OathbreakerProgression");

            BlueprintProgression.ClassWithLevel[] classWithLevelArray = new BlueprintProgression.ClassWithLevel[((IEnumerable<BlueprintCharacterClass>)Classes.Oathbreaker.getOathbreakerArray()).Count<BlueprintCharacterClass>()];
            for (int index = 0; index < classWithLevelArray.Length; ++index)
                classWithLevelArray[index] = Helpers.ClassToClassWithLevel(Classes.Oathbreaker.getOathbreakerArray()[index]);
            OathbreakerProgression.Ranks = 1;
            OathbreakerProgression.IsClassFeature = true;
            OathbreakerProgression.m_Classes = classWithLevelArray;

            OathbreakerProgression.LevelEntries = new LevelEntry[20]
            {
                    Helpers.CreateLevelEntry(1,(BlueprintFeatureBase) OathbreakersBaneFeature, (BlueprintFeatureBase) OathbreakersDirectionFeature),
                    Helpers.CreateLevelEntry(2, (BlueprintFeatureBase) FadedGrace, (BlueprintFeatureBase) OathbreakerSoloTactics),
                    Helpers.CreateLevelEntry(3, (BlueprintFeatureBase) SpitefulTenacity, (BlueprintFeatureBase) OathbreakerTeamworkFeat),
                    Helpers.CreateLevelEntry(4, (BlueprintFeatureBase) OathbreakersBaneUse),
                    Helpers.CreateLevelEntry(5),
                    Helpers.CreateLevelEntry(6, (BlueprintFeatureBase) OathbreakerTeamworkFeat),
                    Helpers.CreateLevelEntry(7, (BlueprintFeatureBase) OathbreakersBaneUse),
                    Helpers.CreateLevelEntry(8),
                    Helpers.CreateLevelEntry(9, (BlueprintFeatureBase) OathbreakerTeamworkFeat),
                    Helpers.CreateLevelEntry(10, (BlueprintFeatureBase) OathbreakersBaneUse),
                    Helpers.CreateLevelEntry(11, (BlueprintFeatureBase) OathbreakersDirectionSwiftFeature),
                    Helpers.CreateLevelEntry(12, (BlueprintFeatureBase) OathbreakerTeamworkFeat),
                    Helpers.CreateLevelEntry(13, (BlueprintFeatureBase) OathbreakersBaneUse),
                    Helpers.CreateLevelEntry(14, (BlueprintFeatureBase) OathbreakerStalwart),
                    Helpers.CreateLevelEntry(15, (BlueprintFeatureBase) OathbreakerTeamworkFeat),
                    Helpers.CreateLevelEntry(16, (BlueprintFeatureBase) OathbreakersBaneUse),
                    Helpers.CreateLevelEntry(17, (BlueprintFeatureBase) AuraOfSelfRighteousness),
                    Helpers.CreateLevelEntry(18, (BlueprintFeatureBase) OathbreakerTeamworkFeat),
                    Helpers.CreateLevelEntry(19, (BlueprintFeatureBase) OathbreakersBaneUse),
                    Helpers.CreateLevelEntry(20, (BlueprintFeatureBase) BreakerOfOaths)

            };
            OathbreakerProgression.m_UIDeterminatorsGroup = new BlueprintFeatureBaseReference[4]
            {
                    OathbreakerProficiencies.ToReference<BlueprintFeatureBaseReference>(),
                    OathbreakersBaneFeature.ToReference<BlueprintFeatureBaseReference>(),
                    OathbreakersDirectionFeature.ToReference<BlueprintFeatureBaseReference>(),
                    OathbreakerSoloTactics.ToReference<BlueprintFeatureBaseReference>() };
            OathbreakerProgression.UIGroups = new UIGroup[4]
             {
                    Helpers.CreateUIGroup(
                       (BlueprintFeatureBase) OathbreakersBaneFeature,
                      (BlueprintFeatureBase)  OathbreakersBaneUse,
                      (BlueprintFeatureBase)  OathbreakersBaneUse,
                      (BlueprintFeatureBase)  OathbreakersBaneUse,
                      (BlueprintFeatureBase)  OathbreakersBaneUse,
                      (BlueprintFeatureBase)  OathbreakersBaneUse,
                     (BlueprintFeatureBase)  OathbreakersBaneUse),
                    Helpers.CreateUIGroup(
                    (BlueprintFeatureBase)  OathbreakersDirectionFeature,
                     (BlueprintFeatureBase) OathbreakersDirectionSwiftFeature),
                    Helpers.CreateUIGroup(
                       (BlueprintFeatureBase) FadedGrace,
                       (BlueprintFeatureBase) SpitefulTenacity,
                       (BlueprintFeatureBase) OathbreakerStalwart,
                       (BlueprintFeatureBase) AuraOfSelfRighteousness,
                      (BlueprintFeatureBase)  BreakerOfOaths),
                    Helpers.CreateUIGroup(
                      (BlueprintFeatureBase) OathbreakerSoloTactics,
                       (BlueprintFeatureBase) OathbreakerTeamworkFeat,
                       (BlueprintFeatureBase) OathbreakerTeamworkFeat,
                       (BlueprintFeatureBase) OathbreakerTeamworkFeat,
                       (BlueprintFeatureBase) OathbreakerTeamworkFeat,
                       (BlueprintFeatureBase) OathbreakerTeamworkFeat,
                       (BlueprintFeatureBase) OathbreakerTeamworkFeat)
             };
        }



    }
}
        }
    }
}
