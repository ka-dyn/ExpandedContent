using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;

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
                bp.SetDescription("\nYou can baffle and befuddle foes with a {g|Encyclopedia:TouchAttack}touch{/g} or a smile, and your beauty and grace are divine. \nDazing Touch: You can " +
                    "cause a living creature to become dazed for 1 {g|Encyclopedia:Combat_Round}round{/g} as a melee touch {g|Encyclopedia:Attack}attack{/g}. Creatures with more " +
                    "{g|Encyclopedia:Hit_Dice}Hit Dice{/g} than your level in the class that gave you access to this domain are unaffected. You can use this ability a number of times per " +
                    "day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. \nCharming Smile: At 8th level, you can cast charm person as a {g|Encyclopedia:Swift_Action}swift action{/g}, " +
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
                bp.SetDescription("\nYou can baffle and befuddle foes with a {g|Encyclopedia:TouchAttack}touch{/g} or a smile, and your beauty and grace are divine. \nDazing Touch: You can " +
                    "cause a living creature to become dazed for 1 {g|Encyclopedia:Combat_Round}round{/g} as a melee touch {g|Encyclopedia:Attack}attack{/g}. Creatures with more " +
                    "{g|Encyclopedia:Hit_Dice}Hit Dice{/g} than your level in the class that gave you access to this domain are unaffected. You can use this ability a number of times per " +
                    "day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. \nCharming Smile: At 8th level, you can cast charm person as a {g|Encyclopedia:Swift_Action}swift action{/g}, " +
                    "with a {g|Encyclopedia:DC}DC{/g} of 10 + 1/2 your level in the class that gave you access to this domain + your Wisdom modifier. The effect of charm person lasts for 1 round. " +
                    "You can use this ability a number of times per day equal to your {g|Encyclopedia:Caster_Level}caster level{/g}. \nDomain {g|Encyclopedia:Spell}Spells{/g}: hypnotism, " +
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
                    "\nCalming Touch: You can touch a creature as a {g|Encyclopedia:Standard_Actions}standard action{/g} to heal it of {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} + " +
                    "1 point per level in the class that gave you access to this domain. This touch also removes the fatigued, shaken, and sickened conditions (but has no effect on more severe " +
                    "conditions). You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. \nGuarded Hearth: At 8th level, you can create a " +
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
                    "\nCalming Touch: You can touch a creature as a {g|Encyclopedia:Standard_Actions}standard action{/g} to heal it of {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} + " +
                    "1 point per level in the class that gave you access to this domain. This touch also removes the fatigued, shaken, and sickened conditions (but has no effect on more severe " +
                    "conditions). You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. \nGuarded Hearth: At 8th level, you can create a " +
                    "ward that protects a specified area. Creating this ward is a {g|Encyclopedia:Full_Round_Action}full-round action{/g}. All friendly creatures in the area receive a resistance " +
                    "{g|Encyclopedia:Bonus}bonus{/g} equal to your Wisdom modifier on all {g|Encyclopedia:Saving_Throw}saving throws{/g} and an equal competence bonus on " +
                    "{g|Encyclopedia:Attack}attack rolls{/g} while inside the warded area. The ward lasts for 1 hour per level in the class that gave you access to this domain. You can use this " +
                    "ability once per day. \nDomain {g|Encyclopedia:Spell}Spells{/g}: bless, communal protection from {g|Encyclopedia:Alignment}alignment{/g}, prayer, communal protection from " +
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
            //Glory
            var GloryDomainBaseFeature = Resources.GetBlueprint<BlueprintFeature>("17e891b3964492f43aae44f994b5d454");

            var GloryDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("d018241b5a761414897ad6dc4df2db9f");
            var GloryDomainBaseResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("a653d96fbcbce64499f425aada462f69");
            var GloryDomainBaseFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("GloryDomainBaseFeatureDruid", bp => {
                bp.SetName("Glory Domain");
                bp.SetDescription("\nYou are infused with the glory of the divine, and are a true foe of the undead. In addition, when you channel positive energy to harm undead creatures, the " +
                    "{g|Encyclopedia:Saving_Throw}save{/g} {g|Encyclopedia:DC}DC{/g} to halve the {g|Encyclopedia:Damage}damage{/g} is increased by 2.\n{g|Encyclopedia:TouchAttack}Touch{/g} of " +
                    "Glory: You can cause your hand to shimmer with divine radiance, allowing you to touch a creature as a {g|Encyclopedia:Standard_Actions}standard action{/g} and give it a " +
                    "{g|Encyclopedia:Bonus}bonus{/g} equal to your level in the class that gave you access to this domain on a single {g|Encyclopedia:Charisma}Charisma{/g}-based " +
                    "{g|Encyclopedia:Skills}skill check{/g} or Charisma {g|Encyclopedia:Ability_Scores}ability check{/g}. This ability lasts for 1 hour or until the creature touched applies " +
                    "the bonus to a {g|Encyclopedia:Dice}roll{/g}. You can use this ability to grant the bonus a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} " +
                    "modifier.\nAura of Heroism: At 8th level, you can emit a 30-foot aura of heroism for a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in " +
                    "the class that gave you access to this domain. Using this ability is a {g|Encyclopedia:Swift_Action}swift action{/g}. Allies in the area are treated as if they were under " +
                    "the effects of heroism {g|Encyclopedia:Spell}spell{/g}.");
                bp.m_Icon = GloryDomainBaseFeature.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { GloryDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = GloryDomainBaseResource;
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { GloryDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var GloryDomainGreaterFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("bf41d1d2cf72e8545b51857f20fa58e7");
            var GloryDomainSpellList = Resources.GetBlueprintReference<BlueprintSpellListReference>("7b3506924ed8354419b7829736ab2c7e");
            var GloryDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("GloryDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass;
                    c.m_SpellList = GloryDomainSpellList;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var GloryDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("GloryDomainProgressionDruid", bp => {
                bp.SetName("Glory Domain");
                bp.SetDescription("\nYou are infused with the glory of the divine, and are a true foe of the undead. In addition, when you channel positive energy to harm undead creatures, the " +
                    "{g|Encyclopedia:Saving_Throw}save{/g} {g|Encyclopedia:DC}DC{/g} to halve the {g|Encyclopedia:Damage}damage{/g} is increased by 2.\n{g|Encyclopedia:TouchAttack}Touch{/g} of " +
                    "Glory: You can cause your hand to shimmer with divine radiance, allowing you to touch a creature as a {g|Encyclopedia:Standard_Actions}standard action{/g} and give it a " +
                    "{g|Encyclopedia:Bonus}bonus{/g} equal to your level in the class that gave you access to this domain on a single {g|Encyclopedia:Charisma}Charisma{/g}-based " +
                    "{g|Encyclopedia:Skills}skill check{/g} or Charisma {g|Encyclopedia:Ability_Scores}ability check{/g}. This ability lasts for 1 hour or until the creature touched applies " +
                    "the bonus to a {g|Encyclopedia:Dice}roll{/g}. You can use this ability to grant the bonus a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} " +
                    "modifier.\nAura of Heroism: At 8th level, you can emit a 30-foot aura of heroism for a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in " +
                    "the class that gave you access to this domain. Using this ability is a {g|Encyclopedia:Swift_Action}swift action{/g}. Allies in the area are treated as if they were under " +
                    "the effects of heroism {g|Encyclopedia:Spell}spell{/g}.\nDomain Spells: shield of faith, bless weapon, searing light, divine power, burst of glory, inspiring recovery, " +
                    "holy sword, holy aura, overwhelming presence.");
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
                    Helpers.LevelEntry(1, GloryDomainBaseFeatureDruid, GloryDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(8, GloryDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(GloryDomainBaseFeatureDruid, GloryDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            //Knowledge
            var KnowledgeDomainBaseFeature = Resources.GetBlueprint<BlueprintFeature>("5335f015063776d429a0b5eab97eb060");

            var KnowledgeDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("02a79a205bce6f5419dcdf26b64f13c6");
            var KnowledgeDomainBaseResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("f88f616a4b6bd5f419025115c52cb329");
            var KnowledgeDomainBaseFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("KnowledgeDomainBaseFeatureDruid", bp => {
                bp.SetName("Knowledge Domain");
                bp.SetDescription("\nYou are a scholar and a sage of legends. In addition, you treat all Knowledge and Lore {g|Encyclopedia:Skills}skills{/g} as class skills. \nVoid Form: You can " +
                    "become semi-tangible as a {g|Encyclopedia:Standard_Actions}standard action{/g}. While in this form, you are immune to {g|Encyclopedia:Critical}critical hits{/g} and gain a +1 " +
                    "deflection {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g}. This bonus increases by 1 at 8th level and every 4 levels thereafter. You can use this power " +
                    "a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class that gave you access to this domain. \nTeaching Moment: At 8th level as a " +
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
                bp.SetDescription("\nYou are a scholar and a sage of legends. In addition, you treat all Knowledge and Lore {g|Encyclopedia:Skills}skills{/g} as class skills. \nVoid Form: You can " +
                    "become semi-tangible as a {g|Encyclopedia:Standard_Actions}standard action{/g}. While in this form, you are immune to {g|Encyclopedia:Critical}critical hits{/g} and gain a +1 " +
                    "deflection {g|Encyclopedia:Bonus}bonus{/g} to {g|Encyclopedia:Armor_Class}AC{/g}. This bonus increases by 1 at 8th level and every 4 levels thereafter. You can use this power " +
                    "a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class that gave you access to this domain. \nTeaching Moment: At 8th level as a " +
                    "{g|Encyclopedia:Swift_Action}swift action{/g} you can grant all allies within 30 feet special insights. Once during the next minute, each affected creature can choose to " +
                    "{g|Encyclopedia:Dice}roll{/g} twice and take the better result before attempting an {g|Encyclopedia:Attack}attack roll{/g}, {g|Encyclopedia:Ability_Scores}ability check{/g}, " +
                    "skill {g|Encyclopedia:Check}check{/g}, or {g|Encyclopedia:Saving_Throw}saving throw{/g}. You can use this ability once per day at 8th level, and one additional time per day for " +
                    "every 4 levels in the class that gave you access to this domain beyond 8th. \nDomain {g|Encyclopedia:Spell}Spells{/g}: true strike, fox's cunning, communal see invisibility, " +
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
                bp.SetDescription("\nYou are a great leader, an inspiration to all who follow the teachings of your faith. \nInspiring Word: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, " +
                    "you can speak an inspiring word to a creature within 30 feet. That creature receives a +2 morale {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Attack}attack rolls{/g}, " +
                    "{g|Encyclopedia:Skills}skill checks{/g}, {g|Encyclopedia:Ability_Scores}ability checks{/g}, and {g|Encyclopedia:Saving_Throw}saving throws{/g} for a number of " +
                    "{g|Encyclopedia:Combat_Round}rounds{/g} equal to 1/2 your level in the class that gave you access to this domain (minimum 1). You can use this power a number of times per day " +
                    "equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. \nInspiring Command: At 8th level, you can issue an inspiring command to your allies, who must all be within 30 feet " +
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
                bp.SetDescription("\nYou are a great leader, an inspiration to all who follow the teachings of your faith. \nInspiring Word: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, " +
                    "you can speak an inspiring word to a creature within 30 feet. That creature receives a +2 morale {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Attack}attack rolls{/g}, " +
                    "{g|Encyclopedia:Skills}skill checks{/g}, {g|Encyclopedia:Ability_Scores}ability checks{/g}, and {g|Encyclopedia:Saving_Throw}saving throws{/g} for a number of " +
                    "{g|Encyclopedia:Combat_Round}rounds{/g} equal to 1/2 your level in the class that gave you access to this domain (minimum 1). You can use this power a number of times per day " +
                    "equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. \nInspiring Command: At 8th level, you can issue an inspiring command to your allies, who must all be within 30 feet " +
                    "of you. Affected allies gain a +2 insight bonus on attack {g|Encyclopedia:Dice}rolls{/g}, {g|Encyclopedia:Armor_Class}AC{/g}, {g|Encyclopedia:CMD}Combat Maneuver Defense{/g}, " +
                    "and skill {g|Encyclopedia:Check}checks{/g} for a number of rounds equal to 1/2 your level in the class that gave you access to this domain (minimum 1). You can use this power " +
                    "a number of times per day equal to 3 + your Wisdom modifier. \nDomain {g|Encyclopedia:Spell}Spells{/g}: divine favor, grace, magical vestment, heroism, dominate person, brilliant " +
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
                    "{g|Encyclopedia:Saving_Throw}saving throws{/g}. This bonus increases by 1 for every 5 levels you possess in the class that gave you access to this domain. \nResistant " +
                    "{g|Encyclopedia:TouchAttack}Touch{/g}: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can touch an ally to grant them your resistance bonus for 1 minute. When you use this " +
                    "ability, you lose your resistance bonus granted by the Protection domain for 1 minute. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} " +
                    "modifier. \nAura of Protection: At 8th level, you can emit a 30-foot aura of protection for a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class " +
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
                    "{g|Encyclopedia:Saving_Throw}saving throws{/g}. This bonus increases by 1 for every 5 levels you possess in the class that gave you access to this domain. \nResistant " +
                    "{g|Encyclopedia:TouchAttack}Touch{/g}: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can touch an ally to grant them your resistance bonus for 1 minute. When you use this " +
                    "ability, you lose your resistance bonus granted by the Protection domain for 1 minute. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} " +
                    "modifier. \nAura of Protection: At 8th level, you can emit a 30-foot aura of protection for a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class " +
                    "that gave you access to this domain. You and your allies within this aura gain a +1 deflection bonus to {g|Encyclopedia:Armor_Class}AC{/g} and resistance 5 against all elements (acid, " +
                    "cold, electricity, fire, and sonic). The deflection bonus increases by +1 for every four levels you possess in the class that gave you access to this domain beyond 8th. At 14th level, " +
                    "the resistance against all elements increases to 10. These rounds do not need to be consecutive. \nDomain {g|Encyclopedia:Spell}Spells{/g}: protection from " +
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
                    "is a mockery of what you hold dear. \nGentle Rest: Your {g|Encyclopedia:TouchAttack}touch{/g} can fill a creature with lethargy, causing a living creature to become staggered " +
                    "for 1 {g|Encyclopedia:Combat_Round}round{/g} as a melee touch {g|Encyclopedia:Attack}attack{/g}. If you touch a staggered living creature, that creature falls asleep for " +
                    "1 round instead. Undead creatures touched are staggered for a number of rounds equal to your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. You can use this ability a number " +
                    "of times per day equal to 3 + your Wisdom modifier. \nWard Against Death: At 8th level, you can emit a 30-foot aura that wards against death for a number of rounds per day " +
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
                    "is a mockery of what you hold dear. \nGentle Rest: Your {g|Encyclopedia:TouchAttack}touch{/g} can fill a creature with lethargy, causing a living creature to become staggered " +
                    "for 1 {g|Encyclopedia:Combat_Round}round{/g} as a melee touch {g|Encyclopedia:Attack}attack{/g}. If you touch a staggered living creature, that creature falls asleep for " +
                    "1 round instead. Undead creatures touched are staggered for a number of rounds equal to your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. You can use this ability a number " +
                    "of times per day equal to 3 + your Wisdom modifier. \nWard Against Death: At 8th level, you can emit a 30-foot aura that wards against death for a number of rounds per day " +
                    "equal to your level in the class that gave you access to this domain. Living creatures in this area are immune to all death effects, energy drain, and effects that cause " +
                    "negative levels. This ward does not remove negative levels that a creature has already gained, but the negative levels have no effect while the creature is inside the warded " +
                    "area. These rounds do not need to be consecutive. \nDomain {g|Encyclopedia:Spell}Spells{/g}: doom, scare, vampiric touch, death ward, slay living, undeath to death, destruction, " +
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
                bp.SetDescription("\nIn strange and eldritch runes you find potent magic. \nBlast Rune: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can create a blast rune in a desired " +
                    "location. Any creature entering a 5-foot area around the rune takes {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} + 1 point for every two levels you possess " +
                    "in the class that gave you access to this domain. This rune deals either acid, cold, electricity, or {g|Encyclopedia:Energy_Damage}fire damage{/g}, decided when you create the rune. " +
                    "The rune lasts a number of {g|Encyclopedia:Combat_Round}rounds{/g} equal to your level in the class that gave you access to this domain. You can use this ability a number of times per " +
                    "day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. \nWarding Rune: At 8th level, you can create a warding rune in a desired location. Any creature entering a 5-foot area " +
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
                bp.SetDescription("\nIn strange and eldritch runes you find potent magic. \nBlast Rune: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can create a blast rune in a desired " +
                    "location. Any creature entering a 5-foot area around the rune takes {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} + 1 point for every two levels you possess " +
                    "in the class that gave you access to this domain. This rune deals either acid, cold, electricity, or {g|Encyclopedia:Energy_Damage}fire damage{/g}, decided when you create the rune. " +
                    "The rune lasts a number of {g|Encyclopedia:Combat_Round}rounds{/g} equal to your level in the class that gave you access to this domain. You can use this ability a number of times per " +
                    "day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier. \nWarding Rune: At 8th level, you can create a warding rune in a desired location. Any creature entering a 5-foot area " +
                    "around the rune must make a {g|Encyclopedia:Saving_Throw}Will save{/g} or they will not be able to {g|Encyclopedia:Attack}attack{/g} for a number of rounds equal to 1/2 your level in " +
                    "the class that gave you access to this domain. The rune lasts a number of rounds equal to your level in the class that gave you access to this domain. You can use this ability once per " +
                    "day at 8th level and one additional time per day for every four levels in the class that gave you access to this domain beyond 8th. \nDomain {g|Encyclopedia:Spell}Spells{/g}: protection " +
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
            //Strength
            var StrengthDomainBaseFeature = Resources.GetBlueprint<BlueprintFeature>("526f99784e9fe4346824e7f210d46112");

            var StrengthDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("1d6364123e1f6a04c88313d83d3b70ee");
            var StrengthDomainBaseResource = Resources.GetBlueprintReference<BlueprintAbilityResourceReference>("95525809d6e672a4880ea629ca5b58ab");
            var StrengthDomainBaseFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("StrengthDomainBaseFeatureDruid", bp => {
                bp.SetName("Strength Domain");
                bp.SetDescription("In strength and brawn there is truth — your faith gives you incredible might and power.\nStrength Surge: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, " +
                    "you can {g|Encyclopedia:TouchAttack}touch{/g} a creature to give it great strength. For 1 {g|Encyclopedia:Combat_Round}round{/g}, the target gains an enhancement " +
                    "{g|Encyclopedia:Bonus}bonus{/g} equal to 1/2 your level in the class that gave you access to this domain (minimum +1) to {g|Encyclopedia:MeleeAttack}melee attacks{/g} and " +
                    "{g|Encyclopedia:Athletics}Athletics checks{/g}. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nMight of the Gods: " +
                    "At 8th level, you add 1/2 of your level in the class that gave you access to this domain as an enhancement bonus to your Athletics {g|Encyclopedia:Check}checks{/g}.");
                bp.m_Icon = StrengthDomainBaseFeature.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StrengthDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = StrengthDomainBaseResource;
                    c.RestoreAmount = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var StrengthDomainGreaterFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("3298fd30e221ef74189a06acbf376d29");
            var StrengthDomainSpellList = Resources.GetBlueprintReference<BlueprintSpellListReference>("03db76fd27428004482f9314c334d1ab");
            var StrengthDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("StrengthDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass;
                    c.m_SpellList = StrengthDomainSpellList;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var StrengthDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("StrengthDomainProgressionDruid", bp => {
                bp.SetName("Strength Domain");
                bp.SetDescription("In {g|Encyclopedia:Strength}strength{/g} and brawn there is truth — your faith gives you incredible might and power.\nStrength Surge: As a " +
                    "{g|Encyclopedia:Standard_Actions}standard action{/g}, you can {g|Encyclopedia:TouchAttack}touch{/g} a creature to give it great strength. For 1 " +
                    "{g|Encyclopedia:Combat_Round}round{/g}, the target gains an enhancement {g|Encyclopedia:Bonus}bonus{/g} equal to 1/2 your level in the class that gave you access to this domain " +
                    "(minimum +1) to {g|Encyclopedia:MeleeAttack}melee attacks{/g} and {g|Encyclopedia:Athletics}Athletics checks{/g}. You can use this ability a number of times per day equal to 3 + " +
                    "your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nMight of the Gods: At 8th level, you add 1/2 of your level in the class that gave you access to this domain as an enhancement " +
                    "bonus to your Athletics {g|Encyclopedia:Check}checks{/g}.\nDomain {g|Encyclopedia:Spell}Spells{/g}: enlarge person, bull's strength, magical vestment, mass enlarge person, " +
                    "righteous might, stoneskin, legendary proportions, frightful aspect, transformation.");
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
                    Helpers.LevelEntry(1, StrengthDomainBaseFeatureDruid, StrengthDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(8, StrengthDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(StrengthDomainBaseFeatureDruid, StrengthDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });


            //Sun
            var SunDomainBaseFeature = Resources.GetBlueprint<BlueprintFeature>("3d8e38c9ed54931469281ab0cec506e9");
            var ChannelPositiveHarm = Resources.GetBlueprintReference<BlueprintAbilityReference>("279447a6bf2d3544d93a0a39c3b8e91d");
            var ChannelEnergyHospitalerHarm = Resources.GetBlueprintReference<BlueprintAbilityReference>("cc17243b2185f814aa909ac6b6599eaa");
            var ChannelEnergyPaladinrHarm = Resources.GetBlueprintReference<BlueprintAbilityReference>("4937473d1cfd7774a979b625fb833b47");
            var ChannelEnergyEmpyrealHarm = Resources.GetBlueprintReference<BlueprintAbilityReference>("e1536ee240c5d4141bf9f9485a665128");

            var SunDomainBaseFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("SunDomainBaseFeatureDruid", bp => {
                bp.SetName("Sun Domain");
                bp.SetDescription("You see truth in the pure and burning light of the sun, and can call upon its blessing or wrath to work great deeds.\nSun's Blessing: Whenever you " +
                    "channel positive energy to harm undead creatures, add your level in the class that gave you access to this domain to the {g|Encyclopedia:Damage}damage{/g} dealt. " +
                    "Undead do not add their channel resistance to their {g|Encyclopedia:Saving_Throw}saves{/g} when you channel positive energy.\nNimbus of Light: At 8th level, you can " +
                    "emit a 30-foot nimbus of light for a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class that gave you access to this domain. " +
                    "Any hostile creature within this radius must succeed at a Fortitude save to resist the effects of this aura. If the creature fails, it is blinded until it leaves " +
                    "the area of the {g|Encyclopedia:Spell}spell{/g}. A creature that has resisted the effect cannot be affected again by this particular aura. In addition, undead " +
                    "within this radius take an amount of damage equal to your level in the class that gave you access to this domain each round that they remain inside the nimbus. " +
                    "These rounds do not need to be consecutive.");
                bp.m_Icon = SunDomainBaseFeature.m_Icon;
                bp.AddComponent<IncreaseSpellDamageByClassLevel>(c => {
                    c.m_Spells = new BlueprintAbilityReference[] { 
                        ChannelPositiveHarm,
                        ChannelEnergyHospitalerHarm,
                        ChannelEnergyPaladinrHarm,
                        ChannelEnergyEmpyrealHarm
                    };
                    c.m_CharacterClass = DruidClass;
                    c.m_Archetypes = new BlueprintArchetypeReference[] { };
                });                
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var SunDomainGreaterFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("3e301c9d0e735b649955139ee0f5f165");
            var SunDomainSpellList = Resources.GetBlueprintReference<BlueprintSpellListReference>("600ffed45d0c3ec43a75dc76bb9377b6");
            var SunDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("SunDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass;
                    c.m_SpellList = SunDomainSpellList;
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            var SunDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("SunDomainProgressionDruid", bp => {
                bp.SetName("Sun Domain");
                bp.SetDescription("You see truth in the pure and burning light of the sun, and can call upon its blessing or wrath to work great deeds.\nSun's Blessing: Whenever you " +
                    "channel positive energy to harm undead creatures, add your level in the class that gave you access to this domain to the {g|Encyclopedia:Damage}damage{/g} dealt. " +
                    "Undead do not add their channel resistance to their {g|Encyclopedia:Saving_Throw}saves{/g} when you channel positive energy.\nNimbus of Light: At 8th level, you can " +
                    "emit a 30-foot nimbus of light for a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class that gave you access to this domain. " +
                    "Any hostile creature within this radius must succeed at a Fortitude save to resist the effects of this aura. If the creature fails, it is blinded until it leaves " +
                    "the area of the {g|Encyclopedia:Spell}spell{/g}. A creature that has resisted the effect cannot be affected again by this particular aura. In addition, undead within " +
                    "this radius take an amount of damage equal to your level in the class that gave you access to this domain each round that they remain inside the nimbus. These rounds " +
                    "do not need to be consecutive.\nDomain Spells: faerie fire, see invisibility, searing light, shield of dawn, flame strike, chains of light, sunbeam, sunburst, " +
                    "elemental swarm (fire).");
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
                    Helpers.LevelEntry(1, SunDomainBaseFeatureDruid, SunDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(8, SunDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(SunDomainBaseFeatureDruid, SunDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });



        }
    }
}
