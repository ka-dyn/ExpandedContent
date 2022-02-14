# Changelog

#v1.3.2 Release

### New Features

* Added `EncyclopediaTool` for adding tooltips to localized text
    * If you create strings using `LocalizationTool` this is automatically called so no changes needed
    * Credit to Hypebringer | Aegonek for porting from Vek's TabletopTweaks
* Updated for compatibility w/ 1.1.7

#v1.3.1 Release

### New Features

* Updated for compatibility w/ 1.1.6

### Fixes 

* Fixed `ConditionsBuilder.Add<C>()`, `ActionsBuilder.Add<A>()`, and `BlueprintConfigurator.AddComponent<C>()` so they actually do not require an init action 

# v1.3.0 Release

* BlueprintCore is a DLL again! This should be the last time this changes.
    * The DLL is unsigned and **must be merged into your assembly**. Follow the instructions in [Getting Started](intro.md) to configure it.
* The tutorials, including on the modding wiki, are now setup to use SDK style project syntax
* New components available that were added in recent game patches

## v1.2.2 Release

* Flatten namespace structure for configurators and remove "Configurator" from file names
    * Long file paths were causing issues restoring the NuGet package

## v1.2.1 Release

* Fixes warnings in legacy projects
    * SDK projects support `<Nullable>enabled</Nullable>` project config
    * Legacy projects require per-file enable: `#nullable enable`

## v1.2.0 Release

The experiment with release as a DLL was a fun but short-lived experiment. The game and Unity DLLs are unsigned which means that referencing them from a signed DLL is unsupported. Although initial testing was fine, it's likely that distributing as a signed DLL would surface hard to debug errors.

* Moving forward BlueprintCore will remain a source package
    * I will include the DLL in a zip on [GitHub](https://github.com/WittleWolfie/WW-Blueprint-Core/releases)
        * If you use the DLL you should use a tool such as [ILRepack](https://github.com/ravibpatel/ILRepack.Lib.MSBuild.Task) instead of packaging the DLL with your mod

### Other Changes

* Removed `*Configure.New(string name)` for clarity; just use the two parameter variant
* Added `BlueprintTool.GetGuidsByName()` method which returns a copy of the internal mapping
* Fixed warnings

## v1.1.0 Release

* BlueprintCore is now a signed DLL instead of a source package
    * The BlueprintCore content folder contains the license, readme, and changelog
    * You must include BlueprintCore.dll with your mod
    * There is no conflict if different mods refer to different versions of the DLL
    * If you prefer including source you can download a source zip on [GitHub](https://github.com/WittleWolfie/WW-Blueprint-Core/releases)
* Fixed logging prefix to include colon and space for readability

## v1.0.1 Release

* Bundled changelog, license, and readme into the package content files
* Fixed validation for enumerable parameters in configurators
* Updated documentation for SDK project configurations

## v1.0.0 Release

This release marks completion of the core functionality. There are a lot of breaking changes.

* Core functionality is **complete**:
    * All blueprint types (BlueprintScriptableObject) has its own configurator
    * All Blueprint Components have constructor methods in the supported configurators
    * All Actions have builder methods
    * All Conditions have builder methods
* Other new APIs:
    * Generic Add w/ init for BlueprintComponent, Action, and Condition
    * Blueprint configurators have an EditComponent method
    * Enumerable blueprint field methods include Set in addition to AddTo and RemoveFrom.
* Generated code field types use primitive names when appropriate
* Fixed ConditionsBuilderStoryEx namespace (previously was MiscEx, now is correctly StoryEx)
* Added validation check for duplicate AbilityRankType definitions in ContextRankConfig
* Generated code makes a best effort attempt to define optional parameters and provide safe default values for types which should not be null
* Added LocalizationTool for creating LocalizedStrings

### Breaking Changes

A lot of codeode was moved around for consistency and new DLL references are required.

* BlueprintTool moved from BlueprintCore.Blueprints to BlueprintCore.Utils
* All BlueprintConfigurators were moved from BlueprintCore.Blueprints to BlueprintCore.Configurators
* Blueprint components are now only exposed in supported configurators
    * i.e. If you don't see a component method in a configurator it means that component is not expected to work with that blueprint type. There's no guarantee since this is based on Owlcat's validation with additional checks added manually to correct issues as they are found.
* New assembly references are required:
    * Owlcat.Runtime.Visual.dll
    * Owlcat.Runtime.UI.dll
* Auto-generated methods provide default values for primitives, nullable types, and specific game types
* ContextRankConfigs API reworked
    * CommonExtensions are now optional parameters on the core functions
    * Adopted the names of the progression types
    * Added param comments for blueprint types

## v0.6.0 Release

* Completed ConditionsBuilder API using auto-generated methods
* Moved SwitchDualCompanion from StoryEx to BasicEx to mirror ContextActionSwitchDualCompanion

## v0.5.0 Release

* Completed ActionsBuilder API using auto-generated methods

## v0.4.1 Release

* New CustomProgression method for ContextRankConfig accepts anonymous tuples for progression entries
    * Current CustomProgression method marked obsolete before removal.
* New LinearProgression method for ContextRankConfig