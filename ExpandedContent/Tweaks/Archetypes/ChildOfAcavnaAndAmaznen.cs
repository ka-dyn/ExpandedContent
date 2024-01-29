using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Mechanics.Actions;
using ExpandedContent.Tweaks.Components;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using UnityEngine;
using Kingmaker.Craft;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Designers.Mechanics.Buffs;
using ExpandedContent.Tweaks.Classes.DrakeClass;
using Kingmaker.UI.UnitSettings.Blueprints;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class ChildOfAcavnaAndAmaznen {
        public static void AddChildOfAcavnaAndAmaznen() {

            var FighterClass = Resources.GetBlueprint<BlueprintCharacterClass>("48ac8db94d5de7645906c7d0ad3bcfbd");
            var FighterProficiencies = Resources.GetBlueprint<BlueprintFeature>("a23591cc77086494ba20880f87e73970");
            var FighterFeatSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("41c8486641f7d6d4283ca9dae4147a9f");

            var BloodragerSpellList = Resources.GetBlueprintReference<BlueprintSpellListReference>("98c05aeff6e3d384f8aec6d584973642");
            var RayCalculateFeature = Resources.GetBlueprint<BlueprintFeature>("d3e6275cfa6e7a04b9213b7b292a011c");
            var TouchCalculateFeature = Resources.GetBlueprint<BlueprintFeature>("62ef1cdb90f1d654d996556669caf7fa");
            var DetectMagic = Resources.GetBlueprint<BlueprintFeature>("ee0b69e90bac14446a4cf9a050f87f2e");
            var ScribingScrollsFeature = Resources.GetBlueprint<BlueprintFeature>("a8a385bf53ee3454593ce9054375a2ec");


            var ArcaneArmorMasteryFeature = Resources.GetBlueprint<BlueprintFeature>("453f5181a5ed3a445abfa3bcd3f4ac0c");
            var WeaponTrainingSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("b8cecf4e5e464ad41b79d5b42b76b399");
            var WeaponTrainingRankUpSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("5f3cc7b9a46b880448275763fe70c0b0");

            var DwarfRace = Resources.GetBlueprint<BlueprintRace>("c4faf439f0e70bd40b5e36ee80d06be7");
            var IconSR = AssetLoader.LoadInternal("Skills", "Icon_SR.png");



            var LightArmorProficiency = Resources.GetBlueprint<BlueprintFeature>("6d3728d4e9c9898458fe5e9532951132");
            var MediumArmorProficiency = Resources.GetBlueprint<BlueprintFeature>("46f4fb320f35704488ba3d513397789d");
            var HeavyArmorProficiency = Resources.GetBlueprint<BlueprintFeature>("1b0f68188dcc435429fb87a022239681");
            var SimpleWeaponProficiency = Resources.GetBlueprint<BlueprintFeature>("e70ecf1ed95ca2f40b754f1adb22bbdd");
            var MartialWeaponProficiency = Resources.GetBlueprint<BlueprintFeature>("203992ef5b35c864390b4e4a1e200629");
            var ShieldsProficiency = Resources.GetBlueprint<BlueprintFeature>("cb8686e7357a68c42bdd9d4e65334633");

            var SpearParryIcon = AssetLoader.LoadInternal("Skills", "Icon_SpearParry.png");



            var ChildOfAcavnaAndAmaznenDailyTable = Helpers.CreateBlueprint<BlueprintSpellsTable>("ChildOfAcavnaAndAmaznenDailyTable", bp => {
                bp.Levels = new SpellsLevelEntry[] {
                        SpellTools.CreateSpellLevelEntry(),//0
                        SpellTools.CreateSpellLevelEntry(0,0),//1
                        SpellTools.CreateSpellLevelEntry(0,0),//2
                        SpellTools.CreateSpellLevelEntry(0,0),//3
                        SpellTools.CreateSpellLevelEntry(0,0),
                        SpellTools.CreateSpellLevelEntry(0,1),//5
                        SpellTools.CreateSpellLevelEntry(0,1),
                        SpellTools.CreateSpellLevelEntry(0,1,0),//7
                        SpellTools.CreateSpellLevelEntry(0,1,1),
                        SpellTools.CreateSpellLevelEntry(0,2,1),//9
                        SpellTools.CreateSpellLevelEntry(0,2,1,0),
                        SpellTools.CreateSpellLevelEntry(0,2,1,1),//11
                        SpellTools.CreateSpellLevelEntry(0,2,2,1),
                        SpellTools.CreateSpellLevelEntry(0,3,2,1,0),
                        SpellTools.CreateSpellLevelEntry(0,3,2,1,1),
                        SpellTools.CreateSpellLevelEntry(0,3,2,2,1),//15
                        SpellTools.CreateSpellLevelEntry(0,3,3,2,1),
                        SpellTools.CreateSpellLevelEntry(0,4,3,2,1),
                        SpellTools.CreateSpellLevelEntry(0,4,3,2,2),//18
                        SpellTools.CreateSpellLevelEntry(0,4,3,3,2),
                        SpellTools.CreateSpellLevelEntry(0,4,4,3,3)//20
                };
            });
            var ChildOfAcavnaAndAmaznenSpellbook = Helpers.CreateBlueprint<BlueprintSpellbook>("ChildOfAcavnaAndAmaznenSpellbook", bp => {
                bp.Name = Helpers.CreateString($"ChildOfAcavnaAndAmaznenSpellbook.Name", "Child of Acavna and Amaznen");
                bp.IsMythic = false;
                bp.m_SpellsPerDay = ChildOfAcavnaAndAmaznenDailyTable.ToReference<BlueprintSpellsTableReference>();
                bp.m_SpellList = BloodragerSpellList;
                bp.m_CharacterClass = FighterClass.ToReference<BlueprintCharacterClassReference>();
                bp.CastingAttribute = StatType.Intelligence;
                bp.Spontaneous = false;
                bp.SpellsPerLevel = 1;
                bp.AllSpellsKnown = false;
                bp.CantripsType = CantripsType.Cantrips;
                bp.CasterLevelModifier = 0;
                bp.CanCopyScrolls = true;
                bp.IsArcane = true;
                bp.IsArcanist = false;
            });

            var ChildOfAcavnaAndAmaznenArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("ChildOfAcavnaAndAmaznenArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"ChildOfAcavnaAndAmaznenArchetype.Name", "Child of Acavna and Amaznen");
                bp.LocalizedDescription = Helpers.CreateString($"ChildOfAcavnaAndAmaznenArchetype.Description", "The child of Acavna and Amaznen has trained in the nearly forgotten arts of Azlant passed down " +
                    "by Aroden, which combine martial prowess with elementary wizardry. Inspired by the deities Aroden once worshiped long ago, children of Acavna and Amaznen strive to understand the dangers " +
                    "of the world and overcome them with knowledge and strength of arms.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"ChildOfAcavnaAndAmaznenArchetype.Description", "The child of Acavna and Amaznen has trained in the nearly forgotten arts of Azlant passed " +
                    "down by Aroden, which combine martial prowess with elementary wizardry. Inspired by the deities Aroden once worshiped long ago, children of Acavna and Amaznen strive to understand the " +
                    "dangers of the world and overcome them with knowledge and strength of arms.");
                bp.m_ReplaceSpellbook = ChildOfAcavnaAndAmaznenSpellbook.ToReference<BlueprintSpellbookReference>();
                bp.RemoveSpellbook = false;
                bp.BuildChanging = true;
                bp.ReplaceClassSkills = true;
                bp.ClassSkills = new StatType[] {
                    StatType.SkillAthletics,
                    StatType.SkillKnowledgeArcana,
                    StatType.SkillKnowledgeWorld,
                    StatType.SkillLoreNature,
                    StatType.SkillPersuasion,
                    StatType.SkillLoreReligion,
                    StatType.SkillUseMagicDevice
                };
                bp.ChangeCasterType = true;
                bp.IsDivineCaster = false;
                bp.IsArcaneCaster = true;
                bp.AddSkillPoints = 2;
                bp.OverrideAttributeRecommendations = true;
                bp.RecommendedAttributes = new StatType[] { StatType.Strength, StatType.Constitution, StatType.Intelligence};
                bp.NotRecommendedAttributes = new StatType[] { StatType.Charisma };
            });


            var OneHandedMartialWeaponProficiency = Helpers.CreateBlueprint<BlueprintFeature>("OneHandedMartialWeaponProficiency", bp => {
                bp.SetName("One Handed Martial Weapons Proficiency");
                bp.SetDescription("You become proficient with all Martial Weapons that can be used one-handed.");
                bp.AddComponent<AddProficiencies>(c => {
                    c.m_RaceRestriction = new BlueprintRaceReference();
                    c.ArmorProficiencies = new ArmorProficiencyGroup[0];
                    c.WeaponProficiencies = new WeaponCategory[] {
                        WeaponCategory.Handaxe,
                        WeaponCategory.Kukri,
                        WeaponCategory.LightHammer,
                        WeaponCategory.LightPick,
                        WeaponCategory.Shortsword,
                        WeaponCategory.Starknife,
                        WeaponCategory.Battleaxe,
                        WeaponCategory.Flail,
                        WeaponCategory.HeavyPick,
                        WeaponCategory.Longsword,
                        WeaponCategory.Rapier,
                        WeaponCategory.Scimitar,
                        WeaponCategory.Warhammer,
                        WeaponCategory.Trident,
                        WeaponCategory.ThrowingAxe
                    };
                });
                bp.AddComponent<AddProficiencies>(c => {
                    c.m_RaceRestriction = DwarfRace.ToReference<BlueprintRaceReference>();
                    c.ArmorProficiencies = new ArmorProficiencyGroup[0];
                    c.WeaponProficiencies = new WeaponCategory[] {
                        WeaponCategory.DwarvenWaraxe
                    };
                });
                bp.m_Icon = MartialWeaponProficiency.m_Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });


            var ChildOfAcavnaAndAmaznenSpellLock = Helpers.CreateBlueprint<BlueprintFeature>("ChildOfAcavnaAndAmaznenSpellLock", bp => {
                bp.SetName("Spell lock before lvl 5");
                bp.SetDescription("");
                bp.AddComponent<ForbidSpellbook>(c => {
                    c.m_Spellbook = ChildOfAcavnaAndAmaznenSpellbook.ToReference<BlueprintSpellbookReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
            });
            var ChildOfAcavnaAndAmaznenProficiencies = Helpers.CreateBlueprint<BlueprintFeature>("ChildOfAcavnaAndAmaznenProficiencies", bp => {
                bp.SetName("Child of Acavna and Amaznen Proficiencies");
                bp.SetDescription("A child of Acavna and Amaznen is proficient with all simple weapons, one-handed martial weapons, all armor (heavy, light, " +
                    "and medium) and shields (excluding tower shields).");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        LightArmorProficiency.ToReference<BlueprintUnitFactReference>(),
                        MediumArmorProficiency.ToReference<BlueprintUnitFactReference>(),
                        SimpleWeaponProficiency.ToReference<BlueprintUnitFactReference>(),
                        OneHandedMartialWeaponProficiency.ToReference<BlueprintUnitFactReference>(),
                        ShieldsProficiency.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var ChildOfAcavnaAndAmaznenDelayedCasting = Helpers.CreateBlueprint<BlueprintFeature>("ChildOfAcavnaAndAmaznenDelayedCasting", bp => {
                bp.SetName("Student of Spellcasting");
                bp.SetDescription("The child of Acavna and Amaznen gains a spellbook starting with spells from the bloodrager spelllist equal to 3 + their Intelligence modifier. " +
                    "\nWhile they are unable to cast these spells before 5th level, they may add one additional per level and copy spells from scrolls into the spellbook until then.");                
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.BeforeThisLevel = true;
                    c.Level = 5;
                    c.m_Class = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetypes = new BlueprintArchetypeReference[] { ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Feature = ChildOfAcavnaAndAmaznenSpellLock.ToReference<BlueprintFeatureReference>();
                });
                bp.m_Icon = IconSR;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var DismissAreaEffectSpell = Resources.GetBlueprint<BlueprintAbility>("97a23111df7547fd8f6417f9ba9b9775");

            var AcidSplashSpell = Resources.GetBlueprint<BlueprintAbility>("0c852a2405dd9f14a8bbcfaf245ff823");
            var RayOfFrostSpell = Resources.GetBlueprint<BlueprintAbility>("9af2ab69df6538f4793b2f9c3cc85603");
            var JoltSpell = Resources.GetBlueprint<BlueprintAbility>("16e23c7a8ae53cc42a93066d19766404");
            var DazeSpell = Resources.GetBlueprint<BlueprintAbility>("55f14bc84d7c85446b07a1b5dd6b2b4c");
            var ResistanceSpell = Resources.GetBlueprint<BlueprintAbility>("7bc8e27cba24f0e43ae64ed201ad5785");
            var TouchOfFatigueSpell = Resources.GetBlueprint<BlueprintAbility>("5bf3315ce1ed4d94e8805706820ef64d");
            var TouchOfFatigueEffectSpell = Resources.GetBlueprint<BlueprintAbility>("cbecdd04ad2523c438123ef596fd2b9f");
            var MageLightSpell = Resources.GetBlueprint<BlueprintAbility>("95f206566c5261c42aa5b3e7e0d1e36c");
            var DisruptUndeadSpell = Resources.GetBlueprint<BlueprintAbility>("652739779aa05504a9ad5db1db6d02ae");
            var FlareSpell = Resources.GetBlueprint<BlueprintAbility>("f0f8e5b9808f44e4eadd22b138131d52");

            var LoreOfAcavnaAndAmaznenHiddenCantrips = Helpers.CreateBlueprint<BlueprintFeature>("LoreOfAcavnaAndAmaznenHiddenCantrips", bp => {
                bp.SetName("Lore of Acavna and Amaznen Hidden");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        AcidSplashSpell.ToReference<BlueprintUnitFactReference>(),
                        RayOfFrostSpell.ToReference<BlueprintUnitFactReference>(),
                        JoltSpell.ToReference<BlueprintUnitFactReference>(),
                        DazeSpell.ToReference<BlueprintUnitFactReference>(),
                        ResistanceSpell.ToReference<BlueprintUnitFactReference>(),
                        TouchOfFatigueSpell.ToReference<BlueprintUnitFactReference>(),
                        MageLightSpell.ToReference<BlueprintUnitFactReference>(),
                        DisruptUndeadSpell.ToReference<BlueprintUnitFactReference>(),
                        FlareSpell.ToReference<BlueprintUnitFactReference>(),
                        DismissAreaEffectSpell.ToReference<BlueprintUnitFactReference>()
                    };
                    c.CasterLevel = 0;
                    c.DoNotRestoreMissingFacts = false;
                });
                bp.AddComponent<LearnSpells>(c => {
                    c.m_CharacterClass = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spells = new BlueprintAbilityReference[] {
                        AcidSplashSpell.ToReference<BlueprintAbilityReference>(),
                        RayOfFrostSpell.ToReference<BlueprintAbilityReference>(),
                        JoltSpell.ToReference<BlueprintAbilityReference>(),
                        DazeSpell.ToReference<BlueprintAbilityReference>(),
                        ResistanceSpell.ToReference<BlueprintAbilityReference>(),
                        TouchOfFatigueSpell.ToReference<BlueprintAbilityReference>(),
                        MageLightSpell.ToReference<BlueprintAbilityReference>(),
                        DisruptUndeadSpell.ToReference<BlueprintAbilityReference>(),
                        FlareSpell.ToReference<BlueprintAbilityReference>(),
                        DismissAreaEffectSpell.ToReference<BlueprintAbilityReference>()
                    };
                });
                bp.AddComponent<BindAbilitiesToClass>(c => {
                    c.m_CharacterClass = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Abilites = new BlueprintAbilityReference[] {
                        AcidSplashSpell.ToReference<BlueprintAbilityReference>(),
                        RayOfFrostSpell.ToReference<BlueprintAbilityReference>(),
                        JoltSpell.ToReference<BlueprintAbilityReference>(),
                        DazeSpell.ToReference<BlueprintAbilityReference>(),
                        ResistanceSpell.ToReference<BlueprintAbilityReference>(),
                        TouchOfFatigueSpell.ToReference<BlueprintAbilityReference>(),
                        TouchOfFatigueEffectSpell.ToReference<BlueprintAbilityReference>(),
                        MageLightSpell.ToReference<BlueprintAbilityReference>(),
                        DisruptUndeadSpell.ToReference<BlueprintAbilityReference>(),
                        FlareSpell.ToReference<BlueprintAbilityReference>()
                    };
                    c.Cantrip = true;
                    c.Stat = StatType.Intelligence;
                    c.LevelStep = 1;
                    c.Odd = false;
                    c.FullCasterChecks = true;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>();
                    c.SpellLevel = 0;
                    c.m_Spell = AcidSplashSpell.ToReference<BlueprintAbilityReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>();
                    c.SpellLevel = 0;
                    c.m_Spell = FlareSpell.ToReference<BlueprintAbilityReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>();
                    c.SpellLevel = 0;
                    c.m_Spell = RayOfFrostSpell.ToReference<BlueprintAbilityReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>();
                    c.SpellLevel = 0;
                    c.m_Spell = JoltSpell.ToReference<BlueprintAbilityReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>();
                    c.SpellLevel = 0;
                    c.m_Spell = DazeSpell.ToReference<BlueprintAbilityReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>();
                    c.SpellLevel = 0;
                    c.m_Spell = ResistanceSpell.ToReference<BlueprintAbilityReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>();
                    c.SpellLevel = 0;
                    c.m_Spell = TouchOfFatigueSpell.ToReference<BlueprintAbilityReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>();
                    c.SpellLevel = 0;
                    c.m_Spell = MageLightSpell.ToReference<BlueprintAbilityReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>();
                    c.SpellLevel = 0;
                    c.m_Spell = DisruptUndeadSpell.ToReference<BlueprintAbilityReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var LoreOfAcavnaAndAmaznenFeature = Helpers.CreateBlueprint<BlueprintFeature>("LoreOfAcavnaAndAmaznenFeature", bp => {
                bp.SetName("Lore of Acavna and Amaznen");
                bp.SetDescription("At 2nd level, a child of Acavna and Amaznen is further initiated into the arcane secrets passed down from Azlant. This gives her minor spellcasting abilities and access " +
                    "to lore collected to give her an edge against the enemies of humanity.\nThe child of Acavna and Amaznen gains the ability to cast 4 cantrips as spell-like abilities, bypassing their " + 
                    "normal inability to cast spells. \nShe also gains a +2 bonus on Knowledge checks.");                
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillKnowledgeArcana;
                    c.Value = 2;
                });
                bp.AddComponent<AddStatBonus>(c => {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Stat = StatType.SkillKnowledgeWorld;
                    c.Value = 2;
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });

            var SpelllikeAcidSplashAbility = Helpers.CreateBlueprint<BlueprintAbility>("SpelllikeAcidSplashAbility", bp => {
                bp.m_DisplayName = AcidSplashSpell.m_DisplayName;
                bp.m_Description = AcidSplashSpell.m_Description;
                bp.m_Icon = AcidSplashSpell.m_Icon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.HasFastAnimation = false;
                bp.m_TargetMapObjects = true;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.m_IsFullRoundAction = false;
                bp.LocalizedDuration = AcidSplashSpell.LocalizedDuration;
                bp.LocalizedSavingThrow = AcidSplashSpell.LocalizedSavingThrow;
                bp.Components = AcidSplashSpell.Components;
                bp.RemoveComponents<SpellListComponent>();
                bp.RemoveComponents<CantripComponent>();
                bp.RemoveComponents<ActionPanelLogic>();
            });
            var SpelllikeRayOfFrostAbility = Helpers.CreateBlueprint<BlueprintAbility>("SpelllikeRayOfFrostAbility", bp => {
                bp.m_DisplayName = RayOfFrostSpell.m_DisplayName;
                bp.m_Description = RayOfFrostSpell.m_Description;
                bp.m_Icon = RayOfFrostSpell.m_Icon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.HasFastAnimation = false;
                bp.m_TargetMapObjects = true;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.m_IsFullRoundAction = false;
                bp.LocalizedDuration = RayOfFrostSpell.LocalizedDuration;
                bp.LocalizedSavingThrow = RayOfFrostSpell.LocalizedSavingThrow;
                bp.Components = RayOfFrostSpell.Components;
                bp.RemoveComponents<SpellListComponent>();
                bp.RemoveComponents<CantripComponent>();
                bp.RemoveComponents<ActionPanelLogic>();
            });
            var SpelllikeJoltAbility = Helpers.CreateBlueprint<BlueprintAbility>("SpelllikeJoltAbility", bp => {
                bp.m_DisplayName = JoltSpell.m_DisplayName;
                bp.m_Description = JoltSpell.m_Description;
                bp.m_Icon = JoltSpell.m_Icon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.HasFastAnimation = false;
                bp.m_TargetMapObjects = true;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.m_IsFullRoundAction = false;
                bp.LocalizedDuration = JoltSpell.LocalizedDuration;
                bp.LocalizedSavingThrow = JoltSpell.LocalizedSavingThrow;
                bp.Components = JoltSpell.Components;
                bp.RemoveComponents<SpellListComponent>();
                bp.RemoveComponents<CantripComponent>();
            });
            var SpelllikeDazeAbility = Helpers.CreateBlueprint<BlueprintAbility>("SpelllikeDazeAbility", bp => {
                bp.m_DisplayName = DazeSpell.m_DisplayName;
                bp.m_Description = DazeSpell.m_Description;
                bp.m_Icon = DazeSpell.m_Icon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.ShouldTurnToTarget = true;
                bp.SpellResistance = true;
                bp.IgnoreSpellResistanceForAlly = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.HasFastAnimation = false;
                bp.m_TargetMapObjects = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.m_IsFullRoundAction = false;
                bp.LocalizedDuration = DazeSpell.LocalizedDuration;
                bp.LocalizedSavingThrow = DazeSpell.LocalizedSavingThrow;
                bp.Components = DazeSpell.Components;
                bp.RemoveComponents<SpellListComponent>();
                bp.RemoveComponents<CantripComponent>();
            });
            var SpelllikeResistanceAbility = Helpers.CreateBlueprint<BlueprintAbility>("SpelllikeResistanceAbility", bp => {
                bp.m_DisplayName = ResistanceSpell.m_DisplayName;
                bp.m_Description = ResistanceSpell.m_Description;
                bp.m_Icon = ResistanceSpell.m_Icon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.HasFastAnimation = false;
                bp.m_TargetMapObjects = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.m_IsFullRoundAction = false;
                bp.LocalizedDuration = ResistanceSpell.LocalizedDuration;
                bp.LocalizedSavingThrow = ResistanceSpell.LocalizedSavingThrow;
                bp.Components = ResistanceSpell.Components;
                bp.RemoveComponents<SpellListComponent>();
                bp.RemoveComponents<CantripComponent>();
            });
            var SpelllikeTouchOfFatigueAbility = Helpers.CreateBlueprint<BlueprintAbility>("SpelllikeTouchOfFatigueAbility", bp => {
                bp.m_DisplayName = TouchOfFatigueSpell.m_DisplayName;
                bp.m_Description = TouchOfFatigueSpell.m_Description;
                bp.m_Icon = TouchOfFatigueSpell.m_Icon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.HasFastAnimation = false;
                bp.m_TargetMapObjects = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.m_IsFullRoundAction = false;
                bp.LocalizedDuration = TouchOfFatigueSpell.LocalizedDuration;
                bp.LocalizedSavingThrow = TouchOfFatigueSpell.LocalizedSavingThrow;
                bp.Components = TouchOfFatigueSpell.Components;
                bp.RemoveComponents<SpellListComponent>();
                bp.RemoveComponents<CantripComponent>();
                bp.RemoveComponents<ActionPanelLogic>();
            });
            var SpelllikeMageLightAbility = Helpers.CreateBlueprint<BlueprintAbility>("SpelllikeMageLightAbility", bp => {
                bp.m_DisplayName = MageLightSpell.m_DisplayName;
                bp.m_Description = MageLightSpell.m_Description;
                bp.m_Icon = MageLightSpell.m_Icon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.HasFastAnimation = false;
                bp.m_TargetMapObjects = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.m_IsFullRoundAction = false;
                bp.LocalizedDuration = MageLightSpell.LocalizedDuration;
                bp.LocalizedSavingThrow = MageLightSpell.LocalizedSavingThrow;
                bp.Components = MageLightSpell.Components;
                bp.RemoveComponents<SpellListComponent>();
                bp.RemoveComponents<CantripComponent>();
            });
            var SpelllikeDisruptUndeadAbility = Helpers.CreateBlueprint<BlueprintAbility>("SpelllikeDisruptUndeadAbility", bp => {
                bp.m_DisplayName = DisruptUndeadSpell.m_DisplayName;
                bp.m_Description = DisruptUndeadSpell.m_Description;
                bp.m_Icon = DisruptUndeadSpell.m_Icon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Directional;
                bp.HasFastAnimation = false;
                bp.m_TargetMapObjects = true;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.m_IsFullRoundAction = false;
                bp.LocalizedDuration = DisruptUndeadSpell.LocalizedDuration;
                bp.LocalizedSavingThrow = DisruptUndeadSpell.LocalizedSavingThrow;
                bp.Components = DisruptUndeadSpell.Components;
                bp.RemoveComponents<SpellListComponent>();
                bp.RemoveComponents<CantripComponent>();
            });
            var SpelllikeFlareAbility = Helpers.CreateBlueprint<BlueprintAbility>("SpelllikeFlareAbility", bp => {
                bp.m_DisplayName = FlareSpell.m_DisplayName;
                bp.m_Description = FlareSpell.m_Description;
                bp.m_Icon = FlareSpell.m_Icon;
                bp.Type = AbilityType.SpellLike;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.HasFastAnimation = false;
                bp.m_TargetMapObjects = false;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.m_IsFullRoundAction = false;
                bp.LocalizedDuration = FlareSpell.LocalizedDuration;
                bp.LocalizedSavingThrow = FlareSpell.LocalizedSavingThrow;
                bp.Components = FlareSpell.Components;
                bp.RemoveComponents<SpellListComponent>();
                bp.RemoveComponents<CantripComponent>();
            });            

            var SpelllikeAcidSplashFeature = Helpers.CreateBlueprint<BlueprintFeature>("SpelllikeAcidSplashFeature", bp => {
                bp.SetName("SpelllikeAcidSplashFeature");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SpelllikeAcidSplashAbility.ToReference<BlueprintUnitFactReference>(),
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });
            var SpelllikeAcidSplashFeatureDisabler = Helpers.CreateBlueprint<BlueprintFeature>("SpelllikeAcidSplashFeatureDisabler", bp => {
                bp.m_DisplayName = AcidSplashSpell.m_DisplayName;
                bp.m_Description = AcidSplashSpell.m_Description;
                bp.m_Icon = AcidSplashSpell.m_Icon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.BeforeThisLevel = true;
                    c.Level = 5;
                    c.m_Class = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetypes = new BlueprintArchetypeReference[] { ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Feature = SpelllikeAcidSplashFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var SpelllikeRayOfFrostFeature = Helpers.CreateBlueprint<BlueprintFeature>("SpelllikeRayOfFrostFeature", bp => {
                bp.SetName("SpelllikeRayOfFrostFeature");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SpelllikeRayOfFrostAbility.ToReference<BlueprintUnitFactReference>(),
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });
            var SpelllikeRayOfFrostFeatureDisabler = Helpers.CreateBlueprint<BlueprintFeature>("SpelllikeRayOfFrostFeatureDisabler", bp => {
                bp.m_DisplayName = RayOfFrostSpell.m_DisplayName;
                bp.m_Description = RayOfFrostSpell.m_Description;
                bp.m_Icon = RayOfFrostSpell.m_Icon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.BeforeThisLevel = true;
                    c.Level = 5;
                    c.m_Class = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetypes = new BlueprintArchetypeReference[] { ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Feature = SpelllikeRayOfFrostFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var SpelllikeJoltFeature = Helpers.CreateBlueprint<BlueprintFeature>("SpelllikeJoltFeature", bp => {
                bp.SetName("SpelllikeJoltFeature");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SpelllikeJoltAbility.ToReference<BlueprintUnitFactReference>(),
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });
            var SpelllikeJoltFeatureDisabler = Helpers.CreateBlueprint<BlueprintFeature>("SpelllikeJoltFeatureDisabler", bp => {
                bp.m_DisplayName = JoltSpell.m_DisplayName;
                bp.m_Description = JoltSpell.m_Description;
                bp.m_Icon = JoltSpell.m_Icon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.BeforeThisLevel = true;
                    c.Level = 5;
                    c.m_Class = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetypes = new BlueprintArchetypeReference[] { ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Feature = SpelllikeJoltFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var SpelllikeDazeFeature = Helpers.CreateBlueprint<BlueprintFeature>("SpelllikeDazeFeature", bp => {
                bp.SetName("SpelllikeDazeFeature");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SpelllikeDazeAbility.ToReference<BlueprintUnitFactReference>(),
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });
            var SpelllikeDazeFeatureDisabler = Helpers.CreateBlueprint<BlueprintFeature>("SpelllikeDazeFeatureDisabler", bp => {
                bp.m_DisplayName = DazeSpell.m_DisplayName;
                bp.m_Description = DazeSpell.m_Description;
                bp.m_Icon = DazeSpell.m_Icon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.BeforeThisLevel = true;
                    c.Level = 5;
                    c.m_Class = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetypes = new BlueprintArchetypeReference[] { ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Feature = SpelllikeDazeFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var SpelllikeResistanceFeature = Helpers.CreateBlueprint<BlueprintFeature>("SpelllikeResistanceFeature", bp => {
                bp.SetName("SpelllikeResistanceFeature");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SpelllikeResistanceAbility.ToReference<BlueprintUnitFactReference>(),
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });
            var SpelllikeResistanceFeatureDisabler = Helpers.CreateBlueprint<BlueprintFeature>("SpelllikeResistanceFeatureDisabler", bp => {
                bp.m_DisplayName = ResistanceSpell.m_DisplayName;
                bp.m_Description = ResistanceSpell.m_Description;
                bp.m_Icon = ResistanceSpell.m_Icon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.BeforeThisLevel = true;
                    c.Level = 5;
                    c.m_Class = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetypes = new BlueprintArchetypeReference[] { ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Feature = SpelllikeResistanceFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var SpelllikeTouchOfFatigueFeature = Helpers.CreateBlueprint<BlueprintFeature>("SpelllikeTouchOfFatigueFeature", bp => {
                bp.SetName("SpelllikeTouchOfFatigueFeature");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SpelllikeTouchOfFatigueAbility.ToReference<BlueprintUnitFactReference>(),
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });
            var SpelllikeTouchOfFatigueFeatureDisabler = Helpers.CreateBlueprint<BlueprintFeature>("SpelllikeTouchOfFatigueFeatureDisabler", bp => {
                bp.m_DisplayName = TouchOfFatigueSpell.m_DisplayName;
                bp.m_Description = TouchOfFatigueSpell.m_Description;
                bp.m_Icon = TouchOfFatigueSpell.m_Icon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.BeforeThisLevel = true;
                    c.Level = 5;
                    c.m_Class = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetypes = new BlueprintArchetypeReference[] { ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Feature = SpelllikeTouchOfFatigueFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var SpelllikeMageLightFeature = Helpers.CreateBlueprint<BlueprintFeature>("SpelllikeMageLightFeature", bp => {
                bp.SetName("SpelllikeMageLightFeature");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SpelllikeMageLightAbility.ToReference<BlueprintUnitFactReference>(),
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });
            var SpelllikeMageLightFeatureDisabler = Helpers.CreateBlueprint<BlueprintFeature>("SpelllikeMageLightFeatureDisabler", bp => {
                bp.m_DisplayName = MageLightSpell.m_DisplayName;
                bp.m_Description = MageLightSpell.m_Description;
                bp.m_Icon = MageLightSpell.m_Icon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.BeforeThisLevel = true;
                    c.Level = 5;
                    c.m_Class = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetypes = new BlueprintArchetypeReference[] { ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Feature = SpelllikeMageLightFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var SpelllikeDisruptUndeadFeature = Helpers.CreateBlueprint<BlueprintFeature>("SpelllikeDisruptUndeadFeature", bp => {
                bp.SetName("SpelllikeDisruptUndeadFeature");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SpelllikeDisruptUndeadAbility.ToReference<BlueprintUnitFactReference>(),
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });
            var SpelllikeDisruptUndeadFeatureDisabler = Helpers.CreateBlueprint<BlueprintFeature>("SpelllikeDisruptUndeadFeatureDisabler", bp => {
                bp.m_DisplayName = DisruptUndeadSpell.m_DisplayName;
                bp.m_Description = DisruptUndeadSpell.m_Description;
                bp.m_Icon = DisruptUndeadSpell.m_Icon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.BeforeThisLevel = true;
                    c.Level = 5;
                    c.m_Class = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetypes = new BlueprintArchetypeReference[] { ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Feature = SpelllikeDisruptUndeadFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var SpelllikeFlareFeature = Helpers.CreateBlueprint<BlueprintFeature>("SpelllikeFlareFeature", bp => {
                bp.SetName("SpelllikeFlareFeature");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        SpelllikeFlareAbility.ToReference<BlueprintUnitFactReference>(),
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
            });
            var SpelllikeFlareFeatureDisabler = Helpers.CreateBlueprint<BlueprintFeature>("SpelllikeFlareFeatureDisabler", bp => {
                bp.m_DisplayName = FlareSpell.m_DisplayName;
                bp.m_Description = FlareSpell.m_Description;
                bp.m_Icon = FlareSpell.m_Icon;
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.BeforeThisLevel = true;
                    c.Level = 5;
                    c.m_Class = FighterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetypes = new BlueprintArchetypeReference[] { ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Feature = SpelllikeFlareFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            var LoreOfAcavnaAndAmaznenCantripSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("LoreOfAcavnaAndAmaznenCantripSelection", bp => {
                bp.SetName("Lore of Acavna and Amaznen");
                bp.SetDescription("The child of Acavna and Amaznen gains the ability to cast 4 cantrips as spell-like abilities, bypassing their " +
                    "normal inability to cast spells.");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.HideInUI = true;
                bp.Group = FeatureGroup.None;
                bp.Mode = SelectionMode.OnlyNew;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {                    
                    SpelllikeAcidSplashFeatureDisabler.ToReference<BlueprintFeatureReference>(),
                    SpelllikeRayOfFrostFeatureDisabler.ToReference<BlueprintFeatureReference>(),
                    SpelllikeJoltFeatureDisabler.ToReference<BlueprintFeatureReference>(),
                    SpelllikeDazeFeatureDisabler.ToReference<BlueprintFeatureReference>(),
                    SpelllikeResistanceFeatureDisabler.ToReference<BlueprintFeatureReference>(),
                    SpelllikeTouchOfFatigueFeatureDisabler.ToReference<BlueprintFeatureReference>(),
                    SpelllikeMageLightFeatureDisabler.ToReference<BlueprintFeatureReference>(),
                    SpelllikeDisruptUndeadFeatureDisabler.ToReference<BlueprintFeatureReference>(),
                    SpelllikeFlareFeatureDisabler.ToReference<BlueprintFeatureReference>(),
                };
            });

            var ChildOfAcavnaAndAmaznenCastingUnlock = Helpers.CreateBlueprint<BlueprintFeature>("ChildOfAcavnaAndAmaznenCastingUnlock", bp => {
                bp.SetName("Martial Wizardry");
                bp.SetDescription("The child of Acavna and Amaznen completes their initial studies, unlocking the ability to cast all cantrips and all learnt 1st level spells. The " +
                    "caster level for these spells is equal to their fighter level. \nThe spell-like cantrips gained from Lore of Acavna and Amaznen are removed in favour of the unlocked " +
                    "cantrips.");
                bp.m_Icon = FlareSpell.m_Icon;                
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            var EldritchArmorTrainingRank30 = Helpers.CreateBlueprint<BlueprintFeature>("EldritchArmorTrainingRank30", bp => {
                bp.SetName("Eldritch Armor Training");
                bp.SetDescription("At 3rd level, a child of Acavna and Amaznen gains eldritch armor training. As a swift action she can also reduce the arcane spell failure " +
                    "chance due to armor she is wearing by 15% for any spells she casts this round. This reduction increases to 20% at 7th level, 25% at 11th level, and 30% at 15th level." +
                    "\nThis ability stacks with the arcane spell failure chance from the arcane armor training and arcane armor mastery feats.");
                bp.m_Icon = ArcaneArmorMasteryFeature.m_Icon;                
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            var EldritchArmorTrainingRank25 = Helpers.CreateBlueprint<BlueprintFeature>("EldritchArmorTrainingRank25", bp => {
                bp.SetName("Eldritch Armor Training");
                bp.SetDescription("At 3rd level, a child of Acavna and Amaznen gains eldritch armor training. As a swift action she can also reduce the arcane spell failure " +
                    "chance due to armor she is wearing by 15% for any spells she casts this round. This reduction increases to 20% at 7th level, 25% at 11th level, and 30% at 15th level." +
                    "\nThis ability stacks with the arcane spell failure chance from the arcane armor training and arcane armor mastery feats.");
                bp.m_Icon = ArcaneArmorMasteryFeature.m_Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });
            var EldritchArmorTrainingRank20 = Helpers.CreateBlueprint<BlueprintFeature>("EldritchArmorTrainingRank20", bp => {
                bp.SetName("Eldritch Armor Training");
                bp.SetDescription("At 3rd level, a child of Acavna and Amaznen gains eldritch armor training. As a swift action she can also reduce the arcane spell failure " +
                    "chance due to armor she is wearing by 15% for any spells she casts this round. This reduction increases to 20% at 7th level, 25% at 11th level, and 30% at 15th level." +
                    "\nThis ability stacks with the arcane spell failure chance from the arcane armor training and arcane armor mastery feats.");
                bp.m_Icon = ArcaneArmorMasteryFeature.m_Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });

            var EldritchArmorTrainingBuff30 = Helpers.CreateBuff("EldritchArmorTrainingBuff30", bp => {
                bp.SetName("Eldritch Armor Training");
                bp.SetDescription("Arcane spell failure chance due to armor she is wearing is reduced by 30% for any spells she casts this round." +
                    "\nThis ability stacks with the arcane spell failure chance from the arcane armor training and arcane armor mastery feats.");
                bp.m_Icon = ArcaneArmorMasteryFeature.m_Icon;
                bp.AddComponent<ArcaneSpellFailureIncrease>(c => {
                    c.Bonus = -30;
                    c.ToShield = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnResurrect;
                bp.Stacking = StackingType.Replace;
            });
            var EldritchArmorTrainingBuff25 = Helpers.CreateBuff("EldritchArmorTrainingBuff25", bp => {
                bp.SetName("Eldritch Armor Training");
                bp.SetDescription("Arcane spell failure chance due to armor she is wearing is reduced by 25% for any spells she casts this round." +
                    "\nThis ability stacks with the arcane spell failure chance from the arcane armor training and arcane armor mastery feats.");
                bp.m_Icon = ArcaneArmorMasteryFeature.m_Icon;
                bp.AddComponent<ArcaneSpellFailureIncrease>(c => {
                    c.Bonus = -25;
                    c.ToShield = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnResurrect;
                bp.Stacking = StackingType.Replace;
            });
            var EldritchArmorTrainingBuff20 = Helpers.CreateBuff("EldritchArmorTrainingBuff20", bp => {
                bp.SetName("Eldritch Armor Training");
                bp.SetDescription("Arcane spell failure chance due to armor she is wearing is reduced by 20% for any spells she casts this round." +
                    "\nThis ability stacks with the arcane spell failure chance from the arcane armor training and arcane armor mastery feats.");
                bp.m_Icon = ArcaneArmorMasteryFeature.m_Icon;
                bp.AddComponent<ArcaneSpellFailureIncrease>(c => {
                    c.Bonus = -20;
                    c.ToShield = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnResurrect;
                bp.Stacking = StackingType.Replace;
            });
            var EldritchArmorTrainingBuff15 = Helpers.CreateBuff("EldritchArmorTrainingBuff15", bp => {
                bp.SetName("Eldritch Armor Training");
                bp.SetDescription("Arcane spell failure chance due to armor she is wearing is reduced by 15% for any spells she casts this round." +
                    "\nThis ability stacks with the arcane spell failure chance from the arcane armor training and arcane armor mastery feats.");
                bp.m_Icon = ArcaneArmorMasteryFeature.m_Icon;
                bp.AddComponent<ArcaneSpellFailureIncrease>(c => {
                    c.Bonus = -15;
                    c.ToShield = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnResurrect;
                bp.Stacking = StackingType.Replace;
            });

            var EldritchArmorTrainingAbility = Helpers.CreateBlueprint<BlueprintAbility>("EldritchArmorTrainingAbility", bp => {
                bp.SetName("Eldritch Armor Training");
                bp.SetDescription("As a swift action she can also reduce the arcane spell failure chance due to armor she is wearing by 15% for any spells she " +
                    "casts this round. This reduction increases to 20% at 7th level, 25% at 11th level, and 30% at 15th level." +
                    "\nThis ability stacks with the arcane spell failure chance from the arcane armor training and arcane armor mastery feats.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.And,
                                Conditions = new Condition[] {
                                    new ContextConditionCasterHasFact() {
                                        m_Fact = EldritchArmorTrainingRank30.ToReference<BlueprintUnitFactReference>(),
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = EldritchArmorTrainingBuff30.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 1
                                    },
                                    DurationSeconds = 0
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.And,
                                        Conditions = new Condition[] {
                                            new ContextConditionCasterHasFact() {
                                                m_Fact = EldritchArmorTrainingRank25.ToReference<BlueprintUnitFactReference>(),
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = EldritchArmorTrainingBuff25.ToReference<BlueprintBuffReference>(),
                                            Permanent = false,
                                            UseDurationSeconds = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = 1
                                            },
                                            DurationSeconds = 0
                                        }
                                        ),
                                    IfFalse = Helpers.CreateActionList(
                                        new Conditional() {
                                            ConditionsChecker = new ConditionsChecker() {
                                                Operation = Operation.And,
                                                Conditions = new Condition[] {
                                                    new ContextConditionCasterHasFact() {
                                                        m_Fact = EldritchArmorTrainingRank20.ToReference<BlueprintUnitFactReference>(),
                                                        Not = false
                                                    }
                                                }
                                            },
                                            IfTrue = Helpers.CreateActionList(
                                                new ContextActionApplyBuff() {
                                                    m_Buff = EldritchArmorTrainingBuff20.ToReference<BlueprintBuffReference>(),
                                                    Permanent = false,
                                                    UseDurationSeconds = false,
                                                    DurationValue = new ContextDurationValue() {
                                                        Rate = DurationRate.Rounds,
                                                        DiceType = DiceType.Zero,
                                                        DiceCountValue = 0,
                                                        BonusValue = 1
                                                    },
                                                    DurationSeconds = 0
                                                }
                                                ),
                                            IfFalse = Helpers.CreateActionList(
                                                new ContextActionApplyBuff() {
                                                    m_Buff = EldritchArmorTrainingBuff15.ToReference<BlueprintBuffReference>(),
                                                    Permanent = false,
                                                    UseDurationSeconds = false,
                                                    DurationValue = new ContextDurationValue() {
                                                        Rate = DurationRate.Rounds,
                                                        DiceType = DiceType.Zero,
                                                        DiceCountValue = 0,
                                                        BonusValue = 1
                                                    },
                                                    DurationSeconds = 0
                                                }
                                                )
                                        }
                                        )
                                }
                                )
                        }
                        );
                });
                bp.m_Icon = ArcaneArmorMasteryFeature.m_Icon;
                bp.Type = AbilityType.Extraordinary;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.SelfTouch;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("EldritchArmorTrainingAbility.Duration", "One round");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var EldritchArmorTrainingFeature = Helpers.CreateBlueprint<BlueprintFeature>("EldritchArmorTrainingFeature", bp => {
                bp.SetName("Eldritch Armor Training");
                bp.SetDescription("At 3rd level, a child of Acavna and Amaznen gains eldritch armor training. As a swift action she can also reduce the arcane spell failure " +
                    "chance due to armor she is wearing by 15% for any spells she casts this round. This reduction increases to 20% at 7th level, 25% at 11th level, and 30% at 15th level." +
                    "\nThis ability stacks with the arcane spell failure chance from the arcane armor training and arcane armor mastery feats.");
                bp.m_Icon = ArcaneArmorMasteryFeature.m_Icon;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        EldritchArmorTrainingAbility.ToReference<BlueprintUnitFactReference>(),
                    };
                });
                bp.Ranks = 1;
                bp.IsClassFeature = true;
            });

            ChildOfAcavnaAndAmaznenArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, FighterProficiencies, FighterFeatSelection),
                    Helpers.LevelEntry(2, FighterFeatSelection),
                    Helpers.LevelEntry(5, WeaponTrainingSelection),
                    Helpers.LevelEntry(8, FighterFeatSelection),
                    Helpers.LevelEntry(9, WeaponTrainingRankUpSelection),
                    Helpers.LevelEntry(14, FighterFeatSelection),
                    Helpers.LevelEntry(20, FighterFeatSelection)
            };
            ChildOfAcavnaAndAmaznenArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, ChildOfAcavnaAndAmaznenDelayedCasting, ChildOfAcavnaAndAmaznenProficiencies, RayCalculateFeature, TouchCalculateFeature, DetectMagic),
                    Helpers.LevelEntry(2, LoreOfAcavnaAndAmaznenFeature, LoreOfAcavnaAndAmaznenHiddenCantrips, LoreOfAcavnaAndAmaznenCantripSelection, LoreOfAcavnaAndAmaznenCantripSelection, LoreOfAcavnaAndAmaznenCantripSelection, LoreOfAcavnaAndAmaznenCantripSelection),
                    Helpers.LevelEntry(3, EldritchArmorTrainingFeature),
                    Helpers.LevelEntry(5, ChildOfAcavnaAndAmaznenCastingUnlock),
                    Helpers.LevelEntry(7, EldritchArmorTrainingRank20),
                    Helpers.LevelEntry(11, EldritchArmorTrainingRank25),
                    Helpers.LevelEntry(15, EldritchArmorTrainingRank30)
            };
            
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Child of Acavna and Amaznen")) { return; }
            FighterClass.m_Archetypes = FighterClass.m_Archetypes.AppendToArray(ChildOfAcavnaAndAmaznenArchetype.ToReference<BlueprintArchetypeReference>());

        }
    }
}
