using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints;
using BlueprintCore.Utils;
using Kingmaker.AreaLogic.Capital;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Area;
using Kingmaker.Corruption;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Dungeon.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Persistence;
using Kingmaker.Globalmap.Blueprints;
using Kingmaker.Localization;
using System;
using System.Linq;

namespace BlueprintCore.Actions.Builder.AreaEx
{
  /// <summary>
  /// Extension to <see cref="ActionsBuilder"/> for actions involving the game map, dungeons, or locations. See also
  /// <see cref="KingdomEx.ActionsBuilderKingdomEx">KingdomEx</see>.
  /// </summary>
  /// <inheritdoc cref="ActionsBuilder"/>
  public static class ActionsBuilderAreaEx
  {
    //----- Kingmaker.Dungeon.Actions -----//

    /// <summary>
    /// Adds <see cref="ActionCreateImportedCompanion"/>
    /// </summary>
    [Implements(typeof(ActionCreateImportedCompanion))]
    public static ActionsBuilder CreateImportedCompanion(this ActionsBuilder builder, int index)
    {
      var createCompanion = ElementTool.Create<ActionCreateImportedCompanion>();
      createCompanion.Index = index;
      return builder.Add(createCompanion);
    }

    /// <summary>
    /// Adds <see cref="ActionEnterToDungeon"/>
    /// </summary>
    [Implements(typeof(ActionEnterToDungeon))]
    public static ActionsBuilder TeleportToLastDungeonStageEntrance(this ActionsBuilder builder, int minStage = 1)
    {
      var teleport = ElementTool.Create<ActionEnterToDungeon>();
      teleport.FirstStage = minStage;
      return builder.Add(teleport);
    }


    /// <summary>
    /// Adds <see cref="ActionGoDeeperIntoDungeon"/>
    /// </summary>
    [Implements(typeof(ActionGoDeeperIntoDungeon))]
    public static ActionsBuilder EnterNextDungeonStage(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ActionGoDeeperIntoDungeon>());
    }

    /// <summary>
    /// Adds <see cref="ActionIncreaseDungeonStage"/>
    /// </summary>
    [Implements(typeof(ActionIncreaseDungeonStage))]
    public static ActionsBuilder IncrementDungeonStage(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ActionIncreaseDungeonStage>());
    }

    /// <summary>
    /// Adds <see cref="ActionSetDungeonStage"/>
    /// </summary>
    [Implements(typeof(ActionSetDungeonStage))]
    public static ActionsBuilder SetDungeonStage(this ActionsBuilder builder, int stage = 1)
    {
      var setStage = ElementTool.Create<ActionSetDungeonStage>();
      setStage.Stage = stage;
      return builder.Add(setStage);
    }

    //----- Kingmaker.Designers.EventConditionActionSystem.Actions -----//

    /// <summary>
    /// Adds <see cref="AreaEntranceChange"/>
    /// </summary>
    /// 
    /// <param name="location"><see cref="BlueprintGlobalMapPoint"/></param>
    /// <param name="newLocation"><see cref="Kingmaker.Blueprints.Area.BlueprintAreaEnterPoint">BlueprintAreaEnterPoint</see></param>
    [Implements(typeof(AreaEntranceChange))]
    public static ActionsBuilder ChangeAreaEntrance(
        this ActionsBuilder builder, string location, string newLocation)
    {
      var changeEntrance = ElementTool.Create<AreaEntranceChange>();
      changeEntrance.m_Location = BlueprintTool.GetRef<BlueprintGlobalMapPoint.Reference>(location);
      changeEntrance.m_NewEntrance = BlueprintTool.GetRef<BlueprintAreaEnterPointReference>(newLocation);
      return builder.Add(changeEntrance);
    }

    /// <summary>
    /// Adds
    /// <see cref="Kingmaker.Designers.EventConditionActionSystem.Actions.ChangeCurrentAreaName">ChangeCurrentAreaName</see>
    /// </summary>
    [Implements(typeof(ChangeCurrentAreaName))]
    public static ActionsBuilder ChangeCurrentAreaName(this ActionsBuilder builder, LocalizedString name)
    {
      var changeName = ElementTool.Create<ChangeCurrentAreaName>();
      changeName.NewName = name;
      return builder.Add(changeName);
    }

    /// <summary>
    /// Adds
    /// <see cref="Kingmaker.Designers.EventConditionActionSystem.Actions.ChangeCurrentAreaName">ChangeCurrentAreaName</see>
    /// </summary>
    [Implements(typeof(ChangeCurrentAreaName))]
    public static ActionsBuilder ResetCurrentAreaName(this ActionsBuilder builder)
    {
      var changeName = ElementTool.Create<ChangeCurrentAreaName>();
      changeName.RestoreDefault = true;
      return builder.Add(changeName);
    }

    /// <summary>
    /// Adds
    /// <see cref="Kingmaker.Designers.EventConditionActionSystem.Actions.AddCampingEncounter">AddCampingEncounter</see>
    /// </summary>
    [Implements(typeof(AddCampingEncounter))]
    public static ActionsBuilder AddCampingEncounter(this ActionsBuilder builder, string encounter)
    {
      var addEncounter = ElementTool.Create<AddCampingEncounter>();
      addEncounter.m_Encounter = BlueprintTool.GetRef<BlueprintCampingEncounterReference>(encounter);
      return builder.Add(addEncounter);
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.Designers.EventConditionActionSystem.Actions.DestroyMapObject">DestroyMapObject</see>
    /// </summary>
    [Implements(typeof(DestroyMapObject))]
    public static ActionsBuilder DestroyMapObject(this ActionsBuilder builder, MapObjectEvaluator obj)
    {
      var destroy = ElementTool.Create<DestroyMapObject>();
      destroy.MapObject = obj;
      return builder.Add(destroy);
    }

    //----- Auto Generated -----//



    /// <summary>
    /// Adds <see cref="CapitalExit"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Destination"><see cref="BlueprintAreaEnterPoint"/></param>
    [Generated]
    [Implements(typeof(CapitalExit))]
    public static ActionsBuilder AddCapitalExit(
        this ActionsBuilder builder,
        string m_Destination,
        AutoSaveMode AutoSaveMode)
    {
      builder.Validate(AutoSaveMode);
      
      var element = ElementTool.Create<CapitalExit>();
      element.m_Destination = BlueprintTool.GetRef<BlueprintAreaEnterPointReference>(m_Destination);
      element.AutoSaveMode = AutoSaveMode;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="DecreaseCorruptionLevelAction"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(DecreaseCorruptionLevelAction))]
    public static ActionsBuilder AddDecreaseCorruptionLevelAction(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<DecreaseCorruptionLevelAction>());
    }

    /// <summary>
    /// Adds <see cref="AskPlayerForLocationName"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Location"><see cref="BlueprintGlobalMapPoint"/></param>
    [Generated]
    [Implements(typeof(AskPlayerForLocationName))]
    public static ActionsBuilder AddAskPlayerForLocationName(
        this ActionsBuilder builder,
        string m_Location,
        Boolean Obligatory,
        LocalizedString Title,
        LocalizedString Hint,
        LocalizedString Default)
    {
      builder.Validate(Obligatory);
      builder.Validate(Title);
      builder.Validate(Hint);
      builder.Validate(Default);
      
      var element = ElementTool.Create<AskPlayerForLocationName>();
      element.m_Location = BlueprintTool.GetRef<BlueprintGlobalMapPoint.Reference>(m_Location);
      element.Obligatory = Obligatory;
      element.Title = Title;
      element.Hint = Hint;
      element.Default = Default;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="GlobalMapTeleport"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(GlobalMapTeleport))]
    public static ActionsBuilder AddGlobalMapTeleport(
        this ActionsBuilder builder,
        LocationEvaluator Destination,
        FloatEvaluator SkipHours,
        Boolean UpdateLocationVisitedTime)
    {
      builder.Validate(Destination);
      builder.Validate(SkipHours);
      builder.Validate(UpdateLocationVisitedTime);
      
      var element = ElementTool.Create<GlobalMapTeleport>();
      element.Destination = Destination;
      element.SkipHours = SkipHours;
      element.UpdateLocationVisitedTime = UpdateLocationVisitedTime;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="HideMapObject"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(HideMapObject))]
    public static ActionsBuilder AddHideMapObject(
        this ActionsBuilder builder,
        MapObjectEvaluator MapObject,
        Boolean Unhide)
    {
      builder.Validate(MapObject);
      builder.Validate(Unhide);
      
      var element = ElementTool.Create<HideMapObject>();
      element.MapObject = MapObject;
      element.Unhide = Unhide;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="LocalMapSetDirty"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(LocalMapSetDirty))]
    public static ActionsBuilder AddLocalMapSetDirty(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<LocalMapSetDirty>());
    }

    /// <summary>
    /// Adds <see cref="MakeServiceCaster"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(MakeServiceCaster))]
    public static ActionsBuilder AddMakeServiceCaster(
        this ActionsBuilder builder,
        UnitEvaluator Unit)
    {
      builder.Validate(Unit);
      
      var element = ElementTool.Create<MakeServiceCaster>();
      element.Unit = Unit;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="MarkLocationClosed"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Location"><see cref="BlueprintGlobalMapPoint"/></param>
    [Generated]
    [Implements(typeof(MarkLocationClosed))]
    public static ActionsBuilder AddMarkLocationClosed(
        this ActionsBuilder builder,
        string m_Location,
        Boolean Closed)
    {
      builder.Validate(Closed);
      
      var element = ElementTool.Create<MarkLocationClosed>();
      element.m_Location = BlueprintTool.GetRef<BlueprintGlobalMapPoint.Reference>(m_Location);
      element.Closed = Closed;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="MarkLocationExplored"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Location"><see cref="BlueprintGlobalMapPoint"/></param>
    [Generated]
    [Implements(typeof(MarkLocationExplored))]
    public static ActionsBuilder AddMarkLocationExplored(
        this ActionsBuilder builder,
        string m_Location,
        Boolean Explored)
    {
      builder.Validate(Explored);
      
      var element = ElementTool.Create<MarkLocationExplored>();
      element.m_Location = BlueprintTool.GetRef<BlueprintGlobalMapPoint.Reference>(m_Location);
      element.Explored = Explored;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="MarkOnLocalMap"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(MarkOnLocalMap))]
    public static ActionsBuilder AddMarkOnLocalMap(
        this ActionsBuilder builder,
        MapObjectEvaluator MapObject,
        Boolean Hidden)
    {
      builder.Validate(MapObject);
      builder.Validate(Hidden);
      
      var element = ElementTool.Create<MarkOnLocalMap>();
      element.MapObject = MapObject;
      element.Hidden = Hidden;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="OpenLootContainer"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(OpenLootContainer))]
    public static ActionsBuilder AddOpenLootContainer(
        this ActionsBuilder builder,
        MapObjectEvaluator MapObject)
    {
      builder.Validate(MapObject);
      
      var element = ElementTool.Create<OpenLootContainer>();
      element.MapObject = MapObject;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RemoveAllAreasFromSave"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Except"><see cref="BlueprintArea"/></param>
    [Generated]
    [Implements(typeof(RemoveAllAreasFromSave))]
    public static ActionsBuilder AddRemoveAllAreasFromSave(
        this ActionsBuilder builder,
        string[] m_Except)
    {
      
      var element = ElementTool.Create<RemoveAllAreasFromSave>();
      element.m_Except = m_Except.Select(bp => BlueprintTool.GetRef<BlueprintAreaReference>(bp)).ToArray();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RemoveAmbush"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(RemoveAmbush))]
    public static ActionsBuilder AddRemoveAmbush(
        this ActionsBuilder builder,
        UnitEvaluator m_Unit,
        Boolean m_ExitStealth)
    {
      builder.Validate(m_Unit);
      builder.Validate(m_ExitStealth);
      
      var element = ElementTool.Create<RemoveAmbush>();
      element.m_Unit = m_Unit;
      element.m_ExitStealth = m_ExitStealth;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RemoveAreaFromSave"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Area"><see cref="BlueprintArea"/></param>
    /// <param name="SpecificMechanic"><see cref="BlueprintAreaMechanics"/></param>
    [Generated]
    [Implements(typeof(RemoveAreaFromSave))]
    public static ActionsBuilder AddRemoveAreaFromSave(
        this ActionsBuilder builder,
        string m_Area,
        string SpecificMechanic)
    {
      
      var element = ElementTool.Create<RemoveAreaFromSave>();
      element.m_Area = BlueprintTool.GetRef<BlueprintAreaReference>(m_Area);
      element.SpecificMechanic = BlueprintTool.GetRef<BlueprintAreaMechanicsReference>(SpecificMechanic);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RemoveCampingEncounter"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Encounter"><see cref="BlueprintCampingEncounter"/></param>
    [Generated]
    [Implements(typeof(RemoveCampingEncounter))]
    public static ActionsBuilder AddRemoveCampingEncounter(
        this ActionsBuilder builder,
        string m_Encounter)
    {
      
      var element = ElementTool.Create<RemoveCampingEncounter>();
      element.m_Encounter = BlueprintTool.GetRef<BlueprintCampingEncounterReference>(m_Encounter);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ResetLocationPerceptionCheck"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Location"><see cref="BlueprintGlobalMapPoint"/></param>
    [Generated]
    [Implements(typeof(ResetLocationPerceptionCheck))]
    public static ActionsBuilder AddResetLocationPerceptionCheck(
        this ActionsBuilder builder,
        string m_Location)
    {
      
      var element = ElementTool.Create<ResetLocationPerceptionCheck>();
      element.m_Location = BlueprintTool.GetRef<BlueprintGlobalMapPoint.Reference>(m_Location);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RevealGlobalMap"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="Points"><see cref="BlueprintGlobalMapPoint"/></param>
    [Generated]
    [Implements(typeof(RevealGlobalMap))]
    public static ActionsBuilder AddRevealGlobalMap(
        this ActionsBuilder builder,
        string[] Points,
        Boolean RevealEdges)
    {
      builder.Validate(RevealEdges);
      
      var element = ElementTool.Create<RevealGlobalMap>();
      element.Points = Points.Select(bp => BlueprintTool.GetRef<BlueprintGlobalMapPoint.Reference>(bp)).ToArray();
      element.RevealEdges = RevealEdges;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ScriptZoneActivate"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ScriptZoneActivate))]
    public static ActionsBuilder AddScriptZoneActivate(
        this ActionsBuilder builder,
        EntityReference ScriptZone)
    {
      builder.Validate(ScriptZone);
      
      var element = ElementTool.Create<ScriptZoneActivate>();
      element.ScriptZone = ScriptZone;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ScriptZoneDeactivate"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ScriptZoneDeactivate))]
    public static ActionsBuilder AddScriptZoneDeactivate(
        this ActionsBuilder builder,
        EntityReference ScriptZone)
    {
      builder.Validate(ScriptZone);
      
      var element = ElementTool.Create<ScriptZoneDeactivate>();
      element.ScriptZone = ScriptZone;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ScripZoneUnits"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ScripZoneUnits))]
    public static ActionsBuilder AddScripZoneUnits(
        this ActionsBuilder builder,
        EntityReference ScriptZone,
        ActionsBuilder Actions)
    {
      builder.Validate(ScriptZone);
      
      var element = ElementTool.Create<ScripZoneUnits>();
      element.ScriptZone = ScriptZone;
      element.Actions = Actions.Build();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SetDeviceState"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(SetDeviceState))]
    public static ActionsBuilder AddSetDeviceState(
        this ActionsBuilder builder,
        MapObjectEvaluator Device,
        IntEvaluator State)
    {
      builder.Validate(Device);
      builder.Validate(State);
      
      var element = ElementTool.Create<SetDeviceState>();
      element.Device = Device;
      element.State = State;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SetDeviceTrigger"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(SetDeviceTrigger))]
    public static ActionsBuilder AddSetDeviceTrigger(
        this ActionsBuilder builder,
        MapObjectEvaluator Device,
        String Trigger)
    {
      builder.Validate(Device);
      foreach (var item in Trigger)
      {
        builder.Validate(item);
      }
      
      var element = ElementTool.Create<SetDeviceTrigger>();
      element.Device = Device;
      element.Trigger = Trigger;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SetDisableDevice"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(SetDisableDevice))]
    public static ActionsBuilder AddSetDisableDevice(
        this ActionsBuilder builder,
        MapObjectEvaluator MapObject,
        Int32 OverrideDC)
    {
      builder.Validate(MapObject);
      builder.Validate(OverrideDC);
      
      var element = ElementTool.Create<SetDisableDevice>();
      element.MapObject = MapObject;
      element.OverrideDC = OverrideDC;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ShowMultiEntrance"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Map"><see cref="BlueprintMultiEntrance"/></param>
    [Generated]
    [Implements(typeof(ShowMultiEntrance))]
    public static ActionsBuilder AddShowMultiEntrance(
        this ActionsBuilder builder,
        string m_Map)
    {
      
      var element = ElementTool.Create<ShowMultiEntrance>();
      element.m_Map = BlueprintTool.GetRef<BlueprintMultiEntranceReference>(m_Map);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SpotMapObject"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(SpotMapObject))]
    public static ActionsBuilder AddSpotMapObject(
        this ActionsBuilder builder,
        MapObjectEvaluator Target,
        UnitEvaluator Spotter)
    {
      builder.Validate(Target);
      builder.Validate(Spotter);
      
      var element = ElementTool.Create<SpotMapObject>();
      element.Target = Target;
      element.Spotter = Spotter;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SpotUnit"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(SpotUnit))]
    public static ActionsBuilder AddSpotUnit(
        this ActionsBuilder builder,
        UnitEvaluator Target,
        UnitEvaluator Spotter)
    {
      builder.Validate(Target);
      builder.Validate(Spotter);
      
      var element = ElementTool.Create<SpotUnit>();
      element.Target = Target;
      element.Spotter = Spotter;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="TeleportParty"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_exitPositon"><see cref="BlueprintAreaEnterPoint"/></param>
    [Generated]
    [Implements(typeof(TeleportParty))]
    public static ActionsBuilder AddTeleportParty(
        this ActionsBuilder builder,
        string m_exitPositon,
        AutoSaveMode AutoSaveMode,
        Boolean ForcePauseAfterTeleport,
        ActionsBuilder AfterTeleport)
    {
      builder.Validate(AutoSaveMode);
      builder.Validate(ForcePauseAfterTeleport);
      
      var element = ElementTool.Create<TeleportParty>();
      element.m_exitPositon = BlueprintTool.GetRef<BlueprintAreaEnterPointReference>(m_exitPositon);
      element.AutoSaveMode = AutoSaveMode;
      element.ForcePauseAfterTeleport = ForcePauseAfterTeleport;
      element.AfterTeleport = AfterTeleport.Build();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="TranslocatePlayer"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(TranslocatePlayer))]
    public static ActionsBuilder AddTranslocatePlayer(
        this ActionsBuilder builder,
        EntityReference transolcatePosition,
        Boolean ByFormationAndWithPets,
        Boolean ScrollCameraToPlayer)
    {
      builder.Validate(transolcatePosition);
      builder.Validate(ByFormationAndWithPets);
      builder.Validate(ScrollCameraToPlayer);
      
      var element = ElementTool.Create<TranslocatePlayer>();
      element.transolcatePosition = transolcatePosition;
      element.ByFormationAndWithPets = ByFormationAndWithPets;
      element.ScrollCameraToPlayer = ScrollCameraToPlayer;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="TranslocateUnit"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(TranslocateUnit))]
    public static ActionsBuilder AddTranslocateUnit(
        this ActionsBuilder builder,
        UnitEvaluator Unit,
        EntityReference translocatePosition,
        PositionEvaluator translocatePositionEvaluator,
        Boolean m_CopyRotation,
        FloatEvaluator translocateOrientationEvaluator)
    {
      builder.Validate(Unit);
      builder.Validate(translocatePosition);
      builder.Validate(translocatePositionEvaluator);
      builder.Validate(m_CopyRotation);
      builder.Validate(translocateOrientationEvaluator);
      
      var element = ElementTool.Create<TranslocateUnit>();
      element.Unit = Unit;
      element.translocatePosition = translocatePosition;
      element.translocatePositionEvaluator = translocatePositionEvaluator;
      element.m_CopyRotation = m_CopyRotation;
      element.translocateOrientationEvaluator = translocateOrientationEvaluator;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="TrapCastSpell"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Spell"><see cref="BlueprintAbility"/></param>
    [Generated]
    [Implements(typeof(TrapCastSpell))]
    public static ActionsBuilder AddTrapCastSpell(
        this ActionsBuilder builder,
        string m_Spell,
        MapObjectEvaluator TrapObject,
        UnitEvaluator TriggeringUnit,
        PositionEvaluator TargetPoint,
        PositionEvaluator ActorPosition,
        Boolean DisableBattleLog,
        Boolean OverrideDC,
        Int32 DC,
        Boolean OverrideSpellLevel,
        Int32 SpellLevel)
    {
      builder.Validate(TrapObject);
      builder.Validate(TriggeringUnit);
      builder.Validate(TargetPoint);
      builder.Validate(ActorPosition);
      builder.Validate(DisableBattleLog);
      builder.Validate(OverrideDC);
      builder.Validate(DC);
      builder.Validate(OverrideSpellLevel);
      builder.Validate(SpellLevel);
      
      var element = ElementTool.Create<TrapCastSpell>();
      element.m_Spell = BlueprintTool.GetRef<BlueprintAbilityReference>(m_Spell);
      element.TrapObject = TrapObject;
      element.TriggeringUnit = TriggeringUnit;
      element.TargetPoint = TargetPoint;
      element.ActorPosition = ActorPosition;
      element.DisableBattleLog = DisableBattleLog;
      element.OverrideDC = OverrideDC;
      element.DC = DC;
      element.OverrideSpellLevel = OverrideSpellLevel;
      element.SpellLevel = SpellLevel;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnlockCookingRecipe"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Recipe"><see cref="BlueprintCookingRecipe"/></param>
    [Generated]
    [Implements(typeof(UnlockCookingRecipe))]
    public static ActionsBuilder AddUnlockCookingRecipe(
        this ActionsBuilder builder,
        string m_Recipe)
    {
      
      var element = ElementTool.Create<UnlockCookingRecipe>();
      element.m_Recipe = BlueprintTool.GetRef<BlueprintCookingRecipeReference>(m_Recipe);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnlockLocation"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Location"><see cref="BlueprintGlobalMapPoint"/></param>
    [Generated]
    [Implements(typeof(UnlockLocation))]
    public static ActionsBuilder AddUnlockLocation(
        this ActionsBuilder builder,
        string m_Location,
        Boolean FakeDescription,
        Boolean HideInstead)
    {
      builder.Validate(FakeDescription);
      builder.Validate(HideInstead);
      
      var element = ElementTool.Create<UnlockLocation>();
      element.m_Location = BlueprintTool.GetRef<BlueprintGlobalMapPoint.Reference>(m_Location);
      element.FakeDescription = FakeDescription;
      element.HideInstead = HideInstead;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnlockMapEdge"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Edge"><see cref="BlueprintGlobalMapEdge"/></param>
    [Generated]
    [Implements(typeof(UnlockMapEdge))]
    public static ActionsBuilder AddUnlockMapEdge(
        this ActionsBuilder builder,
        string m_Edge,
        Boolean OpenEdges)
    {
      builder.Validate(OpenEdges);
      
      var element = ElementTool.Create<UnlockMapEdge>();
      element.m_Edge = BlueprintTool.GetRef<BlueprintGlobalMapEdge.Reference>(m_Edge);
      element.OpenEdges = OpenEdges;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="GameActionSetIsleLock"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(GameActionSetIsleLock))]
    public static ActionsBuilder AddGameActionSetIsleLock(
        this ActionsBuilder builder,
        IsleEvaluator m_Isle,
        Boolean m_IsLock)
    {
      builder.Validate(m_Isle);
      builder.Validate(m_IsLock);
      
      var element = ElementTool.Create<GameActionSetIsleLock>();
      element.m_Isle = m_Isle;
      element.m_IsLock = m_IsLock;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="GameActionSetIsleState"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(GameActionSetIsleState))]
    public static ActionsBuilder AddGameActionSetIsleState(
        this ActionsBuilder builder,
        IsleEvaluator m_Isle,
        String m_StateName)
    {
      builder.Validate(m_Isle);
      foreach (var item in m_StateName)
      {
        builder.Validate(item);
      }
      
      var element = ElementTool.Create<GameActionSetIsleState>();
      element.m_Isle = m_Isle;
      element.m_StateName = m_StateName;
      return builder.Add(element);
    }
  }
}
