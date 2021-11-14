using BlueprintCore.Blueprints;
using BlueprintCore.Utils;
using Kingmaker.Assets.Designers.EventConditionActionSystem.Conditions;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Conditions;
using Kingmaker.ElementsSystem;
using Kingmaker.Enums;
using Kingmaker.Globalmap.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using System;
namespace BlueprintCore.Conditions.Builder.AreaEx
{
  /// <summary>
  /// Extension to <see cref="ConditionsBuilder"/> for conditions involving the game map, dungeons, or locations. See also
  /// <see cref="KingdomEx.ConditionsBuilderKingdomEx">KingdomEx</see>.
  /// </summary>
  /// <inheritdoc cref="ConditionsBuilder"/>
  public static class ConditionsBuilderAreaEx
  {
    //----- Auto Generated -----//



    /// <summary>
    /// Adds <see cref="ContextConditionDungeonStage"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ContextConditionDungeonStage))]
    public static ConditionsBuilder AddContextConditionDungeonStage(
        this ConditionsBuilder builder,
        Int32 MinLevel,
        Int32 MaxLevel,
        bool negate = false)
    {
      builder.Validate(MinLevel);
      builder.Validate(MaxLevel);
      
      var element = ElementTool.Create<ContextConditionDungeonStage>();
      element.MinLevel = MinLevel;
      element.MaxLevel = MaxLevel;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="InCapital"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(InCapital))]
    public static ConditionsBuilder AddInCapital(
        this ConditionsBuilder builder,
        bool negate = false)
    {
      
      var element = ElementTool.Create<InCapital>();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="AreaVisited"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Area"><see cref="BlueprintArea"/></param>
    [Generated]
    [Implements(typeof(AreaVisited))]
    public static ConditionsBuilder AddAreaVisited(
        this ConditionsBuilder builder,
        string m_Area,
        bool negate = false)
    {
      
      var element = ElementTool.Create<AreaVisited>();
      element.m_Area = BlueprintTool.GetRef<BlueprintAreaReference>(m_Area);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="CurrentAreaIs"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Area"><see cref="BlueprintArea"/></param>
    [Generated]
    [Implements(typeof(CurrentAreaIs))]
    public static ConditionsBuilder AddCurrentAreaIs(
        this ConditionsBuilder builder,
        string m_Area,
        bool negate = false)
    {
      
      var element = ElementTool.Create<CurrentAreaIs>();
      element.m_Area = BlueprintTool.GetRef<BlueprintAreaReference>(m_Area);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="IsLootEmpty"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(IsLootEmpty))]
    public static ConditionsBuilder AddIsLootEmpty(
        this ConditionsBuilder builder,
        EntityReference LootObject,
        MapObjectEvaluator MapObject,
        Boolean EvaluateMapObject,
        bool negate = false)
    {
      builder.Validate(LootObject);
      builder.Validate(MapObject);
      builder.Validate(EvaluateMapObject);
      
      var element = ElementTool.Create<IsLootEmpty>();
      element.LootObject = LootObject;
      element.MapObject = MapObject;
      element.EvaluateMapObject = EvaluateMapObject;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="IsPartyInNaturalSetting"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(IsPartyInNaturalSetting))]
    public static ConditionsBuilder AddIsPartyInNaturalSetting(
        this ConditionsBuilder builder,
        GlobalMapZone Setting,
        bool negate = false)
    {
      builder.Validate(Setting);
      
      var element = ElementTool.Create<IsPartyInNaturalSetting>();
      element.Setting = Setting;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="LocationRevealed"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Location"><see cref="BlueprintGlobalMapPoint"/></param>
    [Generated]
    [Implements(typeof(LocationRevealed))]
    public static ConditionsBuilder AddLocationRevealed(
        this ConditionsBuilder builder,
        string m_Location,
        bool negate = false)
    {
      
      var element = ElementTool.Create<LocationRevealed>();
      element.m_Location = BlueprintTool.GetRef<BlueprintGlobalMapPoint.Reference>(m_Location);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="MapObjectDestroyed"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(MapObjectDestroyed))]
    public static ConditionsBuilder AddMapObjectDestroyed(
        this ConditionsBuilder builder,
        MapObjectEvaluator MapObject,
        bool negate = false)
    {
      builder.Validate(MapObject);
      
      var element = ElementTool.Create<MapObjectDestroyed>();
      element.MapObject = MapObject;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="MapObjectRevealed"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(MapObjectRevealed))]
    public static ConditionsBuilder AddMapObjectRevealed(
        this ConditionsBuilder builder,
        MapObjectEvaluator MapObject,
        bool negate = false)
    {
      builder.Validate(MapObject);
      
      var element = ElementTool.Create<MapObjectRevealed>();
      element.MapObject = MapObject;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="PartyInScriptZone"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(PartyInScriptZone))]
    public static ConditionsBuilder AddPartyInScriptZone(
        this ConditionsBuilder builder,
        PartyInScriptZone.CheckType m_Check,
        EntityReference m_ScriptZone,
        bool negate = false)
    {
      builder.Validate(m_Check);
      builder.Validate(m_ScriptZone);
      
      var element = ElementTool.Create<PartyInScriptZone>();
      element.m_Check = m_Check;
      element.m_ScriptZone = m_ScriptZone;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnitInScriptZone"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(UnitInScriptZone))]
    public static ConditionsBuilder AddUnitInScriptZone(
        this ConditionsBuilder builder,
        UnitEvaluator Unit,
        MapObjectEvaluator ScriptZone,
        bool negate = false)
    {
      builder.Validate(Unit);
      builder.Validate(ScriptZone);
      
      var element = ElementTool.Create<UnitInScriptZone>();
      element.Unit = Unit;
      element.ScriptZone = ScriptZone;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnitIsInAreaPart"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_AreaPart"><see cref="BlueprintAreaPart"/></param>
    [Generated]
    [Implements(typeof(UnitIsInAreaPart))]
    public static ConditionsBuilder AddUnitIsInAreaPart(
        this ConditionsBuilder builder,
        string m_AreaPart,
        UnitEvaluator Unit,
        bool negate = false)
    {
      builder.Validate(Unit);
      
      var element = ElementTool.Create<UnitIsInAreaPart>();
      element.m_AreaPart = BlueprintTool.GetRef<BlueprintAreaPartReference>(m_AreaPart);
      element.Unit = Unit;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnitIsInFogOfWar"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(UnitIsInFogOfWar))]
    public static ConditionsBuilder AddUnitIsInFogOfWar(
        this ConditionsBuilder builder,
        UnitEvaluator Target,
        bool negate = false)
    {
      builder.Validate(Target);
      
      var element = ElementTool.Create<UnitIsInFogOfWar>();
      element.Target = Target;
      element.Not = negate;
      return builder.Add(element);
    }
  }
}
