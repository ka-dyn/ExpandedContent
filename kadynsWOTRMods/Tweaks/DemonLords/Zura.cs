using kadynsWOTRMods.Config;
using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Items;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kadynsWOTRMods.Tweaks.DemonLords {
	internal class Zura {


		private static readonly BlueprintFeature ChaosDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("8c7d778bc39fec642befc1435b00f613");
		private static readonly BlueprintFeature EvilDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("351235ac5fc2b7e47801f63d117b656c");
		private static readonly BlueprintFeature DeathDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("a099afe1b0b32554199b230699a69525");
		private static readonly BlueprintFeature MadnessDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("c346bcc77a6613040b3aa915b1ceddec");
		private static readonly BlueprintFeature CrusaderSpellbook = Resources.GetBlueprint<BlueprintFeature>("673d39f7da699aa408cdda6282e7dcc0");
		private static readonly BlueprintFeature ClericSpellbook = Resources.GetBlueprint<BlueprintFeature>("4673d19a0cf2fab4f885cc4d1353da33");
		private static readonly BlueprintFeature InquisitorSpellbook = Resources.GetBlueprint<BlueprintFeature>("57fab75111f377248810ece84193a5a5");
		private static readonly BlueprintFeature ChannelNegativeAllowed = Resources.GetBlueprint<BlueprintFeature>("dab5255d809f77c4395afc2b713e9cd6");
		private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
		private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
		private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");


		public static void AddZura()
		{

			if (ModSettings.AddedContent.DemonLords.IsDisabled("Zura")) { return; }

			BlueprintFeature RapierProficiency = Resources.GetBlueprint<BlueprintFeature>("292d51f3d6a331644a8c29be0614f671");
			BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
			BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
			BlueprintItem ColdIronRapier = Resources.GetBlueprint<BlueprintItem>("ec731c55e657cf0408fd89c648ccc536");

			var ZuraIcon = AssetLoader.LoadInternal("Deities", "Icon_Zura.png");
			var ZuraFeature = Helpers.CreateBlueprint<BlueprintFeature>("ZuraFeature", (bp => {

				bp.SetName("Zura");
				bp.SetDescription("\nTitles: The Vampire Queen, Demon Lord of Cannibalism and Vampires   " +
					"\nRealm: Nesh, The Abyss   " +
					"\nAlignment: Chaotic Evil   " +
					"\nAreas of Concern: Blood, Cannibalism, Vampires   " +
					"\nDomains: Chaos, Death, Evil, Madness  " +
					"\nSubdomains: Blood, Demon, Murder, Undead  " +
					"\nFavoured Weapon: Rapier  " +
					"\nProfane Symbol: Crimson fanged skull rune  " +
					"\nProfane Animal: Vampire bat  " +
					"\nProfane Colours: Red" +
					"\nThe demon lord Zura, is also known as the Vampire Queen, and is worshiped by " +
					"cannibals, drow, and of course, vampires. Her profane symbol is a blood-red, fanged skull, " +
					"embossed with runes. Zura is believed to be the first vampire in all of creation. Supposedly she was an ancient " +
					"Azlanti queen who possessed a lust for eternal life so great she eventually resorted to feeding on " +
					"her own kind. Even thousands of years later, people still whisper the legends of her decadent, " +
					"savage feasts and her baths of warm human blood. Some scholars even claim that her ascension heralded " +
					"the beginning of the ancient Azlant empire's descent into decadence and eventual destruction at the " +
					"beginning of the Age of Darkness. Her sinfulness was so great that, on her death by botched suicide, " +
					"her soul was sent to the Abyss, where she immediately arose as a succubus vampire. Her remaining " +
					"family members changed their names and went into hiding after their estates were plundered and " +
					"razed. Before Earthfall she had ascended to demon lord status. Zura now calls the mountainous Abyssal realm " +
					"of Nesh her home. Nesh has a large number of villages inhabited by captured humanoids harvested from the " +
					"Material Plane. They are, for the most part, unaware that they dwell deep in the Abyss, except the leaders, " +
					"who are rewarded by Zura's minions to keep the truth hidden.   " +
					"\nAppearance: She is said to appear as a voluptuous woman, with a dark gothic beauty that is almost impossible " +
					"to resist. This beautiful form is merely a ruse she uses to seduce her victims; her true form is that of an " +
					"emaciated woman with bat-like wings in place of arms, blood-red eyes and hair, large fangs, and taloned feet.");
				bp.m_Icon = ZuraIcon;
				bp.Ranks = 1;
				bp.IsClassFeature = true;
				bp.HideInCharacterSheetAndLevelUp = false;
				bp.AddComponent<PrerequisiteNoArchetype>(c => {
					c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
					c.m_Archetype = PriestOfBalance.ToReference<BlueprintArchetypeReference>();
				});
				bp.AddComponent<PrerequisiteNoArchetype>(c => {
					c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
					c.m_Archetype = FeralChampionArchetype.ToReference<BlueprintArchetypeReference>();
				});

				bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
				bp.AddComponent<PrerequisiteAlignment>(c => {
					c.Alignment = AlignmentMaskType.ChaoticEvil | AlignmentMaskType.NeutralEvil | AlignmentMaskType.ChaoticNeutral | AlignmentMaskType.TrueNeutral;
				});
				bp.AddComponent<AddFacts>(c => {
					c.m_Facts = new BlueprintUnitFactReference[1] { ChannelNegativeAllowed.ToReference<BlueprintUnitFactReference>() };
				});
				bp.AddComponent<AddFacts>(c => {
					c.m_Facts = new BlueprintUnitFactReference[1] { EvilDomainAllowed.ToReference<BlueprintUnitFactReference>() };
				});
				bp.AddComponent<AddFacts>(c => {
					c.m_Facts = new BlueprintUnitFactReference[1] { ChaosDomainAllowed.ToReference<BlueprintUnitFactReference>() };
				});
				bp.AddComponent<AddFacts>(c => {
					c.m_Facts = new BlueprintUnitFactReference[1] { DeathDomainAllowed.ToReference<BlueprintUnitFactReference>() };
				});
				bp.AddComponent<AddFacts>(c => {
					c.m_Facts = new BlueprintUnitFactReference[1] { MadnessDomainAllowed.ToReference<BlueprintUnitFactReference>() };
				});
				bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
					c.m_Spellbooks = new BlueprintSpellbookReference[1] { CrusaderSpellbook.ToReference<BlueprintSpellbookReference>() };
					c.m_Spellbooks = new BlueprintSpellbookReference[1] { ClericSpellbook.ToReference<BlueprintSpellbookReference>() };
					c.m_Spellbooks = new BlueprintSpellbookReference[1] { InquisitorSpellbook.ToReference<BlueprintSpellbookReference>() };
				});

				bp.AddComponent<AddFeatureOnClassLevel>(c => {
					c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();


					c.m_Feature = RapierProficiency.ToReference<BlueprintFeatureReference>();

					c.Level = 1;
					c.m_Archetypes = null;
					c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
							   InquistorClass.ToReference<BlueprintCharacterClassReference>(),
							   WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
				});
				bp.AddComponent<AddStartingEquipment>(c => {
					c.m_BasicItems = new BlueprintItemReference[1] { ColdIronRapier.ToReference<BlueprintItemReference>() };
					c.m_RestrictedByClass = new BlueprintCharacterClassReference[3] {
								ClericClass.ToReference<BlueprintCharacterClassReference>(),
								InquistorClass.ToReference<BlueprintCharacterClassReference>(),
								WarpriestClass.ToReference<BlueprintCharacterClassReference>()
					};
				});
			}));
			
		}
	}
}
	

