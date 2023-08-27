using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Utility;
using Kingmaker.UnitLogic.Mechanics.Properties;
using System.Linq;
using ExpandedContent.Tweaks.Components;
using Kingmaker.Assets.UnitLogic.Mechanics.Properties;
using Kingmaker.Blueprints.Classes.Prerequisites;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class DivineScourge {
        public static void AddDivineScourge() {

            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var CurseDomainProgression = Resources.GetModBlueprint<BlueprintProgression>("CurseDomainProgression");
            var CurseDomainProgressionSecondary = Resources.GetModBlueprint<BlueprintProgression>("CurseDomainProgressionSecondary");
            var ChannelEnergySelection = Resources.GetBlueprint<BlueprintFeatureSelection>("d332c1748445e8f4f9e92763123e31bd");
            var DomainsSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("48525e5da45c9c243a343fc6545dbdb9");
            var SecondDomainsSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("43281c3d7fe18cc4d91928395837cd1e");
            var WitchHexSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("9846043cf51251a4897728ed6e24e76f");
            var WitchHexEvilEyeSavesAbility = Resources.GetBlueprint<BlueprintAbility>("ba52aed3017521a4abafcbae4ee06d10");

            var DivineScourgeArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("DivineScourgeArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"DivineScourgeArchetype.Name", "Divine Scourge");
                bp.LocalizedDescription = Helpers.CreateString($"DivineScourgeArchetype.Description", "Some divine servants take on the role of dealing out unique punishments on " +
                    "behalf of their deities, taking pleasure in carrying out their sacrosanct duties. Divine scourges make a point of inflicting long-lasting maladies and curses " +
                    "on those deserving of such fates under the tenets of the scourges’ religions.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"DivineScourgeArchetype.Description", "Some divine servants take on the role of dealing out unique punishments " +
                    "on behalf of their deities, taking pleasure in carrying out their sacrosanct duties. Divine scourges make a point of inflicting long-lasting maladies and " +
                    "curses on those deserving of such fates under the tenets of the scourges’ religions.");
                
            });


            CurseDomainProgressionSecondary.AddComponent<PrerequisiteArchetypeLevel>(c => {
                c.Group = Prerequisite.GroupType.Any;
                c.HideInUI = true;
                c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = DivineScourgeArchetype.ToReference<BlueprintArchetypeReference>();
                c.Level = 1;
            });

            var DivineScourgeCurserBackupDomainSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DivineScourgeCurserBackupDomainSelection", bp => {
                bp.SetName("Curser");
                bp.SetDescription("A divine scourge must take the curse subdomain as a domain, regardless of the actual domains offered by her deity. The divine scourge does not receive a second domain." +
                    "\nIf the divine scourge already the curse subdomain from another class, they may select another domain or subdomain granted by their deity.");
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.OnlyNew;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_AllFeatures = SecondDomainsSelection.m_AllFeatures;
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.HideInUI = true;
                    c.m_Feature = CurseDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.HideInUI = true;
                    c.m_Feature = CurseDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
            });
            var DivineScourgeCurserFeature = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DivineScourgeCurserFeature", bp => {
                bp.SetName("Curser");
                bp.SetDescription("A divine scourge must take the curse subdomain as a domain, regardless of the actual domains offered by her deity. The divine scourge does not receive a second domain." +
                    "\nIf the divine scourge already the curse subdomain from another class, they may select another domain or subdomain granted by their deity.");
                bp.m_Icon = WitchHexEvilEyeSavesAbility.m_Icon;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.OnlyNew;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    CurseDomainProgressionSecondary.ToReference<BlueprintFeatureReference>(),
                    DivineScourgeCurserBackupDomainSelection.ToReference<BlueprintFeatureReference>()
                };
                bp.AddComponent<AddFacts>(c => {
                    // To support all features that check for domains this way
                    c.m_Facts = new BlueprintUnitFactReference[] { DomainsSelection.ToReference<BlueprintUnitFactReference>() };
                });
            });

            var WitchHexCackle = Resources.GetBlueprintReference<BlueprintFeatureReference>("36f2467103d4635459d412fb418276f4");
            var WitchHexEvilEye = Resources.GetBlueprintReference<BlueprintFeatureReference>("a977bcfb36169d447914f084793cac2c");
            var WitchHexMisfortune = Resources.GetBlueprintReference<BlueprintFeatureReference>("4e3dfdf000cc24d4fb96fd04e4e5cd0f");
            var WitchHexSlumber = Resources.GetBlueprintReference<BlueprintFeatureReference>("498d7668a6086454cb2def921b89bd8f");
            var WitchHexVulnerability = Resources.GetBlueprintReference<BlueprintFeatureReference>("e049c31708d932f4c8e7a366b1cd44a2");
            var WitchHexAgony = Resources.GetBlueprintReference<BlueprintFeatureReference>("de5f107ec5a340d4dbc333fd86a3cc06");
            var WitchHexDeliciousFright = Resources.GetBlueprintReference<BlueprintFeatureReference>("3d9c7a1ab0b57ab4d951c45f1f438136");
            var WitchHexHoarfrost = Resources.GetBlueprintReference<BlueprintFeatureReference>("b26dea1fc351b3441a4706b10d250188");
            var WitchHexRestlessSlumber = Resources.GetBlueprintReference<BlueprintFeatureReference>("0a31b4aba2b3bc24c85636cb8bdba1d9");




            var DivineScourgeDivineHexesFeature = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DivineScourgeDivineHexesFeature", bp => {
                bp.SetName("Divine Hexes");
                bp.SetDescription("At 3rd level and every 4 cleric levels thereafter, a divine scourge can select the following hexes from the witch class hex list, up to a maximum of five hexes at 19th " +
                    "level: cackle, evil eye, misfortune, slumber, and vulnerability. \nAt 11th level, a divine scourge can instead select from the following list of major hexes: agony, delicious fright, " +
                    "hoarfrost, and restless slumber. \nThe divine scourge uses her Wisdom modifier instead of her Intelligence modifier to determine the save DCs of her hexes.Any hex that refers to " +
                    "using her Intelligence modifier to determine its duration or effect instead uses her Charisma modifier for that purpose.");
                bp.m_Icon = WitchHexSelection.m_Icon;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.WitchHex;
                bp.AddFeatures(WitchHexCackle, WitchHexEvilEye, WitchHexMisfortune, WitchHexSlumber, WitchHexVulnerability);
            });
            var DivineScourgeDivineHexesImprovedFeature = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DivineScourgeDivineHexesImprovedFeature", bp => {
                bp.SetName("Divine Hexes");
                bp.SetDescription("At 3rd level and every 4 cleric levels thereafter, a divine scourge can select the following hexes from the witch class hex list, up to a maximum of five hexes at 19th " +
                    "level: cackle, evil eye, misfortune, slumber, and vulnerability. \nAt 11th level, a divine scourge can instead select from the following list of major hexes: agony, delicious fright, " +
                    "hoarfrost, and restless slumber. \nThe divine scourge uses her Wisdom modifier instead of her Intelligence modifier to determine the save DCs of her hexes.Any hex that refers to " +
                    "using her Intelligence modifier to determine its duration or effect instead uses her Charisma modifier for that purpose.");
                bp.m_Icon = WitchHexSelection.m_Icon;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideInUI = false;
                bp.HideNotAvailibleInUI = false;
                bp.ReapplyOnLevelUp = false;
                bp.Mode = SelectionMode.Default;
                bp.Group = FeatureGroup.WitchHex;
                bp.AddFeatures(WitchHexAgony, WitchHexCackle, WitchHexDeliciousFright, WitchHexEvilEye, WitchHexHoarfrost, WitchHexMisfortune, WitchHexRestlessSlumber, WitchHexSlumber, WitchHexVulnerability);
            });


            //New unit property, different to the others as divine scorge uses Cha and not casting stat
            var DivineScourgeHexFlag = Helpers.CreateBlueprint<BlueprintFeature>("DivineScourgeHexFlag", bp => {
                bp.SetName("DivineScourgeHexFlag");
                bp.SetDescription("");
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            var FakeDivineScourgeCastingStatProperty = Helpers.CreateBlueprint<BlueprintUnitProperty>("FakeDivineScourgeCastingStatProperty", bp => {
                bp.AddComponent<SimplePropertyGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs
                    };
                    c.Property = UnitProperty.StatBonusCharisma;
                });
                bp.AddComponent<FactRankGetter>(c => {
                    c.Settings = new PropertySettings() {
                        m_Progression = PropertySettings.Progression.AsIs
                    };
                    c.m_Fact = DivineScourgeHexFlag.ToReference<BlueprintUnitFactReference>();
                });
                bp.BaseValue = 1;
                bp.OperationOnComponents = BlueprintUnitProperty.MathOperation.Multiply;
            });



            //Abilities
            var WitchHexDeliciousFrightAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("e7489733ac7ccca40917d9364b406adb")
                .GetComponents<ContextRankConfig>()
                .Where(c => c.m_BaseValueType
                .Equals(ContextRankBaseValueType.MaxCustomProperty));
            WitchHexDeliciousFrightAbilityConfig.ForEach(c => {
                c.m_CustomPropertyList = c.m_CustomPropertyList.AppendToArray(FakeDivineScourgeCastingStatProperty.ToReference<BlueprintUnitPropertyReference>());
            });
            var WitchHexEvilEyeACAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("899b08dfc31868e4cb2c6287df9d355c").GetComponent<ContextRankConfig>();
            WitchHexEvilEyeACAbilityConfig.m_CustomPropertyList = WitchHexEvilEyeACAbilityConfig.m_CustomPropertyList.AppendToArray(FakeDivineScourgeCastingStatProperty.ToReference<BlueprintUnitPropertyReference>());
            var WitchHexEvilEyeAttackAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("954650e8a7542e642819716bb78bee86").GetComponent<ContextRankConfig>();
            WitchHexEvilEyeAttackAbilityConfig.m_CustomPropertyList = WitchHexEvilEyeAttackAbilityConfig.m_CustomPropertyList.AppendToArray(FakeDivineScourgeCastingStatProperty.ToReference<BlueprintUnitPropertyReference>());
            var WitchHexEvilEyeSavesAbilityConfig = Resources.GetBlueprint<BlueprintAbility>("ba52aed3017521a4abafcbae4ee06d10").GetComponent<ContextRankConfig>();
            WitchHexEvilEyeSavesAbilityConfig.m_CustomPropertyList = WitchHexEvilEyeSavesAbilityConfig.m_CustomPropertyList.AppendToArray(FakeDivineScourgeCastingStatProperty.ToReference<BlueprintUnitPropertyReference>());

            //Plugging into the normal witch hex stuff
            var WitchHexDCPropertyLevel = Resources.GetBlueprint<BlueprintUnitProperty>("bdc230ce338f427ba74de65597b0d57a").GetComponent<SummClassLevelGetter>();            
            WitchHexDCPropertyLevel.m_Class = WitchHexDCPropertyLevel.m_Class.AppendToArray(ClericClass.ToReference<BlueprintCharacterClassReference>());
            WitchHexDCPropertyLevel.m_Archetypes = WitchHexDCPropertyLevel.m_Archetypes.AppendToArray(DivineScourgeArchetype.ToReference<BlueprintArchetypeReference>());
            var WitchHexCasterLevelPropertyLevel = Resources.GetBlueprint<BlueprintUnitProperty>("2d2243f4f3654512bdda92e80ef65b6d").GetComponent<SummClassLevelGetter>();
            WitchHexCasterLevelPropertyLevel.m_Class = WitchHexCasterLevelPropertyLevel.m_Class.AppendToArray(ClericClass.ToReference<BlueprintCharacterClassReference>());
            WitchHexCasterLevelPropertyLevel.m_Archetypes = WitchHexCasterLevelPropertyLevel.m_Archetypes.AppendToArray(DivineScourgeArchetype.ToReference<BlueprintArchetypeReference>());
            var WitchHexSpellLevelPropertyLevel = Resources.GetBlueprint<BlueprintUnitProperty>("75efe8b64a3a4cd09dda28cef156cfb5").GetComponent<SummClassLevelGetter>();
            WitchHexSpellLevelPropertyLevel.m_Class = WitchHexSpellLevelPropertyLevel.m_Class.AppendToArray(ClericClass.ToReference<BlueprintCharacterClassReference>());
            WitchHexSpellLevelPropertyLevel.m_Archetypes = WitchHexSpellLevelPropertyLevel.m_Archetypes.AppendToArray(DivineScourgeArchetype.ToReference<BlueprintArchetypeReference>());
            WitchTools.AddClassWithArchetypeToHexConfigs(ClericClass, DivineScourgeArchetype);


            //WitchHexDCProperty is sorted in ModSupport.cs to pull in edits from other mods

            DivineScourgeArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, DomainsSelection, SecondDomainsSelection, ChannelEnergySelection)
            };
            DivineScourgeArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, DivineScourgeCurserFeature),
                    Helpers.LevelEntry(3, DivineScourgeHexFlag, DivineScourgeDivineHexesFeature),
                    Helpers.LevelEntry(7, DivineScourgeDivineHexesFeature),
                    Helpers.LevelEntry(11, DivineScourgeDivineHexesImprovedFeature),
                    Helpers.LevelEntry(15, DivineScourgeDivineHexesImprovedFeature),
                    Helpers.LevelEntry(19, DivineScourgeDivineHexesImprovedFeature)

            };
            ClericClass.Progression.UIGroups = ClericClass.Progression.UIGroups.AppendToArray(
                Helpers.CreateUIGroup(DivineScourgeDivineHexesFeature, DivineScourgeDivineHexesImprovedFeature)
            );
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Divine Scourge")) { return; }
            ClericClass.m_Archetypes = ClericClass.m_Archetypes.AppendToArray(DivineScourgeArchetype.ToReference<BlueprintArchetypeReference>());

        }
    }
}
