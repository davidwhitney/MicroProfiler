using System.Web;
using MicroProfiler.DiagnosticsOutputting;
using MicroProfiler.ProfilingDataStorage;

namespace MicroProfiler
{
    public static class MicroProfiler
    {
        private static IMicroProfilerStorage _storage;
        private static IEmitDiagnostics _diagnosticsOutput;

        public static void Configure(IMicroProfilerStorage unitOfWorkStorage, IEmitDiagnostics diagnosticsOutput = null)
        {
            _storage = unitOfWorkStorage;
            _diagnosticsOutput = diagnosticsOutput ?? new DiagnosticsTraceListener();
        }

        public static IMicroProfiler Current
        {
            get
            {
                var storage = _storage ?? new HttpProfilerPerRequestStorage(new HttpContextWrapper(HttpContext.Current));
                var diagnosticsOutput = _diagnosticsOutput ?? new DiagnosticsTraceListener();

                var profiler = new MicroProfilerController(storage, diagnosticsOutput);
                if (storage.Retrieve() == null)
                {
                    profiler.Start();
                }

                return profiler;
            }
        }
    }
}