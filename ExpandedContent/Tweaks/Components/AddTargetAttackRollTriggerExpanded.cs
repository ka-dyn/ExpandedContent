using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.ContextData;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Components {

    [ComponentName("Adding NotCrit version of AddTargetAttackRollTrigger")]

    public class AddTargetAttackRollTriggerExpanded : EntityFactComponentDelegate, ITargetRulebookHandler<RuleAttackRoll>, IRulebookHandler<RuleAttackRoll>, ISubscriber, ITargetRulebookSubscriber {
        [HideIf("CriticalHit")]
        public bool OnlyHit = true;

        public bool CriticalHit;

        public bool NotCriticalHit;

        public bool OnlyMelee;

        public bool NotReach;

        public bool CheckCategory;

        [ShowIf("CheckCategory")]
        public bool Not;

        [ShowIf("CheckCategory")]
        public WeaponCategory[] Categories;

        public bool AffectFriendlyTouchSpells;

        public ActionList ActionsOnAttacker;

        public ActionList ActionOnSelf;

        [InfoBox("Ignore attacker's roll")]
        public bool DoNotPassAttackRoll;

        public void OnEventAboutToTrigger(RuleAttackRoll evt) {
        }

        public void OnEventDidTrigger(RuleAttackRoll evt) {
            if (evt.IsFake || !CheckConditions(evt)) {
                return;
            }

            using (DoNotPassAttackRoll ? null : ContextData<ContextAttackData>.Request().Setup(evt)) {
                (base.Fact as IFactContextOwner)?.RunActionInContext(ActionsOnAttacker, evt.Initiator);
                (base.Fact as IFactContextOwner)?.RunActionInContext(ActionOnSelf, evt.Target);
            }
        }

        public bool CheckConditions(RuleAttackRoll evt) {
            ItemEntity itemEntity = (base.Fact as ItemEnchantment)?.Owner;
            ItemEntityWeapon itemEntityWeapon = (evt.Reason.Rule as RuleAttackWithWeapon)?.Weapon;
            if (itemEntity != null && itemEntity != itemEntityWeapon) {
                return false;
            }

            if (OnlyHit && !evt.IsHit) {
                return false;
            }

            if (CriticalHit && (!evt.IsCriticalConfirmed || evt.FortificationNegatesCriticalHit)) {
                return false;
            }

            if (NotCriticalHit && evt.IsCriticalConfirmed) {
                return false;
            }

            if (OnlyMelee && (evt.Weapon == null || !evt.Weapon.Blueprint.IsMelee)) {
                return false;
            }

            if (NotReach && (evt.Weapon == null || evt.Weapon.Blueprint.Type.AttackRange > GameConsts.MinWeaponRange)) {
                return false;
            }

            if (CheckCategory && !Not && (evt.Weapon == null || !Enumerable.Contains(Categories, evt.Weapon.Blueprint.Type.Category))) {
                return false;
            }

            if (CheckCategory && Not && (evt.Weapon == null || Enumerable.Contains(Categories, evt.Weapon.Blueprint.Type.Category))) {
                return false;
            }

            if (!AffectFriendlyTouchSpells && !evt.Initiator.IsEnemy(evt.Target) && (evt.Weapon.Blueprint.Type.AttackType == AttackType.Touch || evt.Weapon.Blueprint.Type.AttackType == AttackType.RangedTouch)) {
                return false;
            }

            return true;
        }
    }
}
