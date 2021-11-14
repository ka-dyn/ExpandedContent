using BlueprintCore.Blueprints;
using Kingmaker.Blueprints;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Properties;

namespace BlueprintCore.Utils
{
  /// <summary>
  /// Helper class for creating <see cref="ContextValue"/> objects.
  /// </summary>
  public static class ContextValues
  {
    public static ContextValue Constant(int value)
    {
      return new ContextValue { ValueType = ContextValueType.Simple, Value = value };
    }

    /// <summary>
    /// Uses the context value associated with the specified <see cref="AbilityRankType"/>.
    /// </summary>
    /// 
    /// <remarks>
    /// The value associated with an <see cref="AbilityRankType"/> is determined using
    /// <see cref="Kingmaker.UnitLogic.Mechanics.Components.ContextRankConfig">ContextRankConfig</see>. See
    /// <see href="https://github.com/TylerGoeringer/OwlcatModdingWiki/wiki/ContextRankConfig">the wiki page</see> for
    /// more details.
    /// </remarks>
    public static ContextValue Rank(AbilityRankType type = AbilityRankType.Default)
    {
      return new ContextValue { ValueType = ContextValueType.Rank, ValueRank = type };
    }

    /// <summary>
    /// Uses the context value associated with the specified <see cref="AbilitySharedValue"/>.
    /// </summary>
    /// 
    /// <remarks>
    /// Different components and actions will set the associated value of an <see cref="AbilitySharedValue"/>.
    /// </remarks>
    public static ContextValue Shared(AbilitySharedValue type)
    {
      return new ContextValue { ValueType = ContextValueType.Shared, ValueShared = type };
    }

    public static ContextValue Property(UnitProperty property, bool toCaster = false)
    {
      return new ContextValue
      {
        ValueType = toCaster ? ContextValueType.CasterProperty : ContextValueType.TargetProperty,
        Property = property
      };
    }

    public static ContextValue CustomProperty(string property, bool toCaster = false)
    {
      return new ContextValue
      {
        ValueType =
            toCaster ? ContextValueType.CasterProperty : ContextValueType.TargetProperty,
        m_CustomProperty = BlueprintTool.GetRef<BlueprintUnitPropertyReference>(property)
      };
    }
  }
}