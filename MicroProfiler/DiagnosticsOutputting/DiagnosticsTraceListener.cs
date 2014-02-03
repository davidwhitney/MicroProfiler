using System.Collections.Generic;
using System.Diagnostics;
using MicroProfiler.Profiling;

namespace MicroProfiler.DiagnosticsOutputting
{
    public class DiagnosticsTraceListener : IEmitDiagnostics
    {
        public void OutputDiagnostics(List<MicroProfilerProfiledStep> steps, Stopwatch elapsedTimer)
        {
            WriteLine("Profiling session - Total time: " + elapsedTimer.ElapsedMilliseconds + "ms");

            foreach (var task in steps)
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