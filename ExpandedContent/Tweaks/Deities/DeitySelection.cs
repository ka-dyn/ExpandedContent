using BlueprintCore.Blueprints;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Deities {
    internal class DeitySelectionFeature {

        public static void PatchDeitySelection() {

            //Deities of Ancient Osirion
            var AnubisFeature = Resources.GetModBlueprint<BlueprintFeature>("AnubisFeature");
            var ApepFeature = Resources.GetModBlueprint<BlueprintFeature>("ApepFeature");
            var BastetFeature = Resources.GetModBlueprint<BlueprintFeature>("BastetFeature");
            var BesFeature = Resources.GetModBlueprint<BlueprintFeature>("BesFeature");
            var HathorFeature = Resources.GetModBlueprint<BlueprintFeature>("HathorFeature");
            var HorusFeature = Resources.GetModBlueprint<BlueprintFeature>("HorusFeature");
            var IsisFeature = Resources.GetModBlueprint<BlueprintFeature>("IsisFeature");
            var KhepriFeature = Resources.GetModBlueprint<BlueprintFeature>("KhepriFeature");
            var MaatFeature = Resources.GetModBlueprint<BlueprintFeature>("MaatFeature");
            var NeithFeature = Resources.GetModBlueprint<BlueprintFeature>("NeithFeature");
            var NephthysFeature = Resources.GetModBlueprint<BlueprintFeature>("NephthysFeature");
            var OsirisFeature = Resources.GetModBlueprint<BlueprintFeature>("OsirisFeature");
            var PtahFeature = Resources.GetModBlueprint<BlueprintFeature>("PtahFeature");
            var RaFeature = Resources.GetModBlueprint<BlueprintFeature>("RaFeature");
            var SekhmetFeature = Resources.GetModBlueprint<BlueprintFeature>("SekhmetFeature");
            var SelketFeature = Resources.GetModBlueprint<BlueprintFeature>("SelketFeature");
            var SetFeature = Resources.GetModBlueprint<BlueprintFeature>("SetFeature");
            var SobekFeature = Resources.GetModBlueprint<BlueprintFeature>("SobekFeature");
            var ThothFeature = Resources.GetModBlueprint<BlueprintFeature>("ThothFeature");
            var WadjetFeature = Resources.GetModBlueprint<BlueprintFeature>("WadjetFeature");

            //Deities of Tian Xia
            var DaikitsuFeature = Resources.GetModBlueprint<BlueprintFeature>("DaikitsuFeature");
            var FumeiyoshiFeature = Resources.GetModBlueprint<BlueprintFeature>("FumeiyoshiFeature");
            var GeneralSusumuFeature = Resources.GetModBlueprint<BlueprintFeature>("GeneralSusumuFeature");
            var HeiFengFeature = Resources.GetModBlueprint<BlueprintFeature>("HeiFengFeature");
            var KofusachiFeature = Resources.GetModBlueprint<BlueprintFeature>("KofusachiFeature");
            var LadyNanbyoFeature = Resources.GetModBlueprint<BlueprintFeature>("LadyNanbyoFeature");
            var LaoShuPoFeature = Resources.GetModBlueprint<BlueprintFeature>("LaoShuPoFeature");
            var NalinivatiFeature = Resources.GetModBlueprint<BlueprintFeature>("NalinivatiFeature");
            var QiZhongFeature = Resources.GetModBlueprint<BlueprintFeature>("QiZhongFeature");
            var ShizuruFeature = Resources.GetModBlueprint<BlueprintFeature>("ShizuruFeature");
            var WukongFeature = Resources.GetModBlueprint<BlueprintFeature>("WukongFeature");
            var TsukiyoFeature = Resources.GetModBlueprint<BlueprintFeature>("TsukiyoFeature");
            var YaezhingFeature = Resources.GetModBlueprint<BlueprintFeature>("YaezhingFeature");
            var YamatsumiFeature = Resources.GetModBlueprint<BlueprintFeature>("YamatsumiFeature");

            //Demon Lords
            var ZuraFeature = Resources.GetModBlueprint<BlueprintFeature>("ZuraFeature");
            var DeskariFeature = Resources.GetBlueprint<BlueprintFeature>("ddf913858bdf43b4da3b731e082fbcc0");
            var BaphometFeature = Resources.GetBlueprint<BlueprintFeature>("bd72ca8ffcfec5745899ac56c93f12c5");
            var AreshkegalFeature = Resources.GetBlueprint<BlueprintFeature>("d714ecb5d5bb89a42957de0304e459c9");
            var KabririFeature = Resources.GetBlueprint<BlueprintFeature>("f12c1ccc9d600c04f8887cd28a8f45a5");
            var DagonFeature = Resources.GetModBlueprint<BlueprintFeature>("DagonFeature");
            var TreerazerFeature = Resources.GetModBlueprint<BlueprintFeature>("TreerazerFeature");
            var NocticulaFeature = Resources.GetModBlueprint<BlueprintFeature>("NocticulaFeature");



            //Empyreal Lords
            var ArsheaFeature = Resources.GetModBlueprint<BlueprintFeature>("ArsheaFeature");
            var RagathielFeature = Resources.GetModBlueprint<BlueprintFeature>("RagathielFeature");
            var PuluraFeature = Resources.GetBlueprint<BlueprintFeature>("ebb0b46f95dbac74681c78aae895dbd0");
            var AndolettaFeature = Resources.GetModBlueprint<BlueprintFeature>("AndolettaFeature");
            var ArquerosFeature = Resources.GetModBlueprint<BlueprintFeature>("ArquerosFeature");
            var AshavaFeature = Resources.GetModBlueprint<BlueprintFeature>("AshavaFeature");
            var BharnarolFeature = Resources.GetModBlueprint<BlueprintFeature>("BharnarolFeature");
            var BlackButterflyFeature = Resources.GetModBlueprint<BlueprintFeature>("BlackButterflyFeature");
            var ChadaliFeature = Resources.GetModBlueprint<BlueprintFeature>("ChadaliFeature");
            var ChucaroFeature = Resources.GetModBlueprint<BlueprintFeature>("ChucaroFeature");
            var DammerichFeature = Resources.GetModBlueprint<BlueprintFeature>("DammerichFeature");
            var EritriceFeature = Resources.GetModBlueprint<BlueprintFeature>("EritriceFeature");
            var FalaynaFeature = Resources.GetModBlueprint<BlueprintFeature>("FalaynaFeature");
            var GhenshauFeature = Resources.GetModBlueprint<BlueprintFeature>("GhenshauFeature");
            var HalcamoraFeature = Resources.GetModBlueprint<BlueprintFeature>("HalcamoraFeature");
            var ImmonhielFeature = Resources.GetModBlueprint<BlueprintFeature>("ImmonhielFeature");
            var IrezFeature = Resources.GetModBlueprint<BlueprintFeature>("IrezFeature");
            var JaidzFeature = Resources.GetModBlueprint<BlueprintFeature>("JaidzFeature");
            var JalaijataliFeature = Resources.GetModBlueprint<BlueprintFeature>("JalaijataliFeature");
            var KoradaFeature = Resources.GetModBlueprint<BlueprintFeature>("KoradaFeature");
            var LalaciFeature = Resources.GetModBlueprint<BlueprintFeature>("LalaciFeature");
            var LymnierisFeature = Resources.GetModBlueprint<BlueprintFeature>("LymnierisFeature");
            var OlheonFeature = Resources.GetModBlueprint<BlueprintFeature>("OlheonFeature");
            var PicoperiFeature = Resources.GetModBlueprint<BlueprintFeature>("PicoperiFeature");
            var RowdroshFeature = Resources.GetModBlueprint<BlueprintFeature>("RowdroshFeature");
            var SeramaydielFeature = Resources.GetModBlueprint<BlueprintFeature>("SeramaydielFeature");
            var SheiFeature = Resources.GetModBlueprint<BlueprintFeature>("SheiFeature");
            var SinashaktiFeature = Resources.GetModBlueprint<BlueprintFeature>("SinashaktiFeature");
            var SoralyonFeature = Resources.GetModBlueprint<BlueprintFeature>("SoralyonFeature");
            var TanagaarFeature = Resources.GetModBlueprint<BlueprintFeature>("TanagaarFeature");
            var TolcFeature = Resources.GetModBlueprint<BlueprintFeature>("TolcFeature");
            var ValaniFeature = Resources.GetModBlueprint<BlueprintFeature>("ValaniFeature");
            var VildeisFeature = Resources.GetModBlueprint<BlueprintFeature>("VildeisFeature");
            var WinlasFeature = Resources.GetModBlueprint<BlueprintFeature>("WinlasFeature");
            var YlimanchaFeature = Resources.GetModBlueprint<BlueprintFeature>("YlimanchaFeature");
            var ZohlsFeature = Resources.GetModBlueprint<BlueprintFeature>("ZohlsFeature");

            //Elven Pantheon
            var FindeladlaraFeature = Resources.GetModBlueprint<BlueprintFeature>("FindeladlaraFeature");
            var KetephysFeature = Resources.GetModBlueprint<BlueprintFeature>("KetephysFeature");
            var YuelralFeature = Resources.GetModBlueprint<BlueprintFeature>("YuelralFeature");

            //Gods and Goddesses
            var MilaniFeature = Resources.GetModBlueprint<BlueprintFeature>("MilaniFeature");
            var ErastilFeature = Resources.GetBlueprint<BlueprintFeature>("afc775188deb7a44aa4cbde03512c671");
            var IroriFeature = Resources.GetBlueprint<BlueprintFeature>("23a77a5985de08349820429ce1b5a234");
            var IomedaeFeature = Resources.GetBlueprint<BlueprintFeature>("88d5da04361b16746bf5b65795e0c38c");
            var ToragFeature = Resources.GetBlueprint<BlueprintFeature>("d2d5c5a58885a6b489727467e13c3337");
            var ShelynFeature = Resources.GetBlueprint<BlueprintFeature>("b382afa31e4287644b77a8b30ed4aa0b");
            var SarenraeFeature = Resources.GetBlueprint<BlueprintFeature>("c1c4f7f64842e7e48849e5e67be11a1b");
            var AbadarFeature = Resources.GetBlueprint<BlueprintFeature>("6122dacf418611540a3c91e67197ee4e");
            var GorumFeature = Resources.GetBlueprint<BlueprintFeature>("8f49a5d8528a82c44b8c117a89f6b68c");
            var GozrehFeature = Resources.GetBlueprint<BlueprintFeature>("4af983eec2d821b40a3065eb5e8c3a72");
            var GyronnaFeature = Resources.GetBlueprint<BlueprintFeature>("8b535b6842e063d48a571a042c3c6e8f");
            var LamashtuFeature = Resources.GetBlueprint<BlueprintFeature>("f86bc8fbf13221f4f9041608a1fb8585");
            var LichDeityFeature = Resources.GetBlueprint<BlueprintFeature>("b4153b422d02d4f48b3f8f0ceb6a10eb");
            var AsmodeusFeature = Resources.GetBlueprint<BlueprintFeature>("a3a5ccc9c670e6f4ca4a686d23b89900");
            var NethysFeature = Resources.GetBlueprint<BlueprintFeature>("6262cfce7c31626458325ca0909de997");
            var NorgorberFeature = Resources.GetBlueprint<BlueprintFeature>("805b6bdc8c96f4749afc687a003f9628");
            var PharasmaFeature = Resources.GetBlueprint<BlueprintFeature>("458750bc214ab2e44abdeae404ab22e9");
            var RovagugFeature = Resources.GetBlueprint<BlueprintFeature>("04bc2b62273ab744092d992ed72bff41");
            var UrgathoaFeature = Resources.GetBlueprint<BlueprintFeature>("812f6c07148088e41a9ac94b56ac2fc8");
            var ZonKuthonFeature = Resources.GetBlueprint<BlueprintFeature>("f7eed400baa66a744ad361d4df0e6f1b");
            var CalistriaFeature = Resources.GetBlueprint<BlueprintFeature>("c7531715a3f046d4da129619be63f44c");
            var CaydenCaileanFeature = Resources.GetBlueprint<BlueprintFeature>("300e212868bca984687c92bcb66d381b");
            var DesnaFeature = Resources.GetBlueprint<BlueprintFeature>("2c0a3b9971327ba4d9d85354d16998c1");
            var BesmaraFeature = Resources.GetModBlueprint<BlueprintFeature>("BesmaraFeature");
            var AchaekekFeature = Resources.GetModBlueprint<BlueprintFeature>("AchaekekFeature");
            var AlsetaFeature = Resources.GetModBlueprint<BlueprintFeature>("AlsetaFeature");
            var ZyphusFeature = Resources.GetModBlueprint<BlueprintFeature>("ZyphusFeature");
            var KurgessFeature = Resources.GetModBlueprint<BlueprintFeature>("KurgessFeature");
            var YdersiusFeature = Resources.GetModBlueprint<BlueprintFeature>("YdersiusFeature");


            //Philosophies
            var GreenFaithFeature = Resources.GetBlueprint<BlueprintFeature>("99a7a8f13c1300c42878558fa9471e2f");
            var AtheismFeature = Resources.GetBlueprint<BlueprintFeature>("92c0d2da0a836ce418a267093c09ca54");

            //Pantheons
            var GodclawFeature = Resources.GetBlueprint<BlueprintFeature>("583a26e88031d0a4a94c8180105692a5");

            //Dragon Gods/Aspects
            var ApsuFeature = Resources.GetModBlueprint<BlueprintFeature>("ApsuFeature");
            var DahakFeature = Resources.GetModBlueprint<BlueprintFeature>("DahakFeature");

            //Archdevils
            var MephistophelesFeature = Resources.GetModBlueprint<BlueprintFeature>("MephistophelesFeature");
            var DispaterFeature = Resources.GetModBlueprint<BlueprintFeature>("DispaterFeature");

            //The Eldest
            var CountRanalcFeature = Resources.GetModBlueprint<BlueprintFeature>("CountRanalcFeature");
            var TheGreenMotherFeature = Resources.GetModBlueprint<BlueprintFeature>("TheGreenMotherFeature");
            var ImbrexFeature = Resources.GetModBlueprint<BlueprintFeature>("ImbrexFeature");
            var TheLanternKingFeature = Resources.GetModBlueprint<BlueprintFeature>("TheLanternKingFeature");
            var TheLostPrinceFeature = Resources.GetModBlueprint<BlueprintFeature>("TheLostPrinceFeature");
            var MagdhFeature = Resources.GetModBlueprint<BlueprintFeature>("MagdhFeature");
            var NgFeature = Resources.GetModBlueprint<BlueprintFeature>("NgFeature");
            var RagadahnFeature = Resources.GetModBlueprint<BlueprintFeature>("RagadahnFeature");
            var ShykaFeature = Resources.GetModBlueprint<BlueprintFeature>("ShykaFeature");

            //Monitors
            var AtroposFeature = Resources.GetModBlueprint<BlueprintFeature>("AtroposFeature");
            var BarzahkFeature = Resources.GetModBlueprint<BlueprintFeature>("BarzahkFeature");
            var CeyannanFeature = Resources.GetModBlueprint<BlueprintFeature>("CeyannanFeature");
            var KerkamothFeature = Resources.GetModBlueprint<BlueprintFeature>("KerkamothFeature");
            var MonadFeature = Resources.GetModBlueprint<BlueprintFeature>("MonadFeature");
            var SsilameshnikFeature = Resources.GetModBlueprint<BlueprintFeature>("SsilameshnikFeature");



            var DeitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("59e7a76987fe3b547b9cce045f4db3e4");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            
            var DeitiesofAncientOsirionIcon = AssetLoader.LoadInternal("Deities", "Icon_DeitiesofAncientOsirion.jpg");
            var DeitiesofAncientOsirionSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DeitiesofAncientOsirionSelection", bp => {
                bp.SetName("Deities of Ancient Osirion");
                bp.SetDescription("Since the Age of Destiny, the people of Osirion have worshiped their own local gods, in addition " +
                    "to those deities venerated throughout the Inner Sea region. They were most popular during the early days of Osirion, " +
                    "but their faith waned as the Osirian people gradually turned to the worship of foreign deities. When Osirion was under " +
                    "Keleshite rule, the foreign overlords sought to eradicate the faith in the indigenous gods, but they remain a part of the " +
                    "history of Osirion's land and people, and with the restoration of native Osirian rule, interest in these ancient divinities " +
                    "has been rekindled.");
                bp.m_Icon = DeitiesofAncientOsirionIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                AnubisFeature.ToReference<BlueprintFeatureReference>(),
                ApepFeature.ToReference<BlueprintFeatureReference>(),
                BastetFeature.ToReference<BlueprintFeatureReference>(),
                BesFeature.ToReference<BlueprintFeatureReference>(),
                HathorFeature.ToReference<BlueprintFeatureReference>(),
                HorusFeature.ToReference<BlueprintFeatureReference>(),
                IsisFeature.ToReference<BlueprintFeatureReference>(),
                KhepriFeature.ToReference<BlueprintFeatureReference>(),
                MaatFeature.ToReference<BlueprintFeatureReference>(),
                NeithFeature.ToReference<BlueprintFeatureReference>(),
                NephthysFeature.ToReference<BlueprintFeatureReference>(),
                OsirisFeature.ToReference<BlueprintFeatureReference>(),
                PtahFeature.ToReference<BlueprintFeatureReference>(),
                RaFeature.ToReference<BlueprintFeatureReference>(),
                SekhmetFeature.ToReference<BlueprintFeatureReference>(),
                SelketFeature.ToReference<BlueprintFeatureReference>(),
                SetFeature.ToReference<BlueprintFeatureReference>(),
                SobekFeature.ToReference<BlueprintFeatureReference>(),
                ThothFeature.ToReference<BlueprintFeatureReference>(),
                WadjetFeature.ToReference<BlueprintFeatureReference>(),
                };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var DeitiesofTianXiaIcon = AssetLoader.LoadInternal("Deities", "Icon_DeitiesofTianXia.jpg");
            var DeitiesofTianXiaSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DeitiesofTianXiaSelection", bp => {
                bp.SetName("Deities of Tian Xia");
                bp.SetDescription("Just as in the lands of the Inner Sea, religion and faith play a vital role in the daily lives of the people " +
                    "of the Dragon Empires. And while there are deities and powerful extraplanar beings beyond count in search of followers, the " +
                    "faiths of 14 gods are particularly strong in Tian Xia");
                bp.m_Icon = DeitiesofTianXiaIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                DaikitsuFeature.ToReference<BlueprintFeatureReference>(),
                FumeiyoshiFeature.ToReference<BlueprintFeatureReference>(),
                GeneralSusumuFeature.ToReference<BlueprintFeatureReference>(),
                HeiFengFeature.ToReference<BlueprintFeatureReference>(),
                LadyNanbyoFeature.ToReference<BlueprintFeatureReference>(),
                LaoShuPoFeature.ToReference<BlueprintFeatureReference>(),
                NalinivatiFeature.ToReference<BlueprintFeatureReference>(),
                QiZhongFeature.ToReference<BlueprintFeatureReference>(),
                ShizuruFeature.ToReference<BlueprintFeatureReference>(),
                WukongFeature.ToReference<BlueprintFeatureReference>(),
                TsukiyoFeature.ToReference<BlueprintFeatureReference>(),
                YaezhingFeature.ToReference<BlueprintFeatureReference>(),
                YamatsumiFeature.ToReference<BlueprintFeatureReference>(),
                };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var DemonLordsIcon = AssetLoader.LoadInternal("Deities", "Icon_DemonLords.jpg");
            var DemonLordSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DemonLordSelection", bp => {
                bp.SetName("Demon Lords");
                bp.SetDescription("A demon lord is a very powerful and unique demon. They are, by definition, rulers of at least " +
                    "one layer of the Abyss, and have hordes of nascent demon lords and lesser demons in their service. Being creatures " +
                    "of chaos, however, not all demons are servants to a demon lord. As the Abyss is nigh infinite, so too are " +
                    "the number of demon lords.");
                bp.m_Icon = DemonLordsIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                AreshkegalFeature.ToReference<BlueprintFeatureReference>(),
                BaphometFeature.ToReference<BlueprintFeatureReference>(),
                DagonFeature.ToReference<BlueprintFeatureReference>(),
                DeskariFeature.ToReference<BlueprintFeatureReference>(),
                KabririFeature.ToReference<BlueprintFeatureReference>(),
                NocticulaFeature.ToReference<BlueprintFeatureReference>(),
                TreerazerFeature.ToReference<BlueprintFeatureReference>(),
                ZuraFeature.ToReference<BlueprintFeatureReference>(),
                };

                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var ArchdevilIcon = AssetLoader.LoadInternal("Deities", "Icon_Archdevils.jpg");
            var ArchdevilSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("ArchdevilSelection", bp => {
                bp.SetName("Archdevils");
                bp.SetDescription("Archdevils are the nine major devils who rule the nine layers of Hell. They are demigods " +
                    "who posses god-like powers (such as the ability to grant spells to their worshipers), but are less " +
                    "powerful than the master of Hell, Asmodeus. The others pay strict obeisance to him, even while they " +
                    "tirelessly scheme against him and each other. Each archdevil represents a different form of punishment " +
                    "for sinful mortals.");
                bp.m_Icon = ArchdevilIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                DispaterFeature.ToReference<BlueprintFeatureReference>(),
                MephistophelesFeature.ToReference<BlueprintFeatureReference>() };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var EmpyrealLordsIcon = AssetLoader.LoadInternal("Deities", "Icon_EmpyrealLords.jpg");
            var EmpyrealLordSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("EmpyrealLordSelection", bp => {
                bp.SetName("Empyreal Lords");
                bp.SetDescription("Empyreal Lords, or the Lords of the Empyrean, are unique outsiders who have transcended their original " +
                    "forms and have acquired a small spark of divinity, becoming demigods. They guide mortals on the various paths to " +
                    "righteousness, and the goddess Sarenrae herself is said to have risen from their ranks. Angels, archons, azatas, and " +
                    "powerful agathions known as agathion leaders have all entered the ranks of empyreal lords.");
                bp.m_Icon = EmpyrealLordsIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                AndolettaFeature.ToReference<BlueprintFeatureReference>(),
                ArquerosFeature.ToReference<BlueprintFeatureReference>(),
                ArsheaFeature.ToReference<BlueprintFeatureReference>(),
                AshavaFeature.ToReference<BlueprintFeatureReference>(),
                BharnarolFeature.ToReference<BlueprintFeatureReference>(),
                BlackButterflyFeature.ToReference<BlueprintFeatureReference>(),
                ChadaliFeature.ToReference<BlueprintFeatureReference>(),
                ChucaroFeature.ToReference<BlueprintFeatureReference>(),
                DammerichFeature.ToReference<BlueprintFeatureReference>(),
                EritriceFeature.ToReference<BlueprintFeatureReference>(),
                FalaynaFeature.ToReference<BlueprintFeatureReference>(),
                GhenshauFeature.ToReference<BlueprintFeatureReference>(),
                HalcamoraFeature.ToReference<BlueprintFeatureReference>(),
                ImmonhielFeature.ToReference<BlueprintFeatureReference>(),
                IrezFeature.ToReference<BlueprintFeatureReference>(),
                JaidzFeature.ToReference<BlueprintFeatureReference>(),
                JalaijataliFeature.ToReference<BlueprintFeatureReference>(),
                KoradaFeature.ToReference<BlueprintFeatureReference>(),
                LalaciFeature.ToReference<BlueprintFeatureReference>(),
                LymnierisFeature.ToReference<BlueprintFeatureReference>(),
                OlheonFeature.ToReference<BlueprintFeatureReference>(),
                PicoperiFeature.ToReference<BlueprintFeatureReference>(),
                PuluraFeature.ToReference<BlueprintFeatureReference>(),
                RagathielFeature.ToReference<BlueprintFeatureReference>(),
                RowdroshFeature.ToReference<BlueprintFeatureReference>(),
                SeramaydielFeature.ToReference<BlueprintFeatureReference>(),
                SheiFeature.ToReference<BlueprintFeatureReference>(),
                SinashaktiFeature.ToReference<BlueprintFeatureReference>(),
                SoralyonFeature.ToReference<BlueprintFeatureReference>(),
                TanagaarFeature.ToReference<BlueprintFeatureReference>(),
                TolcFeature.ToReference<BlueprintFeatureReference>(),
                ValaniFeature.ToReference<BlueprintFeatureReference>(),
                VildeisFeature.ToReference<BlueprintFeatureReference>(),
                WinlasFeature.ToReference<BlueprintFeatureReference>(),
                YlimanchaFeature.ToReference<BlueprintFeatureReference>(),
                ZohlsFeature.ToReference<BlueprintFeatureReference>()
                };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var ElvenPantheonIcon = AssetLoader.LoadInternal("Deities", "Icon_ElvenPantheon.jpg");
            var ElvenPantheonSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("ElvenPantheonSelection", bp => {
                bp.SetName("Elven Pantheon");
                bp.SetDescription("Although elves will worship any deity that strikes their fancy, the large majority worship the goddesses " +
                    "Calistria and Desna, and to a lesser extent, Nethys, Shelyn, or even more rarely, the various empyreal lords. They tend to " +
                    "have a less formal relationship with the divine, seeing the gods as general inspiration, and are not tied down with the " +
                    "particulars of dogma. In addition to the worship of the major deities, elves also have a number of minor elven gods who are " +
                    "almost only worshiped by their own kind. These include include Alseta goddess of transitions and the magical elf gates, " +
                    "Findeladlara, the goddess of art and architecture, Ketephys, god of the hunt, and Yuelral the Wise, goddess of crafting " +
                    "and magic.");
                bp.m_Icon = ElvenPantheonIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                FindeladlaraFeature.ToReference<BlueprintFeatureReference>(),
                KetephysFeature.ToReference<BlueprintFeatureReference>(),
                YuelralFeature.ToReference<BlueprintFeatureReference>() };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var DeitiesSelectionIcon = AssetLoader.LoadInternal("Deities", "Icon_DeitiesSelection.jpg");
            var DeitiesSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DeitiesSelection", bp => {
                bp.SetName("Gods and Goddesses");
                bp.SetDescription("A deity, also known as a god or goddess, is a being or force of incredible power capable " +
                    "of granting its power to mortal beings through divine magic. These are the only beings that can claim full Divinity. " +
                    "A deity is strongly associated with a specific alignment, " +
                    "several domains, and a plane (typically an Outer Sphere plane).");
                bp.m_Icon = DeitiesSelectionIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                AbadarFeature.ToReference<BlueprintFeatureReference>(),
                AchaekekFeature.ToReference<BlueprintFeatureReference>(),
                AlsetaFeature.ToReference<BlueprintFeatureReference>(),
                AsmodeusFeature.ToReference<BlueprintFeatureReference>(),
                BesmaraFeature.ToReference<BlueprintFeatureReference>(),
                CalistriaFeature.ToReference<BlueprintFeatureReference>(),
                CaydenCaileanFeature.ToReference<BlueprintFeatureReference>(),
                DesnaFeature.ToReference<BlueprintFeatureReference>(),
                ErastilFeature.ToReference<BlueprintFeatureReference>(),
                GorumFeature.ToReference<BlueprintFeatureReference>(),
                GozrehFeature.ToReference<BlueprintFeatureReference>(),
                GyronnaFeature.ToReference<BlueprintFeatureReference>(),
                IomedaeFeature.ToReference<BlueprintFeatureReference>(),
                IroriFeature.ToReference<BlueprintFeatureReference>(),
                KurgessFeature.ToReference<BlueprintFeatureReference>(),
                LamashtuFeature.ToReference<BlueprintFeatureReference>(),
                LichDeityFeature.ToReference<BlueprintFeatureReference>(),
                MilaniFeature.ToReference<BlueprintFeatureReference>(),
                NethysFeature.ToReference<BlueprintFeatureReference>(),
                NorgorberFeature.ToReference<BlueprintFeatureReference>(),
                PharasmaFeature.ToReference<BlueprintFeatureReference>(),
                RovagugFeature.ToReference<BlueprintFeatureReference>(),
                SarenraeFeature.ToReference<BlueprintFeatureReference>(),
                ShelynFeature.ToReference<BlueprintFeatureReference>(),
                ToragFeature.ToReference<BlueprintFeatureReference>(),
                UrgathoaFeature.ToReference<BlueprintFeatureReference>(),
                YdersiusFeature.ToReference<BlueprintFeatureReference>(),
                ZonKuthonFeature.ToReference<BlueprintFeatureReference>(),
                ZyphusFeature.ToReference<BlueprintFeatureReference>(),
                };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var DragonDeityIcon = AssetLoader.LoadInternal("Deities", "Icon_DragonDeities.jpg");
            var DraconicDeitySelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DraconicDeitySelection", bp => {
                bp.SetName("Draconic Deities");
                bp.SetDescription("In accordance with Draconic Lore, at the dawn of time there flowed two waters, fresh and salt, which became Apsu and Tiamat, " +
                    "parents of the first gods. However, their eldest son Dahak came to Hell to rampage, then killed his siblings, " +
                    "whose shattered remains fell into the Material Plane and became the first metallic dragons.");
                bp.m_Icon = DragonDeityIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] { ApsuFeature.ToReference<BlueprintFeatureReference>(), DahakFeature.ToReference<BlueprintFeatureReference>() };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var PhilosophiesIcon = AssetLoader.LoadInternal("Deities", "Icon_Philosophies.jpg");
            var PhilosophiesSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("PhilosophiesSelection", bp => {
                bp.SetName("Philosophies");
                bp.SetDescription("Philosophies of Golarion differ from religions in that they are teachings and ways of " +
                    "thinking that are propounded by a mortal founder who, usually, does not become a deity. By assenting to a " +
                    "particular philosophy, one is generally not limited in choosing any particular occupation; however, some " +
                    "combinations of philosophy and religion present inherent conflicts. The study of various fundamental " +
                    "underpinnings of many philosophies is known as metaphysics.");
                bp.m_Icon = PhilosophiesIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    GreenFaithFeature.ToReference<BlueprintFeatureReference>(),
                    AtheismFeature.ToReference<BlueprintFeatureReference>()
                };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var PantheonIcon = AssetLoader.LoadInternal("Deities", "Icon_Pantheon.jpg");
            var PantheonSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("PantheonSelection", bp => {
                bp.SetName("Pantheons");
                bp.SetDescription("A pantheon is a group of related gods worshipped either individually or together. " +
                    "Most pantheons are associated with a specific ancestry or geopolitical region, but rarely, a pantheon " +
                    "consists of deities with overlapping areas of concern. Followers work to advance the shared interests of their " +
                    "pantheon, directing prayers to whichever god presides over their current activity or circumstance.");
                bp.m_Icon = PantheonIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    GodclawFeature.ToReference<BlueprintFeatureReference>()
                };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var TheEldestIcon = AssetLoader.LoadInternal("Deities", "Icon_TheEldest.jpg");
            var TheEldestSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("TheEldestSelection", bp => {
                bp.SetName("The Eldest");
                bp.SetDescription("When the gods abandoned the First World to focus their attentions on the Material Plane and the cycle " +
                    "of souls, they left behind a power vacuum. The fey were like children abandoned by their parents, and like those children, " +
                    "they fought and despaired, tormenting each other in hopes that an authority would appear to make them stop." +
                    "Into this void stepped the realm’s most powerful remaining residents, creatures that might as well have been gods " +
                    "in the minds of the average fey.Seeking all the same boons that had drawn them to the faith of the original gods—safety, " +
                    "belonging, knowledge, power—the common people not only served these great beings, but worshiped them.In time, these powerful " +
                    "fey learned the trick of granting spells to their worshipers, and thus the Eldest became gods in truth.");
                bp.m_Icon = TheEldestIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                CountRanalcFeature.ToReference<BlueprintFeatureReference>(),
                TheGreenMotherFeature.ToReference<BlueprintFeatureReference>(),
                ImbrexFeature.ToReference<BlueprintFeatureReference>(),
                TheLanternKingFeature.ToReference<BlueprintFeatureReference>(),
                TheLostPrinceFeature.ToReference<BlueprintFeatureReference>(),
                MagdhFeature.ToReference<BlueprintFeatureReference>(),
                NgFeature.ToReference<BlueprintFeatureReference>(),
                RagadahnFeature.ToReference<BlueprintFeatureReference>(),
                ShykaFeature.ToReference<BlueprintFeatureReference>(),
                };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var MonitorsIcon = AssetLoader.LoadInternal("Deities", "Icon_Monitors.jpg");
            var MonitorsSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("MonitorsSelection", bp => {
                bp.SetName("Monitors");
                bp.SetDescription("The monitors are enigmatic and ever at odds, yet all share a duty to the fundamental nature of reality. Acting " +
                    "together, they create a homeostasis perfectly suited to maintaining the multiverse as it now exists. The Aeons claim they forged " +
                    "the monitors in the fires of creation, while Psychopomps insist their beloved Pharasma anointed the first monitors to ensure the " +
                    "smooth flow of souls. Inevitables claim they first gave faces to the rules of reality, and Proteans believe that other monitors are " +
                    "simply corrupted Proteans. Monitors seem born from the multiverse itself—fractured pieces of a mind that finds equilibrium only in the " +
                    "conflict of its component parts.");
                bp.m_Icon = MonitorsIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {                
                AtroposFeature.ToReference<BlueprintFeatureReference>(),
                BarzahkFeature.ToReference<BlueprintFeatureReference>(),
                CeyannanFeature.ToReference<BlueprintFeatureReference>(),
                KerkamothFeature.ToReference<BlueprintFeatureReference>(),
                MonadFeature.ToReference<BlueprintFeatureReference>(),
                SsilameshnikFeature.ToReference<BlueprintFeatureReference>(),
                };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });






            var DeitySelectionIcon = AssetLoader.LoadInternal("Deities", "Icon_DeitySelection.jpg");
            DeitySelection.m_Icon = DeitySelectionIcon;
            DeitySelection.m_AllFeatures = new BlueprintFeatureReference[] {
                DeitiesofAncientOsirionSelection.ToReference<BlueprintFeatureReference>(),
                DeitiesofTianXiaSelection.ToReference<BlueprintFeatureReference>(),
                DemonLordSelection.ToReference<BlueprintFeatureReference>(),
                ArchdevilSelection.ToReference<BlueprintFeatureReference>(),
                EmpyrealLordSelection.ToReference<BlueprintFeatureReference>(),
                ElvenPantheonSelection.ToReference<BlueprintFeatureReference>(),
                DeitiesSelection.ToReference<BlueprintFeatureReference>(),
                DraconicDeitySelection.ToReference<BlueprintFeatureReference>(),
                PhilosophiesSelection.ToReference<BlueprintFeatureReference>(),
                PantheonSelection.ToReference<BlueprintFeatureReference>(),
                TheEldestSelection.ToReference<BlueprintFeatureReference>(),
                MonitorsSelection.ToReference<BlueprintFeatureReference>()
            };
            DeitySelection.Groups = new FeatureGroup[] { FeatureGroup.Deities };
            DeitySelection.Group = FeatureGroup.Deities;

            PaladinClass.RemoveComponents<PrerequisiteFeaturesFromList>();
            PaladinClass.AddComponent<PrerequisiteFeaturesFromList>(c => {
                c.HideInUI = true;
                c.m_Features = new BlueprintFeatureReference[] {
                    AbadarFeature.ToReference<BlueprintFeatureReference>(),
                    IomedaeFeature.ToReference<BlueprintFeatureReference>(),
                    MilaniFeature.ToReference<BlueprintFeatureReference>(),
                    IroriFeature.ToReference<BlueprintFeatureReference>(),
                    ToragFeature.ToReference<BlueprintFeatureReference>(),
                    ShelynFeature.ToReference<BlueprintFeatureReference>(),
                    SarenraeFeature.ToReference<BlueprintFeatureReference>(),
                    ErastilFeature.ToReference<BlueprintFeatureReference>(),
                    ArsheaFeature.ToReference<BlueprintFeatureReference>(),
                    RagathielFeature.ToReference<BlueprintFeatureReference>(),
                    ApsuFeature.ToReference<BlueprintFeatureReference>(),
                    QiZhongFeature.ToReference<BlueprintFeatureReference>(),
                    ShizuruFeature.ToReference<BlueprintFeatureReference>(),
                    TsukiyoFeature.ToReference<BlueprintFeatureReference>(),
                    AnubisFeature.ToReference<BlueprintFeatureReference>(),
                    BesFeature.ToReference<BlueprintFeatureReference>(),
                    HorusFeature.ToReference<BlueprintFeatureReference>(),
                    IsisFeature.ToReference<BlueprintFeatureReference>(),
                    KhepriFeature.ToReference<BlueprintFeatureReference>(),
                    MaatFeature.ToReference<BlueprintFeatureReference>(),
                    NeithFeature.ToReference<BlueprintFeatureReference>(),
                    OsirisFeature.ToReference<BlueprintFeatureReference>(),
                    RaFeature.ToReference<BlueprintFeatureReference>(),
                    ThothFeature.ToReference<BlueprintFeatureReference>(),
                    WadjetFeature.ToReference<BlueprintFeatureReference>(),
                    YuelralFeature.ToReference<BlueprintFeatureReference>(),
                    AndolettaFeature.ToReference<BlueprintFeatureReference>(),
                    ArquerosFeature.ToReference<BlueprintFeatureReference>(),
                    BharnarolFeature.ToReference<BlueprintFeatureReference>(),
                    DammerichFeature.ToReference<BlueprintFeatureReference>(),
                    EritriceFeature.ToReference<BlueprintFeatureReference>(),
                    FalaynaFeature.ToReference<BlueprintFeatureReference>(),
                    GhenshauFeature.ToReference<BlueprintFeatureReference>(),
                    HalcamoraFeature.ToReference<BlueprintFeatureReference>(),
                    IrezFeature.ToReference<BlueprintFeatureReference>(),
                    JaidzFeature.ToReference<BlueprintFeatureReference>(),
                    KoradaFeature.ToReference<BlueprintFeatureReference>(),
                    LymnierisFeature.ToReference<BlueprintFeatureReference>(),
                    OlheonFeature.ToReference<BlueprintFeatureReference>(),
                    RowdroshFeature.ToReference<BlueprintFeatureReference>(),
                    SeramaydielFeature.ToReference<BlueprintFeatureReference>(),
                    SheiFeature.ToReference<BlueprintFeatureReference>(),
                    SoralyonFeature.ToReference<BlueprintFeatureReference>(),
                    TanagaarFeature.ToReference<BlueprintFeatureReference>(),
                    VildeisFeature.ToReference<BlueprintFeatureReference>(),
                    WinlasFeature.ToReference<BlueprintFeatureReference>(),
                    YlimanchaFeature.ToReference<BlueprintFeatureReference>(),
                    ZohlsFeature.ToReference<BlueprintFeatureReference>(),
                    ImbrexFeature.ToReference<BlueprintFeatureReference>(),
                    MagdhFeature.ToReference<BlueprintFeatureReference>(),
                    KerkamothFeature.ToReference<BlueprintFeatureReference>(),
                    AlsetaFeature.ToReference<BlueprintFeatureReference>(),
                    KurgessFeature.ToReference<BlueprintFeatureReference>(),
                };
            });
            var Seelah = Resources.GetBlueprint<BlueprintUnit>("54be53f0b35bf3c4592a97ae335fe765");
            Seelah.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[] { IomedaeFeature.ToReference<BlueprintUnitFactReference>() };
            });
            var Lann = Resources.GetBlueprint<BlueprintUnit>("cb29621d99b902e4da6f5d232352fbda");
            Lann.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[] { IomedaeFeature.ToReference<BlueprintUnitFactReference>() };
            });
            var Wenduag = Resources.GetBlueprint<BlueprintUnit>("ae766624c03058440a036de90a7f2009");
            Wenduag.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[] { LamashtuFeature.ToReference<BlueprintUnitFactReference>() };
            });
            var Woljif = Resources.GetBlueprint<BlueprintUnit>("766435873b1361c4287c351de194e5f9");
            Woljif.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[] { CalistriaFeature.ToReference<BlueprintUnitFactReference>() };
            });
            var Camelia = Resources.GetBlueprint<BlueprintUnit>("397b090721c41044ea3220445300e1b8");
            Camelia.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[] { GreenFaithFeature.ToReference<BlueprintUnitFactReference>() };
            });
            var Arueshalae = Resources.GetBlueprint<BlueprintUnit>("a352873d37ec6c54c9fa8f6da3a6b3e1");
            Arueshalae.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[] { DesnaFeature.ToReference<BlueprintUnitFactReference>() };
            });
            var Sosiel = Resources.GetBlueprint<BlueprintUnit>("1cbbbb892f93c3d439f8417ad7cbb6aa");
            Sosiel.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[] { ShelynFeature.ToReference<BlueprintUnitFactReference>() };
            });
            var Greybor = Resources.GetBlueprint<BlueprintUnit>("f72bb7c48bb3e45458f866045448fb58");
            Greybor.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[] { NorgorberFeature.ToReference<BlueprintUnitFactReference>() };
            });
        }
    }
}








