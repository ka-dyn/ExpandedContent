using ExpandedContent.Extensions;
using ExpandedContent.Utilities;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Craft;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Visual.Animation.Kingmaker.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpandedContent.Tweaks.Spells {
    internal class DazeMonster {

        public static void AddDazeMonster() {
            //var DazeMonsterIcon = AssetLoader.LoadInternal("Skills", "Icon_DazeMonster.jpg");
            //var Icon_ScrollOfDazeMonster = AssetLoader.LoadInternal("Items", "Icon_ScrollOfDazeMonster.png");

            var DazeSpell = Resources.GetBlueprint<BlueprintAbility>("55f14bc84d7c85446b07a1b5dd6b2b4c");
            var DazeBuff = Resources.GetBlueprint<BlueprintBuff>("9934fedff1b14994ea90205d189c8759");
            var UndeadType = Resources.GetBlueprint<BlueprintFeature>("734a29b693e9ec346ba2951b27987e33");
            var UndeadMindAffection = Resources.GetBlueprint<BlueprintFeature>("7853143d87baea1429bb409b023edb6b");
            var ConstructType = Resources.GetBlueprint<BlueprintFeature>("fd389783027d63343b4a5634bd81645f");


            var DazeMonsterAbility = Helpers.CreateBlueprint<BlueprintAbility>("DazeMonsterAbility", bp => {
                bp.SetName("Daze Monster");
                bp.SetDescription("This {g|Encyclopedia:Spell}spell{/g} clouds the mind of a living creature with 6 or fewer {g|Encyclopedia:Hit_Dice}Hit Dice{/g} so that it takes " +
                    "no {g|Encyclopedia:CA_Types}actions{/g}. Creatures of 7 or more HD are not affected. A {g|ConditionDaze}dazed{/g} subject is not {g|ConditionStunned}stunned{/g}, " +
                    "so attackers get no special advantage against it. After a creature has been dazed by this spell, it is immune to the effects of this spell for 1 minute.");
                bp.AddComponent(Helpers.CreateCopy(DazeSpell.GetComponent<AbilityEffectRunAction>()));
                bp.AddComponent<AbilityTargetMaximumHitDice>(c => {
                    c.UseContextInstead = false;
                    c.HitDice = 6;
                    c.ContextHitDice = 0;
                });
                bp.AddComponent<AbilitySpawnFx>(c => {
                    c.PrefabLink = new Kingmaker.ResourceLinks.PrefabLink() { AssetId = "28b3cd92c1fdc194d9ee1e378c23be6b" };
                    c.Time = AbilitySpawnFxTime.OnApplyEffect;
                    c.Anchor = AbilitySpawnFxAnchor.SelectedTarget;
                    c.WeaponTarget = AbilitySpawnFxWeaponTarget.None;
                    c.DestroyOnCast = false;
                    c.Delay = 0;
                    c.PositionAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationAnchor = AbilitySpawnFxAnchor.None;
                    c.OrientationMode = AbilitySpawnFxOrientation.Copy;
                });
                bp.AddComponent<SpellComponent>(c => {
                    c.School = SpellSchool.Enchantment;
                });
                bp.AddComponent<SpellDescriptorComponent>(c => {
                    c.Descriptor = SpellDescriptor.MindAffecting | SpellDescriptor.Compulsion;
                });
                bp.AddComponent<AbilityTargetHasNoFactUnless>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { UndeadType.ToReference<BlueprintUnitFactReference>() };
                    c.m_UnlessFact = UndeadMindAffection.ToReference<BlueprintUnitFactReference>();
                });
                bp.AddComponent<AbilityTargetHasFact>(c => {
                    c.m_CheckedFacts = new BlueprintUnitFactReference[] { ConstructType.ToReference<BlueprintUnitFactReference>() };
                    c.Inverted = true;
                });
                bp.AddComponent<CraftInfoComponent>(c => {
                    c.SavingThrow = CraftSavingThrow.Will;
                    c.AOEType = CraftAOE.None;
                    c.SpellType = CraftSpellType.Debuff;
                });
                bp.m_Icon = DazeSpell.Icon;
                bp.Type = AbilityType.Spell;
                bp.Range = AbilityRange.Close;
                bp.CanTargetPoint = false;
                bp.CanTargetEnemies = true;
                bp.CanTargetFriends = false;
                bp.CanTargetSelf = true;
                bp.SpellResistance = true;
                bp.EffectOnAlly = AbilityEffectOnUnit.None;
                bp.EffectOnEnemy = AbilityEffectOnUnit.Harmful;
                bp.Animation = UnitAnimationActionCastSpell.CastAnimationStyle.Point;
                bp.ActionType = UnitCommand.CommandType.Standard;
                bp.AvailableMetamagic = Metamagic.Quicken | Metamagic.Heighten | Metamagic.Reach | Metamagic.CompletelyNormal;
                bp.LocalizedDuration = Helpers.CreateString("DazeMonsterAbility.Duration", "1 round");
                bp.LocalizedSavingThrow = Helpers.CreateString("DazeMonsterAbility.SavingThrow", "Will negates");
            });
            //var DazeMonsterScroll = ItemTools.CreateScroll("ScrollOfDazeMonster", Icon_ScrollOfDazeMonster, DazeMonsterAbility, 2, 3);
            //VenderTools.AddScrollToLeveledVenders(DazeMonsterScroll);
            DazeMonsterAbility.AddToSpellList(SpellTools.SpellList.BardSpellList, 2);
            DazeMonsterAbility.AddToSpellList(SpellTools.SpellList.BloodragerSpellList, 2);
            DazeMonsterAbility.AddToSpellList(SpellTools.SpellList.WitchSpellList, 2);
            DazeMonsterAbility.AddToSpellList(SpellTools.SpellList.WizardSpellList, 2);
        }

    }
}
