using System.Threading;
using MicroProfiler.Profiling;
using NUnit.Framework;

namespace MicroProfiler.Test.Unit.Profiling
{
    [TestFixture]
    public class ProfiledOperationsTests
    {
        [Test]
        public void WhenStepIsDisposed_RecordsElapsedMsAgainstStep()
        {
            var op = new ProfiledOperations();

            using (op.Step("test"))
            {
                Thread.Sleep(1);
            }

            Assert.That(op.Tasks[0].Label, Is.EqualTo("test"));
            Assert.That(op.Tasks[0].ElapsedMsInRequest, Is.Not.EqualTo(0));
        }

        [Test]
        public void Stop_StopsOverallTimer()
        {
            var op = new ProfiledOperations();
            Assert.That(op.Stopwatch.IsRunning, Is.True);

            op.Stop();
            Assert.That(op.Stopwatch.IsRunning, Is.False);
        }
    }
}
