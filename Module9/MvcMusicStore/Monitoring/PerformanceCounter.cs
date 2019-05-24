using System.Diagnostics;
using PerformanceCounterHelper;

namespace MvcMusicStore.Monitoring
{
    [PerformanceCounterCategory("MvcPerformanceCounter", PerformanceCounterCategoryType.SingleInstance, "Set of counters for MvcMusicStore application")]
    public enum PerformanceCounter
    {
        [PerformanceCounter("LogInCount", "Log In success count", PerformanceCounterType.NumberOfItems32)]
        LogInCounter = 1,

        [PerformanceCounter("LogOffCount", "Log Off success count", PerformanceCounterType.NumberOfItems32)]
        LogOffCounter = 2,
    }
}