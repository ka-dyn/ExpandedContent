using HarmonyLib;
using ExpandedContent.Extensions;
using ExpandedContent.Tweaks;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints.Root;

namespace ExpandedContent.Tweaks
{
    class ContentAdder {




        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            public static void Postfix() {
                if (Initialized) return;
                Initialized = true;

                Classes.DrakeClass.DrakeCompanionClass.AddDrakeCompanionClass();
                Classes.DrakeClass.DrakeCompanionGreen.AddDrakeCompanionGreen();
                Classes.DrakeClass.DrakeCompanionSilver.AddDrakeCompanionSilver();
                Classes.DrakeClass.DrakeCompanionBlack.AddDrakeCompanionBlack();
                Classes.DrakeClass.DrakeCompanionBlue.AddDrakeCompanionBlue();
                Classes.DrakeClass.DrakeCompanionBrass.AddDrakeCompanionBrass();
                Classes.DrakeClass.DrakeCompanionRed.AddDrakeCompanionRed();
                Classes.DrakeClass.DrakeCompanionWhite.AddDrakeCompanionWhite();
                Classes.DrakeClass.DrakeCompanionGold.AddDrakeCompanionGold();
                Classes.DrakeClass.DrakeSpells.AddDrakeSpells();
                Classes.DrakeClass.DrakeCompanionSelection.AddDrakeCompanionSelection();
                Classes.DrakeClass.DrakeMythicAbilities.AddDrakeMythicAbilities();

                Miscellaneous.AlignmentTemplates.AddFiendishTemplate();
                Miscellaneous.Cavalier.AddCavalierFeatures();
             
                Archetypes.LivingScripture.AddLivingScripture();
                Archetypes.PriestOfBalance.PatchPriestOfBalanceArchetype();
                Archetypes.TempleChampion.AddTempleChampion();
                Archetypes.MantisZealot.AddMantisZealot();
                Archetypes.Mooncaller.AddMooncaller();
                Archetypes.DraconicDruid.AddDraconicDruid();
                Archetypes.DrakeWarden.AddDrakeWarden();
                Archetypes.SilverChampion.AddSilverChampion();
                Archetypes.ClutchThief.AddClutchThief();
                Archetypes.OceansEcho.AddOceansEcho();
                Archetypes.DraconicShaman.AddDraconicShaman();

                Classes.OathbreakerClass.AddOathbreakerClass();
                Archetypes.Castigator.AddCastigator();

                Classes.DreadKnightClass.AddDreadKnightClass();
                Archetypes.Conqueror.AddConqueror();
                Archetypes.ClawOfTheFalseWyrm.AddClawOfTheFalseWyrm();

                Archetypes.PatchMantisZealotDeity.MantisZealotDeityPatch();

                RacialArchetypes.Cruoromancer.AllowCruoromancerArchetype();
                RacialArchetypes.CavalierOfThePaw.AllowCavalierOfThePaw();
                RacialArchetypes.Imitator.AllowImitatorArchetype();
                RacialArchetypes.MasterOfAll.AllowMasterOfAllArchetype();
                RacialArchetypes.NineTailedHeir.AllowNineTailedHeirArchetype();
                RacialArchetypes.Purifier.AllowPurifierArchetype();
                RacialArchetypes.ReformedFiend.AllowReformedFiendArchetype();
                RacialArchetypes.SpellDancer.AllowSpellDancerArchetype();
                RacialArchetypes.Stonelord.AllowStonelordArchetype();
                RacialArchetypes.StudentOfStone.AllowStudentOfStoneArchetype();
                RacialArchetypes.WildlandShaman.AllowWildlandShamanArchetype();
                RacialArchetypes.PhantasmalMage.AllowPhantasmalMageArchetype();
        
                Backgrounds.ArchdukeOfCheliax.AddBackgroundArchdukeOfCheliax();

                Spells.HydraulicPush.AddHydraulicPush();
                Spells.Slipstream.AddSlipstream();
                Spells.ScourgeOfTheHorsemen.AddScourgeOfTheHorsemen();
                Spells.RigorMortis.AddRigorMortis();

                Domains.ArchonDomainGood.AddArchonDomainGood();
                Domains.ArchonDomainLaw.AddArchonDomainLaw();
                Domains.BloodDomain.AddBloodDomain();
                Domains.CavesDomain.AddCavesDomain();
                Domains.DemonDomainChaos.AddDemonDomainChaos();
                Domains.DemonDomainEvil.AddDemonDomainEvil();
                Domains.StormDomain.AddStormDomain();
                Domains.IceDomain.AddIceDomain();
                Domains.ScalykindDomain.AddScalykindDomain();
                Domains.DragonDomain.AddDragonDomain();
                Domains.WindDomain.AddWindDomain();
                Domains.UndeadDomain.AddUndeadDomain();
                Domains.RevelationDomain.AddRevelationDomain();
                Domains.FerocityDomain.AddFerocityDomain();
                Domains.PsychopompDomainDeath.AddPsychopompDomainDeath();
                Domains.PsychopompDomainRepose.AddPsychopompDomainRepose();
                Domains.RiversDomain.AddRiversDomain();
                Domains.RageDomain.AddRageDomain();
                Domains.BaseDeityPatch.AddBaseDeityPatch();

                Mysteries.DragonMystery.AddDragonMystery();

                Curses.Vampirism.AddVampirismCurse();

                Archdevils.Dispater.AddDispater();
                Archdevils.Mephistopheles.AddMephistopheles();

                DemonLords.Areshkegal.AddAreshkegal();
                DemonLords.Deskari.AddDeskari();
                DemonLords.Kabriri.AddKabriri();
                DemonLords.Baphomet.AddBaphomet();
                DemonLords.Zura.AddZura();
                DemonLords.Dagon.AddDagonFeature();
                DemonLords.Treerazer.AddTreerazerFeature();
                DemonLords.Nocticula.AddNocticulaFeature();

                Deities.Apsu.AddApsu();
                Deities.Dahak.AddDahakFeature();

                Deities.Daikitsu.AddDaikitsuFeature();
                Deities.Wukong.AddWukongFeature();
                Deities.Fumeiyoshi.AddFumeiyoshiFeature();
                Deities.GeneralSusumu.AddGeneralSusumuFeature();
                Deities.HeiFeng.AddHeiFengFeature();
                Deities.Kofusachi.AddKofusachiFeature();
                Deities.LadyNanbyo.AddLadyNanbyoFeature();
                Deities.LaoShuPo.AddLaoShuPoFeature();
                Deities.Nalinivati.AddNalinivatiFeature();
                Deities.QiZhong.AddQiZhongFeature();
                Deities.Shizuru.AddShizuruFeature();
                Deities.Tsukiyo.AddTsukiyoFeature();
                Deities.Yaezhing.AddYaezhingFeature();
                Deities.Yamatsumi.AddYamatsumiFeature();

                Deities.Anubis.AddAnubisFeature();
                Deities.Apep.AddApepFeature();
                Deities.Bastet.AddBastetFeature();
                Deities.Bes.AddBesFeature();
                Deities.Hathor.AddHathorFeature();
                Deities.Horus.AddHorusFeature();
                Deities.Isis.AddIsisFeature();
                Deities.Khepri.AddKhepriFeature();
                Deities.Maat.AddMaatFeature();
                Deities.Neith.AddNeithFeature();
                Deities.Nephthys.AddNephthysFeature();
                Deities.Osiris.AddOsirisFeature();
                Deities.Ptah.AddPtahFeature();
                Deities.Ra.AddRaFeature();
                Deities.Sekhmet.AddSekhmetFeature();
                Deities.Selket.AddSelketFeature();
                Deities.Set.AddSetFeature();
                Deities.Sobek.AddSobekFeature();
                Deities.Thoth.AddThothFeature();
                Deities.Wadjet.AddWadjetFeature();

                Deities.Findeladlara.AddFindeladlaraFeature();
                Deities.Ketephys.AddKetephysFeature();
                Deities.Yuelral.AddYuelralFeature();
                
                Deities.GreenFaith.AddGreenFaith();
                Deities.Besmara.AddBesmaraFeature();
                Deities.Achaekek.AddAchaekekFeature();
                Deities.Alseta.AddAlsetaFeature();
                Deities.Zyphus.AddZyphusFeature();
                Deities.Kurgess.AddKurgessFeature();
                Deities.Ydersius.AddYdersiusFeature();
                Deities.Groetus.AddGroetus();
               
                Deities.PatchPulura.AddPulura();
                Deities.MilaniSacredWeaponFeature.AddMilaniSacredWeaponFeature();
                Deities.Ragathiel.AddRagathielFeature();
                Deities.Arshea.AddArsheaFeature();
                Deities.Milani.AddMilaniFeature();
                Deities.Andoletta.AddAndolettaFeature();
                Deities.Arqueros.AddArquerosFeature();
                Deities.Ashava.AddAshavaFeature();
                Deities.Bharnarol.AddBharnarolFeature();
                Deities.BlackButterfly.AddBlackButterflyFeature();
                Deities.Chadali.AddChadaliFeature();
                Deities.Chucaro.AddChucaroFeature();
                Deities.Dammerich.AddDammerichFeature();
                Deities.Eritrice.AddEritriceFeature();
                Deities.Falayna.AddFalaynaFeature();
                Deities.Ghenshau.AddGhenshauFeature();
                Deities.Halcamora.AddHalcamoraFeature();
                Deities.Immonhiel.AddImmonhielFeature();
                Deities.Irez.AddIrezFeature();
                Deities.Jaidz.AddJaidzFeature();
                Deities.Jalaijatali.AddJalaijataliFeature();
                Deities.Korada.AddKoradaFeature();
                Deities.Lalaci.AddLalaciFeature();
                Deities.Lymnieris.AddLymnierisFeature();
                Deities.Olheon.AddOlheonFeature();
                Deities.Picoperi.AddPicoperiFeature();
                Deities.Rowdrosh.AddRowdroshFeature();
                Deities.Seramaydiel.AddSeramaydielFeature();
                Deities.Shei.AddSheiFeature();
                Deities.Sinashakti.AddSinashaktiFeature();
                Deities.Soralyon.AddSoralyonFeature();
                Deities.Tanagaar.AddTanagaarFeature();
                Deities.Tolc.AddTolcFeature();
                Deities.Valani.AddValaniFeature();
                Deities.Vildeis.AddVildeisFeature();
                Deities.Winlas.AddWinlasFeature();
                Deities.Ylimancha.AddYlimanchaFeature();
                Deities.Zohls.AddZohlsFeature();

                TheEldest.Ng.AddNgFeature();
                TheEldest.Shyka.AddShykaFeature();
                TheEldest.TheLanternKing.AddTheLanternKingFeature();
                TheEldest.CountRanalc.AddCountRanalcFeature();
                TheEldest.TheGreenMother.AddTheGreenMotherFeature();
                TheEldest.Imbrex.AddImbrexFeature();
                TheEldest.TheLostPrince.AddTheLostPrinceFeature();
                TheEldest.Magdh.AddMagdhFeature();
                TheEldest.Ragadahn.AddRagadahnFeature();

                Monitors.Monad.AddMonadFeature();
                Monitors.Kerkamoth.AddKerkamothFeature();
                Monitors.Atropos.AddAtroposFeature();
                Monitors.Barzahk.AddBarzahkFeature();
                Monitors.Ceyannan.AddCeyannanFeature();
                Monitors.Ssilameshnik.AddSsilameshnikFeature();
                Monitors.Jerishall.AddJerishallFeature();
                Monitors.Otolmens.AddOtolmensFeature();
                Monitors.Valmallos.AddValmallosFeature();
                Monitors.Ilsurrish.AddIlsurrishFeature();
                Monitors.Narriseminek.AddNarriseminekFeature();
                Monitors.Ydajisk.AddYdajiskFeature();
                Monitors.Dammar.AddDammarFeature();
                Monitors.Imot.AddImotFeature();
                Monitors.MotherVulture.AddMotherVultureFeature();
                Monitors.Mrtyu.AddMrtyuFeature();
                Monitors.Narakaas.AddNarakaasFeature();
                Monitors.Phlegyas.AddPhlegyasFeature();
                Monitors.Saloc.AddSalocFeature();
                Monitors.Teshallas.AddTeshallasFeature();
                Monitors.ThePaleHorse.AddThePaleHorseFeature();
                Monitors.Vale.AddValeFeature();
                Monitors.Vavaalrav.AddVavaalravFeature();
                Monitors.Vonymos.AddVonymosFeature();

                TheElderMythos.Abhoth.AddAbhothFeature();
                TheElderMythos.AtlachNacha.AddAtlachNachaFeature();
                TheElderMythos.Azathoth.AddAzathothFeature();
                TheElderMythos.Bokrug.AddBokrugFeature();
                TheElderMythos.ChaugnarFaugn.AddChaugnarFaugnFeature();
                TheElderMythos.Cthulhu.AddCthulhuFeature();
                TheElderMythos.Ghatanothoa.AddGhatanothoaFeature();
                TheElderMythos.Hastur.AddHasturFeature();
                TheElderMythos.Ithaqua.AddIthaquaFeature();
                TheElderMythos.Mhar.AddMharFeature();
                TheElderMythos.Mordiggian.AddMordiggianFeature();
                TheElderMythos.Nhimbaloth.AddNhimbalothFeature();
                TheElderMythos.Nyarlathotep.AddNyarlathotepFeature();
                TheElderMythos.Orgesh.AddOrgeshFeature();
                TheElderMythos.RhanTegoth.AddRhanTegothFeature();
                TheElderMythos.ShubNiggurath.AddShubNiggurathFeature();
                TheElderMythos.Tsathoggua.AddTsathogguaFeature();
                TheElderMythos.XhameDor.AddXhameDorFeature();
                TheElderMythos.Yig.AddYigFeature();
                TheElderMythos.YogSothoth.AddYogSothothFeature();

                Deities.Dretha.AddDrethaFeature();
                Deities.Lanishra.AddLanishraFeature();
                Deities.Nulgreth.AddNulgrethFeature();
                Deities.Rull.AddRullFeature();
                Deities.Sezelrian.AddSezelrianFeature();
                Deities.Varg.AddVargFeature();
                Deities.Verex.AddVerexFeature();
                Deities.Zagresh.AddZagreshFeature();
                
                Deities.PatchLichDeity.AddLichDeity();
                Deities.DeitySelectionFeature.PatchDeitySelection();
                Deities.DeitySelectionFeature.ArchdevilsToggle();
                Deities.DeitySelectionFeature.DeitiesofAncientOsirionToggle();
                Deities.DeitySelectionFeature.DeitiesofTianXiaToggle();
                Deities.DeitySelectionFeature.DemonLordToggle();
                Deities.DeitySelectionFeature.EmpyrealLordsToggle();
                Deities.DeitySelectionFeature.ElvenPantheonToggle();
                Deities.DeitySelectionFeature.DraconicDeityToggle();
                Deities.DeitySelectionFeature.TheEldestToggle();
                Deities.DeitySelectionFeature.MonitorsToggle();
                Deities.DeitySelectionFeature.TheElderMythosToggle();
                Deities.DeitySelectionFeature.OrcPantheonToggle();

                Miscellaneous.HavocDragonPet.AddHavocDragonPet();





            }
        }
    }
}
