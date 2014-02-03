using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MicroProfiler.Profiling
{
    public class ProfiledOperations
    {
        public Guid Id { get; set; }
        public Stopwatch Timer { get; set; }
        public List<MicroProfilerProfiledStep> Tasks { get; private set; }

        public ProfiledOperations()
        {
            Id = Guid.NewGuid();
            Tasks = new List<MicroProfilerProfiledStep>();
            Timer = Stopwatch.StartNew();
        }

        public IProfileASingleStep Step(string label)
        {
            var task = new MicroProfilerProfiledStep(label, profiledStep =>
            {
                profiledStep.ElapsedMsInRequest = Timer.ElapsedMilliseconds;
            }, Timer.ElapsedMilliseconds);

            Tasks.Add(task);
            return task;
        }

        public void Stop()
        {
            Timer.Stop();
        }
    }
}