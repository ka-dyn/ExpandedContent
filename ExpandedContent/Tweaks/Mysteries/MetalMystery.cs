using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Craft;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System.Collections.Generic;
using TabletopTweaks.Core.NewComponents;

namespace ExpandedContent.Tweaks.Mysteries {
    internal class MetalMystery {
        public static void AddMetalMystery() {

            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var OracleRevelationSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("60008a10ad7ad6543b1f63016741a5d2");
            var MetalMysteryIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleMetalMystery.png");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");

            //Spelllist
            var LeadBladesSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("779179912e6c6fe458fa4cfb90d96e10");
            var AlignWeaponSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d026d5c80dbb9224f9a97fec83f5644a");
            var KeenEdgeSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("64923c9c808143c7ab7adba5a27f0ca4");
            var ShieldOfDawnSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("62888999171921e4dafb46de83f4d67d");
            var DanceOfAHundredCutsAbility = Resources.GetModBlueprint<BlueprintAbility>("DanceOfAHundredCutsAbility");
            var BladeBarrierSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("36c8971e91f1745418cc3ffdfac17b74");
            var RepulsionSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("cb55ae9517a444548d3457f91f829679");
            var IronBodySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("198fcc43490993f49899ed086fe723c1");
            var OverWhelmingPresenceSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("41cf93453b027b94886901dbfc680cb9");
            var OwlsWisdomMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("9f5ada581af3db4419b54db77f44e430");
            var OwlsWisdomSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f0455c9295b53904f9e02fc571dd2ce1");
            var RemoveBlindnessSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c927a8b0cd3f5174f8c0b67cdbfde539");
            var ConfusionSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("cf6c901fb7acc904e85c63b342e9c949");
            var MindBlankSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("df2a0ba6b6dcecf429cbb80a56fee5cf");
            var MindBlankMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("87a29febd010993419f2a4a9bee11cfc");
            var TrueSeeingSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4cf3d0fae3239ec478f51e86f49161cb");
            var TrueSeeingMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fa08cb49ade3eee42b5fd42bd33cb407");
            var SoundBurstSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c3893092a333b93499fd0a21845aa265");
            var ShoutSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f09453607e683784c8fca646eec49162");
            var SongOfDiscordSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d38aaf487e29c3d43a3bffa4a4a55f8f");
            var ShoutGreaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fd0d3840c48cafb44bb29e8eb74df204");
            var BrilliantInspirationSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("a5c56f0f699daec44b7aedd8b273b08a");
            var OracleMetalSpells = Helpers.CreateBlueprint<BlueprintFeature>("OracleMetalSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = LeadBladesSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = AlignWeaponSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = KeenEdgeSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShieldOfDawnSpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DanceOfAHundredCutsAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BladeBarrierSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = RepulsionSpell;
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = IronBodySpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = OverWhelmingPresenceSpell;
                    c.SpellLevel = 9;
                });
            });
            var EnlightnedPhilosopherMetalSpells = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherMetalSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = LeadBladesSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = OwlsWisdomSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = RemoveBlindnessSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ConfusionSpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueSeeingSpell;
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = OwlsWisdomMassSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueSeeingMassSpell;
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MindBlankSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MindBlankMassSpell;
                    c.SpellLevel = 9;
                });
            });
            var OceansEchoMetalSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoMetalSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = LeadBladesSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = KeenEdgeSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutSpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SongOfDiscordSpell;
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutGreaterSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BrilliantInspirationSpell;
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = IronBodySpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = OverWhelmingPresenceSpell;
                    c.SpellLevel = 9;
                });
            });
            //Final Revelation
            var WeaponFocus = Resources.GetBlueprint<BlueprintParametrizedFeature>("1e1f627d26ad36f43bbd26cc2bf8ac7e");
            var WeaponFocusGreater = Resources.GetBlueprint<BlueprintParametrizedFeature>("09c9e82965fb4334b984a1e9df3bd088");
            var ImprovedCritical = Resources.GetBlueprint<BlueprintParametrizedFeature>("f4201c85a991369408740c6888362e20");
            var OracleMetalFinalRevelationFocusSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("OracleMetalFinalRevelationFocusSelection", bp => {
                bp.SetName("Final Revelation - Weapon Focus");
                bp.SetDescription("Upon reaching 20th level, you become a master of iron and steel. You gain the benefits of Weapon Focus, Greater " +
                    "Weapon Focus, and Improved Critical with any weapon that you are proficient with. \nYour armor is like a second skin to you—while " +
                    "wearing metal armor you are proficient with, the armor’s maximum Dexterity bonus increases by +5 and you take no armor check penalty.");
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    WeaponFocus.ToReference<BlueprintFeatureReference>()
                };
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var OracleMetalFinalRevelationGreaterFocusSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("OracleMetalFinalRevelationGreaterFocusSelection", bp => {
                bp.SetName("Final Revelation - Greater Weapon Focus");
                bp.SetDescription("Upon reaching 20th level, you become a master of iron and steel. You gain the benefits of Weapon Focus, Greater " +
                    "Weapon Focus, and Improved Critical with any weapon that you are proficient with. \nYour armor is like a second skin to you—while " +
                    "wearing metal armor you are proficient with, the armor’s maximum Dexterity bonus increases by +5 and you take no armor check penalty.");
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    WeaponFocusGreater.ToReference<BlueprintFeatureReference>()
                };
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var OracleMetalFinalRevelationImprovedCritSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("OracleMetalFinalRevelationImprovedCritSelection", bp => {
                bp.SetName("Final Revelation - Improved Critical");
                bp.SetDescription("Upon reaching 20th level, you become a master of iron and steel. You gain the benefits of Weapon Focus, Greater " +
                    "Weapon Focus, and Improved Critical with any weapon that you are proficient with. \nYour armor is like a second skin to you—while " +
                    "wearing metal armor you are proficient with, the armor’s maximum Dexterity bonus increases by +5 and you take no armor check penalty.");
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    ImprovedCritical.ToReference<BlueprintFeatureReference>()
                };
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var OracleMetalFinalRevelation = Helpers.CreateBlueprint<BlueprintProgression>("OracleMetalFinalRevelation", bp => {
                bp.SetName("Final Revelation");
                bp.SetDescription("Upon reaching 20th level, you become a master of iron and steel. You gain the benefits of Weapon Focus, Greater " +
                    "Weapon Focus, and Improved Critical with any weapon that you are proficient with. \nYour armor is like a second skin to you—while " +
                    "wearing metal armor you are proficient with, the armor’s maximum Dexterity bonus increases by +5 and you take no armor check penalty.");
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(20, OracleMetalFinalRevelationFocusSelection, OracleMetalFinalRevelationGreaterFocusSelection, OracleMetalFinalRevelationImprovedCritSelection)
                };
                bp.AddComponent<MaxDexBonusIncrease>(c => {
                    c.Bonus = 5;
                    c.BonesPerRank = 0;
                    c.CheckCategory = true;
                    c.Category = ArmorProficiencyGroup.Heavy;
                });
                bp.AddComponent<IgnoreArmorCheckPenalty>(c => {
                    c.CheckCategory = false;
                    c.Categorys = new ArmorProficiencyGroup[] { };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.IsClassFeature = true;
                bp.GiveFeaturesForPreviousLevels = true;
            });
            //Main Mystery Feature
            var OracleMetalMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleMetalMysteryFeature", bp => {
                bp.m_Icon = MetalMysteryIcon;
                bp.SetName("Metal");
                bp.SetDescription("An oracle with the Metal mystery adds {g|Encyclopedia:Athletics}Athletics{/g}, {g|Encyclopedia:Persuasion}Persuasion{/g} " +
                    "and {g|Encyclopedia:Use_Magic_Device}Use Magic Device{/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.Level = 20;
                    c.m_Feature = OracleMetalFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleMetalSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillAthletics;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillUseMagicDevice;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //EnlightnedPhilosopherMystery
            var EnlightnedPhilosopherMetalMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherMetalMysteryFeature", bp => {
                bp.m_Icon = MetalMysteryIcon;
                bp.SetName("Metal");
                bp.SetDescription("An oracle with the Metal mystery adds {g|Encyclopedia:Athletics}Athletics{/g}, {g|Encyclopedia:Persuasion}Persuasion{/g} " +
                    "and {g|Encyclopedia:Use_Magic_Device}Use Magic Device{/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = EnlightnedPhilosopherMetalSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillAthletics;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillUseMagicDevice;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.EnlightenedPhilosopherMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //DivineHerbalistMystery
            var DivineHerbalistMetalMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("DivineHerbalistMetalMysteryFeature", bp => {
                bp.m_Icon = MetalMysteryIcon;
                bp.SetName("Metal");
                bp.SetDescription("Gain access to the spells and revelations of the Metal mystery. \nDue to the divine herbalist archetype the class skills gained from this archetype" +
                    "are replaced by the master herbalist feature and the brew potions feat.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleMetalFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleMetalSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.DivineHerbalistMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //Ocean's Echo
            var OceansEchoMetalMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoMetalMysteryFeature", bp => {
                bp.m_Icon = MetalMysteryIcon;
                bp.SetName("Metal");
                bp.SetDescription("Gain access to the spells and revelations of the Metal mystery. \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleMetalFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoMetalSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.None };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            


            MysteryTools.RegisterMystery(OracleMetalMysteryFeature);
            MysteryTools.RegisterSecondMystery(OracleMetalMysteryFeature);
            MysteryTools.RegisterEnlightendPhilosopherMystery(EnlightnedPhilosopherMetalMysteryFeature);
            MysteryTools.RegisterSecondEnlightendPhilosopherMystery(EnlightnedPhilosopherMetalMysteryFeature);
            MysteryTools.RegisterHerbalistMystery(DivineHerbalistMetalMysteryFeature);
            MysteryTools.RegisterSecondHerbalistMystery(DivineHerbalistMetalMysteryFeature);
            MysteryTools.RegisterOceansEchoMystery(OceansEchoMetalMysteryFeature);
            MysteryTools.RegisterSecondOceansEchoMystery(OceansEchoMetalMysteryFeature);
            MysteryTools.RegisterHermitMystery(OracleMetalMysteryFeature);
            MysteryTools.RegisterSecondHermitMystery(OracleMetalMysteryFeature);
            MysteryTools.RegisterMysteryGiftSelection(OracleMetalMysteryFeature);
        }
    }
}
