using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace ExpandedContent.Tweaks.Mysteries {
    internal class WinterMystery {
        public static void AddWinterMystery() {

            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var OracleRevelationSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("60008a10ad7ad6543b1f63016741a5d2");
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








        }
    }
}
