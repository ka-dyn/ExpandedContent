using System;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.Items;
using Kingmaker.Items.Slots;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.FactLogic;
using UnityEngine;

namespace ExpandedContent.Tweaks.Components {
    // If you need to add more just add a new restriction type
    [ComponentName("New types of Deflect Arrows")]
    [AllowedOn(typeof(BlueprintUnitFact))]

    public class DeflectArrowsExpanded : UnitFactComponentDelegate<DeflectArrowsExpanded.ComponentData>, IUnitEquipmentHandler, IGlobalSubscriber, ISubscriber, IUnitActiveEquipmentSetHandler {
        public override void OnTurnOn() {
            this.UpdateFeature();
        }
        public override void OnTurnOff() {
            if (base.Data.FeatureEnabled) {
                base.Owner.State.Features.DeflectArrows.Release();
                base.Data.FeatureEnabled = false;
            }
        }
        public void UpdateFeature() {
            bool flag = this.CheckRestriction();
            if (flag && !base.Data.FeatureEnabled) {
                base.Owner.State.Features.DeflectArrows.Retain();
                base.Data.FeatureEnabled = true;
                return;
            }
            if (!flag && base.Data.FeatureEnabled) {
                base.Owner.State.Features.DeflectArrows.Release();
                base.Data.FeatureEnabled = false;
            }
        }
        public bool CheckRestriction() {
            if (!base.Owner.Body.HandsAreEnabled) {
                return false;
            }
            switch (this.m_Restriction) {
                case DeflectArrowsExpanded.RestrictionType.Bow:
                    return DeflectArrowsExpanded.IsWeaponABow(base.Owner.Body.PrimaryHand) || DeflectArrowsExpanded.IsWeaponABow(base.Owner.Body.SecondaryHand);
                case DeflectArrowsExpanded.RestrictionType.MonkWeapon:
                    return DeflectArrowsExpanded.IsWeaponAMonkWeapon(base.Owner.Body.PrimaryHand) || DeflectArrowsExpanded.IsWeaponAMonkWeapon(base.Owner.Body.SecondaryHand);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public static bool IsWeaponABow(HandSlot hand) {
            return hand.MaybeWeapon != null && (hand.MaybeWeapon.Blueprint.Category == WeaponCategory.Longbow || hand.MaybeWeapon.Blueprint.Category == WeaponCategory.Shortbow);
        }
        public static bool IsWeaponAMonkWeapon(HandSlot hand) {
            return hand.MaybeWeapon != null && hand.MaybeWeapon.Blueprint.IsMonk;
        }
        public void HandleEquipmentSlotUpdated(ItemSlot slot, ItemEntity previousItem) {
            if (slot.Owner == base.Owner && slot is HandSlot) {
                this.UpdateFeature();
            }
        }
        public void HandleUnitChangeActiveEquipmentSet(UnitDescriptor unit) {
            if (unit == base.Owner) {
                this.UpdateFeature();
            }
        }

        [SerializeField]
        public DeflectArrowsExpanded.RestrictionType m_Restriction;

        public class ComponentData {
            public bool FeatureEnabled;
        }

        public enum RestrictionType {
            Bow,
            MonkWeapon
        }
    }
}
