using BlueprintCore.Blueprints.Configurators.AI;
using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Classes;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Utilities {
    public static class DeityTools {
        public static class DomainAllowed {
            public static BlueprintFeature AirDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("6e5f4ff5a7010754ca78708ce1a9b233");
            public static BlueprintFeature AnimalDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("9f05f9da2ea5ae44eac47d407a0000e5");
            public static BlueprintFeature ArtificeDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("9656b1c7214180f4b9a6ab56f83b92fb");
            public static BlueprintFeature ChaosDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("8c7d778bc39fec642befc1435b00f613");
            public static BlueprintFeature CharmDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("f1ceba79ee123cc479cece27bc994ff2");
            public static BlueprintFeature CommunityDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("c87004460f3328c408d22c5ead05291f");
            public static BlueprintFeature DarknessDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("6d8e7accdd882e949a63021af5cde4b8");
            public static BlueprintFeature DeathDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("a099afe1b0b32554199b230699a69525");
            public static BlueprintFeature DestructionDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("6832681c9a91bf946a1d9da28c5be4b4");
            public static BlueprintFeature EarthDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("5ca99a6ae118feb449dbbd165a8fe7c4");
            public static BlueprintFeature EvilDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("351235ac5fc2b7e47801f63d117b656c");
            public static BlueprintFeature FireDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("8d4e9731082008640b28417f577f5f31");
            public static BlueprintFeature GloryDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("2418251fa9c8ada4bbfbaaf5c90ac200");
            public static BlueprintFeature GoodDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("882521af8012fc749930b03dc18a69de");
            public static BlueprintFeature HealingDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("73ae164c388990c43ade94cfe8ed5755");
            public static BlueprintFeature IceDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("f15ae4713cee44e98de47746c95c76ab");
            public static BlueprintFeature KnowledgeDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("443d44b3e0ea84046a9bf304c82a0425");
            public static BlueprintFeature LawDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("092714336606cfc45a37d2ab39fabfa8");
            public static BlueprintFeature LiberationDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("801ca88338451a546bca2ee59da87c53");
            public static BlueprintFeature LuckDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("d4e192475bb1a1045859c7664addd461");
            public static BlueprintFeature MadnessDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("c346bcc77a6613040b3aa915b1ceddec");
            public static BlueprintFeature MagicDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("08a5686378a87b64399d329ba4ef71b8");
            public static BlueprintFeature MurderDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("366b00b9fcc54b188768b2dd91e3643d");
            public static BlueprintFeature NobilityDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("e0471d01e73254a4ca23278705b75e57");
            public static BlueprintFeature PlantDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("0e03c2a03222b0b42acf96096b286327");
            public static BlueprintFeature ProtectionDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("d4ce7592bd12d63439907ad64e986e59");
            public static BlueprintFeature ReposeDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("076ba1e3a05fac146acfc956a9f41e95");
            public static BlueprintFeature RuneDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("77637f81d6aa33b4f82873d7934e8c4b");
            public static BlueprintFeature ScalykindDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("c8326083da8e40c4931f473bfa70f9f0");
            public static BlueprintFeature StrengthDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("58d2867520de17247ac6988a31f9e397");
            public static BlueprintFeature SunDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("e28412c548ff21a49ac5b8b792b0aa9b");
            public static BlueprintFeature TravelDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("c008853fe044bd442ae8bd22260592b7");
            public static BlueprintFeature TrickeryDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("eaa368e08628a8641b16cd41cbd2cb33");
            public static BlueprintFeature UndeadDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("060dc9e6dea044a2a478e36545adfadc");
            public static BlueprintFeature WarDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("3795653d6d3b291418164b27be88cb43");
            public static BlueprintFeature WaterDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("8f49469c40e2c6e4db61296558e08966");
            public static BlueprintFeature WeatherDomainAllowed => Resources.GetBlueprint<BlueprintFeature>("9dfdfd4904e98fa48b80c8f63ec2cf11");
            //Modded domains
            public static BlueprintFeature AgathionDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("AgathionDomainAllowed");
            public static BlueprintFeature ArcaneDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("ArcaneDomainAllowed");
            public static BlueprintFeature ArchonDomainGoodAllowed => Resources.GetModBlueprint<BlueprintFeature>("ArchonDomainGoodAllowed");
            public static BlueprintFeature ArchonDomainLawAllowed => Resources.GetModBlueprint<BlueprintFeature>("ArchonDomainLawAllowed");
            public static BlueprintFeature AshDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("AshDomainAllowed");
            public static BlueprintFeature BloodDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("BloodDomainAllowed");
            public static BlueprintFeature CavesDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("CavesDomainAllowed");
            public static BlueprintFeature CurseDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("CurseDomainAllowed");
            public static BlueprintFeature DefenseDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("DefenseDomainAllowed");
            public static BlueprintFeature DemonDomainChaosAllowed => Resources.GetModBlueprint<BlueprintFeature>("DemonDomainChaosAllowed");
            public static BlueprintFeature DemonDomainEvilAllowed => Resources.GetModBlueprint<BlueprintFeature>("DemonDomainEvilAllowed");
            public static BlueprintFeature DivineDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("DivineDomainAllowed");
            public static BlueprintFeature DragonDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("DragonDomainAllowed");
            public static BlueprintFeature DuelsDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("DuelsDomainAllowed");
            public static BlueprintFeature FerocityDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("FerocityDomainAllowed");
            public static BlueprintFeature FistDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("FistDomainAllowed");
            public static BlueprintFeature FurDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("FurDomainAllowed");
            public static BlueprintFeature GrowthDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("GrowthDomainAllowed");
            public static BlueprintFeature HeroismDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("HeroismDomainAllowed");
            public static BlueprintFeature InsanityDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("InsanityDomainAllowed");
            public static BlueprintFeature InsectDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("InsectDomainAllowed");
            public static BlueprintFeature OldIceDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("IceDomainAllowed");
            public static BlueprintFeature LightningDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("LightningDomainAllowed");
            public static BlueprintFeature LoyaltyDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("LoyaltyDomainAllowed");
            public static BlueprintFeature LustDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("LustDomainAllowed");
            public static BlueprintFeature PsychopompDomainDeathAllowed => Resources.GetModBlueprint<BlueprintFeature>("PsychopompDomainDeathAllowed");
            public static BlueprintFeature PsychopompDomainReposeAllowed => Resources.GetModBlueprint<BlueprintFeature>("PsychopompDomainReposeAllowed");
            public static BlueprintFeature RageDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("RageDomainAllowed");
            public static BlueprintFeature ResolveDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("ResolveDomainAllowed");
            public static BlueprintFeature RestorationDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("RestorationDomainAllowed");
            public static BlueprintFeature RevelationDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("RevelationDomainAllowed");
            public static BlueprintFeature RevolutionDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("RevolutionDomainAllowed");
            public static BlueprintFeature RiversDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("RiversDomainAllowed");
            public static BlueprintFeature OldScalykindDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("ScalykindDomainAllowed");
            public static BlueprintFeature SmokeDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("SmokeDomainAllowed");
            public static BlueprintFeature StarsDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("StarsDomainAllowed");
            public static BlueprintFeature StormDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("StormDomainAllowed");
            public static BlueprintFeature ThieveryDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("ThieveryDomainAllowed");
            public static BlueprintFeature OldUndeadDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("UndeadDomainAllowed");
            public static BlueprintFeature WhimsyDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("WhimsyDomainAllowed");
            public static BlueprintFeature WindDomainAllowed => Resources.GetModBlueprint<BlueprintFeature>("WindDomainAllowed");
        }
        public static class SeparatistDomainAllowed {
            public static BlueprintFeature AirDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("fa67965152634a16bf619ad0e72d57ff");
            public static BlueprintFeature AnimalDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("617b8a3d83bc45e391b53ebc13f9e5b1");
            public static BlueprintFeature ArtificeDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("2ab5269971064d37bb26a89ab0dd7c08");
            public static BlueprintFeature ChaosDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("da3be0f595ab46e29a010afdc8f6c858");
            public static BlueprintFeature CharmDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("473a037df8ed4919ab4ccb605f884f54");
            public static BlueprintFeature CommunityDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("c91daec2470c4e87a2833631d2eee347");
            public static BlueprintFeature DarknessDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("94d955787f7248e99504bb2f22297d41");
            public static BlueprintFeature DeathDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("2a97721a11434058a530c70ff73b640c");
            public static BlueprintFeature DestructionDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("0a53b9d715b54242bf84fe105d6f0401");
            public static BlueprintFeature EarthDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("0ceca50b20394281af0a1245b2007dec");
            public static BlueprintFeature EvilDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("33d69c06ca4c4afd94df5bdda511c508");
            public static BlueprintFeature FireDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("10e69991b2c043b69287c3b6a07fec94");
            public static BlueprintFeature GloryDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("f803ea6e161d4a789187d44f8d09fd90");
            public static BlueprintFeature GoodDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("7fd23a5b7fc84e59a71b69289e468294");
            public static BlueprintFeature HealingDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("4a352fc8440544f3b58b4e91f6e0f551");
            public static BlueprintFeature IceDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("76e69d9915454f238c6b0eea0db62703");
            public static BlueprintFeature KnowledgeDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("259a3e560c1b48ab899d878f3332f8ba");
            public static BlueprintFeature LawDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("c0f4e109353b4ddc875cf8a42faa63cd");
            public static BlueprintFeature LiberationDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("31a283dacb484afc9ef8d69979c24d0c");
            public static BlueprintFeature LuckDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("b99c38f89c014eaeab5543c00d795721");
            public static BlueprintFeature MadnessDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("bfbc42a8c94a4d4b9f424565ec97c755");
            public static BlueprintFeature MagicDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("0cdeca405da848d49f922a9422fefa3c");
            public static BlueprintFeature MurderDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("662cf0a1c4654e44b9dad4d8c2faefd3");
            public static BlueprintFeature NobilityDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("c3543d3da9774e81808a2bbe721a2174");
            public static BlueprintFeature PlantDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("71dcf73a8ca0457da97e66ac0d663c42");
            public static BlueprintFeature ProtectionDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("5d8dc80eb8714ad2a14353a0e3cbd20c");
            public static BlueprintFeature ReposeDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("928ec21e21fd471cac25bec3ea3dfaa9");
            public static BlueprintFeature RuneDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("5cf9bc2be3ba4a56a1e17c10f1b5d693");
            public static BlueprintFeature ScalykindDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("4448c8005c854a72a0853319171f29e6");
            public static BlueprintFeature StrengthDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("f07500d237ac4ac9b10860b72a25e8f1");
            public static BlueprintFeature SunDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("10958fa969f84b6486271b9559016058");
            public static BlueprintFeature TravelDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("deb547decbf845b6aaa176c2717723c8");
            public static BlueprintFeature TrickeryDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("085b645950fa492c85b3a3a2fffd6fd6");
            public static BlueprintFeature UndeadDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("b8ea7061fb744142a6c76bc70b5ff74d");
            public static BlueprintFeature WarDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("0535f677358f4f3bb7cd8c5c4b1a58f5");
            public static BlueprintFeature WaterDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("87b27e3d011f4caea0e2679cbd90e7f7");
            public static BlueprintFeature WeatherDomainAllowedSeparatist => Resources.GetBlueprint<BlueprintFeature>("8bc3629d677f48798235b94cc9ba0f36");
            //Modded domains
            public static BlueprintFeature AgathionDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("AgathionDomainAllowedSeparatist");
            public static BlueprintFeature ArcaneDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("ArcaneDomainAllowedSeparatist");
            public static BlueprintFeature ArchonDomainGoodAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("ArchonDomainGoodAllowedSeparatist");
            public static BlueprintFeature ArchonDomainLawAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("ArchonDomainLawAllowedSeparatist");
            public static BlueprintFeature AshDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("AshDomainAllowedSeparatist");
            public static BlueprintFeature BloodDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("BloodDomainAllowedSeparatist");
            public static BlueprintFeature CavesDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("CavesDomainAllowedSeparatist");
            public static BlueprintFeature CurseDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("CurseDomainAllowedSeparatist");
            public static BlueprintFeature DefenseDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("DefenseDomainAllowedSeparatist");
            public static BlueprintFeature DemonDomainChaosAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("DemonDomainChaosAllowedSeparatist");
            public static BlueprintFeature DemonDomainEvilAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("DemonDomainEvilAllowedSeparatist");
            public static BlueprintFeature DivineDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("DivineDomainAllowedSeparatist");
            public static BlueprintFeature DragonDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("DragonDomainAllowedSeparatist");
            public static BlueprintFeature DuelsDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("DuelsDomainAllowedSeparatist");
            public static BlueprintFeature FerocityDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("FerocityDomainAllowedSeparatist");
            public static BlueprintFeature FistDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("FistDomainAllowedSeparatist");
            public static BlueprintFeature FurDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("FurDomainAllowedSeparatist");
            public static BlueprintFeature GrowthDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("GrowthDomainAllowedSeparatist");
            public static BlueprintFeature HeroismDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("HeroismDomainAllowedSeparatist");
            public static BlueprintFeature InsanityDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("InsanityDomainAllowedSeparatist");
            public static BlueprintFeature InsectDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("InsectDomainAllowedSeparatist");
            //public static BlueprintFeature IceDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("IceDomainAllowedSeparatist");
            public static BlueprintFeature LightningDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("LightningDomainAllowedSeparatist");
            public static BlueprintFeature LoyaltyDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("LoyaltyDomainAllowedSeparatist");
            public static BlueprintFeature LustDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("LustDomainAllowedSeparatist");
            public static BlueprintFeature PsychopompDomainDeathAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("PsychopompDomainDeathAllowedSeparatist");
            public static BlueprintFeature PsychopompDomainReposeAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("PsychopompDomainReposeAllowedSeparatist");
            public static BlueprintFeature RageDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("RageDomainAllowedSeparatist");
            public static BlueprintFeature ResolveDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("ResolveDomainAllowedSeparatist");
            public static BlueprintFeature RestorationDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("RestorationDomainAllowedSeparatist");
            public static BlueprintFeature RevelationDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("RevelationDomainAllowedSeparatist");
            public static BlueprintFeature RevolutionDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("RevolutionDomainAllowedSeparatist");
            public static BlueprintFeature RiversDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("RiversDomainAllowedSeparatist");
            //public static BlueprintFeature ScalykindDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("ScalykindDomainAllowedSeparatist");
            public static BlueprintFeature SmokeDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("SmokeDomainAllowedSeparatist");
            public static BlueprintFeature StarsDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("StarsDomainAllowedSeparatist");
            public static BlueprintFeature StormDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("StormDomainAllowedSeparatist");
            public static BlueprintFeature ThieveryDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("ThieveryDomainAllowedSeparatist");
            //public static BlueprintFeature UndeadDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("UndeadDomainAllowedSeparatist");
            public static BlueprintFeature WhimsyDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("WhimsyDomainAllowedSeparatist");
            public static BlueprintFeature WindDomainAllowedSeparatist => Resources.GetModBlueprint<BlueprintFeature>("WindDomainAllowedSeparatist");


        }
        public static void SetAllowedDomains(this BlueprintFeature deity, params BlueprintFeature[] domains) {
            deity.AddComponent<AddFacts>(c => {
                c.m_Facts = domains.Select(f => f.ToReference<BlueprintUnitFactReference>()).ToArray();
            });
        }
        public static void SetDisallowedArchetype(this BlueprintFeature deity, BlueprintCharacterClass characterClass, BlueprintArchetype archetype) {
            deity.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = characterClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = archetype.ToReference<BlueprintArchetypeReference>();
            });
        }
        public static void DisallowAngelfireApostle(this BlueprintFeature deity) {
            BlueprintCharacterClassReference ClericClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("67819271767a9dd4fbfd4ae700befea0");
            BlueprintArchetypeReference AngelfireApostleArchetype = Resources.GetBlueprintReference<BlueprintArchetypeReference>("857bc9fadf70f294795a9cba974a48b8");
            deity.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = ClericClass;
                c.m_Archetype = AngelfireApostleArchetype;
            });
        }
        public static void DisallowDarkSister(this BlueprintFeature deity) {
            BlueprintCharacterClassReference WitchClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("1b9873f1e7bfe5449bc84d03e9c8e3cc");
            BlueprintArchetypeReference DarkSisterArchetype = Resources.GetBlueprintReference<BlueprintArchetypeReference>("411fa458481e44f0855d47a19358874b");
            deity.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = WitchClass;
                c.m_Archetype = DarkSisterArchetype;
            });
        }
        public static void DisallowProphetOfPestilence(this BlueprintFeature deity) {
            BlueprintCharacterClassReference ShamanClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("145f1d3d360a7ad48bd95d392c81b38e");
            BlueprintArchetypeReference ProphetOfPestilenceArchetype = Resources.GetBlueprintReference<BlueprintArchetypeReference>("bcd758a75fb54651a7f668fe2661a307");
            deity.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = ShamanClass;
                c.m_Archetype = ProphetOfPestilenceArchetype;
            });
        }
        public static void DisallowNewMantisZealot(this BlueprintFeature deity) {
            BlueprintCharacterClassReference WarpriestClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("30b5e47d47a0e37438cc5a80c96cfb99");
            BlueprintArchetypeReference NewMantisZealotArchetype = Resources.GetBlueprintReference<BlueprintArchetypeReference>("fc403240fdca4a52a413578169ea7117");
            deity.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = WarpriestClass;
                c.m_Archetype = NewMantisZealotArchetype;
            });
        }
        public static void DisallowElderMythosCultist(this BlueprintFeature deity) {//For Homebrew Archetypes, only use if HA is detected
            BlueprintCharacterClassReference ClericClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("67819271767a9dd4fbfd4ae700befea0");
            BlueprintArchetypeReference ElderMythosCultistArchetype = Resources.GetBlueprintReference<BlueprintArchetypeReference>("b7b9138f2e19b9f45a6ca457f6467710");
            deity.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = ClericClass;
                c.m_Archetype = ElderMythosCultistArchetype;
            });
        }
        public static void AddSacredWeapon(this BlueprintFeature feature, WeaponCategory weapon) {
            BlueprintFeatureReference WeaponFeature = Resources.GetBlueprintReference<BlueprintFeatureReference>("bae328694ee21bc4587c5ffd0035b6a2");
            BlueprintBuffReference Buff1d6 = Resources.GetBlueprintReference<BlueprintBuffReference>("75a3eaf9cff6acd4a8385ba91ffa329a");
            BlueprintBuffReference Buff1d8 = Resources.GetBlueprintReference<BlueprintBuffReference>("06ad6a85cfd5b694c88bdc0eabf8ba16");
            BlueprintBuffReference Buff1d10 = Resources.GetBlueprintReference<BlueprintBuffReference>("e62b125c9f49b084b95a487a6bfb1b7c");
            BlueprintBuffReference Buff2d6 = Resources.GetBlueprintReference<BlueprintBuffReference>("2ffb1848e52e6144d8bb3536c024366e");
            BlueprintBuffReference Buff2d8 = Resources.GetBlueprintReference<BlueprintBuffReference>("a3a655c948afb674882577674644e816");
            feature.AddComponent<SacredWeaponFavoriteDamageOverride>(c => {
                c.Category = weapon;
                c.m_DeaitySacredWeaponFeature = WeaponFeature;
                c.m_Buff1d6 = Buff1d6;
                c.m_Buff1d8 = Buff1d8;
                c.m_Buff1d10 = Buff1d10;
                c.m_Buff2d6 = Buff2d6;
                c.m_Buff2d8 = Buff2d8;
            });
        }
        public static void LazySacredWeaponMaker(string deityname, BlueprintFeature deity, params WeaponCategory[] weapon) {
            BlueprintFeature WeaponFeature = Resources.GetBlueprint<BlueprintFeature>("bae328694ee21bc4587c5ffd0035b6a2");
            var sacredweaponsfeature = Helpers.CreateBlueprint<BlueprintFeature>($"{deityname}SacredWeaponFeature", bp => {
                foreach (var weaponcategory in weapon) { bp.AddSacredWeapon(weaponcategory); }
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
            });
            deity.AddComponent<AddFeatureIfHasFact>(c => {
                c.m_CheckedFact = WeaponFeature.ToReference<BlueprintUnitFactReference>();
                c.m_Feature = sacredweaponsfeature.ToReference<BlueprintUnitFactReference>();
                c.Not = false;
            });
            WeaponFeature.AddComponent<AddFeatureIfHasFact>(c => {
                c.m_CheckedFact = deity.ToReference<BlueprintUnitFactReference>();
                c.m_Feature = sacredweaponsfeature.ToReference<BlueprintUnitFactReference>();
                c.Not = false;
            });
        }
        public static void MagicDeceiverLock(this BlueprintFeature deity) {
            BlueprintProgression LivingGodProgression = Resources.GetBlueprint<BlueprintProgression>("464ffaf88140474a949618bec7955e78");
            BlueprintProgression RazmiriInfiltratorProgression = Resources.GetBlueprint<BlueprintProgression>("cd96109ca30b45dcb6d7390945c8487d");
            deity.AddComponent<PrerequisiteNoFeature>(c => {
                c.m_Feature = LivingGodProgression.ToReference<BlueprintFeatureReference>();
            });
            deity.AddComponent<PrerequisiteNoFeature>(c => {
                c.m_Feature = RazmiriInfiltratorProgression.ToReference<BlueprintFeatureReference>();
            });
        }
        public static void DisallowSoldierOfGaia(this BlueprintFeature deity) {
            BlueprintCharacterClassReference WarpriestClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("30b5e47d47a0e37438cc5a80c96cfb99");
            BlueprintArchetype SoldierOfGaiaArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SoldierOfGaiaArchetype");
            deity.AddComponent<PrerequisiteNoArchetype>(c => {
                c.HideInUI = true;
                c.m_CharacterClass = WarpriestClass;
                c.m_Archetype = SoldierOfGaiaArchetype.ToReference<BlueprintArchetypeReference>();
            });
        }
    }
}
