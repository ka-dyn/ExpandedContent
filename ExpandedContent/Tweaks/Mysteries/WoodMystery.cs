using BlueprintCore.Utils.Assets;
using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Components;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UI.GenericSlot;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
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
using UnityEngine;

namespace ExpandedContent.Tweaks.Mysteries {
    internal class WoodMystery {       

        public static void AddWoodMystery() {

            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var OracleRevelationSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("60008a10ad7ad6543b1f63016741a5d2");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var RavenerHunterArchetype = Resources.GetModBlueprint<BlueprintArchetype>("RavenerHunterArchetype");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");
            var PlantType = Resources.GetBlueprint<BlueprintFeature>("706e61781d692a042b35941f14bc41c5");
            var WoodMysteryIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleWoodMystery.png");
            var ThornBurstIcon = AssetLoader.LoadInternal("Skills", "Icon_ThornBurst.jpg"); //May change this as it looks rubbish
            var LignificationIcon = AssetLoader.LoadInternal("Skills", "Icon_Lignification.jpg"); 
            var WoodenWeaponEnchantIcon = AssetLoader.LoadInternal("Skills", "Icon_WoodenWeaponEnchant.jpg"); //May change this as it looks rubbish
            var OracleRevelationWoodArmorIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleRevelationWoodArmor.jpg");
            var ImmunityToParalysis = Resources.GetBlueprint<BlueprintFeature>("4b152a7bc5bab5042b437b955fea46cd");
            var ImmunityToPoison = Resources.GetBlueprint<BlueprintFeature>("7e3f3228be49cce49bda37f7901bf246");
            var ImmunityToStunning = Resources.GetBlueprint<BlueprintFeature>("bd9df2d4a4cef274285b8827b6769bde");
            var ImmunityToSleep = Resources.GetBlueprint<BlueprintFeature>("c263f44f72df009489409af122b5eefc");
            var BalefulPolymorphBuff = Resources.GetBlueprint<BlueprintBuff>("0a52d8761bfd125429842103aed48b90");

            //Spelllist
            var ShillelaghAbility = Resources.GetModBlueprint<BlueprintAbility>("ShillelaghAbility");
            var BarkSkinSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("5b77d7cc65b8ab74688e74a37fc2f553");
            var BurningEntangleSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("8a76293f5ab8485da95ef6293a11358c");
            var ThornBodySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2daf9c5112f16d54ab3cd6904c705c59");
            var PlantShapeIAbility = Resources.GetModBlueprint<BlueprintAbility>("PlantShapeIAbility");
            var PlantShapeIIAbility = Resources.GetModBlueprint<BlueprintAbility>("PlantShapeIIAbility");
            var PlantShapeIIIAbility = Resources.GetModBlueprint<BlueprintAbility>("PlantShapeIIIAbility");
            var ChangeStaffSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("26be70c4664d07446bdfe83504c1d757");
            var WoodenPhalanxAbility = Resources.GetModBlueprint<BlueprintAbility>("WoodenPhalanxAbility");
            var BreakEnchantmentSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("7792da00c85b9e042a0fdfc2b66ec9a8");
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
            var OracleWoodSpells = Helpers.CreateBlueprint<BlueprintFeature>("OracleWoodSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShillelaghAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BarkSkinSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BurningEntangleSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ThornBodySpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PlantShapeIAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PlantShapeIIAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PlantShapeIIIAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ChangeStaffSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = WoodenPhalanxAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 9;
                });
            });
            var EnlightnedPhilosopherWoodSpells = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherWoodSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShillelaghAbility.ToReference<BlueprintAbilityReference>();
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
            var OceansEchoWoodSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoWoodSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShillelaghAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BurningEntangleSpell;
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
                    c.m_Spell = ChangeStaffSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = WoodenPhalanxAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 9;
                });
            });
            //Final Revelation
            var EnlightenedPhilosopherFinalRevelationBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("9f1ee3c61ef993d448b0b866ee198ea8");
            var EnlightenedPhilosopherFinalRevelationResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("d19c2e7ec505b734a973ce8d0986f4d6");            
            var OracleWoodFinalRevelation = Helpers.CreateBlueprint<BlueprintFeature>("OracleWoodFinalRevelation", bp => {
                bp.SetName("Final Revelation");
                bp.SetDescription("Upon reaching 20th level, you become a living creature of wood. You gain a +4 natural armor bonus to your Armor Class and you gain immunity to paralysis, poison, " +
                    "stunning, sleep, and enemy polymorph effects.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ImmunityToParalysis.ToReference<BlueprintUnitFactReference>(),
                        ImmunityToPoison.ToReference<BlueprintUnitFactReference>(),
                        ImmunityToSleep.ToReference<BlueprintUnitFactReference>(),
                        ImmunityToStunning.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = BalefulPolymorphBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.AC;
                    c.Descriptor = ModifierDescriptor.NaturalArmor;
                    c.Value = 4;
                });                
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.IsClassFeature = true;
            });
            //Main Mystery Feature
            var OracleWoodMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleWoodMysteryFeature", bp => {
                bp.m_Icon = WoodMysteryIcon;
                bp.SetName("Wood");
                bp.SetDescription("An oracle with the wood mystery adds {g|Encyclopedia:Mobility}Mobility{/g}, {g|Encyclopedia:Lore_Nature}Lore (nature){/g}, " +
                    "{g|Encyclopedia:Stealth}Stealth{/g}  and {g|Encyclopedia:Knowledge_World}Knowledge (World) {/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.Level = 20;
                    c.m_Feature = OracleWoodFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleWoodSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillMobility;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillStealth;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //EnlightnedPhilosopherMystery
            var EnlightnedPhilosopherWoodMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherWoodMysteryFeature", bp => {
                bp.m_Icon = WoodMysteryIcon;
                bp.SetName("Wood");
                bp.SetDescription("An oracle with the wood mystery adds {g|Encyclopedia:Mobility}Mobility{/g}, {g|Encyclopedia:Lore_Nature}Lore (nature){/g}, " +
                    "{g|Encyclopedia:Stealth}Stealth{/g}  and {g|Encyclopedia:Knowledge_World}Knowledge (World) {/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = EnlightnedPhilosopherWoodSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillMobility;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillStealth;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.EnlightenedPhilosopherMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //DivineHerbalistMystery
            var DivineHerbalistWoodMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("DivineHerbalistWoodMysteryFeature", bp => {
                bp.m_Icon = WoodMysteryIcon;
                bp.SetName("Wood");
                bp.SetDescription("Gain access to the spells and revelations of the wood mystery. \nDue to the divine herbalist archetype the class skills gained from this archetype" +
                    "are replaced by the master herbalist feature and the brew potions feat.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleWoodFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleWoodSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.DivineHerbalistMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //Ocean's Echo
            var OceansEchoWoodMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoWoodMysteryFeature", bp => {
                bp.m_Icon = WoodMysteryIcon;
                bp.SetName("Wood");
                bp.SetDescription("Gain access to the spells and revelations of the wood mystery. \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleWoodFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoWoodSpells.ToReference<BlueprintFeatureReference>();
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
            //RavenerHunter
            var RavenerHunterWoodMysteryProgression = Helpers.CreateBlueprint<BlueprintProgression>("RavenerHunterWoodMysteryProgression", bp => {
                bp.SetName("Wood");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.m_Icon = WoodMysteryIcon;
                bp.m_AllowNonContextActions = false;
                bp.Groups = new FeatureGroup[] { FeatureGroup.None };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                //LevelEntry added later                
                bp.GiveFeaturesForPreviousLevels = true;
            });

            #region WoodArmor
            var OracleRevelationWoodArmorResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationWoodArmorResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = true,
                    m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[] {
                        RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    },
                    LevelIncrease = 1,
                    StartingIncrease = 1,
                };
            });
            var OracleRevelationWoodArmorDCBuff = Helpers.CreateBuff("OracleRevelationWoodArmorDCBuff", bp => {
                bp.SetName("Wood Armor");
                bp.SetDescription("You can conjure wooden armor around yourself, which grants you a +4 armor bonus. At 7th level, and every four levels thereafter, " +
                    "this bonus increases by +2. At 13th level, this armor grants you DR 5/slashing. You can use this armor for 1 hour per day per oracle level. This " +
                    "duration does not need to be consecutive, but it must be spent in 1-hour increments.");
                bp.m_Icon = OracleRevelationWoodArmorIcon;
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 7;
                    c.m_StepLevel = 4;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>() };
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Heal;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.Zero,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        }
                    };
                    c.Modifier = 2;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 4,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Shared,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Heal,
                            Property = UnitProperty.None
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Armor;
                    c.Stat = StatType.AC;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
            });
            var OracleRevelationWoodArmorDRBuff = Helpers.CreateBuff("OracleRevelationWoodArmorDRBuff", bp => {
                bp.SetName("Wood Armor");
                bp.SetDescription("You can conjure wooden armor around yourself, which grants you a +4 armor bonus. At 7th level, and every four levels thereafter, " +
                    "this bonus increases by +2. At 13th level, this armor grants you DR 5/slashing. You can use this armor for 1 hour per day per oracle level. This " +
                    "duration does not need to be consecutive, but it must be spent in 1-hour increments.");
                bp.m_Icon = OracleRevelationWoodArmorIcon;
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_StartLevel = 7;
                    c.m_StepLevel = 4;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>() };
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Heal;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.Zero,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        }
                    };
                    c.Modifier = 2;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 4,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            Property = UnitProperty.None
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Shared,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Heal,
                            Property = UnitProperty.None
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Armor;
                    c.Stat = StatType.AC;
                    c.Multiplier = 1;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.HasMinimal = false;
                    c.Minimal = 0;
                });
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Simple,
                        Value = 5,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Damage,
                        Property = UnitProperty.None
                    };
                    c.UsePool = false;
                    c.Pool = new ContextValue();
                    c.Or = false;
                    c.BypassedByMaterial = false;
                    c.Material = PhysicalDamageMaterial.Adamantite;
                    c.BypassedByForm = true;
                    c.Form = PhysicalDamageForm.Slashing;
                    c.BypassedByMagic = false;
                    c.MinEnhancementBonus = 1;
                    c.BypassedByAlignment = false;
                    c.Alignment = DamageAlignment.Good;
                    c.BypassedByReality = false;
                    c.Reality = DamageRealityType.Ghost;
                    c.BypassedByWeaponType = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.BypassedByEpic = false;
                    c.m_CheckedFactMythic = new BlueprintUnitFactReference();
                });
                bp.m_Flags = BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
            });
            var OracleRevelationWoodArmorDCAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationWoodArmorDCAbility", bp => {
                bp.SetName("Wood Armor");
                bp.SetDescription("You can conjure wooden armor around yourself, which grants you a +4 armor bonus. At 7th level, and every four levels thereafter, " +
                    "this bonus increases by +2. At 13th level, this armor grants you DR 5/slashing. You can use this armor for 1 hour per day per oracle level. This " +
                    "duration does not need to be consecutive, but it must be spent in 1-hour increments.");
                bp.m_Icon = OracleRevelationWoodArmorIcon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerHour;
                    c.m_RequiredResource = OracleRevelationWoodArmorResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = OracleRevelationWoodArmorDCBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = false;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
                bp.DeactivateIfCombatEnded = false;
            });
            var OracleRevelationWoodArmorDRAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleRevelationWoodArmorDRAbility", bp => {
                bp.SetName("Wood Armor");
                bp.SetDescription("You can conjure wooden armor around yourself, which grants you a +4 armor bonus. At 7th level, and every four levels thereafter, " +
                    "this bonus increases by +2. At 13th level, this armor grants you DR 5/slashing. You can use this armor for 1 hour per day per oracle level. This " +
                    "duration does not need to be consecutive, but it must be spent in 1-hour increments.");
                bp.m_Icon = OracleRevelationWoodArmorIcon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.OncePerHour;
                    c.m_RequiredResource = OracleRevelationWoodArmorResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = OracleRevelationWoodArmorDRBuff.ToReference<BlueprintBuffReference>();
                bp.DeactivateIfOwnerDisabled = false;
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
                bp.DeactivateIfCombatEnded = false;
            });
            var OracleRevelationWoodArmorDC = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWoodArmorDC", bp => {
                bp.SetName("Coat of Many Stars");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationWoodArmorDCAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationWoodArmorDR = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWoodArmorDR", bp => {
                bp.SetName("Coat of Many Stars");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationWoodArmorDRAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationWoodArmorFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWoodArmorFeature", bp => {
                bp.SetName("Wood Armor");
                bp.SetDescription("You can conjure wooden armor around yourself, which grants you a +4 armor bonus. At 7th level, and every four levels thereafter, " +
                    "this bonus increases by +2. At 13th level, this armor grants you DR 5/slashing. You can use this armor for 1 hour per day per oracle level. This " +
                    "duration does not need to be consecutive, but it must be spent in 1-hour increments.");
                bp.m_Icon = OracleRevelationWoodArmorIcon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.Level = 13;
                    c.m_Feature = OracleRevelationWoodArmorDC.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    }; 
                    c.Level = 13;
                    c.m_Feature = OracleRevelationWoodArmorDR.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationWoodArmorResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        RavenerHunterWoodMysteryProgression.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationWoodArmorFeature.ToReference<BlueprintFeatureReference>());
            #endregion

            #region WoodBond
            var OracleRevelationWoodBondFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWoodBondFeature", bp => {
                bp.SetName("Wood Bond");
                bp.SetDescription("Your mystical bond with wood is such that your weapons become an extension of your body. You gain a +1 competence bonus on attack rolls when " +
                    "wielding a weapon made of or mostly consisting of wood (such as a bow, club, quarterstaff, or spear). This bonus increases by +1 at 5th level and every five " +
                    "levels thereafter.");
                bp.AddComponent<WeaponMultipleCategoriesContextAttackBonus>(c => {                    
                    c.Descriptor = ModifierDescriptor.Competence;
                    c.Categories = new WeaponCategory[] { 
                        WeaponCategory.Greatclub, 
                        WeaponCategory.Club, 
                        WeaponCategory.Javelin, 
                        WeaponCategory.Kama, 
                        WeaponCategory.Longbow, 
                        WeaponCategory.Longspear, 
                        WeaponCategory.Nunchaku, 
                        WeaponCategory.Quarterstaff, 
                        WeaponCategory.Shortbow, 
                        WeaponCategory.Shortspear, 
                        WeaponCategory.SlingStaff, 
                        WeaponCategory.Spear, 
                        WeaponCategory.Trident,
                        WeaponCategory.LightCrossbow,
                        WeaponCategory.HeavyCrossbow
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
                    c.m_Progression = ContextRankProgression.OnePlusDivStep;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 5;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>() };
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        RavenerHunterWoodMysteryProgression.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationWoodBondFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region TreeForm
            var PlantShapeIIcon = AssetLoader.LoadInternal("Skills", "Icon_PlantShapeI.jpg");
            var PlantShapeIIIcon = AssetLoader.LoadInternal("Skills", "Icon_PlantShapeII.jpg");
            var PlantShapeIIIIcon = AssetLoader.LoadInternal("Skills", "Icon_PlantShapeIII.jpg");
            var OracleRevelationTreeFormResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationTreeFormResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    LevelIncrease = 0,
                    IncreasedByLevelStartPlusDivStep = false,
                    StartingLevel = 0,
                    StartingIncrease = 0,
                    LevelStep = 0,
                    PerStepIncrease = 0,
                    MinClassLevelIncrease = 0,
                    OtherClassesModifier = 0,
                    IncreasedByStat = false,
                    ResourceBonusStat = StatType.Unknown,
                };
            });
            var PlantShapeIBuff = Resources.GetModBlueprint<BlueprintBuff>("PlantShapeIBuff");
            var PlantShapeIIBuff = Resources.GetModBlueprint<BlueprintBuff>("PlantShapeIIBuff");
            var PlantShapeIIITreantBuff = Resources.GetModBlueprint<BlueprintBuff>("PlantShapeIIITreantBuff");
            var PlantShapeIIIGiantFlytrapBuff = Resources.GetModBlueprint<BlueprintBuff>("PlantShapeIIIGiantFlytrapBuff");
            var TreeFormIAbility = Helpers.CreateBlueprint<BlueprintAbility>("TreeFormIAbility", bp => {
                bp.SetName("Tree Form (Mandragora)");
                bp.SetDescription("You become a small mandragora. You gain a +2 size bonus to your Dexterity and Constitution and a +2 natural armor bonus. " +
                    "Your movement speed is increased by 10 feet. You also gain one 1d6 bite attack, two 1d4 slams and poison ability.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationTreeFormResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = PlantShapeIBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { PlantShapeIBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
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
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>() };
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("TreeFormIAbility.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var TreeFormIIAbility = Helpers.CreateBlueprint<BlueprintAbility>("TreeFormIIAbility", bp => {
                bp.SetName("Tree Form (Shambling Mound)");
                bp.SetDescription("You become a large shambling mound. You gain a +4 size bonus to your Strength, a +2 size bonus to your Constitution, +4 natural " +
                    "armor bonus, resist fire 20, and resist electricity 20. Your movement speed is reduced by 10 feet. You also have two 2d6 slam attacks, the constricting " +
                    "vines ability, and the poison ability.\nConstricting Vines: A shambling mound's vines coil around any creature it hits with a slam attack. The shambling " +
                    "mound attempts a grapple maneuver check against its target, and on a successful check its vines deal 2d6+5 damage and the foe is grappled.\nGrappled " +
                    "characters cannot move, and take a -2 penalty on all attack rolls and a -4 penalty to Dexterity. Grappled characters attempt to escape every round by " +
                    "making a successful combat maneuver, Strength, Athletics, or Mobility check. The DC of this check is the shambling mound's CMD.\nEach round, creatures " +
                    "grappled by a shambling mound suffer 4d6+Strength modifier × 2 damage.\nA shambling mound receives a +4 bonus on grapple maneuver checks.\nPoison:\nSlam; " +
                    "Save: Fortitude\nFrequency: 1/round for 2 rounds\nEffect: 1d2 Strength and 1d2 Dexterity damage\nCure: 1 save\nThe save DC is Constitution-based.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationTreeFormResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = PlantShapeIIBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { PlantShapeIIBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
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
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>() };
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("TreeFormIIAbility.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var TreeFormIIITreantAbility = Helpers.CreateBlueprint<BlueprintAbility>("TreeFormIIITreantAbility", bp => {
                bp.SetName("Tree Form (Treant)");
                bp.SetDescription("You become a huge treant. You gain a +8 size bonus to your Strength, +4 to Constitution, -2 penalty to Dexterity and a +6 " +
                    "natural armor bonus. You also gain two 2d6 slam attacks, damage reduction 10/slashing, vulnerability to fire and overrun ability.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationTreeFormResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = PlantShapeIIITreantBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { PlantShapeIIITreantBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
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
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>() };
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("TreeFormIIITreantAbility.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var TreeFormIIIGiantFlytrapAbility = Helpers.CreateBlueprint<BlueprintAbility>("TreeFormIIIGiantFlytrapAbility", bp => {
                bp.SetName("Tree Form (Giant Flytrap)");
                bp.SetDescription("You become a huge giant flytrap. You gain a +8 size bonus to your Strength, +4 to Constitution, -2 penalty to Dexterity and a +6 " +
                    "natural armor bonus. You also gain four 1d8 bite attacks, acid Resistance 20 and blindsight and poison ability.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationTreeFormResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = PlantShapeIIIGiantFlytrapBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { PlantShapeIIIGiantFlytrapBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<AbilityExecuteActionOnCast>(c => {
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuffsByDescriptor() {
                            NotSelf = true,
                            SpellDescriptor = SpellDescriptor.Polymorph,
                        }
                        );
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
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>() };
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "352469f228a3b1f4cb269c7ab0409b8e" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("TreeFormIIIGiantFlytrapAbility.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var TreeFormIIIAbility = Helpers.CreateBlueprint<BlueprintAbility>("TreeFormIIIAbility", bp => {
                bp.SetName("Tree Form (Final)");
                bp.SetDescription("You become a Huge Treant or a Huge Giant Flytrap.");
                bp.AddComponent<AbilityVariants>(c => {
                    c.m_Variants = new BlueprintAbilityReference[] {
                        TreeFormIIITreantAbility.ToReference<BlueprintAbilityReference>(),
                        TreeFormIIIGiantFlytrapAbility.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationTreeFormResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Polymorph;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Buff;
                });
                bp.m_Icon = PlantShapeIIIIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.CompletelyNormal | Metamagic.Extend;
                bp.LocalizedDuration = Helpers.CreateString("TreeFormIIIAbility.Duration", "1 hour/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            TreeFormIIITreantAbility.m_Parent = TreeFormIIIAbility.ToReference<BlueprintAbilityReference>();
            TreeFormIIIGiantFlytrapAbility.m_Parent = TreeFormIIIAbility.ToReference<BlueprintAbilityReference>();
            var OracleRevelationTreeFormFeature1 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationTreeFormFeature1", bp => {
                bp.SetName("Tree Form");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TreeFormIAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationTreeFormFeature2 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationTreeFormFeature2", bp => {
                bp.SetName("Tree Form");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TreeFormIIAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationTreeFormFeature3 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationTreeFormFeature3", bp => {
                bp.SetName("Tree Form");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { TreeFormIIIAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.HideInUI = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var OracleRevelationTreeFormProgression = Helpers.CreateBlueprint<BlueprintProgression>("OracleRevelationTreeFormProgression", bp => {
                bp.SetName("Tree Form");
                bp.SetDescription("You can assume the form of a mandragora, as plant shape I. At 10th level, you can assume the form of a large shambling mound, as plant shape II. " +
                    "At 12th level, you can assume the form of a Huge Treant or a Huge Giant Flytrap, as plant shape III. You can use this ability once per day, but the duration is 1 " +
                    "hour/level. You must be at least 8th level to select this revelation.");
                bp.m_Icon = PlantShapeIIIIcon;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(8, OracleRevelationTreeFormFeature1),
                    Helpers.LevelEntry(10, OracleRevelationTreeFormFeature2),
                    Helpers.LevelEntry(12, OracleRevelationTreeFormFeature3)
                };
                bp.GiveFeaturesForPreviousLevels = true;
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        RavenerHunterWoodMysteryProgression.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Group = Prerequisite.GroupType.Any;
                    c.Level = 8;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 8;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 8;
                    c.Group = Prerequisite.GroupType.Any;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationTreeFormResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationTreeFormProgression.ToReference<BlueprintFeatureReference>());
            #endregion
            #region WoodenWeaponEnchant
            var MasterWork = Resources.GetBlueprint<BlueprintWeaponEnchantment>("6b38844e2bffbac48b63036b66e735be");
            var Enhancement1 = Resources.GetBlueprint<BlueprintWeaponEnchantment>("d42fc23b92c640846ac137dc26e000d4");
            var Enhancement2 = Resources.GetBlueprint<BlueprintWeaponEnchantment>("eb2faccc4c9487d43b3575d7e77ff3f5");
            var Enhancement3 = Resources.GetBlueprint<BlueprintWeaponEnchantment>("80bb8a737579e35498177e1e3c75899b");
            var Keen = Resources.GetBlueprint<BlueprintWeaponEnchantment>("102a9c8c9b7a75e4fb5844e79deaf4c0");

            var OracleRevelationWoodenWeaponEnchantResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationWoodenWeaponEnchantResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Charisma,
                };
            });
            var OracleRevelationWoodenWeaponEnchantFlag7 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWoodenWeaponEnchantFlag7", bp => {
                bp.SetName("OracleRevelationWoodenWeaponEnchantFlag7");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var OracleRevelationWoodenWeaponEnchantFlag11 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWoodenWeaponEnchantFlag11", bp => {
                bp.SetName("OracleRevelationWoodenWeaponEnchantFlag11");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var OracleRevelationWoodenWeaponEnchantFlag15 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWoodenWeaponEnchantFlag15", bp => {
                bp.SetName("OracleRevelationWoodenWeaponEnchantFlag15");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var OracleRevelationWoodenWeaponEnchantFlag19 = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWoodenWeaponEnchantFlag19", bp => {
                bp.SetName("OracleRevelationWoodenWeaponEnchantFlag19");
                bp.SetDescription("");
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });

            var OracleRevelationWoodenWeaponEnchantBuffBaseMain = Helpers.CreateBuff("OracleRevelationWoodenWeaponEnchantBuffBaseMain", bp => {
                bp.SetName("Wooden Weapon Enchant (Main Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of wood, while enchanted the weapon is considered masterwork. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon gains the keen weapon property (or the equivalent increase to its critical threat range, if it is a bludgeoning weapon). " +
                    "This effect lasts a number of minutes equal to your oracle level. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = WoodenWeaponEnchantIcon;
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = MasterWork.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.PrimaryHand;
                });
            });
            var OracleRevelationWoodenWeaponEnchantBuffBaseOffHand = Helpers.CreateBuff("OracleRevelationWoodenWeaponEnchantBuffBaseOffHand", bp => {
                bp.SetName("Wooden Weapon Enchant (Off Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of wood, while enchanted the weapon is considered masterwork. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon gains the keen weapon property (or the equivalent increase to its critical threat range, if it is a bludgeoning weapon). " +
                    "This effect lasts a number of minutes equal to your oracle level. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = WoodenWeaponEnchantIcon;
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = MasterWork.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.SecondaryHand;
                });
            });
            var OracleRevelationWoodenWeaponEnchantBuff7Main = Helpers.CreateBuff("OracleRevelationWoodenWeaponEnchantBuff7Main", bp => {
                bp.SetName("Wooden Weapon Enchant (Main Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of wood, while enchanted the weapon is considered masterwork. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon gains the keen weapon property (or the equivalent increase to its critical threat range, if it is a bludgeoning weapon). " +
                    "This effect lasts a number of minutes equal to your oracle level. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = WoodenWeaponEnchantIcon;
            });
            var OracleRevelationWoodenWeaponEnchantBuff7OffHand = Helpers.CreateBuff("OracleRevelationWoodenWeaponEnchantBuff7OffHand", bp => {
                bp.SetName("Wooden Weapon Enchant (Off Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of wood, while enchanted the weapon is considered masterwork. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon gains the keen weapon property (or the equivalent increase to its critical threat range, if it is a bludgeoning weapon). " +
                    "This effect lasts a number of minutes equal to your oracle level. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = WoodenWeaponEnchantIcon;
            });
            var OracleRevelationWoodenWeaponEnchantBuff11Main = Helpers.CreateBuff("OracleRevelationWoodenWeaponEnchantBuff11Main", bp => {
                bp.SetName("Wooden Weapon Enchant (Main Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of wood, while enchanted the weapon is considered masterwork. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon gains the keen weapon property (or the equivalent increase to its critical threat range, if it is a bludgeoning weapon). " +
                    "This effect lasts a number of minutes equal to your oracle level. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = WoodenWeaponEnchantIcon;
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = Keen.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.PrimaryHand;
                });
            });
            var OracleRevelationWoodenWeaponEnchantBuff11OffHand = Helpers.CreateBuff("OracleRevelationWoodenWeaponEnchantBuff11OffHand", bp => {
                bp.SetName("Wooden Weapon Enchant (Off Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of wood, while enchanted the weapon is considered masterwork. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon gains the keen weapon property (or the equivalent increase to its critical threat range, if it is a bludgeoning weapon). " +
                    "This effect lasts a number of minutes equal to your oracle level. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = WoodenWeaponEnchantIcon;
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = Keen.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.SecondaryHand;
                });
            });
            var OracleRevelationWoodenWeaponEnchantBuff15Main = Helpers.CreateBuff("OracleRevelationWoodenWeaponEnchantBuff15Main", bp => {
                bp.SetName("Wooden Weapon Enchant (Main Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of wood, while enchanted the weapon is considered masterwork. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon gains the keen weapon property (or the equivalent increase to its critical threat range, if it is a bludgeoning weapon). " +
                    "This effect lasts a number of minutes equal to your oracle level. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = WoodenWeaponEnchantIcon;
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = Keen.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.PrimaryHand;
                });
            });
            var OracleRevelationWoodenWeaponEnchantBuff15OffHand = Helpers.CreateBuff("OracleRevelationWoodenWeaponEnchantBuff15OffHand", bp => {
                bp.SetName("Wooden Weapon Enchant (Off Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of wood, while enchanted the weapon is considered masterwork. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon gains the keen weapon property (or the equivalent increase to its critical threat range, if it is a bludgeoning weapon). " +
                    "This effect lasts a number of minutes equal to your oracle level. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = WoodenWeaponEnchantIcon;
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = Keen.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.SecondaryHand;
                });
            });
            var OracleRevelationWoodenWeaponEnchantBuff19Main = Helpers.CreateBuff("OracleRevelationWoodenWeaponEnchantBuff19Main", bp => {
                bp.SetName("Wooden Weapon Enchant (Main Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of wood, while enchanted the weapon is considered masterwork. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon gains the keen weapon property (or the equivalent increase to its critical threat range, if it is a bludgeoning weapon). " +
                    "This effect lasts a number of minutes equal to your oracle level. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = WoodenWeaponEnchantIcon;
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = Keen.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.PrimaryHand;
                });
            });
            var OracleRevelationWoodenWeaponEnchantBuff19OffHand = Helpers.CreateBuff("OracleRevelationWoodenWeaponEnchantBuff19OffHand", bp => {
                bp.SetName("Wooden Weapon Enchant (Off Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of wood, while enchanted the weapon is considered masterwork. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon gains the keen weapon property (or the equivalent increase to its critical threat range, if it is a bludgeoning weapon). " +
                    "This effect lasts a number of minutes equal to your oracle level. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = WoodenWeaponEnchantIcon;
                bp.AddComponent<BuffEnchantWornItem>(c => {
                    c.m_EnchantmentBlueprint = Keen.ToReference<BlueprintItemEnchantmentReference>();
                    c.Slot = EquipSlotBase.SlotType.SecondaryHand;
                });
            });

            var OracleRevelationWoodenWeaponEnchantAbilityMainHand = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationWoodenWeaponEnchantAbilityMainHand", bp => {
                bp.SetName("Wooden Weapon Enchant (Main Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of wood, while enchanted the weapon is considered masterwork. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon gains the keen weapon property (or the equivalent increase to its critical threat range, if it is a bludgeoning weapon). " +
                    "This effect lasts a number of minutes equal to your oracle level. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = WoodenWeaponEnchantIcon;
                bp.AddComponent<AbilityCasterMainWeaponCheck>(c => {
                    c.Category = new WeaponCategory[] {
                        WeaponCategory.Greatclub,
                        WeaponCategory.Club,
                        WeaponCategory.Javelin,
                        WeaponCategory.Kama,
                        WeaponCategory.Longbow,
                        WeaponCategory.Longspear,
                        WeaponCategory.Nunchaku,
                        WeaponCategory.Quarterstaff,
                        WeaponCategory.Shortbow,
                        WeaponCategory.Shortspear,
                        WeaponCategory.SlingStaff,
                        WeaponCategory.Spear,
                        WeaponCategory.Trident,
                        WeaponCategory.LightCrossbow,
                        WeaponCategory.HeavyCrossbow
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
                                        m_Fact = OracleRevelationWoodenWeaponEnchantFlag19.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleRevelationWoodenWeaponEnchantBuff19Main.ToReference<BlueprintBuffReference>(),
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
                                                m_Fact = OracleRevelationWoodenWeaponEnchantFlag15.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = OracleRevelationWoodenWeaponEnchantBuff15Main.ToReference<BlueprintBuffReference>(),
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
                                                        m_Fact = OracleRevelationWoodenWeaponEnchantFlag11.ToReference<BlueprintUnitFactReference>(),
                                                        Not = false
                                                    }
                                                }
                                            },
                                            IfTrue = Helpers.CreateActionList(
                                                new ContextActionApplyBuff() {
                                                    m_Buff = OracleRevelationWoodenWeaponEnchantBuff11Main.ToReference<BlueprintBuffReference>(),
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
                                                                m_Fact = OracleRevelationWoodenWeaponEnchantFlag7.ToReference<BlueprintUnitFactReference>(),
                                                                Not = false
                                                            }
                                                        }
                                                    },
                                                    IfTrue = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = OracleRevelationWoodenWeaponEnchantBuff7Main.ToReference<BlueprintBuffReference>(),
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
                                                            m_Buff = OracleRevelationWoodenWeaponEnchantBuffBaseMain.ToReference<BlueprintBuffReference>(),
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
                    c.m_RequiredResource = OracleRevelationWoodenWeaponEnchantResource.ToReference<BlueprintAbilityResourceReference>();
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
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>() };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = OracleRevelationWoodenWeaponEnchantFlag7.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        OracleRevelationWoodenWeaponEnchantFlag7.ToReference<BlueprintFeatureReference>(),
                        OracleRevelationWoodenWeaponEnchantFlag15.ToReference<BlueprintFeatureReference>(),
                        OracleRevelationWoodenWeaponEnchantFlag19.ToReference<BlueprintFeatureReference>()
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
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationWoodenWeaponEnchantAbilityMainHand.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationWoodenWeaponEnchantAbilityOffHand = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationWoodenWeaponEnchantAbilityOffHand", bp => {
                bp.SetName("Wooden Weapon Enchant (Off Hand)");
                bp.SetDescription("You can imbue held weapons made mostly of wood, while enchanted the weapon is considered masterwork. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon gains the keen weapon property (or the equivalent increase to its critical threat range, if it is a bludgeoning weapon). " +
                    "This effect lasts a number of minutes equal to your oracle level. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = WoodenWeaponEnchantIcon;
                bp.AddComponent<AbilityCasterOffHandWeaponCheck>(c => {
                    c.Category = new WeaponCategory[] {
                        WeaponCategory.Greatclub,
                        WeaponCategory.Club,
                        WeaponCategory.Javelin,
                        WeaponCategory.Kama,
                        WeaponCategory.Longbow,
                        WeaponCategory.Longspear,
                        WeaponCategory.Nunchaku,
                        WeaponCategory.Quarterstaff,
                        WeaponCategory.Shortbow,
                        WeaponCategory.Shortspear,
                        WeaponCategory.SlingStaff,
                        WeaponCategory.Spear,
                        WeaponCategory.Trident,
                        WeaponCategory.LightCrossbow,
                        WeaponCategory.HeavyCrossbow
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
                                        m_Fact = OracleRevelationWoodenWeaponEnchantFlag19.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleRevelationWoodenWeaponEnchantBuff19OffHand.ToReference<BlueprintBuffReference>(),
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
                                                m_Fact = OracleRevelationWoodenWeaponEnchantFlag15.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = OracleRevelationWoodenWeaponEnchantBuff15OffHand.ToReference<BlueprintBuffReference>(),
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
                                                        m_Fact = OracleRevelationWoodenWeaponEnchantFlag11.ToReference<BlueprintUnitFactReference>(),
                                                        Not = false
                                                    }
                                                }
                                            },
                                            IfTrue = Helpers.CreateActionList(
                                                new ContextActionApplyBuff() {
                                                    m_Buff = OracleRevelationWoodenWeaponEnchantBuff11OffHand.ToReference<BlueprintBuffReference>(),
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
                                                                m_Fact = OracleRevelationWoodenWeaponEnchantFlag7.ToReference<BlueprintUnitFactReference>(),
                                                                Not = false
                                                            }
                                                        }
                                                    },
                                                    IfTrue = Helpers.CreateActionList(
                                                        new ContextActionApplyBuff() {
                                                            m_Buff = OracleRevelationWoodenWeaponEnchantBuff7OffHand.ToReference<BlueprintBuffReference>(),
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
                                                            m_Buff = OracleRevelationWoodenWeaponEnchantBuffBaseOffHand.ToReference<BlueprintBuffReference>(),
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
                    c.m_RequiredResource = OracleRevelationWoodenWeaponEnchantResource.ToReference<BlueprintAbilityResourceReference>();
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
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>() };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.ProjectilesCount;
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureListRanks;
                    c.m_Feature = OracleRevelationWoodenWeaponEnchantFlag7.ToReference<BlueprintFeatureReference>();
                    c.m_FeatureList = new BlueprintFeatureReference[] {
                        OracleRevelationWoodenWeaponEnchantFlag7.ToReference<BlueprintFeatureReference>(),
                        OracleRevelationWoodenWeaponEnchantFlag15.ToReference<BlueprintFeatureReference>(),
                        OracleRevelationWoodenWeaponEnchantFlag19.ToReference<BlueprintFeatureReference>()
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
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationWoodenWeaponEnchantAbilityOffHand.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationWoodenWeaponEnchantAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationWoodenWeaponEnchantAbility", bp => {
                bp.SetName("Wooden Weapon Enchant");
                bp.SetDescription("You can imbue held weapons made mostly of wood, while enchanted the weapon is considered masterwork. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon gains the keen weapon property (or the equivalent increase to its critical threat range, if it is a bludgeoning weapon). " +
                    "This effect lasts a number of minutes equal to your oracle level. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.m_Icon = WoodenWeaponEnchantIcon;
                bp.AddComponent<AbilityVariants>(c => {
                    c.m_Variants = new BlueprintAbilityReference[] {
                        OracleRevelationWoodenWeaponEnchantAbilityMainHand.ToReference<BlueprintAbilityReference>(),
                        OracleRevelationWoodenWeaponEnchantAbilityOffHand.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationWoodenWeaponEnchantResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.EnchantWeapon;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationWoodenWeaponEnchantAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            OracleRevelationWoodenWeaponEnchantAbilityMainHand.m_Parent = OracleRevelationWoodenWeaponEnchantAbility.ToReference<BlueprintAbilityReference>();
            OracleRevelationWoodenWeaponEnchantAbilityOffHand.m_Parent = OracleRevelationWoodenWeaponEnchantAbility.ToReference<BlueprintAbilityReference>();

            var OracleRevelationWoodenWeaponEnchantFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationWoodenWeaponEnchantFeature", bp => {
                bp.SetName("Wooden Weapon Enchant");
                bp.SetDescription("You can imbue held weapons made mostly of wood, while enchanted the weapons are considered masterwork. At 7th level, 15th level, and 19th level, the weapon " +
                    "gains a +1 enhancement bonus. At 11th level, the weapon gains the keen weapon property (or the equivalent increase to its critical threat range, if it is a bludgeoning weapon). " +
                    "This effect lasts a number of minutes equal to your oracle level. You can use this ability a number of times per day equal to 3 + your Charisma modifier.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationWoodenWeaponEnchantAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        RavenerHunterWoodMysteryProgression.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                    }; 
                    c.Level = 7;
                    c.m_Feature = OracleRevelationWoodenWeaponEnchantFlag7.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    }; 
                    c.Level = 11;
                    c.m_Feature = OracleRevelationWoodenWeaponEnchantFlag11.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.Level = 15;
                    c.m_Feature = OracleRevelationWoodenWeaponEnchantFlag15.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.Level = 19;
                    c.m_Feature = OracleRevelationWoodenWeaponEnchantFlag19.ToReference<BlueprintFeatureReference>();
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
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
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
                    c.m_Resource = OracleRevelationWoodenWeaponEnchantResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationWoodenWeaponEnchantFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Lignification = Maybe patch the UnitCondition thing to stop animations
            var Incorporeal = Resources.GetBlueprint<BlueprintFeature>("c4a7f98d743bc784c9d4cf2105852c39");
            var SubtypeElemental = Resources.GetBlueprint<BlueprintFeature>("198fd8924dabcb5478d0f78bd453c586");
            var ConstructType = Resources.GetBlueprint<BlueprintFeature>("fd389783027d63343b4a5634bd81645f");
            var OracleRevelationLignificationResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationLignificationResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    },
                    StartingLevel = 1,
                    LevelStep = 14,
                    StartingIncrease = 1,
                    PerStepIncrease = 1,
                };
                bp.m_UseMax = true;
                bp.m_Max = 2;
            });
            var BalefulPolymorph = Resources.GetBlueprint<BlueprintAbility>("3105d6e9febdc3f41a08d2b7dda1fe74");
            var OracleRevelationLignificationBuff = Helpers.CreateBuff("OracleRevelationLignificationBuff", bp => {
                bp.SetName("Lignification");
                bp.SetDescription("Once per day, you can turn a creature into wood. As a standard action, you may direct your gaze against a single creature within 30 feet. " +
                    "The targeted creature must make a Fortitude save or turn into a mindless, inert statue made out of wood for a number of rounds equal to 1/2 your oracle level. " +
                    "This ability otherwise functions as a flesh to stone spell, except the target turns to wood instead of stone. This can be reversed by any effect that can reverse " +
                    "flesh to stone.");
                bp.m_Icon = LignificationIcon;
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.CantAct;
                });
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.MovementBan;
                });
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.CantMove;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.Petrified;
                });
                bp.AddComponent<AddEnergyVulnerability>(c => {
                    c.Type = DamageEnergyType.Fire;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell;
                bp.FxOnStart = new PrefabLink() { AssetId = "f684f2a037e944f4a894037c86e4194b" };
            });
            var OracleRevelationLignificationAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationLignificationAbility", bp => {
                bp.SetName("Lignification");
                bp.SetDescription("Once per day, you can turn a creature into wood. As a standard action, you may direct your gaze against a single creature within 30 feet. " +
                    "The targeted creature must make a Fortitude save or turn into a mindless, inert statue made out of wood for a number of rounds equal to 1/2 your oracle level. " +
                    "This ability otherwise functions as a flesh to stone spell, except the target turns to wood instead of stone. This can be reversed by any effect that can reverse " +
                    "flesh to stone.");
                bp.AddComponent(BalefulPolymorph.GetComponent<AbilitySpawnFx>());
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Fortitude;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionConditionalSaved() {
                            Succeed = Helpers.CreateActionList(),
                            Failed = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleRevelationLignificationBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage
                                        }
                                    },
                                    DurationSeconds = 0
                                }
                            )
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_Class = new BlueprintCharacterClassReference[] {                        
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Transmutation;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Fortitude;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Debuff;
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationLignificationResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { 
                        Incorporeal.ToReference<BlueprintUnitFactReference>(),
                        SubtypeElemental.ToReference<BlueprintUnitFactReference>(),
                        ConstructType.ToReference<BlueprintUnitFactReference>(),
                        PlantType.ToReference<BlueprintUnitFactReference>()
                    };
                    c.Inverted = true;
                });
                bp.m_Icon = LignificationIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Medium;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationLignificationAbility.Duration", "1 round/2 levels");
                bp.LocalizedSavingThrow = Helpers.CreateString("OracleRevelationLignificationAbility.SavingThrow", "Fortitude negates");
            });
            var OracleRevelationLignificationFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationLignificationFeature", bp => {
                bp.SetName("Lignification");
                bp.SetDescription("Once per day, you can turn a creature into wood. As a standard action, you may direct your gaze against a single creature within 30 feet. " +
                    "The targeted creature must make a Fortitude save or turn into a mindless, inert statue made out of wood for a number of rounds equal to 1/2 your oracle level. " +
                    "This ability otherwise functions as a flesh to stone spell, except the target turns to wood instead of stone. This can be reversed by any effect that can reverse " +
                    "flesh to stone. At 15th level, you can use this ability twice per day. You must be at least 11th level to select this revelation.");
                bp.m_Icon = LignificationIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationLignificationAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { OracleRevelationLignificationAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Charisma;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoWoodMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Group = Prerequisite.GroupType.Any;
                    c.Level = 11;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 11;
                    c.Group = Prerequisite.GroupType.Any;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationLignificationResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationLignificationFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region ThornBurst
            var OracleRevelationThornBurstResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationThornBurstResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByStat = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    StartingLevel = 5,
                    StartingIncrease = 1,
                    LevelStep = 5,
                    PerStepIncrease = 1,
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    }
                };
            });
            var OracleRevelationThornBurstAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationThornBurstAbility", bp => {
                bp.SetName("Thorn Burst");
                bp.SetDescription("As a {g|Encyclopedia:Swift_Action}swift action{/g}, you can cause sharp splinters of wood to explode outward from your body. These splinters " +
                    "deal {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage_Type}piercing damage{/g} per two oracle levels (minimum 1d6) to all creatures within a 10-foot burst. " +
                    "A {g|Encyclopedia:Saving_Throw}Reflex save{/g} halves this {g|Encyclopedia:Damage}damage{/g}. You can use this ability once per day, plus one additional time per day at " +
                    "5th level and every five levels thereafter.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Physical,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = PhysicalDamageForm.Piercing,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Fire
                            },
                            Drain = false,
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
                            Duration = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true,
                            },
                            PreRolledSharedValue = AbilitySharedValue.Damage,
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_UseMax = false;
                    c.m_Max = 0;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationThornBurstResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = new Feet() { m_Value = 10 };
                    c.m_TargetType = TargetType.Any;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker() {
                        Conditions = new Condition[] {
                            new ContextConditionIsCaster() {
                                Not = true
                            }
                        }
                    };
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new PrefabLink() { AssetId = "184fcfe5e9459cc41b7350150f3dd468" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.m_Icon = ThornBurstIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationThornBurstAbilitySpawnFx = OracleRevelationThornBurstAbility.GetComponent<AbilitySpawnFx>();
            OracleRevelationThornBurstAbilitySpawnFx.PrefabLink = OracleRevelationThornBurstAbilitySpawnFx.PrefabLink.CreateDynamicProxy(pfl => {                
                //Main.Log($"Editing: {pfl}");
                pfl.name = "ThornBurst_20feetAoE";
                Main.Log($"{FxDebug.DumpGameObject(pfl.gameObject)}");
                Object.DestroyImmediate(pfl.transform.Find("Root /stone_cast").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("Root /SnowFlakes").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("Root /StonesBig").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("Root /big_stones").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("Root /DropsWithTrail (1)").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("Root /flash").gameObject);
            });

            var OracleRevelationThornBurstFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationThornBurstFeature", bp => {
                bp.SetName("Thorn Burst");
                bp.SetDescription("As a {g|Encyclopedia:Swift_Action}swift action{/g}, you can cause sharp splinters of wood to explode outward from your body. These splinters " +
                    "deal {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage_Type}piercing damage{/g} per two oracle levels (minimum 1d6) to all creatures within a 10-foot burst. " +
                    "A {g|Encyclopedia:Saving_Throw}Reflex save{/g} halves this {g|Encyclopedia:Damage}damage{/g}. You can use this ability once per day, plus one additional time per day at " +
                    "5th level and every five levels thereafter.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationThornBurstAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoWoodMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        RavenerHunterWoodMysteryProgression.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });                
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationThornBurstResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationThornBurstFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            //Ravener Hunter Cont.
            var RavenerHunterWoodRevelationSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("RavenerHunterWoodRevelationSelection", bp => {
                bp.SetName("Wood Revelation");
                bp.SetDescription("At 1st level, the ravener hunter chooses one revelation from her chosen mystery. She must meet the revelation’s prerequisites, using her inquisitor level as her " +
                    "effective oracle level to determine the revelation’s effects, and she never qualifies for the Extra Revelation feat. The ravener hunter gains a second revelation from her " +
                    "chosen mystery at 8th level.");
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.None;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
                bp.AddFeatures(OracleRevelationWoodArmorFeature, OracleRevelationWoodBondFeature, OracleRevelationTreeFormProgression, OracleRevelationWoodenWeaponEnchantFeature,
                   OracleRevelationThornBurstFeature);
            });
            RavenerHunterWoodMysteryProgression.LevelEntries = new LevelEntry[] {
                 Helpers.LevelEntry(1, RavenerHunterWoodRevelationSelection),
                 Helpers.LevelEntry(8, RavenerHunterWoodRevelationSelection)
            };
            RavenerHunterWoodMysteryProgression.UIGroups = new UIGroup[] {
                Helpers.CreateUIGroup(RavenerHunterWoodRevelationSelection, RavenerHunterWoodRevelationSelection)
            };
            var RavenerHunterChargedByNatureSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("RavenerHunterChargedByNatureSelection");
            var SecondChargedByNatureSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("SecondChargedByNatureSelection");
            RavenerHunterChargedByNatureSelection.m_AllFeatures = RavenerHunterChargedByNatureSelection.m_AllFeatures.AppendToArray(RavenerHunterWoodMysteryProgression.ToReference<BlueprintFeatureReference>());
            SecondChargedByNatureSelection.m_AllFeatures = SecondChargedByNatureSelection.m_AllFeatures.AppendToArray(RavenerHunterWoodMysteryProgression.ToReference<BlueprintFeatureReference>());

            MysteryTools.RegisterMystery(OracleWoodMysteryFeature);
            MysteryTools.RegisterSecondMystery(OracleWoodMysteryFeature);
            MysteryTools.RegisterEnlightendPhilosopherMystery(EnlightnedPhilosopherWoodMysteryFeature);
            MysteryTools.RegisterSecondEnlightendPhilosopherMystery(EnlightnedPhilosopherWoodMysteryFeature);
            MysteryTools.RegisterHerbalistMystery(DivineHerbalistWoodMysteryFeature);
            MysteryTools.RegisterSecondHerbalistMystery(DivineHerbalistWoodMysteryFeature);
            MysteryTools.RegisterOceansEchoMystery(OceansEchoWoodMysteryFeature);
            MysteryTools.RegisterSecondOceansEchoMystery(OceansEchoWoodMysteryFeature);
            MysteryTools.RegisterHermitMystery(OracleWoodMysteryFeature);
            MysteryTools.RegisterSecondHermitMystery(OracleWoodMysteryFeature);
            MysteryTools.RegisterMysteryGiftSelection(OracleWoodMysteryFeature);
        }


        ///FX thing if I ever get round to it
        //private static void ModifyFx(GameObject AoE) {
            //UnityEngine.Object.DestroyImmediate(AoE.transform.Find("Root/stone_cast").gameObject);
            //UnityEngine.Object.DestroyImmediate(AoE.transform.Find("Root/SnowFlakes").gameObject);
            //UnityEngine.Object.DestroyImmediate(AoE.transform.Find("Root/StonesBig").gameObject);
            //UnityEngine.Object.DestroyImmediate(AoE.transform.Find("Root/bigstones").gameObject);
            //UnityEngine.Object.DestroyImmediate(AoE.transform.Find("Root/DropsWithTrail (1)").gameObject);
            //UnityEngine.Object.DestroyImmediate(AoE.transform.Find("Root/Flash").gameObject);
        //}

    }
}
