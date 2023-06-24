# Changelog

## v2.8.0 Release

* Updated for 2.1 patch

### Breaking Changes

* Many components changed
* BuffCollectionConfigurator is no longer present
    * BuffCollection (the blueprint) is no longer used in Wrath
* `AbilityEffectRunActionOnClickedTarget` is no longer available
    * Use `AbilityEffectRunAction`, `AbilityEffectRunActionOnClickedPoint`, or `AdditionalAbilityEffectRunActionOnClickedTarget`

## v2.7.8 Release

* Added `AddToLevelEntry` to ProgressionConfigurator to simplify adding new features to an existing progression

## v2.7.7 Release

* FeatureSelectionConfigurator is at feature parity w/ FeatureConfigurator for populating feature groups

## v2.7.6 Release

* New custom FeatureSelectionConfigurator for easily setting feature groups
* FeatureConfigurator is more aggressive about setting `IsClassFeature` to `true`

### Breaking Changes

* Custom FeatureSelectionConfigurator requires updating imports

## v2.7.5 Release

* AbilityConfigurator has an override for `SetLocalizedSavingThrow` for common saving throw strings

## v2.7.4 Release

* Prevents mod conflict which can prevent modded spells from being learnable during level up

### Breaking changes

* SpellListConfigurator namespace has changed (it is now a custom configurator), so imports require updating

## v2.7.3 Release

* Removes `m_DescriptionModifiersCache` from feature configuration
* Fixes a bug which breaks some feature descriptions when you modify them with configurators

### Breaking changes

* You can no longer modify `m_DescriptionModifiersCache` with configurators. This field is populated automatically at runtime, if you want to change it add components inheriting from `DescriptionModifier`

## v2.7.2 Release

* Update with latest patch data

## v2.7.1 Release

* Delayed configuration now sets sane defaults immediately
    * This fixes a conflict w/ WorldCrawl

## v2.7.0 Release

* Update with latest patch data
* New `AssetExtensions` class for Unity extension methods (stolen shamelessly from Bubbles)
    * Includes new class `SnapToTransformWithRotation` and accompanying extensions, `AnchorTo()` and `AnchorToUnit()` for anchoring Unity objects to follow other objects
* Added `SetLocalizedDuration` utility to `AbilityConfigurator` for easy reference of standard duration strings
* Adding a spell to a mod spell list will no longer cause an NPE if the mod is not present
    * An error is still logged in BPCore using the Verbose channel
* Fixed a bug causing validation to always fail for some components

## v2.6.2 Release

* Fixes NPE with DealDamagePreRolled action

## v2.6.1 Release

* Updated to latest game patch (2.0.4k)
* New `AddAll` methods for `ActionsBuilder` and `ConditionsBuilder` support copying existing `ActionLists` and `ConditionsCheckers`
* Implicit operators support passing in `ActionList` for `ActionsBuilder` and `ConditionsChecker` for `ConditionsBuilder`

## v2.6.0 Release

* New `AbilityConfigurator` util methods for creating spells
    * Use `AbilityConfigurator.NewSpell()` instead of `AbilityConfigurator.New()` and read its docs
* Added support for dynamically replacing game assets
    * See `AssetTool.RegisterDynamicPrefabLink()` for details
    * Use this when you want a modified version of an existing asset
* New `ContextDice` and `DamageTypes` utils for creating `ContextDiceValue` and `DamageTypeDescription`, respectively
* Added links to some mods made using BPCore for examples

### Breaking Changes

* If you are using ILStrip there is a new patch, add the following entry point:
    * `BlueprintCore.Utils.Assets.AssetTool/AssetBundle_Patch`

## v2.5.1 Release

* Update for 2.0.2c game version

## v2.5.0 Release

* New Type Utils
    * `CueSelections` - For creating `CueSelection`
    * `CharacterSelections` - For creating `CharacterSelection`
    * `DialogSpeakers` - For creating `DialogSpeaker`
* New Empty Constants for working with Answers, Cues, and Dialogs
* Add option to skip selection processing in `FeatureConfigurator`: `SkipAddToSelections`
* Teamwork feats are automatically added to Lich Skeletal Companion's feat selection

## v2.4.1 Release

* Fix bug causing `CopyFrom()` to ignore all components when using a predicate
* A single delayed configurator failure no longer prevents other delayed configurators from being processed
* Prevent NPE when calling `SetIcon(null)`
    * You shouldn't do this but sometimes it's convenient for methods to call `SetIcon()` on arbitrary input

##  v2.4.0 Release

* Added blueprint copying methods: `CopyFrom()`
    * These are shallow copies that copy all fields
    * Components are only copied if specified, either by type or using a predicate

## v2.3.3 Release

* Fixes a bug preventin archetypes from working properly in ContextRankConfigs

## v2.3.2 Release

* Prevents features from being added to Feature Selection lists they're already on

## v2.3.1 Release

* Updated for EE

## v2.3.0 Release

* Added more support for `FeatureGroup.TeamworkFeat` so it can be configured for sharing features. Note that you need to use the method `AddAsTeamworkFeat()` since configuring for some class features requires GUIDs used to generate additional blueprints.
    * Battle Scion's Battle Prowess
    * Monster Tactician Tactics
    * Tactical Leader Feat Sharing
    * Hunter Tactics
    * Sacred Huntsmaster Tactics
    * Cavalier Tactician
    * Vanguard Tactician
    * Pack Rager's Raging Tactician
* Updated for the latest game version
* Removed obsolete components flagged in the 2.2 release

### Breaking Changes

* All components flagged obsolete have been removed

## v2.2.5 Release

* Added support for multiple strings files and embedded resources
* Features with `FeatureGroup.TeamworkFeat` are automatically added to Tactical Leader's Share Feat Buff

## v2.2.4 Release

* Updated for latest game version
* Added blueprint references to populate content mods, including auto config for BlueprintFeatureSelection
    * Microscopic Content Expansion
    * TTT-Base, TTT-Reworks
    * WOTR_PATH_OF_RAGE
    * Expanded Content
    * Kineticist Expanded Elements
    * Magic Time!
* Added support for delayed configuration
    * Recommended for auto config of BlueprintFeatureSelection to support mods
* Hand tuned more components
* Added `BlueprintTool.TryGet<T>()` to check for presence of a blueprint

### Breaking Changes

* `AddReplaceStatForPrerequisites` has been modified

## v2.2.3 Release

* Updated for latest game version
* Fixed bug with FeatureConfigurator which could incorrectly add features to selection groups
* Added `SetClass()` and upated constructor for ArchetypeConfigurator to automatically add the archetype to the class

## v2.2.2 Release

* Updated for Patch 1.4
* Hand tuned Recommendation components

## v2.2.0 Release

* New Quick Start setup using a project template, no more editing .csproj files!
* Expanded tutorials to cover more advanced modding
    * Activatable Abilities, Transpilers, Assets, and more
* Added several replacement components from TTT-Core
    * Existing methods are flagged as obsolete and will be removed in the next major release, so consider migrating
* Added `Asset<>` and `AssetLink<>` types to allow referencing assets by Asset Id
* Added `ToString()` override to `Blueprint<>`
    * This makes it easier to cast between types
* Added `AddToFeatureSelection()` and `AddToRangerStyles()` methods to `FeatureConfigurator`
    * Convenience methods for adding to `BlueprintFeatureSelection` if the `FeatureGroup` logic doesn't handle it
* ContextValues
    * Added support for `ContextValueType.AbilityParameter`
    * Updated `CustomProperty` to accept `Blueprint<>` instead of `string`
* Added new `HasActionsAvailable` Condition
* Fixed and filled out unit test project
    * Future contributions should include unit tests to the extent possible
* Fixed type specific overrides to apply to Lists and Arrays
* Fixed LogWrapper to respect `EnableInternalVerboseLogs`

### Breaking Changes

* If you are using ILStrip there are new patches, add the following entry points:
    * `BlueprintCore.UnitParts.Replacements.UnitPartBuffSuppressFixed/Buff_OnAttach_Suppression_Patch`
    * `BlueprintCore.Utils.Assets.AssetTool/BlueprintsCaches_Patch`
    * `BlueprintCore.Utils.Assets.AssetTool/BundlesLoadService_Patch`
* Lists and arrays of the following types have been replaced by their BPCore version, which may require updates:
    * `LocalizedString` => `LocalString`
    * `AnimationClipWrapperLink` => `AssetLink<AnimationClipWrapperLink>`
    * `AnimationClipWrapper` => `Asset<AnimationClipWrapper>`
    * `EquipmentEntityLink` => `AssetLink<EquipmentEntityLink>`
    * `EquipmentEntity` => `Asset<EquipmentEntity>`
    * `FamiliarLink` => `AssetLink<FamiliarLink>`
    * `Familiar` => `Asset<Familiar>`
    * `PrefabLink` => `AssetLink<PrefabLink>`
    * `GameObject` => `Asset<GameObject>`
    * `ProjectileLink` => `AssetLink<ProjectileLink>`
    * `Projectile` => `Asset<Projectile>`
    * `SpriteLink` => `AssetLink<SpriteLink>`
    * `Sprite` => `Asset<Sprite>`
    * `TextAssetLink` => `AssetLink<TextAssetLink>`
    * `TextAsset` => `Asset<TextAsset>`
    * `Texture2DLink` => `AssetLink<Texture2DLink>`
    * `Texture2D` => `Asset<Texture2D>`
    * `UnitViewLink` => `AssetLink<UnitViewLink>`
    * `UnitEntityView` => `Asset<UnitEntityView>`
    * `VideoLink` => `AssetLink<VideoLink>`
    * `VideoClip` => `Asset<VideoClip>`

## v2.1.2 Release

* Bugfixes:
    * Restored pre 2.1 API for Randomize and SelectByValue

### Breaking Changes

* If you updated code references you'll have to revert. This was an accidental / change introduced when params became a standard API.

## v2.1.1 Release

* Bugfixes:
    * ArchetypeConfigurator and ProgressionConfigurator `RemoveFromX` functions now function properly
    * All `RemoveFromX` functions accepting a predicate now function as expected
        * Previously the configurators were confused and were keeping everything matching the predicate, not removing
    * UIGroupBuilder and LevelEntryBuilder now work properly
        * Rumor has it they were using static fields so they collided with one another

### Breaking Changes

* If you were using any `RemoveFromX(Func<Blueprint,bool>)` functions the logic is now reversed. This matches the API design but not the function.

## v2.1.0 Release

* Added new localization system for translation support and simplified text references. See [Text, Logging, and Utils](usage/utils.md) for more details.
* Added static references to in-game blueprints. See [Referencing Blueprints](usage/blueprints.md#referencing-blueprints) for more details.
* Configurators now automatically set safe default values for types which cannot be null
* Methods with a single enumerable or flag parameter use `params` syntax
* Removed configurators for QA related blueprints
* Add more constructors for ContextDurationValue
* Removed Modify field methods for primitive and enum types
    * If you have a use case let me know but this API was not intended to exist
* Specific Type Changes
    * AbilityConfigurator
        * Removed AnimationStyle which is deprecated and unused
    * FeatureConfigurator
        * Automatically adds features to the appropriate `BlueprintFeatureSelection`. See [FeatureConfigurator.New()](xref:BlueprintCore.Blueprints.CustomConfigurators.Classes.FeatureConfigurator.New(System.String,System.String,Kingmaker.Blueprints.Classes.FeatureGroup[])).
        * Automatically sets `IsClassFeature` to true for features with `FeatureGroup.Feat`
    * ProgressionConfigurator
        * Added many convenience method overrides for ease of working with ClassWithLevel, LevelEntry, and ArchetypeWithLevel
        * Removed support for AlternateProgressionType which can only be Div2
        * Automatically sets `ForAllOtherClasses` to `false` when setting or adding to `m_AlternateProgressionClasses`
        * Removed support for Remove functions that don't make sense
    * BuffEnchantAnyWeapon is now available in BaseUnitFactConfigurator and all inherited types
    * AddStatBonusIfHasFact replaced by AddStatbonusIfHasFactFixed
    * ContextDurationValue has more convenience constructor methods available
    * BlueprintUnitProperty logs a warning if `BaseValue` is 0 when using multiplication

### Breaking Changes

* ProgressionConfigurator changed namespace
    * It is now hand tuned so it lives in CustomConfigurators
    * Some `RemoveFrom` field methods were removed since they are not useful
* Some ContextAction methods were updated with a stricter API
    * `ArmorEnchantPool` and `ShieldArmorEnchantPool` uses `BlueprintArmorEnchantReference` instead of `BlueprintItemEnchantmentReference`
    * `WeaponEnchantPool` and `ShieldWeaponEnchantPool` uses `BlueprintWeaponEnchantmentReference` instead of `BlueprintItemEnchantmentReference`
    * If you were passing in a BlueprintItemEnchantmentReference directly you'll need to update your calls
* `Buffs`, `ItemEnchantments`, and `Features` were removed
    * Replaced by Refs classes. See [Referencing Blueprints](usage/blueprints.md#referencing-blueprints).
* QA Blueprint configurators were removed
    * I don't expect anyone to use these, but if you have a use case let me know.
* Configurators no longer exposes methods for cache fields
    * These are set at runtime by the blueprint, they should not be set manually.
* Methods with a single enumerable parameter use `params` syntax
    * If they currently require a list they must be updated. You can call `ToArray()` on the list.
* AddStatBonusIfHasFact is no longer supported
    * Use AddStatBonusIfHasFactFixed
* Methods for BlueprintProgression.AlternateProgressionType are removed
    * It is always Div2, there is no other value
* Methods for BlueprintAbility.animationStyle are removed
    * It is unused and deprecated

## v2.0.4 Release

* Fixed an NPE resulting from not specifying optional List or Array parameters

### Breaking Changes

* Fixed a bug incorrectly identifying unique and non-unique components
    * This removes merge handling parameters from non-unique components; you'll need to remove any you have specified

## v2.0.3 Release

### Breaking Changes

* The type `Blueprint<T,  TRef>` has been shortened to `Blueprint<TRef>`
    * You can use a Regex find/replace to fix. In VS:
        * Find: `Blueprint<\w*, `
        * Replace With: `Blueprint<`

## v2.0.2 Release

* Update to 1.3.4e game patch
* Handles GUIDs with uppercase letters
* Type specific handling updates
* New util for creating `UnitConditionExceptions`
* Blueprint field setters use `params` for enumerable types
* Fixed bug with Encyclopedia tagging causing exceptions
* Fixed default merge behavior which was incorrectly set to ComponentMerge.Merge instead of ComponentMerge.Fail

### Breaking Changes

* Namespaces are changes to organize type specific util classes
    * `ContextRankConfigs`
    * `ContextDuration`
    * `ContextValues`
* Blueprint field setters use `params` for enumerable types
    * This breaks for `List<>` fields
* Removed support for `AddStatBonusScaled`
    * This is a legacy type and can be replaced with `AddContextStatBonus`

## v2.0.1 Release

* Reverts `Configurator.Build()` to return the blueprint directly

## v2.0.0 Release

* This is a significant release which is all but guaranteed to require changes to your code when updating.*

### Features

* Flexible Blueprint references
    * BlueprintCore APIs accept blueprints by Guid, Blueprint instance, Blueprint reference, or Name for newly created
      blueprints
* Improved Validation
    * Validator is now up to date with the latest changes in the game validation code
* Removed unused game types from Builder and Configurator APIs
* Improved documentation of games types
    * Examples: Every ActionsBuilder, ConditionsBuilder, and Configurator component method lists up to 3 game blueprints
      which use the implemented type
    * Developer Comments: Taken from attributes in the game code which Owlcat uses to create tooltips and help text in
      their level editor
    * Comments are integrated with the doc comments shown in your IDE
        * In VS you can navigate to the definition of BlueprintCore methods to see the full formatted comments for readability

### Breaking Changes

* Configurator namespaces may have changed to align w/ game code structure
* Method and parameter names changed for ActionsBuilder, ConditionsBuilder, and Configurators
* Validator is no longer static
* Builder and Configurator methods no longer exist for unused game types

### More Details

Two concepts drove the creation of BlueprintCore 2.0:

1. Make it easier to improve and contribute to BlueprintCore, especially with regards to handling of game types
2. Adopt a philosophy of keeping the library as close to the game code as possible

With regards to #1 I will update the contributor docs after release with guidance and examples of how to contribute. In
short, you can provide remarks and instructions on handling game types by editing JSON configuration files. Alternatively,
just share your knowledge in issues on GitHub and I'll do my best to update the library.

For #2 there was a major shift in how the library generates ActionsBuilder, ConditionsBuilder, and Configurator classes.
Previously methods were automatically generated _or_ written by hand entirely. When creating methods by hand I was aggressive
about renaming things to clarify the actual game function.

However, this makes it very hard to do structural improvements since all hand written code requires manual updates.
Additionally a lot of the hand written code was nearly identical to generated code and only existed to rename things.
With this release almost nothing is written by hand. Instead JSON config files allow selective editing of the code
generation behavior.

The end result is both functional and philosophical: method and parameter names map almost 1:1 with the associated type
or field. This makes it easier to take existing knowledge of game code and use it to write BlueprintCore code, as well
as taking BlueprintCore code and find the game code it affects.

### What's Next?

With the major refactoring done I'll resume work on functional library improvements. Please file requests and suggestions
on GitHub, I do read them and try my best to support them. For 2.1 I'm looking into:

* Expanded Tutorial
    * Examples of doing more extensive changes
    * Improved setup instructions and examples of using different BlueprintCore classes and utilities
* Localization improvements
    * Improve API with regards to whether a given string is parsed for tokens
    * Add localization support
    * Reduce boilerplate needed to create and reference LocalizedStrings
* Smarter Configurators for common blueprint types, e.g. CharacterClass, Feature, Ability
    * Create blueprints using existing blueprints as a template, e.g. WizardClass template applies things like 1/2 BAB
      and Saving Throw progressions
    * Combination and creation methods that enforce contracts
    * Automatically add blueprints to appropriate selection groups
* Static Blueprint References for common types
    * Expose an auto-complete list for things like Classes, Archetypes, Feats, and Spells
* Project Setup Tool (Tentative)
    * An automated tool to create a new mod project with BlueprintCore setup
* TTT-Core Support (Tentative)
    * I'd like to provide support for integration w/ TTT-Core without requiring it