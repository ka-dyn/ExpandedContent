using System;

namespace BlueprintCore.Utils
{
  /// <summary>
  /// Identifies which game type is implemented by the method or class.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
  public class ImplementsAttribute : Attribute
  {
#pragma warning disable IDE0060 // Remove unused parameter
    public ImplementsAttribute(Type type) { }
#pragma warning restore IDE0060 // Remove unused parameter
  }

  /// <summary>
  /// Identifies automatically generated classes and methods.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
  public class GeneratedAttribute : Attribute { }
}
