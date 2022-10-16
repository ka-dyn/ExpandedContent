using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.Utility;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UI.GenericSlot;
using Kingmaker.Blueprints.Items.Ecnchantments;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class StormDruid {
        public static void AddStormDruid() {

            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var DruidBondSelection = Resources.GetBlueprint<BlueprintFeature>("3830f3630a33eba49b60f511b4c8f2a8");
            var WoodlandStrideFeature = Resources.GetBlueprint<BlueprintFeature>("11f4072ea766a5840a46e6660894527d");
            var DruidSpontaneousSummonFeature = Resources.GetBlueprint<BlueprintFeature>("b296531ffe013c8499ad712f8ae97f6b");
            var VenomImmunityFeature = Resources.GetBlueprint<BlueprintFeature>("5078622eb5cecaf4683fa16a9b948c2c");
            var ResistNaturesLureFeature = Resources.GetBlueprint<BlueprintFeature>("ad6a5b0e1a65c3540986cf9a7b006388");
            var AirDomainProgressionDruid = Resources.GetBlueprint<BlueprintProgression>("3aef017b78329db4fa53fe8560069886");
            var WeatherDomainProgressionDruid = Resources.GetBlueprint<BlueprintProgression>("4a3516fdc4cda764ebd1279b22d10205");
            var StormDomainProgressionDruid = Resources.GetModBlueprint<BlueprintProgression>("StormDomainProgressionDruid");
            var WindDomainProgressionDruid = Resources.GetModBlueprint<BlueprintProgression>("WindDomainProgressionDruid");
            var RainModerate = Resources.GetBlueprint<BlueprintBuff>("f37b708de9eeb2c4ab248d79bb5b5aa7");
            var SnowModerateBuff = Resources.GetBlueprint<BlueprintBuff>("845332298344c6447972dc9b131add08");
            var RainStormBuff = Resources.GetBlueprint<BlueprintBuff>("7c260a8970e273d439f2a2e19b7196af");
            var RainHeavyBuff = Resources.GetBlueprint<BlueprintBuff>("5c315bec0240479d9fafcc65b9efb574");
            var RainLightBuff = Resources.GetBlueprint<BlueprintBuff>("b13768381de549e2a78f502fa65dd613");
            var SnowHeavyBuff = Resources.GetBlueprint<BlueprintBuff>("4a15ab872f11463da1c1265d5b4324ad");
            var SnowLightBuff = Resources.GetBlueprint<BlueprintBuff>("26d8835510914ca2a8fe74b1519c09ac");


            var StormDruidArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("StormDruidArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"StormDruidArchetype.Name", "Storm Druid");
                bp.LocalizedDescription = Helpers.CreateString($"StormDruidArchetype.Description", "While most druids focus their attention upon the rich earth and the bounty of " +
                    "nature that springs forth from it, the storm druid’s eyes have ever been cast to the skies and the endless expanse of blue, channeling the most raw and " +
                    "untamed aspects of nature.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"StormDruidArchetype.Description", "While most druids focus their attention upon the rich earth and the " +
                    "bounty of nature that springs forth from it, the storm druid’s eyes have ever been cast to the skies and the endless expanse of blue, channeling the " +
                    "most raw and untamed aspects of nature.");
                
            });
            //Spell Bank
            var CallLightningSpell = Resources.GetBlueprint<BlueprintAbility>("2a9ef0e0b5822a24d88b16673a267456");
            var CallLightningStormSpell = Resources.GetBlueprint<BlueprintAbility>("d5a36a7ee8177be4f848b953d1c53c84");
            var ChainLightningSpell = Resources.GetBlueprint<BlueprintAbility>("645558d63604747428d55f0dd3a4cb58");
            var CloudkillSpell = Resources.GetBlueprint<BlueprintAbility>("548d339ba87ee56459c98e80167bdf10");
            var EarPiercingScreamSpell = Resources.GetBlueprint<BlueprintAbility>("8e7cfa5f213a90549aadd18f8f6f4664");
            var ElementalBodyIVAirSpell = Resources.GetBlueprint<BlueprintAbility>("ee63301f83c76694692d4704d8a05bdc");
            var ElementalSwarmAir = Resources.GetBlueprint<BlueprintAbility>("07e8f6479cbcc3f46a12696784805305");
            var FireStormSpell = Resources.GetBlueprint<BlueprintAbility>("e3d0dfe1c8527934294f241e0ae96a8d");
            var IceStorm = Resources.GetBlueprint<BlueprintAbility>("fcb028205a71ee64d98175ff39a0abf9");
            var LightningBoltSpell = Resources.GetBlueprint<BlueprintAbility>("d2cff9243a7ee804cb6d5be47af30c73");
            var ProtectionFromArrowsSpell = Resources.GetBlueprint<BlueprintAbility>("c28de1f98a3f432448e52e5d47c73208");
            var ShockingGraspCast = Resources.GetBlueprint<BlueprintAbility>("ab395d2335d3f384e99dddee8562978f");
            var ShoutGreaterSpell = Resources.GetBlueprint<BlueprintAbility>("fd0d3840c48cafb44bb29e8eb74df204");
            var ShoutSpell = Resources.GetBlueprint<BlueprintAbility>("f09453607e683784c8fca646eec49162");
            var SiroccoSpell = Resources.GetBlueprint<BlueprintAbility>("093ed1d67a539ad4c939d9d05cfe192c");
            var SlowMudSpell = Resources.GetBlueprint<BlueprintAbility>("6b30813c3709fc44b92dc8fd8191f345");
            var SnowballSpell = Resources.GetBlueprint<BlueprintAbility>("9f10909f0be1f5141bf1c102041f93d9");
            var SummonElementalSmallBaseSpell = Resources.GetBlueprint<BlueprintAbility>("970c6db48ff0c6f43afc9dbb48780d03");
            var SunburstSpell = Resources.GetBlueprint<BlueprintAbility>("e96424f70ff884947b06f41a765b7658");
            var TsunamiSpell = Resources.GetBlueprint<BlueprintAbility>("d8144161e352ca846a73cf90e85bf9ac");
            var WindsOfVengeanceSpell = Resources.GetBlueprint<BlueprintAbility>("5d8f1da2fdc0b9242af9f326f9e507be");

            var DismissAreaEffect = Resources.GetBlueprint<BlueprintAbility>("97a23111df7547fd8f6417f9ba9b9775");


            var StormDruidSpontaneousAirDomain = Helpers.CreateBlueprint<BlueprintFeature>("StormDruidSpontaneousAirDomain", bp => {
                bp.SetName("Spontaneous Air Domain Spellcasting");
                bp.SetDescription("A storm druid can channel stored {g|Encyclopedia:Spell}spell{/g} energy into air domain spells that she hasn't prepared ahead of time. She can " +
                    "\"lose\" a prepared spell in order to cast any air domain spell of the same level.");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        ShockingGraspCast.ToReference<BlueprintAbilityReference>(),
                        ProtectionFromArrowsSpell.ToReference<BlueprintAbilityReference>(),
                        LightningBoltSpell.ToReference<BlueprintAbilityReference>(),
                        ShoutSpell.ToReference<BlueprintAbilityReference>(),
                        CloudkillSpell.ToReference<BlueprintAbilityReference>(),
                        ChainLightningSpell.ToReference<BlueprintAbilityReference>(),
                        ElementalBodyIVAirSpell.ToReference<BlueprintAbilityReference>(),
                        ShoutGreaterSpell.ToReference<BlueprintAbilityReference>(),
                        ElementalSwarmAir.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StormDruidSpontaneousWeatherDomain = Helpers.CreateBlueprint<BlueprintFeature>("StormDruidSpontaneousWeatherDomain", bp => {
                bp.SetName("Spontaneous Weather Domain Spellcasting");
                bp.SetDescription("A storm druid can channel stored {g|Encyclopedia:Spell}spell{/g} energy into weather domain spells that she hasn't prepared ahead of time. She can " +
                    "\"lose\" a prepared spell in order to cast any weather domain spell of the same level.");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        SnowballSpell.ToReference<BlueprintAbilityReference>(),
                        SummonElementalSmallBaseSpell.ToReference<BlueprintAbilityReference>(),
                        CallLightningSpell.ToReference<BlueprintAbilityReference>(),
                        SlowMudSpell.ToReference<BlueprintAbilityReference>(),
                        IceStorm.ToReference<BlueprintAbilityReference>(),
                        SiroccoSpell.ToReference<BlueprintAbilityReference>(),
                        FireStormSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        TsunamiSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StormDruidSpontaneousWindDomain = Helpers.CreateBlueprint<BlueprintFeature>("StormDruidSpontaneousWindDomain", bp => {
                bp.SetName("Spontaneous Wind Domain Spellcasting");
                bp.SetDescription("A storm druid can channel stored {g|Encyclopedia:Spell}spell{/g} energy into wind domain spells that she hasn't prepared ahead of time. She can " +
                    "\"lose\" a prepared spell in order to cast any wind domain spell of the same level.");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        EarPiercingScreamSpell.ToReference<BlueprintAbilityReference>(),
                        ProtectionFromArrowsSpell.ToReference<BlueprintAbilityReference>(),
                        LightningBoltSpell.ToReference<BlueprintAbilityReference>(),
                        ShoutSpell.ToReference<BlueprintAbilityReference>(),
                        CloudkillSpell.ToReference<BlueprintAbilityReference>(),
                        SiroccoSpell.ToReference<BlueprintAbilityReference>(),
                        ElementalBodyIVAirSpell.ToReference<BlueprintAbilityReference>(),
                        ShoutGreaterSpell.ToReference<BlueprintAbilityReference>(),
                        WindsOfVengeanceSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var StormDruidSpontaneousStormDomain = Helpers.CreateBlueprint<BlueprintFeature>("StormDruidSpontaneousStormDomain", bp => {
                bp.SetName("Spontaneous Storm Domain Spellcasting");
                bp.SetDescription("A storm druid can channel stored {g|Encyclopedia:Spell}spell{/g} energy into storm domain spells that she hasn't prepared ahead of time. She can " +
                    "\"lose\" a prepared spell in order to cast any storm domain spell of the same level.");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        SnowballSpell.BlueprintAbilityReference,
                        SummonElementalSmallBaseSpell.ToReference<BlueprintAbilityReference>(),
                        CallLightningSpell.ToReference<BlueprintAbilityReference>(),
                        SlowMudSpell.ToReference<BlueprintAbilityReference>(),
                        CallLightningStormSpell.ToReference<BlueprintAbilityReference>(),
                        SiroccoSpell.ToReference<BlueprintAbilityReference>(),
                        FireStormSpell.ToReference<BlueprintAbilityReference>(),
                        SunburstSpell.ToReference<BlueprintAbilityReference>(),
                        TsunamiSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
            });
            var SpontaneousDomainCastingStormDruid = Helpers.CreateBlueprint<BlueprintFeature>("SpontaneousDomainCastingStormDruid", bp => {
                bp.SetName("Spontaneous Domain Spellcasting");
                bp.SetDescription("A storm druid can channel stored {g|Encyclopedia:Spell}spell{/g} energy into domain spells that she hasn't prepared ahead of time. She can " +
                    "\"lose\" a prepared spell in order to cast any domain spell of the same level.");
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = AirDomainProgressionDruid.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = StormDruidSpontaneousAirDomain.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = WeatherDomainProgressionDruid.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = StormDruidSpontaneousWeatherDomain.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = StormDomainProgressionDruid.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = StormDruidSpontaneousStormDomain.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = WindDomainProgressionDruid.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = StormDruidSpontaneousWindDomain.ToReference<BlueprintUnitFactReference>();
                });
                bp.ReapplyOnLevelUp = true;
                bp.HideInUI = false;
                bp.IsClassFeature = true;
            });
            var StormDruidBondSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("StormDruidBondSelection", bp => {
                bp.SetName("Storm Druids Bond");
                bp.SetDescription("A storm druid may not choose an animal companion. A storm druid must choose the Air or Weather domain, or the Storm or Wind subdomain.");
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.OnlyNew;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddFeatures(AirDomainProgressionDruid, WeatherDomainProgressionDruid, StormDomainProgressionDruid, WindDomainProgressionDruid);
            });
            var WindwalkerFeature = Helpers.CreateBlueprint<BlueprintFeature>("WindwalkerFeature", bp => {
                bp.SetName("Windwalker");
                bp.SetDescription("At 4th level, a StormDruid gains a +4 bonus on saving throws to avoid confusion, daze, feeblemind, and insanity effects. She also gains a +4 bonus " +
                    "against the, spell-like, and supernatural abilities of creatures with the shapechanger subtype.");
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = RainLightBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = SnowLightBuff.ToReference<BlueprintBuffReference>();
                });
            });
            var EyesOfTheStormFeature = Helpers.CreateBlueprint<BlueprintFeature>("EyesOfTheStormFeature", bp => {
                bp.SetName("Eyes of the Storm");
                bp.SetDescription("At 4th level, a StormDruid gains a +4 bonus on saving throws to avoid confusion, daze, feeblemind, and insanity effects. She also gains a +4 bonus " +
                    "against the, spell-like, and supernatural abilities of creatures with the shapechanger subtype.");
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = SnowModerateBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = RainModerate.ToReference<BlueprintBuffReference>();
                });                          
            });
            var WindlordDomainSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("WindlordDomainSelection", bp => {
                bp.SetName("Windlords Domain");
                bp.SetDescription("A storm druid may not choose an animal companion. A storm druid must choose the Air or Weather domain, or the Storm, or Wind subdomain.");
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.OnlyNew;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddFeatures(AirDomainProgressionDruid, WeatherDomainProgressionDruid, StormDomainProgressionDruid, WindDomainProgressionDruid);
            });
            var StormLordFeature = Helpers.CreateBlueprint<BlueprintFeature>("StormLordFeature", bp => {
                bp.SetName("Storm Lord");
                bp.SetDescription("At 4th level, a StormDruid gains a +4 bonus on saving throws to avoid confusion, daze, feeblemind, and insanity effects. She also gains a +4 bonus " +
                    "against the, spell-like, and supernatural abilities of creatures with the shapechanger subtype.");
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.Sonic;
                    c.ModifierDescriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 2;
                    c.Bonus = new ContextValue();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = RainHeavyBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = RainStormBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = SnowHeavyBuff.ToReference<BlueprintBuffReference>();
                });
            });
            StormDruidArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, DruidBondSelection, DruidSpontaneousSummonFeature),
                    Helpers.LevelEntry(2, WoodlandStrideFeature),
                    Helpers.LevelEntry(4, ResistNaturesLureFeature),
                    Helpers.LevelEntry(9, VenomImmunityFeature),
            };
            StormDruidArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, StormDruidBondSelection, SpontaneousDomainCastingStormDruid),
                    Helpers.LevelEntry(2, WindwalkerFeature),
                    Helpers.LevelEntry(4, EyesOfTheStormFeature),
                    Helpers.LevelEntry(9, WindlordDomainSelection),
                    Helpers.LevelEntry(13, StormLordFeature)
            };
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Storm Druid")) { return; }
            DruidClass.m_Archetypes = DruidClass.m_Archetypes.AppendToArray(StormDruidArchetype.ToReference<BlueprintArchetypeReference>());

        }
    }
}
