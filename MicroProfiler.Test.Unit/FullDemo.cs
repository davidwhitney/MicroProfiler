using System;
using System.Threading;
using MicroProfiler.DiagnosticsOutputting;
using MicroProfiler.Test.Unit.Fakes;
using NUnit.Framework;

namespace MicroProfiler.Test.Unit
{
    [TestFixture]
    public class FullDemo
    {
        [Test]
        [Ignore]
        public void RunExampleCode()
        {
            // Bootstrapping code
            MicroProfiler.Configure(new FakeStorage(), new DiagnosticsTraceListener());
            // end

            MicroProfiler.Current.Start();


            using (MicroProfiler.Current.Step("My outer loop"))
            {
                Thread.Sleep(1000);

                using (MicroProfiler.Current.Step("My inner loop"))
                {
                    Thread.Sleep(1000);
                }
            }


            MicroProfiler.Current.Stop();
        }
    }
}