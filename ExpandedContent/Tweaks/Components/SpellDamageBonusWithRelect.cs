using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Enums.Damage;
using Kingmaker.Items;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.PubSubSystem;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Mechanics;

namespace ExpandedContent.Tweaks.Components {//No idea if this works
    public class SpellDamageBonusWithRelect : UnitBuffComponentDelegate, ITargetRulebookHandler<RuleCalculateDamage>, IRulebookHandler<RuleCalculateDamage>, IInitiatorRulebookSubscriber {

        public SpellDescriptorWrapper descriptor;
        public bool change_damage_type = false;
        public DamageEnergyType damage_type = DamageEnergyType.Holy;
        public bool reflect_damage = false;
        public bool change_reflect_damage_type = false;
        public DamageEnergyType reflect_damage_type = DamageEnergyType.Holy;
        public bool only_spells = true;
        public bool remove_self_on_apply = false;
        public bool only_from_caster = true;


        public void OnEventDidTrigger(RuleCalculateDamage evt) {
            var damage = evt.DamageBundle.FirstOrDefault();
            if (damage == null) {
                return;
            }

            var context2 = evt.Reason.Context.SourceAbilityContext;
            if (context2 == null) {
                return;
            }
            if (context2.MaybeCaster != this.Fact.MaybeContext.MaybeCaster && only_from_caster) {
                return;
            }
            if ((context2.Ability.Blueprint.Type != AbilityType.Spell || (context2.SpellDescriptor & descriptor) == 0) && only_spells) {
                return;
            }
            BaseDamage new_damage = damage.Copy();
            if (change_damage_type) {
                new_damage = new EnergyDamage(damage.Dice, 0, damage_type);                
            }
            new_damage.Half.Set(value: true, base.Fact);
            new_damage.SourceFact = this.Fact;
            evt.DamageBundle.Append(new_damage);

            var calcualted_damage = evt.CalculatedDamage.FirstOrDefault();
            evt.CalculatedDamage.Add(new DamageValue(new_damage, calcualted_damage.ValueWithoutReduction / 2, calcualted_damage.RollAndBonusValue / 2, calcualted_damage.RollResult /2, calcualted_damage.TacticalCombatDRModifier));
            if (reflect_damage) {
                var reflected_damage = damage.Copy();
                if (change_reflect_damage_type) {
                    reflected_damage = new EnergyDamage(new DiceFormula(calcualted_damage.ValueWithoutReduction / 2, DiceType.One), reflect_damage_type);

                }
                RuleDealDamage evt_dmg = new RuleDealDamage(evt.Initiator, evt.Initiator, new DamageBundle(reflected_damage));
                Rulebook.Trigger<RuleDealDamage>(evt_dmg);

            }

            if (remove_self_on_apply) {
                this.Buff.Remove();
            }
        }

        public void OnEventAboutToTrigger(RuleCalculateDamage evt) {
        }
    }
}
