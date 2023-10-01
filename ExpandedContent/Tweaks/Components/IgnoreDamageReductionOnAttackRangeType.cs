using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.EntitySystem;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;
using UnityEngine.Serialization;
using UnityEngine;

namespace ExpandedContent.Tweaks.Components {
    [ComponentName("Ignore damage reduction on attack from range type")]
    [AllowedOn(typeof(BlueprintUnitFact), false)]
    [AllowMultipleComponents]

    public class IgnoreDamageReductionOnAttackRangeType : EntityFactComponentDelegate, IInitiatorRulebookHandler<RuleDealDamage>, IRulebookHandler<RuleDealDamage>, ISubscriber, IInitiatorRulebookSubscriber {
        public bool OnlyOnFullAttack;

        [ShowIf("OnlyOnFullAttack")]
        public bool OnlyOnFirstAttack;

        public bool CriticalHit;

        [SerializeField]
        [FormerlySerializedAs("WeaponType")]
        public BlueprintWeaponTypeReference m_WeaponType;

        public bool CheckEnemyFact;

        [SerializeField]
        [ShowIf("CheckEnemyFact")]
        public BlueprintUnitFactReference m_CheckedFact;

        public bool OnlyNaturalAttacks;

        public bool CheckWeaponRangeType;

        [ShowIf("CheckWeaponRangeType")]
        public WeaponRangeType RangeType;

        public BlueprintWeaponType WeaponType => m_WeaponType?.Get();

        public BlueprintUnitFact CheckedFact => m_CheckedFact?.Get();

        public void OnEventAboutToTrigger(RuleDealDamage evt) {
            RuleAttackWithWeapon ruleAttackWithWeapon = evt.Reason.Rule as RuleAttackWithWeapon;
            if (ruleAttackWithWeapon != null && CheckCondition(ruleAttackWithWeapon)) {
                evt.IgnoreDamageReduction = true;
            }
        }

        public void OnEventDidTrigger(RuleDealDamage evt) {
        }

        public bool CheckCondition(RuleAttackWithWeapon evt) {
            ItemEntity itemEntity = (base.Fact as ItemEnchantment)?.Owner;
            if (itemEntity != null && itemEntity != evt.Weapon) {
                return false;
            }

            if (CheckWeaponRangeType && !RangeType.IsSuitableWeapon(evt.Weapon)) {
                return false;
            }

            if ((bool)WeaponType && WeaponType != evt.Weapon.Blueprint.Type) {
                return false;
            }

            if (CriticalHit && (!evt.AttackRoll.IsCriticalConfirmed || evt.AttackRoll.FortificationNegatesCriticalHit)) {
                return false;
            }

            if (OnlyOnFullAttack && !evt.IsFullAttack) {
                return false;
            }

            if (OnlyOnFirstAttack && !evt.IsFirstAttack) {
                return false;
            }

            if (CheckEnemyFact && !evt.Target.Descriptor.HasFact(CheckedFact)) {
                return false;
            }

            if (OnlyNaturalAttacks && !evt.Weapon.Blueprint.IsNatural) {
                return false;
            }

            return true;
        }
    }
}
