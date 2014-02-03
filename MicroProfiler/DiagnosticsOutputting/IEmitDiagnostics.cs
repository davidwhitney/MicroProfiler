using System.Collections.Generic;
using System.Diagnostics;
using MicroProfiler.Profiling;

namespace MicroProfiler.DiagnosticsOutputting
{
    public interface IEmitDiagnostics
    {
        void OutputDiagnostics(List<MicroProfilerProfiledStep> steps, Stopwatch elapsedTimer);
    }
}