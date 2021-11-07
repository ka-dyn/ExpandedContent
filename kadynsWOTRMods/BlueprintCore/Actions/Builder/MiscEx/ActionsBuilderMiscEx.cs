using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints;
using BlueprintCore.Utils;
using Kingmaker;
using Kingmaker.Achievements.Actions;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Loot;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.Tutorial;
using Kingmaker.Tutorial.Actions;
using Kingmaker.UnitLogic.FactLogic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueprintCore.Actions.Builder.MiscEx
{
  /// <summary>
  /// Extension to <see cref="ActionsBuilder"/> for actions without a better extension container such as achievements
  /// vendor actions, and CustomEvent.
  /// </summary>
  /// <inheritdoc cref="ActionsBuilder"/>
  public static class ActionsBuilderMiscEx
  {
    //----- Kingmaker.Achievements.Actions -----//

    /// <summary>
    /// Adds <see cref="ActionAchievementIncrementCounter"/>
    /// </summary>
    /// 
    /// <param name="achievement"><see cref="Kingmaker.Achievements.Blueprints.AchievementData">AchievementData</see></param>
    [Implements(typeof(ActionAchievementIncrementCounter))]
    public static ActionsBuilder IncrementAchievement(this ActionsBuilder builder, string achievement)
    {
      var increment = ElementTool.Create<ActionAchievementIncrementCounter>();
      increment.m_Achievement = BlueprintTool.GetRef<AchievementDataReference>(achievement);
      return builder.Add(increment);
    }

    /// <summary>
    /// Adds <see cref="ActionAchievementUnlock"/>
    /// </summary>
    /// 
    /// <param name="achievement"><see cref="Kingmaker.Achievements.Blueprints.AchievementData">AchievementData</see></param>
    [Implements(typeof(ActionAchievementUnlock))]
    public static ActionsBuilder UnlockAchievement(this ActionsBuilder builder, string achievement)
    {
      var unlock = ElementTool.Create<ActionAchievementUnlock>();
      unlock.m_Achievement = BlueprintTool.GetRef<AchievementDataReference>(achievement);
      return builder.Add(unlock);
    }

    //----- Kingmaker.Designers.EventConditionActionSystem.Actions -----//

    /// <summary>
    /// Adds <see cref="Kingmaker.Designers.EventConditionActionSystem.Actions.CreateCustomCompanion">CreateCustomCompanion</see>
    /// </summary>
    [Implements(typeof(CreateCustomCompanion))]
    public static ActionsBuilder CreateCustomCompanion(
        this ActionsBuilder builder,
        bool free = false,
        bool matchPlayerXp = false,
        ActionsBuilder onCreate = null)
    {
      var createCompanion = ElementTool.Create<CreateCustomCompanion>();
      createCompanion.ForFree = free;
      createCompanion.MatchPlayerXpExactly = matchPlayerXp;
      createCompanion.OnCreate = onCreate?.Build() ?? Constants.Empty.Actions;
      return builder.Add(createCompanion);
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.Designers.EventConditionActionSystem.Actions.CustomEvent">CustomEvent</see>
    /// </summary>
    [Implements(typeof(CustomEvent))]
    public static ActionsBuilder CustomEvent(this ActionsBuilder builder, string eventId)
    {
      var customEvent = ElementTool.Create<CustomEvent>();
      customEvent.EventId = eventId;
      return builder.Add(customEvent);
    }

    //----- Auto Generated -----//



    /// <summary>
    /// Adds <see cref="ShowNewTutorial"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Tutorial"><see cref="BlueprintTutorial"/></param>
    [Generated]
    [Implements(typeof(ShowNewTutorial))]
    public static ActionsBuilder AddShowNewTutorial(
        this ActionsBuilder builder,
        string m_Tutorial,
        TutorialContextDataEvaluator[] Evaluators)
    {
      foreach (var item in Evaluators)
      {
        builder.Validate(item);
      }
      
      var element = ElementTool.Create<ShowNewTutorial>();
      element.m_Tutorial = BlueprintTool.GetRef<BlueprintTutorial.Reference>(m_Tutorial);
      element.Evaluators = Evaluators;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="AddVendorItemsAction"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_VendorTable"><see cref="BlueprintUnitLoot"/></param>
    [Generated]
    [Implements(typeof(AddVendorItemsAction))]
    public static ActionsBuilder AddAddVendorItemsAction(
        this ActionsBuilder builder,
        VendorEvaluator m_VendorEvaluator,
        string m_VendorTable)
    {
      builder.Validate(m_VendorEvaluator);
      
      var element = ElementTool.Create<AddVendorItemsAction>();
      element.m_VendorEvaluator = m_VendorEvaluator;
      element.m_VendorTable = BlueprintTool.GetRef<BlueprintUnitLootReference>(m_VendorTable);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ClearVendorTable"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Table"><see cref="BlueprintSharedVendorTable"/></param>
    [Generated]
    [Implements(typeof(ClearVendorTable))]
    public static ActionsBuilder AddClearVendorTable(
        this ActionsBuilder builder,
        string m_Table)
    {
      
      var element = ElementTool.Create<ClearVendorTable>();
      element.m_Table = BlueprintTool.GetRef<BlueprintSharedVendorTableReference>(m_Table);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="AddPremiumReward"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_DlcReward"><see cref="BlueprintDlcReward"/></param>
    /// <param name="Items"><see cref="BlueprintItem"/></param>
    /// <param name="PlayerFeatures"><see cref="BlueprintFeature"/></param>
    [Generated]
    [Implements(typeof(AddPremiumReward))]
    public static ActionsBuilder AddAddPremiumReward(
        this ActionsBuilder builder,
        string m_DlcReward,
        string[] Items,
        string[] PlayerFeatures,
        ActionsBuilder AdditionalActions)
    {
      
      var element = ElementTool.Create<AddPremiumReward>();
      element.m_DlcReward = BlueprintTool.GetRef<BlueprintDlcRewardReference>(m_DlcReward);
      element.Items = Items.Select(bp => BlueprintTool.GetRef<BlueprintItemReference>(bp)).ToList();
      element.PlayerFeatures = PlayerFeatures.Select(bp => BlueprintTool.GetRef<BlueprintFeatureReference>(bp)).ToList();
      element.AdditionalActions = AdditionalActions.Build();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="DebugLog"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(DebugLog))]
    public static ActionsBuilder AddDebugLog(
        this ActionsBuilder builder,
        String Log,
        Boolean Break)
    {
      foreach (var item in Log)
      {
        builder.Validate(item);
      }
      builder.Validate(Break);
      
      var element = ElementTool.Create<DebugLog>();
      element.Log = Log;
      element.Break = Break;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="GameOver"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(GameOver))]
    public static ActionsBuilder AddGameOver(
        this ActionsBuilder builder,
        Player.GameOverReasonType Reason)
    {
      builder.Validate(Reason);
      
      var element = ElementTool.Create<GameOver>();
      element.Reason = Reason;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="MakeAutoSave"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(MakeAutoSave))]
    public static ActionsBuilder AddMakeAutoSave(
        this ActionsBuilder builder,
        Boolean SaveForImport)
    {
      builder.Validate(SaveForImport);
      
      var element = ElementTool.Create<MakeAutoSave>();
      element.SaveForImport = SaveForImport;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="MakeItemNonRemovable"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Item"><see cref="BlueprintItem"/></param>
    [Generated]
    [Implements(typeof(MakeItemNonRemovable))]
    public static ActionsBuilder AddMakeItemNonRemovable(
        this ActionsBuilder builder,
        string m_Item,
        Boolean NonRemovable)
    {
      builder.Validate(NonRemovable);
      
      var element = ElementTool.Create<MakeItemNonRemovable>();
      element.m_Item = BlueprintTool.GetRef<BlueprintItemReference>(m_Item);
      element.NonRemovable = NonRemovable;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="MovePartyItemsAction"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(MovePartyItemsAction))]
    public static ActionsBuilder AddMovePartyItemsAction(
        this ActionsBuilder builder,
        MovePartyItemsAction.ItemType PickupTypes,
        ItemsCollectionEvaluator TargetCollection,
        MovePartyItemsAction.LeaveSettings m_LeaveEquipmentOf)
    {
      builder.Validate(PickupTypes);
      builder.Validate(TargetCollection);
      builder.Validate(m_LeaveEquipmentOf);
      
      var element = ElementTool.Create<MovePartyItemsAction>();
      element.PickupTypes = PickupTypes;
      element.TargetCollection = TargetCollection;
      element.m_LeaveEquipmentOf = m_LeaveEquipmentOf;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="OpenSelectMythicUI"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(OpenSelectMythicUI))]
    public static ActionsBuilder AddOpenSelectMythicUI(
        this ActionsBuilder builder,
        ActionsBuilder m_AfterCommitActions,
        Boolean m_LockStopChargen,
        ActionsBuilder m_AfterStopActions)
    {
      builder.Validate(m_LockStopChargen);
      
      var element = ElementTool.Create<OpenSelectMythicUI>();
      element.m_AfterCommitActions = m_AfterCommitActions.Build();
      element.m_LockStopChargen = m_LockStopChargen;
      element.m_AfterStopActions = m_AfterStopActions.Build();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RemoveItemFromPlayer"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_ItemToRemove"><see cref="BlueprintItem"/></param>
    [Generated]
    [Implements(typeof(RemoveItemFromPlayer))]
    public static ActionsBuilder AddRemoveItemFromPlayer(
        this ActionsBuilder builder,
        Boolean Money,
        Boolean RemoveAll,
        string m_ItemToRemove,
        Boolean m_Silent,
        Int32 Quantity,
        Single Percentage)
    {
      builder.Validate(Money);
      builder.Validate(RemoveAll);
      builder.Validate(m_Silent);
      builder.Validate(Quantity);
      builder.Validate(Percentage);
      
      var element = ElementTool.Create<RemoveItemFromPlayer>();
      element.Money = Money;
      element.RemoveAll = RemoveAll;
      element.m_ItemToRemove = BlueprintTool.GetRef<BlueprintItemReference>(m_ItemToRemove);
      element.m_Silent = m_Silent;
      element.Quantity = Quantity;
      element.Percentage = Percentage;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RemoveItemsFromCollection"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(RemoveItemsFromCollection))]
    public static ActionsBuilder AddRemoveItemsFromCollection(
        this ActionsBuilder builder,
        ItemsCollectionEvaluator Collection,
        List<LootEntry> Loot)
    {
      builder.Validate(Collection);
      foreach (var item in Loot)
      {
        builder.Validate(item);
      }
      
      var element = ElementTool.Create<RemoveItemsFromCollection>();
      element.Collection = Collection;
      element.Loot = Loot;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RemoveDuplicateItems"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Blueprint"><see cref="BlueprintItem"/></param>
    [Generated]
    [Implements(typeof(RemoveDuplicateItems))]
    public static ActionsBuilder AddRemoveDuplicateItems(
        this ActionsBuilder builder,
        UnitEvaluator Unit,
        string m_Blueprint)
    {
      builder.Validate(Unit);
      
      var element = ElementTool.Create<RemoveDuplicateItems>();
      element.Unit = Unit;
      element.m_Blueprint = BlueprintTool.GetRef<BlueprintItemReference>(m_Blueprint);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RestoreItemsCountInCollection"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Item"><see cref="BlueprintItem"/></param>
    [Generated]
    [Implements(typeof(RestoreItemsCountInCollection))]
    public static ActionsBuilder AddRestoreItemsCountInCollection(
        this ActionsBuilder builder,
        string m_Item,
        ItemsCollectionEvaluator Collection,
        IntEvaluator Count)
    {
      builder.Validate(Collection);
      builder.Validate(Count);
      
      var element = ElementTool.Create<RestoreItemsCountInCollection>();
      element.m_Item = BlueprintTool.GetRef<BlueprintItemReference>(m_Item);
      element.Collection = Collection;
      element.Count = Count;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SellCollectibleItems"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_ItemToSell"><see cref="BlueprintItem"/></param>
    [Generated]
    [Implements(typeof(SellCollectibleItems))]
    public static ActionsBuilder AddSellCollectibleItems(
        this ActionsBuilder builder,
        string m_ItemToSell,
        Boolean HalfPrice)
    {
      builder.Validate(HalfPrice);
      
      var element = ElementTool.Create<SellCollectibleItems>();
      element.m_ItemToSell = BlueprintTool.GetRef<BlueprintItemReference>(m_ItemToSell);
      element.HalfPrice = HalfPrice;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SetStartDate"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(SetStartDate))]
    public static ActionsBuilder AddSetStartDate(
        this ActionsBuilder builder,
        String Date)
    {
      foreach (var item in Date)
      {
        builder.Validate(item);
      }
      
      var element = ElementTool.Create<SetStartDate>();
      element.Date = Date;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SetVendorPriceModifier"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(SetVendorPriceModifier))]
    public static ActionsBuilder AddSetVendorPriceModifier(
        this ActionsBuilder builder,
        UnitEvaluator VendorUnit,
        SetVendorPriceModifier.Entry[] m_Entries)
    {
      builder.Validate(VendorUnit);
      foreach (var item in m_Entries)
      {
        builder.Validate(item);
      }
      
      var element = ElementTool.Create<SetVendorPriceModifier>();
      element.VendorUnit = VendorUnit;
      element.m_Entries = m_Entries;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ShowPartySelection"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ShowPartySelection))]
    public static ActionsBuilder AddShowPartySelection(
        this ActionsBuilder builder,
        ActionsBuilder ActionsAfterPartySelection,
        ActionsBuilder ActionsIfCanceled)
    {
      
      var element = ElementTool.Create<ShowPartySelection>();
      element.ActionsAfterPartySelection = ActionsAfterPartySelection.Build();
      element.ActionsIfCanceled = ActionsIfCanceled.Build();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="StartTrade"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(StartTrade))]
    public static ActionsBuilder AddStartTrade(
        this ActionsBuilder builder,
        UnitEvaluator Vendor)
    {
      builder.Validate(Vendor);
      
      var element = ElementTool.Create<StartTrade>();
      element.Vendor = Vendor;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnequipAllItems"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(UnequipAllItems))]
    public static ActionsBuilder AddUnequipAllItems(
        this ActionsBuilder builder,
        UnitEvaluator Target,
        ItemsCollectionEvaluator DestinationContainer,
        Boolean Silent)
    {
      builder.Validate(Target);
      builder.Validate(DestinationContainer);
      builder.Validate(Silent);
      
      var element = ElementTool.Create<UnequipAllItems>();
      element.Target = Target;
      element.DestinationContainer = DestinationContainer;
      element.Silent = Silent;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnequipItem"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Item"><see cref="BlueprintItem"/></param>
    [Generated]
    [Implements(typeof(UnequipItem))]
    public static ActionsBuilder AddUnequipItem(
        this ActionsBuilder builder,
        UnitEvaluator Unit,
        ItemsCollectionEvaluator DestinationContainer,
        Boolean Silent,
        string m_Item,
        Boolean All)
    {
      builder.Validate(Unit);
      builder.Validate(DestinationContainer);
      builder.Validate(Silent);
      builder.Validate(All);
      
      var element = ElementTool.Create<UnequipItem>();
      element.Unit = Unit;
      element.DestinationContainer = DestinationContainer;
      element.Silent = Silent;
      element.m_Item = BlueprintTool.GetRef<BlueprintItemReference>(m_Item);
      element.All = All;
      return builder.Add(element);
    }
  }
}
