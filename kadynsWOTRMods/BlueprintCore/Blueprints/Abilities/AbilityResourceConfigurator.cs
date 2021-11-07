using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Localization;
using System;
using System.Linq;
using UnityEngine;

namespace BlueprintCore.Blueprints.Abilities
{
  /// <summary>Configurator for <see cref="BlueprintAbilityResource"/>.</summary>
  /// <inheritdoc/>
  public class AbilityResourceConfigurator
      : BaseBlueprintConfigurator<BlueprintAbilityResource, AbilityResourceConfigurator>
  {
    private AbilityResourceConfigurator(string name) : base(name) { }

    /// <inheritdoc cref="Buffs.BuffConfigurator.For(string)"/>
    public static AbilityResourceConfigurator For(string name) { return new AbilityResourceConfigurator(name); }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string)"/>
    public static AbilityResourceConfigurator New(string name)
    {
      BlueprintTool.Create<BlueprintAbilityResource>(name);
      return For(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string, string)"/>
    public static AbilityResourceConfigurator New(string name, string assetId)
    {
      BlueprintTool.Create<BlueprintAbilityResource>(name, assetId);
      return For(name);
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbilityResource.LocalizedName"/>
    /// </summary>
    public AbilityResourceConfigurator SetDisplayName(LocalizedString name)
    {
      return OnConfigureInternal(blueprint => blueprint.LocalizedName = name);
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbilityResource.LocalizedDescription"/>
    /// </summary>
    public AbilityResourceConfigurator SetDescription(LocalizedString description)
    {
      return OnConfigureInternal(blueprint => blueprint.LocalizedDescription = description);
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbilityResource.m_Icon"/>
    /// </summary>
    public AbilityResourceConfigurator SetIcon(Sprite icon)
    {
      return OnConfigureInternal(blueprint => blueprint.m_Icon = icon);
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbilityResource.m_MaxAmount"/>
    /// </summary>
    public AbilityResourceConfigurator SetMaxAmount(ResourceAmountBuilder amount)
    {
      return OnConfigureInternal(blueprint => blueprint.m_MaxAmount = amount.Build());
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbilityResource.m_Max"/> and <see cref="BlueprintAbilityResource.m_UseMax"/>
    /// </summary>
    public AbilityResourceConfigurator SetMax(int max)
    {
      return OnConfigureInternal(
          blueprint =>
          {
            blueprint.m_Max = max;
            blueprint.m_UseMax = true;
          });
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbilityResource.m_UseMax"/>
    /// </summary>
    public AbilityResourceConfigurator DisableMax()
    {
      return OnConfigureInternal(blueprint => blueprint.m_UseMax = false);
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbilityResource.m_Min"/>
    /// </summary>
    public AbilityResourceConfigurator SetMin(int min)
    {
      return OnConfigureInternal(blueprint => blueprint.m_Min = min);
    }
  }

  /// <summary>
  /// Builder utility for <see cref="BlueprintAbilityResource.Amount"/>
  /// </summary>
  /// 
  /// <remarks>
  /// Note that you can use <see cref="IncreaseByLevel(string[], int)"/>, <see cref="IncreaseByStat(StatType)"/>, and
  /// <see cref="IncreaseByLevelStartPlusDivStep(string[], float, int, int, int, int, int)"/> simultaneously. Each will
  /// be applied, in that order.
  /// </remarks>
  public class ResourceAmountBuilder
  {
    private BlueprintAbilityResource.Amount Amount = new();
    public static ResourceAmountBuilder New(int baseValue)
    {
      var builder = new ResourceAmountBuilder();
      builder.Amount.BaseValue = baseValue;
      return builder;
    }

    /// <returns>A configured <see cref="BlueprintAbilityResource.Amount"/></returns>
    public BlueprintAbilityResource.Amount Build()
    {
      return Amount;
    }

    /// <summary>
    /// Increases the amount by <c>BonusPerLevel * ClassLevels</c>.
    /// </summary>
    /// 
    /// <remarks>Technically there is logic to support archetypes as well but it is unused and broken.</remarks>
    /// 
    /// <param name="classes"><see cref="Kingmaker.Blueprints.Classes.BlueprintCharacterClass">BlueprintCharacterClass</see></param>
    public ResourceAmountBuilder IncreaseByLevel(string[] classes, int bonusPerLevel = 1)
    {
      Amount.IncreasedByLevel = true;
      Amount.m_Class = classes.Select(clazz => BlueprintTool.GetRef<BlueprintCharacterClassReference>(clazz)).ToArray();
      Amount.LevelIncrease = bonusPerLevel;
      return this;
    }

    /// <summary>
    /// Increases the amount by <c>StatBonus</c>.
    /// </summary>
    public ResourceAmountBuilder IncreaseByStat(StatType stat)
    {
      Amount.IncreasedByStat = true;
      Amount.ResourceBonusStat = stat;
      return this;
    }

    /// <summary>
    /// Beginning at <c>StartingLevel</c>, increases the amount by
    /// <c>StartingBonus + BonusPerStep * (Levels - StartingLevel)/LevelsPerStep</c> or <c>MinBonus</c>, whichever is
    /// larger.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    /// Note that <c>Levels</c> is calculated as
    /// <c>ClassLevels + (CharacterLevel - ClassLevels) * OtherClassLevelsMultiplier</c>
    /// </para>
    /// 
    /// <para>
    /// As with <see cref="IncreaseByLevel(string[], int)"/>, archetype support is technically implemented but it is
    /// unused and broken.
    /// </para>
    /// </remarks>
    /// 
    /// <param name="classes"><see cref="Kingmaker.Blueprints.Classes.BlueprintCharacterClass">BlueprintCharacterClass</see></param>
    public ResourceAmountBuilder IncreaseByLevelStartPlusDivStep(
        string[] classes = null,
        float otherClassLevelsMultiplier = 0f,
        int startingLevel = 0,
        int startingBonus = 0,
        int levelsPerStep = 1,
        int bonusPerStep = 0,
        int minBonus = 0)
    {
      Amount.IncreasedByLevelStartPlusDivStep = true;
      Amount.m_ClassDiv =
          classes is not null
              ? classes.Select(clazz => BlueprintTool.GetRef<BlueprintCharacterClassReference>(clazz))?.ToArray()
              : Array.Empty<BlueprintCharacterClassReference>();
      Amount.OtherClassesModifier = otherClassLevelsMultiplier;
      Amount.StartingLevel = startingLevel;
      Amount.StartingIncrease = startingBonus;
      Amount.LevelStep = levelsPerStep;
      Amount.PerStepIncrease = bonusPerStep;
      Amount.MinClassLevelIncrease = minBonus;
      return this;
    }
  }
}