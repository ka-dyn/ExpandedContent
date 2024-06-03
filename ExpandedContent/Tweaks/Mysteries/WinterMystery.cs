using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using ExpandedContent.Extensions;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.ElementsSystem;
using Kingmaker.Enums;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using UnityEngine;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using ExpandedContent.Tweaks.Classes;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Designers.TempMapCode.Ambush;
using Kingmaker.Visual.HitSystem;
using Kingmaker.Visual.Sound;
using static TabletopTweaks.Core.MechanicsChanges.AdditionalModifierDescriptors;
using Kingmaker.AI.Blueprints;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.Facts;
using System.Collections.Generic;
using Kingmaker.AI.Blueprints.Considerations;
using Kingmaker.Craft;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;



namespace ExpandedContent.Tweaks.Mysteries {
    internal class WinterMystery {
        public static void AddWinterMystery() {

            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var OracleRevelationSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("60008a10ad7ad6543b1f63016741a5d2");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");
            var WinterMysteryIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleWinterMystery.png");

            //Spelllist
            var SnowballSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("9f10909f0be1f5141bf1c102041f93d9");
            var WinterGraspSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("406c6e4a631b43ce8f7a77844b75bf75");//Replace is CO+ is installed
            var VengefulCometsSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("0e1272506f9f4480b7c3e7e1e53b6439");
            var IceStormSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fcb028205a71ee64d98175ff39a0abf9");
            var IcyPrisonSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("65e8d23aef5e7784dbeb27b1fca40931");
            var ConeOfColdSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e7c530f8137630f4d9d7ee1aa7b1edc0");
            var IceBodySpell = Resources.GetBlueprint<BlueprintAbility>("89778dc261fe6094bb2445cb389842d2");
            var PolarRaySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("17696c144a0194c478cbe402b496cb23");
            var MassIcyPrisonSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("1852a9393a23d5741b650a1ea7078abc");
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
            var OracleWinterSpells = Helpers.CreateBlueprint<BlueprintFeature>("OracleWinterSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SnowballSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = WinterGraspSpell;//Replace is CO+ is installed
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = VengefulCometsSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = IceStormSpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = IcyPrisonSpell;
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ConeOfColdSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = IceBodySpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = PolarRaySpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MassIcyPrisonSpell;
                    c.SpellLevel = 9;
                });
            });
            var EnlightnedPhilosopherWinterSpells = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherWinterSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SnowballSpell;
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
            var OceansEchoWinterSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoWinterSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SnowballSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = VengefulCometsSpell;
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
                    c.m_Spell = PolarRaySpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MassIcyPrisonSpell;
                    c.SpellLevel = 9;
                });
            });
            //Final Revelation
            var IceBodyBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("a6da7d6a5c9377047a7bd2680912860f");
            var OracleWinterFinalRevelationAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("OracleWinterFinalRevelationAbility", bp => {
                bp.SetName("Avatar of Winter");
                bp.SetDescription("Your body may transform into living ice, as the ice body spell at will. \nIce Body: You gain {g|Encyclopedia:Energy_Immunity}immunity{/g} " +
                    "to cold, {g|Encyclopedia:Energy_Vulnerability}vulnerability{/g} to fire and {g|Encyclopedia:Damage_Reduction}damage reduction{/g} 5/magic. You are " +
                    "immune to {g|Encyclopedia:AbilityDamage}ability score damage{/g}, {g|ConditionBlind}blindness{/g}, {g|Encyclopedia:Critical}critical hits{/g}, " +
                    "disease, drowning, electricity, poison, stunning, and all {g|Encyclopedia:Spell}spells{/g} or {g|Encyclopedia:Attack}attacks{/g} that affect your " +
                    "physiology or respiration, because you have no physiology or respiration while this spell is in effect. You cannot drink (and thus can't use potions). " +
                    "\nYour {g|Encyclopedia:UnarmedAttack}unarmed attack{/g} deals {g|Encyclopedia:Damage}damage{/g} equal to a club sized for you ({g|Encyclopedia:Dice}1d4{/g} " +
                    "for Small characters or 1d6 for Medium characters) plus 1 point of {g|Encyclopedia:Energy_Damage}cold damage{/g}, and you are considered armed when making " +
                    "unarmed attacks.");
                bp.m_Icon = IceBodySpell.Icon;
                bp.m_Buff = IceBodyBuff;
                bp.IsOnByDefault = true;
                bp.DeactivateIfCombatEnded = false;
                bp.DeactivateImmediately = true;
                bp.DeactivateIfOwnerDisabled = false;
                bp.DeactivateIfOwnerUnconscious = false;
                bp.OnlyInCombat = false;
                bp.DoNotTurnOffOnRest = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
                bp.m_ActivateOnUnitAction = AbilityActivateOnUnitActionType.Attack;
            });
            var OracleWinterFinalRevelation = Helpers.CreateBlueprint<BlueprintFeature>("OracleWinterFinalRevelation", bp => {
                bp.SetName("Final Revelation");
                bp.SetDescription("Upon reaching 20th level, you become an avatar of winter and the North. Your body may transform into living ice, " +
                    "as the ice body spell at will. In addition, your mastery of winter magic is such that any of your attacks that deal cold damage " +
                    "bypass cold immunity or cold resistance.");
                bp.AddComponent<AscendantElement>(c => {
                    c.Element = DamageEnergyType.Cold;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleWinterFinalRevelationAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.IsClassFeature = true;
            });
            //Main Mystery Feature
            var OracleWinterMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleWinterMysteryFeature", bp => {
                bp.m_Icon = WinterMysteryIcon;
                bp.SetName("Winter");
                bp.SetDescription("An oracle with the winter mystery adds {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Stealth}Stealth{/g}, " +
                    "{g|Encyclopedia:Lore_Nature}Lore (nature){/g} and {g|Encyclopedia:Knowledge_World}Knowledge (World) {/g} to " +
                    "her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.Level = 20;
                    c.m_Feature = OracleWinterFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleWinterSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
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
            var EnlightnedPhilosopherWinterMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherWinterMysteryFeature", bp => {
                bp.m_Icon = WinterMysteryIcon;
                bp.SetName("Winter");
                bp.SetDescription("An oracle with the winter mystery adds {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Stealth}Stealth{/g}, " +
                    "{g|Encyclopedia:Lore_Nature}Lore (nature){/g} and {g|Encyclopedia:Knowledge_World}Knowledge (World) {/g} to " +
                    "her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = EnlightnedPhilosopherWinterSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
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
            var DivineHerbalistWinterMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("DivineHerbalistWinterMysteryFeature", bp => {
                bp.m_Icon = WinterMysteryIcon;
                bp.SetName("Winter");
                bp.SetDescription("Gain access to the spells and revelations of the winter mystery. \nDue to the divine herbalist archetype the class skills gained from this archetype" +
                    "are replaced by the master herbalist feature and the brew potions feat.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleWinterFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleWinterSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.DivineHerbalistMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //Ocean's Echo
            var OceansEchoWinterMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoWinterMysteryFeature", bp => {
                bp.m_Icon = WinterMysteryIcon;
                bp.SetName("Winter");
                bp.SetDescription("Gain access to the spells and revelations of the winter mystery. \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleWinterFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoWinterSpells.ToReference<BlueprintFeatureReference>();
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
            //Blizzard
            var Blizzard = Resources.GetBlueprint<BlueprintFeature>("d518226e0f83aaf40aed6466d0ab3fb0").GetComponent<PrerequisiteFeaturesFromList>();
            Blizzard.m_Features = Blizzard.m_Features.AppendToArray(OracleWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            Blizzard.m_Features = Blizzard.m_Features.AppendToArray(EnlightnedPhilosopherWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            Blizzard.m_Features = Blizzard.m_Features.AppendToArray(DivineHerbalistWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            Blizzard.m_Features = Blizzard.m_Features.AppendToArray(OceansEchoWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            //FreezingSpells
            var FreezingSpells = Resources.GetBlueprint<BlueprintFeature>("bc2f6769fe042834db7120e3c8a50b47").GetComponent<PrerequisiteFeaturesFromList>();
            FreezingSpells.m_Features = FreezingSpells.m_Features.AppendToArray(OracleWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            FreezingSpells.m_Features = FreezingSpells.m_Features.AppendToArray(EnlightnedPhilosopherWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            FreezingSpells.m_Features = FreezingSpells.m_Features.AppendToArray(DivineHerbalistWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            FreezingSpells.m_Features = FreezingSpells.m_Features.AppendToArray(OceansEchoWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            //IceArmor
            var IceArmor = Resources.GetBlueprint<BlueprintFeature>("a1cd9835c6699534ca124fab239fdf1c").GetComponent<PrerequisiteFeaturesFromList>();
            IceArmor.m_Features = IceArmor.m_Features.AppendToArray(OracleWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            IceArmor.m_Features = IceArmor.m_Features.AppendToArray(EnlightnedPhilosopherWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            IceArmor.m_Features = IceArmor.m_Features.AppendToArray(DivineHerbalistWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            IceArmor.m_Features = IceArmor.m_Features.AppendToArray(OceansEchoWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            //IceSkin
            var IceSkin = Resources.GetBlueprint<BlueprintFeature>("cdeba08f8137cb141a9aa2f6fe55f99c").GetComponent<PrerequisiteFeaturesFromList>();
            IceSkin.m_Features = IceSkin.m_Features.AppendToArray(OracleWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            IceSkin.m_Features = IceSkin.m_Features.AppendToArray(EnlightnedPhilosopherWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            IceSkin.m_Features = IceSkin.m_Features.AppendToArray(DivineHerbalistWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            IceSkin.m_Features = IceSkin.m_Features.AppendToArray(OceansEchoWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            //WintryTouch
            var WintryTouch = Resources.GetBlueprint<BlueprintFeature>("a63e315828427a54492724e06c0bd969").GetComponent<PrerequisiteFeaturesFromList>();
            WintryTouch.m_Features = WintryTouch.m_Features.AppendToArray(OracleWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            WintryTouch.m_Features = WintryTouch.m_Features.AppendToArray(EnlightnedPhilosopherWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            WintryTouch.m_Features = WintryTouch.m_Features.AppendToArray(DivineHerbalistWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            WintryTouch.m_Features = WintryTouch.m_Features.AppendToArray(OceansEchoWinterMysteryFeature.ToReference<BlueprintFeatureReference>());
            //ColdAura
            var OracleRevelationColdAuraResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationColdAuraResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,                    
                    StartingLevel = 5,
                    LevelStep = 5,
                    StartingIncrease = 1,
                    PerStepIncrease = 1,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                    }
                };
            });
            var CycloneBlizzardBlastAbility = Resources.GetBlueprint<BlueprintAbility>("cca552f27c6ea4f458858fb857212df7");
            var OracleRevelationColdAuraBuff = Helpers.CreateBuff("OracleRevelationColdAuraBuff", bp => {//may give an FX later
                bp.SetName("Cold Aura");
                bp.SetDescription("A flurry of snow momentarily surrounds you, granting you 20% {g|Encyclopedia:Concealment}concealment{/g} until your next turn");
                bp.m_Icon = CycloneBlizzardBlastAbility.Icon;
                bp.AddComponent<AddConcealment>(c => {
                    c.Descriptor = ConcealmentDescriptor.Blur;
                    c.Concealment = Concealment.Partial;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.CheckDistance = false;
                    c.DistanceGreater = 0.Feet();
                    c.OnlyForAttacks = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = new PrefabLink() { AssetId = "4828572a4d3cd3547bf5ff2e9e62ee1d" };
            });
            OracleRevelationColdAuraBuff.FxOnStart = OracleRevelationColdAuraBuff.FxOnStart.CreateDynamicProxy(pfl => {
                Main.Log($"Editing: {pfl}");
                pfl.name = "ColdAura_10feetAoE";
                Main.Log($"{FxDebug.DumpGameObject(pfl.gameObject)}");
                Object.DestroyImmediate(pfl.transform.Find("Ring (1)/GameObject/Ring1/Rotator/FireTrailParticles (1)").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("Ring (1)/GameObject/Ring1/Rotator/Glow (2)").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("Ring (1)/GameObject/Ring1/Rotator/Warhead (1)").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("Ring (1)/GameObject/Ring2/Rotator (1)/FireTrailParticles (2)").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("Ring (1)/GameObject/Ring2/Rotator (1)/Glow (3)").gameObject);
                Object.DestroyImmediate(pfl.transform.Find("Ring (1)/GameObject/Ring2/Rotator (1)/Warhead (2)").gameObject);
            });
            var OracleRevelationColdAuraAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationColdAuraAbility", bp => {
                bp.SetName("Cold Aura");
                bp.SetDescription("As a swift action, you can cause waves of cold to radiate from your body. This cold deals 1d6 points of cold damage " +
                    "per 2 oracle levels to all creatures within 10 feet. A successful Fortitude save halves the damage. In addition, a flurry of snow " +
                    "momentarily surrounds you, granting you 20% {g|Encyclopedia:Concealment}concealment{/g} until your next turn. You can use this ability once per day, plus one additional " +
                    "time per day at 5th level and every 5 levels thereafter.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Fortitude;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
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
                                Energy = DamageEnergyType.Cold
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
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleRevelationColdAuraBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 1
                                    },
                                    DurationSeconds = 0
                                }
                                )
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
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationColdAuraResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = new Feet() { m_Value = 10 };
                    c.m_TargetType = Kingmaker.UnitLogic.Abilities.Components.TargetType.Any;
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
                    c.PrefabLink = new PrefabLink() { AssetId = "e05061bbc743af545b923c88662c9e65" };//CycloneBlizzardBlast
                    c.Time = AbilitySpawnFxTime.OnStart;
                    c.Anchor = AbilitySpawnFxAnchor.Caster;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.m_Icon = CycloneBlizzardBlastAbility.Icon;
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
            var OracleRevelationColdAuraFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationColdAuraFeature", bp => {
                bp.SetName("Cold Aura");
                bp.SetDescription("As a swift action, you can cause waves of cold to radiate from your body. This cold deals 1d6 points of cold damage " +
                    "per 2 oracle levels to all creatures within 10 feet. A successful Fortitude save halves the damage. In addition, a flurry of snow " +
                    "momentarily surrounds you, granting you 20% {g|Encyclopedia:Concealment}concealment{/g} until your next turn. You can use this ability once per day, plus one additional " +
                    "time per day at 5th level and every 5 levels thereafter.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationColdAuraAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleWinterMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherWinterMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistWinterMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoWinterMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationColdAuraResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationColdAuraFeature.ToReference<BlueprintFeatureReference>());
            //ServantOfWinter
            var OracleRevelationServantOfWinterResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationServantOfWinterResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    StartingLevel = 7,
                    LevelStep = 8,
                    StartingIncrease = 1,
                    PerStepIncrease = 1,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    }
                };
            });
            var BlizzardServantUnit = Resources.GetBlueprint<BlueprintUnit>("287a354894264478b6190632380e85f4");
            var OutsiderClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("92ab5f2fe00631b44810deffcc1a97fd");
            var Unlootable = Resources.GetBlueprintReference<BlueprintBuffReference>("0f775c7d5d8b6494197e1ce937754482");
            var ElementalAirBarks = Resources.GetBlueprintReference<BlueprintUnitAsksListReference>("19b7afaf9f430c545b1fd99aafd8af17");
            var ElementalAirPortait = Resources.GetBlueprintReference<BlueprintPortraitReference>("9a3ad3d058da4d27b1b55d7ee3144157"); 
            var ElementalAirType = Resources.GetBlueprintReference<BlueprintUnitTypeReference>("16f736b21b43e12449012844ea5f9ca1");
            var Summoned = Resources.GetBlueprintReference<BlueprintFactionReference>("1b08d9ed04518ec46a9b3e4e23cb5105");
            var SubtypeAir = Resources.GetBlueprint<BlueprintFeature>("dd3d0c7f4f57f304cbdbb68170b1b775");
            var SubtypeElemental = Resources.GetBlueprint<BlueprintFeature>("198fd8924dabcb5478d0f78bd453c586");
            var SubtypeExtraplanar = Resources.GetBlueprint<BlueprintFeature>("136fa0343d5b4b348bdaa05d83408db3");
            var Airborne = Resources.GetBlueprint<BlueprintFeature>("70cffb448c132fa409e49156d013b175");
            var AirMastery = Resources.GetBlueprint<BlueprintFeature>("be52ced7ae1c7354a8ee12d9bad47805");
            var TripImmune = Resources.GetBlueprint<BlueprintFeature>("c1b26f97b974aec469613f968439e7bb");
            var BlizzardImmunity = Resources.GetBlueprint<BlueprintFeature>("c2106ae7120249b0858fc191846b6d37");
            var BlizzardServantFXBuff = Resources.GetBlueprint<BlueprintBuff>("c1171cdd25474a3d86fbb1e54acfd034");
            var ColdResistance10 = Resources.GetBlueprint<BlueprintFeature>("daf27e1f12e736d4294b525489e99de4");
            var ColdResistance20 = Resources.GetBlueprint<BlueprintFeature>("9a50f9d13b7d829419b8d129b21e99e5");
            var ColdResistance30 = Resources.GetBlueprint<BlueprintFeature>("317b2de0512c81d47bb7895e44eddc60");
            var NotADemonFeature = Resources.GetBlueprint<BlueprintFeature>("e2986f96fa1cd3b4f8d9dfd8a9907731");
            var NaturalArmor3 = Resources.GetBlueprint<BlueprintUnitFact>("f6e106931f95fec4eb995f0d0629fb84");
            var NaturalArmor4 = Resources.GetBlueprint<BlueprintUnitFact>("16fc201a83edcde4cbd64c291ebe0d07");
            var NaturalArmor8 = Resources.GetBlueprint<BlueprintUnitFact>("b9342e2a6dc5165489ba3412c50ca3d1");
            var AttackAiAction = Resources.GetBlueprint<BlueprintAiAttack>("866ffa6c34000cd4a86fb1671f86c7d8");
            var AirElementalFollowEnemyAiAction = Resources.GetBlueprint<BlueprintAiFollow>("565c49da5aa97274a9e93bb7c2a6868e");
            var ServantOfWinterSummonPool = Helpers.CreateBlueprint<BlueprintSummonPool>("ServantOfWinterSummonPool", bp => {
                bp.Limit = 0;
                bp.DoNotRemoveDeadUnits = false;
            });
            var BlizzardServantSpike1d6 = Resources.GetBlueprintReference<BlueprintItemWeaponReference>("a5563e20044d40118c41707485780432");
            var BlizzardElementalSpike2d6 = Resources.GetBlueprint<BlueprintItemWeapon>("821e2a36aef04627b7a21e4a42fe477e");
            var BlizzardElementalSpike3d6 = Helpers.CreateBlueprint<BlueprintItemWeapon>("BlizzardElementalSpike3d6", bp => {
                bp.m_DisplayNameText = BlizzardElementalSpike2d6.m_DisplayNameText;
                bp.m_Icon = BlizzardElementalSpike2d6.m_Icon;
                bp.m_Cost = 0;
                bp.m_Weight = 0;
                bp.m_IsNotable = false;
                bp.m_IsJunk = false;
                bp.m_ForceStackable = false;
                bp.m_Destructible = false;
                bp.m_ShardItem = null;
                bp.m_MiscellaneousType = Kingmaker.Blueprints.Items.BlueprintItem.MiscellaneousItemType.None;
                bp.m_InventoryPutSound = BlizzardElementalSpike2d6.m_InventoryPutSound;
                bp.m_InventoryTakeSound = BlizzardElementalSpike2d6.m_InventoryTakeSound;
                bp.NeedSkinningForCollect = false;
                bp.TrashLootTypes = BlizzardElementalSpike2d6.TrashLootTypes;
                bp.CR = 0;
                bp.SpendCharges = true;
                bp.Charges = 1;
                bp.RestoreChargesOnRest = false;
                bp.CasterLevel = 1;
                bp.SpellLevel = 1;
                bp.DC = 11;
                bp.HideAbilityInfo = false;
                bp.IsNonRemovable = false;
                bp.m_EquipmentEntity = new KingmakerEquipmentEntityReference();
                bp.m_EquipmentEntityAlternatives = new KingmakerEquipmentEntityReference[0];
                bp.m_ForcedRampColorPresetIndex = 0;
                bp.m_VisualParameters = BlizzardElementalSpike2d6.m_VisualParameters;
                bp.m_Type = BlizzardElementalSpike2d6.m_Type;
                bp.m_Size = Size.Medium;
                bp.m_Enchantments = BlizzardElementalSpike2d6.m_Enchantments;
                bp.m_OverrideDamageDice = true;
                bp.m_DamageDice = new DiceFormula() {
                    m_Rolls = 3,
                    m_Dice = DiceType.D6
                };
                bp.m_OverrideDamageType = false;
                bp.m_DamageType = BlizzardElementalSpike2d6.m_DamageType;
                bp.CountAsDouble = false;
                bp.Double = false;
                bp.m_SecondWeapon = null;
                bp.KeepInPolymorph = false;
                bp.m_OverrideShardItem = false;
                bp.m_OverrideDestructible = false;
                bp.m_AlwaysPrimary = false;
                bp.m_IsCanChangeVisualOverriden = false;
                bp.m_CanChangeVisual = false;
            });
            var AirElementalInWhirlind = Resources.GetBlueprintReference<BlueprintBuffReference>("8b1b723a20f644c469b99bd541a13a3b");
            var AirElementalMediumWhirlwindAbility = Resources.GetBlueprint<BlueprintAbility>("1e6e67c961c493243a2077a0dc9a73df");
            var SummonedBlizzardElementalWhirlwindDebuff = Helpers.CreateBuff("SummonedBlizzardElementalWhirlwindDebuff", bp => {
                bp.SetName("Whirlwind of Hail");
                bp.SetDescription("Some creatures can transform themselves into whirlwinds and remain in that form for up to 1 {g|Encyclopedia:Combat_Round}round{/g} " +
                    "for every 2 {g|Encyclopedia:Hit_Dice}HD{/g} they have. A creature in whirlwind form cannot make its normal {g|Encyclopedia:Attack}attacks{/g} and " +
                    "does not {g|Encyclopedia:Threatened_Area}threaten{/g} the area around it. Creatures caught in the whirlwind take a –4 {g|Encyclopedia:Penalty}penalty{/g} " +
                    "to {g|Encyclopedia:Dexterity}Dexterity{/g} and a –2 penalty on attack rolls and must succeed on a {g|Encyclopedia:Concentration}concentration check{/g} ({g|Encyclopedia:DC}DC{/g} " +
                    "15 + {g|Encyclopedia:Spell}spell{/g} level) to cast a spell. An affected creature must succeed on a {g|Encyclopedia:Saving_Throw}Reflex save{/g} (DC 10 + half " +
                    "monster's HD + the monster's {g|Encyclopedia:Strength}Strength{/g} modifier) each round or take {g|Encyclopedia:Damage}damage{/g} as if it were hit by the creatures spike attack, " +
                    "plus the same amount of damage dice as cold damage.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Penalty;
                    c.Stat = StatType.Dexterity;
                    c.Value = -4;
                    c.ScaleByBasicAttackBonus = false;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.Penalty;
                    c.Stat = StatType.AdditionalAttackBonus;
                    c.Value = -2;
                    c.ScaleByBasicAttackBonus = false;
                });
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = Kingmaker.UnitLogic.UnitCondition.SpellCastingIsDifficult;
                });
                bp.m_Icon = AirElementalMediumWhirlwindAbility.Icon;
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Stack;
                bp.Ranks = 0;
                bp.TickEachSecond = false;
                bp.Frequency = DurationRate.Rounds;
            });
            var SummonedBlizzardElementalMediumWhirlwindArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("SummonedBlizzardElementalMediumWhirlwindArea", bp => {
                bp.AddComponent<AbilityAreaEffectRunAction>(c => {
                    c.UnitEnter = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {                                    
                                    new ContextConditionIsCaster() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedBlizzardElementalWhirlwindDebuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    DurationSeconds = 0,
                                    IsFromSpell = false,
                                    IsNotDispelable = false,
                                    ToCaster = false,
                                    AsChild = true,
                                    SameDuration = false
                                }
                                )
                        }
                        );
                    c.UnitExit = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = SummonedBlizzardElementalWhirlwindDebuff.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        }
                        );
                    c.UnitMove = Helpers.CreateActionList();
                    c.Round = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionIsCaster() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionSavingThrow() {
                                    Type = SavingThrowType.Reflex,
                                    FromBuff = false,
                                    HasCustomDC = false,
                                    CustomDC = new ContextValue(),
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionConditionalSaved() {
                                            Succeed = Helpers.CreateActionList(),
                                            Failed = Helpers.CreateActionList(
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
                                                            Form = PhysicalDamageForm.Bludgeoning,
                                                            Enhancement = 0,
                                                            EnhancementTotal = 0
                                                        },
                                                        Energy = DamageEnergyType.NegativeEnergy
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
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 1,
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
                                                        },
                                                    },
                                                    IsAoE = false,
                                                    HalfIfSaved = false,
                                                    UseMinHPAfterDamage = false,
                                                    MinHPAfterDamage = 0,
                                                    ResultSharedValue = AbilitySharedValue.Damage,
                                                    CriticalSharedValue = AbilitySharedValue.Damage
                                                },
                                                new ContextActionDealDamage() {
                                                    m_Type = ContextActionDealDamage.Type.Damage,
                                                    DamageType = new DamageTypeDescription() {
                                                        Type = DamageType.Energy,
                                                        Common = new DamageTypeDescription.CommomData() {
                                                            Reality = 0,
                                                            Alignment = 0,
                                                            Precision = false
                                                        },
                                                        Physical = new DamageTypeDescription.PhysicalData() {
                                                            Material = 0,
                                                            Form = PhysicalDamageForm.Bludgeoning,
                                                            Enhancement = 0,
                                                            EnhancementTotal = 0
                                                        },
                                                        Energy = DamageEnergyType.Cold
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
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 1,
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
                                                    IsAoE = false,
                                                    HalfIfSaved = false,
                                                    UseMinHPAfterDamage = false,
                                                    MinHPAfterDamage = 0,
                                                    ResultSharedValue = AbilitySharedValue.Damage,
                                                    CriticalSharedValue = AbilitySharedValue.Damage
                                                }
                                                )
                                        }
                                        )
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Strength;
                    c.m_Progression = ContextRankProgression.BonusValue;
                });
                bp.m_AllowNonContextActions = false;
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Any;
                bp.m_Tags = AreaEffectTags.None;
                bp.SpellResistance = false;
                bp.AffectEnemies = true;
                bp.AggroEnemies = true;
                bp.AffectDead = false;
                bp.IgnoreSleepingUnits = false;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = new Feet() { m_Value = 20 };
                bp.Fx = new PrefabLink() { AssetId = "ce1b2aff16a040145bc24cfc388783d3" };
            });
            var SummonedBlizzardElementalHugeWhirlwindArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("SummonedBlizzardElementalHugeWhirlwindArea", bp => {
                bp.AddComponent<AbilityAreaEffectRunAction>(c => {
                    c.UnitEnter = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionIsCaster() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedBlizzardElementalWhirlwindDebuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    DurationSeconds = 0,
                                    IsFromSpell = false,
                                    IsNotDispelable = false,
                                    ToCaster = false,
                                    AsChild = true,
                                    SameDuration = false
                                }
                                )
                        }
                        );
                    c.UnitExit = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = SummonedBlizzardElementalWhirlwindDebuff.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        }
                        );
                    c.UnitMove = Helpers.CreateActionList();
                    c.Round = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionIsCaster() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionSavingThrow() {
                                    Type = SavingThrowType.Reflex,
                                    FromBuff = false,
                                    HasCustomDC = false,
                                    CustomDC = new ContextValue(),
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionConditionalSaved() {
                                            Succeed = Helpers.CreateActionList(),
                                            Failed = Helpers.CreateActionList(
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
                                                            Form = PhysicalDamageForm.Bludgeoning,
                                                            Enhancement = 0,
                                                            EnhancementTotal = 0
                                                        },
                                                        Energy = DamageEnergyType.NegativeEnergy
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
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 2,
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
                                                        },
                                                    },
                                                    IsAoE = false,
                                                    HalfIfSaved = false,
                                                    UseMinHPAfterDamage = false,
                                                    MinHPAfterDamage = 0,
                                                    ResultSharedValue = AbilitySharedValue.Damage,
                                                    CriticalSharedValue = AbilitySharedValue.Damage
                                                },
                                                new ContextActionDealDamage() {
                                                    m_Type = ContextActionDealDamage.Type.Damage,
                                                    DamageType = new DamageTypeDescription() {
                                                        Type = DamageType.Energy,
                                                        Common = new DamageTypeDescription.CommomData() {
                                                            Reality = 0,
                                                            Alignment = 0,
                                                            Precision = false
                                                        },
                                                        Physical = new DamageTypeDescription.PhysicalData() {
                                                            Material = 0,
                                                            Form = PhysicalDamageForm.Bludgeoning,
                                                            Enhancement = 0,
                                                            EnhancementTotal = 0
                                                        },
                                                        Energy = DamageEnergyType.Cold
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
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 2,
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
                                                    IsAoE = false,
                                                    HalfIfSaved = false,
                                                    UseMinHPAfterDamage = false,
                                                    MinHPAfterDamage = 0,
                                                    ResultSharedValue = AbilitySharedValue.Damage,
                                                    CriticalSharedValue = AbilitySharedValue.Damage
                                                }
                                                )
                                        }
                                        )
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Strength;
                    c.m_Progression = ContextRankProgression.BonusValue;
                });
                bp.m_AllowNonContextActions = false;
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Any;
                bp.m_Tags = AreaEffectTags.None;
                bp.SpellResistance = false;
                bp.AffectEnemies = true;
                bp.AggroEnemies = true;
                bp.AffectDead = false;
                bp.IgnoreSleepingUnits = false;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = new Feet() { m_Value = 20 };
                bp.Fx = new PrefabLink() { AssetId = "ce1b2aff16a040145bc24cfc388783d3" };
            });
            var SummonedBlizzardElementalElderWhirlwindArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("SummonedBlizzardElementalElderWhirlwindArea", bp => {
                bp.AddComponent<AbilityAreaEffectRunAction>(c => {
                    c.UnitEnter = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionIsCaster() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = SummonedBlizzardElementalWhirlwindDebuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 0
                                    },
                                    DurationSeconds = 0,
                                    IsFromSpell = false,
                                    IsNotDispelable = false,
                                    ToCaster = false,
                                    AsChild = true,
                                    SameDuration = false
                                }
                                )
                        }
                        );
                    c.UnitExit = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = SummonedBlizzardElementalWhirlwindDebuff.ToReference<BlueprintBuffReference>(),
                            RemoveRank = false,
                            ToCaster = false
                        }
                        );
                    c.UnitMove = Helpers.CreateActionList();
                    c.Round = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionIsCaster() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(),
                            IfFalse = Helpers.CreateActionList(
                                new ContextActionSavingThrow() {
                                    Type = SavingThrowType.Reflex,
                                    FromBuff = false,
                                    HasCustomDC = false,
                                    CustomDC = new ContextValue(),
                                    Actions = Helpers.CreateActionList(
                                        new ContextActionConditionalSaved() {
                                            Succeed = Helpers.CreateActionList(),
                                            Failed = Helpers.CreateActionList(
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
                                                            Form = PhysicalDamageForm.Bludgeoning,
                                                            Enhancement = 0,
                                                            EnhancementTotal = 0
                                                        },
                                                        Energy = DamageEnergyType.NegativeEnergy
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
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 3,
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
                                                        },
                                                    },
                                                    IsAoE = false,
                                                    HalfIfSaved = false,
                                                    UseMinHPAfterDamage = false,
                                                    MinHPAfterDamage = 0,
                                                    ResultSharedValue = AbilitySharedValue.Damage,
                                                    CriticalSharedValue = AbilitySharedValue.Damage
                                                },
                                                new ContextActionDealDamage() {
                                                    m_Type = ContextActionDealDamage.Type.Damage,
                                                    DamageType = new DamageTypeDescription() {
                                                        Type = DamageType.Energy,
                                                        Common = new DamageTypeDescription.CommomData() {
                                                            Reality = 0,
                                                            Alignment = 0,
                                                            Precision = false
                                                        },
                                                        Physical = new DamageTypeDescription.PhysicalData() {
                                                            Material = 0,
                                                            Form = PhysicalDamageForm.Bludgeoning,
                                                            Enhancement = 0,
                                                            EnhancementTotal = 0
                                                        },
                                                        Energy = DamageEnergyType.Cold
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
                                                            ValueType = ContextValueType.Simple,
                                                            Value = 3,
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
                                                    IsAoE = false,
                                                    HalfIfSaved = false,
                                                    UseMinHPAfterDamage = false,
                                                    MinHPAfterDamage = 0,
                                                    ResultSharedValue = AbilitySharedValue.Damage,
                                                    CriticalSharedValue = AbilitySharedValue.Damage
                                                }
                                                )
                                        }
                                        )
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Strength;
                    c.m_Progression = ContextRankProgression.BonusValue;
                });
                bp.m_AllowNonContextActions = false;
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Any;
                bp.m_Tags = AreaEffectTags.None;
                bp.SpellResistance = false;
                bp.AffectEnemies = true;
                bp.AggroEnemies = true;
                bp.AffectDead = false;
                bp.IgnoreSleepingUnits = false;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = new Feet() { m_Value = 20 };
                bp.Fx = new PrefabLink() { AssetId = "ce1b2aff16a040145bc24cfc388783d3" };
            });
            var NumbingColdFeature = Resources.GetBlueprint<BlueprintFeature>("3c91012b5a084c8c833a3afb6a138d92");
            var Staggered = Resources.GetBlueprintReference<BlueprintBuffReference>("df3950af5a783bd4d91ab73eb8fa0fd3");
            var SummonedNumbingColdDCProperty = Helpers.CreateBlueprint<BlueprintUnitProperty>("SummonedNumbingColdDCProperty", bp => {
                bp.AddComponent<ClassLevelGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.Div2
                    };
                    c.m_Class = OutsiderClass;
                });
                bp.AddComponent<StatValueGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs
                    };
                    c.Stat = StatType.Constitution;
                    c.ValueType = StatValueGetter.ReturnType.Bonus;
                });
                bp.BaseValue = 10;
                bp.OperationOnComponents = BlueprintUnitProperty.MathOperation.Sum;
            });
            var SummonedBlizzardElementalNumbingColdFeature = Helpers.CreateBlueprint<BlueprintFeature>("SummonedBlizzardElementalNumbingColdFeature", bp => {
                bp.m_DisplayName = NumbingColdFeature.m_DisplayName;
                bp.m_Description = NumbingColdFeature.m_Description;
                bp.AddComponent<AddOutgoingDamageTrigger>(c => {
                    c.RunInReasonContext = false;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionSavingThrow() {
                            Type = SavingThrowType.Fortitude,
                            m_ConditionalDCIncrease = new ContextActionSavingThrow.ConditionalDCIncrease[0],
                            UseDCFromContextSavingThrow = false,
                            HasCustomDC = false,
                            CustomDC = new ContextValue(),
                            Actions = Helpers.CreateActionList(
                                new ContextActionConditionalSaved() {
                                    Succeed = Helpers.CreateActionList(),
                                    Failed = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = Staggered,
                                            Permanent = false,
                                            UseDurationSeconds = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = 1,
                                                m_IsExtendable = true
                                            },
                                            IsFromSpell = false,
                                            IsNotDispelable = false,
                                            ToCaster = false,
                                            AsChild = true,
                                            SameDuration = false,
                                            NotLinkToAreaEffect = false
                                        }
                                        )
                                }
                                )
                        }
                        );
                    c.TriggerOnStatDamageOrEnergyDrain = false;
                    c.CheckWeaponType = false;
                    c.CheckAbilityType = false;
                    c.m_AbilityType = AbilityType.Spell;
                    c.CheckSpellDescriptor = false;
                    c.CheckSpellParent = false;
                    c.NotZeroDamage = true;
                    c.CheckDamageDealt = false;
                    c.CompareType = CompareOperation.Type.Equal;
                    c.TargetValue = new ContextValue();
                    c.CheckEnergyDamageType = true;
                    c.EnergyType = DamageEnergyType.Cold;
                    c.TargetKilledByThisDamage = false;
                    c.IgnoreDamageFromThisFact = true;
                    c.m_WeaponType = null;
                    c.m_AbilityList = new BlueprintAbilityReference[0] {};
                    c.SpellDescriptorsList = SpellDescriptor.None;
                });
                bp.AddComponent<ContextSetAbilityParams>(c => {
                    c.Add10ToDC = false;
                    c.DC = new ContextValue() {
                        ValueType = ContextValueType.CasterCustomProperty,
                        Value = 0,
                        Property = UnitProperty.None,
                        m_CustomProperty = SummonedNumbingColdDCProperty.ToReference<BlueprintUnitPropertyReference>(),
                        m_AbilityParameter = AbilityParameterType.Level,
                        PropertyName = ContextPropertyName.Value1
                    };
                    c.CasterLevel = new ContextValue();
                    c.Concentration = new ContextValue();
                    c.SpellLevel = new ContextValue();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = false;
            });

            var SummonedBlizzardElementalMediumWhirlwindBuff = Helpers.CreateBuff("SummonedBlizzardElementalMediumWhirlwindBuff", bp => {
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = SummonedBlizzardElementalMediumWhirlwindArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = Kingmaker.UnitLogic.UnitCondition.CanNotAttack;
                });
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = Kingmaker.UnitLogic.UnitCondition.DisableAttacksOfOpportunity;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = AirElementalInWhirlind,
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0,
                                m_IsExtendable = true
                            },
                            IsFromSpell = false,
                            IsNotDispelable = false,
                            ToCaster = false,
                            AsChild = true,
                            SameDuration = false,
                            NotLinkToAreaEffect = false
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<RemoveBuffIfCasterIsMissing>(c => {
                    c.RemoveOnCasterDeath = true;
                });
                bp.m_Icon = AirElementalMediumWhirlwindAbility.Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
                bp.Ranks = 0;
                bp.Frequency = DurationRate.Rounds;                
            });
            var SummonedBlizzardElementalHugeWhirlwindBuff = Helpers.CreateBuff("SummonedBlizzardElementalHugeWhirlwindBuff", bp => {
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = SummonedBlizzardElementalHugeWhirlwindArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = Kingmaker.UnitLogic.UnitCondition.CanNotAttack;
                });
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = Kingmaker.UnitLogic.UnitCondition.DisableAttacksOfOpportunity;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = AirElementalInWhirlind,
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0,
                                m_IsExtendable = true
                            },
                            IsFromSpell = false,
                            IsNotDispelable = false,
                            ToCaster = false,
                            AsChild = true,
                            SameDuration = false,
                            NotLinkToAreaEffect = false
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<RemoveBuffIfCasterIsMissing>(c => {
                    c.RemoveOnCasterDeath = true;
                });
                bp.m_Icon = AirElementalMediumWhirlwindAbility.Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
                bp.Ranks = 0;
                bp.Frequency = DurationRate.Rounds;
            });
            var SummonedBlizzardElementalElderWhirlwindBuff = Helpers.CreateBuff("SummonedBlizzardElementalElderWhirlwindBuff", bp => {
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = SummonedBlizzardElementalElderWhirlwindArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = Kingmaker.UnitLogic.UnitCondition.CanNotAttack;
                });
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = Kingmaker.UnitLogic.UnitCondition.DisableAttacksOfOpportunity;
                });
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = AirElementalInWhirlind,
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 0,
                                m_IsExtendable = true
                            },
                            IsFromSpell = false,
                            IsNotDispelable = false,
                            ToCaster = false,
                            AsChild = true,
                            SameDuration = false,
                            NotLinkToAreaEffect = false
                        }
                        );
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList();
                });
                bp.AddComponent<RemoveBuffIfCasterIsMissing>(c => {
                    c.RemoveOnCasterDeath = true;
                });
                bp.m_Icon = AirElementalMediumWhirlwindAbility.Icon;
                bp.m_Flags = BlueprintBuff.Flags.IsFromSpell | BlueprintBuff.Flags.StayOnDeath;
                bp.Stacking = StackingType.Replace;
                bp.Ranks = 0;
                bp.Frequency = DurationRate.Rounds;
            });

            var SummonedBlizzardElementalMediumWhirlwindAbility = Helpers.CreateBlueprint<BlueprintAbility>("SummonedBlizzardElementalMediumWhirlwindAbility", bp => {
                bp.m_DisplayName = SummonedBlizzardElementalWhirlwindDebuff.m_DisplayName;
                bp.m_Description = SummonedBlizzardElementalWhirlwindDebuff.m_Description;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = SummonedBlizzardElementalMediumWhirlwindBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default,
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = false,
                            IsNotDispelable = false,
                            ToCaster = true,
                            AsChild = true,
                            SameDuration = false
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = AirElementalInWhirlind,
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default,
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = false,
                            IsNotDispelable = false,
                            ToCaster = true,
                            AsChild = true,
                            SameDuration = false
                        }
                        );
                });
                bp.AddComponent<ContextCalculateAbilityParams>(c => {
                    c.UseKineticistMainStat = false;
                    c.StatType = StatType.Strength;
                    c.StatTypeFromCustomProperty = false;
                    c.m_CustomProperty = null;
                    c.ReplaceCasterLevel = false;
                    c.CasterLevel = new ContextValue();
                    c.ReplaceSpellLevel = false;
                    c.SpellLevel = new ContextValue();
                });
                bp.AddComponent<RecalculateOnStatChange>(c => {
                    c.UseKineticistMainStat = false;
                    c.Stat = StatType.Strength;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CharacterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                });
                bp.m_Icon = AirElementalMediumWhirlwindAbility.Icon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Special;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonedBlizzardElementalHugeWhirlwindAbility = Helpers.CreateBlueprint<BlueprintAbility>("SummonedBlizzardElementalHugeWhirlwindAbility", bp => {
                bp.m_DisplayName = SummonedBlizzardElementalWhirlwindDebuff.m_DisplayName;
                bp.m_Description = SummonedBlizzardElementalWhirlwindDebuff.m_Description;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = SummonedBlizzardElementalHugeWhirlwindBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default,
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = false,
                            IsNotDispelable = false,
                            ToCaster = true,
                            AsChild = true,
                            SameDuration = false
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = AirElementalInWhirlind,
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default,
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = false,
                            IsNotDispelable = false,
                            ToCaster = true,
                            AsChild = true,
                            SameDuration = false
                        }
                        );
                });
                bp.AddComponent<ContextCalculateAbilityParams>(c => {
                    c.UseKineticistMainStat = false;
                    c.StatType = StatType.Strength;
                    c.StatTypeFromCustomProperty = false;
                    c.m_CustomProperty = null;
                    c.ReplaceCasterLevel = false;
                    c.CasterLevel = new ContextValue();
                    c.ReplaceSpellLevel = false;
                    c.SpellLevel = new ContextValue();
                });
                bp.AddComponent<RecalculateOnStatChange>(c => {
                    c.UseKineticistMainStat = false;
                    c.Stat = StatType.Strength;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CharacterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                });
                bp.m_Icon = AirElementalMediumWhirlwindAbility.Icon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Special;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var SummonedBlizzardElementalElderWhirlwindAbility = Helpers.CreateBlueprint<BlueprintAbility>("SummonedBlizzardElementalElderWhirlwindAbility", bp => {
                bp.m_DisplayName = SummonedBlizzardElementalWhirlwindDebuff.m_DisplayName;
                bp.m_Description = SummonedBlizzardElementalWhirlwindDebuff.m_Description;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = SummonedBlizzardElementalElderWhirlwindBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default,
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = false,
                            IsNotDispelable = false,
                            ToCaster = true,
                            AsChild = true,
                            SameDuration = false
                        },
                        new ContextActionApplyBuff() {
                            m_Buff = AirElementalInWhirlind,
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default,
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = false,
                            IsNotDispelable = false,
                            ToCaster = true,
                            AsChild = true,
                            SameDuration = false
                        }
                        );
                });
                bp.AddComponent<ContextCalculateAbilityParams>(c => {
                    c.UseKineticistMainStat = false;
                    c.StatType = StatType.Strength;
                    c.StatTypeFromCustomProperty = false;
                    c.m_CustomProperty = null;
                    c.ReplaceCasterLevel = false;
                    c.CasterLevel = new ContextValue();
                    c.ReplaceSpellLevel = false;
                    c.SpellLevel = new ContextValue();
                });
                bp.AddComponent<RecalculateOnStatChange>(c => {
                    c.UseKineticistMainStat = false;
                    c.Stat = StatType.Strength;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.CharacterLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                });
                bp.m_Icon = AirElementalMediumWhirlwindAbility.Icon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Special;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var UnitsEnoughForWhirlwind = Resources.GetBlueprint<UnitsThreateningConsideration>("dce40e2004109804baadb7c9f3e787ef");
            var SummonedBlizzardElementalMediumWhirlwindAbilityAiAction = Helpers.CreateBlueprint<BlueprintAiCastSpell>("SummonedBlizzardElementalMediumWhirlwindAbilityAiAction", bp => {
                bp.AdditionalBehaviour = false;
                bp.MinDifficulty = Kingmaker.Settings.GameDifficultyOption.Story;
                bp.InvertDifficultyRequirements = false;
                bp.OncePerRound = false;
                bp.CooldownRounds = 0;
                bp.CooldownDice = new DiceFormula() { m_Dice = DiceType.Zero, m_Rolls = 0 };
                bp.StartCooldownRounds = 0;
                bp.CombatCount = 0;
                bp.UseWhenAIDisabled = false;
                bp.UseOnLimitedAI = false;
                bp.BaseScore = 2.1f;
                bp.m_ActorConsiderations = new ConsiderationReference[] { UnitsEnoughForWhirlwind.ToReference<ConsiderationReference>() };
                bp.m_TargetConsiderations = new ConsiderationReference[] { };
                bp.m_AffectedByImpatience = false;
                bp.m_Ability = SummonedBlizzardElementalMediumWhirlwindAbility.ToReference<BlueprintAbilityReference>();
                bp.m_ForceTargetSelf = true;
                bp.m_ForceTargetEnemy = false;
                bp.m_RandomVariant = false;
                bp.m_Variant = null;
                bp.m_VariantsSet = new BlueprintAbilityReference[] { };
                bp.Locators = new EntityReference[] { };
                bp.CheckCasterDistance = false;
                bp.MinCasterDistanceToLocator = 0;
                bp.CheckPartyDistance = false;
                bp.MinPartyDistanceToLocator = 0;
                bp.MaxPartyDistanceToLocator = 0;
            });
            var SummonedBlizzardElementalHugeWhirlwindAbilityAiAction = Helpers.CreateBlueprint<BlueprintAiCastSpell>("SummonedBlizzardElementalHugeWhirlwindAbilityAiAction", bp => {
                bp.AdditionalBehaviour = false;
                bp.MinDifficulty = Kingmaker.Settings.GameDifficultyOption.Story;
                bp.InvertDifficultyRequirements = false;
                bp.OncePerRound = false;
                bp.CooldownRounds = 0;
                bp.CooldownDice = new DiceFormula() { m_Dice = DiceType.Zero, m_Rolls = 0 };
                bp.StartCooldownRounds = 0;
                bp.CombatCount = 0;
                bp.UseWhenAIDisabled = false;
                bp.UseOnLimitedAI = false;
                bp.BaseScore = 2.1f;
                bp.m_ActorConsiderations = new ConsiderationReference[] { UnitsEnoughForWhirlwind.ToReference<ConsiderationReference>() };
                bp.m_TargetConsiderations = new ConsiderationReference[] { };
                bp.m_AffectedByImpatience = false;
                bp.m_Ability = SummonedBlizzardElementalHugeWhirlwindAbility.ToReference<BlueprintAbilityReference>();
                bp.m_ForceTargetSelf = true;
                bp.m_ForceTargetEnemy = false;
                bp.m_RandomVariant = false;
                bp.m_Variant = null;
                bp.m_VariantsSet = new BlueprintAbilityReference[] { };
                bp.Locators = new EntityReference[] { };
                bp.CheckCasterDistance = false;
                bp.MinCasterDistanceToLocator = 0;
                bp.CheckPartyDistance = false;
                bp.MinPartyDistanceToLocator = 0;
                bp.MaxPartyDistanceToLocator = 0;
            });
            var SummonedBlizzardElementalElderWhirlwindAbilityAiAction = Helpers.CreateBlueprint<BlueprintAiCastSpell>("SummonedBlizzardElementalElderWhirlwindAbilityAiAction", bp => {
                bp.AdditionalBehaviour = false;
                bp.MinDifficulty = Kingmaker.Settings.GameDifficultyOption.Story;
                bp.InvertDifficultyRequirements = false;
                bp.OncePerRound = false;
                bp.CooldownRounds = 0;
                bp.CooldownDice = new DiceFormula() { m_Dice = DiceType.Zero, m_Rolls = 0 };
                bp.StartCooldownRounds = 0;
                bp.CombatCount = 0;
                bp.UseWhenAIDisabled = false;
                bp.UseOnLimitedAI = false;
                bp.BaseScore = 2.1f;
                bp.m_ActorConsiderations = new ConsiderationReference[] { UnitsEnoughForWhirlwind.ToReference<ConsiderationReference>() };
                bp.m_TargetConsiderations = new ConsiderationReference[] { };
                bp.m_AffectedByImpatience = false;
                bp.m_Ability = SummonedBlizzardElementalElderWhirlwindAbility.ToReference<BlueprintAbilityReference>();
                bp.m_ForceTargetSelf = true;
                bp.m_ForceTargetEnemy = false;
                bp.m_RandomVariant = false;
                bp.m_Variant = null;
                bp.m_VariantsSet = new BlueprintAbilityReference[] { };
                bp.Locators = new EntityReference[] { };
                bp.CheckCasterDistance = false;
                bp.MinCasterDistanceToLocator = 0;
                bp.CheckPartyDistance = false;
                bp.MinPartyDistanceToLocator = 0;
                bp.MaxPartyDistanceToLocator = 0;
            });
            var SummonedBlizzardElementalBrain = Helpers.CreateBlueprint<BlueprintBrain>("SummonedBlizzardElementalBrain", bp => {
                bp.m_Actions = new BlueprintAiActionReference[] {
                    AttackAiAction.ToReference<BlueprintAiActionReference>(),
                    AirElementalFollowEnemyAiAction.ToReference<BlueprintAiActionReference>(),
                    SummonedBlizzardElementalMediumWhirlwindAbilityAiAction.ToReference<BlueprintAiActionReference>(),
                    SummonedBlizzardElementalHugeWhirlwindAbilityAiAction.ToReference<BlueprintAiActionReference>(),
                    SummonedBlizzardElementalElderWhirlwindAbilityAiAction.ToReference<BlueprintAiActionReference>()
                };
            });

            var OracleRevelationServantOfWinterSummonMedium = Helpers.CreateBlueprint<BlueprintUnit>("OracleRevelationServantOfWinterSummonMedium", bp => {
                bp.SetLocalisedName("Summoned Blizzard Elemental");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = OutsiderClass;
                    c.Levels = 4;
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Unknown;
                    c.Skills = new StatType[] { StatType.SkillPerception | StatType.SkillKnowledgeArcana | StatType.SkillMobility | StatType.SkillStealth };
                    c.Selections = new SelectionEntry[] { };
                    c.DoNotApplyAutomatically = false;
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = Unlootable;
                });
                bp.AddComponent<ChangeUnitSize>(c => {
                    c.m_Type = ChangeUnitSize.ChangeType.Delta;
                    c.SizeDelta = 1;
                    c.Size = Size.Fine;
                });
                bp.m_Type = ElementalAirType;
                bp.Gender = Gender.Male;
                bp.Size = Size.Small;
                bp.Color = BlizzardServantUnit.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = ElementalAirPortait;
                bp.Prefab = new UnitViewLink { AssetId = "4149c9e5f66f9944ba3747409a680674" };
                bp.Visual = new UnitVisualParams() {
                    BloodType = BloodType.Dust,
                    FootprintType = FootprintType.Humanoid,
                    FootprintScale = 1,
                    ArmorFx = new PrefabLink(),
                    BloodPuddleFx = new PrefabLink(),
                    DismemberFx = new PrefabLink(),
                    RipLimbsApartFx = new PrefabLink(),
                    IsNotUseDismember = false,
                    m_Barks = ElementalAirBarks,
                    ReachFXThresholdBonus = 0,
                    DefaultArmorSoundType = ArmorSoundType.Flesh,
                    FootstepSoundSizeType = FootstepSoundSizeType.Ghost,
                    FootSoundType = FootSoundType.Ghost,
                    FootSoundSize = Size.Medium,
                    BodySoundType = BodySoundType.Flesh,
                    BodySoundSize = Size.Medium,
                    FoleySoundPrefix = null, //?
                    NoFinishingBlow = false,
                    ImportanceOverride = 0,
                    SilentCaster = true
                };
                bp.m_Faction = Summoned;
                bp.FactionOverrides = new FactionOverrides();
                bp.m_Brain = SummonedBlizzardElementalBrain.ToReference<BlueprintBrainReference>();
                bp.Body = new BlueprintUnit.UnitBody() {
                    DisableHands = true,
                    m_EmptyHandWeapon = new BlueprintItemWeaponReference(),
                    m_PrimaryHand = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHand = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative3 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative3 = new BlueprintItemEquipmentHandReference(),
                    ActiveHandSet = 0,
                    m_AdditionalLimbs = new BlueprintItemWeaponReference[] {
                        BlizzardServantSpike1d6,
                        BlizzardServantSpike1d6
                    }
                };
                bp.Strength = 14;
                bp.Dexterity = 21;
                bp.Constitution = 14;
                bp.Intelligence = 4;
                bp.Wisdom = 11;
                bp.Charisma = 11;
                bp.Speed = new Feet(60);
                bp.Skills = new BlueprintUnit.UnitSkills() {
                    Acrobatics = 0,
                    Physique = 0,
                    Diplomacy = 0,
                    Thievery = 0,
                    LoreNature = 0,
                    Perception = 0,
                    Stealth = 0,
                    UseMagicDevice = 0,
                    LoreReligion = 0,
                    KnowledgeWorld = 0,
                    KnowledgeArcana = 0,
                };
                bp.MaxHP = 0;
                bp.m_AddFacts = new BlueprintUnitFactReference[] {
                    SubtypeAir.ToReference<BlueprintUnitFactReference>(),
                    SubtypeElemental.ToReference<BlueprintUnitFactReference>(),
                    SubtypeExtraplanar.ToReference<BlueprintUnitFactReference>(),
                    TripImmune.ToReference<BlueprintUnitFactReference>(),
                    Airborne.ToReference<BlueprintUnitFactReference>(),
                    AirMastery.ToReference<BlueprintUnitFactReference>(),
                    BlizzardImmunity.ToReference<BlueprintUnitFactReference>(),
                    BlizzardServantFXBuff.ToReference<BlueprintUnitFactReference>(),
                    NotADemonFeature.ToReference<BlueprintUnitFactReference>(),
                    NaturalArmor3.ToReference<BlueprintUnitFactReference>(),
                    ColdResistance10.ToReference<BlueprintUnitFactReference>(),
                    SummonedBlizzardElementalMediumWhirlwindAbility.ToReference<BlueprintUnitFactReference>(),
                };
            });
            var OracleRevelationServantOfWinterSummonHuge = Helpers.CreateBlueprint<BlueprintUnit>("OracleRevelationServantOfWinterSummonHuge", bp => {
                bp.SetLocalisedName("Summoned Huge Blizzard Elemental");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = OutsiderClass;
                    c.Levels = 10;
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Unknown;
                    c.Skills = new StatType[] { StatType.SkillPerception | StatType.SkillKnowledgeArcana | StatType.SkillMobility | StatType.SkillStealth };
                    c.Selections = new SelectionEntry[] { };
                    c.DoNotApplyAutomatically = false;
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = Unlootable;
                });
                bp.AddComponent<ChangeUnitSize>(c => {
                    c.m_Type = ChangeUnitSize.ChangeType.Delta;
                    c.SizeDelta = 2;
                    c.Size = Size.Fine;
                });
                bp.m_Type = ElementalAirType;
                bp.Gender = Gender.Male;
                bp.Size = Size.Small;
                bp.Color = BlizzardServantUnit.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = ElementalAirPortait;
                bp.Prefab = new UnitViewLink { AssetId = "4149c9e5f66f9944ba3747409a680674" };
                bp.Visual = new UnitVisualParams() {
                    BloodType = BloodType.Dust,
                    FootprintType = FootprintType.Humanoid,
                    FootprintScale = 1,
                    ArmorFx = new PrefabLink(),
                    BloodPuddleFx = new PrefabLink(),
                    DismemberFx = new PrefabLink(),
                    RipLimbsApartFx = new PrefabLink(),
                    IsNotUseDismember = false,
                    m_Barks = ElementalAirBarks,
                    ReachFXThresholdBonus = 0,
                    DefaultArmorSoundType = ArmorSoundType.Flesh,
                    FootstepSoundSizeType = FootstepSoundSizeType.Ghost,
                    FootSoundType = FootSoundType.Ghost,
                    FootSoundSize = Size.Large,
                    BodySoundType = BodySoundType.Flesh,
                    BodySoundSize = Size.Large,
                    FoleySoundPrefix = null, //?
                    NoFinishingBlow = false,
                    ImportanceOverride = 0,
                    SilentCaster = true
                };
                bp.m_Faction = Summoned;
                bp.FactionOverrides = new FactionOverrides();
                bp.m_Brain = SummonedBlizzardElementalBrain.ToReference<BlueprintBrainReference>();
                bp.Body = new BlueprintUnit.UnitBody() {
                    DisableHands = true,
                    m_EmptyHandWeapon = new BlueprintItemWeaponReference(),
                    m_PrimaryHand = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHand = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative3 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative3 = new BlueprintItemEquipmentHandReference(),
                    ActiveHandSet = 0,
                    m_AdditionalLimbs = new BlueprintItemWeaponReference[] {
                        BlizzardElementalSpike2d6.ToReference<BlueprintItemWeaponReference>(),
                        BlizzardElementalSpike2d6.ToReference<BlueprintItemWeaponReference>()
                    }
                };
                bp.Strength = 22;
                bp.Dexterity = 29;
                bp.Constitution = 18;
                bp.Intelligence = 6;
                bp.Wisdom = 11;
                bp.Charisma = 11;
                bp.Speed = new Feet(60);
                bp.Skills = new BlueprintUnit.UnitSkills() {
                    Acrobatics = 0,
                    Physique = 0,
                    Diplomacy = 0,
                    Thievery = 0,
                    LoreNature = 0,
                    Perception = 0,
                    Stealth = 0,
                    UseMagicDevice = 0,
                    LoreReligion = 0,
                    KnowledgeWorld = 0,
                    KnowledgeArcana = 0,
                };
                bp.MaxHP = 0;
                bp.m_AddFacts = new BlueprintUnitFactReference[] {
                    SubtypeAir.ToReference<BlueprintUnitFactReference>(),
                    SubtypeElemental.ToReference<BlueprintUnitFactReference>(),
                    SubtypeExtraplanar.ToReference<BlueprintUnitFactReference>(),
                    TripImmune.ToReference<BlueprintUnitFactReference>(),
                    Airborne.ToReference<BlueprintUnitFactReference>(),
                    AirMastery.ToReference<BlueprintUnitFactReference>(),
                    BlizzardImmunity.ToReference<BlueprintUnitFactReference>(),
                    BlizzardServantFXBuff.ToReference<BlueprintUnitFactReference>(),
                    NotADemonFeature.ToReference<BlueprintUnitFactReference>(),
                    NaturalArmor4.ToReference<BlueprintUnitFactReference>(),
                    ColdResistance20.ToReference<BlueprintUnitFactReference>(),
                    SummonedBlizzardElementalHugeWhirlwindAbility.ToReference<BlueprintUnitFactReference>(),
                    SummonedBlizzardElementalNumbingColdFeature.ToReference<BlueprintUnitFactReference>()
                };
            });
            var OracleRevelationServantOfWinterSummonElder = Helpers.CreateBlueprint<BlueprintUnit>("OracleRevelationServantOfWinterSummonElder", bp => {
                bp.SetLocalisedName("Summoned Elder Blizzard Elemental");
                bp.AddComponent<AddClassLevels>(c => {
                    c.m_CharacterClass = OutsiderClass;
                    c.Levels = 16;
                    c.RaceStat = StatType.Constitution;
                    c.LevelsStat = StatType.Unknown;
                    c.Skills = new StatType[] { StatType.SkillPerception | StatType.SkillKnowledgeArcana | StatType.SkillMobility | StatType.SkillStealth };
                    c.Selections = new SelectionEntry[] { };
                    c.DoNotApplyAutomatically = false;
                });
                bp.AddComponent<BuffOnEntityCreated>(c => {
                    c.m_Buff = Unlootable;
                });
                bp.AddComponent<ChangeUnitSize>(c => {
                    c.m_Type = ChangeUnitSize.ChangeType.Delta;
                    c.SizeDelta = 3;
                    c.Size = Size.Fine;
                });
                bp.m_Type = ElementalAirType;
                bp.Gender = Gender.Male;
                bp.Size = Size.Small;
                bp.Color = BlizzardServantUnit.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.m_Portrait = ElementalAirPortait;
                bp.Prefab = new UnitViewLink { AssetId = "4149c9e5f66f9944ba3747409a680674" };
                bp.Visual = new UnitVisualParams() {
                    BloodType = BloodType.Dust,
                    FootprintType = FootprintType.Humanoid,
                    FootprintScale = 1,
                    ArmorFx = new PrefabLink(),
                    BloodPuddleFx = new PrefabLink(),
                    DismemberFx = new PrefabLink(),
                    RipLimbsApartFx = new PrefabLink(),
                    IsNotUseDismember = false,
                    m_Barks = ElementalAirBarks,
                    ReachFXThresholdBonus = 0,
                    DefaultArmorSoundType = ArmorSoundType.Flesh,
                    FootstepSoundSizeType = FootstepSoundSizeType.Ghost,
                    FootSoundType = FootSoundType.Ghost,
                    FootSoundSize = Size.Large,
                    BodySoundType = BodySoundType.Flesh,
                    BodySoundSize = Size.Large,
                    FoleySoundPrefix = null, //?
                    NoFinishingBlow = false,
                    ImportanceOverride = 0,
                    SilentCaster = true
                };
                bp.m_Faction = Summoned;
                bp.FactionOverrides = new FactionOverrides();
                bp.m_Brain = SummonedBlizzardElementalBrain.ToReference<BlueprintBrainReference>();
                bp.Body = new BlueprintUnit.UnitBody() {
                    DisableHands = true,
                    m_EmptyHandWeapon = new BlueprintItemWeaponReference(),
                    m_PrimaryHand = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHand = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative1 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative2 = new BlueprintItemEquipmentHandReference(),
                    m_PrimaryHandAlternative3 = new BlueprintItemEquipmentHandReference(),
                    m_SecondaryHandAlternative3 = new BlueprintItemEquipmentHandReference(),
                    ActiveHandSet = 0,
                    m_AdditionalLimbs = new BlueprintItemWeaponReference[] {
                        BlizzardElementalSpike3d6.ToReference<BlueprintItemWeaponReference>(),
                        BlizzardElementalSpike3d6.ToReference<BlueprintItemWeaponReference>()
                    }
                };
                bp.Strength = 28;
                bp.Dexterity = 33;
                bp.Constitution = 18;
                bp.Intelligence = 10;
                bp.Wisdom = 11;
                bp.Charisma = 11;
                bp.Speed = new Feet(60);
                bp.Skills = new BlueprintUnit.UnitSkills() {
                    Acrobatics = 0,
                    Physique = 0,
                    Diplomacy = 0,
                    Thievery = 0,
                    LoreNature = 0,
                    Perception = 0,
                    Stealth = 0,
                    UseMagicDevice = 0,
                    LoreReligion = 0,
                    KnowledgeWorld = 0,
                    KnowledgeArcana = 0,
                };
                bp.MaxHP = 0;
                bp.m_AddFacts = new BlueprintUnitFactReference[] {
                    SubtypeAir.ToReference<BlueprintUnitFactReference>(),
                    SubtypeElemental.ToReference<BlueprintUnitFactReference>(),
                    SubtypeExtraplanar.ToReference<BlueprintUnitFactReference>(),
                    TripImmune.ToReference<BlueprintUnitFactReference>(),
                    Airborne.ToReference<BlueprintUnitFactReference>(),
                    AirMastery.ToReference<BlueprintUnitFactReference>(),
                    BlizzardImmunity.ToReference<BlueprintUnitFactReference>(),
                    BlizzardServantFXBuff.ToReference<BlueprintUnitFactReference>(),
                    NotADemonFeature.ToReference<BlueprintUnitFactReference>(),
                    NaturalArmor8.ToReference<BlueprintUnitFactReference>(),
                    ColdResistance30.ToReference<BlueprintUnitFactReference>(),
                    SummonedBlizzardElementalElderWhirlwindAbility.ToReference<BlueprintUnitFactReference>(),
                    SummonedBlizzardElementalNumbingColdFeature.ToReference<BlueprintUnitFactReference>()
                };
            });
            var SummonElementalElderAir = Resources.GetBlueprint<BlueprintAbility>("333efbf776ab61c4da53e9622751d95f");
            var OracleRevelationServantOfWinterAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationServantOfWinterAbility", bp => {
                bp.SetName("Servant of Winter");
                bp.SetDescription("As a full-round action, you can summon a single blizzard elemental to serve you. At 7th level, you can summon a Medium blizzard elemental. " +
                    "At 11th level, you can summon a Huge blizzard elemental. At 15th level, you can summon an elder blizzard elemental. You can use this ability once per day, " +
                    "plus one additional time per day at 15th level. The blizzard elemental serves you for one full day, unless dismissed or another is summoned. You must be at " +
                    "least 7th level before selecting this revelation.");
                bp.m_Icon = SummonElementalElderAir.Icon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionClearSummonPool() {
                            m_SummonPool = ServantOfWinterSummonPool.ToReference<BlueprintSummonPoolReference>()
                        },
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionSharedValueHigher() {
                                        Not = false,
                                        SharedValue = AbilitySharedValue.Damage,
                                        HigherOrEqual = 10,
                                        Inverted = true
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionSpawnMonster() {
                                    m_Blueprint = OracleRevelationServantOfWinterSummonMedium.ToReference<BlueprintUnitReference>(),
                                    AfterSpawn = Helpers.CreateActionList(),
                                    m_SummonPool = ServantOfWinterSummonPool.ToReference<BlueprintSummonPoolReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Days,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 1,
                                        m_IsExtendable = true
                                    },
                                    CountValue = new ContextDiceValue() {
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 1
                                    },
                                    LevelValue = 0,
                                    DoNotLinkToCaster = false,
                                    IsDirectlyControllable = false
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionSharedValueHigher() {
                                                Not = false,
                                                SharedValue = AbilitySharedValue.Damage,
                                                HigherOrEqual = 14,
                                                Inverted = true
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionSpawnMonster() {
                                            m_Blueprint = OracleRevelationServantOfWinterSummonHuge.ToReference<BlueprintUnitReference>(),
                                            AfterSpawn = Helpers.CreateActionList(),
                                            m_SummonPool = ServantOfWinterSummonPool.ToReference<BlueprintSummonPoolReference>(),
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Days,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = 1,
                                                m_IsExtendable = true
                                            },
                                            CountValue = new ContextDiceValue() {
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = 1
                                            },
                                            LevelValue = 0,
                                            DoNotLinkToCaster = false,
                                            IsDirectlyControllable = false
                                        }
                                        ),
                                    IfFalse = Helpers.CreateActionList(
                                        new ContextActionSpawnMonster() {
                                            m_Blueprint = OracleRevelationServantOfWinterSummonElder.ToReference<BlueprintUnitReference>(),
                                            AfterSpawn = Helpers.CreateActionList(),
                                            m_SummonPool = ServantOfWinterSummonPool.ToReference<BlueprintSummonPoolReference>(),
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Days,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = 1,
                                                m_IsExtendable = true
                                            },
                                            CountValue = new ContextDiceValue() {
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = 1
                                            },
                                            LevelValue = 0,
                                            DoNotLinkToCaster = false,
                                            IsDirectlyControllable = false
                                        }
                                        )
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationServantOfWinterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                    c.Amount = 1;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] {                        
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.Zero,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Simple,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.StatBonus
                        }
                    };
                    c.Modifier = 1;
                });
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.ShouldTurnToTarget = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.m_IsFullRoundAction = true;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationServantOfWinterAbility.Duration", "1 day");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationServantOfWinterFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationServantOfWinterFeature", bp => {
                bp.SetName("Servant of Winter");
                bp.SetDescription("As a full-round action, you can summon a single blizzard elemental to serve you. At 7th level, you can summon a Medium blizzard elemental. " +
                    "At 11th level, you can summon a Huge blizzard elemental. At 15th level, you can summon an elder blizzard elemental. You can use this ability once per day, " +
                    "plus one additional time per day at 15th level. The blizzard elemental serves you for one full day, unless dismissed or another is summoned. You must be at " +
                    "least 7th level before selecting this revelation.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationServantOfWinterAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleWinterMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherWinterMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistWinterMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoWinterMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationServantOfWinterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 7;
                    c.Group = Prerequisite.GroupType.Any;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 7;
                    c.Group = Prerequisite.GroupType.Any;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationServantOfWinterFeature.ToReference<BlueprintFeatureReference>());

            MysteryTools.RegisterMystery(OracleWinterMysteryFeature);
            MysteryTools.RegisterSecondMystery(OracleWinterMysteryFeature);
            MysteryTools.RegisterEnlightendPhilosopherMystery(EnlightnedPhilosopherWinterMysteryFeature);
            MysteryTools.RegisterSecondEnlightendPhilosopherMystery(EnlightnedPhilosopherWinterMysteryFeature);
            MysteryTools.RegisterHerbalistMystery(DivineHerbalistWinterMysteryFeature);
            MysteryTools.RegisterSecondHerbalistMystery(DivineHerbalistWinterMysteryFeature);
            MysteryTools.RegisterOceansEchoMystery(OceansEchoWinterMysteryFeature);
            MysteryTools.RegisterSecondOceansEchoMystery(OceansEchoWinterMysteryFeature);
            MysteryTools.RegisterHermitMystery(OracleWinterMysteryFeature);
            MysteryTools.RegisterSecondHermitMystery(OracleWinterMysteryFeature);
            MysteryTools.RegisterMysteryGiftSelection(OracleWinterMysteryFeature);

        }
    }
}
