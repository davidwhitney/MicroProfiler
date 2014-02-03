using System;
using MicroProfiler.Profiling;

namespace MicroProfiler
{
    public interface IMicroProfiler : IDisposable
    {
        Guid Id { get; set; }
        IProfileASingleStep Step(string label);
        void Start();
        void Stop();
    }
}
