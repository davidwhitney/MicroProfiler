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
            Write("Profiling session " + Id + " summary");

            foreach (var task in _tasks)
            {
                Write(task.Label + " started at: +" + task.MsFromRequestStart + "ms  -  duration: " + task.Elapsed.Milliseconds + "ms");
            }

            Write("Total time in Modules " + Stopwatch.ElapsedMilliseconds + "ms");
        }

        public void Write(string message)
        {
            _diagnosticOutput.WriteLine(message);
        }
    }
}