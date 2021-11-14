using BlueprintCore.Conditions.Builder;
using BlueprintCore.Utils;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueprintCore.Actions.Builder
{
  /// <summary>
  /// Fluent builder for <see cref="ActionList"/>.
  /// </summary>
  /// 
  /// <remarks>
  /// <para>
  /// Actions are supported using extension methods. Include the extension namespaces as needed.
  /// </para>
  /// 
  /// <para>
  /// When <see cref="Build">Build</see> is called the <see cref="ActionList"/> is constructed, validated, and returned.
  /// If any errors are detected by <see cref="Validator"/> they will be logged as a warning.
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
  ///   <term><see cref="AreaEx.ActionsBuilderAreaEx">AreaEx</see></term>
  ///   <description>
  ///     Actions involving the game map, dungeons, or locations. See also
  ///     <see cref="KingdomEx.ActionsBuilderKingdomEx">KingdomEx</see> for location related actions specifically
  ///     tied to the Kingdom and Crusade system.
  ///   </description>
  /// </item>
  /// <item>
  ///   <term><see cref="AVEx.ActionsBuilderAVEx">AVEx</see></term>
  ///   <description>
  ///     Actions involving audiovisual effects such as dialogs, camera, cutscenes, and sounds.
  ///   </description>
  /// </item>
  /// <item>
  ///   <term><see cref="BasicEx.ActionsBuilderBasicEx">BasicEx</see></term>
  ///   <description>
  ///     Most game mechanics related actions not included in
  ///     <see cref="ContextEx.ActionsBuilderContextEx">ContextEx</see>.
  ///   </description>
  /// </item>
  /// <item>
  ///   <term><see cref="ContextEx.ActionsBuilderContextEx">ContextEx</see></term>
  ///   <description>
  ///     Most <see cref="Kingmaker.UnitLogic.Mechanics.Actions.ContextAction">ContextAction</see> types. Some
  ///     <see cref="Kingmaker.UnitLogic.Mechanics.Actions.ContextAction">ContextAction</see> types are in more specific
  ///     extensions such as <see cref="AVEx.ActionsBuilderAVEx">AVEx</see> or
  ///     <see cref="KingdomEx.ActionsBuilderKingdomEx">KingdomEx</see>.
  ///   </description>
  /// </item>
  /// <item>
  ///   <term><see cref="KingdomEx.ActionsBuilderKingdomEx">KingdomEx</see></term>
  ///   <description>
  ///     Actions involving the Kingdom and Crusade system.
  ///   </description>
  /// </item>
  /// <item>
  ///   <term><see cref="MiscEx.ActionsBuilderMiscEx">MiscEx</see></term>
  ///   <description>
  ///     Actions without a better extension container such as achievements and CustomEvent.
  ///   </description>
  /// </item>
  /// <item>
  ///   <term><see cref="NewEx.ActionsBuilderNewEx">NewEx</see></term>
  ///   <description>
  ///     Actions defined in BlueprintCore and not available in the base game.
  ///   </description>
  /// </item>
  /// <item>
  ///   <term><see cref="StoryEx.ActionsBuilderStoryEx">StoryEx</see></term>
  ///   <description>
  ///     Actions related to the story such as companion stories, quests, name changes, and etudes.
  ///   </description>
  /// </item>
  /// <item>
  ///   <term><see cref="UpgraderEx.ActionsBuilderUpgraderEx">UpgraderEx</see></term>
  ///   <description>
  ///     All UpgraderOnlyActions.
  ///   </description>
  /// </item>
  /// </list>
  /// 
  /// <example>
  /// Apply a buff and make a melee attack:
  /// <code>
  /// // Provides ApplyBuff and MeleeAttack extensions
  /// using BlueprintCore.Actions.Builder.ContextEx; 
  /// 
  /// var actionList =
  ///     ActionsBuilder.New()
  ///         .ApplyBuff(MyAttackBuff, duration: ContextDuration.Fixed(1))
  ///         .MeleeAttack()
  ///         .build();
  /// </code>
  /// </example>
  /// </remarks>
  public class ActionsBuilder
  {
    private static readonly LogWrapper Logger = LogWrapper.GetInternal("ActionsBuilder");

    private readonly List<GameAction> Actions = new();
    private readonly StringBuilder ValidationWarnings = new();

    private ActionsBuilder() { }

    /// <returns>A new <see cref="ActionsBuilder"/></returns>
    public static ActionsBuilder New() { return new ActionsBuilder(); }

    /// <returns>
    /// An <see cref="ActionList"/> containing all specified actions. Any validation errors are logged as a warning. Do
    /// not call twice on the same builder.
    /// </returns>
    public ActionList Build()
    {
      if (ValidationWarnings.Length > 0) { Logger.Warn(ValidationWarnings.ToString()); }
      return new ActionList { Actions = Actions.ToArray() };
    }

    /// <summary>Adds the specified <see cref="GameAction"/> to the list, with validation.</summary>
    /// 
    /// <remarks>
    /// It is recommended to only call this from an extension class or when adding an action type not supported by the
    /// builder.
    /// </remarks>
    public ActionsBuilder Add(GameAction action)
    {
      Validate(action);
      Actions.Add(action);
      return this;
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.Designers.EventConditionActionSystem.Actions.Conditional">Conditional</see>
    /// </summary>
    [Implements(typeof(Conditional))]
    public ActionsBuilder Conditional(
        ConditionsBuilder conditions, ActionsBuilder ifTrue = null, ActionsBuilder ifFalse = null)
    {
      if (ifTrue == null && ifFalse == null)
      {
        throw new InvalidOperationException("A conditional must have some actions.");
      }
      var conditional = ElementTool.Create<Conditional>();
      conditional.ConditionsChecker = conditions.Build();

      conditional.IfTrue = ifTrue?.Build() ?? Constants.Empty.Actions;
      conditional.IfFalse = ifFalse?.Build() ?? Constants.Empty.Actions;
      return Add(conditional);
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
      Validator.Check(obj).ForEach(str => ValidationWarnings.AppendLine(str));
    }
  }
}
