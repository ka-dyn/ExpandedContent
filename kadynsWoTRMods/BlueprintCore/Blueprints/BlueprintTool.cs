using System;
using System.Collections.Generic;
using System.Linq;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;

namespace BlueprintCore.Blueprints
{
  /// <summary>
  /// Tool for operations on blueprints.
  /// </summary>
  public static class BlueprintTool
  {
    private static readonly LogWrapper Logger = LogWrapper.GetInternal("BlueprintTool");
    private static readonly Dictionary<string, Guid> GuidsByName = new();

    /// <summary>Adds the provided mapping from Name (key) to Guid (value).</summary>
    /// 
    /// <remarks>
    /// <para>
    /// After calling this function you can reference blueprints by Name in BlueprintCore APIs.
    /// </para>
    /// 
    /// <example>
    /// Add a mapping for the Power Attack feat and check to see if the caster has it in a
    /// <see cref="Conditions.Builder.ConditionsBuilder">ConditionsBuilder</see>:
    /// <code>
    ///   BlueprintTool.AddGuidsByName(
    ///       new Dictionary&lt;string, string> { { "PowerAttackFeat", "9972f33f977fc724c838e59641b2fca5" } });
    ///   var conditionsChecker = ConditionsBuilder.New().CasterHasFact("PowerAttackFeat").Build();
    /// </code>
    /// </example>
    /// </remarks>
    public static void AddGuidsByName(Dictionary<string, string> guidsByName)
    {
      AddGuidsByName(guidsByName.Select(entry => (entry.Key, entry.Value)).ToArray());
    }

    /// <summary>Adds the provided mapping from Name to Guid.</summary>
    /// 
    /// <remarks>
    /// <para>
    /// After calling this function you can reference blueprints by Name in BlueprintCore APIs.
    /// </para>
    /// 
    /// <example>
    /// Add a mapping for the Power Attack feat and check to see if the caster has it in a
    /// <see cref="Conditions.Builder.ConditionsBuilder">ConditionsBuilder</see>:
    /// <code>
    ///   BlueprintTool.AddGuidsByName(("PowerAttackFeat", "9972f33f977fc724c838e59641b2fca5"));
    ///   var conditionsChecker = ConditionsBuilder.New().CasterHasFact("PowerAttackFeat").Build();
    /// </code>
    /// </example>
    /// </remarks>
    public static void AddGuidsByName(params (string name, string guid)[] guidsByName)
    {
      guidsByName
          .ToList()
          .ForEach(entry =>
              {
                if (GuidsByName.TryGetValue(entry.name, out Guid guid))
                {
                  if (guid.ToString() == entry.guid)
                  {
                    // Duplicate name to guid mapping, ignore
                    return;
                  }
                  else
                  {
                    throw new InvalidOperationException(
                        $"Duplicate GuidByName. {entry.name} - {entry.guid} requested, but"
                        + $" {entry.name} already maps to {guid}.\n");
                  }
                }

                Logger.Verbose($"Adding GuidByName: {entry.name} - {entry.guid}");
                guid = Guid.Parse(entry.guid);
                GuidsByName.Add(entry.name, guid);
              });
    }

    /// <summary>Creates a new Blueprint of type T.</summary>
    /// 
    /// <remarks>
    /// The Name must be registered using <see cref="AddGuidsByName"/> before calling this. If you prefer
    /// using guid references directly use <see cref="Create{T}(string, string)"/> instead.
    /// </remarks>
    public static T Create<T>(string name) where T : SimpleBlueprint, new()
    {
      return Create<T>(name, GuidsByName[name]);
    }

    /// <summary>Creates a new Blueprint of type T with the given Name and Guid.</summary>
    /// 
    /// <remarks>
    /// If you have already registered the Name and Guid using <see cref="AddGuidsByName"/> you can use
    /// <see cref="Create{T}(string)"/> instead.
    /// </remarks>
    public static T Create<T>(string name, string guid) where T : SimpleBlueprint, new()
    {
      return Create<T>(name, Guid.Parse(guid));
    }

    private static T Create<T>(string name, Guid assetId) where T : SimpleBlueprint, new()
    {
      var guid = new BlueprintGuid(assetId);
      var existingAsset = ResourcesLibrary.TryGetBlueprint(guid);
      if (existingAsset != null)
      {
        throw new InvalidOperationException(
            $"Blueprint creation failed: {name} - {assetId}.\nAlready in use by: {existingAsset.name}.");
      }

      T asset = new()
      {
        name = name,
        AssetGuid = new BlueprintGuid(assetId)
      };
      ResourcesLibrary.BlueprintsCache.AddCachedBlueprint(guid, asset);
      return asset;
    }

    /// <summary>Returns the blueprint with the specified Name or Guid.</summary>
    /// 
    /// <param name="nameOrGuid">Use Name if you have registered it using <see cref="AddGuidsByName"/> or Guid otherwise.</param>
    public static T Get<T>(string nameOrGuid) where T : SimpleBlueprint
    {
      if (!GuidsByName.TryGetValue(nameOrGuid, out Guid assetId)) { assetId = Guid.Parse(nameOrGuid); }

      SimpleBlueprint asset = ResourcesLibrary.TryGetBlueprint(new BlueprintGuid(assetId));
      if (asset is T result) { return result; }
      else
      {
        throw new InvalidOperationException(
            $"Failed to fetch blueprint: {nameOrGuid} - {assetId}.\nIs the type correct? {typeof(T)}");
      }
    }

    /// <summary>Returns a blueprint reference for the specified Name or Guid</summary>
    /// 
    /// <remarks>
    /// This is based on <see cref="BlueprintReferenceBase.CreateTyped{TRef}(SimpleBlueprint)"/> but does not require
    /// fetching the blueprint. This allows referencing a blueprint that has not been created yet. However, if that
    /// blueprint is not created before the reference is used it may fail in unpredictable ways.
    /// </remarks>
    /// 
    /// <param name="nameOrGuid">Use Name if you have registered it using <see cref="AddGuidsByName"/> or Guid otherwise.</param>
    /// <returns>A blueprint reference of type T. If nameOrGuid it returns a non-null, empty reference.</returns>
    public static TRef GetRef<TRef>(string nameOrGuid)
        where TRef : BlueprintReferenceBase, new()
    {
      if (string.IsNullOrEmpty(nameOrGuid)) { return BlueprintReferenceBase.CreateTyped<TRef>(null); }

      if (!GuidsByName.TryGetValue(nameOrGuid, out Guid assetId)) { assetId = Guid.Parse(nameOrGuid); }

      // Copied from BlueprintReferenceBase to allow creating a reference w/o fetching a blueprint.This allows
      // referencing a blueprint before it is added to the cache.
      var reference = Activator.CreateInstance<TRef>();
      reference.deserializedGuid = new BlueprintGuid(assetId);
      return reference;
    }
  }

  /// <summary>Extension methods for types inheriting from <see cref="BlueprintScriptableObject"/></summary>
  public static class BlueprintExtensions
  {
    /// <summary>Returns the first <see cref="BlueprintComponent"/> with the same type as the specified component.</summary>
    public static BlueprintComponent GetComponentMatchingType(this BlueprintScriptableObject obj, BlueprintComponent component)
    {
      foreach (BlueprintComponent current in obj.ComponentsArray)
      {
        if (current.GetType() == component.GetType()) { return current; }
      }
      return null;
    }

    /// <summary> Adds all provided components to the blueprint. </summary>
    public static void AddComponents( this BlueprintScriptableObject obj, params BlueprintComponent[] components)
    {
      if (components == null) { return; }
      obj.SetComponents(CommonTool.Append(components, obj.Components));
    }

    /// <summary>Sets the blueprint's components to the provided list.</summary>
    /// 
    /// <remarks>
    /// <para>
    /// Modified from <see href="https://github.com/Vek17/WrathMods-TabletopTweaks">TabletopTweaks ExtensionMethods</see>.
    /// </para>
    /// 
    /// <para>
    /// This is the preferred way to update a blueprint's components; it ensures that each component has a unique name.
    /// This is important for proper serialization behavior.
    /// </para>
    /// </remarks>
    public static void SetComponents(this BlueprintScriptableObject obj, params BlueprintComponent[] components)
    {
      // Fix names of components. Generally this doesn't matter, but if they have serialization
      // state, then their name needs to be unique.
      var names = new HashSet<string>();
      foreach (var c in components)
      {
        if (string.IsNullOrEmpty(c.name)) { c.name = $"${c.GetType().Name}"; }
        if (!names.Add(c.name))
        {
          string name;
          for (int i = 0; !names.Add(name = $"{c.name}${i}"); i++);
          c.name = name;
        }
      }
      obj.Components = components;
    }
  }
}