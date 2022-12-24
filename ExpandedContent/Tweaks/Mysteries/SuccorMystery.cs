using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.Formations.Facts;
using Kingmaker.ResourceLinks;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.ActivatableAbilities.Restrictions;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Mysteries {
    internal class SuccorMystery {
        public static void AddSuccorMystery() {

            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var OracleRevelationSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("60008a10ad7ad6543b1f63016741a5d2");
            var SuccorMysteryIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleSuccorMystery.png");

            //Spelllist
            var RayOfEnfeeblementSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("450af0402422b0b4980d9c2175869612");
            var ShieldOfFortificationAbility = Resources.GetModBlueprint<BlueprintAbility>("ShieldOfFortificationAbility");
            var ShieldOfFortificationGreaterAbility = Resources.GetModBlueprint<BlueprintAbility>("ShieldOfFortificationGreaterAbility");
            var ClaySkinAbility = Resources.GetModBlueprint<BlueprintAbility>("ClaySkinAbility");
            var StoneSkinSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c66e86905f7606c4eaa5c774f0357b2b");
            var HeroismGreaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e15e5e7045fda2244b98c8f010adfe31");
            var BanishmentSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d361391f645db984bbf58907711a146a");
            var ProjectionFromSpellsSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("42aa71adc7343714fa92e471baa98d42");
            var MindBlankCommunalSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("87a29febd010993419f2a4a9bee11cfc");
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
            var OracleSuccorSpells = Helpers.CreateBlueprint<BlueprintFeature>("OracleSuccorSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = RayOfEnfeeblementSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShieldOfFortificationAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ClaySkinAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShieldOfFortificationGreaterAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = StoneSkinSpell;
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = HeroismGreaterSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BanishmentSpell;
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ProjectionFromSpellsSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MindBlankCommunalSpell;
                    c.SpellLevel = 9;
                });
            });
            var EnlightnedPhilosopherSuccorSpells = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherSuccorSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = RayOfEnfeeblementSpell;
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
            var OceansEchoSuccorSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoSuccorSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = RayOfEnfeeblementSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ClaySkinAbility.ToReference<BlueprintAbilityReference>();
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
                    c.m_Spell = ProjectionFromSpellsSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MindBlankCommunalSpell;
                    c.SpellLevel = 9;
                });
            });
            //Final Revelation
            var OracleSuccorFinalRevelation = Helpers.CreateBlueprint<BlueprintFeature>("OracleSuccorFinalRevelation", bp => {
                bp.SetName("Final Revelation");
                bp.SetDescription("At 20th level, all {g|Encyclopedia:Spell}spells{/g} with cure, restore hp, or temporary hp descriptors cast by the oracle are always empowered, as though using the Empower Spell {g|Encyclopedia:Feat}feat{/g}.");
                bp.AddComponent<AutoMetamagic>(c => {
                    c.m_AllowedAbilities = AutoMetamagic.AllowedType.SpellOnly;
                    c.Metamagic = Metamagic.Empower;
                    c.Abilities = new List<BlueprintAbilityReference>();
                    c.Descriptor = SpellDescriptor.Cure | SpellDescriptor.RestoreHP | SpellDescriptor.TemporaryHP;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.IsClassFeature = true;
            });
            //Main Mystery Feature
            var OracleSuccorMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleSuccorMysteryFeature", bp => {
                bp.m_Icon = SuccorMysteryIcon;
                bp.SetName("Succor");
                bp.SetDescription("An oracle with the succor mystery adds {g|Encyclopedia:Lore_Nature}Lore (nature){/g} and {g|Encyclopedia:Stealth}Stealth{/g} to " +
                    "her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleSuccorFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleSuccorSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillStealth;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //EnlightnedPhilosopherMystery
            var EnlightnedPhilosopherSuccorMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherSuccorMysteryFeature", bp => {
                bp.m_Icon = SuccorMysteryIcon;
                bp.SetName("Succor");
                bp.SetDescription("An oracle with the succor mystery adds {g|Encyclopedia:Lore_Nature}Lore (nature){/g} and {g|Encyclopedia:Stealth}Stealth{/g} to " +
                    "her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = EnlightnedPhilosopherSuccorSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillStealth;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.EnlightenedPhilosopherMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //DivineHerbalistMystery
            var DivineHerbalistSuccorMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("DivineHerbalistSuccorMysteryFeature", bp => {
                bp.m_Icon = SuccorMysteryIcon;
                bp.SetName("Succor");
                bp.SetDescription("Gain access to the spells and revelations of the succor mystery. \nDue to the divine herbalist archetype the class skills gained from this archetype" +
                    "are replaced by the master herbalist feature and the brew potions feat.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleSuccorFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleSuccorSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.DivineHerbalistMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //Ocean's Echo
            var OceansEchoSuccorMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoSuccorMysteryFeature", bp => {
                bp.m_Icon = SuccorMysteryIcon;
                bp.SetName("Succor");
                bp.SetDescription("Gain access to the spells and revelations of the succor mystery. \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleSuccorFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoSuccorSpells.ToReference<BlueprintFeatureReference>();
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
            //Combat Healer
            var CombatHealer = Resources.GetBlueprint<BlueprintFeature>("db1d9829383e78841a6f1145411a54b4").GetComponent<PrerequisiteFeaturesFromList>();
            CombatHealer.m_Features = CombatHealer.m_Features.AppendToArray(OracleSuccorMysteryFeature.ToReference<BlueprintFeatureReference>());
            CombatHealer.m_Features = CombatHealer.m_Features.AppendToArray(EnlightnedPhilosopherSuccorMysteryFeature.ToReference<BlueprintFeatureReference>());
            CombatHealer.m_Features = CombatHealer.m_Features.AppendToArray(DivineHerbalistSuccorMysteryFeature.ToReference<BlueprintFeatureReference>());
            CombatHealer.m_Features = CombatHealer.m_Features.AppendToArray(OceansEchoSuccorMysteryFeature.ToReference<BlueprintFeatureReference>());
            //Enhanced Cures
            var EnhancedCures = Resources.GetBlueprint<BlueprintFeature>("973a22b02c793ca49b48652e3d70ae80").GetComponent<PrerequisiteFeaturesFromList>();
            EnhancedCures.m_Features = EnhancedCures.m_Features.AppendToArray(OracleSuccorMysteryFeature.ToReference<BlueprintFeatureReference>());
            EnhancedCures.m_Features = EnhancedCures.m_Features.AppendToArray(EnlightnedPhilosopherSuccorMysteryFeature.ToReference<BlueprintFeatureReference>());
            EnhancedCures.m_Features = EnhancedCures.m_Features.AppendToArray(DivineHerbalistSuccorMysteryFeature.ToReference<BlueprintFeatureReference>());
            EnhancedCures.m_Features = EnhancedCures.m_Features.AppendToArray(OceansEchoSuccorMysteryFeature.ToReference<BlueprintFeatureReference>());
            //Soul Siphon
            var SoulSiphon = Resources.GetBlueprint<BlueprintFeature>("226c053a75fd7c34cab1b493f5847787").GetComponent<PrerequisiteFeaturesFromList>();
            SoulSiphon.m_Features = SoulSiphon.m_Features.AppendToArray(OracleSuccorMysteryFeature.ToReference<BlueprintFeatureReference>());
            SoulSiphon.m_Features = SoulSiphon.m_Features.AppendToArray(EnlightnedPhilosopherSuccorMysteryFeature.ToReference<BlueprintFeatureReference>());
            SoulSiphon.m_Features = SoulSiphon.m_Features.AppendToArray(DivineHerbalistSuccorMysteryFeature.ToReference<BlueprintFeatureReference>());
            SoulSiphon.m_Features = SoulSiphon.m_Features.AppendToArray(OceansEchoSuccorMysteryFeature.ToReference<BlueprintFeatureReference>());
            //Spirit Boost
            var SpiritBoost = Resources.GetBlueprint<BlueprintFeature>("8cf1bc6fe4d14304392496ff66023271").GetComponent<PrerequisiteFeaturesFromList>();
            SpiritBoost.m_Features = SpiritBoost.m_Features.AppendToArray(OracleSuccorMysteryFeature.ToReference<BlueprintFeatureReference>());
            SpiritBoost.m_Features = SpiritBoost.m_Features.AppendToArray(EnlightnedPhilosopherSuccorMysteryFeature.ToReference<BlueprintFeatureReference>());
            SpiritBoost.m_Features = SpiritBoost.m_Features.AppendToArray(DivineHerbalistSuccorMysteryFeature.ToReference<BlueprintFeatureReference>());
            SpiritBoost.m_Features = SpiritBoost.m_Features.AppendToArray(OceansEchoSuccorMysteryFeature.ToReference<BlueprintFeatureReference>());
            //Enhanced Inflictions
            var InflictLightWounds = Resources.GetBlueprintReference<BlueprintAbilityReference>("e5cb4c4459e437e49a4cd73fde6b9063");
            var InflictLightWoundsCast = Resources.GetBlueprintReference<BlueprintAbilityReference>("e5af3674bb241f14b9a9f6b0c7dc3d27");
            var InflictLightWoundsDamage = Resources.GetBlueprintReference<BlueprintAbilityReference>("f6ff156188dc4e44c850179fb19afaf5");
            var InflictModerateWounds = Resources.GetBlueprintReference<BlueprintAbilityReference>("14d749ecacca90a42b6bf1c3f580bb0c");
            var InflictModerateWoundsCast = Resources.GetBlueprintReference<BlueprintAbilityReference>("65f0b63c45ea82a4f8b8325768a3832d");
            var InflictModerateWoundsDamage = Resources.GetBlueprintReference<BlueprintAbilityReference>("e55f5a1b875a5f242be5b92cf027b69a");
            var InflictSeriousWounds = Resources.GetBlueprintReference<BlueprintAbilityReference>("b0b8a04a3d74e03489862b03f4e467a6");
            var InflictSeriousWoundsCast = Resources.GetBlueprintReference<BlueprintAbilityReference>("bd5da98859cf2b3418f6d68ea66cabbe");
            var InflictSeriousWoundsDamage = Resources.GetBlueprintReference<BlueprintAbilityReference>("095eaa846e2a8c343a54e927816e00af");
            var InflictCriticalWounds = Resources.GetBlueprintReference<BlueprintAbilityReference>("3cf05ef7606f06446ad357845cb4d430");
            var InflictCriticalWoundsCast = Resources.GetBlueprintReference<BlueprintAbilityReference>("651110ed4f117a948b41c05c5c7624c0");
            var InflictCriticalWoundsDamage = Resources.GetBlueprintReference<BlueprintAbilityReference>("2737152467af53b4f9800e7a60644bb6");
            var InflictLightWoundsMass = Resources.GetBlueprintReference<BlueprintAbilityReference>("9da37873d79ef0a468f969e4e5116ad2");
            var InflictLightWoundsMassDamage = Resources.GetBlueprintReference<BlueprintAbilityReference>("b70d903464a738148a19bed630b91f8c");
            var InflictModerateWoundsMass = Resources.GetBlueprintReference<BlueprintAbilityReference>("03944622fbe04824684ec29ff2cec6a7");
            var InflictModerateWoundsMassDamage = Resources.GetBlueprintReference<BlueprintAbilityReference>("89ddb1b4dafc5f541a3dacafbf9ea2dd");
            var InflictSeriousWoundsMass = Resources.GetBlueprintReference<BlueprintAbilityReference>("820170444d4d2a14abc480fcbdb49535");
            var InflictSeriousWoundsMassDamage = Resources.GetBlueprintReference<BlueprintAbilityReference>("aba480ce9381684408290f5434402a32");
            var InflictCriticalWoundsMass = Resources.GetBlueprintReference<BlueprintAbilityReference>("5ee395a2423808c4baf342a4f8395b19");
            var OracleRevelationEnhancedInflictions = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationEnhancedInflictions", bp => {
                bp.SetName("Enhanced Inflictions");
                bp.SetDescription("Whenever you cast a inflict {g|Encyclopedia:Spell}spell{/g}, the maximum number damage dealt is based on your oracle level, not the limit based on the spell. " +
                    "For example, an 11th-level oracle of succor with this revelation may cast inflict light wounds to cause {g|Encyclopedia:Dice}1d8{/g}+11 hit points  of damage instead of the " +
                    "normal 1d8+5 maximum.");
                bp.AddComponent<AddUnlimitedSpell>(c => {
                    c.m_Abilities = new BlueprintAbilityReference[] {
                        InflictLightWounds,
                        InflictLightWoundsCast,
                        InflictLightWoundsDamage,
                        InflictModerateWounds,
                        InflictModerateWoundsCast,
                        InflictModerateWoundsDamage,
                        InflictSeriousWounds,
                        InflictSeriousWoundsCast,
                        InflictSeriousWoundsDamage,
                        InflictCriticalWounds,
                        InflictCriticalWoundsCast,
                        InflictCriticalWoundsDamage,
                        InflictLightWoundsMass,
                        InflictLightWoundsMassDamage,
                        InflictModerateWoundsMass,
                        InflictModerateWoundsMassDamage,
                        InflictSeriousWoundsMass,
                        InflictSeriousWoundsMassDamage,
                        InflictCriticalWoundsMass
                    };
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleSuccorMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherSuccorMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistSuccorMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoSuccorMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationEnhancedInflictions.ToReference<BlueprintFeatureReference>());
            //Perfect Aid
            var OracleRevelationPerfectAid = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationPerfectAid", bp => {
                bp.SetName("Perfect Aid");
                bp.SetDescription("You can effortlessly give aid to your allies, whether that means providing them with help attacking or defending them in the heat of combat. Whenever you use the aid another action " +
                    "to inflict a penalty on attack rolls or to AC against one of your allies, the penalty you inflict increases by 1. This bonus increases by 1 at 4th level and every 5 oracle levels thereafter (to a " +
                    "maximum of -5 at 19th level). It doesn’t stack with other feats or class features that improve the bonus you provide when using the aid another action. This revelation also counts as the Combat " +
                    "Expertise feat, but only for the purpose of meeting the prerequisites of the Swift Aid feat.");
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleSuccorMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherSuccorMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistSuccorMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoSuccorMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationPerfectAid.ToReference<BlueprintFeatureReference>());
            //Shell of Succor
            var OracleRevelationShellOfSuccorIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleRevelationShellOfSuccor.jpg");
            var OracleRevelationShellOfSuccorResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationShellOfSuccorResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    StartingLevel = 3,
                    LevelStep = 8,
                    StartingIncrease = 1,
                    PerStepIncrease = 1
                };
                bp.m_UseMax = true;
                bp.m_Max = 3;
            });
            var OracleRevelationShellOfSuccorBuff = Helpers.CreateBuff("OracleRevelationShellOfSuccorBuff", bp => {
                bp.SetName("Shell of Succor");
                bp.SetDescription("You can surround an ally with bolstering energies that supplement its health and grant it extra vigor. With a touch from you (a standard action), one creature gains a ward of restorative " +
                    "energy, granting it a number of temporary hit points equal to your Charisma bonus + 1d6 per 2 oracle levels you have (maximum 10d6). These temporary hit points last 1 minute per oracle level you have.");
                bp.AddComponent<TemporaryHitPointsFromAbilityValue>(c => {
                    c.Descriptor = ModifierDescriptor.None;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        Value = 0,
                        ValueRank = AbilityRankType.Default,
                        ValueShared = AbilitySharedValue.Heal,
                        Property = UnitProperty.None
                    };
                    c.RemoveWhenHitPointsEnd = true;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.StatBonus;
                    c.m_Stat = StatType.Charisma;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMin = true;
                    c.m_Min = 0;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = true;
                    c.m_Max = 10;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Heal;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.StatBonus,
                            ValueShared = AbilitySharedValue.StatBonus
                        }
                    };
                    c.Modifier = 1;
                });
                bp.m_Icon = OracleRevelationShellOfSuccorIcon;
                bp.Stacking = StackingType.Replace;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
            });
            var OracleRevelationShellOfSuccorAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationShellOfSuccorAbility", bp => {
                bp.SetName("Shell of Succor");
                bp.SetDescription("You can surround an ally with bolstering energies that supplement its health and grant it extra vigor. With a touch from you (a standard action), one creature gains a ward of restorative " +
                    "energy, granting it a number of temporary hit points equal to your Charisma bonus + 1d6 per 2 oracle levels you have (maximum 10d6). These temporary hit points last 1 minute per oracle level you have. " +
                    "You can use this revelation once per day, plus one additional time at 11th and 19th levels. You must be at least 3rd level before selecting this revelation.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleRevelationShellOfSuccorBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = 0,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage
                                }
                            },
                            IsNotDispelable = true
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 0;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationShellOfSuccorResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = OracleRevelationShellOfSuccorIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationShellOfSuccorAbility.Duration", "1 minute/level");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationShellOfSuccor = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationShellOfSuccor", bp => {
                bp.SetName("Shell of Succor");
                bp.SetDescription("You can surround an ally with bolstering energies that supplement its health and grant it extra vigor. With a touch from you (a standard action), one creature gains a ward of restorative " +
                    "energy, granting it a number of temporary hit points equal to your Charisma bonus + 1d6 per 2 oracle levels you have (maximum 10d6). These temporary hit points last 1 minute per oracle level you have. " +
                    "You can use this revelation once per day, plus one additional time at 11th and 19th levels. You must be at least 3rd level before selecting this revelation.");
                bp.m_Icon = OracleRevelationShellOfSuccorIcon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationShellOfSuccorAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] {
                        OracleRevelationShellOfSuccorAbility.ToReference<BlueprintAbilityReference>()
                    };
                    c.Stat = StatType.Charisma;
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 3;
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationShellOfSuccorResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleSuccorMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherSuccorMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistSuccorMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoSuccorMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationShellOfSuccor.ToReference<BlueprintFeatureReference>());
            //Teamwork Mastery 
            var CavalierTacticianAbility = Resources.GetBlueprint<BlueprintAbility>("3ff8ef7ba7b5be0429cf32cd4ddf637c"); //For icon
            var AlliedSpellcaster = Resources.GetBlueprintReference<BlueprintFeatureReference>("9093ceeefe9b84746a5993d619d7c86f");
            var BackToBack = Resources.GetBlueprintReference<BlueprintFeatureReference>("c920f2cd2244d284aa69a146aeefcb2c");
            var CoordinatedDefence = Resources.GetBlueprintReference<BlueprintFeatureReference>("992fd59da1783de49b135ad89142c6d7");
            var CoordinatedManeuvers = Resources.GetBlueprintReference<BlueprintFeatureReference>("b186cea78dce3a04aacff0a81786008c");
            var Outflank = Resources.GetBlueprintReference<BlueprintFeatureReference>("422dab7309e1ad343935f33a4d6e9f11");
            var PreciseStrike = Resources.GetBlueprintReference<BlueprintFeatureReference>("5662d1b793db90c4b9ba68037fd2a768");
            var ShakeItOff = Resources.GetBlueprintReference<BlueprintFeatureReference>("6337b37f2a7c11b4ab0831d6780bce2a");
            var ShieldedCaster = Resources.GetBlueprintReference<BlueprintFeatureReference>("0b707584fc2ea724aa72c396c2230dc7");
            var ShieldWall = Resources.GetBlueprintReference<BlueprintFeatureReference>("8976de442862f82488a4b138a0a89907");
            var SiezeTheMoment = Resources.GetBlueprintReference<BlueprintFeatureReference>("1191ef3065e6f8e4f9fbe1b7e3c0f760");
            var TandemTrip = Resources.GetBlueprintReference<BlueprintFeatureReference>("d26eb8ab2aabd0e45a4d7eec0340bbce");
            var OracleRevelationTeamworkMasteryResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationTeamworkMasteryResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Charisma,
                };
            });
            var CavalierTacticianAlliedSpellcasterBuff = Resources.GetBlueprint<BlueprintBuff>("3de0359d9480cb549ab6cf1eac51f9dc");
            var CavalierTacticianBackToBackBuff = Resources.GetBlueprint<BlueprintBuff>("693964e674883e74b8d0005dbf4a4e6b");
            var CavalierTacticianCoordinatedDefenceBuff = Resources.GetBlueprint<BlueprintBuff>("9c179de4894c295499822714878f3590");
            var CavalierTacticianOutflankBuff = Resources.GetBlueprint<BlueprintBuff>("c7223802e54e8524c8b1e5c71df22f7b");
            var CavalierTacticianPreciseStrikeBuff = Resources.GetBlueprint<BlueprintBuff>("44569e9e95364bf42b1071382a8a89da");
            var CavalierTacticianShakeItOffBuff = Resources.GetBlueprint<BlueprintBuff>("731a11dcc952e744f8a88768e07a0542");
            var CavalierTacticianShieldedCasterBuff = Resources.GetBlueprint<BlueprintBuff>("2f5768f642de59f40acd5211a627a237");
            var CavalierTacticianShieldWallBuff = Resources.GetBlueprint<BlueprintBuff>("e5079510480031146992dafde835c3b8");
            var CavalierTacticianSiezeTheMomentBuff = Resources.GetBlueprint<BlueprintBuff>("953c3dbda63dcdb4aad6c54c1a4590d0");
            var CavalierTacticianTandemTripBuff = Resources.GetBlueprint<BlueprintBuff>("965ea9716b87f4b46a6a8f50523393bd");
            var CavalierTacticianVolleyFireBuff = Resources.GetBlueprint<BlueprintBuff>("a6298b0f87fc7694086cd8eac9d6a2aa");
            var OracleRevelationTeamworkMasteryAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationTeamworkMasteryAbility", bp => {
                bp.SetName("Teamwork Mastery");
                bp.SetDescription("You are an immaculate team player and can distribute your team-based insights to your allies with a touch. You can touch an ally as a standard action to " +
                    "confer upon it the benefits of any one teamwork feat that you have. This effect persists for a number of rounds equal to 1/2 your oracle level (minimum 1). You can confer " +
                    "the benefits of a teamwork feat you have a number of times per day equal to 3 + your Charisma modifier.");
                bp.AddComponent<AbilityApplyFact>(c => {
                    c.m_Restriction = AbilityApplyFact.FactRestriction.CasterHasFact;
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        CavalierTacticianAlliedSpellcasterBuff.ToReference<BlueprintUnitFactReference>(),
                        CavalierTacticianBackToBackBuff.ToReference<BlueprintUnitFactReference>(),
                        CavalierTacticianCoordinatedDefenceBuff.ToReference<BlueprintUnitFactReference>(),
                        CavalierTacticianOutflankBuff.ToReference<BlueprintUnitFactReference>(),
                        CavalierTacticianPreciseStrikeBuff.ToReference<BlueprintUnitFactReference>(),
                        CavalierTacticianShakeItOffBuff.ToReference<BlueprintUnitFactReference>(),
                        CavalierTacticianShieldedCasterBuff.ToReference<BlueprintUnitFactReference>(),
                        CavalierTacticianShieldWallBuff.ToReference<BlueprintUnitFactReference>(),
                        CavalierTacticianSiezeTheMomentBuff.ToReference<BlueprintUnitFactReference>(),
                        CavalierTacticianTandemTripBuff.ToReference<BlueprintUnitFactReference>(),
                        CavalierTacticianVolleyFireBuff.ToReference<BlueprintUnitFactReference>()
                    };
                    c.m_HasDuration = true;
                    c.m_Duration = new ContextDurationValue() {
                        Rate = DurationRate.Rounds,
                        DiceType = DiceType.Zero,
                        DiceCountValue = 0,
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage
                        }
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMax = false;
                    c.m_Max = 0;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationTeamworkMasteryResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Other;
                });
                bp.m_Icon = CavalierTacticianAbility.m_Icon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Empower;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationTeamworkMasteryAbility.Duration", "1 minute/2 levels");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationTeamworkMasterySelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("OracleRevelationTeamworkMasterySelection", bp => {
                bp.SetName("Teamwork Mastery");
                bp.SetDescription("You are an immaculate team player and can distribute your team-based insights to your allies with a touch. This revelation grants you a bonus teamwork feat. " +
                    "You must meet the teamwork feat’s prerequisites, if any. Additionally, you can touch an ally as a standard action to confer upon it the benefits of any one teamwork feat that " +
                    "you have. This effect persists for a number of rounds equal to 1/2 your oracle level (minimum 1). You can confer the benefits of a teamwork feat you have a number of times " +
                    "per day equal to 3 + your Charisma modifier.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationTeamworkMasteryAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationTeamworkMasteryResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    AlliedSpellcaster,
                    BackToBack,
                    CoordinatedDefence,
                    CoordinatedManeuvers,
                    Outflank,
                    PreciseStrike,
                    ShakeItOff,
                    ShieldedCaster,
                    ShieldWall,
                    SiezeTheMoment,
                    TandemTrip
                };
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleSuccorMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherSuccorMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistSuccorMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoSuccorMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.Group = FeatureGroup.TeamworkFeat;
                bp.IgnorePrerequisites = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationTeamworkMasterySelection.AddComponent<PrerequisiteNoFeature>(c => {
                c.m_Feature = OracleRevelationTeamworkMasterySelection.ToReference<BlueprintFeatureReference>();
                });

            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationTeamworkMasterySelection.ToReference<BlueprintFeatureReference>());



            MysteryTools.RegisterMystery(OracleSuccorMysteryFeature);
            MysteryTools.RegisterSecondMystery(OracleSuccorMysteryFeature);
            MysteryTools.RegisterEnlightendPhilosopherMystery(EnlightnedPhilosopherSuccorMysteryFeature);
            MysteryTools.RegisterSecondEnlightendPhilosopherMystery(EnlightnedPhilosopherSuccorMysteryFeature);
            MysteryTools.RegisterHerbalistMystery(DivineHerbalistSuccorMysteryFeature);
            MysteryTools.RegisterSecondHerbalistMystery(DivineHerbalistSuccorMysteryFeature);
            MysteryTools.RegisterOceansEchoMystery(OceansEchoSuccorMysteryFeature);
            MysteryTools.RegisterSecondOceansEchoMystery(OceansEchoSuccorMysteryFeature);
        }
    }
}
