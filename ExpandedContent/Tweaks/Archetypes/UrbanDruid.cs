using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Utility;
using Kingmaker.Enums;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class UrbanDruid {
        public static void AddUrbanDruid() {

            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var DruidBondSelection = Resources.GetBlueprintReference<BlueprintFeatureReference>("3830f3630a33eba49b60f511b4c8f2a8");
            var WoodlandStrideFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("11f4072ea766a5840a46e6660894527d");
            var DruidSpontaneousSummonFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("b296531ffe013c8499ad712f8ae97f6b");
            var VenomImmunityFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("5078622eb5cecaf4683fa16a9b948c2c");
            var ResistNaturesLureFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("ad6a5b0e1a65c3540986cf9a7b006388");

            var WildShapeIWolfFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("19bb148cb92db224abb431642d10efeb");
            var WildShapeIILeopardFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("c4d651bc0d4eabd41b08ee81bfe701d8");
            var WildShapeElementalSmallFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("bddd46a6f6a3e6e4b99008dcf5271c3b");
            var WildShapeIVBearFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("1368c7ce69702444893af5ffd3226e19");
            var WildShapeElementalFeatureAddMediumFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("6e4b88e2a044c67469c038ac2f09d061");
            var WildShapeIIISmilodonFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("253c0c0d00e50a24797445f20af52dc8");
            var WildShapeElementalFeatureAddLargeFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("e66154511a6f9fc49a9de644bd8922db");
            var WildShapeIVShamblingMoundFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("0f31b23c2ab39354bbde4e33e8151495");
            var WildShapeElementalHugeFeatureFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("fe58dd496a36e274b86958f4677071b2");



            var WeatherDomainProgressionDruid = Resources.GetBlueprint<BlueprintProgression>("4a3516fdc4cda764ebd1279b22d10205");
            var CharmDomainProgressionDruid = Resources.GetModBlueprint<BlueprintProgression>("CharmDomainProgressionDruid");
            var CommunityDomainProgressionDruid = Resources.GetModBlueprint<BlueprintProgression>("CommunityDomainProgressionDruid");
            var KnowledgeDomainProgressionDruid = Resources.GetModBlueprint<BlueprintProgression>("KnowledgeDomainProgressionDruid");
            var NobilityDomainProgressionDruid = Resources.GetModBlueprint<BlueprintProgression>("NobilityDomainProgressionDruid");
            var ProtectionDomainProgressionDruid = Resources.GetModBlueprint<BlueprintProgression>("ProtectionDomainProgressionDruid");
            var ReposeDomainProgressionDruid = Resources.GetModBlueprint<BlueprintProgression>("ReposeDomainProgressionDruid");
            var RuneDomainProgressionDruid = Resources.GetModBlueprint<BlueprintProgression>("RuneDomainProgressionDruid");

            var UrbanDruidArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("UrbanDruidArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"UrbanDruidArchetype.Name", "Urban Druid");
                bp.LocalizedDescription = Helpers.CreateString($"UrbanDruidArchetype.Description", "While many druids keep to the wilderness, some make their way within settlements, " +
                    "communing with the animals and vermin who live there and speaking for the nature that runs rampant in civilization’s very cradle.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"UrbanDruidArchetype.Description", "While many druids keep to the wilderness, some make their way within " +
                    "settlements, communing with the animals and vermin who live there and speaking for the nature that runs rampant in civilization’s very cradle.");
            });
            var UrbanDruidWildShape = Helpers.CreateBlueprint<BlueprintFeature>("UrbanDruidWildShape", bp => {
                bp.SetName("Wild Shape - Urban Druid");
                bp.SetDescription("Due to the urban druids closeness to civilization, wild shape transformations are much more difficult to acheve. An urban druid gains wild shape forms " +
                    "4 levels later than a standard druid.");
                bp.IsClassFeature = true;
            });

            //Spell bank
            var BarkskinSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("5b77d7cc65b8ab74688e74a37fc2f553");
            var BlessSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("90e59f4a4ada87243b7b3535a06d0638");
            var BrilliantInspirationSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("a5c56f0f699daec44b7aedd8b273b08a");
            var BurstOfGlorySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("1bc83efec9f8c4b42a46162d72cbf494");
            var CallLightningSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2a9ef0e0b5822a24d88b16673a267456");
            var DeathWardCastSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e9cc9378fd6841f48ad59384e79e9953");
            var DeepSlumberSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("7658b74f626c56a49939d9c20580885e");
            var DestructionSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("3b646e1db3403b940bf620e01d2ce0c7");
            var DispelMagicGreaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f0f761b808dc4b149b08eaf44b99f633");
            var DivineFavorSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("9d5d2d3ffdd73c648af3eb3e585b1113");
            var DominateMonsterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("3c17035ec4717674cae2e841a190e757");
            var DominatePersonSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d7cbd2004ce66a042aeab2e95a3c5c61");
            var DoomSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fbdd8c455ac4cde4a9a3e18c84af9485");
            var EaglesSplendorMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2caa607eadda4ab44934c5c9875e01bc");
            var EuphoricTranquilitySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("740d943e42b60f64a8de74926ba6ddf7");
            var FireStormSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e3d0dfe1c8527934294f241e0ae96a8d");
            var FoxsCunningMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2b24159ad9907a8499c2313ba9c0f615");
            var FoxsCunningSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("ae4d3ad6a8fda1542acf2e9bbc13d113");
            var FrightfulAspectSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e788b02f8d21014488067bdd3ba7b325");
            var GraceSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("464a7193519429f48b4d190acb753cf0");
            var HealMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("867524328b54f25488d371214eea0d90");
            var HeroismGreaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e15e5e7045fda2244b98c8f010adfe31");
            var HeroismSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("5ab0d42fb68c9e34abae4921822b9d63");
            var HideousLaughterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fd4d9fd7f87575d47aafe2a64a6e2d8d");
            var HypnotismSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("88367310478c10b47903463c5d0152b0");
            var IceStorm = Resources.GetBlueprintReference<BlueprintAbilityReference>("fcb028205a71ee64d98175ff39a0abf9");
            var InsanitySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2b044152b3620c841badb090e01ed9de");
            var LegendaryProportionsSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("da1b292d91ba37948893cdbe9ea89e28");
            var MagicalVestmentSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2d4263d80f5136b4296d6eb43a221d7d");
            var OverwhelmingPresenceSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("41cf93453b027b94886901dbfc680cb9");
            var PowerWordBlindSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("261e1788bfc5ac1419eec68b1d485dbc");
            var PowerWordKillSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2f8a67c483dfa0f439b293e094ca9e3c");
            var PowerWordStunSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f958ef62eea5050418fb92dfa944c631");
            var PrayerSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("faabd2cc67efa4646ac58c7bb3e40fcc");
            var ProtectionFromAlignmentCommunalSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2cadf6c6350e4684baa109d067277a45");
            var ProtectionFromAlignmentSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("433b1faf4d02cc34abb0ade5ceda47c4");
            var ProtectionFromArrowsCommunalSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("96c9d98b6a9a7c249b6c4572e4977157");
            var ProtectionFromArrowsSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c28de1f98a3f432448e52e5d47c73208");
            var ProtectionFromEnergyCommunalSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("76a629d019275b94184a1a8733cac45e");
            var ProtectionFromEnergySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d2f116cfe05fcdd4a94e80143b67046f");
            var ProtectionFromSpellsSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("42aa71adc7343714fa92e471baa98d42");
            var RainbowPatternSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4b8265132f9c8174f87ce7fa6d0fe47b");
            var RestorationGreaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fafd77c6bfa85c04ba31fdc1c962c914");
            var ScareSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("08cb5f4c3b2695e44971bf5c45205df0");
            var SeamantleSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("7ef49f184922063499b8f1346fb7f521");
            var SeeInvisibilityCommunalSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("1a045f845778dc54db1c2be33a8c3c0a");
            var SiroccoSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("093ed1d67a539ad4c939d9d05cfe192c");
            var SlayLivingCastSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4fbd47525382517419c66fb548fe9a67");
            var SlowMudSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("6b30813c3709fc44b92dc8fd8191f345");
            var SnowballSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("9f10909f0be1f5141bf1c102041f93d9");
            var SpellResistanceSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("0a5ddfbcfb3989543ac7c936fc256889");
            var StoneskinCommunalSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("7c5d556b9a5883048bf030e20daebe31");
            var SummonElementalSmallBaseSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("970c6db48ff0c6f43afc9dbb48780d03");
            var SunburstSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e96424f70ff884947b06f41a765b7658");
            var TrueSeeingCastSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4cf3d0fae3239ec478f51e86f49161cb");
            var TrueStrikeSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2c38da66e5a599347ac95b3294acbe00");
            var TsunamiSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d8144161e352ca846a73cf90e85bf9ac");
            var UndeadToDeathSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("a9a52760290591844a96d0109e30e04d");
            var VampiricTouchCastSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("8a28a811ca5d20d49a863e832c31cce1");
            var WailOfBansheeSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("b24583190f36a8442b212e45226c54fc");
            var WavesOfExhaustionSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("3e4d3b9a5bd03734d9b053b9067c2f38");

            var ProtectionFromAlignmentEvilSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("eee384c813b6d74498d1b9cc720d61f4");
            var ProtectionFromAlignmentGoodSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2ac7637daeb2aa143a3bae860095b63e");
            var ProtectionFromAlignmentLawSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c3aafbbb6e8fc754fb8c82ede3280051");
            var ProtectionFromAlignmentChaosSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("1eaf1020e82028d4db55e6e464269e00");

            var ProtectionFromAlignmentCommunalEvilSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("93f391b0c5a99e04e83bbfbe3bb6db64");
            var ProtectionFromAlignmentCommunalGoodSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("5bfd4cce1557d5744914f8f6d85959a4");
            var ProtectionFromAlignmentCommunalLawSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("8b8ccc9763e3cc74bbf5acc9c98557b9");
            var ProtectionFromAlignmentCommunalChaosSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("0ec75ec95d9e39d47a23610123ba1bad");

            var ProtectionFromEnergyAcidSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("3d77ee3fc4913c44b9df7c5bbcdc4906");
            var ProtectionFromEnergyColdSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("021d39c8e0eec384ba69140f4875e166");
            var ProtectionFromEnergyElectricitySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("e24ce0c3e8eaaaf498d3656b534093df");
            var ProtectionFromEnergyFireSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("3f9605134d34e1243b096e1f6cb4c148");
            var ProtectionFromEnergySonicSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("0cee375b4e5265a46a13fc269beb8763");

            var ProtectionFromEnergyCommunalAcidSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("de4e28c053d936d45b6c9e361f90acc2");
            var ProtectionFromEnergyCommunalColdSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("3bd50e8acc303d244a1cec9df04ad050");
            var ProtectionFromEnergyCommunalElectricitySpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f10cd112b876a6f449d52dee0a57e602");
            var ProtectionFromEnergyCommunalFireSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("2903d31d6c8356547aa4aae5a3e7a655");
            var ProtectionFromEnergyCommunalSonicSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("b065b2f0589225f4897fc8b345acbfb6");

            var SummonElementalSmallAirSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("9cc6b61eba880b944a8f489c44640b5c");
            var SummonElementalSmallEarthSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("69b36426bb910e341a943f101daed594");
            var SummonElementalSmallFireSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d8f88028204bc2041be9d9d51f58e6a5");
            var SummonElementalSmallWaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("107788f47c4481f4db6da06498b28270");



            var UrbanDruidSpontaneousCharmDomain = Helpers.CreateBlueprint<BlueprintFeature>("UrbanDruidSpontaneousCharmDomain", bp => {
                bp.SetName("Spontaneous Charm Domain Spellcasting");
                bp.SetDescription("A urban druid can channel stored {g|Encyclopedia:Spell}spell{/g} energy into charm domain spells that she hasn't prepared ahead of time. She can " +
                    "\"lose\" a prepared spell in order to cast any charm domain spell of the same level.");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        HypnotismSpell,
                        HideousLaughterSpell,
                        DeepSlumberSpell,
                        RainbowPatternSpell,
                        DominatePersonSpell,
                        EaglesSplendorMassSpell,
                        InsanitySpell,
                        EuphoricTranquilitySpell,
                        DominateMonsterSpell
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
            var UrbanDruidSpontaneousCommunityDomain = Helpers.CreateBlueprint<BlueprintFeature>("UrbanDruidSpontaneousCommunityDomain", bp => {
                bp.SetName("Spontaneous Community Domain Spellcasting");
                bp.SetDescription("A urban druid can channel stored {g|Encyclopedia:Spell}spell{/g} energy into community domain spells that she hasn't prepared ahead of time. She can " +
                    "\"lose\" a prepared spell in order to cast any community domain spell of the same level.");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        BlessSpell,
                        ProtectionFromAlignmentCommunalEvilSpell,
                        PrayerSpell,
                        ProtectionFromEnergyCommunalAcidSpell,
                        BurstOfGlorySpell,
                        StoneskinCommunalSpell,
                        RestorationGreaterSpell,
                        LegendaryProportionsSpell,
                        HealMassSpell
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        ProtectionFromAlignmentCommunalGoodSpell,
                        new BlueprintAbilityReference(),
                        ProtectionFromEnergyCommunalColdSpell,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        ProtectionFromAlignmentCommunalLawSpell,
                        new BlueprintAbilityReference(),
                        ProtectionFromEnergyCommunalElectricitySpell,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        ProtectionFromAlignmentCommunalChaosSpell,
                        new BlueprintAbilityReference(),
                        ProtectionFromEnergyCommunalFireSpell,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        ProtectionFromEnergyCommunalSonicSpell,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
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
            var UrbanDruidSpontaneousKnowledgeDomain = Helpers.CreateBlueprint<BlueprintFeature>("UrbanDruidSpontaneousKnowledgeDomain", bp => {
                bp.SetName("Spontaneous Knowledge Domain Spellcasting");
                bp.SetDescription("A urban druid can channel stored {g|Encyclopedia:Spell}spell{/g} energy into knowledge domain spells that she hasn't prepared ahead of time. She can " +
                    "\"lose\" a prepared spell in order to cast any knowledge domain spell of the same level.");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        TrueStrikeSpell,
                        FoxsCunningSpell,
                        SeeInvisibilityCommunalSpell,
                        DeathWardCastSpell,
                        TrueSeeingCastSpell,
                        FoxsCunningMassSpell,
                        PowerWordBlindSpell,
                        PowerWordStunSpell,
                        PowerWordKillSpell
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
            var UrbanDruidSpontaneousNobilityDomain = Helpers.CreateBlueprint<BlueprintFeature>("UrbanDruidSpontaneousNobilityDomain", bp => {
                bp.SetName("Spontaneous Nobility Domain Spellcasting");
                bp.SetDescription("A urban druid can channel stored {g|Encyclopedia:Spell}spell{/g} energy into nobility domain spells that she hasn't prepared ahead of time. She can " +
                    "\"lose\" a prepared spell in order to cast any nobility domain spell of the same level.");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        DivineFavorSpell,
                        GraceSpell,
                        MagicalVestmentSpell,
                        HeroismSpell,
                        DominatePersonSpell,
                        BrilliantInspirationSpell,
                        HeroismGreaterSpell,
                        FrightfulAspectSpell,
                        OverwhelmingPresenceSpell
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
            var UrbanDruidSpontaneousProtectionDomain = Helpers.CreateBlueprint<BlueprintFeature>("UrbanDruidSpontaneousProtectionDomain", bp => {
                bp.SetName("Spontaneous Protection Domain Spellcasting");
                bp.SetDescription("A urban druid can channel stored {g|Encyclopedia:Spell}spell{/g} energy into protection domain spells that she hasn't prepared ahead of time. She can " +
                    "\"lose\" a prepared spell in order to cast any protection domain spell of the same level.");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        ProtectionFromAlignmentEvilSpell,
                        BarkskinSpell,
                        ProtectionFromEnergyAcidSpell,
                        ProtectionFromEnergyCommunalAcidSpell,
                        SpellResistanceSpell,
                        StoneskinCommunalSpell,
                        RestorationGreaterSpell,
                        ProtectionFromSpellsSpell,
                        SeamantleSpell
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        ProtectionFromAlignmentGoodSpell,
                        new BlueprintAbilityReference(),
                        ProtectionFromEnergyColdSpell,
                        ProtectionFromEnergyCommunalColdSpell,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        ProtectionFromAlignmentLawSpell,
                        new BlueprintAbilityReference(),
                        ProtectionFromEnergyElectricitySpell,
                        ProtectionFromEnergyCommunalElectricitySpell,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        ProtectionFromAlignmentChaosSpell,
                        new BlueprintAbilityReference(),
                        ProtectionFromEnergyFireSpell,
                        ProtectionFromEnergyCommunalFireSpell,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        ProtectionFromEnergySonicSpell,
                        ProtectionFromEnergyCommunalSonicSpell,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
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
            var UrbanDruidSpontaneousReposeDomain = Helpers.CreateBlueprint<BlueprintFeature>("UrbanDruidSpontaneousReposeDomain", bp => {
                bp.SetName("Spontaneous Repose Domain Spellcasting");
                bp.SetDescription("A urban druid can channel stored {g|Encyclopedia:Spell}spell{/g} energy into repose domain spells that she hasn't prepared ahead of time. She can " +
                    "\"lose\" a prepared spell in order to cast any repose domain spell of the same level.");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        DoomSpell,
                        ScareSpell,
                        VampiricTouchCastSpell,
                        DeathWardCastSpell,
                        SlayLivingCastSpell,
                        UndeadToDeathSpell,
                        DestructionSpell,
                        WavesOfExhaustionSpell,
                        WailOfBansheeSpell
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
            var UrbanDruidSpontaneousRuneDomain = Helpers.CreateBlueprint<BlueprintFeature>("UrbanDruidSpontaneousRuneDomain", bp => {
                bp.SetName("Spontaneous Rune Domain Spellcasting");
                bp.SetDescription("A urban druid can channel stored {g|Encyclopedia:Spell}spell{/g} energy into rune domain spells that she hasn't prepared ahead of time. She can " +
                    "\"lose\" a prepared spell in order to cast any rune domain spell of the same level.");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        ProtectionFromAlignmentEvilSpell,
                        ProtectionFromArrowsSpell,
                        ProtectionFromArrowsCommunalSpell,
                        ProtectionFromEnergyCommunalAcidSpell,
                        SpellResistanceSpell,
                        DispelMagicGreaterSpell,
                        PowerWordBlindSpell,
                        PowerWordStunSpell,
                        PowerWordKillSpell
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        ProtectionFromAlignmentGoodSpell,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        ProtectionFromEnergyCommunalColdSpell,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        ProtectionFromAlignmentLawSpell,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        ProtectionFromEnergyCommunalElectricitySpell,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        ProtectionFromAlignmentChaosSpell,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        ProtectionFromEnergyCommunalFireSpell,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        ProtectionFromEnergyCommunalSonicSpell,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
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
            var UrbanDruidSpontaneousWeatherDomain = Helpers.CreateBlueprint<BlueprintFeature>("UrbanDruidSpontaneousWeatherDomain", bp => {
                bp.SetName("Spontaneous Weather Domain Spellcasting");
                bp.SetDescription("A urban druid can channel stored {g|Encyclopedia:Spell}spell{/g} energy into weather domain spells that she hasn't prepared ahead of time. She can " +
                    "\"lose\" a prepared spell in order to cast any weather domain spell of the same level.");
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        SnowballSpell,
                        SummonElementalSmallAirSpell,
                        CallLightningSpell,
                        SlowMudSpell,
                        IceStorm,
                        SiroccoSpell,
                        FireStormSpell,
                        SunburstSpell,
                        TsunamiSpell
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonElementalSmallEarthSpell,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonElementalSmallFireSpell,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
                    };
                });
                bp.AddComponent<SpontaneousSpellConversion>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellsByLevel = new BlueprintAbilityReference[10] {
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        SummonElementalSmallWaterSpell,
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference(),
                        new BlueprintAbilityReference()
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
            var SpontaneousDomainCastingUrbanDruid = Helpers.CreateBlueprint<BlueprintFeature>("SpontaneousDomainCastingUrbanDruid", bp => {
                bp.SetName("Spontaneous Domain Spellcasting");
                bp.SetDescription("A urban druid can channel stored {g|Encyclopedia:Spell}spell{/g} energy into domain spells that she hasn't prepared ahead of time. She can " +
                    "\"lose\" a prepared spell in order to cast any domain spell of the same level.");
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = CharmDomainProgressionDruid.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = UrbanDruidSpontaneousCharmDomain.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = CommunityDomainProgressionDruid.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = UrbanDruidSpontaneousCommunityDomain.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = KnowledgeDomainProgressionDruid.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = UrbanDruidSpontaneousKnowledgeDomain.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = NobilityDomainProgressionDruid.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = UrbanDruidSpontaneousNobilityDomain.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = ProtectionDomainProgressionDruid.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = UrbanDruidSpontaneousProtectionDomain.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = ReposeDomainProgressionDruid.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = UrbanDruidSpontaneousReposeDomain.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = RuneDomainProgressionDruid.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = UrbanDruidSpontaneousRuneDomain.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = WeatherDomainProgressionDruid.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = UrbanDruidSpontaneousWeatherDomain.ToReference<BlueprintUnitFactReference>();
                });
                bp.ReapplyOnLevelUp = true;
                bp.HideInUI = false;
                bp.IsClassFeature = true;
            });
            var UrbanDruidBondSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("UrbanDruidBondSelection", bp => {
                bp.SetName("Urban Druids Bond");
                bp.SetDescription("An urban druid may not select an animal companion. Instead, she must choose from the following domains, rather than those usually " +
                    "available to druids: Charm, Community, Knowledge, Nobility, Protection, Repose, Rune, or Weather.");
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.OnlyNew;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddFeatures(CharmDomainProgressionDruid, CommunityDomainProgressionDruid, KnowledgeDomainProgressionDruid, NobilityDomainProgressionDruid, ProtectionDomainProgressionDruid,
                    ReposeDomainProgressionDruid, RuneDomainProgressionDruid, WeatherDomainProgressionDruid);
            });
            var UrbanDruidLorekeeperFeature = Helpers.CreateBlueprint<BlueprintFeature>("UrbanDruidLorekeeperFeature", bp => {
                bp.SetName("Lorekeeper");
                bp.SetDescription("At 2nd level an urban druid adds Use Magic Device to her class skills. She also gains a +2 bonus on Use Magic Device, Knowledge (Arcana), and Knowledge (World) " +
                    "skill checks");
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillUseMagicDevice;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillUseMagicDevice;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillKnowledgeArcana;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillKnowledgeWorld;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 2;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var UrbanDruidResistTemptationFeature = Helpers.CreateBlueprint<BlueprintFeature>("UrbanDruidResistTemptationFeature", bp => {
                bp.SetName("Resist Temptation");
                bp.SetDescription("At 4th level, an urban druid gains a +2 bonus on saves against divinations and enchantments.");
                bp.AddComponent<SavingThrowBonusAgainstSchool>(c => {
                    c.School = SpellSchool.Divination | SpellSchool.Enchantment;
                    c.ModifierDescriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 2;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var UrbanDruidMentalStrengthFeature = Helpers.CreateBlueprint<BlueprintFeature>("UrbanDruidMentalStrengthFeature", bp => {
                bp.SetName("Mental Strength");
                bp.SetDescription("At 9th level, an urban druid gains immunity to charm and compulsion effects.");
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.Descriptor = SpellDescriptor.Charm | SpellDescriptor.Compulsion;
                    c.m_IgnoreFeature = null;
                    c.CheckFact = false;
                    c.m_FactToCheck = null;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            //Adding Urban Druid to Domain Configs
            //Charm Domain Hooks
            var CharmDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("84cd24a110af59140b066bc2c69619bd").GetComponent<ContextRankConfig>();
            CharmDomainBaseAbilityConfig.m_AdditionalArchetypes = CharmDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            CharmDomainBaseAbilityConfig.m_Class = CharmDomainBaseAbilityConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            var CharmDomainBaseFeatureComnfig = Resources.GetBlueprint<BlueprintFeature>("4847d450fbef9b444abcc3a82337b426").GetComponent<AddFeatureOnClassLevel>();
            CharmDomainBaseFeatureComnfig.m_AdditionalClasses = CharmDomainBaseFeatureComnfig.m_AdditionalClasses.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            CharmDomainBaseFeatureComnfig.m_Archetypes = CharmDomainBaseFeatureComnfig.m_Archetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            var CharmDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("d49f0e3460fd52d4e9660a8ce52142a0");
            CharmDomainGreaterResource.m_MaxAmount.m_Class = CharmDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            CharmDomainGreaterResource.m_MaxAmount.m_Archetypes = CharmDomainGreaterResource.m_MaxAmount.m_Archetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            //Community Domain Hooks
            var CommunityDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("b1b8efd70ba5dd84aa6985d46dc299d5").GetComponent<ContextRankConfig>();
            CommunityDomainBaseAbilityConfig.m_AdditionalArchetypes = CommunityDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            CommunityDomainBaseAbilityConfig.m_Class = CommunityDomainBaseAbilityConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            var CommunityDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("102d61a114786894bb2b30568943ef1f").GetComponent<AddFeatureOnClassLevel>();
            CommunityDomainBaseFeatureConfig.m_AdditionalClasses = CommunityDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            CommunityDomainBaseFeatureConfig.m_Archetypes = CommunityDomainBaseFeatureConfig.m_Archetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            var CommunityDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("76291e62d2496ad41824044aba3077ea").GetComponent<ContextRankConfig>();
            CommunityDomainGreaterAbilityConfig.m_AdditionalArchetypes = CommunityDomainGreaterAbilityConfig.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            CommunityDomainGreaterAbilityConfig.m_Class = CommunityDomainGreaterAbilityConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            //Knowledge Domain Hooks
            var KnowledgeDomainBaseBuffConfig = Resources.GetBlueprint<BlueprintBuff>("35fa55fe2c60e4442b670a88a70c06c3").GetComponent<ContextRankConfig>();
            KnowledgeDomainBaseBuffConfig.m_AdditionalArchetypes = KnowledgeDomainBaseBuffConfig.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            KnowledgeDomainBaseBuffConfig.m_Class = KnowledgeDomainBaseBuffConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            var KnowledgeDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("02a79a205bce6f5419dcdf26b64f13c6").GetComponent<ContextRankConfig>();
            KnowledgeDomainBaseAbilityConfig.m_AdditionalArchetypes = KnowledgeDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            KnowledgeDomainBaseAbilityConfig.m_Class = KnowledgeDomainBaseAbilityConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            var KnowledgedDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("5335f015063776d429a0b5eab97eb060");
            KnowledgedDomainBaseFeatureConfig.GetComponents<AddFeatureOnClassLevel>().ForEach(c => {
                c.m_AdditionalClasses = c.m_AdditionalClasses.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            });
            KnowledgedDomainBaseFeatureConfig.GetComponents<AddFeatureOnClassLevel>().ForEach(c => {
                c.m_Archetypes = c.m_Archetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            });
            var KnowledgeDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("f88f616a4b6bd5f419025115c52cb329");
            KnowledgeDomainBaseResource.m_MaxAmount.m_Class = KnowledgeDomainBaseResource.m_MaxAmount.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            KnowledgeDomainBaseResource.m_MaxAmount.m_Archetypes = KnowledgeDomainBaseResource.m_MaxAmount.m_Archetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            var KnowledgeDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("ec582b195ccb2ef4ea8dcd96a5a6e009").GetComponent<ContextRankConfig>();
            KnowledgeDomainGreaterAbilityConfig.m_AdditionalArchetypes = KnowledgeDomainGreaterAbilityConfig.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            KnowledgeDomainGreaterAbilityConfig.m_Class = KnowledgeDomainGreaterAbilityConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            var KnowledgesDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("34f0a288ff5106645a88440b800686ca");
            KnowledgesDomainGreaterResource.m_MaxAmount.m_ClassDiv = KnowledgesDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            //Nobility Domain Hooks
            var NobilityDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("7a305ef528cb7884385867a2db410102").GetComponent<ContextRankConfig>();
            NobilityDomainBaseAbilityConfig.m_AdditionalArchetypes = NobilityDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            NobilityDomainBaseAbilityConfig.m_Class = NobilityDomainBaseAbilityConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            var NobilityDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("a1a7f3dd904ed8e45b074232f48190d1").GetComponent<AddFeatureOnClassLevel>();
            NobilityDomainBaseFeatureConfig.m_AdditionalClasses = NobilityDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            NobilityDomainBaseFeatureConfig.m_Archetypes = NobilityDomainBaseFeatureConfig.m_Archetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            var NobilityDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("3fc6e1f3acbcb0e4c83badf7709ce53d");
            NobilityDomainBaseResource.m_MaxAmount.m_Class = NobilityDomainBaseResource.m_MaxAmount.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            NobilityDomainBaseResource.m_MaxAmount.m_Archetypes = NobilityDomainBaseResource.m_MaxAmount.m_Archetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            var NobilityDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("2972215a5367ae44b8ddfe435a127a6e").GetComponent<ContextRankConfig>();
            NobilityDomainGreaterAbilityConfig.m_AdditionalArchetypes = NobilityDomainGreaterAbilityConfig.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            NobilityDomainGreaterAbilityConfig.m_Class = NobilityDomainGreaterAbilityConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            var NobilityDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("cb3efe82596c908418c0dba4ef6f4210");
            NobilityDomainGreaterResource.m_MaxAmount.m_ClassDiv = NobilityDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            //Protection Domain Hooks
            var ProtectionDomainBaseBuffConfig = Resources.GetBlueprint<BlueprintBuff>("2ddb4cfc3cfd04c46a66c6cd26df1c06").GetComponent<ContextRankConfig>();
            ProtectionDomainBaseBuffConfig.m_AdditionalArchetypes = ProtectionDomainBaseBuffConfig.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            ProtectionDomainBaseBuffConfig.m_Class = ProtectionDomainBaseBuffConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            var ProtectionDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("c5815bd0bf87bdb4fa9c440c8088149b").GetComponent<ContextRankConfig>();
            ProtectionDomainBaseAbilityConfig.m_AdditionalArchetypes = ProtectionDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            ProtectionDomainBaseAbilityConfig.m_Class = ProtectionDomainBaseAbilityConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            var ProtectionDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("a05a8959c594daa40a1c5add79566566").GetComponent<AddFeatureOnClassLevel>();
            ProtectionDomainBaseFeatureConfig.m_AdditionalClasses = ProtectionDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            ProtectionDomainBaseFeatureConfig.m_Archetypes = ProtectionDomainBaseFeatureConfig.m_Archetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            var ProtectionDomainBaseSelfBuffConfig = Resources.GetBlueprint<BlueprintBuff>("74a4fb45f23705d4db2784d16eb93138").GetComponent<ContextRankConfig>();
            ProtectionDomainBaseSelfBuffConfig.m_AdditionalArchetypes = ProtectionDomainBaseSelfBuffConfig.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            ProtectionDomainBaseSelfBuffConfig.m_Class = ProtectionDomainBaseSelfBuffConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            var ProtectionDomainGreaterEffect = Resources.GetBlueprint<BlueprintBuff>("fea7c44605c90f14fa40b2f2f5ae6339");
            ProtectionDomainGreaterEffect.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_AdditionalArchetypes = c.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            });
            ProtectionDomainGreaterEffect.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            });
            var ProtectionDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("f3d878f77d0ee854b864f5ea1c80e752");
            ProtectionDomainGreaterResource.m_MaxAmount.m_Class = ProtectionDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            ProtectionDomainGreaterResource.m_MaxAmount.m_Archetypes = ProtectionDomainGreaterResource.m_MaxAmount.m_Archetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            //Repose Domain Hooks
            var ReposeDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("8526bc808c303034cb2b7832bccf1482").GetComponent<AddFeatureOnClassLevel>();
            ReposeDomainBaseFeatureConfig.m_AdditionalClasses = ReposeDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            ReposeDomainBaseFeatureConfig.m_Archetypes = ReposeDomainBaseFeatureConfig.m_Archetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            var ReposeDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("aefe627a3a2f8d94ea9d2b3961261282");
            ReposeDomainGreaterResource.m_MaxAmount.m_Class = ReposeDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            ReposeDomainGreaterResource.m_MaxAmount.m_Archetypes = ReposeDomainGreaterResource.m_MaxAmount.m_Archetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            //Rune Domain Hooks
            var RuneDomainGreaterAreaConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("e26de8b0164db23458eb64c21fac2846").GetComponent<ContextRankConfig>();
            RuneDomainGreaterAreaConfig.m_AdditionalArchetypes = RuneDomainGreaterAreaConfig.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            RuneDomainGreaterAreaConfig.m_Class = RuneDomainGreaterAreaConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            var RuneDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("b74c64a0152c7ee46b13ecdd72dda6f3").GetComponent<AddFeatureOnClassLevel>();
            RuneDomainBaseFeatureConfig.m_AdditionalClasses = RuneDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            RuneDomainBaseFeatureConfig.m_Archetypes = RuneDomainBaseFeatureConfig.m_Archetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            var RuneDomainGreaterAbility = Resources.GetBlueprint<BlueprintAbility>("9171a3ce8ea8cac44894b240709804ce");
            RuneDomainGreaterAbility.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_AdditionalArchetypes = c.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            });
            RuneDomainGreaterAbility.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            });
            var RuneDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("cb3efe82596c908418c0dba4ef6f4210");
            RuneDomainGreaterResource.m_MaxAmount.m_ClassDiv = RuneDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            var RuneDomainBaseAbilityFire = Resources.GetBlueprint<BlueprintAbility>("eddfe26a8a3892b47add3cb08db7069d");
            RuneDomainBaseAbilityFire.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_AdditionalArchetypes = c.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            });
            RuneDomainBaseAbilityFire.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            });
            var RuneDomainBaseAbilityCold = Resources.GetBlueprint<BlueprintAbility>("2b81ff42fcbe9434eaf00fb0a873f579");
            RuneDomainBaseAbilityCold.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_AdditionalArchetypes = c.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            });
            RuneDomainBaseAbilityCold.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            });
            var RuneDomainBaseAbilityAcid = Resources.GetBlueprint<BlueprintAbility>("92c821ecc8d73564bad15a8a07ed40f2");
            RuneDomainBaseAbilityAcid.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_AdditionalArchetypes = c.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            });
            RuneDomainBaseAbilityAcid.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            });
            var RuneDomainBaseAbilityFireAreaConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("9b786945d2ec1884184235a488e5cb9e").GetComponent<ContextRankConfig>();
            RuneDomainBaseAbilityFireAreaConfig.m_AdditionalArchetypes = RuneDomainBaseAbilityFireAreaConfig.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            RuneDomainBaseAbilityFireAreaConfig.m_Class = RuneDomainBaseAbilityFireAreaConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            var RuneDomainBaseAbilityColdAreaConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("8b8e98e8e0000f643ad97c744f3f850b").GetComponent<ContextRankConfig>();
            RuneDomainBaseAbilityColdAreaConfig.m_AdditionalArchetypes = RuneDomainBaseAbilityColdAreaConfig.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            RuneDomainBaseAbilityColdAreaConfig.m_Class = RuneDomainBaseAbilityColdAreaConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            var RuneDomainBaseAbilityAcidAreaConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("98c3a36f2a3636c49a3f77c001a25f29").GetComponent<ContextRankConfig>();
            RuneDomainBaseAbilityAcidAreaConfig.m_AdditionalArchetypes = RuneDomainBaseAbilityAcidAreaConfig.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            RuneDomainBaseAbilityAcidAreaConfig.m_Class = RuneDomainBaseAbilityAcidAreaConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            var RuneDomainBaseAbilityElectricity = Resources.GetBlueprint<BlueprintAbility>("b67978e3d5a6c9247a393237bc660339");
            RuneDomainBaseAbilityElectricity.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_AdditionalArchetypes = c.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            });
            RuneDomainBaseAbilityElectricity.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());
            });
            var RuneDomainBaseAbilityElectricityAreaConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("db868c576c69d0e4a8462645267c6cdc").GetComponent<ContextRankConfig>();
            RuneDomainBaseAbilityElectricityAreaConfig.m_AdditionalArchetypes = RuneDomainBaseAbilityElectricityAreaConfig.m_AdditionalArchetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());
            RuneDomainBaseAbilityElectricityAreaConfig.m_Class = RuneDomainBaseAbilityElectricityAreaConfig.m_Class.AppendToArray(DruidClass.ToReference<BlueprintCharacterClassReference>());

            UrbanDruidArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, DruidBondSelection, DruidSpontaneousSummonFeature),
                    Helpers.LevelEntry(2, WoodlandStrideFeature),
                    Helpers.LevelEntry(4, ResistNaturesLureFeature, WildShapeIWolfFeature),
                    Helpers.LevelEntry(6, WildShapeIILeopardFeature, WildShapeElementalSmallFeature),
                    Helpers.LevelEntry(8, WildShapeIVBearFeature, WildShapeElementalFeatureAddMediumFeature),
                    Helpers.LevelEntry(9, VenomImmunityFeature),
                    Helpers.LevelEntry(10, WildShapeIIISmilodonFeature, WildShapeElementalFeatureAddLargeFeature, WildShapeIVShamblingMoundFeature),
                    Helpers.LevelEntry(12, WildShapeElementalHugeFeatureFeature)
            };
            UrbanDruidArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, UrbanDruidBondSelection, SpontaneousDomainCastingUrbanDruid, UrbanDruidWildShape),
                    Helpers.LevelEntry(2, UrbanDruidLorekeeperFeature),
                    Helpers.LevelEntry(4, UrbanDruidResistTemptationFeature),
                    Helpers.LevelEntry(8, WildShapeIWolfFeature),
                    Helpers.LevelEntry(9, UrbanDruidMentalStrengthFeature),
                    Helpers.LevelEntry(10, WildShapeIILeopardFeature, WildShapeElementalSmallFeature),
                    Helpers.LevelEntry(12, WildShapeIVBearFeature, WildShapeElementalFeatureAddMediumFeature),
                    Helpers.LevelEntry(14, WildShapeIIISmilodonFeature, WildShapeElementalFeatureAddLargeFeature, WildShapeIVShamblingMoundFeature),
                    Helpers.LevelEntry(16, WildShapeElementalHugeFeatureFeature)
            };
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Urban Druid")) { return; }
            DruidClass.m_Archetypes = DruidClass.m_Archetypes.AppendToArray(UrbanDruidArchetype.ToReference<BlueprintArchetypeReference>());

        }
    }
}
