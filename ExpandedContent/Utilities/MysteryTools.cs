using ExpandedContent.Extensions;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;

namespace ExpandedContent.Utilities {
    internal class MysteryTools {
        //Base Mystery
        public static void RegisterMystery(BlueprintFeature mystery) {
            BlueprintFeatureSelection OracleMysterySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("5531b975dcdf0e24c98f1ff7e017e741");
            OracleMysterySelection.m_AllFeatures = OracleMysterySelection.m_AllFeatures.AddToArray(mystery.ToReference<BlueprintFeatureReference>());
        }
        public static void RegisterSecondMystery(BlueprintFeature secondmystery) {
            BlueprintFeatureSelection SecondMystery= Resources.GetBlueprint<BlueprintFeatureSelection>("277b0164740b97945a3f8022bd572f48");
            SecondMystery.m_AllFeatures = SecondMystery.m_AllFeatures.AddToArray(secondmystery.ToReference<BlueprintFeatureReference>());
        }
        //Divine Herbalist Mystery
        public static void RegisterHerbalistMystery(BlueprintFeature mystery) {
            BlueprintFeatureSelection DivineHerbalistMysterySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("c11ff5dbd8518c941849b3112d4d6b68");
            DivineHerbalistMysterySelection.m_AllFeatures = DivineHerbalistMysterySelection.m_AllFeatures.AddToArray(mystery.ToReference<BlueprintFeatureReference>());
        }
        public static void RegisterSecondHerbalistMystery(BlueprintFeature secondmystery) {
            BlueprintFeatureSelection SecondMysteryHerbalist = Resources.GetBlueprint<BlueprintFeatureSelection>("cff5c53fe99c48bf863a0005d768f75a");
            SecondMysteryHerbalist.m_AllFeatures = SecondMysteryHerbalist.m_AllFeatures.AddToArray(secondmystery.ToReference<BlueprintFeatureReference>());
        }
        //Enlightend Philosopher Mystery
        public static void RegisterEnlightendPhilosopherMystery(BlueprintFeature mystery) {
            BlueprintFeatureSelection EnlightendPhilosopherMysterySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("9d5fdd3b4a6cd4f40beddbc72b2c07a0");
            EnlightendPhilosopherMysterySelection.m_AllFeatures = EnlightendPhilosopherMysterySelection.m_AllFeatures.AddToArray(mystery.ToReference<BlueprintFeatureReference>());
        }
        public static void RegisterSecondEnlightendPhilosopherMystery(BlueprintFeature secondmystery) {
            BlueprintFeatureSelection SecondMysteryEnlightendPhilosopher = Resources.GetBlueprint<BlueprintFeatureSelection>("4ff6f2905e1f4d1b92930b87f85bf86c");
            SecondMysteryEnlightendPhilosopher.m_AllFeatures = SecondMysteryEnlightendPhilosopher.m_AllFeatures.AddToArray(secondmystery.ToReference<BlueprintFeatureReference>());
        }
        //Lone Strider / Hermit Mystery
        public static void RegisterHermitMystery(BlueprintFeature mystery) {
            BlueprintFeatureSelection HermitMysterySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("bc86ea452bef22a45989c6fa644bd2eb");
            HermitMysterySelection.m_AllFeatures = HermitMysterySelection.m_AllFeatures.AddToArray(mystery.ToReference<BlueprintFeatureReference>());
        }
        public static void RegisterSecondHermitMystery(BlueprintFeature secondmystery) {
            BlueprintFeatureSelection SecondMysteryHermit = Resources.GetBlueprint<BlueprintFeatureSelection>("324476155d5c4968babfecda37bcc87c");
            SecondMysteryHermit.m_AllFeatures = SecondMysteryHermit.m_AllFeatures.AddToArray(secondmystery.ToReference<BlueprintFeatureReference>());
        }
        //Ocean's Echo Mystery
        public static void RegisterOceansEchoMystery(BlueprintFeature mystery) {
            BlueprintFeatureSelection OceansEchoMysterySelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("OceansEchoMysterySelection");
            OceansEchoMysterySelection.m_AllFeatures = OceansEchoMysterySelection.m_AllFeatures.AddToArray(mystery.ToReference<BlueprintFeatureReference>());
        }
        public static void RegisterSecondOceansEchoMystery(BlueprintFeature secondmystery) {
            BlueprintFeatureSelection SecondMysteryOceansEcho = Resources.GetModBlueprint<BlueprintFeatureSelection>("SecondMysteryOceansEcho");
            SecondMysteryOceansEcho.m_AllFeatures = SecondMysteryOceansEcho.m_AllFeatures.AddToArray(secondmystery.ToReference<BlueprintFeatureReference>());
        }
        public static void RegisterMysteryGiftSelection(BlueprintFeature mystery) {
            BlueprintFeatureSelection MysteryGiftSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("e9629fc4de4c4b3ea1c26e9c13c2402d");
            MysteryGiftSelection.m_AllFeatures = MysteryGiftSelection.m_AllFeatures.AddToArray(mystery.ToReference<BlueprintFeatureReference>());
            MysteryGiftSelection.m_Features = MysteryGiftSelection.m_Features.AddToArray(mystery.ToReference<BlueprintFeatureReference>());
        }
        //Ravener Hunter lazy prereq thing
        public static void ConfigureRavenerHunterRevelation(BlueprintFeature revelation, BlueprintProgression mystery) {
            revelation.TemporaryContext(bp => {
                bp.GetComponent<PrerequisiteFeaturesFromList>().TemporaryContext(c => {
                    c.m_Features = c.m_Features.AppendToArray(mystery.ToReference<BlueprintFeatureReference>());
                });
            });
        }
        public static void ConfigureRavenerHunterRevelation(BlueprintProgression revelation, BlueprintProgression mystery) {
            revelation.TemporaryContext(bp => {
                bp.GetComponent<PrerequisiteFeaturesFromList>().TemporaryContext(c => {
                    c.m_Features = c.m_Features.AppendToArray(mystery.ToReference<BlueprintFeatureReference>());
                });
            });
        }
        public static void ConfigureRavenerHunterRevelation(BlueprintFeatureSelection revelation, BlueprintProgression mystery) {
            revelation.TemporaryContext(bp => {
                bp.GetComponent<PrerequisiteFeaturesFromList>().TemporaryContext(c => {
                    c.m_Features = c.m_Features.AppendToArray(mystery.ToReference<BlueprintFeatureReference>());
                });
            });
        }
        public static void ConfigureRavenerHunterRevelation(BlueprintFeature revelation, BlueprintProgression mystery, int level) {
            BlueprintCharacterClass InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            BlueprintArchetype RavenerHunterArchetype = Resources.GetModBlueprint<BlueprintArchetype>("RavenerHunterArchetype");
            revelation.TemporaryContext(bp => {
                bp.GetComponent<PrerequisiteFeaturesFromList>().TemporaryContext(c => {
                    c.m_Features = c.m_Features.AppendToArray(mystery.ToReference<BlueprintFeatureReference>());
                });
                bp.GetComponent<PrerequisiteClassLevel>().TemporaryContext(c => {
                    c.Group = Prerequisite.GroupType.Any;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = level;
                });
            });
        }
        public static void ConfigureRavenerHunterRevelation(BlueprintProgression revelation, BlueprintProgression mystery, int level) {
            BlueprintCharacterClass InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            BlueprintArchetype RavenerHunterArchetype = Resources.GetModBlueprint<BlueprintArchetype>("RavenerHunterArchetype");
            revelation.TemporaryContext(bp => {
                bp.GetComponent<PrerequisiteFeaturesFromList>().TemporaryContext(c => {
                    c.m_Features = c.m_Features.AppendToArray(mystery.ToReference<BlueprintFeatureReference>());
                });
                bp.GetComponent<PrerequisiteClassLevel>().TemporaryContext(c => {
                    c.Group = Prerequisite.GroupType.Any;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = level;
                });
            });
        }
    }
}
