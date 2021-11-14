using System;
using System.Collections.Generic;
using System.Linq;
using BlueprintCore.Abilities.Restrictions.New;
using BlueprintCore.Actions.Builder;
using BlueprintCore.Blueprints.Facts;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Utils;
using Kingmaker.AI.Blueprints;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Localization;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Abilities.Components.CasterCheckers;
using Kingmaker.UnitLogic.Abilities.Components.TargetCheckers;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker.Actions;

namespace BlueprintCore.Blueprints.Abilities
{
  /// <summary>Configurator for <see cref="BlueprintAbility"/>.</summary>
  /// <inheritdoc/>
  public class AbilityConfigurator : BlueprintUnitFactConfigurator<BlueprintAbility, AbilityConfigurator>
  {
    private Metamagic EnableMetamagics;
    private Metamagic DisableMetamagics;

    private readonly List<BlueprintAbility> EnableVariants = new();
    private readonly List<BlueprintAbility> DisableVariants = new();

    private AbilityConfigurator(string name) : base(name) { }

    /// <inheritdoc cref="Buffs.BuffConfigurator.For(string)"/>
    public static AbilityConfigurator For(string name) { return new AbilityConfigurator(name); }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string)"/>
    public static AbilityConfigurator New(string name)
    {
      BlueprintTool.Create<BlueprintAbility>(name);
      return For(name);
    }

    /// <inheritdoc cref="Buffs.BuffConfigurator.New(string, string)"/>
    public static AbilityConfigurator New(string name, string assetId)
    {
      BlueprintTool.Create<BlueprintAbility>(name, assetId);
      return For(name);
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.m_DefaultAiAction"/>
    /// </summary>
    /// 
    /// <param name="aiAction">
    /// <see cref="BlueprintAiCastSpell"/> Has its <see cref="BlueprintAiCastSpell.m_Ability"/> updated to this blueprint.
    /// </param>
    public AbilityConfigurator SetAiAction(string aiAction)
    {
      OnConfigureInternal(
          blueprint =>
          {
            var aiBlueprint = BlueprintTool.Get<BlueprintAiCastSpell>(aiAction);
            aiBlueprint.m_Ability = Blueprint.ToReference<BlueprintAbilityReference>();
            blueprint.m_DefaultAiAction = aiBlueprint.ToReference<BlueprintAiCastSpell.Reference>();
          });
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.Type"/>
    /// </summary>
    public AbilityConfigurator SetType(AbilityType type)
    {
      OnConfigureInternal(blueprint => blueprint.Type = type);
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.Range"/>
    /// </summary>
    /// 
    /// <remarks>Use <see cref="SetCustomRange(int)"/> for <see cref="AbilityRange.Custom"/>.</remarks>
    public AbilityConfigurator SetRange(AbilityRange range)
    {
      if (range == AbilityRange.Custom)
      {
        throw new InvalidOperationException("Use SetCustomRange() for AbilityRange.Custom.");
      }
      OnConfigureInternal(blueprint => blueprint.Range = range);
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.Range"/> and <see cref="BlueprintAbility.CustomRange"/>
    /// </summary>
    public AbilityConfigurator SetCustomRange(int rangeInFeet)
    {
      OnConfigureInternal(
          blueprint =>
          {
            blueprint.Range = AbilityRange.Custom;
            blueprint.CustomRange = new Feet(rangeInFeet);
          });
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.ShowNameForVariant"/> and <see cref="BlueprintAbility.OnlyForAllyCaster"/>
    /// </summary>
    public AbilityConfigurator ShowNameForVariant(bool showName = true, bool onlyForAlly = false)
    {
      OnConfigureInternal(
          blueprint =>
          {
            blueprint.ShowNameForVariant = showName;
            blueprint.OnlyForAllyCaster = onlyForAlly;
          });
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.CanTargetPoint"/>, <see cref="BlueprintAbility.CanTargetEnemies"/>,
    /// <see cref="BlueprintAbility.CanTargetFriends"/>, and <see cref="BlueprintAbility.CanTargetSelf"/>
    /// </summary>
    public AbilityConfigurator AllowTargeting(
        bool? point = null, bool? enemies = null, bool? friends = null, bool? self = null)
    {
      OnConfigureInternal(
          blueprint =>
          {
            blueprint.CanTargetPoint = point ?? blueprint.CanTargetPoint;
            blueprint.CanTargetEnemies = enemies ?? blueprint.CanTargetEnemies;
            blueprint.CanTargetFriends = friends ?? blueprint.CanTargetFriends;
            blueprint.CanTargetSelf = self ?? blueprint.CanTargetSelf;
          });
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.SpellResistance"/>
    /// </summary>
    public AbilityConfigurator ApplySpellResistance(bool applySR = true)
    {
      OnConfigureInternal(blueprint => blueprint.SpellResistance = applySR);
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.ActionBarAutoFillIgnored"/>
    /// </summary>
    public AbilityConfigurator AutoFillActionBar(bool autoFill = true)
    {
      OnConfigureInternal(blueprint => blueprint.ActionBarAutoFillIgnored = !autoFill);
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.Hidden"/>
    /// </summary>
    public AbilityConfigurator HideInUi(bool hide = true)
    {
      OnConfigureInternal(blueprint => blueprint.Hidden = hide);
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.NeedEquipWeapons"/>
    /// </summary>
    public AbilityConfigurator RequireWeapon(bool requireWeapon = true)
    {
      OnConfigureInternal(blueprint => blueprint.NeedEquipWeapons = requireWeapon);
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.NotOffensive"/>
    /// </summary>
    public AbilityConfigurator IsOffensive(bool offensive = true)
    {
      OnConfigureInternal(blueprint => blueprint.NotOffensive = !offensive);
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.EffectOnAlly"/> and <see cref="BlueprintAbility.EffectOnEnemy"/>
    /// </summary>
    public AbilityConfigurator SetEffectOn(
        AbilityEffectOnUnit? onAlly = null, AbilityEffectOnUnit? onEnemy = null)
    {
      OnConfigureInternal(
          blueprint =>
          {
            blueprint.EffectOnAlly = onAlly ?? blueprint.EffectOnAlly;
            blueprint.EffectOnEnemy = onEnemy ?? blueprint.EffectOnEnemy;
          });
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.m_Parent"/>
    /// </summary>
    /// 
    /// <param name="name"><see cref="BlueprintAbility"/> Has this blueprint added as a variant.</param>
    public AbilityConfigurator SetParent(string name)
    {
      OnConfigureInternal(blueprint => SetParent(BlueprintTool.Get<BlueprintAbility>(name)));
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.m_Parent"/>
    /// </summary>
    /// 
    /// <remarks>The parent will be updated to remove this blueprint as a variant.</remarks>
    public AbilityConfigurator ClearParent()
    {
      OnConfigureInternal(blueprint => RemoveParent());
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.Animation"/>
    /// </summary>
    public AbilityConfigurator SetAnimationStyle(UnitAnimationActionCastSpell.CastAnimationStyle style)
    {
      OnConfigureInternal(blueprint => blueprint.Animation = style);
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.ActionType"/> and <see cref="BlueprintAbility.HasFastAnimation"/>
    /// </summary>
    public AbilityConfigurator SetActionType(UnitCommand.CommandType type, bool? hasFastAnimation = null)
    {
      OnConfigureInternal(
          blueprint =>
          {
            blueprint.ActionType = type;
            blueprint.HasFastAnimation = hasFastAnimation ?? blueprint.HasFastAnimation;
          });
      return this;
    }

    /// <summary>
    /// Adds to <see cref="BlueprintAbility.AvailableMetamagic"/>
    /// </summary>
    public AbilityConfigurator AddMetamagics(params Metamagic[] metamagics)
    {
      foreach (Metamagic metamagic in metamagics) { EnableMetamagics |= metamagic; }
      return this;
    }

    /// <summary>
    /// Removes from <see cref="BlueprintAbility.AvailableMetamagic"/>
    /// </summary>
    public AbilityConfigurator RemoveMetamagics(params Metamagic[] metamagics)
    {
      foreach (Metamagic metamagic in metamagics) { DisableMetamagics |= metamagic; }
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.m_IsFullRoundAction"/>
    /// </summary>
    public AbilityConfigurator MakeFullRound(bool fullRound = true)
    {
      OnConfigureInternal(blueprint => blueprint.m_IsFullRoundAction = fullRound);
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.LocalizedDuration"/>
    /// </summary>
    public AbilityConfigurator SetDurationText(LocalizedString duration)
    {
      OnConfigureInternal(blueprint => blueprint.LocalizedDuration = duration);
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.LocalizedSavingThrow"/>
    /// </summary>
    public AbilityConfigurator SetSavingThrowText(LocalizedString savingThrow)
    {
      OnConfigureInternal(blueprint => blueprint.LocalizedSavingThrow = savingThrow);
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.MaterialComponent"/>
    /// </summary>
    public AbilityConfigurator SetMaterialComponent(BlueprintAbility.MaterialComponentData component)
    {
      OnConfigureInternal(blueprint => blueprint.MaterialComponent = component);
      return this;
    }

    /// <summary>
    /// Sets <see cref="BlueprintAbility.DisableLog"/>
    /// </summary>
    public AbilityConfigurator HideFromLog(bool hide = true)
    {
      OnConfigureInternal(blueprint => blueprint.DisableLog = hide);
      return this;
    }

    /// <summary>
    /// Adds <see cref="AbilityCasterAlignment"/>
    /// </summary>
    /// 
    /// <param name="ignoreFact"><see cref="Kingmaker.Blueprints.Facts.BlueprintUnitFact">BlueprintUnitFact</see></param>
    public AbilityConfigurator RequireCasterAlignment(AlignmentMaskType alignment, string ignoreFact = null)
    {
      var hasAlignment = new AbilityCasterAlignment { Alignment = alignment };
      if (ignoreFact != null)
      {
        hasAlignment.m_IgnoreFact = BlueprintTool.GetRef<BlueprintUnitFactReference>(ignoreFact);
      }
      return AddComponent(hasAlignment);
    }

    /// <summary>
    /// Adds <see cref="AbilityCasterHasFacts"/>
    /// </summary>
    /// 
    /// <param name="facts"><see cref="Kingmaker.Blueprints.Facts.BlueprintUnitFact">BlueprintUnitFact</see></param>
    public AbilityConfigurator RequireCasterFacts(params string[] facts)
    {
      var hasFacts = new AbilityCasterHasFacts
      {
        m_Facts = facts.Select(fact => BlueprintTool.GetRef<BlueprintUnitFactReference>(fact)).ToArray()
      };
      return AddComponent(hasFacts);
    }

    /// <summary>
    /// Adds <see cref="AbilityCasterHasNoFacts"/>
    /// </summary>
    /// 
    /// <param name="facts"><see cref="Kingmaker.Blueprints.Facts.BlueprintUnitFact">BlueprintUnitFact</see></param>
    public AbilityConfigurator RequireCasterHasNoFacts(params string[] facts)
    {
      var hasNoFacts = new AbilityCasterHasNoFacts
      {
        m_Facts = facts.Select(fact => BlueprintTool.GetRef<BlueprintUnitFactReference>(fact)).ToArray()
      };
      return AddComponent(hasNoFacts);
    }

    /// <summary>
    /// Adds <see cref="AbilityCasterHasChosenWeapon"/>
    /// </summary>
    /// 
    /// <remarks>
    /// Requires the caster to wield their chosen weapon, i.e. the weapon in which they have Weapon Focus or Weapon
    /// Specialization.
    /// </remarks>
    /// 
    /// <param name="parameterizedWeaponFeature"><see cref="Kingmaker.Blueprints.Classes.Selection.BlueprintParametrizedFeature">BlueprintParametrizedFeature</see></param>
    /// <param name="ignoreFact"><see cref="Kingmaker.Blueprints.Facts.BlueprintUnitFact">BlueprintUnitFact</see></param>
    public AbilityConfigurator RequireCasterHasChosenWeapon(
        string parameterizedWeaponFeature, string ignoreFact = null)
    {
      var hasChosenWeapon = new AbilityCasterHasChosenWeapon
      {
        m_ChosenWeaponFeature = BlueprintTool.GetRef<BlueprintParametrizedFeatureReference>(parameterizedWeaponFeature)
      };
      if (ignoreFact != null)
      {
        hasChosenWeapon.m_IgnoreWeaponFact = BlueprintTool.GetRef<BlueprintUnitFactReference>(ignoreFact);
      }
      return AddComponent(hasChosenWeapon);
    }

    /// <summary>
    /// Adds <see cref="AbilityCasterHasWeaponWithRangeType"/>
    /// </summary>
    public AbilityConfigurator RequireCasterWeaponRange(WeaponRangeType range)
    {
      var hasWeaponRange = new AbilityCasterHasWeaponWithRangeType { RangeType = range };
      return AddComponent(hasWeaponRange);
    }

    /// <summary>
    /// Adds <see cref="AbilityCasterInCombat"/>
    /// </summary>
    public AbilityConfigurator RequireCasterInCombat(bool requireInCombat = true)
    {
      var isInCombat = new AbilityCasterInCombat { Not = !requireInCombat };
      return AddComponent(isInCombat);
    }

    /// <summary>
    /// Adds <see cref="AbilityCasterIsOnFavoredTerrain"/>
    /// </summary>
    /// 
    /// <param name="ignoreFeature"><see cref="Kingmaker.Blueprints.Classes.BlueprintFeature">BlueprintFeature</see></param>
    public AbilityConfigurator RequireCasterOnFavoredTerrain(string ignoreFeature = null)
    {
      var onFavoredTerrain = new AbilityCasterIsOnFavoredTerrain();
      if (ignoreFeature != null)
      {
        onFavoredTerrain.m_IgnoreFeature = BlueprintTool.GetRef<BlueprintFeatureReference>(ignoreFeature);
      }
      return AddComponent(onFavoredTerrain);
    }

    /// <summary>
    /// Adds <see cref="TargetHasBuffsFromCaster"/>
    /// </summary>
    /// 
    /// <param name="buffs"><see cref="Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff">BlueprintBuff</see></param>
    public AbilityConfigurator RequireTargetHasBuffsFromCaster(string[] buffs, bool requireAllBuffs = false)
    {
      var hasBuffs = new TargetHasBuffsFromCaster
      {
        Buffs = buffs.Select(buff => BlueprintTool.GetRef<BlueprintBuffReference>(buff)).ToArray(),
        RequireAllBuffs = requireAllBuffs
      };
      return AddComponent(hasBuffs);
    }

    /// <summary>
    /// Adds <see cref="AbilityCanTargetDead"/>
    /// </summary>
    public AbilityConfigurator CanTargetDead()
    {
      return AddUniqueComponent(new AbilityCanTargetDead(), ComponentMerge.Skip);
    }

    /// <summary>
    /// Adds <see cref="AbilityDeliveredByWeapon"/>
    /// </summary>
    [DeliverEffectAttr]
    public AbilityConfigurator DeliveredByWeapon()
    {
      return AddUniqueComponent(new AbilityDeliveredByWeapon(), ComponentMerge.Skip);
    }

    /// <summary>
    /// Adds <see cref="AbilityEffectRunAction"/>
    /// </summary>
    /// 
    /// <remarks>Default Merge: Appends the given <see cref="Kingmaker.ElementsSystem.ActionList">ActionList</see></remarks>
    [ApplyEffectAttr]
    public AbilityConfigurator RunActions(
        ActionsBuilder actions,
        SavingThrowType savingThrow = SavingThrowType.Unknown,
        ComponentMerge mergeBehavior = ComponentMerge.Merge,
        Action<BlueprintComponent, BlueprintComponent> merge = null)
    {
      var run = new AbilityEffectRunAction
      {
        Actions = actions.Build(),
        SavingThrowType = savingThrow
      };
      return AddUniqueComponent(run, mergeBehavior, merge ?? MergeRunActions);
    }

    private static void MergeRunActions(BlueprintComponent current, BlueprintComponent other)
    {
      var source = current as AbilityEffectRunAction;
      var target = other as AbilityEffectRunAction;
      source.Actions.Actions = CommonTool.Append(source.Actions.Actions, target.Actions.Actions);
    }

    /// <summary>
    /// Adds <see cref="AbilityEffectMiss"/>
    /// </summary>
    /// 
    /// <remarks>Default Merge: Appends the given <see cref="Kingmaker.ElementsSystem.ActionList">ActionList</see></remarks>
    public AbilityConfigurator OnMiss(
        ActionsBuilder actions,
        bool useTargetSelector = true,
        ComponentMerge mergeBehavior = ComponentMerge.Merge,
        Action<BlueprintComponent, BlueprintComponent> merge = null)
    {
      var onMiss = new AbilityEffectMiss
      {
        MissAction = actions.Build(),
        UseTargetSelector = useTargetSelector
      };
      return AddUniqueComponent(onMiss, mergeBehavior, merge ?? MergeMissActions);
    }

    private static void MergeMissActions(BlueprintComponent current, BlueprintComponent other)
    {
      var source = current as AbilityEffectMiss;
      var target = other as AbilityEffectMiss;
      source.MissAction.Actions = CommonTool.Append(source.MissAction.Actions, target.MissAction.Actions);
    }

    // TODO: Replace w/ child classes
    [SelectTargetAttr]
    public AbilityConfigurator SelectTarget(AbilitySelectTarget target)
    {
      return AddComponent(target);
    }

    /// <summary>
    /// Adds <see cref="AbilityExecuteActionOnCast"/>
    /// </summary>
    public AbilityConfigurator OnCast(ActionsBuilder actions, ConditionsBuilder checker = null)
    {
      var onCast = new AbilityExecuteActionOnCast
      {
        Conditions = checker?.Build() ?? Constants.Empty.Conditions,
        Actions = actions.Build()
      };
      return AddComponent(onCast);
    }

    /// <summary>
    /// Adds <see cref="SpellComponent"/>
    /// </summary>
    public AbilityConfigurator SetSpellSchool(
        SpellSchool school,
        ComponentMerge mergeBehavior = ComponentMerge.Replace,
        Action<BlueprintComponent, BlueprintComponent> merge = null)
    {
      var schoolComponent = new SpellComponent { School = school };
      return AddUniqueComponent(schoolComponent, mergeBehavior, merge);
    }

    /// <summary>
    /// Adds <see cref="CantripComponent"/>
    /// </summary>
    public AbilityConfigurator MakeCantrip()
    {
      return AddUniqueComponent(new CantripComponent(), ComponentMerge.Skip);
    }

    /// <summary>
    /// Removes <see cref="CantripComponent"/>
    /// </summary>
    public AbilityConfigurator MakeNotCantrip()
    {
      return RemoveComponents(c => c is CantripComponent);
    }

    /// <summary>
    /// Adds or modifies <see cref="AbilityVariants"/>
    /// </summary>
    /// 
    /// <param name="abilities"><see cref="BlueprintAbility"/> Updates the parent of each ability to this blueprint</param>
    public AbilityConfigurator AddVariants(params string[] abilities)
    {
      EnableVariants.AddRange(abilities.Select(name => BlueprintTool.Get<BlueprintAbility>(name)));
      return this;
    }

    /// <summary>
    /// Modifies <see cref="AbilityVariants"/>
    /// </summary>
    /// 
    /// <param name="abilities"><see cref="BlueprintAbility"/> Removes this blueprint as the parent of each ability</param>
    public AbilityConfigurator RemoveVariants(params string[] abilities)
    {
      DisableVariants.AddRange(abilities.Select(name => BlueprintTool.Get<BlueprintAbility>(name)));
      return this;
    }

    protected override void ConfigureInternal()
    {
      base.ConfigureInternal();

      if (EnableMetamagics > 0) { Blueprint.AvailableMetamagic |= EnableMetamagics; }
      if (DisableMetamagics > 0) { Blueprint.AvailableMetamagic &= ~DisableMetamagics; }

      if (EnableVariants.Count > 0 || DisableVariants.Count > 0) { ConfigureVariants(); }
    }

    protected override void ValidateInternal()
    {
      base.ValidateInternal();

      if (Blueprint.CustomRange > new Feet(0) && Blueprint.Range != AbilityRange.Custom)
      {
        AddValidationWarning("A custom range value is present without AbilityRange.Custom. It will be ignored.");
      }

      var spellComponent = Blueprint.GetComponent<SpellComponent>();
      if (spellComponent != null && spellComponent.School == SpellSchool.None)
      {
        AddValidationWarning("A spell component is present without a SpellSchool.");
      }

      if (Blueprint.GetComponents<AbilityVariants>().Count() > 1)
      {
        AddValidationWarning("Multiple AbilityVariants components present. Only the first is used.");
      }

      if (Blueprint.GetComponents<CantripComponent>().Count() > 1)
      {
        AddValidationWarning("Multiple AbilityVariants components present. Only the first is used.");
      }

      List<AbilityDeliverEffect> deliverEffects = Blueprint.GetComponents<AbilityDeliverEffect>().ToList();
      if (deliverEffects.Count > 1)
      {
        AddValidationWarning("Multiple AbilityDeliverEffects present. Only the first is used.");
      }

      if (Blueprint.GetComponent<AbilityApplyEffect>() is AbilityEffectMiss)
      {
        AddValidationWarning("AbilityEffectMiss is the first AbilityApplyEffect. It will always trigger.");
      }

      List<AbilityApplyEffect> applyEffects =
          Blueprint.GetComponents<AbilityApplyEffect>().ToList();
      if (applyEffects.Count == 1 && applyEffects[0] is AbilityEffectMiss)
      {
        AddValidationWarning("AbilityEffectMiss is the only AbilityApplyEffect. It will trigger on hit or miss.");
      }
      else if (applyEffects.Count == 2 && applyEffects[1] is AbilityEffectMiss)
      {
        var deliverEffect = Blueprint.GetComponent<AbilityDeliverEffect>();
        if (deliverEffect == null)
        {
          AddValidationWarning($"AbilityEffectMiss requires an AbilityDeliverEffect.");
        }
        else if (!SupportsEffectMiss(deliverEffect))
        {
          AddValidationWarning($"AbilityEffectMiss is not compatible with {deliverEffect.GetType()}");
        }
      }
      else if (applyEffects.Count > 1)
      {
        AddValidationWarning("Too many AbilityApplyEffects present. Only the first is used.");
      }

      if (Blueprint.GetComponents<AbilitySelectTarget>().Count() > 1)
      {
        AddValidationWarning("Multiple AbilitySelectTarget components present. Only the first is used.");
      }
    }

    private bool SupportsEffectMiss(AbilityDeliverEffect effect)
    {
      return
          effect is AbilityDeliveredByWeapon
          || effect is AbilityDeliverClashingRocks
          || effect is AbilityDeliverProjectile
          || effect is AbilityDeliverTouch;
    }

    private void SetParent(BlueprintAbility parent)
    {
      Blueprint.Parent = parent;

      var parentVariants = parent.GetComponent<AbilityVariants>();
      var abilityRef = Blueprint.ToReference<BlueprintAbilityReference>();
      if (parentVariants != null)
      {
        parentVariants.m_Variants = CommonTool.Append(parentVariants.m_Variants, abilityRef);
        return;
      }

      parentVariants = new();
      parentVariants.m_Variants = new BlueprintAbilityReference[] { abilityRef };
      parent.AddComponents(parentVariants);
    }

    private void RemoveParent()
    {
      var parentVariants = Blueprint.Parent?.GetComponent<AbilityVariants>();
      Blueprint.Parent = null;
      if (parentVariants == null)
      {
        AddValidationWarning($"Tried to remove an invalid parent.");
        return;
      }
      parentVariants.m_Variants =
          parentVariants.m_Variants
              .ToList()
              .FindAll(ability => ability != Blueprint.ToReference<BlueprintAbilityReference>())
              .ToArray();
    }

    private void ConfigureVariants()
    {
      var component = Blueprint.GetComponent<AbilityVariants>();
      if (component == null)
      {
        // Don't create a component to remove variants
        if (EnableVariants.Count == 0) { return; }

        component = new AbilityVariants();
        AddComponent(component);
      }
      EnableVariants.AddRange(
          component.m_Variants?.Select(reference => reference.Get()) ?? Array.Empty<BlueprintAbility>());
      var variants = EnableVariants.Except(DisableVariants);

      // Each variant must have this as its parent
      variants.ForEach(ability => ability.m_Parent = Blueprint.ToReference<BlueprintAbilityReference>());
      component.m_Variants = variants.Select(ability => ability.ToReference<BlueprintAbilityReference>()).ToArray();

      // Remove this as parent of any variants removed
      DisableVariants.ForEach(
          ability =>
            {
              if (ability.m_Parent?.deserializedGuid == Blueprint.AssetGuid)
              {
                ability.m_Parent = BlueprintTool.GetRef<BlueprintAbilityReference>(null);
              }
            });
    }

    // Attribute for methods which add AbilityApplyEffect components.
    private class ApplyEffectAttr : Attribute { }
    // Attribute for methods which add AbilityDeliverEffect components.
    private class DeliverEffectAttr : Attribute { }
    // Attribute for methods which add AbilitySelectTarget components.
    private class SelectTargetAttr : Attribute { }
  }
}