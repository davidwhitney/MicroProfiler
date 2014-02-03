using System;
using System.Collections.Generic;
using MicroProfiler.DiagnosticsOutputting;
using MicroProfiler.Profiling;
using MicroProfiler.ProfilingDataStorage;

namespace MicroProfiler
{
    public interface IMicroProfiler : IDisposable
    {
        IMicroProfilerStorage Storage { get; }
        ICollection<IEmitDiagnostics> DiagnosticOutput { get; }

        IProfileASingleStep Step(string label);
        IMicroProfiler Start();
        void Stop();
    }
}
