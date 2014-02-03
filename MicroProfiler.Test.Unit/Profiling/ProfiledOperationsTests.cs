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
                // Some timed operation here
            }

            Assert.That(op.Tasks[0].Label, Is.EqualTo("test"));
            Assert.That(op.Tasks[0].ElapsedMsInRequest, Is.Not.EqualTo(0));
        }
    }
}
