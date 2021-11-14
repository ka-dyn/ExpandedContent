using Kingmaker.Blueprints.Classes.Prerequisites;

namespace BlueprintCore.Utils
{
  /// <summary>
  /// Tool for operations on <see cref="Prerequisite"/> objects.
  /// </summary>
  public static class PrereqTool
  {
    /// <summary>
    /// Instantiates a <see cref="Prerequisite"/> object and sets the provided fields.
    /// </summary>
    public static T Create<T>(
        Prerequisite.GroupType group, bool checkInProgression, bool hideInUI)
        where T : Prerequisite, new()
    {
      return new T
      {
        Group = group,
        CheckInProgression = checkInProgression,
        HideInUI = hideInUI
      };
    }
  }
}