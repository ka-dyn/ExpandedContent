using HarmonyLib;
using ExpandedContent;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;
using System;
using ExpandedContent.Extensions;
using ExpandedContent.Config;
using Kingmaker.UnitLogic.FactLogic;

namespace ExpandedContent.Tweaks.Domains {
    // Adding new domains to base game deities
    internal class BaseDeityPatch {
        public static void AddBaseDeityPatch() {
            //Deities
            var AbadarFeature = Resources.GetBlueprint<BlueprintFeature>("6122dacf418611540a3c91e67197ee4e");
            var AsmodeusFeature = Resources.GetBlueprint<BlueprintFeature>("a3a5ccc9c670e6f4ca4a686d23b89900");
            var CalistriaFeature = Resources.GetBlueprint<BlueprintFeature>("c7531715a3f046d4da129619be63f44c");
            var CaydenCaileanFeature = Resources.GetBlueprint<BlueprintFeature>("300e212868bca984687c92bcb66d381b");
            var DesnaFeature = Resources.GetBlueprint<BlueprintFeature>("2c0a3b9971327ba4d9d85354d16998c1");
            var ErastilFeature = Resources.GetBlueprint<BlueprintFeature>("afc775188deb7a44aa4cbde03512c671");
            var GorumFeature = Resources.GetBlueprint<BlueprintFeature>("8f49a5d8528a82c44b8c117a89f6b68c");
            var GozrehFeature = Resources.GetBlueprint<BlueprintFeature>("4af983eec2d821b40a3065eb5e8c3a72");
            var GyronnaFeature = Resources.GetBlueprint<BlueprintFeature>("8b535b6842e063d48a571a042c3c6e8f");
            var IomedaeFeature = Resources.GetBlueprint<BlueprintFeature>("88d5da04361b16746bf5b65795e0c38c");
            var IroriFeature = Resources.GetBlueprint<BlueprintFeature>("23a77a5985de08349820429ce1b5a234");
            var LamashtuFeature = Resources.GetBlueprint<BlueprintFeature>("f86bc8fbf13221f4f9041608a1fb8585");
            var NethysFeature = Resources.GetBlueprint<BlueprintFeature>("6262cfce7c31626458325ca0909de997");
            var NorgorberFeature = Resources.GetBlueprint<BlueprintFeature>("805b6bdc8c96f4749afc687a003f9628");
            var PharasmaFeature = Resources.GetBlueprint<BlueprintFeature>("458750bc214ab2e44abdeae404ab22e9");
            var RovagugFeature = Resources.GetBlueprint<BlueprintFeature>("04bc2b62273ab744092d992ed72bff41");
            var SarenraeFeature = Resources.GetBlueprint<BlueprintFeature>("c1c4f7f64842e7e48849e5e67be11a1b");
            var ShelynFeature = Resources.GetBlueprint<BlueprintFeature>("b382afa31e4287644b77a8b30ed4aa0b");
            var ToragFeature = Resources.GetBlueprint<BlueprintFeature>("d2d5c5a58885a6b489727467e13c3337");
            var UrgathoaFeature = Resources.GetBlueprint<BlueprintFeature>("812f6c07148088e41a9ac94b56ac2fc8");
            var ZonKuthonFeature = Resources.GetBlueprint<BlueprintFeature>("f7eed400baa66a744ad361d4df0e6f1b");
            var GodClawFeature = Resources.GetBlueprint<BlueprintFeature>("583a26e88031d0a4a94c8180105692a5");
            //Domains
            var AgathionDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("AgathionDomainAllowed");
            var ArchonDomainGoodAllowed = Resources.GetModBlueprint<BlueprintFeature>("ArchonDomainGoodAllowed");
            var ArchonDomainLawAllowed = Resources.GetModBlueprint<BlueprintFeature>("ArchonDomainLawAllowed");
            var BloodDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("BloodDomainAllowed");
            var CavesDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("CavesDomainAllowed");
            var CurseDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("CurseDomainAllowed");
            var DemonDomainChaosAllowed = Resources.GetModBlueprint<BlueprintFeature>("DemonDomainChaosAllowed");
            var DemonDomainEvilAllowed = Resources.GetModBlueprint<BlueprintFeature>("DemonDomainEvilAllowed");
            var FerocityDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("FerocityDomainAllowed");
            var FurDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("FurDomainAllowed");
            var IceDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("IceDomainAllowed");
            var LustDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("LustDomainAllowed");
            var PsychopompDomainDeathAllowed = Resources.GetModBlueprint<BlueprintFeature>("PsychopompDomainDeathAllowed");
            var PsychopompDomainReposeAllowed = Resources.GetModBlueprint<BlueprintFeature>("PsychopompDomainReposeAllowed");
            var RageDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("RageDomainAllowed");
            var ResolveDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("ResolveDomainAllowed");
            var RestorationDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("RestorationDomainAllowed");
            var RevelationDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("RevelationDomainAllowed");
            var RevolutionDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("RevolutionDomainAllowed");
            var StormDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("StormDomainAllowed");
            var ThieveryDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("ThieveryDomainAllowed");
            var UndeadDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("UndeadDomainAllowed");
            var WindDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("WindDomainAllowed");

            CaydenCaileanFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { FerocityDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            CaydenCaileanFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { LustDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            CaydenCaileanFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { ResolveDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            CalistriaFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { CurseDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            CalistriaFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { LustDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            CalistriaFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { ThieveryDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            DesnaFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { CurseDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            DesnaFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { RevolutionDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            ErastilFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { ArchonDomainGoodAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            ErastilFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { ArchonDomainLawAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            ErastilFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { FurDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            GorumFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { BloodDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            GorumFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { FerocityDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            GorumFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { RageDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            GorumFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { ResolveDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            GozrehFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { WindDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            GyronnaFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { DemonDomainChaosAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            GyronnaFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { DemonDomainEvilAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            GyronnaFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { RageDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            IomedaeFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { ArchonDomainGoodAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            IomedaeFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { ArchonDomainLawAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            IomedaeFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { RevelationDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            IroriFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { ResolveDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            IroriFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { RestorationDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            LamashtuFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { DemonDomainChaosAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            LamashtuFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { DemonDomainEvilAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            LamashtuFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { FerocityDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            LamashtuFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { ThieveryDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            NorgorberFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { ThieveryDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            PharasmaFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { IceDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            PharasmaFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { PsychopompDomainDeathAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            PharasmaFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { PsychopompDomainReposeAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            RovagugFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { BloodDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            RovagugFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { DemonDomainChaosAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            RovagugFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { DemonDomainEvilAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            RovagugFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { RageDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            RovagugFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { StormDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            SarenraeFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { RestorationDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            SarenraeFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { RevelationDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            SarenraeFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { AgathionDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            ShelynFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { AgathionDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            ToragFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { ArchonDomainGoodAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            ToragFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { ArchonDomainLawAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            ToragFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { CavesDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });            
            UrgathoaFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { BloodDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            UrgathoaFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { UndeadDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            UrgathoaFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { FerocityDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
            ZonKuthonFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[1] { UndeadDomainAllowed.ToReference<BlueprintUnitFactReference>() };
            });
        }
    }
}