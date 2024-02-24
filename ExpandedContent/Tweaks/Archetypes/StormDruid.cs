using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class StormDruid {
        public static void AddStormDruid() {

            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var DruidBondSelection = Resources.GetBlueprintReference<BlueprintFeatureReference>("3830f3630a33eba49b60f511b4c8f2a8");
            var WoodlandStrideFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("4c1419ef6cfc430a9071405788da4a73");
            var DruidSpontaneousSummonFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("b296531ffe013c8499ad712f8ae97f6b");
            var VenomImmunityFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("5078622eb5cecaf4683fa16a9b948c2c");
            var ResistNaturesLureFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("ad6a5b0e1a65c3540986cf9a7b006388");
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
            var CallLightningSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2a9ef0e0b5822a24d88b16673a267456");
            var CallLightningStormSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d5a36a7ee8177be4f848b953d1c53c84");
            var ChainLightningSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("645558d63604747428d55f0dd3a4cb58");
            var CloudkillSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("548d339ba87ee56459c98e80167bdf10");
            var EarPiercingScreamSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("8e7cfa5f213a90549aadd18f8f6f4664");
            var ElementalBodyIVAirSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("ee63301f83c76694692d4704d8a05bdc");
            var ElementalSwarmAir = Resources.GetBlueprintReference<BlueprintAbilityReference>("07e8f6479cbcc3f46a12696784805305");
            var FireStormSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e3d0dfe1c8527934294f241e0ae96a8d");
            var IceStorm = Resources.GetBlueprintReference<BlueprintAbilityReference>("fcb028205a71ee64d98175ff39a0abf9");
            var LightningBoltSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d2cff9243a7ee804cb6d5be47af30c73");
            var ProtectionFromArrowsSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c28de1f98a3f432448e52e5d47c73208");
            var ShockingGraspCast = Resources.GetBlueprintReference<BlueprintAbilityReference>("ab395d2335d3f384e99dddee8562978f");
            var ShoutGreaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fd0d3840c48cafb44bb29e8eb74df204");
            var ShoutSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f09453607e683784c8fca646eec49162");
            var SiroccoSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("093ed1d67a539ad4c939d9d05cfe192c");
            var SlowMudSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("6b30813c3709fc44b92dc8fd8191f345");
            var SnowballSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("9f10909f0be1f5141bf1c102041f93d9");
            var SummonElementalSmallBaseSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("970c6db48ff0c6f43afc9dbb48780d03");
            var SunburstSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e96424f70ff884947b06f41a765b7658");
            var TsunamiSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d8144161e352ca846a73cf90e85bf9ac");
            var WindsOfVengeanceSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("5d8f1da2fdc0b9242af9f326f9e507be");


            var StormDruidSpontaneousAirDomain = Helpers.CreateBlueprint<BlueprintFeature>("StormDruidSpontaneousAirDomain", bp => {
                bp.SetName("Spontaneous Air Domain Spellcasting");
                bp.SetDescription("A storm druid can channel stored {g|Encyclopedia:Spell}spell{/g} energy into air domain spells that she hasn't prepared ahead of time. She can " +
                    "\"lose\" a prepared spell in order to cast any air domain spell of the same level.");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        ShockingGraspCast,
                        ProtectionFromArrowsSpell,
                        LightningBoltSpell,
                        ShoutSpell,
                        CloudkillSpell,
                        ChainLightningSpell,
                        ElementalBodyIVAirSpell,
                        ShoutGreaterSpell,
                        ElementalSwarmAir
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
                        SnowballSpell,
                        SummonElementalSmallBaseSpell,
                        CallLightningSpell,
                        SlowMudSpell,
                        IceStorm,
                        SiroccoSpell,
                        FireStormSpell,
                        SunburstSpell,
                        TsunamiSpell
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
                        EarPiercingScreamSpell,
                        ProtectionFromArrowsSpell,
                        LightningBoltSpell,
                        ShoutSpell,
                        CloudkillSpell,
                        SiroccoSpell,
                        ElementalBodyIVAirSpell,
                        ShoutGreaterSpell,
                        WindsOfVengeanceSpell
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
                        SnowballSpell,
                        SummonElementalSmallBaseSpell,
                        CallLightningSpell,
                        SlowMudSpell,
                        CallLightningStormSpell,
                        SiroccoSpell,
                        FireStormSpell,
                        SunburstSpell,
                        TsunamiSpell
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
                bp.SetDescription("At 2nd level, a storm druid is unaffected by all light wind and snow effects.");
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = RainLightBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = SnowLightBuff.ToReference<BlueprintBuffReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var EyesOfTheStormFeature = Helpers.CreateBlueprint<BlueprintFeature>("EyesOfTheStormFeature", bp => {
                bp.SetName("Eyes of the Storm");
                bp.SetDescription("At 4th level, a storm druid is unaffected by all moderate wind and snow effects.");
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = SnowModerateBuff.ToReference<BlueprintBuffReference>();
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.m_Buff = RainModerate.ToReference<BlueprintBuffReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var WindlordDomainSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("WindlordDomainSelection", bp => {
                bp.SetName("Windlords Domain");
                bp.SetDescription("At 9th level, a storm druid can select another domain or subdomain from those available to her through her nature bond.");
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
                bp.SetDescription("At 13th level, a storm druid is unaffected by all natural and magical wind effects. She gains +2 bonus on saving throws against sonic effects.");
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
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
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
