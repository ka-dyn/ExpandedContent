using ExpandedContent.Extensions;
using ExpandedContent.Tweaks.Components;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Craft;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.EventConditionActionSystem.Conditions;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Enums.Damage;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UI.GenericSlot;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System.Collections.Generic;
using TabletopTweaks.Core.NewComponents;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using static Kingmaker.RuleSystem.Rules.Abilities.RuleApplySpell;
using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.Abilities.Components.AreaEffects;
using UnityEngine.Rendering;

namespace ExpandedContent.Tweaks.Mysteries {
    internal class ApocalypseMystery {
        public static void AddApocalypseMystery() {

            var OracleClass = Resources.GetBlueprint<BlueprintCharacterClass>("20ce9bf8af32bee4c8557a045ab499b1");
            var OracleRevelationSelection = Resources.GetBlueprint<BlueprintFeatureSelection>("60008a10ad7ad6543b1f63016741a5d2");
            var ApocalypseMysteryIcon = AssetLoader.LoadInternal("Skills", "Icon_OracleApocalypseMystery.png");
            var ArcanistClass = Resources.GetBlueprint<BlueprintCharacterClass>("52dbfd8505e22f84fad8d702611f60b7");
            var MagicDeceiverArchetype = Resources.GetBlueprint<BlueprintArchetype>("5c77110cd0414e7eb4c2e485659c9a46");

            //Spelllist
            var DoomSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fbdd8c455ac4cde4a9a3e18c84af9485");//?1
            var SummonNaturesAllyIISpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("298148133cdc3fd42889b99c82711986");
            var ExplosiveRunesSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("ff652882623d43f184eb6abef741e20e");
            var IceStormSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fcb028205a71ee64d98175ff39a0abf9");
            var TidalSurgeSpell = Resources.GetBlueprint<BlueprintAbility>("50bdf209dede4ce1acc722d5fdfcf53c");
            var CircleOfDeathSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("a89dcbbab8f40e44e920cc60636097cf");
            var IncendiaryCloudAbility = Resources.GetModBlueprint<BlueprintAbility>("IncendiaryCloudAbility");
            var MomentOfPrescienceAbility = Resources.GetModBlueprint<BlueprintAbility>("MomentOfPrescienceAbility");
            var MeteorSwarmSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("5e36df08c71748f7936bce310181fb71");
            var OwlsWisdomMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("9f5ada581af3db4419b54db77f44e430");
            var OwlsWisdomSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f0455c9295b53904f9e02fc571dd2ce1");
            var RemoveBlindnessSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c927a8b0cd3f5174f8c0b67cdbfde539");
            var ConfusionSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("cf6c901fb7acc904e85c63b342e9c949");
            var MindBlankSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("df2a0ba6b6dcecf429cbb80a56fee5cf");
            var MindBlankMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("87a29febd010993419f2a4a9bee11cfc");
            var TrueSeeingSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("4cf3d0fae3239ec478f51e86f49161cb");
            var TrueSeeingMassSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fa08cb49ade3eee42b5fd42bd33cb407");
            var SoundBurstSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("c3893092a333b93499fd0a21845aa265");
            var ShoutSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("f09453607e683784c8fca646eec49162");
            var SongOfDiscordSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("d38aaf487e29c3d43a3bffa4a4a55f8f");
            var ShoutGreaterSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("fd0d3840c48cafb44bb29e8eb74df204");
            var BrilliantInspirationSpell = Resources.GetBlueprintReference<BlueprintAbilityReference>("a5c56f0f699daec44b7aedd8b273b08a");
            var OracleApocalypseSpells = Helpers.CreateBlueprint<BlueprintFeature>("OracleApocalypseSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DoomSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SummonNaturesAllyIISpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ExplosiveRunesSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = IceStormSpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TidalSurgeSpell.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = CircleOfDeathSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = IncendiaryCloudAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MomentOfPrescienceAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MeteorSwarmSpell;
                    c.SpellLevel = 9;
                });
            });
            var EnlightnedPhilosopherApocalypseSpells = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherApocalypseSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DoomSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = OwlsWisdomSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = RemoveBlindnessSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ConfusionSpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueSeeingSpell;
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = OwlsWisdomMassSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = TrueSeeingMassSpell;
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MindBlankSpell;
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MindBlankMassSpell;
                    c.SpellLevel = 9;
                });
            });
            var OceansEchoApocalypseSpells = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoApocalypseSpells", bp => {
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = DoomSpell;
                    c.SpellLevel = 1;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SoundBurstSpell;
                    c.SpellLevel = 2;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ExplosiveRunesSpell;
                    c.SpellLevel = 3;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutSpell;
                    c.SpellLevel = 4;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = SongOfDiscordSpell;
                    c.SpellLevel = 5;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = ShoutGreaterSpell;
                    c.SpellLevel = 6;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = BrilliantInspirationSpell;
                    c.SpellLevel = 7;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MomentOfPrescienceAbility.ToReference<BlueprintAbilityReference>();
                    c.SpellLevel = 8;
                });
                bp.AddComponent<AddKnownSpell>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Spell = MeteorSwarmSpell;
                    c.SpellLevel = 9;
                });
            });
            #region Final Revelation
            var BestowCurseFeebleBodyBuff = Resources.GetBlueprint<BlueprintBuff>("c092750ba895e014cb24a25e2e8274a7");
            var BestowCurseWeaknessBuff = Resources.GetBlueprint<BlueprintBuff>("de92c96c86cb2cd4c8eb8e2881b84d99");
            var BestowCurseIdiocyBuff = Resources.GetBlueprint<BlueprintBuff>("7fbb7799e8684434e80487cef9cc7f09");
            var BestowCurseDeteriorationBuff = Resources.GetBlueprint<BlueprintBuff>("caae9592917719a41b601b678a8e6ddf");
            var WeaponFocusGreater = Resources.GetBlueprint<BlueprintParametrizedFeature>("09c9e82965fb4334b984a1e9df3bd088");
            var ImprovedCritical = Resources.GetBlueprint<BlueprintParametrizedFeature>("f4201c85a991369408740c6888362e20");

            var OracleApocalypseLazyCombatBuff = Helpers.CreateBuff("OracleApocalypseLazyCombatBuff", bp => {
                bp.SetName("Not in combat");
                bp.SetDescription("");
                bp.IsClassFeature = true;
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;

            });

            var OracleApocalypseFinalFeebleBodyBuff = Helpers.CreateBuff("OracleApocalypseFinalFeebleBodyBuff", bp => {
                bp.SetName("Critical Curse - Feeble Body");
                bp.SetDescription("During this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability." +
                    "\r\nCurse of Feeble Body — The subject suffers a –6 {g|Encyclopedia:Penalty}penalty{/g} to {g|Encyclopedia:Constitution}Constitution{/g} score.");
                bp.m_Icon = ApocalypseMysteryIcon;
                bp.AddComponent<CombatStateTrigger>(c => {
                    c.CombatStartActions = Helpers.CreateActionList();
                    c.CombatEndActions = Helpers.CreateActionList( new ContextActionRemoveSelf() );
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = true;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Ranged;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = true;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(                        
                        new ContextActionApplyBuff() {
                            m_Buff = BestowCurseFeebleBodyBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue(),
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = true,
                        },
                        new ContextActionRemoveSelf()
                        );
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var OracleApocalypseFinalWeaknessBuff = Helpers.CreateBuff("OracleApocalypseFinalWeaknessBuff", bp => {
                bp.SetName("Critical Curse - Weakness");
                bp.SetDescription("During this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability." +
                    "\r\nCurse of Weakness — The subject suffers a –6 penalty to {g|Encyclopedia:Strength}Strength{/g} and {g|Encyclopedia:Dexterity}Dexterity{/g} scores.");
                bp.m_Icon = ApocalypseMysteryIcon;
                bp.AddComponent<CombatStateTrigger>(c => {
                    c.CombatStartActions = Helpers.CreateActionList();
                    c.CombatEndActions = Helpers.CreateActionList(new ContextActionRemoveSelf());
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = true;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Ranged;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = true;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BestowCurseWeaknessBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue(),
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = true,
                        },
                        new ContextActionRemoveSelf()
                        );
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var OracleApocalypseFinalIdiocyBuff = Helpers.CreateBuff("OracleApocalypseFinalIdiocyBuff", bp => {
                bp.SetName("Critical Curse - Idiocy");
                bp.SetDescription("During this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability." +
                    "\r\nCurse of Idiocy — The subject suffers a –6 penalty to {g|Encyclopedia:Intelligence}Intelligence{/g}, {g|Encyclopedia:Wisdom}Wisdom{/g}, " +
                    "and {g|Encyclopedia:Charisma}Charisma{/g} scores.");
                bp.m_Icon = ApocalypseMysteryIcon;
                bp.AddComponent<CombatStateTrigger>(c => {
                    c.CombatStartActions = Helpers.CreateActionList();
                    c.CombatEndActions = Helpers.CreateActionList(new ContextActionRemoveSelf());
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = true;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Ranged;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = true;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BestowCurseIdiocyBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue(),
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = true,
                        },
                        new ContextActionRemoveSelf()
                        );
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });
            var OracleApocalypseFinalDeteriorationBuff = Helpers.CreateBuff("OracleApocalypseFinalDeteriorationBuff", bp => {
                bp.SetName("Critical Curse - Deterioration");
                bp.SetDescription("During this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability." +
                    "\r\nCurse of Idiocy — The subject suffers a –6 penalty to {g|Encyclopedia:Intelligence}Intelligence{/g}, {g|Encyclopedia:Wisdom}Wisdom{/g}, " +
                    "and {g|Encyclopedia:Charisma}Charisma{/g} scores.");
                bp.m_Icon = ApocalypseMysteryIcon;
                bp.AddComponent<CombatStateTrigger>(c => {
                    c.CombatStartActions = Helpers.CreateActionList();
                    c.CombatEndActions = Helpers.CreateActionList(new ContextActionRemoveSelf());
                });
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = true;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = false;
                    c.Category = WeaponCategory.UnarmedStrike;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.None;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Ranged;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = true;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = BestowCurseDeteriorationBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = new ContextValue(),
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Simple,
                                    Value = 1,
                                    ValueRank = AbilityRankType.Default,
                                    ValueShared = AbilitySharedValue.Damage,
                                    Property = UnitProperty.None
                                },
                                m_IsExtendable = true
                            },
                            IsFromSpell = true,
                        },
                        new ContextActionRemoveSelf()
                        );
                });
                bp.IsClassFeature = true;
                bp.m_Flags = 0;
                bp.Stacking = StackingType.Replace;
            });

            var OracleApocalypseFinalCriticalCurseAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleApocalypseFinalCriticalCurseAbility", bp => {
                bp.SetName("Critical Curse");
                bp.SetDescription("While in combat you may expend a swift action; during this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability." +
                    "\nBestow Curse" +
                    "\nYou place a curse on the subject. Choose one of the following:" +
                    "\r\nCurse of Feeble Body — The subject suffers a –6 {g|Encyclopedia:Penalty}penalty{/g} to {g|Encyclopedia:Constitution}Constitution{/g} score." +
                    "\r\nCurse of Weakness — The subject suffers a –6 penalty to {g|Encyclopedia:Strength}Strength{/g} and {g|Encyclopedia:Dexterity}Dexterity{/g} scores." +
                    "\r\nCurse of Idiocy — The subject suffers a –6 penalty to {g|Encyclopedia:Intelligence}Intelligence{/g}, {g|Encyclopedia:Wisdom}Wisdom{/g}, " +
                    "and {g|Encyclopedia:Charisma}Charisma{/g} scores." +
                    "\r\nCurse of Deterioration — The subject suffers a –4 penalty on {g|Encyclopedia:Attack}attack rolls{/g}, {g|Encyclopedia:Saving_Throw}saves{/g}, " +
                    "{g|Encyclopedia:Ability_Scores}ability checks{/g}, and {g|Encyclopedia:Skills}skill{/g} {g|Encyclopedia:Check}checks{/g}.");
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { OracleApocalypseLazyCombatBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = false;
                });
                bp.m_Icon = ApocalypseMysteryIcon;
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleApocalypseFinalFeebleBodyAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleApocalypseFinalFeebleBodyAbility", bp => {
                bp.SetName("Critical Curse - Feeble Body");
                bp.SetDescription("While in combat you may expend a swift action; during this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability." +
                    "\r\nCurse of Feeble Body — The subject suffers a –6 {g|Encyclopedia:Penalty}penalty{/g} to {g|Encyclopedia:Constitution}Constitution{/g} score.");
                bp.m_Icon = ApocalypseMysteryIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleApocalypseFinalFeebleBodyBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalWeaknessBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalIdiocyBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalDeteriorationBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { OracleApocalypseLazyCombatBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.m_Parent = OracleApocalypseFinalCriticalCurseAbility.ToReference<BlueprintAbilityReference>();
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleApocalypseFinalWeaknessAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleApocalypseFinalWeaknessAbility", bp => {
                bp.SetName("Critical Curse - Weakness");
                bp.SetDescription("While in combat you may expend a swift action; during this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability." +
                    "\r\nCurse of Weakness — The subject suffers a –6 penalty to {g|Encyclopedia:Strength}Strength{/g} and {g|Encyclopedia:Dexterity}Dexterity{/g} scores.");
                bp.m_Icon = ApocalypseMysteryIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleApocalypseFinalWeaknessBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalFeebleBodyBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalIdiocyBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalDeteriorationBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { OracleApocalypseLazyCombatBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.m_Parent = OracleApocalypseFinalCriticalCurseAbility.ToReference<BlueprintAbilityReference>();
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleApocalypseFinalIdiocyAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleApocalypseFinalIdiocyAbility", bp => {
                bp.SetName("Critical Curse - Idiocy");
                bp.SetDescription("While in combat you may expend a swift action; during this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability." +
                    "\r\nCurse of Idiocy — The subject suffers a –6 penalty to {g|Encyclopedia:Intelligence}Intelligence{/g}, {g|Encyclopedia:Wisdom}Wisdom{/g}, " +
                    "and {g|Encyclopedia:Charisma}Charisma{/g} scores.");
                bp.m_Icon = ApocalypseMysteryIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleApocalypseFinalIdiocyBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalFeebleBodyBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalWeaknessBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalDeteriorationBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { OracleApocalypseLazyCombatBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.m_Parent = OracleApocalypseFinalCriticalCurseAbility.ToReference<BlueprintAbilityReference>();
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleApocalypseFinalDeteriorationAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleApocalypseFinalDeteriorationAbility", bp => {
                bp.SetName("Critical Curse - Deterioration");
                bp.SetDescription("While in combat you may expend a swift action; during this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability." +
                    "\r\nCurse of Idiocy — The subject suffers a –6 penalty to {g|Encyclopedia:Intelligence}Intelligence{/g}, {g|Encyclopedia:Wisdom}Wisdom{/g}, " +
                    "and {g|Encyclopedia:Charisma}Charisma{/g} scores.");
                bp.m_Icon = ApocalypseMysteryIcon;
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleApocalypseFinalDeteriorationBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalFeebleBodyBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalWeaknessBuff.ToReference<BlueprintBuffReference>() },
                        new ContextActionRemoveBuff() { m_Buff = OracleApocalypseFinalIdiocyBuff.ToReference<BlueprintBuffReference>() }
                        );
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { OracleApocalypseLazyCombatBuff.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = false;
                });
                bp.m_AllowNonContextActions = false;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.HasFastAnimation = false;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.m_Parent = OracleApocalypseFinalCriticalCurseAbility.ToReference<BlueprintAbilityReference>();
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            OracleApocalypseFinalCriticalCurseAbility.AddComponent<AbilityVariants>(c => {
                c.m_Variants = new BlueprintAbilityReference[] {
                    OracleApocalypseFinalFeebleBodyAbility.ToReference<BlueprintAbilityReference>(),
                    OracleApocalypseFinalWeaknessAbility.ToReference<BlueprintAbilityReference>(),
                    OracleApocalypseFinalIdiocyAbility.ToReference<BlueprintAbilityReference>(),
                    OracleApocalypseFinalDeteriorationAbility.ToReference<BlueprintAbilityReference>()
                };
            });

            var OracleApocalypseFinalRevelation = Helpers.CreateBlueprint<BlueprintFeature>("OracleApocalypseFinalRevelation", bp => {
                bp.SetName("Final Revelation");
                bp.SetDescription("Upon reaching 20th level, you become a herald of the apocalypse and wield the awesome power to fulfill such prophecy. " +
                    "Anytime you successfully cast a spell or use an ability that bestows 1 or more negative levels, you then bestow an 1d4 additional negative levels." +
                    "\nAdditionally, while in combat you may expend a swift action; during this combat the next creature you confirm a critical hit against is cursed " +
                    "by your attack as the bestow curse spell, except the target doesn’t receive a Will saving throw to negate the effects and spell resistance does " +
                    "not apply against this ability.");
                bp.AddComponent<AddExtraDrainToEnergyDrainInstance>(c => {
                    c.CheckSpellDescriptor = false;
                    c.SpellDescriptor = SpellDescriptor.None;
                    c.SpellsOnly = false;
                    c.Value = new ContextDiceValue() {
                        DiceType = DiceType.D4,
                        DiceCountValue = 1,
                        BonusValue = 0
                    };
                });
                bp.AddComponent<CombatStateTrigger>(c => {
                    c.CombatStartActions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleApocalypseLazyCombatBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = true,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                BonusValue = 0,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                m_IsExtendable = true
                            },
                            DurationSeconds = 0
                        });
                    c.CombatEndActions = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            ToCaster = true,
                            m_Buff = OracleApocalypseLazyCombatBuff.ToReference<BlueprintBuffReference>()
                        }
                        );
                });                
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleApocalypseFinalFeebleBodyAbility.ToReference<BlueprintUnitFactReference>(),
                        OracleApocalypseFinalIdiocyAbility.ToReference<BlueprintUnitFactReference>(),
                        OracleApocalypseFinalWeaknessAbility.ToReference<BlueprintUnitFactReference>(),
                        OracleApocalypseFinalDeteriorationAbility.ToReference<BlueprintUnitFactReference>(),
                        OracleApocalypseFinalCriticalCurseAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.m_AllowNonContextActions = false;
                bp.HideInUI = false;
                bp.HideInCharacterSheetAndLevelUp = false;
                bp.IsClassFeature = true;
            });
            #endregion
            //Main Mystery Feature
            var OracleApocalypseMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleApocalypseMysteryFeature", bp => {
                bp.m_Icon = ApocalypseMysteryIcon;
                bp.SetName("Apocalypse");
                bp.SetDescription("An oracle with the apocalypse mystery adds {g|Encyclopedia:Lore_Nature}Lore (nature){/g}, {g|Encyclopedia:Persuasion}Persuasion{/g} " +
                    "and {g|Encyclopedia:Stealth}Stealth{/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    };
                    c.Level = 20;
                    c.m_Feature = OracleApocalypseFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleApocalypseSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillStealth;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //EnlightnedPhilosopherMystery
            var EnlightnedPhilosopherApocalypseMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("EnlightnedPhilosopherApocalypseMysteryFeature", bp => {
                bp.m_Icon = ApocalypseMysteryIcon;
                bp.SetName("Apocalypse");
                bp.SetDescription("An oracle with the apocalypse mystery adds {g|Encyclopedia:Lore_Nature}Lore (nature){/g}, {g|Encyclopedia:Persuasion}Persuasion{/g} " +
                    "and {g|Encyclopedia:Stealth}Stealth{/g} to her list of class {g|Encyclopedia:Skills}skills{/g}.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = EnlightnedPhilosopherApocalypseSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillStealth;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.EnlightenedPhilosopherMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //DivineHerbalistMystery
            var DivineHerbalistApocalypseMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("DivineHerbalistApocalypseMysteryFeature", bp => {
                bp.m_Icon = ApocalypseMysteryIcon;
                bp.SetName("Apocalypse");
                bp.SetDescription("Gain access to the spells and revelations of the apocalypse mystery. \nDue to the divine herbalist archetype the class skills gained from this archetype" +
                    "are replaced by the master herbalist feature and the brew potions feat.");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleApocalypseFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OracleApocalypseSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.DivineHerbalistMystery };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            //Ocean's Echo
            var OceansEchoApocalypseMysteryFeature = Helpers.CreateBlueprint<BlueprintFeature>("OceansEchoApocalypseMysteryFeature", bp => {
                bp.m_Icon = ApocalypseMysteryIcon;
                bp.SetName("Apocalypse");
                bp.SetDescription("Gain access to the spells and revelations of the apocalypse mystery. \nDue to the ocean's echo archetype the class skills gained from this archtype" +
                    "are changed to {g|Encyclopedia:Persuasion}Persuasion{/g}, {g|Encyclopedia:Knowledge_World}Knowledge (world){/g} and {g|Encyclopedia:Lore_Nature}Lore (nature){/g}");
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 20;
                    c.m_Feature = OracleApocalypseFinalRevelation.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Level = 2;
                    c.m_Feature = OceansEchoApocalypseSpells.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillLoreNature;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillPersuasion;
                });
                bp.AddComponent<AddClassSkill>(c => {
                    c.Skill = StatType.SkillKnowledgeWorld;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.None };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            #region Defy Elements
            var OracleRevelationDefyElementsAcidRank = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDefyElementsAcidRank", bp => {
                bp.SetName("Defy Elements - Acid");
                bp.SetDescription("You gain resistance 5 to the acid energy damage, this can be chosen on future selection of the defy elements " +
                    "revelation to increase the effect to a maximum resistance of 20.");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.Ranks = 4;
            });
            var OracleRevelationDefyElementsColdRank = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDefyElementsColdRank", bp => {
                bp.SetName("Defy Elements - Cold");
                bp.SetDescription("You gain resistance 5 to the cold energy damage, this can be chosen on future selection of the defy elements " +
                    "revelation to increase the effect to a maximum resistance of 20.");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.Ranks = 4;
            });
            var OracleRevelationDefyElementsFireRank = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDefyElementsFireRank", bp => {
                bp.SetName("Defy Elements - Fire");
                bp.SetDescription("You gain resistance 5 to the fire energy damage, this can be chosen on future selection of the defy elements " +
                    "revelation to increase the effect to a maximum resistance of 20.");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.Ranks = 4;
            });
            var OracleRevelationDefyElementsElectricityRank = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDefyElementsElectricityRank", bp => {
                bp.SetName("Defy Elements - Electricity");
                bp.SetDescription("You gain resistance 5 to the electricity energy damage, this can be chosen on future selection of the defy elements " +
                    "revelation to increase the effect to a maximum resistance of 20.");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.Ranks = 4;
            });
            var OracleRevelationDefyElementsSonicRank = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDefyElementsSonicRank", bp => {
                bp.SetName("Defy Elements - Sonic");
                bp.SetDescription("You gain resistance 5 to the sonic energy damage, this can be chosen on future selection of the defy elements " +
                    "revelation to increase the effect to a maximum resistance of 20.");
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
                bp.Ranks = 4;
            });
            var OracleRevelationDefyElementsSelection = Helpers.CreateBlueprint<BlueprintFeatureSelection>("OracleRevelationDefyElementsSelection", bp => {
                bp.SetName("Defy Elements");
                bp.SetDescription("Choose one energy type (acid, cold, fire, electricity, or sonic). You gain resistance 5 to the selected energy type. " +
                    "At 5th level and every 5 levels thereafter, you can choose an additional energy type for which to gain resistance 5, or you can choose a " +
                    "previously chosen energy type and increase that resistance by 5 (to a maximum resistance of 20 for any one energy type).");
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Ranks = 5;
                bp.IsClassFeature = true;
                bp.AddFeatures(
                    OracleRevelationDefyElementsAcidRank, 
                    OracleRevelationDefyElementsColdRank, 
                    OracleRevelationDefyElementsFireRank, 
                    OracleRevelationDefyElementsElectricityRank, 
                    OracleRevelationDefyElementsSonicRank
                    );
            });

            var OracleRevelationDefyElementsResistanceFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDefyElementsResistanceFeature", bp => {
                bp.SetName("Defy Elements Res Handler");
                bp.SetDescription("");
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Acid;
                    c.Value = 5;
                    c.UseValueMultiplier = true;
                    c.ValueMultiplier = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.Default
                    };
                });
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Cold;
                    c.Value = 5;
                    c.UseValueMultiplier = true;
                    c.ValueMultiplier = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.DamageDice
                    };
                });
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Fire;
                    c.Value = 5;
                    c.UseValueMultiplier = true;
                    c.ValueMultiplier = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.DamageDiceAlternative
                    };
                });
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Electricity;
                    c.Value = 5;
                    c.UseValueMultiplier = true;
                    c.ValueMultiplier = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.DamageBonus
                    };
                });
                bp.AddComponent<AddDamageResistanceEnergy>(c => {
                    c.Type = DamageEnergyType.Sonic;
                    c.Value = 5;
                    c.UseValueMultiplier = true;
                    c.ValueMultiplier = new ContextValue() {
                        ValueType = ContextValueType.Rank,
                        ValueRank = AbilityRankType.StatBonus
                    };
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;//Acid
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureRank;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Feature = OracleRevelationDefyElementsAcidRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDice;//Cold
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureRank;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Feature = OracleRevelationDefyElementsColdRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageDiceAlternative;//Fire
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureRank;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Feature = OracleRevelationDefyElementsFireRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageBonus;//Electricity
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureRank;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Feature = OracleRevelationDefyElementsElectricityRank.ToReference<BlueprintFeatureReference>();
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.StatBonus;//Sonic
                    c.m_BaseValueType = ContextRankBaseValueType.FeatureRank;
                    c.m_Progression = ContextRankProgression.AsIs;
                    c.m_Feature = OracleRevelationDefyElementsSonicRank.ToReference<BlueprintFeatureReference>();
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });

            var OracleRevelationDefyElementsProgression = Helpers.CreateBlueprint<BlueprintProgression>("OracleRevelationDefyElementsProgression", bp => {
                bp.SetName("Defy Elements");
                bp.SetDescription("Choose one energy type (acid, cold, fire, electricity, or sonic). You gain resistance 5 to the selected energy type. " +
                    "At 5th level and every 5 levels thereafter, you can choose an additional energy type for which to gain resistance 5, or you can choose a " +
                    "previously chosen energy type and increase that resistance by 5 (to a maximum resistance of 20 for any one energy type).");
                bp.m_Classes = new BlueprintProgression.ClassWithLevel[] {
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    },
                    new BlueprintProgression.ClassWithLevel {
                        m_Class = ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.m_Archetypes = new BlueprintProgression.ArchetypeWithLevel[] {                    
                    new BlueprintProgression.ArchetypeWithLevel {
                        m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                        AdditionalLevel = 0
                    }
                };
                bp.LevelEntries = new LevelEntry[] {
                    Helpers.LevelEntry(1, OracleRevelationDefyElementsResistanceFeature, OracleRevelationDefyElementsSelection),
                    Helpers.LevelEntry(5, OracleRevelationDefyElementsSelection),
                    Helpers.LevelEntry(10, OracleRevelationDefyElementsSelection),
                    Helpers.LevelEntry(15, OracleRevelationDefyElementsSelection),
                    Helpers.LevelEntry(20, OracleRevelationDefyElementsSelection)
                };
                bp.GiveFeaturesForPreviousLevels = true;
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationDefyElementsProgression.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Doomsayer
            var ShakenBuff = Resources.GetBlueprint<BlueprintBuff>("25ec6cb6ab1845c48a95f9c20b034220");
            var OracleRevelationDoomsayerShakenBuff = Helpers.CreateBuff("OracleRevelationDoomsayerShakenBuff", bp => {
                bp.SetName("Doomsayers Prophecy");
                bp.SetDescription("This debuff has the same effect as being shaken, with the addition of being able to be extended by the doomsayers actions. " +
                    "\nShaken" +
                    "\nA shaken character takes a –2 {g|Encyclopedia:Penalty}penalty{/g} on {g|Encyclopedia:Attack}attack rolls{/g}, {g|Encyclopedia:Saving_Throw}saving throws{/g}, " +
                    "{g|Encyclopedia:Skills}skill checks{/g}, and {g|Encyclopedia:Ability_Scores}ability checks{/g}. Shaken is a less severe state of fear than " +
                    "{g|ConditionFrightened}frightened{/g}.");
                bp.AddComponent<AddCondition>(c => {
                    c.Condition = Kingmaker.UnitLogic.UnitCondition.Shaken;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.MindAffecting | SpellDescriptor.Fear | SpellDescriptor.Shaken;
                });
                bp.m_Icon = ShakenBuff.Icon;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = ShakenBuff.FxOnStart;
            });
            var OracleRevelationDoomsayerCooldownBuff = Helpers.CreateBuff("OracleRevelationDoomsayerCooldownBuff", bp => {
                bp.m_AllowNonContextActions = false;
                bp.SetName("Doomsayer - Once Per Round");
                bp.SetDescription("");
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.Stacking = StackingType.Replace;
                bp.Frequency = DurationRate.Rounds;
            });
            var OracleRevelationDoomsayerAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationDoomsayerAbility", bp => {
                bp.SetName("Doomsayer");
                bp.SetDescription("As a standard action, you can utter a dire prophecy that strikes fear in enemies within 30 feet, causing them to become shaken until the end of your next round.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionIsCaster() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleRevelationDoomsayerCooldownBuff.ToReference<BlueprintBuffReference>(),
                                    Permanent = false,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 1
                                    },
                                    DurationSeconds = 0,
                                    ToCaster = true
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
                                new Conditional() {
                                    ConditionsChecker = new ConditionsChecker() {
                                        Operation = Operation.Or,
                                        Conditions = new Condition[] {
                                            new ContextConditionIsEnemy() {
                                                Not = false
                                            }
                                        }
                                    },
                                    IfTrue = Helpers.CreateActionList(
                                        new ContextActionApplyBuff() {
                                            m_Buff = OracleRevelationDoomsayerShakenBuff.ToReference<BlueprintBuffReference>(),
                                            Permanent = false,
                                            UseDurationSeconds = false,
                                            DurationValue = new ContextDurationValue() {
                                                Rate = DurationRate.Rounds,
                                                DiceType = DiceType.Zero,
                                                DiceCountValue = 0,
                                                BonusValue = 2
                                            },
                                            DurationSeconds = 0
                                        }
                                        ),
                                    IfFalse = Helpers.CreateActionList()
                                }
                                )
                        }                        
                        );
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = new Feet() { m_Value = 30 };
                    c.m_TargetType = TargetType.Any;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker() { };
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.MindAffecting | SpellDescriptor.Fear;
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationDoomsayerCooldownBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.AOE;
                    c.SpellType = CraftSpellType.Debuff;
                });
                bp.m_Icon = ShakenBuff.Icon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationDoomsayerAbility.Duration", "2 rounds");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var OracleRevelationDoomsayerExtendAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationDoomsayerExtendAbility", bp => {
                bp.SetName("Doomsayer - Extend");
                bp.SetDescription("Continue doomsaying as a move action, extending the effect an additional round on all enemies withing range.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionHasBuffFromCaster() {
                                        Not = false,
                                        m_Buff = OracleRevelationDoomsayerShakenBuff.ToReference<BlueprintBuffReference>()
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
                                new ContextActionReduceBuffDuration() {
                                    m_TargetBuff = OracleRevelationDoomsayerShakenBuff.ToReference<BlueprintBuffReference>(),
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Rounds,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 1,
                                        m_IsExtendable = true
                                    },
                                    Increase = true,
                                    ToTarget = true
                                }
                                ),
                            IfFalse = Helpers.CreateActionList()
                        },                        
                        new ContextActionApplyBuff() {
                            m_Buff = OracleRevelationDoomsayerCooldownBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0,
                            ToCaster = true
                        }
                        );
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = new Feet() { m_Value = 30 };
                    c.m_TargetType = TargetType.Any;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker() {};
                });
                bp.AddComponent<AbilityCasterHasNoFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { OracleRevelationDoomsayerCooldownBuff.ToReference<BlueprintUnitFactReference>() };
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.AOE;
                    c.SpellType = CraftSpellType.Debuff;
                });
                bp.m_Icon = ShakenBuff.Icon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Move;
                bp.AvailableMetamagic = Metamagic.Quicken;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var OracleRevelationDoomsayerSwiftExtend = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDoomsayerSwiftExtend", bp => {
                bp.SetName("OracleRevelationDoomsayerSwiftExtend");
                bp.SetDescription("");
                bp.AddComponent<AutoMetamagic>(c => {
                    c.m_AllowedAbilities = AutoMetamagic.AllowedType.Any;
                    c.Metamagic = Metamagic.Quicken;
                    c.Abilities = new List<BlueprintAbilityReference> { OracleRevelationDoomsayerExtendAbility.ToReference<BlueprintAbilityReference>() };
                });
                bp.HideInUI = true;
                bp.HideInCharacterSheetAndLevelUp = true;
                bp.IsClassFeature = true;
            });

            var OracleRevelationDoomsayerFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDoomsayerFeature", bp => {
                bp.SetName("Doomsayer");
                bp.SetDescription("As a standard action, you can utter a dire prophecy that strikes fear in enemies within 30 feet, causing them to become shaken until the end of your next round. " +
                    "During a round you have not used this ability, you can continue doomsaying as a move action, extending the effect an additional round on all enemies withing range. " +
                    "This ability cannot cause a creature to become frightened or panicked, even if the target was already shaken from another effect; and for the purpose of immunities is a mind altering fear effect. " +
                    "\nAt 15th level you may extend the shaken effect as a swift action instead. " +
                    "\nYou must be at least 7th level to select this revelation.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] { 
                        OracleRevelationDoomsayerAbility.ToReference<BlueprintUnitFactReference>(),
                        OracleRevelationDoomsayerExtendAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddFeatureOnClassLevel>(c => {
                    c.m_Class = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_AdditionalClasses = new BlueprintCharacterClassReference[] {
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.m_Archetypes = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>(),
                    };
                    c.Level = 15;
                    c.m_Feature = OracleRevelationDoomsayerSwiftExtend.ToReference<BlueprintFeatureReference>();
                    c.BeforeThisLevel = false;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.AddComponent<PrerequisiteClassLevel>(c => {
                    c.m_CharacterClass = OracleClass.ToReference<BlueprintCharacterClassReference>();
                    c.Group = Prerequisite.GroupType.Any;
                    c.Level = 7;
                });                
                bp.AddComponent<PrerequisiteArchetypeLevel>(c => {
                    c.Group = Prerequisite.GroupType.Any;
                    c.CheckInProgression = false;
                    c.HideInUI = false;
                    c.m_CharacterClass = ArcanistClass.ToReference<BlueprintCharacterClassReference>();
                    c.m_Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                    c.Level = 7;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationDoomsayerFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Dust to Dust
            var StaggeredIcon = Resources.GetBlueprint<BlueprintBuff>("df3950af5a783bd4d91ab73eb8fa0fd3").Icon;
            var OracleRevelationDustToDustResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationDustToDustResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 0,
                    IncreasedByLevel = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    m_Class = new BlueprintCharacterClassReference[0],
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    m_Archetypes = new BlueprintArchetypeReference[0],
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    },
                    StartingLevel = 1,
                    LevelStep = 9,
                    StartingIncrease = 1,
                    PerStepIncrease = 1,
                };
                bp.m_UseMax = true;
                bp.m_Max = 2;
            });
            var OracleRevelationDustToDustAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationDustToDustAbility", bp => {
                bp.SetName("Dust to Dust");
                bp.SetDescription("Once per day as a standard action, you can cause the weapons around you to shatter in their wielders’ hands. " +
                    "When you use this ability, attempt a disarm combat maneuver against every creature in a 10-foot radius, using your caster level in place of your base attack bonus " +
                    "and your Charisma modifier in place of Strength. \nAt 10th level, you can use this ability twice per day.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionCombatManeuver() {
                            Type = CombatManeuver.Disarm,
                            IgnoreConcealment = true,
                            OnSuccess = Helpers.CreateActionList(),
                            ReplaceStat = true,
                            NewStat = StatType.Charisma,
                            UseKineticistMainStat = false,
                            UseCastingStat = false,
                            UseCasterLevelAsBaseAttack = true,
                            UseBestMentalStat = false,
                            BatteringBlast = false
                        }
                        );
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = new Feet() { m_Value = 10 };
                    c.m_TargetType = TargetType.Enemy;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker() {
                        Conditions = new Condition[] {
                            new ContextConditionIsCaster() {
                                Not = true
                            }
                        }
                    };
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationDustToDustResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.None;
                    c.AOEType = CraftAOE.AOE;
                    c.SpellType = CraftSpellType.Debuff;
                });
                bp.m_Icon = StaggeredIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });
            var OracleRevelationDustToDustFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationDustToDustFeature", bp => {
                bp.SetName("Dust to Dust");
                bp.SetDescription("Once per day as a standard action, you can cause the weapons around you to shatter in their wielders’ hands. " +
                    "When you use this ability, attempt a disarm combat maneuver against every creature in a 10-foot radius, using your caster level in place of your base attack bonus " +
                    "and your Charisma modifier in place of Strength. \nAt 10th level, you can use this ability twice per day.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationDustToDustAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationDustToDustResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationDustToDustFeature.ToReference<BlueprintFeatureReference>());

            #endregion
            #region Erosion Touch and Near Death
            var ErosionTouch = Resources.GetBlueprint<BlueprintFeature>("b459fee5bc4b33449bb883b0ac5a01d8").GetComponent<PrerequisiteFeaturesFromList>();
            ErosionTouch.m_Features = ErosionTouch.m_Features.AppendToArray(OracleApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>());
            ErosionTouch.m_Features = ErosionTouch.m_Features.AppendToArray(EnlightnedPhilosopherApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>());
            ErosionTouch.m_Features = ErosionTouch.m_Features.AppendToArray(DivineHerbalistApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>());
            ErosionTouch.m_Features = ErosionTouch.m_Features.AppendToArray(OceansEchoApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>());
            var NearDeath = Resources.GetBlueprint<BlueprintFeature>("96649fb9694c1164caf7b836898685aa").GetComponent<PrerequisiteFeaturesFromList>();
            NearDeath.m_Features = NearDeath.m_Features.AppendToArray(OracleApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>());
            NearDeath.m_Features = NearDeath.m_Features.AppendToArray(EnlightnedPhilosopherApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>());
            NearDeath.m_Features = NearDeath.m_Features.AppendToArray(DivineHerbalistApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>());
            NearDeath.m_Features = NearDeath.m_Features.AppendToArray(OceansEchoApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Pass the Torch 
            var FirebellyIcon = Resources.GetBlueprint<BlueprintAbility>("b065231094a21d14dbf1c3832f776871").Icon;

            var OracleRevelationPassTheTorchResource = Helpers.CreateBlueprint<BlueprintAbilityResource>("OracleRevelationPassTheTorchResource", bp => {
                bp.m_MaxAmount = new BlueprintAbilityResource.Amount {
                    BaseValue = 1,
                    IncreasedByLevel = false,
                    IncreasedByStat = false,
                    IncreasedByLevelStartPlusDivStep = true,
                    StartingLevel = 5,
                    StartingIncrease = 1,
                    LevelStep = 5,
                    PerStepIncrease = 1,
                    m_ClassDiv = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>()
                    },
                    m_ArchetypesDiv = new BlueprintArchetypeReference[] {
                        MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>()
                    }
                };
            });
            var OracleRevelationPassTheTorchRoundToken = Helpers.CreateBuff("OracleRevelationPassTheTorchRoundToken", bp => {
                bp.SetName("Pass the Torch - Round Token");
                bp.SetDescription("");
                
                bp.m_Flags = BlueprintBuff.Flags.HiddenInUi;
                bp.IsClassFeature = true;
                bp.Stacking = StackingType.Rank;
                bp.Ranks = 20;
            });

            var OracleRevelationPassTheTorchPulse = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationPassTheTorchPulse", bp => {
                bp.SetName("Pass the Torch - Pulse");
                bp.SetDescription("Once per day as a swift action, you can channel the energy of the apocalypse into your body, causing you to ignite. " +
                    "You take 1d4 points of fire damage when you activate this ability and again at the beginning of your turn until you end the effect. " +
                    "Any creature that begins the next round adjacent to you takes 1d6 points of fire damage as the fire spreads, plus 1 additional point of " +
                    "fire damage for each previous round you have had this ability active. For example, adjacent creatures take 1d6+5 points of damage if you " +
                    "have had this ability active for 5 rounds. You can use this ability each time for a number of rounds equal to 1/2 your oracle level, " +
                    "and you can end this ability as a free action. At 5th level and every 5 levels thereafter, you can use this ability one additional time per day.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new Conditional() {
                            ConditionsChecker = new ConditionsChecker() {
                                Operation = Operation.Or,
                                Conditions = new Condition[] {
                                    new ContextConditionIsCaster() {
                                        Not = false
                                    }
                                }
                            },
                            IfTrue = Helpers.CreateActionList(
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
                                    Drain = false,
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
                                        DiceType = DiceType.D4,
                                        DiceCountValue = 1,
                                        BonusValue = 0,
                                    },
                                    IsAoE = false,
                                    HalfIfSaved = false,
                                    UseMinHPAfterDamage = false,
                                    MinHPAfterDamage = 0,
                                    ResultSharedValue = AbilitySharedValue.Damage,
                                    CriticalSharedValue = AbilitySharedValue.Damage
                                },
                                new ContextActionApplyBuff() {
                                    m_Buff = OracleRevelationPassTheTorchRoundToken.ToReference<BlueprintBuffReference>(),
                                    //Permanent = true,
                                    UseDurationSeconds = false,
                                    DurationValue = new ContextDurationValue() {
                                        Rate = DurationRate.Hours,
                                        DiceType = DiceType.Zero,
                                        DiceCountValue = 0,
                                        BonusValue = 1
                                    },
                                    DurationSeconds = 0
                                }
                                ),
                            IfFalse = Helpers.CreateActionList(
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
                                    Drain = false,
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
                                        DiceCountValue = 1,
                                        BonusValue = new ContextValue() {
                                            ValueType = ContextValueType.Rank,
                                            ValueRank = AbilityRankType.DamageBonus
                                        },
                                    },
                                    IsAoE = false,
                                    HalfIfSaved = false,
                                    UseMinHPAfterDamage = false,
                                    MinHPAfterDamage = 0,
                                    ResultSharedValue = AbilitySharedValue.Damage,
                                    CriticalSharedValue = AbilitySharedValue.Damage
                                }
                                )
                        }
                        );
                });
                bp.AddComponent<AbilityTargetsAround>(c => {
                    c.m_Radius = 5.Feet();
                    c.m_TargetType = TargetType.Any;
                    c.m_IncludeDead = false;
                    c.m_Condition = new ConditionsChecker();
                    c.m_SpreadSpeed = 0.Feet();
                });
                //bp.AddComponent<AbilitySpawnFx>(c => {
                //    c.PrefabLink = new PrefabLink() { AssetId = "13eb28db3412f894795b434673d6bbd4" };
                //    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                //    c.Anchor = AbilitySpawnFxAnchor.ClickedTarget;
                //    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                //    c.DestroyOnCast = false;
                //    c.Delay = 0;
                //    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                //    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                //    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                //});
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.DamageBonus;
                    c.m_BaseValueType = ContextRankBaseValueType.CasterBuffRank;
                    c.m_Buff = OracleRevelationPassTheTorchRoundToken.ToReference<BlueprintBuffReference>();
                    c.m_BuffRankMultiplier = 1;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.AsIs;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SpellType = CraftSpellType.Damage;
                    c.SavingThrow = CraftSavingThrow.Reflex;
                    c.AOEType = CraftAOE.AOE;
                });
                bp.m_Icon = FirebellyIcon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.None;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Self;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });




            var OracleRevelationPassTheTorchBuff = Helpers.CreateBuff("OracleRevelationPassTheTorchBuff", bp => {
                bp.SetName("Pass the Torch");
                bp.SetDescription("You take 1d4 points of fire damage when you activate this ability and again at the beginning of your turn until you end the effect. " +
                    "Any creature that begins the next round adjacent to you takes 1d6 points of fire damage as the fire spreads, plus 1 additional point of " +
                    "fire damage for each previous round you have had this ability active. For example, adjacent creatures take 1d6+5 points of damage if you " +
                    "have had this ability active for 5 rounds. You can end this ability early as a free action.");                
                bp.AddComponent<AddFactContextActions>(c => {
                    c.Activated = Helpers.CreateActionList();
                    c.Deactivated = Helpers.CreateActionList();
                    c.NewRound = Helpers.CreateActionList(
                        new ContextActionCastSpell() {
                            m_Spell = OracleRevelationPassTheTorchPulse.ToReference<BlueprintAbilityReference>(),
                            OverrideDC = false,
                            DC = 0,
                            OverrideSpellLevel = false,
                            SpellLevel = 0,
                            CastByTarget = false,
                            LogIfCanNotTarget = false,
                            MarkAsChild = false//?
                        }
                        );
                });
                bp.m_Icon = FirebellyIcon;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
                bp.FxOnStart = new PrefabLink() { AssetId = "26fa35beb7d89bf4dafb93033941700c" };
            });
            

            var OracleRevelationPassTheTorchAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationPassTheTorchAbility", bp => {
                bp.SetName("Pass the Torch");
                bp.SetDescription("Once per day as a swift action, you can channel the energy of the apocalypse into your body, causing you to ignite. " +
                    "You take 1d4 points of fire damage when you activate this ability and again at the beginning of your turn until you end the effect. " +
                    "Any creature that begins the next round adjacent to you takes 1d6 points of fire damage as the fire spreads, plus 1 additional point of " +
                    "fire damage for each previous round you have had this ability active. For example, adjacent creatures take 1d6+5 points of damage if you " +
                    "have had this ability active for 5 rounds. You can use this ability each time for a number of rounds equal to 1/2 your oracle level, " +
                    "and you can end this ability as a free action. At 5th level and every 5 levels thereafter, you can use this ability one additional time per day.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleRevelationPassTheTorchBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Rounds,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = new ContextValue() {
                                    ValueType = ContextValueType.Rank,
                                    ValueRank = AbilityRankType.Default
                                }
                            },
                            DurationSeconds = 0
                        },
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
                            Drain = false,
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
                                DiceType = DiceType.D4,
                                DiceCountValue = 1,
                                BonusValue = 0,
                            },
                            IsAoE = false,
                            HalfIfSaved = false,
                            UseMinHPAfterDamage = false,
                            MinHPAfterDamage = 0,
                            ResultSharedValue = AbilitySharedValue.Damage,
                            CriticalSharedValue = AbilitySharedValue.Damage
                        }
                        ,
                        new ContextActionApplyBuff() {
                            m_Buff = OracleRevelationPassTheTorchRoundToken.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Hours,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0
                        }
                        );
                });
                bp.AddComponent<ContextRankConfig>(c => {
                    c.m_Type = AbilityRankType.Default;
                    c.m_BaseValueType = ContextRankBaseValueType.SummClassLevelWithArchetype;
                    c.m_Stat = StatType.Unknown;
                    c.m_SpecificModifier = ModifierDescriptor.None;
                    c.m_Progression = ContextRankProgression.Div2;
                    c.m_StartLevel = 0;
                    c.m_StepLevel = 0;
                    c.m_UseMin = true;
                    c.m_Min = 1;
                    c.m_UseMax = false;
                    c.m_Max = 0;
                    c.m_Class = new BlueprintCharacterClassReference[] {
                        OracleClass.ToReference<BlueprintCharacterClassReference>(),
                        ArcanistClass.ToReference<BlueprintCharacterClassReference>(),
                    };
                    c.Archetype = MagicDeceiverArchetype.ToReference<BlueprintArchetypeReference>();
                });
                bp.AddComponent<AbilityResourceLogic>(c => {
                    c.m_RequiredResource = OracleRevelationPassTheTorchResource.ToReference<BlueprintAbilityResourceReference>();
                    c.m_IsSpendResource = true;
                });
                bp.m_Icon = FirebellyIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = Helpers.CreateString("OracleRevelationPassTheTorchAbility.Duration", "1 round/2 levels, unless ended by caster");
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var OracleRevelationPassTheTorchRemoveAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationPassTheTorchRemoveAbility", bp => {
                bp.SetName("Pass the Torch - Early Remove");
                bp.SetDescription("You may end the effect of pass the torch early by removing the buff as a free action.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionRemoveBuff() {
                            m_Buff = OracleRevelationPassTheTorchBuff.ToReference<BlueprintBuffReference>()
                        },
                        new ContextActionRemoveBuff() {
                            m_Buff = OracleRevelationPassTheTorchRoundToken.ToReference<BlueprintBuffReference>()
                        }
                        );
                });
                bp.m_Icon = FirebellyIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Free;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            OracleRevelationPassTheTorchRoundToken.AddComponent<AddAbilityUseTrigger>(c => {
                c.ActionsOnAllTargets = false;
                c.AfterCast = false;
                c.ActionsOnTarget = false;
                c.FromSpellbook = false;
                c.m_Spellbooks = new BlueprintSpellbookReference[] { };
                c.ForOneSpell = false;
                c.m_Ability = new BlueprintAbilityReference();
                c.ForMultipleSpells = true;
                c.Abilities = new List<BlueprintAbilityReference>() {
                    OracleRevelationPassTheTorchAbility.ToReference<BlueprintAbilityReference>(),
                    OracleRevelationPassTheTorchRemoveAbility.ToReference<BlueprintAbilityReference>()
                };
                c.MinSpellLevel = false;
                c.MinSpellLevelLimit = 0;
                c.ExactSpellLevel = false;
                c.ExactSpellLevelLimit = 0;
                c.CheckAbilityType = false;
                c.Type = AbilityType.Spell;
                c.CheckDescriptor = false;
                c.SpellDescriptor = new SpellDescriptor();
                c.CheckRange = false;
                c.Range = AbilityRange.Touch;
                c.Action = Helpers.CreateActionList(
                    new ContextActionOnContextCaster() {
                        Actions = Helpers.CreateActionList(
                            new ContextActionRemoveBuff() {
                                m_Buff = OracleRevelationPassTheTorchRoundToken.ToReference<BlueprintBuffReference>()
                            }
                            )
                    });
            });





            var OracleRevelationPassTheTorchFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationPassTheTorchFeature", bp => {
                bp.SetName("Pass the Torch");
                bp.SetDescription("Once per day as a swift action, you can channel the energy of the apocalypse into your body, causing you to ignite. " +
                    "You take 1d4 points of fire damage when you activate this ability and again at the beginning of your turn until you end the effect. " +
                    "Any creature that begins the next round adjacent to you takes 1d6 points of fire damage as the fire spreads, plus 1 additional point of " +
                    "fire damage for each previous round you have had this ability active. For example, adjacent creatures take 1d6+5 points of damage if you " +
                    "have had this ability active for 5 rounds. You can use this ability each time for a number of rounds equal to 1/2 your oracle level, " +
                    "and you can end this ability as a free action. At 5th level and every 5 levels thereafter, you can use this ability one additional time per day.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationPassTheTorchAbility.ToReference<BlueprintUnitFactReference>(),
                        OracleRevelationPassTheTorchRemoveAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<AddAbilityResources>(c => {
                    c.m_Resource = OracleRevelationPassTheTorchResource.ToReference<BlueprintAbilityResourceReference>();
                    c.RestoreAmount = true;
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationPassTheTorchFeature.ToReference<BlueprintFeatureReference>());
            #endregion
            #region Spell Blast 
            var BatteringBlastIcon = Resources.GetBlueprint<BlueprintAbility>("0a2f7c6aa81bc6548ac7780d8b70bcbc").Icon;
            var OracleRevelationSpelBlastBuff = Helpers.CreateBuff("OracleRevelationSpelBlastBuff", bp => {
                bp.SetName("Spell Blast");
                bp.SetDescription("As a swift action empower your ray spells for 1 minute, whenever you confirm a critical hit against an opponent with a ranged touch attack spell, " +
                    "you immediately attempt to bull rush your opponent away from you. You don’t provoke an attack of opportunity for this bull rush attempt.");
                bp.AddComponent<AddInitiatorAttackWithWeaponTrigger>(c => {
                    c.TriggerBeforeAttack = false;
                    c.OnlyHit = true;
                    c.OnMiss = false;
                    c.OnlyOnFullAttack = false;
                    c.OnlyOnFirstAttack = false;
                    c.OnlyOnFirstHit = false;
                    c.CriticalHit = true;
                    c.OnAttackOfOpportunity = false;
                    c.NotCriticalHit = false;
                    c.OnlySneakAttack = false;
                    c.NotSneakAttack = false;
                    c.m_WeaponType = new BlueprintWeaponTypeReference();
                    c.CheckWeaponCategory = true;
                    c.Category = WeaponCategory.Ray;
                    c.CheckWeaponGroup = false;
                    c.Group = WeaponFighterGroup.Bows;
                    c.CheckWeaponRangeType = false;
                    c.RangeType = WeaponRangeType.Melee;
                    c.ActionsOnInitiator = false;
                    c.ReduceHPToZero = false;
                    c.DamageMoreTargetMaxHP = false;
                    c.CheckDistance = false;
                    c.DistanceLessEqual = new Feet(); //?
                    c.AllNaturalAndUnarmed = false;
                    c.DuelistWeapon = false;
                    c.NotExtraAttack = false;
                    c.OnCharge = false;
                    c.Action = Helpers.CreateActionList(
                        new ContextActionCombatManeuver() {
                            Type = CombatManeuver.BullRush,
                            IgnoreConcealment = true,
                            OnSuccess = Helpers.CreateActionList(),
                            ReplaceStat = false,
                            NewStat = StatType.Unknown,
                            UseKineticistMainStat = false,
                            UseCastingStat = false,
                            UseCasterLevelAsBaseAttack = false,
                            UseBestMentalStat = false,
                            BatteringBlast = false
                        }
                        );
                });
                bp.m_Icon = BatteringBlastIcon;
                bp.IsClassFeature = false;
                bp.Stacking = StackingType.Replace;
            });

            var OracleRevelationSpelBlastAbility = Helpers.CreateBlueprint<BlueprintAbility>("OracleRevelationSpelBlastAbility", bp => {
                bp.SetName("Spell Blast");
                bp.SetDescription("As a swift action empower your ray spells for 1 minute, whenever you confirm a critical hit against an opponent with a ranged touch attack spell, " +
                    "you immediately attempt to bull rush your opponent away from you. You don’t provoke an attack of opportunity for this bull rush attempt.");
                bp.AddComponent<AbilityEffectRunAction>(c => {
                    c.SavingThrowType = SavingThrowType.Unknown;
                    c.Actions = Helpers.CreateActionList(
                        new ContextActionApplyBuff() {
                            m_Buff = OracleRevelationSpelBlastBuff.ToReference<BlueprintBuffReference>(),
                            Permanent = false,
                            UseDurationSeconds = false,
                            DurationValue = new ContextDurationValue() {
                                Rate = DurationRate.Minutes,
                                DiceType = DiceType.Zero,
                                DiceCountValue = 0,
                                BonusValue = 1
                            },
                            DurationSeconds = 0
                        }
                        );
                });
                bp.m_Icon = BatteringBlastIcon;
                bp.Type = AbilityType.Supernatural;
                bp.Range = AbilityRange.Personal;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = false;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = false;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Omni;
                bp.ActionType = UnitCommand.CommandType.Swift;
                bp.AvailableMetamagic = 0;
                bp.LocalizedDuration = new Kingmaker.Localization.LocalizedString();
                bp.LocalizedSavingThrow = new Kingmaker.Localization.LocalizedString();
            });

            var OracleRevelationSpelBlastFeature = Helpers.CreateBlueprint<BlueprintFeature>("OracleRevelationSpelBlastFeature", bp => {
                bp.SetName("Spell Blast");
                bp.SetDescription("As a swift action empower your ray spells for 1 minute, whenever you confirm a critical hit against an opponent with a ranged touch attack spell, " +
                    "you immediately attempt to bull rush your opponent away from you. You don’t provoke an attack of opportunity for this bull rush attempt.");
                bp.AddComponent<AddFacts>(c => {
                    c.m_Facts = new BlueprintUnitFactReference[] {
                        OracleRevelationSpelBlastAbility.ToReference<BlueprintUnitFactReference>()
                    };
                });
                bp.AddComponent<PrerequisiteFeaturesFromList>(c => {
                    c.m_Features = new BlueprintFeatureReference[] {
                        OracleApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        EnlightnedPhilosopherApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        DivineHerbalistApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>(),
                        OceansEchoApocalypseMysteryFeature.ToReference<BlueprintFeatureReference>()
                    };
                    c.Amount = 1;
                });
                bp.Groups = new FeatureGroup[] { FeatureGroup.OracleRevelation };
                bp.m_AllowNonContextActions = false;
                bp.IsClassFeature = true;
            });
            OracleRevelationSelection.m_AllFeatures = OracleRevelationSelection.m_AllFeatures.AppendToArray(OracleRevelationSpelBlastFeature.ToReference<BlueprintFeatureReference>());
            #endregion

            MysteryTools.RegisterMystery(OracleApocalypseMysteryFeature);
            MysteryTools.RegisterSecondMystery(OracleApocalypseMysteryFeature);
            MysteryTools.RegisterEnlightendPhilosopherMystery(EnlightnedPhilosopherApocalypseMysteryFeature);
            MysteryTools.RegisterSecondEnlightendPhilosopherMystery(EnlightnedPhilosopherApocalypseMysteryFeature);
            MysteryTools.RegisterHerbalistMystery(DivineHerbalistApocalypseMysteryFeature);
            MysteryTools.RegisterSecondHerbalistMystery(DivineHerbalistApocalypseMysteryFeature);
            MysteryTools.RegisterOceansEchoMystery(OceansEchoApocalypseMysteryFeature);
            MysteryTools.RegisterSecondOceansEchoMystery(OceansEchoApocalypseMysteryFeature);
            MysteryTools.RegisterHermitMystery(OracleApocalypseMysteryFeature);
            MysteryTools.RegisterSecondHermitMystery(OracleApocalypseMysteryFeature);
            MysteryTools.RegisterMysteryGiftSelection(OracleApocalypseMysteryFeature);
        }
    }
}
