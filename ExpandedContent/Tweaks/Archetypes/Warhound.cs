using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.Blueprints.Classes.Selection;
using ExpandedContent.Tweaks.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class Warhound {
        public static void AddWarhound() {

            var SlayerClass = Resources.GetBlueprint<BlueprintCharacterClass>("c75e0971973957d4dbad24bc7957e4fb");
            var SlayerStudyTargetFeature = Resources.GetBlueprint<BlueprintFeature>("09bdd9445ac38044389476689ae8d5a1");
            var SlayerTalentSelection2 = Resources.GetBlueprint<BlueprintFeatureSelection>("04430ad24988baa4daa0bcd4f1c7d118");
            var SlayerTalentSelection6 = Resources.GetBlueprint<BlueprintFeatureSelection>("43d1b15873e926848be2abf0ea3ad9a8");
            var SlayerTalentSelection10 = Resources.GetBlueprint<BlueprintFeatureSelection>("913b9cf25c9536949b43a2651b7ffb66");

            var AnimalCompanionSelectionDruid = Resources.GetBlueprint<BlueprintFeatureSelection>("571f8434d98560c43935e132df65fe76");
            var AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");
            var AnimalCompanionArchetypeSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("65af7290b4efd5f418132141aaa36c1b");
            var MountTargetFeature = Resources.GetBlueprint<BlueprintFeature>("cb06f0e72ffb5c640a156bd9f8000c1d");



            var ConfusionSpell = Resources.GetBlueprint<BlueprintAbility>("cf6c901fb7acc904e85c63b342e9c949");
            var FeeblemindSpell = Resources.GetBlueprint<BlueprintAbility>("444eed6e26f773a40ab6e4d160c67faa");
            var DazeSpell = Resources.GetBlueprint<BlueprintAbility>("55f14bc84d7c85446b07a1b5dd6b2b4c");
            var InsanitySpell = Resources.GetBlueprint<BlueprintAbility>("2b044152b3620c841badb090e01ed9de");

            var WarhoundArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("WarhoundArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"WarhoundArchetype.Name", "Warhound");
                bp.LocalizedDescription = Helpers.CreateString($"WarhoundArchetype.Description", "A warhound is a humanoid beast, " +
                    "a trained stalker who hunts and kills his master’s enemies with the aid of an animal companion.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"WarhoundArchetype.Description", "A warhound is a humanoid beast, " +
                    "a trained stalker who hunts and kills his master’s enemies with the aid of an animal companion.");
            });

            var SenseVitalsIcon = Resources.GetBlueprint<BlueprintAbility>("82962a820ebc0e7408b8582fdc3f4c0c").Icon;
            var WarhoundsEyeFeature = Helpers.CreateBlueprint<BlueprintFeature>("WarhoundsEyeFeature", bp => {
                bp.SetName("Warhounds Eye");
                bp.SetDescription("At 1st level, a warhounds training helps them spot and track targets. They gain a +2 bonus to their Perception " +
                    "and {g|Encyclopedia:Lore_Nature}Lore (nature){/g} checks. " +
                    "\nAdditionally, the warhound does not get the studied target feature at 1st level, instead gaining it at 5th level with the " +
                    "bonus granted being 1 less than the standard slayer progression.");
                bp.m_Icon = SenseVitalsIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillPerception;
                    c.Value = 2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillLoreNature;
                    c.Value = 2;
                });
            });

            var WarhoundAnimalCompanionProgression = Helpers.CreateBlueprint<BlueprintProgression>("WarhoundAnimalCompanionProgression", bp => {
                bp.SetName("Warhound Animal Companion Progression");
                bp.SetName("");
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.GiveFeaturesForPreviousLevels = false;
                bp.ReapplyOnLevelUp = false;
                bp.m_FeaturesRankIncrease = new List<BlueprintFeatureReference>();
                bp.LevelEntries = Enumerable.Range(4, 20)
                    .Select(i => new LevelEntry {
                        Level = i,
                        m_Features = new List<BlueprintFeatureBaseReference> {
                            AnimalCompanionRank.ToReference<BlueprintFeatureBaseReference>()
                        },
                    })
                    .ToArray();
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = SlayerClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel { 
                        m_Archetype = WarhoundArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.UIGroups = new UIGroup[0];
            });

            var WarhoundAnimalCompanionSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("WarhoundAnimalCompanionSelection", bp => {
                bp.SetName("Warhound Animal Companion");
                bp.SetDescription("The warhound has finished training their animal companion to aid in their hunt. This functions like the druid animal " +
                    "companion ability, except that the warhound’s effective druid level is equal to his warhound level –3. " +
                    "\nUnlike normal animals of its kind, an animal companion's {g|Encyclopedia:Hit_Dice}Hit Dice{/g}, abilities, {g|Encyclopedia:Skills}skills{/g}, and " +
                    "{g|Encyclopedia:Feat}feats{/g} advance as the druid advances in level. If a character receives an animal companion from more than one source, her effective " +
                    "druid levels stack for the purposes of determining the statistics and abilities of the companion. Most animal companions increase in {g|Encyclopedia:Size}size{/g} " +
                    "when their druid reaches 4th or 7th level, depending on the companion.");
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = WarhoundAnimalCompanionProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = AnimalCompanionArchetypeSelection.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = MountTargetFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.AnimalCompanion;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddFeatures(AnimalCompanionSelectionDruid.m_AllFeatures);
            });


            

            WarhoundArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, SlayerStudyTargetFeature),
                    Helpers.LevelEntry(2, SlayerTalentSelection2),
                    Helpers.LevelEntry(8, SlayerTalentSelection6),
                    Helpers.LevelEntry(14, SlayerTalentSelection10),
            };
            WarhoundArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, WarhoundsEyeFeature),
                    Helpers.LevelEntry(4, WarhoundAnimalCompanionSelection)
                    //Helpers.LevelEntry(14, WarhoundSwiftAidHuntFeature) Added on AidAnother.cs
            };
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Warhound")) { return; }
            SlayerClass.m_Archetypes = SlayerClass.m_Archetypes.AppendToArray(WarhoundArchetype.ToReference<BlueprintArchetypeReference>());

        }
    }
}
