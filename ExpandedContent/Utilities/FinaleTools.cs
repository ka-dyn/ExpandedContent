using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Utilities {
    internal class FinaleTools {
        public static Conditional RemovePerformance() {
            BlueprintBuff InspireCourageBuff = Resources.GetBlueprint<BlueprintBuff>("b4027a834204042409248889cc8abf67"); //InspireCourage
            BlueprintBuff ArchaeologistLuckBuff = Resources.GetBlueprint<BlueprintBuff>("2ef43eabd1d15fe4399e45806e9ca2d0"); //ArchaeologistLuck
            BlueprintBuff BeastTamerInspireFerocityBuff = Resources.GetBlueprint<BlueprintBuff>("41040d55528a1fb4c8fb519247efeb80"); //BeastTamerInspireFerocity
            BlueprintBuff DirgeBardDanceOfTheDeadBuff = Resources.GetBlueprint<BlueprintBuff>("be20c77faa6c05f4387e52985e3358fc"); //DirgeBardDanceOfTheDead
            BlueprintBuff FlameDancerPerformanceBuff = Resources.GetBlueprint<BlueprintBuff>("bf9493f27bb23d74bb598fb1a7a9fe3a"); //FlameDancerPerformance
            BlueprintBuff InciteRageAllBuff = Resources.GetBlueprint<BlueprintBuff>("4bb4c9464138db14d9ec59df8bf452e7"); //InciteRageAll
            BlueprintBuff InciteRageAlliesBuff = Resources.GetBlueprint<BlueprintBuff>("67a54db32ec015245b1e7e87a47b1fe4"); //InciteRageAllies
            BlueprintBuff InciteRageEnemiesBuff = Resources.GetBlueprint<BlueprintBuff>("b190408f43feb02499e40dd2d6c687d9"); //InciteRageEnemies
            BlueprintBuff StormCallBuff = Resources.GetBlueprint<BlueprintBuff>("e74b11fb5688f7e438ea949475f99d56"); //StormCall
            BlueprintBuff InspireTranquiilityBuff = Resources.GetBlueprint<BlueprintBuff>("3f7a7d22cb32697408f9cb3e31125a35"); //InspireTranquiility
            BlueprintBuff DirgeOfDoomBardBuff = Resources.GetBlueprint<BlueprintBuff>("83eab9b139717ad478d84bbf48ab457f"); //DirgeOfDoomBard
            BlueprintBuff FascinateBuff = Resources.GetBlueprint<BlueprintBuff>("555930f121b364a4e82670b433028728"); //Fascinate
            BlueprintBuff FrighteningTuneBuff = Resources.GetBlueprint<BlueprintBuff>("6d0a82635b9167a4584ff74f5cd50315"); //FrighteningTune
            BlueprintBuff InspireCompetenceBuff = Resources.GetBlueprint<BlueprintBuff>("f58e8500ebc8594499bd804b0277cdd8"); //InspireCompetence
            BlueprintBuff InspireGreatnessBuff = Resources.GetBlueprint<BlueprintBuff>("8618d4515685b6d4197f254b7f56a68c"); //InspireGreatness
            BlueprintBuff InspireHeroicsBuff = Resources.GetBlueprint<BlueprintBuff>("ab81563882fcf3a41bc657e0c6677ea2"); //InspireHeroics
            BlueprintBuff BattleProwessBuff = Resources.GetBlueprint<BlueprintBuff>("5c18dc7ce97fea248bece76f3b7f8e9a"); //BattleProwess
            BlueprintBuff InsightfullContemplationBuff = Resources.GetBlueprint<BlueprintBuff>("9575fd7f7036fce4da6021415a34313a"); //InsightfullContemplation
            BlueprintBuff SongOfInspirationBuff = Resources.GetBlueprint<BlueprintBuff>("ade95f24a1c35ba4dbb5323f9040cd86"); //SongOfInspiration
            BlueprintBuff CallOfTheWildBeastShapeIBuff = Resources.GetBlueprint<BlueprintBuff>("7a59741b863e3fe4780363edea805b89"); //CallOfTheWildBeastShapeI
            BlueprintBuff CallOfTheWildBeastShapeIIBuff = Resources.GetBlueprint<BlueprintBuff>("a72451b12bbf621418eed960d93c6c53"); //CallOfTheWildBeastShapeII
            BlueprintBuff CallOfTheWildBeastShapeIIIBuff = Resources.GetBlueprint<BlueprintBuff>("db9b5a75449d82b49a6bee944ecf824a"); //CallOfTheWildBeastShapeIII
            BlueprintBuff SongOfTheSensesBuff = Resources.GetBlueprint<BlueprintBuff>("1e551c146d52e6446aacae961a79164f"); //SongOfTheSenses
            BlueprintBuff DirgeOfDoomSkaldBuff = Resources.GetBlueprint<BlueprintBuff>("85d52d9ef99b9d24180b48b8da7f29d8"); //DirgeOfDoomSkald
            BlueprintBuff InspiredRageBuff = Resources.GetBlueprint<BlueprintBuff>("220e58fee6ab0bf4fab8d1a66b01edd0"); //InspiredRage
            BlueprintBuff SongOfStrengthBuff = Resources.GetBlueprint<BlueprintBuff>("538c9ee7531e7df4ba7ef3691397bbf3"); //SongOfStrength
            BlueprintBuff SongOfTheFallenBuff = Resources.GetBlueprint<BlueprintBuff>("83518d367a854084699b7e1902a3fcc2"); //SongOfTheFallen
            BlueprintFeature LingeringPerformance = Resources.GetBlueprint<BlueprintFeature>("17239b298065efc459cffe2220ecb559");
            return new Conditional() {
                ConditionsChecker = new ConditionsChecker() {
                    Operation = Operation.And,
                    Conditions = new Condition[] {
                        new ContextConditionCasterHasFact() {
                            Not = false,
                            m_Fact = LingeringPerformance.ToReference<BlueprintUnitFactReference>()
                        }
                    }
                },
                IfTrue = Helpers.CreateActionList(),
                IfFalse = Helpers.CreateActionList(                                    
                    new ContextActionRemoveBuff() {
                        m_Buff = InspireCourageBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = ArchaeologistLuckBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = BeastTamerInspireFerocityBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = DirgeBardDanceOfTheDeadBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = FlameDancerPerformanceBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = InciteRageAllBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = InciteRageAlliesBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = InciteRageEnemiesBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = StormCallBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = InspireTranquiilityBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = DirgeOfDoomBardBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = FascinateBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = FrighteningTuneBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = InspireCompetenceBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = InspireGreatnessBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = InspireHeroicsBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = BattleProwessBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = InsightfullContemplationBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = SongOfInspirationBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = CallOfTheWildBeastShapeIBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = CallOfTheWildBeastShapeIIBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = CallOfTheWildBeastShapeIIIBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = SongOfTheSensesBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = DirgeOfDoomSkaldBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = InspiredRageBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = SongOfStrengthBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    },
                    new ContextActionRemoveBuff() {
                        m_Buff = SongOfTheFallenBuff.ToReference<BlueprintBuffReference>(),
                        RemoveRank = false,
                        ToCaster = true
                    }   
                )
            };
        }
        public static AbilityCasterHasFacts HasPerformance() {
            BlueprintBuff InspireCourageBuff = Resources.GetBlueprint<BlueprintBuff>("b4027a834204042409248889cc8abf67"); //InspireCourage
            BlueprintBuff ArchaeologistLuckBuff = Resources.GetBlueprint<BlueprintBuff>("2ef43eabd1d15fe4399e45806e9ca2d0"); //ArchaeologistLuck
            BlueprintBuff BeastTamerInspireFerocityBuff = Resources.GetBlueprint<BlueprintBuff>("41040d55528a1fb4c8fb519247efeb80"); //BeastTamerInspireFerocity
            BlueprintBuff DirgeBardDanceOfTheDeadBuff = Resources.GetBlueprint<BlueprintBuff>("be20c77faa6c05f4387e52985e3358fc"); //DirgeBardDanceOfTheDead
            BlueprintBuff FlameDancerPerformanceBuff = Resources.GetBlueprint<BlueprintBuff>("bf9493f27bb23d74bb598fb1a7a9fe3a"); //FlameDancerPerformance
            BlueprintBuff InciteRageAllBuff = Resources.GetBlueprint<BlueprintBuff>("4bb4c9464138db14d9ec59df8bf452e7"); //InciteRageAll
            BlueprintBuff InciteRageAlliesBuff = Resources.GetBlueprint<BlueprintBuff>("67a54db32ec015245b1e7e87a47b1fe4"); //InciteRageAllies
            BlueprintBuff InciteRageEnemiesBuff = Resources.GetBlueprint<BlueprintBuff>("b190408f43feb02499e40dd2d6c687d9"); //InciteRageEnemies
            BlueprintBuff StormCallBuff = Resources.GetBlueprint<BlueprintBuff>("e74b11fb5688f7e438ea949475f99d56"); //StormCall
            BlueprintBuff InspireTranquiilityBuff = Resources.GetBlueprint<BlueprintBuff>("3f7a7d22cb32697408f9cb3e31125a35"); //InspireTranquiility
            BlueprintBuff DirgeOfDoomBardBuff = Resources.GetBlueprint<BlueprintBuff>("83eab9b139717ad478d84bbf48ab457f"); //DirgeOfDoomBard
            BlueprintBuff FascinateBuff = Resources.GetBlueprint<BlueprintBuff>("555930f121b364a4e82670b433028728"); //Fascinate
            BlueprintBuff FrighteningTuneBuff = Resources.GetBlueprint<BlueprintBuff>("6d0a82635b9167a4584ff74f5cd50315"); //FrighteningTune
            BlueprintBuff InspireCompetenceBuff = Resources.GetBlueprint<BlueprintBuff>("f58e8500ebc8594499bd804b0277cdd8"); //InspireCompetence
            BlueprintBuff InspireGreatnessBuff = Resources.GetBlueprint<BlueprintBuff>("8618d4515685b6d4197f254b7f56a68c"); //InspireGreatness
            BlueprintBuff InspireHeroicsBuff = Resources.GetBlueprint<BlueprintBuff>("ab81563882fcf3a41bc657e0c6677ea2"); //InspireHeroics
            BlueprintBuff BattleProwessBuff = Resources.GetBlueprint<BlueprintBuff>("5c18dc7ce97fea248bece76f3b7f8e9a"); //BattleProwess
            BlueprintBuff InsightfullContemplationBuff = Resources.GetBlueprint<BlueprintBuff>("9575fd7f7036fce4da6021415a34313a"); //InsightfullContemplation
            BlueprintBuff SongOfInspirationBuff = Resources.GetBlueprint<BlueprintBuff>("ade95f24a1c35ba4dbb5323f9040cd86"); //SongOfInspiration
            BlueprintBuff CallOfTheWildBeastShapeIBuff = Resources.GetBlueprint<BlueprintBuff>("7a59741b863e3fe4780363edea805b89"); //CallOfTheWildBeastShapeI
            BlueprintBuff CallOfTheWildBeastShapeIIBuff = Resources.GetBlueprint<BlueprintBuff>("a72451b12bbf621418eed960d93c6c53"); //CallOfTheWildBeastShapeII
            BlueprintBuff CallOfTheWildBeastShapeIIIBuff = Resources.GetBlueprint<BlueprintBuff>("db9b5a75449d82b49a6bee944ecf824a"); //CallOfTheWildBeastShapeIII
            BlueprintBuff SongOfTheSensesBuff = Resources.GetBlueprint<BlueprintBuff>("1e551c146d52e6446aacae961a79164f"); //SongOfTheSenses
            BlueprintBuff DirgeOfDoomSkaldBuff = Resources.GetBlueprint<BlueprintBuff>("85d52d9ef99b9d24180b48b8da7f29d8"); //DirgeOfDoomSkald
            BlueprintBuff InspiredRageBuff = Resources.GetBlueprint<BlueprintBuff>("220e58fee6ab0bf4fab8d1a66b01edd0"); //InspiredRage
            BlueprintBuff SongOfStrengthBuff = Resources.GetBlueprint<BlueprintBuff>("538c9ee7531e7df4ba7ef3691397bbf3"); //SongOfStrength
            BlueprintBuff SongOfTheFallenBuff = Resources.GetBlueprint<BlueprintBuff>("83518d367a854084699b7e1902a3fcc2"); //SongOfTheFallen
            return new AbilityCasterHasFacts() {
                m_Facts = new BlueprintUnitFactReference[] {
                    InspireCourageBuff.ToReference<BlueprintUnitFactReference>(),
                    ArchaeologistLuckBuff.ToReference<BlueprintUnitFactReference>(),
                    BeastTamerInspireFerocityBuff.ToReference<BlueprintUnitFactReference>(),
                    DirgeBardDanceOfTheDeadBuff.ToReference<BlueprintUnitFactReference>(),
                    FlameDancerPerformanceBuff.ToReference<BlueprintUnitFactReference>(),
                    InciteRageAllBuff.ToReference<BlueprintUnitFactReference>(),
                    InciteRageAlliesBuff.ToReference<BlueprintUnitFactReference>(),
                    InciteRageEnemiesBuff.ToReference<BlueprintUnitFactReference>(),
                    StormCallBuff.ToReference<BlueprintUnitFactReference>(),
                    InspireTranquiilityBuff.ToReference<BlueprintUnitFactReference>(),
                    DirgeOfDoomBardBuff.ToReference<BlueprintUnitFactReference>(),
                    FascinateBuff.ToReference<BlueprintUnitFactReference>(),
                    FrighteningTuneBuff.ToReference<BlueprintUnitFactReference>(),
                    InspireCompetenceBuff.ToReference<BlueprintUnitFactReference>(),
                    InspireGreatnessBuff.ToReference<BlueprintUnitFactReference>(),
                    InspireHeroicsBuff.ToReference<BlueprintUnitFactReference>(),
                    BattleProwessBuff.ToReference<BlueprintUnitFactReference>(),
                    InsightfullContemplationBuff.ToReference<BlueprintUnitFactReference>(),
                    SongOfInspirationBuff.ToReference<BlueprintUnitFactReference>(),
                    CallOfTheWildBeastShapeIBuff.ToReference<BlueprintUnitFactReference>(),
                    CallOfTheWildBeastShapeIIBuff.ToReference<BlueprintUnitFactReference>(),
                    CallOfTheWildBeastShapeIIIBuff.ToReference<BlueprintUnitFactReference>(),
                    SongOfTheSensesBuff.ToReference<BlueprintUnitFactReference>(),
                    DirgeOfDoomSkaldBuff.ToReference<BlueprintUnitFactReference>(),
                    InspiredRageBuff.ToReference<BlueprintUnitFactReference>(),
                    SongOfStrengthBuff.ToReference<BlueprintUnitFactReference>(),
                    SongOfTheFallenBuff.ToReference<BlueprintUnitFactReference>()
                },
                NeedsAll = false
            };
        }
    }
}
