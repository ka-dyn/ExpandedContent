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

namespace ExpandedContent.Tweaks.Archdevils {
    internal class Baalzebul {

        private static readonly BlueprintFeature AirDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("6e5f4ff5a7010754ca78708ce1a9b233");
        private static readonly BlueprintFeature EvilDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("351235ac5fc2b7e47801f63d117b656c");
        private static readonly BlueprintFeature DeathDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("a099afe1b0b32554199b230699a69525");
        private static readonly BlueprintFeature LawDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("092714336606cfc45a37d2ab39fabfa8");
        private static readonly BlueprintFeature UndeadDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("UndeadDomainAllowed");
        private static readonly BlueprintFeature WindDomainAllowed = Resources.GetModBlueprint<BlueprintFeature>("WindDomainAllowed");
        private static readonly BlueprintSpellbook CrusaderSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("673d39f7da699aa408cdda6282e7dcc0");
        private static readonly BlueprintSpellbook ClericSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("4673d19a0cf2fab4f885cc4d1353da33");
        private static readonly BlueprintSpellbook InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");
        private static readonly BlueprintFeature ChannelNegativeAllowed = Resources.GetBlueprint<BlueprintFeature>("dab5255d809f77c4395afc2b713e9cd6");
        private static readonly BlueprintCharacterClass ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
        private static readonly BlueprintCharacterClass InquistorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
        private static readonly BlueprintCharacterClass WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
        private static readonly BlueprintCharacterClass DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
        private static readonly BlueprintFeature MythicIgnoreAlignmentRestrictions = Resources.GetBlueprint<BlueprintFeature>("24e78475f0a243e1a810452d14d0a1bd");



        public static void AddBaalzebul() {


            BlueprintFeature SpearProficiency = Resources.GetBlueprint<BlueprintFeature>("0c10b4bbcbcb6a942a77a6804682c7d8");
            BlueprintArchetype FeralChampionArchetype = Resources.GetBlueprint<BlueprintArchetype>("f68ca492c9c15e241ab73735fbd0fb9f");
            BlueprintArchetype PriestOfBalance = Resources.GetBlueprint<BlueprintArchetype>("a4560e3fb5d247d68fb1a2738fcc0855");
            BlueprintArchetype MantisZealotArchetype = Resources.GetModBlueprint<BlueprintArchetype>("MantisZealotArchetype");
            BlueprintArchetype ClawOfTheFalseWyrmArchetype = Resources.GetModBlueprint<BlueprintArchetype>("ClawOfTheFalseWyrmArchetype");
            BlueprintArchetype SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");

            BlueprintItem ColdIronSpear = Resources.GetBlueprint<BlueprintItem>("f18a9ed7bcba738479b97162b9c77f46");

            var BaalzebulIcon = AssetLoader.LoadInternal("Deities", "Icon_Baalzebul.jpg");
            var BaalzebulFeature = Helpers.CreateBlueprint<BlueprintFeature>("BaalzebulFeature", (bp => {

                bp.SetName("Baalzebul");
                bp.SetDescription("\nTitles: Lord of the Flies, Hell's Angel, The White Son, Lord of the Seventh   " +
                    "\nRealm: Cocytus " +
                    "\nAlignment: Lawful Evil   " +
                    "\nAreas of Concern: Arrogance, Flies, Lies   " +
                    "\nDomains: Air, Death, Evil, Law  " +
                    "\nSubdomains: Devil, Murder, Undead, Wind " +
                    "\nFavoured Weapon: Spear  " +
                    "\nProfane Symbol: Iron crown and diamond  " +
                    "\nProfane Animal: Fly  " +
                    "\nFew in Hell’s armies loathe the Heavens more than Baalzebul. A being of tarnished glory and wounded pride, the Lord of the Seventh was once a creature " +
                    "of luminous form and Asmodeus’s undisputed favorite. Though he is still among the most powerful figures in Hell, millennia of violence and disappointment " +
                    "have warped him into a creature of vicious jealousy and absolute arrogance. Known as the Lord of the Flies, Hell’s Angel, and the White Son, Baalzebul " +
                    "revels in hollow victories, enviously eyeing the gifts of others as he seeks a birthright that was never his. As Asmodeus’s champion, he won his lord " +
                    "countless victories and, when the time came for Exodus, he led those who followed in the Prince of Darkness’s path. In Hell, he expected to rule at his " +
                    "creator’s side, but such was not to be. The creation of Mephistopheles and distribution of Hell’s rule among all the archfiends infuriated Baalzebul, who " +
                    "had long awaited a far greater reward for his service. Forgetting himself, the Lord of That Which Flies railed a ainst his maker, demanding a realm far " +
                    "greater than those who were created after him. Offended, Asmodeus stripped Baalzebul of his magnificent form, fusing him instead with millions of biting " +
                    "flies. Horrified but cowed, Baalzebul fled back to his newly granted realm of Cocytus, taking his throne as Lord of the Flies. Baalzebul rules Cocytus not " +
                    "as the realm it is but as the realm he believes it should be, his every gesture and edict surrounded by mock ceremony and magniloquence. As Baalzebul " +
                    "possesses great knowledge of the ways of magic, many who seek to improve their fortunes through arcane means beseech him for aid, as do lords seeking " +
                    "greater power and any who seek revenge.");
                bp.m_Icon = BaalzebulIcon;
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
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = MantisZealotArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.HideInUI = true;
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ClawOfTheFalseWyrmArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.HideInUI = true;
                    c.m_CharacterClass = InquistorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.Deities };
                bp.AddComponent<PrerequisiteAlignment>(c => {
                    c.Alignment = AlignmentMaskType.LawfulEvil | AlignmentMaskType.NeutralEvil | AlignmentMaskType.LawfulNeutral;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ChannelNegativeAllowed.ToReference<BlueprintUnitFactReference>(),
                        EvilDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        LawDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        AirDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        DeathDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        WindDomainAllowed.ToReference<BlueprintUnitFactReference>(),
                        UndeadDomainAllowed.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<ForbidSpellbookOnAlignmentDeviation>(c => {
                    c.m_Spellbooks = new BlueprintSpellbookReference[] {
                        CrusaderSpellbook.ToReference<BlueprintSpellbookReference>(),
                        ClericSpellbook.ToReference<BlueprintSpellbookReference>(),
                        InquisitorSpellbook.ToReference<BlueprintSpellbookReference>()
                    };
                    c.m_IgnoreFact = MythicIgnoreAlignmentRestrictions.ToReference<BlueprintUnitFactReference>();
                    c.Alignment = AlignmentMaskType.LawfulEvil | AlignmentMaskType.NeutralEvil | AlignmentMaskType.LawfulNeutral;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Feature = SpearProficiency.ToReference<BlueprintFeatureReference>();
                    c.Level = 1;
                    c.m_Archetypes = null;
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[2] {
                        InquistorClass.ToReference<BlueprintCharacterClassReference>(),
                        WarpriestClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AddStartingEquipment>(c => {
                    c.m_BasicItems = new BlueprintItemReference[] {
                        ColdIronSpear.ToReference<BlueprintItemReference>()
                    };
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



