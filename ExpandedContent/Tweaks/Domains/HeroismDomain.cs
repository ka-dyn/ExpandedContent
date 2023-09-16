using System.Collections.Generic;
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
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.Domains {
    internal class HeroismDomain {

        public static void AddHeroismDomain() {

            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var GloryDomainGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("bf41d1d2cf72e8545b51857f20fa58e7");
            var ChannelPositiveHarm = Resources.GetBlueprint<BlueprintAbility>("279447a6bf2d3544d93a0a39c3b8e91d");
            var TouchofGloryIcon = AssetLoader.LoadInternal("Skills", "Icon_TouchofGlory.jpg");


            var HeroismDomainBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("HeroismDomainBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
            });

            var HeroismBuff = Resources.GetBlueprint<BlueprintBuff>("87ab2fed7feaaff47b62a3320a57ad8d");

            var HeroismDomainBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("HeroismDomainBaseAbility", bp => {
                bp.SetName("Touch of Glory");
                bp.SetDescription("As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can {g|Encyclopedia:TouchAttack}touch{/g} an ally to grant him heroism as if the heroism spell " +
                    "had been cast on him for a number of rounds equal to half the level of the class that gave you access to this domain (min 1). You can use this ability a number of times per " +
                    "day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.");
                bp.m_Icon = TouchofGloryIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = HeroismBuff.ToReference<BlueprintBuffReference>(),
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default
                                },
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { TempleChampionArchetype.ToReference<BlueprintArchetypeReference>() };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = HeroismDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Touch;
                bp.CanTargetFriends = true;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Touch;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic |= Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Reach;
                bp.LocalizedDuration = Helpers.CreateString("HeroismDomainBaseAbility.Duration", "1 round/2 levels");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            //Spelllist
            var ShieldOfFaithSpell = Resources.GetBlueprint<BlueprintAbility>("183d5bb91dea3a1489a6db6c9cb64445");
            var BlessWeaponCastSpell = Resources.GetBlueprint<BlueprintAbility>("de954d4cbf995864ba8662e67a473f93");
            var HeroismSpell = Resources.GetBlueprint<BlueprintAbility>("5ab0d42fb68c9e34abae4921822b9d63");
            var DivinePowerSpell = Resources.GetBlueprint<BlueprintAbility>("ef16771cb05d1344989519e87f25b3c5");
            var BurstOfGlirySpell = Resources.GetBlueprint<BlueprintAbility>("1bc83efec9f8c4b42a46162d72cbf494");
            var GreaterHeroismSpell = Resources.GetBlueprint<BlueprintAbility>("e15e5e7045fda2244b98c8f010adfe31");
            var HolySwordSpell = Resources.GetBlueprint<BlueprintAbility>("bea9deffd3ab6734c9534153ddc70bde");
            var HolyAuraSpell = Resources.GetBlueprint<BlueprintAbility>("808ab74c12df8784ab4eeaf6a107dbea");
            var OverwhelmingPresenceSpell = Resources.GetBlueprint<BlueprintAbility>("41cf93453b027b94886901dbfc680cb9");
            var HeroismDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("HeroismDomainSpellList", bp => {
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
                            BlessWeaponCastSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HeroismSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            DivinePowerSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            BurstOfGlirySpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            GreaterHeroismSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            HolySwordSpell.ToReference<BlueprintAbilityReference>()
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
                            OverwhelmingPresenceSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var HeroismDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("HeroismDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = HeroismDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });            
            var HeroismDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("HeroismDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { HeroismDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = HeroismDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { HeroismDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = HeroismDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<IncreaseSpellDC>(c => {
                    c.m_Spell = ChannelPositiveHarm.ToReference<BlueprintAbilityReference>();
                    c.HalfMythicRank = false;
                    c.UseContextBonus = false;
                    c.Value = 0;
                    c.BonusDC = 2;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Heroism Subdomain");
                bp.SetDescription("\nYou are a true example of the heroic nature of your deity. In addition, when you channel positive energy to harm undead creatures, the " +
                    "{g|Encyclopedia:Saving_Throw}save{/g} {g|Encyclopedia:DC}DC{/g} to halve the {g|Encyclopedia:Damage}damage{/g} is increased by 2.\n{g|Encyclopedia:TouchAttack}Touch{/g} " +
                    "of Glory: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can {g|Encyclopedia:TouchAttack}touch{/g} an ally to grant him heroism as if the heroism spell " +
                    "had been cast on him for a number of rounds equal to half the level of the class that gave you access to this domain (min 1). You " +
                    "can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nAura of Heroism: At 8th level, you can emit " +
                    "a 30-foot aura of heroism for a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class that gave you access to this " +
                    "domain. Using this ability is a {g|Encyclopedia:Swift_Action}swift action{/g}. Allies in the area are treated as if they were under the effects of heroism " +
                    "{g|Encyclopedia:Spell}spell{/g}.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var HeroismDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("HeroismDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = HeroismDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var HeroismDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("HeroismDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = HeroismDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = HeroismDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = HeroismDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Heroism Subdomain");
                bp.SetDescription("\nYou are a true example of the heroic nature of your deity. In addition, when you channel positive energy to harm undead creatures, the " +
                    "{g|Encyclopedia:Saving_Throw}save{/g} {g|Encyclopedia:DC}DC{/g} to halve the {g|Encyclopedia:Damage}damage{/g} is increased by 2.\n{g|Encyclopedia:TouchAttack}Touch{/g} " +
                    "of Glory: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can {g|Encyclopedia:TouchAttack}touch{/g} an ally to grant him heroism as if the heroism spell " +
                    "had been cast on him for a number of rounds equal to half the level of the class that gave you access to this domain (min 1). You " +
                    "can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nAura of Heroism: At 8th level, you can emit " +
                    "a 30-foot aura of heroism for a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class that gave you access to this " +
                    "domain. Using this ability is a {g|Encyclopedia:Swift_Action}swift action{/g}. Allies in the area are treated as if they were under the effects of heroism " +
                    "{g|Encyclopedia:Spell}spell{/g}.\nDomain Spells: shield of faith, bless weapon, heroism, divine power, burst of glory, greater heroism, " +
                    "holy sword, holy aura, overwhelming presence.");
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
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
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
                    Helpers.LevelEntry(1, HeroismDomainBaseFeature),
                    Helpers.LevelEntry(8, GloryDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(HeroismDomainBaseFeature, GloryDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var HeroismDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("HeroismDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = HeroismDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = HeroismDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Heroism Subdomain");
                bp.SetDescription("\nYou are a true example of the heroic nature of your deity. In addition, when you channel positive energy to harm undead creatures, the " +
                    "{g|Encyclopedia:Saving_Throw}save{/g} {g|Encyclopedia:DC}DC{/g} to halve the {g|Encyclopedia:Damage}damage{/g} is increased by 2.\n{g|Encyclopedia:TouchAttack}Touch{/g} " +
                    "of Glory: As a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can {g|Encyclopedia:TouchAttack}touch{/g} an ally to grant him heroism as if the heroism spell " +
                    "had been cast on him for a number of rounds equal to half the level of the class that gave you access to this domain (min 1). You " +
                    "can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier.\nAura of Heroism: At 8th level, you can emit " +
                    "a 30-foot aura of heroism for a number of {g|Encyclopedia:Combat_Round}rounds{/g} per day equal to your level in the class that gave you access to this " +
                    "domain. Using this ability is a {g|Encyclopedia:Swift_Action}swift action{/g}. Allies in the area are treated as if they were under the effects of heroism " +
                    "{g|Encyclopedia:Spell}spell{/g}.\nDomain Spells: shield of faith, bless weapon, heroism, divine power, burst of glory, greater heroism, " +
                    "holy sword, holy aura, overwhelming presence.");
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
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = StargazerClass.ToReference<BlueprintCharacterClassReference>(),
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
                    Helpers.LevelEntry(1, HeroismDomainBaseFeature),
                    Helpers.LevelEntry(8, GloryDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(HeroismDomainBaseFeature, GloryDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });            
            HeroismDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() { 
                HeroismDomainProgression.ToReference<BlueprintFeatureReference>(),
                HeroismDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            HeroismDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = HeroismDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            }); 
            HeroismDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = HeroismDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(HeroismDomainBaseAbility.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Heroism Subdomain")) { return; }
            DomainTools.RegisterDomain(HeroismDomainProgression);
            DomainTools.RegisterSecondaryDomain(HeroismDomainProgressionSecondary);
            DomainTools.RegisterDivineHunterDomain(HeroismDomainProgression);
            DomainTools.RegisterTempleDomain(HeroismDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(HeroismDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(HeroismDomainProgression, HeroismDomainProgressionSecondary);
        }
    }
}
