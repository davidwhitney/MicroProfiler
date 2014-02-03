using MicroProfiler.Profiling;
using NUnit.Framework;

namespace MicroProfiler.Test.Unit.Profiling
{
    [TestFixture]
    public class NullProfiledStepTests
    {
        [Test]
        public void Dispose_DoesNothingAtAll_DoesntCrash_DoesntThrow()
        {
            var nps = new NullProfiledStep();

            nps.Dispose();

            Assert.That(nps.Disposed, Is.True);
        }
    }
}
