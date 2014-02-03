using System.Collections.Generic;
using System.Diagnostics;
using MicroProfiler.DiagnosticsOutputting;
using MicroProfiler.Profiling;

namespace MicroProfiler.Test.Unit.Fakes
{
    public class FakeDiagnosticsEmitter : IEmitDiagnostics
    {
        public bool Called { get; set; }
        public List<MicroProfilerProfiledStep> Steps { get; set; }
        public Stopwatch ElapsedTimer { get; set; }

        public void OutputDiagnostics(List<MicroProfilerProfiledStep> steps, Stopwatch elapsedTimer)
        {
            Steps = steps;
            ElapsedTimer = elapsedTimer;
            Called = true;
        }
    }
}