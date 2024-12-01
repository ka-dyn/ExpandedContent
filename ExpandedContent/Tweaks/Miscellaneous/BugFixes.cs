using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Components;
using ExpandedContent.Utilities;
using HarmonyLib;
using Kingmaker.Assets.UnitLogic.Mechanics.Properties;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using System.Linq;
using System.Runtime.Remoting.Contexts;

namespace ExpandedContent.Tweaks.Miscellaneous {
    internal class BugFixes {
        //This mod does not normally contain bug fixes for base game stuff as I just don't what to have to deal with that, however sometimes I'll need to
        //otherwise people might not be able to use mod features.
        //
        //Please delete fixes after Owlcat fixes them

        public static void AddBugFixes() {


            var ShamblingMoundGrappledTargetBuff = Resources.GetBlueprint<BlueprintBuff>("2b5743ae1c3e478ab99defebcc881019");
            ShamblingMoundGrappledTargetBuff.AddComponent<AddFactContextActions>(c => {
                c.Activated = Helpers.CreateActionList(
                    new ContextActionDealDamage() {
                        m_Type = ContextActionDealDamage.Type.Damage,
                        DamageType = new DamageTypeDescription() {
                            Type = DamageType.Physical,
                            Common = new DamageTypeDescription.CommomData() {
                                Reality = 0,
                                Alignment = 0,
                                Precision = false
                            },
                            Physical = new DamageTypeDescription.PhysicalData() {
                                Material = 0,
                                Form = PhysicalDamageForm.Bludgeoning | PhysicalDamageForm.Slashing,
                                Enhancement = 0,
                                EnhancementTotal = 0
                            },
                            Energy = DamageEnergyType.Fire
                        },
                        AbilityType = StatType.Unknown,
                        EnergyDrainType = EnergyDrainType.Temporary,
                        Duration = new ContextDurationValue() {
                            Rate = DurationRate.Rounds,
                            DiceType = DiceType.Zero,
                            DiceCountValue = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 0,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            BonusValue = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 0,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            m_IsExtendable = true,
                        },
                        PreRolledSharedValue = AbilitySharedValue.Damage,
                        Value = new ContextDiceValue() {
                            DiceType = DiceType.D6,
                            DiceCountValue = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 2,
                                ValueRank = AbilityRankType.DamageDice,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            BonusValue = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 5,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                        },
                        IsAoE = false,
                        HalfIfSaved = false,
                        ResultSharedValue = AbilitySharedValue.Damage,
                        CriticalSharedValue = AbilitySharedValue.Damage
                    }
                    );
                c.Deactivated = Helpers.CreateActionList();
                c.NewRound = Helpers.CreateActionList(
                    new ContextActionDealDamage() {
                        m_Type = ContextActionDealDamage.Type.Damage,
                        DamageType = new DamageTypeDescription() {
                            Type = DamageType.Physical,
                            Common = new DamageTypeDescription.CommomData() {
                                Reality = 0,
                                Alignment = 0,
                                Precision = false
                            },
                            Physical = new DamageTypeDescription.PhysicalData() {
                                Material = 0,
                                Form = PhysicalDamageForm.Bludgeoning | PhysicalDamageForm.Slashing,
                                Enhancement = 0,
                                EnhancementTotal = 0
                            },
                            Energy = DamageEnergyType.Fire
                        },
                        AbilityType = StatType.Unknown,
                        EnergyDrainType = EnergyDrainType.Temporary,
                        Duration = new ContextDurationValue() {
                            Rate = DurationRate.Rounds,
                            DiceType = DiceType.Zero,
                            DiceCountValue = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 0,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            BonusValue = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 0,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            m_IsExtendable = true,
                        },
                        PreRolledSharedValue = AbilitySharedValue.Damage,
                        Value = new ContextDiceValue() {
                            DiceType = DiceType.D6,
                            DiceCountValue = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 4,
                                ValueRank = AbilityRankType.DamageDice,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                            BonusValue = new ContextValue() {
                                ValueType = ContextValueType.Simple,
                                Value = 10,
                                ValueRank = AbilityRankType.Default,
                                ValueShared = AbilitySharedValue.Damage,
                                Property = UnitProperty.None
                            },
                        },
                        IsAoE = false,
                        HalfIfSaved = false,
                        ResultSharedValue = AbilitySharedValue.Damage,
                        CriticalSharedValue = AbilitySharedValue.Damage
                    }
                    );
            });


            //Divine Scourge Hex DC patch
            //Needs to be loaded after to not break any mods that edit the MaxCastingAttributeGetter of WitchHexDCProperty
            var ClericClass = Resources.GetBlueprint<BlueprintCharacterClass>("67819271767a9dd4fbfd4ae700befea0");
            var DivineScourgeArchetype = Resources.GetModBlueprint<BlueprintArchetype>("DivineScourgeArchetype");
            var WitchHexDCProperty = Resources.GetBlueprint<BlueprintUnitProperty>("bdc230ce338f427ba74de65597b0d57a");
            WitchHexDCProperty.AddComponent<MaxCastingAttributeWithArchetypeGetter>(c => {
                c.AttributeBonus = true;
                c.DefaultStat = StatType.Intelligence;
                c.m_Classes = WitchHexDCProperty.GetComponent<MaxCastingAttributeGetter>().m_Classes;
                c.m_Archetypes = new BlueprintArchetypeReference[] { DivineScourgeArchetype.ToReference<BlueprintArchetypeReference>() };
            });
            WitchHexDCProperty.GetComponent<MaxCastingAttributeWithArchetypeGetter>().TemporaryContext(c => {
                c.m_Classes = c.m_Classes.AppendToArray(ClericClass.ToReference<BlueprintCharacterClassReference>());
            });
            WitchHexDCProperty.RemoveComponents<MaxCastingAttributeGetter>();


            //Spontaneous Healing Discovery Resource patch
            //Looks like Owlcat changed from using m_Class to m_ClassDiv, but only put in the alchemist class
            var SpontaneousHealingResource = Resources.GetBlueprint<BlueprintAbilityResource>("0b417a7292b2e924782ef2aab9451816").m_MaxAmount;
            var RogueClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("299aa766dee3cbf4790da4efb8c72484");
            var FighterClass = Resources.GetBlueprintReference<BlueprintCharacterClassReference>("48ac8db94d5de7645906c7d0ad3bcfbd");
            var UndergroundChemistArchetype = Resources.GetBlueprintReference<BlueprintArchetypeReference>("b4f5741d42c4cb04abf5ab3fe33f6fc5");
            var MutationWarriorArchetype = Resources.GetBlueprintReference<BlueprintArchetypeReference>("758e0061a077e54409a3bf0eb51511e5");
            SpontaneousHealingResource.m_ClassDiv = SpontaneousHealingResource.m_ClassDiv.AppendToArray(RogueClass, FighterClass);
            SpontaneousHealingResource.m_ArchetypesDiv = SpontaneousHealingResource.m_ArchetypesDiv.AppendToArray(UndergroundChemistArchetype, MutationWarriorArchetype);


        }


        [HarmonyPatch(typeof(SummClassLevelGetter), nameof(SummClassLevelGetter.GetBaseValue))]
        static class SummClassGetterArchetypeFix {
            static void Postfix(SummClassLevelGetter __instance, UnitEntityData unit, ref int __result) {
                int num = 0;
                var classes = new BlueprintCharacterClassReference[0];
                classes = classes.AppendToArray(__instance.m_Class);
                var archetypes = new BlueprintArchetypeReference[0];
                archetypes = archetypes.AppendToArray(__instance.m_Archetypes);
                archetypes = archetypes.AppendToArray(__instance.Archetype);
                foreach (ClassData data in unit.Descriptor.Progression.Classes) {//For each class you have

                    if (!classes.HasReference(data.CharacterClass)) {//Is it in the list go to next if
                        continue;//If not, end this loop and skip to next class
                    }

                    if (archetypes.Length > 0) {

                        if (archetypes.Any(archetype => data.CharacterClass.Archetypes.HasReference(archetype))) {//Does the characterclass have any of the archetypes on the list?

                            if (archetypes.Any(archetype => data.Archetypes.Contains(archetype))) {//If archetype matches add level, if not then ignore
                                num += data.Level;
                            } //no bonus from this class

                        } else {
                            num += data.Level;//as none of the archetypes where part of the class you get the bonus
                        }

                    } else {
                        num += data.Level;//archetype list was 0
                    }
                }
                __result = num;
            }
        }
    }
}
