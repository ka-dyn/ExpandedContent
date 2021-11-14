using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints;
using BlueprintCore.Utils;
using Kingmaker.AreaLogic;
using Kingmaker.AreaLogic.Etudes;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.EventConditionActionSystem.NamedParameters;
using Kingmaker.Designers.Quests.Common;
using Kingmaker.ElementsSystem;
using Kingmaker.Enums;
using Kingmaker.Globalmap.Blueprints;
using Kingmaker.Localization;
using Kingmaker.UI;
using Kingmaker.UnitLogic.Alignments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueprintCore.Actions.Builder.StoryEx
{
  /// <summary>
  /// Extension to <see cref="ActionsBuilder"/> for actions related to the story such as companion stories, quests,
  /// name changes, and etudes.
  /// </summary>
  /// <inheritdoc cref="ActionsBuilder"/>
  public static class ActionsBuilderStoryEx
  {
    //----- Kingmaker.Designers.EventConditionActionSystem.Actions -----//

    /// <summary>
    /// Adds <see cref="Kingmaker.Designers.EventConditionActionSystem.Actions.CompleteEtude">CompleteEtude</see>
    /// </summary>
    /// 
    /// <param name="etude"><see cref="BlueprintEtude"/></param>
    [Implements(typeof(CompleteEtude))]
    public static ActionsBuilder CompleteEtude(
        this ActionsBuilder builder, string etude, BlueprintEvaluator evaluator = null)
    {
      var completeEtude = ElementTool.Create<CompleteEtude>();
      completeEtude.Etude = BlueprintTool.GetRef<BlueprintEtudeReference>(etude);
      if (evaluator != null)
      {
        completeEtude.EtudeEvaluator = evaluator;
        completeEtude.Evaluate = true;
      }
      return builder.Add(completeEtude);
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.Designers.EventConditionActionSystem.Actions.ChangeRomance">ChangeRomance</see>
    /// </summary>
    /// 
    /// <param name="romance"><see cref="BlueprintRomanceCounter"/></param>
    [Implements(typeof(ChangeRomance))]
    public static ActionsBuilder ChangeRomance(
       this ActionsBuilder builder, string romance, IntEvaluator value)
    {
      builder.Validate(value);

      var changeRomance = ElementTool.Create<ChangeRomance>();
      changeRomance.m_Romance = BlueprintTool.GetRef<BlueprintRomanceCounterReference>(romance);
      changeRomance.ValueEvaluator = value;
      return builder.Add(changeRomance);
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.Designers.EventConditionActionSystem.Actions.ChangeUnitName">ChangeUnitName</see>
    /// </summary>
    [Implements(typeof(ChangeUnitName))]
    public static ActionsBuilder ChangeUnitName(
        this ActionsBuilder builder,
        UnitEvaluator unit,
        LocalizedString name,
        bool appendName = false)
    {
      builder.Validate(unit);

      var changeName = ElementTool.Create<ChangeUnitName>();
      changeName.Unit = unit;
      changeName.NewName = name;
      changeName.AddToTheName = appendName;
      return builder.Add(changeName);
    }

    /// <inheritdoc cref="ChangeUnitName"/>
    [Implements(typeof(ChangeUnitName))]
    public static ActionsBuilder ResetUnitName(
        this ActionsBuilder builder, UnitEvaluator unit)
    {
      builder.Validate(unit);

      var changeName = ElementTool.Create<ChangeUnitName>();
      changeName.Unit = unit;
      changeName.ReturnTheOldName = true;
      return builder.Add(changeName);
    }

    //----- Auto Generated -----//



    /// <summary>
    /// Adds <see cref="AlignmentSelector"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(AlignmentSelector))]
    public static ActionsBuilder AddAlignmentSelector(
        this ActionsBuilder builder,
        Boolean SelectClosest,
        AlignmentSelector.ActionAndCondition LawfulGood,
        AlignmentSelector.ActionAndCondition NeutralGood,
        AlignmentSelector.ActionAndCondition ChaoticGood,
        AlignmentSelector.ActionAndCondition LawfulNeutral,
        AlignmentSelector.ActionAndCondition TrueNeutral,
        AlignmentSelector.ActionAndCondition ChaoticNeutral,
        AlignmentSelector.ActionAndCondition LawfulEvil,
        AlignmentSelector.ActionAndCondition NeutralEvil,
        AlignmentSelector.ActionAndCondition ChaoticEvil,
        Dictionary<Alignment,AlignmentSelector.ActionAndCondition> m_ActionsByAlignment)
    {
      builder.Validate(SelectClosest);
      builder.Validate(LawfulGood);
      builder.Validate(NeutralGood);
      builder.Validate(ChaoticGood);
      builder.Validate(LawfulNeutral);
      builder.Validate(TrueNeutral);
      builder.Validate(ChaoticNeutral);
      builder.Validate(LawfulEvil);
      builder.Validate(NeutralEvil);
      builder.Validate(ChaoticEvil);
      foreach (var item in m_ActionsByAlignment)
      {
        builder.Validate(item);
      }
      
      var element = ElementTool.Create<AlignmentSelector>();
      element.SelectClosest = SelectClosest;
      element.LawfulGood = LawfulGood;
      element.NeutralGood = NeutralGood;
      element.ChaoticGood = ChaoticGood;
      element.LawfulNeutral = LawfulNeutral;
      element.TrueNeutral = TrueNeutral;
      element.ChaoticNeutral = ChaoticNeutral;
      element.LawfulEvil = LawfulEvil;
      element.NeutralEvil = NeutralEvil;
      element.ChaoticEvil = ChaoticEvil;
      element.m_ActionsByAlignment = m_ActionsByAlignment;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="DismissAllCompanions"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(DismissAllCompanions))]
    public static ActionsBuilder AddDismissAllCompanions(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<DismissAllCompanions>());
    }

    /// <summary>
    /// Adds <see cref="GiveObjective"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Objective"><see cref="BlueprintQuestObjective"/></param>
    [Generated]
    [Implements(typeof(GiveObjective))]
    public static ActionsBuilder AddGiveObjective(
        this ActionsBuilder builder,
        string m_Objective)
    {
      
      var element = ElementTool.Create<GiveObjective>();
      element.m_Objective = BlueprintTool.GetRef<BlueprintQuestObjectiveReference>(m_Objective);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="HideUnit"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(HideUnit))]
    public static ActionsBuilder AddHideUnit(
        this ActionsBuilder builder,
        UnitEvaluator Target,
        Boolean Unhide,
        Boolean Fade)
    {
      builder.Validate(Target);
      builder.Validate(Unhide);
      builder.Validate(Fade);
      
      var element = ElementTool.Create<HideUnit>();
      element.Target = Target;
      element.Unhide = Unhide;
      element.Fade = Fade;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="HideWeapons"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(HideWeapons))]
    public static ActionsBuilder AddHideWeapons(
        this ActionsBuilder builder,
        UnitEvaluator Target,
        Boolean Hide)
    {
      builder.Validate(Target);
      builder.Validate(Hide);
      
      var element = ElementTool.Create<HideWeapons>();
      element.Target = Target;
      element.Hide = Hide;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="IncrementFlagValue"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Flag"><see cref="BlueprintUnlockableFlag"/></param>
    [Generated]
    [Implements(typeof(IncrementFlagValue))]
    public static ActionsBuilder AddIncrementFlagValue(
        this ActionsBuilder builder,
        string m_Flag,
        IntEvaluator Value,
        Boolean UnlockIfNot)
    {
      builder.Validate(Value);
      builder.Validate(UnlockIfNot);
      
      var element = ElementTool.Create<IncrementFlagValue>();
      element.m_Flag = BlueprintTool.GetRef<BlueprintUnlockableFlagReference>(m_Flag);
      element.Value = Value;
      element.UnlockIfNot = UnlockIfNot;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="InterruptAllActions"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(InterruptAllActions))]
    public static ActionsBuilder AddInterruptAllActions(
        this ActionsBuilder builder,
        UnitEvaluator m_Unit)
    {
      builder.Validate(m_Unit);
      
      var element = ElementTool.Create<InterruptAllActions>();
      element.m_Unit = m_Unit;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="LockAlignment"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(LockAlignment))]
    public static ActionsBuilder AddLockAlignment(
        this ActionsBuilder builder,
        UnitEvaluator Unit,
        AlignmentMaskType AlignmentMask,
        Alignment TargetAlignment)
    {
      builder.Validate(Unit);
      builder.Validate(AlignmentMask);
      builder.Validate(TargetAlignment);
      
      var element = ElementTool.Create<LockAlignment>();
      element.Unit = Unit;
      element.AlignmentMask = AlignmentMask;
      element.TargetAlignment = TargetAlignment;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="LockFlag"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Flag"><see cref="BlueprintUnlockableFlag"/></param>
    [Generated]
    [Implements(typeof(LockFlag))]
    public static ActionsBuilder AddLockFlag(
        this ActionsBuilder builder,
        string m_Flag)
    {
      
      var element = ElementTool.Create<LockFlag>();
      element.m_Flag = BlueprintTool.GetRef<BlueprintUnlockableFlagReference>(m_Flag);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="LockRomance"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Romance"><see cref="BlueprintRomanceCounter"/></param>
    [Generated]
    [Implements(typeof(LockRomance))]
    public static ActionsBuilder AddLockRomance(
        this ActionsBuilder builder,
        string m_Romance)
    {
      
      var element = ElementTool.Create<LockRomance>();
      element.m_Romance = BlueprintTool.GetRef<BlueprintRomanceCounterReference>(m_Romance);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="MarkAnswersSelected"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Answers"><see cref="BlueprintAnswer"/></param>
    [Generated]
    [Implements(typeof(MarkAnswersSelected))]
    public static ActionsBuilder AddMarkAnswersSelected(
        this ActionsBuilder builder,
        string[] m_Answers)
    {
      
      var element = ElementTool.Create<MarkAnswersSelected>();
      element.m_Answers = m_Answers.Select(bp => BlueprintTool.GetRef<BlueprintAnswerReference>(bp)).ToArray();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="MarkCuesSeen"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Cues"><see cref="BlueprintCueBase"/></param>
    [Generated]
    [Implements(typeof(MarkCuesSeen))]
    public static ActionsBuilder AddMarkCuesSeen(
        this ActionsBuilder builder,
        string[] m_Cues)
    {
      
      var element = ElementTool.Create<MarkCuesSeen>();
      element.m_Cues = m_Cues.Select(bp => BlueprintTool.GetRef<BlueprintCueBaseReference>(bp)).ToArray();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="MoveAzataIslandToLocation"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_GlobalMap"><see cref="BlueprintGlobalMap"/></param>
    /// <param name="m_Location"><see cref="BlueprintGlobalMapPoint"/></param>
    [Generated]
    [Implements(typeof(MoveAzataIslandToLocation))]
    public static ActionsBuilder AddMoveAzataIslandToLocation(
        this ActionsBuilder builder,
        string m_GlobalMap,
        string m_Location)
    {
      
      var element = ElementTool.Create<MoveAzataIslandToLocation>();
      element.m_GlobalMap = BlueprintTool.GetRef<BlueprintGlobalMap.Reference>(m_GlobalMap);
      element.m_Location = BlueprintTool.GetRef<BlueprintGlobalMapPoint.Reference>(m_Location);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="MoveAzataIslandToNearestCrossroad"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_GlobalMap"><see cref="BlueprintGlobalMap"/></param>
    [Generated]
    [Implements(typeof(MoveAzataIslandToNearestCrossroad))]
    public static ActionsBuilder AddMoveAzataIslandToNearestCrossroad(
        this ActionsBuilder builder,
        string m_GlobalMap)
    {
      
      var element = ElementTool.Create<MoveAzataIslandToNearestCrossroad>();
      element.m_GlobalMap = BlueprintTool.GetRef<BlueprintGlobalMap.Reference>(m_GlobalMap);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="OverrideUnitReturnPosition"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(OverrideUnitReturnPosition))]
    public static ActionsBuilder AddOverrideUnitReturnPosition(
        this ActionsBuilder builder,
        UnitEvaluator Unit,
        PositionEvaluator Position,
        FloatEvaluator Orientation)
    {
      builder.Validate(Unit);
      builder.Validate(Position);
      builder.Validate(Orientation);
      
      var element = ElementTool.Create<OverrideUnitReturnPosition>();
      element.Unit = Unit;
      element.Position = Position;
      element.Orientation = Orientation;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="PartyMembersAttach"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(PartyMembersAttach))]
    public static ActionsBuilder AddPartyMembersAttach(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<PartyMembersAttach>());
    }

    /// <summary>
    /// Adds <see cref="PartyMembersDetach"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_DetachAllExcept"><see cref="BlueprintUnit"/></param>
    [Generated]
    [Implements(typeof(PartyMembersDetach))]
    public static ActionsBuilder AddPartyMembersDetach(
        this ActionsBuilder builder,
        string[] m_DetachAllExcept,
        Boolean m_RestrictPartySize,
        Int32 m_PartySize,
        ActionsBuilder AfterDetach)
    {
      builder.Validate(m_RestrictPartySize);
      builder.Validate(m_PartySize);
      
      var element = ElementTool.Create<PartyMembersDetach>();
      element.m_DetachAllExcept = m_DetachAllExcept.Select(bp => BlueprintTool.GetRef<BlueprintUnitReference>(bp)).ToArray();
      element.m_RestrictPartySize = m_RestrictPartySize;
      element.m_PartySize = m_PartySize;
      element.AfterDetach = AfterDetach.Build();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="PartyMembersDetachEvaluated"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(PartyMembersDetachEvaluated))]
    public static ActionsBuilder AddPartyMembersDetachEvaluated(
        this ActionsBuilder builder,
        UnitEvaluator[] DetachThese,
        ActionsBuilder AfterDetach,
        Boolean m_RestrictPartySize,
        Int32 m_PartySize)
    {
      foreach (var item in DetachThese)
      {
        builder.Validate(item);
      }
      builder.Validate(m_RestrictPartySize);
      builder.Validate(m_PartySize);
      
      var element = ElementTool.Create<PartyMembersDetachEvaluated>();
      element.DetachThese = DetachThese;
      element.AfterDetach = AfterDetach.Build();
      element.m_RestrictPartySize = m_RestrictPartySize;
      element.m_PartySize = m_PartySize;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="PartyMembersSwapAttachedAndDetached"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(PartyMembersSwapAttachedAndDetached))]
    public static ActionsBuilder AddPartyMembersSwapAttachedAndDetached(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<PartyMembersSwapAttachedAndDetached>());
    }

    /// <summary>
    /// Adds <see cref="Recruit"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(Recruit))]
    public static ActionsBuilder AddRecruit(
        this ActionsBuilder builder,
        Recruit.RecruitData[] Recruited,
        Boolean AddToParty,
        Boolean MatchPlayerXpExactly,
        ActionsBuilder OnRecruit,
        ActionsBuilder OnRecruitImmediate)
    {
      foreach (var item in Recruited)
      {
        builder.Validate(item);
      }
      builder.Validate(AddToParty);
      builder.Validate(MatchPlayerXpExactly);
      
      var element = ElementTool.Create<Recruit>();
      element.Recruited = Recruited;
      element.AddToParty = AddToParty;
      element.MatchPlayerXpExactly = MatchPlayerXpExactly;
      element.OnRecruit = OnRecruit.Build();
      element.OnRecruitImmediate = OnRecruitImmediate.Build();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RecruitInactive"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_CompanionBlueprint"><see cref="BlueprintUnit"/></param>
    [Generated]
    [Implements(typeof(RecruitInactive))]
    public static ActionsBuilder AddRecruitInactive(
        this ActionsBuilder builder,
        string m_CompanionBlueprint,
        ActionsBuilder OnRecruit)
    {
      
      var element = ElementTool.Create<RecruitInactive>();
      element.m_CompanionBlueprint = BlueprintTool.GetRef<BlueprintUnitReference>(m_CompanionBlueprint);
      element.OnRecruit = OnRecruit.Build();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RelockInteraction"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(RelockInteraction))]
    public static ActionsBuilder AddRelockInteraction(
        this ActionsBuilder builder,
        MapObjectEvaluator MapObject)
    {
      builder.Validate(MapObject);
      
      var element = ElementTool.Create<RelockInteraction>();
      element.MapObject = MapObject;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RemoveMythicLevels"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(RemoveMythicLevels))]
    public static ActionsBuilder AddRemoveMythicLevels(
        this ActionsBuilder builder,
        Int32 Levels)
    {
      builder.Validate(Levels);
      
      var element = ElementTool.Create<RemoveMythicLevels>();
      element.Levels = Levels;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ReplaceAllMythicLevelsWithMythicHeroLevels"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ReplaceAllMythicLevelsWithMythicHeroLevels))]
    public static ActionsBuilder AddReplaceAllMythicLevelsWithMythicHeroLevels(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ReplaceAllMythicLevelsWithMythicHeroLevels>());
    }

    /// <summary>
    /// Adds <see cref="ReplaceFeatureInProgression"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Remove"><see cref="BlueprintFeature"/></param>
    /// <param name="m_Add"><see cref="BlueprintFeature"/></param>
    [Generated]
    [Implements(typeof(ReplaceFeatureInProgression))]
    public static ActionsBuilder AddReplaceFeatureInProgression(
        this ActionsBuilder builder,
        UnitEvaluator Unit,
        string m_Remove,
        string m_Add)
    {
      builder.Validate(Unit);
      
      var element = ElementTool.Create<ReplaceFeatureInProgression>();
      element.Unit = Unit;
      element.m_Remove = BlueprintTool.GetRef<BlueprintFeatureReference>(m_Remove);
      element.m_Add = BlueprintTool.GetRef<BlueprintFeatureReference>(m_Add);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ResetQuest"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Quest"><see cref="BlueprintQuest"/></param>
    /// <param name="m_ObjectiveToStart"><see cref="BlueprintQuestObjective"/></param>
    /// <param name="m_ObjectivesToReset"><see cref="BlueprintQuestObjective"/></param>
    [Generated]
    [Implements(typeof(ResetQuest))]
    public static ActionsBuilder AddResetQuest(
        this ActionsBuilder builder,
        string m_Quest,
        string m_ObjectiveToStart,
        string[] m_ObjectivesToReset)
    {
      
      var element = ElementTool.Create<ResetQuest>();
      element.m_Quest = BlueprintTool.GetRef<BlueprintQuestReference>(m_Quest);
      element.m_ObjectiveToStart = BlueprintTool.GetRef<BlueprintQuestObjectiveReference>(m_ObjectiveToStart);
      element.m_ObjectivesToReset = m_ObjectivesToReset.Select(bp => BlueprintTool.GetRef<BlueprintQuestObjectiveReference>(bp)).ToArray();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ResetQuestObjective"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Objective"><see cref="BlueprintQuestObjective"/></param>
    [Generated]
    [Implements(typeof(ResetQuestObjective))]
    public static ActionsBuilder AddResetQuestObjective(
        this ActionsBuilder builder,
        string m_Objective)
    {
      
      var element = ElementTool.Create<ResetQuestObjective>();
      element.m_Objective = BlueprintTool.GetRef<BlueprintQuestObjectiveReference>(m_Objective);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RespecCompanion"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(RespecCompanion))]
    public static ActionsBuilder AddRespecCompanion(
        this ActionsBuilder builder,
        Boolean ForFree,
        Boolean MatchPlayerXpExactly)
    {
      builder.Validate(ForFree);
      builder.Validate(MatchPlayerXpExactly);
      
      var element = ElementTool.Create<RespecCompanion>();
      element.ForFree = ForFree;
      element.MatchPlayerXpExactly = MatchPlayerXpExactly;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RomanceSetMaximum"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Romance"><see cref="BlueprintRomanceCounter"/></param>
    [Generated]
    [Implements(typeof(RomanceSetMaximum))]
    public static ActionsBuilder AddRomanceSetMaximum(
        this ActionsBuilder builder,
        string m_Romance,
        IntEvaluator ValueEvaluator)
    {
      builder.Validate(ValueEvaluator);
      
      var element = ElementTool.Create<RomanceSetMaximum>();
      element.m_Romance = BlueprintTool.GetRef<BlueprintRomanceCounterReference>(m_Romance);
      element.ValueEvaluator = ValueEvaluator;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RomanceSetMinimum"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Romance"><see cref="BlueprintRomanceCounter"/></param>
    [Generated]
    [Implements(typeof(RomanceSetMinimum))]
    public static ActionsBuilder AddRomanceSetMinimum(
        this ActionsBuilder builder,
        string m_Romance,
        IntEvaluator ValueEvaluator)
    {
      builder.Validate(ValueEvaluator);
      
      var element = ElementTool.Create<RomanceSetMinimum>();
      element.m_Romance = BlueprintTool.GetRef<BlueprintRomanceCounterReference>(m_Romance);
      element.ValueEvaluator = ValueEvaluator;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SetDialogPosition"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(SetDialogPosition))]
    public static ActionsBuilder AddSetDialogPosition(
        this ActionsBuilder builder,
        PositionEvaluator Position)
    {
      builder.Validate(Position);
      
      var element = ElementTool.Create<SetDialogPosition>();
      element.Position = Position;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SetMythicLevelForMainCharacter"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(SetMythicLevelForMainCharacter))]
    public static ActionsBuilder AddSetMythicLevelForMainCharacter(
        this ActionsBuilder builder,
        Int32 DesireLevel)
    {
      builder.Validate(DesireLevel);
      
      var element = ElementTool.Create<SetMythicLevelForMainCharacter>();
      element.DesireLevel = DesireLevel;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SetObjectiveStatus"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Objective"><see cref="BlueprintQuestObjective"/></param>
    [Generated]
    [Implements(typeof(SetObjectiveStatus))]
    public static ActionsBuilder AddSetObjectiveStatus(
        this ActionsBuilder builder,
        SummonPoolCountTrigger.ObjectiveStatus Status,
        string m_Objective,
        Boolean StartObjectiveIfNone)
    {
      builder.Validate(Status);
      builder.Validate(StartObjectiveIfNone);
      
      var element = ElementTool.Create<SetObjectiveStatus>();
      element.Status = Status;
      element.m_Objective = BlueprintTool.GetRef<BlueprintQuestObjectiveReference>(m_Objective);
      element.StartObjectiveIfNone = StartObjectiveIfNone;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SetPortrait"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Portrait"><see cref="BlueprintPortrait"/></param>
    [Generated]
    [Implements(typeof(SetPortrait))]
    public static ActionsBuilder AddSetPortrait(
        this ActionsBuilder builder,
        UnitEvaluator Unit,
        string m_Portrait)
    {
      builder.Validate(Unit);
      
      var element = ElementTool.Create<SetPortrait>();
      element.Unit = Unit;
      element.m_Portrait = BlueprintTool.GetRef<BlueprintPortraitReference>(m_Portrait);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ShiftAlignment"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ShiftAlignment))]
    public static ActionsBuilder AddShiftAlignment(
        this ActionsBuilder builder,
        UnitEvaluator Unit,
        AlignmentShiftDirection Alignment,
        IntEvaluator Amount)
    {
      builder.Validate(Unit);
      builder.Validate(Alignment);
      builder.Validate(Amount);
      
      var element = ElementTool.Create<ShiftAlignment>();
      element.Unit = Unit;
      element.Alignment = Alignment;
      element.Amount = Amount;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ShowDialogBox"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ShowDialogBox))]
    public static ActionsBuilder AddShowDialogBox(
        this ActionsBuilder builder,
        LocalizedString Text,
        ParametrizedContextSetter Parameters,
        ActionsBuilder OnAccept,
        ActionsBuilder OnCancel)
    {
      builder.Validate(Text);
      builder.Validate(Parameters);
      
      var element = ElementTool.Create<ShowDialogBox>();
      element.Text = Text;
      element.Parameters = Parameters;
      element.OnAccept = OnAccept.Build();
      element.OnCancel = OnCancel.Build();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ShowMessageBox"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ShowMessageBox))]
    public static ActionsBuilder AddShowMessageBox(
        this ActionsBuilder builder,
        LocalizedString Text,
        ActionsBuilder OnClose,
        Int32 WaitTime)
    {
      builder.Validate(Text);
      builder.Validate(WaitTime);
      
      var element = ElementTool.Create<ShowMessageBox>();
      element.Text = Text;
      element.OnClose = OnClose.Build();
      element.WaitTime = WaitTime;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ShowUIWarning"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ShowUIWarning))]
    public static ActionsBuilder AddShowUIWarning(
        this ActionsBuilder builder,
        WarningNotificationType Type,
        LocalizedString String)
    {
      builder.Validate(Type);
      builder.Validate(String);
      
      var element = ElementTool.Create<ShowUIWarning>();
      element.Type = Type;
      element.String = String;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SplitUnitGroup"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(SplitUnitGroup))]
    public static ActionsBuilder AddSplitUnitGroup(
        this ActionsBuilder builder,
        UnitEvaluator Target)
    {
      builder.Validate(Target);
      
      var element = ElementTool.Create<SplitUnitGroup>();
      element.Target = Target;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="StartCombat"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(StartCombat))]
    public static ActionsBuilder AddStartCombat(
        this ActionsBuilder builder,
        UnitEvaluator Unit1,
        UnitEvaluator Unit2)
    {
      builder.Validate(Unit1);
      builder.Validate(Unit2);
      
      var element = ElementTool.Create<StartCombat>();
      element.Unit1 = Unit1;
      element.Unit2 = Unit2;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="StartDialog"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Dialogue"><see cref="BlueprintDialog"/></param>
    [Generated]
    [Implements(typeof(StartDialog))]
    public static ActionsBuilder AddStartDialog(
        this ActionsBuilder builder,
        UnitEvaluator DialogueOwner,
        string m_Dialogue,
        BlueprintEvaluator DialogEvaluator,
        LocalizedString SpeakerName)
    {
      builder.Validate(DialogueOwner);
      builder.Validate(DialogEvaluator);
      builder.Validate(SpeakerName);
      
      var element = ElementTool.Create<StartDialog>();
      element.DialogueOwner = DialogueOwner;
      element.m_Dialogue = BlueprintTool.GetRef<BlueprintDialogReference>(m_Dialogue);
      element.DialogEvaluator = DialogEvaluator;
      element.SpeakerName = SpeakerName;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="StartEncounter"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Encounter"><see cref="BlueprintRandomEncounter"/></param>
    [Generated]
    [Implements(typeof(StartEncounter))]
    public static ActionsBuilder AddStartEncounter(
        this ActionsBuilder builder,
        string m_Encounter)
    {
      
      var element = ElementTool.Create<StartEncounter>();
      element.m_Encounter = BlueprintTool.GetRef<BlueprintRandomEncounterReference>(m_Encounter);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="StartEtude"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="Etude"><see cref="BlueprintEtude"/></param>
    [Generated]
    [Implements(typeof(StartEtude))]
    public static ActionsBuilder AddStartEtude(
        this ActionsBuilder builder,
        string Etude,
        BlueprintEvaluator EtudeEvaluator,
        Boolean Evaluate)
    {
      builder.Validate(EtudeEvaluator);
      builder.Validate(Evaluate);
      
      var element = ElementTool.Create<StartEtude>();
      element.Etude = BlueprintTool.GetRef<BlueprintEtudeReference>(Etude);
      element.EtudeEvaluator = EtudeEvaluator;
      element.Evaluate = Evaluate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SwitchAzataIsland"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_GlobalMap"><see cref="BlueprintGlobalMap"/></param>
    [Generated]
    [Implements(typeof(SwitchAzataIsland))]
    public static ActionsBuilder AddSwitchAzataIsland(
        this ActionsBuilder builder,
        string m_GlobalMap,
        Boolean IsOn)
    {
      builder.Validate(IsOn);
      
      var element = ElementTool.Create<SwitchAzataIsland>();
      element.m_GlobalMap = BlueprintTool.GetRef<BlueprintGlobalMap.Reference>(m_GlobalMap);
      element.IsOn = IsOn;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SwitchChapter"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(SwitchChapter))]
    public static ActionsBuilder AddSwitchChapter(
        this ActionsBuilder builder,
        Int32 Chapter)
    {
      builder.Validate(Chapter);
      
      var element = ElementTool.Create<SwitchChapter>();
      element.Chapter = Chapter;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SwitchDoor"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(SwitchDoor))]
    public static ActionsBuilder AddSwitchDoor(
        this ActionsBuilder builder,
        MapObjectEvaluator Door,
        Boolean UnlockIfLocked,
        Boolean CloseIfAlreadyOpen,
        Boolean OpenIfAlreadyClosed)
    {
      builder.Validate(Door);
      builder.Validate(UnlockIfLocked);
      builder.Validate(CloseIfAlreadyOpen);
      builder.Validate(OpenIfAlreadyClosed);
      
      var element = ElementTool.Create<SwitchDoor>();
      element.Door = Door;
      element.UnlockIfLocked = UnlockIfLocked;
      element.CloseIfAlreadyOpen = CloseIfAlreadyOpen;
      element.OpenIfAlreadyClosed = OpenIfAlreadyClosed;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SwitchDualCompanion"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(SwitchDualCompanion))]
    public static ActionsBuilder AddSwitchDualCompanion(
        this ActionsBuilder builder,
        UnitEvaluator Unit)
    {
      builder.Validate(Unit);
      
      var element = ElementTool.Create<SwitchDualCompanion>();
      element.Unit = Unit;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SwitchFaction"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Faction"><see cref="BlueprintFaction"/></param>
    [Generated]
    [Implements(typeof(SwitchFaction))]
    public static ActionsBuilder AddSwitchFaction(
        this ActionsBuilder builder,
        UnitEvaluator Target,
        string m_Faction,
        Boolean IncludeGroup,
        Boolean ResetAllRelations)
    {
      builder.Validate(Target);
      builder.Validate(IncludeGroup);
      builder.Validate(ResetAllRelations);
      
      var element = ElementTool.Create<SwitchFaction>();
      element.Target = Target;
      element.m_Faction = BlueprintTool.GetRef<BlueprintFactionReference>(m_Faction);
      element.IncludeGroup = IncludeGroup;
      element.ResetAllRelations = ResetAllRelations;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SwitchInteraction"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(SwitchInteraction))]
    public static ActionsBuilder AddSwitchInteraction(
        this ActionsBuilder builder,
        MapObjectEvaluator MapObject,
        Boolean EnableIfAlreadyDisabled,
        Boolean DisableIfAlreadyEnabled)
    {
      builder.Validate(MapObject);
      builder.Validate(EnableIfAlreadyDisabled);
      builder.Validate(DisableIfAlreadyEnabled);
      
      var element = ElementTool.Create<SwitchInteraction>();
      element.MapObject = MapObject;
      element.EnableIfAlreadyDisabled = EnableIfAlreadyDisabled;
      element.DisableIfAlreadyEnabled = DisableIfAlreadyEnabled;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SwitchRoaming"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(SwitchRoaming))]
    public static ActionsBuilder AddSwitchRoaming(
        this ActionsBuilder builder,
        UnitEvaluator Unit,
        Boolean Disable)
    {
      builder.Validate(Unit);
      builder.Validate(Disable);
      
      var element = ElementTool.Create<SwitchRoaming>();
      element.Unit = Unit;
      element.Disable = Disable;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SwitchToEnemy"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_FactionToAttack"><see cref="BlueprintFaction"/></param>
    [Generated]
    [Implements(typeof(SwitchToEnemy))]
    public static ActionsBuilder AddSwitchToEnemy(
        this ActionsBuilder builder,
        UnitEvaluator Target,
        string m_FactionToAttack)
    {
      builder.Validate(Target);
      
      var element = ElementTool.Create<SwitchToEnemy>();
      element.Target = Target;
      element.m_FactionToAttack = BlueprintTool.GetRef<BlueprintFactionReference>(m_FactionToAttack);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SwitchToNeutral"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="Faction"><see cref="BlueprintFaction"/></param>
    [Generated]
    [Implements(typeof(SwitchToNeutral))]
    public static ActionsBuilder AddSwitchToNeutral(
        this ActionsBuilder builder,
        UnitEvaluator Target,
        string Faction,
        Boolean IncludeGroup)
    {
      builder.Validate(Target);
      builder.Validate(IncludeGroup);
      
      var element = ElementTool.Create<SwitchToNeutral>();
      element.Target = Target;
      element.Faction = BlueprintTool.GetRef<BlueprintFactionReference>(Faction);
      element.IncludeGroup = IncludeGroup;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="TimeSkip"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(TimeSkip))]
    public static ActionsBuilder AddTimeSkip(
        this ActionsBuilder builder,
        TimeSkip.SkipType m_Type,
        IntEvaluator MinutesToSkip,
        TimeOfDay TimeOfDay,
        Boolean NoFatigue,
        Boolean MatchTimeOfDay)
    {
      builder.Validate(m_Type);
      builder.Validate(MinutesToSkip);
      builder.Validate(TimeOfDay);
      builder.Validate(NoFatigue);
      builder.Validate(MatchTimeOfDay);
      
      var element = ElementTool.Create<TimeSkip>();
      element.m_Type = m_Type;
      element.MinutesToSkip = MinutesToSkip;
      element.TimeOfDay = TimeOfDay;
      element.NoFatigue = NoFatigue;
      element.MatchTimeOfDay = MatchTimeOfDay;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnitLookAt"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(UnitLookAt))]
    public static ActionsBuilder AddUnitLookAt(
        this ActionsBuilder builder,
        UnitEvaluator Unit,
        PositionEvaluator Position)
    {
      builder.Validate(Unit);
      builder.Validate(Position);
      
      var element = ElementTool.Create<UnitLookAt>();
      element.Unit = Unit;
      element.Position = Position;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnlinkDualCompanions"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(UnlinkDualCompanions))]
    public static ActionsBuilder AddUnlinkDualCompanions(
        this ActionsBuilder builder,
        UnitEvaluator First,
        UnitEvaluator Second)
    {
      builder.Validate(First);
      builder.Validate(Second);
      
      var element = ElementTool.Create<UnlinkDualCompanions>();
      element.First = First;
      element.Second = Second;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnlockCompanionStory"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Story"><see cref="BlueprintCompanionStory"/></param>
    [Generated]
    [Implements(typeof(UnlockCompanionStory))]
    public static ActionsBuilder AddUnlockCompanionStory(
        this ActionsBuilder builder,
        string m_Story)
    {
      
      var element = ElementTool.Create<UnlockCompanionStory>();
      element.m_Story = BlueprintTool.GetRef<BlueprintCompanionStoryReference>(m_Story);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnlockFlag"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_flag"><see cref="BlueprintUnlockableFlag"/></param>
    [Generated]
    [Implements(typeof(UnlockFlag))]
    public static ActionsBuilder AddUnlockFlag(
        this ActionsBuilder builder,
        string m_flag,
        Int32 flagValue)
    {
      builder.Validate(flagValue);
      
      var element = ElementTool.Create<UnlockFlag>();
      element.m_flag = BlueprintTool.GetRef<BlueprintUnlockableFlagReference>(m_flag);
      element.flagValue = flagValue;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UnmarkAnswersSelected"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Answers"><see cref="BlueprintAnswer"/></param>
    [Generated]
    [Implements(typeof(UnmarkAnswersSelected))]
    public static ActionsBuilder AddUnmarkAnswersSelected(
        this ActionsBuilder builder,
        string[] m_Answers)
    {
      
      var element = ElementTool.Create<UnmarkAnswersSelected>();
      element.m_Answers = m_Answers.Select(bp => BlueprintTool.GetRef<BlueprintAnswerReference>(bp)).ToArray();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="Unrecruit"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_CompanionBlueprint"><see cref="BlueprintUnit"/></param>
    [Generated]
    [Implements(typeof(Unrecruit))]
    public static ActionsBuilder AddUnrecruit(
        this ActionsBuilder builder,
        string m_CompanionBlueprint,
        ActionsBuilder OnUnrecruit)
    {
      
      var element = ElementTool.Create<Unrecruit>();
      element.m_CompanionBlueprint = BlueprintTool.GetRef<BlueprintUnitReference>(m_CompanionBlueprint);
      element.OnUnrecruit = OnUnrecruit.Build();
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UpdateEtudeProgressBar"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="Etude"><see cref="BlueprintEtude"/></param>
    [Generated]
    [Implements(typeof(UpdateEtudeProgressBar))]
    public static ActionsBuilder AddUpdateEtudeProgressBar(
        this ActionsBuilder builder,
        IntEvaluator Progress,
        string Etude)
    {
      builder.Validate(Progress);
      
      var element = ElementTool.Create<UpdateEtudeProgressBar>();
      element.Progress = Progress;
      element.Etude = BlueprintTool.GetRef<BlueprintEtudeReference>(Etude);
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="UpdateEtudes"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(UpdateEtudes))]
    public static ActionsBuilder AddUpdateEtudes(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<UpdateEtudes>());
    }
  }
}
