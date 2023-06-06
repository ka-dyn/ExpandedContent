using Kingmaker;
using Kingmaker.ElementsSystem;
using Kingmaker.Enums;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Components {
    //Based off Zedals Swashbuckler component, needs testing as I've most likely broke it
    internal class SpearParry : UnitFactComponentDelegate, ITargetRulebookHandler<RuleAttackRoll>, ITargetRulebookSubscriber {

        public void OnEventAboutToTrigger(RuleAttackRoll evt) {
            if (!evt.Weapon.Blueprint.IsMelee || evt.Parry != null || !Owner.IsReach(evt.Target, Owner.Body.PrimaryHand) || (!IsWeaponASpear(base.Owner.Body.PrimaryHand) && !IsWeaponASpear(base.Owner.Body.SecondaryHand)))
                return;

            evt.TryParry(Owner, Owner.Body.PrimaryHand.Weapon, 2 * (evt.Initiator.Descriptor.State.Size - Owner.State.Size));

        }
        public void OnEventDidTrigger(RuleAttackRoll evt) {
            
            if (!evt.Parry.IsTriggered)
                return;

            if (evt.Result == AttackResult.Parried && Owner.CombatState.EngagedBy.Count < 2) //FlatFooted
            {                
                Game.Instance.CombatEngagementController.ForceAttackOfOpportunity(Owner, evt.Initiator);

                IFactContextOwner factContextOwner = base.Fact as IFactContextOwner;
                if (factContextOwner != null) 
                {
                    factContextOwner.RunActionInContext(this.ActionOnSelf, evt.Target);
                }
            }            
        }
        //Need to add ActionsOnSelf to apply the -4 penalty
        public static bool IsWeaponASpear(HandSlot hand) {
            return hand.MaybeWeapon != null && (
                hand.MaybeWeapon.Blueprint.Category == WeaponCategory.Shortspear || 
                hand.MaybeWeapon.Blueprint.Category == WeaponCategory.Spear || 
                hand.MaybeWeapon.Blueprint.Category == WeaponCategory.Longspear
                );
        }
        public ActionList ActionOnSelf;
    }
}
