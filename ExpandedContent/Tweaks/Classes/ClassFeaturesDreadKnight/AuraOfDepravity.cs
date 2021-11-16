using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
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
    internal class AuraOfDepravity {


        public static void AddAuraOfDepravityFeature() {

            var AODepIcon = AssetLoader.LoadInternal("Skills", "Icon_AODepravity.png");
            var AuraOfDepravityEffectBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfDepravityEffectBuff", bp => {

                bp.SetName("Aura of Depravity");
                bp.SetDescription("At 17th level, a Dread Knight gains DR 5/good and immunity to compulsion " +
                    "spells and spell-like abilities.Each enemy within 10 feet takes a –4 penalty on saving throws against compulsion effects. " +
                    "This ability functions only while the antipaladin is conscious, not if he is unconscious or dead.");

                bp.IsClassFeature = true;
                bp.m_Icon = AODepIcon;
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.m_DisablingFeature = null;
                    c.SpellDescriptor = SpellDescriptor.Compulsion;
                    c.ModifierDescriptor = Kingmaker.Enums.ModifierDescriptor.Penalty;
                    c.Value = -4;
                });
            });


            var AuraOfDepravityArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("AuraOfDepravityArea", bp => {

                bp.AggroEnemies = true;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = new Feet() { m_Value = 13 };
                bp.AddComponent<AbilityAreaEffectBuff>(c => {
                    c.m_Buff = AuraOfDepravityEffectBuff.ToReference<BlueprintBuffReference>();
                    c.Condition = new ConditionsChecker() {

                        Conditions = new Condition[] {
                            new ContextConditionIsEnemy() {Not = true},
                            
                        }

                    };

                });
            });

            var AuraOfDepravityBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfDepravityBuff", bp => {
                bp.SetName("Aura of Depravity");
                bp.SetDescription("At 17th level, a Dread Knight gains DR 5/good and immunity to compulsion " +
                    "spells and spell-like abilities. Each enemy within 10 feet takes a –4 penalty on saving throws against compulsion effects. " +
                    "This ability functions only while the antipaladin is conscious, not if he is unconscious or dead.");
                bp.m_Icon = AODepIcon;
  
                bp.IsClassFeature = true;
                bp.AddComponent<AddAreaEffect>(c => {
                    c.m_AreaEffect = AuraOfDepravityArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
            });

            var AuraOfDepravityFeature = Helpers.CreateBlueprint<BlueprintFeature>("AuraOfDepravityFeature", bp => {

                bp.SetName("Aura of Depravity");
                bp.SetDescription("At 17th level, a Dread Knight gains DR 5/good and immunity to compulsion " +
                    "spells and spell-like abilities. Each enemy within 10 feet takes a –4 penalty on saving throws against compulsion effects. " +
                    "This ability functions only while the antipaladin is conscious, not if he is unconscious or dead.");
                bp.m_Icon = AODepIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<BuffDescriptorImmunity>(c => {
                    c.m_IgnoreFeature = null;
                    c.m_FactToCheck = null;
                    c.Descriptor = SpellDescriptor.Compulsion;

                });
                bp.AddComponent<AuraFeatureComponent>(c => {
                    c.m_Buff = AuraOfDepravityBuff.ToReference<BlueprintBuffReference>();
                });

                bp.AddComponent<AddDamageResistancePhysical>(c => {
                    c.m_WeaponType = null;
                    c.Material = Kingmaker.Enums.Damage.PhysicalDamageMaterial.Adamantite;
                    c.MinEnhancementBonus = 1;
                    c.BypassedByAlignment = true;
                    c.Alignment = Kingmaker.Enums.Damage.DamageAlignment.Good;
                    c.Reality = Kingmaker.Enums.Damage.DamageRealityType.Ghost;
                    c.m_CheckedFactMythic = null;
                    c.Value = 10;
                    c.Pool = 12;
                });
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c => {
                    c.m_CasterIgnoreImmunityFact = null;
                    c.Descriptor = SpellDescriptor.Compulsion;
                });

            });
        }
    }
}
    