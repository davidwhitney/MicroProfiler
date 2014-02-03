using MicroProfiler.Profiling;

namespace MicroProfiler.ProfilingDataStorage
{
    public interface IStoreProfilingDataForAUnitOfWork
    {
        void Store(ProfiledOperations ops);

        /// <summary>
        /// Retrieves currently active diagnostics session, returning NULL if no profiling session has been started
        /// </summary>
        /// <returns>ProfiledOperations OR null</returns>
        ProfiledOperations Retrieve();
    }
}