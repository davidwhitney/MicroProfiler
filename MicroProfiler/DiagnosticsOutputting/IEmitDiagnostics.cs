using System;
using System.Collections.Generic;
using System.Diagnostics;
using MicroProfiler.Profiling;

namespace MicroProfiler.DiagnosticsOutputting
{
    public interface IEmitDiagnostics
    {
        void OutputDiagnostics(Guid sessionId, List<MicroProfilerProfiledStep> steps, Stopwatch elapsedTimer);
    }
}