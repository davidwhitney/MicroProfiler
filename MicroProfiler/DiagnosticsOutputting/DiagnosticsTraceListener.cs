using System;
using System.Collections.Generic;
using System.Diagnostics;
using MicroProfiler.Profiling;

namespace MicroProfiler.DiagnosticsOutputting
{
    public class DiagnosticsTraceListener : IEmitDiagnostics
    {
        public void OutputDiagnostics(Guid sessionId, List<MicroProfilerProfiledStep> steps, Stopwatch elapsedTimer)
        {
            WriteLine("Profiling session " + sessionId + " summary");

            foreach (var task in steps)
            {
                WriteLine(task.Label + " started at: +" + task.MsFromRequestStart + "ms  -  duration: " +
                          task.Elapsed.Milliseconds + "ms");
            }

            WriteLine("Total time in Modules " + elapsedTimer.ElapsedMilliseconds + "ms");
        }

        private static void WriteLine(string message)
        {
            Trace.WriteLine("[MicroProfiler] " + message);
        }
    }
}