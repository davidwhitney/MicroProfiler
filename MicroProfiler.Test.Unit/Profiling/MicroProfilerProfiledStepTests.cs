using System;
using System.Threading;
using MicroProfiler.Profiling;
using NUnit.Framework;

namespace MicroProfiler.Test.Unit.Profiling
{
    [TestFixture]
    public class MicroProfilerProfiledStepTests
    {
        [Test]
        public void Ctor_InitilisedWithGoodStartState()
        {
            const string label = "this is a description";
            const int timeFromStart = 999;

            var step = new MicroProfilerProfiledStep(label, profiledStep => { }, timeFromStart);

            Assert.That(step.Label, Is.EqualTo(label));
            Assert.That(step.MsFromRequestStart, Is.EqualTo(999));
            Assert.That(step.Start, Is.Not.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void Dispose_HasTimedItsExecution()
        {
            var step = new MicroProfilerProfiledStep("test", profiledStep => { }, 0);
            
            Thread.Sleep(100);
            step.Dispose();

            Assert.That(step.Elapsed, Is.Not.EqualTo(new TimeSpan(0)));
            Assert.That(step.End, Is.Not.EqualTo(DateTime.MinValue));
        }

        [Test]
        public void Dispose_MarksStepAsFinished()
        {
            var step = new MicroProfilerProfiledStep("test", profiledStep => { }, 0);

            step.Dispose();

            Assert.That(step.Finished, Is.True);
        }

        [Test]
        public void Dispose_InvokesCallbackOnCompletion()
        {
            var calledBack = false;
            var step = new MicroProfilerProfiledStep("test", profiledStep => { calledBack = true; }, 0);

            step.Dispose();

            Assert.That(calledBack, Is.True);
        }

    }
}
