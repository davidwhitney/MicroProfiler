using System.Linq;
using MicroProfiler.Test.Unit.Fakes;
using NUnit.Framework;

namespace MicroProfiler.Test.Unit
{
    [TestFixture]
    public class MicroProfilerTests
    {
        [Test]
        public void Current_ReturnsInstanceWithConfiguredStorage()
        {
            var storage = new FakeStorage();
            MicroProfiler.Configure(storage); 
            
            var currentMp = MicroProfiler.Current;

            Assert.That(currentMp.Storage, Is.EqualTo(storage));
        }

        [Test]
        public void Current_ReturnsInstanceWithConfiguredDiagnosticsEmitter()
        {
            var diagnosticsEmitter = new FakeDiagnosticsEmitter();
            MicroProfiler.Configure(new FakeStorage(), diagnosticsEmitter); 

            var currentMp = MicroProfiler.Current;

            Assert.That(currentMp.DiagnosticOutput.ToList()[0], Is.EqualTo(diagnosticsEmitter));
        }

        [Test]
        public void Current_NoActiveSession_ReturnsAProfilerThatIsntRecording()
        {
            var storage = new FakeStorage();
            MicroProfiler.Configure(storage); 
            
            var currentMp = MicroProfiler.Current;

            Assert.That(currentMp, Is.Not.Null);
            Assert.That(storage.StoreCalled, Is.False);
        }

        [Test]
        public void Current_ActiveSession_ReturnsWithoutStartingAgain()
        {
            var storageAlreadyStarted = new FakeStorage(true);
            MicroProfiler.Configure(storageAlreadyStarted); 

            var currentMp = MicroProfiler.Current;

            Assert.That(currentMp, Is.Not.Null);
            Assert.That(storageAlreadyStarted.StoreCalled, Is.False);
        }
    }
}
