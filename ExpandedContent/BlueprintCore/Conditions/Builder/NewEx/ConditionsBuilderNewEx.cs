using BlueprintCore.Conditions.New;
using BlueprintCore.Utils;

namespace BlueprintCore.Conditions.Builder.NewEx
{
  /// <summary>
  /// Extension to <see cref="ConditionsBuilder"/> for conditions defined in BlueprintCore and not available in
  /// the base game.
  /// </summary>
  /// <inheritdoc cref="ConditionsBuilder"/>
  public static class ConditionsBuilderNewEx
  {
    /// <summary>
    /// Adds <see cref="New.TargetInMeleeRange">TargetInMeleeRange</see>
    /// </summary>
    [Implements(typeof(TargetInMeleeRange))]
    public static ConditionsBuilder TargetInMeleeRange(this ConditionsBuilder builder, bool negate = false)
    {
      var condition = ElementTool.Create<TargetInMeleeRange>();
      condition.Not = negate;
      return builder.Add(condition);
    }
  }
}
