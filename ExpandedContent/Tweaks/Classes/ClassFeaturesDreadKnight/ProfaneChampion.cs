using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Classes.ClassFeaturesDreadKnight {
    internal class ProfaneChampion {

        public static void AddProfaneChampion() {

            var ProfaneChampIcon = AssetLoader.LoadInternal("Skills", "Icon_ProfaneChamp.png");
            var DreadKnightChannelNegativeEnergyAbility = Resources.GetModBlueprint<BlueprintAbility>("DreadKnightChannelNegativeEnergyAbility");
            var ChannelTouchOfProfaneCorruptionAbility = Resources.GetModBlueprint<BlueprintAbility>("ChannelTouchOfProfaneCorruptionAbility");
            var TouchOfProfaneCorruptionAbility = Resources.GetModBlueprint<BlueprintAbility>("TouchOfProfaneCorruptionAbility");
            var SinfulAbsolutionResource = Resources.GetModBlueprint<BlueprintAbilityResource>("SinfulAbsolutionResource");
            var TouchOfProfaneCorruptionResource = Resources.GetModBlueprint<BlueprintAbilityResource>("TouchOfProfaneCorruptionResource");
            var ProfaneChampion = Helpers.CreateBlueprint<BlueprintFeature>("ProfaneChampion", bp => {
                bp.SetName("Profane Champion");
                bp.SetDescription("At 20th level, a Dread Knight becomes a conduit for the might of dark powers. Her DR increases to 10/good. " +
                    "\nThe Dread Knight gains an additional 3 uses of sinful absolution and touch of corruption. In addition, whenever she channels negative energy or uses " +
                    "touch of corruption to damage a creature, she deals the maximum possible amount. ");  
                bp.m_Icon = ProfaneChampIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.m_WeaponType = null;
                    c.Material = Kingmaker.Enums.Damage.PhysicalDamageMaterial.Adamantite;
                    c.MinEnhancementBonus = 1;
                    c.BypassedByAlignment = true;
                    c.Alignment = Kingmaker.Enums.Damage.DamageAlignment.Good;
                    c.Reality = Kingmaker.Enums.Damage.DamageRealityType.Ghost;
                    c.m_CheckedFactMythic = null;
                    c.Value = 5;
                    c.Pool = 12;
                });
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = TouchOfProfaneCorruptionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 3;
                });
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = SinfulAbsolutionResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 3;
                });
                bp.AddComponent<AutoMetamagic>(c => {
                    c.m_AllowedAbilities = AutoMetamagic.AllowedType.Any;
                    c.Metamagic = Kingmaker.UnitLogic.Abilities.Metamagic.Maximize;
                    c.Abilities = new List<BlueprintAbilityReference> {
                        DreadKnightChannelNegativeEnergyAbility.ToReference<BlueprintAbilityReference>(),
                        ChannelTouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>(),
                        TouchOfProfaneCorruptionAbility.ToReference<BlueprintAbilityReference>()
                    };
                });


                    
            });
        }
    }
}
            
            


