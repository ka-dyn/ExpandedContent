using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Curses {
    internal class Vampirism {

        public static void AddVampirismCurse() {

            var OracleCurseSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("b0a5118b4fb793241bc7042464b23fab");
            var BeneficialCurse = Resources.GetBlueprint<BlueprintFeatureSelection>("2dda67424ee8e0b4d83ef01a73ca6bff");
            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var WitchClass = Resources.GetBlueprint<BlueprintCharacterClass>("1b9873f1e7bfe5449bc84d03e9c8e3cc");
            var AccursedWitchArchetype = Resources.GetBlueprint<BlueprintArchetype>("c5f6e53e71059fb4d802ce81a277a12d");
            var NegativeEnergyAffinity = Resources.GetBlueprint<BlueprintFeature>("d5ee498e19722854198439629c1841a5");
            var ChannelResistance4 = Resources.GetBlueprint<BlueprintFeature>("a9ac84c6f48b491438f91bb237bc9212");
            var VampiricTouchSpell = Resources.GetBlueprint<BlueprintAbility>("6cbb040023868574b992677885390f92");
            var CreateUndeadSpell = Resources.GetBlueprint<BlueprintAbility>("76a11b460be25e44ca85904d6806e5a3");
            var VampirismIcon = AssetLoader.LoadInternal("Skills", "Icon_VampireCurse.png");

            var VampirismCurseFeatureLevel10 = Helpers.CreateBlueprint<BlueprintFeature>("VampirismCurseFeatureLevel10", bp => {
                bp.SetName("Vampirism");
                bp.SetDescription("");
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = VampiricTouchSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = VampiricTouchSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 3;
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = CreateUndeadSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_Spell = CreateUndeadSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                    c.m_CharacterClass = WitchClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var VampirismCurseFeatureLevel15 = Helpers.CreateBlueprint<BlueprintFeature>("VampirismCurseFeatureLevel15", bp => {
                bp.SetName("Vampirism");
                bp.SetDescription("");
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.BypassedByMagic = true;
                    c.Value = 5;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var VampirismCurseProgression = Helpers.CreateBlueprint<BlueprintProgression>("VampirismCurseProgression", bp => {
                bp.SetName("Vampirism");
                bp.SetDescription("You crave the taste of fresh, warm blood. \nYou take damage from positive energy and heal from negative energy as if you were " +
                    "undead. \nAt 5th level, you gain channel resistance +4. \nAt 10th level, you add vampiric touch to your list of 3rd-level oracle spells known " +
                    "and create undead to your list of 5th-level oracle spells known. \nAt 15th level, you gain damage reduction 5/magic.");
                bp.m_Icon = VampirismIcon;
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleCurse };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = WitchClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, NegativeEnergyAffinity),
                    Helpers.LevelEntry(5, ChannelResistance4),
                    Helpers.LevelEntry(10, VampirismCurseFeatureLevel10),
                    Helpers.LevelEntry(15, VampirismCurseFeatureLevel15)
                };                
                bp.GiveFeaturesForPreviousLevels = true;            
            });
            VampirismCurseProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.CheckInProgression = true;
                c.m_Feature = VampirismCurseProgression.ToReference<BlueprintFeatureReference>();
                c.HideInUI = true;
            });
            var BeneficialVampirismCurseProgression = Helpers.CreateBlueprint<BlueprintProgression>("BeneficialVampirismCurseProgression", bp => {
                bp.SetName("Beneficial Vampirism");
                bp.SetDescription("You crave the taste of fresh, warm blood. \nYou take damage from positive energy and heal from negative energy as if you were " +
                    "undead. \nAt 5th level, you gain channel resistance +4. \nAt 10th level, you add vampiric touch to your list of 3rd-level oracle spells known " +
                    "and create undead to your list of 5th-level oracle spells known. \nAt 15th level, you gain damage reduction 5/magic.\n(The Negative Energy " +
                    "Affinity granted at level 1 is not considered a penalty and therefore is not removed.)");
                bp.m_Icon = VampirismIcon;
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = WitchClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = AccursedWitchArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.CheckInProgression = true;
                    c.m_Feature = VampirismCurseProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, NegativeEnergyAffinity),
                    Helpers.LevelEntry(5, ChannelResistance4),
                    Helpers.LevelEntry(10, VampirismCurseFeatureLevel10),
                    Helpers.LevelEntry(15, VampirismCurseFeatureLevel15)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            OracleCurseSelection.m_AllFeatures = OracleCurseSelection.m_AllFeatures.AppendToArray(VampirismCurseProgression.ToReference<BlueprintFeatureReference>());
            BeneficialCurse.m_AllFeatures = BeneficialCurse.m_AllFeatures.AppendToArray(BeneficialVampirismCurseProgression.ToReference<BlueprintFeatureReference>());
            BeneficialCurse.m_Features = BeneficialCurse.m_Features.AppendToArray(BeneficialVampirismCurseProgression.ToReference<BlueprintFeatureReference>());
        }
    }
}
