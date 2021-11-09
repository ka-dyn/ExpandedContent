using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Designers.Mechanics.Facts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Abilities;
using kadynsWOTRMods;
using kadynsWOTRMods.Utilities;

namespace kadynsTweaks.NewWIP.VindictiveBastard
{
     class VindictiveBastardVindictiveSmiteBuff {
        private static readonly BlueprintFeature SmiteEvilFeature = Resources.GetBlueprint<BlueprintFeature>("3a6db57fce75b0244a6a5819528ddf26");
        private static readonly BlueprintAbility SmiteEvilAbility = Resources.GetBlueprint<BlueprintAbility>("7bb9eb2042e67bf489ccd1374423cdec");
        private static readonly BlueprintBuff SmiteEvilBuff = Resources.GetBlueprint<BlueprintBuff>("b6570b8cbb32eaf4ca8255d0ec3310b0");

        public static void AddVindictiveSmiteBuff() {
            var SmiteEvilBuff = Resources.GetBlueprint<BlueprintBuff>("b6570b8cbb32eaf4ca8255d0ec3310b0");
            var VindictiveBastardVindictiveSmiteBuff = Helpers.CreateBlueprint("VindictiveBastardVindictiveSmiteBuff", bp => {
                bp.SetName("Vindictive Smite");
                bp.SetDescription("A vindictive bastard is particularly ruthless against those who have harmed her or her allies. " +
              "Once per day as a swift action, she can smite one target within sight who is engaged in melee combat with her or an ally. " +
              "She adds her Charisma modifier to her attack rolls and adds her paladin level to damage rolls against the target of her smite. " +
              "In addition, while vindictive smite is in effect, the vindictive bastard gains a deflection bonus equal to her Charisma bonus (if any) " +
              "to her AC against attacks by the target of the smite. If the target of vindictive smite has rendered an ally of the vindictive bastard " +
              "unconscious or dead within the last 24 hours, the bonus on damage rolls on the first attack that hits increases by 2 for every paladin " +
              "level she has. The vindictive smite effect remains until the target of the smite is dead or the next time the vindictive bastard rests " +
              "and regains her uses of this ability.At 4th level and every 3 levels thereafter, the vindictive bastard can invoke her vindictive smite " +
              "one additional time per day, to a maximum of seven times per day at 19th level." +
              "This replaces smite evil.");
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Stack;
                bp.m_Icon = SmiteEvilAbility.Icon;
                bp.ResourceAssetIds = SmiteEvilBuff.ResourceAssetIds; ;
                bp.FxOnStart = SmiteEvilBuff.FxOnStart;
                bp.FxOnRemove = SmiteEvilBuff.FxOnRemove;
                bp.AddComponent<AttackBonusAgainstTarget>(c =>
                {
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = new ContextValue()
                    {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.StatBonus
                    };
                    c.CheckCaster = true;
                });
                bp.AddComponent<DamageBonusAgainstTarget>(c =>
                {
                    c.CheckCaster = true;
                    c.ApplyToSpellDamage = true;
                    c.Value = new ContextValue()
                    {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.DamageBonus
                    };
                });
                bp.AddComponent<ACBonusAgainstTarget>(c =>
                {
                    c.Descriptor = ModifierDescriptor.Deflection;
                    c.Value = new ContextValue()
                    {
                        ValueType = ContextValueType.Shared,
                        ValueShared = AbilitySharedValue.StatBonus
                    };
                    c.CheckCaster = true;

                });

            });

        }
    }
}
