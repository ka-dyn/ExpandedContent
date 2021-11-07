using BlueprintCore.Blueprints;
using BlueprintCore.Utils;
using Kingmaker.Armies.Components;
using Kingmaker.Blueprints;
using Kingmaker.Kingdom;
using Kingmaker.Kingdom.Conditions;
using Kingmaker.Kingdom.Flags;
using Kingmaker.Kingdom.Settlements;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Conditions;
using System;
using System.Linq;
namespace BlueprintCore.Conditions.Builder.KingdomEx
{
  /// <summary>
  /// Extension to <see cref="ConditionsBuilder"/> for conditions involving the Kingdom and Crusade system.
  /// </summary>
  /// <inheritdoc cref="ConditionsBuilder"/>
  public static class ConditionsBuilderKingdomEx
  {
    //----- Auto Generated -----//



    /// <summary>
    /// Adds <see cref="HasTacticalMorale"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(HasTacticalMorale))]
    public static ConditionsBuilder AddHasTacticalMorale(
        this ConditionsBuilder builder,
        bool negate = false)
    {
      
      var element = ElementTool.Create<HasTacticalMorale>();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="TacticalCombatSquadHitPointsCondition"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(TacticalCombatSquadHitPointsCondition))]
    public static ConditionsBuilder AddTacticalCombatSquadHitPointsCondition(
        this ConditionsBuilder builder,
        Boolean CheckInitiatorHP,
        CompareOperation.Type Operation,
        ContextValue ReferenceValue,
        bool negate = false)
    {
      builder.Validate(CheckInitiatorHP);
      builder.Validate(Operation);
      builder.Validate(ReferenceValue);
      
      var element = ElementTool.Create<TacticalCombatSquadHitPointsCondition>();
      element.CheckInitiatorHP = CheckInitiatorHP;
      element.Operation = Operation;
      element.ReferenceValue = ReferenceValue;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="TargetHasArmyTag"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(TargetHasArmyTag))]
    public static ConditionsBuilder AddTargetHasArmyTag(
        this ConditionsBuilder builder,
        ArmyProperties m_Tags,
        Boolean m_NeedAllTags,
        bool negate = false)
    {
      builder.Validate(m_Tags);
      builder.Validate(m_NeedAllTags);
      
      var element = ElementTool.Create<TargetHasArmyTag>();
      element.m_Tags = m_Tags;
      element.m_NeedAllTags = m_NeedAllTags;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ContextConditionGarrisonClear"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_GlobalMapPoint"><see cref="BlueprintGlobalMapPoint"/></param>
    [Generated]
    [Implements(typeof(ContextConditionGarrisonClear))]
    public static ConditionsBuilder AddContextConditionGarrisonClear(
        this ConditionsBuilder builder,
        string m_GlobalMapPoint,
        bool negate = false)
    {
      
      var element = ElementTool.Create<ContextConditionGarrisonClear>();
      element.m_GlobalMapPoint = BlueprintTool.GetRef<BlueprintGlobalMapPointReference>(m_GlobalMapPoint);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="AnySettlementUnderSiege"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(AnySettlementUnderSiege))]
    public static ConditionsBuilder AddAnySettlementUnderSiege(
        this ConditionsBuilder builder,
        bool negate = false)
    {
      
      var element = ElementTool.Create<AnySettlementUnderSiege>();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomMoraleFlagCondition"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Flag"><see cref="BlueprintKingdomMoraleFlag"/></param>
    [Generated]
    [Implements(typeof(KingdomMoraleFlagCondition))]
    public static ConditionsBuilder AddKingdomMoraleFlagCondition(
        this ConditionsBuilder builder,
        string m_Flag,
        KingdomMoraleFlag.State m_State,
        bool negate = false)
    {
      builder.Validate(m_State);
      
      var element = ElementTool.Create<KingdomMoraleFlagCondition>();
      element.m_Flag = BlueprintTool.GetRef<BlueprintKingdomMoraleFlag.Reference>(m_Flag);
      element.m_State = m_State;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ArmyInLocationDefeated"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Location"><see cref="BlueprintGlobalMapPoint"/></param>
    [Generated]
    [Implements(typeof(ArmyInLocationDefeated))]
    public static ConditionsBuilder AddArmyInLocationDefeated(
        this ConditionsBuilder builder,
        string m_Location,
        bool negate = false)
    {
      
      var element = ElementTool.Create<ArmyInLocationDefeated>();
      element.m_Location = BlueprintTool.GetRef<BlueprintGlobalMapPointReference>(m_Location);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="AutoKingdom"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(AutoKingdom))]
    public static ConditionsBuilder AddAutoKingdom(
        this ConditionsBuilder builder,
        bool negate = false)
    {
      
      var element = ElementTool.Create<AutoKingdom>();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="BuildingHasNeighbours"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_SpecificBuildings"><see cref="BlueprintSettlementBuilding"/></param>
    [Generated]
    [Implements(typeof(BuildingHasNeighbours))]
    public static ConditionsBuilder AddBuildingHasNeighbours(
        this ConditionsBuilder builder,
        string[] m_SpecificBuildings,
        Boolean AnywhereInTown,
        bool negate = false)
    {
      builder.Validate(AnywhereInTown);
      
      var element = ElementTool.Create<BuildingHasNeighbours>();
      element.m_SpecificBuildings = m_SpecificBuildings.Select(bp => BlueprintTool.GetRef<BlueprintSettlementBuildingReference>(bp)).ToArray();
      element.AnywhereInTown = AnywhereInTown;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="DaysTillNextMonth"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(DaysTillNextMonth))]
    public static ConditionsBuilder AddDaysTillNextMonth(
        this ConditionsBuilder builder,
        Boolean AtMost,
        Int32 Days,
        bool negate = false)
    {
      builder.Validate(AtMost);
      builder.Validate(Days);
      
      var element = ElementTool.Create<DaysTillNextMonth>();
      element.AtMost = AtMost;
      element.Days = Days;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="EventLifetime"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(EventLifetime))]
    public static ConditionsBuilder AddEventLifetime(
        this ConditionsBuilder builder,
        Int32 LessThan,
        Int32 MoreThan,
        bool negate = false)
    {
      builder.Validate(LessThan);
      builder.Validate(MoreThan);
      
      var element = ElementTool.Create<EventLifetime>();
      element.LessThan = LessThan;
      element.MoreThan = MoreThan;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomAlignmentIs"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomAlignmentIs))]
    public static ConditionsBuilder AddKingdomAlignmentIs(
        this ConditionsBuilder builder,
        AlignmentMaskType Alignment,
        bool negate = false)
    {
      builder.Validate(Alignment);
      
      var element = ElementTool.Create<KingdomAlignmentIs>();
      element.Alignment = Alignment;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomAllArmiesInRegionDefeated"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Region"><see cref="BlueprintRegion"/></param>
    [Generated]
    [Implements(typeof(KingdomAllArmiesInRegionDefeated))]
    public static ConditionsBuilder AddKingdomAllArmiesInRegionDefeated(
        this ConditionsBuilder builder,
        string m_Region,
        bool negate = false)
    {
      
      var element = ElementTool.Create<KingdomAllArmiesInRegionDefeated>();
      element.m_Region = BlueprintTool.GetRef<BlueprintRegionReference>(m_Region);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomArtisanState"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Artisan"><see cref="BlueprintKingdomArtisan"/></param>
    [Generated]
    [Implements(typeof(KingdomArtisanState))]
    public static ConditionsBuilder AddKingdomArtisanState(
        this ConditionsBuilder builder,
        string m_Artisan,
        KingdomArtisanState.CheckType _Check,
        Int32 Tier,
        bool negate = false)
    {
      builder.Validate(_Check);
      builder.Validate(Tier);
      
      var element = ElementTool.Create<KingdomArtisanState>();
      element.m_Artisan = BlueprintTool.GetRef<BlueprintKingdomArtisanReference>(m_Artisan);
      element._Check = _Check;
      element.Tier = Tier;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomBuffIsActive"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Blueprint"><see cref="BlueprintKingdomBuff"/></param>
    /// <param name="m_Region"><see cref="BlueprintRegion"/></param>
    [Generated]
    [Implements(typeof(KingdomBuffIsActive))]
    public static ConditionsBuilder AddKingdomBuffIsActive(
        this ConditionsBuilder builder,
        string m_Blueprint,
        string m_Region,
        bool negate = false)
    {
      
      var element = ElementTool.Create<KingdomBuffIsActive>();
      element.m_Blueprint = BlueprintTool.GetRef<BlueprintKingdomBuffReference>(m_Blueprint);
      element.m_Region = BlueprintTool.GetRef<BlueprintRegionReference>(m_Region);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomChapterWeek"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomChapterWeek))]
    public static ConditionsBuilder AddKingdomChapterWeek(
        this ConditionsBuilder builder,
        Int32 Week,
        bool negate = false)
    {
      builder.Validate(Week);
      
      var element = ElementTool.Create<KingdomChapterWeek>();
      element.Week = Week;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomDay"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomDay))]
    public static ConditionsBuilder AddKingdomDay(
        this ConditionsBuilder builder,
        Boolean AtMost,
        Int32 Day,
        bool negate = false)
    {
      builder.Validate(AtMost);
      builder.Validate(Day);
      
      var element = ElementTool.Create<KingdomDay>();
      element.AtMost = AtMost;
      element.Day = Day;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomEventCanStart"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Event"><see cref="BlueprintKingdomEvent"/></param>
    /// <param name="m_Region"><see cref="BlueprintRegion"/></param>
    [Generated]
    [Implements(typeof(KingdomEventCanStart))]
    public static ConditionsBuilder AddKingdomEventCanStart(
        this ConditionsBuilder builder,
        string m_Event,
        string m_Region,
        bool negate = false)
    {
      
      var element = ElementTool.Create<KingdomEventCanStart>();
      element.m_Event = BlueprintTool.GetRef<BlueprintKingdomEventReference>(m_Event);
      element.m_Region = BlueprintTool.GetRef<BlueprintRegionReference>(m_Region);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomEventIsActive"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Event"><see cref="BlueprintKingdomEvent"/></param>
    [Generated]
    [Implements(typeof(KingdomEventIsActive))]
    public static ConditionsBuilder AddKingdomEventIsActive(
        this ConditionsBuilder builder,
        string m_Event,
        bool negate = false)
    {
      
      var element = ElementTool.Create<KingdomEventIsActive>();
      element.m_Event = BlueprintTool.GetRef<BlueprintKingdomEventReference>(m_Event);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomEventIsBeingResolved"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Event"><see cref="BlueprintKingdomEventBase"/></param>
    [Generated]
    [Implements(typeof(KingdomEventIsBeingResolved))]
    public static ConditionsBuilder AddKingdomEventIsBeingResolved(
        this ConditionsBuilder builder,
        string m_Event,
        bool negate = false)
    {
      
      var element = ElementTool.Create<KingdomEventIsBeingResolved>();
      element.m_Event = BlueprintTool.GetRef<BlueprintKingdomEventBaseReference>(m_Event);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomExists"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomExists))]
    public static ConditionsBuilder AddKingdomExists(
        this ConditionsBuilder builder,
        bool negate = false)
    {
      
      var element = ElementTool.Create<KingdomExists>();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomHasResolvableEvent"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomHasResolvableEvent))]
    public static ConditionsBuilder AddKingdomHasResolvableEvent(
        this ConditionsBuilder builder,
        bool negate = false)
    {
      
      var element = ElementTool.Create<KingdomHasResolvableEvent>();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomHasUpgradeableSettlement"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_SpecificSettlement"><see cref="BlueprintSettlement"/></param>
    [Generated]
    [Implements(typeof(KingdomHasUpgradeableSettlement))]
    public static ConditionsBuilder AddKingdomHasUpgradeableSettlement(
        this ConditionsBuilder builder,
        SettlementState.LevelType Level,
        string m_SpecificSettlement,
        bool negate = false)
    {
      builder.Validate(Level);
      
      var element = ElementTool.Create<KingdomHasUpgradeableSettlement>();
      element.Level = Level;
      element.m_SpecificSettlement = BlueprintTool.GetRef<BlueprintSettlement.Reference>(m_SpecificSettlement);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomIsVisible"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomIsVisible))]
    public static ConditionsBuilder AddKingdomIsVisible(
        this ConditionsBuilder builder,
        bool negate = false)
    {
      
      var element = ElementTool.Create<KingdomIsVisible>();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomLeaderIs"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Unit"><see cref="BlueprintUnit"/></param>
    [Generated]
    [Implements(typeof(KingdomLeaderIs))]
    public static ConditionsBuilder AddKingdomLeaderIs(
        this ConditionsBuilder builder,
        LeaderType Leader,
        string m_Unit,
        Boolean AllowCustomCompanions,
        bool negate = false)
    {
      builder.Validate(Leader);
      builder.Validate(AllowCustomCompanions);
      
      var element = ElementTool.Create<KingdomLeaderIs>();
      element.Leader = Leader;
      element.m_Unit = BlueprintTool.GetRef<BlueprintUnitReference>(m_Unit);
      element.AllowCustomCompanions = AllowCustomCompanions;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomProjectIsAvailable"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Project"><see cref="BlueprintKingdomProject"/></param>
    [Generated]
    [Implements(typeof(KingdomProjectIsAvailable))]
    public static ConditionsBuilder AddKingdomProjectIsAvailable(
        this ConditionsBuilder builder,
        string m_Project,
        Boolean CheckResources,
        Boolean CheckLeader,
        Boolean FinishableThisMonth,
        bool negate = false)
    {
      builder.Validate(CheckResources);
      builder.Validate(CheckLeader);
      builder.Validate(FinishableThisMonth);
      
      var element = ElementTool.Create<KingdomProjectIsAvailable>();
      element.m_Project = BlueprintTool.GetRef<BlueprintKingdomProjectReference>(m_Project);
      element.CheckResources = CheckResources;
      element.CheckLeader = CheckLeader;
      element.FinishableThisMonth = FinishableThisMonth;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomProjectIsDone"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Project"><see cref="BlueprintKingdomProject"/></param>
    [Generated]
    [Implements(typeof(KingdomProjectIsDone))]
    public static ConditionsBuilder AddKingdomProjectIsDone(
        this ConditionsBuilder builder,
        string m_Project,
        bool negate = false)
    {
      
      var element = ElementTool.Create<KingdomProjectIsDone>();
      element.m_Project = BlueprintTool.GetRef<BlueprintKingdomProjectReference>(m_Project);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomRankUpConditions"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomRankUpConditions))]
    public static ConditionsBuilder AddKingdomRankUpConditions(
        this ConditionsBuilder builder,
        KingdomStats.Type Stat,
        Int32 NextRank,
        bool negate = false)
    {
      builder.Validate(Stat);
      builder.Validate(NextRank);
      
      var element = ElementTool.Create<KingdomRankUpConditions>();
      element.Stat = Stat;
      element.NextRank = NextRank;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomRegionIsConquered"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Region"><see cref="BlueprintRegion"/></param>
    [Generated]
    [Implements(typeof(KingdomRegionIsConquered))]
    public static ConditionsBuilder AddKingdomRegionIsConquered(
        this ConditionsBuilder builder,
        string m_Region,
        bool negate = false)
    {
      
      var element = ElementTool.Create<KingdomRegionIsConquered>();
      element.m_Region = BlueprintTool.GetRef<BlueprintRegionReference>(m_Region);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomRegionIsUpgraded"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Region"><see cref="BlueprintRegion"/></param>
    /// <param name="m_Event"><see cref="BlueprintKingdomUpgrade"/></param>
    [Generated]
    [Implements(typeof(KingdomRegionIsUpgraded))]
    public static ConditionsBuilder AddKingdomRegionIsUpgraded(
        this ConditionsBuilder builder,
        string m_Region,
        string m_Event,
        bool negate = false)
    {
      
      var element = ElementTool.Create<KingdomRegionIsUpgraded>();
      element.m_Region = BlueprintTool.GetRef<BlueprintRegionReference>(m_Region);
      element.m_Event = BlueprintTool.GetRef<BlueprintKingdomUpgradeReference>(m_Event);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomSettlementCount"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomSettlementCount))]
    public static ConditionsBuilder AddKingdomSettlementCount(
        this ConditionsBuilder builder,
        SettlementState.LevelType MinLevel,
        Int32 Count,
        bool negate = false)
    {
      builder.Validate(MinLevel);
      builder.Validate(Count);
      
      var element = ElementTool.Create<KingdomSettlementCount>();
      element.MinLevel = MinLevel;
      element.Count = Count;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomSettlementHasBuilding"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Settlement"><see cref="BlueprintSettlement"/></param>
    /// <param name="m_Building"><see cref="BlueprintSettlementBuilding"/></param>
    [Generated]
    [Implements(typeof(KingdomSettlementHasBuilding))]
    public static ConditionsBuilder AddKingdomSettlementHasBuilding(
        this ConditionsBuilder builder,
        string m_Settlement,
        string m_Building,
        bool negate = false)
    {
      
      var element = ElementTool.Create<KingdomSettlementHasBuilding>();
      element.m_Settlement = BlueprintTool.GetRef<BlueprintSettlement.Reference>(m_Settlement);
      element.m_Building = BlueprintTool.GetRef<BlueprintSettlementBuildingReference>(m_Building);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomStatCheck"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomStatCheck))]
    public static ConditionsBuilder AddKingdomStatCheck(
        this ConditionsBuilder builder,
        KingdomStats.Type StatType,
        Int32 Value,
        Boolean AtMost,
        Boolean CheckRank,
        bool negate = false)
    {
      builder.Validate(StatType);
      builder.Validate(Value);
      builder.Validate(AtMost);
      builder.Validate(CheckRank);
      
      var element = ElementTool.Create<KingdomStatCheck>();
      element.StatType = StatType;
      element.Value = Value;
      element.AtMost = AtMost;
      element.CheckRank = CheckRank;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomStatIsMaximum"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomStatIsMaximum))]
    public static ConditionsBuilder AddKingdomStatIsMaximum(
        this ConditionsBuilder builder,
        KingdomStats.Type StatType,
        bool negate = false)
    {
      builder.Validate(StatType);
      
      var element = ElementTool.Create<KingdomStatIsMaximum>();
      element.StatType = StatType;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomTaskResolvedBy"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomTaskResolvedBy))]
    public static ConditionsBuilder AddKingdomTaskResolvedBy(
        this ConditionsBuilder builder,
        LeaderType[] Leaders,
        bool negate = false)
    {
      foreach (var item in Leaders)
      {
        builder.Validate(item);
      }
      
      var element = ElementTool.Create<KingdomTaskResolvedBy>();
      element.Leaders = Leaders;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomUnrestCheck"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomUnrestCheck))]
    public static ConditionsBuilder AddKingdomUnrestCheck(
        this ConditionsBuilder builder,
        KingdomStatusType Value,
        Boolean AtMost,
        bool negate = false)
    {
      builder.Validate(Value);
      builder.Validate(AtMost);
      
      var element = ElementTool.Create<KingdomUnrestCheck>();
      element.Value = Value;
      element.AtMost = AtMost;
      element.Not = negate;
      return builder.Add(element);
    }
  }
}
