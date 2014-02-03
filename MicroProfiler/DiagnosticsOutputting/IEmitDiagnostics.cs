using System.Diagnostics;
using MicroProfiler.Profiling;

namespace MicroProfiler.DiagnosticsOutputting
{
    public interface IEmitDiagnostics
    {
        void OutputDiagnostics(ProfiledOperations operations, Stopwatch elapsedTimer);
    }
}