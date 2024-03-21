using Kingmaker.Blueprints.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExpandedContent.Utilities {
    public static class MountTools {

        public static class RaceOptions {
            public static BlueprintRace Aasimar => Resources.GetBlueprint<BlueprintRace>("b7f02ba92b363064fb873963bec275ee");
            public static BlueprintRace Dhampir => Resources.GetBlueprint<BlueprintRace>("64e8b7d5f1ae91d45bbf1e56a3fdff01");
            public static BlueprintRace Dwarf => Resources.GetBlueprint<BlueprintRace>("c4faf439f0e70bd40b5e36ee80d06be7");
            public static BlueprintRace Elf => Resources.GetBlueprint<BlueprintRace>("25a5878d125338244896ebd3238226c8");
            public static BlueprintRace Gnome => Resources.GetBlueprint<BlueprintRace>("ef35a22c9a27da345a4528f0d5889157");
            public static BlueprintRace HalfElf => Resources.GetBlueprint<BlueprintRace>("b3646842ffbd01643ab4dac7479b20b0");
            public static BlueprintRace Halfling => Resources.GetBlueprint<BlueprintRace>("b0c3ef2729c498f47970bb50fa1acd30");
            public static BlueprintRace HalfOrc => Resources.GetBlueprint<BlueprintRace>("1dc20e195581a804890ddc74218bfd8e");
            public static BlueprintRace Human => Resources.GetBlueprint<BlueprintRace>("0a5d473ead98b0646b94495af250fdc4");
            public static BlueprintRace Kitsune => Resources.GetBlueprint<BlueprintRace>("fd188bb7bb0002e49863aec93bfb9d99");
            public static BlueprintRace Oread => Resources.GetBlueprint<BlueprintRace>("4d4555326b9b7144f93be1ea61337cd7");
            public static BlueprintRace Tiefling => Resources.GetBlueprint<BlueprintRace>("5c4e42124dc2b4647af6e36cf2590500");
            //Weird options
            public static BlueprintRace Android => Resources.GetBlueprint<BlueprintRace>("d1d114f539b74468b157ac69c275f266");
            public static BlueprintRace AscendingSuccubus => Resources.GetBlueprint<BlueprintRace>("5e464d1d5fd0e7a4380b6ce60ef2c83b");

        }

        public static Transform CreateMountBone(Transform parent, string type, Vector3 posOffset, Vector3? rotOffset = null) {
            var offsetBone = new GameObject($"Saddle_{type}_parent");
            offsetBone.transform.SetParent(parent);
            offsetBone.transform.localPosition = posOffset;
            if (rotOffset.HasValue)
                offsetBone.transform.localEulerAngles = rotOffset.Value;

            var target = new GameObject($"Saddle_{type}");
            target.transform.SetParent(offsetBone.transform);

            return target.transform;
        }


    }
}
