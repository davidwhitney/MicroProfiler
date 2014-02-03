using MicroProfiler.Profiling;
using MicroProfiler.ProfilingDataStorage;

namespace MicroProfiler.Test.Unit.Fakes
{
    public class FakeStorage : IMicroProfilerStorage
    {
        private ProfiledOperations _ops;
        public bool StoreCalled { get; set; }

        public FakeStorage(bool makeActive = false)
        {
            if (makeActive)
            {
                _ops = new ProfiledOperations();
            }
        }

        public void Store(ProfiledOperations ops)
        {
            StoreCalled = true;
            _ops = ops;
        }

        public ProfiledOperations Retrieve()
        {
            return _ops;
        }
    }
}