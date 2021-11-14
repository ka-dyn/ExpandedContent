using System;
using System.Reflection;
using Owlcat.Runtime.Core.Logging;

namespace BlueprintCore.Utils
{
  /// <summary>
  /// Wrapper around <see cref="LogChannel"/> which supports dynamically enabling or disabling verbose logging.
  /// </summary>
  /// 
  /// <remarks>
  /// These log events print to the same output as Wrath's game log events. They can be viewed using
  /// <see href="https://github.com/OwlcatOpenSource/RemoteConsole/releases">RemoteConsole</see> or by appending
  /// <c>logging</c> to the executable startup arguments. If you are not using the console, logs print to
  /// <c>AppData\LocalLow\Owlcat Games\Pathfinder Wrath Of The Righteous\GameLog*.txt</c>.
  /// </remarks>
  public class LogWrapper
  {
    /// <summary>
    /// Controls whether calls to <see cref="Verbose(string)"/> are logged. Defaults to false.
    /// </summary>
    /// 
    /// <remarks>
    /// Set this to true while developing or debugging. Consider making this a setting in your modification so users can
    /// capture detailed logs when reporting a problem.
    /// </remarks>
    public static bool EnableVerboseLogs = false;

    internal static string PrefixBase = $"BlueprintCore::{typeof(LogWrapper).Assembly.GetName().Name}";

    internal static LogWrapper GetInternal(string prefix)
    {
      return Get($"{PrefixBase}::{prefix}");
    }

    /// <summary>
    /// Returns a <see cref="LogWrapper"/> which prepends the given prefix to all log events.
    /// </summary>
    public static LogWrapper Get(string prefix)
    {
      LogChannel channel = LogChannelFactory.GetOrCreate(prefix);
      channel.SetSeverity(LogSeverity.Message);
      return new LogWrapper(channel);
    }

    private readonly LogChannel Logger;

    protected LogWrapper(LogChannel logger)
    {
      Logger = logger;
    }

    /// <summary>
    /// Logs an error with a stack trace as well as an exception, if provided.
    /// </summary>
    public virtual void Error(string msg, Exception e = null)
    {
      Logger.Error(msg);
      if (e != null) { Logger.Exception(e); }
    }
    
    public virtual void Info(string msg)
    {
      Logger.Log(msg);
    }

    /// <summary>
    /// Logs a warning with a stack trace.
    /// </summary>
    public virtual void Warn(string msg)
    {
      Logger.Warning(msg);
    }

    /// <summary>
    /// If <see cref="EnableVerboseLogs"/> is false these log are ignored.
    /// </summary>
    public virtual void Verbose(string msg)
    {
      if (EnableVerboseLogs) { Logger.Log(msg); }
    }
  }
}