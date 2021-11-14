using BlueprintCore.Blueprints;
using BlueprintCore.Utils;
using Kingmaker.AreaLogic;
using Kingmaker.AreaLogic.QuestSystem;
using Kingmaker.Assets.Code.Designers.EventConditionActionSystem.Conditions;
using Kingmaker.Assets.Designers.EventConditionActionSystem.Conditions;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Area;
using Kingmaker.Designers.EventConditionActionSystem.Conditions;
using Kingmaker.ElementsSystem;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Alignments;
using System;
using System.Collections.Generic;
namespace BlueprintCore.Conditions.Builder.MiscEx
{
  /// <summary>
  /// Extension to <see cref="ConditionsBuilder"/> for conditions related to the story such as companion stories, quests,
  /// name changes, and etudes.
  /// </summary>
  /// <inheritdoc cref="ConditionsBuilder"/>
  public static class ConditionsBuilderStoryEx
  {
    //----- Auto Generated -----//



    /// <summary>
    /// Adds <see cref="CheckUnitSeeUnit"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(CheckUnitSeeUnit))]
    public static ConditionsBuilder AddCheckUnitSeeUnit(
        this ConditionsBuilder builder,
        UnitEvaluator Unit,
        UnitEvaluator Target,
        bool negate = false)
    {
      builder.Validate(Unit);
      builder.Validate(Target);
      
      var element = ElementTool.Create<CheckUnitSeeUnit>();
      element.Unit = Unit;
      element.Target = Target;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="BarkBanterPlayed"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Banter"><see cref="BlueprintBarkBanter"/></param>
    [Generated]
    [Implements(typeof(BarkBanterPlayed))]
    public static ConditionsBuilder AddBarkBanterPlayed(
        this ConditionsBuilder builder,
        string m_Banter,
        bool negate = false)
    {
      
      var element = ElementTool.Create<BarkBanterPlayed>();
      element.m_Banter = BlueprintTool.GetRef<BlueprintBarkBanterReference>(m_Banter);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="DialogSeen"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Dialog"><see cref="BlueprintDialog"/></param>
    [Generated]
    [Implements(typeof(DialogSeen))]
    public static ConditionsBuilder AddDialogSeen(
        this ConditionsBuilder builder,
        string m_Dialog,
        bool negate = false)
    {
      
      var element = ElementTool.Create<DialogSeen>();
      element.m_Dialog = BlueprintTool.GetRef<BlueprintDialogReference>(m_Dialog);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="AlignmentCheck"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(AlignmentCheck))]
    public static ConditionsBuilder AddAlignmentCheck(
        this ConditionsBuilder builder,
        AlignmentComponent Alignment,
        bool negate = false)
    {
      builder.Validate(Alignment);
      
      var element = ElementTool.Create<AlignmentCheck>();
      element.Alignment = Alignment;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="AnotherEtudeOfGroupIsPlaying"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Group"><see cref="BlueprintEtudeConflictingGroup"/></param>
    [Generated]
    [Implements(typeof(AnotherEtudeOfGroupIsPlaying))]
    public static ConditionsBuilder AddAnotherEtudeOfGroupIsPlaying(
        this ConditionsBuilder builder,
        string m_Group,
        bool negate = false)
    {
      
      var element = ElementTool.Create<AnotherEtudeOfGroupIsPlaying>();
      element.m_Group = BlueprintTool.GetRef<BlueprintEtudeConflictingGroupReference>(m_Group);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="AnswerListShown"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_AnswersList"><see cref="BlueprintAnswersList"/></param>
    [Generated]
    [Implements(typeof(AnswerListShown))]
    public static ConditionsBuilder AddAnswerListShown(
        this ConditionsBuilder builder,
        string m_AnswersList,
        Boolean CurrentDialog,
        bool negate = false)
    {
      builder.Validate(CurrentDialog);
      
      var element = ElementTool.Create<AnswerListShown>();
      element.m_AnswersList = BlueprintTool.GetRef<BlueprintAnswersListReference>(m_AnswersList);
      element.CurrentDialog = CurrentDialog;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="AnswerSelected"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Answer"><see cref="BlueprintAnswer"/></param>
    [Generated]
    [Implements(typeof(AnswerSelected))]
    public static ConditionsBuilder AddAnswerSelected(
        this ConditionsBuilder builder,
        string m_Answer,
        Boolean CurrentDialog,
        bool negate = false)
    {
      builder.Validate(CurrentDialog);
      
      var element = ElementTool.Create<AnswerSelected>();
      element.m_Answer = BlueprintTool.GetRef<BlueprintAnswerReference>(m_Answer);
      element.CurrentDialog = CurrentDialog;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ChangeableDynamicIsLoaded"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ChangeableDynamicIsLoaded))]
    public static ConditionsBuilder AddChangeableDynamicIsLoaded(
        this ConditionsBuilder builder,
        SceneReference Scene,
        bool negate = false)
    {
      builder.Validate(Scene);
      
      var element = ElementTool.Create<ChangeableDynamicIsLoaded>();
      element.Scene = Scene;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="CheckFailed"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Check"><see cref="BlueprintCheck"/></param>
    [Generated]
    [Implements(typeof(CheckFailed))]
    public static ConditionsBuilder AddCheckFailed(
        this ConditionsBuilder builder,
        string m_Check,
        bool negate = false)
    {
      
      var element = ElementTool.Create<CheckFailed>();
      element.m_Check = BlueprintTool.GetRef<BlueprintCheckReference>(m_Check);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="CheckPassed"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Check"><see cref="BlueprintCheck"/></param>
    [Generated]
    [Implements(typeof(CheckPassed))]
    public static ConditionsBuilder AddCheckPassed(
        this ConditionsBuilder builder,
        string m_Check,
        bool negate = false)
    {
      
      var element = ElementTool.Create<CheckPassed>();
      element.m_Check = BlueprintTool.GetRef<BlueprintCheckReference>(m_Check);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="CompanionStoryUnlocked"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_CompanionStory"><see cref="BlueprintCompanionStory"/></param>
    [Generated]
    [Implements(typeof(CompanionStoryUnlocked))]
    public static ConditionsBuilder AddCompanionStoryUnlocked(
        this ConditionsBuilder builder,
        string m_CompanionStory,
        bool negate = false)
    {
      
      var element = ElementTool.Create<CompanionStoryUnlocked>();
      element.m_CompanionStory = BlueprintTool.GetRef<BlueprintCompanionStoryReference>(m_CompanionStory);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="CueSeen"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Cue"><see cref="BlueprintCueBase"/></param>
    [Generated]
    [Implements(typeof(CueSeen))]
    public static ConditionsBuilder AddCueSeen(
        this ConditionsBuilder builder,
        string m_Cue,
        Boolean CurrentDialog,
        bool negate = false)
    {
      builder.Validate(CurrentDialog);
      
      var element = ElementTool.Create<CueSeen>();
      element.m_Cue = BlueprintTool.GetRef<BlueprintCueBaseReference>(m_Cue);
      element.CurrentDialog = CurrentDialog;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="CurrentChapter"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(CurrentChapter))]
    public static ConditionsBuilder AddCurrentChapter(
        this ConditionsBuilder builder,
        Int32 Chapter,
        bool negate = false)
    {
      builder.Validate(Chapter);
      
      var element = ElementTool.Create<CurrentChapter>();
      element.Chapter = Chapter;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="CutsceneQueueState"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(CutsceneQueueState))]
    public static ConditionsBuilder AddCutsceneQueueState(
        this ConditionsBuilder builder,
        Boolean First,
        Boolean Last,
        bool negate = false)
    {
      builder.Validate(First);
      builder.Validate(Last);
      
      var element = ElementTool.Create<CutsceneQueueState>();
      element.First = First;
      element.Last = Last;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="DayOfTheMonth"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(DayOfTheMonth))]
    public static ConditionsBuilder AddDayOfTheMonth(
        this ConditionsBuilder builder,
        Int32 Day,
        bool negate = false)
    {
      builder.Validate(Day);
      
      var element = ElementTool.Create<DayOfTheMonth>();
      element.Day = Day;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="DayOfTheWeek"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(DayOfTheWeek))]
    public static ConditionsBuilder AddDayOfTheWeek(
        this ConditionsBuilder builder,
        DayOfWeek Day,
        bool negate = false)
    {
      builder.Validate(Day);
      
      var element = ElementTool.Create<DayOfTheWeek>();
      element.Day = Day;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="DayTime"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(DayTime))]
    public static ConditionsBuilder AddDayTime(
        this ConditionsBuilder builder,
        TimeOfDay Time,
        bool negate = false)
    {
      builder.Validate(Time);
      
      var element = ElementTool.Create<DayTime>();
      element.Time = Time;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="EtudeStatus"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Etude"><see cref="BlueprintEtude"/></param>
    [Generated]
    [Implements(typeof(EtudeStatus))]
    public static ConditionsBuilder AddEtudeStatus(
        this ConditionsBuilder builder,
        string m_Etude,
        Boolean NotStarted,
        Boolean Started,
        Boolean Playing,
        Boolean CompletionInProgress,
        Boolean Completed,
        bool negate = false)
    {
      builder.Validate(NotStarted);
      builder.Validate(Started);
      builder.Validate(Playing);
      builder.Validate(CompletionInProgress);
      builder.Validate(Completed);
      
      var element = ElementTool.Create<EtudeStatus>();
      element.m_Etude = BlueprintTool.GetRef<BlueprintEtudeReference>(m_Etude);
      element.NotStarted = NotStarted;
      element.Started = Started;
      element.Playing = Playing;
      element.CompletionInProgress = CompletionInProgress;
      element.Completed = Completed;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="FlagInRange"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Flag"><see cref="BlueprintUnlockableFlag"/></param>
    [Generated]
    [Implements(typeof(FlagInRange))]
    public static ConditionsBuilder AddFlagInRange(
        this ConditionsBuilder builder,
        string m_Flag,
        Int32 MinValue,
        Int32 MaxValue,
        bool negate = false)
    {
      builder.Validate(MinValue);
      builder.Validate(MaxValue);
      
      var element = ElementTool.Create<FlagInRange>();
      element.m_Flag = BlueprintTool.GetRef<BlueprintUnlockableFlagReference>(m_Flag);
      element.MinValue = MinValue;
      element.MaxValue = MaxValue;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="FlagUnlocked"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_ConditionFlag"><see cref="BlueprintUnlockableFlag"/></param>
    [Generated]
    [Implements(typeof(FlagUnlocked))]
    public static ConditionsBuilder AddFlagUnlocked(
        this ConditionsBuilder builder,
        string m_ConditionFlag,
        Boolean ExceptSpecifiedValues,
        List<Int32> SpecifiedValues,
        bool negate = false)
    {
      builder.Validate(ExceptSpecifiedValues);
      foreach (var item in SpecifiedValues)
      {
        builder.Validate(item);
      }
      
      var element = ElementTool.Create<FlagUnlocked>();
      element.m_ConditionFlag = BlueprintTool.GetRef<BlueprintUnlockableFlagReference>(m_ConditionFlag);
      element.ExceptSpecifiedValues = ExceptSpecifiedValues;
      element.SpecifiedValues = SpecifiedValues;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="IsLegendPathSelected"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(IsLegendPathSelected))]
    public static ConditionsBuilder AddIsLegendPathSelected(
        this ConditionsBuilder builder,
        bool negate = false)
    {
      
      var element = ElementTool.Create<IsLegendPathSelected>();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="MonthFromList"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(MonthFromList))]
    public static ConditionsBuilder AddMonthFromList(
        this ConditionsBuilder builder,
        Int32[] Months,
        bool negate = false)
    {
      foreach (var item in Months)
      {
        builder.Validate(item);
      }
      
      var element = ElementTool.Create<MonthFromList>();
      element.Months = Months;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ObjectiveStatus"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_QuestObjective"><see cref="BlueprintQuestObjective"/></param>
    [Generated]
    [Implements(typeof(ObjectiveStatus))]
    public static ConditionsBuilder AddObjectiveStatus(
        this ConditionsBuilder builder,
        string m_QuestObjective,
        QuestObjectiveState State,
        bool negate = false)
    {
      builder.Validate(State);
      
      var element = ElementTool.Create<ObjectiveStatus>();
      element.m_QuestObjective = BlueprintTool.GetRef<BlueprintQuestObjectiveReference>(m_QuestObjective);
      element.State = State;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="PlayerAlignmentIs"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(PlayerAlignmentIs))]
    public static ConditionsBuilder AddPlayerAlignmentIs(
        this ConditionsBuilder builder,
        AlignmentMaskType Alignment,
        bool negate = false)
    {
      builder.Validate(Alignment);
      
      var element = ElementTool.Create<PlayerAlignmentIs>();
      element.Alignment = Alignment;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="PlayerHasNoCharactersOnRoster"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(PlayerHasNoCharactersOnRoster))]
    public static ConditionsBuilder AddPlayerHasNoCharactersOnRoster(
        this ConditionsBuilder builder,
        bool negate = false)
    {
      
      var element = ElementTool.Create<PlayerHasNoCharactersOnRoster>();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="PlayerHasRecruitsOnRoster"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(PlayerHasRecruitsOnRoster))]
    public static ConditionsBuilder AddPlayerHasRecruitsOnRoster(
        this ConditionsBuilder builder,
        bool negate = false)
    {
      
      var element = ElementTool.Create<PlayerHasRecruitsOnRoster>();
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="PlayerSignificantClassIs"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_CharacterClass"><see cref="BlueprintCharacterClass"/></param>
    /// <param name="m_CharacterClassGroup"><see cref="BlueprintCharacterClassGroup"/></param>
    [Generated]
    [Implements(typeof(PlayerSignificantClassIs))]
    public static ConditionsBuilder AddPlayerSignificantClassIs(
        this ConditionsBuilder builder,
        Boolean CheckGroup,
        string m_CharacterClass,
        string m_CharacterClassGroup,
        bool negate = false)
    {
      builder.Validate(CheckGroup);
      
      var element = ElementTool.Create<PlayerSignificantClassIs>();
      element.CheckGroup = CheckGroup;
      element.m_CharacterClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(m_CharacterClass);
      element.m_CharacterClassGroup = BlueprintTool.GetRef<BlueprintCharacterClassGroupReference>(m_CharacterClassGroup);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="PlayerTopClassIs"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_CharacterClass"><see cref="BlueprintCharacterClass"/></param>
    /// <param name="m_CharacterClassGroup"><see cref="BlueprintCharacterClassGroup"/></param>
    [Generated]
    [Implements(typeof(PlayerTopClassIs))]
    public static ConditionsBuilder AddPlayerTopClassIs(
        this ConditionsBuilder builder,
        Boolean CheckGroup,
        string m_CharacterClass,
        string m_CharacterClassGroup,
        bool negate = false)
    {
      builder.Validate(CheckGroup);
      
      var element = ElementTool.Create<PlayerTopClassIs>();
      element.CheckGroup = CheckGroup;
      element.m_CharacterClass = BlueprintTool.GetRef<BlueprintCharacterClassReference>(m_CharacterClass);
      element.m_CharacterClassGroup = BlueprintTool.GetRef<BlueprintCharacterClassGroupReference>(m_CharacterClassGroup);
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="QuestStatus"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Quest"><see cref="BlueprintQuest"/></param>
    [Generated]
    [Implements(typeof(QuestStatus))]
    public static ConditionsBuilder AddQuestStatus(
        this ConditionsBuilder builder,
        string m_Quest,
        QuestState State,
        bool negate = false)
    {
      builder.Validate(State);
      
      var element = ElementTool.Create<QuestStatus>();
      element.m_Quest = BlueprintTool.GetRef<BlueprintQuestReference>(m_Quest);
      element.State = State;
      element.Not = negate;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="RomanceLocked"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Romance"><see cref="BlueprintRomanceCounter"/></param>
    [Generated]
    [Implements(typeof(RomanceLocked))]
    public static ConditionsBuilder AddRomanceLocked(
        this ConditionsBuilder builder,
        string m_Romance,
        bool negate = false)
    {
      
      var element = ElementTool.Create<RomanceLocked>();
      element.m_Romance = BlueprintTool.GetRef<BlueprintRomanceCounterReference>(m_Romance);
      element.Not = negate;
      return builder.Add(element);
    }
  }
}
