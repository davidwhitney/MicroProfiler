using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MicroProfiler.DiagnosticsOutputting;
using MicroProfiler.ProfilingDataStorage;

namespace MicroProfiler
{
    public static class MicroProfiler
    {
        private static IMicroProfilerStorage _storage;
        private static IList<IEmitDiagnostics> _diagnosticsOutput;
        private static Func<bool> _autoStartIfThisReturnsTrue;

        public static void Configure(IMicroProfilerStorage unitOfWorkStorage, params IEmitDiagnostics[] diagnosticsOutput)
        {
            _storage = unitOfWorkStorage;
            _diagnosticsOutput = diagnosticsOutput != null ? diagnosticsOutput.ToList() : new List<IEmitDiagnostics>();
        }

        public static void AutoStartWhen(Func<bool> thisIsTrue)
        {
            _autoStartIfThisReturnsTrue = thisIsTrue;
        }

        public static IMicroProfiler Current
        {
            get
            {
                var storage = _storage ?? new HttpProfilerPerRequestStorage(new HttpContextWrapper(HttpContext.Current));
                var diagnosticsOutput = _diagnosticsOutput ?? new List<IEmitDiagnostics> { new DiagnosticsTraceListener() };
                _autoStartIfThisReturnsTrue = _autoStartIfThisReturnsTrue ?? (() => false);

                var profiler = new MicroProfilerController(storage, diagnosticsOutput);
                if (_autoStartIfThisReturnsTrue())
                {
                    profiler.Start();
                }

                return profiler;
            }
        }
    }
}