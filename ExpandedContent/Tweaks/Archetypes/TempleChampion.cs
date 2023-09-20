using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Utility;
using Kingmaker.Designers.Mechanics.Buffs;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class TempleChampion {

        
        public static void AddTempleChampion() {

            //All the var we need to save space

            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var PaladinDivineBondSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("ad7dc4eba7bf92f4aba23f716d7a9ba6");
            var DomainsSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("48525e5da45c9c243a343fc6545dbdb9");
            var SecondDomainsSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("43281c3d7fe18cc4d91928395837cd1e");
            var BlessingSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("6d9dcc2a59210a14891aeedb09d406aa");
            var SecondBlessingSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("b7ce4a67287cda746a59b31c042305cf");
            var ArmoredHulkArmoredSwiftness = Resources.GetBlueprint<BlueprintFeature>("f95f4f3a10917114c82bcbebc4d0fd36");
            var ArmoredHulkImprovedArmoredSwiftness = Resources.GetBlueprint<BlueprintFeature>("0db9a2b013e8fc6409611c2a16ae56bc");
            //Domains
            var AirDomainProgression = Resources.GetBlueprint<BlueprintProgression>("750bfcd133cd52f42acbd4f7bc9cc365");
            var AirDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("d7169e8978d9e9d418398eab946c49e5");
            var AnimalDomainProgression = Resources.GetBlueprint<BlueprintProgression>("23d2f87aa54c89f418e68e790dba11e0");
            var AnimalDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("f13eb6be93dd5234c8126e5384040009");
            var ArtificeDomainProgression = Resources.GetBlueprint<BlueprintProgression>("6454b37f50e10ae41bca83aaaa81ffc2");
            var ArtificeDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("988f3e10cc2fd8f48915acdc640a65b3");
            var ChaosDomainProgression = Resources.GetBlueprint<BlueprintProgression>("5a5d19c246961484a97e1e5dded98ab2");
            var ChaosDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("85e8db7e938d4f947a084a21d3535adf");
            var CharmDomainProgression = Resources.GetBlueprint<BlueprintProgression>("b5c056787d1bf544588ec3a150ed0b3b");
            var CharmDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("242eba70a5e2317479de39de3c1e64ad");
            var CommunityDomainProgression = Resources.GetBlueprint<BlueprintProgression>("b8bbe42616d61ac419971b7910d79fc8");
            var CommunityDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("3a397e27682edfd409cb73ff12de7c51");
            var DarknessDomainProgression = Resources.GetBlueprint<BlueprintProgression>("1e1b4128290b11a41ba55280ede90d7d");
            var DarknessDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("bfdc224a1362f2b4688b57f70adcc26f");
            var DeathDomainProgression = Resources.GetBlueprint<BlueprintProgression>("710d8c959e7036448b473ffa613cdeba");
            var DeathDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("023794a8386506c49aad142846700594");
            var DestructionDomainProgression = Resources.GetBlueprint<BlueprintProgression>("269ff0bf4596f5248864bc2653a2f0e0");
            var DestructionDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("8edced7121849414f8b1dc77a119b4a2");
            var EarthDomainProgression = Resources.GetBlueprint<BlueprintProgression>("08bbcbfe5eb84604481f384c517ac800");
            var EarthDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("4132a011b835a36479d6bc19a1b962e6");
            var EvilDomainProgression = Resources.GetBlueprint<BlueprintProgression>("a8936d29b6051a1418682da1878b644e");
            var EvilDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("82b654d68ea6ce143be5f7df646d6385");
            var FireDomainProgression = Resources.GetBlueprint<BlueprintProgression>("881b2137a1779294c8956fe5b497cc35");
            var FireDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("2bd6aa3c4979fd045bbbda8da586d3fb");
            var GloryDomainProgression = Resources.GetBlueprint<BlueprintProgression>("f0a61a043bcdf0f4c8efc59962afafb8");
            var GloryDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("434531fa0827f4c4a97482ffc71e7234");
            var GoodDomainProgression = Resources.GetBlueprint<BlueprintProgression>("243ab3e7a86d30243bdfe79c83e6adb4");
            var GoodDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("efc4219c7894afc438180737adc0b7ac");
            var HealingDomainProgression = Resources.GetBlueprint<BlueprintProgression>("b0a26ee984b6b6945b884467aa2f1baa");
            var HealingDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("599fb0d60358c354d8c5c4304a73e19a");
            var KnowledgeDomainProgression = Resources.GetBlueprint<BlueprintProgression>("28edbdbefca579b4ab4992e98af71981");
            var KnowledgeDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("72c0d2f6379114947a6072278baedb90");
            var LawDomainProgression = Resources.GetBlueprint<BlueprintProgression>("a723d11a5ae5df0488775e31fac9117d");
            var LawDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("0d9749df9d68ded438ecdf8527085963");
            var LiberationDomainProgression = Resources.GetBlueprint<BlueprintProgression>("df2f14ced8710664ba7db914880c4a02");
            var LiberationDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("34b0e4bb90e3a4f4183b095f0d44ca5d");
            var LuckDomainProgression = Resources.GetBlueprint<BlueprintProgression>("8bd8cfad69085654b9118534e4aa215e");
            var LuckDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("1ba7fc652568a524db218ccff2f9ed90");
            var MadnessDomainProgression = Resources.GetBlueprint<BlueprintProgression>("9ebe166b9b901c746b1858029f13a2c5");
            var MadnessDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("ae9c936c86d248848b2fb90b32b3b41d");
            var MagicDomainProgression = Resources.GetBlueprint<BlueprintProgression>("8f90e7129b0f3b742921c2c9c9bd64fc");
            var MagicDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("9c5053b1ad83a9742839b3ab824abbd2");
            var NobilityDomainProgression = Resources.GetBlueprint<BlueprintProgression>("8480f2d1ca764774895ee6fd610a568e");
            var NobilityDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("6bddbc86f5ee80146ac0fc51923ef4fd");
            var PlantDomainProgression = Resources.GetBlueprint<BlueprintProgression>("467d2a1d2107da64395b591393baad17");
            var PlantDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("22f3a592849c06e4c8869e2132e11597");
            var ProtectionDomainProgression = Resources.GetBlueprint<BlueprintProgression>("b750650400d9d554b880dbf4c8347b24");
            var ProtectionDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("6be37e1316160d949aa2e5a0be99404a");
            var ReposeDomainProgression = Resources.GetBlueprint<BlueprintProgression>("a2ab5a696d0dd134d94b2631151a15ee");
            var ReposeDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("a12fa2f458841ca4bb4ef666e1fbceef");
            var RuneDomainProgression = Resources.GetBlueprint<BlueprintProgression>("6d4dac497c182754d8b1f49071cca3fd");
            var RuneDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("8d176f8fe5616a64ca37835be7c2ccfe");
            var StrenthDomainProgression = Resources.GetBlueprint<BlueprintProgression>("07854f99c8d029b4cbfdf6ae6c7bc452");
            var StrenthDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("2ed973db1af2c8e428ce404fb1e9a20d");
            var SunDomainProgression = Resources.GetBlueprint<BlueprintProgression>("c85c8791ee13d4c4ea10d93c97a19afc");
            var SunDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("0877f33e01ba9884daed94cc7633e09c");
            var TravelDomainProgression = Resources.GetBlueprint<BlueprintProgression>("d169dd2de3630b749a2363c028bb6e7b");
            var TravelDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("9dc676c4ab1d0c643bb2293696375fcf");
            var TrickeryDomainProgression = Resources.GetBlueprint<BlueprintProgression>("cc2d330bb0200e840aeb79140e770198");
            var TrickeryDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("720cb8ed96386d24d8dfe38cad153cbd");
            var WarDomainProgression = Resources.GetBlueprint<BlueprintProgression>("8d454cbb7f25070419a1c8eaf89b5be5");
            var WarDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("2d2b61d86f93ce84f8375f9e59489072");
            var WaterDomainProgression = Resources.GetBlueprint<BlueprintProgression>("e63d9133cebf2cf4788e61432a939084");
            var WaterDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("f05fb4f417465f94fb1b4d6c48ea42cf");
            var WeatherDomainProgression = Resources.GetBlueprint<BlueprintProgression>("c18a821ee662db0439fb873165da25be");
            var WeatherDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("d124d29c7c96fc345943dd17e24990e8");


            var ScriptureIcon = AssetLoader.LoadInternal("Skills", "Icon_Scripture.png");
            var DomainChampionFeature = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DomainChampionFeature", bp => {
                bp.SetName("Domain Champion");
                bp.SetDescription("A Temple Champion does not have the ability to cast spells, instead being granted access to both the Clerical Domains and the Warpriest Blessings " +
                    "of her deitiy to defend her faith and it's faithful. At level 1 she must select both two Domains and a Blessing, gaining access to another " +
                    "Blessing at level 7.");
                bp.m_Icon = ScriptureIcon;
                bp.Group = FeatureGroup.Domain;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
                bp.AddFeatures(AirDomainProgression, AnimalDomainProgression, ArtificeDomainProgression, CharmDomainProgression, CommunityDomainProgression, 
                    DarknessDomainProgression, DeathDomainProgression, DestructionDomainProgression, EarthDomainProgression, FireDomainProgression, 
                    GloryDomainProgression, GoodDomainProgression, HealingDomainProgression, KnowledgeDomainProgression, LawDomainProgression, 
                    LiberationDomainProgression, LuckDomainProgression, MadnessDomainProgression, MagicDomainProgression, NobilityDomainProgression, 
                    PlantDomainProgression, ProtectionDomainProgression, ReposeDomainProgression, RuneDomainProgression, StrenthDomainProgression, 
                    SunDomainProgression, TravelDomainProgression, TrickeryDomainProgression, WarDomainProgression, WaterDomainProgression, WeatherDomainProgression);
                bp.AddComponent<AddFacts>(c => {
                    // To support all features that check for domains this way
                    c.m_Facts = new BlueprintUnitFactReference[] { DomainsSelection.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var DomainChampionFeatureSecondary = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DomainChampionFeatureSecondary", bp => {
                bp.SetName("Champions Second Domain");
                bp.SetDescription("The second Domain granted by Domain Champion.");
                bp.Group = FeatureGroup.ClericSecondaryDomain;
                bp.Groups = new FeatureGroup[0];
                bp.IsClassFeature = true;
                bp.AddFeatures(AirDomainProgressionSecondary, AnimalDomainProgressionSecondary, ArtificeDomainProgressionSecondary, CharmDomainProgressionSecondary, CommunityDomainProgressionSecondary,
                    DarknessDomainProgressionSecondary, DeathDomainProgressionSecondary, DestructionDomainProgressionSecondary, EarthDomainProgressionSecondary, FireDomainProgressionSecondary,
                    GloryDomainProgressionSecondary, GoodDomainProgressionSecondary, HealingDomainProgressionSecondary, KnowledgeDomainProgressionSecondary, LawDomainProgressionSecondary,
                    LiberationDomainProgressionSecondary, LuckDomainProgressionSecondary, MadnessDomainProgressionSecondary, MagicDomainProgressionSecondary, NobilityDomainProgressionSecondary,
                    PlantDomainProgressionSecondary, ProtectionDomainProgressionSecondary, ReposeDomainProgressionSecondary, RuneDomainProgressionSecondary, StrenthDomainProgressionSecondary,
                    SunDomainProgressionSecondary, TravelDomainProgressionSecondary, TrickeryDomainProgressionSecondary, WarDomainProgressionSecondary, WaterDomainProgressionSecondary, WeatherDomainProgressionSecondary);
                bp.AddComponent<AddFacts>(c => {
                    // To support all features that check for domains this way
                    c.m_Facts = new BlueprintUnitFactReference[] { SecondDomainsSelection.ToReference<BlueprintUnitFactReference>() };

                });

            });

            var WisdomOfTheTempleFeature = Helpers.CreateBlueprint<BlueprintFeature>("WisdomOfTheTempleFeature", bp => {
                bp.SetName("Wisdom of the Temple");
                bp.SetDescription("The deeds done in the name of your Deity have been noted, at 15th level the Temple Champion gains the hidden wisdom of her faith, gaining a +4 to " +
                    "her Wisdom Score.");
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.Wisdom;
                    c.Value = 4;
                });
            });

            var TempleChampionArmoredSwiftnessFeature = Helpers.CreateBlueprint<BlueprintFeature>("TempleChampionArmoredSwiftnessFeature", bp => {
                bp.SetName("Temple Armor");
                bp.SetDescription("Constant wearing of the Temples armor has reduced its effect on movement, the Temple Champion gains Armored Swiftness at level 4 and Improved " +
                    "Armored Swiftness at level 12.");
                bp.m_DescriptionShort = Helpers.CreateString($"CastigatorOpportunist.Description", "Constant wearing of the Temples armor has reduced its effect on movement, the Temple Champion " +
                    "gains Armored Swiftness at level 4 and Improved Armored Swiftness at level 12.");
                bp.m_Icon = ArmoredHulkArmoredSwiftness.Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ArmoredHulkArmoredSwiftness.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var TempleChampionIprovedArmoredSwiftnessFeature = Helpers.CreateBlueprint<BlueprintFeature>("TempleChampionIprovedArmoredSwiftnessFeature", bp => {
                bp.SetName("Temple Armor");
                bp.SetDescription("Constant wearing of the Temples armor has reduced its effect on movement, the Temple Champion gains Armored Swiftness at level 4 and Improved " +
                    "Armored Swiftness at level 12.");
                bp.m_DescriptionShort = Helpers.CreateString($"CastigatorOpportunist.Description", "Constant wearing of the Temples armor has reduced its effect on movement, the Temple Champion " +
                    "gains Armored Swiftness at level 4 and Improved Armored Swiftness at level 12.");
                bp.m_Icon = ArmoredHulkImprovedArmoredSwiftness.Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { ArmoredHulkImprovedArmoredSwiftness.ToReference<BlueprintUnitFactReference>() };
                });
            });



            var TempleChampionArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("TempleChampionArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"TempleChampionArchetype.Name", "Temple Champion");
                bp.LocalizedDescription = Helpers.CreateString($"TempleChampionArchetype.Description", "A temple champion is a powerful warrior dedicated to a " +
                    "good or lawful deity. She thinks of herself primarily as a servant of her deity and secondarily as an agent of her deity’s church. She has " +
                    "a refined understanding of a specific aspect of that faith and gives up standard paladin spellcasting in favor of a warpriest’s domain-based " +
                    "blessings and granted powers.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"TempleChampionArchetype.Description", "A temple champion is a powerful warrior dedicated to a " +
                    "good or lawful deity. She thinks of herself primarily as a servant of her deity and secondarily as an agent of her deity’s church. She has " +
                    "a refined understanding of a specific aspect of that faith and gives up standard paladin spellcasting in favor of a warpriest’s domain-based " +
                    "blessings and granted powers.");
                bp.RemoveSpellbook = true;
                bp.OverrideAttributeRecommendations = true;
                bp.RecommendedAttributes = bp.RecommendedAttributes.AppendToArray(StatType.Strength, StatType.Constitution, StatType.Wisdom, StatType.Charisma);
                bp.NotRecommendedAttributes = bp.NotRecommendedAttributes.AppendToArray(StatType.Dexterity, StatType.Intelligence);
                bp.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(5, PaladinDivineBondSelection)
                };
                bp.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, DomainChampionFeature, DomainChampionFeatureSecondary, BlessingSelection),
                    Helpers.LevelEntry(4, TempleChampionArmoredSwiftnessFeature),
                    Helpers.LevelEntry(7, SecondBlessingSelection),
                    Helpers.LevelEntry(12, TempleChampionIprovedArmoredSwiftnessFeature),
                    Helpers.LevelEntry(15, WisdomOfTheTempleFeature)
                };
            });
            #region Domain
            //Allowing Progression on normal Domains

            AirDomainProgression.m_Classes = AirDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            AirDomainProgression.m_Archetypes = AirDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            AnimalDomainProgression.m_Classes = AnimalDomainProgression.m_Classes.AppendToArray(
                 new BlueprintProgression.ClassWithLevel() {
                     m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                     AdditionalLevel = 0
                 });
            AnimalDomainProgression.m_Archetypes = AnimalDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            ArtificeDomainProgression.m_Classes = ArtificeDomainProgression.m_Classes.AppendToArray(
                 new BlueprintProgression.ClassWithLevel() {
                     m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                     AdditionalLevel = 0
                 });
            ArtificeDomainProgression.m_Archetypes = ArtificeDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            CharmDomainProgression.m_Classes = CharmDomainProgression.m_Classes.AppendToArray(
                 new BlueprintProgression.ClassWithLevel() {
                     m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                     AdditionalLevel = 0
                 });
            CharmDomainProgression.m_Archetypes = CharmDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            CommunityDomainProgression.m_Classes = CommunityDomainProgression.m_Classes.AppendToArray(
                 new BlueprintProgression.ClassWithLevel() {
                     m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                     AdditionalLevel = 0
                 });
            CommunityDomainProgression.m_Archetypes = CommunityDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            DarknessDomainProgression.m_Classes = DarknessDomainProgression.m_Classes.AppendToArray(
                 new BlueprintProgression.ClassWithLevel() {
                     m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                     AdditionalLevel = 0
                 });
            DarknessDomainProgression.m_Archetypes = DarknessDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            DeathDomainProgression.m_Classes = DeathDomainProgression.m_Classes.AppendToArray(
                 new BlueprintProgression.ClassWithLevel() {
                     m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                     AdditionalLevel = 0
                 });
            DeathDomainProgression.m_Archetypes = DeathDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            DestructionDomainProgression.m_Classes = DestructionDomainProgression.m_Classes.AppendToArray(
                 new BlueprintProgression.ClassWithLevel() {
                     m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                     AdditionalLevel = 0
                 });
            DestructionDomainProgression.m_Archetypes = DestructionDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            EarthDomainProgression.m_Classes = EarthDomainProgression.m_Classes.AppendToArray(
                 new BlueprintProgression.ClassWithLevel() {
                     m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                     AdditionalLevel = 0
                 });
            EarthDomainProgression.m_Archetypes = EarthDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            FireDomainProgression.m_Classes = FireDomainProgression.m_Classes.AppendToArray(
                 new BlueprintProgression.ClassWithLevel() {
                     m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                     AdditionalLevel = 0
                 });
            FireDomainProgression.m_Archetypes = FireDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            GloryDomainProgression.m_Classes = GloryDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel()
                {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            GloryDomainProgression.m_Archetypes = GloryDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel()
                {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            GoodDomainProgression.m_Classes = GoodDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            GoodDomainProgression.m_Archetypes = GoodDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            HealingDomainProgression.m_Classes = HealingDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            HealingDomainProgression.m_Archetypes = HealingDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            KnowledgeDomainProgression.m_Classes = KnowledgeDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            KnowledgeDomainProgression.m_Archetypes = KnowledgeDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            LawDomainProgression.m_Classes = LawDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            LawDomainProgression.m_Archetypes = LawDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            LiberationDomainProgression.m_Classes = LiberationDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            LiberationDomainProgression.m_Archetypes = LiberationDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            LuckDomainProgression.m_Classes = LuckDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            LuckDomainProgression.m_Archetypes = LuckDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            MadnessDomainProgression.m_Classes = MadnessDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            MadnessDomainProgression.m_Archetypes = MadnessDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            MagicDomainProgression.m_Classes = MagicDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            MagicDomainProgression.m_Archetypes = MagicDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            NobilityDomainProgression.m_Classes = NobilityDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            NobilityDomainProgression.m_Archetypes = NobilityDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            PlantDomainProgression.m_Classes = PlantDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            PlantDomainProgression.m_Archetypes = PlantDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            ProtectionDomainProgression.m_Classes = ProtectionDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            ProtectionDomainProgression.m_Archetypes = ProtectionDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            ReposeDomainProgression.m_Classes = ReposeDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            ReposeDomainProgression.m_Archetypes = ReposeDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            RuneDomainProgression.m_Classes = RuneDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            RuneDomainProgression.m_Archetypes = RuneDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            StrenthDomainProgression.m_Classes = StrenthDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            StrenthDomainProgression.m_Archetypes = StrenthDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            SunDomainProgression.m_Classes = SunDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            SunDomainProgression.m_Archetypes = SunDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            TravelDomainProgression.m_Classes = TravelDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            TravelDomainProgression.m_Archetypes = TravelDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            TrickeryDomainProgression.m_Classes = TrickeryDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            TrickeryDomainProgression.m_Archetypes = TrickeryDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            WarDomainProgression.m_Classes = WarDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            WarDomainProgression.m_Archetypes = WarDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            WaterDomainProgression.m_Classes = WaterDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            WaterDomainProgression.m_Archetypes = WaterDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            WeatherDomainProgression.m_Classes = WeatherDomainProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            WeatherDomainProgression.m_Archetypes = WeatherDomainProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });

            //Allowing Prgression on Secondary Domains
            AirDomainProgressionSecondary.m_Classes = AirDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            AirDomainProgressionSecondary.m_Archetypes = AirDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            AnimalDomainProgressionSecondary.m_Classes = AnimalDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            AnimalDomainProgressionSecondary.m_Archetypes = AnimalDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            ArtificeDomainProgressionSecondary.m_Classes = ArtificeDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            ArtificeDomainProgressionSecondary.m_Archetypes = ArtificeDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            CharmDomainProgressionSecondary.m_Classes = CharmDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            CharmDomainProgressionSecondary.m_Archetypes = CharmDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            CommunityDomainProgressionSecondary.m_Classes = CommunityDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            CommunityDomainProgressionSecondary.m_Archetypes = CommunityDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            DarknessDomainProgressionSecondary.m_Classes = DarknessDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            DarknessDomainProgressionSecondary.m_Archetypes = DarknessDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            DeathDomainProgressionSecondary.m_Classes = DeathDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            DeathDomainProgressionSecondary.m_Archetypes = DeathDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            DestructionDomainProgressionSecondary.m_Classes = DestructionDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            DestructionDomainProgressionSecondary.m_Archetypes = DestructionDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            EarthDomainProgressionSecondary.m_Classes = EarthDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            EarthDomainProgressionSecondary.m_Archetypes = EarthDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            FireDomainProgressionSecondary.m_Classes = FireDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            FireDomainProgressionSecondary.m_Archetypes = FireDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            GloryDomainProgressionSecondary.m_Classes = GloryDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            GloryDomainProgressionSecondary.m_Archetypes = GloryDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            GoodDomainProgressionSecondary.m_Classes = GoodDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            GoodDomainProgressionSecondary.m_Archetypes = GoodDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            HealingDomainProgressionSecondary.m_Classes = HealingDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            HealingDomainProgressionSecondary.m_Archetypes = HealingDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            KnowledgeDomainProgressionSecondary.m_Classes = KnowledgeDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            KnowledgeDomainProgressionSecondary.m_Archetypes = KnowledgeDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            LawDomainProgressionSecondary.m_Classes = LawDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            LawDomainProgressionSecondary.m_Archetypes = LawDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            LiberationDomainProgressionSecondary.m_Classes = LiberationDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            LiberationDomainProgressionSecondary.m_Archetypes = LiberationDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            LuckDomainProgressionSecondary.m_Classes = LuckDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            LuckDomainProgressionSecondary.m_Archetypes = LuckDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            MadnessDomainProgressionSecondary.m_Classes = MadnessDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            MadnessDomainProgressionSecondary.m_Archetypes = MadnessDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            MagicDomainProgressionSecondary.m_Classes = MagicDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            MagicDomainProgressionSecondary.m_Archetypes = MagicDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            NobilityDomainProgressionSecondary.m_Classes = NobilityDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            NobilityDomainProgressionSecondary.m_Archetypes = NobilityDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            PlantDomainProgressionSecondary.m_Classes = PlantDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            PlantDomainProgressionSecondary.m_Archetypes = PlantDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            ProtectionDomainProgressionSecondary.m_Classes = ProtectionDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            ProtectionDomainProgressionSecondary.m_Archetypes = ProtectionDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            ReposeDomainProgressionSecondary.m_Classes = ReposeDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            ReposeDomainProgressionSecondary.m_Archetypes = ReposeDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            RuneDomainProgressionSecondary.m_Classes = RuneDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            RuneDomainProgressionSecondary.m_Archetypes = RuneDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            StrenthDomainProgressionSecondary.m_Classes = StrenthDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            StrenthDomainProgressionSecondary.m_Archetypes = StrenthDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            SunDomainProgressionSecondary.m_Classes = SunDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            SunDomainProgressionSecondary.m_Archetypes = SunDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            TravelDomainProgressionSecondary.m_Classes = TravelDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            TravelDomainProgressionSecondary.m_Archetypes = TravelDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            TrickeryDomainProgressionSecondary.m_Classes = TrickeryDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            TrickeryDomainProgressionSecondary.m_Archetypes = TrickeryDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            WarDomainProgressionSecondary.m_Classes = WarDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            WarDomainProgressionSecondary.m_Archetypes = WarDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            WaterDomainProgressionSecondary.m_Classes = WaterDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            WaterDomainProgressionSecondary.m_Archetypes = WaterDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            WeatherDomainProgressionSecondary.m_Classes = WeatherDomainProgressionSecondary.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = 0
                });
            WeatherDomainProgressionSecondary.m_Archetypes = WeatherDomainProgressionSecondary.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = 0
                });
            //Air Domain Hooks
            var AirDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("39b0c7db785560041b436b558c9df2bb").GetComponent<AddFeatureOnClassLevel>();
            AirDomainBaseFeatureConfig.m_AdditionalClasses = AirDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            AirDomainBaseFeatureConfig.m_Archetypes = AirDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var AirDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("b3494639791901e4db3eda6117ad878f").GetComponent<ContextRankConfig>();
            AirDomainBaseAbilityConfig.m_AdditionalArchetypes = AirDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            AirDomainBaseAbilityConfig.m_Class = AirDomainBaseAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Animal Domain Hooks
            var AnimalDomainBaseFeatureComp0 = Resources.GetBlueprint<BlueprintFeature>("d577aba79b5727a4ab74627c4c6ba23c").GetComponent<AddFeatureOnClassLevel>();
            AnimalDomainBaseFeatureComp0.m_AdditionalClasses = AnimalDomainBaseFeatureComp0.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            AnimalDomainBaseFeatureComp0.m_Archetypes = AnimalDomainBaseFeatureComp0.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var AnimalDomainBaseFeatureComp2 = Resources.GetBlueprint<BlueprintFeature>("d577aba79b5727a4ab74627c4c6ba23c").GetComponent<ContextRankConfig>();
            AnimalDomainBaseFeatureComp2.m_AdditionalArchetypes = AnimalDomainBaseFeatureComp2.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            AnimalDomainBaseFeatureComp2.m_Class = AnimalDomainBaseFeatureComp2.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var DomainAnimalCompanionProgression = Resources.GetBlueprint<BlueprintProgression>("125af359f8bc9a145968b5d8fd8159b8");
            DomainAnimalCompanionProgression.m_Classes = DomainAnimalCompanionProgression.m_Classes.AppendToArray(
                new BlueprintProgression.ClassWithLevel() {
                    m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    AdditionalLevel = -3
                });
            DomainAnimalCompanionProgression.m_Archetypes = DomainAnimalCompanionProgression.m_Archetypes.AppendToArray(
                new BlueprintProgression.ArchetypeWithLevel() {
                    m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    AdditionalLevel = -3
                });
            //Artifice Domain Hooks
            var ArtificeDomainBaseAuraConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("f042f2d62e6785d4e8612a027de1f298").GetComponent<ContextRankConfig>();
            ArtificeDomainBaseAuraConfig.m_AdditionalArchetypes = ArtificeDomainBaseAuraConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            ArtificeDomainBaseAuraConfig.m_Class = ArtificeDomainBaseAuraConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var ArtificeDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("01025d876ac28d349ac42d69ba462059").GetComponent<AddFeatureOnClassLevel>();
            ArtificeDomainBaseFeatureConfig.m_AdditionalClasses = ArtificeDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            ArtificeDomainBaseFeatureConfig.m_Archetypes = ArtificeDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var ArtificeDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("d2c3c7c7efbc71c438dc4e0c3f216407");
            ArtificeDomainBaseResource.m_MaxAmount.m_Class = ArtificeDomainBaseResource.m_MaxAmount.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            ArtificeDomainBaseResource.m_MaxAmount.m_Archetypes = ArtificeDomainBaseResource.m_MaxAmount.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var ArtificeDomainGreaterEffectConfig = Resources.GetBlueprint<BlueprintBuff>("9d4a139cb5605fa409b1be3ad6e87ba9").GetComponent<ContextRankConfig>();
            ArtificeDomainGreaterEffectConfig.m_AdditionalArchetypes = ArtificeDomainGreaterEffectConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            ArtificeDomainGreaterEffectConfig.m_Class = ArtificeDomainGreaterEffectConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Charm Domain Hooks
            var CharmDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("84cd24a110af59140b066bc2c69619bd").GetComponent<ContextRankConfig>();
            CharmDomainBaseAbilityConfig.m_AdditionalArchetypes = CharmDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            CharmDomainBaseAbilityConfig.m_Class = CharmDomainBaseAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var CharmDomainBaseFeatureComnfig = Resources.GetBlueprint<BlueprintFeature>("4847d450fbef9b444abcc3a82337b426").GetComponent<AddFeatureOnClassLevel>();
            CharmDomainBaseFeatureComnfig.m_AdditionalClasses = CharmDomainBaseFeatureComnfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            CharmDomainBaseFeatureComnfig.m_Archetypes = CharmDomainBaseFeatureComnfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var CharmDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("d49f0e3460fd52d4e9660a8ce52142a0");
            CharmDomainGreaterResource.m_MaxAmount.m_Class = CharmDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            CharmDomainGreaterResource.m_MaxAmount.m_Archetypes = CharmDomainGreaterResource.m_MaxAmount.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Community Domain Hooks
            var CommunityDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("b1b8efd70ba5dd84aa6985d46dc299d5").GetComponent<ContextRankConfig>();
            CommunityDomainBaseAbilityConfig.m_AdditionalArchetypes = CommunityDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            CommunityDomainBaseAbilityConfig.m_Class = CommunityDomainBaseAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var CommunityDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("102d61a114786894bb2b30568943ef1f").GetComponent<AddFeatureOnClassLevel>();
            CommunityDomainBaseFeatureConfig.m_AdditionalClasses = CommunityDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            CommunityDomainBaseFeatureConfig.m_Archetypes = CommunityDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var CommunityDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("76291e62d2496ad41824044aba3077ea").GetComponent<ContextRankConfig>();
            CommunityDomainGreaterAbilityConfig.m_AdditionalArchetypes = CommunityDomainGreaterAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            CommunityDomainGreaterAbilityConfig.m_Class = CommunityDomainGreaterAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Darkness Domain Hooks
            var DarknessDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("39ed9d4b1e033e042aac4f9eb9c7315f").GetComponent<ContextRankConfig>();
            DarknessDomainBaseAbilityConfig.m_AdditionalArchetypes = DarknessDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            DarknessDomainBaseAbilityConfig.m_Class = DarknessDomainBaseAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var DarknessDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("9dc5863168155854fa8daf4a780f6663").GetComponent<AddFeatureOnClassLevel>();
            DarknessDomainBaseFeatureConfig.m_AdditionalClasses = DarknessDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            DarknessDomainBaseFeatureConfig.m_Archetypes = DarknessDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
                //If multiple Components with same name use ".ForEach"
            var DarknessDomainGreaterAbility = Resources.GetBlueprint<BlueprintAbility>("31acd268039966940872c916782ae018");
            DarknessDomainGreaterAbility.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_AdditionalArchetypes = c.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>()); 
            });
            DarknessDomainGreaterAbility.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            });
            var DarknessDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("55efb511a2290b94bb218e2d56a51f1f");
            DarknessDomainGreaterResource.m_MaxAmount.m_ClassDiv = DarknessDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Death Domain Hooks
            var DeathDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("979f63920af22344d81da5099c9ec32e").GetComponent<ContextRankConfig>();
            DeathDomainBaseAbilityConfig.m_AdditionalArchetypes = DeathDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            DeathDomainBaseAbilityConfig.m_Class = DeathDomainBaseAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var DeathDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("9809efa15e5f9ad478594479af575a5d").GetComponent<AddFeatureOnClassLevel>();
            DeathDomainBaseFeatureConfig.m_AdditionalClasses = DeathDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            DeathDomainBaseFeatureConfig.m_Archetypes = DeathDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Destruction Domain Hooks
            var DestructionDomainBaseBuffConfig = Resources.GetBlueprint<BlueprintBuff>("0dfe08afb3cf3594987bab12d014e74b").GetComponent<ContextRankConfig>();
            DestructionDomainBaseBuffConfig.m_AdditionalArchetypes = DestructionDomainBaseBuffConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            DestructionDomainBaseBuffConfig.m_Class = DestructionDomainBaseBuffConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var DestructionDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("2d3b9491bc05a114ab10e5b1b30dc86a").GetComponent<AddFeatureOnClassLevel>();
            DestructionDomainBaseFeatureConfig.m_AdditionalClasses = DestructionDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            DestructionDomainBaseFeatureConfig.m_Archetypes = DestructionDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var DestructionDomainGreaterEffectConfig = Resources.GetBlueprint<BlueprintBuff>("f9de414e53a9c23419fa3cfc0daabde7").GetComponent<ContextRankConfig>();
            DestructionDomainGreaterEffectConfig.m_AdditionalArchetypes = DestructionDomainGreaterEffectConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            DestructionDomainGreaterEffectConfig.m_Class = DestructionDomainGreaterEffectConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var DestructionDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("2f9b00619b54bed4ba0c3b02298f9c34").GetComponent<ContextRankConfig>();
            DestructionDomainGreaterAbilityConfig.m_AdditionalArchetypes = DestructionDomainGreaterAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            DestructionDomainGreaterAbilityConfig.m_Class = DestructionDomainGreaterAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var DestructionDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("98f07eabe9cb4f34cb1127de625f4bee");
            DestructionDomainGreaterResource.m_MaxAmount.m_Class = DestructionDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            DestructionDomainGreaterResource.m_MaxAmount.m_Archetypes = DestructionDomainGreaterResource.m_MaxAmount.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Earth Domain Hooks
            var EarthDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("828d82a0e8c5a944bbdb6b12f802ff02").GetComponent<AddFeatureOnClassLevel>();
            EarthDomainBaseFeatureConfig.m_AdditionalClasses = EarthDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            EarthDomainBaseFeatureConfig.m_Archetypes = EarthDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var EarthDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("3ff40918d33219942929f0dbfe5d1dee").GetComponent<ContextRankConfig>();
            EarthDomainBaseAbilityConfig.m_AdditionalArchetypes = EarthDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            EarthDomainBaseAbilityConfig.m_Class = EarthDomainBaseAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Fire Domain Hooks
            var FireDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("42cc125d570c5334c89c6499b55fc0a3").GetComponent<AddFeatureOnClassLevel>();
            FireDomainBaseFeatureConfig.m_AdditionalClasses = FireDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            FireDomainBaseFeatureConfig.m_Archetypes = FireDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var FireDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("4ecdf240d81533f47a5279f5075296b9").GetComponent<ContextRankConfig>();
            FireDomainBaseAbilityConfig.m_AdditionalArchetypes = FireDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            FireDomainBaseAbilityConfig.m_Class = FireDomainBaseAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Glory Domain Hooks
            var GloryDomainBaseBuffConfig = Resources.GetBlueprint<BlueprintBuff>("55edcfff497a1e04a963f72c485da5cb").GetComponent<ContextRankConfig>();
            GloryDomainBaseBuffConfig.m_AdditionalArchetypes = GloryDomainBaseBuffConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            GloryDomainBaseBuffConfig.m_Class = GloryDomainBaseBuffConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var GloryDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("d018241b5a761414897ad6dc4df2db9f").GetComponent<ContextRankConfig>();
            GloryDomainBaseAbilityConfig.m_AdditionalArchetypes = GloryDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            GloryDomainBaseAbilityConfig.m_Class = GloryDomainBaseAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var GloryDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("17e891b3964492f43aae44f994b5d454").GetComponent<AddFeatureOnClassLevel>();
            GloryDomainBaseFeatureConfig.m_AdditionalClasses = GloryDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            GloryDomainBaseFeatureConfig.m_Archetypes = GloryDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var GloryDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("c89e92387e940e541b02c1969cd1fe2a").GetComponent<ContextRankConfig>();
            GloryDomainGreaterAbilityConfig.m_AdditionalArchetypes = GloryDomainGreaterAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            GloryDomainGreaterAbilityConfig.m_Class = GloryDomainGreaterAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Good Domain Hooks
            var GoodDomainBaseBuffConfig = Resources.GetBlueprint<BlueprintBuff>("f185e4585bda72b479956772944ee665").GetComponent<ContextRankConfig>();
            GoodDomainBaseBuffConfig.m_AdditionalArchetypes = GoodDomainBaseBuffConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            GoodDomainBaseBuffConfig.m_Class = GoodDomainBaseBuffConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var GoodDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("f27684b3b72c2f546abf3ef2fb611a05").GetComponent<AddFeatureOnClassLevel>();
            GoodDomainBaseFeatureConfig.m_AdditionalClasses = GoodDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            GoodDomainBaseFeatureConfig.m_Archetypes = GoodDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var GoodDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("017afe6934e10c3489176e759a5f01b0").GetComponent<ContextRankConfig>();
            GoodDomainBaseAbilityConfig.m_AdditionalArchetypes = GoodDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            GoodDomainBaseAbilityConfig.m_Class = GoodDomainBaseAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var GoodDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("7fc3e743ba28fd64f977fb55b7536053").GetComponent<ContextRankConfig>();
            GoodDomainGreaterAbilityConfig.m_AdditionalArchetypes = GoodDomainGreaterAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            GoodDomainGreaterAbilityConfig.m_Class = GoodDomainGreaterAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var GoodDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("8d45a527ce4d3ec47853faaa972c2362");
            GoodDomainGreaterResource.m_MaxAmount.m_ClassDiv = GoodDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Healing Domain Hooks
            var HealingDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("18f734e40dd7966438ab32086c3574e1").GetComponent<ContextRankConfig>();
            HealingDomainBaseAbilityConfig.m_AdditionalArchetypes = HealingDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            HealingDomainBaseAbilityConfig.m_Class = HealingDomainBaseAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var HealingdDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("303cf1c933f343c4d91212f8f4953e3c").GetComponent<AddFeatureOnClassLevel>();
            HealingdDomainBaseFeatureConfig.m_AdditionalClasses = HealingdDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            HealingdDomainBaseFeatureConfig.m_Archetypes = HealingdDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Knowledge Domain Hooks
            var KnowledgeDomainBaseBuffConfig = Resources.GetBlueprint<BlueprintBuff>("35fa55fe2c60e4442b670a88a70c06c3").GetComponent<ContextRankConfig>();
            KnowledgeDomainBaseBuffConfig.m_AdditionalArchetypes = KnowledgeDomainBaseBuffConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            KnowledgeDomainBaseBuffConfig.m_Class = KnowledgeDomainBaseBuffConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var KnowledgeDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("02a79a205bce6f5419dcdf26b64f13c6").GetComponent<ContextRankConfig>();
            KnowledgeDomainBaseAbilityConfig.m_AdditionalArchetypes = KnowledgeDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            KnowledgeDomainBaseAbilityConfig.m_Class = KnowledgeDomainBaseAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var KnowledgedDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("5335f015063776d429a0b5eab97eb060");
            KnowledgedDomainBaseFeatureConfig.GetComponents<AddFeatureOnClassLevel>().ForEach(c => {
                c.m_AdditionalClasses = c.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            });
            KnowledgedDomainBaseFeatureConfig.GetComponents<AddFeatureOnClassLevel>().ForEach(c => {
                c.m_Archetypes = c.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            });
            var KnowledgeDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("f88f616a4b6bd5f419025115c52cb329");
            KnowledgeDomainBaseResource.m_MaxAmount.m_Class = KnowledgeDomainBaseResource.m_MaxAmount.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            KnowledgeDomainBaseResource.m_MaxAmount.m_Archetypes = KnowledgeDomainBaseResource.m_MaxAmount.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var KnowledgeDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("ec582b195ccb2ef4ea8dcd96a5a6e009").GetComponent<ContextRankConfig>();
            KnowledgeDomainGreaterAbilityConfig.m_AdditionalArchetypes = KnowledgeDomainGreaterAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            KnowledgeDomainGreaterAbilityConfig.m_Class = KnowledgeDomainGreaterAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var KnowledgesDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("34f0a288ff5106645a88440b800686ca");
            KnowledgesDomainGreaterResource.m_MaxAmount.m_ClassDiv = KnowledgesDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Law Domain Hooks
            var LawDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("9bd2d216e56a0db44be0df48ffc515af").GetComponent<AddFeatureOnClassLevel>();
            LawDomainBaseFeatureConfig.m_AdditionalClasses = LawDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            LawDomainBaseFeatureConfig.m_Archetypes = LawDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var LawDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("0b1615ec2dabc6f4294a254b709188a4").GetComponent<ContextRankConfig>();
            LawDomainGreaterAbilityConfig.m_AdditionalArchetypes = LawDomainGreaterAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            LawDomainGreaterAbilityConfig.m_Class = LawDomainGreaterAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var LawDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("de7945c4cc6a0a24790941d7e2b85838");
            LawDomainGreaterResource.m_MaxAmount.m_ClassDiv = LawDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Liberation Domain Hooks
            var LiberationDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("7cc934aa505172a40b4a10c14c7681c4").GetComponent<AddFeatureOnClassLevel>();
            LiberationDomainBaseFeatureConfig.m_AdditionalClasses = LiberationDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            LiberationDomainBaseFeatureConfig.m_Archetypes = LiberationDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var LiberationDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("8ddc7f532cf2b3b4c877497856cc5b97");
            LiberationDomainBaseResource.m_MaxAmount.m_Class = LiberationDomainBaseResource.m_MaxAmount.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            LiberationDomainBaseResource.m_MaxAmount.m_Archetypes = LiberationDomainBaseResource.m_MaxAmount.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var LiberationDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("d19e900012a69954c93f3b7533bc3911");
            LiberationDomainGreaterResource.m_MaxAmount.m_Class = LiberationDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            LiberationDomainGreaterResource.m_MaxAmount.m_Archetypes = LiberationDomainGreaterResource.m_MaxAmount.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Luck Domain Hooks
            var LuckDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("2b3818bf4656c1a41b93467755662c78").GetComponent<AddFeatureOnClassLevel>();
            LuckDomainBaseFeatureConfig.m_AdditionalClasses = LuckDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            LuckDomainBaseFeatureConfig.m_Archetypes = LuckDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var LuckDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("0e0668a703fbfcf499d9aa9d918b71ea").GetComponent<ContextRankConfig>();
            LuckDomainGreaterAbilityConfig.m_AdditionalArchetypes = LuckDomainGreaterAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            LuckDomainGreaterAbilityConfig.m_Class = LuckDomainGreaterAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var LuckDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("b209ca75fbea5144c9d73ecb29055a08");
            LuckDomainGreaterResource.m_MaxAmount.m_ClassDiv = LuckDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Madness Domain Hooks
            var MadnessDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("84bf46e8086dbdc438bac875ab0e5c2f").GetComponent<AddFeatureOnClassLevel>();
            MadnessDomainBaseFeatureConfig.m_AdditionalClasses = MadnessDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            MadnessDomainBaseFeatureConfig.m_Archetypes = MadnessDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var MadnessDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("a3d470a27ec5e4540aeaf9723e9b8ae7").GetComponent<ContextRankConfig>();
            MadnessDomainGreaterAbilityConfig.m_AdditionalArchetypes = MadnessDomainGreaterAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            MadnessDomainGreaterAbilityConfig.m_Class = MadnessDomainGreaterAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var MadnessDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("3289ee86c57f6134d81770865c315e8b");
            MadnessDomainGreaterResource.m_MaxAmount.m_ClassDiv = MadnessDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var MadnessDomainBaseAttackRollsBuffConfig = Resources.GetBlueprint<BlueprintBuff>("6c69ec7a32190d44d99e746588de4a9c").GetComponent<ContextRankConfig>();
            MadnessDomainBaseAttackRollsBuffConfig.m_AdditionalArchetypes = MadnessDomainBaseAttackRollsBuffConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            MadnessDomainBaseAttackRollsBuffConfig.m_Class = MadnessDomainBaseAttackRollsBuffConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var MadnessDomainBaseSkillChecksBuffConfig = Resources.GetBlueprint<BlueprintBuff>("3e42877e5e481894880df63ad924e320").GetComponent<ContextRankConfig>();
            MadnessDomainBaseSkillChecksBuffConfig.m_AdditionalArchetypes = MadnessDomainBaseSkillChecksBuffConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            MadnessDomainBaseSkillChecksBuffConfig.m_Class = MadnessDomainBaseSkillChecksBuffConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var MadnessDomainBaseSavingThrowsBuffConfig = Resources.GetBlueprint<BlueprintBuff>("53c721d7519ac3047b818516bb28b20f").GetComponent<ContextRankConfig>();
            MadnessDomainBaseSavingThrowsBuffConfig.m_AdditionalArchetypes = MadnessDomainBaseSavingThrowsBuffConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            MadnessDomainBaseSavingThrowsBuffConfig.m_Class = MadnessDomainBaseSavingThrowsBuffConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var MadnessDomainBaseAbilitySkillChecksConfig = Resources.GetBlueprint<BlueprintAbility>("d92b2eac4dbf31f439e5bc9d2d467ff1").GetComponent<ContextRankConfig>();
            MadnessDomainBaseAbilitySkillChecksConfig.m_AdditionalArchetypes = MadnessDomainBaseAbilitySkillChecksConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            MadnessDomainBaseAbilitySkillChecksConfig.m_Class = MadnessDomainBaseAbilitySkillChecksConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var MadnessDomainBaseAbilityAttackRollsConfig = Resources.GetBlueprint<BlueprintAbility>("c3e4ff89950f1d748be6f5958b1aa19c").GetComponent<ContextRankConfig>();
            MadnessDomainBaseAbilityAttackRollsConfig.m_AdditionalArchetypes = MadnessDomainBaseAbilityAttackRollsConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            MadnessDomainBaseAbilityAttackRollsConfig.m_Class = MadnessDomainBaseAbilityAttackRollsConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var MadnessDomainBaseAbilitySavingThrowsConfig = Resources.GetBlueprint<BlueprintAbility>("c09446b861bac7b4b83877db863150d9").GetComponent<ContextRankConfig>();
            MadnessDomainBaseAbilitySavingThrowsConfig.m_AdditionalArchetypes = MadnessDomainBaseAbilitySavingThrowsConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            MadnessDomainBaseAbilitySavingThrowsConfig.m_Class = MadnessDomainBaseAbilitySavingThrowsConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Magic Domain Hooks
            var MagicDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("90f939eb611ac3743b5de3dd00135e22").GetComponent<AddFeatureOnClassLevel>();
            MagicDomainBaseFeatureConfig.m_AdditionalClasses = MagicDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            MagicDomainBaseFeatureConfig.m_Archetypes = MagicDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var MagicDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("da9e93791894b9d49a1f2bebd80e8085");
            MagicDomainBaseResource.m_MaxAmount.m_ClassDiv = MagicDomainBaseResource.m_MaxAmount.m_ClassDiv.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var MagicDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("3aecc0c5d17390443b30774309145854");
            MagicDomainGreaterResource.m_MaxAmount.m_ClassDiv = MagicDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Nobility Domain Hooks
            var NobilityDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("7a305ef528cb7884385867a2db410102").GetComponent<ContextRankConfig>();
            NobilityDomainBaseAbilityConfig.m_AdditionalArchetypes = NobilityDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            NobilityDomainBaseAbilityConfig.m_Class = NobilityDomainBaseAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var NobilityDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("a1a7f3dd904ed8e45b074232f48190d1").GetComponent<AddFeatureOnClassLevel>();
            NobilityDomainBaseFeatureConfig.m_AdditionalClasses = NobilityDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            NobilityDomainBaseFeatureConfig.m_Archetypes = NobilityDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var NobilityDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("3fc6e1f3acbcb0e4c83badf7709ce53d");
            NobilityDomainBaseResource.m_MaxAmount.m_Class = NobilityDomainBaseResource.m_MaxAmount.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            NobilityDomainBaseResource.m_MaxAmount.m_Archetypes = NobilityDomainBaseResource.m_MaxAmount.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var NobilityDomainGreaterAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("2972215a5367ae44b8ddfe435a127a6e").GetComponent<ContextRankConfig>();
            NobilityDomainGreaterAbilityConfig.m_AdditionalArchetypes = NobilityDomainGreaterAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            NobilityDomainGreaterAbilityConfig.m_Class = NobilityDomainGreaterAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var NobilityDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("cb3efe82596c908418c0dba4ef6f4210");
            NobilityDomainGreaterResource.m_MaxAmount.m_ClassDiv = NobilityDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Plant Domain Hooks
            var PlantDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("e433267d36089d049b34900fde38032b").GetComponent<AddFeatureOnClassLevel>();
            PlantDomainBaseFeatureConfig.m_AdditionalClasses = PlantDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            PlantDomainBaseFeatureConfig.m_Archetypes = PlantDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var PlantDomainGreaterBuffConfig = Resources.GetBlueprint<BlueprintBuff>("58d86cc848805024abbbefd6abe2d433").GetComponent<ContextRankConfig>();
            PlantDomainGreaterBuffConfig.m_AdditionalArchetypes = PlantDomainGreaterBuffConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            PlantDomainGreaterBuffConfig.m_Class = PlantDomainGreaterBuffConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var PlantDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("8942e816a533a4a40b04745c516d085a");
            PlantDomainBaseResource.m_MaxAmount.m_Class = PlantDomainBaseResource.m_MaxAmount.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            PlantDomainBaseResource.m_MaxAmount.m_Archetypes = PlantDomainBaseResource.m_MaxAmount.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var PlantDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("881d696940ec99041aefafd5b2fda189");
            PlantDomainGreaterResource.m_MaxAmount.m_Class = PlantDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            PlantDomainGreaterResource.m_MaxAmount.m_Archetypes = PlantDomainGreaterResource.m_MaxAmount.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Protection Domain Hooks
            var ProtectionDomainBaseBuffConfig = Resources.GetBlueprint<BlueprintBuff>("2ddb4cfc3cfd04c46a66c6cd26df1c06").GetComponent<ContextRankConfig>();
            ProtectionDomainBaseBuffConfig.m_AdditionalArchetypes = ProtectionDomainBaseBuffConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            ProtectionDomainBaseBuffConfig.m_Class = ProtectionDomainBaseBuffConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var ProtectionDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("c5815bd0bf87bdb4fa9c440c8088149b").GetComponent<ContextRankConfig>();
            ProtectionDomainBaseAbilityConfig.m_AdditionalArchetypes = ProtectionDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            ProtectionDomainBaseAbilityConfig.m_Class = ProtectionDomainBaseAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var ProtectionDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("a05a8959c594daa40a1c5add79566566").GetComponent<AddFeatureOnClassLevel>();
            ProtectionDomainBaseFeatureConfig.m_AdditionalClasses = ProtectionDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            ProtectionDomainBaseFeatureConfig.m_Archetypes = ProtectionDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var ProtectionDomainBaseSelfBuffConfig = Resources.GetBlueprint<BlueprintBuff>("74a4fb45f23705d4db2784d16eb93138").GetComponent<ContextRankConfig>();
            ProtectionDomainBaseSelfBuffConfig.m_AdditionalArchetypes = ProtectionDomainBaseSelfBuffConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            ProtectionDomainBaseSelfBuffConfig.m_Class = ProtectionDomainBaseSelfBuffConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var ProtectionDomainGreaterEffect = Resources.GetBlueprint<BlueprintBuff>("fea7c44605c90f14fa40b2f2f5ae6339");
            ProtectionDomainGreaterEffect.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_AdditionalArchetypes = c.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            });
            ProtectionDomainGreaterEffect.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            });
            var ProtectionDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("f3d878f77d0ee854b864f5ea1c80e752");
            ProtectionDomainGreaterResource.m_MaxAmount.m_Class = ProtectionDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            ProtectionDomainGreaterResource.m_MaxAmount.m_Archetypes = ProtectionDomainGreaterResource.m_MaxAmount.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Repose Domain Hooks
            var ReposeDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("8526bc808c303034cb2b7832bccf1482").GetComponent<AddFeatureOnClassLevel>();
            ReposeDomainBaseFeatureConfig.m_AdditionalClasses = ReposeDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            ReposeDomainBaseFeatureConfig.m_Archetypes = ReposeDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var ReposeDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("aefe627a3a2f8d94ea9d2b3961261282");
            ReposeDomainGreaterResource.m_MaxAmount.m_Class = ReposeDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            ReposeDomainGreaterResource.m_MaxAmount.m_Archetypes = ReposeDomainGreaterResource.m_MaxAmount.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Rune Domain Hooks
            var RuneDomainGreaterAreaConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("e26de8b0164db23458eb64c21fac2846").GetComponent<ContextRankConfig>();
            RuneDomainGreaterAreaConfig.m_AdditionalArchetypes = RuneDomainGreaterAreaConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            RuneDomainGreaterAreaConfig.m_Class = RuneDomainGreaterAreaConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var RuneDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("b74c64a0152c7ee46b13ecdd72dda6f3").GetComponent<AddFeatureOnClassLevel>();
            RuneDomainBaseFeatureConfig.m_AdditionalClasses = RuneDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            RuneDomainBaseFeatureConfig.m_Archetypes = RuneDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var RuneDomainGreaterAbility = Resources.GetBlueprint<BlueprintAbility>("9171a3ce8ea8cac44894b240709804ce");
            RuneDomainGreaterAbility.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_AdditionalArchetypes = c.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            });
            RuneDomainGreaterAbility.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            });
            var RuneDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("cb3efe82596c908418c0dba4ef6f4210");
            RuneDomainGreaterResource.m_MaxAmount.m_ClassDiv = RuneDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var RuneDomainBaseAbilityFire = Resources.GetBlueprint<BlueprintAbility>("eddfe26a8a3892b47add3cb08db7069d");
            RuneDomainBaseAbilityFire.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_AdditionalArchetypes = c.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            });
            RuneDomainBaseAbilityFire.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            });
            var RuneDomainBaseAbilityCold = Resources.GetBlueprint<BlueprintAbility>("2b81ff42fcbe9434eaf00fb0a873f579");
            RuneDomainBaseAbilityCold.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_AdditionalArchetypes = c.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            });
            RuneDomainBaseAbilityCold.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            });
            var RuneDomainBaseAbilityAcid = Resources.GetBlueprint<BlueprintAbility>("92c821ecc8d73564bad15a8a07ed40f2");
            RuneDomainBaseAbilityAcid.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_AdditionalArchetypes = c.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            });
            RuneDomainBaseAbilityAcid.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            });
            var RuneDomainBaseAbilityFireAreaConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("9b786945d2ec1884184235a488e5cb9e").GetComponent<ContextRankConfig>();
            RuneDomainBaseAbilityFireAreaConfig.m_AdditionalArchetypes = RuneDomainBaseAbilityFireAreaConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            RuneDomainBaseAbilityFireAreaConfig.m_Class = RuneDomainBaseAbilityFireAreaConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var RuneDomainBaseAbilityColdAreaConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("8b8e98e8e0000f643ad97c744f3f850b").GetComponent<ContextRankConfig>();
            RuneDomainBaseAbilityColdAreaConfig.m_AdditionalArchetypes = RuneDomainBaseAbilityColdAreaConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            RuneDomainBaseAbilityColdAreaConfig.m_Class = RuneDomainBaseAbilityColdAreaConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var RuneDomainBaseAbilityAcidAreaConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("98c3a36f2a3636c49a3f77c001a25f29").GetComponent<ContextRankConfig>();
            RuneDomainBaseAbilityAcidAreaConfig.m_AdditionalArchetypes = RuneDomainBaseAbilityAcidAreaConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            RuneDomainBaseAbilityAcidAreaConfig.m_Class = RuneDomainBaseAbilityAcidAreaConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var RuneDomainBaseAbilityElectricity = Resources.GetBlueprint<BlueprintAbility>("b67978e3d5a6c9247a393237bc660339");
            RuneDomainBaseAbilityElectricity.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_AdditionalArchetypes = c.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            });
            RuneDomainBaseAbilityElectricity.GetComponents<ContextRankConfig>().ForEach(c => {
                c.m_Class = c.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            });
            var RuneDomainBaseAbilityElectricityAreaConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("db868c576c69d0e4a8462645267c6cdc").GetComponent<ContextRankConfig>();
            RuneDomainBaseAbilityElectricityAreaConfig.m_AdditionalArchetypes = RuneDomainBaseAbilityElectricityAreaConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            RuneDomainBaseAbilityElectricityAreaConfig.m_Class = RuneDomainBaseAbilityElectricityAreaConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Strength Domain Hooks
            var StrengthDomainBaseBuffConfig = Resources.GetBlueprint<BlueprintBuff>("94dfcf5f3a72ce8478c8de5db69e752b").GetComponent<ContextRankConfig>();
            StrengthDomainBaseBuffConfig.m_AdditionalArchetypes = StrengthDomainBaseBuffConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            StrengthDomainBaseBuffConfig.m_Class = StrengthDomainBaseBuffConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var StrengthDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("1d6364123e1f6a04c88313d83d3b70ee").GetComponent<ContextRankConfig>();
            StrengthDomainBaseAbilityConfig.m_AdditionalArchetypes = StrengthDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            StrengthDomainBaseAbilityConfig.m_Class = StrengthDomainBaseAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var StrengthDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("526f99784e9fe4346824e7f210d46112").GetComponent<AddFeatureOnClassLevel>();
            StrengthDomainBaseFeatureConfig.m_AdditionalClasses = StrengthDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            StrengthDomainBaseFeatureConfig.m_Archetypes = StrengthDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var StrengthDomainGreaterFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("3298fd30e221ef74189a06acbf376d29").GetComponent<ContextRankConfig>();
            StrengthDomainGreaterFeatureConfig.m_AdditionalArchetypes = StrengthDomainGreaterFeatureConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            StrengthDomainGreaterFeatureConfig.m_Class = StrengthDomainGreaterFeatureConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Sun Domain Hooks
            var SunDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("3d8e38c9ed54931469281ab0cec506e9").GetComponent<AddFeatureOnClassLevel>();
            SunDomainBaseFeatureConfig.m_AdditionalClasses = SunDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            SunDomainBaseFeatureConfig.m_Archetypes = SunDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            ///new - added for fix
            var SunDomainBaseFeatureDamageConfig = Resources.GetBlueprint<BlueprintFeature>("3d8e38c9ed54931469281ab0cec506e9").GetComponent<IncreaseSpellDamageByClassLevel>();
            SunDomainBaseFeatureDamageConfig.m_AdditionalClasses = SunDomainBaseFeatureDamageConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            SunDomainBaseFeatureDamageConfig.m_Archetypes = SunDomainBaseFeatureDamageConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());

            var SunDomainGreaterAuraConfig = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("cfe8c5683c759f047a56a4b5e77ac93f").GetComponent<ContextRankConfig>();
            SunDomainGreaterAuraConfig.m_AdditionalArchetypes = SunDomainGreaterAuraConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            SunDomainGreaterAuraConfig.m_Class = SunDomainGreaterAuraConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var SunDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("6bea29e2257fa6742923ba757435aba8");
            SunDomainGreaterResource.m_MaxAmount.m_Class = SunDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            SunDomainGreaterResource.m_MaxAmount.m_Archetypes = SunDomainGreaterResource.m_MaxAmount.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            SunDomainGreaterResource.m_MaxAmount.m_ClassDiv = SunDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Travel Domain Hooks
            var TravelDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("3079cdfba971d614ab4f49220c6cd228").GetComponent<AddFeatureOnClassLevel>();
            TravelDomainBaseFeatureConfig.m_AdditionalClasses = TravelDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            TravelDomainBaseFeatureConfig.m_Archetypes = TravelDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var TravelDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("52ee1ad8d1ac94d4b92a62acfa8931ad");
            TravelDomainBaseResource.m_MaxAmount.m_Class = TravelDomainBaseResource.m_MaxAmount.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            TravelDomainBaseResource.m_MaxAmount.m_Archetypes = TravelDomainBaseResource.m_MaxAmount.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var TravelDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("657bfb21544642e4f8aef532c9f04ac2");
            TravelDomainGreaterResource.m_MaxAmount.m_Class = TravelDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            TravelDomainGreaterResource.m_MaxAmount.m_Archetypes = TravelDomainGreaterResource.m_MaxAmount.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            TravelDomainGreaterResource.m_MaxAmount.m_ClassDiv = TravelDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Trickery Domain Hooks
            var TrickeryDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("ee7eb5b9c644a0347b36eec653d3dfcb").GetComponent<ContextRankConfig>();
            TrickeryDomainBaseAbilityConfig.m_AdditionalArchetypes = TrickeryDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            TrickeryDomainBaseAbilityConfig.m_Class = TrickeryDomainBaseAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var TrickeryDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("cd1f4a784e0820647a34fe9bd5ffa770").GetComponent<AddFeatureOnClassLevel>();
            TrickeryDomainBaseFeatureConfig.m_AdditionalClasses = TrickeryDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            TrickeryDomainBaseFeatureConfig.m_Archetypes = TrickeryDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var TrickeryDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("148c9ad7e47f4284b9c3686bb440c08c");
            TrickeryDomainBaseResource.m_MaxAmount.m_Class = TrickeryDomainBaseResource.m_MaxAmount.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            TrickeryDomainBaseResource.m_MaxAmount.m_Archetypes = TrickeryDomainBaseResource.m_MaxAmount.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var TrickeryDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("520ad6381e09f8349a237ac4b247082e");
            TrickeryDomainGreaterResource.m_MaxAmount.m_Class = TrickeryDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            TrickeryDomainGreaterResource.m_MaxAmount.m_Archetypes = TrickeryDomainGreaterResource.m_MaxAmount.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            TrickeryDomainGreaterResource.m_MaxAmount.m_ClassDiv = TrickeryDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //War Domain Hooks
            var WarDomainBaseBuffConfig = Resources.GetBlueprint<BlueprintBuff>("aefec65136058694ab20cd71941eec81").GetComponent<ContextRankConfig>();
            WarDomainBaseBuffConfig.m_AdditionalArchetypes = WarDomainBaseBuffConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            WarDomainBaseBuffConfig.m_Class = WarDomainBaseBuffConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var WarDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("fbef6b2053ab6634a82df06f76c260e3").GetComponent<ContextRankConfig>();
            WarDomainBaseAbilityConfig.m_AdditionalArchetypes = WarDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            WarDomainBaseAbilityConfig.m_Class = WarDomainBaseAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var WarDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("640c20da7d6fcbc43b0d30a0a762f122").GetComponent<AddFeatureOnClassLevel>();
            WarDomainBaseFeatureConfig.m_AdditionalClasses = WarDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            WarDomainBaseFeatureConfig.m_Archetypes = WarDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Water Domain Hooks
            var WaterDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("4c21ad24f55f64d4fb722f40720d9ab0").GetComponent<AddFeatureOnClassLevel>();
            WaterDomainBaseFeatureConfig.m_AdditionalClasses = WaterDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            WaterDomainBaseFeatureConfig.m_Archetypes = WaterDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var WaterDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("5e1db2ef80ff361448549beeb7785791").GetComponent<ContextRankConfig>();
            WaterDomainBaseAbilityConfig.m_AdditionalArchetypes = WaterDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            WaterDomainBaseAbilityConfig.m_Class = WaterDomainBaseAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Weather Domain Hooks
            var WeatherDomainBaseFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("1c37869ee06ca33459f16f23f4969e7d").GetComponent<AddFeatureOnClassLevel>();
            WeatherDomainBaseFeatureConfig.m_AdditionalClasses = WeatherDomainBaseFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            WeatherDomainBaseFeatureConfig.m_Archetypes = WeatherDomainBaseFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var WeatherDomainBaseAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("f166325c271dd29449ba9f98d11542d9").GetComponent<ContextRankConfig>();
            WeatherDomainBaseAbilityConfig.m_AdditionalArchetypes = WeatherDomainBaseAbilityConfig.m_AdditionalArchetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            WeatherDomainBaseAbilityConfig.m_Class = WeatherDomainBaseAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            var WeatherDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("53dd76c7053469541b99e01cb25711d6");
            WeatherDomainBaseResource.m_MaxAmount.m_Class = WeatherDomainBaseResource.m_MaxAmount.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            WeatherDomainBaseResource.m_MaxAmount.m_Archetypes = WeatherDomainBaseResource.m_MaxAmount.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var WeatherDomainGreaterResource = Resources.GetBlueprint<BlueprintAbilityResource>("5c88b557e79eaee41a4190712b178970");
            WeatherDomainGreaterResource.m_MaxAmount.m_Class = WeatherDomainGreaterResource.m_MaxAmount.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            WeatherDomainGreaterResource.m_MaxAmount.m_Archetypes = WeatherDomainGreaterResource.m_MaxAmount.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            WeatherDomainGreaterResource.m_MaxAmount.m_ClassDiv = WeatherDomainGreaterResource.m_MaxAmount.m_ClassDiv.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            #endregion

            //Blessings
            var BlessingResource = Resources.GetBlueprint<BlueprintAbilityResource>("d128a6332e4ea7c4a9862b9fdb358cca");
            BlessingResource.m_MaxAmount.m_Class = BlessingResource.m_MaxAmount.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            BlessingResource.m_MaxAmount.m_Archetypes = BlessingResource.m_MaxAmount.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            BlessingResource.m_MaxAmount.m_ClassDiv = BlessingResource.m_MaxAmount.m_ClassDiv.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            //Air
            var AirBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("e1ff99dc3aeaa064e8eecde51c1c4773").GetComponent<AddFeatureOnClassLevel>();
            AirBlessingFeatureConfig.m_AdditionalClasses = AirBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            AirBlessingFeatureConfig.m_Archetypes = AirBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Animal
            var AnimalBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("9d991f8374c3def4cb4a6287f370814d").GetComponent<AddFeatureOnClassLevel>();
            AnimalBlessingFeatureConfig.m_AdditionalClasses = AnimalBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            AnimalBlessingFeatureConfig.m_Archetypes = AnimalBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            var AnimalBlessingMajorAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("93f0098fe08b94f41a351a4fbb00518a").GetComponent<ContextRankConfig>();
            AnimalBlessingMajorAbilityConfig.m_Class = AnimalBlessingMajorAbilityConfig.m_Class.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            AnimalBlessingMajorAbilityConfig.Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>();
            //Darkness
            var DarknessBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("3ed6cd88caecec944b837f57b9be176f").GetComponent<AddFeatureOnClassLevel>();
            DarknessBlessingFeatureConfig.m_AdditionalClasses = DarknessBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            DarknessBlessingFeatureConfig.m_Archetypes = DarknessBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Death
            var DeathBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("6d11e8b00add90c4f93c2ad6d12885f7").GetComponent<AddFeatureOnClassLevel>();
            DeathBlessingFeatureConfig.m_AdditionalClasses = DeathBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            DeathBlessingFeatureConfig.m_Archetypes = DeathBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Destruction
            var DestructionBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("dd5e75a02e4563e44a0931c6f46fb0a7").GetComponent<AddFeatureOnClassLevel>();
            DestructionBlessingFeatureConfig.m_AdditionalClasses = DestructionBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            DestructionBlessingFeatureConfig.m_Archetypes = DestructionBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Earth
            var EarthBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("73c37a22bc9a523409a47218d507acf6").GetComponent<AddFeatureOnClassLevel>();
            EarthBlessingFeatureConfig.m_AdditionalClasses = EarthBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            EarthBlessingFeatureConfig.m_Archetypes = EarthBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Fire
            var FireBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("2368212fa3856d74589e924d3e2074d8").GetComponent<AddFeatureOnClassLevel>();
            FireBlessingFeatureConfig.m_AdditionalClasses = FireBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            FireBlessingFeatureConfig.m_Archetypes = FireBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Healing
            var HealingBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("f3881a1a7b44dc74c9d76907c94e49f2").GetComponent<AddFeatureOnClassLevel>();
            HealingBlessingFeatureConfig.m_AdditionalClasses = HealingBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            HealingBlessingFeatureConfig.m_Archetypes = HealingBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Law
            var LawBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("9c49504e2e4c66d4aa341348356b47a8").GetComponent<AddFeatureOnClassLevel>();
            LawBlessingFeatureConfig.m_AdditionalClasses = LawBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            LawBlessingFeatureConfig.m_Archetypes = LawBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Liberation
            var LiberationBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("61061faf477d67b43b6dedb3e8f205d7").GetComponent<AddFeatureOnClassLevel>();
            LiberationBlessingFeatureConfig.m_AdditionalClasses = LiberationBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            LiberationBlessingFeatureConfig.m_Archetypes = LiberationBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Luck
            var LuckBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("70654ee784fffa74489933a0d2047bbd").GetComponent<AddFeatureOnClassLevel>();
            LuckBlessingFeatureConfig.m_AdditionalClasses = LuckBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            LuckBlessingFeatureConfig.m_Archetypes = LuckBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Madness
            var MadnessBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("a99916a8aad2414f970072db7b760c48").GetComponent<AddFeatureOnClassLevel>();
            MadnessBlessingFeatureConfig.m_AdditionalClasses = MadnessBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            MadnessBlessingFeatureConfig.m_Archetypes = MadnessBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Magic
            var MagicBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("1754ff61a0805714fa2b89c8c1bb87ad").GetComponent<AddFeatureOnClassLevel>();
            MagicBlessingFeatureConfig.m_AdditionalClasses = MagicBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            MagicBlessingFeatureConfig.m_Archetypes = MagicBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Nobility
            var NobilityBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("f52af97d05e5de34ea6e0d1b0af740ea").GetComponent<AddFeatureOnClassLevel>();
            NobilityBlessingFeatureConfig.m_AdditionalClasses = NobilityBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            NobilityBlessingFeatureConfig.m_Archetypes = NobilityBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Protection
            var ProtectionBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("c6a3fa9d8d7f942499e4909cd01ca22d").GetComponent<AddFeatureOnClassLevel>();
            ProtectionBlessingFeatureConfig.m_AdditionalClasses = ProtectionBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            ProtectionBlessingFeatureConfig.m_Archetypes = ProtectionBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Repose
            var ReposeBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("64a416082927673409deb330af04d6d2").GetComponent<AddFeatureOnClassLevel>();
            ReposeBlessingFeatureConfig.m_AdditionalClasses = ReposeBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            ReposeBlessingFeatureConfig.m_Archetypes = ReposeBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Strength
            var StrengthBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("44f9162736a5c2040ae8ede853bc6639").GetComponent<AddFeatureOnClassLevel>();
            StrengthBlessingFeatureConfig.m_AdditionalClasses = StrengthBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            StrengthBlessingFeatureConfig.m_Archetypes = StrengthBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Sun
            var SunBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("ba825e3c77acaec4386e00f691f8f3be").GetComponent<AddFeatureOnClassLevel>();
            SunBlessingFeatureConfig.m_AdditionalClasses = SunBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            SunBlessingFeatureConfig.m_Archetypes = SunBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Travel
            var TravelBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("87641a8efec53d64d853ecc436234dce").GetComponent<AddFeatureOnClassLevel>();
            TravelBlessingFeatureConfig.m_AdditionalClasses = TravelBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            TravelBlessingFeatureConfig.m_Archetypes = TravelBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Trickery
            var TrickeryBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("a8e7abcad0cf8384b9f12c3b075b5cae").GetComponent<AddFeatureOnClassLevel>();
            TrickeryBlessingFeatureConfig.m_AdditionalClasses = TrickeryBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            TrickeryBlessingFeatureConfig.m_Archetypes = TrickeryBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Water
            var WaterBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("0f457943bb99f9b48b709c90bfc0467e").GetComponent<AddFeatureOnClassLevel>();
            WaterBlessingFeatureConfig.m_AdditionalClasses = WaterBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            WaterBlessingFeatureConfig.m_Archetypes = WaterBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            //Weather
            var WeatherBlessingFeatureConfig = Resources.GetBlueprint<BlueprintFeature>("4172d92c598de1d47aa2c0dd51c05e24").GetComponent<AddFeatureOnClassLevel>();
            WeatherBlessingFeatureConfig.m_AdditionalClasses = WeatherBlessingFeatureConfig.m_AdditionalClasses.AppendToArray(PaladinClass.ToReference<BlueprintCharacterClassReference>());
            WeatherBlessingFeatureConfig.m_Archetypes = WeatherBlessingFeatureConfig.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());

            if (ModSettings.AddedContent.Archetypes.IsDisabled("Temple Champion")) { return; }
            PaladinClass.m_Archetypes = PaladinClass.m_Archetypes.AppendToArray(TempleChampionArchetype.ToReference<BlueprintArchetypeReference>());
            
        }
        
    }
}
