using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.Classes.Selection;
using System;
using System.Linq;

namespace BlueprintCore.Blueprints.Classes.Selection
{
  /// <summary>Configurator for <see cref="BlueprintFeatureSelection"/>.</summary>
  /// <inheritdoc/>
  public class FeatureSelectionConfigurator
      : BaseFeatureConfigurator<BlueprintFeatureSelection, FeatureSelectionConfigurator>
  {
    private FeatureSelectionConfigurator(string name) : base(name) { }

    /// <inheritdoc cref="Buffs.BuffConfigurator.For(string)"/>
    public static FeatureSelectionConfigurator For(string name)
    {
      return new FeatureSelectionConfigurator(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string)"/>
    public static FeatureSelectionConfigurator New(string name)
    {
      BlueprintTool.Create<BlueprintFeatureSelection>(name);
      return For(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string, string)"/>
    public static FeatureSelectionConfigurator New(string name, string assetId)
    {
      BlueprintTool.Create<BlueprintFeatureSelection>(name, assetId);
      return For(name);
    }

    /// <summary>
    /// Sets <see cref="BlueprintFeatureSelection.IgnorePrerequisites"/>
    /// </summary>
    public FeatureSelectionConfigurator SetIgnorePrerequisites(bool ignore = true)
    {
      return OnConfigureInternal(blueprint => blueprint.IgnorePrerequisites = ignore);
    }

    /// <summary>
    /// Sets <see cref="BlueprintFeatureSelection.Mode"/>
    /// </summary>
    public FeatureSelectionConfigurator SetMode(SelectionMode mode)
    {
      return OnConfigureInternal(blueprint => blueprint.Mode = mode);
    }

    /// <summary>
    /// Sets <see cref="BlueprintFeatureSelection.Group"/>
    /// </summary>
    public FeatureSelectionConfigurator SetPrimaryGroup(FeatureGroup group)
    {
      return OnConfigureInternal(blueprint => blueprint.Group = group);
    }

    /// <summary>
    /// Sets <see cref="BlueprintFeatureSelection.Group2"/>
    /// </summary>
    public FeatureSelectionConfigurator SetSecondaryGroup(FeatureGroup group)
    {
      return OnConfigureInternal(blueprint => blueprint.Group2 = group);
    }

    /// <summary>
    /// Adds to <see cref="BlueprintFeatureSelection.m_AllFeatures"/>
    /// </summary>
    /// 
    /// <param name="features"><see cref="BlueprintFeature"/></param>
    public FeatureSelectionConfigurator AddFeatures(params string[] features)
    {
      return OnConfigureInternal(
          blueprint =>
          {
            blueprint.m_AllFeatures =
                CommonTool.Append(
                    blueprint.m_AllFeatures,
                    features.Select(feature => BlueprintTool.GetRef<BlueprintFeatureReference>(feature)).ToArray());
          });
    }

    /// <summary>
    /// Removes from <see cref="BlueprintFeatureSelection.m_AllFeatures"/>
    /// </summary>
    /// 
    /// <param name="features"><see cref="BlueprintFeature"/></param>
    public FeatureSelectionConfigurator RemoveFeatures(params string[] features)
    {
      return OnConfigureInternal(
          blueprint =>
          {
            var featureRefs = features.Select(feature => BlueprintTool.GetRef<BlueprintFeatureReference>(feature));
            blueprint.m_AllFeatures = blueprint.m_AllFeatures.Except(featureRefs).ToArray();
          });
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.Blueprints.Classes.Prerequisites.PrerequisiteSelectionPossible">PrerequisiteSelectionPossible</see>
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    /// A feature selection with this component only shows up if the character is eligible for at least one feature.
    /// This is useful when a character has access to different feature selections based on some criteria.
    /// </para>
    /// 
    /// <para>
    /// See ExpandedDefense and WildTalentBonusFeatAir3 blueprints for example usages.
    /// </para>
    /// </remarks>
    public FeatureSelectionConfigurator PrerequisiteSelectionPossible(
        Prerequisite.GroupType group = Prerequisite.GroupType.All,
        bool checkInProgression = false,
        bool hideInUI = false)
    {
      var selectionPossible = PrereqTool.Create<PrerequisiteSelectionPossible>(group, checkInProgression, hideInUI);
      selectionPossible.m_ThisFeature = Blueprint.ToReference<BlueprintFeatureSelectionReference>();
      return AddComponent(selectionPossible);
    }
  }
}