﻿using ExpandedContent.Utilities;
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
    public static class PatchWolverineOnLoad {

        [HarmonyPrefix]
        public static void Prefix(object resource, string guid) {
            if (guid != "dfd41d2cada9b2b4e80a9e6977b69c71") //dire wolverine
                return;
            if (resource is UnitEntityView view) {
                Main.Log("Starting Wolverine Mount Patch");
                PatchDireWolverineAsset(view);
                Main.Log("Finished Wolverine Mount Patch");
            }
        }

        public static void PatchDireWolverineAsset(UnitEntityView view) {
            var offsets = view.gameObject.AddComponent<MountOffsets>();

            offsets.Root = view.Pelvis.FindChildRecursive("Locator_Torso_Upper_01");
            offsets.RootBattle = view.Pelvis.FindChildRecursive("Locator_Torso_Upper_01");

            offsets.PelvisIkTarget = MountTools.CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Torso_Upper_01"),
                "Pelvis",
                new Vector3(0.19f, -0.2f, 0f),
                new Vector3(0f, 0f, 0f));
            offsets.LeftFootIkTarget = MountTools.CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Torso_Upper_01"),
                "LeftFoot",
                new Vector3(0f, 0.1f, 0.37f),
                new Vector3(30f, 10f, 155f));
            offsets.RightFootIkTarget = MountTools.CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Torso_Upper_01"),
                "RightFoot",
                new Vector3(0f, 0.1f, -0.37f),
                new Vector3(330f, 350f, 335f));
            offsets.LeftKneeIkTarget = MountTools.CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Torso_Upper_01"),
                "LeftKnee",
                new Vector3(-0.35f, 0.06f, 0.35f),
                //new Vector3(0f, 0f, 149.1742f));
                new Vector3(0f, 0f, 0f));
            offsets.RightKneeIkTarget = MountTools.CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Torso_Upper_01"),
                "RightKnee",
                new Vector3(-0.35f, 0.06f, -0.35f),
                //new Vector3(0f, 0f, 337.0312f));
                new Vector3(0f, 0f, 0f));

            offsets.Hands = MountTools.CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Head_00"),
                "Hands",
                new Vector3(0.1f, -0.3f, 0f),
                new Vector3(0f, 0f, 0f));

            var offsetConfig = ScriptableObject.CreateInstance<RaceMountOffsetsConfig>();
            offsetConfig.name = "MediumWolverine_MountConfig";

            offsetConfig.offsets = new RaceMountOffsetsConfig.MountOffsetData[] {
                //Normal -DONE
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

                    SaddleRootPosition = Vector3.zero,
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = Vector3.zero,
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = Vector3.zero,
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = Vector3.zero,
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = Vector3.zero,

                    RightKneePosition = Vector3.zero,

                    HandsPosition = new Vector3(0f, 0f, 0f),

                    PelvisPositionWeight = 0.7f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 1.0f,
                    HandsMappingWeight = 0.7f,
                },
                //Dwarf -DONE
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Dwarf
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = Vector3.zero,
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(-0.1f, 0f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = Vector3.zero,
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = Vector3.zero,
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = Vector3.zero,

                    RightKneePosition = Vector3.zero,

                    HandsPosition = new Vector3(0.1f, 0f, 0f),

                    PelvisPositionWeight = 0.7f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.95f,
                    HandsMappingWeight = 0.7f,
                },
                //Elf -DONE
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Elf
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = Vector3.zero,
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = Vector3.zero,
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0f, -0.1f, -0.1f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0f, 0.1f, 0.1f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = Vector3.zero,

                    RightKneePosition = Vector3.zero,

                    HandsPosition = new Vector3(0f, 0f, 0f),

                    PelvisPositionWeight = 0.7f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 1.0f,
                    HandsMappingWeight = 0.7f,
                },
                //Gnome -DONE
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Gnome
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = Vector3.zero,
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0f, 0f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0.15f, 0f, -0.1f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(-0.15f, 0f, 0.1f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = Vector3.zero,

                    RightKneePosition = Vector3.zero,

                    HandsPosition = new Vector3(0.21f, -0.04f, 0f),

                    PelvisPositionWeight = 0.8f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 1.0f,
                    HandsMappingWeight = 0.7f,
                },
                //Halfling -DONE?
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Halfling
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = Vector3.zero,
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0f, 0f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0f, 0f, 0f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0f, 0f, 0f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = Vector3.zero,

                    RightKneePosition = Vector3.zero,

                    HandsPosition = new Vector3(0f, 0f, 0f),

                    PelvisPositionWeight = 0.7f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 1.0f,
                    HandsMappingWeight = 0.7f,
                },
                //Half Orc -Done
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.HalfOrc
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = Vector3.zero,
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0.05f, 0.05f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0.2f, -0.2f, -0.18f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(-0.2f, 0.2f, 0.18f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = Vector3.zero,

                    RightKneePosition = Vector3.zero,

                    HandsPosition = new Vector3(0f, 0f, 0f),

                    PelvisPositionWeight = 0.7f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.95f,
                    HandsMappingWeight = 0.7f,
                },
                //Oread -Done
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Oread
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = Vector3.zero,
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0f, 0f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0.1f, 0f, -0.05f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(-0.1f, 0f, 0.05f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = Vector3.zero,

                    RightKneePosition = Vector3.zero,

                    HandsPosition = new Vector3(0.1f, 0f, 0f),

                    PelvisPositionWeight = 0.7f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.9f,
                    HandsMappingWeight = 0.7f,
                },
            };
            offsets.OffsetsConfig = offsetConfig;
        }


    }
}
