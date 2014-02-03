using System;
using System.Collections.Generic;
using System.Diagnostics;
using MicroProfiler.DiagnosticsOutputting;

namespace MicroProfiler.Profiling
{
    public class ProfiledOperations
    {
        public Guid Id { get; set; }
        public Stopwatch Stopwatch { get; set; }

        private readonly List<MicroProfilerProfiledStep> _tasks;
        private readonly IEmitDiagnostics _diagnosticOutput;

        public ProfiledOperations(IEmitDiagnostics diagnosticOutput)
        {
            _diagnosticOutput = diagnosticOutput;
            Id = Guid.NewGuid();
            _tasks = new List<MicroProfilerProfiledStep>();

            Stopwatch = new Stopwatch();
            Stopwatch.Start();
        }

        public IProfileASingleStep Step(string label)
        {
            var task = new MicroProfilerProfiledStep(label, RegisterTaskFinish, Stopwatch.ElapsedMilliseconds);

            _tasks.Add(task);
            return task;
        }

        public void RegisterTaskFinish(MicroProfilerProfiledStep task)
        {
            task.ElapsedMsInRequest = Stopwatch.ElapsedMilliseconds;
        }

        public void Dispose()
        {
            Stopwatch.Stop();
            _diagnosticOutput.OutputDiagnostics(Id, _tasks, Stopwatch);
        }
    }
}