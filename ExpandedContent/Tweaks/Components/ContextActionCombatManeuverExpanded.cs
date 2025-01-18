using Kingmaker.Blueprints;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Components {

    [ComponentName("Bitters CM stuff")]//Only contains things as I need them, mostly nicked from black tentacles for now
    public class ContextActionCombatManeuverExpanded : ContextAction {

        public CombatManeuver Type = CombatManeuver.Grapple;
        public int Bonus;
        public bool HasBABReplacement = true;
        public ContextValue BABReplacementValue;
        public ActionList OnSuccess;
        public ActionList OnFailure;

        public override string GetCaption() => "Combat maneuver: " + Type.ToString();

        public override void RunAction() {

            if (Target.Unit == null) {
                return;
            } else if (Context.MaybeCaster == null) {
                return;
            }

            RuleCombatManeuver ruleCombatManeuver = new RuleCombatManeuver(Context.MaybeCaster, Target.Unit, Type);
            



            if (HasBABReplacement) {
                int BABbonus = BABReplacementValue.Calculate(Context);
                Main.Log($"This should equal the contextvalue, {BABbonus}");
                BABbonus = BABbonus + Bonus;

                ruleCombatManeuver.OverrideBonus = BABbonus;
            }

            if (Context.TriggerRule(ruleCombatManeuver).Success) {
                OnSuccess.Run();
            } else {
                OnFailure.Run();
            }
        }
    }
}
