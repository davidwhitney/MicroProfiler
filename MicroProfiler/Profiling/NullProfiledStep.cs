namespace MicroProfiler.Profiling
{
    public class NullProfiledStep : IProfileASingleStep
    {
        public bool Disposed { get; private set; }

        public void Dispose()
        {
            Disposed = true;
        }
    }
}