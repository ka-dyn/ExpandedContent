using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.AI.Blueprints;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.FactLogic;

namespace ExpandedContent.Tweaks.Miscellaneous {
    internal class HavocDragonPet {
        public static void AddHavocDragonPet() {
            var UnitDog = Resources.GetBlueprint<BlueprintUnit>("918939943bf32ba4a95470ea696c2ba5");
            var AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");
            var DragonAzataCompanionFeature = Resources.GetBlueprint<BlueprintFeature>("cf36f23d60987224696f03be70351928");
            var AzataDragonUnit = Resources.GetBlueprint<BlueprintUnit>("32a037e97c3d5c54b85da8f639616c57");
            var DragonAzataCompanionRank = Resources.GetBlueprint<BlueprintFeature>("2780764bf33c46745b11f0e1d2d20092");
            var AnimalCompanionSelectionBase = Resources.GetBlueprint<BlueprintFeatureSelection>("90406c575576aee40a34917a1b429254");
            var AnimalCompanionSelectionDomain = Resources.GetBlueprint<BlueprintFeatureSelection>("2ecd6c64683b59944a7fe544033bb533");
            var AnimalCompanionSelectionDruid = Resources.GetBlueprint<BlueprintFeatureSelection>("571f8434d98560c43935e132df65fe76");
            var AnimalCompanionSelectionHunter = Resources.GetBlueprint<BlueprintFeatureSelection>("715ac15eb8bd5e342bc8a0a3c9e3e38f");
            var AnimalCompanionSelectionMadDog = Resources.GetBlueprint<BlueprintFeatureSelection>("738b59d0b58187f4d846b0caaf0f80d7");
            var AnimalCompanionSelectionRanger = Resources.GetBlueprint<BlueprintFeatureSelection>("ee63330662126374e8785cc901941ac7");
            var AnimalCompanionSelectionSacredHuntsmaster = Resources.GetBlueprint<BlueprintFeatureSelection>("2995b36659b9ad3408fd26f137ee2c67");
            var AnimalCompanionSelectionSylvanSorcerer = Resources.GetBlueprint<BlueprintFeatureSelection>("a540d7dfe1e2a174a94198aba037274c");
            var AnimalCompanionSelectionUrbanHunter = Resources.GetBlueprint<BlueprintFeatureSelection>("257375cd139800e459d69ccfe4ca309c");
            var AnimalCompanionSelectionWildlandShaman = Resources.GetBlueprint<BlueprintFeatureSelection>("164c875d6b27483faba479c7f5261915");
            var ArcaneRiderMountSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("82c791d4790c45dcac6038ef6932c3e3");
            var BeastRiderMountSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("f7da602dae8944d499f00074c973c28a");
            var BloodriderMountSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("c81bb2aa48c113c4ba3ee8582eb058ea");
            var CavalierMountSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("0605927df6e2fdd42af6ee2424eb89f2");
            var NomadMountSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("8e1957da5a8144d1b0fcf8875ac6bab7");
            var OracleRevelationBondedMount = Resources.GetBlueprint<BlueprintFeatureSelection>("0234d0dd1cead22428e71a2500afa2e1");
            var PaladinDivineMountSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("e2f0e0efc9e155e43ba431984429678e");
            var ShamanNatureSpiritTrueSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("e7f4cfcd7488ac14bbc3e09426b59fd0");
            var SoheiMonasticMountHorseSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("918432cc97b146a4b93e2b3060bdd1ed");
            var DreadKnightCompanionSelection = Resources.GetModBlueprint<BlueprintFeatureSelection>("DreadKnightCompanionSelection");
            var CharacterBrain = Resources.GetBlueprint<BlueprintBrain>("cf986dd7ba9d4ec46ad8a3a0406d02ae");
            var AzataDragonUnitNPC_medium = Resources.GetBlueprint<BlueprintUnit>("3e56db348cc24838bc78b55a114e552a");
            var AzataDragonUnitNPC_large = Resources.GetBlueprint<BlueprintUnit>("a643ed45374b070468138d16815ca2df");

            var HavocDragonMediumUnit = Helpers.CreateBlueprint<BlueprintUnit>("HavocDragonMediumUnit", bp => {
                bp.m_Portrait = AzataDragonUnit.m_Portrait;
                bp.m_Faction = UnitDog.m_Faction;
                bp.m_Brain = UnitDog.m_Brain;
                bp.m_AddFacts = AzataDragonUnit.m_AddFacts;
                bp.Gender = AzataDragonUnit.Gender;
                bp.LocalizedName = AzataDragonUnit.LocalizedName;
                bp.Size = Size.Medium;
                bp.Color = AzataDragonUnit.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.Prefab = AzataDragonUnitNPC_medium.Prefab;
                bp.Visual = AzataDragonUnitNPC_medium.Visual;
                bp.FactionOverrides = AzataDragonUnit.FactionOverrides;
                bp.Body = AzataDragonUnitNPC_medium.Body;
                bp.Strength = AzataDragonUnit.Strength;
                bp.Dexterity = AzataDragonUnit.Dexterity;
                bp.Constitution = AzataDragonUnit.Constitution;
                bp.Intelligence = AzataDragonUnit.Intelligence;
                bp.Wisdom = AzataDragonUnit.Wisdom;
                bp.Charisma = AzataDragonUnit.Charisma;
                bp.Speed = AzataDragonUnit.Speed;
                bp.Skills = AzataDragonUnit.Skills;
                bp.m_DisplayName = AzataDragonUnit.m_DisplayName;
                bp.m_Description = AzataDragonUnit.m_Description;
                bp.m_DescriptionShort = AzataDragonUnit.m_DescriptionShort;
                bp.ComponentsArray = UnitDog.ComponentsArray;
            });
            var HavocDragonLargeUnit = Helpers.CreateBlueprint<BlueprintUnit>("HavocDragonLargeUnit", bp => {
                bp.m_Portrait = AzataDragonUnit.m_Portrait;
                bp.m_Faction = UnitDog.m_Faction;
                bp.m_Brain = UnitDog.m_Brain;
                bp.m_AddFacts = AzataDragonUnit.m_AddFacts;
                bp.Gender = AzataDragonUnit.Gender;
                bp.LocalizedName = AzataDragonUnit.LocalizedName;
                bp.Size = Size.Large;
                bp.Color = AzataDragonUnit.Color;
                bp.Alignment = Alignment.TrueNeutral;
                bp.Prefab = AzataDragonUnitNPC_medium.Prefab;
                bp.Visual = AzataDragonUnitNPC_medium.Visual;
                bp.FactionOverrides = AzataDragonUnit.FactionOverrides;
                bp.Body = AzataDragonUnitNPC_medium.Body;
                bp.Strength = AzataDragonUnit.Strength;
                bp.Dexterity = AzataDragonUnit.Dexterity;
                bp.Constitution = AzataDragonUnit.Constitution;
                bp.Intelligence = AzataDragonUnit.Intelligence;
                bp.Wisdom = AzataDragonUnit.Wisdom;
                bp.Charisma = AzataDragonUnit.Charisma;
                bp.Speed = AzataDragonUnit.Speed;
                bp.Skills = AzataDragonUnit.Skills;
                bp.m_DisplayName = AzataDragonUnit.m_DisplayName;
                bp.m_Description = AzataDragonUnit.m_Description;
                bp.m_DescriptionShort = AzataDragonUnit.m_DescriptionShort;
                bp.ComponentsArray = UnitDog.ComponentsArray;
            });
            var HavocIcon = AssetLoader.LoadInternal("Skills", "Icon_Havoc.png");
            var HavocDragonPetLarge = Helpers.CreateBlueprint<BlueprintFeature>("HavocDragonPetLarge", bp => {
                bp.SetName("Havoc Dragon Companion - Large");
                bp.SetDescription("This large dragon’s scales and insectile wings dance with color, while its whiplike tail waves as if stirred by an unseen breeze. " +
                "\nDespite their best intentions, the appropriately named havoc dragons often cause collateral damage as they develop whimsical wonderlands of revelry and relaxation.");
                bp.m_DescriptionShort = Helpers.CreateString("HavocDragonPet.DescriptionShort", "This dragon’s scales and insectile wings dance with color, while its whiplike tail waves as if stirred by an unseen breeze. " +
                "\nDespite their best intentions, the appropriately named havoc dragons often cause collateral damage as they develop whimsical wonderlands of revelry and relaxation.");
                bp.m_Icon = HavocIcon;
                bp.Ranks = 1;
                bp.Groups = new FeatureGroup[] { FeatureGroup.AnimalCompanion };
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddPet>(c => {
                    c.m_Pet = HavocDragonLargeUnit.ToReference<BlueprintUnitReference>();
                    c.m_LevelRank = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisitePet>(c => { c.NoCompanion = true; });
            });
            var HavocDragonPetMedium = Helpers.CreateBlueprint<BlueprintFeature>("HavocDragonPetMedium", bp => {
                bp.SetName("Havoc Dragon Companion - Medium");
                bp.SetDescription("This medium dragon’s scales and insectile wings dance with color, while its whiplike tail waves as if stirred by an unseen breeze. " +
                "\nDespite their best intentions, the appropriately named havoc dragons often cause collateral damage as they develop whimsical wonderlands of revelry and relaxation.");
                bp.m_DescriptionShort = Helpers.CreateString("HavocDragonPet.DescriptionShort", "This dragon’s scales and insectile wings dance with color, while its whiplike tail waves as if stirred by an unseen breeze. " +
                "\nDespite their best intentions, the appropriately named havoc dragons often cause collateral damage as they develop whimsical wonderlands of revelry and relaxation.");
                bp.m_Icon = HavocIcon;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.AnimalCompanion };
                bp.IsClassFeature = true;
                bp.AddComponent<AddPet>(c => {
                    c.m_Pet = HavocDragonMediumUnit.ToReference<BlueprintUnitReference>();
                    c.m_LevelRank = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisitePet>(c => { c.NoCompanion = true; });
            });

            //Seems to work
            //var CleaveMythicFeature = Resources.GetBlueprint<BlueprintFeature>("2ced576e-5eb4-48f2-93cf-502f338ee5d0");

            if (ModSettings.AddedContent.RetiredFeatures.IsDisabled("AivuPet")) { return; }
            AnimalCompanionSelectionBase.m_AllFeatures = AnimalCompanionSelectionBase.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            DreadKnightCompanionSelection.m_AllFeatures = DreadKnightCompanionSelection.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionDomain.m_AllFeatures = AnimalCompanionSelectionDomain.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionDruid.m_AllFeatures = AnimalCompanionSelectionDruid.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionHunter.m_AllFeatures = AnimalCompanionSelectionHunter.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionMadDog.m_AllFeatures = AnimalCompanionSelectionMadDog.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionRanger.m_AllFeatures = AnimalCompanionSelectionRanger.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionSacredHuntsmaster.m_AllFeatures = AnimalCompanionSelectionSacredHuntsmaster.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionSylvanSorcerer.m_AllFeatures = AnimalCompanionSelectionSylvanSorcerer.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionUrbanHunter.m_AllFeatures = AnimalCompanionSelectionUrbanHunter.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionWildlandShaman.m_AllFeatures = AnimalCompanionSelectionWildlandShaman.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            BeastRiderMountSelection.m_AllFeatures = BeastRiderMountSelection.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            BloodriderMountSelection.m_AllFeatures = BloodriderMountSelection.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            NomadMountSelection.m_AllFeatures = NomadMountSelection.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            OracleRevelationBondedMount.m_AllFeatures = OracleRevelationBondedMount.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            PaladinDivineMountSelection.m_AllFeatures = PaladinDivineMountSelection.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());

        }
    }
}
