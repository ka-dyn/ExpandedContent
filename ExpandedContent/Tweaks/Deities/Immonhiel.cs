using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.FactLogic;

namespace ExpandedContent.Tweaks.Deities {
    internal class Immonhiel {
        private static readonly BlueprintFeature ChaosDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("8c7d778bc39fec642befc1435b00f613");
        private static readonly BlueprintFeature GoodDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("882521af8012fc749930b03dc18a69de");
        private static readonly BlueprintFeature HealingDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("73ae164c388990c43ade94cfe8ed5755");
        private static readonly BlueprintFeature PlantDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("0e03c2a03222b0b42acf96096b286327");
        private static readonly BlueprintFeature RestorationDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("RestorationDomainAllowed");
        private static readonly BlueprintFeature GrowthDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("GrowthDomainAllowed");
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelPositiveAllowed = Resources.GetBlueprint<BlueprintFeature>("8c769102f3996684fb6e09a2c4e7e5b9");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
        private static readonly BlueprintFeature MythicIgnoreAlignmentRestrictions = Resources.GetBlueprint<BlueprintFeature>("24e78475f0a243e1a810452d14d0a1bd");



        public static void AddImmonhielFeature() {

            BlueprintItem MasterworkHandaxe = Resources.GetBlueprint<BlueprintItem>("8b9c3f3144b46b24f9574f7bedac9eae");

            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintArchetype SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");

            BlueprintFeature HandaxeProficiency = Resources.GetBlueprint<BlueprintFeature>("c59205a5d18930d4b88b74d2acda2f49");
            var ImmonhielIcon = AssetLoader.LoadInternal("Deities", "Icon_Immonhiel.jpg");
            var ImmonhielFeature = Helpers.CreateBlueprint<BlueprintFeature>("ImmonhielFeature", (bp => {

                bp.SetName("Immonhiel");
                bp.SetDescription("\nTitles: Balm-Bringer   " +
                    "\nAlignment: Chaotic Good   " +
                    "\nAreas of Concern: Herbs, Medicine, Toads   " +
                    "\nDomains: Chaos, Good, Healing, Plant   " +
                    "\nSubdomains: Azata, Growth, Restoration, Resurrection   " +
                    "\nFavoured Weapon: Handaxe   " +
                    "\nHoly Symbol: Herb-covered wooden toad   " +
                    "\nSacred Animal: Toad   " +
                    "\nImmonhiel appears as a regal woman of advanced age. Her moss-green hair hangs in two thick braids on either side of her lined " +
                    "face, and she has brown skin and dark, flashing eyes that hold great wisdom. She wears a soft leather vest fringed with emerald " +
                    "green silk and a leather skirt. The innumerable tiny pouches and glass vials that hang from a sash around her waist are rumored to " +
                    "contain cures to every malady known to mortals. The soothing empyreal lord's right arm is not flesh, but a knotty length of pine carved " +
                    "in the shape of an arm with long, capable-looking fingers at its end. Balm-Bringer is a healer on the eternal battlefields of the planes, " +
                    "where she nurses injured angels back to health and soothes the wounds of empyreal lords who clashed with the armies of evil and destruction. " +
                    "Her favored servants travel the mortal world, passing on herbal remedies and advice on how best to tend potent marsh plants, all the while " +
                    "teaching combat medics and battlefield healers the best techniques to ensure their warriors live to fight another day. Balm-Bringer also " +
                    "understands that some cannot be healed. For them, she provides ointments that take away the pain and grant peace until life's final moments. " +
                    "Many have need of Balm-Bringer, and she finds a home wherever she wanders. Nearly all the empyreal lords have hosted Immonhiel at some point, " +
                    "and many mortals have housed her without even knowing the nature of the stranger who slept under their roof.");
                bp.m_Icon = ImmonhielIcon;
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
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.HideInUI = true;
                    c.m_CharacterClass = InquistorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
                });

                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.NeutralGood | AlignmentMaskType.ChaoticGood | AlignmentMaskType.ChaoticNeutral;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ChannelPositiveAllowed.ToReference<BlueprintUnitFactReference>(),
                        ChaosDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        GoodDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        HealingDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        PlantDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        GrowthDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        RestorationDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
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


                    c.m_Feature = HandaxeProficiency.ToReference<BlueprintFeatureReference>();

                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                               InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                               WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[1] { MasterworkHandaxe.ToReference<BlueprintItemReference>() };
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
