using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Domains {
    //Adding Druid versions of normal domains for any archetypes that would need them. Adding archetypes to abilities contextrankconfigs must be done after the archetypes are loaded.
    internal class BaseDomainDruidPatch {
        public static void AddBaseDomainDruidPatch() {
            var DruidClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("610d836f3a3a9ed42a4349b62f002e96");
            //Charm
            var CharmDomainBaseFeature = Resources.GetBlueprint<BlueprintFeature>("4847d450fbef9b444abcc3a82337b426");

            var CharmDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("84cd24a110af59140b066bc2c69619bd");
            var CharmDomainBaseResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("e3ec8bc31cab642488396d259a8ab0d9");
            var CharmDomainBaseFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("CharmDomainBaseFeatureDruid", bp => {
                bp.SetName("Charm Domain");
                bp.SetDescription("\nYou can baffle and befuddle foes with a {g|Encyclopedia:TouchAttack}touch{/g} or a smile, and your beauty and grace are divine. Dazing Touch: You can " +
                    "cause a living creature to become dazed for 1 {g|Encyclopedia:Combat_Round}round{/g} as a melee touch {g|Encyclopedia:Attack}attack{/g}. Creatures with more " +
                    "{g|Encyclopedia:Hit_Dice}Hit Dice{/g} than your level in the class that gave you access to this domain are unaffected. You can use this ability a number of times per " +
                    "day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. Charming Smile: At 8th level, you can cast charm person as a {g|Encyclopedia:Swift_Action}swift action{/g}, " +
                    "with a {g|Encyclopedia:DC}DC{/g} of 10 + 1/2 your level in the class that gave you access to this domain + your Wisdom modifier. The effect of charm person lasts for 1 round. " +
                    "You can use this ability a number of times per day equal to your {g|Encyclopedia:Caster_Level}caster level{/g}.");
                bp.m_Icon = CharmDomainBaseFeature.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { CharmDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = CharmDomainBaseResource;
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { CharmDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var CharmDomainGreaterFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("d1fee57aa8f12b849b5abd5f2b7c4616");
            var CharmDomainSpellList = Resources.GetBlueprintReference<BlueprintSpellListReference>("31c742d02fc33204cad4e02dddf028dd");
            var CharmDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("CharmDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass;
                    c.m_SpellList = CharmDomainSpellList;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var CharmDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("CharmDomainProgressionDruid", bp => {
                bp.SetName("Charm Domain");
                bp.SetDescription("\nYou can baffle and befuddle foes with a {g|Encyclopedia:TouchAttack}touch{/g} or a smile, and your beauty and grace are divine. Dazing Touch: You can " +
                    "cause a living creature to become dazed for 1 {g|Encyclopedia:Combat_Round}round{/g} as a melee touch {g|Encyclopedia:Attack}attack{/g}. Creatures with more " +
                    "{g|Encyclopedia:Hit_Dice}Hit Dice{/g} than your level in the class that gave you access to this domain are unaffected. You can use this ability a number of times per " +
                    "day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. Charming Smile: At 8th level, you can cast charm person as a {g|Encyclopedia:Swift_Action}swift action{/g}, " +
                    "with a {g|Encyclopedia:DC}DC{/g} of 10 + 1/2 your level in the class that gave you access to this domain + your Wisdom modifier. The effect of charm person lasts for 1 round. " +
                    "You can use this ability a number of times per day equal to your {g|Encyclopedia:Caster_Level}caster level{/g}. Domain {g|Encyclopedia:Spell}Spells{/g}: hypnotism, " +
                    "hideous laughter, deep slumber, rainbow pattern, dominate person, mass eagle's splendor, insanity, euphoric tranquility, dominate monster.");
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.m_CharacterClass = DruidClass;
                    c.Level = 1;
                });
                bp.m_AllowNonContextActions = false;
                bp.Groups = new FeatureGroup[] { FeatureGroup.DruidDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = DruidClass,
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, CharmDomainBaseFeatureDruid, CharmDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(8, CharmDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(CharmDomainBaseFeatureDruid, CharmDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            //Community
            var CommunityDomainBaseFeature = Resources.GetBlueprint<BlueprintFeature>("102d61a114786894bb2b30568943ef1f");

            var CommunityDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("b1b8efd70ba5dd84aa6985d46dc299d5");
            var CommunityDomainBaseResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("7ffdbca082a175b498c25064a28262e2");
            var CommunityDomainBaseFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("CommunityDomainBaseFeatureDruid", bp => {
                bp.SetName("Community Domain");
                bp.SetDescription("\nYour {g|Encyclopedia:TouchAttack}touch{/g} can {g|Encyclopedia:Healing}heal{/g} wounds, and your presence instills unity and strengthens emotional bonds. " +
                    "Calming Touch: You can touch a creature as a {g|Encyclopedia:Standard_Actions}standard action{/g} to heal it of {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} + " +
                    "1 point per level in the class that gave you access to this domain. This touch also removes the fatigued, shaken, and sickened conditions (but has no effect on more severe " +
                    "conditions). You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. Guarded Hearth: At 8th level, you can create a " +
                    "ward that protects a specified area. Creating this ward is a {g|Encyclopedia:Full_Round_Action}full-round action{/g}. All friendly creatures in the area receive a resistance " +
                    "{g|Encyclopedia:Bonus}bonus{/g} equal to your Wisdom modifier on all {g|Encyclopedia:Saving_Throw}saving throws{/g} and an equal competence bonus on " +
                    "{g|Encyclopedia:Attack}attack rolls{/g} while inside the warded area. The ward lasts for 1 hour per level in the class that gave you access to this domain. You can use this " +
                    "ability once per day.");
                bp.m_Icon = CommunityDomainBaseFeature.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { CommunityDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = CommunityDomainBaseResource;
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { CommunityDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var CommunityDomainGreaterFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("4cddbb24833e1d24ea1ff0f59574284a");
            var CommunityDomainSpellList = Resources.GetBlueprintReference<BlueprintSpellListReference>("75576ed8cab010644a11f9ecd512a7f9");
            var CommunityDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("CommunityDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass;
                    c.m_SpellList = CommunityDomainSpellList;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var CommunityDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("CommunityDomainProgressionDruid", bp => {
                bp.SetName("Community Domain");
                bp.SetDescription("\nYour {g|Encyclopedia:TouchAttack}touch{/g} can {g|Encyclopedia:Healing}heal{/g} wounds, and your presence instills unity and strengthens emotional bonds. " +
                    "Calming Touch: You can touch a creature as a {g|Encyclopedia:Standard_Actions}standard action{/g} to heal it of {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} + " +
                    "1 point per level in the class that gave you access to this domain. This touch also removes the fatigued, shaken, and sickened conditions (but has no effect on more severe " +
                    "conditions). You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. Guarded Hearth: At 8th level, you can create a " +
                    "ward that protects a specified area. Creating this ward is a {g|Encyclopedia:Full_Round_Action}full-round action{/g}. All friendly creatures in the area receive a resistance " +
                    "{g|Encyclopedia:Bonus}bonus{/g} equal to your Wisdom modifier on all {g|Encyclopedia:Saving_Throw}saving throws{/g} and an equal competence bonus on " +
                    "{g|Encyclopedia:Attack}attack rolls{/g} while inside the warded area. The ward lasts for 1 hour per level in the class that gave you access to this domain. You can use this " +
                    "ability once per day. Domain {g|Encyclopedia:Spell}Spells{/g}: bless, communal protection from {g|Encyclopedia:Alignment}alignment{/g}, prayer, communal protection from " +
                    "energy, burst of glory, communal stoneskin, greater restoration, legendary proportions, mass heal.");
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.m_CharacterClass = DruidClass;
                    c.Level = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.DruidDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = DruidClass,
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, CommunityDomainBaseFeatureDruid, CommunityDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(8, CommunityDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(CommunityDomainBaseFeatureDruid, CommunityDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            //Knowledge
            var KnowledgeDomainBaseFeature= Resources.GetBlueprint<BlueprintFeature>("5335f015063776d429a0b5eab97eb060");

            var KnowledgeDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("02a79a205bce6f5419dcdf26b64f13c6");
            var KnowledgeDomainBaseResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("f88f616a4b6bd5f419025115c52cb329");
            var KnowledgeDomainBaseFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("KnowledgeDomainBaseFeatureDruid", bp => {
                bp.SetName("Knowledge Domain");
                bp.SetDescription("\nYou are a scholar and a sage of legends. In addition, you treat all Knowledge and Lore {g|Encyclopedia:Skills}skills{/g} as class skills. Void Form: You can " +
                    "become semi-tangible as a {g|Encyclopedia:Standard_Actions}standard action{/g}. While in this form, you are immune to {g|Encyclopedia:Critical}critical hits{/g} and gain a +1 " +
                    "deflection {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g}. This bonus increases by 1 at 8th level and every 4 levels thereafter. You can use this power " +
                    "a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class that gave you access to this domain. Teaching Moment: At 8th level as a " +
                    "{g|Encyclopedia:Swift_Action}swift action{/g} you can grant all allies within 30 feet special insights. Once during the next minute, each affected creature can choose to " +
                    "{g|Encyclopedia:Dice}roll{/g} twice and take the better result before attempting an {g|Encyclopedia:Attack}attack roll{/g}, {g|Encyclopedia:Ability_Scores}ability check{/g}, " +
                    "skill {g|Encyclopedia:Check}check{/g}, or {g|Encyclopedia:Saving_Throw}saving throw{/g}. You can use this ability once per day at 8th level, and one additional time per day for " +
                    "every 4 levels in the class that gave you access to this domain beyond 8th.");
                bp.m_Icon = KnowledgeDomainBaseFeature.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { KnowledgeDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = KnowledgeDomainBaseResource;
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { KnowledgeDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var KnowledgeDomainGreaterFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("74ac5dbc420501c4cae29a9db24e4e3a");
            var KnowledgeDomainSpellList = Resources.GetBlueprintReference<BlueprintSpellListReference>("384627980c2a60a43800f14029fbb8a7");
            var KnowledgeDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("KnowledgeDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass;
                    c.m_SpellList = KnowledgeDomainSpellList;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var KnowledgeDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("KnowledgeDomainProgressionDruid", bp => {
                bp.SetName("Knowledge Domain");
                bp.SetDescription("\nYou are a scholar and a sage of legends. In addition, you treat all Knowledge and Lore {g|Encyclopedia:Skills}skills{/g} as class skills. Void Form: You can " +
                    "become semi-tangible as a {g|Encyclopedia:Standard_Actions}standard action{/g}. While in this form, you are immune to {g|Encyclopedia:Critical}critical hits{/g} and gain a +1 " +
                    "deflection {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g}. This bonus increases by 1 at 8th level and every 4 levels thereafter. You can use this power " +
                    "a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class that gave you access to this domain. Teaching Moment: At 8th level as a " +
                    "{g|Encyclopedia:Swift_Action}swift action{/g} you can grant all allies within 30 feet special insights. Once during the next minute, each affected creature can choose to " +
                    "{g|Encyclopedia:Dice}roll{/g} twice and take the better result before attempting an {g|Encyclopedia:Attack}attack roll{/g}, {g|Encyclopedia:Ability_Scores}ability check{/g}, " +
                    "skill {g|Encyclopedia:Check}check{/g}, or {g|Encyclopedia:Saving_Throw}saving throw{/g}. You can use this ability once per day at 8th level, and one additional time per day for " +
                    "every 4 levels in the class that gave you access to this domain beyond 8th. Domain {g|Encyclopedia:Spell}Spells{/g}: true strike, fox's cunning, communal see invisibility, " +
                    "{g|Encyclopedia:Injury_Death}death{/g} ward, true seeing, mass fox's cunning, power word blind, power word stun, power word kill.");
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.m_CharacterClass = DruidClass;
                    c.Level = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.DruidDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = DruidClass,
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, KnowledgeDomainBaseFeatureDruid, KnowledgeDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(8, KnowledgeDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(KnowledgeDomainBaseFeatureDruid, KnowledgeDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            //Nobility
            var NobilityDomainBaseFeature = Resources.GetBlueprint<BlueprintFeature>("a1a7f3dd904ed8e45b074232f48190d1");

            var NobilityDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("7a305ef528cb7884385867a2db410102");
            var NobilityDomainBaseResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("3fc6e1f3acbcb0e4c83badf7709ce53d");
            var NobilityDomainBaseFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("NobilityDomainBaseFeatureDruid", bp => {
                bp.SetName("Nobility Domain");
                bp.SetDescription("\nYou are a great leader, an inspiration to all who follow the teachings of your faith. Inspiring Word: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, " +
                    "you can speak an inspiring word to a creature within 30 feet. That creature receives a +2 morale {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Attack}attack rolls{/g}, " +
                    "{g|Encyclopedia:Skills}skill checks{/g}, {g|Encyclopedia:Ability_Scores}ability checks{/g}, and {g|Encyclopedia:Saving_Throw}saving throws{/g} for a number of " +
                    "{g|Encyclopedia:Combat_Round}rounds{/g} equal to 1/2 your level in the class that gave you access to this domain (minimum 1). You can use this power a number of times per day " +
                    "equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. Inspiring Command: At 8th level, you can issue an inspiring command to your allies, who must all be within 30 feet " +
                    "of you. Affected allies gain a +2 insight bonus on attack {g|Encyclopedia:Dice}rolls{/g}, {g|Encyclopedia:Armor_Class}AC{/g}, {g|Encyclopedia:CMD}Combat Maneuver Defense{/g}, " +
                    "and skill {g|Encyclopedia:Check}checks{/g} for a number of rounds equal to 1/2 your level in the class that gave you access to this domain (minimum 1). You can use this power " +
                    "a number of times per day equal to 3 + your Wisdom modifier.");
                bp.m_Icon = NobilityDomainBaseFeature.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { NobilityDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = NobilityDomainBaseResource;
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { NobilityDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var NobilityDomainGreaterFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("75acf3f9598248344b76f0b87ad27ac1");
            var NobilityDomainSpellList = Resources.GetBlueprintReference<BlueprintSpellListReference>("3de1e283971828f4896a4140acd3c84c");
            var NobilityDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("NobilityDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass;
                    c.m_SpellList = NobilityDomainSpellList;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var NobilityDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("NobilityDomainProgressionDruid", bp => {
                bp.SetName("Nobility Domain");
                bp.SetDescription("\nYou are a great leader, an inspiration to all who follow the teachings of your faith. Inspiring Word: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, " +
                    "you can speak an inspiring word to a creature within 30 feet. That creature receives a +2 morale {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Attack}attack rolls{/g}, " +
                    "{g|Encyclopedia:Skills}skill checks{/g}, {g|Encyclopedia:Ability_Scores}ability checks{/g}, and {g|Encyclopedia:Saving_Throw}saving throws{/g} for a number of " +
                    "{g|Encyclopedia:Combat_Round}rounds{/g} equal to 1/2 your level in the class that gave you access to this domain (minimum 1). You can use this power a number of times per day " +
                    "equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. Inspiring Command: At 8th level, you can issue an inspiring command to your allies, who must all be within 30 feet " +
                    "of you. Affected allies gain a +2 insight bonus on attack {g|Encyclopedia:Dice}rolls{/g}, {g|Encyclopedia:Armor_Class}AC{/g}, {g|Encyclopedia:CMD}Combat Maneuver Defense{/g}, " +
                    "and skill {g|Encyclopedia:Check}checks{/g} for a number of rounds equal to 1/2 your level in the class that gave you access to this domain (minimum 1). You can use this power " +
                    "a number of times per day equal to 3 + your Wisdom modifier. Domain {g|Encyclopedia:Spell}Spells{/g}: divine favor, grace, magical vestment, heroism, dominate person, brilliant " +
                    "inspiration, greater heroism, frightful aspect, overwhelming presence.");
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.m_CharacterClass = DruidClass;
                    c.Level = 1;
                });
                bp.Groups = new FeatureGroup[] {FeatureGroup.DruidDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = DruidClass,
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, NobilityDomainBaseFeatureDruid, NobilityDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(8, NobilityDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(NobilityDomainBaseFeatureDruid, NobilityDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            //Protection
            var ProtectionDomainBaseFeature = Resources.GetBlueprint<BlueprintFeature>("a05a8959c594daa40a1c5add79566566");

            var ProtectionDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("c5815bd0bf87bdb4fa9c440c8088149b");
            var ProtectionDomainBaseReapplyClassFeature = Resources.GetBlueprint<BlueprintFeature>("adddb0b1553f4dbcbb3f546df918af22");
            var ProtectionDomainBaseFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("ProtectionDomainBaseFeatureDruid", bp => {
                bp.SetName("Protection Domain");
                bp.SetDescription("\nYour faith is your greatest source of protection, and you can use that faith to defend others. In addition, you receive a +1 resistance {g|Encyclopedia:Bonus}bonus{/g} on " +
                    "{g|Encyclopedia:Saving_Throw}saving throws{/g}. This bonus increases by 1 for every 5 levels you possess in the class that gave you access to this domain. Resistant " +
                    "{g|Encyclopedia:TouchAttack}Touch{/g}: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can touch an ally to grant them your resistance bonus for 1 minute. When you use this " +
                    "ability, you lose your resistance bonus granted by the Protection domain for 1 minute. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} " +
                    "modifier. Aura of Protection: At 8th level, you can emit a 30-foot aura of protection for a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class " +
                    "that gave you access to this domain. You and your allies within this aura gain a +1 deflection bonus to {g|Encyclopedia:Armor_Class}AC{/g} and resistance 5 against all elements (acid, " +
                    "cold, electricity, fire, and sonic). The deflection bonus increases by +1 for every four levels you possess in the class that gave you access to this domain beyond 8th. At 14th level, " +
                    "the resistance against all elements increases to 10. These rounds do not need to be consecutive.");
                bp.m_Icon = ProtectionDomainBaseFeature.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { 
                        ProtectionDomainBaseAbility.ToReference<BlueprintUnitFactReference>(),
                        ProtectionDomainBaseReapplyClassFeature.ToReference<BlueprintUnitFactReference>()
                    };
                });                
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var ProtectionDomainGreaterFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("e2e9d41bfa7aa364592b9d57dd74c9db");
            var ProtectionDomainSpellList = Resources.GetBlueprintReference<BlueprintSpellListReference>("93228f4df23d2d448a0db59141af8aed");
            var ProtectionDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("ProtectionDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass;
                    c.m_SpellList = ProtectionDomainSpellList;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var ProtectionDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("ProtectionDomainProgressionDruid", bp => {
                bp.SetName("Protection Domain");
                bp.SetDescription("\nYour faith is your greatest source of protection, and you can use that faith to defend others. In addition, you receive a +1 resistance {g|Encyclopedia:Bonus}bonus{/g} on " +
                    "{g|Encyclopedia:Saving_Throw}saving throws{/g}. This bonus increases by 1 for every 5 levels you possess in the class that gave you access to this domain. Resistant " +
                    "{g|Encyclopedia:TouchAttack}Touch{/g}: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can touch an ally to grant them your resistance bonus for 1 minute. When you use this " +
                    "ability, you lose your resistance bonus granted by the Protection domain for 1 minute. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} " +
                    "modifier. Aura of Protection: At 8th level, you can emit a 30-foot aura of protection for a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class " +
                    "that gave you access to this domain. You and your allies within this aura gain a +1 deflection bonus to {g|Encyclopedia:Armor_Class}AC{/g} and resistance 5 against all elements (acid, " +
                    "cold, electricity, fire, and sonic). The deflection bonus increases by +1 for every four levels you possess in the class that gave you access to this domain beyond 8th. At 14th level, " +
                    "the resistance against all elements increases to 10. These rounds do not need to be consecutive. Domain {g|Encyclopedia:Spell}Spells{/g}: protection from " +
                    "{g|Encyclopedia:Alignment}alignment{/g}, barkskin, protection from energy, communal protection from energy, {g|Encyclopedia:Spell_Resistance}spell resistance{/g}, communal stoneskin, " +
                    "greater {g|Encyclopedia:Healing}restoration{/g}, protection from spells, seamantle.");
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.m_CharacterClass = DruidClass;
                    c.Level = 1;
                });
                bp.Groups = new FeatureGroup[] {FeatureGroup.DruidDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = DruidClass,
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, ProtectionDomainBaseFeatureDruid, ProtectionDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(8, ProtectionDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(ProtectionDomainBaseFeatureDruid, ProtectionDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            //Repose
            var ReposeDomainBaseFeature = Resources.GetBlueprint<BlueprintFeature>("8526bc808c303034cb2b7832bccf1482");

            var ReposeDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("30dfb2e83f9de7246ad6cb44e36f2b4d");
            var ReposeDomainBaseResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("2b3b95e5ad1dbbf46be85b7f48a60f6a");
            var ReposeDomainBaseFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("ReposeDomainBaseFeatureDruid", bp => {
                bp.SetName("Repose Domain");
                bp.SetDescription("\nYou see {g|Encyclopedia:Injury_Death}death{/g} not as something to be feared, but as a final rest and reward for a life well spent. The taint of undeath " +
                    "is a mockery of what you hold dear. Gentle Rest: Your {g|Encyclopedia:TouchAttack}touch{/g} can fill a creature with lethargy, causing a living creature to become staggered " +
                    "for 1 {g|Encyclopedia:Combat_Round}round{/g} as a melee touch {g|Encyclopedia:Attack}attack{/g}. If you touch a staggered living creature, that creature falls asleep for " +
                    "1 round instead. Undead creatures touched are staggered for a number of rounds equal to your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. You can use this ability a number " +
                    "of times per day equal to 3 + your Wisdom modifier. Ward Against Death: At 8th level, you can emit a 30-foot aura that wards against death for a number of rounds per day " +
                    "equal to your level in the class that gave you access to this domain. Living creatures in this area are immune to all death effects, energy drain, and effects that cause " +
                    "negative levels. This ward does not remove negative levels that a creature has already gained, but the negative levels have no effect while the creature is inside the warded " +
                    "area. These rounds do not need to be consecutive.");
                bp.m_Icon = ReposeDomainBaseFeature.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ReposeDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = ReposeDomainBaseResource;
                    c.RestoreAmount = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });


            var ReposeDomainGreaterFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("e06bfe3ad866c0e4f8a3d5516b844881");
            var ReposeDomainSpellList = Resources.GetBlueprintReference<BlueprintSpellListReference>("5376474a39713514ca2135d6f9584563");
            var ReposeDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("ReposeDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass;
                    c.m_SpellList = ReposeDomainSpellList;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var ReposeDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("ReposeDomainProgressionDruid", bp => {
                bp.SetName("Repose Domain");
                bp.SetDescription("\nYou see {g|Encyclopedia:Injury_Death}death{/g} not as something to be feared, but as a final rest and reward for a life well spent. The taint of undeath " +
                    "is a mockery of what you hold dear. Gentle Rest: Your {g|Encyclopedia:TouchAttack}touch{/g} can fill a creature with lethargy, causing a living creature to become staggered " +
                    "for 1 {g|Encyclopedia:Combat_Round}round{/g} as a melee touch {g|Encyclopedia:Attack}attack{/g}. If you touch a staggered living creature, that creature falls asleep for " +
                    "1 round instead. Undead creatures touched are staggered for a number of rounds equal to your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. You can use this ability a number " +
                    "of times per day equal to 3 + your Wisdom modifier. Ward Against Death: At 8th level, you can emit a 30-foot aura that wards against death for a number of rounds per day " +
                    "equal to your level in the class that gave you access to this domain. Living creatures in this area are immune to all death effects, energy drain, and effects that cause " +
                    "negative levels. This ward does not remove negative levels that a creature has already gained, but the negative levels have no effect while the creature is inside the warded " +
                    "area. These rounds do not need to be consecutive. Domain {g|Encyclopedia:Spell}Spells{/g}: doom, scare, vampiric touch, death ward, slay living, undeath to death, destruction, " +
                    "waves of exhaustion, wail of the banshee.");
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.m_CharacterClass = DruidClass;
                    c.Level = 1;
                });
                bp.Groups = new FeatureGroup[] {FeatureGroup.DruidDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = DruidClass,
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, ReposeDomainBaseFeatureDruid, ReposeDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(8, ReposeDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(ReposeDomainBaseFeatureDruid, ReposeDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            //Rune
            var RuneDomainBaseFeature = Resources.GetBlueprint<BlueprintFeature>("b74c64a0152c7ee46b13ecdd72dda6f3");


            var RuneDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("56ad05dedfd9df84996f62108125eed5");
            var RuneDomainBaseAbilityAcid = Resources.GetBlueprint<BlueprintAbility>("92c821ecc8d73564bad15a8a07ed40f2");
            var RuneDomainBaseAbilityCold = Resources.GetBlueprint<BlueprintAbility>("2b81ff42fcbe9434eaf00fb0a873f579");
            var RuneDomainBaseAbilityElectricity = Resources.GetBlueprint<BlueprintAbility>("b67978e3d5a6c9247a393237bc660339");
            var RuneDomainBaseAbilityFire = Resources.GetBlueprint<BlueprintAbility>("eddfe26a8a3892b47add3cb08db7069d");
            var RuneDomainBaseResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("fd039f4d480407e41bfd301c9db0a5bd");
            var RuneDomainBaseFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("RuneDomainBaseFeatureDruid", bp => {
                bp.SetName("Rune Domain");
                bp.SetDescription("\nIn strange and eldritch runes you find potent magic. Blast Rune: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can create a blast rune in a desired " +
                    "location. Any creature entering a 5-foot area around the rune takes {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} + 1 point for every two levels you possess " +
                    "in the class that gave you access to this domain. This rune deals either acid, cold, electricity, or {g|Encyclopedia:Energy_Damage}fire damage{/g}, decided when you create the rune. " +
                    "The rune lasts a number of {g|Encyclopedia:Combat_Round}rounds{/g} equal to your level in the class that gave you access to this domain. You can use this ability a number of times per " +
                    "day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. Warding Rune: At 8th level, you can create a warding rune in a desired location. Any creature entering a 5-foot area " +
                    "around the rune must make a {g|Encyclopedia:Saving_Throw}Will save{/g} or they will not be able to {g|Encyclopedia:Attack}attack{/g} for a number of rounds equal to 1/2 your level in " +
                    "the class that gave you access to this domain. The rune lasts a number of rounds equal to your level in the class that gave you access to this domain. You can use this ability once per " +
                    "day at 8th level and one additional time per day for every four levels in the class that gave you access to this domain beyond 8th.");
                bp.m_Icon = RuneDomainBaseFeature.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { 
                        RuneDomainBaseAbilityAcid.ToReference<BlueprintUnitFactReference>(),
                        RuneDomainBaseAbilityCold.ToReference<BlueprintUnitFactReference>(),
                        RuneDomainBaseAbilityElectricity.ToReference<BlueprintUnitFactReference>(),
                        RuneDomainBaseAbilityFire.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = RuneDomainBaseResource;
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { RuneDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });


            var RuneDomainGreaterFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("8a2064b6e41c90e4c8a2880deccac139");
            var RuneDomainSpellList = Resources.GetBlueprintReference<BlueprintSpellListReference>("30076fe3d5f4ef845a7bafb0be57fe44");
            var RuneDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("RuneDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass;
                    c.m_SpellList = RuneDomainSpellList;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var RuneDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("RuneDomainProgressionDruid", bp => {
                bp.SetName("Rune Domain");
                bp.SetDescription("\nIn strange and eldritch runes you find potent magic. Blast Rune: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can create a blast rune in a desired " +
                    "location. Any creature entering a 5-foot area around the rune takes {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} + 1 point for every two levels you possess " +
                    "in the class that gave you access to this domain. This rune deals either acid, cold, electricity, or {g|Encyclopedia:Energy_Damage}fire damage{/g}, decided when you create the rune. " +
                    "The rune lasts a number of {g|Encyclopedia:Combat_Round}rounds{/g} equal to your level in the class that gave you access to this domain. You can use this ability a number of times per " +
                    "day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. Warding Rune: At 8th level, you can create a warding rune in a desired location. Any creature entering a 5-foot area " +
                    "around the rune must make a {g|Encyclopedia:Saving_Throw}Will save{/g} or they will not be able to {g|Encyclopedia:Attack}attack{/g} for a number of rounds equal to 1/2 your level in " +
                    "the class that gave you access to this domain. The rune lasts a number of rounds equal to your level in the class that gave you access to this domain. You can use this ability once per " +
                    "day at 8th level and one additional time per day for every four levels in the class that gave you access to this domain beyond 8th. Domain {g|Encyclopedia:Spell}Spells{/g}: protection " +
                    "from {g|Encyclopedia:Alignment}alignment{/g}, protection from arrows, communal protection from arrows, communal protection from energy, " +
                    "{g|Encyclopedia:Spell_Resistance}spell resistance{/g}, greater dispel magic, power word blind, power word stun, power word kill.");
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.m_CharacterClass = DruidClass;
                    c.Level = 1;
                });
                bp.Groups = new FeatureGroup[] {FeatureGroup.DruidDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = DruidClass,
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, RuneDomainBaseFeatureDruid, RuneDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(8, RuneDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(RuneDomainBaseFeatureDruid, RuneDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
        }
    }
}
