using MicroProfiler.Profiling;
using MicroProfiler.ProfilingDataStorage;

namespace MicroProfiler.Test.Unit.Fakes
{
    public class FakeStorage : IMicroProfilerStorage
    {
        public ProfiledOperations Ops { get; set; }
        public bool StoreCalled { get; set; }

        public FakeStorage(bool makeActive = false)
        {
            if (makeActive)
            {
                Ops = new ProfiledOperations();
            }
        }

        public void Store(ProfiledOperations ops)
        {
            StoreCalled = true;
            Ops = ops;
        }

        public ProfiledOperations Retrieve()
        {
            return Ops;
        }
    }
}