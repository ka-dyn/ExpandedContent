using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;
using System.Linq;
using ExpandedContent.Tweaks.Components;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;

namespace ExpandedContent.Tweaks.Domains {
    internal class PlantDomainPatch {

        public static void PatchPlantDomain() {

            var StargazerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("StargazerClass");
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var EcclesitheurgeArchetype = Resources.GetBlueprint<BlueprintArchetype>("472af8cb3de628f4a805dc4a038971bc");
            var InquisitorClass = Resources.GetBlueprint<BlueprintCharacterClass>("f1a70d9e1b0b41e49874e1fa9052a1ce");
            var HunterClass = Resources.GetBlueprint<BlueprintCharacterClass>("34ecd1b5e1b90b9498795791b0855239");
            var DivineHunterArchetype = Resources.GetBlueprint<BlueprintArchetype>("f996f0a18e5d945459e710ee3a6dd485");
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var TempleChampionArchetype = Resources.GetModBlueprint<BlueprintArchetype>("TempleChampionArchetype");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");
            var WoodenFistIcon = AssetLoader.LoadInternal("Skills", "Icon_WoodenFist.jpg");


            if (ModSettings.AddedContent.Domains.IsDisabled("Plant Domain Patch")) { return; }
            var PlantDomainAllowed = Resources.GetBlueprint<BlueprintFeature>("0e03c2a03222b0b42acf96096b286327");
            var PlantDomainProgression = Resources.GetBlueprint<BlueprintProgression>("467d2a1d2107da64395b591393baad17");
            var PlantDomainProgressionSecondary = Resources.GetBlueprint<BlueprintProgression>("22f3a592849c06e4c8869e2132e11597");
            var PlantDomainProgressionDruid = Resources.GetBlueprint<BlueprintProgression>("ee77836d10835fd49a8831adb3a14640");
            var PlantDomainProgressionSeparatist = Resources.GetBlueprint<BlueprintProgression>("ad7f2170334642fabf6adae5c36e04f4");
            var PlantDomainProgressions = new BlueprintProgression[] { 
                PlantDomainProgression,
                PlantDomainProgressionSecondary,
                PlantDomainProgressionDruid,
                PlantDomainProgressionSeparatist
            };
            foreach (var DomainPrgression in PlantDomainProgressions) {
                DomainPrgression.SetDescription("\nYou find solace in the green, can grow defensive thorns, and can communicate with plants.\nWooden Fist: As a free action, your hands can " +
                    "become as hard as wood, covered in tiny thorns. While you have wooden fists, your unarmed strikes do not provoke attacks of opportunity, and gain a bonus on damage rolls " +
                    "equal to 1/2 your cleric level (minimum +1). You can use this ability for a number of rounds per day equal to 3 + your Wisdom modifier. These rounds do not need to be " +
                    "consecutive.\nBramble Armor: At 6th level, you can cause a host of wooden thorns to burst from your skin as a {g|Encyclopedia:Free_Action}free action{/g}. While bramble " +
                    "armor is in effect, any foe striking you with a melee weapon without {g|Encyclopedia:Reach}reach{/g} takes {g|Encyclopedia:Dice}1d6{/g} points of " +
                    "{g|Encyclopedia:Damage_Type}piercing damage{/g} + 1 point per two levels you possess in the class that gave you access to this domain. You can use this ability for a " +
                    "number of rounds per day equal to your level in the class that gave you access to this domain. These rounds do not need to be consecutive.\nDomain Spells: entangle, barkskin, " +
                    "contagion, thorn body, vinetrap, plant shape II (shambling mound), changestaff, mind blank, shambler.");
            }
            var PlantShapeIIShamblingMoundSpell = Resources.GetModBlueprint<BlueprintAbility>("PlantShapeIIAbility");
            var ShamblerSpell = Resources.GetModBlueprint<BlueprintAbility>("ShamblerAbility");
            var PlantDomainSpellList = Resources.GetBlueprint<BlueprintSpellList>("bd7b088a54b79434f90ed53551ca2189");
            PlantDomainSpellList.SpellsByLevel
                .Where(level => level.SpellLevel == 6)
                .ForEach(level => level.Spells.Clear());
            PlantDomainSpellList.SpellsByLevel[6].m_Spells.Add(PlantShapeIIShamblingMoundSpell.ToReference<BlueprintAbilityReference>());
            PlantDomainSpellList.SpellsByLevel
                .Where(level => level.SpellLevel == 9)
                .ForEach(level => level.Spells.Clear());
            PlantDomainSpellList.SpellsByLevel[9].m_Spells.Add(ShamblerSpell.ToReference<BlueprintAbilityReference>());

            var PlantDomainNewBaseResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("PlantDomainNewBaseResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 3,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
            });
            var PlantDomainNewBaseResourceSeparatist = Helpers.CreateBlueprint<BlueprintAbilityResource>("PlantDomainNewBaseResourceSeparatist", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 2,
                    IncreasedByLevel = false,
                    IncreasedByStat = true,
                    ResourceBonusStat = StatType.Wisdom,
                };
            });

            var PlantDomainNewBaseBuff = Helpers.CreateBuff("PlantDomainNewBaseBuff", bp => {
                bp.SetName("Wooden Fist");
                bp.SetDescription("As a free action, your hands can become as hard as wood, covered in tiny thorns. While you have wooden fists, your unarmed strikes do not provoke " +
                    "attacks of opportunity, and gain a bonus on damage rolls equal to 1/2 your cleric level (minimum +1). You can use this ability for a number of rounds per day " +
                    "equal to 3 + your Wisdom modifier. These rounds do not need to be consecutive.");
                bp.m_Icon = WoodenFistIcon;
                bp.AddComponent<AddMechanicsFeature>(c => {
                    c.m_Feature = AddMechanicsFeature.MechanicsFeatureType.ImprovedUnarmedStrike;
                });
                bp.AddComponent<WeaponTypeContextDamageBonus>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default,
                    };
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Categories = new WeaponCategory[] { WeaponCategory.UnarmedStrike };
                    c.ExceptForCategories = false;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { 
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>(),
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.m_UseMin = true;
                    c.m_Min = 1;
                });
            });
            var PlantDomainNewBaseBuffSeparatist = Helpers.CreateBuff("PlantDomainNewBaseBuffSeparatist", bp => {
                bp.SetName("Wooden Fist");
                bp.SetDescription("As a free action, your hands can become as hard as wood, covered in tiny thorns. While you have wooden fists, your unarmed strikes do not provoke " +
                    "attacks of opportunity, and gain a bonus on damage rolls equal to 1/2 your cleric level (minimum +1). You can use this ability for a number of rounds per day " +
                    "equal to 3 + your Wisdom modifier. These rounds do not need to be consecutive.");
                bp.m_Icon = WoodenFistIcon;
                bp.AddComponent<AddMechanicsFeature>(c => {
                    c.m_Feature = AddMechanicsFeature.MechanicsFeatureType.ImprovedUnarmedStrike;
                });
                bp.AddComponent<WeaponTypeContextDamageBonus>(c => {
                    c.Value = new ContextValue() {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.Damage,
                    };
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Categories = new WeaponCategory[] { WeaponCategory.UnarmedStrike };
                    c.ExceptForCategories = false;
                });
                bp.AddComponent<ContextCalculateSharedValue>(c => {
                    c.ValueType = AbilitySharedValue.Damage;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.One,
                        DiceCountValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.DamageDice
                        },
                        BonusValue = new ContextValue() {
                            ValueType = ContextValueType.Rank,
                            Value = 0,
                            ValueRank = AbilityRankType.Default
                        }
                    };
                    c.Modifier = 1;
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.Archetype = DivineHunterArchetype.ToReference<BlueprintArchetypeReference>();
                    c.m_AdditionalArchetypes = new BlueprintArchetypeReference[] { 
                        TempleChampionArchetype.ToReference<BlueprintArchetypeReference>(),
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        InquisitorClass.ToReference<BlueprintCharacterClassReference>(),
                        HunterClass.ToReference<BlueprintCharacterClassReference>(),
                        PaladinClass.ToReference<BlueprintCharacterClassReference>(),
                        StargazerClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_Progression = ContextRankProgression.DelayedStartPlusDivStep;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        ClericClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_StartLevel = 4;
                    c.m_StepLevel = 2;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                });
            });


            var PlantDomainNewBaseAbility = Helpers.CreateBlueprint<BlueprintActivatableAbility>("PlantDomainNewBaseAbility", bp => {
                bp.SetName("Wooden Fist");
                bp.SetDescription("As a free action, your hands can become as hard as wood, covered in tiny thorns. While you have wooden fists, your unarmed strikes do not provoke " +
                    "attacks of opportunity, and gain a bonus on damage rolls equal to 1/2 your cleric level (minimum +1). You can use this ability for a number of rounds per day " +
                    "equal to 3 + your Wisdom modifier. These rounds do not need to be consecutive.");
                bp.m_Icon = WoodenFistIcon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.NewRound;
                    c.m_RequiredResource = PlantDomainNewBaseResource.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = PlantDomainNewBaseBuff.ToReference<BlueprintBuffReference>();
                bp.IsOnByDefault = false;
                bp.DeactivateIfCombatEnded = true;
                bp.DeactivateIfOwnerDisabled = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
            });
            var PlantDomainNewBaseAbilitySeparatist = Helpers.CreateBlueprint<BlueprintActivatableAbility>("PlantDomainNewBaseAbilitySeparatist", bp => {
                bp.SetName("Wooden Fist");
                bp.SetDescription("As a free action, your hands can become as hard as wood, covered in tiny thorns. While you have wooden fists, your unarmed strikes do not provoke " +
                    "attacks of opportunity, and gain a bonus on damage rolls equal to 1/2 your cleric level (minimum +1). You can use this ability for a number of rounds per day " +
                    "equal to 3 + your Wisdom modifier. These rounds do not need to be consecutive.");
                bp.m_Icon = WoodenFistIcon;
                bp.AddComponent<ActivatableAbilityResourceLogic>(c => {
                    c.SpendType = ActivatableAbilityResourceLogic.ResourceSpendType.NewRound;
                    c.m_RequiredResource = PlantDomainNewBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                });
                bp.m_Buff = PlantDomainNewBaseBuffSeparatist.ToReference<BlueprintBuffReference>();
                bp.IsOnByDefault = false;
                bp.DeactivateIfCombatEnded = true;
                bp.DeactivateIfOwnerDisabled = true;
                bp.ActivationType = AbilityActivationType.Immediately;
                bp.m_ActivateWithUnitCommand = UnitCommand.CommandType.Free;
            });
            var PlantDomainBaseFeature = Resources.GetBlueprint<BlueprintFeature>("e433267d36089d049b34900fde38032b");
            PlantDomainBaseFeature.m_Icon = WoodenFistIcon;
            PlantDomainBaseFeature.SetDescription("You find solace in the green, can grow defensive thorns, and can communicate with plants.\nWooden Fist: As a free action, your hands can " +
                    "become as hard as wood, covered in tiny thorns. While you have wooden fists, your unarmed strikes do not provoke attacks of opportunity, and gain a bonus on damage rolls " +
                    "equal to 1/2 your cleric level (minimum +1). You can use this ability for a number of rounds per day equal to 3 + your Wisdom modifier. These rounds do not need to be " +
                    "consecutive.\nBramble Armor: At 6th level, you can cause a host of wooden thorns to burst from your skin as a {g|Encyclopedia:Free_Action}free action{/g}. While bramble " +
                    "armor is in effect, any foe striking you with a melee weapon without {g|Encyclopedia:Reach}reach{/g} takes {g|Encyclopedia:Dice}1d6{/g} points of " +
                    "{g|Encyclopedia:Damage_Type}piercing damage{/g} + 1 point per two levels you possess in the class that gave you access to this domain. You can use this ability for a " +
                    "number of rounds per day equal to your level in the class that gave you access to this domain. These rounds do not need to be consecutive.");
            PlantDomainBaseFeature.RemoveComponents<AddFacts>();
            PlantDomainBaseFeature.RemoveComponents<AddAbilityResources>();
            PlantDomainBaseFeature.RemoveComponents<ReplaceAbilitiesStat>();
            PlantDomainBaseFeature.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[] {
                    PlantDomainNewBaseAbility.ToReference<BlueprintUnitFactReference>()
                };
            });
            PlantDomainBaseFeature.AddComponent<AddAbilityResources>(c => {
                c.m_Resource = PlantDomainNewBaseResource.ToReference<BlueprintAbilityResourceReference>();
                c.RestoreAmount = true;
            });
            PlantDomainBaseFeature.AddComponent<ReplaceAbilitiesStat>(c => {
                c.m_Ability = new BlueprintAbilityReference[] { PlantDomainNewBaseAbility.ToReference<BlueprintAbilityReference>() };
                c.Stat = StatType.Wisdom;
            });

            var PlantDomainBaseFeatureSeparatist = Resources.GetBlueprint<BlueprintFeature>("7c61c1b3ab5a41b6b24b7fcce0810a9d");
            PlantDomainBaseFeatureSeparatist.m_Icon = WoodenFistIcon;
            PlantDomainBaseFeatureSeparatist.SetDescription("You find solace in the green, can grow defensive thorns, and can communicate with plants.\nWooden Fist: As a free action, your hands can " +
                    "become as hard as wood, covered in tiny thorns. While you have wooden fists, your unarmed strikes do not provoke attacks of opportunity, and gain a bonus on damage rolls " +
                    "equal to 1/2 your cleric level (minimum +1). You can use this ability for a number of rounds per day equal to 3 + your Wisdom modifier. These rounds do not need to be " +
                    "consecutive.\nBramble Armor: At 6th level, you can cause a host of wooden thorns to burst from your skin as a {g|Encyclopedia:Free_Action}free action{/g}. While bramble " +
                    "armor is in effect, any foe striking you with a melee weapon without {g|Encyclopedia:Reach}reach{/g} takes {g|Encyclopedia:Dice}1d6{/g} points of " +
                    "{g|Encyclopedia:Damage_Type}piercing damage{/g} + 1 point per two levels you possess in the class that gave you access to this domain. You can use this ability for a " +
                    "number of rounds per day equal to your level in the class that gave you access to this domain. These rounds do not need to be consecutive.");
            PlantDomainBaseFeatureSeparatist.RemoveComponents<AddFacts>();
            PlantDomainBaseFeatureSeparatist.RemoveComponents<AddAbilityResources>();
            PlantDomainBaseFeatureSeparatist.RemoveComponents<ReplaceAbilitiesStat>();
            PlantDomainBaseFeatureSeparatist.AddComponent<AddFacts>(c => {
                c.m_Facts = new BlueprintUnitFactReference[] {
                    PlantDomainNewBaseAbilitySeparatist.ToReference<BlueprintUnitFactReference>()
                };
            });
            PlantDomainBaseFeatureSeparatist.AddComponent<AddAbilityResources>(c => {
                c.m_Resource = PlantDomainNewBaseResourceSeparatist.ToReference<BlueprintAbilityResourceReference>();
                c.RestoreAmount = true;
            });
            PlantDomainBaseFeatureSeparatist.AddComponent<ReplaceAbilitiesStat>(c => {
                c.m_Ability = new BlueprintAbilityReference[] { PlantDomainNewBaseAbilitySeparatist.ToReference<BlueprintAbilityReference>() };
                c.Stat = StatType.Wisdom;
            });
        }
    }
}
