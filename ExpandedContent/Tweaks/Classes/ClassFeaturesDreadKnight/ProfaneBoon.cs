using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.AI.Blueprints;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using System.Collections.Generic;
using System.Linq;

namespace ExpandedContent.Tweaks.Classes.ClassFeaturesDreadKnight {
    static class ProfaneBoon {


        private static readonly BlueprintFeatureSelection CavalierMountSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("0605927df6e2fdd42af6ee2424eb89f2");





        private static readonly BlueprintFeature AnimalCompanionRank = Resources.GetBlueprint<BlueprintFeature>("1670990255e4fe948a863bafd5dbda5d");


        private static readonly BlueprintFeature MountTargetFeature = Resources.GetBlueprint<BlueprintFeature>("cb06f0e72ffb5c640a156bd9f8000c1d");
        private static readonly BlueprintFeature AnimalCompanionArchetypeSelection = Resources.GetBlueprint<BlueprintFeature>("65af7290b4efd5f418132141aaa36c1b");
        private static readonly BlueprintFeature OtherworldlyCompanionFiendish = Resources.GetBlueprint<BlueprintFeature>("4d7607a0155af7d43b49b785f2051e21");
        private static readonly BlueprintItemEnchantment Vicious = Resources.GetBlueprint<BlueprintItemEnchantment>("a1455a289da208144981e4b1ef92cc56");
        private static readonly BlueprintItemEnchantment Vorpal = Resources.GetBlueprint<BlueprintItemEnchantment>("2f60bfcba52e48a479e4a69868e24ebc");
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
            var PaladinClass = Resources.GetBlueprint<BlueprintCharacterClass>("bfa11238e7ae3544bbeb4d0b92e897ec");
            var DreadKnightClass = Resources.GetModBlueprint<BlueprintCharacterClass>("DreadKnightClass");
            var WeaponBondAdditionalUse = Resources.GetBlueprint<BlueprintFeature>("5a64de5435667da4eae2e4c95ec87917");
            var WeaponBondAxiomaticChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("d76e8a80ab14ac942b6a9b8aaa5860b1");
            var WeaponBondFlamingBurstChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("3af19bdbd6215434f8421a860cc98363");
            var ProfaneWeaponIcon = AssetLoader.LoadInternal("Skills", "Icon_ProfaneWeapon.png");
            var WeaponBondSwitchAbility = Resources.GetBlueprint<BlueprintAbility>("7ff088ab58c69854b82ea95c2b0e35b4");
            WeaponBondSwitchAbility.m_DisplayName = Helpers.CreateString("$WeaponBondSwitchAbility.DisplayName", "Divine/Profane Weapon Bond");
            WeaponBondSwitchAbility.m_Description = Helpers.CreateString("$WeaponBondSwitchAbility.Description", "Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
                "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
                "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
                "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
                "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
                "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
                "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
                "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
                "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
                "levels beyond 5th, to a total of four times per day at 17th level.");
            WeaponBondSwitchAbility.RemoveComponents<ContextRankConfig>();
            WeaponBondSwitchAbility.AddComponent<ContextRankConfig>(c => {
                    c.m_BaseValueType = ContextRankBaseValueType.CharacterLevel;
                    
                    c.m_Max = 20;

            });
            WeaponBondSwitchAbility.RemoveComponents<AbilityCasterAlignment>();
            WeaponBondSwitchAbility.AddComponent<AbilityCasterAlignment>(c => {
                c.Alignment = AlignmentMaskType.Any;
            });
            var WeaponBondDurationBuff = Resources.GetBlueprint<BlueprintBuff>("bf570774501886f47b395a4bfe75eeb2");
            WeaponBondDurationBuff.m_DisplayName = Helpers.CreateString("$WeaponBondSwitchAbility.DisplayName", "Divine/Profane Weapon Bond");
            WeaponBondDurationBuff.m_Description = Helpers.CreateString("$WeaponBondSwitchAbility.Description", "Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
                "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
                "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
                "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
                "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
                "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
                "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
                "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
                "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
                "levels beyond 5th, to a total of four times per day at 17th level.");


            var WeaponBondResourse = Resources.GetBlueprint<BlueprintAbilityResource>("3683d1af071c1744185ff93cba9db10b");
        
            WeaponBondFlamingBurstChoice.SetName("Divine/Profane Weapon Bond - Flaming Burst");
            WeaponBondFlamingBurstChoice.SetDescription("A paladin/dread knight can add the flaming burst property to a weapon enhanced with her divine/profane weapon bond, " +
                "but this consumes 2 points of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon. A flaming burst weapon functions as a " +
                "flaming weapon that also explodes with flame upon striking a successful {g|Encyclopedia:Critical}critical hit{/g}. The fire does not harm " +
                "the wielder. In addition to the extra {g|Encyclopedia:Dice}1d6{/g} {g|Encyclopedia:Energy_Damage}fire damage{/g} from the flaming ability, a " +
                "flaming burst weapon deals an extra 1d10 points of fire {g|Encyclopedia:Damage}damage{/g} on a successful critical hit. If the weapon's " +
                "critical multiplier is ?3, add an extra 2d10 points of fire damage instead, and if the multiplier is ?4, add an extra 3d10 points of fire damage.");
            var WeaponBondBrilliantEnergyChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("f1eec5cc68099384cbfc6964049b24fa");
            WeaponBondBrilliantEnergyChoice.SetName("Divine/Profane Weapon Bond - Brilliant Energy");
            WeaponBondBrilliantEnergyChoice.SetDescription("A paladin/dread knight can add the brilliant energy property to a weapon enhanced with her divine/profane weapon bond, " +
                "but this consumes 4 points of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon.\nA brilliant energy weapon ignores nonliving matter. " +
                "Armor and shield bonuses to {g|Encyclopedia:Armor_Class}AC{/g} (including any enhancement bonuses to that armor) do not count against it because the weapon " +
                "passes through armor. ({g|Encyclopedia:Dexterity}Dexterity{/g}, deflection, dodge, natural armor, and other such bonuses still apply.) A brilliant energy " +
                "weapon cannot harm undead, constructs, or objects.");
            WeaponBondAdditionalUse.SetName("Divine/Profane Weapon Bond - Additional Use");
            WeaponBondAdditionalUse.SetDescription("Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
                "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
                "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
                "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
                "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
                "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
                "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
                "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
                "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
                "levels beyond 5th, to a total of four times per day at 17th level.");
            var WeaponBondKeenChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("27d76f1afda08a64d897cc81201b5218");
            WeaponBondKeenChoice.SetName("Divine/Profane Weapon Bond - Keen");
            WeaponBondKeenChoice.SetDescription("A paladin/dread knight can add the keen property to a weapon enhanced with her divine/profane weapon bond, " +
                "but this consumes 1 point of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon.\nThe keen property doubles the " +
                "threat range of a weapon. This benefit doesn't stack with any other effects that expand the threat range of a weapon " +
                "(such as the keen edge {g|Encyclopedia:Spell}spell{/g} or the Improved {g|Encyclopedia:Critical}Critical{/g} {g|Encyclopedia:Feat}feat{/g}).");
            var WeaponBondSpeedChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("ed1ef581af9d9014fa1386216b31cdae");
            WeaponBondSpeedChoice.SetName("Divine/Profane Weapon Bond - Speed");
            WeaponBondSpeedChoice.SetDescription("A paladin/dread knight can add the {g|Encyclopedia:Speed}speed{/g} property to a weapon " +
                "enhanced with her divine/profane weapon bond, but this consumes 3 points of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to " +
                "this weapon.\nWhen making a full {g|Encyclopedia:Attack}attack{/g}, the wielder of a speed weapon may make one extra attack with it. " +
                "The attack uses the wielder's full {g|Encyclopedia:BAB}base attack bonus{/g}, plus any modifiers appropriate to the situation. " +
                "(This benefit is not cumulative with similar effects, such as a haste {g|Encyclopedia:Spell}spell{/g}.)");
            var WeaponBondFlamingChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("7902941ef70a0dc44bcfc174d6193386");
            WeaponBondFlamingChoice.SetName("Divine/Profane Weapon Bond - Flaming");
            WeaponBondFlamingChoice.SetDescription("A paladin/dread knight can add the flaming property to a weapon enhanced with her divine/profane weapon bond, " +
                "but this consumes 1 point of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon.\nA flaming weapon is sheathed in " +
                "fire that deals an extra {g|Encyclopedia:Dice}1d6{/g} points of {g|Encyclopedia:Energy_Damage}fire damage{/g} on a successful hit. " +
                "The fire does not harm the wielder.");
            WeaponBondAxiomaticChoice.SetName("Divine/Profane Weapon Bond - Axiomatic");
            WeaponBondAxiomaticChoice.SetDescription("A paladin/dread knight can add the axiomatic property to a weapon enhanced with her divine/profane weapon bond, " +
                "but this consumes 2 points of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon.\nAn axiomatic weapon is infused with " +
                "lawful power. It makes the weapon {g|Encyclopedia:Alignment}lawful-aligned{/g} and thus overcomes the corresponding {g|Encyclopedia:Damage_Reduction}damage " +
                "reduction{/g}. It deals an extra {g|Encyclopedia:Dice}2d6{/g} points of {g|Encyclopedia:Damage}damage{/g} against chaotic creatures.");
            
















            var VorpalIcon = AssetLoader.LoadInternal("Skills", "Icon_Vorpal.png");
            var ViciousIcon = AssetLoader.LoadInternal("Skills", "Icon_Vicious.png");

            var FiendishWeaponViciousBuff = Helpers.CreateBlueprint<BlueprintBuff>("FiendishWeaponViciousBuff", bp => {
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Stack;
                bp.AddComponent<AddBondProperty>(c => {
                    c.m_Enchant = Vicious.ToReference<BlueprintItemEnchantmentReference>();
                    c.EnchantPool = EnchantPoolType.DivineWeaponBond;
                });
            });


            var FiendishWeaponViciousChoice = Helpers.CreateBlueprint<BlueprintActivatableAbility>("FiendishWeaponViciousChoice", bp => {
                bp.SetName("Profane Weapon Bond - Vicious");
                bp.SetDescription("A dread knight can add the vicious property to a weapon enhanced with her profane weapon bond, but this " +
                    "consumes 1 points of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon.\nWhen a vicious weapon strikes an " +
                    "opponent, it creates a flash of disruptive energy that resonates between the opponent and the wielder. This energy deals an " +
                    "extra {g|Encyclopedia:Dice}2d6{/g} points of {g|Encyclopedia:Damage}damage{/g} to the opponent and 1d6 points of damage to the wielder. " +
                    "however, die when their heads are cut off.");
                bp.m_Icon = ViciousIcon;
                bp.Group = ActivatableAbilityGroup.DivineWeaponProperty;
                bp.WeightInGroup = 1;
                bp.DeactivateImmediately = true;
                bp.m_Buff = FiendishWeaponViciousBuff.ToReference<BlueprintBuffReference>();
            });

            var FiendishWeaponVorpalBuff = Helpers.CreateBlueprint<BlueprintBuff>("FiendishWeaponVorpalBuff", bp => {
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Stack;
                bp.AddComponent<AddBondProperty>(c => {
                    c.m_Enchant = Vorpal.ToReference<BlueprintItemEnchantmentReference>();
                    c.EnchantPool = EnchantPoolType.DivineWeaponBond;
                });
            });
            var FiendishWeaponVorpalChoice = Helpers.CreateBlueprint<BlueprintActivatableAbility>("FiendishWeaponVorpalChoice", bp => {
                bp.SetName("Profane Weapon Bond - Vorpal");
                bp.SetDescription("A dread knight can add the vorpal property to a weapon enhanced with her profane weapon bond, but this " +
                    "consumes 5 points of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon.\nA vorpal weapon is infused with " +
                    "terrifying power. It allows the weapon to sever the heads of those it strikes.\nUpon a {g|Encyclopedia:Dice}roll{/g} of natural " +
                    "20 (followed by a successful roll to confirm the {g|Encyclopedia:Critical}critical hit{/g}), the weapon severs the opponent's " +
                    "head (if it has one) from its body. Some creatures, such as many aberrations and all oozes, have no heads. Others, " +
                    "such as golems and undead creatures other than vampires, are not affected by the loss of their heads. Most other creatures, " +
                    "however, die when their heads are cut off.");
                bp.m_Icon = VorpalIcon;
                bp.Group = ActivatableAbilityGroup.DivineWeaponProperty;
                bp.WeightInGroup = 4;
                bp.DeactivateImmediately = true;
                bp.m_Buff = FiendishWeaponVorpalBuff.ToReference<BlueprintBuffReference>();
            });
            var FiendishWeaponAnarchicBuff = Helpers.CreateBlueprint<BlueprintBuff>("FiendishWeaponAnarchicBuff", bp => {
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi | BlueprintBuff.Flags.StayOnDeath;
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Stack;
                bp.AddComponent<AddBondProperty>(c => {
                    c.m_Enchant = Anarchic.ToReference<BlueprintItemEnchantmentReference>();
                    c.EnchantPool = EnchantPoolType.DivineWeaponBond;
                });
            });
            var SacredWeaponAnarchicChoice = Resources.GetBlueprint<BlueprintActivatableAbility>("6fdc32d0af41ffb42b8285dbac9a050a");
            var FiendishWeaponAnarchicChoice = Helpers.CreateBlueprint<BlueprintActivatableAbility>("FiendishWeaponAnarchicChoice", bp => {
                bp.SetName("Profane Weapon Bond - Anarchic");
                bp.SetDescription("A dread knight can add the anarchic property to a weapon enhanced with her profane weapon bond, but this " +
                    "consumes 2 points of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon.\nAn anarchic weapon is infused with " +
                    "chaotic power. It makes the weapon {g|Encyclopedia:Alignment}chaotic-aligned{/g} and thus overcomes the corresponding " +
                    "{g|Encyclopedia:Damage_Reduction}damage reduction{/g}. It deals an extra {g|Encyclopedia:Dice}2d6{/g} points of " +
                    "{g|Encyclopedia:Damage}damage{/g} against lawful creatures.");
                bp.m_Icon = SacredWeaponAnarchicChoice.Icon;
                bp.Group = ActivatableAbilityGroup.DivineWeaponProperty;
                bp.WeightInGroup = 2;
                bp.DeactivateImmediately = true;
                bp.m_Buff = FiendishWeaponAnarchicBuff.ToReference<BlueprintBuffReference>();
            });
            var HellknightUnholyAbility = Resources.GetBlueprint<BlueprintActivatableAbility>("a68cd0fbf5d21ef4f8b9375ec0ac53b9");
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
                    "2 points of enhancement {g|Encyclopedia:Bonus}bonus{/g} granted to this weapon.\nAn unholy weapon is imbued with unholy power. " +
                    "This power makes the weapon evil-aligned and thus overcomes the corresponding {g|Encyclopedia:Damage_Reduction}damage reduction{/g}. " +
                    "It deals an extra {g|Encyclopedia:Dice}2d6{/g} points of {g|Encyclopedia:Damage}damage{/g} against all creatures of " +
                    "good {g|Encyclopedia:Alignment}alignment{/g}.");
                bp.m_Icon = HellknightUnholyAbility.Icon;
                bp.Group = ActivatableAbilityGroup.DivineWeaponProperty;
                bp.WeightInGroup = 2;
                bp.DeactivateImmediately = true;
                bp.m_Buff = FiendishWeaponUnholyBuff.ToReference<BlueprintBuffReference>();
            });

            

            var FiendishWeaponBondFeature = Helpers.CreateBlueprint<BlueprintFeature>("FiendishWeaponBondFeature", bp => {
                bp.SetName("Profane Weapon Bond (+1)");
                bp.SetDescription("Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
                "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
                "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
                "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
                "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
                "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
                "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
                "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
                "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
                "levels beyond 5th, to a total of four times per day at 17th level.");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.ComponentsArray = WeaponBondFeature.ComponentsArray;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { FiendishWeaponViciousChoice.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var FiendishWeaponBondPlus2 = Helpers.CreateBlueprint<BlueprintFeature>("FiendishWeaponBondPlus2", bp => {
                bp.SetName("Profane Weapon Bond (+2)");
                bp.SetDescription("Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
                "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
                "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
                "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
                "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
                "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
                "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
                "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
                "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
                "levels beyond 5th, to a total of four times per day at 17th level.");
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
                bp.SetDescription("Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
                "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
                "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
                "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
                "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
                "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
                "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
                "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
                "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
                "levels beyond 5th, to a total of four times per day at 17th level.");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.ComponentsArray = WeaponBondPlus3.ComponentsArray;
            });
            var FiendishWeaponBondPlus4 = Helpers.CreateBlueprint<BlueprintFeature>("FiendishWeaponBondPlus4", bp => {
                bp.SetName("Profane Weapon Bond (+4)");
                bp.SetDescription("Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
                "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
                "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
                "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
                "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
                "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
                "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
                "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
                "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
                "levels beyond 5th, to a total of four times per day at 17th level.");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.ComponentsArray = WeaponBondPlus4.ComponentsArray;

            });
            var FiendishWeaponBondPlus5 = Helpers.CreateBlueprint<BlueprintFeature>("FiendishWeaponBondPlus5", bp => {
                bp.SetName("Profane Weapon Bond (+5)");
                bp.SetDescription("Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
                "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
                "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
                "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
                "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
                "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
                "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
                "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
                "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
                "levels beyond 5th, to a total of four times per day at 17th level.");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.ComponentsArray = WeaponBondPlus5.ComponentsArray;
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { FiendishWeaponVorpalChoice.ToReference<BlueprintUnitFactReference>() };
                });
            });
            var FiendishWeaponBondPlus6 = Helpers.CreateBlueprint<BlueprintFeature>("FiendishWeaponBondPlus6", bp => {
                bp.SetName("Profane Weapon Bond (+6)");
                bp.SetDescription("Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
                "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
                "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
                "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
                "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
                "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
                "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
                "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
                "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
                "levels beyond 5th, to a total of four times per day at 17th level.");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.IsClassFeature = true;
                bp.ComponentsArray = WeaponBondPlus6.ComponentsArray;
            });
            var FiendishWeaponBondProgression = Helpers.CreateBlueprint<BlueprintProgression>("FiendishWeaponBondProgression", bp => {
                bp.SetName("Profane Weapon Bond");
                bp.SetDescription("Upon reaching 5th level, a paladin/dreadknight forms a divine/profane bond with her weapon. " +
                "As a {g|Encyclopedia:Standard_Actions}standard action{/g}, she can call upon the aid of a celestial/fiendish spirit for 1 minute per " +
                "paladin/dreadknight level.\nAt 5th level, this spirit grants the weapon a +1 enhancement {g|Encyclopedia:Bonus}bonus{/g}. For every three " +
                "levels beyond 5th, the weapon gains another +1 enhancement bonus, to a maximum of +6 at 20th level. These bonuses can be added " +
                "to the weapon, stacking with existing weapon bonuses to a maximum of +5.\nAlternatively, they can be used to add any of the " +
                "following weapon properties: axiomatic/anarchic, brilliant energy/vorpal, defending, disruption/vicious, flaming, flaming burst, holy/unholy, keen, " +
                "and {g|Encyclopedia:Speed}speed{/g} depending on class. Adding these properties consumes an amount of bonus equal to the " +
                "property's cost. These bonuses are added to any properties the weapon already has, but duplicate abilities do not " +
                "stack.\nA paladin/dreadknight can use this ability once per day at 5th level, and one additional time per day for every four " +
                "levels beyond 5th, to a total of four times per day at 17th level.");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
            });
            var AnimatedLongsword = Resources.GetBlueprint<BlueprintUnit>("34df84245e875364b9a8832256bc349f");
            var WyvernCharoite = Resources.GetBlueprint<BlueprintUnit>("fb3cf6666c50638439cdecfa45ae80ac");
            var UnitDog = Resources.GetBlueprint<BlueprintUnit>("918939943bf32ba4a95470ea696c2ba5");
            var LongswordCompanionUnit = Helpers.CreateBlueprint<BlueprintUnit>("LongswordCompanionUnit", bp => {
                bp.m_Portrait = AnimatedLongsword.m_Portrait;
                bp.m_Faction = UnitDog.m_Faction;
                bp.m_Brain = UnitDog.m_Brain;
                bp.m_AddFacts = AnimatedLongsword.m_AddFacts;
                bp.Gender = AnimatedLongsword.Gender;
                bp.LocalizedName = AnimatedLongsword.LocalizedName;
                bp.Size = AnimatedLongsword.Size;
                bp.Color = AnimatedLongsword.Color;
                bp.Alignment = Kingmaker.Enums.Alignment.TrueNeutral;
                bp.Prefab = AnimatedLongsword.Prefab;
                bp.Visual = AnimatedLongsword.Visual;
                bp.FactionOverrides = UnitDog.FactionOverrides;
                bp.Body = AnimatedLongsword.Body;
                bp.Strength = UnitDog.Strength;
                bp.Dexterity = UnitDog.Dexterity;
                bp.Constitution = UnitDog.Constitution;
                bp.Intelligence = UnitDog.Intelligence;
                bp.Wisdom = UnitDog.Wisdom;
                bp.Charisma = UnitDog.Charisma;
                bp.Speed = AnimatedLongsword.Speed;
                bp.Skills = UnitDog.Skills;
                bp.m_DisplayName = AnimatedLongsword.m_DisplayName;
                bp.m_Description = AnimatedLongsword.m_Description;
                bp.m_DescriptionShort = AnimatedLongsword.m_DescriptionShort;
                bp.ComponentsArray = UnitDog.ComponentsArray;



            });
            var LongswordCompanion = Helpers.CreateBlueprint<BlueprintFeature>("LongswordCompanion", bp => {
                bp.SetName("Intelligent Longsword");
                bp.SetDescription("You gain an intelligent longsword companion.");
                bp.m_DescriptionShort = Helpers.CreateString("LongswordCompanion.DescriptionShort", "You gain an intelligent longsword companion.");
                bp.m_Icon = ProfaneWeaponIcon;
                bp.Ranks = 1;
                bp.Groups = new FeatureGroup[] { FeatureGroup.AnimalCompanion };
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddPet>(c => {
                    c.m_Pet = LongswordCompanionUnit.ToReference<BlueprintUnitReference>();
                    c.m_LevelRank = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisitePet>(c => { c.NoCompanion = true; });
            });
            FiendishWeaponBondProgression.LevelEntries = new LevelEntry[8] {
                Helpers.LevelEntry(5, (BlueprintFeatureBase) FiendishWeaponBondFeature,(BlueprintFeatureBase) LongswordCompanion),
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



            var DragonAzataCompanionFeature = Resources.GetBlueprint<BlueprintFeature>("cf36f23d60987224696f03be70351928");
            var AzataDragonUnit = Resources.GetBlueprint<BlueprintUnit>("32a037e97c3d5c54b85da8f639616c57");
            var DragonAzataCompanionRank = Resources.GetBlueprint<BlueprintFeature>("2780764bf33c46745b11f0e1d2d20092");
            var AnimalCompanionSelectionBase = Resources.GetBlueprint<BlueprintFeatureSelection>("90406c575576aee40a34917a1b429254");
            var AnimalCompanionSelectionDomain = Resources.GetBlueprint<BlueprintFeatureSelection>("2ecd6c64683b59944a7fe544033bb533");
            var AnimalCompanionSelectionDruid = Resources.GetBlueprint<BlueprintFeatureSelection>("571f8434d98560c43935e132df65fe76");
            var AnimalCompanionSelectionHunter = Resources.GetBlueprint<BlueprintFeatureSelection>("715ac15eb8bd5e342bc8a0a3c9e3e38f");
            var AnimalCompanionSelectionMadDog = Resources.GetBlueprint<BlueprintFeatureSelection>("738b59d0b58187f4d846b0caaf0f80d7");
            var AnimalCompanionSelectionRanger = Resources.GetBlueprint<BlueprintFeatureSelection>("ee63330662126374e8785cc901941ac7");
            var AnimalCompanionSelectionSacredHuntsmaster = Resources.GetBlueprint<BlueprintFeatureSelection>("2995b36659b9ad3408fd26f137ee2c67");
            var AnimalCompanionSelectionSylvanSorcerer = Resources.GetBlueprint<BlueprintFeatureSelection>("a540d7dfe1e2a174a94198aba037274c");
            var AnimalCompanionSelectionUrbanHunter = Resources.GetBlueprint<BlueprintFeatureSelection>("257375cd139800e459d69ccfe4ca309c");
            var AnimalCompanionSelectionWildlandShaman = Resources.GetBlueprint<BlueprintFeatureSelection>("164c875d6b27483faba479c7f5261915");
            var ArcaneRiderMountSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("82c791d4790c45dcac6038ef6932c3e3");
            var BeastRiderMountSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("f7da602dae8944d499f00074c973c28a");
            var BloodriderMountSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("c81bb2aa48c113c4ba3ee8582eb058ea");
            var CavalierMountSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("0605927df6e2fdd42af6ee2424eb89f2");
            var NomadMountSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("8e1957da5a8144d1b0fcf8875ac6bab7");
            var OracleRevelationBondedMount = Resources.GetBlueprint<BlueprintFeatureSelection>("0234d0dd1cead22428e71a2500afa2e1");
            var PaladinDivineMountSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("e2f0e0efc9e155e43ba431984429678e");
            var ShamanNatureSpiritTrueSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("e7f4cfcd7488ac14bbc3e09426b59fd0");
            var SoheiMonasticMountHorseSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("918432cc97b146a4b93e2b3060bdd1ed");

            var CharacterBrain = Resources.GetBlueprint<BlueprintBrain>("cf986dd7ba9d4ec46ad8a3a0406d02ae");
            var AzataDragonUnitNPC_medium = Resources.GetBlueprint<BlueprintUnit>("3e56db348cc24838bc78b55a114e552a");
            var AzataDragonUnitNPC_large = Resources.GetBlueprint<BlueprintUnit>("a643ed45374b070468138d16815ca2df");

            var HavocDragonMediumUnit = Helpers.CreateBlueprint<BlueprintUnit>("HavocDragonMediumUnit", bp => {
                bp.m_Portrait = AzataDragonUnit.m_Portrait;
                bp.m_Faction = UnitDog.m_Faction;
                bp.m_Brain = UnitDog.m_Brain;
                bp.m_AddFacts = AzataDragonUnit.m_AddFacts;
                bp.Gender = AzataDragonUnit.Gender;
                bp.LocalizedName = AzataDragonUnit.LocalizedName;
                bp.Size = Kingmaker.Enums.Size.Medium;
                bp.Color = AzataDragonUnit.Color;
                bp.Alignment = Kingmaker.Enums.Alignment.TrueNeutral;
                bp.Prefab = AzataDragonUnitNPC_medium.Prefab;
                bp.Visual = AzataDragonUnitNPC_medium.Visual;
                bp.FactionOverrides = AzataDragonUnit.FactionOverrides;
                bp.Body = AzataDragonUnitNPC_medium.Body;
                bp.Strength = AzataDragonUnit.Strength;
                bp.Dexterity = AzataDragonUnit.Dexterity;
                bp.Constitution = AzataDragonUnit.Constitution;
                bp.Intelligence = AzataDragonUnit.Intelligence;
                bp.Wisdom = AzataDragonUnit.Wisdom;
                bp.Charisma = AzataDragonUnit.Charisma;
                bp.Speed = AzataDragonUnit.Speed;
                bp.Skills = AzataDragonUnit.Skills;
                bp.m_DisplayName = AzataDragonUnit.m_DisplayName;
                bp.m_Description = AzataDragonUnit.m_Description;
                bp.m_DescriptionShort = AzataDragonUnit.m_DescriptionShort;
                bp.ComponentsArray = UnitDog.ComponentsArray;



            });
            
            var HavocDragonLargeUnit = Helpers.CreateBlueprint<BlueprintUnit>("HavocDragonLargeUnit", bp => {
                bp.m_Portrait = AzataDragonUnit.m_Portrait;
                bp.m_Faction = UnitDog.m_Faction;
                bp.m_Brain = UnitDog.m_Brain;
                bp.m_AddFacts = AzataDragonUnit.m_AddFacts;
                bp.Gender = AzataDragonUnit.Gender;
                bp.LocalizedName = AzataDragonUnit.LocalizedName;
                bp.Size = Kingmaker.Enums.Size.Large;
                bp.Color = AzataDragonUnit.Color;
                bp.Alignment = Kingmaker.Enums.Alignment.TrueNeutral;
                bp.Prefab = AzataDragonUnitNPC_medium.Prefab;
                bp.Visual = AzataDragonUnitNPC_medium.Visual;
                bp.FactionOverrides = AzataDragonUnit.FactionOverrides;
                bp.Body = AzataDragonUnitNPC_medium.Body;
                bp.Strength = AzataDragonUnit.Strength;
                bp.Dexterity = AzataDragonUnit.Dexterity;
                bp.Constitution = AzataDragonUnit.Constitution;
                bp.Intelligence = AzataDragonUnit.Intelligence;
                bp.Wisdom = AzataDragonUnit.Wisdom;
                bp.Charisma = AzataDragonUnit.Charisma;
                bp.Speed = AzataDragonUnit.Speed;
                bp.Skills = AzataDragonUnit.Skills;
                bp.m_DisplayName = AzataDragonUnit.m_DisplayName;
                bp.m_Description = AzataDragonUnit.m_Description;
                bp.m_DescriptionShort = AzataDragonUnit.m_DescriptionShort;
                bp.ComponentsArray = UnitDog.ComponentsArray;



            });
            


            var HavocIcon = AssetLoader.LoadInternal("Skills", "Icon_Havoc.png");
            var HavocDragonPetLarge = Helpers.CreateBlueprint<BlueprintFeature>("HavocDragonPetLarge", bp => {
                bp.SetName("Havoc Dragon Companion - Large");
                bp.SetDescription("This large dragon’s scales and insectile wings dance with color, while its whiplike tail waves as if stirred by an unseen breeze. " +
                "\nDespite their best intentions, the appropriately named havoc dragons often cause collateral damage as they develop whimsical wonderlands of revelry and relaxation.");
                bp.m_DescriptionShort = Helpers.CreateString("HavocDragonPet.DescriptionShort", "This dragon’s scales and insectile wings dance with color, while its whiplike tail waves as if stirred by an unseen breeze. " +
                "\nDespite their best intentions, the appropriately named havoc dragons often cause collateral damage as they develop whimsical wonderlands of revelry and relaxation.");
                bp.m_Icon = HavocIcon;
                bp.Ranks = 1;
                bp.Groups = new FeatureGroup[] { FeatureGroup.AnimalCompanion };
                bp.ReapplyOnLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddPet>(c => { c.m_Pet = HavocDragonLargeUnit.ToReference<BlueprintUnitReference>();
                    c.m_LevelRank = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisitePet>(c => { c.NoCompanion = true; });
            });

            var HavocDragonPetMedium = Helpers.CreateBlueprint<BlueprintFeature>("HavocDragonPetMedium", bp => {
                bp.SetName("Havoc Dragon Companion - Medium");
                bp.SetDescription("This medium dragon’s scales and insectile wings dance with color, while its whiplike tail waves as if stirred by an unseen breeze. " +
                "\nDespite their best intentions, the appropriately named havoc dragons often cause collateral damage as they develop whimsical wonderlands of revelry and relaxation.");
                bp.m_DescriptionShort = Helpers.CreateString("HavocDragonPet.DescriptionShort", "This dragon’s scales and insectile wings dance with color, while its whiplike tail waves as if stirred by an unseen breeze. " +
                "\nDespite their best intentions, the appropriately named havoc dragons often cause collateral damage as they develop whimsical wonderlands of revelry and relaxation.");
                bp.m_Icon = HavocIcon;
                bp.Ranks = 1;
                bp.ReapplyOnLevelUp = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.AnimalCompanion };
                bp.IsClassFeature = true;
                bp.AddComponent<AddPet>(c => {
                    c.m_Pet = HavocDragonMediumUnit.ToReference<BlueprintUnitReference>();
                    c.m_LevelRank = AnimalCompanionRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<PrerequisitePet>(c => { c.NoCompanion = true; });
            });




           
            AnimalCompanionSelectionBase.m_AllFeatures = AnimalCompanionSelectionBase.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            DreadKnightCompanionSelection.m_AllFeatures = DreadKnightCompanionSelection.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionDomain.m_AllFeatures = AnimalCompanionSelectionDomain.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionDruid.m_AllFeatures = AnimalCompanionSelectionDruid.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionHunter.m_AllFeatures = AnimalCompanionSelectionHunter.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionMadDog.m_AllFeatures = AnimalCompanionSelectionMadDog.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionRanger.m_AllFeatures = AnimalCompanionSelectionRanger.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionSacredHuntsmaster.m_AllFeatures = AnimalCompanionSelectionSacredHuntsmaster.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionSylvanSorcerer.m_AllFeatures = AnimalCompanionSelectionSylvanSorcerer.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionUrbanHunter.m_AllFeatures = AnimalCompanionSelectionUrbanHunter.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            AnimalCompanionSelectionWildlandShaman.m_AllFeatures = AnimalCompanionSelectionWildlandShaman.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            BeastRiderMountSelection.m_AllFeatures = BeastRiderMountSelection.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            BloodriderMountSelection.m_AllFeatures = BloodriderMountSelection.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            NomadMountSelection.m_AllFeatures = NomadMountSelection.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            OracleRevelationBondedMount.m_AllFeatures = OracleRevelationBondedMount.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());
            PaladinDivineMountSelection.m_AllFeatures = PaladinDivineMountSelection.m_AllFeatures.AppendToArray(HavocDragonPetMedium.ToReference<BlueprintFeatureReference>(), HavocDragonPetLarge.ToReference<BlueprintFeatureReference>());


        }
    }
}
















