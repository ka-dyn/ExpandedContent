# WW-Blueprint-Core

[![NuGet](https://img.shields.io/nuget/v/WW-Blueprint-Core?style=flat-square)](https://www.nuget.org/packages/WW-Blueprint-Core)

*What is BlueprintCore*: A library to simplify modifying Pathfinder: Wrath of the Righteous. At a glance it provides:

* A fluent API for creating and modifying Blueprints, Actions, and Conditions
```C#
BuffConfigurator.New(MyBuffName, MyBuffGuid).AddStatBonus(stat: StatType.Strength, value: 2).Configure();
```
* Exposed methods for finding all Blueprint, Action, Condition, and Blueprint Component types in the game
* Restricted API for modifying Blueprints which exposes only Blueprint Components usable with the given Blueprint type
* Runtime validation and warnings when potential problems are detected
    * Uses custom logic along with validation checks provided in the game library

If you're interested in contributing, see [How to Contribute](https://wittlewolfie.github.io/WW-Blueprint-Core/articles/contributing.html).

For usage see [Getting Started](https://wittlewolfie.github.io/WW-Blueprint-Core/articles/intro.html).

## Features

### Blueprint Configurators

Each Blueprint class has a corresponding configurator, e.g. `BuffConfigurator`, which exposes a fluent API for modifying its fields and components. Once you call `Configure()` all of the changes are committed and validation errors are logged as a warning.

This API exposes all supported Blueprint Component types available in the game library. When used with auto-complete it provides a quick and easy way to search for Blueprint Component types.

### ActionList and ConditionsChecker Builders

`ActionsBuilder` is a builder API for `ActionList` and `ConditionsBuilder` is a builder API for `ConditionsChecker`.

The builders provide methods for creating all Action and Condition types, spread across extension classes which limit the scope of auto-complete. The extensions are logically grouped so most uses require only one set.

When `Build()` is called the corresponding game type is returned and validation errors are logged as a warning. Library methods accept builders directly and call `Build()` internally to minimize boilerplate.

### Utils

Utility classes provide additional functionality to simplify modifying the game as well as helping ensure correct use of game types.

## Usage

BlueprintCore is available as [NuGet package](https://www.nuget.org/packages/WW-Blueprint-Core/) that provides the source code for compilation into your modification. It requires a [public assembly](https://github.com/WittleWolfie/OwlcatModdingWiki/wiki/Publicise-Assemblies). An optional DLL release is available on [GitHub](https://github.com/WittleWolfie/WW-Blueprint-Core/releases) for use with [ILRepack](https://github.com/ravibpatel/ILRepack.Lib.MSBuild.Task).

For more details see the [documentation](https://wittlewolfie.github.io/WW-Blueprint-Core/articles/intro.html).

### Example

**Partial Implementation of Skald's Vigor**
```C# 
BuffConfigurator.For(SkaldsVigorBuff)
    .ContextRankConfig(ContextRankConfigs.ClassLevel(new string[] { SkaldClass }).WithStartPlusDoubleDivStepProgression(8))
    .FastHealing(1, bonusValue: ContextValues.Rank(AbilityRankType.Default))
    .Configure();

var applyBuff = ActionsBuilder.New().ApplyBuff(SkaldsVigorBuff, permanent: true, dispellable: false);
BuffConfigurator.For(RagingSongBuff)
    .AddFactContextActions(
        onActivated:
            ActionsBuilder.New()
                .Conditional(
                    ConditionsBuilder.New().TargetIsYourself().HasFact(SkaldsVigor),
                    ifTrue: applyBuff)
                .Conditional(
                    ConditionsBuilder.New().CasterHasFact(GreaterSkaldsVigor),
                    ifTrue: applyBuff),
        onDeactivated: ActionsBuilder.New().RemoveBuff(SkaldsVigorBuff))
    .Configure();
```

# Acknowledgements

* Thank you to the Owlcat modders who came before me, documenting their process and sharing their code.
* Thank you to the modders on Discord who helped me learn modding so I could create this library.

# Interested in modding?

* Check out the [OwlcatModdingWiki](https://github.com/WittleWolfie/OwlcatModdingWiki/wiki).
* Join us on [Discord](https://discord.gg/zHbMuYT6).
