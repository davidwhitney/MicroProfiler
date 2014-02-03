using System.Diagnostics;
using MicroProfiler.DiagnosticsOutputting;
using MicroProfiler.Profiling;

namespace MicroProfiler.Test.Unit.Fakes
{
    public class FakeDiagnosticsEmitter : IEmitDiagnostics
    {
        public bool Called { get; set; }
        public ProfiledOperations Operations { get; set; }
        public Stopwatch ElapsedTimer { get; set; }

        public void OutputDiagnostics(ProfiledOperations operations, Stopwatch elapsedTimer)
        {
            Operations = operations;
            ElapsedTimer = elapsedTimer;
            Called = true;
        }
    }
}