﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MicroProfiler.Profiling
{
    public class ProfiledOperations
    {
        public Guid Id { get; set; }
        public Stopwatch Stopwatch { get; set; }
        public List<MicroProfilerProfiledStep> Tasks { get; private set; }

        public ProfiledOperations()
        {
            Id = Guid.NewGuid();
            Tasks = new List<MicroProfilerProfiledStep>();

            Stopwatch = new Stopwatch();
            Stopwatch.Start();
        }

        public IProfileASingleStep Step(string label)
        {
            var task = new MicroProfilerProfiledStep(label, RegisterTaskFinish, Stopwatch.ElapsedMilliseconds);

            Tasks.Add(task);
            return task;
        }

        public void RegisterTaskFinish(MicroProfilerProfiledStep task)
        {
            task.ElapsedMsInRequest = Stopwatch.ElapsedMilliseconds;
        }

        public void Stop()
        {
            Stopwatch.Stop();
        }
    }
}