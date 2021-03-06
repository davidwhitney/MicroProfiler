﻿using System;
using System.Diagnostics;

namespace MicroProfiler.Profiling
{
    public class MicroProfilerProfiledStep : IProfileASingleStep
    {
        private readonly Action<MicroProfilerProfiledStep> _onCompletion;
        private readonly Stopwatch _stopwatch;

        public string Label { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public TimeSpan Elapsed { get; set; }

        public long MsFromRequestStart { get; set; }
        public long ElapsedMsInRequest { get; set; }

        public bool Finished
        {
            get { return !_stopwatch.IsRunning; }
        }

        public MicroProfilerProfiledStep(string label, Action<MicroProfilerProfiledStep> onCompletion, long currentTimeOnClock)
        {
            MsFromRequestStart = currentTimeOnClock;

            _onCompletion = onCompletion;
            Label = label;

            _stopwatch = Stopwatch.StartNew();
            Start = DateTime.Now;
        }

        public void Dispose()
        {
            End = DateTime.Now;
            _stopwatch.Stop();

            Elapsed = _stopwatch.Elapsed;

            _onCompletion(this);
        }
    }
}