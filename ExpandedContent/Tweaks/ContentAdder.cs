using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;

namespace ExpandedContent.Tweaks {
    class ContentAdder {
        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_Patch {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            public static void Postfix() {
                if (Initialized) return;
                Initialized = true;

                Miscellaneous.LazyLocker.AddLazyLocker();

                Miscellaneous.OpeningVolley.AddOpeningVolley();
                Miscellaneous.BullseyeShot.AddBullseyeShot();
                Miscellaneous.PinpointTargeting.AddPinpointTargeting();
                Miscellaneous.DemonHunter.AddDemonHunter();
                Miscellaneous.NobleScion.AddNobleScion();

                Classes.DrakeClass.DrakeCompanionClass.AddDrakeCompanionClass();
                Classes.DrakeClass.DrakeCompanionGreen.AddDrakeCompanionGreen();
                Classes.DrakeClass.DrakeCompanionSilver.AddDrakeCompanionSilver();
                Classes.DrakeClass.DrakeCompanionBlack.AddDrakeCompanionBlack();
                Classes.DrakeClass.DrakeCompanionBlue.AddDrakeCompanionBlue();
                Classes.DrakeClass.DrakeCompanionBrass.AddDrakeCompanionBrass();
                Classes.DrakeClass.DrakeCompanionBronze.AddDrakeCompanionBronze();
                Classes.DrakeClass.DrakeCompanionCopper.AddDrakeCompanionCopper();
                Classes.DrakeClass.DrakeCompanionRed.AddDrakeCompanionRed();
                Classes.DrakeClass.DrakeCompanionWhite.AddDrakeCompanionWhite();
                Classes.DrakeClass.DrakeCompanionGold.AddDrakeCompanionGold();
                Classes.DrakeClass.DrakeCompanionUmbral.AddDrakeCompanionUmbral();
                Classes.DrakeClass.DrakeSpells.AddDrakeSpells();
                Classes.DrakeClass.DrakeCompanionSelection.AddDrakeCompanionSelection();
                Classes.DrakeClass.DrakeMythicAbilities.AddDrakeMythicAbilities();

                AnimalCompanions.CompanionWolverine.AddCompanionWolverine();
                AnimalCompanions.CompanionGiantFly.AddCompanionGiantFly();
                AnimalCompanions.CompanionWebSpider.AddCompanionWebSpider();
                AnimalCompanions.CompanionSaplingTreant.AddCompanionSaplingTreant();
                AnimalCompanions.CompanionCrawlingMound.AddCompanionCrawlingMound();
                AnimalCompanions.ArchetypeDraconic.AddArchetypeDraconic();

                Miscellaneous.AlignmentTemplates.AddFiendishTemplate();
                Miscellaneous.Cavalier.AddCavalierFeatures();

                Domains.BaseDomainDruidPatch.AddBaseDomainDruidPatch();
             
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
                Archetypes.DraconicScholar.AddDraconicScholar();
                Archetypes.UrbanDruid.AddUrbanDruid();
                Archetypes.BearShaman.AddBearShaman();
                Archetypes.LionShaman.AddLionShaman();
                Archetypes.WyrmSinger.AddWyrmSinger();
                Archetypes.Archer.AddArcher();
                Archetypes.SpearFighter.AddSpearFighter();
                Archetypes.RavenerHunter.AddRavenerHunter();
                Archetypes.SkulkingHunter.AddSkulkingHunter();
                Archetypes.SwornOfTheEldest.AddSwornOfTheEldest();
                Archetypes.Mindchemist.AddMindchemist();
                Archetypes.ChildOfAcavnaAndAmaznen.AddChildOfAcavnaAndAmaznen();
                Archetypes.DivineTracker.AddDivineTracker();
                Archetypes.PlantMaster.AddPlantMaster();
                Archetypes.DrakeRider.AddDrakeRider();
                Archetypes.SoldierOfGaia.AddSoldierOfGaia();
                Archetypes.FaithfulParagon.AddFaithfulParagon();

                Archetypes.WaterDancer.AddWaterDancer();

                Classes.OathbreakerClass.AddOathbreakerClass();
                Archetypes.Castigator.AddCastigator();

                Classes.DreadKnightClass.AddDreadKnightClass();
                Archetypes.Conqueror.AddConqueror();
                Archetypes.ClawOfTheFalseWyrm.AddClawOfTheFalseWyrm();

                Archetypes.PrestigeClassSpellbooks.AddPrestigeClassSpellbooks();

                Classes.StargazerClass.AddStargazerClass();


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
                Spells.FinaleSetup.AddFinale();
                Spells.DeadlyFinale.AddDeadlyFinale();
                Spells.RevivingFinale.AddRevivingFinale();
                Spells.StunningFinale.AddStunningFinale();
                Spells.PurgingFinale.AddPurgingFinale();
                Spells.HollowBlades.AddHollowBlades();
                Spells.Goodberry.AddGoodberry();
                Spells.SteamRayFusillade.AddSteamRayFusillade();
                Spells.InflictPain.AddInflictPain();
                Spells.InflictPainMass.AddInflictPainMass();
                Spells.GloomblindBolts.AddGloomblindBolts();
                Spells.FuryOftheSun.AddFuryOftheSun();
                Spells.WallOfFire.AddWallOfFire();
                Spells.InvokeDeity.AddInvokeDeity();
                Spells.ZephyrsFleetness.AddZephyrsFleetness();
                Spells.HypnoticPattern.AddHypnoticPattern();
                Spells.EntropicShield.AddEntropicShield();
                Spells.ShieldOfFortification.AddShieldOfFortification();
                Spells.ShieldOfFortificationGreater.AddShieldOfFortificationGreater();
                Spells.ClaySkin.AddClaySkin();
                Spells.DanceOfAHundredCuts.AddDanceOfAHundredCuts();
                Spells.DanceOfAThousandCuts.AddDanceOfAThousandCuts();
                Spells.ParticulateForm.AddParticulateForm();
                Spells.Shillelagh.AddShillelagh();
                Spells.PlantShape.AddPlantShape();
                Spells.WoodenPhalanx.AddWoodenPhalanx();
                Spells.Shambler.AddShambler();
                Spells.ArcaneConcordance.AddArcaneConcordance();
                Spells.DustOfTwilight.AddDustOfTwilight();
                Spells.BloodMist.AddBloodMist();
                Spells.KingsCastle.AddKingsCastle();
                Spells.Moonstruck.AddMoonstruck();
                Spells.FormOfTheExoticDragon.AddFormOfTheExoticDragon();
                Spells.MomentOfPrescience.AddMomentOfPrescience();
                Spells.ShadowStep.AddShadowStep();
                Spells.ShadowJaunt.AddShadowJaunt();
                Spells.ShadowClaws.AddShadowClaws();
                Spells.MydriaticSpontaneity.AddMydriaticSpontaneity();
                Spells.MydriaticSpontaneityMass.AddMydriaticSpontaneityMass();
                Spells.BurstOfNettles.AddBurstOfNettles();
                Spells.EruptivePustules.AddEruptivePustules();
                Spells.TransmuteBloodToAcid.AddTransmuteBloodToAcid();
                Spells.VerminShape.AddVerminShape();
                Spells.Detonate.AddDetonate();
                Spells.IncendiaryCloud.AddIncendiaryCloud();
                Spells.TrialOfFireAndAcid.AddTrialOfFireAndAcid();
                Spells.CorrosiveConsumption.AddCorrosiveConsumption();

                Miscellaneous.AlchemistDiscoveries.MutagenDiscovery.AddMutagenDiscovery();
                Miscellaneous.AlchemistDiscoveries.MindchemistSkillDiscovery.AddMindchemistSkillDiscovery();
                Miscellaneous.AlchemistDiscoveries.HealingTouchDiscovery.AddHealingTouchDiscovery();
                Miscellaneous.AlchemistDiscoveries.PheromonesDiscovery.AddPheromonesDiccovery();

                Archetypes.Beastmorph.AddBeastmorph();

                Domains.DomainProperties.AddDomainProperties();
                Domains.ImpossibleSubdomainSelection.AddImpossibleSubdomainSelection();
                Domains.ScalykindDomain.AddScalykindDomain();
                Domains.ArchonDomainGood.AddArchonDomainGood();
                Domains.ArchonDomainLaw.AddArchonDomainLaw();
                Domains.BloodDomain.AddBloodDomain();
                Domains.CavesDomain.AddCavesDomain();
                Domains.CurseDomain.AddCurseDomain();
                Domains.DemonDomainChaos.AddDemonDomainChaos();
                Domains.DemonDomainEvil.AddDemonDomainEvil();
                Domains.DragonDomain.AddDragonDomain();
                Domains.FerocityDomain.AddFerocityDomain();
                Domains.IceDomain.AddIceDomain();
                Domains.PsychopompDomainDeath.AddPsychopompDomainDeath();
                Domains.PsychopompDomainRepose.AddPsychopompDomainRepose();
                Domains.RageDomain.AddRageDomain();
                Domains.RestorationDomain.AddRestorationDomain();
                Domains.RevelationDomain.AddRevelationDomain();
                Domains.RevolutionDomain.AddRevolutionDomain();
                Domains.RiversDomain.AddRiversDomain();
                Domains.StarsDomain.AddStarsDomain();
                Domains.StormDomain.AddStormDomain();
                Domains.ThieveryDomain.AddThieveryDomain();
                Domains.UndeadDomain.AddUndeadDomain();
                Domains.WhimsyDomain.AddWhimsyDomain();
                Domains.WindDomain.AddWindDomain();
                Domains.ResolveDomain.AddResolveDomain();
                Domains.AgathionDomain.AddAgathionDomain();
                Domains.LustDomain.AddLustDomain();
                Domains.FurDomain.AddFurDomain();
                Domains.DefenseDomain.AddDefenseDomain();
                Domains.HeroismDomain.AddHeroismDomain();
                Domains.GrowthDomain.AddGrowthDomain();
                Domains.PlantDomainPatch.PatchPlantDomain();
                Domains.FistDomain.AddFistDomain();
                Domains.LoyaltyDomain.AddLoyaltyDomain();
                Domains.ArcaneDomain.AddArcaneDomain();
                Domains.LightningDomain.AddLightningDomain();
                Domains.InsectDomain.AddInsectDomain();
                Domains.InsanityDomain.AddInsanityDomain();
                Domains.DuelsDomain.AddDuelsDomain();
                Domains.SmokeDomain.AddSmokeDomain();
                Domains.AshDomain.AddAshDomain();
                Domains.DivineDomain.AddDivineDomain();
                Domains.BaseDeityPatch.AddBaseDeityPatch();

                Blessings.ArtificeBlessing.AddArtificeBlessing();
                Blessings.WarBlessing.AddWarBlessing();
                Blessings.PlantBlessing.AddPlantBlessing();
                Blessings.CommunityBlessing.AddCommunityBlessing();

                Archetypes.StormDruid.AddStormDruid();
                Archetypes.Treesinger.AddTreesinger();
                Archetypes.DivineScourge.AddDivineScourge();

                Mysteries.MysteryProperties.AddMysteryProperties();
                Mysteries.DragonMystery.AddDragonMystery();
                Mysteries.HeavensMystery.AddHeavensMystery();
                Mysteries.SuccorMystery.AddSuccorMystery();
                Mysteries.WoodMystery.AddWoodMystery();
                Mysteries.WinterMystery.AddWinterMystery();
                Mysteries.LunarMystery.AddLunarMystery();
                Mysteries.ShadowMystery.AddShadowMystery();
                Mysteries.DarkTapestryMystery.AddDarkTapestryMystery();
                Mysteries.MetalMystery.AddMetalMystery();
                Mysteries.ApocalypseMystery.AddApocalypseMystery();

                Spirits.HeavensSpirit.AddHeavensSprit();
                Spirits.SlumsSpirit.AddSlumsSprit();
                Spirits.WoodSpirit.AddWoodSprit();

                Spirits.Hexes.BitingFrostHex.AddBitingFrostHex();
                Spirits.Hexes.DeathlyBeingHex.AddDeathlyBeingHex();
                Spirits.Hexes.EnchancedCuresHex.AddEnchancedCuresHex();
                Spirits.Hexes.ErosionCurseHex.AddErosionCurseHex();
                Spirits.Hexes.EyesOfBattleHex.AddEyesOfBattleHex();
                Spirits.Hexes.SluggishHex.AddSluggishHex();
                Spirits.Hexes.SparkingAuraHex.AddSparkingAuraHex();
                Spirits.Hexes.WardOfStoneHex.AddWardOfStoneHex();

                Curses.Vampirism.AddVampirismCurse();
                Curses.DeepOne.AddDeepOneCurse();
                Curses.Accursed.AddAccursedCurse();
                Curses.GodMeddled.AddGodMeddledCurse();
                Curses.Aboleth.AddAbolethCurse();
                Curses.Lich.AddLichCurse();

                Miscellaneous.AidAnother.AddAidAnother();

                Archdevils.Baalzebul.AddBaalzebul();
                Archdevils.Barbatos.AddBarbatos();
                Archdevils.Belial.AddBelial();
                Archdevils.Dispater.AddDispater();
                Archdevils.Geryon.AddGeryon();
                Archdevils.Mammon.AddMammon();
                Archdevils.Mephistopheles.AddMephistopheles();
                Archdevils.Moloch.AddMoloch();

                DemonLords.Areshkegal.AddAreshkegal();
                DemonLords.Deskari.AddDeskari();
                DemonLords.Kabriri.AddKabriri();
                DemonLords.Baphomet.AddBaphomet();
                DemonLords.Zura.AddZura();
                DemonLords.Dagon.AddDagonFeature();
                DemonLords.Treerazer.AddTreerazerFeature();
                DemonLords.Nocticula.AddNocticulaFeature();
                DemonLords.Pazuzu.AddPazuzuFeature();
                DemonLords.Shivaska.AddShivaskaFeature();
                DemonLords.Nurgal.AddNurgalFeature();
                DemonLords.Orcus.AddOrcusFeature();
                DemonLords.Mestama.AddMestamaFeature();
                DemonLords.Mazmezz.AddMazmezzFeature();
                DemonLords.Jubilex.AddJubilexFeature();
                DemonLords.Gogunta.AddGoguntaFeature();
                DemonLords.CythVsug.AddCythVsugFeature();
                DemonLords.Jezelda.AddJezeldaFeature();
                DemonLords.Shax.AddShaxFeature();
                DemonLords.Abraxas.AddAbraxasFeature();
                DemonLords.Aldinach.AddAldinachFeature();

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
                Deities.Naderi.AddNaderiFeature();
                Deities.Sivanah.AddSivanahFeature();
                Deities.Ghlaunder.AddGhlaunderFeature();
               
                Deities.PatchPulura.AddPulura();
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

                FourHorsemen.Apollyon.AddApollyonFeature();
                FourHorsemen.Charon.AddCharonFeature();
                FourHorsemen.Szuriel.AddSzurielFeature();
                FourHorsemen.Trelmarixian.AddTrelmarixianFeature();
                
                Deities.PatchLichDeity.AddLichDeity();
                Deities.DeitySelectionFeature.PatchDeitySelection();
                Deities.DeitySelectionFeature.ArchdevilsToggle();
                Deities.DeitySelectionFeature.DeitiesofAncientOsirionToggle();
                Deities.DeitySelectionFeature.DeitiesofTianXiaToggle();
                Deities.DeitySelectionFeature.DemonLordToggle();
                Deities.DeitySelectionFeature.EmpyrealLordsToggle();
                Deities.DeitySelectionFeature.ElvenPantheonToggle();
                //Deities.DeitySelectionFeature.DraconicDeityToggle();
                Deities.DeitySelectionFeature.TheEldestToggle();
                Deities.DeitySelectionFeature.MonitorsToggle();
                Deities.DeitySelectionFeature.TheElderMythosToggle();
                Deities.DeitySelectionFeature.OrcPantheonToggle();
                Deities.DeitySelectionFeature.InnerSeaDeitiesregionToggle();
                Deities.DeitySelectionFeature.FourHorsemenToggle();

                Items.AmuletEarlyRider.AddAmuletEarlyRider();

            }
            [HarmonyPriority(Priority.Last)]
            [HarmonyPostfix]
            public static void PatchAfter() {
                Miscellaneous.HavocDragonPet.AddHavocDragonPet();
                Miscellaneous.ShapechangeFeatsPatch.AddShapechangeFeatsPatch();

                Miscellaneous.BugFixes.AddBugFixes();
            }
        }
    }
}
