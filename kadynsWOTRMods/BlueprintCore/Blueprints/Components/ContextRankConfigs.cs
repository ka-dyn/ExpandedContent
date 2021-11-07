using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueprintCore.Blueprints.Components
{
  /// <summary>Helper class for creating <see cref="ContextRankConfig"/> objects.</summary>
  /// 
  /// <remarks>
  /// <para>
  /// Functions are split into three groups:
  /// </para>
  /// 
  /// <list type="bullet">
  /// <item>
  ///   <term>ContextRankConfigs</term>
  ///   <description>
  ///   Base class which creates a config with a specific base value. You can only use a single type, so you should only
  ///   call these functions once for a config.
  ///   </description>
  /// </item>
  /// <item>
  ///   <term><see cref="ProgressionExtensions"/></term>
  ///   <description>
  ///   Extension class which exposes different progressions. Like the base value, you can only have a single
  ///   progression type so you should only call one of these functions.
  ///   </description>
  /// </item>
  /// <item>
  ///   <term><see cref="CommonExtensions"/></term>
  ///   <description>Extension class which exposes common configurable values.</description>
  /// </item>
  /// </list>
  /// 
  /// <para>
  /// See <see href="https://github.com/TylerGoeringer/OwlcatModdingWiki/wiki/ContextRankConfig">ContextRankConfig</see>
  /// on the wiki for more details about the component and
  /// <see href="https://docs.google.com/spreadsheets/d/11nQdJ7DFzS73gwR9xk3gsKbyGgtDM51yNoMv7nNYnPw/edit?usp=sharing">ContextRankConfig Calculator</see>
  /// for help determining which progression to use.
  /// </para>
  /// 
  /// <example>
  /// Create a rank based on <see cref="StatType.Strength"/> with a bonus value of 2 and a max value of 30:
  /// <code>
  ///   var rankConfig = ContextRankConfigs.BaseStat(StatType.Strength).Max(30).Add(2);
  /// </code>
  /// </example>
  /// </remarks>
  public static class ContextRankConfigs
  {
    private static ContextRankConfig NewConfig(
        ContextRankBaseValueType valueType,
        string feature = null,
        string[] featureList = null,
        StatType stat = StatType.Unknown,
        ModifierDescriptor modDescriptor = ModifierDescriptor.None,
        string buff = null,
        bool excludeClasses = false,
        string[] archetypes = null,
        string[] classes = null,
        string property = null,
        string[] propertyList = null)
    {
      return new ContextRankConfig
      {
        m_Type = AbilityRankType.Default,
        m_BaseValueType = valueType,
        m_Feature = BlueprintTool.GetRef<BlueprintFeatureReference>(feature),
        m_FeatureList =
            featureList?
                .Select(feature => BlueprintTool.GetRef<BlueprintFeatureReference>(feature))
                .ToArray(),
        m_Stat = stat,
        m_SpecificModifier = modDescriptor,
        m_Buff = BlueprintTool.GetRef<BlueprintBuffReference>(buff),
        m_ExceptClasses = excludeClasses,
        m_AdditionalArchetypes =
            archetypes?
                .Select(archetype => BlueprintTool.GetRef<BlueprintArchetypeReference>(archetype))
                .ToArray(),
        m_Class =
            classes?
                .Select(clazz => BlueprintTool.GetRef<BlueprintCharacterClassReference>(clazz))
                .ToArray(),
        m_CustomProperty = BlueprintTool.GetRef<BlueprintUnitPropertyReference>(property),
        m_CustomPropertyList =
            propertyList?
                .Select(property => BlueprintTool.GetRef<BlueprintUnitPropertyReference>(property))
                .ToArray(),
        m_Progression = ContextRankProgression.AsIs
      };
    }

    public static ContextRankConfig BaseAttack()
    {
      return NewConfig(ContextRankBaseValueType.BaseAttack);
    }

    public static ContextRankConfig BaseStat(StatType stat)
    {
      return NewConfig(ContextRankBaseValueType.BaseStat, stat: stat);
    }

    public static ContextRankConfig StatBonus(StatType stat, ModifierDescriptor modDescriptor = ModifierDescriptor.None)
    {
      return NewConfig(ContextRankBaseValueType.StatBonus, stat: stat, modDescriptor: modDescriptor);
    }

    public static ContextRankConfig CasterLevel(bool useMax = false)
    {
      return NewConfig(useMax ? ContextRankBaseValueType.MaxCasterLevel : ContextRankBaseValueType.CasterLevel);
    }

    public static ContextRankConfig CharacterLevel()
    {
      return NewConfig(ContextRankBaseValueType.CharacterLevel);
    }

    public static ContextRankConfig CasterCR()
    {
      return NewConfig(ContextRankBaseValueType.CasterCR);
    }

    public static ContextRankConfig DungeonStage()
    {
      return NewConfig(ContextRankBaseValueType.DungeonStage);
    }

    public static ContextRankConfig CustomProperty(string property)
    {
      return NewConfig(ContextRankBaseValueType.CustomProperty, property: property);
    }

    public static ContextRankConfig MaxCustomProperty(params string[] properties)
    {
      return NewConfig(ContextRankBaseValueType.MaxCustomProperty, propertyList: properties);
    }

    public static ContextRankConfig ClassLevel(string[] classes, bool excludeClasses = false)
    {
      return NewConfig(ContextRankBaseValueType.ClassLevel, classes: classes, excludeClasses: excludeClasses);
    }

    public static ContextRankConfig MaxClassLevelWithArchetype(
        string[] classes, string[] archetypes, bool excludeClasses = false)
    {
      return NewConfig(
          ContextRankBaseValueType.MaxClassLevelWithArchetype,
          classes: classes,
          archetypes: archetypes,
          excludeClasses: excludeClasses);
    }

    public static ContextRankConfig SumClassLevelWithArchetype(
        string[] classes, string[] archetypes, bool excludeClasses = false, bool useOwner = false)
    {
      return NewConfig(
          useOwner
              ? ContextRankBaseValueType.OwnerSummClassLevelWithArchetype
              : ContextRankBaseValueType.SummClassLevelWithArchetype,
          classes: classes,
          archetypes: archetypes,
          excludeClasses: excludeClasses);
    }

    public static ContextRankConfig Bombs(
        string feature,
        string[] classes,
        string[] archetypes,
        bool excludeClasses = false)
    {
      return NewConfig(
          ContextRankBaseValueType.Bombs,
          feature: feature,
          classes: classes,
          archetypes: archetypes,
          excludeClasses: excludeClasses);
    }

    public static ContextRankConfig FeatureRank(string feature, bool useMaster = false)
    {
      return NewConfig(
          useMaster ? ContextRankBaseValueType.MasterFeatureRank : ContextRankBaseValueType.FeatureRank,
          feature: feature);
    }

    public static ContextRankConfig FeatureList(string[] features, bool useRanks = false)
    {
      return NewConfig(
          useRanks ? ContextRankBaseValueType.FeatureListRanks : ContextRankBaseValueType.FeatureList,
          featureList: features);
    }

    public static ContextRankConfig MythicLevel(bool useMaster = false)
    {
      return NewConfig(useMaster ? ContextRankBaseValueType.MasterMythicLevel : ContextRankBaseValueType.MythicLevel);
    }

    public static ContextRankConfig MythicLevelPlusBuffRank(string buff)
    {
      return NewConfig(ContextRankBaseValueType.MythicLevelPlusBuffRank, buff: buff);
    }

    public static ContextRankConfig BuffRank(string buff, bool useTarget = false)
    {
      return NewConfig(
          useTarget ? ContextRankBaseValueType.TargetBuffRank : ContextRankBaseValueType.CasterBuffRank, buff: buff);
    }
  }

  /// <summary>
  /// Common parameter extensions for <see cref="ContextRankConfig"/>.
  /// </summary>
  public static class CommonExtensions
  {
    public static ContextRankConfig OfType(this ContextRankConfig config, AbilityRankType rankType)
    {
      config.m_Type = rankType;
      return config;
    }

    public static ContextRankConfig Max(this ContextRankConfig config, int max)
    {
      config.m_UseMax = true;
      config.m_Max = max;
      return config;
    }

    public static ContextRankConfig Min(this ContextRankConfig config, int min)
    {
      config.m_UseMin = true;
      config.m_Min = min;
      return config;
    }
  }

  /// <summary>
  /// Progression extensions for <see cref="ContextRankConfig"/>.
  /// </summary>
  /// 
  /// <remarks>
  /// <para>Only one progression can be applied to a config; only call one of these functions for a config.</para>
  /// 
  /// <para>
  /// If the config should return the base value, no progression is needed.
  /// </para>
  /// 
  /// <para>
  /// Integer division is truncated: <c>3 / 2 = 1</c> (rounds down) and <c>-3 / 2 = -1</c> (rounds up).
  /// </para>
  /// 
  /// <para>
  /// Config progressions are deceptively named; the functions attempt to name them correctly. For easy mapping to the
  /// enum each function comment explains the progression formula and which enums are used. See also the
  /// <see href="https://docs.google.com/spreadsheets/d/11nQdJ7DFzS73gwR9xk3gsKbyGgtDM51yNoMv7nNYnPw/edit?usp=sharing">ContextRankConfig Calculator</see>.
  /// </para>
  /// </remarks>
  public static class ProgressionExtensions
  {
    /// <summary><c>Result = BaseValue / 2 + Bonus</c></summary>
    /// 
    /// <remarks>
    /// Implements <see cref="ContextRankProgression.Div2"/> and <see cref="ContextRankProgression.Div2PlusStep"/>
    /// </remarks>
    public static ContextRankConfig DivideBy2(this ContextRankConfig config, int bonus = 0)
    {
      if (bonus > 0)
      {
        config.m_Progression = ContextRankProgression.Div2PlusStep;
        config.m_StepLevel = bonus;
      }
      else { config.m_Progression = ContextRankProgression.Div2; }
      return config;
    }

    /// <summary><c>Result = 1 + (BaseValue - 1) / 2</c></summary>
    /// 
    /// <remarks>
    /// Implements <see cref="ContextRankProgression.OnePlusDiv2"/>
    /// </remarks>
    public static ContextRankConfig DivideBy2ThenAdd1(this ContextRankConfig config)
    {
      config.m_Progression = ContextRankProgression.OnePlusDiv2;
      return config;
    }

    /// <summary><c>Result = BaseValue / Divisor</c></summary>
    /// 
    /// <remarks>
    /// Implements <see cref="ContextRankProgression.DivStep"/>
    /// </remarks>
    public static ContextRankConfig DivideBy(this ContextRankConfig config, int divisor)
    {
      config.m_Progression = ContextRankProgression.DivStep;
      config.m_StepLevel = divisor;
      return config;
    }

    /// <summary>
    /// <c>Result = 1 + Max((BaseValue - Start) / Divisor, 0)</c><br/>
    /// OR<br/>
    /// <c>Result = 0</c>, if <c>delayStart</c> is <c>true</c> and <c>BaseValue &lt; Start</c>
    /// </summary>
    /// 
    /// <remarks>
    /// Implements <see cref="ContextRankProgression.StartPlusDivStep"/>,
    /// <see cref="ContextRankProgression.DelayedStartPlusDivStep"/>, and <see cref="ContextRankProgression.OnePlusDivStep"/>
    /// </remarks>
    public static ContextRankConfig DivideByThenAdd1(
        this ContextRankConfig config, int divisor, int start = 0, bool delayStart = false)
    {
      config.m_Progression =
          delayStart ? ContextRankProgression.DelayedStartPlusDivStep : ContextRankProgression.StartPlusDivStep;
      config.m_StepLevel = divisor;
      config.m_StartLevel = start;
      return config;
    }

    /// <summary>
    /// <c>Result = 1 + 2 * Max((BaseValue - Start) / Divisor, 0)</c>
    /// </summary>
    /// 
    /// <remarks>
    /// Implements <see cref="ContextRankProgression.StartPlusDoubleDivStep"/>
    /// </remarks>
    public static ContextRankConfig DivideByThenDoubleThenAdd1(
        this ContextRankConfig config, int divisor, int start = 0)
    {
      config.m_Progression = ContextRankProgression.StartPlusDoubleDivStep;
      config.m_StepLevel = divisor;
      config.m_StartLevel = start;
      return config;
    }

    /// <summary>
    /// <c>Result = BaseValue * Multiplier</c>
    /// </summary>
    /// 
    /// <remarks>
    /// Implements <see cref="ContextRankProgression.MultiplyByModifier"/>
    /// </remarks>
    public static ContextRankConfig MultiplyBy(this ContextRankConfig config, int multiplier)
    {
      config.m_Progression = ContextRankProgression.MultiplyByModifier;
      config.m_StepLevel = multiplier;
      return config;
    }

    /// <summary>
    /// <c>Result = BaseValue + Bonus</c><br/>
    /// OR<br/>
    /// <c>OR = 2*BaseValue + Bonus</c>, if <c>doubleBaseValue</c> is <c>true</c>
    /// </summary>
    /// 
    /// <remarks>
    /// Implements <see cref="ContextRankProgression.BonusValue"/> and <see cref="ContextRankProgression.DoublePlusBonusValue"/>
    /// </remarks>
    public static ContextRankConfig Add(
        this ContextRankConfig config, int bonus, bool doubleBaseValue = false)
    {
      config.m_Progression =
          doubleBaseValue
              ? ContextRankProgression.DoublePlusBonusValue
              : ContextRankProgression.BonusValue;
      config.m_StepLevel = bonus;
      return config;
    }

    /// <summary>
    /// <c>Result = BaseValue + BaseValue / 2</c>
    /// </summary>
    /// 
    /// <remarks>
    /// Implements <see cref="ContextRankProgression.HalfMore"/>
    /// </remarks>
    public static ContextRankConfig AddHalf(this ContextRankConfig config)
    {
      config.m_Progression = ContextRankProgression.HalfMore;
      return config;
    }

    /// <summary>Implements <see cref="ContextRankProgression.Custom"/></summary>
    /// 
    /// <remarks>
    /// <para>
    /// Entries must be provided in ascending order by their BaseValue.
    /// </para>
    /// 
    /// <para>
    /// The result is the <see cref="ContextRankConfig.CustomProgressionItem.ProgressionValue">ProgressionValue</see> of
    /// the first entry where the config's BaseValue is less than or equal to the entry's
    /// <see cref="ContextRankConfig.CustomProgressionItem.BaseValue">BaseValue</see>. If the config's BaseValue is
    /// greater than all entry <see cref="ContextRankConfig.CustomProgressionItem.BaseValue">BaseValues</see>, the last
    /// entry's <see cref="ContextRankConfig.CustomProgressionItem.ProgressionValue">ProgressionValue</see> is returned.
    /// </para>
    /// 
    /// <example>
    /// <code>
    ///   ContextRankConfigs.CharacterLevel()
    ///       .CustomProgression(
    ///           new ProgressionEntry(5, 1),
    ///           new ProgressionEntry(10, 2),
    ///           new ProgressionEntry(13, 4),
    ///           new ProgressionEntry(18, 6));
    /// </code>
    /// <list type="bullet">
    /// <item>
    ///   <term>Levels 1-5</term>
    ///   <description><c>Result = 1</c></description>
    /// </item>
    /// <item>
    ///   <term>Levels 6-10</term>
    ///   <description><c>Result = 2</c></description>
    /// </item>
    /// <item>
    ///   <term>Levels 11-13</term>
    ///   <description><c>Result = 4</c></description>
    /// </item>
    /// <item>
    ///   <term>Levels 14+</term>
    ///   <description><c>Result = 6</c></description>
    /// </item>
    /// </list>
    /// </example>
    /// </remarks>
    [Obsolete("Use CustomProgression with anonymous tuples.")]
    public static ContextRankConfig CustomProgression(
        this ContextRankConfig config, params ProgressionEntry[] entries)
    {
      config.m_Progression = ContextRankProgression.Custom;
      config.m_CustomProgression = entries;
      return config;
    }

    /// <summary>Implements <see cref="ContextRankProgression.Custom"/></summary>
    /// 
    /// <remarks>
    /// <para>
    /// Entries must be provided in ascending order by their Base.
    /// </para>
    /// 
    /// <para>
    /// The result is the <see cref="ContextRankConfig.CustomProgressionItem.ProgressionValue">ProgressionValue</see> of
    /// the first entry where the config's BaseValue is less than or equal to the entry's
    /// <see cref="ContextRankConfig.CustomProgressionItem.BaseValue">BaseValue</see>. If the config's BaseValue is
    /// greater than all entry <see cref="ContextRankConfig.CustomProgressionItem.BaseValue">BaseValues</see>, the last
    /// entry's <see cref="ContextRankConfig.CustomProgressionItem.ProgressionValue">ProgressionValue</see> is returned.
    /// </para>
    /// 
    /// <example>
    /// <code>
    ///   ContextRankConfigs.CharacterLevel().CustomProgression((5, 1), (10, 2), (13, 4), (18, 6));
    /// </code>
    /// <list type="bullet">
    /// <item>
    ///   <term>Levels 1-5</term>
    ///   <description><c>Result = 1</c></description>
    /// </item>
    /// <item>
    ///   <term>Levels 6-10</term>
    ///   <description><c>Result = 2</c></description>
    /// </item>
    /// <item>
    ///   <term>Levels 11-13</term>
    ///   <description><c>Result = 4</c></description>
    /// </item>
    /// <item>
    ///   <term>Levels 14+</term>
    ///   <description><c>Result = 6</c></description>
    /// </item>
    /// </list>
    /// </example>
    /// </remarks>
    public static ContextRankConfig CustomProgression(
        this ContextRankConfig config, params (int Base, int Progression)[] progression)
    {
      config.m_Progression = ContextRankProgression.Custom;
      config.m_CustomProgression =
          progression.ToList()
              .Select(
                  entry =>
                    new ContextRankConfig.CustomProgressionItem
                    {
                      BaseValue = entry.Base,
                      ProgressionValue = entry.Progression
                    })
              .ToArray();
      return config;
    }

    /// <summary>
    /// Creates a linear custom progression: <c>ProgressionValue = a * BaseValue + b</c>.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    /// When <c>BaseValue</c> is less than <paramref name="startingBaseValue"/> the result is
    /// <paramref name="progressionValueBeforeStart"/>.
    /// </para>
    /// 
    /// <para>
    /// When <c>BaseValue</c> is greater than or equal to
    /// <paramref name="startingBaseValue"/> the result is <c>a * BaseValue + b</c>.
    /// </para>
    /// 
    /// <para>
    /// When <c>BaseValue</c> exceeds <paramref name="maxBaseValue"/> the result is <c>a * MaxBaseValue + b</c>.
    /// </para>
    /// 
    /// <para>
    /// If specified, <paramref name="maxProgressionValue"/> sets the maximum result.
    /// </para>
    /// 
    /// <para>
    /// If specified, <paramref name="minProgressionValue"/> sets the minimum result.
    /// </para>
    /// 
    /// <para>Results are truncated so 3.6 becomes 3.</para>
    /// 
    /// <example>
    /// The following config returns 0 until <c>CharacterLevel</c> is 4, then <c>1 + 3/4 * CharacterLevel</c>
    /// <code>
    ///   ContextRankConfigs.CharacterLevel().LinearProgression(0.75f, 1, startingBaseValue = 4);
    /// </code>
    /// </example>
    /// </remarks>
    public static ContextRankConfig LinearProgression(
        this ContextRankConfig config,
        float a,
        float b,
        int startingBaseValue = 1,
        int maxBaseValue = 40,
        int progressionValueBeforeStart = 0,
        int? minProgressionValue = null,
        int? maxProgressionValue = null)
    {
      List<(int Base, int Progression)> progression = new();
      int? lastProgressionValue = null;
      // Building in reverse simplifies creating a sparsely populated progression.
      for (int baseValue = maxBaseValue; baseValue >= startingBaseValue; baseValue--)
      {
        int progressionValue =
            Math.Min(
                Math.Max(
                    (int)(a * baseValue + b),
                    minProgressionValue is null ? int.MinValue : minProgressionValue.Value),
                maxProgressionValue is null ? int.MaxValue : maxProgressionValue.Value);
        // Only add an entry if the progression value changes.
        if (progressionValue != lastProgressionValue)
        {
          progression.Add((Base: baseValue, Progression: progressionValue));
          lastProgressionValue = progressionValue;
        }
      }
      progression.Add((Base: startingBaseValue - 1, Progression: progressionValueBeforeStart));

      progression.Reverse();
      return config.CustomProgression(progression.ToArray());
    }
  }

  /// <summary>
  /// Wrapper providing a constructor for <see cref="ContextRankConfig.CustomProgressionItem"/>
  /// </summary>
  [Obsolete("Use CustomProgression with anonymous tuples.")]
  public class ProgressionEntry : ContextRankConfig.CustomProgressionItem
  {
    public ProgressionEntry(int baseValue, int progressionValue) : base()
    {
      BaseValue = baseValue;
      ProgressionValue = progressionValue;
    }
  }
}