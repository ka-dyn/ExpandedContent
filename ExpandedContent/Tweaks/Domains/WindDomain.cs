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
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Utility;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using ExpandedContent.Config;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace ExpandedContent.Tweaks.Domains {
    internal class WindDomain {

        public static void AddWindDomain() {

            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");
            var AirDomainGreaterFeature = Resources.GetBlueprint<BlueprintFeature>("8a8e3f80abc04c34b98324823d14b057");
            var AirDomainCapstone = Resources.GetBlueprint<BlueprintFeature>("d5a54e5a3876f5a498abad99b1984cb5");
            var AirBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("e1ff99dc3aeaa064e8eecde51c1c4773");
            var Kinetic_AirBlastLine00 = Resources.GetBlueprint<BlueprintProjectile>("03689858955c6bf409be06f35f09946a");

            var TorrentAirBlastAbility = Resources.GetBlueprint<BlueprintAbility>("51ede1faa3cdb3b47a46f7579ca02b0a");

            var WindDomainBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("WindDomainBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });
            var WindDomainBaseAbility = Helpers.CreateBlueprint<BlueprintAbility>("WindDomainBaseAbility", bp => {
                bp.SetName("Wind Blast");
                bp.SetDescription("As a standard action, you can unleash a blast of air in a 30-foot line. Make a combat maneuver check against each creature in the line, " +
                    "using your caster level as your base attack bonus and your Wisdom modifier in place of your Strength modifier. Treat the results as a bull rush attempt. " +
                    "You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WindDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        Kinetic_AirBlastLine00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Line;
                    c.m_Length = new Feet() { m_Value = 30 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionCombatManeuver() {
                            Type = CombatManeuver.BullRush,
                            ReplaceStat = true,
                            NewStat = StatType.Wisdom,
                            UseCasterLevelAsBaseAttack = true,
                            OnSuccess = Helpers.CreateActionList() 
                        }
                        );
                });
                bp.m_Icon = TorrentAirBlastAbility.m_Icon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Kineticist;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            //Spelllist
            var EarPiercingScreamSpell = Resources.GetBlueprint<BlueprintAbility>("8e7cfa5f213a90549aadd18f8f6f4664");
            var ProtectionFromArrowsSpell = Resources.GetBlueprint<BlueprintAbility>("c28de1f98a3f432448e52e5d47c73208");
            var LightningBoltSpell = Resources.GetBlueprint<BlueprintAbility>("d2cff9243a7ee804cb6d5be47af30c73");
            var ShoutSpell = Resources.GetBlueprint<BlueprintAbility>("f09453607e683784c8fca646eec49162");
            var CloudkillSpell = Resources.GetBlueprint<BlueprintAbility>("548d339ba87ee56459c98e80167bdf10");
            var SiroccoSpell = Resources.GetBlueprint<BlueprintAbility>("093ed1d67a539ad4c939d9d05cfe192c");
            var ElementalBodyIVAirSpell = Resources.GetBlueprint<BlueprintAbility>("ee63301f83c76694692d4704d8a05bdc");
            var ShoutGreaterSpell = Resources.GetBlueprint<BlueprintAbility>("fd0d3840c48cafb44bb29e8eb74df204");
            var WindsOfVengeanceSpell = Resources.GetBlueprint<BlueprintAbility>("5d8f1da2fdc0b9242af9f326f9e507be");
            var WindDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("WindDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            EarPiercingScreamSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ProtectionFromArrowsSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            LightningBoltSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ShoutSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            CloudkillSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(6) {
                        SpellLevel = 6,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SiroccoSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(7) {
                        SpellLevel = 7,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ElementalBodyIVAirSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            ShoutGreaterSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            WindsOfVengeanceSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var WindDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("WindDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = WindDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var WindDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("WindDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WindDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = WindDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { WindDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = WindDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Wind Subdomain");
                bp.SetDescription("\nYou can manipulate wind, mist, and lightning, and are resistant to {g|Encyclopedia:Energy_Damage}electricity damage{/g}.\nWind Blast: " +
                    "As a standard action, you can unleash a blast of air in a 30-foot line. Make a combat maneuver check to push back each creature in the line, " +
                    "using your caster level as your base attack bonus and your Wisdom modifier in place of your Strength modifier. Treat the results as a bull rush attempt. " +
                    "You can use this ability a number of times per day equal to 3 + your Wisdom modifier.\nElectricity Resistance: At 6th level, you gain resist electricity " +
                    "10. This resistance increases to 20 at 12th level. At 20th level, you gain immunity to electricity.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var WindDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("WindDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = WindDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var WindDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("WindDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = WindDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = WindDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = WindDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Wind Subdomain");
                bp.SetDescription("\nYou can manipulate wind, mist, and lightning, and are resistant to {g|Encyclopedia:Energy_Damage}electricity damage{/g}.\nWind Blast: " +
                    "As a standard action, you can unleash a blast of air in a 30-foot line. Make a combat maneuver check to push back each creature in the line, " +
                    "using your caster level as your base attack bonus and your Wisdom modifier in place of your Strength modifier. Treat the results as a bull rush attempt. " +
                    "You can use this ability a number of times per day equal to 3 + your Wisdom modifier.\nElectricity Resistance: At 6th level, you gain resist electricity " +
                    "10. This resistance increases to 20 at 12th level. At 20th level, you gain immunity to electricity.\nDomain {g|Encyclopedia:Spell}Spells{/g}: ear piercing scream, " +
                    "protection from arrows, lightning bolt, shout, cloudkill, sirocco, elemental body IV (air), greater shout, winds of vengeance.");
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
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
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
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, WindDomainBaseFeature),
                    Helpers.LevelEntry(6, AirDomainGreaterFeature),
                    Helpers.LevelEntry(12, AirDomainGreaterFeature),
                    Helpers.LevelEntry(20, AirDomainCapstone)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(WindDomainBaseFeature, AirDomainGreaterFeature, AirDomainGreaterFeature, AirDomainCapstone)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var WindDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("WindDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = WindDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = WindDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Wind Subdomain");
                bp.SetDescription("\nYou can manipulate wind, mist, and lightning, and are resistant to {g|Encyclopedia:Energy_Damage}electricity damage{/g}.\nWind Blast: " +
                    "As a standard action, you can unleash a blast of air in a 30-foot line. Make a combat maneuver check to push back each creature in the line, " +
                    "using your caster level as your base attack bonus and your Wisdom modifier in place of your Strength modifier. Treat the results as a bull rush attempt. " +
                    "You can use this ability a number of times per day equal to 3 + your Wisdom modifier.\nElectricity Resistance: At 6th level, you gain resist electricity " +
                    "10. This resistance increases to 20 at 12th level. At 20th level, you gain immunity to electricity.\nDomain {g|Encyclopedia:Spell}Spells{/g}: ear piercing scream, " +
                    "protection from arrows, lightning bolt, shout, cloudkill, sirocco, elemental body IV (air), greater shout, winds of vengeance.");
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
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, WindDomainBaseFeature),
                    Helpers.LevelEntry(6, AirDomainGreaterFeature),
                    Helpers.LevelEntry(12, AirDomainGreaterFeature),
                    Helpers.LevelEntry(20, AirDomainCapstone)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(WindDomainBaseFeature, AirDomainGreaterFeature, AirDomainGreaterFeature, AirDomainCapstone)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // WindDomainSpellListFeatureDruid
            var WindDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("WindDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = WindDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            // WindDomainProgressionDruid
            var WindDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("WindDomainProgressionDruid", bp => {
                bp.SetName("Wind Subdomain");
                bp.SetDescription("\nYou can manipulate wind, mist, and lightning, and are resistant to {g|Encyclopedia:Energy_Damage}electricity damage{/g}.\nWind Blast: " +
                    "As a standard action, you can unleash a blast of air in a 30-foot line. Make a combat maneuver check to push back each creature in the line, " +
                    "using your caster level as your base attack bonus and your Wisdom modifier in place of your Strength modifier. Treat the results as a bull rush attempt. " +
                    "You can use this ability a number of times per day equal to 3 + your Wisdom modifier.\nElectricity Resistance: At 6th level, you gain resist electricity " +
                    "10. This resistance increases to 20 at 12th level. At 20th level, you gain immunity to electricity.\nDomain {g|Encyclopedia:Spell}Spells{/g}: ear piercing scream, " +
                    "protection from arrows, lightning bolt, shout, cloudkill, sirocco, elemental body IV (air), greater shout, winds of vengeance.");
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.DruidDomain, FeatureGroup.BlightDruidDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = DruidClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, WindDomainBaseFeature,WindDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(6, AirDomainGreaterFeature),
                    Helpers.LevelEntry(12, AirDomainGreaterFeature),
                    Helpers.LevelEntry(20, AirDomainCapstone)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(WindDomainBaseFeature, AirDomainGreaterFeature, AirDomainGreaterFeature, AirDomainCapstone)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            //Separatist versions
            var ProtectionDomainGreaterFeatureSeparatist = Resources.GetBlueprint<BlueprintFeature>("7eb39ba8115a422bb69c702cc20ca58a");
            var SeparatistAsIsProperty = Resources.GetModBlueprint<BlueprintUnitProperty>("SeparatistAsIsProperty");


            var WindDomainAllowedSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("WindDomainAllowedSeparatist", bp => {
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });



            var WindDomainBaseResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("WindDomainBaseResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 2,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
                bp.IsDomainAbility = true;
            });
            var WindDomainBaseAbilitySeparatist = Helpers.CreateBlueprint<BlueprintAbility>("WindDomainBaseAbilitySeparatist", bp => {
                bp.SetName("Wind Blast");
                bp.SetDescription("As a standard action, you can unleash a blast of air in a 30-foot line. Make a combat maneuver check against each creature in the line, " +
                    "using your caster level as your base attack bonus and your Wisdom modifier in place of your Strength modifier. Treat the results as a bull rush attempt. " +
                    "You can use this ability a number of times per day equal to 3 + your Wisdom modifier.");
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = WindDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        Kinetic_AirBlastLine00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Line;
                    c.m_Length = new Feet() { m_Value = 30 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionCombatManeuver() {
                            Type = CombatManeuver.BullRush,
                            ReplaceStat = true,
                            NewStat = StatType.Wisdom,
                            UseCasterLevelAsBaseAttack = true,
                            OnSuccess = Helpers.CreateActionList()
                        }
                        );
                });
                bp.m_Icon = TorrentAirBlastAbility.m_Icon;
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Kineticist;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.IsDomainAbility = true;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var WindDomainBaseFeatureSeparatist = Helpers.CreateBlueprint<BlueprintFeature>("WindDomainBaseFeatureSeparatist", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WindDomainBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = WindDomainBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { WindDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = WindDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Wind Subdomain");
                bp.SetDescription("\nYou can manipulate wind, mist, and lightning, and are resistant to {g|Encyclopedia:Energy_Damage}electricity damage{/g}.\nWind Blast: " +
                    "As a standard action, you can unleash a blast of air in a 30-foot line. Make a combat maneuver check to push back each creature in the line, " +
                    "using your caster level as your base attack bonus and your Wisdom modifier in place of your Strength modifier. Treat the results as a bull rush attempt. " +
                    "You can use this ability a number of times per day equal to 3 + your Wisdom modifier.\nElectricity Resistance: At 6th level, you gain resist electricity " +
                    "10. This resistance increases to 20 at 12th level. At 20th level, you gain immunity to electricity.");
                bp.IsClassFeature = true;
            });

            var WindDomainProgressionSeparatist = Helpers.CreateBlueprint<BlueprintProgression>("WindDomainProgressionSeparatist", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = WindDomainAllowedSeparatist.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = WindDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = WindDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = WindDomainProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = WindDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Wind Subdomain");
                bp.SetDescription("\nYou can manipulate wind, mist, and lightning, and are resistant to {g|Encyclopedia:Energy_Damage}electricity damage{/g}.\nWind Blast: " +
                    "As a standard action, you can unleash a blast of air in a 30-foot line. Make a combat maneuver check to push back each creature in the line, " +
                    "using your caster level as your base attack bonus and your Wisdom modifier in place of your Strength modifier. Treat the results as a bull rush attempt. " +
                    "You can use this ability a number of times per day equal to 3 + your Wisdom modifier.\nElectricity Resistance: At 6th level, you gain resist electricity " +
                    "10. This resistance increases to 20 at 12th level. At 20th level, you gain immunity to electricity.\nDomain {g|Encyclopedia:Spell}Spells{/g}: ear piercing scream, " +
                    "protection from arrows, lightning bolt, shout, cloudkill, sirocco, elemental body IV (air), greater shout, winds of vengeance.");
                bp.Groups = new FeatureGroup[] { FeatureGroup.SeparatistSecondaryDomain };
                bp.IsClassFeature = true;
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, WindDomainBaseFeatureSeparatist),
                    Helpers.LevelEntry(8, AirDomainGreaterFeature),
                    Helpers.LevelEntry(14, AirDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(WindDomainBaseFeatureSeparatist, AirDomainGreaterFeature, AirDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });

            WindDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() {
                WindDomainProgression.ToReference<BlueprintFeatureReference>(),
                WindDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            WindDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = WindDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            WindDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = WindDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            WindDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = WindDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            WindDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = WindDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            WindDomainProgressionSeparatist.AddComponent<PrerequisiteNoFeature>(c => {
                c.Group = Prerequisite.GroupType.All;
                c.HideInUI = true;
                c.m_Feature = WindDomainProgressionSeparatist.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(WindDomainBaseAbility.ToReference<BlueprintAbilityReference>());
            DomainMastery.Abilities.Add(WindDomainBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Wind Subdomain")) { return; }
            DomainTools.RegisterDomain(WindDomainProgression);
            DomainTools.RegisterSecondaryDomain(WindDomainProgressionSecondary);
            DomainTools.RegisterDruidDomain(WindDomainProgressionDruid);
            DomainTools.RegisterBlightDruidDomain(WindDomainProgressionDruid);
            DomainTools.RegisterDivineHunterDomain(WindDomainProgression);
            DomainTools.RegisterTempleDomain(WindDomainProgression);
            DomainTools.RegisterSecondaryTempleDomain(WindDomainProgressionSecondary);
            DomainTools.RegisterImpossibleSubdomain(WindDomainProgression, WindDomainProgressionSecondary);
            DomainTools.RegisterSeparatistDomain(WindDomainProgressionSeparatist);

        }
    }
}
