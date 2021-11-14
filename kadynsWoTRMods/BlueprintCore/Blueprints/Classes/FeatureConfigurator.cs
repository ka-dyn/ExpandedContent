using Kingmaker.Blueprints.Classes;

namespace BlueprintCore.Blueprints.Classes
{
  /// <summary>Configurator for <see cref="BlueprintFeature"/>.</summary>
  /// <inheritdoc/>
  public class FeatureConfigurator : BaseFeatureConfigurator<BlueprintFeature, FeatureConfigurator>
  {
    private FeatureConfigurator(string name) : base(name) { }

    /// <inheritdoc cref="Buffs.BuffConfigurator.For(string)"/>
    public static FeatureConfigurator For(string name)
    {
      return new FeatureConfigurator(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string)"/>
    public static FeatureConfigurator New(string name)
    {
      BlueprintTool.Create<BlueprintFeature>(name);
      return For(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string, string)"/>
    public static FeatureConfigurator New(string name, string assetId)
    {
      BlueprintTool.Create<BlueprintFeature>(name, assetId);
      return For(name);
    }
  }
}