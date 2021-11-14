using BlueprintCore.Blueprints.Facts;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Designers.Mechanics.Facts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueprintCore.Blueprints.Classes
{
  /// <summary>
  /// Implements common fields and component support for blueprints inheriting from <see cref="BlueprintFeature"/>.
  /// </summary>
  /// <inheritdoc/>
  public abstract class BaseFeatureConfigurator<T, TBuilder> : BlueprintUnitFactConfigurator<T, TBuilder>
      where T : BlueprintFeature
      where TBuilder : BaseBlueprintConfigurator<T, TBuilder>
  {
    private FeatureTag EnableFeatureTags;
    private FeatureTag DisableFeatureTags;

    private readonly List<BlueprintFeatureReference> EnableIsPrerequisiteFor = new();
    private readonly List<BlueprintFeatureReference> DisableIsPrerequisiteFor = new();

    private readonly List<FeatureGroup> EnableGroups = new();
    private readonly List<FeatureGroup> DisableGroups = new();

    protected BaseFeatureConfigurator(string name) : base(name) { }

    /// <summary>
    /// Modifies <see cref="BlueprintFeature.Groups"/>
    /// </summary>
    public TBuilder AddFeatureGroups(params FeatureGroup[] groups)
    {
      EnableGroups.AddRange(groups);
      return Self;
    }

    /// <summary>
    /// Modifies <see cref="BlueprintFeature.Groups"/>
    /// </summary>
    public TBuilder RemoveFeatureGroups(params FeatureGroup[] groups)
    {
      DisableGroups.AddRange(groups);
      return Self;
    }

    /// <summary>
    /// Sets <see cref="BlueprintFeature.IsClassFeature"/>
    /// </summary>
    public TBuilder SetIsClassFeature(bool isClassFeature = true)
    {
      return OnConfigureInternal(blueprint => blueprint.IsClassFeature = isClassFeature);
    }

    /// <summary>
    /// Modifies <see cref="BlueprintFeature.IsPrerequisiteFor"/>
    /// </summary>
    /// 
    /// <param name="features"><see cref="BlueprintFeature"/></param>
    public TBuilder AddIsPrerequisiteFor(params string[] features)
    {
      EnableIsPrerequisiteFor.AddRange(
          features.Select(feature => BlueprintTool.GetRef<BlueprintFeatureReference>(feature)).ToArray());
      return Self;
    }

    /// <summary>
    /// Modifies <see cref="BlueprintFeature.IsPrerequisiteFor"/>
    /// </summary>
    /// 
    /// <param name="features"><see cref="BlueprintFeature"/></param>
    public TBuilder RemoveIsPrerequisiteFor(params string[] features)
    {
      DisableIsPrerequisiteFor.AddRange(
          features
              .Select(
                  feature => BlueprintTool.GetRef<BlueprintFeatureReference>(feature))
              .ToArray());
      return Self;
    }

    /// <summary>
    /// Sets <see cref="BlueprintFeature.Ranks"/>
    /// </summary>
    public TBuilder SetRanks(int ranks)
    {
      return OnConfigureInternal(blueprint => blueprint.Ranks = ranks);
    }

    /// <summary>
    /// Sets <see cref="BlueprintFeature.ReapplyOnLevelUp"/>
    /// </summary>
    public TBuilder SetReapplyOnLevelUp(bool reapply = true)
    {
      return OnConfigureInternal(blueprint => blueprint.ReapplyOnLevelUp = reapply);
    }

    /// <summary>
    /// Adds or modifies <see cref="FeatureTagsComponent"/>
    /// </summary>
    public TBuilder AddFeatureTags(params FeatureTag[] tags)
    {
      foreach (FeatureTag tag in tags) { EnableFeatureTags |= tag; }
      return Self;
    }

    /// <summary>
    /// Modifies <see cref="FeatureTagsComponent"/>
    /// </summary>
    public TBuilder RemoveFeatureTags(params FeatureTag[] tags)
    {
      foreach (FeatureTag tag in tags) { DisableFeatureTags |= tag; }
      return Self;
    }

    protected override void ConfigureInternal()
    {
      base.ConfigureInternal();

      if (EnableFeatureTags > 0 || DisableFeatureTags > 0) { ConfigureFeatureTags(); }

      if (EnableIsPrerequisiteFor.Count > 0 || DisableIsPrerequisiteFor.Count > 0)
      {
        ConfigureIsPrerequisiteFor();
      }
      if (EnableGroups.Count > 0 || DisableGroups.Count > 0) { ConfigureFeatureGroups(); }
    }

    protected override void ValidateInternal()
    {
      base.ValidateInternal();

      if (Blueprint.GetComponents<FeatureTagsComponent>().Count() > 1)
      {
        AddValidationWarning("Multiple FeatureTagsComponents present. Only the first is used.");
      }
    }

    private void ConfigureFeatureTags()
    {
      var component = Blueprint.GetComponent<FeatureTagsComponent>();
      if (component == null)
      {
        // Don't create a component to disable tags
        if (EnableFeatureTags == 0) { return; }

        component = new FeatureTagsComponent();
        AddComponent(component);
      }
      component.FeatureTags |= EnableFeatureTags;
      component.FeatureTags &= ~DisableFeatureTags;
    }

    private void ConfigureIsPrerequisiteFor()
    {
      EnableIsPrerequisiteFor.AddRange(
          Blueprint.IsPrerequisiteFor ?? new List<BlueprintFeatureReference>());
      foreach (BlueprintFeatureReference disableRef in DisableIsPrerequisiteFor)
      {
        EnableIsPrerequisiteFor.Remove(disableRef);
      }
      Blueprint.IsPrerequisiteFor = EnableIsPrerequisiteFor;
    }

    private void ConfigureFeatureGroups()
    {
      EnableGroups.AddRange(Blueprint.Groups ?? Array.Empty<FeatureGroup>());
      foreach (FeatureGroup disableGroup in DisableGroups)
      {
        EnableGroups.Remove(disableGroup);
      }
      Blueprint.Groups = EnableGroups.ToArray();
    }
  }
}