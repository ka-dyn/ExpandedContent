using BlueprintCore.Utils;
using Kingmaker.Designers.EventConditionActionSystem.Conditions;
using Kingmaker.ElementsSystem;
using System.Collections.Generic;

namespace BlueprintCore.Conditions.Builder
{
  /// <summary>
  /// Fluent builder for <see cref="ConditionsChecker"/>
  /// </summary>
  /// 
  /// <remarks>
  /// <para>
  /// Conditions are supported using extension methods. Include the extension namespaces as needed.
  /// </para>
  /// 
  /// <para>
  /// When <see cref="Build"/> is called the <see cref="ConditionsChecker"/> is constructed, validated, and
  /// returned. If any errors are detected by <see cref="Validator"/> they will be logged as a warning.
  /// </para>
  /// 
  /// <para>
  /// Do not call <see cref="Build"/> twice on the same builder.
  /// </para>
  /// 
  /// <para>
  /// If a method calls for a string to represent any type of blueprint, you can pass the blueprint's
  /// <see cref="Kingmaker.Blueprints.SimpleBlueprint.AssetGuid">AssetGuid</see> as a string or as a name you already
  /// provided using <see cref="Blueprints.BlueprintTool.AddGuidsByName">AddGuidsByName()</see>.
  /// </para>
  /// 
  /// <list type="table">
  /// <listheader>Extensions</listheader>
  /// <item>
  ///   <term><see cref="AreaEx.ConditionsBuilderAreaEx">AreaEx</see></term>
  ///   <description>
  ///     Conditions involving the game map, dungeons, or locations. See also
  ///     <see cref="KingdomEx.ConditionsBuilderKingdomEx">KingdomEx</see> for location related conditions
  ///     specifically tied to the Kingdom and Crusade system.
  ///   </description>
  /// </item>
  /// <item>
  ///   <term><see cref="BasicEx.ConditionsBuilderBasicEx">BasicEx</see></term>
  ///   <description>
  ///     Most game mechanics related conditions not included in
  ///     <see cref="ContextEx.ConditionsBuilderContextEx">ContextEx</see>.
  ///   </description>
  /// </item>
  /// <item>
  ///   <term><see cref="ContextEx.ConditionsBuilderContextEx">ContextEx</see></term>
  ///   <description>
  ///     Most <see cref="Kingmaker.UnitLogic.Mechanics.Conditions.ContextCondition">ContextCondition</see> types. Some
  ///     <see cref="Kingmaker.UnitLogic.Mechanics.Conditions.ContextCondition">ContextCondition</see> types are in more
  ///     specific extensions such as <see cref="KingdomEx.ConditionsBuilderKingdomEx">KingdomEx</see>.
  ///   </description>
  /// </item>
  /// <item>
  ///   <term><see cref="KingdomEx.ConditionsBuilderKingdomEx">KingdomEx</see></term>
  ///   <description>
  ///     Conditions involving the Kingdom and Crusade system.
  ///   </description>
  /// </item>
  /// <item>
  ///   <term><see cref="NewEx.ConditionsBuilderMiscEx">MiscEx</see></term>
  ///   <description>
  ///     Conditions without a better extension container such as game difficulty.
  ///   </description>
  /// </item>
  /// <item>
  ///   <term><see cref="NewEx.ConditionsBuilderNewEx">NewEx</see></term>
  ///   <description>
  ///     Conditions defined in BlueprintCore and not available in the base game.
  ///   </description>
  /// </item>
  /// <item>
  ///   <term><see cref="StoryEx.ConditionsBuilderStoryEx">StoryEx</see></term>
  ///   <description>
  ///     Conditions related to the story such as companion stories, quests, name changes, and etudes.
  ///   </description>
  /// </item>
  /// </list>
  /// 
  /// <example>
  /// Make a melee attack if the target is in range:
  /// <code>
  /// // Provides the MeleeAttack extension for ActionsBuilder
  /// using BlueprintCore.Actions.Builder.ContextEx; 
  /// // Provides the TargetInMeleeRange extension for ConditionsBuilder
  /// using BlueprintCore.Conditions.Builder.NewEx;
  /// 
  /// var actionList =
  ///     ActionsBuilder.New()
  ///         .Conditional(
  ///             ConditionsBuilder.New().TargetInMeleeRange(),
  ///             ifTrue: ActionsBuilder.New().MeleeAttack())
  ///         .build();
  /// </code>
  /// </example>
  /// </remarks>
  public class ConditionsBuilder
  {
    private static readonly LogWrapper Logger = LogWrapper.GetInternal("ConditionsBuilder");

    private Operation OperationType = Operation.And;
    private readonly List<Condition> Conditions = new();
    private readonly List<string> ValidationWarnings = new();

    private ConditionsBuilder() { }

    /// <returns>A new <see cref="ConditionsBuilder"/></returns>
    public static ConditionsBuilder New() { return new ConditionsBuilder(); }

    /// <returns>
    /// A <see cref="ConditionsChecker"/> containing all specified conditions. Any validation errors are logged as a
    /// warning. Do not call twice on the same builder.
    /// </returns>
    public ConditionsChecker Build()
    {
      foreach (var warning in ValidationWarnings)
      {
        Logger.Warn(warning);
      }

      var checker = new ConditionsChecker
      {
        Operation = OperationType,
        Conditions = Conditions.ToArray()
      };
      return checker;
    }

    /// <summary>
    /// Causes the checker to return true if at least one conditions check passes. By default all checks must pass.
    /// </summary>
    public ConditionsBuilder UseOr()
    {
      OperationType = Operation.Or;
      return this;
    }

    /// <summary>Adds the specified <see cref="Condition"/> to the checker, with validation.</summary>
    /// 
    /// <remarks>
    /// It is recommended to only call this from an extension class or when adding a condition type not supported by the
    /// builder.
    /// </remarks>
    public ConditionsBuilder Add(Condition condition)
    {
      Validate(condition);
      Conditions.Add(condition);
      return this;
    }

    /// <summary>
    /// Adds <see cref="False"/>
    /// </summary>
    [Implements(typeof(False))]
    public ConditionsBuilder AddFalse()
    {
      return Add(ElementTool.Create<False>());
    }

    /// <summary>
    /// Adds <see cref="False"/>, negated
    /// </summary>
    [Implements(typeof(False))]
    public ConditionsBuilder AddTrue()
    {
      var element = ElementTool.Create<False>();
      element.Not = true;
      return Add(element);
    }

    /// <summary>
    /// Adds <see cref="GreaterThan"/>
    /// </summary>
    [Implements(typeof(GreaterThan))]
    public ConditionsBuilder AddGreaterThan(IntEvaluator Value, IntEvaluator MinValue)
    {
      Validate(Value);
      Validate(MinValue);

      var element = ElementTool.Create<GreaterThan>();
      element.Value = Value;
      element.MinValue = MinValue;
      return Add(element);
    }

    /// <summary>
    /// Adds <see cref="GreaterThan"/>
    /// </summary>
    [Implements(typeof(GreaterThan))]
    public ConditionsBuilder AddGreaterThan(FloatEvaluator Value, FloatEvaluator MinValue)
    {
      Validate(Value);
      Validate(MinValue);

      var element = ElementTool.Create<GreaterThan>();
      element.FloatValues = true;
      element.FloatValue = Value;
      element.FloatMinValue = MinValue;
      return Add(element);
    }

    /// <summary>
    /// Adds <see cref="GreaterThan"/>, negated
    /// </summary>
    [Implements(typeof(GreaterThan))]
    public ConditionsBuilder AddLessThanOrEqualTo(IntEvaluator Value, IntEvaluator MinValue)
    {
      Validate(Value);
      Validate(MinValue);

      var element = ElementTool.Create<GreaterThan>();
      element.Value = Value;
      element.MinValue = MinValue;
      element.Not = true;
      return Add(element);
    }

    /// <summary>
    /// Adds <see cref="GreaterThan"/>, negated
    /// </summary>
    [Implements(typeof(GreaterThan))]
    public ConditionsBuilder AddLessThanOrEqualTo(FloatEvaluator Value, FloatEvaluator MinValue)
    {
      Validate(Value);
      Validate(MinValue);

      var element = ElementTool.Create<GreaterThan>();
      element.FloatValues = true;
      element.FloatValue = Value;
      element.FloatMinValue = MinValue;
      element.Not = true;
      return Add(element);
    }

    /// <summary>
    /// Adds <see cref="IsEqual"/>
    /// </summary>
    [Implements(typeof(IsEqual))]
    public ConditionsBuilder AddIsEqual(IntEvaluator FirstValue, IntEvaluator SecondValue)
    {
      Validate(FirstValue);
      Validate(SecondValue);

      var element = ElementTool.Create<IsEqual>();
      element.FirstValue = FirstValue;
      element.SecondValue = SecondValue;
      return Add(element);
    }

    /// <summary>
    /// Adds <see cref="IsEqual"/>, negated
    /// </summary>
    [Implements(typeof(IsEqual))]
    public ConditionsBuilder AddIsNotEqual(IntEvaluator FirstValue, IntEvaluator SecondValue)
    {
      Validate(FirstValue);
      Validate(SecondValue);

      var element = ElementTool.Create<IsEqual>();
      element.FirstValue = FirstValue;
      element.SecondValue = SecondValue;
      element.Not = true;
      return Add(element);
    }

    /// <summary>
    /// Adds <see cref="LessThan"/>
    /// </summary>
    [Implements(typeof(LessThan))]
    public ConditionsBuilder AddLessThan(IntEvaluator Value, IntEvaluator MaxValue)
    {
      Validate(Value);
      Validate(MaxValue);

      var element = ElementTool.Create<LessThan>();
      element.Value = Value;
      element.MaxValue = MaxValue;
      return Add(element);
    }

    /// <summary>
    /// Adds <see cref="LessThan"/>
    /// </summary>
    [Implements(typeof(LessThan))]
    public ConditionsBuilder AddLessThan(FloatEvaluator Value, FloatEvaluator MaxValue)
    {
      Validate(Value);
      Validate(MaxValue);

      var element = ElementTool.Create<LessThan>();
      element.FloatValues = true;
      element.FloatValue = Value;
      element.FloatMaxValue = MaxValue;
      return Add(element);
    }

    /// <summary>
    /// Adds <see cref="LessThan"/>, negated
    /// </summary>
    [Implements(typeof(LessThan))]
    public ConditionsBuilder AddGreaterThanOrEqualTo(IntEvaluator Value, IntEvaluator MaxValue)
    {
      Validate(Value);
      Validate(MaxValue);

      var element = ElementTool.Create<LessThan>();
      element.Value = Value;
      element.MaxValue = MaxValue;
      element.Not = true;
      return Add(element);
    }

    /// <summary>
    /// Adds <see cref="LessThan"/>, negated
    /// </summary>
    [Implements(typeof(LessThan))]
    public ConditionsBuilder AddGreaterThanOrEqualTo(FloatEvaluator Value, FloatEvaluator MaxValue)
    {
      Validate(Value);
      Validate(MaxValue);

      var element = ElementTool.Create<LessThan>();
      element.FloatValues = true;
      element.FloatValue = Value;
      element.FloatMaxValue = MaxValue;
      element.Not = true;
      return Add(element);
    }

    /// <summary>
    /// Adds <see cref="OrAndLogic"/>
    /// </summary>
    [Implements(typeof(OrAndLogic))]
    public ConditionsBuilder AddOrAndLogic(ConditionsBuilder conditions, bool negate = false)
    {
      var element = ElementTool.Create<OrAndLogic>();
      element.ConditionsChecker = conditions.Build();
      element.Not = negate;
      return Add(element);
    }

    /// <summary>
    /// Runs the object through <see cref="Validator.Check(object)">Validator.Check()</see>, adding any errors to the
    /// validation warnings.
    /// </summary>
    /// 
    /// <remarks>
    /// Exposed for use by extension classes to bundle warnings into the builder. Other classes can use
    /// <see cref="Validator.Check(object)">Validator.Check()</see> directly.
    /// </remarks>
    /// <param name="obj"></param>
    internal void Validate(object obj)
    {
      ValidationWarnings.AddRange(Validator.Check(obj));
    }
  }
}
