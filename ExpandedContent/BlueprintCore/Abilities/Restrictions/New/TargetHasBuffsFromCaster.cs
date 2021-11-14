using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Root;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UI.Log;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Utility;
using System.Linq;
using UnityEngine;

namespace BlueprintCore.Abilities.Restrictions.New
{
  /// <summary>
  /// Requires the target to have specific buffs applied by the caster.
  /// </summary>
  [AllowedOn(typeof(BlueprintAbility), false)]
  public class TargetHasBuffsFromCaster : BlueprintComponent, IAbilityTargetRestriction
  {
    private static readonly LogWrapper Logger = LogWrapper.GetInternal("TargetHasBuff");

    private ReferenceArrayProxy<BlueprintBuff, BlueprintBuffReference> CheckedBuffs
    {
      get { return Buffs; }
    }

    public bool IsTargetRestrictionPassed(UnitEntityData caster, TargetWrapper target)
    {
      UnitEntityData targetUnit = target.Unit;
      if (targetUnit == null)
      {
        Logger.Warn("No target");
        return false;
      }
      if (caster == null)
      {
        Logger.Warn("No caster");
        return false;
      }

      int matchingBuffCount = 0;
      foreach (BlueprintBuff blueprint in CheckedBuffs)
      {
        foreach (Buff buff in targetUnit.Buffs)
        {
          if (buff.Blueprint == blueprint)
          {
            matchingBuffCount += buff.Context.MaybeCaster == caster ? 1 : 0;
          }
        }
      }

      if (RequireAllBuffs)
      {
        return matchingBuffCount == CheckedBuffs.Length;
      }
      return matchingBuffCount > 0;
    }

    public string GetAbilityTargetRestrictionUIText(UnitEntityData caster, TargetWrapper target)
    {
      string facts =
          string.Join(
              ", ",
              Buffs.Select(
                  delegate (BlueprintBuffReference i)
                  {
                    BlueprintBuff blueprintBuff = i.Get();
                    if (blueprintBuff == null) { return null; }
                    return blueprintBuff.Name;
                  }).NotNull<string>());
      return
          BlueprintRoot.Instance.LocalizedTexts.Reasons.TargetHasNoFact.ToString(
              delegate () { GameLogContext.Text = facts; });
    }

    [SerializeField]
    public BlueprintBuffReference[] Buffs;

    /// <summary>
    /// If set to true, all buffs must be present and applied by the caster. Otherwise the restriction passes as long as
    /// one buff is present and applied by the caster.
    /// </summary>
    [SerializeField]
    public bool RequireAllBuffs;
  }

}