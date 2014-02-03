using System;
using MicroProfiler.DiagnosticsOutputting;
using MicroProfiler.Profiling;
using MicroProfiler.ProfilingDataStorage;

namespace MicroProfiler
{
    public class MicroProfiler : IMicroProfiler
    {
        public Guid Id { get; set; }

        private readonly IStoreProfilingDataForAUnitOfWork _storage;
        private readonly IEmitDiagnostics _diagnosticOutput;

        public MicroProfiler(IStoreProfilingDataForAUnitOfWork storage, IEmitDiagnostics diagnosticOutput)
        {
            _storage = storage;
            _diagnosticOutput = diagnosticOutput;
        }

        public IProfileASingleStep Step(string label)
        {
            return _storage.Retrieve() == null
                ? new NullProfiledStep()
                : _storage.Retrieve().Step(label);
        }

        public void Start()
        {
            var profiler = new ProfiledOperations();
            _storage.Store(profiler);
        }

        public void Stop()
        {
            var currentProfile = _storage.Retrieve();
            if (currentProfile == null)
            {
                return;
            }

            currentProfile.Stop();
            
            _diagnosticOutput.OutputDiagnostics(Id, currentProfile.Tasks, currentProfile.Stopwatch);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}