using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Spirits.Hexes {
    internal class EnchancedCuresHex {
        public static void AddEnchancedCuresHex() {

            var ShamanClass = Resources.GetBlueprint<BlueprintCharacterClass>("145f1d3d360a7ad48bd95d392c81b38e");

            var CureLightWounds = Resources.GetBlueprint<BlueprintAbility>("47808d23c67033d4bbab86a1070fd62f");
            var CureLightWoundsCast = Resources.GetBlueprintReference<BlueprintAbilityReference>("5590652e1c2225c4ca30c4a699ab3649");
            var CureLightWoundsDamage = Resources.GetBlueprintReference<BlueprintAbilityReference>("1edd1e201a608a24fa1de33d57502244");
            var CureModerateWounds = Resources.GetBlueprintReference<BlueprintAbilityReference>("1c1ebf5370939a9418da93176cc44cd9");
            var CureModerateWoundsCast = Resources.GetBlueprintReference<BlueprintAbilityReference>("6b90c773a6543dc49b2505858ce33db5");
            var CureModerateWoundsDamage = Resources.GetBlueprintReference<BlueprintAbilityReference>("148673963b23fae4f9fcdcc5d67a91cc");
            var CureSeriousWounds = Resources.GetBlueprintReference<BlueprintAbilityReference>("6e81a6679a0889a429dec9cedcf3729c");
            var CureSeriousWoundsCast = Resources.GetBlueprintReference<BlueprintAbilityReference>("3361c5df793b4c8448756146a88026ad");
            var CureSeriousWoundsDamage = Resources.GetBlueprintReference<BlueprintAbilityReference>("dd5d65e25a4e8b54a87d976c0a80f5b6");
            var CureCriticalWounds = Resources.GetBlueprintReference<BlueprintAbilityReference>("0d657aa811b310e4bbd8586e60156a2d");
            var CureCriticalWoundsCast = Resources.GetBlueprintReference<BlueprintAbilityReference>("41c9016596fe1de4faf67425ed691203");
            var CureCriticalWoundsDamage = Resources.GetBlueprintReference<BlueprintAbilityReference>("dd5d65e25a4e8b54a87d976c0a80f5b6");
            var CureLightWoundsMass = Resources.GetBlueprintReference<BlueprintAbilityReference>("5d3d689392e4ff740a761ef346815074");
            var CureLightWoundsMassDamage = Resources.GetBlueprintReference<BlueprintAbilityReference>("fb7e5fe8b5750f9408398d9659b0f98f");
            var CureModerateWoundsMass = Resources.GetBlueprintReference<BlueprintAbilityReference>("571221cc141bc21449ae96b3944652aa");
            var CureModerateWoundsMassDamage = Resources.GetBlueprintReference<BlueprintAbilityReference>("638363b5afb817d4684c021d36279904");
            var CureSeriousWoundsMass = Resources.GetBlueprintReference<BlueprintAbilityReference>("0cea35de4d553cc439ae80b3a8724397");
            var CureSeriousWoundsMassDamage = Resources.GetBlueprintReference<BlueprintAbilityReference>("21d02c685b2e64b4f852b3efcb0b5ca6");
            var CureCriticalWoundsMass = Resources.GetBlueprintReference<BlueprintAbilityReference>("1f173a16120359e41a20fc75bb53d449");

            var ShamanLifeSpiritProgression = Resources.GetBlueprint<BlueprintProgression>("f9ee5a9146561ba4bac4cb8a7126f85c");
            var ShamanLifeSpiritWanderingFeature = Resources.GetBlueprint<BlueprintFeature>("ea7b7bf33205d4b41a913c2a8487c7c7");
            var ShamanLifeSpiritBaseFeature = Resources.GetBlueprint<BlueprintProgression>("9ae3ba76663a19141a3996c1e3ce95e3");



            var ShamanHexEnchancedCuresFeature = Helpers.CreateBlueprint<BlueprintFeature>("ShamanHexEnchancedCuresFeature", bp => {
                bp.SetName("Enhanced Cures");
                bp.SetDescription("When the shaman casts a cure spell, the maximum number of hit points healed is based on her shaman level, " +
                    "not the limit imposed by the spell. For example an 11th-level shaman with this hex can cast cure light wounds to heal " +
                    "1d8+11 hit points instead of the normal 1d8+5 maximum.");
                bp.m_Icon = CureLightWounds.m_Icon;
                bp.AddComponent<AddUnlimitedSpell>(c => {
                    c.m_Abilities = new BlueprintAbilityReference[] {
                        CureLightWounds.ToReference<BlueprintAbilityReference>(),
                        CureLightWoundsCast,
                        CureLightWoundsDamage,
                        CureModerateWounds,
                        CureModerateWoundsCast,
                        CureModerateWoundsDamage,
                        CureSeriousWounds,
                        CureSeriousWoundsCast,
                        CureSeriousWoundsDamage,
                        CureCriticalWounds,
                        CureCriticalWoundsCast,
                        CureCriticalWoundsDamage,
                        CureLightWoundsMass,
                        CureLightWoundsMassDamage,
                        CureModerateWoundsMass,
                        CureModerateWoundsMassDamage,
                        CureSeriousWoundsMass,
                        CureSeriousWoundsMassDamage,
                        CureCriticalWoundsMass
                    };
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanLifeSpiritProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanLifeSpiritWanderingFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisiteFeature>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_Feature = ShamanLifeSpiritBaseFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.ShamanHex };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            SpiritTools.RegisterShamanHex(ShamanHexEnchancedCuresFeature);

            ShamanLifeSpiritProgression.IsPrerequisiteFor.Add(ShamanHexEnchancedCuresFeature.ToReference<BlueprintFeatureReference>());

        }
    }
}
