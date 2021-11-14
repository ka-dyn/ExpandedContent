using BlueprintCore.Blueprints;
using BlueprintCore.Utils;
using Kingmaker.Assets.Designers.EventConditionActionSystem.Conditions;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Items;
using Kingmaker.Designers.EventConditionActionSystem.Conditions;
using Kingmaker.ElementsSystem;
using Kingmaker.GameModes;
using Kingmaker.Settings.Difficulty;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using System;
namespace BlueprintCore.Conditions.Builder.MiscEx
{
  /// <summary>
  /// Extension to <see cref="ConditionsBuilder"/> for conditions without a better extension container such as
  /// difficulty.
  /// </summary>
  /// <inheritdoc cref="ConditionsBuilder"/>
  public static class ConditionsBuilderMiscEx
  {
    //----- Auto Generated -----//



    /// <summary>
    /// Adds <see cref="ContextConditionDifficultyHigherThan"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ContextConditionDifficultyHigherThan))]
    public static ConditionsBuilder AddContextConditionDifficultyHigherThan(
        this ConditionsBuilder builder,
        Boolean Less,
        Boolean Reverse,
        Boolean CheckOnlyForMonster,
        DifficultyPresetAsset m_Difficulty,
        bool negate = false)
    {
      builder.Validate(Less);
      builder.Validate(Reverse);
      builder.Validate(CheckOnlyForMonster);
      builder.Validate(m_Difficulty);
      
      var element = ElementTool.Create<ContextConditionDifficultyHigherThan>();
      element.Less = Less;
      element.Reverse = Reverse;
      element.CheckOnlyForMonster = CheckOnlyForMonster;
      element.m_Difficulty = m_Difficulty;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="DifficultyHigherThan"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(DifficultyHigherThan))]
    public static ConditionsBuilder AddDifficultyHigherThan(
        this ConditionsBuilder builder,
        DifficultyPresetAsset m_Difficulty,
        bool negate = false)
    {
      builder.Validate(m_Difficulty);
      
      var element = ElementTool.Create<DifficultyHigherThan>();
      element.m_Difficulty = m_Difficulty;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="EnlargedEncountersCapacity"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(EnlargedEncountersCapacity))]
    public static ConditionsBuilder AddEnlargedEncountersCapacity(
        this ConditionsBuilder builder,
        bool negate = false)
    {
      
      var element = ElementTool.Create<EnlargedEncountersCapacity>();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="Paused"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(Paused))]
    public static ConditionsBuilder AddPaused(
        this ConditionsBuilder builder,
        bool negate = false)
    {
      
      var element = ElementTool.Create<Paused>();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="GameModeActive"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(GameModeActive))]
    public static ConditionsBuilder AddGameModeActive(
        this ConditionsBuilder builder,
        GameModeType.Enum m_GameMode,
        bool negate = false)
    {
      builder.Validate(m_GameMode);
      
      var element = ElementTool.Create<GameModeActive>();
      element.m_GameMode = m_GameMode;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="HasEnoughMoneyForCustomCompanion"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(HasEnoughMoneyForCustomCompanion))]
    public static ConditionsBuilder AddHasEnoughMoneyForCustomCompanion(
        this ConditionsBuilder builder,
        bool negate = false)
    {
      
      var element = ElementTool.Create<HasEnoughMoneyForCustomCompanion>();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="HasEnoughMoneyForRespec"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(HasEnoughMoneyForRespec))]
    public static ConditionsBuilder AddHasEnoughMoneyForRespec(
        this ConditionsBuilder builder,
        bool negate = false)
    {
      
      var element = ElementTool.Create<HasEnoughMoneyForRespec>();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="IsDLCEnabled"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_BlueprintDlcReward"><see cref="BlueprintDlcReward"/></param>
    [Generated]
    [Implements(typeof(IsDLCEnabled))]
    public static ConditionsBuilder AddIsDLCEnabled(
        this ConditionsBuilder builder,
        string m_BlueprintDlcReward,
        bool negate = false)
    {
      
      var element = ElementTool.Create<IsDLCEnabled>();
      element.m_BlueprintDlcReward = BlueprintTool.GetRef<BlueprintDlcRewardReference>(m_BlueprintDlcReward);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="IsListContainsItem"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="List"><see cref="BlueprintItemsList"/></param>
    [Generated]
    [Implements(typeof(IsListContainsItem))]
    public static ConditionsBuilder AddIsListContainsItem(
        this ConditionsBuilder builder,
        ItemEvaluator Item,
        string List,
        bool negate = false)
    {
      builder.Validate(Item);
      
      var element = ElementTool.Create<IsListContainsItem>();
      element.Item = Item;
      element.List = BlueprintTool.GetRef<BlueprintItemsList.Reference>(List);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="IsRespecAllowed"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(IsRespecAllowed))]
    public static ConditionsBuilder AddIsRespecAllowed(
        this ConditionsBuilder builder,
        bool negate = false)
    {
      
      var element = ElementTool.Create<IsRespecAllowed>();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="IsUnitCustomCompanion"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(IsUnitCustomCompanion))]
    public static ConditionsBuilder AddIsUnitCustomCompanion(
        this ConditionsBuilder builder,
        UnitEvaluator Unit,
        bool negate = false)
    {
      builder.Validate(Unit);
      
      var element = ElementTool.Create<IsUnitCustomCompanion>();
      element.Unit = Unit;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RespecIsFree"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(RespecIsFree))]
    public static ConditionsBuilder AddRespecIsFree(
        this ConditionsBuilder builder,
        bool negate = false)
    {
      
      var element = ElementTool.Create<RespecIsFree>();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="IsleStateCondition"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(IsleStateCondition))]
    public static ConditionsBuilder AddIsleStateCondition(
        this ConditionsBuilder builder,
        IsleEvaluator m_Isle,
        String m_State,
        bool negate = false)
    {
      builder.Validate(m_Isle);
      foreach (var item in m_State)
      {
        builder.Validate(item);
      }
      
      var element = ElementTool.Create<IsleStateCondition>();
      element.m_Isle = m_Isle;
      element.m_State = m_State;
      element.Not = negate;
      return builder.Add(element);
    }
  }
}
