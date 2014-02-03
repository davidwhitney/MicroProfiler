using MicroProfiler.Profiling;

namespace MicroProfiler.ProfilingDataStorage
{
    public interface IStoreProfilingDataForAUnitOfWork
    {
        void Store(ProfiledOperations ops);
        ProfiledOperations Retrieve();
    }
}