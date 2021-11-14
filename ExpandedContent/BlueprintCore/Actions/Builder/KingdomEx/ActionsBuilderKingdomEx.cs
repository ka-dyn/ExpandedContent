using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints;
using BlueprintCore.Utils;
using Kingmaker.Armies;
using Kingmaker.Armies.Blueprints;
using Kingmaker.Armies.Components;
using Kingmaker.Armies.TacticalCombat.Components;
using Kingmaker.Armies.TacticalCombat.GameActions;
using Kingmaker.Armies.TacticalCombat.Grid;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Loot;
using Kingmaker.Crusade.GlobalMagic;
using Kingmaker.Crusade.GlobalMagic.Actions;
using Kingmaker.Crusade.GlobalMagic.Actions.DamageLogic;
using Kingmaker.Crusade.GlobalMagic.Actions.SummonLogics;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.Enums;
using Kingmaker.Globalmap.Blueprints;
using Kingmaker.Globalmap.State;
using Kingmaker.Kingdom;
using Kingmaker.Kingdom.Actions;
using Kingmaker.Kingdom.Armies;
using Kingmaker.Kingdom.Armies.Actions;
using Kingmaker.Kingdom.Blueprints;
using Kingmaker.Kingdom.Flags;
using Kingmaker.Kingdom.Settlements;
using Kingmaker.Localization;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueprintCore.Actions.Builder.KingdomEx
{
  /// <summary>
  /// Extension to <see cref="ActionsBuilder"/> for actions involving the Kingdom and Crusade
  /// system.
  /// </summary>
  /// <inheritdoc cref="ActionsBuilder"/>
  public static class ActionsBuilderKingdomEx
  {
    //----- Kingmaker.Armies.TacticalCombat.GameActions -----//

    /// <summary>
    /// Adds <see cref="ArmyAdditionalAction"/>
    /// </summary>
    [Implements(typeof(ArmyAdditionalAction))]
    public static ActionsBuilder GrantExtraArmyAction(
        this ActionsBuilder builder,
        bool usableInCurrentTurn = true,
        bool usableInBonusMoraleTurn = true)
    {
      var grantAction = ElementTool.Create<ArmyAdditionalAction>();
      grantAction.m_InCurrentTurn = usableInCurrentTurn;
      grantAction.m_CanAddInBonusMoraleTurn = usableInBonusMoraleTurn;
      return builder.Add(grantAction);
    }

    /// <summary>
    /// Adds <see cref="ContextActionAddCrusadeResource"/>
    /// </summary>
    [Implements(typeof(ContextActionAddCrusadeResource))]
    public static ActionsBuilder AddCrusadeResource(
        this ActionsBuilder builder, KingdomResourcesAmount amount)
    {
      var addResource = ElementTool.Create<ContextActionAddCrusadeResource>();
      addResource.m_ResourcesAmount = amount;
      return builder.Add(addResource);
    }

    /// <summary>
    /// Adds <see cref="ContextActionArmyRemoveFacts"/>
    /// </summary>
    /// 
    /// <param name="facts"><see cref="Kingmaker.Blueprints.Facts.BlueprintUnitFact">BlueprintUnitFact</see></param>
    [Implements(typeof(ContextActionArmyRemoveFacts))]
    public static ActionsBuilder RemoveArmyFacts(
        this ActionsBuilder builder, params string[] facts)
    {
      var removeFacts = ElementTool.Create<ContextActionArmyRemoveFacts>();
      removeFacts.m_FactsToRemove =
          facts.Select(fact => BlueprintTool.GetRef<BlueprintUnitFactReference>(fact)).ToArray();
      return builder.Add(removeFacts);
    }

    /// <summary>
    /// Adds <see cref="ContextActionRestoreLeaderAction"/>
    /// </summary>
    [Implements(typeof(ContextActionRestoreLeaderAction))]
    public static ActionsBuilder RestoreLeaderAction(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionRestoreLeaderAction>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionStopUnit"/>
    /// </summary>
    [Implements(typeof(ContextActionStopUnit))]
    public static ActionsBuilder StopUnit(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionStopUnit>());
    }

    //----- Kingmaker.Crusade.GlobalMagic.Actions -----//

    /// <summary>
    /// Adds <see cref="AddBuffToSquad"/>
    /// </summary>
    /// 
    /// <param name="buff"><see cref="Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff">BlueprintBuff</see></param>
    [Implements(typeof(AddBuffToSquad))]
    public static ActionsBuilder BuffSquad(
        this ActionsBuilder builder,
        string buff,
        GlobalMagicValue durationHours,
        SquadFilter filter)
    {
      var buffSquad = ElementTool.Create<AddBuffToSquad>();
      buffSquad.m_Buff = BlueprintTool.GetRef<BlueprintBuffReference>(buff);
      buffSquad.m_HoursDuration = durationHours;
      buffSquad.m_Filter = filter;
      return builder.Add(buffSquad);
    }

    /// <summary>
    /// Adds <see cref="ChangeArmyMorale"/>
    /// </summary>
    [Implements(typeof(ChangeArmyMorale))]
    public static ActionsBuilder ChangeArmyMorale(
          this ActionsBuilder builder, GlobalMagicValue duration, GlobalMagicValue change)
    {
      var changeMorale = ElementTool.Create<ChangeArmyMorale>();
      changeMorale.m_Duration = duration;
      changeMorale.m_ChangeValue = change;
      return builder.Add(changeMorale);
    }

    /// <summary>
    /// Adds <see cref="FakeSkipTime"/>
    /// </summary>
    [Implements(typeof(FakeSkipTime))]
    public static ActionsBuilder FakeSkipTime(this ActionsBuilder builder, GlobalMagicValue days)
    {
      var skipTime = ElementTool.Create<FakeSkipTime>();
      skipTime.m_SkipDays = days;
      return builder.Add(skipTime);
    }

    /// <summary>
    /// Adds <see cref="GainDiceArmyDamage"/>
    /// </summary>
    [Implements(typeof(GainDiceArmyDamage))]
    public static ActionsBuilder GainArmyDamage(
        this ActionsBuilder builder, SquadFilter filter, GlobalMagicValue dmgDice)
    {
      var gainDmg = ElementTool.Create<GainDiceArmyDamage>();
      gainDmg.m_Filter = filter;
      gainDmg.m_DiceValue = dmgDice;
      return builder.Add(gainDmg);
    }

    /// <summary>
    /// Adds <see cref="RemoveUnitsByExp"/>
    /// </summary>
    [Implements(typeof(RemoveUnitsByExp))]
    public static ActionsBuilder RemoveUnitsByExp(
        this ActionsBuilder builder, SquadFilter filter, GlobalMagicValue exp)
    {
      var removeUnits = ElementTool.Create<RemoveUnitsByExp>();
      removeUnits.m_Filter = filter;
      removeUnits.m_ExpValue = exp;
      return builder.Add(removeUnits);
    }

    /// <summary>
    /// Adds <see cref="GainGlobalMagicSpell"/>
    /// </summary>
    /// 
    /// <param name="spell"><see cref="BlueprintGlobalMagicSpell"/></param>
    [Implements(typeof(GainGlobalMagicSpell))]
    public static ActionsBuilder GainGlobalSpell(this ActionsBuilder builder, string spell)
    {
      var gainSpell = ElementTool.Create<GainGlobalMagicSpell>();
      gainSpell.m_Spell = BlueprintTool.GetRef<BlueprintGlobalMagicSpell.Reference>(spell);
      return builder.Add(gainSpell);
    }

    /// <summary>
    /// Adds <see cref="ManuallySetGlobalSpellCooldown"/>
    /// </summary>
    /// 
    /// <param name="spell"><see cref="BlueprintGlobalMagicSpell"/></param>
    [Implements(typeof(ManuallySetGlobalSpellCooldown))]
    public static ActionsBuilder PutGlobalSpellOnCooldown(this ActionsBuilder builder, string spell)
    {
      var activateCooldown = ElementTool.Create<ManuallySetGlobalSpellCooldown>();
      activateCooldown.m_Spell = BlueprintTool.GetRef<BlueprintGlobalMagicSpell.Reference>(spell);
      return builder.Add(activateCooldown);
    }

    /// <summary>
    /// Adds <see cref="OpenTeleportationInterface"/>
    /// </summary>
    [Implements(typeof(OpenTeleportationInterface))]
    public static ActionsBuilder GlobalTeleport(this ActionsBuilder builder, ActionsBuilder onTeleport)
    {
      var teleport = ElementTool.Create<OpenTeleportationInterface>();
      teleport.m_OnTeleportActions = onTeleport.Build();
      return builder.Add(teleport);
    }

    /// <summary>
    /// Adds <see cref="RemoveGlobalMagicSpell"/>
    /// </summary>
    /// 
    /// <param name="spell"><see cref="BlueprintGlobalMagicSpell"/></param>
    [Implements(typeof(RemoveGlobalMagicSpell))]
    public static ActionsBuilder RemoveGlobalSpell(this ActionsBuilder builder, string spell)
    {
      var removespell = ElementTool.Create<RemoveGlobalMagicSpell>();
      removespell.m_Spell = BlueprintTool.GetRef<BlueprintGlobalMagicSpell.Reference>(spell);
      return builder.Add(removespell);
    }

    /// <summary>
    /// Adds <see cref="RepairLeaderMana"/>
    /// </summary>
    [Implements(typeof(RepairLeaderMana))]
    public static ActionsBuilder RestoreLeaderMana(this ActionsBuilder builder, GlobalMagicValue value)
    {
      var restoreMana = ElementTool.Create<RepairLeaderMana>();
      restoreMana.m_Value = value;
      return builder.Add(restoreMana);
    }

    /// <summary>
    /// Adds <see cref="SummonExistUnits"/>
    /// </summary>
    [Implements(typeof(SummonExistUnits))]
    public static ActionsBuilder SummonMoreUnits(
        this ActionsBuilder builder, GlobalMagicValue expCost)
    {
      var summon = ElementTool.Create<SummonExistUnits>();
      summon.m_SumExpCost = expCost;
      return builder.Add(summon);
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.Crusade.GlobalMagic.Actions.SummonLogics.SummonRandomGroup">SummonRandomGroup</see>
    /// </summary>
    [Implements(typeof(SummonRandomGroup))]
    public static ActionsBuilder SummonRandomGroup(
        this ActionsBuilder builder,
        params SummonRandomGroup.RandomGroup[] groups)
    {
      var summon = ElementTool.Create<SummonRandomGroup>();
      summon.m_RandomGroups = groups;
      return builder.Add(summon);
    }

    /// <summary>
    /// Adds <see cref="TeleportArmyAction"/>
    /// </summary>
    [Implements(typeof(TeleportArmyAction))]
    public static ActionsBuilder TeleportArmy(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<TeleportArmyAction>());
    }

    //----- Kingmaker.Designers.EventConditionActionSystem.Actions -----//

    /// <summary>
    /// Adds <see cref="Kingmaker.Designers.EventConditionActionSystem.Actions.CreateArmy">CreateArmy</see>
    /// </summary>
    /// 
    /// <param name="army"><see cref="BlueprintArmyPreset"/></param>
    /// <param name="location"><see cref="BlueprintGlobalMapPoint"/></param>
    /// <param name="leader"><see cref="BlueprintArmyLeader"/></param>
    [Implements(typeof(CreateArmy))]
    public static ActionsBuilder CreateCrusaderArmy(
        this ActionsBuilder builder,
        string army,
        string location,
        string leader = null,
        int? movePoints = null,
        float? speed = null,
        bool? applyRecruitIncrease = null)
    {
      return builder.Add(
          CreateArmy(
              ArmyFaction.Crusaders,
              army,
              location,
              leader,
              movePoints: movePoints,
              speed: speed,
              applyRecruitIncrease: applyRecruitIncrease));
    }

    /// <inheritdoc cref="CreateCrusaderArmy"/>
    [Implements(typeof(CreateArmy))]
    public static ActionsBuilder CreateCrusaderArmyFromLosses(
        this ActionsBuilder builder,
        string location,
        int sumExperience,
        int maxSquads,
        bool applyRecruitIncrease = false)
    {
      var createArmy = ElementTool.Create<CreateArmyFromLosses>();
      createArmy.m_Location = BlueprintTool.GetRef<BlueprintGlobalMapPoint.Reference>(location);
      createArmy.m_SumExperience = sumExperience;
      createArmy.m_SquadsMaxCount = maxSquads;
      createArmy.m_ApplyRecruitIncrease = applyRecruitIncrease;
      return builder.Add(createArmy);
    }

    /// <inheritdoc cref="CreateCrusaderArmy"/>
    [Implements(typeof(CreateArmy))]
    public static ActionsBuilder CreateDemonArmy(
        this ActionsBuilder builder,
        string army,
        string location,
        string leader = null,
        bool targetNearestEnemy = false,
        float? speed = null)
    {
      return builder.Add(
          CreateArmy(
              ArmyFaction.Demons,
              army,
              location,
              leader,
              target: targetNearestEnemy ? TravelLogicType.NearestEnemy : TravelLogicType.None,
              speed: speed));
    }


    /// <inheritdoc cref="CreateCrusaderArmy"/>
    /// 
    /// <param name="targetLocation"><see cref="BlueprintGlobalMapPoint"/></param>
    /// <param name="onTargetReached"><see cref="BlueprintActionList"/></param>
    [Implements(typeof(CreateArmy))]
    public static ActionsBuilder CreateDemonArmyTargetingLocation(
        this ActionsBuilder builder,
        string army,
        string location,
        string targetLocation,
        string onTargetReached = null,
        string leader = null,
        int? daysToTarget = null)
    {
      return builder.Add(
          CreateArmy(
              ArmyFaction.Demons,
              army,
              location,
              leader,
              targetLocation: targetLocation,
              onTargetReached: onTargetReached,
              daysToTarget: daysToTarget,
              target: TravelLogicType.Location));
    }

    private static CreateArmy CreateArmy(
        ArmyFaction faction,
        string army,
        string location,
        string leader,
        string targetLocation = null,
        string onTargetReached = null,
        int? daysToTarget = null,
        int? movePoints = null,
        float? speed = null,
        bool? applyRecruitIncrease = null,
        TravelLogicType target = TravelLogicType.None)
    {
      var createArmy = ElementTool.Create<CreateArmy>();
      createArmy.Faction = faction;
      createArmy.Preset = BlueprintTool.GetRef<BlueprintArmyPreset.Reference>(army);
      createArmy.Location = BlueprintTool.GetRef<BlueprintGlobalMapPoint.Reference>(location);
      createArmy.m_MoveTarget = target;

      createArmy.m_DaysToDestination = daysToTarget ?? 7;
      createArmy.MovementPoints = movePoints ?? 60;
      createArmy.m_ArmySpeed = speed ?? 1f;
      createArmy.m_ApplyRecruitIncrease = applyRecruitIncrease ?? false;

      if (leader == null)
      {
        createArmy.ArmyLeader = BlueprintReferenceBase.CreateTyped<ArmyLeader.Reference>(null);
      }
      else
      {
        createArmy.ArmyLeader = BlueprintTool.GetRef<ArmyLeader.Reference>(leader);
        createArmy.WithLeader = true;
      }
      createArmy.m_TargetLocation =
          BlueprintTool.GetRef<BlueprintGlobalMapPoint.Reference>(targetLocation);
      createArmy.m_CompleteActions =
          BlueprintTool.GetRef<BlueprintActionList.Reference>(onTargetReached);
      return createArmy;
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.Designers.EventConditionActionSystem.Actions.CreateGarrison">CreateGarrison</see>
    /// </summary>
    /// 
    /// <param name="army"><see cref="BlueprintArmyPreset"/></param>
    /// <param name="location"><see cref="BlueprintGlobalMapPoint"/></param>
    /// <param name="leader"><see cref="BlueprintArmyLeader"/></param>
    [Implements(typeof(CreateGarrison))]
    public static ActionsBuilder CreateGarrison(
        this ActionsBuilder builder,
        string army,
        string location,
        string leader = null,
        bool noReward = true)
    {
      var createGarrison = ElementTool.Create<CreateGarrison>();
      createGarrison.Preset = BlueprintTool.GetRef<BlueprintArmyPreset.Reference>(army);
      createGarrison.Location = BlueprintTool.GetRef<BlueprintGlobalMapPoint.Reference>(location);
      createGarrison.HasNoReward = noReward;

      if (leader == null)
      {
        createGarrison.ArmyLeader = BlueprintReferenceBase.CreateTyped<ArmyLeader.Reference>(null);
      }
      else
      {
        createGarrison.WithLeader = true;
        createGarrison.ArmyLeader = BlueprintTool.GetRef<ArmyLeader.Reference>(leader);
      }
      return builder.Add(createGarrison);
    }

    //----- Kingmaker.Kingdom.Actions -----//

    /// <summary>
    /// Adds <see cref="AddMorale"/>
    /// </summary>
    [Implements(typeof(AddMorale))]
    public static ActionsBuilder IncreaseMorale(this ActionsBuilder builder, DiceFormula value, int bonus)
    {
      var increaseMorale = ElementTool.Create<AddMorale>();
      increaseMorale.Change = value;
      increaseMorale.Bonus = bonus;
      return builder.Add(increaseMorale);
    }

    /// <inheritdoc cref="IncreaseMorale"/>
    [Implements(typeof(AddMorale))]
    public static ActionsBuilder ReduceMorale(this ActionsBuilder builder, DiceFormula value, int bonus)
    {
      var reduceMorale = ElementTool.Create<AddMorale>();
      reduceMorale.Change = value;
      reduceMorale.Bonus = bonus;
      reduceMorale.Substract = true;
      return builder.Add(reduceMorale);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionActivateEventDeck"/>
    /// </summary>
    /// 
    /// <param name="deck"><see cref="BlueprintKingdomDeck"/></param>
    [Implements(typeof(KingdomActionActivateEventDeck))]
    public static ActionsBuilder ActivateEventDeck(this ActionsBuilder builder, string deck)
    {
      var activateDeck = ElementTool.Create<KingdomActionActivateEventDeck>();
      activateDeck.m_Deck = BlueprintTool.GetRef<BlueprintKingdomDeckReference>(deck);
      return builder.Add(activateDeck);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionAddBPRandom"/>
    /// </summary>
    [Implements(typeof(KingdomActionAddBPRandom))]
    public static ActionsBuilder AddBP(
        this ActionsBuilder builder,
        KingdomResource type,
        DiceFormula value,
        int bonus,
        bool showInEventHistory = true)
    {
      var addBP = ElementTool.Create<KingdomActionAddBPRandom>();
      addBP.ResourceType = type;
      addBP.Change = value;
      addBP.Bonus = bonus;
      addBP.IncludeInEventStats = showInEventHistory;
      return builder.Add(addBP);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionAddBuff"/>
    /// </summary>
    /// 
    /// <param name="buff"><see cref="BlueprintKingdomBuff"/></param>
    /// <param name="targetRegion"><see cref="BlueprintRegion"/></param>
    [Implements(typeof(KingdomActionAddBuff))]
    public static ActionsBuilder AddKingdomBuff(
        this ActionsBuilder builder,
        string buff,
        int durationOverrideDays = 0,
        string targetRegion = null,
        bool applyToRegion = true,
        bool applyToAdjacentRegions = false)
    {
      var addBuff = ElementTool.Create<KingdomActionAddBuff>();
      addBuff.m_Blueprint = BlueprintTool.GetRef<BlueprintKingdomBuffReference>(buff);
      addBuff.OverrideDuration = durationOverrideDays;
      addBuff.m_Region = BlueprintTool.GetRef<BlueprintRegionReference>(targetRegion);
      addBuff.ApplyToRegion = applyToRegion;
      addBuff.CopyToAdjacentRegions = applyToAdjacentRegions;
      return builder.Add(addBuff);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionAddFreeBuilding"/>
    /// </summary>
    /// 
    /// <param name="building"><see cref="Kingmaker.Kingdom.Settlements.BlueprintSettlementBuilding">BlueprintSettlementBuilding</see></param>
    /// <param name="settlement"><see cref="BlueprintSettlement"/></param>
    [Implements(typeof(KingdomActionAddFreeBuilding))]
    public static ActionsBuilder AddFreeBuilding(
        this ActionsBuilder builder, string building, int count = 1, string settlement = null)
    {
      var addBuilding = ElementTool.Create<KingdomActionAddFreeBuilding>();
      addBuilding.m_Building = BlueprintTool.GetRef<BlueprintSettlementBuildingReference>(building);
      addBuilding.Count = count;
      if (settlement == null)
      {
        addBuilding.Anywhere = true;
      }
      else
      {
        addBuilding.Anywhere = false;
        addBuilding.m_Settlement = BlueprintTool.GetRef<BlueprintSettlement.Reference>(settlement);
      }
      return builder.Add(addBuilding);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionAddMercenaryReroll"/>
    /// </summary>
    [Implements(typeof(KingdomActionAddMercenaryReroll))]
    public static ActionsBuilder AddFreeMercRerolls(this ActionsBuilder builder, int count = 1)
    {
      var addMercRerolls = ElementTool.Create<KingdomActionAddMercenaryReroll>();
      addMercRerolls.m_FreeRerollsToAdd = count;
      return builder.Add(addMercRerolls);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionAddRandomBuff"/>
    /// </summary>
    /// 
    /// <param name="buffs"><see cref="BlueprintKingdomBuff"/></param>
    [Implements(typeof(KingdomActionAddRandomBuff))]
    public static ActionsBuilder AddRandomKingdomBuff(
        this ActionsBuilder builder, string[] buffs, int durationOverrideDays = 0)
    {
      var addBuff = ElementTool.Create<KingdomActionAddRandomBuff>();
      addBuff.m_Buffs =
          buffs.Select(buff => BlueprintTool.GetRef<BlueprintKingdomBuffReference>(buff)).ToList();
      addBuff.OverrideDurationDays = durationOverrideDays;
      return builder.Add(addBuff);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionArtisanRequestHelp"/>
    /// </summary>
    /// 
    /// <param name="artisan"><see cref="Kingmaker.Kingdom.Artisans.BlueprintKingdomArtisan">BlueprintKingdomArtisan</see></param>
    /// <param name="project"><see cref="BlueprintKingdomProject"/></param>
    [Implements(typeof(KingdomActionArtisanRequestHelp))]
    public static ActionsBuilder ArtisanRequestHelp(this ActionsBuilder builder, string artisan, string project)
    {
      var requestHelp = ElementTool.Create<KingdomActionArtisanRequestHelp>();
      requestHelp.m_Artisan = BlueprintTool.GetRef<BlueprintKingdomArtisanReference>(artisan);
      requestHelp.m_Project = BlueprintTool.GetRef<BlueprintKingdomProjectReference>(project);
      return builder.Add(requestHelp);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionChangeToAutoCrusade"/>
    /// </summary>
    [Implements(typeof(KingdomActionChangeToAutoCrusade))]
    public static ActionsBuilder EnableAutoCrusade(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<KingdomActionChangeToAutoCrusade>());
    }

    /// <summary>
    /// Adds <see cref="KingdomActionCollectLoot"/>
    /// </summary>
    [Implements(typeof(KingdomActionCollectLoot))]
    public static ActionsBuilder CollectKingdomLoot(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<KingdomActionCollectLoot>());
    }

    /// <summary>
    /// Adds <see cref="KingdomActionConquerRegion"/>
    /// </summary>
    /// 
    /// <param name="region"><see cref="BlueprintRegion"/></param>
    [Implements(typeof(KingdomActionConquerRegion))]
    public static ActionsBuilder ConquerRegion(this ActionsBuilder builder, string region)
    {
      var conquer = ElementTool.Create<KingdomActionConquerRegion>();
      conquer.m_Region = BlueprintTool.GetRef<BlueprintRegionReference>(region);
      return builder.Add(conquer);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionDestroyAllSettlements"/>
    /// </summary>
    [Implements(typeof(KingdomActionDestroyAllSettlements))]
    public static ActionsBuilder DestroyAllSettlements(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<KingdomActionDestroyAllSettlements>());
    }

    /// <summary>
    /// Adds <see cref="KingdomActionDisable"/>
    /// </summary>
    [Implements(typeof(KingdomActionDisable))]
    public static ActionsBuilder DisableKingdom(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<KingdomActionDisable>());
    }

    /// <summary>
    /// Adds <see cref="KingdomActionEnable"/>
    /// </summary>
    [Implements(typeof(KingdomActionEnable))]
    public static ActionsBuilder EnableKingdom(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<KingdomActionEnable>());
    }

    /// <summary>
    /// Adds <see cref="KingdomActionFillSettlement"/>
    /// </summary>
    /// 
    /// <param name="settlement"><see cref="BlueprintSettlement"/></param>
    /// <param name="buildList"><see cref="Kingmaker.Kingdom.AI.SettlementBuildList">SettlementBuildList</see></param>
    [Implements(typeof(KingdomActionFillSettlement))]
    public static ActionsBuilder FillSettlement(
        this ActionsBuilder builder, string settlement, string buildList)
    {
      var fill = ElementTool.Create<KingdomActionFillSettlement>();
      fill.m_SpecificSettlement = BlueprintTool.GetRef<BlueprintSettlement.Reference>(settlement);
      fill.m_BuildList = BlueprintTool.GetRef<SettlementBuildListReference>(buildList);
      return builder.Add(fill);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionFillSettlementByLocation"/>
    /// </summary>
    /// 
    /// <param name="location"><see cref="BlueprintGlobalMapPoint"/></param>
    /// <param name="buildList"><see cref="Kingmaker.Kingdom.AI.SettlementBuildList">SettlementBuildList</see></param>
    [Implements(typeof(KingdomActionFillSettlementByLocation))]
    public static ActionsBuilder FillSettlementByLocation(
        this ActionsBuilder builder, string location, string buildList)
    {
      var fill = ElementTool.Create<KingdomActionFillSettlementByLocation>();
      fill.m_SpecificSettlementLocation =
          BlueprintTool.GetRef<BlueprintGlobalMapPointReference>(location);
      fill.m_BuildList = BlueprintTool.GetRef<SettlementBuildListReference>(buildList);
      return builder.Add(fill);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionFinishRandomBuilding"/>
    /// </summary>
    [Implements(typeof(KingdomActionFinishRandomBuilding))]
    public static ActionsBuilder FinishRandomBuilding(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<KingdomActionFinishRandomBuilding>());
    }

    /// <summary>
    /// Adds <see cref="KingdomActionFoundKingdom"/>
    /// </summary>
    [Implements(typeof(KingdomActionFoundKingdom))]
    public static ActionsBuilder FoundKingdom(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<KingdomActionFoundKingdom>());
    }

    /// <summary>
    /// Adds <see cref="KingdomActionFoundSettlement"/>
    /// </summary>
    /// 
    /// <param name="location"><see cref="BlueprintGlobalMapPoint"/></param>
    /// <param name="settlement"><see cref="BlueprintSettlement"/></param>
    [Implements(typeof(KingdomActionFoundSettlement))]
    public static ActionsBuilder FoundSettlement(
        this ActionsBuilder builder, string location, string settlement)
    {
      var found = ElementTool.Create<KingdomActionFoundSettlement>();
      found.m_Location = BlueprintTool.GetRef<BlueprintGlobalMapPoint.Reference>(location);
      found.m_Settlement = BlueprintTool.GetRef<BlueprintSettlement.Reference>(settlement);
      return builder.Add(found);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionGainLeaderExperience"/>
    /// </summary>
    [Implements(typeof(KingdomActionGainLeaderExperience))]
    public static ActionsBuilder GrantLeaderExp(
        this ActionsBuilder builder, IntEvaluator exp, float? leaderLevelMultiplier = null)
    {
      builder.Validate(exp);

      var grantExp = ElementTool.Create<KingdomActionGainLeaderExperience>();
      grantExp.m_Value = exp;
      if (leaderLevelMultiplier != null)
      {
        grantExp.m_MultiplyByLeaderLevel = true;
        grantExp.m_MultiplierCoefficient = leaderLevelMultiplier.Value;
      }
      return builder.Add(grantExp);
    }

    //----- Kingmaker.Kingdom.Blueprints -----//

    /// <summary>
    /// Adds <see cref="Kingmaker.Kingdom.Blueprints.AddCrusadeResources">AddCrusadeResources</see>
    /// </summary>
    [Implements(typeof(AddCrusadeResources))]
    public static ActionsBuilder AddCrusadeResources(
        this ActionsBuilder builder, KingdomResourcesAmount resources)
    {
      var addResources = ElementTool.Create<AddCrusadeResources>();
      addResources._resourcesAmount = resources;
      return builder.Add(addResources);
    }

    //----- Kingmaker.UnitLogic.Mechanics.Actions -----//

    /// <summary>
    /// Adds <see cref="Kingmaker.UnitLogic.Mechanics.Actions.ChangeTacticalMorale">ChangeTacticalMorale</see>
    /// </summary>
    [Implements(typeof(ChangeTacticalMorale))]
    public static ActionsBuilder ChangeTacticalMorale(
        this ActionsBuilder builder, ContextValue value)
    {
      var changeMorale = ElementTool.Create<ChangeTacticalMorale>();
      changeMorale.m_Value = value;
      return builder.Add(changeMorale);
    }

    /// <summary>
    /// Adds <see cref="ContextActionSquadUnitsKill"/>
    /// </summary>
    /// 
    /// <remarks>
    /// Use this to kill non-leader units from the caster's squad. Use <see cref="KillSquadLeaders"/> for leaders units.
    /// </remarks>
    /// 
    /// <param name="percent">Percentage of squad units to kill as a float between 0.0 and 1.0.</param>
    [Implements(typeof(ContextActionSquadUnitsKill))]
    public static ActionsBuilder KillSquadUnits(this ActionsBuilder builder, float percent)
    {
      var kill = ElementTool.Create<ContextActionSquadUnitsKill>();
      kill.m_UseFloatValue = true;
      kill.m_FloatCount = percent;
      return builder.Add(kill);
    }

    /// <summary>
    /// Adds <see cref="ContextActionSquadUnitsKill"/>
    /// </summary>
    /// 
    /// <remarks>
    /// Use this to kill leader units from the caster's squad. Use <see cref="KillSquadUnits"/> for regular units.
    /// </remarks>
    [Implements(typeof(ContextActionSquadUnitsKill))]
    public static ActionsBuilder KillSquadLeaders(
        this ActionsBuilder builder, ContextDiceValue count)
    {
      var kill = ElementTool.Create<ContextActionSquadUnitsKill>();
      kill.m_Count = count;
      return builder.Add(kill);
    }

    /// <summary>
    /// Adds <see cref="ContextActionSummonTacticalSquad"/>
    /// </summary>
    /// 
    /// <param name="unit"><see cref="BlueprintUnit"/></param>
    /// <param name="summonPool"><see cref="BlueprintSummonPool"/></param>
    [Implements(typeof(ContextActionSummonTacticalSquad))]
    public static ActionsBuilder SummonSquad(
        this ActionsBuilder builder,
        string unit,
        ContextValue count,
        ActionsBuilder onSpawn = null,
        string summonPool = null)
    {
      var summon = ElementTool.Create<ContextActionSummonTacticalSquad>();
      summon.m_Blueprint = BlueprintTool.GetRef<BlueprintUnitReference>(unit);
      summon.m_Count = count;
      summon.m_AfterSpawn = onSpawn?.Build() ?? Constants.Empty.Actions;
      summon.m_SummonPool =
          summonPool is null
              ? null
              : BlueprintTool.GetRef<BlueprintSummonPoolReference>(summonPool);
      return builder.Add(summon);
    }

    /// <summary>
    /// Adds <see cref="ContextActionTacticalCombatDealDamage"/>
    /// </summary>
    [Implements(typeof(ContextActionTacticalCombatDealDamage))]
    public static ActionsBuilder TacticalCombatDealDamage(
        this ActionsBuilder builder,
        DamageTypeDescription type,
        DiceType diceType,
        ContextValue diceRolls = null,
        bool dealHalf = false,
        bool ignoreCrit = false,
        int? minHPAfterDmg = null)
    {
      var dmg = ElementTool.Create<ContextActionTacticalCombatDealDamage>();
      dmg.DamageType = type;
      dmg.DiceType = diceType;
      dmg.RollsCount = diceRolls ?? dmg.RollsCount;
      dmg.Half = dealHalf;
      dmg.IgnoreCritical = ignoreCrit;

      if (minHPAfterDmg != null)
      {
        dmg.UseMinHPAfterDamage = true;
        dmg.MinHPAfterDamage = minHPAfterDmg.Value;
      }
      return builder.Add(dmg);
    }

    /// <summary>
    /// Adds <see cref="ContextActionTacticalCombatHealTarget"/>
    /// </summary>
    [Implements(typeof(ContextActionTacticalCombatHealTarget))]
    public static ActionsBuilder TacticalCombatHeal(
        this ActionsBuilder builder,
        DiceType diceType = DiceType.D6,
        ContextValue diceRolls = null)
    {
      var heal = ElementTool.Create<ContextActionTacticalCombatHealTarget>();
      heal.DiceType = diceType;
      heal.RollsCount = diceRolls ?? heal.RollsCount;
      return builder.Add(heal);
    }

    //----- Auto Generated -----//



    /// <summary>
    /// Adds <see cref="KingdomIncreaseIncome"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomIncreaseIncome))]
    public static ActionsBuilder AddKingdomIncreaseIncome(
        this ActionsBuilder builder,
        Int32 Bonus,
        KingdomResource ResourceType)
    {
      builder.Validate(Bonus);
      builder.Validate(ResourceType);
      
      var element = ElementTool.Create<KingdomIncreaseIncome>();
      element.Bonus = Bonus;
      element.ResourceType = ResourceType;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ChangeKingdomMoraleMaximum"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ChangeKingdomMoraleMaximum))]
    public static ActionsBuilder AddChangeKingdomMoraleMaximum(
        this ActionsBuilder builder,
        Int32 m_MaxValueDelta)
    {
      builder.Validate(m_MaxValueDelta);
      
      var element = ElementTool.Create<ChangeKingdomMoraleMaximum>();
      element.m_MaxValueDelta = m_MaxValueDelta;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomAddMoraleFlags"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_NewFlags"><see cref="BlueprintKingdomMoraleFlag"/></param>
    [Generated]
    [Implements(typeof(KingdomAddMoraleFlags))]
    public static ActionsBuilder AddKingdomAddMoraleFlags(
        this ActionsBuilder builder,
        string[] m_NewFlags)
    {
      
      var element = ElementTool.Create<KingdomAddMoraleFlags>();
      element.m_NewFlags = m_NewFlags.Select(bp => BlueprintTool.GetRef<BlueprintKingdomMoraleFlag.Reference>(bp)).ToArray();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomFlagIncrement"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_TargetFlag"><see cref="BlueprintKingdomMoraleFlag"/></param>
    [Generated]
    [Implements(typeof(KingdomFlagIncrement))]
    public static ActionsBuilder AddKingdomFlagIncrement(
        this ActionsBuilder builder,
        string m_TargetFlag,
        Int32 m_Increment)
    {
      builder.Validate(m_Increment);
      
      var element = ElementTool.Create<KingdomFlagIncrement>();
      element.m_TargetFlag = BlueprintTool.GetRef<BlueprintKingdomMoraleFlag.Reference>(m_TargetFlag);
      element.m_Increment = m_Increment;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomMoraleFlagUpdateIncome"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_TargetFlag"><see cref="BlueprintKingdomMoraleFlag"/></param>
    [Generated]
    [Implements(typeof(KingdomMoraleFlagUpdateIncome))]
    public static ActionsBuilder AddKingdomMoraleFlagUpdateIncome(
        this ActionsBuilder builder,
        string m_TargetFlag)
    {
      
      var element = ElementTool.Create<KingdomMoraleFlagUpdateIncome>();
      element.m_TargetFlag = BlueprintTool.GetRef<BlueprintKingdomMoraleFlag.Reference>(m_TargetFlag);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomMoraleUpdateIncome"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomMoraleUpdateIncome))]
    public static ActionsBuilder AddKingdomMoraleUpdateIncome(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<KingdomMoraleUpdateIncome>());
    }

    /// <summary>
    /// Adds <see cref="KingdomRemoveMoraleFlags"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_FlagsToRemove"><see cref="BlueprintKingdomMoraleFlag"/></param>
    [Generated]
    [Implements(typeof(KingdomRemoveMoraleFlags))]
    public static ActionsBuilder AddKingdomRemoveMoraleFlags(
        this ActionsBuilder builder,
        string[] m_FlagsToRemove)
    {
      
      var element = ElementTool.Create<KingdomRemoveMoraleFlags>();
      element.m_FlagsToRemove = m_FlagsToRemove.Select(bp => BlueprintTool.GetRef<BlueprintKingdomMoraleFlag.Reference>(bp)).ToArray();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomSetFlagState"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_TargetFlag"><see cref="BlueprintKingdomMoraleFlag"/></param>
    [Generated]
    [Implements(typeof(KingdomSetFlagState))]
    public static ActionsBuilder AddKingdomSetFlagState(
        this ActionsBuilder builder,
        string m_TargetFlag,
        KingdomMoraleFlag.State m_State,
        Int32 m_MaxDays)
    {
      builder.Validate(m_State);
      builder.Validate(m_MaxDays);
      
      var element = ElementTool.Create<KingdomSetFlagState>();
      element.m_TargetFlag = BlueprintTool.GetRef<BlueprintKingdomMoraleFlag.Reference>(m_TargetFlag);
      element.m_State = m_State;
      element.m_MaxDays = m_MaxDays;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ReduceNegativeMorale"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ReduceNegativeMorale))]
    public static ActionsBuilder AddReduceNegativeMorale(
        this ActionsBuilder builder,
        Int32 m_Value)
    {
      builder.Validate(m_Value);
      
      var element = ElementTool.Create<ReduceNegativeMorale>();
      element.m_Value = m_Value;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="AddGrowthBonus"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(AddGrowthBonus))]
    public static ActionsBuilder AddAddGrowthBonus(
        this ActionsBuilder builder,
        Int32 Bonus)
    {
      builder.Validate(Bonus);
      
      var element = ElementTool.Create<AddGrowthBonus>();
      element.Bonus = Bonus;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="AddMercenaryToPool"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Unit"><see cref="BlueprintUnit"/></param>
    [Generated]
    [Implements(typeof(AddMercenaryToPool))]
    public static ActionsBuilder AddAddMercenaryToPool(
        this ActionsBuilder builder,
        string m_Unit,
        Single m_Weight)
    {
      builder.Validate(m_Weight);
      
      var element = ElementTool.Create<AddMercenaryToPool>();
      element.m_Unit = BlueprintTool.GetRef<BlueprintUnitReference>(m_Unit);
      element.m_Weight = m_Weight;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="AddTacticalArmyFeature"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_ArmyUnits"><see cref="BlueprintUnit"/></param>
    /// <param name="m_Features"><see cref="BlueprintFeature"/></param>
    [Generated]
    [Implements(typeof(AddTacticalArmyFeature))]
    public static ActionsBuilder AddAddTacticalArmyFeature(
        this ActionsBuilder builder,
        MercenariesIncludeOption m_MercenariesFilter,
        Boolean m_ByTag,
        ArmyProperties m_ArmyTag,
        Boolean m_ByUnits,
        string[] m_ArmyUnits,
        string[] m_Features,
        ArmyFaction m_Faction)
    {
      builder.Validate(m_MercenariesFilter);
      builder.Validate(m_ByTag);
      builder.Validate(m_ArmyTag);
      builder.Validate(m_ByUnits);
      builder.Validate(m_Faction);
      
      var element = ElementTool.Create<AddTacticalArmyFeature>();
      element.m_MercenariesFilter = m_MercenariesFilter;
      element.m_ByTag = m_ByTag;
      element.m_ArmyTag = m_ArmyTag;
      element.m_ByUnits = m_ByUnits;
      element.m_ArmyUnits = m_ArmyUnits.Select(bp => BlueprintTool.GetRef<BlueprintUnitReference>(bp)).ToArray();
      element.m_Features = m_Features.Select(bp => BlueprintTool.GetRef<BlueprintFeatureReference>(bp)).ToArray();
      element.m_Faction = m_Faction;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ChangeMercenaryWeight"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Unit"><see cref="BlueprintUnit"/></param>
    [Generated]
    [Implements(typeof(ChangeMercenaryWeight))]
    public static ActionsBuilder AddChangeMercenaryWeight(
        this ActionsBuilder builder,
        string m_Unit,
        Single m_Weight)
    {
      builder.Validate(m_Weight);
      
      var element = ElementTool.Create<ChangeMercenaryWeight>();
      element.m_Unit = BlueprintTool.GetRef<BlueprintUnitReference>(m_Unit);
      element.m_Weight = m_Weight;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="DecreaseRecruitsGrowth"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Unit"><see cref="BlueprintUnit"/></param>
    [Generated]
    [Implements(typeof(DecreaseRecruitsGrowth))]
    public static ActionsBuilder AddDecreaseRecruitsGrowth(
        this ActionsBuilder builder,
        string m_Unit,
        Int32 Count)
    {
      builder.Validate(Count);
      
      var element = ElementTool.Create<DecreaseRecruitsGrowth>();
      element.m_Unit = BlueprintTool.GetRef<BlueprintUnitReference>(m_Unit);
      element.Count = Count;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="DecreaseRecruitsPool"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Unit"><see cref="BlueprintUnit"/></param>
    [Generated]
    [Implements(typeof(DecreaseRecruitsPool))]
    public static ActionsBuilder AddDecreaseRecruitsPool(
        this ActionsBuilder builder,
        string m_Unit,
        Int32 Count)
    {
      builder.Validate(Count);
      
      var element = ElementTool.Create<DecreaseRecruitsPool>();
      element.m_Unit = BlueprintTool.GetRef<BlueprintUnitReference>(m_Unit);
      element.Count = Count;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ExchangeRecruits"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_OldUnit"><see cref="BlueprintUnit"/></param>
    /// <param name="m_NewUnit"><see cref="BlueprintUnit"/></param>
    [Generated]
    [Implements(typeof(ExchangeRecruits))]
    public static ActionsBuilder AddExchangeRecruits(
        this ActionsBuilder builder,
        Int32 NewGrowth,
        Int32 OldGrowth,
        Single ConvertCoefficient,
        string m_OldUnit,
        string m_NewUnit)
    {
      builder.Validate(NewGrowth);
      builder.Validate(OldGrowth);
      builder.Validate(ConvertCoefficient);
      
      var element = ElementTool.Create<ExchangeRecruits>();
      element.NewGrowth = NewGrowth;
      element.OldGrowth = OldGrowth;
      element.ConvertCoefficient = ConvertCoefficient;
      element.m_OldUnit = BlueprintTool.GetRef<BlueprintUnitReference>(m_OldUnit);
      element.m_NewUnit = BlueprintTool.GetRef<BlueprintUnitReference>(m_NewUnit);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="IncreaseRecruitsGrowth"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Unit"><see cref="BlueprintUnit"/></param>
    [Generated]
    [Implements(typeof(IncreaseRecruitsGrowth))]
    public static ActionsBuilder AddIncreaseRecruitsGrowth(
        this ActionsBuilder builder,
        string m_Unit,
        Int32 Count)
    {
      builder.Validate(Count);
      
      var element = ElementTool.Create<IncreaseRecruitsGrowth>();
      element.m_Unit = BlueprintTool.GetRef<BlueprintUnitReference>(m_Unit);
      element.Count = Count;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="IncreaseRecruitsPool"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Unit"><see cref="BlueprintUnit"/></param>
    [Generated]
    [Implements(typeof(IncreaseRecruitsPool))]
    public static ActionsBuilder AddIncreaseRecruitsPool(
        this ActionsBuilder builder,
        string m_Unit,
        Int32 Count)
    {
      builder.Validate(Count);
      
      var element = ElementTool.Create<IncreaseRecruitsPool>();
      element.m_Unit = BlueprintTool.GetRef<BlueprintUnitReference>(m_Unit);
      element.Count = Count;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RemoveMercenaryFromPool"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Unit"><see cref="BlueprintUnit"/></param>
    [Generated]
    [Implements(typeof(RemoveMercenaryFromPool))]
    public static ActionsBuilder AddRemoveMercenaryFromPool(
        this ActionsBuilder builder,
        string m_Unit)
    {
      
      var element = ElementTool.Create<RemoveMercenaryFromPool>();
      element.m_Unit = BlueprintTool.GetRef<BlueprintUnitReference>(m_Unit);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ReplaceBuildings"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_OldBuilding"><see cref="BlueprintSettlementBuilding"/></param>
    /// <param name="m_NewBuilding"><see cref="BlueprintSettlementBuilding"/></param>
    [Generated]
    [Implements(typeof(ReplaceBuildings))]
    public static ActionsBuilder AddReplaceBuildings(
        this ActionsBuilder builder,
        string m_OldBuilding,
        string m_NewBuilding)
    {
      
      var element = ElementTool.Create<ReplaceBuildings>();
      element.m_OldBuilding = BlueprintTool.GetRef<BlueprintSettlementBuildingReference>(m_OldBuilding);
      element.m_NewBuilding = BlueprintTool.GetRef<BlueprintSettlementBuildingReference>(m_NewBuilding);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SetRecruitPoint"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Point"><see cref="BlueprintGlobalMapPoint"/></param>
    [Generated]
    [Implements(typeof(SetRecruitPoint))]
    public static ActionsBuilder AddSetRecruitPoint(
        this ActionsBuilder builder,
        string m_Point)
    {
      
      var element = ElementTool.Create<SetRecruitPoint>();
      element.m_Point = BlueprintTool.GetRef<BlueprintGlobalMapPoint.Reference>(m_Point);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnlockUnitsGrowth"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Unit"><see cref="BlueprintUnit"/></param>
    [Generated]
    [Implements(typeof(UnlockUnitsGrowth))]
    public static ActionsBuilder AddUnlockUnitsGrowth(
        this ActionsBuilder builder,
        string m_Unit)
    {
      
      var element = ElementTool.Create<UnlockUnitsGrowth>();
      element.m_Unit = BlueprintTool.GetRef<BlueprintUnitReference>(m_Unit);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionGetArtisanGift"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Artisan"><see cref="BlueprintKingdomArtisan"/></param>
    [Generated]
    [Implements(typeof(KingdomActionGetArtisanGift))]
    public static ActionsBuilder AddKingdomActionGetArtisanGift(
        this ActionsBuilder builder,
        string m_Artisan)
    {
      
      var element = ElementTool.Create<KingdomActionGetArtisanGift>();
      element.m_Artisan = BlueprintTool.GetRef<BlueprintKingdomArtisanReference>(m_Artisan);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionGetArtisanGiftWithCertainTier"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Artisan"><see cref="BlueprintKingdomArtisan"/></param>
    [Generated]
    [Implements(typeof(KingdomActionGetArtisanGiftWithCertainTier))]
    public static ActionsBuilder AddKingdomActionGetArtisanGiftWithCertainTier(
        this ActionsBuilder builder,
        string m_Artisan,
        Int32 tier)
    {
      builder.Validate(tier);
      
      var element = ElementTool.Create<KingdomActionGetArtisanGiftWithCertainTier>();
      element.m_Artisan = BlueprintTool.GetRef<BlueprintKingdomArtisanReference>(m_Artisan);
      element.tier = tier;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionGetPartyGoldByUnitsCount"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionGetPartyGoldByUnitsCount))]
    public static ActionsBuilder AddKingdomActionGetPartyGoldByUnitsCount(
        this ActionsBuilder builder,
        Int32 m_GoldPerUnit,
        Single m_Coefficient)
    {
      builder.Validate(m_GoldPerUnit);
      builder.Validate(m_Coefficient);
      
      var element = ElementTool.Create<KingdomActionGetPartyGoldByUnitsCount>();
      element.m_GoldPerUnit = m_GoldPerUnit;
      element.m_Coefficient = m_Coefficient;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionGetResourcesByUnitsCount"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionGetResourcesByUnitsCount))]
    public static ActionsBuilder AddKingdomActionGetResourcesByUnitsCount(
        this ActionsBuilder builder,
        KingdomResourcesAmount m_ResourcePerUnit,
        Single m_Coefficient)
    {
      builder.Validate(m_ResourcePerUnit);
      builder.Validate(m_Coefficient);
      
      var element = ElementTool.Create<KingdomActionGetResourcesByUnitsCount>();
      element.m_ResourcePerUnit = m_ResourcePerUnit;
      element.m_Coefficient = m_Coefficient;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionGetResourcesPercent"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionGetResourcesPercent))]
    public static ActionsBuilder AddKingdomActionGetResourcesPercent(
        this ActionsBuilder builder,
        Single m_Percent,
        KingdomResource m_ResourceType,
        Int32 m_MaxResourceCountGained)
    {
      builder.Validate(m_Percent);
      builder.Validate(m_ResourceType);
      builder.Validate(m_MaxResourceCountGained);
      
      var element = ElementTool.Create<KingdomActionGetResourcesPercent>();
      element.m_Percent = m_Percent;
      element.m_ResourceType = m_ResourceType;
      element.m_MaxResourceCountGained = m_MaxResourceCountGained;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionGiveLoot"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionGiveLoot))]
    public static ActionsBuilder AddKingdomActionGiveLoot(
        this ActionsBuilder builder,
        LootEntry[] Loot)
    {
      foreach (var item in Loot)
      {
        builder.Validate(item);
      }
      
      var element = ElementTool.Create<KingdomActionGiveLoot>();
      element.Loot = Loot;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionImproveSettlement"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_SpecificSettlement"><see cref="BlueprintSettlement"/></param>
    [Generated]
    [Implements(typeof(KingdomActionImproveSettlement))]
    public static ActionsBuilder AddKingdomActionImproveSettlement(
        this ActionsBuilder builder,
        SettlementState.LevelType ToLevel,
        string m_SpecificSettlement)
    {
      builder.Validate(ToLevel);
      
      var element = ElementTool.Create<KingdomActionImproveSettlement>();
      element.ToLevel = ToLevel;
      element.m_SpecificSettlement = BlueprintTool.GetRef<BlueprintSettlement.Reference>(m_SpecificSettlement);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionImproveStat"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionImproveStat))]
    public static ActionsBuilder AddKingdomActionImproveStat(
        this ActionsBuilder builder,
        KingdomStats.Type StatType)
    {
      builder.Validate(StatType);
      
      var element = ElementTool.Create<KingdomActionImproveStat>();
      element.StatType = StatType;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionMakeRoll"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionMakeRoll))]
    public static ActionsBuilder AddKingdomActionMakeRoll(
        this ActionsBuilder builder,
        KingdomStats.Type Stat,
        Int32 DC,
        ActionsBuilder OnSuccess,
        ActionsBuilder OnFailure)
    {
      builder.Validate(Stat);
      builder.Validate(DC);
      
      var element = ElementTool.Create<KingdomActionMakeRoll>();
      element.Stat = Stat;
      element.DC = DC;
      element.OnSuccess = OnSuccess.Build();
      element.OnFailure = OnFailure.Build();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionModifyBuildTime"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionModifyBuildTime))]
    public static ActionsBuilder AddKingdomActionModifyBuildTime(
        this ActionsBuilder builder,
        Single ChangeTime)
    {
      builder.Validate(ChangeTime);
      
      var element = ElementTool.Create<KingdomActionModifyBuildTime>();
      element.ChangeTime = ChangeTime;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionModifyClaims"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionModifyClaims))]
    public static ActionsBuilder AddKingdomActionModifyClaims(
        this ActionsBuilder builder,
        Single ChangeTime,
        Single ChangeCost)
    {
      builder.Validate(ChangeTime);
      builder.Validate(ChangeCost);
      
      var element = ElementTool.Create<KingdomActionModifyClaims>();
      element.ChangeTime = ChangeTime;
      element.ChangeCost = ChangeCost;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionModifyEventDC"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionModifyEventDC))]
    public static ActionsBuilder AddKingdomActionModifyEventDC(
        this ActionsBuilder builder,
        Int32 Modifier)
    {
      builder.Validate(Modifier);
      
      var element = ElementTool.Create<KingdomActionModifyEventDC>();
      element.Modifier = Modifier;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionModifyRE"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionModifyRE))]
    public static ActionsBuilder AddKingdomActionModifyRE(
        this ActionsBuilder builder,
        Single UnclaimedChange,
        Single ClaimedChange,
        Single UpgradedChange)
    {
      builder.Validate(UnclaimedChange);
      builder.Validate(ClaimedChange);
      builder.Validate(UpgradedChange);
      
      var element = ElementTool.Create<KingdomActionModifyRE>();
      element.UnclaimedChange = UnclaimedChange;
      element.ClaimedChange = ClaimedChange;
      element.UpgradedChange = UpgradedChange;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionModifyRankTime"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionModifyRankTime))]
    public static ActionsBuilder AddKingdomActionModifyRankTime(
        this ActionsBuilder builder,
        Single ChangeTime)
    {
      builder.Validate(ChangeTime);
      
      var element = ElementTool.Create<KingdomActionModifyRankTime>();
      element.ChangeTime = ChangeTime;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionModifyStatRandom"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionModifyStatRandom))]
    public static ActionsBuilder AddKingdomActionModifyStatRandom(
        this ActionsBuilder builder,
        Boolean IncludeInEventStats,
        DiceFormula Change)
    {
      builder.Validate(IncludeInEventStats);
      builder.Validate(Change);
      
      var element = ElementTool.Create<KingdomActionModifyStatRandom>();
      element.IncludeInEventStats = IncludeInEventStats;
      element.Change = Change;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionModifyStats"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionModifyStats))]
    public static ActionsBuilder AddKingdomActionModifyStats(
        this ActionsBuilder builder,
        Boolean IncludeInEventStats,
        KingdomStats.Changes Changes)
    {
      builder.Validate(IncludeInEventStats);
      builder.Validate(Changes);
      
      var element = ElementTool.Create<KingdomActionModifyStats>();
      element.IncludeInEventStats = IncludeInEventStats;
      element.Changes = Changes;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionModifyUnrest"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionModifyUnrest))]
    public static ActionsBuilder AddKingdomActionModifyUnrest(
        this ActionsBuilder builder,
        Boolean MakeBetter,
        Boolean Bounded,
        KingdomStatusChangeReason Reason,
        SharedStringAsset ReasonString,
        KingdomStatusType UpTo)
    {
      builder.Validate(MakeBetter);
      builder.Validate(Bounded);
      builder.Validate(Reason);
      builder.Validate(ReasonString);
      builder.Validate(UpTo);
      
      var element = ElementTool.Create<KingdomActionModifyUnrest>();
      element.MakeBetter = MakeBetter;
      element.Bounded = Bounded;
      element.Reason = Reason;
      element.ReasonString = ReasonString;
      element.UpTo = UpTo;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionNextChapter"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionNextChapter))]
    public static ActionsBuilder AddKingdomActionNextChapter(
        this ActionsBuilder builder,
        Int32 ChapterNumber)
    {
      builder.Validate(ChapterNumber);
      
      var element = ElementTool.Create<KingdomActionNextChapter>();
      element.ChapterNumber = ChapterNumber;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionPullRankupChangesIntoDialog"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionPullRankupChangesIntoDialog))]
    public static ActionsBuilder AddKingdomActionPullRankupChangesIntoDialog(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<KingdomActionPullRankupChangesIntoDialog>());
    }

    /// <summary>
    /// Adds <see cref="KingdomActionRemoveAllLeaders"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionRemoveAllLeaders))]
    public static ActionsBuilder AddKingdomActionRemoveAllLeaders(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<KingdomActionRemoveAllLeaders>());
    }

    /// <summary>
    /// Adds <see cref="KingdomActionRemoveBuff"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Blueprint"><see cref="BlueprintKingdomBuff"/></param>
    /// <param name="m_Region"><see cref="BlueprintRegion"/></param>
    [Generated]
    [Implements(typeof(KingdomActionRemoveBuff))]
    public static ActionsBuilder AddKingdomActionRemoveBuff(
        this ActionsBuilder builder,
        string m_Blueprint,
        Boolean ApplyToRegion,
        string m_Region,
        Boolean m_AllBuffs)
    {
      builder.Validate(ApplyToRegion);
      builder.Validate(m_AllBuffs);
      
      var element = ElementTool.Create<KingdomActionRemoveBuff>();
      element.m_Blueprint = BlueprintTool.GetRef<BlueprintKingdomBuffReference>(m_Blueprint);
      element.ApplyToRegion = ApplyToRegion;
      element.m_Region = BlueprintTool.GetRef<BlueprintRegionReference>(m_Region);
      element.m_AllBuffs = m_AllBuffs;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionRemoveEvent"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_EventBlueprint"><see cref="BlueprintKingdomEventBase"/></param>
    [Generated]
    [Implements(typeof(KingdomActionRemoveEvent))]
    public static ActionsBuilder AddKingdomActionRemoveEvent(
        this ActionsBuilder builder,
        string m_EventBlueprint,
        Boolean CancelIfInProgress,
        Boolean AllIfMultiple)
    {
      builder.Validate(CancelIfInProgress);
      builder.Validate(AllIfMultiple);
      
      var element = ElementTool.Create<KingdomActionRemoveEvent>();
      element.m_EventBlueprint = BlueprintTool.GetRef<BlueprintKingdomEventBaseReference>(m_EventBlueprint);
      element.CancelIfInProgress = CancelIfInProgress;
      element.AllIfMultiple = AllIfMultiple;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionRemoveEventDeck"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Deck"><see cref="BlueprintKingdomDeck"/></param>
    [Generated]
    [Implements(typeof(KingdomActionRemoveEventDeck))]
    public static ActionsBuilder AddKingdomActionRemoveEventDeck(
        this ActionsBuilder builder,
        string m_Deck)
    {
      
      var element = ElementTool.Create<KingdomActionRemoveEventDeck>();
      element.m_Deck = BlueprintTool.GetRef<BlueprintKingdomDeckReference>(m_Deck);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionRequestArtisanGift"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Artisan"><see cref="BlueprintKingdomArtisan"/></param>
    /// <param name="m_ItemType"><see cref="ArtisanItemDeck"/></param>
    [Generated]
    [Implements(typeof(KingdomActionRequestArtisanGift))]
    public static ActionsBuilder AddKingdomActionRequestArtisanGift(
        this ActionsBuilder builder,
        string m_Artisan,
        string m_ItemType)
    {
      
      var element = ElementTool.Create<KingdomActionRequestArtisanGift>();
      element.m_Artisan = BlueprintTool.GetRef<BlueprintKingdomArtisanReference>(m_Artisan);
      element.m_ItemType = BlueprintTool.GetRef<ArtisanItemDeckReference>(m_ItemType);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionResetRecurrence"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionResetRecurrence))]
    public static ActionsBuilder AddKingdomActionResetRecurrence(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<KingdomActionResetRecurrence>());
    }

    /// <summary>
    /// Adds <see cref="KingdomActionResolveCrusadeEvent"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_EventBlueprint"><see cref="BlueprintCrusadeEvent"/></param>
    [Generated]
    [Implements(typeof(KingdomActionResolveCrusadeEvent))]
    public static ActionsBuilder AddKingdomActionResolveCrusadeEvent(
        this ActionsBuilder builder,
        string m_EventBlueprint,
        Int32 m_SolutionIndex)
    {
      builder.Validate(m_SolutionIndex);
      
      var element = ElementTool.Create<KingdomActionResolveCrusadeEvent>();
      element.m_EventBlueprint = BlueprintTool.GetRef<BlueprintCrusadeEvent.Reference>(m_EventBlueprint);
      element.m_SolutionIndex = m_SolutionIndex;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionResolveEvent"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_EventBlueprint"><see cref="BlueprintKingdomEvent"/></param>
    [Generated]
    [Implements(typeof(KingdomActionResolveEvent))]
    public static ActionsBuilder AddKingdomActionResolveEvent(
        this ActionsBuilder builder,
        string m_EventBlueprint,
        EventResult.MarginType Result,
        Alignment Alignment,
        Boolean FinalResolve)
    {
      builder.Validate(Result);
      builder.Validate(Alignment);
      builder.Validate(FinalResolve);
      
      var element = ElementTool.Create<KingdomActionResolveEvent>();
      element.m_EventBlueprint = BlueprintTool.GetRef<BlueprintKingdomEventReference>(m_EventBlueprint);
      element.Result = Result;
      element.Alignment = Alignment;
      element.FinalResolve = FinalResolve;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionResolveProject"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_EventBlueprint"><see cref="BlueprintKingdomProject"/></param>
    [Generated]
    [Implements(typeof(KingdomActionResolveProject))]
    public static ActionsBuilder AddKingdomActionResolveProject(
        this ActionsBuilder builder,
        string m_EventBlueprint)
    {
      
      var element = ElementTool.Create<KingdomActionResolveProject>();
      element.m_EventBlueprint = BlueprintTool.GetRef<BlueprintKingdomProjectReference>(m_EventBlueprint);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionRestartEvent"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionRestartEvent))]
    public static ActionsBuilder AddKingdomActionRestartEvent(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<KingdomActionRestartEvent>());
    }

    /// <summary>
    /// Adds <see cref="KingdomActionRollbackRecurrence"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionRollbackRecurrence))]
    public static ActionsBuilder AddKingdomActionRollbackRecurrence(
        this ActionsBuilder builder,
        KingdomActionRollbackRecurrence.RollbackType m_Type,
        Int32 m_LastNDays,
        Int32 m_LastNTimes,
        Single m_ResourcesRatio,
        Boolean m_IncludeResources,
        Boolean m_IncludeResourcesPerTurn,
        Boolean m_IncludeStats)
    {
      builder.Validate(m_Type);
      builder.Validate(m_LastNDays);
      builder.Validate(m_LastNTimes);
      builder.Validate(m_ResourcesRatio);
      builder.Validate(m_IncludeResources);
      builder.Validate(m_IncludeResourcesPerTurn);
      builder.Validate(m_IncludeStats);
      
      var element = ElementTool.Create<KingdomActionRollbackRecurrence>();
      element.m_Type = m_Type;
      element.m_LastNDays = m_LastNDays;
      element.m_LastNTimes = m_LastNTimes;
      element.m_ResourcesRatio = m_ResourcesRatio;
      element.m_IncludeResources = m_IncludeResources;
      element.m_IncludeResourcesPerTurn = m_IncludeResourcesPerTurn;
      element.m_IncludeStats = m_IncludeStats;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionSetAlignment"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionSetAlignment))]
    public static ActionsBuilder AddKingdomActionSetAlignment(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<KingdomActionSetAlignment>());
    }

    /// <summary>
    /// Adds <see cref="KingdomActionSetNotVisible"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionSetNotVisible))]
    public static ActionsBuilder AddKingdomActionSetNotVisible(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<KingdomActionSetNotVisible>());
    }

    /// <summary>
    /// Adds <see cref="KingdomActionSetRegionalIncome"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionSetRegionalIncome))]
    public static ActionsBuilder AddKingdomActionSetRegionalIncome(
        this ActionsBuilder builder,
        KingdomResourcesAmount IncomePerClaimed,
        KingdomResourcesAmount IncomePerUpgraded,
        Boolean Add)
    {
      builder.Validate(IncomePerClaimed);
      builder.Validate(IncomePerUpgraded);
      builder.Validate(Add);
      
      var element = ElementTool.Create<KingdomActionSetRegionalIncome>();
      element.IncomePerClaimed = IncomePerClaimed;
      element.IncomePerUpgraded = IncomePerUpgraded;
      element.Add = Add;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionSetVisible"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(KingdomActionSetVisible))]
    public static ActionsBuilder AddKingdomActionSetVisible(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<KingdomActionSetVisible>());
    }

    /// <summary>
    /// Adds <see cref="KingdomActionSpawnRandomArmy"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Armies"><see cref="BlueprintArmyPreset"/></param>
    /// <param name="m_Locations"><see cref="BlueprintGlobalMapPoint"/></param>
    [Generated]
    [Implements(typeof(KingdomActionSpawnRandomArmy))]
    public static ActionsBuilder AddKingdomActionSpawnRandomArmy(
        this ActionsBuilder builder,
        string[] m_Armies,
        ArmyFaction m_Faction,
        string[] m_Locations)
    {
      builder.Validate(m_Faction);
      
      var element = ElementTool.Create<KingdomActionSpawnRandomArmy>();
      element.m_Armies = m_Armies.Select(bp => BlueprintTool.GetRef<BlueprintArmyPresetReference>(bp)).ToList();
      element.m_Faction = m_Faction;
      element.m_Locations = m_Locations.Select(bp => BlueprintTool.GetRef<BlueprintGlobalMapPoint.Reference>(bp)).ToList();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionStartEvent"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Event"><see cref="BlueprintKingdomEventBase"/></param>
    /// <param name="m_Region"><see cref="BlueprintRegion"/></param>
    [Generated]
    [Implements(typeof(KingdomActionStartEvent))]
    public static ActionsBuilder AddKingdomActionStartEvent(
        this ActionsBuilder builder,
        string m_Event,
        string m_Region,
        Boolean RandomRegion,
        Int32 DelayDays,
        Boolean StartNextMonth,
        Boolean CheckTriggerImmediately,
        Boolean CheckTriggerOnStart)
    {
      builder.Validate(RandomRegion);
      builder.Validate(DelayDays);
      builder.Validate(StartNextMonth);
      builder.Validate(CheckTriggerImmediately);
      builder.Validate(CheckTriggerOnStart);
      
      var element = ElementTool.Create<KingdomActionStartEvent>();
      element.m_Event = BlueprintTool.GetRef<BlueprintKingdomEventBaseReference>(m_Event);
      element.m_Region = BlueprintTool.GetRef<BlueprintRegionReference>(m_Region);
      element.RandomRegion = RandomRegion;
      element.DelayDays = DelayDays;
      element.StartNextMonth = StartNextMonth;
      element.CheckTriggerImmediately = CheckTriggerImmediately;
      element.CheckTriggerOnStart = CheckTriggerOnStart;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="KingdomActionUnlockArtisan"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Artisan"><see cref="BlueprintKingdomArtisan"/></param>
    [Generated]
    [Implements(typeof(KingdomActionUnlockArtisan))]
    public static ActionsBuilder AddKingdomActionUnlockArtisan(
        this ActionsBuilder builder,
        string m_Artisan)
    {
      
      var element = ElementTool.Create<KingdomActionUnlockArtisan>();
      element.m_Artisan = BlueprintTool.GetRef<BlueprintKingdomArtisanReference>(m_Artisan);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RemoveCrusadeResources"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(RemoveCrusadeResources))]
    public static ActionsBuilder AddRemoveCrusadeResources(
        this ActionsBuilder builder,
        KingdomResourcesAmount m_ResourcesAmount)
    {
      builder.Validate(m_ResourcesAmount);
      
      var element = ElementTool.Create<RemoveCrusadeResources>();
      element.m_ResourcesAmount = m_ResourcesAmount;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="BlockTacticalCell"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(BlockTacticalCell))]
    public static ActionsBuilder AddBlockTacticalCell(
        this ActionsBuilder builder,
        TacticalMapObstacle.Link m_ObstaclePrefab)
    {
      builder.Validate(m_ObstaclePrefab);
      
      var element = ElementTool.Create<BlockTacticalCell>();
      element.m_ObstaclePrefab = m_ObstaclePrefab;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="TacticalCombatRecoverLeaderMana"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(TacticalCombatRecoverLeaderMana))]
    public static ActionsBuilder AddTacticalCombatRecoverLeaderMana(
        this ActionsBuilder builder,
        ContextValue m_Value)
    {
      builder.Validate(m_Value);
      
      var element = ElementTool.Create<TacticalCombatRecoverLeaderMana>();
      element.m_Value = m_Value;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="CreateArmyFromLosses"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Location"><see cref="BlueprintGlobalMapPoint"/></param>
    [Generated]
    [Implements(typeof(CreateArmyFromLosses))]
    public static ActionsBuilder AddCreateArmyFromLosses(
        this ActionsBuilder builder,
        Int32 m_SumExperience,
        Int32 m_SquadsMaxCount,
        string m_Location,
        Boolean m_ApplyRecruitIncrease)
    {
      builder.Validate(m_SumExperience);
      builder.Validate(m_SquadsMaxCount);
      builder.Validate(m_ApplyRecruitIncrease);
      
      var element = ElementTool.Create<CreateArmyFromLosses>();
      element.m_SumExperience = m_SumExperience;
      element.m_SquadsMaxCount = m_SquadsMaxCount;
      element.m_Location = BlueprintTool.GetRef<BlueprintGlobalMapPoint.Reference>(m_Location);
      element.m_ApplyRecruitIncrease = m_ApplyRecruitIncrease;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="EnterKingdomInterface"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_ReturnPoint"><see cref="BlueprintAreaEnterPoint"/></param>
    [Generated]
    [Implements(typeof(EnterKingdomInterface))]
    public static ActionsBuilder AddEnterKingdomInterface(
        this ActionsBuilder builder,
        string m_ReturnPoint,
        ActionsBuilder TriggerAfterAuto)
    {
      
      var element = ElementTool.Create<EnterKingdomInterface>();
      element.m_ReturnPoint = BlueprintTool.GetRef<BlueprintAreaEnterPointReference>(m_ReturnPoint);
      element.TriggerAfterAuto = TriggerAfterAuto.Build();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RecruiteArmyLeader"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="ArmyLeader"><see cref="BlueprintArmyLeader"/></param>
    [Generated]
    [Implements(typeof(RecruiteArmyLeader))]
    public static ActionsBuilder AddRecruiteArmyLeader(
        this ActionsBuilder builder,
        string ArmyLeader)
    {
      
      var element = ElementTool.Create<RecruiteArmyLeader>();
      element.ArmyLeader = BlueprintTool.GetRef<ArmyLeader.Reference>(ArmyLeader);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RemoveDemonArmies"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_ArmyPreset"><see cref="BlueprintArmyPreset"/></param>
    [Generated]
    [Implements(typeof(RemoveDemonArmies))]
    public static ActionsBuilder AddRemoveDemonArmies(
        this ActionsBuilder builder,
        string m_ArmyPreset,
        ArmyType m_ArmyType)
    {
      builder.Validate(m_ArmyType);
      
      var element = ElementTool.Create<RemoveDemonArmies>();
      element.m_ArmyPreset = BlueprintTool.GetRef<BlueprintArmyPresetReference>(m_ArmyPreset);
      element.m_ArmyType = m_ArmyType;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RemoveGarrison"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Location"><see cref="BlueprintGlobalMapPoint"/></param>
    [Generated]
    [Implements(typeof(RemoveGarrison))]
    public static ActionsBuilder AddRemoveGarrison(
        this ActionsBuilder builder,
        string m_Location,
        Boolean HandleAsGarrisonDefeated)
    {
      builder.Validate(HandleAsGarrisonDefeated);
      
      var element = ElementTool.Create<RemoveGarrison>();
      element.m_Location = BlueprintTool.GetRef<BlueprintGlobalMapPoint.Reference>(m_Location);
      element.HandleAsGarrisonDefeated = HandleAsGarrisonDefeated;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RemoveUnitFromArmy"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_UnitToRemove"><see cref="BlueprintUnit"/></param>
    [Generated]
    [Implements(typeof(RemoveUnitFromArmy))]
    public static ActionsBuilder AddRemoveUnitFromArmy(
        this ActionsBuilder builder,
        ArmiesEvaluator m_Armies,
        RemoveUnitFromArmy.RemoveUnitFromArmyMode m_Mode,
        Boolean m_RemoveCheapestUnit,
        Boolean m_RemoveSpecificUnit,
        string m_UnitToRemove,
        Boolean m_LimitUnitExperienceMinimum,
        Int32 m_UnitExperienceMinimum,
        Boolean m_LimitUnitExperienceMaximum,
        Int32 m_UnitExperienceMaximum,
        UnitTag[] m_UnitTagWhitelist,
        UnitTag[] m_UnitTagBlacklist,
        Int32 m_Experience,
        Single m_Percentage)
    {
      builder.Validate(m_Armies);
      builder.Validate(m_Mode);
      builder.Validate(m_RemoveCheapestUnit);
      builder.Validate(m_RemoveSpecificUnit);
      builder.Validate(m_LimitUnitExperienceMinimum);
      builder.Validate(m_UnitExperienceMinimum);
      builder.Validate(m_LimitUnitExperienceMaximum);
      builder.Validate(m_UnitExperienceMaximum);
      foreach (var item in m_UnitTagWhitelist)
      {
        builder.Validate(item);
      }
      foreach (var item in m_UnitTagBlacklist)
      {
        builder.Validate(item);
      }
      builder.Validate(m_Experience);
      builder.Validate(m_Percentage);
      
      var element = ElementTool.Create<RemoveUnitFromArmy>();
      element.m_Armies = m_Armies;
      element.m_Mode = m_Mode;
      element.m_RemoveCheapestUnit = m_RemoveCheapestUnit;
      element.m_RemoveSpecificUnit = m_RemoveSpecificUnit;
      element.m_UnitToRemove = BlueprintTool.GetRef<BlueprintUnitReference>(m_UnitToRemove);
      element.m_LimitUnitExperienceMinimum = m_LimitUnitExperienceMinimum;
      element.m_UnitExperienceMinimum = m_UnitExperienceMinimum;
      element.m_LimitUnitExperienceMaximum = m_LimitUnitExperienceMaximum;
      element.m_UnitExperienceMaximum = m_UnitExperienceMaximum;
      element.m_UnitTagWhitelist = m_UnitTagWhitelist;
      element.m_UnitTagBlacklist = m_UnitTagBlacklist;
      element.m_Experience = m_Experience;
      element.m_Percentage = m_Percentage;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SetWarCampLocation"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Location"><see cref="BlueprintGlobalMapPoint"/></param>
    [Generated]
    [Implements(typeof(SetWarCampLocation))]
    public static ActionsBuilder AddSetWarCampLocation(
        this ActionsBuilder builder,
        string m_Location)
    {
      
      var element = ElementTool.Create<SetWarCampLocation>();
      element.m_Location = BlueprintTool.GetRef<BlueprintGlobalMapPoint.Reference>(m_Location);
      return builder.Add(element);
    }
  }
}
