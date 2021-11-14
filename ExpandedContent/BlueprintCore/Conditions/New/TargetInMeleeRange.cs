using BlueprintCore.Utils;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Items.Slots;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;

namespace BlueprintCore.Conditions.New
{
  /// <summary>
  /// Checks if the target is in melee range.
  /// </summary>
  public class TargetInMeleeRange : Condition
  {
    private static readonly LogWrapper Logger = LogWrapper.GetInternal("TargetInMeleeRange");

    public override string GetConditionCaption()
    {
      return "Target in melee range condition";
    }

    public override bool CheckCondition()
    {
      UnitEntityData caster = ContextData<MechanicsContext.Data>.Current.Context.MaybeCaster;
      if (caster == null)
      {
        Logger.Warn("No caster");
        return false;
      }
      TargetWrapper target = ContextData<MechanicsContext.Data>.Current.Context.MainTarget;
      if (target == null || target.Unit == null)
      {
        Logger.Warn("No target");
        return false;
      }
      WeaponSlot threatHandleMelee = caster.GetThreatHandMelee();
      if (threatHandleMelee == null)
      {
        Logger.Warn("Caster can't make attack");
        return false;
      }

      // Weapon range is typically close to 0 but DistanceTo() calculates center to center. Add the View.Corpulence for
      // caster & target (unit radius) to compensate.
      float meleeRange =
          threatHandleMelee.Weapon.AttackRange.Meters + caster.View.Corpulence + target.Unit.View.Corpulence;
      float checkValue = caster.DistanceTo(target.Point);
      bool result = checkValue <= meleeRange;
      if (!result)
      {
        Logger.Verbose($"Target out of melee range: {checkValue} - {meleeRange}");
      }
      return result;
    }
  }
}