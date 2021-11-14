using Kingmaker.Blueprints.Items.Armors;
using Kingmaker.ElementsSystem;
using Kingmaker.Enums;
using Kingmaker.Localization;
using Kingmaker.RuleSystem;
using Kingmaker.UnitLogic.Mechanics;

namespace BlueprintCore.Utils
{
  public static class Constants
  {
    /// <summary>Empty, non-null object constants.</summary>
    /// 
    /// <remarks>
    /// It is generally recommended to use these in place of null. Some areas of the Wrath codebase are null safe, but
    /// many are not. Most code behaves correctly with empty objects.
    /// </remarks>
    public static class Empty
    {
      public static readonly ActionList Actions = new() { Actions = new GameAction[0] };
      public static readonly ArmorProficiencyGroup[] ArmorProficiencies =
          new ArmorProficiencyGroup[0];
      public static readonly ConditionsChecker Conditions = new() { Conditions = new Condition[0] };
      public static readonly ContextDiceValue DiceValue = new()
      {
        DiceType = DiceType.Zero,
        DiceCountValue = 0,
        BonusValue = 0
      };
      public static readonly LocalizedString String = new();
      public static readonly WeaponCategory[] WeaponCategories = new WeaponCategory[0];
    }
  }
}