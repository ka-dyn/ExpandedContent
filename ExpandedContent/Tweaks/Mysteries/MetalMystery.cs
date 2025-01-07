using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Components;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.EventConditionActionSystem.Conditions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UI.GenericSlot;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
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
            #region Final Revelation
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
                bp.IgnorePrerequisites = true;
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
                bp.IgnorePrerequisites = true;
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
            #endregion
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
            #region Armor Bank
            var ChainshirtType = Resources.GetBlueprintReference<BlueprintArmorTypeReference>("7467b0ab8641d8f43af7fc46f2108a1a");//Light
            var ChainshirtBardingType = Resources.GetBlueprintReference<BlueprintArmorTypeReference>("7b9bb0bc92bb7414d8ba44bcdd55ece6");//Light
            var BreastplateTypeLight = Resources.GetBlueprintReference<BlueprintArmorTypeReference>("fc833429a7064e499b5177e04a032a72");//Light
            var ScalemailType = Resources.GetBlueprintReference<BlueprintArmorTypeReference>("f95c21c70a5677346b75e447c7225ba6");//Medium
            var ScaleBardingType = Resources.GetBlueprintReference<BlueprintArmorTypeReference>("abefac7d8f98aeb40a167e0f5978d9c7");//Medium
            var ChainmailType = Resources.GetBlueprintReference<BlueprintArmorTypeReference>("cd4a47c5bacbff3498e960eec3a83485");//Medium
            var ChainmailBardingType = Resources.GetBlueprintReference<BlueprintArmorTypeReference>("bdcce0ac4c930b84a849f935a4bdd93e");//Medium
            var BreastplateType = Resources.GetBlueprintReference<BlueprintArmorTypeReference>("d326c3c61a84c6f40977c84fab41503d");//Medium
            var BandedType = Resources.GetBlueprintReference<BlueprintArmorTypeReference>("da1b160cd13f16a429499b96636f6ed9");//Heavy
            var HalfplateType = Resources.GetBlueprintReference<BlueprintArmorTypeReference>("a59bf41f441456a4e8b04b09cb889af8");//Heavy
            var FullBardingType = Resources.GetBlueprintReference<BlueprintArmorTypeReference>("55a6ac3d3ed31fc4d8a7f26f509999a8");//Heavy
            var FullplateType = Resources.GetBlueprintReference<BlueprintArmorTypeReference>("afd9065539b3ac5499eddca923654c4b");//Heavy
            #endregion
            #region ArmorMastery
            var OracleRevelationArmorMasteryEffectFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationArmorMasteryEffectFeature", bp => {
                bp.SetName("OracleRevelationArmorMasteryEffectFeature");
                bp.SetDescription("");
                bp.AddComponent<AddMechanicsFeature>(c => {
                    c.m_Feature = AddMechanicsFeature.MechanicsFeatureType.ImmunityToMediumArmorSpeedPenalty;
                });
                bp.AddComponent<ArmorCheckPenaltyIncrease>(c => {
                    c.Bonus = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.BonesPerRank = 0;
                    c.CheckCategory = false;//Not needed due to HasArmorFeatureUnlock
                    c.Category = ArmorProficiencyGroup.None;
                });
                bp.AddComponent<MaxDexBonusIncrease>(c => {
                    c.Bonus = 0;
                    c.BonesPerRank = 0;
                    c.UseContextInstead = true;
                    c.ContextBonus = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.CheckCategory = false;//Not needed due to HasArmorFeatureUnlock
                    c.Category = ArmorProficiencyGroup.None;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DivStep;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 5;
                    c.m_UseMax = true;
                    c.m_Max = 3;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { };
                });
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.ReapplyOnLevelUp = true;
            });
            var OracleRevelationArmorMasteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationArmorMasteryFeature", bp => {
                bp.SetName("Armor Mastery");
                bp.SetDescription("You become more maneuverable while wearing armor. You can move at your normal speed in medium armor that is made of metal. " +
                    "This does not grant proficiency in armor. \nAt 5th level, whenever you are wearing metal armor, you reduce the armor check penalty by 1 " +
                    "(to a minimum of 0) and increase the maximum Dexterity bonus allowed by your armor by 1. " +
                    "\nAt 10th level, and again at 15th level, these bonuses increase by 1.");
                bp.AddComponent<HasArmorFeatureUnlock>(c => {
                    c.m_NewFact = OracleRevelationArmorMasteryEffectFeature.ToReference<BlueprintUnitFactReference>();
                    c.FilterByBlueprintArmorTypes = true;
                    c.m_BlueprintArmorTypes = new BlueprintArmorTypeReference[] {
                        ScalemailType, ScaleBardingType, ChainmailType, ChainmailBardingType, BreastplateType,
                        BandedType, HalfplateType, FullBardingType, FullplateType
                    };
                    c.FilterByArmorProficiencyGroup = false;
                    c.m_ArmorProficiencyGroupEntries = ArmorProficiencyGroupFlag.Light;//ignored
                    c.m_DisableWhenHasShield = false;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleMetalMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherMetalMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistMetalMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoMetalMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationArmorMasteryFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Dance of the Blades
            var MasterStrikeIcon = Resources.GetBlueprint<BlueprintBuff>("eab680abdb0194343af169af393c2603").Icon;
            var OracleRevelationDanceOfTheBladesFeatureBuff = Helpers.CreateBuff("OracleRevelationDanceOfTheBladesFeatureBuff", bp => {
                bp.SetName("Dance of the Blades - Attack Buff");
                bp.SetDescription("");
                bp.AddComponent<WeaponMultipleCategoriesContextAttackBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Categories = new WeaponCategory[] {
                        WeaponCategory.Dagger,
                        WeaponCategory.LightMace,
                        WeaponCategory.PunchingDagger,
                        WeaponCategory.Sickle,
                        WeaponCategory.HeavyMace,
                        WeaponCategory.Dart,
                        WeaponCategory.Handaxe,
                        WeaponCategory.Kukri,
                        WeaponCategory.LightHammer,
                        WeaponCategory.LightPick,
                        WeaponCategory.Shortsword,
                        WeaponCategory.Starknife,
                        WeaponCategory.WeaponLightShield,
                        WeaponCategory.SpikedLightShield,
                        WeaponCategory.Battleaxe,
                        WeaponCategory.Flail,
                        WeaponCategory.HeavyPick,
                        WeaponCategory.Longsword,
                        WeaponCategory.Rapier,
                        WeaponCategory.Scimitar,
                        WeaponCategory.Warhammer,
                        WeaponCategory.WeaponHeavyShield,
                        WeaponCategory.SpikedHeavyShield,
                        WeaponCategory.EarthBreaker,
                        WeaponCategory.Falchion,
                        WeaponCategory.Glaive,
                        WeaponCategory.Greataxe,
                        WeaponCategory.Greatsword,
                        WeaponCategory.HeavyFlail,
                        WeaponCategory.Scythe,
                        WeaponCategory.Sai,
                        WeaponCategory.Siangham,//??
                        WeaponCategory.BastardSword,
                        WeaponCategory.DuelingSword,
                        WeaponCategory.DwarvenWaraxe,
                        WeaponCategory.Estoc,
                        WeaponCategory.Falcata,
                        WeaponCategory.Tongi,
                        WeaponCategory.ElvenCurvedBlade,
                        WeaponCategory.Fauchard,
                        WeaponCategory.Shuriken,
                        WeaponCategory.Bardiche,
                        WeaponCategory.DoubleSword,
                        WeaponCategory.DoubleAxe,
                        WeaponCategory.Urgrosh,
                        WeaponCategory.HookedHammer,
                        WeaponCategory.ThrowingAxe,
                        WeaponCategory.SawtoothSabre
                    };
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.ExceptForCategories = false;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 7;
                    c.m_StepLevel = 4;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
            });
            var OracleRevelationDanceOfTheBladesAbilityBuff = Helpers.CreateBuff("OracleRevelationDanceOfTheBladesAbilityBuff", bp => {
                bp.SetName("Dance of the Blades - Shield Buff");
                bp.SetDescription("As a move action, you can maneuver your weapon to create a shield of whirling steel around yourself until the start of your next turn; " +
                    "non-incorporeal melee and ranged attacks against you have a 20% miss chance while the shield is active. You must be wielding a metal weapon to use this ability.");
                bp.m_Icon = MasterStrikeIcon;
                bp.AddComponent<AddConcealment>(c => {
                    c.Descriptor = ConcealmentDescriptor.Blur;
                    c.Concealment = Concealment.Partial;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.CheckDistance = false;
                    c.DistanceGreater = 0.Feet();
                    c.OnlyForAttacks = true;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 7;
                    c.m_StepLevel = 4;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                });
            });
            var OracleRevelationDanceOfTheBladesAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationDanceOfTheBladesAbility", bp => {
                bp.SetName("Dance of the Blades");
                bp.SetDescription("As a move action, you can maneuver your weapon to create a shield of whirling steel around yourself until the start of your next turn; " +
                    "non-incorporeal melee and ranged attacks against you have a 20% miss chance while the shield is active. You must be wielding a metal weapon to use this ability.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleRevelationDanceOfTheBladesAbilityBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                BonusValue = 1,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityCasterMainWeaponCheck>(c => {
                    c.Category = new WeaponCategory[] {
                        WeaponCategory.Dagger,
                        WeaponCategory.LightMace,
                        WeaponCategory.PunchingDagger,
                        WeaponCategory.Sickle,
                        WeaponCategory.HeavyMace,
                        WeaponCategory.Dart,
                        WeaponCategory.Handaxe,
                        WeaponCategory.Kukri,
                        WeaponCategory.LightHammer,
                        WeaponCategory.LightPick,
                        WeaponCategory.Shortsword,
                        WeaponCategory.Starknife,
                        WeaponCategory.WeaponLightShield,
                        WeaponCategory.SpikedLightShield,
                        WeaponCategory.Battleaxe,
                        WeaponCategory.Flail,
                        WeaponCategory.HeavyPick,
                        WeaponCategory.Longsword,
                        WeaponCategory.Rapier,
                        WeaponCategory.Scimitar,
                        WeaponCategory.Warhammer,
                        WeaponCategory.WeaponHeavyShield,
                        WeaponCategory.SpikedHeavyShield,
                        WeaponCategory.EarthBreaker,
                        WeaponCategory.Falchion,
                        WeaponCategory.Glaive,
                        WeaponCategory.Greataxe,
                        WeaponCategory.Greatsword,
                        WeaponCategory.HeavyFlail,
                        WeaponCategory.Scythe,
                        WeaponCategory.Sai,
                        WeaponCategory.Siangham,//??
                        WeaponCategory.BastardSword,
                        WeaponCategory.DuelingSword,
                        WeaponCategory.DwarvenWaraxe,
                        WeaponCategory.Estoc,
                        WeaponCategory.Falcata,
                        WeaponCategory.Tongi,
                        WeaponCategory.ElvenCurvedBlade,
                        WeaponCategory.Fauchard,
                        WeaponCategory.Shuriken,
                        WeaponCategory.Bardiche,
                        WeaponCategory.DoubleSword,
                        WeaponCategory.DoubleAxe,
                        WeaponCategory.Urgrosh,
                        WeaponCategory.HookedHammer,
                        WeaponCategory.ThrowingAxe,
                        WeaponCategory.SawtoothSabre
                    };
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = MasterStrikeIcon;
                bp.Type = AbilityType.Extraordinary;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Move;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationDanceOfTheBladesAbility.Duration", "1 round");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationDanceOfTheBladesAbilityFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDanceOfTheBladesAbilityFeature", bp => {
                bp.SetName("Dance of the Blades - Ability Holder");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationDanceOfTheBladesAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;                
            });
            var OracleRevelationDanceOfTheBladesFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDanceOfTheBladesFeature", bp => {
                bp.SetName("Dance of the Blades");
                bp.SetDescription("Your base speed increases by 10 feet. \nAt 7th level, you gain a +1 bonus on attack rolls with a metal weapon in any " +
                    "round in which you move at least 10 feet. This bonus increases by +1 at 11th level, and every four levels thereafter. \nAt 11th level, " +
                    "as a move action, you can maneuver your weapon to create a shield of whirling steel around yourself until the start of your next turn; " +
                    "non-incorporeal melee and ranged attacks against you have a 20% miss chance while the shield is active. " +
                    "You must be wielding a metal weapon to use this ability.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.Speed;
                    c.Value = 10;
                });
                bp.AddComponent<MovementDistanceTrigger>(c => {
                    c.Action = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new IsInCombat() {
                                        Not = false,
                                        Unit = null,
                                        Player = true
                                    }
                                }
                            },
                            IfFalse = Helpers.CreateActionList(),
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleRevelationDanceOfTheBladesFeatureBuff.ToReference<BlueprintBuffReference>(),
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        BonusValue = 1,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        m_IsExtendable = true
                                    },
                                    DurationSeconds = 0
                                }
                                )
                        }
                        );
                    c.DistanceInFeet = new ContextValue() { Value = 10 };
                    c.LimitTiggerCountInOneRound = true;
                    c.TiggerCountMaximumInOneRound = 1;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                    };
                    c.Level = 11;
                    c.m_Feature = OracleRevelationDanceOfTheBladesAbilityFeature.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleMetalMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherMetalMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistMetalMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoMetalMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationDanceOfTheBladesFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Iron Constitution
            var OracleRevelationIronConstitutionFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationIronConstitutionFeature", bp => {
                bp.SetName("Iron Constitution");
                bp.SetDescription("You gain a +1 bonus on Fortitude saves. At 7th level, and again at 14th level, this bonus increases by +1.");
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SaveFortitude;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.OnePlusDivStep;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 7;
                    c.m_UseMax = true;
                    c.m_Max = 3;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { };
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleMetalMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherMetalMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistMetalMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoMetalMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationIronConstitutionFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Iron Skin
            var StoneskinBuff = Resources.GetBlueprint<BlueprintBuff>("37a956d0e7a84ab0bb66baf784767047");
            var OracleRevelationIronSkinResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationIronSkinResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = true,
                    LevelIncrease = 1,
                    IncreasedByLevelStartPlusDivStep = false,
                    StartingLevel = 0,
                    StartingIncrease = 1,
                    LevelStep = 15,
                    PerStepIncrease = 1,
                    MinClassLevelIncrease = 0,
                    OtherClassesModifier = 0,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Unknown,
                    m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                    }
                };
                bp.m_UseMax = true;
                bp.m_Max = 2;
            });
            var OracleRevelationIronSkinBuff = Helpers.CreateBuff("OracleRevelationIronSkinBuff", bp => {
                bp.SetName("Iron Skin");
                bp.SetDescription("The warded creature gains resistance to blows, cuts, stabs, and slashes. The subject gains {g|Encyclopedia:Damage_Reduction}DR{/g} 10/adamantine. " +
                    "It ignores the first 10 points of {g|Encyclopedia:Damage}damage{/g} each time it takes damage from a weapon, though an adamantine weapon overcomes the reduction. " +
                    "Once the {g|Encyclopedia:Spell}spell{/g} has prevented a total of 10 points of damage per {g|Encyclopedia:Caster_Level}caster level{/g} (maximum 150 points), " +
                    "it is discharged.");
                bp.m_Icon = StoneskinBuff.Icon;
                bp.AddComponent(Helpers.CreateCopy(StoneskinBuff.GetComponent<AddDamageResistancePhysical>()));
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.MultiplyByModifier;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 10;
                    c.m_UseMax = true;
                    c.m_Max = 150;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                });
            });
            var OracleRevelationIronSkinAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationIronSkinAbility", bp => {
                bp.SetName("Iron Skin");
                bp.SetDescription("Once per day, your skin hardens and takes on the appearance of iron, granting you DR 10/adamantine. " +
                    "This ability functions as stoneskin, using your oracle level as the caster level, except it only affects you. " +
                    "At 15th level, you can use this ability twice per day. " +
                    "\nStoneskin \nThe warded creature gains resistance to blows, cuts, stabs, and slashes. The subject gains {g|Encyclopedia:Damage_Reduction}DR{/g} 10/adamantine, " +
                    "for 10 minutes per {g|Encyclopedia:Caster_Level}caster level{/g}. " +
                    "It ignores the first 10 points of {g|Encyclopedia:Damage}damage{/g} each time it takes damage from a weapon, though an adamantine weapon overcomes the reduction. " +
                    "Once the {g|Encyclopedia:Spell}spell{/g} has prevented a total of 10 points of damage per {g|Encyclopedia:Caster_Level}caster level{/g} (maximum 150 points), " +
                    "it is discharged.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleRevelationIronSkinBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.TenMinutes,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default
                                },
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 0;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { };
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationIronSkinResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Icon = StoneskinBuff.Icon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Move;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationIronSkinAbility.Duration", "10 minutes/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var OracleRevelationIronSkinFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationIronSkinFeature", bp => {
                bp.SetName("Iron Skin");
                bp.SetDescription("Once per day, your skin hardens and takes on the appearance of iron, granting you DR 10/adamantine. " +
                    "This ability functions as stoneskin, using your oracle level as the caster level, except it only affects you. " +
                    "At 15th level, you can use this ability twice per day. " +
                    "\nStoneskin \nThe warded creature gains resistance to blows, cuts, stabs, and slashes. The subject gains {g|Encyclopedia:Damage_Reduction}DR{/g} 10/adamantine, " +
                    "for 10 minutes per {g|Encyclopedia:Caster_Level}caster level{/g}. " +
                    "It ignores the first 10 points of {g|Encyclopedia:Damage}damage{/g} each time it takes damage from a weapon, though an adamantine weapon overcomes the reduction. " +
                    "Once the {g|Encyclopedia:Spell}spell{/g} has prevented a total of 10 points of damage per {g|Encyclopedia:Caster_Level}caster level{/g} (maximum 150 points), " +
                    "it is discharged.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationIronSkinAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationIronSkinResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Group = Prerequisite.GroupType.Any;
                    c.Level = 11;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 11;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleMetalMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherMetalMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistMetalMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoMetalMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationIronSkinFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Iron Weapon
            var CrusadersEdgeIcon = Resources.GetBlueprint<BlueprintBuff>("7ca348639a91ae042967f796098e3bc3").Icon;
            var ColdIronWeaponEnchentment = Resources.GetBlueprint<BlueprintWeaponEnchantment>("e5990dc76d2a613409916071c898eee8");
            var Enhancement1 = Resources.GetBlueprint<BlueprintWeaponEnchantment>("d42fc23b92c640846ac137dc26e000d4");
            var Enhancement2 = Resources.GetBlueprint<BlueprintWeaponEnchantment>("eb2faccc4c9487d43b3575d7e77ff3f5");
            var Enhancement3 = Resources.GetBlueprint<BlueprintWeaponEnchantment>("80bb8a737579e35498177e1e3c75899b");
            var AdamantineWeaponEnchantment = Resources.GetBlueprint<BlueprintWeaponEnchantment>("ab39e7d59dd12f4429ffef5dca88dc7b");

            var OracleRevelationIronWeaponEnchantResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationIronWeaponEnchantResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Charisma,
                };
            });
            var OracleRevelationIronWeaponEnchantFlag7 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationIronWeaponEnchantFlag7", bp => {
                bp.SetName("OracleRevelationIronWeaponEnchantFlag7");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var OracleRevelationIronWeaponEnchantFlag11 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationIronWeaponEnchantFlag11", bp => {
                bp.SetName("OracleRevelationIronWeaponEnchantFlag11");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var OracleRevelationIronWeaponEnchantFlag15 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationIronWeaponEnchantFlag15", bp => {
                bp.SetName("OracleRevelationIronWeaponEnchantFlag15");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var OracleRevelationIronWeaponEnchantFlag19 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationIronWeaponEnchantFlag19", bp => {
                bp.SetName("OracleRevelationIronWeaponEnchantFlag19");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });

            var OracleRevelationIronWeaponEnchantBuffBaseMain = Helpers.CreateBuff("OracleRevelationIronWeaponEnchantBuffBaseMain", bp => {
                bp.SetName("Iron Weapon Enchant (Main Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of metal, while enchanted the weapons are considered made of cold iron. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon is considered made of adamantine. This effect lasts a number of minutes equal to your oracle level. " +
                    "You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = CrusadersEdgeIcon;
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = ColdIronWeaponEnchentment.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.PrimaryHand;
                });
            });
            var OracleRevelationIronWeaponEnchantBuffBaseOffHand = Helpers.CreateBuff("OracleRevelationIronWeaponEnchantBuffBaseOffHand", bp => {
                bp.SetName("Iron Weapon Enchant (Off Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of metal, while enchanted the weapons are considered made of cold iron. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon is considered made of adamantine. This effect lasts a number of minutes equal to your oracle level. " +
                    "You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = CrusadersEdgeIcon;
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = ColdIronWeaponEnchentment.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.SecondaryHand;
                });
            });
            var OracleRevelationIronWeaponEnchantBuff7Main = Helpers.CreateBuff("OracleRevelationIronWeaponEnchantBuff7Main", bp => {
                bp.SetName("Iron Weapon Enchant (Main Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of metal, while enchanted the weapons are considered made of cold iron. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon is considered made of adamantine. This effect lasts a number of minutes equal to your oracle level. " +
                    "You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = CrusadersEdgeIcon;
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = ColdIronWeaponEnchentment.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.PrimaryHand;
                });
            });
            var OracleRevelationIronWeaponEnchantBuff7OffHand = Helpers.CreateBuff("OracleRevelationIronWeaponEnchantBuff7OffHand", bp => {
                bp.SetName("Iron Weapon Enchant (Off Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of metal, while enchanted the weapons are considered made of cold iron. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon is considered made of adamantine. This effect lasts a number of minutes equal to your oracle level. " +
                    "You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = CrusadersEdgeIcon;
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = ColdIronWeaponEnchentment.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.SecondaryHand;
                });
            });
            var OracleRevelationIronWeaponEnchantBuff11Main = Helpers.CreateBuff("OracleRevelationIronWeaponEnchantBuff11Main", bp => {
                bp.SetName("Iron Weapon Enchant (Main Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of metal, while enchanted the weapons are considered made of cold iron. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon is considered made of adamantine. This effect lasts a number of minutes equal to your oracle level. " +
                    "You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = CrusadersEdgeIcon;
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = ColdIronWeaponEnchentment.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.PrimaryHand;
                });
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = AdamantineWeaponEnchantment.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.PrimaryHand;
                });
            });
            var OracleRevelationIronWeaponEnchantBuff11OffHand = Helpers.CreateBuff("OracleRevelationIronWeaponEnchantBuff11OffHand", bp => {
                bp.SetName("Iron Weapon Enchant (Off Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of metal, while enchanted the weapons are considered made of cold iron. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon is considered made of adamantine. This effect lasts a number of minutes equal to your oracle level. " +
                    "You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = CrusadersEdgeIcon;
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = ColdIronWeaponEnchentment.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.SecondaryHand;
                });
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = AdamantineWeaponEnchantment.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.SecondaryHand;
                });
            });
            var OracleRevelationIronWeaponEnchantBuff15Main = Helpers.CreateBuff("OracleRevelationIronWeaponEnchantBuff15Main", bp => {
                bp.SetName("Iron Weapon Enchant (Main Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of metal, while enchanted the weapons are considered made of cold iron. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon is considered made of adamantine. This effect lasts a number of minutes equal to your oracle level. " +
                    "You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = CrusadersEdgeIcon;
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = ColdIronWeaponEnchentment.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.PrimaryHand;
                });
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = AdamantineWeaponEnchantment.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.PrimaryHand;
                });
            });
            var OracleRevelationIronWeaponEnchantBuff15OffHand = Helpers.CreateBuff("OracleRevelationIronWeaponEnchantBuff15OffHand", bp => {
                bp.SetName("Iron Weapon Enchant (Off Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of metal, while enchanted the weapons are considered made of cold iron. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon is considered made of adamantine. This effect lasts a number of minutes equal to your oracle level. " +
                    "You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = CrusadersEdgeIcon;
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = ColdIronWeaponEnchentment.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.SecondaryHand;
                });
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = AdamantineWeaponEnchantment.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.SecondaryHand;
                });
            });
            var OracleRevelationIronWeaponEnchantBuff19Main = Helpers.CreateBuff("OracleRevelationIronWeaponEnchantBuff19Main", bp => {
                bp.SetName("Iron Weapon Enchant (Main Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of metal, while enchanted the weapons are considered made of cold iron. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon is considered made of adamantine. This effect lasts a number of minutes equal to your oracle level. " +
                    "You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = CrusadersEdgeIcon;
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = ColdIronWeaponEnchentment.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.PrimaryHand;
                });
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = AdamantineWeaponEnchantment.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.PrimaryHand;
                });
            });
            var OracleRevelationIronWeaponEnchantBuff19OffHand = Helpers.CreateBuff("OracleRevelationIronWeaponEnchantBuff19OffHand", bp => {
                bp.SetName("Iron Weapon Enchant (Off Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of metal, while enchanted the weapons are considered made of cold iron. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon is considered made of adamantine. This effect lasts a number of minutes equal to your oracle level. " +
                    "You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = CrusadersEdgeIcon;
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = ColdIronWeaponEnchentment.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.SecondaryHand;
                });
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = AdamantineWeaponEnchantment.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.SecondaryHand;
                });
            });

            var OracleRevelationIronWeaponEnchantAbilityMainHand = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationIronWeaponEnchantAbilityMainHand", bp => {
                bp.SetName("Iron Weapon Enchant (Main Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of metal, while enchanted the weapons are considered made of cold iron. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon is considered made of adamantine. This effect lasts a number of minutes equal to your oracle level. " +
                    "You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = CrusadersEdgeIcon;
                bp.AddComponent<AbilityCasterMainWeaponCheck>(c => {
                    c.Category = new WeaponCategory[] {
                        WeaponCategory.Dagger,
                        WeaponCategory.LightMace,
                        WeaponCategory.PunchingDagger,
                        WeaponCategory.Sickle,
                        WeaponCategory.HeavyMace,
                        WeaponCategory.Dart,
                        WeaponCategory.Handaxe,
                        WeaponCategory.Kukri,
                        WeaponCategory.LightHammer,
                        WeaponCategory.LightPick,
                        WeaponCategory.Shortsword,
                        WeaponCategory.Starknife,
                        WeaponCategory.WeaponLightShield,
                        WeaponCategory.SpikedLightShield,
                        WeaponCategory.Battleaxe,
                        WeaponCategory.Flail,
                        WeaponCategory.HeavyPick,
                        WeaponCategory.Longsword,
                        WeaponCategory.Rapier,
                        WeaponCategory.Scimitar,
                        WeaponCategory.Warhammer,
                        WeaponCategory.WeaponHeavyShield,
                        WeaponCategory.SpikedHeavyShield,
                        WeaponCategory.EarthBreaker,
                        WeaponCategory.Falchion,
                        WeaponCategory.Glaive,
                        WeaponCategory.Greataxe,
                        WeaponCategory.Greatsword,
                        WeaponCategory.HeavyFlail,
                        WeaponCategory.Scythe,
                        WeaponCategory.Sai,
                        WeaponCategory.Siangham,//??
                        WeaponCategory.BastardSword,
                        WeaponCategory.DuelingSword,
                        WeaponCategory.DwarvenWaraxe,
                        WeaponCategory.Estoc,
                        WeaponCategory.Falcata,
                        WeaponCategory.Tongi,
                        WeaponCategory.ElvenCurvedBlade,
                        WeaponCategory.Fauchard,
                        WeaponCategory.Shuriken,
                        WeaponCategory.Bardiche,
                        WeaponCategory.DoubleSword,
                        WeaponCategory.DoubleAxe,
                        WeaponCategory.Urgrosh,
                        WeaponCategory.HookedHammer,
                        WeaponCategory.ThrowingAxe,
                        WeaponCategory.SawtoothSabre
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new EnhanceWeapon() {
                            m_Enchantment = new BlueprintItemEnchantmentReference[] {
                                Enhancement1.ToReference<BlueprintItemEnchantmentReference>(),
                                Enhancement2.ToReference<BlueprintItemEnchantmentReference>(),
                                Enhancement3.ToReference<BlueprintItemEnchantmentReference>()
                            },
                            EnchantmentType = EnhanceWeapon.EnchantmentApplyType.MagicWeapon,
                            Greater = true,
                            UseSecondaryHand = false,
                            EnchantLevel = new ContextValue() {
                                ValueType = ContextValueType.Rank,
                                Value = 1,
                                ValueRank = AbilityRankType.ProjectilesCount
                            },
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default
                                },
                                m_IsExtendable = false
                            }
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionCasterHasFact() {
                                        m_Fact = OracleRevelationIronWeaponEnchantFlag19.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleRevelationIronWeaponEnchantBuff19Main.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Minutes,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        },
                                        m_IsExtendable = false,
                                    },
                                    AsChild = true
                                }),
                            IfFalse = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = OracleRevelationIronWeaponEnchantFlag15.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = OracleRevelationIronWeaponEnchantBuff15Main.ToReference<BlueprintBuffReference>(),
                                            Permanent = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Minutes,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    ValueRank = AbilityRankType.Default
                                                },
                                                m_IsExtendable = false,
                                            },
                                            AsChild = true
                                        }),
                                    IfFalse = Helpers.CreateActionList(
                                        new Conditional() {
                                            ConditionsChecker = new ConditionsChecker() {
                                                Operation = Operation.And,
                                                Conditions = new Condition[] {
                                                    new ContextConditionCasterHasFact() {
                                                        m_Fact = OracleRevelationIronWeaponEnchantFlag11.ToReference<BlueprintUnitFactReference>(),
                                                        Not = false
                                                    }
                                                }
                                            },
                                            IfTrue = Helpers.CreateActionList(
                                                new ContextActionApplyBuff() {
                                                    m_Buff = OracleRevelationIronWeaponEnchantBuff11Main.ToReference<BlueprintBuffReference>(),
                                                    Permanent = false,
                                                    DurationValue = new ContextDurationValue() {
                                                        Rate = DurationRate.Minutes,
                                                        DiceType = DiceType.Zero,
                                                        DiceCountValue = 0,
                                                        BonusValue = new ContextValue() {
                                                            ValueType = ContextValueType.Rank,
                                                            ValueRank = AbilityRankType.Default
                                                        },
                                                        m_IsExtendable = false,
                                                    },
                                                    AsChild = true
                                                }),
                                            IfFalse = Helpers.CreateActionList(
                                                new Conditional() {
                                                    ConditionsChecker = new ConditionsChecker() {
                                                        Operation = Operation.And,
                                                        Conditions = new Condition[] {
                                                            new ContextConditionCasterHasFact() {
                                                                m_Fact = OracleRevelationIronWeaponEnchantFlag7.ToReference<BlueprintUnitFactReference>(),
                                                                Not = false
                                                            }
                                                        }
                                                    },
                                                    IfTrue = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = OracleRevelationIronWeaponEnchantBuff7Main.ToReference<BlueprintBuffReference>(),
                                                            Permanent = false,
                                                            DurationValue = new ContextDurationValue() {
                                                                Rate = DurationRate.Minutes,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = 0,
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    ValueRank = AbilityRankType.Default
                                                                },
                                                                m_IsExtendable = false,
                                                            },
                                                            AsChild = true
                                                        }),
                                                    IfFalse = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = OracleRevelationIronWeaponEnchantBuffBaseMain.ToReference<BlueprintBuffReference>(),
                                                            Permanent = false,
                                                            DurationValue = new ContextDurationValue() {
                                                                Rate = DurationRate.Minutes,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = 0,
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    ValueRank = AbilityRankType.Default
                                                                },
                                                                m_IsExtendable = false,
                                                            },
                                                            AsChild = true
                                                        })
                                                })
                                        })
                                })
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationIronWeaponEnchantResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 0;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = OracleRevelationIronWeaponEnchantFlag7.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        OracleRevelationIronWeaponEnchantFlag7.ToReference<BlueprintFeatureReference>(),
                        OracleRevelationIronWeaponEnchantFlag15.ToReference<BlueprintFeatureReference>(),
                        OracleRevelationIronWeaponEnchantFlag19.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.EnchantWeapon;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationIronWeaponEnchantAbilityMainHand.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationIronWeaponEnchantAbilityOffHand = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationIronWeaponEnchantAbilityOffHand", bp => {
                bp.SetName("Iron Weapon Enchant (Off Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of metal, while enchanted the weapons are considered made of cold iron. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon is considered made of adamantine. This effect lasts a number of minutes equal to your oracle level. " +
                    "You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = CrusadersEdgeIcon;
                bp.AddComponent<AbilityCasterOffHandWeaponCheck>(c => {
                    c.Category = new WeaponCategory[] {                        
                        WeaponCategory.Dagger,
                        WeaponCategory.LightMace,
                        WeaponCategory.PunchingDagger,
                        WeaponCategory.Sickle,
                        WeaponCategory.HeavyMace,
                        WeaponCategory.Dart,
                        WeaponCategory.Handaxe,
                        WeaponCategory.Kukri,
                        WeaponCategory.LightHammer,
                        WeaponCategory.LightPick,
                        WeaponCategory.Shortsword,
                        WeaponCategory.Starknife,
                        WeaponCategory.WeaponLightShield,
                        WeaponCategory.SpikedLightShield,
                        WeaponCategory.Battleaxe,
                        WeaponCategory.Flail,
                        WeaponCategory.HeavyPick,
                        WeaponCategory.Longsword,
                        WeaponCategory.Rapier,
                        WeaponCategory.Scimitar,
                        WeaponCategory.Warhammer,
                        WeaponCategory.WeaponHeavyShield,
                        WeaponCategory.SpikedHeavyShield,
                        WeaponCategory.EarthBreaker,
                        WeaponCategory.Falchion,
                        WeaponCategory.Glaive,
                        WeaponCategory.Greataxe,
                        WeaponCategory.Greatsword,
                        WeaponCategory.HeavyFlail,
                        WeaponCategory.Scythe,
                        WeaponCategory.Sai,
                        WeaponCategory.Siangham,//??
                        WeaponCategory.BastardSword,
                        WeaponCategory.DuelingSword,
                        WeaponCategory.DwarvenWaraxe,
                        WeaponCategory.Estoc,
                        WeaponCategory.Falcata,
                        WeaponCategory.Tongi,
                        WeaponCategory.ElvenCurvedBlade,
                        WeaponCategory.Fauchard,
                        WeaponCategory.Shuriken,
                        WeaponCategory.Bardiche,
                        WeaponCategory.DoubleSword,
                        WeaponCategory.DoubleAxe,
                        WeaponCategory.Urgrosh,
                        WeaponCategory.HookedHammer,
                        WeaponCategory.ThrowingAxe,
                        WeaponCategory.SawtoothSabre
                    };
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new EnhanceWeapon() {
                            m_Enchantment = new BlueprintItemEnchantmentReference[] {
                                Enhancement1.ToReference<BlueprintItemEnchantmentReference>(),
                                Enhancement2.ToReference<BlueprintItemEnchantmentReference>(),
                                Enhancement3.ToReference<BlueprintItemEnchantmentReference>()
                            },
                            EnchantmentType = EnhanceWeapon.EnchantmentApplyType.MagicWeapon,
                            Greater = true,
                            UseSecondaryHand = true,
                            EnchantLevel = new ContextValue() {
                                ValueType = ContextValueType.Rank,
                                Value = 1,
                                ValueRank = AbilityRankType.ProjectilesCount
                            },
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default
                                },
                                m_IsExtendable = false
                            }
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionCasterHasFact() {
                                        m_Fact = OracleRevelationIronWeaponEnchantFlag19.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleRevelationIronWeaponEnchantBuff19OffHand.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Minutes,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.Default
                                        },
                                        m_IsExtendable = false,
                                    },
                                    AsChild = true
                                }),
                            IfFalse = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = OracleRevelationIronWeaponEnchantFlag15.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = OracleRevelationIronWeaponEnchantBuff15OffHand.ToReference<BlueprintBuffReference>(),
                                            Permanent = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Minutes,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = new ContextValue() {
                                                    ValueType = ContextValueType.Rank,
                                                    ValueRank = AbilityRankType.Default
                                                },
                                                m_IsExtendable = false,
                                            },
                                            AsChild = true
                                        }),
                                    IfFalse = Helpers.CreateActionList(
                                        new Conditional() {
                                            ConditionsChecker = new ConditionsChecker() {
                                                Operation = Operation.And,
                                                Conditions = new Condition[] {
                                                    new ContextConditionCasterHasFact() {
                                                        m_Fact = OracleRevelationIronWeaponEnchantFlag11.ToReference<BlueprintUnitFactReference>(),
                                                        Not = false
                                                    }
                                                }
                                            },
                                            IfTrue = Helpers.CreateActionList(
                                                new ContextActionApplyBuff() {
                                                    m_Buff = OracleRevelationIronWeaponEnchantBuff11OffHand.ToReference<BlueprintBuffReference>(),
                                                    Permanent = false,
                                                    DurationValue = new ContextDurationValue() {
                                                        Rate = DurationRate.Minutes,
                                                        DiceType = DiceType.Zero,
                                                        DiceCountValue = 0,
                                                        BonusValue = new ContextValue() {
                                                            ValueType = ContextValueType.Rank,
                                                            ValueRank = AbilityRankType.Default
                                                        },
                                                        m_IsExtendable = false,
                                                    },
                                                    AsChild = true
                                                }),
                                            IfFalse = Helpers.CreateActionList(
                                                new Conditional() {
                                                    ConditionsChecker = new ConditionsChecker() {
                                                        Operation = Operation.And,
                                                        Conditions = new Condition[] {
                                                            new ContextConditionCasterHasFact() {
                                                                m_Fact = OracleRevelationIronWeaponEnchantFlag7.ToReference<BlueprintUnitFactReference>(),
                                                                Not = false
                                                            }
                                                        }
                                                    },
                                                    IfTrue = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = OracleRevelationIronWeaponEnchantBuff7OffHand.ToReference<BlueprintBuffReference>(),
                                                            Permanent = false,
                                                            DurationValue = new ContextDurationValue() {
                                                                Rate = DurationRate.Minutes,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = 0,
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    ValueRank = AbilityRankType.Default
                                                                },
                                                                m_IsExtendable = false,
                                                            },
                                                            AsChild = true
                                                        }),
                                                    IfFalse = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = OracleRevelationIronWeaponEnchantBuffBaseOffHand.ToReference<BlueprintBuffReference>(),
                                                            Permanent = false,
                                                            DurationValue = new ContextDurationValue() {
                                                                Rate = DurationRate.Minutes,
                                                                DiceType = DiceType.Zero,
                                                                DiceCountValue = 0,
                                                                BonusValue = new ContextValue() {
                                                                    ValueType = ContextValueType.Rank,
                                                                    ValueRank = AbilityRankType.Default
                                                                },
                                                                m_IsExtendable = false,
                                                            },
                                                            AsChild = true
                                                        })
                                                })
                                        })
                                })
                        });
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationIronWeaponEnchantResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 0;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] {  };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = OracleRevelationIronWeaponEnchantFlag7.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        OracleRevelationIronWeaponEnchantFlag7.ToReference<BlueprintFeatureReference>(),
                        OracleRevelationIronWeaponEnchantFlag15.ToReference<BlueprintFeatureReference>(),
                        OracleRevelationIronWeaponEnchantFlag19.ToReference<BlueprintFeatureReference>()
                    };
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 1;
                    c.m_UseMax = false;
                    c.m_Max = 20;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.EnchantWeapon;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationIronWeaponEnchantAbilityOffHand.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationIronWeaponEnchantAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationIronWeaponEnchantAbility", bp => {
                bp.SetName("Iron Weapon Enchant");
                bp.SetDescription("You can imbue held weapons made mostly of metal, while enchanted the weapons are considered made of cold iron. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon is considered made of adamantine. This effect lasts a number of minutes equal to your oracle level. " +
                    "You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = CrusadersEdgeIcon;
                bp.AddComponent<AbilityVariants>(c => {
                    c.m_Variants = new BlueprintAbilityReference[] {
                        OracleRevelationIronWeaponEnchantAbilityMainHand.ToReference<BlueprintAbilityReference>(),
                        OracleRevelationIronWeaponEnchantAbilityOffHand.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationIronWeaponEnchantResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.EnchantWeapon;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationIronWeaponEnchantAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            OracleRevelationIronWeaponEnchantAbilityMainHand.m_Parent = OracleRevelationIronWeaponEnchantAbility.ToReference<BlueprintAbilityReference>();
            OracleRevelationIronWeaponEnchantAbilityOffHand.m_Parent = OracleRevelationIronWeaponEnchantAbility.ToReference<BlueprintAbilityReference>();

            var OracleRevelationIronWeaponEnchantFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationIronWeaponEnchantFeature", bp => {
                bp.SetName("Iron Weapon Enchant");
                bp.SetDescription("You can imbue held weapons made mostly of metal, while enchanted the weapons are considered made of cold iron. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon is considered made of adamantine. This effect lasts a number of minutes equal to your oracle level. " +
                    "You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationIronWeaponEnchantAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleMetalMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherMetalMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistMetalMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoMetalMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                    };
                    c.Level = 7;
                    c.m_Feature = OracleRevelationIronWeaponEnchantFlag7.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.Level = 11;
                    c.m_Feature = OracleRevelationIronWeaponEnchantFlag11.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.Level = 15;
                    c.m_Feature = OracleRevelationIronWeaponEnchantFlag15.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.Level = 19;
                    c.m_Feature = OracleRevelationIronWeaponEnchantFlag19.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Group = Prerequisite.GroupType.Any;
                    c.Level = 3;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 3;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationIronWeaponEnchantResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationIronWeaponEnchantFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Skill at Arms
            var MartialWeaponProficiency = Resources.GetBlueprint<BlueprintFeature>("203992ef5b35c864390b4e4a1e200629");
            var HeavyArmorProficiency = Resources.GetBlueprint<BlueprintFeature>("1b0f68188dcc435429fb87a022239681");
            var OracleRevelationSkillAtArmsFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationSkillAtArmsFeature", bp => {
                bp.SetName("Skill at Arms");
                bp.SetDescription("You gain proficiency in all martial weapons and heavy armor.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        MartialWeaponProficiency.ToReference<BlueprintUnitFactReference>(),
                        HeavyArmorProficiency.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleMetalMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherMetalMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistMetalMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoMetalMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationSkillAtArmsFeature.ToReference<BlueprintFeatureReference>());
            #endregion


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
