using System.Diagnostics;
using MicroProfiler.Profiling;

namespace MicroProfiler.DiagnosticsOutputting
{
    public class DiagnosticsTraceListener : IEmitDiagnostics
    {
        public void OutputDiagnostics(ProfiledOperations operations, Stopwatch elapsedTimer)
        {
            WriteLine("Profiling session "+ operations.Id + " - Total time: " + elapsedTimer.ElapsedMilliseconds + "ms");
            WriteLine("Total time: " + elapsedTimer.ElapsedMilliseconds + "ms");

            foreach (var task in operations.Tasks)
            {
                var paddedTime = ("+" + task.MsFromRequestStart + "ms").PadRight(15);
                WriteLine(paddedTime + "Duration: " + task.Elapsed.TotalMilliseconds + "ms" .PadRight(12) + task.Label);
            }
        }

        private static void WriteLine(string message)
        {
            Trace.WriteLine("[MicroProfiler] " + message);
        }
    }
}