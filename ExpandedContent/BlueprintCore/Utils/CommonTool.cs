using System;

namespace BlueprintCore.Utils
{
  /// <summary>
  /// Utility for common operations.
  /// </summary>
  public static class CommonTool
  {
    /// <summary>
    /// Returns an array with the given values appended.
    /// </summary>
    /// 
    /// <remarks>
    /// Remember that this does not do an in-place modification of the array.<br/>
    /// <example>
    /// Typical usage:
    /// <code>
    ///   myArray = myArray.AppendToArray(otherArray);
    /// </code>
    /// </example>
    /// </remarks>
    public static T[] Append<T>(T[] array, params T[] values)
    {
      if (values == null) { return array; }
      var len = array.Length;
      var result = new T[len + values.Length];
      Array.Copy(array, result, len);
      Array.Copy(values, 0, result, len, values.Length);
      return result;
    }
  }
}
