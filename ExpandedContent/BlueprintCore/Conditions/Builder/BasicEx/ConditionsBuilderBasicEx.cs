using BlueprintCore.Blueprints;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Utils;
using Kingmaker.Assets.Designers.EventConditionActionSystem.Conditions;
using Kingmaker.Blueprints;
using Kingmaker.Designers;
using Kingmaker.Designers.EventConditionActionSystem.Conditions;
using Kingmaker.Designers.EventConditionActionSystem.NamedParameters;
using Kingmaker.ElementsSystem;
using Kingmaker.UnitLogic.Buffs.Conditions;
using System;
namespace BlueprintCore.Conditions.Builder.BasicEx
{
  /// <summary>
  /// Extension to <see cref="ConditionsBuilder"/> for most game mechanics related conditions not included in
  /// <see cref="ContextEx.ConditionsBuilderContextEx">ContextEx</see>.
  /// </summary>
  /// <inheritdoc cref="ConditionsBuilder"/>
  public static class ConditionsBuilderBasicEx
  {
    //----- Auto Generated -----//



    /// <summary>
    /// Adds <see cref="DualCompanionInactive"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(DualCompanionInactive))]
    public static ConditionsBuilder AddDualCompanionInactive(
        this ConditionsBuilder builder,
        UnitEvaluator Unit,
        bool negate = false)
    {
      builder.Validate(Unit);
      
      var element = ElementTool.Create<DualCompanionInactive>();
      element.Unit = Unit;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="BuffConditionCheckRoundNumber"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(BuffConditionCheckRoundNumber))]
    public static ConditionsBuilder AddBuffConditionCheckRoundNumber(
        this ConditionsBuilder builder,
        Int32 RoundNumber,
        bool negate = false)
    {
      builder.Validate(RoundNumber);
      
      var element = ElementTool.Create<BuffConditionCheckRoundNumber>();
      element.RoundNumber = RoundNumber;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="CheckConditionsHolder"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="ConditionsHolder"><see cref="ConditionsHolder"/></param>
    [Generated]
    [Implements(typeof(CheckConditionsHolder))]
    public static ConditionsBuilder AddCheckConditionsHolder(
        this ConditionsBuilder builder,
        string ConditionsHolder,
        ParametrizedContextSetter Parameters,
        bool negate = false)
    {
      builder.Validate(Parameters);
      
      var element = ElementTool.Create<CheckConditionsHolder>();
      element.ConditionsHolder = BlueprintTool.GetRef<ConditionsReference>(ConditionsHolder);
      element.Parameters = Parameters;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="CheckItemCondition"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_TargetItem"><see cref="BlueprintItem"/></param>
    [Generated]
    [Implements(typeof(CheckItemCondition))]
    public static ConditionsBuilder AddCheckItemCondition(
        this ConditionsBuilder builder,
        string m_TargetItem,
        CheckItemCondition.RequiredState m_RequiredState,
        UnitEvaluator m_UnitEvaluator,
        bool negate = false)
    {
      builder.Validate(m_RequiredState);
      builder.Validate(m_UnitEvaluator);
      
      var element = ElementTool.Create<CheckItemCondition>();
      element.m_TargetItem = BlueprintTool.GetRef<BlueprintItemReference>(m_TargetItem);
      element.m_RequiredState = m_RequiredState;
      element.m_UnitEvaluator = m_UnitEvaluator;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="CompanionInParty"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_companion"><see cref="BlueprintUnit"/></param>
    [Generated]
    [Implements(typeof(CompanionInParty))]
    public static ConditionsBuilder AddCompanionInParty(
        this ConditionsBuilder builder,
        string m_companion,
        Boolean MatchWhenActive,
        Boolean MatchWhenDetached,
        Boolean MatchWhenRemote,
        Boolean MatchWhenDead,
        Boolean MatchWhenEx,
        bool negate = false)
    {
      builder.Validate(MatchWhenActive);
      builder.Validate(MatchWhenDetached);
      builder.Validate(MatchWhenRemote);
      builder.Validate(MatchWhenDead);
      builder.Validate(MatchWhenEx);
      
      var element = ElementTool.Create<CompanionInParty>();
      element.m_companion = BlueprintTool.GetRef<BlueprintUnitReference>(m_companion);
      element.MatchWhenActive = MatchWhenActive;
      element.MatchWhenDetached = MatchWhenDetached;
      element.MatchWhenRemote = MatchWhenRemote;
      element.MatchWhenDead = MatchWhenDead;
      element.MatchWhenEx = MatchWhenEx;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="CompanionIsDead"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_companion"><see cref="BlueprintUnit"/></param>
    [Generated]
    [Implements(typeof(CompanionIsDead))]
    public static ConditionsBuilder AddCompanionIsDead(
        this ConditionsBuilder builder,
        string m_companion,
        Boolean anyCompanion,
        bool negate = false)
    {
      builder.Validate(anyCompanion);
      
      var element = ElementTool.Create<CompanionIsDead>();
      element.m_companion = BlueprintTool.GetRef<BlueprintUnitReference>(m_companion);
      element.anyCompanion = anyCompanion;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="CompanionIsLost"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Companion"><see cref="BlueprintUnit"/></param>
    [Generated]
    [Implements(typeof(CompanionIsLost))]
    public static ConditionsBuilder AddCompanionIsLost(
        this ConditionsBuilder builder,
        string m_Companion,
        bool negate = false)
    {
      
      var element = ElementTool.Create<CompanionIsLost>();
      element.m_Companion = BlueprintTool.GetRef<BlueprintUnitReference>(m_Companion);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="CompanionIsUnconscious"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="companion"><see cref="BlueprintUnit"/></param>
    [Generated]
    [Implements(typeof(CompanionIsUnconscious))]
    public static ConditionsBuilder AddCompanionIsUnconscious(
        this ConditionsBuilder builder,
        string companion,
        Boolean anyCompanion,
        bool negate = false)
    {
      builder.Validate(anyCompanion);
      
      var element = ElementTool.Create<CompanionIsUnconscious>();
      element.companion = BlueprintTool.GetRef<BlueprintUnitReference>(companion);
      element.anyCompanion = anyCompanion;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="CheckLos"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(CheckLos))]
    public static ConditionsBuilder AddCheckLos(
        this ConditionsBuilder builder,
        PositionEvaluator Point1,
        PositionEvaluator Point2,
        bool negate = false)
    {
      builder.Validate(Point1);
      builder.Validate(Point2);
      
      var element = ElementTool.Create<CheckLos>();
      element.Point1 = Point1;
      element.Point2 = Point2;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="HasBuff"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Buff"><see cref="BlueprintBuff"/></param>
    [Generated]
    [Implements(typeof(HasBuff))]
    public static ConditionsBuilder AddHasBuff(
        this ConditionsBuilder builder,
        UnitEvaluator Target,
        string m_Buff,
        bool negate = false)
    {
      builder.Validate(Target);
      
      var element = ElementTool.Create<HasBuff>();
      element.Target = Target;
      element.m_Buff = BlueprintTool.GetRef<BlueprintBuffReference>(m_Buff);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="HasFact"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Fact"><see cref="BlueprintUnitFact"/></param>
    [Generated]
    [Implements(typeof(HasFact))]
    public static ConditionsBuilder AddHasFact(
        this ConditionsBuilder builder,
        UnitEvaluator Unit,
        string m_Fact,
        bool negate = false)
    {
      builder.Validate(Unit);
      
      var element = ElementTool.Create<HasFact>();
      element.Unit = Unit;
      element.m_Fact = BlueprintTool.GetRef<BlueprintUnitFactReference>(m_Fact);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="IsEnemy"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(IsEnemy))]
    public static ConditionsBuilder AddIsEnemy(
        this ConditionsBuilder builder,
        UnitEvaluator FirstUnit,
        UnitEvaluator SecondUnit,
        bool negate = false)
    {
      builder.Validate(FirstUnit);
      builder.Validate(SecondUnit);
      
      var element = ElementTool.Create<IsEnemy>();
      element.FirstUnit = FirstUnit;
      element.SecondUnit = SecondUnit;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="IsInCombat"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(IsInCombat))]
    public static ConditionsBuilder AddIsInCombat(
        this ConditionsBuilder builder,
        UnitEvaluator Unit,
        Boolean Player,
        bool negate = false)
    {
      builder.Validate(Unit);
      builder.Validate(Player);
      
      var element = ElementTool.Create<IsInCombat>();
      element.Unit = Unit;
      element.Player = Player;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="IsInTurnBasedCombat"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(IsInTurnBasedCombat))]
    public static ConditionsBuilder AddIsInTurnBasedCombat(
        this ConditionsBuilder builder,
        bool negate = false)
    {
      
      var element = ElementTool.Create<IsInTurnBasedCombat>();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="IsPartyMember"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(IsPartyMember))]
    public static ConditionsBuilder AddIsPartyMember(
        this ConditionsBuilder builder,
        UnitEvaluator Unit,
        bool negate = false)
    {
      builder.Validate(Unit);
      
      var element = ElementTool.Create<IsPartyMember>();
      element.Unit = Unit;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="IsUnconscious"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(IsUnconscious))]
    public static ConditionsBuilder AddIsUnconscious(
        this ConditionsBuilder builder,
        UnitEvaluator Unit,
        bool negate = false)
    {
      builder.Validate(Unit);
      
      var element = ElementTool.Create<IsUnconscious>();
      element.Unit = Unit;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="IsUnitLevelLessThan"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(IsUnitLevelLessThan))]
    public static ConditionsBuilder AddIsUnitLevelLessThan(
        this ConditionsBuilder builder,
        UnitEvaluator Unit,
        Int32 Level,
        Boolean CheckExperience,
        bool negate = false)
    {
      builder.Validate(Unit);
      builder.Validate(Level);
      builder.Validate(CheckExperience);
      
      var element = ElementTool.Create<IsUnitLevelLessThan>();
      element.Unit = Unit;
      element.Level = Level;
      element.CheckExperience = CheckExperience;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ItemBlueprint"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="Blueprint"><see cref="BlueprintItem"/></param>
    [Generated]
    [Implements(typeof(ItemBlueprint))]
    public static ConditionsBuilder AddItemBlueprint(
        this ConditionsBuilder builder,
        ItemEvaluator Item,
        string Blueprint,
        bool negate = false)
    {
      builder.Validate(Item);
      
      var element = ElementTool.Create<ItemBlueprint>();
      element.Item = Item;
      element.Blueprint = BlueprintTool.GetRef<BlueprintItemReference>(Blueprint);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ItemFromCollectionCondition"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ItemFromCollectionCondition))]
    public static ConditionsBuilder AddItemFromCollectionCondition(
        this ConditionsBuilder builder,
        ItemsCollectionEvaluator Items,
        Boolean Any,
        ConditionsBuilder Condition,
        bool negate = false)
    {
      builder.Validate(Items);
      builder.Validate(Any);
      
      var element = ElementTool.Create<ItemFromCollectionCondition>();
      element.Items = Items;
      element.Any = Any;
      element.Condition = Condition.Build();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ItemsEnough"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_ItemToCheck"><see cref="BlueprintItem"/></param>
    [Generated]
    [Implements(typeof(ItemsEnough))]
    public static ConditionsBuilder AddItemsEnough(
        this ConditionsBuilder builder,
        Boolean Money,
        string m_ItemToCheck,
        Int32 Quantity,
        bool negate = false)
    {
      builder.Validate(Money);
      builder.Validate(Quantity);
      
      var element = ElementTool.Create<ItemsEnough>();
      element.Money = Money;
      element.m_ItemToCheck = BlueprintTool.GetRef<BlueprintItemReference>(m_ItemToCheck);
      element.Quantity = Quantity;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="PartyCanUseAbility"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(PartyCanUseAbility))]
    public static ConditionsBuilder AddPartyCanUseAbility(
        this ConditionsBuilder builder,
        AbilitiesHelper.AbilityDescription Description,
        Boolean AllowItems,
        bool negate = false)
    {
      builder.Validate(Description);
      builder.Validate(AllowItems);
      
      var element = ElementTool.Create<PartyCanUseAbility>();
      element.Description = Description;
      element.AllowItems = AllowItems;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="PartyUnits"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(PartyUnits))]
    public static ConditionsBuilder AddPartyUnits(
        this ConditionsBuilder builder,
        Boolean Any,
        ConditionsBuilder Conditions,
        bool negate = false)
    {
      builder.Validate(Any);
      
      var element = ElementTool.Create<PartyUnits>();
      element.Any = Any;
      element.Conditions = Conditions.Build();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="PcFemale"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(PcFemale))]
    public static ConditionsBuilder AddPcFemale(
        this ConditionsBuilder builder,
        bool negate = false)
    {
      
      var element = ElementTool.Create<PcFemale>();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="PcMale"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(PcMale))]
    public static ConditionsBuilder AddPcMale(
        this ConditionsBuilder builder,
        bool negate = false)
    {
      
      var element = ElementTool.Create<PcMale>();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="PcRace"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(PcRace))]
    public static ConditionsBuilder AddPcRace(
        this ConditionsBuilder builder,
        Race Race,
        bool negate = false)
    {
      builder.Validate(Race);
      
      var element = ElementTool.Create<PcRace>();
      element.Race = Race;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SummonPoolExistsAndEmpty"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_SummonPool"><see cref="BlueprintSummonPool"/></param>
    [Generated]
    [Implements(typeof(SummonPoolExistsAndEmpty))]
    public static ConditionsBuilder AddSummonPoolExistsAndEmpty(
        this ConditionsBuilder builder,
        string m_SummonPool,
        bool negate = false)
    {
      
      var element = ElementTool.Create<SummonPoolExistsAndEmpty>();
      element.m_SummonPool = BlueprintTool.GetRef<BlueprintSummonPoolReference>(m_SummonPool);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnitIsDead"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(UnitIsDead))]
    public static ConditionsBuilder AddUnitIsDead(
        this ConditionsBuilder builder,
        UnitEvaluator Target,
        bool negate = false)
    {
      builder.Validate(Target);
      
      var element = ElementTool.Create<UnitIsDead>();
      element.Target = Target;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnitIsHidden"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(UnitIsHidden))]
    public static ConditionsBuilder AddUnitIsHidden(
        this ConditionsBuilder builder,
        UnitEvaluator Unit,
        bool negate = false)
    {
      builder.Validate(Unit);
      
      var element = ElementTool.Create<UnitIsHidden>();
      element.Unit = Unit;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnitBlueprint"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Blueprint"><see cref="BlueprintUnit"/></param>
    [Generated]
    [Implements(typeof(UnitBlueprint))]
    public static ConditionsBuilder AddUnitBlueprint(
        this ConditionsBuilder builder,
        UnitEvaluator Unit,
        string m_Blueprint,
        bool negate = false)
    {
      builder.Validate(Unit);
      
      var element = ElementTool.Create<UnitBlueprint>();
      element.Unit = Unit;
      element.m_Blueprint = BlueprintTool.GetRef<BlueprintUnitReference>(m_Blueprint);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnitClass"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Class"><see cref="BlueprintCharacterClass"/></param>
    [Generated]
    [Implements(typeof(UnitClass))]
    public static ConditionsBuilder AddUnitClass(
        this ConditionsBuilder builder,
        UnitEvaluator Unit,
        string m_Class,
        IntEvaluator MinLevel,
        bool negate = false)
    {
      builder.Validate(Unit);
      builder.Validate(MinLevel);
      
      var element = ElementTool.Create<UnitClass>();
      element.Unit = Unit;
      element.m_Class = BlueprintTool.GetRef<BlueprintCharacterClassReference>(m_Class);
      element.MinLevel = MinLevel;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnitEqual"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(UnitEqual))]
    public static ConditionsBuilder AddUnitEqual(
        this ConditionsBuilder builder,
        UnitEvaluator FirstUnit,
        UnitEvaluator SecondUnit,
        bool negate = false)
    {
      builder.Validate(FirstUnit);
      builder.Validate(SecondUnit);
      
      var element = ElementTool.Create<UnitEqual>();
      element.FirstUnit = FirstUnit;
      element.SecondUnit = SecondUnit;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnitFromSpawnerIsDead"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(UnitFromSpawnerIsDead))]
    public static ConditionsBuilder AddUnitFromSpawnerIsDead(
        this ConditionsBuilder builder,
        EntityReference Target,
        bool negate = false)
    {
      builder.Validate(Target);
      
      var element = ElementTool.Create<UnitFromSpawnerIsDead>();
      element.Target = Target;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnitFromSummonPool"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_SummonPool"><see cref="BlueprintSummonPool"/></param>
    [Generated]
    [Implements(typeof(UnitFromSummonPool))]
    public static ConditionsBuilder AddUnitFromSummonPool(
        this ConditionsBuilder builder,
        UnitEvaluator Unit,
        string m_SummonPool,
        bool negate = false)
    {
      builder.Validate(Unit);
      
      var element = ElementTool.Create<UnitFromSummonPool>();
      element.Unit = Unit;
      element.m_SummonPool = BlueprintTool.GetRef<BlueprintSummonPoolReference>(m_SummonPool);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnitGender"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(UnitGender))]
    public static ConditionsBuilder AddUnitGender(
        this ConditionsBuilder builder,
        UnitEvaluator Unit,
        Gender Gender,
        bool negate = false)
    {
      builder.Validate(Unit);
      builder.Validate(Gender);
      
      var element = ElementTool.Create<UnitGender>();
      element.Unit = Unit;
      element.Gender = Gender;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnitIsNull"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(UnitIsNull))]
    public static ConditionsBuilder AddUnitIsNull(
        this ConditionsBuilder builder,
        UnitEvaluator Target,
        bool negate = false)
    {
      builder.Validate(Target);
      
      var element = ElementTool.Create<UnitIsNull>();
      element.Target = Target;
      element.Not = negate;
      return builder.Add(element);
    }
  }
}
