using Kingmaker;
using Kingmaker.ElementsSystem;
using Kingmaker.Enums;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace ExpandedContent.Tweaks.Components {
    internal class SpearParry : UnitFactComponentDelegate, ITargetRulebookHandler<RuleAttackRoll>, ITargetRulebookSubscriber {

        public void OnEventAboutToTrigger(RuleAttackRoll evt) {
            //If holding a spear and the enemy is close enough...
            if (!evt.Weapon.Blueprint.IsMelee || evt.Parry != null || !Owner.IsReach(evt.Target, Owner.Body.PrimaryHand) || (!IsWeaponASpear(base.Owner.Body.PrimaryHand) && !IsWeaponASpear(base.Owner.Body.SecondaryHand)))
                return;
            //..parry them
            evt.TryParry(Owner, Owner.Body.PrimaryHand.Weapon, 2 * (evt.Initiator.Descriptor.State.Size - Owner.State.Size));

        }
        public void OnEventDidTrigger(RuleAttackRoll evt) {
            
            if (!evt.Parry.IsTriggered)
                return;

            bool isFlatFooted = Rulebook.Trigger<RuleCheckTargetFlatFooted>(new RuleCheckTargetFlatFooted(evt.Initiator, evt.Target)).IsFlatFooted; //Flatfooted yes/no
            //If you parried them and are not flatfooted
            if (evt.Result == AttackResult.Parried && !isFlatFooted) {   
                //Stab them
                Game.Instance.CombatEngagementController.ForceAttackOfOpportunity(Owner, evt.Initiator);
                
                IFactContextOwner factContextOwner = base.Fact as IFactContextOwner;
                //Also do this (fill in on component addition)
                if (factContextOwner != null) {
                    factContextOwner.RunActionInContext(this.ActionOnSelf, evt.Target);
                }
            }  
            else if (evt.Result == AttackResult.Parried && isFlatFooted) {
                IFactContextOwner factContextOwner = base.Fact as IFactContextOwner;
                //Also do this (fill in on component addition)
                if (factContextOwner != null) {
                    factContextOwner.RunActionInContext(this.ActionOnSelf, evt.Target);
                }
            }
        }
        public static bool IsWeaponASpear(HandSlot hand) {
            return hand.MaybeWeapon != null && (
                hand.MaybeWeapon.Blueprint.Category == WeaponCategory.Shortspear || 
                hand.MaybeWeapon.Blueprint.Category == WeaponCategory.Spear || 
                hand.MaybeWeapon.Blueprint.Category == WeaponCategory.Longspear ||
                hand.MaybeWeapon.Blueprint.Category == WeaponCategory.Trident
                );
        }
        public ActionList ActionOnSelf;
    }
}
