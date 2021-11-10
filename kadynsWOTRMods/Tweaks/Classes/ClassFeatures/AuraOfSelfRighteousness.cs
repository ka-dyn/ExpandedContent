using kadynsWOTRMods.Extensions;
using kadynsWOTRMods.Utilities;
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

namespace kadynsWOTRMods.Tweaks.Classes.ClassFeatures
{
    internal class AuraOfSelfRighteousness
    {
        private static SpellDescriptorWrapper Compulsion;

        public static void AddAuraOfSelfRighteousnessFeature()
        {
            var AOSRIcon = AssetLoader.LoadInternal("Skills", "Icon_AOSR.png");
            var AuraOfSelfRighteousnessEffectBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfSelfRighteousnessEffectBuff", bp => {

                bp.SetName("Aura of Self-Righteousness");
                bp.SetDescription("At 17th level, an Oathbreaker gains DR 5/lawful or good and immunity to compulsion " +
                        "spells and spell-like abilities. Each ally within 10 feet of her gains a +4 morale bonus on saving throws " +
                        "against compulsion effects. Aura of Self-Righteousness functions only while the Oathbreaker is conscious, " +
                        "not if she is unconscious or dead.");
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.m_Icon = AOSRIcon;
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.m_DisablingFeature = null;
                    c.SpellDescriptor = Compulsion;
                    c.ModifierDescriptor = Kingmaker.Enums.ModifierDescriptor.Morale;
                    c.Value = 4;
                });
            });


            var AuraOfSelfRighteousnessArea = Helpers.CreateBlueprint<BlueprintAbilityAreaEffect>("AuraOfSelfRighteousnessArea", bp => {
                
                bp.AggroEnemies = true;
                bp.Shape = AreaEffectShape.Cylinder;
                bp.Size = new Feet() { m_Value = 13 };
                bp.AddComponent<AbilityAreaEffectBuff>(c => {
                    c.m_Buff = AuraOfSelfRighteousnessEffectBuff.ToReference<BlueprintBuffReference>();
                    c.Condition = new ConditionsChecker() {

                        Conditions = new Condition[] {
                            new ContextConditionIsAlly()
                        }

                    };

                });
            });
            
            var AuraOfSelfRighteousnessBuff = Helpers.CreateBlueprint<BlueprintBuff>("AuraOfSelfRighteousnessBuff", bp =>
            {
                bp.SetName("Aura of Self-Righteousness");
                bp.SetDescription("At 17th level, an Oathbreaker gains DR 5/lawful or good and immunity to compulsion " +
                    "spells and spell-like abilities. Each ally within 10 feet of her gains a +4 morale bonus on saving throws " +
                    "against compulsion effects. Aura of Self-Righteousness functions only while the Oathbreaker is conscious, " +
                    "not if she is unconscious or dead.");
                bp.m_Icon = AOSRIcon;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.AddComponent<AddAreaEffect>(c =>
                {
                    c.m_AreaEffect = AuraOfSelfRighteousnessArea.ToReference<BlueprintAbilityAreaEffectReference>();
                });
            });

            var AuraOfSelfRighteousnessFeature = Helpers.CreateBlueprint<BlueprintFeature>("AuraOfSelfRighteousnessFeature", bp =>
            {

                bp.SetName("Aura of Self-Righteousness");
                bp.SetDescription("At 17th level, an Oathbreaker gains DR 10/Good and immunity to compulsion " +
    "spells and spell-like abilities. Each ally within 10 feet of her gains a +4 morale bonus on saving throws " +
    "against compulsion effects. Aura of Self-Righteousness functions only while the Oathbreaker is conscious, " +
    "not if she is unconscious or dead.");
                bp.m_Icon = AOSRIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<BuffDescriptorImmunity>(c =>
                {
                    c.m_IgnoreFeature = null;
                    c.m_FactToCheck = null;
                    c.Descriptor = Compulsion;

                });
                bp.AddComponent<AuraFeatureComponent>(c =>
                {
                    c.m_Buff = AuraOfSelfRighteousnessBuff.ToReference<BlueprintBuffReference>();
                });

                bp.AddComponent<AddDamageResistancePhysical>(c =>
                {
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
                bp.AddComponent<SpellImmunityToSpellDescriptor>(c =>
                {
                    c.m_CasterIgnoreImmunityFact = null;
                    c.Descriptor = Compulsion;
                });

            });
        }
    }
}
    
    
