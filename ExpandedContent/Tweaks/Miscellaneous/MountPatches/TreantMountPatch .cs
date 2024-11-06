using ExpandedContent.Utilities;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Root;
using Kingmaker.Modding;
using Kingmaker.View;
using Kingmaker.Visual.Mounts;
using Owlcat.Runtime.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ExpandedContent.Tweaks.Miscellaneous.MountPatches {

    [HarmonyPatch(typeof(OwlcatModificationsManager), nameof(OwlcatModificationsManager.OnResourceLoaded))]
    public static class PatchTreantOnLoad {

        [HarmonyPrefix]
        public static void Prefix(object resource, string guid) {
            if (guid != "16f5bf5f4dc3c9e4dab4165b360a5e3d")  //Treant
                return;
            if (resource is UnitEntityView view) {
                Main.Log("Starting Treant Mount Patch");
                PatchTreantAsset(view);
                Main.Log("Finished Treant Mount Patch");
            }
        }
        public static void PatchTreantAsset(UnitEntityView view) {
            var offsets = view.gameObject.AddComponent<MountOffsets>();
            #region Base Config
            offsets.Root = view.Pelvis.FindChildRecursive("Locator_Arm_LeftUpper_00");
            offsets.RootBattle = view.Pelvis.FindChildRecursive("Locator_Arm_LeftUpper_00");

            offsets.PelvisIkTarget = MountTools.CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Arm_LeftUpper_00"),
                "Pelvis",
                new Vector3(-35f, 0f, 35f),
                new Vector3(0f, 0f, 0f));
            offsets.LeftFootIkTarget = MountTools.CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Arm_LeftUpper_00"),
                "LeftFoot",
                new Vector3(-78f, -70f, -29f),
                new Vector3(0f, 0f, 0f));
            offsets.RightFootIkTarget = MountTools.CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Arm_LeftUpper_00"),
                "RightFoot",
                new Vector3(-28f, -70f, -29f),
                new Vector3(0f, 0f, 0f));
            offsets.LeftKneeIkTarget = MountTools.CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Arm_LeftUpper_00"),
                "LeftKnee",
                new Vector3(0f, -2000f, 0f),
                new Vector3(0f, 0f, 0f));
            offsets.RightKneeIkTarget = MountTools.CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Arm_LeftUpper_00"),
                "RightKnee",
                new Vector3(0f, -2000f, 0f),
                new Vector3(0f, 0f, 0f));

            offsets.Hands = MountTools.CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Arm_LeftUpper_00"),//Fails - Locator_Arm_LeftUpper_00 - L_Arm_Clavicle - Locator_Torso_Upper_04
                "Hands",
                new Vector3(-38f, -22f, 65f),
                new Vector3(0f, 0f, 0f));
            #endregion
            #region Medium Config
            var MediumOffsetConfig = ScriptableObject.CreateInstance<RaceMountOffsetsConfig>();
            MediumOffsetConfig.name = "MediumTreant_MountConfig";
            MediumOffsetConfig.offsets = new RaceMountOffsetsConfig.MountOffsetData[] {
                //Normal - Done
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Aasimar,
                        MountTools.RaceOptions.Dhampir,
                        MountTools.RaceOptions.HalfElf,
                        MountTools.RaceOptions.Human,
                        MountTools.RaceOptions.Kitsune,
                        MountTools.RaceOptions.Mongrelman,
                        MountTools.RaceOptions.Tiefling,
                        MountTools.RaceOptions.AscendingSuccubus,
                        MountTools.RaceOptions.Android
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0f, 0f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0f, 0f, 0f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0f, 0f, 0f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(0f, 0f, 0f),

                    PelvisPositionWeight = 0.9f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 1.0f,
                    HandsMappingWeight = 0.7f,
                },
                //Dwarf - Happy
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Dwarf
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0f, 0.05f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0.3f, 0f, 0.12f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0.3f, 0f, -0.12f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0.1f),

                    RightKneePosition = new Vector3(0f, 0f, -0.1f),

                    HandsPosition = new Vector3(0f, 0f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.9f,
                    HandsMappingWeight = 0.7f,
                },
                //Elf - Happy
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Elf
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0f, 0f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0.2f, 0f, -0.1f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0.1f, 0f, -0.1f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(0.1f, -0.05f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.98f,
                    HandsMappingWeight = 0.7f,
                },
                //Gnome - Happy
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Gnome
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(-0.14f, 0f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0.05f, -0.3f, -0.22f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0.05f, -0.3f, 0.22f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(0.18f, 0.05f, 0.1f),

                    PelvisPositionWeight = 0.8f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 1.0f,
                    HandsMappingWeight = 0.7f,
                },
                //Halfling - Happy
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Halfling
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0f, 0f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0f, 0f, 0f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0f, 0f, 0f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(0f, -0.21f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 1.0f,
                    HandsMappingWeight = 0.7f,
                },
                //Half Orc - Happyish
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.HalfOrc
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(-0.1f, 0f, 0.1f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0f, 0f, -0.18f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0f, 0f, -0.18f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(-0.30f, -0.45f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.95f,
                    HandsMappingWeight = 0.7f,
                },
                //Oread - Happyish
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Oread
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(-0.07f, 0f, 0.15f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0f, 0f, 0f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0f, 0f, 0f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(-0.25f, -0.35f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.9f,
                    HandsMappingWeight = 0.7f,
                },
            };
            offsets.MediumOffsetsConfig = MediumOffsetConfig;
            #endregion
            #region Large Config
            var LargeOffsetConfig = ScriptableObject.CreateInstance<RaceMountOffsetsConfig>();
            LargeOffsetConfig.name = "LargeTreant_MountConfig";
            LargeOffsetConfig.offsets = new RaceMountOffsetsConfig.MountOffsetData[] {
                //Normal - Done
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Aasimar,
                        MountTools.RaceOptions.Dhampir,
                        MountTools.RaceOptions.HalfElf,
                        MountTools.RaceOptions.Human,
                        MountTools.RaceOptions.Kitsune,
                        MountTools.RaceOptions.Mongrelman,
                        MountTools.RaceOptions.Tiefling,
                        MountTools.RaceOptions.AscendingSuccubus,
                        MountTools.RaceOptions.Android
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0f, 0f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0f, 0f, 0f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0f, 0f, 0f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(0f, 0f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 1.0f,
                    HandsMappingWeight = 0.7f,
                },
                //Dwarf - Happy
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Dwarf
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0f, 0.05f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0.3f, 0f, 0.12f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0.3f, 0f, -0.12f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0.1f),

                    RightKneePosition = new Vector3(0f, 0f, -0.1f),

                    HandsPosition = new Vector3(0f, 0f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.9f,
                    HandsMappingWeight = 0.7f,
                },
                //Elf - Happy
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Elf
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0f, 0f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0.2f, 0f, -0.1f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0.1f, 0f, -0.1f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(0.1f, -0.05f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.98f,
                    HandsMappingWeight = 0.7f,
                },
                //Gnome - Happy
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Gnome
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0.06f, 0f, -0.03f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0.2f, 0f, 0.28f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0.1f, 0f, 0.28f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(0.15f, -0.08f, -0.05f),

                    PelvisPositionWeight = 0.8f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 1.0f,
                    HandsMappingWeight = 0.7f,
                },
                //Halfling - As good as I can get it
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Halfling
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0.05f, 0f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0.24f, 0.35f, 0.25f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0.05f, 0.35f, 0.25f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(0.05f, -0.24f, -0.09f),

                    PelvisPositionWeight = 0.9f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 1.0f,
                    HandsMappingWeight = 0.7f,
                },
                //Half Orc - Happyish
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.HalfOrc
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(-0.1f, 0f, 0.1f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0f, 0f, -0.18f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0f, 0f, -0.18f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(-0.30f, -0.45f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.95f,
                    HandsMappingWeight = 0.7f,
                },
                //Oread - Happyish
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Oread
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(-0.07f, 0f, 0.15f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0f, 0f, 0f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0f, 0f, 0f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(-0.25f, -0.35f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.9f,
                    HandsMappingWeight = 0.7f,
                },
            };
            offsets.LargeOffsetsConfig = LargeOffsetConfig;
            #endregion
            #region Huge Config
            var HugeOffsetConfig = ScriptableObject.CreateInstance<RaceMountOffsetsConfig>();
            HugeOffsetConfig.name = "HugeTreant_MountConfig";//I don't think there is a way to make the plants huge in game, so I'm going to skip these for now
            HugeOffsetConfig.offsets = new RaceMountOffsetsConfig.MountOffsetData[] {
                //Normal -
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Aasimar,
                        MountTools.RaceOptions.Dhampir,
                        MountTools.RaceOptions.HalfElf,
                        MountTools.RaceOptions.Human,
                        MountTools.RaceOptions.Kitsune,
                        MountTools.RaceOptions.Mongrelman,
                        MountTools.RaceOptions.Tiefling,
                        MountTools.RaceOptions.AscendingSuccubus,
                        MountTools.RaceOptions.Android
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(-0.15f, 0.05f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0f, -0.1f, 0f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0f, -0.1f, 0f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(-0.24f, 0.25f, 0.1f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 1.0f,
                    HandsMappingWeight = 0.7f,
                },
                //Dwarf -
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Dwarf
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0.02f, 0.18f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(-0.15f, -0.15f, -0.19f),
                    LeftFootRotation = new Vector4(0, 0, 1, 0),

                    RightFootPosition = new Vector3(-0.15f, -0.15f, 0.19f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(-0.11f, 0.25f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.9f,
                    HandsMappingWeight = 0.7f,
                },
                //Elf -
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Elf
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0f, 0.06f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0.15f, -0.24f, -0.15f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0.15f, -0.24f, 0.15f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(-0.1f, -0.09f, 0.1f),

                    RightKneePosition = new Vector3(-0.1f, -0.09f, -0.1f),

                    HandsPosition = new Vector3(-0.05f, 0.25f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.98f,
                    HandsMappingWeight = 0.7f,
                },
                //Gnome -
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Gnome
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(-0.15f, 0.08f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(-0.35f, -0.25f, 0.1f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(-0.35f, -0.25f, -0.1f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(-1.5f, -0.1f, 0f),

                    RightKneePosition = new Vector3(-1.5f, -0.1f, 0f),

                    HandsPosition = new Vector3(-0.32f, 0.05f, 0f),

                    PelvisPositionWeight = 0.7f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.9f,
                    HandsMappingWeight = 0.7f,
                },
                //Halfling - but with the height problem
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Halfling
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(-0.05f, 0.35f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(-0.4f, 0.75f, 0.1f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(-0.4f, 0.75f, -0.1f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(-1.5f, 0f, 1.5f),

                    RightKneePosition = new Vector3(-1.5f, 0f, -1.5f),

                    HandsPosition = new Vector3(-0.02f, 0.15f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.995f,
                    HandsMappingWeight = 0.8f,
                },
                //Half Orc -
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.HalfOrc
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0f, 0.11f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0.19f, -0.2f, -0.1f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0.19f, -0.2f, 0.1f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0.1f),

                    RightKneePosition = new Vector3(0f, 0f, -0.1f),

                    HandsPosition = new Vector3(-0.16f, 0.09f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.95f,
                    HandsMappingWeight = 0.7f,
                },
                //Oread -
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Oread
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(-0.09f, 0.16f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(-0.11f, 0f, 0.11f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(-0.11f, 0f, -0.11f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0.01f),

                    RightKneePosition = new Vector3(0f, 0f, -0.01f),

                    HandsPosition = new Vector3(-0.21f, 0.28f, -0.04f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.9f,
                    HandsMappingWeight = 0.7f,
                },
            };
            offsets.HugeOffsetsConfig = HugeOffsetConfig;
            #endregion
        }
    }
}
