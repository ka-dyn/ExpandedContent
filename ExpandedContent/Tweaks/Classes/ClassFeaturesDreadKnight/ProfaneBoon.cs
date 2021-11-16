using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using System.Collections.Generic;
using System.Linq;

namespace ExpandedContent.Tweaks.Classes.ClassFeaturesDreadKnight {
    static class ProfaneBoon {


        private static readonly BlueprintFeatureSelection CavalierMountSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("0605927df6e2fdd42af6ee2424eb89f2");


        
        
   
        private static readonly BlueprintFeature AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");

        
        private static readonly BlueprintFeature MountTargetFeature = Resources.GetBlueprint<BlueprintFeature>("cb06f0e72ffb5c640a156bd9f8000c1d");
        private static readonly BlueprintFeature AnimalCompanionArchetypeSelection = Resources.GetBlueprint<BlueprintFeature>("65af7290b4efd5f418132141aaa36c1b");
        private static readonly BlueprintFeature OtherworldlyCompanionFiendish = Resources.GetBlueprint<BlueprintFeature>("4d7607a0155af7d43b49b785f2051e21");


        private static readonly BlueprintFeature TemplateFiendish = Resources.GetModBlueprint<BlueprintFeature>("TemplateFiendish");
        private static readonly BlueprintFeature WeaponBondFeature = Resources.GetBlueprint<BlueprintFeature>("1c7cdc1605554954f838d85bbdd22d90");
        private static readonly BlueprintItemEnchantment Unholy = Resources.GetBlueprint<BlueprintItemEnchantment>("d05753b8df780fc4bb55b318f06af453");
        private static readonly BlueprintActivatableAbility WeaponBondAxiomaticChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("d76e8a80ab14ac942b6a9b8aaa5860b1");
        private static readonly BlueprintItemEnchantment Anarchic = Resources.GetBlueprint<BlueprintItemEnchantment>("57315bc1e1f62a741be0efde688087e9");
        private static readonly BlueprintActivatableAbility WeaponBondFlamingBurstChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("3af19bdbd6215434f8421a860cc98363");
        private static readonly BlueprintFeature WeaponBondAdditionalUse = Resources.GetBlueprint<BlueprintFeature>("5a64de5435667da4eae2e4c95ec87917");
        private static readonly BlueprintFeature WeaponBondPlus3 = Resources.GetBlueprint<BlueprintFeature>("d2f45a2034d4f7643ba1a450bc5c4c06");
        private static readonly BlueprintFeature WeaponBondPlus4 = Resources.GetBlueprint<BlueprintFeature>("6d73f49b602e29a43a6faa2ea1e4a425");
        private static readonly BlueprintFeature WeaponBondPlus5 = Resources.GetBlueprint<BlueprintFeature>("f17c3ba33bb44d44782cb3851d823011");
        private static readonly BlueprintFeature WeaponBondPlus6 = Resources.GetBlueprint<BlueprintFeature>("b936ee90c070edb46bd76025dc1c5936");

        public static void AddProfaneBoon() {

            var FiendishWeaponAnarchicBuff = Helpers.CreateBlueprint<BlueprintBuff>("FiendishWeaponAnarchicBuff", bp => {
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Stack;
                bp.AddComponent<AddBondProperty>(c => {
                    c.m_Enchant = Anarchic.ToReference<BlueprintItemEnchantmentReference>();
                    c.EnchantPool = EnchantPoolType.DivineWeaponBond;
                });
            });
            var FiendishWeaponAnarchicChoice = Helpers.CreateBlueprint<BlueprintActivatableAbility>("FiendishWeaponAnarchicChoice", bp => {
                bp.SetName("Profane Weapon Bond - Anarchic");
                bp.SetDescription("A dread knight can add the anarchic property to a weapon enhanced with her profane weapon bond, but this consumes " +
                    "2 points of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon. An anarchic weapon is imbued with chaotic power. This power makes the " +
                    "weapon chaotic - aligned and thus overcomes the corresponding { g | Encyclopedia:Damage_Reductiondamage reduction {/g }. It deals an " +
                    "extra { g|Encyclopedia:Dice }2d6{/ } points of { g|Encyclopedia:Damage } damage{/g}against all creatures of law {g|Encyclopedia:Alignment}alignment {/ g}.");
                bp.Group = ActivatableAbilityGroup.DivineWeaponProperty;
                bp.WeightInGroup = 2;
                bp.DeactivateImmediately = true;
                bp.m_Buff = FiendishWeaponAnarchicBuff.ToReference<BlueprintBuffReference>();
            });
            var FiendishWeaponUnholyBuff = Helpers.CreateBlueprint<BlueprintBuff>("FiendishWeaponUnholyBuff", bp => {
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Stack;
                bp.AddComponent<AddBondProperty>(c => {
                    c.m_Enchant = Unholy.ToReference<BlueprintItemEnchantmentReference>();
                    c.EnchantPool = EnchantPoolType.DivineWeaponBond;
                });
            });
            var FiendishWeaponUnholyChoice = Helpers.CreateBlueprint<BlueprintActivatableAbility>("FiendishWeaponUnholyChoice", bp => {
                bp.SetName("Profane Weapon Bond - Unholy");
                bp.SetDescription("A dread knight can add the unholy property to a weapon enhanced with her profane weapon bond, but this consumes " +
                    "2 points of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon. An unholy weapon is imbued with unholy power.This power makes the " +
                    "weapon evil - aligned and thus overcomes the corresponding { g | Encyclopedia:Damage_Reductiondamage reduction {/g }. It deals an " +
                    "extra { g|Encyclopedia:Dice }2d6{/ } points of { g|Encyclopedia:Damage } damage{/g}against all creatures of good {g|Encyclopedia:Alignment}alignment {/ g}.");
                bp.Group = ActivatableAbilityGroup.DivineWeaponProperty;
                bp.WeightInGroup = 2;
                bp.DeactivateImmediately = true;
                bp.m_Buff = FiendishWeaponUnholyBuff.ToReference<BlueprintBuffReference>();
            });

            var DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
            var ProfaneWeaponIcon = AssetLoader.LoadInternal("Skills", "Icon_ProfaneWeapon.png");
            var FiendishWeaponBondFeature = Helpers.CreateBlueprint<BlueprintFeature>("FiendishWeaponBondFeature", bp => {
                bp.SetName("Profane Weapon Bond (+1)");
                bp.SetDescription("Upon reaching 5th level, a Dread Knight forms a Profane bond with her weapon. As " +
                    "a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a " +
                    "Fiendish spirit for 1 minute per Dread Knight level. At 5th level, this spirit grants the weapon a + 1 enhancement " +
                    " g|Encyclopedia:Bonus}bonus{/g}. For every three levels beyond 5th, the weapon gains another +1 " +
                    "enhancement bonus, to a maximum of +6 at 20th level.These bonuses can be added to the weapon, stacking with existing weapon " +
                    "bonuses to a maximum of +5.Alternatively, they can be used to add any of the following weapon properties: anarchic, axiomatic, brilliant energy, " +
                    "flaming, flaming burst, unholy, keen, and { g | Encyclopedia:Speed}    speed{/g}. Adding these properties consumes an amount " +
                    "of bonus equal to the property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities " +
                    "do not stack. A Dread Knight can use this ability once per day at 5th level, and one additional time per day for every four levels beyond 5th, " +
                    "to a total of four times per day at 17th level.");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.ComponentsArray = WeaponBondFeature.ComponentsArray;
            });
            var FiendishWeaponBondPlus2 = Helpers.CreateBlueprint<BlueprintFeature>("FiendishWeaponBondPlus2", bp => {
                bp.SetName("Profane Weapon Bond (+2)");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.AddComponent<IncreaseActivatableAbilityGroupSize>(c => { c.Group = ActivatableAbilityGroup.DivineWeaponProperty; });
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[4] { FiendishWeaponUnholyChoice.ToReference<BlueprintUnitFactReference>(),
                        WeaponBondAxiomaticChoice.ToReference<BlueprintUnitFactReference>(), FiendishWeaponAnarchicChoice.ToReference<BlueprintUnitFactReference>(),
                        WeaponBondFlamingBurstChoice.ToReference<BlueprintUnitFactReference>() };
                });

            });
            var FiendishWeaponBondPlus3 = Helpers.CreateBlueprint<BlueprintFeature>("FiendishWeaponBondPlus3", bp => {
                bp.SetName("Profane Weapon Bond (+3)");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.ComponentsArray = WeaponBondPlus3.ComponentsArray;
            });
            var FiendishWeaponBondPlus4 = Helpers.CreateBlueprint<BlueprintFeature>("FiendishWeaponBondPlus4", bp => {
                bp.SetName("Profane Weapon Bond (+4)");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.ComponentsArray = WeaponBondPlus4.ComponentsArray;
            });
            var FiendishWeaponBondPlus5 = Helpers.CreateBlueprint<BlueprintFeature>("FiendishWeaponBondPlus5", bp => {
                bp.SetName("Profane Weapon Bond (+5)");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.ComponentsArray = WeaponBondPlus5.ComponentsArray;
            });
            var FiendishWeaponBondPlus6 = Helpers.CreateBlueprint<BlueprintFeature>("FiendishWeaponBondPlus6", bp => {
                bp.SetName("Profane Weapon Bond (+6)");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.ComponentsArray = WeaponBondPlus6.ComponentsArray;
            });
            var FiendishWeaponBondProgression = Helpers.CreateBlueprint<BlueprintProgression>("FiendishWeaponBondProgression", bp => {
                bp.SetName("Fiendish Weapon Bond");
                bp.SetDescription("TUpon reaching 5th level, a Dread Knight forms a Profane bond with her weapon. As " +
                    "a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a " +
                    "Fiendish spirit for 1 minute per Dread Knight level. At 5th level, this spirit grants the weapon a + 1 enhancement " +
                    " g|Encyclopedia:Bonus}bonus{/g}. For every three levels beyond 5th, the weapon gains another +1 " +
                    "enhancement bonus, to a maximum of +6 at 20th level.These bonuses can be added to the weapon, stacking with existing weapon " +
                    "bonuses to a maximum of +5.Alternatively, they can be used to add any of the following weapon properties: anarchic, axiomatic, brilliant energy, " +
                    "flaming, flaming burst, unholy, keen, and { g | Encyclopedia:Speed}    speed{/g}. Adding these properties consumes an amount " +
                    "of bonus equal to the property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities " +
                    "do not stack. A Dread Knight can use this ability once per day at 5th level, and one additional time per day for every four levels beyond 5th, " +
                    "to a total of four times per day at 17th level.");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
            });
            FiendishWeaponBondProgression.LevelEntries = new LevelEntry[8] {
                Helpers.LevelEntry(5, (BlueprintFeatureBase) FiendishWeaponBondFeature),
                Helpers.LevelEntry(8, (BlueprintFeatureBase) FiendishWeaponBondPlus2),
                Helpers.LevelEntry(9, (BlueprintFeatureBase) WeaponBondAdditionalUse),
                Helpers.LevelEntry(11, (BlueprintFeatureBase) FiendishWeaponBondPlus3),
                Helpers.LevelEntry(13, (BlueprintFeatureBase) WeaponBondAdditionalUse),
                Helpers.LevelEntry(14, (BlueprintFeatureBase) FiendishWeaponBondPlus4),
                Helpers.LevelEntry(17, (BlueprintFeatureBase) FiendishWeaponBondPlus5, (BlueprintFeatureBase) WeaponBondAdditionalUse),
                Helpers.LevelEntry(20, (BlueprintFeatureBase) FiendishWeaponBondPlus6)


            };
            var DreadKnightAnimalCompanionProgression = Helpers.CreateBlueprint<BlueprintProgression>("DreadKnightAnimalCompanionProgression", bp => {
                bp.SetName("Dread Knight Animal Companion Progression");
                bp.SetName("");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.GiveFeaturesForPreviousLevels = true;
                bp.ReapplyOnLevelUp = true;
                bp.m_ExclusiveProgression = DreadKnightClass.ToReference<BlueprintCharacterClassReference>();
                bp.m_FeatureRankIncrease = new BlueprintFeatureReference();
                bp.LevelEntries = Enumerable.Range(2, 20)
                    .Select(i => new LevelEntry {
                        Level = i,
                        m_Features = new List<BlueprintFeatureBaseReference> {
                            AnimalCompanionRank.ToReference<BlueprintFeatureBaseReference>()
                        },
                    })
                    .ToArray();
                bp.UIGroups = new UIGroup[0];
            });
            var ServantIcon = AssetLoader.LoadInternal("Skills", "Icon_Servant.png");
            var DreadKnightProfaneMountFiendish = Helpers.CreateBlueprint<BlueprintFeature>("DreadKnightProfaneMountFiendish", bp => {
                bp.SetName("Profane Mount — Fiendish");
                bp.SetDescription("A Dread Knight's animal companion gains spell resistance equal to its level +5. It also gains:\n" +
                    "1 — 4 HD: resistance 5 to cold and fire.\n" +
                    "5 — 10 HD: resistance 10 to cold and fire, DR 5/good\n" +
                    "11+ HD: resistance 15 to cold and fire, DR 10/good\n" +
                    "Smite Good (Su): Once per day, the fiendish creature may smite a good-aligned creature. As a swift action, " +
                    "the creature chooses one target within sight to smite. If this target is good, the creature adds its Charisma bonus (if any) to " +
                    "attack rolls and gains a damage bonus equal to its HD against that foe. This effect persists until the target is dead or the creature rests.");
                bp.m_Icon = ServantIcon;
                bp.IsClassFeature = true;
                bp.Ranks = 1;
                bp.AddComponent<AddFeatureToPet>(c => {
                    c.m_Feature = TemplateFiendish.ToReference<BlueprintFeatureReference>();
                });
                bp.AddPrerequisite<PrerequisiteAlignment>(p => {
                    p.Alignment = AlignmentMaskType.Evil | AlignmentMaskType.TrueNeutral;
                    p.HideInUI = true;
                });
            });
            var CavalierMountFeatureWolf = Resources.GetModBlueprint<BlueprintFeature>("CavalierMountFeatureWolf");
            var AnimalCompanionFeatureHorse_PreorderBonus = Resources.GetBlueprint<BlueprintFeature>("bfeb9be0a3c9420b8b2beecc8171029c");
            var AnimalCompanionFeatureHorse = Resources.GetBlueprint<BlueprintFeature>("9dc58b5901677c942854019d1dd98374");
            var DreadKnightCompanionSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("DreadKnightCompanionSelection", bp => {
                bp.SetName("Fiendish Servant");
                bp.SetDescription("The Dread Knight is granted a fiendish steed to carry her into battle. This mount functions " +
                    "as a druid’s animal companion, using the Dread Knight’s level as her effective druid level. The creature must be one that " +
                    "she is capable of riding and must be suitable as a mount. A Medium Dread Knight can select a horse. A Small Dread Knight can select a wolf.");
                bp.IsClassFeature = true;
                bp.ReapplyOnLevelUp = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.Feat };
                bp.Mode = SelectionMode.OnlyNew;
                bp.Group = FeatureGroup.AnimalCompanion;
                bp.Ranks = 1;
                bp.m_Icon = ServantIcon;
                bp.IsPrerequisiteFor = new List<BlueprintFeatureReference>();
                bp.AddFeatures(
                    AnimalCompanionFeatureHorse.ToReference<BlueprintFeatureReference>(),
                    AnimalCompanionFeatureHorse_PreorderBonus.ToReference<BlueprintFeatureReference>(),
                    CavalierMountFeatureWolf.ToReference<BlueprintFeatureReference>()
                );
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = DreadKnightAnimalCompanionProgression.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = MountTargetFeature.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = AnimalCompanionArchetypeSelection.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnApply>(c => {
                    c.m_Feature = DreadKnightProfaneMountFiendish.ToReference<BlueprintFeatureReference>();
                });
            });
            
        }
    }
}
            
         


           


              








