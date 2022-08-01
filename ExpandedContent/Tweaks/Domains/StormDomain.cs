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
using Kingmaker.ResourceLinks;
using ExpandedContent.Config;

namespace ExpandedContent.Tweaks.Domains {
    internal class StormDomain {

        public static void AddStormDomain() {

            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var DruidClass = Resources.GetBlueprint<BlueprintCharacterClass>("610d836f3a3a9ed42a4349b62f002e96");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var WeatherDomainBaseAbility = Resources.GetBlueprint<BlueprintAbility>("f166325c271dd29449ba9f98d11542d9");
            var WeatherDomainBaseResource = Resources.GetBlueprint<BlueprintAbilityResource>("53dd76c7053469541b99e01cb25711d6");
            var GaleAura = AssetLoader.LoadInternal("Skills", "Icon_GaleAura.jpg");
            var WeatherBlessingFeature = Resources.GetBlueprint<BlueprintFeature>("4172d92c598de1d47aa2c0dd51c05e24");
            //StormDomainDifficultTerrainBuff
            var StormDomainDifficultTerrainBuff = Helpers.CreateBuff("StormDomainDifficultTerrainBuff", bp => {
                bp.SetName("Gale Aura");
                bp.SetDescription("Unable to take 5-foot steps and is affected by difficult terrain");
                bp.m_Icon = GaleAura;
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = UnitCondition.DifficultTerrain;
                });
            });
            //StormDomainGreaterArea
            var StormDomainGreaterArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("StormDomainGreaterAreabuff", bp => {
                bp.m_TargetType = BlueprintAbilityAreaEffect.TargetType.Enemy;
                bp.SpellResistance = false;
                bp.AggroEnemies = true;
                bp.AffectEnemies = true;                
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = 30.Feet();
                bp.Fx = new PrefabLink();
                bp.AddComponent(AuraUtils.CreateUnconditionalAuraEffect(StormDomainDifficultTerrainBuff.ToReference<BlueprintBuffReference>()));
            });
            //StormDomainGreaterBuff
            var StormDomainGreaterBuff = Helpers.CreateBuff("StormDomainGreaterBuff", bp => {
                bp.SetName("Gale Aura");
                bp.SetDescription("At 6th level, as a standard action, you can create a 30-foot aura of gale-like winds that slows the " +
                    "progress of enemies. Enemies in the aura cannot take a 5-foot step and treat it as as difficult terrain. You can use this ability " +
                    "for a number of rounds per day equal to your cleric level. The rounds do not need to be consecutive.");
                bp.m_Icon = GaleAura;
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = StormDomainGreaterArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
                bp.m_AllowNonContextActions = false;

            });
            //StormDomainGreaterResource
            var StormDomainGreaterResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("StormDomainGreaterResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = true,
                    m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        DruidClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                    },
                    m_Archetypes = new BlueprintArchetypeReference[] {
                        DivineHunterArchetype.ToReference<BlueprintArchetypeReference>(),
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                    },
                    LevelIncrease = 1,
                    StartingLevel = 6,
                    StartingIncrease = 1,
                };
            });
            //StormDomainGreaterAbility
            var StormDomainGreaterAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("StormDomainGreaterAbility", bp => {
                bp.SetName("Gale Aura");
                bp.SetDescription("At 6th level, as a standard action, you can create a 30-foot aura of gale-like winds that slows the " +
                    "progress of enemies. Enemies in the aura cannot take a 5-foot step and treat it as as difficult terrain. You can use this ability " +
                    "for a number of rounds per day equal to your cleric level. The rounds do not need to be consecutive.");
                bp.m_Icon = GaleAura;
                bp.m_Buff = StormDomainGreaterBuff.ToReference<BlueprintBuffReference>();
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.NewRound;
                    c.m_RequiredResource = StormDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.ActivationType = AbilityActivationType.WithUnitCommand;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Standard;
            });
            //StormDomainGreaterFeature
            var StormDomainGreaterFeature = Helpers.CreateBlueprint<BlueprintFeature>("StormDomainGreaterFeature", bp => {
                bp.SetName("Gale Aura");
                bp.SetDescription("At 6th level, as a standard action, you can create a 30-foot aura of gale-like winds that slows the " +
                    "progress of enemies. Enemies in the aura cannot take a 5-foot step and treat it as as difficult terrain. You can use this ability " +
                    "for a number of rounds per day equal to your cleric level. The rounds do not need to be consecutive.");
                bp.m_Icon = GaleAura;
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = StormDomainGreaterResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { StormDomainGreaterAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });

            //Spelllist
            var SnowballSpell = Resources.GetBlueprint<BlueprintAbility>("9f10909f0be1f5141bf1c102041f93d9");
            var SummonElementalSmallBaseSpell = Resources.GetBlueprint<BlueprintAbility>("970c6db48ff0c6f43afc9dbb48780d03");
            var CallLightningSpell = Resources.GetBlueprint<BlueprintAbility>("2a9ef0e0b5822a24d88b16673a267456");
            var SlowMudSpell = Resources.GetBlueprint<BlueprintAbility>("6b30813c3709fc44b92dc8fd8191f345");
            var CallLightningStormSpell = Resources.GetBlueprint<BlueprintAbility>("d5a36a7ee8177be4f848b953d1c53c84");
            var SiroccoSpell = Resources.GetBlueprint<BlueprintAbility>("093ed1d67a539ad4c939d9d05cfe192c");
            var FireStormSpell = Resources.GetBlueprint<BlueprintAbility>("e3d0dfe1c8527934294f241e0ae96a8d");
            var SunburstSpell = Resources.GetBlueprint<BlueprintAbility>("e96424f70ff884947b06f41a765b7658");
            var TsunamiSpell = Resources.GetBlueprint<BlueprintAbility>("d8144161e352ca846a73cf90e85bf9ac");
            var StormDomainSpellList = Helpers.CreateBlueprint<BlueprintSpellList>("StormDomainSpellList", bp => {
                bp.SpellsByLevel = new SpellLevelList[10] {
                    new SpellLevelList(0) {
                        SpellLevel = 0,
                        m_Spells = new List<BlueprintAbilityReference>()
                    },
                    new SpellLevelList(1) {
                        SpellLevel = 1,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SnowballSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(2) {
                        SpellLevel = 2,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SummonElementalSmallBaseSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(3) {
                        SpellLevel = 3,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            CallLightningSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(4) {
                        SpellLevel = 4,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            SlowMudSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(5) {
                        SpellLevel = 5,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            CallLightningStormSpell.ToReference<BlueprintAbilityReference>()
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
                            SunburstSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(8) {
                        SpellLevel = 8,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            FireStormSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                    new SpellLevelList(9) {
                        SpellLevel = 9,
                        m_Spells = new List<BlueprintAbilityReference>() {
                            TsunamiSpell.ToReference<BlueprintAbilityReference>()
                        }
                    },
                };
            });     
            var StormDomainSpellListFeature = Helpers.CreateBlueprint<BlueprintFeature>("StormDomainSpellListFeature", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = StormDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            
            var StormDomainBaseFeature = Helpers.CreateBlueprint<BlueprintFeature>("StormDomainBaseFeature", bp => {
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { WeatherDomainBaseAbility.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AddAbilityResources>(c => { 
                    c.m_Resource = WeatherDomainBaseResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] { WeatherDomainBaseAbility.ToReference<BlueprintAbilityReference>() };
                    c.Stat = StatType.Wisdom;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 1;
                    c.m_Feature = StormDomainSpellListFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Storm Subdomain");
                bp.SetDescription("\nWith power over storm and sky, you can call down the wrath of the gods upon the world below.\nStorm Burst: As " +
                "a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can create a storm burst targeting any foe within 30 feet as a ranged " +
                "{g|Encyclopedia:TouchAttack}touch attack{/g}. The storm burst deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} + " +
                "1 point for every two levels you possess in the class that gave you access to this domain. In addition, the target is buffeted by winds and rain, " +
                "causing it to take a –2 {g|Encyclopedia:Penalty}penalty{/g} on {g|Encyclopedia:Attack}attack rolls{/g} for 1 {g|Encyclopedia:Combat_Round}round{/g}. " +
                "You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier." +
                "\nGale Aura (Su): At 6th level, as a standard action, you can create a 30-foot aura of gale-like winds that slows the progress of enemies. Creatures " +
                "in the aura cannot take a 5-foot step and treat it as as difficult terrain. You can use this ability for a number of rounds per " +
                "day equal to your cleric level. The rounds do not need to be consecutive.");
                bp.IsClassFeature = true;
            });
            //Deity plug
            var StormDomainAllowed = Helpers.CreateBlueprint<BlueprintFeature>("StormDomainAllowed", bp => {
                // This may buff Ecclest when it shouldn't, needs test
                //bp.AddComponent<AddSpecialSpellListForArchetype>(c => {
                //    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                //    c.m_SpellList = StormDomainSpellList.ToReference<BlueprintSpellListReference>();
                //    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                //});
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;                
            });            
            // Main Blueprint
            var StormDomainProgression = Helpers.CreateBlueprint<BlueprintProgression>("StormDomainProgression", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = StormDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = ClericClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = StormDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = EcclesitheurgeArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<LearnSpellList>(c => {
                    c.m_CharacterClass = HunterClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = StormDomainSpellList.ToReference<BlueprintSpellListReference>();
                    c.m_Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.SetName("Storm Subdomain");
                bp.SetDescription("\nWith power over storm and sky, you can call down the wrath of the gods upon the world below.\nStorm Burst: As " +
                "a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can create a storm burst targeting any foe within 30 feet as a ranged " +
                "{g|Encyclopedia:TouchAttack}touch attack{/g}. The storm burst deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} + " +
                "1 point for every two levels you possess in the class that gave you access to this domain. In addition, the target is buffeted by winds and rain, " +
                "causing it to take a –2 {g|Encyclopedia:Penalty}penalty{/g} on {g|Encyclopedia:Attack}attack rolls{/g} for 1 {g|Encyclopedia:Combat_Round}round{/g}. " +
                "You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier." +
                "\nGale Aura (Su): At 6th level, as a standard action, you can create a 30-foot aura of gale-like winds that slows the progress of enemies. Creatures " +
                "in the aura cannot take a 5-foot step and treat it as as difficult terrain. You can use this ability for a number of rounds per " +
                "day equal to your cleric level. The rounds do not need to be consecutive.\nDomain Spells: snowball, summon small elemental, call lightning, slowing mud, " +
                "call lightning storm, sirocco, sunburst, fire storm, tsunami.");
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
                    Helpers.LevelEntry(1, StormDomainBaseFeature),
                    Helpers.LevelEntry(6, StormDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(StormDomainBaseFeature, StormDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // Secondary Domain Progression
            var StormDomainProgressionSecondary = Helpers.CreateBlueprint<BlueprintProgression>("StormDomainProgressionSecondary", bp => {
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = StormDomainAllowed.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = StormDomainProgression.ToReference<BlueprintFeatureReference>();
                });                
                bp.m_AllowNonContextActions = false;
                bp.SetName("Storm Subdomain");
                bp.SetDescription("\nWith power over storm and sky, you can call down the wrath of the gods upon the world below.\nStorm Burst: As " +
                "a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can create a storm burst targeting any foe within 30 feet as a ranged " +
                "{g|Encyclopedia:TouchAttack}touch attack{/g}. The storm burst deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} + " +
                "1 point for every two levels you possess in the class that gave you access to this domain. In addition, the target is buffeted by winds and rain, " +
                "causing it to take a –2 {g|Encyclopedia:Penalty}penalty{/g} on {g|Encyclopedia:Attack}attack rolls{/g} for 1 {g|Encyclopedia:Combat_Round}round{/g}. " +
                "You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier." +
                "\nGale Aura (Su): At 6th level, as a standard action, you can create a 30-foot aura of gale-like winds that slows the progress of enemies. Creatures " +
                "in the aura cannot take a 5-foot step and treat it as as difficult terrain. You can use this ability for a number of rounds per " +
                "day equal to your cleric level. The rounds do not need to be consecutive.\nDomain Spells: snowball, summon small elemental, call lightning, slowing mud, " +
                "call lightning storm, sirocco, sunburst, fire storm, tsunami.");
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
                    Helpers.LevelEntry(1, StormDomainBaseFeature),
                    Helpers.LevelEntry(6, StormDomainGreaterFeature)
                };
                bp.UIGroups = new UIGroup[] {
                    Helpers.CreateUIGroup(StormDomainBaseFeature, StormDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            // StormDomainSpellListFeatureDruid
            var StormDomainSpellListFeatureDruid = Helpers.CreateBlueprint<BlueprintFeature>("StormDomainSpellListFeatureDruid", bp => {
                bp.AddComponent<AddSpecialSpellList>(c => {
                    c.m_CharacterClass = DruidClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_SpellList = StormDomainSpellList.ToReference<BlueprintSpellListReference>();
                });
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });
            // StormDomainProgressionDruid
            var StormDomainProgressionDruid = Helpers.CreateBlueprint<BlueprintProgression>("StormDomainProgressionDruid", bp => {
                bp.SetName("Storm Subdomain");
                bp.SetDescription("\nWith power over storm and sky, you can call down the wrath of the gods upon the world below.\nStorm Burst: As " +
                "a {g|Encyclopedia:Standard_Actions}standard action{/g}, you can create a storm burst targeting any foe within 30 feet as a ranged " +
                "{g|Encyclopedia:TouchAttack}touch attack{/g}. The storm burst deals {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Damage}damage{/g} + " +
                "1 point for every two levels you possess in the class that gave you access to this domain. In addition, the target is buffeted by winds and rain, " +
                "causing it to take a –2 {g|Encyclopedia:Penalty}penalty{/g} on {g|Encyclopedia:Attack}attack rolls{/g} for 1 {g|Encyclopedia:Combat_Round}round{/g}. " +
                "You can use this ability a number of times per day equal to 3 + your {g|Encyclopedia:Wisdom}Wisdom{/g} modifier." +
                "\nGale Aura (Su): At 6th level, as a standard action, you can create a 30-foot aura of gale-like winds that slows the progress of enemies. Creatures " +
                "in the aura cannot take a 5-foot step and treat it as as difficult terrain. You can use this ability for a number of rounds per " +
                "day equal to your cleric level. The rounds do not need to be consecutive.\nDomain Spells: snowball, summon small elemental, call lightning, slowing mud, " +
                "call lightning storm, sirocco, sunburst, fire storm, tsunami.");
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
                    Helpers.LevelEntry(1, StormDomainBaseFeature, StormDomainSpellListFeatureDruid),
                    Helpers.LevelEntry(6, StormDomainGreaterFeature)
                };
                bp.GiveFeaturesForPreviousLevels = true;
            });
            StormDomainAllowed.IsPrerequisiteFor = new List<BlueprintFeatureReference>() { 
                WeatherBlessingFeature.ToReference<BlueprintFeatureReference>(),
                StormDomainProgression.ToReference<BlueprintFeatureReference>(),
                StormDomainProgressionSecondary.ToReference<BlueprintFeatureReference>()
            };
            StormDomainProgression.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = StormDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            }); 
            StormDomainProgressionSecondary.AddComponent<PrerequisiteNoFeature>(c => {
                    c.Group = Prerequisite.GroupType.All;
                    c.HideInUI = true;
                    c.m_Feature = StormDomainProgressionSecondary.ToReference<BlueprintFeatureReference>();
            });
            var DomainMastery = Resources.GetBlueprint<BlueprintFeature>("2de64f6a1f2baee4f9b7e52e3f046ec5").GetComponent<AutoMetamagic>();
            DomainMastery.Abilities.Add(StormDomainGreaterAbility.ToReference<BlueprintAbilityReference>());
            if (ModSettings.AddedContent.Domains.IsDisabled("Storm Subdomain")) { return; }
            DomainTools.RegisterDomain(StormDomainProgression);
            DomainTools.RegisterSecondaryDomain(StormDomainProgressionSecondary);
            DomainTools.RegisterDruidDomain(StormDomainProgressionDruid);
            DomainTools.RegisterBlightDruidDomain(StormDomainProgressionDruid);
        }

    }
}
