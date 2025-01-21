using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.FactLogic;
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
















            #region Beastform Features
            var BeastmorphBeastformMutagenFeature = Helpers.CreateBlueprint<BlueprintFeature>("BeastmorphBeastformMutagenFeature", bp => {
                bp.SetName("Beastform Mutagen");
                bp.SetDescription("At 3rd level, a beastmorph’s mutagen causes him to take on animalistic features, such as a furry muzzle and pointed ears like a " +
                    "werewolf, scaly skin like a lizardfolk or sahuagin, or compound eyes and mandibles like a giant insect. Before using his mutagen the beastmorph " +
                    "may select one of the following additional effects for the duration. " +
                    "\nScent - Detect unseen foes within 15 feet by sense of smell, as if you had {g|Encyclopedia:Blindsense}blindsense{/g}. " +
                    "\nScaly Skin - Gain a +2 natural armor bonus to AC. " +
                    "\nCompound Eyes - Gain a +4 bonus to {g|Encyclopedia:Perception}perception{/g}.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        BeastmorphBeastformMutagenScent.ToReference<BlueprintUnitFactReference>(),
                        BeastmorphBeastformMutagenScales.ToReference<BlueprintUnitFactReference>(),
                        BeastmorphBeastformMutagenEyes.ToReference<BlueprintUnitFactReference>(),
                        BeastmorphBeastformMutagenBase.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = false;
            });
            var BeastmorphBeastformImprovedMutagenFeature = Helpers.CreateBlueprint<BlueprintFeature>("BeastmorphBeastformImprovedMutagenFeature", bp => {
                bp.SetName("Improved Beastform Mutagen");
                bp.SetDescription("At 6th level, a beastmorph’s mutagen grants him additional abilities and options. He may select two different abilities at once before " +
                    "using his mutagen, and the following effects are added to the list of beastform options. " +
                    "\nSpeed - Your movement speed is increased by 10 feet, increasing to 20 feet at 10th level, and 30 feet at 14th level. " +
                    "\nBuoyancy - You are immune to ground effects and being tripped. ");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        BeastmorphBeastformMutagenSpeed.ToReference<BlueprintUnitFactReference>(),
                        BeastmorphBeastformMutagenBuoyancy.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = false;
            });
            var BeastmorphBeastformGreaterMutagenFeature = Helpers.CreateBlueprint<BlueprintFeature>("BeastmorphBeastformGreaterMutagenFeature", bp => {
                bp.SetName("Greater Beastform Mutagen");
                bp.SetDescription("At 10th level, a beastmorph’s mutagen grants him more additional abilities and options. He may select three different abilities at once before " +
                    "using his mutagen, and the following effects are added to the list of beastform options. " +
                    "\nPounce - You can perform a full attack at the end of your charge. " +
                    "\nTrip - Make a free trip attempt on your first melee attack that hits each round. ");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        BeastmorphBeastformMutagenPounce.ToReference<BlueprintUnitFactReference>(),
                        BeastmorphBeastformMutagenTrip.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = false;
            });
            var BeastmorphBeastformGrandMutagenFeature = Helpers.CreateBlueprint<BlueprintFeature>("BeastmorphBeastformGrandMutagenFeature", bp => {
                bp.SetName("Grand Beastform Mutagen");
                bp.SetDescription("At 14th level, a beastmorph’s mutagen grants him more exotic abilities and options. He may select four different abilities at once before " +
                    "using his mutagen, and the following effects are added to the list of beastform options. " +
                    "\nBlindsense - Detect unseen foes within 30 feet, this range does not stack with the range from the scent mutagen. " +
                    "\nFerocity - When your hit point total is below 0 but you are not killed, you can fight on for 1 more {g|Encyclopedia:Combat_Round}round{/g}. " +
                    "At the end of your next turn, unless brought to above 0 {g|Encyclopedia:HP}hit points{/g}, you immediately fall {g|Encyclopedia:Injury_Death}unconscious{/g}. " +
                    "\nWeb - You gain the ability to fire webs as if a spider, you must wait one minute between uses and the DC of the web is 10 + half your beastmorph level + " +
                    "constitution modifier.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        BeastmorphBeastformMutagenBlinesense.ToReference<BlueprintUnitFactReference>(),
                        BeastmorphBeastformMutagenFerocity.ToReference<BlueprintUnitFactReference>(),
                        BeastmorphBeastformMutagenWeb.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = false;
            });












            #endregion

            BeastmorphArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(2, PoisonResistanceFeature),
                    Helpers.LevelEntry(5, PoisonResistanceFeature),
                    Helpers.LevelEntry(8, PoisonResistanceFeature),
                    Helpers.LevelEntry(10, PoisonImmunityFeature),
                    Helpers.LevelEntry(14, PersistentMutagenFeature)
            };
            BeastmorphArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(3, BeastmorphBeastformMutagenFeature),
                    Helpers.LevelEntry(6, BeastmorphBeastformImprovedMutagenFeature),
                    Helpers.LevelEntry(10, BeastmorphBeastformGreaterMutagenFeature),
                    Helpers.LevelEntry(14, BeastmorphBeastformGrandMutagenFeature)
            };

            if (ModSettings.AddedContent.Archetypes.IsDisabled("Beastmorph")) { return; }
            AlchemistClass.m_Archetypes = AlchemistClass.m_Archetypes.AppendToArray(BeastmorphArchetype.ToReference<BlueprintArchetypeReference>());
        }
    }
}
