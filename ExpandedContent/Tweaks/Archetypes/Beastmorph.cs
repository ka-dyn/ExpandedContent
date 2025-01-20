using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class Beastmorph {
        public static void AddBeastmorph() {

            var AlchemistClass = Resources.GetBlueprint<BlueprintCharacterClass>("0937bec61c0dabc468428f496580c721");
            var PoisonResistanceFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("c9022272c87bd66429176ce5c597989c");
            var PoisonImmunityFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("202af59b918143a4ab7c33d72c8eb6d5");
            var PersistentMutagenFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("75ba281feb2b96547a3bfb12ecaff052");



            var BeastmorphArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("BeastmorphArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"BeastmorphArchetype.Name", "Beastmorph");
                bp.LocalizedDescription = Helpers.CreateString($"BeastmorphArchetype.Description", "Beastmorphs study the anatomy of monsters, " +
                    "learning how they achieve their strange powers. They use their knowledge to duplicate these abilities, but at the cost of " +
                    "taking on inhuman shapes when they use mutagens.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"BeastmorphArchetype.Description", "Beastmorphs study the anatomy of monsters, " +
                    "learning how they achieve their strange powers. They use their knowledge to duplicate these abilities, but at the cost of " +
                    "taking on inhuman shapes when they use mutagens.");
            });








            BeastmorphArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(2, PoisonResistanceFeature),
                    Helpers.LevelEntry(5, PoisonResistanceFeature),
                    Helpers.LevelEntry(8, PoisonResistanceFeature),
                    Helpers.LevelEntry(10, PoisonImmunityFeature),
                    Helpers.LevelEntry(14, PersistentMutagenFeature)
            };
            BeastmorphArchetype.AddFeatures = new LevelEntry[] {
                    //Helpers.LevelEntry(1, BeastmorphSpontaneousCasting, BeastmorphBlessingSelection),
                    //Helpers.LevelEntry(7, BeastmorphFriendOfTheForestFeature )
            };

            if (ModSettings.AddedContent.Archetypes.IsDisabled("Beastmorph")) { return; }
            AlchemistClass.m_Archetypes = AlchemistClass.m_Archetypes.AppendToArray(BeastmorphArchetype.ToReference<BlueprintArchetypeReference>());
        }
    }
}
