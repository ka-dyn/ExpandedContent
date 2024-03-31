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
    public static class PatchBrassDragonOnLoad {

        [HarmonyPrefix]
        public static void Prefix(object resource, string guid) {
            if (guid != "54b46b9ae1e75cd42a40342fc2e753b5")  //Brass and Gold Dragons
                return;
            if (resource is UnitEntityView view) {
                Main.Log("Starting Brass Drake Mount Patch");
                PatchBrassDragonAsset(view);
                Main.Log("Finished Brass Drake Mount Patch");
            }
        }
        public static void PatchBrassDragonAsset(UnitEntityView view) {
            var offsets = view.gameObject.AddComponent<MountOffsets>();
            #region Base Config
            offsets.Root = view.Pelvis.FindChildRecursive("Locator_Torso_Upper_01");
            offsets.RootBattle = view.Pelvis.FindChildRecursive("Locator_Torso_Upper_01");

            offsets.PelvisIkTarget = MountTools.CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Torso_Upper_01"),
                "Pelvis",
                new Vector3(-0.19f, -0.64f, 0f),
                new Vector3(0f, 0f, 0f));
            offsets.LeftFootIkTarget = MountTools.CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Torso_Upper_01"),
                "LeftFoot",
                new Vector3(0.1f, 0.15f, 0.35f),
                new Vector3(0f, 0f, 90f));
            offsets.RightFootIkTarget = MountTools.CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Torso_Upper_01"),
                "RightFoot",
                new Vector3(0.1f, 0.15f, -0.35f),
                new Vector3(0f, 0f, 270f));
            offsets.LeftKneeIkTarget = MountTools.CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Torso_Upper_01"),
                "LeftKnee",
                new Vector3(-0.38f, 0f, 0.38f),
                new Vector3(0f, 0f, 0f));
            offsets.RightKneeIkTarget = MountTools.CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Torso_Upper_01"),
                "RightKnee",
                new Vector3(-0.38f, 0f, -0.38f),
                new Vector3(0f, 0f, 0f));

            offsets.Hands = MountTools.CreateMountBone(view.Pelvis.FindChildRecursive("Locator_Torso_Upper_00"),
                "Hands",
                new Vector3(0f, -0.5f, 0f),
                new Vector3(0f, 0f, 0f));
            #endregion
            #region Medium Config
            var MediumOffsetConfig = ScriptableObject.CreateInstance<RaceMountOffsetsConfig>();
            MediumOffsetConfig.name = "MediumBrassDragon_MountConfig";
            MediumOffsetConfig.offsets = new RaceMountOffsetsConfig.MountOffsetData[] {
                //Normal
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
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 1.0f,
                    HandsMappingWeight = 0.7f,
                },
                //Dwarf
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Dwarf
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0.1f, 0.1f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0f, 0f, 0.1f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0f, 0f, -0.1f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0.1f),

                    RightKneePosition = new Vector3(0f, 0f, -0.1f),

                    HandsPosition = new Vector3(0f, 0f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.9f,
                    HandsMappingWeight = 0.7f,
                },
                //Elf
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

                    LeftFootPosition = new Vector3(0.15f, 0f, 0f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(-0.15f, 0f, 0f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(0f, 0f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.98f,
                    HandsMappingWeight = 0.7f,
                },
                //Gnome
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Gnome
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

                    PelvisPositionWeight = 0.8f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 1.0f,
                    HandsMappingWeight = 0.7f,
                },
                //Halfling
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

                    HandsPosition = new Vector3(0f, 0f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 1.0f,
                    HandsMappingWeight = 0.7f,
                },
                //Half Orc
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.HalfOrc
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0f, 0f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0.15f, -0.15f, 0f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(-0.15f, 0.15f, 0f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(-0.15f, 0f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.95f,
                    HandsMappingWeight = 0.7f,
                },
                //Oread
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Oread
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

                    HandsPosition = new Vector3(0f, 0.1f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.9f,
                    HandsMappingWeight = 0.7f,
                },
            };
            offsets.MediumOffsetsConfig = MediumOffsetConfig;
            #endregion
            #region Large Config
            var LargeOffsetConfig = ScriptableObject.CreateInstance<RaceMountOffsetsConfig>();
            LargeOffsetConfig.name = "LargeBrassDragon_MountConfig";
            LargeOffsetConfig.offsets = new RaceMountOffsetsConfig.MountOffsetData[] {
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

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0.1f, 0.1f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0f, 0f, 0.1f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0f, 0f, -0.1f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0.1f),

                    RightKneePosition = new Vector3(0f, 0f, -0.1f),

                    HandsPosition = new Vector3(0f, 0f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.9f,
                    HandsMappingWeight = 0.7f,
                },
                //Elf -DONE
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

                    LeftFootPosition = new Vector3(0.15f, 0f, 0f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(-0.15f, 0f, 0f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(0f, 0f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.98f,
                    HandsMappingWeight = 0.7f,
                },
                //Gnome -DONE
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Gnome
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(-0.1f, 0f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(-0.1f, 0.35f, 0.05f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0.1f, -0.35f, -0.05f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(0f, 0.1f, 0f),

                    PelvisPositionWeight = 0.8f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 1.0f,
                    HandsMappingWeight = 0.7f,
                },
                //Halfling -DONE
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Halfling
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(-0.16f, 0f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(-0.05f, 0.42f, 0.15f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0.05f, -0.42f, -0.15f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0.02f),

                    RightKneePosition = new Vector3(0f, 0f, -0.02f),

                    HandsPosition = new Vector3(0f, 0.15f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 1.0f,
                    HandsMappingWeight = 0.7f,
                },
                //Half Orc -DONE
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.HalfOrc
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(0f, 0f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0.15f, -0.15f, 0f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(-0.15f, 0.15f, 0f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(-0.15f, 0f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.95f,
                    HandsMappingWeight = 0.7f,
                },
                //Oread -DONE
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Oread
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

                    HandsPosition = new Vector3(0f, 0.1f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.9f,
                    HandsMappingWeight = 0.7f,
                },
            };
            offsets.LargeOffsetsConfig = LargeOffsetConfig;
            #endregion
            #region Huge Config
            var HugeOffsetConfig = ScriptableObject.CreateInstance<RaceMountOffsetsConfig>();
            HugeOffsetConfig.name = "HugeBrassDragon_MountConfig";
            HugeOffsetConfig.offsets = new RaceMountOffsetsConfig.MountOffsetData[] {
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

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(-0.16f, 0.09f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(-0.05f, 0.35f, 0.1f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0.05f, -0.35f, -0.1f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0.05f),

                    RightKneePosition = new Vector3(0f, 0f, -0.05f),

                    HandsPosition = new Vector3(0f, 0.15f, 0f),

                    PelvisPositionWeight = 1f,
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

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(-0.06f, 0.1f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(-0.05f, 0.39f, 0.3f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0.05f, -0.39f, -0.3f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(-0.1f, 0f, 0.15f),

                    RightKneePosition = new Vector3(-0.1f, 0f, -0.15f),

                    HandsPosition = new Vector3(0f, 0.15f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.9f,
                    HandsMappingWeight = 0.7f,
                },
                //Elf -DONE
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Elf
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(-0.16f, 0f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(-0.09f, 0.35f, 0.05f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0.09f, -0.35f, -0.05f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(-0.1f, -0.09f, -0.09f),

                    RightKneePosition = new Vector3(-0.1f, -0.09f, 0.09f),

                    HandsPosition = new Vector3(0f, 0.15f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.98f,
                    HandsMappingWeight = 0.7f,
                },
                //Gnome -DONE
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Gnome
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(-0.25f, 0f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(-0.45f, 0.75f, 0.1f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0.45f, -0.75f, -0.1f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(-1.5f, -0.1f, 0f),

                    RightKneePosition = new Vector3(-1.5f, -0.1f, 0f),

                    HandsPosition = new Vector3(-0.1f, 0.1f, 0f),

                    PelvisPositionWeight = 0.7f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.9f,
                    HandsMappingWeight = 0.7f,
                },
                //Halfling -DONE
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Halfling
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),
                    
                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),
                    
                    PelvisPosition = new Vector3(-0.35f, 0.15f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(-0.4f, 0.75f, 0.1f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0.4f, -0.75f, -0.1f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(-1.5f, 0f, 1.5f),

                    RightKneePosition = new Vector3(-1.5f, 0f, -1.5f),

                    HandsPosition = new Vector3(-0.11f, 0.15f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 0.5f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.995f,
                    HandsMappingWeight = 0.7f,
                },
                //Half Orc -DONE
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.HalfOrc
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(-0.16f, 0.09f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(0.07f, 0.33f, 0.1f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(-0.07f, -0.33f, -0.1f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0f),

                    RightKneePosition = new Vector3(0f, 0f, 0f),

                    HandsPosition = new Vector3(-0.15f, 0f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1f,
                    KneesBendWeight = 1.0f,
                    HandsPositionWeight = 0.95f,
                    HandsMappingWeight = 0.7f,
                },
                //Oread -DONE
                new RaceMountOffsetsConfig.MountOffsetData() {
                    Races = new List<BlueprintRaceReference>() {
                        MountTools.RaceOptions.Oread
                    },
                    RootPosition = new Vector3(0f, 0f, 0f),
                    RootBattlePosition = new Vector3(0f, 0f, 0f),

                    SaddleRootPosition = new Vector3(0f, 0f, 0f),
                    SaddleRootScale = Vector3.one,
                    SaddleRootRotation = new Vector4(0, 0, 0, 1),

                    PelvisPosition = new Vector3(-0.16f, 0.08f, 0f),
                    PelvisRotation = new Vector4(0, 0, 0, 1),

                    LeftFootPosition = new Vector3(-0.04f, 0.39f, 0.07f),
                    LeftFootRotation = new Vector4(0, 0, 0, 1),

                    RightFootPosition = new Vector3(0.04f, -0.39f, -0.07f),
                    RightFootRotation = new Vector4(0, 0, 0, 1),

                    LeftKneePosition = new Vector3(0f, 0f, 0.01f),

                    RightKneePosition = new Vector3(0f, 0f, -0.01f),

                    HandsPosition = new Vector3(0f, 0.19f, 0f),

                    PelvisPositionWeight = 1f,
                    PelvisRotationWeight = 1.0f,
                    FootsPositionWeight = 1.0f,
                    FootsRotationWeight = 1.0f,
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
