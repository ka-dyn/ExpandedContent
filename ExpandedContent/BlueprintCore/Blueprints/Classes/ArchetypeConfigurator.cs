using Kingmaker.Blueprints.Classes;
using Kingmaker.Localization;

namespace BlueprintCore.Blueprints.Classes
{
  /// <summary>Configurator for <see cref="BlueprintArchetype"/>.</summary>
  /// <inheritdoc/>
  public class ArchetypeConfigurator : BaseBlueprintConfigurator<BlueprintArchetype, ArchetypeConfigurator>
  {
    private ArchetypeConfigurator(string name) : base(name) { }

    /// <inheritdoc cref="Buffs.BuffConfigurator.For(string)"/>
    public static ArchetypeConfigurator For(string name)
    {
      return new ArchetypeConfigurator(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string)"/>
    public static ArchetypeConfigurator New(string name)
    {
      BlueprintTool.Create<BlueprintArchetype>(name);
      return For(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string, string)"/>
    public static ArchetypeConfigurator New(string name, string assetId)
    {
      BlueprintTool.Create<BlueprintArchetype>(name, assetId);
      return For(name);
    }

    /// <summary>
    /// Sets <see cref="BlueprintArchetype.LocalizedName"/>
    /// </summary>
    public ArchetypeConfigurator SetDisplayName(LocalizedString name)
    {
      return OnConfigureInternal(blueprint => blueprint.LocalizedName = name);
    }

    /// <summary>
    /// Sets <see cref="BlueprintArchetype.LocalizedDescription"/>
    /// </summary>
    public ArchetypeConfigurator SetDescription(LocalizedString description)
    {
      return OnConfigureInternal(blueprint => blueprint.LocalizedDescription = description);
    }
  }
}