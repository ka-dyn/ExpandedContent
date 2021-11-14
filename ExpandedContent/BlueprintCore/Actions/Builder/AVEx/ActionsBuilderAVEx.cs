using BlueprintCore.Blueprints;
using BlueprintCore.Utils;
using Kingmaker.Assets.UnitLogic.Mechanics.Actions;
using Kingmaker.Blueprints;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.Designers.EventConditionActionSystem.NamedParameters;
using Kingmaker.ElementsSystem;
using Kingmaker.Localization;
using Kingmaker.ResourceLinks;
using Kingmaker.Sound;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Visual.Animation;
using Kingmaker.Visual.Animation.Actions;
using System;
using UnityEngine;

namespace BlueprintCore.Actions.Builder.AVEx
{
  /// <summary>
  /// Extension to <see cref="ActionsBuilder"/> for actions involving audiovisual effects such as dialogs, camera,
  /// cutscenes, and sounds.
  /// </summary>
  /// <inheritdoc cref="ActionsBuilder"/>
  public static class ActionsBuilderAVEx
  {
    /// <summary>
    /// Adds <see cref="ChangeBookEventImage"/>
    /// </summary>
    [Implements(typeof(ChangeBookEventImage))]
    public static ActionsBuilder ChangeBookImage(this ActionsBuilder builder, SpriteLink image)
    {
      var setImage = ElementTool.Create<ChangeBookEventImage>();
      setImage.m_Image = image;
      return builder.Add(setImage);
    }

    /// <summary>
    /// Adds <see cref="CameraToPosition"/>
    /// </summary>
    [Implements(typeof(CameraToPosition))]
    public static ActionsBuilder MoveCamera(this ActionsBuilder builder, PositionEvaluator position)
    {
      builder.Validate(position);

      var moveCamera = ElementTool.Create<CameraToPosition>();
      moveCamera.Position = position;
      return builder.Add(moveCamera);
    }

    /// <summary>
    /// Adds
    /// <see cref="Kingmaker.Designers.EventConditionActionSystem.Actions.AddDialogNotification">AddDialogNotification</see>
    /// </summary>
    [Implements(typeof(AddDialogNotification))]
    public static ActionsBuilder AddDialogNotification(this ActionsBuilder builder, LocalizedString text)
    {
      var notification = ElementTool.Create<AddDialogNotification>();
      notification.Text = text;
      return builder.Add(notification);
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.Designers.EventConditionActionSystem.Actions.ClearBlood">ClearBlood</see>
    /// </summary>
    [Implements(typeof(ClearBlood))]
    public static ActionsBuilder ClearBlood(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ClearBlood>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionRunAnimationClip"/>
    /// </summary>
    [Implements(typeof(ContextActionRunAnimationClip))]
    public static ActionsBuilder RunAnimationClip(
        this ActionsBuilder builder,
        AnimationClipWrapper clip,
        ExecutionMode mode = ExecutionMode.Interrupted,
        float transitionIn = 0.25f,
        float transitionOut = 0.25f)
    {
      var animation = ElementTool.Create<ContextActionRunAnimationClip>();
      animation.ClipWrapper = clip;
      animation.Mode = mode;
      animation.TransitionIn = transitionIn;
      animation.TransitionOut = transitionOut;
      return builder.Add(animation);
    }

    /// <summary>
    /// Adds <see cref="ContextActionShowBark"/>
    /// </summary>
    [Implements(typeof(ContextActionShowBark))]
    public static ActionsBuilder Bark(
        this ActionsBuilder builder,
        LocalizedString bark,
        bool showIfUnconcious = false,
        bool durationBasedOnTextLength = false)
    {
      var showBark = ElementTool.Create<ContextActionShowBark>();
      showBark.WhatToBark = bark;
      showBark.ShowWhileUnconscious = showIfUnconcious;
      showBark.BarkDurationByText = durationBasedOnTextLength;
      return builder.Add(showBark);
    }

    /// <summary>
    /// Adds <see cref="ContextActionSpawnFx"/>
    /// </summary>
    [Implements(typeof(ContextActionSpawnFx))]
    public static ActionsBuilder SpawnFx(this ActionsBuilder builder, PrefabLink prefab)
    {
      var spawnFx = ElementTool.Create<ContextActionSpawnFx>();
      spawnFx.PrefabLink = prefab;
      return builder.Add(spawnFx);
    }

    //----- Kingmaker.Assets.UnitLogic.Mechanics.Actions -----//

    /// <summary>
    /// Adds <see cref="ContextActionPlaySound"/>
    /// </summary>
    [Implements(typeof(ContextActionPlaySound))]
    public static ActionsBuilder PlaySound(this ActionsBuilder builder, string soundName)
    {
      var playSound = ElementTool.Create<ContextActionPlaySound>();
      playSound.SoundName = soundName;
      return builder.Add(playSound);
    }

    //----- Auto Generated -----//



    /// <summary>
    /// Adds <see cref="OverrideRainIntesity"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(OverrideRainIntesity))]
    public static ActionsBuilder AddOverrideRainIntesity(
        this ActionsBuilder builder,
        Single RainIntensity,
        Single Duration)
    {
      builder.Validate(RainIntensity);
      builder.Validate(Duration);
      
      var element = ElementTool.Create<OverrideRainIntesity>();
      element.RainIntensity = RainIntensity;
      element.Duration = Duration;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="Play2DSound"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(Play2DSound))]
    public static ActionsBuilder AddPlay2DSound(
        this ActionsBuilder builder,
        String SoundName,
        Boolean SetSex,
        Boolean SetRace)
    {
      foreach (var item in SoundName)
      {
        builder.Validate(item);
      }
      builder.Validate(SetSex);
      builder.Validate(SetRace);
      
      var element = ElementTool.Create<Play2DSound>();
      element.SoundName = SoundName;
      element.SetSex = SetSex;
      element.SetRace = SetRace;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="Play3DSound"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(Play3DSound))]
    public static ActionsBuilder AddPlay3DSound(
        this ActionsBuilder builder,
        String SoundName,
        EntityReference SoundSourceObject,
        Boolean SetSex,
        Boolean SetRace,
        Boolean SetCurrentSpeaker)
    {
      foreach (var item in SoundName)
      {
        builder.Validate(item);
      }
      builder.Validate(SoundSourceObject);
      builder.Validate(SetSex);
      builder.Validate(SetRace);
      builder.Validate(SetCurrentSpeaker);
      
      var element = ElementTool.Create<Play3DSound>();
      element.SoundName = SoundName;
      element.SoundSourceObject = SoundSourceObject;
      element.SetSex = SetSex;
      element.SetRace = SetRace;
      element.SetCurrentSpeaker = SetCurrentSpeaker;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="PlayAnimationOneShot"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(PlayAnimationOneShot))]
    public static ActionsBuilder AddPlayAnimationOneShot(
        this ActionsBuilder builder,
        AnimationClipWrapper m_ClipWrapper,
        UnitEvaluator Unit,
        Single TransitionIn,
        Single TransitionOut)
    {
      builder.Validate(m_ClipWrapper);
      builder.Validate(Unit);
      builder.Validate(TransitionIn);
      builder.Validate(TransitionOut);
      
      var element = ElementTool.Create<PlayAnimationOneShot>();
      element.m_ClipWrapper = m_ClipWrapper;
      element.Unit = Unit;
      element.TransitionIn = TransitionIn;
      element.TransitionOut = TransitionOut;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="PlayCustomMusic"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(PlayCustomMusic))]
    public static ActionsBuilder AddPlayCustomMusic(
        this ActionsBuilder builder,
        String MusicEventStart,
        String MusicEventStop)
    {
      foreach (var item in MusicEventStart)
      {
        builder.Validate(item);
      }
      foreach (var item in MusicEventStop)
      {
        builder.Validate(item);
      }
      
      var element = ElementTool.Create<PlayCustomMusic>();
      element.MusicEventStart = MusicEventStart;
      element.MusicEventStop = MusicEventStop;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="PlayCutscene"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Cutscene"><see cref="Cutscene"/></param>
    [Generated]
    [Implements(typeof(PlayCutscene))]
    public static ActionsBuilder AddPlayCutscene(
        this ActionsBuilder builder,
        string m_Cutscene,
        Boolean PutInQueue,
        Boolean CheckExistence,
        ParametrizedContextSetter Parameters)
    {
      builder.Validate(PutInQueue);
      builder.Validate(CheckExistence);
      builder.Validate(Parameters);
      
      var element = ElementTool.Create<PlayCutscene>();
      element.m_Cutscene = BlueprintTool.GetRef<CutsceneReference>(m_Cutscene);
      element.PutInQueue = PutInQueue;
      element.CheckExistence = CheckExistence;
      element.Parameters = Parameters;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ReloadMechanic"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ReloadMechanic))]
    public static ActionsBuilder AddReloadMechanic(
        this ActionsBuilder builder,
        String Desc,
        Boolean ClearFx)
    {
      foreach (var item in Desc)
      {
        builder.Validate(item);
      }
      builder.Validate(ClearFx);
      
      var element = ElementTool.Create<ReloadMechanic>();
      element.Desc = Desc;
      element.ClearFx = ClearFx;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SetSoundState"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(SetSoundState))]
    public static ActionsBuilder AddSetSoundState(
        this ActionsBuilder builder,
        AkStateReference m_State)
    {
      builder.Validate(m_State);
      
      var element = ElementTool.Create<SetSoundState>();
      element.m_State = m_State;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ShowBark"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ShowBark))]
    public static ActionsBuilder AddShowBark(
        this ActionsBuilder builder,
        LocalizedString WhatToBark,
        SharedStringAsset WhatToBarkShared,
        Boolean BarkDurationByText,
        UnitEvaluator TargetUnit,
        MapObjectEvaluator TargetMapObject)
    {
      builder.Validate(WhatToBark);
      builder.Validate(WhatToBarkShared);
      builder.Validate(BarkDurationByText);
      builder.Validate(TargetUnit);
      builder.Validate(TargetMapObject);
      
      var element = ElementTool.Create<ShowBark>();
      element.WhatToBark = WhatToBark;
      element.WhatToBarkShared = WhatToBarkShared;
      element.BarkDurationByText = BarkDurationByText;
      element.TargetUnit = TargetUnit;
      element.TargetMapObject = TargetMapObject;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="SpawnFx"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(SpawnFx))]
    public static ActionsBuilder AddSpawnFx(
        this ActionsBuilder builder,
        TransformEvaluator Target,
        GameObject FxPrefab)
    {
      builder.Validate(Target);
      builder.Validate(FxPrefab);
      
      var element = ElementTool.Create<SpawnFx>();
      element.Target = Target;
      element.FxPrefab = FxPrefab;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="StopCustomMusic"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(StopCustomMusic))]
    public static ActionsBuilder AddStopCustomMusic(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<StopCustomMusic>());
    }

    /// <summary>
    /// Adds <see cref="StopCutscene"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_Cutscene"><see cref="Cutscene"/></param>
    [Generated]
    [Implements(typeof(StopCutscene))]
    public static ActionsBuilder AddStopCutscene(
        this ActionsBuilder builder,
        string m_Cutscene,
        UnitEvaluator WithUnit,
        StopCutscene.UnitCheckType m_CheckType)
    {
      builder.Validate(WithUnit);
      builder.Validate(m_CheckType);
      
      var element = ElementTool.Create<StopCutscene>();
      element.m_Cutscene = BlueprintTool.GetRef<CutsceneReference>(m_Cutscene);
      element.WithUnit = WithUnit;
      element.m_CheckType = m_CheckType;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ToggleObjectFx"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ToggleObjectFx))]
    public static ActionsBuilder AddToggleObjectFx(
        this ActionsBuilder builder,
        MapObjectEvaluator Target,
        Boolean ToggleOn)
    {
      builder.Validate(Target);
      builder.Validate(ToggleOn);
      
      var element = ElementTool.Create<ToggleObjectFx>();
      element.Target = Target;
      element.ToggleOn = ToggleOn;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ToggleObjectMusic"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ToggleObjectMusic))]
    public static ActionsBuilder AddToggleObjectMusic(
        this ActionsBuilder builder,
        MapObjectEvaluator Target,
        Boolean ToggleOn)
    {
      builder.Validate(Target);
      builder.Validate(ToggleOn);
      
      var element = ElementTool.Create<ToggleObjectMusic>();
      element.Target = Target;
      element.ToggleOn = ToggleOn;
      return builder.Add(element);
    }
  }
}
