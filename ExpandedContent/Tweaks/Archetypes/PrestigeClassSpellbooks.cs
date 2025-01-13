using ExpandedContent.Utilities;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints;
using ExpandedContent.Extensions;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Buffs;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class PrestigeClassSpellbooks {
        public static void AddPrestigeClassSpellbooks() {


            var MysticTheurgeClass = Resources.GetBlueprint<BlueprintCharacterClass>("0920ea7e4fd7a404282e3d8b0ac41838");
            var MysticTheurgeDivineSpellbookSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("7cd057944ce7896479717778330a4933");
            var LoremasterClass = Resources.GetBlueprint<BlueprintCharacterClass>("4a7c05adfbaf05446a6bf664d28fb103");
            var LoremasterSpellbookSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("7a28ab4dfc010834eabc770152997e87");
            var HellknightSigniferClass = Resources.GetBlueprint<BlueprintCharacterClass>("ee6425d6392101843af35f756ce7fefd");
            var HellknightSigniferSpellbookSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("68782aa7a302b6d43a42a71c6e9b5277");

            #region Inquisitor
            var MysticTheurgeInquisitorLevelSelection1 = Resources.GetBlueprint<BlueprintFeatureSelection>("8ae18c62c0fbfeb4ea77f877883947fd");
            var MysticTheurgeInquisitorLevelSelection2 = Resources.GetBlueprint<BlueprintFeatureSelection>("f78f63d364bd9fe4ea2885d95432c068");
            var MysticTheurgeInquisitorLevelSelection3 = Resources.GetBlueprint<BlueprintFeatureSelection>("5f6c7b84edc68a146955be0600de4095");
            var MysticTheurgeInquisitorLevelSelection4 = Resources.GetBlueprint<BlueprintFeatureSelection>("b93df7bf0405a974cafcda21cbd070f1");
            var MysticTheurgeInquisitorLevelSelection5 = Resources.GetBlueprint<BlueprintFeatureSelection>("b7ed1fc44730bd1459c57763378a5a97");
            var MysticTheurgeInquisitorLevelSelection6 = Resources.GetBlueprint<BlueprintFeatureSelection>("200dec6712442e74c85803a1af72397a");     
            var InquisitorSpelllist = Resources.GetBlueprint<BlueprintSpellList>("57c894665b7895c499b3dce058c284b3");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var LivingScriptureArchetype = Resources.GetModBlueprint<BlueprintArchetype>("LivingScriptureArchetype");
            var RavenerHunterArchetype = Resources.GetModBlueprint<BlueprintArchetype>("RavenerHunterArchetype");            
            var SwornOfTheEldestArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SwornOfTheEldestArchetype");            
            var MysticTheurgeInquisitorProgression = Resources.GetBlueprint<BlueprintProgression>("d21a104c204ed7348a51405e68387013");
            var LoremasterInquisitorProgression = Resources.GetBlueprint<BlueprintProgression>("3528ad685de3e7d4ca03199a5e47a673");
            var HellknightSigniferInquisitorProgression = Resources.GetBlueprint<BlueprintProgression>("68f5313d09541fd48af0189a4d366e21");

            #region Living Scripture
            var LivingScriptureSpellbook = Resources.GetModBlueprint<BlueprintSpellbook>("LivingScriptureSpellbook");

            MysticTheurgeInquisitorProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = LivingScriptureArchetype.ToReference<BlueprintArchetypeReference>();
            });
            var MysticTheurgeLivingScriptureLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("MysticTheurgeLivingScriptureLevelUp", bp => {
                bp.SetName("Living Scripture");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = LivingScriptureSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var MysticTheurgeLivingScriptureProgression = Helpers.CreateBlueprint<BlueprintProgression>("MysticTheurgeLivingScriptureProgression", bp => {
                bp.SetName("Living Scripture");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 2;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = LivingScriptureArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.AddComponent<MysticTheurgeSpellbook>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_MysticTheurge = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.MysticTheurgeDivineSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, MysticTheurgeLivingScriptureLevelUp),
                    Helpers.LevelEntry(2, MysticTheurgeLivingScriptureLevelUp),
                    Helpers.LevelEntry(3, MysticTheurgeLivingScriptureLevelUp),
                    Helpers.LevelEntry(4, MysticTheurgeLivingScriptureLevelUp),
                    Helpers.LevelEntry(5, MysticTheurgeLivingScriptureLevelUp),
                    Helpers.LevelEntry(6, MysticTheurgeLivingScriptureLevelUp),
                    Helpers.LevelEntry(7, MysticTheurgeLivingScriptureLevelUp),
                    Helpers.LevelEntry(8, MysticTheurgeLivingScriptureLevelUp),
                    Helpers.LevelEntry(9, MysticTheurgeLivingScriptureLevelUp),
                    Helpers.LevelEntry(10, MysticTheurgeLivingScriptureLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            MysticTheurgeDivineSpellbookSelection.m_AllFeatures = MysticTheurgeDivineSpellbookSelection.m_AllFeatures.AppendToArray(MysticTheurgeLivingScriptureProgression.ToReference<BlueprintFeatureReference>());

            LoremasterInquisitorProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = LivingScriptureArchetype.ToReference<BlueprintArchetypeReference>();
            });
            var LoremasterLivingScriptureLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("LoremasterLivingScriptureLevelUp", bp => {
                bp.SetName("Living Scripture");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = LivingScriptureSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var LoremasterLivingScriptureProgression = Helpers.CreateBlueprint<BlueprintProgression>("LoremasterLivingScriptureProgression", bp => {
                bp.SetName("Living Scripture");
                bp.SetDescription("When a new loremaster level is gained, the character gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a " +
                    "spellcasting class he belonged to before adding the prestige class. He does not, however, gain other benefits a character of that class would have gained, " +
                    "except for additional spells per day, spells known (if he is a spontaneous spellcaster), and an increased effective level of spellcasting. If a character " +
                    "had more than one spellcasting class before becoming a loremaster, he must decide to which class he adds the new level for purposes of determining the " +
                    "number of spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = LivingScriptureArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ReplaceSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = LoremasterClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, LoremasterLivingScriptureLevelUp),
                    Helpers.LevelEntry(2, LoremasterLivingScriptureLevelUp),
                    Helpers.LevelEntry(3, LoremasterLivingScriptureLevelUp),
                    Helpers.LevelEntry(4, LoremasterLivingScriptureLevelUp),
                    Helpers.LevelEntry(5, LoremasterLivingScriptureLevelUp),
                    Helpers.LevelEntry(6, LoremasterLivingScriptureLevelUp),
                    Helpers.LevelEntry(7, LoremasterLivingScriptureLevelUp),
                    Helpers.LevelEntry(8, LoremasterLivingScriptureLevelUp),
                    Helpers.LevelEntry(9, LoremasterLivingScriptureLevelUp),
                    Helpers.LevelEntry(10, LoremasterLivingScriptureLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            LoremasterSpellbookSelection.m_AllFeatures = LoremasterSpellbookSelection.m_AllFeatures.AppendToArray(LoremasterLivingScriptureProgression.ToReference<BlueprintFeatureReference>());

            HellknightSigniferInquisitorProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = LivingScriptureArchetype.ToReference<BlueprintArchetypeReference>();
            });
            var HellknightSigniferLivingScriptureLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("HellknightSigniferLivingScriptureLevelUp", bp => {
                bp.SetName("Living Scripture");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = LivingScriptureSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var HellknightSigniferLivingScriptureProgression = Helpers.CreateBlueprint<BlueprintProgression>("HellknightSigniferLivingScriptureProgression", bp => {
                bp.SetName("Living Scripture");
                bp.SetDescription("At 1st level, and at every level thereafter, a Hellknight signifer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level " +
                    "in a spellcasting class he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, " +
                    "except for additional spells per day, spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before " +
                    "becoming a Hellknight signifer, he must decide to which class he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = LivingScriptureArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.HellknightSigniferSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = HellknightSigniferClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, HellknightSigniferLivingScriptureLevelUp),
                    Helpers.LevelEntry(2, HellknightSigniferLivingScriptureLevelUp),
                    Helpers.LevelEntry(3, HellknightSigniferLivingScriptureLevelUp),
                    Helpers.LevelEntry(4, HellknightSigniferLivingScriptureLevelUp),
                    Helpers.LevelEntry(5, HellknightSigniferLivingScriptureLevelUp),
                    Helpers.LevelEntry(6, HellknightSigniferLivingScriptureLevelUp),
                    Helpers.LevelEntry(7, HellknightSigniferLivingScriptureLevelUp),
                    Helpers.LevelEntry(8, HellknightSigniferLivingScriptureLevelUp),
                    Helpers.LevelEntry(9, HellknightSigniferLivingScriptureLevelUp),
                    Helpers.LevelEntry(10, HellknightSigniferLivingScriptureLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            HellknightSigniferSpellbookSelection.m_AllFeatures = HellknightSigniferSpellbookSelection.m_AllFeatures.AppendToArray(HellknightSigniferLivingScriptureProgression.ToReference<BlueprintFeatureReference>());

            #endregion

            #region Ravener Hunter            
            var RavenerHunterSpellbook = Resources.GetModBlueprint<BlueprintSpellbook>("RavenerHunterSpellbook");
            var RavenerHunterSpelllist= Resources.GetModBlueprint<BlueprintSpellList>("RavenerHunterSpelllist");

            var MysticTheurgeRavenerHunterLevelParametrized1 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("MysticTheurgeRavenerHunterLevelParametrized1", bp => {
                bp.SetName("Ravener Hunter Spell (1st Level)");
                bp.SetDescription("You can select new known ravener hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = RavenerHunterSpelllist.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 1;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = RavenerHunterSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 1;
                bp.DisallowSpellsInSpellList = false;
                //bp.BlueprintParameterVariants = MysticTheurgeInquisitorLevelParametrized1.BlueprintParameterVariants;
            });
            var MysticTheurgeRavenerHunterLevelParametrized2 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("MysticTheurgeRavenerHunterLevelParametrized2", bp => {
                bp.SetName("Ravener Hunter Spell (2nd Level)");
                bp.SetDescription("You can select new known ravener hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = RavenerHunterSpelllist.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 2;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = RavenerHunterSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 2;
                bp.DisallowSpellsInSpellList = false;
                //bp.BlueprintParameterVariants = MysticTheurgeInquisitorLevelParametrized1.BlueprintParameterVariants;
            });
            var MysticTheurgeRavenerHunterLevelParametrized3 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("MysticTheurgeRavenerHunterLevelParametrized3", bp => {
                bp.SetName("Ravener Hunter Spell (3rd Level)");
                bp.SetDescription("You can select new known ravener hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = RavenerHunterSpelllist.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 3;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = RavenerHunterSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 3;
                bp.DisallowSpellsInSpellList = false;
                //bp.BlueprintParameterVariants = MysticTheurgeInquisitorLevelParametrized1.BlueprintParameterVariants;
            });
            var MysticTheurgeRavenerHunterLevelParametrized4 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("MysticTheurgeRavenerHunterLevelParametrized4", bp => {
                bp.SetName("Ravener Hunter Spell (4th Level)");
                bp.SetDescription("You can select new known ravener hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = RavenerHunterSpelllist.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 4;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = RavenerHunterSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 4;
                bp.DisallowSpellsInSpellList = false;
                //bp.BlueprintParameterVariants = MysticTheurgeInquisitorLevelParametrized1.BlueprintParameterVariants;
            });
            var MysticTheurgeRavenerHunterLevelParametrized5 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("MysticTheurgeRavenerHunterLevelParametrized5", bp => {
                bp.SetName("Ravener Hunter Spell (5th Level)");
                bp.SetDescription("You can select new known ravener hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = RavenerHunterSpelllist.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 5;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = RavenerHunterSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 5;
                bp.DisallowSpellsInSpellList = false;
                //bp.BlueprintParameterVariants = MysticTheurgeInquisitorLevelParametrized1.BlueprintParameterVariants;
            });
            var MysticTheurgeRavenerHunterLevelParametrized6 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("MysticTheurgeRavenerHunterLevelParametrized6", bp => {
                bp.SetName("Ravener Hunter Spell (6th Level)");
                bp.SetDescription("You can select new known ravener hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = RavenerHunterSpelllist.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 6;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = RavenerHunterSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 6;
                bp.DisallowSpellsInSpellList = false;
                //bp.BlueprintParameterVariants = MysticTheurgeInquisitorLevelParametrized1.BlueprintParameterVariants;
            });

            var MysticTheurgeRavenerHunterLevelSelection1 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("MysticTheurgeRavenerHunterLevelSelection1", bp => {
                bp.SetName("Ravener Hunter Spell (1st Level)");
                bp.SetDescription("You can select new known ravener hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Mode = SelectionMode.Default;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    MysticTheurgeRavenerHunterLevelParametrized1.ToReference<BlueprintFeatureReference>()
                };
                bp.m_Features = new BlueprintFeatureReference[] {
                    MysticTheurgeRavenerHunterLevelParametrized1.ToReference<BlueprintFeatureReference>()
                };
            });
            var MysticTheurgeRavenerHunterLevelSelection2 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("MysticTheurgeRavenerHunterLevelSelection2", bp => {
                bp.SetName("Ravener Hunter Spell (2nd Level)");
                bp.SetDescription("You can select new known ravener hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Mode = SelectionMode.Default;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    MysticTheurgeRavenerHunterLevelParametrized2.ToReference<BlueprintFeatureReference>()
                };
                bp.m_Features = new BlueprintFeatureReference[] {
                    MysticTheurgeRavenerHunterLevelParametrized2.ToReference<BlueprintFeatureReference>()
                };
            });
            var MysticTheurgeRavenerHunterLevelSelection3 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("MysticTheurgeRavenerHunterLevelSelection3", bp => {
                bp.SetName("Ravener Hunter Spell (3rd Level)");
                bp.SetDescription("You can select new known ravener hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Mode = SelectionMode.Default;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    MysticTheurgeRavenerHunterLevelParametrized3.ToReference<BlueprintFeatureReference>()
                };
                bp.m_Features = new BlueprintFeatureReference[] {
                    MysticTheurgeRavenerHunterLevelParametrized3.ToReference<BlueprintFeatureReference>()
                };
            });
            var MysticTheurgeRavenerHunterLevelSelection4 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("MysticTheurgeRavenerHunterLevelSelection4", bp => {
                bp.SetName("Ravener Hunter Spell (4th Level)");
                bp.SetDescription("You can select new known ravener hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Mode = SelectionMode.Default;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    MysticTheurgeRavenerHunterLevelParametrized4.ToReference<BlueprintFeatureReference>()
                };
                bp.m_Features = new BlueprintFeatureReference[] {
                    MysticTheurgeRavenerHunterLevelParametrized4.ToReference<BlueprintFeatureReference>()
                };
            });
            var MysticTheurgeRavenerHunterLevelSelection5 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("MysticTheurgeRavenerHunterLevelSelection5", bp => {
                bp.SetName("Ravener Hunter Spell (5th Level)");
                bp.SetDescription("You can select new known ravener hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Mode = SelectionMode.Default;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    MysticTheurgeRavenerHunterLevelParametrized5.ToReference<BlueprintFeatureReference>()
                };
                bp.m_Features = new BlueprintFeatureReference[] {
                    MysticTheurgeRavenerHunterLevelParametrized5.ToReference<BlueprintFeatureReference>()
                };
            });
            var MysticTheurgeRavenerHunterLevelSelection6 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("MysticTheurgeRavenerHunterLevelSelection6", bp => {
                bp.SetName("Ravener Hunter Spell (6th Level)");
                bp.SetDescription("You can select new known ravener hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Mode = SelectionMode.Default;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    MysticTheurgeRavenerHunterLevelParametrized6.ToReference<BlueprintFeatureReference>()
                };
                bp.m_Features = new BlueprintFeatureReference[] {
                    MysticTheurgeRavenerHunterLevelParametrized6.ToReference<BlueprintFeatureReference>()
                };
            });

            MysticTheurgeInquisitorProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
            });
            var MysticTheurgeRavenerHunterLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("MysticTheurgeRavenerHunterLevelUp", bp => {
                bp.SetName("Ravener Hunter");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = RavenerHunterSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var MysticTheurgeRavenerHunterProgression = Helpers.CreateBlueprint<BlueprintProgression>("MysticTheurgeRavenerHunterProgression", bp => {
                bp.SetName("Ravener Hunter");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 2;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = LivingScriptureArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.AddComponent<MysticTheurgeSpellbook>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_MysticTheurge = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.MysticTheurgeDivineSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>() },
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(5, MysticTheurgeRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection2),
                    Helpers.LevelEntry(6, MysticTheurgeRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection2),
                    Helpers.LevelEntry(7, MysticTheurgeRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection1, MysticTheurgeRavenerHunterLevelSelection3, MysticTheurgeRavenerHunterLevelSelection3),
                    Helpers.LevelEntry(8, MysticTheurgeRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection3),
                    Helpers.LevelEntry(9, MysticTheurgeRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection3),
                    Helpers.LevelEntry(10, MysticTheurgeRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection2, MysticTheurgeRavenerHunterLevelSelection4, MysticTheurgeRavenerHunterLevelSelection4),
                    Helpers.LevelEntry(11, MysticTheurgeRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection1, MysticTheurgeRavenerHunterLevelSelection4),
                    Helpers.LevelEntry(12, MysticTheurgeRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection4),
                    Helpers.LevelEntry(13, MysticTheurgeRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection3, MysticTheurgeRavenerHunterLevelSelection5, MysticTheurgeRavenerHunterLevelSelection5),
                    Helpers.LevelEntry(14, MysticTheurgeRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection2, MysticTheurgeRavenerHunterLevelSelection5),
                    Helpers.LevelEntry(15, MysticTheurgeRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection5),
                    Helpers.LevelEntry(16, MysticTheurgeRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection4, MysticTheurgeRavenerHunterLevelSelection6, MysticTheurgeRavenerHunterLevelSelection6),
                    Helpers.LevelEntry(17, MysticTheurgeRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection3, MysticTheurgeRavenerHunterLevelSelection6),
                    Helpers.LevelEntry(18, MysticTheurgeRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection6),
                    Helpers.LevelEntry(19, MysticTheurgeRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection5),
                    Helpers.LevelEntry(20, MysticTheurgeRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection4, MysticTheurgeRavenerHunterLevelSelection6)
                };
                bp.GiveFeaturesForPreviousLevels = false;
                bp.m_ExclusiveProgression = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>();
            });
            MysticTheurgeDivineSpellbookSelection.m_AllFeatures = MysticTheurgeDivineSpellbookSelection.m_AllFeatures.AppendToArray(MysticTheurgeRavenerHunterProgression.ToReference<BlueprintFeatureReference>());

            LoremasterInquisitorProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
            });
            var LoremasterRavenerHunterLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("LoremasterRavenerHunterLevelUp", bp => {
                bp.SetName("Ravener Hunter");
                bp.SetDescription("At 1st level, the loremaster selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new loremaster level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = RavenerHunterSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var LoremasterRavenerHunterProgression = Helpers.CreateBlueprint<BlueprintProgression>("LoremasterRavenerHunterProgression", bp => {
                bp.SetName("Ravener Hunter");
                bp.SetDescription("When a new loremaster level is gained, the character gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a " +
                    "spellcasting class he belonged to before adding the prestige class. He does not, however, gain other benefits a character of that class would have gained, " +
                    "except for additional spells per day, spells known (if he is a spontaneous spellcaster), and an increased effective level of spellcasting. If a character " +
                    "had more than one spellcasting class before becoming a loremaster, he must decide to which class he adds the new level for purposes of determining the " +
                    "number of spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = LivingScriptureArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ReplaceSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = LoremasterClass.ToReference<BlueprintCharacterClassReference>() },
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(5, LoremasterRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection2),
                    Helpers.LevelEntry(6, LoremasterRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection2),
                    Helpers.LevelEntry(7, LoremasterRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection1, MysticTheurgeRavenerHunterLevelSelection3, MysticTheurgeRavenerHunterLevelSelection3),
                    Helpers.LevelEntry(8, LoremasterRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection3),
                    Helpers.LevelEntry(9, LoremasterRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection3),
                    Helpers.LevelEntry(10, LoremasterRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection2, MysticTheurgeRavenerHunterLevelSelection4, MysticTheurgeRavenerHunterLevelSelection4),
                    Helpers.LevelEntry(11, LoremasterRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection1, MysticTheurgeRavenerHunterLevelSelection4),
                    Helpers.LevelEntry(12, LoremasterRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection4),
                    Helpers.LevelEntry(13, LoremasterRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection3, MysticTheurgeRavenerHunterLevelSelection5, MysticTheurgeRavenerHunterLevelSelection5),
                    Helpers.LevelEntry(14, LoremasterRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection2, MysticTheurgeRavenerHunterLevelSelection5),
                    Helpers.LevelEntry(15, LoremasterRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection5),
                    Helpers.LevelEntry(16, LoremasterRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection4, MysticTheurgeRavenerHunterLevelSelection6, MysticTheurgeRavenerHunterLevelSelection6),
                    Helpers.LevelEntry(17, LoremasterRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection3, MysticTheurgeRavenerHunterLevelSelection6),
                    Helpers.LevelEntry(18, LoremasterRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection6),
                    Helpers.LevelEntry(19, LoremasterRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection5),
                    Helpers.LevelEntry(20, LoremasterRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection4, MysticTheurgeRavenerHunterLevelSelection6)
                };
                bp.GiveFeaturesForPreviousLevels = false;
                bp.m_ExclusiveProgression = LoremasterClass.ToReference<BlueprintCharacterClassReference>();
            });
            LoremasterSpellbookSelection.m_AllFeatures = LoremasterSpellbookSelection.m_AllFeatures.AppendToArray(LoremasterRavenerHunterProgression.ToReference<BlueprintFeatureReference>());

            HellknightSigniferInquisitorProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
            });
            var HellknightSigniferRavenerHunterLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("HellknightSigniferRavenerHunterLevelUp", bp => {
                bp.SetName("Ravener Hunter");
                bp.SetDescription("At 1st level, the hellknight signifer selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new hellknight signifer level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = RavenerHunterSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var HellknightSigniferRavenerHunterProgression = Helpers.CreateBlueprint<BlueprintProgression>("HellknightSigniferRavenerHunterProgression", bp => {
                bp.SetName("Ravener Hunter");
                bp.SetDescription("At 1st level, and at every level thereafter, a Hellknight signifer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level " +
                    "in a spellcasting class he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, " +
                    "except for additional spells per day, spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before " +
                    "becoming a Hellknight signifer, he must decide to which class he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = RavenerHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = LivingScriptureArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.HellknightSigniferSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = HellknightSigniferClass.ToReference<BlueprintCharacterClassReference>() },
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(5, HellknightSigniferRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection2),
                    Helpers.LevelEntry(6, HellknightSigniferRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection2),
                    Helpers.LevelEntry(7, HellknightSigniferRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection1, MysticTheurgeRavenerHunterLevelSelection3, MysticTheurgeRavenerHunterLevelSelection3),
                    Helpers.LevelEntry(8, HellknightSigniferRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection3),
                    Helpers.LevelEntry(9, HellknightSigniferRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection3),
                    Helpers.LevelEntry(10, HellknightSigniferRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection2, MysticTheurgeRavenerHunterLevelSelection4, MysticTheurgeRavenerHunterLevelSelection4),
                    Helpers.LevelEntry(11, HellknightSigniferRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection1, MysticTheurgeRavenerHunterLevelSelection4),
                    Helpers.LevelEntry(12, HellknightSigniferRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection4),
                    Helpers.LevelEntry(13, HellknightSigniferRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection3, MysticTheurgeRavenerHunterLevelSelection5, MysticTheurgeRavenerHunterLevelSelection5),
                    Helpers.LevelEntry(14, HellknightSigniferRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection2, MysticTheurgeRavenerHunterLevelSelection5),
                    Helpers.LevelEntry(15, HellknightSigniferRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection5),
                    Helpers.LevelEntry(16, HellknightSigniferRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection4, MysticTheurgeRavenerHunterLevelSelection6, MysticTheurgeRavenerHunterLevelSelection6),
                    Helpers.LevelEntry(17, HellknightSigniferRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection3, MysticTheurgeRavenerHunterLevelSelection6),
                    Helpers.LevelEntry(18, HellknightSigniferRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection6),
                    Helpers.LevelEntry(19, HellknightSigniferRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection5),
                    Helpers.LevelEntry(20, HellknightSigniferRavenerHunterLevelUp, MysticTheurgeRavenerHunterLevelSelection4, MysticTheurgeRavenerHunterLevelSelection6)
                };
                bp.GiveFeaturesForPreviousLevels = false;
                bp.m_ExclusiveProgression = HellknightSigniferClass.ToReference<BlueprintCharacterClassReference>();
            });
            HellknightSigniferSpellbookSelection.m_AllFeatures = HellknightSigniferSpellbookSelection.m_AllFeatures.AppendToArray(HellknightSigniferRavenerHunterProgression.ToReference<BlueprintFeatureReference>());

            #endregion

            #region Sworn of the Eldest            
            var SwornOfTheEldestSpellbook = Resources.GetModBlueprint<BlueprintSpellbook>("SwornOfTheEldestSpellbook");            

            MysticTheurgeInquisitorProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
            });
            var MysticTheurgeSwornOfTheEldestLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("MysticTheurgeSwornOfTheEldestLevelUp", bp => {
                bp.SetName("Sworn of the Eldest");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = SwornOfTheEldestSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var MysticTheurgeSwornOfTheEldestProgression = Helpers.CreateBlueprint<BlueprintProgression>("MysticTheurgeSwornOfTheEldestProgression", bp => {
                bp.SetName("Sworn of the Eldest");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 2;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = LivingScriptureArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.AddComponent<MysticTheurgeSpellbook>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_MysticTheurge = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.MysticTheurgeDivineSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>() },
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(5, MysticTheurgeSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection2),
                    Helpers.LevelEntry(6, MysticTheurgeSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection2),
                    Helpers.LevelEntry(7, MysticTheurgeSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection1, MysticTheurgeInquisitorLevelSelection3, MysticTheurgeInquisitorLevelSelection3),
                    Helpers.LevelEntry(8, MysticTheurgeSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection3),
                    Helpers.LevelEntry(9, MysticTheurgeSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection3),
                    Helpers.LevelEntry(10, MysticTheurgeSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection2, MysticTheurgeInquisitorLevelSelection4, MysticTheurgeInquisitorLevelSelection4),
                    Helpers.LevelEntry(11, MysticTheurgeSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection1, MysticTheurgeInquisitorLevelSelection4),
                    Helpers.LevelEntry(12, MysticTheurgeSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection4),
                    Helpers.LevelEntry(13, MysticTheurgeSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection3, MysticTheurgeInquisitorLevelSelection5, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(14, MysticTheurgeSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection2, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(15, MysticTheurgeSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(16, MysticTheurgeSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection4, MysticTheurgeInquisitorLevelSelection6, MysticTheurgeInquisitorLevelSelection6),
                    Helpers.LevelEntry(17, MysticTheurgeSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection3, MysticTheurgeInquisitorLevelSelection6),
                    Helpers.LevelEntry(18, MysticTheurgeSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection6),
                    Helpers.LevelEntry(19, MysticTheurgeSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(20, MysticTheurgeSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection4, MysticTheurgeInquisitorLevelSelection6)
                };
                bp.GiveFeaturesForPreviousLevels = false;
                bp.m_ExclusiveProgression = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>();
            });
            MysticTheurgeDivineSpellbookSelection.m_AllFeatures = MysticTheurgeDivineSpellbookSelection.m_AllFeatures.AppendToArray(MysticTheurgeSwornOfTheEldestProgression.ToReference<BlueprintFeatureReference>());

            LoremasterInquisitorProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
            });
            var LoremasterSwornOfTheEldestLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("LoremasterSwornOfTheEldestLevelUp", bp => {
                bp.SetName("Sworn of the Eldest");
                bp.SetDescription("At 1st level, the loremaster selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new loremaster level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = SwornOfTheEldestSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var LoremasterSwornOfTheEldestProgression = Helpers.CreateBlueprint<BlueprintProgression>("LoremasterSwornOfTheEldestProgression", bp => {
                bp.SetName("Sworn of the Eldest");
                bp.SetDescription("When a new loremaster level is gained, the character gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a " +
                    "spellcasting class he belonged to before adding the prestige class. He does not, however, gain other benefits a character of that class would have gained, " +
                    "except for additional spells per day, spells known (if he is a spontaneous spellcaster), and an increased effective level of spellcasting. If a character " +
                    "had more than one spellcasting class before becoming a loremaster, he must decide to which class he adds the new level for purposes of determining the " +
                    "number of spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = LivingScriptureArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ReplaceSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = LoremasterClass.ToReference<BlueprintCharacterClassReference>() },
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(5, LoremasterSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection2),
                    Helpers.LevelEntry(6, LoremasterSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection2),
                    Helpers.LevelEntry(7, LoremasterSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection1, MysticTheurgeInquisitorLevelSelection3, MysticTheurgeInquisitorLevelSelection3),
                    Helpers.LevelEntry(8, LoremasterSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection3),
                    Helpers.LevelEntry(9, LoremasterSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection3),
                    Helpers.LevelEntry(10, LoremasterSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection2, MysticTheurgeInquisitorLevelSelection4, MysticTheurgeInquisitorLevelSelection4),
                    Helpers.LevelEntry(11, LoremasterSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection1, MysticTheurgeInquisitorLevelSelection4),
                    Helpers.LevelEntry(12, LoremasterSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection4),
                    Helpers.LevelEntry(13, LoremasterSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection3, MysticTheurgeInquisitorLevelSelection5, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(14, LoremasterSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection2, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(15, LoremasterSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(16, LoremasterSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection4, MysticTheurgeInquisitorLevelSelection6, MysticTheurgeInquisitorLevelSelection6),
                    Helpers.LevelEntry(17, LoremasterSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection3, MysticTheurgeInquisitorLevelSelection6),
                    Helpers.LevelEntry(18, LoremasterSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection6),
                    Helpers.LevelEntry(19, LoremasterSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(20, LoremasterSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection4, MysticTheurgeInquisitorLevelSelection6)
                };
                bp.GiveFeaturesForPreviousLevels = false;
                bp.m_ExclusiveProgression = LoremasterClass.ToReference<BlueprintCharacterClassReference>();
            });
            LoremasterSpellbookSelection.m_AllFeatures = LoremasterSpellbookSelection.m_AllFeatures.AppendToArray(LoremasterSwornOfTheEldestProgression.ToReference<BlueprintFeatureReference>());

            HellknightSigniferInquisitorProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
            });
            var HellknightSigniferSwornOfTheEldestLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("HellknightSigniferSwornOfTheEldestLevelUp", bp => {
                bp.SetName("Sworn of the Eldest");
                bp.SetDescription("At 1st level, the hellknight signifer selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new hellknight signifer level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = SwornOfTheEldestSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var HellknightSigniferSwornOfTheEldestProgression = Helpers.CreateBlueprint<BlueprintProgression>("HellknightSigniferSwornOfTheEldestProgression", bp => {
                bp.SetName("Sworn of the Eldest");
                bp.SetDescription("At 1st level, and at every level thereafter, a Hellknight signifer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level " +
                    "in a spellcasting class he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, " +
                    "except for additional spells per day, spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before " +
                    "becoming a Hellknight signifer, he must decide to which class he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SwornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = LivingScriptureArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.HellknightSigniferSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = HellknightSigniferClass.ToReference<BlueprintCharacterClassReference>() },
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(5, HellknightSigniferSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection2),
                    Helpers.LevelEntry(6, HellknightSigniferSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection2),
                    Helpers.LevelEntry(7, HellknightSigniferSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection1, MysticTheurgeInquisitorLevelSelection3, MysticTheurgeInquisitorLevelSelection3),
                    Helpers.LevelEntry(8, HellknightSigniferSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection3),
                    Helpers.LevelEntry(9, HellknightSigniferSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection3),
                    Helpers.LevelEntry(10, HellknightSigniferSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection2, MysticTheurgeInquisitorLevelSelection4, MysticTheurgeInquisitorLevelSelection4),
                    Helpers.LevelEntry(11, HellknightSigniferSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection1, MysticTheurgeInquisitorLevelSelection4),
                    Helpers.LevelEntry(12, HellknightSigniferSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection4),
                    Helpers.LevelEntry(13, HellknightSigniferSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection3, MysticTheurgeInquisitorLevelSelection5, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(14, HellknightSigniferSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection2, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(15, HellknightSigniferSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(16, HellknightSigniferSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection4, MysticTheurgeInquisitorLevelSelection6, MysticTheurgeInquisitorLevelSelection6),
                    Helpers.LevelEntry(17, HellknightSigniferSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection3, MysticTheurgeInquisitorLevelSelection6),
                    Helpers.LevelEntry(18, HellknightSigniferSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection6),
                    Helpers.LevelEntry(19, HellknightSigniferSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection5),
                    Helpers.LevelEntry(20, HellknightSigniferSwornOfTheEldestLevelUp, MysticTheurgeInquisitorLevelSelection4, MysticTheurgeInquisitorLevelSelection6)
                };
                bp.GiveFeaturesForPreviousLevels = false;
                bp.m_ExclusiveProgression = HellknightSigniferClass.ToReference<BlueprintCharacterClassReference>();
            });
            HellknightSigniferSpellbookSelection.m_AllFeatures = HellknightSigniferSpellbookSelection.m_AllFeatures.AppendToArray(HellknightSigniferSwornOfTheEldestProgression.ToReference<BlueprintFeatureReference>());

            #endregion

            #endregion

            #region Warpriest

            var WarpriestClass = Resources.GetBlueprint<BlueprintCharacterClass>("30b5e47d47a0e37438cc5a80c96cfb99");
            var MysticTheurgeWarpriestProgression = Resources.GetBlueprint<BlueprintProgression>("9a4ad60a34f042b0b4178624aa90f803");
            var LoremasterWarpriestProgression = Resources.GetBlueprint<BlueprintProgression>("286f28e8ca6f49ab9bfac4e92580eba0");
            var HellknightSigniferWarpriestProgression = Resources.GetBlueprint<BlueprintProgression>("edadfb72864f4f359cee399246cc2c1f");

            #region Soldier of Gaia
            var SoldierOfGaiaSpellbook = Resources.GetModBlueprint<BlueprintSpellbook>("SoldierOfGaiaSpellbook");
            var SoldierOfGaiaSpelllist = Resources.GetModBlueprint<BlueprintSpellList>("SoldierOfGaiaSpelllist");
            var SoldierOfGaiaArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SoldierOfGaiaArchetype");

            MysticTheurgeWarpriestProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SoldierOfGaiaArchetype.ToReference<BlueprintArchetypeReference>();
            });
            var MysticTheurgeSoldierOfGaiaLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("MysticTheurgeSoldierOfGaiaLevelUp", bp => {
                bp.SetName("Ravener Hunter");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = SoldierOfGaiaSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var MysticTheurgeSoldierOfGaiaProgression = Helpers.CreateBlueprint<BlueprintProgression>("MysticTheurgeSoldierOfGaiaProgression", bp => {
                bp.SetName("Ravener Hunter");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 2;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SoldierOfGaiaArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });                
                bp.AddComponent<MysticTheurgeSpellbook>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_MysticTheurge = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.MysticTheurgeDivineSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>() },
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = WarpriestClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, MysticTheurgeSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(2, MysticTheurgeSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(3, MysticTheurgeSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(4, MysticTheurgeSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(5, MysticTheurgeSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(6, MysticTheurgeSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(7, MysticTheurgeSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(8, MysticTheurgeSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(9, MysticTheurgeSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(10, MysticTheurgeSoldierOfGaiaLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            MysticTheurgeDivineSpellbookSelection.m_AllFeatures = MysticTheurgeDivineSpellbookSelection.m_AllFeatures.AppendToArray(MysticTheurgeSoldierOfGaiaProgression.ToReference<BlueprintFeatureReference>());

            LoremasterWarpriestProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SoldierOfGaiaArchetype.ToReference<BlueprintArchetypeReference>();
            });
            var LoremasterSoldierOfGaiaLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("LoremasterSoldierOfGaiaLevelUp", bp => {
                bp.SetName("Soldier of Golarion");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = SoldierOfGaiaSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var LoremasterSoldierOfGaiaProgression = Helpers.CreateBlueprint<BlueprintProgression>("LoremasterSoldierOfGaiaProgression", bp => {
                bp.SetName("Soldier of Golarion");
                bp.SetDescription("When a new loremaster level is gained, the character gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a " +
                    "spellcasting class he belonged to before adding the prestige class. He does not, however, gain other benefits a character of that class would have gained, " +
                    "except for additional spells per day, spells known (if he is a spontaneous spellcaster), and an increased effective level of spellcasting. If a character " +
                    "had more than one spellcasting class before becoming a loremaster, he must decide to which class he adds the new level for purposes of determining the " +
                    "number of spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SoldierOfGaiaArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.MythicAdditionalProgressions };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = LoremasterClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, LoremasterSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(2, LoremasterSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(3, LoremasterSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(4, LoremasterSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(5, LoremasterSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(6, LoremasterSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(7, LoremasterSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(8, LoremasterSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(9, LoremasterSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(10, LoremasterSoldierOfGaiaLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            LoremasterSpellbookSelection.m_AllFeatures = LoremasterSpellbookSelection.m_AllFeatures.AppendToArray(LoremasterSoldierOfGaiaProgression.ToReference<BlueprintFeatureReference>());

            HellknightSigniferWarpriestProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SoldierOfGaiaArchetype.ToReference<BlueprintArchetypeReference>();
            });
            var HellknightSigniferSoldierOfGaiaLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("HellknightSigniferSoldierOfGaiaLevelUp", bp => {
                bp.SetName("Soldier of Golarion");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = SoldierOfGaiaSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var HellknightSigniferSoldierOfGaiaProgression = Helpers.CreateBlueprint<BlueprintProgression>("HellknightSigniferSoldierOfGaiaProgression", bp => {
                bp.SetName("Soldier of Golarion");
                bp.SetDescription("At 1st level, and at every level thereafter, a Hellknight signifer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level " +
                    "in a spellcasting class he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, " +
                    "except for additional spells per day, spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before " +
                    "becoming a Hellknight signifer, he must decide to which class he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = WarpriestClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SoldierOfGaiaArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.HellknightSigniferSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = HellknightSigniferClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, HellknightSigniferSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(2, HellknightSigniferSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(3, HellknightSigniferSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(4, HellknightSigniferSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(5, HellknightSigniferSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(6, HellknightSigniferSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(7, HellknightSigniferSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(8, HellknightSigniferSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(9, HellknightSigniferSoldierOfGaiaLevelUp),
                    Helpers.LevelEntry(10, HellknightSigniferSoldierOfGaiaLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            HellknightSigniferSpellbookSelection.m_AllFeatures = HellknightSigniferSpellbookSelection.m_AllFeatures.AppendToArray(HellknightSigniferSoldierOfGaiaProgression.ToReference<BlueprintFeatureReference>());


            #endregion

            #endregion

            #region Hunter
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var SkulkingHunterArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SkulkingHunterArchetype");
            var MysticTheurgeHunterProgression = Resources.GetBlueprint<BlueprintProgression>("e104a28a4bcf4e3aa4271c6e53e2f2f4");
            var LoremasterHunterProgression = Resources.GetBlueprint<BlueprintProgression>("e9be08cf2e3b4586b11f42d6b45b50aa");
            var HellknightSigniferHunterProgression = Resources.GetBlueprint<BlueprintProgression>("5df19d27ed0243bf8f4dd5b39f922fcc");

            #region Skulking Hunter            
            var SkulkingHunterSpellbook = Resources.GetModBlueprint<BlueprintSpellbook>("SkulkingHunterSpellbook");
            var SkulkingHunterSpelllist = Resources.GetModBlueprint<BlueprintSpellList>("SkulkingHunterSpelllist");

            var MysticTheurgeSkulkingHunterLevelParametrized1 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("MysticTheurgeSkulkingHunterLevelParametrized1", bp => {
                bp.SetName("Skulking Hunter Spell (1st Level)");
                bp.SetDescription("You can select new known skulking hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = SkulkingHunterSpelllist.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 1;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = SkulkingHunterSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 1;
                bp.DisallowSpellsInSpellList = false;
                //bp.BlueprintParameterVariants = MysticTheurgeHunterLevelParametrized1.BlueprintParameterVariants;
            });
            var MysticTheurgeSkulkingHunterLevelParametrized2 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("MysticTheurgeSkulkingHunterLevelParametrized2", bp => {
                bp.SetName("Skulking Hunter Spell (2nd Level)");
                bp.SetDescription("You can select new known skulking hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = SkulkingHunterSpelllist.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 2;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = SkulkingHunterSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 2;
                bp.DisallowSpellsInSpellList = false;
                //bp.BlueprintParameterVariants = MysticTheurgeHunterLevelParametrized1.BlueprintParameterVariants;
            });
            var MysticTheurgeSkulkingHunterLevelParametrized3 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("MysticTheurgeSkulkingHunterLevelParametrized3", bp => {
                bp.SetName("Skulking Hunter Spell (3rd Level)");
                bp.SetDescription("You can select new known skulking hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = SkulkingHunterSpelllist.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 3;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = SkulkingHunterSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 3;
                bp.DisallowSpellsInSpellList = false;
                //bp.BlueprintParameterVariants = MysticTheurgeHunterLevelParametrized1.BlueprintParameterVariants;
            });
            var MysticTheurgeSkulkingHunterLevelParametrized4 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("MysticTheurgeSkulkingHunterLevelParametrized4", bp => {
                bp.SetName("Skulking Hunter Spell (4th Level)");
                bp.SetDescription("You can select new known skulking hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = SkulkingHunterSpelllist.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 4;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = SkulkingHunterSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 4;
                bp.DisallowSpellsInSpellList = false;
                //bp.BlueprintParameterVariants = MysticTheurgeHunterLevelParametrized1.BlueprintParameterVariants;
            });
            var MysticTheurgeSkulkingHunterLevelParametrized5 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("MysticTheurgeSkulkingHunterLevelParametrized5", bp => {
                bp.SetName("Skulking Hunter Spell (5th Level)");
                bp.SetDescription("You can select new known skulking hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = SkulkingHunterSpelllist.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 5;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = SkulkingHunterSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 5;
                bp.DisallowSpellsInSpellList = false;
                //bp.BlueprintParameterVariants = MysticTheurgeHunterLevelParametrized1.BlueprintParameterVariants;
            });
            var MysticTheurgeSkulkingHunterLevelParametrized6 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("MysticTheurgeSkulkingHunterLevelParametrized6", bp => {
                bp.SetName("Skulking Hunter Spell (6th Level)");
                bp.SetDescription("You can select new known skulking hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = SkulkingHunterSpelllist.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 6;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = SkulkingHunterSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 6;
                bp.DisallowSpellsInSpellList = false;
                //bp.BlueprintParameterVariants = MysticTheurgeHunterLevelParametrized1.BlueprintParameterVariants;
            });

            var MysticTheurgeSkulkingHunterLevelSelection1 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("MysticTheurgeSkulkingHunterLevelSelection1", bp => {
                bp.SetName("Skulking Hunter Spell (1st Level)");
                bp.SetDescription("You can select new known skulking hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Mode = SelectionMode.Default;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    MysticTheurgeSkulkingHunterLevelParametrized1.ToReference<BlueprintFeatureReference>()
                };
                bp.m_Features = new BlueprintFeatureReference[] {
                    MysticTheurgeSkulkingHunterLevelParametrized1.ToReference<BlueprintFeatureReference>()
                };
            });
            var MysticTheurgeSkulkingHunterLevelSelection2 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("MysticTheurgeSkulkingHunterLevelSelection2", bp => {
                bp.SetName("Skulking Hunter Spell (2nd Level)");
                bp.SetDescription("You can select new known skulking hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Mode = SelectionMode.Default;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    MysticTheurgeSkulkingHunterLevelParametrized2.ToReference<BlueprintFeatureReference>()
                };
                bp.m_Features = new BlueprintFeatureReference[] {
                    MysticTheurgeSkulkingHunterLevelParametrized2.ToReference<BlueprintFeatureReference>()
                };
            });
            var MysticTheurgeSkulkingHunterLevelSelection3 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("MysticTheurgeSkulkingHunterLevelSelection3", bp => {
                bp.SetName("Skulking Hunter Spell (3rd Level)");
                bp.SetDescription("You can select new known skulking hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Mode = SelectionMode.Default;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    MysticTheurgeSkulkingHunterLevelParametrized3.ToReference<BlueprintFeatureReference>()
                };
                bp.m_Features = new BlueprintFeatureReference[] {
                    MysticTheurgeSkulkingHunterLevelParametrized3.ToReference<BlueprintFeatureReference>()
                };
            });
            var MysticTheurgeSkulkingHunterLevelSelection4 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("MysticTheurgeSkulkingHunterLevelSelection4", bp => {
                bp.SetName("Skulking Hunter Spell (4th Level)");
                bp.SetDescription("You can select new known skulking hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Mode = SelectionMode.Default;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    MysticTheurgeSkulkingHunterLevelParametrized4.ToReference<BlueprintFeatureReference>()
                };
                bp.m_Features = new BlueprintFeatureReference[] {
                    MysticTheurgeSkulkingHunterLevelParametrized4.ToReference<BlueprintFeatureReference>()
                };
            });
            var MysticTheurgeSkulkingHunterLevelSelection5 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("MysticTheurgeSkulkingHunterLevelSelection5", bp => {
                bp.SetName("Skulking Hunter Spell (5th Level)");
                bp.SetDescription("You can select new known skulking hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Mode = SelectionMode.Default;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    MysticTheurgeSkulkingHunterLevelParametrized5.ToReference<BlueprintFeatureReference>()
                };
                bp.m_Features = new BlueprintFeatureReference[] {
                    MysticTheurgeSkulkingHunterLevelParametrized5.ToReference<BlueprintFeatureReference>()
                };
            });
            var MysticTheurgeSkulkingHunterLevelSelection6 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("MysticTheurgeSkulkingHunterLevelSelection6", bp => {
                bp.SetName("Skulking Hunter Spell (6th Level)");
                bp.SetDescription("You can select new known skulking hunter {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Mode = SelectionMode.Default;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    MysticTheurgeSkulkingHunterLevelParametrized6.ToReference<BlueprintFeatureReference>()
                };
                bp.m_Features = new BlueprintFeatureReference[] {
                    MysticTheurgeSkulkingHunterLevelParametrized6.ToReference<BlueprintFeatureReference>()
                };
            });

            MysticTheurgeHunterProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SkulkingHunterArchetype.ToReference<BlueprintArchetypeReference>();
            });
            var MysticTheurgeSkulkingHunterLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("MysticTheurgeSkulkingHunterLevelUp", bp => {
                bp.SetName("Skulking Hunter");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = SkulkingHunterSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var MysticTheurgeSkulkingHunterProgression = Helpers.CreateBlueprint<BlueprintProgression>("MysticTheurgeSkulkingHunterProgression", bp => {
                bp.SetName("Skulking Hunter");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 2;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SkulkingHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.AddComponent<MysticTheurgeSpellbook>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_MysticTheurge = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.MysticTheurgeDivineSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>() },
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(5, MysticTheurgeSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection2),
                    Helpers.LevelEntry(6, MysticTheurgeSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection2),
                    Helpers.LevelEntry(7, MysticTheurgeSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection1, MysticTheurgeSkulkingHunterLevelSelection3, MysticTheurgeSkulkingHunterLevelSelection3),
                    Helpers.LevelEntry(8, MysticTheurgeSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection3),
                    Helpers.LevelEntry(9, MysticTheurgeSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection3),
                    Helpers.LevelEntry(10, MysticTheurgeSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection2, MysticTheurgeSkulkingHunterLevelSelection4, MysticTheurgeSkulkingHunterLevelSelection4),
                    Helpers.LevelEntry(11, MysticTheurgeSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection1, MysticTheurgeSkulkingHunterLevelSelection4),
                    Helpers.LevelEntry(12, MysticTheurgeSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection4),
                    Helpers.LevelEntry(13, MysticTheurgeSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection3, MysticTheurgeSkulkingHunterLevelSelection5, MysticTheurgeSkulkingHunterLevelSelection5),
                    Helpers.LevelEntry(14, MysticTheurgeSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection2, MysticTheurgeSkulkingHunterLevelSelection5),
                    Helpers.LevelEntry(15, MysticTheurgeSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection5),
                    Helpers.LevelEntry(16, MysticTheurgeSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection4, MysticTheurgeSkulkingHunterLevelSelection6, MysticTheurgeSkulkingHunterLevelSelection6),
                    Helpers.LevelEntry(17, MysticTheurgeSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection3, MysticTheurgeSkulkingHunterLevelSelection6),
                    Helpers.LevelEntry(18, MysticTheurgeSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection6),
                    Helpers.LevelEntry(19, MysticTheurgeSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection5),
                    Helpers.LevelEntry(20, MysticTheurgeSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection4, MysticTheurgeSkulkingHunterLevelSelection6)
                };
                bp.GiveFeaturesForPreviousLevels = false;
                bp.m_ExclusiveProgression = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>();
            });
            MysticTheurgeDivineSpellbookSelection.m_AllFeatures = MysticTheurgeDivineSpellbookSelection.m_AllFeatures.AppendToArray(MysticTheurgeSkulkingHunterProgression.ToReference<BlueprintFeatureReference>());

            LoremasterHunterProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SkulkingHunterArchetype.ToReference<BlueprintArchetypeReference>();
            });
            var LoremasterSkulkingHunterLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("LoremasterSkulkingHunterLevelUp", bp => {
                bp.SetName("Skulking Hunter");
                bp.SetDescription("At 1st level, the loremaster selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new loremaster level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = SkulkingHunterSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var LoremasterSkulkingHunterProgression = Helpers.CreateBlueprint<BlueprintProgression>("LoremasterSkulkingHunterProgression", bp => {
                bp.SetName("Skulking Hunter");
                bp.SetDescription("When a new loremaster level is gained, the character gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a " +
                    "spellcasting class he belonged to before adding the prestige class. He does not, however, gain other benefits a character of that class would have gained, " +
                    "except for additional spells per day, spells known (if he is a spontaneous spellcaster), and an increased effective level of spellcasting. If a character " +
                    "had more than one spellcasting class before becoming a loremaster, he must decide to which class he adds the new level for purposes of determining the " +
                    "number of spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SkulkingHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ReplaceSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = LoremasterClass.ToReference<BlueprintCharacterClassReference>() },
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(5, LoremasterSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection2),
                    Helpers.LevelEntry(6, LoremasterSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection2),
                    Helpers.LevelEntry(7, LoremasterSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection1, MysticTheurgeSkulkingHunterLevelSelection3, MysticTheurgeSkulkingHunterLevelSelection3),
                    Helpers.LevelEntry(8, LoremasterSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection3),
                    Helpers.LevelEntry(9, LoremasterSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection3),
                    Helpers.LevelEntry(10, LoremasterSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection2, MysticTheurgeSkulkingHunterLevelSelection4, MysticTheurgeSkulkingHunterLevelSelection4),
                    Helpers.LevelEntry(11, LoremasterSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection1, MysticTheurgeSkulkingHunterLevelSelection4),
                    Helpers.LevelEntry(12, LoremasterSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection4),
                    Helpers.LevelEntry(13, LoremasterSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection3, MysticTheurgeSkulkingHunterLevelSelection5, MysticTheurgeSkulkingHunterLevelSelection5),
                    Helpers.LevelEntry(14, LoremasterSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection2, MysticTheurgeSkulkingHunterLevelSelection5),
                    Helpers.LevelEntry(15, LoremasterSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection5),
                    Helpers.LevelEntry(16, LoremasterSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection4, MysticTheurgeSkulkingHunterLevelSelection6, MysticTheurgeSkulkingHunterLevelSelection6),
                    Helpers.LevelEntry(17, LoremasterSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection3, MysticTheurgeSkulkingHunterLevelSelection6),
                    Helpers.LevelEntry(18, LoremasterSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection6),
                    Helpers.LevelEntry(19, LoremasterSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection5),
                    Helpers.LevelEntry(20, LoremasterSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection4, MysticTheurgeSkulkingHunterLevelSelection6)
                };
                bp.GiveFeaturesForPreviousLevels = false;
                bp.m_ExclusiveProgression = LoremasterClass.ToReference<BlueprintCharacterClassReference>();
            });
            LoremasterSpellbookSelection.m_AllFeatures = LoremasterSpellbookSelection.m_AllFeatures.AppendToArray(LoremasterSkulkingHunterProgression.ToReference<BlueprintFeatureReference>());

            HellknightSigniferHunterProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SkulkingHunterArchetype.ToReference<BlueprintArchetypeReference>();
            });
            var HellknightSigniferSkulkingHunterLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("HellknightSigniferSkulkingHunterLevelUp", bp => {
                bp.SetName("Skulking Hunter");
                bp.SetDescription("At 1st level, the hellknight signifer selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new hellknight signifer level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = SkulkingHunterSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var HellknightSigniferSkulkingHunterProgression = Helpers.CreateBlueprint<BlueprintProgression>("HellknightSigniferSkulkingHunterProgression", bp => {
                bp.SetName("Skulking Hunter");
                bp.SetDescription("At 1st level, and at every level thereafter, a Hellknight signifer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level " +
                    "in a spellcasting class he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, " +
                    "except for additional spells per day, spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before " +
                    "becoming a Hellknight signifer, he must decide to which class he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SkulkingHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.HellknightSigniferSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = HellknightSigniferClass.ToReference<BlueprintCharacterClassReference>() },
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(5, HellknightSigniferSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection2),
                    Helpers.LevelEntry(6, HellknightSigniferSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection2),
                    Helpers.LevelEntry(7, HellknightSigniferSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection1, MysticTheurgeSkulkingHunterLevelSelection3, MysticTheurgeSkulkingHunterLevelSelection3),
                    Helpers.LevelEntry(8, HellknightSigniferSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection3),
                    Helpers.LevelEntry(9, HellknightSigniferSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection3),
                    Helpers.LevelEntry(10, HellknightSigniferSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection2, MysticTheurgeSkulkingHunterLevelSelection4, MysticTheurgeSkulkingHunterLevelSelection4),
                    Helpers.LevelEntry(11, HellknightSigniferSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection1, MysticTheurgeSkulkingHunterLevelSelection4),
                    Helpers.LevelEntry(12, HellknightSigniferSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection4),
                    Helpers.LevelEntry(13, HellknightSigniferSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection3, MysticTheurgeSkulkingHunterLevelSelection5, MysticTheurgeSkulkingHunterLevelSelection5),
                    Helpers.LevelEntry(14, HellknightSigniferSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection2, MysticTheurgeSkulkingHunterLevelSelection5),
                    Helpers.LevelEntry(15, HellknightSigniferSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection5),
                    Helpers.LevelEntry(16, HellknightSigniferSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection4, MysticTheurgeSkulkingHunterLevelSelection6, MysticTheurgeSkulkingHunterLevelSelection6),
                    Helpers.LevelEntry(17, HellknightSigniferSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection3, MysticTheurgeSkulkingHunterLevelSelection6),
                    Helpers.LevelEntry(18, HellknightSigniferSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection6),
                    Helpers.LevelEntry(19, HellknightSigniferSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection5),
                    Helpers.LevelEntry(20, HellknightSigniferSkulkingHunterLevelUp, MysticTheurgeSkulkingHunterLevelSelection4, MysticTheurgeSkulkingHunterLevelSelection6)
                };
                bp.GiveFeaturesForPreviousLevels = false;
                bp.m_ExclusiveProgression = HellknightSigniferClass.ToReference<BlueprintCharacterClassReference>();
            });
            HellknightSigniferSpellbookSelection.m_AllFeatures = HellknightSigniferSpellbookSelection.m_AllFeatures.AppendToArray(HellknightSigniferSkulkingHunterProgression.ToReference<BlueprintFeatureReference>());

            #endregion

            #endregion

            #region Paladin
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var SilverChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("SilverChampionArchetype");
            var MysticTheurgePaladinProgression = Resources.GetBlueprint<BlueprintProgression>("39c9af2be5d29ba49a68d6e82ea4e847");
            var LoremasterPaladinProgression = Resources.GetBlueprint<BlueprintProgression>("0eeb308fbec42c84591016146ac7b28f");
            var HellknightSigniferPaladinProgression = Resources.GetBlueprint<BlueprintProgression>("1126e360a78d7404eab6fa5e3fa3a3b4");

            #region Silver Champion
            var SilverChampionSpellbook = Resources.GetModBlueprint<BlueprintSpellbook>("SilverChampionSpellbook");

            MysticTheurgePaladinProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SilverChampionArchetype.ToReference<BlueprintArchetypeReference>();
            });
            var MysticTheurgeSilverChampionLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("MysticTheurgeSilverChampionLevelUp", bp => {
                bp.SetName("Silver Champion");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = SilverChampionSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var MysticTheurgeSilverChampionProgression = Helpers.CreateBlueprint<BlueprintProgression>("MysticTheurgeSilverChampionProgression", bp => {
                bp.SetName("Silver Champion");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 2;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SilverChampionArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.AddComponent<MysticTheurgeSpellbook>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_MysticTheurge = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.MysticTheurgeDivineSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, MysticTheurgeSilverChampionLevelUp),
                    Helpers.LevelEntry(2, MysticTheurgeSilverChampionLevelUp),
                    Helpers.LevelEntry(3, MysticTheurgeSilverChampionLevelUp),
                    Helpers.LevelEntry(4, MysticTheurgeSilverChampionLevelUp),
                    Helpers.LevelEntry(5, MysticTheurgeSilverChampionLevelUp),
                    Helpers.LevelEntry(6, MysticTheurgeSilverChampionLevelUp),
                    Helpers.LevelEntry(7, MysticTheurgeSilverChampionLevelUp),
                    Helpers.LevelEntry(8, MysticTheurgeSilverChampionLevelUp),
                    Helpers.LevelEntry(9, MysticTheurgeSilverChampionLevelUp),
                    Helpers.LevelEntry(10, MysticTheurgeSilverChampionLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            MysticTheurgeDivineSpellbookSelection.m_AllFeatures = MysticTheurgeDivineSpellbookSelection.m_AllFeatures.AppendToArray(MysticTheurgeSilverChampionProgression.ToReference<BlueprintFeatureReference>());

            LoremasterPaladinProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SilverChampionArchetype.ToReference<BlueprintArchetypeReference>();
            });
            var LoremasterSilverChampionLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("LoremasterSilverChampionLevelUp", bp => {
                bp.SetName("Silver Champion");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = SilverChampionSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var LoremasterSilverChampionProgression = Helpers.CreateBlueprint<BlueprintProgression>("LoremasterSilverChampionProgression", bp => {
                bp.SetName("Silver Champion");
                bp.SetDescription("When a new loremaster level is gained, the character gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a " +
                    "spellcasting class he belonged to before adding the prestige class. He does not, however, gain other benefits a character of that class would have gained, " +
                    "except for additional spells per day, spells known (if he is a spontaneous spellcaster), and an increased effective level of spellcasting. If a character " +
                    "had more than one spellcasting class before becoming a loremaster, he must decide to which class he adds the new level for purposes of determining the " +
                    "number of spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SilverChampionArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.MythicAdditionalProgressions };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = LoremasterClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, LoremasterSilverChampionLevelUp),
                    Helpers.LevelEntry(2, LoremasterSilverChampionLevelUp),
                    Helpers.LevelEntry(3, LoremasterSilverChampionLevelUp),
                    Helpers.LevelEntry(4, LoremasterSilverChampionLevelUp),
                    Helpers.LevelEntry(5, LoremasterSilverChampionLevelUp),
                    Helpers.LevelEntry(6, LoremasterSilverChampionLevelUp),
                    Helpers.LevelEntry(7, LoremasterSilverChampionLevelUp),
                    Helpers.LevelEntry(8, LoremasterSilverChampionLevelUp),
                    Helpers.LevelEntry(9, LoremasterSilverChampionLevelUp),
                    Helpers.LevelEntry(10, LoremasterSilverChampionLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            LoremasterSpellbookSelection.m_AllFeatures = LoremasterSpellbookSelection.m_AllFeatures.AppendToArray(LoremasterSilverChampionProgression.ToReference<BlueprintFeatureReference>());

            HellknightSigniferPaladinProgression.AddComponent<PrerequisiteNoArchetype>(c => {
                c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                c.m_Archetype = SilverChampionArchetype.ToReference<BlueprintArchetypeReference>();
            });
            var HellknightSigniferSilverChampionLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("HellknightSigniferSilverChampionLevelUp", bp => {
                bp.SetName("Silver Champion");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = SilverChampionSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var HellknightSigniferSilverChampionProgression = Helpers.CreateBlueprint<BlueprintProgression>("HellknightSigniferSilverChampionProgression", bp => {
                bp.SetName("Silver Champion");
                bp.SetDescription("At 1st level, and at every level thereafter, a Hellknight signifer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level " +
                    "in a spellcasting class he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, " +
                    "except for additional spells per day, spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before " +
                    "becoming a Hellknight signifer, he must decide to which class he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = PaladinClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = SilverChampionArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.HellknightSigniferSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = HellknightSigniferClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, HellknightSigniferSilverChampionLevelUp),
                    Helpers.LevelEntry(2, HellknightSigniferSilverChampionLevelUp),
                    Helpers.LevelEntry(3, HellknightSigniferSilverChampionLevelUp),
                    Helpers.LevelEntry(4, HellknightSigniferSilverChampionLevelUp),
                    Helpers.LevelEntry(5, HellknightSigniferSilverChampionLevelUp),
                    Helpers.LevelEntry(6, HellknightSigniferSilverChampionLevelUp),
                    Helpers.LevelEntry(7, HellknightSigniferSilverChampionLevelUp),
                    Helpers.LevelEntry(8, HellknightSigniferSilverChampionLevelUp),
                    Helpers.LevelEntry(9, HellknightSigniferSilverChampionLevelUp),
                    Helpers.LevelEntry(10, HellknightSigniferSilverChampionLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            HellknightSigniferSpellbookSelection.m_AllFeatures = HellknightSigniferSpellbookSelection.m_AllFeatures.AppendToArray(HellknightSigniferSilverChampionProgression.ToReference<BlueprintFeatureReference>());




            #endregion

            #endregion

            #region Dread Knight
            var DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
            var ClawOfTheFalseWyrmArchetype = Resources.GetModBlueprint<BlueprintArchetype>("ClawOfTheFalseWyrmArchetype");

            #region Main Class
            var DreadKnightSpellbook = Resources.GetModBlueprint<BlueprintSpellbook>("DreadKnightSpellbook");
            var WarpriestSpelllist = Resources.GetBlueprint<BlueprintSpellList>("c5a1b8df32914d74c9b44052ba3e686a");

            var MysticTheurgeDreadKnightLevelParametrized1 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("MysticTheurgeDreadKnightLevelParametrized1", bp => {
                bp.SetName("Dread Knight Spell (1st Level)");
                bp.SetDescription("You can select new known dread knight {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = WarpriestSpelllist.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 1;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = WarpriestSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 1;
                bp.DisallowSpellsInSpellList = false;
                //bp.BlueprintParameterVariants = MysticTheurgeInquisitorLevelParametrized1.BlueprintParameterVariants;
            });
            var MysticTheurgeDreadKnightLevelParametrized2 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("MysticTheurgeDreadKnightLevelParametrized2", bp => {
                bp.SetName("Dread Knight Spell (2nd Level)");
                bp.SetDescription("You can select new known dread knight {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = WarpriestSpelllist.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 2;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = WarpriestSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 2;
                bp.DisallowSpellsInSpellList = false;
                //bp.BlueprintParameterVariants = MysticTheurgeInquisitorLevelParametrized1.BlueprintParameterVariants;
            });
            var MysticTheurgeDreadKnightLevelParametrized3 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("MysticTheurgeDreadKnightLevelParametrized3", bp => {
                bp.SetName("Dread Knight Spell (3rd Level)");
                bp.SetDescription("You can select new known dread knight {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = WarpriestSpelllist.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 3;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = WarpriestSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 3;
                bp.DisallowSpellsInSpellList = false;
                //bp.BlueprintParameterVariants = MysticTheurgeInquisitorLevelParametrized1.BlueprintParameterVariants;
            });
            var MysticTheurgeDreadKnightLevelParametrized4 = Helpers.CreateBlueprint<BlueprintParametrizedFeature>("MysticTheurgeDreadKnightLevelParametrized4", bp => {
                bp.SetName("Dread Knight Spell (4th Level)");
                bp.SetDescription("You can select new known dread knight {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.AddComponent<LearnSpellParametrized>(c => {
                    c.m_SpellcasterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = WarpriestSpelllist.ToReference<BlueprintSpellListReference>();
                    c.SpecificSpellLevel = true;
                    c.SpellLevelPenalty = 0;
                    c.SpellLevel = 4;
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.Ranks = 20;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.ParameterType = FeatureParameterType.LearnSpell;
                bp.WeaponSubCategory = WeaponSubCategory.None;
                bp.SelectionFeatureGroup = FeatureGroup.None;
                bp.RequireProficiency = false;
                bp.m_SpellList = WarpriestSpelllist.ToReference<BlueprintSpellListReference>();
                bp.m_SpellcasterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                bp.SpecificSpellLevel = true;
                bp.SpellLevelPenalty = 1; //????
                bp.SpellLevel = 4;
                bp.DisallowSpellsInSpellList = false;
                //bp.BlueprintParameterVariants = MysticTheurgeInquisitorLevelParametrized1.BlueprintParameterVariants;
            });

            var MysticTheurgeDreadKnightLevelSelection1 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("MysticTheurgeDreadKnightLevelSelection1", bp => {
                bp.SetName("Dread Knight Spell (1st Level)");
                bp.SetDescription("You can select new known dread knight {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Mode = SelectionMode.Default;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    MysticTheurgeDreadKnightLevelParametrized1.ToReference<BlueprintFeatureReference>()
                };
                bp.m_Features = new BlueprintFeatureReference[] {
                    MysticTheurgeDreadKnightLevelParametrized1.ToReference<BlueprintFeatureReference>()
                };
            });
            var MysticTheurgeDreadKnightLevelSelection2 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("MysticTheurgeDreadKnightLevelSelection2", bp => {
                bp.SetName("Dread Knight Spell (2nd Level)");
                bp.SetDescription("You can select new known dread knight {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Mode = SelectionMode.Default;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    MysticTheurgeDreadKnightLevelParametrized2.ToReference<BlueprintFeatureReference>()
                };
                bp.m_Features = new BlueprintFeatureReference[] {
                    MysticTheurgeDreadKnightLevelParametrized2.ToReference<BlueprintFeatureReference>()
                };
            });
            var MysticTheurgeDreadKnightLevelSelection3 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("MysticTheurgeDreadKnightLevelSelection3", bp => {
                bp.SetName("Dread Knight Spell (3rd Level)");
                bp.SetDescription("You can select new known dread knight {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Mode = SelectionMode.Default;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    MysticTheurgeDreadKnightLevelParametrized3.ToReference<BlueprintFeatureReference>()
                };
                bp.m_Features = new BlueprintFeatureReference[] {
                    MysticTheurgeDreadKnightLevelParametrized3.ToReference<BlueprintFeatureReference>()
                };
            });
            var MysticTheurgeDreadKnightLevelSelection4 = Helpers.CreateBlueprint<BlueprintFeatureSelection>("MysticTheurgeDreadKnightLevelSelection4", bp => {
                bp.SetName("Dread Knight Spell (4th Level)");
                bp.SetDescription("You can select new known dread knight {g|Encyclopedia:Spell}spells{/g} when you gain a new level in this class.");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.ReapplyOnLevelUp = false;
                bp.IsClassFeature = true;
                bp.Mode = SelectionMode.Default;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    MysticTheurgeDreadKnightLevelParametrized4.ToReference<BlueprintFeatureReference>()
                };
                bp.m_Features = new BlueprintFeatureReference[] {
                    MysticTheurgeDreadKnightLevelParametrized4.ToReference<BlueprintFeatureReference>()
                };
            });

            var MysticTheurgeDreadKnightLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("MysticTheurgeDreadKnightLevelUp", bp => {
                bp.SetName("Dread Knight");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = DreadKnightSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var MysticTheurgeDreadKnightProgression = Helpers.CreateBlueprint<BlueprintProgression>("MysticTheurgeDreadKnightProgression", bp => {
                bp.SetName("Dread Knight");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 2;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ClawOfTheFalseWyrmArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.AddComponent<MysticTheurgeSpellbook>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_MysticTheurge = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.MysticTheurgeDivineSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>() },
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = DreadKnightClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {                    
                    Helpers.LevelEntry(8, MysticTheurgeDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection2),
                    Helpers.LevelEntry(9, MysticTheurgeDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection1, MysticTheurgeDreadKnightLevelSelection2),
                    Helpers.LevelEntry(10, MysticTheurgeDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection3, MysticTheurgeDreadKnightLevelSelection3),
                    Helpers.LevelEntry(11, MysticTheurgeDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection3),
                    Helpers.LevelEntry(12, MysticTheurgeDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection1, MysticTheurgeDreadKnightLevelSelection2, MysticTheurgeDreadKnightLevelSelection3),
                    Helpers.LevelEntry(13, MysticTheurgeDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection4, MysticTheurgeDreadKnightLevelSelection4),
                    Helpers.LevelEntry(14, MysticTheurgeDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection4),
                    Helpers.LevelEntry(15, MysticTheurgeDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection2, MysticTheurgeDreadKnightLevelSelection3, MysticTheurgeDreadKnightLevelSelection4),
                    Helpers.LevelEntry(16, MysticTheurgeDreadKnightLevelUp),
                    Helpers.LevelEntry(17, MysticTheurgeDreadKnightLevelUp),
                    Helpers.LevelEntry(18, MysticTheurgeDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection3, MysticTheurgeDreadKnightLevelSelection4),
                    Helpers.LevelEntry(19, MysticTheurgeDreadKnightLevelUp),
                    Helpers.LevelEntry(20, MysticTheurgeDreadKnightLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
                bp.m_ExclusiveProgression = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>();
            });
            MysticTheurgeDivineSpellbookSelection.m_AllFeatures = MysticTheurgeDivineSpellbookSelection.m_AllFeatures.AppendToArray(MysticTheurgeDreadKnightProgression.ToReference<BlueprintFeatureReference>());

            var LoremasterDreadKnightLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("LoremasterDreadKnightLevelUp", bp => {
                bp.SetName("Dread Knight");
                bp.SetDescription("At 1st level, the loremaster selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new loremaster level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = DreadKnightSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var LoremasterDreadKnightProgression = Helpers.CreateBlueprint<BlueprintProgression>("LoremasterDreadKnightProgression", bp => {
                bp.SetName("Dread Knight");
                bp.SetDescription("When a new loremaster level is gained, the character gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a " +
                    "spellcasting class he belonged to before adding the prestige class. He does not, however, gain other benefits a character of that class would have gained, " +
                    "except for additional spells per day, spells known (if he is a spontaneous spellcaster), and an increased effective level of spellcasting. If a character " +
                    "had more than one spellcasting class before becoming a loremaster, he must decide to which class he adds the new level for purposes of determining the " +
                    "number of spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ClawOfTheFalseWyrmArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ReplaceSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = LoremasterClass.ToReference<BlueprintCharacterClassReference>() },
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = DreadKnightClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(8, LoremasterDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection2),
                    Helpers.LevelEntry(9, LoremasterDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection1, MysticTheurgeDreadKnightLevelSelection2),
                    Helpers.LevelEntry(10, LoremasterDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection3, MysticTheurgeDreadKnightLevelSelection3),
                    Helpers.LevelEntry(11, LoremasterDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection3),
                    Helpers.LevelEntry(12, LoremasterDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection1, MysticTheurgeDreadKnightLevelSelection2, MysticTheurgeDreadKnightLevelSelection3),
                    Helpers.LevelEntry(13, LoremasterDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection4, MysticTheurgeDreadKnightLevelSelection4),
                    Helpers.LevelEntry(14, LoremasterDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection4),
                    Helpers.LevelEntry(15, LoremasterDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection2, MysticTheurgeDreadKnightLevelSelection3, MysticTheurgeDreadKnightLevelSelection4),
                    Helpers.LevelEntry(16, LoremasterDreadKnightLevelUp),
                    Helpers.LevelEntry(17, LoremasterDreadKnightLevelUp),
                    Helpers.LevelEntry(18, LoremasterDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection3, MysticTheurgeDreadKnightLevelSelection4),
                    Helpers.LevelEntry(19, LoremasterDreadKnightLevelUp),
                    Helpers.LevelEntry(20, LoremasterDreadKnightLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
                bp.m_ExclusiveProgression = LoremasterClass.ToReference<BlueprintCharacterClassReference>();
            });
            LoremasterSpellbookSelection.m_AllFeatures = LoremasterSpellbookSelection.m_AllFeatures.AppendToArray(LoremasterDreadKnightProgression.ToReference<BlueprintFeatureReference>());

            var HellknightSigniferDreadKnightLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("HellknightSigniferDreadKnightLevelUp", bp => {
                bp.SetName("Dread Knight");
                bp.SetDescription("At 1st level, the hellknight signifer selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new hellknight signifer level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = DreadKnightSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var HellknightSigniferDreadKnightProgression = Helpers.CreateBlueprint<BlueprintProgression>("HellknightSigniferDreadKnightProgression", bp => {
                bp.SetName("Dread Knight");
                bp.SetDescription("At 1st level, and at every level thereafter, a Hellknight signifer gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level " +
                    "in a spellcasting class he belonged to before adding the prestige class. He does not, however, gain any other benefit a character of that class would have gained, " +
                    "except for additional spells per day, spells known, and an increased effective level of spellcasting. If a character had more than one spellcasting class before " +
                    "becoming a Hellknight signifer, he must decide to which class he adds the new level for purposes of determining spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteNoArchetype>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ClawOfTheFalseWyrmArchetype.ToReference<BlueprintArchetypeReference>();
                    c.HideInUI = false;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.HellknightSigniferSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = HellknightSigniferClass.ToReference<BlueprintCharacterClassReference>() },
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = DreadKnightClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(8, HellknightSigniferDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection2),
                    Helpers.LevelEntry(9, HellknightSigniferDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection1, MysticTheurgeDreadKnightLevelSelection2),
                    Helpers.LevelEntry(10, HellknightSigniferDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection3, MysticTheurgeDreadKnightLevelSelection3),
                    Helpers.LevelEntry(11, HellknightSigniferDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection3),
                    Helpers.LevelEntry(12, HellknightSigniferDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection1, MysticTheurgeDreadKnightLevelSelection2, MysticTheurgeDreadKnightLevelSelection3),
                    Helpers.LevelEntry(13, HellknightSigniferDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection4, MysticTheurgeDreadKnightLevelSelection4),
                    Helpers.LevelEntry(14, HellknightSigniferDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection4),
                    Helpers.LevelEntry(15, HellknightSigniferDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection2, MysticTheurgeDreadKnightLevelSelection3, MysticTheurgeDreadKnightLevelSelection4),
                    Helpers.LevelEntry(16, HellknightSigniferDreadKnightLevelUp),
                    Helpers.LevelEntry(17, HellknightSigniferDreadKnightLevelUp),
                    Helpers.LevelEntry(18, HellknightSigniferDreadKnightLevelUp, MysticTheurgeDreadKnightLevelSelection3, MysticTheurgeDreadKnightLevelSelection4),
                    Helpers.LevelEntry(19, HellknightSigniferDreadKnightLevelUp),
                    Helpers.LevelEntry(20, HellknightSigniferDreadKnightLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
                bp.m_ExclusiveProgression = HellknightSigniferClass.ToReference<BlueprintCharacterClassReference>();
            });
            HellknightSigniferSpellbookSelection.m_AllFeatures = HellknightSigniferSpellbookSelection.m_AllFeatures.AppendToArray(HellknightSigniferDreadKnightProgression.ToReference<BlueprintFeatureReference>());

            #endregion

            #region Claw Of The False Wyrm
            var ClawOfTheFalseWyrmSpellbook = Resources.GetModBlueprint<BlueprintSpellbook>("ClawOfTheFalseWyrmSpellbook");

            var MysticTheurgeClawOfTheFalseWyrmLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("MysticTheurgeClawOfTheFalseWyrmLevelUp", bp => {
                bp.SetName("Claw of the False Wyrm");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = ClawOfTheFalseWyrmSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var MysticTheurgeClawOfTheFalseWyrmProgression = Helpers.CreateBlueprint<BlueprintProgression>("MysticTheurgeClawOfTheFalseWyrmProgression", bp => {
                bp.SetName("Claw of the False Wyrm");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 2;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ClawOfTheFalseWyrmArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.AddComponent<MysticTheurgeSpellbook>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_MysticTheurge = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.MysticTheurgeDivineSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = MysticTheurgeClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, MysticTheurgeClawOfTheFalseWyrmLevelUp),
                    Helpers.LevelEntry(2, MysticTheurgeClawOfTheFalseWyrmLevelUp),
                    Helpers.LevelEntry(3, MysticTheurgeClawOfTheFalseWyrmLevelUp),
                    Helpers.LevelEntry(4, MysticTheurgeClawOfTheFalseWyrmLevelUp),
                    Helpers.LevelEntry(5, MysticTheurgeClawOfTheFalseWyrmLevelUp),
                    Helpers.LevelEntry(6, MysticTheurgeClawOfTheFalseWyrmLevelUp),
                    Helpers.LevelEntry(7, MysticTheurgeClawOfTheFalseWyrmLevelUp),
                    Helpers.LevelEntry(8, MysticTheurgeClawOfTheFalseWyrmLevelUp),
                    Helpers.LevelEntry(9, MysticTheurgeClawOfTheFalseWyrmLevelUp),
                    Helpers.LevelEntry(10, MysticTheurgeClawOfTheFalseWyrmLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            MysticTheurgeDivineSpellbookSelection.m_AllFeatures = MysticTheurgeDivineSpellbookSelection.m_AllFeatures.AppendToArray(MysticTheurgeClawOfTheFalseWyrmProgression.ToReference<BlueprintFeatureReference>());

            var LoremasterClawOfTheFalseWyrmLevelUp = Helpers.CreateBlueprint<BlueprintFeature>("LoremasterClawOfTheFalseWyrmLevelUp", bp => {
                bp.SetName("Claw of the False Wyrm");
                bp.SetDescription("At 1st level, the mystic theurge selects a divine {g|Encyclopedia:Spell}spellcasting{/g} class she belonged to before adding the prestige class. When a " +
                    "new mystic theurge level is gained, the character gains new spells per day and new spells known as if she had also gained a level in that spellcasting class.");
                bp.AddComponent<AddSpellbookLevel>(c => {
                    c.m_Spellbook = ClawOfTheFalseWyrmSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = false;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.Ranks = 10;
            });
            var LoremasterClawOfTheFalseWyrmProgression = Helpers.CreateBlueprint<BlueprintProgression>("LoremasterClawOfTheFalseWyrmProgression", bp => {
                bp.SetName("Claw of the False Wyrm");
                bp.SetDescription("When a new loremaster level is gained, the character gains new {g|Encyclopedia:Spell}spells{/g} per day as if he had also gained a level in a " +
                    "spellcasting class he belonged to before adding the prestige class. He does not, however, gain other benefits a character of that class would have gained, " +
                    "except for additional spells per day, spells known (if he is a spontaneous spellcaster), and an increased effective level of spellcasting. If a character " +
                    "had more than one spellcasting class before becoming a loremaster, he must decide to which class he adds the new level for purposes of determining the " +
                    "number of spells per day.");
                bp.AddComponent<PrerequisiteClassSpellLevel>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.RequiredSpellLevel = 3;
                    c.HideInUI = false;
                });
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.m_CharacterClass = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ClawOfTheFalseWyrmArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 1;
                    c.HideInUI = false;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ReplaceSpellbook };
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.HideNotAvailibleInUI = true;
                bp.IsClassFeature = true;
                bp.m_AllowNonContextActions = false;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel() { AdditionalLevel = 0, m_Class = LoremasterClass.ToReference<BlueprintCharacterClassReference>() }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, LoremasterClawOfTheFalseWyrmLevelUp),
                    Helpers.LevelEntry(2, LoremasterClawOfTheFalseWyrmLevelUp),
                    Helpers.LevelEntry(3, LoremasterClawOfTheFalseWyrmLevelUp),
                    Helpers.LevelEntry(4, LoremasterClawOfTheFalseWyrmLevelUp),
                    Helpers.LevelEntry(5, LoremasterClawOfTheFalseWyrmLevelUp),
                    Helpers.LevelEntry(6, LoremasterClawOfTheFalseWyrmLevelUp),
                    Helpers.LevelEntry(7, LoremasterClawOfTheFalseWyrmLevelUp),
                    Helpers.LevelEntry(8, LoremasterClawOfTheFalseWyrmLevelUp),
                    Helpers.LevelEntry(9, LoremasterClawOfTheFalseWyrmLevelUp),
                    Helpers.LevelEntry(10, LoremasterClawOfTheFalseWyrmLevelUp)
                };
                bp.GiveFeaturesForPreviousLevels = false;
            });
            LoremasterSpellbookSelection.m_AllFeatures = LoremasterSpellbookSelection.m_AllFeatures.AppendToArray(LoremasterClawOfTheFalseWyrmProgression.ToReference<BlueprintFeatureReference>());

            #endregion

            #endregion
        }
    }
}
