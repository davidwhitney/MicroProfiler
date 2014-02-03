using System.Diagnostics;
using MicroProfiler.Profiling;

namespace MicroProfiler.DiagnosticsOutputting
{
    public class DiagnosticsTraceListener : IEmitDiagnostics
    {
        public void OutputDiagnostics(ProfiledOperations operations, Stopwatch elapsedTimer)
        {
            WriteLine("Session " + operations.Id);
            WriteLine("Clock".PadRight(15) + "Time (inc. children)".PadRight(25) + "Label");

            foreach (var task in operations.Tasks)
            {
                WriteLine(("+" + task.MsFromRequestStart + "ms").PadRight(15) + (task.Elapsed.TotalMilliseconds + "ms").PadRight(25) + task.Label);
            }

            WriteLine("Total time: " + elapsedTimer.ElapsedMilliseconds + "ms");
        }

        private static void WriteLine(string message)
        {
            Trace.WriteLine("[Profiler] " + message);
        }
    }
}