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
using kadynsWOTRMods.Extensions;

namespace kadynsWOTRMods.Tweaks.Classes.ClassFeatures
{
     class OathbreakersBaneBuff {
        private static readonly BlueprintFeature SmiteEvilFeature = Resources.GetBlueprint<BlueprintFeature>("3a6db57fce75b0244a6a5819528ddf26");
        private static readonly BlueprintAbility SmiteEvilAbility = Resources.GetBlueprint<BlueprintAbility>("7bb9eb2042e67bf489ccd1374423cdec");
        private static readonly BlueprintBuff SmiteEvilBuff = Resources.GetBlueprint<BlueprintBuff>("b6570b8cbb32eaf4ca8255d0ec3310b0");

        public static void AddOathbreakersBaneBuff() {
            var OBBaneIcon = AssetLoader.LoadInternal("Skills", "Icon_OBBane.png");
            var SmiteEvilBuff = Resources.GetBlueprint<BlueprintBuff>("b6570b8cbb32eaf4ca8255d0ec3310b0");
            var OathbreakersBaneBuff = Helpers.CreateBlueprint<BlueprintBuff>("OathbreakersBaneBuff", bp => {
                bp.SetName("Oathbreaker's Bane");
                bp.SetDescription("An Oathbreaker is particularly ruthless against those who have harmed her or her allies. " +
              "Once per day as a swift action, she can channel her anger at one target within sight who is engaged in melee combat with her or an ally. " +
              "She adds her Charisma modifier to her attack rolls and adds her Oathbreaker level to damage rolls against the target of her bane. " +
              "In addition, while Oathbreaker's Bane is in effect, the Oathbreaker gains a deflection bonus equal to her Charisma bonus (if any) " +
              "to her AC against attacks by the target of the smite. If the target of Oathbreaker's Bane has rendered an ally of the Oathbreaker " +
              "unconscious or dead within the last 24 hours, the bonus on damage rolls on the first attack that hits increases by 2 for every paladin " +
              "level she has. The Oathbreaker's Bane effect remains until the target of the Oathbreaker's Bane is dead or the next time the Oathbreaker rests " +
              "and regains her uses of this ability.At 4th level and every 3 levels thereafter, the Oathbreaker can invoke her Oathbreaker's Bane " +
              "one additional time per day, to a maximum of seven times per day at 19th level." +
              "This replaces smite evil.");
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Stack;
                bp.m_Icon = OBBaneIcon;
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
