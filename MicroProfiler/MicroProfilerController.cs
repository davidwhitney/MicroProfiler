using MicroProfiler.DiagnosticsOutputting;
using MicroProfiler.Profiling;
using MicroProfiler.ProfilingDataStorage;

namespace MicroProfiler
{
    public class MicroProfilerController : IMicroProfiler
    {
        public IMicroProfilerStorage Storage { get; private set; }
        public IEmitDiagnostics DiagnosticOutput { get; private set; }

        public MicroProfilerController(IMicroProfilerStorage storage, IEmitDiagnostics diagnosticOutput)
        {
            Storage = storage;
            DiagnosticOutput = diagnosticOutput;
        }

        public IMicroProfiler Start()
        {
            var profiler = new ProfiledOperations();
            Storage.Store(profiler);
            return this;
        }

        public IProfileASingleStep Step(string label)
        {
            return Storage.Retrieve() == null
                ? new NullProfiledStep()
                : Storage.Retrieve().Step(label);
        }

        public void Stop()
        {
            var currentProfile = Storage.Retrieve();
            if (currentProfile == null)
            {
                return;
            }

            currentProfile.Stop();
            
            DiagnosticOutput.OutputDiagnostics(currentProfile.Tasks, currentProfile.Timer);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}