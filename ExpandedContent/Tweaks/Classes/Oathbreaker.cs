using HarmonyLib;
using BlueprintCore.Blueprints;
using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Items.Equipment;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints.Root;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Blueprints.Classes.Selection;

namespace ExpandedContent.Tweaks.Classes {

    [HarmonyPatch(typeof(BlueprintsCache), "Init")]
    public class OathbreakerClassAdder {
        private static bool Initialized;
        public static BlueprintProgression OathbreakerProgression;
        public static BlueprintAbilityAreaEffect AuraOfSelfRighteousnessArea;
        public static BlueprintFeature BreakerOfOaths;
        public static BlueprintFeature AuraOfSelfRighteousnessFeature;
        public static BlueprintFeature OathbreakerStalwart;
        public static BlueprintFeature OathbreakersDirectionSwiftFeature;
        public static BlueprintFeature OathbreakersBaneUse;
        public static BlueprintFeatureSelection OathbreakerTeamworkFeat;
        public static BlueprintFeature SpitefulTenacity;
        public static BlueprintFeature FadedGrace;
        public static BlueprintFeature OathbreakerSoloTactics;
        public static BlueprintFeature OathbreakersBaneFeature;
        public static BlueprintFeature OathbreakersDirectionFeature;
        public static BlueprintCharacterClass OathbreakerClass;
        public static BlueprintFeature OathbreakerProficiencies;
        
        

        [HarmonyPriority(Priority.First)]
        public static void Postfix() {
            if (OathbreakerClassAdder.Initialized) return;
            OathbreakerClassAdder.Initialized = true;

            


            Classes.ClassFeaturesOathbreaker.OathbreakersBaneFeature.AddOathbreakersBaneFeature();
            Classes.ClassFeaturesOathbreaker.OathbreakerDefensiveStance.AddDefensiveStance();
            Classes.ClassFeaturesOathbreaker.OathbreakersDirection.AddOathbreakersDirection();
            Classes.ClassFeaturesOathbreaker.OathbreakerSoloTactics.AddOathbreakerSoloTactics();
            Classes.ClassFeaturesOathbreaker.OathbreakerStalwart.AddStalwartFeature();
            Classes.ClassFeaturesOathbreaker.SpitefulTenacity.AddSpitefulTenacity();
            Classes.ClassFeaturesOathbreaker.AuraOfSelfRighteousness.AddAuraOfSelfRighteousnessFeature();
            Classes.ClassFeaturesOathbreaker.FadedGrace.AddFadedGrace();
            Classes.ClassFeaturesOathbreaker.BreakerOfOaths.AddBreakerOfOaths();
            OathbreakerClassAdder.AddOathbreakerProgression();

            OathbreakerClassAdder.AddOathbreakerClass();
            





        }

        public static void AddOathbreakerClass() {
            if (ModSettings.AddedContent.Classes.IsDisabled("Oathbreaker")) { return; }
            var BOOIcon = AssetLoader.LoadInternal("Skills", "Icon_BOO.png");
            var OathbreakerSoloTactics = Resources.GetModBlueprint<BlueprintFeature>("OathbreakerSoloTactics");
            var OathbreakersDirectionFeature = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersDirectionFeature");
            var OathbreakersBaneFeature = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersBaneFeature");
            var OathbreakerProgression = Resources.GetModBlueprint<BlueprintProgression>("OathbreakerProgression");
            var AnimalClass = Resources.GetBlueprint<BlueprintCharacterClass>("4cd1757a0eea7694ba5c933729a53920");
            var DreadfulCalm = Resources.GetModBlueprint<BlueprintFeature>("DreadfulCalm");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var OathbreakerClass = Helpers.CreateBlueprint<BlueprintCharacterClass>("OathbreakerClass", bp => {
                bp.LocalizedName = Helpers.CreateString($"OathbreakerClass.Name", "Oathbreaker");
                bp.LocalizedDescription = Helpers.CreateString($"OathbreakerClass.Description", "While paladins often " +
                    "collaborate with less righteous adventurers in order to further their causes, those who spend too much time around " +
                    "companions with particularly loose morals run the risk of adopting those same unscrupulous ideologies and methods. Such an " +
                    "Oathbreaker, as these fallen paladins are known, strikes out for retribution and revenge, far more interested in " +
                    "tearing down those who have harmed her or her companions than furthering a distant deity’s cause.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"OathbreakerClass.Description", "While paladins often " +
                    "collaborate with less righteous adventurers in order to further their causes, those who spend too much time around " +
                    "companions with particularly loose morals run the risk of adopting those same unscrupulous ideologies and methods. Such an " +
                    "Oathbreaker, as these fallen paladins are known, strikes out for retribution and revenge, far more interested in " +
                    "tearing down those who have harmed her or her companions than furthering a distant deity’s cause.");
                bp.HitDie = Kingmaker.RuleSystem.DiceType.D10;
                bp.m_BaseAttackBonus = PaladinClass.m_BaseAttackBonus;
                bp.m_FortitudeSave = PaladinClass.m_FortitudeSave;
                bp.m_PrototypeId = PaladinClass.m_PrototypeId;
                bp.m_ReflexSave = PaladinClass.m_ReflexSave;
                bp.m_WillSave = PaladinClass.m_WillSave;
                bp.m_Progression = OathbreakerProgression.ToReference<BlueprintProgressionReference>();
                bp.m_Difficulty = PaladinClass.Difficulty;
                bp.RecommendedAttributes = PaladinClass.RecommendedAttributes;
                bp.NotRecommendedAttributes = PaladinClass.NotRecommendedAttributes;
                bp.m_Spellbook = null;
                bp.m_EquipmentEntities = PaladinClass.m_EquipmentEntities;
                bp.m_StartingItems = PaladinClass.StartingItems;
                bp.m_SignatureAbilities = new BlueprintFeatureReference[4]
                {
                    OathbreakersBaneFeature.ToReference<BlueprintFeatureReference>(),
                    OathbreakerSoloTactics.ToReference<BlueprintFeatureReference>(),
                    OathbreakersDirectionFeature.ToReference<BlueprintFeatureReference>(),
                    DreadfulCalm.ToReference<BlueprintFeatureReference>(),

                };
                bp.m_Difficulty = PaladinClass.Difficulty;
                bp.m_DefaultBuild = null;
                bp.m_Archetypes = new BlueprintArchetypeReference[0];
                bp.SkillPoints = 3;
                bp.ClassSkills = new StatType[4] {

                StatType.SkillKnowledgeWorld, StatType.SkillLoreNature, StatType.SkillKnowledgeArcana, StatType.SkillLoreReligion
                };
                bp.IsDivineCaster = true;
                bp.IsArcaneCaster = false;
                bp.StartingGold = 411;
                bp.PrimaryColor = 6;
                bp.SecondaryColor = 11;
                bp.MaleEquipmentEntities = PaladinClass.MaleEquipmentEntities;
                bp.FemaleEquipmentEntities = PaladinClass.FemaleEquipmentEntities;
                bp.RecommendedAttributes = PaladinClass.RecommendedAttributes;
                bp.AddComponent<PrerequisiteNoClassLevel>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_CharacterClass = AnimalClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<PrerequisiteIsPet>(c => {
                    c.Not = true;
                    c.HideInUI = true;
                });
            });
            Helpers.RegisterClass(OathbreakerClass);
            OathbreakerProgression.m_Classes = new BlueprintProgression.ClassWithLevel[] { new BlueprintProgression.ClassWithLevel { m_Class = OathbreakerClass.ToReference<BlueprintCharacterClassReference>() } };
        }

        public static void AddOathbreakerProgression() {


            var OBProf = AssetLoader.LoadInternal("Skills", "Icon_OBProf.png");
            var PaladinClassProficiencies = Resources.GetBlueprint<BlueprintFeature>("b10ff88c03308b649b50c31611c2fefb");
            var OathbreakerProficiencies = Helpers.CreateBlueprint<BlueprintFeature>("OathbreakerProficiencies", bp => {
                bp.SetName("Oathbreaker Proficiences");
                bp.SetDescription("Oathbreakers are proficient with all simple and {g|Encyclopedia:Weapon_Proficiency}martial weapons{/g}, with all types of armor " +
                    "(heavy, medium, and light), and with shields (except tower shields).");
                bp.m_Icon = OBProf;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { PaladinClassProficiencies.ToReference<BlueprintUnitFactReference>() };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });



            var OathbreakersDirectionIncrease = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersDirectionIncrease");
            var OathbreakerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("OathbreakerClass");
            var BreakerOfOaths = Resources.GetModBlueprint<BlueprintFeature>("BreakerOfOaths");
            var AuraOfSelfRighteousnessFeature = Resources.GetModBlueprint<BlueprintFeature>("AuraOfSelfRighteousnessFeature");
            var OathbreakerStalwart = Resources.GetModBlueprint<BlueprintFeature>("OathbreakerStalwart");
            var OathbreakersDirectionSwiftFeature = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersDirectionSwiftFeature");
            var OathbreakersBaneUse = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersBaneUse");
            var OathbreakerTeamworkFeat = Resources.GetModBlueprint<BlueprintFeatureSelection>("OathbreakerTeamworkFeat");
            var SpitefulTenacity = Resources.GetModBlueprint<BlueprintFeature>("SpitefulTenacity");
            var FadedGrace = Resources.GetModBlueprint<BlueprintFeature>("FadedGrace");
            var OathbreakerSoloTactics = Resources.GetModBlueprint<BlueprintFeature>("OathbreakerSoloTactics");
            var OathbreakersBaneFeature = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersBaneFeature");
            var OathbreakersDirectionFeature = Resources.GetModBlueprint<BlueprintFeature>("OathbreakersDirectionFeature");
            var DreadfulCalm = Resources.GetModBlueprint<BlueprintFeature>("DreadfulCalm");
            var DefensivePowers = AssetLoader.LoadInternal("Skills", "Icon_DefensivePowers.png");
            var StalwartDefenderDefensivePowerSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("2cd91c501bda80b47ac2df0d51b02973");
            StalwartDefenderDefensivePowerSelection.m_Icon = DefensivePowers;
            var FeatSelectionIcon = AssetLoader.LoadInternal("Skills", "Icon_FeatSelection.png");
            var FeatSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("303fd456ddb14437946e344bad9a893b");
            FeatSelection.m_Icon = FeatSelectionIcon;
            FeatSelection.SetDescription("At 3rd level and every 3 levels thereafter, this class gains a bonus {g|Encyclopedia:Feat}feat{/g} in addition to " +
                "those gained from normal advancement. These bonus feats must be selected from those listed as Combat Feats, sometimes also called \"fighter bonus feats.\"");
            var OathbreakerProgression = Helpers.CreateBlueprint<BlueprintProgression>("OathbreakerProgression");
            OathbreakerProgression.SetName("Oathbreaker");
            OathbreakerProgression.SetDescription("While paladins often " +
                    "collaborate with less righteous adventurers in order to further their causes, those who spend too much time around " +
                    "companions with particularly loose morals run the risk of adopting those same unscrupulous ideologies and methods. Such an " +
                    "Oathbreaker, as these fallen paladins are known, strikes out for retribution and revenge, far more interested in " +
                    "tearing down those who have harmed her or her companions than furthering a distant deity’s cause.");

            
            
            OathbreakerProgression.LevelEntries = new LevelEntry[20]
            
            {
        Helpers.LevelEntry(1, (BlueprintFeatureBase) OathbreakersBaneFeature, (BlueprintFeatureBase) OathbreakersDirectionFeature, (BlueprintFeatureBase) OathbreakerProficiencies),
        Helpers.LevelEntry(2, (BlueprintFeatureBase) FadedGrace, (BlueprintFeatureBase) OathbreakerSoloTactics),
        Helpers.LevelEntry(3, (BlueprintFeatureBase) SpitefulTenacity, (BlueprintFeatureBase) OathbreakerTeamworkFeat, (BlueprintFeatureBase) FeatSelection),
        Helpers.LevelEntry(4, (BlueprintFeatureBase) OathbreakersBaneUse, (BlueprintFeatureBase) DreadfulCalm),
        Helpers.LevelEntry(5, (BlueprintFeatureBase) OathbreakersDirectionIncrease),
        Helpers.LevelEntry(6, (BlueprintFeatureBase) StalwartDefenderDefensivePowerSelection, (BlueprintFeatureBase) FeatSelection),
        Helpers.LevelEntry(7, (BlueprintFeatureBase) OathbreakersBaneUse),
        Helpers.LevelEntry(8, (BlueprintFeatureBase) StalwartDefenderDefensivePowerSelection),
        Helpers.LevelEntry(9, (BlueprintFeatureBase) OathbreakerTeamworkFeat, (BlueprintFeatureBase) FeatSelection),
        Helpers.LevelEntry(10, (BlueprintFeatureBase) OathbreakersBaneUse, (BlueprintFeatureBase) OathbreakersDirectionIncrease, (BlueprintFeatureBase) StalwartDefenderDefensivePowerSelection),
        Helpers.LevelEntry(11, (BlueprintFeatureBase) OathbreakersDirectionSwiftFeature),
        Helpers.LevelEntry(12, (BlueprintFeatureBase) StalwartDefenderDefensivePowerSelection, (BlueprintFeatureBase) FeatSelection),
        Helpers.LevelEntry(13, (BlueprintFeatureBase) OathbreakersBaneUse),
        Helpers.LevelEntry(14, (BlueprintFeatureBase) OathbreakerStalwart, (BlueprintFeatureBase) StalwartDefenderDefensivePowerSelection),
        Helpers.LevelEntry(15, (BlueprintFeatureBase) OathbreakerTeamworkFeat, (BlueprintFeatureBase) OathbreakersDirectionIncrease, (BlueprintFeatureBase) FeatSelection),
        Helpers.LevelEntry(16, (BlueprintFeatureBase) OathbreakersBaneUse, StalwartDefenderDefensivePowerSelection),
        Helpers.LevelEntry(17, (BlueprintFeatureBase) AuraOfSelfRighteousnessFeature),
        Helpers.LevelEntry(18, (BlueprintFeatureBase) FeatSelection),
        Helpers.LevelEntry(19, (BlueprintFeatureBase) OathbreakersBaneUse),
        Helpers.LevelEntry(20, (BlueprintFeatureBase) BreakerOfOaths, (BlueprintFeatureBase) OathbreakersDirectionIncrease)
            };

            OathbreakerProgression.UIGroups = new UIGroup[]
            {
                 Helpers.CreateUIGroup(OathbreakersBaneFeature, OathbreakersBaneUse),
                 Helpers.CreateUIGroup(OathbreakersDirectionFeature, OathbreakersDirectionIncrease, OathbreakersDirectionSwiftFeature),
                 Helpers.CreateUIGroup(FadedGrace, SpitefulTenacity, OathbreakerStalwart, AuraOfSelfRighteousnessFeature, BreakerOfOaths),
                 Helpers.CreateUIGroup(OathbreakerSoloTactics, OathbreakerTeamworkFeat),
                 Helpers.CreateUIGroup(DreadfulCalm, StalwartDefenderDefensivePowerSelection),
                 Helpers.CreateUIGroup(FeatSelection)
             };
        }
    }
}


























