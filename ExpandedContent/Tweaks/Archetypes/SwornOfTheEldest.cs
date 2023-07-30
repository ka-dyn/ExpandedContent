using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Components;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using System.Collections.Generic;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class SwornOfTheEldest {

        public static void AddSwornOfTheEldest() {

            var name = "SwornOfTheEldest";
            var uiName = "Sworn of the Eldest";

            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var InquisitorSpellbook = Resources.GetBlueprint<BlueprintSpellbook>("57fab75111f377248810ece84193a5a5");

            var cunningInitiative = Resources.GetBlueprintReference<BlueprintFeatureReference>("6be8b4031d8b9fc4f879b72b5428f1e0");
            var sternGaze = Resources.GetBlueprint<BlueprintFeature>("a6d917fd5c9bee0449bd01c92e3b0308");
            var soloTactics = Resources.GetBlueprintReference<BlueprintFeatureReference>("5602845cd22683840a6f28ec46331051");
            var teamworkFeat = Resources.GetBlueprintReference<BlueprintFeatureReference>("d87e2f6a9278ac04caeb0f93eff95fcb");

            var trueJudgmentFeature = Resources.GetBlueprint<BlueprintFeature>("f069b6557a2013544ac3636219186632");



            var whimsicalWorship = Helpers.CreateBlueprint<BlueprintFeature>($"WhimsicalWorshipFeature{name}", bp => {
                bp.SetName("Whimsical Worship");
                bp.SetDescription("A sworn of the Eldest uses her Charisma modifier, rather than her Wisdom modifier, " +
                    "to determine all class features and effects relating to her inquisitor class, including her spells, " +
                    "cunning initiative, and true judgment abilities.");
                bp.IsClassFeature = true;
            });

            var swornOfTheEldestCunningInitiative = Helpers.CreateBlueprint<BlueprintFeature>($"CunningInitiativeFeature{name}", bp => {
                bp.SetName("Cunning Initiative");
                bp.SetDescription("At 2nd level, a sworn of the Eldest adds her {g|Encyclopedia:Charisma}Charisma{/g} modifier on {g|Encyclopedia:Initiative}initiative{/g} {g|Encyclopedia:Check}checks{/g}, in addition to her {g|Encyclopedia:Dexterity}Dexterity{/g} modifier.");
                bp.AddComponent<DerivativeStatBonus>(c => {
                    c.BaseStat = StatType.Charisma;
                    c.DerivativeStat = StatType.Initiative;
                    c.Descriptor = Kingmaker.Enums.ModifierDescriptor.None;
                });
                bp.AddComponent<RecalculateOnStatChange>(c => {
                    c.Stat = StatType.Charisma;
                });
                bp.IsClassFeature = true;
            });


            var swornOfTheEldestFeytongue = Helpers.CreateBlueprint<BlueprintFeature>($"FeytongueFeature{name}", bp => {
                bp.SetName("Feytongue");
                bp.SetDescription("A sworn of the Eldest receives a morale bonus equal to half her inquisitor level (minimum +1) on Bluff and Diplomacy checks.");

                bp.AddComponent(sternGaze.GetComponent<ContextRankConfig>()); //half of Inquisitor level
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = Kingmaker.Enums.ModifierDescriptor.Morale;
                    c.Stat = StatType.CheckBluff;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = Kingmaker.Enums.AbilityRankType.Default
                    };
                });
                bp.AddComponent<AddContextStatBonus>(c => {
                    c.Descriptor = Kingmaker.Enums.ModifierDescriptor.Morale;
                    c.Stat = StatType.CheckDiplomacy;
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = Kingmaker.Enums.AbilityRankType.Default
                    };
                });
                bp.IsClassFeature = true;
            });

            var swornOfTheEldestFeywatcher = Helpers.CreateBlueprint<BlueprintFeature>($"{name}Feywatcher", bp => {
                bp.SetName("Feywatcher");
                bp.SetDescription("Resistant to the tricky ways of other fey, a sworn of the Eldest gains the resist nature’s lure druid class feature.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        Resources.GetBlueprintReference<BlueprintUnitFactReference>("ad6a5b0e1a65c3540986cf9a7b006388")
                    };
                });
                bp.IsClassFeature = true;
            });

            List<BlueprintFeature> magics = new();
            var clericClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("67819271767a9dd4fbfd4ae700befea0");
            var domainSelection = Resources.GetBlueprintReference<BlueprintFeatureSelectionReference>("48525e5da45c9c243a343fc6545dbdb9");
            for (int i = 1; i < 7; i++) {
                var swornOfTheEldestMagicoftheEldest = Helpers.CreateBlueprint<BlueprintFeature>($"MagicoftheEldest{name}{i}", bp => {
                    bp.SetName("Magic of the Eldest");
                    bp.SetDescription("At 3rd level and every 3 levels thereafter, a sworn of the Eldest gains a bonus spell slot of the highest spell level she can cast, and she learns the corresponding spell listed for her domain as a bonus spell known. She can use this bonus spell slot only to cast the domain spell of that level.");
                    bp.AddComponent<AddSpellsPerDay>(c => {
                        c.Amount = 1;
                        c.Levels = new int[] { i };
                    });
                    bp.AddComponent<LearnDomainSpell>(c => {
                        c.CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                        c.ClericClass = clericClass;
                        c.SpellLevel = i;
                        c.DomainSelection = domainSelection;
                    });
                    bp.ReapplyOnLevelUp = true;
                    bp.IsClassFeature = true;
                });
                magics.Add(swornOfTheEldestMagicoftheEldest);
            }


            var swornOfTheEldestSpellbook = Helpers.CreateBlueprint<BlueprintSpellbook>($"{name}Spellbook", bp => {
                bp.Name = Helpers.CreateString($"{name}Spellbook.Name", uiName);
                bp.m_SpellsPerDay = InquisitorSpellbook.m_SpellsPerDay;
                bp.m_SpellsKnown = InquisitorSpellbook.m_SpellsKnown;
                bp.m_SpellList = InquisitorSpellbook.m_SpellList;
                bp.m_CharacterClass = InquisitorClass.ToReference<BlueprintCharacterClassReference>();
                bp.CastingAttribute = StatType.Charisma;
                bp.Spontaneous = true;
                bp.AllSpellsKnown = false;
                bp.CantripsType = CantripsType.Orisions;
                bp.CasterLevelModifier = 0;
                bp.IsArcane = false;
                
            });

            var swornOfTheEldestArchetype = Helpers.CreateBlueprint<BlueprintArchetype>($"{name}Archetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"{name}Archetype.Name", uiName);
                bp.LocalizedDescription = Helpers.CreateString($"{name}Archetype.Description", "Although the Eldest rarely have " +
                    "adherents as other deities do, they still occasionally require mortal agents to advance their aims. Sworn of the Eldest " +
                    "consider their chosen Eldest to be akin to powerful patrons or fey royalty and serve them faithfully.");
                bp.LocalizedDescriptionShort = bp.LocalizedDescription;
                bp.m_ReplaceSpellbook = swornOfTheEldestSpellbook.ToReference<BlueprintSpellbookReference>();
                bp.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, swornOfTheEldestFeytongue),
                    Helpers.LevelEntry(2, swornOfTheEldestCunningInitiative),
                    Helpers.LevelEntry(3, swornOfTheEldestFeywatcher, magics[0]),
                    Helpers.LevelEntry(6, magics[1]),
                    Helpers.LevelEntry(9, magics[2]),
                    Helpers.LevelEntry(12, magics[3]),
                    Helpers.LevelEntry(15, magics[4]),
                    Helpers.LevelEntry(18, magics[5])
                };
                bp.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, sternGaze),
                    Helpers.LevelEntry(2, cunningInitiative),
                    Helpers.LevelEntry(3, soloTactics, teamworkFeat),
                    Helpers.LevelEntry(6, teamworkFeat),
                    Helpers.LevelEntry(9, teamworkFeat),
                    Helpers.LevelEntry(12, teamworkFeat),
                    Helpers.LevelEntry(15, teamworkFeat),
                    Helpers.LevelEntry(18, teamworkFeat)
                };
            });
            InquisitorClass.Progression.UIGroups = InquisitorClass.Progression.UIGroups.AppendToArray(
                Helpers.CreateUIGroup(magics));

            if (ModSettings.AddedContent.Archetypes.IsDisabled(uiName)) { return; }

            // So this is apparently supposed to be compatible with Sanctified Slayer 
            // which removes True Judgement, and switching Wisdom to Charisma everywhere
            // doesn't count for changing True Judgement. Which is why we instead change True Judgement
            // to use stat depending on presense of this archetype. Also for ability to take alteranate capstone.
            if (true) {
                trueJudgmentFeature.RemoveComponents<ReplaceAbilitiesStat>();
                trueJudgmentFeature.AddComponent<ReplaceAbilitiesStatFromArchetype>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] {
                        Resources.GetBlueprintReference<BlueprintAbilityReference>("d69715dc0de8f8b44ac9f20188c7c22e")
                    };
                    c.NormalStat = StatType.Wisdom;
                    c.Archetype = swornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>();
                    c.ArchetypeStat = StatType.Charisma;
                });
            }

            InquisitorClass.m_Archetypes = InquisitorClass.m_Archetypes.AppendToArray(swornOfTheEldestArchetype.ToReference<BlueprintArchetypeReference>());
        }
    }
}
