using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Classes.ClassFeaturesDreadKnight {
    internal class AuraOfSin {


        public static void AddAuraOfSinFeature() {
            var AOSIcon = AssetLoader.LoadInternal("Skills", "Icon_AOS.png");
            var AuraOfSinEffectBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfSinEffectBuff", bp => {

                bp.SetName("Aura of Sin");
                bp.SetDescription("At 14th level, an antipaladin’s weapons are treated as evil-aligned for the purposes of overcoming damage " +
                    "reduction. Any attack made against an enemy within 10 feet of him is treated as evil-aligned for the purposes of overcoming damage " +
                    "reduction. This ability functions only while the antipaladin is conscious, not if he is unconscious or dead.");

                bp.m_Icon = AOSIcon;
                bp.IsClassFeature = true;               
                bp.AddComponent<AddIncomingDamageWeaponProperty>(c => {
                    c.Material = Kingmaker.Enums.Damage.PhysicalDamageMaterial.Adamantite;
                    c.AddAlignment = true;
                    c.Alignment = Kingmaker.Enums.Damage.DamageAlignment.Evil;
                    c.Reality = Kingmaker.Enums.Damage.DamageRealityType.Ghost;
                });
            });


            var AuraOfSinArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("AuraOfDespairArea", bp => {

                bp.AggroEnemies = true;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = new Feet() { m_Value = 13 };
                bp.AddComponent<AbilityAreaEffectBuff>(c => {
                    c.m_Buff = AuraOfSinEffectBuff.ToReference<BlueprintBuffReference>();
                    c.Condition = new ConditionsChecker() {

                        Conditions = new Condition[] {
                            new ContextConditionIsEnemy()
                        }

                    };

                });
            });

            var AuraOfSinBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfSinBuff", bp => {
                bp.SetName("Aura of Sin");
                bp.SetDescription("At 14th level, an antipaladin’s weapons are treated as evil-aligned for the purposes of overcoming damage " +
                    "reduction. Any attack made against an enemy within 10 feet of him is treated as evil-aligned for the purposes of overcoming damage " +
                    "reduction. This ability functions only while the antipaladin is conscious, not if he is unconscious or dead.");
                bp.m_Icon = AOSIcon;

                bp.IsClassFeature = true;
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = AuraOfSinArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
            });

            var AuraOfSinFeature = Helpers.CreateBlueprint<BlueprintFeature>("AuraOfSinFeature", bp => {

                bp.SetName("Aura of Sin");
                bp.SetDescription("At 14th level, an antipaladin’s weapons are treated as evil-aligned for the purposes of overcoming damage " +
                    "reduction. Any attack made against an enemy within 10 feet of him is treated as evil-aligned for the purposes of overcoming damage " +
                    "reduction. This ability functions only while the antipaladin is conscious, not if he is unconscious or dead.");
                bp.m_Icon = AOSIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<AuraFeatureComponent>(c => {
                    c.m_Buff = AuraOfSinBuff.ToReference<BlueprintBuffReference>();
                });

            });
        }
    }
}
    
    

