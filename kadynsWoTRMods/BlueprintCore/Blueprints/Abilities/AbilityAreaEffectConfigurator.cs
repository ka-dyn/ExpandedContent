using Kingmaker.ResourceLinks;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.Utility;
using System.Collections.Generic;

namespace BlueprintCore.Blueprints.Abilities
{
  /// <summary>Configurator for <see cref="BlueprintAbilityAreaEffect"/>.</summary>
  /// <inheritdoc/>
  public class AbilityAreaEffectConfigurator
      : BaseBlueprintConfigurator<BlueprintAbilityAreaEffect, AbilityAreaEffectConfigurator>
  {
    private AbilityAreaEffectConfigurator(string name) : base(name) { }

    /// <inheritdoc cref="Buffs.BuffConfigurator.For(string)"/>
    public static AbilityAreaEffectConfigurator For(string name) { return new AbilityAreaEffectConfigurator(name); }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string)"/>
    public static AbilityAreaEffectConfigurator New(string name)
    {
      BlueprintTool.Create<BlueprintAbilityAreaEffect>(name);
      return For(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string, string)"/>
    public static AbilityAreaEffectConfigurator New(string name, string assetId)
    {
      BlueprintTool.Create<BlueprintAbilityAreaEffect>(name, assetId);
      return For(name);
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbilityAreaEffect.m_TargetType"/>
    /// </summary>
    public AbilityAreaEffectConfigurator SetTargetType(BlueprintAbilityAreaEffect.TargetType type)
    {
      return OnConfigureInternal(blueprint => blueprint.m_TargetType = type);
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbilityAreaEffect.SpellResistance"/>
    /// </summary>
    public AbilityAreaEffectConfigurator ApplySpellResistance(bool applySR = true)
    {
      return OnConfigureInternal(blueprint => blueprint.SpellResistance = applySR);
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbilityAreaEffect.AffectEnemies"/>
    /// </summary>
    public AbilityAreaEffectConfigurator SetAffectEnemies(bool affectEnemies = true)
    {
      return OnConfigureInternal(blueprint => blueprint.AffectEnemies = affectEnemies);
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbilityAreaEffect.AggroEnemies"/>
    /// </summary>
    public AbilityAreaEffectConfigurator SetAggroEnemies(bool aggro = false)
    {
      return OnConfigureInternal(blueprint => blueprint.AggroEnemies = aggro);
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbilityAreaEffect.AffectDead"/>
    /// </summary>
    public AbilityAreaEffectConfigurator SetAffectDead(bool affectDead = true)
    {
      return OnConfigureInternal(blueprint => blueprint.AffectDead = affectDead);
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbilityAreaEffect.IgnoreSleepingUnits"/>
    /// </summary>
    public AbilityAreaEffectConfigurator SetIgnoreSleepingUnits(bool ignore = true)
    {
      return OnConfigureInternal(blueprint => blueprint.IgnoreSleepingUnits = ignore);
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbilityAreaEffect.Shape"/>
    /// </summary>
    public AbilityAreaEffectConfigurator SetShape(AreaEffectShape shape)
    {
      return OnConfigureInternal(blueprint => blueprint.Shape = shape);
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbilityAreaEffect.Size"/>
    /// </summary>
    public AbilityAreaEffectConfigurator SetSize(int radiusInFeet)
    {
      return OnConfigureInternal(blueprint => blueprint.Size = new Feet(radiusInFeet));
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbilityAreaEffect.Fx"/>
    /// </summary>
    public AbilityAreaEffectConfigurator SetFx(PrefabLink prefab)
    {
      return OnConfigureInternal(blueprint => blueprint.Fx = prefab);
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbilityAreaEffect.m_SizeInCells"/> and <see cref="BlueprintAbilityAreaEffect.CanBeUsedInTacticalCombat"/>
    /// </summary>
    /// 
    /// <remarks>Sets <see cref="BlueprintAbilityAreaEffect.CanBeUsedInTacticalCombat"/> to true.</remarks>
    /// <param name="sizeInCells">The game library states this can only be odd and will always be a cylinder.</param>
    public AbilityAreaEffectConfigurator SetSizeInTacticalCombat(int sizeInCells)
    {
      return OnConfigureInternal(
          blueprint =>
          {
            blueprint.m_SizeInCells = sizeInCells;
            blueprint.CanBeUsedInTacticalCombat = true;
          });
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbilityAreaEffect.CanBeUsedInTacticalCombat"/>
    /// </summary>
    public AbilityAreaEffectConfigurator DisableInTacticalCombat()
    {
      return OnConfigureInternal(blueprint => blueprint.CanBeUsedInTacticalCombat = false);
    }
  }
}