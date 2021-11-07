using BlueprintCore.Blueprints;
using BlueprintCore.Utils;
using Kingmaker.Blueprints;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Persistence.Versioning.PlayerUpgraderOnlyActions;
using Kingmaker.EntitySystem.Persistence.Versioning.UnitUpgraderOnlyActions;
using Kingmaker.EntitySystem.Persistence.Versioning.UpgraderOnlyActions;
using System.Linq;

namespace BlueprintCore.Actions.Builder.UpgraderEx
{
  /// <summary>
  /// Extension to <see cref="ActionsBuilder"/> for all UpgraderOnlyActions.
  /// </summary>
  /// <inheritdoc cref="ActionsBuilder"/>
  public static class ActionsBuilderUpgraderEx
  {
    //----- Kingmaker.EntitySystem.Persistence.Versioning.*UpgraderOnlyActions -----//

    /// <summary>
    /// Adds <see cref="Kingmaker.EntitySystem.Persistence.Versioning.PlayerUpgraderOnlyActions.AddFactIfEtudePlaying">AddFactIfEtudePlaying</see>
    /// </summary>
    /// 
    /// <param name="fact"><see cref="Kingmaker.Blueprints.Facts.BlueprintUnitFact">BlueprintUnitFact</see></param>
    /// <param name="etude"><see cref="Kingmaker.AreaLogic.Etudes.BlueprintEtude">BlueprintEtude</see></param>
    [Implements(typeof(AddFactIfEtudePlaying))]
    public static ActionsBuilder AddFactIfEtudePlaying(
        this ActionsBuilder builder,
        AddFactIfEtudePlaying.TargetType target,
        string fact,
        string etude)
    {
      var addFact = ElementTool.Create<AddFactIfEtudePlaying>();
      addFact.m_Target = target;
      addFact.m_Fact = BlueprintTool.GetRef<BlueprintUnitFactReference>(fact);
      addFact.m_Etude = BlueprintTool.GetRef<BlueprintEtudeReference>(etude);
      return builder.Add(addFact);
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.EntitySystem.Persistence.Versioning.PlayerUpgraderOnlyActions.FixKingdomSystemBuffsAndStats">FixKingdomSystemBuffsAndStats</see>
    /// </summary>
    [Implements(typeof(FixKingdomSystemBuffsAndStats))]
    public static ActionsBuilder FixKingdomSystemBuffsAndStats(
        this ActionsBuilder builder,
        float? statPerFinances = null,
        float? statPerMaterials = null,
        float? statPerFavors = null,
        float? expDiplomacyCoefficient = null,
        float? diplomacyBonusCoefficient = null)
    {
      var fix = ElementTool.Create<FixKingdomSystemBuffsAndStats>();
      fix.m_StatPerFinances = statPerFinances is null ? fix.m_StatPerFinances : statPerFinances.Value;
      fix.m_StatPerMaterials = statPerMaterials is null ? fix.m_StatPerMaterials : statPerMaterials.Value;
      fix.m_StatPerFavors = statPerFavors is null ? fix.m_StatPerFavors : statPerFavors.Value;
      fix.m_UnitExpDiplomacyCoefficient =
          expDiplomacyCoefficient is null ? fix.m_UnitExpDiplomacyCoefficient : expDiplomacyCoefficient.Value;
      fix.m_DiplomacyBonusCoefficient =
          diplomacyBonusCoefficient is null ? fix.m_DiplomacyBonusCoefficient : diplomacyBonusCoefficient.Value;
      return builder.Add(fix);
    }

    /// <summary>
    /// Adds <see cref="ReenterScriptzone"/>
    /// </summary>
    [Implements(typeof(ReenterScriptzone))]
    public static ActionsBuilder ReEnterScriptZone(this ActionsBuilder builder, EntityReference scriptZone)
    {
      var reEnter = ElementTool.Create<ReenterScriptzone>();
      reEnter.m_ScriptZone = scriptZone;
      return builder.Add(reEnter);
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.EntitySystem.Persistence.Versioning.PlayerUpgraderOnlyActions.RefreshCrusadeLogistic">RefreshCrusadeLogistic</see>
    /// </summary>
    [Implements(typeof(RefreshCrusadeLogistic))]
    public static ActionsBuilder RefreshCrusadeLogistic(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<RefreshCrusadeLogistic>());
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.EntitySystem.Persistence.Versioning.PlayerUpgraderOnlyActions.RefreshSettingsPreset">RefreshSettingsPreset</see>
    /// </summary>
    [Implements(typeof(RefreshSettingsPreset))]
    public static ActionsBuilder RefreshSettingsPreset(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<RefreshSettingsPreset>());
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.EntitySystem.Persistence.Versioning.PlayerUpgraderOnlyActions.RemoveFact">RemoveFact</see>
    /// </summary>
    /// 
    /// <param name="fact"><see cref="Kingmaker.Blueprints.Facts.BlueprintUnitFact">BlueprintUnitFact</see></param>
    /// <param name="ignoreFacts"><see cref="Kingmaker.Blueprints.Facts.BlueprintUnitFact">BlueprintUnitFact</see>
    /// If the target has any of these facts, the fact will not be removed.
    /// </param>
    [Implements(typeof(RemoveFact))]
    public static ActionsBuilder RemoveFact(
        this ActionsBuilder builder,
        string fact,
        params string[] ignoreFacts)
    {
      var removeFact = ElementTool.Create<RemoveFact>();
      removeFact.m_Fact = BlueprintTool.GetRef<BlueprintUnitFactReference>(fact);
      removeFact.m_AdditionalExceptHasFacts =
          ignoreFacts.Select(ignore => BlueprintTool.GetRef<BlueprintUnitFactReference>(ignore)).ToArray();
      removeFact.m_ExceptHasFact = BlueprintReferenceBase.CreateTyped<BlueprintUnitFactReference>(null);
      return builder.Add(removeFact);
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.EntitySystem.Persistence.Versioning.PlayerUpgraderOnlyActions.ResetMinDifficulty">ResetMinDifficulty</see>
    /// </summary>
    [Implements(typeof(ResetMinDifficulty))]
    public static ActionsBuilder ResetMinDifficulty(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ResetMinDifficulty>());
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.EntitySystem.Persistence.Versioning.UnitUpgraderOnlyActions.FixItemInInventory">FixItemInInventory</see>
    /// </summary>
    /// 
    /// <param name="addItem"><see cref="Kingmaker.Blueprints.Items.BlueprintItem">BlueprintItem</see></param>
    /// <param name="removeItem"><see cref="Kingmaker.Blueprints.Items.BlueprintItem">BlueprintItem</see></param>
    [Implements(typeof(FixItemInInventory))]
    public static ActionsBuilder FixItemInInventory(
        this ActionsBuilder builder,
        string addItem = null,
        string removeItem = null,
        bool equipItem = false)
    {
      var fix = ElementTool.Create<FixItemInInventory>();
      fix.m_ToAdd = BlueprintTool.GetRef<BlueprintItemReference>(addItem);
      fix.m_ToRemove = BlueprintTool.GetRef<BlueprintItemReference>(removeItem);
      fix.m_TryEquip = equipItem;
      return builder.Add(fix);
    }

    /// <summary>
    /// Adds <see cref="RecreateOnLoad"/>
    /// </summary>
    [Implements(typeof(RecreateOnLoad))]
    public static ActionsBuilder ReCreateOnLoad(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<RecreateOnLoad>());
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.EntitySystem.Persistence.Versioning.UnitUpgraderOnlyActions.SetAlignmentFromBlueprint">SetAlignmentFromBlueprint</see>
    /// </summary>
    [Implements(typeof(SetAlignmentFromBlueprint))]
    public static ActionsBuilder SetAlignmentFromBlueprint(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<SetAlignmentFromBlueprint>());
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.EntitySystem.Persistence.Versioning.UnitUpgraderOnlyActions.SetHandsFromBlueprint">SetHandsFromBlueprint</see>
    /// </summary>
    [Implements(typeof(SetHandsFromBlueprint))]
    public static ActionsBuilder SetHandsFromBlueprint(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<SetHandsFromBlueprint>());
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.EntitySystem.Persistence.Versioning.UpgraderOnlyActions.AddFeatureFromProgression">AddFeatureFromProgression</see>
    /// </summary>
    /// 
    /// <param name="feature"><see cref="Kingmaker.Blueprints.Classes.BlueprintFeature">BlueprintFeature</see></param>
    /// <param name="progression"><see cref="Kingmaker.Blueprints.Classes.BlueprintProgression">BlueprintProgression</see></param>
    /// <param name="archetype"><see cref="Kingmaker.Blueprints.Classes.BlueprintArchetype">BlueprintArchetype</see></param>
    /// <param name="selection"><see cref="Kingmaker.Blueprints.Classes.Selection.BlueprintFeatureSelection">BlueprintFeatureSelection</see></param>
    /// <param name="ignoreFeature"><see cref="Kingmaker.Blueprints.Classes.BlueprintFeature">BlueprintFeature</see></param>
    [Implements(typeof(AddFeatureFromProgression))]
    public static ActionsBuilder AddFeatureFromProgression(
        this ActionsBuilder builder,
        string feature,
        string progression,
        int level,
        string archetype = null,
        string selection = null,
        string ignoreFeature = null)
    {
      var addFeature = ElementTool.Create<AddFeatureFromProgression>();
      addFeature.m_Feature = BlueprintTool.GetRef<BlueprintFeatureReference>(feature);
      addFeature.m_Progression = BlueprintTool.GetRef<BlueprintProgressionReference>(progression);
      addFeature.m_Level = level;
      addFeature.m_Archetype = BlueprintTool.GetRef<BlueprintArchetypeReference>(archetype);
      addFeature.m_Selection = BlueprintTool.GetRef<BlueprintFeatureSelectionReference>(selection);
      addFeature.m_ExceptHasFeature = BlueprintTool.GetRef<BlueprintFeatureReference>(ignoreFeature);
      return builder.Add(addFeature);
    }

    /// <summary>
    /// Adds <see cref="RecheckEtude"/>
    /// </summary>
    /// 
    /// <param name="etude"><see cref="Kingmaker.AreaLogic.Etudes.BlueprintEtude">BlueprintEtude</see></param>
    [Implements(typeof(RecheckEtude))]
    public static ActionsBuilder ReCheckEtude(
        this ActionsBuilder builder, string etude, bool redoAfterTrigger = false)
    {
      var reCheck = ElementTool.Create<RecheckEtude>();
      reCheck.Etude = BlueprintTool.GetRef<BlueprintEtudeReference>(etude);
      reCheck.m_RedoOnceTriggers = redoAfterTrigger;
      return builder.Add(reCheck);
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.EntitySystem.Persistence.Versioning.UpgraderOnlyActions.RefreshAllArmyLeaders">RefreshAllArmyLeaders</see>
    /// </summary>
    [Implements(typeof(RefreshAllArmyLeaders))]
    public static ActionsBuilder RefreshAllArmyLeaders(this ActionsBuilder builder, bool playerOnly = false)
    {
      var refresh = ElementTool.Create<RefreshAllArmyLeaders>();
      refresh.m_OnlyPlayerLeaders = playerOnly;
      return builder.Add(refresh);
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.EntitySystem.Persistence.Versioning.UpgraderOnlyActions.RemoveFeatureFromProgression">RemoveFeatureFromProgression</see>
    /// </summary>
    /// 
    /// <param name="feature"><see cref="Kingmaker.Blueprints.Classes.BlueprintFeature">BlueprintFeature</see></param>
    /// <param name="progression"><see cref="Kingmaker.Blueprints.Classes.BlueprintProgression">BlueprintProgression</see></param>
    /// <param name="archetype"><see cref="Kingmaker.Blueprints.Classes.BlueprintArchetype">BlueprintArchetype</see></param>
    /// <param name="ignoreFeature"><see cref="Kingmaker.Blueprints.Classes.BlueprintFeature">BlueprintFeature</see></param>
    [Implements(typeof(RemoveFeatureFromProgression))]
    public static ActionsBuilder RemoveFeatureFromProgression(
        this ActionsBuilder builder,
        string feature,
        string progression,
        int level,
        string archetype = null,
        string ignoreFeature = null)
    {
      var removeFeature = ElementTool.Create<RemoveFeatureFromProgression>();
      removeFeature.m_Feature = BlueprintTool.GetRef<BlueprintFeatureReference>(feature);
      removeFeature.m_Progression =
          BlueprintTool.GetRef<BlueprintProgressionReference>(progression);
      removeFeature.m_Level = level;
      removeFeature.m_Archetype = BlueprintTool.GetRef<BlueprintArchetypeReference>(archetype);
      removeFeature.m_ExceptHasFeature =
          BlueprintTool.GetRef<BlueprintFeatureReference>(ignoreFeature);
      return builder.Add(removeFeature);
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.EntitySystem.Persistence.Versioning.UpgraderOnlyActions.ReplaceFeature">ReplaceFeature</see>
    /// </summary>
    /// 
    /// <param name="oldFeature"><see cref="Kingmaker.Blueprints.Classes.BlueprintFeature">BlueprintFeature</see></param>
    /// <param name="newFeature"><see cref="Kingmaker.Blueprints.Classes.BlueprintFeature">BlueprintFeature</see></param>
    /// <param name="progression"><see cref="Kingmaker.Blueprints.Classes.BlueprintProgression">BlueprintProgression</see></param>
    /// <param name="ignoreFeature"><see cref="Kingmaker.Blueprints.Classes.BlueprintFeature">BlueprintFeature</see></param>
    [Implements(typeof(ReplaceFeature))]
    public static ActionsBuilder ReplaceFeature(
        this ActionsBuilder builder,
        string oldFeature,
        string newFeature,
        string progression,
        string ignoreFeature = null)
    {
      var replaceFeature = ElementTool.Create<ReplaceFeature>();
      replaceFeature.m_ToReplace = BlueprintTool.GetRef<BlueprintFeatureReference>(oldFeature);
      replaceFeature.m_Replacement = BlueprintTool.GetRef<BlueprintFeatureReference>(newFeature);
      replaceFeature.m_FromProgression =
          BlueprintTool.GetRef<BlueprintProgressionReference>(progression);
      replaceFeature.m_ExceptHasFeature =
          BlueprintTool.GetRef<BlueprintFeatureReference>(ignoreFeature);
      return builder.Add(replaceFeature);
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.EntitySystem.Persistence.Versioning.UpgraderOnlyActions.RestartTacticalCombat">RestartTacticalCombat</see>
    /// </summary>
    [Implements(typeof(RestartTacticalCombat))]
    public static ActionsBuilder RestartTacticalCombat(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<RestartTacticalCombat>());
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.EntitySystem.Persistence.Versioning.UpgraderOnlyActions.SetSharedVendorTable">SetSharedVendorTable</see>
    /// </summary>
    /// 
    /// <param name="table"><see cref="Kingmaker.Blueprints.Items.BlueprintSharedVendorTable">BlueprintSharedVendorTable</see></param>
    [Implements(typeof(SetSharedVendorTable))]
    public static ActionsBuilder SetSharedVendorTable(
        this ActionsBuilder builder, string table, UnitEvaluator unit)
    {
      builder.Validate(unit);

      var setVendorTable = new SetSharedVendorTable(unit);
      ElementTool.Init(setVendorTable);
      setVendorTable.m_Table = BlueprintTool.GetRef<BlueprintSharedVendorTableReference>(table);
      setVendorTable.m_Unit = unit;
      return builder.Add(setVendorTable);
    }

    /// <summary>
    /// Adds <see cref="StartEtudeForced"/>
    /// </summary>
    /// 
    /// <param name="etude"><see cref="Kingmaker.AreaLogic.Etudes.BlueprintEtude">BlueprintEtude</see></param>
    [Implements(typeof(StartEtudeForced))]
    public static ActionsBuilder ForceStartEtude(this ActionsBuilder builder, string etude)
    {
      var startEtude = ElementTool.Create<StartEtudeForced>();
      startEtude.Etude = BlueprintTool.GetRef<BlueprintEtudeReference>(etude);
      return builder.Add(startEtude);
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.EntitySystem.Persistence.Versioning.UpgraderOnlyActions.UnStartEtude">UnStartEtude</see>
    /// </summary>
    /// 
    /// <param name="etude"><see cref="Kingmaker.AreaLogic.Etudes.BlueprintEtude">BlueprintEtude</see></param>
    [Implements(typeof(UnStartEtude))]
    public static ActionsBuilder UnStartEtude(this ActionsBuilder builder, string etude)
    {
      var unStartEtude = ElementTool.Create<UnStartEtude>();
      unStartEtude.Etude = BlueprintTool.GetRef<BlueprintEtudeReference>(etude);
      return builder.Add(unStartEtude);
    }

    //----- Auto Generated -----//



    /// <summary>
    /// Adds <see cref="SetRaceFromBlueprint"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(SetRaceFromBlueprint))]
    public static ActionsBuilder AddSetRaceFromBlueprint(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<SetRaceFromBlueprint>());
    }

    /// <summary>
    /// Adds <see cref="RemoveSpell"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Spell"><see cref="BlueprintAbility"/></param>
    /// <param name="m_Spellbook"><see cref="BlueprintSpellbook"/></param>
    [Generated]
    [Implements(typeof(RemoveSpell))]
    public static ActionsBuilder AddRemoveSpell(
        this ActionsBuilder builder,
        string m_Spell,
        string m_Spellbook)
    {
      
      var element = ElementTool.Create<RemoveSpell>();
      element.m_Spell = BlueprintTool.GetRef<BlueprintAbilityReference>(m_Spell);
      element.m_Spellbook = BlueprintTool.GetRef<BlueprintSpellbookReference>(m_Spellbook);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RestoreClassFeature"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Feature"><see cref="BlueprintFeature"/></param>
    [Generated]
    [Implements(typeof(RestoreClassFeature))]
    public static ActionsBuilder AddRestoreClassFeature(
        this ActionsBuilder builder,
        string m_Feature)
    {
      
      var element = ElementTool.Create<RestoreClassFeature>();
      element.m_Feature = BlueprintTool.GetRef<BlueprintFeatureReference>(m_Feature);
      return builder.Add(element);
    }  }
}
