using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.FactLogic;

namespace ExpandedContent.Tweaks.Deities {
    internal class Hathor {
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelPositiveAllowed = Resources.GetBlueprint<BlueprintFeature>("8c769102f3996684fb6e09a2c4e7e5b9");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
        private static readonly BlueprintFeature MythicIgnoreAlignmentRestrictions = Resources.GetBlueprint<BlueprintFeature>("24e78475f0a243e1a810452d14d0a1bd");



        public static void AddHathorFeature() {

            BlueprintItem MasterworkShortsword = Resources.GetBlueprint<BlueprintItem>("455c6750aac8fa94f8ab19f385c73ae7");

            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintArchetype SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");

            BlueprintFeature ShortswordProficiency = Resources.GetBlueprint<BlueprintFeature>("9e828934974f0fc4bbf7542eb0446e45");
            var HathorIcon = AssetLoader.LoadInternal("Deities", "Icon_Hathor.jpg");
            var HathorFeature = Helpers.CreateBlueprint<BlueprintFeature>("HathorFeature", (bp => {
                bp.SetName("Hathor");
                bp.SetDescription("\nTitles: Mistress of Jubilation   " +
                    "\nAlignment: Chaotic Good   " +
                    "\nAreas of Concern: Dance, Joy, Love, Music, The Sky   " +
                    "\nDomains: Air, Chaos, Charm, Good, Travel   " +
                    "\nSubdomains: Azata, Cloud, Exploration, Love, Revelry, Trade   " +
                    "\nFavoured Weapon: Shortsword   " +
                    "\nHoly Symbol: Solar Disk with Horns   " +
                    "\nSacred Animal: Cow   " +
                    "\nThe cow-headed goddess Hathor was worshiped from the earliest days of Ancient Osirion, and remained a popular deity " +
                    "throughout its history. A divinity of air and sky, Hathor is also a goddess of beauty and love, happiness and merriment. " +
                    "She is the queen of the dance, the mistress of music, and the sovereign of song. Hathor is a wife, a mother, and a lover, a " +
                    "goddess of fertility, sexuality, and motherhood. She is also a goddess of trade and goods that bring wealth from foreign lands. " +
                    "Hathor is the daughter of Ra, and sister to Bastet, Maat, and Sekhmet. She is the consort of Horus, and as he is a god of kings, so " +
                    "is she a goddess of queens, though she is venerated by royalty and commoners alike. Hathor appears as a voluptuous cow-headed woman " +
                    "with the solar disk of her father Ra between her horns and carrying a sistrum. Sometimes she is depicted as a human woman with a horned " +
                    "headdress. More rarely, she is depicted as a great wild cow whose milk nourishes the living. Hathor is a patron and protector of women, " +
                    "and is worshiped by mothers, wives, and lovers of all ages. She also serves as a patron of bards, dancers, musicians, and performers, which " +
                    "makes her a popular deity in thriving cities and their playhouses. In her role as a goddess of trade, Hathor is also a patron of miners, " +
                    "particularly those who mine for precious stones. Hathor's priesthood is predominantly female, and her temples hold great festivals full of " +
                    "music and dance where worshipers consume large amounts of beer and wine and revel for days on end.");
                bp.m_Icon = HathorIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.SetDisallowedArchetype(ClericClass, PriestOfBalance);
                bp.SetDisallowedArchetype(WarpriestClass, FeralChampionArchetype);
                bp.SetDisallowedArchetype(InquistorClass, SwornOfTheEldestArchetype);
                bp.DisallowDarkSister();
                bp.DisallowProphetOfPestilence();
                bp.MagicDeceiverLock();
                bp.DisallowNewMantisZealot();

                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.NeutralGood | AlignmentMaskType.ChaoticGood | AlignmentMaskType.ChaoticNeutral;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ChannelPositiveAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.SetAllowedDomains(
                    DeityTools.DomainAllowed.AirDomainAllowed,
                    DeityTools.DomainAllowed.ChaosDomainAllowed,
                    DeityTools.DomainAllowed.CharmDomainAllowed,
                    DeityTools.DomainAllowed.GoodDomainAllowed,
                    DeityTools.DomainAllowed.TravelDomainAllowed,
                    DeityTools.SeparatistDomainAllowed.AnimalDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.ArtificeDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.ArcaneDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.LightningDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.CommunityDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.InsectDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.DarknessDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.DeathDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.DestructionDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.EarthDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.FireDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.GloryDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.HealingDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.IceDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.KnowledgeDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.LiberationDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.LuckDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.MadnessDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.MagicDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.NobilityDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.PlantDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.ProtectionDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.ReposeDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.RuneDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.StrengthDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.SunDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.TrickeryDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.UndeadDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.WarDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.WaterDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.WeatherDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.AgathionDomainAllowedSeparatist,//Good
                    DeityTools.SeparatistDomainAllowed.ArchonDomainGoodAllowedSeparatist,//Good
                    DeityTools.SeparatistDomainAllowed.BloodDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.CavesDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.CurseDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.DefenseDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.DemonDomainChaosAllowedSeparatist,//Chaos
                    DeityTools.SeparatistDomainAllowed.DragonDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.FerocityDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.FistDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.FurDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.GrowthDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.HeroismDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.LustDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.PsychopompDomainDeathAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.PsychopompDomainReposeAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.RageDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.ResolveDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.RestorationDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.RevelationDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.RevolutionDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.RiversDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.ScalykindDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.StarsDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.StormDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.ThieveryDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.WhimsyDomainAllowedSeparatist,//Chaos
                    DeityTools.SeparatistDomainAllowed.WindDomainAllowedSeparatist,
                    DeityTools.SeparatistDomainAllowed.MurderDomainAllowedSeparatist

                );
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[] {
                        CrusaderSpellbook.ToReference<BlueprintSpellbookReference>(),
                        ClericSpellbook.ToReference<BlueprintSpellbookReference>(),
                        InquisitorSpellbook.ToReference<BlueprintSpellbookReference>()
                    };
                    c.m_IgnoreFact = MythicIgnoreAlignmentRestrictions.ToReference<BlueprintUnitFactReference>();
                    c.Alignment = AlignmentMaskType.NeutralGood | AlignmentMaskType.ChaoticGood | AlignmentMaskType.ChaoticNeutral;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();


                    c.m_Feature = ShortswordProficiency.ToReference<BlueprintFeatureReference>();

                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                               InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                               WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[1] { MasterworkShortsword.ToReference<BlueprintItemReference>() };
                    c.m_RestrictedByClass = new BlueprintCharacterClassReference[3] {
                                ClericClass.ToReference<BlueprintCharacterClassReference>(),
                                InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                                WarpriestClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
            }));
            DeityTools.LazySacredWeaponMaker("Hathor", HathorFeature, WeaponCategory.Shortsword);

        }
    }

}
