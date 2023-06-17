using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using HarmonyLib;
using Kingmaker.Blueprints.Root.Strings;
using Kingmaker.EntitySystem;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExpandedContent.Tweaks.Miscellaneous {
    public class DrakePetTypeAdder {
        //Weekend project
        public enum CustomPetProgressionType : int {
            DrakeCompanion = 5000
        }
        [HarmonyPatch(typeof(AddPet), nameof(AddPet.GetPetLevel))]
        static class Patch_DrakeProgression {
            static void Postfix(AddPet __instance, ref int __result) {
                if (__instance.m_UseContextValueLevel) { return; }
                if (!__instance.LevelRank) { return; }
                EntityFact fact = __instance.Owner.GetFact(__instance.LevelRank);
                int rank = Mathf.Min(20, fact?.GetRank() ?? 0);
                switch (__instance.ProgressionType) {
                    case (PetProgressionType)CustomPetProgressionType.DrakeCompanion:
                        __result = Math.Min(__instance.Owner.Progression.CharacterLevel, RankToLevelDrakeCompanion[rank]);
                        return;
                    default:
                        return;
                }
            }

            private static readonly int[] RankToLevelDrakeCompanion = new int[] {
                0,
                2,
                3,
                3,
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                12,
                13,
                14,
                15,
                16,
                17,
                18,
                19,
                20
            };
        }
    }
}
