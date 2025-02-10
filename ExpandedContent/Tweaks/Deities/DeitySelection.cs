using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.FactLogic;

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
            var AbraxasFeature = Resources.GetModBlueprint<BlueprintFeature>("AbraxasFeature");
            var AldinachFeature = Resources.GetModBlueprint<BlueprintFeature>("AldinachFeature");
            var AreshkegalFeature = Resources.GetBlueprint<BlueprintFeature>("d714ecb5d5bb89a42957de0304e459c9");
            var BaphometFeature = Resources.GetBlueprint<BlueprintFeature>("bd72ca8ffcfec5745899ac56c93f12c5");
            var CythVsugFeature = Resources.GetModBlueprint<BlueprintFeature>("CythVsugFeature");
            var DagonFeature = Resources.GetModBlueprint<BlueprintFeature>("DagonFeature");
            var DeskariFeature = Resources.GetBlueprint<BlueprintFeature>("ddf913858bdf43b4da3b731e082fbcc0");
            var GoguntaFeature = Resources.GetModBlueprint<BlueprintFeature>("GoguntaFeature");
            var JezeldaFeature = Resources.GetModBlueprint<BlueprintFeature>("JezeldaFeature");
            var JubilexFeature = Resources.GetModBlueprint<BlueprintFeature>("JubilexFeature");
            var KabririFeature = Resources.GetBlueprint<BlueprintFeature>("f12c1ccc9d600c04f8887cd28a8f45a5");
            var MazmezzFeature = Resources.GetModBlueprint<BlueprintFeature>("MazmezzFeature");
            var MestamaFeature = Resources.GetModBlueprint<BlueprintFeature>("MestamaFeature");
            var NocticulaFeature = Resources.GetModBlueprint<BlueprintFeature>("NocticulaFeature");
            var NurgalFeature = Resources.GetModBlueprint<BlueprintFeature>("NurgalFeature");
            var OrcusFeature = Resources.GetModBlueprint<BlueprintFeature>("OrcusFeature");
            var PazuzuFeature = Resources.GetModBlueprint<BlueprintFeature>("PazuzuFeature");
            var ShaxFeature = Resources.GetModBlueprint<BlueprintFeature>("ShaxFeature");
            var ShivaskaFeature = Resources.GetModBlueprint<BlueprintFeature>("ShivaskaFeature");
            var TreerazerFeature = Resources.GetModBlueprint<BlueprintFeature>("TreerazerFeature");
            var ZuraFeature = Resources.GetModBlueprint<BlueprintFeature>("ZuraFeature");

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
            var GroetusFeature = Resources.GetBlueprint<BlueprintFeature>("c3e4d5681906d5246ab8b0637b98cbfe");
            var NaderiFeature = Resources.GetModBlueprint<BlueprintFeature>("NaderiFeature");
            var NewAchaekekFeature = Resources.GetBlueprint<BlueprintFeature>("a3189d5b7c4d4d91beaa8bfffac3e38e");
            var SivanahFeature = Resources.GetModBlueprint<BlueprintFeature>("SivanahFeature");
            var NewApsuFeature = Resources.GetBlueprint<BlueprintFeature>("772e2673945e4583a804ae01f67efea0");
            var NewDahakFeature = Resources.GetBlueprint<BlueprintFeature>("8f7118d68f6e44dea94dddb51f38cbdd");
            var GhlaunderFeature = Resources.GetModBlueprint<BlueprintFeature>("GhlaunderFeature");


            //Philosophies
            var GreenFaithFeature = Resources.GetBlueprint<BlueprintFeature>("99a7a8f13c1300c42878558fa9471e2f");
            var GreenFaithCameliaFeature = Resources.GetBlueprint<BlueprintFeature>("ca763809e01f4247a3639965364c26cb");
            var AtheismFeature = Resources.GetBlueprint<BlueprintFeature>("92c0d2da0a836ce418a267093c09ca54");

            //Pantheons
            var GodclawFeature = Resources.GetBlueprint<BlueprintFeature>("583a26e88031d0a4a94c8180105692a5");
            var GodclawIcon = AssetLoader.LoadInternal("Deities", "Icon_Godclaw.jpg");
            GodclawFeature.m_Icon = GodclawIcon;

            //Dragon Gods/Aspects
            var ApsuFeature = Resources.GetModBlueprint<BlueprintFeature>("ApsuFeature");
            var DahakFeature = Resources.GetModBlueprint<BlueprintFeature>("DahakFeature");

            //Archdevils
            var BaalzebulFeature = Resources.GetModBlueprint<BlueprintFeature>("BaalzebulFeature");
            var BarbatosFeature = Resources.GetModBlueprint<BlueprintFeature>("BarbatosFeature");
            var BelialFeature = Resources.GetModBlueprint<BlueprintFeature>("BelialFeature");
            var DispaterFeature = Resources.GetModBlueprint<BlueprintFeature>("DispaterFeature");
            var GeryonFeature = Resources.GetModBlueprint<BlueprintFeature>("GeryonFeature");
            var MammonFeature = Resources.GetModBlueprint<BlueprintFeature>("MammonFeature");
            var MephistophelesFeature = Resources.GetModBlueprint<BlueprintFeature>("MephistophelesFeature");
            var MolochFeature = Resources.GetModBlueprint<BlueprintFeature>("MolochFeature");

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
            var IlsurrishFeature = Resources.GetModBlueprint<BlueprintFeature>("IlsurrishFeature");
            var JerishallFeature = Resources.GetModBlueprint<BlueprintFeature>("JerishallFeature");
            var KerkamothFeature = Resources.GetModBlueprint<BlueprintFeature>("KerkamothFeature");
            var MonadFeature = Resources.GetModBlueprint<BlueprintFeature>("MonadFeature");
            var NarriseminekFeature = Resources.GetModBlueprint<BlueprintFeature>("NarriseminekFeature");
            var OtolmensFeature = Resources.GetModBlueprint<BlueprintFeature>("OtolmensFeature");
            var SsilameshnikFeature = Resources.GetModBlueprint<BlueprintFeature>("SsilameshnikFeature");
            var ValmallosFeature = Resources.GetModBlueprint<BlueprintFeature>("ValmallosFeature");
            var YdajiskFeature = Resources.GetModBlueprint<BlueprintFeature>("YdajiskFeature");
            var DammarFeature = Resources.GetModBlueprint<BlueprintFeature>("DammarFeature");
            var ImotFeature = Resources.GetModBlueprint<BlueprintFeature>("ImotFeature");
            var MotherVultureFeature = Resources.GetModBlueprint<BlueprintFeature>("MotherVultureFeature");
            var MrtyuFeature = Resources.GetModBlueprint<BlueprintFeature>("MrtyuFeature");
            var NarakaasFeature = Resources.GetModBlueprint<BlueprintFeature>("NarakaasFeature");
            var PhlegyasFeature = Resources.GetModBlueprint<BlueprintFeature>("PhlegyasFeature");
            var SalocFeature = Resources.GetModBlueprint<BlueprintFeature>("SalocFeature");
            var TeshallasFeature = Resources.GetModBlueprint<BlueprintFeature>("TeshallasFeature");
            var ThePaleHorseFeature = Resources.GetModBlueprint<BlueprintFeature>("ThePaleHorseFeature");
            var ValeFeature = Resources.GetModBlueprint<BlueprintFeature>("ValeFeature");
            var VavaalravFeature = Resources.GetModBlueprint<BlueprintFeature>("VavaalravFeature");
            var VonymosFeature = Resources.GetModBlueprint<BlueprintFeature>("VonymosFeature");

            //The Elder Mythos
            var AbhothFeature = Resources.GetModBlueprint<BlueprintFeature>("AbhothFeature");
            var AtlachNachaFeature = Resources.GetModBlueprint<BlueprintFeature>("AtlachNachaFeature");
            var AzathothFeature = Resources.GetModBlueprint<BlueprintFeature>("AzathothFeature");
            var BokrugFeature = Resources.GetModBlueprint<BlueprintFeature>("BokrugFeature");
            var ChaugnarFaugnFeature = Resources.GetModBlueprint<BlueprintFeature>("ChaugnarFaugnFeature");
            var CthulhuFeature = Resources.GetModBlueprint<BlueprintFeature>("CthulhuFeature");
            var GhatanothoaFeature = Resources.GetModBlueprint<BlueprintFeature>("GhatanothoaFeature");
            var HasturFeature = Resources.GetModBlueprint<BlueprintFeature>("HasturFeature");
            var IthaquaFeature = Resources.GetModBlueprint<BlueprintFeature>("IthaquaFeature");
            var MharFeature = Resources.GetModBlueprint<BlueprintFeature>("MharFeature");
            var MordiggianFeature = Resources.GetModBlueprint<BlueprintFeature>("MordiggianFeature");
            var NhimbalothFeature = Resources.GetModBlueprint<BlueprintFeature>("NhimbalothFeature");
            var NyarlathotepFeature = Resources.GetModBlueprint<BlueprintFeature>("NyarlathotepFeature");
            var OrgeshFeature = Resources.GetModBlueprint<BlueprintFeature>("OrgeshFeature");
            var RhanTegothFeature = Resources.GetModBlueprint<BlueprintFeature>("RhanTegothFeature");
            var ShubNiggurathFeature = Resources.GetModBlueprint<BlueprintFeature>("ShubNiggurathFeature");
            var TsathogguaFeature = Resources.GetModBlueprint<BlueprintFeature>("TsathogguaFeature");
            var XhameDorFeature = Resources.GetModBlueprint<BlueprintFeature>("XhameDorFeature");
            var YigFeature = Resources.GetModBlueprint<BlueprintFeature>("YigFeature");
            var YogSothothFeature = Resources.GetModBlueprint<BlueprintFeature>("YogSothothFeature");

            //Orc Pantheon
            var DrethaFeature = Resources.GetModBlueprint<BlueprintFeature>("DrethaFeature");
            var LanishraFeature = Resources.GetModBlueprint<BlueprintFeature>("LanishraFeature");
            var NulgrethFeature = Resources.GetModBlueprint<BlueprintFeature>("NulgrethFeature");
            var RullFeature = Resources.GetModBlueprint<BlueprintFeature>("RullFeature");
            var SezelrianFeature = Resources.GetModBlueprint<BlueprintFeature>("SezelrianFeature");
            var VargFeature = Resources.GetModBlueprint<BlueprintFeature>("VargFeature");
            var VerexFeature = Resources.GetModBlueprint<BlueprintFeature>("VerexFeature");
            var ZagreshFeature = Resources.GetModBlueprint<BlueprintFeature>("ZagreshFeature");

            //Four Horsemen
            var ApollyonFeature = Resources.GetModBlueprint<BlueprintFeature>("ApollyonFeature");
            var CharonFeature = Resources.GetModBlueprint<BlueprintFeature>("CharonFeature");
            var SzurielFeature = Resources.GetModBlueprint<BlueprintFeature>("SzurielFeature");
            var TrelmarixianFeature = Resources.GetModBlueprint<BlueprintFeature>("TrelmarixianFeature");


            var DeitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("59e7a76987fe3b547b9cce045f4db3e4");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var WarpriestClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("30b5e47d47a0e37438cc5a80c96cfb99");
            
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
                AbraxasFeature.ToReference<BlueprintFeatureReference>(),
                AldinachFeature.ToReference<BlueprintFeatureReference>(),
                AreshkegalFeature.ToReference<BlueprintFeatureReference>(),
                BaphometFeature.ToReference<BlueprintFeatureReference>(),
                CythVsugFeature.ToReference<BlueprintFeatureReference>(),
                DagonFeature.ToReference<BlueprintFeatureReference>(),
                DeskariFeature.ToReference<BlueprintFeatureReference>(),
                GoguntaFeature.ToReference<BlueprintFeatureReference>(),
                JezeldaFeature.ToReference<BlueprintFeatureReference>(),
                JubilexFeature.ToReference<BlueprintFeatureReference>(),
                KabririFeature.ToReference<BlueprintFeatureReference>(),
                MazmezzFeature.ToReference<BlueprintFeatureReference>(),
                MestamaFeature.ToReference<BlueprintFeatureReference>(),
                NocticulaFeature.ToReference<BlueprintFeatureReference>(),
                NurgalFeature.ToReference<BlueprintFeatureReference>(),
                OrcusFeature.ToReference<BlueprintFeatureReference>(),
                PazuzuFeature.ToReference<BlueprintFeatureReference>(),
                ShaxFeature.ToReference<BlueprintFeatureReference>(),
                ShivaskaFeature.ToReference<BlueprintFeatureReference>(),
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
                BaalzebulFeature.ToReference<BlueprintFeatureReference>(),
                BarbatosFeature.ToReference<BlueprintFeatureReference>(),
                BelialFeature.ToReference<BlueprintFeatureReference>(),
                DispaterFeature.ToReference<BlueprintFeatureReference>(),
                GeryonFeature.ToReference<BlueprintFeatureReference>(),
                MammonFeature.ToReference<BlueprintFeatureReference>(),
                MephistophelesFeature.ToReference<BlueprintFeatureReference>(),
                MolochFeature.ToReference<BlueprintFeatureReference>()
                };
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
                bp.SetName("Deities of the Inner Sea region");
                bp.SetDescription("There are hundreds of gods and demigods that are worshiped in the Inner Sea Regions. Ranging from unknown minor deities without a pantheon, " +
                    "to just as powerful as more commonly worshipped gods.");
                bp.m_Icon = DeitiesSelectionIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                AchaekekFeature.ToReference<BlueprintFeatureReference>(),
                AlsetaFeature.ToReference<BlueprintFeatureReference>(),
                BesmaraFeature.ToReference<BlueprintFeatureReference>(),
                KurgessFeature.ToReference<BlueprintFeatureReference>(),
                MilaniFeature.ToReference<BlueprintFeatureReference>(),
                YdersiusFeature.ToReference<BlueprintFeatureReference>(),
                ZyphusFeature.ToReference<BlueprintFeatureReference>(),
                NaderiFeature.ToReference<BlueprintFeatureReference>(),
                SivanahFeature.ToReference<BlueprintFeatureReference>(),
                GhlaunderFeature.ToReference<BlueprintFeatureReference>()
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
            var PhilosophiesSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("PhilosophiesSelection", bp => {//not in use
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
            var PantheonSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("PantheonSelection", bp => {//not in use
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
                    DammarFeature.ToReference<BlueprintFeatureReference>(),
                    IlsurrishFeature.ToReference<BlueprintFeatureReference>(),
                    ImotFeature.ToReference<BlueprintFeatureReference>(),
                    JerishallFeature.ToReference<BlueprintFeatureReference>(),
                    KerkamothFeature.ToReference<BlueprintFeatureReference>(),
                    MonadFeature.ToReference<BlueprintFeatureReference>(),
                    MotherVultureFeature.ToReference<BlueprintFeatureReference>(),
                    MrtyuFeature.ToReference<BlueprintFeatureReference>(),
                    NarakaasFeature.ToReference<BlueprintFeatureReference>(),
                    NarriseminekFeature.ToReference<BlueprintFeatureReference>(),
                    OtolmensFeature.ToReference<BlueprintFeatureReference>(),
                    PhlegyasFeature.ToReference<BlueprintFeatureReference>(),
                    SalocFeature.ToReference<BlueprintFeatureReference>(),
                    SsilameshnikFeature.ToReference<BlueprintFeatureReference>(),
                    TeshallasFeature.ToReference<BlueprintFeatureReference>(),
                    ThePaleHorseFeature.ToReference<BlueprintFeatureReference>(),
                    ValeFeature.ToReference<BlueprintFeatureReference>(),
                    ValmallosFeature.ToReference<BlueprintFeatureReference>(),
                    VavaalravFeature.ToReference<BlueprintFeatureReference>(),
                    VonymosFeature.ToReference<BlueprintFeatureReference>(),
                    YdajiskFeature.ToReference<BlueprintFeatureReference>(),
                };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var TheElderMythosIcon = AssetLoader.LoadInternal("Deities", "Icon_TheElderMythos.jpg");
            var TheElderMythosSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("TheElderMythosSelection", bp => {
                bp.SetName("The Elder Mythos");
                bp.SetDescription("In the early days, there were older gods whose existence dwarfed even those who helped shape the course of written history. " +
                    "These are the unimaginably ancient and inconceivably potent Great Old Ones, who in turn serve and worship the even greater Outer Gods. " +
                    "Scholars call these entities by many names, but on Golarion, their faiths and the entities themselves are known as the Elder Mythos. " +
                    "Worshipped primarily by the mad or desperate, the knowledge gained from gazing into the Dark Tapestry will often break even the strongest " +
                    "mind, freeing it to maddening truth.");
                bp.m_Icon = TheElderMythosIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    AbhothFeature.ToReference<BlueprintFeatureReference>(),
                    AtlachNachaFeature.ToReference<BlueprintFeatureReference>(),
                    AzathothFeature.ToReference<BlueprintFeatureReference>(),
                    BokrugFeature.ToReference<BlueprintFeatureReference>(),
                    ChaugnarFaugnFeature.ToReference<BlueprintFeatureReference>(),
                    CthulhuFeature.ToReference<BlueprintFeatureReference>(),
                    GhatanothoaFeature.ToReference<BlueprintFeatureReference>(),
                    HasturFeature.ToReference<BlueprintFeatureReference>(),
                    IthaquaFeature.ToReference<BlueprintFeatureReference>(),
                    MharFeature.ToReference<BlueprintFeatureReference>(),
                    MordiggianFeature.ToReference<BlueprintFeatureReference>(),
                    NhimbalothFeature.ToReference<BlueprintFeatureReference>(),
                    NyarlathotepFeature.ToReference<BlueprintFeatureReference>(),
                    OrgeshFeature.ToReference<BlueprintFeatureReference>(),
                    RhanTegothFeature.ToReference<BlueprintFeatureReference>(),
                    ShubNiggurathFeature.ToReference<BlueprintFeatureReference>(),
                    TsathogguaFeature.ToReference<BlueprintFeatureReference>(),
                    XhameDorFeature.ToReference<BlueprintFeatureReference>(),
                    YigFeature.ToReference<BlueprintFeatureReference>(),
                    YogSothothFeature.ToReference<BlueprintFeatureReference>()
                };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var OrcPantheonIcon = AssetLoader.LoadInternal("Deities", "Icon_OrcPantheon.jpg");
            var OrcPantheonSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("OrcPantheonSelection", bp => {
                bp.SetName("Orc Pantheon");
                bp.SetDescription("Orcs are not known for their discretion, yet in matters of religion, they are surprisingly tight-lipped. When a common orc " +
                    "refers to a god, even his own patron deity, he never uses its name, only one of its titles. The superstitious orcs believe that the names " +
                    "of gods hold power, and to speak a god’s name is to draw its attention. Orcs may revere violent gods, not even they wish to see the full " +
                    "manifestation of such deities’ destructive power. Only the shamans and witch doctors dare speak these unholy names, and then only in the " +
                    "midst of ecstatic rituals. This reticence on the part of orcs has led many outsiders to the erroneous conclusion that orc gods have no names. " +
                    "Some scholars have even gone so far as to suggest that the orc gods are not even gods at all.");
                bp.m_Icon = OrcPantheonIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    DrethaFeature.ToReference<BlueprintFeatureReference>(),
                    LanishraFeature.ToReference<BlueprintFeatureReference>(),
                    NulgrethFeature.ToReference<BlueprintFeatureReference>(),
                    RullFeature.ToReference<BlueprintFeatureReference>(),
                    SezelrianFeature.ToReference<BlueprintFeatureReference>(),
                    VargFeature.ToReference<BlueprintFeatureReference>(),
                    VerexFeature.ToReference<BlueprintFeatureReference>(),
                    ZagreshFeature.ToReference<BlueprintFeatureReference>()
                };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });
            var FourHorsemenIcon = AssetLoader.LoadInternal("Deities", "Icon_FourHorsemen.jpg");
            var FourHorsemenSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("FourHorsemenSelection", bp => {
                bp.SetName("The Four Horsemen");
                bp.SetDescription("At the end of the River Styx await the rulers of Abaddon: the Four Horsemen of the Apocalypse " +
                    "and their daemonic brood. Hatred for living things fuels all of daemonkind, who see existence as a great mistake. " +
                    "Though all of the Horsemen began their lives as mortals, they are completely devoted to the eradication of reality " +
                    "itself. Riding atop their dreaded steeds, they strive for true apocalypse. Only when all life has been snuffed " +
                    "out does their mission end, and they can then consign themselves to the waiting oblivion. \nOnly the truly nihilistic " +
                    "worship the Horsemen. Those who seek vengeance on all the world or lust for power are drawn to the Horsemen's easy " +
                    "promises, thinking they will be spared from their masters' cataclysmic mission. They never are. To the Horsemen, " +
                    "their followers are entirely disposable, tools with a purpose until they too are devoured.");
                bp.m_Icon = FourHorsemenIcon;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    ApollyonFeature.ToReference<BlueprintFeatureReference>(),
                    CharonFeature.ToReference<BlueprintFeatureReference>(),
                    SzurielFeature.ToReference<BlueprintFeatureReference>(),
                    TrelmarixianFeature.ToReference<BlueprintFeatureReference>()
                };
                bp.IsClassFeature = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.Group = FeatureGroup.Deities;
            });

            var DeitySelectionIcon = AssetLoader.LoadInternal("Deities", "Icon_DeitySelection.jpg");
            DeitySelection.m_Icon = DeitySelectionIcon;
            DeitySelection.m_AllFeatures = new BlueprintFeatureReference[] {
                AbadarFeature.ToReference<BlueprintFeatureReference>(),
                AsmodeusFeature.ToReference<BlueprintFeatureReference>(),
                CalistriaFeature.ToReference<BlueprintFeatureReference>(),
                CaydenCaileanFeature.ToReference<BlueprintFeatureReference>(),
                DesnaFeature.ToReference<BlueprintFeatureReference>(),
                ErastilFeature.ToReference<BlueprintFeatureReference>(),
                GorumFeature.ToReference<BlueprintFeatureReference>(),
                GozrehFeature.ToReference<BlueprintFeatureReference>(),
                GyronnaFeature.ToReference<BlueprintFeatureReference>(),
                IomedaeFeature.ToReference<BlueprintFeatureReference>(),
                IroriFeature.ToReference<BlueprintFeatureReference>(),
                LamashtuFeature.ToReference<BlueprintFeatureReference>(),
                NethysFeature.ToReference<BlueprintFeatureReference>(),
                NorgorberFeature.ToReference<BlueprintFeatureReference>(),
                PharasmaFeature.ToReference<BlueprintFeatureReference>(),
                RovagugFeature.ToReference<BlueprintFeatureReference>(),
                SarenraeFeature.ToReference<BlueprintFeatureReference>(),
                ShelynFeature.ToReference<BlueprintFeatureReference>(),
                ToragFeature.ToReference<BlueprintFeatureReference>(),
                UrgathoaFeature.ToReference<BlueprintFeatureReference>(),
                ZonKuthonFeature.ToReference<BlueprintFeatureReference>(),
                GroetusFeature.ToReference<BlueprintFeatureReference>(),
                GreenFaithFeature.ToReference<BlueprintFeatureReference>(),
                AtheismFeature.ToReference<BlueprintFeatureReference>(),
                GodclawFeature.ToReference<BlueprintFeatureReference>(),
                LichDeityFeature.ToReference<BlueprintFeatureReference>(),
                NewAchaekekFeature.ToReference<BlueprintFeatureReference>(),
                NewApsuFeature.ToReference<BlueprintFeatureReference>(),
                NewDahakFeature.ToReference<BlueprintFeatureReference>()
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
                    JerishallFeature.ToReference<BlueprintFeatureReference>(),
                    OtolmensFeature.ToReference<BlueprintFeatureReference>(),
                    ValmallosFeature.ToReference<BlueprintFeatureReference>(),
                    NewApsuFeature.ToReference<BlueprintFeatureReference>(),
                };
            });
            #region Faithful Paragon Lock
            var FaithfulParagonArchetype = Resources.GetModBlueprint<BlueprintArchetype>("FaithfulParagonArchetype");
            FaithfulParagonArchetype.AddComponent<PrerequisiteFeaturesFromList>(c => {
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
                    JerishallFeature.ToReference<BlueprintFeatureReference>(),
                    OtolmensFeature.ToReference<BlueprintFeatureReference>(),
                    ValmallosFeature.ToReference<BlueprintFeatureReference>(),
                    NewApsuFeature.ToReference<BlueprintFeatureReference>(),
                };
            });
            var FaithfulParagonNotAllowed = new BlueprintFeature[] {
                ApepFeature,
                BastetFeature,
                HathorFeature,
                NephthysFeature,
                PtahFeature,
                SekhmetFeature,
                SelketFeature,
                SetFeature,
                SobekFeature,
                DaikitsuFeature,
                FumeiyoshiFeature,
                GeneralSusumuFeature,
                HeiFengFeature,
                KofusachiFeature,
                LadyNanbyoFeature,
                LaoShuPoFeature,
                NalinivatiFeature,
                WukongFeature,
                YaezhingFeature,
                YamatsumiFeature,
                AbraxasFeature,
                AldinachFeature,
                AreshkegalFeature,
                BaphometFeature,
                CythVsugFeature,
                DagonFeature,
                DeskariFeature,
                GoguntaFeature,
                JezeldaFeature,
                JubilexFeature,
                KabririFeature,
                MazmezzFeature,
                MestamaFeature,
                NocticulaFeature,
                NurgalFeature,
                OrcusFeature,
                PazuzuFeature,
                ShaxFeature,
                ShivaskaFeature,
                TreerazerFeature,
                ZuraFeature,
                PuluraFeature,
                AshavaFeature,
                BlackButterflyFeature,
                ChadaliFeature,
                ChucaroFeature,
                ImmonhielFeature,
                JalaijataliFeature,
                LalaciFeature,
                PicoperiFeature,
                SinashaktiFeature,
                TolcFeature,
                ValaniFeature,
                FindeladlaraFeature,
                KetephysFeature,
                GorumFeature,
                GozrehFeature,
                GyronnaFeature,
                LamashtuFeature,
                LichDeityFeature,
                AsmodeusFeature,
                NethysFeature,
                NorgorberFeature,
                PharasmaFeature,
                RovagugFeature,
                UrgathoaFeature,
                ZonKuthonFeature,
                CalistriaFeature,
                CaydenCaileanFeature,
                DesnaFeature,
                BesmaraFeature,
                AchaekekFeature,
                ZyphusFeature,
                YdersiusFeature,
                GroetusFeature,
                NaderiFeature,
                NewAchaekekFeature,
                SivanahFeature,
                NewDahakFeature,
                GhlaunderFeature,
                GreenFaithFeature,
                GreenFaithCameliaFeature,
                DahakFeature,
                BaalzebulFeature,
                BarbatosFeature,
                BelialFeature,
                DispaterFeature,
                GeryonFeature,
                MammonFeature,
                MephistophelesFeature,
                MolochFeature,
                CountRanalcFeature,
                TheGreenMotherFeature,
                TheLanternKingFeature,
                TheLostPrinceFeature,
                NgFeature,
                RagadahnFeature,
                ShykaFeature,
                AtroposFeature,
                BarzahkFeature,
                CeyannanFeature,
                IlsurrishFeature,
                MonadFeature,
                NarriseminekFeature,
                SsilameshnikFeature,
                YdajiskFeature,
                DammarFeature,
                ImotFeature,
                MotherVultureFeature,
                MrtyuFeature,
                NarakaasFeature,
                PhlegyasFeature,
                SalocFeature,
                TeshallasFeature,
                ThePaleHorseFeature,
                ValeFeature,
                VavaalravFeature,
                VonymosFeature,
                AbhothFeature,
                AtlachNachaFeature,
                AzathothFeature,
                BokrugFeature,
                ChaugnarFaugnFeature,
                CthulhuFeature,
                GhatanothoaFeature,
                HasturFeature,
                IthaquaFeature,
                MharFeature,
                MordiggianFeature,
                NhimbalothFeature,
                NyarlathotepFeature,
                OrgeshFeature,
                RhanTegothFeature,
                ShubNiggurathFeature,
                TsathogguaFeature,
                XhameDorFeature,
                YigFeature,
                YogSothothFeature,
                DrethaFeature,
                LanishraFeature,
                NulgrethFeature,
                RullFeature,
                SezelrianFeature,
                VargFeature,
                VerexFeature,
                ZagreshFeature,
                ApollyonFeature,
                CharonFeature,
                SzurielFeature,
                TrelmarixianFeature
            };
            foreach (var deity in FaithfulParagonNotAllowed) {
                deity.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.HideInUI = true;
                    c.m_CharacterClass = WarpriestClass;
                    c.m_Archetype = FaithfulParagonArchetype.ToReference<BlueprintArchetypeReference>();
                });
            }



            #endregion
            #region Stuff no longer needed but kept in case I need to add a toggle
            //var Seelah = Resources.GetBlueprint<BlueprintUnit>("54be53f0b35bf3c4592a97ae335fe765");
            //Seelah.AddComponent<AddFacts>(c => {
            //    c.m_Facts = new BlueprintUnitFactReference[] { IomedaeFeature.ToReference<BlueprintUnitFactReference>() };
            //});
            //var Lann = Resources.GetBlueprint<BlueprintUnit>("cb29621d99b902e4da6f5d232352fbda");
            //Lann.AddComponent<AddFacts>(c => {
            //    c.m_Facts = new BlueprintUnitFactReference[] { IomedaeFeature.ToReference<BlueprintUnitFactReference>() };
            //});
            //var Wenduag = Resources.GetBlueprint<BlueprintUnit>("ae766624c03058440a036de90a7f2009");
            //Wenduag.AddComponent<AddFacts>(c => {
            //    c.m_Facts = new BlueprintUnitFactReference[] { LamashtuFeature.ToReference<BlueprintUnitFactReference>() };
            //});
            //var Woljif = Resources.GetBlueprint<BlueprintUnit>("766435873b1361c4287c351de194e5f9");
            //Woljif.AddComponent<AddFacts>(c => {
            //    c.m_Facts = new BlueprintUnitFactReference[] { CalistriaFeature.ToReference<BlueprintUnitFactReference>() };
            //});
            //var Camelia = Resources.GetBlueprint<BlueprintUnit>("397b090721c41044ea3220445300e1b8");
            //Camelia.AddComponent<AddFacts>(c => {
            //    c.m_Facts = new BlueprintUnitFactReference[] { GreenFaithCameliaFeature.ToReference<BlueprintUnitFactReference>() };
            //});
            //var Arueshalae = Resources.GetBlueprint<BlueprintUnit>("a352873d37ec6c54c9fa8f6da3a6b3e1");
            //Arueshalae.AddComponent<AddFacts>(c => {
            //    c.m_Facts = new BlueprintUnitFactReference[] { DesnaFeature.ToReference<BlueprintUnitFactReference>() };
            //});
            //var Sosiel = Resources.GetBlueprint<BlueprintUnit>("1cbbbb892f93c3d439f8417ad7cbb6aa");
            //Sosiel.AddComponent<AddFacts>(c => {
            //    c.m_Facts = new BlueprintUnitFactReference[] { ShelynFeature.ToReference<BlueprintUnitFactReference>() };
            //});
            //var Greybor = Resources.GetBlueprint<BlueprintUnit>("f72bb7c48bb3e45458f866045448fb58");
            //Greybor.AddComponent<AddFacts>(c => {
            //    c.m_Facts = new BlueprintUnitFactReference[] { NorgorberFeature.ToReference<BlueprintUnitFactReference>() };
            //});
            //var Nenio = Resources.GetBlueprint<BlueprintUnit>("1b893f7cf2b150e4f8bc2b3c389ba71d");
            //Nenio.AddComponent<AddFacts>(c => {
            //    c.m_Facts = new BlueprintUnitFactReference[] { NethysFeature.ToReference<BlueprintUnitFactReference>() };
            //});
            //var Ember = Resources.GetBlueprint<BlueprintUnit>("2779754eecffd044fbd4842dba55312c");
            //Ember.AddComponent<AddFacts>(c => {
            //    c.m_Facts = new BlueprintUnitFactReference[] { AtheismFeature.ToReference<BlueprintUnitFactReference>() };
            //});
            //var Sendri = Resources.GetBlueprint<BlueprintUnit>("561036c882a640089b1d42f03ebe3a6c");
            //Sendri.AddComponent<AddFacts>(c => {
            //    c.m_Facts = new BlueprintUnitFactReference[] { DesnaFeature.ToReference<BlueprintUnitFactReference>() };
            //});
            //var Daeran = Resources.GetBlueprint<BlueprintUnit>("096fc4a96d675bb45a0396bcaa7aa993");
            //Daeran.AddComponent<AddFacts>(c => {
            //    c.m_Facts = new BlueprintUnitFactReference[] { AtheismFeature.ToReference<BlueprintUnitFactReference>() };
            //});
            //var Regill = Resources.GetBlueprint<BlueprintUnit>("0d37024170b172346b3769df92a971f5");
            //Regill.AddComponent<AddFacts>(c => {
            //    c.m_Facts = new BlueprintUnitFactReference[] { GodclawFeature.ToReference<BlueprintUnitFactReference>() };
            //});
            //var Trever = Resources.GetBlueprint<BlueprintUnit>("0bb1c03b9f7bbcf42bb74478af2c6258");
            //Trever.AddComponent<AddFacts>(c => {
            //    c.m_Facts = new BlueprintUnitFactReference[] { ShelynFeature.ToReference<BlueprintUnitFactReference>() };
            //});
            //var Rekarth = Resources.GetBlueprint<BlueprintUnit>("3e0014e4be454482a2797fd81123d7b4");
            //Rekarth.AddComponent<AddFacts>(c => {
            //    c.m_Facts = new BlueprintUnitFactReference[] { CalistriaFeature.ToReference<BlueprintUnitFactReference>() };
            //});
            #endregion
        }
        public static void ArchdevilsToggle() {
            if (ModSettings.AddedContent.Deities.IsDisabled("Archdevils")) { return; }
            var DeitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("59e7a76987fe3b547b9cce045f4db3e4");
            var ArchdevilSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("ArchdevilSelection");
            DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AddToArray(ArchdevilSelection.ToReference<BlueprintFeatureReference>());
        }
        public static void DeitiesofAncientOsirionToggle() {
            if (ModSettings.AddedContent.Deities.IsDisabled("Deities of Ancient Osirion")) { return; }
            var DeitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("59e7a76987fe3b547b9cce045f4db3e4");
            var DeitiesofAncientOsirionSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("DeitiesofAncientOsirionSelection");
            DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AddToArray(DeitiesofAncientOsirionSelection.ToReference<BlueprintFeatureReference>());
        }
        public static void DeitiesofTianXiaToggle() {
            if (ModSettings.AddedContent.Deities.IsDisabled("Deities of Tian Xia")) { return; }
            var DeitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("59e7a76987fe3b547b9cce045f4db3e4");
            var DeitiesofTianXiaSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("DeitiesofTianXiaSelection");
            DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AddToArray(DeitiesofTianXiaSelection.ToReference<BlueprintFeatureReference>());
        }
        public static void DemonLordToggle() {
            if (ModSettings.AddedContent.Deities.IsDisabled("Demon Lords")) { return; }
            var DeitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("59e7a76987fe3b547b9cce045f4db3e4");
            var DemonLordSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("DemonLordSelection");
            DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AddToArray(DemonLordSelection.ToReference<BlueprintFeatureReference>());
        }
        public static void EmpyrealLordsToggle() {
            if (ModSettings.AddedContent.Deities.IsDisabled("Empyreal Lords")) { return; }
            var DeitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("59e7a76987fe3b547b9cce045f4db3e4");
            var EmpyrealLordSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("EmpyrealLordSelection");
            DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AddToArray(EmpyrealLordSelection.ToReference<BlueprintFeatureReference>());
        }
        public static void ElvenPantheonToggle() {
            if (ModSettings.AddedContent.Deities.IsDisabled("Elven Pantheon")) { return; }
            var DeitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("59e7a76987fe3b547b9cce045f4db3e4");
            var ElvenPantheonSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("ElvenPantheonSelection");
            DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AddToArray(ElvenPantheonSelection.ToReference<BlueprintFeatureReference>());
        }
        //public static void DraconicDeityToggle() {
        //    if (ModSettings.AddedContent.Deities.IsDisabled("Draconic Deities")) { return; }
        //    var DeitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("59e7a76987fe3b547b9cce045f4db3e4");
        //    var DraconicDeitySelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("DraconicDeitySelection");
        //    DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AddToArray(DraconicDeitySelection.ToReference<BlueprintFeatureReference>());
        //}
        public static void TheEldestToggle() {
            if (ModSettings.AddedContent.Deities.IsDisabled("The Eldest")) { return; }
            var DeitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("59e7a76987fe3b547b9cce045f4db3e4");
            var TheEldestSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("TheEldestSelection");
            DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AddToArray(TheEldestSelection.ToReference<BlueprintFeatureReference>());
        }
        public static void MonitorsToggle() {
            if (ModSettings.AddedContent.Deities.IsDisabled("Monitors")) { return; }
            var DeitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("59e7a76987fe3b547b9cce045f4db3e4");
            var MonitorsSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("MonitorsSelection");
            DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AddToArray(MonitorsSelection.ToReference<BlueprintFeatureReference>());
        }
        public static void TheElderMythosToggle() {
            if (ModSettings.AddedContent.Deities.IsDisabled("The Elder Mythos")) { return; }
            var DeitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("59e7a76987fe3b547b9cce045f4db3e4");
            var TheElderMythosSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("TheElderMythosSelection");
            DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AddToArray(TheElderMythosSelection.ToReference<BlueprintFeatureReference>());
        }
        public static void OrcPantheonToggle() {
            if (ModSettings.AddedContent.Deities.IsDisabled("Orc Pantheon")) { return; }
            var DeitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("59e7a76987fe3b547b9cce045f4db3e4");
            var OrcPantheonSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("OrcPantheonSelection");
            DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AddToArray(OrcPantheonSelection.ToReference<BlueprintFeatureReference>());
        }
        public static void  InnerSeaDeitiesregionToggle() {
            if (ModSettings.AddedContent.Deities.IsDisabled("Inner Sea Deities")) { return; }
            var DeitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("59e7a76987fe3b547b9cce045f4db3e4");
            var DeitiesSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("DeitiesSelection");
            DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AddToArray(DeitiesSelection.ToReference<BlueprintFeatureReference>());
        }
        public static void FourHorsemenToggle() {
            if (ModSettings.AddedContent.Deities.IsDisabled("The Four Horsemen")) { return; }
            var DeitySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("59e7a76987fe3b547b9cce045f4db3e4");
            var FourHorsemenSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("FourHorsemenSelection");
            DeitySelection.m_AllFeatures = DeitySelection.m_AllFeatures.AddToArray(FourHorsemenSelection.ToReference<BlueprintFeatureReference>());
        }
    }
}








