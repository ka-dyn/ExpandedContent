using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpandedContent.Utilities;
using ExpandedContent.Extensions;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.ElementsSystem;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.RuleSystem;
using Kingmaker.Utility;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.ResourceLinks;
using Kingmaker.Designers.Mechanics.Buffs;
using ExpandedContent.Config;
using Kingmaker.UnitLogic.Parts;

namespace ExpandedContent.Tweaks.Domains {
    internal class AgathionDomain {

        public static void AddAgathionDomain() {

            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var GoodDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("017afe6934e10c3489176e759a5f01b0");
            var GoodDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("572aade8276366e40b38752be2c55883");
            var ergfe = Resources.GetBlueprint<BlueprintAbility>("808ab74c12df8784ab4eeaf6a107dbea");
            var MadnessDomainGreaterArea = Resources.GetBlueprint<BlueprintAbilityAreaEffect>("19ee79b1da25ea049ba4fea92c2a4025");

            var ProtectionFromEvilBuffSpellImmune = Resources.GetBlueprint<BlueprintBuff>("4a6911969911ce9499bf27dde9bfcedc").GetComponent<AddSpellImmunity>();
            var DominatePersonBuff = Resources.GetBlueprintReference<BlueprintBuffReference>("c0f4e1c24c9cd334ca988ed1bd9d201f");
            var ProtectiveAuraIcon = AssetLoader.LoadInternal("Skills", "Icon_ProtectiveAura.png");


            //AgathionDomainGreaterAreaBuff
            var AgathionDomainGreaterAreaBuff = Helpers.CreateBuff("AgathionDomainGreaterAreaBuff", bp => {
                bp.SetName("Protective Aura");
                bp.SetDescription("While in a Protective Aura allies gain a +2 resistance bonus to all saves and a +2 deflection bonus to AC. \nAdditionally, " +
                    "the aura protects from attempts to dominate or charm the subject made by evil creatures.");
                bp.m_Icon = ProtectiveAuraIcon;
                bp.AddComponent<AddStatBonusAbilityValue>(c => {
                    c.Descriptor = ModifierDescriptor.Deflection;
                    c.Stat = StatType.AC;
                    c.Value = 2;
                });
                bp.AddComponent<AddStatBonusAbilityValue>(c => {
                    c.Descriptor = ModifierDescriptor.Resistance;
                    c.Stat = StatType.SaveFortitude;
                    c.Value = 2;
                });
                bp.AddComponent<AddStatBonusAbilityValue>(c => {
                    c.Descriptor = ModifierDescriptor.Resistance;
                    c.Stat = StatType.SaveReflex;
                    c.Value = 2;
                });
                bp.AddComponent<AddStatBonusAbilityValue>(c => {
                    c.Descriptor = ModifierDescriptor.Resistance;
                    c.Stat = StatType.SaveWill;
                    c.Value = 2;
                });
                bp.AddComponent<AddSpellImmunity>(c => {
                    c.Alignment = AlignmentComponent.Evil;
                    c.Type = SpellImmunityType.Specific;
                    c.m_Exceptions = ProtectionFromEvilBuffSpellImmune.m_Exceptions;
                    c.SpellDescriptor = SpellDescriptor.None;
                    c.InvertedDescriptors = false;
                });
                bp.AddComponent<SpecificBuffImmunity>(c => {
                    c.Alignment = AlignmentComponent.Evil;
                    c.m_Buff = DominatePersonBuff;
                });
            });
            //AgathionDomainGreaterArea
            var AgathionDomainGreaterArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("AgathionDomainGreaterArea", bp => {
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Ally;
                bp.SpellResistance = false;
                bp.AggroEnemies = true;
                bp.AffectEnemies = false;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = 30.Feet();
                bp.Fx = MadnessDomainGreaterArea.Fx;
                bp.AddComponent<AbilityAreaEffectBuff>(c => {
                    c.Condition = new ConditionsChecker() {
                        Operation = Operation.And,
                        Conditions = new Condition[] {
                            new ContextConditionIsAlly()
                        }
                    };
                    c.m_Buff = AgathionDomainGreaterAreaBuff.ToReference<BlueprintBuffReference>();
                });

            });
            //AgathionDomainGreaterBuff
            var AgathionDomainGreaterBuff = Helpers.CreateBuff("AgathionDomainGreaterBuff", bp => {
                bp.SetName("Protective Aura");
                bp.SetDescription("At 8th level, you can emit a 30-foot protective aura as a standard action. Allies in this aura receive a +2 deflection " +
                    "bonus to AC and a +2 resistance bonus on all saving throws. Additionally, the aura protects from attempts to dominate or charm the subject made by evil " +
                    "creatures. You can use this ability for a number of rounds per day equal to your cleric level. These rounds do not need to be consecutive.");
                bp.m_Icon = ProtectiveAuraIcon;
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = AgathionDomainGreaterArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.m_AllowNonContextActions = false;

            });
            //AgathionDomainGreaterResource
            var AgathionDomainGreaterResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("AgathionDomainGreaterResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = true,
                    m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[] {
                        DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    LevelIncrease = 1,
                    StartingLevel = 8,
                    StartingIncrease = 1,
                };
            });
            //AgathionDomainGreaterAbility
            var AgathionDomainGreaterAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("AgathionDomainGreaterAbility", bp => {
                bp.SetName("Protective Aura");
                bp.SetDescription("At 8th level, you can emit a 30-foot protective aura as a standard action. Allies in this aura receive a +2 deflection " +
                    "bonus to AC and a +2 resistance bonus on all saving throws. Additionally, the aura protects from attempts to dominate or charm the subject made by evil " +
                    "creatures. You can use this ability for a number of rounds per day equal to your cleric level. These rounds do not need to be consecutive.");
                bp.m_Icon = ProtectiveAuraIcon;
                bp.m_Buff = AgathionDomainGreaterBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.NewRound;
                    c.m_RequiredResource = AgathionDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
            });
            //AgathionDomainGreaterFeature
            var AgathionDomainGreaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("AgathionDomainGreaterFeature", bp => {
                bp.SetName("Protective Aura");
                bp.SetDescription("At 8th level, you can emit a 30-foot protective aura as a standard action. Allies in this aura receive a +2 deflection " +
                    "bonus to AC and a +2 resistance bonus on all saving throws. Additionally, the aura protects from attempts to dominate or charm the subject made by evil " +
                    "creatures. You can use this ability for a number of rounds per day equal to your cleric level. These rounds do not need to be consecutive.");
                bp.m_Icon = ProtectiveAuraIcon;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = AgathionDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { AgathionDomainGreaterAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            //Spelllist
            var ShieldOfFaithSpell = Resources.GetBlueprint<BlueprintAbility>("183d5bb91dea3a1489a6db6c9cb64445");
            var ProtectionFromEvilCommunalSpell = Resources.GetBlueprint<BlueprintAbility>("93f391b0c5a99e04e83bbfbe3bb6db64");
            var PrayerSpell = Resources.GetBlueprint<BlueprintAbility>("faabd2cc67efa4646ac58c7bb3e40fcc");
            var ForcedRpentanceSpell = Resources.GetBlueprint<BlueprintAbility>("cc0aeb74b35cb7147bff6c53538bbc76");
            var BurstOfGlorySpell = Resources.GetBlueprint<BlueprintAbility>("1bc83efec9f8c4b42a46162d72cbf494");
            var SummonMonsterVIBaseSpell = Resources.GetBlueprint<BlueprintAbility>("e740afbab0147944dab35d83faa0ae1c");
            var HolyWordSpell = Resources.GetBlueprint<BlueprintAbility>("4737294a66c91b844842caee8cf505c8");
            var HolyAuraSpell = Resources.GetBlueprint<BlueprintAbility>("808ab74c12df8784ab4eeaf6a107dbea");
            var SummonMonsterIXBaseSpell = Resources.GetBlueprint<BlueprintAbility>("52b5df2a97df18242aec67610616ded0");
            var AgathionDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("AgathionDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ShieldOfFaithSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ProtectionFromEvilCommunalSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            PrayerSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ForcedRpentanceSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BurstOfGlorySpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SummonMonsterVIBaseSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HolyWordSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HolyAuraSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SummonMonsterIXBaseSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var AgathionDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("AgathionDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = AgathionDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var AgathionDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("AgathionDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { GoodDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = GoodDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { GoodDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = AgathionDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Agathion Subdomain");
                bp.SetDescription("\nYou follow the Agathions path of righteousness.\n{g|Encyclopedia:TouchAttack}Touch{/g} of Good: You can " +
                    "touch a creature as a {g|Encyclopedia:Standard_Actions}standard action{/g}, granting a sacred {g|Encyclopedia:Bonus}bonus{/g} on " +
                    "{g|Encyclopedia:Attack}attack rolls{/g}, {g|Encyclopedia:Skills}skill checks{/g}, {g|Encyclopedia:Ability_Scores}ability checks{/g}, " +
                    "and {g|Encyclopedia:Saving_Throw}saving throws{/g} equal to half your level in the class that gave you access to this domain (minimum 1) " +
                    "for 1 {g|Encyclopedia:Combat_Round}round{/g}. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} " +
                    "modifier.\nProtective Aura: At 8th level, you can emit a 30-foot protective aura as a standard action. Allies in this aura receive a +2 deflection " +
                    "bonus to AC and a +2 resistance bonus on all saving throws. Additionally, the aura protects from attempts to dominate or charm the subject made by evil " +
                    "creatures. You can use this ability for a number of rounds per day equal to your cleric level. These rounds do not need to be consecutive.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var AgathionDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("AgathionDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = AgathionDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var AgathionDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("AgathionDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = AgathionDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = AgathionDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = AgathionDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Agathion Subdomain");
                bp.SetDescription("\nYou follow the Agathions path of righteousness.\n{g|Encyclopedia:TouchAttack}Touch{/g} of Good: You can " +
                    "touch a creature as a {g|Encyclopedia:Standard_Actions}standard action{/g}, granting a sacred {g|Encyclopedia:Bonus}bonus{/g} on " +
                    "{g|Encyclopedia:Attack}attack rolls{/g}, {g|Encyclopedia:Skills}skill checks{/g}, {g|Encyclopedia:Ability_Scores}ability checks{/g}, " +
                    "and {g|Encyclopedia:Saving_Throw}saving throws{/g} equal to half your level in the class that gave you access to this domain (minimum 1) " +
                    "for 1 {g|Encyclopedia:Combat_Round}round{/g}. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} " +
                    "modifier.\nProtective Aura: At 8th level, you can emit a 30-foot protective aura as a standard action. Allies in this aura receive a +2 deflection " +
                    "bonus to AC and a +2 resistance bonus on all saving throws. Additionally, the aura protects from attempts to dominate or charm the subject made by evil " +
                    "creatures. You can use this ability for a number of rounds per day equal to your cleric level. These rounds do not need to be consecutive.\nDomain " +
                    "{g|Encyclopedia:Spell}Spells{/g}: divine favor, communal protection from evil, prayer, forced repentance, burst of glory, summon monster VI, holy word, " +
                    "holy aura, summon monster IX.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.Domain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = -2
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };                
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, AgathionDomainBaseFeature),
                    Helpers.LevelEntry(8, AgathionDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(AgathionDomainBaseFeature, AgathionDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var AgathionDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("AgathionDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = AgathionDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = AgathionDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Agathion Subdomain");
                bp.SetDescription("\nYou follow the Agathions path of righteousness.\n{g|Encyclopedia:TouchAttack}Touch{/g} of Good: You can " +
                    "touch a creature as a {g|Encyclopedia:Standard_Actions}standard action{/g}, granting a sacred {g|Encyclopedia:Bonus}bonus{/g} on " +
                    "{g|Encyclopedia:Attack}attack rolls{/g}, {g|Encyclopedia:Skills}skill checks{/g}, {g|Encyclopedia:Ability_Scores}ability checks{/g}, " +
                    "and {g|Encyclopedia:Saving_Throw}saving throws{/g} equal to half your level in the class that gave you access to this domain (minimum 1) " +
                    "for 1 {g|Encyclopedia:Combat_Round}round{/g}. You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} " +
                    "modifier.\nProtective Aura: At 8th level, you can emit a 30-foot protective aura as a standard action. Allies in this aura receive a +2 deflection " +
                    "bonus to AC and a +2 resistance bonus on all saving throws. Additionally, the aura protects from attempts to dominate or charm the subject made by evil " +
                    "creatures. You can use this ability for a number of rounds per day equal to your cleric level. These rounds do not need to be consecutive.\nDomain " +
                    "{g|Encyclopedia:Spell}Spells{/g}: divine favor, communal protection from evil, prayer, forced repentance, burst of glory, summon monster VI, holy word, " +
                    "holy aura, summon monster IX.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.ClericSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, AgathionDomainBaseFeature),
                    Helpers.LevelEntry(8, AgathionDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(AgathionDomainBaseFeature, AgathionDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            
            AgathionDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() { 
                AgathionDomainProgression.ToReference<BlueprintFeatureReference>(),
                AgathionDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            AgathionDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = AgathionDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            }); 
            AgathionDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = AgathionDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(AgathionDomainGreaterAbility.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Agathion Subdomain")) { return; }
            DomainTools.RegisterDomain(AgathionDomainProgression);
            DomainTools.RegisterSecondaryDomain(AgathionDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(AgathionDomainProgression);
            DomainTools.RegisterTempleDomain(AgathionDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(AgathionDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(AgathionDomainProgression, AgathionDomainProgressionSecondary);
           
        }

    }
}
