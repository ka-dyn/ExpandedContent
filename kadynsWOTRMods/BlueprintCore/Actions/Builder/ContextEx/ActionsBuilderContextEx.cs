using BlueprintCore.Blueprints;
using BlueprintCore.Conditions.Builder;
using BlueprintCore.Utils;
using Kingmaker.Assets.UnitLogic.Mechanics.Actions;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Designers.EventConditionActionSystem.Actions;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.Settings;
using Kingmaker.UI.GenericSlot;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs.Actions;
using Kingmaker.UnitLogic.Class.Kineticist.Actions;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueprintCore.Actions.Builder.ContextEx
{
  /// <summary>
  /// Extension to <see cref="ActionsBuilder"/> for most <see cref="ContextAction"/> types. Some
  /// <see cref="ContextAction"/> types are in more specific extensions such as
  /// <see cref="AVEx.ActionsBuilderAVEx">AVEx</see> or <see cref="KingdomEx.ActionsBuilderKingdomEx">KingdomEx</see>.
  /// </summary>
  /// <inheritdoc cref="ActionsBuilder"/>
  public static class ActionsBuilderContextEx
  {
    /// <summary>
    /// Adds <see cref="ContextActionAddFeature"/>
    /// </summary>
    /// 
    /// <param name="feature"><see cref="Kingmaker.Blueprints.Classes.BlueprintFeature">BlueprintFeature</see></param>
    [Implements(typeof(ContextActionAddFeature))]
    public static ActionsBuilder AddFeature(this ActionsBuilder builder, string feature)
    {
      var addFeature = ElementTool.Create<ContextActionAddFeature>();
      addFeature.m_PermanentFeature = BlueprintTool.GetRef<BlueprintFeatureReference>(feature);
      return builder.Add(addFeature);
    }

    /// <summary>
    /// Adds <see cref="ContextActionAddLocustClone"/>
    /// </summary>
    /// 
    /// <param name="feature"><see cref="Kingmaker.Blueprints.Classes.BlueprintFeature">BlueprintFeature</see></param>
    [Implements(typeof(ContextActionAddLocustClone))]
    public static ActionsBuilder AddLocustClone(this ActionsBuilder builder, string feature)
    {
      var addClone = ElementTool.Create<ContextActionAddLocustClone>();
      addClone.m_CloneFeature = BlueprintTool.GetRef<BlueprintFeatureReference>(feature);
      return builder.Add(addClone);
    }

    /// <summary>
    /// Adds <see cref="ContextActionAeonRollbackToSavedState"/>
    /// </summary>
    [Implements(typeof(ContextActionAeonRollbackToSavedState))]
    public static ActionsBuilder AeonRollbackState(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionAeonRollbackToSavedState>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionApplyBuff"/>
    /// </summary>
    /// 
    /// <param name="buff"><see cref="Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff">BlueprintBuff</see></param>
    [Implements(typeof(ContextActionApplyBuff))]
    public static ActionsBuilder ApplyBuff(
        this ActionsBuilder builder,
        string buff,
        bool useDurationSeconds = false,
        float durationSeconds = 0f,
        ContextDurationValue duration = null,
        bool permanent = false,
        bool isFromSpell = false,
        bool dispellable = true,
        bool toCaster = false,
        bool asChild = true,
        bool sameDuration = false)
    {
      if (!useDurationSeconds && duration == null && !permanent)
      {
        throw new InvalidOperationException("Missing duration.");
      }
      var applyBuff = ElementTool.Create<ContextActionApplyBuff>();
      applyBuff.m_Buff = BlueprintTool.GetRef<BlueprintBuffReference>(buff);
      applyBuff.UseDurationSeconds = useDurationSeconds;
      applyBuff.DurationSeconds = durationSeconds;
      applyBuff.DurationValue = duration;
      applyBuff.Permanent = permanent;
      applyBuff.IsFromSpell = isFromSpell;
      applyBuff.IsNotDispelable = !dispellable;
      applyBuff.ToCaster = toCaster;
      applyBuff.AsChild = asChild;
      applyBuff.SameDuration = sameDuration;
      return builder.Add(applyBuff);
    }

    // Default GUIDs for ArmorEnchantPool and ShieldArmorEnchantPool
    private const string PlusOneArmor = "1d9b60d57afb45c4f9bb0a3c21bb3b98";
    private const string PlusTwoArmor = "d45bfd838c541bb40bde7b0bf0e1b684";
    private const string PlusThreeArmor = "51c51d841e9f16046a169729c13c4d4f";
    private const string PlusFourArmor = "a23bcee56c9fcf64d863dafedb369387";
    private const string PlusFiveArmor = "15d7d6cbbf56bd744b37bbf9225ea83b";

    private static BlueprintItemEnchantmentReference GetItemEnchant(string enchant)
    {
      return BlueprintTool.GetRef<BlueprintItemEnchantmentReference>(enchant);
    }

    /// <summary>
    /// Adds <see cref="ContextActionArmorEnchantPool"/>
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    /// The caster's armor is enchanted based on its available enhancement bonus. <br />
    /// e.g.If the armor can be enchanted to +4 but already has a +1 enchantment, plusThreeEnchantment is applied.
    /// </para>
    /// 
    /// <para>
    /// See ArcaneArmor and SacredArmor blueprints for example usages.
    /// </para>
    /// </remarks>
    /// 
    /// <para>
    /// All enchantments default to the corresponding TemporaryArmorEnhancementBonus* blueprint.
    /// </para>
    /// 
    /// <param name="plusOneEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    /// <param name="plusTwoEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    /// <param name="plusThreeEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    /// <param name="plusFourEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    /// <param name="plusFiveEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    [Implements(typeof(ContextActionArmorEnchantPool))]
    public static ActionsBuilder ArmorEnchantPool(
        this ActionsBuilder builder,
        EnchantPoolType poolType,
        ContextDurationValue duration,
        ActivatableAbilityGroup group = ActivatableAbilityGroup.None,
        string plusOneEnchantment = PlusOneArmor,
        string plusTwoEnchantment = PlusTwoArmor,
        string plusThreeEnchantment = PlusThreeArmor,
        string plusFourEnchantment = PlusFourArmor,
        string plusFiveEnchantment = PlusFiveArmor)
    {
      var enchantArmor = ElementTool.Create<ContextActionArmorEnchantPool>();
      enchantArmor.EnchantPool = poolType;
      enchantArmor.Group = group;
      enchantArmor.DurationValue = duration;
      enchantArmor.m_DefaultEnchantments[0] = GetItemEnchant(plusOneEnchantment);
      enchantArmor.m_DefaultEnchantments[1] = GetItemEnchant(plusTwoEnchantment);
      enchantArmor.m_DefaultEnchantments[2] = GetItemEnchant(plusThreeEnchantment);
      enchantArmor.m_DefaultEnchantments[3] = GetItemEnchant(plusFourEnchantment);
      enchantArmor.m_DefaultEnchantments[4] = GetItemEnchant(plusFiveEnchantment);
      return builder.Add(enchantArmor);
    }

    /// <summary>
    /// Adds <see cref="ContextActionShieldArmorEnchantPool"/>
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    /// The caster's shield is enchanted based on its available enhancement bonus. <br />
    /// e.g.If the shield can be enchanted to +4 but already has a +1 enchantment, plusThreeEnchantment is applied.
    /// </para>
    /// 
    /// <para>
    /// See the SacredArmor blueprint for example usage.
    /// </para>
    /// 
    /// <para>
    /// All enchantments default to the corresponding TemporaryArmorEnhancementBonus* blueprint.
    /// </para>
    /// </remarks>
    /// 
    /// <param name="plusOneEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    /// <param name="plusTwoEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    /// <param name="plusThreeEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    /// <param name="plusFourEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    /// <param name="plusFiveEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    [Implements(typeof(ContextActionShieldArmorEnchantPool))]
    public static ActionsBuilder ShieldArmorEnchantPool(
        this ActionsBuilder builder,
        EnchantPoolType poolType,
        ContextDurationValue duration,
        ActivatableAbilityGroup group = ActivatableAbilityGroup.None,
        string plusOneEnchantment = PlusOneArmor,
        string plusTwoEnchantment = PlusTwoArmor,
        string plusThreeEnchantment = PlusThreeArmor,
        string plusFourEnchantment = PlusFourArmor,
        string plusFiveEnchantment = PlusFiveArmor)
    {
      var enchant = ElementTool.Create<ContextActionShieldArmorEnchantPool>();
      enchant.EnchantPool = poolType;
      enchant.Group = group;
      enchant.DurationValue = duration;
      enchant.m_DefaultEnchantments[0] = GetItemEnchant(plusOneEnchantment);
      enchant.m_DefaultEnchantments[1] = GetItemEnchant(plusTwoEnchantment);
      enchant.m_DefaultEnchantments[2] = GetItemEnchant(plusThreeEnchantment);
      enchant.m_DefaultEnchantments[3] = GetItemEnchant(plusFourEnchantment);
      enchant.m_DefaultEnchantments[4] = GetItemEnchant(plusFiveEnchantment);
      return builder.Add(enchant);
    }

    // Default GUIDs for WeaponEnchantPool and ShieldWeaponEnchantPool
    private const string PlusOneWeapon = "d704f90f54f813043a525f304f6c0050";
    private const string PlusTwoWeapon = "9e9bab3020ec5f64499e007880b37e52";
    private const string PlusThreeWeapon = "d072b841ba0668846adeb007f623bd6c";
    private const string PlusFourWeapon = "6a6a0901d799ceb49b33d4851ff72132";
    private const string PlusFiveWeapon = "746ee366e50611146821d61e391edf16";

    /// <summary>
    /// Adds <see cref="ContextActionWeaponEnchantPool"/>
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    /// The caster's weapon is enchanted based on its available enhancement bonus. <br />
    /// e.g.If the weapon can be enchanted to +4 but already has a +1 enchantment, plusThreeEnchantment is applied.
    /// </para>
    /// 
    /// <para>
    /// See ArcaneWeapon and SacredWeapon blueprints for example usages.
    /// </para>
    /// 
    /// <para>
    /// All enchantments default to the corresponding TemporaryEnhancement* blueprint.
    /// </para>
    /// </remarks>
    /// 
    /// <param name="plusOneEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    /// <param name="plusTwoEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    /// <param name="plusThreeEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    /// <param name="plusFourEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    /// <param name="plusFiveEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    [Implements(typeof(ContextActionWeaponEnchantPool))]
    public static ActionsBuilder WeaponEnchantPool(
        this ActionsBuilder builder,
        EnchantPoolType poolType,
        ContextDurationValue duration,
        ActivatableAbilityGroup group = ActivatableAbilityGroup.None,
        string plusOneEnchantment = PlusOneWeapon,
        string plusTwoEnchantment = PlusTwoWeapon,
        string plusThreeEnchantment = PlusThreeWeapon,
        string plusFourEnchantment = PlusFourWeapon,
        string plusFiveEnchantment = PlusFiveWeapon)
    {
      var enchant = ElementTool.Create<ContextActionWeaponEnchantPool>();
      enchant.EnchantPool = poolType;
      enchant.Group = group;
      enchant.DurationValue = duration;
      enchant.m_DefaultEnchantments[0] = GetItemEnchant(plusOneEnchantment);
      enchant.m_DefaultEnchantments[1] = GetItemEnchant(plusTwoEnchantment);
      enchant.m_DefaultEnchantments[2] = GetItemEnchant(plusThreeEnchantment);
      enchant.m_DefaultEnchantments[3] = GetItemEnchant(plusFourEnchantment);
      enchant.m_DefaultEnchantments[4] = GetItemEnchant(plusFiveEnchantment);
      return builder.Add(enchant);
    }

    /// <summary>
    /// Adds <see cref="ContextActionShieldWeaponEnchantPool"/>
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    /// The caster's shield is enchanted based on its available enhancement bonus. <br />
    /// e.g.If the shield can be enchanted to +4 but already has a +1 enchantment, plusThreeEnchantment is applied.
    /// </para>
    /// 
    /// <para>
    /// See the SacredWeapon blueprint for example usage.
    /// </para>
    /// 
    /// <para>
    /// All enchantments default to the corresponding TemporaryEnhancement* blueprint.
    /// </para>
    /// </remarks>
    /// 
    /// <param name="plusOneEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    /// <param name="plusTwoEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    /// <param name="plusThreeEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    /// <param name="plusFourEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    /// <param name="plusFiveEnchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment"/>BlueprintItemEnchantment</param>
    [Implements(typeof(ContextActionShieldWeaponEnchantPool))]
    public static ActionsBuilder ShieldWeaponEnchantPool(
        this ActionsBuilder builder,
        EnchantPoolType poolType,
        ContextDurationValue duration,
        ActivatableAbilityGroup group = ActivatableAbilityGroup.None,
        string plusOneEnchantment = PlusOneWeapon,
        string plusTwoEnchantment = PlusTwoWeapon,
        string plusThreeEnchantment = PlusThreeWeapon,
        string plusFourEnchantment = PlusFourWeapon,
        string plusFiveEnchantment = PlusFiveWeapon)
    {
      var enchant = ElementTool.Create<ContextActionShieldWeaponEnchantPool>();
      enchant.EnchantPool = poolType;
      enchant.Group = group;
      enchant.DurationValue = duration;
      enchant.m_DefaultEnchantments[0] = GetItemEnchant(plusOneEnchantment);
      enchant.m_DefaultEnchantments[1] = GetItemEnchant(plusTwoEnchantment);
      enchant.m_DefaultEnchantments[2] = GetItemEnchant(plusThreeEnchantment);
      enchant.m_DefaultEnchantments[3] = GetItemEnchant(plusFourEnchantment);
      enchant.m_DefaultEnchantments[4] = GetItemEnchant(plusFiveEnchantment);
      return builder.Add(enchant);
    }

    /// <summary>
    /// Adds <see cref="ContextActionAttackWithWeapon"/>
    /// </summary>
    /// 
    /// <param name="weapon"><see cref="Kingmaker.Blueprints.Items.Weapons.BlueprintItemWeapon">BlueprintItemWeapon</see></param>
    [Implements(typeof(ContextActionAttackWithWeapon))]
    public static ActionsBuilder AttackWithWeapon(this ActionsBuilder builder, StatType damageStat, string weapon)
    {
      var attack = ElementTool.Create<ContextActionAttackWithWeapon>();
      attack.m_Stat = damageStat;
      attack.m_WeaponRef = BlueprintTool.GetRef<BlueprintItemWeaponReference>(weapon);
      return builder.Add(attack);
    }

    /// <summary>
    /// Adds <see cref="ContextActionBatteringBlast"/>
    /// </summary>
    [Implements(typeof(ContextActionBatteringBlast))]
    public static ActionsBuilder BatteringBlast(this ActionsBuilder builder, bool remove = false)
    {
      var blast = ElementTool.Create<ContextActionBatteringBlast>();
      blast.Remove = remove;
      return builder.Add(blast);
    }

    /// <summary>
    /// Adds <see cref="ContextActionBreakFree"/>
    /// </summary>
    [Implements(typeof(ContextActionBreakFree))]
    public static ActionsBuilder BreakFree(
        this ActionsBuilder builder,
        bool useCMB = false,
        bool useCMD = false,
        ActionsBuilder onSuccess = null,
        ActionsBuilder onFail = null)
    {
      var breakFree = ElementTool.Create<ContextActionBreakFree>();
      breakFree.UseCMB = useCMB;
      breakFree.UseCMD = useCMD;
      breakFree.Success = onSuccess?.Build() ?? Constants.Empty.Actions;
      breakFree.Failure = onFail?.Build() ?? Constants.Empty.Actions;
      return builder.Add(breakFree);
    }

    /// <summary>
    /// Adds <see cref="ContextActionBreathOfLife"/>
    /// </summary>
    [Implements(typeof(ContextActionBreathOfLife))]
    public static ActionsBuilder BreathOfLife(
        this ActionsBuilder builder, ContextDiceValue value)
    {
      var breathOfLife = ElementTool.Create<ContextActionBreathOfLife>();
      breathOfLife.Value = value;
      return builder.Add(breathOfLife);
    }

    /// <summary>
    /// Adds <see cref="ContextActionBreathOfMoney"/>
    /// </summary>
    [Implements(typeof(ContextActionBreathOfMoney))]
    public static ActionsBuilder BreathOfMoney(
        this ActionsBuilder builder, ContextValue minCoins, ContextValue maxCoins)
    {
      var breathOfMoney = ElementTool.Create<ContextActionBreathOfMoney>();
      breathOfMoney.MinCoins = minCoins;
      breathOfMoney.MaxCoins = maxCoins;
      return builder.Add(breathOfMoney);
    }

    /// <summary>
    /// Adds <see cref="ContextActionCastSpell"/>
    /// </summary>
    /// 
    /// <param name="spell"><see cref="Kingmaker.UnitLogic.Abilities.Blueprints.BlueprintAbility">BlueprintAbility</see></param>
    [Implements(typeof(ContextActionCastSpell))]
    public static ActionsBuilder CastSpell(
        this ActionsBuilder builder,
        string spell,
        bool castByTarget = false,
        ContextValue overrideDC = null,
        ContextValue overrideLevel = null)
    {
      var castSpell = ElementTool.Create<ContextActionCastSpell>();
      castSpell.m_Spell = BlueprintTool.GetRef<BlueprintAbilityReference>(spell);
      castSpell.CastByTarget = castByTarget;
      if (overrideDC != null)
      {
        castSpell.OverrideDC = true;
        castSpell.DC = overrideDC;
      }
      if (overrideLevel != null)
      {
        castSpell.OverrideSpellLevel = true;
        castSpell.SpellLevel = overrideLevel;
      }
      return builder.Add(castSpell);
    }

    /// <summary>
    /// Adds <see cref="ContextActionChangeSharedValue"/>
    /// </summary>
    [Implements(typeof(ContextActionChangeSharedValue))]
    public static ActionsBuilder SetSharedValue(
        this ActionsBuilder builder, AbilitySharedValue sharedValue, ContextValue setValue)
    {
      return ChangeSharedValue(builder, sharedValue, SharedValueChangeType.Set, set: setValue);
    }

    /// <inheritdoc cref="SetSharedValue"/>
    [Implements(typeof(ContextActionChangeSharedValue))]
    public static ActionsBuilder SetSharedValueToHD(
        this ActionsBuilder builder, AbilitySharedValue sharedValue)
    {
      return ChangeSharedValue(builder, sharedValue, SharedValueChangeType.SubHD);
    }

    /// <inheritdoc cref="SetSharedValue"/>
    [Implements(typeof(ContextActionChangeSharedValue))]
    public static ActionsBuilder AddToSharedValue(
        this ActionsBuilder builder, AbilitySharedValue sharedValue, ContextValue addValue)
    {
      return ChangeSharedValue(builder, sharedValue, SharedValueChangeType.Add, add: addValue);
    }

    /// <inheritdoc cref="SetSharedValue"/>
    [Implements(typeof(ContextActionChangeSharedValue))]
    public static ActionsBuilder MultiplySharedValue(
        this ActionsBuilder builder, AbilitySharedValue sharedValue, ContextValue multiplyValue)
    {
      return ChangeSharedValue(
          builder, sharedValue, SharedValueChangeType.Multiply, multiply: multiplyValue);
    }

    /// <inheritdoc cref="SetSharedValue"/>
    [Implements(typeof(ContextActionChangeSharedValue))]
    public static ActionsBuilder DivideSharedValueBy2(
        this ActionsBuilder builder, AbilitySharedValue sharedValue)
    {
      return ChangeSharedValue(builder, sharedValue, SharedValueChangeType.Div2);
    }

    /// <inheritdoc cref="SetSharedValue"/>
    [Implements(typeof(ContextActionChangeSharedValue))]
    public static ActionsBuilder DivideSharedValueBy4(
        this ActionsBuilder builder, AbilitySharedValue sharedValue)
    {
      return ChangeSharedValue(builder, sharedValue, SharedValueChangeType.Div4);
    }

    private static ActionsBuilder ChangeSharedValue(
        ActionsBuilder builder,
        AbilitySharedValue sharedValue,
        SharedValueChangeType type,
        ContextValue add = null,
        ContextValue set = null,
        ContextValue multiply = null)
    {
      var changeValue = ElementTool.Create<ContextActionChangeSharedValue>();
      changeValue.SharedValue = sharedValue;
      changeValue.Type = type;
      changeValue.AddValue = add;
      changeValue.SetValue = set;
      changeValue.MultiplyValue = multiply;
      return builder.Add(changeValue);
    }

    /// <summary>
    /// Adds <see cref="ContextActionClearSummonPool"/>
    /// </summary>
    /// 
    /// <param name="pool"><see cref="BlueprintSummonPool"/></param>
    [Implements(typeof(ContextActionClearSummonPool))]
    public static ActionsBuilder ClearSummonPool(this ActionsBuilder builder, string pool)
    {
      var clearSummons = ElementTool.Create<ContextActionClearSummonPool>();
      clearSummons.m_SummonPool = BlueprintTool.GetRef<BlueprintSummonPoolReference>(pool);
      return builder.Add(clearSummons);
    }

    /// <summary>
    /// Adds <see cref="ContextActionCombatManeuver"/>
    /// </summary>
    [Implements(typeof(ContextActionCombatManeuver))]
    public static ActionsBuilder CombatManeuver(
        this ActionsBuilder builder,
        CombatManeuver type,
        ActionsBuilder onSuccess,
        bool ignoreConcealment = false,
        bool batteringBlast = false,
        StatType useStat = StatType.Unknown,
        bool useKineticistMainStat = false,
        bool useCastingStat = false,
        bool useCasterLevelForBAB = false,
        bool useBestMentalStat = false)
    {
      var maneuver = ElementTool.Create<ContextActionCombatManeuver>();
      maneuver.Type = type;
      maneuver.OnSuccess = onSuccess.Build();
      maneuver.IgnoreConcealment = ignoreConcealment;
      maneuver.BatteringBlast = batteringBlast;
      maneuver.ReplaceStat =
          useStat != StatType.Unknown
              || useKineticistMainStat
              || useCastingStat
              || useCasterLevelForBAB
              || useBestMentalStat;
      maneuver.NewStat = useStat;
      maneuver.UseKineticistMainStat = useKineticistMainStat;
      maneuver.UseCastingStat = useCastingStat;
      maneuver.UseCasterLevelAsBaseAttack = useCasterLevelForBAB;
      maneuver.UseBestMentalStat = useBestMentalStat;
      return builder.Add(maneuver);
    }

    /// <summary>
    /// Adds <see cref="ContextActionCombatManeuverCustom"/>
    /// </summary>
    [Implements(typeof(ContextActionCombatManeuverCustom))]
    public static ActionsBuilder CustomCombatManeuver(
        this ActionsBuilder builder,
        CombatManeuver type,
        ActionsBuilder onSuccess = null,
        ActionsBuilder onFail = null)
    {
      var maneuver = ElementTool.Create<ContextActionCombatManeuverCustom>();
      maneuver.Type = type;
      maneuver.Success = onSuccess?.Build() ?? Constants.Empty.Actions;
      maneuver.Failure = onFail?.Build() ?? Constants.Empty.Actions;
      return builder.Add(maneuver);
    }

    /// <summary>
    /// Adds <see cref="ContextActionConditionalSaved"/>
    /// </summary>
    [Implements(typeof(ContextActionConditionalSaved))]
    public static ActionsBuilder AfterSavingThrow(
        this ActionsBuilder builder,
        ActionsBuilder ifPassed = null,
        ActionsBuilder ifFailed = null)
    {
      var onSave = ElementTool.Create<ContextActionConditionalSaved>();
      onSave.Succeed = ifPassed?.Build() ?? Constants.Empty.Actions;
      onSave.Failed = ifFailed?.Build() ?? Constants.Empty.Actions;
      return builder.Add(onSave);
    }

    /// <summary>
    /// Adds <see cref="ContextActionDealDamage"/>
    /// </summary>
    /// 
    /// <param name="sharedResult">If specified, the resulting damage is stored in this shared value.</param>
    /// <param name="criticalSharedResult">
    /// If specified and the associated attack roll is a critical, this shared value is set to 1.
    /// </param>
    [Implements(typeof(ContextActionDealDamage))]
    public static ActionsBuilder DealDamage(
        this ActionsBuilder builder,
        DamageTypeDescription type,
        ContextDiceValue value,
        bool dealHalfIfSaved = false,
        bool dealHalf = false,
        bool ignoreCrit = false,
        bool isAOE = false,
        int? minHPAfterDmg = null,
        AbilitySharedValue? sharedResult = null,
        AbilitySharedValue? criticalSharedResult = null)
    {
      return DealDamage(
          builder,
          ContextActionDealDamage.Type.Damage,
          dealHalfIfSaved,
          ignoreCrit,
          dmgType: type,
          value: value,
          dealHalf: dealHalf,
          isAOE: isAOE,
          minHPAfterDmg: minHPAfterDmg,
          sharedResult: sharedResult,
          criticalSharedResult: criticalSharedResult);
    }

    /// <inheritdoc cref="DealDamage"/>
    /// <param name="preRolledValue">Deals damage equal to this shared value.</param>
    [Implements(typeof(ContextActionDealDamage))]
    public static ActionsBuilder DealDamagePreRolled(
        this ActionsBuilder builder,
        DamageTypeDescription type,
        AbilitySharedValue preRolledValue,
        bool dealHalfIfSaved = false,
        bool dealHalf = false,
        bool alreadyHalved = false,
        bool ignoreCrit = false,
        int? minHPAfterDmg = null,
        AbilitySharedValue? sharedResult = null,
        AbilitySharedValue? criticalSharedResult = null)
    {
      return DealDamage(
          builder,
          ContextActionDealDamage.Type.Damage,
          dealHalfIfSaved,
          ignoreCrit,
          dmgType: type,
          preRolledValue: preRolledValue,
          dealHalf: dealHalf,
          alreadyHalved: alreadyHalved,
          minHPAfterDmg: minHPAfterDmg,
          sharedResult: sharedResult,
          criticalSharedResult: criticalSharedResult);
    }

    /// <inheritdoc cref="DealDamage"/>
    [Implements(typeof(ContextActionDealDamage))]
    public static ActionsBuilder DealAbilityDamage(
        this ActionsBuilder builder,
        StatType type,
        ContextDiceValue value,
        bool isDrain = false,
        bool dealHalfIfSaved = false,
        bool ignoreCrit = false,
        int? minStatAfterDmg = null,
        AbilitySharedValue? sharedResult = null,
        AbilitySharedValue? criticalSharedResult = null)
    {
      return DealDamage(
          builder,
          ContextActionDealDamage.Type.AbilityDamage,
          dealHalfIfSaved,
          ignoreCrit,
          statType: type,
          value: value,
          isDrain: isDrain,
          minHPAfterDmg: minStatAfterDmg,
          sharedResult: sharedResult,
          criticalSharedResult: criticalSharedResult);
    }

    /// <inheritdoc cref="DealDamage"/>
    [Implements(typeof(ContextActionDealDamage))]
    public static ActionsBuilder DealPermanentNegativeLevels(
        this ActionsBuilder builder,
        ContextDiceValue damageValue,
        bool dealHalfIfSaved = false,
        bool ignoreCrit = false,
        AbilitySharedValue? sharedResult = null,
        AbilitySharedValue? criticalSharedResult = null)
    {
      return DealDamage(
        builder,
        ContextActionDealDamage.Type.EnergyDrain,
        dealHalfIfSaved,
        ignoreCrit,
        drainType: EnergyDrainType.Permanent,
        value: damageValue,
        sharedResult: sharedResult,
        criticalSharedResult: criticalSharedResult);
    }

    /// <inheritdoc cref="DealDamage"/>
    [Implements(typeof(ContextActionDealDamage))]
    public static ActionsBuilder DealTempNegativeLevels(
        this ActionsBuilder builder,
        ContextDiceValue damageValue,
        ContextDurationValue duration,
        bool permanentOnFailedSave = false,
        bool dealHalfIfSaved = false,
        bool ignoreCrit = false,
        AbilitySharedValue? sharedResult = null,
        AbilitySharedValue? criticalSharedResult = null)
    {
      return DealDamage(
        builder,
        ContextActionDealDamage.Type.EnergyDrain,
        dealHalfIfSaved,
        ignoreCrit,
        drainType:
            permanentOnFailedSave
                ? EnergyDrainType.SaveOrBecamePermanent
                : EnergyDrainType.Temporary,
        duration: duration,
        value: damageValue,
        sharedResult: sharedResult,
        criticalSharedResult: criticalSharedResult);
    }

    private static ActionsBuilder DealDamage(
      ActionsBuilder builder,
      ContextActionDealDamage.Type type,
      bool halfIfSaved,
      bool ignoreCrit,
      bool dealHalf = false,
      bool alreadyHalved = false,
      bool isAOE = false,
      bool isDrain = false,
      ContextDiceValue value = null,
      DamageTypeDescription dmgType = null,
      ContextDurationValue duration = null,
      StatType? statType = null,
      EnergyDrainType? drainType = null,
      int? minHPAfterDmg = null,
      AbilitySharedValue? preRolledValue = null,
      AbilitySharedValue? sharedResult = null,
      AbilitySharedValue? criticalSharedResult = null)
    {
      var dmg = ElementTool.Create<ContextActionDealDamage>();
      dmg.m_Type = type;
      dmg.HalfIfSaved = halfIfSaved;
      dmg.IgnoreCritical = ignoreCrit;
      dmg.Half = dealHalf;
      dmg.AlreadyHalved = alreadyHalved;
      dmg.IsAoE = isAOE;
      dmg.Drain = isDrain;
      dmg.Value = value ?? Constants.Empty.DiceValue;

      if (dmgType is not null)
      {
        builder.Validate(dmgType);
        dmg.DamageType = dmgType;
      }
      if (duration is not null) { dmg.Duration = duration; }
      if (statType is not null) { dmg.AbilityType = statType.Value; }
      if (drainType is not null) { dmg.EnergyDrainType = drainType.Value; }
      if (minHPAfterDmg is not null)
      {
        dmg.UseMinHPAfterDamage = true;
        dmg.MinHPAfterDamage = minHPAfterDmg.Value;
      }
      if (preRolledValue is not null)
      {
        dmg.ReadPreRolledFromSharedValue = true;
        dmg.PreRolledSharedValue = preRolledValue.Value;
      }
      if (sharedResult is not null)
      {
        dmg.WriteResultToSharedValue = true;
        dmg.ResultSharedValue = sharedResult.Value;
      }
      if (criticalSharedResult is not null)
      {
        dmg.WriteCriticalToSharedValue = true;
        dmg.CriticalSharedValue = criticalSharedResult.Value;
      }
      return builder.Add(dmg);
    }

    /// <summary>
    /// Adds <see cref="ContextActionDealWeaponDamage"/>
    /// </summary>
    [Implements(typeof(ContextActionDealWeaponDamage))]
    public static ActionsBuilder DealWeaponDamage(this ActionsBuilder builder, bool allowRanged = false)
    {
      var dmg = ElementTool.Create<ContextActionDealWeaponDamage>();
      dmg.CanBeRanged = allowRanged;
      return builder.Add(dmg);
    }

    /// <summary>
    /// Adds <see cref="ContextActionDetectSecretDoors"/>
    /// </summary>
    [Implements(typeof(ContextActionDetectSecretDoors))]
    public static ActionsBuilder DetectSecretDoors(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionDetectSecretDoors>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionDevourBySwarm"/>
    /// </summary>
    [Implements(typeof(ContextActionDevourBySwarm))]
    public static ActionsBuilder DevourWithSwarm(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionDevourBySwarm>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionDisarm"/>
    /// </summary>
    [Implements(typeof(ContextActionDisarm))]
    public static ActionsBuilder Disarm(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionDisarm>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionDismissAreaEffect"/>
    /// </summary>
    [Implements(typeof(ContextActionDismissAreaEffect))]
    public static ActionsBuilder DismissAOE(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionDismissAreaEffect>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionDismount"/>
    /// </summary>
    [Implements(typeof(ContextActionDismount))]
    public static ActionsBuilder Dismount(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionDismount>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionDispelMagic"/>
    /// </summary>
    /// 
    /// <param name="checkEitherSchoolOrDescriptor">
    /// By default dispel only effects things matching both the required SpellSchool and SpellDescriptor. If this is
    /// true, effects only need to satisfy one or the other.
    /// </param>
    [Implements(typeof(ContextActionDispelMagic))]
    public static ActionsBuilder Dispel(
        this ActionsBuilder builder,
        ContextActionDispelMagic.BuffType type,
        RuleDispelMagic.CheckType checkType,
        ContextValue maxSpellLevel,
        bool onlyDispelEnemyBuffs = false,
        bool removeOnlyOne = false,
        int bonus = 0,
        ContextValue contextBonus = null,
        ActionsBuilder onSuccess = null,
        ActionsBuilder onFail = null,
        SpellSchool[] limitToSchools = null,
        SpellDescriptor? limitToDescriptor = null,
        bool checkEitherSchoolOrDescriptor = false,
        StatType skill = StatType.Unknown,
        ContextValue maxCasterLevel = null)
    {
      var dispel = ElementTool.Create<ContextActionDispelMagic>();
      dispel.m_BuffType = type;
      dispel.m_CheckType = checkType;
      dispel.m_MaxSpellLevel = maxSpellLevel;
      dispel.OnlyTargetEnemyBuffs = onlyDispelEnemyBuffs;
      dispel.m_StopAfterFirstRemoved = removeOnlyOne;
      dispel.CheckBonus = bonus;
      dispel.ContextBonus = contextBonus ?? 0;
      dispel.OnSuccess = onSuccess?.Build() ?? Constants.Empty.Actions;
      dispel.OnFail = onFail?.Build() ?? Constants.Empty.Actions;
      dispel.Schools = limitToSchools ?? Array.Empty<SpellSchool>();
      dispel.Descriptor = limitToDescriptor ?? SpellDescriptor.None;
      dispel.CheckSchoolOrDescriptor = checkEitherSchoolOrDescriptor;
      dispel.m_Skill = skill;

      if (maxCasterLevel is not null)
      {
        dispel.m_UseMaxCasterLevel = true;
        dispel.m_MaxCasterLevel = maxCasterLevel;
      }
      if ((dispel.IsSkillCheck && dispel.m_Skill == StatType.Unknown)
          || !dispel.IsSkillCheck && dispel.m_Skill != StatType.Unknown)
      {
        throw new InvalidCastException($"Mismatched CheckType and StatType: {checkType}, {skill}");
      }
      return builder.Add(dispel);
    }

    /// <summary>
    /// Adds <see cref="ContextActionDropItems"/>
    /// </summary>
    [Implements(typeof(ContextActionDropItems))]
    public static ActionsBuilder DropItems(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionDropItems>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionEnchantWornItem"/>
    /// </summary>
    /// 
    /// <param name="enchantment"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment">BlueprintItemEnchantment</see></param>
    [Implements(typeof(ContextActionEnchantWornItem))]
    public static ActionsBuilder EnchantWornItem(
        this ActionsBuilder builder,
        string enchantment,
        EquipSlotBase.SlotType slot,
        ContextDurationValue duration,
        bool onCaster = false,
        bool removeOnUnequip = false)
    {
      var enchant = ElementTool.Create<ContextActionEnchantWornItem>();
      enchant.m_Enchantment = BlueprintTool.GetRef<BlueprintItemEnchantmentReference>(enchantment);
      enchant.Slot = slot;
      enchant.DurationValue = duration;
      enchant.ToCaster = onCaster;
      enchant.RemoveOnUnequip = removeOnUnequip;
      return builder.Add(enchant);
    }

    /// <summary>
    /// Adds <see cref="ContextActionFinishObjective"/>
    /// </summary>
    [Implements(typeof(ContextActionFinishObjective))]
    public static ActionsBuilder FinishObjective(this ActionsBuilder builder, string objective)
    {
      var finish = ElementTool.Create<ContextActionFinishObjective>();
      finish.m_Objective = BlueprintTool.GetRef<BlueprintQuestObjectiveReference>(objective);
      return builder.Add(finish);
    }

    /// <summary>
    /// Adds <see cref="ContextActionForEachSwallowedUnit"/>
    /// </summary>
    [Implements(typeof(ContextActionForEachSwallowedUnit))]
    public static ActionsBuilder OnEachSwallowedUnit(
        this ActionsBuilder builder, ActionsBuilder onEachUnit)
    {
      var onUnits = ElementTool.Create<ContextActionForEachSwallowedUnit>();
      onUnits.Action = onEachUnit.Build();
      return builder.Add(onUnits);
    }

    /// <summary>
    /// Adds <see cref="ContextActionGiveExperience"/>
    /// </summary>
    [Implements(typeof(ContextActionGiveExperience))]
    public static ActionsBuilder GiveExperience(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionGiveExperience>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionGrapple"/>
    /// </summary>
    /// 
    /// <param name="casterBuff">
    /// <see cref="Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff">BlueprintBuff</see> applied for the duration of
    /// the grapple check.
    /// </param>
    /// <param name="targetBuff">
    /// <see cref="Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff">BlueprintBuff</see> applied for the duration of
    /// the grapple check.
    /// </param>
    [Implements(typeof(ContextActionGrapple))]
    public static ActionsBuilder Grapple(
        this ActionsBuilder builder, string casterBuff = null, string targetBuff = null)
    {
      var grapple = ElementTool.Create<ContextActionGrapple>();
      if (casterBuff != null) { grapple.m_CasterBuff = BlueprintTool.GetRef<BlueprintBuffReference>(casterBuff); }
      if (targetBuff != null) { grapple.m_TargetBuff = BlueprintTool.GetRef<BlueprintBuffReference>(targetBuff); }
      return builder.Add(grapple);
    }

    /// <summary>
    /// Adds <see cref="ContextActionHealEnergyDrain"/>
    /// </summary>
    [Implements(typeof(ContextActionHealEnergyDrain))]
    public static ActionsBuilder HealNegativeLevels(
        this ActionsBuilder builder,
        EnergyDrainHealType tempLevels = EnergyDrainHealType.None,
        EnergyDrainHealType permanentLevels = EnergyDrainHealType.None)
    {
      var heal = ElementTool.Create<ContextActionHealEnergyDrain>();
      heal.TemporaryNegativeLevelsHeal = tempLevels;
      heal.PermanentNegativeLevelsHeal = permanentLevels;
      return builder.Add(heal);
    }

    /// <summary>
    /// Adds <see cref="ContextActionHealStatDamage"/>
    /// </summary>
    /// 
    /// <param name="sharedResult">If specified, the amount of healing done is stored in this shared value.</param>
    [Implements(typeof(ContextActionHealStatDamage))]
    public static ActionsBuilder HealStatDamage(
        this ActionsBuilder builder,
        ContextActionHealStatDamage.StatDamageHealType type,
        ContextActionHealStatDamage.StatClass statClass,
        ContextDiceValue value = null,
        bool healDrain = false,
        AbilitySharedValue? sharedResult = null)
    {
      if (type == ContextActionHealStatDamage.StatDamageHealType.Dice && value == null)
      {
        throw new InvalidOperationException("Cannot use StatDamageHealType.Dice without a value.");
      }

      var heal = ElementTool.Create<ContextActionHealStatDamage>();
      heal.m_HealType = type;
      heal.m_StatClass = statClass;
      heal.Value = value;
      heal.HealDrain = healDrain;
      if (sharedResult != null)
      {
        heal.WriteResultToSharedValue = true;
        heal.ResultSharedValue = sharedResult.Value;
      }
      return builder.Add(heal);
    }

    /// <summary>
    /// Adds <see cref="ContextActionHealTarget"/>
    /// </summary>
    [Implements(typeof(ContextActionHealTarget))]
    public static ActionsBuilder HealTarget(this ActionsBuilder builder, ContextDiceValue value)
    {
      var heal = ElementTool.Create<ContextActionHealTarget>();
      heal.Value = value;
      return builder.Add(heal);
    }

    /// <summary>
    /// Adds <see cref="ContextActionHideInPlainSight"/>
    /// </summary>
    [Implements(typeof(ContextActionHideInPlainSight))]
    public static ActionsBuilder HideInPlainSight(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionHideInPlainSight>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionKill"/>
    /// </summary>
    [Implements(typeof(ContextActionKill))]
    public static ActionsBuilder Kill(
        this ActionsBuilder builder, UnitState.DismemberType dismember = UnitState.DismemberType.None)
    {
      var kill = ElementTool.Create<ContextActionKill>();
      kill.Dismember = dismember;
      return builder.Add(kill);
    }

    /// <summary>
    /// Adds <see cref="ContextActionKnockdownTarget"/>
    /// </summary>
    [Implements(typeof(ContextActionKnockdownTarget))]
    public static ActionsBuilder Knockdown(
        this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionKnockdownTarget>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionMakeKnowledgeCheck"/>
    /// </summary>
    [Implements(typeof(ContextActionMakeKnowledgeCheck))]
    public static ActionsBuilder KnowledgeCheck(
        this ActionsBuilder builder, ActionsBuilder onSuccess = null, ActionsBuilder onFail = null)
    {
      var knowledgeCheck = ElementTool.Create<ContextActionMakeKnowledgeCheck>();
      knowledgeCheck.SuccessActions = onSuccess?.Build() ?? Constants.Empty.Actions;
      knowledgeCheck.FailActions = onFail?.Build() ?? Constants.Empty.Actions;
      return builder.Add(knowledgeCheck);
    }

    /// <summary>
    /// Adds <see cref="ContextActionMarkForceDismemberOwner"/>
    /// </summary>
    [Implements(typeof(ContextActionMarkForceDismemberOwner))]
    public static ActionsBuilder MarkOwnerForDismemberment(
        this ActionsBuilder builder, UnitState.DismemberType type = UnitState.DismemberType.Normal)
    {
      var markForDismemberment = ElementTool.Create<ContextActionMarkForceDismemberOwner>();
      markForDismemberment.ForceDismemberType = type;
      return builder.Add(markForDismemberment);
    }

    /// <summary>
    /// Adds <see cref="ContextActionMeleeAttack"/>
    /// </summary>
    [Implements(typeof(ContextActionMeleeAttack))]
    public static ActionsBuilder MeleeAttack(
        this ActionsBuilder builder,
        bool autoCritThreat = false,
        bool autoCritConfirm = false,
        bool autoHit = false,
        bool ignoreStatBonus = false,
        bool selectNewTarget = false)
    {
      var attack = ElementTool.Create<ContextActionMeleeAttack>();
      attack.AutoCritThreat = autoCritThreat;
      attack.AutoCritConfirmation = autoCritConfirm;
      attack.AutoHit = autoHit;
      attack.IgnoreStatBonus = ignoreStatBonus;
      attack.SelectNewTarget = selectNewTarget;
      return builder.Add(attack);
    }

    /// <summary>
    /// Adds <see cref="ContextActionMount"/>
    /// </summary>
    [Implements(typeof(ContextActionMount))]
    public static ActionsBuilder Mount(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionMount>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionOnContextCaster"/>
    /// </summary>
    [Implements(typeof(ContextActionOnContextCaster))]
    public static ActionsBuilder OnCaster(this ActionsBuilder builder, ActionsBuilder actions)
    {
      var onCaster = ElementTool.Create<ContextActionOnContextCaster>();
      onCaster.Actions = actions.Build();
      return builder.Add(onCaster);
    }

    /// <summary>
    /// Adds <see cref="ContextActionOnOwner"/>
    /// </summary>
    [Implements(typeof(ContextActionOnOwner))]
    public static ActionsBuilder OnOwner(this ActionsBuilder builder, ActionsBuilder actions)
    {
      var onOwner = ElementTool.Create<ContextActionOnOwner>();
      onOwner.Actions = actions.Build();
      return builder.Add(onOwner);
    }

    /// <summary>
    /// Adds <see cref="ContextActionOnRandomAreaTarget"/>
    /// </summary>
    /// 
    /// <remarks>
    /// <list type="bullet">
    ///   <item>
    ///     <description>The component's OnEnemies field is unused. It only targets enemies.</description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///       Only works inside of
    ///       <see cref="Kingmaker.UnitLogic.Abilities.Components.AreaEffects.AbilityAreaEffectRunAction">AbilityAreaEffectRunAction</see>
    ///     </description>
    ///   </item>
    /// </list>
    /// </remarks>
    [Implements(typeof(ContextActionOnRandomAreaTarget))]
    public static ActionsBuilder OnRandomEnemyInAOE(this ActionsBuilder builder, ActionsBuilder actions)
    {
      var onEnemy = ElementTool.Create<ContextActionOnRandomAreaTarget>();
      onEnemy.Actions = actions.Build();
      return builder.Add(onEnemy);
    }

    /// <summary>
    /// Adds <see cref="ContextActionOnRandomTargetsAround"/>
    /// </summary>
    /// 
    /// <param name="ignoreFact">
    /// <see cref="Kingmaker.Blueprints.Facts.BlueprintUnitFact">BlueprintUnitFact</see> units with this fact are
    /// ignored.
    /// </param>
    [Implements(typeof(ContextActionOnRandomTargetsAround))]
    public static ActionsBuilder OnRandomUnitNearTarget(
        this ActionsBuilder builder,
        ActionsBuilder actions,
        int radiusInFeet,
        int numTargets = 1,
        bool targetEnemies = true,
        string ignoreFact = null)
    {
      var onUnit = ElementTool.Create<ContextActionOnRandomTargetsAround>();
      onUnit.Actions = actions.Build();
      onUnit.Radius = new Feet(radiusInFeet);
      onUnit.NumberOfTargets = numTargets;
      onUnit.OnEnemies = targetEnemies;
      onUnit.m_FilterNoFact = BlueprintTool.GetRef<BlueprintUnitFactReference>(ignoreFact);
      return builder.Add(onUnit);
    }

    /// <summary>
    /// Adds <see cref="ContextActionOnSwarmTargets"/>
    /// </summary>
    [Implements(typeof(ContextActionOnSwarmTargets))]
    public static ActionsBuilder OnSwarmTargets(this ActionsBuilder builder, ActionsBuilder actions)
    {
      var onTarget = ElementTool.Create<ContextActionOnSwarmTargets>();
      onTarget.Actions = actions.Build();
      return builder.Add(onTarget);
    }

    /// <summary>
    /// Adds <see cref="ContextActionPartyMembers"/>
    /// </summary>
    [Implements(typeof(ContextActionPartyMembers))]
    public static ActionsBuilder OnPartyMembers(
        this ActionsBuilder builder, ActionsBuilder actions)
    {
      var onParty = ElementTool.Create<ContextActionPartyMembers>();
      onParty.Action = actions.Build();
      return builder.Add(onParty);
    }

    /// <summary>
    /// Adds <see cref="ContextActionProjectileFx"/>
    /// </summary>
    /// 
    /// <param name="projectile"><see cref="BlueprintProjectile"/></param>
    [Implements(typeof(ContextActionProjectileFx))]
    public static ActionsBuilder ProjectileFx(this ActionsBuilder builder, string projectile)
    {
      var projectileFx = ElementTool.Create<ContextActionProjectileFx>();
      projectileFx.m_Projectile = BlueprintTool.GetRef<BlueprintProjectileReference>(projectile);
      return builder.Add(projectileFx);
    }

    /// <summary>
    /// Adds <see cref="ContextActionProvokeAttackFromCaster"/>
    /// </summary>
    [Implements(typeof(ContextActionProvokeAttackFromCaster))]
    public static ActionsBuilder ProvokeOpportunityAttackFromCaster(
        this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionProvokeAttackFromCaster>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionProvokeAttackOfOpportunity"/>
    /// </summary>
    [Implements(typeof(ContextActionProvokeAttackOfOpportunity))]
    public static ActionsBuilder ProvokeOpportunityAttack(
        this ActionsBuilder builder, bool casterProvokes = false)
    {
      var attack = ElementTool.Create<ContextActionProvokeAttackOfOpportunity>();
      attack.ApplyToCaster = casterProvokes;
      return builder.Add(attack);
    }

    /// <summary>
    /// Adds <see cref="ContextActionPush"/>
    /// </summary>
    [Implements(typeof(ContextActionPush))]
    public static ActionsBuilder Push(
        this ActionsBuilder builder,
        ContextValue distance,
        bool provokeOpportunityAttack = false)
    {
      var push = ElementTool.Create<ContextActionPush>();
      push.Distance = distance;
      push.ProvokeAttackOfOpportunity = provokeOpportunityAttack;
      return builder.Add(push);
    }

    /// <summary>
    /// Adds <see cref="ContextActionRandomize"/>
    /// </summary>
    /// 
    /// <param name="weightedActions">
    /// Pair of <see cref="ActionsBuilder"/> and an int representing the relative probability of that action compared
    /// to the rest of the entries. These map to <see cref="ContextActionRandomize.ActionWrapper"/>.
    /// </param>
    [Implements(typeof(ContextActionRandomize))]
    public static ActionsBuilder RandomActions(
        this ActionsBuilder builder,
        params (ActionsBuilder actions, int weight)[] weightedActions)
    {
      var actions = ElementTool.Create<ContextActionRandomize>();
      actions.m_Actions =
          weightedActions
              .Select(
                  weightedAction => new ContextActionRandomize.ActionWrapper
                  {
                    Action = weightedAction.actions.Build(),
                    Weight = weightedAction.weight
                  })
              .ToArray();
      return builder.Add(actions);
    }

    /// <summary>
    /// Adds <see cref="ContextActionRangedAttack"/>
    /// </summary>
    [Implements(typeof(ContextActionRangedAttack))]
    public static ActionsBuilder RangedAttack(
        this ActionsBuilder builder,
        bool autoCritThreat = false,
        bool autoCritConfirm = false,
        bool autoHit = false,
        bool ignoreStatBonus = false,
        bool selectNewTarget = false)
    {
      var attack = ElementTool.Create<ContextActionRangedAttack>();
      attack.AutoCritThreat = autoCritThreat;
      attack.AutoCritConfirmation = autoCritConfirm;
      attack.AutoHit = autoHit;
      attack.IgnoreStatBonus = ignoreStatBonus;
      attack.SelectNewTarget = selectNewTarget;
      return builder.Add(attack);
    }

    /// <summary>
    /// Adds <see cref="ContextActionRecoverItemCharges"/>
    /// </summary>
    /// 
    /// <param name="item"><see cref="Kingmaker.Blueprints.Items.Equipment.BlueprintItemEquipment">BlueprintItemEquipment</see></param>
    [Implements(typeof(ContextActionRecoverItemCharges))]
    public static ActionsBuilder RecoverItemCharges(this ActionsBuilder builder, string item, int charges = 1)
    {
      var recoverCharges = ElementTool.Create<ContextActionRecoverItemCharges>();
      recoverCharges.m_Item = BlueprintTool.GetRef<BlueprintItemEquipmentReference>(item);
      recoverCharges.ChargesRecoverCount = charges;
      return builder.Add(recoverCharges);
    }

    /// <summary>
    /// Adds <see cref="ContextActionReduceBuffDuration"/>
    /// </summary>
    /// 
    /// <param name="buff"><see cref="Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff">BlueprintBuff</see></param>
    [Implements(typeof(ContextActionReduceBuffDuration))]
    public static ActionsBuilder ChangeBuffDuration(
        this ActionsBuilder builder,
        string buff,
        ContextDurationValue duration,
        bool increase,
        bool onTarget = false)
    {
      var changeDuration = ElementTool.Create<ContextActionReduceBuffDuration>();
      changeDuration.m_TargetBuff = BlueprintTool.GetRef<BlueprintBuffReference>(buff);
      changeDuration.DurationValue = duration;
      changeDuration.Increase = increase;
      changeDuration.ToTarget = onTarget;
      return builder.Add(changeDuration);
    }

    /// <summary>
    /// Adds <see cref="ContextActionRemoveBuff"/>
    /// </summary>
    /// 
    /// <param name="buff"><see cref="Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff">BlueprintBuff</see></param>
    [Implements(typeof(ContextActionRemoveBuff))]
    public static ActionsBuilder RemoveBuff(
        this ActionsBuilder builder, string buff, bool removeRank = false, bool toCaster = false)
    {
      var removeBuff = ElementTool.Create<ContextActionRemoveBuff>();
      removeBuff.m_Buff = BlueprintTool.GetRef<BlueprintBuffReference>(buff);
      removeBuff.RemoveRank = removeRank;
      removeBuff.ToCaster = toCaster;
      return builder.Add(removeBuff);
    }

    /// <summary>
    /// Adds <see cref="ContextActionRemoveBuffsByDescriptor"/>
    /// </summary>
    [Implements(typeof(ContextActionRemoveBuffsByDescriptor))]
    public static ActionsBuilder RemoveBuffsWithDescriptor(
        this ActionsBuilder builder, SpellDescriptor descriptor, bool includeThisBuff = false)
    {
      var removeBuffs = ElementTool.Create<ContextActionRemoveBuffsByDescriptor>();
      removeBuffs.SpellDescriptor = descriptor;
      removeBuffs.NotSelf = !includeThisBuff;
      return builder.Add(removeBuffs);
    }

    /// <summary>
    /// Adds <see cref="ContextActionRemoveBuffSingleStack"/>
    /// </summary>
    /// 
    /// <param name="buff"><see cref="Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff">BlueprintBuff</see></param>
    [Implements(typeof(ContextActionRemoveBuffSingleStack))]
    public static ActionsBuilder RemoveBuffStack(this ActionsBuilder builder, string buff)
    {
      var removeStack = ElementTool.Create<ContextActionRemoveBuffSingleStack>();
      removeStack.m_TargetBuff = BlueprintTool.GetRef<BlueprintBuffReference>(buff);
      return builder.Add(removeStack);
    }

    /// <summary>
    /// Adds <see cref="ContextActionRemoveDeathDoor"/>
    /// </summary>
    [Implements(typeof(ContextActionRemoveDeathDoor))]
    public static ActionsBuilder RemoveDeathDoor(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionRemoveDeathDoor>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionRemoveSelf"/>
    /// </summary>
    /// 
    /// <remarks>Only works on buffs and area effects.</remarks>
    [Implements(typeof(ContextActionRemoveSelf))]
    public static ActionsBuilder RemoveSelf(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionRemoveSelf>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionRepeatedActions"/>
    /// </summary>
    [Implements(typeof(ContextActionRepeatedActions))]
    public static ActionsBuilder RepeatActions(
        this ActionsBuilder builder, ActionsBuilder actions, ContextDiceValue times)
    {
      var repeat = ElementTool.Create<ContextActionRepeatedActions>();
      repeat.Actions = actions.Build();
      repeat.Value = times;
      return builder.Add(repeat);
    }

    /// <summary>
    /// Adds <see cref="ContextActionRestoreSpells"/>
    /// </summary>
    /// 
    /// <param name="spellbooks"><see cref="BlueprintSpellbook"/></param>
    [Implements(typeof(ContextActionRestoreSpells))]
    public static ActionsBuilder RestoreSpells(this ActionsBuilder builder, params string[] spellbooks)
    {
      var restoreSpells = ElementTool.Create<ContextActionRestoreSpells>();
      restoreSpells.m_Spellbooks =
          spellbooks
              .Select(spellbook => BlueprintTool.GetRef<BlueprintSpellbookReference>(spellbook))
              .ToArray();
      return builder.Add(restoreSpells);
    }

    /// <summary>
    /// Adds <see cref="ContextActionResurrect"/>
    /// </summary>
    /// 
    /// <param name="healPercent">Percentage of health after resurrection as a float between 0.0 and 1.0.</param>
    /// <param name="resurrectBuff">
    /// <see cref="Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff">BlueprintBuff</see> which replces the default
    /// resurrection buff. Must contain a <see cref="Kingmaker.UnitLogic.Buffs.Components.ResurrectionLogic">ResurrectionLogic</see>
    /// </param>
    [Implements(typeof(ContextActionResurrect))]
    public static ActionsBuilder Resurrect(
        this ActionsBuilder builder, float healPercent, string resurrectBuff = null)
    {
      return Resurrect(builder, resurrectBuff, healPercent, false);
    }

    /// <inheritdoc cref="Resurrect"/>
    [Implements(typeof(ContextActionResurrect))]
    public static ActionsBuilder ResurrectAndFullRestore(this ActionsBuilder builder, string resurrectBuff = null)
    {
      return Resurrect(builder, resurrectBuff, 0.0f, true);
    }

    private static ActionsBuilder Resurrect(
        ActionsBuilder builder, string resurrectBuff, float healPercent, bool fullRestore)
    {
      var resurrect = ElementTool.Create<ContextActionResurrect>();
      resurrect.m_CustomResurrectionBuff =
          BlueprintTool.GetRef<BlueprintBuffReference>(resurrectBuff);
      resurrect.ResultHealth = healPercent;
      resurrect.FullRestore = fullRestore;
      return builder.Add(resurrect);
    }

    /// <summary>
    /// Adds <see cref="ContextActionSavingThrow"/>
    /// </summary>
    /// 
    /// <param name="fromBuff">
    /// If this is true, onResult should have a <see cref="ContextActionConditionalSaved"/> with
    /// <see cref="ContextActionApplyBuff"/> in it's Succeed <see cref="Kingmaker.ElementsSystem.ActionList">ActionList</see>.
    /// The buff associated with that component will be attached to the
    /// <see cref="Kingmaker.RuleSystem.Rules.RuleSavingThrow">RuleSavingThrow</see>.
    /// </param>
    [Implements(typeof(ContextActionSavingThrow))]
    public static ActionsBuilder SavingThrow(
        this ActionsBuilder builder,
        SavingThrowType type,
        ActionsBuilder onResult,
        ContextValue customDC = null,
        bool fromBuff = false,
        params (ConditionsBuilder conditions, ContextValue value)[] conditionalDCModifiers)
    {
      var savingThrow = ElementTool.Create<ContextActionSavingThrow>();
      savingThrow.Type = type;
      savingThrow.Actions = onResult.Build();
      savingThrow.FromBuff = fromBuff;
      savingThrow.m_ConditionalDCIncrease =
          conditionalDCModifiers
              .Select(modifier =>
                  new ContextActionSavingThrow.ConditionalDCIncrease
                  {
                    Condition = modifier.conditions.Build(),
                    Value = modifier.value
                  })
              .ToArray();

      if (customDC is not null)
      {
        savingThrow.HasCustomDC = true;
        savingThrow.CustomDC = customDC;
      }
      return builder.Add(savingThrow);
    }

    /// <summary>
    /// Adds <see cref="ContextActionSelectByValue"/>
    /// </summary>
    [Implements(typeof(ContextActionSelectByValue))]
    public static ActionsBuilder RunActionWithGreatestValue(
        this ActionsBuilder builder, params (ContextValue value, ActionsBuilder action)[] actionVariants)
    {
      var select = ElementTool.Create<ContextActionSelectByValue>();
      select.m_Type = ContextActionSelectByValue.SelectionType.Greatest;
      select.m_Variants =
          actionVariants
              .Select(variant =>
                  new ContextActionSelectByValue.ValueAndAction
                  {
                    Value = variant.value,
                    Action = variant.action.Build()
                  })
              .ToArray();
      return builder.Add(select);
    }

    /// <summary>
    /// Adds <see cref="ContextActionSkillCheck"/>
    /// </summary>
    [Implements(typeof(ContextActionSkillCheck))]
    public static ActionsBuilder SkillCheck(
        this ActionsBuilder builder,
        StatType skill,
        ContextValue customDC = null,
        ActionsBuilder onSuccess = null,
        ActionsBuilder onFail = null,
        params (ConditionsBuilder condition, ContextValue value)[] dcModifiers)
    {
      return SkillCheck(
          builder, skill, customDC, false, onSuccess, onFail: onFail, dcModifiers: dcModifiers);
    }

    /// <inheritdoc cref="SkillCheck"/>
    [Implements(typeof(ContextActionSkillCheck))]
    public static ActionsBuilder SkillCheckWithFailureDegrees(
        this ActionsBuilder builder,
        StatType skill,
        ContextValue customDC = null,
        ActionsBuilder onSuccess = null,
        ActionsBuilder onFailBy5to10 = null,
        ActionsBuilder onFailBy10orMore = null,
        params (ConditionsBuilder condition, ContextValue value)[] dcModifiers)
    {
      return SkillCheck(
          builder,
          skill,
          customDC,
          true,
          onSuccess,
          onFailBy5to10: onFailBy5to10,
          onFailBy10orMore: onFailBy10orMore,
          dcModifiers: dcModifiers);
    }

    private static ActionsBuilder SkillCheck(
        ActionsBuilder builder,
        StatType skill,
        ContextValue customDC,
        bool calculateDCDiff,
        ActionsBuilder onSuccess,
        (ConditionsBuilder condition, ContextValue value)[] dcModifiers,
        ActionsBuilder onFail = null,
        ActionsBuilder onFailBy5to10 = null,
        ActionsBuilder onFailBy10orMore = null)
    {
      var skillCheck = ElementTool.Create<ContextActionSkillCheck>();
      skillCheck.Stat = skill;
      if (customDC is not null)
      {
        skillCheck.UseCustomDC = true;
        skillCheck.CustomDC = customDC;
      }
      skillCheck.Success = onSuccess?.Build() ?? Constants.Empty.Actions;
      skillCheck.Failure = onFail?.Build() ?? Constants.Empty.Actions;
      skillCheck.FailureDiffMoreOrEqual5Less10 = onFailBy5to10?.Build() ?? Constants.Empty.Actions;
      skillCheck.FailureDiffMoreOrEqual10 = onFailBy10orMore?.Build() ?? Constants.Empty.Actions;
      skillCheck.CalculateDCDifference = calculateDCDiff;
      skillCheck.m_ConditionalDCIncrease =
          dcModifiers
              .Select(modifier =>
                  new ContextActionSkillCheck.ConditionalDCIncrease
                  {
                    Condition = modifier.condition.Build(),
                    Value = modifier.value
                  })
              .ToArray();
      return builder.Add(skillCheck);
    }

    /// <summary>
    /// Adds <see cref="ContextActionsOnPet"/>
    /// </summary>
    [Implements(typeof(ContextActionsOnPet))]
    public static ActionsBuilder OnPets(
        this ActionsBuilder builder,
        ActionsBuilder actions,
        bool anyPetType = false,
        PetType type = PetType.AnimalCompanion)
    {
      var onPets = ElementTool.Create<ContextActionsOnPet>();
      onPets.Actions = actions.Build();
      onPets.AllPets = anyPetType;
      onPets.PetType = type;
      return builder.Add(onPets);
    }

    /// <summary>
    /// Adds <see cref="ContextActionSpawnAreaEffect"/>
    /// </summary>
    /// 
    /// <param name="aoe"><see cref="Kingmaker.UnitLogic.Abilities.Blueprints.BlueprintAbilityAreaEffect">BlueprintAbilityAreaEffect</see></param>
    [Implements(typeof(ContextActionSpawnAreaEffect))]
    public static ActionsBuilder SpawnAOE(
        this ActionsBuilder builder, string aoe, ContextDurationValue duration)
    {
      var spawnAOE = ElementTool.Create<ContextActionSpawnAreaEffect>();
      spawnAOE.m_AreaEffect = BlueprintTool.GetRef<BlueprintAbilityAreaEffectReference>(aoe);
      spawnAOE.DurationValue = duration;
      return builder.Add(spawnAOE);
    }

    /// <summary>
    /// Adds <see cref="ContextActionSpawnControllableProjectile"/>
    /// </summary>
    /// 
    /// <param name="projectile"><see cref="BlueprintControllableProjectile"/></param>
    /// <param name="buff"><see cref="Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff">BlueprintBuff</see></param>
    [Implements(typeof(ContextActionSpawnControllableProjectile))]
    public static ActionsBuilder SpawnControllableProjectile(
        this ActionsBuilder builder, string projectile, string buff)
    {
      var spawnProjectile = ElementTool.Create<ContextActionSpawnControllableProjectile>();
      spawnProjectile.ControllableProjectile =
          BlueprintTool.GetRef<BlueprintControllableProjectileReference>(projectile);
      spawnProjectile.AssociatedCasterBuff = BlueprintTool.GetRef<BlueprintBuffReference>(buff);
      return builder.Add(spawnProjectile);
    }

    /// <summary>
    /// Adds <see cref="ContextActionSpawnMonster"/>
    /// </summary>
    /// 
    /// <param name="monster"><see cref="BlueprintUnit"/></param>
    [Implements(typeof(ContextActionSpawnMonster))]
    public static ActionsBuilder SpawnMonster(
        this ActionsBuilder builder,
        string monster,
        ContextDiceValue count,
        ContextDurationValue duration,
        ActionsBuilder onSpawn = null,
        ContextValue level = null,
        bool controllable = false,
        bool linkToCaster = true)
    {
      return SpawnMonsterInternal(
        builder,
        monster,
        count,
        duration,
        onSpawn,
        level,
        controllable,
        linkToCaster);
    }

    /// <inheritdoc cref="SpawnMonster"/>
    /// <param name="summonPool"><see cref="BlueprintSummonPool"/></param>
    [Implements(typeof(ContextActionSpawnMonster))]
    public static ActionsBuilder SpawnMonsterUsingSummonPool(
        this ActionsBuilder builder,
        string monster,
        string summonPool,
        ContextDiceValue count,
        ContextDurationValue duration,
        bool useSummonPoolLimit = false,
        ActionsBuilder onSpawn = null,
        ContextValue level = null,
        bool controllable = false,
        bool linkToCaster = true)
    {
      return SpawnMonsterInternal(
        builder,
        monster,
        count,
        duration,
        onSpawn,
        level,
        controllable,
        linkToCaster,
        summonPool: summonPool,
        useSummonPoolLimit: useSummonPoolLimit);
    }

    private static ActionsBuilder SpawnMonsterInternal(
        this ActionsBuilder builder,
        string monster,
        ContextDiceValue count,
        ContextDurationValue duration,
        ActionsBuilder onSpawn,
        ContextValue level,
        bool controllable,
        bool linkToCaster,
        string summonPool = null,
        bool useSummonPoolLimit = false)
    {
      var spawn = ElementTool.Create<ContextActionSpawnMonster>();
      spawn.m_Blueprint = BlueprintTool.GetRef<BlueprintUnitReference>(monster);
      spawn.CountValue = count;
      spawn.DurationValue = duration;
      spawn.AfterSpawn = onSpawn?.Build() ?? Constants.Empty.Actions;
      spawn.LevelValue = level ?? 0;
      spawn.IsDirectlyControllable = controllable;
      spawn.DoNotLinkToCaster = !linkToCaster;

      spawn.m_SummonPool =
          summonPool is null
              ? null
              : BlueprintTool.GetRef<BlueprintSummonPoolReference>(summonPool);
      spawn.UseLimitFromSummonPool = useSummonPoolLimit;
      return builder.Add(spawn);
    }

    /// <summary>
    /// Adds <see cref="ContextActionSpawnUnlinkedMonster"/>
    /// </summary>
    /// 
    /// <param name="monster"><see cref="BlueprintUnit"/></param>
    [Implements(typeof(ContextActionSpawnUnlinkedMonster))]
    public static ActionsBuilder SpawnMonsterUnlinked(this ActionsBuilder builder, string monster)
    {
      var spawn = ElementTool.Create<ContextActionSpawnUnlinkedMonster>();
      spawn.m_Blueprint = BlueprintTool.GetRef<BlueprintUnitReference>(monster);
      return builder.Add(spawn);
    }

    /// <summary>
    /// Adds <see cref="ContextActionSpendAttackOfOpportunity"/>
    /// </summary>
    [Implements(typeof(ContextActionSpendAttackOfOpportunity))]
    public static ActionsBuilder SpendOpportunityAttack(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionSpendAttackOfOpportunity>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionStealBuffs"/>
    /// </summary>
    [Implements(typeof(ContextActionStealBuffs))]
    public static ActionsBuilder StealBuffs(this ActionsBuilder builder, SpellDescriptor descriptor)
    {
      var steal = ElementTool.Create<ContextActionStealBuffs>();
      steal.m_Descriptor = descriptor;
      return builder.Add(steal);
    }

    /// <summary>
    /// Adds <see cref="ContextActionSwallowWhole"/>
    /// </summary>
    /// 
    /// <param name="buff"><see cref="Kingmaker.UnitLogic.Buffs.Blueprints.BlueprintBuff">BlueprintBuff</see></param>
    [Implements(typeof(ContextActionSwallowWhole))]
    public static ActionsBuilder SwallowWhole(this ActionsBuilder builder, string buff = null)
    {
      var swallow = ElementTool.Create<ContextActionSwallowWhole>();
      swallow.m_TargetBuff =
          buff is null
              ? null
              : BlueprintTool.GetRef<BlueprintBuffReference>(buff);
      return builder.Add(swallow);
    }

    /// <summary>
    /// Adds <see cref="ContextActionSwarmTarget"/>
    /// </summary>
    [Implements(typeof(ContextActionSwarmTarget))]
    public static ActionsBuilder AddToSwarmTargets(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionSwarmTarget>());
    }

    /// <inheritdoc cref="AddToSwarmTargets"/>
    [Implements(typeof(ContextActionSwarmTarget))]
    public static ActionsBuilder RemoveFromSwarmTargets(this ActionsBuilder builder)
    {
      var removeTarget = ElementTool.Create<ContextActionSwarmTarget>();
      removeTarget.Remove = true;
      return builder.Add(removeTarget);
    }

    /// <summary>
    /// Adds <see cref="ContextActionTranslocate"/>
    /// </summary>
    [Implements(typeof(ContextActionTranslocate))]
    public static ActionsBuilder Teleport(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionTranslocate>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionUnsummon"/>
    /// </summary>
    [Implements(typeof(ContextActionUnsummon))]
    public static ActionsBuilder Unsummon(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionUnsummon>());
    }

    /// <summary>
    /// Adds <see cref="ContextRestoreResource"/>
    /// </summary>
    /// 
    /// <param name="resource"><see cref="BlueprintAbilityResource"/></param>
    [Implements(typeof(ContextRestoreResource))]
    public static ActionsBuilder RestoreResource(
        this ActionsBuilder builder, string resource, ContextValue amount = null)
    {
      var restore = ElementTool.Create<ContextRestoreResource>();
      restore.m_Resource = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(resource);

      if (amount != null)
      {
        restore.ContextValueRestoration = true;
        restore.Value = amount;
      }
      return builder.Add(restore);
    }

    /// <summary>
    /// Adds <see cref="ContextRestoreResource"/>
    /// </summary>
    [Implements(typeof(ContextRestoreResource))]
    public static ActionsBuilder RestoreAllResourcesToFull(this ActionsBuilder builder)
    {
      var restore = ElementTool.Create<ContextRestoreResource>();
      restore.m_IsFullRestoreAllResources = true;
      return builder.Add(restore);
    }

    /// <summary>
    /// Adds <see cref="ContextSpendResource"/>
    /// </summary>
    /// 
    /// <param name="resource"><see cref="BlueprintAbilityResource"/></param>
    [Implements(typeof(ContextSpendResource))]
    public static ActionsBuilder SpendResource(
        this ActionsBuilder builder, string resource, ContextValue amount = null)
    {
      var spend = ElementTool.Create<ContextSpendResource>();
      spend.m_Resource = BlueprintTool.GetRef<BlueprintAbilityResourceReference>(resource);

      if (amount != null)
      {
        spend.ContextValueSpendure = true;
        spend.Value = amount;
      }
      return builder.Add(spend);
    }

    // Default GUIDs for Demoralize buffs
    private const string Shaken = "25ec6cb6ab1845c48a95f9c20b034220";
    private const string Frightened = "f08a7239aa961f34c8301518e71d4cdf";
    private const string DisplayWeaponProwess = "ac8d4d2e375a8c841a19ed46696e5af2";
    private const string ShatterConfidenceFeature = "14225a2e4561bfd46874c9a4a97e7133";
    private const string ShatterConfidenceBuff = "51f5a63f1a0cb9047acdad77fc437312";

    /// <summary>
    /// Adds <see cref="Demoralize"/>
    /// </summary>
    [Implements(typeof(Demoralize))]
    public static ActionsBuilder Demoralize(
        this ActionsBuilder builder,
        int bonus = 0,
        bool dazzlingDisplay = false,
        string effect = Shaken,
        string greaterEffect = Frightened,
        string swordlordProwess = DisplayWeaponProwess,
        string shatterConfidenceFeature = ShatterConfidenceFeature,
        string shatterConfidenceBuff = ShatterConfidenceBuff,
        ActionsBuilder tricksterRank3Actions = null)
    {
      var demoralize = ElementTool.Create<Demoralize>();
      demoralize.Bonus = bonus;
      demoralize.DazzlingDisplay = dazzlingDisplay;
      demoralize.m_Buff = BlueprintTool.GetRef<BlueprintBuffReference>(effect);
      demoralize.m_GreaterBuff = BlueprintTool.GetRef<BlueprintBuffReference>(greaterEffect);
      demoralize.m_SwordlordProwessFeature =
          BlueprintTool.GetRef<BlueprintFeatureReference>(swordlordProwess);
      demoralize.m_ShatterConfidenceFeature =
          BlueprintTool.GetRef<BlueprintFeatureReference>(shatterConfidenceFeature);
      demoralize.m_ShatterConfidenceBuff =
          BlueprintTool.GetRef<BlueprintBuffReference>(shatterConfidenceBuff);
      demoralize.TricksterRank3Actions = tricksterRank3Actions?.Build() ?? Constants.Empty.Actions;
      return builder.Add(demoralize);
    }

    /// <summary>
    /// Adds <see cref="EnhanceWeapon"/>
    /// </summary>
    /// 
    /// <param name="enhancements"><see cref="Kingmaker.Blueprints.Items.Ecnchantments.BlueprintItemEnchantment">BlueprintItemEnchantment</see></param>
    [Implements(typeof(EnhanceWeapon))]
    public static ActionsBuilder MagicWeapon(
        this ActionsBuilder builder,
        string[] enhancements,
        ContextDurationValue duration,
        ContextValue level,
        bool greater = false,
        bool useSecondaryHand = false)
    {
      return EnhanceWeaponInternal(
          builder,
          EnhanceWeapon.EnchantmentApplyType.MagicWeapon,
          enhancements,
          duration,
          level,
          greater,
          useSecondaryHand: useSecondaryHand);
    }

    /// <inheritdoc cref="MagicWeapon"/>
    [Implements(typeof(EnhanceWeapon))]
    public static ActionsBuilder MagicFang(
        this ActionsBuilder builder,
        string[] enhancements,
        ContextDurationValue duration,
        ContextValue level,
        bool greater = false)
    {
      return EnhanceWeaponInternal(
          builder,
          EnhanceWeapon.EnchantmentApplyType.MagicFang,
          enhancements,
          duration,
          level,
          greater);
    }

    private static ActionsBuilder EnhanceWeaponInternal(
        ActionsBuilder builder,
        EnhanceWeapon.EnchantmentApplyType type,
        string[] enhancements,
        ContextDurationValue duration,
        ContextValue level,
        bool greater,
        bool useSecondaryHand = false)
    {
      var enhance = ElementTool.Create<EnhanceWeapon>();
      enhance.EnchantmentType = type;
      enhance.m_Enchantment =
          enhancements
              .Select(
                  enhancement =>
                      BlueprintTool.GetRef<BlueprintItemEnchantmentReference>(enhancement))
              .ToArray();
      enhance.DurationValue = duration;
      enhance.EnchantLevel = level;
      enhance.Greater = greater;
      enhance.UseSecondaryHand = useSecondaryHand;
      return builder.Add(enhance);
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.UnitLogic.Mechanics.Actions.SwordlordAdaptiveTacticsAdd">SwordlordAdaptiveTacticsAdd</see>
    /// </summary>
    /// 
    /// <param name="source"><see cref="Kingmaker.Blueprints.Facts.BlueprintUnitFact">BlueprintUnitFact</see></param>
    [Implements(typeof(SwordlordAdaptiveTacticsAdd))]
    public static ActionsBuilder SwordlordAdaptiveTacticsAdd(this ActionsBuilder builder, string source)
    {
      var add = ElementTool.Create<SwordlordAdaptiveTacticsAdd>();
      add.m_Source = BlueprintTool.GetRef<BlueprintUnitFactReference>(source);
      return builder.Add(add);
    }

    /// <summary>
    /// Adds <see cref="Kingmaker.UnitLogic.Mechanics.Actions.SwordlordAdaptiveTacticsClear">SwordlordAdaptiveTacticsClear</see>
    /// </summary>
    [Implements(typeof(SwordlordAdaptiveTacticsClear))]
    public static ActionsBuilder SwordlordAdaptiveTacticsClear(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<SwordlordAdaptiveTacticsClear>());
    }

    //----- Kingmaker.Assets.UnitLogic.Mechanics.Actions -----//

    /// <summary>
    /// Adds <see cref="ContextActionResetAlignment"/>
    /// </summary>
    [Implements(typeof(ContextActionResetAlignment))]
    public static ActionsBuilder ResetAlignment(this ActionsBuilder builder, bool removeMythicLock = false)
    {
      var resetAlignment = ElementTool.Create<ContextActionResetAlignment>();
      resetAlignment.m_ResetAlignmentLock = removeMythicLock;
      return builder.Add(resetAlignment);
    }

    /// <summary>
    /// Adds <see cref="ContextActionSwarmAttack"/>
    /// </summary>
    [Implements(typeof(ContextActionSwarmAttack))]
    public static ActionsBuilder SwarmAttack(this ActionsBuilder builder, ActionsBuilder attackActions)
    {
      var attack = ElementTool.Create<ContextActionSwarmAttack>();
      attack.AttackActions = attackActions.Build();
      return builder.Add(attack);
    }

    /// <summary>
    /// Adds <see cref="ContextActionSwitchDualCompanion"/>
    /// </summary>
    [Implements(typeof(ContextActionSwitchDualCompanion))]
    public static ActionsBuilder SwitchDualCompanion(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionSwitchDualCompanion>());
    }

    //----- Kingmaker.Designers.EventConditionActionSystem.Actions -----//

    /// <summary>
    /// Adds <see cref="ContextActionAddRandomTrashItem"/>
    /// </summary>
    [Implements(typeof(ContextActionAddRandomTrashItem))]
    public static ActionsBuilder GiveRandomTrashToPlayer(
        this ActionsBuilder builder,
        TrashLootType type,
        int maxCost,
        bool identify = false,
        bool silent = false)
    {
      var addTrash = ElementTool.Create<ContextActionAddRandomTrashItem>();
      addTrash.m_LootType = type;
      addTrash.m_MaxCost = maxCost;
      addTrash.m_Identify = identify;
      addTrash.m_Silent = silent;
      return builder.Add(addTrash);
    }

    //----- Auto Generated -----//



    /// <summary>
    /// Adds <see cref="ContextActionAcceptBurn"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ContextActionAcceptBurn))]
    public static ActionsBuilder AddContextActionAcceptBurn(
        this ActionsBuilder builder,
        ContextValue Value)
    {
      builder.Validate(Value);
      
      var element = ElementTool.Create<ContextActionAcceptBurn>();
      element.Value = Value;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ContextActionHealBurn"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ContextActionHealBurn))]
    public static ActionsBuilder AddContextActionHealBurn(
        this ActionsBuilder builder,
        ContextValue Value)
    {
      builder.Validate(Value);
      
      var element = ElementTool.Create<ContextActionHealBurn>();
      element.Value = Value;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="BuffActionAddStatBonus"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(BuffActionAddStatBonus))]
    public static ActionsBuilder AddBuffActionAddStatBonus(
        this ActionsBuilder builder,
        StatType Stat,
        ContextValue Value,
        ModifierDescriptor Descriptor)
    {
      builder.Validate(Stat);
      builder.Validate(Value);
      builder.Validate(Descriptor);
      
      var element = ElementTool.Create<BuffActionAddStatBonus>();
      element.Stat = Stat;
      element.Value = Value;
      element.Descriptor = Descriptor;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="AbilityCustomSharedBurden"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(AbilityCustomSharedBurden))]
    public static ActionsBuilder AddAbilityCustomSharedBurden(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<AbilityCustomSharedBurden>());
    }

    /// <summary>
    /// Adds <see cref="AbilityCustomSharedGrace"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(AbilityCustomSharedGrace))]
    public static ActionsBuilder AddAbilityCustomSharedGrace(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<AbilityCustomSharedGrace>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionDetachFromSpawner"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ContextActionDetachFromSpawner))]
    public static ActionsBuilder AddContextActionDetachFromSpawner(this ActionsBuilder builder)
    {
      return builder.Add(ElementTool.Create<ContextActionDetachFromSpawner>());
    }

    /// <summary>
    /// Adds <see cref="ContextActionPrintHDRestrictionToCombatLog"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ContextActionPrintHDRestrictionToCombatLog))]
    public static ActionsBuilder AddContextActionPrintHDRestrictionToCombatLog(
        this ActionsBuilder builder,
        ContextValue HitDice)
    {
      builder.Validate(HitDice);
      
      var element = ElementTool.Create<ContextActionPrintHDRestrictionToCombatLog>();
      element.HitDice = HitDice;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ContextActionReduceDebilitatingBuffsDuration"/> (Auto Generated)
    /// </summary>
    [Generated]
    [Implements(typeof(ContextActionReduceDebilitatingBuffsDuration))]
    public static ActionsBuilder AddContextActionReduceDebilitatingBuffsDuration(
        this ActionsBuilder builder,
        StatsAdjustmentsType StatsAdjustmentsType)
    {
      builder.Validate(StatsAdjustmentsType);
      
      var element = ElementTool.Create<ContextActionReduceDebilitatingBuffsDuration>();
      element.StatsAdjustmentsType = StatsAdjustmentsType;
      return builder.Add(element);
    }

    /// <summary>
    /// Adds <see cref="ContextActionRestoreAllSpellSlots"/> (Auto Generated)
    /// </summary>
    ///
    /// <param name="m_ExcludeSpellbooks"><see cref="BlueprintSpellbook"/></param>
    [Generated]
    [Implements(typeof(ContextActionRestoreAllSpellSlots))]
    public static ActionsBuilder AddContextActionRestoreAllSpellSlots(
        this ActionsBuilder builder,
        UnitEvaluator m_Target,
        Int32 m_UpToSpellLevel,
        string[] m_ExcludeSpellbooks)
    {
      builder.Validate(m_Target);
      builder.Validate(m_UpToSpellLevel);
      
      var element = ElementTool.Create<ContextActionRestoreAllSpellSlots>();
      element.m_Target = m_Target;
      element.m_UpToSpellLevel = m_UpToSpellLevel;
      element.m_ExcludeSpellbooks = m_ExcludeSpellbooks.Select(bp => BlueprintTool.GetRef<BlueprintSpellbookReference>(bp)).ToList();
      return builder.Add(element);
    }  }
}
