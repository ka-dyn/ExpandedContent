using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.Mechanics.Components;


namespace ExpandedContent.Tweaks.Archetypes {
    internal class DivineTracker {


        public static void AddDivineTracker() {

            var RangerClass = Resources.GetBlueprint<BlueprintCharacterClass>("cda0615668a6df14eb36ba19ee881af6");
            var HuntersBondSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("b705c5184a96a84428eeb35ae2517a14");
            var BlessingSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("6d9dcc2a59210a14891aeedb09d406aa");
            var MartialWeaponProficiency = Resources.GetBlueprint<BlueprintFeature>("203992ef5b35c864390b4e4a1e200629");
            var BlessingResource = Resources.GetBlueprint<BlueprintAbilityResource>("d128a6332e4ea7c4a9862b9fdb358cca");

            var DivineTrackerArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("DivineTrackerArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"DivineTrackerArchetype.Name", "Divine Tracker");
                bp.LocalizedDescription = Helpers.CreateString($"DivineTrackerArchetype.Description", "Blessed by his deity, a divine tracker hunts down those he deems deserving of his " +
                    "retribution. His weapon is likely to find purchase in his favored enemy.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"DivineTrackerArchetype.Description", "Blessed by his deity, a divine tracker hunts down those he deems deserving of " +
                    "his retribution. His weapon is likely to find purchase in his favored enemy.");
                
            });

            var DivineTrackerFavoredWeapon = Helpers.CreateBlueprint<BlueprintFeature>("DivineTrackerFavoredWeapon", bp => {
                bp.SetName("Favored Weapon");
                bp.SetDescription("At 1st level, a divine tracker becomes proficient with the favored weapon of his deity if not already proficient. If his deity’s favored weapon is unarmed " +
                    "strike, he instead gains Improved Unarmed Strike as a bonus feat.");
                bp.m_Icon = MartialWeaponProficiency.Icon;
                bp.IsClassFeature = true;
            });
            var IroriFeatureAddFeatureOnClassLevel = Resources.GetBlueprint<BlueprintFeature>("23a77a5985de08349820429ce1b5a234").GetComponent<AddFeatureOnClassLevel>();
            IroriFeatureAddFeatureOnClassLevel.m_AdditionalClasses = IroriFeatureAddFeatureOnClassLevel.m_AdditionalClasses.AppendToArray(RangerClass.ToReference<BlueprintCharacterClassReference>());
            IroriFeatureAddFeatureOnClassLevel.m_Archetypes = IroriFeatureAddFeatureOnClassLevel.m_Archetypes.AppendToArray(DivineTrackerArchetype.ToReference<BlueprintArchetypeReference>());


            var AirBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("e1ff99dc3aeaa064e8eecde51c1c4773");
            var AnimalBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("9d991f8374c3def4cb4a6287f370814d");
            var ChaosBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("528e316f9f092954b9e38d3a82b1634a");
            var DarknessBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("3ed6cd88caecec944b837f57b9be176f");
            var DeathBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("6d11e8b00add90c4f93c2ad6d12885f7");
            var DestructionBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("dd5e75a02e4563e44a0931c6f46fb0a7");
            var EarthBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("73c37a22bc9a523409a47218d507acf6");
            var EvilBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("f38f3abf6ca3a07499a61f96e342bb16");
            var FireBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("2368212fa3856d74589e924d3e2074d8");
            var GoodBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("60a85144ed37e3a45b343d291dc48079");
            var HealingBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("f3881a1a7b44dc74c9d76907c94e49f2");
            var LawBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("9c49504e2e4c66d4aa341348356b47a8");
            var LiberationBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("61061faf477d67b43b6dedb3e8f205d7");
            var LuckBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("70654ee784fffa74489933a0d2047bbd");
            var MadnessBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("a99916a8aad2414f970072db7b760c48");
            var MagicBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("1754ff61a0805714fa2b89c8c1bb87ad");
            var NobilityBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("f52af97d05e5de34ea6e0d1b0af740ea");
            var ProtectionBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("c6a3fa9d8d7f942499e4909cd01ca22d");
            var ReposeBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("64a416082927673409deb330af04d6d2");
            var StrengthBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("44f9162736a5c2040ae8ede853bc6639");
            var SunBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("ba825e3c77acaec4386e00f691f8f3be");
            var TravelBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("87641a8efec53d64d853ecc436234dce");
            var TrickeryBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("a8e7abcad0cf8384b9f12c3b075b5cae");
            var WaterBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("0f457943bb99f9b48b709c90bfc0467e");
            var WeatherBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("4172d92c598de1d47aa2c0dd51c05e24");

            BlessingResource.m_MaxAmount.m_Class = BlessingResource.m_MaxAmount.m_Class.AppendToArray(RangerClass.ToReference<BlueprintCharacterClassReference>());
            BlessingResource.m_MaxAmount.m_ClassDiv = BlessingResource.m_MaxAmount.m_ClassDiv.AppendToArray(RangerClass.ToReference<BlueprintCharacterClassReference>());
            BlessingResource.m_MaxAmount.m_Archetypes = BlessingResource.m_MaxAmount.m_Archetypes.AppendToArray(DivineTrackerArchetype.ToReference<BlueprintArchetypeReference>());
            BlessingResource.m_MaxAmount.m_ArchetypesDiv = BlessingResource.m_MaxAmount.m_ArchetypesDiv.AppendToArray(DivineTrackerArchetype.ToReference<BlueprintArchetypeReference>());

            var DivineTrackerBlessingSelectionFirst = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DivineTrackerBlessingSelectionFirst", bp => {
                bp.SetName("Trackers Blessings");
                bp.SetDescription("At 4th level, a divine tracker forms a close bond with his deity’s ethos. He selects two warpriest domains from among the domains granted by his deity, and gains the " +
                    "minor blessings of those domains. A divine tracker can select an alignment domain (Chaos, Evil, Good, or Law) only if his alignment matches that domain. A divine tracker uses his " +
                    "ranger level as his warpriest level to determine the effect of the blessing. At 13th level, a divine tracker gains the major blessing from both of his domains.");
                bp.m_Icon = BlessingSelection.m_Icon;
                bp.Groups = new FeatureGroup[0];
                bp.Mode = SelectionMode.OnlyNew;
                bp.IsClassFeature = true;
                bp.m_AllFeatures = new BlueprintFeatureReference[] { }; //Filled out using CreateDivineTrackerBlessing
                bp.m_Features = new BlueprintFeatureReference[] { };
            });
            var DivineTrackerBlessingSelectionSecond = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DivineTrackerBlessingSelectionSecond", bp => {
                bp.SetName("Trackers Blessings");
                bp.SetDescription("At 4th level, a divine tracker forms a close bond with his deity’s ethos. He selects two warpriest domains from among the domains granted by his deity, and gains the " +
                    "minor blessings of those domains. A divine tracker can select an alignment domain (Chaos, Evil, Good, or Law) only if his alignment matches that domain. A divine tracker uses his " +
                    "ranger level as his warpriest level to determine the effect of the blessing. At 13th level, a divine tracker gains the major blessing from both of his domains.");
                bp.m_Icon = BlessingSelection.m_Icon;
                bp.Groups = new FeatureGroup[0];
                bp.Mode = SelectionMode.OnlyNew;
                bp.IsClassFeature = true;
                bp.m_AllFeatures = new BlueprintFeatureReference[] { }; //Filled out using CreateDivineTrackerBlessing
                bp.m_Features = new BlueprintFeatureReference[] { };
            });
            #region CreateDivineTrackerBlessing
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerAirBlessingFeature", AirBlessingFeature, "At 1st level, you can touch any one ally. For 1 minute, his {g|Encyclopedia:RangedAttack}ranged attacks{/g} doesn't provoke an {g|Encyclopedia:Attack_Of_Opportunity}attack of opportunity{/g}.\nAt 13th level, you can touch an ally and give her the gift of flight for 1 minute (as Wings). Whenever the ally succeeds at a charge {g|Encyclopedia:Attack}attack{/g}, that attack deals an amount of additional {g|Encyclopedia:Energy_Damage}electricity damage{/g} equal to your level.");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerAnimalBlessingFeature", AnimalBlessingFeature, "At 1st level, you can touch one ally and grant it feral features. The ally gains 2 claw {g|Encyclopedia:Attack}attacks{/g} that each deal {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} if the ally is Medium or 1d4 if it's Small. These are primary {g|Encyclopedia:NaturalAttack}natural attacks{/g} that replace any similar primary natural attacks the ally might have. This effect lasts for 1 minute.\nAt 13th level, you can summon a battle companion. This ability functions as summon nature's ally V with a duration of 1 minute. This ability can summon only one animal, regardless of the list used. For every 2 levels beyond 10th, the level of the summon nature's ally {g|Encyclopedia:Spell}spell{/g} increases by 1 (to a maximum of summon nature's ally IX at 18th level).");
            var AnimalBlessingMajorAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("93f0098fe08b94f41a351a4fbb00518a").GetComponent<ContextRankConfig>();
            AnimalBlessingMajorAbilityConfig.m_Class = AnimalBlessingMajorAbilityConfig.m_Class.AppendToArray(RangerClass.ToReference<BlueprintCharacterClassReference>());
            AnimalBlessingMajorAbilityConfig.m_AdditionalArchetypes = AnimalBlessingMajorAbilityConfig.m_AdditionalArchetypes.AppendToArray(DivineTrackerArchetype.ToReference<BlueprintArchetypeReference>());
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerChaosBlessingFeature", ChaosBlessingFeature, "At 1st level, you can touch one ally and grant him a chaotic blessing. For 1 minute, this ally deals an additional {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} against lawful creatures. During this time, his {g|Encyclopedia:Attack}attacks{/g} are treated as chaotic for the purposes of overcoming {g|Encyclopedia:Damage_Reduction}damage reduction{/g}.\nAt 13th level, you can summon a battle companion. This ability functions as summon monster IV with a duration of 1 minute, but can only summon demons. This ability can summon only one creature, regardless of the list used. For every 2 levels beyond 10th, the level of the summon monster {g|Encyclopedia:Spell}spell{/g} increases by 1 (to a maximum of summon monster IX at 20th level).");
            var ChaosBlessingMajorAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("6fb60e6cb81138941a07e6861df839c6").GetComponent<ContextRankConfig>();
            ChaosBlessingMajorAbilityConfig.m_Class = ChaosBlessingMajorAbilityConfig.m_Class.AppendToArray(RangerClass.ToReference<BlueprintCharacterClassReference>());
            ChaosBlessingMajorAbilityConfig.m_AdditionalArchetypes = ChaosBlessingMajorAbilityConfig.m_AdditionalArchetypes.AppendToArray(DivineTrackerArchetype.ToReference<BlueprintArchetypeReference>());
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerDarknessBlessingFeature", DarknessBlessingFeature, "At 1st level, you can touch an ally and bestow a darkness blessing. For 1 minute, the ally becomes enshrouded in shadows while in combat, granting it {g|Encyclopedia:Concealment}concealment{/g} (20% miss chance).\nAt 13th level, you can place a shroud of darkness around the eyes of one foe within 30 feet. The target must succeed at a {g|Encyclopedia:Saving_Throw}Will saving throw{/g} or be blinded for 1 minute");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerDeathBlessingFeature", DeathBlessingFeature, "At 1st level, you can take on a corpse-like visage for 1 minute, making you more intimidating and giving you undead-like protection from harm. You gain a +4 {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Persuasion}Persuasion{/g} {g|Encyclopedia:Skills}skill checks{/g} made to intimidate, as well as a +2 profane bonus on {g|Encyclopedia:Saving_Throw}saving throws{/g} against disease, mind-affecting effects, paralysis, poison, and stun.\nAt 13th level, you can make a melee {g|Encyclopedia:TouchAttack}touch attack{/g} against an opponent to deliver grim suffering. If you succeed, you inflict 1 temporary negative level on the target for 1 minute. Alternatively, you can activate this ability as a {g|Encyclopedia:Swift_Action}swift action{/g} to make your next {g|Encyclopedia:MeleeAttack}melee attack{/g} during the next minute apply the effect. These temporary negative levels stack. You gain no benefit from imposing these negative levels (such as the temporary {g|Encyclopedia:HP}hit points{/g} undead gain from enervation).");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerDestructionBlessingFeature", DestructionBlessingFeature, "At 1st level, you can touch an ally and bless them with the power of destruction. For 1 minute, the ally gains a morale {g|Encyclopedia:Bonus}bonus{/g} on weapon {g|Encyclopedia:Damage}damage rolls{/g} equal to half your level (minimum 1).\nAt 13th level, you can touch an ally and bless them with even greater destructive power. For 1 minute, the ally gains a +4 insight bonus on {g|Encyclopedia:Attack}attack rolls{/g} made to confirm {g|Encyclopedia:Critical}critical hits{/g} and has a 50% chance to treat any critical hit or sneak attack against it as a normal hit.");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerEarthBlessingFeature", EarthBlessingFeature, "At 1st level, you can touch one weapon and enhance it with acidic potency. For 1 minute, this weapon emits acrid fumes that deal an additional {g|Encyclopedia:Dice}1d4{/g} points of {g|Encyclopedia:Energy_Damage}acid damage{/g} with each strike.\nAt 13th level, you can touch an ally and harden its armor or clothing. For 1 minute, the ally gains {g|Encyclopedia:Damage_Reduction}DR{/g} 1/—. For every 2 levels beyond 10th, this DR increases by 1 (to a maximum of DR 5/— at 18th level). This doesn't stack with any other {g|Encyclopedia:Damage}damage{/g} resistance or reduction.");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerEvilBlessingFeature", EvilBlessingFeature, "At 1st level, you can touch one ally and grant him an evil blessing. For 1 minute, this ally deals an additional {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} against good creatures. During this time, his {g|Encyclopedia:Attack}attacks{/g} are treated as evil for the purposes of overcoming {g|Encyclopedia:Damage_Reduction}damage reduction{/g}.\nAt 13th level, you can summon a battle companion. This ability functions as summon monster IV with a duration of 1 minute, but only allows to summon demons. This ability can summon only one creature, regardless of the list used. For every 2 levels beyond 10th, the level of the summon monster {g|Encyclopedia:Spell}spell{/g} increases by 1 (to a maximum of summon monster IX at 20th level).");
            var EvilBlessingMajorAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("c9bbd75a934c6b44fbd2b2a0da9b687f").GetComponent<ContextRankConfig>();
            EvilBlessingMajorAbilityConfig.m_Class = EvilBlessingMajorAbilityConfig.m_Class.AppendToArray(RangerClass.ToReference<BlueprintCharacterClassReference>());
            EvilBlessingMajorAbilityConfig.m_AdditionalArchetypes = EvilBlessingMajorAbilityConfig.m_AdditionalArchetypes.AppendToArray(DivineTrackerArchetype.ToReference<BlueprintArchetypeReference>());
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerFireBlessingFeature", FireBlessingFeature, "At 1st level, you can touch one weapon and enhance it with the grandeur of fire. For 1 minute, this weapon glows red-hot and deals an additional {g|Encyclopedia:Dice}1d4{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} with each hit.\nAt 13th level, you can touch an ally to wreath them in flames. This gives the ally cold {g|Encyclopedia:Energy_Resistance}resistance{/g} equal to your warpriest level.");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerGoodBlessingFeature", GoodBlessingFeature, "At 1st level, you can touch one ally and grant him a holy blessing. For 1 minute, this ally deals an additional {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} against evil creatures. During this time, his {g|Encyclopedia:Attack}attacks{/g} are treated as good for the purposes of overcoming {g|Encyclopedia:Damage_Reduction}damage reduction{/g}.\nAt 10th level, as a {g|Encyclopedia:Swift_Action}swift action{/g} you can bolster your allies' defenses against evil. For one {g|Encyclopedia:Combat_Round}round{/g}, all allies within 30 feet gain a +4 sacred {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Armor_Class}AC{/g} and {g|Encyclopedia:Saving_Throw}saving throws{/g} against evil opponents.");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerHealingBlessingFeature", HealingBlessingFeature, "At 1st level, you can add power to a {g|Encyclopedia:Healing}cure{/g} {g|Encyclopedia:Spell}spell{/g} as you cast it. As a {g|Encyclopedia:Swift_Action}swift action{/g}, you can make your next cure spell empowered (as though using the Empower Spell {g|Encyclopedia:Feat}feat{/g}), causing it to heal 50% more {g|Encyclopedia:Damage}damage{/g} (or deal 50% more damage if used against undead). This ability doesn't stack with itself or the Empower Spell feat.\nAt 13th level, you can touch an ally and grant them {g|Encyclopedia:Fast_Healing}fast healing{/g} 3 for 1 minute.");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerLawBlessingFeature", LawBlessingFeature, "At 1st level, you can touch one ally and grant him an axiomatic blessing. For 1 minute, this ally deals an additional {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} against chaotic creatures. During this time, his {g|Encyclopedia:Attack}attacks{/g} are treated as lawful for the purposes of overcoming {g|Encyclopedia:Damage_Reduction}damage reduction{/g}.\nAt 13th level, as a {g|Encyclopedia:Swift_Action}swift action{/g} you can bolster your allies' defenses against chaos. For one {g|Encyclopedia:Combat_Round}round{/g}, all allies within 30 feet gain a +4 sacred {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Armor_Class}AC{/g} and {g|Encyclopedia:Saving_Throw}saving throws{/g} against chaotic opponents.");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerLiberationBlessingFeature", LiberationBlessingFeature, "At 1st level, for 1 {g|Encyclopedia:Combat_Round}round{/g} as a {g|Encyclopedia:Swift_Action}swift action{/g}, you can ignore impediments to your mobility and effects that cause paralysis (as freedom of movement). You can activate this blessing even if you're otherwise unable to take {g|Encyclopedia:CA_Types}actions{/g}, but not if you're {g|Encyclopedia:Injury_Death}unconscious{/g}.\nAt 13th level, as a swift action you can emit a 30-foot aura that affects all allies with the liberation blessing described above. This effect lasts for 1 round.");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerLuckBlessingFeature", LuckBlessingFeature, "At 1st level, you can touch an ally and grant them a lucky presence. The target of this luck can {g|Encyclopedia:Dice}roll{/g} all {g|Encyclopedia:Ability_Scores}ability checks{/g}, {g|Encyclopedia:Attack}attack rolls{/g}, {g|Encyclopedia:Saving_Throw}saving throws{/g}, and {g|Encyclopedia:Skills}skill checks{/g} twice and take the better result. The effect lasts for 1 {g|Encyclopedia:Combat_Round}round{/g}.\nAt 13th level, you can curse an enemy with unlucky presence. The target of this bad luck must roll all ability {g|Encyclopedia:Check}checks{/g}, attack rolls, saving throws, and skill check twice and take the worse result. The effect lasts for 1 round.");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerMadnessBlessingFeature", MadnessBlessingFeature, "At 1st level, as a {g|Encyclopedia:Swift_Action}swift action{/g} you can target a creature within 30 feet that has the cowering, frightened, panicked, or paralyzed condition. That condition is suspended for 1 {g|Encyclopedia:Combat_Round}round{/g}, and the chosen creature gains the confused condition instead. The confused creature {g|Encyclopedia:Dice}rerolls{/g} any result other than \"{g|Encyclopedia:Attack}attack{/g} self\" or \"attack nearest creature.\" The round spent confused counts toward the duration of the suspended effect. At the end of the confused round, the suspended condition resumes.\nAt 13th level, as a swift {g|Encyclopedia:CA_Types}action{/g} you can choose one behavior for all confused creatures within 30 feet to exhibit (as if all creatures rolled the same result). This effect lasts for 1 round. You can use this ability even while you are confused.");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerMagicBlessingFeature", MagicBlessingFeature, "At 1st level, you can cause your melee weapon to fly from your grasp and strike an opponent, then instantly return to you. You can make a single {g|Encyclopedia:Attack}attack{/g} using a melee weapon at a range of 30 feet. This attack is treated as a {g|Encyclopedia:RangedAttack}ranged attack{/g} with a thrown weapon, except that you add your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier to the attack roll instead of your {g|Encyclopedia:Dexterity}Dexterity{/g} modifier (you still add your {g|Encyclopedia:Strength}Strength{/g} modifier to the {g|Encyclopedia:Damage}damage roll{/g} as normal). This ability cannot be used to perform a {g|Encyclopedia:Combat_Maneuvers}combat maneuver{/g}.\nAt 13th level, with a {g|Encyclopedia:Swift_Action}swift action{/g} he can recall any single warpriest {g|Encyclopedia:Spell}spell{/g} that he has already prepared and cast that day. The spell is prepared again, just as if it had not been cast.");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerNobilityBlessingFeature", NobilityBlessingFeature, "At 1st level, you can speak a few words to a creature within 30 feet that fill them with inspiration. You can grant that creature a +2 morale {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Attack}attack rolls{/g}, {g|Encyclopedia:Skills}skill checks{/g}, and {g|Encyclopedia:Saving_Throw}saving throws{/g}. This effect lasts for 1 minute.\nAt 13th level, as a {g|Encyclopedia:Swift_Action}swift action{/g} you can inspire your allies to follow your lead. For one {g|Encyclopedia:Combat_Round}round{/g}, all allies within 30 feet gain a +4 morale bonus on attack rolls, skill {g|Encyclopedia:Check}checks{/g}, and saving throws.");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerProtectionBlessingFeature", ProtectionBlessingFeature, "At 1st level, you can gain a +1 sacred {g|Encyclopedia:Bonus}bonus{/g} on {g|Encyclopedia:Saving_Throw}saving throws{/g} and a +1 sacred bonus to {g|Encyclopedia:Armor_Class}AC{/g} for 1 minute. The bonus increases to +2 at 10th level and +3 at 20th level.\nAt 13th level, you can emit a 30-foot aura of protection for 1 minute. You and your allies within this aura gain {g|Encyclopedia:Energy_Resistance}resistance{/g} 10 against acid, cold, electricity, fire, and sonic. At 15th level, the energy resistance increases to 20.");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerReposeBlessingFeature", ReposeBlessingFeature, "At 1st level, you can fill a living creature with lethargy by hitting it with a melee {g|Encyclopedia:TouchAttack}touch attack{/g}, causing it to become staggered for 1 {g|Encyclopedia:Combat_Round}round{/g}. If the target is already staggered, it falls asleep for 1 round instead. An undead creature that's touched is staggered for a number of rounds equal to your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier (minimum 1).\nAt 13th level, when using channel energy to {g|Encyclopedia:Healing}heal{/g} living creatures, you can take a {g|Encyclopedia:Swift_Action}swift action{/g} on that same turn to also deal {g|Encyclopedia:Damage}damage{/g} to undead creatures (as your channel energy ability). Undead take an amount of damage equal to half the amount healed.");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerStrengthBlessingFeature", StrengthBlessingFeature, "At 1st level, as a {g|Encyclopedia:Swift_Action}swift action{/g} you can focus your own strength. You gain an enhancement {g|Encyclopedia:Bonus}bonus{/g} equal to 1/2 your warpriest level (minimum +1) on {g|Encyclopedia:MeleeAttack}melee attack{/g} rolls, {g|Encyclopedia:Combat_Maneuvers}combat maneuver{/g} {g|Encyclopedia:Check}checks{/g} that rely on {g|Encyclopedia:Strength}Strength{/g}, Strength-based {g|Encyclopedia:Skills}skills{/g}, and Strength checks for 1 {g|Encyclopedia:Combat_Round}round{/g}.\nAt 13th level, as a swift {g|Encyclopedia:CA_Types}action{/g} you can ignore the movement {g|Encyclopedia:Penalty}penalties{/g} caused by wearing medium or heavy armor. This effect lasts for 1 minute. During this time, you can add your Strength modifier on {g|Encyclopedia:Saving_Throw}saving throws{/g} against effects that would cause you to become entangled, staggered, or paralyzed.");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerSunBlessingFeature", SunBlessingFeature, "At 1st level, you can create a flash of sunlight in the eyes of one of your opponents. The target is blinded for 1 {g|Encyclopedia:Combat_Round}round{/g}. If it succeeds at a {g|Encyclopedia:Saving_Throw}Reflex saving throw{/g}, it's instead dazzled for 1 round.\nAt 13th level, you can touch a weapon and grant it either the flaming or undead bane weapon special ability for 1 minute. If you spend two uses of your blessing when activating this ability, the weapon can have both weapon special abilities.");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerTravelBlessingFeature", TravelBlessingFeature, "At 1st level, as a {g|Encyclopedia:Swift_Action}swift action{/g} you gain increased mobility. For 1 {g|Encyclopedia:Combat_Round}round{/g}, you ignore all difficult terrain (including magical terrain) and take no {g|Encyclopedia:Penalty}penalties{/g} for moving through it.\nAt 13th level, you can teleport up to 20 feet as a {g|Encyclopedia:Move_Action}move action{/g}. You must have line of sight to your destination. This teleportation doesn't provoke {g|Encyclopedia:Attack_Of_Opportunity}attacks of opportunity{/g}.");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerTrickeryBlessingFeature", TrickeryBlessingFeature, "At 1st level, as a {g|Encyclopedia:Move_Action}move action{/g} you can create an illusory double of yourself. This double functions as a single mirror image, and lasts for a number of {g|Encyclopedia:Combat_Round}rounds{/g} equal to your warpriest level, or until the illusory duplicate is dispelled or destroyed. You can have no more than one double at a time. The double created by this ability doesn't stack with the additional images from the mirror image {g|Encyclopedia:Spell}spell{/g}. This ability doesn't stack with the additional {g|Encyclopedia:Damage}damage{/g} from the mirror image spell.\nAt 13th level, as a {g|Encyclopedia:Swift_Action}swift action{/g} you can become invisible for 1 round (as greater invisibility).");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerWaterBlessingFeature", WaterBlessingFeature, "At 1st level, you can touch one weapon and enhance it with the power of water. For 1 minute, this weapon glows with a blue-white chill and deals an additional {g|Encyclopedia:Dice}1d4{/g} points of {g|Encyclopedia:Energy_Damage}cold damage{/g} with each strike.\nAt 13th level, you can touch any one ally and wreath it in freezing mist. He gains fire {g|Encyclopedia:Energy_Resistance}resistance{/g} equal to your warpriest level.");
            BlessingTools.CreateDivineTrackerBlessing("DivineTrackerWeatherBlessingFeature", WeatherBlessingFeature, "At 1st level, you can touch one weapon and grant it a blessing of stormy weather. For 1 minute, this weapon glows with blue or yellow sparks and deals an additional {g|Encyclopedia:Dice}1d4{/g} points of {g|Encyclopedia:Energy_Damage}electricity damage{/g} with each hit.\nAt 13th level, you can create a barrier of fast winds around yourself for 1 minute. This gives enemy {g|Encyclopedia:RangedAttack}ranged attack{/g} a chance to miss equal to twice your warpriest level.");
            #endregion

            DivineTrackerArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(4, HuntersBondSelection)
            };
            DivineTrackerArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, DivineTrackerFavoredWeapon),
                    Helpers.LevelEntry(4, DivineTrackerBlessingSelectionFirst, DivineTrackerBlessingSelectionSecond)

            };
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Divine Tracker")) { return; }
            RangerClass.m_Archetypes = RangerClass.m_Archetypes.AppendToArray(DivineTrackerArchetype.ToReference<BlueprintArchetypeReference>());

        }

        
    }
}
