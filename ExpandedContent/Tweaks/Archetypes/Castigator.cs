using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;

namespace ExpandedContent.Tweaks.Archetypes {
    internal class Castigator {
        public static void AddCastigator() {

            var OathbreakerClass = Resources.GetModBlueprint<BlueprintCharacterClass>("OathbreakerClass");
            var DreadfulCalm = Resources.GetModBlueprint<BlueprintFeature>("DreadfulCalm");
            var StalwartDefenderDefensivePowerSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("2cd91c501bda80b47ac2df0d51b02973");
            var CVengIcon = AssetLoader.LoadInternal("Skills", "Icon_CVengeance.png");
            var CastigatorVengeance = Helpers.CreateBlueprint<BlueprintFeature>("CastigatorVengeance", bp => {
                bp.SetName("Vengeance");
                bp.SetDescription("At 4th level, a castigator can make an additional number of {g|Encyclopedia:Attack_Of_Opportunity}attacks of opportunity{/g} in a {g|Encyclopedia:Combat_Round}round{/g} equal " +
                    "to their {g|Encyclopedia:Charisma}Charisma{/g} modifier (minimum 1). This effect stacks with the Combat Reflexes {g|Encyclopedia:Feat}feat{/g}.");
                bp.m_DescriptionShort = Helpers.CreateString($"CastigatorVengeance.Description", "At 4th level, a castigator can make an additional number of {g|Encyclopedia:Attack_Of_Opportunity}attacks of " +
                    "opportunity{/g} in a {g|Encyclopedia:Combat_Round}round{/g} equal " +
                    "to their {g|Encyclopedia:Charisma}Charisma{/g} modifier (minimum 1). This effect stacks with the Combat Reflexes {g|Encyclopedia:Feat}feat{/g}.");
                bp.m_Icon = CVengIcon;
                bp.AddComponent<DerivativeStatBonus>(c => {
                    c.BaseStat = StatType.Charisma;
                    c.DerivativeStat = StatType.AttackOfOpportunityCount;
                });
                bp.AddComponent<RecalculateOnStatChange>(c => {
                    c.Stat = StatType.Charisma;
                });
            });
            var CounterIcon = AssetLoader.LoadInternal("Skills", "Icon_Counter.png");
            var CastigatorPaybackFeature = Helpers.CreateBlueprint<BlueprintFeature>("CastigatorPaybackFeature", bp => {
                bp.SetName("Payback");
                bp.SetDescription("At 8th level, a castigator can make an {g|Encyclopedia:Attack_Of_Opportunity}attack of opportunity{/g} as an immediate {g|Encyclopedia:CA_Types}action{/g} against an " +
                    "opponent who hits the castigator with a {g|Encyclopedia:MeleeAttack}melee attack{/g}, so long as the {g|Encyclopedia:Attack}attacking{/g} creature is " +
                    "within the castigator's {g|Encyclopedia:Reach}reach{/g}.");
                bp.m_DescriptionShort = Helpers.CreateString($"CastigatorPayback.Description", "At 8th level, a castigator can make an {g|Encyclopedia:Attack_Of_Opportunity}attack of opportunity{/g} as an " +
                    "immediate {g|Encyclopedia:CA_Types}action{/g} against an " +
                    "opponent who hits the castigator with a {g|Encyclopedia:MeleeAttack}melee attack{/g}, so long as the {g|Encyclopedia:Attack}attacking{/g} creature is " +
                    "within the castigator's {g|Encyclopedia:Reach}reach{/g}.");
                bp.m_Icon = CounterIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddTargetAttackRollTrigger>(c => {
                    c.OnlyHit = true;
                    c.OnlyMelee = true;
                    c.ActionsOnAttacker = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Conditions = new Condition[] {
                                    new ContextConditionIsAlly() {
                                        Not = true,
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                               new ContextActionProvokeAttackFromCaster()),
                            IfFalse = Helpers.CreateActionList(),
                        });
                    c.ActionOnSelf = Helpers.CreateActionList();
                });
            });
            var OpportunistFeature = Resources.GetBlueprint<BlueprintFeature>("5bb6dc5ce00550441880a6ff8ad4c968");
            var CastigatorOpportunistFeature = Helpers.CreateBlueprint<BlueprintFeature>("CastigatorOpportunistFeature", bp => {
                bp.SetName("Even the Score");
                bp.SetDescription("At 12th level, a castigator gains the Opportunist rogue talent.");
                bp.m_DescriptionShort = Helpers.CreateString($"CastigatorOpportunist.Description", "At 12th level, a castigator gains the Opportunist rogue talent.");
                bp.m_Icon = OpportunistFeature.Icon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OpportunistFeature.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var CastigatorArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("CastigatorArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"CastigatorArchetype.Name", "Castigator");
                bp.LocalizedDescription = Helpers.CreateString($"CastigatorArchetype.Description", "For some, \"an eye for an eye\" becomes more than a saying, it becomes a creed by which they live. " +
                    "The castigator is such an individual, always ready to strike when struck.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"OathbreakerClass.Description", "For some, \"an eye for an eye\" becomes more than a saying, it becomes a creed by which they live. " +
                    "The castigator is such an individual, always ready to strike when struck.");
                bp.m_SignatureAbilities = new BlueprintFeatureReference[3] {
                    CastigatorVengeance.ToReference<BlueprintFeatureReference>(),
                    CastigatorPaybackFeature.ToReference<BlueprintFeatureReference>(),
                    CastigatorOpportunistFeature.ToReference<BlueprintFeatureReference>()
                };
                bp.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(4, DreadfulCalm),
                    Helpers.LevelEntry(6, StalwartDefenderDefensivePowerSelection),
                    Helpers.LevelEntry(8, StalwartDefenderDefensivePowerSelection),
                    Helpers.LevelEntry(10, StalwartDefenderDefensivePowerSelection),
                    Helpers.LevelEntry(12, StalwartDefenderDefensivePowerSelection),
                    Helpers.LevelEntry(14, StalwartDefenderDefensivePowerSelection),
                    Helpers.LevelEntry(16, StalwartDefenderDefensivePowerSelection)
                };
                bp.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(4, CastigatorVengeance),
                    Helpers.LevelEntry(8, CastigatorPaybackFeature),
                    Helpers.LevelEntry(12, CastigatorOpportunistFeature)
                };
            });
            OathbreakerClass.m_Archetypes = OathbreakerClass.m_Archetypes.AppendToArray(CastigatorArchetype.ToReference<BlueprintArchetypeReference>());
        }
    }
}
