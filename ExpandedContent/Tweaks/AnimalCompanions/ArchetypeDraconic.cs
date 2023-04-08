using ExpandedContent.Config;
using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Classes.DrakeClass;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Root.Strings;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;

namespace ExpandedContent.Tweaks.AnimalCompanions {
    internal class ArchetypeDraconic {
        public static void AddArchetypeDraconic() {

            var AnimalCompanionClass = Resources.GetBlueprint<BlueprintCharacterClass>("26b10d4340839004f960f9816f6109fe");
            var EvasionFeature = Resources.GetBlueprint<BlueprintFeature>("576933720c440aa4d8d42b0c54b77e80");
            var AnimalCompanionDevotionFeature = Resources.GetBlueprint<BlueprintFeature>("226f939b7dfd47b4697ec52f79799012");
            var ImprovedEvasionFeature = Resources.GetBlueprint<BlueprintFeature>("ce96af454a6137d47b9c6a1e02e66803");
            var MultiattackFeature = Resources.GetBlueprint<BlueprintFeature>("8ac319e47057e2741b42229210eb43ed");
            var ImmunityToSleep = Resources.GetBlueprint<BlueprintFeature>("c263f44f72df009489409af122b5eefc");
            var ImmunityToParalysis= Resources.GetBlueprint<BlueprintFeature>("4b152a7bc5bab5042b437b955fea46cd");
            var BloodLineElementalFireResistance= Resources.GetBlueprint<BlueprintFeature>("24980315c1bdcc4478ebb717e9b81961");
            var BloodlineDraconicRedBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("3f31704e595e78942b3640cdc9b95d8b");
            var BloodlineDraconicWhiteBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("84be529914c90664aa948d8266bb3fa6");
            var BloodlineDraconicBlueBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("60a3047f434f38544a2878c26955d3ad");
            var BloodlineDraconicBlackBreathWeaponAbility = Resources.GetBlueprint<BlueprintAbility>("1e65b0b2db777e24db96d8bc52cc9207");

            var DraconicArchetype = Helpers.CreateBlueprint<BlueprintArchetype>("DraconicArchetype", bp => {
                bp.LocalizedName = Helpers.CreateString($"DraconicArchetype.Name", "Draconic");
                bp.LocalizedDescription = Helpers.CreateString($"DraconicArchetype.Description", "Draconic companions bear a faint trace of dragon blood that grants them special abilities.");
                bp.LocalizedDescriptionShort = Helpers.CreateString($"DraconicArchetype.Description", "Draconic companions bear a faint trace of dragon blood that grants them special abilities.");

            });
            var DraconicSightAnimalCompanionFeature = Helpers.CreateBlueprint<BlueprintFeature>("DraconicSightAnimalCompanionFeature", bp => {
                bp.SetName("Draconic Sight");
                bp.SetDescription("A draconic companion has better vision than normal for it's kind and gains a +2 to perception checks.");
                bp.AddComponent<AddStatBonus>(c => {
                    c.Stat = StatType.SkillPerception;
                    c.Descriptor = ModifierDescriptor.UntypedStackable;
                    c.Value = 2;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });

                bp.IsClassFeature = true;
            });


            var DraconicResistanceAnimalCompanionFeatureFire = Helpers.CreateBlueprint<BlueprintFeature>("DraconicResistanceAnimalCompanionFeatureFire", bp => {
                bp.SetName("Draconic Resistance - Fire");
                bp.SetDescription("");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Fire;
                    c.Value = 5;
                });
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.Paralysis | SpellDescriptor.Sleep;
                    c.ModifierDescriptor = ModifierDescriptor.Racial;
                    c.Value = 2;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var DraconicResistanceAnimalCompanionFeatureCold = Helpers.CreateBlueprint<BlueprintFeature>("DraconicResistanceAnimalCompanionFeatureCold", bp => {
                bp.SetName("Draconic Resistance - Cold");
                bp.SetDescription("");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Cold;
                    c.Value = 5;
                });
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.Paralysis | SpellDescriptor.Sleep;
                    c.ModifierDescriptor = ModifierDescriptor.Racial;
                    c.Value = 2;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var DraconicResistanceAnimalCompanionFeatureElectricity = Helpers.CreateBlueprint<BlueprintFeature>("DraconicResistanceAnimalCompanionFeatureElectricity", bp => {
                bp.SetName("Draconic Resistance - Air");
                bp.SetDescription("");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Electricity;
                    c.Value = 5;
                });
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.Paralysis | SpellDescriptor.Sleep;
                    c.ModifierDescriptor = ModifierDescriptor.Racial;
                    c.Value = 2;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var DraconicResistanceAnimalCompanionFeatureAcid = Helpers.CreateBlueprint<BlueprintFeature>("DraconicResistanceAnimalCompanionFeatureAcid", bp => {
                bp.SetName("Draconic Resistance - Earth");
                bp.SetDescription("");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Acid;
                    c.Value = 5;
                });
                bp.AddComponent<SavingThrowBonusAgainstDescriptor>(c => {
                    c.SpellDescriptor = SpellDescriptor.Paralysis | SpellDescriptor.Sleep;
                    c.ModifierDescriptor = ModifierDescriptor.Racial;
                    c.Value = 2;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var AnimalCompanionEnergyTypeFire = Helpers.CreateBlueprint<BlueprintFeature>("AnimalCompanionEnergyTypeFire", bp => {
                bp.SetName("Companion Energy Type - Fire");
                bp.SetDescription("At 3rd level, choose acid, cold, electricity, or fire, based on the draconic companion’s draconic ancestor. The draconic companion gains resistance " +
                    "5 against the chosen energy type, as well as a +2 racial bonus on saves against paralysis and sleep.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 6;
                    c.m_Feature = DraconicResistanceAnimalCompanionFeatureFire.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.IsClassFeature = true;
            });
            var AnimalCompanionEnergyTypeCold = Helpers.CreateBlueprint<BlueprintFeature>("AnimalCompanionEnergyTypeCold", bp => {
                bp.SetName("Companion Energy Type - Cold");
                bp.SetDescription("At 3rd level, choose acid, cold, electricity, or fire, based on the draconic companion’s draconic ancestor. The draconic companion gains resistance " +
                    "5 against the chosen energy type, as well as a +2 racial bonus on saves against paralysis and sleep.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 6;
                    c.m_Feature = DraconicResistanceAnimalCompanionFeatureCold.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.IsClassFeature = true;
            });
            var AnimalCompanionEnergyTypeElectricity = Helpers.CreateBlueprint<BlueprintFeature>("AnimalCompanionEnergyTypeElectricity", bp => {
                bp.SetName("Companion Energy Type - Electricity");
                bp.SetDescription("At 3rd level, choose acid, cold, electricity, or fire, based on the draconic companion’s draconic ancestor. The draconic companion gains resistance " +
                    "5 against the chosen energy type, as well as a +2 racial bonus on saves against paralysis and sleep.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 6;
                    c.m_Feature = DraconicResistanceAnimalCompanionFeatureElectricity.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.IsClassFeature = true;
            });
            var AnimalCompanionEnergyTypeAcid = Helpers.CreateBlueprint<BlueprintFeature>("AnimalCompanionEnergyTypeAcid", bp => {
                bp.SetName("Companion Energy Type - Acid");
                bp.SetDescription("At 3rd level, choose acid, cold, electricity, or fire, based on the draconic companion’s draconic ancestor. The draconic companion gains resistance " +
                    "5 against the chosen energy type, as well as a +2 racial bonus on saves against paralysis and sleep.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 6;
                    c.m_Feature = DraconicResistanceAnimalCompanionFeatureAcid.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.IsClassFeature = true;
            });
            var DraconicResistanceAnimalCompanionFeatureSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DraconicResistanceAnimalCompanionFeatureSelection", bp => {
                bp.SetName("Draconic Resistance");
                bp.SetDescription("At 3rd level, choose acid, cold, electricity, or fire, based on the draconic companion’s draconic ancestor. The draconic companion gains resistance " +
                    "5 against the chosen energy type, as well as a +2 racial bonus on saves against paralysis and sleep.");
                bp.m_Icon = BloodLineElementalFireResistance.m_Icon;
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.Group = FeatureGroup.None;
                bp.m_AllFeatures = new BlueprintFeatureReference[] {
                    AnimalCompanionEnergyTypeFire.ToReference<BlueprintFeatureReference>(),
                    AnimalCompanionEnergyTypeCold.ToReference<BlueprintFeatureReference>(),
                    AnimalCompanionEnergyTypeElectricity.ToReference<BlueprintFeatureReference>(),
                    AnimalCompanionEnergyTypeAcid.ToReference<BlueprintFeatureReference>()
                };
            });

            var ImprovedDraconicResistanceAnimalCompanionFire9 = Helpers.CreateBlueprint<BlueprintFeature>("ImprovedDraconicResistanceAnimalCompanionFire9", bp => {
                bp.SetName("Draconic Resistance - Fire");
                bp.SetDescription("");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Fire;
                    c.Value = 10;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var ImprovedDraconicResistanceAnimalCompanionFire15 = Helpers.CreateBlueprint<BlueprintFeature>("ImprovedDraconicResistanceAnimalCompanionFire15", bp => {
                bp.SetName("Draconic Resistance - Fire");
                bp.SetDescription("");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Fire;
                    c.Value = 20;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var ImprovedDraconicResistanceAnimalCompanionFire = Helpers.CreateBlueprint<BlueprintFeature>("ImprovedDraconicResistanceAnimalCompanionFire", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 15;
                    c.m_Feature = ImprovedDraconicResistanceAnimalCompanionFire9.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 15;
                    c.m_Feature = ImprovedDraconicResistanceAnimalCompanionFire15.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ImmunityToSleep.ToReference<BlueprintUnitFactReference>(),
                        ImmunityToParalysis.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var ImprovedDraconicResistanceAnimalCompanionCold9 = Helpers.CreateBlueprint<BlueprintFeature>("ImprovedDraconicResistanceAnimalCompanionCold9", bp => {
                bp.SetName("Draconic Resistance - Cold");
                bp.SetDescription("");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Cold;
                    c.Value = 10;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var ImprovedDraconicResistanceAnimalCompanionCold15 = Helpers.CreateBlueprint<BlueprintFeature>("ImprovedDraconicResistanceAnimalCompanionCold15", bp => {
                bp.SetName("Draconic Resistance - Cold");
                bp.SetDescription("");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Cold;
                    c.Value = 20;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var ImprovedDraconicResistanceAnimalCompanionCold = Helpers.CreateBlueprint<BlueprintFeature>("ImprovedDraconicResistanceAnimalCompanionCold", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 15;
                    c.m_Feature = ImprovedDraconicResistanceAnimalCompanionCold9.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 15;
                    c.m_Feature = ImprovedDraconicResistanceAnimalCompanionCold15.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ImmunityToSleep.ToReference<BlueprintUnitFactReference>(),
                        ImmunityToParalysis.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var ImprovedDraconicResistanceAnimalCompanionElectricity9 = Helpers.CreateBlueprint<BlueprintFeature>("ImprovedDraconicResistanceAnimalCompanionElectricity9", bp => {
                bp.SetName("Draconic Resistance - Electricity");
                bp.SetDescription("");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Electricity;
                    c.Value = 10;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var ImprovedDraconicResistanceAnimalCompanionElectricity15 = Helpers.CreateBlueprint<BlueprintFeature>("ImprovedDraconicResistanceAnimalCompanionElectricity15", bp => {
                bp.SetName("Draconic Resistance - Electricity");
                bp.SetDescription("");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Electricity;
                    c.Value = 20;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var ImprovedDraconicResistanceAnimalCompanionElectricity = Helpers.CreateBlueprint<BlueprintFeature>("ImprovedDraconicResistanceAnimalCompanionElectricity", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 15;
                    c.m_Feature = ImprovedDraconicResistanceAnimalCompanionElectricity9.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 15;
                    c.m_Feature = ImprovedDraconicResistanceAnimalCompanionElectricity15.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ImmunityToSleep.ToReference<BlueprintUnitFactReference>(),
                        ImmunityToParalysis.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var ImprovedDraconicResistanceAnimalCompanionAcid9 = Helpers.CreateBlueprint<BlueprintFeature>("ImprovedDraconicResistanceAnimalCompanionAcid9", bp => {
                bp.SetName("Draconic Resistance - Acid");
                bp.SetDescription("");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Acid;
                    c.Value = 10;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var ImprovedDraconicResistanceAnimalCompanionAcid15 = Helpers.CreateBlueprint<BlueprintFeature>("ImprovedDraconicResistanceAnimalCompanionAcid15", bp => {
                bp.SetName("Draconic Resistance - Acid");
                bp.SetDescription("");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Acid;
                    c.Value = 20;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var ImprovedDraconicResistanceAnimalCompanionAcid = Helpers.CreateBlueprint<BlueprintFeature>("ImprovedDraconicResistanceAnimalCompanionAcid", bp => {
                bp.SetName("");
                bp.SetDescription("");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 15;
                    c.m_Feature = ImprovedDraconicResistanceAnimalCompanionAcid9.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = true;
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 15;
                    c.m_Feature = ImprovedDraconicResistanceAnimalCompanionAcid15.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        ImmunityToSleep.ToReference<BlueprintUnitFactReference>(),
                        ImmunityToParalysis.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });
            var ImprovedDraconicResistanceAnimalCompanionFeature = Helpers.CreateBlueprint<BlueprintFeature>("ImprovedDraconicResistanceAnimalCompanionFeature", bp => {
                bp.SetName("Improved Draconic Resistance");
                bp.SetDescription("At 6th level, a draconic companion becomes immune to paralysis and sleep, and its energy resistance increases to 10. At 15th level, its energy " +
                    "resistance increases to 20.");
                bp.m_Icon = BloodLineElementalFireResistance.m_Icon;
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = AnimalCompanionEnergyTypeFire.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = ImprovedDraconicResistanceAnimalCompanionFire.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = AnimalCompanionEnergyTypeCold.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = ImprovedDraconicResistanceAnimalCompanionCold.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = AnimalCompanionEnergyTypeElectricity.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = ImprovedDraconicResistanceAnimalCompanionElectricity.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = AnimalCompanionEnergyTypeAcid.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = ImprovedDraconicResistanceAnimalCompanionAcid.ToReference<BlueprintUnitFactReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.IsClassFeature = true;
            });

            var DraconicCompanionBreathAbilityResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("DraconicCompanionBreathAbilityResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = false,
                    IncreasedByStat = false
                };                
            });
            var DraconicBreathAbilityCooldown = Helpers.CreateBuff("DraconicBreathAbilityCooldown", bp => {
                bp.m_AllowNonContextActions = false;
                bp.SetName("Breath Weapon - Ability is not ready yet");
                bp.SetDescription("");
                //bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });
            var FireCone30Feet00 = Resources.GetBlueprint<BlueprintProjectile>("acf144d4da2638e4eadde1bb9dac29b4");
            var ColdCone30Feet00 = Resources.GetBlueprint<BlueprintProjectile>("c202b61bf074a7442bf335b27721853f");
            var SonicCone30Feet00 = Resources.GetBlueprint<BlueprintProjectile>("c7fd792125b79904881530dbc2ff83de");
            var AcidCone30Feet00 = Resources.GetBlueprint<BlueprintProjectile>("155104dfdc285f3449610e625fa85729");
            var FireLine00 = Resources.GetBlueprint<BlueprintProjectile>("ecf79fc871f15074e95698a3fef47aee");
            var ColdLine00 = Resources.GetBlueprint<BlueprintProjectile>("df0464dbf5b83804d9980eb42ed37462");
            var LightningBolt00 = Resources.GetBlueprint<BlueprintProjectile>("c7734162c01abdc478418bfb286ed7a5");
            var AcidLine00 = Resources.GetBlueprint<BlueprintProjectile>("33af0c7694f8d734397bd03e6d4b72f1");

            var DraconicCompanionBreathAbilityFireCone = Helpers.CreateBlueprint<BlueprintAbility>("DraconicCompanionBreathAbilityFireCone", bp => {
                bp.SetName("Breath Weapon - Fire Cone");
                bp.SetDescription("At 9th level, a draconic companion gains a breath weapon, usable once per day, that deals 1d6 points of damage per Hit Dice of the energy type " +
                    "matching its draconic resistance in either a 30-foot cone or a 60-foot line (chosen when the draconic companion gains this ability). At 15th level, it can use the " +
                    "breath weapon three times per day, but it must wait 1d4 rounds between uses. Targets of this breath weapon can attempt a Reflex save (DC = 10 + half the draconic " +
                    "companion’s Hit Dice + the draconic companion’s Constitution modifier) for half damage.");
                bp.m_Icon = BloodlineDraconicRedBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        FireCone30Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 30 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = 0,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Fire
                            },
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
                            Duration = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true,
                            },
                            PreRolledSharedValue = AbilitySharedValue.Damage,
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 8,
                                    ValueRank = AbilityRankType.DamageDice,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DraconicBreathAbilityCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;                    
                    c.m_Class = new BlueprintCharacterClassReference[] {                        
                        AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DraconicBreathAbilityCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DraconicCompanionBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DraconicCompanionBreathAbilityFireLine= Helpers.CreateBlueprint<BlueprintAbility>("DraconicCompanionBreathAbilityFireLine", bp => {
                bp.SetName("Breath Weapon - Fire Line");
                bp.SetDescription("At 9th level, a draconic companion gains a breath weapon, usable once per day, that deals 1d6 points of damage per Hit Dice of the energy type " +
                    "matching its draconic resistance in either a 30-foot cone or a 60-foot line (chosen when the draconic companion gains this ability). At 15th level, it can use the " +
                    "breath weapon three times per day, but it must wait 1d4 rounds between uses. Targets of this breath weapon can attempt a Reflex save (DC = 10 + half the draconic " +
                    "companion’s Hit Dice + the draconic companion’s Constitution modifier) for half damage.");
                bp.m_Icon = BloodlineDraconicRedBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        FireLine00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Line;
                    c.m_Length = new Feet() { m_Value = 60 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = 0,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Fire
                            },
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
                            Duration = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true,
                            },
                            PreRolledSharedValue = AbilitySharedValue.Damage,
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 8,
                                    ValueRank = AbilityRankType.DamageDice,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DraconicBreathAbilityCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DraconicBreathAbilityCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DraconicCompanionBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DraconicBreathWeaponAnimalCompanionFeatureFire = Helpers.CreateBlueprint<BlueprintFeature>("DraconicBreathWeaponAnimalCompanionFeatureFire", bp => {
                bp.SetName("Breath Weapon");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        DraconicCompanionBreathAbilityFireCone.ToReference<BlueprintUnitFactReference>(),
                        DraconicCompanionBreathAbilityFireLine.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DraconicCompanionBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] {
                        DraconicCompanionBreathAbilityFireCone.ToReference<BlueprintAbilityReference>(),
                        DraconicCompanionBreathAbilityFireLine.ToReference<BlueprintAbilityReference>()
                    };
                    c.Stat = StatType.Constitution;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var DraconicCompanionBreathAbilityColdCone = Helpers.CreateBlueprint<BlueprintAbility>("DraconicCompanionBreathAbilityColdCone", bp => {
                bp.SetName("Breath Weapon - Cold Cone");
                bp.SetDescription("At 9th level, a draconic companion gains a breath weapon, usable once per day, that deals 1d6 points of damage per Hit Dice of the energy type " +
                    "matching its draconic resistance in either a 30-foot cone or a 60-foot line (chosen when the draconic companion gains this ability). At 15th level, it can use the " +
                    "breath weapon three times per day, but it must wait 1d4 rounds between uses. Targets of this breath weapon can attempt a Reflex save (DC = 10 + half the draconic " +
                    "companion’s Hit Dice + the draconic companion’s Constitution modifier) for half damage.");
                bp.m_Icon = BloodlineDraconicWhiteBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        ColdCone30Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 30 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = 0,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Cold
                            },
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
                            Duration = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true,
                            },
                            PreRolledSharedValue = AbilitySharedValue.Damage,
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 8,
                                    ValueRank = AbilityRankType.DamageDice,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DraconicBreathAbilityCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DraconicBreathAbilityCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DraconicCompanionBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DraconicCompanionBreathAbilityColdLine = Helpers.CreateBlueprint<BlueprintAbility>("DraconicCompanionBreathAbilityColdLine", bp => {
                bp.SetName("Breath Weapon - Cold Line");
                bp.SetDescription("At 9th level, a draconic companion gains a breath weapon, usable once per day, that deals 1d6 points of damage per Hit Dice of the energy type " +
                    "matching its draconic resistance in either a 30-foot cone or a 60-foot line (chosen when the draconic companion gains this ability). At 15th level, it can use the " +
                    "breath weapon three times per day, but it must wait 1d4 rounds between uses. Targets of this breath weapon can attempt a Reflex save (DC = 10 + half the draconic " +
                    "companion’s Hit Dice + the draconic companion’s Constitution modifier) for half damage.");
                bp.m_Icon = BloodlineDraconicWhiteBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        ColdLine00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Line;
                    c.m_Length = new Feet() { m_Value = 60 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = 0,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Cold
                            },
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
                            Duration = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true,
                            },
                            PreRolledSharedValue = AbilitySharedValue.Damage,
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 8,
                                    ValueRank = AbilityRankType.DamageDice,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DraconicBreathAbilityCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DraconicBreathAbilityCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DraconicCompanionBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DraconicBreathWeaponAnimalCompanionFeatureCold = Helpers.CreateBlueprint<BlueprintFeature>("DraconicBreathWeaponAnimalCompanionFeatureCold", bp => {
                bp.SetName("Breath Weapon");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        DraconicCompanionBreathAbilityColdCone.ToReference<BlueprintUnitFactReference>(),
                        DraconicCompanionBreathAbilityColdLine.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DraconicCompanionBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] {
                        DraconicCompanionBreathAbilityColdCone.ToReference<BlueprintAbilityReference>(),
                        DraconicCompanionBreathAbilityColdLine.ToReference<BlueprintAbilityReference>()
                    };
                    c.Stat = StatType.Constitution;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var DraconicCompanionBreathAbilityElectricityCone = Helpers.CreateBlueprint<BlueprintAbility>("DraconicCompanionBreathAbilityElectricityCone", bp => {
                bp.SetName("Breath Weapon - Electricity Cone");
                bp.SetDescription("At 9th level, a draconic companion gains a breath weapon, usable once per day, that deals 1d6 points of damage per Hit Dice of the energy type " +
                    "matching its draconic resistance in either a 30-foot cone or a 60-foot line (chosen when the draconic companion gains this ability). At 15th level, it can use the " +
                    "breath weapon three times per day, but it must wait 1d4 rounds between uses. Targets of this breath weapon can attempt a Reflex save (DC = 10 + half the draconic " +
                    "companion’s Hit Dice + the draconic companion’s Constitution modifier) for half damage.");
                bp.m_Icon = BloodlineDraconicBlueBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        SonicCone30Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 30 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = 0,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Electricity
                            },
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
                            Duration = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true,
                            },
                            PreRolledSharedValue = AbilitySharedValue.Damage,
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 8,
                                    ValueRank = AbilityRankType.DamageDice,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DraconicBreathAbilityCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DraconicBreathAbilityCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DraconicCompanionBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DraconicCompanionBreathAbilityElectricityLine = Helpers.CreateBlueprint<BlueprintAbility>("DraconicCompanionBreathAbilityElectricityLine", bp => {
                bp.SetName("Breath Weapon - Electricity Line");
                bp.SetDescription("At 9th level, a draconic companion gains a breath weapon, usable once per day, that deals 1d6 points of damage per Hit Dice of the energy type " +
                    "matching its draconic resistance in either a 30-foot cone or a 60-foot line (chosen when the draconic companion gains this ability). At 15th level, it can use the " +
                    "breath weapon three times per day, but it must wait 1d4 rounds between uses. Targets of this breath weapon can attempt a Reflex save (DC = 10 + half the draconic " +
                    "companion’s Hit Dice + the draconic companion’s Constitution modifier) for half damage.");
                bp.m_Icon = BloodlineDraconicBlueBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        LightningBolt00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Line;
                    c.m_Length = new Feet() { m_Value = 60 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = 0,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Electricity
                            },
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
                            Duration = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true,
                            },
                            PreRolledSharedValue = AbilitySharedValue.Damage,
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 8,
                                    ValueRank = AbilityRankType.DamageDice,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DraconicBreathAbilityCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DraconicBreathAbilityCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DraconicCompanionBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DraconicBreathWeaponAnimalCompanionFeatureElectricity = Helpers.CreateBlueprint<BlueprintFeature>("DraconicBreathWeaponAnimalCompanionFeatureElectricity", bp => {
                bp.SetName("Breath Weapon");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        DraconicCompanionBreathAbilityElectricityCone.ToReference<BlueprintUnitFactReference>(),
                        DraconicCompanionBreathAbilityElectricityLine.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DraconicCompanionBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] {
                        DraconicCompanionBreathAbilityElectricityCone.ToReference<BlueprintAbilityReference>(),
                        DraconicCompanionBreathAbilityElectricityLine.ToReference<BlueprintAbilityReference>()
                    };
                    c.Stat = StatType.Constitution;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var DraconicCompanionBreathAbilityAcidCone = Helpers.CreateBlueprint<BlueprintAbility>("DraconicCompanionBreathAbilityAcidCone", bp => {
                bp.SetName("Breath Weapon - Acid Cone");
                bp.SetDescription("At 9th level, a draconic companion gains a breath weapon, usable once per day, that deals 1d6 points of damage per Hit Dice of the energy type " +
                    "matching its draconic resistance in either a 30-foot cone or a 60-foot line (chosen when the draconic companion gains this ability). At 15th level, it can use the " +
                    "breath weapon three times per day, but it must wait 1d4 rounds between uses. Targets of this breath weapon can attempt a Reflex save (DC = 10 + half the draconic " +
                    "companion’s Hit Dice + the draconic companion’s Constitution modifier) for half damage.");
                bp.m_Icon = BloodlineDraconicWhiteBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        AcidCone30Feet00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Cone;
                    c.m_Length = new Feet() { m_Value = 30 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = 0,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Acid
                            },
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
                            Duration = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true,
                            },
                            PreRolledSharedValue = AbilitySharedValue.Damage,
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 8,
                                    ValueRank = AbilityRankType.DamageDice,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DraconicBreathAbilityCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DraconicBreathAbilityCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DraconicCompanionBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DraconicCompanionBreathAbilityAcidLine = Helpers.CreateBlueprint<BlueprintAbility>("DraconicCompanionBreathAbilityAcidLine", bp => {
                bp.SetName("Breath Weapon - Acid Line");
                bp.SetDescription("At 9th level, a draconic companion gains a breath weapon, usable once per day, that deals 1d6 points of damage per Hit Dice of the energy type " +
                    "matching its draconic resistance in either a 30-foot cone or a 60-foot line (chosen when the draconic companion gains this ability). At 15th level, it can use the " +
                    "breath weapon three times per day, but it must wait 1d4 rounds between uses. Targets of this breath weapon can attempt a Reflex save (DC = 10 + half the draconic " +
                    "companion’s Hit Dice + the draconic companion’s Constitution modifier) for half damage.");
                bp.m_Icon = BloodlineDraconicWhiteBreathWeaponAbility.m_Icon;
                bp.AddComponent<AbilityDeliverProjectile>(c => {
                    c.m_Projectiles = new BlueprintProjectileReference[] {
                        AcidLine00.ToReference<BlueprintProjectileReference>()
                    };
                    c.Type = AbilityProjectileType.Line;
                    c.m_Length = new Feet() { m_Value = 60 };
                    c.m_LineWidth = new Feet() { m_Value = 5 };
                    c.AttackRollBonusStat = StatType.Unknown;
                });
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Reflex;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionDealDamage() {
                            m_Type = ContextActionDealDamage.Type.Damage,
                            DamageType = new DamageTypeDescription() {
                                Type = DamageType.Energy,
                                Common = new DamageTypeDescription.CommomData() {
                                    Reality = 0,
                                    Alignment = 0,
                                    Precision = false
                                },
                                Physical = new DamageTypeDescription.PhysicalData() {
                                    Material = 0,
                                    Form = 0,
                                    Enhancement = 0,
                                    EnhancementTotal = 0
                                },
                                Energy = DamageEnergyType.Acid
                            },
                            AbilityType = StatType.Unknown,
                            EnergyDrainType = EnergyDrainType.Temporary,
                            Duration = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true,
                            },
                            PreRolledSharedValue = AbilitySharedValue.Damage,
                            Value = new ContextDiceValue() {
                                DiceType = DiceType.D6,
                                DiceCountValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    Value = 8,
                                    ValueRank = AbilityRankType.DamageDice,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 0,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                            },
                            IsAoE = true,
                            HalfIfSaved = true,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        },
                        new ContextActionOnContextCaster() {
                            Actions = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = DraconicBreathAbilityCooldown.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.D4,
                                        DiceCountValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 1,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Simple,
                                            Value = 0,
                                            ValueRank = AbilityRankType.Default,
                                            ValueShared = AbilitySharedValue.Damage,
                                            Property = UnitProperty.None
                                        },
                                    }
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.ClassLevel;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        AnimalCompanionClass.ToReference<BlueprintCharacterClassReference>()
                    };
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { DraconicBreathAbilityCooldown.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = DraconicCompanionBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.Type = AbilityType.Special;
                bp.Range = AbilityRange.Projectile;
                bp.CanTargetPoint = true;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.BreathWeapon;
                bp.AvailableMetamagic = Metamagic.Empower | Metamagic.Maximize | Metamagic.Quicken | Metamagic.Extend | Metamagic.Heighten | Metamagic.Selective | Metamagic.Bolstered;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var DraconicBreathWeaponAnimalCompanionFeatureAcid = Helpers.CreateBlueprint<BlueprintFeature>("DraconicBreathWeaponAnimalCompanionFeatureAcid", bp => {
                bp.SetName("Breath Weapon");
                bp.SetDescription("");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        DraconicCompanionBreathAbilityAcidCone.ToReference<BlueprintUnitFactReference>(),
                        DraconicCompanionBreathAbilityAcidLine.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = DraconicCompanionBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<ReplaceAbilitiesStat>(c => {
                    c.m_Ability = new BlueprintAbilityReference[] {
                        DraconicCompanionBreathAbilityAcidCone.ToReference<BlueprintAbilityReference>(),
                        DraconicCompanionBreathAbilityAcidLine.ToReference<BlueprintAbilityReference>()
                    };
                    c.Stat = StatType.Constitution;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });

            var DraconicBreathWeaponAnimalCompanionFeature = Helpers.CreateBlueprint<BlueprintFeature>("DraconicBreathWeaponAnimalCompanionFeature", bp => {
                bp.SetName("Breath Weapon");
                bp.SetDescription("At 9th level, a draconic companion gains a breath weapon, usable once per day, that deals 1d6 points of damage per Hit Dice of the energy type " +
                    "matching its draconic resistance in either a 30-foot cone or a 60-foot line (chosen when the draconic companion gains this ability). At 15th level, it can use the " +
                    "breath weapon three times per day, but it must wait 1d4 rounds between uses. Targets of this breath weapon can attempt a Reflex save (DC = 10 + half the draconic " +
                    "companion’s Hit Dice + the draconic companion’s Constitution modifier) for half damage.");
                bp.m_Icon = BloodlineDraconicRedBreathWeaponAbility.m_Icon;
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = AnimalCompanionEnergyTypeFire.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DraconicBreathWeaponAnimalCompanionFeatureFire.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = AnimalCompanionEnergyTypeCold.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DraconicBreathWeaponAnimalCompanionFeatureCold.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = AnimalCompanionEnergyTypeElectricity.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DraconicBreathWeaponAnimalCompanionFeatureElectricity.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AddFeatureIfHasFact>(c => {
                    c.m_CheckedFact = AnimalCompanionEnergyTypeAcid.ToReference<BlueprintUnitFactReference>();
                    c.m_Feature = DraconicBreathWeaponAnimalCompanionFeatureAcid.ToReference<BlueprintUnitFactReference>();
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.IsClassFeature = true;
            });


            var DraconicBreathWeaponExtraUse = Helpers.CreateBlueprint<BlueprintFeature>("DraconicBreathWeaponExtraUse", bp => {
                bp.SetName("Draconic Breath Extra Use");
                bp.SetDescription("");
                bp.AddComponent<IncreaseResourceAmount>(c => {
                    c.m_Resource = DraconicCompanionBreathAbilityResource.ToReference<BlueprintAbilityResourceReference>();
                    c.Value = 2;
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = true;
                bp.IsClassFeature = true;
            });


            DraconicArchetype.RemoveFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(3, EvasionFeature),
                    Helpers.LevelEntry(6, AnimalCompanionDevotionFeature),
                    Helpers.LevelEntry(9, MultiattackFeature),
                    Helpers.LevelEntry(15, ImprovedEvasionFeature),
            };
            DraconicArchetype.AddFeatures = new LevelEntry[] {
                    Helpers.LevelEntry(1, DraconicSightAnimalCompanionFeature),
                    Helpers.LevelEntry(3, DraconicResistanceAnimalCompanionFeatureSelection),
                    Helpers.LevelEntry(6, ImprovedDraconicResistanceAnimalCompanionFeature),
                    Helpers.LevelEntry(9, DraconicBreathWeaponAnimalCompanionFeature),
                    Helpers.LevelEntry(15, DraconicBreathWeaponExtraUse)
            };
            if (ModSettings.AddedContent.Archetypes.IsDisabled("Draconic Companion")) { return; }
            AnimalCompanionClass.m_Archetypes = AnimalCompanionClass.m_Archetypes.AppendToArray(DraconicArchetype.ToReference<BlueprintArchetypeReference>());
        }
    }
}
