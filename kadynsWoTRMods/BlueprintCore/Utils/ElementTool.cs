using Kingmaker.ElementsSystem;

namespace BlueprintCore.Utils
{
  /// <summary>
  /// Tool for operations on <see cref="Element"/> objects.
  /// </summary>
  public static class ElementTool
  {
    /// <summary>
    /// Intantiates and initializes <see cref="Element"/> objects.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    /// Internally this calls <see cref="Element.CreateInstance(System.Type)"/> which has some basic object init logic.
    /// If this init logic is not executed exceptions may be thrown when the object is referenced.
    /// </para>
    /// 
    /// <para>
    /// A few types that inherit from <see cref="Element"/> do not have a parameterless constructor. For those types use
    /// <see cref="Init"/>.
    /// </para>
    /// </remarks>
    public static T Create<T>() where T : Element
    {
      return (T)Element.CreateInstance(typeof(T));
    }

    /// <summary>
    /// Initializes <see cref="Element"/> objects.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    /// Mimics logic in <see cref="Element.CreateInstance(System.Type)"/> for types without a parameterless constructor.
    /// Use <see cref="Create"/> whenever possible.
    /// </para>
    /// </remarks>
    public static void Init(Element element)
    {
      element.name = $"${element.GetType()}${System.Guid.NewGuid()}";
    }
  }
}