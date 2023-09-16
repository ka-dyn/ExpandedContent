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
    internal class Isis {
        private static readonly BlueprintFeature CharmDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("f1ceba79ee123cc479cece27bc994ff2");
        private static readonly BlueprintFeature CommunityDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("c87004460f3328c408d22c5ead05291f");
        private static readonly BlueprintFeature GoodDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("882521af8012fc749930b03dc18a69de");
        private static readonly BlueprintFeature HealingDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("73ae164c388990c43ade94cfe8ed5755");
        private static readonly BlueprintFeature MagicDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("08a5686378a87b64399d329ba4ef71b8");
        private static readonly BlueprintFeature AgathionDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("AgathionDomainAllowed");
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelPositiveAllowed = Resources.GetBlueprint<BlueprintFeature>("8c769102f3996684fb6e09a2c4e7e5b9");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
        private static readonly BlueprintCharacterClass PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
        private static readonly BlueprintFeature MythicIgnoreAlignmentRestrictions = Resources.GetBlueprintReference<BlueprintFeatureReference>("24e78475f0a243e1a810452d14d0a1bd");



        public static void AddIsisFeature() {

            BlueprintItem MasterworkQuarterstaff = Resources.GetBlueprint<BlueprintItem>("ad1a532601f8b644991d5012adccee6c");

            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintArchetype SilverChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SilverChampionArchetype");
            BlueprintArchetype SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");

            BlueprintFeature QuarterstaffProficiency = Resources.GetBlueprint<BlueprintFeature>("aed4f88b52ae0fb468895f90da854ad4");
            var IsisIcon = AssetLoader.LoadInternal("Deities", "Icon_Isis.jpg");
            var IsisFeature = Helpers.CreateBlueprint<BlueprintFeature>("IsisFeature", (bp => {

                bp.SetName("Isis");
                bp.SetDescription("\nTitles: Queen of Miracles   " +
                    "\nAlignment: Neutral Good   " +
                    "\nAreas of Concern: Fertility, Magic, Motherhood, Rebirth   " +
                    "\nDomains: Charm, Community, Good, Healing, Magic   " +
                    "\nSubdomains: Agathion, Arcane, Divine, Family, Love, Resurrection   " +
                    "\nFavoured Weapon: Quarterstaff   " +
                    "\nHoly Symbol: Knot of Isis   " +
                    "\nSacred Animal: Kite   " +
                    "\nWhen Set killed his brother Osiris, it was their sister Isis who recovered Osiris's body, and using her magic, " +
                    "conceived a son, Horus, with her dead husband. Enraged, Set then dismembered Osiris's corpse and scattered the remains, " +
                    "but Isis gathered up all of the pieces of her husband's body and with a magic spell resurrected Osiris. Isis ruled as queen " +
                    "alongside Osiris during the Age of Legend, and like him, she is a fertility and nature goddess, viewed as the ideal mother and " +
                    "wife . She is a goddess of rebirth and resurrection, and the protector of the canopic jar that holds the deceased's liver. She is " +
                    "also a deity of magic, both arcane and divine. Isis appears as a beautiful human woman with winged arms, wearing a crown shaped " +
                    "like a throne. She is the loyal wife and partner of Osiris, and is fiercely protective of her son Horus. Isis is close to her " +
                    "sister Nephthys, but is a sworn enemy of her brother Set. Isis is worshiped by sorcerers, wizards, and witches as a patron of magic, " +
                    "but also by mothers, wives, and druids in her role as a goddess of fertility. The vast majority of her clerics are women, but men and " +
                    "women both are initiated into her mystery cults to learn her secret and sacred rites.");
                bp.m_Icon = IsisIcon;
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
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SilverChampionArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.HideInUI = true;
                    c.m_CharacterClass = InquistorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.LawfulGood | AlignmentMaskType.NeutralGood | AlignmentMaskType.ChaoticGood | AlignmentMaskType.TrueNeutral;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ChfannelNegativeAllowed.ToReference<BlueprintUnitFactReference>(),
                        EviflDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        LawDfomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        AirDofmainAllowed.ToReference<BlueprintUnitFactReference>(),
                        DeathDfomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        DeathDfomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        DeathDfomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        DeathDfomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        WindDomfainAllowed.ToReference<BlueprintUnitFactReference>(),
                        UndeadDofmainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[] {
                        CrusaderSpellbook.ToReference<BlueprintSpellbookReference>(),
                        ClericSpellbook.ToReference<BlueprintSpellbookReference>(),
                        InquisitorSpellbook.ToReference<BlueprintSpellbookReference>()
                    };
                    c.m_IgnoreFact = MythicIgnoreAlignmentRestrictions.ToReference<BlueprintUnitFactReference>();
                    c.Alignment = gggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();


                    c.m_Feature = QuarterstaffProficiency.ToReference<BlueprintFeatureReference>();

                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                               InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                               WarpriestClass.ToReference<BlueprintCharacterClassReference>() };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[1] { MasterworkQuarterstaff.ToReference<BlueprintItemReference>() };
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
