using MicroProfiler.Profiling;
using MicroProfiler.Test.Unit.Fakes;
using NUnit.Framework;

namespace MicroProfiler.Test.Unit
{
    [TestFixture]
    public class MicroProfilerControllerTests
    {
        private FakeDiagnosticsEmitter _diag;

        [SetUp]
        public void SetUp()
        {
            _diag = new FakeDiagnosticsEmitter();
        }

        [Test]
        public void Start_StoresNewState()
        {
            var storage = new FakeStorage();
            var mp = new MicroProfilerController(storage, null);

            mp.Start();

            Assert.That(storage.StoreCalled, Is.True);
        }

        [Test]
        public void Stop_WithActiveState_CallsDiagnosticsEmitterWithStoredState()
        {
            var storage = new FakeStorage(true);
            var mp = new MicroProfilerController(storage, _diag);

            mp.Stop();

            Assert.That(_diag.Called, Is.True);
        }

        [Test]
        public void Stop_WithNoActiveState_DoesntOutputAnything()
        {
            var storage = new FakeStorage();
            var mp = new MicroProfilerController(storage, _diag);

            mp.Stop();

            Assert.That(_diag.Called, Is.False);
        }

        [Test]
        public void Step_CalledWithoutStartingProfiling_ReturnsANullProfilingStep()
        {
            var storage = new FakeStorage();
            var mp = new MicroProfilerController(storage, _diag);

            var step = mp.Step("something");

            Assert.That(step, Is.TypeOf<NullProfiledStep>());
        }

        [Test]
        public void Step_CalledAfterStartingProfiling_ReturnsAProfilingStep()
        {
            var storage = new FakeStorage(true);
            var mp = new MicroProfilerController(storage, _diag);

            var step = mp.Step("something");

            Assert.That(step, Is.TypeOf<MicroProfilerProfiledStep>());
        }

        [Test]
        public void Stop_AfterStepping_CallsDiagnosticsWithStepsCreated()
        {
            var storage = new FakeStorage(true);
            var mp = new MicroProfilerController(storage, _diag);

            var step1 = mp.Step("a");
            mp.Stop();

            Assert.That(_diag.Operations.Tasks[0], Is.EqualTo(step1));
        }

        [Test]
        public void Dispose_CallsStopAndCallsDiagnostics()
        {
            var storage = new FakeStorage(true);
            var mp = new MicroProfilerController(storage, _diag);

            mp.Dispose();

            Assert.That(_diag.Called, Is.True);
        }
        
    }
}
