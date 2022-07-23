using ExpandedContent.Extensions;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
