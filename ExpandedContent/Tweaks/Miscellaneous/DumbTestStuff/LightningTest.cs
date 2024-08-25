using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Components;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Miscellaneous.DumbTestStuff {
    internal class LightningTest {
        public static void AddLightningTest() {
            var HollowBladesIcon = AssetLoader.LoadInternal("Skills", "Icon_HollowBlades.jpg");

            var LightningTestBuff = Helpers.CreateBuff("LightningTestBuff", bp => {
                bp.SetName("LightningTest");
                bp.SetDescription("Hollow Blades lowers the momentum and density of the targets melee weapons the moment they would land an attack. All melee weapons used by the target deal damage" +
                    "as if they are one size category smaller than they actually are. For instance, a Large longsword normally deals {g|Encyclopedia:Dice}2d6{/g} points of damage, but it would " +
                    "instead deal 1d8 points of damage if effected by hollow blades.");
                bp.m_Icon = HollowBladesIcon;
                bp.AddComponent<SpellDamageBonusWithRelect>(c => {
                    c.descriptor = SpellDescriptor.Electricity;
                    c.change_damage_type = true;
                    c.damage_type = Kingmaker.Enums.Damage.DamageEnergyType.Divine;
                    c.reflect_damage = true;
                    c.change_reflect_damage_type = true;
                    c.reflect_damage_type = Kingmaker.Enums.Damage.DamageEnergyType.Electricity;
                    c.only_spells = true;
                    c.remove_self_on_apply = true;
                    c.only_from_caster = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = false;
                bp.m_Flags = BlueprintBuff.Flags.RemoveOnRest;
                bp.Stacking = StackingType.Replace;
            });
        }

    }
}
